using System;

namespace Devart.Common;

[Flags]
public enum DumpMode
{
	All = 3,
	Schema = 1,
	Data = 2
}
