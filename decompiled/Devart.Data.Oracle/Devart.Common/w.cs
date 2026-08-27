using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace Devart.Common;

internal class w : ISinglePhaseNotification, IPromotableSinglePhaseNotification
{
	private DbConnectionInternal m_a;

	private IsolationLevel m_b;

	private EventHandler m_c;

	public w(DbConnectionInternal A_0, IsolationLevel A_1)
	{
		this.m_a = A_0;
		this.m_b = A_1;
	}

	[SpecialName]
	public void a(EventHandler A_0)
	{
		EventHandler eventHandler = this.m_c;
		EventHandler eventHandler2;
		do
		{
			eventHandler2 = eventHandler;
			EventHandler value = (EventHandler)Delegate.Combine(eventHandler2, A_0);
			eventHandler = Interlocked.CompareExchange(ref this.m_c, value, eventHandler2);
		}
		while ((object)eventHandler != eventHandler2);
	}

	[SpecialName]
	public void b(EventHandler A_0)
	{
		EventHandler eventHandler = this.m_c;
		EventHandler eventHandler2;
		do
		{
			eventHandler2 = eventHandler;
			EventHandler value = (EventHandler)Delegate.Remove(eventHandler2, A_0);
			eventHandler = Interlocked.CompareExchange(ref this.m_c, value, eventHandler2);
		}
		while ((object)eventHandler != eventHandler2);
	}

	public void a(Guid A_0)
	{
		this.m_a.BeginTransaction(A_0, this.m_b);
	}

	private void c(Enlistment A_0)
	{
		try
		{
			this.m_a.Commit();
		}
		catch
		{
		}
		A_0.Done();
		b();
	}

	void IEnlistmentNotification.Commit(Enlistment A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		this.c(A_0);
	}

	private void b(Enlistment A_0)
	{
		A_0.Done();
		b();
	}

	void IEnlistmentNotification.InDoubt(Enlistment A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0);
	}

	private void a(PreparingEnlistment A_0)
	{
		bool flag = false;
		try
		{
			this.m_a.PrepareCommit();
			flag = true;
			A_0.Prepared();
		}
		catch
		{
			if (flag)
			{
				throw;
			}
			A_0.ForceRollback();
		}
	}

	void IEnlistmentNotification.Prepare(PreparingEnlistment A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	private void a(Enlistment A_0)
	{
		lock (this.m_a)
		{
			try
			{
				this.m_a.Rollback();
			}
			catch
			{
			}
			A_0.Done();
		}
		b();
	}

	void IEnlistmentNotification.Rollback(Enlistment A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	private void a(SinglePhaseEnlistment A_0)
	{
		lock (this.m_a)
		{
			this.m_a.Commit();
			A_0.Committed();
		}
		b();
	}

	void ISinglePhaseNotification.SinglePhaseCommit(SinglePhaseEnlistment A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	private void a()
	{
	}

	void IPromotableSinglePhaseNotification.Initialize()
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a();
	}

	private void c(SinglePhaseEnlistment A_0)
	{
		lock (this.m_a)
		{
			try
			{
				this.m_a.Rollback();
				A_0.Aborted();
			}
			catch (Exception ex)
			{
				A_0.Aborted(ex);
			}
		}
		b();
	}

	void IPromotableSinglePhaseNotification.Rollback(SinglePhaseEnlistment A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		this.c(A_0);
	}

	private void b(SinglePhaseEnlistment A_0)
	{
		lock (this.m_a)
		{
			this.m_a.Commit();
			A_0.Committed();
		}
		b();
	}

	void IPromotableSinglePhaseNotification.SinglePhaseCommit(SinglePhaseEnlistment A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0);
	}

	private byte[] c()
	{
		throw new TransactionPromotionException("There was an error promoting the transaction to a distributed transaction.");
	}

	byte[] ITransactionPromoter.Promote()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		return this.c();
	}

	public void b()
	{
		if (this.m_c != null)
		{
			this.m_c(this, EventArgs.Empty);
		}
	}
}
