using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;
using Devart.Common;

namespace Devart.Data.Oracle;

internal abstract class g
{
	private class a
	{
		private int m_a;

		private int b;

		private Type c;

		private OracleType d;

		public a(int A_0, int A_1, Type A_2, OracleType A_3)
		{
			this.m_a = A_0;
			b = A_1;
			c = A_2;
			d = A_3;
		}

		public virtual bool a(object A_0)
		{
			if (!(A_0 is a a10))
			{
				return false;
			}
			if (this.m_a == a10.m_a && b == a10.b && (object)c == a10.c)
			{
				return d == a10.d;
			}
			return false;
		}

		public virtual int a()
		{
			return (this.m_a << 1) ^ b ^ (((object)c != null) ? c.GetHashCode() : 0) ^ ((d != null) ? d.GetHashCode() : 0);
		}
	}

	private readonly aq m_a;

	private Hashtable m_b;

	private string m_c;

	private OracleConnectMode m_d;

	private string m_e;

	private int m_f;

	protected OracleInfoMessageEventArgs g;

	protected ay h;

	protected SortedList i;

	protected g(aq A_0)
	{
		this.m_a = A_0;
		this.i = new SortedList();
		this.m_b = new Hashtable();
	}

	public virtual void a(ay A_0, g A_1)
	{
		this.g = null;
		this.m_d = A_0.k();
		this.m_e = A_0.af();
		this.h = A_0;
	}

	public virtual void d()
	{
		this.m_c = "";
	}

	public virtual void c()
	{
		d();
	}

	public virtual void a(Guid A_0, IsolationLevel A_1)
	{
		s s2 = h().a(this, A_1: false);
		try
		{
			string a_;
			switch (A_1)
			{
			case IsolationLevel.ReadCommitted:
			case IsolationLevel.Unspecified:
				a_ = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";
				break;
			case IsolationLevel.Serializable:
				a_ = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE";
				break;
			default:
				throw new ArgumentException(string.Format(Devart.Common.al.a("TransactionIsolationLevelNotSupported"), A_1.ToString()));
			}
			s2.a(a_);
			s2.a(1, az.a);
		}
		finally
		{
			s2.l();
		}
	}

	public OracleInfoMessageEventArgs t()
	{
		OracleInfoMessageEventArgs result = this.g;
		this.g = null;
		return result;
	}

	public virtual void e()
	{
	}

	public abstract void l();

	public abstract void o();

	public abstract void f();

	public abstract void a(ay A_0, string A_1);

	public void a(string A_0)
	{
		string a_ = "ALTER SESSION SET CURRENT_SCHEMA = " + OracleUtils.QuoteIfNeed(A_0);
		s s2 = h().a(this, A_1: false);
		try
		{
			s2.a(a_);
			s2.a(1, az.a);
			this.m_e = A_0;
		}
		finally
		{
			s2.l();
		}
	}

	public virtual h[] a(string A_0, int A_1)
	{
		int num = A_0.LastIndexOf('.');
		string text3;
		string text;
		string text2;
		if (num >= 0)
		{
			text = A_0.Substring(0, num);
			text2 = A_0.Substring(num + 1, A_0.Length - num - 1);
			num = text.IndexOf('.');
			if (num >= 0)
			{
				text3 = text.Substring(0, num);
				text = text.Substring(num + 1, text.Length - num - 1);
			}
			else
			{
				text3 = "";
			}
		}
		else
		{
			text3 = "";
			text = A_0;
			text2 = "";
		}
		text3 = ((text3.Length <= 1 || text3[0] != '"') ? text3.ToUpper(CultureInfo.InvariantCulture) : text3.Substring(1, text3.Length - 2));
		text = ((text.Length <= 1 || text[0] != '"') ? text.ToUpper(CultureInfo.InvariantCulture) : text.Substring(1, text.Length - 2));
		text2 = ((text2.Length <= 1 || text2[0] != '"') ? text2.ToUpper(CultureInfo.InvariantCulture) : text2.Substring(1, text2.Length - 2));
		if (A_1 == 0)
		{
			A_1 = 1;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("declare\n  p_owner varchar(30) := :owner;\n  p_object_name varchar(30) := :object_name;\n  p_proc_name varchar(30) := :procedure_name;\n  p_overload number := :overload;\n  p_object_type varchar(19);\n  p_count number;\nbegin\n  if p_owner is null then\n    begin\n      select object_type, owner into p_object_type, p_owner\n        from sys.all_objects\n");
		if (string.Compare(v(), "08.01") >= 0)
		{
			stringBuilder.Append("        where owner = SYS_CONTEXT('USERENV', 'CURRENT_SCHEMA')  and\n");
		}
		else
		{
			stringBuilder.Append("        where owner in (select user from dual) and\n");
		}
		stringBuilder.Append("          object_name = p_object_name and\n          rownum <= 1;\n    exception\n      when no_data_found then\n      begin\n        select object_type, owner into p_object_type, p_owner\n          from sys.all_objects\n          where owner = 'PUBLIC' and\n          object_name = p_object_name and\n          rownum <= 1;\n      exception\n        when no_data_found then\n          if p_proc_name is not null then\n            p_owner := p_object_name;\n            p_object_name := p_proc_name;\n            p_proc_name := null;\n          else\n            raise;\n          end if;\n        end;\n      end;\n    end if;\n  if p_object_type is null then\n    select object_type into p_object_type\n      from sys.all_objects\n      where owner = p_owner and object_name = p_object_name and rownum <= 1;\n  end if;\n  while p_object_type = 'SYNONYM' loop\n    select table_owner, table_name\n      into p_owner, p_object_name\n      from sys.all_synonyms\n      where owner = p_owner and synonym_name = p_object_name;\n    select object_type into p_object_type\n      from sys.all_objects\n      where owner = p_owner and object_name = p_object_name and rownum <= 1;\n  end loop;\n");
		if (string.Compare(v(), "09") >= 0)
		{
			stringBuilder.Append("  select count(*)\n    into p_count\n    from sys.all_procedures\n    where owner = p_owner and object_name = p_object_name\n      and (procedure_name = p_proc_name or p_proc_name is null and procedure_name is null);\n  if p_count < p_overload then\n    raise no_data_found;\n  end if;\n");
		}
		stringBuilder.Append("  :owner := p_owner;\n  :object_name := p_object_name;\n  :procedure_name := p_proc_name;\n  select count(*)\n    into :param_count\n    from sys.all_arguments\n    where owner = p_owner\n      and (p_proc_name is null and package_name is null and object_name = p_object_name or\n        p_proc_name is not null and package_name = p_object_name and object_name = p_proc_name\n          and nvl(overload, 1) = p_overload)\n      and data_type is not null;\nend;");
		int num2 = 0;
		s s2 = h().a(this, A_1: false);
		try
		{
			s2.a(stringBuilder.ToString());
			h[] array = new h[5];
			int num3 = 0;
			array[0].a = "owner";
			array[0].c = 5;
			array[0].n = num3;
			num3 += 2;
			array[0].l = num3;
			array[0].m = 62;
			num3 += array[0].m;
			array[0].d = 30;
			array[1].a = "object_name";
			array[1].c = 5;
			array[1].n = num3;
			num3 += 2;
			array[1].l = num3;
			array[1].m = 62;
			num3 += array[1].m;
			array[1].d = 30;
			array[2].a = "procedure_name";
			array[2].c = 5;
			array[2].n = num3;
			num3 += 2;
			array[2].l = num3;
			array[2].m = 62;
			num3 += array[2].m;
			array[2].d = 30;
			array[3].a = "overload";
			array[3].c = 3;
			array[3].n = num3;
			num3 += 2;
			array[3].l = num3;
			array[3].m = 4;
			num3 += array[3].m;
			array[4].a = "param_count";
			array[4].c = 3;
			array[4].n = num3;
			num3 += 2;
			array[4].l = num3;
			array[4].m = 4;
			num3 += array[4].m;
			byte[] array2 = new byte[num3];
			Hashtable hashtable = new Hashtable();
			hashtable[array[0].l] = text3;
			hashtable[array[1].l] = text;
			hashtable[array[2].l] = text2;
			k k2 = a(array[3].c, 0, null);
			k2.a(array2, array[3].l, (object)A_1);
			array2[array[4].n] = byte.MaxValue;
			array2[array[4].n + 1] = byte.MaxValue;
			s2.b(array, array2, hashtable);
			try
			{
				s2.a(1, az.a);
			}
			catch (OracleException ex)
			{
				if (ex.Code == 1403)
				{
					throw new OracleException(4043, string.Format(Devart.Common.al.a("ObjectDoesNotExist"), A_0));
				}
				throw;
			}
			text3 = ((array2[array[0].n] != 0 || array2[array[0].n + 1] != 0) ? "" : ((string)hashtable[array[0].l]));
			text = ((array2[array[1].n] != 0 || array2[array[1].n + 1] != 0) ? null : ((string)hashtable[array[1].l]));
			text2 = ((array2[array[2].n] != 0 || array2[array[2].n + 1] != 0) ? "" : ((string)hashtable[array[2].l]));
			num2 = ((array2[array[4].n] == 0 && array2[array[4].n + 1] == 0) ? ((int)k2.p(array2, array[4].l, 0)) : 0);
		}
		finally
		{
			s2.l();
		}
		h[] array3 = null;
		if (num2 > 0)
		{
			stringBuilder = new StringBuilder();
			stringBuilder.Append("declare\n  p_owner varchar(30) := :owner;\n  p_object_name varchar(30) := :object_name;\n  p_proc_name varchar(30) := :procedure_name;\n  p_overload number := :overload;\n");
			if (string.Compare(v(), "08.01") >= 0)
			{
				stringBuilder.Append("begin\n  select argument_name, position, data_level, data_type, ");
				if (string.Compare(v(), "11") >= 0)
				{
					stringBuilder.Append("decode(defaulted, 'Y', 1, 0),\n");
				}
				else
				{
					stringBuilder.Append("0,\n");
				}
				stringBuilder.Append("    decode(in_out, 'IN', 0, 'OUT', 1, 'IN/OUT', 2),\n    data_length, data_precision, data_scale, type_owner, type_name, type_subname\n    bulk collect into :name, :position, :level, :data_type, :default, :in_out,\n      :length, :precision, :scale, :type_owner, :type_name, :type_subname\n    from sys.all_arguments\n    where owner = p_owner\n      and (p_proc_name is null and package_name is null and object_name = p_object_name or\n        p_proc_name is not null and package_name = p_object_name and object_name = p_proc_name\n          and nvl(overload, 1) = p_overload)\n      and data_type is not null\n    order by sequence;\nend;");
			}
			else
			{
				stringBuilder.Append("  p_param_index integer;\n  cursor c_params is\n    select argument_name, position, data_level, data_type, 0,\n      decode(in_out, 'IN', 0, 'OUT', 1, 'IN/OUT', 2),\n      data_length, data_precision, data_scale, type_owner, type_name, type_subname\n    from sys.all_arguments\n    where owner = p_owner\n      and (p_proc_name is null and package_name is null and object_name = p_object_name or\n        p_proc_name is not null and package_name = p_object_name and object_name = p_proc_name\n          and nvl(overload, 1) = p_overload)\n      and data_type is not null\n    order by sequence;\nbegin\n  p_param_index := 1;\n  open c_params;\n  loop\n    fetch c_params into :name(p_param_index), :position(p_param_index), :level(p_param_index),\n      :data_type(p_param_index), :default(p_param_index), :in_out(p_param_index),\n      :length(p_param_index), :precision(p_param_index), :scale(p_param_index),\n      :type_owner(p_param_index), :type_name(p_param_index), :type_subname(p_param_index);\n    exit when c_params%notfound;\n    p_param_index := p_param_index + 1;\n  end loop;\n  close c_params;\nend;\n");
			}
			s2 = h().a(this, A_1: false);
			try
			{
				s2.a(stringBuilder.ToString());
				h[] array4 = new h[16];
				int num4 = 0;
				array4[0].a = "owner";
				array4[0].c = 5;
				array4[0].n = num4;
				num4 += 2;
				array4[0].l = num4;
				array4[0].m = 62;
				num4 += array4[0].m;
				array4[0].d = 30;
				array4[1].a = "object_name";
				array4[1].c = 5;
				array4[1].n = num4;
				num4 += 2;
				array4[1].l = num4;
				array4[1].m = 62;
				num4 += array4[1].m;
				array4[1].d = 30;
				array4[2].a = "procedure_name";
				array4[2].c = 5;
				array4[2].n = num4;
				num4 += 2;
				array4[2].l = num4;
				array4[2].m = 62;
				num4 += array4[2].m;
				array4[2].d = 30;
				array4[3].a = "overload";
				array4[3].c = 3;
				array4[3].n = num4;
				num4 += 2;
				array4[3].l = num4;
				array4[3].m = 4;
				num4 += array4[3].m;
				array4[4].a = "name";
				array4[4].c = 1;
				array4[4].aa = num2;
				array4[4].ab = num4;
				num4 += 4;
				array4[4].o = num4;
				array4[4].r = 2;
				num4 += 2 * num2;
				array4[4].n = num4;
				num4 += 2 * num2;
				array4[4].l = num4;
				if (this.m_a.a())
				{
					array4[4].m = 60;
				}
				else
				{
					array4[4].m = 30;
				}
				array4[4].p = array4[4].m;
				num4 += array4[4].m * num2;
				array4[4].d = 30;
				array4[5].a = "position";
				array4[5].c = 3;
				array4[5].aa = num2;
				array4[5].ab = num4;
				num4 += 4;
				array4[5].n = num4;
				num4 += 2 * num2;
				array4[5].l = num4;
				array4[5].m = 2;
				array4[5].p = 2;
				num4 += array4[5].m * num2;
				array4[6].a = "level";
				array4[6].c = 3;
				array4[6].aa = num2;
				array4[6].ab = num4;
				num4 += 4;
				array4[6].n = num4;
				num4 += 2 * num2;
				array4[6].l = num4;
				array4[6].m = 2;
				array4[6].p = 2;
				num4 += array4[6].m * num2;
				array4[7].a = "data_type";
				array4[7].c = 1;
				array4[7].aa = num2;
				array4[7].ab = num4;
				num4 += 4;
				array4[7].o = num4;
				array4[7].r = 2;
				num4 += 2 * num2;
				array4[7].n = num4;
				num4 += 2 * num2;
				array4[7].l = num4;
				if (this.m_a.a())
				{
					array4[7].m = 60;
				}
				else
				{
					array4[7].m = 30;
				}
				array4[7].p = array4[7].m;
				num4 += array4[7].m * num2;
				array4[7].d = 30;
				array4[8].a = "default";
				array4[8].c = 3;
				array4[8].aa = num2;
				array4[8].ab = num4;
				num4 += 4;
				array4[8].n = num4;
				num4 += 2 * num2;
				array4[8].l = num4;
				array4[8].m = 1;
				array4[8].p = 1;
				num4 += array4[8].m * num2;
				array4[9].a = "in_out";
				array4[9].c = 3;
				array4[9].aa = num2;
				array4[9].ab = num4;
				num4 += 4;
				array4[9].n = num4;
				num4 += 2 * num2;
				array4[9].l = num4;
				array4[9].m = 1;
				array4[9].p = 1;
				num4 += array4[9].m * num2;
				array4[10].a = "length";
				array4[10].c = 3;
				array4[10].aa = num2;
				array4[10].ab = num4;
				num4 += 4;
				array4[10].n = num4;
				num4 += 2 * num2;
				array4[10].l = num4;
				array4[10].m = 4;
				array4[10].p = 4;
				num4 += array4[10].m * num2;
				array4[11].a = "precision";
				array4[11].c = 3;
				array4[11].aa = num2;
				array4[11].ab = num4;
				num4 += 4;
				array4[11].n = num4;
				num4 += 2 * num2;
				array4[11].l = num4;
				array4[11].m = 2;
				array4[11].p = 2;
				num4 += array4[11].m * num2;
				array4[12].a = "scale";
				array4[12].c = 3;
				array4[12].aa = num2;
				array4[12].ab = num4;
				num4 += 4;
				array4[12].n = num4;
				num4 += 2 * num2;
				array4[12].l = num4;
				array4[12].m = 2;
				array4[12].p = 2;
				num4 += array4[12].m * num2;
				array4[13].a = "type_owner";
				array4[13].c = 1;
				array4[13].aa = num2;
				array4[13].ab = num4;
				num4 += 4;
				array4[13].o = num4;
				array4[13].r = 2;
				num4 += 2 * num2;
				array4[13].n = num4;
				num4 += 2 * num2;
				array4[13].l = num4;
				if (this.m_a.a())
				{
					array4[13].m = 60;
				}
				else
				{
					array4[13].m = 30;
				}
				array4[13].p = array4[13].m;
				num4 += array4[13].m * num2;
				array4[13].d = 30;
				array4[14].a = "type_name";
				array4[14].c = 1;
				array4[14].aa = num2;
				array4[14].ab = num4;
				num4 += 4;
				array4[14].o = num4;
				array4[14].r = 2;
				num4 += 2 * num2;
				array4[14].n = num4;
				num4 += 2 * num2;
				array4[14].l = num4;
				if (this.m_a.a())
				{
					array4[14].m = 60;
				}
				else
				{
					array4[14].m = 30;
				}
				array4[14].p = array4[14].m;
				num4 += array4[14].m * num2;
				array4[14].d = 30;
				array4[15].a = "type_subname";
				array4[15].c = 1;
				array4[15].aa = num2;
				array4[15].ab = num4;
				num4 += 4;
				array4[15].o = num4;
				array4[15].r = 2;
				num4 += 2 * num2;
				array4[15].n = num4;
				num4 += 2 * num2;
				array4[15].l = num4;
				if (this.m_a.a())
				{
					array4[15].m = 60;
				}
				else
				{
					array4[15].m = 30;
				}
				array4[15].p = array4[15].m;
				num4 += array4[15].m * num2;
				array4[15].d = 30;
				byte[] array5 = new byte[num4];
				Hashtable hashtable2 = new Hashtable();
				hashtable2[array4[0].l] = text3;
				hashtable2[array4[1].l] = text;
				hashtable2[array4[2].l] = text2;
				k k3 = a(array4[3].c, 0, null);
				k3.a(array5, array4[3].l, (object)A_1);
				s2.b(array4, array5, hashtable2);
				try
				{
					s2.a(1, az.a);
				}
				catch (OracleException ex2)
				{
					if (ex2.Code == 1403)
					{
						throw new OracleException(4043, string.Format(Devart.Common.al.a("ObjectDoesNotExist"), A_0));
					}
					throw;
				}
				int num5 = array5[array4[4].ab];
				if (num5 > 0)
				{
					h[] array6 = new h[num5];
					bool flag = false;
					int num6 = 0;
					int num7 = -1;
					bool flag2 = false;
					string text4 = "";
					string text5 = "";
					for (int num8 = 0; num8 < num5; num8++)
					{
						short num9 = Devart.Common.e.h(array5, array4[5].l + num8 * array4[5].p);
						short num10 = Devart.Common.e.h(array5, array4[6].l + num8 * array4[6].p);
						string text6 = (string)hashtable2[array4[7].l + num8 * array4[7].p];
						short num11 = Devart.Common.e.h(array5, array4[10].l + num8 * array4[10].p);
						short num12 = Devart.Common.e.h(array5, array4[11].l + num8 * array4[11].p);
						short num13 = Devart.Common.e.h(array5, array4[12].l + num8 * array4[12].p);
						if (num10 == 0 || flag2)
						{
							num7++;
							if (num9 == 0)
							{
								if (OracleUtils.OracleClientCompatible)
								{
									array6[num7].a = "RETURN_VALUE";
								}
								else
								{
									array6[num7].a = "RESULT";
								}
								array6[num7].k = 3;
							}
							else
							{
								array6[num7].a = (string)hashtable2[array4[4].l + num8 * array4[4].p];
								array6[num7].k = array5[array4[9].l + num8];
								if (num10 == 0)
								{
									array6[num7].j = array5[array4[8].l + num8] == 1;
								}
							}
						}
						_ = -1;
						if (num10 == 0 || flag || flag2)
						{
							if (num10 == 0)
							{
								flag2 = false;
							}
							switch (text6)
							{
							case "VARCHAR2":
							case "VARCHAR":
								array6[num7].c = 97;
								if (num11 > 0)
								{
									array6[num7].d = num11;
								}
								break;
							case "NVARCHAR2":
							case "NCHAR VARYING":
								array6[num7].c = 97;
								array6[num7].h = 2;
								if (num11 > 0)
								{
									array6[num7].d = num11;
								}
								break;
							case "CHAR":
								array6[num7].c = 96;
								if (num11 > 0)
								{
									array6[num7].d = num11;
								}
								break;
							case "NCHAR":
								array6[num7].c = 96;
								array6[num7].h = 2;
								if (num11 > 0)
								{
									array6[num7].d = num11;
								}
								break;
							case "NUMBER":
							case "FLOAT":
								array6[num7].c = 6;
								array6[num7].e = num12;
								array6[num7].f = num13;
								break;
							case "NATIVE INTEGER":
							case "BINARY_INTEGER":
								array6[num7].c = 3;
								break;
							case "DATE":
								array6[num7].c = 12;
								break;
							case "ROWID":
							case "UROWID":
								array6[num7].c = 5;
								array6[num7].d = 19;
								break;
							case "RAW":
								array6[num7].c = 23;
								if (num11 > 0)
								{
									array6[num7].d = num11;
								}
								break;
							case "LONG":
								array6[num7].c = 8;
								break;
							case "LONG RAW":
								array6[num7].c = 24;
								break;
							case "BINARY_FLOAT":
								array6[num7].c = 21;
								break;
							case "BINARY_DOUBLE":
								array6[num7].c = 22;
								break;
							case "REF CURSOR":
								array6[num7].c = 116;
								break;
							case "REF":
								array6[num7].c = 110;
								break;
							case "CLOB":
								array6[num7].c = 112;
								break;
							case "NCLOB":
								array6[num7].c = 112;
								array6[num7].h = 2;
								break;
							case "BLOB":
								array6[num7].c = 113;
								break;
							case "BFILE":
								array6[num7].c = 114;
								break;
							case "CFILE":
								array6[num7].c = 115;
								break;
							case "OBJECT":
							case "UNDEFINED":
								array6[num7].c = 108;
								break;
							case "TABLE":
							case "VARRAY":
								array6[num7].c = 122;
								break;
							case "TIME":
								array6[num7].c = 185;
								break;
							case "TIME WITH TIME ZONE":
								array6[num7].c = 186;
								break;
							case "TIMESTAMP":
								array6[num7].c = 187;
								break;
							case "TIMESTAMP WITH TIME ZONE":
								array6[num7].c = 188;
								break;
							case "TIMESTAMP WITH LOCAL TIME ZONE":
								array6[num7].c = 232;
								break;
							case "INTERVAL YEAR TO MONTH":
								array6[num7].c = 189;
								break;
							case "INTERVAL DAY TO SECOND":
								array6[num7].c = 190;
								break;
							case "PL/SQL RECORD":
								if (flag)
								{
									throw new InvalidOperationException(Devart.Common.al.a("TableOfRecordIsNotSupported"));
								}
								array6[num7].c = 250;
								break;
							case "PL/SQL TABLE":
								flag = true;
								num6 = 1;
								break;
							case "PL/SQL BOOLEAN":
								array6[num7].c = 252;
								break;
							default:
								throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType") + " [" + text6 + "]");
							}
							if (flag2)
							{
								array6[num7].t = text5;
								array6[num7].a = text4 + "$" + array6[num7].a;
							}
							switch (array6[num7].c)
							{
							case 108:
							case 110:
							case 122:
							case 250:
							{
								string text7 = (string)hashtable2[array4[13].l + num8 * array4[13].p];
								string text8 = (string)hashtable2[array4[14].l + num8 * array4[14].p];
								string text9 = (string)hashtable2[array4[15].l + num8 * array4[15].p];
								if (array6[num7].c == 250)
								{
									if (string.IsNullOrEmpty(text8))
									{
										throw new UnsupportedTypeException(Devart.Common.al.a("CannotReceiveRecordType"));
									}
									flag2 = true;
									text4 = array6[num7].a;
									text5 = text7 + '.' + text8 + '.' + text9;
									num7--;
								}
								else if (text9 != null && text9 != "")
								{
									array6[num7].u = text7 + '.' + text8 + '.' + text9;
								}
								else
								{
									array6[num7].u = text7 + '.' + text8;
								}
								break;
							}
							}
						}
						if (num10 > 0 && flag)
						{
							array6[num7].aa = num6;
							flag = false;
							num6 = 0;
						}
					}
					if (num7 != -1)
					{
						array3 = new h[num7 + 1];
						for (int num14 = 0; num14 <= num7; num14++)
						{
							ref h reference = ref array3[num14];
							reference = array6[num14];
						}
					}
				}
			}
			finally
			{
				s2.l();
			}
		}
		return array3;
	}

	public k a(int A_0, int A_1, Type A_2)
	{
		return b(A_0, A_1, A_2, null);
	}

	public k b(int A_0, int A_1, Type A_2, OracleType A_3)
	{
		a key = new a(A_0, A_1, A_2, A_3);
		lock (this.m_b.SyncRoot)
		{
			k k2 = (k)this.m_b[key];
			if (k2 == null)
			{
				k2 = a(A_0, A_1, A_2, A_3);
				this.m_b[key] = k2;
			}
			return k2;
		}
	}

	public k b(int A_0, int A_1, Type A_2)
	{
		return a(A_0, A_1, A_2, null);
	}

	public abstract k a(int A_0, int A_1, Type A_2, OracleType A_3);

	public object b(OracleType A_0)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException("oracleType");
		}
		lock (this.i)
		{
			object obj = this.i[A_0];
			if (obj == null)
			{
				obj = a(A_0);
				A_0.a();
				this.i[A_0] = obj;
			}
			return obj;
		}
	}

	protected abstract object a(OracleType A_0);

	public virtual string a(ref string A_0)
	{
		return "";
	}

	internal abstract void a(object A_0);

	[SpecialName]
	public aq h()
	{
		return this.m_a;
	}

	[SpecialName]
	public abstract bool a();

	[SpecialName]
	public abstract int i();

	[SpecialName]
	public abstract void c(int A_0);

	[SpecialName]
	public abstract int k();

	[SpecialName]
	public abstract void b(int A_0);

	[SpecialName]
	public abstract int g();

	[SpecialName]
	public abstract void a(int A_0);

	[SpecialName]
	public abstract string m();

	[SpecialName]
	public string v()
	{
		if (this.m_c != null && this.m_c != "")
		{
			return this.m_c;
		}
		return this.m_c = OracleUtils.c(m());
	}

	[SpecialName]
	protected void b(string A_0)
	{
		this.m_c = A_0;
	}

	[SpecialName]
	public string x()
	{
		if (this.m_d == OracleConnectMode.SysDba || this.m_d == OracleConnectMode.SysOper)
		{
			return "SYS";
		}
		return this.m_e;
	}

	[SpecialName]
	internal int u()
	{
		if (this.m_f != 0)
		{
			return this.m_f;
		}
		h[] array = new h[1];
		byte[] array2 = new byte[100];
		s s2 = h().a(this, A_1: false);
		s2.a("begin :CharLength := Nvl(Lengthb(Chr(65536)), Nvl(Lengthb(Chr(65536)), 1)); end;");
		array[0].a = "CharLength";
		array[0].n = 0;
		array[0].c = 3;
		array[0].m = 4;
		array[0].l = 2;
		s2.b(array, array2, null);
		s2.a(1, az.a);
		return this.m_f = Devart.Common.e.g(array2, array[0].l);
	}

	[SpecialName]
	internal ay w()
	{
		return this.h;
	}

	[SpecialName]
	public abstract bool b();

	[SpecialName]
	public abstract bool j();

	[SpecialName]
	public virtual byte[] n()
	{
		return null;
	}
}
