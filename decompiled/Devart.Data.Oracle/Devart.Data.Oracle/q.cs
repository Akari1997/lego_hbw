using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Reflection;
using Devart.Common;

namespace Devart.Data.Oracle;

internal class q : DbMetaDataFactory
{
	private string m_a;

	private static Stream b()
	{
		return Assembly.GetExecutingAssembly().GetManifestResourceStream("Devart.Data.Oracle.MetaData.xml");
	}

	public q(string A_0, string A_1)
		: base(b(), A_0, A_1)
	{
		this.m_a = A_1;
	}

	public virtual DataTable a(DbConnection A_0, DbConnectionInternal A_1, string A_2, string[] A_3)
	{
		if (Utils.IsEmpty(A_2))
		{
			return p(A_0, A_3);
		}
		return A_2.ToLower(CultureInfo.InvariantCulture) switch
		{
			"restrictions" => b(A_0), 
			"metadatacollections" => p(A_0, A_3), 
			"reservedwords" => f((DbConnectionBase)A_0, A_3), 
			"users" => o(A_0, A_3), 
			"tables" => e((DbConnectionBase)A_0, A_3), 
			"ssistables" => n(A_0, A_3), 
			"views" => m(A_0, A_3), 
			"columns" => d((DbConnectionBase)A_0, A_3), 
			"ssiscolumns" => c((DbConnectionBase)A_0, A_3), 
			"indexes" => b((DbConnectionBase)A_0, A_3), 
			"indexcolumns" => a((DbConnectionBase)A_0, A_3), 
			"functions" => l(A_0, A_3), 
			"procedures" => k(A_0, A_3), 
			"arguments" => j(A_0, A_3), 
			"synonyms" => i(A_0, A_3), 
			"sequences" => h(A_0, A_3), 
			"packages" => g(A_0, A_3), 
			"packagebodies" => f(A_0, A_3), 
			"primarykeys" => v(A_0, A_3), 
			"primarykeycolumns" => u(A_0, A_3), 
			"foreignkeys" => e(A_0, A_3), 
			"foreignkeycolumns" => d(A_0, A_3), 
			"fullforeignkeycolumns" => c(A_0, A_3), 
			"triggers" => b(A_0, A_3), 
			"clusters" => a(A_0, A_3), 
			"datasourceinformation" => a(A_0), 
			"datatypes" => a(), 
			_ => throw new ArgumentException(Devart.Common.al.a("RequestedCollectionNotDefined", A_2)), 
		};
	}

	private DataTable v(DbConnection A_0, string[] A_1)
	{
		string a_ = "select   OWNER \"Schema\", TABLE_NAME \"TableName\",   CONSTRAINT_NAME \"Name\" FROM SYS.ALL_CONSTRAINTS I WHERE (I.CONSTRAINT_TYPE = 'P') AND OWNER={0} AND TABLE_NAME LIKE {1} AND CONSTRAINT_NAME LIKE {2} ";
		return a(A_0, "PrimaryKeys", a_, A_1, new string[3] { "OWNER", "TABLE_NAME", "CONSTRAINT_NAME" });
	}

	private DataTable u(DbConnection A_0, string[] A_1)
	{
		string a_ = "SELECT OWNER \"Schema\", CONSTRAINT_NAME \"ConstraintName\", TABLE_NAME \"TableName\", COLUMN_NAME \"Name\", POSITION \"Position\" FROM SYS.ALL_CONS_COLUMNS WHERE OWNER={0} AND CONSTRAINT_NAME LIKE {1} AND COLUMN_NAME LIKE {2}";
		return a(A_0, "PrimaryKeyColumns", a_, A_1, new string[3] { "OWNER", "CONSTRAINT_NAME", "COLUMN_NAME" });
	}

	private DataTable t(DbConnection A_0, string[] A_1)
	{
		string a_ = "SELECT OWNER AS \"Owner\", QUEUE_NAME AS \"QueueName\", QUEUE_TABLE AS \"QueueTableName\",  CONSUMER_NAME AS \"Agent\", ADDRESS AS \"Adress\", TRANSFORMATION AS \"Transformation\", DELIVERY_MODE AS \"Delivery\", QUEUE_TO_QUEUE AS \"QueueToQueue\" FROM SYS.ALL_QUEUE_SUBSCRIBERS WHERE OWNER LIKE {0} AND QUEUE_NAME LIKE {1} AND QUEUE_TABLE LIKE {2} AND CONSUMER_NAME LIKE {3} ";
		return a(A_0, "QueueSubscribers", a_, A_1, new string[4] { "OWNER", "QUEUE_NAME", "QUEUE_TABLE", "CONSUMER_NAME" });
	}

	private DataTable s(DbConnection A_0, string[] A_1)
	{
		string a_ = "SELECT QUEUE_OWNER AS \"QueueOwner\", QUEUE_NAME  AS \"QueueName\", PUBLISHER_NAME AS \"Name\", PUBLISHER_ADDRESS AS  \"Address\", PUBLISHER_RULE AS \"Rule\", PUBLISHER_RULE_NAME AS \"RuleName\", PUBLISHER_RULESET AS \"Ruleset\", PUBLISHER_TRANSFORMATION AS \"Transformation\" FROM SYS.ALL_QUEUE_PUBLISHERS WHERE QUEUE_OWNER LIKE {0} AND QUEUE_NAME LIKE {1} AND PUBLISHER_NAME LIKE {2}";
		return a(A_0, "QueuePublishers", a_, A_1, new string[3] { "QUEUE_OWNER", "QUEUE_NAME", "PUBLISHER_NAME" });
	}

	private DataTable r(DbConnection A_0, string[] A_1)
	{
		string a_ = "SELECT QU.OWNER AS \"Owner\", QU.NAME AS \"QueueName\", QU.QUEUE_TABLE AS \"QueueTableName\", QU.QUEUE_TYPE AS \"QueueType\", QU.MAX_RETRIES AS \"MaxRetries\", QU.RETRY_DELAY AS \"RetryDelay\", QU.ENQUEUE_ENABLED AS \"EnqueueEnabled\", QU.DEQUEUE_ENABLED AS \"DequeueEnabled\", QU.RETENTION AS \"RetentionTime\", QU.USER_COMMENT AS \"Comment\", TAB.TYPE AS \"PayloadType\", TAB.OBJECT_TYPE AS \"ObjectTypeName\" FROM SYS.ALL_QUEUES QU LEFT OUTER JOIN SYS.ALL_QUEUE_TABLES TAB ON QU.QUEUE_TABLE = TAB.QUEUE_TABLE WHERE QU.OWNER LIKE {0} AND TAB.OWNER LIKE {0} AND QU.NAME LIKE {1} AND QU.QUEUE_TABLE LIKE {2} AND TAB.TYPE  LIKE {3} AND TAB.OBJECT_TYPE LIKE {4} ";
		return a(A_0, "Queues", a_, A_1, new string[5] { "QU.OWNER", "QU.NAME", "QU.QUEUE_TABLE", "TAB.TYPE", "TAB.OBJECT_TYPE" });
	}

	private DataTable q(DbConnection A_0, string[] A_1)
	{
		string a_ = "SELECT OWNER AS \"Owner\", QUEUE_TABLE AS \"QueueTableName\", TYPE AS \"PayloadType\", OBJECT_TYPE AS \"ObjectTypeName\", SORT_ORDER AS \"SortOrder\", RECIPIENTS AS \"MultipleConsumers\", MESSAGE_GROUPING AS \"MessageGrouping\", COMPATIBLE AS \"Compatible\", PRIMARY_INSTANCE AS \"PrimaryInstance\", SECONDARY_INSTANCE AS \"SecondaryInstance\", USER_COMMENT AS \"Comment\", SECURE AS \"Secure\" FROM SYS.ALL_QUEUE_TABLES WHERE OWNER LIKE {0} AND QUEUE_TABLE LIKE {1} AND TYPE LIKE {2} AND OBJECT_TYPE LIKE {3}";
		return a(A_0, "QueueTables", a_, A_1, new string[4] { "OWNER", "QUEUE_TABLE", "TYPE", "OBJECT_TYPE" });
	}

	private DataTable b(DbConnection A_0)
	{
		DataTable dataTable = new DataTable("Restrictions");
		dataTable.Columns.Add("CollectionName", typeof(string));
		dataTable.Columns.Add("RestrictionName", typeof(string));
		dataTable.Columns.Add("ParameterName", typeof(string));
		dataTable.Columns.Add("RestrictionDefault", typeof(string));
		dataTable.Columns.Add("RestrictionNumber", typeof(int));
		_ = dataTable.Columns.Count;
		object[] array = new object[64]
		{
			new object[5] { "Users", "UserName", "NAME", null, 1 },
			new object[5] { "Tables", "Owner", "OWNER", null, 1 },
			new object[5] { "Tables", "Table", "TABLENAME", null, 2 },
			new object[5] { "Views", "Owner", "OWNER", null, 1 },
			new object[5] { "Views", "View", "VIEWNAME", null, 2 },
			new object[5] { "Columns", "Owner", "OWNER", null, 1 },
			new object[5] { "Columns", "Table", "TABLENAME", null, 2 },
			new object[5] { "Columns", "Column", "COLUMNNAME", null, 3 },
			new object[5] { "Indexes", "Owner", "OWNER", null, 1 },
			new object[5] { "Indexes", "Name", "INDEXNAME", null, 2 },
			new object[5] { "Indexes", "TableOwner", "TABLEOWNER", null, 3 },
			new object[5] { "Indexes", "TableName", "TABLENAME", null, 4 },
			new object[5] { "IndexColumns", "Owner", "OWNER", null, 1 },
			new object[5] { "IndexColumns", "Name", "INDEXNAME", null, 2 },
			new object[5] { "IndexColumns", "TableOwner", "TABLEOWNER", null, 3 },
			new object[5] { "IndexColumns", "TableName", "TABLENAME", null, 4 },
			new object[5] { "IndexColumns", "Column", "COLUMNNAME", null, 5 },
			new object[5] { "Functions", "Owner", "OWNER", null, 1 },
			new object[5] { "Functions", "Name", "OBJECTNAME", null, 2 },
			new object[5] { "Procedures", "Owner", "OWNER", null, 1 },
			new object[5] { "Procedures", "PackageName", "OBJECTNAME", null, 2 },
			new object[5] { "Procedures", "Name", "PROCEDURENAME", null, 3 },
			new object[5] { "Arguments", "Owner", "OWNER", null, 1 },
			new object[5] { "Arguments", "PackageName", "PACKAGENAME", null, 2 },
			new object[5] { "Arguments", "ObjectName", "OBJECTNAME", null, 3 },
			new object[5] { "Arguments", "ArgumentName", "ARGUMENTNAME", null, 4 },
			new object[5] { "Synonyms", "Owner", "OWNER", null, 1 },
			new object[5] { "Synonyms", "Synonym", "SYNONYMNAME", null, 2 },
			new object[5] { "Sequences", "Owner", "OWNER", null, 1 },
			new object[5] { "Sequences", "Sequence", "SEQUENCENAME", null, 2 },
			new object[5] { "Packages", "Owner", "OWNER", null, 1 },
			new object[5] { "Packages", "Name", "PACKAGENAME", null, 2 },
			new object[5] { "PrimaryKeys", "Owner", "OWNER", null, 1 },
			new object[5] { "PrimaryKeys", "TableName", "TABLE_NAME", null, 2 },
			new object[5] { "PrimaryKeys", "Name", "CONSTRAINT_NAME", null, 3 },
			new object[5] { "PrimaryKeyColumns", "Owner", "OWNER", null, 1 },
			new object[5] { "PrimaryKeyColumns", "ConstraintName", "CONSTRAINT_NAME", null, 2 },
			new object[5] { "PrimaryKeyColumns", "Name", "COLUMN_NAME", null, 3 },
			new object[5] { "PackageBodies", "Owner", "OWNER", null, 1 },
			new object[5] { "PackageBodies", "Name", "PKGBODYNAME", null, 2 },
			new object[5] { "ForeignKeys", "Foreign_Key_Owner", "OWNER", null, 1 },
			new object[5] { "ForeignKeys", "Foreign_Key_Table_Name", "TABLENAME", null, 2 },
			new object[5] { "ForeignKeys", "Foreign_Key_Constraint_Name", "CONSTRAINTNAME", null, 3 },
			new object[5] { "ForeignKeyColumns", "Owner", "OWNER", null, 1 },
			new object[5] { "ForeignKeyColumns", "Table_Name", "TABLENAME", null, 2 },
			new object[5] { "ForeignKeyColumns", "Constraint_Name", "CONSTRAINTNAME", null, 3 },
			new object[5] { "Triggers", "Owner", "OWNER", null, 1 },
			new object[5] { "Triggers", "TriggerName", "TRIGGERNAME", null, 2 },
			new object[5] { "Triggers", "TableOwner", "TABLEOWNER", null, 3 },
			new object[5] { "Triggers", "TableName", "TABLENAME", null, 4 },
			new object[5] { "Clusters", "Owner", "OWNER", null, 1 },
			new object[5] { "Clusters", "Name", "CLUSTERNAME", null, 2 },
			new object[5] { "Queues", "Owner", "OWNER", null, 1 },
			new object[5] { "Queues", "QueueName", "QUEUE_NAME", null, 2 },
			new object[5] { "Queues", "QueueTableName", "QUEUE_TABLE", null, 3 },
			new object[5] { "Queues", "PayloadType", "TYPE", null, 4 },
			new object[5] { "Queues", "ObjectTypeName", "OBJECT_TYPE", null, 5 },
			new object[5] { "QueueTables", "Owner", "OWNER", null, 1 },
			new object[5] { "QueueTables", "QueueTableName", "QUEUE_TABLE", null, 2 },
			new object[5] { "QueueTables", "PayloadType", "TYPE", null, 3 },
			new object[5] { "QueueTables", "ObjectTypeName", "OBJECT_TYPE", null, 4 },
			new object[5] { "QueuePublishers", "Owner", "QUEUE_OWNER", null, 1 },
			new object[5] { "QueuePublishers", "QueueName", "QUEUE_NAME", null, 2 },
			new object[5] { "QueuePublishers", "Name", "PUBLISHER_NAME", null, 3 }
		};
		for (int num = 0; num < array.Length; num++)
		{
			dataTable.Rows.Add((object[])array[num]);
		}
		if (string.Compare(this.m_a, "10.02") > 0)
		{
			object[] array2 = new object[4]
			{
				new object[5] { "QueueSubscribers", "Owner", "OWNER", null, 1 },
				new object[5] { "QueueSubscribers", "QueueName", "QUEUE_NAME", null, 2 },
				new object[5] { "QueueSubscribers", "QueueTableName", "QUEUE_TABLE", null, 3 },
				new object[5] { "QueueSubscribers", "Agent", "CONSUMER_NAME", null, 4 }
			};
			for (int num2 = 0; num2 < array2.Length; num2++)
			{
				dataTable.Rows.Add((object[])array2[num2]);
			}
		}
		return dataTable;
	}

	private DataTable a()
	{
		DataTable dataTable = new DataTable("DataTypes");
		dataTable.ReadXml(Assembly.GetExecutingAssembly().GetManifestResourceStream("Devart.Data.Oracle.OracleDataTypes.xml"));
		return dataTable;
	}

	private DataTable a(DbConnection A_0)
	{
		DataTable dataTable = new DataTable("DataSourceInformation");
		dataTable.Columns.Add("CompositeIdentifierSeparatorPattern", typeof(string));
		dataTable.Columns.Add("DataSourceProductName", typeof(string));
		dataTable.Columns.Add("DataSourceProductVersion", typeof(string));
		dataTable.Columns.Add("DataSourceProductVersionNormalized", typeof(string));
		dataTable.Columns.Add("GroupByBehavior", typeof(GroupByBehavior));
		dataTable.Columns.Add("IdentifierPattern", typeof(string));
		dataTable.Columns.Add("IdentifierCase", typeof(IdentifierCase));
		dataTable.Columns.Add("OrderByColumnsInSelect", typeof(bool));
		dataTable.Columns.Add("ParameterMarkerFormat", typeof(string));
		dataTable.Columns.Add("ParameterMarkerPattern", typeof(string));
		dataTable.Columns.Add("ParameterNameMaxLength", typeof(int));
		dataTable.Columns.Add("ParameterNamePattern", typeof(string));
		dataTable.Columns.Add("QuotedIdentifierPattern", typeof(string));
		dataTable.Columns.Add("QuotedIdentifierCase", typeof(IdentifierCase));
		dataTable.Columns.Add("StatementSeparatorPattern", typeof(string));
		dataTable.Columns.Add("StringLiteralPattern", typeof(string));
		dataTable.Columns.Add("SupportedJoinOperators", typeof(SupportedJoinOperators));
		object[] values = new object[dataTable.Columns.Count];
		DataRow dataRow = dataTable.Rows.Add(values);
		dataRow["CompositeIdentifierSeparatorPattern"] = "@|\\.";
		dataRow["DataSourceProductName"] = "Oracle Server";
		dataRow["DataSourceProductVersion"] = A_0.ServerVersion;
		dataRow["DataSourceProductVersionNormalized"] = ((OracleConnection)A_0).d().ServerVersionNormalized;
		dataRow["GroupByBehavior"] = GroupByBehavior.MustContainAll;
		dataRow["IdentifierPattern"] = "^[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}__#$][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}__#$]*$";
		dataRow["IdentifierCase"] = IdentifierCase.Insensitive;
		dataRow["OrderByColumnsInSelect"] = false;
		dataRow["ParameterMarkerFormat"] = ":{0}";
		dataRow["ParameterMarkerPattern"] = ":([\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}__#$][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}__#$]*)";
		dataRow["ParameterNameMaxLength"] = "30";
		dataRow["ParameterNamePattern"] = "^[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}__#$][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}__#$]*$";
		dataRow["QuotedIdentifierPattern"] = "\"^(([^\"]|\"\")*)$\"";
		dataRow["QuotedIdentifierCase"] = IdentifierCase.Sensitive;
		dataRow["StatementSeparatorPattern"] = null;
		dataRow["StringLiteralPattern"] = "'(([^']|'')*)'";
		dataRow["SupportedJoinOperators"] = SupportedJoinOperators.Inner | SupportedJoinOperators.LeftOuter | SupportedJoinOperators.RightOuter | SupportedJoinOperators.FullOuter;
		return dataTable;
	}

	private DataTable p(DbConnection A_0, string[] A_1)
	{
		DataTable dataTable = new DataTable("MetaDataCollections");
		dataTable.Columns.Add("CollectionName", typeof(string));
		dataTable.Columns.Add("NumberOfRestrictions", typeof(int));
		dataTable.Columns.Add("NumberOfIdentifierParts", typeof(int));
		dataTable.Rows.Add("MetaDataCollections", 0, 0);
		dataTable.Rows.Add("Restrictions", 0, 0);
		dataTable.Rows.Add("ReservedWords", 0, 0);
		dataTable.Rows.Add("Users", 1, 0);
		dataTable.Rows.Add("Tables", 2, 0);
		dataTable.Rows.Add("Views", 2, 0);
		dataTable.Rows.Add("Columns", 3, 0);
		dataTable.Rows.Add("Indexes", 4, 0);
		dataTable.Rows.Add("IndexColumns", 5, 0);
		dataTable.Rows.Add("Functions", 2, 0);
		dataTable.Rows.Add("Procedures", 3, 0);
		dataTable.Rows.Add("Arguments", 4, 0);
		dataTable.Rows.Add("Synonyms", 2, 0);
		dataTable.Rows.Add("Sequences", 2, 0);
		dataTable.Rows.Add("Packages", 2, 0);
		dataTable.Rows.Add("PackageBodies", 2, 0);
		dataTable.Rows.Add("PrimaryKeys", 3, 0);
		dataTable.Rows.Add("PrimaryKeyColumns", 3, 0);
		dataTable.Rows.Add("ForeignKeys", 3, 0);
		dataTable.Rows.Add("ForeignKeyColumns", 3, 0);
		dataTable.Rows.Add("Triggers", 2, 0);
		dataTable.Rows.Add("Clusters", 2, 0);
		dataTable.Rows.Add("QueuePublishers", 3, 0);
		dataTable.Rows.Add("Queues", 5, 0);
		dataTable.Rows.Add("QueueTables", 4, 0);
		if (string.Compare(this.m_a, "10.02") > 0)
		{
			dataTable.Rows.Add("QueueSubscribers", 4, 0);
		}
		return dataTable;
	}

	private DataTable f(DbConnectionBase A_0, string[] A_1)
	{
		DataTable dataTable = new DataTable("ReservedWords");
		if (A_0.IsNHibernate)
		{
			dataTable.Columns.Add("ReservedWord", typeof(string));
		}
		else
		{
			dataTable.Columns.Add("Name", typeof(string));
		}
		foreach (string key in an.am.Keys)
		{
			dataTable.Rows.Add(key);
		}
		return dataTable;
	}

	private DataTable o(DbConnection A_0, string[] A_1)
	{
		return a(A_0, "Users", "SELECT USERNAME AS \"Name\", USER_ID AS \"Id\", CREATED AS \"Created\" FROM SYS.ALL_USERS WHERE USERNAME LIKE {0} ORDER BY USERNAME", A_1, new string[1] { "UserName" });
	}

	private DataTable e(DbConnectionBase A_0, string[] A_1)
	{
		string text = ((string.Compare(this.m_a, "10.01.00.02") < 0) ? "" : "DROPPED <> 'YES' AND ");
		string text2 = "ALL_TABLES";
		string text3;
		string text4;
		if (string.Compare(this.m_a, "08") < 0)
		{
			text3 = "";
			text4 = "";
		}
		else if (string.Compare(this.m_a, "08.01") < 0)
		{
			text3 = "";
			text4 = ", TEMPORARY AS \"Temporary\", NESTED AS \"Nested\", BUFFER_POOL AS \"BufferPool\"";
		}
		else if (string.Compare(this.m_a, "08.01.07") < 0)
		{
			text3 = "";
			text4 = ", TEMPORARY AS \"Temporary\", NESTED AS \"Nested\", BUFFER_POOL AS \"BufferPool\", DURATION AS \"Duration\", SKIP_CORRUPT AS \"SkipCorrupt\"";
		}
		else
		{
			text3 = ", CLUSTER_OWNER AS \"ClusterSchema\"";
			text4 = ", TEMPORARY AS \"Temporary\", NESTED AS \"Nested\", BUFFER_POOL AS \"BufferPool\", DURATION AS \"Duration\", SKIP_CORRUPT AS \"SkipCorrupt\"";
			text2 = "all_all_tables";
		}
		string text5 = "";
		string text6 = "";
		string text7 = "";
		if (A_1 != null && A_1.Length > 2 && A_1[2].ToLower() == "comments")
		{
			text5 = ", t2.COMMENTS AS \"Comments\"";
			text6 = ", SYS.all_tab_comments t2";
			text7 = "t1.owner = t2.owner AND t1.table_name = t2.table_name AND ";
			string[] array = new string[A_1.Length - 1];
			for (int num = 0; num < array.Length; num++)
			{
				array[num] = A_1[num];
			}
			A_1 = array;
		}
		string a_ = "SELECT t1.OWNER AS \"Schema\", t1.TABLE_NAME AS \"Name\", DECODE(t1.OWNER, 'SYS', 'System', 'SYSTEM', 'System', 'SYSMAN', 'System','CTXSYS', 'System','MDSYS', 'System','OLAPSYS', 'System', 'ORDSYS', 'System','OUTLN', 'System', 'WKSYS', 'System','WMSYS', 'System','XDB', 'System','ORDPLUGINS', 'System','User') AS \"Type\"" + text3 + ", CLUSTER_NAME AS \"ClusterName\", BACKED_UP AS \"BackedUp\", CACHE AS \"Cache\", TABLE_LOCK AS \"TableLock\"" + text4 + text5 + " FROM SYS." + text2 + " t1" + text6 + " WHERE " + text7 + text + "t1.OWNER={0} AND t1.TABLE_NAME LIKE {1} ORDER BY t1.OWNER, t1.TABLE_NAME";
		DataTable dataTable = a(A_0, "Tables", a_, A_1, new string[2] { "t1.OWNER", "t1.TABLE_NAME" });
		if (A_0.IsNHibernate)
		{
			dataTable.Columns[0].ColumnName = "OWNER";
			dataTable.Columns[1].ColumnName = "TABLE_NAME";
		}
		return dataTable;
	}

	private DataTable n(DbConnection A_0, string[] A_1)
	{
		string userId = ((OracleConnection)A_0).UserId;
		if (A_0 is OracleConnection && A_1 == null)
		{
			A_1 = new string[2]
			{
				(userId[0] == '"') ? userId : userId.ToUpper(),
				null
			};
		}
		string a_ = "SELECT OWNER AS \"Schema\", TABLE_NAME AS \"Name\", DECODE(OWNER, 'SYS', 'System', 'SYSTEM', 'System', 'SYSMAN', 'System','CTXSYS', 'System','MDSYS', 'System','OLAPSYS', 'System', 'ORDSYS', 'System','OUTLN', 'System', 'WKSYS', 'System','WMSYS', 'System','XDB', 'System','ORDPLUGINS', 'System','User') AS \"TableType\"FROM SYS.all_all_tables WHERE OWNER= '" + A_1[0] + "' AND TABLE_NAME LIKE '%' UNION SELECT OWNER AS \"Schema\",VIEW_NAME AS \"Name\", 'View' AS \"TableType\" FROM SYS.ALL_VIEWS WHERE OWNER= '" + A_1[0] + "' AND VIEW_NAME LIKE '%'";
		return a(A_0, "SSISTables", a_, A_1, new string[2] { "OWNER", "VIEW_NAME" });
	}

	private DataTable m(DbConnection A_0, string[] A_1)
	{
		string text = ((string.Compare(this.m_a, "09") >= 0) ? ", SUPERVIEW_NAME AS \"SuperViewName\"" : "");
		string text2 = ((string.Compare(this.m_a, "08") >= 0) ? ", OID_TEXT AS \"OidText\", TYPE_TEXT AS \"TypeText\", VIEW_TYPE AS \"ViewType\", VIEW_TYPE_OWNER AS \"ViewTypeSchema\"" : "");
		string a_ = "SELECT OWNER AS \"Schema\",VIEW_NAME AS \"Name\",TEXT AS \"Text\"" + text2 + text + " FROM SYS.ALL_VIEWS WHERE OWNER={0} AND VIEW_NAME LIKE {1}";
		return a(A_0, "Views", a_, A_1, new string[2] { "OWNER", "VIEW_NAME" });
	}

	private DataTable d(DbConnectionBase A_0, string[] A_1)
	{
		string text;
		string text2;
		if (string.Compare(this.m_a, "08") < 0)
		{
			text = "";
			text2 = "";
		}
		else
		{
			text = ", CHARACTER_SET_NAME AS \"Charset\"";
			text2 = ", DATA_TYPE_OWNER AS \"DataTypeSchema\"";
		}
		string a_;
		if (A_1 == null || A_1.Length <= 3 || (A_1[3] != null && A_1[3].ToLower() != "comments"))
		{
			a_ = "SELECT t1.OWNER AS \"Schema\", t1.TABLE_NAME AS \"Table\", t1.COLUMN_NAME AS \"Name\", COLUMN_ID AS \"Position\", DATA_TYPE AS \"DataType\"" + text2 + ", DATA_LENGTH AS \"Length\", DATA_PRECISION AS \"Precision\", DATA_SCALE AS \"Scale\", DATA_DEFAULT AS \"DefaultValue\", NULLABLE AS \"Nullable\"" + text + " FROM SYS.ALL_TAB_COLUMNS t1 WHERE t1.OWNER={0} AND t1.TABLE_NAME={1} AND t1.COLUMN_NAME LIKE {2} ORDER BY t1.OWNER, t1.TABLE_NAME, \"Position\" ";
		}
		else
		{
			a_ = "SELECT t1.OWNER AS \"Schema\", t1.TABLE_NAME AS \"Table\", t1.COLUMN_NAME AS \"Name\", COLUMN_ID AS \"Position\", DATA_TYPE AS \"DataType\"" + text2 + ", DATA_LENGTH AS \"Length\", DATA_PRECISION AS \"Precision\", DATA_SCALE AS \"Scale\", DATA_DEFAULT AS \"DefaultValue\", NULLABLE AS \"Nullable\"" + text + ", t2.COMMENTS AS \"Comments\" FROM SYS.ALL_TAB_COLUMNS t1, SYS.All_Col_Comments t2 WHERE t1.owner=t2.owner AND t1.COLUMN_NAME=t2.COLUMN_NAME AND t1.Table_Name=t2.Table_Name AND t1.OWNER={0} AND t1.TABLE_NAME={1} AND t1.COLUMN_NAME LIKE {2} ORDER BY t1.OWNER, t1.TABLE_NAME, \"Position\" ";
			string[] array = new string[A_1.Length - 1];
			for (int num = 0; num < array.Length; num++)
			{
				array[num] = A_1[num];
			}
			A_1 = array;
		}
		DataTable dataTable = a(A_0, "Columns", a_, A_1, new string[3] { "t1.OWNER", "t1.TABLE_NAME", "t1.COLUMN_NAME" });
		if (A_0.IsNHibernate)
		{
			dataTable.Columns[0].ColumnName = "OWNER";
			dataTable.Columns[1].ColumnName = "TABLE_NAME";
			dataTable.Columns[2].ColumnName = "COLUMN_NAME";
		}
		return dataTable;
	}

	private DataTable c(DbConnectionBase A_0, string[] A_1)
	{
		string userId = ((OracleConnection)A_0).UserId;
		if (A_0 is OracleConnection && A_1.Length == 3 && A_1[0] == null)
		{
			A_1[0] = ((userId[0] == '"') ? userId : userId.ToUpper());
		}
		return d(A_0, A_1);
	}

	private DataTable b(DbConnectionBase A_0, string[] A_1)
	{
		string text = ((string.Compare(this.m_a, "10.01.00.02") < 0) ? "" : "DROPPED <> 'YES' AND ");
		string text2 = ((string.Compare(this.m_a, "09") >= 0) ? ", JOIN_INDEX AS \"JoinIndex\"" : "");
		string text3;
		string text4;
		if (string.Compare(this.m_a, "08") < 0)
		{
			text3 = "";
			text4 = "";
		}
		else if (string.Compare(this.m_a, "08.01") < 0)
		{
			text3 = ", TEMPORARY AS \"Temporary\", GENERATED AS \"Generated\", BUFFER_POOL AS \"BufferPool\"";
			text4 = ", INDEX_TYPE AS \"Type\"";
		}
		else
		{
			text3 = ", COMPRESSION AS \"Compression\", TEMPORARY AS \"Temporary\", GENERATED AS \"Generated\", BUFFER_POOL AS \"BufferPool\", DURATION AS \"Duration\"";
			text4 = ", INDEX_TYPE AS \"Type\"";
		}
		string a_ = "SELECT OWNER AS \"Schema\", INDEX_NAME AS \"Name\", TABLE_OWNER AS \"TableSchema\", TABLE_NAME AS \"Table\", TABLE_TYPE AS \"TableType\"" + text4 + ", STATUS AS \"Status\", Decode(UNIQUENESS, 'UNIQUE', 'Yes', 'No') AS \"IsUnique\"" + text3 + text2 + " FROM SYS.ALL_INDEXES WHERE " + text + "OWNER = {0} AND INDEX_NAME LIKE {1} AND TABLE_OWNER = {2} AND TABLE_NAME = {3}";
		DataTable dataTable = a(A_0, "Indexes", a_, A_1, new string[4] { "OWNER", "INDEX_NAME", "TABLE_OWNER", "TABLE_NAME" });
		if (A_0.IsNHibernate)
		{
			dataTable.Columns[0].ColumnName = "OWNER";
			dataTable.Columns[1].ColumnName = "INDEX_NAME";
			dataTable.Columns[2].ColumnName = "TABLE_OWNER";
			dataTable.Columns[3].ColumnName = "TABLE_NAME";
		}
		return dataTable;
	}

	private DataTable a(DbConnectionBase A_0, string[] A_1)
	{
		string text = ((string.Compare(this.m_a, "09") >= 0) ? ", CHAR_LENGTH AS \"CharLength\"" : "");
		string text2 = ((string.Compare(this.m_a, "08.01") >= 0) ? ", DESCEND AS \"Descend\"" : "");
		string a_ = "SELECT INDEX_OWNER AS \"Schema\", INDEX_NAME AS \"Index\", TABLE_OWNER AS \"TableSchema\", TABLE_NAME AS \"Table\", COLUMN_NAME AS \"Name\", COLUMN_POSITION AS \"Position\", COLUMN_LENGTH AS \"Length\"" + text + text2 + " FROM SYS.ALL_IND_COLUMNS WHERE INDEX_OWNER = {0} AND INDEX_NAME = {1} AND TABLE_OWNER = {2} AND TABLE_NAME = {3} AND COLUMN_NAME LIKE {4}";
		DataTable dataTable = a(A_0, "IndexColumns", a_, A_1, new string[5] { "INDEX_OWNER", "INDEX_NAME", "TABLE_OWNER", "TABLE_NAME", "COLUMN_NAME" });
		if (A_0.IsNHibernate)
		{
			dataTable.Columns[0].ColumnName = "OWNER";
			dataTable.Columns[1].ColumnName = "INDEX_NAME";
			dataTable.Columns[2].ColumnName = "TABLE_OWNER";
			dataTable.Columns[3].ColumnName = "TABLE_NAME";
			dataTable.Columns[4].ColumnName = "COLUMN_NAME";
		}
		return dataTable;
	}

	private DataTable l(DbConnection A_0, string[] A_1)
	{
		string text = ((string.Compare(this.m_a, "08") < 0) ? "" : ((string.Compare(this.m_a, "08.01") >= 0) ? ", TEMPORARY AS \"Temporary\", GENERATED AS \"Generated\", SECONDARY AS \"Secondary\"" : ", TEMPORARY AS \"Temporary\", GENERATED AS \"Generated\""));
		string a_ = "SELECT OWNER AS \"Schema\", OBJECT_NAME AS \"Name\", CREATED AS \"Created\", LAST_DDL_TIME AS \"Modified\", STATUS AS \"Status\"" + text + " FROM SYS.ALL_OBJECTS WHERE OWNER = {0} AND OBJECT_NAME LIKE {1} AND OBJECT_TYPE = 'FUNCTION'";
		return a(A_0, "Functions", a_, A_1, new string[2] { "OWNER", "OBJECT_NAME" });
	}

	private DataTable k(DbConnection A_0, string[] A_1)
	{
		if (string.Compare(this.m_a, "09") < 0)
		{
			return new DataTable();
		}
		string text = "SELECT OWNER AS \"Schema\", OBJECT_NAME AS \"Package\", PROCEDURE_NAME AS \"Name\", AGGREGATE AS \"Aggregate\",PIPELINED AS \"PipeLined\", IMPLTYPEOWNER AS \"ImplTypeSchema\", IMPLTYPENAME AS \"ImplType\", PARALLEL AS \"Parallel\", INTERFACE AS \"Interface\", DETERMINISTIC AS \"Deterministic\", AUTHID AS \"Authid\" FROM SYS.ALL_PROCEDURES WHERE OWNER = {0} AND OBJECT_NAME LIKE {1}";
		if (A_1 != null && A_1.Length == 3 && string.IsNullOrEmpty(A_1[1]) && !string.IsNullOrEmpty(A_1[2]))
		{
			A_1[1] = A_1[2];
			return a(A_0, "Procedures", text, A_1, new string[2] { "OWNER", "OBJECT_NAME" });
		}
		return a(A_0, "Procedures", text + " AND PROCEDURE_NAME LIKE {2}", A_1, new string[3] { "OWNER", "OBJECT_NAME", "PROCEDURE_NAME" });
	}

	private DataTable j(DbConnection A_0, string[] A_1)
	{
		string text;
		string text2;
		if (string.Compare(this.m_a, "08") < 0)
		{
			text = "";
			text2 = "";
		}
		else
		{
			text = ", TYPE_OWNER AS \"TypeSchema\", TYPE_NAME AS \"TypeName\", TYPE_SUBNAME AS \"SubType\"";
			text2 = ", CHARACTER_SET_NAME AS \"Charset\"";
		}
		string text3 = "SELECT OWNER AS \"Schema\", PACKAGE_NAME AS \"Package\", OBJECT_NAME AS \"Procedure\", OVERLOAD AS \"Overload\", ARGUMENT_NAME AS \"Name\", POSITION AS \"Position\", SEQUENCE AS \"Sequence\", DATA_TYPE AS \"DataType\"" + text + ", IN_OUT AS \"Direction\", DATA_LENGTH AS \"Length\", DATA_PRECISION AS \"Precision\", DATA_SCALE AS \"Scale\"" + text2 + ", DEFAULT_VALUE AS \"DefaultValue\", DEFAULT_LENGTH AS \"DefaultLength\" FROM SYS.ALL_ARGUMENTS WHERE OWNER = {0} AND OBJECT_NAME = {2} AND DATA_LEVEL = 0 AND DATA_TYPE IS NOT NULL";
		text3 = ((A_1 != null && A_1.Length >= 2 && A_1[1] != null) ? (text3 + " AND PACKAGE_NAME = {1}") : (text3 + " AND PACKAGE_NAME IS NULL"));
		if (A_1 != null && A_1.Length >= 4 && A_1[3] != null)
		{
			text3 += " AND ARGUMENT_NAME LIKE {3}";
		}
		return a(A_0, "Arguments", text3, A_1, new string[4] { "OWNER", "PACKAGE_NAME", "OBJECT_NAME", "ARGUMENT_NAME" });
	}

	private DataTable i(DbConnection A_0, string[] A_1)
	{
		return a(A_0, "Synonyms", "SELECT OWNER AS \"Schema\", SYNONYM_NAME AS \"Name\", TABLE_OWNER AS \"ObjectSchema\", TABLE_NAME AS \"ObjectName\", DB_LINK AS \"DbLink\" FROM SYS.ALL_SYNONYMS WHERE OWNER={0} AND SYNONYM_NAME LIKE {1}", A_1, new string[2] { "OWNER", "SYNONYM_NAME" });
	}

	private DataTable h(DbConnection A_0, string[] A_1)
	{
		return a(A_0, "Sequences", "SELECT SEQUENCE_OWNER AS \"Schema\", SEQUENCE_NAME AS \"Name\", MIN_VALUE AS \"MinValue\", MAX_VALUE AS \"MaxValue\", INCREMENT_BY AS \"IncrementBy\", CYCLE_FLAG AS \"Cycle\", ORDER_FLAG AS \"Order\", CACHE_SIZE AS \"CacheSize\",LAST_NUMBER AS \"LastNumber\" FROM SYS.ALL_SEQUENCES WHERE SEQUENCE_OWNER={0} AND SEQUENCE_NAME LIKE {1}", A_1, new string[2] { "SEQUENCE_OWNER", "SEQUENCE_NAME" });
	}

	private DataTable g(DbConnection A_0, string[] A_1)
	{
		string text = ((string.Compare(this.m_a, "08") >= 0) ? ", SUBOBJECT_NAME AS \"SubName\"" : "");
		string a_ = "SELECT OWNER AS \"Schema\", OBJECT_NAME AS \"Name\"" + text + ", CREATED AS \"Created\", LAST_DDL_TIME AS \"Modified\", STATUS AS \"Status\" FROM SYS.ALL_OBJECTS WHERE OWNER= {0} and OBJECT_NAME LIKE {1} AND OBJECT_TYPE = 'PACKAGE'";
		return a(A_0, "Packages", a_, A_1, new string[2] { "OWNER", "OBJECT_NAME" });
	}

	private DataTable f(DbConnection A_0, string[] A_1)
	{
		string text = ((string.Compare(this.m_a, "08") >= 0) ? ", SUBOBJECT_NAME AS \"SubName\"" : "");
		string a_ = "SELECT OWNER AS \"Schema\", OBJECT_NAME AS \"Name\"" + text + ", CREATED AS \"Created\", LAST_DDL_TIME AS \"Modified\", STATUS AS \"Status\" FROM SYS.ALL_OBJECTS WHERE OWNER= {0} and OBJECT_NAME LIKE {1} AND OBJECT_TYPE = 'PACKAGE BODY'";
		return a(A_0, "PackageBodies", a_, A_1, new string[2] { "OWNER", "OBJECT_NAME" });
	}

	private DataTable e(DbConnection A_0, string[] A_1)
	{
		string text;
		string text2;
		if (string.Compare(this.m_a, "09") < 0)
		{
			text = "";
			text2 = "";
		}
		else
		{
			text = ", FKCON.VIEW_RELATED AS \"ViewRelated\"";
			text2 = ", FKCON.INVALID AS \"Invalid\"";
		}
		string text3 = ((string.Compare(this.m_a, "08") < 0) ? "" : ((string.Compare(this.m_a, "08.01") >= 0) ? (", FKCON.BAD AS \"Bad\", FKCON.DEFERRABLE AS \"Deferrable\", FKCON.DEFERRED AS \"Deferred\"" + text2 + ", FKCON.GENERATED AS \"Generated\", FKCON.VALIDATED AS \"Validated\", FKCON.RELY AS \"Rely\", FKCON.LAST_CHANGE AS \"LastChange\"") : (", FKCON.BAD AS \"Bad\", FKCON.DEFERRABLE AS \"Deferrable\", FKCON.DEFERRED AS \"Deferred\"" + text2 + ", FKCON.GENERATED AS \"Generated\", FKCON.VALIDATED AS \"Validated\", FKCON.LAST_CHANGE AS \"LastChange\"")));
		string a_ = "SELECT FKCON.OWNER AS \"Schema\", FKCON.CONSTRAINT_NAME AS \"Name\", FKCON.TABLE_NAME AS \"Table\", PKCON.OWNER AS \"ReferencedSchema\", PKCON.CONSTRAINT_NAME AS \"ReferencedKey\", PKCON.TABLE_NAME AS \"ReferencedTable\", FKCON.SEARCH_CONDITION AS \"SearchCondition\", FKCON.DELETE_RULE AS \"DeleteRule\", FKCON.STATUS AS \"Status\"" + text3 + text + " FROM SYS.ALL_CONSTRAINTS FKCON, SYS.ALL_CONSTRAINTS PKCON WHERE PKCON.CONSTRAINT_NAME = FKCON.R_CONSTRAINT_NAME AND PKCON.OWNER = FKCON.R_OWNER AND FKCON.CONSTRAINT_TYPE = 'R' and FKCON.OWNER = {0} AND FKCON.TABLE_NAME = {1} AND FKCON.CONSTRAINT_NAME LIKE {2}";
		return a(A_0, "ForeignKeys", a_, A_1, new string[3] { "FKCON.OWNER", "FKCON.TABLE_NAME", "FKCON.CONSTRAINT_NAME" });
	}

	private DataTable d(DbConnection A_0, string[] A_1)
	{
		return a(A_0, "ForeignKeyColumns", "SELECT FKCOLS.OWNER AS \"Schema\", FKCOLS.CONSTRAINT_NAME AS \"Constraint\", FKCOLS.TABLE_NAME AS \"Table\", FKCOLS.COLUMN_NAME AS \"Name\", FKCOLS.POSITION AS \"Position\" FROM SYS.ALL_CONS_COLUMNS FKCOLS, SYS.ALL_CONSTRAINTS FKCON where FKCOLS.CONSTRAINT_NAME = FKCON.CONSTRAINT_NAME and FKCON.CONSTRAINT_TYPE = 'R' AND FKCON.OWNER = {0} and  FKCOLS.OWNER = {0} and FKCOLS.TABLE_NAME = {1} and  FKCOLS.CONSTRAINT_NAME LIKE {2} ORDER BY FKCOLS.OWNER, FKCOLS.CONSTRAINT_NAME, FKCOLS.TABLE_NAME, FKCOLS.POSITION", A_1, new string[3] { "FKCOLS.OWNER", "FKCOLS.TABLE_NAME", "FKCOLS.CONSTRAINT_NAME" });
	}

	private DataTable c(DbConnection A_0, string[] A_1)
	{
		string a_ = ((string.Compare(this.m_a, "09") >= 0) ? "\r\nSELECT kcu.OWNER AS \"Schema\",\r\n       kcu.CONSTRAINT_NAME AS \"Constraint\",\r\n       kcu.TABLE_NAME AS \"Table\",\r\n       kcu.COLUMN_NAME AS \"Name\",\r\n       kcu.POSITION AS \"Position\",\r\n       relation_kcu.column_name as \"ReferencedColumn\" \r\nFROM\r\n  ALL_CONSTRAINTS a\r\nINNER JOIN \r\n  ALL_CONSTRAINTS b ON (a.R_OWNER = b.OWNER AND a.R_CONSTRAINT_NAME = b.CONSTRAINT_NAME)\r\nLEFT JOIN\r\n  ALL_CONS_COLUMNS kcu\r\n  ON \r\n  kcu.OWNER = a.OWNER\r\n  AND\r\n  kcu.CONSTRAINT_NAME = a.CONSTRAINT_NAME\r\n  AND\r\n  kcu.TABLE_NAME = a.TABLE_NAME\r\nLEFT JOIN\r\n\tALL_CONS_COLUMNS relation_kcu\r\n\tON \r\n\trelation_kcu.OWNER = b.OWNER\r\n\tAND\r\n\trelation_kcu.CONSTRAINT_NAME = b.CONSTRAINT_NAME\r\n\tAND\r\n\trelation_kcu.TABLE_NAME = b.TABLE_NAME\r\n  AND \r\n  kcu.POSITION = relation_kcu.POSITION\r\nWHERE a.CONSTRAINT_TYPE = 'R'\r\n  AND (b.CONSTRAINT_TYPE = 'P' OR b.CONSTRAINT_TYPE = 'U')\r\n  AND a.OWNER = {0} AND b.OWNER = {0} AND a.TABLE_NAME LIKE {1} AND a.CONSTRAINT_NAME LIKE {2}\r\nORDER BY a.OWNER, a.CONSTRAINT_NAME, a.TABLE_NAME, kcu.POSITION" : "\r\nSELECT kcu.OWNER AS \"Schema\",\r\n       kcu.CONSTRAINT_NAME AS \"Constraint\",\r\n       kcu.TABLE_NAME AS \"Table\",\r\n       kcu.COLUMN_NAME AS \"Name\",\r\n       kcu.POSITION AS \"Position\",\r\n       relation_kcu.column_name as \"ReferencedColumn\" \r\nFROM\r\n  ALL_CONSTRAINTS a, ALL_CONSTRAINTS b, ALL_CONS_COLUMNS kcu, ALL_CONS_COLUMNS relation_kcu\r\nWHERE \r\n  (a.R_OWNER = b.OWNER AND a.R_CONSTRAINT_NAME = b.CONSTRAINT_NAME) AND\r\n  ( kcu.OWNER(+) = a.OWNER AND kcu.CONSTRAINT_NAME(+) = a.CONSTRAINT_NAME AND\r\n  kcu.TABLE_NAME(+) = a.TABLE_NAME ) AND\r\n\t( relation_kcu.OWNER(+) = b.OWNER AND relation_kcu.CONSTRAINT_NAME(+) = b.CONSTRAINT_NAME \r\n  AND relation_kcu.TABLE_NAME(+) = b.TABLE_NAME AND kcu.POSITION = relation_kcu.POSITION ) \r\n  AND a.CONSTRAINT_TYPE = 'R' AND (b.CONSTRAINT_TYPE = 'P' OR b.CONSTRAINT_TYPE = 'U')\r\n  AND a.OWNER = {0} AND b.OWNER = {0} AND a.TABLE_NAME LIKE {1} AND a.CONSTRAINT_NAME LIKE {2}\r\nORDER BY a.OWNER, a.CONSTRAINT_NAME, a.TABLE_NAME, kcu.POSITION");
		return a(A_0, "ForeignKeyColumns", a_, A_1, new string[3] { "FKCOLS.OWNER", "FKCOLS.TABLE_NAME", "FKCOLS.CONSTRAINT_NAME" });
	}

	private DataTable b(DbConnection A_0, string[] A_1)
	{
		string text;
		string text2;
		string text3;
		if (string.Compare(this.m_a, "08.01") < 0)
		{
			text = "";
			text2 = "";
			text3 = "";
		}
		else
		{
			text = ", BASE_OBJECT_TYPE AS \"BaseObjectType\"";
			text2 = ", COLUMN_NAME AS \"Column\"";
			text3 = ", ACTION_TYPE AS \"ActionType\"";
		}
		string a_ = "SELECT OWNER AS \"Schema\", TRIGGER_NAME AS \"Name\", TRIGGER_TYPE AS \"Type\", TRIGGERING_EVENT AS \"Event\", TABLE_OWNER AS \"TableSchema\"" + text + ", TABLE_NAME AS \"Table\"" + text2 + ", REFERENCING_NAMES AS \"ReferencingNames\", WHEN_CLAUSE AS \"When\", STATUS AS \"Status\", DESCRIPTION AS \"Description\"" + text3 + ", TRIGGER_BODY AS \"Body\" FROM SYS.ALL_TRIGGERS WHERE OWNER = {0} AND TRIGGER_NAME LIKE {1} AND TABLE_OWNER = {2} AND TABLE_NAME = {3}";
		return a(A_0, "Triggers", a_, A_1, new string[4] { "OWNER", "TRIGGER_NAME", "TABLE_OWNER", "TABLE_NAME" });
	}

	private DataTable a(DbConnection A_0, string[] A_1)
	{
		string text = ((string.Compare(this.m_a, "08") < 0) ? "" : ((string.Compare(this.m_a, "08.01") >= 0) ? ", BUFFER_POOL AS \"BufferPool\", SINGLE_TABLE AS \"SingleTable\"" : ", BUFFER_POOL AS \"BufferPool\""));
		string text2 = ((string.Compare(this.m_a, "09") >= 0) ? ", DEPENDENCIES AS \"Dependencies\"" : "");
		string a_ = "SELECT OWNER AS \"Schema\", CLUSTER_NAME AS \"Name\", CLUSTER_TYPE AS \"Type\", FUNCTION AS \"Function\", HASHKEYS AS \"Hashkeys\", DEGREE AS \"Degree\", INSTANCES AS \"Instances\", CACHE AS \"Cache\"" + text + text2 + " FROM SYS.ALL_CLUSTERS WHERE OWNER = {0} AND CLUSTER_NAME LIKE {1}";
		return a(A_0, "Clusters", a_, A_1, new string[2] { "OWNER", "CLUSTER_NAME" });
	}

	private DataTable a(DbConnection A_0, string A_1, string A_2, string[] A_3, string[] A_4)
	{
		if (A_3 != null)
		{
			int num = 0;
			foreach (string text in A_3)
			{
				if (text != null && num < A_4.Length)
				{
					A_4[num] = "'" + OracleUtils.UnQuote(text) + "'";
				}
				num++;
			}
		}
		OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(string.Format(A_2, (object?[])A_4), (OracleConnection)A_0);
		DataTable dataTable = new DataTable(A_1);
		oracleDataAdapter.Fill(dataTable);
		return dataTable;
	}
}
