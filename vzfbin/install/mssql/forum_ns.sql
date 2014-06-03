
if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}forum_ns](
  [nid] int identity (1,1) NOT NULL,
  [boardid] int NOT NULL,
  [categoryid] int NOT NULL,
  [forumid] int NOT NULL,
  [left_key] int NOT NULL,
  [right_key] int NOT NULL,
  [level] int NOT NULL DEFAULT (0),
  [tree] int NOT NULL DEFAULT (0),
  [parentid] int NOT NULL DEFAULT (0),
  [trigger_for_delete] bit NOT NULL,  
  [trigger_lock_update] bit NOT NULL, 
  [sortorder] int NOT NULL DEFAULT (0),
  [path_cache] varchar(1024)
  )  
GO

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}forum_ns]') and name=N'PK_{objectQualifier}forum_ns')
	alter table [{databaseSchema}].[{objectQualifier}forum_ns] with nocheck add constraint  [PK_{objectQualifier}forum_ns] primary key clustered (nid)   
go


-- forum_ns
if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}_forum_ns_left_key_right_key_level_tree_idx' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns]'))
	CREATE  INDEX [IX_{objectQualifier}_forum_ns_left_key_right_key_level_tree_idx] ON [{databaseSchema}].[{objectQualifier}forum_ns]([left_key],[right_key],[level],[tree])
go
if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}_forum_ns_parent_id_idx' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns]'))
	CREATE  INDEX [IX_{objectQualifier}_forum_ns_parent_id_idx] ON [{databaseSchema}].[{objectQualifier}forum_ns]([parentid])
go 