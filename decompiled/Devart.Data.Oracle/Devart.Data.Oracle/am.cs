using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class am
{
	private OracleType m_a;

	private readonly Oci m_b;

	private readonly aa c;

	internal readonly v d;

	internal HandleRef e;

	internal string f;

	internal string g;

	internal int h;

	internal am(OracleType A_0, v A_1)
	{
		this.m_a = A_0;
		g = A_0.d;
		A_0.k = 0;
		A_0.l = 2;
		if (g == "")
		{
			throw new ArgumentException(Devart.Common.al.a("TypeNameNotDef"));
		}
		f = A_0.c;
		d = A_1;
		c = A_1.h();
		this.m_b = c.j();
		Encoding encoding = c.o();
		byte[] bytes = encoding.GetBytes(g);
		byte[] array = ((!(f == "")) ? encoding.GetBytes(f) : null);
		c.c(this.m_b.OCITypeByName(c.h(), c.k(), A_1.r(), array, array.Length, bytes, bytes.Length, null, 0, 10, 0, out var tdo));
		e = new HandleRef(this, tdo);
		c.c(this.m_b.OCIHandleAlloc(c.h(), out var hndlpp, 7u, 0u, 0u));
		HandleRef handleRef = new HandleRef(this, hndlpp);
		try
		{
			OracleException ex = null;
			string text = "";
			IntPtr attributep;
			IntPtr sizep;
			HandleRef trgthndlp;
			for (int num = 0; num < 2; num++)
			{
				string text2 = g;
				if (f != "")
				{
					text2 = "\"" + f + "\".\"" + g + "\"";
				}
				if (text != "")
				{
					text2 = text2 + "@" + text;
				}
				bytes = encoding.GetBytes(text2);
				try
				{
					c.c(this.m_b.OCIDescribeAny(A_1.r(), c.k(), bytes, bytes.Length, 1, 0, 6, handleRef));
					ex = null;
				}
				catch (OracleException ex2)
				{
					if (ex2.Code == 4043)
					{
						ex = ex2;
						goto IL_021d;
					}
					throw;
				}
				break;
				IL_021d:
				if (ex != null && num == 0)
				{
					try
					{
						c.c(this.m_b.OCIDescribeAny(A_1.r(), c.k(), bytes, bytes.Length, 1, 0, 7, handleRef));
						c.c(this.m_b.OCIAttrGet(handleRef, 7u, out attributep, out sizep, 124u, c.k()));
						trgthndlp = new HandleRef(this, attributep);
						c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep2, out IntPtr sizep2, 9u, c.k()));
						if (attributep2 == IntPtr.Zero || (int)sizep2 == 0)
						{
							f = "";
						}
						else if (c.a())
						{
							f = Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2);
						}
						else
						{
							f = Marshal.PtrToStringAnsi(attributep2, (int)sizep2);
						}
						c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out attributep2, out sizep2, 4u, c.k()));
						if (attributep2 == IntPtr.Zero || (int)sizep2 == 0)
						{
							g = "";
						}
						else if (c.a())
						{
							g = Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2);
						}
						else
						{
							g = Marshal.PtrToStringAnsi(attributep2, (int)sizep2);
						}
						c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out attributep2, out sizep2, 111u, c.k()));
						text = ((!(attributep2 == IntPtr.Zero) && (int)sizep2 != 0) ? ((!c.a()) ? Marshal.PtrToStringAnsi(attributep2, (int)sizep2) : Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2)) : "");
					}
					catch
					{
						throw ex;
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			c.c(this.m_b.OCIAttrGet(handleRef, 7u, out attributep, out sizep, 124u, c.k()));
			trgthndlp = new HandleRef(this, attributep);
			if (f == "")
			{
				c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep3, out IntPtr sizep3, 9u, c.k()));
				if (c.a())
				{
					f = Marshal.PtrToStringUni(attributep3, (int)sizep3 / 2);
				}
				else
				{
					f = Marshal.PtrToStringAnsi(attributep3, (int)sizep3);
				}
				A_0.c = f;
			}
			c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep4, out IntPtr sizep4, 216u, c.k()));
			A_0.e = (ushort)(int)attributep4;
			h = A_0.e;
			if (c.d() >= 9010000)
			{
				c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep5, out IntPtr _, 279u, c.k()));
				A_0.i = (int)attributep5 != 0;
			}
			else
			{
				A_0.i = true;
			}
			OracleAttributeCollection oracleAttributeCollection = ((A_0.a.Count != 0) ? new OracleAttributeCollection() : A_0.a);
			OracleAttribute oracleAttribute = ((A_0.g != null) ? new OracleAttribute() : null);
			switch (A_0.e)
			{
			case 108:
			{
				c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep8, out IntPtr _, 228u, c.k()));
				c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep9, out IntPtr _, 229u, c.k()));
				int num2 = (int)attributep8;
				HandleRef hndlp = new HandleRef(this, attributep9);
				int num3 = 0;
				for (int num4 = 0; num4 < num2; num4++)
				{
					OracleAttribute oracleAttribute2 = new OracleAttribute();
					c.c(this.m_b.OCIParamGet(hndlp, 53u, c.k(), out var parmdpp, (uint)(num4 + 1)));
					HandleRef handleRef3 = new HandleRef(oracleAttribute2, parmdpp);
					try
					{
						oracleAttribute2.f = num3;
						a(handleRef3, oracleAttribute2);
						num3 = ((oracleAttribute2.h == null || oracleAttribute2.h.a == null || !oracleAttribute2.h.i || oracleAttribute2.DbType == OracleDbType.Ref) ? (num3 + 2) : (num3 + oracleAttribute2.h.l));
						oracleAttributeCollection.a(oracleAttribute2);
					}
					finally
					{
						c.c(c.j().OCIDescriptorFree(handleRef3, 53));
					}
				}
				A_0.l = (short)(2 + num3);
				break;
			}
			case 122:
			{
				if (oracleAttribute == null)
				{
					oracleAttribute = (A_0.g = new OracleAttribute());
				}
				c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out attributep4, out sizep4, 217u, c.k()));
				A_0.e = (ushort)(int)attributep4;
				h = A_0.e;
				c.c(this.m_b.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep6, out IntPtr _, 227u, c.k()));
				HandleRef handleRef2 = new HandleRef(this, attributep6);
				c.c(this.m_b.OCIAttrGet(handleRef2, 53u, out IntPtr attributep7, out IntPtr _, 234u, c.k()));
				A_0.f = (int)attributep7;
				a(handleRef2, oracleAttribute);
				break;
			}
			case 58:
				switch (g.ToUpper(CultureInfo.InvariantCulture))
				{
				default:
					throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
				case "XMLTYPE":
				case "ANYDATA":
					break;
				}
				break;
			default:
				throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
			}
			switch (A_0.e)
			{
			case 108:
				A_0.h = OracleDbType.Object;
				break;
			case 122:
			case 247:
				A_0.h = OracleDbType.Array;
				break;
			case 248:
				A_0.h = OracleDbType.Table;
				break;
			case 58:
				switch (g.ToUpper(CultureInfo.InvariantCulture))
				{
				case "XMLTYPE":
					A_0.h = OracleDbType.Xml;
					break;
				case "ANYDATA":
					A_0.h = OracleDbType.AnyData;
					break;
				default:
					throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
				}
				break;
			default:
				throw new OracleException(3115, Devart.Common.al.a("UnsupportedDataType"));
			}
		}
		finally
		{
			c.c(this.m_b.OCIHandleFree(handleRef, 7));
		}
	}

	internal static string a(ref string A_0, v A_1)
	{
		if (A_0 == "")
		{
			throw new ArgumentException(Devart.Common.al.a("TypeNameNotDef"));
		}
		aa aa2 = A_1.h();
		Oci oci = aa2.j();
		Encoding encoding = aa2.o();
		byte[] bytes = encoding.GetBytes(A_0);
		aa2.c(oci.OCITypeByName(aa2.h(), aa2.k(), A_1.r(), null, 0, bytes, bytes.Length, null, 0, 10, 0, out var _));
		aa2.c(oci.OCIHandleAlloc(aa2.h(), out var hndlpp, 7u, 0u, 0u));
		HandleRef handleRef = new HandleRef(A_1, hndlpp);
		string text = "";
		try
		{
			OracleException ex = null;
			string text2 = "";
			IntPtr attributep;
			IntPtr sizep;
			HandleRef trgthndlp;
			for (int num = 0; num < 2; num++)
			{
				string text3 = A_0;
				if (text != "")
				{
					text3 = "\"" + text + "\".\"" + A_0 + "\"";
				}
				if (text2 != "")
				{
					text3 = text3 + "@" + text2;
				}
				bytes = encoding.GetBytes(text3);
				Buffer.BlockCopy(bytes, 0, bytes, 0, bytes.Length);
				try
				{
					aa2.c(oci.OCIDescribeAny(A_1.r(), aa2.k(), bytes, bytes.Length, 1, 0, 6, handleRef));
					ex = null;
				}
				catch (OracleException ex2)
				{
					if (ex2.Code == 4043)
					{
						ex = ex2;
						goto IL_0169;
					}
					throw;
				}
				break;
				IL_0169:
				if (ex != null && num == 0)
				{
					try
					{
						aa2.c(oci.OCIDescribeAny(A_1.r(), aa2.k(), bytes, bytes.Length, 1, 0, 7, handleRef));
						aa2.c(oci.OCIAttrGet(handleRef, 7u, out attributep, out sizep, 124u, aa2.k()));
						trgthndlp = new HandleRef(A_1, attributep);
						aa2.c(oci.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep2, out IntPtr sizep2, 9u, aa2.k()));
						text = ((attributep2 == IntPtr.Zero || (int)sizep2 == 0) ? "" : ((!aa2.a()) ? Marshal.PtrToStringAnsi(attributep2, (int)sizep2) : Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2)));
						aa2.c(oci.OCIAttrGet(trgthndlp, 53u, out attributep2, out sizep2, 4u, aa2.k()));
						if (attributep2 == IntPtr.Zero || (int)sizep2 == 0)
						{
							A_0 = "";
						}
						else if (aa2.a())
						{
							A_0 = Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2);
						}
						else
						{
							A_0 = Marshal.PtrToStringAnsi(attributep2, (int)sizep2);
						}
						aa2.c(oci.OCIAttrGet(trgthndlp, 53u, out attributep2, out sizep2, 111u, aa2.k()));
						text2 = ((!(attributep2 == IntPtr.Zero) && (int)sizep2 != 0) ? ((!aa2.a()) ? Marshal.PtrToStringAnsi(attributep2, (int)sizep2) : Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2)) : "");
					}
					catch
					{
						throw ex;
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			aa2.c(oci.OCIAttrGet(handleRef, 7u, out attributep, out sizep, 124u, aa2.k()));
			trgthndlp = new HandleRef(A_1, attributep);
			if (text == "")
			{
				aa2.c(oci.OCIAttrGet(trgthndlp, 53u, out IntPtr attributep3, out IntPtr sizep3, 9u, aa2.k()));
				text = ((!aa2.a()) ? Marshal.PtrToStringAnsi(attributep3, (int)sizep3) : Marshal.PtrToStringUni(attributep3, (int)sizep3 / 2));
			}
			return text;
		}
		finally
		{
			aa2.c(oci.OCIHandleFree(handleRef, 7));
		}
	}

	internal void a(HandleRef A_0, OracleAttribute A_1)
	{
		if (this.m_b.OCIAttrGet(A_0, 53u, out IntPtr attributep, out IntPtr sizep, 4u, c.k()) == 0)
		{
			if (c.a())
			{
				A_1.a = Marshal.PtrToStringUni(attributep, (int)sizep / 2);
			}
			else
			{
				A_1.a = Marshal.PtrToStringAnsi(attributep, (int)sizep);
			}
		}
		else
		{
			A_1.a = string.Empty;
		}
		c.c(this.m_b.OCIAttrGet(A_0, 53u, out IntPtr attributep2, out IntPtr _, 1u, c.k()));
		A_1.c = (int)attributep2;
		c.c(this.m_b.OCIAttrGet(A_0, 53u, out IntPtr attributep3, out IntPtr _, 216u, c.k()));
		A_1.b = (int)attributep3;
		IntPtr attributep4 = IntPtr.Zero;
		switch (A_1.b)
		{
		case 1:
		case 5:
		case 11:
		case 96:
		case 97:
		case 104:
		case 112:
		{
			c.c(this.m_b.OCIAttrGet(A_0, 53u, out attributep4, out IntPtr _, 32u, c.k()));
			break;
		}
		}
		A_1.i = d.a(A_1.b, (int)attributep4, null);
		A_1.g = A_1.i.k();
		switch (A_1.b)
		{
		case 12:
			A_1.k = 8;
			break;
		case 2:
		case 3:
		case 4:
		case 6:
		case 21:
		case 22:
		case 246:
		{
			if (A_1.b == 22 || A_1.b == 21)
			{
				A_1.b = 2;
			}
			A_1.g = OracleDbType.Number;
			c.c(this.m_b.OCIAttrGet(A_0, 53u, out IntPtr attributep7, out IntPtr _, 5u, c.k()));
			A_1.d = (short)(int)attributep7;
			c.c(this.m_b.OCIAttrGet(A_0, 53u, out IntPtr attributep8, out IntPtr _, 6u, c.k()));
			A_1.e = (sbyte)(int)attributep8;
			A_1.k = 22;
			if (A_1.d == 0)
			{
				A_1.g = OracleDbType.Number;
				A_1.d = 38;
			}
			else if (A_1.d > 15)
			{
				A_1.g = OracleDbType.Number;
			}
			else if (A_1.d < 10 && A_1.e == 0)
			{
				A_1.g = OracleDbType.Integer;
			}
			else
			{
				A_1.g = OracleDbType.Double;
			}
			break;
		}
		case 58:
		case 108:
		case 110:
		case 122:
		{
			c.c(this.m_b.OCIAttrGet(A_0, 53u, out IntPtr attributep5, out IntPtr sizep5, 8u, c.k()));
			c.c(this.m_b.OCIAttrGet(A_0, 53u, out IntPtr attributep6, out IntPtr sizep6, 9u, c.k()));
			string a_;
			string a_2;
			if (c.a())
			{
				a_ = OracleUtils.a(attributep5, (int)sizep5 / 2);
				a_2 = OracleUtils.a(attributep6, (int)sizep6 / 2);
			}
			else
			{
				a_ = OracleUtils.b(attributep5, (int)sizep5);
				a_2 = OracleUtils.b(attributep6, (int)sizep6);
			}
			A_1.h = OracleType.a(a_2, a_, d);
			A_1.g = ((A_1.b == 110) ? OracleDbType.Ref : A_1.h.h);
			if (A_1.g == OracleDbType.Object && this.m_a.i)
			{
				A_1.k = A_1.h.k;
				if (A_1.h.m)
				{
					A_1.k = (short)((A_1.k + IntPtr.Size - 1) & ~(IntPtr.Size - 1));
				}
			}
			else
			{
				A_1.k = (short)IntPtr.Size;
			}
			break;
		}
		default:
			A_1.k = (short)IntPtr.Size;
			break;
		}
		switch (A_1.g)
		{
		case OracleDbType.Array:
		case OracleDbType.BFile:
		case OracleDbType.Blob:
		case OracleDbType.Char:
		case OracleDbType.Clob:
		case OracleDbType.IntervalDS:
		case OracleDbType.IntervalYM:
		case OracleDbType.NChar:
		case OracleDbType.NClob:
		case OracleDbType.NVarChar:
		case OracleDbType.Ref:
		case OracleDbType.Table:
		case OracleDbType.TimeStamp:
		case OracleDbType.TimeStampLTZ:
		case OracleDbType.TimeStampTZ:
		case OracleDbType.VarChar:
			this.m_a.k = (short)((this.m_a.k + IntPtr.Size - 1) & ~(IntPtr.Size - 1));
			this.m_a.m = true;
			break;
		}
		if (A_1.g == OracleDbType.Object && A_1.h.m)
		{
			this.m_a.k = (short)((this.m_a.k + IntPtr.Size - 1) & ~(IntPtr.Size - 1));
			this.m_a.m = true;
		}
		A_1.j = this.m_a.k;
		this.m_a.k += A_1.k;
	}

	public virtual bool a(object A_0)
	{
		am am2 = (am)A_0;
		HandleRef handleRef = am2.e;
		if (!(e.Handle == handleRef.Handle))
		{
			return b() == am2.b();
		}
		return true;
	}

	public virtual int a()
	{
		int num = (int)e.Handle;
		return num | b().GetHashCode();
	}

	[SpecialName]
	public string b()
	{
		return f + ((f == string.Empty) ? string.Empty : ".") + g;
	}
}
