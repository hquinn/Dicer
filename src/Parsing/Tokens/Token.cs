namespace Dicer;

internal abstract record Token(string Value);

internal record NumberToken(string Value) : Token(Value)
{
	private double? _constant;
	public double Constant => _constant ??= double.Parse(Value);
}

internal record AddToken() : Token("+");
internal record SubtractToken() : Token("-");
internal record MultiplyToken() : Token("*");
internal record DivideToken() : Token("/");
internal record DiceToken() : Token("D");
internal record KeepToken() : Token("K");
internal record MinimumToken() : Token("M");
internal record OpenToken() : Token("(");
internal record CloseToken() : Token(")");