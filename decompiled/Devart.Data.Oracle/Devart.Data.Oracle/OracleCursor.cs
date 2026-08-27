using System;
using System.Data;

namespace Devart.Data.Oracle;

public class OracleCursor : MarshalByRefObject, IDisposable
{
	private readonly OracleConnection m_a;

	private readonly s b;

	private OracleDataReader c;

	private int d;

	private int e;

	private OracleNumberMappingCollection f;

	public static readonly OracleCursor Null = new OracleCursor(null, 0);

	public bool IsClosed => b.a();

	internal s Stmt => b;

	internal int Depts
	{
		get
		{
			return e;
		}
		set
		{
			e = value;
		}
	}

	public OracleConnection Connection => this.m_a;

	internal OracleCursor(OracleCommand A_0, string A_1)
	{
		this.m_a = A_0.Connection;
		d = A_0.FetchSize;
		f = A_0.NumberMappings;
		b = null;
	}

	internal OracleCursor(OracleConnection A_0, int A_1)
	{
		this.m_a = A_0;
		d = A_1;
		b = null;
	}

	internal OracleCursor(OracleCommand A_0)
		: this(A_0.Connection, A_0.FetchSize)
	{
		f = A_0.NumberMappings;
	}

	internal OracleCursor(OracleConnection A_0, OracleDataReader A_1, s A_2, OracleNumberMappingCollection A_3)
		: this(A_0, A_2, A_1.FetchSize)
	{
		f = A_3;
		c = A_1;
	}

	internal OracleCursor(OracleConnection A_0, s A_1, int A_2)
	{
		this.m_a = A_0;
		d = A_2;
		b = A_1;
	}

	~OracleCursor()
	{
		try
		{
			a(A_0: false);
		}
		catch
		{
		}
	}

	private void a(bool A_0)
	{
		if (b != null && c == null)
		{
			b.y();
		}
		GC.SuppressFinalize(this);
	}

	public void Dispose()
	{
		a(A_0: true);
	}

	public void Close()
	{
		a(A_0: true);
	}

	public OracleDataReader GetDataReader()
	{
		if (c == null)
		{
			c = new OracleDataReader(b, new s[1] { b }, this.m_a, CommandBehavior.Default, d, 0, e, f);
			this.m_a.a(c);
		}
		return c;
	}

	internal void a(OracleDataReader A_0)
	{
		c = A_0;
	}
}
