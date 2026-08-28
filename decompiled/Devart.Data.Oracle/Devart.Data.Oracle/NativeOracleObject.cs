using System;

namespace Devart.Data.Oracle;

public class NativeOracleObject : NativeOracleObjectBase, bf
{
	internal new bf Instance => (bf)base.Instance;

	public object this[OracleAttribute attribute]
	{
		get
		{
			return ConvertFromInternalValue(Instance.a(attribute), attribute);
		}
		set
		{
			Instance.a(attribute, GetInternalValue(value, attribute.DbType));
		}
	}

	public NativeOracleObject(string typeName, OracleConnection connection)
		: base(typeName, connection)
	{
	}

	public NativeOracleObject(OracleType objectType, OracleConnection connection)
		: base(objectType, connection)
	{
	}

	internal NativeOracleObject(bf A_0, OracleConnection A_1, bool A_2)
		: base(A_0, A_1, A_2)
	{
	}

	public object GetOracleValue(OracleAttribute attribute)
	{
		return ConvertFromInternalValue(Instance.b(attribute), attribute);
	}

	object bf.b(OracleAttribute attribute)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetOracleValue
		return this.GetOracleValue(attribute);
	}

	public object GetOracleValue(string attributeName)
	{
		return GetOracleValue(base.ObjectType.Attributes[attributeName]);
	}

	public void SetAttributeNotNull(OracleAttribute attribute)
	{
		Instance.c(attribute);
	}

	void bf.c(OracleAttribute attribute)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetAttributeNotNull
		this.SetAttributeNotNull(attribute);
	}

	protected override object GetItemValue(int i)
	{
		throw new InvalidOperationException();
	}

	protected override void SetItemValue(object value, int i)
	{
		throw new InvalidOperationException();
	}

	protected override object GetItemValue(string name)
	{
		return this[base.ObjectType.Attributes[name]];
	}

	protected override void SetItemValue(object value, string name)
	{
		this[base.ObjectType.Attributes[name]] = value;
	}
}
