using System;
using System.Collections;
using System.Data.SqlTypes;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleArray : ak, ICustomOracleArray, ICloneable, INullable
{
	internal OracleObject a;

	protected OracleType oraType;

	protected ArrayList arrayList;

	private bool b;

	private static OracleArray c = new OracleArray(OracleType.r);

	public static OracleArray Null => c;

	public bool IsFixedSize => false;

	public bool IsReadOnly => false;

	public object SyncRoot => this;

	public bool IsSynchronized => false;

	public int Count => arrayList.Count;

	public object this[int i]
	{
		get
		{
			return GetItemValue(i);
		}
		set
		{
			arrayList[i] = OracleObject.b(value, oraType.g);
		}
	}

	public OracleType ObjectType => oraType;

	public int Capacity => oraType.ArrayCapacity;

	public bool IsNull
	{
		get
		{
			return b;
		}
		set
		{
			if (value != b)
			{
				b = value;
				if (!value && this.a != null)
				{
					this.a.SetObjectIsNull(isNull: false, this);
				}
			}
		}
	}

	public OracleArray(string typeName, OracleConnection connection)
		: this(OracleType.GetObjectType(typeName, connection))
	{
	}

	public OracleArray(OracleType oraType)
	{
		this.oraType = oraType;
		arrayList = new ArrayList(0);
		b = true;
	}

	public OracleArray(string typeName, OracleConnection connection, IEnumerable source)
		: this(typeName, connection)
	{
		a(source);
	}

	public OracleArray(OracleType oraType, IEnumerable source)
		: this(oraType)
	{
		a(source);
	}

	internal static object a(ak A_0, OracleConnection A_1, bool A_2)
	{
		if (A_0 == null)
		{
			return null;
		}
		Type udtType = A_0.get_ObjectType().UdtType;
		if ((object)A_0.GetType() == udtType)
		{
			return A_0;
		}
		ICustomOracleArray customOracleArray = (((object)udtType == typeof(OracleArray)) ? new OracleArray(A_0.get_ObjectType()) : (((object)udtType != typeof(OracleTable)) ? ((ICustomOracleArray)Activator.CreateInstance(udtType)) : new OracleTable(A_0.get_ObjectType())));
		NativeOracleArray nativeOracleArray = new NativeOracleArray(A_0, A_1, A_2);
		customOracleArray.FromOracleArray(nativeOracleArray);
		nativeOracleArray.Dispose();
		return customOracleArray;
	}

	private void a(IEnumerable A_0)
	{
		foreach (object item in A_0)
		{
			Add(item);
		}
	}

	void ICustomOracleArray.FromOracleArray(NativeOracleArray oraArray)
	{
		arrayList.Clear();
		IsNull = oraArray.IsNull;
		if (!IsNull)
		{
			_ = oraType.g.h;
			int count = oraArray.Count;
			for (int num = 0; num < count; num++)
			{
				object oracleValue = oraArray.GetOracleValue(num);
				oracleValue = OracleObject.c(oracleValue, oraType.g);
				Add(oracleValue);
			}
		}
	}

	NativeOracleArray ICustomOracleArray.ToOracleArray(OracleConnection con)
	{
		NativeOracleArray nativeOracleArray = new NativeOracleArray(oraType, con);
		a(con, nativeOracleArray);
		return nativeOracleArray;
	}

	internal void a(OracleConnection A_0, NativeOracleArray A_1)
	{
		A_1.IsNull = IsNull;
		if (IsNull)
		{
			return;
		}
		bool flag = oraType.g.h != null;
		for (int num = 0; num < arrayList.Count; num++)
		{
			object obj = arrayList[num];
			if (flag)
			{
				if (obj is ICustomOracleObject customOracleObject)
				{
					obj = customOracleObject.ToOracleObject(A_0);
				}
				else if (obj is ICustomOracleArray customOracleArray)
				{
					obj = customOracleArray.ToOracleArray(A_0);
				}
			}
			A_1.Add(obj);
		}
	}

	public void Clear()
	{
		if (oraType == OracleType.r)
		{
			throw new InvalidOperationException();
		}
		IsNull = true;
		arrayList.Clear();
	}

	public bool Contains(object value)
	{
		for (int num = 0; num < Count; num++)
		{
			if (this[num] == value)
			{
				return true;
			}
		}
		return false;
	}

	public int IndexOf(object value)
	{
		for (int num = 0; num < Count; num++)
		{
			object obj = this[num];
			if ((obj == null && value == null) || (obj != null && obj.Equals(value)))
			{
				return num;
			}
		}
		return -1;
	}

	public void Insert(int index, object value)
	{
		throw new NotSupportedException(Devart.Common.al.a("OraArrayNotSupportAttributes"));
	}

	public void CopyTo(Array array, int index)
	{
		arrayList.CopyTo(array, index);
	}

	public IEnumerator GetEnumerator()
	{
		return arrayList.GetEnumerator();
	}

	public void Remove(object value)
	{
		if (oraType == OracleType.r)
		{
			throw new InvalidOperationException();
		}
		RemoveAt(IndexOf(value));
	}

	public void RemoveAt(int i)
	{
		if (oraType == OracleType.r)
		{
			throw new InvalidOperationException();
		}
		arrayList.RemoveAt(i);
	}

	protected virtual OracleArray CreateInstance()
	{
		return new OracleArray(oraType);
	}

	public object Clone()
	{
		OracleArray oracleArray = CreateInstance();
		oracleArray.b = b;
		if (!b && this.arrayList != null)
		{
			ArrayList arrayList = (oracleArray.arrayList = new ArrayList(this.arrayList.Count));
			for (int num = 0; num < this.arrayList.Count; num++)
			{
				object obj = this.arrayList[num];
				if (obj is ICloneable cloneable)
				{
					object value = cloneable.Clone();
					arrayList.Add(value);
				}
				else
				{
					arrayList.Add(obj);
				}
			}
		}
		return oracleArray;
	}

	public int Add(object value)
	{
		if (oraType == OracleType.r)
		{
			throw new InvalidOperationException();
		}
		if (oraType.ArrayCapacity > 0 && arrayList.Count >= oraType.ArrayCapacity)
		{
			throw new InvalidOperationException("Maximum capacity attained");
		}
		IsNull = false;
		return arrayList.Add(OracleObject.b(value, oraType.g));
	}

	protected object GetItemValue(int i)
	{
		return OracleObject.d(arrayList[i], oraType.g);
	}

	public object GetOracleValue(int i)
	{
		return arrayList[i];
	}

	object ak.a(int i)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetOracleValue
		return this.GetOracleValue(i);
	}
}
