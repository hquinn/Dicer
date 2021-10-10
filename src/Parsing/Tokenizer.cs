using Dicer.Parser.Exceptions;
using Dicer.Parser.Tokens;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dicer.Parser;

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
				yield return new NumberToken(ParseNumber(reader));
			}

			else if (character == '+')
			{
				yield return new AddToken();
				reader.Read();
			}

			else if (character == '-')
			{
				yield return new SubtractToken();
				reader.Read();
			}

			else if (character == '*')
			{
				yield return new MultiplyToken();
				reader.Read();
			}

			else if (character == '/')
			{
				yield return new DivideToken();
				reader.Read();
			}

			else if (character is 'd' or 'D')
			{
				yield return new DiceToken();
				reader.Read();
			}

			else if (character is 'k' or 'K')
			{
				yield return new KeepToken();
				reader.Read();
			}

			else if (character == '(')
			{
				yield return new OpenToken();
				reader.Read();
			}

			else if (character == ')')
			{
				yield return new CloseToken();
				reader.Read();
			}

			else if (char.IsWhiteSpace(character))
			{
				reader.Read();
			}

			else throw new ParsingException($"Token '{character}' cannot be tokenized.");
		}
	}

	private static string ParseNumber(TextReader reader)
	{
		var builder = new StringBuilder();

		do
		{
			builder.Append((char)reader.Read());
		} while (char.IsDigit((char)reader.Peek()));

		return builder.ToString();
	}
}