using System;

namespace Dicer.Parsing.Exceptions;

public class ParsingException : Exception
{
	public ParsingException(string message) : base(message)
	{
	}
}