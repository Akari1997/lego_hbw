using System.Runtime.CompilerServices;
using System.Text;

namespace Devart.Data.Oracle;

internal static class bl
{
	[SpecialName]
	public static Encoding a()
	{
		return Encoding.Default;
	}

	public static Encoding a(int A_0)
	{
		return Encoding.GetEncoding(A_0);
	}
}
