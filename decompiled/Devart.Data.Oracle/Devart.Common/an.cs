using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Devart.Common;

internal class an
{
	private const string m_a = ".";

	public static string a(string A_0, string A_1, IFormatProvider A_2)
	{
		int A_3 = -1;
		int num = 0;
		StringBuilder stringBuilder = new StringBuilder();
		int num2 = A_0.IndexOf(".");
		if (string.IsNullOrEmpty(A_1))
		{
			A_1 = "G";
		}
		NumberFormatInfo numberFormatInfo = ((A_2 != null) ? NumberFormatInfo.GetInstance(A_2) : NumberFormatInfo.CurrentInfo);
		if (A_0[num] == '-' || A_0[num] == '+')
		{
			num++;
		}
		string text;
		string text2;
		if (num2 > -1)
		{
			text = A_0.Substring(num, num2 - num);
			text2 = A_0.Substring(num2 + 1);
		}
		else
		{
			text = A_0.Substring(num, A_0.Length - num);
			text2 = string.Empty;
		}
		if (text.Length > 1 && text[0] == '0')
		{
			text = text.TrimStart(new char[1] { '0' });
		}
		if (text == string.Empty)
		{
			text = "0";
		}
		text2 = text2.TrimEnd(new char[1] { '0' });
		if (text2 == string.Empty)
		{
			num2 = -1;
		}
		bool flag = a(A_0);
		if (a(A_1, out var A_4, out A_3))
		{
			switch (A_4)
			{
			case 'G':
			case 'g':
			{
				int num3 = a(text, text2);
				if (A_3 < 1)
				{
					A_3 = int.MaxValue;
				}
				if (num3 > -5 && num3 < A_3)
				{
					stringBuilder.Append(a(text, text2, A_3, numberFormatInfo, A_4: true, flag));
				}
				else
				{
					stringBuilder.Append(a(text, text2, A_3, numberFormatInfo, A_4: true, A_4 == 'G', flag));
				}
				break;
			}
			case 'F':
			case 'f':
				if (A_3 < 0)
				{
					A_3 = numberFormatInfo.NumberDecimalDigits;
				}
				stringBuilder.Append(a(text, text2, A_3, numberFormatInfo, A_4: false, flag));
				break;
			case 'E':
			case 'e':
				if (A_3 < 0)
				{
					A_3 = 6;
				}
				stringBuilder.Append(a(text, text2, A_3, numberFormatInfo, A_4: false, A_4 == 'E', flag));
				break;
			case 'N':
			case 'n':
				if (A_3 < 0)
				{
					A_3 = numberFormatInfo.NumberDecimalDigits;
				}
				stringBuilder.Append(a(text, text2, A_3, numberFormatInfo, flag));
				break;
			case 'P':
			case 'p':
				if (A_3 < 0)
				{
					A_3 = numberFormatInfo.PercentDecimalDigits;
				}
				stringBuilder.Append(b(text, text2, A_3, numberFormatInfo, flag));
				break;
			case 'C':
			case 'c':
				if (A_3 < 0)
				{
					A_3 = numberFormatInfo.CurrencyDecimalDigits;
				}
				stringBuilder.Append(c(text, text2, A_3, numberFormatInfo, flag));
				break;
			default:
				throw new FormatException("Format specifier was invalid.");
			}
		}
		else
		{
			stringBuilder.Append(a(A_1, text, text2, A_3, numberFormatInfo, flag));
		}
		return stringBuilder.ToString();
	}

	private static string c(string A_0, int A_1, string A_2)
	{
		string text = "";
		bool flag = false;
		switch (A_1)
		{
		case 0:
			text = "({0})";
			flag = true;
			break;
		case 1:
			text = "{0}{1}";
			break;
		case 2:
			text = "{0} {1}";
			break;
		case 3:
			text = "{1}{0}";
			break;
		case 4:
			text = "{1} {0}";
			break;
		default:
			throw new ArgumentOutOfRangeException("numberNegativePattern", "Valid values are between 0 and 4, inclusive.");
		}
		if (flag)
		{
			return string.Format(text, A_0);
		}
		return string.Format(text, A_2, A_0);
	}

	private static string b(string A_0, int A_1, string A_2)
	{
		string text = "";
		return string.Format(A_1 switch
		{
			0 => "{0} {1}", 
			1 => "{0}{1}", 
			2 => "{1}{0}", 
			3 => "{1} {0}", 
			_ => throw new ArgumentOutOfRangeException("percentPositivePattern", "Valid values are between 0 and 3, inclusive."), 
		}, A_0, A_2);
	}

	private static string b(string A_0, int A_1, string A_2, string A_3)
	{
		string text = "";
		return string.Format(A_1 switch
		{
			0 => "{0}{1} {2}", 
			1 => "{0}{1}{2}", 
			2 => "{0}{2}{1}", 
			3 => "{2}{0}{1}", 
			4 => "{2}{1}{0}", 
			5 => "{1}{0}{2}", 
			6 => "{1}{2}{0}", 
			7 => "{0}{2} {1}", 
			8 => "{1} {2}{0}", 
			9 => "{2} {1}{0}", 
			10 => "{2} {0}{1}", 
			11 => "{1}{0} {2}", 
			_ => throw new ArgumentOutOfRangeException("percentNegativePattern", "Valid values are between 0 and 11, inclusive."), 
		}, A_3, A_0, A_2);
	}

	private static string a(string A_0, int A_1, string A_2)
	{
		string text = "";
		return string.Format(A_1 switch
		{
			0 => "{0}{1}", 
			1 => "{1}{0}", 
			2 => "{0} {1}", 
			3 => "{1} {0}", 
			_ => throw new ArgumentOutOfRangeException("currencyPositivePattern", "Valid values are between 0 and 3, inclusive."), 
		}, A_2, A_0);
	}

	private static string a(string A_0, int A_1, string A_2, string A_3)
	{
		string text = "";
		bool flag = false;
		switch (A_1)
		{
		case 0:
			text = "({0}{1})";
			flag = true;
			break;
		case 1:
			text = "{2}{0}{1}";
			break;
		case 2:
			text = "{0}{2}{1}";
			break;
		case 3:
			text = "{0}{1}{2}";
			break;
		case 4:
			text = "({1}{0})";
			break;
		case 5:
			text = "{2}{1}{0}";
			break;
		case 6:
			text = "{1}{2}{0}";
			break;
		case 7:
			text = "{1}{0}{2}";
			break;
		case 8:
			text = "{2}{1} {0}";
			break;
		case 9:
			text = "{2}{0} {1}";
			break;
		case 10:
			text = "{1} {0}{2}";
			break;
		case 11:
			text = "{0} {1}{2}";
			break;
		case 12:
			text = "{0} {2}{1}";
			break;
		case 13:
			text = "{1}{2} {0}";
			break;
		case 14:
			text = "({0} {1})";
			break;
		case 15:
			text = "({1} {0})";
			break;
		default:
			throw new ArgumentOutOfRangeException("currencyNegativePattern", "Valid values are between 0 and 15, inclusive.");
		}
		if (flag)
		{
			return string.Format(text, A_2, A_0);
		}
		return string.Format(text, A_2, A_0, A_3);
	}

	private static string c(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4)
	{
		byte b2 = 0;
		StringBuilder stringBuilder = new StringBuilder();
		if (A_2 > 0)
		{
			A_1 = ((A_2 > A_1.Length) ? (A_1 + b2.ToString($"D{A_2 - A_1.Length}")) : a(A_1, A_2, A_2: false));
		}
		else
		{
			A_0 = a(A_0 + A_1, A_0.Length, A_2: false);
			A_1 = string.Empty;
		}
		stringBuilder.Append(a(A_0, A_3.CurrencyGroupSizes, A_3.CurrencyGroupSeparator));
		if (A_1 != string.Empty)
		{
			stringBuilder.Append(A_3.CurrencyDecimalSeparator);
			stringBuilder.Append(A_1);
		}
		string a_ = stringBuilder.ToString();
		if (A_4 && !a(a_, A_3))
		{
			return a(a_, A_3.CurrencyNegativePattern, A_3.CurrencySymbol, A_3.NegativeSign);
		}
		return a(a_, A_3.CurrencyPositivePattern, A_3.CurrencySymbol);
	}

	private static string b(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4)
	{
		byte b2 = 0;
		StringBuilder stringBuilder = new StringBuilder();
		if (A_1.Length >= 2)
		{
			A_0 += A_1.Substring(0, 2);
			A_1 = A_1.Substring(2);
		}
		else
		{
			A_0 = A_0 + A_1 + b2.ToString($"D{2 - A_1.Length}");
			A_1 = string.Empty;
		}
		A_0 = A_0.TrimStart(new char[1] { '0' });
		if (A_0 == string.Empty)
		{
			A_0 = "0";
		}
		if (A_2 > 0)
		{
			A_1 = ((A_2 > A_1.Length) ? (A_1 + b2.ToString($"D{A_2 - A_1.Length}")) : a(A_1, A_2, A_2: false));
		}
		else
		{
			A_0 = a(A_0 + A_1, A_0.Length, A_2: false);
			A_1 = string.Empty;
		}
		stringBuilder.Append(a(A_0, A_3.PercentGroupSizes, A_3.PercentGroupSeparator));
		if (A_1 != string.Empty)
		{
			stringBuilder.Append(A_3.PercentDecimalSeparator);
			stringBuilder.Append(A_1);
		}
		string a_ = stringBuilder.ToString();
		if (A_4 && !a(a_, A_3))
		{
			return b(a_, A_3.PercentNegativePattern, A_3.PercentSymbol, A_3.NegativeSign);
		}
		return b(a_, A_3.PercentPositivePattern, A_3.PercentSymbol);
	}

	private static string a(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4)
	{
		byte b2 = 0;
		StringBuilder stringBuilder = new StringBuilder();
		if (A_2 > 0)
		{
			A_1 = ((A_2 > A_1.Length) ? (A_1 + b2.ToString($"D{A_2 - A_1.Length}")) : a(A_1, A_2, A_2: false));
		}
		else
		{
			A_0 = a(A_0 + A_1, A_0.Length, A_2: false);
			A_1 = string.Empty;
		}
		stringBuilder.Append(a(A_0, A_3.NumberGroupSizes, A_3.NumberGroupSeparator));
		if (A_1 != string.Empty)
		{
			stringBuilder.Append(A_3.PercentDecimalSeparator);
			stringBuilder.Append(A_1);
		}
		string text = stringBuilder.ToString();
		if (A_4 && !a(text, A_3))
		{
			return c(text, A_3.NumberNegativePattern, A_3.NegativeSign);
		}
		return text;
	}

	private static string a(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4, bool A_5, bool A_6)
	{
		StringBuilder stringBuilder = new StringBuilder();
		char c2 = (A_5 ? 'E' : 'e');
		int num = 0;
		string positiveSign = A_3.PositiveSign;
		string negativeSign = A_3.NegativeSign;
		byte b2 = 0;
		if (A_4)
		{
			if (A_0 == "0")
			{
				string text = A_1.TrimStart(new char[1] { '0' });
				num = A_1.Length - text.Length + 1;
				text = a(text, Math.Min(A_2, text.Length), A_2: true);
				stringBuilder.Append(text[0]);
				if (text.Length > 1)
				{
					stringBuilder.Append(A_3.NumberDecimalSeparator);
					stringBuilder.Append(text.Substring(1));
				}
				stringBuilder.Append(c2 + negativeSign + num.ToString("D2"));
			}
			else
			{
				num = A_0.Length - 1;
				A_0 = a(A_0, A_2, A_2: true);
				stringBuilder.Append(A_0[0]);
				if (A_0.Length > 1)
				{
					stringBuilder.Append(A_3.NumberDecimalSeparator);
					stringBuilder.Append(A_0.Substring(1));
				}
				stringBuilder.Append(c2 + positiveSign + num.ToString("D2"));
			}
		}
		else
		{
			num = a(A_0, A_1);
			string text;
			string text2;
			if (num >= 0)
			{
				text = A_0 + A_1;
				text2 = positiveSign;
			}
			else
			{
				num = -num;
				text = A_1.Substring(num - 1);
				text2 = negativeSign;
			}
			if (A_2 > 0)
			{
				stringBuilder.Append(text[0]);
				stringBuilder.Append(A_3.NumberDecimalSeparator);
				if (A_2 <= text.Length - 1)
				{
					stringBuilder.Append(a(text.Substring(1), A_2, A_2: false));
				}
				else
				{
					stringBuilder.Append(text.Substring(1));
					stringBuilder.Append(b2.ToString($"D{A_2 - text.Length + 1}"));
				}
				stringBuilder.Append(c2 + text2 + num.ToString("D3"));
			}
			else
			{
				stringBuilder.Append(a(text, 1, A_2: false));
				stringBuilder.Append(c2 + text2 + num.ToString("D3"));
			}
		}
		string text3 = stringBuilder.ToString();
		if (A_6 && !a(text3, A_3))
		{
			return c(text3, NumberFormatInfo.InvariantInfo.NumberNegativePattern, A_3.NegativeSign);
		}
		return text3;
	}

	private static string a(string A_0, string A_1, int A_2, NumberFormatInfo A_3, bool A_4, bool A_5)
	{
		StringBuilder stringBuilder = new StringBuilder();
		byte b2 = 0;
		if (A_4)
		{
			if (A_0 == "0")
			{
				stringBuilder.Append(A_0);
				string text = A_1.TrimStart(new char[1] { '0' });
				if (text != string.Empty)
				{
					stringBuilder.Append(A_3.NumberDecimalSeparator);
					stringBuilder.Append(A_1.Substring(0, A_1.Length - text.Length));
					stringBuilder.Append(a(text, Math.Min(A_2, text.Length), A_2: true));
				}
			}
			else if (A_2 > A_0.Length)
			{
				stringBuilder.Append(A_0);
				if (A_1.Length > 0)
				{
					A_1 = a(A_1, Math.Min(A_2 - A_0.Length, A_1.Length), A_2: true);
				}
				if (A_1.Length > 0)
				{
					stringBuilder.Append(A_3.NumberDecimalSeparator);
					stringBuilder.Append(A_1);
				}
			}
			else
			{
				stringBuilder.Append(a(A_0 + A_1, A_2, A_2: false));
			}
		}
		else if (A_2 > 0)
		{
			stringBuilder.Append(A_0);
			stringBuilder.Append(A_3.NumberDecimalSeparator);
			if (A_2 <= A_1.Length)
			{
				stringBuilder.Append(a(A_1, A_2, A_2: false));
			}
			else
			{
				stringBuilder.Append(A_1);
				stringBuilder.Append(b2.ToString($"D{A_2 - A_1.Length}"));
			}
		}
		else
		{
			stringBuilder.Append(a(A_0 + A_1, A_0.Length, A_2: false));
		}
		string text2 = stringBuilder.ToString();
		if (A_5 && !a(text2, A_3))
		{
			return c(text2, NumberFormatInfo.InvariantInfo.NumberNegativePattern, A_3.NegativeSign);
		}
		return text2;
	}

	private static string a(string A_0, string A_1, string A_2, int A_3, NumberFormatInfo A_4, bool A_5)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		bool flag = false;
		bool flag2 = false;
		int num5 = 0;
		List<string> list = new List<string>();
		int num6 = -1;
		int num7 = -1;
		while (++num6 < A_0.Length)
		{
			char c2 = A_0[num6];
			switch (c2)
			{
			case '.':
				if (!flag)
				{
					flag = true;
					if (num3 + num == 0)
					{
						stringBuilder.Append("{" + ++num7 + "}");
						num3++;
						list.Add("#");
					}
					stringBuilder.Append("{" + ++num7 + "}");
					list.Add(A_4.NumberDecimalSeparator);
				}
				break;
			case ',':
				num5++;
				break;
			case '#':
			case '0':
				if (num + num3 > 0 && num5 > 0)
				{
					flag2 = true;
				}
				num5 = 0;
				if (flag)
				{
					if (c2 == '0')
					{
						num2++;
					}
					else
					{
						num4++;
					}
				}
				else if (c2 == '0')
				{
					num++;
				}
				else
				{
					num3++;
				}
				stringBuilder.Append("{" + ++num7 + "}");
				list.Add(c2.ToString());
				break;
			default:
				stringBuilder.Append(A_0[num6]);
				break;
			}
		}
		if (num2 + num4 > 0)
		{
			if (A_2.Length > 0)
			{
				A_2 = a(A_2, Math.Min(A_2.Length, num2 + num4), A_2: false);
				A_2 = A_2.TrimEnd(new char[1] { '0' });
			}
		}
		else
		{
			A_1 = a(A_1 + A_2, A_1.Length, A_2: false);
			A_2 = string.Empty;
		}
		if (num5 > 0)
		{
			int num8 = num5 * 3;
			A_1 = ((A_1.Length <= num8) ? "0" : A_1.Substring(0, A_1.Length - num8));
		}
		if (flag2)
		{
			A_1 = a(A_1, A_4.NumberGroupSizes, A_4.NumberGroupSeparator);
		}
		int num9 = num + num3;
		int num10 = num9 - A_1.Length;
		if (A_1 == "0")
		{
			num10 = num9;
		}
		bool flag3 = false;
		for (int num11 = 0; num11 < num9; num11++)
		{
			if (num10 > num11)
			{
				if (list[num11] == "#")
				{
					list[num11] = (flag3 ? "0" : string.Empty);
				}
				else if (list[num11] == "0")
				{
					flag3 = true;
				}
			}
			else if (num10 < 0 && num11 == 0)
			{
				list[num11] = A_1.Substring(0, num11 - num10 + 1);
			}
			else
			{
				list[num11] = A_1[num11 - num10].ToString();
			}
		}
		if (flag)
		{
			if (num2 <= 0 && (num4 <= 0 || A_2.Length <= 0))
			{
				list[num9] = string.Empty;
			}
			num9++;
		}
		for (int num12 = num9; num12 < list.Count; num12++)
		{
			if (num12 - num9 >= A_2.Length)
			{
				if (list[num12] == "#")
				{
					list[num12] = ((num2 == 0) ? string.Empty : "0");
				}
				else if (list[num12] == "0")
				{
					num2--;
				}
			}
			else
			{
				if (list[num12] == "0")
				{
					num2--;
				}
				list[num12] = A_2[num12 - num9].ToString();
			}
		}
		string text = string.Format(stringBuilder.ToString(), (object?[])list.ToArray());
		if (A_5 && !a(text, A_4))
		{
			return c(text, A_4.NumberNegativePattern, A_4.NegativeSign);
		}
		return text;
	}

	private static bool a(string A_0, NumberFormatInfo A_1)
	{
		int num = A_0.IndexOf("e", StringComparison.CurrentCultureIgnoreCase);
		if (num > -1)
		{
			A_0 = A_0.Substring(0, num);
		}
		A_0 = A_0.Trim(new char[1] { '0' });
		if (!(A_0 == string.Empty))
		{
			return A_0 == A_1.NumberDecimalSeparator;
		}
		return true;
	}

	private static string a(string A_0, int[] A_1, string A_2)
	{
		int num = 0;
		int num2 = 0;
		string text = string.Empty;
		while (true)
		{
			if (num < A_1.Length)
			{
				num2 = ((A_1[num] == 0) ? A_0.Length : A_1[num++]);
			}
			if (num2 >= A_0.Length)
			{
				break;
			}
			text = A_2 + A_0.Substring(A_0.Length - num2) + text;
			A_0 = A_0.Substring(0, A_0.Length - num2);
		}
		return A_0 + text;
	}

	private static int a(string A_0, string A_1)
	{
		int result = 0;
		int num = -1;
		if (A_0 == "0")
		{
			if (A_1 != string.Empty)
			{
				for (int num2 = 0; num2 < A_1.Length; num2++)
				{
					if (A_1[num2] != '0')
					{
						num = num2;
						break;
					}
				}
				if (num > -1)
				{
					result = -num - 1;
				}
			}
		}
		else
		{
			result = A_0.Length - 1;
		}
		return result;
	}

	private static string a(string A_0, int A_1, bool A_2)
	{
		string empty = string.Empty;
		if (A_0.Length == 0 || A_0.Length < A_1)
		{
			throw new IndexOutOfRangeException("Index was outside the bounds of the array");
		}
		if (A_0.Length == A_1)
		{
			empty = A_0;
		}
		else if (byte.Parse(A_0[A_1].ToString()) >= 5)
		{
			empty = A_0.Substring(0, A_1 - 1);
			byte b2 = byte.Parse(A_0[A_1 - 1].ToString());
			empty += ++b2;
		}
		else
		{
			empty = A_0.Substring(0, A_1);
		}
		if (A_2)
		{
			empty = empty.TrimEnd(new char[1] { '0' });
		}
		return empty;
	}

	private static bool a(string A_0, out char A_1, out int A_2)
	{
		bool result = true;
		A_1 = A_0[0];
		A_2 = -1;
		if (A_1 == 'G' || A_1 == 'g' || A_1 == 'F' || A_1 == 'f' || A_1 == 'E' || A_1 == 'e' || A_1 == 'N' || A_1 == 'n' || A_1 == 'P' || A_1 == 'p' || A_1 == 'C' || A_1 == 'c')
		{
			if (A_0.Length > 1 && !int.TryParse(A_0.Substring(1), out A_2))
			{
				result = false;
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	public static bool a(string A_0)
	{
		if (A_0.Length > 0)
		{
			return A_0[0] == '-';
		}
		return false;
	}
}
