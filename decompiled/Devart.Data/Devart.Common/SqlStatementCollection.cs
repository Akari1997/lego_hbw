using System.Collections;

namespace Devart.Common;

public class SqlStatementCollection : CollectionBase
{
	public SqlStatement this[int index]
	{
		get
		{
			return (SqlStatement)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	public int Add(SqlStatement value)
	{
		return base.List.Add(value);
	}

	public void Insert(int index, SqlStatement value)
	{
		base.List.Insert(index, value);
	}

	public int IndexOf(SqlStatement value)
	{
		return base.List.IndexOf(value);
	}

	public bool Contains(SqlStatement value)
	{
		return base.List.Contains(value);
	}

	public void Remove(SqlStatement value)
	{
		base.List.Remove(value);
	}

	public void CopyTo(SqlStatement[] array, int index)
	{
		base.List.CopyTo(array, index);
	}
}
