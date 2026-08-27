using System;

namespace Devart.Common;

public abstract class TransactionStateChangeEventArgs : EventArgs
{
	private TransactionAction a;

	public TransactionAction Action => a;

	protected TransactionStateChangeEventArgs(TransactionAction action)
	{
		a = action;
	}
}
