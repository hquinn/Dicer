using Dicer.Models;
using Dicer.Rollers;
using Dicer.Rounding;

namespace Dicer.Nodes;

/// <summary>
/// Represents a node in an expression tree, used for calculating mathematical expressions.
/// </summary>
public interface INode
{
	/// <summary>
	/// Calculates the node.
	/// </summary>
	/// <param name="roller">The <see cref="IRoller"/> to use when rolling dice.</param>
	/// <param name="roundingStrategy">The <see cref="IRoundingStrategy"/> to use when rounding values.</param>
	/// <returns>The <see cref="NodeResponse"/> of the resulting calculations.</returns>
	NodeResponse Evaluate(IRoller roller, IRoundingStrategy roundingStrategy);
}