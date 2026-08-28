using System;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleProviderException : DbProviderException
{
	internal OracleProviderException()
	{
	}

	internal OracleProviderException(string A_0)
		: base(A_0)
	{
	}

	internal OracleProviderException(string A_0, Exception A_1)
		: base(A_0, A_1)
	{
	}
}
