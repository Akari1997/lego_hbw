using System;
using Devart.Common;

namespace Devart.Data.Oracle;

public sealed class OracleBFile : OracleLob
{
	private string a;

	private string b;

	public new static readonly OracleBFile Null = new OracleBFile();

	public string DirectoryName
	{
		get
		{
			if (base.IsNull)
			{
				return string.Empty;
			}
			if (b == null)
			{
				base.LobLocator.a(out b, out a);
			}
			return b;
		}
	}

	public bool FileExists => base.LobLocator.j();

	public string FileName
	{
		get
		{
			if (base.IsNull)
			{
				return string.Empty;
			}
			if (a == null)
			{
				base.LobLocator.a(out b, out a);
			}
			return a;
		}
	}

	public bool IsOpen
	{
		get
		{
			if (base.LobLocator == null)
			{
				return false;
			}
			return base.LobLocator.h();
		}
	}

	public override bool CanWrite => false;

	public OracleBFile()
		: base(OracleDbType.BFile)
	{
	}

	public OracleBFile(string directory, string fileName)
		: this()
	{
		SetFileName(directory, fileName);
	}

	public OracleBFile(OracleConnection connection)
		: base(connection, OracleDbType.BFile)
	{
	}

	public OracleBFile(OracleConnection connection, string directory, string fileName)
		: this(connection)
	{
		SetFileName(directory, fileName);
	}

	internal OracleBFile(OracleConnection A_0, OracleDataReader A_1, int A_2, a3 A_3)
		: base(A_0, A_1, A_2, A_3, OracleDbType.BFile)
	{
	}

	private OracleBFile(OracleBFile A_0)
		: base(A_0)
	{
		a = A_0.a;
		b = A_0.b;
	}

	public override object Clone()
	{
		return new OracleBFile(this);
	}

	public override void Close()
	{
		if (IsOpen)
		{
			CloseFile();
		}
		base.Close();
	}

	public void CloseFile()
	{
		if (IsOpen)
		{
			base.LobLocator.n();
		}
	}

	public new long CopyTo(OracleLob dest)
	{
		return CopyTo(dest, 0L);
	}

	public new long CopyTo(OracleLob dest, long destOffset)
	{
		return CopyTo(0L, dest, destOffset, Length);
	}

	public new long CopyTo(long srcOffset, OracleLob dest, long destOffset, long amount)
	{
		Utils.CheckArgumentNull(dest, "dest");
		if (base.LobLocator != null)
		{
			if (dest.IsNull)
			{
				throw new InvalidOperationException(Devart.Common.al.a("CanNotCopyToNULLBFILE"));
			}
			return dest.LobLocator.b(base.LobLocator, (int)srcOffset, (int)destOffset, (int)amount);
		}
		return 0L;
	}

	public void OpenFile()
	{
		if (base.IsNull)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotOpenNULLLOB"));
		}
		base.LobLocator.g();
	}

	public void SetFileName(string directory, string fileName)
	{
		if (b == directory && a == fileName)
		{
			return;
		}
		if (base.IsNull)
		{
			throw new InvalidOperationException(Devart.Common.al.a("CanNotModifyNULLBFILE"));
		}
		if (base.Cached)
		{
			modifiedCache = true;
		}
		else
		{
			if (IsOpen)
			{
				CloseFile();
			}
			base.LobLocator.a(directory, fileName);
		}
		b = directory;
		a = fileName;
		ClearCache();
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (!base.Cached && !IsOpen)
		{
			if (!OracleUtils.OracleClientCompatible)
			{
				throw new InvalidOperationException(Devart.Common.al.a("CanNotReadClosedBFILE"));
			}
			OpenFile();
		}
		return base.Read(buffer, offset, count);
	}

	protected override void ReadLobCache()
	{
		bool isOpen = IsOpen;
		if (!isOpen)
		{
			OpenFile();
		}
		try
		{
			base.ReadLobCache();
			if (base.LobLocatorInternal == null)
			{
				b = DirectoryName;
				a = FileName;
			}
			else
			{
				((a3)base.LobLocatorInternal).a(out b, out a);
			}
		}
		finally
		{
			if (!isOpen)
			{
				CloseFile();
			}
		}
	}

	protected override void WriteLobCache(bool clearLob)
	{
		if (!base.IsNull)
		{
			if (b != null && a != null)
			{
				((a3)base.LobLocatorInternal).a(b, a);
			}
			modifiedCache = false;
		}
	}

	internal override void ClearLobLocator()
	{
		base.ClearLobLocator();
		ClearCache();
	}
}
