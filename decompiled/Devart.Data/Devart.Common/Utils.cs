using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Devart.Common;

public sealed class Utils
{
	public enum a
	{
		a = 0,
		b = 467,
		c = 34404,
		d = 448,
		e = 3772,
		f = 332,
		g = 512,
		h = 36929,
		i = 614,
		j = 870,
		k = 1126,
		l = 496,
		m = 497,
		n = 358,
		o = 418,
		p = 419,
		q = 422,
		r = 424,
		s = 450,
		t = 361
	}

	public static bool DesignMode;

	private static bool? m_a;

	public static bool MonoDetected
	{
		get
		{
			if (!Utils.m_a.HasValue)
			{
				Utils.m_a = false;
				object[] customAttributes = typeof(DbConnection).Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: true);
				if (customAttributes != null && customAttributes.Length > 0)
				{
					Utils.m_a = ((AssemblyProductAttribute)customAttributes[0]).Product.IndexOf("MONO") >= 0;
				}
			}
			return Utils.m_a.Value;
		}
	}

	public static DataTable SortTable(DataTable table, string columnsName)
	{
		DataView dataView = new DataView(table);
		dataView.Sort = columnsName;
		DataTable dataTable = table.Clone();
		DataRow[] array = new DataRow[dataView.Count];
		for (int i = 0; i < dataView.Count; i++)
		{
			array[i] = dataView[i].Row;
		}
		AddRowsToTable(dataTable, array);
		return dataTable;
	}

	public static void AddRowsToTable(DataTable destTable, ICollection srcRows)
	{
		foreach (DataRow srcRow in srcRows)
		{
			DataRow dataRow2 = destTable.NewRow();
			dataRow2.ItemArray = srcRow.ItemArray;
			destTable.Rows.Add(dataRow2);
		}
	}

	public static void FilterTable(ref DataTable table, string filterExpr)
	{
		if (filterExpr != null)
		{
			DataView dataView = new DataView(table);
			dataView.RowFilter = filterExpr;
			table = table.Clone();
			DataRow[] array = new DataRow[dataView.Count];
			for (int i = 0; i < dataView.Count; i++)
			{
				array[i] = dataView[i].Row;
			}
			AddRowsToTable(table, array);
		}
	}

	public static bool Compare(string st1, string st2)
	{
		return CultureInfo.CurrentCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0;
	}

	public static bool CompareInvariant(string st1, string st2)
	{
		return CultureInfo.InvariantCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0;
	}

	public static bool Compare(string st1, string st2, bool ignoreCase)
	{
		if (ignoreCase)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0;
		}
		return st1 == st2;
	}

	public static bool CompareInvariant(string st1, string st2, bool ignoreCase)
	{
		if (ignoreCase)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0;
		}
		return CultureInfo.InvariantCulture.CompareInfo.Compare(st1, st2, CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0;
	}

	public static bool CompareSuffix(string source, string suffix, bool ignoreCase)
	{
		return CompareSuffix(source, suffix, ignoreCase, null);
	}

	public static bool CompareSuffix(string source, string suffix, bool ignoreCase, string[] excludeStrings)
	{
		if (excludeStrings != null)
		{
			for (int i = 0; i < excludeStrings.Length; i++)
			{
				if (!IsEmpty(excludeStrings[i]))
				{
					source = source.Replace(excludeStrings[i], "");
					suffix = suffix.Replace(excludeStrings[i], "");
				}
			}
		}
		CompareOptions compareOptions = CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
		if (ignoreCase)
		{
			compareOptions |= CompareOptions.IgnoreCase;
		}
		return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(source, suffix, compareOptions);
	}

	public static void CheckArgumentNull(object value, string parameterName)
	{
		if (value == null)
		{
			throw new ArgumentNullException(parameterName);
		}
	}

	public static void CheckArgumentNull(object value, string parameterName, string resMessage)
	{
		if (value == null)
		{
			throw new ArgumentNullException(parameterName, n.a(resMessage));
		}
	}

	public static void CheckConnectionOpen(IDbConnection connection)
	{
		if (connection == null)
		{
			throw new InvalidOperationException(n.a("ConnectionNotInit"));
		}
		if (connection.State != ConnectionState.Open)
		{
			throw new InvalidOperationException(n.a("ConnMustOpen"));
		}
	}

	public static Hashtable CreateHashtable(bool ignoreCase)
	{
		if (ignoreCase)
		{
			return new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		}
		return new Hashtable(StringComparer.InvariantCulture);
	}

	public static bool IsIpAddress(string hostname)
	{
		if (hostname == null || hostname == "")
		{
			return false;
		}
		foreach (char c2 in hostname)
		{
			if ((c2 < '0' || c2 > '9') && c2 != '.')
			{
				return false;
			}
		}
		return true;
	}

	public static bool IsEmpty(string st)
	{
		if (st != null)
		{
			return st.Length == 0;
		}
		return true;
	}

	public static bool IsEmpty(ICollection collection)
	{
		if (collection != null)
		{
			return collection.Count == 0;
		}
		return true;
	}

	public static bool IsNull(object val)
	{
		if (val != null)
		{
			return val == DBNull.Value;
		}
		return true;
	}

	public static bool IsNumber(string s)
	{
		if (!TryParse(s, NumberStyles.Number, null, out var result))
		{
			return TryParse(s, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out result);
		}
		return true;
	}

	public static bool IsBasicLetter(char c)
	{
		if (c < 'a' || c > 'z')
		{
			if (c >= 'A')
			{
				return c <= 'Z';
			}
			return false;
		}
		return true;
	}

	public static string ObjectToString(object obj)
	{
		if (obj == null)
		{
			return string.Empty;
		}
		return obj.ToString();
	}

	public static object Parse(string s, Type enumType)
	{
		return Parse(s, enumType, ignoreCase: true);
	}

	public static object Parse(string s, Type enumType, bool ignoreCase)
	{
		if (TryParse(s, out var value, enumType, ignoreCase))
		{
			return value;
		}
		throw new ArgumentException(n.a("RequestedValueNotFound", s));
	}

	public static bool TryParse(string s, out int i)
	{
		bool result = TryParse(s, NumberStyles.Integer, null, out var result2);
		i = (int)result2;
		return result;
	}

	public static bool TryParse(string s, out double d)
	{
		return TryParse(s, NumberStyles.Number, null, out d);
	}

	public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
	{
		return double.TryParse(s, style, provider, out result);
	}

	public static bool TryParse(string s, out bool b)
	{
		return TryParse(s, out b, ignoreCase: true);
	}

	public static bool TryParse(string s, out bool b, bool ignoreCase)
	{
		if (Compare(s, bool.TrueString, ignoreCase))
		{
			b = true;
			return true;
		}
		if (Compare(s, bool.FalseString, ignoreCase))
		{
			b = false;
			return true;
		}
		b = false;
		return false;
	}

	public static bool TryParse(string s, out object value, Type enumType)
	{
		return TryParse(s, out value, enumType, ignoreCase: true);
	}

	public static bool TryParse(string s, out object value, Type enumType, bool ignoreCase)
	{
		FieldInfo[] fields = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (string.Compare(s, fieldInfo.Name, ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture) == 0)
			{
				value = fieldInfo.GetValue(null);
				return true;
			}
		}
		value = null;
		return false;
	}

	public static int ParseIntWith0(string s)
	{
		int i;
		for (i = 0; i < s.Length && s[i] == '0'; i++)
		{
		}
		if (i > 0)
		{
			s = s.Substring(i);
		}
		if (s == "" || s == "0")
		{
			return 0;
		}
		return int.Parse(s);
	}

	public static int TryParseInt(string s, ref int pos)
	{
		int num = pos;
		int length = s.Length;
		while (pos < length && char.IsDigit(s[pos]))
		{
			pos++;
		}
		if (pos == num)
		{
			return 0;
		}
		return ParseIntWith0(s.Substring(num, pos - num));
	}

	public static bool TryGetValue(Hashtable dictionary, object key, out object val)
	{
		val = dictionary[key];
		if (val == null)
		{
			return false;
		}
		return true;
	}

	public static object GetWeakTarget(WeakReference weakReference)
	{
		try
		{
			if (weakReference == null || !weakReference.IsAlive)
			{
				return null;
			}
			return weakReference.Target;
		}
		catch
		{
			return null;
		}
	}

	public static void SetWeakTarget(ref WeakReference weakReference, object target)
	{
		if (target == null)
		{
			weakReference = null;
		}
		else
		{
			weakReference = new WeakReference(target);
		}
	}

	public static bool GetWeakIsAlive(WeakReference weakReference)
	{
		if (weakReference == null)
		{
			return false;
		}
		try
		{
			return weakReference.IsAlive;
		}
		catch
		{
			return false;
		}
	}

	public static byte[] GetMaxBytes(Encoding encoding, string s, out int byteCount)
	{
		int length = s.Length;
		byte[] array = new byte[encoding.GetMaxByteCount(length) + 1];
		byteCount = encoding.GetBytes(s, 0, length, array, 0);
		return array;
	}

	public static string[] SplitItems(string names, char[] quotes)
	{
		CheckArgumentNull(names, "names");
		ArrayList arrayList = new ArrayList();
		int num = 0;
		int length = names.Length;
		int num2 = 0;
		char c2 = '\0';
		char c3 = c2;
		char c4 = '\0';
		while (num < length)
		{
			c4 = names[num];
			if (c4 == ';' && c2 == '\0')
			{
				arrayList.Add(names.Substring(num2, num - num2));
				num2 = ++num;
				continue;
			}
			if (c2 == '\\')
			{
				c2 = c3;
			}
			else
			{
				bool flag = false;
				if (quotes != null)
				{
					for (int i = 0; i < quotes.Length; i++)
					{
						if (quotes[i] == c4)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					if (c2 == '\0')
					{
						c2 = c4;
					}
					else if (c2 == c4)
					{
						c2 = '\0';
					}
				}
			}
			num++;
		}
		arrayList.Add(names.Substring(num2, num - num2));
		return (string[])arrayList.ToArray(typeof(string));
	}

	public static bool WaitOne(WaitHandle waitHandle, TimeSpan timeout, bool exitContext)
	{
		return waitHandle.WaitOne(timeout, exitContext);
	}

	public static bool NeedQuote(string name, Hashtable keywords, char[] prefixes, char[] suffixes)
	{
		return NeedQuote(name, keywords, prefixes, suffixes, checkCase: false);
	}

	public static bool NeedQuote(string name, Hashtable keywords, char[] prefixes, char[] suffixes, bool checkCase)
	{
		CheckArgumentNull(name, "name");
		if (prefixes == null || suffixes == null)
		{
			return false;
		}
		char c2 = '\0';
		string text = name.Trim();
		if (keywords[ToUpperInvariant(text)] != null)
		{
			return true;
		}
		int length = text.Length;
		if (length > 0)
		{
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < prefixes.Length; i++)
			{
				if (text[0] == prefixes[i])
				{
					flag = true;
					break;
				}
			}
			for (int num = 0; num < suffixes.Length; num++)
			{
				if (text[length - 1] == suffixes[num])
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2 && !flag)
			{
				bool flag3 = true;
				for (int num2 = 0; num2 < length; num2++)
				{
					c2 = text[num2];
					if (checkCase && char.IsUpper(c2))
					{
						return true;
					}
					if (flag3 && !char.IsDigit(c2))
					{
						flag3 = false;
					}
					if (!char.IsLetterOrDigit(c2) && c2 != '_' && c2 != '$')
					{
						return true;
					}
				}
				if (flag3)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static string ToLowerInvariant(string value)
	{
		return value?.ToLower(CultureInfo.InvariantCulture);
	}

	public static string ToUpperInvariant(string value)
	{
		return value?.ToUpper(CultureInfo.InvariantCulture);
	}

	public static char ToLowerInvariant(char value)
	{
		return char.ToLower(value, CultureInfo.InvariantCulture);
	}

	public static char ToUpperInvariant(char value)
	{
		return char.ToUpper(value, CultureInfo.InvariantCulture);
	}

	public static bool ByteArrayEquals(byte[] value1, byte[] value2)
	{
		if (value1.Length != value2.Length)
		{
			return false;
		}
		for (int i = 0; i < value1.Length; i++)
		{
			if (value1[i] != value2[i])
			{
				return false;
			}
		}
		return true;
	}

	public static string TruncateVersion(string version, int count)
	{
		if (count > 4 || count <= 0)
		{
			throw new ArgumentException("The value 'count' must be greater than 0 and less than or equal to 4", "count");
		}
		if (!string.IsNullOrEmpty(version))
		{
			string[] array = version.Split(new char[1] { '.' });
			if (array.Length < count)
			{
				throw new InvalidOperationException($"It is impossible to truncate version to {count} parts because version has been truncated already.");
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count - 1; i++)
			{
				stringBuilder.Append(array[i]);
				stringBuilder.Append(".");
			}
			stringBuilder.Append(array[count - 1]);
			return stringBuilder.ToString();
		}
		return string.Empty;
	}

	public static a GetDllMachineType(string dllPath)
	{
		using FileStream fileStream = new FileStream(dllPath, FileMode.Open, FileAccess.Read);
		using BinaryReader binaryReader = new BinaryReader(fileStream);
		fileStream.Seek(60L, SeekOrigin.Begin);
		int num = binaryReader.ReadInt32();
		fileStream.Seek(num, SeekOrigin.Begin);
		uint num2 = binaryReader.ReadUInt32();
		if (num2 != 17744)
		{
			return Utils.a.a;
		}
		return (a)binaryReader.ReadUInt16();
	}

	public static bool? UnmanagedDllIs64Bit(string dllPath)
	{
		a dllMachineType;
		try
		{
			dllMachineType = GetDllMachineType(dllPath);
		}
		catch
		{
			return null;
		}
		switch (dllMachineType)
		{
		case Utils.a.g:
		case Utils.a.c:
			return true;
		case Utils.a.f:
			return false;
		default:
			return null;
		}
	}
}
