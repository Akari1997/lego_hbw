using System.Data;
using System.Data.Common;

namespace Devart.Data.Oracle;

public sealed class OracleDataSourceEnumerator : DbDataSourceEnumerator
{
	private static OracleDataSourceEnumerator a;

	public static OracleDataSourceEnumerator Instance
	{
		get
		{
			if (a == null)
			{
				a = new OracleDataSourceEnumerator();
			}
			return a;
		}
	}

	public override DataTable GetDataSources()
	{
		return GetDataSources("");
	}

	public DataTable GetDataSources(string homeName)
	{
		DataTable dataTable = new DataTable("datasource");
		DataColumn column = new DataColumn("InstanceName", typeof(string));
		DataColumn column2 = new DataColumn("ServerName", typeof(string));
		DataColumn column3 = new DataColumn("ServiceName", typeof(string));
		DataColumn column4 = new DataColumn("Protocol", typeof(string));
		DataColumn column5 = new DataColumn("Port", typeof(string));
		dataTable.Columns.Add(column);
		dataTable.Columns.Add(column2);
		dataTable.Columns.Add(column3);
		dataTable.Columns.Add(column4);
		dataTable.Columns.Add(column5);
		string[] serverList = OracleConnection.GetServerList(homeName);
		string[] array = serverList;
		foreach (string value in array)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["InstanceName"] = value;
			dataRow["ServerName"] = string.Empty;
			dataRow["ServiceName"] = string.Empty;
			dataRow["Protocol"] = string.Empty;
			dataRow["Port"] = string.Empty;
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}
}
