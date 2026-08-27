using System;

namespace Devart.Common;

internal static class e
{
	private static void b(byte[] A_0)
	{
		for (int num = 0; num < A_0.Length / 2; num++)
		{
			byte b2 = A_0[num];
			A_0[num] = A_0[A_0.Length - num - 1];
			A_0[A_0.Length - num - 1] = b2;
		}
	}

	private static byte[] a(byte[] A_0, ref byte[] A_1, int A_2, int A_3)
	{
		int num = A_2 + A_3 - 1;
		for (int num2 = 0; num2 < A_3; num2++)
		{
			A_1[num2] = A_0[num--];
		}
		return A_1;
	}

	public static byte[] a(bool A_0)
	{
		return BitConverter.GetBytes(A_0);
	}

	public static byte[] a(char A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static byte[] a(double A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static byte[] a(short A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static byte[] a(int A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static byte[] a(long A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static byte[] a(float A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static byte[] a(ushort A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static byte[] a(uint A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static byte[] a(ulong A_0)
	{
		byte[] bytes = BitConverter.GetBytes(A_0);
		if (!BitConverter.IsLittleEndian)
		{
			b(bytes);
		}
		return bytes;
	}

	public static bool k(byte[] A_0, int A_1)
	{
		return BitConverter.ToBoolean(A_0, A_1);
	}

	public static char j(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[2];
			A_0 = a(A_0, ref A_2, A_1, 2);
			A_1 = 0;
		}
		return BitConverter.ToChar(A_0, A_1);
	}

	public static double i(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[8];
			A_0 = a(A_0, ref A_2, A_1, 8);
			A_1 = 0;
		}
		return BitConverter.ToDouble(A_0, A_1);
	}

	public static short h(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[2];
			A_0 = a(A_0, ref A_2, A_1, 2);
			A_1 = 0;
		}
		return BitConverter.ToInt16(A_0, A_1);
	}

	public static int g(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[4];
			A_0 = a(A_0, ref A_2, A_1, 4);
			A_1 = 0;
		}
		return BitConverter.ToInt32(A_0, A_1);
	}

	public static long f(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[8];
			A_0 = a(A_0, ref A_2, A_1, 8);
			A_1 = 0;
		}
		return BitConverter.ToInt64(A_0, A_1);
	}

	public static float e(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[4];
			A_0 = a(A_0, ref A_2, A_1, 4);
			A_1 = 0;
		}
		return BitConverter.ToSingle(A_0, A_1);
	}

	public static string a(byte[] A_0)
	{
		return BitConverter.ToString(A_0);
	}

	public static string d(byte[] A_0, int A_1)
	{
		return BitConverter.ToString(A_0, A_1);
	}

	public static string a(byte[] A_0, int A_1, int A_2)
	{
		return BitConverter.ToString(A_0, A_1, A_2);
	}

	public static ushort c(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[2];
			A_0 = a(A_0, ref A_2, A_1, 2);
			A_1 = 0;
		}
		return BitConverter.ToUInt16(A_0, A_1);
	}

	public static uint b(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[4];
			A_0 = a(A_0, ref A_2, A_1, 4);
			A_1 = 0;
		}
		return BitConverter.ToUInt32(A_0, A_1);
	}

	public static ulong a(byte[] A_0, int A_1)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte[] A_2 = new byte[8];
			A_0 = a(A_0, ref A_2, A_1, 8);
			A_1 = 0;
		}
		return BitConverter.ToUInt64(A_0, A_1);
	}
}
