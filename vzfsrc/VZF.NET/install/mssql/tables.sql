/* Version 1.0.2 */

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn]
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

/*
** Create missing tables
*/

/* Create Thanks Table */
if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Thanks]') and type in (N'U'))
CREATE TABLE [{databaseSchema}].[{objectQualifier}Thanks](
	[ThanksID] [int] IDENTITY(1,1) NOT NULL,
	[ThanksFromUserID] [int] NOT NULL,
	[ThanksToUserID] [int] NOT NULL,
	[MessageID] [int] NOT NULL,
	[ThanksDate] [smalldatetime] NOT NULL
	constraint [PK_{objectQualifier}Thanks] primary key(ThanksID)
	)
GO

/* YAF Buddy Table */
if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Buddy]') and type in (N'U'))
CREATE TABLE [{databaseSchema}].[{objectQualifier}Buddy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FromUserID] [int] NOT NULL,
	[ToUserID] [int] NOT NULL,
	[Approved] [bit] NOT NULL,
	[Requested] [datetime] NOT NULL
	constraint [PK_{objectQualifier}Buddy] primary key(ID)
	)
GO

/* YAF FavoriteTopic Table */
if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}FavoriteTopic]') and type in (N'U'))
CREATE TABLE [{databaseSchema}].[{objectQualifier}FavoriteTopic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TopicID] [int] NOT NULL
	constraint [PK_{objectQualifier}FavoriteTopic] primary key(ID)
	)
GO

/* YAF Album Tables*/
if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserAlbum]') and type in (N'U'))
CREATE TABLE [{databaseSchema}].[{objectQualifier}UserAlbum](
	[AlbumID] [INT] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Title] [NVARCHAR](255),
	[CoverImageID] [INT],
	[Updated] [DATETIME] NOT NULL
	constraint [PK_{objectQualifier}User_Album] primary key(AlbumID)
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserAlbumImage]') and type in (N'U'))
CREATE TABLE [{databaseSchema}].[{objectQualifier}UserAlbumImage](
	[ImageID] [INT] IDENTITY(1,1) NOT NULL,
	[AlbumID] [int] NOT NULL,
	[Caption] [NVARCHAR](255),
	[FileName] [NVARCHAR](255) NOT NULL,
	[Bytes] [INT] NOT NULL,
	[ContentType] [NVARCHAR](50),
	[Uploaded] [DATETIME] NOT NULL,
	[Downloads] [INT] NOT NULL
	constraint [PK_{objectQualifier}User_AlbumImage] primary key(ImageID)
	)
GO
if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Active]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Active](
		SessionID		nvarchar (24) NOT NULL ,
		BoardID			int NOT NULL ,
		UserID			int NOT NULL ,
		IP				nvarchar (15) NOT NULL ,
		[Login]			datetime NOT NULL ,
		LastActive		datetime NOT NULL ,
		Location		nvarchar (50) NOT NULL ,
		ForumID			int NULL ,
		TopicID			int NULL ,
		Browser			nvarchar (50) NULL ,
		[Platform]		nvarchar (50) NULL,
		Flags           int NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ActiveAccess]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}ActiveAccess](		
		UserID			    int NOT NULL ,
		BoardID			    int NOT NULL ,			
		ForumID			    int NOT NULL,
		IsAdmin				bit NOT NULL ,
		IsForumModerator	bit NOT NULL ,
		IsModerator			bit NOT NULL ,
		IsGuestX			bit NOT NULL,
		LastActive			datetime NULL ,
		ReadAccess			bit NOT NULL ,
		PostAccess			bit NOT NULL ,
		ReplyAccess			bit NOT NULL,
		PriorityAccess		bit NOT NULL,
		PollAccess			bit NOT NULL,
		VoteAccess			bit NOT NULL,
		ModeratorAccess		bit NOT NULL,
		EditAccess			bit NOT NULL,
		DeleteAccess		bit NOT NULL,
		UploadAccess		bit NOT NULL,		
		DownloadAccess		bit NOT NULL,
		UserForumAccess     bit NOT NULL		
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AdminPageUserAccess]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}AdminPageUserAccess](
		UserID		    int NOT NULL,		
		PageName		nvarchar (128) NOT NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}EventLogGroupAccess]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}EventLogGroupAccess](
		GroupID		    int NOT NULL,	
		EventTypeID     int NOT NULL,  	
		EventTypeName	nvarchar (128) NOT NULL,
		DeleteAccess    bit NOT NULL constraint [DF_{objectQualifier}EventLogGroupAccess_DeleteAccess] default (0)
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}BannedIP]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}BannedIP](
		ID				int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL ,
		Mask			nvarchar (15) NOT NULL ,
		Since			datetime NOT NULL,
		Reason          nvarchar (128) NULL,
		UserID			int null
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Category]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Category](
		CategoryID		int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL ,
		[Name]			[nvarchar](128) NOT NULL,
		[CategoryImage] [nvarchar](255) NULL,		
		SortOrder		smallint NOT NULL,
		PollGroupID int null 
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}CheckEmail]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}CheckEmail](
		CheckEmailID	int IDENTITY (1, 1) NOT NULL ,
		UserID			int NOT NULL ,
		Email			nvarchar (255) NOT NULL ,
		Created			datetime NOT NULL ,
		[Hash]			nvarchar (32) NOT NULL 
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Choice]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Choice](
		ChoiceID		int IDENTITY (1, 1) NOT NULL ,
		PollID			int NOT NULL ,
		Choice			nvarchar (50) NOT NULL ,
		Votes			int NOT NULL,
		[ObjectPath] nvarchar(255) NULL,
		[MimeType] varchar(50) NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVote]') and type in (N'U'))
	CREATE TABLE [{databaseSchema}].[{objectQualifier}PollVote] (
		[PollVoteID] [int] IDENTITY (1, 1) NOT NULL ,
		[PollID] [int] NOT NULL ,
		[UserID] [int] NULL ,
		[RemoteIP] [varchar] (57) NULL,
		[ChoiceID] [int] NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVoteRefuse]') and type in (N'U'))
	CREATE TABLE [{databaseSchema}].[{objectQualifier}PollVoteRefuse] (
		[RefuseID] [int] IDENTITY (1, 1) NOT NULL ,		
		[PollID] [int] NOT NULL ,
		[UserID] [int] NULL ,
		[RemoteIP] [varchar] (57) NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Forum](
		ForumID			int IDENTITY (1, 1) NOT NULL ,
		CategoryID		int NOT NULL ,
		ParentID		int NULL ,
		Name			nvarchar (50) NOT NULL ,
		[Description]	nvarchar (255) NULL ,
		SortOrder		smallint NOT NULL ,
		LastPosted		datetime NULL ,
		LastTopicID		int NULL ,
		LastMessageID	int NULL ,
		LastUserID		int NULL ,
		LastUserName	nvarchar (255) NULL ,
		LastUserDisplayName	nvarchar (255) NULL,
		NumTopics		int NOT NULL,
		NumPosts		int NOT NULL,
		RemoteURL		nvarchar(100) null,
		Flags			int not null constraint [DF_{objectQualifier}Forum_Flags] default (0),
		[IsLocked]		AS (CONVERT([bit],sign([Flags]&(1)),(0))),
		[IsHidden]		AS (CONVERT([bit],sign([Flags]&(2)),(0))),
		[IsNoCount]		AS (CONVERT([bit],sign([Flags]&(4)),(0))),
		[IsModerated]	AS (CONVERT([bit],sign([Flags]&(8)),(0))),
		ThemeURL		nvarchar(50) NULL,
		PollGroupID     int null
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ForumHistory]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}ForumHistory](
		ForumID			    int NOT NULL,
		ChangedUserID		int,	
		ChangedUserName		nvarchar (255) NOT NULL,
		ChangedDisplayName	nvarchar (255) NOT NULL,
		ChangedDate        datetime  NOT NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ForumAccess]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}ForumAccess](
		GroupID			int NOT NULL ,
		ForumID			int NOT NULL ,
		AccessMaskID	int NOT NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Group](
		GroupID		int IDENTITY (1, 1) NOT NULL ,
		BoardID		int NOT NULL ,
		[Name]		nvarchar (255) NOT NULL ,
		Flags		int not null constraint [DF_{objectQualifier}Group_Flags] default (0)
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}GroupHistory]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}GroupHistory](
		GroupID			    int not null,
		ChangedUserID		int,	
		ChangedUserName		nvarchar (255) NOT NULL,
		ChangedDisplayName	nvarchar (255) NOT NULL,
		ChangedDate        datetime  NOT NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Mail]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Mail](
		[MailID] [int] IDENTITY(1,1) NOT NULL,
		[FromUser] [nvarchar](255) NOT NULL,
		[FromUserName] [nvarchar](255) NULL,
		[ToUser] [nvarchar](255) NOT NULL,
		[ToUserName] [nvarchar](255) NULL,
		[Created] [datetime] NOT NULL,
		[Subject] [nvarchar](100) NOT NULL,
		[Body] [ntext] NOT NULL,
		[BodyHtml] [ntext] NULL,
		[SendTries] [int] NOT NULL CONSTRAINT [DF_{objectQualifier}Mail_SendTries]  DEFAULT ((0)),
		[SendAttempt] [datetime] NULL,
		[ProcessID] [int] NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Message](
		MessageID		    int IDENTITY (1, 1) NOT NULL ,
		TopicID			    int NOT NULL ,
		ReplyTo			    int NULL ,
		Position		    int NOT NULL ,
		Indent			    int NOT NULL ,
		UserID			    int NOT NULL ,
		UserName		    nvarchar (255) NULL ,
		UserDisplayName		nvarchar (255) NULL ,
		Posted			    datetime NOT NULL ,
		[Message]		    ntext NOT NULL ,
		IP				    nvarchar (15) NOT NULL ,
		Edited			    datetime NULL ,
		Flags			    int NOT NULL constraint [DF_{objectQualifier}Message_Flags] default (23),
		EditReason          nvarchar (100) NULL ,
		IsModeratorChanged  bit NOT NULL CONSTRAINT [DF_{objectQualifier}Message_IsModeratorChanged] DEFAULT (0),
		DeleteReason        nvarchar (100)  NULL,
		ExternalMessageId	nvarchar(255) NULL,
		ReferenceMessageId	nvarchar(255) NULL,
		IsDeleted		    AS (CONVERT([bit],sign([Flags]&(8)),0)),
		IsApproved		    AS (CONVERT([bit],sign([Flags]&(16)),(0)))
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageHistory]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}MessageHistory](
		MessageID		    int NOT NULL ,
		[Message]		    ntext NOT NULL ,
		IP				    nvarchar (15) NOT NULL ,
		Edited			    datetime NOT NULL,
		EditedBy		    int NULL,	
		EditReason          nvarchar (100) NULL ,
		IsModeratorChanged  bit NOT NULL CONSTRAINT [DF_{objectQualifier}MessageHistory_IsModeratorChanged] DEFAULT (0),
		Flags               int NOT NULL constraint [DF_{objectQualifier}MessageHistory_Flags] default (23)	  
	)
GO

exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}MessageHistory, MessageHistoryID')
GO

IF NOT EXISTS (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageReported]') and type in (N'U'))
	CREATE TABLE [{databaseSchema}].[{objectQualifier}MessageReported](
		[MessageID] [int] NOT NULL,
		[Message] [ntext] NULL,
		[Resolved] [bit] NULL,
		[ResolvedBy] [int] NULL,
		[ResolvedDate] [datetime] NULL
	)
GO

IF NOT EXISTS (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageReportedAudit]') and type in (N'U'))
	CREATE TABLE [{databaseSchema}].[{objectQualifier}MessageReportedAudit](
		[LogID] [int] IDENTITY(1,1) NOT NULL,
		[UserID] [int] NULL,
		[MessageID] [int] NOT NULL,
		[Reported] [datetime] NULL
		)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PMessage]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}PMessage](
		PMessageID		int IDENTITY (1, 1) NOT NULL ,
		ReplyTo			    int NULL ,
		FromUserID		int NOT NULL ,
		Created			datetime NOT NULL ,
		[Subject]		nvarchar (100) NOT NULL ,
		Body			ntext NOT NULL,
		Flags			int NOT NULL 
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollGroupCluster]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}PollGroupCluster](		
		PollGroupID int IDENTITY (1, 1) NOT NULL,
		UserID	    int not NULL,
		[Flags]     int NOT NULL constraint [DF_{objectQualifier}PollGroupCluster_Flags] default (0),
		[IsBound]   AS (CONVERT([bit],sign([Flags]&(2)),(0)))	
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Poll](
		PollID			int IDENTITY (1, 1) NOT NULL ,
		Question		nvarchar (50) NOT NULL,
		Closes datetime NULL,		
		PollGroupID int NULL,
		UserID int not NULL,	
		[ObjectPath] nvarchar(255) NULL,
		[MimeType] varchar(50) NULL,
		[Flags] int NOT NULL constraint [DF_{objectQualifier}Poll_Flags] default (0),		
		[IsClosedBound]	AS (CONVERT([bit],sign([Flags]&(4)),(0))),
		[AllowMultipleChoices] AS (CONVERT([bit],sign([Flags]&(8)),(0))),
		[ShowVoters] AS (CONVERT([bit],sign([Flags]&(16)),(0))),
		[AllowSkipVote] AS (CONVERT([bit],sign([Flags]&(32)),(0)))
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Smiley]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Smiley](
		SmileyID		int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL ,
		Code			nvarchar (10) NOT NULL ,
		Icon			nvarchar (50) NOT NULL ,
		Emoticon		nvarchar (50) NULL ,
		SortOrder		tinyint	NOT NULL DEFAULT 0
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Topic](
		TopicID			    int IDENTITY (1, 1) NOT NULL ,
		ForumID			    int NOT NULL ,
		UserID			    int NOT NULL ,
		UserName		    nvarchar (255) NULL ,
		UserDisplayName		nvarchar (255) NULL ,	
		Posted			    datetime NOT NULL ,
		Topic			    nvarchar (100) NOT NULL ,
		[Description]		nvarchar (255) NULL ,
		[Status]	     	nvarchar (255) NULL ,
		[Styles]	     	nvarchar (255) NULL ,
		[Views]			    int NOT NULL ,
		[Priority]		    smallint NOT NULL ,
		PollID			    int NULL ,
		TopicMovedID	    int NULL ,
		LastPosted		    datetime NULL ,
		LastMessageID	    int NULL ,
		LastUserID		    int NULL ,
		LastUserName	    nvarchar (255) NULL,
		LastUserDisplayName	nvarchar (255) NULL,	
		NumPosts		    int NOT NULL,
		Flags			    int not null constraint [DF_{objectQualifier}Topic_Flags] default (0),
		IsDeleted		    AS (CONVERT([bit],sign([Flags]&(8)),0)),
		[IsQuestion]        AS (CONVERT([bit],sign([Flags]&(1024)),(0))),
		[AnswerMessageId]   [int] NULL,
		[LastMessageFlags]	[int] NULL,
		[TopicImage]        nvarchar(255) NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}User](
		UserID			int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL,
		ProviderUserKey	nvarchar(64),
		[Name]			nvarchar (255) NOT NULL,
		[DisplayName]	nvarchar (255) NOT NULL,
		[Password]		nvarchar (32) NOT NULL,
		[Email]			nvarchar (255) NULL,
		Joined			datetime NOT NULL,
		LastVisit		datetime NOT NULL,
		IP				nvarchar (15) NULL,
		NumPosts		int NOT NULL,
		TimeZone		int NOT NULL,
		Avatar			nvarchar (255) NULL,
		[Signature]		ntext NULL,
		AvatarImage		varbinary(max) NULL,
		AvatarImageType	nvarchar (50) NULL,
		RankID			[int] NOT NULL,
		Suspended		[datetime] NULL,
		LanguageFile	nvarchar(50) NULL,
		ThemeFile		nvarchar(50) NULL,
		[UseSingleSignOn][bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_UseSingleSignOn] DEFAULT (0),
		TextEditor		nvarchar(50) NULL,
		OverrideDefaultThemes	bit NOT NULL CONSTRAINT [DF_{objectQualifier}User_OverrideDefaultThemes] DEFAULT (1),
		[PMNotification] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_PMNotification] DEFAULT (1),
		[AutoWatchTopics] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_AutoWatchTopics] DEFAULT (0),
		[DailyDigest] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_DailyDigest] DEFAULT (0),
		[NotificationType] [int] DEFAULT (10),
		[Flags] [int]	NOT NULL CONSTRAINT [DF_{objectQualifier}User_Flags] DEFAULT (0),
		[Points] [int]	NOT NULL CONSTRAINT [DF_{objectQualifier}User_Points] DEFAULT (0),		
		[IsApproved]	AS (CONVERT([bit],sign([Flags]&(2)),(0))),
		[IsGuest]	AS (CONVERT([bit],sign([Flags]&(4)),(0))),
		[IsCaptchaExcluded]	AS (CONVERT([bit],sign([Flags]&(8)),(0))),
		[IsActiveExcluded] AS (CONVERT([bit],sign([Flags]&(16)),(0))),
		[IsDST]	AS (CONVERT([bit],sign([Flags]&(32)),(0))),
		[IsDirty]	AS (CONVERT([bit],sign([Flags]&(64)),(0))),
		[Culture] varchar (10) DEFAULT (10),
		[IsFacebookUser][bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_IsFacebookUser] DEFAULT (0),
		[IsTwitterUser][bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_IsTwitterUser] DEFAULT (0)
)
GO

IF not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserProfile]') and type in (N'U'))
	CREATE TABLE [{databaseSchema}].[{objectQualifier}UserProfile]
	(
		[UserID] [int] NOT NULL,
		[LastUpdatedDate] [datetime] NOT NULL,
		-- added columns
		[LastActivity] [datetime],
		[ApplicationName] [nvarchar](255) NOT NULL,	
		[IsAnonymous] [bit] NOT NULL,
		[UserName] [nvarchar](255) NOT NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}WatchForum]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}WatchForum](
		WatchForumID	int IDENTITY (1, 1) NOT NULL ,
		ForumID			int NOT NULL ,
		UserID			int NOT NULL ,
		Created			datetime NOT NULL ,
		LastMail		datetime null
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}WatchTopic]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}WatchTopic](
		WatchTopicID	int IDENTITY (1, 1) NOT NULL ,
		TopicID			int NOT NULL ,
		UserID			int NOT NULL ,
		Created			datetime NOT NULL ,
		LastMail		datetime null
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Attachment]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Attachment](
		AttachmentID	int IDENTITY (1, 1) not null,
		MessageID		int not null,		
		[FileName]		nvarchar(255) not null,
		Bytes			int not null,
		FileID			int null,
		ContentType		nvarchar(50) null,
		Downloads		int not null,
		FileData		varbinary(max) null
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserGroup]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}UserGroup](
		UserID			int NOT NULL,
		GroupID			int NOT NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}Rank](
		RankID			int IDENTITY (1, 1) NOT NULL,
		BoardID			int NOT NULL ,
		Name			nvarchar (50) NOT NULL,
		MinPosts		int NULL,
		RankImage		nvarchar (50) NULL,
		Flags			int not null constraint [DF_{objectQualifier}Rank_Flags] default (0)
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}AccessMask](
		AccessMaskID	int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL ,
		Name			nvarchar(50) NOT NULL ,
		Flags			int not null constraint [DF_{objectQualifier}AccessMask_Flags] default (0)
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMaskHistory]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}AccessMaskHistory](
		AccessMaskID		int NOT NULL,
		ChangedUserID		int,	
		ChangedUserName		nvarchar (255) NOT NULL,
		ChangedDisplayName	nvarchar (255) NOT NULL,
		ChangedDate         datetime  NOT NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserForum]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}UserForum](
		UserID			int NOT NULL ,
		ForumID			int NOT NULL ,
		AccessMaskID	int NOT NULL ,
		Invited			datetime NOT NULL,
		Accepted		bit NOT NULL
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Board]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}Board](
		BoardID			int IDENTITY (1, 1) NOT NULL,
		Name			nvarchar(50) NOT NULL,
		AllowThreaded	bit NOT NULL,
		MembershipAppName nvarchar(255) NULL,
		RolesAppName nvarchar(255) NULL
	)
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}NntpServer]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}NntpServer](
		NntpServerID	int IDENTITY (1, 1) not null,
		BoardID			int NOT NULL ,
		Name			nvarchar(50) not null,
		[Address]			nvarchar(100) not null,
		Port			int null,
		UserName		nvarchar(255) null,
		UserPass		nvarchar(50) null
		
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}NntpForum]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}NntpForum](
		NntpForumID		int IDENTITY (1, 1) not null,
		NntpServerID	int not null,
		GroupName		nvarchar(100) not null,
		ForumID			int not null,
		LastMessageNo	int not null,
		LastUpdate		datetime not null,
		Active			bit not null,
		DateCutOff		datetime null
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}NntpTopic]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}NntpTopic](
		NntpTopicID		int IDENTITY (1, 1) not null,
		NntpForumID		int not null,
		Thread			varchar(64) not null,
		TopicID			int not null
	)
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserPMessage]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}UserPMessage](
		UserPMessageID	int IDENTITY (1, 1) not null,
		UserID			int not null,
		PMessageID		int not null,
		[Flags]			int NOT NULL DEFAULT ((0)),
		[IsRead]		AS (CONVERT([bit],sign([Flags]&(1)),(0))),
		[IsInOutbox]	AS (CONVERT([bit],sign([Flags]&(2)),(0))),
		[IsArchived]	AS (CONVERT([bit],sign([Flags]&(4)),(0))),
		[IsDeleted]		AS (CONVERT([bit],sign([Flags]&(8)),(0))),
		[IsReply]		[bit] NOT NULL  DEFAULT (0)		
	)
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Replace_Words]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}Replace_Words](
		ID				int IDENTITY (1, 1) NOT NULL,
		BoardId			int NOT NULL,
		BadWord			nvarchar (255) NULL ,
		GoodWord		nvarchar (255) NULL ,
		constraint [PK_{objectQualifier}Replace_Words] primary key(ID)
	)
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Registry]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}Registry](
		RegistryID		int IDENTITY(1, 1) NOT NULL,
		Name			nvarchar(50) NOT NULL,
		Value			ntext,
		BoardID			int,
		CONSTRAINT [PK_{objectQualifier}Registry] PRIMARY KEY (RegistryID)
	)
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}EventLog]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}EventLog](
		EventLogID	int identity(1,1) not null,
		EventTime	datetime not null constraint [DF_{objectQualifier}EventLog_EventTime] default GETUTCDATE() ,
		UserID		int,
		[Source]	nvarchar(50) not null,
		Description	ntext not null,
		constraint [PK_{objectQualifier}EventLog] primary key(EventLogID)
	)
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Extension]') and type in (N'U'))
BEGIN
	CREATE TABLE [{databaseSchema}].[{objectQualifier}Extension](
		ExtensionID int IDENTITY(1,1) NOT NULL,
		BoardId int NOT NULL,
		Extension nvarchar(10) NOT NULL,
		CONSTRAINT [PK_{objectQualifier}Extension] PRIMARY KEY(ExtensionID)
	)
END
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}BBCode]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}BBCode](
		[BBCodeID] [int] IDENTITY(1,1) NOT NULL,
		[BoardID] [int] NOT NULL,
		[Name] [nvarchar](255) NOT NULL,
		[Description] [nvarchar](4000) NULL,
		[OnClickJS] [nvarchar](1000) NULL,
		[DisplayJS] [ntext] NULL,
		[EditJS] [ntext] NULL,
		[DisplayCSS] [ntext] NULL,
		[SearchRegex] [ntext] NULL,
		[ReplaceRegex] [ntext] NULL,
		[Variables] [nvarchar](1000) NULL,
		[UseModule] [bit] NULL,
		[ModuleClass] [nvarchar](255) NULL,		
		[ExecOrder] [int] NOT NULL,
		CONSTRAINT [PK_{objectQualifier}BBCode] PRIMARY KEY (BBCodeID)
	)
end
GO


if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Medal]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}Medal](
		[BoardID] [int] NOT NULL,
		[MedalID] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](100) NOT NULL,
		[Description] [ntext] NOT NULL,
		[Message] [nvarchar](100) NOT NULL,
		[Category] [nvarchar](50) NULL,
		[MedalURL] [nvarchar](250) NOT NULL,
		[RibbonURL] [nvarchar](250) NULL,
		[SmallMedalURL] [nvarchar](250) NOT NULL,
		[SmallRibbonURL] [nvarchar](250) NULL,
		[SmallMedalWidth] [smallint] NOT NULL,
		[SmallMedalHeight] [smallint] NOT NULL,
		[SmallRibbonWidth] [smallint] NULL,
		[SmallRibbonHeight] [smallint] NULL,
		[SortOrder] [tinyint] NOT NULL CONSTRAINT [DF_{objectQualifier}Medal_DefaultOrder]  DEFAULT ((255)),
		[Flags] [int] NOT NULL CONSTRAINT [DF_{objectQualifier}Medal_Flags]  DEFAULT ((0)),
		CONSTRAINT [PK_{objectQualifier}Medal] PRIMARY KEY CLUSTERED ([MedalID] ASC)
		)
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}GroupMedal]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}GroupMedal](
		[GroupID] [int] NOT NULL,
		[MedalID] [int] NOT NULL,
		[Message] [nvarchar](100) NULL,
		[Hide] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}GroupMedal_Hide]  DEFAULT ((0)),
		[OnlyRibbon] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}GroupMedal_OnlyRibbon]  DEFAULT ((0)),
		[SortOrder] [tinyint] NOT NULL CONSTRAINT [DF_{objectQualifier}GroupMedal_SortOrder]  DEFAULT ((255))
		)
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserMedal]') and type in (N'U'))
begin
	create table [{databaseSchema}].[{objectQualifier}UserMedal](
		[UserID] [int] NOT NULL,
		[MedalID] [int] NOT NULL,
		[Message] [nvarchar](100) NULL,
		[Hide] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}UserMedal_Hide]  DEFAULT ((0)),
		[OnlyRibbon] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}UserMedal_OnlyRibbon]  DEFAULT ((0)),
		[SortOrder] [tinyint] NOT NULL CONSTRAINT [DF_{objectQualifier}UserMedal_SortOrder]  DEFAULT ((255)),
		[DateAwarded] [datetime] NOT NULL
	)
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}IgnoreUser]') and type in (N'U'))
begin
	CREATE TABLE [{databaseSchema}].[{objectQualifier}IgnoreUser]
	(
		[UserID] int NOT NULL,
		[IgnoredUserID] int NOT NULL
	)
end
GO


if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ShoutboxMessage]') and type in (N'U'))
begin
	CREATE TABLE [{databaseSchema}].[{objectQualifier}ShoutboxMessage](
		[ShoutBoxMessageID] [int] IDENTITY(1,1) NOT NULL,		
		[BoardId] [int] NOT NULL,
		[UserID] [int] NULL,
		[UserName] [nvarchar](255) NOT NULL,
		[UserDisplayName] [nvarchar](255) NOT NULL,
		[Message] [ntext] NULL,
		[Date] [datetime] NOT NULL,
		[IP] [varchar](50) NOT NULL
	)
end
GO	

exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}Board, BoardUID')
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Board]') and name=N'BoardUID')
begin
alter table [{databaseSchema}].[{objectQualifier}Board] drop column  BoardUID
end
GO

-- Mail Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Mail]') and name=N'FromUserName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Mail] add [FromUserName] [nvarchar](255) NULL
	alter table [{databaseSchema}].[{objectQualifier}Mail] add [ToUserName] [nvarchar](255) NULL
	alter table [{databaseSchema}].[{objectQualifier}Mail] add [BodyHtml] [ntext] NULL		
	alter table [{databaseSchema}].[{objectQualifier}Mail] add [SendTries] [int] NOT NULL CONSTRAINT [DF_{objectQualifier}Mail_SendTries]  DEFAULT ((0))		
	alter table [{databaseSchema}].[{objectQualifier}Mail] add [SendAttempt] [datetime] NULL
	alter table [{databaseSchema}].[{objectQualifier}Mail] add [ProcessID] [int] NULL	
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Mail]') and name=N'FromUserName' and precision < 255)
begin
alter table [{databaseSchema}].[{objectQualifier}Mail] alter column [FromUserName] [nvarchar](255) NULL
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Mail]') and name=N'FromUser' and precision < 255)
begin
alter table [{databaseSchema}].[{objectQualifier}Mail] alter column [FromUser] [nvarchar](255) NULL
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Mail]') and name=N'ToUserName' and precision < 255)
begin
alter table [{databaseSchema}].[{objectQualifier}Mail] alter column [ToUserName] [nvarchar](255) NULL
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Mail]') and name=N'ToUser' and precision < 255)
begin
alter table [{databaseSchema}].[{objectQualifier}Mail] alter column [ToUser] [nvarchar](255) NULL
end
GO

-- Active Table
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Active]') and name=N'Location' and precision < 255)
	alter table [{databaseSchema}].[{objectQualifier}Active] alter column [Location] nvarchar(255) NOT NULL
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Active]') and name=N'ForumPage')
begin
	alter table [{databaseSchema}].[{objectQualifier}Active] add [ForumPage] nvarchar(255)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Active]') and name=N'ForumPage' and precision < 255)
	alter table [{databaseSchema}].[{objectQualifier}Active] alter column [ForumPage] nvarchar(255) 
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Active]') and name=N'IP' and precision < 39)
	alter table [{databaseSchema}].[{objectQualifier}Active] alter column [IP] varchar(39) not null 
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Active]') and name=N'Flags')
	alter table [{databaseSchema}].[{objectQualifier}Active] add [Flags] int NULL 
GO

if exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Active]') and type in (N'U'))    
	grant delete on [{databaseSchema}].[{objectQualifier}Active] to public
	exec('delete from [{databaseSchema}].[{objectQualifier}Active]')
	revoke delete on [{databaseSchema}].[{objectQualifier}Active] from public
GO

-- Board Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Board]') and name=N'MembershipAppName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Board] add MembershipAppName nvarchar(255)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Board]') and name=N'RolesAppName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Board] add RolesAppName nvarchar(255)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Board]') and name=N'MembershipAppName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Board] add MembershipAppName nvarchar(255)
end
GO

-- UserPMessage Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserPMessage]') and name=N'Flags')
begin
	-- add new "Flags" field to UserPMessage
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] add Flags int not null  CONSTRAINT [DF_{objectQualifier}UserPMessage_Flags] DEFAULT (0)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserPMessage]') and name=N'IsRead')
BEGIN
	if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserPMessage]') and name=N'IsArchived')
	BEGIN	
		-- Copy "IsRead" value over
		grant update on [{databaseSchema}].[{objectQualifier}UserPMessage] to public
		exec('update [{databaseSchema}].[{objectQualifier}UserPMessage] set Flags = IsRead')
		revoke update on [{databaseSchema}].[{objectQualifier}UserPMessage] from public
		
		-- drop the old column
		alter table [{databaseSchema}].[{objectQualifier}UserPMessage] drop column IsRead
		
		-- Verify flags isn't NULL
		grant update on [{databaseSchema}].[{objectQualifier}UserPMessage] to public
		exec('update [{databaseSchema}].[{objectQualifier}UserPMessage] set Flags = 1 WHERE Flags IS NULL')
		revoke update on [{databaseSchema}].[{objectQualifier}UserPMessage] from public
		
		-- add new calculated columns	
		alter table [{databaseSchema}].[{objectQualifier}UserPMessage] ADD [IsRead] AS (CONVERT([bit],sign([Flags]&(1)),(0)))
		alter table [{databaseSchema}].[{objectQualifier}UserPMessage] ADD [IsInOutbox] AS (CONVERT([bit],sign([Flags]&(2)),(0)))
		alter table [{databaseSchema}].[{objectQualifier}UserPMessage] ADD [IsArchived] AS (CONVERT([bit],sign([Flags]&(4)),(0)))
	END
END
GO

IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserPMessage]') AND name=N'IsDeleted')
BEGIN
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] ADD [IsDeleted] AS (CONVERT([bit],sign([Flags]&(8)),(0)))
END
GO

-- User Table
if exists(select top 1 1 from [{databaseSchema}].[{objectQualifier}Group] where ([Flags] & 2)=2)
begin
  update [{databaseSchema}].[{objectQualifier}User] set [Flags] = [Flags] | 4 where UserID in (select distinct UserID from [{databaseSchema}].[{objectQualifier}UserGroup] a join [{databaseSchema}].[{objectQualifier}Group] b on b.GroupID=a.GroupID and (b.[Flags] & 2)=2)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsApproved')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] ADD [IsApproved] AS (CONVERT([bit],sign([Flags]&(2)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'NotificationType')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] ADD NotificationType int default(10)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'NotificationType')
begin
	update  [{databaseSchema}].[{objectQualifier}User] SET [NotificationType]=10 WHERE [NotificationType] IS NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsActiveExcluded')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] ADD [IsActiveExcluded] AS (CONVERT([bit],sign([Flags]&(16)),(0)))
end
GO

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Signature' and system_type_id <> 99)
	alter table [{databaseSchema}].[{objectQualifier}User] alter column Signature ntext null
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Flags')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [Flags] int not null constraint [DF_{objectQualifier}User_Flags] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'IsQuestion')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add IsQuestion AS (CONVERT([bit],sign([Flags]&(1024)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'TextEditor')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add TextEditor nvarchar(50) NULL
end
GO
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'UseSingleSignOn')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [UseSingleSignOn][bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_UseSingleSignOn] DEFAULT (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'AnswerMessageId')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add AnswerMessageId INT NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'TopicImage')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add TopicImage nvarchar(255) NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'TopicImageType')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add TopicImageType nvarchar(50) NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'TopicImageBin')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add TopicImageBin varbinary(max) NULL
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsHostAdmin')
begin
	grant update on [{databaseSchema}].[{objectQualifier}User] to public
	exec('update [{databaseSchema}].[{objectQualifier}User] set Flags = Flags | 1 where IsHostAdmin<>0')
	revoke update on [{databaseSchema}].[{objectQualifier}User] from public
	alter table [{databaseSchema}].[{objectQualifier}User] drop column IsHostAdmin
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVoteRefuse]') and name=N'BoardID')
begin
alter table [{databaseSchema}].[{objectQualifier}PollVoteRefuse] drop column [BoardID] 
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Approved')
begin
	grant update on [{databaseSchema}].[{objectQualifier}User] to public
	exec('update [{databaseSchema}].[{objectQualifier}User] set Flags = Flags | 2 where Approved<>0')
	revoke update on [{databaseSchema}].[{objectQualifier}User] from public
	alter table [{databaseSchema}].[{objectQualifier}User] drop column Approved
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'ProviderUserKey')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add ProviderUserKey nvarchar(64)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'DisplayName')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] ADD [DisplayName] nvarchar(255) 
	grant update on [{databaseSchema}].[{objectQualifier}User] to public
	exec('update [{databaseSchema}].[{objectQualifier}User] SET DisplayName = [Name]')
	revoke update on [{databaseSchema}].[{objectQualifier}User] from public	
	ALTER TABLE [{databaseSchema}].[{objectQualifier}User] ALTER COLUMN [DisplayName] nvarchar(255) NOT NULL
end
GO

-- convert uniqueidentifier to nvarchar(64)
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'ProviderUserKey' and system_type_id = 36)
begin
	-- drop the provider user key index if it exists...
	if exists(select 1 from sys.indexes where name=N'IX_{objectQualifier}User_ProviderUserKey' and object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]'))
	begin
		DROP INDEX [{databaseSchema}].[{objectQualifier}User].[IX_{objectQualifier}User_ProviderUserKey]
	end
	-- alter the column
	ALTER TABLE [{databaseSchema}].[{objectQualifier}User] ALTER COLUMN ProviderUserKey nvarchar(64)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'PMNotification')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [PMNotification] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_PMNotification] DEFAULT (1)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'DailyDigest')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [DailyDigest] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_DailyDigest] DEFAULT (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'AutoWatchTopics')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [AutoWatchTopics] [bit] NOT NULL CONSTRAINT [DF_{objectQualifier}User_AutoWatchTopics] DEFAULT (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'OverrideDefaultThemes')
begin
alter table [{databaseSchema}].[{objectQualifier}User] add  [OverrideDefaultThemes]	bit NOT NULL CONSTRAINT [DF_{objectQualifier}User_OverrideDefaultThemes] DEFAULT (1)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'OverrideDefaultThemes')
begin
	update  [{databaseSchema}].[{objectQualifier}User] SET [OverrideDefaultThemes]=1 WHERE [OverrideDefaultThemes] = 0
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Points')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [Points] [int] NOT NULL CONSTRAINT [DF_{objectQualifier}User_Points] DEFAULT (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'AvatarImageType')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [AvatarImageType] nvarchar(50) NULL
end
GO

-- make sure the gender column is nullable
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Gender')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] alter column Gender tinyint NULL
end
GO

-- Add 8-letter Language Code column
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Culture')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add Culture varchar(10) NULL
end
GO

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IP' and precision < 39)
	alter table [{databaseSchema}].[{objectQualifier}User] alter column [IP] varchar(39) null
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Name' and precision < 255)
begin
	alter table [{databaseSchema}].[{objectQualifier}User] alter column [Name] nvarchar(255) not null
end
GO

-- Only remove User table columns if version is 30+
IF EXISTS (SELECT ver FROM (SELECT CAST(CAST(value as nvarchar(255)) as int) as ver FROM [{databaseSchema}].[{objectQualifier}Registry] WHERE name = 'version') reg WHERE ver > 30)
BEGIN
	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Gender')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column Gender
	end

	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Location')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column Location
	end

	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'HomePage')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column HomePage
	end

	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'MSN')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column MSN
	end

	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'YIM')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column YIM
	end

	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'AIM')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column AIM
	end

	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'ICQ')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column ICQ
	end

	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'RealName')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column RealName
	end

	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Occupation')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column Occupation
	end
	
	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Interests')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column Interests
	end
	
	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Weblog')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column Weblog
	end
	
	if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'WeblogUrl')
	begin
		alter table [{databaseSchema}].[{objectQualifier}User] drop column WeblogUrl
		alter table [{databaseSchema}].[{objectQualifier}User] drop column WeblogUsername
		alter table [{databaseSchema}].[{objectQualifier}User] drop column WeblogID
	end
	
END
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'TopicsPerPage')
    begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [TopicsPerPage] int NOT NULL CONSTRAINT [DF_{objectQualifier}User_TopicsPerPage] DEFAULT (20)
    end
GO
	if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'PostsPerPage')
    begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [PostsPerPage] int NOT NULL CONSTRAINT [DF_{objectQualifier}User_PostsPerPage] DEFAULT (20)
    end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsGuest')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsGuest] AS (CONVERT([bit],sign([Flags]&(4)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsCaptchaExcluded')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsCaptchaExcluded] AS (CONVERT([bit],sign([Flags]&(8)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsDST')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsDST] AS (CONVERT([bit],sign([Flags]&(32)),(0)))
end
GO
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsDirty')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsDirty] AS (CONVERT([bit],sign([Flags]&(64)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsFacebookUser')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsFacebookUser][bit] NOT NULL CONSTRAINT [DF_{objectQualifier}IsFacebookUser] DEFAULT (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsTwitterUser')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsTwitterUser][bit] NOT NULL CONSTRAINT [DF_{objectQualifier}IsTwitterUser] DEFAULT (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'CommonViewType')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [CommonViewType] int NOT NULL CONSTRAINT [DF_{objectQualifier}CommonViewType] DEFAULT (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'UserStyle')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [UserStyle] varchar(510) 		
end
GO	

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'StyleFlags')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [StyleFlags] [int] NOT NULL CONSTRAINT [DF_{objectQualifier}User_StyleFlags] DEFAULT (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsUserStyle')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsUserStyle] AS (CONVERT([bit],sign([StyleFlags]&(1)),(0)))
end
GO	

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsGroupStyle')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsGroupStyle] AS (CONVERT([bit],sign([StyleFlags]&(2)),(0)))
end
GO	

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'IsRankStyle')
begin
	alter table [{databaseSchema}].[{objectQualifier}User] add [IsRankStyle] AS (CONVERT([bit],sign([StyleFlags]&(4)),(0)))
end
GO	

-- Forum Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'RemoteURL')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add RemoteURL nvarchar(100) null
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'Flags')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add Flags int not null constraint [DF_{objectQualifier}Forum_Flags] default (0)
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'ThemeURL')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add ThemeURL nvarchar(50) NULL
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'Locked')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Forum] set Flags = Flags | 1 where Locked<>0')
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop column Locked
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'Hidden')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Forum] set Flags = Flags | 2 where Hidden<>0')
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop column Hidden
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IsTest')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Forum] set Flags = Flags | 4 where IsTest<>0')
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop column IsTest
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'Moderated')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Forum] set Flags = Flags | 8 where Moderated<>0')
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop column Moderated
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'ImageURL')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add ImageURL nvarchar(128) NULL
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'Styles')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add Styles nvarchar(255) NULL
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'LastUserName' and precision < 255)
	alter table [{databaseSchema}].[{objectQualifier}Forum] alter column [LastUserName]	nvarchar (255) NULL 
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'LastUserDisplayName')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add [LastUserDisplayName]	nvarchar (255) NULL
	
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'PollGroupID')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add PollGroupID int NULL
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IsHidden')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] ADD [IsHidden] AS (CONVERT([bit],sign([Flags]&(2)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IsLocked')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] ADD [IsLocked] AS (CONVERT([bit],sign([Flags]&(1)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IsNoCount')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] ADD [IsNoCount] AS (CONVERT([bit],sign([Flags]&(4)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IsModerated')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] ADD [IsModerated] AS (CONVERT([bit],sign([Flags]&(8)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'IsUserForum')
	alter table [{databaseSchema}].[{objectQualifier}Forum] add IsUserForum bit NOT NULL constraint [DF_{objectQualifier}Forum_IsUserForum] default (0)
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'UserID')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] drop column [UserID]
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'left_key')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add [left_key] int NULL
end
GO
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'right_key')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add [right_key] int NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'level')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add [level] int NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'trigger_for_delete')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add trigger_for_delete int not null constraint [DF_{objectQualifier}Forum_TriggerDel] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'trigger_lock_update')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add trigger_lock_update int not null constraint [DF_{objectQualifier}Forum_TriggerUpd] default (0)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'Description')
begin
	ALTER TABLE [{databaseSchema}].[{objectQualifier}Forum]
ALTER COLUMN [Description] NVARCHAR(255) NULL
end
GO

IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') AND name = N'CanHavePersForums')
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}Forum] ADD [CanHavePersForums] [bit] NOT NULL constraint [DF_{objectQualifier}Forum_CanHavePersForums] default (0)
END
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'CreatedByUserID')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add CreatedByUserID int null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'CreatedByUserName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add CreatedByUserName  nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'CreatedByUserDisplayName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add CreatedByUserDisplayName  nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'CreatedDate')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] add CreatedDate  datetime null
end
GO

-- Group Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'Flags')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add Flags int not null constraint [DF_{objectQualifier}Group_Flags] default (0)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'Name' and precision < 255)
begin
if exists (select top 1 1 from sys.indexes where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'IX_{objectQualifier}Group')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] drop constraint [IX_{objectQualifier}Group] 
end
	alter table [{databaseSchema}].[{objectQualifier}Group] alter column [Name] nvarchar(255) NOT NULL
end
GO


if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'IsAdmin')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Group] set Flags = Flags | 1 where IsAdmin<>0')
	alter table [{databaseSchema}].[{objectQualifier}Group] drop column IsAdmin
end
GO


if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'IsGuest')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Group] set Flags = Flags | 2 where IsGuest<>0')
	alter table [{databaseSchema}].[{objectQualifier}Group] drop column IsGuest
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'IsStart')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Group] set Flags = Flags | 4 where IsStart<>0')
	alter table [{databaseSchema}].[{objectQualifier}Group] drop column IsStart
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'IsModerator')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Group] set Flags = Flags | 8 where IsModerator<>0')
	alter table [{databaseSchema}].[{objectQualifier}Group] drop column IsModerator
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'PMLimit')
begin

		alter table [{databaseSchema}].[{objectQualifier}Group] add PMLimit int not null	constraint [DF_{objectQualifier}Group_PMLimit] default (0)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'PMLimit' and is_nullable=1)
begin		
		grant update on [{databaseSchema}].[{objectQualifier}Group] to public
		exec('update [{databaseSchema}].[{objectQualifier}Group] set PMLimit = 30 WHERE PMLimit IS NULL')
		alter table [{databaseSchema}].[{objectQualifier}Group] alter column [PMLimit] integer NOT NULL
		revoke update on [{databaseSchema}].[{objectQualifier}Group] from public	    
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'Style')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add Style nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'SortOrder')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add SortOrder smallint not null constraint [DF_{objectQualifier}Group_SortOrder] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'Description')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add Description nvarchar(128) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrSigChars')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add UsrSigChars int not null constraint [DF_{objectQualifier}Group_UsrSigChars] default (0)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrSigChars')
begin
grant update on [{databaseSchema}].[{objectQualifier}Group] to public
		exec('update [{databaseSchema}].[{objectQualifier}Group] set UsrSigChars = 128 WHERE UsrSigChars = 0 AND Name != ''Guest'' ')
		revoke update on [{databaseSchema}].[{objectQualifier}Group] from public	
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrSigBBCodes')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add UsrSigBBCodes nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrSigHTMLTags')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add UsrSigHTMLTags nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrAlbums')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add UsrAlbums int not null constraint [DF_{objectQualifier}Group_UsrAlbums] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrAlbumImages')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add UsrAlbumImages int not null constraint [DF_{objectQualifier}Group_UsrAlbumImages] default (0)
end
GO

if exists (SELECT top 1 o.name,s.name FROM  sys.columns s INNER JOIN
						 sys.objects o ON o.object_id = s.object_id
WHERE        (s.is_computed = 1) AND (o.type = 'U') and (s.object_id=object_id(N'[{databaseSchema}].[{objectQualifier}Group]')) AND (s.name ='IsHidden'))
begin
alter table [{databaseSchema}].[{objectQualifier}Group] drop column [IsHidden]
end

if exists (SELECT top 1 o.name,s.name FROM  sys.columns s INNER JOIN
						 sys.objects o ON o.object_id = s.object_id
WHERE        (s.is_computed = 1) AND (o.type = 'U') and (s.object_id=object_id(N'[{databaseSchema}].[{objectQualifier}Group]')) AND (s.name ='IsUserGroup'))
begin
alter table [{databaseSchema}].[{objectQualifier}Group] drop column [IsUserGroup]
end

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrPersonalForums')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add UsrPersonalForums int not null constraint [DF_{objectQualifier}Group_UsrPersonalForums] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrPersonalMasks')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add UsrPersonalMasks int not null constraint [DF_{objectQualifier}Group_UsrPersonalMasks] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'UsrPersonalGroups')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add UsrPersonalGroups int not null constraint [DF_{objectQualifier}Group_UsrPersonalGroups] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'CreatedByUserID')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add CreatedByUserID int null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'CreatedByUserName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add CreatedByUserName  nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'CreatedByUserDisplayName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add CreatedByUserDisplayName  nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'CreatedDate')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add CreatedDate  datetime null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'IsUserGroup')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add IsUserGroup  bit not null constraint [DF_{objectQualifier}Group_IsUserGroup] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'IsHidden')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] add IsHidden  bit not null constraint [DF_{objectQualifier}Group_IsHidden] default (0)
end
GO

-- AccessMask Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'Flags')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add Flags int not null constraint [DF_{objectQualifier}AccessMask_Flags] default (0)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'ReadAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 1 where ReadAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column ReadAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'PostAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 2 where PostAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column PostAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'ReplyAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 4 where ReplyAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column ReplyAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'PriorityAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 8 where PriorityAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column PriorityAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'PollAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 16 where PollAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column PollAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'VoteAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 32 where VoteAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column VoteAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'ModeratorAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 64 where ModeratorAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column ModeratorAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'EditAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 128 where EditAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column EditAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'DeleteAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 256 where DeleteAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column DeleteAccess
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'UploadAccess')
begin
	exec('update [{databaseSchema}].[{objectQualifier}AccessMask] set Flags = Flags | 512 where UploadAccess<>0')
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] drop column UploadAccess
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'SortOrder')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add SortOrder smallint not null default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'CreatedByUserID')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add CreatedByUserID int null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'CreatedByUserName')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add CreatedByUserName  nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'CreatedByUserDisplayName')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add CreatedByUserDisplayName  nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'CreatedDate')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add CreatedDate  datetime null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'IsUserMask')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add IsUserMask  bit not null constraint [DF_{objectQualifier}AccessMask_IsUserMask] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'IsAdminMask')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] add IsAdminMask  bit not null constraint [DF_{objectQualifier}AccessMask_IsAdminMask] default (0)
end
GO

-- NntpForum Table

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}NntpForum]') and name=N'Active')
begin
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] add Active bit null
	exec('update [{databaseSchema}].[{objectQualifier}NntpForum] set Active=1 where Active is null')
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] alter column Active bit not null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}NntpForum]') and name=N'DateCutOff')
	alter table [{databaseSchema}].[{objectQualifier}NntpForum] ADD	DateCutOff datetime NULL
GO

-- NntpTopic Table

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntptopic]') and name=N'Thread' and precision < 64)
	alter table [{databaseSchema}].[{objectQualifier}nntptopic] alter column [Thread]	nvarchar (64) NULL 
GO

-- ReplaceWords Table
if exists (select * from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Replace_Words]') and name=N'badword' and precision < 255)
	alter table [{databaseSchema}].[{objectQualifier}Replace_Words] alter column badword nvarchar(255) NULL
GO

if exists (select * from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Replace_Words]') and name=N'goodword' and precision < 255)
	alter table [{databaseSchema}].[{objectQualifier}Replace_Words] alter column goodword nvarchar(255) NULL
GO	

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Replace_Words]') and name=N'BoardID')
begin
	alter table [{databaseSchema}].[{objectQualifier}Replace_Words] add BoardID int not null constraint [DF_{objectQualifier}Replace_Words_BoardID] default (1)
end
GO

-- ShoutboxMessage Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ShoutboxMessage]') and name=N'BoardID')
begin
	alter table [{databaseSchema}].[{objectQualifier}ShoutboxMessage] add BoardID int not null constraint [DF_{objectQualifier}ShoutboxMessage_BoardID] default (1)
end
GO
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ShoutboxMessage]') and name=N'UserDisplayName')
begin	
	alter table [{databaseSchema}].[{objectQualifier}ShoutboxMessage] add UserDisplayName nvarchar (255) null
	-- alter table [{databaseSchema}].[{objectQualifier}ShoutboxMessage] alter column UserDisplayName nvarchar (255) not null
end
GO

-- BBCode Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}BBCode]') and name=N'UseModule')
begin
	alter table [{databaseSchema}].[{objectQualifier}BBCode] add UseModule bit null
	alter table [{databaseSchema}].[{objectQualifier}BBCode] add ModuleClass nvarchar(255) null
end
GO

-- Registry Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Registry]') and name=N'BoardID')
	alter table [{databaseSchema}].[{objectQualifier}Registry] add BoardID int
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Registry]') and name=N'Value' and system_type_id <> 99)
	alter table [{databaseSchema}].[{objectQualifier}Registry] alter column Value ntext null
GO

-- PMessage Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PMessage]') and name=N'Flags')
begin
	alter table [{databaseSchema}].[{objectQualifier}PMessage] add Flags int not null constraint [DF_{objectQualifier}Message_Flags] default (23)
end
GO

-- Message Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'Flags')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add Flags int not null constraint [DF_{objectQualifier}Topic_Flags] default (0)
	update [{databaseSchema}].[{objectQualifier}Message] set Flags = Flags & 7
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'Approved')
begin
	exec('update [{databaseSchema}].[{objectQualifier}Message] set Flags = Flags | 16 where Approved<>0')
	alter table [{databaseSchema}].[{objectQualifier}Message] drop column Approved
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'BlogPostID')
begin
	alter table [{databaseSchema}].[{objectQualifier}Message] add BlogPostID nvarchar(50)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'IsDeleted')
begin
	alter table [{databaseSchema}].[{objectQualifier}Message] ADD [IsDeleted] AS (CONVERT([bit],sign([Flags]&(8)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'UserDisplayName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Message] add UserDisplayName nvarchar(255) 

end
GO



if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'IsApproved')
begin
	alter table [{databaseSchema}].[{objectQualifier}Message] ADD [IsApproved] AS (CONVERT([bit],sign([Flags]&(16)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'EditReason')
	alter table [{databaseSchema}].[{objectQualifier}Message] add EditReason nvarchar (100) NULL
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'IsModeratorChanged')
	alter table [{databaseSchema}].[{objectQualifier}Message] add 	IsModeratorChanged  bit NOT NULL CONSTRAINT [DF_{objectQualifier}Message_IsModeratorChanged] DEFAULT (0)
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'DeleteReason')
	alter table [{databaseSchema}].[{objectQualifier}Message] add DeleteReason  nvarchar (100)  NULL
GO
	
-- an attempt to migrate the legacy report abuse and report spam features flags		
 if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'Flags')
begin
	grant update on [{databaseSchema}].[{objectQualifier}Message] to public	
	exec('update [{databaseSchema}].[{objectQualifier}Message] SET [{databaseSchema}].[{objectQualifier}Message].Flags =  ([{databaseSchema}].[{objectQualifier}Message].Flags  ^ POWER(2, 8) | POWER(2, 7))
		WHERE (([{databaseSchema}].[{objectQualifier}Message].Flags & 256)=256)')
	-- exec('update [{databaseSchema}].[{objectQualifier}Message] SET [{databaseSchema}].[{objectQualifier}Message].Flags =  ([{databaseSchema}].[{objectQualifier}Message].Flags  ^ POWER(2, 9) | POWER(2, 7)
	---	WHERE (([{databaseSchema}].[{objectQualifier}Message].Flags & 512)=512)')			
	revoke update on [{databaseSchema}].[{objectQualifier}Message] from public	
end


if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'EditedBy')
	alter table [{databaseSchema}].[{objectQualifier}Message] add [EditedBy]   int  NULL
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'ExternalMessageId')
	alter table [{databaseSchema}].[{objectQualifier}Message] add [ExternalMessageId]   nvarchar(255) NULL
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'ReferenceMessageId')
	alter table [{databaseSchema}].[{objectQualifier}Message] add [ReferenceMessageId]   nvarchar(255) NULL
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'ExternalMessageId' and precision < 255)
begin
	alter table [{databaseSchema}].[{objectQualifier}Message] alter column [ExternalMessageId] nvarchar (255) NULL
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'IP' and precision < 39)
begin
	alter table [{databaseSchema}].[{objectQualifier}Message] alter column [IP] varchar(39) not null
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'UserName' and precision < 255)
begin
	alter table [{databaseSchema}].[{objectQualifier}Message] alter column [UserName] nvarchar (255) NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'Description')
	alter table [{databaseSchema}].[{objectQualifier}Message] add [Description] nvarchar (4000) NULL
GO

-- MessageHistory Table

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageHistory]') and name=N'MessageHistoryID')
begin
	alter table [{databaseSchema}].[{objectQualifier}MessageHistory] drop column [MessageHistoryID]
end
GO
-- the dependency should be dropped first
if exists (select top 1 1 from  sys.objects where name=N'IX_{objectQualifier}MessageHistory' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}MessageHistory]'))
	alter table [{databaseSchema}].[{objectQualifier}MessageHistory] drop constraint [IX_{objectQualifier}MessageHistory] 
GO
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageHistory]') and name=N'Edited' and is_nullable=1)
begin		
		grant update on [{databaseSchema}].[{objectQualifier}MessageHistory] to public
		-- exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}MessageHistory, Edited')
		exec('update [{databaseSchema}].[{objectQualifier}MessageHistory] set Edited = GETDATE() WHERE Edited IS NULL')
		alter table [{databaseSchema}].[{objectQualifier}MessageHistory] alter column [Edited] datetime NOT NULL
		revoke update on [{databaseSchema}].[{objectQualifier}MessageHistory] from public	    
end
GO

-- Topic Table
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'IsLocked')
begin
	grant update on [{databaseSchema}].[{objectQualifier}Topic] to public
	exec('update [{databaseSchema}].[{objectQualifier}Topic] set Flags = Flags | 1 where IsLocked<>0')
	revoke update on [{databaseSchema}].[{objectQualifier}Topic] from public
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop column IsLocked
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'IsDeleted')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] ADD [IsDeleted] AS (CONVERT([bit],sign([Flags]&(8)),(0)))
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'UserName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] alter column [UserName]	nvarchar (255) NULL 
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'LastUserName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] alter column [LastUserName]	nvarchar (255) NULL	
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'UserDisplayName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add [UserDisplayName]	nvarchar (255) NULL 

end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'LastUserDisplayName')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add [LastUserDisplayName]		nvarchar (255) NULL 
end
GO


if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'LastMessageFlags')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add [LastMessageFlags] int null
	grant update on [{databaseSchema}].[{objectQualifier}Topic] to public
	-- vzrus : we don't migrate flags to not slow down update and possible timeouts. Users can run maintenance scripts? Else use cursors.
	exec('update [{databaseSchema}].[{objectQualifier}Topic] set LastMessageFlags = 22 WHERE LastMessageFlags IS NULL')
	revoke update on [{databaseSchema}].[{objectQualifier}Topic] from public	
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'Description')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add [Description] nvarchar(255) null	
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'LinkDate')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add [LinkDate] datetime null	
end
GO

-- Rank Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'Flags')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add Flags int not null constraint [DF_{objectQualifier}Rank_Flags] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'PMLimit')
begin	
	alter table [{databaseSchema}].[{objectQualifier}Rank] add PMLimit int null 
	grant update on [{databaseSchema}].[{objectQualifier}Rank] to public
	exec('update [{databaseSchema}].[{objectQualifier}Rank] set PMLimit = 0 WHERE PMLimit IS NULL')
	alter table [{databaseSchema}].[{objectQualifier}Rank] alter column [PMLimit] integer NOT NULL
	revoke update on [{databaseSchema}].[{objectQualifier}Rank] from public
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'Style')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add Style nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'SortOrder')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add SortOrder int not null constraint [DF_{objectQualifier}Rank_SortOrder] default (0)
end
GO

exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}Rank, SortOrder')
GO
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'SortOrder')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] alter column [SortOrder] int NOT NULL
end

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'Description')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add [Description] nvarchar(128) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'UsrSigChars')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add UsrSigChars int not null constraint [DF_{objectQualifier}Rank_UsrSigChars] default (0)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'UsrSigChars')
begin
grant update on [{databaseSchema}].[{objectQualifier}Rank] to public
		exec('update [{databaseSchema}].[{objectQualifier}Rank] set UsrSigChars = 128 WHERE UsrSigChars = 0 AND Name != ''Guest'' ')
		revoke update on [{databaseSchema}].[{objectQualifier}Rank] from public	
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'UsrSigBBCodes')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add UsrSigBBCodes nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'UsrSigHTMLTags')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add UsrSigHTMLTags nvarchar(255) null
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'UsrAlbums')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add UsrAlbums int not null constraint [DF_{objectQualifier}Rank_UsrAlbums] default (0)
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'UsrAlbumImages')
begin
	alter table [{databaseSchema}].[{objectQualifier}Rank] add UsrAlbumImages int not null constraint [DF_{objectQualifier}Rank_UsrAlbumImages] default (0)
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'IsStart')
begin
	grant update on [{databaseSchema}].[{objectQualifier}Rank] to public
	exec('update [{databaseSchema}].[{objectQualifier}Rank] set Flags = Flags | 1 where IsStart<>0')
	revoke update on [{databaseSchema}].[{objectQualifier}Rank] from public
	alter table [{databaseSchema}].[{objectQualifier}Rank] drop column IsStart
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Rank]') and name=N'IsLadder')
begin
	grant update on [{databaseSchema}].[{objectQualifier}Rank] to public
	exec('update [{databaseSchema}].[{objectQualifier}Rank] set Flags = Flags | 2 where IsLadder<>0')
	revoke update on [{databaseSchema}].[{objectQualifier}Rank] from public
	alter table [{databaseSchema}].[{objectQualifier}Rank] drop column IsLadder
end
GO

--vzrus: eof migrate to independent multiple polls


-- Poll Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'Closes')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add Closes datetime null
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'Question' and precision < 256 )
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] alter column Question nvarchar(256) NOT NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'PollGroupID')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add PollGroupID int NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'UserID')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add [UserID] int NOT NULL constraint [DF_{objectQualifier}Poll_UserID] default (1)
end
GO

IF  EXISTS (SELECT top 1 1 FROM sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_migration]') and type in (N'P', N'PC'))
begin
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_migration]		
end
GO

-- should drop it else error
if exists(select top 1 1 from sys.objects where name=N'FK_{objectQualifier}Topic_{objectQualifier}Poll' and parent_object_id=object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and type in (N'F'))
	alter table [{databaseSchema}].[{objectQualifier}Topic] drop constraint [FK_{objectQualifier}Topic_{objectQualifier}Poll] 
GO 

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'Flags')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add [Flags] int NULL
end
GO

create procedure [{databaseSchema}].[{objectQualifier}pollgroup_migration]
 as
  begin
	 declare @ptmp int
	 declare @ttmp int
	 declare @utmp int 
	 declare @PollGroupID int

		declare c cursor for
		select  PollID,TopicID, UserID from [{databaseSchema}].[{objectQualifier}Topic] where PollID IS NOT NULL
				
		open c
		
		fetch next from c into @ptmp, @ttmp, @utmp
		while @@FETCH_STATUS = 0
		begin
		if @ptmp is not null
		begin
		insert into [{databaseSchema}].[{objectQualifier}PollGroupCluster](UserID, Flags) values (@utmp, 0)	
		SET @PollGroupID = SCOPE_IDENTITY()  
		
				update [{databaseSchema}].[{objectQualifier}Topic] SET PollID = @PollGroupID WHERE TopicID = @ttmp
				update [{databaseSchema}].[{objectQualifier}Poll] SET UserID = @utmp, PollGroupID = @PollGroupID, Flags = 0 WHERE PollID = @ptmp
		end       
		fetch next from c into @ptmp, @ttmp, @utmp
		end

		close c
		deallocate c 

		end
GO

if (not exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}PollGroupCluster]) and exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}Poll]))
begin
	--vzrus: migrate to independent multiple polls	
	exec('[{databaseSchema}].[{objectQualifier}pollgroup_migration]')	

		-- vzrus: drop the temporary  sp
IF  EXISTS (SELECT * FROM sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_migration]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_migration]		
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'Flags')
begin
	grant update on [{databaseSchema}].[{objectQualifier}Poll] to public
	exec('update [{databaseSchema}].[{objectQualifier}Poll] set Flags = 0 where Flags is null')
	revoke update on [{databaseSchema}].[{objectQualifier}Poll] from public
	-- here computed columns on Flags should be dropped if exist before
	-- alter table [{databaseSchema}].[{objectQualifier}Poll] alter column Flags int not null
	-- alter table [{databaseSchema}].[{objectQualifier}Poll] add constraint [DF_{objectQualifier}Poll_Flags] default(0) for Flags
end
GO

-- TODO: change userid to not null

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'ObjectPath')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add [ObjectPath] nvarchar(255) NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'MimeType')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add [MimeType] varchar(50) NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'IsClosedBound')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add [IsClosedBound] AS (CONVERT([bit],sign([Flags]&(4)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'AllowMultipleChoices')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add [AllowMultipleChoices] AS (CONVERT([bit],sign([Flags]&(8)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'ShowVoters')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add [ShowVoters] AS (CONVERT([bit],sign([Flags]&(16)),(0)))
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Poll]') and name=N'AllowSkipVote')
begin
	alter table [{databaseSchema}].[{objectQualifier}Poll] add [AllowSkipVote] AS (CONVERT([bit],sign([Flags]&(32)),(0)))
end
GO

 -- PollGroupTable
 if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollGroupCluster]') and name=N'IsBound')
 begin
	alter table [{databaseSchema}].[{objectQualifier}PollGroupCluster] add [IsBound]	AS (CONVERT([bit],sign([Flags]&(2)),(0)))
 end
GO
 
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollGroupCluster]') and name=N'Flags')
begin
	grant update on [{databaseSchema}].[{objectQualifier}PollGroupCluster] to public
	exec('update [{databaseSchema}].[{objectQualifier}PollGroupCluster] set Flags = 0 where Flags is null')
	revoke update on [{databaseSchema}].[{objectQualifier}PollGroupCluster] from public
	-- alter table [{databaseSchema}].[{objectQualifier}PollGroupCluster] alter column Flags int not null
	-- alter table [{databaseSchema}].[{objectQualifier}PollGroupCluster] add constraint [DF_{objectQualifier}PollGroupCluster_Flags] default(0) for Flags
end
GO
-- ActiveAccess Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ActiveAccess]') and name=N'LastActive')
begin
	alter table [{databaseSchema}].[{objectQualifier}ActiveAccess] add [LastActive] datetime NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ActiveAccess]') and name=N'IsGuestX')
begin
	delete from [{databaseSchema}].[{objectQualifier}ActiveAccess]
	alter table [{databaseSchema}].[{objectQualifier}ActiveAccess] add [IsGuestX] bit NOT NULL
end
GO
-- drop the old contrained just in case
if exists (select top 1 1 from  sys.indexes where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ActiveAccess]') and name=N'IX_{objectQualifier}ActiveAccess')
	alter table [{databaseSchema}].[{objectQualifier}ActiveAccess] drop constraint [IX_{objectQualifier}ActiveAccess]
GO

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ActiveAccess]') and name=N'ForumID' and is_nullable=1)
	alter table [{databaseSchema}].[{objectQualifier}ActiveAccess] alter column ForumID int not null
GO

-- Choice Table
-- this is a dummy it doesn't work
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Choice]') and name= N'Choice' and precision < 255 )
begin
	alter table [{databaseSchema}].[{objectQualifier}Choice] alter column Choice varchar(255) NOT NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Choice]') and name=N'ObjectPath')
begin
	alter table [{databaseSchema}].[{objectQualifier}Choice] add [ObjectPath] nvarchar(255) NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Choice]') and name=N'MimeType')
begin
	alter table [{databaseSchema}].[{objectQualifier}Choice] add [MimeType] varchar(50) NULL
end
GO

-- EventLog Table
if not exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}EventLog]') and name=N'Type')
begin
	alter table [{databaseSchema}].[{objectQualifier}EventLog] add Type int not null constraint [DF_{objectQualifier}EventLog_Type] default (0)
	exec('update [{databaseSchema}].[{objectQualifier}EventLog] set Type = 0')
end
GO

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}EventLog]') and name=N'UserID' and is_nullable=0)
	alter table [{databaseSchema}].[{objectQualifier}EventLog] alter column UserID int null
GO	

-- Smiley Table
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Smiley]') and name=N'SortOrder')
begin
	alter table [{databaseSchema}].[{objectQualifier}Smiley] add SortOrder tinyint NOT NULL DEFAULT 0
end
GO

-- Category Table
IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Category]') AND name = N'CategoryImage')
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}Category] ADD [CategoryImage] [nvarchar](255) NULL
END
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Category]') and name=N'PollGroupID')
	alter table [{databaseSchema}].[{objectQualifier}Category] add PollGroupID int NULL
GO

IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Category]') AND name = N'CanHavePersForums')
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}Category] ADD [CanHavePersForums] [bit] NOT NULL constraint [DF_{objectQualifier}Category_CanHavePersForums] default (0)
END
GO

-- MessageReportedAudit Table
IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageReportedAudit]') AND name = N'ReportedNumber')
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}MessageReportedAudit] ADD [ReportedNumber] int NOT NULL DEFAULT 1
END
GO

IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageReportedAudit]') AND name = N'ReportText')
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}MessageReportedAudit] ADD [ReportText] nvarchar(4000)  NULL 
END
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageReportedAudit]') and name=N'MessageID' and is_nullable=1)
begin
		alter table [{databaseSchema}].[{objectQualifier}MessageReportedAudit] alter column [MessageID] integer NOT NULL		    
end
GO

-- BannedIP Table

IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}BannedIP]') AND name = N'Reason')
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}BannedIP] ADD [Reason] nvarchar(128)  NULL 
END
GO

IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}BannedIP]') AND name = N'UserID')
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}BannedIP] ADD [UserID] int  null 
END
GO

IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}BannedIP]') AND name = N'Mask' and precision < 56)
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}BannedIP] alter column [Mask] varchar(57) not  null 
END
GO

-- PollVote Table

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVote]') and name=N'RemoteIP' and precision<39)
	-- vzrus: should drop the index to change the field
	if exists(select * from sys.indexes where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVote]') and name=N'IX_{objectQualifier}PollVote_RemoteIP')
	begin
	begin
	drop index [{databaseSchema}].[{objectQualifier}PollVote].[IX_{objectQualifier}PollVote_RemoteIP]
	end
	alter table [{databaseSchema}].[{objectQualifier}PollVote] alter column [RemoteIP] varchar(39) null
	end
GO	

IF NOT exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PollVote]') AND name = N'ChoiceID')
BEGIN
	ALTER TABLE [{databaseSchema}].[{objectQualifier}PollVote] ADD [ChoiceID] int  null 
END
GO


-- MessageHistory Table

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}MessageHistory]') and name=N'IP' and precision<39)
	alter table [{databaseSchema}].[{objectQualifier}MessageHistory] alter column [IP] varchar(39) not null
GO

-- NntpServer Table

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}NntpServer]') and name=N'UserName' and precision<255)
	alter table [{databaseSchema}].[{objectQualifier}NntpServer] alter column [UserName] nvarchar(255) null
GO

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Email' and precision<255)
	alter table [{databaseSchema}].[{objectQualifier}User] alter column [Email] nvarchar(255) null
GO

if exists(select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}CheckEmail]') and name=N'Email' and precision<255)
	alter table [{databaseSchema}].[{objectQualifier}CheckEmail] alter column [Email] nvarchar(255) null
GO

-- Create Topic Read Tracking Table

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}TopicReadTracking]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}TopicReadTracking](
		UserID			int NOT NULL ,
		TopicID			int NOT NULL ,
		LastAccessDate	datetime NOT NULL
	)
GO

-- Create Forum Read Tracking Table

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ForumReadTracking]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}ForumReadTracking](
		UserID			int NOT NULL ,
		ForumID			int NOT NULL ,
		LastAccessDate	datetime NOT NULL
	)
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'Status')
begin
	alter table [{databaseSchema}].[{objectQualifier}Topic] add [Status] nvarchar(255) null	
end
GO

if exists(select top 1 1 from sys.objects where name=N'PK_{objectQualifier}ForumReadTracking')
	alter table [{databaseSchema}].[{objectQualifier}ForumReadTracking] drop constraint [PK_{objectQualifier}ForumReadTracking] 
GO 

if exists(select top 1 1 from sys.objects where name=N'PK_{objectQualifier}TopicReadTracking')
	alter table [{databaseSchema}].[{objectQualifier}TopicReadTracking] drop constraint [PK_{objectQualifier}TopicReadTracking] 
GO 

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ForumReadTracking]') and name=N'TrackingID')
begin
	alter table [{databaseSchema}].[{objectQualifier}ForumReadTracking] drop column TrackingID
end
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}TopicReadTracking]') and name=N'TrackingID')
begin
	alter table [{databaseSchema}].[{objectQualifier}TopicReadTracking] drop column TrackingID
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}TopicStatus]') and type in (N'U'))
BEGIN
	CREATE TABLE [{databaseSchema}].[{objectQualifier}TopicStatus](
		TopicStatusID int IDENTITY(1,1) NOT NULL,
		TopicStatusName nvarchar(100) NOT NULL,
		BoardID int NOT NULL,
		DefaultDescription nvarchar(100) NOT NULL,
		CONSTRAINT [PK_{objectQualifier}TopicStatus] PRIMARY KEY(TopicStatusID)
	)
END
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Tags]') and type in (N'U'))
BEGIN
	CREATE TABLE [{databaseSchema}].[{objectQualifier}Tags](	  
		TagID int IDENTITY(1,1) NOT NULL,
		Tag nvarchar(1024) NOT NULL, 
		TagCount  int NOT NULL constraint [DF_{objectQualifier}Tags_TagCount] default (0)	  
		CONSTRAINT [PK_{objectQualifier}Tags_TagID] PRIMARY KEY(TagID)
	)
END
GO
exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}AccessMask, SortOrder')
GO
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}AccessMask]') and name=N'SortOrder')
begin
	alter table [{databaseSchema}].[{objectQualifier}AccessMask] alter column [SortOrder] int NOT NULL
end
GO

exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}Group, SortOrder')
GO
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Group]') and name=N'SortOrder')
begin
	alter table [{databaseSchema}].[{objectQualifier}Group] alter column [SortOrder] int NOT NULL
end
GO
exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}Forum, SortOrder')
GO
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Forum]') and name=N'SortOrder')
begin
	alter table [{databaseSchema}].[{objectQualifier}Forum] alter column [SortOrder] int NOT NULL
end
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}TopicTags]') and type in (N'U'))
BEGIN
	CREATE TABLE [{databaseSchema}].[{objectQualifier}TopicTags](	  
		TagID int NOT NULL,
		TopicID int NOT NULL
		CONSTRAINT [PK_{objectQualifier}TopicTags_TagIDTopicID] PRIMARY KEY(TagID,TopicID)
	)
END
GO

exec('[{databaseSchema}].[{objectQualifier}drop_defaultconstraint_oncolumn] {objectQualifier}User, Culture')
GO

-- Add 8-letter Language Code column
if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User]') and name=N'Culture' and precision=5)
begin
	alter table [{databaseSchema}].[{objectQualifier}User] alter column [Culture] varchar(10) NULL
end
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Topic]') and name=N'Styles')
	alter table [{databaseSchema}].[{objectQualifier}Topic] add Styles nvarchar(255) NULL
GO

if not exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ReputationVote]') and type in (N'U'))
	create table [{databaseSchema}].[{objectQualifier}ReputationVote](
		ReputationFromUserID  int NOT NULL,
		ReputationToUserID	  int NOT NULL,
		VoteDate	datetime NOT NULL
	)
GO	

-- display names upgrade routine can run really for ages on large forums 
IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_initdisplayname]'))
DROP procedure [{databaseSchema}].[{objectQualifier}forum_initdisplayname]
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_initdisplayname] as
 
begin 
     update d
       set d.LastUserDisplayName = ISNULL((select top 1 f.LastUserDisplayName FROM [{databaseSchema}].[{objectQualifier}Forum] f
          join [{databaseSchema}].[{objectQualifier}User] u on u.UserID = f.LastUserID where u.UserID = d.LastUserID),
           (select top 1 f.LastUserName FROM [{databaseSchema}].[{objectQualifier}Forum] f
          join [{databaseSchema}].[{objectQualifier}User] u on u.UserID = f.LastUserID where u.UserID = d.LastUserID ))
       from [{databaseSchema}].[{objectQualifier}Forum] d where d.LastUserDisplayName IS NULL OR d.LastUserDisplayName = d.LastUserName;
         
    update d
       set d.UserDisplayName = ISNULL((select top 1 f.UserDisplayName FROM [{databaseSchema}].[{objectQualifier}ShoutboxMessage] f
          join [{databaseSchema}].[{objectQualifier}User] u on u.UserID = f.UserID where u.UserID = d.UserID),
           (select top 1 f.UserName FROM [{databaseSchema}].[{objectQualifier}ShoutboxMessage] f
          join [{databaseSchema}].[{objectQualifier}User] u on u.UserID = f.UserID where u.UserID = d.UserID ))
       from [{databaseSchema}].[{objectQualifier}ShoutboxMessage] d where d.UserDisplayName IS NULL OR d.UserDisplayName = d.UserName;
    update d
       set d.UserDisplayName = ISNULL((select top 1 m.UserDisplayName FROM [{databaseSchema}].[{objectQualifier}Message] m
          join [{databaseSchema}].[{objectQualifier}User] u on u.UserID = m.UserID where u.UserID = d.UserID),
           (select top 1 m.UserName FROM [{databaseSchema}].[{objectQualifier}Message] m
          join [{databaseSchema}].[{objectQualifier}User] u on u.UserID = m.UserID where u.UserID = d.UserID ))
       from [{databaseSchema}].[{objectQualifier}Message] d where d.UserDisplayName IS NULL OR d.UserDisplayName = d.UserName;
     update d
       set d.UserDisplayName = ISNULL((select top 1 t.UserDisplayName FROM [{databaseSchema}].[{objectQualifier}Topic] t
          join [{databaseSchema}].[{objectQualifier}User] u on u.UserID = t.UserID where u.UserID = d.UserID),
           (select top 1 t.UserName FROM [{databaseSchema}].[{objectQualifier}Topic] t
          join [{databaseSchema}].[{objectQualifier}User] u on u.UserID = t.UserID where u.UserID = d.UserID ))
       from [{databaseSchema}].[{objectQualifier}Message] d where d.UserDisplayName IS NULL OR d.UserDisplayName = d.UserName;
 
end
GO

if exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}Message] where UserDisplayName IS NULL)
exec('[{databaseSchema}].[{objectQualifier}forum_initdisplayname]')
GO

-- add ReplyTo Column to PMessage Table if not exists
if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}PMessage]') and name=N'ReplyTo')
	alter table [{databaseSchema}].[{objectQualifier}PMessage] add ReplyTo int NULL
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserPMessage]') and name=N'IsReply')
	alter table [{databaseSchema}].[{objectQualifier}UserPMessage] ADD [IsReply] [bit] NOT NULL  DEFAULT (0)
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}ActiveAccess]') and name=N'UserForumAccess')
	alter table [{databaseSchema}].[{objectQualifier}ActiveAccess] ADD [UserForumAccess] [bit] NOT NULL  DEFAULT (0)
GO

if not exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}UserProfile]') and name=N'Birthday')
	alter table [{databaseSchema}].[{objectQualifier}UserProfile] add Birthday datetime NULL
GO

if exists (select top 1 1 from sys.columns where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}Message]') and name=N'Edited')
begin
	grant update on [{databaseSchema}].[{objectQualifier}Message] to public
	exec('update [{databaseSchema}].[{objectQualifier}Message] set Edited = Posted where Edited is null')	
end
GO