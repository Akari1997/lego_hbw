using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Devart.Data.Oracle;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[Obsolete("This class is designed for compatibility with OracleClient only.")]
public struct OracleMonthSpan : IComparable, INullable
{
	public static readonly OracleMonthSpan MaxValue;

	public static readonly OracleMonthSpan MinValue;

	public static readonly OracleMonthSpan Null;

	internal OracleIntervalYM a;

	public bool IsNull => a.IsNull;

	public int Value
	{
		get
		{
			if (IsNull)
			{
				throw new InvalidOperationException();
			}
			return (int)a.Value;
		}
	}

	internal OracleMonthSpan(OracleIntervalYM A_0)
	{
		OracleUtils.a(typeof(OracleMonthSpan));
		a = A_0;
	}

	public OracleMonthSpan(int months)
		: this(new OracleIntervalYM(months))
	{
	}

	public OracleMonthSpan(int years, int months)
		: this(new OracleIntervalYM(years, months))
	{
	}

	public OracleMonthSpan(OracleMonthSpan from)
		: this(from.a)
	{
	}

	public int CompareTo(object obj)
	{
		if (obj == null || (object)obj.GetType() != typeof(OracleMonthSpan))
		{
			throw new ArgumentException();
		}
		OracleMonthSpan oracleMonthSpan = (OracleMonthSpan)obj;
		if (IsNull)
		{
			if (!oracleMonthSpan.IsNull)
			{
				return -1;
			}
			return 0;
		}
		if (oracleMonthSpan.IsNull)
		{
			return 1;
		}
		return a.CompareTo(oracleMonthSpan.a);
	}

	public override bool Equals(object value)
	{
		if (value == null || (object)value.GetType() != typeof(OracleMonthSpan))
		{
			return false;
		}
		OracleMonthSpan oracleMonthSpan = (OracleMonthSpan)value;
		return (this == oracleMonthSpan).IsTrue;
	}

	public override int GetHashCode()
	{
		return a.GetHashCode();
	}

	public static OracleMonthSpan Parse(string s)
	{
		return new OracleMonthSpan(OracleIntervalYM.Parse(s));
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return "Null";
		}
		return a.Value.ToString(CultureInfo.CurrentCulture);
	}

	public static OracleBoolean Equals(OracleMonthSpan x, OracleMonthSpan y)
	{
		return x == y;
	}

	public static OracleBoolean GreaterThan(OracleMonthSpan x, OracleMonthSpan y)
	{
		return x > y;
	}

	public static OracleBoolean GreaterThanOrEqual(OracleMonthSpan x, OracleMonthSpan y)
	{
		return x >= y;
	}

	public static OracleBoolean LessThan(OracleMonthSpan x, OracleMonthSpan y)
	{
		return x < y;
	}

	public static OracleBoolean LessThanOrEqual(OracleMonthSpan x, OracleMonthSpan y)
	{
		return x <= y;
	}

	public static OracleBoolean NotEquals(OracleMonthSpan x, OracleMonthSpan y)
	{
		return x != y;
	}

	public static explicit operator int(OracleMonthSpan x)
	{
		return x.Value;
	}

	public static explicit operator OracleMonthSpan(string x)
	{
		return Parse(x);
	}

	public static OracleBoolean operator ==(OracleMonthSpan x, OracleMonthSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a == y.a);
	}

	public static OracleBoolean operator >(OracleMonthSpan x, OracleMonthSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a > y.a);
	}

	public static OracleBoolean operator >=(OracleMonthSpan x, OracleMonthSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a >= y.a);
	}

	public static OracleBoolean operator <(OracleMonthSpan x, OracleMonthSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a < y.a);
	}

	public static OracleBoolean operator <=(OracleMonthSpan x, OracleMonthSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a <= y.a);
	}

	public static OracleBoolean operator !=(OracleMonthSpan x, OracleMonthSpan y)
	{
		if (x.IsNull || y.IsNull)
		{
			return OracleBoolean.Null;
		}
		return new OracleBoolean(x.a != y.a);
	}
}
