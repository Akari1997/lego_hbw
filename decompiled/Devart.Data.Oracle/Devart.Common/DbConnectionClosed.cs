using System;
using System.Data;
using System.Data.Common;

namespace Devart.Common;

internal class DbConnectionClosed : DbConnectionInternal
{
	private new readonly ConnectionState m_a;

	internal new static DbConnectionClosed b = new DbConnectionClosed(ConnectionState.Closed, A_1: true, A_2: false);

	internal new static DbConnectionClosed c;

	internal new static DbConnectionClosed d;

	internal new static DbConnectionClosed e;

	internal static DbConnectionClosed f;

	public override string ServerVersion
	{
		get
		{
			throw new InvalidOperationException(al.a("ClosedConnectionError"));
		}
	}

	public override string ServerVersionNormalized
	{
		get
		{
			throw new InvalidOperationException(al.a("ClosedConnectionError"));
		}
	}

	public override ConnectionState State => this.m_a;

	protected DbConnectionClosed(ConnectionState A_0, bool A_1, bool A_2)
		: base(A_1, A_2)
	{
		this.m_a = A_0;
	}

	protected override void e()
	{
		throw new InvalidOperationException(al.a("ClosedConnectionError"));
	}

	public override DbTransaction BeginTransaction(IsolationLevel il)
	{
		throw new InvalidOperationException(al.a("ClosedConnectionError"));
	}

	public override void ChangeDatabase(string database)
	{
		throw new InvalidOperationException(al.a("ClosedConnectionError"));
	}

	public override void Close()
	{
	}

	protected override void c()
	{
		throw new InvalidOperationException(al.a("ClosedConnectionError"));
	}

	protected override void a(object A_0)
	{
		throw new InvalidOperationException(al.a("ClosedConnectionError"));
	}

	protected internal override DataTable a(DbConnectionBase A_0, string A_1, string[] A_2)
	{
		throw new InvalidOperationException(al.a("ClosedConnectionError"));
	}

	public override void Open(DbConnectionBase outerConnection)
	{
		if (this == f)
		{
			throw new InvalidOperationException(al.a("ConnectionAlreadyOpen"));
		}
		Utils.CheckArgumentNull(outerConnection, "outerConnection");
		if (outerConnection.a(f, this, A_2: false))
		{
			DbConnectionInternal dbConnectionInternal = null;
			try
			{
				DbConnectionFactory dbConnectionFactory = outerConnection.ConnectionFactory;
				dbConnectionInternal = dbConnectionFactory.b(outerConnection);
			}
			catch
			{
				outerConnection.a(this, A_1: false);
				throw;
			}
			if (dbConnectionInternal == null)
			{
				outerConnection.a(this, A_1: false);
				throw new InvalidOperationException(al.a("GetConnectionReturnsNull"));
			}
			outerConnection.a(dbConnectionInternal, A_1: true);
		}
	}

	static DbConnectionClosed()
	{
		DbConnectionClosed.c = new DbConnectionClosed(ConnectionState.Closed, A_1: false, A_2: true);
		d = new DbConnectionClosed(ConnectionState.Closed, A_1: true, A_2: true);
		DbConnectionClosed.e = new DbConnectionClosed(ConnectionState.Open, A_1: true, A_2: false);
		f = new DbConnectionClosed(ConnectionState.Connecting, A_1: true, A_2: false);
	}
}
