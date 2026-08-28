using System;

namespace Devart.Data.Oracle;

internal class ag : at
{
	private new readonly int m_a;

	public ag(aa A_0, int A_1)
		: base(A_0)
	{
		this.m_a = A_1;
	}

	public override IntPtr a()
	{
		aa aa2 = b();
		aa2.c(aa2.j().OCIDescriptorAlloc(aa2.h(), out var descpp, this.m_a, 0u, 0u));
		return descpp;
	}

	public override void a(IntPtr A_0)
	{
		aa aa2 = b();
		aa2.c(aa2.j().OCIDescriptorFree(A_0, this.m_a));
	}
}
