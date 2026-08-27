using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace Devart.Common;

public class DbDataRowView : ICustomTypeDescriptor, IEditableObject, IListSource, IDataErrorInfo
{
	private DataRowView m_a;

	private DataRow b;

	internal IList c;

	private int d;

	internal bool e;

	private bool f;

	private PropertyChangedEventHandler g;

	private bool h;

	private Hashtable i;

	private bool NeededUpdate
	{
		get
		{
			if (!f && (b.RowState == DataRowState.Unchanged || b.RowState == DataRowState.Detached))
			{
				return b.HasVersion(DataRowVersion.Proposed);
			}
			return true;
		}
	}

	bool IListSource.ContainsListCollection => false;

	internal DataRowView InnerRowView
	{
		get
		{
			return this.m_a;
		}
		set
		{
			this.m_a = value;
		}
	}

	public DataRow Row
	{
		get
		{
			return b;
		}
		set
		{
			b = value;
		}
	}

	internal bool InEditMode
	{
		get
		{
			return f;
		}
		set
		{
			f = value;
		}
	}

	public int Index => d;

	internal int IndexInternal
	{
		set
		{
			d = value;
		}
	}

	internal Hashtable RelationViews
	{
		get
		{
			if (i == null)
			{
				i = new Hashtable();
			}
			return i;
		}
	}

	public object this[string columnName]
	{
		get
		{
			if (Row == null)
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = ((ICustomTypeDescriptor)this).GetProperties()[columnName];
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			return propertyDescriptor.GetValue(this);
		}
		set
		{
			PropertyDescriptor propertyDescriptor = ((ICustomTypeDescriptor)this).GetProperties()[columnName];
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			propertyDescriptor.SetValue(this, value);
		}
	}

	public object this[int index]
	{
		get
		{
			if (Row == null)
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = ((ICustomTypeDescriptor)this).GetProperties()[index];
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			return propertyDescriptor.GetValue(this);
		}
		set
		{
			PropertyDescriptor propertyDescriptor = ((ICustomTypeDescriptor)this).GetProperties()[index];
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			propertyDescriptor.SetValue(this, value);
		}
	}

	public bool IsNew => e;

	public bool IsEdit
	{
		get
		{
			if (Row != null && !Row.HasVersion(DataRowVersion.Proposed))
			{
				return ((DbDataTableView)c).a(Row);
			}
			return true;
		}
	}

	public string Error
	{
		get
		{
			if (this.m_a == null)
			{
				return string.Empty;
			}
			return ((IDataErrorInfo)this.m_a).Error;
		}
	}

	string IDataErrorInfo.this[string columnName]
	{
		get
		{
			if (InnerRowView != null && InnerRowView.DataView.Table.Columns.Contains(columnName))
			{
				return ((IDataErrorInfo)InnerRowView)[columnName];
			}
			return string.Empty;
		}
	}

	public DbDataTableView DataView => (DbDataTableView)c;

	public event PropertyChangedEventHandler PropertyChanged
	{
		add
		{
			g = (PropertyChangedEventHandler)Delegate.Combine(g, value);
		}
		remove
		{
			g = (PropertyChangedEventHandler)Delegate.Remove(g, value);
		}
	}

	internal DbDataRowView(DataRow A_0, IList A_1, DataRowView A_2, int A_3)
	{
		b = A_0;
		c = A_1;
		d = A_3;
		this.m_a = A_2;
	}

	private void a(object A_0, PropertyChangedEventArgs A_1)
	{
		if (g != null)
		{
			g(A_0, A_1);
		}
		if (c is DbDataTableView dbDataTableView)
		{
			dbDataTableView.a(this, A_1);
		}
	}

	AttributeCollection ICustomTypeDescriptor.GetAttributes()
	{
		if (this.m_a != null)
		{
			return ((ICustomTypeDescriptor)this.m_a).GetAttributes();
		}
		return TypeDescriptor.GetAttributes(Row, noCustomTypeDesc: true);
	}

	string ICustomTypeDescriptor.GetClassName()
	{
		if (this.m_a != null)
		{
			return ((ICustomTypeDescriptor)this.m_a).GetClassName();
		}
		return TypeDescriptor.GetClassName(Row, noCustomTypeDesc: true);
	}

	string ICustomTypeDescriptor.GetComponentName()
	{
		if (this.m_a != null)
		{
			return ((ICustomTypeDescriptor)this.m_a).GetComponentName();
		}
		return TypeDescriptor.GetComponentName(Row, noCustomTypeDesc: true);
	}

	TypeConverter ICustomTypeDescriptor.GetConverter()
	{
		if (this.m_a != null)
		{
			return ((ICustomTypeDescriptor)this.m_a).GetConverter();
		}
		return TypeDescriptor.GetConverter(Row, noCustomTypeDesc: true);
	}

	EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
	{
		if (this.m_a != null)
		{
			return ((ICustomTypeDescriptor)this.m_a).GetDefaultEvent();
		}
		return TypeDescriptor.GetDefaultEvent(Row, noCustomTypeDesc: true);
	}

	PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
	{
		if (this.m_a != null)
		{
			return ((ICustomTypeDescriptor)this.m_a).GetDefaultProperty();
		}
		return TypeDescriptor.GetDefaultProperty(Row, noCustomTypeDesc: true);
	}

	object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
	{
		if (this.m_a != null)
		{
			return ((ICustomTypeDescriptor)this.m_a).GetEditor(editorBaseType);
		}
		return TypeDescriptor.GetEditor(Row, editorBaseType, noCustomTypeDesc: true);
	}

	EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
	{
		return TypeDescriptor.GetEvents(Row, noCustomTypeDesc: true);
	}

	EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
	{
		return TypeDescriptor.GetEvents(Row, attributes, noCustomTypeDesc: true);
	}

	object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
	{
		return this;
	}

	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
	{
		return ((DbDataTable)b.Table).c((PropertyDescriptor[])null);
	}

	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
	{
		return ((DbDataTable)b.Table).c((PropertyDescriptor[])null);
	}

	internal object a(DataColumn A_0, g A_1)
	{
		if (A_1.c() != null)
		{
			return CreateChildView(A_1.c());
		}
		if (Row == null)
		{
			return null;
		}
		object a_ = ((A_0.Table != null) ? Row[A_0] : null);
		a_ = ((DbDataTable)Row.Table).b(a_, A_0.DataType, this, A_1);
		if (a_ != null)
		{
			return a_;
		}
		return DBNull.Value;
	}

	internal void a(DataColumn A_0, object A_1, PropertyDescriptor A_2)
	{
		if (Row == null || h)
		{
			return;
		}
		DbDataTable dbDataTable = (DbDataTable)Row.Table;
		A_1 = dbDataTable.a(A_1, A_0.DataType, this, A_2);
		string columnName = A_0.ColumnName;
		object a_ = Row[columnName];
		a_ = dbDataTable.a(a_, A_0.DataType, this, A_2);
		if (a_ == null && A_1 == null)
		{
			return;
		}
		Type type = ((a_ != null) ? a_.GetType() : A_1.GetType());
		if ((type.IsValueType && (a_ == null || !a_.Equals(A_1))) || (!type.IsValueType && a_ != A_1))
		{
			InEditMode = true;
			DbDataTableView dbDataTableView = (DbDataTableView)c;
			if (dbDataTableView.a(Row))
			{
				dbDataTableView.a(Row, A_1: false);
				Row.BeginEdit();
			}
			Row[columnName] = A_1;
			a(this, new PropertyChangedEventArgs(A_2.Name));
		}
	}

	public void BeginEdit()
	{
		if (c is DbDataTableView dbDataTableView)
		{
			dbDataTableView.d(this);
		}
		if (Row != null)
		{
			((DbDataTableView)c).a(Row, A_1: true);
		}
	}

	public void CancelEdit()
	{
		e = false;
		if (Row == null)
		{
			return;
		}
		DbDataTableView dbDataTableView = c as DbDataTableView;
		dbDataTableView?.a(Row, A_1: false);
		if (dbDataTableView != null && dbDataTableView.AddNewRow == this)
		{
			_ = Row;
			dbDataTableView.AddNewRow = null;
			dbDataTableView.f.disableEvents++;
			dbDataTableView.f.disableUpdateEvents++;
			dbDataTableView.f.storeEvents++;
			try
			{
				if (this.m_a.Row.RowState == DataRowState.Detached)
				{
					dbDataTableView.b(this.m_a.Row);
				}
				this.m_a.CancelEdit();
				if (this.m_a.Row.RowState == DataRowState.Added)
				{
					this.m_a.Row.Delete();
				}
			}
			finally
			{
				dbDataTableView.f.disableEvents--;
				dbDataTableView.f.disableUpdateEvents--;
				dbDataTableView.f.storeEvents--;
			}
			f = false;
			dbDataTableView.f();
		}
		else
		{
			InnerRowView.CancelEdit();
			f = false;
			dbDataTableView.a(c, new ListChangedEventArgs(ListChangedType.ItemChanged, Index));
		}
	}

	public void EndEdit()
	{
		if (h)
		{
			return;
		}
		h = true;
		try
		{
			if (b != null && c != null)
			{
				if (c is DbDataTableView dbDataTableView)
				{
					dbDataTableView.a(Row, A_1: false);
					if (dbDataTableView.AddNewRow == this)
					{
						try
						{
							int index = Index;
							dbDataTableView.k();
							dbDataTableView.b(index);
						}
						finally
						{
							dbDataTableView.f();
						}
					}
					else if (NeededUpdate)
					{
						try
						{
							dbDataTableView.c(this);
							dbDataTableView.f();
						}
						catch
						{
							Row.CancelEdit();
							throw;
						}
					}
				}
				Row.EndEdit();
			}
			f = false;
		}
		finally
		{
			h = false;
		}
	}

	IList IListSource.GetList()
	{
		return null;
	}

	public DbDataTableView CreateChildView(string relationName)
	{
		return CreateChildView(((DbDataTableView)c).f.ChildRelations[relationName]);
	}

	private object[] a(DataRelation A_0)
	{
		object[] array = new object[A_0.ParentColumns.Length];
		if (Row != null)
		{
			for (int i = 0; i < A_0.ParentColumns.Length; i++)
			{
				array[i] = Row[A_0.ParentColumns[i]];
			}
		}
		return array;
	}

	private bool a(DataView A_0, object[] A_1, DataView A_2, object[] A_3)
	{
		if (A_0 == null || A_2 == null)
		{
			return true;
		}
		if (A_0 == null)
		{
			return false;
		}
		if ((object)A_0.GetType() != A_2.GetType() || A_0.Table != A_2.Table)
		{
			return false;
		}
		if (A_1.Length != A_3.Length)
		{
			return false;
		}
		for (int i = 0; i < A_1.Length; i++)
		{
			if (A_1[i] != null || A_3[i] != null)
			{
				if (A_1[i] == null)
				{
					return false;
				}
				if (!A_1[i].Equals(A_3[i]))
				{
					return false;
				}
			}
		}
		return true;
	}

	public DbDataTableView CreateChildView(DataRelation dataRelation)
	{
		DataView dataView;
		DbDataTable a_;
		if (this.m_a == null)
		{
			dataView = null;
			a_ = dataRelation.ChildTable as DbDataTable;
		}
		else
		{
			dataView = this.m_a.CreateChildView(dataRelation);
			a_ = (DbDataTable)dataView.Table;
		}
		ak ak2 = (ak)RelationViews[dataRelation];
		object[] a_2 = a(dataRelation);
		bool flag = ak2 == null;
		DbDataTableView dbDataTableView = null;
		if (!flag)
		{
			dbDataTableView = ak2.a;
			flag = !a(dataView, a_2, dbDataTableView.g, ak2.b);
		}
		if (flag)
		{
			dbDataTableView = new DbDataTableView(a_, dataView);
			dbDataTableView.h = this;
			RelationViews[dataRelation] = new ak(dbDataTableView, a_2);
		}
		return dbDataTableView;
	}

	internal static object[] a(DataRow A_0, DataRowVersion A_1)
	{
		DataColumnCollection columns = A_0.Table.Columns;
		object[] array = new object[columns.Count];
		for (int i = 0; i < array.Length; i++)
		{
			_ = columns[i];
			array[i] = A_0[i, A_1];
		}
		return array;
	}

	internal static object[] a(DataRow A_0, bool A_1)
	{
		DataColumnCollection columns = A_0.Table.Columns;
		object[] array = new object[columns.Count];
		for (int i = 0; i < array.Length; i++)
		{
			DataColumn dataColumn = columns[i];
			if (A_1 || !dataColumn.ReadOnly)
			{
				array[i] = A_0[i];
			}
		}
		return array;
	}

	public void Refresh()
	{
		((DbDataTableView)c).RefreshRow(this);
	}

	public void Delete()
	{
		c.Remove(this);
	}
}
