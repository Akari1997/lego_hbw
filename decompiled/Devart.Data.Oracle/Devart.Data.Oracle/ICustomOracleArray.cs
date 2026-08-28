namespace Devart.Data.Oracle;

public interface ICustomOracleArray
{
	void FromOracleArray(NativeOracleArray oraArray);

	NativeOracleArray ToOracleArray(OracleConnection con);
}
