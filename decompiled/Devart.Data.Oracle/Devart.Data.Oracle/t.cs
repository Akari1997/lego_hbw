using System;
using System.Runtime.CompilerServices;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class t
{
	private object m_a;

	private WeakReference m_b;

	public t(object A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public object a()
	{
		return this.m_a;
	}

	[SpecialName]
	public void a(object A_0)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public Devart.Common.z b()
	{
		if (this.m_b == null)
		{
			return null;
		}
		return (Devart.Common.z)this.m_b.Target;
	}

	[SpecialName]
	public void a(Devart.Common.z A_0)
	{
		this.m_b = new WeakReference(A_0);
	}
}
