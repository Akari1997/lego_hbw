using System;
using System.ComponentModel;
using System.Threading;

namespace Devart.Common;

[TypeConverter(typeof(b))]
public class ProxyOptions
{
	public const int DefaultPort = 3128;

	private string m_a = string.Empty;

	private int b = 3128;

	private string c = string.Empty;

	private string d = string.Empty;

	private ProxyOptionsPropertyChanged e;

	public static readonly string ProxyHostKeyword = "Proxy Host";

	public static readonly string ProxyPortKeyword = "Proxy Port";

	public static readonly string ProxyUserKeyword = "Proxy User";

	public static readonly string ProxyPasswordKeyword = "Proxy Password";

	[RefreshProperties(RefreshProperties.Repaint)]
	[r("ProxyOptions_Host")]
	[DefaultValue("")]
	[Browsable(true)]
	public string Host
	{
		get
		{
			return this.m_a;
		}
		set
		{
			if (this.m_a != value)
			{
				this.m_a = value;
				a(ProxyHostKeyword, value);
			}
		}
	}

	[Browsable(true)]
	[r("ProxyOptions_Port")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(3128)]
	public int Port
	{
		get
		{
			return b;
		}
		set
		{
			if (b != value)
			{
				b = value;
				a(ProxyPortKeyword, value);
			}
		}
	}

	[Browsable(true)]
	[DefaultValue("")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[r("ProxyOptions_User")]
	public string User
	{
		get
		{
			return c;
		}
		set
		{
			if (c != value)
			{
				c = value;
				a(ProxyUserKeyword, value);
			}
		}
	}

	[RefreshProperties(RefreshProperties.Repaint)]
	[r("ProxyOptions_Password")]
	[PasswordPropertyText(true)]
	[Browsable(true)]
	[DefaultValue("")]
	public string Password
	{
		get
		{
			return d;
		}
		set
		{
			if (d != value)
			{
				d = value;
				a(ProxyPasswordKeyword, value);
			}
		}
	}

	[r("ProxyOptions_ProxyAddress")]
	[DefaultValue("")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Browsable(true)]
	public Uri ProxyAddress
	{
		get
		{
			UriBuilder uriBuilder = new UriBuilder();
			uriBuilder.Host = this.m_a;
			uriBuilder.Port = b;
			uriBuilder.UserName = c;
			uriBuilder.Password = d;
			return uriBuilder.Uri;
		}
	}

	public event ProxyOptionsPropertyChanged PropertyChanged
	{
		add
		{
			ProxyOptionsPropertyChanged proxyOptionsPropertyChanged = e;
			ProxyOptionsPropertyChanged proxyOptionsPropertyChanged2;
			do
			{
				proxyOptionsPropertyChanged2 = proxyOptionsPropertyChanged;
				ProxyOptionsPropertyChanged value2 = (ProxyOptionsPropertyChanged)Delegate.Combine(proxyOptionsPropertyChanged2, value);
				proxyOptionsPropertyChanged = Interlocked.CompareExchange(ref e, value2, proxyOptionsPropertyChanged2);
			}
			while ((object)proxyOptionsPropertyChanged != proxyOptionsPropertyChanged2);
		}
		remove
		{
			ProxyOptionsPropertyChanged proxyOptionsPropertyChanged = e;
			ProxyOptionsPropertyChanged proxyOptionsPropertyChanged2;
			do
			{
				proxyOptionsPropertyChanged2 = proxyOptionsPropertyChanged;
				ProxyOptionsPropertyChanged value2 = (ProxyOptionsPropertyChanged)Delegate.Remove(proxyOptionsPropertyChanged2, value);
				proxyOptionsPropertyChanged = Interlocked.CompareExchange(ref e, value2, proxyOptionsPropertyChanged2);
			}
			while ((object)proxyOptionsPropertyChanged != proxyOptionsPropertyChanged2);
		}
	}

	public ProxyOptions()
	{
	}

	public ProxyOptions(string host, int port, string user, string password)
	{
		this.m_a = host;
		b = port;
		c = user;
		d = password;
	}

	private void a(string A_0, object A_1)
	{
		if (e != null)
		{
			e(A_0, A_1);
		}
	}

	public override string ToString()
	{
		return "ProxyOptions";
	}

	public bool ShouldSerialize()
	{
		if (!(this.m_a != string.Empty) && b == 3128 && !(c != string.Empty))
		{
			return d != string.Empty;
		}
		return true;
	}
}
