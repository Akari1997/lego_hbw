using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Devart.Common;

namespace Devart.Data.Oracle;

[DesignTimeVisible(true)]
[Devart.Common.i("DbDataAdapter_Description")]
[DefaultEvent("RowUpdated")]
[ToolboxItem(true)]
public class OracleDataAdapter : Devart.Common.DbDataAdapter, IDbDataAdapter, ICloneable
{
	private OracleCommand m_a;

	private OracleCommand m_b;

	private OracleCommand m_c;

	private OracleCommand m_d;

	private DataColumn m_e;

	private bool m_f;

	private OracleDataReader g;

	private int h;

	private ab i;

	private static readonly object j = new object();

	private static readonly object k = new object();

	private static readonly object l = new object();

	private static readonly object m = new object();

	private bool n;

	private bool o;

	private bool p;

	private bool q;

	IDbCommand IDbDataAdapter.SelectCommand
	{
		get
		{
			return SelectCommand;
		}
		set
		{
			SelectCommand = (OracleCommand)value;
		}
	}

	[Devart.Common.i("DbDataAdapter_SelectCommand")]
	[MergableProperty(false)]
	[Category("Fill")]
	public new OracleCommand SelectCommand
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
				base.SelectCommand = value;
			}
		}
	}

	[Browsable(false)]
	[MergableProperty(false)]
	public new DataTableMappingCollection TableMappings => base.TableMappings;

	IDbCommand IDbDataAdapter.InsertCommand
	{
		get
		{
			return InsertCommand;
		}
		set
		{
			InsertCommand = (OracleCommand)value;
		}
	}

	[Category("Update")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Devart.Common.i("DbDataAdapter_InsertCommand")]
	[MergableProperty(false)]
	public new OracleCommand InsertCommand
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
				base.InsertCommand = value;
			}
		}
	}

	IDbCommand IDbDataAdapter.UpdateCommand
	{
		get
		{
			return UpdateCommand;
		}
		set
		{
			UpdateCommand = (OracleCommand)value;
		}
	}

	[RefreshProperties(RefreshProperties.Repaint)]
	[MergableProperty(false)]
	[Category("Update")]
	[Devart.Common.i("DbDataAdapter_UpdateCommand")]
	public new OracleCommand UpdateCommand
	{
		get
		{
			return this.m_c;
		}
		set
		{
			if (this.m_c != value)
			{
				this.m_c = value;
				base.UpdateCommand = value;
			}
		}
	}

	IDbCommand IDbDataAdapter.DeleteCommand
	{
		get
		{
			return DeleteCommand;
		}
		set
		{
			DeleteCommand = (OracleCommand)value;
		}
	}

	[Category("Update")]
	[MergableProperty(false)]
	[Devart.Common.i("DbDataAdapter_DeleteCommand")]
	[RefreshProperties(RefreshProperties.Repaint)]
	public new OracleCommand DeleteCommand
	{
		get
		{
			return this.m_d;
		}
		set
		{
			if (this.m_d != value)
			{
				this.m_d = value;
				base.DeleteCommand = value;
			}
		}
	}

	[DefaultValue(1)]
	public override int UpdateBatchSize
	{
		get
		{
			return h;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			h = value;
		}
	}

	[Devart.Common.i("DbDataAdapter_RowUpdating")]
	public event OracleRowUpdatingEventHandler RowUpdating
	{
		add
		{
			base.Events.AddHandler(k, value);
		}
		remove
		{
			base.Events.RemoveHandler(k, value);
		}
	}

	[Devart.Common.i("DbDataAdapter_RowUpdated")]
	public event OracleRowUpdatedEventHandler RowUpdated
	{
		add
		{
			base.Events.AddHandler(j, value);
		}
		remove
		{
			base.Events.RemoveHandler(j, value);
		}
	}

	internal event OracleRowUpdatingEventHandler RowUpdatingInternal
	{
		add
		{
			base.Events.AddHandler(m, value);
		}
		remove
		{
			base.Events.RemoveHandler(m, value);
		}
	}

	internal event OracleRowUpdatedEventHandler RowUpdatedInternal
	{
		add
		{
			base.Events.AddHandler(l, value);
		}
		remove
		{
			base.Events.RemoveHandler(l, value);
		}
	}

	public OracleDataAdapter()
	{
		n = false;
		o = false;
		p = false;
		q = false;
		h = 1;
	}

	public OracleDataAdapter(OracleCommand selectCommand)
		: this()
	{
		SelectCommand = selectCommand;
	}

	public OracleDataAdapter(string selectCommandText, string selectConnectionString)
		: this(new OracleCommand(selectCommandText, new OracleConnection(selectConnectionString)))
	{
	}

	public OracleDataAdapter(string selectCommandText, OracleConnection selectConnection)
		: this(new OracleCommand(selectCommandText, selectConnection))
	{
	}

	private bool f()
	{
		if (!n)
		{
			return SelectCommand != null;
		}
		return true;
	}

	private bool e()
	{
		if (!o)
		{
			return InsertCommand != null;
		}
		return true;
	}

	private bool d()
	{
		if (!p)
		{
			return UpdateCommand != null;
		}
		return true;
	}

	private bool c()
	{
		if (!q)
		{
			return DeleteCommand != null;
		}
		return true;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			this.m_a = null;
			this.m_b = null;
			this.m_c = null;
			this.m_d = null;
		}
		base.Dispose(disposing);
	}

	internal int a(DataTable A_0, IDataReader A_1)
	{
		return LoadTable(A_0, (DbDataReader)A_1);
	}

	private static Type a(OracleDataReader A_0, int A_1, bool A_2)
	{
		if (A_2)
		{
			return A_0.GetProviderSpecificFieldType(A_1);
		}
		return A_0.GetFieldType(A_1);
	}

	private static void a(string A_0, DataColumnCollection A_1, OracleAttributeCollection A_2, bool A_3, Hashtable A_4)
	{
		for (int num = 0; num < A_2.Count; num++)
		{
			OracleAttribute oracleAttribute = A_2[num];
			if (A_1[A_0 + oracleAttribute.a] == null)
			{
				if (A_3 || (oracleAttribute.g != OracleDbType.Array && oracleAttribute.g != OracleDbType.Table))
				{
					A_1.Add(A_0 + oracleAttribute.a, OracleUtils.OracleDbTypeToType(oracleAttribute.g));
				}
				else
				{
					A_1.Add(A_0 + oracleAttribute.a, typeof(int));
				}
			}
			if (oracleAttribute.h != null)
			{
				A_4[A_0 + oracleAttribute.a] = oracleAttribute.h;
				if (oracleAttribute.g == OracleDbType.Object)
				{
					a(A_0 + oracleAttribute.a + ".", A_1, oracleAttribute.h.a, A_3, A_4);
				}
			}
		}
	}

	private void a(string A_0, DataColumn A_1, DataTable A_2, OracleAttributeCollection A_3, bool A_4)
	{
		for (int num = 0; num < A_3.Count; num++)
		{
			OracleAttribute oracleAttribute = A_3[num];
			string text = A_0 + oracleAttribute.a;
			DataColumn dataColumn = A_2.Columns[text];
			if (oracleAttribute.h == null || (oracleAttribute.g != OracleDbType.Array && oracleAttribute.g != OracleDbType.Table))
			{
				foreach (DataRow row in A_2.Rows)
				{
					if (row[A_1] != DBNull.Value)
					{
						OracleObject oracleObject = (OracleObject)row[A_1];
						row[dataColumn] = oracleObject[oracleAttribute];
					}
				}
			}
			if (oracleAttribute.h != null)
			{
				OracleDbType oracleDbType = oracleAttribute.g;
				if (oracleDbType == OracleDbType.Object)
				{
					a(text + ".", dataColumn, A_2, oracleAttribute.ObjectType.Attributes, A_4);
				}
			}
		}
		if (!A_4)
		{
			return;
		}
		foreach (DataRow row2 in A_2.Rows)
		{
			if (row2[A_1] != DBNull.Value)
			{
				row2[A_1] = DBNull.Value;
			}
		}
	}

	public void InitializeSequentialFill()
	{
		if (SelectCommand == null)
		{
			throw new InvalidOperationException("SelectCommand property has not been initialized.");
		}
		SelectCommand.Prepare();
		this.m_f = true;
	}

	public void TerminateSequentialFill()
	{
		this.m_f = false;
		if (g != null)
		{
			g.Close();
			g = null;
		}
		if (SelectCommand == null)
		{
			throw new InvalidOperationException("SelectCommand property has not been initialized.");
		}
		SelectCommand.g();
	}

	private int a(DataTable[] A_0, OracleDataReader A_1, DataSet A_2, string A_3, int A_4, int A_5)
	{
		int result = 0;
		bool a_ = A_1.ObjectView;
		try
		{
			bool flag = false;
			Hashtable A_6 = null;
			DataTable dataTable = null;
			if (A_0 != null && A_0.Length != 0 && A_0[0] != null)
			{
				dataTable = A_0[0];
				DataTableMapping dataTableMapping = null;
				if (A_3 == null && TableMappings.Count > 0)
				{
					dataTableMapping = TableMappings[0];
				}
				if (dataTableMapping == null && A_3 != null && TableMappings.Contains(A_3))
				{
					dataTableMapping = TableMappings[A_3];
				}
				Hashtable a_2 = null;
				a(dataTable, A_1, dataTableMapping, ReturnProviderSpecificTypes, a_2, out A_6, flag, base.MissingSchemaAction);
			}
			result = ((A_2 == null) ? base.Fill(A_0, A_1, A_4, A_5) : base.Fill(A_2, A_3, A_1, A_4, A_5));
			if (dataTable != null)
			{
				dataTable.BeginLoadData();
				try
				{
					for (int num = 0; num < dataTable.Columns.Count; num++)
					{
						DataColumn dataColumn = dataTable.Columns[num];
						if ((object)dataColumn.DataType == typeof(OracleObject) && dataColumn.ColumnName.IndexOf('.') < 0 && flag)
						{
							string columnName = dataColumn.ColumnName;
							OracleType oracleType = (OracleType)A_6[columnName];
							a(columnName + ".", dataColumn, dataTable, oracleType.a, flag);
						}
					}
				}
				finally
				{
					dataTable.EndLoadData();
				}
			}
		}
		finally
		{
			A_1.ObjectView = a_;
		}
		return result;
	}

	internal static void a(DataTable A_0, OracleDataReader A_1, DataTableMapping A_2, bool A_3, Hashtable A_4, out Hashtable A_5, bool A_6, MissingSchemaAction A_7)
	{
		_ = A_0.Columns.Count;
		bool flag = false;
		A_4?.Clear();
		if (!flag)
		{
			A_6 = true;
		}
		A_5 = new Hashtable();
		A_1.ObjectView = flag;
		int fieldCount = A_1.FieldCount;
		ArrayList arrayList = new ArrayList();
		Hashtable hashtable = new Hashtable();
		for (int num = 0; num < fieldCount; num++)
		{
			string text = A_1.GetName(num);
			if (hashtable.ContainsKey(text.ToUpper()))
			{
				int num2 = 1;
				while (true)
				{
					string text2 = text + num2;
					bool flag2 = false;
					if (hashtable.ContainsKey(text2.ToUpper()))
					{
						flag2 = true;
					}
					else
					{
						for (int num3 = 0; num3 < fieldCount; num3++)
						{
							if (A_1.GetName(num3) == text2)
							{
								flag2 = true;
								break;
							}
						}
					}
					if (!flag2)
					{
						break;
					}
					num2++;
				}
				text += num2;
			}
			arrayList.Add(text);
			hashtable.Add(text.ToUpper(), null);
		}
		for (int num4 = 0; num4 < fieldCount; num4++)
		{
			OracleDbType oracleDbType = A_1.e(num4);
			OracleType objectType = A_1.GetObjectType(num4);
			string text3 = (string)arrayList[num4];
			if (A_2 != null && A_2.ColumnMappings.Contains(text3))
			{
				text3 = A_2.ColumnMappings[text3].DataSetColumn;
			}
			bool flag3 = false;
			int num5 = A_0.Columns.IndexOf(text3);
			if (num5 >= 0)
			{
				flag3 = true;
				if (A_4 != null)
				{
					A_4[num4] = num5;
				}
			}
			else
			{
				flag3 = Devart.Common.DbDataAdapter.CheckMissingSchemaAction(text3, A_0.TableName, A_7);
			}
			if (objectType != null)
			{
				A_5[text3] = objectType;
			}
			DataColumn dataColumn = null;
			switch (oracleDbType)
			{
			case OracleDbType.Array:
			case OracleDbType.Cursor:
			case OracleDbType.Table:
				if (flag && !flag3)
				{
					dataColumn = A_0.Columns.Add(text3, a(A_1, num4, A_3));
					if (A_4 != null)
					{
						A_4[num4] = A_0.Columns.Count - 1;
					}
				}
				break;
			case OracleDbType.Object:
				if (!flag3)
				{
					dataColumn = A_0.Columns.Add(text3, a(A_1, num4, A_3));
					if (A_4 != null)
					{
						A_4[num4] = A_0.Columns.Count - 1;
					}
				}
				if (objectType != null && (!flag || A_6))
				{
					a(text3 + ".", A_0.Columns, objectType.a, flag, A_5);
				}
				break;
			default:
				if (!flag3)
				{
					dataColumn = A_0.Columns.Add(text3, a(A_1, num4, A_3));
					if (A_4 != null)
					{
						A_4[num4] = A_0.Columns.Count - 1;
					}
				}
				break;
			}
			if (dataColumn != null)
			{
				dataColumn.ExtendedProperties.Add(SchemaTableColumn.ColumnName, A_1.GetName(num4));
				dataColumn.ExtendedProperties.Add(SchemaTableColumn.ColumnOrdinal, num4);
			}
		}
	}

	internal static string[] a(ICollection<string> A_0)
	{
		return Devart.Common.DbDataAdapter.GetIndexedFieldNames(A_0, firstColumnBugCompatibleMode: false);
	}

	public int Fill(DataTable dataTable, OracleCursor cursor)
	{
		if (dataTable == null)
		{
			throw new ArgumentNullException("dataTable");
		}
		if (cursor == null)
		{
			throw new ArgumentNullException("cursor");
		}
		OracleDataReader dataReader = cursor.GetDataReader();
		int num = 0;
		try
		{
			return Fill(dataTable, dataReader);
		}
		finally
		{
			dataReader.Close();
		}
	}

	public int Fill(DataSet dataSet, OracleCursor cursor)
	{
		string srcTable = "Table";
		if (dataSet == null)
		{
			throw new ArgumentNullException("dataSet");
		}
		if (cursor == null)
		{
			throw new ArgumentNullException("cursor");
		}
		OracleDataReader dataReader = cursor.GetDataReader();
		int num = 0;
		int startRecord = 0;
		int maxRecords = 0;
		try
		{
			return Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
		}
		finally
		{
			dataReader.Close();
		}
	}

	protected override int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
	{
		if (Utils.MonoDetected)
		{
			CommandBehavior commandBehavior = behavior;
			if (command.Connection.State == ConnectionState.Closed)
			{
				command.Connection.Open();
				commandBehavior |= CommandBehavior.CloseConnection;
			}
			return Fill(new DataTable[1] { dataTable }, command.ExecuteReader(commandBehavior), 0, 0);
		}
		return base.Fill(dataTable, command, behavior);
	}

	public int Fill(DataSet dataSet, string srcTable, OracleCursor cursor)
	{
		if (dataSet == null)
		{
			throw new ArgumentNullException("dataSet");
		}
		if (cursor == null)
		{
			throw new ArgumentNullException("cursor");
		}
		OracleDataReader dataReader = cursor.GetDataReader();
		int num = 0;
		int startRecord = 0;
		int maxRecords = 0;
		try
		{
			return Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
		}
		finally
		{
			dataReader.Close();
		}
	}

	public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, OracleCursor cursor)
	{
		if (dataSet == null)
		{
			throw new ArgumentNullException("dataSet");
		}
		if (cursor == null)
		{
			throw new ArgumentNullException("cursor");
		}
		OracleDataReader dataReader = cursor.GetDataReader();
		int num = 0;
		try
		{
			return Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
		}
		finally
		{
			dataReader.Close();
		}
	}

	private int a(DataSet A_0, string A_1, IDataReader A_2, int A_3, int A_4)
	{
		if (A_2 is OracleDataReader)
		{
			string name = A_1;
			if (TableMappings.Contains(A_1))
			{
				name = TableMappings[A_1].DataSetTable;
			}
			DataTable dataTable = A_0.Tables[name];
			if (dataTable == null)
			{
				dataTable = A_0.Tables.Add(name);
			}
			DataTable[] a_ = new DataTable[1] { dataTable };
			return a(a_, (OracleDataReader)A_2, A_0, A_1, A_3, A_4);
		}
		return base.Fill(A_0, A_1, A_2, A_3, A_4);
	}

	private string a(OracleDataReader A_0)
	{
		int fieldCount = A_0.FieldCount;
		for (int num = 0; num < fieldCount; num++)
		{
			if (A_0.e(num) == OracleDbType.RowId)
			{
				return A_0.GetName(num);
			}
		}
		return null;
	}

	protected override int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
	{
		if (this.m_f)
		{
			OracleCommand oracleCommand = (OracleCommand)command;
			OracleDataReader oracleDataReader = oracleCommand.DataReaderInternal;
			int num = 0;
			if (oracleDataReader == null || oracleDataReader != g || (num = startRecord - oracleDataReader.CurrentRecord) < 0)
			{
				if (!oracleCommand.IsPreparedInternal)
				{
					oracleCommand.Prepare();
				}
				oracleDataReader = (g = oracleCommand.ExecuteReader(behavior));
				num = startRecord;
			}
			return Fill(dataTables, oracleDataReader, num, maxRecords);
		}
		return base.Fill(dataTables, startRecord, maxRecords, command, behavior);
	}

	protected override int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
	{
		if (this.m_f)
		{
			OracleCommand oracleCommand = (OracleCommand)command;
			OracleDataReader oracleDataReader = oracleCommand.DataReaderInternal;
			int num = 0;
			if (oracleDataReader == null || oracleDataReader != g || (num = startRecord - oracleDataReader.CurrentRecord) < 0)
			{
				if (!oracleCommand.IsPreparedInternal)
				{
					oracleCommand.Prepare();
				}
				oracleDataReader = (g = oracleCommand.ExecuteReader(behavior));
				num = startRecord;
			}
			return Fill(dataSet, srcTable, oracleDataReader, num, maxRecords);
		}
		return base.Fill(dataSet, startRecord, maxRecords, srcTable, command, behavior);
	}

	protected override int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
	{
		string text = a((OracleDataReader)dataReader);
		int result = a(dataSet, srcTable, dataReader, startRecord, maxRecords);
		if (text != null)
		{
			if (TableMappings == null || TableMappings.Count == 0 || !TableMappings.Contains(srcTable))
			{
				DataTable dataTable = dataSet.Tables[srcTable];
				this.m_e = dataTable.Columns[text];
			}
			else
			{
				DataTableMapping dataTableMapping = TableMappings[srcTable];
				DataTable dataTable2 = dataSet.Tables[dataTableMapping.DataSetTable];
				if (dataTableMapping.ColumnMappings.Contains(text))
				{
					this.m_e = dataTable2.Columns[dataTableMapping.ColumnMappings[text].DataSetColumn];
				}
				else
				{
					this.m_e = dataTable2.Columns[text];
				}
			}
		}
		else
		{
			this.m_e = null;
		}
		return result;
	}

	protected override int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
	{
		string text = a((OracleDataReader)dataReader);
		int result = a(dataTables, dataReader, null, null, startRecord, maxRecords);
		this.m_e = null;
		if (dataTables != null && dataTables.Length != 0 && dataTables[0] != null)
		{
			DataTable dataTable = dataTables[0];
			if (text != null)
			{
				if (TableMappings == null || TableMappings.Count == 0 || !TableMappings.Contains(dataTable.TableName))
				{
					this.m_e = dataTable.Columns[text];
				}
				else
				{
					DataTableMapping byDataSetTable = TableMappings.GetByDataSetTable(dataTable.TableName);
					if (byDataSetTable.ColumnMappings.Contains(text))
					{
						this.m_e = dataTable.Columns[byDataSetTable.ColumnMappings[text].DataSetColumn];
					}
					else
					{
						this.m_e = dataTable.Columns[text];
					}
				}
			}
		}
		return result;
	}

	private int a(DataTable[] A_0, IDataReader A_1, DataSet A_2, string A_3, int A_4, int A_5)
	{
		if (A_1 is OracleDataReader)
		{
			return a(A_0, (OracleDataReader)A_1, null, null, A_4, A_5);
		}
		return base.Fill(A_0, A_1, A_4, A_5);
	}

	protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
	{
		return new OracleRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
	}

	protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
	{
		return new OracleRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
	}

	protected override void OnRowUpdating(RowUpdatingEventArgs value)
	{
		if (value is OracleRowUpdatingEventArgs e2)
		{
			((OracleRowUpdatingEventHandler)base.Events[m])?.Invoke(this, e2);
			((OracleRowUpdatingEventHandler)base.Events[k])?.Invoke(this, e2);
		}
		DataRow row = value.Row;
		if (row.RowState != DataRowState.Deleted && this.m_e != null && row.Table.Columns.IndexOf(this.m_e.ColumnName) >= 0)
		{
			((OracleCommand)value.Command).SaveRowId = true;
		}
	}

	protected override void OnRowUpdated(RowUpdatedEventArgs value)
	{
		DataRow row = value.Row;
		if (row != null && row.RowState != DataRowState.Deleted && value.Status != UpdateStatus.ErrorsOccurred && this.m_e != null && row.Table.Columns.IndexOf(this.m_e.ColumnName) >= 0 && Utils.IsNull(row[this.m_e]))
		{
			row[this.m_e] = ((OracleCommand)value.Command).GetRowId();
		}
		if (value is OracleRowUpdatedEventArgs e2)
		{
			((OracleRowUpdatedEventHandler)base.Events[j])?.Invoke(this, e2);
			((OracleRowUpdatedEventHandler)base.Events[l])?.Invoke(this, e2);
		}
	}

	protected override void CustomizeDataTableColumns(DataTable[] tables)
	{
	}

	private bool b()
	{
		if (TableMappings != null)
		{
			return TableMappings.Count > 0;
		}
		return false;
	}

	private void a()
	{
		TableMappings.Clear();
	}

	protected override void InitializeBatching()
	{
		_ = InsertCommand;
		_ = DeleteCommand;
		_ = UpdateCommand;
		if (i == null)
		{
			i = new ab();
		}
		i.b();
		OracleCommand oracleCommand = InsertCommand;
		if (oracleCommand == null)
		{
			oracleCommand = UpdateCommand;
			if (oracleCommand == null)
			{
				oracleCommand = DeleteCommand;
				if (oracleCommand == null)
				{
					oracleCommand = SelectCommand;
				}
			}
		}
		if (oracleCommand != null)
		{
			i.d().Connection = oracleCommand.Connection;
			i.d().CommandTimeout = oracleCommand.CommandTimeout;
		}
	}

	protected override int AddToBatch(IDbCommand command)
	{
		if (i != null)
		{
			return i.a(command as OracleCommand);
		}
		return -1;
	}

	protected override void ClearBatch()
	{
		if (i != null)
		{
			i.b();
		}
	}

	protected override int ExecuteBatch()
	{
		if (i != null)
		{
			i.c();
			return i.d().ExecuteNonQuery();
		}
		return -1;
	}

	protected override void TerminateBatching()
	{
	}

	protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
	{
		if (i != null)
		{
			return i.a(commandIdentifier, parameterIndex);
		}
		return null;
	}
}
