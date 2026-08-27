using System;
using System.Data;
using System.Data.Common;
using System.Threading;

namespace Devart.Common;

public abstract class DbTransactionBase : DbTransaction
{
	protected IsolationLevel isolationLevel;

	protected bool isDisposed;

	private string a;

	private TransactionStateChangingEventHandler b;

	private TransactionStateChangedEventHandler c;

	private string AlreadyDisposedErrorMessage
	{
		get
		{
			if (a == null)
			{
				a = GetType().Name + " is disposed.";
			}
			return a;
		}
	}

	public override IsolationLevel IsolationLevel => isolationLevel;

	public event TransactionStateChangingEventHandler StateChanging
	{
		add
		{
			TransactionStateChangingEventHandler transactionStateChangingEventHandler = b;
			TransactionStateChangingEventHandler transactionStateChangingEventHandler2;
			do
			{
				transactionStateChangingEventHandler2 = transactionStateChangingEventHandler;
				TransactionStateChangingEventHandler value2 = (TransactionStateChangingEventHandler)Delegate.Combine(transactionStateChangingEventHandler2, value);
				transactionStateChangingEventHandler = Interlocked.CompareExchange(ref b, value2, transactionStateChangingEventHandler2);
			}
			while ((object)transactionStateChangingEventHandler != transactionStateChangingEventHandler2);
		}
		remove
		{
			TransactionStateChangingEventHandler transactionStateChangingEventHandler = b;
			TransactionStateChangingEventHandler transactionStateChangingEventHandler2;
			do
			{
				transactionStateChangingEventHandler2 = transactionStateChangingEventHandler;
				TransactionStateChangingEventHandler value2 = (TransactionStateChangingEventHandler)Delegate.Remove(transactionStateChangingEventHandler2, value);
				transactionStateChangingEventHandler = Interlocked.CompareExchange(ref b, value2, transactionStateChangingEventHandler2);
			}
			while ((object)transactionStateChangingEventHandler != transactionStateChangingEventHandler2);
		}
	}

	public event TransactionStateChangedEventHandler StateChanged
	{
		add
		{
			TransactionStateChangedEventHandler transactionStateChangedEventHandler = c;
			TransactionStateChangedEventHandler transactionStateChangedEventHandler2;
			do
			{
				transactionStateChangedEventHandler2 = transactionStateChangedEventHandler;
				TransactionStateChangedEventHandler value2 = (TransactionStateChangedEventHandler)Delegate.Combine(transactionStateChangedEventHandler2, value);
				transactionStateChangedEventHandler = Interlocked.CompareExchange(ref c, value2, transactionStateChangedEventHandler2);
			}
			while ((object)transactionStateChangedEventHandler != transactionStateChangedEventHandler2);
		}
		remove
		{
			TransactionStateChangedEventHandler transactionStateChangedEventHandler = c;
			TransactionStateChangedEventHandler transactionStateChangedEventHandler2;
			do
			{
				transactionStateChangedEventHandler2 = transactionStateChangedEventHandler;
				TransactionStateChangedEventHandler value2 = (TransactionStateChangedEventHandler)Delegate.Remove(transactionStateChangedEventHandler2, value);
				transactionStateChangedEventHandler = Interlocked.CompareExchange(ref c, value2, transactionStateChangedEventHandler2);
			}
			while ((object)transactionStateChangedEventHandler != transactionStateChangedEventHandler2);
		}
	}

	protected void CheckDisposed()
	{
		if (isDisposed)
		{
			throw new InvalidOperationException(AlreadyDisposedErrorMessage);
		}
	}

	protected void OnStateChanging(TransactionAction action, DbConnection connection)
	{
		if (b != null)
		{
			TransactionStateChangingEventArgs e = new TransactionStateChangingEventArgs(action);
			b(this, e);
		}
	}

	protected void OnStateChanged(TransactionAction action, DbConnection connection)
	{
		if (c != null)
		{
			TransactionStateChangedEventArgs e = new TransactionStateChangedEventArgs(action);
			c(this, e);
		}
	}
}
