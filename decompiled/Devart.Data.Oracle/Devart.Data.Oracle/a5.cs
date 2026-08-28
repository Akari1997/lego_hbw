using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Devart.Common;

namespace Devart.Data.Oracle;

internal sealed class a5 : s
{
	private new class a
	{
		public int a;

		public az b;

		public int c;

		public Exception d;

		public a(int A_0, az A_1)
		{
			a = A_0;
			b = A_1;
		}
	}

	private new readonly aa m_a;

	private new readonly Oci m_b;

	private new readonly HandleRef m_c;

	private new readonly HandleRef m_d;

	private new readonly HandleRef m_e;

	private new HandleRef m_f;

	internal new int g;

	private new bool m_h;

	private new byte[] m_i;

	private new int m_j;

	private new int m_k;

	private new Devart.Common.z m_l;

	private new Devart.Common.z m_m;

	private Devart.Common.z m_n;

	private bool m_o;

	private int m_p;

	private h[] m_q;

	private IntPtr[] m_r;

	private new string s;

	private new long[] t;

	private new static byte[] u = new byte[0];

	private new static byte[] v = new byte[2] { 255, 255 };

	public a5(v A_0, bool A_1)
		: base(A_0)
	{
		this.m_a = A_0.h();
		this.m_c = this.m_a.h();
		this.m_d = this.m_a.k();
		this.m_e = A_0.r();
		this.m_b = this.m_a.j();
		if (A_1 || !A_0.j())
		{
			r();
		}
	}

	public a5(v A_0, IntPtr A_1, bool A_2)
		: base(A_0)
	{
		this.m_a = A_0.h();
		this.m_c = this.m_a.h();
		this.m_d = this.m_a.k();
		this.m_e = A_0.r();
		this.m_b = this.m_a.j();
		this.g = (A_2 ? (-1) : 0);
		this.m_f = new HandleRef(this, A_1);
		this.m_h = true;
	}

	public void r()
	{
		d(this.m_b.OCIHandleAlloc(this.m_c, out var hndlpp, 4u, 0u, 0u));
		this.m_f = new HandleRef(this, hndlpp);
		this.g = -1;
	}

	public override void a(string A_0)
	{
		base.a(A_0);
		try
		{
			if (aa().j())
			{
				byte[] array = null;
				uint num = 0u;
				Encoding encoding = this.m_a.o();
				s = "Stmt" + A_0.GetHashCode();
				array = encoding.GetBytes(s);
				num = (uint)array.Length;
				byte[] bytes = encoding.GetBytes(A_0);
				d(this.m_b.OCIStmtPrepare2(this.m_e, out var stmthp, this.m_d, bytes, (uint)bytes.Length, array, num, 1u, 0u));
				this.m_f = new HandleRef(this, stmthp);
				this.g = 1;
			}
			else if (this.m_a.a())
			{
				byte[] bytes2 = Encoding.Unicode.GetBytes(A_0);
				d(this.m_b.OCIStmtPrepare(this.m_f, this.m_d, bytes2, bytes2.Length, 1u, 0u));
			}
			else
			{
				d(this.m_b.OCIStmtPrepare(this.m_f, this.m_d, A_0, OracleUtils.GetOCITextLength(Encoding.Default, A_0), 1u, 0u));
			}
		}
		catch (AccessViolationException a_)
		{
			throw new OracleException(81, Devart.Common.al.a("InternalOracleClientError"), a_);
		}
		this.m_h = true;
	}

	public override void l()
	{
		this.m_h = false;
		try
		{
			try
			{
				if (this.m_o)
				{
					n();
				}
				if (this.g == 0 || (this.g == 2 && this.m_a.d() >= 9020000))
				{
					return;
				}
				if (aa().j() && this.g == 1)
				{
					Encoding encoding = this.m_a.o();
					byte[] array = null;
					uint num = 0u;
					array = encoding.GetBytes(s);
					num = (uint)array.Length;
					uint mode = 0u;
					if (!base.h)
					{
						mode = 16u;
					}
					this.m_b.OCIStmtRelease(this.m_f, this.m_d, array, num, mode);
				}
				else
				{
					this.m_b.OCIHandleFree(this.m_f, 4);
				}
				this.g = 0;
				this.m_f = new HandleRef(this, IntPtr.Zero);
			}
			finally
			{
				if (this.m_l != null)
				{
					this.m_l.a();
					this.m_l = null;
				}
				if (this.m_m != null)
				{
					this.m_m.a();
					this.m_m = null;
				}
				if (this.m_n != null)
				{
					this.m_n.a();
					this.m_n = null;
				}
			}
		}
		catch (SEHException a_)
		{
			throw new OracleException(81, Devart.Common.al.a("InternalOracleClientError"), a_);
		}
	}

	public override void e()
	{
		if (this.m_m != null)
		{
			this.m_m.c();
		}
		else
		{
			this.m_m = new Devart.Common.z();
		}
	}

	public override void b(h[] A_0, byte[] A_1, Hashtable A_2)
	{
		base.e = A_0;
		base.f = A_1;
		if (this.m_l != null)
		{
			this.m_l.c();
		}
		else
		{
			this.m_l = new Devart.Common.z();
		}
		IntPtr a_ = this.m_l.b(A_1);
		Encoding encoding = (this.m_a.a() ? Encoding.Unicode : null);
		int attributep = this.m_a.b((v)aa());
		int num = A_0.Length;
		for (int num2 = 0; num2 < num; num2++)
		{
			h h2 = A_0[num2];
			IntPtr intPtr = Devart.Common.z.a(a_, h2.l);
			IntPtr indp = Devart.Common.z.a(a_, h2.n);
			int num3;
			int num4;
			if (h2.c == 24 || h2.c == 8)
			{
				num3 = 2;
				num4 = int.MaxValue;
				intPtr = IntPtr.Zero;
				indp = IntPtr.Zero;
			}
			else
			{
				num3 = 0;
				num4 = h2.m;
			}
			IntPtr alenp = ((h2.o <= 0) ? IntPtr.Zero : Devart.Common.z.a(a_, h2.o));
			IntPtr curelep = ((h2.ab <= 0) ? IntPtr.Zero : Devart.Common.z.a(a_, h2.ab));
			string text = h2.a;
			IntPtr bindpp;
			if (encoding != null)
			{
				byte[] bytes = encoding.GetBytes(text);
				d(this.m_b.OCIBindByName(this.m_f, out bindpp, this.m_d, bytes, bytes.Length, intPtr, num4, h2.c, indp, alenp, 0, h2.aa, curelep, num3));
			}
			else
			{
				d(this.m_b.OCIBindByName(this.m_f, out bindpp, this.m_d, text, OracleUtils.GetOCITextLength(Encoding.Default, text), intPtr, num4, h2.c, indp, alenp, 0, h2.aa, curelep, num3));
			}
			HandleRef handleRef = new HandleRef(this, bindpp);
			if (attributep > 0)
			{
				d(this.m_b.OCIAttrSet(handleRef, 5u, ref attributep, 0, 31u, this.m_d));
			}
			if (h2.h > 0)
			{
				d(this.m_b.OCIAttrSet(handleRef, 5u, ref h2.h, 0, 32u, this.m_d));
			}
			if (h2.s != null)
			{
				am am2 = (am)h2.s;
				if (h2.c != 110)
				{
					d(this.m_b.OCIBindObject(handleRef, this.m_d, am2.e.Handle, intPtr, IntPtr.Zero, Devart.Common.z.a(intPtr, IntPtr.Size), IntPtr.Zero));
				}
				else
				{
					d(this.m_b.OCIBindObject(handleRef, this.m_d, am2.e.Handle, intPtr, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
				}
			}
			if (h2.p > 0)
			{
				int indskip = ((h2.s == null) ? h2.q : h2.p);
				d(this.m_b.OCIBindArrayOfStruct(handleRef, this.m_d, h2.p, indskip, h2.r, 0));
			}
			if (num3 == 2)
			{
				if (this.m_l == null)
				{
					this.m_l = new Devart.Common.z();
				}
				Oci.g a_2 = a;
				Oci.c a_3 = a;
				IntPtr icbfp = this.m_l.a(a_2);
				IntPtr ocbfp = this.m_l.a(a_3);
				IntPtr zero = IntPtr.Zero;
				IntPtr zero2 = IntPtr.Zero;
				t t2 = new t(h2.v);
				GCHandle gCHandle = GCHandle.Alloc(t2, GCHandleType.Normal);
				zero = (IntPtr)gCHandle;
				this.m_l.a(gCHandle);
				t2.a(this.m_l);
				t2 = new t(h2.w);
				gCHandle = GCHandle.Alloc(t2, GCHandleType.Normal);
				zero2 = (IntPtr)gCHandle;
				this.m_l.a(gCHandle);
				t2.a(this.m_l);
				d(this.m_b.OCIBindDynamic(bindpp, this.m_d, zero, icbfp, zero2, ocbfp));
			}
			switch (h2.c)
			{
			case 1:
			case 5:
			case 11:
			case 96:
			case 97:
			case 104:
				if (string.Compare(aa().v(), "09") < 0)
				{
					break;
				}
				num4 *= 3;
				if (h2.c == 97)
				{
					if (num4 > 2000)
					{
						num4 = 2000;
					}
				}
				else if (f() != a9.i && f() != a9.j && num4 > 4000)
				{
					num4 = 4000;
				}
				if (num4 < 4)
				{
					num4 = 4;
				}
				d(this.m_b.OCIAttrSet(handleRef, 5u, ref num4, 0, 33u, this.m_d));
				break;
			}
		}
	}

	public override void a(h[] A_0, byte[] A_1, Hashtable A_2)
	{
		this.m_q = A_0;
		if (this.m_o)
		{
			n();
		}
		this.m_i = A_1;
		if (this.m_m == null)
		{
			this.m_m = new Devart.Common.z();
		}
		IntPtr a_ = this.m_m.b(A_1);
		this.m_r = new IntPtr[A_0.Length];
		int attributep = this.m_a.b((v)aa());
		for (int num = 0; num < A_0.Length; num++)
		{
			h h2 = A_0[num];
			short num2 = h2.c;
			IntPtr intPtr = Devart.Common.z.a(a_, A_0[num].l);
			IntPtr indp = Devart.Common.z.a(a_, A_0[num].n);
			int num3 = A_0[num].o;
			IntPtr rlenp = ((num3 == 0) ? IntPtr.Zero : Devart.Common.z.a(a_, num3));
			int num4 = 0;
			if (A_0[num].w != null)
			{
				num4 = 2;
				intPtr = IntPtr.Zero;
				if (string.Compare(aa().v(), "08.00.00") >= 0)
				{
					indp = IntPtr.Zero;
				}
			}
			switch (num2)
			{
			case 11:
			case 104:
				num2 = 5;
				break;
			case 110:
				indp = IntPtr.Zero;
				break;
			}
			IntPtr defnpp = IntPtr.Zero;
			d(this.m_b.OCIDefineByPos(this.m_f, ref defnpp, this.m_d, num + 1, intPtr, A_0[num].m, num2, indp, rlenp, IntPtr.Zero, num4));
			this.m_r[num] = defnpp;
			HandleRef handleRef = new HandleRef(this, defnpp);
			if (A_0[num].s != null)
			{
				if (A_0[num].c != 110)
				{
					d(this.m_b.OCIDefineObject(defnpp, this.m_d, ((am)A_0[num].s).e.Handle, intPtr, IntPtr.Zero, Devart.Common.z.a(intPtr, IntPtr.Size), IntPtr.Zero));
				}
				else
				{
					d(this.m_b.OCIDefineObject(defnpp, this.m_d, IntPtr.Zero, intPtr, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
				}
			}
			if (num4 == 2)
			{
				if (string.Compare(aa().v(), "08.00.00") >= 0)
				{
					Oci.f a_2 = a;
					if (this.m_m == null)
					{
						this.m_m = new Devart.Common.z();
					}
					IntPtr ocbfp = this.m_m.a(a_2);
					d(this.m_b.OCIDefineDynamic(handleRef, this.m_d, p(), ocbfp));
				}
				if (this.m_n == null)
				{
					this.m_n = new Devart.Common.z();
				}
			}
			else
			{
				d(this.m_b.OCIDefineArrayOfStruct(handleRef, this.m_d, A_0[num].p, A_0[num].q, A_0[num].r, 0));
			}
			object obj = null;
			switch (A_0[num].c)
			{
			case 1:
			case 5:
			case 11:
			case 96:
			case 97:
			case 104:
			{
				if (attributep > 0)
				{
					d(this.m_b.OCIAttrSet(handleRef, 6u, ref attributep, 0, 31u, this.m_d));
				}
				int attributep2 = A_0[num].h;
				if (attributep2 != 0)
				{
					d(this.m_b.OCIAttrSet(handleRef, 6u, ref attributep2, 0, 32u, this.m_d));
				}
				break;
			}
			case 112:
			case 113:
			case 114:
			case 115:
				obj = this.m_a.b(50);
				break;
			case 58:
			case 108:
			case 122:
			case 247:
			case 248:
				obj = this.m_a.a(A_0[num].c, (am)A_0[num].s);
				break;
			case 110:
				obj = this.m_a.a(A_0[num].c, (am)A_0[num].s);
				break;
			case 187:
				obj = this.m_a.b(68);
				break;
			case 188:
				obj = this.m_a.b(69);
				break;
			case 232:
				obj = this.m_a.b(70);
				break;
			case 189:
				obj = this.m_a.b(62);
				break;
			case 190:
				obj = this.m_a.b(63);
				break;
			case 116:
				obj = this.m_a.b(4);
				break;
			}
			if (obj != null)
			{
				this.m_o = true;
				A_0[num].v = obj;
			}
		}
	}

	private static int a(IntPtr A_0, IntPtr A_1, int A_2, int A_3, out IntPtr A_4, out int A_5, out byte A_6, out IntPtr A_7)
	{
		a6 a10 = null;
		t t2 = (t)((GCHandle)A_0).Target;
		object obj = t2.a();
		if (obj != null)
		{
			if (obj is a6)
			{
				a10 = (a6)obj;
			}
			else if (obj is Array)
			{
				Array array = (Array)obj;
				a10 = (a6)array.GetValue(A_2);
			}
		}
		if (a10 == null || a10.f() == null)
		{
			A_4 = t2.b().b(u);
			A_5 = 0;
			A_6 = 3;
			A_7 = t2.b().b(v);
		}
		else
		{
			byte[] array2 = a10.f();
			A_4 = t2.b().b(array2);
			A_5 = array2.Length;
			if (a10.i())
			{
				if (a10.b() == 1)
				{
					A_6 = 1;
				}
				else
				{
					A_6 = 2;
				}
			}
			else if (a10.b() <= 0)
			{
				A_6 = 0;
			}
			else
			{
				A_6 = 3;
			}
			A_7 = t2.b().b(a10.e);
		}
		return -24200;
	}

	private static int a(IntPtr A_0, int A_1, int A_2, int A_3, out IntPtr A_4, out IntPtr A_5, ref byte A_6, out IntPtr A_7, out int A_8)
	{
		a6 a10 = null;
		t t2 = (t)((GCHandle)A_0).Target;
		object obj = t2.a();
		if (obj != null)
		{
			if (obj is a6)
			{
				a10 = (a6)obj;
			}
			else if (obj is Array)
			{
				Array array = (Array)obj;
				a10 = (a6)array.GetValue(A_2);
			}
		}
		if (a10 == null)
		{
			A_4 = t2.b().b(u);
			A_5 = IntPtr.Zero;
			A_6 = 3;
			A_7 = IntPtr.Zero;
			A_8 = 0;
		}
		else
		{
			if (A_6 == 1 || A_6 == 0)
			{
				a10.j();
			}
			a10.c();
			A_4 = t2.b().b(a10.f());
			A_5 = t2.b().b(a10.f);
			A_6 = 2;
			A_7 = t2.b().b(a10.e);
			A_8 = 0;
		}
		return -24200;
	}

	private static int a(IntPtr A_0, IntPtr A_1, int A_2, out IntPtr A_3, out IntPtr A_4, ref byte A_5, out IntPtr A_6, out ushort A_7)
	{
		a5 a10 = (a5)((GCHandle)A_0).Target;
		h[] array = a10.m_q;
		IntPtr[] array2 = a10.m_r;
		int num = array2.Length;
		int num2 = 0;
		for (int num3 = 0; num3 < num; num3++)
		{
			if (array2[num3] == A_1)
			{
				num2 = num3;
				break;
			}
		}
		array[num2].y(array[num2].w, A_1, A_2, out var A_8);
		A_4 = a10.m_n.b(A_8.f);
		A_3 = a10.m_n.b(A_8.f());
		IntPtr intPtr = a10.m_n.b(A_8.e);
		if (a10.v().d() >= 8010600 && a10.v().d() < 9000000)
		{
			intPtr = Devart.Common.z.a(intPtr, -A_2 * 2);
		}
		A_6 = intPtr;
		A_5 = 2;
		A_7 = 0;
		return -24200;
	}

	public override h[] m()
	{
		bool flag = this.m_a.a();
		if (this.m_b.OCIAttrGet(this.m_f, 4u, out IntPtr attributep, out IntPtr _, 18u, this.m_d) != 0)
		{
			return null;
		}
		int num = (int)attributep;
		if (num == 0)
		{
			return null;
		}
		h[] array = new h[num];
		for (int num2 = 0; num2 < num; num2++)
		{
			d(this.m_b.OCIParamGet(this.m_f, 4u, this.m_d, out var parmdpp, (uint)(num2 + 1)));
			HandleRef handleRef = new HandleRef(array[num2], parmdpp);
			try
			{
				d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep2, out IntPtr sizep2, 4u, this.m_d));
				d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep3, out IntPtr _, 2u, this.m_d));
				d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep4, out IntPtr sizep4, 1u, this.m_d));
				d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep5, out IntPtr _, 7u, this.m_d));
				if (flag)
				{
					array[num2].a = Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2);
				}
				else
				{
					array[num2].a = Marshal.PtrToStringAnsi(attributep2, (int)sizep2);
				}
				array[num2].d = (int)attributep4;
				array[num2].c = (short)(int)attributep3;
				array[num2].i = (int)attributep5 != 0;
				if (this.m_a.d() >= 12010000 && string.Compare(aa().v(), "12") >= 0)
				{
					d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep6, out IntPtr _, 104u, this.m_d));
					long num3 = (long)attributep6;
					if ((num3 & 1) != 0)
					{
						if ((num3 & 2) != 0)
						{
							array[num2].ad = OracleIdentityType.GeneratedAlways;
						}
						else if ((num3 & 4) != 0)
						{
							array[num2].ad = OracleIdentityType.GeneratedByDefaultOnNull;
						}
						else
						{
							array[num2].ad = OracleIdentityType.GeneratedByDefault;
						}
					}
				}
				switch (array[num2].c)
				{
				case 2:
				case 6:
				case 187:
				case 188:
				case 189:
				case 190:
				case 232:
				{
					d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep8, out IntPtr _, 5u, this.m_d));
					array[num2].e = (short)(int)attributep8;
					d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep9, out IntPtr _, 6u, this.m_d));
					array[num2].f = (sbyte)(int)attributep9;
					break;
				}
				case 108:
				case 110:
				case 122:
				{
					d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep10, out IntPtr sizep10, 8u, this.m_d));
					string name = ((!flag) ? Marshal.PtrToStringAnsi(attributep10, (int)sizep10) : Marshal.PtrToStringUni(attributep10, (int)sizep10 / 2));
					d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep11, out IntPtr sizep11, 9u, this.m_d));
					string text = ((!flag) ? Marshal.PtrToStringAnsi(attributep11, (int)sizep11) : Marshal.PtrToStringUni(attributep11, (int)sizep11 / 2));
					if (text != "")
					{
						array[num2].u = OracleUtils.QuoteIfNeed(text) + "." + OracleUtils.QuoteIfNeed(name);
					}
					else
					{
						array[num2].u = OracleUtils.QuoteIfNeed(name);
					}
					break;
				}
				case 1:
				case 5:
				case 11:
				case 96:
				case 97:
				case 104:
				{
					if (this.m_a.d() >= 9000000)
					{
						d(this.m_b.OCIAttrGet(handleRef, 53u, out attributep4, out sizep4, 286u, this.m_d));
						array[num2].d = (int)attributep4;
					}
					d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep12, out IntPtr _, 32u, this.m_d));
					array[num2].h = (((int)attributep12 == 2) ? 2 : 0);
					break;
				}
				case 112:
				{
					d(this.m_b.OCIAttrGet(handleRef, 53u, out IntPtr attributep7, out IntPtr _, 32u, this.m_d));
					array[num2].h = (((int)attributep7 == 2) ? 2 : 0);
					break;
				}
				case 106:
					throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
				}
			}
			finally
			{
				d(this.m_b.OCIDescriptorFree(handleRef, 53));
			}
		}
		return array;
	}

	private void a(int A_0, out int A_1, out int A_2)
	{
		string text = u();
		int num = 0;
		int num2 = num;
		if (A_0 > 0)
		{
			A_1 = 1;
			while (num < text.Length && num < A_0)
			{
				if (text[num] == '\r' || text[num] == '\n')
				{
					num = ((text[num] != '\r' || text[num + 1] != '\n') ? (num + 1) : (num + 2));
					A_1++;
					num2 = num;
				}
				else
				{
					num++;
				}
			}
			A_2 = num - num2 + 1;
		}
		else
		{
			A_1 = 0;
			A_2 = 0;
		}
	}

	public override void a(OracleException A_0)
	{
		if (this.m_a.d() < 8000500)
		{
			return;
		}
		IntPtr attributep;
		IntPtr sizep;
		if (this.m_a.d() >= 8010000)
		{
			d(this.m_b.OCIAttrGet(this.m_f, 4u, out attributep, out sizep, 129u, this.m_d));
		}
		else
		{
			d(this.m_b.OCIAttrGet(this.m_f, 4u, out attributep, out sizep, 128u, this.m_d));
		}
		int num = (int)attributep;
		if (u().Length > 0 && num > u().Length - 1)
		{
			num = u().Length - 1;
		}
		A_0.a(num);
		IntPtr attributep2 = IntPtr.Zero;
		IntPtr sizep2 = IntPtr.Zero;
		this.m_b.OCIAttrGet(this.m_f, 4u, out attributep2, out sizep2, 73u, this.m_d);
		int num2 = (int)attributep2;
		if (num2 <= 0)
		{
			return;
		}
		OracleError[] array = new OracleError[num2];
		for (int num3 = 0; num3 < num2; num3++)
		{
			IntPtr hndlpp = IntPtr.Zero;
			this.m_b.OCIHandleAlloc(this.m_c, out hndlpp, 2u, 0u, 0u);
			this.m_b.OCIParamGet(this.m_d, 2u, this.m_d, out hndlpp, (uint)num3);
			HandleRef handleRef = new HandleRef(this, hndlpp);
			try
			{
				IntPtr attributep3 = IntPtr.Zero;
				IntPtr sizep3 = IntPtr.Zero;
				this.m_b.OCIAttrGet(handleRef, 2u, out attributep3, out sizep3, 74u, this.m_d);
				string a_ = this.m_a.a(out var A_1, handleRef, out var A_2);
				OracleError oracleError = new OracleError((int)attributep3, A_1, a_, OracleObjectType.Unknown, null, null);
				oracleError.IsRecoverable = A_2;
				array[num3] = oracleError;
			}
			finally
			{
				this.m_b.OCIHandleFree(handleRef, 2);
			}
		}
		OracleErrorCollection a_2 = new OracleErrorCollection(array);
		A_0.a(a_2);
	}

	public override bool a(int A_0, az A_1)
	{
		this.m_k = 0;
		this.m_p = 0;
		if (f() == a9.b && A_0 > 0)
		{
			if (this.m_n != null)
			{
				this.m_n.c();
			}
			if (this.m_o)
			{
				c(A_0);
			}
		}
		else if (this.m_o)
		{
			n();
		}
		a a10 = new a(A_0, A_1);
		if (base.l > 0)
		{
			Thread thread = new Thread((ParameterizedThreadStart)a);
			thread.Start(a10);
			if (!thread.Join(base.l * 1000))
			{
				aa().f();
				thread.Join();
			}
		}
		else
		{
			a(a10);
		}
		if (a10.d != null)
		{
			throw a10.d;
		}
		int num = a10.c;
		base.i = false;
		base.j = "";
		switch (k())
		{
		case Devart.Data.Oracle.x.x:
		case Devart.Data.Oracle.x.y:
		case Devart.Data.Oracle.x.ad:
		case Devart.Data.Oracle.x.a6:
		case Devart.Data.Oracle.x.a7:
		case Devart.Data.Oracle.x.bn:
		case Devart.Data.Oracle.x.bq:
		case Devart.Data.Oracle.x.br:
		case Devart.Data.Oracle.x.bs:
		case Devart.Data.Oracle.x.b1:
		case Devart.Data.Oracle.x.b2:
		case Devart.Data.Oracle.x.b4:
		case Devart.Data.Oracle.x.b5:
		case Devart.Data.Oracle.x.b7:
		case Devart.Data.Oracle.x.b8:
		case Devart.Data.Oracle.x.c1:
			if (string.Compare(OracleUtils.c(aa().m()), "10") >= 0)
			{
				base.i = true;
				base.j = "24439";
			}
			this.m_j = -1;
			break;
		case Devart.Data.Oracle.x.d:
		case Devart.Data.Oracle.x.f:
		case Devart.Data.Oracle.x.j:
		{
			d(this.m_b.OCIAttrGet(this.m_f, 4u, out IntPtr attributep2, out IntPtr _, 9u, this.m_d));
			this.m_j = (int)attributep2;
			if (A_0 > 1 && (A_1 & az.k) == az.k)
			{
				if (this.m_j > 0)
				{
					d(this.m_b.OCIAttrGet(this.m_f, 4u, out IntPtr attributep3, out IntPtr sizep3, 469u, this.m_d));
					int num2 = (int)sizep3;
					t = new long[num2];
					Marshal.Copy(attributep3, t, 0, num2);
				}
				else
				{
					t = new long[A_0];
				}
			}
			break;
		}
		case Devart.Data.Oracle.x.e:
			if (A_0 > 0 && num != 99)
			{
				d(this.m_b.OCIAttrGet(this.m_f, 4u, out IntPtr attributep, out IntPtr _, 9u, this.m_d));
				this.m_k = attributep.ToInt32();
			}
			this.m_j = -1;
			break;
		default:
			this.m_j = -1;
			break;
		}
		switch (num)
		{
		case 1:
		{
			string text = this.m_a.a(out var A_2);
			if (A_2 == 24344)
			{
				base.i = true;
				base.j = text;
			}
			else
			{
				d(num);
			}
			break;
		}
		case 100:
			if (f() == a9.i || f() == a9.j)
			{
				d(num);
			}
			base.k = true;
			return false;
		default:
			d(num);
			break;
		}
		return true;
	}

	private void a(object A_0)
	{
		a a10 = (a)A_0;
		try
		{
			a10.c = this.m_b.OCIStmtExecute(this.m_e, this.m_f, this.m_d, a10.a, 0, 0, 0, (int)a10.b);
		}
		catch (AccessViolationException a_)
		{
			a10.d = new OracleException(81, Devart.Common.al.a("InternalOracleClientError"), a_);
		}
	}

	private void n()
	{
		int num = this.m_q.Length;
		for (int num2 = 0; num2 < num; num2++)
		{
			at at2 = (at)this.m_q[num2].v;
			if (at2 == null)
			{
				continue;
			}
			int num3 = this.m_q[num2].l;
			int num4 = this.m_q[num2].p;
			for (int num5 = 0; num5 < this.m_p; num5++)
			{
				IntPtr intPtr = Devart.Common.z.e(this.m_i, num3);
				if ((long)intPtr != 0 && this.m_i[num3 - 1] != 0)
				{
					at2.a(intPtr);
					this.m_i[num3 - 1] = 0;
				}
				num3 += num4;
			}
		}
	}

	private void c(int A_0)
	{
		n();
		Array.Clear(this.m_i, 0, this.m_i.Length);
		int num = this.m_q.Length;
		for (int num2 = 0; num2 < num; num2++)
		{
			at at2 = (at)this.m_q[num2].v;
			if (at2 == null)
			{
				continue;
			}
			int num3 = this.m_q[num2].l;
			int num4 = this.m_q[num2].p;
			for (int num5 = 0; num5 < A_0; num5++)
			{
				if (this.m_q[num2].s == null)
				{
					IntPtr a_ = at2.a();
					Devart.Common.z.a(this.m_i, num3, a_);
				}
				else
				{
					Devart.Common.z.a(this.m_i, num3, IntPtr.Zero);
				}
				this.m_i[num3 - 1] = byte.MaxValue;
				num3 += num4;
			}
		}
		this.m_p = A_0;
	}

	public override bool b(int A_0)
	{
		if (this.m_n != null)
		{
			this.m_n.c();
		}
		if (this.m_o)
		{
			c(A_0);
		}
		while (true)
		{
			int num = this.m_b.OCIStmtFetch(this.m_f, this.m_d, A_0, 2, 0u);
			if (num != 99)
			{
				d(this.m_b.OCIAttrGet(this.m_f, 4u, out IntPtr attributep, out IntPtr _, 9u, this.m_d));
				this.m_k = attributep.ToInt32();
			}
			switch (num)
			{
			case 0:
				return true;
			case 100:
				return false;
			case 99:
				break;
			default:
				d(num);
				return true;
			}
			d(this.m_b.OCIStmtGetPieceInfo(this.m_f, this.m_d, out var hndlpp, out var typep, out var _, out var iterp, out var _, out var piecep));
			a(p(), hndlpp, iterp, out var A_1, out var A_2, ref piecep, out var A_3, out var A_4);
			d(this.m_b.OCIStmtSetPieceInfo(hndlpp, typep, this.m_d, A_1, A_2, piecep, A_3, ref A_4));
		}
	}

	public void d(int A_0)
	{
		if (A_0 != 0)
		{
			this.m_a.c(A_0);
		}
	}

	[SpecialName]
	public override bool a()
	{
		return !this.m_h;
	}

	[SpecialName]
	public override int i()
	{
		return this.m_j;
	}

	[SpecialName]
	public override int h()
	{
		d(this.m_b.OCIAttrGet(this.m_f, 4u, out int attributep, out int _, 11u, this.m_d));
		return attributep;
	}

	[SpecialName]
	public override void a(int A_0)
	{
		d(this.m_b.OCIAttrSet(this.m_f, 4u, ref A_0, 0, 11u, this.m_d));
	}

	[SpecialName]
	public override af g()
	{
		d(this.m_b.OCIAttrGet(this.m_f, 4u, out int attributep, out int _, 182u, this.m_d));
		return (af)attributep;
	}

	[SpecialName]
	public override a9 f()
	{
		d(this.m_b.OCIAttrGet(this.m_f, 4u, out int attributep, out int _, 24u, this.m_d));
		return (a9)attributep;
	}

	[SpecialName]
	public override x k()
	{
		d(this.m_b.OCIAttrGet(this.m_f, 4u, out int attributep, out int _, 10u, this.m_d));
		return (x)attributep;
	}

	[SpecialName]
	public override int j()
	{
		return this.m_k;
	}

	[SpecialName]
	public override string b()
	{
		IntPtr descpp = IntPtr.Zero;
		IntPtr sizep = IntPtr.Zero;
		string result = string.Empty;
		d(this.m_b.OCIDescriptorAlloc(this.m_c, out descpp, 54, 0u, 0u));
		try
		{
			int num = 100;
			num = this.m_b.OCIAttrGet(this.m_f, 4u, descpp, out sizep, 19u, this.m_d);
			if (num != 100)
			{
				d(num);
				int num2 = this.m_a.d();
				if (num2 >= 9000000)
				{
					byte[] array = new byte[4000];
					HandleRef rowidDesc = new HandleRef(this, descpp);
					ushort outbflp = 4000;
					d(this.m_b.OCIRowidToChar(rowidDesc, array, ref outbflp, this.m_d));
					result = Encoding.Default.GetString(array, 0, outbflp);
				}
				else if (num2 >= 8010000)
				{
					bk.d d2 = default(bk.d);
					d2 = (bk.d)Marshal.PtrToStructure(descpp, typeof(bk.d));
					if (d2.c != IntPtr.Zero)
					{
						bk.b b2 = default(bk.b);
						b2 = (bk.b)Marshal.PtrToStructure(d2.c, typeof(bk.b));
						if (b2.a == 2)
						{
							byte[] array2 = new byte[14];
							Marshal.Copy(Devart.Common.z.a(d2.c, 1), array2, 0, 14);
							int a_ = Marshal.ReadInt16(descpp, 12);
							result = bk.a(array2, a_);
						}
						else
						{
							result = bk.a(b2);
						}
					}
				}
				else
				{
					bk.e e2 = default(bk.e);
					result = bk.a(((bk.e)Marshal.PtrToStructure(descpp, typeof(bk.e))).c);
				}
			}
		}
		finally
		{
			d(this.m_b.OCIDescriptorFree(descpp, 54));
		}
		return result;
	}

	[SpecialName]
	public IntPtr q()
	{
		return this.m_f.Handle;
	}

	[SpecialName]
	public IntPtr p()
	{
		GCHandle gCHandle = GCHandle.Alloc(this);
		if (this.m_l == null)
		{
			this.m_l = new Devart.Common.z();
		}
		this.m_l.a(gCHandle);
		return (IntPtr)gCHandle;
	}

	public bool b(object A_0)
	{
		if (!(A_0 is a5 a10))
		{
			return base.Equals(A_0);
		}
		return a10.q() == q();
	}

	public int o()
	{
		return q().GetHashCode();
	}

	[SpecialName]
	public override long[] d()
	{
		return t;
	}

	public override s[] c()
	{
		if (this.m_a.d() < 12010000 || string.Compare(aa().v(), "12") < 0)
		{
			return null;
		}
		d(this.m_b.OCIAttrGet(this.m_f, 4u, out IntPtr attributep, out IntPtr _, 463u, this.m_d));
		int num = attributep.ToInt32();
		if (num == 0)
		{
			return null;
		}
		s[] array = new s[num];
		for (int num2 = 0; num2 < num; num2++)
		{
			d(this.m_b.OCIStmtGetNextResult(this.m_f, this.m_d, out var result, out var rtype, 0));
			if (rtype == 1)
			{
				a5 a10 = new a5((v)aa(), result, A_2: false);
				a10.g = 0;
				array[num2] = a10;
				continue;
			}
			throw new NotSupportedException($"Unknown result type {rtype} rtype.");
		}
		return array;
	}
}
