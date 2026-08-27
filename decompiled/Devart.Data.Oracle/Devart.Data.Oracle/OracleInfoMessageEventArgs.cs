using System;

namespace Devart.Data.Oracle;

public class OracleInfoMessageEventArgs : EventArgs
{
	private string m_a;

	private int b;

	private OracleErrorCollection c;

	private string d;

	public string Message => this.m_a;

	public int Code => b;

	public string Source => d;

	public OracleErrorCollection Errors => c;

	internal OracleInfoMessageEventArgs(string A_0, int A_1, string A_2)
	{
		this.m_a = A_0;
		b = A_1;
		d = A_2;
	}

	internal void a(OracleErrorCollection A_0)
	{
		c = A_0;
	}
}
