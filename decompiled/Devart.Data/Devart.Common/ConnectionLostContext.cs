namespace Devart.Common;

public enum ConnectionLostContext
{
	None,
	HasPrepared,
	InTransaction,
	InFetch
}
