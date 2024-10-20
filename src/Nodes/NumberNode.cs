﻿using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
///     Node that represents a number.
/// </summary>
internal record NumberNode(double Number) : DiceExpression
{
    internal override ExpressionResponse Evaluate(IDiceRoller roller, IRoundingStrategy diceRoundingStrategy)
    {
        return new ExpressionResponse(Number);
    }

    public override string ToString()
    {
        return $"{Number}";
    }
}