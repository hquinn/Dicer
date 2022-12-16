namespace Dicer;

internal abstract record BaseNode : INode
{
	public NodeResponse Evaluate(Roller roller, RoundingStrategy roundingStrategy)
	{
		return Evaluate(roller.Create(), roundingStrategy.Create());
	}

	internal abstract NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy);
}