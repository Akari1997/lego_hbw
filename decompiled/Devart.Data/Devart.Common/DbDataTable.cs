using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using Devart.Data;

namespace Devart.Common;

public abstract class DbDataTable : DataTable, IListSource, ISupportInitializeNotification
{
	private delegate void a(CultureInfo A_0);

	private const int m_a = int.MaxValue;

	private const int m_b = 2147483646;

	private const ConflictOption m_c = ConflictOption.CompareAllSearchableValues;

	private const string m_d = "Can not establish master/detail relation";

	protected internal DataTable dataTable;

	private object m_e;

	private bool m_f;

	protected internal DbConnection connection;

	internal DbCommand g;

	protected DbCommand currenSelectCommand;

	protected DbCommand detailSelectCommand;

	internal DbCommand h;

	internal DbCommand i;

	internal DbCommand j;

	protected IDataReader reader;

	private bool m_k;

	private bool m_l;

	private bool m;

	private bool m_n;

	private int m_o;

	private int m_p;

	private bool m_q;

	private DbDataTableView m_r;

	internal EventHandler s;

	private a m_t;

	internal DataTableMapping u;

	private bool m_v;

	private bool w;

	private object m_x;

	private object m_y;

	private int z;

	private int m_aa;

	private bool ab;

	protected internal int disableEvents;

	protected internal int storeEvents;

	protected internal int disableUpdateEvents;

	internal bool ac;

	protected PropertyDescriptorCollection propertyDescriptorsCache;

	protected Hashtable readerMappings;

	internal aq ad;

	internal ParentDataRelation ae;

	private bool af;

	private bool ag;

	private bool ah;

	private int ai;

	protected bool hasComplexFields;

	protected bool returnProviderSpecificTypesInternal;

	private bool aj;

	private bool ak;

	private bool al;

	private static readonly object am;

	private static readonly object an;

	private static readonly object ao;

	private static readonly object ap;

	private static readonly object aq;

	private bool ar;

	private bool @as;

	private MissingSchemaAction at;

	private int au;

	protected DataTable schemaTable;

	private static bool av;

	protected int indexOfColumnOnlyOriginalValue;

	private int aw;

	private DataRow ax;

	protected DbDataAdapter dataAdapter;

	protected DbCommandBuilder commandBuilder;

	private string ay;

	private ArrayList az;

	private bool a0;

	private int a1;

	bool ISupportInitializeNotification.IsInitialized => !fInitInProgress;

	[Browsable(false)]
	public object SyncRoot => this.m_x;

	[Category("Live Data")]
	[DefaultValue(false)]
	[r("DbDataTable_QueryRecordCount")]
	public bool QueryRecordCount
	{
		get
		{
			return w;
		}
		set
		{
			w = value;
		}
	}

	bool IListSource.ContainsListCollection => false;

	protected bool UserDefinedColumns
	{
		get
		{
			if (Site != null && !(Site.Container is INestedContainer))
			{
				for (int i = 0; i < Columns.Count; i++)
				{
					DataColumn dataColumn = Columns[i];
					if (dataColumn != null && dataColumn.Site != null)
					{
						return true;
					}
				}
				return false;
			}
			return ab;
		}
		set
		{
			if (Site == null)
			{
				ab = value;
			}
			else
			{
				if (Site.Container == null)
				{
					return;
				}
				for (int i = 0; i < Columns.Count; i++)
				{
					DataColumn dataColumn = Columns[i];
					if (dataColumn != null && dataColumn.Site == null)
					{
						Site.Container.Add(dataColumn, Site.Name + "_" + dataColumn.ColumnName);
					}
				}
			}
		}
	}

	[Browsable(false)]
	public DataTable SchemaTable => GetSchemaTable();

	[Browsable(false)]
	public int RecordCount
	{
		get
		{
			if (w || !FetchComplete)
			{
				return this.m_o;
			}
			return base.Rows.Count;
		}
	}

	internal bool FetchComplete
	{
		get
		{
			if (reader != null && reader.IsClosed && !this.m_l)
			{
				o();
				FetchCompleted(GetSchemaTable());
				c(this, null);
			}
			if (reader != null && !reader.IsClosed)
			{
				return this.m_l;
			}
			return true;
		}
	}

	private object DependentColumnsInternal
	{
		get
		{
			try
			{
				FieldInfo field = typeof(DbDataTable).GetField("dependentColumns", BindingFlags.Instance | BindingFlags.NonPublic);
				if ((object)field != null)
				{
					return field.GetValue(this);
				}
			}
			catch
			{
			}
			return null;
		}
		set
		{
			try
			{
				typeof(DbDataTable).GetField("dependentColumns", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(this, value);
			}
			catch
			{
			}
		}
	}

	protected internal DbCommandBuilder CommandBuilderInternal
	{
		get
		{
			CheckDataAdapterCreated();
			return commandBuilder;
		}
	}

	protected internal DbDataAdapter DataAdapterInternal
	{
		get
		{
			CheckDataAdapterCreated();
			return dataAdapter;
		}
	}

	[DefaultValue(false)]
	[r("DbTable_Active")]
	[Category("Live Data")]
	public bool Active
	{
		get
		{
			if (fInitInProgress)
			{
				return this.m_f;
			}
			return m;
		}
		set
		{
			if (fInitInProgress)
			{
				this.m_f = value;
				return;
			}
			@as = value;
			if (value != m)
			{
				if (value)
				{
					v();
					Open();
				}
				else
				{
					Close();
				}
				m = value;
			}
		}
	}

	[r("DbTable_StartRecord")]
	[Category("Live Data")]
	[DefaultValue(0)]
	public int StartRecord
	{
		get
		{
			return z;
		}
		set
		{
			if (z != value)
			{
				if (!fInitInProgress && Active && this.m_aa > 0)
				{
					a(value, this.m_aa);
				}
				z = value;
			}
		}
	}

	[Category("Live Data")]
	[DefaultValue(0)]
	[r("DbTable_MaxRecords")]
	public int MaxRecords
	{
		get
		{
			return this.m_aa;
		}
		set
		{
			if (this.m_aa == value)
			{
				return;
			}
			this.m_aa = value;
			if (!fInitInProgress && Active)
			{
				if (value == 0)
				{
					Close();
					Open();
				}
				else
				{
					a(z, value);
				}
			}
		}
	}

	public DataTableMapping TableMapping => u;

	[MergableProperty(false)]
	[Browsable(false)]
	public DbConnection Connection
	{
		get
		{
			return connection;
		}
		set
		{
			if (connection != value)
			{
				connection = value;
				if (SelectCommand != null)
				{
					SelectCommand.Connection = value;
				}
				if (InsertCommand != null)
				{
					InsertCommand.Connection = value;
				}
				if (UpdateCommand != null)
				{
					UpdateCommand.Connection = value;
				}
				if (DeleteCommand != null)
				{
					DeleteCommand.Connection = value;
				}
			}
		}
	}

	[MergableProperty(false)]
	[Browsable(false)]
	public DbCommand SelectCommand
	{
		get
		{
			return this.g;
		}
		set
		{
			if (DesignMode && value == null && Active)
			{
				Active = false;
			}
			this.g = value;
			((IDbDataAdapter)DataAdapterInternal).SelectCommand = value;
		}
	}

	[MergableProperty(false)]
	[Browsable(false)]
	public DbCommand InsertCommand
	{
		get
		{
			return this.h;
		}
		set
		{
			this.h = value;
			((IDbDataAdapter)DataAdapterInternal).InsertCommand = value;
		}
	}

	[Browsable(false)]
	[MergableProperty(false)]
	public DbCommand UpdateCommand
	{
		get
		{
			return this.i;
		}
		set
		{
			this.i = value;
			((IDbDataAdapter)DataAdapterInternal).UpdateCommand = value;
		}
	}

	[MergableProperty(false)]
	[Browsable(false)]
	public DbCommand DeleteCommand
	{
		get
		{
			return this.j;
		}
		set
		{
			this.j = value;
			((IDbDataAdapter)DataAdapterInternal).DeleteCommand = value;
		}
	}

	[DefaultValue(true)]
	[Category("Update")]
	[r("DbTable_CachedUpdates")]
	public bool CachedUpdates
	{
		get
		{
			return this.m_v;
		}
		set
		{
			if (this.m_v && !value)
			{
				if (this.m_r != null && this.m_r.CurrentRowView != null)
				{
					((IEditableObject)this.m_r.CurrentRowView).EndEdit();
				}
				Update();
			}
			this.m_v = value;
		}
	}

	[DefaultValue(false)]
	[r("DbTable_FetchAll")]
	[Category("Live Data")]
	public virtual bool FetchAll
	{
		get
		{
			return ag;
		}
		set
		{
			ag = value;
		}
	}

	[DefaultValue(false)]
	[r("DbTable_NonBlocking")]
	[Category("Live Data")]
	public bool NonBlocking
	{
		get
		{
			return af;
		}
		set
		{
			af = value;
		}
	}

	protected new EventHandlerList Events => base.Events;

	internal string FullName
	{
		get
		{
			if ((object)GetType().BaseType == typeof(DbDataTable) || !(base.DataSet is DbDataSet))
			{
				return Name;
			}
			return (base.DataSet as DbDataSet).Name + "." + Name;
		}
	}

	[DefaultValue("")]
	[Browsable(false)]
	public string Name
	{
		get
		{
			if (Site == null || Site.Name == null)
			{
				if (ay != null)
				{
					return ay;
				}
				return string.Empty;
			}
			return Site.Name;
		}
		set
		{
			if (Site == null)
			{
				ay = ((value == null) ? string.Empty : value);
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public object Owner
	{
		get
		{
			return this.m_e;
		}
		set
		{
			SetOwner(value);
		}
	}

	[Browsable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[MergableProperty(false)]
	[Category("Data")]
	[r("DbDataTable_ParentRelation")]
	public ParentDataRelation ParentRelation
	{
		get
		{
			if (ae == null)
			{
				ae = new ParentDataRelation();
				ae.ChildTableInternal = this;
			}
			return ae;
		}
		set
		{
			if (value == null && ae != null)
			{
				ae.ChildTableInternal = null;
			}
			ae = value;
			if (ae != null)
			{
				ae.ChildTableInternal = this;
			}
		}
	}

	[DesignOnly(true)]
	[DefaultValue(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public bool DesignTimeVisible
	{
		get
		{
			return ah;
		}
		set
		{
			ah = value;
			TypeDescriptor.Refresh(this);
		}
	}

	[MergableProperty(false)]
	public new DataColumnCollection Columns => base.Columns;

	[MergableProperty(false)]
	public new ConstraintCollection Constraints => base.Constraints;

	[MergableProperty(false)]
	public new DataColumn[] PrimaryKey
	{
		get
		{
			return base.PrimaryKey;
		}
		set
		{
			aj = true;
			try
			{
				base.PrimaryKey = value;
			}
			finally
			{
				aj = false;
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	protected internal bool Reloading
	{
		get
		{
			return ar;
		}
		set
		{
			ar = value;
		}
	}

	[Category("Update")]
	[DefaultValue("")]
	[r("DbDataTable_KeyFields")]
	public string UpdatingKeyFields
	{
		get
		{
			if (commandBuilder == null)
			{
				return "";
			}
			return commandBuilder.KeyFields;
		}
		set
		{
			if (!(value == "") || commandBuilder != null)
			{
				CommandBuilderInternal.KeyFields = value;
			}
		}
	}

	[MergableProperty(false)]
	[Category("Update")]
	[r("DbDataTable_UpdatingFields")]
	[DefaultValue("")]
	public string UpdatingFields
	{
		get
		{
			if (commandBuilder == null)
			{
				return "";
			}
			return commandBuilder.UpdatingFields;
		}
		set
		{
			if (!(value == "") || commandBuilder != null)
			{
				CommandBuilderInternal.UpdatingFields = value;
			}
		}
	}

	[MergableProperty(false)]
	public string UpdatingTable
	{
		get
		{
			if (commandBuilder == null)
			{
				return "";
			}
			return commandBuilder.UpdatingTable;
		}
		set
		{
			if (!(value == "") || commandBuilder != null)
			{
				CommandBuilderInternal.UpdatingTable = value;
			}
		}
	}

	[DefaultValue(false)]
	[r("DbDataTable_Quoted")]
	[Category("Update")]
	public bool Quoted
	{
		get
		{
			if (commandBuilder == null)
			{
				return false;
			}
			return commandBuilder.Quoted;
		}
		set
		{
			if (value || commandBuilder != null)
			{
				CommandBuilderInternal.Quoted = value;
			}
		}
	}

	[Category("Update")]
	[DefaultValue(RefreshRowMode.None)]
	[r("DbDataTable_RefreshMode")]
	public RefreshRowMode RefreshMode
	{
		get
		{
			if (commandBuilder == null)
			{
				return RefreshRowMode.None;
			}
			return commandBuilder.RefreshMode;
		}
		set
		{
			if (value != RefreshRowMode.None || commandBuilder != null)
			{
				CommandBuilderInternal.RefreshMode = value;
			}
		}
	}

	[DefaultValue("")]
	[r("DbDataTable_RefreshingFields")]
	[Category("Update")]
	[MergableProperty(false)]
	public string RefreshingFields
	{
		get
		{
			if (commandBuilder == null)
			{
				return "";
			}
			return commandBuilder.RefreshingFields;
		}
		set
		{
			if (!(value == "") || commandBuilder != null)
			{
				CommandBuilderInternal.RefreshingFields = value;
			}
		}
	}

	[Category("Update")]
	[r("DbDataTable_ConflictOption")]
	[DefaultValue(ConflictOption.CompareAllSearchableValues)]
	public virtual ConflictOption ConflictOption
	{
		get
		{
			if (commandBuilder == null)
			{
				return ConflictOption.CompareAllSearchableValues;
			}
			return commandBuilder.ConflictOption;
		}
		set
		{
			if (value != ConflictOption.CompareAllSearchableValues || commandBuilder != null)
			{
				CommandBuilderInternal.ConflictOption = value;
			}
		}
	}

	[Category("Update")]
	[r("DbDataTable_UpdateBatchSize")]
	[DefaultValue(1)]
	public int UpdateBatchSize
	{
		get
		{
			if (dataAdapter == null)
			{
				return 1;
			}
			return dataAdapter.UpdateBatchSize;
		}
		set
		{
			if (value != 1 || dataAdapter != null)
			{
				DataAdapterInternal.UpdateBatchSize = value;
			}
		}
	}

	[r("DbDataTable_ReturnProviderSpecificTypes")]
	[DefaultValue(false)]
	[Category("Fill")]
	public bool ReturnProviderSpecificTypes
	{
		get
		{
			return returnProviderSpecificTypesInternal;
		}
		set
		{
			returnProviderSpecificTypesInternal = value;
			if (dataAdapter != null)
			{
				dataAdapter.ReturnProviderSpecificTypes = value;
			}
		}
	}

	[DefaultValue(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public bool RetrieveAutoIncrementSeed
	{
		get
		{
			return al;
		}
		set
		{
			al = value;
		}
	}

	[r("DbDataTable_RemotingFormat")]
	[DefaultValue(SerializationFormat.Xml)]
	public new SerializationFormat RemotingFormat
	{
		get
		{
			return base.RemotingFormat;
		}
		set
		{
			base.RemotingFormat = value;
		}
	}

	[DefaultValue(MissingSchemaAction.AddWithKey)]
	[r("DbDataTable_MissingSchemaAction")]
	public MissingSchemaAction MissingSchemaAction
	{
		get
		{
			return at;
		}
		set
		{
			if (value == MissingSchemaAction.Add || value == MissingSchemaAction.AddWithKey)
			{
				at = value;
				return;
			}
			throw new NotSupportedException(Devart.Common.n.a("MissingSchemaActionNotSupported"));
		}
	}

	internal object FetchRowSyncRoot
	{
		get
		{
			if (base.DataSet is DbDataSet dbDataSet)
			{
				return dbDataSet.FetchRowSyncRoot;
			}
			if (this.m_y == null)
			{
				this.m_y = new object();
			}
			return this.m_y;
		}
	}

	protected CommandBehavior ExecuteCommBehavior
	{
		get
		{
			if (at != MissingSchemaAction.AddWithKey)
			{
				return CommandBehavior.Default;
			}
			return CommandBehavior.KeyInfo;
		}
	}

	internal bool AllowCruidDuringFetch
	{
		get
		{
			return a0;
		}
		set
		{
			a0 = value;
		}
	}

	public static bool DisableListChangedEvents
	{
		get
		{
			return av;
		}
		set
		{
			av = value;
		}
	}

	[r("DbDataTable_Disposed")]
	public new event EventHandler Disposed
	{
		add
		{
			base.Disposed += value;
		}
		remove
		{
			base.Disposed -= value;
		}
	}

	event EventHandler ISupportInitializeNotification.Initialized
	{
		add
		{
			Events.AddHandler(aq, value);
		}
		remove
		{
			Events.RemoveHandler(aq, value);
		}
	}

	[r("DbDataTable_RowFetched")]
	[Category("Fill")]
	public event EventHandler RowFetched
	{
		add
		{
			Events.AddHandler(ao, value);
		}
		remove
		{
			Events.RemoveHandler(ao, value);
		}
	}

	[Category("Fill")]
	[r("DbDataTable_FetchFinished")]
	public event EventHandler FetchFinished
	{
		add
		{
			Events.AddHandler(ap, value);
		}
		remove
		{
			Events.RemoveHandler(ap, value);
		}
	}

	[r("DbDataTable_FillError")]
	[Category("Fill")]
	public event FillErrorEventHandler FillError
	{
		add
		{
			Events.AddHandler(am, value);
		}
		remove
		{
			Events.RemoveHandler(am, value);
		}
	}

	internal event EventHandler ListChanged
	{
		add
		{
			Events.AddHandler(an, value);
		}
		remove
		{
			Events.RemoveHandler(an, value);
		}
	}

	static DbDataTable()
	{
		am = new object();
		an = new object();
		ao = new object();
		ap = new object();
		aq = new object();
	}

	protected DbDataTable()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		this.m_l = true;
		this.m_v = true;
		this.m_x = new object();
		ah = true;
		al = true;
		at = MissingSchemaAction.AddWithKey;
		indexOfColumnOnlyOriginalValue = -1;
		ay = string.Empty;
		base._002Ector();
		dataTable = this;
		dataTable.Columns.CollectionChanged += a;
		dataTable.Constraints.CollectionChanged += b;
		ad = new aq(null, (Control)Owner, a);
		u = new DataTableMapping();
		this.m_p = -1;
		this.m_q = true;
		this.s = d;
	}

	protected DbDataTable(SerializationInfo info, StreamingContext context)
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		this.m_l = true;
		this.m_v = true;
		this.m_x = new object();
		ah = true;
		al = true;
		at = MissingSchemaAction.AddWithKey;
		indexOfColumnOnlyOriginalValue = -1;
		ay = string.Empty;
		base._002Ector(info, context);
		dataTable = this;
		dataTable.Columns.CollectionChanged += a;
		dataTable.Constraints.CollectionChanged += b;
		u = new DataTableMapping();
		this.m_p = -1;
		this.m_q = true;
		this.s = d;
		ad = new aq(null, (Control)Owner, a);
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		if (this.m_r != null)
		{
			this.m_r.Dispose();
			this.m_r = null;
		}
		if (reader != null)
		{
			reader.Dispose();
			reader = null;
		}
		if (schemaTable != null)
		{
			schemaTable.Dispose();
			schemaTable = null;
		}
		if (commandBuilder != null)
		{
			commandBuilder.Dispose();
			commandBuilder = null;
		}
		if (dataAdapter != null)
		{
			dataAdapter.Dispose();
			dataAdapter = null;
		}
		if (ad != null)
		{
			((IDisposable)ad).Dispose();
			ad = null;
		}
		ae = null;
		propertyDescriptorsCache = null;
		dataTable = null;
		readerMappings = null;
		u = null;
		if (Owner != null && !DesignMode)
		{
			GlobalComponentsCache.RemoveFromGlobalList(this);
		}
	}

	public override void EndInit()
	{
		if (fInitInProgress)
		{
			base.EndInit();
			if (ParentRelation.ParentTable == null || ((ISupportInitializeNotification)ParentRelation.ParentTable).IsInitialized)
			{
				s();
			}
			q();
		}
	}

	private void s()
	{
		try
		{
			Active = this.m_f;
		}
		catch
		{
			m = this.m_f;
			throw;
		}
	}

	private void r()
	{
		au--;
		if (au == 0)
		{
			if (this.m_r != null)
			{
				this.m_r.a(this, new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
			if (FetchComplete)
			{
				c(this, null);
			}
		}
	}

	internal void d(bool A_0)
	{
		DataRelationCollection parentRelations = base.ParentRelations;
		for (int i = 0; i < parentRelations.Count; i++)
		{
			DataRelation dataRelation = parentRelations[i];
			if (dataRelation.ParentTable is DbDataTable dbDataTable)
			{
				dbDataTable.b(A_0);
			}
		}
	}

	internal void v()
	{
		if (base.DataSet == null || !base.DataSet.EnforceConstraints)
		{
			return;
		}
		DataRelationCollection parentRelations = base.ParentRelations;
		for (int i = 0; i < parentRelations.Count; i++)
		{
			DataRelation dataRelation = parentRelations[i];
			if (dataRelation.ParentTable is DbDataTable dbDataTable)
			{
				if (!dbDataTable.Active && !dbDataTable.@as)
				{
					throw new InvalidOperationException($"The parent table '{dbDataTable.Name}' used in a relation should be opened before '{Name}'");
				}
				if (!dbDataTable.@as)
				{
					dbDataTable.EndInit();
				}
			}
		}
	}

	internal void d(object A_0, EventArgs A_1)
	{
		s();
	}

	private void q()
	{
		((EventHandler)Events[aq])?.Invoke(this, EventArgs.Empty);
	}

	internal void b(bool A_0)
	{
		if (FetchComplete)
		{
			return;
		}
		bool flag = false;
		if (base.DataSet != null)
		{
			flag = base.DataSet.EnforceConstraints;
			base.DataSet.EnforceConstraints = false;
		}
		try
		{
			int count = base.Rows.Count;
			au++;
			try
			{
				a(int.MaxValue, A_0, A_2: false);
			}
			finally
			{
				if (base.Rows.Count - count > 0)
				{
					r();
				}
				else
				{
					au--;
				}
			}
			if (base.Rows.Count - count > 0)
			{
				d(A_0);
			}
		}
		catch (ConstraintException)
		{
			flag = false;
			throw;
		}
		finally
		{
			if (flag)
			{
				base.DataSet.EnforceConstraints = true;
			}
		}
	}

	private void p()
	{
		b(A_0: true);
	}

	public int Fill(object[] parameterValues)
	{
		CheckSelectCommand();
		a(parameterValues);
		return Fill();
	}

	public int Fill()
	{
		if (!m || (!this.m_k && FetchComplete))
		{
			CloseReader();
			ClearSchemaTableCache();
			ac = true;
			CheckSelectCommand();
			ExecuteCommand();
			CheckColumnsCreated(throwOnEmptySchemaTable: false);
			if (this.m_r != null)
			{
				this.m_r.j();
			}
			a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
		}
		int count = base.Rows.Count;
		p();
		return base.Rows.Count - count;
	}

	private void a(CultureInfo A_0)
	{
		lock (this.m_x)
		{
			Thread.CurrentThread.CurrentCulture = A_0;
			a(2147483646, A_1: false, A_2: false);
			this.m_t = null;
			c(this, null);
		}
	}

	private void c(object A_0, EventArgs A_1)
	{
		((EventHandler)Events[ap])?.Invoke(A_0, null);
	}

	private void b(object A_0, EventArgs A_1)
	{
		((EventHandler)Events[ao])?.Invoke(A_0, null);
	}

	protected void FetchToPosition(int index)
	{
		a(index, A_1: true, A_2: false);
	}

	protected virtual void GetDataRow(DataRow row)
	{
		DbDataReaderBase dbDataReaderBase = (DbDataReaderBase)reader;
		int visibleFieldCount = dbDataReaderBase.VisibleFieldCount;
		if (!UserDefinedColumns && readerMappings.Count == visibleFieldCount)
		{
			object[] array = new object[reader.FieldCount];
			bool flag = visibleFieldCount < reader.FieldCount;
			if (returnProviderSpecificTypesInternal)
			{
				dbDataReaderBase.GetProviderSpecificValues(array);
				if (indexOfColumnOnlyOriginalValue > -1)
				{
					array[indexOfColumnOnlyOriginalValue] = reader.GetValue(indexOfColumnOnlyOriginalValue);
				}
				if (flag)
				{
					object[] array2 = new object[visibleFieldCount];
					Array.Copy(array, 0, array2, 0, visibleFieldCount);
					array = array2;
				}
			}
			else
			{
				reader.GetValues(array);
				if (flag)
				{
					object[] array3 = new object[visibleFieldCount];
					Array.Copy(array, 0, array3, 0, visibleFieldCount);
					array = array3;
				}
			}
			row.ItemArray = array;
			return;
		}
		int num = visibleFieldCount;
		for (int i = 0; i < num; i++)
		{
			object obj = readerMappings[i];
			if (obj != null)
			{
				GetField(row, (int)obj, reader, i);
			}
		}
	}

	protected static bool IsCatchableExceptionType(Exception e)
	{
		Type type = e.GetType();
		if ((object)type != typeof(StackOverflowException) && (object)type != typeof(OutOfMemoryException) && (object)type != typeof(ThreadAbortException) && (object)type != typeof(NullReferenceException) && (object)type != typeof(AccessViolationException))
		{
			return !typeof(SecurityException).IsAssignableFrom(type);
		}
		return false;
	}

	private void a(object A_0, FillErrorEventArgs A_1)
	{
		((FillErrorEventHandler)Events[am])?.Invoke(this, A_1);
	}

	protected bool RaiseFillError(ref Exception e, object[] dataValues)
	{
		FillErrorEventArgs e2 = new FillErrorEventArgs(this, dataValues);
		e2.Errors = e;
		a(this, e2);
		e = e2.Errors;
		return e2.Continue;
	}

	protected virtual void GetField(DataRow row, int rowInd, IDataReader reader, int readerInd)
	{
		if (returnProviderSpecificTypesInternal)
		{
			row[rowInd] = ((DbDataReaderBase)reader).GetProviderSpecificValue(readerInd);
		}
		else
		{
			row[rowInd] = reader.GetValue(readerInd);
		}
	}

	protected virtual void CheckReaderMappings()
	{
		if (readerMappings != null)
		{
			return;
		}
		if (FetchComplete)
		{
			readerMappings = null;
			return;
		}
		int fieldCount = reader.FieldCount;
		readerMappings = new Hashtable();
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < fieldCount; i++)
		{
			string text = reader.GetName(i);
			string text2 = text;
			int num = 0;
			if (Utils.IsEmpty(text))
			{
				text = "Column";
				text2 = "Column1";
				num = 1;
			}
			if (arrayList.IndexOf(text2.ToUpper()) >= 0)
			{
				bool flag;
				do
				{
					num++;
					text2 = text + num;
					flag = arrayList.IndexOf(text2.ToUpper()) >= 0;
					if (flag)
					{
						continue;
					}
					int fieldCount2 = reader.FieldCount;
					for (int num2 = 0; num2 < fieldCount2; num2++)
					{
						if (reader.GetName(num2) == text2)
						{
							flag = true;
							break;
						}
					}
				}
				while (flag);
			}
			arrayList.Add(text2.ToUpper());
			string columnNameFromMapping = GetColumnNameFromMapping(text2);
			int num3 = Columns.IndexOf(columnNameFromMapping);
			if (num3 >= 0)
			{
				readerMappings[i] = num3;
			}
		}
	}

	protected string GetColumnNameFromMapping(string sourceName)
	{
		if (u != null)
		{
			int num = u.ColumnMappings.IndexOf(sourceName);
			if (num >= 0)
			{
				sourceName = u.ColumnMappings[num].DataSetColumn;
			}
		}
		return sourceName;
	}

	public bool Read()
	{
		int num = this.m_p;
		a(this.m_p + 1, A_1: true, A_2: false);
		return this.m_p > num;
	}

	internal void a(int A_0, bool A_1, bool A_2)
	{
		if (!A_2 && this.m_aa > 0)
		{
			return;
		}
		do
		{
			if (A_0 <= this.m_p)
			{
				return;
			}
		}
		while (!Monitor.TryEnter(this.m_x));
		if (A_0 <= this.m_p)
		{
			return;
		}
		try
		{
			if (FetchComplete)
			{
				return;
			}
			if (a1 > 0)
			{
				throw new InvalidOperationException("Cannot fetch rows in this state.");
			}
			DataRow dataRow = null;
			aw++;
			try
			{
				bool flag = this.m_q;
				Interlocked.Exchange(ref ai, 0);
				while (A_0 > this.m_p && !FetchComplete)
				{
					lock (FetchRowSyncRoot)
					{
						if (this.m_q)
						{
							this.m_q = false;
							bool flag2;
							try
							{
								flag2 = reader.Read();
							}
							catch
							{
								CancelFetch();
								throw;
							}
							if (!flag2)
							{
								DataTable dataTable = GetSchemaTable();
								o();
								FetchCompleted(dataTable);
								this.m_o = this.m_p + 1;
								if (A_1)
								{
									c(this, null);
								}
								break;
							}
						}
						bool flag3 = false;
						try
						{
							dataRow = null;
							dataRow = this.dataTable.NewRow();
							GetDataRow(dataRow);
						}
						catch (Exception ex)
						{
							if (!IsCatchableExceptionType(ex) || !RaiseFillError(ref ex, dataRow?.ItemArray))
							{
								throw;
							}
							flag3 = true;
						}
						try
						{
							if (!reader.Read())
							{
								DataTable dataTable2 = GetSchemaTable();
								o();
								FetchCompleted(dataTable2);
							}
						}
						catch
						{
							CancelFetch();
							throw;
						}
						if (!flag3)
						{
							if (flag)
							{
								this.m_p++;
								if (FetchComplete || !w)
								{
									this.m_o = this.m_p + 1;
								}
							}
							disableEvents++;
							try
							{
								int num = 0;
								while (num < 2)
								{
									try
									{
										flag3 = false;
										this.dataTable.Rows.Add(dataRow);
										num = 2;
									}
									catch (Exception ex2)
									{
										num++;
										if (!IsCatchableExceptionType(ex2) || !RaiseFillError(ref ex2, dataRow.ItemArray))
										{
											throw;
										}
										flag3 = true;
										if (num == 2 && flag)
										{
											this.m_p--;
											if (FetchComplete || !w)
											{
												this.m_o = this.m_p + 1;
											}
										}
									}
								}
								if (!flag3)
								{
									dataRow.AcceptChanges();
								}
							}
							finally
							{
								disableEvents--;
							}
							if (!flag3)
							{
								if ((!flag || FetchComplete) && A_1 && au == 0 && !w && this.m_r != null && aw < 20)
								{
									int count = this.dataTable.Rows.Count;
									this.m_r.a(this, new ListChangedEventArgs(ListChangedType.ItemAdded, count - 1));
									if (aw == 19 && count < this.dataTable.Rows.Count)
									{
										this.m_r.a(this, new ListChangedEventArgs(ListChangedType.ItemAdded, this.dataTable.Rows.Count - 1));
									}
								}
								if (!flag)
								{
									this.m_p++;
									if (FetchComplete || !w)
									{
										this.m_o = this.m_p + 1;
									}
								}
								b((object)this, (EventArgs)null);
							}
						}
						if (FetchComplete && A_1 && au == 0)
						{
							c(this, null);
						}
						if (ai != 0)
						{
							Interlocked.Exchange(ref ai, 0);
							break;
						}
					}
				}
			}
			finally
			{
				aw--;
			}
		}
		finally
		{
			Monitor.Exit(this.m_x);
		}
	}

	protected virtual void FetchCompleted(DataTable schemaTable)
	{
	}

	private void o()
	{
		if (!this.m_l)
		{
			if (!this.m_k)
			{
				reader.Close();
			}
			else if (!reader.IsClosed && !reader.NextResult())
			{
				reader.Close();
			}
		}
		this.m_l = true;
	}

	public IAsyncResult BeginFill(AsyncCallback callback, object stateObject)
	{
		if (this.m_t != null)
		{
			new InvalidOperationException(Devart.Common.n.a("FetchInProgress"));
		}
		this.m_t = a;
		if (FetchComplete)
		{
			if (Active)
			{
				Clear();
			}
			CloseReader();
			ClearSchemaTableCache();
			ac = true;
			CheckSelectCommand();
			ExecuteCommand();
			CheckColumnsCreated(throwOnEmptySchemaTable: false);
			if (this.m_r != null)
			{
				this.m_r.j();
			}
			a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
		}
		return this.m_t.BeginInvoke(Thread.CurrentThread.CurrentCulture, callback, stateObject);
	}

	public void EndFill(IAsyncResult result)
	{
		if (result == null)
		{
			throw new ArgumentNullException("result");
		}
		try
		{
			if (this.m_t != null)
			{
				this.m_t.EndInvoke(result);
			}
		}
		finally
		{
			this.m_t = null;
		}
	}

	public void SuspendFill()
	{
		SuspendFill(wait: false);
	}

	public void SuspendFill(bool wait)
	{
		Interlocked.Exchange(ref ai, 1);
		if (wait)
		{
			lock (this.m_x)
			{
			}
		}
	}

	protected DbConnection GetConnection()
	{
		DbConnection dbConnection = connection;
		if (dbConnection != null)
		{
			return dbConnection;
		}
		if (this.g != null && this.g.Connection != null)
		{
			dbConnection = this.g.Connection;
		}
		if (dbConnection != null)
		{
			return dbConnection;
		}
		if (base.DataSet is DbDataSet dbDataSet)
		{
			dbConnection = dbDataSet.Connection;
		}
		return dbConnection;
	}

	protected virtual void AddPropertyDescriptor(PropertyDescriptorCollection propertyDescriptors, DataColumn dataColumn)
	{
		g g2 = null;
		if (propertyDescriptorsCache != null)
		{
			g2 = propertyDescriptorsCache.Find(dataColumn.ColumnName, ignoreCase: false) as g;
			if (g2 != null && g2.a() != dataColumn)
			{
				g2.a(dataColumn);
			}
		}
		if (g2 == null)
		{
			g2 = new g(dataColumn);
		}
		propertyDescriptors.Add(g2);
	}

	private void a(PropertyDescriptor[] A_0)
	{
		PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
		if (A_0 == null || A_0.Length == 0)
		{
			int count = Columns.Count;
			for (int i = 0; i < count; i++)
			{
				DataColumn dataColumn = Columns[i];
				AddPropertyDescriptor(propertyDescriptorCollection, dataColumn);
			}
			int count2 = base.ChildRelations.Count;
			for (int num = 0; num < count2; num++)
			{
				g value = new g(base.ChildRelations[num]);
				propertyDescriptorCollection.Add(value);
			}
		}
		propertyDescriptorsCache = propertyDescriptorCollection;
		ac = false;
	}

	internal PropertyDescriptorCollection c(PropertyDescriptor[] A_0)
	{
		return GetProperties(A_0);
	}

	protected virtual PropertyDescriptorCollection GetProperties(PropertyDescriptor[] listAccessors)
	{
		if (listAccessors != null && listAccessors.Length > 0)
		{
			int count = base.ChildRelations.Count;
			if (count == 0)
			{
				return new PropertyDescriptorCollection(null);
			}
			PropertyDescriptor propertyDescriptor = listAccessors[0];
			for (int i = 0; i < count; i++)
			{
				DataRelation dataRelation = base.ChildRelations[i];
				if (!(dataRelation.RelationName == propertyDescriptor.Name) || !(dataRelation.ChildTable is DbDataTable))
				{
					continue;
				}
				PropertyDescriptor[] array = null;
				if (listAccessors.Length > 1)
				{
					array = new PropertyDescriptor[listAccessors.Length - 1];
					for (int num = 1; num < listAccessors.Length; num++)
					{
						array[num - 1] = listAccessors[num];
					}
				}
				return ((DbDataTable)dataRelation.ChildTable).c(array);
			}
			return new PropertyDescriptorCollection(null);
		}
		if (propertyDescriptorsCache != null && !ac)
		{
			return propertyDescriptorsCache;
		}
		a(listAccessors);
		return propertyDescriptorsCache;
	}

	internal PropertyDescriptorCollection b(PropertyDescriptor[] A_0)
	{
		PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
		for (int i = 0; i < Columns.Count; i++)
		{
			g value = new g(Columns[i]);
			propertyDescriptorCollection.Add(value);
		}
		return propertyDescriptorCollection;
	}

	IList IListSource.GetList()
	{
		if (this.m_r == null)
		{
			this.m_r = new DbDataTableView(this, base.DefaultView);
		}
		return this.m_r;
	}

	public void Open()
	{
		if (m)
		{
			return;
		}
		m = true;
		this.m_n = true;
		try
		{
			ac = true;
			if (this.m_aa != 0)
			{
				a(z, this.m_aa);
				return;
			}
			CheckSelectCommand();
			CacheGetSchemaTable();
			ExecuteCommand();
			CheckColumnsCreated(throwOnEmptySchemaTable: true);
			if (Utils.IsEmpty(base.TableName))
			{
				DataTable dataTable = GetSchemaTable();
				if (dataTable.Rows.Count > 0)
				{
					string text = dataTable.Rows[0][SchemaTableColumn.BaseTableName].ToString();
					foreach (DataRow row in dataTable.Rows)
					{
						if (text != row[SchemaTableColumn.BaseTableName].ToString())
						{
							base.TableName = "Table";
							break;
						}
					}
					if (Utils.IsEmpty(base.TableName))
					{
						base.TableName = text;
					}
				}
				else
				{
					base.TableName = "Table";
				}
			}
			if (this.m_r != null)
			{
				this.m_r.j();
			}
			a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
			h();
		}
		catch
		{
			m = false;
			if (reader != null)
			{
				reader.Dispose();
			}
			reader = null;
			n();
			throw;
		}
		finally
		{
			this.m_n = false;
		}
	}

	protected virtual void CacheGetSchemaTable()
	{
		bool flag = j();
		try
		{
			GetSchemaTable();
		}
		catch
		{
			if (flag)
			{
				i();
			}
			throw;
		}
	}

	protected virtual void ClearSchemaTableCache()
	{
		schemaTable = null;
	}

	private void b(object A_0, CollectionChangeEventArgs A_1)
	{
		if (aj)
		{
			UserDefinedColumns = true;
		}
	}

	private void a(object A_0, CollectionChangeEventArgs A_1)
	{
		if (disableEvents <= 0)
		{
			readerMappings = null;
			CheckReaderMappings();
			if (A_1.Action == CollectionChangeAction.Add)
			{
				a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, Columns.Count - 1));
			}
			a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, Columns.Count - 1));
		}
	}

	protected virtual DataTable GetSchemaTable()
	{
		if (schemaTable == null)
		{
			if (reader == null || reader.IsClosed)
			{
				if (DataAdapterInternal.SelectCommand == null || DataAdapterInternal.SelectCommand.Connection == null)
				{
					return null;
				}
				using (IDataReader dataReader = DataAdapterInternal.SelectCommand.ExecuteReader(ExecuteCommBehavior | CommandBehavior.SingleResult | CommandBehavior.SchemaOnly | CommandBehavior.SequentialAccess))
				{
					schemaTable = dataReader.GetSchemaTable();
				}
				return schemaTable;
			}
			schemaTable = reader.GetSchemaTable();
		}
		return schemaTable;
	}

	protected virtual void CreateColumns()
	{
		bool flag;
		if (reader == null || reader.IsClosed)
		{
			flag = true;
			ac = true;
			CheckSelectCommand();
			ExecuteCommand();
		}
		else
		{
			flag = false;
		}
		disableEvents++;
		try
		{
			CreateColumnsInternal(throwOnEmptySchemaTable: true);
		}
		finally
		{
			disableEvents--;
			if (flag)
			{
				CloseReader();
			}
		}
	}

	protected virtual void CreateColumnsInternal(bool throwOnEmptySchemaTable)
	{
		DataTable dataTable = GetSchemaTable();
		if (dataTable == null)
		{
			if (throwOnEmptySchemaTable)
			{
				throw new ArgumentNullException("Unable to create column set because retrieving of schemaTable for the SELECT command failed.");
			}
			return;
		}
		indexOfColumnOnlyOriginalValue = -1;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		string text = null;
		DataColumn dataColumn = null;
		if (dataTable.Columns.Contains(SchemaTableOptionalColumn.IsHidden))
		{
			dataColumn = dataTable.Columns[SchemaTableOptionalColumn.IsHidden];
		}
		string[] array = new string[dataTable.Rows.Count];
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			array[i] = dataTable.Rows[i][SchemaTableColumn.ColumnName].ToString();
		}
		string[] indexedFieldNames = GetIndexedFieldNames(array);
		bool flag = false;
		for (int num = 0; num < dataTable.Rows.Count; num++)
		{
			DataRow dataRow = dataTable.Rows[num];
			if (dataColumn != null)
			{
				object obj = dataRow[dataColumn];
				if (obj is bool && (bool)obj)
				{
					continue;
				}
			}
			string text2 = indexedFieldNames[num];
			int num2 = 0;
			string text3 = text2;
			if (Utils.IsEmpty(text2))
			{
				text2 = "Column";
				text3 = "Column1";
				num2 = 1;
			}
			if (arrayList.IndexOf(text3.ToUpper()) >= 0)
			{
				bool flag2;
				do
				{
					num2++;
					text3 = text2 + num2;
					flag2 = arrayList.IndexOf(text3.ToUpper()) >= 0;
					if (flag2)
					{
						continue;
					}
					foreach (DataRow row in dataTable.Rows)
					{
						if (row[SchemaTableColumn.ColumnName].ToString() == text3)
						{
							flag2 = true;
							break;
						}
					}
				}
				while (flag2);
			}
			arrayList.Add(text3.ToUpper());
			Type type = ((!returnProviderSpecificTypesInternal) ? ((Type)dataRow[SchemaTableColumn.DataType]) : ((Type)dataRow[SchemaTableOptionalColumn.ProviderSpecificDataType]));
			DataColumn dataColumn2 = this.dataTable.Columns.Add(GetColumnNameFromMapping(text3), type);
			string text4 = dataRow[SchemaTableColumn.BaseTableName] as string;
			if (text == null)
			{
				if (text4 == null)
				{
					text = "";
					arrayList2 = null;
				}
				else
				{
					text = text4;
				}
			}
			else if (text4 != null && text != text4)
			{
				text = "";
				arrayList2 = null;
			}
			object obj2 = dataRow[SchemaTableColumn.IsKey];
			if (arrayList2 != null && (MissingSchemaAction & MissingSchemaAction.AddWithKey) == MissingSchemaAction.AddWithKey && obj2 is bool && (bool)obj2)
			{
				arrayList2.Add(dataColumn2);
				if (!flag)
				{
					object obj3 = dataRow[SchemaTableColumn.AllowDBNull];
					if (obj3 is bool && (bool)obj3)
					{
						flag = true;
					}
				}
			}
			object obj4 = dataRow[SchemaTableColumn.IsUnique];
			if ((MissingSchemaAction & MissingSchemaAction.AddWithKey) == MissingSchemaAction.AddWithKey && obj4 is bool && (bool)obj4 && (!(obj2 is bool) || !(bool)obj2))
			{
				dataColumn2.Unique = true;
			}
			dataColumn2.ReadOnly = (bool)dataRow[SchemaTableOptionalColumn.IsReadOnly];
			dataColumn2.AllowDBNull = (bool)dataRow[SchemaTableColumn.AllowDBNull];
			if (dataRow.Table.Columns.Contains(SchemaTableOptionalColumn.IsAutoIncrement) && Convert.ToBoolean(dataRow[SchemaTableOptionalColumn.IsAutoIncrement]) && dataColumn2.ReadOnly)
			{
				dataColumn2.AllowDBNull = true;
			}
			ColumnAdded(dataColumn2, dataRow, num);
		}
		if (arrayList2 == null || arrayList2.Count <= 0)
		{
			return;
		}
		if (!flag)
		{
			base.PrimaryKey = (DataColumn[])arrayList2.ToArray(typeof(DataColumn));
			return;
		}
		UniqueConstraint uniqueConstraint = new UniqueConstraint("", (DataColumn[])arrayList2.ToArray(typeof(DataColumn)));
		ConstraintCollection constraints = Constraints;
		_ = constraints.Count;
		for (int num3 = 0; num3 < constraints.Count; num3++)
		{
			if (uniqueConstraint.Equals(constraints[num3]))
			{
				uniqueConstraint = null;
				break;
			}
		}
		if (uniqueConstraint != null)
		{
			constraints.Add(uniqueConstraint);
		}
	}

	protected static string[] GetIndexedFieldNames(ICollection<string> fieldNames)
	{
		int count = fieldNames.Count;
		List<string> list = new List<string>(fieldNames);
		string[] array = new string[count];
		List<KeyValuePair<string, int>> list2 = new List<KeyValuePair<string, int>>(count / 2);
		for (int i = 0; i < count; i++)
		{
			string text = (array[i] = list[i]);
			if (Utils.IsEmpty(text))
			{
				list2.Add(new KeyValuePair<string, int>(text, i));
				continue;
			}
			for (int num = i + 1; num < count; num++)
			{
				if (string.Compare(text, list[num], StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					list2.Add(new KeyValuePair<string, int>(text, num));
					break;
				}
			}
		}
		foreach (KeyValuePair<string, int> item in list2)
		{
			string text2 = item.Key;
			string text3 = text2;
			int num2 = 1;
			if (Utils.IsEmpty(text2))
			{
				text2 = "Column";
				text3 = "Column1";
				num2 = 2;
			}
			int value = item.Value;
			while (a(list, text3))
			{
				text3 = text2 + num2;
				num2++;
			}
			array[value] = text3;
			list.Add(text3);
		}
		return array;
	}

	private static bool a(ICollection<string> A_0, string A_1)
	{
		foreach (string item in A_0)
		{
			if (string.Compare(A_1, item, StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return true;
			}
		}
		return false;
	}

	protected virtual void ColumnAdded(DataColumn column, DataRow schemaRow, int index)
	{
	}

	protected virtual void CheckColumnsCreated(bool throwOnEmptySchemaTable)
	{
		disableEvents++;
		try
		{
			ab = dataTable.Columns.Count != 0 || dataTable.Constraints.Count > 0 || dataTable.PrimaryKey.Length > 0;
			if (!UserDefinedColumns)
			{
				CreateColumnsInternal(throwOnEmptySchemaTable);
			}
			CheckReaderMappings();
		}
		finally
		{
			disableEvents--;
		}
	}

	public void CancelFetch()
	{
		SuspendFill(wait: false);
		if (!FetchComplete)
		{
			this.m_o = this.m_p + 1;
			try
			{
				CloseReader();
			}
			finally
			{
				a(new ListChangedEventArgs(ListChangedType.Reset, base.DefaultView.Count - 1));
			}
		}
	}

	protected virtual void CloseReader()
	{
		n();
		readerMappings = null;
		if (reader != null)
		{
			try
			{
				o();
			}
			finally
			{
				reader = null;
				this.m_k = false;
				this.m_l = true;
			}
		}
	}

	public void Close()
	{
		if (m)
		{
			a(A_0: true);
		}
	}

	public new void Clear()
	{
		a(A_0: false);
	}

	private void a(bool A_0)
	{
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		if (this.m_r != null)
		{
			this.m_r.m();
		}
		if (A_0)
		{
			CloseReader();
		}
		ClearSchemaTableCache();
		if (A_0 && base.DataSet != null && base.DataSet.Relations != null)
		{
			int count = base.DataSet.Relations.Count;
			for (int i = 0; i < count; i++)
			{
				DataRelation dataRelation = base.DataSet.Relations[i];
				if (dataRelation.ChildTable == this)
				{
					for (int num = 0; num < dataRelation.ChildColumns.Length; num++)
					{
						if (Columns.IndexOf(dataRelation.ChildColumns[num]) >= 0)
						{
							A_0 = false;
							break;
						}
					}
				}
				if (!A_0)
				{
					break;
				}
				if (dataRelation.ParentTable == this)
				{
					for (int num2 = 0; num2 < dataRelation.ParentColumns.Length; num2++)
					{
						if (Columns.IndexOf(dataRelation.ParentColumns[num2]) >= 0)
						{
							A_0 = false;
							break;
						}
					}
				}
				if (!A_0)
				{
					break;
				}
			}
		}
		if (A_0)
		{
			disableEvents++;
			try
			{
				DataColumn[] array = null;
				DataColumn[] array2 = null;
				Constraint[] array3 = null;
				if (UserDefinedColumns)
				{
					array2 = PrimaryKey;
					base.PrimaryKey = null;
					array = new DataColumn[Columns.Count];
					Columns.CopyTo(array, 0);
					array3 = new Constraint[Constraints.Count];
					Constraints.CopyTo(array3, 0);
				}
				ac = true;
				if (this.m_e != null && this.m_e is Control)
				{
					BindingManagerBase val = ((Control)this.m_e).BindingContext[(object)this];
					FieldInfo field = ((object)val).GetType().GetField("onCurrentItemChangedHandler", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
					if ((object)field != null)
					{
						MulticastDelegate multicastDelegate = (MulticastDelegate)field.GetValue(val);
						if ((object)multicastDelegate != null)
						{
							Delegate[] invocationList = multicastDelegate.GetInvocationList();
							for (int num3 = invocationList.Length - 1; num3 >= 0; num3--)
							{
								Delegate obj = (MulticastDelegate)invocationList[num3];
								if (obj.Target.GetType().Name == "RelatedCurrencyManager")
								{
									multicastDelegate = (MulticastDelegate)Delegate.Remove(multicastDelegate, obj);
								}
							}
						}
						field.SetValue(val, multicastDelegate);
					}
				}
				base.PrimaryKey = null;
				Constraints.Clear();
				Columns.Clear();
				base.Clear();
				Reset();
				if (array != null)
				{
					Columns.AddRange(array);
				}
				if (array2 != null)
				{
					base.PrimaryKey = array2;
				}
				if (array3 != null)
				{
					foreach (Constraint constraint in array3)
					{
						bool flag = false;
						int count2 = Constraints.Count;
						for (int num5 = 0; num5 < count2; num5++)
						{
							Constraint constraint2 = Constraints[num5];
							if (constraint2.Equals(constraint))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							Constraints.Add(constraint);
						}
					}
				}
			}
			finally
			{
				disableEvents--;
			}
			UserDefinedColumns = false;
		}
		else if (base.DataSet != null)
		{
			bool enforceConstraints = base.DataSet.EnforceConstraints;
			base.DataSet.EnforceConstraints = false;
			base.Clear();
			base.DataSet.EnforceConstraints = enforceConstraints;
		}
		else
		{
			base.Clear();
		}
		n();
		currenSelectCommand = null;
		if (detailSelectCommand != null)
		{
			detailSelectCommand.Dispose();
			detailSelectCommand = null;
		}
		m = false;
		a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
		a(new ListChangedEventArgs(ListChangedType.Reset, null));
	}

	private void n()
	{
		this.m_p = -1;
		this.m_q = true;
		this.m_o = 0;
	}

	internal DataRow a(int A_0)
	{
		FetchToPosition(A_0);
		if (A_0 >= dataTable.Rows.Count)
		{
			return null;
		}
		return dataTable.Rows[A_0];
	}

	internal void a(ref DataRow A_0, bool A_1)
	{
		if (A_1)
		{
			p();
		}
		if (!this.m_v)
		{
			a(A_0, DataRowAction.Add);
		}
		DataRowChangeEventArgs e = new DataRowChangeEventArgs(A_0, DataRowAction.Add);
		OnRowChanging(e);
		disableEvents++;
		disableUpdateEvents++;
		try
		{
			DataRow dataRow = null;
			object[] itemArray = DbDataRowView.a(A_0, A_1: true);
			try
			{
				if (!this.m_v)
				{
					dataTable.BeginLoadData();
				}
				try
				{
					if (!this.m_v)
					{
						storeEvents++;
						try
						{
							dataRow = dataTable.Rows.Add(DbDataRowView.a(A_0, A_1: true));
						}
						finally
						{
							storeEvents--;
						}
						try
						{
							storeEvents++;
							try
							{
								a(dataRow);
							}
							finally
							{
								storeEvents--;
							}
							b(A_0, dataRow);
						}
						catch
						{
							throw;
						}
						dataTable.Rows.Add(A_0);
					}
					else
					{
						storeEvents++;
						try
						{
							dataTable.Rows.Add(A_0);
						}
						finally
						{
							storeEvents--;
						}
					}
				}
				finally
				{
					if (!this.m_v && dataRow != null)
					{
						dataRow.Delete();
						if (dataRow.RowState == DataRowState.Deleted)
						{
							dataRow.AcceptChanges();
						}
						if (base.DefaultView.Count == 0)
						{
							DataRowView dataRowView = base.DefaultView.AddNew();
							DataRow row = dataRowView.Row;
							dataRowView.BeginEdit();
							row.BeginEdit();
							row.ItemArray = itemArray;
							A_0 = row;
						}
					}
				}
			}
			finally
			{
				if (!this.m_v)
				{
					storeEvents++;
					try
					{
						dataTable.EndLoadData();
					}
					finally
					{
						storeEvents--;
					}
				}
			}
		}
		finally
		{
			disableEvents--;
			disableUpdateEvents--;
		}
		if (!this.m_v)
		{
			A_0.AcceptChanges();
		}
		OnRowChanged(e);
		this.m_o++;
		if (!FetchComplete)
		{
			this.m_p++;
		}
	}

	private void a(DataRow A_0, DataRowAction A_1)
	{
		try
		{
			if (!A_0.HasVersion(DataRowVersion.Proposed))
			{
				return;
			}
			int count = Columns.Count;
			if (count > 0)
			{
				MethodInfo method = typeof(DataColumn).GetMethod("CheckColumnConstraint", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
				if ((object)method != null)
				{
					for (int i = 0; i < count; i++)
					{
						DataColumn dataColumn = Columns[i];
						if (dataColumn.Expression == "" || A_1 != DataRowAction.Add)
						{
							method.Invoke(dataColumn, new object[2] { A_0, A_1 });
						}
					}
				}
			}
			int count2 = Constraints.Count;
			if (count2 <= 0)
			{
				return;
			}
			MethodInfo method2 = typeof(Constraint).GetMethod("CheckConstraint", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new Type[2]
			{
				typeof(DataRow),
				typeof(DataRowAction)
			}, null);
			if ((object)method2 != null)
			{
				for (int num = 0; num < count2; num++)
				{
					method2.Invoke(Constraints[num], new object[2] { A_0, A_1 });
				}
			}
		}
		catch (TargetInvocationException ex)
		{
			if (ex.InnerException != null)
			{
				throw ex.InnerException;
			}
			throw;
		}
	}

	internal void b(DataRow A_0)
	{
		if (ax == A_0)
		{
			return;
		}
		ax = A_0;
		try
		{
			if (this.m_v)
			{
				return;
			}
			a(A_0, DataRowAction.Change);
			disableEvents++;
			disableUpdateEvents++;
			DataRow dataRow = null;
			DbDataRowView.a(A_0, A_1: true);
			try
			{
				object a_ = DependentColumnsInternal;
				DependentColumnsInternal = null;
				try
				{
					dataTable.BeginLoadData();
					try
					{
						object[] values = ((!A_0.HasVersion(DataRowVersion.Original)) ? DbDataRowView.a(A_0, A_1: true) : DbDataRowView.a(A_0, DataRowVersion.Original));
						dataRow = dataTable.Rows.Add(values);
						dataRow.AcceptChanges();
						dataRow.BeginEdit();
						object[] array = DbDataRowView.a(A_0, A_1: false);
						for (int i = 0; i < array.Length; i++)
						{
							object obj = dataRow[i];
							object obj2 = array[i];
							if (obj != obj2 && (obj == null || obj2 == null || !obj.Equals(obj2)))
							{
								DataColumn dataColumn = Columns[i];
								if (!dataColumn.ReadOnly)
								{
									dataRow[i] = array[i];
								}
							}
						}
						storeEvents++;
						try
						{
							dataRow.EndEdit();
							a(dataRow);
						}
						finally
						{
							storeEvents--;
							a(dataRow, A_0);
						}
						b(A_0, dataRow);
					}
					finally
					{
						if (dataRow != null)
						{
							dataRow.Delete();
							if (dataRow.RowState == DataRowState.Deleted)
							{
								dataRow.AcceptChanges();
							}
						}
						disableEvents--;
						disableUpdateEvents--;
					}
				}
				finally
				{
					DependentColumnsInternal = a_;
				}
			}
			finally
			{
				dataTable.EndLoadData();
			}
			disableEvents++;
			A_0.AcceptChanges();
			disableEvents--;
		}
		finally
		{
			ax = null;
		}
	}

	private static bool b(DataRow A_0, DataRow A_1)
	{
		bool result = false;
		DataColumnCollection columns = A_0.Table.Columns;
		object[] array = new object[columns.Count];
		for (int i = 0; i < array.Length; i++)
		{
			DataColumn dataColumn = columns[i];
			if (A_0[i] != A_1[i] && (A_0.RowState == DataRowState.Detached || !dataColumn.ReadOnly))
			{
				A_0[i] = A_1[i];
				result = true;
			}
		}
		return result;
	}

	internal void b(int A_0)
	{
		DataRow dataRow = base.Rows[A_0];
		p();
		dataRow.Delete();
		if (!this.m_v)
		{
			a(dataRow);
		}
	}

	internal void c(DataRow A_0)
	{
		if (!AllowCruidDuringFetch)
		{
			p();
		}
		if (!this.m_v)
		{
			object[] values = DbDataRowView.a(A_0, DataRowVersion.Original);
			disableEvents++;
			disableUpdateEvents++;
			try
			{
				object a_ = DependentColumnsInternal;
				DependentColumnsInternal = null;
				try
				{
					dataTable.BeginLoadData();
					DataRow dataRow = dataTable.Rows.Add(values);
					dataRow.AcceptChanges();
					dataRow.Delete();
					try
					{
						a(dataRow);
					}
					finally
					{
						if (dataRow.RowState != DataRowState.Detached)
						{
							dataRow.AcceptChanges();
						}
					}
				}
				finally
				{
					DependentColumnsInternal = a_;
				}
			}
			finally
			{
				dataTable.EndLoadData();
				disableEvents--;
				disableUpdateEvents--;
			}
		}
		storeEvents++;
		try
		{
			A_0.Delete();
		}
		finally
		{
			storeEvents--;
		}
		if (!this.m_v)
		{
			A_0.AcceptChanges();
		}
		if (!FetchComplete)
		{
			this.m_p--;
			this.m_o--;
		}
	}

	protected virtual void CreateDataAdapter()
	{
		dataAdapter.SelectCommand = currenSelectCommand;
		dataAdapter.InsertCommand = this.h;
		dataAdapter.UpdateCommand = this.i;
		dataAdapter.DeleteCommand = this.j;
		dataAdapter.MissingSchemaAction = at;
		dataAdapter.FillError += a;
		dataAdapter.ReturnProviderSpecificTypes = returnProviderSpecificTypesInternal;
		if (u != null)
		{
			string sourceTable = u.SourceTable;
			dataAdapter.TableMappings.Add(u);
			u.SourceTable = sourceTable;
		}
		commandBuilder.ConflictOption = ConflictOption.CompareAllSearchableValues;
		commandBuilder.DataAdapter = dataAdapter;
		commandBuilder.RefreshMode = RefreshMode;
	}

	protected void CheckDataAdapterCreated()
	{
		if (dataAdapter == null)
		{
			CreateDataAdapter();
		}
	}

	public void FillSchema()
	{
		CheckSelectCommand();
		DataAdapterInternal.FillSchema(this, SchemaType.Source);
	}

	private int a(int A_0, int A_1)
	{
		ConnectionState connectionState = ConnectionState.Open;
		DbCommand selectCommand = dataAdapter.SelectCommand;
		try
		{
			if (selectCommand.Connection != null)
			{
				connectionState = selectCommand.Connection.State;
				if (connectionState == ConnectionState.Closed)
				{
					selectCommand.Connection.Open();
				}
			}
			Clear();
			int result = FillPage(A_0, A_1, null);
			m = true;
			if (w)
			{
				this.m_o = GetRecordCount();
			}
			return result;
		}
		finally
		{
			if (connectionState == ConnectionState.Closed)
			{
				selectCommand.Connection.Close();
			}
		}
	}

	public void RefreshRow(DataRow row)
	{
		IDbCommand dbCommand = CommandBuilderInternal.a(null, u, row, A_3: false, null);
		IDataReader dataReader = dbCommand.ExecuteReader(ExecuteCommBehavior);
		try
		{
			if (!dataReader.Read())
			{
				return;
			}
			int fieldCount = dataReader.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				string text = dataReader.GetName(i);
				if (u != null)
				{
					int num = u.ColumnMappings.IndexOf(text);
					if (num >= 0)
					{
						text = u.ColumnMappings[num].DataSetColumn;
					}
				}
				int num2 = Columns.IndexOf(text);
				if (num2 != -1)
				{
					GetField(row, num2, dataReader, i);
				}
			}
		}
		finally
		{
			dataReader.Close();
		}
	}

	public int FillPage(int startRecord, int maxRecords)
	{
		return FillPage(startRecord, maxRecords, null);
	}

	public int FillPage(int startRecord, int maxRecords, object[] parameterValues)
	{
		CloseReader();
		ClearSchemaTableCache();
		CheckSelectCommand();
		a(parameterValues);
		if (this.m_r != null)
		{
			this.m_r.j();
		}
		disableEvents++;
		ConnectionState connectionState = ConnectionState.Open;
		DbCommand selectCommand = dataAdapter.SelectCommand;
		int result;
		try
		{
			if (selectCommand.Connection != null)
			{
				connectionState = selectCommand.Connection.State;
				if (connectionState == ConnectionState.Closed)
				{
					selectCommand.Connection.Open();
				}
			}
			au++;
			try
			{
				result = FillPage(selectCommand, startRecord, maxRecords);
			}
			finally
			{
				r();
			}
			this.m_o = base.Rows.Count;
		}
		finally
		{
			disableEvents--;
			if (connectionState == ConnectionState.Closed)
			{
				selectCommand.Connection.Close();
			}
		}
		a(new ListChangedEventArgs(ListChangedType.Reset, null));
		a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
		if (commandBuilder != null)
		{
			commandBuilder.RefreshSchema();
		}
		return result;
	}

	protected void CheckSelectCommand()
	{
		if (this.g == null)
		{
			throw new InvalidOperationException(Devart.Common.n.a("SelectCommandNotInit"));
		}
		DbConnection dbConnection = GetConnection();
		if (dbConnection != null && (this.g.Connection == null || this.g.Connection != dbConnection))
		{
			this.g.Connection = dbConnection;
		}
		if (this.g.Connection == null)
		{
			throw new InvalidOperationException(Devart.Common.n.a("ConnectionNotInit"));
		}
		detailSelectCommand = a(this.g.Connection);
		if (detailSelectCommand != null)
		{
			currenSelectCommand = detailSelectCommand;
		}
		else
		{
			currenSelectCommand = this.g;
		}
	}

	private static PropertyDescriptor a(PropertyDescriptorCollection A_0, string A_1)
	{
		PropertyDescriptor propertyDescriptor = null;
		if (A_0 != null)
		{
			for (int i = 0; i < A_0.Count; i++)
			{
				if (string.Compare(A_0[i].Name, A_1, ignoreCase: false) == 0)
				{
					propertyDescriptor = A_0[i];
					break;
				}
			}
			if (propertyDescriptor != null)
			{
				return propertyDescriptor;
			}
			for (int num = 0; num < A_0.Count; num++)
			{
				if (string.Compare(A_0[num].Name, A_1, ignoreCase: true) == 0)
				{
					if (propertyDescriptor != null)
					{
						propertyDescriptor = null;
						break;
					}
					propertyDescriptor = A_0[num];
				}
			}
		}
		return propertyDescriptor;
	}

	private DbCommand a(DbConnection A_0)
	{
		if (!ParentDataRelation.a(ae))
		{
			return null;
		}
		ae.c();
		if (this.g.CommandType != CommandType.StoredProcedure)
		{
			string text = ((this.g.CommandType != CommandType.TableDirect) ? this.g.CommandText : ("SELECT * FROM " + this.g.CommandText + " WHERE "));
			PropertyDescriptorCollection itemProperties = ((ITypedList)((IListSource)ae.ParentTable).GetList()).GetItemProperties(null);
			if (ad.j() >= 0)
			{
				_ = (DbDataRowView)((IListSource)ae.ParentTable).GetList()[ad.j()];
			}
			string text2 = "";
			for (int i = 0; i < ae.ParentColumnNames.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = a(itemProperties, ae.ParentColumnNames[i]);
				if (propertyDescriptor == null)
				{
					throw new Exception("Can not establish master/detail relation");
				}
				if (this.g.Parameters.IndexOf(propertyDescriptor.Name) < 0)
				{
					if (text2 != "")
					{
						text2 += " AND ";
					}
					string text3 = ae.ChildColumnNames[i];
					if (text3 == null || text3 == "")
					{
						throw new Exception("Can not establish master/detail relation");
					}
					object obj = text2;
					text2 = string.Concat(new object[5]
					{
						obj,
						ae.ChildColumnNames[i],
						" = ",
						GetParameterPlaceholder(),
						propertyDescriptor.Name
					});
				}
			}
			DbCommandBase dbCommandBase = (DbCommandBase)A_0.CreateCommand();
			if (this.g.CommandType == CommandType.TableDirect)
			{
				text += text2;
			}
			else if (text2 != "")
			{
				text = AddWhere(text, text2);
			}
			dbCommandBase.CommandText = text;
			dbCommandBase.CreateParameters();
			return dbCommandBase;
		}
		return null;
	}

	private void l()
	{
		if (!ParentDataRelation.a(ae))
		{
			return;
		}
		ae.c();
		if (currenSelectCommand.CommandType == CommandType.TableDirect)
		{
			throw new Exception("Can not establish master/detail relation");
		}
		DbDataRowView component = null;
		PropertyDescriptorCollection itemProperties = ((ITypedList)((IListSource)ae.ParentTable).GetList()).GetItemProperties(null);
		if (ad.j() >= 0)
		{
			component = (DbDataRowView)((IListSource)ae.ParentTable).GetList()[ad.j()];
		}
		string[] parentColumnNames = ae.ParentColumnNames;
		IDataParameterCollection parameters = currenSelectCommand.Parameters;
		for (int i = 0; i < parentColumnNames.Length; i++)
		{
			string parameterName = GetParameterName(parentColumnNames[i]);
			int num = parameters.IndexOf(parameterName);
			if (num < 0)
			{
				throw new Exception("Can not establish master/detail relation");
			}
			DbParameter dbParameter = (DbParameter)parameters[num];
			if (itemProperties == null)
			{
				dbParameter.Value = null;
				continue;
			}
			PropertyDescriptor propertyDescriptor = a(itemProperties, parentColumnNames[i]);
			if (propertyDescriptor == null)
			{
				throw new Exception("Can not establish master/detail relation");
			}
			dbParameter.Value = propertyDescriptor.GetValue(component);
		}
	}

	private void a(object[] A_0)
	{
		if (A_0 != null)
		{
			if (currenSelectCommand == null)
			{
				throw new InvalidOperationException(Devart.Common.n.a("SelectCommandNotInit"));
			}
			for (int i = 0; i < A_0.Length; i++)
			{
				a(currenSelectCommand, i).Value = A_0[i];
			}
		}
	}

	private void a(object A_0, int A_1)
	{
		if (!m || this.m_n)
		{
			return;
		}
		if (!ParentDataRelation.a(ae) || ae.ChildColumnNames == null || ae.ChildColumnNames.Length == 0 || ae.ParentColumnNames == null || ae.ParentColumnNames.Length == 0)
		{
			Close();
			return;
		}
		ae.c();
		disableEvents++;
		try
		{
			g();
		}
		finally
		{
			disableEvents--;
		}
		ExecuteCommand();
		CheckReaderMappings();
		if (this.m_r != null)
		{
			this.m_r.j();
		}
		a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
		h();
	}

	protected virtual void ExecuteCommand()
	{
		ak = true;
		bool flag = currenSelectCommand.Connection.State == ConnectionState.Closed;
		try
		{
			k();
			reader = currenSelectCommand.ExecuteReader(ExecuteCommBehavior);
			this.m_l = false;
			this.m_k = false;
		}
		catch
		{
			if (flag)
			{
				currenSelectCommand.Connection.Close();
			}
			throw;
		}
	}

	protected void Prepare()
	{
		if (w)
		{
			if (this.g == null)
			{
				throw new QueryRecordCountException(Devart.Common.n.a("SelectCommandNotDefined"));
			}
			CheckSelectCommand();
			k();
		}
		ak = true;
	}

	private void k()
	{
		j();
		l();
		if (w)
		{
			this.m_o = GetRecordCount();
		}
	}

	private bool j()
	{
		if (currenSelectCommand.Connection.State == ConnectionState.Closed)
		{
			currenSelectCommand.Connection.Open();
			return true;
		}
		return false;
	}

	private void i()
	{
		currenSelectCommand.Connection.Close();
	}

	protected int GetRecordCount()
	{
		return ((DbCommandBase)currenSelectCommand).GetRecordCount();
	}

	protected virtual char GetParameterPlaceholder()
	{
		return ':';
	}

	protected virtual string GetParameterName(string parameterName)
	{
		return parameterName;
	}

	private void h()
	{
		if (ag)
		{
			if (af)
			{
				d(A_0: true);
				BeginFill(null, null);
			}
			else
			{
				p();
			}
		}
		else
		{
			d(A_0: true);
			if (base.ParentRelations.Count > 0)
			{
				FetchToPosition(1);
			}
		}
	}

	private void g()
	{
		if (this.m_r != null)
		{
			this.m_r.m();
		}
		CloseReader();
		disableEvents++;
		try
		{
			ac = true;
			base.Clear();
		}
		finally
		{
			disableEvents--;
		}
		this.m_p = -1;
		this.m_q = true;
		this.m_o = 0;
		a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
		a(new ListChangedEventArgs(ListChangedType.Reset, null));
	}

	private static IDataParameter a(DbCommand A_0, int A_1)
	{
		if (A_1 < A_0.Parameters.Count)
		{
			return A_0.Parameters[A_1];
		}
		IDataParameter dataParameter = A_0.CreateParameter();
		A_0.Parameters.Add(dataParameter);
		return dataParameter;
	}

	private void f()
	{
		DbConnection dbConnection = GetConnection();
		if (dbConnection == null && currenSelectCommand != null)
		{
			dbConnection = currenSelectCommand.Connection;
		}
		if (dbConnection != null)
		{
			if (currenSelectCommand != null && currenSelectCommand.Connection == null)
			{
				currenSelectCommand.Connection = dbConnection;
			}
			if (this.h != null && this.h.Connection == null)
			{
				this.h.Connection = dbConnection;
			}
			if (this.i != null && this.i.Connection == null)
			{
				this.i.Connection = dbConnection;
			}
			if (this.j != null && this.j.Connection == null)
			{
				this.j.Connection = dbConnection;
			}
		}
	}

	private int a(DataRow A_0)
	{
		if (this.m_r != null && this.m_r.CurrentIndex >= 0)
		{
			CommandBuilderInternal.SetAllValues = hasComplexFields;
		}
		else
		{
			CommandBuilderInternal.SetAllValues = false;
		}
		BeforeUpdatingRow(A_0);
		DataRow[] dataRows = new DataRow[1] { A_0 };
		f();
		try
		{
			return DataAdapterInternal.Update(dataRows);
		}
		finally
		{
			AfterUpdatingRow(A_0);
		}
	}

	protected virtual void BeforeUpdatingRow(DataRow row)
	{
	}

	protected virtual void AfterUpdatingRow(DataRow row)
	{
	}

	public virtual int Update()
	{
		f();
		return DataAdapterInternal.Update(this);
	}

	public int UpdateRows(DataRow[] datarows)
	{
		f();
		return DataAdapterInternal.Update(datarows);
	}

	protected virtual DbCommand CloneCommand(DbCommand command)
	{
		if (!(command is ICloneable))
		{
			return null;
		}
		return (DbCommand)((ICloneable)command).Clone();
	}

	internal DbDataTable a(DbDataTable A_0)
	{
		A_0.Connection = Connection;
		A_0.StartRecord = StartRecord;
		A_0.MaxRecords = MaxRecords;
		A_0.SelectCommand = CloneCommand(SelectCommand);
		A_0.InsertCommand = CloneCommand(InsertCommand);
		A_0.UpdateCommand = CloneCommand(UpdateCommand);
		A_0.DeleteCommand = CloneCommand(DeleteCommand);
		if ((object)A_0.GetType().BaseType.BaseType != typeof(DbDataTable))
		{
			A_0.TableMapping.DataSetTable = TableMapping.DataSetTable;
			A_0.TableMapping.SourceTable = TableMapping.SourceTable;
			int count = TableMapping.ColumnMappings.Count;
			for (int i = 0; i < count; i++)
			{
				DataColumnMapping dataColumnMapping = TableMapping.ColumnMappings[i];
				DataColumnMapping value = new DataColumnMapping(dataColumnMapping.SourceColumn, dataColumnMapping.DataSetColumn);
				A_0.TableMapping.ColumnMappings.Add(value);
			}
		}
		A_0.ReturnProviderSpecificTypes = ReturnProviderSpecificTypes;
		A_0.CachedUpdates = CachedUpdates;
		A_0.NonBlocking = NonBlocking;
		A_0.QueryRecordCount = QueryRecordCount;
		A_0.RefreshMode = RefreshMode;
		A_0.RefreshingFields = RefreshingFields;
		A_0.Quoted = Quoted;
		A_0.UpdatingTable = UpdatingTable;
		A_0.FetchAll = FetchAll;
		return A_0;
	}

	public override DataTable Clone()
	{
		Hashtable hashtable = new Hashtable();
		for (int i = 0; i < Columns.Count; i++)
		{
			DataColumn dataColumn = Columns[i];
			if (dataColumn.Expression != null && dataColumn.Expression != "")
			{
				hashtable[dataColumn] = dataColumn.Expression;
				dataColumn.Expression = "";
			}
		}
		try
		{
			DbDataTable dbDataTable = (DbDataTable)base.Clone();
			a(dbDataTable);
			foreach (DictionaryEntry item in hashtable)
			{
				try
				{
					dbDataTable.Columns[((DataColumn)item.Key).ColumnName].Expression = (string)item.Value;
				}
				catch
				{
				}
			}
			return dbDataTable;
		}
		finally
		{
			foreach (DictionaryEntry item2 in hashtable)
			{
				((DataColumn)item2.Key).Expression = (string)item2.Value;
			}
		}
	}

	public new DataRow[] Select()
	{
		if (m)
		{
			FetchToPosition(int.MaxValue);
		}
		return base.Select();
	}

	public new DataRow[] Select(string filterExpression)
	{
		if (m)
		{
			FetchToPosition(int.MaxValue);
		}
		return base.Select(filterExpression);
	}

	public new DataRow[] Select(string filterExpression, string sort)
	{
		if (m)
		{
			FetchToPosition(int.MaxValue);
		}
		return base.Select(filterExpression, sort);
	}

	public new DataRow[] Select(string filterExpression, string sort, DataViewRowState recordStates)
	{
		if (m)
		{
			FetchToPosition(int.MaxValue);
		}
		return base.Select(filterExpression, sort, recordStates);
	}

	internal static void a(DataTable A_0, DbDataTable A_1)
	{
		A_1.TableName = A_0.TableName;
		A_1.Namespace = A_0.Namespace;
		A_1.Prefix = A_0.Prefix;
		A_1.Locale = A_0.Locale;
		A_1.CaseSensitive = A_0.CaseSensitive;
		A_1.DisplayExpression = A_0.DisplayExpression;
		A_1.MinimumCapacity = A_0.MinimumCapacity;
		A_1.RemotingFormat = A_0.RemotingFormat;
		DataColumnCollection columns = A_0.Columns;
		for (int i = 0; i < columns.Count; i++)
		{
			A_1.Columns.Add(a(columns[i]));
		}
		for (int num = 0; num < columns.Count; num++)
		{
			A_1.Columns[columns[num].ColumnName].Expression = columns[num].Expression;
		}
		DataColumn[] primaryKey = A_0.PrimaryKey;
		if (primaryKey.Length > 0)
		{
			DataColumn[] array = new DataColumn[primaryKey.Length];
			for (int num2 = 0; num2 < primaryKey.Length; num2++)
			{
				array[num2] = A_1.Columns[primaryKey[num2].Ordinal];
			}
			((DataTable)A_1).PrimaryKey = array;
		}
		for (int num3 = 0; num3 < A_0.Constraints.Count; num3++)
		{
			ForeignKeyConstraint foreignKeyConstraint = A_0.Constraints[num3] as ForeignKeyConstraint;
			UniqueConstraint uniqueConstraint = A_0.Constraints[num3] as UniqueConstraint;
			if (foreignKeyConstraint != null)
			{
				if (foreignKeyConstraint.Table == foreignKeyConstraint.RelatedTable)
				{
					A_1.Constraints.Add(a(foreignKeyConstraint));
				}
			}
			else
			{
				if (uniqueConstraint == null)
				{
					continue;
				}
				string[] array2 = new string[uniqueConstraint.Columns.Length];
				for (int num4 = 0; num4 < uniqueConstraint.Columns.Length; num4++)
				{
					array2[num4] = uniqueConstraint.Columns[num4].ColumnName;
				}
				UniqueConstraint uniqueConstraint2 = new UniqueConstraint(uniqueConstraint.ConstraintName, array2, uniqueConstraint.IsPrimaryKey);
				foreach (object key in uniqueConstraint.ExtendedProperties.Keys)
				{
					uniqueConstraint2.ExtendedProperties[key] = uniqueConstraint.ExtendedProperties[key];
				}
				A_1.Constraints.Add(a(uniqueConstraint));
			}
		}
		for (int num5 = 0; num5 < A_0.Constraints.Count; num5++)
		{
			if (A_1.Constraints.Contains(A_0.Constraints[num5].ConstraintName))
			{
				continue;
			}
			ForeignKeyConstraint foreignKeyConstraint2 = A_0.Constraints[num5] as ForeignKeyConstraint;
			UniqueConstraint uniqueConstraint3 = A_0.Constraints[num5] as UniqueConstraint;
			if (foreignKeyConstraint2 != null)
			{
				if (foreignKeyConstraint2.Table == foreignKeyConstraint2.RelatedTable)
				{
					A_1.Constraints.Add(a(foreignKeyConstraint2));
				}
			}
			else if (uniqueConstraint3 != null)
			{
				A_1.Constraints.Add(a(uniqueConstraint3));
			}
		}
		if (A_0.ExtendedProperties == null)
		{
			return;
		}
		foreach (object key2 in A_0.ExtendedProperties.Keys)
		{
			A_1.ExtendedProperties[key2] = A_0.ExtendedProperties[key2];
		}
	}

	private static ForeignKeyConstraint a(ForeignKeyConstraint A_0)
	{
		string[] array = new string[A_0.RelatedColumns.Length];
		for (int i = 0; i < A_0.RelatedColumns.Length; i++)
		{
			array[i] = A_0.RelatedColumns[i].ColumnName;
		}
		string[] childColumnNames = new string[A_0.Columns.Length];
		for (int num = 0; num < A_0.Columns.Length; num++)
		{
			array[num] = A_0.Columns[num].ColumnName;
		}
		ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(A_0.ConstraintName, A_0.RelatedTable.TableName, A_0.RelatedTable.Namespace, array, childColumnNames, A_0.AcceptRejectRule, A_0.DeleteRule, A_0.UpdateRule);
		foreach (object key in A_0.ExtendedProperties.Keys)
		{
			foreignKeyConstraint.ExtendedProperties[key] = A_0.ExtendedProperties[key];
		}
		return foreignKeyConstraint;
	}

	private static UniqueConstraint a(UniqueConstraint A_0)
	{
		string[] array = new string[A_0.Columns.Length];
		for (int i = 0; i < A_0.Columns.Length; i++)
		{
			array[i] = A_0.Columns[i].ColumnName;
		}
		UniqueConstraint uniqueConstraint = new UniqueConstraint(A_0.ConstraintName, array, A_0.IsPrimaryKey);
		foreach (object key in A_0.ExtendedProperties.Keys)
		{
			uniqueConstraint.ExtendedProperties[key] = A_0.ExtendedProperties[key];
		}
		return uniqueConstraint;
	}

	private static DataColumn a(DataColumn A_0)
	{
		DataColumn dataColumn = new DataColumn();
		dataColumn.AllowDBNull = A_0.AllowDBNull;
		dataColumn.AutoIncrement = A_0.AutoIncrement;
		dataColumn.AutoIncrementStep = A_0.AutoIncrementStep;
		dataColumn.AutoIncrementSeed = A_0.AutoIncrementSeed;
		dataColumn.Caption = A_0.Caption;
		dataColumn.ColumnName = A_0.ColumnName;
		dataColumn.Prefix = A_0.Prefix;
		dataColumn.DataType = A_0.DataType;
		dataColumn.DefaultValue = A_0.DefaultValue;
		dataColumn.ColumnMapping = A_0.ColumnMapping;
		dataColumn.ReadOnly = A_0.ReadOnly;
		dataColumn.MaxLength = A_0.MaxLength;
		dataColumn.DateTimeMode = A_0.DateTimeMode;
		if (A_0.ExtendedProperties != null)
		{
			foreach (object key in A_0.ExtendedProperties.Keys)
			{
				dataColumn.ExtendedProperties[key] = A_0.ExtendedProperties[key];
			}
		}
		return dataColumn;
	}

	private void a(object A_0, EventArgs A_1)
	{
		if (!fInitInProgress)
		{
			((EventHandler)Events[an])?.Invoke(A_0, null);
		}
	}

	protected override void OnTableClearing(DataTableClearEventArgs e)
	{
		a((object)this, (EventArgs)null);
		if (disableEvents <= 0)
		{
			base.OnTableClearing(e);
		}
	}

	protected override void OnRowDeleting(DataRowChangeEventArgs e)
	{
		if (disableUpdateEvents <= 0)
		{
			if (disableEvents <= 0)
			{
				a((object)this, (EventArgs)null);
			}
			base.OnRowDeleting(e);
		}
	}

	protected override void OnTableNewRow(DataTableNewRowEventArgs e)
	{
		a((object)this, (EventArgs)null);
		if (disableUpdateEvents <= 0)
		{
			base.OnTableNewRow(e);
		}
	}

	protected void SetOwnerReal(object value)
	{
		bool flag = this.m_e != value;
		if (this.m_e != null && !DesignMode)
		{
			GlobalComponentsCache.RemoveFromGlobalList(this);
		}
		this.m_e = value;
		if (this.m_e != null && !DesignMode)
		{
			GlobalComponentsCache.AddToGlobalList(this);
		}
		ad.b(this.m_e);
		if (flag)
		{
			a((object)this, (EventArgs)null);
		}
	}

	internal void x()
	{
		ParentRelation = null;
	}

	internal object b(object A_0, Type A_1, IEditableObject A_2, PropertyDescriptor A_3)
	{
		return GetPropertyValue(A_0, A_1, A_2, A_3);
	}

	protected virtual object GetPropertyValue(object obj, Type objType, IEditableObject objectItemView, PropertyDescriptor propertyDescriptor)
	{
		return obj;
	}

	internal object a(object A_0, Type A_1, IEditableObject A_2, PropertyDescriptor A_3)
	{
		return GetViewValue(A_0, A_1, A_2, A_3);
	}

	protected virtual object GetViewValue(object obj, Type objType, IEditableObject objectItemView, PropertyDescriptor propertyDescriptor)
	{
		return obj;
	}

	internal Type a(Type A_0)
	{
		return GetPropertyType(A_0);
	}

	protected virtual Type GetPropertyType(Type objType)
	{
		return objType;
	}

	internal void d(DataRow A_0)
	{
		InitNewRow(A_0);
	}

	protected virtual void InitNewRow(DataRow newRow)
	{
		if (!ParentDataRelation.a(ae))
		{
			return;
		}
		ae.c();
		string[] parentColumnNames = ae.ParentColumnNames;
		string[] childColumnNames = ae.ChildColumnNames;
		DbDataRowView component = null;
		PropertyDescriptorCollection itemProperties = ((ITypedList)((IListSource)ae.ParentTable).GetList()).GetItemProperties(null);
		if (ad.j() >= 0)
		{
			component = (DbDataRowView)((IListSource)ae.ParentTable).GetList()[ad.j()];
		}
		for (int i = 0; i < parentColumnNames.Length; i++)
		{
			int num = Columns.IndexOf(childColumnNames[i]);
			if (num < 0)
			{
				continue;
			}
			DataColumn column = Columns[num];
			if (itemProperties == null)
			{
				newRow[column] = DBNull.Value;
				continue;
			}
			PropertyDescriptor propertyDescriptor = a(itemProperties, parentColumnNames[i]);
			if (propertyDescriptor == null)
			{
				throw new Exception("Can not establish master/detail relation");
			}
			object value = propertyDescriptor.GetValue(component);
			if (value == null)
			{
				value = DBNull.Value;
			}
			newRow[column] = value;
		}
	}

	internal static ITypedList a(object A_0, string A_1, out object A_2)
	{
		ITypedList typedList = null;
		typedList = ((!(A_0 is IListSource)) ? ((ITypedList)A_0) : ((ITypedList)(A_0 as IListSource).GetList()));
		A_2 = null;
		if (A_0 is DbDataSet dbDataSet)
		{
			A_2 = dbDataSet.Owner;
		}
		if (A_0 is DbDataTable dbDataTable)
		{
			A_2 = dbDataTable.Owner;
		}
		if (A_0 is DataLink dataLink)
		{
			A_2 = dataLink.Owner;
		}
		ITypedList typedList2 = a(typedList, A_1, "", ref A_2, A_0);
		if (typedList2 is DbDataTableView dbDataTableView && A_2 == null && dbDataTableView.f != null)
		{
			A_2 = dbDataTableView.f.Owner;
		}
		return typedList2;
	}

	private static ITypedList a(ITypedList A_0, string A_1, string A_2, ref object A_3, object A_4)
	{
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		if (A_1 == null || A_1 == "" || A_0 == null)
		{
			return A_0;
		}
		string a_ = "";
		int num = A_1.IndexOf('.');
		if (num >= 0)
		{
			a_ = A_1.Substring(num + 1);
			A_1 = A_1.Substring(0, num);
			if (A_1 == "")
			{
				return null;
			}
		}
		PropertyDescriptorCollection itemProperties = A_0.GetItemProperties(null);
		for (int i = 0; i < itemProperties.Count; i++)
		{
			PropertyDescriptor propertyDescriptor = itemProperties[i];
			if (!(propertyDescriptor.Name == A_1))
			{
				continue;
			}
			if (propertyDescriptor is DataViewManagerPropertyDescriptor dataViewManagerPropertyDescriptor)
			{
				DbDataTableView dbDataTableView = (DbDataTableView)dataViewManagerPropertyDescriptor.GetValue(((IList)A_0)[0]);
				if (A_3 == null && dbDataTableView.f != null)
				{
					A_3 = dbDataTableView.f.Owner;
				}
				return a(dbDataTableView, a_, (A_2 == "") ? A_1 : (A_2 + "." + A_1), ref A_3, A_4);
			}
			if (propertyDescriptor is g g2 && g2.c() != null)
			{
				DbDataTable dbDataTable = g2.c().ParentTable as DbDataTable;
				if (A_3 == null && dbDataTable != null)
				{
					A_3 = dbDataTable.Owner;
				}
				int index = 0;
				if (A_3 != null)
				{
					CurrencyManager val = (CurrencyManager)((Control)A_3).BindingContext[A_4, A_2];
					if (val != null)
					{
						index = ((BindingManagerBase)val).Position;
					}
				}
				return a((ITypedList)g2.GetValue(((IList)A_0)[index]), a_, (A_2 == "") ? A_1 : (A_2 + "." + A_1), ref A_3, A_4);
			}
			return null;
		}
		return null;
	}

	protected override void OnRowChanged(DataRowChangeEventArgs e)
	{
		if (disableEvents <= 0)
		{
			base.OnRowChanged(e);
		}
	}

	protected override void OnRowChanging(DataRowChangeEventArgs e)
	{
		if (disableEvents <= 0)
		{
			base.OnRowChanging(e);
		}
	}

	protected override void OnRowDeleted(DataRowChangeEventArgs e)
	{
		if (disableUpdateEvents <= 0)
		{
			base.OnRowDeleted(e);
		}
	}

	public void ReadComplete(DataRow row)
	{
		if (row.RowState != DataRowState.Unchanged)
		{
			throw new InvalidOperationException(Devart.Common.n.a("ErrorReadModifiedOrDetachedRow"));
		}
		IDbCommand dbCommand = CommandBuilderInternal.a(TableMapping, row, A_2: true);
		if (dbCommand == null)
		{
			throw new InvalidOperationException("Can not create command for loading fields data.");
		}
		IDataReader dataReader = dbCommand.ExecuteReader(ExecuteCommBehavior);
		if (!dataReader.Read())
		{
			throw new InvalidOperationException(Devart.Common.n.a("RowNotExist"));
		}
		disableEvents++;
		disableUpdateEvents++;
		try
		{
			for (int i = 0; i < dataReader.FieldCount; i++)
			{
				string text = dataReader.GetName(i);
				if (u != null)
				{
					int num = u.ColumnMappings.IndexOf(text);
					if (num >= 0)
					{
						text = u.ColumnMappings[num].DataSetColumn;
					}
				}
				int num2 = Columns.IndexOf(text);
				if (num2 != -1)
				{
					GetField(row, num2, dataReader, i);
				}
			}
		}
		finally
		{
			disableEvents--;
			disableUpdateEvents--;
		}
		int num3 = dataTable.Rows.IndexOf(row);
		if (num3 >= 0 && this.m_r != null)
		{
			this.m_r.a(this, new ListChangedEventArgs(ListChangedType.ItemChanged, num3));
		}
	}

	protected void SetPrimaryKey(DataColumn[] value)
	{
		base.PrimaryKey = value;
	}

	private void e()
	{
		if (Site == null)
		{
			return;
		}
		if (Active)
		{
			for (int i = 0; i < Columns.Count; i++)
			{
				DataColumn dataColumn = Columns[i];
				if (dataColumn != null)
				{
					dataColumn.Site = null;
				}
			}
		}
		else
		{
			Columns.Clear();
		}
	}

	private bool d()
	{
		if (Columns.Count > 0)
		{
			return UserDefinedColumns;
		}
		return false;
	}

	private bool c()
	{
		if (Constraints.Count > 0)
		{
			return UserDefinedColumns;
		}
		return false;
	}

	private bool b()
	{
		if (PrimaryKey.Length > 0)
		{
			return UserDefinedColumns;
		}
		return false;
	}

	private bool a()
	{
		if (ae != null)
		{
			if (ae.ParentTable == null && ae.ParentColumnNames == null)
			{
				return ae.ChildColumnNames != null;
			}
			return true;
		}
		return false;
	}

	protected virtual void SetOwner(object value)
	{
		throw new Exception();
	}

	protected int FillPage(IDataReader reader, int startRecord, int maxRecords)
	{
		bool flag = base.DataSet != null && base.DataSet.EnforceConstraints;
		if (flag)
		{
			base.DataSet.EnforceConstraints = false;
		}
		try
		{
			this.reader = reader;
			this.m_l = false;
			int result = 0;
			try
			{
				CheckColumnsCreated(throwOnEmptySchemaTable: true);
				int num = this.m_p;
				a(int.MaxValue, A_1: true, A_2: true);
				result = this.m_p - num;
			}
			finally
			{
				CloseReader();
			}
			return result;
		}
		finally
		{
			if (flag)
			{
				base.DataSet.EnforceConstraints = true;
			}
		}
	}

	protected virtual int FillPage(DbCommand command, int startRecord, int maxRecords)
	{
		throw new Exception();
	}

	protected virtual string AddWhere(string commandText, string whereText)
	{
		throw new Exception();
	}

	protected object MakeModifiedObjectTree(DbDataRowView dataRowView, object oldValue, PropertyDescriptor propertyDescriptor)
	{
		dataRowView.InEditMode = true;
		if (dataRowView.Row != null)
		{
			dataRowView.Row.BeginEdit();
			string text = propertyDescriptor.Name;
			int num = text.IndexOf('.');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			DataColumn dataColumn = dataRowView.Row.Table.Columns[text];
			if (dataColumn != null)
			{
				if ((dataRowView.Row.HasVersion(DataRowVersion.Original) && dataRowView.Row[dataColumn] == dataRowView.Row[dataColumn, DataRowVersion.Original]) || NeedCloneColumnValue(oldValue))
				{
					object obj = CloneValue(oldValue, dataColumn);
					dataRowView.Row[dataColumn] = obj;
					return obj;
				}
				return dataRowView.Row[dataColumn];
			}
		}
		return oldValue;
	}

	protected virtual bool NeedCloneColumnValue(object oldValue)
	{
		return false;
	}

	protected virtual object CloneValue(object oldValue, DataColumn column)
	{
		if (!(oldValue is ICloneable cloneable))
		{
			throw new InvalidOperationException();
		}
		return cloneable.Clone();
	}

	internal void a(DbDataTableView A_0)
	{
		if (az == null)
		{
			az = new ArrayList();
		}
		lock (az)
		{
			for (int num = az.Count - 1; num >= 0; num--)
			{
				WeakReference weakReference = (WeakReference)az[num];
				if (!Utils.GetWeakIsAlive(weakReference))
				{
					az.RemoveAt(num);
				}
				else if (Utils.GetWeakTarget(weakReference) == A_0)
				{
					return;
				}
			}
			az.Add(new WeakReference(A_0));
		}
	}

	internal void a(ListChangedEventArgs A_0)
	{
		if (az == null)
		{
			return;
		}
		for (int num = az.Count - 1; num >= 0; num--)
		{
			WeakReference weakReference = (WeakReference)az[num];
			if (!Utils.GetWeakIsAlive(weakReference))
			{
				az.RemoveAt(num);
			}
			else
			{
				DbDataTableView dbDataTableView = (DbDataTableView)Utils.GetWeakTarget(weakReference);
				dbDataTableView.a(dbDataTableView, A_0);
			}
		}
	}

	internal void aa()
	{
		for (int num = az.Count - 1; num >= 0; num--)
		{
			WeakReference weakReference = (WeakReference)az[num];
			if (!Utils.GetWeakIsAlive(weakReference))
			{
				az.RemoveAt(num);
			}
			else
			{
				DbDataTableView dbDataTableView = (DbDataTableView)Utils.GetWeakTarget(weakReference);
				dbDataTableView.l++;
			}
		}
		try
		{
			if (az == null)
			{
				return;
			}
			bool flag = false;
			while (!flag)
			{
				flag = true;
				for (int num2 = az.Count - 1; num2 >= 0; num2--)
				{
					WeakReference weakReference2 = (WeakReference)az[num2];
					if (!Utils.GetWeakIsAlive(weakReference2))
					{
						az.RemoveAt(num2);
					}
					else
					{
						DbDataTableView dbDataTableView2 = (DbDataTableView)Utils.GetWeakTarget(weakReference2);
						if (dbDataTableView2.i())
						{
							flag = false;
						}
					}
				}
			}
		}
		finally
		{
			for (int num3 = az.Count - 1; num3 >= 0; num3--)
			{
				WeakReference weakReference3 = (WeakReference)az[num3];
				if (!Utils.GetWeakIsAlive(weakReference3))
				{
					az.RemoveAt(num3);
				}
				else
				{
					DbDataTableView dbDataTableView3 = (DbDataTableView)Utils.GetWeakTarget(weakReference3);
					dbDataTableView3.l--;
					if (dbDataTableView3.l < 0)
					{
						dbDataTableView3.l = 0;
					}
				}
			}
		}
	}

	private void a(DataRow A_0, DataRow A_1)
	{
		if (az == null)
		{
			return;
		}
		for (int num = az.Count - 1; num >= 0; num--)
		{
			WeakReference weakReference = (WeakReference)az[num];
			if (!Utils.GetWeakIsAlive(weakReference))
			{
				az.RemoveAt(num);
			}
			else
			{
				DbDataTableView dbDataTableView = (DbDataTableView)Utils.GetWeakTarget(weakReference);
				int num2 = -1;
				int num3 = -1;
				int num4 = ((dbDataTableView.g != null) ? dbDataTableView.g.Count : 0);
				for (int num5 = num4 - 1; num5 >= 0; num5--)
				{
					DataRowView dataRowView = dbDataTableView.g[num5];
					if (dataRowView.Row == A_1)
					{
						num2 = num5;
					}
					if (dataRowView.Row == A_0)
					{
						num3 = num5;
					}
					if (num2 != -1 && num3 != -1)
					{
						break;
					}
				}
				if (num2 != -1 && num3 != -1)
				{
					dbDataTableView.a(num3, num2);
				}
				else if (num2 != -1 && num3 == -1)
				{
					dbDataTableView.c(num2);
				}
			}
		}
	}

	protected virtual void Open(IDataReader reader)
	{
		OpenInternal(reader);
	}

	protected virtual void OpenInternal(IDataReader reader)
	{
		if (m)
		{
			return;
		}
		this.m_n = true;
		m = true;
		try
		{
			ac = true;
			this.m_k = true;
			this.m_l = false;
			this.reader = reader;
			if (this.m_aa != 0)
			{
				throw new ArgumentException(Devart.Common.n.a("MaxRecordsMustBeZero"));
			}
			if (!ak)
			{
				Prepare();
			}
			ak = false;
			CheckColumnsCreated(throwOnEmptySchemaTable: true);
			if (this.m_r != null)
			{
				this.m_r.j();
			}
			a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
			h();
		}
		catch
		{
			m = false;
			this.reader = null;
			n();
			throw;
		}
		finally
		{
			this.m_n = false;
		}
	}

	protected virtual void DecrementAutoIncrementCurrent(DataRow row)
	{
	}

	internal void e(DataRow A_0)
	{
		DecrementAutoIncrementCurrent(A_0);
	}

	internal void y()
	{
		a1++;
	}

	internal void t()
	{
		if (a1 > 0)
		{
			a1--;
		}
	}
}
