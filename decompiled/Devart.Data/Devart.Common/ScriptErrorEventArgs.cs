using System;

namespace Devart.Common;

public sealed class ScriptErrorEventArgs : EventArgs
{
	private readonly Exception a;

	private bool b;

	private readonly int c;

	private readonly int d;

	private readonly int e;

	private readonly int f;

	private readonly string g;

	private SqlStatementType h;

	public Exception Exception => a;

	public bool Ignore
	{
		get
		{
			return b;
		}
		set
		{
			b = value;
		}
	}

	public string Text => g;

	public int Offset => f;

	public int Length => c;

	public int LineNumber => d;

	public int LinePosition => e;

	public SqlStatementType StatementType => h;

	public ScriptErrorEventArgs(Exception e, string text, int offset, int length, int lineNumber, int linePosition, SqlStatementType statementType)
	{
		a = e;
		g = text;
		f = offset;
		c = length;
		d = lineNumber;
		this.e = linePosition;
		h = statementType;
	}
}
