using System;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Transactions;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class ap : DbConnectionInternal
{
	private new readonly g m_a;

	private new bool m_b;

	private new OracleInfoMessageEventArgs c;

	public ap(ay A_0, ap A_1)
	{
		A_0.UsersConnectionString(hidePassword: true);
		try
		{
			base.m = A_0;
			g g2 = null;
			string text = A_0.f();
			OracleHomeCollection oracleHomeCollection = OracleHomeCollection.SingletonInstance;
			OracleHome oracleHome = ((text == string.Empty) ? oracleHomeCollection.DefaultHome : oracleHomeCollection[text]);
			if (oracleHome == null)
			{
				throw new OracleException(-1, string.Format(Devart.Common.al.a("CanNotFoundOracleHome"), text));
			}
			bool a_ = !A_0.g() && OracleUtils.UseSeparateOCIEnvironment;
			aq aq2 = Devart.Data.Oracle.aa.a(A_0.u(), A_1: true, oracleHome, a_);
			if (A_1 != null)
			{
				g2 = A_1.l();
			}
			this.m_b = g2 != null;
			this.m_a = aq2.b();
			this.m_a.c(A_0.v());
			this.m_a.b(A_0.y());
			this.m_a.a(A_0, g2);
			c = this.m_a.t();
		}
		catch (Exception a_2)
		{
			a(a_2);
			if (this.m_a != null)
			{
				try
				{
					this.m_a.c();
				}
				catch
				{
				}
			}
			throw;
		}
	}

	protected override void a(bool A_0)
	{
		if (A_0)
		{
			try
			{
				this.m_a.d();
			}
			catch (Exception)
			{
				throw;
			}
		}
	}

	public new OracleInfoMessageEventArgs j()
	{
		OracleInfoMessageEventArgs result = c;
		c = null;
		return result;
	}

	[SpecialName]
	protected virtual bool f()
	{
		return b().Enlist;
	}

	public virtual void u()
	{
		DbConnectionBase dbConnectionBase = (DbConnectionBase)base.Owner;
		if (dbConnectionBase == null)
		{
			this.m_a.l();
			return;
		}
		using (dbConnectionBase.LocalFailoverManager.StartUse())
		{
			try
			{
				this.m_a.l();
			}
			catch (Exception ex)
			{
				dbConnectionBase.LocalFailoverManager.DoLocalFailoverEvent(dbConnectionBase, ConnectionLostCause.Execute, RetryMode.Raise, ex);
				throw;
			}
		}
	}

	public virtual void v()
	{
		DbConnectionBase dbConnectionBase = (DbConnectionBase)base.Owner;
		if (dbConnectionBase == null)
		{
			try
			{
				this.m_a.o();
				return;
			}
			catch (Exception)
			{
				throw;
			}
		}
		using (dbConnectionBase.LocalFailoverManager.StartUse())
		{
			try
			{
				this.m_a.o();
			}
			catch (Exception ex2)
			{
				dbConnectionBase.LocalFailoverManager.DoLocalFailoverEvent(dbConnectionBase, ConnectionLostCause.Execute, RetryMode.Raise, ex2);
				throw;
			}
		}
	}

	public virtual void a(Guid A_0, System.Transactions.IsolationLevel A_1)
	{
		base.LastOwner = base.Owner;
		this.m_a.a(A_0, A_1);
	}

	[SpecialName]
	public virtual bool m()
	{
		return false;
	}

	[SpecialName]
	public virtual bool q()
	{
		return !b().TransactionScopeLocal;
	}

	public virtual void k()
	{
		DbConnectionBase dbConnectionBase = (DbConnectionBase)base.Owner;
		if (dbConnectionBase == null)
		{
			this.m_a.e();
			return;
		}
		using (dbConnectionBase.LocalFailoverManager.StartUse())
		{
			try
			{
				this.m_a.e();
			}
			catch (Exception ex)
			{
				dbConnectionBase.LocalFailoverManager.DoLocalFailoverEvent(dbConnectionBase, ConnectionLostCause.Execute, RetryMode.Raise, ex);
				throw;
			}
		}
	}

	public virtual void a(string A_0)
	{
		if (A_0 == null || string.IsNullOrEmpty(A_0.Trim()))
		{
			throw new ArgumentException("Schema name cannot be null, the empty string, or contain only whitespace.");
		}
		DbConnectionBase dbConnectionBase = (DbConnectionBase)base.Owner;
		if (dbConnectionBase == null)
		{
			this.m_a.a(A_0);
			return;
		}
		using (dbConnectionBase.LocalFailoverManager.StartUse())
		{
			try
			{
				this.m_a.a(A_0);
			}
			catch (Exception ex)
			{
				dbConnectionBase.LocalFailoverManager.DoLocalFailoverEvent(dbConnectionBase, ConnectionLostCause.Execute, RetryMode.Raise, ex);
				throw;
			}
		}
	}

	internal override DbReferenceCollection d()
	{
		return new ba();
	}

	[SpecialName]
	public virtual string t()
	{
		return this.m_a.m();
	}

	[SpecialName]
	public string r()
	{
		int num = this.m_a.h().d();
		return string.Format("{0}.{1}.{2}.{3}", new object[4]
		{
			num / 1000000 % 100,
			num / 10000 % 100,
			num / 100 % 100,
			num % 100
		});
	}

	[SpecialName]
	public virtual string x()
	{
		return OracleUtils.c(ServerVersion);
	}

	private OracleConnection g()
	{
		OracleConnection oracleConnection = (OracleConnection)base.Owner;
		if (oracleConnection == null && base.Transaction == null)
		{
			throw new InvalidOperationException(Devart.Common.al.a("InternalConnectionWithoutProxy"));
		}
		return oracleConnection;
	}

	public virtual DbTransaction a(System.Data.IsolationLevel A_0)
	{
		return new OracleTransaction(g(), A_0);
	}

	internal void a(Exception A_0)
	{
		if (A_0 is OracleException ex && Oci.IsFatalError(ex))
		{
			ad();
		}
	}

	[SpecialName]
	public new HandleRef o()
	{
		return ((v)this.m_a).r();
	}

	[SpecialName]
	public HandleRef h()
	{
		return ((v)this.m_a).h().h();
	}

	[SpecialName]
	public new HandleRef n()
	{
		return ((v)this.m_a).h().k();
	}

	protected override void a()
	{
		if (base.o != null)
		{
			base.a();
			if (b().w())
			{
				((v)this.m_a).s();
			}
		}
	}

	[SpecialName]
	public aq w()
	{
		return this.m_a.h();
	}

	[SpecialName]
	public g l()
	{
		return this.m_a;
	}

	[SpecialName]
	public virtual ConnectionState p()
	{
		return ConnectionState.Open;
	}

	protected override void a(object A_0)
	{
	}

	public virtual void i()
	{
		DbConnectionBase dbConnectionBase = (DbConnectionBase)base.Owner;
		if (dbConnectionBase == null)
		{
			return;
		}
		if (base.Transaction != null)
		{
			base.ConnectionIsClosedAndDeffered = true;
			dbConnectionBase.a(DbConnectionClosed.d, A_1: true);
			base.Owner = null;
			return;
		}
		OracleConnection oracleConnection = (OracleConnection)dbConnectionBase;
		try
		{
			if (oracleConnection.b != null || b().ad())
			{
				oracleConnection.Rollback();
			}
		}
		catch
		{
		}
		base.Close();
	}

	public bool s()
	{
		if (this.m_a != null)
		{
			try
			{
				s s2 = this.m_a.h().a(this.m_a, A_1: false);
				try
				{
					s2.a("begin null; end;");
					s2.a(1, az.a);
					return true;
				}
				finally
				{
					s2.l();
				}
			}
			catch
			{
			}
		}
		ad();
		return false;
	}

	[SpecialName]
	protected ay b()
	{
		return (ay)base.ConnectionOptions;
	}
}
