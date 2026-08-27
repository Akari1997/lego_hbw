using System;
using System.ComponentModel;
using System.Globalization;

namespace Devart.Data.Oracle;

internal class i : a
{
	public i()
	{
		base.a = OracleDbType.Raw;
	}

	public override bool b(ITypeDescriptorContext A_0, Type A_1)
	{
		if ((object)A_1 == typeof(byte[]))
		{
			return true;
		}
		return base.b(A_0, A_1);
	}

	public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2, Type A_3)
	{
		if ((object)A_3 == typeof(byte[]) && A_2 != null)
		{
			return ((OracleBinary)A_2).Value;
		}
		return base.a(A_0, A_1, A_2, A_3);
	}

	public override bool a(ITypeDescriptorContext A_0, Type A_1)
	{
		if ((object)A_1 == typeof(byte[]))
		{
			return true;
		}
		return base.b(A_0, A_1);
	}

	public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2)
	{
		if (A_2 is string str)
		{
			return OracleBinary.Parse(str);
		}
		if (A_2 is byte[] value)
		{
			return new OracleBinary(value);
		}
		return base.a(A_0, A_1, A_2);
	}
}
