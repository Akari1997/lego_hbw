using System;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

namespace Devart.Data.Oracle;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[Obsolete("This class is designed for compatibility with OracleClient only.")]
public struct OracleTimeSpan : IComparable, INullable
{
	internal OracleIntervalDS a;

	public static readonly OracleTimeSpan MaxValue;

	public static readonly OracleTimeSpan MinValue;

	public static readonly OracleTimeSpan Null;

	public bool IsNull => a.IsNull;

	public TimeSpan Value
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

	public int Days => a.Days;

	public int Hours => a.Hours;

	public int Minutes => a.Minutes;

	public int Seconds => a.Seconds;

	public int Milliseconds => a.Nanoseconds / 1000;

	public OracleTimeSpan(TimeSpan ts)
		: this(new OracleIntervalDS(ts))
	{
	}

	public OracleTimeSpan(long ticks)
		: this(new OracleIntervalDS(new TimeSpan(ticks)))
	{
	}

	public OracleTimeSpan(int hours, int minutes, int seconds)
		: this(new OracleIntervalDS(0, hours, minutes, seconds, 0))
	{
	}

	public OracleTimeSpan(int days, int hours, int minutes, int seconds)
		: this(new OracleIntervalDS(days, hours, minutes, seconds, 0))
	{
	}

	public OracleTimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		: this(new OracleIntervalDS(days, hours, minutes, seconds, milliseconds))
	{
	}

	public OracleTimeSpan(OracleTimeSpan from)
		: this(from.a)
	{
	}

	internal OracleTimeSpan(OracleIntervalDS A_0)
	{
		OracleUtils.a(typeof(OracleTimeSpan));
		a = A_0;
	}

	public int CompareTo(object obj)
	{
		if (obj == null || (object)obj.GetType() != typeof(OracleTimeSpan))
		{
			throw new ArgumentException();
		}
		obj = ((OracleTimeSpan)obj).a;
		return a.CompareTo(obj);
	}

	public override bool Equals(object value)
	{
		if (value != null && (object)value.GetType() == typeof(OracleTimeSpan))
		{
			value = ((OracleTimeSpan)value).a;
		}
		return a.Equals(value);
	}

	public override int GetHashCode()
	{
		return a.GetHashCode();
	}

	public static OracleTimeSpan Parse(string s)
	{
		return new OracleTimeSpan(OracleIntervalDS.Parse(s));
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return "Null";
		}
		return a.ToString();
	}

	public static OracleBoolean Equals(OracleTimeSpan x, OracleTimeSpan y)
	{
		return x == y;
	}

	public static OracleBoolean GreaterThan(OracleTimeSpan x, OracleTimeSpan y)
	{
		return x > y;
	}

	public static OracleBoolean GreaterThanOrEqual(OracleTimeSpan x, OracleTimeSpan y)
	{
		return x >= y;
	}

	public static OracleBoolean LessThan(OracleTimeSpan x, OracleTimeSpan y)
	{
		return x < y;
	}

	public static OracleBoolean LessThanOrEqual(OracleTimeSpan x, OracleTimeSpan y)
	{
		return x <= y;
	}

	public static OracleBoolean NotEquals(OracleTimeSpan x, OracleTimeSpan y)
	{
		return x != y;
	}

	public static explicit operator TimeSpan(OracleTimeSpan x)
	{
		return x.Value;
	}

	public static explicit operator OracleTimeSpan(string x)
	{
		return Parse(x);
	}

	public static OracleBoolean operator ==(OracleTimeSpan x, OracleTimeSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.Value == y.Value);
	}

	public static OracleBoolean operator >(OracleTimeSpan x, OracleTimeSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.Value > y.Value);
	}

	public static OracleBoolean operator >=(OracleTimeSpan x, OracleTimeSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.Value >= y.Value);
	}

	public static OracleBoolean operator <(OracleTimeSpan x, OracleTimeSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.Value < y.Value);
	}

	public static OracleBoolean operator <=(OracleTimeSpan x, OracleTimeSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.Value <= y.Value);
	}

	public static OracleBoolean operator !=(OracleTimeSpan x, OracleTimeSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.Value != y.Value);
	}
}
