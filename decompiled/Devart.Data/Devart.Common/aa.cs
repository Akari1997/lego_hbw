using System.Runtime.CompilerServices;

namespace Devart.Common;

internal class aa : Token
{
	private readonly int m_a;

	private readonly int m_b;

	public aa(TokenType A_0, object A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, int A_8)
		: base(A_0, A_1, A_2, A_3, A_4, A_5, A_6)
	{
		this.m_b = A_7;
		this.m_a = A_8;
	}

	[SpecialName]
	public virtual int a()
	{
		return this.m_b;
	}

	[SpecialName]
	public virtual int c()
	{
		return this.m_a;
	}

	[SpecialName]
	public virtual int b()
	{
		return EndPosition - this.m_b;
	}
}
