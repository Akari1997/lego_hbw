using System;
using System.Security.Cryptography;
using System.Text;

namespace Devart.Common;

internal sealed class ai
{
	public static uint c(byte[] A_0, int A_1)
	{
		return (uint)(A_0[A_1] | (A_0[A_1 + 1] << 8) | (A_0[A_1 + 2] << 16) | (A_0[A_1 + 3] << 24));
	}

	public static void b(uint A_0, byte[] A_1, int A_2)
	{
		A_1[A_2] = (byte)(A_0 & 0xFF);
		A_1[A_2 + 1] = (byte)((A_0 >> 8) & 0xFF);
		A_1[A_2 + 2] = (byte)((A_0 >> 16) & 0xFF);
		A_1[A_2 + 3] = (byte)((A_0 >> 24) & 0xFF);
	}

	public static uint b(byte[] A_0, int A_1)
	{
		return (uint)((A_0[A_1] << 24) | (A_0[A_1 + 1] << 16) | (A_0[A_1 + 2] << 8) | A_0[A_1 + 3]);
	}

	public static void a(uint A_0, byte[] A_1, int A_2)
	{
		A_1[A_2] = (byte)((A_0 >> 24) & 0xFF);
		A_1[A_2 + 1] = (byte)((A_0 >> 16) & 0xFF);
		A_1[A_2 + 2] = (byte)((A_0 >> 8) & 0xFF);
		A_1[A_2 + 3] = (byte)(A_0 & 0xFF);
	}

	public static ulong b(byte[] A_0)
	{
		return (ulong)((A_0[0] << 24) | (A_0[1] << 16) | (A_0[2] << 8) | A_0[3] | (A_0[4] << 24) | (A_0[5] << 16) | (A_0[6] << 8) | A_0[7]);
	}

	public static void a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4)
	{
		while (A_2 > 0)
		{
			A_3[A_4++] ^= A_0[A_1++];
			A_2--;
		}
	}

	public static int a(byte[] A_0, int A_1, byte[] A_2, int A_3, int A_4)
	{
		for (int num = 0; num < A_4; num++)
		{
			if (A_0[A_1 + num] != A_2[A_3 + num])
			{
				return A_2[A_3 + num] - A_0[A_1 + num];
			}
		}
		return 0;
	}

	public static int a(byte[] A_0, byte[] A_1)
	{
		if (A_0.Length != A_1.Length)
		{
			return A_1.Length - A_0.Length;
		}
		return a(A_0, 0, A_1, 0, A_0.Length);
	}

	public static int a(byte[] A_0, int A_1, int A_2, byte A_3)
	{
		int num = A_1 + A_2;
		for (int num2 = A_1; num2 < num; num2++)
		{
			if (A_0[num2] != A_3)
			{
				return A_3 - A_0[num2];
			}
		}
		return 0;
	}

	public static byte[] a(byte[] A_0, int A_1)
	{
		byte[] array = new byte[A_1];
		Buffer.BlockCopy(A_0, 0, array, 0, Math.Min(A_0.Length, array.Length));
		return array;
	}

	public static byte[] a(int A_0)
	{
		byte[] array = new byte[A_0];
		RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
		rNGCryptoServiceProvider.GetBytes(array);
		return array;
	}

	public static byte[] a(uint[] A_0)
	{
		int num = A_0.Length;
		byte[] array = new byte[num * 4];
		int num2 = 0;
		for (int num3 = 0; num3 < num; num3++)
		{
			uint num4 = A_0[num3];
			array[num2 + 3] = (byte)num4;
			array[num2 + 2] = (byte)(num4 >> 8);
			array[num2 + 1] = (byte)(num4 >> 16);
			array[num2] = (byte)(num4 >> 24);
			num2 += 4;
		}
		return array;
	}

	public static string a(byte[] A_0)
	{
		return Encoding.ASCII.GetString(A_0, 0, A_0.Length);
	}
}
