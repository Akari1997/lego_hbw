using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Devart.Common;

public abstract class DbCommandBase : DbCommand
{
	private delegate object a(CommandBehavior A_0);

	public const int DefaultCommandTimeout = 30;

	private string m_a;

	private int b;

	private CommandType c;

	private UpdateRowSource d;

	private bool e;

	private bool f;

	private IDisposable g;

	private string h;

	protected WeakReference weakDataReader;

	private a i;

	private string j;

	private object k;

	[DefaultValue("")]
	[r("DbCommand_CommandText")]
	[Category("Data")]
	[MergableProperty(false)]
	[RefreshProperties(RefreshProperties.All)]
	public override string CommandText
	{
		get
		{
			string text = this.m_a;
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (this.m_a != value)
			{
				PropertyChanging();
				this.m_a = value;
				UpdateParameters();
			}
		}
	}

	[Category("Data")]
	[r("DbCommand_CommandTimeout")]
	public override int CommandTimeout
	{
		get
		{
			return b;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentException(Devart.Common.n.a("InvalidCommandTimeout", value));
			}
			b = value;
		}
	}

	[Category("Data")]
	[RefreshProperties(RefreshProperties.All)]
	[r("DbCommand_CommandType")]
	[DefaultValue(CommandType.Text)]
	public override CommandType CommandType
	{
		get
		{
			CommandType commandType = c;
			if (commandType == (CommandType)0)
			{
				return CommandType.Text;
			}
			return commandType;
		}
		set
		{
			if (c != value)
			{
				if (value != CommandType.Text && value != CommandType.StoredProcedure && value != CommandType.TableDirect)
				{
					throw new ArgumentException(Devart.Common.n.a("InvalidCommandType", value));
				}
				PropertyChanging();
				c = value;
				UpdateParameters();
			}
		}
	}

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignOnly(true)]
	[DefaultValue(true)]
	public override bool DesignTimeVisible
	{
		get
		{
			return !e;
		}
		set
		{
			e = !value;
			TypeDescriptor.Refresh(this);
		}
	}

	[r("DbCommand_UpdatedRowSource")]
	[DefaultValue(UpdateRowSource.Both)]
	[Category("Update")]
	public override UpdateRowSource UpdatedRowSource
	{
		get
		{
			return d;
		}
		set
		{
			if (d != value)
			{
				if (value != UpdateRowSource.None && value != UpdateRowSource.OutputParameters && value != UpdateRowSource.FirstReturnedRecord && value != UpdateRowSource.Both)
				{
					throw new ArgumentException(Devart.Common.n.a("InvalidUpdateRowSource"));
				}
				d = value;
			}
		}
	}

	protected abstract ILocalFailoverManager LocalFailoverManager { get; }

	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(false)]
	[Category("Behavior")]
	[r("DbCommand_ParameterCheck")]
	public bool ParameterCheck
	{
		get
		{
			return f;
		}
		set
		{
			if (value != f)
			{
				f = value;
				UpdateParameters();
			}
		}
	}

	protected internal string Sql
	{
		get
		{
			if (CommandText.Length == 0)
			{
				throw new InvalidOperationException(Devart.Common.n.a("CommandTextRequired"));
			}
			if (h == null)
			{
				h = CreateSql();
			}
			return h;
		}
	}

	protected internal IDisposable Stmt => g;

	protected internal bool IsPrepared => g != null;

	protected internal bool HasOpenReader
	{
		get
		{
			DbDataReader dataReader = DataReader;
			if (dataReader != null)
			{
				return !dataReader.IsClosed;
			}
			return false;
		}
	}

	protected internal DbDataReader DataReader => (DbDataReader)Utils.GetWeakTarget(weakDataReader);

	[EditorBrowsable(EditorBrowsableState.Never)]
	[DefaultValue("")]
	[Browsable(false)]
	public string Name
	{
		get
		{
			if (Site == null)
			{
				if (j != null)
				{
					return j;
				}
				return string.Empty;
			}
			return Site.Name;
		}
		set
		{
			if (Site == null)
			{
				j = ((value == null) ? string.Empty : value);
			}
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public object Owner
	{
		get
		{
			return k;
		}
		set
		{
			if (k != null && !base.DesignMode)
			{
				GlobalComponentsCache.RemoveFromGlobalList(this);
			}
			k = value;
			if (k != null && !base.DesignMode)
			{
				GlobalComponentsCache.AddToGlobalList(this);
			}
		}
	}

	protected DbCommandBase()
	{
		b = 30;
		d = UpdateRowSource.Both;
	}

	protected DbCommandBase(DbCommandBase from)
	{
		this.m_a = from.m_a;
		c = from.c;
		b = from.b;
		d = from.d;
		e = from.e;
	}

	protected void SaveParameter(DbParameter result)
	{
		if (DbCommandBuilder.a != null)
		{
			DbCommandBuilder.a.Add(this);
			DbCommandBuilder.a.Add(result);
		}
	}

	public override void Cancel()
	{
	}

	public override int ExecuteNonQuery()
	{
		using IDataReader dataReader = ExecuteReader();
		dataReader.Close();
		return dataReader.RecordsAffected;
	}

	protected virtual void PropertyChanging()
	{
		Unprepare();
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void ResetCommandTimeout()
	{
		b = 30;
	}

	protected virtual bool ShouldSerializeCommandTimeout()
	{
		return b != 30;
	}

	public int GetRecordCount()
	{
		try
		{
			using DbCommand dbCommand = base.Connection.CreateCommand();
			dbCommand.CommandTimeout = CommandTimeout;
			dbCommand.CommandText = GetRecordCountSql(n());
			dbCommand.Parameters.Clear();
			for (int i = 0; i < base.Parameters.Count; i++)
			{
				DbParameterBase dbParameterBase;
				if (base.Parameters[i] is ICloneable cloneable)
				{
					dbParameterBase = (DbParameterBase)cloneable.Clone();
				}
				else
				{
					dbParameterBase = (DbParameterBase)dbCommand.CreateParameter();
					((DbParameterBase)base.Parameters[i]).CopyTo(dbParameterBase);
				}
				dbCommand.Parameters.Add(dbParameterBase);
			}
			return Convert.ToInt32(dbCommand.ExecuteScalar());
		}
		catch (Exception a_)
		{
			throw new QueryRecordCountException(Devart.Common.n.a("ErrorRetrievingRecordCount"), a_);
		}
	}

	protected virtual string GetRecordCountSql(string commandText)
	{
		return "select count(*) as record_count from (" + commandText + "\r\n) record_count_table";
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			Unprepare();
		}
		base.Dispose(disposing);
		if (!base.DesignMode && Owner != null)
		{
			GlobalComponentsCache.RemoveFromGlobalList(this);
		}
	}

	protected abstract void ParseSqlParameters(string sql);

	protected abstract void DescribeProcedure(string name);

	protected abstract void ClearParameters();

	protected internal void SetParameterCheck(bool parameterCheck)
	{
		f = parameterCheck;
	}

	protected internal void CreateParameters()
	{
		switch (CommandType)
		{
		case CommandType.TableDirect:
			if (Utils.IsEmpty(CommandText))
			{
				throw new InvalidOperationException(Devart.Common.n.a("TableNameNotDef"));
			}
			ClearParameters();
			break;
		case CommandType.Text:
			ParseSqlParameters(CommandText);
			break;
		case CommandType.StoredProcedure:
			if (Utils.IsEmpty(CommandText))
			{
				throw new InvalidOperationException(Devart.Common.n.a("ProcNameNotDef"));
			}
			DescribeProcedure(CommandText);
			break;
		}
	}

	protected internal void UpdateParameters()
	{
		if (f)
		{
			switch (CommandType)
			{
			case CommandType.TableDirect:
				ClearParameters();
				break;
			case CommandType.Text:
				ParseSqlParameters(CommandText);
				break;
			}
		}
	}

	protected virtual string CreateSql()
	{
		return CommandType switch
		{
			CommandType.Text => CommandText, 
			CommandType.TableDirect => "SELECT * FROM " + CommandText, 
			CommandType.StoredProcedure => CreateStoredProcSql(CommandText), 
			_ => throw new InvalidOperationException(Devart.Common.n.a("InvalidCommandType")), 
		};
	}

	internal string n()
	{
		return CreateSql();
	}

	protected abstract string CreateStoredProcSql(string name);

	protected abstract void AddCommand();

	protected abstract void RemoveCommand();

	protected abstract void AddDataReader(DbDataReader reader);

	public override void Prepare()
	{
		Utils.CheckConnectionOpen(base.Connection);
		using (LocalFailoverManager.StartUse(fireConnErrorEvent: true))
		{
			while (true)
			{
				try
				{
					if (!IsPrepared)
					{
						g = InternalPrepare(implicitPrepare: false, 0, 0);
						AddCommand();
					}
					break;
				}
				catch (Exception ex)
				{
					if (LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Prepare, RetryMode.Reexecute, ex) == RetryMode.Raise)
					{
						throw;
					}
				}
			}
		}
	}

	protected virtual bool IsReadOnlyOperation(IDisposable stmt)
	{
		return false;
	}

	protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
	{
		return ExecuteDbDataReader(behavior, nonQuery: false);
	}

	protected DbDataReader ExecuteDbDataReader(CommandBehavior behavior, bool nonQuery)
	{
		Utils.CheckConnectionOpen(base.Connection);
		if (HasOpenReader)
		{
			throw new InvalidOperationException(Devart.Common.n.a("ReaderNotClosed"));
		}
		IDisposable disposable = g;
		bool flag = true;
		using (LocalFailoverManager.StartUse(fireConnErrorEvent: true))
		{
			while (true)
			{
				bool flag2 = false;
				try
				{
					DbDataReader dbDataReader;
					if (disposable == null)
					{
						if (flag)
						{
							flag = false;
							UseLoadBalancing(ignoreBalancing: false);
						}
						disposable = InternalPrepare(implicitPrepare: true, 0, 0);
						flag2 = IsReadOnlyOperation(disposable);
						try
						{
							dbDataReader = InternalExecute(behavior, disposable, 0, 0, nonQuery);
							AddDataReader(dbDataReader);
							return dbDataReader;
						}
						catch
						{
							try
							{
								disposable.Dispose();
							}
							catch
							{
							}
							disposable = null;
							throw;
						}
						finally
						{
							disposable?.Dispose();
						}
					}
					if (flag)
					{
						flag = false;
						UseLoadBalancing(ignoreBalancing: true);
					}
					dbDataReader = InternalExecute(behavior, disposable, 0, 0, nonQuery);
					flag2 = IsReadOnlyOperation(disposable);
					Utils.SetWeakTarget(ref weakDataReader, dbDataReader);
					AddDataReader(dbDataReader);
					return dbDataReader;
				}
				catch (Exception ex)
				{
					if (LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Execute, flag2 ? RetryMode.Reexecute : RetryMode.Raise, ex) == RetryMode.Raise)
					{
						throw;
					}
				}
			}
		}
	}

	public DbDataReader ExecutePageReader(CommandBehavior behavior, int startRecord, int maxRecords)
	{
		return ExecutePageReaderInternal(behavior, startRecord, maxRecords);
	}

	protected virtual DbDataReader ExecutePageReaderInternal(CommandBehavior behavior, int startRecord, int maxRecords)
	{
		Utils.CheckConnectionOpen(base.Connection);
		using (LocalFailoverManager.StartUse(fireConnErrorEvent: true))
		{
			while (true)
			{
				try
				{
					IDisposable disposable = InternalPrepare(implicitPrepare: true, startRecord, maxRecords);
					try
					{
						DbDataReader dbDataReader = InternalExecute(behavior, disposable, startRecord, maxRecords);
						AddDataReader(dbDataReader);
						return dbDataReader;
					}
					finally
					{
						disposable.Dispose();
					}
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

	public override object ExecuteScalar()
	{
		using (IDataReader dataReader = ExecuteReader())
		{
			bool flag = false;
			do
			{
				if (dataReader.FieldCount > 0)
				{
					flag = true;
					break;
				}
			}
			while (dataReader.NextResult());
			if (flag && dataReader.Read())
			{
				return dataReader.GetValue(0);
			}
		}
		return null;
	}

	protected abstract IDisposable InternalPrepare(bool implicitPrepare, int startRecord, int maxRecords);

	protected abstract DbDataReader InternalExecute(CommandBehavior behavior, IDisposable stmt, int startRecord, int maxRecords);

	protected virtual DbDataReader InternalExecute(CommandBehavior behavior, IDisposable stmt, int startRecord, int maxRecords, bool nonQuery)
	{
		return InternalExecute(behavior, stmt, startRecord, maxRecords);
	}

	protected virtual void UseLoadBalancing(bool ignoreBalancing)
	{
	}

	protected virtual void Unprepare()
	{
		if (g != null)
		{
			if (base.Connection != null)
			{
				RemoveCommand();
			}
			g.Dispose();
			g = null;
		}
		weakDataReader = null;
		h = null;
	}

	internal void o()
	{
		Unprepare();
	}

	private object AsyncExecuteReader(CommandBehavior behavior)
	{
		return ExecuteReader(behavior);
	}

	public IAsyncResult BeginExecuteReader()
	{
		return BeginExecuteReader(null, null, CommandBehavior.Default);
	}

	public IAsyncResult BeginExecuteReader(CommandBehavior behavior)
	{
		return BeginExecuteReader(null, null, behavior);
	}

	public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject)
	{
		return BeginExecuteReader(callback, stateObject, CommandBehavior.Default);
	}

	public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior)
	{
		if (i != null)
		{
			throw new InvalidOperationException(Devart.Common.n.a("ExecutionInProgress"));
		}
		i = AsyncExecuteReader;
		return i.BeginInvoke(behavior, callback, stateObject);
	}

	public DbDataReader EndExecuteReader(IAsyncResult result)
	{
		Utils.CheckArgumentNull(result, "result");
		try
		{
			return (DbDataReader)i.EndInvoke(result);
		}
		finally
		{
			i = null;
		}
	}

	private object a(CommandBehavior A_0)
	{
		return ExecuteNonQuery();
	}

	public IAsyncResult BeginExecuteNonQuery()
	{
		return BeginExecuteNonQuery(null, null);
	}

	public IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject)
	{
		if (i != null)
		{
			throw new InvalidOperationException();
		}
		i = a;
		return i.BeginInvoke(CommandBehavior.Default, callback, stateObject);
	}

	public int EndExecuteNonQuery(IAsyncResult result)
	{
		if (i == null)
		{
			throw new InvalidOperationException();
		}
		try
		{
			return (int)i.EndInvoke(result);
		}
		finally
		{
			i = null;
		}
	}
}
