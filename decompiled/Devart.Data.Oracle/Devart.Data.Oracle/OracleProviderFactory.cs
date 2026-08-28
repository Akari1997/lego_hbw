using System;
using System.Data.Common;

namespace Devart.Data.Oracle;

public class OracleProviderFactory : DbProviderFactory, IServiceProvider
{
	private static object a = null;

	public static OracleProviderFactory Instance = new OracleProviderFactory();

	public override bool CanCreateDataSourceEnumerator => false;

	public override DbCommand CreateCommand()
	{
		return new OracleCommand();
	}

	public override DbConnection CreateConnection()
	{
		return new OracleConnection();
	}

	public override DbDataAdapter CreateDataAdapter()
	{
		return new OracleDataAdapter();
	}

	public override DbParameter CreateParameter()
	{
		return new OracleParameter();
	}

	public override DbConnectionStringBuilder CreateConnectionStringBuilder()
	{
		return new OracleConnectionStringBuilder();
	}

	public override DbCommandBuilder CreateCommandBuilder()
	{
		OracleCommandBuilder oracleCommandBuilder = new OracleCommandBuilder();
		oracleCommandBuilder.Quoted = true;
		return oracleCommandBuilder;
	}

	public override DbDataSourceEnumerator CreateDataSourceEnumerator()
	{
		return new OracleDataSourceEnumerator();
	}

	object IServiceProvider.GetService(Type serviceType)
	{
		return null;
	}
}
