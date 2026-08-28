namespace Devart.Common;

public sealed class TransactionStateChangingEventArgs : TransactionStateChangeEventArgs
{
	public TransactionStateChangingEventArgs(TransactionAction action)
		: base(action)
	{
	}
}
