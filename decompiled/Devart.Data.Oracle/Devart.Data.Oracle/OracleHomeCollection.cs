using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using Devart.Common;
using Microsoft.Win32;

namespace Devart.Data.Oracle;

[ListBindable(false)]
public class OracleHomeCollection : CollectionBase
{
	private OracleHome m_a;

	private static OracleHomeCollection m_b;

	internal static OracleHomeCollection SingletonInstance
	{
		get
		{
			if (OracleHomeCollection.m_b != null)
			{
				return OracleHomeCollection.m_b;
			}
			OracleHomeCollection oracleHomeCollection = new OracleHomeCollection();
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ORACLE\\ALL_HOMES");
			if (registryKey != null)
			{
				string[] subKeyNames = registryKey.GetSubKeyNames();
				foreach (string text in subKeyNames)
				{
					RegistryKey registryKey2 = registryKey.OpenSubKey(text);
					RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ORACLE\\HOME" + text.Substring(2));
					string a_;
					if (registryKey3 != null)
					{
						a_ = (string)registryKey3.GetValue("TNS_ADMIN");
						registryKey3.Close();
					}
					else
					{
						a_ = "";
					}
					oracleHomeCollection.a(new OracleHome((string)registryKey2.GetValue("NAME"), (string)registryKey2.GetValue("NLS_LANG"), (string)registryKey2.GetValue("PATH"), a_));
				}
				registryKey.Close();
			}
			RegistryKey registryKey4 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ORACLE");
			if (registryKey4 != null)
			{
				string[] subKeyNames2 = registryKey4.GetSubKeyNames();
				foreach (string text2 in subKeyNames2)
				{
					if (text2.StartsWith("KEY_"))
					{
						RegistryKey registryKey5 = registryKey4.OpenSubKey(text2);
						string a_2 = (string)registryKey5.GetValue("ORACLE_HOME_NAME");
						string a_3 = (string)registryKey5.GetValue("NLS_LANG");
						string text3 = (string)registryKey5.GetValue("ORACLE_HOME");
						string a_4 = (string)registryKey5.GetValue("TNS_ADMIN");
						if (text3 != null && text3 != "")
						{
							oracleHomeCollection.a(new OracleHome(a_2, a_3, text3, a_4));
						}
					}
				}
				registryKey4.Close();
			}
			string text4 = ".;" + Environment.GetEnvironmentVariable("PATH");
			string[] array = text4.Split(new char[1] { ';' });
			for (int num3 = 0; num3 < array.Length; num3++)
			{
				try
				{
					string text5 = array[num3];
					if (text5 == "")
					{
						continue;
					}
					if (text5[text5.Length - 1] == '\\')
					{
						text5 = text5.Substring(0, text5.Length - 1);
					}
					string text6 = Path.Combine(text5, "Oci.dll");
					if (!File.Exists(text6))
					{
						continue;
					}
					for (int num4 = 0; num4 < oracleHomeCollection.Count; num4++)
					{
						if (string.Compare(text5, Path.Combine(oracleHomeCollection[num4].Path, "bin"), ignoreCase: true, CultureInfo.InvariantCulture) == 0)
						{
							oracleHomeCollection.m_a = oracleHomeCollection[num4];
							return OracleHomeCollection.m_b = oracleHomeCollection;
						}
						if (string.Compare(text5, oracleHomeCollection[num4].Path, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
						{
							oracleHomeCollection.m_a = oracleHomeCollection[num4];
							return OracleHomeCollection.m_b = oracleHomeCollection;
						}
					}
					bool? flag = Utils.UnmanagedDllIs64Bit(text6);
					if (!flag.HasValue || !((IntPtr.Size == 4) ^ flag.Value))
					{
						continue;
					}
					oracleHomeCollection.a(new OracleHome("", "", text5, ""));
					oracleHomeCollection.m_a = oracleHomeCollection[oracleHomeCollection.Count - 1];
					return OracleHomeCollection.m_b = oracleHomeCollection;
				}
				catch
				{
				}
			}
			if (oracleHomeCollection.Count == 0)
			{
				throw new InvalidOperationException(Devart.Common.al.a("CanNotObtainOracleClientFromRegistry"));
			}
			if (oracleHomeCollection.m_a == null)
			{
				oracleHomeCollection.m_a = oracleHomeCollection[oracleHomeCollection.Count - 1];
				return OracleHomeCollection.m_b = oracleHomeCollection;
			}
			return OracleHomeCollection.m_b = oracleHomeCollection;
		}
	}

	internal OracleHome this[int A_0] => (OracleHome)base.List[A_0];

	public OracleHome this[string name]
	{
		get
		{
			for (int num = 0; num < base.List.Count; num++)
			{
				if (string.Compare(((OracleHome)base.List[num]).Name, name, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
				{
					return (OracleHome)base.List[num];
				}
			}
			return null;
		}
	}

	public OracleHome DefaultHome => this.m_a;

	internal OracleHomeCollection()
	{
	}

	internal void a(OracleHome A_0)
	{
		base.List.Add(A_0);
	}

	internal int a(string A_0)
	{
		for (int num = 0; num < base.List.Count; num++)
		{
			if (string.Compare(((OracleHome)base.List[num]).Name, A_0, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				return num;
			}
		}
		return -1;
	}

	internal bool b(OracleHome A_0)
	{
		return base.List.Contains(A_0);
	}

	public bool Contains(string name)
	{
		bool result = false;
		for (int num = 0; num < base.List.Count; num++)
		{
			if (string.Compare(((OracleHome)base.List[num]).Name, name, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
			{
				return result;
			}
		}
		return result;
	}
}
