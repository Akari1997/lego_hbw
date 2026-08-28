using System;

namespace Devart.Common;

public class MonitorEventArgs : EventArgs
{
	private d m_a;

	private MonitorEventType b;

	private string c;

	private MonitorTracePoint d;

	private string e;

	private string[] f;

	private double g;

	public MonitorEventType EventType => b;

	internal d EventTypeInternal => this.m_a;

	internal bool IsUserEvent
	{
		get
		{
			switch (this.m_a)
			{
			case Devart.Common.d.j:
			case Devart.Common.d.k:
			case Devart.Common.d.p:
				return false;
			default:
				return true;
			}
		}
	}

	public string Description => c;

	public MonitorTracePoint TracePoint => d;

	public string ExtraInfo => e;

	public string[] CallStack => f;

	public double Duration => g;

	internal MonitorEventArgs(d A_0, string A_1, MonitorTracePoint A_2, string A_3, string[] A_4, double A_5)
	{
		this.m_a = A_0;
		b = a(A_0);
		c = A_1;
		d = A_2;
		e = A_3;
		f = A_4;
		g = A_5;
	}

	private MonitorEventType a(d A_0)
	{
		if (A_0 <= Devart.Common.d.i)
		{
			return (MonitorEventType)A_0;
		}
		switch (A_0)
		{
		case Devart.Common.d.l:
		case Devart.Common.d.m:
			return MonitorEventType.Connect;
		case Devart.Common.d.n:
			return MonitorEventType.ActivateInPool;
		case Devart.Common.d.o:
			return MonitorEventType.ReturnToPool;
		case Devart.Common.d.e:
		case Devart.Common.d.j:
		case Devart.Common.d.k:
		case Devart.Common.d.p:
			return MonitorEventType.Custom;
		default:
			throw new NotSupportedException(A_0.ToString());
		}
	}
}
