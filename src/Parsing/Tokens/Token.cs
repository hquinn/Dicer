namespace Dicer.Parser.Tokens;

public abstract record Token(string Value);

public record NumberToken(string Value) : Token(Value)
{
	private double? _constant;
	public double Constant => _constant ??= double.Parse(Value);
}
public record AddToken() : Token("+");
public record SubtractToken() : Token("-");
public record MultiplyToken() : Token("*");
public record DivideToken() : Token("/");
public record DiceToken() : Token("D");
public record KeepToken() : Token("K");
public record OpenToken() : Token("(");
public record CloseToken() : Token(")");