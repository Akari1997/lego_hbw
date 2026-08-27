using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class s : PropertyDescriptor
{
	internal s(string A_0)
		: base(A_0, null)
	{
	}

	public virtual bool b(object A_0)
	{
		return false;
	}

	[SpecialName]
	public virtual Type a()
	{
		return typeof(object);
	}

	public virtual object d(object A_0)
	{
		return A_0;
	}

	[SpecialName]
	public virtual bool b()
	{
		return true;
	}

	[SpecialName]
	public virtual Type c()
	{
		return typeof(object);
	}

	public virtual void a(object A_0)
	{
	}

	public virtual void a(object A_0, object A_1)
	{
	}

	public virtual bool c(object A_0)
	{
		return false;
	}
}
