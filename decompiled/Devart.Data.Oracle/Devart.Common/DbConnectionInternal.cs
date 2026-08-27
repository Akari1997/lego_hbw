using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace Devart.Common;

internal abstract class DbConnectionInternal
{
	private readonly bool m_a;

	private readonly bool m_b;

	protected WeakReference c;

	private object m_d;

	private DbConnectionPool m_e;

	private DbReferenceCollection f;

	private DateTime m_g;

	private bool h;

	private int i;

	protected Guid j;

	protected bool k;

	private WeakReference l;

	protected DbConnectionOptions m;

	internal object n;

	protected Transaction o;

	protected virtual bool EnlistOnActive => true;

	internal bool AllowSetConnectionString => this.m_a;

	internal DateTime CreateTime => this.m_g;

	internal bool CanBePooled
	{
		get
		{
			if (!h)
			{
				return !Utils.GetWeakIsAlive(this.c);
			}
			return false;
		}
	}

	internal DbReferenceCollection ReferenceCollection => f;

	protected internal bool IsConnectionDoomed => h;

	internal virtual bool IsEmancipated
	{
		get
		{
			if (i < 1 && !Utils.GetWeakIsAlive(this.c))
			{
				return !ConnectionIsClosedAndDeffered;
			}
			return false;
		}
	}

	protected internal object Owner
	{
		get
		{
			return Utils.GetWeakTarget(this.c);
		}
		set
		{
			Utils.SetWeakTarget(ref this.c, value);
		}
	}

	protected internal object LastOwner
	{
		get
		{
			object obj = Owner;
			if (obj != null)
			{
				return obj;
			}
			return this.m_d;
		}
		set
		{
			this.m_d = value;
		}
	}

	internal DbConnectionPool Pool => this.m_e;

	public abstract string ServerVersion { get; }

	public virtual string ServerVersionNormalized
	{
		get
		{
			throw new NotSupportedException();
		}
	}

	public bool ShouldHidePassword => this.m_b;

	public abstract ConnectionState State { get; }

	public Transaction Transaction
	{
		get
		{
			lock (this)
			{
				ab();
				return o;
			}
		}
	}

	public Transaction LastTransaction
	{
		get
		{
			Transaction transaction = ((l == null) ? null : ((Transaction)l.Target));
			if (transaction != null)
			{
				try
				{
					_ = transaction.TransactionInformation;
				}
				catch (ObjectDisposedException)
				{
					transaction = null;
					l = null;
				}
			}
			return transaction;
		}
	}

	public virtual bool SimulateTwoPhaseCommit => false;

	public virtual bool TwoPhaseCommitSupported => false;

	public bool ConnectionIsClosedAndDeffered
	{
		get
		{
			return k;
		}
		set
		{
			k = value;
		}
	}

	protected DbConnectionOptions ConnectionOptions => m;

	protected DbConnectionInternal()
		: this(A_0: true, A_1: false)
	{
	}

	internal DbConnectionInternal(bool A_0, bool A_1)
	{
		this.m_b = A_0;
		this.m_a = A_1;
		this.m_g = DateTime.UtcNow;
	}

	internal void ai()
	{
		e();
	}

	internal void aa()
	{
		c();
		z();
		a(Owner);
	}

	internal void a(object A_0, int A_1)
	{
		DbReferenceCollection dbReferenceCollection = f;
		if (dbReferenceCollection == null)
		{
			dbReferenceCollection = (f = d());
		}
		dbReferenceCollection?.Add(A_0, A_1);
	}

	public abstract DbTransaction BeginTransaction(System.Data.IsolationLevel il);

	public virtual void ChangeDatabase(string value)
	{
		throw new NotSupportedException();
	}

	public virtual void CloseInternalConnection()
	{
		DbConnectionPool dbConnectionPool = Pool;
		if (dbConnectionPool != null && State == ConnectionState.Open)
		{
			a(dbConnectionPool.ConnectionOptions);
			aa();
			lock (this)
			{
				d(Owner);
			}
			dbConnectionPool.PutObject(this);
		}
		else
		{
			c();
			Owner = null;
			Dispose();
			a(Owner);
		}
	}

	public virtual void Close()
	{
		DbConnectionBase dbConnectionBase = (DbConnectionBase)Owner;
		if (dbConnectionBase == null || !dbConnectionBase.a(DbConnectionClosed.e, this, A_2: false))
		{
			return;
		}
		try
		{
			CloseInternalConnection();
		}
		finally
		{
			dbConnectionBase.a(DbConnectionClosed.d, A_1: true);
		}
	}

	protected virtual void a(DbConnectionOptions A_0)
	{
	}

	protected internal void af()
	{
		h = true;
		Pool?.MarkInvalidVersion();
	}

	internal virtual DbReferenceCollection d()
	{
		return new DbReferenceCollection();
	}

	protected virtual void c()
	{
		a();
		l = null;
	}

	protected virtual void e()
	{
		if (EnlistOnActive)
		{
			EnlistToDistributedTransaction(Transaction.Current);
		}
	}

	protected abstract void a(object A_0);

	protected virtual void z()
	{
	}

	public virtual void Commit()
	{
	}

	public virtual void Rollback()
	{
	}

	protected void ac()
	{
	}

	public void Dispose()
	{
		a(A_0: true);
	}

	protected virtual void a(bool A_0)
	{
		if (A_0)
		{
			Close();
		}
		this.m_e = null;
	}

	protected internal virtual DataTable a(DbConnectionBase A_0, string A_1, string[] A_2)
	{
		DbMetaDataFactory dbMetaDataFactory = A_0.b(this);
		return dbMetaDataFactory.GetSchema(A_0, this, A_1, A_2);
	}

	internal void e(object A_0)
	{
		this.m_e = null;
		Owner = A_0;
		i = -1;
	}

	internal void a(DbConnectionPool A_0)
	{
		this.m_e = A_0;
	}

	internal void a(int A_0)
	{
		a(A_0, 0);
	}

	internal void a(int A_0, int A_1)
	{
		ReferenceCollection?.Notify(A_0, A_1, this);
	}

	public virtual void Open(DbConnectionBase outerConnection)
	{
		throw new InvalidOperationException(al.a("ConnectionAlreadyOpen"));
	}

	internal void c(object A_0)
	{
		if (Owner != null)
		{
			throw new InvalidOperationException(al.a("PooledObjectHasOwner"));
		}
		Owner = A_0;
		i--;
		if (Pool != null)
		{
			if (i != 0)
			{
				throw new InvalidOperationException(al.a("PooledObjectInPoolMoreThanOnce"));
			}
		}
		else if (i != -1)
		{
			throw new InvalidOperationException(al.a("NonPooledObjectUsedMoreThanOnce"));
		}
	}

	internal void d(object A_0)
	{
		if (A_0 == null)
		{
			if (Owner != null)
			{
				throw new InvalidOperationException(al.a("UnpooledObjectHasOwner"));
			}
		}
		else if (Owner != A_0)
		{
			throw new InvalidOperationException(al.a("UnpooledObjectHasWrongOwner"));
		}
		if (i != 0)
		{
			throw new InvalidOperationException(al.a("PushingObjectSecondTime"));
		}
		i++;
		Owner = null;
	}

	protected void ae()
	{
		ReferenceCollection?.Purge();
	}

	internal void g(object A_0)
	{
		ReferenceCollection?.Remove(A_0);
	}

	internal void ad()
	{
		af();
	}

	internal bool ak()
	{
		if (f == null)
		{
			return false;
		}
		foreach (object item in f)
		{
			if (item is IDataReader)
			{
				return true;
			}
		}
		return false;
	}

	internal bool an()
	{
		if (f == null)
		{
			return false;
		}
		PropertyInfo property = typeof(DbCommandBase).GetProperty("IsPrepared", BindingFlags.Instance | BindingFlags.NonPublic);
		foreach (object item in f)
		{
			if (item is DbCommandBase obj && (bool)property.GetValue(obj, null))
			{
				return true;
			}
		}
		return false;
	}

	protected virtual w b(Transaction A_0)
	{
		return new w(this, A_0.IsolationLevel);
	}

	public void EnlistToDistributedTransaction(Transaction transaction)
	{
		if (!(transaction == null))
		{
			EnlistToDistributedTransactionInternal(transaction);
		}
	}

	public void EnlistToDistributedTransactionInternal(Transaction transaction)
	{
		if (((DbConnectionBase)Owner).InTransaction())
		{
			throw new InvalidOperationException("Cannot enlist in the distributed transaction because local transaction already exists.");
		}
		if (Transaction != null)
		{
			if (Transaction != transaction)
			{
				throw new InvalidOperationException("Connection is already attached to distributed transaction.");
			}
			return;
		}
		o = transaction;
		l = new WeakReference(transaction);
		w w2 = b(transaction);
		if (!TwoPhaseCommitSupported)
		{
			if (ah.d(transaction))
			{
				throw new InvalidOperationException("Cannot enlist local transaction, because current global transaction already contains distributed transactions.");
			}
			j = Guid.Empty;
			a(transaction, w2);
		}
		else if (!SimulateTwoPhaseCommit)
		{
			j = a(transaction);
			transaction.EnlistVolatile((IEnlistmentNotification)w2, EnlistmentOptions.None);
		}
		else
		{
			j = Guid.Empty;
			transaction.EnlistVolatile(w2, EnlistmentOptions.None);
		}
		ah.a(transaction, this);
		w2.a(a);
		w2.a(j);
	}

	private static void a(Transaction A_0, w A_1)
	{
		if (!A_0.EnlistPromotableSinglePhase(A_1))
		{
			throw new InvalidOperationException("Cannot enlist transaction. Possibly single phase transaction was already used.");
		}
	}

	private static Guid a(Transaction A_0)
	{
		byte[] array = A_0.TransactionInformation.DistributedIdentifier.ToByteArray();
		byte[] bytes = Encoding.Default.GetBytes(A_0.TransactionInformation.LocalIdentifier);
		byte[] array2 = new byte[array.Length + bytes.Length];
		Buffer.BlockCopy(array, 0, array2, 0, array.Length);
		Buffer.BlockCopy(bytes, 0, array2, array.Length, bytes.Length);
		SHA1 sHA = SHA1.Create();
		byte[] src = sHA.ComputeHash(array2);
		byte[] array3 = new byte[16];
		Buffer.BlockCopy(src, 0, array3, 0, array3.Length);
		((IDisposable)sHA).Dispose();
		return new Guid(array3);
	}

	protected virtual void a()
	{
		if (!(o == null))
		{
			o = null;
		}
	}

	protected void ab()
	{
		if (!(o == null))
		{
			bool flag = true;
			try
			{
				_ = o.TransactionInformation;
			}
			catch (ObjectDisposedException)
			{
				flag = false;
			}
			if (!flag)
			{
				a();
			}
		}
	}

	private void a(object A_0, EventArgs A_1)
	{
		lock (this)
		{
			if (ConnectionIsClosedAndDeffered)
			{
				CloseInternalConnection();
				ConnectionIsClosedAndDeffered = false;
			}
			else
			{
				a();
			}
		}
	}

	public virtual void BeginTransaction(Guid distributedIdentifier, System.Transactions.IsolationLevel isolationLevel)
	{
	}

	public virtual void PrepareCommit()
	{
	}

	public bool EquivalentTo(DbConnectionOptions options)
	{
		return ConnectionOptions.Equals(options);
	}
}
