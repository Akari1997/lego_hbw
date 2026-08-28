using System;

namespace Devart.Common;

internal sealed class y
{
	private y()
	{
	}

	internal static bool c(object A_0)
	{
		Utils.CheckArgumentNull(A_0, "value");
		if (A_0 is string text)
		{
			if (Utils.CompareInvariant(text, "true") || Utils.CompareInvariant(text, "yes"))
			{
				return true;
			}
			if (Utils.CompareInvariant(text, "false") || Utils.CompareInvariant(text, "no"))
			{
				return false;
			}
			string st = text.Trim();
			if (Utils.CompareInvariant(st, "true") || Utils.CompareInvariant(st, "yes"))
			{
				return true;
			}
			if (Utils.CompareInvariant(st, "false") || Utils.CompareInvariant(st, "no"))
			{
				return false;
			}
			return bool.Parse(text);
		}
		try
		{
			return ((IConvertible)A_0).ToBoolean(null);
		}
		catch (Exception innerException)
		{
			throw new InvalidOperationException(n.a("ConvertFailed", A_0.GetType(), typeof(bool)), innerException);
		}
	}

	internal static int b(object A_0)
	{
		Utils.CheckArgumentNull(A_0, "value");
		try
		{
			return ((IConvertible)A_0).ToInt32(null);
		}
		catch (Exception innerException)
		{
			throw new InvalidOperationException(n.a("ConvertFailed", A_0.GetType(), typeof(int)), innerException);
		}
	}

	internal static string a(object A_0)
	{
		Utils.CheckArgumentNull(A_0, "value");
		try
		{
			return ((IConvertible)A_0).ToString(null);
		}
		catch (Exception innerException)
		{
			throw new InvalidOperationException(n.a("ConvertFailed", A_0.GetType(), typeof(string)), innerException);
		}
	}
}
