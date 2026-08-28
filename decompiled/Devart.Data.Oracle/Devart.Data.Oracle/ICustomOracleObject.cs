namespace Devart.Data.Oracle;

public interface ICustomOracleObject
{
	void FromOracleObject(NativeOracleObject oraObject);

	NativeOracleObject ToOracleObject(OracleConnection con);
}
