using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(bb))]
public struct OracleString(string s) : IComparable, INullable, IXmlSerializable
{
	private string m_a = s;

	public static readonly OracleString Empty = new OracleString(string.Empty);

	public static readonly OracleString Null = default(OracleString);

	public bool IsNull => this.m_a == null;

	public int Length
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return this.m_a.Length;
		}
	}

	public string Value => this.m_a;

	public char this[int index]
	{
		get
		{
			if (IsNull)
			{
				throw new InvalidOperationException();
			}
			return this.m_a[index];
		}
	}

	public int CompareTo(object obj)
	{
		if (obj == null || obj == DBNull.Value)
		{
			if (!IsNull)
			{
				return 1;
			}
			return 0;
		}
		OracleString oracleString = ((!(obj is OracleString)) ? ((OracleString)obj.ToString()) : ((OracleString)obj));
		if (IsNull)
		{
			if (!oracleString.IsNull)
			{
				return -1;
			}
			return 0;
		}
		if (oracleString.IsNull)
		{
			return 1;
		}
		return CultureInfo.CurrentCulture.CompareInfo.Compare(this.m_a, oracleString.m_a);
	}

	public static OracleString Concat(OracleString value1, OracleString value2)
	{
		if (value1.IsNull)
		{
			return value2;
		}
		if (value2.IsNull)
		{
			return value1;
		}
		return new OracleString(value1.m_a + value2.m_a);
	}

	public override bool Equals(object value)
	{
		return CompareTo(value) == 0;
	}

	public static bool Equals(OracleString value1, OracleString value2)
	{
		return value1.CompareTo(value2) == 0;
	}

	public static bool NotEquals(OracleString value1, OracleString value2)
	{
		return value1.CompareTo(value2) != 0;
	}

	public static bool GreaterThan(OracleString value1, OracleString value2)
	{
		return value1.CompareTo(value2) == 1;
	}

	public static bool GreaterThanOrEqual(OracleString value1, OracleString value2)
	{
		return value1.CompareTo(value2) >= 0;
	}

	public static bool LessThan(OracleString value1, OracleString value2)
	{
		return value1.CompareTo(value2) == -1;
	}

	public static bool LessThanOrEqual(OracleString value1, OracleString value2)
	{
		return value1.CompareTo(value2) <= 0;
	}

	public override int GetHashCode()
	{
		if (this.m_a == null)
		{
			return DBNull.Value.GetHashCode();
		}
		return this.m_a.GetHashCode();
	}

	public override string ToString()
	{
		return this.m_a;
	}

	public static OracleString operator +(OracleString value1, OracleString value2)
	{
		return Concat(value1, value2);
	}

	public static bool operator ==(OracleString value1, OracleString value2)
	{
		return Equals(value1, value2);
	}

	public static bool operator >(OracleString value1, OracleString value2)
	{
		return GreaterThan(value1, value2);
	}

	public static bool operator >=(OracleString value1, OracleString value2)
	{
		return GreaterThanOrEqual(value1, value2);
	}

	public static bool operator !=(OracleString value1, OracleString value2)
	{
		return NotEquals(value1, value2);
	}

	public static bool operator <(OracleString value1, OracleString value2)
	{
		return LessThan(value1, value2);
	}

	public static bool operator <=(OracleString value1, OracleString value2)
	{
		return LessThanOrEqual(value1, value2);
	}

	public static explicit operator string(OracleString value)
	{
		return value.ToString();
	}

	public static implicit operator OracleString(string value)
	{
		return new OracleString(value);
	}

	internal IntPtr a()
	{
		return IntPtr.Zero;
	}

	XmlSchema IXmlSerializable.GetSchema()
	{
		return null;
	}

	void IXmlSerializable.ReadXml(XmlReader reader)
	{
		if (OracleUtils.a(reader))
		{
			this.m_a = null;
		}
		else
		{
			this.m_a = reader.ReadElementString();
		}
	}

	void IXmlSerializable.WriteXml(XmlWriter writer)
	{
		if (IsNull)
		{
			OracleUtils.a(writer);
		}
		else
		{
			writer.WriteString(this.m_a);
		}
	}
}
