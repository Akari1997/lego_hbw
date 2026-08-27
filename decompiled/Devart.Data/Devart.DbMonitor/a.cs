using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Devart.DbMonitor;

internal class a : e
{
	private new int m_a;

	private int m_b;

	private int m_c;

	private string m_d;

	private new d m_e;

	private new string m_f;

	private int m_g;

	private string m_h;

	private d m_i;

	private string m_j;

	private string m_k;

	private List<b> m_l;

	private List<string> m_m;

	private p m_n;

	private i m_o;

	private f m_p;

	public a()
	{
		a(Devart.DbMonitor.g.c);
		this.m_l = new List<b>();
		this.m_m = new List<string>();
	}

	public override void a(h A_0)
	{
		base.a(A_0);
		A_0.a(o());
		A_0.a(g());
		A_0.a(k());
		A_0.a(p());
		A_0.a((int)n());
		A_0.a(f());
		A_0.a(j());
		A_0.a(d());
		A_0.a((int)a());
		A_0.a(i());
		A_0.a(e());
		A_0.a(b().Count);
		foreach (b item in b())
		{
			A_0.a(item.d());
			A_0.a(item.b());
			A_0.a(item.a());
			A_0.a(item.c());
		}
		A_0.a(l().Count);
		foreach (string item2 in l())
		{
			A_0.a(item2);
		}
	}

	public override void a(j A_0)
	{
		base.a(A_0);
		a(A_0.b());
		c(A_0.b());
		b(A_0.b());
		e(A_0.c());
		b((d)A_0.b());
		b(A_0.c());
		d(A_0.b());
		a(A_0.c());
		a((d)A_0.b());
		c(A_0.c());
		d(A_0.c());
		int num = A_0.b();
		for (int num2 = 0; num2 < num; num2++)
		{
			b item = default(b);
			item.c(A_0.c());
			item.b(A_0.c());
			item.a(A_0.c());
			item.d(A_0.c());
			b().Add(item);
		}
		num = A_0.b();
		for (int num3 = 0; num3 < num; num3++)
		{
			l().Add(A_0.c());
		}
	}

	[SpecialName]
	public int o()
	{
		return this.m_a;
	}

	[SpecialName]
	public void a(int A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public int g()
	{
		return this.m_b;
	}

	[SpecialName]
	public void c(int A_0)
	{
		this.m_b = A_0;
	}

	[SpecialName]
	public int k()
	{
		return this.m_c;
	}

	[SpecialName]
	public void b(int A_0)
	{
		this.m_c = A_0;
	}

	[SpecialName]
	public string p()
	{
		return this.m_d;
	}

	[SpecialName]
	public void e(string A_0)
	{
		this.m_d = A_0;
	}

	[SpecialName]
	internal d n()
	{
		return this.m_e;
	}

	[SpecialName]
	internal void b(d A_0)
	{
		this.m_e = A_0;
	}

	[SpecialName]
	public string f()
	{
		return this.m_f;
	}

	[SpecialName]
	public void b(string A_0)
	{
		this.m_f = A_0;
	}

	[SpecialName]
	public int j()
	{
		return this.m_g;
	}

	[SpecialName]
	public void d(int A_0)
	{
		this.m_g = A_0;
	}

	[SpecialName]
	public string d()
	{
		return this.m_h;
	}

	[SpecialName]
	public void a(string A_0)
	{
		this.m_h = A_0;
	}

	[SpecialName]
	internal d a()
	{
		return this.m_i;
	}

	[SpecialName]
	internal void a(d A_0)
	{
		this.m_i = A_0;
	}

	[SpecialName]
	public string i()
	{
		return this.m_j;
	}

	[SpecialName]
	public void c(string A_0)
	{
		this.m_j = A_0;
	}

	[SpecialName]
	public string e()
	{
		return this.m_k;
	}

	[SpecialName]
	public void d(string A_0)
	{
		this.m_k = A_0;
	}

	[SpecialName]
	internal List<b> b()
	{
		return this.m_l;
	}

	[SpecialName]
	internal void a(List<b> A_0)
	{
		this.m_l = A_0;
	}

	[SpecialName]
	public List<string> l()
	{
		return this.m_m;
	}

	[SpecialName]
	public void a(List<string> A_0)
	{
		this.m_m = A_0;
	}

	[SpecialName]
	internal p c()
	{
		return this.m_n;
	}

	[SpecialName]
	internal void a(p A_0)
	{
		this.m_n = A_0;
	}

	[SpecialName]
	internal i m()
	{
		return this.m_o;
	}

	[SpecialName]
	internal void a(i A_0)
	{
		this.m_o = A_0;
	}

	[SpecialName]
	internal f h()
	{
		return this.m_p;
	}

	[SpecialName]
	internal void a(f A_0)
	{
		this.m_p = A_0;
	}
}
