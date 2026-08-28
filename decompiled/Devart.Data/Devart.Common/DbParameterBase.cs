using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Devart.Common;

public abstract class DbParameterBase : DbParameter
{
	private object m_a;

	private ParameterDirection b;

	private bool c;

	private string d;

	private object e;

	private int m_f;

	private string g;

	private bool h;

	private DataRowVersion i;

	private object j;

	protected object CoercedValue
	{
		get
		{
			return this.m_a;
		}
		set
		{
			this.m_a = value;
		}
	}

	[r("DataParameterdirection")]
	[Category("Data")]
	[RefreshProperties(RefreshProperties.All)]
	public override ParameterDirection Direction
	{
		get
		{
			ParameterDirection parameterDirection = b;
			if (parameterDirection == (ParameterDirection)0)
			{
				return ParameterDirection.Input;
			}
			return parameterDirection;
		}
		set
		{
			if (b != value)
			{
				switch (value)
				{
				case ParameterDirection.Input:
				case ParameterDirection.Output:
				case ParameterDirection.InputOutput:
				case ParameterDirection.ReturnValue:
					PropertyChanging();
					b = value;
					break;
				default:
					throw new InvalidOperationException(n.a("InvalidParameterDirection", value));
				}
			}
		}
	}

	public override bool IsNullable
	{
		get
		{
			return c;
		}
		set
		{
			c = value;
		}
	}

	[Category("DataCategory_Data")]
	[r("DataParameterparameterName")]
	public override string ParameterName
	{
		get
		{
			string text = d;
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value != null && value.Length > 0 && value[0] == ':')
			{
				value = value.Substring(1);
			}
			if (d != value)
			{
				PropertyChanging();
				d = value;
			}
		}
	}

	[r("DbDataParametersize")]
	[Category("DataCategory_Data")]
	public override int Size
	{
		get
		{
			int num = this.m_f;
			if (num == 0)
			{
				num = ValueSize(Value);
			}
			return num;
		}
		set
		{
			if (this.m_f != value)
			{
				if (value < -1)
				{
					throw new InvalidOperationException(n.a("InvalidSizeValue", value));
				}
				PropertyChanging();
				this.m_f = value;
			}
		}
	}

	[r("DataParametersourceColumn")]
	[Category("DataCategory_Update")]
	public override string SourceColumn
	{
		get
		{
			string text = g;
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			g = value;
		}
	}

	public override bool SourceColumnNullMapping
	{
		get
		{
			return h;
		}
		set
		{
			h = value;
		}
	}

	[r("DataParametersourceVersion")]
	[Category("DataCategory_Update")]
	public override DataRowVersion SourceVersion
	{
		get
		{
			DataRowVersion dataRowVersion = i;
			if (dataRowVersion == (DataRowVersion)0)
			{
				return DataRowVersion.Current;
			}
			return dataRowVersion;
		}
		set
		{
			switch (value)
			{
			case DataRowVersion.Original:
			case DataRowVersion.Current:
			case DataRowVersion.Proposed:
			case DataRowVersion.Default:
				i = value;
				break;
			default:
				throw new InvalidOperationException(n.a("InvalidDataRowVersion", value));
			}
		}
	}

	[r("DataParameter_Value")]
	[RefreshProperties(RefreshProperties.All)]
	[Category("DataCategory_Data")]
	[TypeConverter(typeof(StringConverter))]
	public override object Value
	{
		get
		{
			return j;
		}
		set
		{
			if (Utils.MonoDetected && SourceColumnNullMapping && DbType == DbType.Int32)
			{
				value = ((value == null || DBNull.Value == value || (value is INullable && ((INullable)value).IsNull)) ? 1 : 0);
			}
			this.m_a = null;
			j = value;
		}
	}

	protected DbParameterBase()
	{
	}

	protected DbParameterBase(DbParameterBase source)
	{
		Utils.CheckArgumentNull(source, "source");
		source.a(this);
		if (j is ICloneable cloneable)
		{
			j = cloneable.Clone();
		}
	}

	internal object a(object A_0, object A_1)
	{
		object obj = e;
		if (A_1 == obj)
		{
			e = A_0;
		}
		return obj;
	}

	public void CopyTo(DbParameter destination)
	{
		Utils.CheckArgumentNull(destination, "destination");
		a((DbParameterBase)destination);
	}

	private void a(DbParameterBase A_0)
	{
		A_0.d = d;
		A_0.j = j;
		A_0.b = b;
		A_0.m_f = this.m_f;
		A_0.g = g;
		A_0.i = i;
		A_0.h = h;
		A_0.c = c;
	}

	protected virtual void PropertyChanging()
	{
	}

	internal void f()
	{
		e = null;
	}

	protected void ResetSize()
	{
		if (this.m_f != 0)
		{
			PropertyChanging();
			this.m_f = 0;
		}
	}

	protected bool ShouldSerializeSize()
	{
		return this.m_f != 0;
	}

	public override string ToString()
	{
		return ParameterName;
	}

	protected virtual byte ValuePrecision(object value)
	{
		return 0;
	}

	protected virtual byte ValueScale(object value)
	{
		if (value is decimal)
		{
			return (byte)((decimal.GetBits((decimal)value)[3] & 0xFF0000) >> 16);
		}
		return 0;
	}

	protected virtual int ValueSize(object value)
	{
		if (!Utils.IsNull(value))
		{
			if (value is string text)
			{
				return text.Length;
			}
			if (value is byte[] array)
			{
				return array.Length;
			}
			if (value is char[] array2)
			{
				return array2.Length;
			}
			if (value is byte || value is char)
			{
				return 1;
			}
		}
		return 0;
	}
}
