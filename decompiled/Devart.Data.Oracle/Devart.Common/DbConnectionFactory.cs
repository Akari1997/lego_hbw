using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Devart.Common;

internal abstract class DbConnectionFactory
{
	private readonly ArrayList m_a;

	public abstract DbProviderFactory ProviderFactory { get; }

	protected DbConnectionFactory()
	{
		this.m_a = new ArrayList();
	}

	public void ClearAllPools()
	{
		ClearAllPools(forced: false);
	}

	public void ClearAllPools(bool forced)
	{
		DbConnectionPoolGroup[] array;
		lock (this.m_a)
		{
			array = new DbConnectionPoolGroup[this.m_a.Count];
			this.m_a.CopyTo(array, 0);
		}
		DbConnectionPoolGroup[] array2 = array;
		foreach (DbConnectionPoolGroup dbConnectionPoolGroup in array2)
		{
			dbConnectionPoolGroup.Clear(forced);
		}
	}

	public void ClearPool(DbConnectionBase connection)
	{
		Utils.CheckArgumentNull(connection, "connection");
		connection.Pool?.Clear();
	}

	public void ClearPool(string connectionString)
	{
		Utils.CheckArgumentNull(connectionString, "connectionString");
		a(connectionString)?.Clear();
	}

	protected abstract DbConnectionInternal a(DbConnectionOptions A_0, object A_1, DbConnectionBase A_2);

	protected abstract DbConnectionOptions a(string A_0, DbConnectionOptions A_1);

	protected abstract DbConnectionPoolOptions a(DbConnectionOptions A_0);

	protected virtual DbMetaDataFactory b(DbConnectionInternal A_0)
	{
		throw new NotSupportedException();
	}

	internal DbConnectionInternal a(DbConnectionBase A_0, DbConnectionOptions A_1)
	{
		if (A_1 == null)
		{
			throw new InvalidOperationException(al.a("ConnectionStringNotInitialized"));
		}
		A_1.ToString();
		DbConnectionInternal dbConnectionInternal = null;
		try
		{
			dbConnectionInternal = a(A_1, null, A_0);
			if (dbConnectionInternal != null)
			{
				dbConnectionInternal.e(A_0);
				dbConnectionInternal.n = A_0;
			}
		}
		catch (Exception)
		{
			throw;
		}
		return dbConnectionInternal;
	}

	internal DbConnectionInternal a(DbConnectionPool A_0, DbConnectionOptions A_1, DbConnectionBase A_2)
	{
		DbConnectionInternal dbConnectionInternal = null;
		try
		{
			dbConnectionInternal = a(A_1, null, A_2);
			if (dbConnectionInternal != null)
			{
				dbConnectionInternal.a(A_0);
				lock (dbConnectionInternal)
				{
					dbConnectionInternal.d(null);
				}
			}
		}
		catch (Exception)
		{
			throw;
		}
		return dbConnectionInternal;
	}

	internal DbConnectionInternal b(DbConnectionBase A_0)
	{
		DbConnectionInternal dbConnectionInternal = a(A_0);
		if (dbConnectionInternal != null)
		{
			dbConnectionInternal.Owner = A_0;
			return dbConnectionInternal;
		}
		DbConnectionPool dbConnectionPool = A_0.Pool;
		if (dbConnectionPool == null)
		{
			dbConnectionInternal = a(A_0, A_0.UserConnectionOptions);
		}
		else
		{
			dbConnectionInternal = (DbConnectionInternal)dbConnectionPool.GetObject(A_0);
			if (dbConnectionInternal == null)
			{
				throw new InvalidOperationException(al.a("PooledOpenTimeout"));
			}
			lock (dbConnectionInternal)
			{
				dbConnectionInternal.c(A_0);
			}
		}
		try
		{
			dbConnectionInternal.ai();
			return dbConnectionInternal;
		}
		catch
		{
			if (dbConnectionPool != null)
			{
				lock (dbConnectionInternal)
				{
					dbConnectionPool.RemoveObject(dbConnectionInternal);
				}
			}
			dbConnectionInternal.Dispose();
			throw;
		}
	}

	private DbConnectionInternal a(DbConnectionBase A_0)
	{
		DbConnectionOptions dbConnectionOptions = A_0.ConnectionOptions;
		if (dbConnectionOptions != null && dbConnectionOptions.Enlist && Transaction.Current != null)
		{
			return ah.a(Transaction.Current, dbConnectionOptions);
		}
		return null;
	}

	private DbConnectionPool a(string A_0)
	{
		DbConnectionOptions dbConnectionOptions = a(A_0, null);
		if (dbConnectionOptions == null)
		{
			throw new InvalidOperationException(al.a("ConnectionOptionsMissing"));
		}
		DbConnectionPoolGroup[] array;
		lock (this.m_a)
		{
			array = new DbConnectionPoolGroup[this.m_a.Count];
			this.m_a.CopyTo(array, 0);
		}
		DbConnectionPoolOptions obj = a(dbConnectionOptions);
		DbConnectionPoolGroup[] array2 = array;
		foreach (DbConnectionPoolGroup dbConnectionPoolGroup in array2)
		{
			if (dbConnectionPoolGroup.PoolOptions.Equals(obj))
			{
				return dbConnectionPoolGroup.b(dbConnectionOptions);
			}
		}
		return null;
	}

	internal DbConnectionPool a(string A_0, ref DbConnectionOptions A_1)
	{
		DbConnectionOptions dbConnectionOptions;
		if (A_0 == null || A_0 == "")
		{
			if (A_1 == null)
			{
				return null;
			}
			dbConnectionOptions = A_1;
		}
		else
		{
			dbConnectionOptions = a(A_0, A_1);
			if (dbConnectionOptions == null)
			{
				throw new InvalidOperationException(al.a("ConnectionOptionsMissing"));
			}
			A_1 = dbConnectionOptions;
		}
		DbConnectionPoolOptions dbConnectionPoolOptions = a(dbConnectionOptions);
		if (dbConnectionPoolOptions == null)
		{
			return null;
		}
		DbConnectionPoolGroup dbConnectionPoolGroup = null;
		lock (this.m_a)
		{
			foreach (DbConnectionPoolGroup item in this.m_a)
			{
				if (item.PoolOptions.Equals(dbConnectionPoolOptions))
				{
					dbConnectionPoolGroup = item;
					break;
				}
			}
			if (dbConnectionPoolGroup == null)
			{
				dbConnectionPoolGroup = new DbConnectionPoolGroup(this, dbConnectionPoolOptions);
				this.m_a.Add(dbConnectionPoolGroup);
			}
		}
		return dbConnectionPoolGroup.a(dbConnectionOptions);
	}

	internal DbMetaDataFactory a(DbConnectionPool A_0, DbConnectionInternal A_1)
	{
		DbMetaDataFactory dbMetaDataFactory = null;
		if (A_0 != null)
		{
			dbMetaDataFactory = A_0.MetaDataFactory;
		}
		if (dbMetaDataFactory == null)
		{
			dbMetaDataFactory = b(A_1);
			if (A_0 != null)
			{
				A_0.MetaDataFactory = dbMetaDataFactory;
			}
		}
		return dbMetaDataFactory;
	}

	protected internal virtual bool a(DbConnectionInternal A_0)
	{
		return A_0.State == ConnectionState.Open;
	}
}
