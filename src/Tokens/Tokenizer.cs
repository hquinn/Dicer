using System;
using System.Collections.Generic;

namespace Dicer.Tokens;

internal static class Tokenizer
{
    internal static List<Token> Tokenize(string input)
    {
        var tokens = new List<Token>(input.Length);
        var length = input.Length;

        for (var i = 0; i < length; i++)
        {
            var character = input[i];

            if (char.IsDigit(character))
                tokens.Add(new Token(ParseNumber(input.AsSpan(), ref i)));
            else
                AddOperatorToken(character, tokens);
        }

        return tokens;
    }

    private static double ParseNumber(ReadOnlySpan<char> input, ref int index)
    {
        var start = index;
        var dotCount = 0;

        for (; index < input.Length; index++)
        {
            var current = input[index];
            if (current == '.')
            {
                dotCount++;

                if (dotCount > 1) break;
            }
            else if (!char.IsDigit(current))
            {
                break;
            }
        }

        index--; // Adjust for the outer loop increment in Tokenize
        return double.Parse(input.Slice(start, index - start + 1)); // Adjusted for inclusive parsing
    }

    private static void AddOperatorToken(char character, List<Token> tokens)
    {
        switch (character)
        {
            case '+':
                tokens.Add(new Token(TokenType.Add));
                break;
            case '-':
                tokens.Add(new Token(TokenType.Subtract));
                break;
            case '*':
                tokens.Add(new Token(TokenType.Multiply));
                break;
            case '/':
                tokens.Add(new Token(TokenType.Divide));
                break;
            case 'd' or 'D':
                tokens.Add(new Token(TokenType.Dice));
                break;
            case 'k' or 'K':
                tokens.Add(new Token(TokenType.Keep));
                break;
            case 'm' or 'M':
                tokens.Add(new Token(TokenType.Minimum));
                break;
            case '(':
                tokens.Add(new Token(TokenType.Open));
                break;
            case ')':
                tokens.Add(new Token(TokenType.Close));
                break;
            default:
                if (!char.IsWhiteSpace(character))
                    throw new ParsingException($"Token '{character}' cannot be tokenized.");
                break;
        }
    }
}