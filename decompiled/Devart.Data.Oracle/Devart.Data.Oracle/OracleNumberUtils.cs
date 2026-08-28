using System;
using System.Globalization;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleNumberUtils
{
	private static double[,] m_a;

	private static float[,] m_b;

	private static byte[] m_c;

	internal static byte a(uint A_0, byte[] A_1, int A_2, bool A_3)
	{
		if (A_0 == 0)
		{
			A_1[A_2] = 128;
			return 1;
		}
		byte[] array = new byte[20];
		byte[] array2 = new byte[20];
		int num = (A_3 ? 1 : (-1));
		byte b2 = 0;
		while (A_0 != 0)
		{
			array2[b2] = (byte)Math.Abs((decimal)A_0 % 100m);
			A_0 /= 100;
			b2++;
		}
		b2--;
		byte b3 = 0;
		byte b4 = b2;
		byte b5 = b4;
		while (b3 <= b4)
		{
			array[b3] = array2[b5];
			b3++;
			b5--;
		}
		while (b2 > 0 && array[b2] == 0)
		{
			b2--;
			b3--;
		}
		byte result;
		if (num >= 0)
		{
			result = (byte)(b3 + 1);
			A_1[A_2] = (byte)(b4 + 128 + 65);
			for (b2 = 0; b2 < b3; b2++)
			{
				A_1[A_2 + b2 + 1] = (byte)(array[b2] + 1);
			}
		}
		else
		{
			result = ((b3 >= 20) ? ((byte)(b3 + 1)) : ((byte)(b3 + 2)));
			A_1[A_2] = (byte)(~(b4 + 128 + 65));
			for (b2 = 0; b2 < b3; b2++)
			{
				A_1[A_2 + b2 + 1] = (byte)(101 - array[b2]);
			}
			if (b2 < 20)
			{
				A_1[A_2 + b2 + 1] = 102;
			}
		}
		return result;
	}

	internal static int h(byte[] A_0, int A_1)
	{
		uint num = a(A_0, A_1 + 1, (int)A_0[A_1], out int A_2);
		if ((A_2 >= 0 && num > int.MaxValue) || (num != 0 && A_2 < 0 && num - 1 > int.MaxValue))
		{
			throw new OverflowException(Devart.Common.al.a("ValueWasLargeOrSmallForInt32"));
		}
		if (A_2 < 0)
		{
			return (int)(0 - num);
		}
		return (int)num;
	}

	internal static long g(byte[] A_0, int A_1)
	{
		ulong num = a(A_0, A_1 + 1, (int)A_0[A_1], out long A_2);
		if ((A_2 >= 0 && num > long.MaxValue) || (num != 0 && A_2 < 0 && num - 1 > long.MaxValue))
		{
			throw new OverflowException(Devart.Common.al.a("ValueWasLargeOrSmallForInt64"));
		}
		if (A_2 < 0)
		{
			return (long)(0L - num);
		}
		return (long)num;
	}

	internal static uint a(byte[] A_0, int A_1, int A_2, out int A_3)
	{
		A_3 = 1;
		if (A_2 == 0 || (A_2 == 1 && A_0[A_1] == 128))
		{
			return 0u;
		}
		if (A_2 == 2 && A_0[A_1] == byte.MaxValue && A_0[A_1] == 101)
		{
			return 0u;
		}
		if (A_2 == 1 && A_0[A_1] == 0)
		{
			return 0u;
		}
		byte[] array;
		if ((A_0[A_1] & -128) != 0)
		{
			array = new byte[A_2];
			array[0] = (byte)((A_0[A_1] & -129) - 65);
			for (int num = 1; num < A_2; num++)
			{
				array[num] = (byte)(A_0[A_1 + num] - 1);
			}
		}
		else
		{
			if (A_2 - 1 == 20 && A_0[A_1 + A_2 - 1] != 102)
			{
				array = new byte[A_2];
			}
			else
			{
				array = new byte[A_2 - 1];
				A_2--;
			}
			array[0] = (byte)((~A_0[A_1] & 0xFFFFFF7Fu) - 65);
			for (int num2 = 1; num2 < A_2; num2++)
			{
				array[num2] = (byte)(101 - A_0[A_1 + num2]);
			}
		}
		int num3 = (sbyte)array[0];
		A_2--;
		uint num4 = 0u;
		int num5 = ((A_2 > num3 + 1) ? (num3 + 1) : A_2);
		for (int num6 = 0; num6 < num5; num6++)
		{
			num4 = num4 * 100 + array[num6 + 1];
		}
		for (int num7 = num3 - A_2; num7 >= 0; num7--)
		{
			num4 *= 100;
		}
		if ((A_0[A_1] & -128) == 0)
		{
			A_3 = -1;
		}
		return num4;
	}

	internal static byte a(ulong A_0, byte[] A_1, int A_2, bool A_3)
	{
		if (A_0 == 0)
		{
			A_1[A_2] = 128;
			return 1;
		}
		byte[] array = new byte[20];
		byte[] array2 = new byte[20];
		int num = (A_3 ? 1 : (-1));
		byte b2 = 0;
		while (A_0 != 0)
		{
			array2[b2] = (byte)Math.Abs((decimal)A_0 % 100m);
			A_0 /= 100;
			b2++;
		}
		b2--;
		byte b3 = 0;
		byte b4 = b2;
		byte b5 = b4;
		while (b3 <= b4)
		{
			array[b3] = array2[b5];
			b3++;
			b5--;
		}
		while (b2 > 0 && array[b2] == 0)
		{
			b2--;
			b3--;
		}
		byte result;
		if (num >= 0)
		{
			result = (byte)(b3 + 1);
			A_1[A_2] = (byte)(b4 + 128 + 65);
			for (b2 = 0; b2 < b3; b2++)
			{
				A_1[A_2 + b2 + 1] = (byte)(array[b2] + 1);
			}
		}
		else
		{
			result = ((b3 >= 20) ? ((byte)(b3 + 1)) : ((byte)(b3 + 2)));
			A_1[A_2] = (byte)(~(b4 + 128 + 65));
			for (b2 = 0; b2 < b3; b2++)
			{
				A_1[A_2 + b2 + 1] = (byte)(101 - array[b2]);
			}
			if (b2 < 20)
			{
				A_1[A_2 + b2 + 1] = 102;
			}
		}
		return result;
	}

	internal static ulong a(byte[] A_0, int A_1, int A_2, out long A_3)
	{
		A_3 = 1L;
		if (A_2 == 0 || (A_2 == 1 && A_0[A_1] == 128))
		{
			return 0uL;
		}
		if (A_2 == 2 && A_0[A_1] == byte.MaxValue && A_0[A_1] == 101)
		{
			return 0uL;
		}
		if (A_2 == 1 && A_0[A_1] == 0)
		{
			return 0uL;
		}
		byte[] array;
		if ((A_0[A_1] & -128) != 0)
		{
			array = new byte[A_2];
			array[0] = (byte)((A_0[A_1] & -129) - 65);
			for (int num = 1; num < A_2; num++)
			{
				array[num] = (byte)(A_0[A_1 + num] - 1);
			}
		}
		else
		{
			if (A_2 - 1 == 20 && A_0[A_1 + A_2 - 1] != 102)
			{
				array = new byte[A_2];
			}
			else
			{
				array = new byte[A_2 - 1];
				A_2--;
			}
			array[0] = (byte)((~A_0[A_1] & 0xFFFFFF7Fu) - 65);
			for (int num2 = 1; num2 < A_2; num2++)
			{
				array[num2] = (byte)(101 - A_0[A_1 + num2]);
			}
		}
		int num3 = (sbyte)array[0];
		A_2--;
		ulong num4 = 0uL;
		int num5 = ((A_2 > num3 + 1) ? (num3 + 1) : A_2);
		for (int num6 = 0; num6 < num5; num6++)
		{
			num4 = num4 * 100 + array[num6 + 1];
		}
		for (int num7 = num3 - A_2; num7 >= 0; num7--)
		{
			num4 *= 100;
		}
		if ((A_0[A_1] & -128) == 0)
		{
			A_3 = -1L;
		}
		return num4;
	}

	internal static byte a(double A_0, byte[] A_1, int A_2)
	{
		if (A_0 == 0.0)
		{
			A_1[A_2] = 128;
			return 1;
		}
		if (A_0 == double.PositiveInfinity)
		{
			A_1[A_2] = byte.MaxValue;
			A_1[A_2 + 1] = 101;
			return 2;
		}
		if (A_0 == double.NegativeInfinity)
		{
			A_1[A_2] = 0;
			return 1;
		}
		byte[] array = new byte[20];
		int num = ((A_0 > 0.0) ? 1 : (-1));
		A_0 = Math.Abs(A_0);
		int num2 = 0;
		if (A_0 < 1.0)
		{
			for (int num3 = 0; num3 <= 7; num3++)
			{
				if (OracleNumberUtils.m_a[num3, 2] >= A_0)
				{
					num2 -= (int)OracleNumberUtils.m_a[num3, 0];
					A_0 *= OracleNumberUtils.m_a[num3, 1];
				}
			}
			if (A_0 < 1.0)
			{
				num2--;
				A_0 *= 100.0;
			}
		}
		else
		{
			for (int num4 = 0; num4 <= 7; num4++)
			{
				if (OracleNumberUtils.m_a[num4, 1] <= A_0)
				{
					num2 += (int)OracleNumberUtils.m_a[num4, 0];
					A_0 /= OracleNumberUtils.m_a[num4, 1];
				}
			}
		}
		bool flag = A_0 < 10.0;
		byte b2 = 8;
		int num5 = 0;
		byte b3 = (byte)A_0;
		for (; num5 < b2; num5++)
		{
			array[num5] = b3;
			A_0 = (A_0 - (double)(int)b3) * 100.0;
			b3 = (byte)A_0;
		}
		num5 = 7;
		if (flag)
		{
			if (b3 >= 50)
			{
				array[num5]++;
			}
		}
		else
		{
			array[num5] = (byte)((array[num5] + 5) / 10 * 10);
		}
		while (array[num5] == 100)
		{
			if (num5 == 0)
			{
				num2++;
				array[num5] = 1;
				break;
			}
			array[num5] = 0;
			num5--;
			array[num5]++;
		}
		num5 = 7;
		while (num5 != 0 && array[num5] == 0)
		{
			b2--;
			num5--;
		}
		byte b4 = b2;
		if (num2 + 128 + 65 > 255)
		{
			throw new OverflowException("Value was either too large or too small for a OracleNumber.");
		}
		if (num2 + 128 + 65 < 128)
		{
			A_1[A_2] = 128;
			return 1;
		}
		byte result;
		if (num >= 0)
		{
			result = (byte)(b4 + 1);
			A_1[A_2] = (byte)(num2 + 128 + 65);
			for (int num6 = 0; num6 < b4; num6++)
			{
				A_1[A_2 + num6 + 1] = (byte)(array[num6] + 1);
			}
		}
		else
		{
			result = ((b4 >= 20) ? ((byte)(b4 + 1)) : ((byte)(b4 + 2)));
			A_1[A_2] = (byte)(~(num2 + 128 + 65));
			int num7;
			for (num7 = 0; num7 < b4; num7++)
			{
				A_1[A_2 + num7 + 1] = (byte)(101 - array[num7]);
			}
			if (num7 < 20)
			{
				A_1[A_2 + num7 + 1] = 102;
			}
		}
		return result;
	}

	internal static float f(byte[] A_0, int A_1)
	{
		return b(A_0, A_1 + 1, A_0[A_1]);
	}

	internal static float b(byte[] A_0, int A_1, int A_2)
	{
		if (A_2 == 0 || (A_2 == 1 && A_0[A_1] == 128))
		{
			return 0f;
		}
		if (A_2 == 2 && A_0[A_1] == byte.MaxValue && A_0[A_1] == 101)
		{
			return float.PositiveInfinity;
		}
		if (A_2 == 1 && A_0[A_1] == 0)
		{
			return float.NegativeInfinity;
		}
		byte[] array;
		int num;
		if ((A_0[A_1] & -128) != 0)
		{
			array = new byte[A_2];
			array[0] = (byte)((A_0[A_1] & -129) - 65);
			for (num = 1; num < A_2; num++)
			{
				array[num] = (byte)(A_0[A_1 + num] - 1);
			}
		}
		else
		{
			if (A_2 - 1 == 20 && A_0[A_2 - 1] != 102)
			{
				array = new byte[A_2];
			}
			else
			{
				array = new byte[A_2 - 1];
				A_2--;
			}
			array[0] = (byte)((~A_0[A_1] & 0xFFFFFF7Fu) - 65);
			for (num = 1; num < A_2; num++)
			{
				array[num] = (byte)(101 - A_0[A_1 + num]);
			}
		}
		int num2 = (sbyte)array[0];
		float num3 = 0f;
		num = 1;
		int num4 = 0;
		int num5 = num2 + 1;
		while (num < A_2 && num4 < 15)
		{
			num3 = num3 * 100f + (float)(int)array[num];
			num++;
			num4++;
			num5--;
		}
		if (num < A_2 && array[num] >= 50)
		{
			num3++;
		}
		if (num5 > 0)
		{
			for (num = 0; num <= 7; num++)
			{
				if (((num5 >> 7 - num) & 1) > 0)
				{
					num3 *= OracleNumberUtils.m_b[num, 1];
				}
			}
		}
		else
		{
			for (num = 0; num <= 7; num++)
			{
				if (((-num5 >> 7 - num) & 1) > 0)
				{
					num3 /= OracleNumberUtils.m_b[num, 1];
				}
			}
		}
		if ((A_0[A_1] & -128) == 0)
		{
			num3 = 0f - num3;
		}
		return num3;
	}

	internal static double e(byte[] A_0, int A_1)
	{
		return a(A_0, A_1 + 1, A_0[A_1]);
	}

	internal static double a(byte[] A_0, int A_1, int A_2)
	{
		if (A_2 == 0 || (A_2 == 1 && A_0[A_1] == 128))
		{
			return 0.0;
		}
		if (A_2 == 2 && A_0[A_1] == byte.MaxValue && A_0[A_1] == 101)
		{
			return double.PositiveInfinity;
		}
		if (A_2 == 1 && A_0[A_1] == 0)
		{
			return double.NegativeInfinity;
		}
		byte[] array;
		int num;
		if ((A_0[A_1] & -128) != 0)
		{
			array = new byte[A_2];
			array[0] = (byte)((A_0[A_1] & -129) - 65);
			for (num = 1; num < A_2; num++)
			{
				array[num] = (byte)(A_0[A_1 + num] - 1);
			}
		}
		else
		{
			if (A_2 - 1 == 20 && A_0[A_2 - 1] != 102)
			{
				array = new byte[A_2];
			}
			else
			{
				array = new byte[A_2 - 1];
				A_2--;
			}
			array[0] = (byte)((~A_0[A_1] & 0xFFFFFF7Fu) - 65);
			for (num = 1; num < A_2; num++)
			{
				array[num] = (byte)(101 - A_0[A_1 + num]);
			}
		}
		int num2 = (sbyte)array[0];
		double num3 = 0.0;
		num = 1;
		int num4 = 0;
		int num5 = num2 + 1;
		while (num < A_2 && num4 < 15)
		{
			num3 = num3 * 100.0 + (double)(int)array[num];
			num++;
			num4++;
			num5--;
		}
		if (num < A_2 && array[num] >= 50)
		{
			num3++;
		}
		if (num5 > 0)
		{
			for (num = 0; num <= 7; num++)
			{
				if (((num5 >> 7 - num) & 1) > 0)
				{
					num3 *= OracleNumberUtils.m_a[num, 1];
				}
			}
		}
		else
		{
			for (num = 0; num <= 7; num++)
			{
				if (((-num5 >> 7 - num) & 1) > 0)
				{
					num3 /= OracleNumberUtils.m_a[num, 1];
				}
			}
		}
		if ((A_0[A_1] & -128) == 0)
		{
			num3 = 0.0 - num3;
		}
		return num3;
	}

	internal static byte[] a(decimal A_0)
	{
		byte[] array = new byte[22];
		if (A_0 == 0m)
		{
			array[0] = 1;
			array[1] = 128;
			return array;
		}
		int[] bits = decimal.GetBits(A_0);
		int num = bits[3] >> 31;
		sbyte b2 = (sbyte)(bits[3] >> 16);
		bits[3] = 0;
		decimal num2 = new decimal(bits);
		bool flag = b2 % 2 > 0;
		uint[] array2 = new uint[4];
		decimal num3 = 100000000m;
		if (flag)
		{
			b2++;
			decimal num4 = 10000000m;
			array2[0] = (uint)(num2 % num4) * 10;
			num2 /= num4;
		}
		else
		{
			array2[0] = (uint)(num2 % num3);
			num2 /= num3;
		}
		array2[1] = (uint)(num2 % num3);
		num2 /= num3;
		ulong num5 = (ulong)num2;
		array2[2] = (uint)(num5 % 100000000);
		array2[3] = (uint)(num5 / 100000000);
		byte[] array3 = new byte[15];
		for (int num6 = 0; num6 < 15; num6++)
		{
			int num7 = num6 >> 2;
			array3[num6] = (byte)(array2[num7] % 100);
			array2[num7] /= 100u;
		}
		int num8 = 0;
		for (int num9 = 14; num9 >= 0; num9--)
		{
			if (array3[num9] != 0)
			{
				num8 = num9 + 1;
				break;
			}
		}
		b2 = (sbyte)(num8 - b2 / 2 - 1);
		int num10 = 0;
		for (int num11 = 0; num11 <= 14 && array3[num11] == 0; num11++)
		{
			num10++;
		}
		if (num == 0)
		{
			array[0] = (byte)(num8 - num10 + 1);
			array[1] = (byte)(b2 + 128 + 65);
			for (int num12 = 0; num12 < num8 - num10; num12++)
			{
				array[num12 + 2] = (byte)(array3[num8 - 1 - num12] + 1);
			}
		}
		else
		{
			if (num8 - num10 < 20)
			{
				array[0] = (byte)(num8 - num10 + 2);
			}
			else
			{
				array[0] = (byte)(num8 - num10 + 1);
			}
			array[1] = (byte)(~(b2 + 128 + 65));
			int num13;
			for (num13 = 0; num13 < num8 - num10; num13++)
			{
				array[num13 + 2] = (byte)(101 - array3[num8 - 1 - num13]);
			}
			if (num13 < 20)
			{
				array[num13 + 2] = 102;
			}
		}
		return array;
	}

	private static bool a(byte[] A_0)
	{
		if (A_0.Length < 15)
		{
			return true;
		}
		if (A_0.Length > 15)
		{
			return false;
		}
		for (int num = 0; num < 15; num++)
		{
			if (A_0[num] < OracleNumberUtils.m_c[num])
			{
				return true;
			}
			if (A_0[num] > OracleNumberUtils.m_c[num])
			{
				return false;
			}
		}
		return true;
	}

	internal static decimal d(byte[] A_0, int A_1)
	{
		if (A_0[A_1] == 1 && A_0[A_1 + 1] == 128)
		{
			return 0m;
		}
		if (A_0[A_1] == 2 && A_0[A_1 + 1] == byte.MaxValue && A_0[A_1 + 1] == 101)
		{
			return decimal.MaxValue;
		}
		if (A_0[A_1] == 1 && A_0[A_1 + 1] == 0)
		{
			return decimal.MinValue;
		}
		byte b2 = A_0[A_1];
		if (b2 == 0)
		{
			return 0m;
		}
		int num;
		if ((A_0[A_1 + 1] & -128) != 0)
		{
			num = (sbyte)((A_0[A_1 + 1] & -129) - 65) + 1;
		}
		else
		{
			num = (sbyte)((~A_0[A_1 + 1] & 0xFFFFFF7Fu) - 65) + 1;
			if (b2 - 1 != 20 || A_0[A_1 + b2] == 102)
			{
				b2--;
			}
		}
		byte b3 = ((num <= 0 || num > 14 || b2 - 1 >= num) ? ((byte)(b2 - 1)) : ((byte)num));
		byte[] array = new byte[b3];
		if ((A_0[A_1 + 1] & -128) != 0)
		{
			for (int num2 = 0; num2 < b2 - 1; num2++)
			{
				array[num2] = (byte)(A_0[A_1 + num2 + 2] - 1);
			}
		}
		else
		{
			for (int num2 = 0; num2 < b2 - 1; num2++)
			{
				array[num2] = (byte)(101 - A_0[A_1 + num2 + 2]);
			}
		}
		int num3 = 0;
		int num4;
		if (num <= 0)
		{
			num4 = b3;
			if (num4 > 14 + num)
			{
				num4 = 14 + num;
			}
			if (num4 < 0)
			{
				num4 = 0;
				num = 0;
			}
		}
		else if (a(array))
		{
			num4 = b3;
		}
		else if (b3 == 15)
		{
			byte[] array2 = new byte[b3];
			for (int num2 = b3 - 1; num2 >= 0; num2--)
			{
				int num5 = array[num2] / 10;
				int num6 = array[num2] % 10;
				array2[num2] = (byte)num5;
				if (num2 < b3 - 1)
				{
					array2[num2 + 1] += (byte)(num6 * 10);
				}
			}
			if (a(array2))
			{
				array = array2;
				num4 = b3;
				num3 = 1;
			}
			else
			{
				num4 = 14;
			}
		}
		else
		{
			num4 = 14;
		}
		uint[] array3 = new uint[4];
		uint[] array4 = array3;
		for (int num7 = 0; num7 < 4; num7++)
		{
			int num8 = num7 * 4 + 4;
			int num2;
			for (num2 = ((num4 - num8 >= 0) ? (num4 - num8) : 0); num2 < num4 - num8 + 4; num2++)
			{
				array4[num7] = array4[num7] * 100 + array[num2];
			}
			if (num2 <= 4)
			{
				break;
			}
		}
		ulong num9 = (ulong)((long)array4[3] * 100000000L + array4[2]);
		ulong num10 = (ulong)((long)(uint)num9 * 100000000L + array4[1]);
		ulong num11 = (ulong)((long)(uint)(num9 >> 32) * 100000000L + (uint)(num10 >> 32));
		num10 = (ulong)((long)(uint)num10 * 100000000L + array4[0]);
		num9 = (ulong)((long)(uint)num11 * 100000000L + (uint)(num10 >> 32));
		num11 = (ulong)((long)(uint)(num11 >> 32) * 100000000L + (uint)(num9 >> 32));
		return new decimal((int)num10, (int)num9, (int)num11, (A_0[A_1 + 1] & -128) == 0, (byte)((num4 - num) * 2 - num3));
	}

	internal static string c(byte[] A_0, int A_1)
	{
		return a(A_0, A_1, "G", CultureInfo.CurrentCulture);
	}

	internal static string a(byte[] A_0, int A_1, string A_2, IFormatProvider A_3)
	{
		string a_ = b(A_0, A_1);
		return Devart.Common.an.a(a_, A_2, A_3);
	}

	internal static string b(byte[] A_0, int A_1)
	{
		if (A_0[A_1] == 1 && A_0[A_1 + 1] == 128)
		{
			return "0";
		}
		if (A_0[A_1] == 2 && A_0[A_1 + 1] == byte.MaxValue && A_0[A_1 + 1] == 101)
		{
			return "0";
		}
		if (A_0[A_1] == 1 && A_0[A_1 + 1] == 0)
		{
			return "0";
		}
		byte b2 = A_0[A_1];
		if (b2 == 0)
		{
			return "0";
		}
		int num;
		if ((A_0[A_1 + 1] & -128) != 0)
		{
			num = (sbyte)((A_0[A_1 + 1] & -129) - 65);
		}
		else
		{
			num = (sbyte)((~A_0[A_1 + 1] & 0xFFFFFF7Fu) - 65);
			if (b2 - 1 != 20 || A_0[A_1 + b2] == 102)
			{
				b2--;
			}
		}
		byte b3 = ((num <= 0 || num > 14 || b2 - 1 >= num) ? ((byte)(b2 - 1)) : ((byte)num));
		byte[] array = new byte[b3];
		if ((A_0[A_1 + 1] & -128) != 0)
		{
			for (int num2 = 0; num2 < b2 - 1; num2++)
			{
				array[num2] = (byte)(A_0[A_1 + num2 + 2] - 1);
			}
		}
		else
		{
			for (int num2 = 0; num2 < b2 - 1; num2++)
			{
				array[num2] = (byte)(101 - A_0[A_1 + num2 + 2]);
			}
		}
		char c2 = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator[0];
		StringBuilder stringBuilder = new StringBuilder();
		if (num < -1)
		{
			stringBuilder.Append("0" + c2);
			stringBuilder.Append(new string('0', (-num - 1) * 2));
		}
		else if (num == -1)
		{
			stringBuilder.Append("0");
		}
		for (int num2 = 0; num2 < array.Length; num2++)
		{
			if (num == num2 - 1)
			{
				stringBuilder.Append(c2);
			}
			stringBuilder.AppendFormat(null, "{0:D2}", new object[1] { array[num2] });
		}
		if (num >= array.Length)
		{
			stringBuilder.Append(new string('0', (num - array.Length + 1) * 2));
		}
		if (num <= array.Length - 2 && array[^1] % 10 == 0)
		{
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
		}
		if (num >= 0 && array[0] < 10)
		{
			stringBuilder.Remove(0, 1);
		}
		if ((A_0[A_1 + 1] & -128) == 0)
		{
			stringBuilder.Insert(0, "-");
		}
		return stringBuilder.ToString();
	}

	internal static bool a(byte[] A_0, int A_1)
	{
		return d(A_0, A_1) != 0m;
	}

	static OracleNumberUtils()
	{
		OracleNumberUtils.m_a = new double[8, 3]
		{
			{ 128.0, 1E+256, 1E-256 },
			{ 64.0, 1E+128, 1E-128 },
			{ 32.0, 1E+64, 1E-64 },
			{ 16.0, 1E+32, 1E-32 },
			{ 8.0, 10000000000000000.0, 1E-16 },
			{ 4.0, 100000000.0, 1E-08 },
			{ 2.0, 10000.0, 0.0001 },
			{ 1.0, 100.0, 0.01 }
		};
		OracleNumberUtils.m_b = new float[8, 3]
		{
			{
				128f,
				float.PositiveInfinity,
				0f
			},
			{
				64f,
				float.PositiveInfinity,
				0f
			},
			{
				32f,
				float.PositiveInfinity,
				0f
			},
			{ 16f, 1E+32f, 1E-32f },
			{ 8f, 1E+16f, 1E-16f },
			{ 4f, 100000000f, 1E-08f },
			{ 2f, 10000f, 0.0001f },
			{ 1f, 100f, 0.01f }
		};
		OracleNumberUtils.m_c = new byte[15]
		{
			7, 92, 28, 16, 25, 14, 26, 43, 37, 59,
			35, 43, 95, 3, 35
		};
	}
}
