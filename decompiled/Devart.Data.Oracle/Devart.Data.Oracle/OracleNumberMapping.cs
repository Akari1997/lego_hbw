using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Devart.Common;

namespace Devart.Data.Oracle;

[Obfuscation]
public class OracleNumberMapping : ICloneable
{
	private OracleNumberType m_a;

	private int m_b;

	private int c;

	private Type d;

	private PropertyChangedEventHandler e;

	[Category("General")]
	[DefaultValue("")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Devart.Common.i("OracleNumberMapping_NumberType")]
	public OracleNumberType NumberType
	{
		get
		{
			return this.m_a;
		}
		set
		{
			if (this.m_a != value)
			{
				this.m_a = value;
				a("NumberType");
			}
		}
	}

	[RefreshProperties(RefreshProperties.Repaint)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Devart.Common.i("OracleNumberMapping_FromPrecision")]
	[Category("General")]
	[DefaultValue("")]
	public int FromPrecision
	{
		get
		{
			return this.m_b;
		}
		set
		{
			if (this.m_b != value)
			{
				this.m_b = value;
				a("FromPrecision");
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Devart.Common.i("OracleNumberMapping_ToPrecision")]
	[Category("General")]
	[DefaultValue("")]
	public int ToPrecision
	{
		get
		{
			return c;
		}
		set
		{
			if (c != value)
			{
				c = value;
				a("ToPrecision");
			}
		}
	}

	[Devart.Common.i("OracleNumberMapping_ValueType")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue("")]
	[Editor("Devart.Data.Oracle.Design.OracleNumberMappingValueTypeEditor, Devart.Data.Oracle.Design, Version=8.1.36.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Category("General")]
	public Type ValueType
	{
		get
		{
			return d;
		}
		set
		{
			if ((object)d != value)
			{
				d = value;
				a("ValueType");
			}
		}
	}

	internal event PropertyChangedEventHandler PropertyChanged
	{
		add
		{
			PropertyChangedEventHandler propertyChangedEventHandler = e;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
				propertyChangedEventHandler = Interlocked.CompareExchange(ref e, value2, propertyChangedEventHandler2);
			}
			while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
		}
		remove
		{
			PropertyChangedEventHandler propertyChangedEventHandler = e;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
				propertyChangedEventHandler = Interlocked.CompareExchange(ref e, value2, propertyChangedEventHandler2);
			}
			while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
		}
	}

	public OracleNumberMapping()
	{
		this.m_a = OracleNumberType.Number;
		this.m_b = 0;
		c = 0;
		d = typeof(decimal);
	}

	public OracleNumberMapping(OracleNumberType numberType, int precision, Type valueType)
		: this(numberType, precision, precision, valueType)
	{
	}

	public OracleNumberMapping(OracleNumberType numberType, int fromPrecision, int toPrecision, Type valueType)
	{
		this.m_a = numberType;
		this.m_b = fromPrecision;
		c = toPrecision;
		d = valueType;
	}

	public object Clone()
	{
		return new OracleNumberMapping(this.m_a, this.m_b, c, d);
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as OracleNumberMapping);
	}

	public bool Equals(OracleNumberMapping other)
	{
		if (other == null)
		{
			return false;
		}
		if (NumberType == other.NumberType && FromPrecision == other.FromPrecision && ToPrecision == other.ToPrecision)
		{
			return (object)ValueType == other.ValueType;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return this.m_a.GetHashCode() | this.m_b.GetHashCode() | c.GetHashCode() | d.GetHashCode();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(32);
		switch (NumberType)
		{
		case OracleNumberType.Float:
			stringBuilder.Append("FLOAT");
			break;
		case OracleNumberType.Integer:
		case OracleNumberType.Number:
			stringBuilder.Append("NUMBER");
			break;
		default:
			throw new InvalidOperationException();
		}
		switch (NumberType)
		{
		case OracleNumberType.Integer:
		case OracleNumberType.Float:
			if (FromPrecision == ToPrecision)
			{
				if (FromPrecision != 0 || NumberType != OracleNumberType.Integer)
				{
					stringBuilder.AppendFormat(null, "({0})", new object[1] { FromPrecision });
				}
			}
			else
			{
				stringBuilder.AppendFormat(null, "({0}-{1})", new object[2] { FromPrecision, ToPrecision });
			}
			break;
		case OracleNumberType.Number:
			if (FromPrecision == ToPrecision)
			{
				stringBuilder.AppendFormat(null, "({0},x)", new object[1] { FromPrecision });
			}
			else
			{
				stringBuilder.AppendFormat(null, "({0},x-{1},x)", new object[2] { FromPrecision, ToPrecision });
			}
			break;
		default:
			throw new InvalidOperationException();
		}
		stringBuilder.Append(" to ");
		stringBuilder.Append(ValueType.Name);
		return stringBuilder.ToString();
	}

	internal string a()
	{
		return string.Format("{0},{1},{2},{3}", new object[4]
		{
			NumberType.ToString().ToUpper(CultureInfo.InvariantCulture),
			FromPrecision,
			ToPrecision,
			ValueType.FullName
		});
	}

	internal static OracleNumberMapping b(string A_0)
	{
		if (a(A_0, out var A_1, out var A_2))
		{
			return A_1;
		}
		throw new FormatException(A_2);
	}

	internal static bool a(string A_0, out OracleNumberMapping A_1, out string A_2)
	{
		A_1 = null;
		A_2 = $"String '{A_0}' was not recognized as a valid OracleNumberMapping.";
		A_0 = A_0.Trim();
		Match match = Regex.Match(A_0, "[\\(]?\\s*(\\w+)\\s*,\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*,\\s*([A-Za-z0-9.]+)\\s*[\\)]?", RegexOptions.CultureInvariant);
		if (match.Success)
		{
			if (match.Groups.Count != 5)
			{
				return false;
			}
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			string value3 = match.Groups[3].Value;
			string value4 = match.Groups[4].Value;
			if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value2) || string.IsNullOrEmpty(value3) || string.IsNullOrEmpty(value4))
			{
				return false;
			}
			OracleNumberType oracleNumberType = OracleNumberType.Number;
			switch (value.Trim().ToLower(CultureInfo.InvariantCulture))
			{
			case "integer":
			case "number":
			case "float":
			{
				oracleNumberType = (OracleNumberType)Enum.Parse(typeof(OracleNumberType), value, ignoreCase: true);
				if (!int.TryParse(value2, out var result))
				{
					A_2 += $" Value '{value2}' is not a valid precision.";
					return false;
				}
				if (!int.TryParse(value3, out var result2))
				{
					A_2 += $" Value '{value3}' is not a valid precision.";
					return false;
				}
				string text = value4;
				if (text.IndexOf(".") < 0)
				{
					text = "System." + text;
				}
				switch (text.ToLower(CultureInfo.InvariantCulture))
				{
				default:
					A_2 += $" There is an unknown value type '{value4}'. Value type can be only 'System.SByte', 'System.Byte', 'System.Int16', 'System.Int32', 'System.Int64', 'System.UInt16', 'System.UInt32', 'System.UInt64', 'System.Single', 'System.Double', 'System.Decimal', or 'System.Boolean'.";
					return false;
				case "system.sbyte":
				case "system.byte":
				case "system.int16":
				case "system.int32":
				case "system.int64":
				case "system.uint16":
				case "system.uint32":
				case "system.uint64":
				case "system.single":
				case "system.double":
				case "system.decimal":
				case "system.boolean":
				{
					Type type = Type.GetType(text, throwOnError: false, ignoreCase: true);
					if ((object)type == null)
					{
						A_2 += $" There is an unknown value type '{value4}'. Value type can be only 'System.SByte', 'System.Byte', 'System.Int16', 'System.Int32', 'System.Int64', 'System.UInt16', 'System.UInt32', 'System.UInt64', 'System.Single', 'System.Double', 'System.Decimal', or 'System.Boolean'.";
						return false;
					}
					A_1 = new OracleNumberMapping(oracleNumberType, result, result2, type);
					return true;
				}
				}
			}
			default:
				A_2 += $" There is an unknown OracleNumberType '{value}'. OracleNumberType can be only 'INTEGER', 'NUMBER', or 'FLOAT'.";
				return false;
			}
		}
		return false;
	}

	private void a(string A_0)
	{
		if (e != null)
		{
			e(this, new PropertyChangedEventArgs(A_0));
		}
	}
}
