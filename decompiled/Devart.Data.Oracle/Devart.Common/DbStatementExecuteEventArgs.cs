using System;
using System.Data;

namespace Devart.Common;

public class DbStatementExecuteEventArgs : EventArgs
{
	private readonly string a;

	private readonly int b;

	private readonly int c;

	private readonly int d;

	private readonly int e;

	private readonly SqlStatementType f;

	protected IDataReader readerInternal;

	private SqlStatementStatus g = SqlStatementStatus.Continue;

	public string Text => a;

	public int Offset => e;

	public int Length => b;

	public int LineNumber => c;

	public int LinePosition => d;

	public IDataReader Reader
	{
		get
		{
			return readerInternal;
		}
		set
		{
			readerInternal = value;
		}
	}

	public SqlStatementType StatementType => f;

	public SqlStatementStatus StatementStatus
	{
		get
		{
			return g;
		}
		set
		{
			g = value;
		}
	}

	internal DbStatementExecuteEventArgs(string A_0, int A_1, int A_2, int A_3, int A_4, SqlStatementType A_5)
	{
		a = A_0;
		e = A_1;
		b = A_2;
		c = A_3;
		d = A_4;
		f = A_5;
	}
}
