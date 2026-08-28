using System;

namespace Devart.Data.Oracle;

internal class c : at
{
	public c(aa A_0)
		: base(A_0)
	{
	}

	public override IntPtr a()
	{
		aa aa2 = b();
		aa2.c(aa2.j().OCIHandleAlloc(aa2.h(), out var hndlpp, 4u, 0u, 0u));
		return hndlpp;
	}

	public override void a(IntPtr A_0)
	{
		aa aa2 = b();
		aa2.c(aa2.j().OCIHandleFree(A_0, 4));
	}
}
