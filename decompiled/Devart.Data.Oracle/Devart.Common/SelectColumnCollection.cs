using System.Collections;
using System.ComponentModel;

namespace Devart.Common;

[ListBindable(false)]
internal sealed class SelectColumnCollection : CollectionBase, IList
{
	private SelectStatement m_a;

	private SelectStatementCollection b;

	private readonly bool c;

	public SelectColumn this[int index]
	{
		get
		{
			return (SelectColumn)base.List[index];
		}
		set
		{
			SelectColumn selectColumn = (SelectColumn)base.List[index];
			if (((SelectStatementNode)selectColumn).a != -1)
			{
				((SelectStatementNode)value).a = ((SelectStatementNode)selectColumn).a;
				((SelectStatementNode)value).b = ((SelectStatementNode)selectColumn).b;
				value.d();
			}
			base.List[index] = value;
		}
	}

	internal SelectStatementCollection CollectionNode => b;

	internal SelectColumnCollection(SelectStatement A_0, string A_1, bool A_2)
	{
		b = new SelectStatementCollection(this, A_1);
		c = A_2;
		this.m_a = A_0;
	}

	public SelectColumn Add(string name)
	{
		SelectColumn selectColumn = new SelectColumn(name);
		Add(selectColumn);
		return selectColumn;
	}

	public SelectColumn Add(string name, bool quote)
	{
		SelectColumn selectColumn = new SelectColumn(quote ? a(name) : name);
		Add(selectColumn);
		return selectColumn;
	}

	public SelectColumn Add(string schema, string table, string name, string alias)
	{
		SelectColumn selectColumn = new SelectColumn(schema, table, name, alias);
		Add(selectColumn);
		return selectColumn;
	}

	public SelectColumn Add(string schema, string table, string name, string alias, bool quote)
	{
		SelectColumn selectColumn = new SelectColumn(quote ? a(schema) : schema, quote ? a(table) : table, quote ? a(name) : name, quote ? a(alias) : alias);
		Add(selectColumn);
		return selectColumn;
	}

	public SelectColumn Add(string database, string schema, string table, string name, string alias)
	{
		SelectColumn selectColumn = new SelectColumn(database, schema, table, name, alias);
		Add(selectColumn);
		return selectColumn;
	}

	public SelectColumn Add(string database, string schema, string table, string name, string alias, bool quote)
	{
		SelectColumn selectColumn = new SelectColumn(quote ? a(database) : database, quote ? a(schema) : schema, quote ? a(table) : table, quote ? a(name) : name, quote ? a(alias) : alias);
		Add(selectColumn);
		return selectColumn;
	}

	public int Add(SelectColumn value)
	{
		return base.List.Add(value);
	}

	public void Insert(int index, SelectColumn value)
	{
		base.List.Insert(index, value);
	}

	public int IndexOf(SelectColumn value)
	{
		return base.List.IndexOf(value);
	}

	public bool Contains(SelectColumn value)
	{
		return base.List.Contains(value);
	}

	internal static void a(IList A_0, IList A_1, int A_2)
	{
		SelectStatementNode selectStatementNode = new SelectStatementNode();
		int num = A_0.Count;
		for (int num2 = A_0.Count - 1; num2 > -1; num2--)
		{
			if (((SelectStatementNode)A_0[num2]).Binded)
			{
				num = num2;
				break;
			}
		}
		if (A_2 + 1 < num)
		{
			selectStatementNode.a = ((SelectStatementNode)A_0[A_2]).a;
			selectStatementNode.b = ((SelectStatementNode)A_0[A_2 + 1]).a;
		}
		else if (A_2 > 0)
		{
			selectStatementNode.a = ((SelectStatementNode)A_0[A_2 - 1]).b;
			selectStatementNode.b = ((SelectStatementNode)A_0[A_2]).b;
		}
		else
		{
			selectStatementNode.a = ((SelectStatementNode)A_0[A_2]).a;
			selectStatementNode.b = ((SelectStatementNode)A_0[A_2]).b;
		}
		A_1.Add(selectStatementNode);
	}

	public void Remove(SelectColumn value)
	{
		RemoveAt(IndexOf(value));
	}

	protected void a(int A_0, object A_1)
	{
		if (base.Count == 1)
		{
			CollectionNode.Node.d();
		}
		CollectionNode.AddDeleted(A_0);
	}

	protected void a()
	{
		while (base.Count > 0)
		{
			RemoveAt(0);
		}
	}

	public void CopyTo(SelectColumn[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	internal void a(ICollection A_0)
	{
		base.InnerList.AddRange(A_0);
	}

	private string a(string A_0)
	{
		return this.m_a.QuoteStart + A_0 + this.m_a.QuoteEnd;
	}
}
