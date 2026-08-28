using System;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace Devart.Data;

[Serializable]
internal class DataLinkToolboxItem : ToolboxItem
{
	public DataLinkToolboxItem()
		: base(typeof(DataLink))
	{
	}

	public DataLinkToolboxItem(SerializationInfo info, StreamingContext context)
		: base(typeof(DataLink))
	{
		((ToolboxItem)this).Deserialize(info, context);
	}
}
