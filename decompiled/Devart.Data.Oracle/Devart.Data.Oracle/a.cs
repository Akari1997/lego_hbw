using System;
using System.ComponentModel;
using System.Globalization;

namespace Devart.Data.Oracle;

internal class a : TypeConverter
{
	protected OracleDbType a;

	public virtual bool b(ITypeDescriptorContext A_0, Type A_1)
	{
		if ((object)A_1 == typeof(string))
		{
			return true;
		}
		return base.CanConvertTo(A_0, A_1);
	}

	public virtual object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2, Type A_3)
	{
		if ((object)A_3 == typeof(string) && A_2 != null)
		{
			if (A_2 is IFormattable formattable)
			{
				return formattable.ToString("", A_1);
			}
			return A_2.ToString();
		}
		return base.ConvertTo(A_0, A_1, A_2, A_3);
	}

	public virtual bool a(ITypeDescriptorContext A_0, Type A_1)
	{
		if ((object)A_1 == typeof(string))
		{
			return true;
		}
		return base.CanConvertTo(A_0, A_1);
	}

	public virtual object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2)
	{
		string text = A_2 as string;
		if (A_2 != null)
		{
			switch (this.a)
			{
			case OracleDbType.Char:
			case OracleDbType.VarChar:
				return new OracleString(text);
			case OracleDbType.Number:
				return OracleNumber.Parse(text);
			case OracleDbType.TimeStamp:
			case OracleDbType.TimeStampLTZ:
			case OracleDbType.TimeStampTZ:
			{
				if (OracleUtils.OracleClientCompatible)
				{
					return OracleDateTime.Parse(text);
				}
				OracleDbType[] array = new OracleDbType[3]
				{
					OracleDbType.TimeStampTZ,
					OracleDbType.TimeStamp,
					OracleDbType.TimeStampLTZ
				};
				for (int j = 0; j < 3; j++)
				{
					OracleDbType timeStampType = array[j];
					try
					{
						return OracleTimeStamp.Parse(text, timeStampType);
					}
					catch
					{
					}
				}
				return OracleTimeStamp.Parse(text, OracleDbType.TimeStampTZ);
			}
			case OracleDbType.IntervalDS:
				if (OracleUtils.OracleClientCompatible)
				{
					return OracleTimeSpan.Parse(text);
				}
				return OracleIntervalDS.Parse(text);
			case OracleDbType.IntervalYM:
				if (OracleUtils.OracleClientCompatible)
				{
					return OracleMonthSpan.Parse(text);
				}
				return OracleIntervalYM.Parse(text);
			case OracleDbType.Date:
				return OracleDate.Parse(text);
			case OracleDbType.Raw:
				return OracleBinary.Parse(text);
			case OracleDbType.Ref:
				return new OracleRef(text);
			case OracleDbType.Xml:
				return new OracleXml(text);
			}
		}
		return base.ConvertFrom(A_0, A_1, A_2);
	}
}
