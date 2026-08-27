using System;
using System.Collections;
using System.Data;
using System.Threading;

namespace Devart.Common;

internal sealed class DbConnectionPool
{
	private const int m_a = 8;

	private const int m_b = 0;

	private readonly DbConnectionOptions m_c;

	private readonly DbConnectionPoolGroup d;

	private readonly int e;

	private readonly int f;

	private readonly int[] g;

	private readonly int[] h;

	private readonly object[] i;

	private readonly object j;

	private readonly object k;

	private readonly bool l;

	private readonly TimeSpan m;

	private readonly TimeSpan n;

	private readonly AutoResetEvent o;

	private readonly ArrayList p;

	private DbMetaDataFactory q;

	private Timer r;

	private bool s;

	private int t;

	private int u;

	private int v;

	private int w;

	private int x;

	private int y;

	private int z;

	private bool aa;

	public bool Active => s;

	internal DbMetaDataFactory MetaDataFactory
	{
		get
		{
			return q;
		}
		set
		{
			q = value;
		}
	}

	internal int TotalCount => u;

	internal DbConnectionOptions ConnectionOptions => this.m_c;

	internal ArrayList Objects => p;

	internal int MinPoolSize => f;

	public int Version => z;

	public int Count => v;

	public DbConnectionPoolGroup PoolGroup => d;

	internal DbConnectionPool(DbConnectionPoolGroup A_0, DbConnectionOptions A_1)
	{
		d = A_0;
		this.m_c = A_1;
		e = A_0.PoolOptions.MaxPoolSize;
		f = A_0.PoolOptions.MinPoolSize;
		n = new TimeSpan(0, 0, A_0.PoolOptions.CreationTimeout);
		l = A_0.PoolOptions.UseLoadBalancing;
		m = A_0.PoolOptions.LoadBalanceTimeout;
		i = new object[e];
		g = new int[e];
		h = new int[8];
		j = new object();
		k = new object();
		o = new AutoResetEvent(initialState: false);
		p = new ArrayList(e);
	}

	public object GetObject(DbConnectionBase owningConnection)
	{
		lock (j)
		{
			if (!Active)
			{
				Startup();
			}
			while (u < f)
			{
				object value = a(owningConnection);
				lock (k)
				{
					Interlocked.Exchange(ref i[y], value);
					Interlocked.Exchange(ref g[y], z);
					if (++y == e)
					{
						y = 0;
					}
					int num = Interlocked.Increment(ref v);
					if (num < w + f)
					{
						w = num - f;
					}
				}
				Interlocked.Increment(ref u);
			}
			object obj;
			if (v > 0)
			{
				_ = g[x];
				obj = Interlocked.Exchange(ref i[x], null);
				if (++x == e)
				{
					x = 0;
				}
				Interlocked.Decrement(ref v);
				if (NeedValidateOnGet())
				{
					if (d.a(obj, A_1: true))
					{
						PoolValidated();
					}
					else
					{
						b(obj);
						obj = a(owningConnection);
					}
				}
				if (!c(obj))
				{
					b(obj);
					obj = a(owningConnection);
				}
				return obj;
			}
			if (u == e)
			{
				if (Utils.WaitOne(o, n, exitContext: false))
				{
					return GetObject(owningConnection);
				}
				return null;
			}
			obj = a(owningConnection);
			Interlocked.Increment(ref u);
			return obj;
		}
	}

	public bool PutObject(object value)
	{
		DbConnectionInternal dbConnectionInternal = (DbConnectionInternal)value;
		if (!dbConnectionInternal.CanBePooled || (l && dbConnectionInternal.CreateTime.Add(m) < DateTime.UtcNow) || dbConnectionInternal.State != ConnectionState.Open)
		{
			RemoveObject(value);
			return false;
		}
		PutObject(value, Interlocked.Increment(ref z));
		return true;
	}

	public void PutObject(object value, int version)
	{
		if (value is DbConnectionInternal { State: not ConnectionState.Open })
		{
			throw new InvalidOperationException("Trying to put closed connection to pool");
		}
		lock (k)
		{
			Interlocked.Exchange(ref i[y], value);
			Interlocked.Exchange(ref g[y], version);
			if (++y == e)
			{
				y = 0;
			}
			int num = Interlocked.Increment(ref v);
			if (num < w + f)
			{
				w = num - f;
			}
			o.Set();
		}
	}

	public bool NeedValidateOnGet()
	{
		if (!aa)
		{
			return this.m_c.ValidateConnection;
		}
		return true;
	}

	public bool NeedValidateOnPruneConnPool()
	{
		return !this.m_c.ValidateConnection;
	}

	public void MarkInvalidVersion()
	{
		lock (k)
		{
			aa = true;
		}
	}

	public void PoolValidated()
	{
		lock (k)
		{
			aa = false;
		}
	}

	private bool c(object A_0)
	{
		DbConnectionInternal dbConnectionInternal = (DbConnectionInternal)A_0;
		return dbConnectionInternal.State == ConnectionState.Open;
	}

	public void EnqueueStatistics(out int firstVersion, out int lastVersion, out int position, out int doomed)
	{
		int num = h.Length - 1;
		firstVersion = h[0];
		lastVersion = h[num];
		Array.Copy(h, 0, h, 1, num);
		h[0] = Version;
		lock (k)
		{
			doomed = (w + 8 - 2) / 8;
			w = Count - f - doomed;
			position = y;
		}
	}

	public object PeekObject(int checkVersion, int position, out int version)
	{
		lock (j)
		{
			if (v > 0 && (x != position || v == e) && g[x] - checkVersion <= 0)
			{
				object result = Interlocked.Exchange(ref i[x], null);
				version = g[x];
				if (++x == e)
				{
					x = 0;
				}
				Interlocked.Decrement(ref v);
				return result;
			}
			version = 0;
			return null;
		}
	}

	public void DoomObject()
	{
		object obj = null;
		lock (j)
		{
			if (v > 0)
			{
				obj = Interlocked.Exchange(ref i[x], null);
				if (++x == e)
				{
					x = 0;
				}
				Interlocked.Decrement(ref v);
			}
		}
		if (obj != null)
		{
			RemoveObject(obj);
		}
	}

	private void b(object A_0)
	{
		lock (p)
		{
			p.Remove(A_0);
		}
		d.b(A_0);
	}

	public void RemoveObject(object connection)
	{
		lock (p)
		{
			p.Remove(connection);
		}
		Interlocked.Decrement(ref u);
		o.Set();
		d.b(connection);
	}

	private object a(DbConnectionBase A_0)
	{
		object obj = d.a(this, A_0);
		lock (p)
		{
			p.Add(obj);
			return obj;
		}
	}

	public void Clear()
	{
		Clear(0, forced: false);
	}

	public void Clear(int cleanupWait, bool forced)
	{
		t = Version;
		if (forced)
		{
			a((object)null);
		}
		else
		{
			r = a(cleanupWait);
		}
	}

	private Timer a(int A_0)
	{
		return new Timer(a, null, A_0, A_0);
	}

	private void a(object A_0)
	{
		int num = v;
		for (int num2 = 0; num2 < num; num2++)
		{
			object obj = PeekObject(t, -1, out var _);
			if (obj == null)
			{
				break;
			}
			RemoveObject(obj);
		}
		Timer timer = r;
		r = null;
		timer?.Dispose();
	}

	public void Startup()
	{
		d.b(this);
		s = true;
	}

	public bool Shutdown()
	{
		lock (j)
		{
			if (u == 0)
			{
				s = false;
				return true;
			}
			return false;
		}
	}
}
