using System.Collections;
using System.ComponentModel;

namespace Devart.Common;

[ListBindable(false)]
internal sealed class SelectTableCollection : CollectionBase, IList
{
	private SelectStatementCollection m_a;

	private SelectStatement b;

	public SelectTable this[int index]
	{
		get
		{
			return (SelectTable)base.List[index];
		}
		set
		{
			SelectTable selectTable = (SelectTable)base.List[index];
			if (((SelectStatementNode)selectTable).a != -1)
			{
				((SelectStatementNode)value).a = ((SelectStatementNode)selectTable).a;
				((SelectStatementNode)value).b = ((SelectStatementNode)selectTable).b;
				value.d();
			}
			base.List[index] = value;
		}
	}

	internal SelectStatementCollection CollectionNode => this.m_a;

	internal SelectTableCollection(SelectStatement A_0, string A_1)
	{
		this.m_a = new SelectStatementCollection(this, A_1);
		b = A_0;
	}

	public SelectTable Add(string name)
	{
		SelectTable selectTable = new SelectTable(name);
		Add(selectTable);
		return selectTable;
	}

	public SelectTable Add(string name, bool quoted)
	{
		SelectTable selectTable = new SelectTable(quoted ? a(name) : name);
		Add(selectTable);
		return selectTable;
	}

	public SelectTable Add(string schema, string name, string alias)
	{
		SelectTable selectTable = new SelectTable(schema, name, alias);
		Add(selectTable);
		return selectTable;
	}

	public SelectTable Add(string schema, string name, string alias, bool quote)
	{
		SelectTable selectTable = new SelectTable(quote ? a(schema) : schema, quote ? a(name) : name, quote ? a(alias) : alias);
		Add(selectTable);
		return selectTable;
	}

	public SelectTable Add(string database, string schema, string name, string alias)
	{
		SelectTable selectTable = new SelectTable(database, schema, name, alias);
		Add(selectTable);
		return selectTable;
	}

	public SelectTable Add(string database, string schema, string name, string alias, bool quote)
	{
		SelectTable selectTable = new SelectTable(quote ? a(database) : database, quote ? a(schema) : schema, quote ? a(name) : name, quote ? a(alias) : alias);
		Add(selectTable);
		return selectTable;
	}

	public int Add(SelectTable value)
	{
		return base.List.Add(value);
	}

	public void Insert(int index, SelectTable value)
	{
		base.List.Insert(index, value);
	}

	public int IndexOf(SelectTable value)
	{
		return base.List.IndexOf(value);
	}

	public bool Contains(SelectTable value)
	{
		return base.List.Contains(value);
	}

	public void Remove(SelectTable value)
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

	public void CopyTo(SelectTable[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	internal void a(ICollection A_0)
	{
		base.InnerList.AddRange(A_0);
	}

	private string a(string A_0)
	{
		return b.QuoteStart + A_0 + b.QuoteEnd;
	}
}
