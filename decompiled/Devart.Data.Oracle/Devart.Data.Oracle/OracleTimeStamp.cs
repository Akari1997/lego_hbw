using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Devart.Common;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(aj))]
public struct OracleTimeStamp : IComparable, INullable, IXmlSerializable
{
	private const string m_a = "ORA-01862 the numeric value does not match the length of the format item";

	private const string m_b = "ORA-01821 date format not recognized";

	private const string m_c = "ORA-01858 a non-numeric character was found where a numeric was expected";

	private const string m_d = "ORA-01840 input value not long enough for date format";

	private const string e = "ORA-01874 time zone hour must be between -12 && 13";

	private const string f = "ORA-01875 time zone minute must be between -59 && 59";

	private const string g = "ORA-01851 minutes must be between 0 && 59";

	private const string h = "ORA-01852 seconds must be between 0 && 59";

	private const string i = "ORA-01849 hour must be between 1 && 12";

	private const string j = "ORA-01850 hour must be between 0 && 23";

	private const string k = "ORA-01841 (full) year must be between -4713 && +9999, && not be 0";

	private const string l = "ORA-01848 day of year must be between 1 && 365 (366 for (leap year)";

	private const string m = "ORA-01847 day of month must be between 1 && last day of month";

	private const string n = "ORA-01843 not a valid month";

	private const string o = "ORA-01846 not a valid day of the week";

	private const string p = "ORA-01855 AM/A.M. || PM/P.M. required";

	private const string q = "ORA-01856 BC/B.C. || AD/A.D. required";

	private const string r = "ORA-01861 literal does not match format string";

	private const string s = "ORA-01810 format code appears twice";

	private const string t = "ORA-01880 the fractional seconds must be between 0 && 999999999";

	private const string u = "ORA-01833 month conflicts with Julian date";

	private const string v = "ORA-01834 day of month conflicts with Julian date";

	private const string w = "ORA-01835 day of week conflicts with Julian date";

	private const string x = "ORA-01818 'HH24' precludes use of meridian indicator";

	private const string y = "ORA-01819 signed year precludes use of BC/AD";

	private const string z = "ORA-01839 date not valid for (month specified";

	private const string aa = "ORA-01878 specified field not found in datetime || interval";

	private const string ab = "ORA-01870 the intervals || datetimes are not mutually comparable";

	private const string ac = "ORA-01882: timezone region not found";

	private const string ad = " -+/,.;:'";

	internal short ae;

	internal byte af;

	internal byte ag;

	internal byte ah;

	internal byte ai;

	internal byte aj;

	internal int ak;

	internal sbyte al;

	internal sbyte am;

	internal int an;

	private bool ao;

	private OracleDbType ap;

	private int aq;

	public static readonly OracleTimeStamp MaxValue;

	public static readonly OracleTimeStamp MinValue;

	public static readonly OracleTimeStamp Null;

	private static string[] ar;

	private static string[] @as;

	private static string[] at;

	private static string[] au;

	private static int[] av;

	private static string[] aw;

	private static sbyte[] ax;

	private static sbyte[] ay;

	private static Dictionary<string, int> az;

	private static Dictionary<int, int> a0;

	public int Day
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return ag;
		}
	}

	public bool IsNull => !ao;

	public int Hour
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return ah;
		}
	}

	public double Millisecond
	{
		get
		{
			if (IsNull)
			{
				return 0.0;
			}
			return ak / 1000000;
		}
	}

	public int Nanosecond
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return ak;
		}
	}

	public int Minute
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return ai;
		}
	}

	public int Month
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return af;
		}
	}

	public int Second
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return aj;
		}
	}

	public OracleDbType TimeStampType => ap;

	public string TimeZone
	{
		get
		{
			if (an >= 0)
			{
				return aw[an];
			}
			string text = "";
			text = ((al >= 0 && (al != 0 || am >= 0)) ? (text + "+") : (text + "-"));
			return text + Math.Abs(al).ToString("00") + ":" + Math.Abs(am).ToString("00");
		}
	}

	public TimeSpan TimeZoneOffset => new TimeSpan(al, am, 0);

	public DateTime Value
	{
		get
		{
			if (IsNull)
			{
				return DateTime.MinValue;
			}
			return new DateTime(ae, af, ag, ah, ai, aj).AddTicks(Nanosecond / 100);
		}
	}

	public int Year
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return ae;
		}
	}

	public OracleTimeStamp(DateTime dt)
		: this(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, (int)(dt.Ticks % 10000000 * 100), 0, 0, OracleDbType.TimeStamp)
	{
	}

	public OracleTimeStamp(DateTime dt, TimeSpan timeZone)
		: this(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, (int)(dt.Ticks % 10000000 * 100), timeZone.Hours, timeZone.Minutes, OracleDbType.TimeStampTZ)
	{
	}

	public OracleTimeStamp(DateTime dt, string timeZone)
		: this(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, (int)(dt.Ticks % 10000000 * 100), timeZone, OracleDbType.TimeStampTZ)
	{
	}

	public OracleTimeStamp(int year, int month, int day)
		: this(year, month, day, 0, 0, 0, 0, "", OracleDbType.TimeStamp)
	{
	}

	public OracleTimeStamp(int year, int month, int day, TimeSpan timeZone)
		: this(year, month, day, 0, 0, 0, 0, timeZone)
	{
	}

	public OracleTimeStamp(int year, int month, int day, string timeZone)
		: this(year, month, day, 0, 0, 0, 0, timeZone)
	{
	}

	public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second)
		: this(year, month, day, hour, minute, second, 0, "", OracleDbType.TimeStamp)
	{
	}

	public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, int nanosecond)
		: this(year, month, day, hour, minute, second, nanosecond, "", OracleDbType.TimeStamp)
	{
	}

	public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, int nanosecond, TimeSpan timeZone)
		: this(year, month, day, hour, minute, second, nanosecond, timeZone.Hours, timeZone.Minutes, OracleDbType.TimeStampTZ)
	{
	}

	public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, int nanosecond, string timeZone)
		: this(year, month, day, hour, minute, second, nanosecond, timeZone, OracleDbType.TimeStampTZ)
	{
	}

	public OracleTimeStamp(DateTime dt, OracleDbType dbType)
		: this(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, (int)(dt.Ticks % 10000000 * 100), dbType)
	{
	}

	public OracleTimeStamp(int year, int month, int day, OracleDbType dbType)
		: this(year, month, day, 0, 0, 0, dbType)
	{
	}

	public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, OracleDbType dbType)
		: this(year, month, day, hour, minute, second, 0, dbType)
	{
	}

	public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, int nanosecond, OracleDbType dbType)
		: this(year, month, day, hour, minute, second, nanosecond, "", dbType)
	{
	}

	internal OracleTimeStamp(int A_0, int A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, int A_8, int A_9, OracleDbType A_10)
		: this(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_10)
	{
		an = A_9;
	}

	internal void d(int A_0)
	{
		aq = A_0;
	}

	internal OracleTimeStamp(int A_0, int A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, int A_8, OracleDbType A_9)
		: this(A_9)
	{
		aq = 9;
		if (A_0 < -4712 || A_0 > 9999)
		{
			throw new ArgumentException(Devart.Common.al.a("YearIsOutOfRange"), "year");
		}
		if (A_1 < 1 || A_1 > 12)
		{
			throw new ArgumentException(Devart.Common.al.a("MonthIsOutOfRange"), "month");
		}
		if (A_2 < 1 || A_2 > 31)
		{
			throw new ArgumentException(Devart.Common.al.a("DayIsOutOfRange"), "day");
		}
		if (A_3 < 0 || A_3 > 23)
		{
			throw new ArgumentException(Devart.Common.al.a("HourIsOutOfRange"), "hour");
		}
		if (A_4 < 0 || A_4 > 59)
		{
			throw new ArgumentException(Devart.Common.al.a("MinuteIsOutOfRange"), "minute");
		}
		if (A_5 < 0 || A_5 > 59)
		{
			throw new ArgumentException(Devart.Common.al.a("SecondIsOutOfRange"), "second");
		}
		if (A_6 < 0 || A_6 > 999999999)
		{
			throw new ArgumentException(Devart.Common.al.a("NanosecondIsOutOfRange"), "nanosecond");
		}
		ae = (short)A_0;
		af = (byte)A_1;
		ag = (byte)A_2;
		ah = (byte)A_3;
		ai = (byte)A_4;
		ae = (short)A_0;
		af = (byte)A_1;
		ag = (byte)A_2;
		ah = (byte)A_3;
		ai = (byte)A_4;
		aj = (byte)A_5;
		ak = A_6;
		ao = true;
		if (ap == OracleDbType.TimeStampLTZ)
		{
			OracleGlobalization oracleGlobalization = OracleGlobalization.ApplicationGlobalization;
			al = oracleGlobalization.TzHour;
			am = oracleGlobalization.TzMinute;
		}
		else if (ap == OracleDbType.TimeStamp)
		{
			al = 0;
			am = 0;
		}
		else
		{
			al = (sbyte)A_7;
			am = (sbyte)A_8;
		}
	}

	public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, int nanosecond, TimeSpan timeZone, OracleDbType dbType)
		: this(year, month, day, hour, minute, second, nanosecond, timeZone.Hours, timeZone.Minutes, dbType)
	{
	}

	public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, int nanosecond, string timeZone, OracleDbType dbType)
		: this(year, month, day, hour, minute, second, nanosecond, 0, 0, dbType)
	{
		if (timeZone == null)
		{
			throw new ArgumentNullException("timeZone");
		}
		if (ap == OracleDbType.TimeStampTZ)
		{
			if (!string.IsNullOrEmpty(timeZone) && timeZone.Length > 1 && char.IsLetter(timeZone[0]))
			{
				an = a(timeZone);
			}
			if (an < 0)
			{
				a(timeZone, out al, out am);
				return;
			}
			al = ax[an];
			am = ay[an];
		}
	}

	internal static void a(string A_0, out sbyte A_1, out sbyte A_2)
	{
		int num = 1;
		int A_3 = 0;
		if (A_0 == "")
		{
			A_1 = 0;
			A_2 = 0;
			return;
		}
		if (A_0[A_3] == '-')
		{
			A_3++;
			num = -1;
		}
		else if (A_0[A_3] == '+')
		{
			A_3++;
			num = 1;
		}
		A_1 = (sbyte)(num * a(A_0, A_0.Length, ref A_3, 2, -12, 14, 1874, "ORA-01874 time zone hour must be between -12 && 13"));
		if (A_1 < -12)
		{
			throw new OracleException(1874, "ORA-01874 time zone hour must be between -12 && 13");
		}
		if (A_0[A_3] != ':')
		{
			throw new OracleException(1821, "ORA-01821 date format not recognized");
		}
		A_3++;
		if (A_0[A_3] == '-' || A_0[A_3] == '+')
		{
			A_3++;
		}
		A_2 = (sbyte)a(A_0, A_0.Length, ref A_3, 2, -59, 59, 1875, "ORA-01875 time zone minute must be between -59 && 59");
		if (A_1 < 0)
		{
			A_2 = (sbyte)(-A_2);
		}
		else if (A_1 == 0 && num == -1)
		{
			A_2 = (sbyte)(-A_2);
		}
		if (A_1 != 14 || A_2 == 0)
		{
			return;
		}
		throw new OracleException(1875, "ORA-01875 time zone minute must be between -59 && 59");
	}

	internal OracleTimeStamp(OracleDbType A_0)
	{
		aq = 9;
		ae = 0;
		af = 0;
		ag = 0;
		ah = 0;
		ai = 0;
		aj = 0;
		ak = 0;
		al = 0;
		am = 0;
		ao = false;
		ap = A_0;
		an = -1;
	}

	internal OracleTimeStamp(IntPtr A_0, OracleDbType A_1, v A_2)
		: this(A_1)
	{
		if (A_2.h().d() < 9000000)
		{
			throw new InvalidOperationException(Devart.Common.al.a("NeedOCI9Interface"));
		}
		if (A_0 != IntPtr.Zero)
		{
			HandleRef hndl = A_2.h().h();
			HandleRef err = A_2.h().k();
			Oci oci = A_2.h().j();
			A_2.h().c(oci.OCIDateTimeGetDate(hndl, err, A_0, out var year, out var month, out var day));
			A_2.h().c(oci.OCIDateTimeGetTime(hndl, err, A_0, out var hour, out var min, out var sec, out var fsec));
			ae = year;
			af = month;
			ag = day;
			ah = hour;
			ai = min;
			aj = sec;
			ak = (int)fsec;
			ao = true;
			ap = A_1;
			if (A_1 == OracleDbType.TimeStampTZ || A_1 == OracleDbType.TimeStampLTZ)
			{
				int num = 10;
				A_2.h().c(oci.OCIDateTimeGetTimeZoneOffset(hndl, err, A_0, out al, out am));
				byte[] array = new byte[100];
				int buflen = 50;
				A_2.h().c(oci.OCIDateTimeGetTimeZoneName(hndl, err, A_0, array, ref buflen));
				string text = A_2.h().o().GetString(array, 0, buflen);
				if (text != null)
				{
					an = a(text.Trim());
				}
			}
		}
		else
		{
			ao = false;
		}
	}

	static OracleTimeStamp()
	{
		a();
		MaxValue = new OracleTimeStamp(9999, 12, 31, 23, 59, 59, 999999999);
		MinValue = new OracleTimeStamp(-4712, 1, 1, 0, 0, 0, 0);
		Null = default(OracleTimeStamp);
		ar = new string[7];
		ar[0] = "SUN";
		ar[1] = "MON";
		ar[2] = "TUE";
		ar[3] = "WED";
		ar[4] = "THU";
		ar[5] = "FRI";
		ar[6] = "SAT";
		@as = new string[12];
		@as[0] = "JANUARY";
		@as[1] = "FEBRUARY";
		@as[2] = "MARCH";
		@as[3] = "APRIL";
		@as[4] = "MAY";
		@as[5] = "JUNE";
		@as[6] = "JULY";
		@as[7] = "AUGUST";
		@as[8] = "SEPTEMBER";
		@as[9] = "OCTOBER";
		@as[10] = "NOVEMBER";
		@as[11] = "DECEMBER";
		at = new string[12];
		at[0] = "JAN";
		at[1] = "FEB";
		at[2] = "MAR";
		at[3] = "APR";
		at[4] = "MAY";
		at[5] = "JUN";
		at[6] = "JUL";
		at[7] = "AUG";
		at[8] = "SEP";
		at[9] = "OCT";
		at[10] = "NOV";
		at[11] = "DEC";
		au = new string[12]
		{
			"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X",
			"XI", "XII"
		};
	}

	internal static int a(string A_0, int A_1, ref int A_2, int A_3, int A_4, int A_5, int A_6, string A_7)
	{
		if (A_2 >= A_1 || A_0[A_2] < '0' || A_0[A_2] > '9')
		{
			throw new OracleException(1858, "ORA-01858 a non-numeric character was found where a numeric was expected");
		}
		string text = "";
		for (int num = 0; num < A_3; num++)
		{
			if (A_2 >= A_1)
			{
				break;
			}
			if (A_0[A_2] < '0')
			{
				break;
			}
			if (A_0[A_2] > '9')
			{
				break;
			}
			text += A_0[A_2];
			A_2++;
		}
		int num2 = Utils.ParseIntWith0(text);
		if (A_5 > 0 && (num2 < A_4 || num2 > A_5))
		{
			throw new OracleException(A_6, A_7);
		}
		return num2;
	}

	public OracleTimeStamp AddDays(double days)
	{
		return AddSeconds(days * 24.0 * 60.0 * 60.0);
	}

	public OracleTimeStamp AddHours(double hours)
	{
		return AddSeconds(hours * 60.0 * 60.0);
	}

	public OracleTimeStamp AddMilliseconds(double milliseconds)
	{
		return AddSeconds(milliseconds / 1000.0);
	}

	public OracleTimeStamp AddMinutes(double minutes)
	{
		return AddSeconds(minutes * 60.0);
	}

	public OracleTimeStamp AddMonths(long months)
	{
		if (IsNull)
		{
			return Null;
		}
		long num = af - 1 + months;
		long num2 = ae + num / 12;
		num = num % 12 + 1;
		if (num2 < -4712 || num2 > 9999)
		{
			throw new ArgumentException(Devart.Common.al.a("YearIsOutOfRange"), "year");
		}
		return new OracleTimeStamp((short)num2, (byte)num, ag, ah, ai, aj, ak, al, am, an, ap);
	}

	public OracleTimeStamp AddSeconds(double seconds)
	{
		if (IsNull)
		{
			return Null;
		}
		int num = 0;
		short num2 = ae;
		while (num2 <= 1)
		{
			num2 += 4000;
			num++;
		}
		DateTime dateTime = new DateTime(num2, af, ag, ah, ai, aj);
		long num3 = (long)seconds * 10000000;
		long num4 = (long)(seconds * 1000000000.0) - num3 * 100;
		if (ak + num4 > 1000000000)
		{
			num3++;
		}
		else if (ak + num4 < -1000000000)
		{
			num3--;
		}
		TimeSpan value = new TimeSpan(num3);
		dateTime = dateTime.Add(value);
		num2 = (short)dateTime.Year;
		while (num > 0)
		{
			num2 -= 4000;
			num--;
		}
		return new OracleTimeStamp(num2, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, (int)(((ak + num4) % 1000000000) & 0x7FFFFFFFFFFFFFFFL), al, am, an, ap);
	}

	public OracleTimeStamp AddYears(int years)
	{
		if (IsNull)
		{
			return Null;
		}
		long num = ae + years;
		if (num < -4712 || num > 9999)
		{
			throw new ArgumentException(Devart.Common.al.a("YearIsOutOfRange"), "year");
		}
		return new OracleTimeStamp((int)num, af, ag, ah, ai, aj, ak, al, am, an, ap);
	}

	internal static void a(ref short A_0, ref byte A_1, ref byte A_2, ref byte A_3, ref byte A_4, TimeSpan A_5, bool A_6)
	{
		a(ref A_0, ref A_1, ref A_2, ref A_3, ref A_4, A_5, A_6, A_7: false);
	}

	internal static TimeSpan a(ref short A_0, ref byte A_1, ref byte A_2, ref byte A_3, ref byte A_4, TimeSpan A_5, bool A_6, bool A_7)
	{
		TimeSpan result = TimeSpan.Zero;
		int num;
		for (num = A_0; num <= 1; num += 4000)
		{
		}
		while (num >= 8000)
		{
			num -= 4000;
		}
		DateTime dateTime = new DateTime(num, A_1, A_2, A_3, A_4, 0, 0);
		if (A_6)
		{
			if (A_7 && A_5.Ticks >= 0 && DateTime.MaxValue - A_5 < dateTime)
			{
				dateTime = DateTime.MaxValue;
				result = dateTime - (DateTime.MaxValue - A_5);
			}
			else if (A_7 && A_5.Ticks < 0 && DateTime.MinValue - A_5 > dateTime)
			{
				dateTime = DateTime.MinValue;
				result = DateTime.MinValue - A_5 - dateTime;
			}
			else
			{
				dateTime += A_5;
			}
		}
		else if (A_7 && A_5.Ticks >= 0 && DateTime.MinValue + A_5 > dateTime)
		{
			dateTime = DateTime.MinValue;
			result = DateTime.MinValue + A_5 - dateTime;
		}
		else if (A_7 && A_5.Ticks < 0 && DateTime.MaxValue + A_5 < dateTime)
		{
			dateTime = DateTime.MaxValue;
			result = dateTime - (DateTime.MaxValue + A_5);
		}
		else
		{
			dateTime -= A_5;
		}
		A_0 = (short)(dateTime.Year + A_0 - num);
		A_1 = (byte)dateTime.Month;
		A_2 = (byte)dateTime.Day;
		A_3 = (byte)dateTime.Hour;
		A_4 = (byte)dateTime.Minute;
		return result;
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
		if ((object)obj.GetType() != typeof(OracleTimeStamp))
		{
			throw new ArgumentException();
		}
		OracleTimeStamp oracleTimeStamp = (OracleTimeStamp)obj;
		if (IsNull)
		{
			if (!oracleTimeStamp.IsNull)
			{
				return -1;
			}
			return 0;
		}
		if (oracleTimeStamp.IsNull)
		{
			return 1;
		}
		short A_ = (short)oracleTimeStamp.Year;
		byte A_2 = (byte)oracleTimeStamp.Month;
		byte A_3 = (byte)oracleTimeStamp.Day;
		byte A_4 = (byte)oracleTimeStamp.Hour;
		byte A_5 = (byte)oracleTimeStamp.Minute;
		TimeSpan timeSpan = TimeSpan.Zero;
		if (oracleTimeStamp.ap == OracleDbType.TimeStampTZ || oracleTimeStamp.ap == OracleDbType.TimeStampLTZ)
		{
			timeSpan = a(ref A_, ref A_2, ref A_3, ref A_4, ref A_5, oracleTimeStamp.TimeZoneOffset, A_6: true, A_7: true);
		}
		short A_6 = ae;
		byte A_7 = af;
		byte A_8 = ag;
		byte A_9 = ah;
		byte A_10 = ai;
		TimeSpan timeSpan2 = TimeSpan.Zero;
		if (ap == OracleDbType.TimeStampTZ || ap == OracleDbType.TimeStampLTZ)
		{
			timeSpan2 = a(ref A_6, ref A_7, ref A_8, ref A_9, ref A_10, TimeZoneOffset, A_6: true, A_7: true);
		}
		if (A_ > A_6)
		{
			return -1;
		}
		if (A_ < A_6)
		{
			return 1;
		}
		if (A_2 > A_7)
		{
			return -1;
		}
		if (A_2 < A_7)
		{
			return 1;
		}
		if (A_3 > A_8)
		{
			return -1;
		}
		if (A_3 < A_8)
		{
			return 1;
		}
		if (A_4 > A_9)
		{
			return -1;
		}
		if (A_4 < A_9)
		{
			return 1;
		}
		if (A_5 > A_10)
		{
			return -1;
		}
		if (A_5 < A_10)
		{
			return 1;
		}
		if (oracleTimeStamp.Second > aj)
		{
			return -1;
		}
		if (oracleTimeStamp.Second < aj)
		{
			return 1;
		}
		if (oracleTimeStamp.Nanosecond > ak)
		{
			return -1;
		}
		if (oracleTimeStamp.Nanosecond < ak)
		{
			return 1;
		}
		if (timeSpan > timeSpan2)
		{
			return -1;
		}
		if (timeSpan < timeSpan2)
		{
			return 1;
		}
		return 0;
	}

	public OracleIntervalDS GetDaysBetween(OracleTimeStamp value)
	{
		OracleTimeStamp oracleTimeStamp = value;
		if (IsNull || oracleTimeStamp.IsNull)
		{
			return OracleIntervalDS.Null;
		}
		short A_ = (short)oracleTimeStamp.Year;
		byte A_2 = (byte)oracleTimeStamp.Month;
		byte A_3 = (byte)oracleTimeStamp.Day;
		byte A_4 = (byte)oracleTimeStamp.Hour;
		byte A_5 = (byte)oracleTimeStamp.Minute;
		if (oracleTimeStamp.ap == OracleDbType.TimeStampTZ || oracleTimeStamp.ap == OracleDbType.TimeStampLTZ)
		{
			a(ref A_, ref A_2, ref A_3, ref A_4, ref A_5, oracleTimeStamp.TimeZoneOffset, A_6: true);
		}
		short A_6 = ae;
		byte A_7 = af;
		byte A_8 = ag;
		byte A_9 = ah;
		byte A_10 = ai;
		if (ap == OracleDbType.TimeStampTZ || ap == OracleDbType.TimeStampLTZ)
		{
			a(ref A_6, ref A_7, ref A_8, ref A_9, ref A_10, TimeZoneOffset, A_6: true);
		}
		int num = 0;
		while (A_ <= 1)
		{
			A_ += 4000;
			num++;
		}
		int num2 = 0;
		while (A_6 < 0)
		{
			A_6 += 4000;
			num2++;
		}
		int num3 = (new DateTime(A_, A_2, A_3, A_4, A_5, oracleTimeStamp.Second, (int)oracleTimeStamp.Millisecond) - new DateTime(A_6, A_7, A_8, A_9, A_10, aj, (int)Millisecond)).Days;
		while (num > 0)
		{
			num3 -= 1461000;
			num--;
		}
		while (num2 > 0)
		{
			num3 += 1461000;
			num2--;
		}
		return new OracleIntervalDS(num3, 0, 0, 0, 0);
	}

	public override int GetHashCode()
	{
		int num = ae;
		num |= af << 16;
		num |= ag << 24;
		num |= ah;
		num |= ai << 8;
		num |= aj << 16;
		num |= al << 24;
		num |= am;
		num |= ak;
		num |= ak;
		return num | an;
	}

	public static OracleTimeStamp GetSysDate()
	{
		return new OracleTimeStamp(DateTime.Now);
	}

	public OracleIntervalYM GetYearsBetween(OracleTimeStamp value)
	{
		OracleTimeStamp oracleTimeStamp = value;
		if (IsNull || oracleTimeStamp.IsNull)
		{
			return OracleIntervalYM.Null;
		}
		short A_ = (short)oracleTimeStamp.Year;
		byte A_2 = (byte)oracleTimeStamp.Month;
		byte A_3 = (byte)oracleTimeStamp.Day;
		byte A_4 = (byte)oracleTimeStamp.Hour;
		byte A_5 = (byte)oracleTimeStamp.Minute;
		if (oracleTimeStamp.ap == OracleDbType.TimeStampTZ || oracleTimeStamp.ap == OracleDbType.TimeStampLTZ)
		{
			a(ref A_, ref A_2, ref A_3, ref A_4, ref A_5, oracleTimeStamp.TimeZoneOffset, A_6: true);
		}
		short num = ae;
		byte b2 = af;
		byte b3 = ag;
		byte b4 = ah;
		byte b5 = ai;
		int years = ((A_ > num) ? ((A_2 > b2) ? (A_ - num) : ((A_2 < b2) ? (A_ - num - 1) : ((A_3 > b3) ? (A_ - num) : ((A_3 < b3) ? (A_ - num - 1) : ((A_4 > b4) ? (A_ - num) : ((A_4 < b4) ? (A_ - num - 1) : ((A_5 > b5) ? (A_ - num) : ((A_5 < b5) ? (A_ - num - 1) : ((oracleTimeStamp.Second > aj) ? (A_ - num) : ((oracleTimeStamp.Second < aj) ? (A_ - num - 1) : ((oracleTimeStamp.Nanosecond < ak) ? (A_ - num - 1) : (A_ - num)))))))))))) : ((A_2 < b2) ? (A_ - num) : ((A_2 > b2) ? (A_ - num - 1) : ((A_3 < b3) ? (A_ - num) : ((A_3 > b3) ? (A_ - num - 1) : ((A_4 < b4) ? (A_ - num) : ((A_4 > b4) ? (A_ - num - 1) : ((A_5 < b5) ? (A_ - num) : ((A_5 > b5) ? (A_ - num - 1) : ((oracleTimeStamp.Second < aj) ? (A_ - num) : ((oracleTimeStamp.Second > aj) ? (A_ - num - 1) : ((oracleTimeStamp.Nanosecond > ak) ? (A_ - num - 1) : (A_ - num)))))))))))));
		return new OracleIntervalYM(years, 0);
	}

	public override bool Equals(object value)
	{
		return CompareTo(value) == 0;
	}

	public static bool Equals(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return value1.CompareTo(value2) == 0;
	}

	public static bool GreaterThan(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return value1.CompareTo(value2) == 1;
	}

	public static bool GreaterThanOrEqual(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return value1.CompareTo(value2) >= 0;
	}

	public static bool LessThan(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return value1.CompareTo(value2) == -1;
	}

	public static bool LessThanOrEqual(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return value1.CompareTo(value2) <= 0;
	}

	public static bool NotEquals(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return value1.CompareTo(value2) != 0;
	}

	public OracleDate ToOracleDate()
	{
		if (IsNull)
		{
			return OracleDate.Null;
		}
		return new OracleDate(ae, af, ag, ah, ai, aj);
	}

	private static string c(string A_0, bool A_1)
	{
		if (A_1)
		{
			return "0";
		}
		return A_0;
	}

	private static string a(bool A_0)
	{
		if (A_0)
		{
			return "{0}";
		}
		return "{0,-9:G}";
	}

	private static string c(int A_0)
	{
		return au[A_0 - 1];
	}

	private static string a(bool A_0, bool A_1, string A_2)
	{
		string text = "";
		CultureInfo culture = OracleGlobalization.ApplicationGlobalization.s;
		if (A_2.Length > 0)
		{
			text = ((!A_0) ? A_2[0].ToString().ToUpper(culture) : A_2[0].ToString().ToLower(culture));
		}
		if (A_2.Length > 1)
		{
			text = ((!A_1 && !A_0) ? (text + A_2.Substring(1, A_2.Length - 1).ToUpper(culture)) : (text + A_2.Substring(1, A_2.Length - 1).ToLower(culture)));
		}
		return text;
	}

	private static string b(string A_0, bool A_1)
	{
		CultureInfo culture = OracleGlobalization.ApplicationGlobalization.s;
		if (A_1)
		{
			return A_0.ToLower(culture);
		}
		return A_0;
	}

	public override string ToString()
	{
		return ToString((ap == OracleDbType.TimeStampTZ) ? OracleGlobalization.ApplicationGlobalization.TimeStampTZFormat : OracleGlobalization.ApplicationGlobalization.TimeStampFormat);
	}

	public string ToString(string format)
	{
		if (IsNull)
		{
			return string.Empty;
		}
		short a_ = ae;
		byte a_2 = af;
		byte a_3 = ag;
		byte a_4 = ah;
		byte a_5 = ai;
		byte a_6 = aj;
		int a_7 = ak;
		sbyte a_8 = 0;
		sbyte a_9 = 0;
		if (ap != OracleDbType.TimeStampLTZ)
		{
			a_8 = al;
			a_9 = am;
		}
		return a(format, a_, a_2, a_3, a_4, a_5, a_6, a_7, a_8, a_9, an, aq);
	}

	internal static string a(string A_0, short A_1, byte A_2, byte A_3, byte A_4, byte A_5, byte A_6, int A_7, sbyte A_8, sbyte A_9, int A_10, int A_11)
	{
		OracleGlobalization oracleGlobalization = OracleGlobalization.ApplicationGlobalization;
		string text = A_0;
		int length = text.Length;
		text += "                        ";
		string text2 = text.ToUpper(oracleGlobalization.s);
		int num;
		for (num = A_1; num <= 1; num += 4000)
		{
		}
		DateTime dateTime = new DateTime(num, A_2, A_3, A_4, A_5, A_6).AddTicks(A_7 / 100);
		Calendar calendar = oracleGlobalization.s.Calendar;
		bool flag = false;
		int num2 = 0;
		string text3 = "";
		while (num2 < length)
		{
			switch (text2[num2])
			{
			case 'A':
				num2++;
				if (text2[num2] == 'D')
				{
					num2++;
					text3 = ((A_1 >= 0) ? (text3 + b("AD", text[num2 - 2] == 'a')) : (text3 + b("BC", text[num2 - 2] == 'a')));
					break;
				}
				if (text2.Substring(num2, 3) == ".D.")
				{
					num2 += 3;
					text3 = ((A_1 >= 0) ? (text3 + b("A.D.", text[num2 - 4] == 'a')) : (text3 + b("B.C.", text[num2 - 4] == 'a')));
					break;
				}
				if (text2[num2] == 'M')
				{
					num2++;
					text3 = ((A_4 >= 12) ? (text3 + b("PM", text[num2 - 2] == 'a')) : (text3 + b("AM", text[num2 - 2] == 'a')));
					break;
				}
				if (text2.Substring(num2, 3) == ".M.")
				{
					num2 += 3;
					text3 = ((A_4 >= 12) ? (text3 + b("P.M.", text[num2 - 4] == 'a')) : (text3 + b("A.M.", text[num2 - 4] == 'a')));
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'B':
				num2++;
				if (text2[num2] == 'C')
				{
					num2++;
					text3 = ((A_1 >= 0) ? (text3 + b("AD", text[num2 - 2] == 'b')) : (text3 + b("BC", text[num2 - 2] == 'b')));
					break;
				}
				if (text2.Substring(num2, 3) == ".C.")
				{
					num2 += 3;
					text3 = ((A_1 >= 0) ? (text3 + b("A.D.", text[num2 - 4] == 'b')) : (text3 + b("B.C.", text[num2 - 4] == 'b')));
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'C':
				num2++;
				if (text2[num2] == 'C')
				{
					num2++;
					text3 += ((Math.Abs(A_1) - 1) / 100 + 1).ToString(c("00", flag));
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'D':
				num2++;
				if (text2.Substring(num2, 2) == "DD")
				{
					num2 += 2;
					text3 += calendar.GetDayOfYear(dateTime).ToString(c("000", flag));
				}
				else if (text2[num2] == 'D')
				{
					num2++;
					text3 += A_3.ToString(c("00", flag));
				}
				else if (text2[num2] == 'Y')
				{
					num2++;
					text3 += a(text[num2 - 2] == 'd', text[num2 - 1] == 'y', ar[(int)calendar.GetDayOfWeek(dateTime)]);
				}
				else if (text2.Substring(num2, 2) == "AY")
				{
					num2 += 2;
					text3 += string.Format(a(flag), a(text[num2 - 3] == 'd', text[num2 - 2] == 'a', calendar.GetDayOfWeek(dateTime).ToString()));
				}
				else
				{
					text3 += calendar.GetDayOfWeek(dateTime);
				}
				break;
			case 'F':
				num2++;
				if (text2[num2] == 'M')
				{
					num2++;
					flag = !flag;
					break;
				}
				if (text2[num2] == 'X')
				{
					num2++;
					break;
				}
				if (text2[num2] == 'F')
				{
					num2++;
					int length2 = A_11;
					if (text[num2] >= '1' && text[num2] <= '9')
					{
						length2 = text[num2] - 48;
						num2++;
					}
					string text4 = A_7.ToString("000000000").Substring(0, length2);
					text3 += text4;
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'H':
				num2++;
				if (text2[num2] == 'H')
				{
					num2++;
					if (text2.Substring(num2, 2) == "24")
					{
						num2 += 2;
						text3 += A_4.ToString(c("00", flag));
					}
					else if (text2.Substring(num2, 2) == "12")
					{
						num2 += 2;
						text3 += ((A_4 + 11) % 12 + 1).ToString(c("00", flag));
					}
					else
					{
						text3 += ((A_4 + 11) % 12 + 1).ToString(c("00", flag));
					}
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'I':
				num2++;
				if (text2.Substring(num2, 3) == "YYY")
				{
					num2 += 3;
					text3 += ((int)Math.Abs(A_1)).ToString(c("0000", flag));
				}
				else if (text2.Substring(num2, 2) == "YY")
				{
					num2 += 2;
					text3 += (Math.Abs(A_1) % 1000).ToString(c("000", flag));
				}
				else if (text2[num2] == 'Y')
				{
					num2++;
					text3 += (Math.Abs(A_1) % 100).ToString(c("00", flag));
				}
				else if (text2[num2] == 'W')
				{
					num2++;
					text3 += calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday).ToString(c("00", flag));
				}
				else
				{
					text3 += (Math.Abs(A_1) % 10).ToString("0");
				}
				break;
			case 'J':
				num2++;
				text3 += ((dateTime - new DateTime(1, 1, 1)).Days + 2415019 - (num - A_1) / 400 * 146097).ToString(c("0000000", flag));
				break;
			case 'M':
				num2++;
				if (text2[num2] == 'I')
				{
					num2++;
					text3 += A_5.ToString(c("00", flag));
					break;
				}
				if (text2[num2] == 'M')
				{
					num2++;
					text3 += A_2.ToString(c("00", flag));
					break;
				}
				if (text2.Substring(num2, 4) == "ONTH")
				{
					num2 += 4;
					text3 += string.Format(a(flag), a(text[num2 - 5] == 'm', text[num2 - 6] == 'o', @as[A_2 - 1]));
					break;
				}
				if (text2.Substring(num2, 2) == "ON")
				{
					num2 += 2;
					text3 += a(text[num2 - 3] == 'm', text[num2 - 2] == 'o', at[A_2 - 1]);
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'P':
				num2++;
				if (text2[num2] == 'M')
				{
					num2++;
					text3 = ((A_4 >= 12) ? (text3 + b("PM", text[num2 - 2] == 'p')) : (text3 + b("AM", text[num2 - 2] == 'p')));
					break;
				}
				if (text2.Substring(num2, 3) == ".M.")
				{
					num2 += 3;
					text3 = ((A_4 >= 12) ? (text3 + b("P.M.", text[num2 - 4] == 'p')) : (text3 + b("A.M.", text[num2 - 4] == 'p')));
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'Q':
				num2++;
				text3 += (A_2 - 1) / 3 + 1;
				break;
			case 'R':
				num2++;
				if (text2[num2] == 'M')
				{
					num2++;
					text3 += a(text[num2 - 2] == 'r', text[num2 - 1] == 'm', c(A_2));
					break;
				}
				if (text2.Substring(num2, 3) == "RRR")
				{
					num2 += 3;
					text3 += Math.Abs(A_1).ToString(c("0000", flag));
					break;
				}
				if (text2[num2] == 'R')
				{
					num2++;
					text3 += (Math.Abs(A_1) % 100).ToString(c("00", flag));
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'S':
				num2++;
				if (text2.Substring(num2, 4) == "YYYY")
				{
					num2 += 4;
					text3 = ((A_1 >= 0) ? (text3 + ' ') : (text3 + '-'));
					text3 += Math.Abs(A_1).ToString(c("0000", flag));
					break;
				}
				if (text2.Substring(num2, 4) == "SSSS")
				{
					num2 += 4;
					text3 += ((dateTime.Hour * 60 + dateTime.Minute) * 60 + dateTime.Second).ToString(c("00000", flag));
					break;
				}
				if (text2[num2] == 'S')
				{
					num2++;
					text3 += A_6.ToString(c("00", flag));
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'T':
				num2++;
				if (text2.Substring(num2, 2) == "ZH")
				{
					num2 += 2;
					text3 = ((A_8 >= 0 && (A_8 != 0 || A_9 >= 0)) ? (text3 + '+') : (text3 + '-'));
					text3 += Math.Abs(A_8).ToString(c("00", flag));
				}
				else if (text2.Substring(num2, 2) == "ZM")
				{
					num2 += 2;
					text3 += Math.Abs(A_9).ToString(c("00", flag));
				}
				else if (text2.Substring(num2, 2) == "ZR")
				{
					num2 += 2;
					if (A_10 >= 0)
					{
						text3 += aw[A_10].ToUpper();
						break;
					}
					text3 = ((A_8 >= 0 && (A_8 != 0 || A_9 >= 0)) ? (text3 + '+') : (text3 + '-'));
					text3 = text3 + Math.Abs(A_8).ToString("00") + ":" + Math.Abs(A_9).ToString("00");
				}
				else
				{
					if (!(text2.Substring(num2, 2) == "ZD"))
					{
						throw new OracleException(1821, "ORA-01821 date format not recognized");
					}
					num2 += 2;
				}
				break;
			case 'W':
				num2++;
				if (text2[num2] == 'W')
				{
					num2++;
					text3 += calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString(c("00", flag));
				}
				else
				{
					text3 += (A_3 - 1) / 7 + 1;
				}
				break;
			case 'X':
				num2++;
				text3 += NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
				break;
			case 'Y':
				num2++;
				if (text2.Substring(num2, 4) == ",YYY")
				{
					num2 += 4;
					text3 = text3 + (Math.Abs(A_1) / 1000).ToString("0") + "," + (Math.Abs(A_1) % 1000).ToString("000");
				}
				else if (text2.Substring(num2, 3) == "YYY")
				{
					num2 += 3;
					text3 += Math.Abs(A_1).ToString(c("0000", flag));
				}
				else if (text2.Substring(num2, 2) == "YY")
				{
					num2 += 2;
					text3 += (Math.Abs(A_1) % 1000).ToString(c("000", flag));
				}
				else if (text2[num2] == 'Y')
				{
					num2++;
					text3 += (Math.Abs(A_1) % 100).ToString(c("00", flag));
				}
				else
				{
					text3 += (Math.Abs(A_1) % 10).ToString("0");
				}
				break;
			case '"':
				for (num2++; num2 <= length && text[num2] != '"'; num2++)
				{
					text3 += text[num2];
				}
				num2++;
				break;
			default:
			{
				int num3 = " -+/,.;:'".IndexOf(text[num2]);
				if (num3 >= 0)
				{
					text3 += text[num2];
					num2++;
					break;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			}
			}
		}
		return text3;
	}

	public OracleTimeStamp ToUniversalTime()
	{
		if (IsNull)
		{
			return Null;
		}
		switch (ap)
		{
		case OracleDbType.TimeStampTZ:
		{
			_ = TimeZoneOffset;
			short A_ = (short)Year;
			byte A_2 = (byte)Month;
			byte A_3 = (byte)Day;
			byte A_4 = (byte)Hour;
			byte A_5 = (byte)Minute;
			a(ref A_, ref A_2, ref A_3, ref A_4, ref A_5, TimeZoneOffset, A_6: false);
			return new OracleTimeStamp(A_, A_2, A_3, A_4, A_5, aj, ak, "+00-00", ap);
		}
		case OracleDbType.TimeStampLTZ:
			return new OracleTimeStamp(ae, af, ag, ah, ai, aj, ak, OracleDbType.TimeStampTZ);
		default:
			return this;
		}
	}

	internal static string b(string A_0)
	{
		A_0 += "      ";
		string text = A_0.ToUpper(CultureInfo.InvariantCulture);
		string text2 = "";
		bool flag = false;
		int num = 0;
		StringBuilder stringBuilder = new StringBuilder("");
		while (num < A_0.Length)
		{
			text2 = "";
			switch (text[num])
			{
			case 'A':
				num++;
				if (text[num] == 'D')
				{
					num++;
					text2 = "gg";
				}
				else if (text.Substring(num, 3) == ".D.")
				{
					num += 3;
					text2 = "gg";
				}
				else if (text[num] == 'M')
				{
					num++;
					text2 = "tt";
				}
				else if (text.Substring(num, 3) == ".M.")
				{
					num += 3;
					text2 = "tt";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'B':
				num++;
				if (text[num] == 'C')
				{
					num++;
					text2 = "gg";
				}
				else if (text.Substring(num, 3) == ".C.")
				{
					num += 3;
					text2 = "gg";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'C':
				num++;
				if (text[num] == 'C')
				{
					num++;
					text2 = "";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'D':
				num++;
				if (text.Substring(num, 2) == "DD")
				{
					num += 2;
					text2 = "";
				}
				else if (text[num] == 'D')
				{
					num++;
					text2 = "dd";
				}
				else if (text[num] == 'Y')
				{
					num++;
					text2 = "ddd";
				}
				else if (text.Substring(num, 2) == "AY")
				{
					num += 2;
					text2 = "dddd";
				}
				else
				{
					text2 = "";
				}
				break;
			case 'E':
				num++;
				if (text[num] == 'E')
				{
					num++;
					text2 = "gg";
				}
				else
				{
					text2 = "gg";
				}
				break;
			case 'F':
				num++;
				if (text[num] == 'M')
				{
					num++;
					flag = true;
				}
				else if (text[num] == 'F')
				{
					num++;
					if (char.IsDigit(A_0[num]))
					{
						num++;
						switch (Utils.ParseIntWith0(A_0[num].ToString()))
						{
						case 0:
							text2 = "";
							break;
						case 1:
							text2 = "f";
							break;
						case 2:
							text2 = "ff";
							break;
						case 3:
							text2 = "fff";
							break;
						case 4:
							text2 = "ffff";
							break;
						case 5:
							text2 = "fffff";
							break;
						case 6:
							text2 = "ffffff";
							break;
						case 7:
							text2 = "fffffff";
							break;
						default:
							text2 = "fffffff";
							break;
						}
					}
					text2 = "";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'H':
				num++;
				if (text[num] == 'H')
				{
					num++;
					if (A_0.Substring(num, 2) == "24")
					{
						num += 2;
						text2 = "HH";
					}
					else if (A_0.Substring(num, 2) == "12")
					{
						num += 2;
						text2 = "hh";
					}
					else
					{
						text2 = "hh";
					}
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'I':
				num++;
				if (text.Substring(num, 3) == "YYY")
				{
					num += 3;
					text2 = "yyyy";
				}
				else if (text.Substring(num, 2) == "YY")
				{
					num += 2;
					text2 = "yyyy";
				}
				else if (text[num] == 'Y')
				{
					num++;
					text2 = "yy";
				}
				else if ((A_0[num - 1] == 'I' && A_0[num] == 'W') || (A_0[num - 1] == 'i' && A_0[num] == 'w'))
				{
					num++;
					text2 = "";
				}
				else
				{
					text2 = "y";
				}
				break;
			case 'J':
				num++;
				text2 = "";
				break;
			case 'M':
				num++;
				if (text[num] == 'I')
				{
					num++;
					text2 = "mm";
				}
				else if (text[num] == 'M')
				{
					num++;
					text2 = "MM";
				}
				else if (text.Substring(num, 4) == "ONTH")
				{
					num += 4;
					text2 = "MMMM";
				}
				else if (text.Substring(num, 2) == "ON")
				{
					num += 2;
					text2 = "MMM";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'P':
				num++;
				if (text[num] == 'M')
				{
					num++;
					text2 = "tt";
				}
				else if (A_0[num - 1] == 'P' && A_0.Substring(num, 3) == ".M.")
				{
					num += 3;
					text2 = "tt";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'Q':
				num++;
				text2 = "";
				break;
			case 'R':
				num++;
				if (text[num] == 'M')
				{
					num++;
					text2 = "M";
				}
				else if (text.Substring(num, 3) == "RRR")
				{
					num += 3;
					text2 = "yyyy";
				}
				else if (text[num] == 'R')
				{
					num++;
					text2 = "yy";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'S':
				num++;
				if (text.Substring(num, 3) == "SSS")
				{
					num += 3;
					text2 = "";
				}
				else if (text.Substring(num, 2) == "CC")
				{
					num += 2;
					text2 = "";
				}
				else if (text[num] == 'S')
				{
					num++;
					text2 = "ss";
				}
				else if (text.Substring(num, 4) == "YEAR")
				{
					num += 4;
					text2 = "yyyy";
				}
				else if (text.Substring(num, 4) == "YYYY")
				{
					num += 4;
					text2 = "yyyy";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'T':
				num++;
				if (text.Substring(num, 2) == "ZD")
				{
					num += 2;
					text2 = "";
				}
				else if (text.Substring(num, 2) == "ZH")
				{
					num += 2;
					text2 = "zz";
					if (text.Substring(num, 3) == ":ZM")
					{
						num += 3;
						text2 = "zzz";
					}
				}
				else if (text.Substring(num, 2) == "ZM")
				{
					num += 2;
					text2 = "";
				}
				else if (text.Substring(num, 2) == "ZR")
				{
					num += 2;
					text2 = "zzz";
				}
				else
				{
					text2 = A_0[num - 1].ToString();
				}
				break;
			case 'W':
				num++;
				if (text[num] == 'W')
				{
					num++;
					text2 = "";
				}
				else
				{
					text2 = "";
				}
				break;
			case 'X':
				num++;
				text2 = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
				break;
			case 'Y':
				num++;
				if (text.Substring(num, 4) == ",YYY")
				{
					num += 4;
					text2 = "yyyy";
				}
				else if (text.Substring(num, 3) == "YYY")
				{
					num += 3;
					text2 = "yyyy";
				}
				else if (text.Substring(num, 2) == "YY")
				{
					num += 2;
					text2 = "yyyy";
				}
				else if (text[num] == 'Y')
				{
					num++;
					text2 = "yy";
				}
				else
				{
					text2 = "y";
				}
				break;
			case '"':
				num++;
				text2 += "\"";
				for (; num <= A_0.Length && A_0[num] != '"'; num++)
				{
					text2 = text2 + text2 + A_0[num];
				}
				text2 += "\"";
				num++;
				break;
			default:
				text2 = A_0[num].ToString();
				num++;
				break;
			}
			if (flag && text2 != "")
			{
				if (text2 == "dd")
				{
					text2 = "d";
				}
				if (text2 == "MM")
				{
					text2 = "M";
				}
				if (text2 == "yy")
				{
					text2 = "y";
				}
				if (text2 == "hh")
				{
					text2 = "h";
				}
				if (text2 == "HH")
				{
					text2 = "H";
				}
				if (text2 == "mm")
				{
					text2 = "m";
				}
				if (text2 == "ss")
				{
					text2 = "s";
				}
				if (text2 == "zz")
				{
					text2 = "z";
				}
				flag = false;
			}
			stringBuilder.Append(text2);
		}
		stringBuilder.Remove(stringBuilder.Length - 6, 6);
		return stringBuilder.ToString();
	}

	internal static string a(string A_0, bool A_1)
	{
		string text = "";
		int num = 0;
		bool flag = false;
		string text2 = A_0 + "     ";
		while (num < A_0.Length)
		{
			switch (text2[num])
			{
			case 'd':
				num++;
				if (text2[num] == 'd')
				{
					num++;
					if (text2[num] == 'd')
					{
						num++;
						if (text2[num] == 'd')
						{
							num++;
							text += "fmDAYfm";
						}
						else
						{
							text += "DY";
						}
					}
					else
					{
						text += "DD";
					}
				}
				else
				{
					text += "fmDDfm";
				}
				break;
			case 'M':
				num++;
				if (text2[num] == 'M')
				{
					num++;
					if (text2[num] == 'M')
					{
						num++;
						if (text2[num] == 'M')
						{
							num++;
							text += "MONTH";
						}
						else
						{
							text += "MON";
						}
					}
					else
					{
						text += "MM";
					}
				}
				else
				{
					text += "fmMMfm";
				}
				break;
			case 'y':
				num++;
				if (text2[num] == 'y')
				{
					num++;
					if (text2[num] == 'y')
					{
						num++;
						if (text2[num] == 'y')
						{
							num++;
							text += "YYYY";
						}
						else
						{
							text += "YYYY";
						}
					}
					else
					{
						text += "YY";
					}
				}
				else
				{
					text += "fmYYfm";
				}
				break;
			case 'g':
				num++;
				if (text2[num] == 'g')
				{
					num++;
				}
				text += "E";
				break;
			case 't':
				num++;
				if (text2[num] == 't')
				{
					num++;
				}
				text += "AM";
				break;
			case 'h':
				num++;
				if (text2[num] == 'h')
				{
					num++;
					text += "HH12";
				}
				else
				{
					text += "fmHH12fm";
				}
				break;
			case 'H':
				num++;
				if (text2[num] == 'H')
				{
					num++;
					text += "HH24";
				}
				else
				{
					text += "fmHH24fm";
				}
				break;
			case 'm':
				num++;
				if (text2[num] == 'm')
				{
					num++;
					text += "MI";
				}
				else
				{
					text += "fmMIfm";
				}
				break;
			case 's':
			{
				num++;
				string text3 = "";
				if (text2[num] == 's')
				{
					num++;
					text = text + "SS" + text3;
				}
				else
				{
					text = text + "fmSSfm" + text3;
				}
				break;
			}
			case 'f':
			{
				num++;
				int num3 = 1;
				while (text2[num] == 'f' && num3 <= 7)
				{
					num++;
					num3++;
				}
				text = text + "FF" + num3;
				break;
			}
			case 'z':
				num++;
				flag = true;
				if (text2[num] == 'z')
				{
					if (text2[num] == 'z')
					{
						num++;
						text += "TZH:TZM";
					}
					else
					{
						text += "TZH";
					}
				}
				else
				{
					text += "TZH";
				}
				break;
			case '\'':
				text += text2[num];
				num++;
				break;
			case '"':
			{
				int num2 = num;
				num++;
				num = text2.IndexOf(text2[num2], num);
				if (num < 0)
				{
					num += num2 + 1;
					text += text2.Substring(num2, num - num2);
				}
				else
				{
					num = A_0.Length;
					text = text + text2.Substring(num2, num - num2) + "\"";
				}
				break;
			}
			case ':':
				num++;
				text += DateTimeFormatInfo.CurrentInfo.TimeSeparator;
				break;
			case '/':
				num++;
				text += DateTimeFormatInfo.CurrentInfo.DateSeparator;
				break;
			default:
				text += text2[num];
				num++;
				break;
			}
		}
		if (!flag && A_1)
		{
			text += " TZH:TZM";
		}
		return text;
	}

	private static string a(string A_0, int A_1, ref int A_2, int A_3, char A_4)
	{
		return a(A_0, A_1, ref A_2, A_3, A_4, int.MaxValue, A_6: false);
	}

	private static string a(string A_0, int A_1, ref int A_2, int A_3, char A_4, int A_5)
	{
		return a(A_0, A_1, ref A_2, A_3, A_4, A_5, A_6: false);
	}

	private static string a(string A_0, int A_1, ref int A_2, int A_3, char A_4, int A_5, bool A_6)
	{
		string text = "";
		bool flag = " -+/,.;:'".IndexOf(A_4) >= 0;
		if (A_5 > A_3)
		{
			A_5 = A_3;
		}
		if (A_1 - A_2 < A_5)
		{
			throw new OracleException(1840, "ORA-01840 input value not long enough for date format");
		}
		if (A_6 && A_1 - A_2 > 0 && (A_0[A_2] < '0' || A_0[A_2] > '9'))
		{
			throw new OracleException(1858, "ORA-01858 a non-numeric character was found where a numeric was expected");
		}
		for (int num = 0; A_3 <= 0 || num < A_3; num++)
		{
			if (A_1 < A_2)
			{
				break;
			}
			char c2 = A_0[A_2];
			if ((flag && A_4 == c2) || (A_6 && (c2 < '0' || c2 > '9')))
			{
				break;
			}
			text += c2;
			A_2++;
		}
		return text;
	}

	private static int a(string A_0, int A_1, string A_2)
	{
		for (int num = 1; num <= 12; num++)
		{
			if (A_0 == au[num - 1])
			{
				return num;
			}
		}
		throw new OracleException(A_1, A_2);
	}

	public static OracleTimeStamp Parse(string value)
	{
		return Parse(value, OracleDbType.TimeStamp);
	}

	public static bool TryParse(string value, out OracleTimeStamp result)
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

	public static OracleTimeStamp Parse(string value, OracleDbType timeStampType)
	{
		return Parse(value, (timeStampType == OracleDbType.TimeStampTZ) ? OracleGlobalization.ApplicationGlobalization.TimeStampTZFormat : OracleGlobalization.ApplicationGlobalization.TimeStampFormat, timeStampType);
	}

	public static bool TryParse(string value, OracleDbType timeStampType, out OracleTimeStamp result)
	{
		try
		{
			result = Parse(value, timeStampType);
			return true;
		}
		catch (Exception)
		{
			result = Null;
			return false;
		}
	}

	private static int a(short A_0, byte A_1)
	{
		int[] array = new int[12]
		{
			31, 28, 31, 30, 31, 30, 31, 31, 30, 31,
			30, 31
		};
		if (A_1 == 2)
		{
			if (A_0 % 4 == 0)
			{
				return 29;
			}
			return 28;
		}
		return array[A_1 - 1];
	}

	public static OracleTimeStamp Parse(string value, string format, OracleDbType timeStampType)
	{
		a(value, format, timeStampType, out var A_, out var A_2, out var A_3, out var A_4, out var A_5, out var A_6, out var A_7, out var A_8, out var A_9, out var A_10, 9);
		if (timeStampType == OracleDbType.TimeStampLTZ)
		{
			a(ref A_, ref A_2, ref A_3, ref A_4, ref A_5, new TimeSpan(A_8, A_9, 0), A_6: false);
		}
		return new OracleTimeStamp(A_, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9, A_10, timeStampType);
	}

	public static bool TryParse(string value, string format, OracleDbType timeStampType, out OracleTimeStamp result)
	{
		try
		{
			result = Parse(value, format, timeStampType);
			return true;
		}
		catch (Exception)
		{
			result = Null;
			return false;
		}
	}

	internal static void a(string A_0, string A_1, OracleDbType A_2, out short A_3, out byte A_4, out byte A_5, out byte A_6, out byte A_7, out byte A_8, out int A_9, out sbyte A_10, out sbyte A_11, out int A_12, int A_13)
	{
		if (A_2 != OracleDbType.TimeStamp)
		{
			OracleGlobalization oracleGlobalization = OracleGlobalization.ApplicationGlobalization;
			A_10 = oracleGlobalization.TzHour;
			A_11 = oracleGlobalization.TzMinute;
		}
		else
		{
			A_10 = 0;
			A_11 = 0;
			new TimeSpan(0L);
		}
		A_12 = -1;
		string text = A_0.ToUpper(CultureInfo.InvariantCulture);
		int length = text.Length;
		text += "                        ";
		A_1 = A_1.ToUpper(CultureInfo.InvariantCulture);
		int length2 = A_1.Length;
		A_1 += "                        ";
		DateTime now = DateTime.Now;
		A_3 = short.MaxValue;
		A_4 = byte.MaxValue;
		A_5 = byte.MaxValue;
		A_6 = byte.MaxValue;
		A_7 = byte.MaxValue;
		A_8 = byte.MaxValue;
		A_9 = int.MaxValue;
		int num = int.MaxValue;
		int num2 = int.MaxValue;
		string text2 = "";
		string text3 = "";
		byte b2 = 0;
		int num3 = -9999;
		bool flag = true;
		int num4 = 0;
		int A_14 = 0;
		int num5 = 0;
		while (num5 <= length2)
		{
			switch (A_1[num5])
			{
			case 'A':
				num5++;
				if (A_1[num5] == 'D')
				{
					num5++;
					if (num3 == -4713)
					{
						throw new OracleException(1819, "ORA-01819 signed year precludes use of BC/AD");
					}
					num3 = 0;
					text3 = a(text, length, ref A_14, 2, A_1[num5]);
					if (text3 != "BC" && text3 != "AD")
					{
						throw new OracleException(1856, "ORA-01856 BC/B.C. || AD/A.D. required");
					}
					continue;
				}
				if (A_1.Substring(num5, 3) == ".D.")
				{
					num5 += 3;
					if (num3 == -4713)
					{
						throw new OracleException(1819, "ORA-01819 signed year precludes use of BC/AD");
					}
					num3 = 0;
					text3 = a(text, length, ref A_14, 4, A_1[num5]);
					if (text3 != "B.C." && text3 != "B.C.")
					{
						throw new OracleException(1856, "ORA-01856 BC/B.C. || AD/A.D. required");
					}
					continue;
				}
				if (A_1[num5] == 'M')
				{
					num5++;
					if (b2 == 24)
					{
						throw new OracleException(1818, "ORA-01818 'HH24' precludes use of meridian indicator");
					}
					b2 = 12;
					text2 = a(text, length, ref A_14, 2, A_1[num5]);
					if (text2 != "AM" && text2 != "PM")
					{
						throw new OracleException(1855, "ORA-01855 AM/A.M. || PM/P.M. required");
					}
					continue;
				}
				if (A_1.Substring(num5, 3) == ".M.")
				{
					num5 += 3;
					if (b2 == 24)
					{
						throw new OracleException(1818, "ORA-01818 'HH24' precludes use of meridian indicator");
					}
					b2 = 12;
					text2 = a(text, length, ref A_14, 4, A_1[num5]);
					if (text2 != "A.M." && text2 != "P.M.")
					{
						throw new OracleException(1855, "ORA-01855 AM/A.M. || PM/P.M. required");
					}
					continue;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'B':
				num5++;
				if (A_1[num5] == 'C')
				{
					num5++;
					if (num3 == -4713)
					{
						throw new OracleException(1819, "ORA-01819 signed year precludes use of BC/AD");
					}
					num3 = 0;
					text3 = a(text, length, ref A_14, 2, A_1[num5]);
					if (text3 != "BC" && text3 != "AD")
					{
						throw new OracleException(1856, "ORA-01856 BC/B.C. || AD/A.D. required");
					}
					continue;
				}
				if (A_1.Substring(num5, 3) == ".C.")
				{
					num5 += 3;
					if (num3 == -4713)
					{
						throw new OracleException(1819, "ORA-01819 signed year precludes use of BC/AD");
					}
					num3 = 0;
					text3 = a(text, length, ref A_14, 4, A_1[num5]);
					if (text3 != "B.C." && text3 != "B.C.")
					{
						throw new OracleException(1856, "ORA-01856 BC/B.C. || AD/A.D. required");
					}
					continue;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'C':
				num5++;
				if (A_1[num5] == 'C')
				{
					num5++;
					continue;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'D':
				num5++;
				if (A_1.Substring(num5, 2) == "DD")
				{
					num5 += 2;
					num = a(text, length, ref A_14, 3, 1, 366, 1848, "ORA-01848 day of year must be between 1 && 365 (366 for (leap year)");
				}
				else if (A_1[num5] == 'D')
				{
					num5++;
					A_5 = (byte)a(text, length, ref A_14, 2, 1, 31, 1847, "ORA-01847 day of month must be between 1 && last day of month");
				}
				else if (A_1[num5] == 'Y')
				{
					num5++;
					string a_ = a(text, length, ref A_14, 3, A_1[num5]);
					num2 = -1;
					for (int num6 = 0; num6 < 7; num6++)
					{
						if (ar[num6] == a_)
						{
							num2 = num6;
							break;
						}
					}
					if (num2 < 0)
					{
						throw new OracleException(1846, "ORA-01846 not a valid day of the week");
					}
				}
				else if (A_1.Substring(num5, 2) == "AY")
				{
					num5++;
					string a_ = a(text, length, ref A_14, 9, A_1[num5]);
					num2 = -1;
					for (int num6 = 0; num6 < 7; num6++)
					{
						if (((DayOfWeek)num6).ToString() == a_)
						{
							num2 = num6;
							break;
						}
					}
					if (num2 < 0)
					{
						throw new OracleException(1846, "ORA-01846 not a valid day of the week");
					}
				}
				else
				{
					num2 = a(text, length, ref A_14, 1, 1, 7, 1846, "ORA-01846 not a valid day of the week");
				}
				continue;
			case 'F':
				num5++;
				if (A_1[num5] == 'M')
				{
					num5++;
					continue;
				}
				if (A_1[num5] == 'X')
				{
					num5++;
					continue;
				}
				if (A_1[num5] == 'F')
				{
					num5++;
					if (flag)
					{
						int num8 = 9;
						if (A_1[num5] >= '1' && A_1[num5] <= '9')
						{
							num8 = int.Parse(A_1[num5].ToString());
							num5++;
						}
						string a_ = a(text, length, ref A_14, num8, A_1[num5], 0, A_6: true);
						A_9 = int.Parse(a_);
						if (A_9.ToString().Length > num8)
						{
							throw new OracleException(1880, "ORA-01880 the fractional seconds must be between 0 && 999999999");
						}
						A_9 = int.Parse(a_.Substring(0, (a_.Length > A_13) ? A_13 : a_.Length));
						for (int num6 = 1; num6 <= 9 - a_.Length; num6++)
						{
							A_9 *= 10;
						}
					}
					continue;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'H':
				num5++;
				if (A_1[num5] == 'H')
				{
					num5++;
					if (A_1.Substring(num5, 2) == "24")
					{
						num5 += 2;
						if (b2 == 12)
						{
							throw new OracleException(1818, "ORA-01818 'HH24' precludes use of meridian indicator");
						}
						b2 = 24;
						A_6 = (byte)a(text, length, ref A_14, 2, 0, 23, 1849, "ORA-01850 hour must be between 0 && 23");
					}
					else if (A_1.Substring(num5, 2) == "12")
					{
						num5 += 2;
						b2 = 12;
						A_6 = (byte)a(text, length, ref A_14, 2, 1, 12, 1850, "ORA-01849 hour must be between 1 && 12");
					}
					else
					{
						A_6 = (byte)a(text, length, ref A_14, 2, 1, 12, 1850, "ORA-01849 hour must be between 1 && 12");
						b2 = 12;
					}
					continue;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'M':
				num5++;
				if (A_1[num5] == 'I')
				{
					num5++;
					A_7 = (byte)a(text, length, ref A_14, 2, 0, 59, 1851, "ORA-01851 minutes must be between 0 && 59");
					continue;
				}
				if (A_1[num5] == 'M')
				{
					num5++;
					A_4 = (byte)a(text, length, ref A_14, 2, 1, 12, 1843, "ORA-01843 not a valid month");
					continue;
				}
				if (A_1.Substring(num5, 4) == "ONTH")
				{
					num5 += 4;
					string a_ = a(text, length, ref A_14, 9, A_1[num5], 1).ToUpper(CultureInfo.InvariantCulture);
					A_4 = byte.MaxValue;
					for (int num6 = 0; num6 < 12; num6++)
					{
						if (@as[num6].ToString().ToUpper(CultureInfo.InvariantCulture) == a_)
						{
							A_4 = (byte)(num6 + 1);
							break;
						}
					}
					if (A_4 != byte.MaxValue)
					{
						continue;
					}
					throw new OracleException(1843, "ORA-01843 not a valid month");
				}
				if (A_1.Substring(num5, 2) == "ON")
				{
					num5 += 2;
					string a_ = a(text, length, ref A_14, 3, A_1[num5]).ToUpper(CultureInfo.InvariantCulture);
					A_4 = byte.MaxValue;
					for (int num6 = 0; num6 < 12; num6++)
					{
						if (at[num6].ToString().ToUpper(CultureInfo.InvariantCulture) == a_)
						{
							A_4 = (byte)(num6 + 1);
							break;
						}
					}
					if (A_4 != byte.MaxValue)
					{
						continue;
					}
					throw new OracleException(1843, "ORA-01843 not a valid month");
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'P':
				num5++;
				if (A_1[num5] == 'P')
				{
					num5++;
					if (b2 == 24)
					{
						throw new OracleException(1818, "ORA-01818 'HH24' precludes use of meridian indicator");
					}
					b2 = 12;
					text2 = a(text, length, ref A_14, 2, A_1[num5]);
					if (text2 != "AM" && text2 != "PM")
					{
						throw new OracleException(1855, "ORA-01855 AM/A.M. || PM/P.M. required");
					}
					continue;
				}
				if (A_1.Substring(num5, 3) == ".P.")
				{
					num5 += 3;
					if (b2 == 24)
					{
						throw new OracleException(1818, "ORA-01818 'HH24' precludes use of meridian indicator");
					}
					b2 = 12;
					text2 = a(text, length, ref A_14, 3, A_1[num5]);
					if (text2 != "A.M." && text2 != "P.M.")
					{
						throw new OracleException(1855, "ORA-01855 AM/A.M. || PM/P.M. required");
					}
					continue;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'R':
				num5++;
				if (A_1[num5] == 'M')
				{
					num5++;
					string a_ = a(text, length, ref A_14, 4, A_1[num5], 1);
					A_4 = (byte)a(a_, 1843, "ORA-01843 not a valid month");
					continue;
				}
				if (A_1.Substring(num5, 3) == "RRR")
				{
					num5 += 3;
					A_3 = (short)a(text, length, ref A_14, 4, 0, 9999, 1821, "ORA-01821 date format not recognized");
					if (A_3 < 100)
					{
						if (A_3 < 50 && now.Year % 100 >= 50)
						{
							A_3 = (short)((now.Year / 100 + 1) * 100 + A_3);
						}
						else if (A_3 >= 50 && now.Year % 100 < 50)
						{
							A_3 = (short)((now.Year / 100 - 1) * 100 + A_3);
						}
						else
						{
							A_3 = (short)(now.Year / 100 * 100 + A_3);
						}
					}
					continue;
				}
				if (A_1[num5] == 'R')
				{
					num5++;
					A_3 = (short)a(text, length, ref A_14, 4, 0, 9999, 1821, "ORA-01821 date format not recognized");
					if (A_3 < 100)
					{
						if (A_3 < 50 && now.Year % 100 >= 50)
						{
							A_3 = (short)((now.Year / 100 + 1) * 100 + A_3);
						}
						else if (A_3 >= 50 && now.Year % 100 < 50)
						{
							A_3 = (short)((now.Year / 100 - 1) * 100 + A_3);
						}
						else
						{
							A_3 = (short)(now.Year / 100 * 100 + A_3);
						}
					}
					continue;
				}
				throw new OracleException(1821, "ORA-01821 date format not recognized");
			case 'S':
				num5++;
				if (A_1.Substring(num5, 4) == "YYYY")
				{
					num5 += 4;
					A_3 = (short)a(text, length, ref A_14, 4, 0, 9999, 1821, "ORA-01821 date format not recognized");
					if (num3 == 0)
					{
						throw new OracleException(1819, "ORA-01819 signed year precludes use of BC/AD");
					}
					num3 = -4713;
					if (num4 < 0)
					{
						A_3 = (short)(-A_3);
					}
					if (A_3 < -4713)
					{
						throw new OracleException(1841, "ORA-01841 (full) year must be between -4713 && +9999, && not be 0");
					}
				}
				else
				{
					if (A_1[num5] != 'S')
					{
						throw new OracleException(1821, "ORA-01821 date format not recognized");
					}
					num5++;
					A_8 = (byte)a(text, length, ref A_14, 2, 0, 59, 1852, "ORA-01852 seconds must be between 0 && 59");
				}
				continue;
			case 'T':
				num5++;
				if (A_1.Substring(num5, 2) == "ZH")
				{
					num5 += 2;
					if (A_2 != OracleDbType.TimeStampTZ)
					{
						throw new OracleException(1821, "ORA-01821 date format not recognized");
					}
					if (num4 == 0)
					{
						num4 = 1;
					}
					if (text[A_14] == '-')
					{
						num4 = -1;
						A_14++;
					}
					else if (text[A_14] == '+')
					{
						num4 = 1;
						A_14++;
					}
					A_10 = (sbyte)(num4 * a(text, length, ref A_14, 2, -12, 13, 1874, "ORA-01874 time zone hour must be between -12 && 13"));
					if (A_10 < -12)
					{
						throw new OracleException(1874, "ORA-01874 time zone hour must be between -12 && 13");
					}
					num4 = 0;
				}
				else if (A_1.Substring(num5, 2) == "ZM")
				{
					num5 += 2;
					if (A_2 != OracleDbType.TimeStampTZ)
					{
						throw new OracleException(1821, "ORA-01821 date format not recognized");
					}
					if (text[A_14] == '-' || text[A_14] == '+')
					{
						A_14++;
					}
					A_11 = (sbyte)a(text, length, ref A_14, 2, -59, 59, 1875, "ORA-01875 time zone minute must be between -59 && 59");
				}
				else if (A_1.Substring(num5, 2) == "ZR")
				{
					num5 += 2;
					if (A_2 != OracleDbType.TimeStampTZ)
					{
						throw new OracleException(1821, "ORA-01821 date format not recognized");
					}
					int num7 = A_14;
					A_12 = a(a(text, length, ref A_14, 100, ' ', 3));
					if (A_12 >= 0)
					{
						A_10 = ax[A_12];
						A_11 = ay[A_12];
						continue;
					}
					A_14 = num7;
					if (num4 == 0)
					{
						num4 = 1;
					}
					if (text[A_14] == '-')
					{
						A_14++;
						num4 = -1;
					}
					else if (text[A_14] == '+')
					{
						A_14++;
						num4 = 1;
					}
					A_10 = (sbyte)(num4 * a(text, length, ref A_14, 2, -12, 13, 1874, "ORA-01874 time zone hour must be between -12 && 13"));
					if (A_10 < -12)
					{
						throw new OracleException(1874, "ORA-01874 time zone hour must be between -12 && 13");
					}
					num4 = 0;
					string a_ = a(text, length, ref A_14, 1, '?');
					if (a_ != ":")
					{
						throw new OracleException(1821, "ORA-01821 date format not recognized");
					}
					if (text[A_14] == '-' || text[A_14] == '+')
					{
						A_14++;
					}
					A_11 = (sbyte)a(text, length, ref A_14, 2, -59, 59, 1875, "ORA-01875 time zone minute must be between -59 && 59");
				}
				else if (A_1.Substring(num5, 2) == "ZD")
				{
					num5 += 2;
					if (A_2 != OracleDbType.TimeStampLTZ)
					{
						throw new OracleException(1821, "ORA-01821 date format not recognized");
					}
				}
				continue;
			case 'X':
				num5++;
				for (; A_14 <= length && !(text[A_14].ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) && " -+/,.;:'".IndexOf(text[A_14]) > 0; A_14++)
				{
				}
				flag = text[A_14].ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
				if (flag)
				{
					A_14++;
				}
				for (; A_14 <= length && " -+/,.;:'".IndexOf(text[A_14]) > 0; A_14++)
				{
				}
				continue;
			case 'Y':
				num5++;
				if (A_1.Substring(num5, 3) == "YYY")
				{
					num5 += 3;
					A_3 = (short)a(text, length, ref A_14, 4, 0, 9999, 1821, "ORA-01821 date format not recognized");
				}
				else if (A_1.Substring(num5, 2) == "YY")
				{
					num5 += 2;
					A_3 = (short)a(text, length, ref A_14, 3, 0, 9999, 1821, "ORA-01821 date format not recognized");
					A_3 = (short)(now.Year / 1000 * 1000 + A_3);
				}
				else if (A_1[num5] == 'Y')
				{
					num5++;
					A_3 = (short)a(text, length, ref A_14, 4, 0, 9999, 1821, "ORA-01821 date format not recognized");
					if (A_3 < 100)
					{
						A_3 = (short)(now.Year / 100 * 100 + A_3);
					}
				}
				else
				{
					A_3 = (short)a(text, length, ref A_14, 1, 0, 10, 1821, "ORA-01821 date format not recognized");
					A_3 = (short)(now.Year / 10 * 10 + A_3);
				}
				continue;
			case '"':
				num5++;
				while (num5 <= length2 && A_1[num5] != '"' && A_14 <= length)
				{
					if (text[A_14].ToString().ToUpper(CultureInfo.InvariantCulture) != A_1[num5].ToString().ToUpper(CultureInfo.InvariantCulture))
					{
						throw new OracleException(1861, "ORA-01861 literal does not match format string");
					}
					num5++;
					A_14++;
				}
				num5++;
				continue;
			}
			int num9 = " -+/,.;:'".IndexOf(A_1[num5]);
			if (num9 >= 0)
			{
				num5++;
				for (; A_14 <= length && " -+/,.;:'".IndexOf(text[A_14]) >= 0; A_14++)
				{
					num4 = ((text[A_14] == '-') ? (-1) : 0);
				}
				continue;
			}
			throw new OracleException(1821, "ORA-01821 date format not recognized");
		}
		if (A_3 == short.MaxValue)
		{
			A_3 = (short)now.Year;
		}
		else if (text3 == "BC" || text3 == "B.C.")
		{
			A_3 = (short)(-A_3);
		}
		short num10;
		for (num10 = A_3; num10 <= 1; num10 += 4000)
		{
		}
		if (num != int.MaxValue)
		{
			if (num > ((num10 % 4 == 0) ? 366 : 365))
			{
				throw new OracleException(1848, "ORA-01848 day of year must be between 1 && 365 (366 for (leap year)");
			}
			DateTime dateTime = new DateTime(num10, 1, 1) + new TimeSpan(num, 0, 0, 0);
			_ = dateTime.Year;
			byte b3 = (byte)dateTime.Month;
			byte b4 = (byte)dateTime.Day;
			if (A_4 != byte.MaxValue && A_4 != b3)
			{
				throw new OracleException(1833, "ORA-01833 month conflicts with Julian date");
			}
			if (A_5 != byte.MaxValue && A_5 != b4)
			{
				throw new OracleException(1834, "ORA-01834 day of month conflicts with Julian date");
			}
			A_4 = b3;
			A_5 = b4;
		}
		if (A_4 == byte.MaxValue)
		{
			A_4 = (byte)now.Month;
		}
		if (A_5 == byte.MaxValue)
		{
			A_5 = 1;
		}
		if (A_5 > a(num10, A_4))
		{
			throw new OracleException(1839, "ORA-01839 date not valid for (month specified");
		}
		if (num2 != int.MaxValue)
		{
			Calendar calendar = CultureInfo.InvariantCulture.Calendar;
			DateTime dateTime = new DateTime(num10, A_4, A_5);
			if (calendar.GetDayOfWeek(dateTime) != (DayOfWeek)num2)
			{
				throw new OracleException(1835, "ORA-01835 day of week conflicts with Julian date");
			}
		}
		if (A_6 == byte.MaxValue)
		{
			A_6 = 0;
		}
		if (A_7 == byte.MaxValue)
		{
			A_7 = 0;
		}
		if (A_8 == byte.MaxValue)
		{
			A_8 = 0;
		}
		if (A_9 == int.MaxValue)
		{
			A_9 = 0;
		}
		if (b2 == 12)
		{
			if (text2 == "PM" || text2 == "P.M.")
			{
				if (A_6 != 12)
				{
					A_6 += 12;
				}
			}
			else if (A_6 == 12)
			{
				A_6 = 0;
			}
		}
		if (A_10 < 0)
		{
			A_11 = (sbyte)(-A_11);
		}
	}

	public static bool operator ==(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return Equals(value1, value2);
	}

	public static bool operator >(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return GreaterThan(value1, value2);
	}

	public static bool operator >=(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return GreaterThanOrEqual(value1, value2);
	}

	public static bool operator !=(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return NotEquals(value1, value2);
	}

	public static bool operator <(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return LessThan(value1, value2);
	}

	public static bool operator <=(OracleTimeStamp value1, OracleTimeStamp value2)
	{
		return LessThanOrEqual(value1, value2);
	}

	public static OracleTimeStamp operator +(OracleTimeStamp value1, OracleIntervalDS value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		return value1 + (TimeSpan)value2;
	}

	public static OracleTimeStamp operator +(OracleTimeStamp value1, OracleIntervalYM value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		int num = value1.Year;
		int num2 = value1.Month + value2.Months;
		if (num2 > 12 || num2 <= 0)
		{
			num += (num2 - 1) / 12;
			num2 = (num2 - 1) % 12 + 1;
		}
		return new OracleTimeStamp(num, num2, value1.Day, value1.Hour, value1.Minute, value1.Second, value1.Nanosecond, value1.al, value1.am, value1.an, value1.TimeStampType);
	}

	public static OracleTimeStamp operator +(OracleTimeStamp value1, TimeSpan value2)
	{
		if (value1.IsNull)
		{
			return Null;
		}
		int num = 0;
		short num2 = (short)value1.Year;
		while (num2 <= 1)
		{
			num2 += 4000;
			num++;
		}
		DateTime dateTime = new DateTime(num2, value1.Month, value1.Day, value1.Hour, value1.Minute, value1.Second).AddTicks(value1.Nanosecond / 100).Add(value2);
		num2 = (short)dateTime.Year;
		while (num > 0)
		{
			num2 -= 4000;
			num--;
		}
		return new OracleTimeStamp(num2, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, ((int)(dateTime.Ticks % 10000000 * 100) + value1.Nanosecond) & 0x64, value1.al, value1.am, value1.an, value1.TimeStampType);
	}

	public static OracleTimeStamp operator -(OracleTimeStamp value1, OracleIntervalDS value2)
	{
		return value1 + -value2;
	}

	public static OracleTimeStamp operator -(OracleTimeStamp value1, OracleIntervalYM value2)
	{
		return value1 + -value2;
	}

	public static OracleTimeStamp operator -(OracleTimeStamp value1, TimeSpan value2)
	{
		return value1 + -value2;
	}

	XmlSchema IXmlSerializable.GetSchema()
	{
		return null;
	}

	void IXmlSerializable.ReadXml(XmlReader reader)
	{
		if (OracleUtils.a(reader))
		{
			ao = false;
			return;
		}
		string value = reader.ReadElementString();
		if (!TryParse(value, OracleDbType.TimeStamp, out var result))
		{
			result = Parse(value, OracleDbType.TimeStampTZ);
		}
		ao = true;
		ae = result.ae;
		af = result.af;
		ag = result.ag;
		ah = result.ah;
		ai = result.ai;
		aj = result.aj;
		ak = result.ak;
		al = result.al;
		am = result.am;
		an = result.an;
		ap = result.ap;
		aq = result.aq;
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

	internal static int a(string A_0)
	{
		if (az.TryGetValue(A_0, out var value))
		{
			return value;
		}
		return -1;
	}

	internal static int b(int A_0)
	{
		if (a0.TryGetValue(A_0, out var value))
		{
			return value;
		}
		return -1;
	}

	internal static int a(int A_0)
	{
		return av[A_0];
	}

	private static void a()
	{
		lock (typeof(OracleTimeStamp))
		{
			if (aw == null)
			{
				av = new int[377]
				{
					30848, 45184, 62592, 17537, 44160, 6273, 8321, 9345, 7297, 54400,
					61568, 57472, 13441, 129, 45185, 43137, 18562, 59522, 54402, 8323,
					45193, 57474, 64642, 3203, 47233, 48258, 7298, 12418, 13443, 6275,
					23682, 38017, 14466, 24706, 62594, 4227, 11394, 3202, 39041, 53377,
					1154, 28802, 17546, 48265, 58498, 15491, 56449, 45186, 30850, 31874,
					5251, 57473, 25730, 48281, 50305, 49281, 51329, 48257, 9346, 5250,
					34946, 41089, 50313, 55426, 9347, 40065, 52353, 60546, 38018, 131,
					35970, 16514, 13442, 43138, 12419, 59521, 36994, 36993, 44161, 56450,
					39042, 46209, 1155, 63618, 40066, 6274, 64641, 1171, 2179, 61570,
					14467, 39065, 55425, 47234, 130, 33922, 16515, 36996, 64643, 45188,
					2180, 8324, 10380, 11396, 6276, 61571, 54404, 53380, 48260, 50307,
					16517, 10373, 18565, 13445, 17541, 14469, 20613, 11397, 32909, 29829,
					27781, 33925, 32917, 25733, 30853, 34957, 28805, 34949, 31877, 25741,
					32925, 26757, 27789, 29837, 32901, 30861, 31885, 26765, 33933, 1163,
					56458, 61578, 139, 57481, 63625, 64649, 59529, 1162, 55433, 2186,
					64657, 10378, 47237, 2187, 3215, 38041, 38033, 25738, 49285, 45192,
					52365, 37017, 37009, 1152, 1168, 16512, 25728, 26752, 27776, 17536,
					18560, 19584, 20608, 21632, 22656, 23680, 24704, 1200, 1184, 15488,
					6272, 5248, 4224, 3200, 2176, 14464, 13440, 12416, 11392, 10368,
					9344, 8320, 7296, 1216, 12422, 1158, 51333, 28806, 64645, 59533,
					57477, 16518, 2182, 60549, 52357, 134, 62597, 23686, 17542, 24710,
					15494, 28814, 50309, 7302, 20614, 56453, 11398, 18566, 13446, 63621,
					59525, 4230, 3206, 19590, 3222, 28822, 27782, 60545, 17538, 46210,
					2178, 47242, 10370, 63617, 8322, 13454, 47236, 13444, 12420, 57476,
					15492, 14468, 9348, 52355, 51331, 41092, 21636, 16516, 4228, 60555,
					60547, 53379, 39044, 53387, 43140, 29828, 58499, 63619, 52356, 23702,
					5252, 7300, 10372, 49283, 56452, 28804, 62595, 51332, 22660, 23684,
					19588, 132, 140, 55428, 6284, 30852, 27780, 1156, 50308, 49284,
					31876, 56451, 32900, 46212, 17540, 59523, 28830, 58501, 21638, 61573,
					53381, 3214, 55429, 6278, 14470, 28838, 22662, 50317, 50325, 1160,
					1176, 1208, 1192, 1224, 63627, 2199, 14477, 53382, 56454, 57478,
					61574, 62598, 8332, 10388, 34954, 11404, 16527, 57480, 48261, 17554,
					16522, 13450, 39081, 39073, 39049, 23695, 24719, 23687, 24711, 3207,
					34951, 6279, 7303, 10375, 2183, 38023, 13447, 16519, 8327, 39047,
					25735, 26759, 22663, 30855, 29831, 5255, 14471, 30871, 9351, 35975,
					40071, 42119, 14478, 15502, 59531, 40097, 40081, 64651, 17548, 37004,
					23694, 43145, 45201, 46217, 38025, 48273, 37001, 2191, 50321, 53385,
					39057, 40073, 40089, 30863, 1232, 18574, 46213
				};
				aw = new string[377]
				{
					"Africa/Algiers", "Africa/Cairo", "Africa/Casablanca", "Africa/Ceuta", "Africa/Djibouti", "Africa/Freetown", "Africa/Johannesburg", "Africa/Khartoum", "Africa/Mogadishu", "Africa/Nairobi",
					"Africa/Nouakchott", "Africa/Tripoli", "Africa/Tunis", "Africa/Windhoek", "America/Adak", "America/Anchorage", "America/Anguilla", "America/Araguaina", "America/Aruba", "America/Asuncion",
					"America/Atka", "America/Belem", "America/Boa_Vista", "America/Bogota", "America/Boise", "America/Buenos_Aires", "America/Cambridge_Bay", "America/Cancun", "America/Caracas", "America/Cayenne",
					"America/Cayman", "America/Chicago", "America/Chihuahua", "America/Costa_Rica", "America/Cuiaba", "America/Curacao", "America/Dawson", "America/Dawson_Creek", "America/Denver", "America/Detroit",
					"America/Edmonton", "America/El_Salvador", "America/Ensenada", "America/Fort_Wayne", "America/Fortaleza", "America/Godthab", "America/Goose_Bay", "America/Grand_Turk", "America/Guadeloupe", "America/Guatemala",
					"America/Guayaquil", "America/Halifax", "America/Havana", "America/Indiana/Indianapolis", "America/Indiana/Knox", "America/Indiana/Marengo", "America/Indiana/Vevay", "America/Indianapolis", "America/Inuvik", "America/Iqaluit",
					"America/Jamaica", "America/Juneau", "America/Knox_IN", "America/La_Paz", "America/Lima", "America/Los_Angeles", "America/Louisville", "America/Maceio", "America/Managua", "America/Manaus",
					"America/Martinique", "America/Mazatlan", "America/Mexico_City", "America/Miquelon", "America/Montevideo", "America/Montreal", "America/Montserrat", "America/New_York", "America/Nome", "America/Noronha",
					"America/Panama", "America/Phoenix", "America/Porto_Acre", "America/Porto_Velho", "America/Puerto_Rico", "America/Rankin_Inlet", "America/Regina", "America/Rio_Branco", "America/Santiago", "America/Sao_Paulo",
					"America/Scoresbysund", "America/Shiprock", "America/St_Johns", "America/St_Thomas", "America/Swift_Current", "America/Tegucigalpa", "America/Thule", "Asia/Singapore", "Asia/Taipei", "Asia/Tashkent",
					"Asia/Tbilisi", "Asia/Tehran", "Asia/Tel_Aviv", "Asia/Tokyo", "Asia/Ujung_Pandang", "Asia/Urumqi", "Asia/Vladivostok", "Asia/Yakutsk", "Asia/Yekaterinburg", "Asia/Yerevan",
					"Atlantic/Azores", "Atlantic/Bermuda", "Atlantic/Canary", "Atlantic/Faeroe", "Atlantic/Madeira", "Atlantic/Reykjavik", "Atlantic/St_Helena", "Atlantic/Stanley", "Australia/ACT", "Australia/Adelaide",
					"Australia/Brisbane", "Australia/Broken_Hill", "Australia/Canberra", "Australia/Darwin", "Australia/Hobart", "Australia/LHI", "Australia/Lindeman", "Australia/Lord_Howe", "Australia/Melbourne", "Australia/North",
					"Australia/NSW", "Australia/Perth", "Australia/Queensland", "Australia/South", "Australia/Sydney", "Australia/Tasmania", "Australia/Victoria", "Australia/West", "Australia/Yancowinna", "Brazil/Acre",
					"Brazil/DeNoronha", "Brazil/East", "Brazil/West", "Canada/Atlantic", "Canada/Central", "Canada/East-Saskatchewan", "Canada/Eastern", "Canada/Mountain", "Canada/Newfoundland", "Canada/Pacific",
					"Canada/Saskatchewan", "Canada/Yukon", "CET", "Chile/Continental", "Chile/EasterIsland", "CST", "CST6CDT", "Cuba", "EET", "Egypt",
					"Eire", "EST", "EST5EDT", "Etc/GMT", "Etc/GMT+0", "Etc/GMT+1", "Etc/GMT+10", "Etc/GMT+11", "Etc/GMT+12", "Etc/GMT+2",
					"Etc/GMT+3", "Etc/GMT+4", "Etc/GMT+5", "Etc/GMT+6", "Etc/GMT+7", "Etc/GMT+8", "Etc/GMT+9", "Etc/GMT0", "Etc/GMT-0", "Etc/GMT-1",
					"Etc/GMT-10", "Etc/GMT-11", "Etc/GMT-12", "Etc/GMT-13", "Etc/GMT-14", "Etc/GMT-2", "Etc/GMT-3", "Etc/GMT-4", "Etc/GMT-5", "Etc/GMT-6",
					"Etc/GMT-7", "Etc/GMT-8", "Etc/GMT-9", "Etc/Greenwich", "Europe/Amsterdam", "Europe/Athens", "Europe/Belfast", "Europe/Belgrade", "Europe/Berlin", "Europe/Bratislava",
					"Europe/Brussels", "Europe/Bucharest", "Europe/Budapest", "Europe/Copenhagen", "Europe/Dublin", "Europe/Gibraltar", "Europe/Helsinki", "Europe/Istanbul", "Europe/Kaliningrad", "Europe/Kiev",
					"Europe/Lisbon", "Europe/Ljubljana", "Europe/London", "Europe/Luxembourg", "Europe/Madrid", "Europe/Minsk", "Europe/Monaco", "Europe/Moscow", "Europe/Oslo", "Europe/Paris",
					"Europe/Prague", "Europe/Riga", "Europe/Rome", "Europe/Samara", "Europe/San_Marino", "Europe/Sarajevo", "Europe/Simferopol", "America/Thunder_Bay", "America/Tijuana", "America/Tortola",
					"America/Vancouver", "America/Virgin", "America/Whitehorse", "America/Winnipeg", "America/Yellowknife", "Arctic/Longyearbyen", "Asia/Aden", "Asia/Almaty", "Asia/Amman", "Asia/Anadyr",
					"Asia/Aqtau", "Asia/Aqtobe", "Asia/Baghdad", "Asia/Bahrain", "Asia/Baku", "Asia/Bangkok", "Asia/Beirut", "Asia/Bishkek", "Asia/Calcutta", "Asia/Chongqing",
					"Asia/Chungking", "Asia/Dacca", "Asia/Damascus", "Asia/Dhaka", "Asia/Dubai", "Asia/Gaza", "Asia/Harbin", "Asia/Hong_Kong", "Asia/Irkutsk", "Asia/Istanbul",
					"Asia/Jakarta", "Asia/Jayapura", "Asia/Jerusalem", "Asia/Kabul", "Asia/Kamchatka", "Asia/Karachi", "Asia/Kashgar", "Asia/Krasnoyarsk", "Asia/Kuala_Lumpur", "Asia/Kuching",
					"Asia/Kuwait", "Asia/Macao", "Asia/Macau", "Asia/Magadan", "Asia/Makassar", "Asia/Manila", "Asia/Muscat", "Asia/Nicosia", "Asia/Novosibirsk", "Asia/Omsk",
					"Asia/Qatar", "Asia/Rangoon", "Asia/Riyadh", "Asia/Saigon", "Asia/Seoul", "Asia/Shanghai", "Europe/Skopje", "Europe/Sofia", "Europe/Stockholm", "Europe/Tallinn",
					"Europe/Tirane", "Europe/Vatican", "Europe/Vienna", "Europe/Vilnius", "Europe/Warsaw", "Europe/Zagreb", "Europe/Zurich", "GB", "GB-Eire", "GMT",
					"GMT+0", "GMT0", "GMT-0", "Greenwich", "Hongkong", "HST", "Iceland", "Indian/Chagos", "Indian/Christmas", "Indian/Cocos",
					"Indian/Mayotte", "Indian/Reunion", "Iran", "Israel", "Jamaica", "Japan", "Kwajalein", "Libya", "MET", "Mexico/BajaNorte",
					"Mexico/BajaSur", "Mexico/General", "MST", "MST7MDT", "Navajo", "NZ", "NZ-CHAT", "Pacific/Auckland", "Pacific/Chatham", "Pacific/Easter",
					"Pacific/Fakaofo", "Pacific/Fiji", "Pacific/Gambier", "Pacific/Guam", "Pacific/Honolulu", "Pacific/Johnston", "Pacific/Kiritimati", "Pacific/Kwajalein", "Pacific/Marquesas", "Pacific/Midway",
					"Pacific/Niue", "Pacific/Norfolk", "Pacific/Noumea", "Pacific/Pago_Pago", "Pacific/Pitcairn", "Pacific/Rarotonga", "Pacific/Saipan", "Pacific/Samoa", "Pacific/Tahiti", "Pacific/Tongatapu",
					"Pacific/Wake", "Pacific/Wallis", "Poland", "Portugal", "PRC", "PST", "PST8PDT", "ROC", "ROK", "Singapore",
					"Turkey", "US/Alaska", "US/Aleutian", "US/Arizona", "US/Central", "US/East-Indiana", "US/Eastern", "US/Hawaii", "US/Indiana-Starke", "US/Michigan",
					"US/Mountain", "US/Pacific", "US/Pacific-New", "US/Samoa", "UTC", "W-SU", "WET"
				};
				ax = new sbyte[377]
				{
					1, 2, 0, 1, 3, 0, 2, 2, 3, 3,
					0, 2, 1, 2, -10, -9, -4, -3, -4, -3,
					-10, -3, -4, -5, -7, -3, -7, -6, -4, -3,
					-5, -6, -7, -6, -4, -4, -8, -7, -7, -5,
					-7, -6, -8, -5, -3, -3, -4, -5, -4, -6,
					-5, -4, -5, -5, -5, -5, -5, -5, -7, -5,
					-5, -9, -5, -4, -5, -8, -5, -3, -6, -4,
					-4, -7, -6, -3, -3, -5, -4, -5, -9, -2,
					-5, -7, -5, -4, -4, -6, -6, -5, -3, -2,
					-1, -7, -3, -4, -6, -6, -4, 8, 8, 5,
					4, 3, 2, 9, 8, 8, 10, 9, 5, 4,
					-1, -4, 0, 0, 0, 0, 0, -3, 11, 10,
					10, 10, 11, 9, 11, 11, 10, 11, 11, 9,
					11, 8, 10, 10, 11, 11, 11, 8, 10, -5,
					-2, -2, -4, -4, -6, -6, -5, -7, -3, -8,
					-6, -8, 1, -3, -5, -6, -6, -5, 2, 2,
					0, -5, -5, 0, 0, -1, -10, -11, -12, -2,
					-3, -4, -5, -6, -7, -8, -9, 0, 0, 1,
					10, 11, 12, 13, 14, 2, 3, 4, 5, 6,
					7, 8, 9, 0, 1, 2, 0, 1, 1, 1,
					1, 2, 1, 1, 0, 1, 2, 2, 2, 2,
					0, 1, 0, 1, 1, 2, 1, 3, 1, 1,
					1, 2, 1, 4, 1, 1, 2, -5, -8, -4,
					-8, -4, -8, -6, -7, 1, 3, 6, 2, 12,
					4, 5, 3, 3, 4, 7, 2, 5, 5, 8,
					8, 6, 2, 6, 4, 2, 8, 8, 8, 2,
					7, 9, 2, 4, 12, 5, 8, 7, 8, 8,
					3, 8, 8, 11, 8, 8, 4, 2, 6, 6,
					3, 6, 3, 7, 9, 8, 1, 2, 1, 2,
					1, 1, 1, 2, 1, 1, 1, 0, 0, 0,
					0, 0, 0, 0, 8, -10, 0, 5, 7, 6,
					3, 4, 3, 2, -5, 9, 12, 2, 1, -8,
					-7, -6, -7, -7, -7, 13, 13, 13, 13, -5,
					-10, 12, -9, 10, -10, -10, 14, 12, -9, -11,
					-11, 11, 11, -11, -8, -10, 10, -11, -10, 13,
					12, 12, 1, 0, 8, -8, -8, 8, 9, 8,
					2, -9, -10, -7, -6, -5, -5, -10, -5, -5,
					-7, -8, -8, -11, 0, 3, 0
				};
				ay = new sbyte[377]
				{
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 30, 0, 0, 0, 0, 0, 0, 0,
					0, 30, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 30,
					0, 30, 0, 30, 0, 0, 0, 0, 0, 30,
					0, 0, 0, 30, 0, 0, 0, 0, 30, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 30, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 30, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 30, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 30, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 30,
					0, 0, 30, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 45, 0, 45, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 30, 0,
					0, 30, 0, 0, 30, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0
				};
				az = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
				a0 = new Dictionary<int, int>();
				for (int num = 0; num < aw.Length; num++)
				{
					az[aw[num]] = num;
					a0[av[num]] = num;
				}
			}
		}
	}
}
