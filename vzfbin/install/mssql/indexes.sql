/*
** Indexes
*/
-- {objectQualifier}Buddy
if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Buddy_UserID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Buddy]'))
	CREATE  INDEX [IX_{objectQualifier}Buddy_UserID] ON [{databaseSchema}].[{objectQualifier}Buddy]([FromUserID],[ToUserID])
go

-- {objectQualifier}Registry

if exists(select top 1 1 from sys.indexes  where name=N'IX_Name' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Registry]'))
	drop index [{databaseSchema}].[{objectQualifier}Registry].[IX_Name]
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Registry_Name' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Registry]'))
	CREATE  INDEX [IX_{objectQualifier}Registry_Name] ON [{databaseSchema}].[{objectQualifier}Registry]([BoardID],[Name])
go

-- {objectQualifier}PollVote

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}PollVote_RemoteIP' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVote]'))
 CREATE  INDEX [IX_{objectQualifier}PollVote_RemoteIP] ON [{databaseSchema}].[{objectQualifier}PollVote]([RemoteIP])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}PollVote_UserID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVote]'))
 CREATE  INDEX [IX_{objectQualifier}PollVote_UserID] ON [{databaseSchema}].[{objectQualifier}PollVote]([UserID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}PollVote_PollID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVote]'))
 CREATE  INDEX [IX_{objectQualifier}PollVote_PollID] ON [{databaseSchema}].[{objectQualifier}PollVote]([PollID])
go

-- {objectQualifier}UserGroup

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}UserGroup_UserID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserGroup]'))
 CREATE  INDEX [IX_{objectQualifier}UserGroup_UserID] ON [{databaseSchema}].[{objectQualifier}UserGroup]([UserID])
go

-- {objectQualifier}Message

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Message_TopicID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]'))
 CREATE  INDEX [IX_{objectQualifier}Message_TopicID] ON [{databaseSchema}].[{objectQualifier}Message]([TopicID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Message_UserID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]'))
 CREATE  INDEX [IX_{objectQualifier}Message_UserID] ON [{databaseSchema}].[{objectQualifier}Message]([UserID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Message_Flags' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]'))
 CREATE  INDEX [IX_{objectQualifier}Message_Flags] ON [{databaseSchema}].[{objectQualifier}Message]([Flags])
go

IF  NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'[{databaseSchema}].[{objectQualifier}Message]') AND name = N'IX_{objectQualifier}Message_Posted_Desc')
CREATE NONCLUSTERED INDEX [IX_{objectQualifier}Message_Posted_Desc] ON [{databaseSchema}].[{objectQualifier}Message] 
(
	[Posted] DESC
) ON [PRIMARY]
GO

-- {objectQualifier}Topic

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Topic_ForumID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]'))
 CREATE  INDEX [IX_{objectQualifier}Topic_ForumID] ON [{databaseSchema}].[{objectQualifier}Topic]([ForumID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Topic_UserID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]'))
 CREATE  INDEX [IX_{objectQualifier}Topic_UserID] ON [{databaseSchema}].[{objectQualifier}Topic]([UserID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Topic_Flags' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]'))
 CREATE  INDEX [IX_{objectQualifier}Topic_Flags] ON [{databaseSchema}].[{objectQualifier}Topic]([Flags])
go

-- {objectQualifier}Forum

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Forum_CategoryID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]'))
 CREATE  INDEX [IX_{objectQualifier}Forum_CategoryID] ON [{databaseSchema}].[{objectQualifier}Forum]([CategoryID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Forum_ParentID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]'))
 CREATE  INDEX [IX_{objectQualifier}Forum_ParentID] ON [{databaseSchema}].[{objectQualifier}Forum]([ParentID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Forum_Flags' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]'))
 CREATE  INDEX [IX_{objectQualifier}Forum_Flags] ON [{databaseSchema}].[{objectQualifier}Forum]([Flags])
go

-- {objectQualifier}User

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}User_Flags' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]'))
 CREATE  INDEX [IX_{objectQualifier}User_Flags] ON [{databaseSchema}].[{objectQualifier}User]([Flags])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}User_Joined' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]'))
 CREATE  INDEX [IX_{objectQualifier}User_Joined] ON [{databaseSchema}].[{objectQualifier}User]([Joined])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}User_ProviderUserKey' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]'))
 CREATE  INDEX [IX_{objectQualifier}User_ProviderUserKey] ON [{databaseSchema}].[{objectQualifier}User]([ProviderUserKey])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}User_Name' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]'))
 CREATE  INDEX [IX_{objectQualifier}User_Name] ON [{databaseSchema}].[{objectQualifier}User]([Name])
go

-- {objectQualifier}ForumAccess

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}ForumAccess_ForumID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ForumAccess]'))
 CREATE  INDEX [IX_{objectQualifier}ForumAccess_ForumID] ON [{databaseSchema}].[{objectQualifier}ForumAccess]([ForumID])
go

-- {objectQualifier}UserPMessage

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}UserPMessage_UserID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserPMessage]'))
 CREATE  INDEX [IX_{objectQualifier}UserPMessage_UserID] ON [{databaseSchema}].[{objectQualifier}UserPMessage]([UserID])
go

-- {objectQualifier}Category

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Category_BoardID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Category]'))
 CREATE  INDEX [IX_{objectQualifier}Category_BoardID] ON [{databaseSchema}].[{objectQualifier}Category]([BoardID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}Category_Name' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Category]'))
 CREATE  INDEX [IX_{objectQualifier}Category_Name] ON [{databaseSchema}].[{objectQualifier}Category]([Name])
go

-- {objectQualifier}FavoriteTopic

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}FavoriteTopic_TopicID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}FavoriteTopic]'))
 CREATE  INDEX [IX_{objectQualifier}FavoriteTopic_TopicID] ON [{databaseSchema}].[{objectQualifier}FavoriteTopic]([TopicID])
go

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}FavoriteTopic_UserID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}FavoriteTopic]'))
 CREATE  INDEX [IX_{objectQualifier}FavoriteTopic_UserID] ON [{databaseSchema}].[{objectQualifier}FavoriteTopic]([UserID])
go

-- {objectQualifier}UserAlbum

if not exists(select top 1 1 from sys.indexes  where name=N'IX_{objectQualifier}UserAlbumImage_AlbumID' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserAlbumImage]'))
 CREATE  INDEX [IX_{objectQualifier}UserAlbumImage_AlbumID] ON [{databaseSchema}].[{objectQualifier}UserAlbumImage]([AlbumID])
go

-- {objectQualifier}Thanks

IF NOT EXISTS (SELECT 1 FROM sys.indexes  where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Thanks]') AND name = N'IX_{objectQualifier}Thanks_MessageID')
CREATE  INDEX [IX_{objectQualifier}Thanks_MessageID] ON [{databaseSchema}].[{objectQualifier}Thanks] 
(
	[MessageID] ASC
)
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes  where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Thanks]') AND name = N'IX_{objectQualifier}Thanks_ThanksFromUserID')
CREATE  INDEX [IX_{objectQualifier}Thanks_ThanksFromUserID] ON [{databaseSchema}].[{objectQualifier}Thanks] 
(
	[ThanksFromUserID] ASC
)
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes  where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Thanks]') AND name = N'IX_{objectQualifier}Thanks_ThanksToUserID')
CREATE  INDEX [IX_{objectQualifier}Thanks_ThanksToUserID] ON [{databaseSchema}].[{objectQualifier}Thanks] 
(
	[ThanksToUserID] ASC
)
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes  where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}FavoriteTopic]') AND name = N'IX_{objectQualifier}FavoriteTopic_TopicID')
CREATE  INDEX [IX_{objectQualifier}FavoriteTopic_TopicID] ON [{databaseSchema}].[{objectQualifier}FavoriteTopic] 
(
	[TopicID] ASC
)
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes  where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}FavoriteTopic]') AND name = N'IX_{objectQualifier}FavoriteTopic_UserID')
CREATE  INDEX [IX_{objectQualifier}FavoriteTopic_UserID] ON [{databaseSchema}].[{objectQualifier}FavoriteTopic] 
(
	[UserID] ASC
)
GO

IF  NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'[{databaseSchema}].[{objectQualifier}Topic]') AND name = N'IX_{objectQualifier}Topic_LastPosted_Desc')
CREATE NONCLUSTERED INDEX [IX_{objectQualifier}Topic_LastPosted_Desc] ON [{databaseSchema}].[{objectQualifier}Topic] 
(
	[LastPosted] DESC
) ON [PRIMARY]
GO

IF  NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'[{databaseSchema}].[{objectQualifier}Group]') AND name = N'IX_{objectQualifier}Group_SortOrder')
CREATE NONCLUSTERED INDEX [IX_{objectQualifier}Group_SortOrder] ON [{databaseSchema}].[{objectQualifier}Group] 
(
	[SortOrder] ASC
) ON [PRIMARY]
GO

IF  NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID(N'[{databaseSchema}].[{objectQualifier}User]') AND name = N'IX_{objectQualifier}User_DisplayName')
CREATE NONCLUSTERED INDEX [IX_{objectQualifier}User_DisplayName] ON [{databaseSchema}].[{objectQualifier}User] 
(
	[DisplayName] ASC
) ON [PRIMARY]
GO
