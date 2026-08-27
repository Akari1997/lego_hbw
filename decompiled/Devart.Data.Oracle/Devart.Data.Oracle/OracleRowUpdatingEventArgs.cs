using System.Data;
using System.Data.Common;

namespace Devart.Data.Oracle;

public class OracleRowUpdatingEventArgs : RowUpdatingEventArgs
{
	public new OracleCommand Command
	{
		get
		{
			return (OracleCommand)base.Command;
		}
		set
		{
			base.Command = value;
		}
	}

	public OracleRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		: base(row, command, statementType, tableMapping)
	{
	}
}
