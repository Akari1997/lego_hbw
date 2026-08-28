using System;
using System.Collections;
using System.Threading;

namespace Devart.Common;

internal class DbConnectionPoolGroup
{
	private const int m_a = 60000;

	private const int m_b = 30000;

	private const int c = 10000;

	private const int d = 40000;

	private readonly DbConnectionPoolOptions e;

	private readonly DbConnectionFactory f;

	private readonly ArrayList g;

	private readonly ArrayList h;

	private readonly Timer i;

	private int j;

	private static readonly a k = new a();

	public DbConnectionPoolOptions PoolOptions => e;

	public DbConnectionFactory ConnectionFactory => f;

	public DbConnectionPoolGroup(DbConnectionFactory connectionFactory, DbConnectionPoolOptions poolOptions)
	{
		e = poolOptions;
		f = connectionFactory;
		g = new ArrayList();
		h = new ArrayList();
		i = a();
	}

	public void Clear(bool forced)
	{
		lock (h)
		{
			foreach (DbConnectionPool item in h)
			{
				int cleanupWait = k.a(10000, 40000);
				item.Clear(cleanupWait, forced);
			}
		}
	}

	private Timer a()
	{
		TimerCallback callback = a;
		return new Timer(callback, null, 60000, 30000);
	}

	internal DbConnectionPool b(DbConnectionOptions A_0)
	{
		DbConnectionPool[] array;
		lock (h)
		{
			array = new DbConnectionPool[h.Count];
			h.CopyTo(array, 0);
		}
		DbConnectionPool[] array2 = array;
		foreach (DbConnectionPool dbConnectionPool in array2)
		{
			if (dbConnectionPool.ConnectionOptions.Equals(A_0))
			{
				return dbConnectionPool;
			}
		}
		return null;
	}

	internal DbConnectionPool a(DbConnectionOptions A_0)
	{
		lock (g)
		{
			DbConnectionPool dbConnectionPool;
			foreach (WeakReference item in g)
			{
				if (!item.IsAlive)
				{
					continue;
				}
				try
				{
					dbConnectionPool = (DbConnectionPool)item.Target;
					if (dbConnectionPool != null && dbConnectionPool.ConnectionOptions.Equals(A_0))
					{
						return dbConnectionPool;
					}
				}
				catch
				{
				}
			}
			dbConnectionPool = new DbConnectionPool(this, A_0);
			g.Add(new WeakReference(dbConnectionPool));
			return dbConnectionPool;
		}
	}

	private void a(object A_0)
	{
		if (j != 0)
		{
			return;
		}
		Interlocked.Increment(ref j);
		try
		{
			DbConnectionPool[] array;
			lock (h)
			{
				array = new DbConnectionPool[h.Count];
				h.CopyTo(array, 0);
			}
			DbConnectionPool[] array2 = array;
			foreach (DbConnectionPool dbConnectionPool in array2)
			{
				a(dbConnectionPool);
				if (dbConnectionPool.TotalCount != 0)
				{
					continue;
				}
				lock (h)
				{
					if (dbConnectionPool.Shutdown())
					{
						h.Remove(dbConnectionPool);
					}
				}
			}
			lock (g)
			{
				int num2 = 0;
				while (num2 < g.Count)
				{
					WeakReference weakReference = (WeakReference)g[num2];
					if (!weakReference.IsAlive)
					{
						g.RemoveAt(num2);
					}
					else
					{
						num2++;
					}
				}
			}
		}
		finally
		{
			Interlocked.Decrement(ref j);
		}
	}

	private void a(DbConnectionPool A_0)
	{
		ArrayList arrayList;
		lock (arrayList = A_0.Objects)
		{
			int num = 0;
			while (num < arrayList.Count)
			{
				DbConnectionInternal dbConnectionInternal = (DbConnectionInternal)arrayList[num];
				if (dbConnectionInternal.IsEmancipated)
				{
					dbConnectionInternal.d(null);
					if (!dbConnectionInternal.CanBePooled)
					{
						A_0.RemoveObject(dbConnectionInternal);
						continue;
					}
					if (!A_0.PutObject(dbConnectionInternal))
					{
						continue;
					}
				}
				num++;
			}
		}
		A_0.EnqueueStatistics(out var firstVersion, out var lastVersion, out var position, out var doomed);
		int count = A_0.Count;
		int num2 = 0;
		for (int num3 = 0; num3 < count; num3++)
		{
			object obj = A_0.PeekObject(firstVersion, position, out var version);
			if (obj == null)
			{
				break;
			}
			if (version - lastVersion <= 0 && A_0.TotalCount > A_0.MinPoolSize)
			{
				A_0.RemoveObject(obj);
				num2++;
			}
			else if (a(obj, A_0.NeedValidateOnPruneConnPool()))
			{
				A_0.PutObject(obj, version);
			}
			else
			{
				A_0.RemoveObject(obj);
				num2++;
			}
		}
		if (num2 < doomed)
		{
			doomed -= num2;
			for (int num4 = 0; num4 < doomed; num4++)
			{
				A_0.DoomObject();
			}
		}
		A_0.PoolValidated();
	}

	internal void b(DbConnectionPool A_0)
	{
		lock (h)
		{
			h.Add(A_0);
		}
	}

	internal bool a(object A_0, bool A_1)
	{
		if (A_1)
		{
			return f.a((DbConnectionInternal)A_0);
		}
		return true;
	}

	internal object a(DbConnectionPool A_0, DbConnectionBase A_1)
	{
		return f.a(A_0, A_0.ConnectionOptions, A_1);
	}

	internal void b(object A_0)
	{
		try
		{
			((DbConnectionInternal)A_0).Dispose();
		}
		catch
		{
		}
	}
}
