using System;

namespace Dicer.Exceptions;

public class ParsingException : Exception
{
    public ParsingException(string message) : base(message)
    {
    }
}