using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using Devart.Common;

namespace Devart.DbMonitor;

internal class m : h, IDisposable
{
	public const int a = 1000;

	public const int b = 5000;

	public const int c = 1000;

	private string m_d;

	private int m_e;

	private int m_f;

	private int m_g;

	private TcpClient m_h;

	private NetworkStream m_i;

	private int j;

	private bool k;

	public m()
	{
		this.m_f = 5000;
		this.m_g = 1000;
	}

	public void h()
	{
		if (b())
		{
			c();
		}
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in h
		this.h();
	}

	public bool g()
	{
		if (k && Environment.TickCount < j + this.m_f)
		{
			return false;
		}
		if (string.IsNullOrEmpty(d()))
		{
			b("localhost");
		}
		if (f() == 0)
		{
			d(1000);
		}
		try
		{
			if (IPAddress.TryParse(this.m_d, out IPAddress address))
			{
				this.m_h = new TcpClient(address.AddressFamily);
			}
			else
			{
				this.m_h = new TcpClient();
			}
			this.m_h.NoDelay = true;
			this.m_h.SendTimeout = a();
			if (Utils.IsIpAddress(d()))
			{
				IPAddress address2 = IPAddress.Parse(d());
				this.m_h.Connect(address2, this.m_e);
			}
			else
			{
				this.m_h.Connect(this.m_d, this.m_e);
			}
			this.m_i = this.m_h.GetStream();
			k = false;
			return true;
		}
		catch (SocketException)
		{
			c();
			k = true;
			j = Environment.TickCount;
			return false;
		}
	}

	public void c()
	{
		if (this.m_i != null)
		{
			this.m_i.Close();
			this.m_i = null;
		}
		if (this.m_h != null)
		{
			this.m_h.Close();
			this.m_h = null;
		}
	}

	[SpecialName]
	public bool b()
	{
		return this.m_i != null;
	}

	public bool i()
	{
		if (!b())
		{
			return false;
		}
		try
		{
			a(6);
		}
		catch (IOException)
		{
			c();
		}
		return b();
	}

	public void a(o A_0)
	{
		try
		{
			a((int)A_0.v());
			A_0.a(this);
		}
		catch (IOException)
		{
			c();
		}
	}

	public void a(byte A_0)
	{
		this.m_i.WriteByte(A_0);
	}

	public void a(int A_0)
	{
		byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(A_0));
		if (!BitConverter.IsLittleEndian)
		{
			a(bytes);
		}
		this.m_i.Write(bytes, 0, 4);
	}

	public void a(string A_0)
	{
		byte[] array = new byte[0];
		if (A_0 != null)
		{
			array = Encoding.UTF8.GetBytes(A_0);
		}
		if (array != null && array.Length > 0)
		{
			a(array.Length);
			this.m_i.Write(array, 0, array.Length);
		}
		else
		{
			a(0);
		}
	}

	private static void a(byte[] A_0)
	{
		for (int num = 0; num < A_0.Length / 2; num++)
		{
			byte b2 = A_0[num];
			A_0[num] = A_0[A_0.Length - num - 1];
			A_0[A_0.Length - num - 1] = b2;
		}
	}

	[SpecialName]
	public string d()
	{
		return this.m_d;
	}

	[SpecialName]
	public void b(string A_0)
	{
		this.m_d = A_0;
	}

	[SpecialName]
	public int f()
	{
		return this.m_e;
	}

	[SpecialName]
	public void d(int A_0)
	{
		this.m_e = A_0;
	}

	[SpecialName]
	public int e()
	{
		return this.m_f;
	}

	[SpecialName]
	public void c(int A_0)
	{
		this.m_f = A_0;
	}

	[SpecialName]
	public int a()
	{
		return this.m_g;
	}

	[SpecialName]
	public void b(int A_0)
	{
		this.m_g = A_0;
	}
}
