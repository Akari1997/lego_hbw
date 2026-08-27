using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Devart.Common;

public class DbDataTableView : IDisposable, ITypedList, ICancelAddNew, IBindingListView
{
	private bool m_a;

	private ListChangedEventHandler m_b;

	private DbDataRowView m_c;

	private DbDataRowView m_d;

	private int e;

	internal DbDataTable f;

	internal DataView g;

	internal DbDataRowView h;

	private ArrayList m_i;

	private ArrayList m_j;

	private BindingSource m_k;

	internal int l;

	private ArrayList m_m = new ArrayList();

	private EventHandlerList n;

	private static readonly object o = new object();

	private static readonly object p = new object();

	private static readonly object q = new object();

	private static readonly object r = new object();

	private static readonly object s = new object();

	private static readonly object t = new object();

	internal ListChangedEventArgs u;

	private MethodInfo v;

	int ICollection.Count
	{
		get
		{
			int num = 0;
			if (this.f != null && g != null)
			{
				lock (this.f.FetchRowSyncRoot)
				{
					if (g.RowFilter != "")
					{
						this.f.b(A_0: false);
					}
					if (this.f.QueryRecordCount)
					{
						if (!this.m_a)
						{
							num = ((!this.f.FetchComplete && !(g.RowFilter != "")) ? this.f.RecordCount : g.Count);
						}
						else
						{
							this.f.b(A_0: false);
							num = g.Count;
						}
					}
					else
					{
						num += g.Count + ((!this.f.FetchComplete && (this.m_c == null || !this.f.AllowCruidDuringFetch) && !(g.RowFilter != "")) ? 1 : 0);
					}
				}
			}
			return num;
		}
	}

	bool ICollection.IsSynchronized => false;

	object ICollection.SyncRoot => this;

	private BindingSource BindingSource
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			if (this.m_k == null)
			{
				this.m_k = new BindingSource();
				this.m_k.DataSource = this.f;
			}
			return this.m_k;
		}
	}

	bool IList.IsFixedSize => false;

	bool IList.IsReadOnly => false;

	object IList.this[int index]
	{
		get
		{
			return a(index, A_1: true);
		}
		set
		{
			if (this.f != null && g != null && index == g.Count - 1 && this.m_c != null)
			{
				AddNewRow = (DbDataRowView)value;
			}
		}
	}

	bool IBindingList.AllowEdit
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).AllowEdit;
			}
			return false;
		}
	}

	bool IBindingList.AllowNew
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).AllowNew;
			}
			return false;
		}
	}

	bool IBindingList.AllowRemove
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).AllowRemove;
			}
			return false;
		}
	}

	bool IBindingList.IsSorted
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).IsSorted;
			}
			return false;
		}
	}

	ListSortDirection IBindingList.SortDirection
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).SortDirection;
			}
			return ListSortDirection.Ascending;
		}
	}

	PropertyDescriptor IBindingList.SortProperty
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).SortProperty;
			}
			return null;
		}
	}

	bool IBindingList.SupportsChangeNotification
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).SupportsChangeNotification;
			}
			return true;
		}
	}

	bool IBindingList.SupportsSearching
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).SupportsSearching;
			}
			return false;
		}
	}

	bool IBindingList.SupportsSorting
	{
		get
		{
			if (g != null)
			{
				return ((IBindingList)g).SupportsSorting;
			}
			return false;
		}
	}

	protected EventHandlerList Events
	{
		get
		{
			if (n == null)
			{
				n = new EventHandlerList();
			}
			return n;
		}
	}

	internal DbDataRowView AddNewRow
	{
		get
		{
			return this.m_c;
		}
		set
		{
			if (this.m_c != value)
			{
				if (this.m_c == null && value != null)
				{
					this.f.y();
				}
				if (this.m_c != null && value == null)
				{
					this.f.t();
				}
				this.m_c = value;
			}
		}
	}

	public DbDataTable DataTable => this.f;

	internal int CurrentIndex => e;

	internal DbDataRowView CurrentRowView => this.m_d;

	private ArrayList RowViewCache
	{
		get
		{
			if (this.m_i == null)
			{
				this.m_i = new ArrayList();
			}
			return this.m_i;
		}
	}

	internal bool IsDetailView => this.m_a;

	internal CurrencyManager CurrencyManager => BindingSource.CurrencyManager;

	string IBindingListView.Filter
	{
		get
		{
			if (g != null)
			{
				return ((IBindingListView)g).Filter;
			}
			return null;
		}
		set
		{
			if (g != null)
			{
				((IBindingListView)g).Filter = value;
			}
		}
	}

	ListSortDescriptionCollection IBindingListView.SortDescriptions
	{
		get
		{
			if (g != null)
			{
				return ((IBindingListView)g).SortDescriptions;
			}
			return null;
		}
	}

	bool IBindingListView.SupportsAdvancedSorting
	{
		get
		{
			if (g != null)
			{
				return ((IBindingListView)g).SupportsAdvancedSorting;
			}
			return false;
		}
	}

	bool IBindingListView.SupportsFiltering
	{
		get
		{
			if (g != null)
			{
				return ((IBindingListView)g).SupportsFiltering;
			}
			return false;
		}
	}

	event ListChangedEventHandler IBindingList.ListChanged
	{
		add
		{
			Events.AddHandler(o, value);
		}
		remove
		{
			Events.RemoveHandler(o, value);
		}
	}

	public event PropertyChangedEventHandler PropertyChanged
	{
		add
		{
			Events.AddHandler(t, value);
		}
		remove
		{
			Events.RemoveHandler(t, value);
		}
	}

	public DbDataTableView()
		: this(null, null)
	{
	}

	public DbDataTableView(DbDataTable table)
		: this(table, new DataView(table))
	{
	}

	public DbDataTableView(DbDataTable table, string RowFilter, string Sort, DataViewRowState RowState)
		: this(table, new DataView(table, RowFilter, Sort, RowState))
	{
	}

	internal DbDataTableView(DbDataTable A_0, DataView A_1)
	{
		this.f = A_0;
		g = A_1;
		A_0?.a(this);
		this.m_b = b;
		if (A_0 != null && A_1 != null)
		{
			this.m_a = A_1.GetType().Name == "RelatedView";
			((IBindingList)A_1).ListChanged += this.m_b;
		}
		e = -1;
	}

	~DbDataTableView()
	{
		Dispose(disposing: false);
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (this.f != null && g != null)
		{
			((IBindingList)g).ListChanged -= this.m_b;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		if (this.f != null)
		{
			this.f.b(A_0: false);
		}
		int count = ((ICollection)this).Count;
		DbDataRowView[] array = new DbDataRowView[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = (DbDataRowView)((IList)this)[i];
		}
		return array.GetEnumerator();
	}

	void ICollection.CopyTo(Array array, int index)
	{
		if (this.f != null)
		{
			this.f.b(A_0: false);
		}
		int count = ((ICollection)this).Count;
		for (int i = 0; i < count; i++)
		{
			array.SetValue(((IList)this)[i], index + i);
		}
	}

	int IList.Add(object value)
	{
		return 0;
	}

	void IList.Clear()
	{
	}

	bool IList.Contains(object value)
	{
		return false;
	}

	int IList.IndexOf(object value)
	{
		return 0;
	}

	void IList.Insert(int index, object value)
	{
	}

	void IList.Remove(object value)
	{
		if (!(value is DbDataRowView dbDataRowView))
		{
			throw new ArgumentException();
		}
		((IList)this).RemoveAt(dbDataRowView.Index);
	}

	void IList.RemoveAt(int index)
	{
		if (this.f == null || g == null || index >= g.Count)
		{
			return;
		}
		if (this.m_c != null && index == g.Count - 1)
		{
			m();
			return;
		}
		DataRowView dataRowView = g[index];
		DataRow row = dataRowView.Row;
		this.f.disableEvents++;
		try
		{
			this.f.c(row);
			if (index < RowViewCache.Count)
			{
				RowViewCache.RemoveAt(index);
			}
		}
		finally
		{
			this.f.disableEvents--;
		}
		f();
	}

	private object a(int A_0, bool A_1)
	{
		if (this.m_c != null && this.m_c.Index == A_0)
		{
			return this.m_c;
		}
		DataRowView dataRowView = null;
		DataRow dataRow = null;
		if (this.f != null && g != null)
		{
			if (this.m_a || g.RowFilter != "")
			{
				this.f.b(A_0: false);
			}
			else if (A_0 + 100 >= g.Count - 1)
			{
				this.f.a(A_0 + 100, A_1, A_2: false);
			}
			lock (this.f.FetchRowSyncRoot)
			{
				if (A_0 == g.Count - 1 && this.m_c != null)
				{
					return this.m_c;
				}
				if (A_0 < 0)
				{
					return new DbDataRowView(null, this, null, A_0);
				}
				if (A_0 < g.Count && A_0 >= 0)
				{
					dataRowView = g[A_0];
					dataRow = dataRowView.Row;
				}
				DbDataRowView dbDataRowView = a(A_0);
				if (dbDataRowView == null)
				{
					dbDataRowView = (DbDataRowView)(this.m_i[A_0] = new DbDataRowView(dataRow, this, dataRowView, A_0));
				}
				else
				{
					if (dataRow != dbDataRowView.Row)
					{
						dbDataRowView.Row = dataRow;
					}
					if (dataRowView != dbDataRowView.InnerRowView)
					{
						dbDataRowView.InnerRowView = dataRowView;
					}
				}
				dbDataRowView.IndexInternal = A_0;
				return dbDataRowView;
			}
		}
		return new DbDataRowView(dataRow, this, dataRowView, A_0);
	}

	void IBindingList.AddIndex(PropertyDescriptor property)
	{
		if (g != null)
		{
			((IBindingList)g).AddIndex(property);
		}
	}

	object IBindingList.AddNew()
	{
		if (this.m_c != null)
		{
			m();
		}
		if (this.f != null && g != null && !this.f.FetchComplete)
		{
			if (this.f.AllowCruidDuringFetch)
			{
				this.f.a(((ICollection)this).Count - 1, A_1: false, A_2: false);
			}
			else
			{
				int count = g.Count;
				this.f.b(A_0: true);
				if (g.Count > count)
				{
					return ((IList)this)[count - 1];
				}
			}
		}
		int num = ((this.f != null) ? g.Count : 0);
		DataRowView dataRowView;
		DataRow dataRow;
		if (this.f != null && g != null)
		{
			this.f.disableEvents++;
			this.f.storeEvents++;
			try
			{
				dataRowView = g.AddNew();
				dataRow = dataRowView.Row;
			}
			finally
			{
				this.f.disableEvents--;
				this.f.storeEvents--;
			}
			if (RowViewCache.Count == g.Count)
			{
				DbDataRowView dbDataRowView = (DbDataRowView)RowViewCache[g.Count - 1];
				if (dbDataRowView != null && dbDataRowView.Row == dataRow && dbDataRowView.Index == num)
				{
					AddNewRow = dbDataRowView;
				}
				RowViewCache.Remove(g.Count - 1);
			}
		}
		else
		{
			dataRowView = null;
			dataRow = null;
		}
		if (this.m_c == null)
		{
			AddNewRow = new DbDataRowView(dataRow, this, dataRowView, num);
		}
		this.m_c.e = true;
		f();
		return this.m_c;
	}

	void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
	{
		if (g != null && property is g)
		{
			this.f.b(A_0: true);
			((IBindingList)g).ApplySort(property, direction);
		}
	}

	int IBindingList.Find(PropertyDescriptor property, object key)
	{
		if (g != null)
		{
			return ((IBindingList)g).Find(property, key);
		}
		return 0;
	}

	void IBindingList.RemoveIndex(PropertyDescriptor property)
	{
		if (g != null)
		{
			((IBindingList)g).RemoveIndex(property);
		}
	}

	void IBindingList.RemoveSort()
	{
		if (g != null)
		{
			((IBindingList)g).RemoveSort();
		}
	}

	internal void f()
	{
		if (this.f != null)
		{
			this.f.aa();
		}
	}

	internal bool i()
	{
		if (l > 0 && l - 1 < this.m_m.Count)
		{
			ArrayList arrayList = (ArrayList)this.m_m[l - 1];
			if (arrayList.Count > 0)
			{
				ListChangedEventArgs a_ = (ListChangedEventArgs)arrayList[0];
				arrayList.RemoveAt(0);
				for (int num = l - 1; num >= 0; num--)
				{
					arrayList = (ArrayList)this.m_m[num];
					if (arrayList.Count != 0)
					{
						break;
					}
					this.m_m.RemoveAt(num);
				}
				a(this, a_);
				return true;
			}
		}
		return false;
	}

	internal void b(int A_0)
	{
		if (l < 0 || l >= this.m_m.Count)
		{
			return;
		}
		ArrayList arrayList = (ArrayList)this.m_m[l];
		if (arrayList.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < arrayList.Count; i++)
		{
			ListChangedEventArgs e = (ListChangedEventArgs)arrayList[i];
			if (e.ListChangedType == ListChangedType.ItemAdded && e.OldIndex == -1)
			{
				arrayList[i] = new ListChangedEventArgs(ListChangedType.ItemMoved, e.NewIndex, A_0);
				break;
			}
		}
	}

	internal void c(int A_0)
	{
		if (l < 0 || l >= this.m_m.Count)
		{
			return;
		}
		ArrayList arrayList = (ArrayList)this.m_m[l];
		if (arrayList.Count <= 0)
		{
			return;
		}
		for (int num = arrayList.Count - 1; num >= 0; num--)
		{
			ListChangedEventArgs e = (ListChangedEventArgs)arrayList[num];
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				arrayList[num] = new ListChangedEventArgs(ListChangedType.ItemDeleted, A_0, -1);
			}
		}
	}

	internal void a(int A_0, int A_1)
	{
		if (l < 0 || l >= this.m_m.Count)
		{
			return;
		}
		ArrayList arrayList = (ArrayList)this.m_m[l];
		if (arrayList.Count <= 0)
		{
			return;
		}
		bool flag = false;
		int num = -1;
		int newIndex = ((A_0 > A_1) ? (A_0 - 1) : A_0);
		for (int num2 = arrayList.Count - 1; num2 >= 0; num2--)
		{
			ListChangedEventArgs e = (ListChangedEventArgs)arrayList[num2];
			if (e.NewIndex == A_0)
			{
				if (e.ListChangedType == ListChangedType.ItemChanged)
				{
					flag = true;
				}
				if (e.ListChangedType == ListChangedType.ItemMoved)
				{
					if (A_0 > A_1 && e.OldIndex < A_1)
					{
						A_1++;
					}
					else if (A_0 < A_1 && e.OldIndex >= A_1)
					{
						A_1--;
					}
					A_0 = e.OldIndex;
					num = ((A_0 < A_1) ? (A_1 - 1) : A_1);
				}
			}
		}
		arrayList.Clear();
		if (num != -1)
		{
			arrayList.Add(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, num));
			if (flag)
			{
				arrayList.Add(new ListChangedEventArgs(ListChangedType.ItemChanged, newIndex, -1));
			}
		}
		else if (flag)
		{
			int newIndex2 = ((A_0 < A_1) ? (A_1 - 1) : A_1);
			arrayList.Add(new ListChangedEventArgs(ListChangedType.ItemChanged, newIndex2, -1));
		}
	}

	internal void b(object A_0, ListChangedEventArgs A_1)
	{
		if (A_1.ListChangedType == ListChangedType.ItemMoved && A_1.OldIndex == e)
		{
			e = A_1.NewIndex;
			CurrentRowView.IndexInternal = A_1.NewIndex;
			if (CurrentRowView.InnerRowView != null)
			{
				CurrentRowView.Row = CurrentRowView.InnerRowView.Row;
			}
		}
		if (this.f != null && this.f.storeEvents > 0 && l >= 0)
		{
			while (l >= this.m_m.Count)
			{
				this.m_m.Add(new ArrayList());
			}
			ArrayList arrayList = (ArrayList)this.m_m[l];
			arrayList.Add(A_1);
		}
		a(this, A_1);
	}

	internal void a(object A_0, ListChangedEventArgs A_1)
	{
		if (A_1.ListChangedType == ListChangedType.Reset)
		{
			this.m_i = null;
		}
		if (this.f != null && this.f.disableEvents > 0)
		{
			return;
		}
		if (A_1.ListChangedType == ListChangedType.ItemAdded && this.f != null && g != null && A_1.NewIndex < g.Count)
		{
			DataRowView dataRowView = g[A_1.NewIndex];
			if (dataRowView.Row.RowState == DataRowState.Detached || dataRowView.Row.RowState == DataRowState.Added)
			{
				this.f.d(dataRowView.Row);
			}
		}
		if (DbDataTable.DisableListChangedEvents)
		{
			return;
		}
		ListChangedEventHandler listChangedEventHandler = (ListChangedEventHandler)Events[o];
		if (listChangedEventHandler == null)
		{
			return;
		}
		if (this.f != null && (A_1.ListChangedType == ListChangedType.PropertyDescriptorAdded || A_1.ListChangedType == ListChangedType.PropertyDescriptorChanged || A_1.ListChangedType == ListChangedType.PropertyDescriptorDeleted))
		{
			this.f.ac = true;
		}
		u = A_1;
		try
		{
			listChangedEventHandler(A_0, A_1);
		}
		catch (Exception ex)
		{
			u = null;
			if (ex.StackTrace != null && ex.StackTrace.IndexOf("at Devart.Common.DbDataTable.") > 0)
			{
				throw;
			}
		}
	}

	PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
	{
		if (this.f == null)
		{
			if (Utils.DesignMode)
			{
				return new ac(null);
			}
			return new PropertyDescriptorCollection(null);
		}
		return this.f.c(listAccessors);
	}

	string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
	{
		if (this.f == null)
		{
			return "";
		}
		return this.f.TableName;
	}

	internal void m()
	{
		if (this.m_c != null)
		{
			((IEditableObject)this.m_c).CancelEdit();
			AddNewRow = null;
		}
	}

	internal void b(DataRow A_0)
	{
		if (this.f != null)
		{
			this.f.e(A_0);
		}
	}

	void ICancelAddNew.CancelNew(int itemIndex)
	{
		if (this.m_c != null && this.m_c.Index == itemIndex)
		{
			m();
		}
	}

	void ICancelAddNew.EndNew(int itemIndex)
	{
		if (this.m_c != null && this.m_c.Index == itemIndex)
		{
			((IEditableObject)this.m_c).EndEdit();
		}
	}

	private int a(DbDataRowView A_0)
	{
		if (A_0.Row != A_0.InnerRowView.Row)
		{
			for (int i = 0; i < g.Count; i++)
			{
				DataRowView dataRowView = g[i];
				if (dataRowView.Row == A_0.Row)
				{
					A_0.InnerRowView = dataRowView;
					return i;
				}
			}
		}
		return -1;
	}

	internal bool k()
	{
		if (this.f != null && g != null && this.m_c != null && this.m_c.e)
		{
			DataRow A_ = this.m_c.Row;
			bool flag = g.Count == 1;
			object[] array = DbDataRowView.a(A_, A_1: true);
			int index = this.m_c.Index;
			try
			{
				a(this.m_c.Index);
				this.m_i[this.m_c.Index] = this.m_c;
				this.f.a(ref A_, A_1: false);
				a(this.m_c);
				a(this.m_c.Row, array);
			}
			catch
			{
				if (!this.f.CachedUpdates && g.Count == 0 && flag)
				{
					this.f.disableEvents++;
					this.f.disableUpdateEvents++;
					this.f.storeEvents++;
					try
					{
						DataRowView dataRowView = g.AddNew();
						DataRow row = dataRowView.Row;
						for (int i = 0; i < this.f.Columns.Count; i++)
						{
							row[i] = array[i];
						}
						A_ = row;
						this.m_c.Row = row;
						this.m_c.InnerRowView = dataRowView;
					}
					finally
					{
						this.f.disableEvents--;
						this.f.disableUpdateEvents--;
						this.f.storeEvents--;
					}
					b(index);
					f();
				}
				throw;
			}
			finally
			{
				if (this.m_c.Row != A_)
				{
					this.m_c.Row = A_;
				}
			}
			this.f.disableEvents++;
			this.f.storeEvents++;
			try
			{
				this.m_c.InnerRowView.CancelEdit();
			}
			finally
			{
				this.f.disableEvents--;
				this.f.storeEvents--;
			}
			_ = this.m_c.Index;
			_ = g.Count;
			this.m_c.e = false;
			AddNewRow = null;
			return true;
		}
		AddNewRow = null;
		return false;
	}

	internal void c(DbDataRowView A_0)
	{
		if (this.f != null && g != null)
		{
			DataRow row = A_0.Row;
			this.f.b(row);
		}
	}

	private static bool a(DataRow A_0, object[] A_1)
	{
		bool result = false;
		DataColumnCollection columns = A_0.Table.Columns;
		int count = columns.Count;
		for (int i = 0; i < count; i++)
		{
			_ = columns[i];
			object obj = A_1[i];
			object obj2 = A_0[i];
			if (obj != obj2 && (obj == null || obj2 == null || !obj.Equals(obj2)))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	internal void d(DbDataRowView A_0)
	{
		if (A_0 == null)
		{
			this.m_d = null;
			e = -1;
		}
		else
		{
			this.m_d = A_0;
			e = A_0.Index;
		}
	}

	private DbDataRowView a(int A_0)
	{
		if (A_0 >= RowViewCache.Count)
		{
			do
			{
				this.m_i.Add(null);
			}
			while (A_0 >= this.m_i.Count);
			return null;
		}
		return (DbDataRowView)this.m_i[A_0];
	}

	private MethodInfo a()
	{
		if ((object)v != null)
		{
			return v;
		}
		MethodInfo[] methods = g.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
		foreach (MethodInfo methodInfo in methods)
		{
			if (methodInfo.Name == "UpdateIndex")
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters != null && parameters.Length == 1 && (object)parameters[0].ParameterType == typeof(bool))
				{
					v = methodInfo;
					return methodInfo;
				}
			}
		}
		return null;
	}

	internal bool a(DataRow A_0)
	{
		if (this.m_j != null)
		{
			return this.m_j.Contains(A_0);
		}
		return false;
	}

	internal void a(DataRow A_0, bool A_1)
	{
		if (a(A_0) != A_1)
		{
			if (this.m_j == null)
			{
				this.m_j = new ArrayList();
			}
			if (A_1)
			{
				this.m_j.Add(A_0);
			}
			else
			{
				this.m_j.Remove(A_0);
			}
		}
	}

	internal void j()
	{
		a()?.Invoke(g, new object[1] { true });
	}

	public void RefreshRow(DbDataRowView rowView)
	{
		if (this.f != null)
		{
			object[] a_ = DbDataRowView.a(rowView.Row, A_1: true);
			this.f.RefreshRow(rowView.Row);
			if (a(rowView.Row, a_))
			{
				a(this, new ListChangedEventArgs(ListChangedType.ItemChanged, rowView.Index, rowView.Index));
			}
		}
	}

	internal CurrencyManager a(string A_0)
	{
		return BindingSource.GetRelatedCurrencyManager(A_0);
	}

	void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
	{
		if (g != null)
		{
			((IBindingListView)g).ApplySort(sorts);
		}
	}

	void IBindingListView.RemoveFilter()
	{
		if (g != null)
		{
			((IBindingListView)g).RemoveFilter();
		}
	}

	internal void a(object A_0, PropertyChangedEventArgs A_1)
	{
		((PropertyChangedEventHandler)Events[t])?.Invoke(A_0, A_1);
	}
}
