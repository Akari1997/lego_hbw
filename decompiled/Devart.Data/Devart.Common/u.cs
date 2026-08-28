using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class u : PropertyDescriptor
{
	private Type m_a;

	private Type m_b;

	private bool m_c;

	private bool m_d;

	private string e;

	public u(string A_0, Type A_1, Type A_2, bool A_3, Attribute[] A_4)
		: base(A_0, A_4)
	{
		this.m_a = A_1;
		this.m_b = A_2;
		this.m_c = A_3;
		e = base.DisplayName;
		foreach (Attribute attribute in A_4)
		{
			if (attribute is DisplayNameAttribute)
			{
				e = ((DisplayNameAttribute)attribute).DisplayName;
				break;
			}
		}
	}

	public virtual bool b(object A_0)
	{
		if (A_0 is DbConnectionStringBuilder dbConnectionStringBuilder)
		{
			return dbConnectionStringBuilder.ShouldSerialize(DisplayName);
		}
		return false;
	}

	public virtual object d(object A_0)
	{
		if (A_0 is DbConnectionStringBuilder dbConnectionStringBuilder && dbConnectionStringBuilder.TryGetValue(DisplayName, out object value))
		{
			return value;
		}
		return null;
	}

	public virtual void a(object A_0)
	{
		if (A_0 is DbConnectionStringBuilder dbConnectionStringBuilder)
		{
			dbConnectionStringBuilder.Remove(DisplayName);
			if (b())
			{
				a(dbConnectionStringBuilder);
			}
		}
	}

	public virtual void a(object A_0, object A_1)
	{
		if (A_0 is DbConnectionStringBuilder dbConnectionStringBuilder)
		{
			if ((object)PropertyType == typeof(string) && A_1.Equals(string.Empty))
			{
				A_1 = null;
			}
			dbConnectionStringBuilder[DisplayName] = A_1;
			if (b())
			{
				a(dbConnectionStringBuilder);
			}
		}
	}

	protected virtual void a(DbConnectionStringBuilder A_0)
	{
	}

	public virtual bool c(object A_0)
	{
		if (A_0 is DbConnectionStringBuilder dbConnectionStringBuilder)
		{
			return dbConnectionStringBuilder.ShouldSerialize(DisplayName);
		}
		return false;
	}

	[SpecialName]
	public virtual Type a()
	{
		return this.m_a;
	}

	[SpecialName]
	public virtual bool c()
	{
		return this.m_c;
	}

	[SpecialName]
	public virtual Type d()
	{
		return this.m_b;
	}

	[SpecialName]
	internal bool b()
	{
		return this.m_d;
	}

	[SpecialName]
	internal void a(bool A_0)
	{
		this.m_d = A_0;
	}

	[SpecialName]
	public virtual string get_DisplayName()
	{
		return e;
	}
}
