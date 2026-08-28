using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Devart.Common;

public abstract class DbDataSet : DataSet, IListSource, ISupportInitialize
{
	private ao m_a;

	private object m_b;

	private bool c;

	private CollectionChangeEventHandler d;

	private string e = string.Empty;

	private DbConnection f;

	private object g;

	bool IListSource.ContainsListCollection => true;

	[DefaultValue("")]
	[Browsable(false)]
	public string Name
	{
		get
		{
			if (Site != null)
			{
				return Site.Name;
			}
			if (e != null)
			{
				return e;
			}
			return string.Empty;
		}
		set
		{
			if (Site == null)
			{
				e = ((value == null) ? string.Empty : value);
			}
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public object Owner
	{
		get
		{
			return this.m_b;
		}
		set
		{
			if (this.m_b != value)
			{
				for (int i = 0; i < base.Tables.Count; i++)
				{
					if (base.Tables[i] is DbDataTable dbDataTable && dbDataTable.Owner != value)
					{
						dbDataTable.Owner = value;
					}
				}
			}
			if (this.m_b != null && !DesignMode)
			{
				GlobalComponentsCache.RemoveFromGlobalList(this);
			}
			this.m_b = value;
			if (this.m_b != null && !DesignMode)
			{
				GlobalComponentsCache.AddToGlobalList(this);
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	protected internal bool Reloading
	{
		get
		{
			return c;
		}
		set
		{
			c = value;
			for (int i = 0; i < base.Tables.Count; i++)
			{
				if (base.Tables[i] is DbDataTable dbDataTable)
				{
					dbDataTable.Reloading = value;
				}
			}
		}
	}

	[MergableProperty(false)]
	[Browsable(false)]
	public DbConnection Connection
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

	[DefaultValue(SerializationFormat.Xml)]
	[r("DbDataSet_RomotingFormat")]
	public new SerializationFormat RemotingFormat
	{
		get
		{
			return base.RemotingFormat;
		}
		set
		{
			base.RemotingFormat = value;
		}
	}

	internal object FetchRowSyncRoot
	{
		get
		{
			if (g == null)
			{
				g = new object();
			}
			return g;
		}
	}

	public DbDataSet()
	{
		d = a;
		base.Tables.CollectionChanged += d;
		base.Relations.CollectionChanged += b;
	}

	public DbDataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema)
		: base(info, context, ConstructSchema)
	{
		d = a;
		base.Tables.CollectionChanged += d;
		base.Relations.CollectionChanged += b;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			base.Tables.CollectionChanged -= d;
		}
		base.Dispose(disposing);
		if (Owner != null && !DesignMode)
		{
			GlobalComponentsCache.RemoveFromGlobalList(this);
		}
	}

	private void b(object A_0, CollectionChangeEventArgs A_1)
	{
		DataRelation dataRelation = A_1.Element as DataRelation;
		if (A_1.Action != CollectionChangeAction.Add || dataRelation == null)
		{
			return;
		}
		if (dataRelation.ChildTable is DbDataTable { Site: not null } dbDataTable && dbDataTable.Site.Container != null)
		{
			for (int i = 0; i < dbDataTable.Columns.Count; i++)
			{
				DataColumn dataColumn = dbDataTable.Columns[i];
				if (dataColumn != null && dataColumn.Site == null)
				{
					dbDataTable.Site.Container.Add(dataColumn, dbDataTable.Site.Name + "_" + dataColumn.ColumnName);
				}
			}
		}
		if (!(dataRelation.ParentTable is DbDataTable { Site: not null } dbDataTable2) || dbDataTable2.Site.Container == null)
		{
			return;
		}
		for (int num = 0; num < dbDataTable2.Columns.Count; num++)
		{
			DataColumn dataColumn2 = dbDataTable2.Columns[num];
			if (dataColumn2 != null && dataColumn2.Site == null)
			{
				dbDataTable2.Site.Container.Add(dataColumn2, dbDataTable2.Site.Name + "_" + dataColumn2.ColumnName);
			}
		}
	}

	IList IListSource.GetList()
	{
		if (this.m_a == null)
		{
			this.m_a = new ao(base.DefaultViewManager);
		}
		return this.m_a;
	}

	private void a(object A_0, CollectionChangeEventArgs A_1)
	{
		if (A_1.Action == CollectionChangeAction.Add && A_1.Element is DbDataTable dbDataTable)
		{
			dbDataTable.Owner = Owner;
		}
		if (this.m_a != null)
		{
			this.m_a.a(A_0, new ListChangedEventArgs(ListChangedType.Reset, 0));
		}
	}

	public void Fill()
	{
		bool enforceConstraints = base.EnforceConstraints;
		try
		{
			base.EnforceConstraints = false;
			int count = base.Tables.Count;
			for (int num = count - 1; num >= 0; num--)
			{
				if (base.Tables[num] is DbDataTable dbDataTable)
				{
					dbDataTable.Fill();
				}
			}
		}
		finally
		{
			base.EnforceConstraints = enforceConstraints;
		}
	}

	public new void Clear()
	{
		bool enforceConstraints = base.EnforceConstraints;
		try
		{
			base.EnforceConstraints = false;
			for (int i = 0; i < base.Tables.Count; i++)
			{
				DataTable dataTable = base.Tables[i];
				if (dataTable is DbDataTable dbDataTable)
				{
					dbDataTable.Clear();
				}
				else
				{
					dataTable.Clear();
				}
			}
		}
		finally
		{
			base.EnforceConstraints = enforceConstraints;
		}
	}

	public void Update()
	{
		bool enforceConstraints = base.EnforceConstraints;
		try
		{
			base.EnforceConstraints = false;
			int count = base.Tables.Count;
			for (int i = 0; i < count; i++)
			{
				if (base.Tables[i] is DbDataTable dbDataTable)
				{
					dbDataTable.Update();
				}
			}
		}
		finally
		{
			base.EnforceConstraints = enforceConstraints;
		}
	}

	public override DataSet Clone()
	{
		DbDataSet dbDataSet = (DbDataSet)base.Clone();
		for (int i = 0; i < dbDataSet.Tables.Count; i++)
		{
			if (base.Tables[i] is DbDataTable dbDataTable)
			{
				dbDataTable.a((DbDataTable)dbDataSet.Tables[i]);
			}
		}
		dbDataSet.Connection = Connection;
		return dbDataSet;
	}

	internal DbDataTable a(DbDataTable A_0, PropertyDescriptor[] A_1, int A_2, out int A_3)
	{
		if (A_1.Length < A_2 + 1)
		{
			A_3 = A_1.Length - 1;
			return A_0;
		}
		PropertyDescriptor propertyDescriptor = A_1[A_2];
		if (A_0 == null)
		{
			if (propertyDescriptor is DataViewManagerPropertyDescriptor)
			{
				return a(((DataViewManagerPropertyDescriptor)propertyDescriptor).DataTable, A_1, A_2 + 1, out A_3);
			}
			A_3 = -1;
			return null;
		}
		if (propertyDescriptor is g && ((g)propertyDescriptor).c() != null)
		{
			return a((DbDataTable)((g)propertyDescriptor).c().ChildTable, A_1, A_2 + 1, out A_3);
		}
		if (propertyDescriptor is g)
		{
			A_3 = A_2 - 1;
			return A_0;
		}
		A_3 = -1;
		return null;
	}

	public new XmlReadMode ReadXml(Stream stream)
	{
		XmlReadMode result = base.ReadXml(stream);
		a();
		return result;
	}

	public new XmlReadMode ReadXml(string fileName)
	{
		XmlReadMode result = base.ReadXml(fileName);
		a();
		return result;
	}

	public new XmlReadMode ReadXml(TextReader reader)
	{
		XmlReadMode result = base.ReadXml(reader);
		a();
		return result;
	}

	public new XmlReadMode ReadXml(XmlReader reader)
	{
		XmlReadMode result = base.ReadXml(reader);
		a();
		return result;
	}

	public new XmlReadMode ReadXml(Stream stream, XmlReadMode mode)
	{
		XmlReadMode result = base.ReadXml(stream, mode);
		a();
		return result;
	}

	public new XmlReadMode ReadXml(string fileName, XmlReadMode mode)
	{
		XmlReadMode result = base.ReadXml(fileName, mode);
		a();
		return result;
	}

	public new XmlReadMode ReadXml(TextReader reader, XmlReadMode mode)
	{
		XmlReadMode result = base.ReadXml(reader, mode);
		a();
		return result;
	}

	public new XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
	{
		XmlReadMode result = base.ReadXml(reader, mode);
		a();
		return result;
	}

	private void a()
	{
		bool flag = false;
		for (int i = 0; i < base.Tables.Count; i++)
		{
			if (!(base.Tables[i] is DbDataTable))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		DataTable[] array = new DataTable[base.Tables.Count];
		DataRelation[] array2 = new DataRelation[base.Relations.Count];
		ArrayList[] array3 = new ArrayList[base.Tables.Count];
		Dictionary<UniqueConstraint, bool> dictionary = new Dictionary<UniqueConstraint, bool>();
		base.Relations.CopyTo(array2, 0);
		base.Relations.Clear();
		for (int num = 0; num < base.Tables.Count; num++)
		{
			array[num] = base.Tables[num];
			array3[num] = new ArrayList();
			for (int num2 = 0; num2 < base.Tables[num].Constraints.Count; num2++)
			{
				if (base.Tables[num].Constraints[num2] is ForeignKeyConstraint)
				{
					array3[num].Add(base.Tables[num].Constraints[num2]);
					base.Tables[num].Constraints.RemoveAt(num2--);
				}
			}
		}
		for (int num3 = 0; num3 < base.Tables.Count; num3++)
		{
			foreach (Constraint constraint3 in base.Tables[num3].Constraints)
			{
				array3[num3].Add(constraint3);
				if (constraint3 is UniqueConstraint)
				{
					dictionary.Add((UniqueConstraint)constraint3, ((UniqueConstraint)constraint3).IsPrimaryKey);
				}
			}
			base.Tables[num3].Constraints.Clear();
		}
		base.Tables.Clear();
		for (int num4 = 0; num4 < array.Length; num4++)
		{
			DbDataTable dbDataTable = CreateDataTable();
			DbDataTable.a(array[num4], dbDataTable);
			dbDataTable.BeginLoadData();
			DataTable dataTable = array[num4];
			try
			{
				foreach (DataRow row in dataTable.Rows)
				{
					dbDataTable.Rows.Add(row.ItemArray);
				}
			}
			finally
			{
				dbDataTable.EndLoadData();
			}
			base.Tables.Add(dbDataTable);
		}
		for (int num5 = 0; num5 < base.Tables.Count; num5++)
		{
			for (int num6 = 0; num6 < array3[num5].Count; num6++)
			{
				if (!(array3[num5][num6] is UniqueConstraint))
				{
					continue;
				}
				UniqueConstraint uniqueConstraint = (UniqueConstraint)array3[num5][num6];
				DataColumn[] array4 = new DataColumn[uniqueConstraint.Columns.Length];
				for (int num7 = 0; num7 < array4.Length; num7++)
				{
					array4[num7] = base.Tables[num5].Columns[uniqueConstraint.Columns[num7].ColumnName];
				}
				UniqueConstraint uniqueConstraint2 = new UniqueConstraint(uniqueConstraint.ConstraintName, array4, dictionary[uniqueConstraint]);
				foreach (object key in uniqueConstraint.ExtendedProperties.Keys)
				{
					uniqueConstraint2.ExtendedProperties.Add(key, uniqueConstraint.ExtendedProperties[key]);
				}
				base.Tables[num5].Constraints.Add(uniqueConstraint2);
				array3[num5].RemoveAt(num6--);
			}
		}
		for (int num8 = 0; num8 < base.Tables.Count; num8++)
		{
			foreach (Constraint item in array3[num8])
			{
				if (!(item is ForeignKeyConstraint))
				{
					continue;
				}
				ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)item;
				DataColumn[] array5 = new DataColumn[foreignKeyConstraint.Columns.Length];
				for (int num9 = 0; num9 < array5.Length; num9++)
				{
					array5[num9] = base.Tables[foreignKeyConstraint.Table.TableName].Columns[foreignKeyConstraint.Columns[num9].ColumnName];
				}
				DataColumn[] array6 = new DataColumn[foreignKeyConstraint.RelatedColumns.Length];
				for (int num10 = 0; num10 < array6.Length; num10++)
				{
					array6[num10] = base.Tables[foreignKeyConstraint.RelatedTable.TableName].Columns[foreignKeyConstraint.RelatedColumns[num10].ColumnName];
				}
				ForeignKeyConstraint foreignKeyConstraint2 = new ForeignKeyConstraint(foreignKeyConstraint.ConstraintName, array6, array5);
				foreignKeyConstraint2.AcceptRejectRule = foreignKeyConstraint.AcceptRejectRule;
				foreignKeyConstraint2.DeleteRule = foreignKeyConstraint.DeleteRule;
				foreignKeyConstraint2.UpdateRule = foreignKeyConstraint.UpdateRule;
				foreach (object key2 in foreignKeyConstraint.ExtendedProperties.Keys)
				{
					foreignKeyConstraint2.ExtendedProperties.Add(key2, foreignKeyConstraint.ExtendedProperties[key2]);
				}
				base.Tables[num8].Constraints.Add(foreignKeyConstraint2);
			}
		}
		for (int num11 = 0; num11 < array2.Length; num11++)
		{
			string[] array7 = new string[array2[num11].ParentColumns.Length];
			for (int num12 = 0; num12 < array2[num11].ParentColumns.Length; num12++)
			{
				array7[num12] = array2[num11].ParentColumns[num12].ColumnName;
			}
			string[] array8 = new string[array2[num11].ChildColumns.Length];
			for (int num13 = 0; num13 < array2[num11].ChildColumns.Length; num13++)
			{
				array8[num13] = array2[num11].ChildColumns[num13].ColumnName;
			}
			new DataRelation(array2[num11].RelationName, array2[num11].ParentTable.TableName, array2[num11].ChildTable.TableName, array7, array8, array2[num11].Nested);
		}
	}

	public virtual DbDataTable CreateDataTable()
	{
		throw new Exception();
	}

	protected override bool ShouldSerializeRelations()
	{
		if (base.Relations == null)
		{
			return true;
		}
		return base.Relations.Count != 0;
	}

	protected override bool ShouldSerializeTables()
	{
		if (base.Tables == null)
		{
			return true;
		}
		return base.Tables.Count != 0;
	}

	void ISupportInitialize.BeginInit()
	{
		BeginInit();
		for (int i = 0; i < base.Tables.Count; i++)
		{
			if (base.Tables[i] is DbDataTable dbDataTable)
			{
				dbDataTable.BeginInit();
			}
		}
	}

	void ISupportInitialize.EndInit()
	{
		for (int i = 0; i < base.Tables.Count; i++)
		{
			if (base.Tables[i] is DbDataTable dbDataTable)
			{
				dbDataTable.EndInit();
			}
		}
		EndInit();
	}
}
