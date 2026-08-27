using System;
using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class q : Attribute
{
	private string m_a;

	public q(string A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public string a()
	{
		return this.m_a;
	}
}
