using System;

namespace Devart.Data.Oracle;

internal class n : at
{
	public n(aa A_0)
		: base(A_0)
	{
	}

	public override IntPtr a()
	{
		aa aa2 = b();
		aa2.c(aa2.j().OCIDescriptorAlloc(aa2.h(), out var descpp, 50, 0u, 0u));
		aa2.c(aa2.j().OCIAttrSet(descpp, 50u, IntPtr.Zero, 0, 45u, aa2.k()));
		return descpp;
	}

	public override void a(IntPtr A_0)
	{
		aa aa2 = b();
		aa2.c(aa2.j().OCIDescriptorFree(A_0, 50));
	}
}
