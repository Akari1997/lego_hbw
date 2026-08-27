using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Devart.Common;

internal class al
{
	internal const string a = "PoolTimeoutExpired";

	internal const string b = "MissingSourceCommand";

	internal const string c = "MissingSourceCommandConnection";

	internal const string d = "DynamicSQLNoTableInfo";

	internal const string e = "DynamicSQLJoinUnsupported";

	internal const string f = "DynamicSqlGenerationNotSupp";

	internal const string g = "AdapterNotInited";

	internal const string h = "ConnectionNotInit";

	internal const string i = "SelectCommandNotInit";

	internal const string j = "ConnNotOpen";

	internal const string k = "InvalidDbMonitorVersion";

	internal const string l = "ParamNameMissing";

	internal const string m = "ParamValueMissing";

	internal const string n = "InvalidChar";

	internal const string o = "UnknownParameter";

	internal const string p = "ConnectionStateNotSupported";

	internal const string q = "ParametersUniqueName";

	internal const string r = "ParameterNull";

	internal const string s = "InvalidParameterType";

	internal const string t = "ParametersIsNotParent";

	internal const string u = "ParametersIsParent";

	internal const string v = "ParametersMappingIndex";

	internal const string w = "ParametersSourceIndex";

	internal const string x = "ParametersRemoveInvalidObject";

	internal const string y = "CannotConvert";

	internal const string z = "UnknownType";

	internal const string aa = "ReaderNotClosed";

	internal const string ab = "IncorrectFormat";

	internal const string ac = "ConnectionAlreadyOpen";

	internal const string ad = "DelegatedTransactionPresent";

	internal const string ae = "ClosedConnectionError";

	internal const string af = "OpenConnectionStringSet";

	internal const string ag = "PooledOpenTimeout";

	internal const string ah = "ParameterNameMissing";

	internal const string ai = "ParameterValueMissing";

	internal const string aj = "OutParameterValueMissing";

	internal const string ak = "InvalidConnectionString";

	internal const string al = "UnknownConnectionStringParameter";

	internal const string am = "InvalidCommandTimeout";

	internal const string an = "InvalidCommandType";

	internal const string ao = "InvalidUpdateRowSource";

	internal const string ap = "ConvertFailed";

	internal const string aq = "DataReaderNoData";

	internal const string ar = "DataReaderClosed";

	internal const string @as = "InvalidSourceBufferIndex";

	internal const string at = "InvalidDestinationBufferIndex";

	internal const string au = "InvalidDestinationBufferIndexOrOffset";

	internal const string av = "InvalidBufferSizeOrIndex";

	internal const string aw = "IndexOutOfRange";

	internal const string ax = "InvalidParameterDirection";

	internal const string ay = "InvalidOffsetValue";

	internal const string az = "InvalidSizeValue";

	internal const string a0 = "InvalidDataRowVersion";

	internal const string a1 = "InvalidDataLength";

	internal const string a2 = "CollectionRemoveInvalidObject";

	internal const string a3 = "DbException";

	internal const string a4 = "KeywordNotSupported";

	internal const string a5 = "RequestedValueNotFound";

	internal const string a6 = "InvalidConnectionOptionValue";

	internal const string a7 = "InvalidMinMaxPoolSizeValues";

	internal const string a8 = "InternalConnectionWithoutProxy";

	internal const string a9 = "ConnectionStringNotInitialized";

	internal const string ba = "CommandCannotBeNull";

	internal const string bb = "IdentifierIsNotQuoted";

	internal const string bc = "CatalogSeparatorNotSupported";

	internal const string bd = "SchemaSeparatorNotSupported";

	internal const string be = "NoQuoteChange";

	internal const string bf = "ConnMustOpen";

	internal const string bg = "RequestedCollectionNotDefined";

	internal const string bh = "ExecutionInProgress";

	internal const string bi = "PooledObjectHasOwner";

	internal const string bj = "PooledObjectInPoolMoreThanOnce";

	internal const string bk = "NonPooledObjectUsedMoreThanOnce";

	internal const string bl = "UnpooledObjectHasOwner";

	internal const string bm = "UnpooledObjectHasWrongOwner";

	internal const string bn = "PushingObjectSecondTime";

	internal const string bo = "GetConnectionReturnsNull";

	internal const string bp = "ConnectionOptionsMissing";

	internal const string bq = "ConnectionPoolOptionsMissing";

	internal const string br = "CommandTextRequired";

	internal const string bs = "TableNameNotDef";

	internal const string bt = "ProcNameNotDef";

	internal const string bu = "DuplicateStringParameter";

	internal const string bv = "DbCommandBuilder_AllRefreshNotSupported";

	internal const string bw = "Data is Null. This method or property cannot be called on Null values.";

	internal const string bx = "MissingSchemaActionNotSupported";

	internal const string by = "SslConnectionIsNotAllowed";

	internal const string bz = "DataAdapterMissingSourceCommand";

	internal const string b0 = "TransactionIsolationLevelNotSupported";

	internal const string b1 = "StreamAlreadyClosed";

	internal const string b2 = "StreamNotOpened";

	internal const string b3 = "StreamNoRead";

	internal const string b4 = "StreamNoWrite";

	internal const string b5 = "ReadFromStreamFailed";

	internal const string b6 = "WriteToStreamFailed";

	internal const string b7 = "Timeout_Exception";

	internal const string b8 = "YouMustSetupDumpTextProperty";

	internal const string b9 = "QueryShouldSelectDataFromOneTable";

	internal const string ca = "TransactionNotDisposed";

	internal const string cb = "InvalidCast_FromTo";

	internal const string cc = "DbDump_ExecutionInProgress";

	internal const string cd = "DbMonitor_Host";

	internal const string ce = "DbMonitor_Port";

	internal const string cf = "DbMonitor_UseApp";

	internal const string cg = "DbMonitor_EventQueueLimit";

	internal const string ch = "DbMonitor_NotSupportApp";

	internal const string ci = "InvalidSshAuthenticationType";

	internal const string cj = "ArgumentOutOfRangeNegativeValue";

	internal const string ck = "NonSequentialColumnAccess";

	internal const string cl = "NonSeqByteAccess";

	internal const string cm = "NonCharColumn";

	internal const string cn = "NonBlobColumn";

	private static ResourceManager co;

	private static ResourceManager cp;

	private static CultureInfo cq;

	static al()
	{
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
		string text = ".Strings.resources";
		string text2 = ".Common.resources";
		cq = CultureInfo.InvariantCulture;
		string[] array = manifestResourceNames;
		foreach (string text3 in array)
		{
			if (co == null && text3.Length > text.Length && text3.Substring(text3.Length - text.Length) == text)
			{
				co = new ResourceManager(text3.Substring(0, text3.Length - 10), executingAssembly);
			}
			if (cp == null && text3.Length > text2.Length && text3.Substring(text3.Length - text2.Length) == text2)
			{
				cp = new ResourceManager(text3.Substring(0, text3.Length - 10), executingAssembly);
			}
		}
	}

	internal static string a(string A_0)
	{
		string text = "";
		if (cp != null)
		{
			text = cp.GetString(A_0, cq);
		}
		if (Utils.IsEmpty(text) && co != null)
		{
			text = co.GetString(A_0, cq);
		}
		if (Utils.IsEmpty(text))
		{
			return A_0;
		}
		return text;
	}

	internal static string a(string A_0, object A_1)
	{
		return string.Format(a(A_0), A_1);
	}

	internal static string a(string A_0, object A_1, object A_2)
	{
		return string.Format(a(A_0), A_1, A_2);
	}

	internal static string a(string A_0, object A_1, object A_2, object A_3)
	{
		return string.Format(a(A_0), A_1, A_2, A_3);
	}
}
