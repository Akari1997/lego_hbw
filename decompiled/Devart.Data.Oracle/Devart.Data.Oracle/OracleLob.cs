using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleLob : Stream, IDisposable, ICloneable, INullable, IComparable
{
	private OracleConnection m_a;

	private a3 m_b;

	private readonly OracleDbType m_c;

	private int m_d;

	private OracleCommand e;

	protected ReadLobMode readLobMode;

	private bool m_f;

	private MemoryStream m_g;

	private bool h;

	protected bool modifiedCache;

	private ap i;

	private static int j = 10000;

	private bool k;

	public static readonly long MaxSize = 4294967295L;

	public static readonly OracleLob NullBlob = new OracleLob(OracleDbType.Blob);

	public static readonly OracleLob NullClob = new OracleLob(OracleDbType.Clob);

	public static readonly OracleLob NullNClob = new OracleLob(OracleDbType.NClob);

	[Obsolete("This property is designed for compatibility with OracleClient only.")]
	public new static readonly OracleLob Null = new OracleLob(OracleDbType.Blob);

	public override bool CanRead => !IsNull;

	public override bool CanSeek => !IsNull;

	public override bool CanWrite
	{
		get
		{
			if (this.m_c != OracleDbType.BFile)
			{
				if (IsNull)
				{
					return Cached;
				}
				return true;
			}
			return false;
		}
	}

	public OracleConnection Connection
	{
		get
		{
			return this.m_a;
		}
		set
		{
			if (this.m_a != value)
			{
				if (this.m_b != null)
				{
					this.m_b.o();
				}
				this.m_b = null;
				this.m_a = value;
			}
		}
	}

	public bool IsBatched
	{
		get
		{
			if (IsNull)
			{
				return false;
			}
			return this.m_b.b();
		}
	}

	public bool IsEmpty => Length == 0;

	public bool IsNull
	{
		get
		{
			if (Cached && this.m_g == null)
			{
				return this.m_b == null;
			}
			if (Cached)
			{
				a();
				if (this.m_g != null)
				{
					return h;
				}
			}
			c();
			return this.m_b == null;
		}
	}

	public bool IsTemporary
	{
		get
		{
			if (IsNull)
			{
				return false;
			}
			if (d())
			{
				return this.m_b.l();
			}
			return true;
		}
	}

	public override long Length
	{
		get
		{
			if (IsNull)
			{
				return 0L;
			}
			if (Cached && this.m_g != null)
			{
				return this.m_g.Length;
			}
			if (this.m_c == OracleDbType.NClob || (this.m_c == OracleDbType.Clob && k))
			{
				return this.m_b.f() * 2;
			}
			return this.m_b.f();
		}
	}

	public OracleDbType LobType => this.m_c;

	public int ChunkSize
	{
		get
		{
			if (IsNull)
			{
				return 0;
			}
			return LobLocator.k();
		}
	}

	public override long Position
	{
		get
		{
			if (IsNull)
			{
				return 0L;
			}
			return this.m_d;
		}
		set
		{
			Seek(value, SeekOrigin.Begin);
		}
	}

	public object Value
	{
		get
		{
			if (IsNull)
			{
				return DBNull.Value;
			}
			int num = (int)Length;
			if (num == 0)
			{
				if (this.m_c == OracleDbType.Clob || this.m_c == OracleDbType.NClob)
				{
					return string.Empty;
				}
				return new byte[0];
			}
			byte[] array;
			if (Cached)
			{
				a();
				array = this.m_g.GetBuffer();
				if (array.Length > num)
				{
					array = new byte[num];
					if (num > 0)
					{
						Buffer.BlockCopy(this.m_g.GetBuffer(), 0, array, 0, num);
					}
				}
			}
			else
			{
				array = new byte[num];
				this.m_b.a(0, array, 0, array.Length);
			}
			if (this.m_c == OracleDbType.Clob || this.m_c == OracleDbType.NClob)
			{
				if (this.m_c == OracleDbType.NClob || k)
				{
					return Encoding.Unicode.GetString(array, 0, num);
				}
				return bl.a().GetString(array, 0, num);
			}
			return array;
		}
	}

	public bool Cached
	{
		get
		{
			if (readLobMode != ReadLobMode.CachedDirect)
			{
				return readLobMode == ReadLobMode.DefferedCachedDirect;
			}
			return true;
		}
		set
		{
			if (value != Cached)
			{
				if (value)
				{
					readLobMode = ReadLobMode.CachedDirect;
					return;
				}
				ClearCache();
				readLobMode = ReadLobMode.Direct;
			}
		}
	}

	internal a3 LobLocator
	{
		get
		{
			b();
			return this.m_b;
		}
	}

	protected object LobLocatorInternal => this.m_b;

	public OracleLob(OracleDbType lobType)
		: this(lobType, isUnicode: false)
	{
	}

	public OracleLob(OracleDbType lobType, bool isUnicode)
	{
		this.m_c = lobType;
		this.m_a = null;
		k = isUnicode;
		this.m_f = false;
		modifiedCache = false;
		readLobMode = ReadLobMode.CachedDirect;
		this.m_g = new MemoryStream();
		h = true;
	}

	public OracleLob(OracleConnection connection, OracleDbType lobType)
		: this(connection, lobType, isCaching: false)
	{
	}

	public OracleLob(OracleConnection connection, OracleDbType lobType, bool isCaching)
	{
		Utils.CheckArgumentNull(connection, "connection");
		this.m_c = lobType;
		this.m_a = connection;
		this.m_f = isCaching;
		modifiedCache = false;
		readLobMode = ReadLobMode.CachedDirect;
		b();
	}

	internal OracleLob(OracleConnection A_0, OracleDataReader A_1, int A_2, a3 A_3, OracleDbType A_4)
	{
		this.m_c = A_4;
		this.m_a = A_0;
		if (this.m_a != null)
		{
			i = this.m_a.d();
		}
		modifiedCache = false;
		if (A_3 != null)
		{
			k = A_3.t().h().a();
		}
		if (A_1 != null)
		{
			readLobMode = A_1.ReadLobMode;
			if (readLobMode == ReadLobMode.Value)
			{
				readLobMode = ReadLobMode.Direct;
			}
			if (A_1.ReadLobMode == ReadLobMode.Deferred)
			{
				e = a(A_1, A_2);
				this.m_b = null;
			}
			else
			{
				this.m_b = A_3;
			}
		}
		else
		{
			readLobMode = ReadLobMode.CachedDirect;
			this.m_b = A_3;
		}
		if (readLobMode == ReadLobMode.CachedDirect && this.m_b != null)
		{
			ReadLobCache();
		}
	}

	private string a(string A_0)
	{
		return "\"" + A_0 + "\"";
	}

	protected virtual string GetFullTableName(string schemaName, string tableName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (!Utils.IsEmpty(schemaName))
		{
			stringBuilder.Append(a(schemaName));
			stringBuilder.Append(".");
		}
		stringBuilder.Append(a(tableName));
		return stringBuilder.ToString();
	}

	private bool a(DataRow A_0)
	{
		if (Convert.ToBoolean(A_0["IsLong"]))
		{
			return true;
		}
		Type type = (Type)A_0["DataType"];
		if ((object)type != typeof(IDataReader) && (object)type != typeof(OracleObject) && (object)type != typeof(OracleArray))
		{
			return (object)type == typeof(OracleTable);
		}
		return true;
	}

	private static IDbDataParameter a(IDbCommand A_0, int A_1)
	{
		if (A_1 < A_0.Parameters.Count)
		{
			return (IDbDataParameter)A_0.Parameters[A_1];
		}
		return A_0.CreateParameter();
	}

	private void a(DbParameter A_0, DataRow A_1)
	{
		OracleParameter oracleParameter = (OracleParameter)A_0;
		object obj = A_1["ProviderType", DataRowVersion.Default];
		if (obj is int)
		{
			OracleDbType oracleDbType = (oracleParameter.OracleDbType = (OracleDbType)obj);
			if (oracleDbType == OracleDbType.Xml)
			{
				oracleParameter.ObjectTypeName = "SYS.XMLTYPE";
			}
			return;
		}
		try
		{
			obj = A_1["DataType", DataRowVersion.Default];
			oracleParameter.OracleDbType = OracleUtils.TypeToOracleDbType((Type)obj);
			if (oracleParameter.OracleDbType == OracleDbType.Xml)
			{
				oracleParameter.ObjectTypeName = "SYS.XMLTYPE";
			}
		}
		catch
		{
			oracleParameter.e();
		}
	}

	private OracleCommand a(OracleDataReader A_0, int A_1)
	{
		string name = A_0.GetName(A_1);
		StringBuilder stringBuilder = new StringBuilder("SELECT ");
		stringBuilder.Append(name);
		stringBuilder.Append(" FROM ");
		A_0.a(A_0.CommandBehaviorInternal | CommandBehavior.KeyInfo);
		DataTable schemaTable = A_0.GetSchemaTable();
		new ArrayList();
		string text = null;
		string text2 = null;
		foreach (DataRow row in schemaTable.Rows)
		{
			if ((string)row[SchemaTableColumn.ColumnName] == name)
			{
				text = row[SchemaTableColumn.BaseTableName] as string;
				text2 = row["BaseSchemaName"] as string;
				break;
			}
		}
		if (text == null || text == "")
		{
			throw new InvalidOperationException("Cannot retrieve key information");
		}
		stringBuilder.Append(GetFullTableName(text2, text));
		stringBuilder.Append(" WHERE ");
		DataRowCollection rows = schemaTable.Rows;
		ArrayList arrayList = new ArrayList();
		for (int num = 0; num < rows.Count; num++)
		{
			DataRow dataRow2 = rows[num];
			if (dataRow2 != null && Convert.ToString(dataRow2["BaseColumnName"]) != "" && Convert.ToString(dataRow2["BaseTableName"]) == text && Convert.ToString(dataRow2["BaseSchemaName"]) == text2 && !a(dataRow2) && (Convert.ToBoolean(dataRow2["IsKey"]) || Convert.ToBoolean(dataRow2["IsUnique"])))
			{
				arrayList.Add(dataRow2);
			}
		}
		if (arrayList.Count == 0)
		{
			throw new InvalidOperationException("Cannot retrieve key information");
		}
		e = new OracleCommand();
		e.Connection = this.m_a;
		for (int num2 = 0; num2 < arrayList.Count; num2++)
		{
			if (num2 > 0)
			{
				stringBuilder.Append(" AND ");
			}
			DataRow dataRow3 = (DataRow)arrayList[num2];
			string text3 = Convert.ToString(dataRow3["BaseColumnName"]);
			int ordinal = -1;
			for (int num3 = 0; num3 < A_0.FieldCount; num3++)
			{
				if (A_0.GetName(num3) == text3)
				{
					ordinal = num3;
					break;
				}
			}
			object value = A_0.GetValue(ordinal);
			string text4 = "p" + (e.Parameters.Count + 1).ToString(CultureInfo.CurrentCulture);
			string arg = a(text3);
			if (Convert.IsDBNull(value))
			{
				stringBuilder.Append($"{arg} IS NULL");
			}
			else
			{
				stringBuilder.Append($"{arg} = :{text4}");
			}
			if (!Convert.IsDBNull(value))
			{
				DbParameter dbParameter = e.CreateParameter();
				dbParameter.ParameterName = text4;
				a(dbParameter, dataRow3);
				dbParameter.Direction = ParameterDirection.Input;
				dbParameter.Value = value;
				dbParameter.SourceColumn = text3;
				dbParameter.SourceVersion = DataRowVersion.Original;
				e.Parameters.Add(dbParameter);
			}
		}
		e.CommandText = stringBuilder.ToString();
		return e;
	}

	protected OracleLob(OracleLob from)
	{
		this.m_b = from.m_b;
		k = from.k;
		this.m_c = from.m_c;
		this.m_d = from.m_d;
		this.m_a = from.m_a;
		from.a();
		if (from.m_g == null)
		{
			this.m_g = null;
		}
		else
		{
			if (this.m_g == null)
			{
				this.m_g = new MemoryStream();
			}
			from.m_g.WriteTo(this.m_g);
		}
		readLobMode = from.readLobMode;
		h = from.h;
	}

	public new void Dispose()
	{
		Close();
		GC.SuppressFinalize(this);
	}

	internal void f()
	{
		a();
	}

	internal void g()
	{
		this.m_d = 0;
		if (Cached)
		{
			a();
			this.m_g.Seek(0L, SeekOrigin.Begin);
		}
	}

	public override string ToString()
	{
		if ((this.m_c == OracleDbType.Clob || this.m_c == OracleDbType.NClob) && Value is string result)
		{
			return result;
		}
		if (this.m_c == OracleDbType.BFile)
		{
			return "BFile";
		}
		return "Blob(" + Length.ToString(CultureInfo.InvariantCulture) + ")";
	}

	public void Append(OracleLob source)
	{
		Utils.CheckArgumentNull(source, "source");
		if (IsNull)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotAppendToNULLOracleLob"));
		}
		if (Cached || source.Cached)
		{
			if (source.IsNull)
			{
				return;
			}
			int num = j;
			int num2 = (int)source.Length;
			if (num > num2)
			{
				num = num2;
			}
			byte[] buffer = new byte[num];
			int num3 = source.m_d;
			source.Seek(0L, SeekOrigin.Begin);
			try
			{
				int num4 = 0;
				Seek(0L, SeekOrigin.End);
				int num6;
				for (; num4 < num2; num4 += num6)
				{
					int num5 = num;
					if (num5 > num2 - num4)
					{
						num5 = num2 - num4;
					}
					num6 = source.Read(buffer, 0, num5);
					if (num6 != 0)
					{
						Write(buffer, 0, num6);
						continue;
					}
					break;
				}
				return;
			}
			finally
			{
				source.Seek(num3, SeekOrigin.Begin);
			}
		}
		LobLocator.b(source.LobLocator);
	}

	public void BeginBatch()
	{
		BeginBatch(OracleLobOpenMode.ReadOnly);
	}

	public void BeginBatch(OracleLobOpenMode openMode)
	{
		if (!Cached && !IsNull)
		{
			LobLocator.a(openMode);
		}
	}

	public virtual object Clone()
	{
		return new OracleLob(this);
	}

	public override void Close()
	{
		if (Cached && this.m_g != null)
		{
			this.m_g.Close();
		}
		if (this.m_b != null)
		{
			this.m_b.o();
		}
		this.m_b = null;
	}

	public long CopyTo(OracleLob dest)
	{
		return CopyTo(0L, dest, 0L, Length);
	}

	public long CopyTo(OracleLob dest, long destOffset)
	{
		return CopyTo(0L, dest, destOffset, Length);
	}

	public long CopyTo(long srcOffset, OracleLob dest, long destOffset, long amount)
	{
		Utils.CheckArgumentNull(dest, "dest");
		if (Cached || dest.Cached)
		{
			int num = j;
			if (num > Length)
			{
				num = (int)Length;
			}
			byte[] buffer = new byte[num];
			int num2 = this.m_d;
			Seek(srcOffset, SeekOrigin.Begin);
			int num3 = 0;
			try
			{
				dest.Seek(destOffset, SeekOrigin.Begin);
				int num5;
				for (; num3 < amount; num3 += num5)
				{
					int num4 = num;
					if (num4 > amount - num3)
					{
						num4 = (int)amount - num3;
					}
					num5 = Read(buffer, 0, num4);
					if (num5 != 0)
					{
						dest.Write(buffer, 0, num5);
						continue;
					}
					break;
				}
			}
			finally
			{
				Seek(num2, SeekOrigin.Begin);
			}
			return num3;
		}
		if (this.m_b != null)
		{
			if (dest.IsNull)
			{
				throw new InvalidOperationException(Devart.Common.al.a("CanNotCopyToNULLLOB"));
			}
			return dest.m_b.a(this.m_b, (int)srcOffset, (int)destOffset, (int)amount);
		}
		return 0L;
	}

	public void EndBatch()
	{
		if (!Cached)
		{
			LobLocator.m();
		}
	}

	public long Erase()
	{
		return Erase(1L, Length);
	}

	public long Erase(long offset, long amount)
	{
		if (IsNull)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotEraseFromNULLLOB"));
		}
		offset--;
		if (Cached)
		{
			if (offset < 0 || amount > Length - offset)
			{
				return 0L;
			}
			a();
			if (amount == Length - offset)
			{
				this.m_g.SetLength(offset);
			}
			else
			{
				byte[] buffer = this.m_g.GetBuffer();
				long length = Length;
				this.m_g.SetLength(0L);
				this.m_g.Read(buffer, 0, (int)offset);
				this.m_g.Read(buffer, (int)(offset + amount), (int)(length - (offset + amount)));
			}
			modifiedCache = true;
			return amount;
		}
		return this.m_b.a((int)offset + 1, (int)amount);
	}

	public bool IsEqual(OracleLob val)
	{
		if (IsNull)
		{
			return val.IsNull;
		}
		if (val.IsNull)
		{
			return false;
		}
		return LobLocator.a(val.LobLocator);
	}

	public override void Flush()
	{
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (IsNull)
		{
			return 0;
		}
		int num;
		if (Cached)
		{
			a();
			num = this.m_g.Read(buffer, offset, count);
		}
		else
		{
			num = this.m_b.a(this.m_d, buffer, offset, count);
		}
		this.m_d += num;
		return num;
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		int num = 0;
		if (!Cached)
		{
			num = origin switch
			{
				SeekOrigin.Begin => (int)offset, 
				SeekOrigin.Current => (int)(this.m_d + offset), 
				SeekOrigin.End => (int)(Length + offset), 
				_ => throw new ArgumentException(Devart.Common.al.a("InvalidSeekOriginValue"), "origin"), 
			};
		}
		else
		{
			a();
			num = (int)this.m_g.Seek(offset, origin);
		}
		if (num < 0 || num > Length)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotSeekBeyondLOBLength"));
		}
		return this.m_d = num;
	}

	public override void SetLength(long len)
	{
		if (this.m_c == OracleDbType.BFile)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotChangeBFILELength"));
		}
		if (IsNull)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotChangeNULLLOBLength"));
		}
		if (Cached)
		{
			a();
			if (len == Length)
			{
				return;
			}
			this.m_g.SetLength(len);
			modifiedCache = true;
		}
		else
		{
			this.m_b.a((int)len);
		}
		this.m_d = Math.Min((int)len, this.m_d);
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (this.m_c == OracleDbType.BFile)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotWriteToBFILE"));
		}
		if (IsNull && !Cached)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotWriteToNULLLOB"));
		}
		int num;
		if (Cached)
		{
			a();
			this.m_g.Write(buffer, offset, count);
			num = count;
			modifiedCache = true;
			h = false;
		}
		else
		{
			num = this.m_b.b(this.m_d, buffer, offset, count);
		}
		this.m_d += num;
	}

	public override void WriteByte(byte value)
	{
		Write(new byte[1] { value }, 0, 1);
	}

	private bool d()
	{
		if (this.m_b == null || (this.m_a != null && this.m_a.d() != i))
		{
			return false;
		}
		return true;
	}

	private void c()
	{
		if (readLobMode != ReadLobMode.Deferred || d())
		{
			return;
		}
		Utils.CheckArgumentNull(this.m_a, "connection");
		i = this.m_a.d();
		if (e == null)
		{
			return;
		}
		OracleDataReader oracleDataReader = e.ExecuteReader();
		try
		{
			if (oracleDataReader.Read())
			{
				OracleLob oracleLob = oracleDataReader.GetOracleLob(0);
				if (oracleLob != null && !oracleDataReader.Read())
				{
					this.m_b = oracleLob.LobLocator;
					k = this.m_b.t().h().a();
				}
			}
		}
		finally
		{
			oracleDataReader.Close();
		}
	}

	private void b()
	{
		if (d())
		{
			if (Cached && modifiedCache)
			{
				WriteLobCache(clearLob: true);
			}
			return;
		}
		Utils.CheckArgumentNull(this.m_a, "connection");
		if (readLobMode == ReadLobMode.Deferred)
		{
			c();
		}
		else
		{
			if (!Cached && readLobMode != ReadLobMode.Direct)
			{
				return;
			}
			ah ah2 = (ah)0;
			switch (this.m_c)
			{
			case OracleDbType.Clob:
				ah2 = ah.b;
				break;
			case OracleDbType.NClob:
				ah2 = ah.c;
				break;
			case OracleDbType.Blob:
				ah2 = ah.a;
				break;
			default:
				throw new ArgumentException(Devart.Common.al.a("InvalidDataTypeForOracleLob"), "lobType");
			case OracleDbType.BFile:
				break;
			}
			i = this.m_a.d();
			aq aq2 = i.w();
			if (ah2 != 0)
			{
				this.m_b = aq2.a(i.l(), ah2, this.m_f);
			}
			else
			{
				this.m_b = aq2.a(i.l());
			}
			if (this.m_g != null && this.m_c == OracleDbType.Clob && k != this.m_b.t().h().a())
			{
				Encoding srcEncoding;
				Encoding dstEncoding;
				if (k)
				{
					srcEncoding = Encoding.Unicode;
					dstEncoding = bl.a();
				}
				else
				{
					dstEncoding = Encoding.Unicode;
					srcEncoding = bl.a();
				}
				byte[] array = Encoding.Convert(srcEncoding, dstEncoding, this.m_g.GetBuffer(), 0, (int)this.m_g.Length);
				this.m_g = new MemoryStream();
				this.m_g.Write(array, 0, array.Length);
			}
			k = this.m_b.t().h().a();
			if (Cached)
			{
				WriteLobCache(clearLob: false);
			}
		}
	}

	protected virtual void WriteLobCache(bool clearLob)
	{
		if (clearLob)
		{
			this.m_b.a(0);
		}
		if (this.m_g != null && !h && Length != 0)
		{
			byte[] buffer = this.m_g.GetBuffer();
			this.m_b.b(0, buffer, 0, (int)this.m_g.Length);
			modifiedCache = false;
			this.m_g.Seek(this.m_d, SeekOrigin.Begin);
		}
	}

	private void a()
	{
		if (Cached && this.m_g == null)
		{
			ReadLobCache();
		}
	}

	protected void ClearCache()
	{
		this.m_d = 0;
		if (Cached && this.m_g != null)
		{
			this.m_g.SetLength(0L);
		}
	}

	protected virtual void ReadLobCache()
	{
		this.m_g = null;
		c();
		h = this.m_b == null;
		this.m_g = new MemoryStream();
		if (!h)
		{
			int num = this.m_b.f();
			if (num != 0)
			{
				byte[] array = ((this.m_c != OracleDbType.Clob && this.m_c != OracleDbType.NClob) ? new byte[num] : ((!this.m_b.t().h().a() && this.m_c != OracleDbType.NClob) ? new byte[num * 4] : new byte[num * 2]));
				int count = this.m_b.a(0, array, 0, array.Length);
				this.m_g.Write(array, 0, count);
				modifiedCache = false;
				this.m_g.Seek(this.m_d, SeekOrigin.Begin);
			}
		}
	}

	internal virtual void MakeDisconnected()
	{
		if (Cached && !IsTemporary)
		{
			a();
			if (this.m_b != null)
			{
				this.m_b.o();
				this.m_b = null;
			}
		}
		if (readLobMode != ReadLobMode.DefferedCachedDirect && readLobMode != ReadLobMode.Deferred)
		{
			readLobMode = ReadLobMode.DefferedCachedDirect;
		}
	}

	internal virtual void ClearLobLocator()
	{
		if (Cached && this.m_g != null && !IsTemporary && this.m_b != null)
		{
			this.m_b.o();
			this.m_b = null;
		}
	}

	int IComparable.CompareTo(object obj)
	{
		if (obj == null || obj == DBNull.Value)
		{
			if (!IsNull)
			{
				return 1;
			}
			return 0;
		}
		if (!(obj is OracleLob oracleLob))
		{
			throw new ArgumentException();
		}
		if (IsNull)
		{
			if (!oracleLob.IsNull)
			{
				return -1;
			}
			return 0;
		}
		if (oracleLob.IsNull)
		{
			return 1;
		}
		long length = Length;
		long length2 = oracleLob.Length;
		if (length > length2)
		{
			return 1;
		}
		if (length > length2)
		{
			return -1;
		}
		return 0;
	}
}
