using System.Runtime.CompilerServices;

namespace Devart.Data.Oracle;

internal abstract class a3
{
	private readonly g m_a;

	protected a3(g A_0)
	{
		this.m_a = A_0;
	}

	public abstract void o();

	public abstract int a(int A_0, byte[] A_1, int A_2, int A_3);

	public abstract int b(int A_0, byte[] A_1, int A_2, int A_3);

	public abstract void b(a3 A_0);

	public abstract long a(a3 A_0, int A_1, int A_2, int A_3);

	public abstract long b(a3 A_0, int A_1, int A_2, int A_3);

	public abstract void a(OracleLobOpenMode A_0);

	public abstract void m();

	public abstract int a(int A_0, int A_1);

	public abstract void a(int A_0);

	public abstract bool a(a3 A_0);

	public abstract void g();

	public abstract void a(string A_0, string A_1);

	public abstract void a(out string A_0, out string A_1);

	public abstract void n();

	public virtual byte[] s()
	{
		int num = f();
		if (num == 0)
		{
			return new byte[0];
		}
		byte[] array = new byte[num];
		a(0, array, 0, num);
		return array;
	}

	public abstract string i();

	[SpecialName]
	public g t()
	{
		return this.m_a;
	}

	[SpecialName]
	public abstract bool a();

	[SpecialName]
	public abstract bool e();

	[SpecialName]
	public abstract bool b();

	[SpecialName]
	public abstract bool l();

	[SpecialName]
	public abstract bool c();

	[SpecialName]
	public abstract bool h();

	[SpecialName]
	public abstract bool j();

	[SpecialName]
	public abstract int k();

	[SpecialName]
	public abstract short d();

	[SpecialName]
	public abstract byte p();

	[SpecialName]
	public abstract int f();
}
