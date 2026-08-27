namespace Devart.Common;

public sealed class TransactionStateChangedEventArgs : TransactionStateChangeEventArgs
{
	public TransactionStateChangedEventArgs(TransactionAction action)
		: base(action)
	{
	}
}
