namespace Dicer.Tests.Factories;

public class NodeResponseFactory
{
	public static NodeResponse CreateSimpleResponse(double value)
	{
		return new(value);
	}
}