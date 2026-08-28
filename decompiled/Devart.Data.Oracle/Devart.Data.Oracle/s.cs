using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Devart.Data.Oracle;

internal abstract class s : IDisposable
{
	private readonly g m_a;

	private readonly aq m_b;

	private bool m_c;

	private string m_d;

	protected h[] e;

	protected byte[] f;

	protected Hashtable g;

	protected bool h;

	protected bool i;

	protected string j;

	protected bool k;

	protected int l;

	protected s(g A_0)
	{
		this.m_a = A_0;
		this.m_b = A_0.h();
		this.i = false;
		this.j = null;
	}

	public void w()
	{
		this.m_c = true;
	}

	public void y()
	{
		if (this.m_c)
		{
			this.m_c = false;
		}
		else
		{
			l();
		}
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in y
		this.y();
	}

	public abstract void l();

	public abstract void e();

	public virtual void a(string A_0)
	{
		this.m_d = A_0;
	}

	public abstract void b(h[] A_0, byte[] A_1, Hashtable A_2);

	public abstract void a(h[] A_0, byte[] A_1, Hashtable A_2);

	public abstract h[] m();

	public abstract bool a(int A_0, az A_1);

	public abstract bool b(int A_0);

	public abstract void a(OracleException A_0);

	[SpecialName]
	public g aa()
	{
		return this.m_a;
	}

	[SpecialName]
	public aq v()
	{
		return this.m_b;
	}

	[SpecialName]
	public string u()
	{
		return this.m_d;
	}

	[SpecialName]
	public abstract bool a();

	[SpecialName]
	public abstract int i();

	[SpecialName]
	public abstract int h();

	[SpecialName]
	public abstract void a(int A_0);

	[SpecialName]
	public abstract af g();

	[SpecialName]
	public abstract a9 f();

	[SpecialName]
	public abstract x k();

	[SpecialName]
	public abstract int j();

	[SpecialName]
	public abstract string b();

	[SpecialName]
	public virtual long[] d()
	{
		return null;
	}

	[SpecialName]
	internal bool ab()
	{
		return this.i;
	}

	[SpecialName]
	internal string t()
	{
		return this.j;
	}

	[SpecialName]
	public int ac()
	{
		return this.l;
	}

	[SpecialName]
	public void e(int A_0)
	{
		this.l = A_0;
	}

	[SpecialName]
	public h[] x()
	{
		return this.e;
	}

	[SpecialName]
	public byte[] ae()
	{
		return this.f;
	}

	[SpecialName]
	public Hashtable s()
	{
		return this.g;
	}

	[SpecialName]
	public bool ad()
	{
		return this.k;
	}

	[SpecialName]
	public bool z()
	{
		return this.h;
	}

	[SpecialName]
	public void a(bool A_0)
	{
		this.h = A_0;
	}

	public virtual s[] c()
	{
		return null;
	}
}
