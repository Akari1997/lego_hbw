using System.Data;
using System.Data.Common;
using System.IO;

namespace Devart.Common;

internal class DbMetaDataFactory
{
	public DbMetaDataFactory(Stream xmlStream, string serverVersion, string normalizedServerVersion)
	{
	}

	public virtual DataTable GetSchema(DbConnection connection, DbConnectionInternal internalConnection, string collectionName, string[] restrictions)
	{
		return null;
	}
}
