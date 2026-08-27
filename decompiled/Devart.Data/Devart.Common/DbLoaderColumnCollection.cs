using System;
using System.Collections;
using System.ComponentModel;

namespace Devart.Common;

[ListBindable(false)]
public class DbLoaderColumnCollection : CollectionBase, IList
{
	public DbLoaderColumn this[int index]
	{
		get
		{
			return (DbLoaderColumn)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	public DbLoaderColumn this[string name]
	{
		get
		{
			int num = IndexOf(name);
			if (num < 0)
			{
				return null;
			}
			return (DbLoaderColumn)base.List[num];
		}
		set
		{
			int num = IndexOf(name);
			if (num >= 0)
			{
				base.List[num] = value;
			}
		}
	}

	public int Add(DbLoaderColumn value)
	{
		return base.List.Add(value);
	}

	public void Insert(int index, DbLoaderColumn value)
	{
		base.List.Insert(index, value);
	}

	public int IndexOf(DbLoaderColumn value)
	{
		return base.List.IndexOf(value);
	}

	public int IndexOf(string name)
	{
		int num = a(name);
		if (num == -2)
		{
			throw new ArgumentException(string.Format(n.a("DbLoader_TwoColunmsExist"), name));
		}
		return num;
	}

	public bool Contains(DbLoaderColumn value)
	{
		return base.List.Contains(value);
	}

	public bool Contains(string name)
	{
		bool result = false;
		for (int i = 0; i < base.List.Count; i++)
		{
			if (string.Compare(((DbLoaderColumn)base.List[i]).Name, name, ignoreCase: true) == 0)
			{
				return result;
			}
		}
		return result;
	}

	public void Remove(DbLoaderColumn value)
	{
		base.List.Remove(value);
	}

	public void CopyTo(DbLoaderColumn[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override void OnInsert(int index, object value)
	{
		DbLoaderColumn dbLoaderColumn = (DbLoaderColumn)value;
		if (Utils.IsEmpty(dbLoaderColumn.Name))
		{
			dbLoaderColumn.Name = "Column" + a();
		}
	}

	private int a(string A_0)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException("name");
		}
		int num = -1;
		for (int i = 0; i < base.List.Count; i++)
		{
			if (Utils.Compare(A_0, this[i].Name, ignoreCase: false))
			{
				num = i;
				break;
			}
		}
		if (num > -1)
		{
			return num;
		}
		for (int num2 = 0; num2 < base.List.Count; num2++)
		{
			if (Utils.Compare(A_0, this[num2].Name, ignoreCase: true))
			{
				if (num != -1)
				{
					num = -2;
					break;
				}
				num = num2;
			}
		}
		return num;
	}

	private int a()
	{
		int num = 1;
		DbLoaderColumn dbLoaderColumn = null;
		bool flag;
		do
		{
			flag = false;
			for (int i = 0; i < base.List.Count; i++)
			{
				dbLoaderColumn = (DbLoaderColumn)base.List[i];
				if (dbLoaderColumn.Name == "Column" + num)
				{
					flag = true;
					num++;
					break;
				}
			}
		}
		while (flag);
		return num;
	}
}
