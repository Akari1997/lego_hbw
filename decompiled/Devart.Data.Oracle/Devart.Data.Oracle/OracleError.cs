using System;

namespace Devart.Data.Oracle;

[Serializable]
public class OracleError
{
	private int m_a;

	private int b;

	private string c;

	private int d;

	private int e;

	private string f;

	private string g;

	private OracleObjectType h;

	private bool i;

	public int ArrayBindIndex => this.m_a;

	public int Code => b;

	public string Message => c;

	public int LineNumber => d;

	public int LinePosition => e;

	public OracleObjectType ObjectType => h;

	public string ObjectName => f;

	public string ObjectOwner => g;

	public bool IsRecoverable
	{
		get
		{
			return i;
		}
		internal set
		{
			i = value;
		}
	}

	internal OracleError(int A_0, int A_1, string A_2, OracleObjectType A_3, string A_4, string A_5)
	{
		h = A_3;
		f = A_4;
		this.m_a = A_0;
		b = A_1;
		c = A_2;
		g = A_5;
	}

	public override string ToString()
	{
		return c;
	}

	internal void a(int A_0, int A_1)
	{
		d = A_0;
		e = A_1;
	}
}
