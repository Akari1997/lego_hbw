using System;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

[Serializable]
public sealed class OraclePermission : CodeAccessPermission, IUnrestrictedPermission
{
	private bool a;

	private bool b;

	public bool AllowBlankPassword
	{
		get
		{
			return b;
		}
		set
		{
			b = value;
		}
	}

	public OraclePermission(PermissionState state)
	{
		if (state == PermissionState.Unrestricted)
		{
			a = true;
		}
		else
		{
			a = false;
		}
	}

	internal OraclePermission(OraclePermissionAttribute A_0)
	{
		b = A_0.AllowBlankPassword;
	}

	public bool IsUnrestricted()
	{
		return a;
	}

	public override IPermission Copy()
	{
		OraclePermission oraclePermission = new OraclePermission(PermissionState.None);
		if (IsUnrestricted())
		{
			oraclePermission.a = true;
		}
		else
		{
			oraclePermission.a = false;
		}
		return (IPermission)(object)oraclePermission;
	}

	public override IPermission Intersect(IPermission target)
	{
		try
		{
			if (target == null)
			{
				return null;
			}
			OraclePermission oraclePermission = (OraclePermission)(object)target;
			if (!oraclePermission.IsUnrestricted())
			{
				return (IPermission)(object)oraclePermission;
			}
			return ((CodeAccessPermission)this).Copy();
		}
		catch (InvalidCastException)
		{
			throw new ArgumentException(Devart.Common.al.a("WrongTypeArgument"), ((object)this).GetType().FullName);
		}
	}

	public override bool IsSubsetOf(IPermission target)
	{
		if (target == null)
		{
			return !a;
		}
		try
		{
			OraclePermission oraclePermission = (OraclePermission)(object)target;
			if (a == oraclePermission.a)
			{
				return true;
			}
			return false;
		}
		catch (InvalidCastException)
		{
			throw new ArgumentException(Devart.Common.al.a("WrongTypeArgument"), ((object)this).GetType().FullName);
		}
	}

	public override void FromXml(SecurityElement passedElement)
	{
		if (passedElement == null)
		{
			throw new ArgumentNullException();
		}
		string tag = passedElement.Tag;
		if (tag != "Permission" || tag != "IPermission")
		{
			throw new ArgumentException(Devart.Common.al.a("PassedArgumentNotValidSecurityElement"));
		}
		int num = int.Parse(passedElement.Attribute("version"));
		if (num != 1)
		{
			throw new InvalidOperationException(Devart.Common.al.a("InvalidSecurityXMLVersion"));
		}
		string text = passedElement.Attribute("Unrestricted");
		if (text == null)
		{
			a = false;
		}
		else
		{
			a = bool.Parse(text);
		}
		if (a)
		{
			string text2 = passedElement.Attribute("AllowBlankPassword");
			if (text2 == null)
			{
				b = false;
			}
			else
			{
				b = bool.Parse(text2);
			}
		}
	}

	public override SecurityElement ToXml()
	{
		SecurityElement securityElement = new SecurityElement("IPermission");
		Type type = ((object)this).GetType();
		StringBuilder stringBuilder = new StringBuilder(type.Assembly.ToString());
		stringBuilder.Replace('"', '\'');
		securityElement.AddAttribute("class", type.FullName + ", " + stringBuilder);
		securityElement.AddAttribute("version", "1");
		if (a)
		{
			securityElement.AddAttribute("Unrestricted", "true");
		}
		else
		{
			securityElement.AddAttribute("AllowBlankPassword", b.ToString());
		}
		return securityElement;
	}
}
