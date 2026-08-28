using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Devart.Common;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(a1))]
public struct OracleNumber : IComparable, INullable, IFormattable, IXmlSerializable, IConvertible
{
	internal byte[] a = new byte[22];

	private static byte[] m_b;

	private static byte[] m_c;

	private static byte[] m_d;

	private static byte[] e;

	public static readonly OracleNumber MaxPrecision;

	public static readonly OracleNumber MaxScale;

	public static readonly OracleNumber MaxValue;

	public static readonly OracleNumber MinScale;

	public static readonly OracleNumber MinValue;

	public static readonly OracleNumber Null;

	public static readonly OracleNumber One;

	public static readonly OracleNumber Pi;

	public static readonly OracleNumber Zero;

	public byte[] BinData
	{
		get
		{
			if (IsNull)
			{
				return null;
			}
			return (byte[])this.a.Clone();
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
			byte[] array = new byte[this.a[0]];
			Buffer.BlockCopy(this.a, 1, array, 0, array.Length);
			return array;
		}
	}

	public bool IsNull => this.a == null;

	public decimal Value
	{
		get
		{
			if (IsNull)
			{
				return 0m;
			}
			return (decimal)this;
		}
	}

	public OracleNumber(decimal value)
		: this(A_0: false)
	{
		this.a = OracleNumberUtils.a(value);
	}

	public OracleNumber(double value)
		: this(A_0: false)
	{
		this.a[0] = OracleNumberUtils.a(value, this.a, 1);
	}

	public OracleNumber(float value)
		: this(double.Parse(value.ToString()))
	{
	}

	public OracleNumber(int value)
		: this(A_0: false)
	{
		if (value == int.MinValue)
		{
			this.a[0] = OracleNumberUtils.a((ulong)Math.Abs((long)value), this.a, 1, value > 0);
		}
		else
		{
			this.a[0] = OracleNumberUtils.a((uint)Math.Abs(value), this.a, 1, value > 0);
		}
	}

	public OracleNumber(long value)
		: this(A_0: false)
	{
		this.a[0] = OracleNumberUtils.a((ulong)Math.Abs(value), this.a, 1, value > 0);
	}

	public OracleNumber(byte[] value)
		: this(value, A_1: true)
	{
	}

	internal OracleNumber(byte[] A_0, bool A_1)
		: this(A_0: false)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException();
		}
		if (A_1)
		{
			if (A_0.Length > 22)
			{
				throw new ArgumentException(Devart.Common.al.a("ValueLengthMustBe22"));
			}
			Buffer.BlockCopy(A_0, 0, this.a, 0, A_0.Length);
		}
		else
		{
			this.a[0] = (byte)A_0.Length;
			Buffer.BlockCopy(A_0, 0, this.a, 1, A_0.Length);
		}
	}

	static OracleNumber()
	{
		OracleNumber.m_b = new byte[22]
		{
			20, 255, 100, 100, 100, 100, 100, 100, 100, 100,
			100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
			100, 0
		};
		OracleNumber.m_c = new byte[22]
		{
			21, 0, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 102
		};
		OracleNumber.m_d = new byte[22]
		{
			21, 193, 4, 15, 16, 93, 66, 36, 90, 80,
			33, 39, 47, 27, 44, 39, 33, 80, 51, 29,
			85, 21
		};
		e = new byte[22]
		{
			11, 202, 19, 45, 68, 45, 8, 38, 10, 56,
			17, 17, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0
		};
		MaxPrecision = new OracleNumber(38);
		MaxScale = new OracleNumber(127);
		MaxValue = new OracleNumber(OracleNumber.m_b);
		MinScale = new OracleNumber(-84);
		MinValue = new OracleNumber(OracleNumber.m_c);
		Null = default(OracleNumber);
		One = new OracleNumber(1);
		Pi = new OracleNumber(OracleNumber.m_d);
		Zero = new OracleNumber(0);
	}

	private OracleNumber(bool A_0)
	{
	}

	internal static OracleNumber a(byte[] A_0, int A_1)
	{
		byte[] array = new byte[22];
		Buffer.BlockCopy(A_0, A_1, array, 0, 22);
		return new OracleNumber(array);
	}

	public static int ToInt32(OracleNumber val)
	{
		if (val.IsNull)
		{
			return 0;
		}
		int A_;
		return Convert.ToInt32(OracleNumberUtils.a(val.a, 1, (int)val.a[0], out A_)) * A_;
	}

	public static long ToInt64(OracleNumber val)
	{
		if (val.IsNull)
		{
			return 0L;
		}
		long A_;
		return Convert.ToInt64(OracleNumberUtils.a(val.a, 1, (int)val.a[0], out A_)) * A_;
	}

	public static float ToFloat(OracleNumber val)
	{
		if (val.IsNull)
		{
			return 0f;
		}
		return OracleNumberUtils.b(val.a, 1, val.a[0]);
	}

	public static double ToDouble(OracleNumber val)
	{
		if (val.IsNull)
		{
			return 0.0;
		}
		return OracleNumberUtils.a(val.a, 1, val.a[0]);
	}

	public static decimal ToDecimal(OracleNumber val)
	{
		if (val.IsNull)
		{
			return 0m;
		}
		return OracleNumberUtils.d(val.a, 0);
	}

	public static OracleNumber Abs(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		if ((value.a[0] == 1 && value.a[1] == 128) || (value.a[0] == 2 && value.a[1] == byte.MaxValue && value.a[1] == 101) || (value.a[0] == 1 && value.a[1] == 0) || (value.a[1] & -128) != 0)
		{
			return new OracleNumber(value.a);
		}
		byte[] array = new byte[22];
		int num = ((value.a[1] - 1 != 20 || value.a[value.a[1]] == 102) ? (value.a[0] - 1) : value.a[0]);
		array[0] = (byte)num;
		array[1] = (byte)(~value.a[1]);
		for (int num2 = 2; num2 <= num; num2++)
		{
			array[num2] = (byte)(102 - value.a[num2]);
		}
		return new OracleNumber(array);
	}

	public static OracleNumber Asin(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberArcSin(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Acos(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberArcCos(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Add(OracleNumber value1, OracleNumber value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberAdd(aa2.k(), value1.a, value2.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Atan(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberArcTan(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Atan2(OracleNumber value1, OracleNumber value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberArcTan2(aa2.k(), value1.a, value2.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Ceiling(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberCeil(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	private static void a(byte[] A_0, out int A_1, out int A_2, out int A_3)
	{
		byte b2 = A_0[0];
		if (b2 == 0 || (A_0[0] == 1 && A_0[1] == 128) || (A_0[0] == 2 && A_0[1] == byte.MaxValue && A_0[1] == 101) || (A_0[0] == 1 && A_0[1] == 0))
		{
			A_1 = 0;
			A_2 = 0;
			A_3 = 0;
			return;
		}
		if ((A_0[1] & -128) != 0)
		{
			A_2 = (sbyte)((A_0[1] & -129) - 65);
			A_3 = 1;
		}
		else
		{
			A_3 = -1;
			A_2 = (sbyte)((~A_0[1] & 0xFFFFFF7Fu) - 65);
			if (b2 - 1 != 20 || A_0[b2] == 102)
			{
				b2--;
			}
		}
		if (A_2 > 0 && A_2 <= 14 && b2 - 1 < A_2)
		{
			A_1 = (byte)A_2;
		}
		else
		{
			A_1 = (byte)(b2 - 1);
		}
	}

	private static void a(byte[] A_0, int A_1, out byte[] A_2)
	{
		bool flag = (A_0[1] & -128) != 0;
		byte b2 = A_0[0];
		if (!flag && (b2 - 1 != 20 || A_0[b2] == 102))
		{
			b2--;
		}
		A_2 = new byte[A_1];
		if (flag)
		{
			for (int num = 0; num < b2 - 1; num++)
			{
				A_2[num] = (byte)(A_0[num + 2] - 1);
			}
		}
		else
		{
			for (int num = 0; num < b2 - 1; num++)
			{
				A_2[num] = (byte)(101 - A_0[num + 2]);
			}
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
		if ((object)obj.GetType() != typeof(OracleNumber))
		{
			throw new ArgumentException();
		}
		OracleNumber oracleNumber = (OracleNumber)obj;
		if (IsNull)
		{
			if (!oracleNumber.IsNull)
			{
				return -1;
			}
			return 0;
		}
		if (oracleNumber.IsNull)
		{
			return 1;
		}
		a(BinData, out var A_, out var A_2, out var A_3);
		a(oracleNumber.BinData, out var A_4, out var A_5, out var A_6);
		if (A_3 > A_6)
		{
			return 1;
		}
		if (A_3 < A_6)
		{
			return -1;
		}
		if (A_2 > A_5)
		{
			return A_3;
		}
		if (A_2 < A_5)
		{
			return -1 * A_3;
		}
		a(this.a, A_, out var A_7);
		a(oracleNumber.a, A_4, out var A_8);
		int num = A_7.Length;
		if (A_8.Length < num)
		{
			num = A_8.Length;
		}
		for (int num2 = 0; num2 < num; num2++)
		{
			if (A_7[num2] > A_8[num2])
			{
				return A_3;
			}
			if (A_7[num2] < A_8[num2])
			{
				return -1 * A_3;
			}
		}
		if (A_7.Length > A_8.Length)
		{
			return A_3;
		}
		if (A_7.Length < A_8.Length)
		{
			return -1 * A_3;
		}
		return 0;
	}

	public override bool Equals(object value)
	{
		if (value == null || value == DBNull.Value)
		{
			return IsNull;
		}
		if ((object)value.GetType() != typeof(OracleNumber))
		{
			return false;
		}
		OracleNumber oracleNumber = (OracleNumber)value;
		if (IsNull)
		{
			if (!oracleNumber.IsNull)
			{
				return false;
			}
			return true;
		}
		if (oracleNumber.IsNull)
		{
			return false;
		}
		if (this.a[0] != oracleNumber.a[0])
		{
			return false;
		}
		for (int num = 1; num <= this.a[0]; num++)
		{
			if (this.a[num] != oracleNumber.a[num])
			{
				return false;
			}
		}
		return true;
	}

	public static OracleNumber Cos(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberCos(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Cosh(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberHypCos(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Divide(OracleNumber value1, OracleNumber value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		byte[] array = new byte[22];
		b(value1.a, value2.a, array);
		return new OracleNumber(array);
	}

	private static void b(byte[] A_0, byte[] A_1, byte[] A_2)
	{
		aa aa2 = aa.g();
		aa2.c(aa2.j().OCINumberDiv(aa2.k(), A_0, A_1, A_2));
	}

	public static bool Equals(OracleNumber value1, OracleNumber value2)
	{
		return value1.CompareTo(value2) == 0;
	}

	public static bool GreaterThan(OracleNumber value1, OracleNumber value2)
	{
		return value1.CompareTo(value2) == 1;
	}

	public static bool GreaterThanOrEqual(OracleNumber value1, OracleNumber value2)
	{
		return value1.CompareTo(value2) >= 0;
	}

	public static bool LessThan(OracleNumber value1, OracleNumber value2)
	{
		return value1.CompareTo(value2) == -1;
	}

	public static bool LessThanOrEqual(OracleNumber value1, OracleNumber value2)
	{
		return value1.CompareTo(value2) <= 0;
	}

	public static bool NotEquals(OracleNumber value1, OracleNumber value2)
	{
		return value1.CompareTo(value2) != 0;
	}

	public static OracleNumber Exp(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberExp(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Floor(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberFloor(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public override int GetHashCode()
	{
		if (this.a == null)
		{
			return DBNull.Value.GetHashCode();
		}
		int num = int.MaxValue;
		for (int num2 = 0; num2 <= 4; num2++)
		{
			int num3 = num2 * 4;
			num ^= this.a[num3] + (this.a[1 + num3] << 8) + (this.a[2 + num3] << 16) + (this.a[3 + num3] << 24);
		}
		return num ^ (this.a[20] + this.a[21] << 8);
	}

	public static OracleNumber Log(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberLn(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Log(OracleNumber value, int logBase)
	{
		return Log(value, new OracleNumber(logBase));
	}

	public static OracleNumber Log(OracleNumber value, OracleNumber logBase)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberLog(aa2.k(), logBase.a, value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Log10(OracleNumber value)
	{
		return Log(value, 10);
	}

	public static OracleNumber Max(OracleNumber value1, OracleNumber value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		if (value1 > value2)
		{
			return value1;
		}
		return value2;
	}

	public static OracleNumber Min(OracleNumber value1, OracleNumber value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		if (value1 > value2)
		{
			return value2;
		}
		return value1;
	}

	public static OracleNumber Modulo(OracleNumber value1, OracleNumber value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		byte[] array = new byte[22];
		a(value1.a, value2.a, array);
		return new OracleNumber(array);
	}

	private static void a(byte[] A_0, byte[] A_1, byte[] A_2)
	{
		aa aa2 = aa.g();
		aa2.c(aa2.j().OCINumberMod(aa2.k(), A_0, A_1, A_2));
	}

	public static OracleNumber Multiply(OracleNumber value1, OracleNumber value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberMul(aa2.k(), value1.a, value2.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Negate(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberNeg(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	private static string b(string A_0)
	{
		if (Utils.IsEmpty(A_0))
		{
			return string.Empty;
		}
		char[] array = new char[A_0.Length];
		bool flag = A_0[0] == '+' || A_0[0] == '-';
		bool flag2 = false;
		bool flag3 = false;
		int num;
		if (flag)
		{
			array[0] = 'S';
			num = 1;
		}
		else
		{
			num = 0;
		}
		OracleGlobalization oracleGlobalization = OracleGlobalization.ApplicationGlobalization;
		char c2 = oracleGlobalization.NumericCharacters[0];
		char c3 = oracleGlobalization.NumericCharacters[1];
		int num2 = 64;
		for (; num < A_0.Length; num++)
		{
			if (A_0[num] == c2 && !flag2)
			{
				array[num] = 'D';
				flag2 = true;
				continue;
			}
			if (A_0[num] == c3 && flag3 && !flag2)
			{
				array[num] = 'G';
				continue;
			}
			if (A_0[num] == 'e' || A_0[num] == 'E')
			{
				if (flag)
				{
					return 'S' + "9D99999999999999999999999999999999999999999EEEE";
				}
				return "9D99999999999999999999999999999999999999999EEEE";
			}
			array[num] = '9';
			flag3 = true;
		}
		if (num > num2)
		{
			if (flag)
			{
				return new string(array, 0, num2);
			}
			return new string(array, 0, num2 - 1);
		}
		return new string(array);
	}

	private static OracleNumber a(string A_0)
	{
		A_0 = A_0.Trim();
		int num = 0;
		int num2 = A_0.Length - 1;
		int num3 = -1;
		int num4 = 1;
		if (A_0[0] == '+')
		{
			num4 = 1;
			num++;
		}
		else if (A_0[0] == '-')
		{
			num4 = -1;
			num++;
		}
		OracleGlobalization oracleGlobalization = OracleGlobalization.ApplicationGlobalization;
		char c2 = oracleGlobalization.NumericCharacters[0];
		for (int num5 = num; num5 < A_0.Length && A_0[num5] == '0'; num5++)
		{
			num++;
		}
		for (int num6 = num; num6 <= num2; num6++)
		{
			if (A_0[num6] == c2)
			{
				num3 = num6;
				break;
			}
		}
		for (int num7 = num; num7 < A_0.Length && (A_0[num7] == '0' || A_0[num7] == c2); num7++)
		{
			num++;
		}
		if (num3 != -1)
		{
			int num8 = num2;
			while (num8 >= 0 && (A_0[num8] == '0' || A_0[num8] == c2))
			{
				num2--;
				num8--;
			}
		}
		if (num > num2 || (num == num2 && (A_0[num] == '0' || A_0[num] == c2)))
		{
			return Zero;
		}
		if (num3 == num2)
		{
			num2--;
		}
		if (num3 == num)
		{
			num++;
		}
		if (num3 == -1)
		{
			num3 = num2 + 1;
		}
		byte[] array = new byte[20];
		int num9 = 0;
		int num10 = (((num <= num3 || (num - num3) % 2 <= 0) && (num >= num3 || (num3 - num) % 2 != 0)) ? 1 : 0);
		int num11 = 0;
		int num12 = 0;
		for (int num13 = num; num13 <= num2; num13++)
		{
			if (num3 != num13)
			{
				char c3 = A_0[num13];
				num12++;
				num9 = ((num10 != 0) ? (num9 * 10 + byte.Parse(c3.ToString())) : byte.Parse(c3.ToString()));
				if (num12 > 38)
				{
					throw new OverflowException();
				}
				if (num10 != 0)
				{
					array[num11++] = (byte)num9;
				}
				num10 = 1 - num10;
			}
		}
		if (num10 != 0)
		{
			array[num11++] = (byte)(num9 * 10);
		}
		while (num11 - 1 >= 0 && array[num11 - 1] == 0)
		{
			num11--;
		}
		sbyte b2 = (sbyte)((num3 - num - 1) / 2);
		byte[] array2 = new byte[22];
		if (num4 == 1)
		{
			array2[0] = (byte)(num11 + 1);
			array2[1] = (byte)(b2 + 128 + 65);
			for (int num14 = 0; num14 < num11; num14++)
			{
				array2[num14 + 2] = (byte)(array[num14] + 1);
			}
		}
		else
		{
			if (num11 < 20)
			{
				array2[0] = (byte)(num11 + 2);
			}
			else
			{
				array2[0] = (byte)(num11 + 1);
			}
			array2[1] = (byte)(~(b2 + 128 + 65));
			int num15;
			for (num15 = 0; num15 < num11; num15++)
			{
				array2[num15 + 2] = (byte)(101 - array[num15]);
			}
			if (num15 < 20)
			{
				array2[num15 + 2] = 102;
			}
		}
		return new OracleNumber(array2);
	}

	public static OracleNumber Parse(string value)
	{
		try
		{
			return Parse(value, b(value));
		}
		catch (Exception)
		{
			if (double.TryParse(value, out var result))
			{
				return new OracleNumber(result);
			}
			throw;
		}
	}

	public static bool TryParse(string value, out OracleNumber result)
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

	public static OracleNumber Parse(string value, string format)
	{
		if (Utils.IsEmpty(value))
		{
			return Null;
		}
		aa aa2 = null;
		if (aa.f())
		{
			aa2 = aa.g();
		}
		if (aa2 == null)
		{
			return a(value);
		}
		OracleGlobalization oracleGlobalization = OracleGlobalization.ApplicationGlobalization;
		string numericCharacters = oracleGlobalization.NumericCharacters;
		numericCharacters = numericCharacters.Replace("'", "''");
		string text = $"NLS_NUMERIC_CHARACTERS='{numericCharacters}' NLS_CURRENCY='{oracleGlobalization.Currency}' NLS_ISO_CURRENCY='{oracleGlobalization.ISOCurrency}'";
		Encoding encoding = aa2.o();
		byte[] bytes = encoding.GetBytes(value);
		byte[] bytes2 = encoding.GetBytes(format);
		byte[] bytes3 = encoding.GetBytes(text);
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberFromText(aa2.k(), bytes, (uint)bytes.Length, bytes2, (uint)bytes2.Length, bytes3, (uint)bytes3.Length, array));
		return new OracleNumber(array);
	}

	public static bool TryParse(string value, string format, out OracleNumber result)
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

	public static OracleNumber Pow(OracleNumber value, int power)
	{
		return Pow(value, new OracleNumber(power));
	}

	public static OracleNumber Pow(OracleNumber value, OracleNumber power)
	{
		if (value.IsNull || power.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberPower(aa2.k(), value.a, power.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Round(OracleNumber value, int position)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberRound(aa2.k(), value.a, position, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Shift(OracleNumber value, int digits)
	{
		if (value.IsNull)
		{
			return Null;
		}
		long value2 = (long)Math.Pow(10.0, Math.Abs(digits));
		OracleNumber value3 = new OracleNumber(value2);
		if (digits < 0)
		{
			return Divide(value, value3);
		}
		return Multiply(value, value3);
	}

	public static int Sign(OracleNumber value)
	{
		if (value.IsNull)
		{
			return 0;
		}
		aa aa2 = aa.g();
		if (aa2.d() >= 8010000)
		{
			aa2.c(aa2.j().OCINumberSign(aa2.k(), value.a, out var result));
			return result;
		}
		if (value < 0)
		{
			return -1;
		}
		if (value == 0)
		{
			return 0;
		}
		return 1;
	}

	public static OracleNumber Sin(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberSin(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Sinh(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberHypSin(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Sqrt(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberSqrt(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Subtract(OracleNumber value1, OracleNumber value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberSub(aa2.k(), value1.a, value2.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Tan(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberTan(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public static OracleNumber Tanh(OracleNumber value)
	{
		if (value.IsNull)
		{
			return Null;
		}
		aa aa2 = aa.g();
		byte[] array = new byte[22];
		aa2.c(aa2.j().OCINumberHypTan(aa2.k(), value.a, array));
		return new OracleNumber(array);
	}

	public override string ToString()
	{
		if (OracleUtils.UseOCINumberFormating)
		{
			return ToString("TM9", OracleGlobalization.ApplicationGlobalization);
		}
		return ToString("", CultureInfo.CurrentCulture);
	}

	public string ToString(string format)
	{
		if (OracleUtils.UseOCINumberFormating)
		{
			return ToString(format, OracleGlobalization.ApplicationGlobalization);
		}
		return ToString(format, CultureInfo.CurrentCulture);
	}

	public string ToString(IFormatProvider provider)
	{
		return ToString("", provider);
	}

	public string ToString(string format, IFormatProvider provider)
	{
		if (IsNull)
		{
			return string.Empty;
		}
		return OracleNumberUtils.a(this.a, 0, format, provider);
	}

	public string ToString(string format, OracleGlobalization globalization)
	{
		if (IsNull)
		{
			return string.Empty;
		}
		aa aa2 = null;
		if (aa.f())
		{
			aa2 = aa.g();
		}
		if (aa2 == null)
		{
			return OracleNumberUtils.c(this.a, 0);
		}
		uint buf_size = 128u;
		byte[] array = new byte[buf_size];
		string text = ((!(format == "TM9")) ? string.Format("NLS_NUMERIC_CHARACTERS='{0}' NLS_CURRENCY='{1}' NLS_ISO_CURRENCY='{2}' NLS_DUAL_CURRENCY='{3}'", new object[4] { globalization.NumericCharacters, globalization.Currency, globalization.ISOCurrency, globalization.DualCurrency }) : string.Format("NLS_NUMERIC_CHARACTERS='{0}'", globalization.NumericCharacters, globalization.Currency));
		Encoding encoding = aa2.o();
		byte[] bytes = encoding.GetBytes(text);
		byte[] bytes2 = encoding.GetBytes(format);
		aa2.c(aa2.j().OCINumberToText(aa2.k(), this.a, bytes2, (uint)bytes2.Length, bytes, (uint)bytes.Length, ref buf_size, array));
		return encoding.GetString(array, 0, (int)buf_size);
	}

	public static OracleNumber Truncate(OracleNumber value, int position)
	{
		if (value.IsNull)
		{
			return Null;
		}
		byte[] array = new byte[22];
		a(value.a, position, array);
		return new OracleNumber(array);
	}

	private static void a(byte[] A_0, int A_1, byte[] A_2)
	{
		aa aa2 = aa.g();
		aa2.c(aa2.j().OCINumberTrunc(aa2.k(), A_0, A_1, A_2));
	}

	public static implicit operator OracleNumber(decimal value)
	{
		return new OracleNumber(value);
	}

	public static implicit operator OracleNumber(int value)
	{
		return new OracleNumber(value);
	}

	public static implicit operator OracleNumber(long value)
	{
		return new OracleNumber(value);
	}

	public static explicit operator OracleNumber(double value)
	{
		return new OracleNumber(value);
	}

	public static explicit operator OracleNumber(float value)
	{
		return new OracleNumber(value);
	}

	public static explicit operator OracleNumber(string value)
	{
		if (value == null)
		{
			return Null;
		}
		return Parse(value);
	}

	public static explicit operator decimal(OracleNumber val)
	{
		return ToDecimal(val);
	}

	[SpecialName]
	public static int d(OracleNumber A_0)
	{
		return ToInt32(A_0);
	}

	[SpecialName]
	public static long c(OracleNumber A_0)
	{
		return ToInt64(A_0);
	}

	[SpecialName]
	public static double b(OracleNumber A_0)
	{
		return ToDouble(A_0);
	}

	[SpecialName]
	public static float a(OracleNumber A_0)
	{
		return ToFloat(A_0);
	}

	public static OracleNumber operator +(OracleNumber value1, OracleNumber value2)
	{
		return Add(value1, value2);
	}

	public static OracleNumber operator /(OracleNumber value1, OracleNumber value2)
	{
		return Divide(value1, value2);
	}

	public static bool operator ==(OracleNumber value1, OracleNumber value2)
	{
		return Equals(value1, value2);
	}

	public static bool operator >(OracleNumber value1, OracleNumber value2)
	{
		return GreaterThan(value1, value2);
	}

	public static bool operator >=(OracleNumber value1, OracleNumber value2)
	{
		return GreaterThanOrEqual(value1, value2);
	}

	public static bool operator !=(OracleNumber value1, OracleNumber value2)
	{
		return NotEquals(value1, value2);
	}

	public static bool operator <(OracleNumber value1, OracleNumber value2)
	{
		return LessThan(value1, value2);
	}

	public static bool operator <=(OracleNumber value1, OracleNumber value2)
	{
		return LessThanOrEqual(value1, value2);
	}

	public static OracleNumber operator *(OracleNumber value1, OracleNumber value2)
	{
		return Multiply(value1, value2);
	}

	public static OracleNumber operator -(OracleNumber value1, OracleNumber value2)
	{
		return Subtract(value1, value2);
	}

	public static OracleNumber operator %(OracleNumber value, OracleNumber devider)
	{
		return Modulo(value, devider);
	}

	private bool a()
	{
		if (Sign(this) == 0)
		{
			return true;
		}
		int num = this.a[0] - 1;
		bool flag = Sign(this) < 1;
		int num2 = ((!flag) ? (this.a[1] - 193) : ((byte)(~this.a[1]) - 128 - 65));
		int num3 = 0;
		for (int num4 = 0; num4 < num; num4++)
		{
			if (!flag)
			{
				num3 = ((this.a[num4 + 2] - 1 >= 10) ? (num3 + 2) : (num3 + 1));
				continue;
			}
			if (this.a[num4 + 2] == 102)
			{
				break;
			}
			num3 = ((101 - this.a[num4 + 2] >= 10) ? (num3 + 2) : (num3 + 1));
		}
		if ((num3 > 2 || num2 != 0) && (((flag || this.a[2] - 1 >= 10) && (!flag || 101 - this.a[2] >= 10)) ? (num3 - 2 > num2 * 2) : (num3 - 1 > num2 * 2)))
		{
			return false;
		}
		return true;
	}

	private static uint b(byte[] A_0)
	{
		int A_1;
		return OracleNumberUtils.a(A_0, 1, (int)A_0[0], out A_1);
	}

	private static ulong a(byte[] A_0)
	{
		long A_1;
		return OracleNumberUtils.a(A_0, 1, (int)A_0[0], out A_1);
	}

	private static OracleNumber a(uint A_0)
	{
		byte[] array = new byte[22];
		array[0] = OracleNumberUtils.a(A_0, array, 1, A_3: true);
		return new OracleNumber(array);
	}

	private static OracleNumber a(ulong A_0)
	{
		byte[] array = new byte[22];
		array[0] = OracleNumberUtils.a(A_0, array, 1, A_3: true);
		return new OracleNumber(array);
	}

	XmlSchema IXmlSerializable.GetSchema()
	{
		return null;
	}

	void IXmlSerializable.ReadXml(XmlReader reader)
	{
		if (!OracleUtils.a(reader))
		{
			this.a = Parse(reader.ReadElementString()).BinData;
		}
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

	TypeCode IConvertible.GetTypeCode()
	{
		if (IsNull)
		{
			return TypeCode.DBNull;
		}
		return TypeCode.Object;
	}

	bool IConvertible.ToBoolean(IFormatProvider provider)
	{
		return Convert.ToBoolean(Value);
	}

	byte IConvertible.ToByte(IFormatProvider provider)
	{
		return Convert.ToByte(Value);
	}

	char IConvertible.ToChar(IFormatProvider provider)
	{
		throw new InvalidCastException();
	}

	DateTime IConvertible.ToDateTime(IFormatProvider provider)
	{
		throw new InvalidCastException();
	}

	decimal IConvertible.ToDecimal(IFormatProvider provider)
	{
		return ToDecimal(this);
	}

	double IConvertible.ToDouble(IFormatProvider provider)
	{
		return ToDouble(this);
	}

	short IConvertible.ToInt16(IFormatProvider provider)
	{
		return Convert.ToInt16(Value);
	}

	int IConvertible.ToInt32(IFormatProvider provider)
	{
		return Convert.ToInt32(Value);
	}

	long IConvertible.ToInt64(IFormatProvider provider)
	{
		return Convert.ToInt64(Value);
	}

	sbyte IConvertible.ToSByte(IFormatProvider provider)
	{
		return Convert.ToSByte(Value);
	}

	float IConvertible.ToSingle(IFormatProvider provider)
	{
		return ToFloat(this);
	}

	object IConvertible.ToType(Type conversionType, IFormatProvider provider)
	{
		if ((object)conversionType == typeof(OracleNumber))
		{
			return this;
		}
		return Convert.ChangeType(Value, conversionType, provider);
	}

	ushort IConvertible.ToUInt16(IFormatProvider provider)
	{
		return Convert.ToUInt16(Value);
	}

	uint IConvertible.ToUInt32(IFormatProvider provider)
	{
		return Convert.ToUInt32(Value);
	}

	ulong IConvertible.ToUInt64(IFormatProvider provider)
	{
		return Convert.ToUInt64(Value);
	}
}
