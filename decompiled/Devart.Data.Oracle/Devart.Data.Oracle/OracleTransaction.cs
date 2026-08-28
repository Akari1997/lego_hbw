using System;
using System.Data;
using System.Data.Common;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleTransaction : DbTransactionBase
{
	private OracleConnection m_a;

	public new OracleConnection Connection => this.m_a;

	protected override DbConnection DbConnection => this.m_a;

	internal OracleTransaction(OracleConnection A_0, IsolationLevel A_1)
	{
		ap ap2 = A_0.d();
		g g2 = ap2.l();
		try
		{
			if (ap2.Transaction != null)
			{
				throw new InvalidOperationException("Local transaction can not be started while in a distributed transaction.");
			}
			this.m_a = A_0;
			if (A_1 == IsolationLevel.Unspecified)
			{
				A_1 = IsolationLevel.ReadCommitted;
			}
			isolationLevel = A_1;
			g2.l();
			using IDbCommand dbCommand = A_0.CreateCommand();
			switch (A_1)
			{
			case IsolationLevel.Serializable:
				dbCommand.CommandText = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE";
				dbCommand.ExecuteNonQuery();
				break;
			default:
				throw new ArgumentException(string.Format(Devart.Common.al.a("TransactionIsolationLevelNotSupported"), A_1.ToString()));
			case IsolationLevel.ReadCommitted:
				break;
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (!isDisposed && disposing)
		{
			a();
			b();
		}
	}

	private void b()
	{
		if (this.m_a != null)
		{
			this.m_a.b = null;
			if (this.m_a.InnerConnection is ap ap2)
			{
				ap2.l();
			}
		}
		this.m_a = null;
		isolationLevel = IsolationLevel.Unspecified;
		isDisposed = true;
		GC.SuppressFinalize(this);
	}

	public override void Commit()
	{
		CheckDisposed();
		ap ap2 = this.m_a.d();
		OnStateChanging(TransactionAction.Commit, this.m_a);
		try
		{
			ap2.Commit();
			OnStateChanged(TransactionAction.Commit, this.m_a);
		}
		catch (Exception ex)
		{
			if (ex is OracleException a_)
			{
				this.m_a.a(this, a_);
			}
			ap2.a(ex);
			throw;
		}
		finally
		{
			b();
		}
	}

	public override void Rollback()
	{
		CheckDisposed();
		Dispose();
	}

	private void a()
	{
		ap ap2 = this.m_a.d();
		OnStateChanging(TransactionAction.Rollback, this.m_a);
		try
		{
			ap2.Rollback();
			OnStateChanged(TransactionAction.Rollback, this.m_a);
		}
		catch (Exception ex)
		{
			if (ex is OracleException a_)
			{
				this.m_a.a(this, a_);
			}
			ap2.a(ex);
			throw;
		}
	}

	public new void Rollback(string savePointName)
	{
		CheckDisposed();
		OnStateChanging(TransactionAction.RollbackToSavepoint, this.m_a);
		using (OracleCommand oracleCommand = this.m_a.CreateCommand())
		{
			oracleCommand.CommandText = "ROLLBACK TO SAVEPOINT " + savePointName;
			oracleCommand.ExecuteNonQuery();
		}
		OnStateChanged(TransactionAction.RollbackToSavepoint, this.m_a);
	}

	public new void Save(string savePointName)
	{
		CheckDisposed();
		OnStateChanging(TransactionAction.Savepoint, this.m_a);
		using (OracleCommand oracleCommand = this.m_a.CreateCommand())
		{
			oracleCommand.CommandText = "SAVEPOINT " + savePointName;
			oracleCommand.ExecuteNonQuery();
		}
		OnStateChanged(TransactionAction.Savepoint, this.m_a);
	}
}
