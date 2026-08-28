using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(i))]
public struct OracleBinary : IComparable, INullable, IXmlSerializable
{
	private bool m_a;

	private byte[] b;

	public static readonly OracleBinary Null = new OracleBinary(A_0: true);

	public bool IsNull => this.m_a;

	public byte this[int index]
	{
		get
		{
			if (this.m_a)
			{
				return 0;
			}
			return b[index];
		}
	}

	public int Length
	{
		get
		{
			if (this.m_a)
			{
				return 0;
			}
			return b.Length;
		}
	}

	public byte[] Value
	{
		get
		{
			if (this.m_a)
			{
				return null;
			}
			return b;
		}
	}

	public OracleBinary(byte[] value)
		: this(A_0: false)
	{
		if (value == null)
		{
			throw new ArgumentNullException();
		}
		b = (byte[])value.Clone();
	}

	private OracleBinary(bool A_0)
	{
		b = null;
		this.m_a = A_0;
	}

	public int CompareTo(object obj)
	{
		byte[] array;
		bool flag;
		int num;
		if (obj is byte[])
		{
			array = (byte[])obj;
			flag = array == null;
			num = ((!flag) ? array.Length : 0);
		}
		else
		{
			OracleBinary oracleBinary = (OracleBinary)obj;
			flag = oracleBinary.IsNull;
			array = oracleBinary.b;
			num = ((!flag) ? oracleBinary.Length : 0);
		}
		if (IsNull)
		{
			if (!flag)
			{
				return -1;
			}
			return 0;
		}
		if (flag)
		{
			return 1;
		}
		byte[] array2 = b;
		int num2 = b.Length;
		int num3 = num;
		bool flag2 = num2 < num3;
		int num4 = ((!flag2) ? num3 : num2);
		for (int j = 0; j < num4; j++)
		{
			if (array2[j] != array[j])
			{
				if (array2[j] < array[j])
				{
					return -1;
				}
				return 1;
			}
		}
		if (num2 == num3)
		{
			return 0;
		}
		byte b2 = 0;
		if (flag2)
		{
			for (int j = num4; j < num3; j++)
			{
				if (array[j] != b2)
				{
					return -1;
				}
			}
		}
		else
		{
			for (int j = num4; j < num2; j++)
			{
				if (array2[j] != b2)
				{
					return 1;
				}
			}
		}
		return 0;
	}

	public static OracleBinary Concat(OracleBinary value1, OracleBinary value2)
	{
		if (value1.IsNull || value2.IsNull)
		{
			return Null;
		}
		byte[] array = new byte[value1.Length + value2.Length];
		Buffer.BlockCopy(value1.b, 0, array, 0, value1.Length);
		Buffer.BlockCopy(value2.b, 0, array, value1.Length, value2.Length);
		return array;
	}

	public override int GetHashCode()
	{
		if (this.m_a)
		{
			return 0;
		}
		return b.GetHashCode();
	}

	public override bool Equals(object value)
	{
		if (value == null || value == DBNull.Value)
		{
			return IsNull;
		}
		if ((object)value.GetType() != typeof(OracleBinary))
		{
			return false;
		}
		return CompareTo(value) == 0;
	}

	public static bool Equals(OracleBinary value1, OracleBinary value2)
	{
		return value1.CompareTo(value2) == 0;
	}

	public static bool NotEquals(OracleBinary value1, OracleBinary value2)
	{
		return value1.CompareTo(value2) != 0;
	}

	public static bool GreaterThan(OracleBinary value1, OracleBinary value2)
	{
		return value1.CompareTo(value2) == 1;
	}

	public static bool GreaterThanOrEqual(OracleBinary value1, OracleBinary value2)
	{
		return value1.CompareTo(value2) >= 0;
	}

	public static bool LessThan(OracleBinary value1, OracleBinary value2)
	{
		return value1.CompareTo(value2) == -1;
	}

	public static bool LessThanOrEqual(OracleBinary value1, OracleBinary value2)
	{
		return value1.CompareTo(value2) <= 0;
	}

	public static OracleBinary operator +(OracleBinary value1, OracleBinary value2)
	{
		return Concat(value1, value2);
	}

	public static bool operator ==(OracleBinary value1, OracleBinary value2)
	{
		return Equals(value1, value2);
	}

	public static bool operator >(OracleBinary value1, OracleBinary value2)
	{
		return GreaterThan(value1, value2);
	}

	public static bool operator >=(OracleBinary value1, OracleBinary value2)
	{
		return GreaterThanOrEqual(value1, value2);
	}

	public static bool operator !=(OracleBinary value1, OracleBinary value2)
	{
		return NotEquals(value1, value2);
	}

	public static bool operator <(OracleBinary value1, OracleBinary value2)
	{
		return LessThan(value1, value2);
	}

	public static bool operator <=(OracleBinary value1, OracleBinary value2)
	{
		return LessThanOrEqual(value1, value2);
	}

	public static explicit operator byte[](OracleBinary val)
	{
		if (val.IsNull)
		{
			return null;
		}
		return val.Value;
	}

	public static implicit operator OracleBinary(byte[] bytes)
	{
		if (bytes == null)
		{
			return Null;
		}
		return new OracleBinary(bytes);
	}

	public override string ToString()
	{
		if (IsNull)
		{
			return null;
		}
		StringBuilder stringBuilder = new StringBuilder();
		int num = b.Length;
		for (int j = 0; j < num; j++)
		{
			stringBuilder.Append($"{b[j]:X2}");
		}
		return stringBuilder.ToString();
	}

	public static OracleBinary Parse(string str)
	{
		OracleBinary result = a(str, out var A_);
		if (A_ != null)
		{
			throw A_;
		}
		return result;
	}

	public static bool TryParse(string str, out OracleBinary value)
	{
		value = a(str, out var A_);
		return A_ == null;
	}

	private static byte a(char A_0, out Exception A_1)
	{
		A_1 = null;
		if (A_0 >= '0' && A_0 <= '9')
		{
			return (byte)(A_0 - 48);
		}
		if (A_0 >= 'A' && A_0 <= 'F')
		{
			return (byte)(A_0 - 65 + 10);
		}
		if (A_0 >= 'a' && A_0 <= 'f')
		{
			return (byte)(A_0 - 97 + 10);
		}
		A_1 = new ArgumentException($"The character '{A_0}' cannot be used in hexadecimal numbers.");
		return 0;
	}

	private static OracleBinary a(string A_0, out Exception A_1)
	{
		A_1 = null;
		OracleBinary result = default(OracleBinary);
		if (A_0 == null || A_0 == string.Empty)
		{
			result.m_a = true;
			return result;
		}
		int num = A_0.Length + 1 >> 1;
		byte[] array = new byte[num];
		int num2 = -(A_0.Length & 1);
		int num3 = 0;
		while (num3 < num)
		{
			byte b2;
			if (num2 >= 0)
			{
				b2 = a(A_0[num2], out A_1);
				if (A_1 != null)
				{
					return null;
				}
			}
			else
			{
				b2 = 0;
			}
			byte b3 = a(A_0[num2 + 1], out A_1);
			if (A_1 != null)
			{
				return null;
			}
			array[num3] = (byte)((b2 << 4) | b3);
			num3++;
			num2 += 2;
		}
		result.b = array;
		return result;
	}

	XmlSchema IXmlSerializable.GetSchema()
	{
		return null;
	}

	void IXmlSerializable.ReadXml(XmlReader reader)
	{
		if (OracleUtils.a(reader))
		{
			b = null;
			return;
		}
		string value = reader.ReadElementString();
		if (!string.IsNullOrEmpty(value))
		{
			b = Convert.FromBase64String(value);
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
			writer.WriteString(Convert.ToBase64String(b));
		}
	}
}
