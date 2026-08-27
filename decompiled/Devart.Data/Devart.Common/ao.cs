using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class ao : IBindingList, ITypedList
{
	internal DataViewManager a;

	private ListChangedEventHandler m_b;

	private PropertyDescriptorCollection m_c;

	public ao(DataViewManager A_0)
	{
		this.a = A_0;
	}

	private IEnumerator e()
	{
		return ((IEnumerable)this.a).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in e
		return this.e();
	}

	private void a(Array A_0, int A_1)
	{
		((ICollection)this.a).CopyTo(A_0, A_1);
	}

	void ICollection.CopyTo(Array A_0, int A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0, A_1);
	}

	[SpecialName]
	private int i()
	{
		return ((ICollection)this.a).Count;
	}

	int ICollection.get_Count()
	{
		//ILSpy generated this explicit interface implementation from .override directive in i
		return this.i();
	}

	[SpecialName]
	private bool b()
	{
		return ((ICollection)this.a).IsSynchronized;
	}

	bool ICollection.get_IsSynchronized()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b();
	}

	[SpecialName]
	private object a()
	{
		return ((ICollection)this.a).SyncRoot;
	}

	object ICollection.get_SyncRoot()
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a();
	}

	private int c(object A_0)
	{
		return ((IList)this.a).Add(A_0);
	}

	int IList.Add(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		return this.c(A_0);
	}

	private void n()
	{
		((IList)this.a).Clear();
	}

	void IList.Clear()
	{
		//ILSpy generated this explicit interface implementation from .override directive in n
		this.n();
	}

	private bool a(object A_0)
	{
		return ((IList)this.a).Contains(A_0);
	}

	bool IList.Contains(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0);
	}

	private int b(object A_0)
	{
		return ((IList)this.a).IndexOf(A_0);
	}

	int IList.IndexOf(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b(A_0);
	}

	private void b(int A_0, object A_1)
	{
		((IList)this.a).Insert(A_0, A_1);
	}

	void IList.Insert(int A_0, object A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0, A_1);
	}

	private void d(object A_0)
	{
		((IList)this.a).Remove(A_0);
	}

	void IList.Remove(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		this.d(A_0);
	}

	private void b(int A_0)
	{
		((IList)this.a).RemoveAt(A_0);
	}

	void IList.RemoveAt(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0);
	}

	[SpecialName]
	private bool f()
	{
		return ((IList)this.a).IsFixedSize;
	}

	bool IList.get_IsFixedSize()
	{
		//ILSpy generated this explicit interface implementation from .override directive in f
		return this.f();
	}

	[SpecialName]
	private bool c()
	{
		return ((IList)this.a).IsReadOnly;
	}

	bool IList.get_IsReadOnly()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		return this.c();
	}

	[SpecialName]
	private object a(int A_0)
	{
		return ((IList)this.a)[A_0];
	}

	object IList.get_Item(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0);
	}

	[SpecialName]
	private void a(int A_0, object A_1)
	{
		((IList)this.a)[A_0] = A_1;
	}

	void IList.set_Item(int A_0, object A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0, A_1);
	}

	[SpecialName]
	private bool k()
	{
		return ((IBindingList)this.a).AllowEdit;
	}

	bool IBindingList.get_AllowEdit()
	{
		//ILSpy generated this explicit interface implementation from .override directive in k
		return this.k();
	}

	[SpecialName]
	private bool r()
	{
		return ((IBindingList)this.a).AllowNew;
	}

	bool IBindingList.get_AllowNew()
	{
		//ILSpy generated this explicit interface implementation from .override directive in r
		return this.r();
	}

	[SpecialName]
	private bool d()
	{
		return ((IBindingList)this.a).AllowRemove;
	}

	bool IBindingList.get_AllowRemove()
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		return this.d();
	}

	[SpecialName]
	private bool h()
	{
		return ((IBindingList)this.a).IsSorted;
	}

	bool IBindingList.get_IsSorted()
	{
		//ILSpy generated this explicit interface implementation from .override directive in h
		return this.h();
	}

	[SpecialName]
	private ListSortDirection p()
	{
		return ((IBindingList)this.a).SortDirection;
	}

	ListSortDirection IBindingList.get_SortDirection()
	{
		//ILSpy generated this explicit interface implementation from .override directive in p
		return this.p();
	}

	[SpecialName]
	private PropertyDescriptor q()
	{
		return ((IBindingList)this.a).SortProperty;
	}

	PropertyDescriptor IBindingList.get_SortProperty()
	{
		//ILSpy generated this explicit interface implementation from .override directive in q
		return this.q();
	}

	[SpecialName]
	private bool l()
	{
		return ((IBindingList)this.a).SupportsChangeNotification;
	}

	bool IBindingList.get_SupportsChangeNotification()
	{
		//ILSpy generated this explicit interface implementation from .override directive in l
		return this.l();
	}

	[SpecialName]
	private bool g()
	{
		return ((IBindingList)this.a).SupportsSearching;
	}

	bool IBindingList.get_SupportsSearching()
	{
		//ILSpy generated this explicit interface implementation from .override directive in g
		return this.g();
	}

	[SpecialName]
	private bool m()
	{
		return ((IBindingList)this.a).SupportsSorting;
	}

	bool IBindingList.get_SupportsSorting()
	{
		//ILSpy generated this explicit interface implementation from .override directive in m
		return this.m();
	}

	private void b(PropertyDescriptor A_0)
	{
		((IBindingList)this.a).AddIndex(A_0);
	}

	void IBindingList.AddIndex(PropertyDescriptor A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0);
	}

	private object j()
	{
		return ((IBindingList)this.a).AddNew();
	}

	object IBindingList.AddNew()
	{
		//ILSpy generated this explicit interface implementation from .override directive in j
		return this.j();
	}

	private void a(PropertyDescriptor A_0, ListSortDirection A_1)
	{
		((IBindingList)this.a).ApplySort(A_0, A_1);
	}

	void IBindingList.ApplySort(PropertyDescriptor A_0, ListSortDirection A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0, A_1);
	}

	private int a(PropertyDescriptor A_0, object A_1)
	{
		return ((IBindingList)this.a).Find(A_0, A_1);
	}

	int IBindingList.Find(PropertyDescriptor A_0, object A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0, A_1);
	}

	private void a(PropertyDescriptor A_0)
	{
		((IBindingList)this.a).RemoveIndex(A_0);
	}

	void IBindingList.RemoveIndex(PropertyDescriptor A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	private void o()
	{
		((IBindingList)this.a).RemoveSort();
	}

	void IBindingList.RemoveSort()
	{
		//ILSpy generated this explicit interface implementation from .override directive in o
		this.o();
	}

	[SpecialName]
	private void b(ListChangedEventHandler A_0)
	{
		this.m_b = (ListChangedEventHandler)Delegate.Combine(this.m_b, A_0);
	}

	void IBindingList.add_ListChanged(ListChangedEventHandler A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0);
	}

	[SpecialName]
	private void a(ListChangedEventHandler A_0)
	{
		this.m_b = (ListChangedEventHandler)Delegate.Remove(this.m_b, A_0);
	}

	void IBindingList.remove_ListChanged(ListChangedEventHandler A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	internal void a(object A_0, ListChangedEventArgs A_1)
	{
		this.m_c = null;
		if (this.m_b == null)
		{
			return;
		}
		try
		{
			this.m_b(A_0, A_1);
		}
		catch
		{
		}
	}

	private PropertyDescriptorCollection b(PropertyDescriptor[] A_0)
	{
		PropertyDescriptorCollection itemProperties;
		if (A_0 == null || A_0.Length == 0)
		{
			if (this.m_c == null)
			{
				itemProperties = ((ITypedList)this.a).GetItemProperties(A_0);
				this.m_c = new PropertyDescriptorCollection(null);
				for (int i = 0; i < itemProperties.Count; i++)
				{
					if (!(this.a.DataSet.Tables[i] is DbDataTable))
					{
						this.m_c.Add(itemProperties[i]);
						continue;
					}
					PropertyDescriptor value = new DataViewManagerPropertyDescriptor(itemProperties[i], (DbDataTable)this.a.DataSet.Tables[i]);
					this.m_c.Add(value);
				}
			}
			return this.m_c;
		}
		itemProperties = ((ITypedList)this.a).GetItemProperties(A_0);
		DbDataTable dbDataTable2 = ((DbDataSet)this.a.DataSet).a(null, A_0, 0, out var A_1);
		if (dbDataTable2 != null)
		{
			PropertyDescriptor[] array;
			if (A_1 < A_0.Length - 1)
			{
				array = new PropertyDescriptor[A_0.Length - 1 - A_1];
				for (int num = A_1 + 1; num < A_0.Length; num++)
				{
					array[num - A_1 - 1] = A_0[num];
				}
			}
			else
			{
				array = null;
			}
			return dbDataTable2.c(array);
		}
		return itemProperties;
	}

	PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b(A_0);
	}

	private string a(PropertyDescriptor[] A_0)
	{
		return ((ITypedList)this.a).GetListName(A_0);
	}

	string ITypedList.GetListName(PropertyDescriptor[] A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0);
	}
}
