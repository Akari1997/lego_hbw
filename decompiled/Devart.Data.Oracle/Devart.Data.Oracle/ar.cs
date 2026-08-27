using System;
using System.Runtime.InteropServices;

namespace Devart.Data.Oracle;

internal class ar : m
{
	public ar(string A_0, v A_1)
		: this(OracleType.a(A_0, A_1), A_1)
	{
	}

	public ar(OracleType A_0, v A_1)
		: base(A_0, A_1)
	{
	}

	internal ar(HandleRef A_0, IntPtr A_1, OracleType A_2, v A_3, bool A_4, bool A_5)
		: base(A_0, A_1, A_2, A_3, A_4, A_5)
	{
	}

	protected override int a()
	{
		j().c(o().OCITableLast(((r)this).b.h(), ((r)this).b.k(), ((r)this).f, out var index));
		return index;
	}

	protected override void a(int A_0)
	{
		j().c(o().OCITableDelete(((r)this).b.h(), ((r)this).b.k(), A_0, ((r)this).f));
	}
}
