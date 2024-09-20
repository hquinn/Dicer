using System;
using System.Collections.Generic;
using Dicer.Helpers;

namespace Dicer;

/// <summary>
///     Represents the result from calculating an <see cref="DiceExpression" />.
/// </summary>
public record ExpressionResponse
{
    public ExpressionResponse(double result, IReadOnlyCollection<RollResponse>? rollResponses = null)
    {
        Result = result;
        RollResponses = rollResponses ?? Array.Empty<RollResponse>();
    }

    /// <summary>
    ///     Final result of the calculation from the <see cref="DiceExpression" />.
    /// </summary>
    public double Result { get; }

    /// <summary>
    ///     All rolls and results which occurred from the <see cref="DiceExpression" />.
    /// </summary>
    public IReadOnlyCollection<RollResponse> RollResponses { get; }

    internal static ExpressionResponse Plus(ExpressionResponse first, ExpressionResponse second)
    {
        var calculation = first.Result + second.Result;

        return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
    }

    internal static ExpressionResponse Minus(ExpressionResponse first, ExpressionResponse second)
    {
        var calculation = first.Result - second.Result;

        return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
    }

    internal static ExpressionResponse Times(ExpressionResponse first, ExpressionResponse second)
    {
        var calculation = first.Result * second.Result;

        return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
    }

    internal static ExpressionResponse Divide(ExpressionResponse first, ExpressionResponse second)
    {
        var calculation = first.Result / second.Result;

        return CreateNodeResponse(calculation, first.RollResponses, second.RollResponses);
    }

    internal static ExpressionResponse Unary(ExpressionResponse expression)
    {
        var calculation = -expression.Result;

        return CreateNodeResponse(calculation, expression.RollResponses);
    }

    private static ExpressionResponse CreateNodeResponse(double result,
        params IReadOnlyCollection<RollResponse>[] rolls)
    {
        return new ExpressionResponse(result, RollHelpers.Merge(rolls));
    }
}