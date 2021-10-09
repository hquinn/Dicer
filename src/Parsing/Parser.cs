using Dicer.Nodes;
using Dicer.Parser.Exceptions;
using Dicer.Parser.Tokens;
using System.Collections.Generic;
using static Dicer.Parser.Tokenizer;

namespace Dicer.Parser;

public static class Parser
{
	/// <summary>
	/// Parses the <paramref name="input"/> into an <see cref="INode"/> expression tree.
	/// </summary>
	/// <param name="input">The mathematical expression to parse.</param>
	/// <returns><see cref="INode"/> expression tree.</returns>
	/// <exception cref="ParsingException">For invalid characters in <see cref="input"/></exception>
	public static INode Parse(string input)
	{
		var tokens = new LinkedList<Token>(Tokenize(input));
		var token = tokens.First;
		var node = ParseAddSubtract(ref token);

		if (token is not null)
		{
			throw new ParsingException($"Token {token.Value.Value} is invalid at this position");
		}

		return node;
	}

	private static INode ParseAddSubtract(ref LinkedListNode<Token>? token)
	{
		var lhs = ParseMultiplyDivide(ref token);

		while (token is not null)
		{
			Token? currentToken = null;

			if (token.Value is AddToken add)
			{
				currentToken = add;
			}
			else if (token.Value is SubtractToken subtract)
			{
				currentToken = subtract;
			}

			if (currentToken is null)
			{
				return lhs;
			}

			Increment(ref token);

			var rhs = ParseMultiplyDivide(ref token);

			if (currentToken is AddToken)
			{
				lhs = new AddNode(lhs, rhs);
			}
			else
			{
				lhs = new SubtractNode(lhs, rhs);
			}
		}
		return lhs;
	}

	private static INode ParseMultiplyDivide(ref LinkedListNode<Token>? token)
	{
		var lhs = ParseDice(ref token);

		while (token is not null)
		{
			Token? currentToken = null;

			if (token.Value is MultiplyToken multiply)
			{
				currentToken = multiply;
			}
			else if (token.Value is DivideToken divide)
			{
				currentToken = divide;
			}

			if (currentToken is null)
			{
				return lhs;
			}

			Increment(ref token);

			var rhs = ParseDice(ref token);

			if (currentToken is MultiplyToken)
			{
				lhs = new MultiplyNode(lhs, rhs);
			}
			else
			{
				lhs = new DivideNode(lhs, rhs);
			}
		}

		return lhs;
	}

	private static INode ParseDice(ref LinkedListNode<Token>? token)
	{
		var lhs = ParseUnary(ref token);

		while (token is not null)
		{
			Token? currentToken = null;

			if (token.Value is DiceToken dice)
			{
				currentToken = dice;
			}

			if (currentToken is null)
			{
				return lhs;
			}

			Increment(ref token);

			var rhs = ParseUnary(ref token);

			if (token?.Value is KeepToken)
			{
				Increment(ref token);
				var khs = ParseUnary(ref token);
				lhs = new DiceNode(lhs, rhs, khs);
			}

			else if (currentToken is DiceToken)
			{
				lhs = new DiceNode(lhs, rhs);
			}
		}

		return lhs;
	}

	private static INode ParseUnary(ref LinkedListNode<Token>? token)
	{
		while (token is not null)
		{
			if (token.Value is AddToken)
			{
				Increment(ref token);

				continue;
			}
			if (token.Value is SubtractToken)
			{
				Increment(ref token);

				return new UnaryNode(ParseLeaf(ref token));
			}
			return ParseLeaf(ref token);
		}

		return ParseLeaf(ref token);
	}

	private static INode ParseLeaf(ref LinkedListNode<Token>? token)
	{
		if (token?.Value is NumberToken number)
		{
			Increment(ref token);
			return new NumberNode(number.Constant);
		}

		if (token?.Value is OpenToken)
		{
			Increment(ref token);
			var node = ParseAddSubtract(ref token);

			if (token?.Value is not CloseToken)
			{
				throw new ParsingException("No corresponding closing token");
			}

			Increment(ref token);

			return node;
		}

		throw new ParsingException($"Token {token?.Value.Value} is invalid at this position");
	}

	private static void Increment(ref LinkedListNode<Token>? token)
	{
		token = token?.Next;
	}
}