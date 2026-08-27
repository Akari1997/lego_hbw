using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal abstract class r : IDisposable
{
	protected Oci a;

	protected aa b;

	protected v c;

	protected OracleType d;

	protected am e;

	protected HandleRef f;

	protected HandleRef g;

	protected r h;

	internal static IntPtr i;

	public r(OracleType A_0, v A_1)
		: this(A_0, A_1, A_2: true)
	{
	}

	internal r(OracleType A_0, v A_1, bool A_2)
	{
		Utils.CheckArgumentNull(A_0, "oraType");
		Utils.CheckArgumentNull(A_1, "session");
		this.d = A_0;
		c = A_1;
		e = (am)A_1.b(A_0);
		this.b = A_1.h();
		this.a = this.b.j();
		if (A_2)
		{
			this.b.c(this.a.OCIObjectNew(this.b.h(), this.b.k(), A_1.r(), A_0.e, e.e, IntPtr.Zero, 10, 1, out var instance));
			f = new HandleRef(this, instance);
			this.b.c(this.a.OCIObjectGetInd(this.b.h(), this.b.k(), f, out var null_struct));
			this.g = new HandleRef(this, null_struct);
		}
	}

	internal r(HandleRef A_0, IntPtr A_1, OracleType A_2, v A_3, bool A_4, bool A_5)
		: this(A_2, A_3, A_2: false)
	{
		if (A_5)
		{
			f = new HandleRef(this, A_0.Handle);
		}
		else
		{
			f = A_0;
		}
		if (A_1 == IntPtr.Zero)
		{
			this.b.c(this.a.OCIObjectGetInd(this.b.h(), this.b.k(), A_0, out A_1));
		}
		this.g = new HandleRef(A_0.Wrapper, A_1);
		if (A_4)
		{
			set_IsNull(value: true);
		}
		if (A_0.Wrapper is r)
		{
			this.h = (r)A_0.Wrapper;
		}
	}

	protected virtual void i()
	{
		try
		{
			d();
		}
		catch
		{
		}
		finally
		{
			base.Finalize();
		}
	}

	protected virtual void d()
	{
		if (f.Wrapper == this)
		{
			if (!c.a())
			{
				this.b.c(this.a.OCIObjectFree(this.b.h(), this.b.k(), f, 1));
			}
			f = new HandleRef(null, IntPtr.Zero);
		}
	}

	[SpecialName]
	internal bool h()
	{
		return f.Wrapper != this;
	}

	public void p()
	{
		b(A_0: true);
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in p
		this.p();
	}

	protected virtual void b(bool A_0)
	{
		d();
		GC.SuppressFinalize(this);
	}

	[SpecialName]
	internal Oci o()
	{
		return this.a;
	}

	[SpecialName]
	internal aa j()
	{
		return this.b;
	}

	[SpecialName]
	internal v g()
	{
		return c;
	}

	[SpecialName]
	internal IntPtr l()
	{
		return f.Handle;
	}

	[SpecialName]
	internal IntPtr k()
	{
		return this.g.Handle;
	}

	protected internal virtual void a(bool A_0, object A_1)
	{
	}

	internal object a(OracleAttribute A_0, IntPtr A_1)
	{
		switch (A_0.b)
		{
		case 1:
		case 5:
		case 9:
		case 96:
		{
			IntPtr vs = Marshal.ReadIntPtr(A_1);
			IntPtr intPtr = this.a.OCIStringPtr(this.b.h(), vs);
			int num2 = this.a.OCIStringSize(this.b.h(), vs);
			if (A_0.b == 96 || A_0.b == 1)
			{
				if (this.b.a())
				{
					while (num2 > 0 && Marshal.ReadByte(intPtr, num2 - 2) == 32)
					{
						num2 -= 2;
					}
				}
				else
				{
					while (num2 > 0 && Marshal.ReadByte(intPtr, num2 - 1) == 32)
					{
						num2--;
					}
				}
			}
			if (intPtr == IntPtr.Zero)
			{
				return string.Empty;
			}
			if (this.b.a())
			{
				return Marshal.PtrToStringUni(intPtr, num2 / 2);
			}
			return Marshal.PtrToStringAnsi(intPtr, num2);
		}
		case 2:
		case 3:
		case 4:
		case 6:
		case 246:
		{
			byte[] array3 = new byte[A_0.c];
			Marshal.Copy(A_1, array3, 0, array3.Length);
			return new OracleNumber(array3);
		}
		case 100:
			return Devart.Common.e.e(Devart.Common.e.a(Marshal.ReadInt32(A_1, 4)), 0);
		case 101:
			return Devart.Common.e.i(Devart.Common.e.a(Marshal.ReadInt64(A_1, 4)), 0);
		case 12:
		{
			byte[] array = new byte[8];
			Marshal.Copy(A_1, array, 0, 8);
			if (array[0] == 0 && array[1] == 0 && array[2] == 0)
			{
				return DBNull.Value;
			}
			return OracleDate.f(array, 0);
		}
		case 95:
		{
			IntPtr raw = Marshal.ReadIntPtr(A_1);
			byte[] array2 = new byte[this.a.OCIRawSize(this.b.h(), raw)];
			Marshal.Copy(this.a.OCIRawPtr(this.b.h(), raw), array2, 0, array2.Length);
			return array2;
		}
		case 187:
			return new OracleTimeStamp(Marshal.ReadIntPtr(A_1), OracleDbType.TimeStamp, c);
		case 188:
			return new OracleTimeStamp(Marshal.ReadIntPtr(A_1), OracleDbType.TimeStampTZ, c);
		case 232:
			return new OracleTimeStamp(Marshal.ReadIntPtr(A_1), OracleDbType.TimeStampLTZ, c);
		case 189:
			return new OracleIntervalYM(Marshal.ReadIntPtr(A_1), c);
		case 190:
			return new OracleIntervalDS(Marshal.ReadIntPtr(A_1), c);
		case 113:
		case 114:
			return new p(c, Marshal.ReadIntPtr(A_1), 0, 0, A_4: false);
		case 112:
		case 115:
		{
			int num = ((c.h().a() || OracleUtils.OracleClientCompatible) ? 1000 : 0);
			int a_2 = ((A_0.i.k() == OracleDbType.NClob) ? 2 : 0);
			return new p(c, Marshal.ReadIntPtr(A_1), (short)num, a_2, A_4: false);
		}
		case 110:
			return new l(Marshal.ReadIntPtr(A_1), A_0.h, c, A_3: false);
		case 58:
			if (A_0.DbType == OracleDbType.Xml)
			{
				HandleRef a_ = new HandleRef(this, Marshal.ReadIntPtr(A_1));
				return new bj(a_, IntPtr.Zero, c, A_3: false);
			}
			throw new InvalidOperationException();
		default:
			throw new InvalidOperationException();
		}
	}

	internal static object a(object A_0, OracleAttribute A_1)
	{
		if (A_0 is OracleNumber)
		{
			return A_1.g switch
			{
				OracleDbType.Integer => OracleNumber.d((OracleNumber)A_0), 
				OracleDbType.Double => OracleNumber.b((OracleNumber)A_0), 
				_ => ((OracleNumber)A_0).Value, 
			};
		}
		if (A_0 is OracleLob)
		{
			return ((OracleLob)A_0).Value;
		}
		if (A_0 is OracleDate oracleDate)
		{
			return oracleDate.Value;
		}
		if (A_0 is OracleIntervalDS oracleIntervalDS)
		{
			return oracleIntervalDS.Value;
		}
		if (A_0 is OracleIntervalYM oracleIntervalYM)
		{
			return oracleIntervalYM.Value;
		}
		if (A_0 is OracleTimeStamp oracleTimeStamp)
		{
			return oracleTimeStamp.Value;
		}
		return A_0;
	}

	internal void a(object A_0, OracleAttribute A_1, IntPtr A_2)
	{
		switch (A_1.b)
		{
		case 1:
		case 9:
		case 96:
		{
			byte[] bytes;
			int new_size;
			if (this.b.a())
			{
				bytes = Encoding.Unicode.GetBytes((string)A_0);
				new_size = bytes.Length + 2;
			}
			else
			{
				bytes = Encoding.Default.GetBytes((string)A_0);
				new_size = bytes.Length + 1;
			}
			if (bytes.Length > A_1.c * ((!this.b.a()) ? 1 : 2))
			{
				throw new OracleException(21525, "ORA-21525: attribute number or (collection element at index) %s violated its constraints");
			}
			this.b.c(this.a.OCIStringResize(this.b.h(), this.b.k(), new_size, A_2));
			this.b.c(this.a.OCIStringAssignText(this.b.h(), this.b.k(), bytes, bytes.Length, A_2));
			break;
		}
		case 2:
		case 3:
		case 4:
		case 6:
		case 246:
			if (A_0 is OracleNumber || A_0 is decimal || A_0 is string)
			{
				OracleNumber oracleNumber = ((A_0 is OracleNumber) ? ((OracleNumber)A_0) : new OracleNumber(Convert.ToDecimal(A_0)));
				this.b.c(this.a.OCINumberAssign(this.b.k(), oracleNumber.a, A_2));
				break;
			}
			if (A_0 is long || A_0 is int || A_0 is uint || A_0 is short || A_0 is ushort || A_0 is byte || A_0 is sbyte)
			{
				long inum = Convert.ToInt64(A_0);
				this.b.c(this.a.OCINumberFromInt(this.b.k(), ref inum, Marshal.SizeOf(typeof(long)), 2, A_2));
				break;
			}
			if (A_0 is ulong inum2)
			{
				this.b.c(this.a.OCINumberFromInt(this.b.k(), ref inum2, Marshal.SizeOf(typeof(long)), 0, A_2));
				break;
			}
			if (A_0 is double rnum)
			{
				this.b.c(this.a.OCINumberFromReal(this.b.k(), ref rnum, Marshal.SizeOf(typeof(double)), A_2));
				break;
			}
			if (A_0 is float rnum2)
			{
				this.b.c(this.a.OCINumberFromReal(this.b.k(), ref rnum2, Marshal.SizeOf(typeof(float)), A_2));
				break;
			}
			throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
		case 12:
			if (A_0 is DateTime || A_0 is string)
			{
				DateTime dt = ((A_0 is DateTime) ? ((DateTime)A_0) : DateTime.Parse((string)A_0));
				byte[] binData = new OracleDate(dt).BinData;
				Marshal.Copy(binData, 0, A_2, binData.Length);
				break;
			}
			if (A_0 is OracleTimeStamp oracleTimeStamp2)
			{
				byte[] binData2 = oracleTimeStamp2.ToOracleDate().BinData;
				Marshal.Copy(binData2, 0, A_2, binData2.Length);
				break;
			}
			if (A_0 is OracleDate { BinData: var binData3 })
			{
				Marshal.Copy(binData3, 0, A_2, binData3.Length);
				break;
			}
			throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
		case 95:
		{
			if (!(A_0 is byte[]) && !(A_0 is OracleBinary))
			{
				throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
			}
			byte[] array = ((!(A_0 is byte[])) ? ((OracleBinary)A_0).Value : ((byte[])A_0));
			this.b.c(this.a.OCIRawAssignBytes(this.b.h(), this.b.k(), array, array.Length, A_2));
			break;
		}
		case 187:
		case 188:
		case 232:
		{
			OracleTimeStamp oracleTimeStamp;
			if (A_0 is OracleTimeStamp)
			{
				oracleTimeStamp = (OracleTimeStamp)A_0;
			}
			else if (A_0 is DateTime)
			{
				oracleTimeStamp = new OracleTimeStamp((DateTime)A_0);
			}
			else
			{
				if (!(A_0 is OracleDateTime) || !OracleUtils.OracleClientCompatible)
				{
					throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
				}
				oracleTimeStamp = ((OracleDateTime)A_0).a;
			}
			int num = 0;
			num = oracleTimeStamp.TimeStampType switch
			{
				OracleDbType.TimeStamp => 68, 
				OracleDbType.TimeStampTZ => 69, 
				OracleDbType.TimeStampLTZ => 70, 
				_ => throw new InvalidOperationException(), 
			};
			this.b.c(this.a.OCIDescriptorAlloc(this.b.h(), out var descpp, num, 0u, 0u));
			if (oracleTimeStamp.TimeStampType == OracleDbType.TimeStampTZ)
			{
				byte[] bytes2 = g().h().o().GetBytes(oracleTimeStamp.TimeZone);
				this.b.c(this.a.OCIDateTimeConstruct(this.b.h(), this.b.k(), descpp, (short)oracleTimeStamp.Year, (byte)oracleTimeStamp.Month, (byte)oracleTimeStamp.Day, (byte)oracleTimeStamp.Hour, (byte)oracleTimeStamp.Minute, (byte)oracleTimeStamp.Second, (uint)oracleTimeStamp.Nanosecond, bytes2, (uint)bytes2.Length));
			}
			else
			{
				this.b.c(this.a.OCIDateTimeConstruct(this.b.h(), this.b.k(), descpp, (short)oracleTimeStamp.Year, (byte)oracleTimeStamp.Month, (byte)oracleTimeStamp.Day, (byte)oracleTimeStamp.Hour, (byte)oracleTimeStamp.Minute, (byte)oracleTimeStamp.Second, (uint)oracleTimeStamp.Nanosecond, null, 0u));
			}
			Marshal.WriteIntPtr(A_2, descpp);
			break;
		}
		case 190:
			if (A_0 is OracleTimeSpan && OracleUtils.OracleClientCompatible)
			{
				A_0 = ((OracleTimeSpan)A_0).a;
			}
			if (A_0 is TimeSpan timeSpan)
			{
				int fsec = (int)(timeSpan.Ticks % 10000000);
				this.b.c(this.a.OCIIntervalSetDaySecond(this.b.h(), this.b.k(), timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, fsec, A_2));
				break;
			}
			if (A_0 is OracleIntervalDS oracleIntervalDS)
			{
				this.b.c(this.a.OCIIntervalSetDaySecond(this.b.h(), this.b.k(), oracleIntervalDS.Days, oracleIntervalDS.Hours, oracleIntervalDS.Minutes, oracleIntervalDS.Seconds, oracleIntervalDS.Nanoseconds, A_2));
				break;
			}
			throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
		case 189:
			if (A_0 is OracleMonthSpan && OracleUtils.OracleClientCompatible)
			{
				A_0 = ((OracleMonthSpan)A_0).a;
			}
			if (A_0 is OracleIntervalYM oracleIntervalYM)
			{
				this.b.c(this.a.OCIIntervalSetYearMonth(this.b.h(), this.b.k(), oracleIntervalYM.Years, oracleIntervalYM.Months, A_2));
				break;
			}
			throw new ArgumentException(Devart.Common.al.a("CanNotConvert"));
		case 112:
		case 113:
		case 114:
		case 115:
		{
			p p2 = (p)A_0;
			if (j().d() >= 11000000 || !j().a())
			{
				byte[] names = j().o().GetBytes(A_1.Name);
				int[] lengths = new int[1] { names.Length };
				j().c(j().j().OCIObjectSetAttr(this.b.h(), this.b.k(), f, this.g, e.e, ref names, lengths, 1u, 0u, 0u, 0, IntPtr.Zero, p2.q()));
			}
			else
			{
				Marshal.WriteIntPtr(A_2, p2.q());
			}
			break;
		}
		case 100:
			Marshal.WriteInt32(A_2, Devart.Common.e.g(Devart.Common.e.a((float)A_0), 0));
			break;
		case 101:
			Marshal.WriteInt64(A_2, Devart.Common.e.f(Devart.Common.e.a((double)A_0), 0));
			break;
		default:
			throw new InvalidOperationException();
		}
	}

	public void m()
	{
		this.b.c(this.a.OCIObjectMarkUpdate(this.b.h(), this.b.k(), f));
		this.b.c(this.a.OCIObjectFlush(this.b.h(), this.b.k(), f));
	}

	public virtual void q()
	{
		this.b.c(this.a.OCIObjectRefresh(this.b.h(), this.b.k(), f));
	}

	[SpecialName]
	internal virtual HandleRef r()
	{
		return f;
	}

	[SpecialName]
	internal virtual HandleRef n()
	{
		return this.g;
	}

	[SpecialName]
	public OracleType get_ObjectType()
	{
		return this.d;
	}

	[SpecialName]
	public virtual bool get_IsNull()
	{
		if (this.g.Handle == IntPtr.Zero)
		{
			return false;
		}
		return Marshal.ReadInt16(this.g.Handle) != 0;
	}

	[SpecialName]
	public virtual void set_IsNull(bool value)
	{
		if (this.g.Handle != IntPtr.Zero)
		{
			if (value)
			{
				Marshal.WriteInt16(this.g.Handle, -1);
			}
			else
			{
				Marshal.WriteInt16(this.g.Handle, 0);
			}
		}
	}

	static r()
	{
		Devart.Data.Oracle.r.i = GCHandle.Alloc((short)(-1), GCHandleType.Pinned).AddrOfPinnedObject();
	}
}
