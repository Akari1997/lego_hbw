namespace Devart.Common;

internal class SelectStatementNode
{
	internal int a = -1;

	internal int b = -1;

	private bool c;

	internal int Length
	{
		get
		{
			if (Current)
			{
				return this.b - this.a + 1;
			}
			return ToString().Length;
		}
	}

	internal bool Current
	{
		get
		{
			if (!c)
			{
				return Binded;
			}
			return false;
		}
	}

	internal bool Binded => this.a != -1;

	internal bool Marker
	{
		get
		{
			if (this.a != -1)
			{
				return this.b == -1;
			}
			return false;
		}
	}

	internal void d()
	{
		c = true;
	}

	internal void b()
	{
		c = false;
	}

	internal void a(int A_0)
	{
		if (this.a != -1)
		{
			this.a += A_0;
		}
		if (this.b != -1)
		{
			this.b += A_0;
		}
	}

	internal virtual string a(string A_0)
	{
		return ToString();
	}
}
