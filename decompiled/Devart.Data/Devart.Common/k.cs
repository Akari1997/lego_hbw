using System;

namespace Devart.Common;

internal class k : WeakReference
{
	public string a;

	public k(object A_0, string A_1)
		: base(A_0)
	{
		a = A_1;
	}
}
