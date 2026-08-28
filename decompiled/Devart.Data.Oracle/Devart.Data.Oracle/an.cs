using System.Collections;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class an
{
	public const int a = 3001;

	public const int b = 3002;

	public const int c = 3003;

	public const int d = 3004;

	public const int e = 3005;

	public const int f = 3006;

	public const int g = 3007;

	public const int h = 3008;

	public const int i = 3009;

	public const int j = 3010;

	public const int k = 3011;

	public const int l = 3012;

	public const int m = 3013;

	public const int n = 3015;

	public const int o = 3016;

	public const int p = 3017;

	public const int q = 3019;

	public const int r = 3020;

	public const int s = 3021;

	public const int t = 3022;

	public const int u = 3023;

	public const int v = 3024;

	public const int w = 3025;

	public const int x = 3026;

	public const int y = 3027;

	public const int z = 3028;

	public const int aa = 3029;

	public const int ab = 3030;

	public const int ac = 3031;

	public const int ad = 3032;

	public const int ae = 3033;

	public const int af = 3034;

	public const int ag = 3035;

	public const int ah = 3036;

	public const int ai = 3037;

	public const int aj = 3039;

	public const int ak = 3040;

	public static Hashtable al;

	public static Hashtable am;

	static an()
	{
		al = new Hashtable(Devart.Common.ag.@as);
		am = new Hashtable(Devart.Common.ag.at);
		am.Add("SYSDATE", 3001);
		am.Add("USER", 3002);
		am.Add("SYSTIMESTAMP", 3003);
		am.Add("UID", 3004);
		am.Add("START", 3005);
		am.Add("CONNECT", 3006);
		am.Add("BEGIN", 3007);
		am.Add("END", 3008);
		am.Add("DECLARE", 3009);
		am.Add("PROCEDURE", 3010);
		am.Add("FUNCTION", 3011);
		am.Add("PACKAGE", 3012);
		am.Add("TRIGGER", 3013);
		am.Add("REPLACE", 3015);
		am.Add("BODY", 3016);
		am.Add("TYPE", 3017);
		am.Add("TIMESTAMP", 3019);
		am.Add("COLUMN", 3020);
		am.Add("TABLE", 3021);
		am.Add("INDEX", 3022);
		am.Add("COMMENT", 3023);
		am.Add("CHAR", 3024);
		am.Add("VARCHAR", 3025);
		am.Add("VARCHAR2", 3025);
		am.Add("LONG", 3026);
		am.Add("NUMBER", 3027);
		am.Add("DATE", 3028);
		am.Add("RAW", 3029);
		am.Add("ROWNUM", 3031);
		am.Add("ORA_ROWSCN", 3032);
		am.Add("LEVEL", 3033);
		am.Add("CONNECT_BY_ISCYCLE", 3034);
		am.Add("CONNECT_BY_ISLEAF", 3035);
		am.Add("PIVOT", 3036);
		am.Add("UNPIVOT", 3037);
		am.Add("INTERSECT", 3039);
		am.Add("MINUS", 3040);
	}

	public static bool a(string A_0)
	{
		switch (A_0)
		{
		case "TYPE":
		case "DECLARE":
		case "SYSTIMESTAMP":
		case "BEGIN":
		case "PROCEDURE":
		case "FUNCTION":
		case "PACKAGE":
		case "BODY":
		case "TIMESTAMP":
		case "ORA_ROWSCN":
		case "CONNECT_BY_ISCYCLE":
		case "CONNECT_BY_ISLEAF":
			return true;
		default:
			return false;
		}
	}
}
