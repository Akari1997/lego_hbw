using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Devart.DbMonitor;

internal class n : IDisposable
{
	private const string m_a = "{0} monitoring is started";

	private const string m_b = "{0} monitoring is finished";

	private const string m_c = "dbMonitor is not active";

	private m m_d;

	private string m_e;

	private string m_f;

	private int m_g;

	private int m_h;

	private int m_i;

	private int j;

	public n()
	{
		this.m_h = 5000;
		this.m_i = 1000;
	}

	public void d()
	{
		if (this.m_d != null && this.m_d.b())
		{
			f();
		}
		this.m_d = null;
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		this.d();
	}

	public void g()
	{
		if (!e())
		{
			throw new InvalidOperationException("dbMonitor is not active");
		}
		l l2 = new l();
		l2.f(Process.GetCurrentProcess().Id);
		l2.a(k.a);
		l2.f($"{h()} monitoring is started");
		l2.a(Application.ExecutablePath);
		if (string.IsNullOrEmpty(l2.a()))
		{
			l2.a(typeof(l).Assembly.Location);
		}
		this.m_d.a(l2);
	}

	public void f()
	{
		if (this.m_d != null && this.m_d.i())
		{
			e e2 = new e();
			e2.f(Process.GetCurrentProcess().Id);
			e2.a(k.b);
			e2.f($"{h()} monitoring is finished");
			this.m_d.a(e2);
			this.m_d.c();
		}
	}

	[SpecialName]
	public bool e()
	{
		if (this.m_d == null || !this.m_d.i())
		{
			if (this.m_d == null)
			{
				this.m_d = new m();
			}
			this.m_d.b(b());
			this.m_d.d(c());
			this.m_d.c(i());
			this.m_d.b(a());
			if (this.m_d.g())
			{
				return true;
			}
			return false;
		}
		return true;
	}

	public void a(c A_0)
	{
		c(A_0);
		b(A_0);
	}

	public void c(c A_0)
	{
		if (this.m_d == null || !this.m_d.i())
		{
			g();
		}
		A_0.a(Interlocked.Increment(ref j));
		a a2 = new a();
		a2.f(Process.GetCurrentProcess().Id);
		a2.a(A_0.i());
		a2.f(A_0.g());
		a2.c(Environment.TickCount);
		a2.a(A_0.p());
		a2.b(A_0.l());
		a2.e(A_0.q());
		a2.b(A_0.n());
		a2.b(A_0.f());
		a2.d(A_0.k());
		a2.a(A_0.d());
		a2.a(A_0.a());
		a2.c(A_0.j());
		a2.d(A_0.e());
		a2.a(A_0.c());
		a2.a(A_0.m());
		this.m_d.a(a2);
	}

	public void b(c A_0)
	{
		if (this.m_d == null || !this.m_d.i())
		{
			g();
		}
		i i2 = new i();
		i2.f(Process.GetCurrentProcess().Id);
		i2.a(A_0.i());
		i2.f(A_0.g());
		i2.b(Environment.TickCount);
		if (A_0.p() == 0)
		{
			i2.a(j);
		}
		else
		{
			i2.a(A_0.p());
		}
		i2.c(Convert.ToInt32(A_0.o()));
		i2.a(A_0.b());
		i2.a(A_0.c());
		i2.d(A_0.h());
		this.m_d.a(i2);
	}

	[SpecialName]
	public string h()
	{
		return this.m_e;
	}

	[SpecialName]
	public void b(string A_0)
	{
		this.m_e = A_0;
	}

	[SpecialName]
	public string b()
	{
		return this.m_f;
	}

	[SpecialName]
	public void a(string A_0)
	{
		this.m_f = A_0;
	}

	[SpecialName]
	public int c()
	{
		return this.m_g;
	}

	[SpecialName]
	public void c(int A_0)
	{
		this.m_g = A_0;
	}

	[SpecialName]
	public int i()
	{
		return this.m_h;
	}

	[SpecialName]
	public void b(int A_0)
	{
		this.m_h = A_0;
	}

	[SpecialName]
	public int a()
	{
		return this.m_i;
	}

	[SpecialName]
	public void a(int A_0)
	{
		this.m_i = A_0;
	}
}
