/*
** Triggers
*/

if exists(select 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Active_insert]') and type in (N'TR'))
	drop trigger [{databaseSchema}].[{objectQualifier}Active_insert]
go

if exists(select 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum_update]') and type in (N'TR'))
	drop trigger [{databaseSchema}].[{objectQualifier}Forum_update]
go

/*
CREATE TRIGGER [{databaseSchema}].[{objectQualifier}Forum_update] ON [{databaseSchema}].[{objectQualifier}Forum] FOR UPDATE AS
BEGIN
	IF UPDATE(LastTopicID) OR UPDATE(LastMessageID)
	BEGIN	
		-- recursively update the forum
		DECLARE @ParentID int		

		SET @ParentID = (SELECT TOP 1 ParentID FROM inserted)
		
		WHILE (@ParentID IS NOT NULL)
		BEGIN
			UPDATE a SET
				a.LastPosted = b.LastPosted,
				a.LastTopicID = b.LastTopicID,
				a.LastMessageID = b.LastMessageID,
				a.LastUserID = b.LastUserID,
				a.LastUserName = b.LastUserName
			FROM
				[{databaseSchema}].[{objectQualifier}Forum]] a, inserted b
			WHERE
				a.ForumID = @ParentID AND ((a.LastPosted < b.LastPosted) OR a.LastPosted IS NULL);
			
			SET @ParentID = (SELECT ParentID FROM [{databaseSchema}].[{objectQualifier}Forum] WHERE ForumID = @ParentID)
		END
	END
END
*/
GO

if exists(select 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group_update]') and type in (N'TR'))
	drop trigger [{databaseSchema}].[{objectQualifier}Group_update]
GO

if exists(select 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group_insert]') and type in (N'TR'))
	drop trigger [{databaseSchema}].[{objectQualifier}Group_insert]
GO

if exists(select 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserGroup_insert]') and type in (N'TR'))
	drop trigger [{databaseSchema}].[{objectQualifier}UserGroup_insert]
GO

if exists(select 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserGroup_delete]') and type in (N'TR'))
	drop trigger [{databaseSchema}].[{objectQualifier}UserGroup_delete]
GO

