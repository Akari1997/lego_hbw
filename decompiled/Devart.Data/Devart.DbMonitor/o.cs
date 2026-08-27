using System.Runtime.CompilerServices;

namespace Devart.DbMonitor;

internal abstract class o
{
	private g m_a;

	private int b;

	public o(g A_0)
	{
		this.m_a = A_0;
	}

	public virtual void a(h A_0)
	{
		A_0.a(b);
	}

	public virtual void a(j A_0)
	{
		b = A_0.b();
	}

	[SpecialName]
	internal g v()
	{
		return this.m_a;
	}

	[SpecialName]
	internal void a(g A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public int u()
	{
		return b;
	}

	[SpecialName]
	public void f(int A_0)
	{
		b = A_0;
	}
}
