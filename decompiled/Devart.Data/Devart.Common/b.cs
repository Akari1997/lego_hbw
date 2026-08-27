using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Devart.Common;

internal class b : ExpandableObjectConverter
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
		if ((object)destinationType == typeof(InstanceDescriptor) && value is ProxyOptions)
		{
			ProxyOptions proxyOptions = (ProxyOptions)value;
			Type[] types = new Type[4]
			{
				typeof(string),
				typeof(int),
				typeof(string),
				typeof(string)
			};
			ConstructorInfo constructor = typeof(ProxyOptions).GetConstructor(types);
			if ((object)constructor != null)
			{
				return new InstanceDescriptor(constructor, new object[4] { proxyOptions.Host, proxyOptions.Port, proxyOptions.User, proxyOptions.Password });
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
