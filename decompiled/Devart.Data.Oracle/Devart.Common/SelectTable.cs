namespace Devart.Common;

internal sealed class SelectTable : SelectStatementNode
{
	private new string a;

	private new string b;

	private string c;

	private new string d;

	private string e;

	internal string f;

	internal string g;

	public string Database
	{
		get
		{
			return a;
		}
		set
		{
			a = value;
			d();
		}
	}

	public string Schema
	{
		get
		{
			return b;
		}
		set
		{
			b = value;
			d();
		}
	}

	public string Name
	{
		get
		{
			return c;
		}
		set
		{
			c = value;
			d();
		}
	}

	public string Alias
	{
		get
		{
			return d;
		}
		set
		{
			d = value;
			d();
		}
	}

	public string SubQuery
	{
		get
		{
			return e;
		}
		set
		{
			e = value;
			if (e != null)
			{
				c = (b = (a = null));
			}
			d();
		}
	}

	public string JoinClause
	{
		get
		{
			return f;
		}
		set
		{
			f = value;
			d();
		}
	}

	public string JoinCondition
	{
		get
		{
			return g;
		}
		set
		{
			g = value;
			d();
		}
	}

	public SelectTable(string name)
		: this(string.Empty, string.Empty, name, string.Empty)
	{
	}

	public SelectTable(string schema, string name, string alias)
		: this(string.Empty, schema, name, alias, null, -1, -1)
	{
	}

	public SelectTable(string database, string schema, string name, string alias)
		: this(database, schema, name, alias, null, -1, -1)
	{
	}

	internal SelectTable(string A_0, string A_1, string A_2, string A_3, string A_4, int A_5, int A_6)
	{
		a = A_0;
		b = A_1;
		c = A_2;
		d = A_3;
		e = A_4;
		base.a = A_5;
		base.b = A_6;
	}

	public override string ToString()
	{
		string text = (Utils.IsEmpty(d) ? null : (" " + d));
		string text2 = "";
		if (!Utils.IsEmpty(e))
		{
			text2 = e + text;
			if (!Utils.IsEmpty(f))
			{
				text2 = f + " " + text2 + " " + g;
			}
			return text2;
		}
		text2 = (Utils.IsEmpty(b) ? (c + text) : (Utils.IsEmpty(a) ? (b + "." + c + text) : (a + "." + b + "." + c + text)));
		if (!Utils.IsEmpty(f))
		{
			text2 = f + " " + text2 + " " + g;
		}
		return text2;
	}

	public override bool Equals(object obj)
	{
		if (obj == null || (object)GetType() != obj.GetType())
		{
			return false;
		}
		SelectTable selectTable = (SelectTable)obj;
		if (selectTable.d == d && selectTable.c == c && selectTable.b == b)
		{
			return selectTable.a == a;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return a.GetHashCode() ^ b.GetHashCode() ^ c.GetHashCode() ^ d.GetHashCode();
	}
}
