using System;
using System.Runtime.CompilerServices;

namespace Devart.Data.Oracle;

internal abstract class at
{
	private readonly aa m_a;

	protected at(aa A_0)
	{
		this.m_a = A_0;
	}

	public abstract IntPtr a();

	public abstract void a(IntPtr A_0);

	[SpecialName]
	public aa b()
	{
		return this.m_a;
	}
}
