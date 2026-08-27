using System;
using System.Runtime.InteropServices;

namespace Devart.Common;

internal class t
{
	public struct a
	{
		private int a;

		private int b;

		public int c;

		public int d;

		private IntPtr e;

		public string f;

		private IntPtr g;

		private IntPtr h;
	}

	public static int a;

	public static int b;

	public static int c;

	public static int d;

	public static int e;

	public static int f;

	[DllImport("Mpr.dll")]
	public static extern int WNetEnumResource(IntPtr A_0, ref int A_1, IntPtr A_2, ref int A_3);

	[DllImport("Mpr.dll")]
	public static extern int WNetOpenEnum(int A_0, int A_1, int A_2, IntPtr A_3, out IntPtr A_4);

	[DllImport("Mpr.dll")]
	public static extern int WNetCloseEnum(IntPtr A_0);

	static t()
	{
		t.a = 0;
		b = 0;
		c = 2;
		d = 259;
		e = 2;
		f = 2;
	}
}
