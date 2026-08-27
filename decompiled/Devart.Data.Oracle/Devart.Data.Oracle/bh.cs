using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Devart.Common;

namespace Devart.Data.Oracle;

[DefaultMember("Item")]
internal class bh : r, bf
{
	private new Hashtable m_a = new Hashtable();

	public bh(string A_0, v A_1)
		: this(OracleType.a(A_0, A_1), A_1)
	{
	}

	public bh(OracleType A_0, v A_1)
		: base(A_0, A_1)
	{
	}

	internal bh(HandleRef A_0, IntPtr A_1, OracleType A_2, v A_3, bool A_4, bool A_5)
		: base(A_0, A_1, A_2, A_3, A_4, A_5)
	{
	}

	[SpecialName]
	public new object b(OracleAttribute A_0)
	{
		return Devart.Data.Oracle.r.a(d(A_0), A_0);
	}

	object bf.a(OracleAttribute A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b(A_0);
	}

	[SpecialName]
	public new void a(OracleAttribute A_0, object A_1)
	{
		a(A_1, A_0);
	}

	[SpecialName]
	public virtual bool a()
	{
		return base.get_IsNull();
	}

	bool w.get_IsNull()
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a();
	}

	[SpecialName]
	public virtual void a(bool A_0)
	{
		if (A_0 == base.get_IsNull())
		{
			return;
		}
		base.set_IsNull(A_0);
		if (A_0)
		{
			return;
		}
		foreach (OracleAttribute item in b())
		{
			a((object)null, item);
		}
		if (base.h != null)
		{
			base.h.a(A_0: false, this);
		}
	}

	void w.set_IsNull(bool A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0);
	}

	[SpecialName]
	internal new OracleAttributeCollection b()
	{
		return base.d.a;
	}

	public new object d(OracleAttribute A_0)
	{
		object obj = this.m_a[A_0];
		if (obj == null)
		{
			obj = a(A_0);
			if (A_0.h != null)
			{
				this.m_a.Add(A_0, obj);
			}
		}
		return obj;
	}

	object bf.b(OracleAttribute A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		return this.d(A_0);
	}

	public new object a(string A_0)
	{
		return d(b()[A_0]);
	}

	protected internal override void a(bool A_0, object A_1)
	{
		OracleAttribute oracleAttribute = null;
		foreach (DictionaryEntry item in this.m_a)
		{
			if (item.Value == A_1)
			{
				oracleAttribute = (OracleAttribute)item.Key;
				break;
			}
		}
		if (oracleAttribute == null)
		{
			throw new InvalidOperationException();
		}
		if (A_0)
		{
			Marshal.WriteInt16(k(), 2 + oracleAttribute.f, -1);
		}
		else
		{
			Marshal.WriteInt16(k(), 2 + oracleAttribute.f, 0);
		}
	}

	public new void c(OracleAttribute A_0)
	{
		byte[] names = j().o().GetBytes(A_0.Name);
		int[] lengths = new int[1] { names.Length };
		j().c(j().j().OCIObjectSetAttr(j().h(), j().k(), f, base.g, e.e, ref names, lengths, 1u, 0u, 0u, 0, IntPtr.Zero, IntPtr.Zero));
		IntPtr ptr = Devart.Common.z.a(k(), 2 + A_0.f);
		Marshal.WriteInt16(ptr, 0);
	}

	private new object a(OracleAttribute A_0)
	{
		short attr_null_status = -1;
		IntPtr attr_null_struct = IntPtr.Zero;
		if (get_IsNull())
		{
			return DBNull.Value;
		}
		IntPtr attr_value;
		if (j().d() >= 11000000 || !j().a())
		{
			byte[] names = j().o().GetBytes(A_0.Name);
			int[] lengths = new int[1] { names.Length };
			j().c(o().OCIObjectGetAttr(j().h(), j().k(), f, base.g, e.e, ref names, lengths, 1u, 0u, 0u, out attr_null_status, out attr_null_struct, out attr_value, out var _));
		}
		else
		{
			attr_value = Devart.Common.z.a(l(), A_0.j);
		}
		if (attr_null_struct == IntPtr.Zero)
		{
			attr_null_struct = Devart.Common.z.a(k(), 2 + A_0.f);
			attr_null_status = Marshal.ReadInt16(attr_null_struct);
		}
		bool flag = attr_null_status != 0 || get_IsNull();
		if (flag)
		{
			return DBNull.Value;
		}
		switch (A_0.b)
		{
		case 108:
			return new bh(new HandleRef(this, attr_value), attr_null_struct, A_0.h, base.c, flag, A_5: false);
		case 122:
		case 247:
			if (A_0.ObjectType.e == 247 || A_0.ObjectType.e == 248)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(attr_value);
				attr_value = intPtr;
			}
			if (A_0.g == OracleDbType.Table)
			{
				return new ar(new HandleRef(this, attr_value), attr_null_struct, A_0.h, base.c, flag, A_5: false);
			}
			return new m(new HandleRef(this, attr_value), attr_null_struct, A_0.h, base.c, flag, A_5: false);
		case 248:
			return new ar(new HandleRef(this, attr_value), attr_null_struct, A_0.h, base.c, flag, A_5: false);
		default:
			if (flag)
			{
				return DBNull.Value;
			}
			return base.a(A_0, attr_value);
		}
	}

	private new void a(object A_0, OracleAttribute A_1)
	{
		if (A_0 == null || A_0 == DBNull.Value)
		{
			Marshal.WriteInt16(k(), 2 + A_1.f, -1);
			if (A_1.h != null)
			{
				this.m_a.Remove(A_1);
			}
			return;
		}
		set_IsNull(value: false);
		Marshal.WriteInt16(k(), 2 + A_1.f, 0);
		byte[] names = j().o().GetBytes(A_1.Name);
		int[] lengths = new int[1] { names.Length };
		switch (A_1.b)
		{
		case 58:
		case 108:
		case 122:
		case 247:
		case 248:
		{
			if (A_1.h != null)
			{
				this.m_a.Remove(A_1);
			}
			if (!(A_0 is r))
			{
				throw new InvalidOperationException();
			}
			r r2 = (r)A_0;
			if (j().d() >= 11000000 || !j().a())
			{
				j().c(o().OCIObjectSetAttr(j().h(), j().k(), f, base.g, e.e, ref names, lengths, 1u, 0u, 0u, 0, r2.k(), r2.l()));
				break;
			}
			IntPtr intPtr3 = Devart.Common.z.a(l(), A_1.j);
			if (A_1.b == 108 && A_1.ObjectType.i)
			{
				IntPtr intPtr4 = r2.l();
				int num = A_1.k;
				int num2 = A_1.ObjectType.l;
				IntPtr intPtr5 = r2.k();
				IntPtr intPtr6 = Devart.Common.z.a(k(), 2 + A_1.f);
				if (IntPtr.Size == 4)
				{
					for (int num3 = 0; num3 < num; num3++)
					{
						Marshal.WriteByte(intPtr3, Marshal.ReadByte(intPtr4));
						intPtr3 = (IntPtr)((int)intPtr3 + 1);
						intPtr4 = (IntPtr)((int)intPtr4 + 1);
					}
					for (int num4 = 0; num4 < num2; num4++)
					{
						Marshal.WriteByte(intPtr6, Marshal.ReadByte(intPtr5));
						intPtr5 = (IntPtr)((int)intPtr5 + 1);
						intPtr6 = (IntPtr)((int)intPtr6 + 1);
					}
				}
				else
				{
					for (int num5 = 0; num5 < num; num5++)
					{
						Marshal.WriteByte(intPtr3, Marshal.ReadByte(intPtr4));
						intPtr3 = (IntPtr)((long)intPtr3 + 1);
						intPtr4 = (IntPtr)((long)intPtr4 + 1);
					}
					for (int num6 = 0; num6 < num2; num6++)
					{
						Marshal.WriteByte(intPtr6, Marshal.ReadByte(intPtr5));
						intPtr5 = (IntPtr)((long)intPtr5 + 1);
						intPtr6 = (IntPtr)((long)intPtr6 + 1);
					}
				}
			}
			else
			{
				Marshal.WriteIntPtr(intPtr3, r2.l());
			}
			break;
		}
		case 2:
		case 12:
		{
			IntPtr intPtr;
			if (j().d() >= 11000000 || !j().a())
			{
				intPtr = Marshal.AllocHGlobal(30);
				IntPtr intPtr2 = Marshal.AllocHGlobal(2);
				Marshal.WriteInt16(intPtr2, 0);
				short null_status = 0;
				try
				{
					a(A_0, A_1, intPtr);
					j().c(o().OCIObjectSetAttr(j().h(), j().k(), f, base.g, e.e, ref names, lengths, 1u, 0u, 0u, null_status, intPtr2, intPtr));
					break;
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr2);
					Marshal.FreeHGlobal(intPtr);
				}
			}
			intPtr = Devart.Common.z.a(l(), A_1.j);
			a(A_0, A_1, intPtr);
			break;
		}
		default:
		{
			IntPtr attr_value;
			if (j().d() >= 11000000 || !j().a())
			{
				j().c(o().OCIObjectGetAttr(j().h(), j().k(), f, base.g, e.e, ref names, lengths, 1u, 0u, 0u, out var _, out var _, out attr_value, out var _));
			}
			else
			{
				attr_value = Devart.Common.z.a(l(), A_1.j);
			}
			a(A_0, A_1, attr_value);
			break;
		}
		}
	}
}
