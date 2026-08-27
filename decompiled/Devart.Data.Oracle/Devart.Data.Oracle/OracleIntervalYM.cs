using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Devart.Common;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(bc))]
public struct OracleIntervalYM : IComparable, INullable, IXmlSerializable
{
	private const int m_a = 9;

	private const int b = 6;

	private bool c;

	private int d;

	private int e;

	public static readonly OracleIntervalYM MaxValue;

	public static readonly OracleIntervalYM MinValue;

	public static readonly OracleIntervalYM Null;

	public static readonly OracleIntervalYM Zero;

	public bool IsNull => !c;

	public int Months
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

	public double TotalYears
	{
		get
		{
			if (IsNull)
			{
				return 0.0;
			}
			return (double)d + (double)e / 12.0;
		}
	}

	public long Value
	{
		get
		{
			if (IsNull)
			{
				return 0L;
			}
			return d * 12 + e;
		}
	}

	public int Years
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

	public OracleIntervalYM(long totalMonths)
	{
		c = true;
		d = Math.Abs((int)(totalMonths / 12));
		e = Math.Abs((int)(totalMonths % 12));
		if (totalMonths < 0)
		{
			d = -d;
			e = -e;
		}
	}

	public OracleIntervalYM(double totalYears)
	{
		c = true;
		double num = Math.Abs(totalYears);
		d = (int)Math.Floor(num);
		e = (int)Math.Floor((num - Math.Floor(num)) * 12.0);
		if (totalYears < 0.0)
		{
			d = -d;
			e = -e;
		}
	}

	public OracleIntervalYM(int years, int months)
	{
		c = true;
		if (years < 0 || months < 0)
		{
			d = -Math.Abs(years);
			e = -Math.Abs(months);
		}
		else
		{
			d = Math.Abs(years);
			e = Math.Abs(months);
		}
	}

	internal OracleIntervalYM(IntPtr A_0, v A_1)
	{
		if (A_1.h().d() < 9000000)
		{
			throw new InvalidOperationException(Devart.Common.al.a("NeedOCI9Interface"));
		}
		if ((int)A_0 != 0)
		{
			HandleRef hndl = A_1.h().h();
			HandleRef err = A_1.h().k();
			Oci oci = A_1.h().j();
			A_1.h().c(oci.OCIIntervalGetYearMonth(hndl, err, out var yr, out var mnth, A_0));
			c = true;
			d = yr;
			e = mnth;
		}
		else
		{
			c = false;
			d = 0;
			e = 0;
		}
	}

	static OracleIntervalYM()
	{
		MaxValue = new OracleIntervalYM(999999999, 11);
		MinValue = new OracleIntervalYM(-999999999, -11);
		Null = default(OracleIntervalYM);
		Zero = new OracleIntervalYM(0, 0);
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
		if ((object)obj.GetType() != typeof(OracleIntervalYM))
		{
			throw new ArgumentException();
		}
		OracleIntervalYM oracleIntervalYM = (OracleIntervalYM)obj;
		if (IsNull)
		{
			if (!oracleIntervalYM.IsNull)
			{
				return -1;
			}
			return 0;
		}
		if (oracleIntervalYM.IsNull)
		{
			return 1;
		}
		if (oracleIntervalYM.Years > d)
		{
			return -1;
		}
		if (oracleIntervalYM.Years < d)
		{
			return 1;
		}
		if (oracleIntervalYM.Months > e)
		{
			return -1;
		}
		if (oracleIntervalYM.Months < e)
		{
			return 1;
		}
		return 0;
	}

	public override bool Equals(object value)
	{
		if ((object)value.GetType() != typeof(OracleIntervalYM))
		{
			return false;
		}
		return CompareTo(value) == 0;
	}

	public static bool Equals(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return value1.CompareTo(value2) == 0;
	}

	public static bool GreaterThan(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return value1.CompareTo(value2) == 1;
	}

	public static bool GreaterThanOrEqual(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return value1.CompareTo(value2) >= 0;
	}

	public static bool LessThan(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return value1.CompareTo(value2) == -1;
	}

	public static bool LessThanOrEqual(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return value1.CompareTo(value2) <= 0;
	}

	public static bool NotEquals(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return value1.CompareTo(value2) != 0;
	}

	public override int GetHashCode()
	{
		if (IsNull)
		{
			return DBNull.Value.GetHashCode();
		}
		return Value.GetHashCode();
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return "";
		}
		bool flag = d < 0 || e < 0;
		string text = Math.Abs(d).ToString("00") + "-" + Math.Abs(e).ToString("00");
		if (flag)
		{
			return "-" + text;
		}
		return "+" + text;
	}

	internal static bool a(string A_0, ref int A_1)
	{
		while (A_1 < A_0.Length && A_0[A_1] == ' ')
		{
			A_1++;
		}
		bool result = false;
		if (A_0[A_1] == '-' || A_0[A_1] == '+')
		{
			if (A_0[A_1] == '-')
			{
				result = true;
			}
			A_1++;
		}
		return result;
	}

	internal static int a(string A_0, ref int A_1, string A_2, int A_3, string A_4, bool A_5)
	{
		while (A_1 < A_0.Length && A_0[A_1] == ' ')
		{
			A_1++;
		}
		string text;
		if (A_2 != "")
		{
			int num;
			for (num = A_1; num < A_0.Length && A_0[num].ToString() != A_2; num++)
			{
			}
			if (num >= A_0.Length)
			{
				throw new OracleException(1867, Devart.Common.al.a("ORA01867"));
			}
			text = A_0.Substring(A_1, num - 1 - (A_1 - 1)).Trim();
			A_1 = num + 1;
		}
		else
		{
			text = A_0.Substring(A_1, A_0.Length - A_1);
			A_1 = A_0.Length;
		}
		if (text == "" || text[1] < '0' || text[1] > '9')
		{
			throw new OracleException(1867, Devart.Common.al.a("ORA01867"));
		}
		int num2;
		try
		{
			if (A_5)
			{
				if (text.Length > 9)
				{
					text = text.Substring(0, 9);
				}
				num2 = int.Parse(text);
				for (int num3 = 1; num3 <= 9 - text.Length; num3++)
				{
					num2 *= 10;
				}
			}
			else
			{
				num2 = int.Parse(text);
			}
		}
		catch
		{
			throw new OracleException(1867, Devart.Common.al.a("ORA01867"));
		}
		if (num2 > A_3 && A_4 != "")
		{
			throw new Exception(A_4);
		}
		return num2;
	}

	public static OracleIntervalYM Parse(string value)
	{
		int num = 9;
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
		int A_ = 1;
		bool flag = a(value, ref A_);
		int num4 = a(value, ref A_, "-", num2, Devart.Common.al.a("ORA01873"), A_5: false);
		int num5 = a(value, ref A_, "", 11, Devart.Common.al.a("ORA01843"), A_5: false);
		if (flag)
		{
			num4 = -num4;
			num5 = -num5;
		}
		return new OracleIntervalYM(num4, num5);
	}

	public static bool TryParse(string value, out OracleIntervalYM result)
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

	public static OracleIntervalYM Add(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		return Add(value1.Years, value1.Months, value1.Years >= 0 && value1.Months >= 0, value2.Years, value2.Months, value2.Years >= 0 && value2.Months >= 0);
	}

	public static OracleIntervalYM Add(int year1, int month1, bool isPositive1, int year2, int month2, bool isPositive2)
	{
		bool flag;
		int num;
		int num2;
		if ((isPositive1 && isPositive2) || (!isPositive1 && !isPositive2))
		{
			flag = isPositive1;
			num = month1 + month2;
			num2 = year1 + year1 + num / 12;
			if (num2 < -999999999)
			{
				return MinValue;
			}
			if (num2 > 999999999)
			{
				return MaxValue;
			}
		}
		else
		{
			bool flag2 = false;
			if (year1 > year2)
			{
				flag2 = true;
			}
			else if (month1 > month2)
			{
				flag2 = true;
			}
			if (flag2)
			{
				flag = isPositive1;
				num = month1 - month2;
				num2 = num / 12;
				num2 = num2 + year1 - year2;
			}
			else
			{
				flag = isPositive2;
				num = month2 - month1;
				num2 = num / 12;
				num2 = num2 + year2 - year1;
			}
		}
		if (!flag)
		{
			num2 = -num2;
			num = -num;
		}
		return new OracleIntervalYM(num2, num);
	}

	private static OracleIntervalYM a(OracleIntervalYM A_0, double A_1)
	{
		if ((double)A_0.Years * A_1 >= 1000000000.0)
		{
			return MaxValue;
		}
		if ((double)A_0.Years * A_1 <= -1000000000.0)
		{
			return MinValue;
		}
		int num = Math.Abs(A_0.Years);
		int num2 = Math.Abs(A_0.Months);
		bool flag = A_1 < 0.0 == (A_0.Years < 0 || A_0.Months < 0);
		A_1 = Math.Abs(A_1);
		long num3 = 0L;
		int num4 = 0;
		long num5 = (long)Math.Floor(A_1);
		if (num5 != 0)
		{
			long num6 = num2 * num5;
			long num7 = num6 / 12;
			num4 = (int)(num6 % 12);
			num3 = num * num5 + num7;
			if (num3 >= 1000000000)
			{
				return MaxValue;
			}
			if (num3 <= -1000000000)
			{
				return MinValue;
			}
		}
		double num8 = 0.0;
		double num9 = 0.0;
		double num10 = A_1 - Math.Floor(A_1);
		if (num10 != 0.0)
		{
			num8 = (double)num * num10;
			double num11 = (double)num - Math.Floor(num8) / num10;
			num8 = Math.Floor(num8);
			num9 = ((double)num2 + num11 * 12.0) * num10;
			num11 = (double)num2 - Math.Floor(num9) / num10;
			num9 = Math.Floor(num9);
		}
		if (!flag)
		{
			num3 = -num3;
			num8 = 0.0 - num8;
			num4 = -num4;
			num9 = 0.0 - num9;
		}
		return new OracleIntervalYM((int)num3 + (int)num8, num4 + (int)num9);
	}

	public static OracleIntervalYM Divide(OracleIntervalYM value, OracleNumber divisor)
	{
		if (value.IsNull || divisor.IsNull)
		{
			return Null;
		}
		return a(value, 1.0 / OracleNumber.b(divisor));
	}

	public static OracleIntervalYM Multiply(OracleIntervalYM value, OracleNumber multiplier)
	{
		if (value.IsNull || multiplier.IsNull)
		{
			return Null;
		}
		return a(value, OracleNumber.b(multiplier));
	}

	public static OracleIntervalYM Subtract(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		return new OracleIntervalYM(value1.Value - value2.Value);
	}

	public static bool operator ==(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return Equals(value1, value2);
	}

	public static bool operator >(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return GreaterThan(value1, value2);
	}

	public static bool operator >=(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return GreaterThanOrEqual(value1, value2);
	}

	public static bool operator !=(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return NotEquals(value1, value2);
	}

	public static bool operator <(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return LessThan(value1, value2);
	}

	public static bool operator <=(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return LessThanOrEqual(value1, value2);
	}

	public static OracleIntervalYM operator +(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return Add(value1, value2);
	}

	public static OracleIntervalYM operator -(OracleIntervalYM value1)
	{
		return new OracleIntervalYM(-value1.Years, -value1.Months);
	}

	public static OracleIntervalYM operator -(OracleIntervalYM value1, OracleIntervalYM value2)
	{
		return Subtract(value1, value2);
	}

	public static OracleIntervalYM operator *(OracleIntervalYM value1, int multiplier)
	{
		return a(value1, multiplier);
	}

	public static OracleIntervalYM operator /(OracleIntervalYM value1, int divisor)
	{
		return a(value1, 1.0 / (double)divisor);
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
			c = false;
			return;
		}
		OracleIntervalYM oracleIntervalYM = Parse(reader.ReadElementString());
		c = true;
		d = oracleIntervalYM.d;
		e = oracleIntervalYM.e;
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
