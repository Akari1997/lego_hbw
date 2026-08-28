using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Devart.Common;

public abstract class DbDataReaderBase : DbDataReader
{
	private CommandBehavior a;

	private Hashtable b;

	protected bool closed;

	protected DataTable schemaTable;

	protected internal CommandBehavior CommandBehavior => a;

	public override int Depth
	{
		get
		{
			AssertReaderIsOpen("Depth");
			return 0;
		}
	}

	public override int FieldCount
	{
		get
		{
			AssertReaderIsOpen("FieldCount");
			return 0;
		}
	}

	public override bool HasRows
	{
		get
		{
			AssertReaderIsOpen("HasRows");
			return true;
		}
	}

	public override bool IsClosed => closed;

	public abstract bool EndOfData { get; }

	protected abstract bool IsValidRow { get; }

	public override object this[string name]
	{
		get
		{
			int ordinal = GetOrdinal(name);
			return GetValue(ordinal);
		}
	}

	public override object this[int ordinal] => GetValue(ordinal);

	public override int RecordsAffected => 0;

	protected DbDataReaderBase(CommandBehavior behavior)
	{
		a = behavior;
	}

	protected void AssertReaderHasColumns()
	{
		if (FieldCount <= 0)
		{
			throw new InvalidOperationException(Devart.Common.n.a("DataReaderNoData"));
		}
	}

	protected void AssertReaderHasData()
	{
		if (!IsValidRow)
		{
			throw new InvalidOperationException(Devart.Common.n.a("DataReaderNoData"));
		}
	}

	protected void AssertReaderIsOpen(string methodName)
	{
		if (closed)
		{
			throw new InvalidOperationException(Devart.Common.n.a("DataReaderClosed", methodName));
		}
	}

	public override void Close()
	{
		b = null;
		schemaTable = null;
		closed = true;
		GC.SuppressFinalize(this);
	}

	protected static DataTable CreateSchemaTable(int columnCount)
	{
		DataTable dataTable = new DataTable("SchemaTable");
		dataTable.Locale = CultureInfo.InvariantCulture;
		dataTable.MinimumCapacity = columnCount;
		DataColumn column = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
		DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
		DataColumn column2 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
		DataColumn column3 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
		DataColumn column4 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
		DataColumn column5 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
		DataColumn column6 = new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
		DataColumn column7 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
		DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
		DataColumn column8 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
		DataColumn column9 = new DataColumn(SchemaTableColumn.IsAliased, typeof(bool));
		DataColumn column10 = new DataColumn(SchemaTableColumn.IsExpression, typeof(bool));
		DataColumn column11 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
		DataColumn column12 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
		DataColumn column13 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
		DataColumn column14 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
		DataColumn column15 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
		dataColumn.DefaultValue = 0;
		dataColumn2.DefaultValue = false;
		DataColumnCollection columns = dataTable.Columns;
		columns.Add(column);
		columns.Add(dataColumn);
		columns.Add(column2);
		columns.Add(column3);
		columns.Add(column4);
		columns.Add(column5);
		columns.Add(column6);
		columns.Add(column7);
		columns.Add(dataColumn2);
		columns.Add(column8);
		columns.Add(column9);
		columns.Add(column10);
		columns.Add(column11);
		columns.Add(column12);
		columns.Add(column13);
		columns.Add(column14);
		columns.Add(column15);
		for (int i = 0; i < columns.Count; i++)
		{
			columns[i].ReadOnly = true;
		}
		return dataTable;
	}

	protected virtual void FillSchemaTable(DataTable dataTable)
	{
		throw new NotSupportedException("FillSchemaTable");
	}

	public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
	{
		AssertReaderIsOpen("GetBytes");
		AssertReaderHasData();
		if (fieldOffset < 0 || fieldOffset > int.MaxValue)
		{
			throw new ArgumentException(Devart.Common.n.a("InvalidSourceBufferIndex", fieldOffset), "fieldOffset");
		}
		if (bufferOffset < 0)
		{
			throw new ArgumentException(Devart.Common.n.a("InvalidDestinationBufferIndex", bufferOffset), "bufferOffset");
		}
		if (length < 0)
		{
			throw new ArgumentException(Devart.Common.n.a("InvalidBufferSizeOrIndex", length, ordinal));
		}
		byte[] array = (byte[])GetValue(ordinal);
		if (buffer == null)
		{
			return array.Length;
		}
		int num = (int)Math.Min(array.Length - fieldOffset, length);
		Buffer.BlockCopy(array, (int)fieldOffset, buffer, bufferOffset, num);
		return num;
	}

	public override long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
	{
		AssertReaderIsOpen("GetChars");
		AssertReaderHasData();
		if (fieldOffset < 0 || fieldOffset > int.MaxValue)
		{
			throw new ArgumentException(Devart.Common.n.a("InvalidSourceBufferIndex", fieldOffset), "fieldOffset");
		}
		if (bufferOffset < 0)
		{
			throw new ArgumentException(Devart.Common.n.a("InvalidDestinationBufferIndex", bufferOffset), "bufferOffset");
		}
		if (length < 0)
		{
			throw new ArgumentException(Devart.Common.n.a("InvalidBufferSizeOrIndex", length, ordinal));
		}
		string text = GetString(ordinal);
		if (buffer == null)
		{
			return text.Length;
		}
		int num = (int)Math.Min(text.Length - fieldOffset, length);
		text.CopyTo((int)fieldOffset, buffer, bufferOffset, num);
		return num;
	}

	public override string GetDataTypeName(int ordinal)
	{
		throw new NotSupportedException("GetDataTypeName");
	}

	public override IEnumerator GetEnumerator()
	{
		return new DbEnumerator((IDataReader)this, IsCommandBehavior(CommandBehavior.CloseConnection));
	}

	public override Type GetFieldType(int ordinal)
	{
		throw new NotSupportedException("GetFieldType");
	}

	public override string GetName(int ordinal)
	{
		throw new NotSupportedException("GetName");
	}

	public override int GetOrdinal(string name)
	{
		Utils.CheckArgumentNull(name, "name");
		AssertReaderIsOpen("GetOrdinal");
		AssertReaderHasColumns();
		int fieldCount = FieldCount;
		if (b == null)
		{
			b = Utils.CreateHashtable(ignoreCase: true);
			for (int i = 0; i < fieldCount; i++)
			{
				string name2 = GetName(i);
				if (!b.Contains(name2))
				{
					b.Add(name2, i);
				}
			}
		}
		object obj = b[name];
		if (obj == null)
		{
			throw new IndexOutOfRangeException(Devart.Common.n.a("IndexOutOfRange", name));
		}
		return (int)obj;
	}

	public override DataTable GetSchemaTable()
	{
		DataTable dataTable = schemaTable;
		if (dataTable == null)
		{
			AssertReaderIsOpen("GetSchemaTable");
			if (FieldCount > 0)
			{
				dataTable = CreateSchemaTable(FieldCount);
				FillSchemaTable(dataTable);
				schemaTable = dataTable;
			}
			else if (FieldCount <= 0)
			{
				schemaTable = null;
			}
		}
		return dataTable;
	}

	public override object GetValue(int ordinal)
	{
		throw new NotSupportedException("GetValue");
	}

	public override int GetValues(object[] values)
	{
		Utils.CheckArgumentNull(values, "values");
		AssertReaderIsOpen("GetValues");
		AssertReaderHasData();
		int num = Math.Min(values.Length, FieldCount);
		for (int i = 0; i < num; i++)
		{
			values[i] = GetValue(i);
		}
		return num;
	}

	public bool GetBoolean(string name)
	{
		return GetBoolean(GetOrdinal(name));
	}

	public byte GetByte(string name)
	{
		return GetByte(GetOrdinal(name));
	}

	public long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length)
	{
		return GetBytes(GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
	}

	public char GetChar(string name)
	{
		return GetChar(GetOrdinal(name));
	}

	public long GetChars(string name, long fieldOffset, char[] buffer, int bufferOffset, int length)
	{
		return GetChars(GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
	}

	public string GetDataTypeName(string name)
	{
		return GetDataTypeName(GetOrdinal(name));
	}

	public DateTime GetDateTime(string name)
	{
		return GetDateTime(GetOrdinal(name));
	}

	public DateTimeOffset GetDateTimeOffset(string name)
	{
		return GetDateTimeOffset(GetOrdinal(name));
	}

	public decimal GetDecimal(string name)
	{
		return GetDecimal(GetOrdinal(name));
	}

	public double GetDouble(string name)
	{
		return GetDouble(GetOrdinal(name));
	}

	public Type GetFieldType(string name)
	{
		return GetFieldType(GetOrdinal(name));
	}

	public float GetFloat(string name)
	{
		return GetFloat(GetOrdinal(name));
	}

	public Guid GetGuid(string name)
	{
		return GetGuid(GetOrdinal(name));
	}

	public short GetInt16(string name)
	{
		return GetInt16(GetOrdinal(name));
	}

	public int GetInt32(string name)
	{
		return GetInt32(GetOrdinal(name));
	}

	public long GetInt64(string name)
	{
		return GetInt64(GetOrdinal(name));
	}

	public Type GetProviderSpecificFieldType(string name)
	{
		return GetProviderSpecificFieldType(GetOrdinal(name));
	}

	public object GetProviderSpecificValue(string name)
	{
		return GetProviderSpecificValue(GetOrdinal(name));
	}

	public string GetString(string name)
	{
		return GetString(GetOrdinal(name));
	}

	public object GetValue(string name)
	{
		return GetValue(GetOrdinal(name));
	}

	public bool IsDBNull(string name)
	{
		return IsDBNull(GetOrdinal(name));
	}

	protected bool IsCommandBehavior(CommandBehavior condition)
	{
		return condition == (condition & a);
	}

	public override bool IsDBNull(int ordinal)
	{
		AssertReaderIsOpen("IsDBNull");
		AssertReaderHasData();
		object value = GetValue(ordinal);
		return Convert.IsDBNull(value);
	}

	public override bool NextResult()
	{
		AssertReaderIsOpen("NextResult");
		b = null;
		schemaTable = null;
		return false;
	}

	public override bool Read()
	{
		AssertReaderIsOpen("Read");
		return false;
	}

	protected internal void SetCommandBehavior(CommandBehavior commandBehavior)
	{
		if ((commandBehavior & CommandBehavior.KeyInfo) == CommandBehavior.KeyInfo && !IsCommandBehavior(CommandBehavior.KeyInfo))
		{
			schemaTable = null;
		}
		a = commandBehavior;
	}

	internal void n()
	{
		Close();
	}

	public virtual DateTimeOffset GetDateTimeOffset(int ordinal)
	{
		DateTime dateTime = GetDateTime(ordinal);
		return new DateTimeOffset(dateTime);
	}
}
