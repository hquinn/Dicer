namespace Dicer;

/// <summary>
///     Node that represents a number.
/// </summary>
internal record NumberNode(double Number) : BaseNode
{
	internal override NodeResponse Evaluate(IRoller roller, IRoundingStrategy diceRoundingStrategy)
	{
		return new(Number);
	}

	public override string ToString()
	{
		return $"{Number}";
	}
}