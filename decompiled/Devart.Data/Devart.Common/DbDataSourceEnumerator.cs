using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace Devart.Common;

public abstract class DbDataSourceEnumerator : System.Data.Common.DbDataSourceEnumerator
{
	protected int port;

	private string m_a;

	private string b;

	private static h[] c;

	private static int d = 0;

	protected DbDataSourceEnumerator(string factoryName, string serverPrefix, int port)
	{
		this.port = port;
		this.m_a = factoryName;
		b = serverPrefix;
	}

	public override DataTable GetDataSources()
	{
		DataTable dataTable = new DataTable(b + "DataSources");
		dataTable.Locale = CultureInfo.InvariantCulture;
		dataTable.Columns.Add("ServerName", typeof(string));
		dataTable.Columns.Add("InstanceName", typeof(string));
		dataTable.Columns.Add("IsClustered", typeof(string));
		dataTable.Columns.Add("Version", typeof(string));
		dataTable.Columns.Add("FactoryName", typeof(string));
		IList list = a(5000);
		if (list != null)
		{
			foreach (string item in list)
			{
				if (ProcessServer(item, out var instanceName, out var isClustered, out var version))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow[0] = item;
					dataRow[1] = instanceName;
					dataRow[2] = isClustered;
					dataRow[3] = version;
					dataRow[4] = this.m_a;
					dataTable.Rows.Add(dataRow);
				}
			}
		}
		foreach (DataColumn column in dataTable.Columns)
		{
			column.ReadOnly = true;
		}
		return dataTable;
	}

	protected virtual bool ProcessServer(string host, out string instanceName, out string isClustered, out string version)
	{
		instanceName = (isClustered = (version = string.Empty));
		return true;
	}

	private IList a(int A_0)
	{
		IList list = new ArrayList();
		ArrayList arrayList = new ArrayList();
		IntPtr zero = IntPtr.Zero;
		if (!a(arrayList, zero))
		{
			return null;
		}
		IList result = new ArrayList();
		c = new h[arrayList.Count];
		d = 0;
		int num = 0;
		try
		{
			foreach (string item in arrayList)
			{
				ThreadStart start = a;
				Thread thread = new Thread(start);
				thread.Name = "Devart_DbDataSourceEnumerator_" + item;
				list.Add(thread);
				h h2 = new h();
				h2.a = item;
				h2.c = result;
				h2.b = port;
				thread.IsBackground = true;
				lock (c.SyncRoot)
				{
					c[num++] = h2;
				}
				thread.Start();
			}
			Thread.Sleep(A_0);
			foreach (Thread item2 in list)
			{
				if (item2.ThreadState == ThreadState.Running)
				{
					try
					{
						item2.Abort();
					}
					catch
					{
					}
				}
			}
			return result;
		}
		catch (ThreadAbortException)
		{
			foreach (Thread item3 in list)
			{
				if (item3.ThreadState == ThreadState.Running)
				{
					try
					{
						item3.Abort();
					}
					catch
					{
					}
				}
			}
			throw;
		}
	}

	private static void a()
	{
		h h2;
		lock (c.SyncRoot)
		{
			h2 = c[d++];
		}
		bool flag;
		using (TcpClient tcpClient = new TcpClient())
		{
			tcpClient.NoDelay = false;
			tcpClient.ReceiveTimeout = 50;
			tcpClient.SendTimeout = 50;
			try
			{
				tcpClient.Connect(h2.a, h2.b);
				tcpClient.Close();
				flag = true;
			}
			catch (SocketException)
			{
				flag = false;
			}
		}
		if (flag)
		{
			lock (h2.a)
			{
				h2.c.Add(h2.a);
			}
		}
	}

	private static bool a(IList A_0, IntPtr A_1)
	{
		int A_2 = 16384;
		int A_3 = -1;
		int num = t.WNetOpenEnum(t.c, t.b, 0, A_1, out var A_4);
		if (num != t.a)
		{
			return false;
		}
		IntPtr intPtr = Marshal.AllocHGlobal(A_2);
		try
		{
			do
			{
				if ((num = t.WNetEnumResource(A_4, ref A_3, intPtr, ref A_2)) != t.a)
				{
					continue;
				}
				for (int i = 0; i < A_3; i++)
				{
					IntPtr intPtr2 = ((IntPtr.Size != 8) ? ((IntPtr)(intPtr.ToInt32() + i * Marshal.SizeOf(typeof(t.a)))) : ((IntPtr)(intPtr.ToInt64() + Convert.ToInt64(i * Marshal.SizeOf(typeof(t.a))))));
					t.a a = (t.a)Marshal.PtrToStructure(intPtr2, typeof(t.a));
					if ((a.d & t.e) != 0 && a.c != t.f)
					{
						DbDataSourceEnumerator.a(A_0, intPtr2);
					}
					else
					{
						A_0.Add((a.f.Substring(0, 2) == "\\\\") ? a.f.Substring(2) : a.f);
					}
				}
			}
			while (num != t.d);
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
			num = t.WNetCloseEnum(A_4);
		}
		if (num != t.a)
		{
			return false;
		}
		return true;
	}
}
