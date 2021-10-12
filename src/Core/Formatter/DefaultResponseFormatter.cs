using Dicer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicer.Formatter;

public class DefaultResponseFormatter : IResponseFormatter
{
	public string Format(NodeResponse response, params string[] args)
	{
		var builder = new StringBuilder();
		builder.AppendLine($"{GetHeader(args)}: {response.Result}");
		OutputRolls(response, builder);

		return builder.ToString();
	}

	public string Format(IEnumerable<NodeResponse> responses, params string[] args)
	{
		var builder = new StringBuilder();
		var count = 1;

		foreach (var response in responses)
		{
			builder.AppendLine(Format(response, args.Append(count.ToString()).ToArray()));
			count++;
		}

		return builder.ToString();
	}

	private static string GetHeader(string[] args)
	{
		return args.Any() ? string.Join(" ", args) : "Result";
	}

	private static void OutputRolls(NodeResponse response, StringBuilder builder)
	{
		foreach (var (_, rolls) in response.RollResponses!)
		{
			var rollArray = rolls as Roll[] ?? rolls.ToArray();

			if (rollArray.Any())
			{
				builder.AppendLine($"Die Size({rollArray.First().DieSize}): [{string.Join(", ", rollArray.Select(x => x.Result))}]");
			}
		}
	}
}