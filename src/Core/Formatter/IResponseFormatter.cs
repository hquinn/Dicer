using System.Collections.Generic;
using Dicer.Models;

namespace Dicer.Formatter;

public interface IResponseFormatter
{
	string Format(NodeResponse response, params string[] args);

	string Format(IEnumerable<NodeResponse> responses, params string[] args);
}