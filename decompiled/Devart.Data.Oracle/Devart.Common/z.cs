using System;
using System.Runtime.InteropServices;

namespace Devart.Common;

internal sealed class z : IDisposable
{
	internal struct a
	{
		public Delegate a;
	}

	private GCHandle[] m_a;

	private int m_b;

	public z()
	{
		this.m_a = new GCHandle[4];
	}

	protected void b()
	{
		try
		{
			c();
		}
		finally
		{
			base.Finalize();
		}
	}

	public void c()
	{
		for (int num = 0; num < this.m_b; num++)
		{
			this.m_a[num].Free();
		}
		this.m_a = new GCHandle[4];
		this.m_b = 0;
		GC.SuppressFinalize(this);
	}

	public IntPtr a(Delegate A_0)
	{
		if ((object)A_0 == null)
		{
			return IntPtr.Zero;
		}
		IntPtr intPtr = Marshal.AllocHGlobal(20);
		try
		{
			a a2 = default(a);
			a2.a = A_0;
			a((object)a2.a);
			Marshal.StructureToPtr((object)a2, intPtr, false);
			return Marshal.ReadIntPtr(intPtr);
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
		}
	}

	public void a(GCHandle A_0)
	{
		if (this.m_a.Length == this.m_b)
		{
			GCHandle[] array = new GCHandle[this.m_a.Length * 2];
			this.m_a.CopyTo(array, 0);
			this.m_a = array;
		}
		this.m_a[this.m_b] = A_0;
		this.m_b++;
	}

	public IntPtr a(object A_0)
	{
		GCHandle gCHandle = GCHandle.Alloc(A_0, GCHandleType.Normal);
		a(gCHandle);
		return Marshal.ReadIntPtr((IntPtr)gCHandle);
	}

	public IntPtr b(object A_0)
	{
		GCHandle a_ = GCHandle.Alloc(A_0, GCHandleType.Pinned);
		a(a_);
		return a_.AddrOfPinnedObject();
	}

	internal static void a(byte[] A_0, int A_1, IntPtr A_2)
	{
		if (IntPtr.Size == 4)
		{
			a(A_0, A_1, A_2.ToInt32());
		}
		else
		{
			a(A_0, A_1, (long)A_2);
		}
	}

	internal static IntPtr e(byte[] A_0, int A_1)
	{
		if (IntPtr.Size == 4)
		{
			return (IntPtr)c(A_0, A_1);
		}
		return (IntPtr)b(A_0, A_1);
	}

	internal static void d(byte[] A_0, int A_1)
	{
		A_0[A_1++] = 0;
		A_0[A_1++] = 0;
		A_0[A_1++] = 0;
		A_0[A_1++] = 0;
		if (IntPtr.Size != 4)
		{
			A_0[A_1++] = 0;
			A_0[A_1++] = 0;
			A_0[A_1++] = 0;
			A_0[A_1] = 0;
		}
	}

	internal static void a(byte[] A_0, int A_1, int A_2)
	{
		A_0[A_1++] = (byte)A_2;
		A_0[A_1++] = (byte)(A_2 >> 8);
		A_0[A_1++] = (byte)(A_2 >> 16);
		A_0[A_1] = (byte)(A_2 >> 24);
	}

	internal static void a(byte[] A_0, int A_1, long A_2)
	{
		A_0[A_1] = (byte)A_2;
		A_0[A_1 + 1] = (byte)(A_2 >> 8);
		A_0[A_1 + 2] = (byte)(A_2 >> 16);
		A_0[A_1 + 3] = (byte)(A_2 >> 24);
		A_0[A_1 + 4] = (byte)(A_2 >> 32);
		A_0[A_1 + 5] = (byte)(A_2 >> 40);
		A_0[A_1 + 6] = (byte)(A_2 >> 48);
		A_0[A_1 + 7] = (byte)(A_2 >> 56);
	}

	internal static int c(byte[] A_0, int A_1)
	{
		return A_0[A_1++] | (A_0[A_1++] << 8) | (A_0[A_1++] << 16) | (A_0[A_1] << 24);
	}

	internal static long b(byte[] A_0, int A_1)
	{
		uint num = (uint)(A_0[A_1] | (A_0[A_1 + 1] << 8) | (A_0[A_1 + 2] << 16) | (A_0[A_1 + 3] << 24));
		uint num2 = (uint)(A_0[A_1 + 4] | (A_0[A_1 + 5] << 8) | (A_0[A_1 + 6] << 16) | (A_0[A_1 + 7] << 24));
		return (long)(((ulong)num2 << 32) | num);
	}

	internal static void a(byte[] A_0, int A_1)
	{
		A_0[A_1++] = 0;
		A_0[A_1++] = 0;
		A_0[A_1++] = 0;
		A_0[A_1] = 0;
	}

	internal static byte[] b(IntPtr A_0, int A_1)
	{
		byte[] array = new byte[A_1];
		Marshal.Copy(A_0, array, 0, A_1);
		return array;
	}

	internal static IntPtr a(IntPtr A_0, int A_1)
	{
		if (IntPtr.Size == 4)
		{
			return (IntPtr)((int)A_0 + A_1);
		}
		return (IntPtr)((long)A_0 + A_1);
	}

	public void a()
	{
		c();
		GC.SuppressFinalize(this);
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a();
	}
}
