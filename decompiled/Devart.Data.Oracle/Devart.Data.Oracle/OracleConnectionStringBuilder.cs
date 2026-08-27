using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using Devart.Common;

namespace Devart.Data.Oracle;

[Obfuscation]
public class OracleConnectionStringBuilder : DbConnectionStringBuilder
{
	private enum b
	{
		a,
		b,
		c,
		d,
		e,
		f,
		g,
		h,
		i,
		j,
		k,
		l,
		m,
		n,
		o,
		p,
		q,
		r,
		s,
		t,
		u,
		v,
		w,
		x,
		y,
		z,
		aa,
		ab,
		ac,
		ad,
		ae,
		af,
		ag
	}

	internal sealed class a : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if ((object)typeof(InstanceDescriptor) == destinationType)
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if ((object)destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if ((object)typeof(InstanceDescriptor) == destinationType && value is OracleConnectionStringBuilder a_)
			{
				return a(a_);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		private InstanceDescriptor a(OracleConnectionStringBuilder A_0)
		{
			Type[] types = new Type[1] { typeof(string) };
			object[] arguments = new object[1] { A_0.ConnectionString };
			ConstructorInfo constructor = typeof(OracleConnectionStringBuilder).GetConstructor(types);
			return new InstanceDescriptor(constructor, arguments);
		}
	}

	private bool m_a;

	private int m_b;

	private int c;

	private int d;

	private string e;

	private string f;

	private string g;

	private int h;

	private int i;

	private bool j;

	private OracleConnectMode k;

	private bool l;

	private string m;

	private string n;

	private bool o;

	private int p;

	private int q;

	private int r;

	private int s;

	private bool t;

	private string u;

	private string v;

	private int w;

	private string x;

	private bool y;

	private int z;

	private bool aa;

	private bool ab;

	private bool ac;

	private bool ad;

	private OracleNumberMappingCollection ae;

	private bool af;

	private static readonly string[] ag;

	private static readonly Hashtable ah;

	public override ICollection Keys => ag;

	public override ICollection Values
	{
		get
		{
			object[] array = new object[ag.Length];
			for (int num = 0; num < ag.Length; num++)
			{
				array[num] = b((b)num);
			}
			return array;
		}
	}

	public override object this[string keyword]
	{
		get
		{
			b a_ = a(keyword);
			return b(a_);
		}
		set
		{
			if (value != null)
			{
				switch (a(keyword))
				{
				case OracleConnectionStringBuilder.b.j:
					Pooling = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.k:
					MinPoolSize = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.l:
					MaxPoolSize = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.m:
					ConnectionLifetime = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.a:
					UserId = Devart.Common.ap.a(value);
					break;
				case OracleConnectionStringBuilder.b.b:
					Password = Devart.Common.ap.a(value);
					break;
				case OracleConnectionStringBuilder.b.c:
					Server = Devart.Common.ap.a(value);
					break;
				case OracleConnectionStringBuilder.b.h:
					ConnectionTimeout = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.i:
					DefaultCommandTimeout = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.g:
					Unicode = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.d:
					ConnectMode = a(value);
					break;
				case OracleConnectionStringBuilder.b.n:
					ValidateConnection = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.e:
					Home = Devart.Common.ap.a(value);
					break;
				case OracleConnectionStringBuilder.b.f:
					ClientId = Devart.Common.ap.a(value);
					break;
				case OracleConnectionStringBuilder.b.o:
					OciSessionPooling = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.p:
					OciSessionPoolMinSize = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.q:
					OciSessionPoolIncrement = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.r:
					OciSessionPoolMaxSize = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.s:
					OciSessionPoolConnectionLifetime = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.t:
					OciSessionPoolAllowWaiting = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.u:
					OciSessionPoolUserId = Devart.Common.ap.a(value);
					break;
				case OracleConnectionStringBuilder.b.v:
					OciSessionPoolPassword = Devart.Common.ap.a(value);
					break;
				case OracleConnectionStringBuilder.b.w:
					StatementCacheSize = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.x:
					ConnectionClass = Devart.Common.ap.a(value);
					break;
				case OracleConnectionStringBuilder.b.y:
					OraMts = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.z:
					LobBlockSize = Devart.Common.ap.b(value);
					break;
				case OracleConnectionStringBuilder.b.aa:
					Enlist = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.ab:
					TransactionScopeLocal = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.ac:
					PersistSecurityInfo = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.ad:
					TrimFixedChar = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.ae:
					NumberMappings.CollectionChanged -= a;
					if (value is string)
					{
						NumberMappings = OracleNumberMappingCollection.Parse(value as string);
					}
					else
					{
						NumberMappings = (OracleNumberMappingCollection)value;
					}
					NumberMappings.CollectionChanged += a;
					break;
				case OracleConnectionStringBuilder.b.af:
					PassParametersByName = Devart.Common.ap.c(value);
					break;
				case OracleConnectionStringBuilder.b.ag:
					base.InitializationCommand = Devart.Common.ap.a(value);
					break;
				default:
					throw new NotSupportedException(Devart.Common.al.a("KeywordNotSupported", keyword));
				}
			}
			else
			{
				Remove(keyword);
			}
		}
	}

	public override bool IsFixedSize => true;

	[DisplayName("Pooling")]
	[Devart.Common.i("DbConnectionString_Pooling")]
	[Category("Pooling")]
	[RefreshProperties(RefreshProperties.All)]
	public bool Pooling
	{
		get
		{
			return this.m_a;
		}
		set
		{
			SetValue("Pooling", value);
			this.m_a = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("DbConnectionString_MinPoolSize")]
	[Category("Pooling")]
	[DisplayName("Min Pool Size")]
	public int MinPoolSize
	{
		get
		{
			return this.m_b;
		}
		set
		{
			SetValue("Min Pool Size", value);
			this.m_b = value;
		}
	}

	[DisplayName("Max Pool Size")]
	[Devart.Common.i("DbConnectionString_MaxPoolSize")]
	[Category("Pooling")]
	[RefreshProperties(RefreshProperties.All)]
	public int MaxPoolSize
	{
		get
		{
			return c;
		}
		set
		{
			SetValue("Max Pool Size", value);
			c = value;
		}
	}

	[Category("Pooling")]
	[DisplayName("Connection Lifetime")]
	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("DbConnectionString_ConnectionLifetime")]
	public int ConnectionLifetime
	{
		get
		{
			return d;
		}
		set
		{
			SetValue("Connection Lifetime", value);
			d = value;
		}
	}

	[DisplayName("User Id")]
	[Devart.Common.i("DbConnectionString_UserId")]
	[RefreshProperties(RefreshProperties.All)]
	[Category("Security")]
	public string UserId
	{
		get
		{
			return e;
		}
		set
		{
			SetValue("User Id", value);
			e = value;
		}
	}

	[Devart.Common.i("DbConnectionString_Password")]
	[DisplayName("Password")]
	[RefreshProperties(RefreshProperties.All)]
	[Category("Security")]
	[PasswordPropertyText(true)]
	public string Password
	{
		get
		{
			return f;
		}
		set
		{
			SetValue("Password", value);
			f = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("DbConnectionString_Server")]
	[DisplayName("Server")]
	[Category("Source")]
	public string Server
	{
		get
		{
			return g;
		}
		set
		{
			SetValue("Server", value);
			g = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("DbConnectionString_ConnectionTimeout")]
	[DisplayName("Connection Timeout")]
	[Category("Initialization")]
	public int ConnectionTimeout
	{
		get
		{
			return h;
		}
		set
		{
			SetValue("Connection Timeout", value);
			h = value;
		}
	}

	[Devart.Common.i("DbConnectionString_DefaultCommandTimeout")]
	[Category("Initialization")]
	[DisplayName("Default Command Timeout")]
	[RefreshProperties(RefreshProperties.All)]
	public int DefaultCommandTimeout
	{
		get
		{
			return i;
		}
		set
		{
			SetValue("Default Command Timeout", value);
			i = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("DbConnectionString_ConnectMode")]
	[DisplayName("Connect Mode")]
	[Category("Initialization")]
	public OracleConnectMode ConnectMode
	{
		get
		{
			return k;
		}
		set
		{
			SetValue("Connect Mode", value.ToString());
			k = value;
		}
	}

	[Category("Initialization")]
	[DisplayName("Unicode")]
	[Devart.Common.i("OracleConnectionString_Unicode")]
	[RefreshProperties(RefreshProperties.All)]
	public bool Unicode
	{
		get
		{
			return j;
		}
		set
		{
			SetValue("Unicode", value);
			j = value;
		}
	}

	[Category("Pooling")]
	[Devart.Common.i("DbConnectionString_ValidateConnection")]
	[DisplayName("Validate Connection")]
	[RefreshProperties(RefreshProperties.All)]
	public bool ValidateConnection
	{
		get
		{
			return l;
		}
		set
		{
			SetValue("Validate Connection", value);
			l = value;
		}
	}

	[Devart.Common.i("DbConnectionString_Home")]
	[RefreshProperties(RefreshProperties.All)]
	[Category("Initialization")]
	[DisplayName("Home")]
	public string Home
	{
		get
		{
			return m;
		}
		set
		{
			SetValue("Home", value);
			m = value;
		}
	}

	[DisplayName("ClientId")]
	[Devart.Common.i("OracleConnection_ClientId")]
	[Category("Initialization")]
	[RefreshProperties(RefreshProperties.All)]
	public string ClientId
	{
		get
		{
			return n;
		}
		set
		{
			SetValue("ClientId", value);
			n = value;
		}
	}

	[Devart.Common.i("DbConnectionString_OciSessionPooling")]
	[Category("OCI Session Pooling")]
	[DisplayName("Oci Session Pooling")]
	[RefreshProperties(RefreshProperties.All)]
	public bool OciSessionPooling
	{
		get
		{
			return o;
		}
		set
		{
			SetValue("Oci Session Pooling", value);
			o = value;
		}
	}

	[Category("OCI Session Pooling")]
	[Devart.Common.i("DbConnectionString_OciSessionPoolMinSize")]
	[DisplayName("Oci Session Pool Min Size")]
	[RefreshProperties(RefreshProperties.All)]
	public int OciSessionPoolMinSize
	{
		get
		{
			return p;
		}
		set
		{
			SetValue("Oci Session Pool Min Size", value);
			p = value;
		}
	}

	[DisplayName("Oci Session Pool Increment")]
	[Devart.Common.i("DbConnectionString_OciSessionPoolIncrement")]
	[RefreshProperties(RefreshProperties.All)]
	[Category("OCI Session Pooling")]
	public int OciSessionPoolIncrement
	{
		get
		{
			return q;
		}
		set
		{
			SetValue("Oci Session Pool Increment", value);
			q = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[DisplayName("Oci Session Pool Max Size")]
	[Devart.Common.i("DbConnectionString_OciSessionPoolMaxSize")]
	[Category("OCI Session Pooling")]
	public int OciSessionPoolMaxSize
	{
		get
		{
			return r;
		}
		set
		{
			SetValue("Oci Session Pool Max Size", value);
			r = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[DisplayName("Oci Session Pool Connection Lifetime")]
	[Category("OCI Session Pooling")]
	[Devart.Common.i("DbConnectionString_OciSessionPoolConnectionLifetime")]
	public int OciSessionPoolConnectionLifetime
	{
		get
		{
			return s;
		}
		set
		{
			SetValue("Oci Session Pool Connection Lifetime", value);
			s = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[Category("OCI Session Pooling")]
	[DisplayName("Oci Session Pool Allow Waiting")]
	[Devart.Common.i("DbConnectionString_OciSessionPoolAllowWaiting")]
	public bool OciSessionPoolAllowWaiting
	{
		get
		{
			return t;
		}
		set
		{
			SetValue("Oci Session Pool Allow Waiting", value);
			t = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[DisplayName("Oci Session Pool User Id")]
	[Devart.Common.i("DbConnectionString_OciSessionPoolUserId")]
	[Category("OCI Session Pooling")]
	public string OciSessionPoolUserId
	{
		get
		{
			return u;
		}
		set
		{
			SetValue("Oci Session Pool User Id", value);
			u = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[DisplayName("Oci Session Pool Password")]
	[Devart.Common.i("DbConnectionString_OciSessionPoolPassword")]
	[Category("OCI Session Pooling")]
	public string OciSessionPoolPassword
	{
		get
		{
			return v;
		}
		set
		{
			SetValue("Oci Session Pool Password", value);
			v = value;
		}
	}

	[Category("OCI Session Pooling")]
	[RefreshProperties(RefreshProperties.All)]
	[DisplayName("Connection Class")]
	[Devart.Common.i("DbConnectionString_ConnectionClass")]
	public string ConnectionClass
	{
		get
		{
			return x;
		}
		set
		{
			SetValue("Connection Class", value);
			x = value;
		}
	}

	[Category("Initialization")]
	[RefreshProperties(RefreshProperties.All)]
	[DisplayName("Statement Cache Size")]
	[Devart.Common.i("DbConnectionString_StatementCacheSize")]
	public int StatementCacheSize
	{
		get
		{
			return w;
		}
		set
		{
			SetValue("Statement Cache Size", value);
			w = value;
		}
	}

	[DisplayName("OraMts")]
	[Devart.Common.i("DbConnectionString_OraMts")]
	[Category("Initialization")]
	[RefreshProperties(RefreshProperties.All)]
	public bool OraMts
	{
		get
		{
			return y;
		}
		set
		{
			SetValue("OraMts", value);
			y = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[DisplayName("Lob Block Size")]
	[Devart.Common.i("DbConnectionString_LobBlockSize")]
	[Category("Initialization")]
	public int LobBlockSize
	{
		get
		{
			return z;
		}
		set
		{
			SetValue("Lob Block Size", value);
			z = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("DbConnectionString_Enlist")]
	[DisplayName("Enlist")]
	[Category("Initialization")]
	public bool Enlist
	{
		get
		{
			return aa;
		}
		set
		{
			SetValue("Enlist", value);
			aa = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("DbConnectionString_TransactionScopeLocal")]
	[Category("Initialization")]
	[DisplayName("Transaction Scope Local")]
	public bool TransactionScopeLocal
	{
		get
		{
			return ab;
		}
		set
		{
			SetValue("Transaction Scope Local", value);
			ab = value;
		}
	}

	[Category("Security")]
	[Devart.Common.i("DbConnectionString_PersistSecurityInfo")]
	[RefreshProperties(RefreshProperties.All)]
	[DisplayName("Persist Security Info")]
	public bool PersistSecurityInfo
	{
		get
		{
			return ac;
		}
		set
		{
			SetValue("Persist Security Info", value);
			ac = value;
		}
	}

	[DisplayName("Trim Fixed Char")]
	[RefreshProperties(RefreshProperties.All)]
	[Category("Provider Behaviour")]
	[Devart.Common.i("DbConnectionString_TrimFixedChar")]
	public bool TrimFixedChar
	{
		get
		{
			return ad;
		}
		set
		{
			SetValue("Trim Fixed Char", value);
			ad = value;
		}
	}

	[RefreshProperties(RefreshProperties.All)]
	[Devart.Common.i("DbConnectionString_NumberMappings")]
	[Category("Provider Behaviour")]
	[DisplayName("Number Mappings")]
	public OracleNumberMappingCollection NumberMappings
	{
		get
		{
			return ae;
		}
		set
		{
			SetValue("Number Mappings", value.ToString());
			ae = value;
		}
	}

	[Devart.Common.i("DbConnectionString_PassParametersByName")]
	[DisplayName("Pass Parameters By Name")]
	[Category("Provider Behaviour")]
	[RefreshProperties(RefreshProperties.All)]
	public bool PassParametersByName
	{
		get
		{
			return af;
		}
		set
		{
			SetValue("Pass Parameters By Name", value.ToString());
			af = value;
		}
	}

	static OracleConnectionStringBuilder()
	{
		string[] array = new string[33];
		array[0] = "User Id";
		array[1] = "Password";
		array[2] = "Server";
		array[3] = "Connect Mode";
		array[7] = "Connection Timeout";
		array[8] = "Default Command Timeout";
		array[6] = "Unicode";
		array[9] = "Pooling";
		array[10] = "Min Pool Size";
		array[11] = "Max Pool Size";
		array[12] = "Connection Lifetime";
		array[13] = "Validate Connection";
		array[4] = "Home";
		array[5] = "ClientId";
		array[14] = "Oci Session Pooling";
		array[15] = "Oci Session Pool Min Size";
		array[16] = "Oci Session Pool Increment";
		array[17] = "Oci Session Pool Max Size";
		array[18] = "Oci Session Pool Connection Lifetime";
		array[19] = "Oci Session Pool Allow Waiting";
		array[20] = "Oci Session Pool User Id";
		array[21] = "Oci Session Pool Password";
		array[22] = "Statement Cache Size";
		array[23] = "Connection Class";
		array[24] = "OraMts";
		array[25] = "Lob Block Size";
		array[26] = "Enlist";
		array[27] = "Transaction Scope Local";
		array[28] = "Persist Security Info";
		array[29] = "Trim Fixed Char";
		array[30] = "Number Mappings";
		array[31] = "Pass Parameters By Name";
		array[32] = "Initialization Command";
		ag = array;
		ah = new Hashtable(StringComparer.InvariantCultureIgnoreCase)
		{
			{
				"User Id",
				OracleConnectionStringBuilder.b.a
			},
			{
				"Password",
				OracleConnectionStringBuilder.b.b
			},
			{
				"Server",
				OracleConnectionStringBuilder.b.c
			},
			{
				"Connection Timeout",
				OracleConnectionStringBuilder.b.h
			},
			{
				"Default Command Timeout",
				OracleConnectionStringBuilder.b.i
			},
			{
				"Connect Mode",
				OracleConnectionStringBuilder.b.d
			},
			{
				"Unicode",
				OracleConnectionStringBuilder.b.g
			},
			{
				"Pooling",
				OracleConnectionStringBuilder.b.j
			},
			{
				"Min Pool Size",
				OracleConnectionStringBuilder.b.k
			},
			{
				"Max Pool Size",
				OracleConnectionStringBuilder.b.l
			},
			{
				"Connection Lifetime",
				OracleConnectionStringBuilder.b.m
			},
			{
				"UserId",
				OracleConnectionStringBuilder.b.a
			},
			{
				"user",
				OracleConnectionStringBuilder.b.a
			},
			{
				"uid",
				OracleConnectionStringBuilder.b.a
			},
			{
				"pwd",
				OracleConnectionStringBuilder.b.b
			},
			{
				"data source",
				OracleConnectionStringBuilder.b.c
			},
			{
				"connect timeout",
				OracleConnectionStringBuilder.b.h
			},
			{
				"connection Mode",
				OracleConnectionStringBuilder.b.d
			},
			{
				"mode",
				OracleConnectionStringBuilder.b.d
			},
			{
				"validate connection",
				OracleConnectionStringBuilder.b.n
			},
			{
				"validateconnection",
				OracleConnectionStringBuilder.b.n
			},
			{
				"oracle home",
				OracleConnectionStringBuilder.b.e
			},
			{
				"Home",
				OracleConnectionStringBuilder.b.e
			},
			{
				"ClientId",
				OracleConnectionStringBuilder.b.f
			},
			{
				"oci session pooling",
				OracleConnectionStringBuilder.b.o
			},
			{
				"oci session pool min size",
				OracleConnectionStringBuilder.b.p
			},
			{
				"oci session pool increment",
				OracleConnectionStringBuilder.b.q
			},
			{
				"oci session pool max size",
				OracleConnectionStringBuilder.b.r
			},
			{
				"oci session pool connection lifetime",
				OracleConnectionStringBuilder.b.s
			},
			{
				"oci session pool allow waiting",
				OracleConnectionStringBuilder.b.t
			},
			{
				"oci session pool user id",
				OracleConnectionStringBuilder.b.u
			},
			{
				"oci session pool password",
				OracleConnectionStringBuilder.b.v
			},
			{
				"statement cache size",
				OracleConnectionStringBuilder.b.w
			},
			{
				"statementcachesize",
				OracleConnectionStringBuilder.b.w
			},
			{
				"connection class",
				OracleConnectionStringBuilder.b.x
			},
			{
				"connectionclass",
				OracleConnectionStringBuilder.b.x
			},
			{
				"OraMts",
				OracleConnectionStringBuilder.b.y
			},
			{
				"Lob Block Size",
				OracleConnectionStringBuilder.b.z
			},
			{
				"LobBlockSize",
				OracleConnectionStringBuilder.b.z
			},
			{
				"Enlist",
				OracleConnectionStringBuilder.b.aa
			},
			{
				"Transaction Scope Local",
				OracleConnectionStringBuilder.b.ab
			},
			{
				"TransactionScopeLocal",
				OracleConnectionStringBuilder.b.ab
			},
			{
				"Persist Security Info",
				OracleConnectionStringBuilder.b.ac
			},
			{
				"Trim Fixed Char",
				OracleConnectionStringBuilder.b.ad
			},
			{
				"TrimFixedChar",
				OracleConnectionStringBuilder.b.ad
			},
			{
				"Number Mappings",
				OracleConnectionStringBuilder.b.ae
			},
			{
				"NumberMappings",
				OracleConnectionStringBuilder.b.ae
			},
			{
				"Pass Parameters By Name",
				OracleConnectionStringBuilder.b.af
			},
			{
				"PassParametersByName",
				OracleConnectionStringBuilder.b.af
			},
			{
				"Initialization Command",
				OracleConnectionStringBuilder.b.ag
			},
			{
				"InitializationCommand",
				OracleConnectionStringBuilder.b.ag
			}
		};
	}

	public OracleConnectionStringBuilder()
		: this(null)
	{
	}

	public OracleConnectionStringBuilder(string connectionString)
	{
		this.m_a = true;
		this.m_b = 0;
		c = 100;
		d = 0;
		e = "";
		f = "";
		g = "";
		h = 15;
		i = 0;
		j = false;
		k = OracleConnectMode.Default;
		l = false;
		m = "";
		n = "";
		o = false;
		p = 0;
		q = 1;
		r = 100;
		s = 0;
		t = true;
		u = "";
		v = "";
		w = 0;
		x = "";
		y = false;
		z = 0;
		aa = true;
		ab = false;
		ac = false;
		ad = true;
		ae = new OracleNumberMappingCollection();
		ae.CollectionChanged += a;
		af = false;
		initializationCommandInternal = "";
		if (connectionString != null && connectionString != "")
		{
			base.ConnectionString = connectionString;
		}
	}

	public override void Clear()
	{
		base.Clear();
		for (int num = 0; num < ag.Length; num++)
		{
			a((b)num);
		}
	}

	internal void a()
	{
		ClearPropertyDescriptors();
	}

	public override bool ContainsKey(string keyword)
	{
		Utils.CheckArgumentNull(keyword, "keyword");
		return ah.ContainsKey(keyword);
	}

	public override bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder, bool loginOnly)
	{
		if ((object)connectionStringBuilder.GetType() != typeof(OracleConnectionStringBuilder))
		{
			return false;
		}
		OracleConnectionStringBuilder oracleConnectionStringBuilder = (OracleConnectionStringBuilder)connectionStringBuilder;
		if (loginOnly)
		{
			if (Utils.Compare(Home, oracleConnectionStringBuilder.Home, ignoreCase: true) && Utils.Compare(ClientId, oracleConnectionStringBuilder.ClientId, ignoreCase: true) && Utils.Compare(UserId, oracleConnectionStringBuilder.UserId, ignoreCase: true))
			{
				return Utils.Compare(Server, oracleConnectionStringBuilder.Server, ignoreCase: true);
			}
			return false;
		}
		return base.EquivalentTo(connectionStringBuilder);
	}

	private object b(b A_0)
	{
		return A_0 switch
		{
			OracleConnectionStringBuilder.b.a => UserId, 
			OracleConnectionStringBuilder.b.b => Password, 
			OracleConnectionStringBuilder.b.c => Server, 
			OracleConnectionStringBuilder.b.h => ConnectionTimeout, 
			OracleConnectionStringBuilder.b.i => DefaultCommandTimeout, 
			OracleConnectionStringBuilder.b.g => Unicode, 
			OracleConnectionStringBuilder.b.d => ConnectMode, 
			OracleConnectionStringBuilder.b.j => Pooling, 
			OracleConnectionStringBuilder.b.k => MinPoolSize, 
			OracleConnectionStringBuilder.b.l => MaxPoolSize, 
			OracleConnectionStringBuilder.b.m => ConnectionLifetime, 
			OracleConnectionStringBuilder.b.n => ValidateConnection, 
			OracleConnectionStringBuilder.b.e => Home, 
			OracleConnectionStringBuilder.b.f => ClientId, 
			OracleConnectionStringBuilder.b.o => OciSessionPooling, 
			OracleConnectionStringBuilder.b.p => OciSessionPoolMinSize, 
			OracleConnectionStringBuilder.b.q => OciSessionPoolIncrement, 
			OracleConnectionStringBuilder.b.r => OciSessionPoolMaxSize, 
			OracleConnectionStringBuilder.b.s => OciSessionPoolConnectionLifetime, 
			OracleConnectionStringBuilder.b.t => OciSessionPoolAllowWaiting, 
			OracleConnectionStringBuilder.b.u => OciSessionPoolUserId, 
			OracleConnectionStringBuilder.b.v => OciSessionPoolPassword, 
			OracleConnectionStringBuilder.b.w => StatementCacheSize, 
			OracleConnectionStringBuilder.b.x => ConnectionClass, 
			OracleConnectionStringBuilder.b.y => OraMts, 
			OracleConnectionStringBuilder.b.z => LobBlockSize, 
			OracleConnectionStringBuilder.b.aa => Enlist, 
			OracleConnectionStringBuilder.b.ab => TransactionScopeLocal, 
			OracleConnectionStringBuilder.b.ac => PersistSecurityInfo, 
			OracleConnectionStringBuilder.b.ad => TrimFixedChar, 
			OracleConnectionStringBuilder.b.ae => NumberMappings, 
			OracleConnectionStringBuilder.b.af => PassParametersByName, 
			OracleConnectionStringBuilder.b.ag => base.InitializationCommand, 
			_ => throw new NotSupportedException(Devart.Common.al.a("KeywordNotSupported", ag[(int)A_0])), 
		};
	}

	private Attribute[] a(AttributeCollection A_0)
	{
		Attribute[] array = new Attribute[A_0.Count];
		A_0.CopyTo(array, 0);
		return array;
	}

	private b a(string A_0)
	{
		Utils.CheckArgumentNull(A_0, "keyword");
		if (Utils.TryGetValue(ah, A_0, out var val))
		{
			return (b)val;
		}
		throw new NotSupportedException(Devart.Common.al.a("KeywordNotSupported", A_0));
	}

	protected override void GetProperties(Hashtable propertyDescriptors)
	{
		foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this, noCustomTypeDesc: true))
		{
			bool a_ = false;
			bool flag = false;
			string displayName = property.DisplayName;
			if ("Integrated Security" == displayName)
			{
				a_ = true;
				flag = property.IsReadOnly;
			}
			else
			{
				if ("Password" != displayName && "User Id" != displayName)
				{
					continue;
				}
				flag = false;
			}
			Attribute[] a_2 = a(property.Attributes);
			Devart.Common.aj aj2 = new Devart.Common.aj(property.Name, property.ComponentType, property.PropertyType, flag, a_2);
			aj2.a(a_);
			propertyDescriptors[displayName] = aj2;
		}
		base.GetProperties(propertyDescriptors);
	}

	public override bool Remove(string keyword)
	{
		Utils.CheckArgumentNull(keyword, "keyword");
		if (Utils.TryGetValue(ah, keyword, out var val))
		{
			base.Remove(ag[(int)val]);
			a((b)val);
			return true;
		}
		return false;
	}

	private void a(b A_0)
	{
		switch (A_0)
		{
		case OracleConnectionStringBuilder.b.j:
			this.m_a = true;
			break;
		case OracleConnectionStringBuilder.b.k:
			this.m_b = 0;
			break;
		case OracleConnectionStringBuilder.b.l:
			c = 100;
			break;
		case OracleConnectionStringBuilder.b.m:
			d = 0;
			break;
		case OracleConnectionStringBuilder.b.a:
			e = "";
			break;
		case OracleConnectionStringBuilder.b.b:
			f = "";
			break;
		case OracleConnectionStringBuilder.b.c:
			g = "";
			break;
		case OracleConnectionStringBuilder.b.h:
			h = 15;
			break;
		case OracleConnectionStringBuilder.b.i:
			i = 0;
			break;
		case OracleConnectionStringBuilder.b.d:
			k = OracleConnectMode.Default;
			break;
		case OracleConnectionStringBuilder.b.g:
			j = false;
			break;
		case OracleConnectionStringBuilder.b.n:
			l = false;
			break;
		case OracleConnectionStringBuilder.b.e:
			m = "";
			break;
		case OracleConnectionStringBuilder.b.f:
			n = "";
			break;
		case OracleConnectionStringBuilder.b.o:
			o = false;
			break;
		case OracleConnectionStringBuilder.b.p:
			p = 0;
			break;
		case OracleConnectionStringBuilder.b.q:
			q = 1;
			break;
		case OracleConnectionStringBuilder.b.r:
			r = 100;
			break;
		case OracleConnectionStringBuilder.b.s:
			s = 0;
			break;
		case OracleConnectionStringBuilder.b.t:
			t = true;
			break;
		case OracleConnectionStringBuilder.b.u:
			u = "";
			break;
		case OracleConnectionStringBuilder.b.v:
			v = "";
			break;
		case OracleConnectionStringBuilder.b.w:
			w = 0;
			break;
		case OracleConnectionStringBuilder.b.x:
			x = "";
			break;
		case OracleConnectionStringBuilder.b.y:
			y = false;
			break;
		case OracleConnectionStringBuilder.b.z:
			z = 0;
			break;
		case OracleConnectionStringBuilder.b.aa:
			aa = true;
			break;
		case OracleConnectionStringBuilder.b.ab:
			ab = false;
			break;
		case OracleConnectionStringBuilder.b.ac:
			ac = false;
			break;
		case OracleConnectionStringBuilder.b.ad:
			ad = true;
			break;
		case OracleConnectionStringBuilder.b.ae:
			ae.CollectionChanged -= a;
			ae.Clear();
			ae.CollectionChanged += a;
			break;
		case OracleConnectionStringBuilder.b.af:
			af = false;
			break;
		case OracleConnectionStringBuilder.b.ag:
			initializationCommandInternal = "";
			break;
		default:
			throw new NotSupportedException(Devart.Common.al.a("KeywordNotSupported", ag[(int)A_0]));
		}
	}

	public override bool ShouldSerialize(string keyword)
	{
		Utils.CheckArgumentNull(keyword, "keyword");
		if (Utils.TryGetValue(ah, keyword, out var val))
		{
			return base.ShouldSerialize(ag[(int)val]);
		}
		return false;
	}

	public override bool TryGetValue(string keyword, out object value)
	{
		if (Utils.TryGetValue(ah, keyword, out var val))
		{
			value = b((b)val);
			return true;
		}
		value = null;
		return false;
	}

	internal static OracleConnectMode a(object A_0)
	{
		if (A_0 is OracleConnectMode)
		{
			return (OracleConnectMode)A_0;
		}
		switch (Devart.Common.ap.a(A_0).ToLower())
		{
		case "":
		case "default":
			return OracleConnectMode.Default;
		case "sysdba":
			return OracleConnectMode.SysDba;
		case "sysoper":
			return OracleConnectMode.SysOper;
		case "sysasm":
			return OracleConnectMode.SysAsm;
		case "sysbackup":
			return OracleConnectMode.SysBackup;
		case "sysdg":
			return OracleConnectMode.SysDg;
		case "syskm":
			return OracleConnectMode.SysKm;
		default:
			throw new ArgumentException(Devart.Common.al.a("InvalidConnectMode"));
		}
	}

	private void a(object A_0, EventArgs A_1)
	{
		NumberMappings = ae;
	}

	public override string ToString()
	{
		return base.ToString();
	}
}
