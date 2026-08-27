using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class a8
{
	public SelectTable a;

	public int b;

	public List<ao> c;

	public Dictionary<string, List<ao>> d;

	public Dictionary<string, int> e;

	public ao f;

	private string g;

	public a8(SelectTable A_0)
	{
		this.a = A_0;
		string name = A_0.Name;
		if (name != null)
		{
			int num = name.IndexOf("\"@\"");
			if (num >= 0)
			{
				g = name.Substring(num + 3, name.Length - num - 4);
				A_0.Name = name.Substring(1, num - 1);
			}
			else
			{
				g = "";
			}
		}
		else
		{
			g = "";
		}
		this.c = new List<ao>();
		this.d = new Dictionary<string, List<ao>>();
		e = new Dictionary<string, int>();
	}

	[SpecialName]
	public string a()
	{
		return this.a.Schema;
	}

	[SpecialName]
	public void a(string A_0)
	{
		this.a.Schema = A_0;
	}

	[SpecialName]
	public string d()
	{
		return this.a.Name;
	}

	[SpecialName]
	public void b(string A_0)
	{
		this.a.Name = A_0;
	}

	[SpecialName]
	public string b()
	{
		return g;
	}

	[SpecialName]
	public void c(string A_0)
	{
		g = A_0;
	}

	[SpecialName]
	public string c()
	{
		return this.a.Alias;
	}

	[SpecialName]
	public void d(string A_0)
	{
		this.a.Alias = A_0;
	}
}
