using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Devart.DbMonitor;

internal class i : e
{
	private new int m_a;

	private int m_b;

	private int m_c;

	private string m_d;

	private new List<b> m_e;

	private new int m_f;

	public i()
	{
		a(g.d);
		this.m_e = new List<b>();
	}

	public override void a(h A_0)
	{
		base.a(A_0);
		A_0.a(c());
		A_0.a(b());
		A_0.a(f());
		A_0.a(d());
		A_0.a(a().Count);
		foreach (b item in a())
		{
			A_0.a(item.d());
			A_0.a(item.b());
			A_0.a(item.a());
			A_0.a(item.c());
		}
		A_0.a(e());
	}

	public override void a(j A_0)
	{
		base.a(A_0);
		a(A_0.b());
		b(A_0.b());
		c(A_0.b());
		a(A_0.c());
		int num = A_0.b();
		for (int num2 = 0; num2 < num; num2++)
		{
			b item = default(b);
			item.c(A_0.c());
			item.b(A_0.c());
			item.a(A_0.c());
			item.d(A_0.c());
			a().Add(item);
		}
		d(A_0.b());
	}

	[SpecialName]
	public int c()
	{
		return this.m_a;
	}

	[SpecialName]
	public void a(int A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public int b()
	{
		return this.m_b;
	}

	[SpecialName]
	public void b(int A_0)
	{
		this.m_b = A_0;
	}

	[SpecialName]
	public int f()
	{
		return this.m_c;
	}

	[SpecialName]
	public void c(int A_0)
	{
		this.m_c = A_0;
	}

	[SpecialName]
	public string d()
	{
		return this.m_d;
	}

	[SpecialName]
	public void a(string A_0)
	{
		this.m_d = A_0;
	}

	[SpecialName]
	internal List<b> a()
	{
		return this.m_e;
	}

	[SpecialName]
	internal void a(List<b> A_0)
	{
		this.m_e = A_0;
	}

	[SpecialName]
	public int e()
	{
		return this.m_f;
	}

	[SpecialName]
	public void d(int A_0)
	{
		this.m_f = A_0;
	}
}
