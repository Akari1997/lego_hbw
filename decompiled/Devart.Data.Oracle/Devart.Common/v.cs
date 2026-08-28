using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Devart.Common;

internal class v
{
	protected Type a;

	protected readonly DbType b;

	protected readonly DbType c;

	protected readonly Encoding d;

	protected static readonly double[] e;

	protected static readonly double[] f;

	protected static readonly float[] g;

	protected static byte h;

	protected v(DbType A_0, Encoding A_1)
		: this(A_0, A_0, A_1)
	{
	}

	protected v(DbType A_0, DbType A_1, Encoding A_2)
	{
		this.c = A_0;
		this.d = A_2;
		this.b = A_1;
		switch (A_1)
		{
		case DbType.AnsiString:
		case DbType.String:
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			this.a = typeof(string);
			break;
		case DbType.Binary:
			this.a = typeof(byte[]);
			break;
		case DbType.Boolean:
			this.a = typeof(bool);
			break;
		case DbType.Byte:
			this.a = typeof(byte);
			break;
		case DbType.Date:
		case DbType.DateTime:
			this.a = typeof(DateTime);
			break;
		case DbType.Currency:
		case DbType.Decimal:
		case DbType.UInt64:
		case DbType.VarNumeric:
			this.a = typeof(decimal);
			break;
		case DbType.Double:
			this.a = typeof(double);
			break;
		case DbType.Guid:
			this.a = typeof(Guid);
			break;
		case DbType.Int16:
		case DbType.SByte:
			this.a = typeof(short);
			break;
		case DbType.Int32:
		case DbType.UInt16:
			this.a = typeof(int);
			break;
		case DbType.Int64:
		case DbType.UInt32:
			this.a = typeof(long);
			break;
		case DbType.Object:
			this.a = typeof(object);
			break;
		case DbType.Single:
			this.a = typeof(float);
			break;
		case DbType.Time:
			this.a = typeof(TimeSpan);
			break;
		}
	}

	static v()
	{
		v.h = 46;
		byte[] a_ = new byte[256]
		{
			0, 0, 0, 0, 0, 0, 240, 63, 0, 0,
			0, 0, 0, 0, 36, 64, 0, 0, 0, 0,
			0, 0, 89, 64, 0, 0, 0, 0, 0, 64,
			143, 64, 0, 0, 0, 0, 0, 136, 195, 64,
			0, 0, 0, 0, 0, 106, 248, 64, 0, 0,
			0, 0, 128, 132, 46, 65, 0, 0, 0, 0,
			208, 18, 99, 65, 0, 0, 0, 0, 132, 215,
			151, 65, 0, 0, 0, 0, 101, 205, 205, 65,
			0, 0, 0, 32, 95, 160, 2, 66, 0, 0,
			0, 232, 118, 72, 55, 66, 0, 0, 0, 162,
			148, 26, 109, 66, 0, 0, 64, 229, 156, 48,
			162, 66, 0, 0, 144, 30, 196, 188, 214, 66,
			0, 0, 52, 38, 245, 107, 12, 67, 0, 128,
			224, 55, 121, 195, 65, 67, 0, 160, 216, 133,
			87, 52, 118, 67, 0, 200, 78, 103, 109, 193,
			171, 67, 0, 61, 145, 96, 228, 88, 225, 67,
			64, 140, 181, 120, 29, 175, 21, 68, 80, 239,
			226, 214, 228, 26, 75, 68, 146, 213, 77, 6,
			207, 240, 128, 68, 246, 74, 225, 199, 2, 45,
			181, 68, 180, 157, 217, 121, 67, 120, 234, 68,
			145, 2, 40, 44, 42, 139, 32, 69, 53, 3,
			50, 183, 244, 173, 84, 69, 2, 132, 254, 228,
			113, 217, 137, 69, 129, 18, 31, 47, 231, 39,
			192, 69, 33, 215, 230, 250, 224, 49, 244, 69,
			234, 140, 160, 57, 89, 62, 41, 70, 36, 176,
			8, 136, 239, 141, 95, 70
		};
		byte[] a_2 = new byte[80]
		{
			0, 0, 0, 0, 0, 0, 240, 63, 23, 110,
			5, 181, 181, 184, 147, 70, 245, 249, 63, 233,
			3, 79, 56, 77, 99, 179, 216, 98, 117, 246,
			221, 83, 50, 29, 48, 249, 72, 119, 130, 90,
			195, 252, 111, 37, 212, 194, 38, 97, 235, 36,
			167, 241, 30, 14, 204, 103, 153, 103, 252, 223,
			82, 74, 113, 110, 60, 191, 115, 127, 221, 79,
			21, 117, 70, 141, 43, 131, 223, 68, 186, 123
		};
		byte[] a_3 = new byte[156]
		{
			0, 0, 128, 63, 0, 0, 32, 65, 0, 0,
			200, 66, 0, 0, 122, 68, 0, 64, 28, 70,
			0, 80, 195, 71, 0, 36, 116, 73, 128, 150,
			24, 75, 32, 188, 190, 76, 40, 107, 110, 78,
			249, 2, 21, 80, 183, 67, 186, 81, 165, 212,
			104, 83, 231, 132, 17, 85, 33, 230, 181, 86,
			169, 95, 99, 88, 202, 27, 14, 90, 188, 162,
			177, 91, 107, 11, 94, 93, 35, 199, 10, 95,
			236, 120, 173, 96, 39, 215, 88, 98, 120, 134,
			7, 100, 22, 104, 169, 101, 28, 194, 83, 103,
			81, 89, 4, 105, 166, 111, 165, 106, 143, 203,
			78, 108, 57, 63, 1, 110, 8, 143, 161, 111,
			202, 242, 73, 113, 124, 111, 252, 114, 174, 197,
			157, 116, 25, 55, 69, 118, 223, 132, 246, 119,
			12, 19, 154, 121, 206, 151, 64, 123, 194, 189,
			240, 124, 153, 118, 150, 126
		};
		v.e = new double[32];
		v.f = new double[10];
		v.g = new float[39];
		int num = 0;
		for (int num2 = 0; num2 < 32; num2++)
		{
			v.e[num2] = Devart.Common.e.i(a_, num);
			num += 8;
		}
		num = 0;
		for (int num3 = 0; num3 < 10; num3++)
		{
			v.f[num3] = Devart.Common.e.i(a_2, num);
			num += 8;
		}
		num = 0;
		for (int num4 = 0; num4 < 39; num4++)
		{
			v.g[num4] = Devart.Common.e.e(a_3, num);
			num += 4;
		}
	}

	public virtual bool m(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
			return bool.Parse(aq(A_0, A_1, A_2));
		case DbType.String:
		case DbType.AnsiStringFixedLength:
			return an(A_0, A_1, A_2);
		case DbType.StringFixedLength:
			return bool.Parse(b(A_0, A_1, A_2, Encoding.Unicode));
		case DbType.Byte:
			return ae(A_0, A_1, A_2) != 0;
		case DbType.SByte:
			return (sbyte)ae(A_0, A_1, A_2) != 0;
		case DbType.Int16:
			return ad(A_0, A_1, A_2) != 0;
		case DbType.UInt16:
			return av(A_0, A_1, A_2) != 0;
		case DbType.Int32:
			return aj(A_0, A_1, A_2) != 0;
		case DbType.UInt32:
			return @as(A_0, A_1, A_2) != 0;
		case DbType.Int64:
			return al(A_0, A_1, A_2) != 0;
		case DbType.UInt64:
			return ah(A_0, A_1, A_2) != 0;
		case DbType.Single:
			return ag(A_0, A_1, A_2) != 0f;
		case DbType.Double:
			return ac(A_0, A_1, A_2) != 0.0;
		case DbType.Binary:
			return ak(A_0, A_1, A_2)[0] != 0;
		case DbType.Decimal:
			return af(A_0, A_1, A_2) != 0m;
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(bool)));
		}
	}

	public virtual byte i(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.String:
			return byte.Parse(aq(A_0, A_1, A_2));
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return byte.Parse(ab(A_0, A_1, A_2));
		case DbType.Byte:
		case DbType.Int16:
		case DbType.Int32:
		case DbType.Int64:
		case DbType.SByte:
		case DbType.UInt16:
		case DbType.UInt32:
		case DbType.UInt64:
			return ae(A_0, A_1, A_2);
		case DbType.Binary:
		{
			byte[] array = ak(A_0, A_1, A_2);
			return ae(array, 0, array.Length);
		}
		case DbType.Single:
			return (byte)ag(A_0, A_1, A_2);
		case DbType.Double:
			return (byte)ac(A_0, A_1, A_2);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(byte)));
		}
	}

	public virtual char n(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.Byte:
		case DbType.SByte:
		case DbType.AnsiStringFixedLength:
			return (char)A_0[A_1];
		case DbType.Int16:
		case DbType.Int32:
		case DbType.Int64:
		case DbType.String:
		case DbType.UInt16:
		case DbType.UInt32:
		case DbType.UInt64:
		case DbType.StringFixedLength:
			return (char)ad(A_0, A_1, A_2);
		case DbType.Single:
			return (char)ag(A_0, A_1, A_2);
		case DbType.Double:
			return (char)ac(A_0, A_1, A_2);
		case DbType.Binary:
			return ao(A_0, A_1, A_2);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(char)));
		}
	}

	public virtual DateTime e(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.String:
			return DateTime.Parse(aq(A_0, A_1, A_2));
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return DateTime.Parse(ab(A_0, A_1, A_2));
		case DbType.Date:
		case DbType.DateTime:
			return ar(A_0, A_1, A_2);
		case DbType.Time:
			return new DateTime(at(A_0, A_1, A_2).Ticks);
		case DbType.Byte:
			return new DateTime(ae(A_0, A_1, A_2));
		case DbType.SByte:
			return new DateTime((sbyte)ae(A_0, A_1, A_2));
		case DbType.Int16:
			return new DateTime(ad(A_0, A_1, A_2));
		case DbType.UInt16:
			return new DateTime(av(A_0, A_1, A_2));
		case DbType.Int32:
			return new DateTime(aj(A_0, A_1, A_2));
		case DbType.UInt32:
			return new DateTime(@as(A_0, A_1, A_2));
		case DbType.Int64:
			return new DateTime(al(A_0, A_1, A_2));
		case DbType.UInt64:
			return new DateTime((long)ah(A_0, A_1, A_2));
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(DateTime)));
		}
	}

	public virtual TimeSpan f(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.String:
			return TimeSpan.Parse(aq(A_0, A_1, A_2));
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return TimeSpan.Parse(ab(A_0, A_1, A_2));
		case DbType.Byte:
			return new TimeSpan(ae(A_0, A_1, A_2));
		case DbType.SByte:
			return new TimeSpan((sbyte)ae(A_0, A_1, A_2));
		case DbType.Int16:
			return new TimeSpan(ad(A_0, A_1, A_2));
		case DbType.UInt16:
			return new TimeSpan(av(A_0, A_1, A_2));
		case DbType.Int32:
			return new TimeSpan(aj(A_0, A_1, A_2));
		case DbType.UInt32:
			return new TimeSpan(@as(A_0, A_1, A_2));
		case DbType.Int64:
			return new TimeSpan(al(A_0, A_1, A_2));
		case DbType.UInt64:
			return new TimeSpan((long)ah(A_0, A_1, A_2));
		case DbType.Double:
			return TimeSpan.FromDays(ac(A_0, A_1, A_2));
		case DbType.Single:
			return TimeSpan.FromDays(ag(A_0, A_1, A_2));
		case DbType.Time:
			return at(A_0, A_1, A_2);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(TimeSpan)));
		}
	}

	public virtual decimal h(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.Decimal:
			return af(A_0, A_1, A_2);
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
			return aa(A_0, A_1, A_2);
		case DbType.String:
			return decimal.Parse(aq(A_0, A_1, A_2));
		case DbType.StringFixedLength:
			return decimal.Parse(ab(A_0, A_1, A_2));
		case DbType.Byte:
			return ae(A_0, A_1, A_2);
		case DbType.SByte:
			return (sbyte)ae(A_0, A_1, A_2);
		case DbType.Int16:
			return ad(A_0, A_1, A_2);
		case DbType.UInt16:
			return av(A_0, A_1, A_2);
		case DbType.Int32:
			return aj(A_0, A_1, A_2);
		case DbType.UInt32:
			return @as(A_0, A_1, A_2);
		case DbType.Int64:
			return al(A_0, A_1, A_2);
		case DbType.UInt64:
			return ah(A_0, A_1, A_2);
		case DbType.Single:
			return (decimal)ag(A_0, A_1, A_2);
		case DbType.Double:
			return (decimal)ac(A_0, A_1, A_2);
		case DbType.Binary:
		{
			byte[] array = ak(A_0, A_1, A_2);
			float num = array.Length / 4;
			if ((double)(num - (float)(int)num) > 0.0)
			{
				num++;
			}
			int[] array2 = new int[(int)num];
			Buffer.BlockCopy(array, 0, array2, 0, array.Length);
			return new decimal(array2);
		}
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(decimal)));
		}
	}

	public virtual double d(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
			return w(A_0, A_1, A_2);
		case DbType.String:
		case DbType.StringFixedLength:
			return double.Parse(aq(A_0, A_1, A_2));
		case DbType.Byte:
			return (int)ae(A_0, A_1, A_2);
		case DbType.SByte:
			return (sbyte)ae(A_0, A_1, A_2);
		case DbType.Int16:
			return ad(A_0, A_1, A_2);
		case DbType.UInt16:
			return (int)av(A_0, A_1, A_2);
		case DbType.Int32:
			return aj(A_0, A_1, A_2);
		case DbType.UInt32:
			return @as(A_0, A_1, A_2);
		case DbType.Int64:
			return al(A_0, A_1, A_2);
		case DbType.UInt64:
			return ah(A_0, A_1, A_2);
		case DbType.Single:
			return ag(A_0, A_1, A_2);
		case DbType.Double:
			return ac(A_0, A_1, A_2);
		case DbType.Binary:
			return Devart.Common.e.i(ak(A_0, A_1, A_2), 0);
		case DbType.Decimal:
			return (double)af(A_0, A_1, A_2);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(double)));
		}
	}

	public virtual float c(byte[] A_0, int A_1, int A_2)
	{
		return this.c switch
		{
			DbType.AnsiString => x(A_0, A_1, A_2), 
			DbType.String => float.Parse(aq(A_0, A_1, A_2)), 
			DbType.AnsiStringFixedLength => x(A_0, A_1, A_2), 
			DbType.StringFixedLength => float.Parse(ab(A_0, A_1, A_2)), 
			DbType.Byte => (int)ae(A_0, A_1, A_2), 
			DbType.SByte => (sbyte)ae(A_0, A_1, A_2), 
			DbType.Int16 => ad(A_0, A_1, A_2), 
			DbType.UInt16 => (int)av(A_0, A_1, A_2), 
			DbType.Int32 => aj(A_0, A_1, A_2), 
			DbType.UInt32 => @as(A_0, A_1, A_2), 
			DbType.Int64 => al(A_0, A_1, A_2), 
			DbType.UInt64 => ah(A_0, A_1, A_2), 
			DbType.Single => ag(A_0, A_1, A_2), 
			DbType.Binary => Devart.Common.e.e(ak(A_0, A_1, A_2), 0), 
			DbType.Double => (float)ac(A_0, A_1, A_2), 
			_ => throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(float))), 
		};
	}

	public virtual short b(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
			return (short)ap(A_0, A_1, A_2);
		case DbType.String:
			return short.Parse(aq(A_0, A_1, A_2));
		case DbType.StringFixedLength:
			return short.Parse(ab(A_0, A_1, A_2));
		case DbType.Byte:
			return ae(A_0, A_1, A_2);
		case DbType.SByte:
			return (sbyte)ae(A_0, A_1, A_2);
		case DbType.Int16:
			return ad(A_0, A_1, A_2);
		case DbType.Int32:
			return (short)aj(A_0, A_1, A_2);
		case DbType.Int64:
			return (short)al(A_0, A_1, A_2);
		case DbType.UInt16:
			return (short)av(A_0, A_1, A_2);
		case DbType.UInt32:
			return (short)@as(A_0, A_1, A_2);
		case DbType.UInt64:
			return (short)ah(A_0, A_1, A_2);
		case DbType.Single:
			return (short)ag(A_0, A_1, A_2);
		case DbType.Double:
			return (short)ac(A_0, A_1, A_2);
		case DbType.Binary:
			return Devart.Common.e.h(ak(A_0, A_1, A_2), 0);
		case DbType.Decimal:
			return (short)af(A_0, A_1, A_2);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(short)));
		}
	}

	public virtual int a(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
			return ap(A_0, A_1, A_2);
		case DbType.String:
			return Utils.ParseIntWith0(aq(A_0, A_1, A_2));
		case DbType.StringFixedLength:
			return Utils.ParseIntWith0(ab(A_0, A_1, A_2));
		case DbType.Byte:
			return ae(A_0, A_1, A_2);
		case DbType.SByte:
			return (sbyte)ae(A_0, A_1, A_2);
		case DbType.Int16:
			return ad(A_0, A_1, A_2);
		case DbType.UInt16:
			return av(A_0, A_1, A_2);
		case DbType.Int32:
			return aj(A_0, A_1, A_2);
		case DbType.Int64:
			return (int)al(A_0, A_1, A_2);
		case DbType.UInt32:
			return (int)@as(A_0, A_1, A_2);
		case DbType.UInt64:
			return (int)ah(A_0, A_1, A_2);
		case DbType.Single:
			return (int)ag(A_0, A_1, A_2);
		case DbType.Double:
			return (int)ac(A_0, A_1, A_2);
		case DbType.Binary:
			return Devart.Common.e.g(ak(A_0, A_1, A_2), 0);
		case DbType.Decimal:
			return (int)af(A_0, A_1, A_2);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(int)));
		}
	}

	public virtual long g(byte[] A_0, int A_1, int A_2)
	{
		return this.c switch
		{
			DbType.AnsiString => z(A_0, A_1, A_2), 
			DbType.String => long.Parse(aq(A_0, A_1, A_2)), 
			DbType.AnsiStringFixedLength => z(A_0, A_1, A_2), 
			DbType.StringFixedLength => long.Parse(ab(A_0, A_1, A_2)), 
			DbType.Byte => ae(A_0, A_1, A_2), 
			DbType.SByte => (sbyte)ae(A_0, A_1, A_2), 
			DbType.Int16 => ad(A_0, A_1, A_2), 
			DbType.UInt16 => av(A_0, A_1, A_2), 
			DbType.Int32 => aj(A_0, A_1, A_2), 
			DbType.UInt32 => @as(A_0, A_1, A_2), 
			DbType.Int64 => al(A_0, A_1, A_2), 
			DbType.UInt64 => (long)ah(A_0, A_1, A_2), 
			DbType.Single => (long)ag(A_0, A_1, A_2), 
			DbType.Double => (long)ac(A_0, A_1, A_2), 
			DbType.Binary => Devart.Common.e.f(ak(A_0, A_1, A_2), 0), 
			DbType.Decimal => (long)af(A_0, A_1, A_2), 
			_ => throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(long))), 
		};
	}

	public virtual string o(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.String:
			return aq(A_0, A_1, A_2);
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return ab(A_0, A_1, A_2);
		case DbType.Byte:
			return ae(A_0, A_1, A_2).ToString();
		case DbType.SByte:
			return ((sbyte)ae(A_0, A_1, A_2)).ToString();
		case DbType.Int16:
			return ad(A_0, A_1, A_2).ToString();
		case DbType.UInt16:
			return av(A_0, A_1, A_2).ToString();
		case DbType.Int32:
			return aj(A_0, A_1, A_2).ToString();
		case DbType.UInt32:
			return @as(A_0, A_1, A_2).ToString();
		case DbType.Int64:
			return al(A_0, A_1, A_2).ToString();
		case DbType.UInt64:
			return ah(A_0, A_1, A_2).ToString();
		case DbType.Single:
			return ag(A_0, A_1, A_2).ToString();
		case DbType.Double:
			return ac(A_0, A_1, A_2).ToString();
		case DbType.Boolean:
			return an(A_0, A_1, A_2).ToString();
		case DbType.Decimal:
			return af(A_0, A_1, A_2).ToString();
		case DbType.Binary:
		{
			byte[] array = ak(A_0, A_1, A_2);
			return this.d.GetString(array, 0, array.Length);
		}
		case DbType.Guid:
			return q(A_0, A_1, A_2).ToString();
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(string)));
		}
	}

	public virtual Guid q(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.Guid:
			return au(A_0, A_1, A_2);
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return new Guid(ab(A_0, A_1, A_2));
		case DbType.Binary:
		{
			byte[] array = ak(A_0, A_1, A_2);
			if (A_2 < 16)
			{
				return Guid.Empty;
			}
			return new Guid(Devart.Common.e.g(array, 0), Devart.Common.e.h(array, 4), Devart.Common.e.h(array, 6), array[8], array[9], array[10], array[11], array[12], array[13], array[14], array[15]);
		}
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(Guid)));
		}
	}

	public virtual byte[] y(byte[] A_0, int A_1, int A_2)
	{
		switch (this.c)
		{
		case DbType.AnsiString:
		case DbType.Binary:
		case DbType.String:
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return ak(A_0, A_1, A_2);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(byte[])));
		}
	}

	public virtual object p(byte[] A_0, int A_1, int A_2)
	{
		switch (this.b)
		{
		case DbType.Int32:
		case DbType.UInt16:
			return a(A_0, A_1, A_2);
		case DbType.Int64:
		case DbType.UInt32:
			return g(A_0, A_1, A_2);
		case DbType.AnsiString:
		case DbType.String:
			return aq(A_0, A_1, A_2);
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return ab(A_0, A_1, A_2);
		case DbType.Date:
		case DbType.DateTime:
			return e(A_0, A_1, A_2);
		case DbType.Single:
			return c(A_0, A_1, A_2);
		case DbType.Double:
			return d(A_0, A_1, A_2);
		case DbType.Byte:
			return i(A_0, A_1, A_2);
		case DbType.Int16:
		case DbType.SByte:
			return b(A_0, A_1, A_2);
		case DbType.Decimal:
		case DbType.UInt64:
			return h(A_0, A_1, A_2);
		case DbType.Binary:
			return y(A_0, A_1, A_2);
		case DbType.Boolean:
			return m(A_0, A_1, A_2);
		case DbType.Time:
			return f(A_0, A_1, A_2);
		case DbType.Guid:
			return au(A_0, A_1, A_2);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(object)));
		}
	}

	protected virtual TimeSpan at(byte[] A_0, int A_1, int A_2)
	{
		return TimeSpan.Parse(aq(A_0, A_1, A_2));
	}

	protected virtual string aq(byte[] A_0, int A_1, int A_2)
	{
		return a(A_0, A_1, A_2, this.d);
	}

	internal static string a(byte[] A_0, int A_1, int A_2, Encoding A_3)
	{
		if (A_2 < 0)
		{
			int num = A_1;
			A_2 = A_0.Length;
			if (A_3 == Encoding.Unicode)
			{
				for (; num + 1 < A_2 && (A_0[num] != 0 || A_0[num + 1] != 0); num += 2)
				{
				}
			}
			else
			{
				for (; num < A_2 && A_0[num] != 0; num++)
				{
				}
			}
			A_2 = num - A_1;
		}
		return new string(A_3.GetChars(A_0, A_1, A_2));
	}

	protected virtual string ab(byte[] A_0, int A_1, int A_2)
	{
		int num = A_1;
		A_2 += A_1;
		if (this.d == Encoding.Unicode)
		{
			for (; num + 1 < A_2 && (A_0[num] != 0 || A_0[num + 1] != 0); num += 2)
			{
			}
			while (num - 1 > A_1 && A_0[num - 2] <= 32 && A_0[num - 1] == 0)
			{
				num -= 2;
			}
		}
		else
		{
			for (; num < A_2 && A_0[num] != 0; num++)
			{
			}
			while (num > A_1 && A_0[num - 1] <= 32)
			{
				num--;
			}
		}
		return new string(this.d.GetChars(A_0, A_1, num - A_1));
	}

	protected virtual byte[] ak(byte[] A_0, int A_1, int A_2)
	{
		if (A_2 < 0)
		{
			int num = A_1;
			A_2 = A_0.Length;
			if (this.d == Encoding.Unicode)
			{
				for (; num + 1 < A_2 && (A_0[num] != 0 || A_0[num + 1] != 0); num += 2)
				{
				}
			}
			else
			{
				for (; num < A_2 && A_0[num] != 0; num++)
				{
				}
			}
			A_2 = num - A_1;
		}
		byte[] array = new byte[A_2];
		Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
		return array;
	}

	protected virtual DateTime ar(byte[] A_0, int A_1, int A_2)
	{
		return DateTime.Parse(aq(A_0, A_1, A_2));
	}

	protected virtual int ai(byte[] A_0, int A_1, int A_2)
	{
		return aj(A_0, A_1, A_2) & 0xFFFFFF;
	}

	public virtual int ap(byte[] A_0, int A_1, int A_2)
	{
		return (int)z(A_0, A_1, A_2);
	}

	public virtual long z(byte[] A_0, int A_1, int A_2)
	{
		int num = A_1 + A_2;
		if (A_0[num - 1] == 32)
		{
			while (A_0[num - 1] == 32)
			{
				num--;
			}
		}
		byte b2 = A_0[A_1];
		bool flag;
		if (b2 == 45)
		{
			flag = true;
			A_1++;
		}
		else
		{
			flag = false;
			if (b2 == 43)
			{
				A_1++;
			}
		}
		long num2 = 0L;
		for (int num3 = A_1; num3 < num; num3++)
		{
			b2 = A_0[num3];
			if (b2 == 0 || b2 == v.h)
			{
				break;
			}
			if (b2 < 48 || b2 > 57)
			{
				throw new FormatException("IncorrectFormat");
			}
			num2 = num2 * 10 + (b2 - 48);
		}
		if (!flag)
		{
			return num2;
		}
		return -num2;
	}

	public virtual ulong am(byte[] A_0, int A_1, int A_2)
	{
		int num = A_1 + A_2;
		ulong num2 = 0uL;
		if (A_0[num - 1] == 32)
		{
			while (A_0[num - 1] == 32)
			{
				num--;
			}
		}
		byte b2 = A_0[A_1];
		bool flag;
		if (b2 == 45)
		{
			flag = true;
			A_1++;
		}
		else
		{
			flag = false;
			if (b2 == 43)
			{
				A_1++;
			}
		}
		for (int num3 = A_1; num3 < num; num3++)
		{
			b2 = A_0[num3];
			if (b2 == 0 || b2 == v.h)
			{
				break;
			}
			if (b2 < 48 || b2 > 57)
			{
				throw new FormatException("IncorrectFormat");
			}
			num2 = num2 * 10 + (byte)(b2 - 48);
		}
		if (flag)
		{
			long num4 = (long)(0L - num2);
			num2 = (ulong)num4;
		}
		return num2;
	}

	public virtual decimal aa(byte[] A_0, int A_1, int A_2)
	{
		int num = A_1;
		int num2 = A_2 + num;
		while (A_0[num2 - 1] == 32)
		{
			num2--;
		}
		bool isNegative;
		if (A_0[num] == 45)
		{
			isNegative = true;
			num++;
		}
		else
		{
			isNegative = false;
			if (A_0[num] == 43)
			{
				num++;
			}
		}
		int num3 = -1;
		bool flag = false;
		for (int num4 = num; num4 < num2; num4++)
		{
			if (A_0[num4] > 48 && A_0[num4] < 58 && !flag)
			{
				num = num4;
				flag = true;
			}
			if (A_0[num4] == v.h || A_0[num4] == 44)
			{
				num3 = num4;
				if (!flag)
				{
					num = num3 - 1;
				}
				break;
			}
		}
		if (num3 != -1)
		{
			for (int num5 = num2 - 1; num5 > num; num5--)
			{
				if (A_0[num5] > 48 && A_0[num5] < 58)
				{
					num2 = num5 + 1;
					break;
				}
				if (A_0[num5] == v.h || A_0[num5] == 44)
				{
					num2 = num5;
					num3 = -1;
					break;
				}
			}
		}
		if (num2 - num > 27)
		{
			int capacity = num2 - A_1;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			for (int num6 = A_1; num6 < num2; num6++)
			{
				stringBuilder.Append((char)A_0[num6]);
			}
			stringBuilder.Replace(',', '.');
			return decimal.Parse(stringBuilder.ToString(), CultureInfo.InvariantCulture);
		}
		int num7 = num2 - 18;
		if (num7 <= num)
		{
			num7 = num;
		}
		else if (num3 >= num7)
		{
			num7--;
		}
		int num8 = num2 - 9;
		if (num8 <= num)
		{
			num8 = num;
		}
		else if (num3 >= num8)
		{
			num8--;
		}
		uint num9 = 0u;
		for (int num10 = num; num10 < num7; num10++)
		{
			if (num10 != num3)
			{
				if (A_0[num10] < 48 || A_0[num10] > 57)
				{
					throw new FormatException("IncorrectFormat");
				}
				num9 = num9 * 10 + A_0[num10] - 48;
			}
		}
		uint num11 = 0u;
		for (int num12 = num7; num12 < num8; num12++)
		{
			if (num12 != num3)
			{
				if (A_0[num12] < 48 || A_0[num12] > 57)
				{
					throw new FormatException("IncorrectFormat");
				}
				num11 = num11 * 10 + A_0[num12] - 48;
			}
		}
		uint num13 = 0u;
		for (int num14 = num8; num14 < num2; num14++)
		{
			if (num14 != num3)
			{
				if (A_0[num14] < 48 || A_0[num14] > 57)
				{
					throw new FormatException("IncorrectFormat");
				}
				num13 = num13 * 10 + A_0[num14] - 48;
			}
		}
		ulong num15 = (ulong)num9 * 1000000000uL;
		uint num16 = (uint)num15;
		num15 = (num15 >> 32) * 1000000000;
		uint num17 = (uint)num15;
		int num18 = (int)(num15 >> 32);
		num15 = (ulong)(((long)num11 + (long)num16) * 1000000000 + num13);
		int lo = (int)num15;
		num15 = (num15 >> 32) + num17;
		num18 += (int)(num15 >> 32);
		int mid = (int)num15;
		num3 = ((num3 > 0) ? (num2 - num3 - 1) : 0);
		return new decimal(lo, mid, num18, isNegative, (byte)num3);
	}

	private static double a(int A_0)
	{
		if (A_0 < 32)
		{
			return v.e[A_0];
		}
		return v.e[A_0 & 0x1F] * v.f[A_0 >> 5];
	}

	public virtual void a(byte[] A_0, int A_1, object A_2)
	{
		switch (this.c)
		{
		case DbType.Int16:
		{
			short num2 = Convert.ToInt16(A_2);
			A_0[A_1++] = (byte)num2;
			A_0[A_1] = (byte)(num2 >> 8);
			break;
		}
		case DbType.Int32:
		{
			int num = Convert.ToInt32(A_2);
			A_0[A_1++] = (byte)num;
			A_0[A_1++] = (byte)(num >> 8);
			A_0[A_1++] = (byte)(num >> 16);
			A_0[A_1] = (byte)(num >> 24);
			break;
		}
		case DbType.Int64:
		{
			long[] src3 = new long[1] { Convert.ToInt64(A_2) };
			Buffer.BlockCopy(src3, 0, A_0, A_1, 8);
			break;
		}
		case DbType.Single:
		{
			float[] src2 = new float[1] { Convert.ToSingle(A_2) };
			Buffer.BlockCopy(src2, 0, A_0, A_1, 4);
			break;
		}
		case DbType.Double:
		{
			double[] src = new double[1] { Convert.ToDouble(A_2) };
			Buffer.BlockCopy(src, 0, A_0, A_1, 8);
			break;
		}
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
			if (A_2 != null)
			{
				string text2 = A_2.ToString();
				int bytes2 = this.d.GetBytes(text2, 0, text2.Length, A_0, A_1);
				A_0[A_1 + bytes2] = 0;
			}
			else
			{
				A_0[A_1] = 0;
			}
			break;
		case DbType.String:
		case DbType.StringFixedLength:
			if (A_2 != null)
			{
				string text = A_2.ToString();
				int bytes = Encoding.Unicode.GetBytes(text, 0, text.Length, A_0, A_1);
				A_0[A_1 + bytes] = 0;
			}
			else
			{
				A_0[A_1] = 0;
			}
			break;
		case DbType.Binary:
		{
			byte[] array = (byte[])A_2;
			if (array != null)
			{
				Buffer.BlockCopy(array, 0, A_0, A_1, array.Length);
			}
			break;
		}
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), typeof(object), c()));
		}
	}

	protected virtual byte ae(byte[] A_0, int A_1, int A_2)
	{
		return A_0[A_1];
	}

	protected virtual double ac(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.i(A_0, A_1);
	}

	protected virtual float ag(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.e(A_0, A_1);
	}

	protected virtual ulong ah(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.a(A_0, A_1);
	}

	protected virtual long al(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.f(A_0, A_1);
	}

	protected virtual uint @as(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.b(A_0, A_1);
	}

	protected virtual int aj(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.g(A_0, A_1);
	}

	protected virtual ushort av(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.c(A_0, A_1);
	}

	protected virtual short ad(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.h(A_0, A_1);
	}

	protected virtual bool an(byte[] A_0, int A_1, int A_2)
	{
		return bool.Parse(aq(A_0, A_1, A_2));
	}

	protected virtual char ao(byte[] A_0, int A_1, int A_2)
	{
		return Devart.Common.e.j(A_0, A_1);
	}

	protected virtual string b(byte[] A_0, int A_1, int A_2, Encoding A_3)
	{
		char[] chars = A_3.GetChars(A_0, A_1, A_2);
		int num = chars.Length;
		while (num > 0 && chars[num - 1].CompareTo(' ') <= 0)
		{
			num--;
		}
		return new string(chars, 0, num);
	}

	protected virtual double w(byte[] A_0, int A_1, int A_2)
	{
		string strA = this.d.GetString(A_0, A_1, A_2);
		try
		{
			return double.Parse(strA, CultureInfo.InvariantCulture);
		}
		catch (OverflowException ex)
		{
			try
			{
				if (string.Compare(strA, double.MaxValue.ToString(CultureInfo.InvariantCulture), StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					return double.MaxValue;
				}
				if (string.Compare(strA, double.MinValue.ToString(CultureInfo.InvariantCulture), StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					return double.MinValue;
				}
				throw ex;
			}
			catch
			{
				throw ex;
			}
		}
	}

	public virtual float x(byte[] A_0, int A_1, int A_2)
	{
		return float.Parse(this.d.GetString(A_0, A_1, A_2), CultureInfo.InvariantCulture);
	}

	protected virtual decimal af(byte[] A_0, int A_1, int A_2)
	{
		return aa(A_0, A_1, A_2);
	}

	protected virtual Guid au(byte[] A_0, int A_1, int A_2)
	{
		return new Guid(aq(A_0, A_1, A_2));
	}

	[SpecialName]
	protected virtual Type c()
	{
		return this.a;
	}

	[SpecialName]
	public virtual string a()
	{
		return this.b.ToString();
	}

	[SpecialName]
	public Type o()
	{
		return this.a;
	}

	[SpecialName]
	public DbType n()
	{
		return this.b;
	}

	[SpecialName]
	public Encoding m()
	{
		return this.d;
	}
}
