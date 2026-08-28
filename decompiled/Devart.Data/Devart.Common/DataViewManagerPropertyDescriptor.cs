using System;
using System.ComponentModel;
using System.Data;

namespace Devart.Common;

public class DataViewManagerPropertyDescriptor : PropertyDescriptor
{
	private PropertyDescriptor a;

	private DbDataTable b;

	public override Type ComponentType => a.ComponentType;

	public override bool IsReadOnly => a.IsReadOnly;

	public override Type PropertyType => a.PropertyType;

	public override TypeConverter Converter => a.Converter;

	internal DbDataTable DataTable => b;

	public DataViewManagerPropertyDescriptor(PropertyDescriptor originalDescriptor, DbDataTable dataTable)
		: base(originalDescriptor.Name, null)
	{
		a = originalDescriptor;
		b = dataTable;
	}

	public override bool CanResetValue(object component)
	{
		return a.CanResetValue(component);
	}

	public override object GetValue(object component)
	{
		DataView dataView = (DataView)a.GetValue(component);
		return ((IListSource)dataView.Table).GetList();
	}

	public override void ResetValue(object component)
	{
		a.ResetValue(component);
	}

	public override void SetValue(object component, object value)
	{
		a.SetValue(component, value);
	}

	public override bool ShouldSerializeValue(object component)
	{
		return a.ShouldSerializeValue(component);
	}
}
