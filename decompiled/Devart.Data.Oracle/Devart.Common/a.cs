using System;
using System.Security.Cryptography;

namespace Devart.Common;

internal sealed class a
{
	private RNGCryptoServiceProvider m_a = new RNGCryptoServiceProvider();

	private byte[] m_b = new byte[4];

	public int a()
	{
		this.m_a.GetBytes(this.m_b);
		return BitConverter.ToInt32(this.m_b, 0) & 0x7FFFFFFF;
	}

	public int a(int A_0)
	{
		if (A_0 < 0)
		{
			throw new ArgumentOutOfRangeException("maxValue");
		}
		return a(0, A_0);
	}

	public int a(int A_0, int A_1)
	{
		if (A_0 > A_1)
		{
			throw new ArgumentOutOfRangeException("minValue");
		}
		if (A_0 == A_1)
		{
			return A_0;
		}
		long num = A_1 - A_0;
		uint num2;
		long num3;
		long num4;
		do
		{
			this.m_a.GetBytes(this.m_b);
			num2 = BitConverter.ToUInt32(this.m_b, 0);
			num3 = 4294967296L;
			num4 = num3 % num;
		}
		while (num2 >= num3 - num4);
		return (int)(A_0 + num2 % num);
	}

	public double b()
	{
		this.m_a.GetBytes(this.m_b);
		uint num = BitConverter.ToUInt32(this.m_b, 0);
		return (double)num / 4294967296.0;
	}

	public void a(byte[] A_0)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException("buffer");
		}
		this.m_a.GetBytes(A_0);
	}
}
