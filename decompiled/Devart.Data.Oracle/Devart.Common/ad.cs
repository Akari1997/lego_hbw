using System.ComponentModel;
using System.Data;

namespace Devart.Common;

internal class ad : EnumConverter
{
	public ad()
		: base(typeof(ConnectionState))
	{
	}

	public virtual bool a(ITypeDescriptorContext A_0)
	{
		return true;
	}

	public virtual StandardValuesCollection b(ITypeDescriptorContext A_0)
	{
		return new StandardValuesCollection(new ConnectionState[2]
		{
			ConnectionState.Open,
			ConnectionState.Closed
		});
	}
}
