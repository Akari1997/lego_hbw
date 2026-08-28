using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal sealed class p : a3
{
	private new readonly Oci m_a;

	private new readonly HandleRef m_b;

	private new readonly HandleRef m_c;

	private new readonly HandleRef m_d;

	private new IntPtr m_e;

	private new bool m_f;

	private new short m_g;

	private new byte m_h;

	private new int m_i;

	public p(v A_0)
		: base(A_0)
	{
		aa aa2 = A_0.h();
		this.m_b = aa2.h();
		this.m_c = aa2.k();
		this.m_d = A_0.r();
		this.m_a = aa2.j();
		this.m_i = A_0.w().r();
	}

	public p(v A_0, int A_1)
		: this(A_0)
	{
		b(56);
	}

	public p(v A_0, ah A_1, bool A_2)
		: this(A_0)
	{
		b(50);
		if (A_1 == ah.c)
		{
			this.m_h = 2;
			this.m_g = 1000;
		}
		if ((A_0.h().a() || OracleUtils.OracleClientCompatible) && A_1 != ah.a)
		{
			this.m_g = 1000;
		}
		if (A_1 == ah.c)
		{
			using (s s2 = new a5(A_0, A_1: false))
			{
				s2.a("begin sys.dbms_lob.createtemporary(:1, false); end;");
				h[] array = new h[1];
				array[0].a = "1";
				array[0].c = 112;
				array[0].h = 2;
				array[0].n = 0;
				array[0].l = 2;
				array[0].m = IntPtr.Size;
				byte[] array2 = new byte[2 + IntPtr.Size];
				Devart.Common.z.a(array2, array[0].l, this.m_e);
				s2.b(array, array2, null);
				s2.a(1, az.a);
				return;
			}
		}
		c(this.m_a.OCILobCreateTemporary(this.m_d, this.m_c, this.m_e, this.m_g, this.m_h, (byte)A_1, A_2, 10));
	}

	public p(v A_0, IntPtr A_1, short A_2, int A_3, bool A_4)
		: this(A_0)
	{
		this.m_f = A_4;
		this.m_e = A_1;
		this.m_h = (byte)A_3;
		this.m_g = A_2;
		if (A_3 == 2)
		{
			this.m_g = 1000;
		}
	}

	private void b(int A_0)
	{
		c(this.m_a.OCIDescriptorAlloc(this.m_b, out this.m_e, A_0, 0u, 0u));
		this.m_f = true;
	}

	public override void o()
	{
		if (this.m_f)
		{
			if (l())
			{
				c(this.m_a.OCILobFreeTemporary(this.m_d, this.m_c, this.m_e));
			}
			c(this.m_a.OCIDescriptorFree(this.m_e, 50));
		}
		this.m_e = IntPtr.Zero;
		this.m_f = false;
	}

	public override int a(int A_0, byte[] A_1, int A_2, int A_3)
	{
		if (A_1 == null)
		{
			throw new ArgumentNullException("buffer");
		}
		if (A_2 < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("BufferOffsetNotNegative"), "offset");
		}
		if (A_3 < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("CountCanNotBeNegative"), "count");
		}
		if (A_2 + A_3 > A_1.Length)
		{
			throw new ArgumentException(Devart.Common.al.a("BufferOffCcanNotBeGreaterBufferLength"));
		}
		if (A_3 == 0)
		{
			return 0;
		}
		int num = A_3;
		if (this.m_i == 0)
		{
			if (this.m_g == 1000)
			{
				A_3 /= 2;
				A_0 /= 2;
			}
			c(this.m_a.OCILobRead(this.m_d, this.m_c, this.m_e, ref A_3, A_0 + 1, A_1, A_1.Length - A_2, 0, 0, this.m_g, this.m_h));
		}
		else
		{
			A_3 = 0;
			int num2 = this.m_i * 1024 * 1024;
			byte[] array = ((num >= num2) ? new byte[num2] : A_1);
			int num3 = num;
			int num4 = ((this.m_g != 1000) ? A_0 : (A_0 / 2));
			int num5 = A_0;
			while (num3 > 0)
			{
				int num6 = ((num3 > num2) ? num2 : num3);
				int amtp = ((this.m_g != 1000) ? num6 : (num6 / 2));
				c(this.m_a.OCILobRead(this.m_d, this.m_c, this.m_e, ref amtp, num4 + 1, array, num6, 0, 0, this.m_g, this.m_h));
				Array.Copy(array, 0, A_1, num5, num6);
				num3 -= num6;
				num5 += num6;
				num4 += amtp;
				A_3 += amtp;
			}
		}
		if (this.m_g == 1000)
		{
			A_3 *= 2;
		}
		return A_3;
	}

	public override string i()
	{
		OracleDbType a_ = ((p() == 2) ? OracleDbType.NClob : OracleDbType.Clob);
		OracleLob oracleLob = new OracleLob(null, null, 0, this, a_);
		oracleLob.f();
		long num = oracleLob.Length;
		if (num <= 64000)
		{
			return (string)oracleLob.Value;
		}
		if (num < 128)
		{
			num = 128L;
		}
		else if (num > 1441792)
		{
			num = 1441792L;
		}
		Encoding encoding = ((this.m_h != 2 && !t().h().a()) ? Encoding.Default : Encoding.Unicode);
		StreamReader streamReader = new StreamReader(oracleLob, encoding, detectEncodingFromByteOrderMarks: true, (int)num);
		return streamReader.ReadToEnd();
	}

	public override int b(int A_0, byte[] A_1, int A_2, int A_3)
	{
		if (A_1 == null)
		{
			throw new ArgumentNullException("buffer");
		}
		if (A_2 < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("BufferOffsetNotNegative"), "offset");
		}
		if (A_3 < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("CountCanNotBeNegative"), "count");
		}
		if (A_2 + A_3 > A_1.Length)
		{
			throw new ArgumentException(Devart.Common.al.a("BufferOffCcanNotBeGreaterBufferLength"));
		}
		if (A_3 == 0)
		{
			return 0;
		}
		int num = A_3;
		if (this.m_i == 0)
		{
			if (this.m_g == 1000)
			{
				A_3 /= 2;
				A_0 /= 2;
			}
			c(this.m_a.OCILobWrite(this.m_d, this.m_c, this.m_e, ref A_3, A_0 + 1, A_1, num, 0, 0, 0, this.m_g, this.m_h));
		}
		else
		{
			A_3 = 0;
			int num2 = 16777216;
			byte[] array = ((num >= num2) ? new byte[num2] : A_1);
			int num3 = num;
			int num4 = ((this.m_g != 1000) ? A_0 : (A_0 / 2));
			int num5 = A_0;
			while (num3 > 0)
			{
				int num6 = ((num3 > num2) ? num2 : num3);
				int amtp = ((this.m_g != 1000) ? num6 : (num6 / 2));
				Array.Copy(A_1, num5, array, 0, num6);
				c(this.m_a.OCILobWrite(this.m_d, this.m_c, this.m_e, ref amtp, num4 + 1, array, num6, 0, 0, 0, this.m_g, this.m_h));
				num3 -= num6;
				num5 += num6;
				num4 += amtp;
				A_3 += amtp;
			}
		}
		if (this.m_g == 1000)
		{
			A_3 *= 2;
		}
		return A_3;
	}

	public override void b(a3 A_0)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException("source");
		}
		c(this.m_a.OCILobAppend(this.m_d, this.m_c, this.m_e, ((p)A_0).m_e));
	}

	public override long a(a3 A_0, int A_1, int A_2, int A_3)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException("source");
		}
		c(this.m_a.OCILobCopy(this.m_d, this.m_c, this.m_e, ((p)A_0).m_e, A_3, A_2 + 1, A_1 + 1));
		return A_3;
	}

	public override long b(a3 A_0, int A_1, int A_2, int A_3)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException("source");
		}
		c(this.m_a.OCILobLoadFromFile(this.m_d, this.m_c, this.m_e, ((p)A_0).m_e, A_3, A_2 + 1, A_1 + 1));
		return A_3;
	}

	public override void a(OracleLobOpenMode A_0)
	{
		c(this.m_a.OCILobOpen(this.m_d, this.m_c, this.m_e, (byte)A_0));
	}

	public override void m()
	{
		c(this.m_a.OCILobClose(this.m_d, this.m_c, this.m_e));
	}

	public override int a(int A_0, int A_1)
	{
		c(this.m_a.OCILobErase(this.m_d, this.m_c, this.m_e, ref A_1, A_0));
		return A_1;
	}

	public override void a(int A_0)
	{
		c(this.m_a.OCILobTrim(this.m_d, this.m_c, this.m_e, A_0));
	}

	public override bool a(a3 A_0)
	{
		c(this.m_a.OCILobIsEqual(this.m_b, this.m_e, ((p)A_0).m_e, out var is_equal));
		return is_equal != 0;
	}

	public override void g()
	{
		c(this.m_a.OCILobFileOpen(this.m_d, this.m_c, this.m_e, 1));
	}

	public override void a(string A_0, string A_1)
	{
		Encoding encoding = r().o();
		byte[] bytes = encoding.GetBytes(A_0);
		byte[] bytes2 = encoding.GetBytes(A_1);
		c(this.m_a.OCILobFileSetName(this.m_b, this.m_c, ref this.m_e, bytes, (short)bytes.Length, bytes2, (short)bytes2.Length));
	}

	public override void a(out string A_0, out string A_1)
	{
		int num = 1;
		if (r().a())
		{
			num = 2;
		}
		byte[] array = new byte[30 * num];
		ushort d_length = (ushort)array.Length;
		byte[] array2 = new byte[255 * num];
		ushort f_length = (ushort)array2.Length;
		c(this.m_a.OCILobFileGetName(this.m_b, this.m_c, this.m_e, array, ref d_length, array2, ref f_length));
		Encoding encoding = r().o();
		A_0 = encoding.GetString(array, 0, d_length);
		A_1 = encoding.GetString(array2, 0, f_length);
	}

	public override void n()
	{
		c(this.m_a.OCILobFileClose(this.m_d, this.m_c, this.m_e));
	}

	public void c(int A_0)
	{
		if (A_0 != 0)
		{
			r().c(A_0);
		}
	}

	[SpecialName]
	public aa r()
	{
		return (aa)t().h();
	}

	[SpecialName]
	public IntPtr q()
	{
		return this.m_e;
	}

	[SpecialName]
	public override bool a()
	{
		return this.m_e == IntPtr.Zero;
	}

	[SpecialName]
	public override bool e()
	{
		throw new NotImplementedException();
	}

	[SpecialName]
	public override bool b()
	{
		c(this.m_a.OCILobIsOpen(this.m_d, this.m_c, this.m_e, out var flag));
		return flag != 0;
	}

	[SpecialName]
	public override bool l()
	{
		c(this.m_a.OCILobIsTemporary(this.m_b, this.m_c, this.m_e, out var is_temporary));
		return is_temporary != 0;
	}

	[SpecialName]
	public override bool c()
	{
		return f() == 0;
	}

	[SpecialName]
	public override bool h()
	{
		c(this.m_a.OCILobFileIsOpen(this.m_d, this.m_c, this.m_e, out var flag));
		return flag != 0;
	}

	[SpecialName]
	public override bool j()
	{
		c(this.m_a.OCILobFileExists(this.m_d, this.m_c, this.m_e, out var flag));
		return flag != 0;
	}

	[SpecialName]
	public override int k()
	{
		c(this.m_a.OCILobGetChunkSize(this.m_d, this.m_c, this.m_e, out var chunk_size));
		return chunk_size;
	}

	[SpecialName]
	public override short d()
	{
		return this.m_g;
	}

	[SpecialName]
	public override byte p()
	{
		return this.m_h;
	}

	[SpecialName]
	public override int f()
	{
		c(this.m_a.OCILobGetLength(this.m_d, this.m_c, this.m_e, out var lenp));
		return lenp;
	}
}
