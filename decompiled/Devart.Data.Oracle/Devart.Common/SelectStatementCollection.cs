using System.Collections;

namespace Devart.Common;

internal sealed class SelectStatementCollection
{
	private readonly ArrayList a = new ArrayList();

	private readonly SelectStatementNode b = new SelectStatementNode();

	private string c;

	private readonly IList d;

	public IList List => d;

	public SelectStatementNode Node => b;

	public IList Removed => a;

	public string SqlPrefix => c;

	public SelectStatementCollection(IList collection, string sqlPrefix)
	{
		d = collection;
		c = sqlPrefix;
	}

	public void ResetSqlPrefix()
	{
		c = string.Empty;
	}

	public void AddDeleted(int index)
	{
		if (!((SelectStatementNode)d[index]).Binded)
		{
			return;
		}
		SelectStatementNode selectStatementNode = new SelectStatementNode();
		int num = -1;
		int num2 = -1;
		for (int num3 = index + 1; num3 < d.Count; num3++)
		{
			if (((SelectStatementNode)d[num3]).Binded)
			{
				num = num3;
				break;
			}
		}
		for (int num4 = index - 1; num4 > -1; num4--)
		{
			if (((SelectStatementNode)d[num4]).Binded)
			{
				num2 = num4;
				break;
			}
		}
		if (num != -1)
		{
			selectStatementNode.a = ((SelectStatementNode)d[index]).a;
			selectStatementNode.b = ((SelectStatementNode)d[num]).a;
		}
		else if (num2 != -1)
		{
			selectStatementNode.a = ((SelectStatementNode)d[num2]).b + 1;
			selectStatementNode.b = ((SelectStatementNode)d[index]).b + 1;
		}
		else
		{
			selectStatementNode.a = ((SelectStatementNode)d[index]).a;
			selectStatementNode.b = ((SelectStatementNode)d[index]).b + 1;
		}
		a.Add(selectStatementNode);
	}
}
