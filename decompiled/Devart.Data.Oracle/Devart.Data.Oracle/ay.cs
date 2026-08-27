using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using Devart.Common;

namespace Devart.Data.Oracle;

internal sealed class ay : DbConnectionOptions
{
	private new bool m_a;

	private int m_b;

	private new int m_c;

	private new int m_d;

	private string m_e;

	private string m_f;

	private string m_g;

	private string m_h;

	private int m_i;

	private int m_j;

	private bool m_k;

	private OracleConnectMode m_l;

	private bool m_m;

	private string m_n;

	private bool m_o;

	private int m_p;

	private int m_q;

	private int m_r;

	private int m_s;

	private bool m_t;

	private string m_u;

	private string m_v;

	private int m_w;

	private string m_x;

	private bool m_y;

	private int m_z;

	private bool m_aa;

	private bool m_ab;

	private bool m_ac;

	private bool m_ad;

	private OracleNumberMappingCollection m_ae;

	private bool m_af;

	private static Hashtable m_ag;

	internal static Hashtable a()
	{
		Hashtable hashtable = ay.m_ag;
		if (hashtable != null)
		{
			return hashtable;
		}
		hashtable = (ay.m_ag = Utils.CreateHashtable(ignoreCase: true));
		hashtable.Add("pooling", "pooling");
		hashtable.Add("min pool size", "min pool size");
		hashtable.Add("max pool size", "max pool size");
		hashtable.Add("connection lifetime", "connection lifetime");
		hashtable.Add("user id", "user id");
		hashtable.Add("userid", "user id");
		hashtable.Add("user", "user id");
		hashtable.Add("uid", "user id");
		hashtable.Add("password", "password");
		hashtable.Add("pwd", "password");
		hashtable.Add("data source", "server");
		hashtable.Add("server", "server");
		hashtable.Add("clientid", "clientid");
		hashtable.Add("client identifier", "clientid");
		hashtable.Add("connection timeout", "connection timeout");
		hashtable.Add("default command timeout", "default command timeout");
		hashtable.Add("connect timeout", "connection timeout");
		hashtable.Add("unicode", "unicode");
		hashtable.Add("connection mode", "connect mode");
		hashtable.Add("connect mode", "connect mode");
		hashtable.Add("mode", "connect mode");
		hashtable.Add("validate connection", "validate connection");
		hashtable.Add("validateconnection", "validate connection");
		hashtable.Add("home", "home");
		hashtable.Add("oracle home", "home");
		hashtable.Add("direct", "direct");
		hashtable.Add("host", "server");
		hashtable.Add("port", "port");
		hashtable.Add("sid", "sid");
		hashtable.Add("service name", "service name");
		hashtable.Add("enlist", "enlist");
		hashtable.Add("transaction scope local", "transaction scope local");
		hashtable.Add("transactionscopelocal", "transaction scope local");
		hashtable.Add("persist security info", "persist security info");
		hashtable.Add("oci session pooling", "oci session pooling");
		hashtable.Add("oci session pool min size", "oci session pool min size");
		hashtable.Add("oci session pool increment", "oci session pool increment");
		hashtable.Add("oci session pool max size", "oci session pool max size");
		hashtable.Add("oci session pool connection lifetime", "oci session pool connection lifetime");
		hashtable.Add("oci session pool allow waiting", "oci session pool allow waiting");
		hashtable.Add("oci session pool user id", "oci session pool user id");
		hashtable.Add("oci session pool password", "oci session pool password");
		hashtable.Add("trim fixed char", "trim fixed char");
		hashtable.Add("trimfixedchar", "trim fixed char");
		hashtable.Add("number mappings", "number mappings");
		hashtable.Add("numbermappings", "number mappings");
		hashtable.Add("pass parameters by name", "pass parameters by name");
		hashtable.Add("passparametersbyname", "pass parameters by name");
		hashtable.Add("statement cache size", "statement cache size");
		hashtable.Add("statementcachesize", "statement cache size");
		hashtable.Add("connection class", "connection class");
		hashtable.Add("connectionclass", "connection class");
		hashtable.Add("oramts", "oramts");
		hashtable.Add("lob block size", "lob block size");
		hashtable.Add("lobblocksize", "lob block size");
		hashtable.Add("initialization command", "initialization command");
		hashtable.Add("initializationcommand", "initializationcommand");
		return hashtable;
	}

	public ay(string A_0)
		: base(A_0, a(), useFirstKeyValuePair: false)
	{
		this.m_a = ConvertValueToBoolean("pooling", defaultValue: true);
		this.m_b = ConvertValueToInt32("min pool size", 0);
		this.m_c = ConvertValueToInt32("max pool size", 100);
		this.m_d = ConvertValueToInt32("connection lifetime", 0);
		this.m_e = ConvertValueToStringWithQuotes("user id", "");
		this.m_f = ConvertValueToString("password", "");
		this.m_g = ConvertValueToString("server", "");
		this.m_h = ConvertValueToString("clientid", "");
		this.m_i = ConvertValueToInt32("connection timeout", 15);
		this.m_j = ConvertValueToInt32("default command timeout", 0);
		this.m_k = ConvertValueToBoolean("unicode", defaultValue: false);
		this.m_l = a("connect mode", OracleConnectMode.Default);
		this.m_m = ConvertValueToBoolean("validate connection", defaultValue: false);
		this.m_n = ConvertValueToString("home", "");
		this.m_o = ConvertValueToBoolean("oci session pooling", defaultValue: false);
		this.m_p = ConvertValueToInt32("oci session pool min size", 0);
		this.m_q = ConvertValueToInt32("oci session pool increment", 1);
		this.m_r = ConvertValueToInt32("oci session pool max size", 100);
		this.m_s = ConvertValueToInt32("oci session pool connection lifetime", 0);
		this.m_t = ConvertValueToBoolean("oci session pool allow waiting", defaultValue: true);
		this.m_u = ConvertValueToString("oci session pool user id", "");
		this.m_v = ConvertValueToString("oci session pool password", "");
		this.m_w = ConvertValueToInt32("statement cache size", 0);
		this.m_x = ConvertValueToString("connection class", "");
		this.m_y = ConvertValueToBoolean("oramts", defaultValue: false);
		this.m_z = ConvertValueToInt32("lob block size", 0);
		string value = ConvertValueToString("direct", "");
		if (!string.IsNullOrEmpty(value))
		{
			throw new ArgumentException("Express Edition doesn't support Direct mode. Do not use Direct parameter of connection string. Refer to dotConnect for Oracle editions matrix.");
		}
		this.m_aa = ConvertValueToBoolean("enlist", defaultValue: true);
		this.m_ab = ConvertValueToBoolean("transaction scope local", defaultValue: false);
		this.m_ac = ConvertValueToBoolean("persist security info", defaultValue: false);
		if (this.m_i < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "connection timeout"));
		}
		if (this.m_j < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "default command timeout"));
		}
		if (this.m_d < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "connection lifetime"));
		}
		if (this.m_c < 1)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "max pool size"));
		}
		if (this.m_b < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "min pool size"));
		}
		if (this.m_c < this.m_b)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidMinMaxPoolSizeValues"));
		}
		if (this.m_s < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "oci session pool connection lifetime"));
		}
		if (this.m_r < 1)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "oci session pool max size"));
		}
		if (this.m_p < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "oci session pool min size"));
		}
		if (this.m_r < this.m_p)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidMinMaxPoolSizeValues"));
		}
		if (this.m_w < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "statement cache size"));
		}
		if (this.m_z < 0)
		{
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectionOptionValue", "lob block size"));
		}
		this.m_ad = ConvertValueToBoolean("trim fixed char", defaultValue: true);
		string text = base["number mappings"];
		if (text != null)
		{
			this.m_ae = OracleNumberMappingCollection.Parse(text);
		}
		else
		{
			this.m_ae = new OracleNumberMappingCollection();
		}
		this.m_ae.CollectionChanged += a;
		this.m_af = ConvertValueToBoolean("pass parameters by name", defaultValue: false);
	}

	private void a(object A_0, EventArgs A_1)
	{
		base["number mappings"] = this.m_ae.ToString();
	}

	public ay(ay A_0)
		: base(A_0)
	{
		this.m_a = A_0.m_a;
		this.m_b = A_0.m_b;
		this.m_c = A_0.m_c;
		this.m_d = A_0.m_d;
		this.m_e = A_0.m_e;
		this.m_f = A_0.m_f;
		this.m_g = A_0.m_g;
		this.m_h = A_0.m_h;
		this.m_i = A_0.m_i;
		this.m_j = A_0.m_j;
		this.m_k = A_0.m_k;
		this.m_l = A_0.m_l;
		this.m_m = A_0.m_m;
		this.m_n = A_0.m_n;
		this.m_o = A_0.m_o;
		this.m_p = A_0.m_p;
		this.m_q = A_0.m_q;
		this.m_r = A_0.m_r;
		this.m_s = A_0.m_s;
		this.m_t = A_0.m_t;
		this.m_u = A_0.m_u;
		this.m_v = A_0.m_v;
		this.m_w = A_0.m_w;
		this.m_x = A_0.m_x;
		this.m_y = A_0.m_y;
		this.m_z = A_0.m_z;
		this.m_aa = A_0.m_aa;
		this.m_ab = A_0.m_ab;
		this.m_ac = ConvertValueToBoolean("persist security info", defaultValue: false);
		this.m_ad = A_0.m_ad;
		this.m_ae = A_0.c().Clone();
		this.m_ae.CollectionChanged += a;
		this.m_af = A_0.m_af;
	}

	private OracleConnectMode a(string A_0, OracleConnectMode A_1)
	{
		return OracleConnectionStringBuilder.a(ConvertValueToString(A_0, OracleUtils.a(A_1)).ToLower());
	}

	public bool a(object A_0)
	{
		if (!(A_0 is ay ay2))
		{
			return false;
		}
		StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
		if (Equals(A_0) && this.m_a == ay2.m_a && this.m_b == ay2.m_b && this.m_c == ay2.m_c && this.m_d == ay2.m_d && string.Compare(this.m_e, ay2.m_e, comparisonType) == 0 && this.m_f == ay2.m_f && string.Compare(this.m_h, ay2.m_h, comparisonType) == 0 && this.m_i == ay2.m_i && this.m_j == ay2.m_j && this.m_k == ay2.m_k && this.m_l == ay2.m_l && this.m_ac == ay2.m_ac && string.Compare(this.m_g, ay2.m_g, comparisonType) == 0 && this.m_m == ay2.m_m && string.Compare(this.m_n, ay2.m_n, ignoreCase: true, CultureInfo.InvariantCulture) == 0 && this.m_o == ay2.m_o && this.m_p == ay2.m_p && this.m_q == ay2.m_q && this.m_r == ay2.m_r && this.m_s == ay2.m_s && this.m_t == ay2.m_t && this.m_u == ay2.m_u && this.m_v == ay2.m_v && this.m_w == ay2.m_w && this.m_x == ay2.m_x && this.m_y == ay2.m_y && this.m_z == ay2.m_z && this.m_aa == ay2.m_aa && this.m_ab == ay2.m_ab && this.m_ad == ay2.m_ad && this.m_ae.Equals(ay2.c()))
		{
			return this.m_af == ay2.m_af;
		}
		return false;
	}

	public string a(bool A_0)
	{
		if (A_0 && !base.HasPersistablePassword)
		{
			ay ay2 = new ay(this);
			return ay2.c("Password");
		}
		return ToString();
	}

	public int s()
	{
		return GetHashCode() | this.m_a.GetHashCode() | this.m_b.GetHashCode() | this.m_c.GetHashCode() | this.m_d.GetHashCode() | this.m_e.GetHashCode() | this.m_f.GetHashCode() | this.m_h.GetHashCode() | this.m_i.GetHashCode() | this.m_j.GetHashCode() | this.m_k.GetHashCode() | this.m_l.GetHashCode() | this.m_ac.GetHashCode() | this.m_g.GetHashCode() | this.m_m.GetHashCode() | this.m_n.GetHashCode() | this.m_o.GetHashCode() | this.m_p.GetHashCode() | this.m_q.GetHashCode() | this.m_r.GetHashCode() | this.m_s.GetHashCode() | this.m_t.GetHashCode() | this.m_u.GetHashCode() | this.m_v.GetHashCode() | this.m_w.GetHashCode() | this.m_x.GetHashCode() | this.m_y.GetHashCode() | this.m_z.GetHashCode() | this.m_aa.GetHashCode() | this.m_ab.GetHashCode() | this.m_ad.GetHashCode() | this.m_ae.GetHashCode() | this.m_af.GetHashCode();
	}

	[SpecialName]
	public int p()
	{
		return this.m_b;
	}

	[SpecialName]
	public int o()
	{
		return this.m_c;
	}

	[SpecialName]
	public bool ad()
	{
		return this.m_a;
	}

	[SpecialName]
	public int x()
	{
		return this.m_d;
	}

	[SpecialName]
	public string af()
	{
		return this.m_e;
	}

	[SpecialName]
	public string n()
	{
		return this.m_f;
	}

	[SpecialName]
	public string ag()
	{
		return this.m_g;
	}

	[SpecialName]
	public string t()
	{
		return this.m_h;
	}

	[SpecialName]
	public int v()
	{
		return this.m_i;
	}

	[SpecialName]
	public int y()
	{
		return this.m_j;
	}

	[SpecialName]
	public OracleConnectMode k()
	{
		return this.m_l;
	}

	[SpecialName]
	public bool b()
	{
		return this.m_m;
	}

	[SpecialName]
	public bool u()
	{
		return this.m_k;
	}

	[SpecialName]
	public string f()
	{
		return this.m_n;
	}

	[SpecialName]
	public bool g()
	{
		return this.m_o;
	}

	[SpecialName]
	public int z()
	{
		return this.m_p;
	}

	[SpecialName]
	public new int d()
	{
		return this.m_q;
	}

	[SpecialName]
	public int j()
	{
		return this.m_r;
	}

	[SpecialName]
	public int l()
	{
		return this.m_s;
	}

	[SpecialName]
	public bool m()
	{
		return this.m_t;
	}

	[SpecialName]
	public string q()
	{
		return this.m_u;
	}

	[SpecialName]
	public string i()
	{
		return this.m_v;
	}

	[SpecialName]
	public int e()
	{
		return this.m_w;
	}

	[SpecialName]
	public string h()
	{
		return this.m_x;
	}

	[SpecialName]
	public bool w()
	{
		return this.m_y;
	}

	[SpecialName]
	public int r()
	{
		return this.m_z;
	}

	[SpecialName]
	public bool ab()
	{
		return this.m_aa;
	}

	[SpecialName]
	public bool ae()
	{
		return this.m_ab;
	}

	[SpecialName]
	public bool aa()
	{
		return this.m_ad;
	}

	[SpecialName]
	public new OracleNumberMappingCollection c()
	{
		return this.m_ae;
	}

	[SpecialName]
	public bool ac()
	{
		return this.m_af;
	}
}
