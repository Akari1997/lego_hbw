using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Devart.Common;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Event)]
internal class i : DescriptionAttribute
{
	private static bool m_a;

	private static ResourceManager b;

	private static ResourceManager c;

	private bool d;

	public i(string A_0)
		: base(A_0)
	{
		if (i.m_a)
		{
			return;
		}
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		string fullName = executingAssembly.FullName;
		fullName = fullName.Substring(0, fullName.IndexOf(","));
		string[] array = new string[6] { ".CF.", ".POCKETPC.", ".SMARTPHONE.", ".WINDOWSCE.", ".ASMMETA.", ".110." };
		fullName += ".";
		for (int num = 0; num < array.Length; num++)
		{
			int num2 = fullName.ToUpper().IndexOf(array[num], 0);
			if (num2 != -1)
			{
				fullName = fullName.Remove(num2, array[num].Length - 1);
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
		i.m_a = true;
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

	static i()
	{
		i.m_a = false;
	}
}
