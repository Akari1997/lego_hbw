using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class g : PropertyDescriptor
{
	private DataColumn m_a;

	private DataRelation m_b;

	public g(DataColumn A_0)
		: base(A_0.ColumnName, null)
	{
		this.m_a = A_0;
	}

	public g(DataRelation A_0)
		: base(A_0.RelationName, null)
	{
		this.m_b = A_0;
	}

	[SpecialName]
	public virtual Type b()
	{
		return typeof(DbDataRowView);
	}

	[SpecialName]
	public virtual bool d()
	{
		return this.m_a.ReadOnly;
	}

	[SpecialName]
	public virtual Type g()
	{
		if (this.m_b != null)
		{
			return typeof(IBindingList);
		}
		return ((DbDataTable)this.m_a.Table).a(this.m_a.DataType);
	}

	public virtual bool c(object A_0)
	{
		return false;
	}

	public virtual object e(object A_0)
	{
		if (A_0 is DbDataRowView dbDataRowView)
		{
			if (this.m_a != null && dbDataRowView.Row != null && dbDataRowView.Row.Table != this.m_a.Table)
			{
				this.m_a = dbDataRowView.Row.Table.Columns[this.m_a.ColumnName];
			}
			return dbDataRowView.a(this.m_a, this);
		}
		return DBNull.Value;
	}

	public virtual void a(object A_0)
	{
	}

	public virtual void a(object A_0, object A_1)
	{
		if (A_0 is DbDataRowView dbDataRowView)
		{
			dbDataRowView.a(a(), A_1, this);
		}
	}

	public virtual bool d(object A_0)
	{
		return true;
	}

	[SpecialName]
	public virtual TypeConverter f()
	{
		Type dataType = this.m_a.DataType;
		object[] customAttributes = dataType.GetCustomAttributes(typeof(TypeConverterAttribute), inherit: true);
		if (customAttributes != null && customAttributes.Length > 0)
		{
			return (TypeConverter)Activator.CreateInstance(Type.GetType(((TypeConverterAttribute)customAttributes[0]).ConverterTypeName));
		}
		return base.Converter;
	}

	[SpecialName]
	internal DataRelation c()
	{
		return this.m_b;
	}

	[SpecialName]
	internal DataColumn a()
	{
		return this.m_a;
	}

	[SpecialName]
	internal void a(DataColumn A_0)
	{
		this.m_a = A_0;
	}

	public virtual bool b(object A_0)
	{
		if (A_0 is g)
		{
			g g2 = (g)A_0;
			if (g2.c() == c())
			{
				return g2.a() == a();
			}
			return false;
		}
		return false;
	}

	public virtual int e()
	{
		if (c() == null)
		{
			return a().GetHashCode();
		}
		return c().GetHashCode() ^ a().GetHashCode();
	}

	[SpecialName]
	public virtual string get_DisplayName()
	{
		if (this.m_a == null)
		{
			return base.DisplayName;
		}
		return this.m_a.Caption;
	}
}
