using System;
using System.Data;
using System.Data.Common;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleRowUpdatedEventArgs : RowUpdatedEventArgs
{
	public new OracleCommand Command => (OracleCommand)base.Command;

	public OracleRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		: base(row, command, statementType, tableMapping)
	{
		if (command == null || row == null)
		{
			return;
		}
		foreach (DataColumn column2 in row.Table.Columns)
		{
			string columnName = column2.ColumnName;
			int num = columnName.IndexOf(".");
			if (num < 0)
			{
				continue;
			}
			string text = columnName.Substring(0, num);
			string text2 = columnName.Substring(num + 1);
			int num2 = -1;
			int num3 = 0;
			foreach (OracleParameter parameter in command.Parameters)
			{
				if (parameter.SourceColumn == text)
				{
					num2 = num3;
					break;
				}
				num3++;
			}
			if (num2 < 0)
			{
				continue;
			}
			OracleParameter oracleParameter2 = (OracleParameter)command.Parameters[num2];
			if (oracleParameter2.OracleDbType != OracleDbType.Object)
			{
				continue;
			}
			OracleObject oracleObject;
			if (oracleParameter2.Value == null || oracleParameter2.Value == DBNull.Value)
			{
				if (oracleParameter2.ObjectTypeName == null || oracleParameter2.ObjectTypeName == "")
				{
					throw new ArgumentException(Devart.Common.al.a("ObjectTypeNameMustBeSpecified"));
				}
				oracleObject = (OracleObject)(oracleParameter2.Value = new OracleObject(oracleParameter2.ObjectTypeName, (OracleConnection)command.Connection));
			}
			else
			{
				oracleObject = (OracleObject)oracleParameter2.Value;
			}
			if (text2.IndexOf('.') < 0)
			{
				oracleObject[text2] = row[column2];
			}
		}
		foreach (OracleParameter parameter2 in command.Parameters)
		{
			string sourceColumn = parameter2.SourceColumn;
			int num4 = sourceColumn.IndexOf(".");
			if (num4 < 0)
			{
				continue;
			}
			string relationName = sourceColumn.Substring(0, num4);
			sourceColumn = sourceColumn.Substring(num4 + 1);
			DataRow parentRow = row.GetParentRow(relationName, parameter2.SourceVersion);
			if (parentRow != null)
			{
				DataColumnCollection columns = parentRow.Table.Columns;
				int num5 = columns.IndexOf(sourceColumn);
				if (num5 >= 0)
				{
					DataColumn column = columns[num5];
					parameter2.Value = parentRow[column, parameter2.SourceVersion];
				}
			}
		}
	}
}
