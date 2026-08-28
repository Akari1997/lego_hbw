using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using Devart.Common;

namespace Devart.Data.Oracle;

[TypeConverter(typeof(be))]
public class OracleXml : INullable, IDisposable
{
	private string m_a;

	private OracleConnection b;

	private al m_c;

	public static readonly OracleXml Null = new OracleXml();

	internal al XmlObject
	{
		get
		{
			Utils.CheckArgumentNull(b, "connection");
			g g2 = b.d().l();
			if (this.m_c == null || this.m_c.b() != g2)
			{
				a();
				this.m_c = g2.h().a(g2, this.m_a);
			}
			return this.m_c;
		}
	}

	public OracleConnection Connection
	{
		get
		{
			return b;
		}
		set
		{
			if (value != b)
			{
				a();
				b = value;
			}
		}
	}

	public bool IsNull
	{
		get
		{
			if (this.m_a != null)
			{
				return this.m_a == "";
			}
			return true;
		}
	}

	public string Value => this.m_a;

	public OracleXml()
		: this((string)null, (OracleConnection)null)
	{
	}

	public OracleXml(string value)
		: this(value, null)
	{
	}

	public OracleXml(char[] value)
		: this(new string(value), null)
	{
	}

	public OracleXml(OracleLob lob)
		: this(lob, null)
	{
	}

	public OracleXml(OracleConnection connection)
		: this((string)null, connection)
	{
	}

	public OracleXml(OracleLob lob, OracleConnection connection)
	{
		b = connection;
		if (lob.IsNull)
		{
			this.m_a = "";
			return;
		}
		object value = lob.Value;
		if (value is string text)
		{
			this.m_a = text;
		}
		else if (value is byte[] array)
		{
			this.m_a = bl.a().GetString(array, 0, array.Length);
		}
		else
		{
			this.m_a = "";
		}
	}

	public OracleXml(char[] value, OracleConnection connection)
		: this(new string(value), connection)
	{
	}

	public OracleXml(string value, OracleConnection connection)
	{
		b = connection;
		this.m_a = value;
	}

	internal OracleXml(al A_0, OracleConnection A_1)
	{
		b = A_1;
		this.m_c = A_0;
		this.m_a = A_0.a();
	}

	public void Dispose()
	{
		a();
	}

	internal void c()
	{
		a();
	}

	private void a()
	{
		if (this.m_c != null)
		{
			if (this.m_c is IDisposable disposable)
			{
				disposable.Dispose();
			}
			this.m_c = null;
		}
	}

	public OracleXml Extract(string xpathExpr, string nsMap)
	{
		return new OracleXml(XmlObject.a(xpathExpr, nsMap), b);
	}

	public OracleXml Extract(string xpathExpr, XmlNamespaceManager nsMgr)
	{
		return new OracleXml(XmlObject.a(xpathExpr, a(nsMgr)), b);
	}

	public XmlDocument GetXmlDocument()
	{
		string value = Value;
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.PreserveWhitespace = true;
		xmlDocument.LoadXml(value);
		value = null;
		return xmlDocument;
	}

	public XmlReader GetXmlReader()
	{
		string value = Value;
		TextReader input = new StringReader(value);
		XmlReader result = new XmlTextReader(input);
		value = null;
		input = null;
		return result;
	}

	public bool IsExists(string xpathExpr, string nsMap)
	{
		return XmlObject.b(xpathExpr, nsMap);
	}

	public bool IsExists(string xpathExpr, XmlNamespaceManager nsMgr)
	{
		return XmlObject.b(xpathExpr, a(nsMgr));
	}

	public override string ToString()
	{
		return Value;
	}

	public OracleXml Transform(OracleXml xsldoc)
	{
		return new OracleXml(XmlObject.a(xsldoc.XmlObject), b);
	}

	public OracleXml Transform(string xsldoc)
	{
		OracleXml oracleXml = new OracleXml(xsldoc);
		return new OracleXml(XmlObject.a(oracleXml.XmlObject), b);
	}

	private string a(XmlNamespaceManager A_0)
	{
		string text = null;
		if (A_0 != null)
		{
			text = string.Empty;
			foreach (string item in A_0)
			{
				string text3 = A_0.LookupNamespace(item);
				if (!item.Equals(string.Empty) || !text3.Equals(string.Empty))
				{
					if (!text.Equals(string.Empty))
					{
						text += " ";
					}
					text = text + "xmlns:" + item + "=" + text3;
				}
			}
		}
		return text;
	}
}
