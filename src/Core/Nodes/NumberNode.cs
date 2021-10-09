using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
/// Node that represents a number.
/// </summary>
public class NumberNode : INode
{
	private readonly double _number;

	public NumberNode(double number)
	{
		_number = number;
	}

	/// <inheritdoc />
	public NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy)
	{
		return new(roundingStrategy.Round(_number));
	}

	public override string ToString()
	{
		return $"{_number}";
	}
}