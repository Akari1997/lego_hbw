using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing.Design;
using System.Reflection;

namespace Devart.Common;

[Serializable]
public abstract class DataSetToolboxItem : ToolboxItem
{
	protected abstract string ProviderPrefix { get; }

	protected abstract string ProviderName { get; }

	protected abstract string ProviderRegKey { get; }

	protected DataSetToolboxItem()
	{
	}

	protected DataSetToolboxItem(Type type)
		: base(type)
	{
	}

	protected override IComponent[] CreateComponentsCore(IDesignerHost host)
	{
		MethodInfo methodInfo = (Assembly.LoadWithPartialName("Devart.Data.Design")?.GetType("Devart.Common.Design.DataSetToolboxItemDialog"))?.GetMethod("CreateComponent", new Type[5]
		{
			typeof(IDesignerHost),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(DataSet)
		});
		if ((object)methodInfo != null)
		{
			return (IComponent[])methodInfo.Invoke(null, new object[5]
			{
				host,
				ProviderPrefix,
				ProviderName,
				ProviderRegKey,
				CreateDataSet()
			});
		}
		return new IComponent[1] { CreateDataSet() };
	}

	protected abstract DataSet CreateDataSet();
}
