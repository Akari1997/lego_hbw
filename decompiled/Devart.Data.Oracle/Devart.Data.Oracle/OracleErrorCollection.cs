using System;
using System.Collections;

namespace Devart.Data.Oracle;

[Serializable]
public class OracleErrorCollection : ICollection
{
	private OracleError[] a;

	public int Count => a.Length;

	public OracleError this[int index] => a[index];

	bool ICollection.IsSynchronized => ((ICollection)a).IsSynchronized;

	object ICollection.SyncRoot => ((ICollection)a).SyncRoot;

	internal OracleErrorCollection()
		: this(null)
	{
	}

	internal OracleErrorCollection(OracleError[] A_0)
	{
		a = A_0;
	}

	public IEnumerator GetEnumerator()
	{
		return a.GetEnumerator();
	}

	public void CopyTo(Array array, int index)
	{
		a.CopyTo(array, index);
	}
}
