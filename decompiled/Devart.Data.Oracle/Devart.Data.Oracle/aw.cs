using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Globalization;
using System.Reflection;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class aw : ExpandableObjectConverter
{
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if ((object)destinationType == typeof(InstanceDescriptor))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if ((object)destinationType == null)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidDestinationType"));
		}
		if ((object)destinationType == typeof(InstanceDescriptor) && value is OracleParameter)
		{
			OracleParameter oracleParameter = (OracleParameter)value;
			object obj = oracleParameter.Value;
			OracleDbType oracleDbType = oracleParameter.OracleDbType;
			if (oracleDbType == OracleDbType.BFile || oracleDbType == OracleDbType.Blob || oracleDbType == OracleDbType.LongRaw || oracleDbType == OracleDbType.Raw || oracleDbType == OracleDbType.Cursor)
			{
				obj = null;
			}
			if (oracleParameter.SourceColumnNullMapping)
			{
				Type[] types = new Type[13]
				{
					typeof(string),
					typeof(OracleDbType),
					typeof(int),
					typeof(ParameterDirection),
					typeof(bool),
					typeof(byte),
					typeof(byte),
					typeof(string),
					typeof(DataRowVersion),
					typeof(bool),
					typeof(object),
					typeof(int),
					typeof(string)
				};
				ConstructorInfo constructor = typeof(OracleParameter).GetConstructor(types);
				if ((object)constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[13]
					{
						oracleParameter.ParameterName,
						oracleParameter.OracleDbType,
						oracleParameter.Size,
						oracleParameter.Direction,
						oracleParameter.IsNullable,
						(byte)0,
						(byte)0,
						oracleParameter.SourceColumn,
						oracleParameter.SourceVersion,
						oracleParameter.SourceColumnNullMapping,
						obj,
						oracleParameter.ArrayLength,
						oracleParameter.ObjectTypeName
					});
				}
			}
			else if (oracleParameter.ArrayLength != 0 || !Utils.IsEmpty(oracleParameter.ObjectTypeName))
			{
				Type[] types = new Type[12]
				{
					typeof(string),
					typeof(OracleDbType),
					typeof(int),
					typeof(ParameterDirection),
					typeof(bool),
					typeof(byte),
					typeof(byte),
					typeof(string),
					typeof(DataRowVersion),
					typeof(object),
					typeof(int),
					typeof(string)
				};
				ConstructorInfo constructor = typeof(OracleParameter).GetConstructor(types);
				if ((object)constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[12]
					{
						oracleParameter.ParameterName,
						oracleParameter.OracleDbType,
						oracleParameter.Size,
						oracleParameter.Direction,
						oracleParameter.IsNullable,
						(byte)0,
						(byte)0,
						oracleParameter.SourceColumn,
						oracleParameter.SourceVersion,
						obj,
						oracleParameter.ArrayLength,
						oracleParameter.ObjectTypeName
					});
				}
			}
			else if (oracleParameter.SourceVersion != DataRowVersion.Current || oracleParameter.Scale != 0 || oracleParameter.Precision != 0 || !oracleParameter.IsNullable || oracleParameter.Direction != ParameterDirection.Input || (obj != null && oracleParameter.OracleDbType != OracleDbType.VarChar) || (oracleParameter.SourceColumn != string.Empty && obj != null) || (oracleParameter.Size != 0 && obj != null))
			{
				Type[] types = new Type[10]
				{
					typeof(string),
					typeof(OracleDbType),
					typeof(int),
					typeof(ParameterDirection),
					typeof(bool),
					typeof(byte),
					typeof(byte),
					typeof(string),
					typeof(DataRowVersion),
					typeof(object)
				};
				ConstructorInfo constructor = typeof(OracleParameter).GetConstructor(types);
				if ((object)constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[10]
					{
						oracleParameter.ParameterName,
						oracleParameter.OracleDbType,
						oracleParameter.Size,
						oracleParameter.Direction,
						oracleParameter.IsNullable,
						(byte)0,
						(byte)0,
						oracleParameter.SourceColumn,
						oracleParameter.SourceVersion,
						obj
					});
				}
			}
			else if (oracleParameter.SourceColumn != string.Empty)
			{
				Type[] types = new Type[4]
				{
					typeof(string),
					typeof(OracleDbType),
					typeof(int),
					typeof(string)
				};
				ConstructorInfo constructor = typeof(OracleParameter).GetConstructor(types);
				if ((object)constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[4] { oracleParameter.ParameterName, oracleParameter.OracleDbType, oracleParameter.Size, oracleParameter.SourceColumn });
				}
			}
			else if (oracleParameter.Size != 0)
			{
				Type[] types = new Type[3]
				{
					typeof(string),
					typeof(OracleDbType),
					typeof(int)
				};
				ConstructorInfo constructor = typeof(OracleParameter).GetConstructor(types);
				if ((object)constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[3] { oracleParameter.ParameterName, oracleParameter.OracleDbType, oracleParameter.Size });
				}
			}
			else if (oracleParameter.OracleDbType != OracleDbType.VarChar)
			{
				Type[] types = new Type[2]
				{
					typeof(string),
					typeof(OracleDbType)
				};
				ConstructorInfo constructor = typeof(OracleParameter).GetConstructor(types);
				if ((object)constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[2] { oracleParameter.ParameterName, oracleParameter.OracleDbType });
				}
			}
			else
			{
				Type[] types = new Type[2]
				{
					typeof(string),
					typeof(object)
				};
				ConstructorInfo constructor = typeof(OracleParameter).GetConstructor(types);
				if ((object)constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[2] { oracleParameter.ParameterName, obj });
				}
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
