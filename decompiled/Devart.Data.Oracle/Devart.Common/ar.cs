using System;
using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class ar : Attribute
{
	private string m_a;

	public ar(string A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public string a()
	{
		return this.m_a;
	}
}
