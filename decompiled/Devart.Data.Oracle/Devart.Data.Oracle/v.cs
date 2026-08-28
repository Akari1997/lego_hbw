using System;
using System.Collections;
using System.EnterpriseServices;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Transactions;
using Devart.Common;

namespace Devart.Data.Oracle;

internal sealed class v : g
{
	private new class a
	{
		public ay a;

		public g b;

		public Exception c;

		public a(ay A_0, g A_1)
		{
			a = A_0;
			b = A_1;
		}
	}

	private new readonly aa m_a;

	private new readonly Oci m_b;

	private new readonly HandleRef m_c;

	private new readonly HandleRef m_d;

	private new HandleRef m_e;

	private new HandleRef m_f;

	private new HandleRef m_g;

	private new bool m_h;

	private new bool m_i;

	private new string m_j;

	private new Devart.Common.z m_k;

	private new bool m_l;

	private new bool m_m;

	private new bool m_n;

	private new IntPtr m_o = IntPtr.Zero;

	private IntPtr m_p = IntPtr.Zero;

	private bool m_q;

	private bool m_r;

	private Delegate m_s;

	private new IntPtr t;

	private new GCHandle u;

	private new IntPtr m_v;

	private new WeakReference w;

	public v(aa A_0)
		: base(A_0)
	{
		this.m_a = A_0;
		this.m_c = A_0.h();
		this.m_d = A_0.k();
		this.m_b = A_0.j();
		this.m_l = !A_0.p();
	}

	public override void a(ay A_0, g A_1)
	{
		if (this.m_h)
		{
			return;
		}
		a a10 = new a(A_0, A_1);
		if (A_0.v() > 0)
		{
			Thread thread = new Thread((ParameterizedThreadStart)b);
			thread.Start(a10);
			if (!thread.Join(A_0.v() * 1000))
			{
				this.m_i = true;
				thread.Abort();
				throw new OracleException(0, Devart.Common.al.a("TimeoutError"));
			}
		}
		else
		{
			b(a10);
		}
		if (a10.c != null)
		{
			throw a10.c;
		}
		base.h = A_0;
	}

	protected void q()
	{
		try
		{
			if (this.m_h)
			{
				d();
			}
		}
		finally
		{
			base.Finalize();
		}
	}

	private void b(object A_0)
	{
		a a10 = (a)A_0;
		ay ay2 = a10.a;
		g g2 = a10.b;
		base.a(ay2, g2);
		this.m_m = ay2.g();
		this.m_n = false;
		v v2 = g2 as v;
		if (this.m_h)
		{
			return;
		}
		IntPtr hndlpp = IntPtr.Zero;
		IntPtr hndlpp2 = IntPtr.Zero;
		IntPtr hndlpp3 = IntPtr.Zero;
		try
		{
			try
			{
				if (g2 == null)
				{
					e(this.m_b.OCIHandleAlloc(this.m_c, out hndlpp, 8u, 0u, 0u));
					this.m_f = new HandleRef(this, hndlpp);
				}
				if (!this.m_m)
				{
					e(this.m_b.OCIHandleAlloc(this.m_c, out hndlpp2, 3u, 0u, 0u));
					this.m_e = new HandleRef(this, hndlpp2);
				}
				e(this.m_b.OCIHandleAlloc(this.m_c, out hndlpp3, 9u, 0u, 0u));
				this.m_g = new HandleRef(this, hndlpp3);
				Encoding encoding = this.m_a.o();
				if (!string.IsNullOrEmpty(ay2.t()) && this.m_a.d() >= 9000000)
				{
					byte[] bytes = encoding.GetBytes(ay2.t());
					e(this.m_b.OCIAttrSet(this.m_g, 9u, bytes, bytes.Length, 278u, this.m_d));
				}
				au au2 = null;
				this.m_l = this.m_l && !this.m_m;
				if (this.m_m)
				{
					au2 = this.m_a.a(ay2.ag(), ay2.q(), ay2.i(), ay2.z(), ay2.j(), ay2.d(), ay2.l(), ay2.m());
				}
				if (!this.m_m && g2 == null)
				{
					byte[] maxBytes = Utils.GetMaxBytes(encoding, ay2.ag(), out var byteCount);
					e(this.m_b.OCIServerAttach(this.m_f, this.m_d, maxBytes, byteCount, 0u));
				}
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					if (!this.m_m)
					{
						if (g2 == null)
						{
							e(this.m_b.OCIAttrSet(this.m_e, 3u, this.m_f.Handle, 0, 6u, this.m_d));
						}
						else
						{
							e(this.m_b.OCIAttrSet(this.m_e, 3u, (IntPtr)v2.m_f, 0, 6u, this.m_d));
						}
					}
					uint credt = 1u;
					int byteCount2;
					if (g2 != null)
					{
						if (intPtr == IntPtr.Zero)
						{
							intPtr = Marshal.AllocHGlobal(100);
						}
						byte[] maxBytes2 = Utils.GetMaxBytes(encoding, ay2.af(), out byteCount2);
						Marshal.Copy(maxBytes2, 0, intPtr, maxBytes2.Length);
						e(this.m_b.OCIAttrSet(this.m_g, 9u, intPtr, byteCount2, 22u, this.m_d));
						if (!Utils.IsEmpty(ay2.n()))
						{
							maxBytes2 = Utils.GetMaxBytes(encoding, ay2.n(), out byteCount2);
							Marshal.Copy(maxBytes2, 0, intPtr, maxBytes2.Length);
							e(this.m_b.OCIAttrSet(this.m_g, 9u, intPtr, byteCount2, 23u, this.m_d));
						}
						e(this.m_b.OCIAttrSet(this.m_g, 9u, (IntPtr)v2.m_g, 0, 99u, this.m_d));
						credt = 3u;
					}
					else if (!Utils.IsEmpty(ay2.af()) && ay2.af() != "/")
					{
						if (intPtr == IntPtr.Zero)
						{
							intPtr = Marshal.AllocHGlobal(100);
						}
						byte[] maxBytes3 = Utils.GetMaxBytes(encoding, ay2.af(), out byteCount2);
						Marshal.Copy(maxBytes3, 0, intPtr, maxBytes3.Length);
						e(this.m_b.OCIAttrSet(this.m_g, 9u, intPtr, byteCount2, 22u, this.m_d));
						byte[] maxBytes4 = Utils.GetMaxBytes(encoding, ay2.n(), out byteCount2);
						Marshal.Copy(maxBytes4, 0, intPtr, maxBytes4.Length);
						e(this.m_b.OCIAttrSet(this.m_g, 9u, intPtr, byteCount2, 23u, this.m_d));
					}
					else
					{
						credt = 2u;
					}
					if (this.m_m && ay2.h() != "" && this.m_a.d() >= 11000000)
					{
						if (intPtr == IntPtr.Zero)
						{
							intPtr = Marshal.AllocHGlobal(100);
						}
						byte[] maxBytes5 = Utils.GetMaxBytes(encoding, ay2.h(), out byteCount2);
						Marshal.Copy(maxBytes5, 0, intPtr, maxBytes5.Length);
						e(this.m_b.OCIAttrSet(this.m_g, 9u, intPtr, byteCount2, 425u, this.m_d));
					}
					if (this.m_f.Handle != IntPtr.Zero && (this.m_m || this.m_a.d() >= 8010704))
					{
						string strA = "";
						try
						{
							strA = v();
						}
						catch (OracleException)
						{
						}
						if ((ay2.Enlist || !ay2.TransactionScopeLocal) && (string.Compare(strA, "09") >= 0 || (string.Compare(strA, "08") >= 0 && ay2.Enlist)))
						{
							string text = ay2.ag();
							if (text.Length > 16)
							{
								text = text.Substring(0, 16);
							}
							byte[] bytes2 = encoding.GetBytes(text);
							int size = bytes2.Length;
							e(this.m_b.OCIAttrSet(this.m_f, 8u, bytes2, size, 25u, this.m_d));
							e(this.m_b.OCIAttrSet(this.m_f, 8u, bytes2, size, 26u, this.m_d));
						}
						this.m_j = null;
						base.b((string)null);
					}
					uint num = (uint)ay2.k();
					this.m_n = ay2.e() > 0 && !this.m_a.a() && this.m_a.d() >= 9020000;
					if (!this.m_m)
					{
						if (this.m_a.d() / 1000000 == 9 && string.Compare(OracleUtils.c(m()), "08") < 0)
						{
							throw new OracleException(3134, Devart.Common.al.a("ORA03134"));
						}
						if (this.m_n)
						{
							num |= 0x40;
						}
						try
						{
							e(this.m_b.OCISessionBegin(this.m_e, this.m_d, this.m_g, credt, num));
						}
						catch (OracleException ex2)
						{
							if (ex2.Code != 28002)
							{
								throw;
							}
							base.g = new OracleInfoMessageEventArgs(ex2.Message, ex2.Code, "Devart.Data.Oracle");
						}
						e(this.m_b.OCIAttrSet(this.m_e, 3u, this.m_g.Handle, 0, 7u, this.m_d));
						if (this.m_n)
						{
							int attributep = ay2.e();
							e(this.m_b.OCIAttrSet(this.m_e, 3u, ref attributep, 4, 176u, this.m_d));
						}
					}
					else
					{
						byte[] bytes3 = encoding.GetBytes(au2.b());
						int dbName_len = bytes3.Length;
						e(this.m_b.OCISessionGet(this.m_c, this.m_d, out hndlpp2, this.m_g, bytes3, (uint)dbName_len, null, 0u, out var _, out var _, out var _, (uint)(1 | ((g2 != null || ay2.q() != "") ? 8 : 0))));
						this.m_e = new HandleRef(this, hndlpp2);
					}
				}
				catch
				{
					if (!this.m_m)
					{
						this.m_b.OCIServerDetach(this.m_f, this.m_d, 0u);
					}
					throw;
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
				this.m_h = true;
			}
			catch (Exception ex3)
			{
				if (this.m_g.Handle != IntPtr.Zero)
				{
					this.m_b.OCIHandleFree(this.m_g, 9);
				}
				if (this.m_e.Handle != IntPtr.Zero)
				{
					this.m_b.OCIHandleFree(this.m_e, 3);
				}
				if (this.m_f.Handle != IntPtr.Zero)
				{
					this.m_b.OCIHandleFree(this.m_f, 8);
				}
				a10.c = ex3;
			}
		}
		catch (AccessViolationException a_)
		{
			a10.c = new OracleException(81, Devart.Common.al.a("InternalOracleClientError"), a_);
		}
		catch (ThreadAbortException)
		{
			d();
		}
	}

	public void d(int A_0)
	{
		if (A_0 != 0)
		{
			this.m_a.c(-1);
		}
	}

	public void e(int A_0)
	{
		if (A_0 != 0)
		{
			this.m_a.c(A_0);
		}
	}

	public override void c()
	{
		if (!this.m_i)
		{
			base.c();
		}
	}

	public override void d()
	{
		base.d();
		if (this.m_h)
		{
			this.m_j = "";
			try
			{
				try
				{
					if (!this.m_m)
					{
						e(this.m_b.OCISessionEnd(this.m_e, this.m_d, this.m_g, 0u));
						if (this.m_f.Handle != IntPtr.Zero)
						{
							e(this.m_b.OCIServerDetach(this.m_f, this.m_d, 0u));
						}
					}
					else
					{
						e(this.m_b.OCISessionRelease(this.m_e, this.m_d, null, 0u, 0u));
					}
				}
				catch (OracleException ex)
				{
					int code = ex.Code;
					if (code != 28 && code != 3114 && code != 3121)
					{
						throw;
					}
				}
				finally
				{
					this.m_h = false;
					if (!this.m_m)
					{
						this.m_b.OCIHandleFree(this.m_g, 9);
						this.m_b.OCIHandleFree(this.m_e, 3);
						if (this.m_f.Handle != IntPtr.Zero)
						{
							this.m_b.OCIHandleFree(this.m_f, 8);
						}
					}
					this.m_s = null;
					if (u.IsAllocated)
					{
						u.Free();
					}
					if (m_v != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(m_v);
					}
				}
			}
			catch (SEHException a_)
			{
				throw new OracleException(81, Devart.Common.al.a("InternalOracleClientError"), a_);
			}
		}
		if (this.m_l)
		{
			this.m_a.c();
			this.m_l = false;
		}
	}

	public override void e()
	{
		if (!this.m_r || base.h.w())
		{
			return;
		}
		int num = this.m_b.OCITransPrepare(this.m_e, this.m_d, 0u);
		if (num == 1)
		{
			this.m_a.a(out var A_);
			if (A_ == 24767)
			{
				this.m_q = true;
				return;
			}
		}
		e(num);
	}

	public override void l()
	{
		if (!this.m_r || (!this.m_q && !base.h.w()))
		{
			uint flags = (this.m_r ? 16777216u : 0u);
			e(this.m_b.OCITransCommit(this.m_e, this.m_d, flags));
		}
		if (this.m_r && !base.h.w())
		{
			e(this.m_b.OCIAttrSet(this.m_e, 3u, IntPtr.Zero, 0, 8u, this.m_d));
			if (this.m_o != IntPtr.Zero)
			{
				e(this.m_b.OCIHandleFree(this.m_o, 10));
				this.m_o = IntPtr.Zero;
			}
		}
		this.m_r = false;
	}

	public override void o()
	{
		if (!this.m_r || (!this.m_q && !base.h.w()))
		{
			e(this.m_b.OCITransRollback(this.m_e, this.m_d, 0u));
		}
		if (this.m_r && !base.h.w())
		{
			e(this.m_b.OCIAttrSet(this.m_e, 3u, IntPtr.Zero, 0, 8u, this.m_d));
			if (this.m_o != IntPtr.Zero)
			{
				e(this.m_b.OCIHandleFree(this.m_o, 10));
				this.m_o = IntPtr.Zero;
			}
		}
		this.m_r = false;
	}

	public void a(Transaction A_0)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		ITransaction lpTrans = ((A_0 == null) ? ((ITransaction)null) : ((ITransaction)TransactionInterop.GetDtcTransaction(A_0)));
		lock (this.m_a)
		{
			d(this.m_b.OraMTSEnlCtxGet(base.h.af(), base.h.n(), base.h.ag(), this.m_e, this.m_d, 0u, out this.m_p));
			d(this.m_b.OraMTSJoinTxn(this.m_p, lpTrans));
		}
	}

	public void s()
	{
		if (!(this.m_p == IntPtr.Zero))
		{
			lock (this.m_a)
			{
				d(this.m_b.OraMTSJoinTxn(this.m_p, null));
				d(this.m_b.OraMTSEnlCtxRel(this.m_p));
			}
			this.m_p = IntPtr.Zero;
		}
	}

	public override void a(Guid A_0, IsolationLevel A_1)
	{
		l();
		if (A_0 != Guid.Empty)
		{
			if (base.h.w())
			{
				a(Transaction.Current);
				this.m_q = false;
				this.m_r = true;
				return;
			}
			e(this.m_b.OCIHandleAlloc(this.m_c, out this.m_o, 10u, 0u, 0u));
			HandleRef trgthndlp = new HandleRef(this, this.m_o);
			byte[] array = Guid.NewGuid().ToByteArray();
			byte[] array2 = A_0.ToByteArray();
			byte[] array3 = new byte[140];
			Devart.Common.z.a(array3, 0, 1000);
			Devart.Common.z.a(array3, 4, array2.Length);
			Devart.Common.z.a(array3, 8, array.Length);
			Devart.Common.z z2 = new Devart.Common.z();
			try
			{
				Buffer.BlockCopy(array2, 0, array3, 12, array2.Length);
				Buffer.BlockCopy(array, 0, array3, 12 + array2.Length, array.Length);
				e(this.m_b.OCIAttrSet(trgthndlp, 10u, array3, array3.Length, 27u, this.m_d));
				e(this.m_b.OCIAttrSet(this.m_e, 3u, this.m_o, 0, 8u, this.m_d));
				uint num = 0u;
				num = 512u;
				num = num | 1 | 0x20000;
				e(this.m_b.OCITransStart(this.m_e, this.m_d, 120u, num));
				this.m_q = false;
				this.m_r = true;
				return;
			}
			finally
			{
				z2.a();
			}
		}
		string a_;
		switch (A_1)
		{
		case IsolationLevel.ReadCommitted:
			a_ = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";
			break;
		case IsolationLevel.Serializable:
		case IsolationLevel.Snapshot:
			a_ = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE";
			break;
		default:
			throw new ArgumentException(string.Format(Devart.Common.al.a("TransactionIsolationLevelNotSupported"), A_1.ToString()));
		}
		s s2 = h().a(this, A_1: false);
		try
		{
			s2.a(a_);
			s2.a(1, az.a);
		}
		finally
		{
			s2.l();
		}
	}

	public override void f()
	{
		e(this.m_b.OCIBreak(this.m_e, this.m_d));
	}

	public override void a(ay A_0, string A_1)
	{
		HandleRef handleRef;
		HandleRef handleRef2;
		HandleRef handleRef3;
		if (this.m_h)
		{
			handleRef = this.m_e;
			handleRef2 = this.m_f;
			handleRef3 = this.m_g;
		}
		else
		{
			IntPtr hndlpp = IntPtr.Zero;
			IntPtr hndlpp2 = IntPtr.Zero;
			IntPtr hndlpp3 = IntPtr.Zero;
			e(this.m_b.OCIHandleAlloc(this.m_c, out hndlpp, 8u, 0u, 0u));
			e(this.m_b.OCIHandleAlloc(this.m_c, out hndlpp2, 3u, 0u, 0u));
			e(this.m_b.OCIHandleAlloc(this.m_c, out hndlpp3, 9u, 0u, 0u));
			handleRef2 = new HandleRef(this, hndlpp);
			handleRef = new HandleRef(this, hndlpp2);
			handleRef3 = new HandleRef(this, hndlpp3);
		}
		try
		{
			Encoding encoding = this.m_a.o();
			if (!this.m_h)
			{
				byte[] bytes = encoding.GetBytes(A_0.ag());
				e(this.m_b.OCIServerAttach(handleRef2, this.m_d, bytes, bytes.Length, 0u));
			}
			try
			{
				if (!this.m_h)
				{
					e(this.m_b.OCIAttrSet(handleRef, 3u, handleRef2.Handle, 0, 6u, this.m_d));
					e(this.m_b.OCIAttrSet(handleRef, 3u, handleRef3.Handle, 0, 7u, this.m_d));
				}
				byte[] bytes2 = encoding.GetBytes(A_0.af());
				byte[] bytes3 = encoding.GetBytes(A_0.n());
				byte[] bytes4 = encoding.GetBytes(A_1);
				e(this.m_b.OCIPasswordChange(handleRef, this.m_d, bytes2, (uint)bytes2.Length, bytes3, (uint)bytes3.Length, bytes4, bytes4.Length, 8u));
				if (!this.m_h)
				{
					e(this.m_b.OCISessionEnd(handleRef, this.m_d, handleRef3, 0u));
				}
			}
			finally
			{
				if (!this.m_h)
				{
					e(this.m_b.OCIServerDetach(handleRef2, this.m_d, 0u));
				}
			}
		}
		finally
		{
			if (!this.m_h)
			{
				this.m_b.OCIHandleFree(handleRef3, 9);
				this.m_b.OCIHandleFree(handleRef2, 8);
				this.m_b.OCIHandleFree(handleRef, 3);
			}
		}
	}

	private static int a(IntPtr A_0, IntPtr A_1, IntPtr A_2, int A_3, int A_4)
	{
		v v2 = (v)((GCHandle)A_2).Target;
		if (v2.a(A_4 switch
		{
			2 => OracleFailoverState.Abort, 
			8 => OracleFailoverState.Begin, 
			1 => OracleFailoverState.End, 
			16 => OracleFailoverState.Error, 
			4 => OracleFailoverState.Reauth, 
			_ => OracleFailoverState.Unknown, 
		}, A_3 switch
		{
			1 => OracleFailoverType.None, 
			4 => OracleFailoverType.Select, 
			2 => OracleFailoverType.Session, 
			8 => OracleFailoverType.Transaction, 
			_ => OracleFailoverType.Unknown, 
		}))
		{
			return 25410;
		}
		return 0;
	}

	internal bool a(OracleFailoverState A_0, OracleFailoverType A_1)
	{
		return ((OracleConnection)Utils.GetWeakTarget(w))?.a(A_0, A_1) ?? false;
	}

	internal override void a(object A_0)
	{
		if ((object)this.m_s == null)
		{
			this.m_s = new Oci.b(a);
			if (this.m_k == null)
			{
				this.m_k = new Devart.Common.z();
			}
			t = this.m_k.a(this.m_s);
			Utils.SetWeakTarget(ref w, A_0);
			u = GCHandle.Alloc(this);
			bk.a a10 = new bk.a
			{
				a = t,
				b = u
			};
			m_v = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(bk.a)));
			Marshal.StructureToPtr((object)a10, m_v, true);
			e(this.m_b.OCIAttrSet(this.m_f, 8u, m_v, 0, 43u, this.m_d));
		}
	}

	public override h[] a(string A_0, int A_1)
	{
		if (string.Compare(v(), "08") < 0)
		{
			return base.a(A_0, A_1);
		}
		e(this.m_b.OCIHandleAlloc(this.m_c, out var hndlpp, 7u, 0u, 0u));
		HandleRef handleRef = new HandleRef(this, hndlpp);
		byte[] attributep = new byte[1] { 1 };
		e(this.m_b.OCIAttrSet(handleRef, 7u, attributep, 0, 250u, this.m_d));
		Encoding encoding = this.m_a.o();
		try
		{
			if (A_1 == 1)
			{
				A_1 = 0;
			}
			ushort num = 0;
			A_0 = OracleUtils.QuotedSQLName(A_0);
			IntPtr attributep2;
			IntPtr sizep;
			HandleRef handleRef2;
			byte b2;
			int num6;
			while (true)
			{
				int num2 = A_0.IndexOf('.');
				int num3 = 0;
				if (num2 > 0)
				{
					num3 = A_0.IndexOf('.', num2 + 1);
				}
				string text = ((num3 <= 0) ? A_0 : A_0.Substring(0, num3));
				byte[] bytes = encoding.GetBytes(text);
				int num4 = this.m_b.OCIDescribeAny(this.m_e, this.m_d, bytes, bytes.Length, 1, 0, 0, handleRef);
				if (num4 != 0 && num2 > 0 && num3 == -1)
				{
					text = A_0.Substring(0, num2);
					bytes = encoding.GetBytes(text);
					e(this.m_b.OCIDescribeAny(this.m_e, this.m_d, bytes, bytes.Length, 1, 0, 0, handleRef));
				}
				else
				{
					e(num4);
				}
				if (num3 == -1)
				{
					num3 = num2;
				}
				attributep2 = IntPtr.Zero;
				sizep = IntPtr.Zero;
				byte[] array = null;
				e(this.m_b.OCIAttrGet(handleRef, 7u, out IntPtr attributep3, out IntPtr sizep2, 124u, this.m_d));
				handleRef2 = new HandleRef(this, attributep3);
				e(this.m_b.OCIAttrGet(handleRef2, 53u, out IntPtr attributep4, out IntPtr _, 123u, this.m_d));
				b2 = (byte)(int)attributep4;
				int num5 = 0;
				num6 = num5;
				switch (b2)
				{
				case 5:
				case 6:
				{
					e(this.m_b.OCIAttrGet(handleRef, 7u, out attributep3, out sizep2, 124u, this.m_d));
					handleRef2 = new HandleRef(this, attributep3);
					IntPtr attributep5;
					IntPtr sizep4;
					if (b2 == 5)
					{
						e(this.m_b.OCIAttrGet(handleRef2, 53u, out attributep5, out sizep4, 109u, this.m_d));
						num5 = 0;
					}
					else
					{
						e(this.m_b.OCIAttrGet(handleRef2, 53u, out attributep5, out sizep4, 231u, this.m_d));
						num5 = 1;
					}
					HandleRef handleRef3 = new HandleRef(this, attributep5);
					e(this.m_b.OCIAttrGet(handleRef3, 53u, out IntPtr attributep6, out IntPtr _, 121u, this.m_d));
					text = A_0.Substring(num3 + 1, A_0.Length - num3 - 1);
					if (text.Length > 2 && text[0] == '"' && text[text.Length - 1] == '"')
					{
						text = text.Substring(1, text.Length - 2);
					}
					num6 = num5;
					num = 0;
					int num7 = (int)attributep6;
					while (num6 < num7 + num5)
					{
						e(this.m_b.OCIParamGet(handleRef3, 53u, this.m_d, out attributep3, (uint)num6));
						handleRef2 = new HandleRef(this, attributep3);
						try
						{
							e(this.m_b.OCIAttrGet(handleRef2, 53u, out attributep2, out sizep, 4u, this.m_d));
							byte[] array2 = new byte[(int)sizep];
							Marshal.Copy(attributep2, array2, 0, array2.Length);
							string strB = encoding.GetString(array2);
							if (string.Compare(text, strB, ignoreCase: true, CultureInfo.InvariantCulture) != 0)
							{
								goto IL_0381;
							}
							if (A_1 > 0)
							{
								num++;
							}
							if (A_1 != num)
							{
								goto IL_0381;
							}
							goto end_IL_0318;
							IL_0381:
							num6++;
							continue;
							end_IL_0318:;
						}
						finally
						{
							e(this.m_b.OCIDescriptorFree(handleRef2, 53));
						}
						break;
					}
					if (num6 == num7 + num5)
					{
						throw new OracleException(4043, string.Format(Devart.Common.al.a("ObjectDoesNotExist"), A_0));
					}
					break;
				}
				case 7:
					A_0 = ((num3 <= 0) ? string.Empty : A_0.Substring(num3, A_0.Length - num3));
					goto IL_03f3;
				default:
					throw new OracleException(4043, string.Format(Devart.Common.al.a("ObjectDoesNotExist"), A_0));
				case 3:
				case 4:
					break;
				}
				break;
				IL_03f3:
				e(this.m_b.OCIAttrGet(handleRef2, 53u, out IntPtr attributep7, out IntPtr sizep6, 4u, this.m_d));
				byte[] array3 = new byte[(int)sizep6];
				Marshal.Copy(attributep7, array3, 0, array3.Length);
				A_0 = encoding.GetString(array3) + A_0;
				e(this.m_b.OCIAttrGet(handleRef2, 53u, out IntPtr attributep8, out IntPtr sizep7, 9u, this.m_d));
				byte[] array4 = new byte[(int)sizep7];
				Marshal.Copy(attributep8, array4, 0, array4.Length);
				A_0 = encoding.GetString(array4) + '.' + A_0;
			}
			e(this.m_b.OCIAttrGet(handleRef2, 53u, out IntPtr attributep9, out IntPtr _, 108u, this.m_d));
			HandleRef handleRef4 = new HandleRef(this, attributep9);
			e(this.m_b.OCIAttrGet(handleRef4, 53u, out IntPtr attributep10, out IntPtr _, 121u, this.m_d));
			byte b3 = 0;
			IntPtr attributep11 = IntPtr.Zero;
			IntPtr sizep10 = IntPtr.Zero;
			IntPtr parmdpp = IntPtr.Zero;
			if (this.m_a.d() < 8010000)
			{
				if (b2 == 4 || (b2 == 5 && this.m_b.OCIParamGet(handleRef4, 53u, this.m_d, out parmdpp, 0u) == 0))
				{
					b3 = 3;
				}
			}
			else
			{
				e(this.m_b.OCIAttrGet(handleRef4, 53u, out attributep11, out sizep10, 128u, this.m_d));
				b3 = (byte)(int)attributep11;
			}
			ArrayList arrayList = new ArrayList();
			h h2 = default(h);
			int num8 = (int)attributep10;
			if (b3 == 3 || b3 == 8)
			{
				num6 = 0;
				if (b2 != 6)
				{
					num8--;
				}
			}
			else
			{
				num6 = 1;
			}
			IntPtr attributep12 = IntPtr.Zero;
			IntPtr sizep11 = IntPtr.Zero;
			HandleRef hndlp = new HandleRef(null, IntPtr.Zero);
			string a_ = "";
			int num9 = 0;
			while (num6 <= num8)
			{
				if (attributep12 == IntPtr.Zero)
				{
					e(this.m_b.OCIParamGet(handleRef4, 53u, this.m_d, out parmdpp, (uint)num6));
				}
				else
				{
					e(this.m_b.OCIParamGet(hndlp, 53u, this.m_d, out parmdpp, 1u));
				}
				HandleRef handleRef5 = new HandleRef(this, parmdpp);
				try
				{
					e(this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep13, out IntPtr _, 2u, this.m_d));
					if (num6 > 0)
					{
						e(this.m_b.OCIAttrGet(handleRef5, 53u, out int attributep14, out int _, 212u, this.m_d));
						h2.j = attributep14 != 0;
					}
					int num10 = (int)attributep13;
					if (num10 != 0 && A_1 == num)
					{
						if (attributep12 == IntPtr.Zero)
						{
							if (num6 > 0)
							{
								e(this.m_b.OCIAttrGet(handleRef5, 53u, out attributep2, out sizep, 4u, this.m_d));
							}
							e(this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep15, out IntPtr _, 213u, this.m_d));
							num9++;
							if (num6 > 0)
							{
								byte[] array = new byte[(int)sizep];
								Marshal.Copy(attributep2, array, 0, array.Length);
								a_ = (h2.a = encoding.GetString(array));
								h2.k = (int)attributep15;
							}
							else
							{
								a_ = (h2.a = ((!OracleUtils.OracleClientCompatible) ? "RESULT" : "RETURN_VALUE"));
								h2.k = 3;
							}
						}
						IntPtr attributep16 = IntPtr.Zero;
						IntPtr sizep15 = IntPtr.Zero;
						IntPtr attributep17 = IntPtr.Zero;
						IntPtr sizep16 = IntPtr.Zero;
						IntPtr attributep18 = IntPtr.Zero;
						IntPtr sizep17 = IntPtr.Zero;
						if (num10 != 108 && b2 != 6)
						{
							e(this.m_b.OCIAttrGet(handleRef5, 53u, out attributep16, out sizep15, 1u, this.m_d));
							e(this.m_b.OCIAttrGet(handleRef5, 53u, out attributep17, out sizep16, 6u, this.m_d));
							e(this.m_b.OCIAttrGet(handleRef5, 53u, out attributep18, out sizep17, 5u, this.m_d));
						}
						int num11 = (int)attributep16;
						h2.c = (short)num10;
						if (num11 > 0)
						{
							h2.d = num11;
						}
						switch (num10)
						{
						case 2:
							if ((int)attributep18 == 0)
							{
								h2.e = 38;
							}
							else
							{
								h2.e = (short)(int)attributep18;
							}
							h2.f = (sbyte)(int)attributep17;
							break;
						case 251:
							e(this.m_b.OCIAttrGet(handleRef5, 53u, out attributep12, out sizep11, 108u, this.m_d));
							h2.aa = 1;
							hndlp = new HandleRef(this, attributep12);
							goto end_IL_0636;
						case 250:
						{
							if (attributep12 != IntPtr.Zero)
							{
								throw new InvalidOperationException(Devart.Common.al.a("TableOfRecordIsNotSupported"));
							}
							e(this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep22, out IntPtr sizep21, 8u, this.m_d));
							if ((int)sizep21 > 0)
							{
								byte[] array = new byte[(int)sizep21];
								Marshal.Copy(attributep22, array, 0, array.Length);
								string text3 = encoding.GetString(array);
								e(this.m_b.OCIAttrGet(handleRef5, 53u, out attributep22, out sizep21, 10u, this.m_d));
								if ((int)sizep21 > 0)
								{
									array = new byte[(int)sizep21];
									Marshal.Copy(attributep22, array, 0, array.Length);
									string text4 = encoding.GetString(array);
									e(this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep23, out IntPtr _, 108u, this.m_d));
									HandleRef handleRef6 = new HandleRef(this, attributep23);
									e(this.m_b.OCIAttrGet(handleRef6, 53u, out IntPtr attributep24, out IntPtr _, 121u, this.m_d));
									int num12 = (int)attributep24;
									try
									{
										for (int num13 = 0; num13 < num12; num13++)
										{
											e(this.m_b.OCIParamGet(handleRef6, 53u, this.m_d, out var parmdpp2, (uint)(num13 + 1)));
											HandleRef trgthndlp = new HandleRef(this, parmdpp2);
											e(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep25, out IntPtr sizep24, 4u, this.m_d));
											string text5;
											if ((int)sizep24 > 0)
											{
												array = new byte[(int)sizep24];
												Marshal.Copy(attributep25, array, 0, array.Length);
												text5 = encoding.GetString(array);
											}
											else
											{
												text5 = "";
											}
											e(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep26, out IntPtr _, 2u, this.m_d));
											_ = IntPtr.Zero;
											_ = IntPtr.Zero;
											_ = IntPtr.Zero;
											_ = IntPtr.Zero;
											_ = IntPtr.Zero;
											_ = IntPtr.Zero;
											if (num10 != 108)
											{
												e(this.m_b.OCIAttrGet(trgthndlp, 53u, out attributep16, out sizep15, 1u, this.m_d));
												e(this.m_b.OCIAttrGet(trgthndlp, 53u, out attributep17, out sizep16, 6u, this.m_d));
												e(this.m_b.OCIAttrGet(trgthndlp, 53u, out attributep18, out sizep17, 5u, this.m_d));
											}
											h h3 = new h(a_);
											if ((short)(int)attributep26 == 108)
											{
												e(this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep27, out IntPtr sizep26, 8u, this.m_d));
												string name3 = encoding.GetString(Devart.Common.z.b(attributep27, (int)sizep26), 0, (int)sizep26);
												e(this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep28, out IntPtr sizep27, 9u, this.m_d));
												string name4 = encoding.GetString(Devart.Common.z.b(attributep28, (int)sizep27), 0, (int)sizep27);
												IntPtr sizep28 = IntPtr.Zero;
												this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep29, out sizep28, 10u, this.m_d);
												string text6 = "";
												if ((int)sizep28 > 0)
												{
													text6 = encoding.GetString(Devart.Common.z.b(attributep29, (int)sizep28), 0, (int)sizep28);
												}
												if (text6 != "")
												{
													h3.u = OracleUtils.QuoteIfNeed(name4) + "." + OracleUtils.QuoteIfNeed(name3) + "." + OracleUtils.QuoteIfNeed(text6);
												}
												else
												{
													h3.u = OracleUtils.QuoteIfNeed(name4) + "." + OracleUtils.QuoteIfNeed(name3);
												}
											}
											string text7 = h3.a + "$" + text5;
											if (text7.Length > 30)
											{
												text7 = "p" + num9 + "$" + text5;
											}
											h3.a = text7;
											h3.t = text3 + "." + text4;
											h3.c = (short)(int)attributep26;
											h3.d = (int)attributep16;
											h3.f = (sbyte)(int)attributep17;
											h3.e = (short)(int)attributep18;
											h3.k = h2.k;
											if (num13 < num12 - 1)
											{
												arrayList.Add(h3);
											}
											else
											{
												h2 = h3;
											}
										}
									}
									finally
									{
										e(this.m_b.OCIDescriptorFree(handleRef6, 53));
									}
									break;
								}
								throw new UnsupportedTypeException(Devart.Common.al.a("CannotReceiveRecordType"));
							}
							throw new UnsupportedTypeException(Devart.Common.al.a("CannotReceiveRecordType"));
						}
						case 108:
						{
							e(this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep19, out IntPtr sizep18, 8u, this.m_d));
							string name = encoding.GetString(Devart.Common.z.b(attributep19, (int)sizep18), 0, (int)sizep18);
							e(this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep20, out IntPtr sizep19, 9u, this.m_d));
							string name2 = encoding.GetString(Devart.Common.z.b(attributep20, (int)sizep19), 0, (int)sizep19);
							IntPtr sizep20 = IntPtr.Zero;
							this.m_b.OCIAttrGet(handleRef5, 53u, out IntPtr attributep21, out sizep20, 10u, this.m_d);
							string text2 = "";
							if ((int)sizep20 > 0)
							{
								text2 = encoding.GetString(Devart.Common.z.b(attributep21, (int)sizep20), 0, (int)sizep20);
							}
							if (text2 != "")
							{
								h2.u = OracleUtils.QuoteIfNeed(name2) + "." + OracleUtils.QuoteIfNeed(name) + "." + OracleUtils.QuoteIfNeed(text2);
							}
							else
							{
								h2.u = OracleUtils.QuoteIfNeed(name2) + "." + OracleUtils.QuoteIfNeed(name);
							}
							break;
						}
						}
					}
					arrayList.Add(h2);
					h2 = default(h);
					attributep12 = IntPtr.Zero;
					num6++;
					end_IL_0636:;
				}
				finally
				{
					e(this.m_b.OCIDescriptorFree(handleRef5, 53));
				}
			}
			object obj = arrayList.ToArray(typeof(h));
			return (h[])obj;
		}
		finally
		{
			e(this.m_b.OCIHandleFree(handleRef, 7));
		}
	}

	public override k a(int A_0, int A_1, Type A_2, OracleType A_3)
	{
		return new bg(this, A_0, A_1, A_2, A_3);
	}

	protected override object a(OracleType A_0)
	{
		return new am(A_0, this);
	}

	public override string a(ref string A_0)
	{
		return am.a(ref A_0, this);
	}

	[SpecialName]
	public new aa h()
	{
		return this.m_a;
	}

	[SpecialName]
	public override bool a()
	{
		return !this.m_h;
	}

	[SpecialName]
	public override int i()
	{
		return 0;
	}

	[SpecialName]
	public override void c(int A_0)
	{
	}

	[SpecialName]
	public override int k()
	{
		return 0;
	}

	[SpecialName]
	public override void b(int A_0)
	{
	}

	[SpecialName]
	public override int g()
	{
		return 0;
	}

	[SpecialName]
	public override void a(int A_0)
	{
	}

	[SpecialName]
	public override string m()
	{
		if (this.m_j != null && this.m_j != "")
		{
			return this.m_j;
		}
		byte[] array = new byte[1024];
		e(this.m_b.OCIServerVersion(this.m_e, this.m_d, array, array.Length, 3));
		int num = 0;
		Encoding encoding;
		if (this.m_a.a())
		{
			while (array[num] != 0 || array[++num] != 0)
			{
				num++;
			}
			encoding = Encoding.Unicode;
		}
		else
		{
			for (; array[num] != 0; num++)
			{
			}
			encoding = Encoding.Default;
		}
		return this.m_j = encoding.GetString(array, 0, num);
	}

	[SpecialName]
	public HandleRef r()
	{
		return this.m_e;
	}

	[SpecialName]
	public Oci p()
	{
		return this.m_b;
	}

	[SpecialName]
	public override bool b()
	{
		return false;
	}

	[SpecialName]
	public override bool j()
	{
		return this.m_n;
	}

	[SpecialName]
	public override byte[] n()
	{
		if (this.m_a.d() < 12010000 || string.Compare(v(), "12") < 0)
		{
			return null;
		}
		e(this.m_b.OCIAttrGet(this.m_g, 9u, out IntPtr attributep, out IntPtr sizep, 462u, this.m_d));
		int num = (int)sizep;
		if (num <= 0)
		{
			return null;
		}
		byte[] array = new byte[num];
		Marshal.Copy(attributep, array, 0, num);
		return array;
	}
}
