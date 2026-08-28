using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Devart.Data.Oracle;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[Obsolete("This class is designed for compatibility with OracleClient only.")]
public struct OracleDateTime : IComparable, INullable
{
	internal OracleTimeStamp a;

	public static readonly OracleDateTime MaxValue;

	public static readonly OracleDateTime MinValue;

	public static readonly OracleDateTime Null;

	public bool IsNull => a.IsNull;

	public DateTime Value
	{
		get
		{
			if (IsNull)
			{
				throw new InvalidOperationException();
			}
			return a.Value;
		}
	}

	public int Year => a.Year;

	public int Month => a.Month;

	public int Day => a.Day;

	public int Hour => a.Hour;

	public int Minute => a.Minute;

	public int Second => a.Second;

	public int Millisecond => a.Nanosecond / 1000;

	internal OracleDateTime(OracleTimeStamp A_0)
	{
		OracleUtils.a(typeof(OracleDateTime));
		a = A_0;
	}

	public OracleDateTime(DateTime dt)
		: this(new OracleTimeStamp(dt))
	{
	}

	public OracleDateTime(long ticks)
		: this(new OracleTimeStamp(new DateTime(ticks)))
	{
	}

	public OracleDateTime(int year, int month, int day)
		: this(new OracleTimeStamp(year, month, day, OracleDbType.TimeStamp))
	{
	}

	public OracleDateTime(int year, int month, int day, Calendar calendar)
		: this(new OracleTimeStamp(new DateTime(year, month, day, calendar)))
	{
	}

	public OracleDateTime(int year, int month, int day, int hour, int minute, int second)
		: this(new OracleTimeStamp(year, month, day, hour, minute, second))
	{
	}

	public OracleDateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		: this(new OracleTimeStamp(new DateTime(year, month, day, hour, minute, second, calendar)))
	{
	}

	public OracleDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		: this(new OracleTimeStamp(year, month, day, hour, minute, second, millisecond * 1000))
	{
	}

	public OracleDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		: this(new OracleTimeStamp(new DateTime(year, month, day, hour, minute, second, millisecond, calendar)))
	{
	}

	public OracleDateTime(OracleDateTime from)
		: this(from.a)
	{
	}

	public int CompareTo(object obj)
	{
		if (obj == null || (object)obj.GetType() != typeof(OracleDateTime))
		{
			throw new ArgumentException();
		}
		obj = ((OracleDateTime)obj).a;
		return a.CompareTo(obj);
	}

	public override bool Equals(object value)
	{
		if (value != null && (object)value.GetType() == typeof(OracleDateTime))
		{
			value = ((OracleDateTime)value).a;
		}
		return a.Equals(value);
	}

	public override int GetHashCode()
	{
		return a.GetHashCode();
	}

	public static OracleDateTime Parse(string s)
	{
		return new OracleDateTime(OracleTimeStamp.Parse(s));
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return "Null";
		}
		return a.Value.ToString((IFormatProvider?)null);
	}

	public static OracleBoolean Equals(OracleDateTime x, OracleDateTime y)
	{
		return x == y;
	}

	public static OracleBoolean GreaterThan(OracleDateTime x, OracleDateTime y)
	{
		return x > y;
	}

	public static OracleBoolean GreaterThanOrEqual(OracleDateTime x, OracleDateTime y)
	{
		return x >= y;
	}

	public static OracleBoolean LessThan(OracleDateTime x, OracleDateTime y)
	{
		return x < y;
	}

	public static OracleBoolean LessThanOrEqual(OracleDateTime x, OracleDateTime y)
	{
		return x <= y;
	}

	public static OracleBoolean NotEquals(OracleDateTime x, OracleDateTime y)
	{
		return x != y;
	}

	public static explicit operator DateTime(OracleDateTime x)
	{
		return x.Value;
	}

	public static explicit operator OracleDateTime(string x)
	{
		return Parse(x);
	}

	public static OracleBoolean operator ==(OracleDateTime x, OracleDateTime y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a == y.a);
	}

	public static OracleBoolean operator >(OracleDateTime x, OracleDateTime y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a > y.a);
	}

	public static OracleBoolean operator >=(OracleDateTime x, OracleDateTime y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a >= y.a);
	}

	public static OracleBoolean operator <(OracleDateTime x, OracleDateTime y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a < y.a);
	}

	public static OracleBoolean operator <=(OracleDateTime x, OracleDateTime y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a <= y.a);
	}

	public static OracleBoolean operator !=(OracleDateTime x, OracleDateTime y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a != y.a);
	}
}
