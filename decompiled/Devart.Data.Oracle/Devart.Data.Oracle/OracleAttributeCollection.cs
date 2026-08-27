using System;
using System.Collections;
using System.Globalization;

namespace Devart.Data.Oracle;

public sealed class OracleAttributeCollection : CollectionBase
{
	public OracleAttribute this[string name]
	{
		get
		{
			OracleAttribute oracleAttribute = a(name);
			if (oracleAttribute == null)
			{
				oracleAttribute = a(name.ToUpper(CultureInfo.InvariantCulture));
			}
			if (oracleAttribute == null)
			{
				throw new InvalidOperationException();
			}
			return oracleAttribute;
		}
	}

	public OracleAttribute this[int i] => (OracleAttribute)base.InnerList[i];

	internal OracleAttributeCollection()
	{
	}

	internal OracleAttribute a(string A_0)
	{
		foreach (OracleAttribute inner in base.InnerList)
		{
			if (inner.Name == A_0)
			{
				return inner;
			}
		}
		return null;
	}

	internal void a(OracleAttribute A_0)
	{
		base.InnerList.Add(A_0);
	}
}
