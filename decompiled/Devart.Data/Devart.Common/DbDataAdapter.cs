using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Devart.Common;

public abstract class DbDataAdapter : System.Data.Common.DbDataAdapter, IDbDataAdapter
{
	internal int a(DataTable A_0, IDataReader A_1)
	{
		return LoadTable(A_0, (DbDataReader)A_1);
	}

	protected virtual int LoadTable(DataTable table, DbDataReader dataReader)
	{
		return Fill(table, dataReader);
	}

	public int FillPage(DataTable dataTable, int startRecord, int maxRecords)
	{
		CommandBehavior behavior = CommandBehavior.Default;
		DbCommandBase dbCommandBase = (DbCommandBase)base.SelectCommand;
		if (dbCommandBase.Connection != null && dbCommandBase.Connection.State == ConnectionState.Closed)
		{
			dbCommandBase.Connection.Open();
			behavior = CommandBehavior.CloseConnection;
		}
		DbDataReader dbDataReader = dbCommandBase.ExecutePageReader(behavior, startRecord, maxRecords);
		try
		{
			return LoadTable(dataTable, dbDataReader);
		}
		finally
		{
			dbDataReader.Close();
		}
	}

	protected override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader)
	{
		DataTable[] array = base.FillSchema(dataSet, schemaType, srcTable, dataReader);
		CustomizeDataTableColumns(array);
		return array;
	}

	protected override DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader)
	{
		DataTable result = base.FillSchema(dataTable, schemaType, dataReader);
		CustomizeDataTableColumns(new DataTable[1] { dataTable });
		return result;
	}

	protected virtual void CustomizeDataTableColumns(DataTable[] tables)
	{
		foreach (DataTable dataTable in tables)
		{
			foreach (DataColumn column in dataTable.Columns)
			{
				column.MaxLength = -1;
			}
		}
	}

	protected static bool CheckMissingSchemaAction(string fieldName, string tableName, MissingSchemaAction missingSchemaAction)
	{
		switch (missingSchemaAction)
		{
		case MissingSchemaAction.Add:
		case MissingSchemaAction.AddWithKey:
			return false;
		case MissingSchemaAction.Error:
			throw new InvalidOperationException($"Missing the DataColumn {fieldName} in the DataTable {tableName}.");
		case MissingSchemaAction.Ignore:
			return true;
		default:
			return false;
		}
	}

	protected static string[] GetIndexedFieldNames(ICollection<string> fieldNames)
	{
		return GetIndexedFieldNames(fieldNames, firstColumnBugCompatibleMode: true);
	}

	protected static string[] GetIndexedFieldNames(ICollection<string> fieldNames, bool firstColumnBugCompatibleMode)
	{
		int count = fieldNames.Count;
		List<string> list = new List<string>(fieldNames);
		string[] array = new string[count];
		List<KeyValuePair<string, int>> list2 = new List<KeyValuePair<string, int>>(count / 2);
		for (int i = 0; i < count; i++)
		{
			string text = (array[i] = list[i]);
			if (Utils.IsEmpty(text))
			{
				list2.Add(new KeyValuePair<string, int>(text, i));
				continue;
			}
			for (int num = i + 1; num < count; num++)
			{
				if (string.Compare(text, list[num], StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					string key = (firstColumnBugCompatibleMode ? text : list[num]);
					list2.Add(new KeyValuePair<string, int>(key, num));
					break;
				}
			}
		}
		foreach (KeyValuePair<string, int> item in list2)
		{
			string text2 = item.Key;
			string text3 = text2;
			int num2 = 1;
			if (Utils.IsEmpty(text2))
			{
				text2 = "Column";
				text3 = "Column1";
				num2 = 2;
			}
			int value = item.Value;
			while (a(list, text3))
			{
				text3 = text2 + num2;
				num2++;
			}
			array[value] = text3;
			list.Add(text3);
		}
		return array;
	}

	private static bool a(ICollection<string> A_0, string A_1)
	{
		foreach (string item in A_0)
		{
			if (string.Compare(A_1, item, StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return true;
			}
		}
		return false;
	}
}
