using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Devart.Common;

public abstract class DbCommandBuilder : DbCommandBuilderBase
{
	protected string[] updatingFieldsList;

	private static bool throwOnInvalidQuote = true;

	internal new static ArrayList a;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override string QuotePrefix
	{
		get
		{
			if (!Quoted)
			{
				return string.Empty;
			}
			return base.QuotePrefix;
		}
		set
		{
			if (value == string.Empty || IsValidQuote(value, prefix: true))
			{
				base.QuotePrefix = value;
			}
			else if (throwOnInvalidQuote)
			{
				throw new ArgumentException($"'{value}' is not acceptable value for the property 'QuotePrefix'.");
			}
		}
	}

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string QuoteSuffix
	{
		get
		{
			if (!Quoted)
			{
				return string.Empty;
			}
			return base.QuoteSuffix;
		}
		set
		{
			if (value == string.Empty || IsValidQuote(value, prefix: false))
			{
				base.QuoteSuffix = value;
			}
			else if (throwOnInvalidQuote)
			{
				throw new ArgumentException($"'{value}' is not acceptable value for the property 'QuoteSuffix'.");
			}
		}
	}

	[MergableProperty(false)]
	[r("DbCommandBuilder_UpdatingFields")]
	[Category("Update")]
	public abstract string UpdatingFields { get; set; }

	protected void BeforeCreatingCommand()
	{
		if (Utils.MonoDetected)
		{
			a = new ArrayList();
			if (schemaTable == null)
			{
				BuildSchema(closeConnection: true);
			}
		}
	}

	protected void AfterCreatingCommand()
	{
		if (Utils.MonoDetected)
		{
			a = null;
		}
	}

	public new DbCommand GetInsertCommand()
	{
		BeforeCreatingCommand();
		DbCommand insertCommand;
		try
		{
			insertCommand = base.GetInsertCommand();
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes(insertCommand);
			}
		}
		finally
		{
			AfterCreatingCommand();
		}
		if (insertCommand != null && (base.RefreshMode & RefreshRowMode.AfterInsert) != RefreshRowMode.None)
		{
			AddRefreshSql(insertCommand, StatementType.Insert);
		}
		return insertCommand;
	}

	protected void CorrectParameterPrefixes(DbCommand command)
	{
		if (command == null)
		{
			return;
		}
		if (a != null)
		{
			int num = -1;
			int num2;
			for (num2 = 0; num2 < a.Count; num2++)
			{
				DbCommand dbCommand = (DbCommand)a[num2];
				if (dbCommand == command)
				{
					DbParameter dbParameter = (DbParameter)a[num2 + 1];
					int num3 = command.Parameters.IndexOf(dbParameter);
					if (num3 >= 0)
					{
						num = num3;
					}
					else
					{
						dbParameter.SourceColumnNullMapping = true;
						dbParameter.SourceVersion = DataRowVersion.Original;
						dbParameter.SourceColumn = command.Parameters[num + 1].SourceColumn;
						command.Parameters.Insert(num + 1, dbParameter);
					}
				}
				num2++;
			}
		}
		char c2 = GetParameterPlaceholder(0)[0];
		if (c2 == '@')
		{
			return;
		}
		string text = command.CommandText;
		foreach (DbParameter parameter in command.Parameters)
		{
			_ = parameter.ParameterName;
			string text2 = parameter.ParameterName;
			string text3 = text2;
			if (!string.IsNullOrEmpty(parameter.ParameterName))
			{
				if (parameter.ParameterName[0] == '@' && c2 != '@')
				{
					text2 = parameter.ParameterName.Substring(1);
				}
				else
				{
					text3 = '@' + parameter.ParameterName;
				}
			}
			string text4 = c2 + text2;
			int startIndex = 0;
			while (true)
			{
				int num4 = text.IndexOf(text3, startIndex);
				if (num4 == -1)
				{
					break;
				}
				startIndex = num4 + text2.Length;
				if (num4 > 0 && !char.IsLetterOrDigit(text[num4 - 1]) && (num4 + text3.Length >= text.Length || !char.IsLetterOrDigit(text[num4 + text3.Length])))
				{
					text = text.Substring(0, num4) + text4 + text.Substring(num4 + text3.Length);
				}
			}
			parameter.ParameterName = text2;
			DataRow dataRow = null;
			foreach (DataRow row in schemaTable.Rows)
			{
				if ((string)row[SchemaTableColumn.BaseColumnName] == parameter.SourceColumn)
				{
					dataRow = row;
					break;
				}
			}
			if (dataRow != null)
			{
				ApplyParameterInfo(parameter, dataRow, StatementType.Insert, whereClause: true);
			}
		}
		command.CommandText = text;
	}

	public new DbCommand GetInsertCommand(bool useColumnsForParameterNames)
	{
		BeforeCreatingCommand();
		DbCommand insertCommand;
		try
		{
			insertCommand = base.GetInsertCommand(useColumnsForParameterNames);
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes(insertCommand);
			}
		}
		finally
		{
			AfterCreatingCommand();
		}
		if (insertCommand != null && (base.RefreshMode & RefreshRowMode.AfterInsert) != RefreshRowMode.None)
		{
			AddRefreshSql(insertCommand, useColumnsForParameterNames, StatementType.Insert);
		}
		return insertCommand;
	}

	public DbCommand GetInsertCommand(string[] fields, bool useColumnsForParameterNames)
	{
		updatingFieldsList = fields;
		RefreshSchema();
		BeforeCreatingCommand();
		DbCommand insertCommand;
		try
		{
			insertCommand = base.GetInsertCommand(useColumnsForParameterNames);
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes(insertCommand);
			}
		}
		finally
		{
			AfterCreatingCommand();
		}
		if (insertCommand != null && (base.RefreshMode & RefreshRowMode.AfterInsert) != RefreshRowMode.None)
		{
			AddRefreshSql(insertCommand, null, useColumnsForParameterNames, StatementType.Insert);
		}
		return insertCommand;
	}

	protected void RowUpdatingHandler(object sender, RowUpdatingEventArgs ruevent)
	{
		BeforeCreatingCommand();
		try
		{
			RowUpdatingHandler(ruevent);
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes((DbCommand)ruevent.Command);
			}
		}
		finally
		{
			AfterCreatingCommand();
		}
	}

	public new DbCommand GetRefreshCommand()
	{
		return (DbCommand)base.GetRefreshCommand();
	}

	public new DbCommand GetRefreshCommand(bool useColumnsForParameterNames)
	{
		return (DbCommand)base.GetRefreshCommand(useColumnsForParameterNames);
	}

	public new DbCommand GetRefreshCommand(string[] fields, bool useColumnsForParameterNames)
	{
		updatingFieldsList = fields;
		RefreshSchema();
		return (DbCommand)base.GetRefreshCommand(useColumnsForParameterNames);
	}

	public new DbCommand GetUpdateCommand()
	{
		BeforeCreatingCommand();
		DbCommand updateCommand;
		try
		{
			updateCommand = base.GetUpdateCommand();
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes(updateCommand);
			}
		}
		finally
		{
			AfterCreatingCommand();
		}
		if (updateCommand != null && (base.RefreshMode & RefreshRowMode.AfterUpdate) != RefreshRowMode.None)
		{
			AddRefreshSql(updateCommand, StatementType.Update);
		}
		return updateCommand;
	}

	public new DbCommand GetUpdateCommand(bool useColumnsForParameterNames)
	{
		BeforeCreatingCommand();
		DbCommand updateCommand;
		try
		{
			updateCommand = base.GetUpdateCommand(useColumnsForParameterNames);
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes(updateCommand);
			}
		}
		finally
		{
			AfterCreatingCommand();
		}
		if (updateCommand != null && (base.RefreshMode & RefreshRowMode.AfterUpdate) != RefreshRowMode.None)
		{
			AddRefreshSql(updateCommand, useColumnsForParameterNames, StatementType.Update);
		}
		return updateCommand;
	}

	public DbCommand GetUpdateCommand(string[] fields, bool useColumnsForParameterNames)
	{
		updatingFieldsList = fields;
		RefreshSchema();
		BeforeCreatingCommand();
		DbCommand updateCommand;
		try
		{
			updateCommand = base.GetUpdateCommand(useColumnsForParameterNames);
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes(updateCommand);
			}
		}
		finally
		{
			AfterCreatingCommand();
		}
		if (updateCommand != null && (base.RefreshMode & RefreshRowMode.AfterUpdate) != RefreshRowMode.None)
		{
			AddRefreshSql(updateCommand, null, useColumnsForParameterNames, StatementType.Update);
		}
		return updateCommand;
	}

	public new DbCommand GetDeleteCommand()
	{
		BeforeCreatingCommand();
		try
		{
			DbCommand deleteCommand = base.GetDeleteCommand();
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes(deleteCommand);
			}
			return deleteCommand;
		}
		finally
		{
			AfterCreatingCommand();
		}
	}

	public new DbCommand GetDeleteCommand(bool useColumnsForParameterNames)
	{
		BeforeCreatingCommand();
		try
		{
			DbCommand deleteCommand = base.GetDeleteCommand(useColumnsForParameterNames);
			if (Utils.MonoDetected)
			{
				CorrectParameterPrefixes(deleteCommand);
			}
			return deleteCommand;
		}
		finally
		{
			AfterCreatingCommand();
		}
	}

	protected override DataTable GetSchemaTable(DbCommand srcCommand)
	{
		DataTable dataTable;
		using (IDataReader dataReader = srcCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
		{
			dataTable = dataReader.GetSchemaTable();
		}
		return GetUpdateSchemaTable(dataTable);
	}

	protected virtual DataTable GetUpdateSchemaTable(DataTable schemaTable)
	{
		if (schemaTable == null)
		{
			return schemaTable;
		}
		DataRow[] array = new DataRow[schemaTable.Rows.Count];
		schemaTable.Rows.CopyTo(array, 0);
		DataColumn dataColumn = schemaTable.Columns[SchemaTableColumn.IsKey];
		dataColumn.ReadOnly = false;
		DataColumn dataColumn2 = ((schemaTable.Columns.IndexOf(SchemaTableOptionalColumn.IsAutoIncrement) == -1) ? schemaTable.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool)) : schemaTable.Columns[SchemaTableOptionalColumn.IsAutoIncrement]);
		dataColumn2.ReadOnly = false;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		DataColumn dataColumn3 = schemaTable.Columns[SchemaTableColumn.BaseSchemaName];
		flag2 = dataColumn3.ReadOnly;
		dataColumn3.ReadOnly = false;
		DataColumn dataColumn4 = schemaTable.Columns[SchemaTableOptionalColumn.BaseCatalogName];
		flag3 = dataColumn4.ReadOnly;
		dataColumn4.ReadOnly = false;
		if (updatingFieldsList != null && updatingFieldsList.Length > 0)
		{
			string[] array2 = updatingFieldsList;
			foreach (string text in array2)
			{
				bool flag4 = false;
				if (text.Trim() == string.Empty)
				{
					continue;
				}
				DataRow[] array3 = array;
				foreach (DataRow dataRow in array3)
				{
					if (string.Compare(text, Utils.ObjectToString(dataRow[SchemaTableColumn.ColumnName]), ignoreCase: false) == 0)
					{
						flag4 = true;
					}
				}
				if (!flag4)
				{
					DataRow[] array4 = array;
					foreach (DataRow dataRow2 in array4)
					{
						if (string.Compare(text, Utils.ObjectToString(dataRow2[SchemaTableColumn.ColumnName]), ignoreCase: true) == 0)
						{
							if (flag4)
							{
								throw new InvalidOperationException($"There is two updating field with name {text} in schema table.");
							}
							flag4 = true;
						}
					}
				}
				if (!flag4)
				{
					throw new InvalidOperationException($"Updating field with name {text} does not exist in schema table.");
				}
			}
		}
		DataRow[] array5 = array;
		foreach (DataRow dataRow3 in array5)
		{
			if (!Utils.IsEmpty(base.UpdatingTable))
			{
				string source = Utils.ObjectToString(dataRow3[SchemaTableColumn.BaseTableName]);
				if (base.UpdatingTable.IndexOf('.') != -1)
				{
					source = GetFullTableName(dataRow3);
				}
				if (!Utils.CompareSuffix(source, base.UpdatingTable, ignoreCase: true, new string[2] { base.QuotePrefix, base.QuoteSuffix }))
				{
					schemaTable.Rows.Remove(dataRow3);
					continue;
				}
			}
			string st = Utils.ObjectToString(dataRow3[SchemaTableColumn.ColumnName]);
			if (updatingFieldsList != null && updatingFieldsList.Length > 0)
			{
				bool flag5 = false;
				string[] array6 = updatingFieldsList;
				foreach (string st2 in array6)
				{
					if (Utils.Compare(st2, st))
					{
						flag5 = true;
						break;
					}
				}
				dataRow3[SchemaTableOptionalColumn.IsAutoIncrement] = !flag5;
			}
			if (keyFieldsList != null)
			{
				bool flag6 = false;
				string[] array7 = keyFieldsList;
				foreach (string st3 in array7)
				{
					if (Utils.Compare(st3, st))
					{
						flag6 = true;
						break;
					}
				}
				dataRow3[dataColumn] = flag6;
			}
			if (dataRow3[dataColumn] is bool && (bool)dataRow3[dataColumn])
			{
				flag = true;
			}
			if (!UseSchema)
			{
				dataRow3[dataColumn3] = string.Empty;
				dataRow3[dataColumn4] = string.Empty;
			}
			else if (!UseCatalog)
			{
				dataRow3[dataColumn4] = string.Empty;
			}
		}
		if (!flag)
		{
			SetAlternativeKey(schemaTable);
		}
		dataColumn.ReadOnly = true;
		dataColumn2.ReadOnly = true;
		dataColumn3.ReadOnly = flag2;
		dataColumn4.ReadOnly = flag3;
		return schemaTable;
	}

	protected virtual void SetAlternativeKey(DataTable schemaTable)
	{
		DataColumn column = schemaTable.Columns[SchemaTableColumn.IsKey];
		foreach (DataRow row in schemaTable.Rows)
		{
			if (row.RowState != DataRowState.Deleted)
			{
				row[column] = true;
			}
		}
	}

	public override string QuoteIdentifier(string unquotedIdentifier)
	{
		Utils.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
		if (Quoted)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(QuotePrefix);
			stringBuilder.Append(unquotedIdentifier);
			stringBuilder.Append(QuoteSuffix);
			return stringBuilder.ToString();
		}
		return unquotedIdentifier;
	}

	protected abstract bool IsValidQuote(string quote, bool prefix);
}
