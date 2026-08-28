using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace Devart.Common;

public class DbConnectionStringBuilder : System.Data.Common.DbConnectionStringBuilder, ICustomTypeDescriptor
{
	protected string initializationCommandInternal;

	private PropertyDescriptorCollection m_a;

	public override object this[string keyword]
	{
		get
		{
			return base[keyword];
		}
		set
		{
			if (value == null || (value is string && (string)value == string.Empty))
			{
				Remove(keyword);
			}
			else
			{
				base[keyword] = value;
			}
		}
	}

	[DisplayName("Initialization Command")]
	[r("DbConnectionString_InitializationCommand")]
	[Category("Provider Behaviour")]
	[RefreshProperties(RefreshProperties.All)]
	public string InitializationCommand
	{
		get
		{
			return initializationCommandInternal;
		}
		set
		{
			SetValue("Initialization Command", value);
			initializationCommandInternal = value;
		}
	}

	protected internal new void ClearPropertyDescriptors()
	{
		this.m_a = null;
	}

	public virtual bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder, bool loginOnly)
	{
		return EquivalentTo(connectionStringBuilder);
	}

	private PropertyDescriptorCollection a(Hashtable A_0)
	{
		ICollection keys = Keys;
		PropertyDescriptor[] array = new PropertyDescriptor[keys.Count];
		object[] array2 = new object[keys.Count];
		keys.CopyTo(array2, 0);
		int num = 0;
		for (int i = 0; i < array2.Length; i++)
		{
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)A_0[array2[i]];
			if (propertyDescriptor != null)
			{
				array[num++] = propertyDescriptor;
			}
		}
		PropertyDescriptor[] array3 = new PropertyDescriptor[num];
		Array.Copy(array, array3, num);
		return new PropertyDescriptorCollection(array3);
	}

	private PropertyDescriptorCollection a()
	{
		PropertyDescriptorCollection propertyDescriptorCollection = this.m_a;
		if (propertyDescriptorCollection == null)
		{
			Hashtable hashtable = new Hashtable();
			GetProperties(hashtable);
			propertyDescriptorCollection = (this.m_a = a(hashtable));
		}
		return propertyDescriptorCollection;
	}

	private PropertyDescriptorCollection a(Attribute[] A_0)
	{
		PropertyDescriptorCollection propertyDescriptorCollection = a();
		if (A_0 == null || A_0.Length == 0)
		{
			return propertyDescriptorCollection;
		}
		PropertyDescriptor[] array = new PropertyDescriptor[propertyDescriptorCollection.Count];
		int num = 0;
		foreach (PropertyDescriptor item in propertyDescriptorCollection)
		{
			bool flag = true;
			foreach (Attribute attribute in A_0)
			{
				Attribute attribute2 = item.Attributes[attribute.GetType()];
				if ((attribute2 == null && !attribute.IsDefaultAttribute()) || !attribute2.Match(attribute))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				array[num] = item;
				num++;
			}
		}
		PropertyDescriptor[] array2 = new PropertyDescriptor[num];
		Array.Copy(array, array2, num);
		return new PropertyDescriptorCollection(array2);
	}

	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
	{
		return a();
	}

	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
	{
		return a(attributes);
	}

	protected void SetValue(string keyword, bool value)
	{
		a(keyword, value.ToString(null));
	}

	protected void SetValue(string keyword, int value)
	{
		a(keyword, value.ToString((IFormatProvider?)null));
	}

	protected void SetValue(string keyword, string value)
	{
		Utils.CheckArgumentNull(value, keyword);
		a(keyword, value);
	}

	private void a(string A_0, string A_1)
	{
		if (string.IsNullOrEmpty(A_1))
		{
			Remove(A_0);
		}
		else
		{
			base[A_0] = A_1;
		}
	}
}
