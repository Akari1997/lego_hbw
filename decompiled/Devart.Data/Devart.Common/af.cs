using System;
using System.Runtime.InteropServices;

namespace Devart.Common;

internal static class af
{
	private static bool m_a;

	private static double m_b;

	private static double c;

	static af()
	{
		af.m_a = true;
		c = 0.0;
		b();
	}

	private static void b()
	{
		long A_ = 0L;
		af.m_a = QueryPerformanceFrequency(ref A_) != 0;
		if (af.m_a)
		{
			af.m_b = 1.0 / (double)A_;
			long a_ = a();
			for (int i = 0; i < 999; i++)
			{
				a(a());
			}
			double num = a(a_);
			c = (0.0 - num) * 0.001;
		}
	}

	public static long a()
	{
		if (af.m_a)
		{
			long A_ = 0L;
			af.m_a = QueryPerformanceCounter(ref A_) != 0;
			return A_;
		}
		return (uint)Environment.TickCount;
	}

	public static double a(long A_0)
	{
		if (af.m_a)
		{
			long A_1 = 0L;
			QueryPerformanceCounter(ref A_1);
			double num = (double)(A_1 - A_0) * af.m_b + c;
			if (num < 0.0)
			{
				num = 0.0;
			}
			return num;
		}
		uint num2 = (uint)A_0;
		uint tickCount = (uint)Environment.TickCount;
		uint num3 = ((tickCount > num2) ? (tickCount - num2) : (num2 - tickCount));
		return (double)num3 * 0.001;
	}

	[DllImport("kernel32.dll")]
	private static extern short QueryPerformanceCounter(ref long A_0);

	[DllImport("kernel32.dll")]
	private static extern short QueryPerformanceFrequency(ref long A_0);
}
