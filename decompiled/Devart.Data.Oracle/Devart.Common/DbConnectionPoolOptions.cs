using System;

namespace Devart.Common;

internal sealed class DbConnectionPoolOptions
{
	private readonly int a;

	private readonly bool b;

	private readonly TimeSpan c;

	private readonly int d;

	private readonly int e;

	private readonly bool f;

	private readonly bool g;

	private readonly bool h;

	public int CreationTimeout => a;

	public bool HasTransactionAffinity => b;

	public TimeSpan LoadBalanceTimeout => c;

	public int MaxPoolSize => d;

	public int MinPoolSize => e;

	public bool PoolByIdentity => f;

	public bool UseDeactivateQueue => g;

	public bool UseLoadBalancing => h;

	public DbConnectionPoolOptions(bool poolByIdentity, int minPoolSize, int maxPoolSize, int creationTimeout, int loadBalanceTimeout, bool hasTransactionAffinity, bool useDeactivateQueue)
	{
		f = poolByIdentity;
		e = minPoolSize;
		d = maxPoolSize;
		a = creationTimeout;
		if (loadBalanceTimeout != 0)
		{
			c = new TimeSpan(0, 0, loadBalanceTimeout);
			h = true;
		}
		b = hasTransactionAffinity;
		g = useDeactivateQueue;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is DbConnectionPoolOptions dbConnectionPoolOptions))
		{
			return false;
		}
		if (a == dbConnectionPoolOptions.a && b == dbConnectionPoolOptions.b && c == dbConnectionPoolOptions.c && d == dbConnectionPoolOptions.d && e == dbConnectionPoolOptions.e && f == dbConnectionPoolOptions.f && g == dbConnectionPoolOptions.g)
		{
			return h == dbConnectionPoolOptions.h;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
