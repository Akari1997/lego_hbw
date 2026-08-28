using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Devart.Data.Oracle;

internal class bj : r, al
{
	private new static string m_a;

	public bj(string A_0, v A_1)
		: base(OracleType.a("SYS", "XMLTYPE", A_1), A_1, A_2: false)
	{
		a(A_0);
	}

	internal bj(HandleRef A_0, IntPtr A_1, v A_2, bool A_3)
		: base(OracleType.a("SYS", "XMLTYPE", A_2), A_2, A_2: false)
	{
		if (A_3)
		{
			f = new HandleRef(this, A_0.Handle);
		}
		else
		{
			f = A_0;
		}
		base.g = new HandleRef(A_0.Wrapper, A_1);
	}

	public new string b()
	{
		if (base.get_IsNull())
		{
			d();
			return "";
		}
		if (g().v().StartsWith("09.02.00.01") && a())
		{
			throw new OracleException(-1, bj.m_a);
		}
		StringBuilder stringBuilder = new StringBuilder();
		j().c(o().OCIDescriptorAlloc(j().h(), out var descpp, 74, 0u, 0u));
		HandleRef handleRef = new HandleRef(this, descpp);
		try
		{
			j().c(o().a(j().k(), handleRef, f, 0));
			long A_ = 65536L;
			byte[] array = new byte[A_];
			while (A_ > 0)
			{
				int num = o().a(j().k(), handleRef, array, ref A_, 0);
				if (num == 100)
				{
					A_ = 0L;
				}
				else
				{
					j().c(num);
				}
				if (A_ > 0)
				{
					string value = j().l().GetString(array, 0, (int)A_);
					stringBuilder.Append(value);
				}
			}
			j().c(o().a(j().k(), handleRef));
		}
		finally
		{
			j().c(o().OCIDescriptorFree(handleRef, 74));
		}
		d();
		return stringBuilder.ToString();
	}

	string al.a()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b();
	}

	private new void a(string A_0)
	{
		if (string.IsNullOrEmpty(A_0))
		{
			j().c(o().OCIObjectNew(j().h(), j().k(), g().r(), base.d.e, e.e, IntPtr.Zero, 10, 1, out var instance));
			f = new HandleRef(this, instance);
			j().c(o().OCIObjectGetInd(j().h(), j().k(), f, out var null_struct));
			base.g = new HandleRef(this, null_struct);
			base.set_IsNull(value: true);
			return;
		}
		if (A_0.Length >= 4000 || j().a())
		{
			byte[] bytes = j().o().GetBytes(A_0);
			p p2 = new p(g(), ah.b, A_2: false);
			p2.b(0, bytes, 0, bytes.Length);
			a(p2);
			p2.o();
			return;
		}
		j().c(o().OCIObjectNew(j().h(), j().k(), g().r(), 1, 0, 0, 10, 1, out var instance2));
		if (j().a())
		{
			byte[] bytes2 = Encoding.Unicode.GetBytes(A_0);
			j().c(o().OCIStringAssignText(j().h(), j().k(), bytes2, bytes2.Length, ref instance2));
		}
		else
		{
			j().c(o().OCIStringAssignText(j().h(), j().k(), A_0, A_0.Length, ref instance2));
		}
		j().c(o().OCIXMLTypeCreateFromSrc(g().r(), j().k(), 10, 1, instance2, IntPtr.Zero, out var retInstance));
		f = new HandleRef(this, retInstance);
	}

	private new void a(p A_0)
	{
		if (j().d() == 9020001 && (g().v().StartsWith("09.02.00.01") || g().v().StartsWith("10.01.00.01") || g().v().StartsWith("09.02.00.04")))
		{
			throw new OracleException(-1, bj.m_a);
		}
		j().c(o().OCIXMLTypeCreateFromSrc(g().r(), j().k(), 10, 2, A_0.q(), IntPtr.Zero, out var retInstance));
		f = new HandleRef(this, retInstance);
	}

	protected override void d()
	{
		HandleRef handleRef = f;
		if (handleRef.Handle != IntPtr.Zero)
		{
			base.d();
		}
	}

	public new al a(string A_0, string A_1)
	{
		if (string.IsNullOrEmpty(A_0))
		{
			throw new ArgumentNullException("xpathExpr");
		}
		int num = 0;
		IntPtr retDoc = IntPtr.Zero;
		f = r();
		try
		{
			num = o().OCIXMLTypeExtract(j().k(), r(), 10, A_0, A_0.Length, A_1, A_1.Length, out retDoc);
		}
		catch
		{
			num = -1;
		}
		finally
		{
			if (num != 0)
			{
				j().c(num);
			}
		}
		if (IntPtr.Zero != retDoc)
		{
			return new bj(new HandleRef(this, retDoc), IntPtr.Zero, g(), A_3: true);
		}
		return new bj("", g());
	}

	public new bool b(string A_0, string A_1)
	{
		if (string.IsNullOrEmpty(A_0))
		{
			throw new ArgumentNullException("xpathExpr");
		}
		int num = 0;
		int retval = 0;
		f = r();
		try
		{
			num = o().OCIXMLTypeExists(j().k(), f, A_0, A_0.Length, A_1, A_1.Length, out retval);
		}
		catch
		{
			num = -1;
		}
		finally
		{
			if (num != 0)
			{
				j().c(num);
			}
		}
		return retval != 0;
	}

	[SpecialName]
	private new bool a()
	{
		j().c(o().OCIXMLTypeIsSchemaBased(j().k(), f, out var retval));
		return retval != 0;
	}

	public new al a(al A_0)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException("xsldoc");
		}
		int num = 0;
		IntPtr retDoc = l();
		f = r();
		try
		{
			num = o().OCIXMLTypeTransform(j().k(), 10, f, ((bj)A_0).r(), out retDoc);
		}
		catch
		{
			num = -1;
		}
		finally
		{
			if (num != 0)
			{
				j().c(num);
			}
		}
		if (IntPtr.Zero != retDoc)
		{
			return new bj(new HandleRef(this, retDoc), IntPtr.Zero, g(), A_3: true);
		}
		return new bj("", g());
	}

	[SpecialName]
	private new g c()
	{
		return g();
	}

	g al.b()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		return this.c();
	}

	static bj()
	{
		bj.m_a = "Requested operation with XmlType is not supported on this Oracle client/server.";
	}
}
