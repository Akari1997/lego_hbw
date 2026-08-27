using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;

namespace Devart.Common;

public abstract class DbScript : Component
{
	private const int m_a = 30;

	protected readonly Lexer lexer;

	private SqlStatementCollection m_b;

	private bool c;

	private IDbConnection d;

	private int e;

	internal uint f;

	private StreamReader g;

	private int h = 30;

	protected bool commandTimeoutChanged;

	private string i;

	private ScriptErrorEventHandler j;

	private ScriptProgressEventHandler k;

	[MergableProperty(false)]
	public IDbConnection Connection
	{
		get
		{
			return d;
		}
		set
		{
			d = value;
			InitializeFromConnection();
		}
	}

	[TypeConverter("Devart.Common.Design.DbScriptScriptTextConverter, Devart.Data.Design")]
	[MergableProperty(false)]
	public virtual string ScriptText
	{
		get
		{
			string text = lexer.Text;
			if (text == null)
			{
				return "";
			}
			return text;
		}
		set
		{
			if (lexer.Text != value)
			{
				lexer.Text = value;
				InternalReset();
			}
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public SqlStatementCollection Statements
	{
		get
		{
			if (lexer.TextReader != null)
			{
				throw new InvalidOperationException();
			}
			if (this.m_b == null)
			{
				this.m_b = CreateStatementCollection();
				lexer.Reset();
				SqlStatement stmt;
				while (GetNextStatement(out stmt))
				{
					this.m_b.Add(stmt);
				}
				e = 0;
			}
			return this.m_b;
		}
	}

	[DefaultValue(30)]
	[r("DbCommand_CommandTimeout")]
	[Category("Data")]
	public int CommandTimeout
	{
		get
		{
			if (!commandTimeoutChanged && Connection != null)
			{
				return GetDefaultCommandTimeout();
			}
			return h;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentException(n.a("InvalidCommandTimeout", value));
			}
			if (h != value)
			{
				h = value;
				commandTimeoutChanged = true;
			}
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
				if (i != null)
				{
					return i;
				}
				return string.Empty;
			}
			return Site.Name;
		}
		set
		{
			if (Site == null)
			{
				i = ((value == null) ? string.Empty : value);
			}
		}
	}

	[r("DbScript_Error")]
	public event ScriptErrorEventHandler Error
	{
		add
		{
			ScriptErrorEventHandler scriptErrorEventHandler = j;
			ScriptErrorEventHandler scriptErrorEventHandler2;
			do
			{
				scriptErrorEventHandler2 = scriptErrorEventHandler;
				ScriptErrorEventHandler value2 = (ScriptErrorEventHandler)Delegate.Combine(scriptErrorEventHandler2, value);
				scriptErrorEventHandler = Interlocked.CompareExchange(ref j, value2, scriptErrorEventHandler2);
			}
			while ((object)scriptErrorEventHandler != scriptErrorEventHandler2);
		}
		remove
		{
			ScriptErrorEventHandler scriptErrorEventHandler = j;
			ScriptErrorEventHandler scriptErrorEventHandler2;
			do
			{
				scriptErrorEventHandler2 = scriptErrorEventHandler;
				ScriptErrorEventHandler value2 = (ScriptErrorEventHandler)Delegate.Remove(scriptErrorEventHandler2, value);
				scriptErrorEventHandler = Interlocked.CompareExchange(ref j, value2, scriptErrorEventHandler2);
			}
			while ((object)scriptErrorEventHandler != scriptErrorEventHandler2);
		}
	}

	[r("DbScript_Progress")]
	public event ScriptProgressEventHandler Progress
	{
		add
		{
			ScriptProgressEventHandler scriptProgressEventHandler = k;
			ScriptProgressEventHandler scriptProgressEventHandler2;
			do
			{
				scriptProgressEventHandler2 = scriptProgressEventHandler;
				ScriptProgressEventHandler value2 = (ScriptProgressEventHandler)Delegate.Combine(scriptProgressEventHandler2, value);
				scriptProgressEventHandler = Interlocked.CompareExchange(ref k, value2, scriptProgressEventHandler2);
			}
			while ((object)scriptProgressEventHandler != scriptProgressEventHandler2);
		}
		remove
		{
			ScriptProgressEventHandler scriptProgressEventHandler = k;
			ScriptProgressEventHandler scriptProgressEventHandler2;
			do
			{
				scriptProgressEventHandler2 = scriptProgressEventHandler;
				ScriptProgressEventHandler value2 = (ScriptProgressEventHandler)Delegate.Remove(scriptProgressEventHandler2, value);
				scriptProgressEventHandler = Interlocked.CompareExchange(ref k, value2, scriptProgressEventHandler2);
			}
			while ((object)scriptProgressEventHandler != scriptProgressEventHandler2);
		}
	}

	protected DbScript(Lexer lexer)
	{
		this.lexer = lexer;
	}

	protected override void Dispose(bool disposing)
	{
		if (g != null)
		{
			g.Close();
		}
		base.Dispose(disposing);
	}

	public void Execute()
	{
		c = false;
		if (d == null)
		{
			throw new Exception(n.a("ConnectionNotInit"));
		}
		if (d.State != ConnectionState.Open)
		{
			throw new InvalidOperationException(n.a("ConnNotOpen"));
		}
		Reset();
		IDataReader reader;
		while (ExecuteNext(out reader))
		{
			reader?.Close();
		}
	}

	public void Open(Stream stream)
	{
		lexer.TextReader = new StreamReader(stream, Encoding.Default);
		InternalReset();
	}

	public void Open(string fileName)
	{
		if (g != null)
		{
			g.Close();
			g = null;
		}
		g = new StreamReader(fileName, Encoding.Default);
		lexer.TextReader = g;
		InternalReset();
	}

	public void Open(TextReader reader)
	{
		lexer.TextReader = reader;
		InternalReset();
	}

	public bool ExecuteNext(out IDataReader reader)
	{
		if (d == null)
		{
			throw new Exception(n.a("ConnectionNotInit"));
		}
		if (d.State != ConnectionState.Open)
		{
			throw new InvalidOperationException(n.a("ConnNotOpen"));
		}
		reader = null;
		SqlStatement stmt = null;
		bool flag = false;
		try
		{
			if (this.m_b == null)
			{
				GetNextStatement(out stmt);
			}
			else if (this.e < this.m_b.Count)
			{
				stmt = this.m_b[this.e];
				this.e++;
			}
			if (stmt != null)
			{
				reader = stmt.Execute();
				flag = true;
			}
		}
		catch (Exception ex)
		{
			a(stmt, out var A_, out var A_2, out var A_3, out var A_4, out var A_5, out var A_6);
			ScriptErrorEventArgs e = new ScriptErrorEventArgs(ex, A_, A_2, A_3, A_4, A_5, A_6);
			OnError(e);
			if (!e.Ignore)
			{
				throw;
			}
			flag = true;
		}
		flag &= !c;
		if (flag)
		{
			OnProgress(stmt);
		}
		return flag;
	}

	public virtual void Reset()
	{
		if (this.m_b == null)
		{
			lexer.Reset();
		}
		else
		{
			e = 0;
		}
	}

	protected virtual void InternalReset()
	{
		f++;
		if (this.m_b != null)
		{
			this.m_b.Clear();
		}
		this.m_b = null;
	}

	protected virtual bool CanExecuteStatement(SqlStatement sqlStatement)
	{
		return sqlStatement.StatementType != SqlStatementType.Extended;
	}

	protected void CancelExecute()
	{
		c = true;
	}

	protected SqlStatement CreateSqlStatement(int offset, int length, int line, int position, string text, SqlStatementType statementType)
	{
		return new SqlStatement(this, offset, length, line, position, text, statementType);
	}

	internal IDataReader a(SqlStatement A_0)
	{
		return ExecuteSqlStatement(A_0);
	}

	protected virtual IDataReader ExecuteSqlStatement(SqlStatement sqlStatement)
	{
		IDataReader reader;
		switch (OnSqlStatementExecute(sqlStatement, out reader))
		{
		case SqlStatementStatus.Cancel:
			CancelExecute();
			break;
		case SqlStatementStatus.Continue:
			if (CanExecuteStatement(sqlStatement))
			{
				using (IDbCommand dbCommand = CreateCommand())
				{
					dbCommand.CommandTimeout = CommandTimeout;
					a(dbCommand);
					dbCommand.CommandText = sqlStatement.Text;
					return dbCommand.ExecuteReader();
				}
			}
			break;
		}
		return reader;
	}

	protected virtual IDbCommand CreateCommand()
	{
		return Connection.CreateCommand();
	}

	protected virtual SqlStatementStatus OnSqlStatementExecute(SqlStatement stmt, out IDataReader reader)
	{
		reader = null;
		return SqlStatementStatus.Continue;
	}

	protected abstract bool GetNextStatement(out SqlStatement stmt);

	protected virtual SqlStatementType GetStatementType(Token token)
	{
		SqlStatementType result = SqlStatementType.Unknown;
		switch (token.Id)
		{
		case 2028:
			result = SqlStatementType.Select;
			break;
		case 2029:
			result = SqlStatementType.Insert;
			break;
		case 2031:
			result = SqlStatementType.Update;
			break;
		case 2030:
			result = SqlStatementType.Delete;
			break;
		case 2025:
			result = SqlStatementType.Alter;
			break;
		case 2027:
			result = SqlStatementType.Drop;
			break;
		case 2026:
			result = SqlStatementType.Create;
			break;
		case 2033:
			result = SqlStatementType.Commit;
			break;
		case 2034:
			result = SqlStatementType.Rollback;
			break;
		case 2032:
			result = SqlStatementType.With;
			break;
		}
		return result;
	}

	protected void OnProgress(SqlStatement stmt)
	{
		if (k != null)
		{
			a(stmt, out var A_, out var A_2, out var A_3, out var A_4, out var A_5, out var A_6);
			k(this, new ScriptProgressEventArgs(A_, A_2, A_3, A_4, A_5, A_6));
		}
	}

	private static void a(SqlStatement A_0, out string A_1, out int A_2, out int A_3, out int A_4, out int A_5, out SqlStatementType A_6)
	{
		A_1 = null;
		A_2 = -1;
		A_3 = -1;
		A_4 = -1;
		A_5 = -1;
		A_6 = (SqlStatementType)(-1);
		if (A_0 != null)
		{
			A_1 = A_0.Text;
			A_2 = A_0.Offset;
			A_3 = A_0.Length;
			A_4 = A_0.LineNumber;
			A_5 = A_0.LinePosition;
			A_6 = A_0.StatementType;
		}
	}

	protected void OnError(ScriptErrorEventArgs e)
	{
		if (j != null)
		{
			j(this, e);
		}
	}

	internal string b(SqlStatement A_0)
	{
		return GetStatementText(A_0);
	}

	protected virtual SqlStatementCollection CreateStatementCollection()
	{
		return new SqlStatementCollection();
	}

	protected virtual string GetStatementText(SqlStatement stmt)
	{
		if (stmt != null)
		{
			return ScriptText.Substring(stmt.Offset, stmt.Length);
		}
		return string.Empty;
	}

	protected virtual void InitializeFromConnection()
	{
	}

	protected virtual int GetDefaultCommandTimeout()
	{
		return h;
	}

	private void a(object A_0)
	{
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(A_0).Find("Name", ignoreCase: false);
		if (propertyDescriptor != null)
		{
			if (Utils.IsEmpty(Name))
			{
				string fullName = GetType().FullName;
				int num = fullName.LastIndexOfAny(new char[1] { '.' });
				propertyDescriptor.SetValue(A_0, fullName.Substring(num + 1, fullName.Length - num - 1) + GetHashCode());
			}
			else
			{
				propertyDescriptor.SetValue(A_0, Name);
			}
		}
	}
}
