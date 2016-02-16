/* Yet Another Forum.NET Firebird data layer by vzrus
 * Copyright (C) 2006-2016 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only
 * General class structure is based on MS SQL Server code,
 * created by YAF developers
 *
 * http://www.yetanotherforum.net/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation;version 2 only
 * of the License.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */


 EXECUTE BLOCK
 AS
 DECLARE trName varchar(100);
 BEGIN
 FOR SELECT rdb$trigger_name from rdb$triggers
  where (rdb$system_flag = 0 or rdb$system_flag is null 
 INTO :trName    
 DO    
 EXECUTE STATEMENT 'DROP TRIGGER ' || :trName;    
 END
--GO

EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}ACCESSMASK','TR_AI_{objectQualifier}ACCESS_ACCESSMASKID','AccessMaskID','SEQ_{objectQualifier}ACCESSMASK_ACCESSMASKID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}ATTACHMENT','TR_AI_{objectQualifier}ATTACH_ATTACHMENTID','AttachmentID','SEQ_{objectQualifier}ATTACHMENT_ATTACHMENTID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}BANNEDIP','TR_AI_{objectQualifier}BANNEDIP_ID','ID','SEQ_{objectQualifier}bannedip_ID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}BBCODE','TR_AI_{objectQualifier}BBCODE_BBCODEID','BBCodeID','SEQ_{objectQualifier}BBCODE_BBCODEID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}BOARD','TR_AI_{objectQualifier}BOARD_BOARDID','BoardID','SEQ_{objectQualifier}BOARD_BOARDID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}CATEGORY','TR_AI_{objectQualifier}CATEGORY_CATEGORYID','CategoryID','SEQ_{objectQualifier}CATEGORY_CATEGORYID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}CHECKEMAIL','TR_AI_{objectQualifier}CHEKE_CHECKEMAILID','CheckEmailID','SEQ_{objectQualifier}CHECKEMAIL_CHECKEMAILID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}CHOICE','TR_AI_{objectQualifier}CHOICE_CHOICEID','ChoiceID','SEQ_{objectQualifier}CHOICE_CHOICEID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}EVENTLOG','TR_AI_{objectQualifier}EVENTLOG_EVENTLOGID','EventLogID','SEQ_{objectQualifier}EVENTLOG_EVENTLOGID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}EXTENSION','TR_AI_{objectQualifier}EXTENSION_EXTENSIONID','ExtensionID','SEQ_{objectQualifier}EXTENSION_EXTENSIONID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}FORUM','TR_AI_{objectQualifier}FORUM_FORUMID','ForumID','SEQ_{objectQualifier}FORUM_FORUMID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}GROUP','FK_{objectQualifier}GROUP_GROUPID','GroupID','SEQ_{objectQualifier}GROUP_GROUPID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}MAIL','TR_AI_{objectQualifier}MAIL_MAILID','MailID','SEQ_{objectQualifier}MAIL_MAILID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}MEDAL','TR_AI_{objectQualifier}MEDAL_MEDALID','MedalID','SEQ_{objectQualifier}MEDAL_MEDALID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}MESSAGE','TR_AI_{objectQualifier}MESSAGE_MESSAGEID','MessageID','SEQ_{objectQualifier}MESSAGE_MESSAGEID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}MESSAGEREPORTEDAUDIT','TR_AI_{objectQualifier}MESSAG_LOGID','LogID','SEQ_{objectQualifier}MESSAGE_LOGID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}NNTPFORUM','TR_AI_{objectQualifier}NNTPFORUM_NNTPFORUMID','NntpForumID','SEQ_{objectQualifier}NNTPFORUM_NNTPFORUMID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}NNTPSERVER','TR_AI_{objectQualifier}NNTPSE_NNTPSERVERID','NntpServerID','SEQ_{objectQualifier}NNTPSERVER_NNTPSERVERID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}NNTPTOPIC','TR_AI_{objectQualifier}NNTPTOPIC_NNTPTOPICID','NntpTopicID','SEQ_{objectQualifier}NNTPTOPIC_NNTPTOPICID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}PMESSAGE','TR_AI_{objectQualifier}PMESSAGE_PMESSAGEID','PMessageID','SEQ_{objectQualifier}PMESSAGE_PMESSAGEID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}POLL','TR_AI_{objectQualifier}POLL_POLLID','PollID','SEQ_{objectQualifier}POLL_POLLID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}POLLVOTE','TR_AI_{objectQualifier}POLLVOTE_POLLVOTEID','PollVoteID','SEQ_{objectQualifier}POLLVOTE_POLLVOTEID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}RANK','TR_AI_{objectQualifier}RANK_RANKID','RankID','SEQ_{objectQualifier}RANK_RANKID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}REGISTRY','TR_AI_{objectQualifier}REGISTRY_REGISTRYID','RegistryID','SEQ_{objectQualifier}REGISTRY_REGISTRYID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}REPLACE_WORDS','TR_AI_{objectQualifier}REPLACE_WORDS_ID','ID','SEQ_{objectQualifier}REPLACE_WORDS_ID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}SMILEY','TR_AI_{objectQualifier}SMILEY_SMILEYID','SmileyID','SEQ_{objectQualifier}SMILEY_SMILEYID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}TOPIC','TR_AI_{objectQualifier}TOPIC_TOPICID','TopicID','SEQ_{objectQualifier}TOPIC_TOPICID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}USER','TR_AI_{objectQualifier}USER_USERID','UserID','SEQ_{objectQualifier}USER_USERID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}USERPMESSAGE','TR_AI_{objectQualifier}USERPM_USERPMESSAGEID','UserPMessageID','SEQ_{objectQualifier}USERPME_USERPMESSAGEID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}WATCHFORUM','TR_AI_{objectQualifier}WATCHF_WATCHFORUMID','WatchForumID','SEQ_{objectQualifier}WATCHFORUM_WATCHFORUMID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}WATCHTOPIC','TR_AI_{objectQualifier}WATCHT_WATCHTOPICID','WatchTopicID','SEQ_{objectQualifier}WATCHTOPIC_WATCHTOPICID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}ATTACHMENT','TR_AI_{objectQualifier}SBOXMESG_SBOXMESGID','SHOUTBOXMESSAGEID','SEQ_{objectQualifier}SBOXMESG_SBOXMESGID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}POLL','TR_AI_{objectQualifier}POLL_POLLID','POLLID','SEQ_{objectQualifier}POLL_POLLID','ACTIVE','BEFORE INSERT',0,1);
--GO
EXECUTE PROCEDURE DP_DROP_TRIGGER_PROC('{objectQualifier}TOPICSTATUS','TR_AI_{objectQualifier}TOPSTATUS_TOPSTID','TOPICSTATUSID','SEQ_{objectQualifier}TOPSTATUS_TOPSTID','ACTIVE','BEFORE INSERT',0,1);
--GO

