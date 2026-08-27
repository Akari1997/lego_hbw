using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Devart.Common;

namespace Devart.Data.Oracle;

[DefaultMember("Item")]
internal class m : r, ak
{
	private new EventHandlerList m_a;

	private new int m_b;

	public m(string A_0, v A_1)
		: this(OracleType.a(A_0, A_1), A_1)
	{
	}

	public m(OracleType A_0, v A_1)
		: base(A_0, A_1)
	{
		this.m_a = new EventHandlerList();
		this.m_b = -1;
	}

	internal m(HandleRef A_0, IntPtr A_1, OracleType A_2, v A_3, bool A_4, bool A_5)
		: base(A_0, A_1, A_2, A_3, A_4, A_5)
	{
		this.m_a = new EventHandlerList();
		this.m_b = -1;
	}

	public new object e(int A_0)
	{
		return c(A_0);
	}

	object ak.a(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in e
		return this.e(A_0);
	}

	protected new virtual object c(int A_0)
	{
		j().c(o().OCICollGetElem(j().h(), j().k(), base.f, A_0, out var exists, out var elem, out var elemind));
		bool flag = !exists || elemind == IntPtr.Zero || Marshal.ReadInt16(elemind) != 0 || get_IsNull();
		if (base.d.g.ObjectType != null)
		{
			switch (base.d.g.ObjectType.e)
			{
			case 108:
				return new bh(new HandleRef(this, elem), IntPtr.Zero, base.d.g.h, base.c, flag, A_5: false);
			case 122:
			case 247:
				return new m(new HandleRef(this, elem), IntPtr.Zero, base.d.g.h, base.c, flag, A_5: false);
			case 248:
				return new ar(new HandleRef(this, elem), IntPtr.Zero, base.d.g.h, base.c, flag, A_5: false);
			default:
				throw new InvalidOperationException();
			}
		}
		if (flag)
		{
			return DBNull.Value;
		}
		return a(base.d.g, elem);
	}

	protected new virtual void a(object A_0, int A_1)
	{
		switch (base.d.g.b)
		{
		case 58:
		case 108:
		case 122:
		case 247:
		case 248:
		{
			if (!(A_0 is r))
			{
				throw new InvalidOperationException();
			}
			r r2 = (r)A_0;
			j().c(o().OCICollAssignElem(j().h(), j().k(), A_1, r2.l(), r2.k(), base.f));
			return;
		}
		}
		j().c(o().OCICollGetElem(j().h(), j().k(), base.f, A_1, out var exists, out var elem, out var elemind));
		short num = (short)((elemind == IntPtr.Zero) ? (-1) : Marshal.ReadInt16(elemind));
		if (!exists || num != 0)
		{
			throw new InvalidOperationException();
		}
		if (A_0 == null || A_0 == DBNull.Value)
		{
			Marshal.WriteInt16(elemind, -1);
			return;
		}
		Marshal.WriteInt16(elemind, 0);
		a(A_0, base.d.g, elem);
	}

	public new int c(object A_0)
	{
		return e(A_0);
	}

	int IList.Add(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c
		return this.c(A_0);
	}

	protected new virtual int a()
	{
		return get_Count() - 1;
	}

	internal new int e(object A_0)
	{
		IntPtr elem;
		IntPtr elemind;
		if (A_0 is r)
		{
			elem = ((r)A_0).l();
			elemind = ((r)A_0).k();
		}
		else
		{
			elem = (elemind = IntPtr.Zero);
		}
		j().c(o().OCICollAppend(j().h(), j().k(), elem, elemind, base.f));
		this.m_b = -1;
		int num = a();
		if (!(A_0 is r))
		{
			a(A_0, num);
		}
		return num;
	}

	protected new virtual void a(int A_0)
	{
		throw new InvalidOperationException();
	}

	public new void b(object A_0)
	{
		d(a(A_0));
	}

	void IList.Remove(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0);
	}

	public new void d(int A_0)
	{
		a(A_0);
		this.m_b = -1;
	}

	void IList.RemoveAt(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		this.d(A_0);
	}

	public new void b()
	{
		j().c(o().OCICollTrim(j().h(), j().k(), get_Count(), base.f));
		this.m_b = 0;
	}

	void IList.Clear()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b();
	}

	public new bool d(object A_0)
	{
		for (int num = 0; num < get_Count(); num++)
		{
			if (b(num) == A_0)
			{
				return true;
			}
		}
		return false;
	}

	bool IList.Contains(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in d
		return this.d(A_0);
	}

	public new int a(object A_0)
	{
		for (int num = 0; num < get_Count(); num++)
		{
			if (b(num) == A_0)
			{
				return num;
			}
		}
		return -1;
	}

	int IList.IndexOf(object A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		return this.a(A_0);
	}

	public new void b(int A_0, object A_1)
	{
		throw new NotSupportedException(Devart.Common.al.a("OraArrayNotSupportAttributes"));
	}

	void IList.Insert(int A_0, object A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b(A_0, A_1);
	}

	public void CopyTo(Array array, int index)
	{
		for (int num = 0; num < get_Count(); num++)
		{
			array.SetValue(b(num), index + num);
		}
	}

	public IEnumerator GetEnumerator()
	{
		return new y(this);
	}

	[SpecialName]
	public bool get_IsSynchronized()
	{
		return false;
	}

	[SpecialName]
	public object get_SyncRoot()
	{
		return this;
	}

	[SpecialName]
	public new object b(int A_0)
	{
		return Devart.Data.Oracle.r.a(c(A_0), base.d.g);
	}

	object IList.get_Item(int A_0)
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b(A_0);
	}

	[SpecialName]
	public new void a(int A_0, object A_1)
	{
		a(A_1, A_0);
	}

	void IList.set_Item(int A_0, object A_1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in a
		this.a(A_0, A_1);
	}

	[SpecialName]
	public new bool f()
	{
		return false;
	}

	bool IList.get_IsFixedSize()
	{
		//ILSpy generated this explicit interface implementation from .override directive in f
		return this.f();
	}

	[SpecialName]
	public new bool e()
	{
		return false;
	}

	bool IList.get_IsReadOnly()
	{
		//ILSpy generated this explicit interface implementation from .override directive in e
		return this.e();
	}

	[SpecialName]
	public int get_Count()
	{
		if (this.m_b < 0)
		{
			j().c(o().OCITableSize(j().h(), j().k(), base.f, out this.m_b));
		}
		return this.m_b;
	}

	[SpecialName]
	public new int c()
	{
		return base.d.ArrayCapacity;
	}
}
