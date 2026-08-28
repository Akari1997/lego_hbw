using System;
using System.Globalization;

namespace Devart.Data.Oracle;

[Obsolete("This class is designed for compatibility with OracleClient only.")]
public struct OracleBoolean : IComparable
{
	private bool? a;

	public static readonly OracleBoolean False = new OracleBoolean(value: false);

	public static readonly OracleBoolean Null = new OracleBoolean(null);

	public static readonly OracleBoolean One = new OracleBoolean(value: true);

	public static readonly OracleBoolean True = new OracleBoolean(value: true);

	public static readonly OracleBoolean Zero = new OracleBoolean(value: false);

	public bool IsFalse
	{
		get
		{
			if (a.HasValue)
			{
				return !a.Value;
			}
			return false;
		}
	}

	public bool IsNull => !a.HasValue;

	public bool IsTrue
	{
		get
		{
			if (a.HasValue)
			{
				return a.Value;
			}
			return false;
		}
	}

	public bool Value
	{
		get
		{
			if (!a.HasValue)
			{
				throw new InvalidOperationException();
			}
			return a.Value;
		}
	}

	public OracleBoolean(bool value)
		: this((bool?)value)
	{
	}

	public OracleBoolean(int value)
		: this(value != 0)
	{
	}

	internal OracleBoolean(bool? A_0)
	{
		OracleUtils.a(typeof(OracleBoolean));
		a = A_0;
	}

	public int CompareTo(object obj)
	{
		if (obj == null || (object)obj.GetType() != typeof(OracleBoolean))
		{
			throw new ArgumentException("obj");
		}
		OracleBoolean oracleBoolean = (OracleBoolean)obj;
		obj = oracleBoolean.a;
		if (!a.HasValue)
		{
			if (oracleBoolean.a.HasValue)
			{
				return -1;
			}
			return 0;
		}
		if (!oracleBoolean.a.HasValue)
		{
			return 1;
		}
		return a.Value.CompareTo(oracleBoolean.Value);
	}

	public override bool Equals(object value)
	{
		if (!(value is OracleBoolean oracleBoolean))
		{
			return false;
		}
		if (oracleBoolean.IsNull || IsNull)
		{
			if (oracleBoolean.IsNull)
			{
				return IsNull;
			}
			return false;
		}
		return (this == oracleBoolean).Value;
	}

	public override int GetHashCode()
	{
		return a.GetHashCode();
	}

	public static OracleBoolean Parse(string s)
	{
		try
		{
			return new OracleBoolean(int.Parse(s, CultureInfo.InvariantCulture));
		}
		catch (Exception ex)
		{
			Type type = ex.GetType();
			if ((object)type != typeof(ArgumentNullException) && (object)type != typeof(FormatException) && (object)type != typeof(OverflowException))
			{
				throw ex;
			}
			return new OracleBoolean(bool.Parse(s));
		}
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return "Null";
		}
		return a.Value.ToString(CultureInfo.CurrentCulture);
	}

	public static OracleBoolean And(OracleBoolean x, OracleBoolean y)
	{
		return x & y;
	}

	public static OracleBoolean Equals(OracleBoolean x, OracleBoolean y)
	{
		return x == y;
	}

	public static OracleBoolean NotEquals(OracleBoolean x, OracleBoolean y)
	{
		return x != y;
	}

	public static OracleBoolean OnesComplement(OracleBoolean x)
	{
		return ~x;
	}

	public static OracleBoolean Or(OracleBoolean x, OracleBoolean y)
	{
		return x | y;
	}

	public static OracleBoolean Xor(OracleBoolean x, OracleBoolean y)
	{
		return x ^ y;
	}

	public static implicit operator OracleBoolean(bool x)
	{
		return new OracleBoolean(x);
	}

	public static explicit operator OracleBoolean(string x)
	{
		return Parse(x);
	}

	public static explicit operator OracleBoolean(OracleNumber x)
	{
		return new OracleBoolean(x != 0);
	}

	public static explicit operator bool(OracleBoolean x)
	{
		return x.Value;
	}

	public static OracleBoolean op_LogicalNot(OracleBoolean x)
	{
		return ~x;
	}

	public static OracleBoolean operator ~(OracleBoolean x)
	{
		if (x.IsNull)
		{
			return x;
		}
		return new OracleBoolean(!x.Value);
	}

	public static bool operator true(OracleBoolean x)
	{
		return x.IsTrue;
	}

	public static bool operator false(OracleBoolean x)
	{
		return x.IsFalse;
	}

	public static OracleBoolean operator &(OracleBoolean x, OracleBoolean y)
	{
		if (x.IsNull || y.IsNull)
		{
			return Null;
		}
		return new OracleBoolean(x.Value & y.Value);
	}

	public static OracleBoolean operator ==(OracleBoolean x, OracleBoolean y)
	{
		if (x.IsNull || y.IsNull)
		{
			return Null;
		}
		return new OracleBoolean(x.Value == y.Value);
	}

	public static OracleBoolean operator !=(OracleBoolean x, OracleBoolean y)
	{
		if (x.IsNull || y.IsNull)
		{
			return Null;
		}
		return new OracleBoolean(x.Value != y.Value);
	}

	public static OracleBoolean operator |(OracleBoolean x, OracleBoolean y)
	{
		if (x.IsNull || y.IsNull)
		{
			return Null;
		}
		return new OracleBoolean(x.Value | y.Value);
	}

	public static OracleBoolean operator ^(OracleBoolean x, OracleBoolean y)
	{
		if (x.IsNull || y.IsNull)
		{
			return Null;
		}
		return new OracleBoolean(x.Value ^ y.Value);
	}
}
