using System;
using System.ComponentModel;
using System.Globalization;

namespace Devart.Common;

[TypeConverter(typeof(l))]
public class ParentDataRelation
{
	private IListSource m_a;

	private DbDataTable m_b;

	private string[] m_c;

	private string[] m_d;

	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(null)]
	[Editor("Devart.Common.Design.ParentDataRelationParentTableEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
	public DbDataTable ParentTable
	{
		get
		{
			return (DbDataTable)this.m_a;
		}
		set
		{
			if (this.m_a != value)
			{
				if (this.m_b != null && this.m_a is ISupportInitializeNotification)
				{
					((ISupportInitializeNotification)this.m_a).Initialized -= this.m_b.s;
				}
				this.m_a = value;
				if (this.m_b != null && this.m_a is ISupportInitializeNotification)
				{
					((ISupportInitializeNotification)this.m_a).Initialized += this.m_b.s;
				}
				if (this.m_b != null)
				{
					this.m_b.ad.b(ParentTable);
				}
			}
		}
	}

	internal DbDataTable ChildTableInternal
	{
		set
		{
			if (this.m_b != value)
			{
				if (value == null)
				{
					this.m_b.ad.b(null);
				}
				if (this.m_b != null && this.m_a is ISupportInitializeNotification)
				{
					((ISupportInitializeNotification)this.m_a).Initialized -= this.m_b.s;
				}
				this.m_b = value;
				if (this.m_b != null && this.m_a is ISupportInitializeNotification)
				{
					((ISupportInitializeNotification)this.m_a).Initialized += this.m_b.s;
				}
				if (this.m_b != null)
				{
					this.m_b.ad.b(ParentTable);
					this.m_b.ad.a(ParentColumnNames);
				}
			}
		}
	}

	internal DbDataTable ChildTable => this.m_b;

	[RefreshProperties(RefreshProperties.Repaint)]
	[Editor("Devart.Common.Design.ParentDataRelationParentColumnsNamesEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
	[TypeConverter(typeof(m))]
	public string[] ParentColumnNames
	{
		get
		{
			return this.m_c;
		}
		set
		{
			this.m_c = value;
			if (this.m_b != null)
			{
				this.m_b.ad.a(value);
			}
		}
	}

	[RefreshProperties(RefreshProperties.Repaint)]
	[Editor("Devart.Common.Design.ParentDataRelationChildColumnsNamesEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
	[TypeConverter(typeof(m))]
	public string[] ChildColumnNames
	{
		get
		{
			return this.m_d;
		}
		set
		{
			this.m_d = value;
		}
	}

	internal ParentDataRelation()
	{
	}

	public ParentDataRelation(IListSource parentTable, string[] parentColumnNames, string[] childColumnNames)
	{
		this.m_a = parentTable;
		this.m_c = parentColumnNames;
		this.m_d = childColumnNames;
	}

	internal void b()
	{
		ParentTable = null;
	}

	internal void d()
	{
		ParentColumnNames = null;
	}

	internal void a()
	{
		ChildColumnNames = null;
	}

	private static bool a(string[] A_0, string A_1)
	{
		for (int i = 0; i < A_0.Length; i++)
		{
			if (string.Compare(A_0[i], A_1, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				return true;
			}
		}
		return false;
	}

	internal static bool a(ParentDataRelation A_0)
	{
		if (A_0 != null)
		{
			return A_0.ParentTable != null;
		}
		return false;
	}

	internal void c()
	{
		if (ChildColumnNames == null || ChildColumnNames.Length == 0)
		{
			throw new ArgumentException("The ChildColumnNames property is not set in the ParentDataRelation object.");
		}
		if (ParentColumnNames == null || ParentColumnNames.Length != ChildColumnNames.Length)
		{
			throw new ArgumentException("The ParentColumnNames property is not set in the ParentDataRelation object.");
		}
		if (ParentTable == null)
		{
			throw new ArgumentException("The ParentTable property is not set in the ParentDataRelation object.");
		}
		this.m_b.ad.b(ParentTable);
	}
}
