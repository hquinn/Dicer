using System;

namespace Dicer.Parser.Exceptions;

public class ParsingException : Exception
{
	public ParsingException(string message) : base(message) { }
}