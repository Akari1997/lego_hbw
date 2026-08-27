using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleArrayDataReader : MarshalByRefObject, IDataReader, IEnumerable
{
	private const string m_a = "OracleDataReader is closed.";

	private const string m_b = "No data exists for the row/column.";

	private const string m_c = "Index was outside the bounds of the array.";

	private OracleArray m_d;

	private bool e;

	private int f;

	private int g;

	private DataTable h;

	public int Depth
	{
		get
		{
			c();
			return 0;
		}
	}

	public bool IsClosed => e;

	public int RecordsAffected => this.m_d.Count;

	private OracleAttributeCollection Attributes
	{
		get
		{
			OracleAttribute oracleAttribute = this.m_d.ObjectType.g;
			if (oracleAttribute.DbType == OracleDbType.Object)
			{
				return oracleAttribute.h.Attributes;
			}
			return null;
		}
	}

	public int FieldCount
	{
		get
		{
			c();
			return Attributes?.Count ?? 1;
		}
	}

	public object this[string name] => GetValue(GetOrdinal(name));

	public object this[int i] => GetValue(i);

	internal OracleArrayDataReader(OracleArray A_0)
	{
		this.m_d = A_0;
		d();
	}

	~OracleArrayDataReader()
	{
		a(A_0: false);
	}

	void IDisposable.Dispose()
	{
		a(A_0: true);
		GC.SuppressFinalize(this);
	}

	private void a(bool A_0)
	{
		if (!e)
		{
			e = true;
			if (A_0)
			{
				h = null;
			}
		}
	}

	private void c()
	{
		if (e)
		{
			throw new InvalidOperationException("OracleDataReader is closed.");
		}
	}

	private void b()
	{
		if (f >= g)
		{
			throw new InvalidOperationException("No data exists for the row/column.");
		}
	}

	private void b(int A_0)
	{
		c();
		if (A_0 >= FieldCount)
		{
			throw new IndexOutOfRangeException("Index was outside the bounds of the array.");
		}
	}

	internal void d()
	{
		f = -1;
		g = -1;
	}

	public void Close()
	{
		a(A_0: true);
	}

	public DataTable GetSchemaTable()
	{
		OracleDataReader oracleDataReader = null;
		try
		{
			c();
			if (FieldCount == 0)
			{
				return null;
			}
			if (h != null)
			{
				return h;
			}
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.MinimumCapacity = FieldCount;
			DataColumnCollection columns = dataTable.Columns;
			DataColumn column = columns.Add("ColumnName", typeof(string));
			DataColumn column2 = columns.Add("ColumnOrdinal", typeof(int));
			DataColumn column3 = columns.Add("ColumnSize", typeof(int));
			DataColumn column4 = columns.Add("NumericPrecision", typeof(short));
			DataColumn column5 = columns.Add("NumericScale", typeof(short));
			DataColumn column6 = columns.Add("DataType", typeof(object));
			DataColumn column7 = columns.Add("ProviderType", typeof(int));
			DataColumn column8 = columns.Add("IsLong", typeof(bool));
			DataColumn column9 = columns.Add("AllowDBNull", typeof(bool));
			DataColumn column10 = columns.Add("IsReadOnly", typeof(bool));
			DataColumn column11 = columns.Add("IsRowVersion", typeof(bool));
			columns.Add("IsUnique", typeof(bool));
			columns.Add("IsKey", typeof(bool));
			DataColumn column12 = columns.Add("IsAutoIncrement", typeof(bool));
			columns.Add("BaseSchemaName", typeof(string));
			columns.Add("BaseCatalogName", typeof(string));
			columns.Add("BaseTableName", typeof(string));
			DataColumn column13 = columns.Add("BaseColumnName", typeof(string));
			columns.Add("IsAliased", typeof(bool));
			columns.Add("IsExpression", typeof(bool));
			DataColumn column14 = columns.Add("TypeName", typeof(string));
			DataColumn column15 = columns.Add("TypeSchemaName", typeof(string));
			OracleAttributeCollection oracleAttributeCollection = Attributes;
			if (oracleAttributeCollection.Count == 0)
			{
				return h = dataTable;
			}
			for (int num = 0; num < FieldCount; num++)
			{
				DataRow dataRow = dataTable.NewRow();
				OracleDbType oracleDbType = oracleAttributeCollection[num].g;
				dataRow[column] = GetName(num);
				dataRow[column2] = num;
				dataRow[column4] = oracleAttributeCollection[num].d;
				dataRow[column5] = oracleAttributeCollection[num].e;
				dataRow[column6] = GetFieldType(num);
				dataRow[column7] = (int)oracleDbType;
				dataRow[column9] = true;
				if (oracleAttributeCollection[num].h != null)
				{
					dataRow[column14] = oracleAttributeCollection[num].h.d;
					dataRow[column15] = oracleAttributeCollection[num].h.c;
				}
				else
				{
					dataRow[column14] = GetDataTypeName(num);
					dataRow[column15] = "";
				}
				bool flag = oracleDbType == OracleDbType.Clob || oracleDbType == OracleDbType.Blob || oracleDbType == OracleDbType.BFile || oracleDbType == OracleDbType.Long || oracleDbType == OracleDbType.LongRaw || oracleDbType == OracleDbType.NClob;
				dataRow[column8] = flag;
				if (flag)
				{
					dataRow[column3] = int.MaxValue;
				}
				else
				{
					dataRow[column3] = oracleAttributeCollection[num].c;
				}
				if (oracleDbType == OracleDbType.RowId)
				{
					dataRow[column11] = true;
					dataRow[column12] = true;
				}
				else
				{
					dataRow[column11] = false;
					dataRow[column12] = false;
				}
				dataRow[column10] = false;
				dataRow[column13] = GetName(num);
				dataTable.Rows.Add(dataRow);
			}
			h = dataTable;
		}
		finally
		{
			oracleDataReader?.Close();
		}
		return h;
	}

	public bool NextResult()
	{
		return false;
	}

	public bool Read()
	{
		g = this.m_d.Count;
		if (f < g - 1)
		{
			f++;
			return true;
		}
		return false;
	}

	private OracleAttribute a(int A_0)
	{
		OracleAttributeCollection oracleAttributeCollection = Attributes;
		if (oracleAttributeCollection == null)
		{
			return this.m_d.ObjectType.g;
		}
		return Attributes[A_0];
	}

	public bool GetBoolean(int i)
	{
		throw new NotSupportedException();
	}

	public byte GetByte(int i)
	{
		throw new NotSupportedException();
	}

	public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
	{
		OracleLob oracleLob = (OracleLob)GetOracleValue(i);
		oracleLob.Position = fieldOffset;
		return oracleLob.Read(buffer, bufferoffset, length);
	}

	public char GetChar(int i)
	{
		throw new NotSupportedException();
	}

	public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
	{
		OracleLob stream = (OracleLob)GetOracleValue(i);
		try
		{
			StreamReader streamReader = new StreamReader(stream, Encoding.Unicode);
			return streamReader.ReadBlock(buffer, bufferoffset, length);
		}
		finally
		{
			StreamReader streamReader = null;
		}
	}

	public IDataReader GetData(int i)
	{
		object oracleValue = GetOracleValue(i);
		if (oracleValue is OracleCursor)
		{
			return ((OracleCursor)oracleValue).GetDataReader();
		}
		throw new ArgumentException("Cannot convert");
	}

	public string GetDataTypeName(int i)
	{
		b(i);
		return a(i).TypeName;
	}

	public DateTime GetDateTime(int i)
	{
		object oracleValue = GetOracleValue(i);
		if (oracleValue is OracleTimeStamp oracleTimeStamp)
		{
			return oracleTimeStamp.Value;
		}
		return Convert.ToDateTime(oracleValue);
	}

	public decimal GetDecimal(int i)
	{
		object oracleValue = GetOracleValue(i);
		if (oracleValue is OracleNumber oracleNumber)
		{
			return oracleNumber.Value;
		}
		return Convert.ToDecimal(oracleValue);
	}

	public double GetDouble(int i)
	{
		object oracleValue = GetOracleValue(i);
		if (oracleValue is OracleNumber)
		{
			return OracleNumber.b((OracleNumber)oracleValue);
		}
		return Convert.ToDouble(oracleValue);
	}

	public Type GetFieldType(int i)
	{
		b(i);
		return OracleUtils.OracleDbTypeToType(a(i).DbType);
	}

	public float GetFloat(int i)
	{
		object oracleValue = GetOracleValue(i);
		if (oracleValue is OracleNumber)
		{
			return OracleNumber.a((OracleNumber)oracleValue);
		}
		return Convert.ToSingle(oracleValue);
	}

	public Guid GetGuid(int i)
	{
		throw new NotSupportedException();
	}

	public short GetInt16(int i)
	{
		object oracleValue = GetOracleValue(i);
		if (oracleValue is OracleNumber)
		{
			return (short)OracleNumber.d((OracleNumber)oracleValue);
		}
		return Convert.ToInt16(oracleValue);
	}

	public int GetInt32(int i)
	{
		object oracleValue = GetOracleValue(i);
		if (oracleValue is OracleNumber)
		{
			return OracleNumber.d((OracleNumber)oracleValue);
		}
		return Convert.ToInt32(oracleValue);
	}

	public long GetInt64(int i)
	{
		object oracleValue = GetOracleValue(i);
		if (oracleValue is OracleNumber)
		{
			return OracleNumber.c((OracleNumber)oracleValue);
		}
		return Convert.ToInt64(GetOracleValue(i));
	}

	public string GetName(int i)
	{
		b(i);
		return a(i).a;
	}

	public int GetOrdinal(string name)
	{
		c();
		for (int num = 0; num < Attributes.Count; num++)
		{
			if (Utils.Compare(name, a(num).Name))
			{
				return num;
			}
		}
		throw new IndexOutOfRangeException();
	}

	public object GetValue(int i)
	{
		b();
		b(i);
		object obj = this.m_d[f];
		if (obj is OracleObject oracleObject)
		{
			obj = oracleObject[a(i)];
		}
		if (obj is OracleLob)
		{
			return ((OracleLob)obj).Value;
		}
		if (obj is OracleCursor)
		{
			return ((OracleCursor)obj).GetDataReader();
		}
		return obj;
	}

	public OracleBFile GetOracleBFile(int i)
	{
		return (OracleBFile)GetOracleValue(i);
	}

	public OracleCursor GetOracleCursor(int i)
	{
		return (OracleCursor)GetOracleValue(i);
	}

	public OracleBinary GetOracleBinary(int i)
	{
		return (OracleBinary)GetOracleValue(i);
	}

	public OracleDate GetOracleDate(int i)
	{
		return (OracleDate)GetOracleValue(i);
	}

	[Obsolete("This method is designed for compatibility with OracleClient only.")]
	public OracleTimeSpan GetOracleTimeSpan(int i)
	{
		OracleUtils.a("GetOracleTimeSpan");
		return (OracleTimeSpan)GetOracleValue(i);
	}

	[Obsolete("This method is designed for compatibility with OracleClient only.")]
	public OracleMonthSpan GetOracleMonthSpan(int i)
	{
		OracleUtils.a("GetOracleMonthSpan");
		return (OracleMonthSpan)GetOracleValue(i);
	}

	public OracleIntervalDS GetOracleIntervalDS(int i)
	{
		return (OracleIntervalDS)GetOracleValue(i);
	}

	public OracleIntervalYM GetOracleIntervalYM(int i)
	{
		return (OracleIntervalYM)GetOracleValue(i);
	}

	public OracleLob GetOracleLob(int i)
	{
		return (OracleLob)GetOracleValue(i);
	}

	public OracleNumber GetOracleNumber(int i)
	{
		return (OracleNumber)GetOracleValue(i);
	}

	public OracleObject GetOracleObject(int i)
	{
		return (OracleObject)GetOracleValue(i);
	}

	public OracleArray GetOracleArray(int i)
	{
		return (OracleArray)GetOracleValue(i);
	}

	public OracleTable GetOracleTable(int i)
	{
		return (OracleTable)GetOracleValue(i);
	}

	public OracleRef GetOracleRef(int i)
	{
		return (OracleRef)GetOracleValue(i);
	}

	public OracleString GetOracleString(int i)
	{
		return (OracleString)GetOracleValue(i);
	}

	public object GetOracleValue(int i)
	{
		b();
		b(i);
		object obj = this.m_d[f];
		if (obj is OracleObject oracleObject)
		{
			return oracleObject.GetOracleValue(a(i));
		}
		return obj;
	}

	public int GetValues(object[] values)
	{
		int num = Math.Min(values.Length, FieldCount);
		for (int num2 = 0; num2 < num; num2++)
		{
			values[num2] = GetValue(num2);
		}
		return num;
	}

	public bool IsDBNull(int i)
	{
		return GetOracleValue(i) == DBNull.Value;
	}

	public string GetString(int i)
	{
		return OracleUtils.OracleValueToString(GetOracleValue(i));
	}

	public IEnumerator GetEnumerator()
	{
		return new DbEnumerator(this, closeReader: false);
	}
}
