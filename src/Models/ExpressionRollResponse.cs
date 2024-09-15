using System;

namespace Dicer.Models;

internal class ExpressionRollResponse
{
    public ExpressionRollResponse(ExpressionResponse? result, IRoundingStrategy roundingStrategy)
    {
        var value = result?.Result ?? 0;

        IsNegative = value < 0;
        Result = (int)roundingStrategy.Round(Math.Abs(value));
    }

    public int Result { get; }
    public bool IsNegative { get; }
}