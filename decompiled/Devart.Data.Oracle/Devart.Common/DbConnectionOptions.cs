using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Devart.Common;

internal class DbConnectionOptions
{
	private readonly ArrayList m_a;

	private string m_b;

	internal readonly bool c;

	internal readonly bool d;

	private string e;

	public bool IsEmpty => this.m_a.Count == 0;

	public string this[string keyword]
	{
		get
		{
			return b(keyword)?.d();
		}
		set
		{
			int num = a(keyword, out var A_);
			if (num >= 0)
			{
				am am2 = (am)this.m_a[num];
				StringBuilder stringBuilder = new StringBuilder(this.m_b);
				int length = stringBuilder.Length;
				A_ += am2.e();
				a(stringBuilder, A_, am2.c(), value);
				this.m_b = stringBuilder.ToString();
				am2.a(am2.c() + (stringBuilder.Length - length));
				am2.a(value);
				return;
			}
			StringBuilder stringBuilder2 = new StringBuilder(this.m_b);
			a(stringBuilder2, keyword, value);
			if (A_ > 0 && stringBuilder2[A_] == ';')
			{
				am am3 = (am)this.m_a[this.m_a.Count - 1];
				am3.b(am3.b() + 1);
				A_++;
			}
			this.m_b = stringBuilder2.ToString();
			int length2 = stringBuilder2.Length;
			am value2 = new am(keyword, value, length2 - A_, 0, A_4: false);
			this.m_a.Add(value2);
		}
	}

	public ICollection Keys
	{
		get
		{
			int count = this.m_a.Count;
			string[] array = new string[count];
			for (int num = 0; num < count; num++)
			{
				am am2 = (am)this.m_a[num];
				array[num] = am2.f();
			}
			return array;
		}
	}

	public ICollection Values
	{
		get
		{
			int count = this.m_a.Count;
			string[] array = new string[count];
			for (int num = 0; num < count; num++)
			{
				am am2 = (am)this.m_a[num];
				array[num] = am2.d();
			}
			return array;
		}
	}

	public IDictionary Dictionary
	{
		get
		{
			int count = this.m_a.Count;
			Hashtable hashtable = Utils.CreateHashtable(ignoreCase: true);
			for (int num = 0; num < count; num++)
			{
				am am2 = (am)this.m_a[num];
				hashtable.Add(am2.f(), am2.d());
			}
			return hashtable;
		}
	}

	public int Count => this.m_a.Count;

	public virtual bool Enlist => false;

	public virtual bool TransactionScopeLocal => false;

	public virtual bool ValidateConnection => false;

	public string InitializationCommand => e;

	internal bool HasPersistablePassword
	{
		get
		{
			if (this.c)
			{
				return ConvertValueToBoolean("persist security info", defaultValue: false);
			}
			return true;
		}
	}

	public DbConnectionOptions(string connectionString, Hashtable synonyms, bool useFirstKeyValuePair)
	{
		this.m_a = new ArrayList();
		this.m_b = connectionString;
		this.c = true;
		d = useFirstKeyValuePair;
		ParseConnectionString(synonyms);
		e = ConvertValueToString("initialization command", "");
	}

	protected DbConnectionOptions(DbConnectionOptions from)
	{
		this.m_b = from.m_b;
		this.c = from.c;
		d = from.d;
		ArrayList arrayList = from.m_a;
		int count = arrayList.Count;
		this.m_a = new ArrayList(count);
		for (int num = 0; num < count; num++)
		{
			am a_ = (am)arrayList[num];
			this.m_a.Add(new am(a_));
		}
		e = from.e;
	}

	public void ParseConnectionString(Hashtable synonyms)
	{
		if (this.m_b == null || this.m_b == string.Empty)
		{
			return;
		}
		string text = this.m_b.TrimEnd(new char[0]);
		int num = 0;
		while (num < text.Length)
		{
			string text2 = "";
			bool a_ = false;
			int num2 = text.IndexOf('=', num);
			if (num2 != -1)
			{
				text2 = text.Substring(num, num2 - num).Trim(new char[4] { ' ', ';', '\r', '\n' });
				if (text2 == "")
				{
					throw new ArgumentException(al.a("ParameterNameMissing"));
				}
			}
			int num3;
			for (num3 = num2 + 1; num3 < text.Length && text[num3] == ' '; num3++)
			{
				num2 = num3;
			}
			string text3;
			if (num3 < text.Length)
			{
				if (text[num3] == '"' || text[num3] == '\'')
				{
					char c2 = text[num3];
					num3 = text.IndexOf(c2, num3 + 1);
					if (num3 == -1)
					{
						throw new ArgumentException(string.Format(al.a("ParameterValueMissing"), text2));
					}
					text3 = text.Substring(num2 + 2, num3 - num2 - 2);
					a_ = c2 == '"';
					if (++num3 < text.Length)
					{
						int num4 = num3;
						while (num3 < text.Length && text[num3] == c2)
						{
							num3 = text.IndexOf(c2, num3 + 1);
							if (num3 == -1)
							{
								throw new ArgumentException(string.Format(al.a("ParameterValueMissing"), text2));
							}
							num3++;
							text3 += text.Substring(num4, num3 - num4 - 1);
							num4 = num3;
						}
						for (; num3 < text.Length && text[num3] == ' '; num3++)
						{
						}
						if (num3 != text.Length && text[num3] != ';')
						{
							throw new ArgumentException(al.a("InvalidChar"));
						}
					}
				}
				else
				{
					num3 = text.IndexOf(';', num3);
					if (num3 == -1)
					{
						num3 = text.Length;
					}
					text3 = text.Substring(num2 + 1, num3 - num2 - 1).TrimEnd(new char[0]);
				}
			}
			else
			{
				text3 = string.Empty;
			}
			int num5 = num3;
			if (num3 < text.Length)
			{
				num5++;
			}
			string text4;
			if (synonyms != null)
			{
				text4 = (string)synonyms[Utils.ToLowerInvariant(text2)];
				if (text4 == null)
				{
					throw new InvalidOperationException(al.a("UnknownConnectionStringParameter", text2));
				}
			}
			else
			{
				text4 = text2;
			}
			if (a(text4, out var _) >= 0)
			{
				throw new ArgumentException(string.Format(al.a("DuplicateStringParameter"), text4), text4);
			}
			this.m_a.Add(new am(text4, text3, num5 - num, num2 - num + 1, a_));
			num = num3 + 1;
		}
	}

	internal string c(string A_0)
	{
		int num = a(A_0, out var A_1);
		if (num >= 0)
		{
			am am2 = (am)this.m_a[num];
			this.m_b = this.m_b.Remove(A_1, am2.b());
			this.m_a.RemoveAt(num);
		}
		return this.m_b;
	}

	internal string a(string A_0, string A_1)
	{
		int num = a(A_0, out var _);
		if (num >= 0)
		{
			c(A_0);
		}
		this[A_0] = A_1;
		return this.m_b;
	}

	protected internal bool ContainsKey(string keyName)
	{
		int A_;
		return a(keyName, out A_) >= 0;
	}

	private int a(string A_0, out int A_1)
	{
		A_1 = 0;
		for (int num = 0; num < this.m_a.Count; num++)
		{
			am am2 = (am)this.m_a[num];
			if (Utils.CompareInvariant(am2.f(), A_0))
			{
				return num;
			}
			A_1 += am2.b();
		}
		A_1 = this.m_b.Length;
		return -1;
	}

	private am b(string A_0)
	{
		int num = a(A_0, out var _);
		if (num < 0)
		{
			return null;
		}
		return (am)this.m_a[num];
	}

	public virtual string UsersConnectionString(bool hidePassword)
	{
		if (hidePassword)
		{
			DbConnectionOptions dbConnectionOptions = new DbConnectionOptions(this);
			return dbConnectionOptions.c("Password");
		}
		return this.m_b;
	}

	private static string a(string A_0)
	{
		if (new Regex("^[^\"'=;\\s\\p{Cc}]*$").IsMatch(A_0))
		{
			return A_0;
		}
		if (A_0.IndexOf('"') != -1 && A_0.IndexOf('\'') == -1)
		{
			return "'" + A_0 + "'";
		}
		return "\"" + A_0.Replace("\"", "\"\"") + "\"";
	}

	internal static void a(StringBuilder A_0, string A_1, string A_2)
	{
		if (A_0.Length > 0 && A_0[A_0.Length - 1] != ';')
		{
			A_0.Append(";");
		}
		A_0.Append(A_1);
		A_0.Append("=");
		if (A_2 != null)
		{
			A_0.Append(a(A_2));
		}
		A_0.Append(";");
	}

	internal static void a(StringBuilder A_0, int A_1, int A_2, string A_3)
	{
		int num = A_1 + A_2;
		if (num < A_0.Length && A_0[num] != ';' && A_0[num] != ' ')
		{
			A_2++;
		}
		A_0.Remove(A_1, A_2);
		A_0.Insert(A_1, a(A_3));
	}

	public bool ConvertValueToBoolean(string keyName, bool defaultValue)
	{
		string text = this[keyName];
		if (text == null)
		{
			return defaultValue;
		}
		return ap.c(text);
	}

	public int ConvertValueToInt32(string keyName, int defaultValue)
	{
		string text = this[keyName];
		if (text == null)
		{
			return defaultValue;
		}
		return ap.b(text);
	}

	public string ConvertValueToString(string keyName, string defaultValue)
	{
		string text = this[keyName];
		if (text == null)
		{
			return defaultValue;
		}
		return ap.a(text);
	}

	public string ConvertValueToStringWithQuotes(string keyName, string defaultValue)
	{
		am am2 = b(keyName);
		string text = null;
		if (am2 != null)
		{
			text = am2.d();
		}
		if (text == null)
		{
			return defaultValue;
		}
		text = ap.a(am2.d());
		if (am2.a())
		{
			text = $"\"{text}\"";
		}
		return text;
	}

	public void Clear()
	{
		this.m_a.Clear();
		this.m_b = "";
	}

	public override string ToString()
	{
		return this.m_b;
	}

	public static string RemoveKeyValuePairs(string connectionString, string[] keyNames)
	{
		Utils.CheckArgumentNull(connectionString, "connectionString");
		Utils.CheckArgumentNull(keyNames, "keyNames");
		DbConnectionOptions dbConnectionOptions = new DbConnectionOptions(connectionString, null, useFirstKeyValuePair: false);
		for (int num = 0; num < keyNames.Length; num++)
		{
			dbConnectionOptions.c(keyNames[num]);
		}
		return dbConnectionOptions.ToString();
	}

	public override bool Equals(object obj)
	{
		if (!(obj is DbConnectionOptions dbConnectionOptions))
		{
			return false;
		}
		return e == dbConnectionOptions.e;
	}

	public override int GetHashCode()
	{
		return e.GetHashCode();
	}
}
