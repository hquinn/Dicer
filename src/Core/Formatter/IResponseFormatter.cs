using Dicer.Models;
using System.Collections.Generic;

namespace Dicer.Formatter;

public interface IResponseFormatter
{
	string Format(NodeResponse response, params string[] args);

	string Format(IEnumerable<NodeResponse> responses, params string[] args);
}