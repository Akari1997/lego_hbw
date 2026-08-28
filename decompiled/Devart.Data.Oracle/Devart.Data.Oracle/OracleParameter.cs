using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(aw))]
public class OracleParameter : DbParameterBase, ICloneable
{
	private OracleDbType m_a;

	private string m_b;

	private OracleType m_c;

	internal string d;

	private bool[] m_e;

	internal object f;

	internal Type g;

	private string h;

	[Browsable(false)]
	[DefaultValue(DbType.String)]
	public override DbType DbType
	{
		get
		{
			return OracleUtils.OracleDbTypeToDbType(OracleDbType);
		}
		set
		{
			if (ArrayLength > 0)
			{
				a(OracleDbType);
			}
			if (value == DbType.Object)
			{
				this.m_a = (OracleDbType)0;
				return;
			}
			OracleDbType oracleDbType = OracleUtils.a(value);
			if (oracleDbType == OracleDbType.VarChar)
			{
				this.m_a = (OracleDbType)0;
			}
			else
			{
				OracleDbType = oracleDbType;
			}
		}
	}

	[Category("Data")]
	[Devart.Common.i("OracleParameter_OracleType")]
	[DbProviderSpecificTypeProperty(true)]
	[DefaultValue(OracleDbType.VarChar)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public OracleDbType OracleDbType
	{
		get
		{
			if (this.m_a != 0)
			{
				return this.m_a;
			}
			if (ArrayLength != 0)
			{
				Array a_ = (Array)Value;
				Type type = a(a_);
				if ((object)type != typeof(object))
				{
					return OracleUtils.TypeToOracleDbType(type);
				}
			}
			else if (!Utils.IsNull(Value))
			{
				return OracleUtils.a(Value);
			}
			return OracleDbType.VarChar;
		}
		set
		{
			if (value != this.m_a)
			{
				this.m_a = value;
				try
				{
					base.CoercedValue = OracleUtils.ObjectToOracleValue(Value, this.m_a);
				}
				catch
				{
					base.CoercedValue = null;
				}
			}
		}
	}

	[Category("Data")]
	[DefaultValue("")]
	public string ObjectTypeName
	{
		get
		{
			return this.m_b;
		}
		set
		{
			this.m_b = value;
		}
	}

	[DefaultValue("")]
	[Category("Data")]
	internal string RecordTypeName
	{
		get
		{
			if (this.d == null)
			{
				return string.Empty;
			}
			return this.d;
		}
		set
		{
			if (value == null)
			{
				this.d = string.Empty;
			}
			else
			{
				this.d = value;
			}
		}
	}

	public object this[int index]
	{
		get
		{
			if (!(Value is Array))
			{
				throw new InvalidOperationException(Devart.Common.al.a("ParameterValueTypeNotArray"));
			}
			if (Utils.IsNull(Value))
			{
				throw new InvalidOperationException(Devart.Common.al.a("ParameterValueIsNull"));
			}
			object value = ((Array)Value).GetValue(index);
			if (this.m_e == null)
			{
				this.m_e = new bool[ArrayLength];
			}
			if (this.m_e[index])
			{
				return DBNull.Value;
			}
			return value;
		}
		set
		{
			f = null;
			if (this.m_a == OracleDbType.Array || this.m_a == OracleDbType.Table)
			{
				if (Utils.IsNull(Value))
				{
					Value = new object[ArrayLength];
					this.m_e = new bool[ArrayLength];
				}
				Array array = (Array)Value;
				array.SetValue(Convert.ChangeType(value, array.GetType().GetElementType(), null), index);
				return;
			}
			if (Utils.IsNull(Value))
			{
				Value = Array.CreateInstance(OracleUtils.a(this.m_a, A_1: true), ArrayLength);
				this.m_e = new bool[ArrayLength];
			}
			object value2 = null;
			if (!Utils.IsNull(value))
			{
				bool a_ = OracleUtils.b(Value.GetType().GetElementType());
				value2 = ((this.m_a != OracleDbType.Array && this.m_a != OracleDbType.Table) ? OracleUtils.a(value, this.m_a, a_) : value);
				if (this.m_e != null)
				{
					this.m_e[index] = false;
				}
			}
			else
			{
				if (this.m_e == null)
				{
					this.m_e = new bool[ArrayLength];
				}
				this.m_e[index] = true;
			}
			((Array)Value).SetValue(value2, index);
		}
	}

	[Browsable(false)]
	public object OracleValue
	{
		get
		{
			if (f != null)
			{
				switch (OracleDbType)
				{
				case OracleDbType.Long:
					if (f != typeof(OracleString))
					{
						return e(Value);
					}
					return f;
				case OracleDbType.LongRaw:
					if (f != typeof(OracleBinary))
					{
						return e(Value);
					}
					return f;
				case OracleDbType.Array:
				case OracleDbType.Object:
				case OracleDbType.Ref:
				case OracleDbType.Table:
				case OracleDbType.Xml:
				case OracleDbType.AnyData:
					return OracleUtils.a(f, OracleDbType, A_2: false);
				default:
					return f;
				}
			}
			if (ArrayLength != 0)
			{
				if (!Utils.IsNull(Value))
				{
					Array array = (Array)Value;
					if (Utils.IsNull(array))
					{
						return DBNull.Value;
					}
					Array array2 = Array.CreateInstance(OracleUtils.a(this.m_a, A_1: false), array.Length);
					for (int num = 0; num < array.Length; num++)
					{
						array2.SetValue(e(array.GetValue(num)), num);
					}
					return array2;
				}
				return Value;
			}
			return e(Value);
		}
		set
		{
			f = value;
			Value = value;
		}
	}

	public int ArrayLength
	{
		get
		{
			return c(Value);
		}
		set
		{
			f = null;
			if (value < 0)
			{
				throw new ArgumentException(Devart.Common.al.a("ArrayLengthCanNotBeLess0"));
			}
			if (value > 0)
			{
				a(OracleDbType);
			}
			int arrayLength = ArrayLength;
			if (arrayLength == value)
			{
				return;
			}
			if (value != 0)
			{
				if (!Utils.IsNull(Value))
				{
					Type type = null;
					Array array = null;
					bool[] array2 = null;
					if (Value is Array)
					{
						type = Value.GetType().GetElementType();
						array = Array.CreateInstance(type, value);
						array2 = new bool[value];
						int length = Math.Min(arrayLength, value);
						Array.Copy((Array)Value, 0, array, 0, length);
						if (this.m_e != null)
						{
							Array.Copy(this.m_e, 0, array2, 0, length);
						}
					}
					else if (Utils.IsNull(Value))
					{
						type = OracleUtils.a(this.m_a, A_1: true);
						array = Array.CreateInstance(type, value);
						array2 = new bool[value];
					}
					else
					{
						array = Array.CreateInstance(Value.GetType(), value);
						array.SetValue(Value, 0);
						array2 = new bool[value];
					}
					Value = array;
					this.m_e = array2;
				}
				else
				{
					Value = Array.CreateInstance(OracleUtils.OracleDbTypeToType(OracleDbType), value);
					this.m_e = null;
				}
			}
			else
			{
				Value = null;
				this.m_e = null;
			}
		}
	}

	[Browsable(false)]
	public new byte Precision
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	[Browsable(false)]
	public new byte Scale
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	internal int AssignedSize
	{
		get
		{
			if (ShouldSerializeSize())
			{
				return Size;
			}
			return 0;
		}
	}

	public override object Value
	{
		get
		{
			object obj = base.Value;
			if (obj == null)
			{
				switch (this.m_a)
				{
				case OracleDbType.BFile:
				case OracleDbType.Blob:
				case OracleDbType.Clob:
				case OracleDbType.NClob:
					if (!(f is OracleLob oracleLob))
					{
						obj = DBNull.Value;
						break;
					}
					if (this.m_a == OracleDbType.BFile)
					{
						((OracleBFile)oracleLob).OpenFile();
					}
					try
					{
						int num = (int)oracleLob.Length;
						byte[] array = new byte[num];
						oracleLob.Seek(0L, SeekOrigin.Begin);
						oracleLob.Read(array, 0, num);
						if ((object)g == typeof(byte[]))
						{
							obj = array;
						}
						else if ((object)g != typeof(OracleBinary))
						{
							Encoding encoding = ((this.m_a != OracleDbType.NClob && !oracleLob.Connection.d().w().a()) ? bl.a() : Encoding.Unicode);
							obj = (((object)g == typeof(string)) ? encoding.GetString(array, 0, array.Length) : (((object)g == typeof(char[])) ? encoding.GetChars(array) : (((object)g == typeof(OracleString)) ? ((object)new OracleString(encoding.GetString(array, 0, array.Length))) : ((this.m_a != OracleDbType.Clob && this.m_a != OracleDbType.NClob) ? ((object)array) : ((object)encoding.GetString(array, 0, array.Length))))));
						}
						else
						{
							obj = new OracleBinary(array);
						}
					}
					finally
					{
						if (this.m_a == OracleDbType.BFile)
						{
							((OracleBFile)oracleLob).CloseFile();
						}
					}
					break;
				}
				base.Value = obj;
			}
			return obj;
		}
		set
		{
			base.Value = value;
			if (Utils.MonoDetected && OracleDbType == OracleDbType.Integer && SourceColumnNullMapping)
			{
				value = ((value == null || DBNull.Value == value || (value is INullable && ((INullable)value).IsNull)) ? 1 : 0);
			}
			int arrayLength = ArrayLength;
			if (arrayLength > 0)
			{
				if (this.m_e == null || this.m_e.Length != arrayLength)
				{
					this.m_e = new bool[arrayLength];
				}
				Array array = Value as Array;
				for (int num = 0; num < arrayLength; num++)
				{
					object obj = array?.GetValue(num);
					this.m_e[num] = obj == null;
				}
			}
			else
			{
				this.m_e = null;
			}
		}
	}

	[Devart.Common.i("DataParameterparameterName")]
	[Category("DataCategory_Data")]
	public override string ParameterName
	{
		get
		{
			return base.ParameterName;
		}
		set
		{
			if (OracleUtils.OracleClientCompatible && value != null && value.Length > 0 && value[0] == ':')
			{
				base.ParameterName = ":" + value;
			}
			else
			{
				base.ParameterName = value;
			}
		}
	}

	internal string UsedParameterName
	{
		get
		{
			if (h != null)
			{
				return h;
			}
			return ParameterName;
		}
		set
		{
			h = value;
		}
	}

	public OracleParameter()
	{
	}

	public OracleParameter(string parameterName, object value)
	{
		ParameterName = parameterName;
		Value = value;
	}

	public OracleParameter(string parameterName, OracleDbType dbType)
	{
		ParameterName = parameterName;
		if (dbType != 0)
		{
			OracleDbType = dbType;
		}
		else
		{
			Value = (int)dbType;
		}
	}

	public OracleParameter(string parameterName, OracleDbType dbType, int size)
	{
		ParameterName = parameterName;
		OracleDbType = dbType;
		Size = size;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, ParameterDirection direction)
	{
		ParameterName = parameterName;
		OracleDbType = dbType;
		Direction = direction;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, string objectTypeName)
	{
		ParameterName = parameterName;
		OracleDbType = dbType;
		ObjectTypeName = objectTypeName;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, int size, string sourceColumn)
	{
		ParameterName = parameterName;
		OracleDbType = dbType;
		Size = size;
		SourceColumn = sourceColumn;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, object value, ParameterDirection direction)
	{
		ParameterName = parameterName;
		OracleDbType = dbType;
		Direction = direction;
		Value = value;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, int size, object value, ParameterDirection direction)
	{
		ParameterName = parameterName;
		OracleDbType = dbType;
		Direction = direction;
		Size = size;
		Value = value;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, int size, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
		: this(parameterName, dbType, size, direction, isNullable: false, 0, 0, sourceColumn, sourceVersion, value)
	{
		SourceColumnNullMapping = sourceColumnNullMapping;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
	{
		ParameterName = parameterName;
		OracleDbType = dbType;
		Size = size;
		Direction = direction;
		SourceColumn = sourceColumn;
		SourceVersion = sourceVersion;
		Value = value;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value, int arrayLength, string objectTypeName)
	{
		ParameterName = parameterName;
		OracleDbType = dbType;
		Size = size;
		Direction = direction;
		SourceColumn = sourceColumn;
		SourceVersion = sourceVersion;
		ArrayLength = arrayLength;
		ObjectTypeName = objectTypeName;
		Value = value;
	}

	public OracleParameter(string parameterName, OracleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, int arrayLength, string objectTypeName)
		: this(parameterName, dbType, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value, arrayLength, objectTypeName)
	{
		SourceColumnNullMapping = sourceColumnNullMapping;
	}

	public override string ToString()
	{
		return ParameterName;
	}

	public object Clone()
	{
		OracleParameter oracleParameter = new OracleParameter();
		oracleParameter.ParameterName = ParameterName;
		oracleParameter.m_a = this.m_a;
		oracleParameter.Size = Size;
		oracleParameter.Direction = Direction;
		oracleParameter.ArrayLength = ArrayLength;
		oracleParameter.Value = Value;
		oracleParameter.SourceColumn = SourceColumn;
		oracleParameter.SourceVersion = SourceVersion;
		oracleParameter.ObjectTypeName = ObjectTypeName;
		oracleParameter.SourceColumnNullMapping = SourceColumnNullMapping;
		return oracleParameter;
	}

	private Type a(Array A_0)
	{
		Type type = A_0.GetType().GetElementType();
		if ((object)type == typeof(object))
		{
			foreach (object item in A_0)
			{
				if (item != null && !(item is DBNull))
				{
					type = item.GetType();
					break;
				}
			}
		}
		return type;
	}

	private object e(object A_0)
	{
		return OracleUtils.a(A_0, OracleDbType, A_2: false);
	}

	private static bool b(OracleDbType A_0)
	{
		switch (A_0)
		{
		case OracleDbType.Boolean:
		case OracleDbType.Char:
		case OracleDbType.Date:
		case OracleDbType.Double:
		case OracleDbType.Float:
		case OracleDbType.Integer:
		case OracleDbType.NChar:
		case OracleDbType.NVarChar:
		case OracleDbType.Number:
		case OracleDbType.Raw:
		case OracleDbType.RowId:
		case OracleDbType.VarChar:
			return true;
		default:
			return false;
		}
	}

	private void a(OracleDbType A_0)
	{
		if (!b(A_0))
		{
			throw new ArgumentException($"Type \"OracleDbType.{A_0.ToString()}\" cannot be used in PL/SQL table parameter.");
		}
	}

	private static bool d(object A_0)
	{
		if (A_0 is Array)
		{
			if (!(A_0 is byte[]))
			{
				return !(A_0 is char[]);
			}
			return false;
		}
		return false;
	}

	private static int c(object A_0)
	{
		if (d(A_0))
		{
			return ((Array)A_0).Length;
		}
		return 0;
	}

	private static int a(object A_0, aq A_1)
	{
		if (Utils.IsNull(A_0))
		{
			return 0;
		}
		Type type = A_0.GetType();
		if ((object)type == typeof(byte))
		{
			return 1;
		}
		if ((object)type == typeof(char))
		{
			if (!A_1.a() && bl.a().GetMaxByteCount(0) > 1)
			{
				return bl.a().GetByteCount(new char[1] { (char)A_0 });
			}
			return 0;
		}
		if (type.IsPrimitive)
		{
			return 0;
		}
		if ((object)type == typeof(string))
		{
			string text = (string)A_0;
			if (!A_1.a() && bl.a().GetMaxByteCount(0) > 1)
			{
				return bl.a().GetByteCount(text);
			}
			return 0;
		}
		if ((object)type == typeof(OracleString))
		{
			if (!A_1.a() && bl.a().GetMaxByteCount(0) > 1 && ((OracleString)A_0).Value != null)
			{
				return bl.a().GetByteCount(((OracleString)A_0).Value);
			}
			return 0;
		}
		if ((object)type == typeof(char[]))
		{
			if (!A_1.a() && bl.a().GetMaxByteCount(0) > 1)
			{
				return bl.a().GetByteCount((char[])A_0);
			}
			return 0;
		}
		if ((object)type == typeof(OracleBinary))
		{
			if (!((OracleBinary)A_0).IsNull)
			{
				return ((OracleBinary)A_0).Length;
			}
			return 0;
		}
		if (A_0 is byte[] array)
		{
			return array.Length;
		}
		return 0;
	}

	internal static int a(object A_0, ref h A_1, OracleDbType A_2, int A_3, ParameterDirection A_4, int A_5, int A_6, g A_7)
	{
		int num = c(A_0);
		int num2;
		if (A_6 <= 0)
		{
			num2 = ((num <= 0) ? 1 : num);
		}
		else
		{
			if (A_6 != num)
			{
				throw new ArgumentException(Devart.Common.al.a("ArgumentItersMustBeEqualToArrayLength"));
			}
			num2 = A_6;
		}
		bool flag = A_6 <= 0 && (num > 0 || (d(A_0) && b(A_2)));
		aq aq2 = A_7.h();
		int A_8 = A_3;
		if (A_3 == 0)
		{
			A_8 = a(A_0, aq2);
		}
		A_1.n = A_5;
		A_1.l = A_5 + 2 * num2;
		switch (A_2)
		{
		case OracleDbType.RowId:
			A_1.c = 5;
			if (aq2.a())
			{
				A_1.o = A_1.l + A_1.l % 2;
				A_1.l = A_1.o + 2 * num2;
				A_1.m = 38;
			}
			else
			{
				A_1.m = 19;
			}
			A_1.d = 19;
			break;
		case OracleDbType.NVarChar:
		case OracleDbType.VarChar:
		{
			if (A_4 != ParameterDirection.Input && A_3 == 0)
			{
				A_8 = 0;
			}
			bool flag2 = A_7.b() && A_8 == 0 && num == 0 && A_4 == ParameterDirection.Input;
			if (flag2)
			{
				if (!a(A_0, ref A_8))
				{
					flag2 = false;
				}
				else if (A_8 == 0)
				{
					A_8 = 1;
				}
			}
			else if (A_8 == 0 && num > 0)
			{
				if (A_4 == ParameterDirection.Input)
				{
					A_8 = 1;
				}
				foreach (object item in (IEnumerable)A_0)
				{
					if (item != null && item != DBNull.Value)
					{
						int A_9 = 0;
						if (a(item, ref A_9))
						{
							A_8 = Math.Max(A_8, A_9);
						}
					}
				}
			}
			A_1.c = 5;
			if (aq2.a())
			{
				A_1.l += A_1.l % 2;
				A_1.o = A_1.l;
				A_1.l = A_1.o + 2 * num2;
				if (A_8 > 32767)
				{
					A_8 = 32767;
				}
				if (!flag2 && A_8 == 0)
				{
					if (A_7.v().StartsWith("08.01"))
					{
						A_1.m = 4000;
					}
					else if (flag)
					{
						A_1.m = 8000;
					}
					else
					{
						A_1.m = 8002;
					}
					A_1.d = 4000;
				}
				else
				{
					A_1.m = (A_8 << 1) + 2;
					A_1.d = A_8;
				}
			}
			else
			{
				if (A_8 > 32767)
				{
					A_8 = 32767;
				}
				if (!flag2 && A_8 == 0)
				{
					if (aq2.d() >= 12010000 && string.Compare(A_7.v(), "12") >= 0)
					{
						if (num2 <= 1)
						{
							A_1.m = 32768;
						}
						else
						{
							A_1.m = 32767;
						}
					}
					else if (aq2.d() >= 8010000)
					{
						if (num2 <= 1)
						{
							A_1.m = 4001;
						}
						else
						{
							A_1.m = 4000;
						}
					}
					else
					{
						A_1.m = 4000;
					}
					A_1.d = 4000;
				}
				else
				{
					A_1.m = A_8 + 1;
					A_1.d = A_8;
				}
				if (string.Compare(A_7.v(), "08") < 0)
				{
					A_1.m = 2000;
					A_1.d = 2000;
				}
			}
			if (A_2 == OracleDbType.NVarChar)
			{
				A_1.h = 2;
			}
			break;
		}
		case OracleDbType.Char:
		case OracleDbType.NChar:
		{
			if (A_4 != ParameterDirection.Input && A_3 == 0)
			{
				A_8 = 0;
			}
			bool flag2 = A_7.b() && A_8 == 0 && num == 0 && A_4 == ParameterDirection.Input;
			if (flag2)
			{
				if (!a(A_0, ref A_8))
				{
					flag2 = false;
				}
				else if (A_8 == 0)
				{
					A_8 = 1;
				}
			}
			A_1.c = 96;
			A_1.o = A_1.l;
			A_1.l = A_1.o + 2 * num2;
			if (aq2.a())
			{
				A_1.l += A_1.l % 2;
				if (!flag2 && (A_8 == 0 || A_8 > 1000))
				{
					A_1.m = 4000;
					A_1.d = 2000;
				}
				else
				{
					A_1.m = A_8 << 1;
					A_1.d = A_8;
				}
			}
			else if (!flag2 && (A_8 == 0 || A_8 > 2000))
			{
				A_1.m = 2000;
				A_1.d = 2000;
			}
			else
			{
				A_1.m = A_8;
				A_1.d = A_8;
			}
			if (A_2 == OracleDbType.NChar)
			{
				A_1.h = 2;
			}
			break;
		}
		case OracleDbType.Long:
			A_1.c = 8;
			goto IL_04d6;
		case OracleDbType.LongRaw:
			A_1.c = 24;
			goto IL_04d6;
		case OracleDbType.Integer:
			A_1.c = 3;
			A_1.m = 4;
			break;
		case OracleDbType.Boolean:
			A_1.c = 3;
			A_1.m = 4;
			break;
		case OracleDbType.Float:
			if (aq2.d() >= 10000000 && string.Compare(A_7.v(), "10") > 0)
			{
				A_1.c = 21;
				A_1.m = 4;
			}
			else
			{
				A_1.c = 4;
				A_1.m = 8;
			}
			break;
		case OracleDbType.Double:
			if (aq2.d() >= 10000000 && string.Compare(A_7.v(), "10") > 0)
			{
				A_1.c = 22;
			}
			else
			{
				A_1.c = 4;
			}
			A_1.m = 8;
			break;
		case OracleDbType.Number:
			A_1.c = 6;
			A_1.m = 22;
			break;
		case OracleDbType.IntervalDS:
			A_1.c = 190;
			break;
		case OracleDbType.IntervalYM:
			A_1.c = 189;
			break;
		case OracleDbType.TimeStamp:
			A_1.c = 187;
			break;
		case OracleDbType.TimeStampLTZ:
			A_1.c = 232;
			break;
		case OracleDbType.TimeStampTZ:
			A_1.c = 188;
			break;
		case OracleDbType.Raw:
			A_1.c = 23;
			if (A_8 == 0)
			{
				if (aq2.d() >= 12010000 && string.Compare(A_7.v(), "12") >= 0)
				{
					A_1.m = 32767;
				}
				else
				{
					A_1.m = 2000;
				}
				A_1.o = A_1.l;
				A_1.l = A_1.o + 2 * num2;
				break;
			}
			if (A_8 <= 2000)
			{
				A_1.m = A_8;
				A_1.o = A_1.l;
				A_1.l = A_1.o + 2 * num2;
				break;
			}
			A_1.m = A_8;
			A_1.o = 0;
			if (A_4 == ParameterDirection.InputOutput || A_4 == ParameterDirection.Output)
			{
				A_1.o = A_1.l;
				A_1.l = A_1.o + 2 * num2;
			}
			break;
		case OracleDbType.Clob:
		case OracleDbType.NClob:
			A_1.c = 112;
			if (A_2 == OracleDbType.NClob)
			{
				A_1.h = 2;
			}
			break;
		case OracleDbType.Blob:
			A_1.c = 113;
			break;
		case OracleDbType.BFile:
			A_1.c = 114;
			break;
		case OracleDbType.Cursor:
			A_1.c = 116;
			break;
		case OracleDbType.Date:
			if (string.Compare(A_7.v(), "08") < 0)
			{
				A_1.c = 12;
			}
			else
			{
				A_1.c = 156;
			}
			if (aq2.a())
			{
				A_1.m = 8;
			}
			else
			{
				A_1.m = 7;
			}
			break;
		case OracleDbType.Array:
		case OracleDbType.Object:
		case OracleDbType.Ref:
		case OracleDbType.Table:
		case OracleDbType.Xml:
			if (aq2.a())
			{
				A_1.l += A_1.l % 2;
			}
			if (A_2 == OracleDbType.Ref)
			{
				A_1.c = 110;
			}
			else
			{
				A_1.c = 108;
			}
			num = 0;
			if (A_2 == OracleDbType.Ref)
			{
				A_1.m = IntPtr.Size;
			}
			else
			{
				A_1.m = IntPtr.Size * 2;
			}
			break;
		case OracleDbType.AnyData:
			if (aq2.a())
			{
				A_1.l += A_1.l % 2;
			}
			A_1.c = 108;
			A_1.m = IntPtr.Size * 2;
			break;
		default:
			{
				throw new ArgumentException(Devart.Common.al.a("UnknownType"));
			}
			IL_04d6:
			A_1.n = -1;
			A_1.l = A_5;
			A_1.m = 0;
			A_1.x = b;
			A_1.y = a;
			break;
		}
		if (A_1.m == 0)
		{
			A_1.m = aq2.a(A_1.c);
		}
		if (A_6 > 0 || flag)
		{
			A_1.p = A_1.m;
			A_1.q = 2;
			if (A_1.o > 0)
			{
				A_1.r = 2;
			}
			if (flag)
			{
				A_1.z = num;
				A_1.aa = ((num <= 0) ? 1 : num);
				A_1.ab = A_1.l + A_1.m * num;
				return A_1.ab + 4;
			}
		}
		return A_1.l + A_1.m * num2;
	}

	private static bool a(object A_0, ref int A_1)
	{
		if (A_0 == null || A_0 == DBNull.Value)
		{
			A_1 = 0;
		}
		else if (A_0 is string)
		{
			A_1 = ((string)A_0).Length;
		}
		else if (A_0 is OracleString)
		{
			A_1 = ((OracleString)A_0).Length;
		}
		else if (A_0 is char)
		{
			A_1 = 1;
		}
		else if (A_0 is char[])
		{
			A_1 = ((char[])A_0).Length;
		}
		else
		{
			if (!(A_0 is Enum))
			{
				return false;
			}
			A_1 = Convert.ToString(A_0).Length;
		}
		return true;
	}

	private static byte[] a(object A_0, bool A_1)
	{
		if (A_0 == null || a(A_0))
		{
			return null;
		}
		if (A_0 is byte[])
		{
			return (byte[])A_0;
		}
		if (A_0 is OracleBinary oracleBinary)
		{
			return oracleBinary.Value;
		}
		Encoding encoding = ((!A_1) ? bl.a() : Encoding.Unicode);
		if (A_0 is string)
		{
			return encoding.GetBytes((string)A_0);
		}
		if (A_0 is char[])
		{
			return encoding.GetBytes((char[])A_0);
		}
		if (A_0 is OracleString { Value: var value })
		{
			if (value == null)
			{
				return null;
			}
			return encoding.GetBytes(value);
		}
		if (A_0 == null)
		{
			return null;
		}
		if (A_0 is bool)
		{
			return Devart.Common.e.a((bool)A_0);
		}
		if (A_0 is char)
		{
			return Devart.Common.e.a((char)A_0);
		}
		if (A_0 is double)
		{
			return Devart.Common.e.a((double)A_0);
		}
		if (A_0 is short)
		{
			return Devart.Common.e.a((short)A_0);
		}
		if (A_0 is int)
		{
			return Devart.Common.e.a((int)A_0);
		}
		if (A_0 is long)
		{
			return Devart.Common.e.a((long)A_0);
		}
		if (A_0 is float)
		{
			return Devart.Common.e.a((float)A_0);
		}
		if (A_0 is ushort)
		{
			return Devart.Common.e.a((ushort)A_0);
		}
		if (A_0 is uint)
		{
			return Devart.Common.e.a((uint)A_0);
		}
		if (A_0 is ulong)
		{
			return Devart.Common.e.a((ulong)A_0);
		}
		if (A_0 is OracleNumber oracleNumber)
		{
			return oracleNumber.BinData;
		}
		if (A_0 is Guid guid)
		{
			return guid.ToByteArray();
		}
		throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
	}

	internal static string b(object A_0)
	{
		if ((object)A_0.GetType() == typeof(char[]))
		{
			return new string((char[])A_0);
		}
		return Convert.ToString(A_0, CultureInfo.InvariantCulture);
	}

	internal static object a(OracleDbType A_0, object A_1, object A_2, byte[] A_3, Hashtable A_4, int A_5, int A_6, int A_7, int A_8, int A_9, bool A_10, OracleConnection A_11, ParameterDirection A_12, OracleType A_13, g A_14, out bool A_15)
	{
		A_15 = true;
		object obj = null;
		aq aq2 = A_14.h();
		if (A_2 is INullable && ((INullable)A_2).IsNull)
		{
			A_2 = null;
		}
		k k2 = A_14.b(A_8, 0, null, A_13);
		k2.a(A_4);
		k2.a(A_11);
		if (A_12 == ParameterDirection.Input || A_12 == ParameterDirection.InputOutput)
		{
			if (A_2 != null && !a(A_2) && !(A_2 as string == ""))
			{
				if (A_6 >= 0)
				{
					A_3[A_6] = 0;
					A_3[A_6 + 1] = 0;
				}
				bool flag = false;
				object a_ = ((A_12 != ParameterDirection.Input) ? DBNull.Value : null);
				switch (A_0)
				{
				case OracleDbType.Char:
				case OracleDbType.NChar:
				case OracleDbType.NVarChar:
				case OracleDbType.RowId:
				case OracleDbType.VarChar:
				{
					Encoding encoding = A_14.h().o();
					int num = A_9;
					bool flag2 = A_14.h().a();
					bool flag3 = A_0 == OracleDbType.Char || A_0 == OracleDbType.NChar;
					if (!flag3)
					{
						num = ((!flag2) ? (num - 1) : ((num >> 1) - 1));
					}
					if (A_14.b())
					{
						string value = b(A_2);
						A_4[A_5] = value;
					}
					else
					{
						int num2;
						if (A_2 is char[])
						{
							char[] array = (char[])A_2;
							if (array.Length > num)
							{
								throw new OracleException(0, Devart.Common.al.a("ParamValueTooLong"));
							}
							num2 = encoding.GetBytes(array, 0, Math.Min(array.Length, num), A_3, A_5);
						}
						else
						{
							string text = b(A_2);
							if (text == null || (text == "" && (A_0 == OracleDbType.Char || A_0 == OracleDbType.NChar)))
							{
								flag = true;
								break;
							}
							if (text.Length > num)
							{
								throw new OracleException(0, Devart.Common.al.a("ParamValueTooLong"));
							}
							num2 = encoding.GetBytes(text, 0, Math.Min(text.Length, num), A_3, A_5);
						}
						if (!flag3)
						{
							if (!flag2)
							{
								A_3[A_5 + num2] = 0;
								num2++;
							}
							else
							{
								A_3[A_5 + num2] = 0;
								A_3[A_5 + num2 + 1] = 0;
							}
						}
						if (A_7 > 0)
						{
							int num3 = num2;
							if (flag3 && flag2)
							{
								num3 = num2 >> 1;
							}
							A_3[A_7] = (byte)num3;
							A_3[A_7 + 1] = (byte)(num3 >> 8);
						}
					}
					return obj;
				}
				case OracleDbType.Long:
				case OracleDbType.LongRaw:
				{
					bool a_2 = A_14.h().a();
					byte[] a_3 = ((A_0 != OracleDbType.LongRaw && !(A_2 is byte[]) && !(A_2 is char[])) ? a(b(A_2), a_2) : a(A_2, a_2));
					obj = new a6(a_3, A_1: false);
					break;
				}
				case OracleDbType.Integer:
				{
					int num5 = ((!(A_2 is OracleNumber)) ? Convert.ToInt32(A_2) : OracleNumber.d((OracleNumber)A_2));
					A_3[A_5] = (byte)num5;
					A_3[A_5 + 1] = (byte)(num5 >> 8);
					A_3[A_5 + 2] = (byte)(num5 >> 16);
					A_3[A_5 + 3] = (byte)(num5 >> 24);
					return obj;
				}
				case OracleDbType.Boolean:
					if (Convert.ToBoolean(A_2))
					{
						A_3[A_5] = 1;
					}
					else
					{
						A_3[A_5] = 0;
					}
					return obj;
				case OracleDbType.Double:
				case OracleDbType.Float:
					if (A_8 == 21)
					{
						float a_4 = ((!(A_2 is OracleNumber)) ? Convert.ToSingle(A_2) : OracleNumber.a((OracleNumber)A_2));
						byte[] src = Devart.Common.e.a(a_4);
						Buffer.BlockCopy(src, 0, A_3, A_5, 4);
					}
					else
					{
						double a_5 = ((!(A_2 is OracleNumber)) ? Convert.ToDouble(A_2) : OracleNumber.b((OracleNumber)A_2));
						byte[] src2 = Devart.Common.e.a(a_5);
						Buffer.BlockCopy(src2, 0, A_3, A_5, 8);
					}
					return obj;
				case OracleDbType.Number:
				{
					OracleNumber oracleNumber = ((A_2 is OracleNumber) ? ((OracleNumber)A_2) : ((!(A_2 is string) && !(A_2 is OracleString)) ? new OracleNumber(Convert.ToDecimal(A_2)) : OracleNumber.Parse(b(A_2))));
					if (oracleNumber.BinData != null)
					{
						Buffer.BlockCopy(oracleNumber.BinData, 0, A_3, A_5, 22);
					}
					else
					{
						if (A_6 >= 0)
						{
							A_3[A_6] = 0;
							A_3[A_6 + 1] = 0;
						}
						Buffer.BlockCopy(new byte[22], 0, A_3, A_5, 22);
					}
					return obj;
				}
				case OracleDbType.IntervalDS:
				{
					OracleIntervalDS oracleIntervalDS;
					if (A_2 is OracleTimeSpan && OracleUtils.OracleClientCompatible)
					{
						oracleIntervalDS = ((OracleTimeSpan)A_2).a;
					}
					else if (A_2 is OracleIntervalDS)
					{
						oracleIntervalDS = (OracleIntervalDS)A_2;
					}
					else
					{
						oracleIntervalDS = ((A_2 is TimeSpan) ? new OracleIntervalDS((TimeSpan)A_2) : ((!(A_2 is string) && !(A_2 is OracleString)) ? new OracleIntervalDS(Convert.ToDouble(A_2)) : OracleIntervalDS.Parse(b(A_2))));
						obj = oracleIntervalDS;
					}
					flag = oracleIntervalDS.IsNull;
					if (flag)
					{
						k2.a(A_3, A_5, a_);
					}
					else
					{
						k2.a(A_3, A_5, oracleIntervalDS);
					}
					break;
				}
				case OracleDbType.IntervalYM:
				{
					OracleIntervalYM oracleIntervalYM;
					if (A_2 is OracleMonthSpan && OracleUtils.OracleClientCompatible)
					{
						oracleIntervalYM = ((OracleMonthSpan)A_2).a;
					}
					else if (A_2 is OracleIntervalYM)
					{
						oracleIntervalYM = (OracleIntervalYM)A_2;
					}
					else if (A_2 is string || A_2 is OracleString)
					{
						oracleIntervalYM = OracleIntervalYM.Parse(b(A_2));
						obj = oracleIntervalYM;
					}
					else
					{
						oracleIntervalYM = (((object)A_2.GetType() != typeof(double) && (object)A_2.GetType() != typeof(float) && (object)A_2.GetType() != typeof(decimal) && (object)A_2.GetType() != typeof(OracleNumber)) ? new OracleIntervalYM(Convert.ToInt64(A_2)) : new OracleIntervalYM(Convert.ToDouble(A_2)));
						obj = oracleIntervalYM;
					}
					flag = oracleIntervalYM.IsNull;
					if (flag)
					{
						k2.a(A_3, A_5, a_);
					}
					else
					{
						k2.a(A_3, A_5, oracleIntervalYM);
					}
					break;
				}
				case OracleDbType.TimeStamp:
				case OracleDbType.TimeStampLTZ:
				case OracleDbType.TimeStampTZ:
				{
					OracleTimeStamp oracleTimeStamp;
					if (A_2 is OracleDateTime && OracleUtils.OracleClientCompatible)
					{
						oracleTimeStamp = ((OracleDateTime)A_2).a;
					}
					else if (A_2 is OracleTimeStamp)
					{
						oracleTimeStamp = (OracleTimeStamp)A_2;
					}
					else
					{
						oracleTimeStamp = ((A_2 is OracleDate) ? new OracleTimeStamp(((OracleDate)A_2).Value, A_0) : ((!(A_2 is string) && !(A_2 is OracleString)) ? new OracleTimeStamp(Convert.ToDateTime(A_2), A_0) : OracleTimeStamp.Parse(b(A_2), A_0)));
						obj = oracleTimeStamp;
					}
					flag = oracleTimeStamp.IsNull;
					if (flag)
					{
						k2.a(A_3, A_5, a_);
					}
					else
					{
						k2.a(A_3, A_5, oracleTimeStamp);
					}
					break;
				}
				case OracleDbType.Raw:
				{
					byte[] array3 = a(A_2, A_14.h().a());
					if (array3 == null)
					{
						flag = true;
						break;
					}
					int num4 = Math.Min(array3.Length, A_9);
					Buffer.BlockCopy(array3, 0, A_3, A_5, num4);
					if (A_7 > 0)
					{
						A_3[A_7] = (byte)num4;
						A_3[A_7 + 1] = (byte)(num4 >> 8);
					}
					break;
				}
				case OracleDbType.BFile:
				case OracleDbType.Blob:
				case OracleDbType.Clob:
				case OracleDbType.NClob:
				{
					OracleLob oracleLob;
					if (A_2 is OracleLob)
					{
						oracleLob = (OracleLob)A_2;
						if (oracleLob.Cached && A_0 != OracleDbType.BFile)
						{
							oracleLob.ClearLobLocator();
						}
					}
					else
					{
						if (!A_10)
						{
							A_15 = false;
							aa aa2 = ((v)A_14).h();
							aa2.c(aa2.j().OCIDescriptorAlloc(aa2.h(), out var descpp, 50, 0u, 0u));
							k2.a(A_3, A_5, descpp);
							break;
						}
						bool flag4 = A_14.h().a() || OracleUtils.OracleClientCompatible;
						byte[] array2 = ((A_0 != OracleDbType.Blob && A_0 != OracleDbType.BFile && !(A_2 is byte[]) && !(A_2 is char[])) ? a(b(A_2), flag4 || A_0 == OracleDbType.NClob) : a(A_2, flag4 || A_0 == OracleDbType.NClob));
						if (array2 == null)
						{
							flag = true;
							k2.a(A_3, A_5, a_);
							break;
						}
						if (!(A_1 is OracleLob))
						{
							oracleLob = ((A_0 != OracleDbType.BFile) ? new OracleLob(A_11, A_0) : new OracleBFile(A_11));
						}
						else
						{
							oracleLob = (OracleLob)A_1;
							oracleLob.Erase();
						}
						oracleLob.Write(array2, 0, array2.Length);
						obj = oracleLob;
					}
					if (oracleLob.Connection == null)
					{
						oracleLob.Connection = A_11;
					}
					flag = oracleLob.IsNull;
					if (flag)
					{
						k2.a(A_3, A_5, a_);
					}
					else
					{
						k2.a(A_3, A_5, oracleLob.LobLocator);
					}
					break;
				}
				case OracleDbType.Cursor:
				{
					OracleCursor oracleCursor = A_2 as OracleCursor;
					s s2 = null;
					if (oracleCursor != null)
					{
						s2 = oracleCursor.Stmt;
					}
					if (s2 == null || s2.a())
					{
						s2 = aq2.a(A_14, A_1: true);
					}
					k2.a(A_3, A_5, s2);
					obj = s2;
					break;
				}
				case OracleDbType.Date:
				{
					OracleDate oracleDate;
					if (A_2 is OracleDate)
					{
						oracleDate = (OracleDate)A_2;
					}
					else
					{
						oracleDate = ((A_2 is OracleTimeStamp) ? new OracleDate(((OracleTimeStamp)A_2).Value) : ((!(A_2 is string) && !(A_2 is OracleString)) ? new OracleDate(Convert.ToDateTime(A_2)) : OracleDate.Parse(b(A_2))));
						obj = oracleDate;
					}
					flag = oracleDate.IsNull;
					if (!flag)
					{
						if (string.Compare(A_14.v(), "08") < 0)
						{
							Buffer.BlockCopy(oracleDate.NativeBinData, 0, A_3, A_5, 7);
						}
						else
						{
							Buffer.BlockCopy(oracleDate.BinData, 0, A_3, A_5, 7);
						}
					}
					break;
				}
				case OracleDbType.Object:
					if (A_2 is ICustomOracleObject)
					{
						if (!A_14.b())
						{
							NativeOracleObject nativeOracleObject = ((ICustomOracleObject)A_2).ToOracleObject(A_11);
							obj = nativeOracleObject;
							k2.a(A_3, A_5, nativeOracleObject.Instance);
						}
						else
						{
							k2.a(A_3, A_5, A_2);
						}
					}
					else
					{
						NativeOracleObject nativeOracleObject2 = (NativeOracleObject)A_2;
						k2.a(A_3, A_5, nativeOracleObject2.Instance);
					}
					break;
				case OracleDbType.Array:
				case OracleDbType.Table:
					if (A_2 is ICustomOracleArray)
					{
						if (!A_14.b())
						{
							NativeOracleArray nativeOracleArray = ((ICustomOracleArray)A_2).ToOracleArray(A_11);
							obj = nativeOracleArray;
							k2.a(A_3, A_5, nativeOracleArray.Instance);
						}
						else
						{
							k2.a(A_3, A_5, A_2);
						}
					}
					else if (A_2 is IEnumerable)
					{
						NativeOracleArray nativeOracleArray2 = ((A_0 != OracleDbType.Table) ? new NativeOracleArray(A_13, A_11) : new NativeOracleTable(A_13, A_11));
						obj = nativeOracleArray2;
						foreach (object item in (IEnumerable)A_2)
						{
							nativeOracleArray2.Add(item);
						}
						k2.a(A_3, A_5, nativeOracleArray2.Instance);
					}
					else
					{
						NativeOracleArray nativeOracleArray3 = (NativeOracleArray)A_2;
						k2.a(A_3, A_5, nativeOracleArray3.Instance);
					}
					break;
				case OracleDbType.Ref:
					if (A_2 is OracleRef)
					{
						OracleRef oracleRef = (OracleRef)A_2;
						flag = oracleRef.IsNull;
						oracleRef.Connection = A_11;
						k2.a(A_3, A_5, oracleRef.Handle);
					}
					break;
				case OracleDbType.Xml:
				{
					OracleXml oracleXml;
					if (A_2 is OracleXml)
					{
						oracleXml = (OracleXml)A_2;
					}
					else
					{
						oracleXml = ((A_2 is string) ? new OracleXml((string)A_2) : ((A_2 is char[]) ? new OracleXml((char[])A_2) : ((!(A_2 is OracleLob)) ? new OracleXml() : new OracleXml((OracleLob)A_2))));
						obj = oracleXml;
					}
					oracleXml.Connection = A_11;
					k2.a(A_3, A_5, oracleXml.XmlObject);
					break;
				}
				case OracleDbType.AnyData:
					if (A_2 is OracleAnyData oracleAnyData)
					{
						oracleAnyData.Connection = A_11;
						k2.a(A_3, A_5, oracleAnyData.AnyDataObject);
					}
					else
					{
						flag = true;
					}
					break;
				default:
					throw new ArgumentException(Devart.Common.al.a("UnknownType"));
				}
				if (flag && A_6 >= 0)
				{
					A_3[A_6] = byte.MaxValue;
					A_3[A_6 + 1] = byte.MaxValue;
				}
				return obj;
			}
			if (A_6 >= 0)
			{
				A_3[A_6] = byte.MaxValue;
				A_3[A_6 + 1] = byte.MaxValue;
			}
			if (A_12 == ParameterDirection.Input)
			{
				switch (A_0)
				{
				case OracleDbType.Cursor:
				{
					s s3 = aq2.a(A_14, A_1: true);
					k2.a(A_3, A_5, s3);
					obj = s3;
					break;
				}
				case OracleDbType.TimeStamp:
				case OracleDbType.TimeStampLTZ:
				case OracleDbType.TimeStampTZ:
				{
					OracleTimeStamp oracleTimeStamp2 = new OracleTimeStamp(1, 1, 2, A_0);
					k2.a(A_3, A_5, oracleTimeStamp2);
					break;
				}
				case OracleDbType.Object:
					if (!A_14.b())
					{
						NativeOracleObject nativeOracleObject3 = new NativeOracleObject(A_13, A_11);
						obj = nativeOracleObject3;
						nativeOracleObject3.IsNull = true;
						k2.a(A_3, A_5, nativeOracleObject3.Instance);
					}
					else
					{
						k2.a(A_3, A_5, null);
					}
					break;
				case OracleDbType.Array:
					if (!A_14.b())
					{
						NativeOracleArray nativeOracleArray4 = new NativeOracleArray(A_13, A_11);
						obj = nativeOracleArray4;
						nativeOracleArray4.IsNull = true;
						k2.a(A_3, A_5, nativeOracleArray4.Instance);
					}
					else
					{
						k2.a(A_3, A_5, null);
					}
					break;
				case OracleDbType.Table:
					if (!A_14.b())
					{
						NativeOracleTable nativeOracleTable = new NativeOracleTable(A_13, A_11);
						obj = nativeOracleTable;
						nativeOracleTable.IsNull = true;
						k2.a(A_3, A_5, nativeOracleTable.Instance);
					}
					else
					{
						k2.a(A_3, A_5, null);
					}
					break;
				case OracleDbType.BFile:
				case OracleDbType.Blob:
				case OracleDbType.Clob:
				case OracleDbType.IntervalDS:
				case OracleDbType.IntervalYM:
				case OracleDbType.NClob:
				case OracleDbType.Ref:
				case OracleDbType.Xml:
					k2.a(A_3, A_5, null);
					break;
				}
				return obj;
			}
			switch (A_0)
			{
			case OracleDbType.BFile:
			case OracleDbType.Blob:
			case OracleDbType.Clob:
			case OracleDbType.NClob:
				if (A_14 is v)
				{
					A_15 = false;
					aa aa3 = ((v)A_14).h();
					aa3.c(aa3.j().OCIDescriptorAlloc(aa3.h(), out var descpp2, 50, 0u, 0u));
					k2.a(A_3, A_5, descpp2);
				}
				break;
			}
		}
		if (A_12 == ParameterDirection.Output || A_12 == ParameterDirection.ReturnValue)
		{
			if (A_6 >= 0)
			{
				A_3[A_6] = byte.MaxValue;
				A_3[A_6 + 1] = byte.MaxValue;
			}
			switch (A_0)
			{
			case OracleDbType.BFile:
			case OracleDbType.Blob:
			case OracleDbType.Clob:
			case OracleDbType.NClob:
				if (A_14 is v)
				{
					A_15 = false;
					aa aa4 = ((v)A_14).h();
					aa4.c(aa4.j().OCIDescriptorAlloc(aa4.h(), out var descpp3, 50, 0u, 0u));
					k2.a(A_3, A_5, descpp3);
				}
				break;
			}
		}
		switch (A_0)
		{
		case OracleDbType.Long:
		case OracleDbType.LongRaw:
			if (obj == null)
			{
				obj = new a6(A_0: false);
			}
			return obj;
		case OracleDbType.IntervalDS:
		{
			OracleIntervalDS oracleIntervalDS2;
			if (A_2 is OracleTimeSpan && OracleUtils.OracleClientCompatible)
			{
				oracleIntervalDS2 = ((OracleTimeSpan)A_2).a;
			}
			else if (A_2 is OracleIntervalDS)
			{
				oracleIntervalDS2 = (OracleIntervalDS)A_2;
			}
			else
			{
				oracleIntervalDS2 = new OracleIntervalDS(TimeSpan.Zero);
				obj = oracleIntervalDS2;
			}
			k2.a(A_3, A_5, oracleIntervalDS2);
			break;
		}
		case OracleDbType.IntervalYM:
		{
			OracleIntervalYM oracleIntervalYM2;
			if (A_2 is OracleMonthSpan && OracleUtils.OracleClientCompatible)
			{
				oracleIntervalYM2 = ((OracleMonthSpan)A_2).a;
			}
			else if (A_2 is OracleIntervalYM)
			{
				oracleIntervalYM2 = (OracleIntervalYM)A_2;
			}
			else
			{
				oracleIntervalYM2 = new OracleIntervalYM(0L);
				obj = oracleIntervalYM2;
			}
			k2.a(A_3, A_5, oracleIntervalYM2);
			break;
		}
		case OracleDbType.TimeStamp:
		case OracleDbType.TimeStampLTZ:
		case OracleDbType.TimeStampTZ:
		{
			OracleTimeStamp oracleTimeStamp3;
			if (A_2 is OracleDateTime && OracleUtils.OracleClientCompatible)
			{
				oracleTimeStamp3 = ((OracleDateTime)A_2).a;
			}
			else if (A_2 is OracleTimeStamp)
			{
				oracleTimeStamp3 = (OracleTimeStamp)A_2;
			}
			else
			{
				oracleTimeStamp3 = new OracleTimeStamp(DateTime.MinValue, A_0);
				obj = oracleTimeStamp3;
			}
			k2.a(A_3, A_5, oracleTimeStamp3);
			break;
		}
		case OracleDbType.Cursor:
		{
			s s4 = aq2.a(A_14, A_1: true);
			k2.a(A_3, A_5, s4);
			obj = s4;
			break;
		}
		case OracleDbType.Object:
			if (!A_14.b())
			{
				NativeOracleObject nativeOracleObject4 = new NativeOracleObject(A_13, A_11);
				obj = nativeOracleObject4;
				nativeOracleObject4.IsNull = true;
				k2.a(A_3, A_5, nativeOracleObject4.Instance);
			}
			else
			{
				k2.a(A_3, A_5, null);
			}
			break;
		case OracleDbType.Array:
			if (!A_14.b())
			{
				NativeOracleArray nativeOracleArray5 = new NativeOracleArray(A_13, A_11);
				obj = nativeOracleArray5;
				nativeOracleArray5.IsNull = true;
				k2.a(A_3, A_5, nativeOracleArray5.Instance);
			}
			else
			{
				k2.a(A_3, A_5, null);
			}
			break;
		case OracleDbType.Table:
			if (!A_14.b())
			{
				NativeOracleTable nativeOracleTable2 = new NativeOracleTable(A_13, A_11);
				obj = nativeOracleTable2;
				nativeOracleTable2.IsNull = true;
				k2.a(A_3, A_5, nativeOracleTable2.Instance);
			}
			else
			{
				k2.a(A_3, A_5, null);
			}
			break;
		case OracleDbType.Ref:
		case OracleDbType.Xml:
			k2.a(A_3, A_5, null);
			break;
		}
		return obj;
	}

	internal void a(ref h A_0, bool A_1, OracleConnection A_2, byte[] A_3, Hashtable A_4, g A_5, out bool A_6, int A_7)
	{
		int num = ArrayLength;
		OracleDbType oracleDbType = OracleDbType;
		switch (oracleDbType)
		{
		case OracleDbType.Array:
		case OracleDbType.Object:
		case OracleDbType.Ref:
		case OracleDbType.Table:
		case OracleDbType.Xml:
		case OracleDbType.AnyData:
		{
			if (num > 0 && A_7 == 0 && (oracleDbType == OracleDbType.Array || oracleDbType == OracleDbType.Table))
			{
				num = 0;
			}
			object obj = Value;
			if (obj is Array array)
			{
				foreach (object item in array)
				{
					if (item != null)
					{
						obj = item;
						break;
					}
				}
			}
			OracleType oracleType = null;
			if (obj is OracleRef)
			{
				OracleRef oracleRef = (OracleRef)obj;
				oracleType = oracleRef.RefType;
				if (oracleType == null)
				{
					if (this.m_b == null || this.m_b == "")
					{
						throw new ArgumentException(Devart.Common.al.a("ObjectTypeNameMustBeSpecified"));
					}
					oracleType = (oracleRef.RefType = OracleType.a(this.m_b, A_5));
				}
			}
			else if (obj is OracleObject && obj != OracleObject.Null)
			{
				oracleType = ((OracleObject)obj).ObjectType;
			}
			else if (obj is OracleArray && obj != OracleArray.Null && obj != OracleTable.Null)
			{
				oracleType = ((OracleArray)obj).ObjectType;
			}
			else if (obj is OracleXml || oracleDbType == OracleDbType.Xml)
			{
				oracleType = OracleType.a("SYS", "XMLTYPE", A_5);
			}
			else if (obj is OracleAnyData || oracleDbType == OracleDbType.AnyData)
			{
				oracleType = OracleType.a("SYS", "ANYDATA", A_5);
			}
			else if (obj is NativeOracleObject)
			{
				NativeOracleObject nativeOracleObject = (NativeOracleObject)obj;
				oracleType = nativeOracleObject.ObjectType;
			}
			else if (obj is NativeOracleArray)
			{
				NativeOracleArray nativeOracleArray = (NativeOracleArray)obj;
				oracleType = nativeOracleArray.ObjectType;
			}
			else
			{
				if (this.m_b == null || !(this.m_b != ""))
				{
					throw new ArgumentException(Devart.Common.al.a("ObjectTypeNameMustBeSpecified"));
				}
				oracleType = OracleType.a(this.m_b, A_5);
			}
			this.m_c = oracleType;
			A_0.s = A_5.b(oracleType);
			break;
		}
		}
		if (num == 0 && (!b(oracleDbType) || !d(Value)))
		{
			object obj2 = a(oracleDbType, f, Value, A_3, A_4, A_0.l, A_0.n, A_0.o, A_0.c, A_0.m, A_1, A_2, Direction, this.m_c, A_5, out A_6);
			if (obj2 != null && f != obj2 && f is IDisposable disposable)
			{
				disposable.Dispose();
			}
			f = obj2;
		}
		else
		{
			A_6 = true;
			Array array2 = Value as Array;
			Array array3 = null;
			int num2 = A_0.l;
			int num3 = A_0.n;
			int num4 = A_0.o;
			int a_ = A_0.c;
			int num5 = A_0.p;
			int num6 = A_0.q;
			int num7 = A_0.r;
			int a_2 = A_0.m;
			for (int num8 = 0; num8 < num; num8++)
			{
				object a_3 = array2?.GetValue(num8);
				if (this.m_e != null && this.m_e[num8])
				{
					a_3 = null;
				}
				object a_4 = null;
				if (array3 != null && num8 < array3.Length)
				{
					a_4 = array3.GetValue(num8);
				}
				a_4 = a(oracleDbType, a_4, a_3, A_3, A_4, num2, num3, num4, a_, a_2, A_1, A_2, Direction, this.m_c, A_5, out A_6);
				if (a_4 != null)
				{
					if (array3 == null)
					{
						array3 = Array.CreateInstance(a_4.GetType(), num);
					}
					array3.SetValue(a_4, num8);
				}
				num2 += num5;
				if (num3 >= 0)
				{
					num3 += num6;
				}
				if (num4 > 0)
				{
					num4 += num7;
				}
			}
			if (A_0.ab > 0)
			{
				A_3[A_0.ab] = (byte)num;
				A_3[A_0.ab + 1] = (byte)(num >> 8);
			}
			f = array3;
		}
		switch (oracleDbType)
		{
		case OracleDbType.Long:
		case OracleDbType.LongRaw:
			A_0.v = f;
			A_0.w = f;
			if (Direction == ParameterDirection.Input && num == 0)
			{
				if (f != null)
				{
					A_0.m = ((a6)f).e();
				}
				if (A_0.m == 0)
				{
					A_0.m = 1;
				}
			}
			else
			{
				A_0.m = int.MaxValue;
			}
			break;
		case OracleDbType.Cursor:
			if (Value is OracleCursor oracleCursor && Direction != ParameterDirection.Input && Direction != ParameterDirection.InputOutput)
			{
				oracleCursor.Close();
			}
			break;
		}
	}

	private static int a(byte[] A_0, int A_1, bool A_2)
	{
		int num = 0;
		int num2 = A_0.Length;
		if (!A_2)
		{
			while (A_0[A_1] != 0 && A_1 < num2)
			{
				A_1++;
				num++;
			}
		}
		else
		{
			while ((A_0[A_1] != 0 || A_0[A_1 + 1] != 0) && A_1 + 1 < num2)
			{
				A_1 += 2;
				num += 2;
			}
		}
		return num;
	}

	private static object a(OracleDbType A_0, object A_1, ref Type A_2, ref object A_3, byte[] A_4, Hashtable A_5, int A_6, int A_7, int A_8, int A_9, bool A_10, bool A_11, OracleCommand A_12, ParameterDirection A_13, g A_14, OracleType A_15)
	{
		bool flag = false;
		if (A_7 >= 0 && A_9 != 108)
		{
			flag = Devart.Common.e.h(A_4, A_7) != 0;
		}
		k k2 = A_14.b(A_9, (A_0 == OracleDbType.NChar || A_0 == OracleDbType.NClob) ? 2 : 0, null, A_15);
		k2.a(A_5);
		k2.a(A_12.Connection);
		switch (A_0)
		{
		case OracleDbType.BFile:
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
			if (A_1 is OracleLob { IsNull: false } oracleLob)
			{
				if (oracleLob.Connection == null)
				{
					oracleLob.Connection = A_12.Connection;
				}
				if (A_13 == ParameterDirection.Input)
				{
					return A_1;
				}
				if (flag)
				{
					return DBNull.Value;
				}
				return oracleLob;
			}
			if (!(A_3 is OracleLob))
			{
				a3 a11 = k2.a(A_4, A_6, A_2: false, A_3: true);
				if (A_0 == OracleDbType.BFile)
				{
					if (A_13 == ParameterDirection.Input)
					{
						return A_1;
					}
					if (flag)
					{
						return DBNull.Value;
					}
					A_3 = new OracleBFile(A_12.Connection, null, 0, a11);
					if (A_1 is OracleBFile)
					{
						return A_3;
					}
				}
				else
				{
					if (A_1 is OracleLob)
					{
						if (A_13 == ParameterDirection.Input)
						{
							return A_1;
						}
						if (flag)
						{
							return DBNull.Value;
						}
						return new OracleLob(A_12.Connection, null, 0, a11, A_0);
					}
					if (!A_10 && A_1 != null && !a(A_1) && (A_13 == ParameterDirection.Input || A_13 == ParameterDirection.InputOutput))
					{
						byte[] array5 = ((A_0 != OracleDbType.Blob && A_0 != OracleDbType.BFile && !(A_1 is byte[]) && !(A_1 is char[])) ? a(b(A_1), a11.d() == 1000 || a11.p() == 2) : a(A_1, a11.d() == 1000 || a11.p() == 2));
						if (array5 != null)
						{
							((Stream)(A_3 = new OracleLob(A_12.Connection, null, 0, a11, A_0))).Write(array5, 0, array5.Length);
							return A_1;
						}
					}
					if (A_13 == ParameterDirection.Input)
					{
						return A_1;
					}
					if (flag)
					{
						return DBNull.Value;
					}
					OracleLob oracleLob3 = new OracleLob(A_12.Connection, null, 0, a11, A_0);
					A_3 = oracleLob3;
					if (OracleUtils.OracleClientCompatible)
					{
						return A_3;
					}
				}
			}
			else if (flag)
			{
				return DBNull.Value;
			}
			if (A_12.ReturnProviderSpecificTypes)
			{
				return A_3;
			}
			if (A_1 == null || A_1 == DBNull.Value)
			{
				A_2 = null;
			}
			else
			{
				A_2 = A_1.GetType();
			}
			return null;
		default:
			if (A_13 != ParameterDirection.Input)
			{
				if (flag)
				{
					return DBNull.Value;
				}
				switch (A_0)
				{
				case OracleDbType.Cursor:
				{
					s s2 = k2.a(A_4, A_6, A_2: false);
					if (s2.Equals(A_3) && A_1 is OracleCursor oracleCursor && s2.Equals(oracleCursor.Stmt))
					{
						if (oracleCursor.Stmt is a5 && ((a5)oracleCursor.Stmt).g == 1)
						{
							((a5)oracleCursor.Stmt).g = 2;
						}
						return A_1;
					}
					A_3 = null;
					return new OracleCursor(A_12.Connection, s2, A_12.FetchSize);
				}
				case OracleDbType.Char:
				case OracleDbType.NChar:
				case OracleDbType.NVarChar:
				case OracleDbType.RowId:
				case OracleDbType.VarChar:
				{
					string text;
					if (A_14.b())
					{
						text = (string)A_5[A_6];
						if (text == null)
						{
							if (A_12.ReturnProviderSpecificTypes)
							{
								return new OracleString("");
							}
							return null;
						}
					}
					else
					{
						int count = ((A_8 <= 0) ? a(A_4, A_6, A_11) : Devart.Common.e.c(A_4, A_8));
						Encoding encoding2 = ((!A_11) ? bl.a() : Encoding.Unicode);
						text = encoding2.GetString(A_4, A_6, count);
					}
					if ((A_0 == OracleDbType.Char || A_0 == OracleDbType.NChar) && A_12.Connection.TrimFixedChar)
					{
						text = text.TrimEnd(new char[1] { ' ' });
					}
					if (A_1 is char[])
					{
						return text.ToCharArray();
					}
					if (A_1 is OracleString)
					{
						return new OracleString(text);
					}
					if ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes)
					{
						return new OracleString(text);
					}
					return text;
				}
				case OracleDbType.Integer:
					return Devart.Common.e.g(A_4, A_6);
				case OracleDbType.Boolean:
					return Devart.Common.e.g(A_4, A_6) != 0;
				case OracleDbType.Double:
				case OracleDbType.Float:
					if (A_9 == 21)
					{
						return Devart.Common.e.e(A_4, A_6);
					}
					return Devart.Common.e.i(A_4, A_6);
				case OracleDbType.Number:
				{
					byte[] array = new byte[22];
					Buffer.BlockCopy(A_4, A_6, array, 0, 22);
					OracleNumber oracleNumber = new OracleNumber(array);
					A_3 = oracleNumber;
					if (A_1 is OracleNumber)
					{
						return oracleNumber;
					}
					if ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes)
					{
						return oracleNumber;
					}
					return oracleNumber.Value;
				}
				case OracleDbType.IntervalDS:
				{
					OracleIntervalDS oracleIntervalDS = k2.u(A_4, A_6, -1);
					A_3 = oracleIntervalDS;
					if (A_1 is OracleIntervalDS)
					{
						return oracleIntervalDS;
					}
					if (A_1 is OracleTimeSpan && OracleUtils.OracleClientCompatible)
					{
						return new OracleTimeSpan(oracleIntervalDS);
					}
					if ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes)
					{
						if (OracleUtils.OracleClientCompatible)
						{
							return new OracleTimeSpan(oracleIntervalDS);
						}
						return oracleIntervalDS;
					}
					return oracleIntervalDS.Value;
				}
				case OracleDbType.IntervalYM:
				{
					OracleIntervalYM oracleIntervalYM = k2.r(A_4, A_6, -1);
					A_3 = oracleIntervalYM;
					if (A_1 is OracleIntervalYM || A_1 == null || a(A_1))
					{
						return oracleIntervalYM;
					}
					if ((A_1 is OracleMonthSpan || A_1 == null || a(A_1)) && OracleUtils.OracleClientCompatible)
					{
						return new OracleMonthSpan(oracleIntervalYM);
					}
					long value = oracleIntervalYM.Value;
					if (A_1 is int)
					{
						return (int)value;
					}
					if (A_1 is long)
					{
						return value;
					}
					if (A_1 is double)
					{
						return (double)value;
					}
					if ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes)
					{
						if (OracleUtils.OracleClientCompatible)
						{
							return new OracleMonthSpan(oracleIntervalYM);
						}
						return oracleIntervalYM;
					}
					return oracleIntervalYM.Value;
				}
				case OracleDbType.TimeStamp:
				case OracleDbType.TimeStampLTZ:
				case OracleDbType.TimeStampTZ:
				{
					OracleTimeStamp oracleTimeStamp = k2.v(A_4, A_6, -1);
					A_3 = oracleTimeStamp;
					if (A_1 is OracleTimeStamp)
					{
						return oracleTimeStamp;
					}
					if (A_1 is OracleDateTime && OracleUtils.OracleClientCompatible)
					{
						return new OracleDateTime(oracleTimeStamp);
					}
					if (A_1 is OracleDate)
					{
						return new OracleDate(oracleTimeStamp.Value);
					}
					if ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes)
					{
						if (OracleUtils.OracleClientCompatible)
						{
							return new OracleDateTime(oracleTimeStamp);
						}
						return oracleTimeStamp;
					}
					return oracleTimeStamp.Value;
				}
				case OracleDbType.Raw:
				{
					int num = Devart.Common.e.h(A_4, A_8);
					byte[] array3 = new byte[num];
					Buffer.BlockCopy(A_4, A_6, array3, 0, num);
					A_3 = new OracleBinary(array3);
					if (A_1 is OracleBinary)
					{
						return A_3;
					}
					if ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes)
					{
						return A_3;
					}
					return array3;
				}
				case OracleDbType.Date:
				{
					byte[] array4 = new byte[7];
					Buffer.BlockCopy(A_4, A_6, array4, 0, 7);
					if (array4[2] == 0 && array4[3] == 0 && array4[4] == 0 && array4[5] == 0 && array4[6] == 0)
					{
						return DBNull.Value;
					}
					OracleDate oracleDate = ((string.Compare(A_14.v(), "08") >= 0) ? OracleDate.f(array4, 0) : OracleDate.e(array4, 0));
					A_3 = oracleDate;
					if (A_1 is OracleDate)
					{
						return oracleDate;
					}
					if (A_1 is OracleTimeStamp)
					{
						return new OracleTimeStamp(oracleDate.Value, ((OracleTimeStamp)A_1).TimeStampType);
					}
					if ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes)
					{
						return oracleDate;
					}
					return oracleDate.Value;
				}
				case OracleDbType.Object:
				{
					NativeOracleObject nativeOracleObject = A_3 as NativeOracleObject;
					if (nativeOracleObject == null)
					{
						nativeOracleObject = A_1 as NativeOracleObject;
					}
					bf bf2 = null;
					if (nativeOracleObject != null)
					{
						bf2 = nativeOracleObject.Instance;
					}
					bf bf3 = k2.a(A_4, A_6, A_2: false, bf2);
					if (bf3 != bf2)
					{
						if (nativeOracleObject != null && A_3 == nativeOracleObject)
						{
							nativeOracleObject.Dispose();
						}
						nativeOracleObject = (NativeOracleObject)(A_3 = ((bf3 == null) ? null : new NativeOracleObject(bf3, A_12.Connection, A_2: true)));
					}
					if (bf3 == null || bf3.get_IsNull())
					{
						return DBNull.Value;
					}
					if (A_1 is NativeOracleObject)
					{
						A_3 = null;
						return nativeOracleObject;
					}
					return OracleObject.a(bf3, A_12.Connection, A_2: false);
				}
				case OracleDbType.Array:
				case OracleDbType.Table:
				{
					NativeOracleArray nativeOracleArray = A_3 as NativeOracleArray;
					if (nativeOracleArray == null)
					{
						nativeOracleArray = A_1 as NativeOracleArray;
					}
					ak ak2 = null;
					if (nativeOracleArray != null)
					{
						ak2 = nativeOracleArray.Instance;
					}
					ak ak3 = k2.a(A_4, A_6, A_2: false, ak2);
					if (ak3 != ak2)
					{
						if (nativeOracleArray != null && A_3 == nativeOracleArray)
						{
							nativeOracleArray.Dispose();
						}
						nativeOracleArray = (NativeOracleArray)(A_3 = ((ak3 == null) ? null : new NativeOracleArray(ak3, A_12.Connection, A_2: true)));
					}
					if (ak3 == null || ak3.get_IsNull())
					{
						return DBNull.Value;
					}
					if (A_1 is NativeOracleArray)
					{
						A_3 = null;
						return nativeOracleArray;
					}
					if (A_1 is Array)
					{
						return nativeOracleArray.Value;
					}
					return OracleArray.a(ak3, A_12.Connection, A_2: false);
				}
				case OracleDbType.Long:
				case OracleDbType.LongRaw:
				{
					a6 a10 = (a6)A_3;
					if (a10 != null)
					{
						if (A_1 is byte[])
						{
							return a10.f();
						}
						if (A_1 is OracleBinary)
						{
							return new OracleBinary(a10.f());
						}
						byte[] array2 = a10.f();
						Encoding encoding = ((!A_11) ? bl.a() : Encoding.Unicode);
						if (A_1 is string)
						{
							return encoding.GetString(array2, 0, a10.h());
						}
						if (A_1 is char[])
						{
							return encoding.GetChars(array2, 0, a10.h());
						}
						if (A_1 is OracleString)
						{
							return new OracleString(encoding.GetString(array2, 0, a10.h()));
						}
						if (A_0 == OracleDbType.Long)
						{
							return encoding.GetString(array2, 0, a10.h());
						}
						if ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes)
						{
							return new OracleBinary(a10.f());
						}
						return array2;
					}
					return DBNull.Value;
				}
				case OracleDbType.Ref:
				{
					OracleRef oracleRef = A_1 as OracleRef;
					f f2 = oracleRef?.Handle;
					f f3 = k2.a(A_4, A_6, A_2: false, f2);
					if (oracleRef == null || (f2 != f3 && !Utils.ByteArrayEquals(f2.a(), f3.a())))
					{
						A_1 = new OracleRef(f3, A_15, A_12.Connection);
					}
					break;
				}
				case OracleDbType.Xml:
				{
					OracleXml oracleXml = A_1 as OracleXml;
					if (oracleXml == null)
					{
						oracleXml = A_3 as OracleXml;
					}
					al al2 = oracleXml?.XmlObject;
					al al3 = k2.a(A_4, A_6, A_2: false, al2);
					if (al3 != al2)
					{
						if (oracleXml != null && oracleXml == A_3)
						{
							oracleXml.Dispose();
							A_3 = null;
						}
						if (al3 == null)
						{
							return DBNull.Value;
						}
						oracleXml = new OracleXml(al3, A_12.Connection);
						if (A_1 is OracleXml || ((A_1 == null || A_1 == DBNull.Value) && A_12.ReturnProviderSpecificTypes))
						{
							return oracleXml;
						}
						A_3 = oracleXml;
						if (A_1 is char[])
						{
							return oracleXml.Value.ToCharArray();
						}
						return oracleXml.Value;
					}
					break;
				}
				case OracleDbType.AnyData:
				{
					b b2 = ((!(A_1 is OracleAnyData oracleAnyData)) ? null : oracleAnyData.AnyDataObject);
					b b3 = k2.a(A_4, A_6, A_2: false, b2);
					if (b3 == null)
					{
						return DBNull.Value;
					}
					if (b3 != b2)
					{
						A_1 = new OracleAnyData(b3, A_12.Connection);
					}
					break;
				}
				}
			}
			return A_1;
		}
	}

	internal void a(ref h A_0, bool A_1, bool A_2, OracleCommand A_3, byte[] A_4, Hashtable A_5, g A_6)
	{
		int num = ArrayLength;
		OracleDbType oracleDbType = OracleDbType;
		OracleDbType oracleDbType2 = oracleDbType;
		if (oracleDbType2 == OracleDbType.Array || oracleDbType2 == OracleDbType.Table)
		{
			num = 0;
		}
		g = null;
		if (num == 0)
		{
			a(oracleDbType, Value, A_4, A_5, A_0.l, A_0.n, A_0.o, A_0.c, A_1, A_2, A_3, Direction, A_6, this.m_c);
			if (a(oracleDbType, Direction))
			{
				Value = a(oracleDbType, Value, ref g, ref f, A_4, A_5, A_0.l, A_0.n, A_0.o, A_0.c, A_1, A_2, A_3, Direction, A_6, this.m_c);
			}
			if (Value == null || a(Value))
			{
				f = null;
			}
			return;
		}
		Array array = Value as Array;
		Array array2 = null;
		Array array3 = f as Array;
		Array array4 = null;
		Type type = null;
		int num2 = A_0.l;
		int num3 = A_0.n;
		int num4 = A_0.o;
		int num5 = A_0.c;
		int num6 = A_0.p;
		int num7 = A_0.q;
		int num8 = A_0.r;
		if (A_0.ab > 0)
		{
			num = A_4[A_0.ab] | (A_4[A_0.ab + 1] << 8);
		}
		if (this.m_e == null)
		{
			this.m_e = new bool[num];
		}
		if (array != null)
		{
			type = array.GetType().GetElementType();
			array2 = Array.CreateInstance(type, num);
			type = a(array);
		}
		for (int num9 = 0; num9 < num; num9++)
		{
			object a_ = array?.GetValue(num9);
			object A_7 = array3?.GetValue(num9);
			if (this.m_e[num9])
			{
				a_ = null;
				A_7 = null;
			}
			a(oracleDbType, a_, A_4, A_5, num2, num3, num4, num5, A_1, A_2, A_3, Direction, A_6, this.m_c);
			a_ = a(oracleDbType, a_, ref g, ref A_7, A_4, A_5, num2, num3, num4, num5, A_1, A_2, A_3, Direction, A_6, this.m_c);
			if (a_ == null && (oracleDbType == OracleDbType.Clob || oracleDbType == OracleDbType.NClob || oracleDbType == OracleDbType.Blob || oracleDbType == OracleDbType.BFile))
			{
				a_ = A_7;
			}
			if (a_ != null && a_ != DBNull.Value)
			{
				if (array2 == null)
				{
					type = a_.GetType();
					array2 = Array.CreateInstance(type, num);
				}
				if (A_7 != null)
				{
					if (array4 == null)
					{
						array4 = Array.CreateInstance(A_7.GetType(), num);
					}
					array4.SetValue(A_7, num9);
				}
				if (oracleDbType == OracleDbType.Clob || oracleDbType == OracleDbType.NClob || oracleDbType == OracleDbType.Blob || oracleDbType == OracleDbType.BFile)
				{
					if ((object)array2.GetType().GetElementType() == typeof(OracleLob))
					{
						array2.SetValue(a_, num9);
					}
					else if ((object)a_.GetType() == typeof(OracleLob))
					{
						array2.SetValue(((OracleLob)a_).Value, num9);
					}
					else
					{
						array2.SetValue(a_, num9);
					}
				}
				else
				{
					Type type2 = type;
					if (type2.IsGenericType && type2.IsValueType && type2.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
					{
						type2 = Nullable.GetUnderlyingType(type2) ?? type2;
					}
					object value = Convert.ChangeType(a_, type2, CultureInfo.CurrentCulture);
					array2.SetValue(value, num9);
				}
				this.m_e[num9] = false;
			}
			else
			{
				this.m_e[num9] = true;
			}
			num2 += num6;
			if (num3 >= 0)
			{
				num3 += num7;
			}
			if (num4 > 0)
			{
				num4 += num8;
			}
		}
		if (array2 == null)
		{
			array2 = Array.CreateInstance(OracleUtils.OracleDbTypeToType(oracleDbType), num);
		}
		base.Value = array2;
		f = array4;
	}

	private static void a(OracleDbType A_0, object A_1, byte[] A_2, Hashtable A_3, int A_4, int A_5, int A_6, int A_7, bool A_8, bool A_9, OracleCommand A_10, ParameterDirection A_11, g A_12, OracleType A_13)
	{
		if (A_12.b() || A_11 != ParameterDirection.Input)
		{
			return;
		}
		switch (A_0)
		{
		case OracleDbType.IntervalDS:
		case OracleDbType.IntervalYM:
		case OracleDbType.TimeStamp:
		case OracleDbType.TimeStampLTZ:
		case OracleDbType.TimeStampTZ:
		{
			bool flag = false;
			if (A_5 >= 0 && A_7 != 108)
			{
				flag = Devart.Common.e.h(A_2, A_5) != 0;
			}
			if (!flag || (A_0 != OracleDbType.IntervalDS && A_0 != OracleDbType.IntervalDS))
			{
				bg bg2 = (bg)A_12.b(A_7, (A_0 == OracleDbType.NChar || A_0 == OracleDbType.NClob) ? 2 : 0, null, A_13);
				bg2.a(A_3);
				bg2.a(A_10.Connection);
				bg2.c(A_2, A_4);
			}
			break;
		}
		}
	}

	private bool a(OracleDbType A_0, ParameterDirection A_1)
	{
		switch (A_0)
		{
		case OracleDbType.BFile:
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
		case OracleDbType.Xml:
			return true;
		default:
			return A_1 != ParameterDirection.Input;
		}
	}

	internal static int b(object A_0, IntPtr A_1, int A_2, out a6 A_3)
	{
		a6 a10 = null;
		if (A_0 != null)
		{
			if (A_0 is a6)
			{
				a10 = (a6)A_0;
			}
			else if (A_0 is Array)
			{
				Array array = (Array)A_0;
				a10 = (a6)array.GetValue(A_2);
			}
		}
		A_3 = a10;
		return -24200;
	}

	internal static int a(object A_0, IntPtr A_1, int A_2, out a6 A_3)
	{
		a6 a10 = null;
		if (A_0 != null)
		{
			if (A_0 is a6)
			{
				a10 = (a6)A_0;
			}
			else if (A_0 is Array)
			{
				Array array = (Array)A_0;
				a10 = (a6)array.GetValue(A_2);
			}
		}
		if (a10 != null)
		{
			a10.j();
			a10.c();
		}
		A_3 = a10;
		return -24200;
	}

	public new void CopyTo(DbParameter destination)
	{
		base.CopyTo(destination);
		((OracleParameter)destination).m_a = this.m_a;
		((OracleParameter)destination).m_b = this.m_b;
		((OracleParameter)destination).d = this.d;
	}

	public override void ResetDbType()
	{
		this.m_a = (OracleDbType)0;
	}

	internal void e()
	{
		this.m_a = (OracleDbType)0;
	}

	protected override int ValueSize(object value)
	{
		if (value != null && (object)value.GetType() == typeof(OracleString))
		{
			return ((OracleString)value).Length;
		}
		return base.ValueSize(value);
	}

	internal void a()
	{
		switch (OracleDbType)
		{
		case OracleDbType.Object:
			if (f != Value && f is NativeOracleObject nativeOracleObject)
			{
				nativeOracleObject.Dispose();
				f = null;
			}
			break;
		case OracleDbType.Array:
		case OracleDbType.Table:
			if (f != Value && f is NativeOracleArray nativeOracleArray)
			{
				nativeOracleArray.Dispose();
				f = null;
			}
			break;
		case OracleDbType.Ref:
			if (f != Value && f is f f2)
			{
				f2.Dispose();
				f = null;
			}
			break;
		case OracleDbType.Xml:
			if (f != Value && f is OracleXml oracleXml)
			{
				oracleXml.c();
			}
			break;
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
			if (f != Value && f is OracleLob oracleLob)
			{
				if (Direction == ParameterDirection.Input)
				{
					oracleLob.Dispose();
					f = null;
				}
				else
				{
					oracleLob.MakeDisconnected();
				}
			}
			break;
		}
	}

	private static bool a(object A_0)
	{
		return Convert.IsDBNull(A_0);
	}
}
