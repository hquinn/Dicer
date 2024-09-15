using System;

namespace Dicer.Tokens;

internal readonly struct Token
{
    public readonly double? Constant;

    public readonly TokenType TokenType;

    public Token(double? constant)
    {
        TokenType = TokenType.Number;
        Constant = constant;
    }

    public Token(TokenType tokenType)
    {
        TokenType = tokenType;
        Constant = null;
    }

    public override string ToString()
    {
        return TokenType switch
        {
            TokenType.Number => Constant?.ToString() ?? "N/A",
            TokenType.Add => "+",
            TokenType.Subtract => "-",
            TokenType.Multiply => "*",
            TokenType.Divide => "/",
            TokenType.Dice => "D",
            TokenType.Keep => "K",
            TokenType.Minimum => "M",
            TokenType.Open => "(",
            TokenType.Close => ")",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}