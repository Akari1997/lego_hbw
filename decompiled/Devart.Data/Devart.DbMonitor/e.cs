using System;
using System.Runtime.CompilerServices;

namespace Devart.DbMonitor;

internal class e : o
{
	private new k m_a;

	private string b;

	private DateTime c;

	private int d;

	public e()
		: base(g.a)
	{
	}

	public override void a(h A_0)
	{
		base.a(A_0);
		A_0.a((int)r());
		A_0.a(t());
	}

	public override void a(j A_0)
	{
		base.a(A_0);
		a((k)A_0.b());
		f(A_0.c());
	}

	[SpecialName]
	internal k r()
	{
		return this.m_a;
	}

	[SpecialName]
	internal void a(k A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public string t()
	{
		return b;
	}

	[SpecialName]
	public void f(string A_0)
	{
		b = A_0;
	}

	[SpecialName]
	public DateTime q()
	{
		return c;
	}

	[SpecialName]
	public void a(DateTime A_0)
	{
		c = A_0;
	}

	[SpecialName]
	public int s()
	{
		return d;
	}

	[SpecialName]
	public void e(int A_0)
	{
		d = A_0;
	}
}
