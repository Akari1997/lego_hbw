using System;
using System.Globalization;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal sealed class OracleSelectStatement : SelectStatement
{
	private new bool m_a = true;

	private new static string[] m_b;

	public OracleSelectStatement()
		: base("\"", "\"")
	{
	}

	internal OracleSelectStatement(bool A_0)
		: base("\"", "\"")
	{
		this.m_a = A_0;
	}

	private new bool a(string A_0)
	{
		string text = A_0.ToUpper();
		for (int num = 0; num < OracleSelectStatement.m_b.Length; num++)
		{
			if (OracleSelectStatement.m_b[num] == text)
			{
				return true;
			}
		}
		return false;
	}

	public static bool TryParse(string text, ParserBehavior behavior, out OracleSelectStatement statement)
	{
		return a(text, behavior, A_2: true, A_3: false, out statement);
	}

	private new static bool a(string A_0, ParserBehavior A_1, bool A_2, bool A_3, out OracleSelectStatement A_4)
	{
		A_4 = new OracleSelectStatement(A_2);
		((SelectStatement)A_4).a = A_0;
		A_4.r = A_3;
		return A_4.a(A_1);
	}

	public static OracleSelectStatement Parse(string text, ParserBehavior behavior)
	{
		return a(text, behavior, A_2: true);
	}

	internal new static OracleSelectStatement a(string A_0, ParserBehavior A_1, bool A_2)
	{
		return a(A_0, A_1, A_2, A_3: true);
	}

	internal new static OracleSelectStatement a(string A_0, ParserBehavior A_1, bool A_2, bool A_3)
	{
		if (a(A_0, A_1, A_2, A_3, out var A_4))
		{
			return A_4;
		}
		throw new InvalidOperationException("Result can not be false");
	}

	protected override object b()
	{
		LexerBehavior lexerBehavior = (this.m_a ? LexerBehavior.QuotedIdent : LexerBehavior.UpperedIdent);
		Lexer lexer = new Lexer(base.a, an.al, an.am, LexerBehavior.OmitBlank | LexerBehavior.OmitComment | LexerBehavior.HandleEscaping | lexerBehavior);
		lexer.CultureInfo = CultureInfo.InvariantCulture;
		lexer.DecimalSeparator = NumberFormatInfo.InvariantInfo.NumberDecimalSeparator;
		return lexer;
	}

	protected override bool a(object A_0, out string A_1, out string A_2, out string A_3)
	{
		A_1 = string.Empty;
		Lexer lexer = (Lexer)A_0;
		Token current = lexer.Current;
		A_2 = "";
		A_3 = current.ToString();
		current = lexer.GetNextToken();
		bool flag = false;
		while (true)
		{
			if (current.Id == 1006)
			{
				current = lexer.GetNextToken();
				if (current.Type == TokenType.Identifier || (current.Type == TokenType.Keyword && !a(current.ToString())))
				{
					if (A_2 == "")
					{
						A_2 = A_3;
						A_3 = current.ToString();
					}
					else
					{
						A_3 = A_3 + "." + current.ToString();
					}
					current = lexer.GetNextToken();
					continue;
				}
				return b("SelectStatement_FromParseIdentifierError");
			}
			if (flag || !(current.Value is char) || (char)current.Value != '@')
			{
				break;
			}
			flag = true;
			current = lexer.GetNextToken();
			if (current.Type == TokenType.Identifier || (current.Type == TokenType.Keyword && !a(current.ToString())))
			{
				A_3 = "\"" + A_3 + "\"@\"" + current.ToString();
				current = lexer.GetNextToken();
				while (current.Id == 1006)
				{
					current = lexer.GetNextToken();
					A_3 = A_3 + "." + current.ToString();
					current = lexer.GetNextToken();
				}
				A_3 += "\"";
				continue;
			}
			return b("SelectStatement_FromParseIdentifierError");
		}
		return true;
	}

	protected override bool a(object A_0)
	{
		return true;
	}

	protected override bool a(object A_0, bool A_1)
	{
		Lexer lexer = (Lexer)A_0;
		Token current = lexer.Current;
		if (current.Id == (A_1 ? 2002 : 2022))
		{
			bool flag = true;
			byte b2 = 0;
			string text = string.Empty;
			current = lexer.GetNextToken();
			while (flag)
			{
				if (current.Type == TokenType.End)
				{
					return true;
				}
				switch (current.Type)
				{
				case TokenType.Keyword:
					if (b2 == 0)
					{
						switch (current.Id)
						{
						case 2022:
						case 3010:
							return true;
						case 2003:
						case 2005:
							text = "by";
							break;
						}
					}
					break;
				case TokenType.Symbol:
					if (current.Id == 1009)
					{
						b2++;
					}
					else if (current.Id == 1008 && --b2 < 0)
					{
						return b("SelectStatement_FromParseIdentifierError");
					}
					break;
				}
				if (text.Length != 0 && lexer.PeekNextToken().Value is string && string.Compare(text, (string)lexer.PeekNextToken().Value, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
				current = lexer.GetNextToken();
			}
		}
		return true;
	}

	protected override bool b(int A_0)
	{
		return A_0 != 3010;
	}

	private new void d()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("select ");
		foreach (SelectColumn column in base.Columns)
		{
			stringBuilder.Append(column.a(AsKeyword) + ",");
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		stringBuilder.Append(" from ");
		foreach (SelectTable table in base.Tables)
		{
			stringBuilder.Append(table.ToString() + ",");
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		if (Where != null && Where.Length != 0)
		{
			stringBuilder.Append(" where " + Where);
		}
		if (base.GroupBy.Count > 0)
		{
			stringBuilder.Append(" group by ");
			foreach (SelectColumn item in base.GroupBy)
			{
				stringBuilder.Append(item.a(null) + ",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
		}
		if (base.OrderBy.Count > 0)
		{
			stringBuilder.Append(" order by ");
			foreach (SelectColumn item2 in base.OrderBy)
			{
				stringBuilder.Append(item2.a(null) + ",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
		}
		base.a = stringBuilder.ToString();
	}

	private new void c()
	{
		StringBuilder stringBuilder = new StringBuilder(base.a);
		int A_ = 0;
		int A_2 = 0;
		b(base.Columns.CollectionNode, stringBuilder, ref A_, ref A_2, AsKeyword);
		b(base.Tables.CollectionNode, stringBuilder, ref A_, ref A_2, " ");
		SelectStatement.a(base.d, base.e, base.b, stringBuilder, ref A_, ref A_2, " where ");
		b(base.GroupBy.CollectionNode, stringBuilder, ref A_, ref A_2, null);
		SelectStatement.a(base.f, base.g, base.c, stringBuilder, ref A_, ref A_2, " having ");
		b(base.OrderBy.CollectionNode, stringBuilder, ref A_, ref A_2, null);
		base.a = stringBuilder.ToString();
	}

	protected override bool a()
	{
		int num = -1;
		foreach (SelectColumn column in base.Columns)
		{
			if (((SelectStatementNode)column).b > num)
			{
				num = ((SelectStatementNode)column).b;
			}
		}
		if (base.Columns.CollectionNode.Node.Binded)
		{
			c();
		}
		else
		{
			d();
		}
		return true;
	}

	protected override bool a(int A_0)
	{
		return base.a(A_0);
	}

	public override void Clear()
	{
		base.Clear();
	}

	internal override bool a(SelectTable A_0, Token A_1)
	{
		if (A_1.Type == TokenType.Keyword)
		{
			if (A_1.Id != 3036)
			{
				return A_1.Id != 3037;
			}
			return false;
		}
		return true;
	}

	static OracleSelectStatement()
	{
		OracleSelectStatement.m_b = new string[13]
		{
			"SYSDATE", "USER", "UID", "BEGIN", "END", "DECLARE", "TRIGGER", "COLUMN", "CONNECT", "DESC",
			"SET", "START", "WHENEVER"
		};
	}
}
