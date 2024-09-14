using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dicer;

internal static class Tokenizer
{
	internal static IEnumerable<Token> Tokenize(string input)
	{
		var reader = new StringReader(input);

		while (reader.Peek() != -1)
		{
			var character = (char)reader.Peek();

			if (char.IsDigit(character))
			{
				yield return new Token(ParseNumber(reader));
			}

			else if (character == '+')
			{
				yield return new Token(TokenType.Add);

				reader.Read();
			}

			else if (character == '-')
			{
				yield return new Token(TokenType.Subtract);

				reader.Read();
			}

			else if (character == '*')
			{
				yield return new Token(TokenType.Multiply);

				reader.Read();
			}

			else if (character == '/')
			{
				yield return new Token(TokenType.Divide);

				reader.Read();
			}

			else if (character is 'd' or 'D')
			{
				yield return new Token(TokenType.Dice);

				reader.Read();
			}

			else if (character is 'k' or 'K')
			{
				yield return new Token(TokenType.Keep);

				reader.Read();
			}

			else if (character is 'm' or 'M')
			{
				yield return new Token(TokenType.Minimum);

				reader.Read();
			}

			else if (character == '(')
			{
				yield return new Token(TokenType.Open);

				reader.Read();
			}

			else if (character == ')')
			{
				yield return new Token(TokenType.Close);

				reader.Read();
			}

			else if (char.IsWhiteSpace(character))
			{
				reader.Read();
			}

			else
			{
				throw new ParsingException($"Token '{character}' cannot be tokenized.");
			}
		}
	}

	private static double ParseNumber(TextReader reader)
	{
		var builder = new StringBuilder();
		var dotCount = 0;

		do
		{
			var character = (char)reader.Read();

			if (character == '.')
			{
				dotCount++;

				if (dotCount > 1)
				{
					throw new ParsingException("Number has too many full stops");
				}
			}

			builder.Append(character);
		} while (char.IsDigit((char)reader.Peek()) || (char)reader.Peek() == '.');

		return double.Parse(builder.ToString());
	}
}