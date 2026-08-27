using System;
using System.Collections.Generic;
using System.Transactions;

namespace Devart.Common;

internal class ah
{
	private static Dictionary<Transaction, ah> m_a;

	private Dictionary<DbConnectionOptions, List<DbConnectionInternal>> m_b;

	public ah()
	{
		this.m_b = new Dictionary<DbConnectionOptions, List<DbConnectionInternal>>();
	}

	public static bool d(Transaction A_0)
	{
		return c(A_0)?.a() ?? false;
	}

	public static DbConnectionInternal a(Transaction A_0, DbConnectionOptions A_1)
	{
		return c(A_0)?.a(A_1);
	}

	public static void a(Transaction A_0, DbConnectionInternal A_1)
	{
		ah ah2 = b(A_0);
		ah2.a(A_1);
	}

	public static ah c(Transaction A_0)
	{
		ah.m_a.TryGetValue(A_0, out var value);
		return value;
	}

	public static ah b(Transaction A_0)
	{
		lock (ah.m_a)
		{
			if (!ah.m_a.TryGetValue(A_0, out var value))
			{
				value = new ah();
				ah.m_a.Add(A_0, value);
				A_0.TransactionCompleted += a;
			}
			return value;
		}
	}

	private static void a(object A_0, TransactionEventArgs A_1)
	{
		a(A_1.Transaction);
	}

	private static void a(Transaction A_0)
	{
		lock (ah.m_a)
		{
			ah.m_a.Remove(A_0);
		}
	}

	public void a(DbConnectionInternal A_0)
	{
		lock (this.m_b)
		{
			DbConnectionOptions key = ((DbConnectionBase)A_0.Owner).ConnectionOptions;
			if (!this.m_b.TryGetValue(key, out var value))
			{
				value = new List<DbConnectionInternal>();
				this.m_b.Add(key, value);
			}
			value.Add(A_0);
		}
	}

	public bool a()
	{
		return this.m_b.Count > 0;
	}

	public DbConnectionInternal a(DbConnectionOptions A_0)
	{
		lock (this.m_b)
		{
			if (!this.m_b.TryGetValue(A_0, out var value))
			{
				if (A_0.TransactionScopeLocal && this.m_b.Count > 0)
				{
					throw new NotSupportedException("Connections with different connection strings inside the same local transaction are not currently supported.");
				}
				return null;
			}
			foreach (DbConnectionInternal item in value)
			{
				if (item.ConnectionIsClosedAndDeffered)
				{
					item.ConnectionIsClosedAndDeffered = false;
					return item;
				}
			}
			if (A_0.TransactionScopeLocal)
			{
				throw new NotSupportedException("Multiple simultaneous connections inside the same local transaction are not currently supported.");
			}
			return null;
		}
	}

	static ah()
	{
		ah.m_a = new Dictionary<Transaction, ah>();
	}
}
