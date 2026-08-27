using System.Collections;

namespace Devart.Common;

public class CommonLexem
{
	public const int StringQuote = 10001;

	public const int IdentQuote = 10002;

	public const int IdentQuoteBegin = 10003;

	public const int IdentQuoteEnd = 10004;

	public const int InlineComment = 10005;

	public const int CommentBegin = 10006;

	public const int CommentEnd = 10007;

	public const int IdentPrefix = 10008;

	public const int CommentExtBegin = 10009;

	public static Hashtable symbols = new Hashtable();

	public static Hashtable keywords = new Hashtable();
}
