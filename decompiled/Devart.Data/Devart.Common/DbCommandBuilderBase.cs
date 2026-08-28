using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Devart.Common;

public abstract class DbCommandBuilderBase : System.Data.Common.DbCommandBuilder
{
	private MissingMappingAction m_a;

	protected DataRow[] schemaRows;

	private bool m_b;

	private DataRow[] m_c;

	protected DataTable schemaTable;

	protected string tableName;

	private IDbCommand d;

	protected RefreshRowMode refreshRowMode;

	protected string[] refreshingFieldsList;

	private bool e = true;

	private bool f = true;

	private string g;

	protected string[] keyFieldsList;

	private bool h;

	[DefaultValue("")]
	[r("DbCommandBuilder_UpdatingTable")]
	public string UpdatingTable
	{
		get
		{
			if (g == null)
			{
				return string.Empty;
			}
			return g;
		}
		set
		{
			if (g != value)
			{
				g = value;
				RefreshSchema();
			}
		}
	}

	[DefaultValue("")]
	[r("DbCommandBuilder_KeyFields")]
	public string KeyFields
	{
		get
		{
			if (keyFieldsList == null)
			{
				return string.Empty;
			}
			return string.Join(";", keyFieldsList);
		}
		set
		{
			if (KeyFields != value)
			{
				if (Utils.IsEmpty(value))
				{
					keyFieldsList = null;
				}
				else
				{
					keyFieldsList = value.Split(new char[1] { ';' });
				}
				RefreshSchema();
			}
		}
	}

	[r("DbCommandBuilder_Quoted")]
	[DefaultValue(false)]
	[Category("Behavior")]
	public virtual bool Quoted
	{
		get
		{
			return h;
		}
		set
		{
			h = value;
		}
	}

	[Category("Update")]
	[DefaultValue(RefreshRowMode.None)]
	[r("DbCommandBuilder_RefreshMode")]
	public RefreshRowMode RefreshMode
	{
		get
		{
			return refreshRowMode;
		}
		set
		{
			refreshRowMode = value;
		}
	}

	protected abstract char[] QuoteSymbols { get; }

	[DefaultValue("")]
	[r("DbCommandBuilder_RefreshingFields")]
	[Category("Update")]
	public virtual string RefreshingFields
	{
		get
		{
			if (refreshingFieldsList == null)
			{
				return string.Empty;
			}
			return string.Join(";", refreshingFieldsList);
		}
		set
		{
			if (RefreshingFields != value)
			{
				if (Utils.IsEmpty(value))
				{
					refreshingFieldsList = null;
				}
				else
				{
					refreshingFieldsList = Utils.SplitItems(value, QuoteSymbols);
				}
				RefreshSchema();
			}
		}
	}

	[Category("Schema")]
	[r("DbCommandBuilder_UseSchema")]
	[DefaultValue(true)]
	public virtual bool UseSchema
	{
		get
		{
			return e;
		}
		set
		{
			if (e != value)
			{
				e = value;
				RefreshSchema();
			}
		}
	}

	[r("DbCommandBuilder_UseCatalog")]
	[Category("Schema")]
	[DefaultValue(true)]
	public virtual bool UseCatalog
	{
		get
		{
			return f;
		}
		set
		{
			if (f != value)
			{
				f = value;
				RefreshSchema();
			}
		}
	}

	private void c()
	{
		schemaTable = null;
		schemaRows = null;
		tableName = null;
		this.m_c = null;
	}

	protected virtual string GetParameterPlaceholder(string parameterName)
	{
		return ':' + parameterName;
	}

	protected virtual IDbCommand BuildNewCommand(IDbCommand command)
	{
		IDbCommand dbCommand = null;
		if (base.DataAdapter != null)
		{
			dbCommand = base.DataAdapter.SelectCommand;
		}
		if (command == null)
		{
			command = dbCommand.Connection.CreateCommand();
			command.CommandTimeout = dbCommand.CommandTimeout;
			command.Transaction = dbCommand.Transaction;
		}
		else
		{
			command.Parameters.Clear();
		}
		command.CommandType = CommandType.Text;
		if (RefreshMode != RefreshRowMode.None)
		{
			command.UpdatedRowSource = UpdateRowSource.Both;
		}
		else
		{
			command.UpdatedRowSource = UpdateRowSource.None;
		}
		return command;
	}

	protected override DbCommand InitializeCommand(DbCommand command)
	{
		DbCommand dbCommand = base.InitializeCommand(command);
		if (RefreshMode != RefreshRowMode.None)
		{
			if (base.DataAdapter != null && base.DataAdapter.UpdateBatchSize > 1)
			{
				throw new InvalidOperationException("When batching, the RefreshMode property value of RefreshRowMode.AfterInsert, RefreshRowMode.AfterUpdate or RefreshRowMode.Both is invalid.");
			}
			dbCommand.UpdatedRowSource = UpdateRowSource.Both;
		}
		return dbCommand;
	}

	public IDbCommand GetRefreshCommand()
	{
		return GetRefreshCommand(null, useColumnsForParameterNames: false);
	}

	public IDbCommand GetRefreshCommand(bool useColumnsForParameterNames)
	{
		return GetRefreshCommand(null, useColumnsForParameterNames);
	}

	public IDbCommand GetRefreshCommand(string[] fields, bool useColumnsForParameterNames)
	{
		return a(fields, null, null, useColumnsForParameterNames, null);
	}

	internal IDbCommand a(string[] A_0, DataTableMapping A_1, DataRow A_2, bool A_3, IDataParameterCollection A_4)
	{
		BuildSchema(closeConnection: true);
		IDbCommand result = BuildRefreshSqlCommand(A_1, A_2, A_3, A_4);
		schemaRows = null;
		return result;
	}

	protected virtual string GetLastInsertIdKeyword()
	{
		return string.Empty;
	}

	protected virtual string BuildAutoIncrementRefreshSql(DataTableMapping mappings, DataRow dataRow, bool useColumnsForParametersNames)
	{
		if (GetLastInsertIdKeyword() == string.Empty || GetLastInsertIdKeyword() == null)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		DataRow dataRow2 = null;
		ArrayList arrayList = new ArrayList();
		if (tableName == null || tableName == string.Empty)
		{
			return string.Empty;
		}
		if (schemaRows == null)
		{
			return string.Empty;
		}
		bool flag = schemaRows.Length > 0 && schemaRows[0].Table.Columns.Contains(SchemaTableColumn.IsExpression);
		for (int i = 0; i < schemaRows.Length; i++)
		{
			DataRow dataRow3 = schemaRows[i];
			if (dataRow3 != null && Convert.ToString(dataRow3[SchemaTableColumn.BaseColumnName]) != "" && (!flag || !(bool)dataRow3[SchemaTableColumn.IsExpression]) && !(bool)dataRow3[SchemaTableOptionalColumn.IsRowVersion])
			{
				if (((bool)dataRow3[SchemaTableOptionalColumn.IsAutoIncrement] || (dataRow3.Table.Columns.Contains("IsIdentity") && (bool)dataRow3["IsIdentity"])) && (bool)dataRow3[SchemaTableColumn.IsKey])
				{
					dataRow2 = dataRow3;
				}
				arrayList.Add(dataRow3);
			}
		}
		if (dataRow2 == null)
		{
			return string.Empty;
		}
		stringBuilder.Append("SELECT ");
		if (refreshingFieldsList != null && refreshingFieldsList.Length > 0)
		{
			for (int i = 0; i < refreshingFieldsList.Length; i++)
			{
				string text = refreshingFieldsList[i];
				if (!Utils.IsEmpty(text))
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(QuoteIdentifier(text));
				}
			}
		}
		else if (arrayList.Count == 0)
		{
			stringBuilder.Append("*");
		}
		else
		{
			for (int i = 0; i < arrayList.Count; i++)
			{
				DataRow dataRow3 = (DataRow)arrayList[i];
				string unquotedIdentifier = (string)dataRow3[SchemaTableColumn.ColumnName];
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(QuoteIdentifier(unquotedIdentifier));
			}
		}
		stringBuilder.Append(" FROM ");
		stringBuilder.Append(tableName);
		stringBuilder.Append(" WHERE ");
		stringBuilder.Append(QuoteIdentifier((string)dataRow2[SchemaTableColumn.ColumnName]));
		stringBuilder.Append(" = " + GetLastInsertIdKeyword());
		return stringBuilder.ToString();
	}

	protected virtual IDbCommand BuildRefreshSqlCommand(DataTableMapping mappings, DataRow dataRow, bool useColumnsForParametersNames, IDataParameterCollection mainCommandParameters)
	{
		if (tableName == string.Empty)
		{
			return null;
		}
		IDbCommand dbCommand = BuildNewCommand(d);
		StringBuilder stringBuilder = new StringBuilder("SELECT ");
		ArrayList arrayList = new ArrayList();
		bool flag = schemaRows.Length > 0 && schemaRows[0].Table.Columns.Contains(SchemaTableColumn.IsExpression);
		for (int i = 0; i < schemaRows.Length; i++)
		{
			DataRow dataRow2 = schemaRows[i];
			if (dataRow2 != null && Convert.ToString(dataRow2[SchemaTableColumn.BaseColumnName]) != "" && !(bool)dataRow2[SchemaTableOptionalColumn.IsReadOnly] && (!flag || !(bool)dataRow2[SchemaTableColumn.IsExpression]) && !(bool)dataRow2[SchemaTableOptionalColumn.IsRowVersion])
			{
				arrayList.Add(dataRow2);
			}
		}
		if (refreshingFieldsList != null && refreshingFieldsList.Length > 0)
		{
			for (int i = 0; i < refreshingFieldsList.Length; i++)
			{
				if (!(refreshingFieldsList[i].Trim() == string.Empty))
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					if (Quoted)
					{
						stringBuilder.Append(QuoteIdentifier(refreshingFieldsList[i]));
					}
					else
					{
						stringBuilder.Append(refreshingFieldsList[i]);
					}
				}
			}
		}
		else if (arrayList.Count == 0)
		{
			stringBuilder.Append("*");
		}
		else
		{
			for (int i = 0; i < arrayList.Count; i++)
			{
				DataRow dataRow2 = (DataRow)arrayList[i];
				string text = (string)dataRow2[SchemaTableColumn.BaseColumnName];
				if (text.Trim() != string.Empty)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					if (Quoted)
					{
						stringBuilder.Append(QuoteIdentifier(text));
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
			}
		}
		stringBuilder.Append(" FROM ");
		stringBuilder.Append(tableName);
		a(stringBuilder, dbCommand, 0, mappings, dataRow, StatementType.Delete, useColumnsForParametersNames, mainCommandParameters, refreshingFieldsList);
		dbCommand.CommandText = stringBuilder.ToString();
		d = dbCommand;
		return dbCommand;
	}

	protected virtual void AddRefreshSql(IDbCommand command, StatementType statementType)
	{
		a(command, a(null, null, null, A_3: true, command.Parameters));
	}

	protected virtual void AddRefreshSql(IDbCommand command, bool useColumnsForParameterNames, StatementType statementType)
	{
		a(command, a(null, null, null, useColumnsForParameterNames, command.Parameters));
	}

	protected virtual void AddRefreshSql(IDbCommand command, string[] fields, bool useColumnsForParameterNames, StatementType statementType)
	{
		string[] array = refreshingFieldsList;
		try
		{
			if (fields != null)
			{
				refreshingFieldsList = fields;
			}
			a(command, a(fields, null, null, useColumnsForParameterNames, command.Parameters));
		}
		finally
		{
			if (fields != null)
			{
				refreshingFieldsList = array;
			}
		}
	}

	public override void RefreshSchema()
	{
		c();
		base.RefreshSchema();
	}

	private void a(IDbCommand A_0, IDbCommand A_1)
	{
		if (A_1 != null)
		{
			A_0.CommandText = A_0.CommandText + ";\n" + A_1.CommandText;
			while (A_1.Parameters.Count > 0)
			{
				object value = A_1.Parameters[0];
				A_1.Parameters.RemoveAt(0);
				A_0.Parameters.Add(value);
			}
		}
	}

	private static string a(string A_0, IDataParameterCollection A_1)
	{
		DbParameter dbParameter = null;
		foreach (DbParameter item in A_1)
		{
			if (!(item.SourceColumn == A_0) || item.SourceColumnNullMapping)
			{
				continue;
			}
			if (dbParameter != null)
			{
				if (a(item.SourceVersion, dbParameter.SourceVersion))
				{
					dbParameter = item;
				}
			}
			else
			{
				dbParameter = item;
			}
		}
		return dbParameter?.ParameterName;
	}

	private static bool a(DataRowVersion A_0, DataRowVersion A_1)
	{
		switch (A_0)
		{
		case DataRowVersion.Current:
			return true;
		case DataRowVersion.Default:
			return A_1 != DataRowVersion.Current;
		case DataRowVersion.Original:
			if (A_1 != DataRowVersion.Current)
			{
				return A_1 != DataRowVersion.Default;
			}
			return false;
		default:
			return A_1 == DataRowVersion.Proposed;
		}
	}

	protected void BuildSchema(bool closeConnection)
	{
		a(closeConnection);
		b();
	}

	private void a(bool A_0)
	{
		if (base.DataAdapter == null)
		{
			throw new InvalidOperationException(n.a("MissingSourceCommand"));
		}
		IDbCommand selectCommand = base.DataAdapter.SelectCommand;
		if (selectCommand == null)
		{
			throw new InvalidOperationException(n.a("MissingSourceCommand"));
		}
		if (selectCommand.Connection == null)
		{
			throw new InvalidOperationException(n.a("MissingSourceCommandConnection"));
		}
		IDbCommand dbCommand;
		if (!Utils.IsEmpty(UpdatingTable) && selectCommand.CommandType == CommandType.StoredProcedure)
		{
			dbCommand = selectCommand.Connection.CreateCommand();
			dbCommand.CommandText = UpdatingTable;
			dbCommand.CommandType = CommandType.TableDirect;
		}
		else
		{
			dbCommand = selectCommand;
		}
		IDbConnection connection = dbCommand.Connection;
		if (schemaTable != null)
		{
			return;
		}
		if (connection.State != ConnectionState.Open)
		{
			connection.Open();
		}
		else
		{
			A_0 = false;
		}
		try
		{
			schemaTable = GetSchemaTable((DbCommand)dbCommand);
		}
		finally
		{
			if (A_0 && connection.State != ConnectionState.Closed)
			{
				connection.Close();
			}
		}
	}

	private void b()
	{
		if (base.DataAdapter != null)
		{
			this.m_a = base.DataAdapter.MissingMappingAction;
			if (this.m_a != MissingMappingAction.Passthrough)
			{
				this.m_a = MissingMappingAction.Error;
			}
		}
		if (schemaRows == null)
		{
			a();
		}
		if (schemaRows != null)
		{
			for (int i = 0; i < schemaRows.Length; i++)
			{
				DataRow dataRow = schemaRows[i];
				if (dataRow != null)
				{
					tableName = GetFullTableName(dataRow);
					break;
				}
			}
		}
		if (this.m_c != null || Utils.IsEmpty(keyFieldsList))
		{
			return;
		}
		this.m_c = new DataRow[keyFieldsList.Length];
		for (int num = 0; num < keyFieldsList.Length; num++)
		{
			foreach (DataRow row in schemaTable.Rows)
			{
				if ((string)row[SchemaTableColumn.BaseColumnName] == keyFieldsList[num])
				{
					this.m_c[num] = row;
					break;
				}
			}
			if (this.m_c[num] == null)
			{
				this.m_c[num] = schemaTable.NewRow();
				InitSchemaRow(this.m_c[num]);
				this.m_c[num][SchemaTableColumn.BaseColumnName] = keyFieldsList[num];
			}
		}
	}

	private void a()
	{
		DataRow[] array = null;
		bool flag = true;
		if (schemaTable == null)
		{
			return;
		}
		array = new DataRow[schemaTable.Rows.Count];
		for (int i = 0; i < schemaTable.Rows.Count; i++)
		{
			array[i] = schemaTable.Rows[i];
		}
		if (array.Length == 0)
		{
			throw new InvalidOperationException(n.a("DynamicSQLNoTableInfo"));
		}
		string text = "";
		string text2 = "";
		this.m_b = false;
		for (int num = 0; num < array.Length; num++)
		{
			DataRow dataRow = array[num];
			string text3 = Convert.ToString(dataRow[SchemaTableColumn.BaseTableName]);
			if (text3 == "")
			{
				array[num] = null;
				continue;
			}
			string text4 = Convert.ToString(dataRow[SchemaTableColumn.BaseSchemaName]);
			if (UpdatingTable != string.Empty)
			{
				string source = text3;
				if (UpdatingTable.IndexOf('.') != -1)
				{
					source = GetFullTableName(dataRow);
				}
				if (!Utils.CompareSuffix(source, UpdatingTable, ignoreCase: true, new string[2] { QuotePrefix, QuoteSuffix }) || (!Utils.IsEmpty(text2) && string.Compare(text2, text3, ignoreCase: true) == 0 && text2 != text3))
				{
					array[num] = null;
					continue;
				}
			}
			if (Convert.ToBoolean(dataRow[SchemaTableColumn.IsKey]) || Convert.ToBoolean(dataRow[SchemaTableColumn.IsUnique]))
			{
				this.m_b = true;
			}
			else
			{
				flag = false;
			}
			if (text2 == "")
			{
				text = text4;
				text2 = text3;
			}
			else if (text2 != text3 || text != text4)
			{
				throw new InvalidOperationException(n.a("DynamicSQLJoinUnsupported"));
			}
		}
		if (flag)
		{
			this.m_b = false;
		}
		if (text2 == "")
		{
			throw new InvalidOperationException(n.a("DynamicSQLNoTableInfo"));
		}
		schemaRows = array;
	}

	protected virtual void InitSchemaRow(DataRow schemaRow)
	{
		schemaRow[SchemaTableColumn.ColumnName] = string.Empty;
		schemaRow[SchemaTableColumn.ColumnOrdinal] = 0;
		schemaRow[SchemaTableColumn.ColumnSize] = 0;
		schemaRow[SchemaTableColumn.NumericPrecision] = 0;
		schemaRow[SchemaTableColumn.NumericScale] = 0;
		schemaRow[SchemaTableColumn.DataType] = typeof(object);
		schemaRow[SchemaTableOptionalColumn.ProviderSpecificDataType] = typeof(object);
		schemaRow[SchemaTableColumn.ProviderType] = 0;
		schemaRow[SchemaTableColumn.IsLong] = false;
		schemaRow[SchemaTableColumn.AllowDBNull] = true;
		schemaRow[SchemaTableOptionalColumn.IsReadOnly] = false;
		schemaRow[SchemaTableOptionalColumn.IsRowVersion] = false;
		schemaRow[SchemaTableColumn.IsUnique] = false;
		schemaRow[SchemaTableColumn.IsKey] = false;
		schemaRow[SchemaTableOptionalColumn.IsAutoIncrement] = false;
		schemaRow[SchemaTableColumn.BaseSchemaName] = string.Empty;
		schemaRow[SchemaTableOptionalColumn.BaseCatalogName] = string.Empty;
		schemaRow[SchemaTableColumn.BaseTableName] = string.Empty;
		schemaRow[SchemaTableColumn.BaseColumnName] = string.Empty;
		if (schemaRow.Table.Columns.Contains(SchemaTableColumn.IsAliased))
		{
			schemaRow[SchemaTableColumn.IsAliased] = false;
		}
		if (schemaRow.Table.Columns.Contains(SchemaTableColumn.IsExpression))
		{
			schemaRow[SchemaTableColumn.IsExpression] = false;
		}
	}

	protected virtual bool ExcludeFromWhere(DataRow schemaRow)
	{
		return Convert.ToBoolean(schemaRow[SchemaTableColumn.IsLong]);
	}

	private DataColumn a(string A_0, DataTableMapping A_1, DataRow A_2)
	{
		if (A_0 != "")
		{
			DataColumnMapping columnMappingBySchemaAction = A_1.GetColumnMappingBySchemaAction(A_0, this.m_a);
			if (columnMappingBySchemaAction != null)
			{
				return columnMappingBySchemaAction.GetDataColumnBySchemaAction(A_2.Table, null, MissingSchemaAction.Error);
			}
		}
		return null;
	}

	protected virtual string GetParameterName(string parameterName, IList parameters)
	{
		return GetParameterName(parameters.Count + 1);
	}

	private static IDbDataParameter b(IDbCommand A_0, int A_1)
	{
		if (A_1 < A_0.Parameters.Count)
		{
			return (IDbDataParameter)A_0.Parameters[A_1];
		}
		return A_0.CreateParameter();
	}

	private static void a(IDbCommand A_0, int A_1)
	{
		while (A_0.Parameters.Count > A_1)
		{
			A_0.Parameters.RemoveAt(A_1);
		}
	}

	protected virtual string GetFullTableName(DataRow schemaRow)
	{
		return GetFullTableName(schemaRow[SchemaTableOptionalColumn.BaseCatalogName] as string, schemaRow[SchemaTableColumn.BaseSchemaName] as string, schemaRow[SchemaTableColumn.BaseTableName] as string);
	}

	protected string GetFullTableName(string catalogName, string schemaName, string tableName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (!Utils.IsEmpty(tableName))
		{
			if (!Utils.IsEmpty(catalogName))
			{
				stringBuilder.Append(QuoteIdentifier(catalogName));
				stringBuilder.Append(".");
			}
			if (!Utils.IsEmpty(schemaName))
			{
				stringBuilder.Append(QuoteIdentifier(schemaName));
				stringBuilder.Append(".");
			}
			stringBuilder.Append(QuoteIdentifier(tableName));
		}
		return stringBuilder.ToString();
	}

	private void a(StringBuilder A_0, IDbCommand A_1, int A_2, DataTableMapping A_3, DataRow A_4, StatementType A_5, bool A_6, string[] A_7)
	{
		a(A_0, A_1, A_2, A_3, A_4, A_5, A_6, null, A_7);
	}

	private void a(StringBuilder A_0, IDbCommand A_1, int A_2, DataTableMapping A_3, DataRow A_4, StatementType A_5, bool A_6, IDataParameterCollection A_7, string[] A_8)
	{
		DataRow dataRow = null;
		int num = 0;
		A_0.Append(" WHERE ");
		DataRow[] array = ((this.m_c == null) ? schemaRows : this.m_c);
		for (int i = 0; i < array.Length; i++)
		{
			dataRow = array[i];
			if (dataRow == null || !(Convert.ToString(dataRow[SchemaTableColumn.BaseColumnName]) != ""))
			{
				continue;
			}
			if (A_8 != null && A_8.Length != 0 && this.m_c == null && !this.m_b)
			{
				bool flag = false;
				foreach (string text in A_8)
				{
					if (text != null && text != string.Empty && text == Convert.ToString(dataRow[SchemaTableColumn.BaseColumnName]))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					continue;
				}
			}
			if (this.m_c == null && (ExcludeFromWhere(dataRow) || (this.m_b && !Convert.ToBoolean(dataRow[SchemaTableColumn.IsKey]) && !Convert.ToBoolean(dataRow[SchemaTableColumn.IsUnique]))))
			{
				continue;
			}
			if (num > 0)
			{
				A_0.Append(" AND ");
			}
			string text2 = Convert.ToString(dataRow[SchemaTableColumn.ColumnName]);
			object value = null;
			if (A_3 != null && A_4 != null)
			{
				DataColumn dataColumn = a(text2, A_3, A_4);
				if (dataColumn != null)
				{
					value = A_4[dataColumn, DataRowVersion.Original];
				}
			}
			string text3 = Convert.ToString(dataRow[SchemaTableColumn.BaseColumnName]);
			bool flag2 = true;
			string text4 = null;
			if (A_7 != null)
			{
				text4 = a(text2, A_7);
				if (text4 == null)
				{
					if (A_2 < A_7.Count)
					{
						A_2 = A_7.Count;
					}
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			if (flag2)
			{
				text4 = ((!A_6) ? GetParameterName(A_2 + 1) : GetParameterName(text3, A_1.Parameters));
			}
			text3 = QuoteIdentifier(text3);
			if (Convert.IsDBNull(value))
			{
				A_0.Append($"{text3} IS NULL");
			}
			else
			{
				A_0.Append(text3);
				A_0.Append(" = ");
				A_0.Append(GetParameterPlaceholder(text4));
			}
			if (!Convert.IsDBNull(value))
			{
				if (flag2)
				{
					IDbDataParameter dbDataParameter = b(A_1, A_2);
					dbDataParameter.ParameterName = text4;
					ApplyParameterInfo((DbParameter)dbDataParameter, dataRow, A_5, whereClause: true);
					dbDataParameter.Direction = ParameterDirection.Input;
					dbDataParameter.Value = value;
					dbDataParameter.SourceColumn = text2;
					dbDataParameter.SourceVersion = DataRowVersion.Original;
					A_1.Parameters.Add(dbDataParameter);
				}
				A_2++;
			}
			num++;
		}
		if (num == 0)
		{
			if (A_7 == null)
			{
				throw new InvalidOperationException(n.a("DynamicSqlGenerationNotSupp"));
			}
			throw new InvalidOperationException(n.a("DbCommandBuilder_AllRefreshNotSupported"));
		}
		a(A_1, A_2);
	}

	private string a(DataColumn A_0, DataTableMapping A_1, DataRow A_2)
	{
		DataColumnMappingCollection columnMappings = A_1.ColumnMappings;
		string columnName = A_0.ColumnName;
		for (int i = 0; i < columnMappings.Count; i++)
		{
			DataColumnMapping dataColumnMapping = columnMappings[i];
			if (dataColumnMapping.DataSetColumn == columnName)
			{
				return A_1.SourceTable;
			}
		}
		return columnName;
	}

	internal IDbCommand a(DataTableMapping A_0, DataRow A_1, bool A_2)
	{
		BuildSchema(closeConnection: true);
		if (base.DataAdapter == null || base.DataAdapter.SelectCommand == null)
		{
			return null;
		}
		if (tableName == "")
		{
			return null;
		}
		IDbCommand dbCommand = BuildNewCommand(null);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("SELECT ");
		int num = 0;
		DataColumnCollection columns = A_1.Table.Columns;
		for (int i = 0; i < columns.Count; i++)
		{
			DataColumn a_ = columns[i];
			string text = a(a_, A_0, A_1);
			bool flag = false;
			for (int num2 = 0; num2 < schemaRows.Length; num2++)
			{
				DataRow dataRow = schemaRows[num2];
				if (dataRow != null && text == (string)dataRow[SchemaTableColumn.BaseColumnName])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (num > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(QuoteIdentifier(text));
				num++;
			}
		}
		if (num == 0)
		{
			return null;
		}
		stringBuilder.Append(" FROM ");
		stringBuilder.Append(tableName);
		a(stringBuilder, dbCommand, 0, A_0, A_1, StatementType.Update, A_2, null);
		dbCommand.CommandText = stringBuilder.ToString();
		return dbCommand;
	}
}
