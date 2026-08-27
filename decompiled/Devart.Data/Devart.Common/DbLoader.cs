using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Devart.Common;

public abstract class DbLoader : Component
{
	private DbConnection m_a;

	private string b;

	private DbLoaderColumnCollection c;

	protected static readonly object errorEventKey = new object();

	protected bool isOpened;

	protected int loaderBufferSize = 262144;

	[MergableProperty(false)]
	public DbConnection Connection
	{
		get
		{
			return this.m_a;
		}
		set
		{
			if (this.m_a != value)
			{
				this.m_a = value;
			}
		}
	}

	[Category("Options")]
	[r("DbLoader_BufferSize")]
	[DefaultValue(262144)]
	public int BufferSize
	{
		get
		{
			return loaderBufferSize;
		}
		set
		{
			loaderBufferSize = value;
		}
	}

	[RefreshProperties(RefreshProperties.Repaint)]
	[MergableProperty(false)]
	[r("DbLoader_TableName")]
	[Category("Data")]
	public virtual string TableName
	{
		get
		{
			if (Utils.IsEmpty(b))
			{
				return string.Empty;
			}
			return b;
		}
		set
		{
			if (b != value)
			{
				b = value;
			}
		}
	}

	[r("DbLoader_Columns")]
	[Category("Data")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[MergableProperty(false)]
	public DbLoaderColumnCollection Columns => c;

	public object this[string columnName]
	{
		set
		{
			SetValue(columnName, value);
		}
	}

	public object this[int columnIndex]
	{
		set
		{
			SetValue(columnIndex, value);
		}
	}

	public DbLoader()
		: this(string.Empty)
	{
	}

	public DbLoader(string tableName)
		: this(tableName, null)
	{
	}

	public DbLoader(string tableName, DbConnection connection)
	{
		TableName = tableName;
		Connection = connection;
		c = InitColumns();
	}

	internal void a()
	{
		Dispose(disposing: false);
	}

	public void SetValue(string name, object value)
	{
		SetValue(GetColumnIndex(name), value);
	}

	public void SetNull(int i)
	{
		SetValue(i, null);
	}

	public void SetNull(string name)
	{
		SetValue(name, null);
	}

	protected void CheckOpen()
	{
		if (!isOpened)
		{
			throw new InvalidOperationException(n.a("DbLoader_CallOpenFirst"));
		}
	}

	protected void CheckConnection()
	{
		if (this.m_a == null)
		{
			throw new InvalidOperationException(n.a("DbLoader_ConnectionWasNotInitialized"));
		}
		if (this.m_a.State != ConnectionState.Open)
		{
			throw new InvalidOperationException(n.a("DbLoader_ConnectionWasNotOpened"));
		}
	}

	protected void CheckTableName()
	{
		if (Utils.IsEmpty(b))
		{
			throw new InvalidOperationException(n.a("DbLoader_TableWasNotInitialized"));
		}
	}

	protected int GetColumnIndex(string name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		name = UnQuote(name);
		int num = -1;
		for (int i = 0; i < c.Count; i++)
		{
			if (Utils.Compare(name, UnQuote(c[i].Name), ignoreCase: false))
			{
				num = i;
				break;
			}
		}
		if (num > -1)
		{
			return num;
		}
		for (int num2 = 0; num2 < c.Count; num2++)
		{
			if (Utils.Compare(name, UnQuote(c[num2].Name), ignoreCase: true))
			{
				if (num != -1)
				{
					num = -2;
					break;
				}
				num = num2;
			}
		}
		return num switch
		{
			-1 => throw new ArgumentException(string.Format(n.a("DbLoader_ColunmWithNameDoesNotExist"), name)), 
			-2 => throw new ArgumentException(string.Format(n.a("DbLoader_TwoColunmsExist"), name)), 
			_ => num, 
		};
	}

	public void LoadTable(DataTable table)
	{
		LoadTable(table, null);
	}

	protected abstract string QuoteIfNeed(string name);

	protected abstract string UnQuote(string name);

	public void LoadTable(DataTable table, IColumnMappingCollection columnMappings)
	{
		if (table == null)
		{
			throw new ArgumentNullException("table");
		}
		if (Columns.Count == 0)
		{
			if (columnMappings != null)
			{
				foreach (IColumnMapping columnMapping in columnMappings)
				{
					Columns.Add(CreateColumn(columnMapping.SourceColumn, table.Columns[columnMapping.DataSetColumn].DataType));
				}
			}
			else
			{
				foreach (DataColumn column in table.Columns)
				{
					Columns.Add(CreateColumn(column.ColumnName, column.DataType));
				}
			}
		}
		bool flag = !isOpened;
		if (!isOpened)
		{
			Open();
		}
		try
		{
			LoadTableInternal(table, columnMappings);
		}
		finally
		{
			if (flag)
			{
				Close();
			}
		}
	}

	public abstract void Open();

	public abstract void Close();

	public abstract void NextRow();

	public abstract void CreateColumns();

	public abstract void SetValue(int i, object value);

	protected virtual void LoadTableInternal(DataTable table, IColumnMappingCollection columnMappings)
	{
		if (columnMappings != null)
		{
			foreach (DataRow row in table.Rows)
			{
				foreach (IColumnMapping columnMapping in columnMappings)
				{
					SetValue(columnMapping.SourceColumn, row[columnMapping.DataSetColumn]);
				}
				NextRow();
			}
			return;
		}
		foreach (DataRow row2 in table.Rows)
		{
			foreach (DataColumn column in table.Columns)
			{
				SetValue(column.ColumnName, row2[column]);
			}
			NextRow();
		}
	}

	protected abstract DbLoaderColumnCollection InitColumns();

	protected abstract DbLoaderColumn CreateColumn(string name, Type type);
}
