using System;
using System.Collections;
using System.Data;
using System.Globalization;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleType : MarshalByRefObject, IComparable
{
	internal OracleAttributeCollection a;

	internal ay b;

	internal string c;

	internal string d;

	internal int e;

	internal int f;

	internal OracleAttribute g;

	internal OracleDbType h;

	internal bool i;

	internal static Hashtable j = new Hashtable();

	internal short k;

	internal short l;

	internal bool m;

	internal short n;

	internal short o;

	internal byte[] p;

	private Type q;

	internal static readonly OracleType r = new OracleType(null, "", "");

	public string TypeName => d;

	public string SchemaName => c;

	public string Name => OracleUtils.QuoteIfNeed(c) + ((c == string.Empty) ? string.Empty : ".") + OracleUtils.QuoteIfNeed(d);

	public OracleAttributeCollection Attributes => this.a;

	public bool IsDescribed
	{
		get
		{
			if (this.a == null)
			{
				return g != null;
			}
			return true;
		}
	}

	public OracleDbType DbType => h;

	internal int ItemTypeCode => g.b;

	public OracleDbType ItemDbType => g.g;

	public OracleType ItemObjectType => g.h;

	public int ArrayCapacity => f;

	public Type UdtType
	{
		get
		{
			if ((object)q != null)
			{
				return q;
			}
			return DbType switch
			{
				OracleDbType.Object => typeof(OracleObject), 
				OracleDbType.Array => typeof(OracleArray), 
				OracleDbType.Table => typeof(OracleTable), 
				OracleDbType.Xml => typeof(OracleXml), 
				OracleDbType.AnyData => typeof(OracleAnyData), 
				_ => null, 
			};
		}
		set
		{
			q = value;
		}
	}

	internal OracleType(string A_0, string A_1, ay A_2, g A_3)
		: this(A_2, A_0, A_1)
	{
		Hashtable hashtable = (Hashtable)j[A_2];
		if (hashtable[Name] == null)
		{
			hashtable[Name] = this;
		}
		try
		{
			A_3.b(this);
		}
		catch
		{
			hashtable.Remove(Name);
			throw;
		}
	}

	internal OracleType(string A_0, string A_1, ay A_2)
		: this(A_2, A_0, A_1)
	{
		Hashtable hashtable = (Hashtable)j[A_2];
		if (hashtable[Name] == null)
		{
			hashtable[Name] = this;
		}
	}

	private OracleType(ay A_0, string A_1, string A_2)
	{
		this.a = new OracleAttributeCollection();
		if (A_1 == null)
		{
			A_1 = "";
		}
		c = A_1;
		d = A_2;
		b = A_0;
		k = 0;
		l = 2;
	}

	internal void a()
	{
		n = 0;
		o = 0;
		int num = ((this.a != null) ? this.a.Count : 0);
		for (int num2 = 0; num2 < num; num2++)
		{
			OracleAttribute oracleAttribute = this.a[num2];
			switch (oracleAttribute.b)
			{
			case 1:
			case 5:
			case 9:
			case 95:
			case 96:
			case 108:
			case 110:
			case 112:
			case 113:
			case 114:
			case 115:
			case 122:
			case 247:
			case 248:
				oracleAttribute.l = o++;
				break;
			case 2:
			case 4:
			case 6:
				oracleAttribute.l = n;
				n += 23;
				break;
			case 3:
			case 246:
				oracleAttribute.l = n;
				n += 23;
				break;
			case 12:
				oracleAttribute.l = n;
				n += 9;
				break;
			case 187:
			case 188:
			case 232:
				oracleAttribute.l = n;
				n += 14;
				break;
			case 189:
				oracleAttribute.l = n;
				n += 4;
				break;
			case 190:
				oracleAttribute.l = n;
				n += 9;
				break;
			case 100:
				oracleAttribute.l = n;
				n += 5;
				break;
			case 101:
				oracleAttribute.l = n;
				n += 9;
				break;
			case 58:
				if (oracleAttribute.DbType == OracleDbType.Xml)
				{
					oracleAttribute.l = o++;
					break;
				}
				throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
			default:
				throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
			}
		}
	}

	public static OracleType GetObjectType(string name, OracleConnection connection)
	{
		return a(name, connection.d().l());
	}

	internal static OracleType a(string A_0, g A_1)
	{
		int num = A_0.IndexOf(".");
		string text;
		string text2;
		if (num < 0)
		{
			text = string.Empty;
			text2 = A_0;
		}
		else
		{
			text = A_0.Substring(0, num);
			text2 = A_0.Substring(num + 1);
		}
		text = ((text.Length <= 1 || text[0] != '"') ? text.ToUpper(CultureInfo.InvariantCulture) : text.Substring(1, text.Length - 2));
		text2 = ((text2.Length <= 1 || text2[0] != '"') ? text2.ToUpper(CultureInfo.InvariantCulture) : text2.Substring(1, text2.Length - 2));
		return a(text, text2, A_1);
	}

	public static OracleType GetObjectType(string schemaName, string typeName, string connectionString)
	{
		if (typeName == "")
		{
			throw new ArgumentException(Devart.Common.al.a("TypeNameNotDef"));
		}
		ay ay2 = new ay(connectionString);
		OracleType oracleType = null;
		lock (j.SyncRoot)
		{
			Hashtable hashtable = (Hashtable)j[ay2];
			if (hashtable == null)
			{
				hashtable = (Hashtable)(j[ay2] = new Hashtable());
			}
			string key = OracleUtils.QuoteIfNeed(schemaName) + ((schemaName == string.Empty) ? string.Empty : ".") + OracleUtils.QuoteIfNeed(typeName);
			oracleType = (OracleType)hashtable[key];
			if (oracleType == null)
			{
				oracleType = new OracleType(schemaName, typeName, ay2);
			}
		}
		return oracleType;
	}

	internal static OracleType a(string A_0, string A_1, g A_2)
	{
		if (A_2 == null)
		{
			throw new ArgumentNullException("session");
		}
		OracleType oracleType = null;
		lock (j.SyncRoot)
		{
			ay ay2 = A_2.w();
			Hashtable hashtable = (Hashtable)j[ay2];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				j[ay2] = hashtable;
			}
			if (A_0 == "")
			{
				A_0 = A_2.a(ref A_1);
			}
			string key = OracleUtils.QuoteIfNeed(A_0) + ((A_0 == string.Empty) ? string.Empty : ".") + OracleUtils.QuoteIfNeed(A_1);
			oracleType = (OracleType)hashtable[key];
			if (oracleType == null)
			{
				oracleType = new OracleType(A_0, A_1, ay2, A_2);
			}
		}
		return oracleType;
	}

	public static OracleType GetObjectType(string schemaName, string typeName, OracleConnection connection)
	{
		if (typeName == "")
		{
			throw new ArgumentException(Devart.Common.al.a("TypeNameNotDef"));
		}
		ap ap2 = connection.d();
		return a(schemaName, typeName, ap2.l());
	}

	internal static void a(OracleType A_0)
	{
		lock (j.SyncRoot)
		{
			Hashtable hashtable = (Hashtable)j[A_0.b];
			if (hashtable == null)
			{
				hashtable = (Hashtable)(j[A_0.b] = new Hashtable());
			}
			if (hashtable[A_0.Name] == null)
			{
				hashtable[A_0.Name] = A_0;
			}
		}
	}

	public void Describe(OracleConnection connection)
	{
		if (connection.ConnectionOptions != b)
		{
			throw new ArgumentException("ConnectionOptions must coincide");
		}
		bool flag = connection.State == ConnectionState.Open;
		if (!flag)
		{
			connection.Open();
		}
		try
		{
			connection.d().l().b(this);
		}
		finally
		{
			if (!flag)
			{
				connection.Close();
			}
		}
	}

	public override bool Equals(object value)
	{
		OracleType oracleType = (OracleType)value;
		return Name == oracleType.Name;
	}

	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	public int CompareTo(object obj)
	{
		OracleType oracleType = obj as OracleType;
		return Name.CompareTo(oracleType.Name);
	}
}
