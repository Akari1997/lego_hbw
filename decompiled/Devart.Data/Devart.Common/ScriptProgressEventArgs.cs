using System;

namespace Devart.Common;

public sealed class ScriptProgressEventArgs : EventArgs
{
	private readonly string a;

	private readonly int b;

	private readonly int c;

	private readonly int d;

	private readonly int e;

	private SqlStatementType f;

	public string Text => a;

	public int Offset => e;

	public int Length => b;

	public int LineNumber => c;

	public int LinePosition => c;

	public SqlStatementType StatementType => f;

	internal ScriptProgressEventArgs(string A_0, int A_1, int A_2, int A_3, int A_4, SqlStatementType A_5)
	{
		a = A_0;
		e = A_1;
		b = A_2;
		c = A_3;
		d = A_4;
		f = A_5;
	}
}
