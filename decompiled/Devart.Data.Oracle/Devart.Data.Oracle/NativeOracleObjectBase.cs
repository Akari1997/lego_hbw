using System;
using Devart.Common;

namespace Devart.Data.Oracle;

public abstract class NativeOracleObjectBase : MarshalByRefObject, IDisposable
{
	private w m_a;

	private OracleConnection b;

	private bool c;

	internal w Instance => this.m_a;

	public OracleType ObjectType => this.m_a.get_ObjectType();

	public bool IsNull
	{
		get
		{
			return this.m_a.get_IsNull();
		}
		set
		{
			this.m_a.set_IsNull(value);
		}
	}

	public OracleConnection Connection => b;

	public object this[string Name]
	{
		get
		{
			NativeOracleObjectBase nativeOracleObjectBase = a(Name, out var A_, out var A_2);
			if (A_ < Name.Length)
			{
				return nativeOracleObjectBase.GetItemValue(Name.Substring(A_));
			}
			return nativeOracleObjectBase.GetItemValue(A_2);
		}
		set
		{
			NativeOracleObjectBase nativeOracleObjectBase = a(Name, out var A_, out var A_2);
			if (A_ < Name.Length)
			{
				nativeOracleObjectBase.SetItemValue(value, Name.Substring(A_));
			}
			else
			{
				nativeOracleObjectBase.SetItemValue(value, A_2);
			}
		}
	}

	public NativeOracleObjectBase(string typeName, OracleConnection connection)
		: this(OracleType.GetObjectType(typeName, connection), connection)
	{
	}

	public NativeOracleObjectBase(OracleType objectType, OracleConnection connection)
	{
		Utils.CheckArgumentNull(connection, "connection");
		g g2 = connection.d().l();
		this.m_a = g2.h().a(g2, objectType);
		b = connection;
		c = true;
	}

	internal NativeOracleObjectBase(w A_0, OracleConnection A_1, bool A_2)
	{
		this.m_a = A_0;
		b = A_1;
		c = A_2;
	}

	public void Dispose()
	{
		if (c && this.m_a is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}

	protected abstract object GetItemValue(int i);

	protected abstract void SetItemValue(object value, int i);

	protected abstract object GetItemValue(string name);

	protected abstract void SetItemValue(object value, string name);

	private NativeOracleObjectBase a(string A_0, out int A_1, out int A_2)
	{
		NativeOracleObjectBase nativeOracleObjectBase = this;
		A_1 = 0;
		A_2 = 0;
		for (int num = 0; num < A_0.Length; num++)
		{
			switch (A_0[num])
			{
			case '.':
			case '[':
				nativeOracleObjectBase = ((A_1 != num) ? ((NativeOracleObjectBase)nativeOracleObjectBase.GetItemValue(A_0.Substring(A_1, num - A_1))) : ((NativeOracleObjectBase)nativeOracleObjectBase.GetItemValue(A_2)));
				A_1 = num + 1;
				break;
			case ',':
				nativeOracleObjectBase = (NativeOracleObjectBase)nativeOracleObjectBase.GetItemValue(Convert.ToInt32(A_0.Substring(A_1, num - A_1)));
				A_1 = num + 1;
				break;
			case ']':
				A_2 = Convert.ToInt32(A_0.Substring(A_1, num - A_1));
				A_1 = num + 1;
				break;
			}
		}
		return nativeOracleObjectBase;
	}

	public void Flush()
	{
		((r)this.m_a).m();
	}

	public void Refresh()
	{
		((r)this.m_a).q();
	}

	protected object ConvertFromInternalValue(object value, OracleAttribute attribute)
	{
		switch (attribute.DbType)
		{
		case OracleDbType.Object:
			if (value is bf a_2)
			{
				return new NativeOracleObject(a_2, b, A_2: false);
			}
			break;
		case OracleDbType.Array:
		case OracleDbType.Table:
			if (value is ak a_4)
			{
				if (attribute.DbType == OracleDbType.Array)
				{
					return new NativeOracleArray(a_4, b, A_2: false);
				}
				return new NativeOracleTable(a_4, b, A_2: false);
			}
			break;
		case OracleDbType.BFile:
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
			if (value is a3 a_5)
			{
				OracleLob oracleLob = ((attribute.DbType == OracleDbType.BFile) ? new OracleBFile(b, null, 0, a_5) : new OracleLob(b, null, 0, a_5, attribute.DbType));
				oracleLob.MakeDisconnected();
				return oracleLob;
			}
			break;
		case OracleDbType.Xml:
			if (value is al a_3)
			{
				OracleXml oracleXml = new OracleXml(a_3, b);
				oracleXml.c();
				return oracleXml;
			}
			break;
		case OracleDbType.Ref:
			if (value is f a_)
			{
				return new OracleRef(a_, attribute.h, b);
			}
			break;
		}
		return value;
	}

	protected object GetInternalValue(object value, OracleDbType oracleDbType)
	{
		switch (oracleDbType)
		{
		case OracleDbType.Object:
			if (value is NativeOracleObject nativeOracleObject)
			{
				return nativeOracleObject.Instance;
			}
			break;
		case OracleDbType.Array:
		case OracleDbType.Table:
			if (value is NativeOracleArray nativeOracleArray)
			{
				return nativeOracleArray.Instance;
			}
			break;
		case OracleDbType.BFile:
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
			if (value is OracleLob oracleLob)
			{
				oracleLob.Connection = b;
				value = oracleLob.LobLocator;
			}
			break;
		case OracleDbType.Xml:
			if (value is OracleXml oracleXml)
			{
				oracleXml.Connection = b;
				value = oracleXml.XmlObject;
			}
			break;
		}
		return value;
	}
}
