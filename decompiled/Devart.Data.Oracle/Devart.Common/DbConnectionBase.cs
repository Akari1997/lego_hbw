using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Transactions;

namespace Devart.Common;

public abstract class DbConnectionBase : DbConnection
{
	private delegate void a();

	private const int m_a = int.MaxValue;

	private readonly DbConnectionFactory m_b;

	private DbConnectionInternal c;

	private DbConnectionPool d;

	private StateChangeEventHandler e;

	private DbConnectionOptions f;

	private int g;

	private bool h = true;

	private bool i;

	private bool j;

	private ILocalFailoverManager k;

	private static readonly object l = new object();

	private a m;

	private string n = string.Empty;

	private object o;

	private TransactionStateChangingEventHandler p;

	private TransactionStateChangedEventHandler q;

	protected internal int CloseCount => g;

	internal DbConnectionFactory ConnectionFactory => this.m_b;

	internal DbConnectionOptions ConnectionOptions
	{
		get
		{
			DbConnectionPool dbConnectionPool = Pool;
			if (dbConnectionPool == null)
			{
				return f;
			}
			return dbConnectionPool.ConnectionOptions;
		}
		set
		{
			DbConnectionPool dbConnectionPool = ConnectionFactory.a(null, ref value);
			DbConnectionInternal dbConnectionInternal = InnerConnection;
			bool flag = dbConnectionInternal.AllowSetConnectionString;
			if (flag && (flag = a(DbConnectionClosed.b, dbConnectionInternal, A_2: false)))
			{
				f = value;
				d = dbConnectionPool;
				c = DbConnectionClosed.c;
			}
			if (!flag)
			{
				throw new InvalidOperationException(al.a("OpenConnectionStringSet"));
			}
		}
	}

	[MergableProperty(false)]
	public override string ConnectionString
	{
		get
		{
			bool hidePassword = InnerConnection.ShouldHidePassword;
			DbConnectionOptions dbConnectionOptions = UserConnectionOptions;
			if (dbConnectionOptions == null)
			{
				return string.Empty;
			}
			if (base.DesignMode)
			{
				hidePassword = false;
			}
			string text = dbConnectionOptions.UsersConnectionString(hidePassword).TrimEnd(new char[0]);
			if (text.Length > 0 && text[text.Length - 1] != ';')
			{
				return text + ";";
			}
			return text;
		}
		set
		{
			if (ConnectionString != value)
			{
				Close();
				DbConnectionOptions A_ = null;
				DbConnectionPool dbConnectionPool = ConnectionFactory.a(value, ref A_);
				DbConnectionInternal dbConnectionInternal = InnerConnection;
				bool flag = dbConnectionInternal.AllowSetConnectionString;
				if (flag && (flag = a(DbConnectionClosed.b, dbConnectionInternal, A_2: false)))
				{
					f = A_;
					d = dbConnectionPool;
					c = DbConnectionClosed.c;
				}
				if (!flag)
				{
					throw new InvalidOperationException(al.a("OpenConnectionStringSet"));
				}
			}
		}
	}

	public override int ConnectionTimeout => ConnectionTimeoutInternal;

	protected virtual int ConnectionTimeoutInternal => 0;

	internal DbConnectionInternal InnerConnection => c;

	internal DbConnectionPool Pool
	{
		get
		{
			return d;
		}
		set
		{
			d = value;
		}
	}

	internal DbConnectionOptions UserConnectionOptions => f;

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[i("DbConnection_State")]
	[TypeConverter(typeof(ad))]
	public override ConnectionState State => ConnectionStateInternal;

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected internal virtual ConnectionState ConnectionStateInternal
	{
		get
		{
			return InnerConnection.State;
		}
		set
		{
			if (value == State)
			{
				return;
			}
			switch (value)
			{
			case ConnectionState.Closed:
				Close();
				return;
			case ConnectionState.Open:
				Open();
				return;
			}
			if (base.DesignMode)
			{
				if (State == ConnectionState.Open)
				{
					Close();
				}
				else
				{
					Open();
				}
				return;
			}
			throw new NotSupportedException(string.Format(al.a("ConnectionStateNotSupported"), value));
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string ServerVersion
	{
		get
		{
			if (State == ConnectionState.Open)
			{
				return InnerConnection.ServerVersion;
			}
			if (base.DesignMode)
			{
				return string.Empty;
			}
			throw new InvalidOperationException("Invalid operation. The connection is closed.");
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[i("DbConnection_Database")]
	[Browsable(false)]
	public override string Database => DatabaseInternal;

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	protected virtual string DatabaseInternal
	{
		get
		{
			return string.Empty;
		}
		set
		{
		}
	}

	[i("DbConnection_DataSource")]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string DataSource => DataSourceInternal;

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected virtual string DataSourceInternal
	{
		get
		{
			return string.Empty;
		}
		set
		{
		}
	}

	[Browsable(false)]
	[DefaultValue(true)]
	[DesignOnly(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool DesignTimeVisible
	{
		get
		{
			return h;
		}
		set
		{
			h = value;
			TypeDescriptor.Refresh(this);
		}
	}

	internal ILocalFailoverManager LocalFailoverManager
	{
		get
		{
			if (k == null)
			{
				k = new x(this);
			}
			return k;
		}
	}

	protected bool LocalFailover
	{
		get
		{
			return j;
		}
		set
		{
			j = value;
		}
	}

	[DefaultValue("")]
	[Browsable(false)]
	public string Name
	{
		get
		{
			if (Site == null)
			{
				if (n != null)
				{
					return n;
				}
				return string.Empty;
			}
			return Site.Name;
		}
		set
		{
			if (Site == null)
			{
				n = ((value == null) ? string.Empty : value);
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public object Owner
	{
		get
		{
			return o;
		}
		set
		{
		}
	}

	internal bool IsNHibernate
	{
		get
		{
			return i;
		}
		set
		{
			i = value;
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool InDistributedTransaction
	{
		get
		{
			if (InnerConnection != null)
			{
				return InnerConnection.Transaction != null;
			}
			return false;
		}
	}

	[i("DbConnection_StateChange")]
	[Category("StateChange")]
	public override event StateChangeEventHandler StateChange
	{
		add
		{
			e = (StateChangeEventHandler)Delegate.Combine(e, value);
		}
		remove
		{
			e = (StateChangeEventHandler)Delegate.Remove(e, value);
		}
	}

	protected event ConnectionLostEventHandler ConnectionLost
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

	public event TransactionStateChangingEventHandler TransactionStateChanging
	{
		add
		{
			TransactionStateChangingEventHandler transactionStateChangingEventHandler = p;
			TransactionStateChangingEventHandler transactionStateChangingEventHandler2;
			do
			{
				transactionStateChangingEventHandler2 = transactionStateChangingEventHandler;
				TransactionStateChangingEventHandler value2 = (TransactionStateChangingEventHandler)Delegate.Combine(transactionStateChangingEventHandler2, value);
				transactionStateChangingEventHandler = Interlocked.CompareExchange(ref p, value2, transactionStateChangingEventHandler2);
			}
			while ((object)transactionStateChangingEventHandler != transactionStateChangingEventHandler2);
		}
		remove
		{
			TransactionStateChangingEventHandler transactionStateChangingEventHandler = p;
			TransactionStateChangingEventHandler transactionStateChangingEventHandler2;
			do
			{
				transactionStateChangingEventHandler2 = transactionStateChangingEventHandler;
				TransactionStateChangingEventHandler value2 = (TransactionStateChangingEventHandler)Delegate.Remove(transactionStateChangingEventHandler2, value);
				transactionStateChangingEventHandler = Interlocked.CompareExchange(ref p, value2, transactionStateChangingEventHandler2);
			}
			while ((object)transactionStateChangingEventHandler != transactionStateChangingEventHandler2);
		}
	}

	public event TransactionStateChangedEventHandler TransactionStateChanged
	{
		add
		{
			TransactionStateChangedEventHandler transactionStateChangedEventHandler = q;
			TransactionStateChangedEventHandler transactionStateChangedEventHandler2;
			do
			{
				transactionStateChangedEventHandler2 = transactionStateChangedEventHandler;
				TransactionStateChangedEventHandler value2 = (TransactionStateChangedEventHandler)Delegate.Combine(transactionStateChangedEventHandler2, value);
				transactionStateChangedEventHandler = Interlocked.CompareExchange(ref q, value2, transactionStateChangedEventHandler2);
			}
			while ((object)transactionStateChangedEventHandler != transactionStateChangedEventHandler2);
		}
		remove
		{
			TransactionStateChangedEventHandler transactionStateChangedEventHandler = q;
			TransactionStateChangedEventHandler transactionStateChangedEventHandler2;
			do
			{
				transactionStateChangedEventHandler2 = transactionStateChangedEventHandler;
				TransactionStateChangedEventHandler value2 = (TransactionStateChangedEventHandler)Delegate.Remove(transactionStateChangedEventHandler2, value);
				transactionStateChangedEventHandler = Interlocked.CompareExchange(ref q, value2, transactionStateChangedEventHandler2);
			}
			while ((object)transactionStateChangedEventHandler != transactionStateChangedEventHandler2);
		}
	}

	protected DbConnectionBase(DbConnectionBase connection)
	{
		Utils.CheckArgumentNull(connection, "connection");
		this.m_b = connection.ConnectionFactory;
		f = connection.UserConnectionOptions;
		d = connection.Pool;
		c = DbConnectionClosed.c;
	}

	internal DbConnectionBase(DbConnectionFactory A_0)
	{
		Utils.CheckArgumentNull(A_0, "connectionFactory");
		this.m_b = A_0;
		c = DbConnectionClosed.c;
	}

	protected internal void AddWeakReference(object value, int tag)
	{
		InnerConnection.a(value, tag);
	}

	protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
	{
		OnTransactionStateChanging(TransactionAction.BeginTransaction);
		DbTransactionBase dbTransactionBase = (DbTransactionBase)InnerConnection.BeginTransaction(isolationLevel);
		OnTransactionStateChanged(TransactionAction.BeginTransaction);
		dbTransactionBase.StateChanging += Transaction_StateChanging;
		dbTransactionBase.StateChanged += Transaction_StateChanged;
		return dbTransactionBase;
	}

	protected void Transaction_StateChanging(object sender, TransactionStateChangeEventArgs e)
	{
		OnTransactionStateChanging(e.Action);
	}

	protected void Transaction_StateChanged(object sender, TransactionStateChangeEventArgs e)
	{
		OnTransactionStateChanged(e.Action);
		if (e.Action == TransactionAction.Commit || e.Action == TransactionAction.Rollback)
		{
			DbTransactionBase dbTransactionBase = (DbTransactionBase)sender;
			dbTransactionBase.StateChanging -= Transaction_StateChanging;
			dbTransactionBase.StateChanged -= Transaction_StateChanged;
		}
	}

	public override void ChangeDatabase(string value)
	{
		InnerConnection.ChangeDatabase(value);
	}

	public override void Close()
	{
		try
		{
			a(0);
		}
		finally
		{
			InnerConnection.Close();
			GC.KeepAlive(this);
		}
	}

	protected override DbCommand CreateDbCommand()
	{
		DbProviderFactory providerFactory = ConnectionFactory.ProviderFactory;
		DbCommand dbCommand = providerFactory.CreateCommand();
		dbCommand.Connection = this;
		return dbCommand;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			f = null;
			d = null;
			Close();
		}
		base.Dispose(disposing);
	}

	private DbMetaDataFactory a(DbConnectionInternal A_0)
	{
		return this.m_b.a(d, A_0);
	}

	internal DbMetaDataFactory b(DbConnectionInternal A_0)
	{
		return a(A_0);
	}

	public override DataTable GetSchema()
	{
		return GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
	}

	public override DataTable GetSchema(string collectionName)
	{
		return GetSchema(collectionName, null);
	}

	public override DataTable GetSchema(string collectionName, string[] restrictionValues)
	{
		return InnerConnection.a(this, collectionName, restrictionValues);
	}

	internal void a(int A_0)
	{
		InnerConnection.a(A_0);
	}

	protected void OnStateChange(ConnectionState originalState, ConnectionState currentState)
	{
		e?.Invoke(this, new StateChangeEventArgs(originalState, currentState));
	}

	private void a(ConnectionState A_0, ConnectionState A_1)
	{
		if (A_0 != A_1)
		{
			if (A_1 == ConnectionState.Closed)
			{
				Interlocked.Increment(ref g);
			}
			OnStateChange(A_0, A_1);
		}
	}

	public override void Open()
	{
		if (State == ConnectionState.Open)
		{
			return;
		}
		using (LocalFailoverManager.StartUse(fireConnErrorEvent: true))
		{
			try
			{
				InnerConnection.Open(this);
			}
			catch (Exception ex)
			{
				if (LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Connect, RetryMode.Raise, ex) == RetryMode.Raise)
				{
					throw;
				}
			}
		}
		if (State != ConnectionState.Open || string.IsNullOrEmpty(ConnectionOptions.InitializationCommand))
		{
			return;
		}
		DbCommand dbCommand = CreateCommand();
		dbCommand.CommandText = ConnectionOptions.InitializationCommand;
		try
		{
			dbCommand.ExecuteNonQuery();
		}
		catch (Exception ex2)
		{
			try
			{
				Close();
			}
			catch
			{
			}
			throw ex2;
		}
	}

	protected internal void RemoveWeakReference(object value)
	{
		InnerConnection.g(value);
	}

	internal bool a(DbConnectionInternal A_0, bool A_1)
	{
		DbConnectionInternal dbConnectionInternal = Interlocked.Exchange(ref c, A_0);
		if (A_1)
		{
			ConnectionState a_ = dbConnectionInternal.State & ConnectionState.Open;
			ConnectionState a_2 = A_0.State & ConnectionState.Open;
			a(a_, a_2);
		}
		return true;
	}

	internal bool a(DbConnectionInternal A_0, DbConnectionInternal A_1, bool A_2)
	{
		DbConnectionInternal dbConnectionInternal = Interlocked.CompareExchange(ref c, A_0, A_1);
		bool flag = A_1 == dbConnectionInternal;
		if (A_2 && flag)
		{
			ConnectionState a_ = dbConnectionInternal.State & ConnectionState.Open;
			ConnectionState a_2 = A_0.State & ConnectionState.Open;
			a(a_, a_2);
		}
		return flag;
	}

	public IAsyncResult BeginOpen(AsyncCallback callback, object stateObject)
	{
		if (m != null)
		{
			throw new InvalidOperationException();
		}
		m = Open;
		return m.BeginInvoke(callback, stateObject);
	}

	public void EndOpen(IAsyncResult result)
	{
		if (m == null)
		{
			throw new InvalidOperationException();
		}
		try
		{
			m.EndInvoke(result);
		}
		finally
		{
			m = null;
		}
	}

	internal void b(DbCommand A_0)
	{
		AddWeakReference(A_0, 1);
	}

	internal void a(DbCommand A_0)
	{
		RemoveWeakReference(A_0);
	}

	internal void a(DbDataReader A_0)
	{
		AddWeakReference(A_0, 2);
	}

	internal void b(DbDataReader A_0)
	{
		RemoveWeakReference(A_0);
	}

	public override void EnlistTransaction(Transaction transaction)
	{
		DbConnectionInternal dbConnectionInternal = InnerConnection;
		if (dbConnectionInternal == null || dbConnectionInternal is DbConnectionClosed)
		{
			throw new InvalidOperationException(al.a("ConnMustOpen"));
		}
		dbConnectionInternal.EnlistToDistributedTransaction(transaction);
	}

	protected virtual bool IsConnectionLostError(Exception e)
	{
		throw new Exception("The method or operation is not implemented.");
	}

	protected internal virtual bool InTransaction()
	{
		throw new Exception("The method or operation is not implemented.");
	}

	internal RetryMode a(object A_0, ConnectionLostCause A_1, RetryMode A_2, Exception A_3, ref int A_4, bool A_5)
	{
		if (!LocalFailover || HasLocalFailoverRestriction())
		{
			return RetryMode.Raise;
		}
		ConnectionLostEventHandler connectionLostEventHandler = (ConnectionLostEventHandler)base.Events[l];
		if (connectionLostEventHandler != null)
		{
			while (true)
			{
				if (c == null || A_4 > 2147483646 || !IsConnectionLostError(A_3))
				{
					return RetryMode.Raise;
				}
				ConnectionLostContext context = ConnectionLostContext.None;
				if (c.ak())
				{
					context = ConnectionLostContext.InFetch;
					A_2 = RetryMode.Raise;
				}
				else if (c.an())
				{
					context = ConnectionLostContext.HasPrepared;
					A_2 = RetryMode.Raise;
				}
				else if (InTransaction())
				{
					context = ConnectionLostContext.InTransaction;
					A_2 = RetryMode.Raise;
				}
				A_4++;
				ConnectionLostEventArgs e2 = new ConnectionLostEventArgs(A_0, A_1, context, A_2, A_4);
				connectionLostEventHandler(A_0, e2);
				A_2 = e2.RetryMode;
				try
				{
					RetryMode retryMode = A_2;
					if (retryMode == RetryMode.Reexecute)
					{
						Reconnect();
					}
					else if (A_5)
					{
						DoErrorEvent(A_3);
					}
				}
				catch (Exception)
				{
					A_2 = RetryMode.Raise;
					continue;
				}
				break;
			}
		}
		else
		{
			A_2 = RetryMode.Raise;
		}
		return A_2;
	}

	protected virtual void Reconnect()
	{
	}

	protected virtual void DoErrorEvent(Exception ex)
	{
	}

	protected virtual bool HasLocalFailoverRestriction()
	{
		return false;
	}

	protected void OnTransactionStateChanging(TransactionAction action)
	{
		if (p != null)
		{
			TransactionStateChangingEventArgs e2 = new TransactionStateChangingEventArgs(action);
			p(this, e2);
		}
	}

	protected void OnTransactionStateChanged(TransactionAction action)
	{
		if (q != null)
		{
			TransactionStateChangedEventArgs e2 = new TransactionStateChangedEventArgs(action);
			q(this, e2);
		}
	}
}
