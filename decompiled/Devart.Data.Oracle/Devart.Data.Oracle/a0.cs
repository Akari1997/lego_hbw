using System;
using System.ComponentModel;
using System.Globalization;

namespace Devart.Data.Oracle;

internal class a0 : a
{
	public a0()
	{
		base.a = OracleDbType.Date;
	}

	public override bool b(ITypeDescriptorContext A_0, Type A_1)
	{
		if ((object)A_1 == typeof(DateTime))
		{
			return true;
		}
		return base.b(A_0, A_1);
	}

	public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2, Type A_3)
	{
		if ((object)A_3 == typeof(DateTime) && A_2 != null)
		{
			return ((OracleDate)A_2).Value;
		}
		return base.a(A_0, A_1, A_2, A_3);
	}

	public override bool a(ITypeDescriptorContext A_0, Type A_1)
	{
		if ((object)A_1 == typeof(DateTime))
		{
			return true;
		}
		return base.b(A_0, A_1);
	}

	public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2)
	{
		if ((object)A_2.GetType() == typeof(DateTime))
		{
			return new OracleDate((DateTime)A_2);
		}
		return base.a(A_0, A_1, A_2);
	}
}
