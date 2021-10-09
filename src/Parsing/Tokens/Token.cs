namespace Dicer.Parser.Tokens
{
	public abstract record Token(string Value, int Precedence);

	public record NumberToken(string Value) : Token(Value, 1)
	{
		private double? _constant;
		public double Constant => _constant ??= double.Parse(Value);
	}
	public record AddToken() : Token("+", 2);
	public record SubtractToken() : Token("-", 2);
	public record MultiplyToken() : Token("*", 3);
	public record DivideToken() : Token("/", 3);
	public record DiceToken() : Token("D", 4);
	public record KeepToken() : Token("K", 4);
	public record OpenToken() : Token("(", 6);
	public record CloseToken() : Token(")", 6);
}