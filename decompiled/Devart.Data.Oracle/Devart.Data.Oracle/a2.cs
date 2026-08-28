using System;
using System.Data;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class a2 : r, b
{
	private new static string m_a;

	internal a2(HandleRef A_0, IntPtr A_1, v A_2, bool A_3, bool A_4)
		: base(A_0, A_1, OracleType.a("SYS", "ANYDATA", A_2), A_2, A_3, A_4)
	{
		base.g = new HandleRef(this, IntPtr.Zero);
	}

	public a2(v A_0, OracleDbType A_1, object A_2, OracleConnection A_3)
		: base(OracleType.a("SYS", "ANYDATA", A_0), A_0, A_2: false)
	{
		a(A_1, A_2, A_3);
	}

	private new h b(OracleDbType A_0, object A_1, OracleConnection A_2)
	{
		h A_3 = default(h);
		OracleParameter.a(A_1, ref A_3, A_0, 0, ParameterDirection.Input, 0, 0, g());
		switch (A_3.c)
		{
		case 5:
			if (A_1 != null)
			{
				if ((object)A_1.GetType() == typeof(OracleString))
				{
					A_3.m = Encoding.Default.GetByteCount(((OracleString)A_1).Value) + 1;
				}
				else if ((object)A_1.GetType() == typeof(char[]))
				{
					A_3.m = Encoding.Default.GetByteCount((char[])A_1) + 1;
				}
				else if ((object)A_1.GetType() == typeof(string))
				{
					A_3.m = Encoding.Default.GetByteCount((string)A_1) + 1;
				}
				if (g().h().a())
				{
					A_3.m *= 2;
				}
			}
			break;
		case 6:
			A_3.c = 2;
			break;
		case 21:
		case 22:
			A_3.m = 8;
			break;
		case 58:
		case 108:
		case 122:
		case 247:
		case 248:
		{
			OracleType oracleType = null;
			if (A_1 is NativeOracleObject nativeOracleObject)
			{
				oracleType = nativeOracleObject.ObjectType;
			}
			else if (A_1 is NativeOracleArray nativeOracleArray)
			{
				oracleType = nativeOracleArray.ObjectType;
			}
			else if (A_1 is OracleXml oracleXml)
			{
				oracleXml.Connection = A_2;
				oracleType = ((bj)oracleXml.XmlObject).get_ObjectType();
			}
			if (oracleType != null)
			{
				A_3.s = g().b(oracleType);
			}
			break;
		}
		case 110:
			if (A_1 is OracleRef oracleRef)
			{
				oracleRef.Connection = A_2;
				A_3.s = g().b(oracleRef.RefType);
			}
			break;
		}
		return A_3;
	}

	private new k a(IntPtr A_0, ushort A_1, OracleConnection A_2)
	{
		int num = a(A_1, out var A_3);
		switch (num)
		{
		case 58:
		case 108:
		case 110:
		case 122:
		case 247:
		case 248:
		{
			if (A_0 == IntPtr.Zero)
			{
				throw new OracleException(-1, "Cannot detect the type of Oracle object.");
			}
			OracleType a_ = a(A_0);
			k k2 = g().b(A_1, 0, null, a_);
			k2.a(A_2);
			return k2;
		}
		default:
			return g().b(num, A_3, null, null);
		}
	}

	private new OracleType a(IntPtr A_0)
	{
		j().c(o().OCIHandleAlloc(j().h(), out var hndlpp, 7u, 0u, 0u));
		HandleRef handleRef = new HandleRef(this, hndlpp);
		try
		{
			j().c(o().OCIDescribeAny(g().r(), j().k(), A_0, IntPtr.Size, 3, 0, 6, handleRef));
			j().c(o().OCIAttrGet(handleRef, 7u, out IntPtr attributep, out IntPtr _, 124u, j().k()));
			HandleRef trgthndlp = new HandleRef(this, attributep);
			j().c(o().OCIAttrGet(trgthndlp, 53u, out IntPtr attributep2, out IntPtr sizep2, 9u, j().k()));
			string a_ = ((attributep2 == IntPtr.Zero || (int)sizep2 == 0) ? "" : ((!j().a()) ? Marshal.PtrToStringAnsi(attributep2, (int)sizep2) : Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2)));
			j().c(o().OCIAttrGet(trgthndlp, 53u, out attributep2, out sizep2, 4u, j().k()));
			string a_2 = ((attributep2 == IntPtr.Zero || (int)sizep2 == 0) ? "" : ((!j().a()) ? Marshal.PtrToStringAnsi(attributep2, (int)sizep2) : Marshal.PtrToStringUni(attributep2, (int)sizep2 / 2)));
			return OracleType.a(a_, a_2, g());
		}
		finally
		{
			j().c(o().OCIHandleFree(handleRef, 7));
		}
	}

	private new int a(OracleDbType A_0, out int A_1)
	{
		A_1 = 1;
		switch (A_0)
		{
		case OracleDbType.NVarChar:
			A_1 = 2;
			return 9;
		case OracleDbType.VarChar:
			return 9;
		case OracleDbType.Char:
			return 96;
		case OracleDbType.NChar:
			A_1 = 2;
			return 96;
		case OracleDbType.Number:
			return 2;
		case OracleDbType.Date:
			return 12;
		case OracleDbType.RowId:
			return 11;
		case OracleDbType.Raw:
			return 95;
		case OracleDbType.Long:
			return 8;
		case OracleDbType.LongRaw:
			return 24;
		case OracleDbType.NClob:
			A_1 = 2;
			return 112;
		case OracleDbType.Clob:
			return 112;
		case OracleDbType.Blob:
			return 113;
		case OracleDbType.BFile:
			return 114;
		case OracleDbType.Cursor:
			return 102;
		case OracleDbType.Object:
			return 108;
		case OracleDbType.Array:
			return 247;
		case OracleDbType.Table:
			return 248;
		case OracleDbType.Ref:
			return 110;
		case OracleDbType.TimeStamp:
			return 187;
		case OracleDbType.TimeStampLTZ:
			return 232;
		case OracleDbType.TimeStampTZ:
			return 188;
		case OracleDbType.IntervalDS:
			return 190;
		case OracleDbType.IntervalYM:
			return 189;
		case OracleDbType.Integer:
			return 3;
		case OracleDbType.Double:
		case OracleDbType.Float:
			return 4;
		case OracleDbType.Xml:
			return 58;
		case OracleDbType.Boolean:
			return 252;
		default:
			return 0;
		}
	}

	private new ushort a(short A_0, byte A_1)
	{
		switch (A_0)
		{
		case 7:
			return 7;
		case 105:
			return 105;
		case 9:
			return 9;
		case 1:
			return 1;
		case 5:
			if (A_1 == 2)
			{
				return 287;
			}
			return 9;
		case 96:
		case 97:
			if (A_1 == 2)
			{
				return 286;
			}
			return 96;
		case 2:
			return 2;
		case 186:
			return 186;
		case 185:
			return 185;
		case 156:
			return 12;
		case 11:
			return 104;
		case 23:
		case 95:
			return 95;
		case 112:
			if (A_1 == 2)
			{
				return 288;
			}
			return 112;
		case 113:
			return 113;
		case 114:
			return 114;
		case 115:
			return 115;
		case 108:
			return 108;
		case 122:
			return 122;
		case 247:
			return 247;
		case 248:
			return 248;
		case 110:
			return 110;
		case 187:
			return 187;
		case 232:
			return 232;
		case 188:
			return 188;
		case 190:
			return 190;
		case 189:
			return 189;
		case 3:
			return 3;
		case 246:
			return 28;
		case 4:
			return 4;
		case 22:
			return 101;
		case 21:
			return 100;
		case 58:
			return 58;
		default:
			return 0;
		}
	}

	private new int a(ushort A_0, out byte A_1)
	{
		A_1 = 1;
		switch (A_0)
		{
		case 7:
			return 7;
		case 105:
			return 105;
		case 9:
			return 9;
		case 1:
			return 1;
		case 287:
			A_1 = 2;
			return 5;
		case 96:
			return 96;
		case 286:
			A_1 = 2;
			return 96;
		case 2:
			return 2;
		case 186:
			return 186;
		case 185:
			return 185;
		case 12:
			return 156;
		case 104:
			return 11;
		case 95:
			return 95;
		case 112:
			return 112;
		case 288:
			A_1 = 2;
			return 112;
		case 113:
			return 113;
		case 114:
			return 114;
		case 115:
			return 115;
		case 108:
			return 108;
		case 122:
			return 122;
		case 247:
			return 247;
		case 248:
			return 248;
		case 110:
			return 110;
		case 187:
			return 187;
		case 232:
			return 232;
		case 188:
			return 188;
		case 190:
			return 190;
		case 189:
			return 189;
		case 3:
		case 29:
			return 3;
		case 28:
			return 246;
		case 4:
		case 21:
		case 22:
			return 4;
		case 101:
			return 22;
		case 100:
			return 21;
		case 58:
			return 58;
		default:
			return 0;
		}
	}

	[SpecialName]
	public virtual bool a()
	{
		j().c(o().OCIAnyDataIsNull(g().r(), j().k(), f, out var isNull));
		return isNull;
	}

	private new void a(OracleDbType A_0, object A_1, OracleConnection A_2)
	{
		h h2 = b(A_0, A_1, A_2);
		ushort num = a(h2.c, (byte)h2.h);
		IntPtr intPtr = ((h2.s == null) ? IntPtr.Zero : ((am)h2.s).e.Handle);
		k k2 = a(intPtr, num, A_2);
		IntPtr sdata = IntPtr.Zero;
		byte[] data_value = new byte[h2.m + 2];
		object obj = A_1;
		OracleDbType oracleDbType = k2.k();
		switch (num)
		{
		case 110:
			oracleDbType = OracleDbType.Ref;
			break;
		case 232:
		{
			obj = ((obj is OracleTimeStamp) ? ((object)(OracleTimeStamp)obj) : ((!(obj is OracleDate oracleDate)) ? ((!(obj is string) && !(obj is OracleString)) ? ((object)new OracleTimeStamp(Convert.ToDateTime(obj), oracleDbType)) : ((object)OracleTimeStamp.Parse(OracleParameter.b(obj), oracleDbType))) : ((object)new OracleTimeStamp(oracleDate.Value, oracleDbType))));
			OracleTimeStamp oracleTimeStamp = (OracleTimeStamp)obj;
			obj = oracleTimeStamp + oracleTimeStamp.TimeZoneOffset;
			break;
		}
		}
		OracleParameter.a(oracleDbType, null, obj, data_value, null, 0, h2.m, 0, h2.c, h2.m, A_10: true, A_2, ParameterDirection.Input, null, g(), out var _);
		short null_ind = Devart.Common.e.h(data_value, h2.m);
		switch (num)
		{
		case 112:
		case 113:
		case 114:
		case 288:
			j().c(o().OCIAnyDataConvert(g().r(), j().k(), num, intPtr, 10, ref null_ind, ref data_value, (uint)h2.m, ref sdata));
			break;
		case 1:
		case 9:
		case 96:
		case 286:
		case 287:
		{
			int typecode = num;
			switch (num)
			{
			case 286:
				typecode = 96;
				break;
			case 287:
				typecode = 9;
				break;
			}
			j().c(o().OCIObjectNew(j().h(), j().k(), c.r(), typecode, 0, 0, 10, 1, out var instance2));
			int num2 = h2.m;
			if (num2 > 0)
			{
				if (j().a())
				{
					if (Devart.Common.e.h(data_value, num2 - 2) == 0)
					{
						num2 -= 2;
					}
				}
				else if (data_value[num2 - 1] == 0)
				{
					num2--;
				}
			}
			j().c(o().OCIStringAssignText(j().h(), j().k(), data_value, num2, ref instance2));
			j().c(o().OCIAnyDataConvert(g().r(), j().k(), num, IntPtr.Zero, 10, ref null_ind, instance2, (uint)num2, ref sdata));
			break;
		}
		case 95:
		{
			j().c(o().OCIObjectNew(j().h(), j().k(), c.r(), num, 0, 0, 10, 1, out var instance));
			j().c(o().OCIRawAssignBytes(j().h(), j().k(), data_value, h2.m, ref instance));
			j().c(o().OCIAnyDataConvert(g().r(), j().k(), num, IntPtr.Zero, 10, ref null_ind, instance, (uint)h2.m, ref sdata));
			break;
		}
		case 58:
		case 110:
		case 187:
		case 188:
		case 189:
		case 190:
		case 232:
		{
			if (obj is INullable { IsNull: not false })
			{
				null_ind = -1;
			}
			IntPtr data_value3 = ((null_ind == 0) ? Devart.Common.z.e(data_value, 0) : IntPtr.Zero);
			j().c(o().OCIAnyDataConvert(g().r(), j().k(), num, intPtr, 10, ref null_ind, data_value3, (uint)h2.m, ref sdata));
			break;
		}
		case 108:
		case 122:
		case 247:
		case 248:
		{
			IntPtr data_value2;
			IntPtr null_ind2;
			if (null_ind != 0)
			{
				data_value2 = IntPtr.Zero;
				null_ind2 = IntPtr.Zero;
			}
			else
			{
				data_value2 = Devart.Common.z.e(data_value, 0);
				null_ind2 = Devart.Common.z.e(data_value, IntPtr.Size);
			}
			j().c(o().OCIAnyDataConvert(g().r(), j().k(), num, intPtr, 10, null_ind2, data_value2, (uint)h2.m, ref sdata));
			break;
		}
		default:
			j().c(o().OCIAnyDataConvert(g().r(), j().k(), num, intPtr, 10, ref null_ind, data_value, (uint)h2.m, ref sdata));
			break;
		}
		f = new HandleRef(this, sdata);
	}

	public new object a(OracleConnection A_0, out OracleDbType A_1)
	{
		if (OracleUtils.c(c.m()).StartsWith("09.00.00.00"))
		{
			throw new OracleException(-1, a2.m_a);
		}
		uint length = 0u;
		IntPtr data_value = IntPtr.Zero;
		byte[] array = null;
		Devart.Common.z z2 = null;
		try
		{
			j().c(o().OCIAnyDataGetType(g().r(), j().k(), f, out var typecode, out var tdo));
			k k2 = a(tdo, typecode, A_0);
			A_1 = k2.k();
			int num;
			switch (typecode)
			{
			case 100:
				length = 4u;
				num = 2;
				break;
			case 101:
				length = 8u;
				num = 2;
				break;
			case 108:
				num = 3;
				break;
			default:
				num = 0;
				break;
			}
			short null_ind;
			switch (num)
			{
			case 3:
			{
				IntPtr null_ind2 = IntPtr.Zero;
				j().c(o().OCIAnyDataAccess(g().r(), j().k(), f, typecode, tdo, ref null_ind2, out data_value, out length));
				null_ind = Marshal.ReadInt16(null_ind2);
				break;
			}
			case 0:
				j().c(o().OCIAnyDataAccess(g().r(), j().k(), f, typecode, tdo, out null_ind, out data_value, out length));
				break;
			case 1:
				array = new byte[length];
				j().c(o().OCIAnyDataAccess(g().r(), j().k(), f, typecode, tdo, out null_ind, array, out length));
				break;
			default:
				array = new byte[length];
				z2 = new Devart.Common.z();
				data_value = z2.b(array);
				j().c(o().OCIAnyDataAccess(g().r(), j().k(), f, typecode, tdo, out null_ind, out data_value, out length));
				break;
			}
			if (null_ind != 0)
			{
				return DBNull.Value;
			}
			switch (typecode)
			{
			case 1:
			case 9:
			case 96:
			case 286:
			case 287:
			{
				int num2 = o().OCIStringSize(j().h(), data_value);
				array = new byte[num2];
				Marshal.Copy(o().OCIStringPtr(j().h(), data_value), array, 0, num2);
				return k2.t(array, 0, num2);
			}
			case 100:
			case 101:
				return k2.t(array, 0, (int)length);
			case 2:
			{
				byte[] array2 = new byte[22];
				Marshal.Copy(data_value, array2, 0, (int)length);
				return k2.t(array2, 0, (int)length);
			}
			case 58:
			case 108:
			case 122:
			case 247:
			case 248:
			{
				byte[] array2 = new byte[IntPtr.Size * 2];
				byte[] src = ((IntPtr.Size != 4) ? Devart.Common.e.a(data_value.ToInt64()) : Devart.Common.e.a(data_value.ToInt32()));
				Buffer.BlockCopy(src, 0, array2, 0, IntPtr.Size);
				return k2.e(array2, 0, A_2: false);
			}
			case 110:
			{
				byte[] array2 = new byte[IntPtr.Size * 2];
				byte[] src2 = ((IntPtr.Size != 4) ? Devart.Common.e.a(data_value.ToInt64()) : Devart.Common.e.a(data_value.ToInt32()));
				Buffer.BlockCopy(src2, 0, array2, 0, IntPtr.Size);
				return k2.b(array2, 0, A_2: false);
			}
			case 95:
			case 187:
			case 188:
			case 189:
			case 190:
			case 232:
			{
				byte[] a_ = ((IntPtr.Size != 4) ? Devart.Common.e.a(data_value.ToInt64()) : Devart.Common.e.a(data_value.ToInt32()));
				return k2.t(a_, 0, IntPtr.Size);
			}
			default:
			{
				byte[] array2 = new byte[length];
				Marshal.Copy(data_value, array2, 0, (int)length);
				return k2.t(array2, 0, (int)length);
			}
			}
		}
		finally
		{
			z2?.a();
		}
	}

	[SpecialName]
	private new g b()
	{
		return g();
	}

	g b.a()
	{
		//ILSpy generated this explicit interface implementation from .override directive in b
		return this.b();
	}

	static a2()
	{
		a2.m_a = "Requested operation with OracleAnyData is not supported on this Oracle client/server.";
	}
}
