using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using Devart.Common;

namespace Devart.Data.Oracle;

[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner", "System.Drawing.Design.UITypeEditor, System.Drawing")]
[ListBindable(false)]
public class OracleParameterCollection : DbParameterBaseCollection, IList
{
	private OracleCommand m_a;

	public new OracleParameter this[int index]
	{
		get
		{
			return (OracleParameter)GetParameter(index);
		}
		set
		{
			SetParameter(index, value);
		}
	}

	public new OracleParameter this[string parameterName]
	{
		get
		{
			return (OracleParameter)GetParameter(parameterName);
		}
		set
		{
			SetParameter(parameterName, value);
		}
	}

	protected override Type ItemType => typeof(OracleParameter);

	protected override DbCommandBase Parent => this.m_a;

	public OracleParameterCollection()
	{
	}

	internal OracleParameterCollection(OracleCommand A_0)
	{
		this.m_a = A_0;
	}

	public OracleParameter Add(string parameterName, object value)
	{
		return Add(new OracleParameter(parameterName, value));
	}

	public OracleParameter Add(string parameterName, OracleDbType type)
	{
		return Add(new OracleParameter(parameterName, type));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, int size)
	{
		return Add(new OracleParameter(parameterName, dbType, size));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, ParameterDirection direction)
	{
		return Add(new OracleParameter(parameterName, dbType, direction));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, string objectTypeName)
	{
		return Add(new OracleParameter(parameterName, dbType, objectTypeName));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, int size, string sourceColumn)
	{
		return Add(new OracleParameter(parameterName, dbType, size, sourceColumn));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, object value, ParameterDirection direction)
	{
		return Add(new OracleParameter(parameterName, dbType, value, direction));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, int size, object value, ParameterDirection direction)
	{
		return Add(new OracleParameter(parameterName, dbType, size, value, direction));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, int size, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
	{
		return Add(new OracleParameter(parameterName, dbType, size, direction, sourceColumn, sourceVersion, sourceColumnNullMapping, value));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
	{
		return Add(new OracleParameter(parameterName, dbType, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value, int arrayLength, string objectTypeName)
	{
		return Add(new OracleParameter(parameterName, dbType, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value, arrayLength, objectTypeName));
	}

	public OracleParameter Add(string parameterName, OracleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, int arrayLength, string objectTypeName)
	{
		return Add(new OracleParameter(parameterName, dbType, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, sourceColumnNullMapping, value, arrayLength, objectTypeName));
	}

	public OracleParameter Add(OracleParameter value)
	{
		Add((object)value);
		return value;
	}

	protected override void OnChange()
	{
		base.OnChange();
		if (Parent != null)
		{
			this.m_a.g();
		}
	}

	protected override void ValidateType(object value)
	{
		if (value == null)
		{
			throw new ArgumentException(string.Format(Devart.Common.al.a("ParameterNull"), typeof(OracleParameter).ToString()));
		}
		if (!(value is OracleParameter))
		{
			throw new ArgumentException(string.Format(Devart.Common.al.a("InvalidParameterType"), typeof(OracleParameter).ToString()));
		}
	}

	protected internal void SetParent(OracleCommand oracleCommand)
	{
		this.m_a = oracleCommand;
	}

	internal OracleCommand a()
	{
		return this.m_a;
	}

	public OracleParameter AddWithValue(string parameterName, object value)
	{
		return Add(new OracleParameter(parameterName, value));
	}
}
