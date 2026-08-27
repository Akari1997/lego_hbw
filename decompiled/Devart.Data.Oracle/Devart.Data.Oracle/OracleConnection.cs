using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

[Devart.Common.i("OracleConnection_Description")]
[ToolboxItem(true)]
[DesignTimeVisible(true)]
public class OracleConnection : DbConnectionBase, IDbConnection, ICloneable
{
	private new bool m_a = true;

	internal new OracleTransaction b;

	private OraclePermission m_c;

	private static readonly object m_d;

	private static readonly object m_e;

	private static readonly object f;

	private bool g = true;

	private bool h;

	private bool m_i;

	private bool j;

	private OracleNumberMappingCollection k;

	internal OracleConnection l;

	[DefaultValue(true)]
	[Devart.Common.i("OracleConnection_AutoCommit")]
	public bool AutoCommit
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

	protected override string DataSourceInternal
	{
		get
		{
			return Server;
		}
		set
		{
		}
	}

	protected override DbProviderFactory DbProviderFactory => OracleProviderFactory.Instance;

	[Devart.Common.i("DbConnection_User")]
	[Category("Login")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue("")]
	public string UserId
	{
		get
		{
			ay ay2 = ConnectionOptions;
			if (ay2 == null)
			{
				return "";
			}
			return ay2.af();
		}
		set
		{
			if (UserId != value)
			{
				Close();
				a("User Id", value);
			}
		}
	}

	[DefaultValue("")]
	[Category("Login")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[PasswordPropertyText(true)]
	[Devart.Common.i("DbConnection_Password")]
	[RefreshProperties(RefreshProperties.Repaint)]
	public string Password
	{
		get
		{
			ay ay2 = ConnectionOptions;
			if (ay2 == null)
			{
				return "";
			}
			return ay2.n();
		}
		set
		{
			if (Password != value)
			{
				Close();
				a("Password", value);
			}
		}
	}

	[RefreshProperties(RefreshProperties.Repaint)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Devart.Common.i("OracleConnection_Server")]
	[Category("Login")]
	[DefaultValue("")]
	public string Server
	{
		get
		{
			ay ay2 = ConnectionOptions;
			if (ay2 == null)
			{
				return "";
			}
			return ay2.ag();
		}
		set
		{
			if (Server != value)
			{
				Close();
				a("Server", value);
			}
		}
	}

	[RefreshProperties(RefreshProperties.Repaint)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue("")]
	[Devart.Common.i("OracleConnection_Home")]
	[Category("Misc")]
	public string Home
	{
		get
		{
			ay ay2 = ConnectionOptions;
			if (ay2 == null)
			{
				return "";
			}
			return ay2.f();
		}
		set
		{
			if (Home != value)
			{
				Close();
				a("Home", value);
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Category("Misc")]
	[DefaultValue("")]
	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("OracleConnection_ClientId")]
	public string ClientId
	{
		get
		{
			ay ay2 = ConnectionOptions;
			if (ay2 == null)
			{
				return "";
			}
			return ay2.t();
		}
		set
		{
			if (ClientId != value)
			{
				Close();
				a("ClientId", value);
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Devart.Common.i("OracleConnection_ConnectMode")]
	[Category("Login")]
	[DefaultValue(OracleConnectMode.Default)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public OracleConnectMode ConnectMode
	{
		get
		{
			return ConnectionOptions?.k() ?? OracleConnectMode.Default;
		}
		set
		{
			if (ConnectMode != value)
			{
				if (!Enum.IsDefined(typeof(OracleConnectMode), value))
				{
					throw new ArgumentException("Invalid connection mode value.");
				}
				Close();
				a("Connect Mode", value);
			}
		}
	}

	[Category("Misc")]
	[Devart.Common.i("OracleConnectionString_Unicode")]
	[DefaultValue(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public bool Unicode
	{
		get
		{
			return ConnectionOptions?.u() ?? false;
		}
		set
		{
			if (Unicode != value)
			{
				Close();
				a("Unicode", value);
			}
		}
	}

	[Category("Data")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Devart.Common.i("OracleConnection_ConnectionString")]
	[SettingsBindable(true)]
	[DefaultValue("")]
	public new string ConnectionString
	{
		get
		{
			return base.ConnectionString;
		}
		set
		{
			if (ConnectionString != value)
			{
				Close();
				b();
				base.ConnectionString = value;
			}
		}
	}

	[DefaultValue(ConnectionState.Closed)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Browsable(true)]
	[Devart.Common.i("DbConnection_State")]
	public new ConnectionState State
	{
		get
		{
			return base.State;
		}
		set
		{
			base.ConnectionStateInternal = value;
		}
	}

	[Browsable(false)]
	public string ClientVersion
	{
		get
		{
			OracleHomeCollection oracleHomeCollection = OracleHomeCollection.SingletonInstance;
			OracleHome oracleHome = ((Home == string.Empty) ? oracleHomeCollection.DefaultHome : oracleHomeCollection[Home]);
			if (oracleHome == null)
			{
				throw new OracleException(-1, string.Format(Devart.Common.al.a("CanNotFoundOracleHome"), this));
			}
			return oracleHome.ClientVersion;
		}
	}

	[Category("Source")]
	[Devart.Common.i("OracleConnection_ServerVersion")]
	[Browsable(true)]
	public new string ServerVersion
	{
		get
		{
			if (State == ConnectionState.Open)
			{
				return base.ServerVersion;
			}
			return "";
		}
	}

	[Devart.Common.i("DbConnection_ConnectionTimeout")]
	[DefaultValue(15)]
	[Category("Initialization")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Browsable(false)]
	public new int ConnectionTimeout => ConnectionOptions?.v() ?? 0;

	int IDbConnection.ConnectionTimeout => ConnectionTimeout;

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string Database => string.Empty;

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override string DatabaseInternal
	{
		get
		{
			return string.Empty;
		}
		set
		{
		}
	}

	internal HandleRef EnvHandle => d().h();

	internal HandleRef ErrHandle => d().n();

	internal HandleRef SvcCtxHandle => GetNativeHandle();

	[Devart.Common.i("DbConnectionString_TrimFixedChar")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[DefaultValue(true)]
	public bool TrimFixedChar
	{
		get
		{
			if (h)
			{
				return g;
			}
			return ConnectionOptions?.aa() ?? g;
		}
		set
		{
			h = true;
			g = value;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[Devart.Common.i("DbConnectionString_PassParametersByName")]
	[DefaultValue(true)]
	public bool PassParametersByName
	{
		get
		{
			if (j)
			{
				return this.m_i;
			}
			return ConnectionOptions?.ac() ?? this.m_i;
		}
		set
		{
			j = true;
			this.m_i = value;
		}
	}

	public static OracleHomeCollection Homes => OracleHomeCollection.SingletonInstance;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[Devart.Common.i("DbConnectionString_NumberMappings")]
	[DefaultValue(null)]
	public OracleNumberMappingCollection NumberMappings
	{
		get
		{
			if (k == null)
			{
				ay ay2 = ConnectionOptions;
				if (ay2 != null)
				{
					k = ay2.c().Clone();
				}
			}
			if (k == null)
			{
				k = new OracleNumberMappingCollection();
			}
			return k;
		}
	}

	[Browsable(false)]
	[DefaultValue("")]
	public new string Name
	{
		get
		{
			return base.Name;
		}
		set
		{
			base.Name = value;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new object Owner
	{
		get
		{
			return base.Owner;
		}
		set
		{
			base.Owner = value;
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Devart.Common.i("OracleConnection_LogicalTransactionId")]
	public byte[] LogicalTransactionId
	{
		get
		{
			g g2 = ((ap)base.InnerConnection).l();
			return g2.n();
		}
	}

	internal new ay ConnectionOptions
	{
		get
		{
			return base.ConnectionOptions as ay;
		}
		set
		{
			base.ConnectionOptions = value;
		}
	}

	[Devart.Common.i("OracleConnection_InfoMessage")]
	public event OracleInfoMessageEventHandler InfoMessage
	{
		add
		{
			base.Events.AddHandler(OracleConnection.m_d, value);
		}
		remove
		{
			base.Events.RemoveHandler(OracleConnection.m_d, value);
		}
	}

	[Devart.Common.i("OracleConnection_Error")]
	public event OracleConnectionErrorEventHandler Error
	{
		add
		{
			base.Events.AddHandler(OracleConnection.m_e, value);
		}
		remove
		{
			base.Events.RemoveHandler(OracleConnection.m_e, value);
		}
	}

	[Devart.Common.i("OracleConnection_Failover")]
	public event OracleFailoverEventHandler Failover
	{
		add
		{
			base.Events.AddHandler(f, value);
		}
		remove
		{
			base.Events.RemoveHandler(f, value);
		}
	}

	public OracleConnection()
		: base(u.b)
	{
	}

	public OracleConnection(string connectionString)
		: this()
	{
		ConnectionString = connectionString;
	}

	internal OracleConnection(OracleConnection A_0)
		: base(A_0)
	{
	}

	internal ap d()
	{
		if (!(base.InnerConnection is ap result))
		{
			throw new InvalidOperationException(Devart.Common.al.a("ConnMustOpen"));
		}
		return result;
	}

	internal void b(DbConnectionBase A_0)
	{
		AddWeakReference(A_0, 101);
	}

	internal void a(DbConnectionBase A_0)
	{
		RemoveWeakReference(A_0);
	}

	public void Open(OracleConnection proxy)
	{
		if (proxy == null)
		{
			throw new ArgumentNullException("proxy");
		}
		l = proxy;
		try
		{
			Open();
			b(proxy);
		}
		finally
		{
			l = null;
		}
	}

	public override void Open()
	{
		if (State == ConnectionState.Open)
		{
			return;
		}
		ay ay2 = ConnectionOptions;
		if (l != null && (ay2.ad() || ay2.g()))
		{
			throw new ArgumentException("Cannot use with pooling.");
		}
		ay2?.UsersConnectionString(hidePassword: true);
		try
		{
			base.Open();
			if (!(base.InnerConnection is DbConnectionClosed))
			{
				OracleInfoMessageEventArgs e2 = ((ap)base.InnerConnection).j();
				if (e2 != null)
				{
					i()?.Invoke(this, e2);
				}
				if (l == null)
				{
					g g2 = ((ap)base.InnerConnection).l();
					g2.a(this);
				}
			}
		}
		catch (OracleException a_)
		{
			a(this, a_);
			throw;
		}
	}

	public override void Close()
	{
		base.Close();
	}

	private void a(string A_0, object A_1)
	{
		string connectionString = ((base.UserConnectionOptions != null) ? base.UserConnectionOptions.ToString() : "");
		OracleConnectionStringBuilder oracleConnectionStringBuilder = new OracleConnectionStringBuilder(connectionString);
		oracleConnectionStringBuilder[A_0] = A_1;
		ConnectionString = oracleConnectionStringBuilder.ConnectionString;
	}

	public void ChangePassword(string newPassword)
	{
		ay ay2 = ConnectionOptions;
		if (ay2 == null || ay2.af() == null || ay2.n() == null || ay2.ag() == null)
		{
			throw new InvalidOperationException(Devart.Common.al.a("YouMustSetupNamePasswordServer"));
		}
		if (newPassword == null)
		{
			throw new ArgumentNullException("newPassword");
		}
		g g2;
		if (!(base.InnerConnection is ap ap2))
		{
			string text = ay2.f();
			OracleHomeCollection oracleHomeCollection = OracleHomeCollection.SingletonInstance;
			OracleHome oracleHome = ((text == string.Empty) ? oracleHomeCollection.DefaultHome : oracleHomeCollection[text]);
			if (oracleHome == null)
			{
				throw new OracleException(-1, string.Format(Devart.Common.al.a("CanNotFoundOracleHome"), text));
			}
			aq aq2 = aa.a(ay2.u(), A_1: true, oracleHome, !ay2.g());
			g2 = aq2.b();
			g2.c(ay2.v());
			g2.b(ay2.y());
		}
		else
		{
			g2 = ap2.l();
		}
		g2.a(ay2, newPassword);
		if (State == ConnectionState.Closed)
		{
			a("Password", newPassword);
		}
	}

	IDbTransaction IDbConnection.BeginTransaction()
	{
		return BeginTransaction();
	}

	public new OracleTransaction BeginTransaction()
	{
		return BeginTransaction(IsolationLevel.ReadCommitted);
	}

	IDbTransaction IDbConnection.BeginTransaction(IsolationLevel iso)
	{
		return BeginTransaction(iso);
	}

	public new OracleTransaction BeginTransaction(IsolationLevel il)
	{
		using (base.LocalFailoverManager.StartUse())
		{
			while (true)
			{
				if (State != ConnectionState.Open)
				{
					throw new InvalidOperationException(Devart.Common.al.a("ConnMustOpen"));
				}
				if (this.b != null)
				{
					break;
				}
				e();
				try
				{
					OnTransactionStateChanging(TransactionAction.BeginTransaction);
					this.b = new OracleTransaction(this, il);
					OnTransactionStateChanged(TransactionAction.BeginTransaction);
					this.b.StateChanging += base.Transaction_StateChanging;
					this.b.StateChanged += base.Transaction_StateChanged;
					return this.b;
				}
				catch (Exception ex)
				{
					if (base.LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.StartTransaction, RetryMode.Reexecute, ex) == RetryMode.Raise)
					{
						throw;
					}
				}
			}
			throw new InvalidOperationException("Transaction already exists.");
		}
	}

	protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
	{
		return BeginTransaction(isolationLevel);
	}

	public void Commit()
	{
		e();
		if (this.b != null)
		{
			this.b.Commit();
			return;
		}
		ap ap2 = d();
		try
		{
			ap2.Commit();
		}
		catch (Exception ex)
		{
			if (ex is OracleException a_)
			{
				a(this, a_);
			}
			ap2.a(ex);
			throw;
		}
	}

	public void Rollback()
	{
		e();
		if (this.b != null)
		{
			this.b.Rollback();
			return;
		}
		ap ap2 = d();
		try
		{
			ap2.Rollback();
		}
		catch (Exception ex)
		{
			if (ex is OracleException a_)
			{
				a(this, a_);
			}
			ap2.a(ex);
			throw;
		}
	}

	IDbCommand IDbConnection.CreateCommand()
	{
		return CreateDbCommand();
	}

	public new OracleCommand CreateCommand()
	{
		return (OracleCommand)CreateDbCommand();
	}

	protected override DbCommand CreateDbCommand()
	{
		OracleCommand oracleCommand = (OracleCommand)base.CreateDbCommand();
		ay ay2 = ConnectionOptions;
		if (ay2 != null)
		{
			oracleCommand.CommandTimeout = ay2.y();
		}
		return oracleCommand;
	}

	void IDbConnection.ChangeDatabase(string databaseName)
	{
	}

	public object Clone()
	{
		return new OracleConnection(this);
	}

	internal void e()
	{
		if (this.m_c == null)
		{
			this.m_c = new OraclePermission(PermissionState.Unrestricted);
		}
		((CodeAccessPermission)this.m_c).Assert();
	}

	internal bool a(OracleFailoverState A_0, OracleFailoverType A_1)
	{
		OracleFailoverEventHandler oracleFailoverEventHandler = (OracleFailoverEventHandler)base.Events[f];
		if (oracleFailoverEventHandler == null)
		{
			return false;
		}
		OracleFailoverEventsArgs oracleFailoverEventsArgs = new OracleFailoverEventsArgs(A_0, A_1);
		oracleFailoverEventHandler(this, oracleFailoverEventsArgs);
		if (oracleFailoverEventsArgs.Retry && A_0 == OracleFailoverState.Error)
		{
			return true;
		}
		return false;
	}

	public OracleGlobalization GetSessionInfo()
	{
		OracleGlobalization oracleGlobalization = new OracleGlobalization();
		GetSessionInfo(oracleGlobalization);
		return oracleGlobalization;
	}

	public OracleGlobalization GetDatabaseInfo()
	{
		OracleGlobalization oracleGlobalization = new OracleGlobalization();
		GetDatabaseInfo(oracleGlobalization);
		return oracleGlobalization;
	}

	public void GetSessionInfo(OracleGlobalization oraGlob)
	{
		string a_ = "SELECT * FROM SYS.NLS_SESSION_PARAMETERS UNION ALL SELECT 'TIME_ZONE', SESSIONTIMEZONE FROM DUAL";
		a(oraGlob, a_);
	}

	public void GetDatabaseInfo(OracleGlobalization oraGlob)
	{
		string a_ = "SELECT * FROM SYS.NLS_DATABASE_PARAMETERS UNION ALL SELECT 'TIME_ZONE', DBTIMEZONE FROM DUAL";
		a(oraGlob, a_);
	}

	private void a(OracleGlobalization A_0, string A_1)
	{
		if (State != ConnectionState.Open)
		{
			throw new InvalidOperationException(Devart.Common.al.a("ConnMustOpen"));
		}
		OracleCommand oracleCommand = CreateCommand();
		oracleCommand.CommandText = A_1;
		using OracleDataReader oracleDataReader = oracleCommand.ExecuteReader();
		while (oracleDataReader.Read())
		{
			switch (oracleDataReader.GetString(0))
			{
			case "NLS_CURRENCY":
				A_0.Currency = oracleDataReader.GetString(1);
				break;
			case "NLS_DATE_FORMAT":
				A_0.DateFormat = oracleDataReader.GetString(1);
				break;
			case "NLS_DATE_LANGUAGE":
				A_0.DateLanguage = oracleDataReader.GetString(1);
				break;
			case "NLS_DUAL_CURRENCY":
				A_0.DualCurrency = oracleDataReader.GetString(1);
				break;
			case "NLS_ISO_CURRENCY":
				A_0.ISOCurrency = oracleDataReader.GetString(1);
				break;
			case "NLS_LANGUAGE":
				A_0.Language = oracleDataReader.GetString(1);
				break;
			case "NLS_NUMERIC_CHARACTERS":
				A_0.NumericCharacters = oracleDataReader.GetString(1);
				break;
			case "NLS_TERRITORY":
				A_0.Territory = oracleDataReader.GetString(1);
				break;
			case "NLS_TIMESTAMP_FORMAT":
				A_0.TimeStampFormat = oracleDataReader.GetString(1);
				break;
			case "NLS_TIMESTAMP_TZ_FORMAT":
				A_0.TimeStampTZFormat = oracleDataReader.GetString(1);
				break;
			case "TIME_ZONE":
				A_0.TimeZone = oracleDataReader.GetString(1);
				break;
			}
		}
	}

	public void SetSessionInfo(OracleGlobalization oraGlob)
	{
		if (State != ConnectionState.Open)
		{
			throw new InvalidOperationException(Devart.Common.al.a("ConnMustOpen"));
		}
		StringBuilder stringBuilder = new StringBuilder("ALTER SESSION SET", 512);
		string strA = OracleUtils.c(ServerVersion);
		if (string.Compare(strA, "08") > 0)
		{
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_TERRITORY=\"{0}\"", oraGlob.Territory);
			}
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_LANGUAGE=\"{0}\"", oraGlob.Language);
			}
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_DATE_LANGUAGE=\"{0}\"", oraGlob.DateLanguage);
			}
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_CURRENCY=\"{0}\"", oraGlob.Currency);
			}
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_DATE_FORMAT='{0}'", oraGlob.DateFormat);
			}
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_ISO_CURRENCY=\"{0}\"", oraGlob.ISOCurrency);
			}
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_NUMERIC_CHARACTERS=\"{0}\"", oraGlob.NumericCharacters);
			}
			if (string.Compare(strA, "08.01") > 0 && !Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_DUAL_CURRENCY=\"{0}\"", oraGlob.DualCurrency);
			}
		}
		if (string.Compare(strA, "09") > 0)
		{
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_NCHAR_CONV_EXCP=\"{0}\"", oraGlob.NCharConversionException);
			}
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_TIMESTAMP_FORMAT='{0}'", oraGlob.TimeStampFormat);
			}
			if (!Utils.IsEmpty(oraGlob.Territory))
			{
				stringBuilder.AppendFormat(" NLS_TIMESTAMP_TZ_FORMAT='{0}'", oraGlob.TimeStampTZFormat);
			}
		}
		if (oraGlob.TimeZone != null && oraGlob.TimeZone.Length != 0)
		{
			if (oraGlob.TimeZone.ToLower() == "local")
			{
				stringBuilder.AppendFormat(" TIME_ZONE=local", new object[0]);
			}
			else if (oraGlob.TimeZone.ToLower() == "dbtimezone")
			{
				stringBuilder.AppendFormat(" TIME_ZONE=DBTIMEZONE", new object[0]);
			}
			else if (oraGlob.TimeZone.Length > 0)
			{
				stringBuilder.AppendFormat(" TIME_ZONE='{0}'", oraGlob.TimeZone);
			}
		}
		OracleCommand oracleCommand = CreateCommand();
		oracleCommand.CommandText = stringBuilder.ToString();
		oracleCommand.ExecuteNonQuery();
	}

	private void c()
	{
		ConnectMode = OracleConnectMode.Default;
	}

	public static string[] GetServerList()
	{
		return GetServerList("");
	}

	public static string[] GetServerList(string homeName)
	{
		OracleHome oracleHome = ((!(homeName == "")) ? Homes[homeName] : Homes.DefaultHome);
		if (oracleHome != null)
		{
			return oracleHome.GetServerList();
		}
		return new string[0];
	}

	public static void ClearAllPools()
	{
		u.b.ClearAllPools();
	}

	public static void ClearPool(OracleConnection connection)
	{
		ay ay2 = connection.ConnectionOptions;
		string connectionString = ((ay2 != null) ? ay2.UsersConnectionString(hidePassword: false) : "");
		u.b.ClearPool(connectionString);
	}

	public HandleRef GetNativeHandle()
	{
		return d().o();
	}

	internal OracleInfoMessageEventHandler i()
	{
		return (OracleInfoMessageEventHandler)base.Events[OracleConnection.m_d];
	}

	internal void a(object A_0, OracleException A_1)
	{
		OracleConnectionErrorEventArgs e2 = new OracleConnectionErrorEventArgs(A_1.Message, A_1.Code);
		((OracleConnectionErrorEventHandler)base.Events[OracleConnection.m_e])?.Invoke(A_0, e2);
	}

	protected override void Reconnect()
	{
		if (base.InnerConnection is ap ap2)
		{
			ap2.ad();
		}
		Close();
		Open();
	}

	protected internal override bool InTransaction()
	{
		return this.b != null;
	}

	protected override bool IsConnectionLostError(Exception e)
	{
		if (!(e is OracleException ex))
		{
			return false;
		}
		if (ex.Code == 28 || ex.Code == 1012 || ex.Code == 3113 || ex.Code == 3114 || ex.Code == 3135 || ex.Code == 12571 || ex.Code == 12545)
		{
			return true;
		}
		return false;
	}

	private void b()
	{
		g = true;
		h = false;
		this.m_i = false;
		j = false;
		k = null;
	}

	public OracleLogicalTransactionStatus GetLogicalTransactionStatus(byte[] ltxid)
	{
		Utils.CheckConnectionOpen(this);
		if (ltxid == null || string.Compare(base.InnerConnection.ServerVersionNormalized, "12") < 0)
		{
			return null;
		}
		OracleCommand oracleCommand = new OracleCommand();
		oracleCommand.Connection = this;
		oracleCommand.CommandText = "SYS.DBMS_APP_CONT.GET_LTXID_OUTCOME";
		oracleCommand.CommandType = CommandType.StoredProcedure;
		oracleCommand.Parameters.Add("client_ltxid", OracleDbType.Raw, ltxid, ParameterDirection.Input);
		OracleParameter oracleParameter = oracleCommand.Parameters.Add("committed", OracleDbType.Boolean, ParameterDirection.Output);
		OracleParameter oracleParameter2 = oracleCommand.Parameters.Add("userCallCompleted", OracleDbType.Boolean, ParameterDirection.Output);
		oracleCommand.ExecuteNonQuery();
		bool a_ = (bool)oracleParameter.Value;
		bool a_2 = (bool)oracleParameter2.Value;
		return new OracleLogicalTransactionStatus(a_, a_2);
	}

	public bool Ping()
	{
		Utils.CheckConnectionOpen(this);
		return d()?.s() ?? false;
	}

	static OracleConnection()
	{
		OracleConnection.m_d = new object();
		OracleConnection.m_e = new object();
		f = new object();
	}
}
