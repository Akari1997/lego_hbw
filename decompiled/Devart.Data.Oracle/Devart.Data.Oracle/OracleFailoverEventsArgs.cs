using System;

namespace Devart.Data.Oracle;

public sealed class OracleFailoverEventsArgs : EventArgs
{
	private OracleFailoverState a;

	private OracleFailoverType b;

	private bool c;

	public OracleFailoverState State => a;

	public OracleFailoverType Type => b;

	public bool Retry
	{
		get
		{
			return c;
		}
		set
		{
			c = value;
		}
	}

	internal OracleFailoverEventsArgs(OracleFailoverState A_0, OracleFailoverType A_1)
	{
		a = A_0;
		b = A_1;
	}
}
