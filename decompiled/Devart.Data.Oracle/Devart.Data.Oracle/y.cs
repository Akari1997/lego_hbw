using System.Collections;
using System.Runtime.CompilerServices;

namespace Devart.Data.Oracle;

internal class y : IEnumerator
{
	private ak m_a;

	private int m_b;

	private object m_c;

	internal y(ak A_0)
	{
		this.m_a = A_0;
	}

	public bool c()
	{
		if (this.m_b < this.m_a.Count)
		{
			this.m_c = this.m_a[this.m_b];
			this.m_b++;
			return true;
		}
		return false;
	}

	bool IEnumerator.MoveNext()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		return this.c();
	}

	public void a()
	{
		this.m_b = 0;
		this.m_c = null;
	}

	void IEnumerator.Reset()
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a();
	}

	[SpecialName]
	public object b()
	{
		return this.m_c;
	}

	object IEnumerator.get_Current()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b();
	}
}
