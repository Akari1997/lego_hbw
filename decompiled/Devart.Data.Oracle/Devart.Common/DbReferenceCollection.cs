using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class DbReferenceCollection : IEnumerable
{
	internal struct a
	{
		private int m_a;

		private WeakReference m_b;

		public a(object A_0, int A_1)
		{
			this.m_a = A_1;
			this.m_b = null;
			a(A_0);
		}

		public bool a(int A_0, int A_1, object A_2, DbReferenceCollection A_3)
		{
			if (!c())
			{
				return true;
			}
			if (A_1 == 0 || A_1 == this.m_a)
			{
				object obj = a();
				if (obj != null && !A_3.NotifyItem(A_0, obj, this.m_a, A_2))
				{
					this.m_a = 0;
					this.m_b = null;
				}
			}
			return false;
		}

		[SpecialName]
		public bool c()
		{
			return this.m_b != null;
		}

		[SpecialName]
		public int b()
		{
			return this.m_a;
		}

		[SpecialName]
		public void a(int A_0)
		{
			this.m_a = A_0;
		}

		[SpecialName]
		public object a()
		{
			return Utils.GetWeakTarget(this.m_b);
		}

		[SpecialName]
		public void a(object A_0)
		{
			Utils.SetWeakTarget(ref this.m_b, A_0);
		}
	}

	private sealed class b : IEnumerator
	{
		private int m_a;

		private readonly List<a> m_b;

		internal b(List<a> A_0)
		{
			this.m_b = A_0;
			this.m_a = -1;
		}

		private bool b()
		{
			for (int num = this.m_a + 1; num < this.m_b.Count; num++)
			{
				this.m_a = num;
				if (this.m_b[num].c())
				{
					return true;
				}
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in b
			return this.b();
		}

		private void a()
		{
			this.m_a = -1;
		}

		void IEnumerator.Reset()
		{
			//ILSpy generated this explicit interface implementation from .override directive in a
			this.a();
		}

		[SpecialName]
		private object c()
		{
			return this.m_b[this.m_a].a();
		}

		object IEnumerator.get_Current()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c
			return this.c();
		}
	}

	internal const int a = 1;

	internal const int b = 2;

	private List<a> c;

	public DbReferenceCollection()
	{
		c = new List<a>(100);
	}

	public void Add(object value)
	{
		Add(value, 0);
	}

	public virtual void Add(object value, int tag)
	{
		AddItem(value, tag);
	}

	protected void AddItem(object value, int tag)
	{
		for (int num = 0; num < c.Count; num++)
		{
			if (c[num].a() == null)
			{
				c[num] = new a(value, tag);
				return;
			}
		}
		c.Add(new a(value, tag));
	}

	public IEnumerator GetEnumerator()
	{
		return new b(c);
	}

	public void Notify(int message, int tag, object connectionInternal)
	{
		for (int num = 0; num < c.Count; num++)
		{
			c[num].a(message, tag, connectionInternal, this);
		}
	}

	public void Purge()
	{
		c.Clear();
	}

	public virtual void Remove(object value)
	{
		RemoveItem(value);
	}

	protected void RemoveItem(object value)
	{
		for (int num = 0; num < c.Count; num++)
		{
			if (value == c[num].a())
			{
				c[num] = default(a);
				break;
			}
		}
	}

	protected virtual bool NotifyItem(int message, object value, int tag, object connectionInternal)
	{
		((IDisposable)value).Dispose();
		return false;
	}
}
