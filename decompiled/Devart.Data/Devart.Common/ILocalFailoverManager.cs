using System;
using System.ComponentModel;

namespace Devart.Common;

[EditorBrowsable(EditorBrowsableState.Never)]
[Browsable(false)]
public interface ILocalFailoverManager : IDisposable
{
	ILocalFailoverManager StartUse();

	ILocalFailoverManager StartUse(bool fireConnErrorEvent);

	RetryMode DoLocalFailoverEvent(object sender, ConnectionLostCause cause, RetryMode retryMode, Exception e);
}
