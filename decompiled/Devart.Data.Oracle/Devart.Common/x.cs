using System;

namespace Devart.Common;

internal class x : ILocalFailoverManager
{
	private DbConnectionBase m_a;

	private int m_b;

	private int c;

	private RetryMode d = RetryMode.Reexecute;

	private bool e;

	internal x(DbConnectionBase A_0)
	{
		this.m_a = A_0;
	}

	public ILocalFailoverManager a()
	{
		return a(A_0: false);
	}

	ILocalFailoverManager ILocalFailoverManager.StartUse()
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a();
	}

	public ILocalFailoverManager a(bool A_0)
	{
		this.m_b++;
		if (this.m_b <= 1)
		{
			e = A_0;
		}
		return this;
	}

	ILocalFailoverManager ILocalFailoverManager.StartUse(bool A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0);
	}

	public RetryMode a(object A_0, ConnectionLostCause A_1, RetryMode A_2, Exception A_3)
	{
		if (d == RetryMode.Reexecute && A_2 == RetryMode.Raise)
		{
			d = RetryMode.Raise;
		}
		if (this.m_b <= 1)
		{
			return this.m_a.a(A_0, A_1, A_2, A_3, ref c, e);
		}
		return RetryMode.Raise;
	}

	RetryMode ILocalFailoverManager.DoLocalFailoverEvent(object A_0, ConnectionLostCause A_1, RetryMode A_2, Exception A_3)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0, A_1, A_2, A_3);
	}

	public void b()
	{
		this.m_b--;
		if (this.m_b == 0)
		{
			d = RetryMode.Reexecute;
			c = 0;
			e = false;
		}
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b();
	}
}
