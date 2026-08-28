using System;
using System.Data;

namespace Devart.Common;

public class SqlStatement
{
	private readonly DbScript m_a;

	private readonly int b;

	private readonly int c;

	private readonly int d;

	private readonly int e;

	private readonly uint f;

	private readonly string g;

	private readonly SqlStatementType h;

	public string Text
	{
		get
		{
			a();
			if (g != null)
			{
				return g;
			}
			return this.m_a.b(this);
		}
	}

	public int Offset
	{
		get
		{
			a();
			return b;
		}
	}

	public int Length
	{
		get
		{
			a();
			return c;
		}
	}

	public int LineNumber
	{
		get
		{
			a();
			return d;
		}
	}

	public int LinePosition
	{
		get
		{
			a();
			return e;
		}
	}

	public SqlStatementType StatementType => h;

	protected internal SqlStatement(DbScript script, int offset, int length, int line, int position, string text, SqlStatementType statementType)
	{
		this.m_a = script;
		b = offset;
		c = length;
		d = line;
		e = position;
		f = script.f;
		g = text;
		h = statementType;
	}

	public IDataReader Execute()
	{
		a();
		return this.m_a.a(this);
	}

	private void a()
	{
		if (this.m_a == null || f != this.m_a.f)
		{
			throw new InvalidOperationException("The SqlStatement object is no longer valid due to modification of DbScript.ScriptText property.");
		}
	}
}
