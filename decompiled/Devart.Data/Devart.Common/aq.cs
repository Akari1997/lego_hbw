using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Devart.Data;

namespace Devart.Common;

internal class aq : IDisposable
{
	private Control m_a;

	private string[] m_b;

	private IListSource m_c;

	private object m_d;

	private object m_e;

	private CurrencyManager m_f;

	private ab m_g;

	private ListChangedEventHandler m_h;

	private EventHandler m_i;

	private EventHandler m_j;

	public aq(DbDataTable A_0, Control A_1, ab A_2)
	{
		this.m_i = b;
		this.m_j = b;
		this.m_h = a;
		this.m_g = A_2;
		this.m_a = A_1;
		b(A_0);
	}

	private void g()
	{
		a(A_0: true);
		GC.SuppressFinalize(this);
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in g
		this.g();
	}

	private void a(bool A_0)
	{
		this.m_g = null;
		d();
		if (this.m_c != null)
		{
			a(this.m_h);
		}
	}

	protected virtual void i()
	{
		try
		{
			a(A_0: false);
		}
		finally
		{
			base.Finalize();
		}
	}

	private bool e()
	{
		if (this.m_a == null || this.m_a.IsDisposed)
		{
			d();
			return false;
		}
		if (!b())
		{
			d();
			return false;
		}
		return true;
	}

	private void b(object A_0, EventArgs A_1)
	{
	}

	private void d()
	{
		if (this.m_f != null)
		{
			((BindingManagerBase)this.m_f).CurrentChanged -= a;
			((BindingManagerBase)this.m_f).PositionChanged -= this.m_i;
			this.m_f.MetaDataChanged -= this.m_j;
		}
	}

	private object c()
	{
		object obj = null;
		if (this.m_f != null && ((BindingManagerBase)this.m_f).Position >= 0 && this.m_b != null && this.m_b.Length != 0 && ((BindingManagerBase)this.m_f).Current is DbDataRowView { Row: var row })
		{
			if (this.m_b.Length > 1)
			{
				obj = new object[this.m_b.Length];
			}
			for (int i = 0; i < this.m_b.Length; i++)
			{
				if (this.m_b.Length > 1)
				{
					((object[])obj)[i] = row[this.m_b[i]];
				}
				else
				{
					obj = row[this.m_b[i]];
				}
			}
		}
		return obj;
	}

	private bool a(object A_0)
	{
		if (A_0 == null && this.m_e == null)
		{
			return true;
		}
		if (A_0 == null || this.m_e == null)
		{
			return false;
		}
		if (A_0 == DBNull.Value && this.m_e == DBNull.Value)
		{
			return true;
		}
		object[] array = A_0 as object[];
		object[] array2 = this.m_e as object[];
		if (array != null && array2 != null)
		{
			if (array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}
		return A_0.Equals(this.m_e);
	}

	public void f()
	{
		if (this.m_c != null && a(this.m_c) != this.m_d)
		{
			b(this.m_c);
		}
		if (this.m_a != null && b())
		{
			d();
			this.m_f = a();
			if (this.m_f != null)
			{
				((BindingManagerBase)this.m_f).CurrentChanged += a;
				((BindingManagerBase)this.m_f).PositionChanged += this.m_i;
				this.m_f.MetaDataChanged += this.m_j;
			}
			object obj = null;
			if (this.m_g != null)
			{
				obj = c();
				if (!a(obj))
				{
					this.m_e = obj;
					this.m_g(this.m_c, ((BindingManagerBase)this.m_f).Position);
				}
			}
		}
		else
		{
			this.m_e = null;
			d();
		}
	}

	private void a(object A_0, EventArgs A_1)
	{
		e();
		f();
	}

	private void a(object A_0, ListChangedEventArgs A_1)
	{
		bool flag = false;
		switch (A_1.ListChangedType)
		{
		case ListChangedType.Reset:
		case ListChangedType.ItemDeleted:
			flag = true;
			break;
		case ListChangedType.ItemAdded:
		case ListChangedType.ItemChanged:
			if (A_1.NewIndex == ((BindingManagerBase)this.m_f).Position)
			{
				flag = true;
			}
			break;
		case ListChangedType.PropertyDescriptorChanged:
		{
			if (A_1.PropertyDescriptor == null)
			{
				break;
			}
			for (int i = 0; i < this.m_b.Length; i++)
			{
				if (string.Compare(A_1.PropertyDescriptor.Name, this.m_b[i], ignoreCase: true) == 0)
				{
					flag = true;
					break;
				}
			}
			break;
		}
		}
		if (flag)
		{
			f();
		}
	}

	[SpecialName]
	internal object h()
	{
		return this.m_d;
	}

	[SpecialName]
	public string[] k()
	{
		return this.m_b;
	}

	[SpecialName]
	public void a(string[] A_0)
	{
		this.m_b = A_0;
		c();
	}

	private static object a(IListSource A_0)
	{
		if (A_0 is DbDataTable dbDataTable)
		{
			return dbDataTable.Owner;
		}
		return null;
	}

	private bool b()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		IListSource listSource = this.m_c;
		ICurrencyManagerProvider val = (ICurrencyManagerProvider)((listSource is ICurrencyManagerProvider) ? listSource : null);
		if (val != null)
		{
			return true;
		}
		if (this.m_c == null || this.m_d == null || ((Control)this.m_d).IsDisposed)
		{
			return false;
		}
		return true;
	}

	private CurrencyManager a()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		IListSource listSource = this.m_c;
		ICurrencyManagerProvider val = (ICurrencyManagerProvider)((listSource is ICurrencyManagerProvider) ? listSource : null);
		if (val != null)
		{
			return val.CurrencyManager;
		}
		if (this.m_c != null && this.m_d is Control)
		{
			BindingContext bindingContext = ((Control)this.m_d).BindingContext;
			return (CurrencyManager)bindingContext[(object)this.m_c];
		}
		return null;
	}

	[SpecialName]
	public void b(IListSource A_0)
	{
		if (this.m_c != A_0 || (A_0 != null && a(A_0) != this.m_d))
		{
			a(this.m_h);
			this.m_c = A_0;
			this.m_d = null;
			if (A_0 != null)
			{
				this.m_d = a(A_0);
			}
			b(this.m_h);
			f();
		}
	}

	[SpecialName]
	public void b(object A_0)
	{
		if (this.m_a != ((A_0 is Control) ? A_0 : null))
		{
			this.m_a = (Control)((A_0 is Control) ? A_0 : null);
			f();
		}
	}

	[SpecialName]
	public int j()
	{
		if (this.m_f != null)
		{
			return ((BindingManagerBase)this.m_f).Position;
		}
		return -1;
	}

	[SpecialName]
	private void b(ListChangedEventHandler A_0)
	{
		if (this.m_c != null)
		{
			if (this.m_c is DbDataTable dbDataTable)
			{
				((IBindingList)((IListSource)dbDataTable).GetList()).ListChanged += A_0;
			}
			else if (this.m_c is DataLink dataLink)
			{
				((BindingSource)dataLink).ListChanged += A_0;
			}
		}
	}

	[SpecialName]
	private void a(ListChangedEventHandler A_0)
	{
		if (this.m_c != null)
		{
			if (this.m_c is DbDataTable dbDataTable)
			{
				((IBindingList)((IListSource)dbDataTable).GetList()).ListChanged -= A_0;
			}
			else if (this.m_c is DataLink dataLink)
			{
				((BindingSource)dataLink).ListChanged -= A_0;
			}
		}
	}
}
