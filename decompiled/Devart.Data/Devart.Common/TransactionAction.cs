namespace Devart.Common;

public enum TransactionAction
{
	BeginTransaction = 1,
	Commit,
	Rollback,
	Savepoint,
	ReleaseSavepoint,
	RollbackToSavepoint
}
