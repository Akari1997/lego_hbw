using System;
using System.Security;
using System.Security.Permissions;

namespace Devart.Data.Oracle;

[Serializable]
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public sealed class OraclePermissionAttribute : CodeAccessSecurityAttribute
{
	private bool a;

	public bool AllowBlankPassword
	{
		get
		{
			return a;
		}
		set
		{
			a = value;
		}
	}

	public OraclePermissionAttribute(SecurityAction action)
		: base(action)
	{
	}

	public override IPermission CreatePermission()
	{
		return (IPermission)(object)new OraclePermission(this);
	}
}
