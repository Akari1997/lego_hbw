namespace Devart.Common;

public class Token
{
	public readonly TokenType Type;

	public readonly object Value;

	public readonly int Id;

	public readonly int StartPosition;

	public readonly int EndPosition;

	public readonly int LineBegin;

	public readonly int LineNumber;

	public static Token Begin = new Token(TokenType.Begin, "", 0, 0, 0, 0, 0);

	public static Token Empty = new Token(TokenType.End, null, 0, 0, 0, 0, 0);

	public int LinePosition => StartPosition - LineBegin;

	public virtual int EndLineBegin => LineBegin;

	public virtual int EndLineNumber => LineNumber;

	public virtual int EndLinePosition => EndPosition - LineBegin;

	public Token(TokenType type, object value, int id, int startPosition, int endPosition, int lineBegin, int lineNumber)
	{
		Type = type;
		Value = value;
		Id = id;
		StartPosition = startPosition;
		EndPosition = endPosition;
		LineBegin = lineBegin;
		LineNumber = lineNumber;
	}

	public override string ToString()
	{
		if (Value == null)
		{
			return string.Empty;
		}
		return Value.ToString();
	}
}
