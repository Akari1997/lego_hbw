using System;

namespace Devart.Data.Oracle;

public class OracleAttribute : MarshalByRefObject
{
	internal string a;

	internal int b;

	internal int c;

	internal int d;

	internal int e;

	internal int f;

	internal OracleDbType g;

	internal OracleType h;

	internal k i;

	internal short j;

	internal short k;

	internal short l;

	internal bool m;

	public string Name => a;

	public OracleDbType DbType => g;

	public int Precision => d;

	public int Scale => e;

	public OracleType ObjectType => h;

	public string TypeName
	{
		get
		{
			if (h == null)
			{
				return OracleUtils.a(g);
			}
			return h.Name;
		}
	}

	internal OracleAttribute()
	{
	}
}
