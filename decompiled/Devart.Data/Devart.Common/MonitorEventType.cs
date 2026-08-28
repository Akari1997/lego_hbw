namespace Devart.Common;

public enum MonitorEventType
{
	Connect,
	Disconnect,
	Prepare,
	Execute,
	BeginTransaction,
	Commit,
	Rollback,
	Error,
	ActivateInPool,
	ReturnToPool,
	Custom
}
