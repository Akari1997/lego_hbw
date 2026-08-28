namespace Devart.Data.Oracle;

public enum OracleConnectMode
{
	Default = 0,
	SysDba = 2,
	SysOper = 4,
	SysAsm = 0x8000,
	SysBackup = 0x20000,
	SysDg = 0x40000,
	SysKm = 0x80000
}
