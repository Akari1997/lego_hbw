using System;
using System.Runtime.Serialization;

namespace Devart.Common;

[Serializable]
public class ProxyException : Exception
{
	public ProxyException(string message)
		: base(message)
	{
	}

	public ProxyException(Exception inner)
		: base(inner.Message)
	{
	}

	protected ProxyException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
