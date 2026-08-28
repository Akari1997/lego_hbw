using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Devart.DbMonitor;

internal class f
{
	private string m_a;

	private bool m_b;

	private bool m_c;

	private bool m_d;

	private f m_e;

	private List<f> m_f;

	public f()
	{
		this.m_f = new List<f>();
	}

	public f h()
	{
		f f2 = a();
		while (f2 != null && f2.c())
		{
			f2 = f2.a();
		}
		return f2;
	}

	public f f()
	{
		f f2 = a();
		while (f2 != null && f2.i())
		{
			f2 = f2.a();
		}
		return f2;
	}

	[SpecialName]
	public string d()
	{
		return this.m_a;
	}

	[SpecialName]
	public void a(string A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public bool c()
	{
		return this.m_b;
	}

	[SpecialName]
	public void a(bool A_0)
	{
		this.m_b = A_0;
	}

	[SpecialName]
	public bool g()
	{
		return this.m_c;
	}

	[SpecialName]
	public void c(bool A_0)
	{
		this.m_c = A_0;
	}

	[SpecialName]
	public bool b()
	{
		return this.m_d;
	}

	[SpecialName]
	public void b(bool A_0)
	{
		this.m_d = A_0;
	}

	[SpecialName]
	internal f a()
	{
		return this.m_e;
	}

	[SpecialName]
	internal void a(f A_0)
	{
		if (this.m_e != A_0)
		{
			if (this.m_e == null)
			{
				throw new ArgumentNullException("Parent");
			}
			this.m_e = A_0;
			this.m_e.e().Add(this);
		}
	}

	[SpecialName]
	internal List<f> e()
	{
		return this.m_f;
	}

	[SpecialName]
	internal void a(List<f> A_0)
	{
		this.m_f = A_0;
	}

	[SpecialName]
	public bool i()
	{
		return c() | g();
	}
}
