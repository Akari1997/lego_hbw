using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Devart.Data.Oracle;

[Obfuscation]
public class OracleNumberMappingCollection : IList<OracleNumberMapping>, IList, ICloneable
{
	internal bool a;

	private List<OracleNumberMapping> b;

	private int m_c = 4;

	private EventHandler d;

	public OracleNumberMapping this[int index]
	{
		get
		{
			if (Count == 0)
			{
				throw new IndexOutOfRangeException();
			}
			return List[index];
		}
		set
		{
			if (Count == 0)
			{
				throw new IndexOutOfRangeException();
			}
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			if (List[index] != value)
			{
				value.PropertyChanged -= a;
				List[index] = value;
				value.PropertyChanged += a;
			}
		}
	}

	public int Count
	{
		get
		{
			if (b == null)
			{
				return 0;
			}
			return List.Count;
		}
	}

	private List<OracleNumberMapping> List
	{
		get
		{
			if (b == null)
			{
				b = new List<OracleNumberMapping>(this.m_c);
			}
			return b;
		}
	}

	bool IList.IsFixedSize => false;

	bool IList.IsReadOnly => this.a;

	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			a(value);
			this[index] = (OracleNumberMapping)value;
		}
	}

	bool ICollection.IsSynchronized => false;

	object ICollection.SyncRoot => this;

	bool ICollection<OracleNumberMapping>.IsReadOnly => false;

	internal event EventHandler CollectionChanged
	{
		add
		{
			EventHandler eventHandler = d;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref d, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler eventHandler = d;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref d, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	public OracleNumberMappingCollection()
	{
	}

	public OracleNumberMappingCollection(int capacity)
	{
		this.m_c = capacity;
	}

	public OracleNumberMappingCollection(IEnumerable<OracleNumberMapping> values)
	{
		AddRange(values);
	}

	public void Add(OracleNumberMapping value)
	{
		if (value == null)
		{
			throw new ArgumentNullException();
		}
		c();
		List.Add(value);
		value.PropertyChanged += a;
		a();
	}

	public OracleNumberMapping Add(OracleNumberType numberType, int precision, Type valueType)
	{
		OracleNumberMapping oracleNumberMapping = new OracleNumberMapping(numberType, precision, valueType);
		Add(oracleNumberMapping);
		return oracleNumberMapping;
	}

	public OracleNumberMapping Add(OracleNumberType numberType, int fromPrecision, int toPrecision, Type valueType)
	{
		OracleNumberMapping oracleNumberMapping = new OracleNumberMapping(numberType, fromPrecision, toPrecision, valueType);
		Add(oracleNumberMapping);
		return oracleNumberMapping;
	}

	public void AddRange(IEnumerable values)
	{
		if (values == null)
		{
			throw new ArgumentNullException();
		}
		c();
		foreach (object value in values)
		{
			a(value);
			OracleNumberMapping oracleNumberMapping = (OracleNumberMapping)value;
			oracleNumberMapping.PropertyChanged += a;
			List.Add(oracleNumberMapping);
		}
		a();
	}

	public void AddRange(IEnumerable<OracleNumberMapping> values)
	{
		if (values == null)
		{
			throw new ArgumentNullException();
		}
		foreach (OracleNumberMapping value in values)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			value.PropertyChanged += a;
		}
		List.AddRange(values);
		a();
	}

	public void Clear()
	{
		c();
		if (List.Count == 0)
		{
			return;
		}
		foreach (OracleNumberMapping item in List)
		{
			item.PropertyChanged -= a;
		}
		List.Clear();
		a();
	}

	public bool Contains(OracleNumberMapping value)
	{
		if (Count == 0)
		{
			return false;
		}
		return List.Contains(value);
	}

	public int IndexOf(OracleNumberMapping value)
	{
		if (Count == 0)
		{
			return -1;
		}
		return List.IndexOf(value);
	}

	public void Insert(int index, OracleNumberMapping value)
	{
		if (value == null)
		{
			throw new ArgumentNullException();
		}
		c();
		List.Insert(index, value);
		value.PropertyChanged += a;
		a();
	}

	public void Remove(OracleNumberMapping value)
	{
		if (value == null)
		{
			throw new ArgumentNullException();
		}
		c();
		if (Count != 0)
		{
			value.PropertyChanged -= a;
			List.Remove(value);
			a();
		}
	}

	public void RemoveAt(int index)
	{
		c();
		if (Count != 0)
		{
			List[index].PropertyChanged -= a;
			List.RemoveAt(index);
			a();
		}
	}

	private void c()
	{
		if (this.a)
		{
			throw new InvalidOperationException("Modifications are not allowed after using NumberMappingCollection in OracleCommand. Please create a new instance of " + typeof(OracleNumberMappingCollection).Name);
		}
	}

	private void a(object A_0)
	{
		if (A_0 == null)
		{
			throw new ArgumentNullException();
		}
		if (!typeof(OracleNumberMapping).IsAssignableFrom(A_0.GetType()))
		{
			throw new InvalidCastException($"Cannot convert type {A_0.GetType().Name} to type OracleNumberMapping.");
		}
	}

	public OracleNumberMappingCollection Clone()
	{
		OracleNumberMappingCollection oracleNumberMappingCollection = new OracleNumberMappingCollection();
		foreach (OracleNumberMapping item in List)
		{
			oracleNumberMappingCollection.Add((OracleNumberMapping)item.Clone());
		}
		return oracleNumberMappingCollection;
	}

	object ICloneable.Clone()
	{
		return Clone();
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as OracleNumberMappingCollection);
	}

	public bool Equals(OracleNumberMappingCollection other)
	{
		if (other == null)
		{
			return false;
		}
		if (Count != other.Count)
		{
			return false;
		}
		if (Count == 0)
		{
			return true;
		}
		for (int j = 0; j < Count; j++)
		{
			if (!this[j].Equals(other[j]))
			{
				return false;
			}
		}
		return true;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (Count > 1)
		{
			stringBuilder.Append("(");
		}
		foreach (OracleNumberMapping item in List)
		{
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.AppendFormat(null, "({0})", new object[1] { item.a() });
		}
		if (Count > 1)
		{
			stringBuilder.Append(")");
		}
		return stringBuilder.ToString();
	}

	public static OracleNumberMappingCollection Parse(string value)
	{
		if (a(value, out var A_, out var A_2))
		{
			return A_;
		}
		throw new FormatException(A_2);
	}

	public static bool TryParse(string value, out OracleNumberMappingCollection numberMappings)
	{
		string A_;
		return a(value, out numberMappings, out A_);
	}

	private static bool a(string A_0, out OracleNumberMappingCollection A_1, out string A_2)
	{
		A_1 = new OracleNumberMappingCollection();
		A_2 = "String was not recognized as a valid OracleNumberMappingCollection.";
		A_0 = A_0.Trim();
		if (A_0.Length < 4)
		{
			return false;
		}
		if (A_0[0] == '(' && A_0[1] == '(' && A_0[A_0.Length - 1] == ')' && A_0[A_0.Length - 2] == ')')
		{
			A_0 = A_0.Substring(1, A_0.Length - 2);
		}
		MatchCollection matchCollection = Regex.Matches(A_0, "\\s*\\(\\s*(\\w+)\\s*,\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*,\\s*([A-Za-z0-9.]+)\\s*\\)\\s*", RegexOptions.CultureInvariant);
		if (matchCollection.Count == 0)
		{
			return false;
		}
		foreach (Match item in matchCollection)
		{
			if (!item.Success)
			{
				A_1 = null;
				return false;
			}
			if (!OracleNumberMapping.a(item.Groups[0].Value, out var A_3, out var A_4))
			{
				A_2 = A_4;
				A_1 = null;
				return false;
			}
			A_1.Add(A_3);
		}
		return true;
	}

	int IList.Add(object value)
	{
		a(value);
		Add((OracleNumberMapping)value);
		return IndexOf((OracleNumberMapping)value);
	}

	void IList.Remove(object value)
	{
		a(value);
		Remove((OracleNumberMapping)value);
	}

	bool IList.Contains(object value)
	{
		return Contains(value as OracleNumberMapping);
	}

	int IList.IndexOf(object value)
	{
		return IndexOf(value as OracleNumberMapping);
	}

	void IList.Insert(int index, object value)
	{
		a(value);
		Insert(index, (OracleNumberMapping)value);
	}

	void ICollection.CopyTo(Array array, int index)
	{
		((ICollection<OracleNumberMapping>)this).CopyTo((OracleNumberMapping[])array, index);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return List.GetEnumerator();
	}

	void ICollection<OracleNumberMapping>.CopyTo(OracleNumberMapping[] array, int arrayIndex)
	{
		List.CopyTo(array, arrayIndex);
	}

	bool ICollection<OracleNumberMapping>.Remove(OracleNumberMapping item)
	{
		Remove(item);
		return true;
	}

	public IEnumerator<OracleNumberMapping> GetEnumerator()
	{
		return List.GetEnumerator();
	}

	private void a()
	{
		if (d != null)
		{
			d(this, new EventArgs());
		}
	}

	private void a(object A_0, PropertyChangedEventArgs A_1)
	{
		a();
	}
}
