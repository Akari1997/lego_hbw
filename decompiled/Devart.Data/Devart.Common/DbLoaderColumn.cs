using System;
using System.ComponentModel;

namespace Devart.Common;

public class DbLoaderColumn : MarshalByRefObject
{
	private string a = string.Empty;

	private int b;

	private int c;

	private int d;

	public string Name
	{
		get
		{
			return a;
		}
		set
		{
			if (a == null)
			{
				throw new ArgumentNullException("name");
			}
			a = value;
		}
	}

	[DefaultValue(0)]
	public virtual int Size
	{
		get
		{
			return b;
		}
		set
		{
			b = value;
		}
	}

	[DefaultValue(0)]
	public virtual int Precision
	{
		get
		{
			return c;
		}
		set
		{
			c = value;
		}
	}

	[DefaultValue(0)]
	public int Scale
	{
		get
		{
			return d;
		}
		set
		{
			d = value;
		}
	}

	public DbLoaderColumn()
	{
	}

	public DbLoaderColumn(string name, int size, int precision, int scale)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		a = name;
		b = size;
		c = precision;
		d = scale;
	}

	public override string ToString()
	{
		return Name;
	}
}
