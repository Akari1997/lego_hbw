using System;

namespace Devart.Data.Oracle;

internal struct h(string A_0)
{
	internal delegate int a(object A_0, IntPtr A_1, int A_2, out a6 A_3);

	public string a = A_0;

	public int b = -1;

	public short c = 0;

	public int d = 0;

	public int e = 0;

	public int f = 0;

	public int g = 0;

	public int h = 0;

	public bool i = false;

	public bool j = false;

	public int k = 0;

	public int l = 0;

	public int m = 0;

	public int n = 0;

	public int o = 0;

	public int p = 0;

	public int q = 0;

	public int r = 0;

	public object s = null;

	public string t = null;

	public string u = null;

	public object v = null;

	public object w = null;

	public a x = null;

	public a y = null;

	public int z = 0;

	public int aa = 0;

	public int ab = 0;

	public Type ac = null;

	public OracleIdentityType ad = OracleIdentityType.None;

	public string a()
	{
		string text = u;
		if (s != null && s is am)
		{
			text = ((am)s).b();
		}
		if (!string.IsNullOrEmpty(text))
		{
			return $"OracleBind '{this.a}' of type '{text}'";
		}
		return $"OracleBind '{this.a}' of type #{c}";
	}
}
