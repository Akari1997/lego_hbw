using System;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleObject : MarshalByRefObject, ICustomOracleObject, ICloneable, INullable, bf
{
	internal OracleType a;

	internal OracleObject b;

	private bool m_c;

	private object[] m_d;

	private byte[] e;

	private static readonly OracleObject f = new OracleObject(OracleType.r);

	public static OracleObject Null => f;

	public OracleType ObjectType => this.a;

	public object this[string Name]
	{
		get
		{
			if (this.a == OracleType.r)
			{
				return DBNull.Value;
			}
			return this[Attributes[Name]];
		}
		set
		{
			if (this.a == OracleType.r)
			{
				throw new InvalidOperationException();
			}
			this[Attributes[Name]] = value;
		}
	}

	public object this[OracleAttribute attribute]
	{
		get
		{
			if (this.a == OracleType.r)
			{
				return DBNull.Value;
			}
			return d(GetOracleValue(attribute), attribute);
		}
		set
		{
			if (this.a == OracleType.r)
			{
				throw new InvalidOperationException();
			}
			a(b(value, attribute), attribute);
		}
	}

	internal OracleAttributeCollection Attributes => this.a.a;

	public bool IsNull
	{
		get
		{
			return this.m_c;
		}
		set
		{
			if (value == this.m_c)
			{
				return;
			}
			this.m_c = value;
			if (value)
			{
				return;
			}
			foreach (OracleAttribute item in Attributes)
			{
				a((object)null, item);
			}
			if (this.b != null)
			{
				this.b.SetObjectIsNull(isNull: false, this);
			}
		}
	}

	public OracleObject(string typeName, OracleConnection connection)
		: this(OracleType.GetObjectType(typeName, connection))
	{
	}

	public OracleObject(OracleType oraType)
	{
		this.m_c = true;
		this.a = oraType;
		this.m_d = new object[oraType.o];
		e = new byte[oraType.n];
	}

	internal static object a(bf A_0, OracleConnection A_1, bool A_2)
	{
		if (A_0 == null)
		{
			return null;
		}
		Type udtType = A_0.get_ObjectType().UdtType;
		if ((object)A_0.GetType() == udtType)
		{
			return A_0;
		}
		ICustomOracleObject customOracleObject = (((object)udtType != typeof(OracleObject)) ? ((ICustomOracleObject)Activator.CreateInstance(udtType)) : new OracleObject(A_0.get_ObjectType()));
		NativeOracleObject nativeOracleObject = new NativeOracleObject(A_0, A_1, A_2);
		customOracleObject.FromOracleObject(nativeOracleObject);
		nativeOracleObject.Dispose();
		return customOracleObject;
	}

	internal static object d(object A_0, OracleAttribute A_1)
	{
		if (A_0 == null || A_0 == DBNull.Value)
		{
			return A_0;
		}
		switch (A_1.b)
		{
		case 1:
		case 3:
		case 5:
		case 9:
		case 95:
		case 96:
		case 108:
		case 110:
		case 122:
		case 246:
		case 247:
		case 248:
			return A_0;
		case 12:
			return ((OracleDate)A_0).Value;
		case 114:
		case 115:
			if (A_0 is OracleBFile)
			{
				OracleBFile oracleBFile = (OracleBFile)A_0;
				object value = DBNull.Value;
				if (!oracleBFile.IsNull)
				{
					oracleBFile.OpenFile();
					try
					{
						value = oracleBFile.Value;
					}
					finally
					{
						oracleBFile.CloseFile();
					}
				}
				return value;
			}
			return A_0;
		case 112:
		case 113:
			if (A_0 is OracleLob)
			{
				return ((OracleLob)A_0).Value;
			}
			return A_0;
		case 2:
		case 4:
		case 6:
			switch (A_1.g)
			{
			case OracleDbType.Integer:
				return OracleNumber.d((OracleNumber)A_0);
			case OracleDbType.Double:
				return OracleNumber.b((OracleNumber)A_0);
			default:
				if (!(A_0 is OracleNumber oracleNumber))
				{
					return A_0;
				}
				return oracleNumber.Value;
			}
		case 187:
		case 188:
		case 232:
			return ((OracleTimeStamp)A_0).Value;
		case 189:
			return ((OracleIntervalYM)A_0).Value;
		case 190:
			return ((OracleIntervalDS)A_0).Value;
		case 100:
			return A_0;
		case 101:
			return A_0;
		case 58:
			if (A_1.DbType == OracleDbType.Xml)
			{
				if (A_0 is OracleXml)
				{
					return ((OracleXml)A_0).Value;
				}
				if (A_0 is string)
				{
					return A_0;
				}
			}
			throw new InvalidOperationException();
		default:
			throw new InvalidOperationException();
		}
	}

	internal void a(string A_0, object A_1)
	{
		if (A_0 == null || A_0 == "")
		{
			throw new ArgumentNullException();
		}
		int num = A_0.IndexOf('.');
		string name;
		string text;
		if (num < 0)
		{
			name = A_0;
			text = "";
			this[name] = A_1;
			return;
		}
		name = A_0.Substring(0, num);
		text = A_0.Substring(num + 1);
		object obj = this[name];
		if (!(text != ""))
		{
			return;
		}
		OracleObject oracleObject = obj as OracleObject;
		if (oracleObject == null)
		{
			OracleAttribute oracleAttribute = this.a.Attributes[name];
			if (oracleAttribute.DbType == OracleDbType.Object)
			{
				oracleObject = (OracleObject)(this[name] = new OracleObject(oracleAttribute.h));
			}
		}
		oracleObject?.a(text, A_1);
	}

	internal object a(string A_0)
	{
		if (A_0 == null || A_0 == "")
		{
			throw new ArgumentNullException();
		}
		int num = A_0.IndexOf('.');
		string name;
		string text;
		if (num < 0)
		{
			name = A_0;
			text = "";
		}
		else
		{
			name = A_0.Substring(0, num);
			text = A_0.Substring(num + 1);
		}
		object obj = this[name];
		if (text != "" && obj is OracleObject oracleObject)
		{
			return oracleObject.a(text);
		}
		return obj;
	}

	public object GetOracleValue(OracleAttribute attribute)
	{
		return a(attribute);
	}

	object bf.b(OracleAttribute attribute)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetOracleValue
		return this.GetOracleValue(attribute);
	}

	public object GetOracleValue(string attributeName)
	{
		return GetOracleValue(Attributes[attributeName]);
	}

	void ICustomOracleObject.FromOracleObject(NativeOracleObject oraObject)
	{
		IsNull = oraObject.IsNull;
		if (IsNull)
		{
			return;
		}
		foreach (OracleAttribute attribute in oraObject.ObjectType.Attributes)
		{
			object oracleValue = oraObject.GetOracleValue(attribute);
			oracleValue = c(oracleValue, attribute);
			a(oracleValue, attribute);
		}
	}

	NativeOracleObject ICustomOracleObject.ToOracleObject(OracleConnection con)
	{
		NativeOracleObject nativeOracleObject = new NativeOracleObject(this.a, con);
		a(con, nativeOracleObject);
		return nativeOracleObject;
	}

	internal void a(OracleConnection A_0, NativeOracleObject A_1)
	{
		A_1.IsNull = IsNull;
		if (IsNull)
		{
			return;
		}
		foreach (OracleAttribute item in Attributes)
		{
			object obj = a(item);
			switch (item.DbType)
			{
			case OracleDbType.Object:
				if (item.h.i && obj is OracleObject oracleObject)
				{
					A_1.SetAttributeNotNull(item);
					NativeOracleObject a_ = (NativeOracleObject)A_1[item];
					oracleObject.a(A_0, a_);
					continue;
				}
				if (obj is ICustomOracleObject customOracleObject)
				{
					obj = customOracleObject.ToOracleObject(A_0);
				}
				break;
			case OracleDbType.Array:
			case OracleDbType.Table:
				if (obj is ICustomOracleArray customOracleArray)
				{
					obj = customOracleArray.ToOracleArray(A_0);
				}
				break;
			}
			A_1[item] = obj;
		}
	}

	internal static object c(object A_0, OracleAttribute A_1)
	{
		switch (A_1.DbType)
		{
		case OracleDbType.Object:
			if (A_0 is NativeOracleObject nativeOracleObject)
			{
				ICustomOracleObject customOracleObject = new OracleObject(nativeOracleObject.ObjectType);
				customOracleObject.FromOracleObject(nativeOracleObject);
				return customOracleObject;
			}
			break;
		case OracleDbType.Table:
			if (A_0 is NativeOracleTable nativeOracleTable)
			{
				ICustomOracleArray customOracleArray2 = new OracleTable(nativeOracleTable.ObjectType);
				customOracleArray2.FromOracleArray(nativeOracleTable);
				return customOracleArray2;
			}
			break;
		case OracleDbType.Array:
			if (A_0 is NativeOracleArray nativeOracleArray)
			{
				ICustomOracleArray customOracleArray = new OracleArray(nativeOracleArray.ObjectType);
				customOracleArray.FromOracleArray(nativeOracleArray);
				return customOracleArray;
			}
			break;
		}
		return A_0;
	}

	internal static object b(object A_0, OracleAttribute A_1)
	{
		if (A_0 == null || A_0 == DBNull.Value)
		{
			return A_0;
		}
		switch (A_1.b)
		{
		case 108:
		case 122:
		case 247:
		case 248:
			A_0 = c(A_0, A_1);
			if (!(A_0 is ICustomOracleObject) && !(A_0 is ICustomOracleArray))
			{
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			break;
		case 110:
			if (!(A_0 is OracleRef))
			{
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			break;
		case 1:
		case 5:
		case 9:
		case 96:
			if (!(A_0 is string))
			{
				A_0 = A_0.ToString();
			}
			break;
		case 114:
		case 115:
			if (!(A_0 is OracleBFile))
			{
				throw new InvalidOperationException(Devart.Common.al.a("CanNotWriteToBFILE"));
			}
			break;
		case 112:
			if (!(A_0 is OracleLob))
			{
				string text = A_0.ToString();
				OracleLob oracleLob3 = new OracleLob(A_1.DbType, isUnicode: true);
				byte[] bytes = Encoding.Unicode.GetBytes(text);
				oracleLob3.Write(bytes, 0, bytes.Length);
				A_0 = oracleLob3;
			}
			break;
		case 113:
			if (!(A_0 is OracleLob))
			{
				if (!(A_0 is byte[] array))
				{
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				OracleLob oracleLob = new OracleLob(OracleDbType.Blob);
				oracleLob.Write(array, 0, array.Length);
				A_0 = oracleLob;
			}
			break;
		case 95:
			if (A_0 is byte[])
			{
				break;
			}
			if (A_0 is OracleLob)
			{
				OracleLob oracleLob2 = (OracleLob)A_0;
				if (oracleLob2.LobType != OracleDbType.Blob && oracleLob2.LobType != OracleDbType.BFile)
				{
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				A_0 = oracleLob2.Value;
			}
			else
			{
				if (!(A_0 is OracleBinary oracleBinary))
				{
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				A_0 = oracleBinary.Value;
			}
			break;
		case 2:
		case 3:
		case 4:
		case 6:
		case 246:
			if (A_0 is OracleNumber)
			{
				break;
			}
			if (A_0 is decimal || A_0 is string)
			{
				A_0 = new OracleNumber(Convert.ToDecimal(A_0));
				break;
			}
			if (A_0 is long || A_0 is int || A_0 is uint || A_0 is short || A_0 is ushort || A_0 is byte || A_0 is sbyte)
			{
				long value = Convert.ToInt64(A_0);
				A_0 = new OracleNumber(value);
				break;
			}
			if (A_0 is ulong value2)
			{
				A_0 = new OracleNumber((long)value2);
				break;
			}
			if (A_0 is double value3)
			{
				A_0 = new OracleNumber(value3);
				break;
			}
			if (A_0 is float num)
			{
				A_0 = new OracleNumber((double)num);
				break;
			}
			throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
		case 100:
			if (!(A_0 is float))
			{
				if (!(A_0 is long) && !(A_0 is ulong) && !(A_0 is int) && !(A_0 is uint) && !(A_0 is short) && !(A_0 is ushort) && !(A_0 is byte) && !(A_0 is sbyte) && !(A_0 is double) && !(A_0 is int))
				{
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				A_0 = Convert.ToSingle(A_0);
			}
			break;
		case 101:
			if (!(A_0 is double))
			{
				if (!(A_0 is long) && !(A_0 is ulong) && !(A_0 is int) && !(A_0 is uint) && !(A_0 is short) && !(A_0 is ushort) && !(A_0 is byte) && !(A_0 is sbyte) && !(A_0 is float) && !(A_0 is int))
				{
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				A_0 = Convert.ToDouble(A_0);
			}
			break;
		case 12:
			if (A_0 is OracleDate)
			{
				break;
			}
			if (A_0 is DateTime)
			{
				A_0 = new OracleDate((DateTime)A_0);
				break;
			}
			if (A_0 is string)
			{
				A_0 = OracleDate.Parse((string)A_0);
				break;
			}
			if (A_0 is OracleTimeStamp oracleTimeStamp)
			{
				A_0 = oracleTimeStamp.ToOracleDate();
				break;
			}
			throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
		case 187:
		case 188:
		case 232:
			if (A_0 is OracleDateTime && OracleUtils.OracleClientCompatible)
			{
				A_0 = ((OracleDateTime)A_0).a;
			}
			if (!(A_0 is OracleTimeStamp))
			{
				if (!(A_0 is DateTime))
				{
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				A_0 = new OracleTimeStamp((DateTime)A_0);
			}
			break;
		case 189:
			if (A_0 is OracleMonthSpan && OracleUtils.OracleClientCompatible)
			{
				A_0 = ((OracleMonthSpan)A_0).a;
			}
			if (!(A_0 is OracleIntervalYM))
			{
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			break;
		case 190:
			if (A_0 is OracleTimeSpan && OracleUtils.OracleClientCompatible)
			{
				A_0 = ((OracleTimeSpan)A_0).a;
			}
			if (!(A_0 is OracleIntervalDS))
			{
				if (!(A_0 is TimeSpan))
				{
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				A_0 = new OracleIntervalDS((TimeSpan)A_0);
			}
			break;
		case 58:
			if (A_1.DbType != OracleDbType.Xml)
			{
				throw new InvalidOperationException();
			}
			if (A_0 is OracleXml)
			{
				break;
			}
			if (A_0 is string)
			{
				A_0 = new OracleXml((string)A_0);
				break;
			}
			if (A_0 is char[])
			{
				A_0 = new OracleXml((char[])A_0);
				break;
			}
			throw new InvalidOperationException();
		default:
			throw new InvalidOperationException();
		}
		return A_0;
	}

	private void a(OracleAttribute A_0, bool A_1)
	{
		if (!A_1)
		{
			IsNull = false;
		}
		switch (A_0.b)
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
			if (A_1)
			{
				this.m_d[A_0.l] = null;
			}
			break;
		case 58:
			if (A_0.DbType == OracleDbType.Xml)
			{
				if (A_1)
				{
					this.m_d[A_0.l] = null;
				}
				break;
			}
			throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
		default:
			if (A_1)
			{
				e[A_0.l] = 0;
			}
			else
			{
				e[A_0.l] = byte.MaxValue;
			}
			break;
		}
	}

	private void a(object A_0, OracleAttribute A_1)
	{
		bool flag = A_0 == null || A_0 == DBNull.Value;
		a(A_1, flag);
		if (flag)
		{
			return;
		}
		int num = A_1.l;
		switch (A_1.b)
		{
		case 108:
			if (this.m_d[num] is OracleObject oracleObject)
			{
				oracleObject.b = null;
			}
			this.m_d[num] = A_0;
			if (A_0 is OracleObject oracleObject2)
			{
				oracleObject2.b = this;
			}
			break;
		case 122:
		case 247:
		case 248:
			if (this.m_d[num] is OracleArray oracleArray)
			{
				oracleArray.a = null;
			}
			this.m_d[num] = A_0;
			if (A_0 is OracleArray oracleArray2)
			{
				oracleArray2.a = this;
			}
			break;
		case 58:
			if (A_1.DbType == OracleDbType.Xml)
			{
				this.m_d[num] = A_0;
				break;
			}
			throw new InvalidOperationException();
		case 1:
		case 5:
		case 9:
		case 95:
		case 96:
		case 110:
		case 112:
		case 113:
		case 114:
		case 115:
			this.m_d[num] = A_0;
			break;
		case 2:
		case 3:
		case 4:
		case 6:
		case 246:
		{
			OracleNumber oracleNumber = (OracleNumber)A_0;
			num++;
			Buffer.BlockCopy(oracleNumber.a, 0, e, num, 22);
			break;
		}
		case 100:
			num++;
			Buffer.BlockCopy(Devart.Common.e.a((float)A_0), 0, e, num, 4);
			break;
		case 101:
			num++;
			Buffer.BlockCopy(Devart.Common.e.a((double)A_0), 0, e, num, 8);
			break;
		case 12:
		{
			OracleDate oracleDate = (OracleDate)A_0;
			num++;
			e[num] = (byte)oracleDate.Year;
			e[num + 1] = (byte)(oracleDate.Year >> 8);
			e[num + 2] = (byte)oracleDate.Month;
			e[num + 3] = (byte)oracleDate.Day;
			e[num + 4] = (byte)oracleDate.Hour;
			e[num + 5] = (byte)oracleDate.Minute;
			e[num + 6] = (byte)oracleDate.Second;
			break;
		}
		case 187:
		case 188:
		case 232:
		{
			OracleTimeStamp oracleTimeStamp = (OracleTimeStamp)A_0;
			num++;
			e[num] = (byte)oracleTimeStamp.ae;
			e[num + 1] = (byte)(oracleTimeStamp.ae >> 8);
			e[num + 2] = oracleTimeStamp.af;
			e[num + 3] = oracleTimeStamp.ag;
			e[num + 4] = oracleTimeStamp.ah;
			e[num + 5] = oracleTimeStamp.ai;
			e[num + 6] = oracleTimeStamp.aj;
			e[num + 7] = (byte)oracleTimeStamp.ak;
			e[num + 8] = (byte)(oracleTimeStamp.ak >> 8);
			e[num + 9] = (byte)(oracleTimeStamp.ak >> 16);
			e[num + 10] = (byte)(oracleTimeStamp.ak >> 24);
			if (oracleTimeStamp.an >= 0)
			{
				int num2 = OracleTimeStamp.a(oracleTimeStamp.an);
				e[num + 11] = (byte)num2;
				e[num + 12] = (byte)(num2 >> 8);
			}
			else
			{
				e[num + 11] = (byte)oracleTimeStamp.al;
				e[num + 12] = (byte)oracleTimeStamp.am;
			}
			break;
		}
		case 189:
		{
			OracleIntervalYM oracleIntervalYM = (OracleIntervalYM)A_0;
			num++;
			e[num] = (byte)oracleIntervalYM.Years;
			e[num + 1] = (byte)(oracleIntervalYM.Years >> 8);
			e[num + 2] = (byte)oracleIntervalYM.Months;
			break;
		}
		case 190:
		{
			OracleIntervalDS oracleIntervalDS = (OracleIntervalDS)A_0;
			num++;
			e[num] = (byte)oracleIntervalDS.Days;
			e[num + 1] = (byte)oracleIntervalDS.Hours;
			e[num + 2] = (byte)oracleIntervalDS.Minutes;
			e[num + 3] = (byte)oracleIntervalDS.Seconds;
			e[num + 4] = (byte)oracleIntervalDS.Nanoseconds;
			e[num + 5] = (byte)(oracleIntervalDS.Nanoseconds >> 8);
			e[num + 6] = (byte)(oracleIntervalDS.Nanoseconds >> 16);
			e[num + 7] = (byte)(oracleIntervalDS.Nanoseconds >> 24);
			break;
		}
		default:
			throw new InvalidOperationException();
		}
	}

	private object a(OracleAttribute A_0)
	{
		if (IsNull)
		{
			return DBNull.Value;
		}
		int num = A_0.l;
		object obj;
		switch (A_0.b)
		{
		case 108:
			obj = this.m_d[num];
			if (obj == null)
			{
				return DBNull.Value;
			}
			break;
		case 122:
		case 247:
			obj = this.m_d[num];
			if (obj == null)
			{
				return DBNull.Value;
			}
			break;
		case 248:
			obj = this.m_d[num];
			if (obj == null)
			{
				return DBNull.Value;
			}
			break;
		case 1:
		case 5:
		case 9:
		case 95:
		case 96:
		case 110:
		case 112:
		case 113:
		case 114:
		case 115:
			obj = this.m_d[num];
			break;
		case 3:
		case 246:
			if (e[num++] == 0)
			{
				return DBNull.Value;
			}
			obj = OracleNumber.d(OracleNumber.a(e, num));
			break;
		case 2:
		case 4:
		case 6:
			if (e[num++] == 0)
			{
				return DBNull.Value;
			}
			obj = OracleNumber.a(e, num);
			break;
		case 100:
			if (e[num++] == 0)
			{
				return DBNull.Value;
			}
			obj = Devart.Common.e.e(e, num);
			break;
		case 101:
			if (e[num++] == 0)
			{
				return DBNull.Value;
			}
			obj = Devart.Common.e.i(e, num);
			break;
		case 12:
			if (e[num++] == 0)
			{
				return DBNull.Value;
			}
			obj = OracleDate.f(e, num);
			break;
		case 187:
		case 188:
		case 232:
			if (e[num++] == 0)
			{
				return DBNull.Value;
			}
			obj = new OracleTimeStamp(e[num] | (e[num + 1] << 8), e[num + 2], e[num + 3], e[num + 4], e[num + 5], e[num + 6], e[num + 7] | (e[num + 8] << 8) | (e[num + 9] << 16) | (e[num + 10] << 24), e[num + 11], e[num + 12], A_0.DbType);
			if (OracleUtils.OracleClientCompatible)
			{
				obj = new OracleDateTime((OracleTimeStamp)obj);
			}
			break;
		case 189:
			if (e[num++] == 0)
			{
				return DBNull.Value;
			}
			obj = new OracleIntervalYM(e[num] | (e[num + 1] << 8), e[num + 2]);
			if (OracleUtils.OracleClientCompatible)
			{
				obj = new OracleMonthSpan((OracleIntervalYM)obj);
			}
			break;
		case 190:
			if (e[num++] == 0)
			{
				return DBNull.Value;
			}
			obj = new OracleIntervalDS(e[num], e[num + 1], e[num + 2], e[num + 3], e[num + 4] | (e[num + 5] << 8) | (e[num + 6] << 16) | (e[num + 7] << 24));
			if (OracleUtils.OracleClientCompatible)
			{
				obj = new OracleTimeSpan((OracleIntervalDS)obj);
			}
			break;
		case 58:
			if (A_0.g == OracleDbType.Xml)
			{
				obj = this.m_d[num];
				if (obj == null)
				{
					return DBNull.Value;
				}
				break;
			}
			throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
		default:
			throw new InvalidOperationException();
		}
		return obj;
	}

	protected internal void SetObjectIsNull(bool isNull, object obj)
	{
		OracleAttribute oracleAttribute = null;
		for (int num = 0; num < Attributes.Count; num++)
		{
			OracleAttribute oracleAttribute2 = Attributes[num];
			if (this[oracleAttribute2] == obj)
			{
				oracleAttribute = oracleAttribute2;
				break;
			}
		}
		if (oracleAttribute == null)
		{
			throw new InvalidOperationException();
		}
		a(oracleAttribute, isNull);
	}

	public static void ExecuteMethod(ICustomOracleObject obj, OracleConnection connection, string typeName, string methodName, params object[] parameters)
	{
		ExecuteMethod(obj, connection, typeName, methodName, null, parameters);
	}

	public static object ExecuteMethod(ICustomOracleObject obj, OracleConnection connection, string typeName, string methodName, Type resultType, params object[] parameters)
	{
		if (obj == null)
		{
			throw new ArgumentNullException("obj");
		}
		if (connection == null)
		{
			throw new ArgumentNullException("connection");
		}
		if (typeName == null)
		{
			throw new ArgumentNullException("typeName");
		}
		if (methodName == null)
		{
			throw new ArgumentNullException("methodName");
		}
		connection.e();
		using OracleCommand oracleCommand = new OracleCommand(typeName + '.' + methodName, connection);
		oracleCommand.CommandType = CommandType.StoredProcedure;
		OracleParameter oracleParameter = new OracleParameter("SELF", OracleDbType.Object, obj, ParameterDirection.InputOutput);
		oracleParameter.ObjectTypeName = typeName;
		oracleCommand.Parameters.Add(oracleParameter);
		if (parameters != null)
		{
			int num = parameters.Length;
			for (int num2 = 0; num2 < num; num2++)
			{
				oracleParameter = oracleCommand.Parameters.Add("p" + num2, parameters[num2]);
				oracleParameter.Direction = ParameterDirection.InputOutput;
			}
		}
		OracleParameter oracleParameter2;
		if ((object)resultType != null)
		{
			oracleParameter2 = new OracleParameter("RESULT", OracleUtils.TypeToOracleDbType(resultType));
			oracleParameter2.Direction = ParameterDirection.ReturnValue;
			oracleCommand.Parameters.Insert(0, oracleParameter2);
		}
		else
		{
			oracleParameter2 = null;
		}
		oracleCommand.ExecuteNonQuery();
		if (parameters != null)
		{
			int num3 = ((oracleParameter2 == null) ? 1 : 2);
			int num4 = parameters.Length;
			for (int num5 = 0; num5 < num4; num5++)
			{
				oracleParameter = oracleCommand.Parameters[num3 + num5];
				if (oracleParameter.Direction != ParameterDirection.Input)
				{
					parameters[num5] = oracleParameter.Value;
				}
			}
		}
		return oracleParameter2?.Value;
	}

	public static object ExecuteMethod(ICustomOracleObject obj, OracleConnection connection, string typeName, string methodName, OracleParameterCollection parameters)
	{
		return ExecuteMethod(obj, connection, typeName, methodName, parameters, parameterCheck: false);
	}

	public static object ExecuteMethod(ICustomOracleObject obj, OracleConnection connection, string typeName, string methodName, OracleParameterCollection parameters, bool parameterCheck)
	{
		if (obj == null)
		{
			throw new ArgumentNullException("obj");
		}
		if (connection == null)
		{
			throw new ArgumentNullException("connection");
		}
		if (typeName == null)
		{
			throw new ArgumentNullException("typeName");
		}
		if (methodName == null)
		{
			throw new ArgumentNullException("methodName");
		}
		connection.e();
		using OracleCommand oracleCommand = new OracleCommand(typeName + '.' + methodName, connection);
		oracleCommand.CommandType = CommandType.StoredProcedure;
		oracleCommand.ParameterCheck = parameterCheck;
		oracleCommand.a(parameters);
		OracleParameter oracleParameter = null;
		if (oracleCommand.Parameters.Contains("result"))
		{
			oracleParameter = oracleCommand.Parameters["result"];
		}
		int index = ((oracleParameter != null) ? 1 : 0);
		OracleParameter oracleParameter2 = new OracleParameter("SELF", OracleDbType.Object, obj, ParameterDirection.InputOutput);
		oracleParameter2.ObjectTypeName = typeName;
		oracleCommand.Parameters.Insert(index, oracleParameter2);
		oracleCommand.ExecuteNonQuery();
		return oracleParameter?.Value;
	}

	public void ExecuteMethod(OracleConnection connection, string name, params object[] parameters)
	{
		ExecuteMethod(connection, name, null, parameters);
	}

	public object ExecuteMethod(OracleConnection connection, string name, Type resultType, params object[] parameters)
	{
		return ExecuteMethod(this, connection, this.a.Name, name, resultType, parameters);
	}

	public object ExecuteMethod(OracleConnection connection, string name, OracleParameterCollection parameters)
	{
		return ExecuteMethod(connection, name, parameters, parameterCheck: false);
	}

	public object ExecuteMethod(OracleConnection connection, string name, OracleParameterCollection parameters, bool parameterCheck)
	{
		return ExecuteMethod(this, connection, this.a.Name, name, parameters, parameterCheck);
	}

	public object Clone()
	{
		OracleObject oracleObject = new OracleObject(this.a);
		oracleObject.m_c = this.m_c;
		if (!this.m_c)
		{
			for (int num = 0; num < this.m_d.Length; num++)
			{
				object obj = this.m_d[num];
				if (obj is ICloneable cloneable)
				{
					object obj2 = cloneable.Clone();
					oracleObject.m_d[num] = obj2;
					if (obj2 is OracleObject oracleObject2)
					{
						oracleObject2.b = oracleObject;
					}
					if (obj2 is OracleArray oracleArray)
					{
						oracleArray.a = oracleObject;
					}
				}
				else
				{
					oracleObject.m_d[num] = obj;
				}
			}
			Buffer.BlockCopy(e, 0, oracleObject.e, 0, e.Length);
		}
		return oracleObject;
	}

	void bf.SetAttributeNotNull(OracleAttribute attribute)
	{
		if (attribute.DbType == OracleDbType.Object && attribute.ObjectType.i)
		{
			this[attribute] = new OracleObject(attribute.ObjectType);
			return;
		}
		throw new InvalidOperationException();
	}
}
