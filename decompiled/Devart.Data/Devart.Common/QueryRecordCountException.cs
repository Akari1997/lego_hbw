using System;

namespace Devart.Common;

public class QueryRecordCountException : Exception
{
	internal QueryRecordCountException()
	{
	}

	internal QueryRecordCountException(string A_0)
		: base(A_0)
	{
	}

	internal QueryRecordCountException(string A_0, Exception A_1)
		: base(A_0, A_1)
	{
	}
}
