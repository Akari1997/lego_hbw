using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using Devart.Common;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(a0))]
public struct OracleDate : IComparable, INullable
{
	private int m_a;

	private int m_b;

	private int m_c;

	private int m_d;

	private int m_e;

	private int m_f;

	private bool g;

	public static readonly OracleDate MaxValue = new OracleDate(9999, 12, 31, 23, 59, 59);

	public static readonly OracleDate MinValue = new OracleDate(-4712, 1, 1, 0, 0, 0);

	public static readonly OracleDate Null = new OracleDate(A_0: true);

	public int Day => this.m_c;

	public int Hour => this.m_d;

	public bool IsNull => g;

	public int Minute => this.m_e;

	public int Month => this.m_b;

	public int Second => this.m_f;

	public bool IsValidDateTime => this.m_a >= 0;

	public DateTime Value
	{
		get
		{
			if (this.m_a == 0 && this.m_b == 0 && this.m_c == 0 && this.m_d == 0 && this.m_e == 0 && this.m_f == 0)
			{
				return default(DateTime);
			}
			return new DateTime(this.m_a, this.m_b, this.m_c, this.m_d, this.m_e, this.m_f);
		}
	}

	public int Year => this.m_a;

	public byte[] BinData
	{
		get
		{
			if (IsNull)
			{
				return null;
			}
			byte[] array = Devart.Common.e.a((short)this.m_a);
			return new byte[7]
			{
				array[0],
				array[1],
				(byte)this.m_b,
				(byte)this.m_c,
				(byte)this.m_d,
				(byte)this.m_e,
				(byte)this.m_f
			};
		}
	}

	internal byte[] NativeBinData
	{
		get
		{
			if (IsNull)
			{
				return null;
			}
			return new byte[7]
			{
				(byte)(this.m_a / 100 + 100),
				(byte)(this.m_a % 100 + 100),
				(byte)this.m_b,
				(byte)this.m_c,
				(byte)(this.m_d + 1),
				(byte)(this.m_e + 1),
				(byte)(this.m_f + 1)
			};
		}
	}

	public OracleDate(DateTime dt)
		: this(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second)
	{
	}

	public OracleDate(int year, int month, int day)
		: this(year, month, day, 0, 0, 0)
	{
	}

	public OracleDate(int year, int month, int day, int hour, int minute, int second)
	{
		if (year < -4712 || year > 9999)
		{
			throw new ArgumentException(Devart.Common.al.a("YearIsOutOfRange"), "year");
		}
		if (month < 1 || month > 12)
		{
			throw new ArgumentException(Devart.Common.al.a("MonthIsOutOfRange"), "month");
		}
		if (day < 1 || day > 31)
		{
			throw new ArgumentException(Devart.Common.al.a("DayIsOutOfRange"), "day");
		}
		if (hour < 0 || hour > 23)
		{
			throw new ArgumentException(Devart.Common.al.a("HourIsOutOfRange"), "hour");
		}
		if (minute < 0 || minute > 59)
		{
			throw new ArgumentException(Devart.Common.al.a("MinuteIsOutOfRange"), "minute");
		}
		if (second < 0 || second > 59)
		{
			throw new ArgumentException(Devart.Common.al.a("SecondIsOutOfRange"), "second");
		}
		this.m_a = year;
		this.m_b = month;
		this.m_c = day;
		this.m_d = hour;
		this.m_e = minute;
		this.m_f = second;
		g = false;
	}

	internal OracleDate(Oci.h A_0)
	{
		this.m_a = A_0.a;
		this.m_b = A_0.b;
		this.m_c = A_0.c;
		this.m_d = A_0.d;
		this.m_e = A_0.e;
		this.m_f = A_0.f;
		g = false;
	}

	private OracleDate(bool A_0)
	{
		g = A_0;
		this.m_a = 0;
		this.m_b = 0;
		this.m_c = 0;
		this.m_d = 0;
		this.m_e = 0;
		this.m_f = 0;
	}

	internal static OracleDate f(byte[] A_0, int A_1)
	{
		short year = Devart.Common.e.h(A_0, A_1);
		A_1 += 2;
		byte month = A_0[A_1++];
		byte day = A_0[A_1++];
		byte hour = A_0[A_1++];
		byte minute = A_0[A_1++];
		byte second = A_0[A_1];
		try
		{
			return new OracleDate(year, month, day, hour, minute, second);
		}
		catch (ArgumentException)
		{
			return new OracleDate(1, 1, 1, 0, 0, 0);
		}
	}

	internal static OracleDate e(byte[] A_0, int A_1)
	{
		short year = (short)((A_0[A_1] - 100) * 100 + A_0[A_1 + 1] - 100);
		A_1 += 2;
		byte month = A_0[A_1++];
		byte day = A_0[A_1++];
		byte hour = (byte)(A_0[A_1++] - 1);
		byte minute = (byte)(A_0[A_1++] - 1);
		byte second = (byte)(A_0[A_1] - 1);
		try
		{
			return new OracleDate(year, month, day, hour, minute, second);
		}
		catch (ArgumentException)
		{
			return new OracleDate(1, 1, 1, 0, 0, 0);
		}
	}

	internal static OracleTimeStamp d(byte[] A_0, int A_1)
	{
		short year = Devart.Common.e.h(A_0, A_1);
		A_1 += 2;
		byte month = A_0[A_1++];
		byte day = A_0[A_1++];
		byte hour = A_0[A_1++];
		byte minute = A_0[A_1++];
		byte second = A_0[A_1];
		try
		{
			return new OracleTimeStamp(year, month, day, hour, minute, second);
		}
		catch (ArgumentException)
		{
			return new OracleTimeStamp(1, 1, 1, 0, 0, 0);
		}
	}

	internal static OracleTimeStamp c(byte[] A_0, int A_1)
	{
		short year = (short)((A_0[A_1] - 100) * 100 + A_0[A_1 + 1] - 100);
		A_1 += 2;
		byte month = A_0[A_1++];
		byte day = A_0[A_1++];
		byte hour = (byte)(A_0[A_1++] + 1);
		byte minute = (byte)(A_0[A_1++] + 1);
		byte second = (byte)(A_0[A_1] + 1);
		try
		{
			return new OracleTimeStamp(year, month, day, hour, minute, second);
		}
		catch (ArgumentException)
		{
			return new OracleTimeStamp(1, 1, 1, 0, 0, 0);
		}
	}

	internal static DateTime b(byte[] A_0, int A_1)
	{
		short year = Devart.Common.e.h(A_0, A_1);
		A_1 += 2;
		byte month = A_0[A_1++];
		byte day = A_0[A_1++];
		byte hour = A_0[A_1++];
		byte minute = A_0[A_1++];
		byte second = A_0[A_1];
		try
		{
			return new DateTime(year, month, day, hour, minute, second);
		}
		catch (ArgumentException)
		{
			return DateTime.MinValue;
		}
	}

	internal static DateTime a(byte[] A_0, int A_1)
	{
		short year = (short)((A_0[A_1] - 100) * 100 + A_0[A_1 + 1] - 100);
		A_1 += 2;
		byte month = A_0[A_1++];
		byte day = A_0[A_1++];
		byte hour = (byte)((sbyte)A_0[A_1++] - 1);
		byte minute = (byte)((sbyte)A_0[A_1++] - 1);
		byte second = (byte)((sbyte)A_0[A_1] - 1);
		try
		{
			return new DateTime(year, month, day, hour, minute, second);
		}
		catch (ArgumentException)
		{
			return DateTime.MinValue;
		}
	}

	public int CompareTo(object obj)
	{
		if (obj == null || obj == DBNull.Value)
		{
			if (!IsNull)
			{
				return 1;
			}
			return 0;
		}
		if ((object)obj.GetType() != typeof(OracleDate))
		{
			throw new ArgumentException();
		}
		OracleDate oracleDate = (OracleDate)obj;
		if (IsNull)
		{
			if (!oracleDate.IsNull)
			{
				return -1;
			}
			return 0;
		}
		if (oracleDate.IsNull)
		{
			return 1;
		}
		if (this.m_a > oracleDate.m_a)
		{
			return 1;
		}
		if (this.m_a < oracleDate.m_a)
		{
			return -1;
		}
		if (this.m_b > oracleDate.m_b)
		{
			return 1;
		}
		if (this.m_b < oracleDate.m_b)
		{
			return -1;
		}
		if (this.m_c > oracleDate.m_c)
		{
			return 1;
		}
		if (this.m_c < oracleDate.m_c)
		{
			return -1;
		}
		if (this.m_d > oracleDate.m_d)
		{
			return 1;
		}
		if (this.m_d < oracleDate.m_d)
		{
			return -1;
		}
		if (this.m_e > oracleDate.m_e)
		{
			return 1;
		}
		if (this.m_e < oracleDate.m_e)
		{
			return -1;
		}
		if (this.m_f > oracleDate.m_f)
		{
			return 1;
		}
		if (this.m_f < oracleDate.m_f)
		{
			return -1;
		}
		return 0;
	}

	public override bool Equals(object value)
	{
		if (value == null || value == DBNull.Value)
		{
			return IsNull;
		}
		if ((object)value.GetType() != typeof(OracleDate))
		{
			return false;
		}
		return CompareTo(value) == 0;
	}

	public static bool Equals(OracleDate value1, OracleDate value2)
	{
		return value1.CompareTo(value2) == 0;
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	public static bool GreaterThan(OracleDate value1, OracleDate value2)
	{
		return value1.CompareTo(value2) == 1;
	}

	public static bool GreaterThanOrEqual(OracleDate value1, OracleDate value2)
	{
		return value1.CompareTo(value2) >= 0;
	}

	public static bool LessThan(OracleDate value1, OracleDate value2)
	{
		return value1.CompareTo(value2) == -1;
	}

	public static bool LessThanOrEqual(OracleDate value1, OracleDate value2)
	{
		return value1.CompareTo(value2) <= 0;
	}

	public static bool NotEquals(OracleDate value1, OracleDate value2)
	{
		return value1.CompareTo(value2) != 0;
	}

	public int GetDaysBetween(OracleDate val)
	{
		if (val.IsNull)
		{
			return 0;
		}
		throw new NotSupportedException();
	}

	public static OracleDate GetSysDate()
	{
		throw new NotSupportedException();
	}

	public static OracleDate Parse(string value)
	{
		return Parse(value, OracleGlobalization.ApplicationGlobalization.DateFormat);
	}

	public static bool TryParse(string value, out OracleDate result)
	{
		try
		{
			result = Parse(value);
			return true;
		}
		catch (Exception)
		{
			result = Null;
			return false;
		}
	}

	public static OracleDate Parse(string value, string format)
	{
		OracleTimeStamp.a(value, format, OracleDbType.TimeStamp, out var A_, out var A_2, out var A_3, out var A_4, out var A_5, out var A_6, out var _, out var _, out var _, out var _, 9);
		return new OracleDate(A_, A_2, A_3, A_4, A_5, A_6);
	}

	public static bool TryParse(string value, string format, out OracleDate result)
	{
		try
		{
			result = Parse(value, format);
			return true;
		}
		catch (Exception)
		{
			result = Null;
			return false;
		}
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return string.Empty;
		}
		return ToString(OracleGlobalization.ApplicationGlobalization.DateFormat);
	}

	public string ToString(string format)
	{
		if (IsNull)
		{
			return string.Empty;
		}
		return OracleTimeStamp.a(format, (short)this.m_a, (byte)this.m_b, (byte)this.m_c, (byte)this.m_d, (byte)this.m_e, (byte)this.m_f, 0, 0, 0, 0, 9);
	}

	public OracleTimeStamp ToOracleTimeStamp()
	{
		if (IsNull)
		{
			return OracleTimeStamp.Null;
		}
		return new OracleTimeStamp(Value);
	}

	public static bool operator ==(OracleDate value1, OracleDate value2)
	{
		return Equals(value1, value2);
	}

	public static bool operator >(OracleDate value1, OracleDate value2)
	{
		return GreaterThan(value1, value2);
	}

	public static bool operator >=(OracleDate value1, OracleDate value2)
	{
		return GreaterThanOrEqual(value1, value2);
	}

	public static bool operator !=(OracleDate value1, OracleDate value2)
	{
		return NotEquals(value1, value2);
	}

	public static bool operator <(OracleDate value1, OracleDate value2)
	{
		return LessThan(value1, value2);
	}

	public static bool operator <=(OracleDate value1, OracleDate value2)
	{
		return LessThanOrEqual(value1, value2);
	}

	public static explicit operator DateTime(OracleDate val)
	{
		return val.Value;
	}

	public static explicit operator OracleDate(DateTime dt)
	{
		return new OracleDate(dt);
	}

	internal IntPtr a()
	{
		return IntPtr.Zero;
	}

	private void a(int A_0)
	{
		throw new NotSupportedException();
	}
}
