using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Devart.Common;

internal class m : TypeConverter
{
	public virtual bool a(ITypeDescriptorContext A_0, Type A_1)
	{
		if ((object)A_1 == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(A_0, A_1);
	}

	public virtual bool b(ITypeDescriptorContext A_0, Type A_1)
	{
		if ((object)A_1 == typeof(string[]))
		{
			return true;
		}
		return base.CanConvertTo(A_0, A_1);
	}

	public virtual object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2)
	{
		if (A_2 == null || A_2 as string == "")
		{
			return null;
		}
		if (A_2 is string)
		{
			return ((string)A_2).Split(new char[1] { ';' });
		}
		return base.ConvertFrom(A_0, A_1, A_2);
	}

	public virtual object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2, Type A_3)
	{
		if ((object)A_3 == typeof(string) && A_2 is string[])
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = (string[])A_2;
			foreach (string value in array)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(";");
				}
				stringBuilder.Append(value);
			}
			return stringBuilder?.ToString();
		}
		return base.ConvertTo(A_0, A_1, A_2, A_3);
	}
}
