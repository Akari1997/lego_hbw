using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class l : f
{
	private v m_a;

	private OracleType m_b;

	private HandleRef m_c;

	private r m_d;

	private aa m_e;

	private bool m_f;

	public l(byte[] A_0, OracleType A_1, v A_2)
		: this(A_1, A_2, A_2: true)
	{
		this.m_c = a(A_0);
	}

	public l(IntPtr A_0, OracleType A_1, v A_2, bool A_3)
		: this(A_1, A_2, A_3)
	{
		this.m_c = new HandleRef(this, A_0);
	}

	private l(OracleType A_0, v A_1, bool A_2)
	{
		Utils.CheckArgumentNull(A_1, "session");
		Utils.CheckArgumentNull(A_0, "refType");
		this.m_a = A_1;
		this.m_e = A_1.h();
		this.m_b = A_0;
		this.m_f = A_2;
	}

	protected virtual void f()
	{
		try
		{
			b();
		}
		catch
		{
		}
		finally
		{
			base.Finalize();
		}
	}

	public void b()
	{
		GC.SuppressFinalize(this);
		g();
		if (this.m_f && this.m_c.Handle != IntPtr.Zero)
		{
			this.m_e.c(this.m_e.j().OCIObjectFree(this.m_a.h().h(), this.m_a.h().k(), this.m_c, 1));
		}
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b();
	}

	public byte[] h()
	{
		if (a())
		{
			return null;
		}
		byte[] array = new byte[100];
		int hex_length = array.Length;
		this.m_e.c(this.m_e.j().OCIRefToHex(this.m_a.h().h(), this.m_a.h().k(), this.m_c, array, ref hex_length));
		string text = Encoding.Default.GetString(array, 0, hex_length);
		byte[] array2 = new byte[text.Length / 2];
		for (int num = 0; num < array2.Length; num++)
		{
			string text2 = text.Substring(num * 2, 2);
			array2[num] = byte.Parse(text2, NumberStyles.AllowHexSpecifier);
		}
		return array2;
	}

	byte[] f.a()
	{
		//ILSpy generated this explicit interface implementation from .override directive in h
		return this.h();
	}

	public object a(OracleConnection A_0)
	{
		e();
		return this.m_d;
	}

	public void e()
	{
		if (this.m_d == null)
		{
			if (a())
			{
				throw new InvalidOperationException();
			}
			this.m_e.c(this.m_e.j().OCIObjectPin(this.m_a.h().h(), this.m_a.h().k(), this.m_c, IntPtr.Zero, 3, 10, 1, out var obj));
			this.m_d = a(new HandleRef(this, obj));
		}
	}

	public void g()
	{
		if (this.m_d != null)
		{
			this.m_e.c(this.m_e.j().OCIObjectUnpin(this.m_e.h(), this.m_e.k(), this.m_d.r()));
			this.m_d = null;
		}
	}

	private r a(HandleRef A_0)
	{
		this.m_e.c(this.m_e.j().OCIObjectGetInd(this.m_a.h().h(), this.m_a.h().k(), A_0, out var null_struct));
		return this.m_b.h switch
		{
			OracleDbType.Array => new m(A_0, null_struct, this.m_b, this.m_a, A_4: false, A_5: false), 
			OracleDbType.Object => new bh(A_0, null_struct, this.m_b, this.m_a, A_4: false, A_5: false), 
			OracleDbType.Table => new ar(A_0, null_struct, this.m_b, this.m_a, A_4: false, A_5: false), 
			_ => throw new InvalidOperationException(), 
		};
	}

	private bool a()
	{
		return this.m_e.j().OCIRefIsNull(this.m_e.h(), this.m_c);
	}

	private HandleRef a(byte[] A_0)
	{
		IntPtr Ref = IntPtr.Zero;
		if (A_0 == null)
		{
			am am2 = (am)this.m_a.b(this.m_b);
			this.m_e.c(this.m_e.j().OCIObjectNew(this.m_e.h(), this.m_e.k(), this.m_a.r(), 110, am2.e, IntPtr.Zero, 10, 1, out Ref));
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int num = 0; num < A_0.Length; num++)
			{
				stringBuilder.AppendFormat("{0:X2}", A_0[num]);
			}
			string text = stringBuilder.ToString();
			Encoding encoding = this.m_e.o();
			byte[] bytes = encoding.GetBytes(text);
			this.m_e.c(this.m_e.j().OCIRefFromHex(this.m_e.h(), this.m_e.k(), this.m_a.r(), bytes, (uint)bytes.Length, ref Ref));
		}
		return new HandleRef(this, Ref);
	}

	[SpecialName]
	public HandleRef c()
	{
		return this.m_c;
	}

	[SpecialName]
	public g d()
	{
		return this.m_a;
	}

	g f.b()
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		return this.d();
	}
}
