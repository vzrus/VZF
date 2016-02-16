IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}defaultconstraint_name]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}defaultconstraint_name]
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn](@tablename varchar(255), @columnname varchar(255)) as
BEGIN
DECLARE @DefName sysname

SELECT
  @DefName = o1.name
FROM
  sys.objects o1
  INNER JOIN sys.columns c ON
  o1.object_id = c.default_object_id
  INNER JOIN sys.objects o2 ON
  c.object_id = o2.object_id
WHERE
  o2.name = @tablename AND
  c.name = @columnname
  
IF @DefName IS NOT NULL
  EXECUTE ('ALTER TABLE ' + @tablename + ' DROP CONSTRAINT [' + @DefName + ']')
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}defaultconstraint_name](@tablename varchar(255), @columnname varchar(255)) as
BEGIN
DECLARE @DefName sysname

SELECT
  @DefName = o1.name
FROM
  sys.objects o1
  INNER JOIN sys.columns c ON
  o1.object_id = c.default_object_id
  INNER JOIN sys.objects o2 ON
  c.object_id = o2.object_id
WHERE
  o2.name = @tablename AND
  c.name = @columnname
END
GO
