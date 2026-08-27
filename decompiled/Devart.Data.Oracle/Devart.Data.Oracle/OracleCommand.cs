using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

[Devart.Common.i("OracleCommand_Description")]
[DesignTimeVisible(true)]
[ToolboxItem(true)]
public class OracleCommand : DbCommandBase, ICloneable
{
	private OracleConnection m_a;

	private OracleParameterCollection m_b;

	private bool m_c = true;

	private int m_d;

	protected int iters;

	private bool m_e;

	private long[] f;

	private string m_g;

	private byte[] m_h;

	private Hashtable i;

	private e j;

	private bool k;

	private bool m_l;

	private string m;

	private bool n;

	private bool o;

	private string p;

	private bool q;

	private OracleNumberMappingCollection r = ac.a;

	private bool s;

	private bool t;

	internal OracleDataReader DataReaderInternal => (OracleDataReader)base.DataReader;

	internal bool IsPreparedInternal => base.IsPrepared;

	[DefaultValue("")]
	[Category("Data")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Devart.Common.i("DbCommand_CommandText")]
	public override string CommandText
	{
		get
		{
			return base.CommandText;
		}
		set
		{
			if (base.CommandText != value)
			{
				j = null;
			}
			base.CommandText = value;
		}
	}

	[Devart.Common.i("DbCommand_CommandType")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Category("Data")]
	[DefaultValue(CommandType.Text)]
	public override CommandType CommandType
	{
		get
		{
			return base.CommandType;
		}
		set
		{
			base.CommandType = value;
		}
	}

	[Devart.Common.i("DbCommand_Connection")]
	[MergableProperty(false)]
	[Category("Behavior")]
	[DefaultValue(null)]
	public new OracleConnection Connection
	{
		get
		{
			return this.m_a;
		}
		set
		{
			if (this.m_a != value)
			{
				PropertyChanging();
				this.m_a = value;
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[MergableProperty(false)]
	[Category("Data")]
	[Devart.Common.i("DbCommand_Parameters")]
	public new OracleParameterCollection Parameters
	{
		get
		{
			if (this.m_b == null)
			{
				this.m_b = new OracleParameterCollection(this);
			}
			return this.m_b;
		}
	}

	[Browsable(false)]
	[DefaultValue(null)]
	public new OracleTransaction Transaction
	{
		get
		{
			if (this.m_a == null)
			{
				return null;
			}
			return this.m_a.b;
		}
		set
		{
		}
	}

	[Category("Misc")]
	[Devart.Common.i("OracleCommand_Cached")]
	[DefaultValue(true)]
	public bool Cached
	{
		get
		{
			return this.m_c;
		}
		set
		{
			this.m_c = value;
		}
	}

	[Category("Misc")]
	[DefaultValue(0)]
	[Devart.Common.i("OracleCommand_FetchSize")]
	public int FetchSize
	{
		get
		{
			return this.m_d;
		}
		set
		{
			this.m_d = value;
		}
	}

	internal bool SaveRowId
	{
		get
		{
			return this.m_l;
		}
		set
		{
			this.m_l = value;
		}
	}

	protected override DbConnection DbConnection
	{
		get
		{
			return this.m_a;
		}
		set
		{
			this.m_a = (OracleConnection)value;
		}
	}

	protected override DbParameterCollection DbParameterCollection => Parameters;

	protected override DbTransaction DbTransaction
	{
		get
		{
			if (this.m_a == null)
			{
				return null;
			}
			return this.m_a.b;
		}
		set
		{
		}
	}

	internal bool ReturnProviderSpecificTypes
	{
		get
		{
			return n;
		}
		set
		{
			n = value;
		}
	}

	[DefaultValue(false)]
	[Devart.Common.i("OracleCommand_PassParametersByName")]
	[Category("Misc")]
	public bool PassParametersByName
	{
		get
		{
			if (this.m_a != null && this.m_a.PassParametersByName)
			{
				return true;
			}
			return o;
		}
		set
		{
			o = value;
		}
	}

	private bool IsPassParametersByName
	{
		get
		{
			if (!PassParametersByName)
			{
				return OracleUtils.OracleClientCompatible;
			}
			return true;
		}
	}

	internal OracleNumberMappingCollection NumberMappings
	{
		get
		{
			if (r != ac.a)
			{
				return r;
			}
			if (this.m_a != null && this.m_a.NumberMappings != null && this.m_a.NumberMappings.Count > 0)
			{
				return this.m_a.NumberMappings;
			}
			return null;
		}
		set
		{
			r = value;
		}
	}

	protected override ILocalFailoverManager LocalFailoverManager => this.m_a.LocalFailoverManager;

	[Category("Data")]
	[Devart.Common.i("DbCommand_CommandTimeout")]
	public override int CommandTimeout
	{
		get
		{
			if (!k && Connection != null && Connection.ConnectionOptions != null)
			{
				return Connection.ConnectionOptions.y();
			}
			return base.CommandTimeout;
		}
		set
		{
			if (base.CommandTimeout != value)
			{
				base.CommandTimeout = value;
				k = true;
			}
		}
	}

	[Browsable(false)]
	public string TableValuedResultType
	{
		get
		{
			return p;
		}
		set
		{
			p = value;
			q = !string.IsNullOrEmpty(p);
		}
	}

	public OracleCommand()
	{
		base.CommandTimeout = 0;
		s = false;
	}

	private bool e()
	{
		if (!s)
		{
			return Connection != null;
		}
		return true;
	}

	public OracleCommand(string commandText)
		: this()
	{
		CommandText = commandText;
	}

	public OracleCommand(string commandText, OracleConnection connection)
		: this()
	{
		CommandText = commandText;
		Connection = connection;
	}

	public OracleCommand(string commandText, OracleConnection connection, OracleTransaction transaction)
		: this()
	{
		CommandText = commandText;
		Connection = connection;
		Transaction = transaction;
	}

	public OracleCommand(string commandText, OracleTransaction transaction)
		: this()
	{
		CommandText = commandText;
		Connection = transaction.Connection;
		Transaction = transaction;
	}

	protected override void Dispose(bool disposing)
	{
		if (this.m_b != null)
		{
			this.m_b.SetParent(null);
		}
		base.Dispose(disposing);
	}

	public override int ExecuteNonQuery()
	{
		t = true;
		try
		{
			using IDataReader dataReader = ExecuteDbDataReader(CommandBehavior.Default, nonQuery: true);
			((OracleDataReader)dataReader).CloseCursors = false;
			dataReader.Close();
			return dataReader.RecordsAffected;
		}
		finally
		{
			t = false;
		}
	}

	private bool d()
	{
		if (CommandType != CommandType.StoredProcedure)
		{
			return false;
		}
		OracleParameterCollection parameters = Parameters;
		if (parameters == null)
		{
			return true;
		}
		int count = parameters.Count;
		for (int num = 0; num < count; num++)
		{
			OracleParameter oracleParameter = parameters[num];
			if (oracleParameter.Direction != ParameterDirection.Input && oracleParameter.OracleDbType == OracleDbType.Cursor)
			{
				return false;
			}
		}
		return true;
	}

	public OracleCursor ExecuteCursor()
	{
		OracleDataReader oracleDataReader = ExecuteReader();
		OracleCursor oracleCursor = oracleDataReader.f();
		oracleCursor.a(null);
		return oracleCursor;
	}

	public new OracleDataReader ExecuteReader()
	{
		return (OracleDataReader)base.ExecuteReader(CommandBehavior.Default);
	}

	public new OracleDataReader ExecuteReader(CommandBehavior behavior)
	{
		return (OracleDataReader)base.ExecuteReader(behavior);
	}

	public new OracleDataReader ExecutePageReader(CommandBehavior behavior, int startRecord, int maxRecords)
	{
		return (OracleDataReader)base.ExecutePageReader(behavior, startRecord, maxRecords);
	}

	protected override DbDataReader ExecutePageReaderInternal(CommandBehavior behavior, int startRecord, int maxRecords)
	{
		OracleDataReader oracleDataReader = (OracleDataReader)base.ExecutePageReaderInternal(behavior, startRecord, maxRecords);
		oracleDataReader.SchemaSql = base.Sql;
		return oracleDataReader;
	}

	public int ExecuteArray(int iters)
	{
		long[] A_;
		return a(iters, A_1: false, out A_);
	}

	public int ExecuteArray(int iters, out long[] rowsAffected)
	{
		return a(iters, A_1: true, out rowsAffected);
	}

	private int a(int A_0, bool A_1, out long[] A_2)
	{
		if (A_0 < 1)
		{
			throw new ArgumentException(Devart.Common.al.a("ItersGreater1"));
		}
		if (A_0 >= 65535)
		{
			throw new ArgumentException(Devart.Common.al.a("ItersLess65535"));
		}
		try
		{
			this.m_e = A_1;
			iters = A_0;
			int result = ExecuteNonQuery();
			A_2 = f;
			return result;
		}
		finally
		{
			iters = 0;
			this.m_e = false;
			f = null;
		}
	}

	public new OracleDataReader EndExecuteReader(IAsyncResult result)
	{
		return (OracleDataReader)base.EndExecuteReader(result);
	}

	public string GetRowId()
	{
		if (m != null)
		{
			return m;
		}
		if (base.Stmt != null)
		{
			return ((s)base.Stmt).b();
		}
		return string.Empty;
	}

	public override void Cancel()
	{
		if (this.m_a != null && this.m_a.State == ConnectionState.Open)
		{
			g g2 = this.m_a.d().l();
			g2.f();
		}
	}

	protected override void AddCommand()
	{
		Connection.b(this);
	}

	protected override void RemoveCommand()
	{
		Connection.a(this);
	}

	protected override void AddDataReader(DbDataReader reader)
	{
		Connection.a(reader);
	}

	internal void g()
	{
		Unprepare();
	}

	public new OracleParameter CreateParameter()
	{
		return new OracleParameter();
	}

	protected override void Unprepare()
	{
		base.Unprepare();
		this.m_g = null;
	}

	private string a(string A_0, int A_1)
	{
		if (A_1 == 0)
		{
			return A_0;
		}
		if (this.m_g != null)
		{
			return this.m_g + A_1;
		}
		OracleDataReader oracleDataReader = ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
		try
		{
			DataTable schemaTable = oracleDataReader.GetSchemaTable();
			StringBuilder stringBuilder = new StringBuilder();
			DataColumn column = schemaTable.Columns["ColumnName"];
			stringBuilder.Append("SELECT ");
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			for (int num = 0; num < schemaTable.Rows.Count; num++)
			{
				DataRow dataRow = schemaTable.Rows[num];
				if (num > 0)
				{
					stringBuilder2.Append(", ");
					stringBuilder3.Append(", ");
				}
				string text = $"\"{dataRow[column]}\"";
				string text2 = "F" + num;
				stringBuilder2.Append(text + ' ' + text2);
				stringBuilder3.Append(text2 + ' ' + text);
			}
			stringBuilder.Append((object?)stringBuilder3);
			stringBuilder.Append(" FROM (SELECT ");
			stringBuilder.Append((object?)stringBuilder2);
			stringBuilder.Append(", ROWNUM DOTCONNECT_QUERY_ROWNUM FROM (\n");
			stringBuilder.Append(A_0);
			stringBuilder.Append("\n) Q1 ) Q WHERE Q.DOTCONNECT_QUERY_ROWNUM > ");
			this.m_g = stringBuilder.ToString();
			stringBuilder.Append(A_1);
			return stringBuilder.ToString();
		}
		finally
		{
			oracleDataReader.Close();
		}
	}

	protected override string CreateStoredProcSql(string procName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		g g2 = this.m_a.d().l();
		if (procName.IndexOfAny(new char[2] { '.', ':' }) >= 0)
		{
			if (string.Compare(g2.v(), "09") >= 0)
			{
				int num = procName.IndexOf(".");
				if (num >= 0 && procName.Substring(0, num).ToUpper(CultureInfo.InvariantCulture) == g2.x().ToUpper(CultureInfo.InvariantCulture))
				{
					procName = procName.Substring(num + 1);
				}
			}
			int num2 = procName.IndexOf(':');
			if (num2 != -1)
			{
				stringBuilder.Append(procName.Substring(0, num2));
			}
			else
			{
				stringBuilder.Append(procName);
			}
		}
		else
		{
			stringBuilder.Append(procName);
		}
		OracleParameter[] array = new OracleParameter[(this.m_b != null) ? this.m_b.Count : 0];
		if (this.m_b != null)
		{
			this.m_b.CopyTo(array, 0);
		}
		bool flag = base.ParameterCheck;
		bool flag2 = flag;
		if (!t && !flag && d())
		{
			base.ParameterCheck = true;
			flag = true;
		}
		try
		{
			if (flag)
			{
				DescribeProcedure(CommandText);
				if (this.m_b != null)
				{
					if (!flag2 && d())
					{
						this.m_b.Clear();
						this.m_b.AddRange(array);
					}
					else
					{
						foreach (OracleParameter oracleParameter in array)
						{
							if (this.m_b.IndexOf(oracleParameter.ParameterName) >= 0)
							{
								this.m_b.Add(oracleParameter);
							}
							else
							{
								if (oracleParameter.Direction != ParameterDirection.ReturnValue)
								{
									continue;
								}
								int num4 = 0;
								OracleParameter[] array2 = array;
								foreach (OracleParameter oracleParameter2 in array2)
								{
									if (oracleParameter2.Direction == ParameterDirection.ReturnValue)
									{
										num4++;
									}
								}
								if (num4 != 1)
								{
									this.m_b.Add(oracleParameter);
									continue;
								}
								OracleParameter oracleParameter3 = null;
								foreach (OracleParameter item in this.m_b)
								{
									if (item.Direction == ParameterDirection.ReturnValue)
									{
										if (oracleParameter3 != null)
										{
											oracleParameter3 = null;
											break;
										}
										oracleParameter3 = item;
									}
								}
								if (oracleParameter3 == null)
								{
									this.m_b.Add(oracleParameter);
									continue;
								}
								string parameterName = oracleParameter.ParameterName;
								oracleParameter.ParameterName = oracleParameter3.ParameterName;
								this.m_b.Add(oracleParameter);
								oracleParameter.ParameterName = parameterName;
							}
						}
						array = new OracleParameter[this.m_b.Count];
						this.m_b.CopyTo(array, 0);
					}
				}
			}
			if (array.Length == 0)
			{
				if (q)
				{
					stringBuilder2.Append("SELECT * FROM TABLE(").Append(stringBuilder.ToString()).Append(")");
				}
				else
				{
					stringBuilder2.Append("BEGIN\n  ").Append(stringBuilder.ToString()).Append(";\nEND;");
				}
				return stringBuilder2.ToString().Replace('\r', ' ');
			}
			bool flag3 = flag;
			bool flag4 = false;
			foreach (OracleParameter oracleParameter5 in array)
			{
				if (!flag4 && oracleParameter5.Direction == ParameterDirection.ReturnValue)
				{
					if (!Utils.IsEmpty(oracleParameter5.RecordTypeName))
					{
						string text = "r" + Math.Abs(oracleParameter5.d.GetHashCode());
						stringBuilder.Insert(0, text + " := ");
					}
					else if (oracleParameter5.OracleDbType != OracleDbType.Boolean)
					{
						stringBuilder.Insert(0, ":" + oracleParameter5.ParameterName + " := ");
					}
					else
					{
						stringBuilder.Insert(0, oracleParameter5.ParameterName + " := ");
					}
					flag4 = true;
				}
				if (!flag3 && oracleParameter5.ParameterName.IndexOf('$') != -1 && Utils.IsEmpty(oracleParameter5.RecordTypeName))
				{
					flag3 = true;
				}
			}
			StringBuilder stringBuilder3 = null;
			StringBuilder stringBuilder4 = null;
			StringBuilder stringBuilder5 = null;
			ArrayList arrayList = null;
			bool flag5 = false;
			if (!flag && flag3)
			{
				DescribeProcedure(CommandText);
				if (this.m_b != null)
				{
					foreach (OracleParameter oracleParameter6 in array)
					{
						int num8 = this.m_b.IndexOf(oracleParameter6.ParameterName);
						if (num8 >= 0)
						{
							this.m_b[num8].Value = oracleParameter6.Value;
						}
					}
					this.m_b.CopyTo(array, 0);
				}
			}
			ArrayList a_ = new ArrayList();
			OracleParameter[] array3 = array;
			foreach (OracleParameter oracleParameter7 in array3)
			{
				string text2 = OracleUtils.a(oracleParameter7.ParameterName, a_);
				if (text2 != oracleParameter7.ParameterName)
				{
					oracleParameter7.UsedParameterName = text2;
				}
				if (oracleParameter7.OracleDbType == OracleDbType.Boolean)
				{
					if (q)
					{
						throw new InvalidOperationException("BOOLEAN parameter type is not supported with Table valued functions.");
					}
					if (stringBuilder3 == null)
					{
						stringBuilder3 = new StringBuilder();
					}
					stringBuilder3.Append("  ").Append(text2);
					stringBuilder3.Append(" ").Append("BOOLEAN").Append(";\n");
					if (oracleParameter7.Direction == ParameterDirection.ReturnValue || oracleParameter7.Direction == ParameterDirection.Output || oracleParameter7.Direction == ParameterDirection.InputOutput)
					{
						if (stringBuilder5 == null)
						{
							stringBuilder5 = new StringBuilder();
						}
						stringBuilder5.Append("  ").Append(':').Append(text2);
						stringBuilder5.Append(" := sys.diutil.bool_to_int(");
						stringBuilder5.Append(text2);
						stringBuilder5.Append(");\n");
					}
					if (oracleParameter7.Direction == ParameterDirection.Input || oracleParameter7.Direction == ParameterDirection.InputOutput)
					{
						if (stringBuilder4 == null)
						{
							stringBuilder4 = new StringBuilder();
						}
						stringBuilder4.Append("  ").Append(text2);
						stringBuilder4.Append(" := sys.diutil.int_to_bool(:");
						stringBuilder4.Append(text2);
						stringBuilder4.Append(");\n");
					}
					if (oracleParameter7.Direction != ParameterDirection.ReturnValue)
					{
						if (!flag5)
						{
							stringBuilder.Append("(");
							flag5 = true;
						}
						else
						{
							stringBuilder.Append(", ");
						}
						if (IsPassParametersByName)
						{
							stringBuilder.Append(oracleParameter7.ParameterName);
							stringBuilder.Append(" => ");
						}
						stringBuilder.Append(text2);
					}
				}
				else if (!Utils.IsEmpty(oracleParameter7.RecordTypeName))
				{
					if (q)
					{
						throw new InvalidOperationException("RECORD parameter type is not supported with Table valued functions.");
					}
					string text3 = "r" + Math.Abs(oracleParameter7.d.GetHashCode());
					if (arrayList == null || !arrayList.Contains(oracleParameter7.d))
					{
						if (stringBuilder3 == null)
						{
							stringBuilder3 = new StringBuilder();
						}
						stringBuilder3.Append("  ").Append(text3).Append(" ");
						stringBuilder3.Append(oracleParameter7.d).Append(";\n");
						if (oracleParameter7.Direction != ParameterDirection.ReturnValue)
						{
							if (!flag5)
							{
								stringBuilder.Append("(");
								flag5 = true;
							}
							else
							{
								stringBuilder.Append(", ");
							}
							if (IsPassParametersByName)
							{
								stringBuilder.Append(oracleParameter7.ParameterName);
								stringBuilder.Append(" => ");
							}
							stringBuilder.Append(text3);
						}
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(oracleParameter7.d);
					}
					if (oracleParameter7.Direction == ParameterDirection.ReturnValue || oracleParameter7.Direction == ParameterDirection.Output || oracleParameter7.Direction == ParameterDirection.InputOutput)
					{
						if (stringBuilder5 == null)
						{
							stringBuilder5 = new StringBuilder();
						}
						stringBuilder5.Append("  :");
						stringBuilder5.Append(text2);
						stringBuilder5.Append(" := ");
						stringBuilder5.Append(text3 + ".");
						stringBuilder5.Append(oracleParameter7.ParameterName.Split(new char[1] { '$' }).GetValue(1));
						stringBuilder5.Append(";\n");
					}
					if (oracleParameter7.Direction == ParameterDirection.Input || oracleParameter7.Direction == ParameterDirection.InputOutput)
					{
						if (stringBuilder4 == null)
						{
							stringBuilder4 = new StringBuilder();
						}
						stringBuilder4.Append("  " + text3 + ".");
						stringBuilder4.Append(oracleParameter7.ParameterName.Split(new char[1] { '$' }).GetValue(1));
						stringBuilder4.Append(" := :");
						stringBuilder4.Append(text2);
						stringBuilder4.Append(";\n");
					}
				}
				else if (oracleParameter7.Direction != ParameterDirection.ReturnValue)
				{
					if (!flag5)
					{
						stringBuilder.Append("(");
						flag5 = true;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					if (IsPassParametersByName)
					{
						stringBuilder.Append(oracleParameter7.ParameterName);
						stringBuilder.Append(" => ");
					}
					stringBuilder.Append(':');
					stringBuilder.Append(text2);
				}
			}
			if (flag5)
			{
				stringBuilder.Append(")");
			}
			if (!q)
			{
				stringBuilder.Append(';');
			}
			if (stringBuilder3 != null)
			{
				stringBuilder2.Append("DECLARE\n");
				stringBuilder2.Append(stringBuilder3.ToString());
			}
			if (q)
			{
				stringBuilder2.Append("SELECT * FROM TABLE(");
			}
			else
			{
				stringBuilder2.Append("BEGIN\n");
			}
			if (stringBuilder4 != null)
			{
				stringBuilder2.Append(stringBuilder4.ToString());
			}
			stringBuilder2.Append("  ").Append(stringBuilder.ToString()).Append('\n');
			if (stringBuilder5 != null)
			{
				stringBuilder2.Append(stringBuilder5.ToString());
			}
			if (q)
			{
				stringBuilder2.Append(")");
			}
			else
			{
				stringBuilder2.Append("END;");
			}
		}
		finally
		{
			base.ParameterCheck = flag2;
		}
		return stringBuilder2.ToString();
	}

	protected override void ParseSqlParameters(string sql)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		int num = 0;
		int num2 = 0;
		int num3 = sql?.Length ?? 0;
		OracleParameterCollection oracleParameterCollection = null;
		string text = "";
		bool flag5 = true;
		int num4 = 0;
		bool flag6 = false;
		StringBuilder stringBuilder = new StringBuilder();
		for (; num < num3; num++)
		{
			char c2 = sql[num];
			switch (c2)
			{
			case ' ':
			case '(':
			case ')':
			case ',':
				flag6 = false;
				if (flag5)
				{
					if (stringBuilder.ToString().ToUpper(CultureInfo.InvariantCulture) == "CREATE")
					{
						num4 |= 1;
					}
					if (stringBuilder.ToString().ToUpper(CultureInfo.InvariantCulture) == "TRIGGER")
					{
						num4 |= 2;
					}
					if (num4 == 3)
					{
						flag5 = false;
					}
					stringBuilder.Remove(0, stringBuilder.Length);
				}
				break;
			case 'C':
			case 'T':
			case 'c':
			case 't':
				if (flag5 && !flag6)
				{
					flag6 = true;
					stringBuilder.Remove(0, stringBuilder.Length);
				}
				if (flag5 && flag6)
				{
					stringBuilder.Append(c2);
				}
				break;
			case '\t':
			case '\n':
				flag6 = false;
				flag = false;
				break;
			case '-':
				flag6 = false;
				if (!flag2 && !flag3 && !flag4 && num + 1 < sql.Length && sql[num + 1] == '-')
				{
					flag = true;
				}
				break;
			case '/':
				flag6 = false;
				if (!flag && !flag3 && !flag4 && num + 1 < sql.Length && sql[num + 1] == '*')
				{
					flag2 = true;
				}
				break;
			case '\'':
				flag6 = false;
				if (!flag && !flag2 && !flag4)
				{
					flag3 = !flag3;
				}
				break;
			case '"':
				flag6 = false;
				if (!flag && !flag2 && !flag3)
				{
					flag4 = !flag4;
				}
				break;
			case '*':
				flag6 = false;
				if (!flag && !flag3 && !flag4 && flag2 && num + 1 < sql.Length && sql[num + 1] == '/')
				{
					flag2 = false;
				}
				break;
			case ':':
			{
				if (num4 == 3)
				{
					break;
				}
				flag6 = false;
				if (flag || flag2 || flag3 || flag4)
				{
					break;
				}
				num2 = ++num;
				bool flag7 = false;
				if (num < num3 && sql[num] == '"')
				{
					flag7 = true;
					num++;
				}
				while (num < num3)
				{
					c2 = sql[num].ToString().ToUpper(CultureInfo.InvariantCulture)[0];
					if (flag7)
					{
						if (c2 == '"')
						{
							num++;
							break;
						}
						num++;
					}
					else
					{
						if (('0' > c2 || c2 > '9') && ('A' > c2 || c2 > 'Z') && c2 != '_' && c2 != '#')
						{
							break;
						}
						num++;
					}
				}
				if (num <= num2)
				{
					break;
				}
				text = sql.Substring(num2, num - num2);
				if (oracleParameterCollection == null)
				{
					oracleParameterCollection = new OracleParameterCollection(null);
				}
				if (oracleParameterCollection.IndexOf(text) == -1)
				{
					int num5 = Parameters.IndexOf(text);
					OracleParameter oracleParameter;
					if (num5 == -1)
					{
						oracleParameter = CreateParameter();
						oracleParameter.ParameterName = text;
					}
					else
					{
						oracleParameter = (OracleParameter)this.m_b[num5].Clone();
					}
					oracleParameterCollection.Add(oracleParameter);
				}
				break;
			}
			default:
				if (flag5 && flag6)
				{
					stringBuilder.Append(c2);
				}
				break;
			}
		}
		if (this.m_b != null)
		{
			this.m_b.Clear();
		}
		if (oracleParameterCollection != null)
		{
			while (oracleParameterCollection.Count > 0)
			{
				OracleParameter oracleParameter = oracleParameterCollection[0];
				oracleParameterCollection.RemoveAt(0);
				Parameters.Add(oracleParameter);
			}
		}
	}

	protected override void ClearParameters()
	{
		if (this.m_b != null)
		{
			this.m_b.Clear();
		}
	}

	private h[] a(s A_0, int A_1, OracleParameterCollection A_2, g A_3, out bool A_4)
	{
		int count = A_2.Count;
		h[] array;
		if (A_0.x() != null && !A_3.b())
		{
			array = A_0.x();
			this.m_h = A_0.ae();
			i = A_0.s();
		}
		else
		{
			array = new h[count];
			int num = 0;
			for (int num2 = 0; num2 < A_2.Count; num2++)
			{
				OracleParameter oracleParameter = A_2[num2];
				num = OracleParameter.a(oracleParameter.Value, ref array[num2], oracleParameter.OracleDbType, oracleParameter.AssignedSize, oracleParameter.Direction, num, A_1, A_3);
				array[num2].a = oracleParameter.UsedParameterName;
				array[num2].b = num2;
			}
			this.m_h = new byte[num];
			i = new Hashtable();
		}
		A_4 = true;
		bool a_ = A_3.h().d() >= 8010000;
		for (int num3 = 0; num3 < array.Length; num3++)
		{
			OracleParameter oracleParameter = ((array[num3].b == -1) ? A_2[num3] : A_2[array[num3].b]);
			oracleParameter.a(ref array[num3], a_, Connection, this.m_h, i, A_3, out var A_5, A_1);
			if (!A_5)
			{
				A_4 = false;
			}
		}
		return array;
	}

	private void a(OracleParameterCollection A_0, h[] A_1, aq A_2, g A_3)
	{
		_ = A_0.Count;
		int num = A_2.d();
		bool a_ = num >= 8010000;
		bool a_2 = A_2.a();
		int num2 = 0;
		foreach (OracleParameter item in A_0)
		{
			item.a(ref A_1[num2], a_, a_2, this, this.m_h, i, A_3);
			num2++;
		}
		this.m_h = null;
	}

	public object Clone()
	{
		OracleCommand oracleCommand = new OracleCommand();
		oracleCommand.PassParametersByName = PassParametersByName;
		oracleCommand.CommandTimeout = CommandTimeout;
		oracleCommand.CommandText = CommandText;
		oracleCommand.CommandType = CommandType;
		oracleCommand.Connection = Connection;
		foreach (OracleParameter parameter in Parameters)
		{
			oracleCommand.Parameters.Add(parameter.Clone());
		}
		oracleCommand.ParameterCheck = base.ParameterCheck;
		oracleCommand.UpdatedRowSource = UpdatedRowSource;
		return oracleCommand;
	}

	internal void h()
	{
		CreateParameters();
	}

	protected override IDisposable InternalPrepare(bool implicitPrepare, int startRecord, int maxRecords)
	{
		s s2 = null;
		try
		{
			g g2 = this.m_a.d().l();
			s2 = g2.h().a(g2, A_1: false);
			s2.e(CommandTimeout);
			s2.a(this.m_c);
			string a_ = a(base.Sql, startRecord);
			s2.a(a_);
			return s2;
		}
		catch (Exception ex)
		{
			ap ap2 = Connection.d();
			if (ex is OracleException a_2)
			{
				this.m_a.a(this, a_2);
			}
			ap2.a(ex);
			throw;
		}
	}

	private void a(string A_0, OracleInfoMessageEventArgs A_1)
	{
		Lexer lexer = new Lexer(A_0, an.al, an.am, LexerBehavior.OmitBlank | LexerBehavior.OmitComment | LexerBehavior.UpperedIdent);
		lexer.CultureInfo = CultureInfo.InvariantCulture;
		string text = null;
		Token nextToken = lexer.GetNextToken();
		bool flag = true;
		int num = 0;
		if (nextToken.Id == 2026)
		{
			num = 2026;
			nextToken = lexer.GetNextToken();
			if (nextToken.ToString().ToUpper() == "OR")
			{
				nextToken = lexer.GetNextToken();
				if (nextToken.Id == 3015)
				{
					nextToken = lexer.GetNextToken();
				}
				else
				{
					flag = false;
				}
			}
		}
		else if (nextToken.Id == 2025)
		{
			num = 2025;
			nextToken = lexer.GetNextToken();
		}
		else
		{
			flag = false;
		}
		OracleError[] array = null;
		if (flag)
		{
			string text2 = nextToken.ToString().ToUpper(CultureInfo.InvariantCulture);
			if (nextToken.Id == 3010 || nextToken.Id == 3011 || nextToken.Id == 3013 || nextToken.Id == 3012 || text2 == "TYPE" || text2 == "VIEW")
			{
				if (num == 2025)
				{
					if (nextToken.Id == 3012)
					{
						text = "in ('PACKAGE', 'PACKAGE BODY')";
					}
					else if (nextToken.Id == 3017)
					{
						text = "in ('TYPE', 'TYPE BODY')";
					}
				}
				nextToken = lexer.GetNextToken();
				string text3 = nextToken.ToString();
				if (nextToken.Id == 3016 && (text2 == "PACKAGE" || text2 == "TYPE"))
				{
					text2 = text2 + " " + text3;
					text3 = lexer.GetNextToken().ToString();
				}
				string text4 = text3;
				string text5 = "";
				text3 = lexer.GetNextToken().ToString();
				if (text3 == ".")
				{
					text5 = text4;
					text4 = lexer.GetNextToken().ToString();
				}
				OracleCommand oracleCommand = new OracleCommand();
				oracleCommand.Connection = this.m_a;
				if (text5 != "")
				{
					oracleCommand.CommandText = string.Format("SELECT Line,Position,Text,Type,owner FROM {0} WHERE Owner = :Owner and Name = :Name and Type {1} ORDER BY Sequence", (this.m_a.ConnectMode == OracleConnectMode.SysDba) ? "DBA_ERRORS" : "ALL_ERRORS", (text != null) ? text : "= :Type");
					oracleCommand.Parameters.Add("Owner", OracleDbType.VarChar).Value = text5;
				}
				else
				{
					oracleCommand.CommandText = string.Format("SELECT Line,Position,Text,Type,user FROM User_Errors WHERE Name = :Name and Type {0} ORDER BY Sequence", (text != null) ? text : "= :Type");
				}
				oracleCommand.Parameters.Add("Name", OracleDbType.VarChar).Value = text4;
				if (text == null)
				{
					oracleCommand.Parameters.Add("Type", OracleDbType.VarChar).Value = text2.ToUpper(CultureInfo.InvariantCulture);
				}
				IDataReader dataReader = oracleCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList(10);
				while (dataReader.Read())
				{
					int a_ = (int)(decimal)dataReader.GetValue(0);
					int a_2 = (int)(decimal)dataReader.GetValue(1);
					string text6 = (string)dataReader.GetValue(2);
					text2 = (string)dataReader.GetValue(3);
					text5 = (string)dataReader.GetValue(4);
					while (text6 != null)
					{
						int length = text6.Length;
						if (length <= 0 || (text6[length - 1] != '\n' && text6[length - 1] != '\r'))
						{
							break;
						}
						text6 = text6.Substring(0, length - 1);
					}
					OracleError oracleError = new OracleError(0, 0, text6, text2 switch
					{
						"CONSUMER GROUP" => OracleObjectType.ConsumerGroup, 
						"EVALUATION CONTEXT" => OracleObjectType.EvaluationContext, 
						"FUNCTION" => OracleObjectType.Function, 
						"INDEX" => OracleObjectType.Index, 
						"LIBRARY" => OracleObjectType.Library, 
						"LOB" => OracleObjectType.Lob, 
						"OPERATOR" => OracleObjectType.Operator, 
						"PACKAGE" => OracleObjectType.Package, 
						"PACKAGE BODY" => OracleObjectType.PackageBody, 
						"PROCEDURE" => OracleObjectType.Procedure, 
						"SYNONYM" => OracleObjectType.Synonym, 
						"TABLE" => OracleObjectType.Table, 
						"TRIGGER" => OracleObjectType.Trigger, 
						"TYPE" => OracleObjectType.Type, 
						"TYPE BODY" => OracleObjectType.TypeBody, 
						"VIEW" => OracleObjectType.View, 
						_ => OracleObjectType.Unknown, 
					}, text4, text5);
					oracleError.a(a_, a_2);
					arrayList.Add(oracleError);
				}
				array = (OracleError[])arrayList.ToArray(typeof(OracleError));
			}
		}
		OracleErrorCollection a_3 = ((array != null) ? new OracleErrorCollection(array) : null);
		A_1.a(a_3);
	}

	protected override DbDataReader InternalExecute(CommandBehavior behavior, IDisposable disposable, int startRecord, int maxRecords)
	{
		return InternalExecute(behavior, disposable, startRecord, maxRecords, nonQuery: false);
	}

	protected override DbDataReader InternalExecute(CommandBehavior behavior, IDisposable disposable, int startRecord, int maxRecords, bool nonQuery)
	{
		s s2 = (s)disposable;
		OracleDataReader oracleDataReader = null;
		try
		{
			if (this.m_a.d().Transaction == null && this.m_a.d().LastTransaction != null)
			{
				throw new InvalidOperationException(Devart.Common.al.a("TransactionNotDisposed"));
			}
			s2.e(CommandTimeout);
			g g2 = s2.aa();
			aq aq2 = g2.h();
			int num = iters;
			int num2 = aq2.d();
			OracleParameterCollection oracleParameterCollection = this.m_b;
			bool A_ = true;
			s2.e();
			h[] array = null;
			int num3;
			if (oracleParameterCollection != null)
			{
				num3 = oracleParameterCollection.Count;
				if (num3 > 0)
				{
					array = a(s2, num, oracleParameterCollection, g2, out A_);
					if (s2.x() != array)
					{
						s2.b(array, this.m_h, i);
					}
				}
			}
			else
			{
				num3 = 0;
			}
			if (s2.f() != a9.b)
			{
				if (num == 0)
				{
					num = 1;
				}
			}
			else if (num2 >= 9000000)
			{
				s2.a(this.m_d);
			}
			az az2 = az.a;
			if ((behavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default && s2.f() != a9.i && s2.f() != a9.j)
			{
				az2 = az.e;
			}
			if (this.m_a.b == null && this.m_a.d().Transaction == null && this.m_a.AutoCommit && s2.f() != a9.b)
			{
				if (A_)
				{
					az2 |= az.f;
				}
				A_ = !A_;
			}
			else
			{
				A_ = false;
			}
			if (num > 1 && num2 >= 8010000)
			{
				az2 |= az.h;
				if (this.m_e)
				{
					if (g2.b() || g2.h().d() < 12010000 || string.Compare(g2.v(), "12.01.00.01") < 0)
					{
						this.m_e = false;
					}
					else
					{
						az2 |= az.k;
					}
				}
			}
			bool flag = s2.f() == a9.b && j != null && FetchSize > 0;
			s[] array2 = null;
			if (flag)
			{
				array2 = new s[1] { s2 };
				oracleDataReader = new OracleDataReader(s2, array2, this.m_a, behavior, this.m_d, maxRecords, 0, NumberMappings, j, nonQuery);
				if (!nonQuery)
				{
					num = oracleDataReader.e();
					oracleDataReader.k();
				}
				else
				{
					num = 0;
				}
			}
			bool flag2 = s2.a(num, az2);
			OracleException ex = null;
			if (s2.ab())
			{
				OracleInfoMessageEventArgs e2 = ((!(s2.t() == "24439")) ? new OracleInfoMessageEventArgs(s2.t(), 24344, "Devart.Data.Oracle") : new OracleInfoMessageEventArgs("", 24439, "Devart.Data.Oracle"));
				OracleInfoMessageEventHandler oracleInfoMessageEventHandler = this.m_a.i();
				if (oracleInfoMessageEventHandler != null)
				{
					a(CommandText, e2);
					if (e2.Code != 24439 || (e2.Errors != null && e2.Errors.Count > 0))
					{
						oracleInfoMessageEventHandler(this, e2);
					}
				}
				if (e2.Code != 24439 || (e2.Errors != null && e2.Errors.Count > 0))
				{
					ex = new OracleException(e2.Code, e2.Message);
				}
			}
			else if (s2.ad() && s2.aa().h() is aa aa2)
			{
				string a_ = aa2.a(out var A_2, out var _);
				if (A_2 == 1403)
				{
					OracleInfoMessageEventArgs e3 = new OracleInfoMessageEventArgs(a_, A_2, "Devart.Data.Oracle");
					this.m_a.i()?.Invoke(this, e3);
				}
			}
			if (num3 > 0)
			{
				a(oracleParameterCollection, array, aq2, g2);
			}
			if (this.m_l)
			{
				m = GetRowId();
			}
			if (A_)
			{
				g2.l();
			}
			if (this.m_e)
			{
				f = s2.d();
			}
			if (s2.f() == a9.b)
			{
				if (!flag)
				{
					array2 = new s[1] { s2 };
					oracleDataReader = new OracleDataReader(s2, array2, this.m_a, behavior, this.m_d, maxRecords, NumberMappings, nonQuery);
					j = oracleDataReader.DescribeInformation;
				}
				else
				{
					oracleDataReader.a(s2.j(), !flag2);
				}
			}
			else
			{
				List<s> list = new List<s>();
				if (oracleParameterCollection != null)
				{
					foreach (OracleParameter item in oracleParameterCollection)
					{
						if (item.OracleDbType == OracleDbType.Cursor && item.Value is OracleCursor oracleCursor)
						{
							list.Add(oracleCursor.Stmt);
							if ((behavior & CommandBehavior.SingleResult) != CommandBehavior.Default)
							{
								break;
							}
						}
					}
				}
				s[] array3 = s2.c();
				if (array3 != null)
				{
					list.AddRange(array3);
				}
				if (list.Count == 0)
				{
					array2 = new s[1] { s2 };
				}
				else
				{
					array2 = new s[list.Count];
					list.CopyTo(array2, 0);
				}
				oracleDataReader = new OracleDataReader(s2, array2, this.m_a, behavior, this.m_d, maxRecords, NumberMappings, nonQuery);
			}
			s2.w();
			if (array2[0] != s2)
			{
				s[] array4 = array2;
				foreach (s s3 in array4)
				{
					s3.w();
				}
			}
			if (ex != null)
			{
				this.m_a.a(this, ex);
			}
			if (this.m_a.State == ConnectionState.Broken || this.m_a.State == ConnectionState.Closed)
			{
				oracleDataReader.Close();
			}
			return oracleDataReader;
		}
		catch (OracleException ex2)
		{
			s2.a(ex2);
			if (ex2.Errors == null || ex2.Errors.Count == 1)
			{
				Unprepare();
			}
			oracleDataReader?.Dispose();
			this.m_a.a(this, ex2);
			try
			{
				ap ap2 = this.m_a.d();
				ap2.a(ex2);
			}
			catch (Exception)
			{
			}
			throw;
		}
		finally
		{
			c();
		}
	}

	private void c()
	{
		if (this.m_b == null)
		{
			return;
		}
		foreach (OracleParameter item in this.m_b)
		{
			item.a();
		}
	}

	protected override void DescribeProcedure(string name)
	{
		Utils.CheckConnectionOpen(this.m_a);
		ap ap2 = this.m_a.d();
		g g2 = ap2.l();
		using (LocalFailoverManager.StartUse())
		{
			while (true)
			{
				try
				{
					int a_ = 0;
					int num = name.IndexOf(':');
					if (num != -1)
					{
						string text = name.Substring(num + 1, name.Length - num - 1);
						a_ = int.Parse(text.Trim());
						name = name.Substring(0, num).Trim();
					}
					h[] array = g2.a(name, a_);
					int num2 = ((array != null) ? array.Length : 0);
					List<string> list = new List<string>((this.m_b != null) ? this.m_b.Count : 0);
					if (this.m_b != null)
					{
						foreach (OracleParameter item in this.m_b)
						{
							list.Add(item.ParameterName.ToUpper(CultureInfo.InvariantCulture));
						}
						this.m_b.Clear();
					}
					q = false;
					for (int num3 = 0; num3 < num2; num3++)
					{
						h h2 = array[num3];
						if (IsPassParametersByName && h2.j && h2.k == 0 && !list.Contains(h2.a))
						{
							continue;
						}
						if (num3 == 0 && h2.k == 3 && a(name))
						{
							q = true;
							p = h2.u;
							continue;
						}
						OracleParameter oracleParameter2 = new OracleParameter();
						if (this.m_b == null)
						{
							this.m_b = new OracleParameterCollection(this);
						}
						switch (h2.k)
						{
						case 0:
							oracleParameter2.Direction = ParameterDirection.Input;
							break;
						case 1:
							oracleParameter2.Direction = ParameterDirection.Output;
							break;
						case 2:
							oracleParameter2.Direction = ParameterDirection.InputOutput;
							break;
						case 3:
							oracleParameter2.Direction = ParameterDirection.ReturnValue;
							break;
						}
						oracleParameter2.ParameterName = h2.a;
						oracleParameter2.ObjectTypeName = h2.u;
						k k2 = g2.b(A_3: (h2.u == null) ? null : OracleType.GetObjectType(h2.u, this.m_a), A_0: h2.c, A_1: h2.h, A_2: null);
						oracleParameter2.OracleDbType = k2.k();
						if (h2.t != null)
						{
							oracleParameter2.RecordTypeName = h2.t;
						}
						oracleParameter2.ArrayLength = h2.aa;
						oracleParameter2.Scale = (byte)h2.f;
						oracleParameter2.Size = h2.d;
						oracleParameter2.Precision = (byte)h2.e;
						this.m_b.Add(oracleParameter2);
					}
					break;
				}
				catch (Exception ex)
				{
					if (LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Execute, RetryMode.Reexecute, ex) == RetryMode.Raise)
					{
						throw;
					}
				}
			}
		}
	}

	private bool a(string A_0)
	{
		if (string.Compare(this.m_a.d().ServerVersionNormalized, "09") < 0)
		{
			return false;
		}
		A_0 = OracleUtils.QuotedSQLName(A_0);
		int num = A_0.IndexOf('.');
		string text;
		string text2;
		string text3;
		if (num < 0 || num == A_0.Length - 1)
		{
			text = OracleUtils.UnQuote(A_0);
			text2 = null;
			text3 = null;
		}
		else
		{
			text = OracleUtils.UnQuote(A_0.Substring(num + 1));
			text2 = null;
			text3 = OracleUtils.UnQuote(A_0.Substring(0, num));
			int num2 = text.IndexOf('.');
			if (num2 >= 0 && num2 != text.Length - 1)
			{
				text2 = OracleUtils.UnQuote(text.Substring(0, num2));
				text = OracleUtils.UnQuote(text.Substring(num2 + 1));
			}
		}
		if (!a(text, text2, text3, out var A_1) && text2 == null && text3 != null)
		{
			text2 = text3;
			text3 = null;
			a(text, text2, text3, out A_1);
		}
		return A_1;
	}

	private bool a(string A_0, string A_1, string A_2, out bool A_3)
	{
		OracleCommand oracleCommand;
		if (A_2 == null)
		{
			if (A_1 == null)
			{
				oracleCommand = new OracleCommand("select PIPELINED from sys.user_procedures where object_name = :procname and procedure_name is null", this.m_a);
				OracleParameter oracleParameter = new OracleParameter("procname", OracleDbType.VarChar);
				oracleParameter.Value = A_0;
				oracleCommand.Parameters.Add(oracleParameter);
			}
			else
			{
				oracleCommand = new OracleCommand("select PIPELINED from sys.user_procedures where object_name = :packname and procedure_name = :procname", this.m_a);
				OracleParameter oracleParameter2 = new OracleParameter("packname", OracleDbType.VarChar);
				oracleParameter2.Value = A_1;
				oracleCommand.Parameters.Add(oracleParameter2);
				oracleParameter2 = new OracleParameter("procname", OracleDbType.VarChar);
				oracleParameter2.Value = A_0;
				oracleCommand.Parameters.Add(oracleParameter2);
			}
		}
		else if (A_1 == null)
		{
			oracleCommand = new OracleCommand("select PIPELINED from sys.all_procedures where owner = :schemaname and object_name = :procname and procedure_name is null", this.m_a);
			OracleParameter oracleParameter3 = new OracleParameter("schemaname", OracleDbType.VarChar);
			oracleParameter3.Value = A_2;
			oracleCommand.Parameters.Add(oracleParameter3);
			oracleParameter3 = new OracleParameter("procname", OracleDbType.VarChar);
			oracleParameter3.Value = A_0;
			oracleCommand.Parameters.Add(oracleParameter3);
		}
		else
		{
			oracleCommand = new OracleCommand("select PIPELINED from sys.all_procedures where owner = :schemaname and object_name = :packname and procedure_name = :procname", this.m_a);
			OracleParameter oracleParameter4 = new OracleParameter("schemaname", OracleDbType.VarChar);
			oracleParameter4.Value = A_2;
			oracleCommand.Parameters.Add(oracleParameter4);
			oracleParameter4 = new OracleParameter("packname", OracleDbType.VarChar);
			oracleParameter4.Value = A_1;
			oracleCommand.Parameters.Add(oracleParameter4);
			oracleParameter4 = new OracleParameter("procname", OracleDbType.VarChar);
			oracleParameter4.Value = A_0;
			oracleCommand.Parameters.Add(oracleParameter4);
		}
		using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader())
		{
			if (oracleDataReader.Read())
			{
				A_3 = oracleDataReader.GetString(0) == "YES";
				return true;
			}
		}
		A_3 = false;
		return false;
	}

	internal void a(OracleParameterCollection A_0)
	{
		if (A_0 != null)
		{
			if (A_0.a() != null)
			{
				throw new ArgumentException("Cannot use parameters collection together with another OracleCommand", "parameters");
			}
			A_0.SetParent(this);
		}
		this.m_b = A_0;
	}

	internal bool l()
	{
		if (this.m_b != null)
		{
			return this.m_b.Count > 0;
		}
		return false;
	}

	private void b()
	{
		if (this.m_b != null)
		{
			this.m_b.Clear();
		}
	}

	protected override DbParameter CreateDbParameter()
	{
		DbParameter result = CreateParameter();
		SaveParameter(result);
		return result;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public override void ResetCommandTimeout()
	{
		base.CommandTimeout = 0;
		k = false;
	}

	protected override bool ShouldSerializeCommandTimeout()
	{
		return base.CommandTimeout != 0;
	}

	protected override bool IsReadOnlyOperation(IDisposable stmt)
	{
		return ((s)stmt).f() == a9.b;
	}
}
