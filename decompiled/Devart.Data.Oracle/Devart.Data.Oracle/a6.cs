using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace Devart.Data.Oracle;

internal sealed class a6 : MarshalByRefObject
{
	private const int m_a = 65534;

	private const int m_b = 4096;

	private readonly ArrayList m_c;

	private int m_d;

	internal byte[] e;

	internal byte[] f;

	private bool m_g;

	public a6(bool A_0)
	{
		this.m_g = A_0;
		this.m_c = new ArrayList(16);
		this.m_d = -1;
		this.e = new byte[4];
		this.f = new byte[4];
	}

	public a6(byte[] A_0, bool A_1)
		: this(A_1)
	{
		if (A_0 == null)
		{
			c(-1);
			return;
		}
		a(A_0.Length);
		this.m_c.Add(A_0);
		this.m_d = 0;
	}

	public void c()
	{
		if (this.m_c.Count == 0 && this.m_g)
		{
			b(4096);
		}
		else
		{
			b(65534);
		}
	}

	public void b(int A_0)
	{
		a(A_0);
		byte[] value = new byte[A_0];
		this.m_c.Add(value);
		this.m_d = this.m_c.Count - 1;
	}

	public bool a()
	{
		this.m_d = 0;
		return this.m_c.Count != 0;
	}

	public bool i()
	{
		this.m_d++;
		bool flag = this.m_d < this.m_c.Count;
		if (!flag)
		{
			this.m_d--;
		}
		return flag;
	}

	public void j()
	{
		this.m_c.Clear();
		this.m_d = -1;
	}

	public string a(Encoding A_0)
	{
		if (this.e[0] != 0)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder(e());
		int count = this.m_c.Count;
		for (int j = 0; j < count; j++)
		{
			byte[] array = (byte[])this.m_c[j];
			int count2 = ((j != count - 1) ? array.Length : h());
			string value = A_0.GetString(array, 0, count2);
			stringBuilder.Append(value);
		}
		return stringBuilder.ToString();
	}

	public byte[] g()
	{
		if (this.e[0] != 0)
		{
			return null;
		}
		byte[] array = new byte[e()];
		int count = this.m_c.Count;
		int num = 0;
		for (int j = 0; j < count; j++)
		{
			byte[] array2 = (byte[])this.m_c[j];
			int num2 = ((j != count - 1) ? array2.Length : h());
			Buffer.BlockCopy(array2, 0, array, num, num2);
			num += num2;
		}
		return array;
	}

	[SpecialName]
	public byte[] f()
	{
		if (this.m_d < 0 || this.m_d >= this.m_c.Count)
		{
			return null;
		}
		return (byte[])this.m_c[this.m_d];
	}

	[SpecialName]
	public void a(byte[] A_0)
	{
		if (A_0 == null)
		{
			c(-1);
			return;
		}
		j();
		a(A_0.Length);
		this.m_c.Add(A_0);
		this.m_d = 0;
	}

	[SpecialName]
	public int d()
	{
		return (sbyte)this.e[0];
	}

	[SpecialName]
	public void c(int A_0)
	{
		if (A_0 == 0)
		{
			this.e[0] = 0;
			this.e[1] = 0;
		}
		else
		{
			this.e[0] = byte.MaxValue;
			this.e[1] = byte.MaxValue;
		}
	}

	[SpecialName]
	public int h()
	{
		return this.f[0] | (this.f[1] << 8) | (this.f[2] << 16) | (this.f[3] << 24);
	}

	[SpecialName]
	public void a(int A_0)
	{
		int num = A_0;
		this.f[0] = (byte)num;
		num >>= 8;
		this.f[1] = (byte)num;
		num >>= 8;
		this.f[2] = (byte)num;
		num >>= 8;
		this.f[3] = (byte)num;
	}

	[SpecialName]
	public int e()
	{
		int num = 0;
		int count = this.m_c.Count;
		for (int j = 0; j < count; j++)
		{
			byte[] array = (byte[])this.m_c[j];
			num = ((j != count - 1) ? (num + array.Length) : (num + h()));
		}
		return num;
	}

	[SpecialName]
	public int b()
	{
		return this.m_d;
	}
}
