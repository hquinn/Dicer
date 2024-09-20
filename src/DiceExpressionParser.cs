using System.Collections.Generic;
using Dicer.Exceptions;
using Dicer.Nodes;
using Dicer.Tokens;

namespace Dicer;

public static class DiceExpressionParser
{
    /// <summary>
    ///     Parses the <paramref name="input" /> into an <see cref="IDiceExpression" /> expression tree.
    /// </summary>
    public static IDiceExpression Parse(string input)
    {
        var tokens = Tokenizer.Tokenize(input);
        var index = 0;
        var node = ParseAddSubtract(tokens, ref index);

        if (index < tokens.Count)
        {
            throw new ParsingException($"Token {tokens[index]} is invalid at this position");
        }

        return node;
    }

    private static BaseNode ParseAddSubtract(List<Token> tokens, ref int index)
    {
        var lhs = ParseMultiplyDivide(tokens, ref index);

        while (index < tokens.Count)
        {
            var currentToken = tokens[index];

            if (currentToken.TokenType is TokenType.Add or TokenType.Subtract)
            {
                index++;
                var rhs = ParseMultiplyDivide(tokens, ref index);

                lhs = currentToken.TokenType == TokenType.Add
                    ? new AddNode(lhs, rhs)
                    : new SubtractNode(lhs, rhs);
            }
            else
            {
                return lhs;
            }
        }

        return lhs;
    }

    private static BaseNode ParseMultiplyDivide(List<Token> tokens, ref int index)
    {
        var lhs = ParseDice(tokens, ref index);

        while (index < tokens.Count)
        {
            var currentToken = tokens[index];

            if (currentToken.TokenType is TokenType.Multiply or TokenType.Divide)
            {
                index++;
                var rhs = ParseDice(tokens, ref index);

                lhs = currentToken.TokenType == TokenType.Multiply
                    ? new MultiplyNode(lhs, rhs)
                    : new DivideNode(lhs, rhs);
            }
            else
            {
                return lhs;
            }
        }

        return lhs;
    }

    private static BaseNode ParseDice(List<Token> tokens, ref int index)
    {
        var lhs = ParseUnary(tokens, ref index); // Parse the left-hand side (before 'd')

        while (index < tokens.Count)
        {
            // Check for 'Dice' token
            if (tokens[index].TokenType == TokenType.Dice)
            {
                index++;
                var rhs = ParseUnary(tokens, ref index); // Parse right-hand side (after 'd')

                // Prepare placeholders for 'Keep' and 'Minimum' nodes
                BaseNode? khs = null;
                BaseNode? mhs = null;

                // Check for 'Keep' or 'Minimum' tokens (in any order)
                while (index < tokens.Count && tokens[index].TokenType is TokenType.Keep or TokenType.Minimum)
                {
                    if (tokens[index].TokenType is TokenType.Keep)
                    {
                        index++; // Move past the 'Keep' token
                        khs = ParseUnary(tokens, ref index); // Parse the expression after 'Keep'
                    }
                    else if (tokens[index].TokenType is TokenType.Minimum)
                    {
                        index++; // Move past the 'Minimum' token
                        mhs = ParseUnary(tokens, ref index); // Parse the expression after 'Minimum'
                    }
                }

                // Now construct the DiceNode based on what we have
                if (khs is not null && mhs is not null)
                {
                    lhs = new DiceNode(lhs, rhs, khs, mhs); // Dice with both Keep and Minimum
                }
                else if (khs is not null)
                {
                    lhs = new DiceNode(lhs, rhs, khs); // Dice with only Keep
                }
                else if (mhs is not null)
                {
                    lhs = new DiceNode(lhs, rhs, null, mhs); // Dice with only Minimum
                }
                else
                {
                    lhs = new DiceNode(lhs, rhs); // Just Dice without Keep or Minimum
                }
            }
            else
            {
                return lhs; // Return if no Dice token found
            }
        }

        return lhs;
    }

    private static BaseNode ParseUnary(List<Token> tokens, ref int index)
    {
        while (index < tokens.Count)
        {
            switch (tokens[index].TokenType)
            {
                case TokenType.Add:
                    index++; // Ignore unary plus
                    continue;

                case TokenType.Subtract:
                    index++;
                    return new UnaryNode(ParseUnary(tokens, ref index));

                default:
                    return ParseLeaf(tokens, ref index);
            }
        }

        return ParseLeaf(tokens, ref index);
    }

    private static BaseNode ParseLeaf(List<Token> tokens, ref int index)
    {
        if (index >= tokens.Count)
        {
            throw new ParsingException("Unexpected end of input");
        }

        // Check for a number
        if (tokens[index].TokenType == TokenType.Number)
        {
            var number = tokens[index].Constant!.Value;
            index++;
            return new NumberNode(number);
        }

        // Check for an open parenthesis
        if (tokens[index].TokenType == TokenType.Open)
        {
            index++; // Move past the '(' token
            var node = ParseAddSubtract(tokens, ref index); // Parse the expression inside parentheses

            // Check for a closing parenthesis
            if (index >= tokens.Count || tokens[index].TokenType != TokenType.Close)
            {
                throw new ParsingException("No corresponding closing parenthesis found");
            }

            index++; // Move past the ')' token
            return node;
        }

        // If no valid token is found, throw a ParsingException
        throw new ParsingException($"Invalid token '{tokens[index]}' at position {index}");
    }
}