using System;
using System.Collections;
using Devart.Common;

namespace Devart.Data.Oracle;

internal sealed class z : k
{
	private new readonly ArrayList m_a;

	private new readonly int m_b;

	public z(g A_0, ArrayList A_1, int A_2, int A_3)
		: base(A_0, A_2, null)
	{
		this.m_a = A_1;
		this.m_b = A_3;
	}

	public override bool m(byte[] A_0, int A_1, int A_2)
	{
		return bool.Parse(o(A_0, A_1, A_2));
	}

	public override byte i(byte[] A_0, int A_1, int A_2)
	{
		return byte.Parse(o(A_0, A_1, A_2));
	}

	public override char n(byte[] A_0, int A_1, int A_2)
	{
		return o(A_0, A_1, A_2)[0];
	}

	public override DateTime e(byte[] A_0, int A_1, int A_2)
	{
		return DateTime.Parse(o(A_0, A_1, A_2));
	}

	public override TimeSpan f(byte[] A_0, int A_1, int A_2)
	{
		return TimeSpan.Parse(o(A_0, A_1, A_2));
	}

	public override decimal h(byte[] A_0, int A_1, int A_2)
	{
		return decimal.Parse(o(A_0, A_1, A_2));
	}

	public override double d(byte[] A_0, int A_1, int A_2)
	{
		return double.Parse(o(A_0, A_1, A_2));
	}

	public override float c(byte[] A_0, int A_1, int A_2)
	{
		return float.Parse(o(A_0, A_1, A_2));
	}

	public override short b(byte[] A_0, int A_1, int A_2)
	{
		return short.Parse(o(A_0, A_1, A_2));
	}

	public override int a(byte[] A_0, int A_1, int A_2)
	{
		return Utils.ParseIntWith0(o(A_0, A_1, A_2));
	}

	public override long g(byte[] A_0, int A_1, int A_2)
	{
		return long.Parse(o(A_0, A_1, A_2));
	}

	public override OracleNumber l(byte[] A_0, int A_1, int A_2)
	{
		return OracleNumber.Parse(o(A_0, A_1, A_2));
	}

	public override string o(byte[] A_0, int A_1, int A_2)
	{
		int index = A_1 / this.m_b;
		a6 a10 = (a6)this.m_a[index];
		return a10.a(((Devart.Common.v)this).d);
	}

	public override Guid q(byte[] A_0, int A_1, int A_2)
	{
		return new Guid(o(A_0, A_1, A_2));
	}

	public override byte[] y(byte[] A_0, int A_1, int A_2)
	{
		int index = A_1 / this.m_b;
		a6 a10 = (a6)this.m_a[index];
		return a10.g();
	}

	public override object p(byte[] A_0, int A_1, int A_2)
	{
		if (e() == 8)
		{
			return o(A_0, A_1, A_2);
		}
		return y(A_0, A_1, A_2);
	}

	public override a3 a(byte[] A_0, int A_1, bool A_2, bool A_3)
	{
		return base.a(A_0, A_1, A_2, A_3);
	}

	public override OracleString k(byte[] A_0, int A_1, int A_2)
	{
		return new OracleString(o(A_0, A_1, A_2));
	}

	public override OracleBinary j(byte[] A_0, int A_1, int A_2)
	{
		return y(A_0, A_1, A_2);
	}

	public override object t(byte[] A_0, int A_1, int A_2)
	{
		if (e() == 8)
		{
			return k(A_0, A_1, A_2);
		}
		return j(A_0, A_1, A_2);
	}
}
