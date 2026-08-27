using System;

namespace Devart.Data.Oracle;

public sealed class OracleConnectionErrorEventArgs : EventArgs
{
	private string a;

	private int b;

	public string Message => a;

	public int Code => b;

	internal OracleConnectionErrorEventArgs(string A_0, int A_1)
	{
		a = A_0;
		b = A_1;
	}
}
