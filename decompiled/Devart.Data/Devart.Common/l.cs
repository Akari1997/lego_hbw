using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Devart.Common;

internal class l : ExpandableObjectConverter
{
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if ((object)destinationType == typeof(InstanceDescriptor))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if ((object)destinationType == null)
		{
			throw new ArgumentException("Invalid destination type");
		}
		if ((object)destinationType == typeof(InstanceDescriptor) && value is ParentDataRelation)
		{
			ParentDataRelation parentDataRelation = (ParentDataRelation)value;
			Type[] types = new Type[3]
			{
				typeof(DbDataTable),
				typeof(string[]),
				typeof(string[])
			};
			ConstructorInfo constructor = typeof(ParentDataRelation).GetConstructor(types);
			if ((object)constructor != null)
			{
				return new InstanceDescriptor(constructor, new object[3] { parentDataRelation.ParentTable, parentDataRelation.ParentColumnNames, parentDataRelation.ChildColumnNames });
			}
		}
		else if ((object)destinationType == typeof(string))
		{
			return "(ParentRelation)";
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
