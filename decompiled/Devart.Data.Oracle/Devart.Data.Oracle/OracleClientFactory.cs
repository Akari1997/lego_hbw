using System;

namespace Devart.Data.Oracle;

[Obsolete("This class is designed for compatibility with OracleClient only.")]
public class OracleClientFactory : OracleProviderFactory
{
	public new static OracleProviderFactory Instance = new OracleClientFactory();
}
