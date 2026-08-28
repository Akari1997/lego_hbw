using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class k : Devart.Common.v
{
	private new readonly Type m_a;

	private new readonly int m_b;

	private new OracleDbType m_c;

	private new readonly bool m_d;

	private new readonly int m_e;

	private new readonly g m_f;

	private new readonly Type m_g;

	private new bool m_h;

	private new OracleType m_i;

	private OracleConnection m_j;

	private static Hashtable m_k;

	private new static DbType a(int A_0)
	{
		switch (A_0)
		{
		case 1:
		case 5:
		case 8:
		case 9:
		case 11:
		case 58:
		case 104:
		case 112:
		case 115:
			return DbType.AnsiString;
		case 96:
			return DbType.AnsiStringFixedLength;
		case 2:
		case 6:
			return DbType.Decimal;
		case 12:
		case 156:
			return DbType.Date;
		case 23:
		case 24:
		case 95:
		case 113:
		case 114:
			return DbType.Binary;
		case 187:
		case 188:
		case 232:
			return DbType.DateTime;
		case 189:
			return DbType.Int32;
		case 190:
			return DbType.Time;
		case 3:
		case 246:
			return DbType.Int32;
		case 4:
		case 22:
		case 101:
			return DbType.Double;
		case 21:
		case 100:
			return DbType.Single;
		case 102:
		case 108:
		case 110:
		case 116:
		case 122:
		case 247:
		case 248:
			return DbType.Object;
		default:
			return DbType.AnsiString;
		}
	}

	protected k(g A_0, int A_1, Type A_2)
		: this(A_0, A_1, 0, A_2, A_0.h().o(), null)
	{
	}

	protected k(g A_0, int A_1, int A_2, Type A_3, Encoding A_4, OracleType A_5)
		: base(a(A_1), A_4)
	{
		this.m_f = A_0;
		this.m_e = A_2;
		this.m_b = A_1;
		this.m_g = A_3;
		this.m_i = A_5;
		switch (A_1)
		{
		case 1:
		case 5:
		case 9:
			if (A_2 == 2)
			{
				this.m_c = OracleDbType.NVarChar;
			}
			else
			{
				this.m_c = OracleDbType.VarChar;
			}
			this.m_d = true;
			break;
		case 96:
			if (A_2 == 2)
			{
				this.m_c = OracleDbType.NChar;
			}
			else
			{
				this.m_c = OracleDbType.Char;
			}
			this.m_d = true;
			break;
		case 2:
		case 6:
			this.m_c = OracleDbType.Number;
			break;
		case 12:
		case 156:
			this.m_c = OracleDbType.Date;
			break;
		case 11:
		case 104:
			this.m_c = OracleDbType.RowId;
			A_1 = 5;
			break;
		case 23:
		case 95:
			this.m_c = OracleDbType.Raw;
			this.m_d = true;
			break;
		case 8:
			this.m_c = OracleDbType.Long;
			break;
		case 24:
			this.m_c = OracleDbType.LongRaw;
			break;
		case 112:
			if (A_2 == 2)
			{
				this.m_c = OracleDbType.NClob;
			}
			else
			{
				this.m_c = OracleDbType.Clob;
			}
			break;
		case 113:
			this.m_c = OracleDbType.Blob;
			break;
		case 114:
			this.m_c = OracleDbType.BFile;
			break;
		case 115:
			this.m_c = OracleDbType.BFile;
			break;
		case 102:
		case 116:
			this.m_c = OracleDbType.Cursor;
			break;
		case 58:
		case 108:
			this.m_c = OracleDbType.Object;
			break;
		case 122:
		case 247:
			this.m_c = OracleDbType.Array;
			break;
		case 248:
			this.m_c = OracleDbType.Table;
			break;
		case 110:
			this.m_c = OracleDbType.Ref;
			break;
		case 187:
			this.m_c = OracleDbType.TimeStamp;
			break;
		case 232:
			this.m_c = OracleDbType.TimeStampLTZ;
			break;
		case 188:
			this.m_c = OracleDbType.TimeStampTZ;
			break;
		case 190:
			this.m_c = OracleDbType.IntervalDS;
			break;
		case 189:
			this.m_c = OracleDbType.IntervalYM;
			break;
		case 3:
		case 246:
			this.m_c = OracleDbType.Integer;
			this.m_d = true;
			break;
		case 4:
			this.m_c = OracleDbType.Double;
			this.m_d = true;
			break;
		case 22:
		case 101:
			this.m_c = OracleDbType.Double;
			this.m_d = true;
			break;
		case 21:
		case 100:
			this.m_c = OracleDbType.Float;
			this.m_d = true;
			break;
		case 252:
			this.m_c = OracleDbType.Boolean;
			break;
		default:
			this.m_c = (OracleDbType)0;
			break;
		}
		if (A_5 != null && this.m_c != OracleDbType.Ref)
		{
			this.m_c = A_5.DbType;
		}
		if ((object)A_3 != null)
		{
			base.a = A_3;
		}
		else
		{
			switch (this.m_c)
			{
			case OracleDbType.Object:
				base.a = typeof(OracleObject);
				break;
			case OracleDbType.Array:
				base.a = typeof(OracleArray);
				break;
			case OracleDbType.Table:
				base.a = typeof(OracleTable);
				break;
			}
		}
		this.m_a = (Type)Devart.Data.Oracle.k.m_k[this.m_c];
	}

	static k()
	{
		Devart.Data.Oracle.k.m_k = new Hashtable();
		Devart.Common.v.h = (byte)CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
		Devart.Data.Oracle.k.m_k[OracleDbType.VarChar] = typeof(OracleString);
		Devart.Data.Oracle.k.m_k[OracleDbType.Char] = typeof(OracleString);
		Devart.Data.Oracle.k.m_k[OracleDbType.NChar] = typeof(OracleString);
		Devart.Data.Oracle.k.m_k[OracleDbType.NVarChar] = typeof(OracleString);
		Devart.Data.Oracle.k.m_k[OracleDbType.Number] = typeof(OracleNumber);
		Devart.Data.Oracle.k.m_k[OracleDbType.Date] = typeof(OracleDate);
		Devart.Data.Oracle.k.m_k[OracleDbType.RowId] = typeof(OracleString);
		Devart.Data.Oracle.k.m_k[OracleDbType.Raw] = typeof(OracleBinary);
		Devart.Data.Oracle.k.m_k[OracleDbType.Long] = typeof(OracleString);
		Devart.Data.Oracle.k.m_k[OracleDbType.LongRaw] = typeof(OracleBinary);
		Devart.Data.Oracle.k.m_k[OracleDbType.Integer] = typeof(OracleNumber);
		Devart.Data.Oracle.k.m_k[OracleDbType.Double] = typeof(OracleNumber);
		Devart.Data.Oracle.k.m_k[OracleDbType.Float] = typeof(OracleNumber);
		if (OracleUtils.OracleClientCompatible)
		{
			Devart.Data.Oracle.k.m_k[OracleDbType.TimeStamp] = typeof(OracleDateTime);
			Devart.Data.Oracle.k.m_k[OracleDbType.TimeStampLTZ] = typeof(OracleDateTime);
			Devart.Data.Oracle.k.m_k[OracleDbType.TimeStampTZ] = typeof(OracleDateTime);
			Devart.Data.Oracle.k.m_k[OracleDbType.IntervalDS] = typeof(OracleTimeSpan);
			Devart.Data.Oracle.k.m_k[OracleDbType.IntervalYM] = typeof(OracleMonthSpan);
		}
		else
		{
			Devart.Data.Oracle.k.m_k[OracleDbType.TimeStamp] = typeof(OracleTimeStamp);
			Devart.Data.Oracle.k.m_k[OracleDbType.TimeStampLTZ] = typeof(OracleTimeStamp);
			Devart.Data.Oracle.k.m_k[OracleDbType.TimeStampTZ] = typeof(OracleTimeStamp);
			Devart.Data.Oracle.k.m_k[OracleDbType.IntervalDS] = typeof(OracleIntervalDS);
			Devart.Data.Oracle.k.m_k[OracleDbType.IntervalYM] = typeof(OracleIntervalYM);
		}
		Devart.Data.Oracle.k.m_k[OracleDbType.AnyData] = typeof(OracleAnyData);
		Devart.Data.Oracle.k.m_k[OracleDbType.Object] = typeof(OracleObject);
		Devart.Data.Oracle.k.m_k[OracleDbType.Array] = typeof(OracleArray);
		Devart.Data.Oracle.k.m_k[OracleDbType.Table] = typeof(OracleTable);
		Devart.Data.Oracle.k.m_k[OracleDbType.Ref] = typeof(OracleRef);
		Devart.Data.Oracle.k.m_k[OracleDbType.Xml] = typeof(OracleXml);
		Devart.Data.Oracle.k.m_k[OracleDbType.BFile] = typeof(OracleBFile);
		Devart.Data.Oracle.k.m_k[OracleDbType.Blob] = typeof(OracleLob);
		Devart.Data.Oracle.k.m_k[OracleDbType.Clob] = typeof(OracleLob);
		Devart.Data.Oracle.k.m_k[OracleDbType.NClob] = typeof(OracleLob);
		Devart.Data.Oracle.k.m_k[OracleDbType.Boolean] = typeof(OracleNumber);
		Devart.Data.Oracle.k.m_k[OracleDbType.Cursor] = typeof(OracleCursor);
	}

	protected new void a(OracleDbType A_0)
	{
		this.m_c = A_0;
	}

	public override long z(byte[] A_0, int A_1, int A_2)
	{
		string text = aq(A_0, A_1, A_2);
		return long.Parse(text, CultureInfo.InvariantCulture);
	}

	public override decimal aa(byte[] A_0, int A_1, int A_2)
	{
		string text = aq(A_0, A_1, A_2);
		return decimal.Parse(text, CultureInfo.InvariantCulture);
	}

	protected override double w(byte[] A_0, int A_1, int A_2)
	{
		string text = aq(A_0, A_1, A_2);
		return double.Parse(text, CultureInfo.InvariantCulture);
	}

	public override float x(byte[] A_0, int A_1, int A_2)
	{
		string text = aq(A_0, A_1, A_2);
		return float.Parse(text, CultureInfo.InvariantCulture);
	}

	public override bool m(byte[] A_0, int A_1, int A_2)
	{
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return OracleNumberUtils.d(A_0, A_1) != 0m;
		}
		return base.m(A_0, A_1, A_2);
	}

	public override byte i(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.i(A_0, A_1, A_2);
		}
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return (byte)OracleNumberUtils.h(A_0, A_1);
		}
		return base.i(A_0, A_1, A_2);
	}

	public override char n(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.n(A_0, A_1, A_2);
		}
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return (char)OracleNumberUtils.h(A_0, A_1);
		}
		return base.n(A_0, A_1, A_2);
	}

	public override DateTime e(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.e(A_0, A_1, A_2);
		}
		switch (this.m_b)
		{
		case 12:
			return OracleDate.a(A_0, A_1);
		case 156:
			return OracleDate.b(A_0, A_1);
		case 187:
		case 188:
		case 232:
			return v(A_0, A_1, A_2).Value;
		default:
			return base.e(A_0, A_1, A_2);
		}
	}

	public override decimal h(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.h(A_0, A_1, A_2);
		}
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return OracleNumberUtils.d(A_0, A_1);
		}
		return base.h(A_0, A_1, A_2);
	}

	public override double d(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.d(A_0, A_1, A_2);
		}
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return OracleNumberUtils.e(A_0, A_1);
		}
		return base.d(A_0, A_1, A_2);
	}

	public override float c(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.c(A_0, A_1, A_2);
		}
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return OracleNumberUtils.f(A_0, A_1);
		}
		return base.c(A_0, A_1, A_2);
	}

	public override short b(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.b(A_0, A_1, A_2);
		}
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return (short)OracleNumberUtils.h(A_0, A_1);
		}
		return base.b(A_0, A_1, A_2);
	}

	public override int a(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.a(A_0, A_1, A_2);
		}
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return OracleNumberUtils.h(A_0, A_1);
		}
		return base.a(A_0, A_1, A_2);
	}

	public override long g(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return base.g(A_0, A_1, A_2);
		}
		int num = this.m_b;
		if (num == 2 || num == 6)
		{
			return OracleNumberUtils.g(A_0, A_1);
		}
		return base.g(A_0, A_1, A_2);
	}

	public override string o(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_b == 96)
		{
			if (OracleUtils.OracleClientCompatible || !f())
			{
				return aq(A_0, A_1, A_2);
			}
			return ab(A_0, A_1, A_2);
		}
		if (this.m_d)
		{
			return base.o(A_0, A_1, A_2);
		}
		switch (this.m_b)
		{
		case 112:
		case 115:
		{
			a3 a10 = a(A_0, A_1, A_2: false, A_3: false);
			return a10.i();
		}
		case 2:
		case 6:
			return OracleNumberUtils.c(A_0, A_1);
		case 12:
			return OracleDate.a(A_0, A_1).ToString();
		case 156:
			return OracleDate.b(A_0, A_1).ToString();
		case 187:
		case 188:
		case 232:
			return v(A_0, A_1, A_2).ToString();
		case 190:
			return u(A_0, A_1, A_2).ToString();
		case 189:
			return r(A_0, A_1, A_2).ToString();
		default:
			return base.o(A_0, A_1, A_2);
		}
	}

	public override Guid q(byte[] A_0, int A_1, int A_2)
	{
		DbType dbType = base.c;
		if (dbType == DbType.Binary)
		{
			byte[] array = ak(A_0, A_1, A_2);
			return new Guid(array);
		}
		return base.q(A_0, A_1, A_2);
	}

	public override TimeSpan f(byte[] A_0, int A_1, int A_2)
	{
		int num = this.m_b;
		if (num == 190)
		{
			return u(A_0, A_1, A_2).Value;
		}
		return base.f(A_0, A_1, A_2);
	}

	public override object p(byte[] A_0, int A_1, int A_2)
	{
		if ((object)this.m_g != null)
		{
			switch (this.m_b)
			{
			case 2:
			case 6:
			{
				int A_4;
				switch (OracleUtils.c(this.m_g))
				{
				case TypeCode.Int16:
					return (short)OracleNumberUtils.h(A_0, A_1);
				case TypeCode.Int32:
					return OracleNumberUtils.h(A_0, A_1);
				case TypeCode.Byte:
					return (byte)OracleNumberUtils.h(A_0, A_1);
				case TypeCode.UInt16:
					return (ushort)OracleNumberUtils.a(A_0, A_1 + 1, (int)A_0[A_1], out A_4);
				case TypeCode.SByte:
					return (sbyte)OracleNumberUtils.h(A_0, A_1);
				case TypeCode.Single:
					return OracleNumberUtils.f(A_0, A_1);
				case TypeCode.Double:
					return OracleNumberUtils.e(A_0, A_1);
				case TypeCode.UInt32:
					return OracleNumberUtils.a(A_0, A_1 + 1, (int)A_0[A_1], out A_4);
				case TypeCode.UInt64:
				{
					long A_3;
					return OracleNumberUtils.a(A_0, A_1 + 1, (int)A_0[A_1], out A_3);
				}
				case TypeCode.Int64:
					return OracleNumberUtils.g(A_0, A_1);
				case TypeCode.Object:
					if ((object)this.m_g == typeof(decimal))
					{
						return OracleNumberUtils.d(A_0, A_1);
					}
					break;
				case TypeCode.Boolean:
					return OracleNumberUtils.a(A_0, A_1);
				}
				break;
			}
			case 3:
			case 4:
			{
				object value = base.p(A_0, A_1, A_2);
				return Convert.ChangeType(value, this.m_g, CultureInfo.InvariantCulture);
			}
			}
		}
		if (this.m_b == 96)
		{
			if (OracleUtils.OracleClientCompatible || !f())
			{
				return aq(A_0, A_1, A_2);
			}
			return ab(A_0, A_1, A_2);
		}
		if (this.m_d)
		{
			if (!f() && this.m_c == OracleDbType.NChar)
			{
				return aq(A_0, A_1, A_2);
			}
			return base.p(A_0, A_1, A_2);
		}
		switch (this.m_b)
		{
		case 2:
		case 6:
			return OracleNumberUtils.d(A_0, A_1);
		case 12:
			return OracleDate.a(A_0, A_1);
		case 156:
			return OracleDate.b(A_0, A_1);
		case 187:
		case 188:
		case 232:
			return v(A_0, A_1, A_2).Value;
		case 190:
			return u(A_0, A_1, A_2).Value;
		case 189:
			return r(A_0, A_1, A_2).Value;
		case 112:
		case 115:
			return o(A_0, A_1, A_2);
		case 113:
		{
			a3 a11 = a(A_0, A_1, A_2: false, A_3: false);
			return a11.s();
		}
		case 114:
		{
			a3 a10 = a(A_0, A_1, A_2: false, A_3: false);
			a10.g();
			try
			{
				return a10.s();
			}
			finally
			{
				a10.n();
			}
		}
		case 58:
		case 108:
		case 122:
		case 247:
		case 248:
			if (k() == OracleDbType.Xml)
			{
				al al2 = a(A_0, A_1, false, (al)null);
				if (al2 != null)
				{
					return al2.a();
				}
			}
			return e(A_0, A_1, A_2: true);
		case 110:
			return b(A_0, A_1, A_2: false);
		default:
			return base.p(A_0, A_1, A_2);
		}
	}

	public new virtual s a(byte[] A_0, int A_1, bool A_2)
	{
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), "OracleCursor"));
	}

	public virtual OracleBinary j(byte[] A_0, int A_1, int A_2)
	{
		try
		{
			return new OracleBinary(base.y(A_0, A_1, A_2));
		}
		catch
		{
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleBinary)));
		}
	}

	public virtual OracleDate s(byte[] A_0, int A_1, int A_2)
	{
		switch (this.m_b)
		{
		case 12:
			return OracleDate.e(A_0, A_1);
		case 156:
			return OracleDate.f(A_0, A_1);
		case 187:
		case 188:
		case 232:
			return v(A_0, A_1, A_2).ToOracleDate();
		default:
			try
			{
				return new OracleDate(e(A_0, A_1, A_2));
			}
			catch
			{
				throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleDate)));
			}
		}
	}

	public virtual OracleTimeStamp v(byte[] A_0, int A_1, int A_2)
	{
		switch (this.m_b)
		{
		case 12:
			return OracleDate.c(A_0, A_1);
		case 156:
			return OracleDate.d(A_0, A_1);
		case 1:
		case 5:
		case 9:
			return OracleTimeStamp.Parse(aq(A_0, A_1, A_2));
		case 96:
			return OracleTimeStamp.Parse(b(A_0, A_1, A_2, base.d));
		default:
			try
			{
				return new OracleTimeStamp(e(A_0, A_1, A_2));
			}
			catch
			{
				throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleTimeStamp)));
			}
		}
	}

	public virtual OracleIntervalDS u(byte[] A_0, int A_1, int A_2)
	{
		switch (this.m_b)
		{
		case 2:
		case 6:
			return new OracleIntervalDS(OracleNumberUtils.e(A_0, A_1));
		case 1:
		case 5:
		case 9:
			return OracleIntervalDS.Parse(aq(A_0, A_1, A_2));
		case 96:
			return OracleIntervalDS.Parse(b(A_0, A_1, A_2, base.d));
		default:
			try
			{
				return new OracleIntervalDS(f(A_0, A_1, A_2));
			}
			catch
			{
				throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleIntervalDS)));
			}
		}
	}

	public virtual OracleIntervalYM r(byte[] A_0, int A_1, int A_2)
	{
		try
		{
			switch (this.m_b)
			{
			case 1:
			case 5:
			case 9:
				return OracleIntervalYM.Parse(aq(A_0, A_1, A_2));
			case 96:
				return OracleIntervalYM.Parse(b(A_0, A_1, A_2, base.d));
			case 3:
			case 246:
				return new OracleIntervalYM(Devart.Common.e.g(A_0, A_1));
			case 2:
			case 6:
				return new OracleIntervalYM(OracleNumberUtils.g(A_0, A_1));
			case 4:
			case 22:
			case 101:
				return new OracleIntervalYM((long)Devart.Common.e.i(A_0, A_1));
			case 21:
			case 100:
				return new OracleIntervalYM((long)Devart.Common.e.e(A_0, A_1));
			default:
				throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleIntervalYM)));
			}
		}
		catch
		{
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleIntervalYM)));
		}
	}

	public new virtual a3 a(byte[] A_0, int A_1, bool A_2, bool A_3)
	{
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), "OracleLob"));
	}

	public virtual OracleNumber l(byte[] A_0, int A_1, int A_2)
	{
		switch (this.m_b)
		{
		case 96:
			return OracleNumber.Parse(b(A_0, A_1, A_2, base.d));
		case 1:
		case 5:
		case 8:
		case 9:
			return OracleNumber.Parse(aq(A_0, A_1, A_2));
		case 4:
		case 22:
		case 101:
			return new OracleNumber(Devart.Common.e.i(A_0, A_1));
		case 21:
		case 100:
			return new OracleNumber(Devart.Common.e.e(A_0, A_1));
		case 3:
		case 246:
			return new OracleNumber(Devart.Common.e.g(A_0, A_1));
		case 2:
		case 6:
			return OracleNumber.a(A_0, A_1);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleNumber)));
		}
	}

	public virtual OracleString k(byte[] A_0, int A_1, int A_2)
	{
		if (this.m_d)
		{
			return new OracleString(base.o(A_0, A_1, A_2));
		}
		switch (this.m_b)
		{
		case 2:
		case 6:
			return OracleNumberUtils.c(A_0, A_1);
		case 12:
			return OracleDate.a(A_0, A_1).ToString();
		case 156:
			return OracleDate.b(A_0, A_1).ToString();
		case 187:
		case 188:
		case 232:
			return v(A_0, A_1, A_2).ToString();
		case 190:
			return u(A_0, A_1, A_2).ToString();
		case 189:
			return r(A_0, A_1, A_2).ToString();
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleString)));
		}
	}

	public virtual object t(byte[] A_0, int A_1, int A_2)
	{
		switch (this.m_b)
		{
		case 1:
		case 5:
		case 9:
		case 11:
		case 104:
			return new OracleString(aq(A_0, A_1, A_2));
		case 96:
			if (OracleUtils.OracleClientCompatible)
			{
				return new OracleString(Devart.Common.v.a(A_0, A_1, A_2, base.d));
			}
			return new OracleString(b(A_0, A_1, A_2, base.d));
		case 2:
		case 6:
			return OracleNumber.a(A_0, A_1);
		case 12:
			return OracleDate.e(A_0, A_1);
		case 156:
			return OracleDate.f(A_0, A_1);
		case 23:
			return new OracleBinary(ak(A_0, A_1, A_2));
		case 3:
		case 4:
		case 21:
		case 22:
		case 100:
		case 101:
		case 246:
			return l(A_0, A_1, A_2);
		case 187:
		case 188:
		case 232:
			return v(A_0, A_1, A_2);
		case 190:
			return u(A_0, A_1, A_2);
		case 189:
			return r(A_0, A_1, A_2);
		case 102:
		case 116:
			return a(A_0, A_1, A_2: true);
		case 112:
		case 113:
		case 114:
		case 115:
			return a(A_0, A_1, A_2: true, A_3: false);
		case 95:
			return a(A_0, A_1);
		case 58:
		case 108:
		case 122:
		case 247:
		case 248:
			return e(A_0, A_1, A_2: true);
		case 110:
			return b(A_0, A_1, A_2: true);
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(object)));
		}
	}

	protected new virtual object a(byte[] A_0, int A_1)
	{
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleBinary)));
	}

	public new virtual bf a(byte[] A_0, int A_1, bool A_2, bf A_3)
	{
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleObject)));
	}

	public new virtual ak a(byte[] A_0, int A_1, bool A_2, ak A_3)
	{
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleArray)));
	}

	public new virtual al a(byte[] A_0, int A_1, bool A_2, al A_3)
	{
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleXml)));
	}

	public new virtual b a(byte[] A_0, int A_1, bool A_2, b A_3)
	{
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleAnyData)));
	}

	public new virtual NativeOracleObject c(byte[] A_0, int A_1, bool A_2)
	{
		OracleDbType oracleDbType = k();
		if (oracleDbType == OracleDbType.Object)
		{
			bf bf2 = a(A_0, A_1, A_2, (bf)null);
			if (bf2 == null)
			{
				return null;
			}
			return new NativeOracleObject(bf2, l(), A_2: true);
		}
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(NativeOracleObject)));
	}

	public new virtual NativeOracleArray d(byte[] A_0, int A_1, bool A_2)
	{
		OracleDbType oracleDbType = k();
		if (oracleDbType == OracleDbType.Array || oracleDbType == OracleDbType.Table)
		{
			ak ak2 = a(A_0, A_1, A_2, (ak)null);
			if (ak2 == null)
			{
				return null;
			}
			if (k() == OracleDbType.Array)
			{
				return new NativeOracleArray(ak2, l(), A_2: true);
			}
			return new NativeOracleTable(ak2, l(), A_2: true);
		}
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(NativeOracleArray)));
	}

	public new object e(byte[] A_0, int A_1, bool A_2)
	{
		switch (k())
		{
		case OracleDbType.Array:
		case OracleDbType.Table:
		{
			ak ak2 = a(A_0, A_1, A_2, (ak)null);
			if (ak2 == null)
			{
				ak2 = new OracleArray(this.m_i);
			}
			return OracleArray.a(ak2, l(), A_2: true);
		}
		case OracleDbType.Object:
		{
			bf bf2 = a(A_0, A_1, A_2, (bf)null);
			if (bf2 == null)
			{
				bf2 = new OracleObject(this.m_i);
			}
			return OracleObject.a(bf2, l(), A_2: true);
		}
		case OracleDbType.AnyData:
		{
			b b2 = a(A_0, A_1, A_2, (b)null);
			if (b2 == null)
			{
				return OracleAnyData.Null;
			}
			return new OracleAnyData(b2, l());
		}
		case OracleDbType.Xml:
		{
			al al2 = a(A_0, A_1, A_2, (al)null);
			if (al2 == null)
			{
				return OracleXml.Null;
			}
			return new OracleXml(al2, l());
		}
		default:
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleObject)));
		}
	}

	public new static object a(object A_0, OracleType A_1, OracleConnection A_2)
	{
		switch (A_1.DbType)
		{
		case OracleDbType.Object:
			return OracleObject.a((bf)A_0, A_2, A_2: false);
		case OracleDbType.Array:
		case OracleDbType.Table:
			return OracleArray.a((ak)A_0, A_2, A_2: false);
		default:
			throw new InvalidOperationException();
		}
	}

	public new virtual f a(byte[] A_0, int A_1, bool A_2, f A_3)
	{
		throw new InvalidOperationException(string.Format(Devart.Common.al.a("ConvertFailed"), c(), typeof(OracleRef)));
	}

	public new OracleRef b(byte[] A_0, int A_1, bool A_2)
	{
		f a_ = a(A_0, A_1, A_2, (f)null);
		return new OracleRef(a_, this.m_i, l());
	}

	public override void a(byte[] A_0, int A_1, object A_2)
	{
		base.a(A_0, A_1, A_2);
	}

	[SpecialName]
	public new static Hashtable d()
	{
		return Devart.Data.Oracle.k.m_k;
	}

	[SpecialName]
	protected override Type c()
	{
		return this.m_a;
	}

	[SpecialName]
	public override string a()
	{
		switch (this.m_b)
		{
		case 1:
		case 5:
		case 9:
			if (this.m_c == OracleDbType.NVarChar)
			{
				return "NVARCHAR2";
			}
			return "VARCHAR2";
		case 96:
			if (this.m_c == OracleDbType.NChar)
			{
				return "NCHAR";
			}
			return "CHAR";
		case 2:
		case 6:
			return "NUMBER";
		case 12:
		case 156:
			return "DATE";
		case 11:
		case 104:
			return "ROWID";
		case 23:
		case 95:
			return "RAW";
		case 8:
			return "LONG";
		case 24:
			return "LONG RAW";
		case 112:
			if (this.m_c == OracleDbType.NClob)
			{
				return "NCLOB";
			}
			return "CLOB";
		case 113:
			return "BLOB";
		case 114:
			return "BFILE";
		case 115:
			return "CFILE";
		case 187:
			return "TIMESTAMP";
		case 232:
			return "TIMESTAMP WITH LOCAL TIME ZONE";
		case 188:
			return "TIMESTAMP WITH TIME ZONE";
		case 190:
			return "INTERVAL DAY TO SECOND";
		case 189:
			return "INTERVAL YEAR TO MONTH";
		case 3:
		case 246:
			return "NUMBER";
		case 4:
			return "NUMBER";
		case 22:
		case 101:
			return "BINARY_DOUBLE";
		case 21:
		case 100:
			return "BINARY_FLOAT";
		case 58:
			return "SYS.XMLTYPE";
		default:
			return base.a();
		}
	}

	[SpecialName]
	public new Type h()
	{
		return this.m_a;
	}

	[SpecialName]
	public OracleDbType k()
	{
		return this.m_c;
	}

	[SpecialName]
	public new int e()
	{
		return this.m_b;
	}

	[SpecialName]
	public int i()
	{
		return this.m_e;
	}

	[SpecialName]
	protected new g b()
	{
		return this.m_f;
	}

	[SpecialName]
	public new virtual void a(Hashtable A_0)
	{
	}

	[SpecialName]
	public new bool f()
	{
		return this.m_h;
	}

	[SpecialName]
	public new void a(bool A_0)
	{
		this.m_h = A_0;
	}

	[SpecialName]
	public OracleType j()
	{
		return this.m_i;
	}

	[SpecialName]
	public OracleConnection l()
	{
		return this.m_j;
	}

	[SpecialName]
	public new void a(OracleConnection A_0)
	{
		this.m_j = A_0;
	}

	public new virtual void g()
	{
	}
}
