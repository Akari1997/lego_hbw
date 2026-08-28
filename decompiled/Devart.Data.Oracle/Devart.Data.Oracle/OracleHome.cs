using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Devart.Common;
using Microsoft.Win32;

namespace Devart.Data.Oracle;

public sealed class OracleHome
{
	private OracleGlobalization m_a;

	private readonly string m_b;

	private readonly string c;

	private readonly string d;

	private readonly string e;

	private string f;

	private Oci g;

	private static readonly Hashtable h = new Hashtable();

	// Ensure we can set DLL search directory so that oci.dll and its dependencies can be found
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	private static extern bool SetDllDirectory(string lpPathName);

	// Try to open ORACLE registry key preferring the 64-bit view first, then 32-bit, then fallback
	private static RegistryKey OpenOracleRegistryKey()
	{
		try
		{
			RegistryKey key64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
					.OpenSubKey("SOFTWARE\\ORACLE");
			if (key64 != null) return key64;
		}
		catch
		{
			// ignore and try 32-bit view
		}

		try
		{
			RegistryKey key32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
					.OpenSubKey("SOFTWARE\\ORACLE");
			if (key32 != null) return key32;
		}
		catch
		{
			// ignore
		}

		// fallback for older platforms / partial trust
		return Registry.LocalMachine.OpenSubKey("SOFTWARE\\ORACLE");
	}

	public string Name => this.m_b;

	public string NlsLang => c;

	public string Path => d;

	internal int ClientVersionNumber
	{
		get
		{
			string clientVersion = ClientVersion;
			int pos = 0;
			int num = Utils.TryParseInt(clientVersion, ref pos);
			pos++;
			int num2 = Utils.TryParseInt(clientVersion, ref pos);
			pos++;
			int num3 = Utils.TryParseInt(clientVersion, ref pos);
			pos++;
			int num4 = Utils.TryParseInt(clientVersion, ref pos);
			return ((num * 100 + num2) * 100 + num3) * 100 + num4;
		}
	}

	public string ClientVersion
	{
		get
		{
			if (f != null)
			{
				return f;
			}
			FileVersionInfo fileVersionInfo = null;
			string text = System.IO.Path.Combine(Path, "bin\\oci.dll");
			if (File.Exists(text))
			{
				fileVersionInfo = FileVersionInfo.GetVersionInfo(text);
			}
			else
			{
				text = System.IO.Path.Combine(Path, "oci.dll");
				if (File.Exists(text))
				{
					fileVersionInfo = FileVersionInfo.GetVersionInfo(text);
				}
			}
			if (fileVersionInfo == null || fileVersionInfo.FileVersion == null)
			{
				throw new FileNotFoundException(Devart.Common.al.a("CanNotLoadOciFromHome") + " " + Name + ".", "oci.dll");
			}
			string fileVersion = fileVersionInfo.FileVersion;
			int pos = 0;
			int num = Utils.TryParseInt(fileVersion, ref pos);
			pos++;
			int num2 = Utils.TryParseInt(fileVersion, ref pos);
			pos++;
			int num3 = Utils.TryParseInt(fileVersion, ref pos);
			pos++;
			int num4 = Utils.TryParseInt(fileVersion, ref pos);
			return f = string.Format("{0}.{1}.{2}.{3}", new object[4] { num, num2, num3, num4 });
		}
	}

	internal OracleHome(string A_0, string A_1, string A_2, string A_3)
	{
		if (A_0 == null)
		{
			A_0 = "";
		}
		if (A_1 == null)
		{
			A_1 = "";
		}
		if (A_2 == null)
		{
			A_2 = "";
		}
		this.m_b = A_0;
		c = A_1;
		if (A_2 != null && A_2 != "" && A_2.EndsWith("\\"))
		{
			A_2 = A_2.Substring(0, A_2.Length - 1);
		}
		d = A_2;
		e = A_3;
	}

	public OracleGlobalization GetClientInfo()
	{
		OracleGlobalization oracleGlobalization = new OracleGlobalization();
		GetClientInfo(oracleGlobalization);
		return oracleGlobalization;
	}

	public void GetClientInfo(OracleGlobalization oraGlob)
	{
		if (this.m_a == null)
		{
			this.m_a = new OracleGlobalization();
			this.m_a.ClientCharacterSet = "US7ASCII";
			this.m_a.Currency = "$";
			this.m_a.DateFormat = OracleTimeStamp.a(DateTimeFormatInfo.CurrentInfo.ShortDatePattern, A_1: false);
			this.m_a.DateLanguage = "AMERICAN";
			this.m_a.DualCurrency = "$";
			this.m_a.ISOCurrency = "AMERICA";
			this.m_a.Language = "AMERICAN";
			this.m_a.NCharConversionException = true;
			this.m_a.NumericCharacters = $"{NumberFormatInfo.CurrentInfo.NumberDecimalSeparator}{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}";
			this.m_a.Territory = "AMERICA";
			this.m_a.TimeStampFormat = OracleTimeStamp.a(DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.LongTimePattern, A_1: false);
			this.m_a.TimeStampTZFormat = OracleTimeStamp.a(DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.LongTimePattern, A_1: true);
			TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
			int hours = utcOffset.Hours;
			int minutes = utcOffset.Minutes;
			string text = "";
			text = ((hours >= 0 && (hours != 0 || minutes >= 0)) ? (text + "+") : (text + "-"));
			text = text + Math.Abs(hours).ToString("00") + ":" + Math.Abs(minutes).ToString("00");
			this.m_a.TimeZone = text;
			RegistryKey registryKey = OpenOracleRegistryKey();
			if (registryKey != null)
			{
				string[] subKeyNames = registryKey.GetSubKeyNames();
				foreach (string name in subKeyNames)
				{
					RegistryKey registryKey2 = registryKey.OpenSubKey(name);
					string text2 = (string)registryKey2.GetValue("ORACLE_HOME_NAME");
					if (text2 == null || !(text2 == Name))
					{
						continue;
					}
					string text3 = (string)registryKey2.GetValue("NLS_LANG");
					if (text3 != null)
					{
						int num2 = text3.IndexOf('_');
						int num3 = text3.IndexOf('.');
						if (num2 >= 0 && num3 >= 0 && num2 < num3)
						{
							this.m_a.Language = text3.Substring(0, num2);
							this.m_a.Territory = text3.Substring(num2 + 1, num3 - num2 - 1);
							this.m_a.ClientCharacterSet = text3.Substring(num3 + 1);
						}
					}
					string text4 = (string)registryKey2.GetValue("NLS_DATE_FORMAT");
					if (text4 != null)
					{
						this.m_a.DateFormat = text4;
					}
					string text5 = (string)registryKey2.GetValue("NLS_DATE_LANGUAGE");
					if (text5 != null)
					{
						this.m_a.DateLanguage = text5;
					}
					else
					{
						this.m_a.DateLanguage = this.m_a.Language;
					}
					string text6 = (string)registryKey2.GetValue("NLS_TIMESTAMP_FORMAT");
					if (text6 != null)
					{
						this.m_a.TimeStampFormat = text6;
					}
					string text7 = (string)registryKey2.GetValue("NLS_TIMESTAMP_TZ_FORMAT");
					if (text7 != null)
					{
						this.m_a.TimeStampTZFormat = text7;
					}
					string text8 = (string)registryKey2.GetValue("NLS_CURRENCY");
					if (text8 != null)
					{
						this.m_a.Currency = text8;
					}
					string text9 = (string)registryKey2.GetValue("NLS_DUAL_CURRENCY");
					if (text9 != null)
					{
						this.m_a.DualCurrency = text9;
					}
					break;
				}
				registryKey.Close();
			}
			this.m_a.e();
		}
		oraGlob.a(this.m_a);
	}

	public string[] GetServerList()
	{
		string text = null;
		string value = null;
		text = Environment.GetEnvironmentVariable("TNS_ADMIN");
		text = ((text == null) ? string.Empty : ((!(text != string.Empty) || text[text.Length - 1] == '\\') ? (text + "tnsnames.ora") : (text + '\\' + "tnsnames.ora")));
		if (File.Exists(text))
		{
			StreamReader streamReader = null;
			try
			{
				streamReader = new StreamReader(text);
				value = streamReader.ReadToEnd();
			}
			finally
			{
				streamReader?.Close();
			}
		}
		else
		{
			_ = Name;
			text = e;
			text = ((text == null) ? "tnsnames.ora" : ((!(text != string.Empty) || text[text.Length - 1] == '\\') ? (text + "tnsnames.ora") : (text + '\\' + "tnsnames.ora")));
			if (File.Exists(text))
			{
				StreamReader streamReader2 = null;
				try
				{
					streamReader2 = new StreamReader(text);
					value = streamReader2.ReadToEnd();
				}
				finally
				{
					streamReader2?.Close();
				}
			}
			else
			{
				text = Path + "\\net80\\admin" + '\\' + "tnsnames.ora";
				if (!File.Exists(text))
				{
					text = Path + "\\network\\admin" + '\\' + "tnsnames.ora";
				}
				if (!File.Exists(text))
				{
					text = string.Empty;
				}
				if (text == string.Empty)
				{
					throw new InvalidOperationException(Devart.Common.al.a("CanNotFindTnsnames"));
				}
				StreamReader streamReader3 = null;
				try
				{
					streamReader3 = new StreamReader(text);
					value = streamReader3.ReadToEnd();
				}
				finally
				{
					streamReader3?.Close();
				}
			}
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add(value);
		StringCollection stringCollection = a(arrayList);
		if (stringCollection.Count != 0)
		{
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}
		return new string[0];
	}

	private static StringCollection a(ArrayList A_0)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("(", 1001);
		hashtable.Add(")", 1002);
		StringCollection stringCollection = new StringCollection();
		for (int num = 0; num < A_0.Count; num++)
		{
			string text = (string)A_0[num];
			Lexer lexer = new Lexer(text, hashtable, new Hashtable(), LexerBehavior.OmitBlank | LexerBehavior.OmitComment);
			lexer.CultureInfo = CultureInfo.InvariantCulture;
			lexer.InlineComments = new string[1] { "#" };
			TokenType tokenType = TokenType.Begin;
			int num2 = 0;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			do
			{
				if (num2 == 0 && (tokenType == TokenType.Identifier || text2 == "-"))
				{
					text4 = ((!(text3 == ".")) ? (text4 + text2) : (text4 + "." + text2));
				}
				text3 = text2;
				Token nextToken = lexer.GetNextToken();
				text2 = nextToken.ToString();
				tokenType = nextToken.Type;
				switch (text2)
				{
				case "(":
					num2++;
					break;
				case ")":
					num2--;
					break;
				case "=":
					if (num2 != 0)
					{
						break;
					}
					if (text4.ToUpper(CultureInfo.InvariantCulture) == "IFILE")
					{
						string text5 = a(lexer);
						nextToken = lexer.Current;
						text2 = nextToken.ToString();
						tokenType = nextToken.Type;
						if (text5 != null && File.Exists(text5))
						{
							StreamReader streamReader = null;
							try
							{
								streamReader = new StreamReader(text5);
								string text6 = streamReader.ReadToEnd();
								bool flag = false;
								for (int num3 = 0; num3 < A_0.Count; num3++)
								{
									if (string.Compare((string)A_0[num3], text6) == 0)
									{
										flag = true;
									}
								}
								if (!flag)
								{
									A_0.Add(text6);
								}
							}
							finally
							{
								streamReader?.Close();
							}
						}
					}
					else
					{
						stringCollection.Add(text4);
					}
					text4 = string.Empty;
					break;
				}
			}
			while (tokenType != TokenType.End);
		}
		return stringCollection;
	}

	private static string a(Lexer A_0)
	{
		Token token = A_0.Current;
		StringBuilder stringBuilder = new StringBuilder();
		int num = token.EndPosition;
		for (string text = A_0.Text; num < text.Length; num++)
		{
			char c2 = text[num];
			if (c2 == '\n')
			{
				break;
			}
			stringBuilder.Append(c2);
		}
		int lineNumber = token.LineNumber;
		while (lineNumber == token.LineNumber && token.Type != TokenType.End)
		{
			token = A_0.GetNextToken();
		}
		return stringBuilder.ToString().Trim();
	}

	internal Oci b()
	{
		if (g != null)
		{
			return g;
		}
		string text = System.IO.Path.Combine(Path, "bin\\oci.dll");
		if (!File.Exists(text))
		{
			text = System.IO.Path.Combine(Path, "oci.dll");
			if (!File.Exists(text))
			{
				throw new FileNotFoundException(Devart.Common.al.a("CanNotLoadOciFromHome") + " " + Name + ".", "oci.dll");
			}
		}
		int num = ClientVersionNumber / 1000000;
		if (num < 10)
		{
			string text2 = System.IO.Path.GetDirectoryName(text);
			if (text2 == "")
			{
				text2 = ".";
			}
			string fullPath = System.IO.Path.GetFullPath(text2);
			string environmentVariable = Environment.GetEnvironmentVariable("PATH");
			string[] array = environmentVariable.Split(new char[1] { ';' });
			bool flag = false;
			foreach (string text3 in array)
			{
				if (text3 == "")
				{
					continue;
				}
				if (text3[text3.Length - 1] == '\\')
				{
					if (string.Compare(fullPath + "\\", text3, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
					{
						flag = true;
						break;
					}
				}
				else if (string.Compare(fullPath, text3, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw new InvalidOperationException(Devart.Common.al.a("CanNotLoadOracleClient"));
			}
		}
		g = b(text, num);
		return g;
	}

	private static Oci b(string A_0, int A_1)
	{
		Oci oci = (Oci)h[A_0];
		if (oci == null)
		{
			oci = a(A_0, A_1);
			h[A_0] = oci;
		}
		return oci;
	}

	private static Oci a(string A_0, int A_1)
	{
		AppDomain domain = Thread.GetDomain();
		string name = "OciCall.dll";
		AssemblyName assemblyName = new AssemblyName();
		assemblyName.Name = name;
		AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
		ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(name);
		Type typeFromHandle = typeof(Oci);
		TypeBuilder typeBuilder = moduleBuilder.DefineType("OciDynamicType", TypeAttributes.Public, typeFromHandle);
		Type typeFromHandle2 = typeof(SuppressUnmanagedCodeSecurityAttribute);
		typeBuilder.SetCustomAttribute(typeFromHandle2.GetConstructors()[0], new byte[0]);
		ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
		ILGenerator iLGenerator = constructorBuilder.GetILGenerator();
		iLGenerator.Emit(OpCodes.Ldarg_0);
		Type typeFromHandle3 = typeof(object);
		iLGenerator.Emit(OpCodes.Call, typeFromHandle3.GetConstructors()[0]);
		iLGenerator.Emit(OpCodes.Ret);
		Type typeFromHandle4 = typeof(PreserveSigAttribute);
		ConstructorInfo con = typeFromHandle4.GetConstructors()[0];
		string text = System.IO.Path.GetDirectoryName(A_0);
		if (text == null)
		{
			text = "";
		}

		// Ensure the oci.dll directory is in the process DLL search path so dependent native DLLs can be resolved
		try
		{
			if (!string.IsNullOrEmpty(text))
			{
				SetDllDirectory(text);
			}
		}
		catch
		{
			// Non-fatal: if we cannot set the DLL directory, the original behavior will still be attempted
		}

		Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
		MethodInfo[] methods = typeFromHandle.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
		foreach (MethodInfo methodInfo in methods)
		{
			if (!methodInfo.IsAbstract)
			{
				continue;
			}
			string text2 = A_0;
			string entryName = methodInfo.Name;
			object[] customAttributes = methodInfo.GetCustomAttributes(typeof(Devart.Common.ar), inherit: false);
			Devart.Common.ar ar2 = null;
			if (customAttributes.Length > 0)
			{
				ar2 = customAttributes[0] as Devart.Common.ar;
			}
			if (ar2 != null)
			{
				if (dictionary.TryGetValue(ar2.a(), out var value))
				{
					text2 = value;
				}
				else
				{
					string text3 = string.Format(ar2.a(), A_1);
					text2 = System.IO.Path.Combine(text, text3);
					if (!File.Exists(text2))
					{
						string text4 = System.IO.Path.Combine(text, "bin\\" + text3);
						if (File.Exists(text4))
						{
							text2 = text4;
						}
					}
					dictionary.Add(ar2.a(), text2);
				}
			}
			customAttributes = methodInfo.GetCustomAttributes(typeof(Devart.Common.q), inherit: false);
			Devart.Common.q q2 = null;
			if (customAttributes.Length > 0)
			{
				q2 = customAttributes[0] as Devart.Common.q;
			}
			if (q2 != null)
			{
				entryName = q2.a();
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int num2 = 0; num2 < parameters.Length; num2++)
			{
				array[num2] = parameters[num2].ParameterType;
			}
			MethodBuilder methodBuilder = typeBuilder.DefinePInvokeMethod("native" + methodInfo.Name, text2, entryName, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig, Call[...]
			methodBuilder.SetCustomAttribute(con, new byte[0]);
			MethodBuilder methodBuilder2 = typeBuilder.DefineMethod(methodInfo.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, methodInfo.CallingConvention, methodIn[...]
			ILGenerator iLGenerator2 = methodBuilder2.GetILGenerator();
			for (int num3 = 0; num3 < parameters.Length; num3++)
			{
				switch (num3)
				{
				case 0:
					iLGenerator2.Emit(OpCodes.Ldarg_1);
					break;
				case 1:
					iLGenerator2.Emit(OpCodes.Ldarg_2);
					break;
				case 2:
					iLGenerator2.Emit(OpCodes.Ldarg_3);
					break;
				default:
					iLGenerator2.Emit(OpCodes.Ldarg_S, (byte)(num3 + 1));
					break;
				}
			}
			iLGenerator2.EmitCall(OpCodes.Call, methodBuilder, null);
			iLGenerator2.Emit(OpCodes.Ret);
		}
		Type type = typeBuilder.CreateType();
		Oci oci = (Oci)Activator.CreateInstance(type);
		oci.ociDllPath = A_0;
		return oci;
	}
}
