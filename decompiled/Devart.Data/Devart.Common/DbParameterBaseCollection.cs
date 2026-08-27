using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace Devart.Common;

public abstract class DbParameterBaseCollection : DbParameterCollection
{
	private ArrayList m_a;

	public override int Count
	{
		get
		{
			if (this.m_a == null)
			{
				return 0;
			}
			return this.m_a.Count;
		}
	}

	private ArrayList InnerList
	{
		get
		{
			ArrayList arrayList = this.m_a;
			if (arrayList == null)
			{
				arrayList = (this.m_a = new ArrayList());
			}
			return arrayList;
		}
	}

	public override bool IsFixedSize => InnerList.IsFixedSize;

	public override bool IsReadOnly => InnerList.IsReadOnly;

	public override bool IsSynchronized => InnerList.IsSynchronized;

	protected abstract Type ItemType { get; }

	protected virtual string ParameterNamePrefix => "Parameter";

	public override object SyncRoot => InnerList.SyncRoot;

	protected abstract DbCommandBase Parent { get; }

	[EditorBrowsable(EditorBrowsableState.Never)]
	public override int Add(object value)
	{
		OnChange();
		ValidateType(value);
		if (Parent != null && Parent.ParameterCheck)
		{
			DbParameter dbParameter = (DbParameter)value;
			int num = IndexOf(dbParameter.ParameterName);
			if (num >= 0)
			{
				SetParameter(num, dbParameter);
				return num;
			}
		}
		Validate(-1, value);
		InnerList.Add((DbParameterBase)value);
		return Count - 1;
	}

	public override void AddRange(Array values)
	{
		Utils.CheckArgumentNull(values, "values");
		OnChange();
		foreach (object value in values)
		{
			ValidateType(value);
		}
		foreach (DbParameter value2 in values)
		{
			Validate(-1, value2);
			InnerList.Add((DbParameterBase)value2);
		}
	}

	protected int CheckName(string parameterName)
	{
		int num = IndexOf(parameterName);
		if (num < 0)
		{
			throw new ArgumentException(n.a("ParametersSourceIndex", parameterName));
		}
		return num;
	}

	public override void Clear()
	{
		OnChange();
		ArrayList arrayList = this.m_a;
		if (arrayList == null)
		{
			return;
		}
		foreach (DbParameterBase item in arrayList)
		{
			item.f();
		}
		arrayList.Clear();
	}

	public override bool Contains(object value)
	{
		return IndexOf(value) != -1;
	}

	public override bool Contains(string value)
	{
		return IndexOf(value) != -1;
	}

	public override void CopyTo(Array array, int index)
	{
		InnerList.CopyTo(array, index);
	}

	public override IEnumerator GetEnumerator()
	{
		return InnerList.GetEnumerator();
	}

	protected override DbParameter GetParameter(int index)
	{
		b(index);
		return (DbParameter)InnerList[index];
	}

	protected override DbParameter GetParameter(string name)
	{
		return GetParameter(CheckName(name));
	}

	public override int IndexOf(object value)
	{
		if (value != null)
		{
			ValidateType(value);
			ArrayList arrayList = this.m_a;
			if (arrayList != null)
			{
				int count = arrayList.Count;
				for (int i = 0; i < count; i++)
				{
					if (value == arrayList[i])
					{
						return i;
					}
				}
			}
		}
		return -1;
	}

	public override int IndexOf(string parameterName)
	{
		return IndexOf(this.m_a, parameterName);
	}

	protected internal static int IndexOf(IEnumerable items, string parameterName)
	{
		if (items != null)
		{
			int num = 0;
			foreach (DbParameter item in items)
			{
				if (parameterName == item.ParameterName)
				{
					return num;
				}
				num++;
			}
			num = 0;
			foreach (DbParameter item2 in items)
			{
				if (string.Compare(parameterName, item2.ParameterName, StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					return num;
				}
				num++;
			}
		}
		return -1;
	}

	public override void Insert(int index, object value)
	{
		OnChange();
		ValidateType(value);
		if (Parent != null && Parent.ParameterCheck)
		{
			DbParameter dbParameter = (DbParameter)value;
			int num = IndexOf(dbParameter.ParameterName);
			if (num >= 0)
			{
				DbParameterBase dbParameterBase = (DbParameterBase)InnerList[num];
				InnerList.Remove(dbParameterBase);
				InnerList.Insert(index, dbParameterBase);
				SetParameter(index, dbParameter);
				return;
			}
		}
		Validate(-1, value);
		InnerList.Insert(index, (DbParameterBase)value);
	}

	protected virtual void OnChange()
	{
	}

	private void b(int A_0)
	{
		if (A_0 < 0 || A_0 >= Count)
		{
			throw new IndexOutOfRangeException(n.a("ParametersMappingIndex", A_0));
		}
	}

	public override void Remove(object value)
	{
		OnChange();
		ValidateType(value);
		int num = IndexOf(value);
		if (num != -1)
		{
			a(num);
		}
		else if (this != ((DbParameterBase)value).a(null, this))
		{
			throw new InvalidOperationException(n.a("CollectionRemoveInvalidObject", ItemType, this));
		}
	}

	public override void RemoveAt(int index)
	{
		OnChange();
		b(index);
		a(index);
	}

	public override void RemoveAt(string parameterName)
	{
		OnChange();
		int a_ = CheckName(parameterName);
		a(a_);
	}

	private void a(int A_0)
	{
		ArrayList arrayList = InnerList;
		DbParameterBase dbParameterBase = (DbParameterBase)arrayList[A_0];
		arrayList.RemoveAt(A_0);
		dbParameterBase.f();
	}

	private void a(int A_0, object A_1)
	{
		ArrayList arrayList = InnerList;
		ValidateType(A_1);
		Validate(A_0, A_1);
		DbParameterBase dbParameterBase = (DbParameterBase)arrayList[A_0];
		arrayList[A_0] = (DbParameterBase)A_1;
		dbParameterBase.f();
	}

	protected override void SetParameter(int index, DbParameter value)
	{
		OnChange();
		b(index);
		a(index, value);
	}

	protected override void SetParameter(string name, DbParameter value)
	{
		SetParameter(CheckName(name), value);
	}

	protected virtual void Validate(int index, object value)
	{
		Utils.CheckArgumentNull(value, "value");
		DbParameterBase dbParameterBase = (DbParameterBase)value;
		object obj = dbParameterBase.a(this, null);
		if (obj != null)
		{
			if (obj != this)
			{
				throw new ArgumentException(n.a("ParametersIsNotParent", dbParameterBase.ParameterName));
			}
			if (IndexOf(value) != index)
			{
				throw new ArgumentException(n.a("ParametersIsParent", dbParameterBase.ParameterName));
			}
		}
		string parameterName = dbParameterBase.ParameterName;
		if (parameterName.Length == 0)
		{
			index = 1;
			do
			{
				parameterName = ParameterNamePrefix + index;
				index++;
			}
			while (IndexOf(parameterName) != -1);
			dbParameterBase.ParameterName = parameterName;
		}
	}

	protected virtual void ValidateType(object value)
	{
		Utils.CheckArgumentNull(value, "value");
		if (!ItemType.IsInstanceOfType(value))
		{
			throw new ArgumentException(n.a("InvalidParameterType"));
		}
	}
}
