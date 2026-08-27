using System.Data.Common;
using System.Runtime.CompilerServices;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class u : DbConnectionFactory
{
	private new const int m_a = 30;

	public new static u b;

	private u()
	{
	}

	protected override DbConnectionOptions a(string A_0, DbConnectionOptions A_1)
	{
		return new ay(A_0);
	}

	protected override DbConnectionPoolOptions a(DbConnectionOptions A_0)
	{
		ay ay2 = (ay)A_0;
		if (ay2.ad() && !ay2.g())
		{
			return new DbConnectionPoolOptions(poolByIdentity: false, ay2.p(), ay2.o(), ay2.v(), ay2.x(), hasTransactionAffinity: false, useDeactivateQueue: true);
		}
		return null;
	}

	[SpecialName]
	public virtual DbProviderFactory a()
	{
		return OracleProviderFactory.Instance;
	}

	protected override DbMetaDataFactory b(DbConnectionInternal A_0)
	{
		return new q(A_0.ServerVersion, A_0.ServerVersionNormalized);
	}

	protected override DbConnectionInternal a(DbConnectionOptions A_0, object A_1, DbConnectionBase A_2)
	{
		ap a_ = null;
		if (A_2 != null)
		{
			OracleConnection oracleConnection = ((OracleConnection)A_2).l;
			if (oracleConnection != null)
			{
				a_ = oracleConnection.d();
			}
		}
		return new ap((ay)A_0, a_);
	}

	protected internal override bool a(DbConnectionInternal A_0)
	{
		try
		{
			((ap)A_0).Commit();
		}
		catch
		{
			return false;
		}
		return true;
	}

	static u()
	{
		u.b = new u();
	}
}
