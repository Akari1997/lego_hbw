namespace Devart.Data.Oracle;

public class OracleLogicalTransactionStatus
{
	private bool a;

	private bool b;

	public bool Committed => a;

	public bool UserCallCompleted => b;

	internal OracleLogicalTransactionStatus(bool A_0, bool A_1)
	{
		a = A_0;
		b = UserCallCompleted;
	}
}
