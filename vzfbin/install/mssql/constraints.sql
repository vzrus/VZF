/*
** Drop old Foreign keys
*/


if exists (select top 1 1 from  sys.objects where name=N'FK_Active_Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Active] drop constraint [FK_Active_Forum]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Active_Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Active] drop constraint [FK_Active_Topic]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Active_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Active] drop constraint [FK_Active_User]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_CheckEmail_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}CheckEmail]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] drop constraint [FK_CheckEmail_User]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Choice_Poll' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Choice]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Choice] drop constraint [FK_Choice_Poll]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Forum_Category' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint [FK_Forum_Category]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Forum_Message' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint [FK_Forum_Message]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Forum_Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint [FK_Forum_Topic]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Forum_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint [FK_Forum_User]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_ForumAccess_Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumAccess]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumAccess] drop constraint [FK_ForumAccess_Forum]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_ForumAccess_Group' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumAccess]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumAccess] drop constraint [FK_ForumAccess_Group]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Message_Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Message]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Message] drop constraint [FK_Message_Topic]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Message_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Message]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Message] drop constraint [FK_Message_User]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_PMessage_User1' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}PMessage]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}PMessage] drop constraint [FK_PMessage_User1]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Topic_Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [FK_Topic_Forum]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Topic_Message' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [FK_Topic_Message]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Topic_Poll' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [FK_Topic_Poll]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Topic_Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [FK_Topic_Topic]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Topic_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [FK_Topic_User]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Topic_User2' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [FK_Topic_User2]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_WatchForum_Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}WatchForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}WatchForum] drop constraint [FK_WatchForum_Forum]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_WatchForum_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}WatchForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}WatchForum] drop constraint [FK_WatchForum_User]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_WatchTopic_Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}WatchTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}WatchTopic] drop constraint [FK_WatchTopic_Topic]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_WatchTopic_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}WatchTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}WatchTopic] drop constraint [FK_WatchTopic_User]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Active_Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Attachment] drop constraint [FK_Attachment_Message]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_UserGroup_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserGroup]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserGroup] drop constraint [FK_UserGroup_User]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_UserGroup_Group' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserGroup]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserGroup] drop constraint [FK_UserGroup_Group]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_Attachment_Message' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Attachment]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Attachment] drop constraint [FK_Attachment_Message]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_NntpForum_NntpServer' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] drop constraint [FK_NntpForum_NntpServer]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_NntpForum_Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] drop constraint [FK_NntpForum_Forum]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_NntpTopic_NntpForum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpTopic] drop constraint [FK_NntpTopic_NntpForum]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_NntpTopic_Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpTopic] drop constraint [FK_NntpTopic_Topic]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_ForumAccess_AccessMask' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumAccess]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumAccess] drop constraint [FK_ForumAccess_AccessMask]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_UserForum_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserForum] drop constraint [FK_UserForum_User]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_UserForum_Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserForum] drop constraint [FK_UserForum_Forum]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_UserForum_AccessMask' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserForum] drop constraint [FK_UserForum_AccessMask]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_Category_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Category]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Category] drop constraint [FK_Category_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_AccessMask_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}AccessMask]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop constraint [FK_AccessMask_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_Active_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Active] drop constraint [FK_Active_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_BannedIP_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}BannedIP]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}BannedIP] drop constraint [FK_BannedIP_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_Group_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Group]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Group] drop constraint [FK_Group_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_NntpServer_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpServer]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpServer] drop constraint [FK_NntpServer_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_Rank_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Rank]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Rank] drop constraint [FK_Rank_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_Smiley_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Smiley]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Smiley] drop constraint [FK_Smiley_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_User_Rank' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}User]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}User] drop constraint [FK_User_Rank]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_User_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}User]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}User] drop constraint [FK_User_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_Forum_Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint [FK_Forum_Forum]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_Message_Message' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Message]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Message] drop constraint [FK_Message_Message]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_UserPMessage_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserPMessage]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] drop constraint [FK_UserPMessage_User]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_UserPMessage_PMessage' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserPMessage]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] drop constraint [FK_UserPMessage_PMessage]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_Registry_Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Registry]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Registry] drop constraint [FK_Registry_Board]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_EventLog_User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}EventLog]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}EventLog] drop constraint [FK_EventLog_User]
go


if exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}PollVote_{objectQualifier}Poll' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}PollVote]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}PollVote] drop constraint [FK_{objectQualifier}PollVote_{objectQualifier}Poll]
go

if exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Topic_{objectQualifier}Poll' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [FK_{objectQualifier}Topic_{objectQualifier}Poll] 
go 

/* Drop old primary keys */

if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}BannedIP]') and name=N'PK_BannedIP')
	alter table [{databaseSchema}].[{objectQualifier}BannedIP] drop constraint [PK_BannedIP]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Category]') and name=N'PK_Category')
	alter table [{databaseSchema}].[{objectQualifier}Category] drop constraint [PK_Category]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}CheckEmail]') and name=N'PK_CheckEmail')
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] drop constraint [PK_CheckEmail]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Choice]') and name=N'PK_Choice')
	alter table [{databaseSchema}].[{objectQualifier}Choice] drop constraint [PK_Choice]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Forum]') and name=N'PK_Forum')
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint [PK_Forum]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ForumAccess]') and name=N'PK_ForumAccess')
	alter table [{databaseSchema}].[{objectQualifier}ForumAccess] drop constraint [PK_ForumAccess]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Group]') and name=N'PK_Group')
	alter table [{databaseSchema}].[{objectQualifier}Group] drop constraint [PK_Group]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Mail]') and name=N'PK_Mail')
	alter table [{databaseSchema}].[{objectQualifier}Mail] drop constraint [PK_Mail]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Message]') and name=N'PK_Message')
	alter table [{databaseSchema}].[{objectQualifier}Message] drop constraint [PK_Message]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}PMessage]') and name=N'PK_PMessage')
	alter table [{databaseSchema}].[{objectQualifier}PMessage] drop constraint [PK_PMessage]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Poll]') and name=N'PK_Poll')
	alter table [{databaseSchema}].[{objectQualifier}Poll] drop constraint [PK_Poll]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Smiley]') and name=N'PK_Smiley')
	alter table [{databaseSchema}].[{objectQualifier}Smiley] drop constraint [PK_Smiley]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Topic]') and name=N'PK_Topic')
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [PK_Topic]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}User]') and name=N'PK_User')
	alter table [{databaseSchema}].[{objectQualifier}User] drop constraint [PK_User]
go

if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}WatchForum]') and name=N'PK_WatchForum')
	alter table [{databaseSchema}].[{objectQualifier}WatchForum] drop constraint [PK_WatchForum]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}WatchTopic]') and name=N'PK_WatchTopic')
	alter table [{databaseSchema}].[{objectQualifier}WatchTopic] drop constraint [PK_WatchTopic]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserGroup]') and name=N'PK_UserGroup')
	alter table [{databaseSchema}].[{objectQualifier}UserGroup] drop constraint [PK_UserGroup]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Rank]') and name=N'PK_Rank')
	alter table [{databaseSchema}].[{objectQualifier}Rank] drop constraint [PK_Rank]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}NntpServer]') and name=N'PK_NntpServer')
	alter table [{databaseSchema}].[{objectQualifier}NntpServer] drop constraint [PK_NntpServer]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}NntpForum]') and name=N'PK_NntpForum')
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] drop constraint [PK_NntpForum]
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}NntpTopic]') and name=N'PK_NntpTopic')
	alter table [{databaseSchema}].[{objectQualifier}NntpTopic] drop constraint [PK_NntpTopic]
go


if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'PK_AccessMask')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop constraint [PK_AccessMask]
go


if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserForum]') and name=N'PK_UserForum')
	alter table [{databaseSchema}].[{objectQualifier}UserForum] drop constraint [PK_UserForum]
go


if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Board]') and name=N'PK_Board')
	alter table [{databaseSchema}].[{objectQualifier}Board] drop constraint [PK_Board]
go


if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Active]') and name=N'PK_Active')
	alter table [{databaseSchema}].[{objectQualifier}Active] drop constraint [PK_Active]
go


if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserPMessage]') and name=N'PK_UserPMessage')
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] drop constraint [PK_UserPMessage]
go


if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Attachment]') and name=N'PK_Attachment')
	alter table [{databaseSchema}].[{objectQualifier}Attachment] drop constraint [PK_Attachment]
go


if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Active]') and name=N'PK_Active')
	alter table [{databaseSchema}].[{objectQualifier}Active] drop constraint [PK_Active]
go


if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}PollVote]') and name=N'PK_PollVote')
	alter table [{databaseSchema}].[{objectQualifier}PollVote] drop constraint [PK_PollVote]
go

if exists(select top 1 1 from sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}GroupMedal]') and name=N'PK_{objectQualifier}GroupMedal_1')
	alter table [{databaseSchema}].[{objectQualifier}GroupMedal] drop constraint [PK_{objectQualifier}GroupMedal_1]
go



/*
** Unique constraints
*/


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}CheckEmail]') and name=N'IX_CheckEmail')
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] drop constraint IX_CheckEmail
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IX_Forum')
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint IX_Forum
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}WatchForum]') and name=N'IX_WatchForum')
	alter table [{databaseSchema}].[{objectQualifier}WatchForum] drop constraint IX_WatchForum
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}WatchTopic]') and name=N'IX_WatchTopic')
	alter table [{databaseSchema}].[{objectQualifier}WatchTopic] drop constraint IX_WatchTopic
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Category]') and name=N'IX_Category')
	alter table [{databaseSchema}].[{objectQualifier}Category] drop constraint IX_Category
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Rank]') and name=N'IX_Rank')
	alter table [{databaseSchema}].[{objectQualifier}Rank] drop constraint IX_Rank
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}User]') and name=N'IX_User')
	alter table [{databaseSchema}].[{objectQualifier}User] drop constraint IX_User
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Group]') and name=N'IX_Group')
	alter table [{databaseSchema}].[{objectQualifier}Group] drop constraint IX_Group
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}BannedIP]') and name=N'IX_BannedIP')
	alter table [{databaseSchema}].[{objectQualifier}BannedIP] drop constraint IX_BannedIP
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Smiley]') and name=N'IX_Smiley')
	alter table [{databaseSchema}].[{objectQualifier}Smiley] drop constraint IX_Smiley
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}BannedIP]') and name=N'IX_BannedIP')
	alter table [{databaseSchema}].[{objectQualifier}BannedIP] drop constraint IX_BannedIP
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Category]') and name=N'IX_Category')
	alter table [{databaseSchema}].[{objectQualifier}Category] drop constraint IX_Category
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}CheckEmail]') and name=N'IX_CheckEmail')
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] drop constraint IX_CheckEmail
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IX_Forum')
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint IX_Forum
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Group]') and name=N'IX_Group')
	alter table [{databaseSchema}].[{objectQualifier}Group] drop constraint IX_Group
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Rank]') and name=N'IX_Rank')
	alter table [{databaseSchema}].[{objectQualifier}Rank] drop constraint IX_Rank
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Smiley]') and name=N'IX_Smiley')
	alter table [{databaseSchema}].[{objectQualifier}Smiley] drop constraint IX_Smiley
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}User]') and name=N'IX_User')
	alter table [{databaseSchema}].[{objectQualifier}User] drop constraint IX_User
go


if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Thanks]') and name=N'IX_{objectQualifier}Thanks')
	alter table [{databaseSchema}].[{objectQualifier}Thanks] drop constraint [IX_{objectQualifier}Thanks]
go

if exists (select top 1 1 from  sys.objects where name=N'IX_{objectQualifier}MessageHistory' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}MessageHistory]'))
	alter table [{databaseSchema}].[{objectQualifier}MessageHistory] drop constraint [IX_{objectQualifier}MessageHistory] 
go 
/* Wrong index */
/* Modified by Herman_Herman for SQL2K Compatibility */

if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Thanks]') and name=N'IX_{objectQualifier}Thanks_UserID')
	alter table [{databaseSchema}].[{objectQualifier}Thanks] drop constraint [IX_{objectQualifier}Thanks_UserID]
go

-- vzrus: to allow duplicate forum names
if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IX_{objectQualifier}Forum')
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop constraint [IX_{objectQualifier}Forum]
go

if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ForumReadTracking]') and name=N'IX_{objectQualifier}ForumReadTracking')
	alter table [{databaseSchema}].[{objectQualifier}ForumReadTracking] drop constraint [IX_{objectQualifier}ForumReadTracking]
go

if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}TopicReadTracking]') and name=N'IX_{objectQualifier}TopicReadTracking')
	alter table [{databaseSchema}].[{objectQualifier}TopicReadTracking] drop constraint [IX_{objectQualifier}TopicReadTracking]
go

if exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ReputationVote]') and name=N'IX_{objectQualifier}ReputationVote')
	alter table [{databaseSchema}].[{objectQualifier}ReputationVote] drop constraint [IX_{objectQualifier}ReputationVote]
go

/* Build new constraints */

/*
** Primary keys
*/

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}BannedIP]') and name=N'PK_{objectQualifier}BannedIP')
	alter table [{databaseSchema}].[{objectQualifier}BannedIP] with nocheck add constraint [PK_{objectQualifier}BannedIP] primary key clustered(ID)
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Buddy]') and name=N'PK_{objectQualifier}Buddy')
	alter table [{databaseSchema}].[{objectQualifier}Buddy] with nocheck add constraint [PK_{objectQualifier}Buddy] primary key clustered(ID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Category]') and name=N'PK_{objectQualifier}Category')
	alter table [{databaseSchema}].[{objectQualifier}Category] with nocheck add constraint [PK_{objectQualifier}Category] primary key clustered(CategoryID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}CheckEmail]') and name=N'PK_{objectQualifier}CheckEmail')
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] with nocheck add constraint [PK_{objectQualifier}CheckEmail] primary key clustered(CheckEmailID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Choice]') and name=N'PK_{objectQualifier}Choice')
	alter table [{databaseSchema}].[{objectQualifier}Choice] with nocheck add constraint [PK_{objectQualifier}Choice] primary key clustered(ChoiceID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Forum]') and name=N'PK_{objectQualifier}Forum')
	alter table [{databaseSchema}].[{objectQualifier}Forum] with nocheck add constraint [PK_{objectQualifier}Forum] primary key clustered(ForumID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ForumAccess]') and name=N'PK_{objectQualifier}ForumAccess')
	alter table [{databaseSchema}].[{objectQualifier}ForumAccess] with nocheck add constraint [PK_{objectQualifier}ForumAccess] primary key clustered(GroupID,ForumID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}MessageReported]') and name=N'PK_{objectQualifier}MessageReported')
	alter table [{databaseSchema}].[{objectQualifier}MessageReported] with nocheck add constraint [PK_{objectQualifier}MessageReported] primary key clustered(MessageID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Group]') and name=N'PK_{objectQualifier}Group')
	alter table [{databaseSchema}].[{objectQualifier}Group] with nocheck add constraint [PK_{objectQualifier}Group] primary key clustered(GroupID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}GroupMedal]') and name=N'PK_{objectQualifier}GroupMedal')
	alter table [{databaseSchema}].[{objectQualifier}GroupMedal] with nocheck add constraint [PK_{objectQualifier}GroupMedal] primary key clustered(MedalID,GroupID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ForumReadTracking]') and name=N'PK_{objectQualifier}ForumReadTracking')
	alter table [{databaseSchema}].[{objectQualifier}ForumReadTracking] with nocheck add constraint [PK_{objectQualifier}ForumReadTracking] primary key clustered(UserID,ForumID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}TopicReadTracking]') and name=N'PK_{objectQualifier}TopicReadTracking')
	alter table [{databaseSchema}].[{objectQualifier}TopicReadTracking] with nocheck add constraint [PK_{objectQualifier}TopicReadTracking] primary key clustered(UserID,TopicID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserMedal]') and name=N'PK_{objectQualifier}UserMedal')
	alter table [{databaseSchema}].[{objectQualifier}UserMedal] with nocheck add constraint [PK_{objectQualifier}UserMedal] primary key clustered(MedalID,UserID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Mail]') and name=N'PK_{objectQualifier}Mail')
	alter table [{databaseSchema}].[{objectQualifier}Mail] with nocheck add constraint [PK_{objectQualifier}Mail] primary key clustered(MailID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserProfile]') and name=N'PK_{objectQualifier}UserProfile')
	alter table [{databaseSchema}].[{objectQualifier}UserProfile] with nocheck add constraint [PK_{objectQualifier}UserProfile] primary key clustered(UserID,ApplicationName)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Message]') and name=N'PK_{objectQualifier}Message')
	alter table [{databaseSchema}].[{objectQualifier}Message] with nocheck add constraint [PK_{objectQualifier}Message] primary key clustered(MessageID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}PMessage]') and name=N'PK_{objectQualifier}PMessage')
	alter table [{databaseSchema}].[{objectQualifier}PMessage] with nocheck add constraint [PK_{objectQualifier}PMessage] primary key clustered(PMessageID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}PollGroupCluster]') and name=N'PK_{objectQualifier}PollGroupCluster')
	alter table [{databaseSchema}].[{objectQualifier}PollGroupCluster] with nocheck add constraint [PK_{objectQualifier}PollGroupCluster] primary key clustered(PollGroupID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Poll]') and name=N'PK_{objectQualifier}Poll')
	alter table [{databaseSchema}].[{objectQualifier}Poll] with nocheck add constraint [PK_{objectQualifier}Poll] primary key clustered(PollID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Smiley]') and name=N'PK_{objectQualifier}Smiley')
	alter table [{databaseSchema}].[{objectQualifier}Smiley] with nocheck add constraint [PK_{objectQualifier}Smiley] primary key clustered(SmileyID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Topic]') and name=N'PK_{objectQualifier}Topic')
	alter table [{databaseSchema}].[{objectQualifier}Topic] with nocheck add constraint [PK_{objectQualifier}Topic] primary key clustered(TopicID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}FavoriteTopic]') and name=N'PK_{objectQualifier}FavoriteTopic')
	alter table [{databaseSchema}].[{objectQualifier}FavoriteTopic] with nocheck add constraint [PK_{objectQualifier}FavoriteTopic] primary key clustered(ID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}User]') and ( name=N'PK_{objectQualifier}User' OR name=N'PK_{objectQualifier}{objectQualifier}User' ) )
	alter table [{databaseSchema}].[{objectQualifier}User] with nocheck add constraint [PK_{objectQualifier}User] primary key clustered(UserID)   
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}WatchForum]') and name=N'PK_{objectQualifier}WatchForum')
	alter table [{databaseSchema}].[{objectQualifier}WatchForum] with nocheck add constraint [PK_{objectQualifier}WatchForum] primary key clustered(WatchForumID)   
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}WatchTopic]') and name=N'PK_{objectQualifier}WatchTopic')
	alter table [{databaseSchema}].[{objectQualifier}WatchTopic] with nocheck add constraint [PK_{objectQualifier}WatchTopic] primary key clustered(WatchTopicID)   
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserGroup]') and name=N'PK_{objectQualifier}UserGroup')
	alter table [{databaseSchema}].[{objectQualifier}UserGroup] with nocheck add constraint [PK_{objectQualifier}UserGroup] primary key clustered(UserID,GroupID)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Rank]') and name=N'PK_{objectQualifier}Rank')
	alter table [{databaseSchema}].[{objectQualifier}Rank] with nocheck add constraint [PK_{objectQualifier}Rank] primary key clustered(RankID)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}NntpServer]') and name=N'PK_{objectQualifier}NntpServer')
	alter table [{databaseSchema}].[{objectQualifier}NntpServer] with nocheck add constraint [PK_{objectQualifier}NntpServer] primary key clustered (NntpServerID) 
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}NntpForum]') and name=N'PK_{objectQualifier}NntpForum')
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] with nocheck add constraint [PK_{objectQualifier}NntpForum] primary key clustered (NntpForumID) 
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}NntpTopic]') and name=N'PK_{objectQualifier}NntpTopic')
	alter table [{databaseSchema}].[{objectQualifier}NntpTopic] with nocheck add constraint [PK_{objectQualifier}NntpTopic] primary key clustered (NntpTopicID) 
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'PK_{objectQualifier}AccessMask')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] with nocheck add constraint [PK_{objectQualifier}AccessMask] primary key clustered (AccessMaskID) 
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserForum]') and name=N'PK_{objectQualifier}UserForum')
	alter table [{databaseSchema}].[{objectQualifier}UserForum] with nocheck add constraint [PK_{objectQualifier}UserForum] primary key clustered (UserID,ForumID) 
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Board]') and name=N'PK_{objectQualifier}Board')
	alter table [{databaseSchema}].[{objectQualifier}Board] with nocheck add constraint [PK_{objectQualifier}Board] primary key clustered (BoardID)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Active]') and name=N'PK_{objectQualifier}Active')
	alter table [{databaseSchema}].[{objectQualifier}Active] with nocheck add constraint [PK_{objectQualifier}Active] primary key clustered(SessionID,BoardID)
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserPMessage]') and name=N'PK_{objectQualifier}UserPMessage')
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] with nocheck add constraint [PK_{objectQualifier}UserPMessage] primary key clustered (UserPMessageID) 
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Attachment]') and name=N'PK_{objectQualifier}Attachment')
	alter table [{databaseSchema}].[{objectQualifier}Attachment] with nocheck add constraint [PK_{objectQualifier}Attachment] primary key clustered (AttachmentID) 
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Active]') and name=N'PK_{objectQualifier}Active')
	alter table [{databaseSchema}].[{objectQualifier}Active] with nocheck add constraint [PK_{objectQualifier}Active] primary key clustered(SessionID,BoardID)
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}PollVote]') and name=N'PK_{objectQualifier}PollVote')
	alter table [{databaseSchema}].[{objectQualifier}PollVote] with nocheck add constraint [PK_{objectQualifier}PollVote] primary key clustered(PollVoteID)
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}IgnoreUser]') and name=N'PK_{objectQualifier}IgnoreUser')
	alter table [{databaseSchema}].[{objectQualifier}IgnoreUser] with nocheck add constraint [PK_{objectQualifier}IgnoreUser] PRIMARY KEY CLUSTERED (UserID, IgnoredUserID)
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ShoutboxMessage]') and name=N'PK_{objectQualifier}ShoutboxMessage')
	alter table [{databaseSchema}].[{objectQualifier}ShoutboxMessage] with nocheck add constraint [PK_{objectQualifier}ShoutboxMessage] PRIMARY KEY CLUSTERED (ShoutBoxMessageID)
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Thanks]') and name=N'PK_{objectQualifier}Thanks')
	alter table [{databaseSchema}].[{objectQualifier}Thanks] with nocheck add constraint [PK_{objectQualifier}Thanks] PRIMARY KEY CLUSTERED (ThanksID)
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}MessageHistory]') and name=N'PK_{objectQualifier}MessageHistory')
	alter table [{databaseSchema}].[{objectQualifier}MessageHistory] with nocheck add constraint  [PK_{objectQualifier}MessageHistory] primary key clustered (MessageID,Edited)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ActiveAccess]') and name=N'PK_{objectQualifier}ActiveAccess')
	alter table [{databaseSchema}].[{objectQualifier}ActiveAccess] with nocheck add constraint  [PK_{objectQualifier}ActiveAccess] primary key clustered (UserID,ForumID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ReputationVote]') and name=N'PK_{objectQualifier}ReputationVote')
	alter table [{databaseSchema}].[{objectQualifier}ReputationVote] with nocheck add constraint  [PK_{objectQualifier}ReputationVote] primary key clustered (ReputationFromUserID,ReputationToUserID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}AdminPageUserAccess]') and name=N'PK_{objectQualifier}AdminPageUserAccess')
	alter table [{databaseSchema}].[{objectQualifier}AdminPageUserAccess] with nocheck add constraint [PK_{objectQualifier}AdminPageUserAccess] primary key clustered(UserID,PageName)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}EventLogGroupAccess]') and name=N'PK_{objectQualifier}EventLogGroupAccess')
	alter table [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] with nocheck add constraint [PK_{objectQualifier}EventLogGroupAccess] primary key clustered(GroupID,EventTypeID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}AccessMaskHistory]') and name=N'PK_{objectQualifier}AccessMaskHistory')
	alter table [{databaseSchema}].[{objectQualifier}AccessMaskHistory] with nocheck add constraint  [PK_{objectQualifier}AccessMaskHistory] primary key clustered (AccessMaskID,ChangedDate)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}ForumHistory]') and name=N'PK_{objectQualifier}ForumHistory')
	alter table [{databaseSchema}].[{objectQualifier}ForumHistory] with nocheck add constraint  [PK_{objectQualifier}ForumHistory] primary key clustered (ForumID,ChangedDate)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}GroupHistory]') and name=N'PK_{objectQualifier}GroupHistory')
	alter table [{databaseSchema}].[{objectQualifier}GroupHistory] with nocheck add constraint  [PK_{objectQualifier}GroupHistory] primary key clustered (GroupID,ChangedDate)   
go

/*
** Unique constraints
*/

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}CheckEmail]') and name=N'IX_{objectQualifier}CheckEmail')
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] add constraint [IX_{objectQualifier}CheckEmail] unique nonclustered ([Hash])   
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}WatchForum]') and name=N'IX_{objectQualifier}WatchForum')
	alter table [{databaseSchema}].[{objectQualifier}WatchForum] add constraint [IX_{objectQualifier}WatchForum] unique nonclustered (ForumID,UserID)   
go 

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}UserProfile]') and name=N'IX_{objectQualifier}UserProfile')
	alter table [{databaseSchema}].[{objectQualifier}UserProfile] add constraint [IX_{objectQualifier}UserProfile] unique nonclustered (UserID,ApplicationName)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}WatchTopic]') and name=N'IX_{objectQualifier}WatchTopic')
	alter table [{databaseSchema}].[{objectQualifier}WatchTopic] add constraint [IX_{objectQualifier}WatchTopic] unique nonclustered (TopicID,UserID)   
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Category]') and name=N'IX_{objectQualifier}Category')
	alter table [{databaseSchema}].[{objectQualifier}Category] add constraint [IX_{objectQualifier}Category] unique nonclustered(BoardID,Name)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Rank]') and name=N'IX_{objectQualifier}Rank')
	alter table [{databaseSchema}].[{objectQualifier}Rank] add constraint [IX_{objectQualifier}Rank] unique(BoardID,Name)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}User]') and name=N'IX_{objectQualifier}User')
	alter table [{databaseSchema}].[{objectQualifier}User] add constraint [IX_{objectQualifier}User] unique nonclustered(BoardID,Name)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Group]') and name=N'IX_{objectQualifier}Group')
	alter table [{databaseSchema}].[{objectQualifier}Group] add constraint [IX_{objectQualifier}Group] unique nonclustered(BoardID,Name)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}BannedIP]') and name=N'IX_{objectQualifier}BannedIP')
	alter table [{databaseSchema}].[{objectQualifier}BannedIP] add constraint [IX_{objectQualifier}BannedIP] unique nonclustered(BoardID,Mask)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Smiley]') and name=N'IX_{objectQualifier}Smiley')
	alter table [{databaseSchema}].[{objectQualifier}Smiley] add constraint [IX_{objectQualifier}Smiley] unique nonclustered(BoardID,Code)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}BannedIP]') and name=N'IX_{objectQualifier}BannedIP')
	alter table [{databaseSchema}].[{objectQualifier}BannedIP] add constraint [IX_{objectQualifier}BannedIP] unique nonclustered(BoardID,Mask)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Category]') and name=N'IX_{objectQualifier}Category')
	alter table [{databaseSchema}].[{objectQualifier}Category] add constraint [IX_{objectQualifier}Category] unique nonclustered(BoardID,Name)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}CheckEmail]') and name=N'IX_{objectQualifier}CheckEmail')
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] add constraint [IX_{objectQualifier}CheckEmail] unique nonclustered([Hash])
go


/* if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IX_{objectQualifier}Forum')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add constraint IX_{objectQualifier}Forum unique nonclustered(CategoryID,Name)   
*/


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Group]') and name=N'IX_{objectQualifier}Group')
	alter table [{databaseSchema}].[{objectQualifier}Group] add constraint [IX_{objectQualifier}Group] unique nonclustered(BoardID,Name)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Rank]') and name=N'IX_{objectQualifier}Rank')
	alter table [{databaseSchema}].[{objectQualifier}Rank] add constraint [IX_{objectQualifier}Rank] unique nonclustered(BoardID,Name)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Smiley]') and name=N'IX_{objectQualifier}Smiley')
	alter table [{databaseSchema}].[{objectQualifier}Smiley] add constraint [IX_{objectQualifier}Smiley] unique nonclustered(BoardID,Code)
go


if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}User]') and name=N'IX_{objectQualifier}User')
	alter table [{databaseSchema}].[{objectQualifier}User] add constraint [IX_{objectQualifier}User] unique nonclustered(BoardID,Name)
go

if not exists (select top 1 1 from  sys.indexes  where object_id = object_id('[{databaseSchema}].[{objectQualifier}Buddy]') and name=N'IX_{objectQualifier}Buddy')
	alter table [{databaseSchema}].[{objectQualifier}Buddy] add constraint [IX_{objectQualifier}Buddy] unique nonclustered([FromUserID],[ToUserID])
go

/*
** Foreign keys
*/


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Active_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Active] add constraint [FK_{objectQualifier}Active_{objectQualifier}Forum] foreign key (ForumID) references [{databaseSchema}].[{objectQualifier}Forum] (ForumID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Active_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Active] add constraint [FK_{objectQualifier}Active_{objectQualifier}Topic] foreign key (TopicID) references [{databaseSchema}].[{objectQualifier}Topic] (TopicID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Active_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Active] add constraint [FK_{objectQualifier}Active_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}CheckEmail_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}CheckEmail]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] add constraint [FK_{objectQualifier}CheckEmail_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Choice_{objectQualifier}Poll' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Choice]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Choice] add constraint [FK_{objectQualifier}Choice_{objectQualifier}Poll] foreign key (PollID) references [{databaseSchema}].[{objectQualifier}Poll] (PollID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}FavoriteTopic_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}FavoriteTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}FavoriteTopic] add constraint [FK_{objectQualifier}FavoriteTopic_{objectQualifier}Topic] foreign key (TopicID) references [{databaseSchema}].[{objectQualifier}Topic] (TopicID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}FavoriteTopic_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}FavoriteTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}FavoriteTopic] add constraint [FK_{objectQualifier}FavoriteTopic_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserProfile_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserProfile]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserProfile] add constraint [FK_{objectQualifier}UserProfile_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID) on delete cascade
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Forum_{objectQualifier}Category' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] add constraint [FK_{objectQualifier}Forum_{objectQualifier}Category] foreign key (CategoryID) references [{databaseSchema}].[{objectQualifier}Category] (CategoryID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Forum_{objectQualifier}Message' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] add constraint [FK_{objectQualifier}Forum_{objectQualifier}Message] foreign key (LastMessageID) references [{databaseSchema}].[{objectQualifier}Message] (MessageID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Forum_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] add constraint [FK_{objectQualifier}Forum_{objectQualifier}Topic] foreign key (LastTopicID) references [{databaseSchema}].[{objectQualifier}Topic] (TopicID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Forum_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] add constraint [FK_{objectQualifier}Forum_{objectQualifier}User] foreign key (LastUserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}ForumAccess_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumAccess]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumAccess] add constraint [FK_{objectQualifier}ForumAccess_{objectQualifier}Forum] foreign key (ForumID) references [{databaseSchema}].[{objectQualifier}Forum] (ForumID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}ForumAccess_{objectQualifier}Group' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumAccess]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumAccess] add constraint [FK_{objectQualifier}ForumAccess_{objectQualifier}Group] foreign key (GroupID) references [{databaseSchema}].[{objectQualifier}Group] (GroupID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Message_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Message]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Message] add constraint [FK_{objectQualifier}Message_{objectQualifier}Topic] foreign key (TopicID) references [{databaseSchema}].[{objectQualifier}Topic] (TopicID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Message_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Message]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Message] add constraint [FK_{objectQualifier}Message_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}PMessage_{objectQualifier}User1' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}PMessage]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}PMessage] add constraint [FK_{objectQualifier}PMessage_{objectQualifier}User1] foreign key (FromUserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Topic_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] add constraint [FK_{objectQualifier}Topic_{objectQualifier}Forum] foreign key (ForumID) references [{databaseSchema}].[{objectQualifier}Forum] (ForumID) ON DELETE CASCADE
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Topic_{objectQualifier}Message' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] add constraint [FK_{objectQualifier}Topic_{objectQualifier}Message] foreign key (LastMessageID) references [{databaseSchema}].[{objectQualifier}Message] (MessageID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Topic_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] add constraint [FK_{objectQualifier}Topic_{objectQualifier}Topic] foreign key (TopicMovedID) references [{databaseSchema}].[{objectQualifier}Topic] (TopicID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Topic_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] add constraint [FK_{objectQualifier}Topic_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Topic_{objectQualifier}User2' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] add constraint [FK_{objectQualifier}Topic_{objectQualifier}User2] foreign key (LastUserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}WatchForum_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}WatchForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}WatchForum] add constraint [FK_{objectQualifier}WatchForum_{objectQualifier}Forum] foreign key (ForumID) references [{databaseSchema}].[{objectQualifier}Forum](ForumID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}WatchForum_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}WatchForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}WatchForum] add constraint [FK_{objectQualifier}WatchForum_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User](UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}WatchTopic_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}WatchTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}WatchTopic] add constraint [FK_{objectQualifier}WatchTopic_{objectQualifier}Topic] foreign key (TopicID) references [{databaseSchema}].[{objectQualifier}Topic](TopicID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}WatchTopic_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}WatchTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}WatchTopic] add constraint [FK_{objectQualifier}WatchTopic_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User](UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Active_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Attachment] add constraint [FK_{objectQualifier}Active_{objectQualifier}Forum] foreign key (MessageID) references [{databaseSchema}].[{objectQualifier}Message] (MessageID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserGroup_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserGroup]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserGroup] add constraint [FK_{objectQualifier}UserGroup_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User](UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserGroup_{objectQualifier}Group' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserGroup]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserGroup] add constraint [FK_{objectQualifier}UserGroup_{objectQualifier}Group] foreign key(GroupID) references [{databaseSchema}].[{objectQualifier}Group] (GroupID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Attachment_{objectQualifier}Message' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Attachment]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Attachment] add constraint [FK_{objectQualifier}Attachment_{objectQualifier}Message] foreign key (MessageID) references [{databaseSchema}].[{objectQualifier}Message] (MessageID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}NntpForum_{objectQualifier}NntpServer' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] add constraint [FK_{objectQualifier}NntpForum_{objectQualifier}NntpServer] foreign key (NntpServerID) references [{databaseSchema}].[{objectQualifier}NntpServer](NntpServerID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}NntpForum_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] add constraint [FK_{objectQualifier}NntpForum_{objectQualifier}Forum] foreign key (ForumID) references [{databaseSchema}].[{objectQualifier}Forum](ForumID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}NntpTopic_{objectQualifier}NntpForum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpTopic] add constraint [FK_{objectQualifier}NntpTopic_{objectQualifier}NntpForum] foreign key (NntpForumID) references [{databaseSchema}].[{objectQualifier}NntpForum](NntpForumID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}NntpTopic_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpTopic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpTopic] add constraint [FK_{objectQualifier}NntpTopic_{objectQualifier}Topic] foreign key (TopicID) references [{databaseSchema}].[{objectQualifier}Topic](TopicID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}ForumAccess_{objectQualifier}AccessMask' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumAccess]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumAccess] add constraint [FK_{objectQualifier}ForumAccess_{objectQualifier}AccessMask] foreign key (AccessMaskID) references [{databaseSchema}].[{objectQualifier}AccessMask] (AccessMaskID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserAlbumImage_{objectQualifier}UserAlbum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserAlbumImage]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserAlbumImage] add constraint [FK_{objectQualifier}UserAlbumImage_{objectQualifier}UserAlbum] foreign key (AlbumID) references [{databaseSchema}].[{objectQualifier}UserAlbum] (AlbumID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserForum_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserForum] add constraint [FK_{objectQualifier}UserForum_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserForum_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserForum] add constraint [FK_{objectQualifier}UserForum_{objectQualifier}Forum] foreign key (ForumID) references [{databaseSchema}].[{objectQualifier}Forum] (ForumID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserForum_{objectQualifier}AccessMask' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserForum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserForum] add constraint [FK_{objectQualifier}UserForum_{objectQualifier}AccessMask] foreign key (AccessMaskID) references [{databaseSchema}].[{objectQualifier}AccessMask] (AccessMaskID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Category_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Category]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Category] add constraint [FK_{objectQualifier}Category_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board] (BoardID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}AccessMask_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}AccessMask]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add constraint [FK_{objectQualifier}AccessMask_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board] (BoardID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}AccessMaskHistory_{objectQualifier}AccessMask' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}AccessMaskHistory]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}AccessMaskHistory] add constraint [FK_{objectQualifier}AccessMaskHistory_{objectQualifier}AccessMask] foreign key(AccessMaskID) references [{databaseSchema}].[{objectQualifier}AccessMask] (AccessMaskID) ON DELETE CASCADE
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Active_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Active]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Active] add constraint [FK_{objectQualifier}Active_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board] (BoardID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}BannedIP_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}BannedIP]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}BannedIP] add constraint [FK_{objectQualifier}BannedIP_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board] (BoardID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Group_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Group]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Group] add constraint [FK_{objectQualifier}Group_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board] (BoardID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}GroupMedal_{objectQualifier}Group' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}GroupMedal]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}GroupMedal] add constraint [FK_{objectQualifier}GroupMedal_{objectQualifier}Group] foreign key(GroupID) references [{databaseSchema}].[{objectQualifier}Group] (GroupID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}GroupHistory_{objectQualifier}Group' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}GroupHistory]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}GroupHistory] add constraint [FK_{objectQualifier}GroupHistory_{objectQualifier}Group] foreign key(GroupID) references [{databaseSchema}].[{objectQualifier}Group] (GroupID)  ON DELETE CASCADE
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}GroupMedal_{objectQualifier}Medal' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}GroupMedal]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}GroupMedal] add constraint [FK_{objectQualifier}GroupMedal_{objectQualifier}Medal] foreign key(MedalID) references [{databaseSchema}].[{objectQualifier}Medal] (MedalID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserMedal_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserMedal]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserMedal] add constraint [FK_{objectQualifier}UserMedal_{objectQualifier}User] foreign key(UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserMedal_{objectQualifier}Medal' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserMedal]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserMedal] add constraint [FK_{objectQualifier}UserMedal_{objectQualifier}Medal] foreign key(MedalID) references [{databaseSchema}].[{objectQualifier}Medal] (MedalID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserProfile_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserProfile]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserProfile] add constraint [FK_{objectQualifier}UserProfile_{objectQualifier}User] foreign key(UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go

 if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}MessageReportedAudit_{objectQualifier}MessageReported' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}MessageReportedAudit]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}MessageReportedAudit] add constraint [FK_{objectQualifier}MessageReportedAudit_{objectQualifier}MessageReported] foreign key(MessageID) references [{databaseSchema}].[{objectQualifier}MessageReported] (MessageID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}NntpServer_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}NntpServer]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}NntpServer] add constraint [FK_{objectQualifier}NntpServer_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board] (BoardID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Rank_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Rank]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Rank] add constraint [FK_{objectQualifier}Rank_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board] (BoardID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Smiley_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Smiley]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Smiley] add constraint [FK_{objectQualifier}Smiley_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board] (BoardID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}User_{objectQualifier}Rank' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}User]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}User] add constraint [FK_{objectQualifier}User_{objectQualifier}Rank] foreign key(RankID) references [{databaseSchema}].[{objectQualifier}Rank](RankID)
go


if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}User_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}User]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}User] add constraint [FK_{objectQualifier}User_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board](BoardID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Forum_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] add constraint [FK_{objectQualifier}Forum_{objectQualifier}Forum] foreign key(ParentID) references [{databaseSchema}].[{objectQualifier}Forum](ForumID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Message_{objectQualifier}Message' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Message]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Message] add constraint [FK_{objectQualifier}Message_{objectQualifier}Message] foreign key(ReplyTo) references [{databaseSchema}].[{objectQualifier}Message](MessageID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserPMessage_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserPMessage]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] add constraint [FK_{objectQualifier}UserPMessage_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}UserPMessage_{objectQualifier}PMessage' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}UserPMessage]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] add constraint [FK_{objectQualifier}UserPMessage_{objectQualifier}PMessage] foreign key (PMessageID) references [{databaseSchema}].[{objectQualifier}PMessage] (PMessageID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Registry_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Registry]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Registry] add constraint [FK_{objectQualifier}Registry_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board](BoardID) on delete cascade
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}PollVote_{objectQualifier}Poll' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}PollVote]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}PollVote] add constraint [FK_{objectQualifier}PollVote_{objectQualifier}Poll] foreign key(PollID) references [{databaseSchema}].[{objectQualifier}Poll](PollID) on delete cascade
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Poll_{objectQualifier}PollGroupCluster' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Poll]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Poll] add constraint [FK_{objectQualifier}Poll_{objectQualifier}PollGroupCluster] foreign key(PollGroupID) references [{databaseSchema}].[{objectQualifier}PollGroupCluster](PollGroupID)  on delete cascade 
go

 if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Topic_{objectQualifier}PollGroupCluster' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] add constraint [FK_{objectQualifier}Topic_{objectQualifier}PollGroupCluster] foreign key(PollID) references [{databaseSchema}].[{objectQualifier}PollGroupCluster](PollGroupID)  

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Forum_{objectQualifier}PollGroupCluster' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Forum] add constraint [FK_{objectQualifier}Forum_{objectQualifier}PollGroupCluster] foreign key(PollGroupID) references [{databaseSchema}].[{objectQualifier}PollGroupCluster](PollGroupID)  

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}ForumHistory_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumHistory]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumHistory] add constraint [FK_{objectQualifier}ForumHistory_{objectQualifier}Forum] foreign key(ForumID) references [{databaseSchema}].[{objectQualifier}Forum](ForumID)  ON DELETE CASCADE 

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Category_{objectQualifier}PollGroupCluster' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Category]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Category] add constraint [FK_{objectQualifier}Category_{objectQualifier}PollGroupCluster] foreign key(PollGroupID) references [{databaseSchema}].[{objectQualifier}PollGroupCluster](PollGroupID)  

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}EventLog_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}EventLog]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}EventLog] add constraint [FK_{objectQualifier}EventLog_{objectQualifier}User] foreign key(UserID) references [{databaseSchema}].[{objectQualifier}User](UserID) on delete cascade
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Extension_{objectQualifier}Board' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Extension]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Extension] add constraint [FK_{objectQualifier}Extension_{objectQualifier}Board] foreign key(BoardID) references [{databaseSchema}].[{objectQualifier}Board](BoardID) on delete cascade
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}BBCode_Board' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}BBCode]') and type in (N'F'))
	ALTER TABLE [{databaseSchema}].[{objectQualifier}BBCode] ADD CONSTRAINT [FK_{objectQualifier}BBCode_Board] FOREIGN KEY([BoardID]) REFERENCES [{databaseSchema}].[{objectQualifier}Board] ([BoardID]) ON DELETE NO ACTION
GO

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Buddy_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Buddy]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Buddy] add constraint [FK_{objectQualifier}Buddy_{objectQualifier}User] foreign key(FromUserID) references [{databaseSchema}].[{objectQualifier}User] (UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}Thanks_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}Thanks]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Thanks] add constraint [FK_{objectQualifier}Thanks_{objectQualifier}User] foreign key (ThanksFromUserID) references [{databaseSchema}].[{objectQualifier}User](UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}TopicTags_{objectQualifier}Tags' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}TopicTags]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}TopicTags] add constraint [FK_{objectQualifier}TopicTags_{objectQualifier}Tags] foreign key (TagID) references [{databaseSchema}].[{objectQualifier}Tags](TagID) on delete cascade
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}TopicTags_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}TopicTags]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}TopicTags] add constraint [FK_{objectQualifier}TopicTags_{objectQualifier}Topic] foreign key (TopicID) references [{databaseSchema}].[{objectQualifier}Topic](TopicID) on delete cascade
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}MessageHistory_MessageID' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}MessageHistory]') and type in (N'F'))
	ALTER TABLE [{databaseSchema}].[{objectQualifier}MessageHistory] ADD CONSTRAINT [FK_{objectQualifier}MessageHistory_MessageID] FOREIGN KEY([MessageID]) REFERENCES [{databaseSchema}].[{objectQualifier}Message] ([MessageID]) 

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}ForumReadTracking_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumReadTracking]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumReadTracking] add constraint [FK_{objectQualifier}ForumReadTracking_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User](UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}ForumReadTracking_{objectQualifier}Forum' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ForumReadTracking]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ForumReadTracking] add constraint [FK_{objectQualifier}ForumReadTracking_{objectQualifier}Forum] foreign key (ForumID) references [{databaseSchema}].[{objectQualifier}Forum](ForumID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}TopicReadTracking_{objectQualifier}User' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}TopicReadTracking]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}TopicReadTracking] add constraint [FK_{objectQualifier}TopicReadTracking_{objectQualifier}User] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User](UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}TopicReadTracking_{objectQualifier}Topic' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}TopicReadTracking]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}TopicReadTracking] add constraint [FK_{objectQualifier}TopicReadTracking_{objectQualifier}Topic] foreign key (TopicID) references [{databaseSchema}].[{objectQualifier}Topic](TopicID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}ReputationVote_{objectQualifier}User_From' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ReputationVote]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ReputationVote] add constraint [FK_{objectQualifier}ReputationVote_{objectQualifier}User_From] foreign key (ReputationFromUserID) references [{databaseSchema}].[{objectQualifier}User](UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}ReputationVote_{objectQualifier}User_To' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}ReputationVote]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}ReputationVote] add constraint [FK_{objectQualifier}ReputationVote_{objectQualifier}User_To] foreign key (ReputationToUserID) references [{databaseSchema}].[{objectQualifier}User](UserID)
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}AdminPageUserAccess_{objectQualifier}UserID' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}AdminPageUserAccess]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}AdminPageUserAccess] add constraint [FK_{objectQualifier}AdminPageUserAccess_{objectQualifier}UserID] foreign key (UserID) references [{databaseSchema}].[{objectQualifier}User](UserID) ON DELETE CASCADE
go

if not exists (select top 1 1 from  sys.objects where name=N'FK_{objectQualifier}EventLogGroupAccess_{objectQualifier}GroupID' and parent_object_id=object_id('[{databaseSchema}].[{objectQualifier}EventLogGroupAccess]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] add constraint [FK_{objectQualifier}EventLogGroupAccess_{objectQualifier}GroupID] foreign key (GroupID) references [{databaseSchema}].[{objectQualifier}Group](GroupID) ON DELETE CASCADE
go

/* Default Constraints */
if exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}Message_Flags' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}Message]'))
	alter table [{databaseSchema}].[{objectQualifier}Message] drop constraint [DF_{objectQualifier}Message_Flags]
go

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}Message_Flags' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}Message]'))
	alter table [{databaseSchema}].[{objectQualifier}Message] add constraint [DF_{objectQualifier}Message_Flags] default (23) for Flags
go

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}Rank_PMLimit' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}Rank]'))
	alter table [{databaseSchema}].[{objectQualifier}Rank] add constraint [DF_{objectQualifier}Rank_PMLimit] default (0) for PMLimit
go

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}Group_PMLimit' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}Group]'))
	alter table [{databaseSchema}].[{objectQualifier}Group] add constraint [DF_{objectQualifier}Group_PMLimit] default (30) for PMLimit
go

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}User_PMNotification' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}User]'))
	alter table [{databaseSchema}].[{objectQualifier}User] add constraint [DF_{objectQualifier}User_PMNotification] default (1) for PMNotification
go

if exists (select top 1 1 from  sys.objects where name=N'DF_EventLog_EventTime' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}EventLog]'))
	alter table [{databaseSchema}].[{objectQualifier}EventLog] drop constraint [DF_EventLog_EventTime]
go

if exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}EventLog_EventTime' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}EventLog]'))
	alter table [{databaseSchema}].[{objectQualifier}EventLog] drop constraint [DF_{objectQualifier}EventLog_EventTime]
go

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}EventLog_EventTime' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}EventLog]'))
	alter table [{databaseSchema}].[{objectQualifier}EventLog] add constraint [DF_{objectQualifier}EventLog_EventTime] default(GETUTCDATE() ) for EventTime
go


if exists (select top 1 1 from  sys.objects where name=N'DF_EventLog_Type' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}EventLog]'))
	alter table [{databaseSchema}].[{objectQualifier}EventLog] drop constraint [DF_EventLog_Type]
go

exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}ActiveAccess, IsGuestX')
GO

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}ActiveAccess_IsGuestX' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}ActiveAccess]'))
	alter table [{databaseSchema}].[{objectQualifier}ActiveAccess] add constraint [DF_{objectQualifier}ActiveAccess_IsGuestX] default(0) for IsGuestX
go

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}EventLog_Type' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}EventLog]'))
	alter table [{databaseSchema}].[{objectQualifier}EventLog] add constraint [DF_{objectQualifier}EventLog_Type] default(0) for Type
go

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}Extension_BoardID' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}Extension]'))
	alter table [{databaseSchema}].[{objectQualifier}Extension] add constraint [DF_{objectQualifier}Extension_BoardID] default(1) for BoardID
go

if not exists (select top 1 1 from  sys.objects where name=N'DF_{objectQualifier}ActiveAccess_IsGuestX' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}ActiveAccess]'))
	alter table [{databaseSchema}].[{objectQualifier}ActiveAccess] add constraint [DF_{objectQualifier}ActiveAccess_IsGuestX] default(1) for BoardID
go


/***** VIEWS ******/

/****** Object:  Index [{objectQualifier}vaccess_user_UserForum]    Script Date: 09/28/2009 22:30:20 ******/

/****** Object:  Index [{objectQualifier}vaccess_user_UserForum]    Script Date: 09/28/2009 22:30:20 ******/
IF NOT exists (select top 1 1 from sys.indexes WHERE object_id = OBJECT_ID(N'[{databaseSchema}].[{objectQualifier}vaccess_user]') AND name = N'{objectQualifier}vaccess_user_UserForum_PK')
SET ARITHABORT ON
CREATE UNIQUE CLUSTERED INDEX [{objectQualifier}vaccess_user_UserForum_PK] ON [{databaseSchema}].[{objectQualifier}vaccess_user] 
(
	[UserID] ASC,
	[ForumID] ASC,
	[AccessMaskID] ASC,
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index [{objectQualifier}vaccess_null_UserForum]    Script Date: 09/28/2009 22:30:36 ******/
IF NOT exists (select top 1 1 from sys.indexes WHERE object_id = OBJECT_ID(N'[{databaseSchema}].[{objectQualifier}vaccess_null]') AND name = N'{objectQualifier}vaccess_null_UserForum_PK')
SET ARITHABORT ON
CREATE UNIQUE CLUSTERED INDEX [{objectQualifier}vaccess_null_UserForum_PK] ON [{databaseSchema}].[{objectQualifier}vaccess_null] 
(
	[UserID] ASC,
	[ForumID] ASC,
	[AccessMaskID] ASC,
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index [{objectQualifier}vaccess_group_UserGroup]    Script Date: 09/28/2009 22:30:55 ******/
IF NOT exists (select top 1 1 from sys.indexes WHERE object_id = OBJECT_ID(N'[{databaseSchema}].[{objectQualifier}vaccess_group]') AND name = N'{objectQualifier}vaccess_group_UserForum_PK')
SET ARITHABORT ON
CREATE UNIQUE CLUSTERED INDEX [{objectQualifier}vaccess_group_UserForum_PK] ON [{databaseSchema}].[{objectQualifier}vaccess_group] 
(
	[UserID] ASC,
	[ForumID] ASC,
	[AccessMaskID] ASC,
	[GroupID] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


