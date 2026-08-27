using System;
using System.EnterpriseServices;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using Devart.Common;

namespace Devart.Data.Oracle;

[SuppressUnmanagedCodeSecurity]
[CLSCompliant(false)]
[Obfuscation]
public abstract class Oci
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	protected internal delegate int b(IntPtr A_0, IntPtr A_1, IntPtr A_2, int A_3, int A_4);

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	protected internal struct h
	{
		public short a;

		public byte b;

		public byte c;

		public byte d;

		public byte e;

		public byte f;

		internal h(int A_0, int A_1, int A_2, int A_3, int A_4, int A_5)
		{
			a = (short)A_0;
			b = (byte)A_1;
			c = (byte)A_2;
			d = (byte)A_3;
			e = (byte)A_4;
			f = (byte)A_5;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct d
	{
		public byte a;

		public byte b;

		public byte c;

		public byte d;

		public byte e;

		public byte f;

		public byte g;

		private d(bool A_0)
		{
			a = (b = (c = (d = (e = (f = (g = 0))))));
		}
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	protected internal delegate int g(IntPtr A_0, IntPtr A_1, int A_2, int A_3, out IntPtr A_4, out int A_5, out byte A_6, out IntPtr A_7);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	protected internal delegate int c(IntPtr A_0, int A_1, int A_2, int A_3, out IntPtr A_4, out IntPtr A_5, ref byte A_6, out IntPtr A_7, out int A_8);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	protected internal delegate int f(IntPtr A_0, IntPtr A_1, int A_2, out IntPtr A_3, out IntPtr A_4, ref byte A_5, out IntPtr A_6, out ushort A_7);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	protected internal delegate short a(IntPtr A_0, IntPtr A_1, IntPtr A_2, uint A_3, IntPtr A_4, uint A_5);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	protected internal delegate int e(IntPtr A_0, IntPtr A_1, IntPtr A_2, uint A_3, IntPtr A_4, uint A_5);

	public string ociDllPath;

	internal bool a;

	[Devart.Common.q("OCIInitialize")]
	protected internal abstract int OCIInitialize(int mode, IntPtr ctxp, IntPtr malocfp, IntPtr ralocfp, IntPtr mfreefp);

	[Devart.Common.q("OCIEnvInit")]
	protected internal abstract int OCIEnvInit(out IntPtr envhpp, uint mode, uint xtramemsz, uint usrmempp);

	[Devart.Common.q("OCIEnvCreate")]
	protected internal abstract int OCIEnvCreate(out IntPtr envhpp, int mode, IntPtr ctxp, IntPtr malocfp, IntPtr ralocfp, IntPtr mfreefp, uint xtramemsz, uint usrmempp);

	[Devart.Common.q("OCIEnvNlsCreate")]
	protected internal abstract int OCIEnvNlsCreate(out IntPtr envhpp, int mode, IntPtr ctxp, IntPtr malocfp, IntPtr ralocfp, IntPtr mfreefp, uint xtramemsz, uint usrmempp, ushort charset, ushort ncharset);

	[Devart.Common.q("OCIServerAttach")]
	protected internal abstract int OCIServerAttach(HandleRef srvhp, HandleRef errhp, byte[] dblink, int dblink_len, uint mode);

	[Devart.Common.q("OCITerminate")]
	protected internal abstract int OCITerminate(int mode);

	[Devart.Common.q("OCIServerDetach")]
	protected internal abstract int OCIServerDetach(HandleRef srvhp, HandleRef errhp, uint mode);

	[Devart.Common.q("OCISessionBegin")]
	protected internal abstract int OCISessionBegin(HandleRef svchp, HandleRef errhp, HandleRef usrhp, uint credt, uint mode);

	[Devart.Common.q("OCISessionEnd")]
	protected internal abstract int OCISessionEnd(HandleRef svchp, HandleRef errhp, HandleRef usrhp, uint mode);

	[Devart.Common.q("OCIPasswordChange")]
	protected internal abstract int OCIPasswordChange(HandleRef svchp, HandleRef errhp, byte[] user_name, uint usernm_len, byte[] opasswd, uint opasswd_len, byte[] npasswd, int npasswd_len, uint mode);

	[Devart.Common.q("OCIBreak")]
	protected internal abstract int OCIBreak(HandleRef handlep, HandleRef errhp);

	[Devart.Common.q("OCIAttrGet")]
	protected internal abstract int OCIAttrGet(HandleRef trgthndlp, uint trghndltyp, out IntPtr attributep, out IntPtr sizep, uint attrtype, HandleRef errhp);

	[Devart.Common.q("OCIAttrGet")]
	protected internal abstract int OCIAttrGet(HandleRef trgthndlp, uint trghndltyp, out int attributep, out int sizep, uint attrtype, HandleRef errhp);

	[Devart.Common.q("OCIAttrGet")]
	protected internal abstract int OCIAttrGet(HandleRef trgthndlp, uint trghndltyp, IntPtr attributep, out IntPtr sizep, uint attrtype, HandleRef errhp);

	[Devart.Common.q("OCIAttrGet")]
	protected internal abstract int OCIAttrGet(HandleRef trgthndlp, uint trghndltyp, byte[] attributep, out IntPtr sizep, uint attrtype, HandleRef errhp);

	[Devart.Common.q("OCIAttrSet")]
	protected internal abstract int OCIAttrSet(IntPtr trgthndlp, uint trghndltyp, IntPtr attributep, int size, uint attrtype, HandleRef errhp);

	[Devart.Common.q("OCIAttrSet")]
	protected internal abstract int OCIAttrSet(HandleRef trgthndlp, uint trghndltyp, IntPtr attributep, int size, uint attrtype, HandleRef errhp);

	[Devart.Common.q("OCIAttrSet")]
	protected internal abstract int OCIAttrSet(HandleRef trgthndlp, uint trghndltyp, ref int attributep, int size, uint attrtype, HandleRef errhp);

	[Devart.Common.q("OCIAttrSet")]
	protected internal abstract int OCIAttrSet(HandleRef trgthndlp, uint trghndltyp, byte[] attributep, int size, uint attrtype, HandleRef errhp);

	[Devart.Common.q("OCIDescriptorAlloc")]
	protected internal abstract int OCIDescriptorAlloc(HandleRef parenth, out IntPtr descpp, int type, uint xtramem_sz, uint usrmempp);

	[Devart.Common.q("OCIDescriptorAlloc")]
	protected internal abstract int OCIDescriptorAlloc(HandleRef parenth, [Out] byte[] descpp, uint type, uint xtramem_sz, uint usrmempp);

	[Devart.Common.q("OCIDescriptorFree")]
	protected internal abstract int OCIDescriptorFree(HandleRef descp, int type);

	[Devart.Common.q("OCIDescriptorFree")]
	protected internal abstract int OCIDescriptorFree(IntPtr descp, int type);

	[Devart.Common.q("OCIHandleAlloc")]
	protected internal abstract int OCIHandleAlloc(HandleRef parenth, out IntPtr hndlpp, uint type, uint xtramem_sz, uint usrmempp);

	[Devart.Common.q("OCIHandleAlloc")]
	protected internal abstract int OCIHandleAlloc(IntPtr parenth, out IntPtr hndlpp, uint type, uint xtramem_sz, uint usrmempp);

	[Devart.Common.q("OCIHandleFree")]
	protected internal abstract int OCIHandleFree(HandleRef hndlp, int type);

	[Devart.Common.q("OCIHandleFree")]
	protected internal abstract int OCIHandleFree(IntPtr hndlp, int type);

	[Devart.Common.q("OCIParamGet")]
	protected internal abstract int OCIParamGet(HandleRef hndlp, uint htype, HandleRef errhp, out IntPtr parmdpp, uint pos);

	[Devart.Common.q("OCITransPrepare")]
	protected internal abstract int OCITransPrepare(HandleRef svchp, HandleRef errhp, uint flags);

	[Devart.Common.q("OCITransCommit")]
	protected internal abstract int OCITransCommit(HandleRef svchp, HandleRef errhp, uint flags);

	[Devart.Common.q("OCITransRollback")]
	protected internal abstract int OCITransRollback(HandleRef svchp, HandleRef errhp, uint flags);

	[Devart.Common.q("OCITransStart")]
	protected internal abstract int OCITransStart(HandleRef svchp, HandleRef errhp, uint timeout, uint flags);

	[Devart.Common.q("OCIBindByName")]
	protected internal abstract int OCIBindByName(HandleRef stmtp, out IntPtr bindpp, HandleRef errhp, byte[] placeholder, int placeh_len, IntPtr valuep, int value_sz, short dty, IntPtr indp, IntPtr alenp, int rcodep, int maxarr_len, IntPtr curelep, int mode);

	[Devart.Common.q("OCIBindByName")]
	protected internal abstract int OCIBindByName(HandleRef stmtp, out IntPtr bindpp, HandleRef errhp, string placeholder, int placeh_len, IntPtr valuep, int value_sz, short dty, IntPtr indp, IntPtr alenp, int rcodep, int maxarr_len, IntPtr curelep, int mode);

	[Devart.Common.q("OCIBindArrayOfStruct")]
	protected internal abstract int OCIBindArrayOfStruct(HandleRef bindp, HandleRef errhp, int pvskip, int indskip, int alskip, int rcskip);

	[Devart.Common.q("OCIBindObject")]
	protected internal abstract int OCIBindObject(HandleRef bindp, HandleRef errhp, IntPtr type, IntPtr pgvpp, IntPtr pvszsp, IntPtr indpp, IntPtr indszp);

	[Devart.Common.q("OCIBindObject")]
	protected internal abstract int OCIBindObject(HandleRef bindp, HandleRef errhp, IntPtr type, IntPtr pgvpp, IntPtr pvszsp, ref byte[] indpp, IntPtr indszp);

	[Devart.Common.q("OCIStmtGetBindInfo")]
	protected internal abstract int OCIStmtGetBindInfo(HandleRef stmtp, HandleRef errhp, uint size, uint startloc, ref int found, out byte[] bvnp, byte[] bvnl, out byte[] invp, byte[] inpl, byte[] dupl, IntPtr[] hndl);

	[Devart.Common.q("OCIBindDynamic")]
	protected internal abstract int OCIBindDynamic(IntPtr bindp, HandleRef errhp, IntPtr ictxp, IntPtr icbfp, IntPtr octxp, IntPtr ocbfp);

	[Devart.Common.q("OCIDefineArrayOfStruct")]
	protected internal abstract int OCIDefineArrayOfStruct(HandleRef defnp, HandleRef errhp, int pvskip, int indskip, int rlskip, int rcskip);

	[Devart.Common.q("OCIDefineByPos")]
	protected internal abstract int OCIDefineByPos(HandleRef stmtp, ref IntPtr defnpp, HandleRef errhp, int position, IntPtr valuep, int value_sz, short dty, IntPtr indp, IntPtr rlenp, IntPtr rcodep, int mode);

	[Devart.Common.q("OCIDefineObject")]
	protected internal abstract int OCIDefineObject(IntPtr defnp, HandleRef errhp, IntPtr type, IntPtr pgvpp, IntPtr pvszsp, IntPtr indpp, IntPtr indszp);

	[Devart.Common.q("OCIDefineDynamic")]
	protected internal abstract int OCIDefineDynamic(HandleRef defnp, HandleRef errhp, IntPtr octxp, IntPtr ocbfp);

	[Devart.Common.q("OCIDescribeAny")]
	protected internal abstract int OCIDescribeAny(HandleRef svchp, HandleRef errhp, IntPtr objptr, int objnm_len, byte objptr_typ, byte info_level, byte objtyp, HandleRef dschp);

	[Devart.Common.q("OCIDescribeAny")]
	protected internal abstract int OCIDescribeAny(HandleRef svchp, HandleRef errhp, byte[] objptr, int objnm_len, byte objptr_typ, byte info_level, byte objtyp, HandleRef dschp);

	[Devart.Common.q("OCIStmtPrepare")]
	protected internal abstract int OCIStmtPrepare(HandleRef stmtp, HandleRef errhp, byte[] stmt, int stmt_len, uint language, uint mode);

	[Devart.Common.q("OCIStmtPrepare")]
	protected internal abstract int OCIStmtPrepare(HandleRef stmtp, HandleRef errhp, string stmt, int stmt_len, uint language, uint mode);

	[Devart.Common.q("OCIStmtPrepare2")]
	protected internal abstract int OCIStmtPrepare2(HandleRef svchp, out IntPtr stmthp, HandleRef errhp, byte[] stmttext, uint stmt_len, byte[] key, uint keylen, uint language, uint mode);

	[Devart.Common.q("OCIStmtRelease")]
	protected internal abstract int OCIStmtRelease(HandleRef stmthp, HandleRef errhp, byte[] key, uint keylen, uint mode);

	[Devart.Common.q("OCIStmtExecute")]
	protected internal abstract int OCIStmtExecute(HandleRef svchp, HandleRef stmtp, HandleRef errhp, int iters, int rowoff, int snap_in, int snap_out, int mode);

	[Devart.Common.q("OCIStmtFetch")]
	protected internal abstract int OCIStmtFetch(HandleRef stmtp, HandleRef errhp, int nrows, ushort orientation, uint mode);

	[Devart.Common.q("OCIStmtGetPieceInfo")]
	protected internal abstract short OCIStmtGetPieceInfo(HandleRef stmtp, HandleRef errhp, out IntPtr hndlpp, out int typep, out byte in_outp, out int iterp, out int idxp, out byte piecep);

	[Devart.Common.q("OCIStmtSetPieceInfo")]
	protected internal abstract short OCIStmtSetPieceInfo(IntPtr hndlp, int type, HandleRef errhp, IntPtr bufp, IntPtr alenp, byte piece, IntPtr indp, ref ushort rcodep);

	[Devart.Common.q("OCIRowidToChar")]
	protected internal abstract int OCIRowidToChar(HandleRef rowidDesc, byte[] outbfp, ref ushort outbflp, HandleRef errhp);

	[Devart.Common.q("OCILobCreateTemporary")]
	protected internal abstract int OCILobCreateTemporary(HandleRef svchp, HandleRef errhp, IntPtr locp, short csid, byte csfrm, byte lobtype, bool cache, short duration);

	[Devart.Common.q("OCILobFreeTemporary")]
	protected internal abstract int OCILobFreeTemporary(HandleRef svchp, HandleRef errhp, IntPtr locp);

	[Devart.Common.q("OCILobIsTemporary")]
	protected internal abstract int OCILobIsTemporary(HandleRef envhp, HandleRef errhp, IntPtr locp, out int is_temporary);

	[Devart.Common.q("OCILobGetLength")]
	protected internal abstract int OCILobGetLength(HandleRef svchp, HandleRef errhp, IntPtr locp, out int lenp);

	[Devart.Common.q("OCILobGetChunkSize")]
	protected internal abstract int OCILobGetChunkSize(HandleRef svchp, HandleRef errhp, IntPtr locp, out int chunk_size);

	[Devart.Common.q("OCILobRead")]
	protected internal abstract int OCILobRead(HandleRef svchp, HandleRef errhp, IntPtr locp, ref int amtp, int offset, [In][Out] byte[] bufp, int bufl, int ctxp, int cbfp, short csid, byte csfrm);

	[Devart.Common.q("OCILobWrite")]
	protected internal abstract int OCILobWrite(HandleRef svchp, HandleRef errhp, IntPtr locp, ref int amtp, int offset, [In][Out] byte[] bufp, int bufl, byte piece, int ctxp, int cbfp, short csid, byte csfrm);

	[Devart.Common.q("OCILobWrite")]
	protected internal abstract int OCILobWrite(HandleRef svchp, HandleRef errhp, IntPtr locp, ref int amtp, int offset, IntPtr bufp, int bufl, byte piece, int ctxp, int cbfp, short csid, byte csfrm);

	[Devart.Common.q("OCILobErase")]
	protected internal abstract int OCILobErase(HandleRef svchp, HandleRef errhp, IntPtr locp, [In][Out][MarshalAs(UnmanagedType.U4)] ref int amount, int offset);

	[Devart.Common.q("OCILobAppend")]
	protected internal abstract int OCILobAppend(HandleRef svchp, HandleRef errhp, IntPtr dst_locp, IntPtr src_locp);

	[Devart.Common.q("OCILobCopy")]
	protected internal abstract int OCILobCopy(HandleRef svchp, HandleRef errhp, IntPtr dst_locp, IntPtr src_locp, int amount, int dst_offset, int src_offset);

	[Devart.Common.q("OCILobFileExists")]
	protected internal abstract int OCILobFileExists(HandleRef svchp, HandleRef errhp, IntPtr filep, out int flag);

	[Devart.Common.q("OCILobFileGetName")]
	protected internal abstract int OCILobFileGetName(HandleRef envhp, HandleRef errhp, IntPtr filep, IntPtr dir_alias, ref ushort d_length, IntPtr filename, ref ushort f_length);

	[Devart.Common.q("OCILobFileSetName")]
	protected internal abstract int OCILobFileSetName(HandleRef envhp, HandleRef errhp, [In][Out] ref IntPtr filepp, byte[] dir_alias, short d_length, byte[] filename, short f_length);

	[Devart.Common.q("OCILobIsOpen")]
	protected internal abstract int OCILobIsOpen(HandleRef svchp, HandleRef errhp, IntPtr locp, out int flag);

	[Devart.Common.q("OCILobOpen")]
	protected internal abstract int OCILobOpen(HandleRef svchp, HandleRef errhp, IntPtr locp, byte mode);

	[Devart.Common.q("OCILobClose")]
	protected internal abstract int OCILobClose(HandleRef svchp, HandleRef errhp, IntPtr locp);

	[Devart.Common.q("OCILobTrim")]
	protected internal abstract int OCILobTrim(HandleRef svchp, HandleRef errhp, IntPtr locp, int newlen);

	[Devart.Common.q("OCILobIsEqual")]
	protected internal abstract int OCILobIsEqual(HandleRef envhp, IntPtr x, IntPtr y, out int is_equal);

	[Devart.Common.q("OCILobFileClose")]
	protected internal abstract int OCILobFileClose(HandleRef svchp, HandleRef errhp, IntPtr filep);

	[Devart.Common.q("OCILobFileGetName")]
	protected internal abstract int OCILobFileGetName(HandleRef envhp, HandleRef errhp, IntPtr filep, [Out] byte[] dir_alias, ref ushort d_length, [Out] byte[] filename, ref ushort f_length);

	[Devart.Common.q("OCILobFileIsOpen")]
	protected internal abstract int OCILobFileIsOpen(HandleRef svchp, HandleRef errhp, IntPtr filep, out int flag);

	[Devart.Common.q("OCILobFileOpen")]
	protected internal abstract int OCILobFileOpen(HandleRef svchp, HandleRef errhp, IntPtr filep, byte mode);

	[Devart.Common.q("OCILobFileSetName")]
	protected internal abstract int OCILobFileSetName(HandleRef envhp, HandleRef errhp, IntPtr filepp, byte[] dir_alias, ref ushort d_length, byte[] filename, ref ushort f_length);

	[Devart.Common.q("OCILobLoadFromFile")]
	protected internal abstract int OCILobLoadFromFile(HandleRef svchp, HandleRef errhp, IntPtr dst_locp, IntPtr src_locp, int amount, int dst_offset, int src_offset);

	[Devart.Common.q("OCIErrorGet")]
	protected internal abstract int OCIErrorGet(HandleRef hndlp, int recordno, string sqlstate, out int errcodep, byte[] bufp, int bufsiz, int type);

	[Devart.Common.q("OCINumberAssign")]
	protected internal abstract int OCINumberAssign(HandleRef err, byte[] from, IntPtr to);

	[Devart.Common.q("OCINumberFromInt")]
	protected internal abstract int OCINumberFromInt(HandleRef err, ref int inum, int inum_length, int inum_s_flag, byte[] number);

	[Devart.Common.q("OCINumberFromInt")]
	protected internal abstract int OCINumberFromInt(HandleRef err, ref uint inum, int inum_length, int inum_s_flag, byte[] number);

	[Devart.Common.q("OCINumberFromInt")]
	protected internal abstract int OCINumberFromInt(HandleRef err, ref long inum, int inum_length, int inum_s_flag, byte[] number);

	[Devart.Common.q("OCINumberFromInt")]
	protected internal abstract int OCINumberFromInt(HandleRef err, ref ulong inum, int inum_length, int inum_s_flag, byte[] number);

	[Devart.Common.q("OCINumberFromInt")]
	protected internal abstract int OCINumberFromInt(HandleRef err, ref long inum, int inum_length, int inum_s_flag, IntPtr number);

	[Devart.Common.q("OCINumberFromInt")]
	protected internal abstract int OCINumberFromInt(HandleRef err, ref ulong inum, int inum_length, int inum_s_flag, IntPtr number);

	[Devart.Common.q("OCINumberFromReal")]
	protected internal abstract int OCINumberFromReal(HandleRef err, ref float rnum, int rnum_length, byte[] number);

	[Devart.Common.q("OCINumberFromReal")]
	protected internal abstract int OCINumberFromReal(HandleRef err, ref double rnum, int rnum_length, byte[] number);

	[Devart.Common.q("OCINumberFromReal")]
	protected internal abstract int OCINumberFromReal(HandleRef err, ref float rnum, int rnum_length, IntPtr number);

	[Devart.Common.q("OCINumberFromReal")]
	protected internal abstract int OCINumberFromReal(HandleRef err, ref double rnum, int rnum_length, IntPtr number);

	[Devart.Common.q("OCINumberFromText")]
	protected internal abstract int OCINumberFromText(HandleRef err, byte[] str, uint str_length, byte[] fmt, uint fmt_length, byte[] nls_params, uint nls_p_length, [Out] byte[] number);

	[Devart.Common.q("OCINumberFromText")]
	protected internal abstract int OCINumberFromText(HandleRef err, string str, uint str_length, string fmt, uint fmt_length, string nls_params, uint nls_p_length, IntPtr number);

	[Devart.Common.q("OCINumberToText")]
	protected internal abstract int OCINumberToText(HandleRef err, byte[] number, byte[] fmt, uint fmt_length, byte[] nls_params, uint nls_p_length, ref uint buf_size, byte[] buf);

	[Devart.Common.q("OCINumberAbs")]
	protected internal abstract int OCINumberAbs(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberArcCos")]
	protected internal abstract int OCINumberArcCos(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberAdd")]
	protected internal abstract int OCINumberAdd(HandleRef err, byte[] number1, byte[] number2, [Out] byte[] result);

	[Devart.Common.q("OCINumberArcSin")]
	protected internal abstract int OCINumberArcSin(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberArcTan")]
	protected internal abstract int OCINumberArcTan(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberArcTan2")]
	protected internal abstract int OCINumberArcTan2(HandleRef err, byte[] number1, byte[] number2, [Out] byte[] result);

	[Devart.Common.q("OCINumberCeil")]
	protected internal abstract int OCINumberCeil(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberCmp")]
	protected internal abstract int OCINumberCmp(HandleRef err, byte[] number1, byte[] number2, out int result);

	[Devart.Common.q("OCINumberCos")]
	protected internal abstract int OCINumberCos(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberHypCos")]
	protected internal abstract int OCINumberHypCos(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberDiv")]
	protected internal abstract int OCINumberDiv(HandleRef err, byte[] number1, byte[] number2, byte[] result);

	[Devart.Common.q("OCINumberExp")]
	protected internal abstract int OCINumberExp(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberFloor")]
	protected internal abstract int OCINumberFloor(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberLn")]
	protected internal abstract int OCINumberLn(HandleRef err, byte[] number, [Out] byte[] results);

	[Devart.Common.q("OCINumberLog")]
	protected internal abstract int OCINumberLog(HandleRef err, byte[] baseVal, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberMod")]
	protected internal abstract int OCINumberMod(HandleRef err, byte[] number1, byte[] number2, byte[] result);

	[Devart.Common.q("OCINumberMul")]
	protected internal abstract int OCINumberMul(HandleRef err, byte[] number1, byte[] number2, [Out] byte[] result);

	[Devart.Common.q("OCINumberNeg")]
	protected internal abstract int OCINumberNeg(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberPower")]
	protected internal abstract int OCINumberPower(HandleRef err, byte[] baseVal, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberRound")]
	protected internal abstract int OCINumberRound(HandleRef err, byte[] number, int decplace, [Out] byte[] result);

	[Devart.Common.q("OCINumberShift")]
	protected internal abstract int OCINumberShift(HandleRef err, byte[] number, int nDig, [Out] byte[] result);

	[Devart.Common.q("OCINumberSign")]
	protected internal abstract int OCINumberSign(HandleRef err, byte[] number, out int result);

	[Devart.Common.q("OCINumberSin")]
	protected internal abstract int OCINumberSin(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberHypSin")]
	protected internal abstract int OCINumberHypSin(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberSqrt")]
	protected internal abstract int OCINumberSqrt(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberSub")]
	protected internal abstract int OCINumberSub(HandleRef err, byte[] number1, byte[] number2, [Out] byte[] result);

	[Devart.Common.q("OCINumberTan")]
	protected internal abstract int OCINumberTan(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberHypTan")]
	protected internal abstract int OCINumberHypTan(HandleRef err, byte[] number, [Out] byte[] result);

	[Devart.Common.q("OCINumberTrunc")]
	protected internal abstract int OCINumberTrunc(HandleRef err, byte[] number, int decplace, [Out] byte[] result);

	[Devart.Common.q("OCINumberToInt")]
	protected internal abstract int OCINumberToInt(HandleRef err, byte[] number, uint rsl_length, uint rsl_flag, out int rsl);

	[Devart.Common.q("OCINumberToInt")]
	protected internal abstract int OCINumberToInt(HandleRef err, byte[] number, uint rsl_length, uint rsl_flag, out uint rsl);

	[Devart.Common.q("OCINumberToInt")]
	protected internal abstract int OCINumberToInt(HandleRef err, byte[] number, uint rsl_length, uint rsl_flag, out long rsl);

	[Devart.Common.q("OCINumberToInt")]
	protected internal abstract int OCINumberToInt(HandleRef err, byte[] number, uint rsl_length, uint rsl_flag, out ulong rsl);

	[Devart.Common.q("OCINumberToReal")]
	protected internal abstract int OCINumberToReal(HandleRef err, byte[] number, uint rsl_length, out double rsl);

	[Devart.Common.q("OCINumberToReal")]
	protected internal abstract int OCINumberToReal(HandleRef err, byte[] number, uint rsl_length, out float rsl);

	[Devart.Common.q("OCINumberIsInt")]
	protected internal abstract int OCINumberIsInt(HandleRef err, byte[] number, out int result);

	[Devart.Common.q("OCIDateAssign")]
	protected internal abstract int OCIDateAssign(HandleRef err, ref h from, ref h to);

	[Devart.Common.q("OCIDateToText")]
	protected internal abstract int OCIDateToText(HandleRef err, ref h date, byte[] fmt, byte fmt_length, byte[] lang_name, uint lang_length, ref uint buf_size, [Out] byte[] buf);

	[Devart.Common.q("OCIDateFromText")]
	protected internal abstract int OCIDateFromText(HandleRef err, byte[] date_str, uint d_str_length, byte[] fmt, byte fmt_length, byte[] lang_name, uint lang_length, ref h date);

	[Devart.Common.q("OCIDateCompare")]
	protected internal abstract int OCIDateCompare(HandleRef err, ref h date1, ref h date2, out int result);

	[Devart.Common.q("OCIDateAddMonths")]
	protected internal abstract int OCIDateAddMonths(HandleRef err, ref h date, int num_months, ref h result);

	[Devart.Common.q("OCIDateAddDays")]
	protected internal abstract int OCIDateAddDays(HandleRef err, ref h date, int num_days, ref h result);

	[Devart.Common.q("OCIDateLastDay")]
	protected internal abstract int OCIDateLastDay(HandleRef err, ref h date, ref h last_day);

	[Devart.Common.q("OCIDateDaysBetween")]
	protected internal abstract int OCIDateDaysBetween(HandleRef err, ref h date1, ref h date2, out int num_days);

	[Devart.Common.q("OCIDateZoneToZone")]
	protected internal abstract int OCIDateZoneToZone(HandleRef err, ref h date1, byte[] zon1, uint zon1_length, byte[] zon2, uint zon2_length, ref h date2);

	[Devart.Common.q("OCIDateNextDay")]
	protected internal abstract int OCIDateNextDay(HandleRef err, ref h date, byte[] day_p, uint day_length, ref h next_day);

	[Devart.Common.q("OCIDateCheck")]
	protected internal abstract int OCIDateCheck(HandleRef err, ref h date, out uint valid);

	[Devart.Common.q("OCIDateSysDate")]
	protected internal abstract int OCIDateSysDate(HandleRef err, ref h sys_date);

	[Devart.Common.q("OCIIntervalSetDaySecond")]
	protected internal abstract int OCIIntervalSetDaySecond(HandleRef hndl, HandleRef err, int dy, int hr, int mm, int ss, int fsec, IntPtr result);

	[Devart.Common.q("OCIIntervalSetYearMonth")]
	protected internal abstract int OCIIntervalSetYearMonth(HandleRef hndl, HandleRef err, int yr, int mnth, IntPtr interval);

	[Devart.Common.q("OCIIntervalCheck")]
	protected internal abstract int OCIIntervalCheck(HandleRef hndl, HandleRef err, IntPtr interval, out uint valid);

	[Devart.Common.q("OCIIntervalFromNumber")]
	protected internal abstract int OCIIntervalFromNumber(HandleRef hndl, HandleRef err, IntPtr interval, byte[] number);

	[Devart.Common.q("OCIIntervalGetDaySecond")]
	protected internal abstract int OCIIntervalGetDaySecond(HandleRef hndl, HandleRef err, out int dy, out int hr, out int mm, out int ss, out int fsec, IntPtr interval);

	[Devart.Common.q("OCIIntervalGetYearMonth")]
	protected internal abstract int OCIIntervalGetYearMonth(HandleRef hndl, HandleRef err, out int yr, out int mnth, IntPtr interval);

	[Devart.Common.q("OCIIntervalToNumber")]
	protected internal abstract int OCIIntervalToNumber(HandleRef hndl, HandleRef err, IntPtr interval, [Out] byte[] number);

	[Devart.Common.q("OCIIntervalCompare")]
	protected internal abstract int OCIIntervalCompare(HandleRef hndl, HandleRef err, IntPtr inter1, IntPtr inter2, out int result);

	[Devart.Common.q("OCIIntervalToText")]
	protected internal abstract int OCIIntervalToText(HandleRef hndl, HandleRef err, IntPtr interval, byte lfprec, byte fsprec, byte[] buffer, int buflen, out int resultlen);

	[Devart.Common.q("OCIIntervalAdd")]
	protected internal abstract int OCIIntervalAdd(HandleRef hndl, HandleRef err, IntPtr addend1, IntPtr addend2, IntPtr result);

	[Devart.Common.q("OCIIntervalDivide")]
	protected internal abstract int OCIIntervalDivide(HandleRef hndl, HandleRef err, IntPtr dividend, byte[] divisor, IntPtr result);

	[Devart.Common.q("OCIIntervalMultiply")]
	protected internal abstract int OCIIntervalMultiply(HandleRef hndl, HandleRef err, IntPtr inter, byte[] nfactor, IntPtr result);

	[Devart.Common.q("OCIIntervalSubtract")]
	protected internal abstract int OCIIntervalSubtract(HandleRef hndl, HandleRef err, IntPtr minuend, IntPtr subtrahend, IntPtr result);

	[Devart.Common.q("OCIIntervalFromText")]
	protected internal abstract int OCIIntervalFromText(HandleRef hndl, HandleRef err, byte[] inpstring, int str_len, IntPtr result);

	[Devart.Common.q("OCIDateTimeCheck")]
	protected internal abstract int OCIDateTimeCheck(HandleRef hndl, HandleRef err, IntPtr date, out uint valid);

	[Devart.Common.q("OCIDateTimeConstruct")]
	protected internal abstract int OCIDateTimeConstruct(HandleRef hndl, HandleRef err, IntPtr datetime, short year, byte month, byte day, byte hour, byte min, byte sec, uint fsec, byte[] timezone, uint timezone_length);

	[Devart.Common.q("OCIDateTimeGetDate")]
	protected internal abstract int OCIDateTimeGetDate(HandleRef hndl, HandleRef err, IntPtr datetime, out short year, out byte month, out byte day);

	[Devart.Common.q("OCIDateTimeGetTime")]
	protected internal abstract int OCIDateTimeGetTime(HandleRef hndl, HandleRef err, IntPtr datetime, out byte hour, out byte min, out byte sec, out uint fsec);

	[Devart.Common.q("OCIDateTimeGetTimeZoneName")]
	protected internal abstract int OCIDateTimeGetTimeZoneName(HandleRef hndl, HandleRef err, IntPtr datetime, [Out] byte[] buf, ref int buflen);

	[Devart.Common.q("OCIDateTimeGetTimeZoneOffset")]
	protected internal abstract int OCIDateTimeGetTimeZoneOffset(HandleRef hndl, HandleRef err, IntPtr datetime, out sbyte hour, out sbyte min);

	[Devart.Common.q("OCIDateTimeCompare")]
	protected internal abstract int OCIDateTimeCompare(HandleRef hndl, HandleRef err, IntPtr date1, IntPtr date2, out int result);

	[Devart.Common.q("OCIDateTimeSysTimeStamp")]
	protected internal abstract int OCIDateTimeSysTimeStamp(HandleRef hndl, HandleRef err, IntPtr sys_date);

	[Devart.Common.q("OCIDateTimeFromText")]
	protected internal abstract int OCIDateTimeFromText(HandleRef hndl, HandleRef err, byte[] date_str, int dstr_length, byte[] fmt, byte fmt_length, byte[] lang_name, int lang_length, IntPtr datetime);

	[Devart.Common.q("OCIDateTimeToText")]
	protected internal abstract int OCIDateTimeToText(HandleRef hndl, HandleRef err, IntPtr date, byte[] fmt, byte fmt_length, byte fsprec, byte[] lang_name, int lang_length, ref uint buf_size, [Out] byte[] buf);

	[Devart.Common.q("OCIDateTimeIntervalAdd")]
	protected internal abstract int OCIDateTimeIntervalAdd(HandleRef hndl, HandleRef err, IntPtr datetime, IntPtr inter, IntPtr outdatetime);

	[Devart.Common.q("OCIDateTimeIntervalSub")]
	protected internal abstract int OCIDateTimeIntervalSub(HandleRef hndl, HandleRef err, IntPtr datetime, IntPtr inter, IntPtr outdatetime);

	[Devart.Common.q("OCIDateTimeSubtract")]
	protected internal abstract int OCIDateTimeSubtract(HandleRef hndl, HandleRef err, IntPtr indate1, IntPtr indate2, IntPtr inter);

	[Devart.Common.q("OCIRawAssignBytes")]
	protected internal abstract int OCIRawAssignBytes(HandleRef env, HandleRef err, byte[] rhs, int rhs_len, ref IntPtr lhs);

	[Devart.Common.q("OCIRawAssignBytes")]
	protected internal abstract int OCIRawAssignBytes(HandleRef env, HandleRef err, byte[] rhs, int rhs_len, IntPtr lhs);

	[Devart.Common.q("OCIRawAssignRaw")]
	protected internal abstract int OCIRawAssignRaw(HandleRef env, HandleRef err, IntPtr rhs, IntPtr lhs);

	[Devart.Common.q("OCIRawSize")]
	protected internal abstract int OCIRawSize(HandleRef env, IntPtr raw);

	[Devart.Common.q("OCIRawResize")]
	protected internal abstract int OCIRawResize(HandleRef env, HandleRef err, int new_size, ref IntPtr raw);

	[Devart.Common.q("OCIRawResize")]
	protected internal abstract int OCIRawResize(HandleRef env, HandleRef err, int new_size, IntPtr raw);

	[Devart.Common.q("OCIRawPtr")]
	protected internal abstract IntPtr OCIRawPtr(HandleRef env, IntPtr raw);

	[Devart.Common.q("OCIObjectGetAttr")]
	protected internal abstract int OCIObjectGetAttr(HandleRef env, HandleRef err, HandleRef instance, HandleRef null_struct, HandleRef tdo, ref byte[] names, int[] lengths, uint name_count, uint indexes, uint index_count, out short attr_null_status, out IntPtr attr_null_struct, out IntPtr attr_value, out IntPtr attr_tdo);

	[Devart.Common.q("OCIObjectGetInd")]
	protected internal abstract int OCIObjectGetInd(HandleRef env, HandleRef err, HandleRef instance, out IntPtr null_struct);

	[Devart.Common.q("OCIObjectGetInd")]
	protected internal abstract int OCIObjectGetInd(HandleRef env, HandleRef err, int instance, out int null_struct);

	[Devart.Common.q("OCIObjectNew")]
	protected internal abstract int OCIObjectNew(HandleRef env, HandleRef err, HandleRef svc, int typecode, HandleRef tdo, IntPtr table, short duration, int value, out IntPtr instance);

	[Devart.Common.q("OCIObjectNew")]
	protected internal abstract int OCIObjectNew(HandleRef env, HandleRef err, HandleRef svc, int typecode, int tdo, int table, short duration, int value, out IntPtr instance);

	[Devart.Common.q("OCIObjectSetAttr")]
	protected internal abstract int OCIObjectSetAttr(HandleRef env, HandleRef err, HandleRef instance, HandleRef null_struct, HandleRef tdo, ref byte[] names, int[] lengths, uint name_count, uint indexes, uint index_count, short null_status, IntPtr attr_null_struct, IntPtr attr_value);

	[Devart.Common.q("OCITypeByName")]
	protected internal abstract int OCITypeByName(HandleRef env, HandleRef err, HandleRef svc, byte[] schema_name, int s_length, byte[] type_name, int t_length, string version_name, int v_length, short pin_duration, int get_option, out IntPtr tdo);

	[Devart.Common.q("OCITypeByRef")]
	protected internal abstract int OCITypeByRef(HandleRef env, HandleRef err, IntPtr type_ref, short pin_duration, int get_option, IntPtr tdo);

	[Devart.Common.q("OCIStringAssignText")]
	protected internal abstract int OCIStringAssignText(HandleRef env, HandleRef err, string rhs, int rhs_len, ref IntPtr lhs);

	[Devart.Common.q("OCIStringAssignText")]
	protected internal abstract int OCIStringAssignText(HandleRef env, HandleRef err, string rhs, int rhs_len, IntPtr lhs);

	[Devart.Common.q("OCIStringAssignText")]
	protected internal abstract int OCIStringAssignText(HandleRef env, HandleRef err, byte[] rhs, int rhs_len, IntPtr lhs);

	[Devart.Common.q("OCIStringAssignText")]
	protected internal abstract int OCIStringAssignText(HandleRef env, HandleRef err, byte[] rhs, int rhs_len, ref IntPtr lhs);

	[Devart.Common.q("OCIStringPtr")]
	protected internal abstract IntPtr OCIStringPtr(HandleRef env, IntPtr vs);

	[Devart.Common.q("OCIStringResize")]
	protected internal abstract int OCIStringResize(HandleRef env, HandleRef err, int new_size, ref IntPtr str);

	[Devart.Common.q("OCIStringResize")]
	protected internal abstract int OCIStringResize(HandleRef env, HandleRef err, int new_size, IntPtr str);

	[Devart.Common.q("OCIStringSize")]
	protected internal abstract int OCIStringSize(HandleRef env, IntPtr vs);

	[Devart.Common.q("OCICollGetElem")]
	protected internal abstract int OCICollGetElem(HandleRef env, HandleRef err, HandleRef coll, int index, out bool exists, out IntPtr elem, out IntPtr elemind);

	[Devart.Common.q("OCICollSize")]
	protected internal abstract int OCICollSize(HandleRef env, HandleRef err, HandleRef coll, out int size);

	[Devart.Common.q("OCICollTrim")]
	protected internal abstract int OCICollTrim(HandleRef env, HandleRef err, int trim_num, HandleRef coll);

	[Devart.Common.q("OCIIterCreate")]
	protected internal abstract int OCIIterCreate(HandleRef env, HandleRef err, HandleRef coll, out IntPtr itr);

	[Devart.Common.q("OCIIterDelete")]
	protected internal abstract int OCIIterDelete(HandleRef env, HandleRef err, ref IntPtr itr);

	[Devart.Common.q("OCIIterInit")]
	protected internal abstract int OCIIterInit(HandleRef env, HandleRef err, HandleRef coll, IntPtr itr);

	[Devart.Common.q("OCIIterNext")]
	protected internal abstract int OCIIterNext(HandleRef env, HandleRef err, IntPtr itr, out IntPtr elem, out IntPtr elemind, out bool eoc);

	[Devart.Common.q("OCIServerVersion")]
	protected internal abstract int OCIServerVersion(HandleRef hndlp, HandleRef errhp, byte[] bufp, int bufsz, byte hndltype);

	[Devart.Common.q("OCIUnicodeToCharSet")]
	protected internal abstract int OCIUnicodeToCharSet(HandleRef hndl, IntPtr dst, int dstlen, IntPtr src, int srclen, ref int rsize);

	[Devart.Common.q("OCICharSetToUnicode")]
	protected internal abstract int OCICharSetToUnicode(HandleRef envhp, IntPtr dst, int dstlen, IntPtr src, int srclen, out int rsize);

	[Devart.Common.q("OCIRefIsNull")]
	protected internal abstract bool OCIRefIsNull(HandleRef env, HandleRef Ref);

	[Devart.Common.q("OCIObjectPin")]
	protected internal abstract int OCIObjectPin(HandleRef env, HandleRef err, HandleRef object_ref, IntPtr corhdl, int pin_option, short pin_duration, int lock_option, out IntPtr obj);

	[Devart.Common.q("OCIRefToHex")]
	protected internal abstract int OCIRefToHex(HandleRef env, HandleRef err, HandleRef Ref, byte[] hex, ref int hex_length);

	[Devart.Common.q("OCIRefFromHex")]
	protected internal abstract int OCIRefFromHex(HandleRef env, HandleRef err, HandleRef svchp, byte[] str, uint str_length, ref IntPtr Ref);

	[Devart.Common.q("OCIObjectUnpin")]
	protected internal abstract int OCIObjectUnpin(HandleRef env, HandleRef err, HandleRef obj);

	[Devart.Common.q("OCIObjectFree")]
	protected internal abstract int OCIObjectFree(HandleRef env, HandleRef err, HandleRef instance, short flags);

	[Devart.Common.q("OCIRefClear")]
	protected internal abstract void OCIRefClear(HandleRef env, HandleRef Ref);

	[Devart.Common.q("OCICollAppend")]
	protected internal abstract int OCICollAppend(HandleRef env, HandleRef err, IntPtr elem, IntPtr elemind, HandleRef coll);

	[Devart.Common.q("OCITableSize")]
	protected internal abstract int OCITableSize(HandleRef env, HandleRef err, HandleRef tbl, out int size);

	[Devart.Common.q("OCITableNext")]
	protected internal abstract int OCITableNext(HandleRef env, HandleRef err, int index, HandleRef tbl, out int next_index, out bool exists);

	[Devart.Common.q("OCITableLast")]
	protected internal abstract int OCITableLast(HandleRef env, HandleRef err, HandleRef tbl, out int index);

	[Devart.Common.q("OCITableFirst")]
	protected internal abstract int OCITableFirst(HandleRef env, HandleRef err, HandleRef tbl, out int index);

	[Devart.Common.q("OCIObjectFlush")]
	protected internal abstract int OCIObjectFlush(HandleRef env, HandleRef err, HandleRef Object);

	[Devart.Common.q("OCITableDelete")]
	protected internal abstract int OCITableDelete(HandleRef env, HandleRef err, int index, HandleRef tbl);

	[Devart.Common.q("OCIObjectMarkUpdate")]
	protected internal abstract int OCIObjectMarkUpdate(HandleRef env, HandleRef err, HandleRef Object);

	[Devart.Common.q("OCIObjectGetObjectRef")]
	protected internal abstract int OCIObjectGetObjectRef(HandleRef env, HandleRef err, HandleRef Object, out IntPtr object_ref);

	[Devart.Common.q("OCIObjectRefresh")]
	protected internal abstract int OCIObjectRefresh(HandleRef env, HandleRef err, HandleRef Object);

	[Devart.Common.q("OCICollAssignElem")]
	protected internal abstract int OCICollAssignElem(HandleRef env, HandleRef err, int index, IntPtr elem, IntPtr elemind, HandleRef coll);

	[Devart.Common.q("OCIDirPathPrepare")]
	protected internal abstract int OCIDirPathPrepare(HandleRef dpctx, HandleRef svchp, HandleRef errhp);

	[Devart.Common.q("OCIDirPathColArrayReset")]
	protected internal abstract int OCIDirPathColArrayReset(HandleRef dpca, HandleRef errhp);

	[Devart.Common.q("OCIDirPathStreamReset")]
	protected internal abstract int OCIDirPathStreamReset(HandleRef dpstr, HandleRef errhp);

	[Devart.Common.q("OCIDirPathFinish")]
	protected internal abstract int OCIDirPathFinish(HandleRef dpctx, HandleRef errhp);

	[Devart.Common.q("OCIDirPathColArrayToStream")]
	protected internal abstract int OCIDirPathColArrayToStream(HandleRef dpca, HandleRef dpctx, HandleRef dpstr, HandleRef errhp, uint rowcnt, uint rowoff);

	[Devart.Common.q("OCIDirPathLoadStream")]
	protected internal abstract int OCIDirPathLoadStream(HandleRef dpctx, HandleRef dpstr, HandleRef errhp);

	[Devart.Common.q("OCIDirPathColArrayEntrySet")]
	protected internal abstract int OCIDirPathColArrayEntrySet(HandleRef dpca, HandleRef errhp, uint rownum, ushort colIdx, IntPtr cvalp, uint clen, byte cflg);

	[Devart.Common.q("OCIPStreamFromXMLType")]
	[Devart.Common.ar("oraclient{0}.dll")]
	protected internal abstract int OCIPStreamFromXMLTypeStd(HandleRef errhp, HandleRef phOCIDescriptor, HandleRef pobject, int res);

	[Devart.Common.q("OCIPStreamRead")]
	[Devart.Common.ar("oraclient{0}.dll")]
	protected internal abstract int OCIPStreamReadStd(HandleRef errhp, HandleRef phOCIDescriptor, [Out] byte[] pStr, ref long Len, int res);

	[Devart.Common.ar("oraclient{0}.dll")]
	[Devart.Common.q("OCIPStreamClose")]
	protected internal abstract int OCIPStreamCloseStd(HandleRef errhp, HandleRef phOCIDescriptor);

	[Devart.Common.q("OCIPStreamFromXMLType")]
	protected internal abstract int OCIPStreamFromXMLTypeInstant(HandleRef errhp, HandleRef phOCIDescriptor, HandleRef pobject, int res);

	[Devart.Common.q("OCIPStreamRead")]
	protected internal abstract int OCIPStreamReadInstant(HandleRef errhp, HandleRef phOCIDescriptor, [Out] byte[] pStr, ref long Len, int res);

	[Devart.Common.q("OCIPStreamClose")]
	protected internal abstract int OCIPStreamCloseInstant(HandleRef errhp, HandleRef phOCIDescriptor);

	internal int a(HandleRef A_0, HandleRef A_1, HandleRef A_2, int A_3)
	{
		if (!this.a)
		{
			try
			{
				return OCIPStreamFromXMLTypeStd(A_0, A_1, A_2, A_3);
			}
			catch (DllNotFoundException)
			{
				this.a = true;
			}
		}
		return OCIPStreamFromXMLTypeInstant(A_0, A_1, A_2, A_3);
	}

	internal int a(HandleRef A_0, HandleRef A_1, [Out] byte[] A_2, ref long A_3, int A_4)
	{
		if (!this.a)
		{
			try
			{
				return OCIPStreamReadStd(A_0, A_1, A_2, ref A_3, A_4);
			}
			catch (DllNotFoundException)
			{
				this.a = true;
			}
		}
		return OCIPStreamReadInstant(A_0, A_1, A_2, ref A_3, A_4);
	}

	internal int a(HandleRef A_0, HandleRef A_1)
	{
		if (!this.a)
		{
			try
			{
				return OCIPStreamCloseStd(A_0, A_1);
			}
			catch (DllNotFoundException)
			{
				this.a = true;
			}
		}
		return OCIPStreamCloseInstant(A_0, A_1);
	}

	[Devart.Common.q("OCIXMLTypeCreateFromSrc")]
	protected internal abstract int OCIXMLTypeCreateFromSrc(HandleRef svchp, HandleRef errhp, short dur, byte src_type, IntPtr src_ptr, IntPtr ind, out IntPtr retInstance);

	[Devart.Common.q("OCIXMLTypeTransform")]
	protected internal abstract int OCIXMLTypeTransform(HandleRef errhp, short dur, HandleRef doc, HandleRef xsldoc, out IntPtr retDoc);

	[Devart.Common.q("OCIXMLTypeExists")]
	protected internal abstract int OCIXMLTypeExists(HandleRef errhp, HandleRef doc, string xpathexpr, int xpathexpr_Len, string nsmap, int nsmap_Len, out int retval);

	[Devart.Common.q("OCIXMLTypeIsSchemaBased")]
	protected internal abstract int OCIXMLTypeIsSchemaBased(HandleRef errhp, HandleRef doc, out int retval);

	[Devart.Common.q("OCIXMLTypeExtract")]
	protected internal abstract int OCIXMLTypeExtract(HandleRef errhp, HandleRef doc, short dur, string xpathexpr, int xpathexpr_Len, string nsmap, int nsmap_Len, out IntPtr retDoc);

	[Devart.Common.q("OCISubscriptionRegister")]
	protected internal abstract int OCISubscriptionRegister(HandleRef svchp, ref IntPtr subscrhpp, short count, HandleRef errhp, int mode);

	[Devart.Common.q("OCISubscriptionUnRegister")]
	protected internal abstract int OCISubscriptionUnRegister(HandleRef svchp, HandleRef subscrhp, HandleRef errhp, int mode);

	[Devart.Common.q("OCIConnectionPoolCreate")]
	protected internal abstract int OCIConnectionPoolCreate(HandleRef envhp, HandleRef errhp, HandleRef poolhp, out IntPtr poolName, out int poolNameLen, byte[] dblink, int dblinkLen, uint connMin, uint connMax, uint connIncr, byte[] poolUsername, int poolUserLen, byte[] poolPassword, int poolPassLen, uint mode);

	[Devart.Common.q("OCISessionPoolCreate")]
	protected internal abstract int OCISessionPoolCreate(HandleRef envhp, HandleRef errhp, HandleRef spoolhp, out IntPtr poolName, out int poolNameLen, byte[] connStr, uint connStrLen, uint sessMin, uint sessMax, uint sessIncr, byte[] userid, uint useridLen, byte[] password, uint passwordLen, uint mode);

	[Devart.Common.q("OCIConnectionPoolDestroy")]
	protected internal abstract int OCIConnectionPoolDestroy(HandleRef poolhp, HandleRef errhp, uint mode);

	[Devart.Common.q("OCISessionPoolDestroy")]
	protected internal abstract int OCISessionPoolDestroy(HandleRef spoolhp, HandleRef errhp, uint mode);

	[Devart.Common.q("OCISessionGet")]
	protected internal abstract int OCISessionGet(HandleRef envhp, HandleRef errhp, out IntPtr svchp, HandleRef authInfop, byte[] dbName, uint dbName_len, byte[] tagInfo, uint tagInfo_len, out IntPtr retTagInfo, out uint retTagInfo_len, out bool found, uint mode);

	[Devart.Common.q("OCISessionRelease")]
	protected internal abstract int OCISessionRelease(HandleRef svchp, HandleRef errhp, byte[] tag, uint tag_len, uint mode);

	[Devart.Common.q("OCIStmtGetNextResult")]
	protected internal abstract int OCIStmtGetNextResult(HandleRef stmt, HandleRef errhp, out IntPtr result, out int rtype, int mode);

	[Devart.Common.q("OCIAnyDataIsNull")]
	protected internal abstract int OCIAnyDataIsNull(HandleRef svchp, HandleRef errhp, HandleRef sdata, out bool isNull);

	[Devart.Common.q("OCIAnyDataGetType")]
	protected internal abstract int OCIAnyDataGetType(HandleRef svchp, HandleRef errhp, HandleRef data, out ushort typecode, out IntPtr tdo);

	[Devart.Common.q("OCIAnyDataTypeCodeToSqlt")]
	protected internal abstract int OCIAnyDataTypeCodeToSqlt(HandleRef errhp, ushort typecode, out byte sqltcode, out byte csfrm);

	[Devart.Common.q("OCIAnyDataAccess")]
	protected internal abstract int OCIAnyDataAccess(HandleRef svchp, HandleRef errhp, HandleRef sdata, ushort typecode, IntPtr tdo, out short null_ind, byte[] data_value, out uint length);

	[Devart.Common.q("OCIAnyDataAccess")]
	protected internal abstract int OCIAnyDataAccess(HandleRef svchp, HandleRef errhp, HandleRef sdata, ushort typecode, IntPtr tdo, out short null_ind, out IntPtr data_value, out uint length);

	[Devart.Common.q("OCIAnyDataAccess")]
	protected internal abstract int OCIAnyDataAccess(HandleRef svchp, HandleRef errhp, HandleRef sdata, ushort typecode, IntPtr tdo, ref IntPtr null_ind, out IntPtr data_value, out uint length);

	[Devart.Common.q("OCIAnyDataConvert")]
	protected internal abstract int OCIAnyDataConvert(HandleRef svchp, HandleRef errhp, ushort typecode, IntPtr tdo, short duration, ref short null_ind, IntPtr data_value, uint length, ref IntPtr sdata);

	[Devart.Common.q("OCIAnyDataConvert")]
	protected internal abstract int OCIAnyDataConvert(HandleRef svchp, HandleRef errhp, ushort typecode, IntPtr tdo, short duration, IntPtr null_ind, IntPtr data_value, uint length, ref IntPtr sdata);

	[Devart.Common.q("OCIAnyDataConvert")]
	protected internal abstract int OCIAnyDataConvert(HandleRef svchp, HandleRef errhp, ushort typecode, IntPtr tdo, short duration, ref short null_ind, byte[] data_value, uint length, ref IntPtr sdata);

	[Devart.Common.q("OCIAnyDataConvert")]
	protected internal abstract int OCIAnyDataConvert(HandleRef svchp, HandleRef errhp, ushort typecode, IntPtr tdo, short duration, ref short null_ind, ref byte[] data_value, uint length, ref IntPtr sdata);

	[Devart.Common.ar("oramts.dll")]
	[Devart.Common.q("OraMTSSvcGet")]
	protected internal abstract int OraMTSSvcGet(string lpUName, string lpPsswd, string lpDbnam, out IntPtr pOCISvc, out IntPtr pOCIEnv, uint ConFlg);

	[Devart.Common.ar("oramts.dll")]
	[Devart.Common.q("OraMTSSvcRel")]
	protected internal abstract int OraMTSSvcRel(IntPtr svcCtx);

	[Devart.Common.q("OraMTSJoinTxn")]
	[Devart.Common.ar("oramts.dll")]
	protected internal abstract int OraMTSJoinTxn(IntPtr svchp, ITransaction lpTrans);

	[Devart.Common.ar("oramts.dll")]
	[Devart.Common.q("OraMTSEnlCtxGet")]
	protected internal abstract int OraMTSEnlCtxGet(string lpUName, string lpPsswd, string lpDbnam, HandleRef pOCISvc, HandleRef errhp, uint dwFlags, out IntPtr pCtxt);

	[Devart.Common.q("OraMTSEnlCtxRel")]
	[Devart.Common.ar("oramts.dll")]
	protected internal abstract int OraMTSEnlCtxRel(IntPtr svcCtx);

	[Devart.Common.ar("oramts.dll")]
	[Devart.Common.q("OraMTSSvcEnlist")]
	protected internal abstract int OraMTSSvcEnlist(IntPtr svcCtx, IntPtr OCIErr, ITransaction lpTrans, long dwFlags);

	[Devart.Common.q("OraMTSOCIErrGet")]
	[Devart.Common.ar("oramts.dll")]
	protected internal abstract int OraMTSOCIErrGet(HandleRef OCIErr, byte[] lpcEMsg, ref int lpdLen);

	public static bool IsFatalError(OracleException ex)
	{
		int code = ex.Code;
		switch (code)
		{
		default:
			if (code != 200 && code != 603 && code != 81 && code != 1033 && code != 12571)
			{
				return code == 2396;
			}
			break;
		case 28:
		case 203:
		case 204:
		case 205:
		case 206:
		case 207:
		case 208:
		case 1012:
		case 3113:
		case 3114:
		case 12203:
			break;
		}
		return true;
	}
}
