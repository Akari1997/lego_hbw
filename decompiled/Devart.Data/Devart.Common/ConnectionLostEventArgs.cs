using System;

namespace Devart.Common;

public class ConnectionLostEventArgs : EventArgs
{
	private readonly object a;

	private readonly ConnectionLostCause b;

	private readonly int c;

	private RetryMode d;

	private ConnectionLostContext e;

	public object Component => a;

	public ConnectionLostCause Cause => b;

	public int AttemptNumber => c;

	public RetryMode RetryMode
	{
		get
		{
			return d;
		}
		set
		{
			d = value;
		}
	}

	public ConnectionLostContext Context
	{
		get
		{
			return e;
		}
		set
		{
			e = value;
		}
	}

	public ConnectionLostEventArgs(object component, ConnectionLostCause cause, ConnectionLostContext context, RetryMode retryMode, int attemptNumber)
	{
		a = component;
		d = retryMode;
		c = attemptNumber;
		b = cause;
		e = context;
	}
}
