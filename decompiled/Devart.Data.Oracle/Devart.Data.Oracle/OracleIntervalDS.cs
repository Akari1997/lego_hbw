using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Devart.Common;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(ax))]
public struct OracleIntervalDS : IComparable, INullable, IXmlSerializable
{
	private bool m_a;

	private int b;

	private int c;

	private int d;

	private int e;

	private int f;

	private int g;

	private int h;

	public static readonly OracleIntervalDS MaxValue;

	public static readonly OracleIntervalDS MinValue;

	public static readonly OracleIntervalDS Null;

	public static readonly OracleIntervalDS Zero;

	public int Days
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return b;
		}
	}

	public int Hours
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return c;
		}
	}

	public bool IsNull => !this.m_a;

	public double Milliseconds
	{
		get
		{
			if (IsNull)
			{
				return 0.0;
			}
			return f / 1000000;
		}
	}

	public int Nanoseconds
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return f;
		}
	}

	public int Minutes
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return d;
		}
	}

	public int Seconds
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return e;
		}
	}

	public double TotalDays => (double)b + ((double)c + ((double)d + ((double)e + (double)f / 1000000000.0) / 60.0) / 60.0) * 24.0;

	public TimeSpan Value
	{
		get
		{
			if (IsNull)
			{
				return TimeSpan.MinValue;
			}
			return new TimeSpan(((((long)b * 24L + c) * 60 + d) * 60 + e) * 10000000 + f / 100);
		}
	}

	public OracleIntervalDS(TimeSpan ts)
	{
		g = 2;
		h = 6;
		this.m_a = true;
		b = ts.Days;
		c = ts.Hours;
		d = ts.Minutes;
		e = ts.Seconds;
		f = (int)(ts.Ticks % 10000000) * 100;
	}

	public OracleIntervalDS(double totalDays)
	{
		g = 2;
		h = 6;
		this.m_a = true;
		double num = Math.Abs(totalDays);
		b = (int)Math.Floor(num);
		num = (num - Math.Floor(num)) * 24.0;
		c = (int)Math.Floor(num);
		num = (num - Math.Floor(num)) * 60.0;
		d = (int)Math.Floor(num);
		num = (num - Math.Floor(num)) * 60.0;
		e = (int)Math.Floor(num);
		num = (num - Math.Floor(num)) * 1000000000.0;
		f = (int)Math.Floor(num);
		if (totalDays < 0.0)
		{
			b = -b;
			c = -c;
			d = -d;
			e = -e;
			f = -f;
		}
	}

	public OracleIntervalDS(int days, int hours, int minutes, int seconds, double milliSeconds)
		: this(days, hours, minutes, seconds, (int)(milliSeconds * 10000000.0))
	{
	}

	public OracleIntervalDS(int days, int hours, int minutes, int seconds, int nanoSeconds)
	{
		g = 2;
		h = 6;
		if (days < -999999999 || days > 999999999)
		{
			throw new ArgumentOutOfRangeException("days");
		}
		if (hours < -23 || hours > 23)
		{
			throw new ArgumentOutOfRangeException("hours");
		}
		if (minutes < -59 || minutes > 59)
		{
			throw new ArgumentOutOfRangeException("minutes");
		}
		if (seconds < -59 || seconds > 59)
		{
			throw new ArgumentOutOfRangeException("seconds");
		}
		if (nanoSeconds < -999999999 || nanoSeconds > 999999999)
		{
			throw new ArgumentOutOfRangeException("nonoSeconds");
		}
		this.m_a = true;
		b = Math.Abs(days);
		c = Math.Abs(hours);
		d = Math.Abs(minutes);
		e = Math.Abs(seconds);
		f = Math.Abs(nanoSeconds);
		if (days < 0 || days < 0 || hours < 0 || minutes < 0 || seconds < 0 || nanoSeconds < 0)
		{
			b = -b;
			c = -c;
			d = -d;
			e = -e;
			f = -f;
		}
	}

	internal void a(int A_0, int A_1)
	{
		g = A_0;
		h = A_1;
	}

	internal OracleIntervalDS(IntPtr A_0, v A_1)
	{
		if (A_1.h().d() < 9000000)
		{
			throw new InvalidOperationException(Devart.Common.al.a("NeedOCI9Interface"));
		}
		g = 2;
		h = 6;
		if ((int)A_0 != 0)
		{
			HandleRef hndl = A_1.h().h();
			HandleRef err = A_1.h().k();
			Oci oci = A_1.h().j();
			A_1.h().c(oci.OCIIntervalGetDaySecond(hndl, err, out var dy, out var hr, out var mm, out var ss, out var fsec, A_0));
			this.m_a = true;
			b = dy;
			c = hr;
			d = mm;
			e = ss;
			f = fsec;
		}
		else
		{
			this.m_a = false;
			b = 0;
			c = 0;
			d = 0;
			e = 0;
			f = 0;
		}
	}

	static OracleIntervalDS()
	{
		MaxValue = new OracleIntervalDS(999999999, 23, 59, 59, 999999999);
		MinValue = new OracleIntervalDS(-999999999, -23, -59, -59, -999999999);
		Null = default(OracleIntervalDS);
		Zero = new OracleIntervalDS(0, 0, 0, 0, 0);
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
		if ((object)obj.GetType() != typeof(OracleIntervalDS))
		{
			throw new ArgumentException();
		}
		OracleIntervalDS oracleIntervalDS = (OracleIntervalDS)obj;
		if (IsNull)
		{
			if (!oracleIntervalDS.IsNull)
			{
				return -1;
			}
			return 0;
		}
		if (oracleIntervalDS.IsNull)
		{
			return 1;
		}
		if (oracleIntervalDS.TotalDays > TotalDays)
		{
			return -1;
		}
		if (oracleIntervalDS.TotalDays < TotalDays)
		{
			return 1;
		}
		return 0;
	}

	public override bool Equals(object value)
	{
		if ((object)value.GetType() != typeof(OracleIntervalDS))
		{
			return false;
		}
		return CompareTo(value) == 0;
	}

	public static bool Equals(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return value1.CompareTo(value2) == 0;
	}

	public static bool GreaterThan(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return value1.CompareTo(value2) == 1;
	}

	public static bool GreaterThanOrEqual(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return value1.CompareTo(value2) >= 0;
	}

	public static bool LessThan(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return value1.CompareTo(value2) == -1;
	}

	public static bool LessThanOrEqual(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return value1.CompareTo(value2) <= 0;
	}

	public static bool NotEquals(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return value1.CompareTo(value2) != 0;
	}

	public override int GetHashCode()
	{
		if (IsNull)
		{
			return DBNull.Value.GetHashCode();
		}
		return TotalDays.GetHashCode();
	}

	internal static void a(ref string A_0, int A_1, string A_2, int A_3, ref int A_4, bool A_5)
	{
		if (A_4 == 0)
		{
			if (A_1 > 0)
			{
				A_4 = 1;
			}
			else if (A_1 < 0)
			{
				A_4 = -1;
			}
		}
		_ = A_4 * A_1;
		_ = 0;
		int length = A_3.ToString().Length;
		if (A_5)
		{
			A_0 += (A_4 * A_1).ToString("000000000").Substring(0, length);
		}
		else
		{
			string text = (A_4 * A_1).ToString();
			for (int num = text.Length; num < length; num++)
			{
				text = "0" + text;
			}
			A_0 += text;
		}
		if (A_2 != "" && A_2 != null)
		{
			A_0 += A_2;
		}
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return "";
		}
		int num = g;
		int num2 = 1;
		for (int num3 = 1; num3 <= num; num3++)
		{
			num2 *= 10;
		}
		num2--;
		num = h;
		int num4 = 1;
		for (int num5 = 1; num5 <= num; num5++)
		{
			num4 *= 10;
		}
		num4--;
		int A_ = 0;
		string A_2 = "";
		a(ref A_2, b, " ", num2, ref A_, A_5: false);
		a(ref A_2, c, ":", 23, ref A_, A_5: false);
		a(ref A_2, d, ":", 59, ref A_, A_5: false);
		a(ref A_2, e, ".", 59, ref A_, A_5: false);
		a(ref A_2, f, "", num4, ref A_, A_5: true);
		if (A_ >= 0)
		{
			return "+" + A_2;
		}
		return "-" + A_2;
	}

	public static OracleIntervalDS Parse(string value)
	{
		int num = 2;
		if (num < 0 || num > 9)
		{
			num = 9;
		}
		if (num == 0)
		{
			num = 1;
		}
		int num2 = 1;
		for (int num3 = 1; num3 <= num; num3++)
		{
			num2 *= 10;
		}
		num2--;
		num = 6;
		if (num < 0 || num > 9)
		{
			num = 9;
		}
		if (num == 0)
		{
			num = 1;
		}
		int num4 = 1;
		for (int num5 = 1; num5 <= num; num5++)
		{
			num4 *= 10;
		}
		num4--;
		int A_ = 0;
		bool flag = OracleIntervalYM.a(value, ref A_);
		int num6 = OracleIntervalYM.a(value, ref A_, " ", num2, Devart.Common.al.a("ORA01873"), A_5: false);
		int num7 = OracleIntervalYM.a(value, ref A_, ":", 23, Devart.Common.al.a("ORA01850"), A_5: false);
		int num8 = OracleIntervalYM.a(value, ref A_, ":", 59, Devart.Common.al.a("ORA01851"), A_5: false);
		int num9 = OracleIntervalYM.a(value, ref A_, ".", 59, Devart.Common.al.a("ORA01852"), A_5: false);
		int num10 = OracleIntervalYM.a(value, ref A_, "", num4, "", A_5: true);
		if (flag)
		{
			num6 = -num6;
			num7 = -num7;
			num8 = -num8;
			num9 = -num9;
			num10 = -num10;
		}
		return new OracleIntervalDS(num6, num7, num8, num9, num10);
	}

	public static bool TryParse(string value, out OracleIntervalDS result)
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

	public static OracleIntervalDS Add(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		return Add(value1.Days, value1.Hours, value1.Minutes, value1.Seconds, value1.Nanoseconds, value1.Days >= 0 && value1.Hours >= 0 && value1.Minutes >= 0 && value1.Seconds >= 0 && value1.Nanoseconds >= 0, value2.Days, value2.Hours, value2.Minutes, value2.Seconds, value2.Nanoseconds, value2.Days >= 0 && value2.Hours >= 0 && value2.Minutes >= 0 && value2.Seconds >= 0 && value2.Nanoseconds >= 0);
	}

	public static OracleIntervalDS Divide(OracleIntervalDS value, OracleNumber divisor)
	{
		if (value.IsNull || divisor.IsNull)
		{
			return Null;
		}
		return a(value, 1.0 / OracleNumber.b(divisor));
	}

	public static OracleIntervalDS Multiply(OracleIntervalDS value, OracleNumber multiplier)
	{
		if (value.IsNull || multiplier.IsNull)
		{
			return Null;
		}
		return a(value, OracleNumber.b(multiplier));
	}

	private static OracleIntervalDS a(OracleIntervalDS A_0, double A_1)
	{
		if ((double)A_0.Days * A_1 >= 1000000000.0)
		{
			return MaxValue;
		}
		if ((double)A_0.Days * A_1 <= -1000000000.0)
		{
			return MinValue;
		}
		int days = A_0.Days;
		int hours = A_0.Hours;
		int minutes = A_0.Minutes;
		int seconds = A_0.Seconds;
		int nanoseconds = A_0.Nanoseconds;
		bool flag = A_1 < 0.0 == (A_0.Days < 0 || A_0.Hours < 0 || A_0.Minutes < 0 || A_0.Seconds < 0 || A_0.Nanoseconds < 0);
		A_1 = Math.Abs(A_1);
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		long num6 = (long)Math.Floor(A_1);
		if (num6 != 0)
		{
			long num7 = nanoseconds * num6;
			long num8 = num7 / 1000000000;
			num5 = (int)(num7 % 1000000000);
			num7 = seconds * num6 + num8;
			num8 = num7 / 60;
			num4 = (int)(num7 % 60);
			num7 = minutes * num6 + num8;
			num8 = num7 / 60;
			num3 = (int)(num7 % 60);
			num7 = hours * num6 + num8;
			num8 = num7 / 24;
			num2 = (int)(num7 % 24);
			num = (int)(days * num6 + num8);
			if (num >= 1000000000)
			{
				return MaxValue;
			}
			if (num <= -1000000000)
			{
				return MinValue;
			}
		}
		double num9 = 0.0;
		double num10 = 0.0;
		double num11 = 0.0;
		double num12 = 0.0;
		double num13 = 0.0;
		double num14 = A_1 - Math.Floor(A_1);
		if (num14 != 0.0)
		{
			num9 = (double)days * num14;
			double num15 = (double)days - Math.Floor(num9);
			num9 = Math.Floor(num9);
			num10 = ((double)hours + num15 * 24.0) * num14;
			num15 = (double)hours - Math.Floor(num10) / num14;
			num10 = Math.Floor(num10);
			num11 = ((double)minutes + num15 * 60.0) * num14;
			num15 = (double)minutes - Math.Floor(num11) / num14;
			num11 = Math.Floor(num11);
			num12 = ((double)seconds + num15 * 60.0) * num14;
			num15 = (double)seconds - Math.Floor(num12) / num14;
			num12 = Math.Floor(num12);
			num13 = ((double)nanoseconds + num15 * 1000000000.0) * num14;
			num15 = (double)nanoseconds - Math.Floor(num13) / num14;
			num13 = Math.Floor(num13);
		}
		if (!flag)
		{
			num = -num;
			num9 = 0.0 - num9;
			num2 = -num2;
			num10 = 0.0 - num10;
			num3 = -num3;
			num11 = 0.0 - num11;
			num4 = -num4;
			num12 = 0.0 - num12;
			num5 = -num5;
			num13 = 0.0 - num13;
		}
		return new OracleIntervalDS(num + (int)num9, num2 + (int)num10, num3 + (int)num11, num4 + (int)num12, num5 + (int)num13);
	}

	public static OracleIntervalDS Subtract(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		return Add(value1.Days, value1.Hours, value1.Minutes, value1.Seconds, value1.Nanoseconds, value1.Days >= 0 && value1.Hours >= 0 && value1.Minutes >= 0 && value1.Seconds >= 0 && value1.Nanoseconds >= 0, value2.Days, value2.Hours, value2.Minutes, value2.Seconds, value2.Nanoseconds, value2.Days < 0 || value2.Hours < 0 || value2.Minutes < 0 || value2.Seconds < 0 || value2.Nanoseconds < 0);
	}

	public static OracleIntervalDS Add(int day1, int hour1, int minute1, int second1, int fsecond1, bool isPositive1, int day2, int hour2, int minute2, int second2, int fsecond2, bool isPositive2)
	{
		int num;
		bool flag;
		int num2;
		int num3;
		int num4;
		int num5;
		if ((isPositive1 && isPositive2) || (!isPositive1 && !isPositive2))
		{
			num = fsecond1 + fsecond2;
			num2 = num / 1000000000;
			num2 = num2 + second1 + second2;
			num3 = num2 / 60;
			num3 = num3 + minute1 + minute2;
			num4 = num3 / 60;
			num4 = num4 + hour1 + hour2;
			num5 = num4 / 24;
			num5 = num5 + day1 + day2;
			if (num5 >= 1000000000)
			{
				return MaxValue;
			}
			if (num5 <= -1000000000)
			{
				return MinValue;
			}
			flag = isPositive1;
		}
		else
		{
			bool flag2 = false;
			if (day1 > day2)
			{
				flag2 = true;
			}
			else if (hour1 > hour2)
			{
				flag2 = true;
			}
			if (minute1 > minute2)
			{
				flag2 = true;
			}
			if (second1 > second2)
			{
				flag2 = true;
			}
			if (fsecond1 > fsecond2)
			{
				flag2 = true;
			}
			if (flag2)
			{
				flag = isPositive1;
				num = fsecond1 - fsecond2;
				num2 = num / 1000000000;
				num2 = num2 + second1 - second2;
				num3 = num2 / 60;
				num3 = num3 + minute1 - minute2;
				num4 = num3 / 60;
				num4 = num4 + hour1 - hour2;
				num5 = num4 / 24;
				num5 = num5 + day1 - day2;
			}
			else
			{
				flag = isPositive2;
				num = fsecond2 - fsecond1;
				num2 = num / 1000000000;
				num2 = num2 + second2 - second1;
				num3 = num2 / 60;
				num3 = num3 + minute2 - minute1;
				num4 = num3 / 60;
				num4 = num4 + hour2 - hour1;
				num5 = num4 / 24;
				num5 = num5 + day2 - day1;
			}
		}
		if (!flag)
		{
			num5 = -num5;
			num4 = -num4;
			num3 = -num3;
			num2 = -num2;
			num = -num;
		}
		return new OracleIntervalDS(num5, num4, num3, num2, num);
	}

	public static bool operator ==(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return Equals(value1, value2);
	}

	public static bool operator >(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return GreaterThan(value1, value2);
	}

	public static bool operator >=(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return GreaterThanOrEqual(value1, value2);
	}

	public static bool operator !=(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return NotEquals(value1, value2);
	}

	public static bool operator <(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return LessThan(value1, value2);
	}

	public static bool operator <=(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return LessThanOrEqual(value1, value2);
	}

	public static OracleIntervalDS operator +(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return Add(value1, value2);
	}

	public static OracleIntervalDS operator -(OracleIntervalDS value1, OracleIntervalDS value2)
	{
		return Subtract(value1, value2);
	}

	public static OracleIntervalDS operator -(OracleIntervalDS value1)
	{
		return new OracleIntervalDS(-value1.Days, -value1.Hours, -value1.Minutes, -value1.Seconds, -value1.Nanoseconds);
	}

	public static OracleIntervalDS operator *(OracleIntervalDS value1, int multiplier)
	{
		return a(value1, multiplier);
	}

	public static OracleIntervalDS operator /(OracleIntervalDS value1, int divisor)
	{
		return a(value1, 1.0 / (double)divisor);
	}

	public static explicit operator TimeSpan(OracleIntervalDS value)
	{
		return value.Value;
	}

	public static implicit operator OracleIntervalDS(TimeSpan value)
	{
		return new OracleIntervalDS(value);
	}

	private void a()
	{
	}

	XmlSchema IXmlSerializable.GetSchema()
	{
		return null;
	}

	void IXmlSerializable.ReadXml(XmlReader reader)
	{
		if (OracleUtils.a(reader))
		{
			this.m_a = false;
			return;
		}
		OracleIntervalDS oracleIntervalDS = Parse(reader.ReadElementString());
		this.m_a = true;
		b = oracleIntervalDS.b;
		c = oracleIntervalDS.c;
		d = oracleIntervalDS.d;
		e = oracleIntervalDS.e;
		f = oracleIntervalDS.f;
		g = oracleIntervalDS.g;
		h = oracleIntervalDS.h;
	}

	void IXmlSerializable.WriteXml(XmlWriter writer)
	{
		if (IsNull)
		{
			OracleUtils.a(writer);
		}
		else
		{
			writer.WriteString(ToString());
		}
	}
}
