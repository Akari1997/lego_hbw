using System;

namespace Devart.Data.Oracle;

public class UnsupportedTypeException : OracleProviderException
{
	internal UnsupportedTypeException()
	{
	}

	internal UnsupportedTypeException(string A_0)
		: base(A_0)
	{
	}

	internal UnsupportedTypeException(string A_0, Exception A_1)
		: base(A_0, A_1)
	{
	}
}
