using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(d))]
public class OracleRef : MarshalByRefObject, IDisposable
{
	private OracleConnection m_a;

	private OracleType b;

	private byte[] c;

	private object d;

	private f e;

	internal f Handle
	{
		get
		{
			Utils.CheckArgumentNull(this.m_a, "connection");
			g g2 = this.m_a.d().l();
			if (e == null || e.b() != g2)
			{
				a();
				e = g2.h().a(g2, b, c);
			}
			return e;
		}
	}

	public OracleConnection Connection
	{
		get
		{
			return this.m_a;
		}
		set
		{
			this.m_a = value;
		}
	}

	public OracleType RefType
	{
		get
		{
			return b;
		}
		internal set
		{
			b = value;
		}
	}

	public string Value => ToString();

	public bool IsNull => c == null;

	public bool IsPinned => d != null;

	public OracleRef(string value)
		: this(value, null, null)
	{
	}

	public OracleRef(string value, OracleType refType)
		: this(value, refType, null)
	{
	}

	public OracleRef(string value, OracleType refType, OracleConnection connection)
	{
		this.m_a = connection;
		b = refType;
		if (value != null)
		{
			byte[] array = new byte[value.Length / 2];
			for (int num = 0; num < array.Length; num++)
			{
				string text = value.Substring(num * 2, 2);
				array[num] = byte.Parse(text, NumberStyles.AllowHexSpecifier);
			}
			c = array;
		}
	}

	internal OracleRef(f A_0, OracleType A_1, OracleConnection A_2)
	{
		this.m_a = A_2;
		b = A_1;
		if (A_0 != null)
		{
			c = A_0.a();
		}
	}

	public void Dispose()
	{
		a();
	}

	public void Close()
	{
		Dispose();
	}

	public object GetObject()
	{
		if (d == null)
		{
			if (c == null)
			{
				return null;
			}
			d = k.a(Handle.a(this.m_a), b, this.m_a);
		}
		return d;
	}

	public NativeOracleObjectBase GetNativeObject()
	{
		if (c == null)
		{
			return null;
		}
		object obj = Handle.a(this.m_a);
		return b.DbType switch
		{
			OracleDbType.Object => new NativeOracleObject((bf)obj, this.m_a, A_2: false), 
			OracleDbType.Array => new NativeOracleArray((ak)obj, this.m_a, A_2: false), 
			OracleDbType.Table => new NativeOracleTable((ak)obj, this.m_a, A_2: false), 
			_ => throw new InvalidOperationException(), 
		};
	}

	public void Pin()
	{
	}

	public void Pin(OracleConnection connection)
	{
		Connection = connection;
		Pin();
	}

	public void Unpin()
	{
	}

	public override string ToString()
	{
		if (c == null)
		{
			return null;
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int num = 0; num < c.Length; num++)
		{
			stringBuilder.Append($"{c[num]:X2}");
		}
		return stringBuilder.ToString();
	}

	private void a()
	{
		if (e != null)
		{
			e.Dispose();
			e = null;
		}
	}
}
