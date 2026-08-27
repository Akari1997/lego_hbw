using System.Runtime.CompilerServices;

namespace Devart.Data.Oracle;

internal interface w
{
	[SpecialName]
	OracleType get_ObjectType();

	[SpecialName]
	bool get_IsNull();

	[SpecialName]
	void set_IsNull(bool value);
}
