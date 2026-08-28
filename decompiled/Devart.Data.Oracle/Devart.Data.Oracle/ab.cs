using System;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Devart.Data.Oracle;

internal class ab
{
	private static string m_a;

	private StringBuilder m_b;

	private OracleCommand m_c;

	private static int m_d;

	private static string e;

	private static string f;

	private int g;

	private ArrayList h;

	private static Regex i;

	private static string j;

	private static string k;

	private StringBuilder l;

	private Hashtable m;

	static ab()
	{
		j = "BEGIN \n";
		k = "END;";
		e = ":C";
		f = ";\n";
		ab.m_a = ":";
	}

	internal ab()
	{
		this.m_c = new OracleCommand();
		h = new ArrayList();
		this.m_b = new StringBuilder();
		l = new StringBuilder();
		m = new Hashtable();
		this.m_b.Append(j);
		g = 0;
	}

	internal int a(OracleCommand A_0)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException();
		}
		OracleParameterCollection parameters = A_0.Parameters;
		OracleParameter[] array = a(parameters);
		h.Add(array);
		if (CommandType.StoredProcedure == A_0.CommandType)
		{
			this.m_b.Append(b(A_0, array));
			this.m_c.Parameters.AddRange(array);
		}
		else
		{
			a(A_0, array);
		}
		return h.Count - 1;
	}

	private string b(OracleCommand A_0, OracleParameter[] A_1)
	{
		OracleParameter oracleParameter = null;
		int num = 0;
		int num2 = 0;
		string commandText = A_0.CommandText;
		num = A_1.Length;
		l.Remove(0, l.Length);
		if (A_1 == null || num == 0)
		{
			l.Append("Begin " + commandText + "(); End;");
		}
		else if ((oracleParameter = a(A_1)) == null)
		{
			l.Append("Begin " + commandText + "(" + A_1[0].ParameterName + "=>" + e + g);
			A_1[0].ParameterName = e + g++;
			for (num2 = 1; num2 < num; num2++)
			{
				l.Append(", " + A_1[num2].ParameterName + "=>" + e + g);
				A_1[num2].ParameterName = e + g++;
			}
			l.Append("); End;");
		}
		else
		{
			if (A_1[0] == oracleParameter)
			{
				if (num > 1)
				{
					l.Append("Begin :ret" + g);
					oracleParameter.ParameterName = ":ret" + g++;
					l.Append(" := " + commandText + "(" + A_1[1].ParameterName + "=>" + e + g);
					A_1[1].ParameterName = e + g++;
					num2 = 2;
				}
				else
				{
					l.Append("Begin :ret" + g);
					oracleParameter.ParameterName = ":ret" + g++;
					l.Append(" := " + commandText + "(");
					num2 = 1;
				}
			}
			else
			{
				l.Append("Begin :ret" + g);
				oracleParameter.ParameterName = ":ret" + g++;
				l.Append(" := " + commandText + "(" + A_1[0].ParameterName + "=>" + e + g);
				A_1[0].ParameterName = e + g++;
				num2 = 1;
			}
			for (; num2 < num; num2++)
			{
				if (A_1[num2] != oracleParameter)
				{
					l.Append(", " + A_1[num2].ParameterName + "=>" + e + g);
					A_1[num2].ParameterName = e + g++;
				}
			}
			l.Append("); End;");
		}
		return l.ToString();
	}

	private OracleParameter[] a(OracleParameterCollection A_0)
	{
		OracleParameter[] array = new OracleParameter[A_0.Count];
		for (int num = 0; num < A_0.Count; num++)
		{
			array[num] = A_0[num].Clone() as OracleParameter;
		}
		return array;
	}

	internal void c()
	{
		this.m_b.Append(k);
		this.m_c.CommandText = this.m_b.ToString();
	}

	internal OracleParameter a(int A_0, int A_1)
	{
		if (h.Count >= A_0)
		{
			OracleParameter[] array = (OracleParameter[])h[A_0];
			if (array.Length >= A_1)
			{
				return array[A_1];
			}
		}
		return null;
	}

	private static Regex a()
	{
		if (i == null)
		{
			string pattern = "[\\s]+|(?<string>'([^']|'')*')|(?<comment>(/\\*([^\\*]|\\*[^/])*\\*/)|(--.*))|(?<bindparammarker>:[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_#$]+)|(?<query>select)|(?<identifier>([\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_#$]+)|(\"([^\"]|\"\")*\"))|(?<other>.)";
			i = new Regex(pattern, RegexOptions.ExplicitCapture);
			ab.m_d = i.GroupNumberFromName("bindparammarker");
		}
		return i;
	}

	private OracleParameter a(OracleParameter[] A_0)
	{
		int num = A_0.Length;
		for (int num2 = 0; num2 < num; num2++)
		{
			if (A_0[num2].Direction == ParameterDirection.ReturnValue)
			{
				return A_0[num2];
			}
		}
		return null;
	}

	internal void b()
	{
		this.m_c.CommandText = null;
		this.m_c.Parameters.Clear();
		m.Clear();
		this.m_b.Remove(0, this.m_b.Length);
		this.m_b.Append(j);
		g = 0;
		h.Clear();
	}

	private void a(OracleCommand A_0, OracleParameter[] A_1)
	{
		string commandText = A_0.CommandText;
		new ArrayList();
		Regex regex = a();
		m.Clear();
		string text = null;
		l.Remove(0, l.Length);
		l.Append(commandText);
		int num = 0;
		int num2 = 0;
		Match match = regex.Match(commandText);
		while (Match.Empty != match)
		{
			if (match.Groups[ab.m_d].Success)
			{
				string text2 = match.Groups[ab.m_d].Value.Substring(1);
				num2 = A_0.Parameters.IndexOf(text2);
				if (0 > num2)
				{
					num2 = A_0.Parameters.IndexOf(ab.m_a + text2);
					if (0 > num2)
					{
						throw new ArgumentOutOfRangeException();
					}
				}
				if ((text = m[text2] as string) == null)
				{
					text = e + g;
					g++;
					m[text2] = text;
				}
				A_1[num2].ParameterName = text;
				l.Remove(num + match.Index, match.Length);
				l.Insert(num + match.Index, text);
				num += text.Length - match.Length;
			}
			match = match.NextMatch();
		}
		this.m_b.Append(l.ToString());
		this.m_b.Append(f);
		this.m_c.Parameters.AddRange(A_1);
	}

	[SpecialName]
	internal OracleCommand d()
	{
		return this.m_c;
	}
}
