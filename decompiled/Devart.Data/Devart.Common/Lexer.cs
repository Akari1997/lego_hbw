using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace Devart.Common;

public sealed class Lexer
{
	public const int DefaultMaxSymbolLength = 3;

	private const int m_a = 8192;

	private const char m_b = '\uffff';

	private string m_c;

	private int m_d;

	private TextReader m_e;

	private int m_f;

	private int g;

	private int h;

	private int i;

	private int j;

	private StringBuilder k;

	private StringBuilder l;

	private LexerBehavior m;

	private Hashtable n;

	private Hashtable o;

	private char[] p = new char[1] { '\'' };

	private char q = '"';

	private char r = '"';

	private char s = '"';

	private char t;

	private string[] u = new string[1] { "--" };

	private string v = "/*";

	private string w = "*/";

	private string x;

	private string y = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

	private char z = '$';

	private char aa = '#';

	private int ab = 3;

	private Token ac;

	private Token ad;

	private Token ae;

	private GetSymbolsHandler af;

	private bool ag;

	private CompareInfo ah = CultureInfo.CurrentCulture.CompareInfo;

	private CultureInfo ai = CultureInfo.CurrentCulture;

	public CultureInfo CultureInfo
	{
		get
		{
			return ai;
		}
		set
		{
			ai = value;
			ah = ai.CompareInfo;
		}
	}

	public Token Current => ac;

	public bool IsEmpty => this.m_c == null;

	public string Text
	{
		get
		{
			if (this.m_e != null)
			{
				return "";
			}
			return this.m_c;
		}
		set
		{
			this.m_c = value;
			this.m_e = null;
			Reset();
		}
	}

	public TextReader TextReader
	{
		get
		{
			return this.m_e;
		}
		set
		{
			this.m_e = value;
			this.m_c = null;
			Reset();
		}
	}

	public string StringQuote
	{
		get
		{
			return new string(p);
		}
		set
		{
			p = value.ToCharArray();
		}
	}

	public int MaxSymbolLength
	{
		get
		{
			return ab;
		}
		set
		{
			ab = value;
		}
	}

	public Hashtable Keywords => o;

	public Hashtable Symbols => n;

	public GetSymbolsHandler GetSymbols
	{
		get
		{
			return af;
		}
		set
		{
			af = value;
		}
	}

	public char[] IdentChars
	{
		get
		{
			if (z == '\0' && aa == '\0')
			{
				return new char[0];
			}
			if (aa == '\0')
			{
				return new char[1] { z };
			}
			return new char[2] { z, aa };
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (value.Length)
			{
			case 0:
				z = '\0';
				aa = '\0';
				break;
			case 1:
				z = value[0];
				aa = '\0';
				break;
			case 2:
				z = value[0];
				aa = value[1];
				break;
			default:
				throw new ArgumentNullException("Lexer does not support more than two custom identifier chars.");
			}
		}
	}

	public string IdentQuote
	{
		get
		{
			return q.ToString();
		}
		set
		{
			if (value == null || value == string.Empty)
			{
				q = '\uffff';
			}
			else
			{
				q = value[0];
			}
		}
	}

	public string IdentQuoteBegin
	{
		get
		{
			return r.ToString();
		}
		set
		{
			if (value == null || value == string.Empty)
			{
				r = '\uffff';
			}
			else
			{
				r = value[0];
			}
		}
	}

	public string IdentQuoteEnd
	{
		get
		{
			return s.ToString();
		}
		set
		{
			if (value == null || value == string.Empty)
			{
				s = '\uffff';
			}
			else
			{
				s = value[0];
			}
		}
	}

	public string[] InlineComments
	{
		get
		{
			return u;
		}
		set
		{
			u = value;
		}
	}

	public string CommentBegin
	{
		get
		{
			return v;
		}
		set
		{
			v = value;
		}
	}

	public string CommentEnd
	{
		get
		{
			return w;
		}
		set
		{
			w = value;
		}
	}

	public string DecimalSeparator
	{
		get
		{
			return y;
		}
		set
		{
			y = value;
		}
	}

	private Lexer(Hashtable A_0, Hashtable A_1, LexerBehavior A_2)
	{
		m = A_2;
		n = A_0;
		o = A_1;
		int num = 0;
		j = -1;
		foreach (DictionaryEntry item in (IEnumerable)A_0)
		{
			switch ((int)item.Value)
			{
			case 10001:
				p = ((string)item.Key).ToCharArray();
				break;
			case 10008:
				t = ((string)item.Key)[0];
				break;
			case 10002:
				q = ((string)item.Key)[0];
				break;
			case 10003:
				r = ((string)item.Key)[0];
				break;
			case 10004:
				s = ((string)item.Key)[0];
				break;
			case 10005:
			{
				if (++num == 1)
				{
					u = new string[1] { item.Key.ToString() };
					break;
				}
				string[] array = new string[num];
				u.CopyTo(array, 0);
				array[num - 1] = item.Key.ToString();
				u = array;
				break;
			}
			case 10006:
				v = (string)item.Key;
				break;
			case 10007:
				w = (string)item.Key;
				break;
			case 10009:
				x = (string)item.Key;
				break;
			}
		}
	}

	public Lexer(TextReader reader, GetSymbolsHandler getSymbolId, Hashtable symbols, Hashtable keywords, LexerBehavior behavior)
		: this(symbols, keywords, behavior)
	{
		this.m_e = reader;
		af = getSymbolId;
		Reset();
	}

	public Lexer(string text, LexerBehavior behavior)
		: this(text, CommonLexem.symbols, CommonLexem.keywords, behavior)
	{
	}

	public Lexer(string text, Hashtable symbols, Hashtable keywords, LexerBehavior behavior)
		: this(symbols, keywords, behavior)
	{
		this.m_c = text;
		Reset();
	}

	public Lexer(TextReader reader, LexerBehavior behavior)
		: this(reader, CommonLexem.symbols, CommonLexem.keywords, behavior)
	{
	}

	public Lexer(TextReader reader, Hashtable symbols, Hashtable keywords, LexerBehavior behavior)
		: this(symbols, keywords, behavior)
	{
		this.m_e = reader;
		Reset();
	}

	public void Reset()
	{
		ad = (ac = Token.Begin);
		ae = null;
		g = 0;
		this.m_d = 0;
		h = 0;
		k = null;
		if (this.m_e != null)
		{
			if (this.m_c == null)
			{
				this.m_f = 0;
				f();
			}
		}
		else
		{
			this.m_f = ((this.m_c != null) ? this.m_c.Length : 0);
		}
	}

	public Token PeekPreviousToken()
	{
		return ad;
	}

	public Token GetNextToken()
	{
		ad = ac;
		if (ae != null)
		{
			ac = ae;
			ae = null;
		}
		else
		{
			ac = a(ac);
		}
		return ac;
	}

	private void f()
	{
		if (this.m_e == null)
		{
			return;
		}
		if (k == null)
		{
			if (h < g && (m & LexerBehavior.OmitTokenValue) == 0 && !ag)
			{
				k = new StringBuilder(this.m_c.Substring(h, g - h));
			}
			else
			{
				h = 0;
			}
		}
		if (j >= 0)
		{
			if (l != null)
			{
				l.Append(this.m_c, 0, g);
			}
			else if (j < g)
			{
				l = new StringBuilder(this.m_c.Substring(j, g - j));
			}
			else
			{
				j = 0;
			}
		}
		char[] array = new char[8192];
		if (g < this.m_f)
		{
			if (g == 0)
			{
				throw new ArgumentException("'CharBufferCapacity' to small.");
			}
			this.m_f -= g;
			this.m_c.CopyTo(g, array, 0, this.m_f);
		}
		else
		{
			this.m_f = 0;
		}
		this.m_f += this.m_e.ReadBlock(array, this.m_f, 8192 - this.m_f);
		this.m_c = new string(array);
		g = 0;
	}

	private char e()
	{
		if (g < this.m_f)
		{
			return this.m_c[g];
		}
		if (this.m_f == 8192)
		{
			f();
			if (g < this.m_f)
			{
				return this.m_c[g];
			}
		}
		return '\uffff';
	}

	private char c(int A_0)
	{
		int num = g + A_0;
		if (num < this.m_f)
		{
			return this.m_c[num];
		}
		if (this.m_f == 8192)
		{
			f();
			A_0 += g;
			if (A_0 < this.m_f)
			{
				return this.m_c[A_0];
			}
		}
		return '\uffff';
	}

	private string b(int A_0)
	{
		if (g + A_0 <= this.m_f)
		{
			return this.m_c.Substring(g, A_0);
		}
		if (this.m_f == 8192)
		{
			f();
		}
		int num = this.m_f - g;
		if (A_0 <= num)
		{
			return this.m_c.Substring(g, A_0);
		}
		return this.m_c.Substring(g, num);
	}

	private char d()
	{
		if (g < this.m_f)
		{
			this.m_d++;
			return this.m_c[g++];
		}
		if (this.m_f == 8192)
		{
			f();
			if (g < this.m_f)
			{
				this.m_d++;
				return this.m_c[g++];
			}
		}
		return '\uffff';
	}

	private void c()
	{
		if (g < this.m_f)
		{
			this.m_d++;
			g++;
		}
		else if (this.m_f == 8192)
		{
			f();
			this.m_d++;
			g++;
		}
	}

	private bool b()
	{
		if (g < this.m_f)
		{
			return false;
		}
		if (this.m_f == 8192)
		{
			f();
			return g >= this.m_f;
		}
		return true;
	}

	private bool b(string A_0)
	{
		int length = A_0.Length;
		if (this.m_f - g < length && this.m_f == 8192)
		{
			f();
		}
		if (this.m_f - g < length || this.m_c[g] != A_0[0])
		{
			return false;
		}
		if (ah.Compare(this.m_c, g, length, A_0, 0, length, CompareOptions.None) == 0)
		{
			this.m_d += length;
			g += length;
			return true;
		}
		return false;
	}

	private bool a(string A_0)
	{
		if (A_0 == null || A_0.Length == 0)
		{
			return false;
		}
		int length = A_0.Length;
		if (this.m_f - g < length && this.m_f == 8192)
		{
			f();
		}
		if (this.m_f - g < length || this.m_c[g] != A_0[0])
		{
			return false;
		}
		return ah.Compare(this.m_c, g, length, A_0, 0, length, CompareOptions.None) == 0;
	}

	private void a(int A_0)
	{
		if (this.m_f - g < A_0 && this.m_f == 8192)
		{
			f();
		}
		int num = Math.Min(this.m_f - g, A_0);
		this.m_d += num;
		g += num;
	}

	private string a()
	{
		if (k == null)
		{
			if ((m & LexerBehavior.OmitTokenValue) != 0)
			{
				return null;
			}
			return this.m_c.Substring(h, g - h - i);
		}
		return k.ToString(0, k.Length);
	}

	public void BeginBlock()
	{
		j = g - (this.m_d - Current.EndPosition);
		if (ae != null)
		{
			j = g - (this.m_d - ae.StartPosition);
			if (j < 0)
			{
				l = new StringBuilder(ae.ToString(), 0, -j, this.m_c.Length);
				j = 0;
			}
		}
	}

	public string EndBlock(int lenEnd)
	{
		string result;
		if (l == null)
		{
			result = this.m_c.Substring(j, g - j - lenEnd);
		}
		else
		{
			if (g != 0)
			{
				l.Append(this.m_c.Substring(0, g));
			}
			result = l.ToString(0, l.Length - lenEnd);
			l = null;
		}
		j = -1;
		return result;
	}

	public string EndBlock(Token to)
	{
		return EndBlock(this.m_d - to.StartPosition);
	}

	private Token a(Token A_0)
	{
		if (this.m_c == null)
		{
			return Token.Empty;
		}
		int num = A_0.EndLineBegin;
		int num2 = A_0.EndLineNumber;
		int num3 = num;
		int num4 = num2;
		bool flag = (m & LexerBehavior.BreakBlank) != 0;
		if (this.m_e == null)
		{
			g = (this.m_d = A_0.EndPosition);
		}
		int num5 = this.m_d;
		h = g;
		this.i = 0;
		k = null;
		bool flag2 = l == null && j == g;
		char A_1 = e();
		bool flag3;
		do
		{
			flag3 = false;
			if (A_1 == '\uffff' && b())
			{
				if (this.m_e != null)
				{
					this.m_e.Close();
				}
				return new aa(TokenType.End, "", 0, num5, num5, num, num2, num3, num4);
			}
			if (A_1 <= ' ')
			{
				bool flag4 = (m & LexerBehavior.OmitBlank) != 0;
				do
				{
					c();
					if (a(ref A_1))
					{
						num4++;
						num3 = this.m_d;
						if (flag && e() <= ' ')
						{
							return new aa(TokenType.Blank, a(), 0, num5, this.m_d, num, num2, num3, num4);
						}
					}
					if (flag4)
					{
						h = g;
						if (flag2)
						{
							j = g;
						}
					}
					else if (k != null)
					{
						k.Append(A_1);
					}
					A_1 = e();
				}
				while ((A_1 != '\uffff' || !b()) && A_1 <= ' ');
			}
			if (this.m_d > num5)
			{
				if ((m & LexerBehavior.OmitBlank) == 0)
				{
					return new aa(TokenType.Blank, a(), 0, num5, this.m_d, num, num2, num3, num4);
				}
				flag3 = true;
				num5 = this.m_d;
			}
			for (int i = 0; i < u.Length; i++)
			{
				string text = u[i];
				if (!b(text))
				{
					continue;
				}
				bool flag5 = (m & LexerBehavior.OmitComment) != 0;
				flag3 = true;
				if (flag5)
				{
					h = g;
				}
				else if (k != null)
				{
					k.Append(text);
				}
				A_1 = d();
				while (true)
				{
					if (a(ref A_1))
					{
						num4++;
						num3 = this.m_d;
						if (flag5)
						{
							h = g;
						}
						A_1 = e();
						break;
					}
					A_1 = d();
					if (A_1 == '\uffff' && b())
					{
						break;
					}
					if (flag5)
					{
						h = g;
					}
					else if (k != null)
					{
						k.Append(A_1);
					}
				}
				if (flag5)
				{
					num5 = this.m_d;
					break;
				}
				return new aa(TokenType.Comment, a(), 0, num5, this.m_d, num, num2, num3, num4);
			}
			if (x != null && b(x))
			{
				this.m_d -= x.Length;
				g -= x.Length;
			}
			else if (b(v))
			{
				bool flag6 = (m & LexerBehavior.OmitComment) != 0;
				flag3 = true;
				if (flag6)
				{
					h = g;
				}
				else if (k != null)
				{
					k.Append(v);
				}
				while (true)
				{
					if (b(w))
					{
						A_1 = e();
						if (flag6)
						{
							h = g;
						}
						else if (k != null)
						{
							k.Append(w);
						}
						break;
					}
					A_1 = d();
					if (a(ref A_1))
					{
						num4++;
						num3 = this.m_d;
					}
					else if (A_1 == '\uffff' && b())
					{
						break;
					}
					if (flag6)
					{
						h = g;
					}
					else if (k != null)
					{
						k.Append(A_1);
					}
				}
				if (!flag6)
				{
					return new aa(TokenType.Comment, a(), 0, num5, this.m_d, num, num2, num3, num4);
				}
				num5 = this.m_d;
			}
			num2 = num4;
			num = num3;
		}
		while (flag3);
		for (int num6 = 0; num6 < p.Length; num6++)
		{
			char c2 = p[num6];
			if (A_1 != c2)
			{
				continue;
			}
			ag = (m & LexerBehavior.OmitTokenStringValue) != 0;
			c();
			if ((m & LexerBehavior.QuotedString) == 0)
			{
				h = g;
			}
			while (true)
			{
				A_1 = d();
				if (a(ref A_1))
				{
					num4++;
					num3 = this.m_d;
				}
				else if (A_1 == '\\' && (m & LexerBehavior.HandleEscaping) != 0)
				{
					if (k != null)
					{
						k.Append(A_1);
					}
					A_1 = d();
				}
				else if (A_1 == c2)
				{
					if ((m & LexerBehavior.StringDoubleQuote) == 0 || e() != c2)
					{
						if ((m & LexerBehavior.QuotedString) != 0)
						{
							if (k != null)
							{
								k.Append(A_1);
							}
						}
						else
						{
							this.i = 1;
						}
						break;
					}
					if (k != null)
					{
						k.Append(A_1);
					}
					A_1 = d();
				}
				else if (A_1 == '\uffff' && b())
				{
					break;
				}
				if (k != null)
				{
					k.Append(A_1);
				}
			}
			string text2 = null;
			if (!ag)
			{
				text2 = a();
				if ((m & LexerBehavior.StringDoubleQuote) != 0)
				{
					text2 = text2.Replace(new string(c2, 2), c2.ToString());
				}
			}
			else
			{
				ag = false;
			}
			return new aa(TokenType.String, text2, 0, num5, this.m_d, num, num2, num3, num4);
		}
		if (A_1 == q || A_1 == r)
		{
			char c3 = ((A_1 == q) ? q : s);
			c();
			bool flag7 = (m & LexerBehavior.QuotedIdent) != 0;
			if (!flag7)
			{
				h = g;
			}
			bool flag8 = false;
			while (true)
			{
				A_1 = d();
				if (A_1 == c3)
				{
					if ((m & LexerBehavior.IdentDoubleQuote) == 0 || e() != c3)
					{
						if (flag7)
						{
							if (k != null)
							{
								k.Append(A_1);
							}
						}
						else
						{
							this.i = 1;
						}
						break;
					}
					flag8 = true;
					if (k != null)
					{
						k.Append(A_1);
					}
					A_1 = d();
				}
				if (a(ref A_1))
				{
					num4++;
					num3 = this.m_d;
				}
				else if (A_1 == '\uffff' && b())
				{
					break;
				}
				if (k != null)
				{
					k.Append(A_1);
				}
			}
			string text3 = a();
			if ((m & LexerBehavior.IdentDoubleQuote) != 0 && flag8)
			{
				text3 = text3.Replace(new string(c3, 2), c3.ToString());
			}
			return new aa(TokenType.Identifier, text3, 0, num5, this.m_d, num, num2, num3, num4);
		}
		if (A_1 == t)
		{
			c();
			if ((m & LexerBehavior.QuotedIdent) == 0)
			{
				h = g;
			}
			A_1 = e();
			if (char.IsLetterOrDigit(A_1) || A_1 == '_' || A_1 == z || A_1 == aa)
			{
				do
				{
					c();
					if (k != null)
					{
						k.Append(A_1);
					}
					A_1 = e();
				}
				while (char.IsLetterOrDigit(A_1) || A_1 == '_' || A_1 == z || A_1 == aa);
			}
			string text4 = a();
			if ((m & LexerBehavior.UpperedIdent) != 0)
			{
				text4 = text4.ToUpper(ai);
			}
			else if ((m & LexerBehavior.LoweredIdent) != 0)
			{
				text4 = text4.ToLower(ai);
			}
			return new aa(TokenType.Identifier, text4, 0, num5, this.m_d, num, num2, num3, num4);
		}
		if (A_1 >= '0' && A_1 <= '9')
		{
			do
			{
				c();
				if (k != null)
				{
					k.Append(A_1);
				}
				A_1 = e();
			}
			while (A_1 >= '0' && A_1 <= '9');
			if (a(y))
			{
				int length = y.Length;
				char c4 = c(length);
				if (c4 >= '0' && c4 <= '9')
				{
					if (k != null)
					{
						k.Append(y);
					}
					a(length);
					A_1 = c4;
					do
					{
						c();
						if (k != null)
						{
							k.Append(A_1);
						}
						A_1 = e();
					}
					while (A_1 >= '0' && A_1 <= '9');
				}
			}
			if (char.IsLetter(A_1))
			{
				do
				{
					c();
					if (k != null)
					{
						k.Append(A_1);
					}
					A_1 = e();
				}
				while ((A_1 >= '0' && A_1 <= '9') || A_1 == '-' || A_1 == '+' || char.IsLetter(A_1));
				return new aa(TokenType.Undefined, a(), 0, num5, this.m_d, num, num2, num3, num4);
			}
			object obj = a();
			try
			{
				if (obj != null)
				{
					obj = ((!(DecimalSeparator == NumberFormatInfo.InvariantInfo.NumberDecimalSeparator)) ? ((object)Convert.ToDecimal(obj, NumberFormatInfo.CurrentInfo)) : ((object)Convert.ToDecimal(obj, NumberFormatInfo.InvariantInfo)));
				}
			}
			catch
			{
			}
			return new aa(TokenType.Number, obj, 0, num5, this.m_d, num, num2, num3, num4);
		}
		if (char.IsLetter(A_1) || A_1 == '_' || A_1 == z || A_1 == aa)
		{
			LexerBehavior lexerBehavior = m;
			m = (LexerBehavior)0;
			string text5;
			try
			{
				do
				{
					c();
					if (k != null)
					{
						k.Append(A_1);
					}
					A_1 = e();
				}
				while (char.IsLetterOrDigit(A_1) || A_1 == '_' || A_1 == z || A_1 == aa);
				text5 = a();
			}
			finally
			{
				m = lexerBehavior;
			}
			object obj3;
			if ((m & LexerBehavior.UpperedIdent) != 0)
			{
				text5 = text5.ToUpper(ai);
				obj3 = o[text5];
			}
			else
			{
				obj3 = o[text5.ToUpper(ai)];
				if ((m & LexerBehavior.LoweredIdent) != 0)
				{
					text5 = text5.ToLower(ai);
				}
			}
			TokenType a_ = TokenType.Keyword;
			if (obj3 == null)
			{
				obj3 = 0;
				a_ = TokenType.Identifier;
			}
			return new aa(a_, text5, (int)obj3, num5, this.m_d, num, num2, num3, num4);
		}
		string text6 = b(ab);
		int num7 = text6.Length;
		while (num7 > 0)
		{
			object obj4 = ((af == null) ? n[text6] : af(text6));
			if (obj4 != null)
			{
				a(num7);
				return new aa(TokenType.Symbol, text6, (int)obj4, num5, this.m_d, num, num2, num3, num4);
			}
			num7--;
			text6 = text6.Substring(0, num7);
		}
		return new aa(TokenType.Char, d(), 0, num5, this.m_d, num, num2, num3, num4);
	}

	private bool a(ref char A_0)
	{
		if (A_0 == '\n')
		{
			return true;
		}
		if (A_0 == '\r')
		{
			if (e() == '\n')
			{
				a(1);
				A_0 = e();
			}
			return true;
		}
		return false;
	}

	public Token LookForSymbols(params string[] symbols)
	{
		if (this.m_c == null)
		{
			return Token.Empty;
		}
		ad = ac;
		ae = null;
		int endLineBegin = ac.EndLineBegin;
		int endLineNumber = ac.EndLineNumber;
		int a_ = endLineBegin;
		int num = endLineNumber;
		if (this.m_e == null)
		{
			g = (this.m_d = ac.EndPosition);
		}
		int num2 = this.m_d;
		h = g;
		this.i = 0;
		k = null;
		char c2 = e();
		if (c2 == '\uffff' && b())
		{
			if (this.m_e != null)
			{
				this.m_e.Close();
			}
			return new aa(TokenType.End, "", 0, num2, num2, endLineBegin, endLineNumber, a_, num);
		}
		while (true)
		{
			bool flag = false;
			foreach (string a_2 in symbols)
			{
				if (a(a_2))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				break;
			}
			c2 = d();
			if (a(ref c2))
			{
				num++;
				a_ = this.m_d;
			}
			else if (c2 == '\uffff' && b())
			{
				break;
			}
			if (k != null)
			{
				k.Append(c2);
			}
		}
		ac = new aa(TokenType.Symbol, a(), 0, num2, this.m_d, endLineBegin, endLineNumber, a_, num);
		return ac;
	}

	public Token PeekNextToken()
	{
		if (ae == null)
		{
			ae = a(ac);
		}
		return ae;
	}
}
