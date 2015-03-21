-- This scripts for PostgreSQL Yet Another Forum https://github.com/vzrus/YetAnotherForumExtraDataLayers http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team  https://github.com/vzrus
-- They are distributed under terms of GPLv2 licence only as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2009-2012




CREATE TYPE {databaseSchema}.{objectQualifier}user_table_type AS
(
  "UserID" integer,
  "BoardID" integer,
  "ProviderUserKey" varchar(36),
  "Name" varchar(128),
  "Password" varchar(32),
  "Email" varchar(128),
  "Joined" timestamp,
  "LastVisit" timestamp,
  "IP" varchar(39),
  "NumPosts" integer,
  "TimeZone" integer,
  "Avatar" varchar(255),
  "Signature" text,
  "AvatarImage" bytea,
  "AvatarImageType" varchar(128),
  "RankID" integer,  
  "Suspended" timestamp,
  "LanguageFile" varchar(128),
  "ThemeFile" varchar(128),
  "OverrideDefaultThemes" boolean,
  "PMNotification" boolean,
  "Flags" integer,
  "Points" integer,
  "AutoWatchTopics" boolean,
  "DisplayName" varchar(128),
  "CultureUser" varchar(10),  
  "DailyDigest" boolean,
  "NotificationType" integer,
  "TextEditor" varchar(50),
  "UseSingleSignOn" boolean 
);
--GO

-- derived types

CREATE TYPE {databaseSchema}.{objectQualifier}accessmask_list_return_type AS
(
"AccessMaskID" integer,
"BoardID" integer,
"Name" varchar,
"Flags" integer,
"SortOrder" integer,
"IsUserMask" boolean,
"IsAdminMask" boolean,
"CreatedByUserID" integer,
"CreatedByUserName" varchar,
"CreatedByUserDisplayName" varchar,
"TotalRows" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}active_list_return_type AS
(
"UserID" integer,
"UserName" varchar(128),
"UserDisplayName" varchar(128),
"IP" varchar(39),
"SessionID" varchar(32),
"ForumID" integer,
"TopicID" integer,
"ForumName" varchar(128),
"TopicName" varchar(128), 
"IsGuest" boolean,
"IsCrawler" boolean,
"IsHidden" boolean,
"Style" varchar(255), 
"UserCount" integer,
"Login" timestamp,
"LastActive" timestamp,
"Location" varchar(1024),
"Active"  integer,
"Browser" varchar(128),
"Platform" varchar(128),
"ForumPage"  varchar(1024),
"ActiveSpan" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}active_list_user_return_type AS
(
"UserID" integer,
"UserName" varchar(128),
"UserDisplayName" varchar(128),
"IP" varchar(39),
"SessionID" varchar(32),
"ForumID" integer,
"HasForumAccess" integer,
"TopicID" integer,
"ForumName" varchar(128),
"TopicName" varchar(128), 
"IsGuest" boolean,
"IsCrawler" boolean,
"IsHidden" boolean,
"Style" varchar(1024), 
"UserCount" integer,
"Login" timestamp,
"LastActive" timestamp,
"Location" varchar(1024),
"Active"  integer,
"Browser" varchar(128),
"Platform" varchar(128),
"ForumPage"  varchar(1024),
"ActiveSpan" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}active_listforum_return_type AS
(
"UserID" integer,
"UserName" varchar(128),
"UserDisplayName" varchar(128),
"IsCrawler" boolean,
"IsHidden" boolean,
"Style" varchar(255), 
"UserCount" integer,
"Browser" varchar(128)
);

--GO

CREATE TYPE {databaseSchema}.{objectQualifier}active_listtopic_return_type AS
(
"UserID" integer,
"UserName" varchar(128),
"UserDisplayName" varchar(128),
"IsCrawler" boolean,
"IsHidden" boolean,
"Style" varchar(255), 
"UserCount" integer,
"Browser" varchar(128) 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}active_stats_return_type AS
(
"ActiveUsers" integer, 
"ActiveGuests" integer, 
"ActiveMembers" integer, 
"ActiveHidden" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}attachment_list_return_type AS
(
"AttachmentID" integer,
"MessageID" integer,
"FileName" varchar(255),
"Bytes" integer,
"FileID" integer,
"ContentType" varchar(128),
"Downloads" integer,
"FileData" bytea,
"BoardID" integer,
"Posted" timestamp,
"ForumID" integer,
"ForumName" varchar(128),
"TopicID" integer, 
"TopicName"  varchar(128),
"TotalRows" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}bannedip_list_return_type AS
(
"ID" integer,
"BoardID" integer,
"Mask" varchar(57),
"Since" timestamp,
"Reason" varchar(128),
"UserID" integer,
"TotalRows" integer
);
--GO

 CREATE TYPE {databaseSchema}.{objectQualifier}bbcode_list_return_type AS
 (
 "BBCodeID" integer,
 "BoardID" integer, 
 "Name" varchar(255),
 "Description" varchar(4000),
 "OnClickJS" varchar(1000),
 "DisplayJS" text,
 "EditJS" text,
 "DisplayCSS" text,
 "SearchRegex" text,
 "ReplaceRegex" text,
 "Variables" varchar(1000),
 "UseModule" boolean,
 "ModuleClass" varchar(255),
 "ExecOrder" integer
 );
 
 --GO 

CREATE TYPE {databaseSchema}.{objectQualifier}board_list_return_type AS
(
	"BoardID" integer,
	"Name" varchar(128),
	"AllowThreaded" boolean,
	"MembershipAppName" varchar(255),
	"RolesAppName" varchar(255),
	"SQLVersion" varchar(512)
);

--GO

CREATE TYPE {databaseSchema}.{objectQualifier}board_poststats_return_type AS
(
"Posts" integer,
"Topics" integer,
"Forums" integer,
"LastPostInfoID" integer,
"LastPost" timestamp,
"LastUserID" integer,
"LastUser" varchar(255),
"LastUserDisplayName" varchar(255),
"LastUserStyle"  varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}board_userstats_return_type AS
(
"Members" integer,
"MaxUsers" integer,
"MaxUsersWhen" timestamp,
"LastMemberInfoID" integer,
"LastMemberID" integer,
"LastMember" varchar(255),
"LastMemberDisplayName" varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}board_stats_return_type AS
(
"NumPosts" integer,
"NumTopics" integer,
"NumUsers" integer,
"BoardStart" timestamp 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}category_list_return_type AS
(
"CategoryID" integer,
"BoardID" integer,
"Name" varchar(128),
"CategoryImage" varchar(255),
"SortOrder" integer,
"PollGroupID" integer,
"CanHavePersForums" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}category_pfaccesslist_return_type AS
(
"CategoryID" integer,
"BoardID" integer,
"Name" varchar(128),
"CategoryImage" varchar(255),
"SortOrder" integer,
"PollGroupID" integer,
"CanHavePersForums" boolean,
"HasForumsForPersForums" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}category_listread_return_type AS
(
"CategoryID" integer,
"Name" varchar(128),
"CategoryImage" varchar(255),
"ForumID" integer,
"Flags"  integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}category_simplelist_return_type AS
(
"CategoryID" integer,
"Name" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}checkemail_list_return_type AS (
	"CheckEmailID" integer,
	"UserID" integer,
	"Email" varchar(128),
	"Created" timestamp,
	"Hash" varchar(32)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}checkemail_update_return_type AS (
	"ProviderUserKey" varchar(64),    
	"Email" varchar(128)   
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}extension_return_type AS
(
"ExtensionID" integer,
"BoardID" integer,
"Extension" varchar(10)
);
--GO

CREATE TYPE  {databaseSchema}.{objectQualifier}eventlog_list_return_type AS
(
"EventLogID" integer,
"EventTime" timestamp,
"UserID" integer,
"Source" varchar(128),
"Description" text,
"Type" integer,
"Name" varchar(255),
"TotalRows" integer
);
--GO

CREATE TYPE  {databaseSchema}.{objectQualifier}eventloggroupaccess_list_rt AS
(
"GroupID" integer,
"EventTypeID" integer,
"EventTypeName" varchar(128),
"DeleteAccess" boolean,
"GroupName" varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_list_return_type AS
(
"ForumID" integer,
"CategoryID" integer,
"ParentID" integer,
"Name" varchar(128),
"Description" varchar(255),
"SortOrder" integer,
"LastPosted" timestamp,
"LastTopicID" integer,
"LastMessageID" integer,
"LastUserID" integer,
"LastUserName" varchar(128),
"NumTopics" integer,
"NumPosts" integer,
"RemoteURL" varchar(100),
"Flags" integer,
"ThemeURL" varchar(100),
"ImageURL" varchar(128),
"Styles" varchar(255),
"PollGroupID" integer,
"IsUserForum" boolean,
"CreatedByUserID" integer,
"CanHavePersForums" boolean
);

--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_listall_return_type AS
(
"CategoryID" integer,
"Category" varchar(100),
"ForumID" integer,
"Forum" varchar(100),
"Indent" integer,
"ParentID" integer,
"Flags" integer,
"PollGroupID" integer,
"IsHidden" boolean,
"CanHavePersForums" boolean,
"ReadAccess" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_listall_fromcat_return_type AS
(
"CategoryID" integer,
"Category" varchar(255),
"ForumID" integer,
"Forum" varchar(100),
"ParentID" integer,
"PollGroupID" integer,
"CanHavePersForums" boolean,
"IsHidden" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_listallmymoderated_return_type AS
(
"CategoryID" integer,
"Category" varchar(255),
"ForumID" integer,
"Forum" varchar(100),
"Indent" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_listpath_return_type AS
(
"ForumID" integer,
"Name" varchar(255),
"Level" integer
);

CREATE TYPE {databaseSchema}.{objectQualifier}forumid_return_type AS
(
"ForumID" integer
);
--GO 

CREATE TYPE {databaseSchema}.{objectQualifier}forum_listread_helper_return_type AS
(
"LastPosted" timestamp,
"LastMessageID" integer,
"LastMessageFlags" integer,
"LastUserID" integer,
"LastTopicID" integer,
"TopicMovedID" integer,
"LastTopicName" varchar(255)
);
--GO 

CREATE TYPE {databaseSchema}.{objectQualifier}forum_listread_return_type AS
(
"CategoryID" integer,
"Category" varchar(128),
"ForumID" integer,
"ParentID" integer,
"Forum" varchar(128),
"Description" varchar(255),
"ImageUrl"  varchar(128),
"PollGroupID" integer,
"IsUserForum" boolean,
/*"LastTopicID" integer,*/
"Topics" integer,
"Posts" integer,
/* "Subforums" integer, */
"LastTopicID" integer,
"LastTopicStatus" varchar(255),
"LastTopicStyles" varchar(255),
"TopicMovedID" integer,
"LastPosted" timestamp,
"LastMessageID" integer,
"LastMessageFlags" integer,
"LastUserID" integer,
"LastTopicName"  varchar(128),
"LastUser"  varchar(128),
"LastUserDisplayName" varchar(128),
"Flags" integer,
"Style"  varchar(255),
"Viewing" integer,
"RemoteURL" varchar(255),
"ReadAccess" integer,
"LastForumAccess"  timestamp,
"LastTopicAccess"  timestamp 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_ns_listread_return_type AS
(
"CategoryID" integer,
"Category" varchar(128),
"ForumID" integer,
"ParentID" integer,
"Forum" varchar(128),
"Description" varchar(255),
"ImageUrl"  varchar(128),
"PollGroupID" integer,
"IsUserForum" boolean,
left_key integer,
right_key integer,
"PageUserID" integer,
/*"LastTopicID" integer,*/
"Topics" integer,
"Posts" integer,
/* "Subforums" integer, */
"LastTopicID" integer,
"LastTopicStatus" varchar(255),
"LastTopicStyles" varchar(255),
"TopicMovedID" integer,
"LastPosted" timestamp,
"LastMessageID" integer,
"LastMessageFlags" integer,
"LastUserID" integer,
"LastTopicName"  varchar(128),
"LastUser"  varchar(128),
"LastUserDisplayName" varchar(128),
"Flags" integer,
"Style"  varchar(255),
"Viewing" integer,
"RemoteURL" varchar(255),
"ReadAccess" integer,
"LastForumAccess"  timestamp,
"LastTopicAccess"  timestamp 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_listtopics_return_type AS
(
	"TopicID" integer,
	"ForumID" integer,
	"UserID" integer,
	"UserName" varchar(128),
	"Posted" timestamp,
	"Topic" varchar(128),
	"Views" integer,
	"Priority" smallint,
	"PollID" integer,
	"TopicMovedID" integer,
	"LastPosted" timestamp,
	"LastMessageID" integer,    
	"LastUserID" integer,
	"LastUserName" varchar(128),
	"NumPosts" integer,
	"Flags" integer,
	"AnswerMessageID" integer,
	"LastMessageFlags" integer,
	"Description" varchar(255),
	"Status" varchar(255),
	"IsDeleted" boolean,
	"IsQuestion" boolean
);
--GO


CREATE TYPE {databaseSchema}.{objectQualifier}forum_moderatelist_return_type AS
(
"ForumID" integer,
"CategoryID" integer,
"ParentID" integer,
"Name" varchar(128),
"Description" varchar(255),
"SortOrder" integer,
"LastPosted" timestamp,
"LastTopicID" integer,
"LastMessageID" integer,
"LastUserID" integer,
"LastUserName" varchar(128),
"NumTopics" integer,
"NumPosts" integer,
"RemoteURL" varchar(128),
"Flags" integer,
"ThemeURL" varchar(128),
"ImageURL" varchar(128),
"Styles" varchar(128),
"PollGroupID" integer,
"MessageCount" integer,
"ReportedCount" integer,
"ModeratorAccess" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_moderators_return_type AS
(
"ForumID"  integer,
"ForumName" varchar(255),
"ModeratorID" integer,
"ModeratorName" varchar(255),
"ModeratorDisplayName" varchar(255),
"ModeratorEmail" varchar(255),
"ModeratorAvatar" varchar(255),
"ModeratorAvatarImage" boolean,
"Style" varchar(255),
"IsGroup" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}moderators_team_list_return_type AS
(
"ForumID"  integer,
"ForumName" varchar(255),
"ModeratorID" integer,
"ModeratorName" varchar(255),
"ModeratorDisplayName" varchar(255),
"ModeratorEmail" varchar(255),
"ModeratorAvatar" varchar(255),
"ModeratorAvatarImage" boolean,
"Style" varchar(255),
"IsGroup" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_simplelist_return_type AS
(
"ForumID" integer,
"Name" varchar(128),
"LastPosted" timestamp
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forumaccess_group_return_type AS
(
"GroupID" integer,
"ForumID" integer,
"AccessMaskID" integer,
"ForumName"  varchar(128),
"CategoryName" varchar(128),
"CategoryID" integer,
"ParentID" integer,
"BoardName"  varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forumaccess_list_return_type AS
(
"GroupID" integer,
"ForumID" integer,
"AccessMaskID" integer,
"GroupName"  varchar(128),
"IsUserGroup" boolean,
"IsAdminGroup" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}group_list_return_type AS
(
"GroupID" integer,
"BoardID" integer,
"Name" varchar(128),
"Flags" integer,
"PMLimit" integer,
"Style" varchar(256),
"SortOrder" integer,
"Description" varchar(128),
"UsrSigChars" integer,
"UsrSigBBCodes" varchar(255),
"UsrSigHTMLTags"  varchar(255),
"UsrAlbums" integer,
"UsrAlbumImages" integer,
"IsAdminGroup" boolean,
"IsUserGroup" boolean,
"UsrPersonalMasks" integer,
"UsrPersonalGroups" integer,
"UsrPersonalForums" integer,
"CreatedByUserID" integer,
"CreatedByUserName" varchar(255),
"CreatedByUserDisplayName" varchar(255),
"TotalRows" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}group_medal_list_return_type AS
(
"MedalID" integer,
"BoardID" integer,
"Name" varchar(128),
"MedalURL" varchar(255),
"RibbonURL" varchar(255),
"SmallMedalURL" varchar(255),
"SmallRibbonURL" varchar(255),
"SmallMedalWidth" integer,
"SmallMedalHeight" integer,
"SmallRibbonWidth" integer,
"SmallRibbonHeight" integer,
"SortOrder" integer,
"Flags" integer,
"GroupName" varchar(128),
"GroupID" integer,
"Message" text,
"MessageEx" text,
"Hide" boolean,
"OnlyRibbon" boolean,
"CurrentSortOrder" smallint
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}group_member_return_type AS
(
"GroupID" integer,
"Name" varchar(128),
"IsHidden" boolean,
"IsUserGroup" boolean,
"Style" varchar(1024),
"Member" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}mail_list_return_type AS (
"MailID" integer,
"FromUser" varchar(128),
"FromUserName" varchar(128),
"ToUser" varchar(128),
"ToUserName" varchar(128),
"Created" timestamp,
"Subject" varchar(255),
"Body" text,
"BodyHtml" text,
"SendTries" integer,
"SendAttempt" timestamp,
"ProcessID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}medal_list_return_type AS 
(
"MedalID" integer,
"BoardID" integer,
"Name" varchar(128),
"Description" text,
"Message" varchar(128),
"Category" varchar(128),
"MedalURL" varchar(250),
"RibbonURL" varchar(250),
"SmallMedalURL" varchar(250),
"SmallRibbonURL" varchar(250),
"SmallMedalWidth" smallint,
"SmallMedalHeight" smallint,
"SmallRibbonWidth" smallint,
"SmallRibbonHeight" smallint,
"SortOrder" integer,
"Flags" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}medal_listusers_return_type AS 
(
"UserID" integer,
"Name" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_findunread_return_type AS 
(
"MessageID" integer,
"MessagePosition" integer, 
"FirstMessageID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_getreplies_return_type AS 
(
"MessageID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_list_return_type AS 
(
"MessageID" integer,
"UserID" integer,
"UserName" varchar(255),
"UserDisplayName" varchar(255),
"Message" text,
"TopicID" integer,
"ForumID" integer,
"Topic"  varchar(255),
"Status"  varchar(255),
"Styles"  varchar(255),
"Priority"  smallint,
"Description"  varchar(255),
"Flags" integer,
"TopicOwnerID"  integer,
"Edited" timestamp,
"TopicFlags"  integer,
"ForumFlags"  integer,
"EditReason" varchar(100),
"Position"  integer,
"IsModeratorChanged" boolean,
"DeleteReason" varchar(100),
"BlogPostID"   varchar(100),
"PollID"   integer,
"IP" varchar(39),
"ReplyTo" integer,
"ExternalMessageId" varchar(255),
"ReferenceMessageId" varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_listreported_return_type AS 
(
"MessageID" integer,
"Message" text,
"Resolved" boolean,
"ResolvedBy" integer,
"ResolvedDate" timestamp,
"OriginalMessage" text,
"Flags" integer,
"IsModeratorChanged" boolean,
"UserName" varchar(128),
"UserDisplayName" varchar(255),
"UserID" integer,
"Posted" timestamp,
"TopicID" integer,
"Topic" varchar(128), 
"NumberOfReports" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_reply_list_return_type AS 
(
"MessageID" integer,
"Posted" timestamp,
"Subject" varchar(128),
"Message" text,
"UserID" integer,
"Flags" integer,
"UserName" varchar(128),
"Signature" text);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_simplelist_return_type AS
(
"MessageID" integer,
"TopicID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_unapproved_return_type AS 
(
"MessageID" integer,
"UserID" integer, 
"UserName" varchar(128),
"UserDisplayName" varchar(128),
"Posted" timestamp,
"TopicID" integer, 
"Topic" varchar(128),
"MessageCount" integer,
"Message" text,
"Flags" integer,
"IsModeratorChanged" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}nntpforum_list_return_type AS 
(
"Name" varchar(128),
"Address" varchar(100),
"Port" integer,
"UserName" varchar(128),
"UserPass" varchar(128),
"NntpServerID" integer,
"NntpForumID" integer,
"GroupName" varchar(128),
"ForumID" integer,
"LastMessageNo" integer,
"LastUpdate" timestamp,
"Active" boolean,
"DateCutOff"  timestamp,
"ForumName" varchar(128),
"CategoryID" integer 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}nntpserver_list_return_type AS 
(
"NntpServerID" integer,
"BoardID" integer,
"Name" varchar(128),
"Address" varchar(128),
"Port" integer,
"UserName" varchar(128),
"UserPass" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}nntptopic_list_return_type AS 
(
"NntpTopicID" integer,
"NntpForumID" integer,
"Thread" character(32),
"TopicID" integer
);
--GO

SELECT {databaseSchema}.{objectQualifier}drop_type('{databaseSchema}','{objectQualifier}pmessage_info_return_type');
--GO
CREATE TYPE {databaseSchema}.{objectQualifier}pmessage_info_return_type AS 
(
"NumRead" integer,
"NumUnread" integer,
"NumTotal" integer
);
--GO

SELECT {databaseSchema}.{objectQualifier}drop_type('{databaseSchema}','{objectQualifier}pmessage_list_return_type');
--GO
CREATE TYPE {databaseSchema}.{objectQualifier}pmessage_list_return_type AS 
(
"PMessageID" integer,
"ReplyTo" integer,
"UserPMessageID" integer,
"FromUserID" integer,
"FromUser" varchar(128),
"ToUserID" integer,
"ToUser" varchar(128),
"Created" timestamp,
"Subject" varchar(128),
"Body" text,
"Flags" integer,
"IsRead" boolean,
"IsInOutbox" boolean,
"IsArchived" boolean,
"IsDeleted" boolean,
"IsReply" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}poll_save_return_type AS
(
"PollID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}poll_stats_return_type AS
(
"PollID" integer,
"Question" varchar(128),
"Closes" timestamp,
"UserID" integer,
"ObjectPath"  varchar(255),
"MimeType"  varchar(50),
"QuestionObjectPath"  varchar(255),
"QuestionMimeType"  varchar(50),
"ChoiceID" integer,
"Choice" varchar(128),
"Votes" integer,
"IsBound" boolean,
"IsClosedBound" boolean,
"AllowMultipleChoices"  boolean,
"ShowVoters" boolean,
"AllowSkipVote" boolean,
"Total" integer,
"Stats" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}pollgroup_stats_return_type AS
(
"GroupUserID" integer,
"PollID" integer,
"PollGroupID" integer,
"Question" varchar(128),
"Closes" timestamp,
"ChoiceID" integer,
"Choice"   varchar(255),
"Votes" integer,
"ObjectPath"  varchar(255),
"MimeType"  varchar(50),
"QuestionObjectPath"  varchar(255),
"QuestionMimeType"  varchar(50),
"IsBound" boolean,
"IsClosedBound" boolean,
"AllowMultipleChoices"  boolean,
"ShowVoters" boolean,
"AllowSkipVote" boolean,
"Total" integer,
"Stats" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}pollgroup_votecheck_return_type AS
(
"PollID" integer,
"ChoiceID" integer,
"UserName"  varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}pollvote_check_return_type AS
(
"PollVoteID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}post_alluser_type AS
(
"Posted" timestamp,
"Subject" varchar(128),
"MessageID" integer,
"Message" text,
"IP" varchar(39),
"UserID" integer,
"Flags" integer,
"UserName" varchar(128),
"UserDisplayName" varchar(128),
"Signature" text,
"TopicID" integer,
"ForumID" integer,
"ReadAccess" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}post_list_type AS
(
		"TopicID" integer,
		"Topic"  varchar(255),
		"Priority" integer,
		"Description"  varchar(255),
		"Status"  varchar(255),
		"Styles"  varchar(255),
		"PollID" integer,
		"TopicOwnerID" integer,
		"TopicFlags" integer,
		"ForumFlags" integer,
		"MessageID" integer,
		"Posted" timestamp, 		
		"Message" text,
		"MessageDescription" varchar(255),
		"UserID" integer,
		"Position" integer,
		"Indent" integer,
		"IP" varchar(39),
		"Flags" integer,
		"EditReason" varchar(255) ,
		"IsModeratorChanged" boolean,
		"IsDeleted" boolean,
		"DeleteReason" varchar(255),
		"BlogPostID" varchar(255),
		"ExternalMessageId" varchar(255),
		"ReferenceMessageId" varchar(255),
		"UserName" varchar(255),
		"DisplayName" varchar(255),
		"Suspended" timestamp,
		"Joined" timestamp,
		"Avatar" varchar(100),
		"Signature" text,
		"Posts" integer,
		"Points" integer,
		"ReputationVoteDate" timestamp,
		"IsGuest" bool,
		"Views" integer,
		"ForumID" integer,
		"RankName" varchar(128),
		"RankImage" varchar(100),
		"RankStyle"  varchar(1024),
		"Style"  varchar(1024),
		"Edited" timestamp,
		"EditedBy" integer,
		"HasAttachments" integer,
		"HasAvatarImage" integer,
		"TotalRows" integer,
		"PageIndex" integer, 
		"FirstRowNumber" integer
);        
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}post_list_reverse10_return_type AS
(
"Posted" timestamp,
"Subject" varchar(128),
"Message" text,
"UserID" integer,
"Flags" integer,
"UserName" varchar(128),
"UserDisplayName" varchar(128),
"Signature" text
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}rank_list_return_type AS
(
  "RankID" integer,
  "BoardID" integer,
  "Name" varchar(128),
  "MinPosts" integer,
  "RankImage" varchar(128),
  "Flags" integer,
  "PMLimit" integer,
  "Style" varchar(256),
  "SortOrder" integer,
  "Description" varchar(128),
  "UsrSigChars" integer,
  "UsrSigBBCodes" varchar(255),
  "UsrSigHTMLTags"  varchar(255),
  "UsrAlbums" integer,
  "UsrAlbumImages" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}registry_return_type AS (
	"RegistryID" integer,
	"Name" varchar(128),
	"Value" text,
	"BoardID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}replace_words_list_select  AS (
	"ID" integer,
	"BoardID" integer ,
	"BadWord" varchar(255),
	"GoodWord" varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}smiley_list_selecttype AS (
	"SmileyID" integer,
	"BoardID" integer,
	"Code" varchar(10),
	"Icon" varchar(128),
	"Emoticon" varchar(128),
	"SortOrder" int
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}smiley_listunique_selecttype AS (
"Icon" varchar(128), 
"Emoticon" varchar(128),
"Code" varchar(10),
"SortOrder" smallint);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_active_return_type AS
(
"ForumID" integer,
"TopicID" integer,
"TopicMovedID" integer,
"Posted" timestamp,
"LinkTopicID" integer,
"Subject" varchar(255),
"Status" varchar(255),
"Styles" varchar(255),
"Description" varchar(255),
"UserID" integer,
"Starter" varchar(128),
"StarterDisplay" varchar(128),
"NumPostsDeleted" integer,
"Replies" integer,
"Views" integer,
"LastPosted" timestamp,
"LastUserID" integer,
"LastUserName" varchar(128),
"LastUserDisplayName" varchar(128),
"LastMessageID" integer,
"LastMessageFlags" integer,
"LastTopicID" integer,
"TopicFlags" integer,
"FavoriteCount" integer,
"Priority" smallint,
"PollID" integer,
"ForumName" varchar(128),
"ForumFlags" integer,
"FirstMessage" text,
"StarterStyle"  varchar(255),
"LastUserStyle"  varchar(255),
"LastForumAccess"  timestamp,
"LastTopicAccess"  timestamp,
"TopicTags" text,  
"TopicImage" varchar(255),
"TopicImageType" varchar(50),
"TopicImageBin" bytea,
"HasAttachments" integer,
"TotalRows" integer,
"PageIndex" integer	  	     
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_unread_return_type AS
(
"ForumID" integer,
"TopicID" integer,
"TopicMovedID" integer,
"Posted" timestamp,
"LinkTopicID" integer,
"Subject" varchar(255),
"Status" varchar(255),
"Styles" varchar(255),
"Description" varchar(255),
"UserID" integer,
"Starter" varchar(128),
"StarterDisplay" varchar(128),
"NumPostsDeleted" integer,
"Replies" integer,
"Views" integer,
"LastPosted" timestamp,
"LastUserID" integer,
"LastUserName" varchar(128),
"LastUserDisplayName" varchar(128),
"LastMessageID" integer,
"LastMessageFlags" integer,
"LastTopicID" integer,
"TopicFlags" integer,
"FavoriteCount" integer,
"Priority" smallint,
"PollID" integer,
"ForumName" varchar(128),
"ForumFlags" integer,
"FirstMessage" text,
"StarterStyle"  varchar(255),
"LastUserStyle"  varchar(255),
"LastForumAccess"  timestamp,
"LastTopicAccess"  timestamp,
"TopicTags" text,  
"TopicImage" varchar(255),
"TopicImageType" varchar(50),
"TopicImageBin" bytea,
"HasAttachments" integer,
"TotalRows" integer,
"PageIndex" integer	  	     
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_announcements_return_type AS
(
"Topic" varchar(128),
"LastPosted" timestamp,
"Posted" timestamp, 
"TopicID" integer,
"LastMessageID" integer,
"LastMessageFlags" integer,
"ForumID" integer,
"Flags" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_findnext_return_type AS
(
"TopicID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}pollgroup_list_return_type AS
(
"Question"  varchar(255),
"PollGroupID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_findprevnext_return_type AS
(
"TopicID"  integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_info_return_type AS (
	"TopicID" integer,
	"ForumID" integer,
	"UserID" integer,
	"UserName" varchar(128),
	"Posted" timestamp,
	"Topic" varchar(128),
	"Views" integer,
	"Priority" smallint,
	"PollID" integer,
	"TopicMovedID" integer,
	"LastPosted" timestamp,
	"LastMessageID" integer,    
	"LastUserID" integer,
	"LastUserName" varchar(128),
	"NumPosts" integer,
	"Flags" integer,
	"AnswerMessageID" integer,
	"LastMessageFlags" integer,
	"Description" varchar(255),
	"Status" varchar(255),
	"Styles" varchar(255),
	"TopicTags" text,	
	"TopicImage" varchar(255),
	"TopicImageType" varchar(50),
	"TopicImageBin" bytea,
	"FirstMessage" text,
	"IsLocked" boolean,
	"IsDeleted" boolean,
	"IsPersistent" boolean,
	"IsQuestion" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_latest_return_type AS
(
"LastPosted" timestamp,
"ForumID" integer,
"Forum" varchar(100),
"Topic" varchar(128),
"Status" varchar(255),
"Styles" varchar(255),
"TopicID" integer,
"TopicMovedID" integer,
"UserID" integer,
"UserName" varchar(255),
"UserDisplayName" varchar(255),
"StarterIsGuest" boolean,
"LastMessageID" integer,
"LastMessageFlags" integer,
"LastUserID" integer,
"NumPosts" integer,
"Posted"  timestamp,
"LastUserName" varchar(128),
"LastUserDisplayName" varchar(128),
"LastUserStyle"  varchar(255),
"LastUserIsGuest" boolean,
"LastForumAccess" timestamp,
"LastTopicAccess"  timestamp
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}rss_topic_latest_return_type AS
(
"LastMessage" text, 
"LastPosted" timestamp,
"ForumID" integer,
"Forum" varchar(128),
"Topic" varchar(128),
"TopicID" integer,
"TopicMovedID" integer,
"UserID" integer,
"UserName" varchar(255),
"UserDisplayName" varchar(255),
"StarterIsGuest" boolean,
"LastMessageID" integer,
"LastMessageFlags" integer,
"LastUserID" integer,
"Posted" timestamp,
"LastUserName" varchar(255),
"LastUserDisplayName" varchar(255),
"LastUserIsGuest" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_list_return_type AS (   
	"ForumID" integer,
	"TopicID" integer,
	"Posted" timestamp,
	"LinkTopicID" integer,
	"TopicMovedID" integer,
	"FavoriteCount" integer,
	"Subject" varchar(255),
	"Description" varchar(255),
	"Status" varchar(255),
	"Styles" varchar(255),
	"UserID" integer,    
	"Starter" varchar(128),
	"StarterDisplay" varchar(128),
	"Replies" integer,
	"NumPostsDeleted" integer,  
	"Views" integer,
	"LastPosted" timestamp,   
	"LastUserID" integer,
	"LastUserName" varchar(128),
	"LastUserDisplayName" varchar(128),
	"LastMessageID" integer,
	"LastTopicID" integer,
	"LinkDate" timestamp, 
	"TopicFlags" integer,
	"Priority" smallint,
	"PollID" integer,
	"ForumFlags" integer,
	"FirstMessage"   text,	
	"StarterStyle"  varchar(255), 
	"LastUserStyle"  varchar(255),
	"LastForumAccess"  timestamp,
	"LastTopicAccess"  timestamp,
	"TopicTags" text,  
	"TopicImage" varchar(255),
	"TopicImageType" varchar(50),
	"TopicImageBin" bytea,
	"HasAttachments" integer,
	"TotalRows" integer,
	"PageIndex" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_bytags_rt AS (   
	"ForumID" integer,
	"TopicID" integer,
	"Posted" timestamp,
	"LinkTopicID" integer,
	"TopicMovedID" integer,
	"FavoriteCount" integer,
	"Subject" varchar(255),
	"Description" varchar(255),
	"Status" varchar(255),
	"Styles" varchar(255),
	"UserID" integer,    
	"Starter" varchar(128),
	"StarterDisplay" varchar(128),
	"Replies" integer,
	"NumPostsDeleted" integer,  
	"Views" integer,
	"LastPosted" timestamp,   
	"LastUserID" integer,
	"LastUserName" varchar(128),
	"LastUserDisplayName" varchar(128),
	"LastMessageID" integer,
	"LastTopicID" integer,
	"LinkDate" timestamp, 
	"TopicFlags" integer,
	"Priority" smallint,
	"PollID" integer,
	"ForumFlags" integer,
	"FirstMessage"   text,	
	"StarterStyle"  varchar(255), 
	"LastUserStyle"  varchar(255),
	"LastForumAccess"  timestamp,
	"LastTopicAccess"  timestamp,
	"Tags" text,
	"TopicImage" varchar(255),
	"TopicImageBin" bytea,
	"TopicImageType" varchar(50),	
	"HasAttachments" integer,
	"TotalRows" integer,
	"PageIndex" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topics_byuser_return_type AS (  
	"ForumID" integer,
	"TopicID" integer,
	"TopicMovedID" integer,
	"Posted" timestamp,
	"LinkTopicID" integer,	
	"Subject" varchar(255),
	"Description" varchar(255),
	"Status"  varchar(255),
	"Styles"  varchar(255),
	"UserID" integer,    
	"Starter" varchar(255),
	"StarterDisplay" varchar(255),
	"NumPostsDeleted" integer,  
	"Replies" integer,   
	"Views" integer,
	"LastPosted" timestamp,   
	"LastUserID" integer,
	"LastUserName" varchar(255),
	"LastUserDisplayName" varchar(255),
	"LastMessageID" integer,
	"LastMessageFlags" integer,
	"LastTopicID" integer,
	"TopicFlags" integer,
	"FavoriteCount" integer,
	"Priority" smallint,
	"PollID" integer,
	"ForumName" varchar(255),	
	"ForumFlags" integer,
	"FirstMessage"  text,
	"StarterStyle" varchar(255),
	"LastUserStyle" varchar(255),
	"LastForumAccess"  timestamp,
	"LastTopicAccess"   timestamp,
    "TopicTags" text,  
	"TopicImage" varchar(255),
	"TopicImageType" varchar(50),
	"TopicImageBin" bytea,
	"HasAttachments" integer,
	"TotalRows" integer,
	"PageIndex" integer	  	     
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_unanswered_rt AS (  
	"ForumID" integer,
	"TopicID" integer,
	"TopicMovedID" integer,
	"Posted" timestamp,
	"LinkTopicID" integer,	
	"Subject" varchar(255),
	"Description" varchar(255),
	"Status"  varchar(255),
	"Styles"  varchar(255),
	"UserID" integer,    
	"Starter" varchar(255),
	"StarterDisplay" varchar(255),
	"NumPostsDeleted" integer,  
	"Replies" integer,   
	"Views" integer,
	"LastPosted" timestamp,   
	"LastUserID" integer,
	"LastUserName" varchar(255),
	"LastUserDisplayName" varchar(255),
	"LastMessageID" integer,
	"LastMessageFlags" integer,
	"LastTopicID" integer,
	"TopicFlags" integer,
	"FavoriteCount" integer,
	"Priority" smallint,
	"PollID" integer,
	"ForumName" varchar(255),	
	"ForumFlags" integer,
	"FirstMessage"  text,
	"StarterStyle" varchar(255),
	"LastUserStyle" varchar(255),
	"LastForumAccess"  timestamp,
	"LastTopicAccess"   timestamp,  
	"TopicTags" text,
	"TopicImage" varchar(255),
	"TopicImageType" varchar(50),
	"TopicImageBin" bytea,
	"HasAttachments" integer,
	"TotalRows" integer,
	"PageIndex" integer	  	     
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_listmessages_return_type AS (
  "MessageID" integer,
	"TopicID" integer,
	"ReplyTo" integer,
	"Position" integer,
	"Indent" integer,
	"UserID" integer,
	"UserName" varchar(128),
	"UserDisplayName" varchar(128),
	"Posted" timestamp,
	"Message" text,
	"IP" varchar(39),
	"Edited" timestamp,
	"Flags" integer,
	"EditReason" varchar(100),
	"IsModeratorChanged" boolean,
	"DeleteReason" varchar(100),
	"IsDeleted" boolean,
	"IsApproved" boolean,
	"BlogPostID" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_prune_return_type AS
("COUNT" integer);
--GO

select {databaseSchema}.{objectQualifier}drop_type('{databaseSchema}','{objectQualifier}topic_save_return_type');
--GO
CREATE TYPE {databaseSchema}.{objectQualifier}topic_save_return_type AS
(
 "TopicID" integer,
 "MessageID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_simplelist_return_type AS
(
"TopicID" integer,
"Topic" varchar(128)
);
--GO

CREATE TYPE  {databaseSchema}.{objectQualifier}user_accessmasks_by_type AS
(
"AccessMaskID" integer,
"AccessMaskName" varchar(128),
"AccessMaskFlags" integer,
"IsUserMask" boolean,
"ForumID" integer,
"ForumName" varchar(128),
"CategoryID" integer,
"ParentID" integer,
"GroupID" integer,
"GroupName" varchar(255)
);
--GO

CREATE TYPE  {databaseSchema}.{objectQualifier}user_accessmasks_return_type AS
(
"AccessMaskID" integer,
"AccessMaskName" varchar(128),
"ForumID" integer,
"ForumName" varchar(128),
"CategoryID" integer,
"ParentID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_activity_rank_return_type AS
(
"ID" integer,
"Name" varchar(255),
"DisplayName" varchar(255),
"Joined" timestamp,
"UserStyle" varchar(255),
"IsHidden" boolean,
"NumOfPosts" integer,
"NumOfAllIntervalPosts" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_adminsave_return_type AS
(
"UserID" integer
);
--GO


CREATE TYPE {databaseSchema}.{objectQualifier}user_aspnet_return_type AS
(
"UserID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_avatarimage_return_type AS 
(
"UserID" integer,
"AvatarImage" bytea,
"AvatarImageType" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_changepassword_return_type AS
(
"Success" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_emails_return_type AS
(
"Email" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_find_return_type AS
(
  -- {databaseSchema}.{objectQualifier}user_table_type
  "UserID" integer,
  "BoardID" integer,
  "ProviderUserKey" varchar(36),
  "Name" varchar(128),
  "Password" varchar(32),
  "Email" varchar(128),
  "Joined" timestamp,
  "LastVisit" timestamp,
  "IP" varchar(39),
  "NumPosts" integer,
  "TimeZone" integer,
  "Avatar" varchar(255),
  "Signature" text,
  "AvatarImage" bytea,
  "AvatarImageType" varchar(128),
  "RankID" integer,  
  "Suspended" timestamp,
  "LanguageFile" varchar(128),
  "ThemeFile" varchar(128),
  "OverrideDefaultThemes" boolean,
  "PMNotification" boolean,
  "Flags" integer,
  "Points" integer,
  "AutoWatchTopics" boolean,
  "DisplayName" varchar(128),
  "Culture" varchar(10), 
  "CultureUser" varchar(10),  
  "DailyDigest" boolean,
  "NotificationType" integer,
  "TextEditor" varchar(50),
  "UseSingleSignOn" boolean,
  "IsApproved" boolean,
  "IsActiveExcluded" boolean, 
  "IsGuest" boolean,
  "IsAdmin" boolean 
);
--GO


CREATE TYPE {databaseSchema}.{objectQualifier}user_get_return_type AS
(
"UserID" integer
);
--GO


CREATE TYPE {databaseSchema}.{objectQualifier}user_getpoints_return_type AS
(
"Points" integer
);
--GO


CREATE TYPE {databaseSchema}.{objectQualifier}user_getsignature_return_type AS
(
"Signature" text
);
--GO

SELECT {databaseSchema}.{objectQualifier}drop_type('{databaseSchema}','{objectQualifier}user_list_return_type');
--GO
CREATE TYPE {databaseSchema}.{objectQualifier}user_list_return_type AS
(
  -- {databaseSchema}.{objectQualifier}user_table_type 
  "UserID" integer,
  "BoardID" integer,
  "ProviderUserKey" varchar(36),
  "Name" varchar(128),
  "Password" varchar(32),
  "Email" varchar(128),
  "Joined" timestamp,
  "LastVisit" timestamp,
  "IP" varchar(39),
  "NumPosts" integer,
  "TimeZone" integer,
  "Avatar" varchar(255),
  "Signature" text,
  "AvatarImage" bytea,
  "AvatarImageType" varchar(128),
  "RankID" integer,  
  "Suspended" timestamp,
  "LanguageFile" varchar(128),
  "ThemeFile" varchar(128),
  "OverrideDefaultThemes" boolean,
  "PMNotification" boolean,
  "Flags" integer,
  "Points" integer,
  "AutoWatchTopics" boolean,
  "DisplayName" varchar(128),
  "CultureUser" varchar(10), 
  "Culture" varchar(10), 
  "DailyDigest" boolean,
  "NotificationType" integer,
  "IsFacebookUser" boolean,
  "IsTwitterUser" boolean, 
  "TextEditor" varchar(50),
  "UseSingleSignOn" boolean,
  "IsApproved" boolean,
  "IsActiveExcluded" boolean,
  "TopicsPerPage" integer,
  "PostsPerPage" integer, 
  "RankName" varchar(128),
  "Style"  varchar(255),
  "NumDays" integer,
  "NumPostsForum" integer,
  "HasAvatarImage" int2,  
  "IsGuest" boolean,
  "IsHostAdmin" integer,
  "IsAdmin" integer,
  "IsForumModerator" boolean, 
  "IsModerator" boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}admin_list_rt AS
(
  -- {databaseSchema}.{objectQualifier}user_table_type	
  "UserID" integer,
  "BoardID" integer,
  "BoardName" varchar(128),
  "ProviderUserKey" uuid,
  "Name" varchar(128),
  "Password" varchar(32),
  "Email" varchar(128),
  "Joined" timestamp,
  "LastVisit" timestamp,
  "IP" varchar(39),
  "NumPosts" integer,
  "TimeZone" integer,
  "Avatar" varchar(255),
  "Signature" text,
  "AvatarImage" bytea,
  "AvatarImageType" varchar(128),
  "RankID" integer,  
  "Suspended" timestamp,
  "LanguageFile" varchar(128),
  "ThemeFile" varchar(128),
  "OverrideDefaultThemes" boolean,
  "PMNotification" boolean,
  "Flags" integer,
  "Points" integer,
  "AutoWatchTopics" boolean,
  "DisplayName" varchar(128),
  "CultureUser" varchar(10),  
  "DailyDigest" boolean,
  "NotificationType" integer,
  "IsFacebookUser" boolean,
  "IsTwitterUser" boolean,
  "TextEditor" varchar(50),
  "UseSingleSignOn" boolean,
  "RankName"  varchar(50),
  "Style"  varchar(255),
  "NumDays" integer,
  "NumPostsForum" integer,
  "HasAvatarImage" int2,  
  "IsAdmin" boolean,
  "IsHostAdmin" boolean
  /* ,
  "IsForumModerator" boolean, 
  "IsModerator" boolean */
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}admin_pageaccess_rt AS
(
  -- {databaseSchema}.{objectQualifier}user_table_type	
  "UserID" integer,
  "BoardID" integer,
  "BoardName" varchar(128), 
  "Name" varchar(128),  
  "DisplayName" varchar(128),
  "CultureUser" varchar(10),  
  "Style"  varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_listmembers_return_type AS
(
 --  {databaseSchema}.{objectQualifier}user_table_type 
 "UserID" integer,
  "BoardID" integer,
  "ProviderUserKey" uuid,
  "Name" varchar(128),
  "Password" varchar(32),
  "Email" varchar(128),
  "Joined" timestamp,
  "LastVisit" timestamp,
  "IP" varchar(39),
  "NumPosts" integer,
  "TimeZone" integer,
  "Avatar" varchar(255),
  "Signature" text,
  "AvatarImage" bytea,
  "AvatarImageType" varchar(128),
  "RankID" integer,  
  "Suspended" timestamp,
  "LanguageFile" varchar(128),
  "ThemeFile" varchar(128),
  "OverrideDefaultThemes" boolean,
  "PMNotification" boolean,
  "Flags" integer,
  "Points" integer,
  "AutoWatchTopics" boolean,
  "DisplayName" varchar(128),
  "CultureUser" varchar(10),  
  "DailyDigest" boolean,
  "NotificationType" integer,
  "TextEditor" varchar(50),
  "UseSingleSignOn" boolean,
  "IsApproved" boolean,
  "IsActiveExcluded" boolean,
  "RankName" varchar(128),
  "Style"  varchar(255),
  "TotalCount" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_listmedals_return_type AS
(
"MedalID" integer,
"Name" varchar(128),
"Message" text,
"MedalURL" varchar(250),
"RibbonURL" varchar(250),
"SmallMedalURL" varchar(250),
"SmallRibbonURL" varchar(250),
"SmallMedalWidth" integer,
"SmallMedalHeight" integer,
"SmallRibbonWidth" integer,
"SmallRibbonHeight" integer,
"SortOrder" integer,
"Hide" boolean,
"OnlyRibbon" boolean,
"Flags" integer,
"DateAwarded" timestamp 
);
--GO    

CREATE TYPE {databaseSchema}.{objectQualifier}user_login_return_type AS
(
"UserID" integer
);
--GO

SELECT {databaseSchema}.{objectQualifier}drop_type('{databaseSchema}','{objectQualifier}user_medal_list_return_type');
--GO
CREATE TYPE {databaseSchema}.{objectQualifier}user_medal_list_return_type AS
(
"MedalID" integer,
"Name" varchar(128),
"MedalURL" varchar(250),
"RibbonURL" varchar(250),
"SmallMedalURL" varchar(250),
"SmallRibbonURL" varchar(250),
"SmallMedalWidth" integer,
"SmallMedalHeight" integer,
"SmallRibbonWidth" integer,
"SmallRibbonHeight" integer,
"SortOrder" integer,
"Flags" integer,
"UserName" varchar(128),
"DisplayName" varchar(128),
"UserID" integer,
"Message" text,
"MessageEx" text,
"Hide" boolean,
"OnlyRibbon" boolean,
"CurrentSortOrder" smallint,
"DateAwarded" timestamp 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_pmcount_return_type AS
(
"NumberIn" integer,
"NumberOut" integer,
"NumberArchived" integer,
"NumberTotal" integer,
"NumberAllowed" integer
);
--GO


CREATE TYPE {databaseSchema}.{objectQualifier}user_recoverpassword_return_type AS
(
"UserID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_simplelist_return_type AS
(
"UserID" integer,
"Name"  varchar(128),
"DisplayName"  varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}userforum_list_return_type AS
(
  -- {databaseSchema}.{objectQualifier}user_table_type
  "UserID" integer,
  "BoardID" integer,
  "ProviderUserKey" uuid,
  "Name" varchar(128),
  "Password" varchar(32),
  "Email" varchar(128),
  "Joined" timestamp,
  "LastVisit" timestamp,
  "IP" varchar(39),
  "NumPosts" integer,
  "TimeZone" integer,
  "Avatar" varchar(255),
  "Signature" text,
  "AvatarImage" bytea,
  "AvatarImageType" varchar(128),
  "RankID" integer,  
  "Suspended" timestamp,
  "LanguageFile" varchar(128),
  "ThemeFile" varchar(128),
  "OverrideDefaultThemes" boolean,
  "PMNotification" boolean,
  "Flags" integer,
  "Points" integer,
  "AutoWatchTopics" boolean,
  "DisplayName" varchar(128),
  "CultureUser" varchar(10),  
  "DailyDigest" boolean,
  "NotificationType" integer,
  "TextEditor" varchar(50),
  "UseSingleSignOn" boolean,
  "IsApproved" boolean,
  "IsActiveExcluded" boolean,
  "AccessMaskID" integer,  
  "Accepted" boolean,
  "Access" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}usergroup_list_return_type AS
(
"GroupID" integer,
"Name" varchar(128),
"Style"  varchar(255)
);
--GO

SELECT {databaseSchema}.{objectQualifier}drop_type('{databaseSchema}','{objectQualifier}userpmessage_list_return_type');
--GO
CREATE TYPE {databaseSchema}.{objectQualifier}userpmessage_list_return_type AS
(
"PMessageID" integer,
"ReplyTo" boolean,
"FromUserID" integer,
"Created" timestamp,
"Subject" varchar(100),
"Body" text,
"Flags" integer,
"FromUser" varchar(128),
"ToUserID" integer,
"ToUser" varchar(128),
"IsRead" boolean,
"IsReply" boolean,
"UserPMessageID" integer
);
--GO


CREATE TYPE {databaseSchema}.{objectQualifier}watchforum_check_return_type AS
(
"WatchForumID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}watchforum_list_return_type AS
(
"WatchForumID" integer,
"ForumID" integer,
"UserID" integer,
"Created" timestamp,
"LastMail" timestamp,
"ForumName" varchar(128),
"Messages" integer,
"Topics" integer,
"LastPosted" timestamp,
"LastMessageID"  integer,
"LastTopicID" integer,
"LastUserID" integer,
"LastUserName" varchar(128),
"LastUserDisplayName" varchar(128)
);
--GO


CREATE TYPE {databaseSchema}.{objectQualifier}watchtopic_check_return_type AS
(
"WatchTopicID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}watchtopic_list_return_type AS
(
"WatchTopicID" integer,
"TopicID" integer,
"UserID" integer,
"Created" timestamp,
"LastMail" timestamp,
"TopicName" varchar(128),
"Replies" integer,
"Views" integer,
"LastPosted" timestamp,
"LastMessageID" integer,
"LastUserID" integer,
"LastUserName" varchar(128),
"LastUserDisplayName" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}pageload_return_type AS 
(
"ActiveUpdate" bool,
"PreviousVisit" timestamp,
"UserID" integer,
"ForumID" integer,
"IsAdmin" boolean,
"IsGuest" boolean,
"IsForumModerator" boolean,
"IsModerator"  boolean,
"LastActive" timestamp,
"ReadAccess" boolean,			
"PostAccess" boolean,
"ReplyAccess" boolean,
"PriorityAccess" boolean,
"PollAccess" boolean,
"VoteAccess" boolean,
"ModeratorAccess" boolean,
"EditAccess" boolean,
"DeleteAccess" boolean,
"UploadAccess" boolean,		
"DownloadAccess" boolean,
"UserForumAccess" boolean,
"IsCrawler" boolean,
"IsMobileDevice" boolean,		
"CategoryID" integer,
"CategoryName" varchar(128),
"ForumName" varchar(128),
"ForumTheme" varchar(100),
"TopicID" integer,
"TopicName" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_lazydata_return_type AS 
(
"ProviderUserKey" varchar(64),
"UserFlags" integer,
"UserName" varchar(128),
"DisplayName" varchar(128),
"Suspended" timestamp,
"ThemeFile" varchar(128),
"LanguageFile" varchar(128),
"UseSingleSignOn" bool,
"IsDirty" bool,
"TextEditor" varchar(15),
"TimeZoneUser" integer,
"CultureUser" varchar(10),
"IsFacebookUser" boolean,
"IsTwitterUser" boolean,
"IsGuest" boolean,
"MailsPending" integer,
"UnreadPrivate" integer,
"LastUnreadPm"  timestamp,
"PendingBuddies" integer,
"LastPendingBuddies"   timestamp,
"UserStyle"  varchar(255),
"NumAlbums" integer,
"UsrAlbums"  integer,
"UserHasBuddies" boolean,
"BoardVoteAccess" boolean,
"Reputation" integer,
"PersonalForumsNumber" integer,
"PersonalAccessMasksNumber" integer,
"PersonalGroupsNumber" integer,
"UsrPersonalGroups" integer,
"UsrPersonalMasks" integer,
"UsrPersonalForums" integer,
"CommonViewType" integer,
"TopicsPerPage" integer,
"PostsPerPage" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}rsstopic_list_return_type AS
(
"Topic" varchar(128),
"TopicID" integer, 
"Name" varchar(128), 
"LastPosted" timestamp,
"LastUserID" integer,
"LastMessageID" integer,
"LastMessageFlags" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}last_posted_return_type AS
(
 lasttopicid integer,
 lastposted timestamp 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}vaccess_combo_return_type AS
(
   "UserID" integer,
   "ForumID" integer,
   "IsAdmin" integer,
   "IsForumModerator" integer,
   "IsModerator"  integer,
   "ReadAccess" integer,			
   "PostAccess" integer,
   "ReplyAccess" integer,
   "PriorityAccess" integer,
   "PollAccess" integer,
   "VoteAccess" integer,
   "ModeratorAccess" integer,
   "EditAccess" integer,
   "DeleteAccess" integer,
   "UploadAccess" integer,		
   "DownloadAccess" integer,
   "UserForumAccess" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}ignoreuser_return_type AS
(
"UserID" integer,
"IgnoredUserID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}shoutbox_getmessages_return_type AS
(
"ShoutBoxMessageID" integer,	
"UserID" integer,
"UserName" varchar(255),
"UserDisplayName" varchar(255),
"Message" text,
"Date" timestamp,
"Style"  varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_getthanks_return_type AS
(		
"UserID" integer, 
"ThanksDate" timestamp, 
"Name"  varchar(128),
"DisplayName"  varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_getthanksto_return_type AS
(		
thankstonumber integer, 
thankstopostsnumber integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}pageload_guests_type AS
(		
ici_userid integer,
ici_rowcount integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}group_rank_style_type AS
(
	"LegendID" integer,
	"Name" varchar(128),
	"Style" varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_listreporters_return_type AS
(
	"UserID" integer,
	"UserName" varchar(128),
	"UserDisplayName" varchar(128),
	"ReportedNumber" integer,
	"ReportText" text
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_getallthanks_return_type AS
(
	"FromUserID" integer,
	"ThanksDate" timestamp,
	"MessageID" integer,
	"ToUserID" integer,
		"ThanksFromUserNumber" integer,
		"ThanksToUserNumber" integer,
		"ThanksToUserPostsNumber" integer  
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_favorite_list_return_type AS
(
	"TopicID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_favorite_details_return_type AS
(
	"ForumID" integer,
	"TopicID" integer,
	"TopicMovedID" integer,
	"Posted"  timestamp,
	"LinkTopicID" integer,
	"Subject" varchar(255),
	"Status" varchar(255),
	"Styles" varchar(255),
	"Description" varchar(255),
	"UserID" integer,
	"Starter"  varchar(255),
	"StarterDisplay"  varchar(255),
	"NumPostsDeleted" integer,
	"Replies" integer,
	"Views" integer,
	"LastPosted"  timestamp,
	"LastUserID" integer,
	"LastUserName"  varchar(255),
	"LastUserDisplayName"  varchar(255),
	"LastMessageID"  integer,
	"LastMessageFlags"  integer,
	"LastTopicID" integer,
	"TopicFlags" integer,
	"FavoriteCount" integer,
	"Priority" integer,
	"PollID" integer,
	"ForumName"  varchar(128),	
	"ForumFlags" integer,
	"FirstMessage" text,
	"StarterStyle"   varchar(255),
	"LastUserStyle"   varchar(255),
	"LastForumAccess"   timestamp,
	"LastTopicAccess"   timestamp,
    "TopicTags" text,  
    "TopicImage" varchar(255),
    "TopicImageType" varchar(50),
    "TopicImageBin" bytea,
    "HasAttachments" integer,
	"TotalRows" integer,
	"PageIndex" integer		
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}buddy_addrequest_rt AS
(
    "i_approved" boolean,
	"i_paramoutput" varchar(255)
	
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}buddy_list_return_type AS
(
	"UserID" integer,
	"BoardID" integer,
	"Name" varchar(255),
	"DisplayName" varchar(255),
	"Joined" timestamp,
	"NumPosts" integer,
	"RankName" varchar(128),
	"Approved" boolean,
	"FromUserID" integer,
	"Requested" timestamp         
	
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}buddy_remove_return_type AS
(
"i_paramoutput" varchar(128)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}album_list_return_type AS
(
"AlbumID" integer,
"UserID" integer,
"Title" varchar(255),
"CoverImageID" integer,
"Updated"  timestamp 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}album_getstats_return_type AS
(
"AlbumNumber" integer,
"ImageNumber" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}album_image_list_return_type AS
(
 "ImageID" integer,
 "AlbumID" integer,
 "Caption" varchar(255),	
 "FileName"  varchar(255),
 "Bytes" integer,
 "ContentType"  varchar(50),
 "Uploaded" timestamp,
 "Downloads" integer,
 "UserID" integer
 );
 --GO

CREATE TYPE {databaseSchema}.{objectQualifier}album_image_list_return_type2 AS
(
 "ImageID" integer,
 "AlbumID" integer,
 "Caption" varchar(255),	
 "FileName"  varchar(255),
 "Bytes" integer,
 "ContentType"  varchar(50),
 "Uploaded" timestamp,
 "Downloads" integer
 );
 --GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_getsignaturedata_return_type AS
(
 "UsrSigChars" integer,
 "UsrSigBBCodes" varchar(255),	
 "UsrSigHTMLTags"  varchar(255)
 );
 --GO 

CREATE TYPE {databaseSchema}.{objectQualifier}user_getalbumsdata_return_type AS
(
 "NumAlbums" integer,
 "NumImages" integer,
 "UsrAlbums" integer,	
 "UsrAlbumImages"  integer
 );
 --GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_secdata_return_type AS
(
 "MessageID" integer,
 "UserID" integer,
 "Name" varchar(128),
 "UserDisplayName" varchar(255),
 "Message" text,
 "Posted" timestamp,
 "TopicID" integer,
 "ForumID" integer,	
 "Topic"   varchar(128),
 "Priority" integer,
 "Flags" integer,
 "Edited" timestamp,
 "TopicFlags" integer,
 "EditReason"   varchar(128),
 "Position" integer,
 "IsModeratorChanged" boolean,
 "DeleteReason"   varchar(128),
 "BlogPostID" integer,
 "PollID" integer,
 "IP"   varchar(39),
 "EditedBy" integer
 );
 --GO

CREATE TYPE {databaseSchema}.{objectQualifier}messagehistory_list_return_type AS
(
  "MessageID"		integer,
  "Message"			text,
  "IP"				varchar (15),
  "Edited"			timestamp,  	
  "EditReason"      varchar(128),
  "IsModeratorChanged"  boolean,
  "Flags" integer,  
  "EditedBy"		integer,
  "UserID" integer, 
  "UserName" varchar(255), 
  "UserDisplayName" varchar(255), 
  "ForumID" integer,  
  "TopicID" integer, 
  "Topic" text,  
  "Posted" timestamp 
);
--GO
 
	CREATE TYPE {databaseSchema}.{objectQualifier}user_viewallthanks_return_type AS
(
  "ThanksFromUserID" integer,
  "ThanksToUserID"	 integer,
  "MessageID"		 integer,
  "ForumID"		     integer,
  "TopicID"		     integer,
  "Topic"		     varchar(128),
  "UserID"		     integer, 
  "Posted"		     timestamp,  	
  "Message"          text, 
  "Flags"            integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_viewthankstofrom_return_type AS
( 
  "ThanksToUserID"	 integer,
  "MessageID"		 integer,
  "ForumID"		     integer,
  "TopicID"		     integer,
  "Topic"		     varchar(255),
  "UserID"		     integer, 
  "DisplayName"      varchar(255),
  "UserName"         varchar(255),
  "Posted"		     timestamp,  	
  "Message"          text, 
  "Flags"            integer,
  "TotalRows"        integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}message_gettextbyids_return_type AS
(
"MessageID"  integer,
"Message" text
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}recent_users_rt AS
(
"UserID"  integer,
"UserName" varchar(255),
"UserDisplayName"  varchar(255),
"LastVisit" timestamp,
"IsCrawler" integer,
"UserCount" integer,
"IsHidden" smallint,
"Style"  varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_listread_tmp AS
(
"UserID"  integer,
"ForumID" integer,
"ParentID" integer,
"ReadAccess" smallint
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topicstatus_list_return_type AS
(
"TopicStatusID"  integer,
"TopicStatusName" varchar(255),
"BoardID" integer,
"DefaultDescription" varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}dbinfo_usertype_fields_info_rt AS
(
"ColumnName"  varchar(255),
"ColumnType" varchar(255),
"NotNull" boolean ,
"HasDefinition" boolean,
"Npacl" varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}album_images_by_user_rt AS
(
"ImageID" integer,
"AlbumID" integer,
"Caption" varchar(255),	
"FileName" varchar(255),
"Bytes" integer,
"ContentType" varchar(50),
"Uploaded" timestamp,
"Downloads" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_ns_getsubtree_rt AS
(
"ForumID"  integer,
"ParentID" integer,
"Level" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_ns_getch_accgroup_rt AS
(
"BoardID" integer,
"BoardName" varchar(255),
"CategoryID" integer,
"CategoryName" varchar(255),
"Title" varchar(255),
"ForumID"  integer,
"AccessMaskID" integer,
"ParentID" integer,
"Level" integer,
"HasChildren"  boolean
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_ns_getchildren_rt AS
(
"BoardID" integer,
"BoardName" varchar(255),
"CategoryID"  integer,
"CategoryName" varchar(255),
"Title" varchar(255),
"ForumID"  integer,
"ParentID" integer,
"Level" integer,
"HasChildren" boolean 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_ns_getch_user_rt AS
(
"BoardID" integer,
"BoardName" varchar(255),
"CategoryID"  integer,
"CategoryName" varchar(255),
"Title" varchar(255),
"NoAccess" boolean,
"ForumID"  integer,
"ParentID" integer,
"Level" integer,
"HasChildren" boolean 
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_cataccess_actuser_rt AS
(
"CategoryID"  integer,
"CategoryName" varchar(255),
"SortOrder" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}adminpageaccess_list_rt AS
(
"UserID" integer,
"PageName" 	varchar(255),
"UserName"  varchar(255), 
"UserDisplayName"  varchar(255), 
"BoardName"   varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}group_eventlogaccesslist_rt AS
(
"GroupID" integer,
"BoardID" integer,
"Name" varchar(128),
"Flags" integer,
"PMLimit" integer,
"Style" varchar(255),
"SortOrder" integer,
"Description" varchar(128),
"UsrSigChars" integer,
"UsrSigBBCodes" varchar(255),
"UsrSigHTMLTags"  varchar(255),
"UsrAlbums" integer,
"UsrAlbumImages" integer,
"BoardName"  varchar(255)

);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}topic_tags_rt AS
(
"TagID" integer,
"Tag" varchar(128),
"TagCount" integer,
"MaxTagCount" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}forum_tags_rt AS
(
"TagID" integer,
"Tag" varchar(128),
"TagCount" integer,
"MaxTagCount" integer,
"TotalCount" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}category_getadjacentforum_rt AS
(
i_approved integer,
i_paramoutput integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}pollgroup_save_rt AS
(
"PollGroupID" integer,
"NewPollGroupID" integer
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}user_listtodaysbirthdays_rt AS
(
"Birthday" timestamp,
"UserID" integer,
"TimeZone" integer,
"UserName" varchar(255),
"UserDisplayName" varchar(255),
"Style" varchar(255)
);
--GO

CREATE TYPE {databaseSchema}.{objectQualifier}digest_topicactivenew_rt AS
(
"ForumName" varchar(255),
				 "Subject" varchar(255),
				 "StartedUserName" varchar(255),
				 "LastUserName"  varchar(255),
				 "LastMessageID" integer,
				 "LastMessage" text,
				 "Replies" integer
);
--GO