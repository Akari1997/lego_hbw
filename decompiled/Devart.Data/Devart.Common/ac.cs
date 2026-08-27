using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Devart.Common;

[DefaultMember("Item")]
internal class ac : PropertyDescriptorCollection
{
	public ac(PropertyDescriptor[] A_0)
		: base(A_0)
	{
	}

	[SpecialName]
	public virtual PropertyDescriptor a(int A_0)
	{
		return base[A_0];
	}

	[SpecialName]
	public virtual PropertyDescriptor a(string A_0)
	{
		PropertyDescriptor propertyDescriptor = base[A_0];
		if (propertyDescriptor == null)
		{
			return new s(A_0);
		}
		return propertyDescriptor;
	}

	public virtual PropertyDescriptor a(string A_0, bool A_1)
	{
		PropertyDescriptor propertyDescriptor = base.Find(A_0, A_1);
		if (propertyDescriptor == null)
		{
			return new s(A_0);
		}
		return propertyDescriptor;
	}

	public virtual IEnumerator a()
	{
		return base.GetEnumerator();
	}

	public virtual PropertyDescriptorCollection b()
	{
		return base.Sort();
	}

	public virtual PropertyDescriptorCollection a(IComparer A_0)
	{
		return base.Sort(A_0);
	}

	public virtual PropertyDescriptorCollection a(string[] A_0)
	{
		return base.Sort(A_0);
	}

	public virtual PropertyDescriptorCollection a(string[] A_0, IComparer A_1)
	{
		return base.Sort(A_0, A_1);
	}
}
