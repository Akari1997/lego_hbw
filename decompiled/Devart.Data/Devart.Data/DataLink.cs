using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;
using Devart.Common;

namespace Devart.Data;

[DesignTimeVisible(true)]
[DefaultEvent("CurrentChanged")]
[ToolboxItem(typeof(DataLinkToolboxItem))]
[DesignerSerializer("Devart.Common.Design.DataLinkSerializer, Devart.Data.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design")]
[r("DataLink_Description")]
[Designer("Devart.Common.Design.DataLinkDesigner, Devart.Data.Design")]
public class DataLink : BindingSource, IListSource
{
	private object m_a;

	private object m_b;

	private DbDataTable m_c;

	private DataLink m_d;

	private string m_e;

	private object m_f;

	private ae m_g;

	private DbDataTableView h;

	private CurrencyManager i;

	private bool j;

	private CurrencyManager k;

	private EventHandler l;

	private EventHandler m;

	private EventHandler n;

	private EventHandler o;

	private ListChangedEventHandler p;

	private EventHandler q;

	private bool r;

	private bool s;

	private bool t;

	private bool u;

	private static readonly object v = new object();

	private static readonly object w = new object();

	private static readonly object x = new object();

	private static readonly object y = new object();

	private bool z;

	private bool aa;

	private DbDataTableView ab;

	private string ac;

	[Browsable(false)]
	public IList List => DataTableView.h();

	[MergableProperty(false)]
	[Editor("Devart.Common.Design.DataLinkDataMemberEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
	[r("DataLink_DataMember")]
	[DefaultValue("")]
	public string DataMember
	{
		get
		{
			if (this.m_e == null)
			{
				return string.Empty;
			}
			return this.m_e;
		}
		set
		{
			if (this.m_e != value)
			{
				this.m_e = value;
				d();
				((BindingSource)this).OnDataMemberChanged(EventArgs.Empty);
			}
		}
	}

	[r("DataLink_Synchronized")]
	[DefaultValue(false)]
	public bool Synchronized
	{
		get
		{
			return t;
		}
		set
		{
			if (t != value)
			{
				t = value;
				d();
			}
		}
	}

	[r("DataLink_DataSource")]
	[Editor("Devart.Common.Design.DataLinkLinkEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[MergableProperty(false)]
	[TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design")]
	public object DataSource
	{
		get
		{
			return this.m_a;
		}
		set
		{
			if (this.m_a == value)
			{
				return;
			}
			if (this.m_c != null)
			{
				this.m_c.ListChanged -= f;
			}
			if (this.m_d != null)
			{
				this.m_d.LinkChanged -= a;
			}
			this.m_a = value;
			this.m_b = null;
			this.m_c = this.m_a as DbDataTable;
			DbDataSet dbDataSet = null;
			h = null;
			if (this.m_c != null && DataMember == "")
			{
				h = ((IListSource)this.m_c).GetList() as DbDataTableView;
				this.m_b = this.m_c.Owner;
				this.m_c.ListChanged += f;
			}
			else
			{
				dbDataSet = this.m_a as DbDataSet;
				if (dbDataSet != null)
				{
					this.m_b = dbDataSet.Owner;
				}
				else
				{
					this.m_d = this.m_a as DataLink;
					if (this.m_d != null)
					{
						this.m_b = this.m_d.Owner;
						this.m_d.LinkChanged += a;
					}
				}
			}
			if (value != null && dbDataSet == null && this.m_c == null && this.m_d == null && this.m_b == null)
			{
				this.m_a = null;
				throw new ArgumentException(string.Format("Only values of {0}, {1}, {2} types are allowed in DataSource property", typeof(DbDataSet).Name, typeof(DbDataTable).Name));
			}
			((BindingSource)this).DataSource = null;
			((BindingSource)this).DataSource = DataTableView;
			d();
			if (q != null)
			{
				q(this, new EventArgs());
			}
		}
	}

	bool IListSource.ContainsListCollection => false;

	private DbDataTableView NullDataTableView
	{
		get
		{
			if (ab == null)
			{
				ab = new DbDataTableView(null, null);
			}
			return ab;
		}
	}

	private ae DataTableView
	{
		get
		{
			if (this.m_g == null)
			{
				if (h != null)
				{
					this.m_g = new ae(h);
				}
				else
				{
					this.m_g = new ae(NullDataTableView);
				}
			}
			return this.m_g;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public int Position
	{
		get
		{
			return ((BindingSource)this).Position;
		}
		set
		{
			((BindingSource)this).Position = value;
		}
	}

	[DefaultValue(false)]
	[r("DataLink_DataMember")]
	public bool SeparateEditing
	{
		get
		{
			return u;
		}
		set
		{
			if (u != value)
			{
				u = value;
				DataTableView.a(u);
				if (!u)
				{
					d();
				}
			}
		}
	}

	[Browsable(false)]
	[DefaultValue("")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public string Name
	{
		get
		{
			if (((Component)(object)this).Site == null)
			{
				if (ac != null)
				{
					return ac;
				}
				return string.Empty;
			}
			return ((Component)(object)this).Site.Name;
		}
		set
		{
			if (((Component)(object)this).Site == null)
			{
				ac = ((value == null) ? string.Empty : value);
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public object Owner
	{
		get
		{
			return this.m_f;
		}
		set
		{
			bool flag = this.m_f != value;
			this.m_f = value;
			if (flag)
			{
				f(this, null);
			}
		}
	}

	public DbDataRowView CurrentRow => ((BindingSource)this).Current as DbDataRowView;

	internal event EventHandler LinkChanged
	{
		add
		{
			q = (EventHandler)Delegate.Combine(q, value);
		}
		remove
		{
			q = (EventHandler)Delegate.Remove(q, value);
		}
	}

	public DataLink()
	{
		l = e;
		m = d;
		n = c;
		o = b;
		p = a;
		z = false;
	}

	~DataLink()
	{
		try
		{
			((Component)(object)this).Dispose(false);
		}
		finally
		{
			((Component)this).Finalize();
		}
	}

	protected override void Dispose(bool disposing)
	{
		e();
		if (this.m_c != null)
		{
			this.m_c.ListChanged -= f;
		}
		((BindingSource)this).Dispose(disposing);
	}

	private bool g()
	{
		if (!z)
		{
			return DataSource != null;
		}
		return true;
	}

	private bool f()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null || ((Control)Owner).IsDisposed)
		{
			e();
			return false;
		}
		if (this.m_a == null || (!j && (this.m_b == null || ((Control)this.m_b).IsDisposed)))
		{
			e();
			return false;
		}
		return true;
	}

	private void e(object A_0, EventArgs A_1)
	{
		if (!t || r || !f())
		{
			return;
		}
		r = true;
		try
		{
			if (k != null && i != null && ((BindingManagerBase)i).Position != ((BindingManagerBase)k).Position && ((BindingManagerBase)i).Position < ((BindingManagerBase)k).Count && !a(h))
			{
				((BindingManagerBase)k).Position = ((BindingManagerBase)i).Position;
			}
		}
		finally
		{
			r = false;
		}
	}

	private bool a(DbDataTableView A_0)
	{
		if (A_0 != null && A_0.u != null)
		{
			return A_0.u.ListChangedType == ListChangedType.ItemMoved;
		}
		return false;
	}

	private void d(object A_0, EventArgs A_1)
	{
		if (r || !f())
		{
			return;
		}
		r = true;
		try
		{
			d();
		}
		finally
		{
			r = false;
		}
	}

	private void c(object A_0, EventArgs A_1)
	{
		if (!t || r || !f())
		{
			return;
		}
		r = true;
		try
		{
			if (k != null && i != null && ((BindingManagerBase)i).Position != ((BindingManagerBase)k).Position && ((BindingManagerBase)k).Position < ((BindingManagerBase)i).Count && !a(DataTableView.h()))
			{
				((BindingManagerBase)i).Position = ((BindingManagerBase)k).Position;
			}
		}
		finally
		{
			r = false;
		}
	}

	private void b(object A_0, EventArgs A_1)
	{
		if (r || !f())
		{
			return;
		}
		r = true;
		try
		{
		}
		finally
		{
			r = false;
		}
	}

	private void e()
	{
		if (i != null)
		{
			((BindingManagerBase)i).PositionChanged -= l;
			((BindingManagerBase)i).CurrentChanged -= l;
			i.ListChanged -= p;
			i.MetaDataChanged -= m;
		}
		if (k != null)
		{
			((BindingManagerBase)k).PositionChanged -= n;
			k.MetaDataChanged -= o;
		}
	}

	private void a(object A_0, ListChangedEventArgs A_1)
	{
		if (s)
		{
			return;
		}
		s = true;
		aa = true;
		try
		{
			if (A_1.ListChangedType == ListChangedType.Reset)
			{
				d();
			}
		}
		finally
		{
			s = false;
			aa = false;
		}
	}

	private void d()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		c();
		e();
		i = null;
		if (this.m_a != null)
		{
			if (this.m_b is Control)
			{
				BindingContext bindingContext = ((Control)this.m_b).BindingContext;
				if (h != null)
				{
					i = (CurrencyManager)bindingContext[this.m_a, DataMember];
					j = false;
				}
			}
			if (i == null && h != null)
			{
				i = h.CurrencyManager;
				j = true;
			}
			if (i != null)
			{
				((BindingManagerBase)i).CurrentChanged += l;
				i.ListChanged += p;
				i.MetaDataChanged += m;
			}
		}
		k = null;
		if (this.m_a != null)
		{
			k = ((ICurrencyManagerProvider)this).CurrencyManager;
			if (k != null)
			{
				k.MetaDataChanged += o;
			}
		}
		bool flag = r;
		bool flag2 = s;
		r = true;
		s = true;
		try
		{
			if (h != null)
			{
				DataTableView.c(h);
			}
			else
			{
				DataTableView.c(NullDataTableView);
			}
		}
		finally
		{
			r = flag;
			s = flag2;
		}
		if (k != null && i != null)
		{
			((BindingManagerBase)i).PositionChanged += l;
			((BindingManagerBase)k).PositionChanged += n;
			if (t && ((BindingManagerBase)k).Position != ((BindingManagerBase)i).Position && ((BindingManagerBase)i).Position < ((BindingManagerBase)k).Count)
			{
				((BindingManagerBase)k).Position = ((BindingManagerBase)i).Position;
			}
		}
	}

	public override object AddNew()
	{
		if (h != null)
		{
			DbDataTable dataTable = h.DataTable;
			if (dataTable != null && !dataTable.FetchComplete)
			{
				dataTable.b(A_0: true);
			}
		}
		return ((BindingSource)this).AddNew();
	}

	protected override void OnListChanged(ListChangedEventArgs e)
	{
		if (aa)
		{
			return;
		}
		aa = true;
		try
		{
			if (e.ListChangedType == ListChangedType.Reset)
			{
				f(this, e);
			}
			((BindingSource)this).OnListChanged(e);
		}
		finally
		{
			aa = false;
		}
	}

	internal void f(object A_0, EventArgs A_1)
	{
		d();
	}

	private void a(object A_0, EventArgs A_1)
	{
		d();
	}

	private void c()
	{
		if (this.m_c != null)
		{
			this.m_c.ListChanged -= f;
		}
		this.m_c = null;
		this.m_b = null;
		h = null;
		ITypedList typedList = DbDataTable.a(this.m_a, this.m_e, out this.m_b);
		if (typedList is ae ae2)
		{
			typedList = ae2.h();
		}
		h = typedList as DbDataTableView;
		if (h != null)
		{
			this.m_c = h.f;
			if (this.m_c != null)
			{
				this.m_c.ListChanged += f;
			}
		}
	}

	IList IListSource.GetList()
	{
		return DataTableView;
	}

	public int Find(string propertyName, object key)
	{
		if (this.m_g == null)
		{
			return ((BindingSource)this).Find(propertyName, key);
		}
		PropertyDescriptor propertyDescriptor = ((ITypedList)this.m_g).GetItemProperties((PropertyDescriptor[]?)null)?.Find(propertyName, ignoreCase: true);
		if (propertyDescriptor == null)
		{
			throw new ArgumentException($"DataMember property '{propertyName}' cannot be found on the DataSource.");
		}
		return ((BindingSource)this).Find(propertyDescriptor, key);
	}
}
