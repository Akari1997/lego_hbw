using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Devart.Common;

namespace Devart.Data.Oracle;

internal sealed class bg : k
{
	private new readonly bool m_a;

	private new Oci m_b;

	private new readonly HandleRef m_c;

	private new readonly HandleRef m_d;

	public bg(v A_0, int A_1, int A_2, Type A_3, OracleType A_4)
		: base(A_0, A_1, A_2, A_3, A_0.h().o(), A_4)
	{
		this.m_c = A_0.h().h();
		this.m_d = A_0.h().k();
		this.m_b = A_0.h().j();
		switch (A_1)
		{
		case 58:
		case 108:
		case 110:
		case 112:
		case 113:
		case 114:
		case 115:
		case 116:
		case 122:
		case 187:
		case 188:
		case 232:
		case 247:
		case 248:
			return;
		}
		this.m_a = true;
	}

	public override byte i(byte[] A_0, int A_1, int A_2)
	{
		return base.i(A_0, A_1, A_2);
	}

	public override DateTime e(byte[] A_0, int A_1, int A_2)
	{
		return base.e(A_0, A_1, A_2);
	}

	public override TimeSpan f(byte[] A_0, int A_1, int A_2)
	{
		return base.f(A_0, A_1, A_2);
	}

	public override decimal h(byte[] A_0, int A_1, int A_2)
	{
		return base.h(A_0, A_1, A_2);
	}

	public override double d(byte[] A_0, int A_1, int A_2)
	{
		return base.d(A_0, A_1, A_2);
	}

	public override float c(byte[] A_0, int A_1, int A_2)
	{
		return base.c(A_0, A_1, A_2);
	}

	public override short b(byte[] A_0, int A_1, int A_2)
	{
		int num = e();
		if (num == 189)
		{
			return (short)r(A_0, A_1, A_2).Value;
		}
		return base.b(A_0, A_1, A_2);
	}

	public override int a(byte[] A_0, int A_1, int A_2)
	{
		int num = e();
		if (num == 189)
		{
			return (int)r(A_0, A_1, A_2).Value;
		}
		return base.a(A_0, A_1, A_2);
	}

	public override long g(byte[] A_0, int A_1, int A_2)
	{
		int num = e();
		if (num == 189)
		{
			return r(A_0, A_1, A_2).Value;
		}
		return base.g(A_0, A_1, A_2);
	}

	public override s a(byte[] A_0, int A_1, bool A_2)
	{
		if (this.m_a)
		{
			return base.a(A_0, A_1, A_2);
		}
		int num = e();
		if (num == 116)
		{
			bool a_ = true;
			if (A_2)
			{
				if (A_0[A_1 - 1] == 0)
				{
					a_ = false;
				}
				A_0[A_1 - 1] = 0;
			}
			IntPtr a_2 = Devart.Common.z.e(A_0, A_1);
			return new a5(b(), a_2, a_);
		}
		return base.a(A_0, A_1, A_2);
	}

	public override OracleBinary j(byte[] A_0, int A_1, int A_2)
	{
		return base.j(A_0, A_1, A_2);
	}

	public override OracleDate s(byte[] A_0, int A_1, int A_2)
	{
		return base.s(A_0, A_1, A_2);
	}

	public override OracleTimeStamp v(byte[] A_0, int A_1, int A_2)
	{
		switch (e())
		{
		case 187:
		case 188:
		case 232:
			return new OracleTimeStamp(Devart.Common.z.e(A_0, A_1), k(), b());
		default:
			return base.v(A_0, A_1, A_2);
		}
	}

	public override OracleIntervalDS u(byte[] A_0, int A_1, int A_2)
	{
		int num = e();
		if (num == 190)
		{
			return new OracleIntervalDS(Devart.Common.z.e(A_0, A_1), b());
		}
		return base.u(A_0, A_1, A_2);
	}

	public override OracleIntervalYM r(byte[] A_0, int A_1, int A_2)
	{
		int num = e();
		if (num == 189)
		{
			return new OracleIntervalYM(Devart.Common.z.e(A_0, A_1), b());
		}
		return base.r(A_0, A_1, A_2);
	}

	public override a3 a(byte[] A_0, int A_1, bool A_2, bool A_3)
	{
		switch (e())
		{
		case 112:
		case 113:
		case 114:
		case 115:
		{
			bool a_ = A_3;
			if (A_2)
			{
				a_ = A_0[A_1 - 1] != 0;
				A_0[A_1 - 1] = 0;
			}
			IntPtr a_2 = Devart.Common.z.e(A_0, A_1);
			return new p(A_2: (short)(((b().h().a() || OracleUtils.OracleClientCompatible) && (e() == 112 || e() == 115)) ? 1000 : 0), A_0: b(), A_1: a_2, A_3: i(), A_4: a_);
		}
		default:
			return base.a(A_0, A_1, A_2, A_3);
		}
	}

	public override OracleNumber l(byte[] A_0, int A_1, int A_2)
	{
		switch (e())
		{
		case 4:
		case 22:
		case 101:
		{
			double rnum2 = Devart.Common.e.i(A_0, A_1);
			byte[] array3 = new byte[22];
			a(this.m_b.OCINumberFromReal(this.m_d, ref rnum2, Marshal.SizeOf(typeof(double)), array3));
			return new OracleNumber(array3);
		}
		case 21:
		case 100:
		{
			float rnum = Devart.Common.e.e(A_0, A_1);
			byte[] array2 = new byte[22];
			a(this.m_b.OCINumberFromReal(this.m_d, ref rnum, Marshal.SizeOf(typeof(float)), array2));
			return new OracleNumber(array2);
		}
		case 3:
		case 246:
		{
			int inum = Devart.Common.e.g(A_0, A_1);
			byte[] array = new byte[22];
			a(this.m_b.OCINumberFromInt(this.m_d, ref inum, Marshal.SizeOf(typeof(int)), 2, array));
			return new OracleNumber(array);
		}
		default:
			return base.l(A_0, A_1, A_2);
		}
	}

	public override OracleString k(byte[] A_0, int A_1, int A_2)
	{
		return base.k(A_0, A_1, A_2);
	}

	public override bf a(byte[] A_0, int A_1, bool A_2, bf A_3)
	{
		OracleDbType oracleDbType = k();
		if (oracleDbType == OracleDbType.Object)
		{
			return (bf)a(A_0, A_1, A_2, (bh)A_3);
		}
		return base.a(A_0, A_1, A_2, A_3);
	}

	public override ak a(byte[] A_0, int A_1, bool A_2, ak A_3)
	{
		OracleDbType oracleDbType = k();
		if (oracleDbType == OracleDbType.Array || oracleDbType == OracleDbType.Table)
		{
			return (ak)a(A_0, A_1, A_2, (m)A_3);
		}
		return base.a(A_0, A_1, A_2, A_3);
	}

	public override al a(byte[] A_0, int A_1, bool A_2, al A_3)
	{
		OracleDbType oracleDbType = k();
		if (oracleDbType == OracleDbType.Xml)
		{
			return (al)a(A_0, A_1, A_2, (bj)A_3);
		}
		return base.a(A_0, A_1, A_2, A_3);
	}

	public override b a(byte[] A_0, int A_1, bool A_2, b A_3)
	{
		OracleDbType oracleDbType = k();
		if (oracleDbType == OracleDbType.AnyData)
		{
			return (b)a(A_0, A_1, A_2, (a2)A_3);
		}
		return base.a(A_0, A_1, A_2, A_3);
	}

	public override f a(byte[] A_0, int A_1, bool A_2, f A_3)
	{
		OracleDbType oracleDbType = k();
		if (oracleDbType == OracleDbType.Ref)
		{
			IntPtr intPtr = Devart.Common.z.e(A_0, A_1);
			if (A_3 != null && ((l)A_3).c().Handle == intPtr)
			{
				return A_3;
			}
			return new l(intPtr, j(), b(), !A_2);
		}
		return base.a(A_0, A_1, A_2, A_3);
	}

	public override void a(byte[] A_0, int A_1, object A_2)
	{
		switch (e())
		{
		case 112:
		case 113:
		case 114:
		case 115:
			if (A_2 != null && A_2 != DBNull.Value)
			{
				if (A_2 is p p2)
				{
					Devart.Common.z.a(A_0, A_1, p2.q());
				}
				else
				{
					Devart.Common.z.a(A_0, A_1, (IntPtr)A_2);
				}
			}
			else
			{
				Devart.Common.z.d(A_0, A_1);
			}
			break;
		case 116:
			if (A_2 != null)
			{
				a5 a10 = (a5)A_2;
				Devart.Common.z.a(A_0, A_1, a10.q());
			}
			else
			{
				Devart.Common.z.d(A_0, A_1);
			}
			break;
		case 189:
			if (b().h().d() < 9000000)
			{
				throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
			}
			if (A_2 != null)
			{
				OracleIntervalYM oracleIntervalYM = (OracleIntervalYM)A_2;
				a(this.m_b.OCIDescriptorAlloc(this.m_c, out var descpp2, 62, 0u, 0u));
				int inum = 0;
				byte[] number = new byte[22];
				a(this.m_b.OCINumberFromInt(this.m_d, ref inum, 4, 0, number));
				a(this.m_b.OCIIntervalFromNumber(this.m_c, this.m_d, descpp2, number));
				a(this.m_b.OCIIntervalSetYearMonth(this.m_c, this.m_d, oracleIntervalYM.Years, oracleIntervalYM.Months, descpp2));
				Devart.Common.z.a(A_0, A_1, descpp2);
			}
			else
			{
				Devart.Common.z.d(A_0, A_1);
			}
			break;
		case 190:
			if (b().h().d() < 9000000)
			{
				throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
			}
			if (A_2 != null)
			{
				OracleIntervalDS oracleIntervalDS = (OracleIntervalDS)A_2;
				a(this.m_b.OCIDescriptorAlloc(this.m_c, out var descpp3, 63, 0u, 0u));
				int inum2 = 0;
				byte[] number2 = new byte[22];
				a(this.m_b.OCINumberFromInt(this.m_d, ref inum2, 4, 0, number2));
				a(this.m_b.OCIIntervalFromNumber(this.m_c, this.m_d, descpp3, number2));
				a(this.m_b.OCIIntervalSetDaySecond(this.m_c, this.m_d, oracleIntervalDS.Days, oracleIntervalDS.Hours, oracleIntervalDS.Minutes, oracleIntervalDS.Seconds, oracleIntervalDS.Nanoseconds, descpp3));
				Devart.Common.z.a(A_0, A_1, descpp3);
			}
			else
			{
				Devart.Common.z.d(A_0, A_1);
			}
			break;
		case 187:
		case 188:
		case 232:
			if (b().h().d() < 9000000)
			{
				throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
			}
			if (A_2 != null)
			{
				OracleTimeStamp oracleTimeStamp = (OracleTimeStamp)A_2;
				int type = 0;
				switch (e())
				{
				case 187:
					type = 68;
					break;
				case 188:
					type = 69;
					break;
				case 232:
					type = 70;
					break;
				}
				a(this.m_b.OCIDescriptorAlloc(this.m_c, out var descpp, type, 0u, 0u));
				if (oracleTimeStamp.TimeStampType == OracleDbType.TimeStampTZ)
				{
					byte[] bytes = b().h().o().GetBytes(oracleTimeStamp.TimeZone);
					a(this.m_b.OCIDateTimeConstruct(this.m_c, this.m_d, descpp, (short)oracleTimeStamp.Year, (byte)oracleTimeStamp.Month, (byte)oracleTimeStamp.Day, (byte)oracleTimeStamp.Hour, (byte)oracleTimeStamp.Minute, (byte)oracleTimeStamp.Second, (uint)oracleTimeStamp.Nanosecond, bytes, (uint)bytes.Length));
				}
				else
				{
					a(this.m_b.OCIDateTimeConstruct(this.m_c, this.m_d, descpp, (short)oracleTimeStamp.Year, (byte)oracleTimeStamp.Month, (byte)oracleTimeStamp.Day, (byte)oracleTimeStamp.Hour, (byte)oracleTimeStamp.Minute, (byte)oracleTimeStamp.Second, (uint)oracleTimeStamp.Nanosecond, null, 0u));
				}
				Devart.Common.z.a(A_0, A_1, descpp);
			}
			else
			{
				Devart.Common.z.d(A_0, A_1);
			}
			break;
		case 58:
		case 108:
		case 122:
		case 247:
		case 248:
			if (A_2 == null || A_2 == DBNull.Value)
			{
				Devart.Common.z.a(A_0, A_1, IntPtr.Zero);
				Devart.Common.z.a(A_0, A_1 + IntPtr.Size, Devart.Data.Oracle.r.i);
			}
			else
			{
				r r2 = (r)A_2;
				Devart.Common.z.a(A_0, A_1, r2.r().Handle);
				Devart.Common.z.a(A_0, A_1 + IntPtr.Size, r2.n().Handle);
			}
			break;
		case 110:
		{
			if (A_2 == null || A_2 == DBNull.Value)
			{
				Devart.Common.z.a(A_0, A_1, IntPtr.Zero);
				break;
			}
			l l2 = (l)A_2;
			Devart.Common.z.a(A_0, A_1, l2.c().Handle);
			break;
		}
		default:
			base.a(A_0, A_1, A_2);
			break;
		}
	}

	protected override object a(byte[] A_0, int A_1)
	{
		int num = e();
		if (num == 95)
		{
			IntPtr raw = Marshal.ReadIntPtr(A_0, A_1);
			byte[] array = new byte[this.m_b.OCIRawSize(this.m_c, raw)];
			Marshal.Copy(this.m_b.OCIRawPtr(this.m_c, raw), array, 0, array.Length);
			return new OracleBinary(array);
		}
		return base.a(A_0, A_1);
	}

	private new r a(byte[] A_0, int A_1, bool A_2, r A_3)
	{
		IntPtr intPtr = Devart.Common.z.e(A_0, A_1);
		if (intPtr == IntPtr.Zero)
		{
			return null;
		}
		if (A_3 != null && A_3.l() == intPtr)
		{
			return A_3;
		}
		IntPtr a_ = Devart.Common.z.e(A_0, A_1 + IntPtr.Size);
		r r2 = null;
		bool flag = true;
		if (A_2)
		{
			OracleDbType oracleDbType = j().h;
			if (oracleDbType == OracleDbType.Array || oracleDbType == OracleDbType.Object || oracleDbType == OracleDbType.Table)
			{
				flag = false;
			}
			else if (A_0[A_1 - 1] == 0)
			{
				flag = false;
			}
		}
		switch (j().h)
		{
		case OracleDbType.Array:
			r2 = new m(new HandleRef(j(), intPtr), a_, j(), b(), A_4: false, flag);
			break;
		case OracleDbType.Object:
			r2 = new bh(new HandleRef(j(), intPtr), a_, j(), b(), A_4: false, flag);
			break;
		case OracleDbType.Table:
			r2 = new ar(new HandleRef(j(), intPtr), a_, j(), b(), A_4: false, flag);
			break;
		case OracleDbType.Xml:
			r2 = new bj(new HandleRef(j(), intPtr), a_, b(), flag);
			break;
		case OracleDbType.AnyData:
			r2 = new a2(new HandleRef(j(), intPtr), a_, b(), A_3: false, flag);
			break;
		default:
			return null;
		}
		if (A_2 && flag && r2 != null)
		{
			A_0[A_1 - 1] = 0;
		}
		return r2;
	}

	private new void a(int A_0)
	{
		if (A_0 != 0)
		{
			b().h().c(A_0);
		}
	}

	private new static void b(byte[] A_0, int A_1)
	{
	}

	[SpecialName]
	public override string a()
	{
		switch (e())
		{
		case 108:
		case 110:
		case 122:
		case 247:
		case 248:
			return j().Name;
		default:
			return base.a();
		}
	}

	[SpecialName]
	public new v b()
	{
		return (v)base.b();
	}

	internal new void c(byte[] A_0, int A_1)
	{
		switch (e())
		{
		case 187:
		case 188:
		case 189:
		case 190:
		case 232:
		{
			IntPtr intPtr = Devart.Common.z.e(A_0, A_1);
			if (intPtr != IntPtr.Zero)
			{
				int type = 0;
				switch (e())
				{
				case 187:
					type = 68;
					break;
				case 188:
					type = 69;
					break;
				case 232:
					type = 70;
					break;
				case 190:
					type = 63;
					break;
				case 189:
					type = 62;
					break;
				}
				a(this.m_b.OCIDescriptorFree(intPtr, type));
			}
			break;
		}
		}
	}
}
