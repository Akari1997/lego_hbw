using System;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleUtils
{
	internal const string a = "This class is designed for compatibility with OracleClient only.";

	internal const string b = "This method is designed for compatibility with OracleClient only.";

	internal const string c = "This property is designed for compatibility with OracleClient only.";

	public const string GettingServerListMessage = "Scanning the network...";

	private static ArrayList d;

	public static bool OracleClientCompatible;

	private static bool e;

	private static bool f;

	private static ArrayList g;

	private static bool h;

	public static bool UseSeparateOCIEnvironment
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

	public static bool UseOCINumberFormating
	{
		get
		{
			return f;
		}
		set
		{
			f = value;
		}
	}

	public static bool DataSourcesObtained => h;

	static OracleUtils()
	{
		d = new ArrayList();
		OracleClientCompatible = false;
		e = true;
		f = false;
		g = new ArrayList();
		h = false;
		d.Add("ACCESS");
		d.Add("ADD");
		d.Add("ALL");
		d.Add("ALTER");
		d.Add("AND");
		d.Add("ANY");
		d.Add("AS");
		d.Add("ASC");
		d.Add("AUDIT");
		d.Add("BETWEEN");
		d.Add("BY");
		d.Add("CHAR");
		d.Add("CHECK");
		d.Add("CLUSTER");
		d.Add("COLUMN");
		d.Add("COMMENT");
		d.Add("COMPRESS");
		d.Add("CONNECT");
		d.Add("CREATE");
		d.Add("CURRENT");
		d.Add("DATE");
		d.Add("DECIMAL");
		d.Add("DEFAULT");
		d.Add("DELETE");
		d.Add("DESC");
		d.Add("DISTINCT");
		d.Add("DROP");
		d.Add("ELSE");
		d.Add("EXCLUSIVE");
		d.Add("EXISTS");
		d.Add("FILE");
		d.Add("FLOAT");
		d.Add("FOR");
		d.Add("FROM");
		d.Add("GRANT");
		d.Add("GROUP");
		d.Add("HAVING");
		d.Add("IDENTIFIED");
		d.Add("IMMEDIATE");
		d.Add("IN");
		d.Add("INCREMENT");
		d.Add("INDEX");
		d.Add("INITIAL");
		d.Add("INSERT");
		d.Add("INTEGER");
		d.Add("INTERSECT");
		d.Add("INTO");
		d.Add("IS");
		d.Add("LEVEL");
		d.Add("LIKE");
		d.Add("LOCK");
		d.Add("LONG");
		d.Add("MAXEXTENTS");
		d.Add("MINUS");
		d.Add("MLSLABEL");
		d.Add("MODE");
		d.Add("MODIFY");
		d.Add("NOAUDIT");
		d.Add("NOCOMPRESS");
		d.Add("NOT");
		d.Add("NOWAIT");
		d.Add("NULL");
		d.Add("NUMBER");
		d.Add("OF");
		d.Add("OFFLINE");
		d.Add("ON");
		d.Add("ONLINE");
		d.Add("OPTION");
		d.Add("OR");
		d.Add("ORDER");
		d.Add("PCTFREE");
		d.Add("PRIOR");
		d.Add("PRIVILEGES");
		d.Add("PUBLIC");
		d.Add("RAW");
		d.Add("RENAME");
		d.Add("RESOURCE");
		d.Add("REVOKE");
		d.Add("ROW");
		d.Add("ROWID");
		d.Add("ROWNUM");
		d.Add("ROWS");
		d.Add("SELECT");
		d.Add("SESSION");
		d.Add("SET");
		d.Add("SHARE");
		d.Add("SIZE");
		d.Add("SMALLINT");
		d.Add("START");
		d.Add("SUCCESSFUL");
		d.Add("SYNONYM");
		d.Add("SYSDATE");
		d.Add("TABLE");
		d.Add("THEN");
		d.Add("TO");
		d.Add("TRIGGER");
		d.Add("UID");
		d.Add("UNION");
		d.Add("UNIQUE");
		d.Add("UPDATE");
		d.Add("USER");
		d.Add("VALIDATE");
		d.Add("VALUES");
		d.Add("VARCHAR");
		d.Add("VARCHAR2");
		d.Add("VIEW");
		d.Add("WHENEVER");
		d.Add("WHERE");
		d.Add("WITH");
	}

	private OracleUtils()
	{
	}

	internal static TypeCode c(Type A_0)
	{
		return Type.GetTypeCode(A_0);
	}

	public static string OracleValueToString(object value)
	{
		if (Utils.IsNull(value))
		{
			return string.Empty;
		}
		return Convert.ToString(value);
	}

	public static object ObjectToOracleValue(object value, OracleDbType dbType)
	{
		return a(value, dbType, A_2: true);
	}

	internal static object c(OracleDbType A_0)
	{
		switch (A_0)
		{
		case OracleDbType.BFile:
			return OracleBFile.Null;
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
			return b(A_0);
		case OracleDbType.Date:
			return OracleDate.Null;
		case OracleDbType.IntervalDS:
			if (OracleClientCompatible)
			{
				return OracleTimeSpan.Null;
			}
			return OracleIntervalDS.Null;
		case OracleDbType.IntervalYM:
			if (OracleClientCompatible)
			{
				return OracleMonthSpan.Null;
			}
			return OracleIntervalYM.Null;
		case OracleDbType.Char:
		case OracleDbType.Long:
		case OracleDbType.NChar:
		case OracleDbType.NVarChar:
		case OracleDbType.RowId:
		case OracleDbType.VarChar:
			return OracleString.Null;
		case OracleDbType.LongRaw:
		case OracleDbType.Raw:
			return OracleBinary.Null;
		case OracleDbType.Double:
		case OracleDbType.Float:
		case OracleDbType.Integer:
		case OracleDbType.Number:
			return OracleNumber.Null;
		case OracleDbType.TimeStamp:
		case OracleDbType.TimeStampLTZ:
		case OracleDbType.TimeStampTZ:
			if (OracleClientCompatible)
			{
				return OracleDateTime.Null;
			}
			return OracleTimeStamp.Null;
		case OracleDbType.Xml:
			return OracleXml.Null;
		case OracleDbType.AnyData:
			return OracleAnyData.Null;
		default:
			return null;
		}
	}

	internal static object a(object A_0, OracleDbType A_1, bool A_2)
	{
		if (A_0 == null || A_0 == DBNull.Value || (A_0 is INullable && ((INullable)A_0).IsNull))
		{
			if (!A_2)
			{
				Type type = a(A_1, A_1: false);
				if ((object)type == typeof(OracleCursor) || (object)type == typeof(OracleArray) || (object)type == typeof(OracleObject) || (object)type == typeof(OracleRef) || (object)type == typeof(OracleTable))
				{
					return A_0;
				}
				if ((object)type == typeof(OracleBFile))
				{
					return OracleBFile.Null;
				}
				if ((object)type == typeof(OracleBinary))
				{
					return OracleBinary.Null;
				}
				if ((object)type == typeof(OracleDate))
				{
					return OracleDate.Null;
				}
				if ((object)type == typeof(OracleIntervalDS))
				{
					if (OracleClientCompatible)
					{
						return OracleTimeSpan.Null;
					}
					return OracleIntervalDS.Null;
				}
				if ((object)type == typeof(OracleIntervalYM))
				{
					if (OracleClientCompatible)
					{
						return OracleMonthSpan.Null;
					}
					return OracleIntervalYM.Null;
				}
				if ((object)type == typeof(OracleLob))
				{
					return b(A_1);
				}
				if ((object)type == typeof(OracleNumber))
				{
					return OracleNumber.Null;
				}
				if ((object)type == typeof(OracleString))
				{
					return OracleString.Null;
				}
				if ((object)type == typeof(OracleTimeStamp))
				{
					if (OracleClientCompatible)
					{
						return OracleDateTime.Null;
					}
					return OracleTimeStamp.Null;
				}
				return A_0;
			}
			return A_0;
		}
		if (A_0.GetType().IsArray)
		{
			if (A_0 is char[] && (A_1 == OracleDbType.VarChar || A_1 == OracleDbType.Char || A_1 == OracleDbType.NChar || A_1 == OracleDbType.NVarChar || A_1 == OracleDbType.RowId || A_1 == OracleDbType.Long || A_1 == OracleDbType.Clob || A_1 == OracleDbType.NClob))
			{
				return A_0;
			}
			if (A_0 is byte[])
			{
				switch (A_1)
				{
				case OracleDbType.LongRaw:
				case OracleDbType.Raw:
					if (A_2)
					{
						return A_0;
					}
					return new OracleBinary((byte[])A_0);
				case OracleDbType.BFile:
				case OracleDbType.Blob:
					return A_0;
				default:
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
			}
			Type elementType = A_0.GetType().GetElementType();
			Type type2 = a(A_1, A_2);
			if ((object)elementType == type2)
			{
				return A_0;
			}
			throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
		}
		if (b(A_0.GetType()) && A_2)
		{
			Type type3 = a(A_1, A_1: true);
			if (A_0 is TimeSpan)
			{
				if ((object)type3 == typeof(TimeSpan))
				{
					return A_0;
				}
				if ((object)type3 == typeof(string))
				{
					return A_0.ToString();
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if ((object)type3 == typeof(TimeSpan))
			{
				if (A_0 is string)
				{
					return TimeSpan.Parse(A_0 as string);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if ((object)type3 == typeof(bool))
			{
				if (A_0 is bool)
				{
					return A_0;
				}
				if (A_0 is string)
				{
					return bool.Parse((string)A_0);
				}
				if (A_0 is int)
				{
					return (int)A_0 != 0;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			return Convert.ChangeType(A_0, type3, null);
		}
		if (!b(A_0.GetType()) && !A_2)
		{
			Type type4 = a(A_1, A_2);
			if (A_0 is OracleBFile)
			{
				if ((object)type4 == typeof(OracleBinary))
				{
					return new OracleBinary((byte[])((OracleLob)A_0).Value);
				}
				if ((object)type4 == typeof(OracleBFile))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleLob))
				{
					return (OracleLob)A_0;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleBinary)
			{
				if ((object)type4 == typeof(OracleBinary))
				{
					return A_0;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleDate)
			{
				if ((object)type4 == typeof(OracleDate))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleTimeStamp))
				{
					return ((OracleDate)A_0).ToOracleTimeStamp();
				}
				if ((object)type4 == typeof(OracleString))
				{
					return new OracleString(((OracleDate)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleIntervalDS)
			{
				if ((object)type4 == typeof(OracleIntervalDS))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleString))
				{
					return new OracleString(((OracleIntervalDS)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleTimeSpan)
			{
				if ((object)type4 == typeof(OracleTimeSpan))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleString))
				{
					return new OracleString(((OracleTimeSpan)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleMonthSpan)
			{
				if ((object)type4 == typeof(OracleMonthSpan))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleString))
				{
					return new OracleString(((OracleMonthSpan)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleIntervalYM)
			{
				if ((object)type4 == typeof(OracleIntervalYM))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleString))
				{
					return new OracleString(((OracleIntervalYM)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleLob)
			{
				OracleDbType lobType = ((OracleLob)A_0).LobType;
				if ((object)type4 == typeof(OracleLob))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleString))
				{
					if (lobType == OracleDbType.Clob || lobType == OracleDbType.NClob)
					{
						return new OracleString((string)((OracleLob)A_0).Value);
					}
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				if ((object)type4 == typeof(OracleBinary))
				{
					if (lobType == OracleDbType.Blob)
					{
						return new OracleBinary((byte[])((OracleLob)A_0).Value);
					}
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleNumber)
			{
				if ((object)type4 == typeof(OracleNumber))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleString))
				{
					return new OracleString(((OracleNumber)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleDateTime)
			{
				if ((object)type4 == typeof(OracleDateTime))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleString))
				{
					return new OracleString(((OracleDateTime)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleTimeStamp)
			{
				if ((object)type4 == typeof(OracleTimeStamp))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleString))
				{
					return new OracleString(((OracleTimeStamp)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleString)
			{
				if ((object)type4 == typeof(OracleString))
				{
					return A_0;
				}
				if ((object)type4 == typeof(OracleDate))
				{
					return OracleDate.Parse(((OracleString)A_0/*cast due to constrained. prefix*/).ToString());
				}
				if ((object)type4 == typeof(OracleTimeSpan))
				{
					return OracleTimeSpan.Parse(((OracleString)A_0/*cast due to constrained. prefix*/).ToString());
				}
				if ((object)type4 == typeof(OracleMonthSpan))
				{
					return OracleMonthSpan.Parse(((OracleString)A_0/*cast due to constrained. prefix*/).ToString());
				}
				if ((object)type4 == typeof(OracleDateTime))
				{
					return OracleDateTime.Parse(((OracleString)A_0/*cast due to constrained. prefix*/).ToString());
				}
				if ((object)type4 == typeof(OracleIntervalDS))
				{
					return OracleIntervalDS.Parse(((OracleString)A_0/*cast due to constrained. prefix*/).ToString());
				}
				if ((object)type4 == typeof(OracleIntervalYM))
				{
					return OracleIntervalYM.Parse(((OracleString)A_0/*cast due to constrained. prefix*/).ToString());
				}
				if ((object)type4 == typeof(OracleNumber))
				{
					return OracleNumber.Parse(((OracleString)A_0/*cast due to constrained. prefix*/).ToString());
				}
				if ((object)type4 == typeof(OracleTimeStamp))
				{
					return OracleTimeStamp.Parse(((OracleString)A_0/*cast due to constrained. prefix*/).ToString());
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleCursor)
			{
				if ((object)type4 == typeof(OracleCursor))
				{
					return A_0;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleTable)
			{
				if ((object)type4 == typeof(OracleTable))
				{
					return A_0;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is NativeOracleTable)
			{
				if ((object)type4 == typeof(OracleTable))
				{
					NativeOracleTable nativeOracleTable = A_0 as NativeOracleTable;
					OracleTable oracleTable = new OracleTable(nativeOracleTable.ObjectType);
					((ICustomOracleArray)oracleTable).FromOracleArray((NativeOracleArray)nativeOracleTable);
					return oracleTable;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is ICustomOracleArray)
			{
				if ((object)type4 == typeof(OracleArray))
				{
					return A_0;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is NativeOracleArray)
			{
				if ((object)type4 == typeof(OracleArray))
				{
					NativeOracleArray nativeOracleArray = A_0 as NativeOracleArray;
					OracleArray oracleArray = new OracleArray(nativeOracleArray.ObjectType);
					((ICustomOracleArray)oracleArray).FromOracleArray(nativeOracleArray);
					return oracleArray;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is ICustomOracleObject)
			{
				if ((object)type4 == typeof(OracleObject))
				{
					return A_0;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is NativeOracleObject)
			{
				if ((object)type4 == typeof(OracleObject))
				{
					NativeOracleObject nativeOracleObject = A_0 as NativeOracleObject;
					OracleObject oracleObject = new OracleObject(nativeOracleObject.ObjectType);
					((ICustomOracleObject)oracleObject).FromOracleObject(nativeOracleObject);
					return oracleObject;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleRef)
			{
				if ((object)type4 == typeof(OracleRef))
				{
					return A_0;
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if ((object)A_0.GetType() == type4)
			{
				return A_0;
			}
			throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
		}
		if (!b(A_0.GetType()) && A_2)
		{
			Type type5 = a(A_1, A_2);
			if (A_0 is OracleBFile)
			{
				return ((OracleBFile)A_0).Value;
			}
			if (A_0 is OracleBinary oracleBinary)
			{
				return oracleBinary.Value;
			}
			if (A_0 is OracleDate oracleDate)
			{
				return oracleDate.Value;
			}
			if (A_0 is OracleTimeSpan oracleTimeSpan)
			{
				return oracleTimeSpan.Value;
			}
			if (A_0 is OracleMonthSpan oracleMonthSpan)
			{
				return oracleMonthSpan.Value;
			}
			if (A_0 is OracleIntervalDS oracleIntervalDS)
			{
				return oracleIntervalDS.Value;
			}
			if (A_0 is OracleIntervalYM oracleIntervalYM)
			{
				return oracleIntervalYM.Value;
			}
			if (A_0 is OracleLob)
			{
				return ((OracleLob)A_0).Value;
			}
			if (A_0 is OracleXml)
			{
				return ((OracleXml)A_0).ToString();
			}
			if (A_0 is OracleNumber)
			{
				if ((object)type5 == typeof(decimal))
				{
					return (decimal)(OracleNumber)A_0;
				}
				if ((object)type5 == typeof(int))
				{
					return OracleNumber.d((OracleNumber)A_0);
				}
				if ((object)type5 == typeof(double))
				{
					return OracleNumber.b((OracleNumber)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			if (A_0 is OracleString oracleString)
			{
				return oracleString.Value;
			}
			if (A_0 is OracleDateTime oracleDateTime)
			{
				return oracleDateTime.Value;
			}
			if (A_0 is OracleTimeStamp oracleTimeStamp)
			{
				return oracleTimeStamp.Value;
			}
			if (A_0 is OracleCursor || A_0 is OracleArray || A_0 is OracleObject || A_0 is OracleRef || A_0 is OracleTable)
			{
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
		}
		if (b(A_0.GetType()) && !A_2)
		{
			Type type6 = a(A_1, A_2);
			switch (c(A_0.GetType()))
			{
			case TypeCode.Boolean:
				if ((object)type6 == typeof(int))
				{
					return ((bool)A_0) ? 1 : 0;
				}
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Char:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.SByte:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((int)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Byte:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((int)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Int16:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((short)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.UInt16:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((ushort)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Int32:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((int)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.UInt32:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((long)A_0);
				}
				if ((object)type6 == typeof(OracleMonthSpan))
				{
					return new OracleMonthSpan((int)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Int64:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((long)A_0);
				}
				if ((object)type6 == typeof(OracleIntervalYM))
				{
					return new OracleIntervalYM((long)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.UInt64:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((decimal)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Single:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((float)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Double:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((double)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Decimal:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return new OracleNumber((decimal)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.DateTime:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString(Convert.ToString(A_0));
				}
				if ((object)type6 == typeof(OracleDate))
				{
					return new OracleDate((DateTime)A_0);
				}
				if ((object)type6 == typeof(OracleTimeStamp))
				{
					return new OracleTimeStamp((DateTime)A_0);
				}
				if ((object)type6 == typeof(OracleDateTime))
				{
					return new OracleDateTime((DateTime)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.String:
				if ((object)type6 == typeof(OracleString))
				{
					return new OracleString((string)A_0);
				}
				if ((object)type6 == typeof(OracleDate))
				{
					return OracleDate.Parse((string)A_0);
				}
				if ((object)type6 == typeof(OracleTimeSpan))
				{
					return OracleTimeSpan.Parse((string)A_0);
				}
				if ((object)type6 == typeof(OracleMonthSpan))
				{
					return OracleMonthSpan.Parse((string)A_0);
				}
				if ((object)type6 == typeof(OracleDateTime))
				{
					return OracleDateTime.Parse((string)A_0);
				}
				if ((object)type6 == typeof(OracleIntervalDS))
				{
					return OracleIntervalDS.Parse((string)A_0);
				}
				if ((object)type6 == typeof(OracleIntervalYM))
				{
					return OracleIntervalYM.Parse((string)A_0);
				}
				if ((object)type6 == typeof(OracleNumber))
				{
					return OracleNumber.Parse((string)A_0);
				}
				if ((object)type6 == typeof(OracleTimeStamp))
				{
					return OracleTimeStamp.Parse((string)A_0);
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			case TypeCode.Object:
				if (A_0 is TimeSpan)
				{
					if ((object)type6 == typeof(OracleString))
					{
						return new OracleString((string)A_0);
					}
					if ((object)type6 == typeof(OracleIntervalDS))
					{
						return new OracleIntervalDS((TimeSpan)A_0);
					}
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
		}
		return null;
	}

	internal static OracleLob b(OracleDbType A_0)
	{
		switch (A_0)
		{
		case OracleDbType.Blob:
			if (OracleClientCompatible)
			{
				return OracleLob.Null;
			}
			return OracleLob.NullBlob;
		case OracleDbType.Clob:
			if (OracleClientCompatible)
			{
				return OracleLob.Null;
			}
			return OracleLob.NullClob;
		case OracleDbType.NClob:
			if (OracleClientCompatible)
			{
				return OracleLob.Null;
			}
			return OracleLob.NullNClob;
		default:
			throw new InvalidOperationException();
		}
	}

	public static Type OracleDbTypeToType(int oraDbType)
	{
		return OracleDbTypeToType((OracleDbType)oraDbType);
	}

	public static Type OracleDbTypeToType(OracleDbType oraDbType)
	{
		return a(oraDbType, A_1: true);
	}

	internal static Type a(OracleDbType A_0, bool A_1)
	{
		switch (A_0)
		{
		case OracleDbType.Char:
		case OracleDbType.Long:
		case OracleDbType.NChar:
		case OracleDbType.NVarChar:
		case OracleDbType.RowId:
		case OracleDbType.VarChar:
			if (A_1)
			{
				return typeof(string);
			}
			return typeof(OracleString);
		case OracleDbType.Integer:
			if (A_1)
			{
				return typeof(int);
			}
			return typeof(OracleNumber);
		case OracleDbType.Double:
			if (A_1)
			{
				return typeof(double);
			}
			return typeof(OracleNumber);
		case OracleDbType.Float:
			if (A_1)
			{
				return typeof(double);
			}
			return typeof(OracleNumber);
		case OracleDbType.Number:
			if (A_1)
			{
				return typeof(decimal);
			}
			return typeof(OracleNumber);
		case OracleDbType.Date:
			if (A_1)
			{
				return typeof(DateTime);
			}
			return typeof(OracleDate);
		case OracleDbType.TimeStamp:
		case OracleDbType.TimeStampLTZ:
		case OracleDbType.TimeStampTZ:
			if (A_1)
			{
				return typeof(DateTime);
			}
			if (OracleClientCompatible)
			{
				return typeof(OracleDateTime);
			}
			return typeof(OracleTimeStamp);
		case OracleDbType.IntervalDS:
			if (A_1)
			{
				return typeof(TimeSpan);
			}
			if (OracleClientCompatible)
			{
				return typeof(OracleTimeSpan);
			}
			return typeof(OracleIntervalDS);
		case OracleDbType.IntervalYM:
			if (A_1)
			{
				return typeof(int);
			}
			if (OracleClientCompatible)
			{
				return typeof(OracleMonthSpan);
			}
			return typeof(OracleIntervalYM);
		case OracleDbType.Boolean:
			return typeof(bool);
		case OracleDbType.LongRaw:
		case OracleDbType.Raw:
			if (A_1)
			{
				return typeof(byte[]);
			}
			return typeof(OracleBinary);
		case OracleDbType.Clob:
		case OracleDbType.NClob:
			if (A_1)
			{
				return typeof(string);
			}
			return typeof(OracleLob);
		case OracleDbType.Blob:
			if (A_1)
			{
				return typeof(byte[]);
			}
			return typeof(OracleLob);
		case OracleDbType.BFile:
			if (A_1)
			{
				return typeof(byte[]);
			}
			return typeof(OracleBFile);
		case OracleDbType.Cursor:
			return typeof(OracleCursor);
		case OracleDbType.Object:
			return typeof(OracleObject);
		case OracleDbType.Array:
			return typeof(OracleArray);
		case OracleDbType.Table:
			return typeof(OracleTable);
		case OracleDbType.Ref:
			if (A_1)
			{
				return typeof(string);
			}
			return typeof(OracleRef);
		case OracleDbType.Xml:
			if (A_1)
			{
				return typeof(string);
			}
			return typeof(OracleXml);
		case OracleDbType.AnyData:
			return typeof(OracleAnyData);
		default:
			throw new NotSupportedException();
		}
	}

	public static OracleDbType TypeToOracleDbType(Type type)
	{
		if (!a(type, out var A_))
		{
			throw new NotSupportedException();
		}
		return A_;
	}

	internal static bool a(Type A_0, out OracleDbType A_1)
	{
		switch (c(A_0))
		{
		case TypeCode.DBNull:
		case TypeCode.Char:
		case TypeCode.String:
			A_1 = OracleDbType.VarChar;
			return true;
		case TypeCode.SByte:
		case TypeCode.Byte:
		case TypeCode.Int16:
		case TypeCode.UInt16:
		case TypeCode.Int32:
		case TypeCode.UInt32:
			A_1 = OracleDbType.Integer;
			return true;
		case TypeCode.Int64:
		case TypeCode.UInt64:
			A_1 = OracleDbType.Number;
			return true;
		case TypeCode.Single:
			A_1 = OracleDbType.Float;
			return true;
		case TypeCode.Double:
			A_1 = OracleDbType.Double;
			return true;
		case TypeCode.Decimal:
			A_1 = OracleDbType.Number;
			return true;
		case TypeCode.DateTime:
			A_1 = OracleDbType.Date;
			return true;
		case TypeCode.Object:
			if (A_0.IsArray)
			{
				switch (c(A_0.GetElementType()))
				{
				case TypeCode.Byte:
					A_1 = OracleDbType.Blob;
					return true;
				case TypeCode.Char:
					A_1 = OracleDbType.VarChar;
					return true;
				default:
					throw new ArgumentException(string.Format(Devart.Common.al.a("ArrayTypeCodeNotSuppotred"), A_0.ToString()));
				}
			}
			if ((object)A_0 == typeof(TimeSpan))
			{
				A_1 = OracleDbType.IntervalDS;
			}
			else if ((object)A_0 == typeof(OracleBFile))
			{
				A_1 = OracleDbType.BFile;
			}
			else if ((object)A_0 == typeof(OracleDate))
			{
				A_1 = OracleDbType.Date;
			}
			else if ((object)A_0 == typeof(OracleNumber))
			{
				A_1 = OracleDbType.Number;
			}
			else if ((object)A_0 == typeof(OracleIntervalDS) || ((object)A_0 == typeof(OracleTimeSpan) && OracleClientCompatible))
			{
				A_1 = OracleDbType.IntervalDS;
			}
			else if ((object)A_0 == typeof(OracleIntervalYM) || ((object)A_0 == typeof(OracleMonthSpan) && OracleClientCompatible))
			{
				A_1 = OracleDbType.IntervalYM;
			}
			else if ((object)A_0 == typeof(OracleTimeStamp) || ((object)A_0 == typeof(OracleDateTime) && OracleClientCompatible))
			{
				A_1 = OracleDbType.TimeStamp;
			}
			else if ((object)A_0 == typeof(OracleCursor))
			{
				A_1 = OracleDbType.Cursor;
			}
			else if ((object)A_0 == typeof(OracleXml))
			{
				A_1 = OracleDbType.Xml;
			}
			else if ((object)A_0 == typeof(OracleAnyData))
			{
				A_1 = OracleDbType.AnyData;
			}
			else if ((object)A_0 == typeof(OracleObject) || (object)A_0 == typeof(NativeOracleObject))
			{
				A_1 = OracleDbType.Object;
			}
			else if ((object)A_0 == typeof(OracleArray) || (object)A_0 == typeof(NativeOracleArray))
			{
				A_1 = OracleDbType.Array;
			}
			else if ((object)A_0 == typeof(OracleTable) || (object)A_0 == typeof(NativeOracleTable))
			{
				A_1 = OracleDbType.Table;
			}
			else if ((object)A_0 == typeof(OracleBinary))
			{
				A_1 = OracleDbType.Raw;
			}
			else
			{
				if ((object)A_0 != typeof(OracleString))
				{
					A_1 = OracleDbType.VarChar;
					return false;
				}
				A_1 = OracleDbType.VarChar;
			}
			return true;
		default:
			A_1 = OracleDbType.VarChar;
			return false;
		}
	}

	internal static OracleDbType a(object A_0)
	{
		if (Utils.IsNull(A_0))
		{
			return OracleDbType.VarChar;
		}
		Type type = A_0.GetType();
		switch (c(type))
		{
		case TypeCode.DBNull:
		case TypeCode.Char:
			return OracleDbType.VarChar;
		case TypeCode.String:
			if (((string)A_0).Length <= 4000)
			{
				return OracleDbType.VarChar;
			}
			return OracleDbType.Clob;
		case TypeCode.SByte:
		case TypeCode.Byte:
		case TypeCode.Int16:
		case TypeCode.UInt16:
		case TypeCode.Int32:
		case TypeCode.UInt32:
			return OracleDbType.Integer;
		case TypeCode.Boolean:
			return OracleDbType.Boolean;
		case TypeCode.Int64:
		case TypeCode.UInt64:
		case TypeCode.Decimal:
			return OracleDbType.Number;
		case TypeCode.Single:
			return OracleDbType.Float;
		case TypeCode.Double:
			return OracleDbType.Double;
		case TypeCode.DateTime:
			return OracleDbType.Date;
		case TypeCode.Object:
			if (type.IsArray)
			{
				switch (c(type.GetElementType()))
				{
				case TypeCode.Byte:
					return OracleDbType.Blob;
				case TypeCode.Char:
					return OracleDbType.VarChar;
				default:
					if (((Array)A_0).Length != 0)
					{
						return a(((Array)A_0).GetValue(0));
					}
					return TypeToOracleDbType(type.GetElementType());
				}
			}
			if (A_0 is TimeSpan)
			{
				return OracleDbType.IntervalDS;
			}
			if (A_0 is OracleBFile)
			{
				return OracleDbType.BFile;
			}
			if (A_0 is OracleBinary)
			{
				return OracleDbType.Raw;
			}
			if (A_0 is OracleDate)
			{
				return OracleDbType.Date;
			}
			if (A_0 is OracleNumber)
			{
				return OracleDbType.Number;
			}
			if (A_0 is OracleIntervalDS || (A_0 is OracleTimeSpan && OracleClientCompatible))
			{
				return OracleDbType.IntervalDS;
			}
			if (A_0 is OracleIntervalYM || (A_0 is OracleMonthSpan && OracleClientCompatible))
			{
				return OracleDbType.IntervalYM;
			}
			if (A_0 is OracleLob)
			{
				return ((OracleLob)A_0).LobType;
			}
			if (A_0 is OracleDateTime && OracleClientCompatible)
			{
				return OracleDbType.TimeStamp;
			}
			if (A_0 is OracleTimeStamp oracleTimeStamp)
			{
				return oracleTimeStamp.TimeStampType;
			}
			if (A_0 is OracleTable || A_0 is NativeOracleTable)
			{
				return OracleDbType.Table;
			}
			if (A_0 is OracleArray || A_0 is NativeOracleArray)
			{
				return OracleDbType.Array;
			}
			if (A_0 is OracleObject || A_0 is NativeOracleObject)
			{
				return OracleDbType.Object;
			}
			if (A_0 is OracleRef)
			{
				return OracleDbType.Ref;
			}
			if (A_0 is OracleXml)
			{
				return OracleDbType.Xml;
			}
			if (A_0 is OracleAnyData)
			{
				return OracleDbType.AnyData;
			}
			if (A_0 is OracleString)
			{
				return OracleDbType.VarChar;
			}
			if (A_0 is OracleCursor)
			{
				return OracleDbType.Cursor;
			}
			throw new ArgumentException(string.Format(Devart.Common.al.a("ValueTypeNotSuppotred"), type.ToString()));
		default:
			throw new ArgumentException(string.Format(Devart.Common.al.a("ValueTypeNotSuppotred"), type.ToString()));
		}
	}

	public static DbType OracleDbTypeToDbType(int oraDbType)
	{
		return OracleDbTypeToDbType((OracleDbType)oraDbType);
	}

	public static DbType OracleDbTypeToDbType(OracleDbType oraDbType)
	{
		switch (oraDbType)
		{
		case OracleDbType.Char:
		case OracleDbType.NChar:
		case OracleDbType.NClob:
		case OracleDbType.NVarChar:
		case OracleDbType.RowId:
		case OracleDbType.VarChar:
		case OracleDbType.Xml:
			return DbType.String;
		case OracleDbType.Integer:
			return DbType.Int32;
		case OracleDbType.Float:
			return DbType.Single;
		case OracleDbType.Double:
			return DbType.Double;
		case OracleDbType.Number:
			return DbType.Decimal;
		case OracleDbType.Date:
		case OracleDbType.TimeStamp:
		case OracleDbType.TimeStampLTZ:
		case OracleDbType.TimeStampTZ:
			return DbType.DateTime;
		case OracleDbType.IntervalDS:
			return DbType.Time;
		case OracleDbType.IntervalYM:
			return DbType.Int32;
		case OracleDbType.Clob:
		case OracleDbType.Long:
			return DbType.String;
		case OracleDbType.BFile:
		case OracleDbType.Blob:
		case OracleDbType.LongRaw:
		case OracleDbType.Raw:
			return DbType.Binary;
		case OracleDbType.Array:
		case OracleDbType.Cursor:
		case OracleDbType.Object:
		case OracleDbType.Ref:
		case OracleDbType.Table:
			return DbType.Object;
		case OracleDbType.Boolean:
			return DbType.Boolean;
		default:
			throw new NotSupportedException();
		}
	}

	internal static OracleDbType a(DbType A_0)
	{
		switch (A_0)
		{
		case DbType.AnsiString:
		case DbType.String:
			return OracleDbType.VarChar;
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return OracleDbType.Char;
		case DbType.Byte:
		case DbType.Int16:
		case DbType.Int32:
		case DbType.SByte:
		case DbType.UInt16:
			return OracleDbType.Integer;
		case DbType.Single:
			return OracleDbType.Float;
		case DbType.Double:
			return OracleDbType.Double;
		case DbType.Date:
			return OracleDbType.Date;
		case DbType.DateTime:
			return OracleDbType.TimeStamp;
		case DbType.Time:
			return OracleDbType.IntervalDS;
		case DbType.Binary:
			return OracleDbType.Blob;
		case DbType.Boolean:
			return OracleDbType.Boolean;
		case DbType.Currency:
		case DbType.Decimal:
		case DbType.Int64:
		case DbType.UInt64:
		case DbType.VarNumeric:
			return OracleDbType.Number;
		case DbType.Object:
			return OracleDbType.Object;
		case DbType.Guid:
			return OracleDbType.Raw;
		default:
			throw new NotSupportedException();
		}
	}

	internal static string b(IntPtr A_0, int A_1)
	{
		while (A_1 > 0 && Marshal.ReadByte(A_0, A_1 - 1) == 0)
		{
			A_1--;
		}
		return Marshal.PtrToStringAnsi(A_0, A_1);
	}

	internal static string a(IntPtr A_0, int A_1)
	{
		while (A_1 > 0 && Marshal.ReadInt16(A_0, A_1 * 2 - 2) == 0)
		{
			A_1--;
		}
		return Marshal.PtrToStringUni(A_0, A_1);
	}

	internal static string a(OracleDbType A_0)
	{
		switch (A_0)
		{
		case OracleDbType.BFile:
			return "BFILE";
		case OracleDbType.Blob:
			return "BLOB";
		case OracleDbType.Char:
			return "CHAR";
		case OracleDbType.Clob:
			return "CLOB";
		case OracleDbType.Cursor:
			return "CURSOR";
		case OracleDbType.Date:
			return "DATE";
		case OracleDbType.Double:
		case OracleDbType.Float:
			return "FLOAT";
		case OracleDbType.Integer:
			return "INTEGER";
		case OracleDbType.IntervalDS:
			return "INTERVALDS";
		case OracleDbType.IntervalYM:
			return "INTERVALYM";
		case OracleDbType.Long:
			return "LONG";
		case OracleDbType.LongRaw:
			return "LONG RAW";
		case OracleDbType.NChar:
			return "NCHAR";
		case OracleDbType.NClob:
			return "NCLOB";
		case OracleDbType.NVarChar:
			return "NVARCHAR2";
		case OracleDbType.Number:
			return "NUMBER";
		case OracleDbType.Raw:
			return "RAW";
		case OracleDbType.RowId:
			return "ROWID";
		case OracleDbType.TimeStamp:
			return "TIMESTAMP";
		case OracleDbType.TimeStampLTZ:
			return "TIMESTAMP_LTZ";
		case OracleDbType.TimeStampTZ:
			return "TIMESTAMP_TZ";
		case OracleDbType.VarChar:
			return "VARCHAR2";
		default:
			return "";
		}
	}

	internal static bool b(Type A_0)
	{
		if (A_0.IsPrimitive || (object)A_0 == typeof(string) || (object)A_0 == typeof(decimal) || (object)A_0 == typeof(TimeSpan) || (object)A_0 == typeof(DateTime) || (object)A_0 == typeof(char[]) || (object)A_0 == typeof(byte[]))
		{
			return true;
		}
		if (A_0.IsArray)
		{
			return false;
		}
		if (typeof(ICustomOracleObject).IsAssignableFrom(A_0) || typeof(ICustomOracleArray).IsAssignableFrom(A_0) || (object)A_0 == typeof(NativeOracleObject) || (object)A_0 == typeof(NativeOracleArray) || (object)A_0 == typeof(NativeOracleTable) || (object)A_0 == typeof(OracleXml) || (object)A_0 == typeof(OracleAnyData) || (object)A_0 == typeof(OracleRef) || (object)A_0 == typeof(bool) || (object)A_0 == typeof(OracleBFile) || (object)A_0 == typeof(OracleBinary) || (object)A_0 == typeof(OracleCursor) || (object)A_0 == typeof(OracleDate) || (object)A_0 == typeof(OracleIntervalDS) || (object)A_0 == typeof(OracleIntervalYM) || (object)A_0 == typeof(OracleMonthSpan) || (object)A_0 == typeof(OracleTimeSpan) || (object)A_0 == typeof(OracleDateTime) || (object)A_0 == typeof(OracleLob) || (object)A_0 == typeof(OracleNumber) || (object)A_0 == typeof(OracleString) || (object)A_0 == typeof(OracleTimeStamp))
		{
			return false;
		}
		throw new ArgumentException(Devart.Common.al.a("UnknownType") + " " + A_0);
	}

	internal static string c(string A_0)
	{
		int num = A_0.IndexOf('.');
		if (num != -1)
		{
			num--;
			while (num >= 0 && char.IsDigit(A_0[num]))
			{
				num--;
			}
			num++;
			int num2 = Utils.TryParseInt(A_0, ref num);
			num++;
			int num3 = Utils.TryParseInt(A_0, ref num);
			num++;
			int num4 = Utils.TryParseInt(A_0, ref num);
			num++;
			int num5 = Utils.TryParseInt(A_0, ref num);
			return num2.ToString("00") + "." + num3.ToString("00") + "." + num4.ToString("00") + "." + num5.ToString("00");
		}
		throw new InvalidOperationException("");
	}

	internal static string a(string A_0, ArrayList A_1)
	{
		if (A_0 == null || A_0 == "")
		{
			throw new ArgumentException();
		}
		for (int num = 0; num < A_0.Length; num++)
		{
			char c2 = A_0[num];
			if ((c2 < 'A' || c2 > 'Z') && (c2 < 'a' || c2 > 'z') && (num == 0 || ((c2 < '0' || c2 > '9') && c2 != '#' && c2 != '_' && c2 != '$')))
			{
				if (num == 0)
				{
					A_0 = A_0.Substring(1);
					num--;
				}
				else
				{
					A_0 = A_0.Substring(0, num) + "_" + A_0.Substring(num + 1);
				}
			}
		}
		if (A_0 == "")
		{
			A_0 = "p";
		}
		int num2 = 0;
		string text = A_0;
		while (true)
		{
			bool flag = false;
			foreach (string item in A_1)
			{
				if (string.Compare(item, text, StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				break;
			}
			text = A_0 + ++num2;
		}
		A_1.Add(text);
		return text;
	}

	public static bool NeedQuote(string name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		char c2 = '"';
		string text = name.Trim();
		if (d.Contains(text.ToUpper(CultureInfo.InvariantCulture)))
		{
			return true;
		}
		int length = text.Length;
		if (length > 0 && (text[0] != c2 || text[length - 1] != c2))
		{
			if (text[0] >= '0' && text[0] <= '9')
			{
				return true;
			}
			for (int num = 0; num < length; num++)
			{
				char c3 = text[num];
				if ((c3 < 'A' || c3 > 'Z') && (num == 0 || ((c3 < '0' || c3 > '9') && c3 != '#' && c3 != '_' && c3 != '$')))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static string QuoteIfNeed(string name)
	{
		if (NeedQuote(name))
		{
			return "\"" + name + "\"";
		}
		return name;
	}

	public static string UnQuote(string name)
	{
		if (name.Length == 0)
		{
			return name;
		}
		int num = name.Length - 1;
		if (name[0] == '"' && name[num] == '"')
		{
			name = name.Remove(num, 1);
			name = name.Remove(0, 1);
			return name;
		}
		return name;
	}

	internal static string a(OracleConnectMode A_0)
	{
		return A_0 switch
		{
			OracleConnectMode.Default => "Default", 
			OracleConnectMode.SysDba => "SysDba", 
			OracleConnectMode.SysOper => "SysOper", 
			OracleConnectMode.SysAsm => "SysAsm", 
			_ => throw new ArgumentException(), 
		};
	}

	public static string GetCharSetName(int codePage)
	{
		return codePage switch
		{
			1256 => "AR8MSWIN", 
			1250 => "EE8MSWIN", 
			1251 => "CL8MSWIN", 
			1252 => "WE8MSWIN", 
			1253 => "EL8MSWIN", 
			1254 => "TR8MSWIN", 
			1255 => "IW8MSWIN", 
			1257 => "BLT8MSWIN", 
			1258 => "VN8MSWIN", 
			923 => "ET8MSWIN", 
			921 => "LT8MSWIN", 
			949 => "KO16MSWIN", 
			936 => "ZHS16MSWIN", 
			950 => "ZHT16MSWIN950", 
			20127 => "US-ASCII", 
			437 => "CP437", 
			37 => "CP037", 
			500 => "CP500", 
			1140 => "CP01140", 
			20285 => "CP285", 
			1146 => "CP01146", 
			850 => "CP850", 
			20106 => "DIN_66003", 
			20107 => "SEN_850200_B", 
			1148 => "CP01148", 
			858 => "CP00858", 
			28591 => "ISO-8859-1", 
			28592 => "ISO-8859-2", 
			28593 => "ISO-8859-3", 
			28594 => "ISO-8859-4", 
			28595 => "ISO-8859-5", 
			28596 => "ISO-8859-6", 
			28597 => "ISO-8859-7", 
			38598 => "ISO-8859-8-I", 
			28599 => "ISO-8859-9", 
			874 => "TIS-620", 
			28605 => "ISO-8859-15", 
			28603 => "ISO-8859-13", 
			21866 => "KOI8-U", 
			20420 => "CP420", 
			20424 => "CP424", 
			1026 => "CP1026", 
			20871 => "CP871", 
			20284 => "CP284", 
			1145 => "CP01145", 
			20924 => "CP00924", 
			852 => "CP852", 
			866 => "CP866", 
			862 => "CP862", 
			855 => "CP855", 
			857 => "CP857", 
			860 => "CP860", 
			861 => "CP861", 
			20273 => "CP273", 
			20280 => "CP280", 
			20277 => "IBM277", 
			20278 => "CP278", 
			870 => "CP870", 
			20297 => "CP297", 
			1141 => "CP01141", 
			865 => "CP865", 
			20866 => "KOI8-R", 
			1142 => "CP01142", 
			1143 => "CP01143", 
			1144 => "CP01144", 
			20108 => "NS_4551-1", 
			1147 => "CP01147", 
			20423 => "CP423", 
			51932 => "EUC-JP", 
			932 => "SHIFT_JIS", 
			51949 => "EUC-KR", 
			863 => "CP863", 
			10000 => "MACINTOSH", 
			869 => "CP869", 
			54936 => "GB18030", 
			65001 => "UTF-8", 
			1200 => "UTF-16", 
			1201 => "UTF-16BE", 
			_ => "", 
		};
	}

	public static int GetOCITextLength(Encoding encoding, string str)
	{
		if (encoding != Encoding.Unicode && encoding.GetMaxByteCount(0) > 1)
		{
			return encoding.GetByteCount(str);
		}
		return str.Length;
	}

	internal static string b(string A_0)
	{
		if (A_0.IndexOf('\'') >= 0)
		{
			return A_0.Replace("'", "''");
		}
		return A_0;
	}

	internal static void a(string A_0)
	{
		if (!OracleClientCompatible)
		{
			throw new InvalidOperationException($"Method \"{A_0}\" is designed for compartibility with OracleClient only. Please set OracleUtils.OracleClientCompatible=true");
		}
	}

	internal static void a(Type A_0)
	{
		if (!OracleClientCompatible)
		{
			throw new InvalidOperationException($"Class \"{A_0.Name}\" is designed for compartibility with OracleClient only. Please set OracleUtils.OracleClientCompatible=true");
		}
	}

	public static string QuotedSQLName(string name)
	{
		int num = name.IndexOf('.');
		if (num > 0)
		{
			string text = name.Substring(0, num).Trim();
			if (text.Length <= 1 || text[0] != '"' || text[text.Length - 1] != '"')
			{
				text = text.ToUpper(CultureInfo.InvariantCulture);
			}
			return text + '.' + QuotedSQLName(name.Substring(num + 1, name.Length - num - 1));
		}
		string text2 = name.Trim();
		if (text2.Length <= 1 || text2[0] != '"' || text2[text2.Length - 1] != '"')
		{
			text2 = text2.ToUpper(CultureInfo.InvariantCulture);
		}
		return text2;
	}

	internal static void a(XmlWriter A_0)
	{
		A_0.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
	}

	internal static bool a(XmlReader A_0)
	{
		string attribute = A_0.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
		bool flag = attribute != null && XmlConvert.ToBoolean(attribute);
		if (flag)
		{
			A_0.ReadElementString();
		}
		return flag;
	}

	public static ArrayList GetServers()
	{
		ArrayList arrayList = new ArrayList();
		lock (arrayList.SyncRoot)
		{
			if (arrayList.Count == 0)
			{
				DataTable dataSources = OracleDataSourceEnumerator.Instance.GetDataSources();
				ArrayList arrayList2 = new ArrayList(dataSources.Rows.Count);
				foreach (DataRow row in dataSources.Rows)
				{
					string text = row["InstanceName"] as string;
					if (Utils.IsEmpty(text))
					{
						text = "localhost";
					}
					if (!arrayList2.Contains(text))
					{
						arrayList2.Add(text);
					}
				}
				arrayList2.Sort();
				arrayList = arrayList2;
			}
		}
		h = true;
		return arrayList;
	}
}
