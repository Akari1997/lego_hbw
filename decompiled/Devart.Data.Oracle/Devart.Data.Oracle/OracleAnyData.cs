using System;
using System.Data.SqlTypes;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleAnyData : INullable, IDisposable
{
	private object m_a;

	private object b;

	private OracleDbType c;

	private OracleConnection d;

	private static OracleAnyData e = new OracleAnyData(OracleDbType.VarChar, null);

	private b f;

	public static OracleAnyData Null => e;

	public bool IsNull
	{
		get
		{
			if (this.m_a != null)
			{
				return this.m_a == DBNull.Value;
			}
			return true;
		}
	}

	public OracleDbType OracleDbType => c;

	internal b AnyDataObject
	{
		get
		{
			Utils.CheckArgumentNull(d, "connection");
			g g2 = d.d().l();
			if (f == null || f.a() != g2)
			{
				object obj = ((this.m_a is ICustomOracleObject) ? ((ICustomOracleObject)this.m_a).ToOracleObject(d) : ((!(this.m_a is ICustomOracleArray)) ? this.m_a : ((ICustomOracleArray)this.m_a).ToOracleArray(d)));
				try
				{
					f = g2.h().a(g2, c, obj, d);
				}
				finally
				{
					if (obj != this.m_a)
					{
						((IDisposable)obj).Dispose();
					}
				}
			}
			return f;
		}
	}

	internal OracleConnection Connection
	{
		get
		{
			return d;
		}
		set
		{
			if (value != d)
			{
				a();
				d = value;
			}
		}
	}

	public object OracleValue => this.m_a;

	public object Value
	{
		get
		{
			if (b != null)
			{
				if (b == DBNull.Value)
				{
					return null;
				}
				return b;
			}
			if (this.m_a is OracleCursor || this.m_a is OracleArray || this.m_a is OracleObject || this.m_a is OracleRef || this.m_a is OracleTable)
			{
				return this.m_a;
			}
			b = OracleUtils.a(OracleValue, c, A_2: true);
			return b;
		}
	}

	internal OracleAnyData(b A_0, OracleConnection A_1)
	{
		d = A_1;
		f = A_0;
		this.m_a = A_0.a(A_1, out c);
	}

	public OracleAnyData(OracleDbType oracleDbType, object oracleValue)
	{
		this.m_a = oracleValue;
		c = oracleDbType;
	}

	public void Dispose()
	{
		a();
	}

	private void a()
	{
		if (f != null)
		{
			f.Dispose();
			f = null;
		}
	}
}
