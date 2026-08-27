using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

public class OracleGlobalization
{
	private string m_a;

	private string m_b;

	private string c;

	private string d;

	private string m_e;

	private string f;

	private string g;

	private bool h;

	private string i;

	private string j;

	private string k;

	private string l;

	private string m;

	private int n;

	private string o;

	private sbyte p;

	private sbyte q;

	private bool r;

	internal CultureInfo s = CultureInfo.InvariantCulture;

	private static OracleGlobalization t;

	public string ClientCharacterSet
	{
		get
		{
			return this.m_a;
		}
		set
		{
			this.m_a = value;
			n = -1;
		}
	}

	public string Currency
	{
		get
		{
			return this.m_b;
		}
		set
		{
			this.m_b = value;
		}
	}

	public string DateFormat
	{
		get
		{
			return c;
		}
		set
		{
			o = null;
			c = value;
		}
	}

	public string DateLanguage
	{
		get
		{
			return d;
		}
		set
		{
			d = value;
		}
	}

	public string DualCurrency
	{
		get
		{
			return this.m_e;
		}
		set
		{
			this.m_e = value;
		}
	}

	public string ISOCurrency
	{
		get
		{
			return f;
		}
		set
		{
			f = value;
		}
	}

	public string Language
	{
		get
		{
			return g;
		}
		set
		{
			g = value;
		}
	}

	public bool NCharConversionException
	{
		get
		{
			return h;
		}
		set
		{
			h = value;
		}
	}

	public string NumericCharacters
	{
		get
		{
			return i;
		}
		set
		{
			if (value == null || value.Length < 1)
			{
				throw new ArgumentException();
			}
			if (value.Length == 1)
			{
				i = value + " ";
			}
			else
			{
				i = value;
			}
		}
	}

	public string Territory
	{
		get
		{
			return j;
		}
		set
		{
			j = value;
		}
	}

	public string TimeStampFormat
	{
		get
		{
			return k;
		}
		set
		{
			k = value;
		}
	}

	public string TimeStampTZFormat
	{
		get
		{
			return l;
		}
		set
		{
			l = value;
		}
	}

	public string TimeZone
	{
		get
		{
			return m;
		}
		set
		{
			m = value;
		}
	}

	internal sbyte TzHour
	{
		get
		{
			if (p == -100)
			{
				OracleTimeStamp.a(m, out p, out q);
			}
			return p;
		}
	}

	internal sbyte TzMinute
	{
		get
		{
			if (q == -100)
			{
				OracleTimeStamp.a(m, out p, out q);
			}
			return q;
		}
	}

	internal static OracleGlobalization ApplicationGlobalization
	{
		get
		{
			if (t == null)
			{
				t = GetApplicationInfo();
			}
			return t;
		}
	}

	internal string DateFormatInternal
	{
		get
		{
			if (o == null)
			{
				o = OracleTimeStamp.b(c);
			}
			return o;
		}
	}

	internal OracleGlobalization()
	{
		n = -1;
		p = -100;
		q = -100;
		o = null;
		r = false;
	}

	public object Clone()
	{
		OracleGlobalization oracleGlobalization = new OracleGlobalization();
		oracleGlobalization.a(this);
		return oracleGlobalization;
	}

	internal void a(OracleGlobalization A_0)
	{
		s = A_0.s;
		this.m_a = A_0.m_a;
		n = A_0.n;
		this.m_b = A_0.m_b;
		c = A_0.c;
		o = A_0.o;
		d = A_0.d;
		this.m_e = A_0.m_e;
		f = A_0.f;
		g = A_0.g;
		h = A_0.h;
		i = A_0.i;
		j = A_0.j;
		k = A_0.k;
		l = A_0.l;
		m = A_0.m;
		p = A_0.p;
		q = A_0.q;
	}

	private void a(g A_0)
	{
		if (Utils.IsEmpty(this.m_a))
		{
			n = 0;
			return;
		}
		if (A_0 is v v2)
		{
			if (string.Compare(v2.v(), "08.00.00") < 0)
			{
				n = 0;
				return;
			}
			a5 a10 = (a5)v2.h().a(v2, A_1: false);
			h[] array = new h[2];
			byte[] array2 = new byte[100];
			Hashtable hashtable = new Hashtable();
			try
			{
				a10.a("begin :res := NLS_CHARSET_ID(:Charset);end;");
				array[0].a = "res";
				array[0].c = 3;
				array[0].n = 32;
				array[0].l = 36;
				array[0].m = 4;
				array[1].a = "Charset";
				array[1].c = 5;
				array[1].n = 0;
				array[1].l = 2;
				array[1].m = 30;
				array[1].d = 30;
				string text = ((this.m_a.Length <= 30) ? this.m_a : this.m_a.Substring(0, 30));
				int bytes = Encoding.Default.GetBytes(text, 0, text.Length, array2, 2);
				if (bytes > 30)
				{
					throw new InvalidOperationException("Invalid size of Charset parameter");
				}
				hashtable[array[1].l] = text;
				a10.b(array, array2, hashtable);
				a10.a(1, az.a);
			}
			finally
			{
				a10.l();
			}
			if (Devart.Common.e.h(array2, array[0].n) == 0)
			{
				n = Devart.Common.e.g(array2, array[0].l);
			}
			else
			{
				n = 0;
			}
			return;
		}
		throw new ArgumentException();
	}

	internal void e()
	{
		n = 0;
	}

	internal int b(g A_0)
	{
		if (n != -1)
		{
			return n;
		}
		if (r)
		{
			return 0;
		}
		r = true;
		try
		{
			a(A_0);
		}
		finally
		{
			r = false;
		}
		return n;
	}

	public static OracleGlobalization GetSystemInfo()
	{
		OracleGlobalization oracleGlobalization = new OracleGlobalization();
		GetSystemInfo(oracleGlobalization);
		return oracleGlobalization;
	}

	public static OracleGlobalization GetSystemInfo(CultureInfo culture)
	{
		OracleGlobalization oracleGlobalization = new OracleGlobalization();
		GetSystemInfo(oracleGlobalization, culture);
		return oracleGlobalization;
	}

	public static void GetSystemInfo(OracleGlobalization oraGlob)
	{
		GetSystemInfo(oraGlob, CultureInfo.CurrentCulture);
	}

	public static void GetSystemInfo(OracleGlobalization oraGlob, CultureInfo culture)
	{
		oraGlob.s = culture;
		oraGlob.ClientCharacterSet = OracleUtils.GetCharSetName(culture.TextInfo.ANSICodePage);
		string text = culture.NumberFormat.CurrencySymbol;
		if (text.Length > 1)
		{
			text = " ";
		}
		oraGlob.Currency = text;
		oraGlob.DateFormat = OracleTimeStamp.a(culture.DateTimeFormat.ShortDatePattern, A_1: false);
		oraGlob.DateLanguage = "AMERICAN";
		oraGlob.DualCurrency = culture.NumberFormat.CurrencySymbol;
		oraGlob.ISOCurrency = "AMERICA";
		oraGlob.Language = "AMERICAN";
		oraGlob.NCharConversionException = true;
		string text2 = culture.NumberFormat.NumberGroupSeparator;
		if (text2 == "'")
		{
			text2 = " ";
		}
		oraGlob.NumericCharacters = culture.NumberFormat.NumberDecimalSeparator + text2;
		oraGlob.Territory = "AMERICA";
		oraGlob.TimeStampFormat = OracleTimeStamp.a(culture.DateTimeFormat.ShortDatePattern + " " + culture.DateTimeFormat.LongTimePattern, A_1: false);
		oraGlob.TimeStampTZFormat = OracleTimeStamp.a(culture.DateTimeFormat.ShortDatePattern + " " + culture.DateTimeFormat.LongTimePattern, A_1: true);
		TimeSpan utcOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		int hours = utcOffset.Hours;
		int minutes = utcOffset.Minutes;
		string text3 = "";
		text3 = ((hours >= 0 && (hours != 0 || minutes >= 0)) ? (text3 + "+") : (text3 + "-"));
		text3 = text3 + Math.Abs(hours).ToString("00") + ":" + Math.Abs(minutes).ToString("00");
		oraGlob.TimeZone = text3;
	}

	public static OracleGlobalization GetApplicationInfo()
	{
		OracleGlobalization oracleGlobalization = new OracleGlobalization();
		GetApplicationInfo(oracleGlobalization);
		return oracleGlobalization;
	}

	public static void GetApplicationInfo(OracleGlobalization oraGlob)
	{
		if (t == null)
		{
			OracleGlobalization oraGlob2 = new OracleGlobalization();
			try
			{
				OracleHomeCollection.SingletonInstance.DefaultHome.GetClientInfo(oraGlob2);
			}
			catch
			{
				GetSystemInfo(oraGlob2);
			}
			t = oraGlob2;
		}
		oraGlob.a(t);
	}

	public static void SetApplicationInfo(OracleGlobalization oraGlob)
	{
		if (t == null)
		{
			t = new OracleGlobalization();
		}
		t.a(oraGlob);
	}
}
