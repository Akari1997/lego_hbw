namespace Devart.Data.Oracle;

public class NativeOracleTable : NativeOracleArray
{
	public NativeOracleTable(string typeName, OracleConnection connection)
		: base(typeName, connection)
	{
	}

	public NativeOracleTable(OracleType objectType, OracleConnection connection)
		: base(objectType, connection)
	{
	}

	internal NativeOracleTable(ak A_0, OracleConnection A_1, bool A_2)
		: base(A_0, A_1, A_2)
	{
	}
}
