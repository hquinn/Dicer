﻿using System;

namespace Dicer;

public class ParsingException : Exception
{
    public ParsingException(string message) : base(message)
    {
    }
}