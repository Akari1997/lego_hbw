using System.Runtime.CompilerServices;

namespace Devart.DbMonitor;

internal class l : e
{
	private new string m_a;

	public l()
	{
		a(g.b);
	}

	public override void a(h A_0)
	{
		base.a(A_0);
		A_0.a(this.m_a);
	}

	public override void a(j A_0)
	{
		base.a(A_0);
		this.m_a = A_0.c();
	}

	[SpecialName]
	public string a()
	{
		return this.m_a;
	}

	[SpecialName]
	public void a(string A_0)
	{
		this.m_a = A_0;
	}
}
