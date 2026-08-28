using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

[Devart.Common.i("DbCommandBuilder_Description")]
public class OracleCommandBuilder : Devart.Common.DbCommandBuilder
{
	private bool m_a;

	private bool m_b;

	private bool c;

	[DefaultValue(null)]
	[MergableProperty(false)]
	[Devart.Common.i("CommandBuilder_DataAdapter")]
	[Category("Data")]
	public new OracleDataAdapter DataAdapter
	{
		get
		{
			return (OracleDataAdapter)base.DataAdapter;
		}
		set
		{
			if (Utils.MonoDetected)
			{
				if (base.DataAdapter != value)
				{
					RefreshSchema();
					if (base.DataAdapter != null)
					{
						SetRowUpdatingHandler(base.DataAdapter);
						base.DataAdapter = null;
					}
					if (value != null)
					{
						SetRowUpdatingHandler(value);
						base.DataAdapter = value;
					}
				}
			}
			else
			{
				base.DataAdapter = value;
			}
		}
	}

	[MergableProperty(false)]
	[Category("Behavior")]
	[Devart.Common.i("DbCommandBuilder_KeyFields")]
	public new string KeyFields
	{
		get
		{
			return base.KeyFields;
		}
		set
		{
			base.KeyFields = value;
		}
	}

	[Category("Data")]
	[Devart.Common.i("DbCommandBuilder_UpdatingTable")]
	[MergableProperty(false)]
	public new string UpdatingTable
	{
		get
		{
			return base.UpdatingTable;
		}
		set
		{
			base.UpdatingTable = value;
		}
	}

	[Category("Update")]
	[Devart.Common.i("DbCommandBuilder_UpdatingFields")]
	public override string UpdatingFields
	{
		get
		{
			if (updatingFieldsList == null)
			{
				return string.Empty;
			}
			return string.Join(";", updatingFieldsList);
		}
		set
		{
			if (UpdatingFields != value)
			{
				if (Utils.IsEmpty(value))
				{
					updatingFieldsList = null;
				}
				else
				{
					updatingFieldsList = Utils.SplitItems(value, new char[2] { '`', '"' });
				}
				RefreshSchema();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string CatalogSeparator
	{
		get
		{
			return ".";
		}
		set
		{
			if (value != ".")
			{
				throw new NotSupportedException(Devart.Common.al.a("CatalogSeparatorNotSupported"));
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string SchemaSeparator
	{
		get
		{
			return ".";
		}
		set
		{
			if (value != ".")
			{
				throw new NotSupportedException(Devart.Common.al.a("SchemaSeparatorNotSupported"));
			}
		}
	}

	[MergableProperty(false)]
	[Category("Update")]
	[Devart.Common.i("DbCommandBuilder_RefreshingFields")]
	public override string RefreshingFields
	{
		get
		{
			return base.RefreshingFields;
		}
		set
		{
			base.RefreshingFields = value;
		}
	}

	[Category("Update")]
	[DefaultValue(false)]
	[Devart.Common.i("OracleCommandBuilder_IdentityInsert")]
	public bool IdentityInsert
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
				RefreshSchema();
			}
		}
	}

	[Category("Update")]
	[Devart.Common.i("OracleCommandBuilder_IdentityUpdate")]
	[DefaultValue(false)]
	public bool IdentityUpdate
	{
		get
		{
			return this.m_b;
		}
		set
		{
			if (this.m_b != value)
			{
				this.m_b = value;
				RefreshSchema();
			}
		}
	}

	protected override char[] QuoteSymbols => new char[1] { '"' };

	public OracleCommandBuilder()
	{
		QuotePrefix = (QuoteSuffix = "\"");
	}

	public OracleCommandBuilder(OracleDataAdapter adapter)
	{
		QuotePrefix = (QuoteSuffix = "\"");
		DataAdapter = adapter;
	}

	protected override IDbCommand BuildNewCommand(IDbCommand command)
	{
		command = base.BuildNewCommand(command);
		((OracleCommand)command).ReturnProviderSpecificTypes = DataAdapter.ReturnProviderSpecificTypes;
		return command;
	}

	protected override DbCommand InitializeCommand(DbCommand command)
	{
		command = base.InitializeCommand(command);
		((OracleCommand)command).ReturnProviderSpecificTypes = DataAdapter.ReturnProviderSpecificTypes;
		return command;
	}

	protected override string GetParameterName(int parameterOrdinal)
	{
		string text = "p" + parameterOrdinal.ToString(CultureInfo.CurrentCulture);
		if (Utils.MonoDetected)
		{
			return "@" + text;
		}
		return text;
	}

	protected override string GetParameterName(string parameterName)
	{
		return parameterName;
	}

	protected override string GetParameterPlaceholder(int parameterOrdinal)
	{
		return ":" + GetParameterName(parameterOrdinal);
	}

	public static void DeriveParameters(OracleCommand command)
	{
		if (command == null)
		{
			throw new ArgumentNullException(Devart.Common.al.a("ValueCannotBeNull"));
		}
		if (command.CommandType == CommandType.StoredProcedure || !command.ParameterCheck)
		{
			command.Parameters.Clear();
			command.h();
		}
	}

	protected override void ApplyParameterInfo(DbParameter dbParameter, DataRow schemaRow, StatementType statementType, bool whereClause)
	{
		OracleParameter oracleParameter = (OracleParameter)dbParameter;
		object obj = schemaRow[SchemaTableColumn.ProviderType, DataRowVersion.Default];
		OracleDbType oracleDbType;
		if (obj is int)
		{
			oracleDbType = (OracleDbType)obj;
		}
		else
		{
			obj = schemaRow[SchemaTableColumn.DataType, DataRowVersion.Default];
			oracleDbType = OracleUtils.TypeToOracleDbType((Type)obj);
		}
		try
		{
			if (Utils.MonoDetected && oracleParameter.SourceColumnNullMapping)
			{
				oracleDbType = OracleDbType.Integer;
			}
			oracleParameter.OracleDbType = oracleDbType;
			switch (oracleDbType)
			{
			case OracleDbType.Char:
			case OracleDbType.NChar:
			case OracleDbType.NVarChar:
			case OracleDbType.VarChar:
			{
				int size = (int)schemaRow[SchemaTableColumn.ColumnSize];
				oracleParameter.Size = size;
				break;
			}
			case OracleDbType.Xml:
				oracleParameter.ObjectTypeName = "SYS.XMLTYPE";
				break;
			case OracleDbType.Array:
			case OracleDbType.Object:
			case OracleDbType.Ref:
			case OracleDbType.Table:
			{
				string text = schemaRow["TypeName", DataRowVersion.Default] as string;
				string text2 = schemaRow["TypeSchemaName", DataRowVersion.Default] as string;
				if (text != null)
				{
					text = OracleUtils.QuoteIfNeed(text);
					if (text2 != null)
					{
						text = OracleUtils.QuoteIfNeed(text2) + "." + text;
					}
				}
				oracleParameter.ObjectTypeName = text;
				break;
			}
			}
		}
		catch
		{
			oracleParameter.e();
		}
	}

	public new OracleCommand GetInsertCommand()
	{
		b();
		return (OracleCommand)base.GetInsertCommand();
	}

	public new OracleCommand GetInsertCommand(bool useColumnsForParameterNames)
	{
		b();
		return (OracleCommand)base.GetInsertCommand(useColumnsForParameterNames);
	}

	public new OracleCommand GetInsertCommand(string[] fields, bool useColumnsForParameterNames)
	{
		b();
		return (OracleCommand)base.GetInsertCommand(fields, useColumnsForParameterNames);
	}

	public new OracleCommand GetUpdateCommand()
	{
		a();
		return (OracleCommand)base.GetUpdateCommand();
	}

	public new OracleCommand GetUpdateCommand(bool useColumnsForParameterNames)
	{
		a();
		return (OracleCommand)base.GetUpdateCommand(useColumnsForParameterNames);
	}

	public new OracleCommand GetUpdateCommand(string[] fields, bool useColumnsForParameterNames)
	{
		a();
		return (OracleCommand)base.GetUpdateCommand(fields, useColumnsForParameterNames);
	}

	public new OracleCommand GetDeleteCommand()
	{
		return (OracleCommand)base.GetDeleteCommand();
	}

	public new OracleCommand GetDeleteCommand(bool useColumnsForParameterNames)
	{
		return (OracleCommand)base.GetDeleteCommand(useColumnsForParameterNames);
	}

	private void b()
	{
		a(A_0: false);
	}

	private void a()
	{
		a(A_0: true);
	}

	public new OracleCommand GetRefreshCommand()
	{
		return (OracleCommand)base.GetRefreshCommand();
	}

	public new OracleCommand GetRefreshCommand(bool useColumnsForParameterNames)
	{
		return (OracleCommand)base.GetRefreshCommand(useColumnsForParameterNames);
	}

	public new OracleCommand GetRefreshCommand(string[] fields, bool useColumnsForParameterNames)
	{
		return (OracleCommand)base.GetRefreshCommand(fields, useColumnsForParameterNames);
	}

	protected override void AddRefreshSql(IDbCommand command, bool useColumnsForParameterNames, StatementType statementType)
	{
		AddRefreshSql(command, null, useColumnsForParameterNames, statementType);
	}

	protected override void AddRefreshSql(IDbCommand command, StatementType statementType)
	{
		AddRefreshSql(command, null, useColumnsForParameterNames: false, statementType);
	}

	protected override void AddRefreshSql(IDbCommand command, string[] fields, bool useColumnsForParameterNames, StatementType statementType)
	{
		BuildSchema(closeConnection: true);
		if (schemaRows == null)
		{
			return;
		}
		List<string> list = new List<string>(schemaRows.Length);
		bool flag = refreshingFieldsList != null && refreshingFieldsList.Length > 0;
		if (flag)
		{
			for (int num = 0; num < refreshingFieldsList.Length; num++)
			{
				if (!(refreshingFieldsList[num].Trim() == string.Empty))
				{
					list.Add(refreshingFieldsList[num]);
				}
			}
		}
		List<string> list2 = new List<string>(schemaRows.Length);
		List<int> list3 = new List<int>();
		for (int num2 = 0; num2 < schemaRows.Length; num2++)
		{
			DataRow dataRow = schemaRows[num2];
			if (dataRow == null || !(Convert.ToString(dataRow[SchemaTableColumn.BaseColumnName]) != ""))
			{
				continue;
			}
			string item = (string)dataRow[SchemaTableColumn.ColumnName];
			list2.Add(item);
			if (!flag)
			{
				switch (Convert.ToString(dataRow["TypeName"]))
				{
				case "LONG":
				case "LONG RAW":
				case "XMLTYPE":
					list3.Add(list2.Count - 1);
					break;
				}
			}
		}
		string[] array = OracleDataAdapter.a(list2);
		List<string> list4 = new List<string>(array.Length);
		if (flag)
		{
			foreach (string item2 in list)
			{
				int num3 = -1;
				for (int num4 = 0; num4 < list2.Count; num4++)
				{
					string strB = list2[num4];
					if (string.Compare(item2, strB, ignoreCase: false, CultureInfo.InvariantCulture) == 0)
					{
						num3 = num4;
						break;
					}
				}
				if (num3 >= 0)
				{
					list4.Add(array[num3]);
				}
				else
				{
					list4.Add(null);
				}
			}
		}
		else
		{
			for (int num5 = 0; num5 < list2.Count; num5++)
			{
				if (!list3.Contains(num5))
				{
					list.Add(list2[num5]);
					list4.Add(array[num5]);
				}
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		List<string> list5 = new List<string>(schemaRows.Length);
		for (int num6 = list.Count - 1; num6 >= 0; num6--)
		{
			string text = list[num6];
			string text2 = list4[num6];
			string text3 = null;
			text3 = ((text2 == null) ? a(command, text, A_2: false) : a(command, text2, A_2: true));
			if (text3 == null)
			{
				DataRow dataRow2 = null;
				for (int num7 = 0; num7 < schemaRows.Length; num7++)
				{
					DataRow dataRow3 = schemaRows[num7];
					if (dataRow3 != null && text == (string)dataRow3[SchemaTableColumn.BaseColumnName])
					{
						dataRow2 = dataRow3;
						break;
					}
				}
				if (dataRow2 == null)
				{
					list.RemoveAt(num6);
				}
				else
				{
					OracleParameter oracleParameter = new OracleParameter();
					oracleParameter.Direction = ParameterDirection.Output;
					oracleParameter.SourceColumn = ((text2 != null) ? text2 : text);
					ApplyParameterInfo(oracleParameter, dataRow2, StatementType.Update, whereClause: false);
					command.Parameters.Add(oracleParameter);
					text3 = oracleParameter.ParameterName;
				}
			}
			if (text3 != null)
			{
				list5.Insert(0, text3);
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(" RETURNING ");
		for (int num8 = 0; num8 < list.Count; num8++)
		{
			if (num8 > 0)
			{
				stringBuilder.Append(", ");
			}
			if (Quoted)
			{
				stringBuilder.Append('"');
			}
			stringBuilder.Append(list[num8]);
			if (Quoted)
			{
				stringBuilder.Append('"');
			}
		}
		stringBuilder.Append(" INTO ");
		for (int num9 = 0; num9 < list5.Count; num9++)
		{
			if (num9 > 0)
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(":");
			stringBuilder.Append(list5[num9]);
		}
		command.CommandText += stringBuilder.ToString();
	}

	private static string a(IDbCommand A_0, string A_1, bool A_2)
	{
		string text = null;
		for (int num = 0; num < A_0.Parameters.Count; num++)
		{
			OracleParameter oracleParameter = (OracleParameter)A_0.Parameters[num];
			if (oracleParameter.SourceVersion == DataRowVersion.Current && string.Compare(oracleParameter.SourceColumn, A_1, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				if (oracleParameter.Direction == ParameterDirection.Input)
				{
					oracleParameter.Direction = ParameterDirection.InputOutput;
				}
				if (!A_2 && text != null)
				{
					return null;
				}
				text = oracleParameter.ParameterName;
				if (A_2)
				{
					return text;
				}
			}
		}
		return text;
	}

	protected override void SetRowUpdatingHandler(System.Data.Common.DbDataAdapter adapter)
	{
		if (adapter == base.DataAdapter)
		{
			((OracleDataAdapter)adapter).RowUpdating -= a;
		}
		else
		{
			((OracleDataAdapter)adapter).RowUpdating += a;
		}
	}

	private void a(object A_0, OracleRowUpdatingEventArgs A_1)
	{
		bool flag = A_1.Command != null;
		if (A_1.StatementType != StatementType.Select && !string.IsNullOrEmpty(UpdatingTable) && A_1.Row != null && A_1.TableMapping != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(schemaTable.Rows.Count);
			foreach (DataRow row in schemaTable.Rows)
			{
				dictionary.Add(row[SchemaTableColumn.ColumnName].ToString(), (int)row[SchemaTableColumn.ColumnOrdinal]);
			}
			foreach (DataColumn column in A_1.Row.Table.Columns)
			{
				if (!column.ExtendedProperties.ContainsKey(SchemaTableColumn.ColumnName))
				{
					continue;
				}
				string text = (string)column.ExtendedProperties[SchemaTableColumn.ColumnName];
				if (!dictionary.ContainsKey(text))
				{
					continue;
				}
				DataColumn dataColumn2 = A_1.TableMapping.GetDataColumn(text, null, A_1.Row.Table, DataAdapter.MissingMappingAction, MissingSchemaAction.Error);
				int num = -1;
				if (column.ExtendedProperties.ContainsKey(SchemaTableColumn.ColumnOrdinal))
				{
					num = (int)dataColumn2.ExtendedProperties[SchemaTableColumn.ColumnOrdinal];
				}
				int num2 = dictionary[text];
				if (num == num2)
				{
					continue;
				}
				bool flag2 = false;
				foreach (DataColumnMapping columnMapping in A_1.TableMapping.ColumnMappings)
				{
					if (columnMapping.SourceColumn == text)
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					continue;
				}
				foreach (DataColumn column2 in A_1.Row.Table.Columns)
				{
					if (column2.ExtendedProperties.ContainsKey(SchemaTableColumn.ColumnOrdinal))
					{
						int num3 = (int)column2.ExtendedProperties[SchemaTableColumn.ColumnOrdinal];
						if (num3 == num2)
						{
							A_1.TableMapping.ColumnMappings.Add(text, column2.ColumnName);
						}
					}
				}
			}
		}
		RowUpdatingHandler(A_0, A_1);
		if (!flag && A_1.Command != null && ((A_1.StatementType == StatementType.Update && (base.RefreshMode & RefreshRowMode.AfterUpdate) != RefreshRowMode.None) || (A_1.StatementType == StatementType.Insert && (base.RefreshMode & RefreshRowMode.AfterInsert) != RefreshRowMode.None)))
		{
			AddRefreshSql(A_1.Command, A_1.StatementType);
		}
	}

	protected override DataTable GetUpdateSchemaTable(DataTable schemaTable)
	{
		schemaTable = base.GetUpdateSchemaTable(schemaTable);
		if (schemaTable == null)
		{
			return schemaTable;
		}
		DataColumn dataColumn = schemaTable.Columns[SchemaTableColumn.IsLong];
		DataColumn dataColumn2 = schemaTable.Columns[SchemaTableColumn.ProviderType];
		DataColumn column = schemaTable.Columns["IdentityType"];
		c = false;
		foreach (DataRow row in schemaTable.Rows)
		{
			switch ((OracleDbType)(int)row[dataColumn2])
			{
			case OracleDbType.Boolean:
			case OracleDbType.Double:
			case OracleDbType.Float:
			case OracleDbType.Integer:
				if (DataAdapter.ReturnProviderSpecificTypes)
				{
					dataColumn2.ReadOnly = false;
					try
					{
						row[SchemaTableColumn.ProviderType] = OracleDbType.Number;
					}
					finally
					{
						dataColumn2.ReadOnly = true;
					}
				}
				break;
			case OracleDbType.RowId:
				if ((OracleDbType)row[SchemaTableColumn.ProviderType] == OracleDbType.RowId && (string)row[SchemaTableColumn.ColumnName] == "ROWID")
				{
					DataColumn dataColumn3 = schemaTable.Columns[SchemaTableOptionalColumn.IsAutoIncrement];
					dataColumn3.ReadOnly = false;
					try
					{
						row[dataColumn3] = true;
					}
					finally
					{
						dataColumn3.ReadOnly = true;
					}
				}
				break;
			case OracleDbType.Array:
			case OracleDbType.Object:
			case OracleDbType.Table:
				dataColumn.ReadOnly = false;
				try
				{
					row[dataColumn] = true;
				}
				finally
				{
					dataColumn.ReadOnly = true;
				}
				break;
			}
			OracleIdentityType oracleIdentityType = (OracleIdentityType)row[column];
			if (oracleIdentityType == OracleIdentityType.None)
			{
				continue;
			}
			DataColumn dataColumn4 = null;
			switch (oracleIdentityType)
			{
			case OracleIdentityType.GeneratedAlways:
				dataColumn4 = schemaTable.Columns[SchemaTableOptionalColumn.IsAutoIncrement];
				break;
			case OracleIdentityType.GeneratedByDefault:
			case OracleIdentityType.GeneratedByDefaultOnNull:
				if (!IdentityInsert && !IdentityUpdate)
				{
					dataColumn4 = schemaTable.Columns[SchemaTableOptionalColumn.IsAutoIncrement];
					break;
				}
				if (IdentityInsert && IdentityUpdate)
				{
					dataColumn4 = null;
					break;
				}
				if (IdentityUpdate)
				{
					dataColumn4 = schemaTable.Columns[SchemaTableColumn.IsExpression];
					break;
				}
				c = true;
				dataColumn4 = null;
				break;
			default:
				throw new InvalidOperationException();
			}
			if (dataColumn4 != null)
			{
				dataColumn4.ReadOnly = false;
				try
				{
					row[dataColumn4] = true;
				}
				finally
				{
					dataColumn4.ReadOnly = true;
				}
			}
		}
		return schemaTable;
	}

	private void a(bool A_0)
	{
		if (!c)
		{
			return;
		}
		if (schemaTable == null)
		{
			BuildSchema(closeConnection: true);
		}
		if (schemaTable == null)
		{
			return;
		}
		DataColumn column = schemaTable.Columns["IdentityType"];
		DataColumn dataColumn = schemaTable.Columns[SchemaTableOptionalColumn.IsAutoIncrement];
		foreach (DataRow row in schemaTable.Rows)
		{
			OracleIdentityType oracleIdentityType = (OracleIdentityType)row[column];
			if (oracleIdentityType == OracleIdentityType.GeneratedByDefault || oracleIdentityType == OracleIdentityType.GeneratedByDefaultOnNull)
			{
				dataColumn.ReadOnly = false;
				try
				{
					row[dataColumn] = (A_0 && !IdentityUpdate) || (!A_0 && !IdentityInsert);
				}
				finally
				{
					dataColumn.ReadOnly = true;
				}
			}
		}
	}

	protected override void SetAlternativeKey(DataTable schemaTable)
	{
		if (OracleUtils.OracleClientCompatible)
		{
			return;
		}
		DataColumn column = schemaTable.Columns[SchemaTableColumn.ColumnName];
		DataColumn column2 = schemaTable.Columns[SchemaTableColumn.ProviderType];
		DataColumn column3 = schemaTable.Columns[SchemaTableColumn.IsKey];
		foreach (DataRow row in schemaTable.Rows)
		{
			if (row.RowState != DataRowState.Deleted && (OracleDbType)row[column2] == OracleDbType.RowId && (string)row[column] == "ROWID")
			{
				row[column3] = true;
				return;
			}
		}
		base.SetAlternativeKey(schemaTable);
	}

	protected override DataTable GetSchemaTable(DbCommand srcCommand)
	{
		if (DataAdapter == null || DataAdapter.SelectCommand == null || DataAdapter.SelectCommand != srcCommand)
		{
			return base.GetSchemaTable(srcCommand);
		}
		if (schemaTable == null)
		{
			schemaTable = base.GetSchemaTable(srcCommand);
		}
		return schemaTable;
	}

	protected override bool IsValidQuote(string quote, bool prefix)
	{
		return quote == "\"";
	}

	public override string QuoteIdentifier(string unquotedIdentifier)
	{
		Utils.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
		if (Quoted)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (QuotePrefix.Length == 0)
			{
				stringBuilder.Append("\"");
			}
			else
			{
				stringBuilder.Append(QuotePrefix);
			}
			stringBuilder.Append(unquotedIdentifier);
			if (QuoteSuffix.Length == 0)
			{
				stringBuilder.Append("\"");
			}
			else
			{
				stringBuilder.Append(QuoteSuffix);
			}
			return stringBuilder.ToString();
		}
		return unquotedIdentifier;
	}

	public override string UnquoteIdentifier(string quotedIdentifier)
	{
		Utils.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
		if (Quoted)
		{
			if (quotedIdentifier.Length < 2 || !quotedIdentifier.StartsWith(QuotePrefix) || !quotedIdentifier.EndsWith(QuoteSuffix))
			{
				throw new ArgumentException(Devart.Common.al.a("IdentifierIsNotQuoted"));
			}
			return quotedIdentifier.Substring(QuotePrefix.Length, quotedIdentifier.Length - QuotePrefix.Length - QuoteSuffix.Length).Replace("\"\"", "\"");
		}
		return quotedIdentifier;
	}

	protected override bool ExcludeFromWhere(DataRow schemaRow)
	{
		if (base.ExcludeFromWhere(schemaRow))
		{
			return true;
		}
		Type type = (Type)schemaRow[SchemaTableColumn.DataType];
		if ((object)type != typeof(IDataReader) && (object)type != typeof(OracleObject) && (object)type != typeof(OracleArray) && (object)type != typeof(OracleTable) && (object)type != typeof(OracleRef) && (object)type != typeof(OracleXml))
		{
			return (object)type == typeof(OracleAnyData);
		}
		return true;
	}
}
