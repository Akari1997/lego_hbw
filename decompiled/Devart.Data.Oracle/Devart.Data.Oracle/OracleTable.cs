namespace Devart.Data.Oracle;

public class OracleTable : OracleArray, ICustomOracleArray
{
	private new static OracleTable a = new OracleTable(OracleType.r);

	public new static OracleTable Null => a;

	public OracleTable(OracleType oraType)
		: base(oraType)
	{
	}

	NativeOracleArray ICustomOracleArray.ToOracleArray(OracleConnection con)
	{
		NativeOracleArray nativeOracleArray = new NativeOracleTable(oraType, con);
		a(con, nativeOracleArray);
		return nativeOracleArray;
	}

	protected override OracleArray CreateInstance()
	{
		return new OracleTable(oraType);
	}
}
