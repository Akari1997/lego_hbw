namespace Devart.Common;

internal sealed class SelectColumn : SelectStatementNode
{
	private new string m_a;

	private new string b;

	private string c;

	private new string d;

	private string e;

	private string f;

	public string Database
	{
		get
		{
			return this.m_a;
		}
		set
		{
			this.m_a = value;
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

	public string Table
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

	public string Name
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

	public string Alias
	{
		get
		{
			return e;
		}
		set
		{
			e = value;
			d();
		}
	}

	public string Expression
	{
		get
		{
			return f;
		}
		set
		{
			f = value;
			if (f != null)
			{
				d = (c = (b = (this.m_a = null)));
			}
			d();
		}
	}

	public SelectColumn(string name)
		: this(string.Empty, string.Empty, string.Empty, name, string.Empty)
	{
	}

	public SelectColumn(string schema, string table, string name, string alias)
		: this(string.Empty, schema, table, name, alias, null, -1, -1)
	{
	}

	public SelectColumn(string database, string schema, string table, string name, string alias)
		: this(database, schema, table, name, alias, null, -1, -1)
	{
	}

	internal SelectColumn(string A_0, string A_1, string A_2, string A_3, string A_4, string A_5, int A_6, int A_7)
	{
		this.m_a = A_0;
		b = A_1;
		c = A_2;
		d = A_3;
		f = A_5;
		base.a = A_6;
		base.b = A_7;
		e = A_4;
	}

	public override string ToString()
	{
		return a(" ");
	}

	internal override string a(string A_0)
	{
		string text = ((A_0 == null || Utils.IsEmpty(e)) ? null : (A_0 + e));
		if (!Utils.IsEmpty(Expression))
		{
			return f + text;
		}
		text = d + text;
		if (!Utils.IsEmpty(c))
		{
			text = c + "." + text;
			if (!Utils.IsEmpty(b))
			{
				text = b + "." + text;
			}
			if (!Utils.IsEmpty(this.m_a))
			{
				text = this.m_a + "." + text;
			}
		}
		return text;
	}

	public override bool Equals(object obj)
	{
		if (obj == null || (object)GetType() != obj.GetType())
		{
			return false;
		}
		SelectColumn selectColumn = (SelectColumn)obj;
		if (selectColumn.e == e && selectColumn.d == d && selectColumn.b == b && selectColumn.c == c)
		{
			return selectColumn.m_a == this.m_a;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return this.m_a.GetHashCode() ^ b.GetHashCode() ^ c.GetHashCode() ^ d.GetHashCode() ^ e.GetHashCode();
	}
}
