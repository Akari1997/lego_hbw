using System.Collections;
using System.Runtime.CompilerServices;

namespace Devart.Data.Oracle;

internal class ai : ArrayList
{
	private bool m_a;

	public ai(bool A_0, int A_1)
		: base(A_1)
	{
		this.m_a = A_0;
	}

	[SpecialName]
	public bool a()
	{
		return this.m_a;
	}
}
