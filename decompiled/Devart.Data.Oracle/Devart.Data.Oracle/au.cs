using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class au : IDisposable
{
	private aa m_a;

	private HandleRef m_b;

	private string c;

	public au(aa A_0, string A_1, string A_2, string A_3, int A_4, int A_5, int A_6, int A_7, bool A_8)
	{
		this.m_a = A_0;
		this.m_a.c(this.m_a.j().OCIHandleAlloc(this.m_a.h(), out var hndlpp, 27u, 0u, 0u));
		this.m_b = new HandleRef(this, hndlpp);
		Encoding encoding = A_0.o();
		byte[] maxBytes = Utils.GetMaxBytes(encoding, A_1, out var byteCount);
		byte[] maxBytes2 = Utils.GetMaxBytes(encoding, A_2, out var byteCount2);
		byte[] maxBytes3 = Utils.GetMaxBytes(encoding, A_3, out var byteCount3);
		this.m_a.c(this.m_a.j().OCISessionPoolCreate(this.m_a.h(), this.m_a.k(), this.m_b, out var poolName, out var poolNameLen, maxBytes, (uint)byteCount, (uint)A_4, (uint)A_5, (uint)A_6, maxBytes2, (uint)byteCount2, maxBytes3, (uint)byteCount3, 0u));
		if (A_7 > 0)
		{
			this.m_a.c(this.m_a.j().OCIAttrSet(this.m_b, 27u, ref A_7, 0, 308u, this.m_a.k()));
		}
		int attributep = ((!A_8) ? 1 : 0);
		this.m_a.c(this.m_a.j().OCIAttrSet(this.m_b, 27u, ref attributep, 0, 309u, this.m_a.k()));
		if (A_0.a())
		{
			c = Marshal.PtrToStringUni(poolName, poolNameLen / 2);
		}
		else
		{
			c = Marshal.PtrToStringAnsi(poolName, poolNameLen);
		}
	}

	public void a()
	{
		this.m_a.c(this.m_a.j().OCISessionPoolDestroy(this.m_b, this.m_a.k(), 0u));
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a();
	}

	[SpecialName]
	public string b()
	{
		return c;
	}
}
