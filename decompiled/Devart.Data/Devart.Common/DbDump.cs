using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

namespace Devart.Common;

public abstract class DbDump : Component
{
	private delegate void b(a A_0, params object[] A_1);

	private enum a
	{
		a,
		b,
		c,
		d,
		e,
		f,
		g,
		h,
		i,
		j,
		k,
		l
	}

	private DbConnection m_a;

	private string m_b;

	private StringCollection m_c;

	private bool m_d;

	private bool e;

	private DumpMode f;

	private bool g;

	private b h;

	public DbConnection Connection
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

	public virtual string Tables
	{
		get
		{
			return b();
		}
		set
		{
			a(value);
		}
	}

	public virtual string DumpText
	{
		get
		{
			return this.m_b;
		}
		set
		{
			this.m_b = value;
		}
	}

	[r("DbDump_QuoteIdentifier")]
	[DefaultValue(false)]
	[Category("Options")]
	public bool QuoteIdentifier
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

	[r("DbDump_IncludeDrop")]
	[Category("Options")]
	[DefaultValue(false)]
	public bool IncludeDrop
	{
		get
		{
			return e;
		}
		set
		{
			e = value;
		}
	}

	[r("DbDump_Mode")]
	[Category("Options")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(DumpMode.All)]
	public DumpMode Mode
	{
		get
		{
			return f;
		}
		set
		{
			f = value;
		}
	}

	[DefaultValue(false)]
	[r("DbDump_GenerateHeader")]
	[Category("Options")]
	public bool GenerateHeader
	{
		get
		{
			return g;
		}
		set
		{
			g = value;
		}
	}

	protected abstract Encoding Encoding { get; }

	protected StringCollection InnerTables => this.m_c;

	protected bool BackupData => (Mode & DumpMode.Data) != 0;

	protected bool BackupSchema => (Mode & DumpMode.Schema) != 0;

	protected DbDump()
	{
		this.m_b = string.Empty;
		this.m_c = new StringCollection();
		this.m_d = false;
		e = false;
		f = DumpMode.All;
		g = false;
	}

	public void Backup()
	{
		a("Backup", A_1: false);
		d();
	}

	public void Backup(string fileName)
	{
		a("Backup", A_1: false);
		d(fileName);
	}

	public void Backup(Stream stream)
	{
		a("Backup", A_1: false);
		b(stream);
	}

	public void Backup(TextWriter writer)
	{
		a("Backup", A_1: false);
		a(writer);
	}

	public IAsyncResult BeginBackup()
	{
		return BeginBackup(null, null);
	}

	public IAsyncResult BeginBackup(AsyncCallback callback, object stateObject)
	{
		a("BeginBackup", A_1: true);
		return h.BeginInvoke(DbDump.a.a, null, callback, stateObject);
	}

	public IAsyncResult BeginBackup(string fileName)
	{
		return BeginBackup(fileName, null, null);
	}

	public IAsyncResult BeginBackup(string fileName, AsyncCallback callback, object stateObject)
	{
		a("BeginBackup", A_1: true);
		return h.BeginInvoke(DbDump.a.b, new object[1] { fileName }, callback, stateObject);
	}

	public IAsyncResult BeginBackup(Stream stream)
	{
		return BeginBackup(stream, null, null);
	}

	public IAsyncResult BeginBackup(Stream stream, AsyncCallback callback, object stateObject)
	{
		a("BeginBackup", A_1: true);
		return h.BeginInvoke(DbDump.a.c, new object[1] { stream }, callback, stateObject);
	}

	public IAsyncResult BeginBackup(TextWriter writer)
	{
		return BeginBackup(writer, null, null);
	}

	public IAsyncResult BeginBackup(TextWriter writer, AsyncCallback callback, object stateObject)
	{
		a("BeginBackup", A_1: true);
		return h.BeginInvoke(DbDump.a.d, new object[1] { writer }, callback, stateObject);
	}

	public void EndBackup(IAsyncResult result)
	{
		a(result);
	}

	public void BackupQuery(string query)
	{
		a("BackupQuery", A_1: false);
		c(query);
	}

	public void BackupQuery(string query, string fileName)
	{
		a("BackupQuery", A_1: false);
		a(query, fileName);
	}

	public void BackupQuery(string query, Stream stream)
	{
		a("BackupQuery", A_1: false);
		a(query, stream);
	}

	public void BackupQuery(string query, TextWriter writer)
	{
		a("BackupQuery", A_1: false);
		a(query, writer);
	}

	public IAsyncResult BeginBackupQuery(string query)
	{
		return BeginBackupQuery(query, null, null);
	}

	public IAsyncResult BeginBackupQuery(string query, AsyncCallback callback, object stateObject)
	{
		a("BeginBackupQuery", A_1: true);
		return h.BeginInvoke(DbDump.a.e, new object[1] { query }, callback, stateObject);
	}

	public IAsyncResult BeginBackupQuery(string query, string fileName)
	{
		return BeginBackupQuery(query, fileName, null, null);
	}

	public IAsyncResult BeginBackupQuery(string query, string fileName, AsyncCallback callback, object stateObject)
	{
		a("BeginBackupQuery", A_1: true);
		return h.BeginInvoke(DbDump.a.f, new object[2] { query, fileName }, callback, stateObject);
	}

	public IAsyncResult BeginBackupQuery(string query, Stream stream)
	{
		return BeginBackupQuery(query, stream, null, null);
	}

	public IAsyncResult BeginBackupQuery(string query, Stream stream, AsyncCallback callback, object stateObject)
	{
		a("BeginBackupQuery", A_1: true);
		return h.BeginInvoke(DbDump.a.g, new object[2] { query, stream }, callback, stateObject);
	}

	public IAsyncResult BeginBackupQuery(string query, TextWriter writer)
	{
		return BeginBackupQuery(query, writer, null, null);
	}

	public IAsyncResult BeginBackupQuery(string query, TextWriter writer, AsyncCallback callback, object stateObject)
	{
		a("BeginBackupQuery", A_1: true);
		return h.BeginInvoke(DbDump.a.h, new object[2] { query, writer }, callback, stateObject);
	}

	public void EndBackupQuery(IAsyncResult result)
	{
		a(result);
	}

	public void Restore()
	{
		a("Restore", A_1: false);
		c();
	}

	public void Restore(string fileName)
	{
		a("Restore", A_1: false);
		b(fileName);
	}

	public void Restore(Stream stream)
	{
		a("Restore", A_1: false);
		a(stream);
	}

	public void Restore(TextReader reader)
	{
		a("Restore", A_1: false);
		a(reader);
	}

	public IAsyncResult BeginRestore()
	{
		return BeginRestore(null, null);
	}

	public IAsyncResult BeginRestore(AsyncCallback callback, object stateObject)
	{
		a("BeginRestore", A_1: true);
		return h.BeginInvoke(DbDump.a.i, null, callback, stateObject);
	}

	public IAsyncResult BeginRestore(string fileName)
	{
		return BeginRestore(fileName, null, null);
	}

	public IAsyncResult BeginRestore(string fileName, AsyncCallback callback, object stateObject)
	{
		a("BeginRestore", A_1: true);
		return h.BeginInvoke(DbDump.a.j, new object[1] { fileName }, callback, stateObject);
	}

	public IAsyncResult BeginRestore(Stream stream)
	{
		return BeginRestore(stream, null, null);
	}

	public IAsyncResult BeginRestore(Stream stream, AsyncCallback callback, object stateObject)
	{
		a("BeginRestore", A_1: true);
		return h.BeginInvoke(DbDump.a.j, new object[1] { stream }, callback, stateObject);
	}

	public IAsyncResult BeginRestore(TextReader reader)
	{
		return BeginRestore(reader, null, null);
	}

	public IAsyncResult BeginRestore(TextReader reader, AsyncCallback callback, object stateObject)
	{
		a("BeginRestore", A_1: true);
		return h.BeginInvoke(DbDump.a.j, new object[1] { reader }, callback, stateObject);
	}

	public void EndRestore(IAsyncResult result)
	{
		a(result);
	}

	protected abstract void InternalBackup(TextWriter writer);

	protected abstract void InternalBackupQuery(TextWriter writer, string query);

	protected abstract void InternalRestore(TextReader reader);

	private void d()
	{
		using StringWriter stringWriter = new StringWriter();
		try
		{
			a(stringWriter);
		}
		finally
		{
			this.m_b = stringWriter.ToString();
		}
	}

	private void d(string A_0)
	{
		using StreamWriter a_ = new StreamWriter(A_0, append: false, Encoding);
		a(a_);
	}

	private void b(Stream A_0)
	{
		a(new StreamWriter(A_0, Encoding));
	}

	private void a(TextWriter A_0)
	{
		try
		{
			InternalBackup(A_0);
		}
		finally
		{
			A_0.Flush();
		}
	}

	private void c(string A_0)
	{
		using StringWriter stringWriter = new StringWriter();
		try
		{
			a(A_0, stringWriter);
		}
		finally
		{
			this.m_b = stringWriter.ToString();
		}
	}

	private void a(string A_0, string A_1)
	{
		using StreamWriter a_ = new StreamWriter(A_1, append: false, Encoding);
		a(A_0, a_);
	}

	private void a(string A_0, Stream A_1)
	{
		a(A_0, new StreamWriter(A_1, Encoding));
	}

	private void a(string A_0, TextWriter A_1)
	{
		InternalBackupQuery(A_1, A_0);
	}

	private void c()
	{
		if (Utils.IsEmpty(this.m_b))
		{
			throw new InvalidOperationException(n.a("YouMustSetupDumpTextProperty"));
		}
		using StringReader a_ = new StringReader(this.m_b);
		a(a_);
	}

	private void b(string A_0)
	{
		using StreamReader a_ = new StreamReader(A_0, Encoding);
		a(a_);
	}

	private void a(Stream A_0)
	{
		a(new StreamReader(A_0, Encoding));
	}

	private void a(TextReader A_0)
	{
		InternalRestore(A_0);
	}

	protected void CheckConnection()
	{
		if (this.m_a == null)
		{
			throw new InvalidOperationException(n.a("ConnectionNotInit"));
		}
		if (this.m_a.State != ConnectionState.Open)
		{
			throw new InvalidOperationException(n.a("ConnMustOpen"));
		}
	}

	private string b()
	{
		string text = "";
		StringEnumerator enumerator = this.m_c.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				if (current != string.Empty)
				{
					text = text + current + ";";
				}
			}
			return text;
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void a(string A_0)
	{
		this.m_c.Clear();
		if (A_0 == null || !(A_0 != ""))
		{
			return;
		}
		string[] array = A_0.Split(new char[1] { ';' });
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text != string.Empty)
			{
				this.m_c.Add(text);
			}
		}
	}

	private bool a()
	{
		return Connection != null;
	}

	private void a(a A_0, params object[] A_1)
	{
		switch (A_0)
		{
		case DbDump.a.a:
			d();
			break;
		case DbDump.a.b:
			d((string)A_1[0]);
			break;
		case DbDump.a.c:
			b((Stream)A_1[0]);
			break;
		case DbDump.a.d:
			a((TextWriter)A_1[0]);
			break;
		case DbDump.a.e:
			c((string)A_1[0]);
			break;
		case DbDump.a.f:
			a((string)A_1[0], (string)A_1[1]);
			break;
		case DbDump.a.g:
			a((string)A_1[0], (Stream)A_1[1]);
			break;
		case DbDump.a.h:
			a((string)A_1[0], (TextWriter)A_1[1]);
			break;
		case DbDump.a.i:
			c();
			break;
		case DbDump.a.j:
			b((string)A_1[0]);
			break;
		case DbDump.a.k:
			a((Stream)A_1[0]);
			break;
		case DbDump.a.l:
			a((TextReader)A_1[0]);
			break;
		}
	}

	private void a(IAsyncResult A_0)
	{
		Utils.CheckArgumentNull(A_0, "result");
		if (h != null)
		{
			try
			{
				h.EndInvoke(A_0);
			}
			finally
			{
				h = null;
			}
		}
	}

	private void a(string A_0, bool A_1)
	{
		if (h != null)
		{
			throw new InvalidOperationException(n.a("DbDump_ExecutionInProgress", A_0));
		}
		if (A_1)
		{
			h = a;
		}
	}
}
