using System.Runtime.CompilerServices;

namespace Devart.DbMonitor;

internal struct b(string A_0, string A_1, string A_2, string A_3)
{
	private string m_a = A_0;

	private string m_b = A_1;

	private string m_c = A_2;

	private string m_d = A_3;

	[SpecialName]
	public string d()
	{
		return this.m_a;
	}

	[SpecialName]
	public void c(string A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public string b()
	{
		return this.m_b;
	}

	[SpecialName]
	public void b(string A_0)
	{
		this.m_b = A_0;
	}

	[SpecialName]
	public string a()
	{
		return this.m_c;
	}

	[SpecialName]
	public void a(string A_0)
	{
		this.m_c = A_0;
	}

	[SpecialName]
	public string c()
	{
		return this.m_d;
	}

	[SpecialName]
	public void d(string A_0)
	{
		this.m_d = A_0;
	}
}
