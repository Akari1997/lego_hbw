using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class am
{
	private string m_a;

	private string m_b;

	private bool m_c;

	private int m_d;

	private int m_e;

	public am(string A_0, string A_1, int A_2, int A_3, bool A_4)
	{
		this.m_a = A_0;
		this.m_b = A_1;
		this.m_c = A_4;
		this.m_d = A_2;
		if (A_3 == 0)
		{
			this.m_e = A_0.Length + 1;
		}
		else
		{
			this.m_e = A_3;
		}
	}

	public am(am A_0)
	{
		this.m_a = A_0.m_a;
		this.m_b = A_0.m_b;
		this.m_d = A_0.m_d;
		this.m_e = A_0.m_e;
	}

	[SpecialName]
	public string f()
	{
		return this.m_a;
	}

	[SpecialName]
	public string d()
	{
		return this.m_b;
	}

	[SpecialName]
	public void a(string A_0)
	{
		this.m_b = A_0;
	}

	[SpecialName]
	public bool a()
	{
		return this.m_c;
	}

	[SpecialName]
	public void a(bool A_0)
	{
		this.m_c = A_0;
	}

	[SpecialName]
	public int b()
	{
		return this.m_d;
	}

	[SpecialName]
	public void b(int A_0)
	{
		this.m_d = A_0;
	}

	[SpecialName]
	public int e()
	{
		return this.m_e;
	}

	[SpecialName]
	public int c()
	{
		return this.m_d - this.m_e - 1;
	}

	[SpecialName]
	public void a(int A_0)
	{
		this.m_d = A_0 + this.m_e + 1;
	}
}
