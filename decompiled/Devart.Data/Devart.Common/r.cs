using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Devart.Common;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Event)]
internal class r : DescriptionAttribute
{
	private static bool m_a;

	private static ResourceManager b;

	private static ResourceManager c;

	private bool d;

	public r(string A_0)
		: base(A_0)
	{
		if (r.m_a)
		{
			return;
		}
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		string fullName = executingAssembly.FullName;
		fullName = fullName.Substring(0, fullName.IndexOf(","));
		string[] array = new string[6] { ".CF.", ".POCKETPC.", ".SMARTPHONE.", ".WINDOWSCE.", ".ASMMETA.", ".110." };
		fullName += ".";
		for (int i = 0; i < array.Length; i++)
		{
			int num = fullName.ToUpper().IndexOf(array[i], 0);
			if (num != -1)
			{
				fullName = fullName.Remove(num, array[i].Length - 1);
			}
		}
		fullName = fullName.Substring(0, fullName.Length - 1);
		if (executingAssembly.GetManifestResourceInfo(fullName + ".Strings.resources") != null)
		{
			b = new ResourceManager(fullName + ".Strings", executingAssembly);
		}
		if (executingAssembly.GetManifestResourceInfo(fullName + ".Common.resources") != null)
		{
			c = new ResourceManager(fullName + ".Common", executingAssembly);
		}
		r.m_a = true;
	}

	[SpecialName]
	public virtual string a()
	{
		if (!d)
		{
			d = true;
			string text = null;
			if (c != null)
			{
				text = c.GetString(base.Description);
			}
			if (text == null && b != null)
			{
				text = b.GetString(base.Description);
			}
			if (text != null)
			{
				base.DescriptionValue = text;
			}
		}
		return base.Description;
	}

	static r()
	{
		r.m_a = false;
	}
}
