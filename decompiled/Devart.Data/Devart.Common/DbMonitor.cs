using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using Devart.DbMonitor;

namespace Devart.Common;

public abstract class DbMonitor : Component, IDisposable
{
	private const string m_a = "ConnectionString = \"";

	private const string m_b = "Connect: ";

	private const string c = "Creating pool manager";

	private const string d = "Creating pool with connections string: ";

	private const string e = "Creating object";

	private const string f = "Taking connection from connection pool: ";

	private const string g = "Connection is taken from pool.";

	private const string h = "Connection is returning to pool.";

	private const string i = "Connection is returned to pool.";

	private const string j = "Disconnect";

	private const string k = "Begin local transaction";

	private const string l = "Begin distributed transaction";

	private const string m = "Begin TransactionScope local transaction";

	private const string n = "Prepare commit";

	private const string o = "Commit";

	private const string p = "Rollback";

	private const string q = "Open connection: ";

	private const string r = "Close connection";

	private const string s = "Execute: ";

	private const string t = "Prepare: ";

	private bool u;

	private bool v = true;

	private static Dictionary<int, long> w = new Dictionary<int, long>();

	private static c x = new c();

	private MonitorEventHandler y;

	protected abstract string ProductName { get; }

	[DefaultValue(false)]
	[r("DbMonitor_IsActive")]
	[Category("Behavior")]
	public abstract bool IsActive { get; set; }

	[DefaultValue(false)]
	[r("DbMonitor_UseIdeOutput")]
	[Category("Behavior")]
	public bool UseIdeOutput
	{
		get
		{
			return u;
		}
		set
		{
			u = value;
		}
	}

	[DefaultValue("localhost")]
	[r("DbMonitor_Host")]
	public string Host
	{
		get
		{
			return x.e();
		}
		set
		{
			if (DbMonitorAppAvailable)
			{
				x.a(value);
				return;
			}
			throw new NotSupportedException(Devart.Common.n.a("DbMonitor_NotSupportApp"));
		}
	}

	[r("DbMonitor_Port")]
	[DefaultValue(1000)]
	public int Port
	{
		get
		{
			return x.g();
		}
		set
		{
			if (DbMonitorAppAvailable)
			{
				x.c(value);
				return;
			}
			throw new NotSupportedException(Devart.Common.n.a("DbMonitor_NotSupportApp"));
		}
	}

	[r("DbMonitor_UseApp")]
	[DefaultValue(true)]
	public bool UseApp
	{
		get
		{
			if (DbMonitorAppAvailable)
			{
				return v;
			}
			return false;
		}
		set
		{
			if (DbMonitorAppAvailable)
			{
				v = value;
				if (!v)
				{
					b();
				}
				return;
			}
			throw new NotSupportedException(Devart.Common.n.a("DbMonitor_NotSupportApp"));
		}
	}

	[DefaultValue(1000)]
	[r("DbMonitor_EventQueueLimit")]
	public int EventQueueLimit
	{
		get
		{
			return x.d();
		}
		set
		{
			if (DbMonitorAppAvailable)
			{
				x.b(value);
				return;
			}
			throw new NotSupportedException(Devart.Common.n.a("DbMonitor_NotSupportApp"));
		}
	}

	protected abstract bool DbMonitorAppAvailable { get; }

	[r("DbMonitor_TraceEvent")]
	public event MonitorEventHandler TraceEvent
	{
		add
		{
			MonitorEventHandler monitorEventHandler = y;
			MonitorEventHandler monitorEventHandler2;
			do
			{
				monitorEventHandler2 = monitorEventHandler;
				MonitorEventHandler value2 = (MonitorEventHandler)Delegate.Combine(monitorEventHandler2, value);
				monitorEventHandler = Interlocked.CompareExchange(ref y, value2, monitorEventHandler2);
			}
			while ((object)monitorEventHandler != monitorEventHandler2);
		}
		remove
		{
			MonitorEventHandler monitorEventHandler = y;
			MonitorEventHandler monitorEventHandler2;
			do
			{
				monitorEventHandler2 = monitorEventHandler;
				MonitorEventHandler value2 = (MonitorEventHandler)Delegate.Remove(monitorEventHandler2, value);
				monitorEventHandler = Interlocked.CompareExchange(ref y, value2, monitorEventHandler2);
			}
			while ((object)monitorEventHandler != monitorEventHandler2);
		}
	}

	void IDisposable.Dispose()
	{
		Dispose(disposing: true);
	}

	protected override void Dispose(bool disposing)
	{
		IsActive = false;
		base.Dispose(disposing);
	}

	internal void a(IDbDataParameter A_0, out string A_1, out string A_2, out string A_3, out string A_4)
	{
		GetParameterInfo(A_0, out A_1, out A_2, out A_3, out A_4);
	}

	private static void a(DbMonitor A_0, object A_1, MonitorEventArgs A_2)
	{
		a(A_0, A_1, A_2, A_3: false);
	}

	private static void a(DbMonitor A_0, object A_1, MonitorEventArgs A_2, bool A_3)
	{
		if (A_0 != null && A_1 != null)
		{
			if (A_2.TracePoint == MonitorTracePoint.BeforeEvent)
			{
				a(A_1);
			}
			A_0.OnTraceEvent(A_1, A_2, A_3);
		}
	}

	protected static void OnPoolManagerCreate(DbMonitor monitor, MonitorTracePoint tracePoint, object sender)
	{
		if (monitor != null)
		{
			MonitorEventArgs a_ = new MonitorEventArgs(Devart.Common.d.j, "Creating pool manager", tracePoint, null, a(), a(sender, tracePoint));
			a(monitor, sender, a_, A_3: true);
		}
	}

	protected static void OnPoolGroupCreate(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, string connectionString)
	{
		if (monitor != null)
		{
			string a_ = "Creating pool with connections string: \"" + connectionString + "\"";
			MonitorEventArgs a_2 = new MonitorEventArgs(Devart.Common.d.k, a_, tracePoint, connectionString, a(), a(sender, tracePoint));
			a(monitor, sender, a_2, A_3: true);
		}
	}

	protected static void OnConnect(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, string connectionString, bool pooled)
	{
		if (monitor != null)
		{
			string a_ = "Connect: \"" + connectionString + "\"";
			a(monitor, sender, new MonitorEventArgs(pooled ? Devart.Common.d.l : Devart.Common.d.m, a_, tracePoint, connectionString, a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnDisconnect(DbMonitor monitor, MonitorTracePoint tracePoint, object sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.b, "Disconnect", tracePoint, "", a(), a(sender, tracePoint)));
		}
	}

	protected static void OnTakeFromPool(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender, string connectionString, object dbConnectionPool)
	{
		if (monitor != null)
		{
			string a_ = "Taking connection from connection pool: \"" + connectionString + "\"";
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.n, a_, tracePoint, connectionString, a(), a(sender, tracePoint)));
			if (tracePoint == MonitorTracePoint.AfterEvent)
			{
				a(monitor, sender, dbConnectionPool, connectionString, "Connection is taken from pool.");
			}
		}
	}

	private static void a(DbMonitor A_0, object A_1, object A_2, string A_3, string A_4)
	{
		if (A_0 != null)
		{
			int poolGroupConnectionCount = A_0.GetPoolGroupConnectionCount(A_2);
			if (poolGroupConnectionCount >= 0)
			{
				string a_ = A_4 + " Pool has " + poolGroupConnectionCount + " connection(s).";
				a(A_0, A_1, new MonitorEventArgs(Devart.Common.d.n, a_, MonitorTracePoint.BeforeEvent, A_3, a(), 0.0));
				a(A_0, A_1, new MonitorEventArgs(Devart.Common.d.n, a_, MonitorTracePoint.AfterEvent, A_3, a(), 0.0));
			}
		}
	}

	protected static void OnReturnToPool(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, object dbConnectionPool, string connectionString)
	{
		if (monitor != null)
		{
			string empty = string.Empty;
			int poolGroupConnectionCount = monitor.GetPoolGroupConnectionCount(dbConnectionPool);
			empty = "Connection is returned to pool. Pool has " + poolGroupConnectionCount + " connection(s).";
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.o, empty, tracePoint, connectionString, a(), a(sender, tracePoint)));
		}
	}

	protected static void OnActivate(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender, string connectionString)
	{
		if (monitor != null)
		{
			string a_ = "Open connection: \"" + connectionString + "\"";
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.a, a_, tracePoint, connectionString, a(), a(sender, tracePoint)));
		}
	}

	protected static void OnDeactivate(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, string connectionString)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.b, "Close connection", tracePoint, connectionString, a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnExecute(DbMonitor monitor, MonitorTracePoint tracePoint, IDbCommand sender, string sql, int rowsAffected)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.d, "Execute: " + sql, tracePoint, rowsAffected.ToString(), a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnPrepare(DbMonitor monitor, MonitorTracePoint tracePoint, IDbCommand sender, string sql)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.c, "Prepare: " + sql, tracePoint, null, a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnBeginLocalTransaction(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.e, "Begin local transaction", tracePoint, "", a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnBeginDistributedTransaction(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.e, "Begin distributed transaction", tracePoint, "", a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnBeginTransactionScopeLocalTransaction(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.e, "Begin TransactionScope local transaction", tracePoint, "", a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnPrepareCommit(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.f, "Prepare commit", tracePoint, "", a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnCommit(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.f, "Commit", tracePoint, "", a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnRollback(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.g, "Rollback", tracePoint, "", a(), a(sender, tracePoint)));
		}
	}

	protected internal static void OnError(DbMonitor monitor, Exception e, object sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.h, e.Message, MonitorTracePoint.AfterEvent, "", a(), a(sender, MonitorTracePoint.AfterEvent)));
		}
	}

	protected internal static void OnCustomAction(DbMonitor monitor, MonitorTracePoint tracePoint, string description, object sender)
	{
		if (monitor != null)
		{
			a(monitor, sender, new MonitorEventArgs(Devart.Common.d.i, description, tracePoint, "", a(), a(sender, MonitorTracePoint.AfterEvent)));
		}
	}

	protected static void OnCreate(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, bool isParentMessage)
	{
		if (monitor != null)
		{
			MonitorEventArgs a_ = new MonitorEventArgs(Devart.Common.d.p, "Creating object", tracePoint, "", a(), a(sender, MonitorTracePoint.AfterEvent));
			a(monitor, sender, a_, isParentMessage);
		}
	}

	private string a(object A_0, string[] A_1)
	{
		string objectName = GetObjectName(A_0);
		if (!string.IsNullOrEmpty(objectName))
		{
			return objectName;
		}
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(A_0).Find("Name", ignoreCase: false);
		if (propertyDescriptor != null && !Utils.IsEmpty((string)propertyDescriptor.GetValue(A_0)))
		{
			return (string)propertyDescriptor.GetValue(A_0) + " (" + A_0.GetHashCode() + ")";
		}
		if (A_0 is Component && ((Component)A_0).Site != null)
		{
			return ((Component)A_0).Site.Name;
		}
		string fullName = A_0.GetType().FullName;
		int num = fullName.LastIndexOfAny(new char[1] { '.' });
		string text = A_0.GetHashCode().ToString();
		if (A_1 != null && A_1.Length > 0)
		{
			string text2 = null;
			for (int num2 = A_1.Length - 1; num2 >= 0; num2--)
			{
				text2 = A_1[num2];
				if (!text2.StartsWith("Devart.") && !text2.StartsWith("System.Data."))
				{
					break;
				}
			}
			if (!string.IsNullOrEmpty(text2))
			{
				int num3 = text2.IndexOf('(');
				if (num3 >= 0)
				{
					text2 = text2.Substring(0, num3);
				}
				text = "in " + text2.Split(new char[1] { '.' })[^1].TrimEnd(new char[2] { ')', '(' });
			}
		}
		return fullName.Substring(num + 1, fullName.Length - num - 1) + " (" + text + ")";
	}

	private static string[] a()
	{
		StackTrace stackTrace = new StackTrace(fNeedFileInfo: false);
		_ = stackTrace.FrameCount;
		List<string> list = new List<string>(stackTrace.FrameCount);
		for (int num = 0; num < stackTrace.FrameCount; num++)
		{
			MethodBase method = stackTrace.GetFrame(num).GetMethod();
			list.Add(a(method));
		}
		list.Reverse();
		return list.ToArray();
	}

	private static string a(MethodBase A_0)
	{
		string value = (((object)A_0.DeclaringType != null) ? (A_0.DeclaringType.FullName + '.' + A_0.Name) : A_0.Name);
		StringBuilder stringBuilder = new StringBuilder(value);
		stringBuilder.Append('(');
		ParameterInfo[] parameters = A_0.GetParameters();
		foreach (ParameterInfo parameterInfo in parameters)
		{
			Type parameterType = parameterInfo.ParameterType;
			string text = string.Empty;
			if (parameterType.IsArray)
			{
				text = a(parameterType);
			}
			stringBuilder.Append(parameterType.Name + text + ' ' + parameterInfo.Name);
		}
		stringBuilder.Append(')');
		return stringBuilder.ToString();
	}

	private static string a(Type A_0)
	{
		int arrayRank = A_0.GetArrayRank();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append('[');
		stringBuilder.Append(',', arrayRank - 1);
		stringBuilder.Append(']');
		return stringBuilder.ToString();
	}

	private static void a(object A_0)
	{
		int hashCode = A_0.GetHashCode();
		long value = af.a();
		lock (w)
		{
			if (w.ContainsKey(hashCode))
			{
				w[hashCode] = value;
			}
			else
			{
				w.Add(hashCode, value);
			}
		}
	}

	private static double a(object A_0, MonitorTracePoint A_1)
	{
		double result = -1.0;
		if (A_1 == MonitorTracePoint.AfterEvent && A_0 != null)
		{
			int hashCode = A_0.GetHashCode();
			long num = 0L;
			lock (w)
			{
				if (w.ContainsKey(hashCode))
				{
					num = w[hashCode];
					w.Remove(hashCode);
				}
			}
			if (num > 0)
			{
				result = af.a(num);
			}
		}
		return result;
	}

	protected void OnTraceEvent(object sender, MonitorEventArgs e, bool parentMessage)
	{
		if (DbMonitorAppAvailable)
		{
			try
			{
				Devart.DbMonitor.c a_ = a(sender, e);
				a(e.TracePoint, a_, parentMessage);
			}
			catch
			{
			}
		}
		try
		{
			if (e.TracePoint == MonitorTracePoint.AfterEvent && u)
			{
				Type typeFromHandle = typeof(Trace);
				MethodInfo method = typeFromHandle.GetMethod("WriteLine", new Type[1] { typeof(string) });
				string text = ProductName + ": " + GetObjectName(sender) + " - " + e.Description;
				method.Invoke(typeFromHandle, new object[1] { text });
			}
		}
		catch
		{
		}
		try
		{
			if (y != null && e.IsUserEvent)
			{
				y(sender, e);
			}
		}
		catch
		{
		}
	}

	internal void b()
	{
		try
		{
			x.b();
		}
		catch
		{
		}
	}

	private Devart.DbMonitor.c a(object A_0, MonitorEventArgs A_1)
	{
		Devart.DbMonitor.c c2 = new Devart.DbMonitor.c();
		c2.d(A_1.Description);
		object parentObject = GetParentObject(A_0);
		if (parentObject != null)
		{
			c2.c(parentObject.GetHashCode());
			c2.a(a(parentObject, A_1.CallStack));
		}
		else
		{
			c2.c(0);
			c2.a(string.Empty);
		}
		if (parentObject is IDbConnection)
		{
			c2.a(Devart.DbMonitor.d.b);
		}
		c2.b(A_0.GetHashCode());
		c2.f(a(A_0, A_1.CallStack));
		c2.m().AddRange(A_1.CallStack);
		switch (A_1.EventTypeInternal)
		{
		case Devart.Common.d.j:
			c2.a(Devart.DbMonitor.k.n);
			c2.b(Devart.DbMonitor.d.f);
			break;
		case Devart.Common.d.k:
			c2.a(Devart.DbMonitor.k.n);
			c2.b(Devart.DbMonitor.d.f);
			break;
		case Devart.Common.d.l:
			c2.a(Devart.DbMonitor.k.c);
			c2.b(Devart.DbMonitor.d.f);
			break;
		case Devart.Common.d.m:
			c2.a(Devart.DbMonitor.k.c);
			c2.b(Devart.DbMonitor.d.b);
			break;
		case Devart.Common.d.a:
			c2.a(Devart.DbMonitor.k.c);
			c2.b(Devart.DbMonitor.d.b);
			break;
		case Devart.Common.d.n:
		case Devart.Common.d.o:
			c2.a(Devart.DbMonitor.k.p);
			c2.b(Devart.DbMonitor.d.f);
			break;
		case Devart.Common.d.b:
			c2.a(Devart.DbMonitor.k.d);
			c2.b(Devart.DbMonitor.d.b);
			break;
		case Devart.Common.d.c:
		case Devart.Common.d.d:
		{
			if (A_1.EventTypeInternal == Devart.Common.d.c)
			{
				c2.a(Devart.DbMonitor.k.i);
			}
			else
			{
				c2.a(Devart.DbMonitor.k.k);
			}
			IDbCommand dbCommand = (IDbCommand)A_0;
			Devart.DbMonitor.b[] array = new Devart.DbMonitor.b[dbCommand.Parameters.Count];
			for (int num = 0; num < array.Length; num++)
			{
				GetParameterInfo((IDbDataParameter)dbCommand.Parameters[num], out var name, out var dbType, out var direction, out var value);
				array[num].c(name);
				array[num].b(dbType);
				array[num].a(direction);
				array[num].d(value);
			}
			int a_ = -1;
			if (A_1.ExtraInfo != null)
			{
				a_ = Convert.ToInt32(A_1.ExtraInfo);
			}
			c2.c().AddRange(array);
			c2.e(dbCommand.CommandText);
			c2.d(a_);
			c2.b(Devart.DbMonitor.d.d);
			break;
		}
		case Devart.Common.d.e:
			c2.a(Devart.DbMonitor.k.e);
			c2.b(Devart.DbMonitor.d.b);
			break;
		case Devart.Common.d.f:
			c2.a(Devart.DbMonitor.k.f);
			c2.b(Devart.DbMonitor.d.c);
			break;
		case Devart.Common.d.g:
			c2.a(Devart.DbMonitor.k.g);
			c2.b(Devart.DbMonitor.d.c);
			break;
		case Devart.Common.d.h:
			c2.d("Error");
			c2.a(Devart.DbMonitor.k.q);
			c2.g(A_1.Description);
			c2.a(A_0: true);
			break;
		case Devart.Common.d.p:
			c2.a(Devart.DbMonitor.k.n);
			if (A_0 is IDbConnection)
			{
				c2.b(Devart.DbMonitor.d.b);
			}
			else if (A_0 is IDbCommand)
			{
				c2.b(Devart.DbMonitor.d.d);
			}
			if (!(c2.q() == A_0.GetType().FullName.ToString()))
			{
			}
			break;
		default:
			c2.a(Devart.DbMonitor.k.q);
			break;
		}
		return c2;
	}

	private void a(MonitorTracePoint A_0, Devart.DbMonitor.c A_1, bool A_2)
	{
		if (UseApp)
		{
			if (!object.ReferenceEquals(x.c(), ProductName))
			{
				x.b(ProductName);
			}
			x.a(A_1, A_0, A_2);
		}
	}

	protected virtual object GetParentObject(object sender)
	{
		if (sender is IDbCommand dbCommand)
		{
			return dbCommand.Connection;
		}
		return null;
	}

	protected abstract string GetObjectName(object obj);

	protected abstract int GetPoolGroupConnectionCount(object dbConnectionPool);

	protected virtual void GetParameterInfo(IDbDataParameter parameter, out string name, out string dbType, out string direction, out string value)
	{
		name = parameter.ParameterName;
		dbType = parameter.DbType.ToString();
		direction = parameter.Direction.ToString();
		object value2 = parameter.Value;
		if (Utils.IsNull(value2))
		{
			value = "NULL";
		}
		else
		{
			value = value2.ToString();
		}
	}

	protected void SetMonitorActive(bool value)
	{
		if (!value)
		{
			b();
		}
	}
}
