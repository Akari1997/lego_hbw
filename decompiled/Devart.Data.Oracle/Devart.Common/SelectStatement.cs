using System;
using System.Collections;
using System.Text;

namespace Devart.Common;

internal class SelectStatement
{
	protected string a;

	protected string b = string.Empty;

	protected string c = string.Empty;

	protected SelectStatementNode d = new SelectStatementNode();

	protected SelectStatementNode e = new SelectStatementNode();

	protected SelectStatementNode f = new SelectStatementNode();

	protected SelectStatementNode g = new SelectStatementNode();

	protected SelectColumnCollection h;

	protected SelectColumnCollection i;

	protected SelectColumnCollection j;

	protected SelectTableCollection k;

	private bool l;

	private bool m;

	internal int n;

	internal Token o;

	private readonly string p;

	private readonly string q;

	protected bool r = true;

	private ArrayList s = new ArrayList(new int[6] { 2028, 2001, 2002, 2003, 2005, 2021 });

	public SelectColumnCollection Columns => h;

	public SelectTableCollection Tables => k;

	public virtual string Where
	{
		get
		{
			if (this.d.Current)
			{
				return this.a.Substring(this.d.a, this.d.b - this.d.a + 1);
			}
			return this.b;
		}
		set
		{
			if (Where != value)
			{
				this.b = ((value == null) ? string.Empty : value);
				this.d.d();
			}
		}
	}

	public SelectColumnCollection OrderBy => j;

	public SelectColumnCollection GroupBy => i;

	public virtual string Having
	{
		get
		{
			if (this.f.Current)
			{
				return this.a.Substring(this.f.a, this.f.b - this.f.a + 1);
			}
			return this.c;
		}
		set
		{
			if (Having != value)
			{
				this.c = ((value == null) ? string.Empty : value);
				this.f.d();
			}
		}
	}

	public bool Distinct
	{
		get
		{
			return l;
		}
		set
		{
			l = value;
		}
	}

	public bool All
	{
		get
		{
			return m;
		}
		set
		{
			m = value;
		}
	}

	internal Token SelectToken => o;

	protected virtual string AsKeyword => " ";

	internal string QuoteEnd => q;

	internal string QuoteStart => p;

	protected SelectStatement(string A_0, string A_1)
	{
		p = A_0;
		q = A_1;
		h = new SelectColumnCollection(this, "select ", A_2: false);
		i = new SelectColumnCollection(this, "group by ", A_2: true);
		j = new SelectColumnCollection(this, "order by ", A_2: true);
		k = new SelectTableCollection(this, "from ");
	}

	protected virtual object b()
	{
		return new Lexer(this.a, ag.@as, ag.at, LexerBehavior.OmitBlank | LexerBehavior.OmitComment | LexerBehavior.UpperedIdent);
	}

	protected virtual bool d(object A_0)
	{
		return false;
	}

	protected virtual bool e(object A_0)
	{
		switch (((Token)A_0).Id)
		{
		case 2012:
		case 2013:
		case 2014:
		case 2015:
		case 2016:
		case 2017:
		case 2023:
			return true;
		default:
			return false;
		}
	}

	protected bool b(string A_0)
	{
		if (r)
		{
			throw new InvalidOperationException(al.a(A_0));
		}
		return false;
	}

	protected bool c(string A_0)
	{
		if (r)
		{
			throw new ArgumentNullException(al.a(A_0));
		}
		return false;
	}

	protected virtual bool f()
	{
		Lexer lexer = (Lexer)b();
		while (lexer.GetNextToken() != Token.Empty)
		{
			Token current = lexer.Current;
			if (current.Type == TokenType.End)
			{
				break;
			}
			Token token = lexer.PeekNextToken();
			if (current.Type == TokenType.Symbol && (string)current.Value == ";" && token.Type != TokenType.End)
			{
				return b("SelectStatement_EndExpected");
			}
		}
		return true;
	}

	protected bool a(ParserBehavior A_0)
	{
		if (this.a == null)
		{
			c("text");
		}
		if (!f())
		{
			return false;
		}
		Clear();
		Lexer lexer = (Lexer)b();
		if (lexer.GetNextToken().Id != 2028)
		{
			return b("SelectStatement_SelectNotFoundError");
		}
		o = lexer.Current;
		if ((A_0 & ParserBehavior.Columns) != ParserBehavior.None)
		{
			h.CollectionNode.Node.a = o.StartPosition;
		}
		Token nextToken = lexer.GetNextToken();
		while (c(nextToken))
		{
			nextToken = lexer.GetNextToken();
		}
		if (nextToken.Type == TokenType.End)
		{
			return b("SelectStatement_EndOfStatement");
		}
		return a(lexer, A_0);
	}

	protected virtual bool a(object A_0, ParserBehavior A_1)
	{
		if (!b(A_0, (A_1 & ParserBehavior.Columns) != 0))
		{
			return false;
		}
		if (!e(A_0, (A_1 & ParserBehavior.Tables) != 0))
		{
			return false;
		}
		if (!c(A_0, (A_1 & ParserBehavior.Where) != 0))
		{
			return false;
		}
		g(A_0, (A_1 & ParserBehavior.GroupBy) != 0);
		d(A_0, (A_1 & ParserBehavior.Having) != 0);
		f(A_0, (A_1 & ParserBehavior.OrderBy) != 0);
		return a(A_0);
	}

	protected virtual bool b(object A_0, bool A_1)
	{
		Lexer lexer = (Lexer)A_0;
		Token token = lexer.Current;
		int startPosition = token.StartPosition;
		do
		{
			int startPosition2 = token.StartPosition;
			int num = 0;
			int num2 = 0;
			string a_ = string.Empty;
			string text = string.Empty;
			string a_2 = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			bool flag = false;
			if (token.Id == 2001)
			{
				if (A_1)
				{
					return b("Unexpected FROM found.");
				}
				return true;
			}
			int num3 = -1;
			do
			{
				if (token.Type == TokenType.Identifier || c((int)token.Type))
				{
					Token token2 = lexer.PeekPreviousToken();
					if ((token2.Type == TokenType.Identifier || token2.Type == TokenType.Number || token2.Type == TokenType.String || token2.Id == 1008) && num == 0)
					{
						token = lexer.GetNextToken();
						if (token.Id == 1004 || token.Id == 2001)
						{
							a_ = lexer.PeekPreviousToken().ToString();
							break;
						}
						flag = true;
						if (token.Id == 2007)
						{
							break;
						}
					}
					else
					{
						text = token.ToString();
					}
				}
				else if (token.Id == 1006)
				{
					a_2 = text2;
					text2 = text3;
					text3 = text;
					num2++;
					flag = flag || num2 > 3 || lexer.PeekPreviousToken().Type != TokenType.Identifier;
				}
				else if (token.Id == 1001)
				{
					bool flag2 = lexer.PeekPreviousToken().Id == 1004;
					if ((!flag && ((A_1 && h.Count == 0) || (num2 > 0 && lexer.PeekPreviousToken().Id == 1006))) || flag2)
					{
						text = token.ToString();
						token = lexer.GetNextToken();
						if ((!flag2 && token.Id == 1004 && num2 != 0) || token.Id == 2001)
						{
							n++;
							break;
						}
						if (n > 0)
						{
							flag = true;
						}
						if (token.Id == 1004 || token.Id == 2007)
						{
							break;
						}
					}
				}
				if (token.Id == 1009)
				{
					flag = true;
					num++;
				}
				else if (token.Id == 1008 && --num < 0)
				{
					return b("SelectStatement_TooMuchParenteses");
				}
				flag = flag || (token.Id != 2001 && token.Type != TokenType.Identifier && token.Id != 1006 && token.Id != 1009 && token.Id != 1008);
				if (flag)
				{
					num3 = token.EndPosition;
				}
				token = lexer.GetNextToken();
			}
			while (token.Type != TokenType.End && ((token.Id != 2007 && token.Id != 2001 && token.Id != 1004) || num != 0));
			if (num != 0 && token.Type == TokenType.End)
			{
				return b("SelectStatement_EndOfStatement");
			}
			flag = flag || lexer.PeekPreviousToken().Id == 1006;
			if (token.Id == 2007)
			{
				token = lexer.GetNextToken();
				if (c((int)token.Type))
				{
					a_ = token.ToString();
					token = lexer.GetNextToken();
				}
			}
			int a_3 = lexer.PeekPreviousToken().EndPosition - 1;
			if (token.Id == 1004)
			{
				token = lexer.GetNextToken();
				if (token.Type == TokenType.Keyword && a(token.Id))
				{
					return b("SelectStatement_KeywordError");
				}
				if (token.Type == TokenType.End)
				{
					return b("SelectStatement_EndOfStatement");
				}
			}
			if (A_1)
			{
				if (flag && num3 != -1)
				{
					h.Add(new SelectColumn("", "", "", "", a_, this.a.Substring(startPosition2, num3 - startPosition2), startPosition2, a_3));
				}
				else
				{
					h.Add(new SelectColumn(a_2, text2, text3, text, a_, null, startPosition2, a_3));
				}
			}
		}
		while (lexer.Current.Type != TokenType.End && lexer.Current.Id != 2001);
		if (A_1)
		{
			h.CollectionNode.Node.b = lexer.PeekPreviousToken().EndPosition - 1;
			h.CollectionNode.Node.a = startPosition;
		}
		return true;
	}

	protected virtual bool c(int A_0)
	{
		return A_0 == 3;
	}

	protected virtual bool b(char A_0)
	{
		if (!char.IsLetterOrDigit(A_0) && A_0 != '$' && A_0 != '`' && A_0 != '"' && A_0 != '*')
		{
			return A_0 == ')';
		}
		return true;
	}

	protected virtual bool e(object A_0, bool A_1)
	{
		Lexer lexer = (Lexer)A_0;
		Token current = lexer.Current;
		int startPosition = current.StartPosition;
		if (A_1)
		{
			k.CollectionNode.Node.a = current.StartPosition;
			k.CollectionNode.Node.b();
		}
		current = lexer.GetNextToken();
		if (current.Type == TokenType.End)
		{
			return true;
		}
		string text = null;
		string A_2 = null;
		int num = int.MinValue;
		int num2 = int.MinValue;
		int num3 = 0;
		do
		{
			startPosition = current.StartPosition;
			string A_3 = "";
			string A_4 = "";
			string A_5 = "";
			bool flag = false;
			if (current.Type == TokenType.Identifier)
			{
				a(lexer, out A_5, out A_3, out A_4);
				current = lexer.Current;
			}
			else
			{
				if (current.Id != 1009)
				{
					return b("SelectStatement_FromParseIdentifierExprError");
				}
				int num4 = 1;
				flag = true;
				do
				{
					current = lexer.GetNextToken();
					if (current.Id == 1009)
					{
						num4++;
					}
					else if (current.Id == 1008)
					{
						num4--;
					}
					if (current.Type == TokenType.End)
					{
						return b("SelectStatement_EndOfStatement");
					}
				}
				while (num4 > 0);
				num3 = current.EndPosition;
				current = lexer.GetNextToken();
			}
			string a_ = "";
			if (current.Id == 2007)
			{
				current = lexer.GetNextToken();
				if (current.Type != TokenType.Identifier)
				{
					return b("SelectStatement_FromParseAliasError");
				}
				a_ = current.ToString();
				current = lexer.GetNextToken();
			}
			else if (current.Type == TokenType.Identifier)
			{
				a_ = current.ToString();
				current = lexer.GetNextToken();
			}
			SelectTable selectTable = null;
			if (current.Id != 1009)
			{
				if (A_1)
				{
					selectTable = new SelectTable(A_5, A_3, A_4, a_, flag ? this.a.Substring(startPosition, num3 - startPosition) : null, startPosition, lexer.PeekPreviousToken().EndPosition - 1);
					selectTable.f = text;
					if (!a(selectTable, current))
					{
						return false;
					}
					k.Add(selectTable);
				}
			}
			else
			{
				a(lexer, A_1: false, out var _);
				current = lexer.Current;
			}
			Tables.CollectionNode.Node.b = lexer.PeekPreviousToken().EndPosition - 1;
			if (current.Id == 2011 || current.Id == 2024)
			{
				a(lexer, A_1: true, out A_2);
				num2 = lexer.PeekPreviousToken().EndPosition;
				if (selectTable != null && !Utils.IsEmpty(text) && !Utils.IsEmpty(A_2) && num != int.MinValue && num2 != int.MinValue)
				{
					selectTable.g = A_2;
					text = null;
					A_2 = null;
					((SelectStatementNode)selectTable).a = num;
					((SelectStatementNode)selectTable).b = num2;
					num = int.MinValue;
					num2 = int.MinValue;
				}
				current = lexer.Current;
			}
			if (current.Id == 1004)
			{
				current = lexer.GetNextToken();
			}
			else if (e(current))
			{
				num = current.StartPosition;
				do
				{
					text += (Utils.IsEmpty(text) ? current.ToString() : (" " + current.ToString()));
					current = lexer.GetNextToken();
				}
				while (e(current));
			}
		}
		while (current.Type != TokenType.Keyword && current.Type != TokenType.End && (current.Type != TokenType.Symbol || !(current.ToString() == ";")));
		return true;
	}

	internal virtual bool a(SelectTable A_0, Token A_1)
	{
		return true;
	}

	protected virtual bool c(object A_0, bool A_1)
	{
		Lexer lexer = (Lexer)A_0;
		Token current = lexer.Current;
		if (current.Type == TokenType.Keyword && current.Id == 2002)
		{
			if (A_1)
			{
				this.e.a = current.StartPosition;
				this.d.a = lexer.PeekNextToken().StartPosition;
			}
			if (!a(lexer, A_1: true))
			{
				return false;
			}
			if (A_1)
			{
				this.e.b = (this.d.b = lexer.PeekPreviousToken().EndPosition - 1);
				this.d.b();
			}
		}
		return true;
	}

	protected virtual void g(object A_0, bool A_1)
	{
		Lexer lexer = (Lexer)A_0;
		Token current = lexer.Current;
		if (current.Type == TokenType.Keyword && current.Id == 2005)
		{
			int startPosition = current.StartPosition;
			current = lexer.GetNextToken();
			if (current.Type == TokenType.Keyword && current.Id == 2004)
			{
				a(lexer, A_1: true, startPosition, A_1);
			}
		}
	}

	protected virtual void d(object A_0, bool A_1)
	{
		Lexer lexer = (Lexer)A_0;
		Token current = lexer.Current;
		if (current.Type == TokenType.Keyword && current.Id == 2022)
		{
			if (A_1)
			{
				this.g.a = current.StartPosition;
				this.f.a = lexer.PeekNextToken().StartPosition;
			}
			a(lexer, A_1: false);
			if (A_1)
			{
				this.g.b = (this.f.b = lexer.PeekPreviousToken().EndPosition - 1);
				this.f.b();
			}
		}
	}

	protected virtual void f(object A_0, bool A_1)
	{
		Lexer lexer = (Lexer)A_0;
		Token current = lexer.Current;
		if (current.Type == TokenType.Keyword && current.Id == 2003)
		{
			int startPosition = current.StartPosition;
			current = lexer.GetNextToken();
			if (current.Type == TokenType.Keyword && current.Id == 2004)
			{
				a(lexer, A_1: false, startPosition, A_1);
			}
		}
	}

	protected virtual bool a(object A_0, out string A_1, out string A_2, out string A_3)
	{
		Lexer lexer = (Lexer)A_0;
		Token current = lexer.Current;
		A_1 = "";
		A_2 = "";
		A_3 = current.ToString();
		current = lexer.GetNextToken();
		if (current.Id == 1006)
		{
			current = lexer.GetNextToken();
			bool flag = true;
			if (current.Type != TokenType.Identifier)
			{
				return b("SelectStatement_FromParseIdentifierError");
			}
			A_2 = A_3;
			A_3 = current.ToString();
			current = lexer.GetNextToken();
			if (current.Id == 1006)
			{
				current = lexer.GetNextToken();
				flag = true;
				if (current.Type != TokenType.Identifier)
				{
					return b("SelectStatement_FromParseIdentifierError");
				}
				A_1 = A_2;
				A_2 = A_3;
				A_3 = current.ToString();
				current = lexer.GetNextToken();
				if (current.Id == 1006)
				{
					A_1 = A_2;
					A_2 = A_3;
					A_3 = current.ToString();
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				current = lexer.GetNextToken();
			}
		}
		return true;
	}

	protected virtual bool a(object A_0)
	{
		return true;
	}

	protected virtual bool b(int A_0)
	{
		throw new InvalidOperationException("The method CheckId(int id) is not implemented.");
	}

	protected virtual bool a(object A_0, bool A_1, int A_2, bool A_3)
	{
		IList list = null;
		SelectStatementNode selectStatementNode = null;
		if (A_3)
		{
			list = (A_1 ? GroupBy : OrderBy);
			selectStatementNode = (A_1 ? GroupBy.CollectionNode.Node : OrderBy.CollectionNode.Node);
			list.Clear();
			selectStatementNode.a = A_2;
		}
		ArrayList arrayList = new ArrayList();
		Lexer lexer = (Lexer)A_0;
		Token nextToken = lexer.GetNextToken();
		if (a(nextToken.Id))
		{
			return b($"Unexpected keyword found: {nextToken}.");
		}
		int startPosition = nextToken.StartPosition;
		bool flag = false;
		do
		{
			int num = 0;
			int num2 = 0;
			string text = "";
			string text2 = "";
			string text3 = "";
			string a_ = "";
			bool flag2 = false;
			do
			{
				if (nextToken.Type == TokenType.Identifier)
				{
					Token token = lexer.PeekPreviousToken();
					if ((token.Type == TokenType.Identifier || token.Type == TokenType.Number || token.Type == TokenType.String || token.Id == 1008) && num == 0)
					{
						nextToken = lexer.GetNextToken();
						if (nextToken.Id == 1004 || nextToken.Id == 2001)
						{
							break;
						}
						flag2 = true;
						if (nextToken.Id == 2007)
						{
							break;
						}
					}
					else
					{
						text = nextToken.ToString();
					}
				}
				else if (nextToken.Id == 1006)
				{
					a_ = text2;
					text2 = text3;
					text3 = text;
					num2++;
					flag2 = flag2 || num2 > 3 || lexer.PeekPreviousToken().Type != TokenType.Identifier;
				}
				else if (nextToken.Id == 1001)
				{
					bool flag3 = lexer.PeekPreviousToken().Id == 1004;
					if ((!flag2 && (arrayList.Count == 0 || (num2 > 0 && lexer.PeekPreviousToken().Id == 1006))) || flag3)
					{
						text = nextToken.ToString();
						nextToken = lexer.GetNextToken();
						if ((!flag3 && nextToken.Id == 1004 && num2 != 0) || nextToken.Id == 2001)
						{
							n++;
							break;
						}
						if (n > 0)
						{
							flag2 = true;
						}
						if (nextToken.Id == 1004 || nextToken.Id == 2007)
						{
							break;
						}
					}
				}
				if (nextToken.Id == 1009)
				{
					flag2 = true;
					num++;
				}
				else if (nextToken.Id == 1008)
				{
					num--;
				}
				flag2 = flag2 || (nextToken.Type != TokenType.Identifier && nextToken.Id != 1006 && nextToken.Id != 1009 && nextToken.Id != 1008);
				nextToken = lexer.GetNextToken();
				flag = !A_1 || lexer.Current.Id != 2003 || lexer.PeekNextToken().Id != 2004;
				flag = flag && b(lexer.Current.Id) && lexer.Current.Type != TokenType.End;
			}
			while ((nextToken.Id != 2007 && lexer.Current.Id != 2022 && flag && nextToken.Id != 1004 && nextToken.Id != 1010) || num != 0);
			flag2 = flag2 || lexer.PeekPreviousToken().Id == 1006;
			int num3 = lexer.PeekPreviousToken().EndPosition - 1;
			if (nextToken.Id == 1004)
			{
				nextToken = lexer.GetNextToken();
				if (nextToken.Type == TokenType.Keyword)
				{
					return b("Error occurred while parsing sort clause.");
				}
				if (nextToken.Type == TokenType.End)
				{
					return b("Error occurred while parsing sort clause.");
				}
			}
			if (flag2)
			{
				arrayList.Add(new SelectColumn(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, this.a.Substring(startPosition, num3 - startPosition + 1), startPosition, num3));
			}
			else
			{
				arrayList.Add(new SelectColumn(a_, text2, text3, text, string.Empty, null, startPosition, num3));
			}
			startPosition = nextToken.StartPosition;
			flag = !A_1 || lexer.Current.Id != 2003 || lexer.PeekNextToken().Id != 2004;
		}
		while (flag && b(lexer.Current.Id) && lexer.Current.Type != TokenType.End && lexer.Current.Id != 2022 && lexer.Current.Type != TokenType.End && lexer.Current.Id != 1010);
		if (A_3)
		{
			foreach (object item in arrayList)
			{
				list.Add(item);
			}
			selectStatementNode.b = lexer.PeekPreviousToken().EndPosition - 1;
			selectStatementNode.b();
		}
		return true;
	}

	protected virtual bool a(object A_0, bool A_1)
	{
		throw new InvalidOperationException("The method ParseCondition() is not implemented.");
	}

	protected virtual bool b(object A_0)
	{
		return false;
	}

	private void a(Lexer A_0, bool A_1, out string A_2)
	{
		Token token = A_0.Current;
		int num = 0;
		int startPosition = A_0.Current.StartPosition;
		do
		{
			if (token.Id == 1009)
			{
				num++;
			}
			else if (token.Id == 1008)
			{
				num--;
			}
			token = A_0.GetNextToken();
		}
		while (num > 0 || (A_1 && token.Id != 1004 && token.Type != TokenType.End && token.Id != 2002 && token.Id != 2005 && token.Id != 2006 && token.Id != 2003 && !d(token) && !e(token) && !b(token)));
		A_2 = A_0.Text.Substring(startPosition, A_0.PeekPreviousToken().EndPosition - startPosition);
	}

	public virtual void Clear()
	{
		Columns.Clear();
		Tables.Clear();
		GroupBy.Clear();
		OrderBy.Clear();
		Where = string.Empty;
	}

	protected virtual bool a()
	{
		throw new InvalidOperationException("The method Apply() is not implemented.");
	}

	public override string ToString()
	{
		a();
		return this.a.Trim();
	}

	private static bool a(char A_0)
	{
		if (!char.IsControl(A_0) && !char.IsPunctuation(A_0) && !char.IsSeparator(A_0))
		{
			return char.IsWhiteSpace(A_0);
		}
		return true;
	}

	private static void a(IList A_0, int A_1, int A_2, int A_3)
	{
		if (A_3 > -1)
		{
			foreach (SelectStatementNode item in A_0)
			{
				if (item.a >= A_3)
				{
					item.a(A_1);
				}
			}
			return;
		}
		int count = A_0.Count;
		for (int num = A_2; num < count; num++)
		{
			SelectStatementNode selectStatementNode2 = (SelectStatementNode)A_0[num];
			if (selectStatementNode2.Binded)
			{
				selectStatementNode2.a(A_1);
			}
		}
	}

	private static void a(SelectStatementCollection A_0, StringBuilder A_1, ref int A_2, ref int A_3, string A_4)
	{
		IList list = A_0.List;
		SelectStatementNode node = A_0.Node;
		string sqlPrefix = A_0.SqlPrefix;
		if (A_3 > 1 && A_1.Length > A_3 - 1 && !a(A_1[A_3 - 1]) && A_1.Length > A_3 && !a(A_1[A_3]))
		{
			A_1.Insert(A_3, " ");
			A_3++;
		}
		node.a = A_3;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(sqlPrefix);
		foreach (SelectStatementNode item in list)
		{
			item.a = stringBuilder.Length;
			item.b = stringBuilder.Length + item.ToString().Length - 1;
			stringBuilder.Append(item.ToString() + ",");
			item.b();
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		if (node.a + 1 < A_1.Length && !a(A_1[node.a + 1]))
		{
			stringBuilder.Append(' ');
		}
		if (node.a > 0 && node.a - 1 < A_1.Length && !a(A_1[node.a - 1]))
		{
			stringBuilder.Insert(0, " ");
		}
		A_1.Insert(node.a, stringBuilder);
		a(list, node.a, 0, -1);
		node.b = node.a + stringBuilder.Length - 1;
		node.b();
		A_2 += stringBuilder.Length;
		A_3 += stringBuilder.Length;
	}

	internal int b(SelectStatementCollection A_0, StringBuilder A_1, ref int A_2, ref int A_3, string A_4)
	{
		IList list = A_0.List;
		SelectStatementNode node = A_0.Node;
		IList removed = A_0.Removed;
		_ = A_0.SqlPrefix;
		node.a(A_2);
		if (list.Count == 0)
		{
			if (node.Binded && !node.Current && !node.Marker)
			{
				A_1.Remove(node.a, node.b - node.a + 1);
				A_2 -= node.b - node.a + 1;
				node.d();
				node.b = -1;
				removed.Clear();
				A_3 = node.a;
			}
		}
		else if (node.Binded)
		{
			if (node.Current)
			{
				A_3 = a(A_0, A_1, ref A_2, A_4);
			}
			else if (node.Marker)
			{
				a(A_0, A_1, ref A_2, ref node.a, A_4);
				A_3 = node.a;
			}
			else
			{
				A_3 = a(A_0, A_1, ref A_2, A_4);
			}
		}
		else
		{
			a(A_0, A_1, ref A_2, ref A_3, A_4);
		}
		return A_3;
	}

	private int a(SelectStatementCollection A_0, StringBuilder A_1, ref int A_2, string A_3)
	{
		IList list = A_0.List;
		IList removed = A_0.Removed;
		SelectStatementNode selectStatementNode = null;
		int num = A_2;
		int num2 = 0;
		a(list, A_2, 0, -1);
		foreach (SelectStatementNode item in list)
		{
			if (item.Binded && (item.a < num2 || num2 == 0))
			{
				num2 = item.a - 1;
			}
		}
		num2 = ((num2 != 0) ? (num2 + 1) : (A_0.Node.a + A_0.SqlPrefix.Length));
		if (removed.Count > 0)
		{
			a(removed, A_2, 0, -1);
			for (int num3 = 0; num3 < removed.Count; num3++)
			{
				SelectStatementNode selectStatementNode3 = (SelectStatementNode)removed[num3];
				for (int num4 = 0; num4 < removed.Count; num4++)
				{
					SelectStatementNode selectStatementNode4 = (SelectStatementNode)removed[num4];
					if (num3 != num4 && selectStatementNode4.Binded)
					{
						if (selectStatementNode3.a >= selectStatementNode4.a && selectStatementNode3.b <= selectStatementNode4.b)
						{
							selectStatementNode3.a = -1;
							break;
						}
						if (selectStatementNode3.a <= selectStatementNode4.a && selectStatementNode3.b >= selectStatementNode4.a && selectStatementNode3.b <= selectStatementNode4.b)
						{
							selectStatementNode4.a = selectStatementNode3.a;
							selectStatementNode3.a = -1;
							break;
						}
						if (selectStatementNode3.a >= selectStatementNode4.a && selectStatementNode3.a <= selectStatementNode4.b && selectStatementNode3.b >= selectStatementNode4.b)
						{
							selectStatementNode4.b = selectStatementNode3.b;
							selectStatementNode3.a = -1;
							break;
						}
					}
				}
			}
			for (int num5 = 0; num5 < removed.Count; num5++)
			{
				selectStatementNode = (SelectStatementNode)removed[num5];
				if (selectStatementNode.Binded)
				{
					A_1.Remove(selectStatementNode.a, selectStatementNode.b - selectStatementNode.a);
					A_2 -= selectStatementNode.b - selectStatementNode.a;
					if (num2 > selectStatementNode.a)
					{
						num2 -= selectStatementNode.b - selectStatementNode.a;
					}
					a(list, selectStatementNode.a - selectStatementNode.b, -1, selectStatementNode.a);
					a(removed, selectStatementNode.a - selectStatementNode.b, -1, selectStatementNode.a);
				}
			}
			removed.Clear();
		}
		bool flag = false;
		for (int num6 = 0; num6 < list.Count; num6++)
		{
			selectStatementNode = (SelectStatementNode)list[num6];
			if (!selectStatementNode.Binded)
			{
				if (num6 > 0)
				{
					A_1.Insert(num2++, ",");
					A_2++;
					a(list, 2, num6, -1);
				}
				flag = true;
				A_1.Insert(num2, selectStatementNode.a(A_3));
				selectStatementNode.b = num2 + selectStatementNode.a(A_3).Length - 1;
				selectStatementNode.a = num2;
				A_2 += selectStatementNode.b - selectStatementNode.a + 1;
				num2 = selectStatementNode.b + 1;
				if (num6 == 0 && num6 <= list.Count - 1 && num6 + 1 < list.Count && ((SelectStatementNode)list[num6 + 1]).Binded)
				{
					A_1.Insert(num2, ",");
					a(list, 2, num6 + 1, -1);
					A_2++;
				}
				a(list, selectStatementNode.b - selectStatementNode.a, num6 + 1, -1);
			}
			else
			{
				if (!selectStatementNode.Current)
				{
					while (selectStatementNode.b < A_1.Length && !b(A_1[selectStatementNode.b]))
					{
						selectStatementNode.b--;
					}
					A_1.Remove(selectStatementNode.a, selectStatementNode.b - selectStatementNode.a + 1);
					A_1.Insert(selectStatementNode.a, selectStatementNode.a(A_3));
					A_2 += selectStatementNode.a(A_3).Length - selectStatementNode.b + selectStatementNode.a - 1;
					a(list, selectStatementNode.a(A_3).Length - selectStatementNode.b + selectStatementNode.a - 1, num6 + 1, -1);
					selectStatementNode.b = selectStatementNode.a + selectStatementNode.a(A_3).Length - 1;
				}
				num2 = selectStatementNode.b + 1;
			}
			selectStatementNode.b();
		}
		if (flag && selectStatementNode != null && num2 > 0 && num2 < A_1.Length && !a(A_1[num2 - 1]) && !a(A_1[num2]))
		{
			A_2++;
			A_1.Insert(num2, " ");
		}
		A_0.Node.b += A_2 - num;
		return num2;
	}

	protected static void a(SelectStatementNode A_0, SelectStatementNode A_1, string A_2, StringBuilder A_3, ref int A_4, ref int A_5, string A_6)
	{
		if (A_0.Binded)
		{
			A_1.a(A_4);
			if (A_0.Current)
			{
				A_0.a(A_4);
			}
			else
			{
				A_0.a(A_4);
				if (A_2.Length != 0)
				{
					A_3.Remove(A_0.a, A_0.b - A_0.a + 1);
					A_3.Insert(A_0.a, A_2);
					A_4 += A_2.Length - A_0.b + A_0.a - 1;
					A_1.b = (A_0.b = A_0.a + A_2.Length - 1);
					if (A_0.b + 1 < A_3.Length && !a(A_3[A_0.b + 1]))
					{
						A_3.Insert(A_0.b, " ");
						A_4++;
					}
				}
				else
				{
					A_0.a = (A_0.b = -1);
					A_3.Remove(A_1.a, A_1.b - A_1.a + 1);
					A_4 -= A_1.b - A_1.a + 1;
					A_1.b = -1;
				}
				A_0.b();
				A_1.b();
			}
		}
		else if (A_2 != null && A_2.Length != 0)
		{
			if (A_1.Marker)
			{
				A_5 = A_1.a;
			}
			else
			{
				A_1.a = A_5;
			}
			string text = string.Empty;
			if (A_3.Length > A_5 - 1 && !a(A_3[A_5 - 1]))
			{
				text = " ";
				A_1.a++;
			}
			string text2 = text + A_6 + " " + A_2;
			A_3.Insert(A_5, text2);
			A_4 += text2.Length;
			A_0.a = A_5 + text.Length + A_6.Length + 1;
			A_0.b = A_0.a + A_2.Length - 1;
			A_1.b = A_0.b;
			A_0.b();
		}
		if (A_0.Binded)
		{
			A_5 = A_0.b + 1;
		}
	}

	protected virtual bool c(object A_0)
	{
		if (((Token)A_0).Id == 2009)
		{
			l = true;
		}
		else if (((Token)A_0).Id == 2008)
		{
			m = true;
		}
		if (((Token)A_0).Id != 2009)
		{
			return ((Token)A_0).Id == 2008;
		}
		return true;
	}

	public void AddWhereCondition(string condition)
	{
		Where = ((Where.Length == 0) ? condition : ("(" + Where + ") AND " + condition + " "));
	}

	public void AddHavingCondition(string condition)
	{
		Having = ((Having.Length == 0) ? condition : ("(" + Having + ") AND " + condition + " "));
	}

	protected virtual bool a(int A_0)
	{
		if (s.IndexOf(A_0) != -1)
		{
			s.Remove(A_0);
			return true;
		}
		if (A_0 != 2004 && A_0 != 2006 && A_0 != 2012 && A_0 != 2013 && A_0 != 2014 && A_0 != 2015 && A_0 != 2017)
		{
			return A_0 == 2016;
		}
		return true;
	}
}
