using System;
using System.Runtime.InteropServices;

namespace Devart.Data.Oracle;

internal class a7 : at
{
	private new am m_a;

	private new v b;

	public a7(aa A_0, am A_1)
		: base(A_0)
	{
		this.m_a = A_1;
		b = A_1.d;
	}

	public override IntPtr a()
	{
		aa aa2 = b();
		aa2.c(aa2.j().OCIObjectNew(aa2.h(), aa2.k(), b.r(), 110, this.m_a.e, IntPtr.Zero, 10, 1, out var instance));
		return instance;
	}

	public override void a(IntPtr A_0)
	{
		aa aa2 = b();
		HandleRef instance = new HandleRef(this, A_0);
		aa2.c(aa2.j().OCIObjectFree(aa2.h(), aa2.k(), instance, 1));
	}
}
