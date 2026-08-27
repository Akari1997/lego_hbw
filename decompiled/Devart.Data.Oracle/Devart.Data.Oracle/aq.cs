using System.Runtime.CompilerServices;
using System.Text;

namespace Devart.Data.Oracle;

internal abstract class aq
{
	private readonly bool m_a;

	private bool m_b = true;

	protected aq(bool A_0)
	{
		this.m_a = A_0;
	}

	public abstract void c();

	public abstract g b();

	public abstract s a(g A_0, bool A_1);

	public abstract a3 a(g A_0, ah A_1, bool A_2);

	public abstract a3 a(g A_0);

	public abstract w a(g A_0, OracleType A_1);

	public abstract al a(g A_0, string A_1);

	public abstract b a(g A_0, OracleDbType A_1, object A_2, OracleConnection A_3);

	public abstract f a(g A_0, OracleType A_1, byte[] A_2);

	public abstract int a(int A_0);

	[SpecialName]
	public bool m()
	{
		return this.m_a;
	}

	[SpecialName]
	public abstract int d();

	[SpecialName]
	public abstract bool a();

	[SpecialName]
	public Encoding o()
	{
		if (a())
		{
			return Encoding.Unicode;
		}
		return bl.a();
	}

	[SpecialName]
	public Encoding l()
	{
		if (a())
		{
			return Encoding.UTF8;
		}
		return bl.a();
	}

	[SpecialName]
	public virtual bool n()
	{
		return false;
	}

	public int b(g A_0)
	{
		return OracleGlobalization.ApplicationGlobalization.b(A_0);
	}

	[SpecialName]
	public bool p()
	{
		return this.m_b;
	}

	[SpecialName]
	public void a(bool A_0)
	{
		this.m_b = A_0;
	}
}
