using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Devart.Common;

internal class ae : IDisposable, ITypedList, ICancelAddNew, IBindingListView
{
	private DbDataTableView m_a;

	private ListChangedEventHandler m_b;

	private bool m_c;

	private PropertyChangedEventHandler m_d;

	public ae(DbDataTableView A_0)
	{
		a(A_0);
		if (A_0 == null)
		{
			throw new ArgumentNullException();
		}
	}

	public void a()
	{
		GC.SuppressFinalize(this);
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a();
	}

	private IEnumerator g()
	{
		return ((IEnumerable)this.m_a).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in g
		return this.g();
	}

	private void a(Array A_0, int A_1)
	{
		((ICollection)this.m_a).CopyTo(A_0, A_1);
	}

	void ICollection.CopyTo(Array A_0, int A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0, A_1);
	}

	[SpecialName]
	private int n()
	{
		return ((ICollection)this.m_a).Count;
	}

	int ICollection.get_Count()
	{
		//ILSpy generated this explicit interface implementation from .override directive in n
		return this.n();
	}

	[SpecialName]
	private bool c()
	{
		return ((ICollection)this.m_a).IsSynchronized;
	}

	bool ICollection.get_IsSynchronized()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		return this.c();
	}

	[SpecialName]
	private object b()
	{
		return ((ICollection)this.m_a).SyncRoot;
	}

	object ICollection.get_SyncRoot()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b();
	}

	private int c(object A_0)
	{
		return ((IList)this.m_a).Add(A_0);
	}

	int IList.Add(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		return this.c(A_0);
	}

	private void u()
	{
		((IList)this.m_a).Clear();
	}

	void IList.Clear()
	{
		//ILSpy generated this explicit interface implementation from .override directive in u
		this.u();
	}

	private bool a(object A_0)
	{
		return ((IList)this.m_a).Contains(A_0);
	}

	bool IList.Contains(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0);
	}

	private int b(object A_0)
	{
		return ((IList)this.m_a).IndexOf(A_0);
	}

	int IList.IndexOf(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b(A_0);
	}

	private void b(int A_0, object A_1)
	{
		((IList)this.m_a).Insert(A_0, A_1);
	}

	void IList.Insert(int A_0, object A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0, A_1);
	}

	private void d(object A_0)
	{
		((IList)this.m_a).Remove(A_0);
	}

	void IList.Remove(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		this.d(A_0);
	}

	private void c(int A_0)
	{
		((IList)this.m_a).RemoveAt(A_0);
	}

	void IList.RemoveAt(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		this.c(A_0);
	}

	[SpecialName]
	private bool j()
	{
		return ((IList)this.m_a).IsFixedSize;
	}

	bool IList.get_IsFixedSize()
	{
		//ILSpy generated this explicit interface implementation from .override directive in j
		return this.j();
	}

	[SpecialName]
	private bool e()
	{
		return ((IList)this.m_a).IsReadOnly;
	}

	bool IList.get_IsReadOnly()
	{
		//ILSpy generated this explicit interface implementation from .override directive in e
		return this.e();
	}

	[SpecialName]
	private object a(int A_0)
	{
		return ((IList)this.m_a)[A_0];
	}

	object IList.get_Item(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0);
	}

	[SpecialName]
	private void a(int A_0, object A_1)
	{
		((IList)this.m_a)[A_0] = A_1;
	}

	void IList.set_Item(int A_0, object A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0, A_1);
	}

	[SpecialName]
	private bool q()
	{
		return ((IBindingList)this.m_a).AllowEdit;
	}

	bool IBindingList.get_AllowEdit()
	{
		//ILSpy generated this explicit interface implementation from .override directive in q
		return this.q();
	}

	[SpecialName]
	private bool aa()
	{
		return ((IBindingList)this.m_a).AllowNew;
	}

	bool IBindingList.get_AllowNew()
	{
		//ILSpy generated this explicit interface implementation from .override directive in aa
		return this.aa();
	}

	[SpecialName]
	private bool f()
	{
		return ((IBindingList)this.m_a).AllowRemove;
	}

	bool IBindingList.get_AllowRemove()
	{
		//ILSpy generated this explicit interface implementation from .override directive in f
		return this.f();
	}

	[SpecialName]
	private bool m()
	{
		return ((IBindingList)this.m_a).IsSorted;
	}

	bool IBindingList.get_IsSorted()
	{
		//ILSpy generated this explicit interface implementation from .override directive in m
		return this.m();
	}

	[SpecialName]
	private ListSortDirection y()
	{
		return ((IBindingList)this.m_a).SortDirection;
	}

	ListSortDirection IBindingList.get_SortDirection()
	{
		//ILSpy generated this explicit interface implementation from .override directive in y
		return this.y();
	}

	[SpecialName]
	private PropertyDescriptor z()
	{
		return ((IBindingList)this.m_a).SortProperty;
	}

	PropertyDescriptor IBindingList.get_SortProperty()
	{
		//ILSpy generated this explicit interface implementation from .override directive in z
		return this.z();
	}

	[SpecialName]
	private bool s()
	{
		return ((IBindingList)this.m_a).SupportsChangeNotification;
	}

	bool IBindingList.get_SupportsChangeNotification()
	{
		//ILSpy generated this explicit interface implementation from .override directive in s
		return this.s();
	}

	[SpecialName]
	private bool l()
	{
		return ((IBindingList)this.m_a).SupportsSearching;
	}

	bool IBindingList.get_SupportsSearching()
	{
		//ILSpy generated this explicit interface implementation from .override directive in l
		return this.l();
	}

	[SpecialName]
	private bool t()
	{
		return ((IBindingList)this.m_a).SupportsSorting;
	}

	bool IBindingList.get_SupportsSorting()
	{
		//ILSpy generated this explicit interface implementation from .override directive in t
		return this.t();
	}

	private void b(PropertyDescriptor A_0)
	{
		((IBindingList)this.m_a).AddIndex(A_0);
	}

	void IBindingList.AddIndex(PropertyDescriptor A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0);
	}

	private object p()
	{
		return ((IBindingList)this.m_a).AddNew();
	}

	object IBindingList.AddNew()
	{
		//ILSpy generated this explicit interface implementation from .override directive in p
		return this.p();
	}

	private void a(PropertyDescriptor A_0, ListSortDirection A_1)
	{
		((IBindingList)this.m_a).ApplySort(A_0, A_1);
	}

	void IBindingList.ApplySort(PropertyDescriptor A_0, ListSortDirection A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0, A_1);
	}

	private int a(PropertyDescriptor A_0, object A_1)
	{
		return ((IBindingList)this.m_a).Find(A_0, A_1);
	}

	int IBindingList.Find(PropertyDescriptor A_0, object A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0, A_1);
	}

	private void a(PropertyDescriptor A_0)
	{
		((IBindingList)this.m_a).RemoveIndex(A_0);
	}

	void IBindingList.RemoveIndex(PropertyDescriptor A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	private void x()
	{
		((IBindingList)this.m_a).RemoveSort();
	}

	void IBindingList.RemoveSort()
	{
		//ILSpy generated this explicit interface implementation from .override directive in x
		this.x();
	}

	internal void a(object A_0, ListChangedEventArgs A_1)
	{
		if (this.m_b == null || (this.m_a.f != null && this.m_a.f.disableEvents > 0))
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

	private PropertyDescriptorCollection b(PropertyDescriptor[] A_0)
	{
		return ((ITypedList)this.m_a).GetItemProperties(A_0);
	}

	PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b(A_0);
	}

	private string a(PropertyDescriptor[] A_0)
	{
		return ((ITypedList)this.m_a).GetListName(A_0);
	}

	string ITypedList.GetListName(PropertyDescriptor[] A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0);
	}

	private void b(int A_0)
	{
		((ICancelAddNew)this.m_a).CancelNew(A_0);
	}

	void ICancelAddNew.CancelNew(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0);
	}

	private void d(int A_0)
	{
		((ICancelAddNew)this.m_a).EndNew(A_0);
	}

	void ICancelAddNew.EndNew(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		this.d(A_0);
	}

	internal void c(DbDataTableView A_0)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException();
		}
		if (!a(this.m_a, A_0))
		{
			a(A_0);
			a(this, new ListChangedEventArgs(ListChangedType.Reset, null));
			a(this, new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
		}
	}

	private bool a(DbDataTableView A_0, DbDataTableView A_1)
	{
		if (!k())
		{
			return A_0 == A_1;
		}
		if ((A_0.g == null && A_1.g == null) || (A_0.DataTable == null && A_1.DataTable == null))
		{
			return true;
		}
		if (A_0.g == null || A_1.g == null || A_0.DataTable == null || A_1.DataTable == null)
		{
			return false;
		}
		if (A_0.f == A_1.f && (object)A_0.g.GetType() == A_1.g.GetType())
		{
			if (!(A_0.g.GetType().Name != "RelatedView"))
			{
				return A_0.g == A_1.g;
			}
			return true;
		}
		return false;
	}

	private DbDataTableView b(DbDataTableView A_0)
	{
		if (!k())
		{
			return A_0;
		}
		if (A_0.g == null || A_0.DataTable == null)
		{
			return A_0;
		}
		if ((object)A_0.g.GetType() == typeof(DataView))
		{
			return new DbDataTableView(A_0.f, new DataView(A_0.f));
		}
		DbDataRowView dbDataRowView = A_0.h;
		DataRelation relation = null;
		foreach (DictionaryEntry item in dbDataRowView.RelationViews)
		{
			if (((ak)item.Value).a == A_0)
			{
				relation = (DataRelation)item.Key;
				break;
			}
		}
		return new DbDataTableView(A_0.f, dbDataRowView.InnerRowView.CreateChildView(relation));
	}

	[SpecialName]
	internal CurrencyManager w()
	{
		return this.m_a.CurrencyManager;
	}

	internal CurrencyManager b(string A_0)
	{
		return this.m_a.a(A_0);
	}

	private void a(ListSortDescriptionCollection A_0)
	{
		((IBindingListView)this.m_a).ApplySort(A_0);
	}

	void IBindingListView.ApplySort(ListSortDescriptionCollection A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	[SpecialName]
	private string d()
	{
		return ((IBindingListView)this.m_a).Filter;
	}

	string IBindingListView.get_Filter()
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		return this.d();
	}

	[SpecialName]
	private void a(string A_0)
	{
		((IBindingListView)this.m_a).Filter = A_0;
	}

	void IBindingListView.set_Filter(string A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	private void o()
	{
		((IBindingListView)this.m_a).RemoveFilter();
	}

	void IBindingListView.RemoveFilter()
	{
		//ILSpy generated this explicit interface implementation from .override directive in o
		this.o();
	}

	[SpecialName]
	private ListSortDescriptionCollection i()
	{
		return ((IBindingListView)this.m_a).SortDescriptions;
	}

	ListSortDescriptionCollection IBindingListView.get_SortDescriptions()
	{
		//ILSpy generated this explicit interface implementation from .override directive in i
		return this.i();
	}

	[SpecialName]
	private bool v()
	{
		return ((IBindingListView)this.m_a).SupportsAdvancedSorting;
	}

	bool IBindingListView.get_SupportsAdvancedSorting()
	{
		//ILSpy generated this explicit interface implementation from .override directive in v
		return this.v();
	}

	[SpecialName]
	private bool r()
	{
		return ((IBindingListView)this.m_a).SupportsFiltering;
	}

	bool IBindingListView.get_SupportsFiltering()
	{
		//ILSpy generated this explicit interface implementation from .override directive in r
		return this.r();
	}

	[SpecialName]
	public bool k()
	{
		return this.m_c;
	}

	[SpecialName]
	public void a(bool A_0)
	{
		if (this.m_c != A_0)
		{
			this.m_c = A_0;
			if (this.m_c)
			{
				a(this.m_a);
			}
		}
	}

	internal void a(object A_0, PropertyChangedEventArgs A_1)
	{
		if (this.m_d != null)
		{
			this.m_d(A_0, A_1);
		}
	}

	[SpecialName]
	public void b(PropertyChangedEventHandler A_0)
	{
		if (this.m_d == null && this.m_a != null)
		{
			this.m_a.PropertyChanged += a;
		}
		this.m_d = (PropertyChangedEventHandler)Delegate.Combine(this.m_d, A_0);
	}

	[SpecialName]
	public void a(PropertyChangedEventHandler A_0)
	{
		this.m_d = (PropertyChangedEventHandler)Delegate.Remove(this.m_d, A_0);
		if (this.m_d == null && this.m_a != null)
		{
			this.m_a.PropertyChanged -= a;
		}
	}

	private void a(DbDataTableView A_0)
	{
		if (this.m_a != null)
		{
			((IBindingList)this.m_a).ListChanged -= a;
			if (this.m_d != null)
			{
				this.m_a.PropertyChanged -= a;
			}
		}
		this.m_a = b(A_0);
		if (this.m_a != null)
		{
			((IBindingList)this.m_a).ListChanged += a;
			if (this.m_d != null)
			{
				this.m_a.PropertyChanged += a;
			}
		}
	}

	[SpecialName]
	public DbDataTableView h()
	{
		return this.m_a;
	}
}
