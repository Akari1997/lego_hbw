using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Devart.Data.Oracle;

[Serializable]
public class OracleException : DbException, ISerializable
{
	private int m_a;

	private int b;

	private OracleErrorCollection c;

	private bool d;

	public int Code => this.m_a;

	public int Offset => b;

	public OracleErrorCollection Errors => c;

	public bool IsRecoverable
	{
		get
		{
			if (c != null && c.Count > 0)
			{
				return c[0].IsRecoverable;
			}
			return d;
		}
		internal set
		{
			d = value;
		}
	}

	internal OracleException(int A_0, string A_1)
		: this(A_0, A_1, A_2: false)
	{
	}

	internal OracleException(int A_0, string A_1, bool A_2)
		: base(A_1)
	{
		this.m_a = A_0;
		d = A_2;
	}

	internal OracleException(int A_0, string A_1, Exception A_2)
		: base(A_1, A_2)
	{
		this.m_a = A_0;
	}

	protected OracleException(SerializationInfo si, StreamingContext context)
		: base(si, context)
	{
		this.m_a = si.GetInt32("code");
	}

	internal void a(int A_0)
	{
		b = A_0;
	}

	internal void a(OracleErrorCollection A_0)
	{
		c = A_0;
	}

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("code", this.m_a);
	}
}
