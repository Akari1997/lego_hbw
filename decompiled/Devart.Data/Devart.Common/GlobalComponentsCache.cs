using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace Devart.Common;

public class GlobalComponentsCache
{
	private static Hashtable a;

	private static ComponentAddedEventHandler b;

	private static ComponentRemovedEventHandler c;

	public static event ComponentAddedEventHandler ComponentAdded
	{
		add
		{
			b = (ComponentAddedEventHandler)Delegate.Combine(b, value);
		}
		remove
		{
			b = (ComponentAddedEventHandler)Delegate.Remove(b, value);
		}
	}

	public static event ComponentRemovedEventHandler ComponentRemoved
	{
		add
		{
			c = (ComponentRemovedEventHandler)Delegate.Combine(c, value);
		}
		remove
		{
			c = (ComponentRemovedEventHandler)Delegate.Remove(c, value);
		}
	}

	static GlobalComponentsCache()
	{
		a = new Hashtable();
	}

	public static void RemoveFromGlobalList(IComponent component)
	{
		lock (typeof(GlobalComponentsCache))
		{
			if (!(a[component.GetType()] is ArrayList arrayList))
			{
				return;
			}
			IComponent component2 = null;
			string keyString = GetKeyString(component);
			for (int i = 0; i < arrayList.Count; i++)
			{
				k k2 = (k)arrayList[i];
				if (!Utils.GetWeakIsAlive(k2) || (((IComponent)k2.Target).Site == null && Utils.DesignMode))
				{
					arrayList.Remove(k2);
					i--;
					continue;
				}
				IComponent component3 = (IComponent)k2.Target;
				if (component3 == component || GetKeyString(component3) == keyString)
				{
					arrayList.Remove(k2);
					component2 = component3;
					break;
				}
			}
			if (arrayList.Count == 0)
			{
				a.Remove(component.GetType());
			}
			if (component2 != null && c != null)
			{
				c(component2);
			}
		}
	}

	public static bool AddToGlobalList(IComponent component)
	{
		return AddToGlobalList(component, null);
	}

	public static bool AddToGlobalList(IComponent component, string groupName)
	{
		lock (typeof(GlobalComponentsCache))
		{
			ArrayList arrayList = a[component.GetType()] as ArrayList;
			bool flag = arrayList == null;
			if (flag)
			{
				arrayList = new ArrayList();
			}
			string keyString = GetKeyString(component);
			for (int i = 0; i < arrayList.Count; i++)
			{
				k k2 = (k)arrayList[i];
				if (!Utils.GetWeakIsAlive(k2) || (((IComponent)k2.Target).Site == null && Utils.DesignMode))
				{
					arrayList.Remove(k2);
					i--;
					continue;
				}
				IComponent component2 = (IComponent)k2.Target;
				if (component2 == component)
				{
					return false;
				}
				if (GetKeyString(component2) == keyString && k2.a == groupName)
				{
					arrayList[i] = new k(component, groupName);
					return true;
				}
			}
			arrayList.Add(new k(component, groupName));
			if (flag)
			{
				a[component.GetType()] = arrayList;
			}
			if (b != null)
			{
				b(component);
			}
		}
		return true;
	}

	public static IComponent GetObjectByName(string name)
	{
		lock (typeof(GlobalComponentsCache))
		{
			ArrayList arrayList = null;
			foreach (DictionaryEntry item in a)
			{
				ArrayList arrayList2 = item.Value as ArrayList;
				for (int i = 0; i < arrayList2.Count; i++)
				{
					k k2 = (k)arrayList2[i];
					if (!Utils.GetWeakIsAlive(k2) || (((IComponent)k2.Target).Site == null && Utils.DesignMode))
					{
						arrayList2.Remove(k2);
						i--;
						continue;
					}
					IComponent component = (IComponent)k2.Target;
					if (GetKeyString(component) == name)
					{
						return (IComponent)k2.Target;
					}
				}
				if (arrayList2.Count == 0)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(item.Key);
				}
			}
			if (arrayList != null)
			{
				for (int num = 0; num < arrayList.Count; num++)
				{
					a.Remove(arrayList[num]);
				}
			}
			if (!Utils.DesignMode)
			{
				throw new Exception("Cannot find component by name " + name + " in global components cache");
			}
		}
		return null;
	}

	public static string GetKeyString(IComponent component)
	{
		if (component != null)
		{
			PropertyInfo property = component.GetType().GetProperty("Name");
			if ((object)property == null)
			{
				return "";
			}
			string text = (string)property.GetValue(component, null);
			object obj = null;
			if ((component.Site != null && component.Site.DesignMode) || Utils.DesignMode)
			{
				IContainer container;
				if (component.Site != null)
				{
					container = component.Site.Container;
					if (component.Site.Container is INestedContainer && ((INestedContainer)container).Owner.Site != null)
					{
						text = ((INestedContainer)container).Owner.Site.Name + "." + text;
						container = ((INestedContainer)container).Owner.Site.Container;
					}
				}
				else
				{
					container = null;
				}
				if (container != null && container is IDesignerHost designerHost)
				{
					obj = designerHost.RootComponent;
				}
				if (obj == null)
				{
					PropertyInfo property2 = component.GetType().GetProperty("Owner");
					if ((object)property2 != null)
					{
						obj = property2.GetValue(component, null);
					}
				}
				if (obj is IComponent { Site: not null } component2)
				{
					text = component2.Site.Name + "." + text;
				}
			}
			else
			{
				PropertyInfo property3 = component.GetType().GetProperty("Owner");
				if ((object)property3 == null)
				{
					return "";
				}
				obj = property3.GetValue(component, null);
				if (component is DbDataTable dbDataTable)
				{
					text = dbDataTable.FullName;
				}
				string text2 = null;
				Type type = obj.GetType();
				while (text2 == null)
				{
					FieldInfo field = type.GetField(text, BindingFlags.Instance | BindingFlags.NonPublic);
					if ((object)field == null)
					{
						field = type.GetField(text, BindingFlags.Instance | BindingFlags.Public);
					}
					if ((object)field != null)
					{
						text2 = type.ToString();
					}
					if (text2 == null)
					{
						type = type.BaseType;
					}
					if ((object)type == typeof(Component) || type.FullName == "System.Web.UI.Control")
					{
						break;
					}
				}
				if (text2 == null)
				{
					text2 = obj.GetType().ToString();
				}
				text2 = text2.Substring(text2.LastIndexOf(".") + 1);
				text = text2 + "." + text;
			}
			return text;
		}
		if (component != null && component.Site != null)
		{
			return component.Site.Name;
		}
		return "";
	}

	public static ArrayList GetObjects(Type objectType)
	{
		return GetObjects(objectType, null);
	}

	public static ArrayList GetObjects(Type objectType, string groupName)
	{
		lock (typeof(GlobalComponentsCache))
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = null;
			foreach (DictionaryEntry item in a)
			{
				if (!objectType.IsAssignableFrom((Type)item.Key))
				{
					continue;
				}
				ArrayList arrayList3 = (ArrayList)item.Value;
				if (arrayList3 == null)
				{
					continue;
				}
				for (int i = 0; i < arrayList3.Count; i++)
				{
					k k2 = (k)arrayList3[i];
					if (!Utils.GetWeakIsAlive(k2) || (((IComponent)k2.Target).Site == null && Utils.DesignMode))
					{
						arrayList3.Remove(k2);
						i--;
					}
				}
				if (arrayList3.Count == 0)
				{
					if (arrayList2 == null)
					{
						arrayList2 = new ArrayList();
					}
					arrayList2.Add(item.Key);
				}
			}
			if (arrayList2 != null)
			{
				for (int num = 0; num < arrayList2.Count; num++)
				{
					a.Remove(arrayList2[num]);
				}
			}
			foreach (DictionaryEntry item2 in a)
			{
				if (!objectType.IsAssignableFrom((Type)item2.Key))
				{
					continue;
				}
				ArrayList arrayList4 = (ArrayList)item2.Value;
				if (arrayList4 == null)
				{
					continue;
				}
				for (int num2 = 0; num2 < arrayList4.Count; num2++)
				{
					k k3 = (k)arrayList4[num2];
					if (Utils.GetWeakIsAlive(k3) && (((IComponent)k3.Target).Site != null || Utils.DesignMode) && (groupName == null || groupName == "" || k3.a == groupName))
					{
						arrayList.Add(k3.Target);
					}
				}
			}
			return arrayList;
		}
	}
}
