using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal sealed class aa : aq
{
	internal new const string a = "bin\\oci.dll";

	internal new const string b = "oci.dll";

	private new readonly Oci m_c;

	private new readonly HandleRef m_d;

	private HandleRef m_e;

	private int m_f;

	private readonly bool m_g;

	private Hashtable m_h;

	private Hashtable m_i;

	private static Hashtable[] m_j;

	private static aa m_k;

	private new static bool l;

	public aa(bool A_0, bool A_1, OracleHome A_2)
		: base(A_1)
	{
		if (A_2 == null)
		{
			throw new ArgumentNullException("home");
		}
		this.m_c = A_2.b();
		this.m_f = A_2.ClientVersionNumber;
		IntPtr envhpp = IntPtr.Zero;
		IntPtr hndlpp = IntPtr.Zero;
		try
		{
			int num = 2;
			if (A_1 && (d() >= 9000000 || d() < 8010000))
			{
				num |= 1;
			}
			if (d() < 8010000)
			{
				c(this.m_c.OCIInitialize(num, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
				c(this.m_c.OCIEnvInit(out envhpp, 0u, 0u, 0u));
				c(this.m_c.OCIHandleAlloc(envhpp, out hndlpp, 2u, 0u, 0u));
			}
			else
			{
				if (d() >= 9000000 && A_0)
				{
					num |= 0x4000;
					this.m_g = true;
				}
				if (this.m_c.OCIEnvCreate(out envhpp, num, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0u, 0u) != 0)
				{
					throw new OracleException(-1, Devart.Common.al.a("CanNotLoadOracleClient"));
				}
				c(this.m_c.OCIHandleAlloc(envhpp, out hndlpp, 2u, 0u, 0u));
			}
			this.m_d = new HandleRef(this, envhpp);
			this.m_e = new HandleRef(this, hndlpp);
		}
		catch (BadImageFormatException a_)
		{
			string arg = ((IntPtr.Size == 8) ? "64x" : "32x");
			string format = "Unable to load {0}. Please check that you use {1} version of Oracle client with {1} application.";
			throw new OracleProviderException(string.Format(format, this.m_c.ociDllPath, arg), a_);
		}
		catch (Exception ex)
		{
			if (hndlpp != IntPtr.Zero)
			{
				this.m_c.OCIHandleFree(hndlpp, 2);
			}
			if (envhpp != IntPtr.Zero)
			{
				this.m_c.OCIHandleFree(envhpp, 1);
			}
			if (ex is AccessViolationException a_2)
			{
				throw new OracleException(81, Devart.Common.al.a("InternalOracleClientError"), a_2);
			}
			throw;
		}
	}

	public static aa a(bool A_0, bool A_1, OracleHome A_2, bool A_3)
	{
		if (A_2 == null)
		{
			throw new ArgumentNullException("home");
		}
		string clientVersion = A_2.ClientVersion;
		int pos = 0;
		int num = Utils.TryParseInt(clientVersion, ref pos);
		pos++;
		int num2 = Utils.TryParseInt(clientVersion, ref pos);
		if (A_3 || num < 8 || (num == 8 && num2 < 1))
		{
			aa aa2 = new aa(A_0, A_1, A_2);
			aa2.a(A_0: false);
			return aa2;
		}
		int num3 = (A_0 ? 1 : 0) + (A_1 ? 2 : 0);
		aa aa3;
		if (aa.m_j[num3] == null)
		{
			aa.m_j[num3] = new Hashtable();
			aa3 = null;
		}
		else
		{
			aa3 = (aa)aa.m_j[num3][A_2];
		}
		if (aa3 == null)
		{
			aa3 = new aa(A_0, A_1, A_2);
			aa.m_j[num3][A_2] = aa3;
		}
		return aa3;
	}

	public override void c()
	{
		if (this.m_i != null)
		{
			foreach (object item in this.m_i)
			{
				((au)item).a();
			}
			this.m_i = null;
		}
		if (this.m_e.Handle != IntPtr.Zero)
		{
			this.m_c.OCIHandleFree(this.m_e, 2);
		}
		HandleRef handleRef = this.m_d;
		if (handleRef.Handle != IntPtr.Zero)
		{
			this.m_c.OCIHandleFree(this.m_d, 1);
		}
	}

	public override g b()
	{
		return new v(this);
	}

	public override s a(g A_0, bool A_1)
	{
		return new a5((v)A_0, A_1);
	}

	public override a3 a(g A_0, ah A_1, bool A_2)
	{
		return new p((v)A_0, A_1, A_2);
	}

	public override a3 a(g A_0)
	{
		return new p((v)A_0, 56);
	}

	public override w a(g A_0, OracleType A_1)
	{
		return A_1.DbType switch
		{
			OracleDbType.Object => new bh(A_1, (v)A_0), 
			OracleDbType.Array => new m(A_1, (v)A_0), 
			OracleDbType.Table => new ar(A_1, (v)A_0), 
			_ => throw new InvalidOperationException(), 
		};
	}

	public override al a(g A_0, string A_1)
	{
		return new bj(A_1, (v)A_0);
	}

	public override b a(g A_0, OracleDbType A_1, object A_2, OracleConnection A_3)
	{
		return new a2((v)A_0, A_1, A_2, A_3);
	}

	public override f a(g A_0, OracleType A_1, byte[] A_2)
	{
		return new l(A_2, A_1, (v)A_0);
	}

	public override int a(int A_0)
	{
		switch (A_0)
		{
		case 8:
		case 24:
		case 112:
		case 113:
		case 114:
		case 115:
		case 116:
		case 187:
		case 188:
		case 189:
		case 190:
		case 232:
			return IntPtr.Size;
		default:
			return 0;
		}
	}

	public void c(int A_0)
	{
		int A_1;
		switch (A_0)
		{
		case -2:
			throw new OracleException(0, Devart.Common.al.a("OciInvalidHandle"));
		case 1:
		{
			string text = a(out A_1);
			if (A_1 != 24344 && A_1 != 24381 && A_1 != 28002)
			{
				break;
			}
			throw new OracleException(A_1, text);
		}
		case -1:
		{
			string text = a(out A_1, out var A_2);
			throw new OracleException(A_1, text, A_2);
		}
		case 100:
		{
			string text = a(out A_1);
			if (!string.IsNullOrEmpty(text))
			{
				throw new OracleException(A_1, text);
			}
			throw new OracleException(1403, Devart.Common.al.a("OciNoData"));
		}
		}
	}

	internal string i()
	{
		int A_;
		return a(out A_);
	}

	internal string a(out int A_0)
	{
		bool A_1;
		return a(out A_0, this.m_e, A_2: true, out A_1, A_4: false);
	}

	internal string a(out int A_0, out bool A_1)
	{
		return a(out A_0, this.m_e, A_2: true, out A_1, A_4: true);
	}

	internal string a(out int A_0, HandleRef A_1, out bool A_2)
	{
		return a(out A_0, A_1, A_2: false, out A_2, A_4: true);
	}

	private string a(out int A_0, HandleRef A_1, bool A_2, out bool A_3, bool A_4)
	{
		A_3 = false;
		if (A_2 && A_1.Handle == IntPtr.Zero)
		{
			A_0 = 0;
			return Devart.Common.al.a("OciNotInitialized");
		}
		int num = 1024;
		if (this.m_g)
		{
			num *= 2;
		}
		byte[] array = new byte[num];
		this.m_c.OCIErrorGet(A_1, 1, null, out A_0, array, array.Length, 2);
		string result;
		if (this.m_g)
		{
			int num2;
			for (num2 = 0; num2 + 1 < array.Length && (array[num2] != 0 || array[num2 + 1] != 0); num2 += 2)
			{
			}
			num2 -= 2;
			while (num2 > 0 && (array[num2] == 10 || array[num2] == 13) && array[num2 + 1] == 0)
			{
				array[num2] = 0;
				array[num2 + 1] = 0;
				num2 -= 2;
			}
			result = Devart.Common.v.a(array, 0, -1, Encoding.Unicode);
		}
		else
		{
			int num3;
			for (num3 = 0; num3 < array.Length && array[num3] != 0; num3++)
			{
			}
			num3--;
			while (num3 > 0 && (array[num3] == 10 || array[num3] == 13))
			{
				array[num3] = 0;
				num3--;
			}
			result = Devart.Common.v.a(array, 0, -1, Encoding.Default);
		}
		if (d() >= 12010000)
		{
			this.m_c.OCIAttrGet(A_1, 2u, out IntPtr attributep, out IntPtr _, 472u, A_1);
			int num4 = (int)attributep;
			A_3 = num4 != 0;
		}
		return result;
	}

	public at a(int A_0, am A_1)
	{
		at at2 = null;
		switch (A_0)
		{
		case 58:
		case 108:
		case 122:
		case 247:
		case 248:
			return new a4(this, A_1);
		case 110:
			return new a7(this, A_1);
		default:
			return b(A_0);
		}
	}

	public at b(int A_0)
	{
		at at2 = null;
		if (this.m_h == null)
		{
			this.m_h = new Hashtable();
		}
		else
		{
			at2 = (at)this.m_h[A_0];
		}
		if (at2 == null)
		{
			at2 = A_0 switch
			{
				4 => new c(this), 
				50 => new n(this), 
				_ => new ag(this, A_0), 
			};
			this.m_h[A_0] = at2;
		}
		return at2;
	}

	[SpecialName]
	public static aa g()
	{
		aa aa2 = e();
		if (aa2 != null)
		{
			return aa2;
		}
		throw new InvalidOperationException(Devart.Common.al.a("YouCallMethodWhenOCILoaded"));
	}

	[SpecialName]
	public static bool f()
	{
		return e() != null;
	}

	private static aa e()
	{
		Hashtable[] array = aa.m_j;
		foreach (Hashtable hashtable in array)
		{
			if (hashtable == null)
			{
				continue;
			}
			{
				IEnumerator enumerator = hashtable.Values.GetEnumerator();
				try
				{
					if (enumerator.MoveNext())
					{
						return (aa)enumerator.Current;
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		if (aa.m_k == null && !l)
		{
			try
			{
				OracleHome defaultHome = OracleHomeCollection.SingletonInstance.DefaultHome;
				if (defaultHome != null)
				{
					aa.m_k = a(A_0: false, A_1: false, defaultHome, A_3: true);
				}
			}
			catch (Exception)
			{
				l = true;
			}
		}
		return aa.m_k;
	}

	public au a(string A_0, string A_1, string A_2, int A_3, int A_4, int A_5, int A_6, bool A_7)
	{
		if (this.m_i == null)
		{
			this.m_i = new Hashtable();
		}
		string text = A_0.ToUpper() + "|" + A_3 + "|" + A_4 + "|" + A_5 + "|" + A_6 + "|" + A_7;
		if (A_1 != null && A_1 != "")
		{
			text = text + "|" + A_1.ToUpper();
		}
		au au2 = this.m_i[text] as au;
		if (au2 == null)
		{
			au2 = new au(this, A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
			this.m_i[text] = au2;
		}
		return au2;
	}

	[SpecialName]
	public override int d()
	{
		return this.m_f;
	}

	[SpecialName]
	public override bool a()
	{
		return this.m_g;
	}

	[SpecialName]
	public Oci j()
	{
		return this.m_c;
	}

	[SpecialName]
	public HandleRef h()
	{
		return this.m_d;
	}

	[SpecialName]
	public HandleRef k()
	{
		return this.m_e;
	}

	static aa()
	{
		aa.m_j = new Hashtable[4];
		aa.m_k = null;
		l = false;
	}
}
