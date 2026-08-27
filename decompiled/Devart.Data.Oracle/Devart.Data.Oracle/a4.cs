using System;
using System.Runtime.InteropServices;

namespace Devart.Data.Oracle;

internal class a4 : at
{
	private new am m_a;

	private new v b;

	public a4(aa A_0, am A_1)
		: base(A_0)
	{
		this.m_a = A_1;
		b = A_1.d;
	}

	public override IntPtr a()
	{
		aa aa2 = b();
		IntPtr instance = IntPtr.Zero;
		if (this.m_a.h != 58 || this.m_a.b() != "SYS.ANYDATA")
		{
			aa2.c(aa2.j().OCIObjectNew(aa2.h(), aa2.k(), b.r(), this.m_a.h, this.m_a.e, IntPtr.Zero, 10, 1, out instance));
		}
		return instance;
	}

	public override void a(IntPtr A_0)
	{
		aa aa2 = b();
		HandleRef instance = new HandleRef(this, A_0);
		aa2.c(aa2.j().OCIObjectFree(aa2.h(), aa2.k(), instance, 1));
	}
}
