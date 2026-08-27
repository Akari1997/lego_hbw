using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Devart.DbMonitor;

namespace Devart.Common;

internal class c : IDisposable
{
	private class a
	{
		public readonly Devart.DbMonitor.c a;

		public readonly MonitorTracePoint b;

		public readonly bool c;

		public a(Devart.DbMonitor.c A_0, MonitorTracePoint A_1, bool A_2)
		{
			this.a = A_0;
			b = A_1;
			c = A_2;
		}

		public virtual string a()
		{
			return string.Format("{0} {1} {2} IsParent:{3}", new object[4]
			{
				this.a.i(),
				this.a.g(),
				this.a.q(),
				c
			});
		}
	}

	private Devart.DbMonitor.n m_a;

	private Thread m_b;

	private AutoResetEvent m_c = new AutoResetEvent(initialState: true);

	private LinkedList<a> m_d = new LinkedList<a>();

	private bool m_e;

	private object m_f = new object();

	private Stack<int> m_g = new Stack<int>(10);

	private string h = "localhost";

	private int i = 1000;

	private int j = 1000;

	private int k;

	private string l;

	public void b()
	{
		this.m_e = true;
		this.m_c.Set();
		if (this.m_b != null)
		{
			this.m_b.Join();
			this.m_b = null;
			this.m_d.Clear();
		}
		if (this.m_a != null)
		{
			try
			{
				this.m_a.f();
				this.m_a.d();
			}
			catch
			{
			}
			this.m_a = null;
		}
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		this.b();
	}

	public void a(Devart.DbMonitor.c A_0, MonitorTracePoint A_1, bool A_2)
	{
		a value = new a(A_0, A_1, A_2);
		lock (this.m_f)
		{
			if (j > 0 && this.m_d.Count > j)
			{
				k++;
			}
			else
			{
				if (k > 0)
				{
					Devart.DbMonitor.c c2 = new Devart.DbMonitor.c();
					c2.a(Devart.DbMonitor.k.q);
					c2.d($"{k} events rejected.");
					a value2 = new a(c2, MonitorTracePoint.BeforeEvent, A_2: false);
					this.m_d.AddLast(value2);
					value2 = new a(c2, MonitorTracePoint.AfterEvent, A_2: false);
					this.m_d.AddLast(value2);
					k = 0;
				}
				this.m_d.AddLast(value);
			}
		}
		if (this.m_b == null)
		{
			this.m_b = new Thread(a);
			this.m_b.Name = "Devart_DbMonitor";
			this.m_b.IsBackground = true;
			this.m_b.Start();
			this.m_e = false;
		}
		this.m_c.Set();
	}

	private void a()
	{
		while (!this.m_e)
		{
			this.m_c.WaitOne();
			int count = this.m_d.Count;
			while (this.m_d.Count > 0)
			{
				a value = this.m_d.First.Value;
				try
				{
					if (this.m_a == null)
					{
						this.m_a = new Devart.DbMonitor.n();
						this.m_a.b(c());
						this.m_a.a(e());
						this.m_a.c(g());
						this.m_a.g();
					}
					Devart.DbMonitor.c c2 = value.a;
					if (value.b == MonitorTracePoint.BeforeEvent)
					{
						this.m_a.c(c2);
						a(c2.p());
					}
					else
					{
						if (value.b != MonitorTracePoint.AfterEvent)
						{
							throw new NotSupportedException(value.b.ToString());
						}
						c2.a(f());
						this.m_a.b(c2);
					}
					lock (this.m_f)
					{
						this.m_d.RemoveFirst();
					}
				}
				catch
				{
					this.m_a = null;
					lock (this.m_f)
					{
						for (int num = 0; num < count; num++)
						{
							this.m_d.RemoveFirst();
						}
					}
					break;
				}
			}
		}
	}

	internal int f()
	{
		if (this.m_g.Count == 0)
		{
			return 0;
		}
		return this.m_g.Pop();
	}

	internal void a(int A_0)
	{
		this.m_g.Push(A_0);
	}

	[SpecialName]
	public string c()
	{
		return l;
	}

	[SpecialName]
	public void b(string A_0)
	{
		l = A_0;
	}

	[SpecialName]
	public string e()
	{
		return h;
	}

	[SpecialName]
	public void a(string A_0)
	{
		if (h != A_0)
		{
			h = A_0;
			b();
		}
	}

	[SpecialName]
	public int g()
	{
		return i;
	}

	[SpecialName]
	public void c(int A_0)
	{
		if (i != A_0)
		{
			i = A_0;
			b();
		}
	}

	[SpecialName]
	public int d()
	{
		return j;
	}

	[SpecialName]
	public void b(int A_0)
	{
		j = A_0;
	}
}
