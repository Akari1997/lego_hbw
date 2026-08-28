using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleDataReader : DbDataReaderBase, IDisposable
{
	internal const int a = 65535;

	private readonly OracleConnection m_b;

	internal readonly s[] c;

	private readonly s m_d;

	private readonly int m_e;

	private readonly int m_f;

	private readonly int g;

	private bool h;

	private int i;

	private int j;

	private s m_k;

	private h[] l;

	private e m;

	private byte[] n;

	private Hashtable o;

	private object[] p;

	private int q;

	private int r;

	private int s;

	private int t;

	private int u;

	private int v;

	private int w;

	private bool x;

	private bool y;

	private k[] z;

	private ArrayList aa;

	private OracleSelectStatement ab;

	private bool ac = true;

	private bool ad;

	private ReadLobMode ae;

	private OracleNumberMappingCollection af;

	private string ag;

	private bool ah = true;

	internal bool CloseCursors
	{
		set
		{
			ah = value;
		}
	}

	internal CommandBehavior CommandBehaviorInternal => base.CommandBehavior;

	public override int Depth
	{
		get
		{
			AssertReaderIsOpen("Depth");
			return g;
		}
	}

	public override bool EndOfData
	{
		get
		{
			if (!IsClosed)
			{
				if (s > t)
				{
					return !ac;
				}
				return false;
			}
			return true;
		}
	}

	protected override bool IsValidRow
	{
		get
		{
			if (s <= t)
			{
				return !y;
			}
			return false;
		}
	}

	public override int RecordsAffected => this.m_f;

	public override bool HasRows
	{
		get
		{
			if (t == -1)
			{
				bool flag = ac;
				Read();
				ac = flag;
				return y = u != 0;
			}
			return u != 0;
		}
	}

	public override int FieldCount
	{
		get
		{
			AssertReaderIsOpen("FieldCount");
			if (l == null)
			{
				return 0;
			}
			return l.Length;
		}
	}

	internal OracleSelectStatement Parser => ab;

	internal bool ObjectView
	{
		get
		{
			return ad;
		}
		set
		{
			if (ad != value)
			{
				ad = value;
				schemaTable = null;
				if (l != null && p != null)
				{
					p = new object[l.Length];
				}
			}
		}
	}

	internal ReadLobMode ReadLobMode
	{
		get
		{
			return ae;
		}
		set
		{
			if (ae != value)
			{
				schemaTable = null;
				ae = value;
				if (l != null && p != null)
				{
					p = new object[l.Length];
				}
			}
		}
	}

	internal int CurrentRecord => v;

	internal int FetchSize
	{
		get
		{
			if (!h)
			{
				return i;
			}
			return 0;
		}
	}

	internal e DescribeInformation => m;

	internal string SchemaSql
	{
		get
		{
			if (ag == null && this.m_k != null)
			{
				return this.m_k.u();
			}
			return ag;
		}
		set
		{
			ag = value;
		}
	}

	internal OracleDataReader(s A_0, s[] A_1, OracleConnection A_2, CommandBehavior A_3, int A_4, int A_5, OracleNumberMappingCollection A_6)
		: this(A_0, A_1, A_2, A_3, A_4, A_5, 0, A_6, A_8: false)
	{
	}

	internal OracleDataReader(s A_0, s[] A_1, OracleConnection A_2, CommandBehavior A_3, int A_4, int A_5, int A_6, OracleNumberMappingCollection A_7)
		: this(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8: false)
	{
	}

	internal OracleDataReader(s A_0, s[] A_1, OracleConnection A_2, CommandBehavior A_3, int A_4, int A_5, OracleNumberMappingCollection A_6, bool A_7)
		: this(A_0, A_1, A_2, A_3, A_4, A_5, 0, A_6, A_7)
	{
	}

	internal OracleDataReader(s A_0, s[] A_1, OracleConnection A_2, CommandBehavior A_3, int A_4, int A_5, int A_6, OracleNumberMappingCollection A_7, bool A_8)
		: base(A_3)
	{
		this.m_b = A_2;
		this.c = A_1;
		this.m_d = A_0;
		g = A_6;
		af = A_7;
		A_4 = (IsCommandBehavior(CommandBehavior.SingleRow) ? 1 : A_4);
		h = A_4 == 0;
		i = A_4;
		this.m_e = A_5;
		this.m_f = A_0.i();
		ae = ReadLobMode.Value;
		if (!A_8)
		{
			b();
		}
	}

	internal OracleDataReader(s A_0, s[] A_1, OracleConnection A_2, CommandBehavior A_3, int A_4, int A_5, int A_6, OracleNumberMappingCollection A_7, e A_8)
		: this(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9: false)
	{
	}

	internal OracleDataReader(s A_0, s[] A_1, OracleConnection A_2, CommandBehavior A_3, int A_4, int A_5, int A_6, OracleNumberMappingCollection A_7, e A_8, bool A_9)
		: base(A_3)
	{
		m = A_8;
		this.m_b = A_2;
		this.c = A_1;
		this.m_d = A_0;
		g = A_6;
		af = A_7;
		A_4 = (IsCommandBehavior(CommandBehavior.SingleRow) ? 1 : A_4);
		h = A_4 == 0;
		i = A_4;
		this.m_e = A_5;
		this.m_f = A_0.i();
		ae = ReadLobMode.Value;
		if (!A_9)
		{
			b();
		}
	}

	~OracleDataReader()
	{
		try
		{
			Close();
		}
		catch (ObjectDisposedException)
		{
		}
		catch (OracleException)
		{
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			Close();
		}
	}

	void IDisposable.Dispose()
	{
		Close();
	}

	private void b()
	{
		this.m_k = this.c[j];
		v = 0;
		w = 1000;
		t = -1;
		ac = true;
		r = 0;
		s = 0;
		u = 0;
		if (af != null)
		{
			af.a = true;
		}
		g g2 = this.m_d.aa();
		bool flag = j == 0 && m != null;
		bool flag2;
		if (flag)
		{
			l = m.a;
			flag2 = m.b;
			q = m.c;
		}
		else
		{
			l = this.m_k.m();
			x = l == null;
			bool flag3 = this.m_d.v().a();
			flag2 = false;
			if (l != null)
			{
				int num = l.Length;
				aq aq2 = g2.h();
				bool flag4 = false;
				int num2 = 0;
				for (int num3 = 0; num3 < num; num3++)
				{
					int num4 = l[num3].c;
					l[num3].n = num2;
					l[num3].m = l[num3].d;
					bool flag5 = false;
					switch (num4)
					{
					case 1:
						num2 += num2 % 2;
						if (g2.b())
						{
							l[num3].m = 1;
						}
						else if (flag3)
						{
							l[num3].m = l[num3].m * 2 + 2;
						}
						else
						{
							l[num3].m++;
						}
						l[num3].c = 5;
						l[num3].o = num2 + 2;
						num2 += 4;
						break;
					case 96:
						num2 += num2 % 2;
						if (g2.b())
						{
							l[num3].m = 1;
						}
						else if (flag3)
						{
							l[num3].m *= 2;
						}
						l[num3].o = num2 + 2;
						num2 += 4;
						break;
					case 100:
						l[num3].c = 21;
						break;
					case 101:
						l[num3].c = 22;
						break;
					case 2:
					case 6:
					{
						a(l[num3].e, l[num3].f, out var A_, out var A_2);
						l[num3].c = A_;
						l[num3].ac = A_2;
						switch (A_)
						{
						case 6:
							l[num3].m = 22;
							break;
						case 3:
							l[num3].m = 4;
							break;
						case 4:
							l[num3].m = 8;
							break;
						default:
							throw new InvalidExpressionException();
						}
						break;
					}
					case 12:
					case 156:
						if (string.Compare(OracleUtils.c(this.m_b.ServerVersion), "08") < 0)
						{
							l[num3].c = 12;
						}
						else
						{
							l[num3].c = 156;
						}
						break;
					case 108:
					case 110:
					case 122:
					{
						flag2 = true;
						OracleType objectType = OracleType.GetObjectType(l[num3].u, this.m_b);
						object obj = g2.b(objectType);
						l[num3].s = obj;
						l[num3].m = IntPtr.Size;
						l[num3].l = num2 + 2 + 2;
						num2 += 2 + IntPtr.Size * 2 + 2;
						flag5 = true;
						break;
					}
					case 8:
					case 24:
						flag4 = true;
						l[num3].y = a;
						l[num3].m = int.MaxValue;
						num2 += 2;
						flag5 = true;
						break;
					case 11:
					case 104:
						num2 += num2 % 2;
						if (g2.b())
						{
							l[num3].d = Math.Max(40, l[num3].m * 2);
							l[num3].m = 1;
						}
						else if (flag3)
						{
							l[num3].m = (l[num3].d = Math.Max(40, l[num3].m * 2));
						}
						else
						{
							l[num3].m = (l[num3].d = Math.Max(20, l[num3].m * 2));
						}
						l[num3].o = num2 + 2;
						num2 += 4;
						l[num3].l = num2 + 2;
						num2 += 2 + l[num3].m;
						flag5 = true;
						break;
					case 23:
						num2 += 2;
						num2 += num2 % 2;
						l[num3].o = num2;
						num2 += 2;
						break;
					case 187:
					case 188:
					case 189:
					case 190:
					case 232:
						if (g2.h().d() < 9000000)
						{
							throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
						}
						l[num3].m = aq2.a(num4);
						l[num3].l = num2 + 2 + 2;
						num2 += 2 + l[num3].m + 2;
						flag5 = true;
						break;
					case 112:
					case 113:
					case 114:
					case 115:
					case 116:
						flag2 = true;
						l[num3].m = aq2.a(num4);
						l[num3].l = num2 + 2 + 2;
						num2 += 2 + l[num3].m + 2;
						flag5 = true;
						break;
					case 58:
					case 102:
						flag2 = true;
						l[num3].m = aq2.a(num4);
						break;
					default:
						l[num3].m = aq2.a(num4);
						break;
					}
					if (!flag5)
					{
						l[num3].l = num2 + 2;
						num2 += 2 + l[num3].m;
					}
					num2 += num2 % 2;
				}
				for (int num5 = 0; num5 < l.Length; num5++)
				{
					l[num5].q = num2;
					if (l[num5].o != 0)
					{
						l[num5].r = num2;
					}
					l[num5].p = num2;
				}
				q = num2;
				if (j == 0 && !flag4)
				{
					m = new e();
					m.a = l;
					m.b = flag2;
					m.c = q;
				}
			}
		}
		if (l != null)
		{
			z = new k[l.Length];
			for (int num6 = 0; num6 < l.Length; num6++)
			{
				int num7 = l[num6].c;
				switch (num7)
				{
				case 108:
				case 110:
				case 122:
				{
					OracleType objectType2 = OracleType.GetObjectType(l[num6].u, this.m_b);
					z[num6] = g2.b(num7, l[num6].h, l[num6].ac, objectType2);
					z[num6].a(this.m_b);
					break;
				}
				case 8:
				case 24:
				{
					w = 100;
					ArrayList arrayList = new ai(g2.b() && string.Compare(g2.v(), "09") <= 0 && g2.h().a(), i);
					l[num6].w = arrayList;
					if (aa == null)
					{
						aa = new ArrayList();
					}
					aa.Add(arrayList);
					z[num6] = new z(g2, arrayList, l[num6].c, q);
					break;
				}
				default:
					z[num6] = g2.a(l[num6].c, l[num6].h, l[num6].ac);
					break;
				}
				z[num6].a(this.m_b.TrimFixedChar);
			}
			e();
			if (flag || !h)
			{
				a();
			}
		}
		if (flag2)
		{
			p = new object[l.Length];
		}
		else
		{
			p = null;
		}
	}

	private void a(int A_0, int A_1, out short A_2, out Type A_3)
	{
		A_3 = null;
		if (af != null && af.Count != 0)
		{
			foreach (OracleNumberMapping item in af)
			{
				bool flag = false;
				flag = (A_1 == -127 && A_0 != 0 && item.NumberType == OracleNumberType.Float) || (A_1 == 0 && item.NumberType == OracleNumberType.Integer) || item.NumberType == OracleNumberType.Number;
				if (flag && A_0 >= item.FromPrecision && A_0 <= item.ToPrecision)
				{
					A_3 = item.ValueType;
					break;
				}
			}
		}
		if ((object)A_3 != null)
		{
			A_2 = 0;
			switch (OracleUtils.c(A_3))
			{
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
				A_2 = 3;
				break;
			case TypeCode.Single:
			case TypeCode.Double:
				A_2 = 4;
				break;
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				A_2 = 6;
				break;
			case TypeCode.Boolean:
			case TypeCode.Decimal:
				A_2 = 6;
				break;
			}
			if (A_2 == 0)
			{
				throw new NotSupportedException($"Type {A_3.Name} is not supported as GetValue result.");
			}
		}
		else if (A_0 == 0 || A_0 > 15)
		{
			A_2 = 6;
			A_3 = typeof(decimal);
		}
		else if (A_0 < 10 && A_1 == 0)
		{
			A_2 = 3;
			A_3 = typeof(int);
		}
		else
		{
			A_2 = 4;
			A_3 = typeof(double);
		}
	}

	public override void Close()
	{
		if (closed)
		{
			return;
		}
		closed = true;
		if (this.m_b != null)
		{
			this.m_b.b(this);
		}
		using (this.m_b.LocalFailoverManager.StartUse())
		{
			try
			{
				GC.SuppressFinalize(this);
				Exception ex = null;
				try
				{
					this.m_d.y();
				}
				catch (Exception ex2)
				{
					ex = ex2;
					ap ap2 = this.m_b.InnerConnection as ap;
					if (ex2 is OracleException a_)
					{
						this.m_b.a(this, a_);
					}
					ap2?.a(ex2);
					if (!(ex2 is OracleException) || Oci.IsFatalError((OracleException)ex2))
					{
						throw;
					}
				}
				for (int num = 0; num < this.c.Length; num++)
				{
					try
					{
						if (this.c[num] != this.m_d)
						{
							if (ah)
							{
								this.c[num].l();
							}
							else
							{
								this.c[num].y();
							}
						}
					}
					catch (Exception ex3)
					{
						ex = ex3;
						ap ap3 = this.m_b.InnerConnection as ap;
						if (ex3 is OracleException a_2)
						{
							this.m_b.a(this, a_2);
						}
						ap3?.a(ex3);
						if (!(ex3 is OracleException) || Oci.IsFatalError((OracleException)ex3))
						{
							throw;
						}
					}
				}
				if (ex != null)
				{
					throw ex;
				}
			}
			catch (Exception ex4)
			{
				if (this.m_b.LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Read, RetryMode.Reexecute, ex4) == RetryMode.Raise)
				{
					throw;
				}
			}
		}
		if (this.m_b != null && IsCommandBehavior(CommandBehavior.CloseConnection))
		{
			this.m_b.Close();
		}
		l = null;
		p = null;
	}

	private void c(int A_0)
	{
		if (closed)
		{
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("DataReaderClosed"), "access column"));
		}
		if (l == null || A_0 >= l.Length)
		{
			throw new IndexOutOfRangeException();
		}
	}

	private void b(int A_0)
	{
		if (closed)
		{
			throw new InvalidOperationException(string.Format(Devart.Common.al.a("DataReaderClosed"), "access column data"));
		}
		if (l == null || A_0 >= l.Length)
		{
			throw new IndexOutOfRangeException();
		}
		if (s > t || y)
		{
			throw new InvalidOperationException(Devart.Common.al.a("DataReaderNoData"));
		}
	}

	private static int a(object A_0, IntPtr A_1, int A_2, out a6 A_3)
	{
		ai ai2 = (ai)A_0;
		while (ai2.Count <= A_2)
		{
			ai2.Add(new a6(ai2.a()));
		}
		A_3 = (a6)ai2[A_2];
		A_3.c();
		return -24200;
	}

	private void a()
	{
		n = new byte[i * q];
		o = new Hashtable();
		this.m_k.a(l, n, o);
	}

	public override bool Read()
	{
		AssertReaderIsOpen("Read");
		ac = false;
		if ((base.CommandBehavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default)
		{
			return false;
		}
		if (this.m_e > 0 && v >= this.m_e)
		{
			return false;
		}
		v++;
		if (y)
		{
			y = false;
			return IsValidRow;
		}
		if (l != null && p != null)
		{
			p = new object[l.Length];
		}
		if (s < t)
		{
			s++;
			r += q;
			return true;
		}
		if (x)
		{
			if (s == t)
			{
				s++;
			}
			return false;
		}
		using (this.m_b.LocalFailoverManager.StartUse())
		{
			try
			{
				int a_ = i;
				if (h)
				{
					a();
					k();
				}
				if (aa != null)
				{
					foreach (ArrayList item in aa)
					{
						item?.Clear();
					}
				}
				x = !this.m_k.b(a_);
				int num = this.m_k.j();
				s = 0;
				r = 0;
				t = num - u - 1;
				u = num;
			}
			catch (OracleException a_2)
			{
				this.m_b.LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Read, RetryMode.Raise, a_2);
				this.m_b.a(this, a_2);
				throw;
			}
		}
		return s <= t;
	}

	internal int e()
	{
		if (i == 0 && h)
		{
			i = Math.Max(4000 / q, 20);
			if (i > w)
			{
				i = w;
			}
		}
		return i;
	}

	internal void k()
	{
		if (h)
		{
			if (i < w)
			{
				i <<= 1;
			}
			else
			{
				h = false;
			}
		}
	}

	internal void a(int A_0, bool A_1)
	{
		u = A_0;
		x = A_1;
		s = 0;
		r = 0;
		t = A_0 - 1;
		y = true;
	}

	public override bool NextResult()
	{
		base.NextResult();
		using (this.m_b.LocalFailoverManager.StartUse())
		{
			try
			{
				p = null;
				if (this.m_k != this.m_d)
				{
					this.m_k.l();
				}
				if (j >= this.c.Length - 1)
				{
					return false;
				}
				j++;
				b();
				return true;
			}
			catch (Exception ex)
			{
				this.m_b.LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Read, RetryMode.Raise, ex);
				throw;
			}
		}
	}

	protected override void FillSchemaTable(DataTable dataTable)
	{
		DataColumnCollection columns = dataTable.Columns;
		DataColumn column = columns[SchemaTableColumn.ColumnName];
		DataColumn column2 = columns[SchemaTableColumn.ColumnOrdinal];
		DataColumn column3 = columns[SchemaTableColumn.ColumnSize];
		DataColumn column4 = columns[SchemaTableColumn.NumericPrecision];
		DataColumn column5 = columns[SchemaTableColumn.NumericScale];
		DataColumn column6 = columns[SchemaTableColumn.DataType];
		DataColumn column7 = columns[SchemaTableOptionalColumn.ProviderSpecificDataType];
		DataColumn column8 = columns[SchemaTableColumn.ProviderType];
		DataColumn column9 = columns[SchemaTableColumn.IsLong];
		DataColumn column10 = columns[SchemaTableColumn.AllowDBNull];
		DataColumn column11 = columns[SchemaTableColumn.IsUnique];
		DataColumn column12 = columns[SchemaTableColumn.IsKey];
		DataColumn column13 = columns[SchemaTableColumn.BaseSchemaName];
		DataColumn column14 = columns[SchemaTableColumn.BaseTableName];
		DataColumn column15 = columns[SchemaTableColumn.BaseColumnName];
		DataColumn column16 = columns[SchemaTableColumn.IsExpression];
		DataColumn column17 = columns[SchemaTableColumn.IsAliased];
		DataColumn column18 = columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
		columns.Add(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
		DataColumn column19 = columns.Add(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
		DataColumn column20 = columns.Add(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
		DataColumn column21 = columns.Add("TypeName", typeof(string));
		DataColumn column22 = columns.Add("TypeSchemaName", typeof(string));
		DataColumn column23 = columns.Add("IdentityType", typeof(OracleIdentityType));
		if (FieldCount == 0)
		{
			return;
		}
		ab = null;
		List<a8> list = new List<a8>();
		if (SchemaSql != null && SchemaSql != "")
		{
			try
			{
				ab = OracleSelectStatement.a(SchemaSql, ParserBehavior.All, A_2: false);
				for (int num = 0; num < ab.Tables.Count; num++)
				{
					list.Add(new a8(ab.Tables[num]));
				}
				if (list.Count == 0 || ab.Columns.Count == 0)
				{
					ab = null;
				}
			}
			catch
			{
				ab = null;
			}
		}
		Dictionary<string, a8> dictionary = new Dictionary<string, a8>();
		if (IsCommandBehavior(CommandBehavior.KeyInfo) && ab != null)
		{
			OracleCommand oracleCommand = null;
			try
			{
				for (int num2 = 0; num2 < list.Count; num2++)
				{
					a8 a10 = list[num2];
					if (a10.d() == null || a10.d() == "" || a10.b != 0)
					{
						continue;
					}
					if (a10.b() == "")
					{
						if (oracleCommand == null)
						{
							oracleCommand = this.m_b.CreateCommand();
							string text = ((string.Compare(this.m_b.d().ServerVersionNormalized, "08.01") >= 0) ? "SYS_CONTEXT('USERENV', 'CURRENT_SCHEMA')" : "user");
							oracleCommand.CommandText = "declare\n  p_owner varchar(30) := :1;\n  p_table_name varchar(30) := :2;\n  p_object_type varchar(19);\n  p_db_link varchar(128);\nbegin\n  loop\n    if p_owner is null then\n      select object_type, owner into p_object_type, p_owner\n      from (\n";
							bool flag = string.Compare(this.m_b.d().ServerVersionNormalized, "09") < 0;
							if (flag)
							{
								OracleCommand oracleCommand2 = oracleCommand;
								oracleCommand2.CommandText = oracleCommand2.CommandText + "    select 1 as id, object_type, owner\n        from sys.all_objects\n         where owner = " + text + "\n          and object_name = p_table_name\n      union \n         select 2 AS id, object_type, owner \n           from sys.all_objects \n            where owner = 'PUBLIC'            and object_name = p_table_name\n";
							}
							else
							{
								OracleCommand oracleCommand3 = oracleCommand;
								oracleCommand3.CommandText = oracleCommand3.CommandText + "    select object_type, owner\n        from sys.all_objects\n        where owner in ('PUBLIC', " + text + ")\n          and object_name = p_table_name\n        order by decode(owner, 'PUBLIC', 2, 1)\n";
							}
							oracleCommand.CommandText += "      )\n      where rownum <= 1;\n      :1 := p_owner;\n    else\n      select object_type into p_object_type\n        from sys.all_objects\n        where owner = p_owner and object_name = p_table_name and rownum <= 1;\n    end if;\n    if p_object_type != 'SYNONYM' then\n      exit;\n    end if;\n    select table_owner, table_name, db_link\n      into p_owner, p_table_name, p_db_link\n      from sys.all_synonyms\n      where owner = p_owner and synonym_name = p_table_name;\n    if p_db_link is not null then\n      return;\n    end if;\n  end loop;\n  open :3 for\n    select column_name, column_id, ' ' as constraint_type\n      from sys.all_tab_columns\n      where owner = p_owner and table_name = p_table_name\n    union all\n    select * from (\n      select cc.column_name, 0 as column_id, cs.constraint_type || ':' || cs.constraint_name\n      from sys.all_constraints cs, sys.all_cons_columns cc\n      where cc.owner = p_owner and cc.table_name = p_table_name and\n        cs.owner = cc.owner and cs.table_name = cc.table_name and\n        cs.constraint_name = cc.constraint_name and cs.constraint_type in ('P','U')\n";
							if (!flag)
							{
								oracleCommand.CommandText += "      order by cs.constraint_type, cs.constraint_name";
							}
							oracleCommand.CommandText += "  );\n end;";
							oracleCommand.Parameters.Add("1", OracleDbType.VarChar, ParameterDirection.InputOutput);
							oracleCommand.Parameters.Add("2", OracleDbType.VarChar, ParameterDirection.Input);
							oracleCommand.Parameters.Add("3", OracleDbType.Cursor, ParameterDirection.Output);
							oracleCommand.Prepare();
						}
						oracleCommand.Parameters[0].Value = a10.a();
						oracleCommand.Parameters[1].Value = a10.d();
						oracleCommand.ExecuteNonQuery();
						if (a10.a() == "")
						{
							a10.a(oracleCommand.Parameters[0].Value as string);
						}
						if (oracleCommand.Parameters[2].OracleValue is OracleCursor oracleCursor)
						{
							using OracleDataReader oracleDataReader = oracleCursor.GetDataReader();
							int num3 = 0;
							string text2 = "";
							ao ao2 = null;
							while (oracleDataReader.Read())
							{
								string text3 = oracleDataReader.GetString(0).TrimEnd(new char[0]);
								int @int = oracleDataReader.GetInt32(1);
								string text4 = oracleDataReader.GetString(2);
								if (@int > 0)
								{
									dictionary[text3] = a10;
									num3++;
									continue;
								}
								if (text4 != text2)
								{
									text2 = text4;
									ao2 = new ao();
									ao2.a = text2[0] == 'P';
								}
								ao2.b.Add(text3);
								a10.c.Add(ao2);
								if (!a10.d.TryGetValue(text3, out var value))
								{
									value = new List<ao>();
									a10.d[text3] = value;
								}
								value.Add(ao2);
							}
							a10.b = num3;
						}
					}
					if (a10.b != 0 || list.Count <= 1)
					{
						continue;
					}
					bool flag2 = false;
					foreach (SelectColumn column24 in ab.Columns)
					{
						if (column24.Name == "*")
						{
							if (column24.Table == "" || (ab.n > 1 && (column24.Table == a10.c() || column24.Table == a10.d())))
							{
								flag2 = true;
								break;
							}
						}
						else if (column24.Table == "" && !dictionary.ContainsKey(column24.Name))
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						continue;
					}
					using OracleCommand oracleCommand4 = this.m_b.CreateCommand();
					string text5 = '"' + a10.d() + '"';
					if (a10.a() != "")
					{
						text5 = '"' + a10.a() + "\"." + text5;
					}
					if (a10.b() != "")
					{
						text5 = text5 + '@' + a10.b();
					}
					oracleCommand4.CommandText = "SELECT * FROM " + text5;
					using OracleDataReader oracleDataReader2 = oracleCommand4.ExecuteReader(CommandBehavior.SchemaOnly);
					a10.b = oracleDataReader2.FieldCount;
					for (int num4 = 0; num4 < oracleDataReader2.FieldCount; num4++)
					{
						dictionary[oracleDataReader2.GetName(num4)] = a10;
					}
				}
			}
			finally
			{
				oracleCommand?.Dispose();
			}
		}
		bi[] array = null;
		if (ab != null)
		{
			array = new bi[FieldCount];
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			SelectColumn selectColumn2 = null;
			a8 value2 = null;
			for (int num9 = 0; num9 < FieldCount; num9++)
			{
				string text6 = null;
				if (num6 == 0)
				{
					if (num7 > 0)
					{
						value2 = list[num7];
					}
					else
					{
						selectColumn2 = ab.Columns[num5];
						if (selectColumn2.Expression != null && an.a(selectColumn2.Expression.ToUpper(CultureInfo.InvariantCulture)))
						{
							selectColumn2.Name = selectColumn2.Expression;
							selectColumn2.Expression = null;
						}
						value2 = null;
						if (selectColumn2.Expression == null)
						{
							if (list.Count == 1)
							{
								value2 = list[0];
							}
							else if (selectColumn2.Table == "")
							{
								if (IsCommandBehavior(CommandBehavior.KeyInfo))
								{
									if (selectColumn2.Name == "*")
									{
										value2 = list[0];
									}
									else if (!dictionary.TryGetValue(selectColumn2.Name, out value2))
									{
										selectColumn2.Expression = selectColumn2.Name;
										selectColumn2.Name = "";
									}
								}
							}
							else if (IsCommandBehavior(CommandBehavior.KeyInfo) || selectColumn2.Name != "*" || ab.n == 1)
							{
								foreach (a8 item in list)
								{
									if ((item.c() != "" && item.c() == selectColumn2.Table) || (item.d() != "" && item.d() == selectColumn2.Table))
									{
										value2 = item;
										if (item.a() == selectColumn2.Schema)
										{
											break;
										}
									}
								}
							}
						}
					}
				}
				if (selectColumn2.Expression != null)
				{
					if (!selectColumn2.Expression.EndsWith(".*"))
					{
						num5++;
					}
				}
				else if (selectColumn2.Name == "*")
				{
					text6 = GetName(num9);
					if (num6 == 0)
					{
						num8 = ((ab.n == 1 && (selectColumn2.Table != "" || list.Count == 1)) ? (FieldCount - ab.Columns.Count + 1) : (value2?.b ?? 0));
					}
					num6++;
					if (num6 == num8)
					{
						num6 = 0;
						if (selectColumn2.Table == "")
						{
							num7++;
						}
						else
						{
							num5++;
						}
					}
				}
				else
				{
					text6 = selectColumn2.Name;
					num5++;
				}
				array[num9] = new bi(text6, selectColumn2, value2);
				if (value2 != null && text6 != null)
				{
					value2.e[text6] = num9;
				}
			}
		}
		bool flag3 = false;
		if (IsCommandBehavior(CommandBehavior.KeyInfo) && ab != null)
		{
			flag3 = true;
			foreach (a8 item2 in list)
			{
				if (item2.c.Count == 0)
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				foreach (a8 item3 in list)
				{
					foreach (ao item4 in item3.c)
					{
						flag3 = true;
						foreach (string item5 in item4.b)
						{
							if (!item3.e.TryGetValue(item5, out var value3) || (!item4.a && l[value3].i))
							{
								flag3 = false;
								break;
							}
						}
						if (flag3)
						{
							item3.f = item4;
							break;
						}
					}
					if (!flag3)
					{
						break;
					}
				}
			}
		}
		for (int num10 = 0; num10 < FieldCount; num10++)
		{
			DataRow dataRow = dataTable.NewRow();
			OracleDbType oracleDbType = z[num10].k();
			dataRow[column] = GetName(num10);
			dataRow[column2] = num10;
			dataRow[column4] = l[num10].e;
			dataRow[column5] = l[num10].f;
			dataRow[column6] = GetFieldType(num10);
			Type providerSpecificFieldType = GetProviderSpecificFieldType(num10);
			if ((object)providerSpecificFieldType == null)
			{
				dataRow[column7] = DBNull.Value;
			}
			else
			{
				dataRow[column7] = providerSpecificFieldType;
			}
			dataRow[column8] = (int)oracleDbType;
			dataRow[column10] = l[num10].i;
			if (l[num10].s != null)
			{
				k k2 = z[num10];
				dataRow[column21] = k2.j().d;
				dataRow[column22] = k2.j().c;
			}
			else
			{
				dataRow[column21] = GetDataTypeName(num10);
				dataRow[column22] = "";
			}
			bool flag4 = oracleDbType == OracleDbType.Clob || oracleDbType == OracleDbType.Blob || oracleDbType == OracleDbType.BFile || oracleDbType == OracleDbType.Long || oracleDbType == OracleDbType.LongRaw || oracleDbType == OracleDbType.NClob || oracleDbType == OracleDbType.Xml;
			dataRow[column9] = flag4;
			if (flag4)
			{
				dataRow[column3] = int.MaxValue;
			}
			else
			{
				dataRow[column3] = l[num10].d;
			}
			dataRow[column18] = false;
			dataRow[column20] = false;
			dataRow[column12] = false;
			dataRow[column11] = false;
			if (oracleDbType == OracleDbType.RowId && l[num10].a == "ROWID")
			{
				dataRow[column19] = true;
			}
			else
			{
				dataRow[column19] = false;
			}
			dataRow[column23] = l[num10].ad;
			if (l[num10].ad == OracleIdentityType.GeneratedAlways)
			{
				dataRow[column18] = true;
			}
			if (ab != null)
			{
				bi bi2 = array[num10];
				string text7 = bi2.a;
				SelectColumn selectColumn3 = bi2.b;
				a8 a11 = bi2.c;
				dataRow[column17] = selectColumn3.Alias != "";
				if (selectColumn3.Expression != null)
				{
					dataRow[column16] = true;
					dataRow[column19] = true;
				}
				else
				{
					dataRow[column16] = false;
					if (a11 != null)
					{
						if (a11.b() == "")
						{
							if (a11.a() == "")
							{
								dataRow[column13] = this.m_b.UserId.ToUpper(CultureInfo.InvariantCulture);
							}
							else if (a11.a() != "PUBLIC")
							{
								dataRow[column13] = a11.a();
							}
							dataRow[column14] = a11.d();
						}
						else
						{
							dataRow[column13] = a11.a();
							dataRow[column14] = a11.d() + "@" + a11.b();
						}
						dataRow[column15] = text7;
						if (IsCommandBehavior(CommandBehavior.KeyInfo) && a11.d.TryGetValue(text7, out var value4))
						{
							foreach (ao item6 in value4)
							{
								if (flag3 && item6 == a11.f)
								{
									dataRow[column12] = true;
								}
								if (!item6.a && list.Count == 1 && item6.b.Count == 1)
								{
									dataRow[column11] = true;
								}
							}
						}
					}
				}
			}
			else
			{
				dataRow[column15] = GetName(num10);
			}
			dataTable.Rows.Add(dataRow);
		}
	}

	private short a(int A_0)
	{
		switch (l[A_0].c)
		{
		case 156:
		{
			int num = r + l[A_0].l;
			short num2 = Devart.Common.e.h(n, num);
			num += 2;
			byte b2 = n[num++];
			byte b3 = n[num++];
			byte b4 = n[num++];
			byte b5 = n[num++];
			byte b6 = n[num];
			if (b6 < 0 || b6 > 59 || b5 < 0 || b5 > 59 || b4 < 0 || b4 > 24 || (num2 == 0 && b2 == 0 && b3 == 0 && b4 == 0 && b5 == 0 && b6 == 0))
			{
				return -1;
			}
			break;
		}
		case 12:
		{
			int num3 = r + l[A_0].l;
			_ = n[num3];
			_ = n[num3 + 1];
			num3 += 2;
			_ = n[num3++];
			_ = n[num3++];
			byte b7 = (byte)((sbyte)n[num3++] - 1);
			byte b8 = (byte)((sbyte)n[num3++] - 1);
			byte b9 = (byte)((sbyte)n[num3] - 1);
			if (b9 < 0 || b9 > 59 || b8 < 0 || b8 > 59 || b7 < 0 || b7 > 24)
			{
				return -1;
			}
			break;
		}
		case 8:
		case 24:
			if (l[A_0].w != null)
			{
				int index = r / q;
				if (!(((ArrayList)l[A_0].w)[index] is a6 a10))
				{
					return -1;
				}
				return (short)a10.d();
			}
			break;
		}
		return Devart.Common.e.h(n, r + l[A_0].n);
	}

	protected override DbDataReader GetDbDataReader(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		s s2 = ((p == null || p[i] == null) ? z[i].a(n, r + l[i].l, A_2: true) : ((s)p[i]));
		if (p != null)
		{
			p[i] = s2;
		}
		return new OracleDataReader(s2, new s[1] { s2 }, this.m_b, CommandBehavior.Default, 0, 0, g + 1, af);
	}

	public override string GetName(int i)
	{
		c(i);
		return l[i].a;
	}

	public override Type GetFieldType(int i)
	{
		c(i);
		k k2 = z[i];
		switch (k2.k())
		{
		case OracleDbType.BFile:
			if (ae == ReadLobMode.Value)
			{
				return k2.o();
			}
			return typeof(OracleBFile);
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
			if (ae == ReadLobMode.Value)
			{
				return k2.o();
			}
			return typeof(OracleLob);
		case OracleDbType.Array:
			if (ObjectView)
			{
				return k2.o();
			}
			return typeof(IDataReader);
		case OracleDbType.Table:
			if (ObjectView)
			{
				return k2.o();
			}
			return typeof(IDataReader);
		case OracleDbType.Cursor:
			return typeof(OracleDataReader);
		case OracleDbType.Ref:
		case OracleDbType.Xml:
			return typeof(string);
		default:
			return k2.o();
		}
	}

	public override string GetDataTypeName(int i)
	{
		c(i);
		string result = z[i].a();
		int num = z[i].e();
		if (num == 2 || num == 6)
		{
			if (l[i].e == 126 && (byte)l[i].f == 129)
			{
				return "FLOAT";
			}
			if (l[i].e == 63 && (byte)l[i].f == 129)
			{
				return "REAL";
			}
		}
		return result;
	}

	internal OracleDbType e(int A_0)
	{
		c(A_0);
		return z[A_0].k();
	}

	public override bool IsDBNull(int i)
	{
		b(i);
		return a(i) == -1;
	}

	public override string GetString(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return string.Empty;
		}
		k k2 = z[i];
		k2.a(o);
		OracleDbType oracleDbType = k2.k();
		if (oracleDbType == OracleDbType.Xml)
		{
			return GetValue(i) as string;
		}
		return k2.o(n, r + l[i].l, d(i));
	}

	public override short GetInt16(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return 0;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.b(n, r + l[i].l, d(i));
	}

	public override int GetInt32(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return 0;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.a(n, r + l[i].l, d(i));
	}

	public override long GetInt64(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return 0L;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.g(n, r + l[i].l, d(i));
	}

	public override double GetDouble(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return 0.0;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.d(n, r + l[i].l, d(i));
	}

	public override float GetFloat(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return 0f;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.c(n, r + l[i].l, d(i));
	}

	public override decimal GetDecimal(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return 0m;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.h(n, r + l[i].l, d(i));
	}

	public override DateTime GetDateTime(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return DateTime.MinValue;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.e(n, r + l[i].l, d(i));
	}

	public override byte GetByte(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return 0;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.i(n, r + l[i].l, d(i));
	}

	public override bool GetBoolean(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return false;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.m(n, r + l[i].l, d(i));
	}

	public override char GetChar(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return '\0';
		}
		k k2 = z[i];
		k2.a(o);
		return k2.n(n, r + l[i].l, d(i));
	}

	public override Guid GetGuid(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return Guid.Empty;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.q(n, r + l[i].l, d(i));
	}

	public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
	{
		if (bufferOffset < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("BufferOffsetNotNegative"), "bufferOffset");
		}
		if (fieldOffset < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("FieldOffsetNotNegative"), "fieldOffset");
		}
		if (length < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("LengthNotNegative"), "length");
		}
		if (a(i) == -1)
		{
			return 0L;
		}
		k k2 = z[i];
		k2.a(o);
		if (buffer == null)
		{
			switch (k2.k())
			{
			case OracleDbType.Number:
				return GetOracleNumber(i).BinData.Length;
			case OracleDbType.Char:
			case OracleDbType.VarChar:
			{
				string text = GetString(i);
				Encoding encoding = this.m_d.v().o();
				return encoding.GetByteCount(text);
			}
			case OracleDbType.BFile:
			case OracleDbType.Blob:
			case OracleDbType.Clob:
			case OracleDbType.NClob:
			{
				a3 a10 = k2.a(n, r + l[i].l, A_2: false, A_3: false);
				if (a10.p() == 2 || a10.d() == 1000)
				{
					return a10.f() + 2;
				}
				return a10.f();
			}
			case OracleDbType.Long:
			case OracleDbType.LongRaw:
			case OracleDbType.Raw:
			{
				byte[] array = k2.y(n, r + l[i].l, d(i));
				return array.Length;
			}
			default:
				throw new InvalidOperationException();
			}
		}
		switch (k2.k())
		{
		case OracleDbType.Number:
		{
			byte[] binData = GetOracleNumber(i).BinData;
			Buffer.BlockCopy(binData, (int)fieldOffset, buffer, bufferOffset, length);
			return binData.Length;
		}
		case OracleDbType.Char:
		case OracleDbType.VarChar:
		{
			string text2 = GetString(i);
			Encoding encoding2 = this.m_d.v().o();
			byte[] bytes = encoding2.GetBytes(text2);
			Buffer.BlockCopy(bytes, (int)fieldOffset, buffer, bufferOffset, length);
			return bytes.Length;
		}
		case OracleDbType.BFile:
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
		{
			a3 a11 = k2.a(n, r + l[i].l, A_2: false, A_3: false);
			if (k2.k() == OracleDbType.BFile)
			{
				a11.g();
			}
			try
			{
				return a11.a((int)fieldOffset, buffer, bufferOffset, length);
			}
			finally
			{
				if (k2.k() == OracleDbType.BFile)
				{
					a11.n();
				}
			}
		}
		case OracleDbType.Long:
		case OracleDbType.LongRaw:
		case OracleDbType.Raw:
		{
			byte[] array2 = k2.y(n, r + l[i].l, d(i));
			int num = Math.Min(length, array2.Length);
			Buffer.BlockCopy(array2, (int)fieldOffset, buffer, bufferOffset, num);
			return num;
		}
		default:
			throw new InvalidOperationException();
		}
	}

	public override long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
	{
		if (bufferOffset < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("BufferOffsetNotNegative"), "bufferOffset");
		}
		if (fieldOffset < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("FieldOffsetNotNegative"), "fieldOffset");
		}
		if (length < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("LengthNotNegative"), "length");
		}
		if (a(i) == -1)
		{
			return 0L;
		}
		bool flag = this.m_d.v().a();
		Encoding encoding = this.m_d.v().o();
		k k2 = z[i];
		k2.a(o);
		if (buffer == null)
		{
			switch (k2.k())
			{
			case OracleDbType.Char:
			case OracleDbType.NChar:
			case OracleDbType.NVarChar:
			case OracleDbType.VarChar:
				return GetString(i).Length;
			case OracleDbType.BFile:
			case OracleDbType.Blob:
			case OracleDbType.Clob:
			case OracleDbType.NClob:
			{
				a3 a10 = k2.a(n, r + l[i].l, A_2: false, A_3: false);
				return a10.f();
			}
			case OracleDbType.Long:
			case OracleDbType.LongRaw:
			case OracleDbType.Raw:
			{
				byte[] array = k2.y(n, r + l[i].l, d(i));
				return flag ? (array.Length / 2) : array.Length;
			}
			default:
				throw new NotSupportedException($"Type {GetDataTypeName(i)} is not supported.");
			}
		}
		int num = (flag ? (length * 2) : length);
		long num2 = (flag ? (fieldOffset * 2) : fieldOffset);
		switch (k2.k())
		{
		case OracleDbType.BFile:
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
		{
			a3 a11 = k2.a(n, r + l[i].l, A_2: false, A_3: false);
			if (!flag && (a11.p() == 2 || a11.d() == 1000))
			{
				flag = true;
				encoding = Encoding.Unicode;
				num = length * 2;
				num2 = fieldOffset * 2;
			}
			byte[] array3 = new byte[num];
			if (k2.k() == OracleDbType.BFile)
			{
				a11.g();
			}
			try
			{
				num = a11.a((int)num2, array3, 0, num);
			}
			finally
			{
				if (k2.k() == OracleDbType.BFile)
				{
					a11.n();
				}
			}
			return encoding.GetChars(array3, 0, num, buffer, bufferOffset);
		}
		case OracleDbType.Char:
		case OracleDbType.Long:
		case OracleDbType.LongRaw:
		case OracleDbType.NChar:
		case OracleDbType.NVarChar:
		case OracleDbType.Raw:
		case OracleDbType.VarChar:
		{
			byte[] array2 = k2.y(n, r + l[i].l, d(i));
			Buffer.BlockCopy(array2, (int)fieldOffset, buffer, bufferOffset, num);
			return encoding.GetChars(array2, 0, length, buffer, bufferOffset);
		}
		default:
			throw new NotSupportedException($"Type {GetDataTypeName(i)} is not supported.");
		}
	}

	public TimeSpan GetTimeSpan(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return TimeSpan.MinValue;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.f(n, r + l[i].l, d(i));
	}

	public TimeSpan GetTimeSpan(string name)
	{
		return GetTimeSpan(GetOrdinal(name));
	}

	public override object GetValue(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return DBNull.Value;
		}
		k k2 = z[i];
		k2.a(o);
		switch (k2.k())
		{
		case OracleDbType.BFile:
			if (ae == ReadLobMode.Value)
			{
				return k2.p(n, r + l[i].l, d(i));
			}
			return new OracleBFile(this.m_b, this, i, k2.a(n, r + l[i].l, A_2: true, A_3: false));
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
		{
			object obj = ((p == null || p[i] == null) ? null : p[i]);
			if (obj is OracleLob oracleLob)
			{
				return oracleLob.Value;
			}
			if (k2.k() == OracleDbType.Blob)
			{
				if (obj is byte[])
				{
					return obj as byte[];
				}
			}
			else if (obj is string)
			{
				return obj as string;
			}
			obj = ((ae != ReadLobMode.Value) ? new OracleLob(this.m_b, this, i, k2.a(n, r + l[i].l, A_2: true, A_3: false), k2.k()) : k2.p(n, r + l[i].l, d(i)));
			if (p != null)
			{
				p[i] = obj;
			}
			return obj;
		}
		case OracleDbType.Ref:
		{
			object obj;
			if (p != null && p[i] != null)
			{
				obj = p[i];
			}
			else
			{
				obj = k2.b(n, r + l[i].l, A_2: true);
				if (p != null)
				{
					p[i] = obj;
				}
			}
			if (obj != null)
			{
				obj = obj.ToString();
			}
			return obj;
		}
		case OracleDbType.Cursor:
		{
			object obj = ((p == null || p[i] == null) ? new OracleCursor(this.m_b, k2.a(n, r + l[i].l, A_2: true), FetchSize) : p[i]);
			if (p != null)
			{
				p[i] = obj;
			}
			return obj;
		}
		case OracleDbType.Array:
		case OracleDbType.Object:
		case OracleDbType.Table:
		case OracleDbType.Xml:
		{
			object obj;
			if (p != null && p[i] != null)
			{
				obj = p[i];
			}
			else if ((k2.k() == OracleDbType.Array || k2.k() == OracleDbType.Table) && !ObjectView)
			{
				object obj2 = k2.d(n, r + l[i].l, A_2: true);
				obj = ((k2.k() != OracleDbType.Table) ? new OracleArray(k2.j()) : new OracleTable(k2.j()));
				((ICustomOracleArray)obj).FromOracleArray((NativeOracleArray)obj2);
			}
			else
			{
				obj = k2.e(n, r + l[i].l, A_2: true);
			}
			if (p != null)
			{
				p[i] = obj;
			}
			if ((k2.k() == OracleDbType.Array || k2.k() == OracleDbType.Table) && !ObjectView)
			{
				obj = new OracleArrayDataReader((OracleArray)obj);
			}
			else if (k2.k() == OracleDbType.Xml)
			{
				obj = ((OracleXml)obj).Value;
			}
			return obj;
		}
		default:
			return k2.p(n, r + l[i].l, d(i));
		}
	}

	public override int GetValues(object[] values)
	{
		AssertReaderIsOpen("GetValues");
		AssertReaderHasData();
		int num = Math.Min(values.Length, FieldCount);
		for (int num2 = 0; num2 < num; num2++)
		{
			if (a(num2) == -1)
			{
				values[num2] = DBNull.Value;
				continue;
			}
			k k2 = z[num2];
			k2.a(o);
			switch (k2.k())
			{
			case OracleDbType.BFile:
			{
				object obj2 = ((ae != ReadLobMode.Value) ? new OracleBFile(this.m_b, this, num2, k2.a(n, r + l[num2].l, A_2: true, A_3: false)) : k2.p(n, r + l[num2].l, d(num2)));
				values[num2] = obj2;
				break;
			}
			case OracleDbType.Blob:
			case OracleDbType.Clob:
			case OracleDbType.NClob:
			{
				object obj3 = null;
				if (p != null && p[num2] != null)
				{
					obj3 = p[num2];
				}
				if (obj3 is OracleLob oracleLob)
				{
					values[num2] = oracleLob.Value;
					break;
				}
				if (k2.k() == OracleDbType.Blob)
				{
					if (obj3 is byte[])
					{
						values[num2] = obj3 as byte[];
						break;
					}
				}
				else if (obj3 is string)
				{
					values[num2] = obj3 as string;
					break;
				}
				obj3 = (values[num2] = ((ae != ReadLobMode.Value) ? new OracleLob(this.m_b, this, num2, k2.a(n, r + l[num2].l, A_2: true, A_3: false), k2.k()) : k2.p(n, r + l[num2].l, d(num2))));
				if (p != null)
				{
					p[num2] = obj3;
				}
				break;
			}
			case OracleDbType.Ref:
			{
				object obj4;
				if (p != null && p[num2] != null)
				{
					obj4 = p[num2];
				}
				else
				{
					obj4 = k2.b(n, r + l[num2].l, A_2: true);
					if (p != null)
					{
						p[num2] = obj4;
					}
				}
				if (obj4 != null)
				{
					obj4 = obj4.ToString();
				}
				values[num2] = obj4;
				break;
			}
			case OracleDbType.Array:
			case OracleDbType.Cursor:
			case OracleDbType.Object:
			case OracleDbType.Table:
			case OracleDbType.Xml:
			{
				object obj = ((p != null && p[num2] != null) ? p[num2] : ((k2.k() != OracleDbType.Cursor) ? k2.e(n, r + l[num2].l, A_2: true) : k2.t(n, r + l[num2].l, d(num2))));
				if (p != null)
				{
					p[num2] = obj;
				}
				if (k2.k() == OracleDbType.Cursor)
				{
					obj = new OracleDataReader((s)obj, new s[1] { (s)obj }, this.m_b, base.CommandBehavior, i, this.m_e, g + 1, af);
				}
				if ((k2.k() == OracleDbType.Array || k2.k() == OracleDbType.Table) && !ObjectView)
				{
					obj = new OracleArrayDataReader((OracleArray)obj);
				}
				if (k2.k() == OracleDbType.Xml)
				{
					obj = ((OracleXml)obj).Value;
				}
				values[num2] = obj;
				break;
			}
			default:
				values[num2] = k2.p(n, r + l[num2].l, d(num2));
				break;
			}
		}
		return num;
	}

	public OracleType GetObjectType(int i)
	{
		c(i);
		return z[i].j();
	}

	public OracleType GetObjectType(string name)
	{
		return GetObjectType(GetOrdinal(name));
	}

	public OracleBFile GetOracleBFile(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return OracleBFile.Null;
		}
		if (p != null && p[i] != null)
		{
			return (OracleBFile)p[i];
		}
		OracleBFile oracleBFile = new OracleBFile(this.m_b, this, i, z[i].a(n, r + l[i].l, A_2: true, A_3: false));
		if (p != null)
		{
			p[i] = oracleBFile;
		}
		return oracleBFile;
	}

	public OracleBFile GetOracleBFile(string name)
	{
		return GetOracleBFile(GetOrdinal(name));
	}

	public OracleCursor GetOracleCursor(int i)
	{
		b(i);
		OracleCursor oracleCursor;
		if (a(i) == -1)
		{
			oracleCursor = OracleCursor.Null;
		}
		else if (p != null && p[i] != null)
		{
			oracleCursor = (OracleCursor)p[i];
		}
		else
		{
			oracleCursor = new OracleCursor(this.m_b, z[i].a(n, r + l[i].l, A_2: true), FetchSize);
			if (p != null)
			{
				p[i] = oracleCursor;
			}
		}
		oracleCursor.Depts = g + 1;
		return oracleCursor;
	}

	public OracleCursor GetOracleCursor(string name)
	{
		return GetOracleCursor(GetOrdinal(name));
	}

	internal OracleCursor f()
	{
		return new OracleCursor(this.m_b, this, this.m_d, af);
	}

	public OracleBinary GetOracleBinary(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return OracleBinary.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.j(n, r + l[i].l, d(i));
	}

	public OracleBinary GetOracleBinary(string name)
	{
		return GetOracleBinary(GetOrdinal(name));
	}

	public OracleDate GetOracleDate(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return OracleDate.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.s(n, r + l[i].l, d(i));
	}

	public OracleDate GetOracleDate(string name)
	{
		return GetOracleDate(GetOrdinal(name));
	}

	[Obsolete("This method is designed for compatibility with OracleClient only.")]
	public OracleDateTime GetOracleDateTime(int i)
	{
		OracleUtils.a("GetOracleDateTime");
		b(i);
		if (a(i) == -1)
		{
			return OracleDateTime.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return new OracleDateTime(k2.v(n, r + l[i].l, d(i)));
	}

	[Obsolete("This method is designed for compatibility with OracleClient only.")]
	public OracleTimeSpan GetOracleTimeSpan(int i)
	{
		OracleUtils.a("GetOracleTimeSpan");
		b(i);
		if (a(i) == -1)
		{
			return OracleTimeSpan.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return new OracleTimeSpan(k2.u(n, r + l[i].l, d(i)));
	}

	[Obsolete("This method is designed for compatibility with OracleClient only.")]
	public OracleMonthSpan GetOracleMonthSpan(int i)
	{
		OracleUtils.a("GetOracleMonthSpan");
		b(i);
		if (a(i) == -1)
		{
			return OracleMonthSpan.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return new OracleMonthSpan(k2.r(n, r + l[i].l, d(i)));
	}

	public OracleTimeStamp GetOracleTimeStamp(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return OracleTimeStamp.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.v(n, r + l[i].l, d(i));
	}

	public OracleTimeStamp GetOracleTimeStamp(string name)
	{
		return GetOracleTimeStamp(GetOrdinal(name));
	}

	public OracleIntervalDS GetOracleIntervalDS(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return OracleIntervalDS.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.u(n, r + l[i].l, d(i));
	}

	public OracleIntervalDS GetOracleIntervalDS(string name)
	{
		return GetOracleIntervalDS(GetOrdinal(name));
	}

	public OracleIntervalYM GetOracleIntervalYM(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return OracleIntervalYM.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.r(n, r + l[i].l, d(i));
	}

	public OracleIntervalYM GetOracleIntervalYM(string name)
	{
		return GetOracleIntervalYM(GetOrdinal(name));
	}

	public OracleLob GetOracleLob(int i)
	{
		b(i);
		k k2 = z[i];
		k2.a(o);
		if (a(i) == -1)
		{
			OracleDbType oracleDbType = e(i);
			switch (oracleDbType)
			{
			case OracleDbType.BFile:
				return OracleBFile.Null;
			case OracleDbType.Blob:
			case OracleDbType.Clob:
			case OracleDbType.NClob:
				return OracleUtils.b(oracleDbType);
			default:
				return null;
			}
		}
		if (p != null && p[i] != null)
		{
			return (OracleLob)p[i];
		}
		OracleLob oracleLob = new OracleLob(this.m_b, this, i, k2.a(n, r + l[i].l, A_2: true, A_3: false), k2.k());
		if (p != null)
		{
			p[i] = oracleLob;
		}
		return oracleLob;
	}

	public OracleLob GetOracleLob(string name)
	{
		return GetOracleLob(GetOrdinal(name));
	}

	public OracleNumber GetOracleNumber(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return OracleNumber.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.l(n, r + l[i].l, d(i));
	}

	public OracleNumber GetOracleNumber(string name)
	{
		return GetOracleNumber(GetOrdinal(name));
	}

	public OracleObject GetOracleObject(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		if (p != null && p[i] != null)
		{
			return (OracleObject)p[i];
		}
		k k2 = z[i];
		k2.a(o);
		OracleObject oracleObject = (OracleObject)k2.e(n, r + l[i].l, A_2: true);
		if (p != null)
		{
			p[i] = oracleObject;
		}
		return oracleObject;
	}

	public OracleObject GetOracleObject(string name)
	{
		return GetOracleObject(GetOrdinal(name));
	}

	public OracleArray GetOracleArray(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		if (p != null && p[i] != null)
		{
			return (OracleArray)p[i];
		}
		k k2 = z[i];
		k2.a(o);
		OracleArray oracleArray = (OracleArray)z[i].e(n, r + l[i].l, A_2: true);
		if (p != null)
		{
			p[i] = oracleArray;
		}
		return oracleArray;
	}

	public OracleArray GetOracleArray(string name)
	{
		return GetOracleArray(GetOrdinal(name));
	}

	public OracleTable GetOracleTable(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		if (p != null && p[i] != null)
		{
			return (OracleTable)p[i];
		}
		k k2 = z[i];
		k2.a(o);
		OracleTable oracleTable = (OracleTable)z[i].e(n, r + l[i].l, A_2: true);
		if (p != null)
		{
			p[i] = oracleTable;
		}
		return oracleTable;
	}

	public OracleTable GetOracleTable(string name)
	{
		return GetOracleTable(GetOrdinal(name));
	}

	public OracleRef GetOracleRef(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		if (p != null && p[i] != null)
		{
			return (OracleRef)p[i];
		}
		k k2 = z[i];
		k2.a(o);
		OracleRef oracleRef = k2.b(n, r + l[i].l, A_2: true);
		if (p != null)
		{
			p[i] = oracleRef;
		}
		return oracleRef;
	}

	public OracleRef GetOracleRef(string name)
	{
		return GetOracleRef(GetOrdinal(name));
	}

	public NativeOracleObject GetNativeOracleObject(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.c(n, r + l[i].l, A_2: true);
	}

	public NativeOracleObject GetNativeOracleObject(string name)
	{
		return GetNativeOracleObject(GetOrdinal(name));
	}

	public NativeOracleArray GetNativeOracleArray(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.d(n, r + l[i].l, A_2: true);
	}

	public NativeOracleArray GetNativeOracleArray(string name)
	{
		return GetNativeOracleArray(GetOrdinal(name));
	}

	public NativeOracleTable GetNativeOracleTable(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		k k2 = z[i];
		k2.a(o);
		return (NativeOracleTable)k2.d(n, r + l[i].l, A_2: true);
	}

	public NativeOracleTable GetNativeOracleTable(string name)
	{
		return GetNativeOracleTable(GetOrdinal(name));
	}

	public OracleString GetOracleString(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return OracleString.Null;
		}
		k k2 = z[i];
		k2.a(o);
		return k2.k(n, r + l[i].l, d(i));
	}

	public OracleString GetOracleString(string name)
	{
		return GetOracleString(GetOrdinal(name));
	}

	public OracleXml GetOracleXml(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		if (p != null && p[i] != null)
		{
			return (OracleXml)p[i];
		}
		k k2 = z[i];
		k2.a(o);
		OracleXml oracleXml = (OracleXml)k2.e(n, r + l[i].l, A_2: true);
		if (p != null)
		{
			p[i] = oracleXml;
		}
		return oracleXml;
	}

	public OracleXml GetOracleXml(string name)
	{
		return GetOracleXml(GetOrdinal(name));
	}

	public OracleAnyData GetOracleAnyData(int i)
	{
		b(i);
		if (a(i) == -1)
		{
			return null;
		}
		if (p != null && p[i] != null)
		{
			return (OracleAnyData)p[i];
		}
		OracleAnyData oracleAnyData = (OracleAnyData)z[i].e(n, r + l[i].l, A_2: true);
		if (p != null)
		{
			p[i] = oracleAnyData;
		}
		return oracleAnyData;
	}

	public OracleAnyData GetOracleAnyData(string name)
	{
		return GetOracleAnyData(GetOrdinal(name));
	}

	public object GetOracleValue(int i)
	{
		b(i);
		k k2 = z[i];
		k2.a(o);
		if (a(i) == -1)
		{
			return DBNull.Value;
		}
		switch (k2.k())
		{
		case OracleDbType.Array:
		case OracleDbType.Object:
		case OracleDbType.Table:
		case OracleDbType.Xml:
		case OracleDbType.AnyData:
		{
			object obj3 = ((p == null || p[i] == null) ? k2.e(n, r + l[i].l, A_2: true) : p[i]);
			if (p != null)
			{
				p[i] = obj3;
			}
			return obj3;
		}
		case OracleDbType.BFile:
		{
			if (p != null && p[i] != null)
			{
				return p[i];
			}
			object obj5 = new OracleBFile(this.m_b, this, i, k2.a(n, r + l[i].l, A_2: true, A_3: false));
			if (p != null)
			{
				p[i] = obj5;
			}
			return obj5;
		}
		case OracleDbType.Blob:
		case OracleDbType.Clob:
		case OracleDbType.NClob:
		{
			if (p != null && p[i] is OracleLob)
			{
				return p[i];
			}
			object obj4 = new OracleLob(this.m_b, this, i, k2.a(n, r + l[i].l, A_2: true, A_3: false), k2.k());
			if (p != null)
			{
				p[i] = obj4;
			}
			return obj4;
		}
		case OracleDbType.Cursor:
		{
			if (p != null && p[i] != null)
			{
				return p[i];
			}
			object obj = new OracleCursor(this.m_b, k2.a(n, r + l[i].l, A_2: true), FetchSize);
			if (p != null)
			{
				p[i] = obj;
			}
			return obj;
		}
		case OracleDbType.Ref:
		{
			if (p != null && p[i] != null)
			{
				return p[i];
			}
			object obj2 = k2.b(n, r + l[i].l, A_2: true);
			if (obj2 is OracleRef { IsNull: not false })
			{
				return DBNull.Value;
			}
			if (p != null)
			{
				p[i] = obj2;
			}
			return obj2;
		}
		case OracleDbType.TimeStamp:
		case OracleDbType.TimeStampLTZ:
		case OracleDbType.TimeStampTZ:
		{
			if (OracleUtils.OracleClientCompatible)
			{
				return new OracleDateTime(k2.v(n, r + l[i].l, d(i)));
			}
			OracleTimeStamp oracleTimeStamp = k2.v(n, r + l[i].l, d(i));
			return oracleTimeStamp;
		}
		case OracleDbType.IntervalDS:
		{
			if (OracleUtils.OracleClientCompatible)
			{
				return new OracleTimeSpan(k2.u(n, r + l[i].l, d(i)));
			}
			OracleIntervalDS oracleIntervalDS = (OracleIntervalDS)k2.t(n, r + l[i].l, d(i));
			oracleIntervalDS.a(l[i].e, l[i].f);
			return oracleIntervalDS;
		}
		case OracleDbType.IntervalYM:
			if (OracleUtils.OracleClientCompatible)
			{
				return new OracleMonthSpan(k2.r(n, r + l[i].l, d(i)));
			}
			return k2.t(n, r + l[i].l, d(i));
		default:
			return k2.t(n, r + l[i].l, d(i));
		}
	}

	public object GetOracleValue(string name)
	{
		return GetOracleValue(GetOrdinal(name));
	}

	public int GetOracleValues(object[] values)
	{
		int num = Math.Min(values.Length, FieldCount);
		for (int num2 = 0; num2 < num; num2++)
		{
			k k2 = z[num2];
			switch (k2.k())
			{
			case OracleDbType.Blob:
			case OracleDbType.Clob:
			case OracleDbType.NClob:
			{
				if (p != null && p[num2] is OracleLob)
				{
					values[num2] = p[num2];
					break;
				}
				object oracleValue2 = GetOracleValue(num2);
				if (p != null)
				{
					p[num2] = oracleValue2;
				}
				values[num2] = oracleValue2;
				break;
			}
			case OracleDbType.Array:
			case OracleDbType.BFile:
			case OracleDbType.Cursor:
			case OracleDbType.Object:
			case OracleDbType.Ref:
			case OracleDbType.Table:
			case OracleDbType.Xml:
			{
				if (p != null && p[num2] != null)
				{
					values[num2] = p[num2];
					break;
				}
				object oracleValue = GetOracleValue(num2);
				if (p != null)
				{
					p[num2] = oracleValue;
				}
				values[num2] = oracleValue;
				break;
			}
			default:
				values[num2] = GetOracleValue(num2);
				break;
			}
		}
		return num;
	}

	internal int d(int A_0)
	{
		h h2 = l[A_0];
		if (h2.o != 0)
		{
			int num = Devart.Common.e.h(n, r + h2.o);
			if (this.m_d.v().a())
			{
				switch (h2.c)
				{
				case 1:
				case 5:
				case 8:
				case 9:
				case 11:
				case 96:
				case 97:
				case 104:
					num *= 2;
					break;
				}
			}
			return num;
		}
		return -1;
	}

	internal void a(CommandBehavior A_0)
	{
		SetCommandBehavior(A_0);
	}

	public override Type GetProviderSpecificFieldType(int i)
	{
		c(i);
		return z[i].h();
	}

	public override object GetProviderSpecificValue(int i)
	{
		return GetOracleValue(i);
	}

	public override int GetProviderSpecificValues(object[] values)
	{
		return GetOracleValues(values);
	}

	public override DateTimeOffset GetDateTimeOffset(int i)
	{
		OracleTimeStamp oracleTimeStamp = GetOracleTimeStamp(i);
		return new DateTimeOffset(oracleTimeStamp.Value, oracleTimeStamp.TimeZoneOffset);
	}
}
