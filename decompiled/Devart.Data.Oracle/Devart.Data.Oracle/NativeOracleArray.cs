using System;
using System.Collections;
using System.Globalization;
using Devart.Common;

namespace Devart.Data.Oracle;

public class NativeOracleArray : NativeOracleObjectBase, ak
{
	internal new ak Instance => (ak)base.Instance;

	public Array Value
	{
		get
		{
			Type type = OracleUtils.OracleDbTypeToType(base.ObjectType.g.DbType);
			Array array = Array.CreateInstance(type, Count);
			for (int num = 0; num < Count; num++)
			{
				array.SetValue(Convert.ChangeType(this[num], type, CultureInfo.CurrentCulture), num);
			}
			return array;
		}
		set
		{
			Clear();
			if (value != null)
			{
				for (int num = 0; num < value.Length; num++)
				{
					Add(value.GetValue(num));
				}
			}
		}
	}

	public bool IsFixedSize => Instance.IsFixedSize;

	public bool IsReadOnly => Instance.IsReadOnly;

	public object this[int index]
	{
		get
		{
			return b(Instance[index]);
		}
		set
		{
			Instance[index] = a(value);
		}
	}

	public int Count => Instance.Count;

	public bool IsSynchronized => Instance.IsSynchronized;

	public object SyncRoot => Instance.SyncRoot;

	public NativeOracleArray(string typeName, OracleConnection connection)
		: base(typeName, connection)
	{
	}

	public NativeOracleArray(OracleType objectType, OracleConnection connection)
		: base(objectType, connection)
	{
	}

	internal NativeOracleArray(ak A_0, OracleConnection A_1, bool A_2)
		: base(A_0, A_1, A_2)
	{
	}

	public object GetOracleValue(int i)
	{
		return b(Instance.a(i));
	}

	object ak.a(int i)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetOracleValue
		return this.GetOracleValue(i);
	}

	public int Add(object value)
	{
		return Instance.Add(a(value));
	}

	public void Clear()
	{
		Instance.Clear();
	}

	public bool Contains(object value)
	{
		return Instance.Contains(a(value));
	}

	public int IndexOf(object value)
	{
		return Instance.IndexOf(a(value));
	}

	public void Insert(int index, object value)
	{
		Instance.Insert(index, a(value));
	}

	public void Remove(object value)
	{
		Instance.Remove(a(value));
	}

	public void RemoveAt(int index)
	{
		Instance.RemoveAt(index);
	}

	public void CopyTo(Array array, int index)
	{
		for (int num = 0; num < Count; num++)
		{
			array.SetValue(this[num], index + num);
		}
	}

	public IEnumerator GetEnumerator()
	{
		return new y(this);
	}

	protected override object GetItemValue(int i)
	{
		return this[i];
	}

	protected override void SetItemValue(object value, int i)
	{
		this[i] = value;
	}

	protected override object GetItemValue(string name)
	{
		throw new NotSupportedException(Devart.Common.al.a("OraArrayNotSupportAttributes"));
	}

	protected override void SetItemValue(object value, string name)
	{
		throw new NotSupportedException(Devart.Common.al.a("OraArrayNotSupportAttributes"));
	}

	private object b(object A_0)
	{
		return ConvertFromInternalValue(A_0, base.ObjectType.g);
	}

	private object a(object A_0)
	{
		return GetInternalValue(A_0, base.ObjectType.g.DbType);
	}
}
