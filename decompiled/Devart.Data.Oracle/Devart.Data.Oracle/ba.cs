using Devart.Common;

namespace Devart.Data.Oracle;

internal class ba : DbReferenceCollection
{
	internal new const int a = 101;

	protected virtual bool a(int A_0, object A_1, int A_2, object A_3)
	{
		if (A_2 == 101)
		{
			((OracleConnection)A_1).Close();
			return false;
		}
		return base.NotifyItem(A_0, A_1, A_2, A_3);
	}
}
