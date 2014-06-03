-- This scripts for MySQL for Yet Another Forum http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team https://github.com/vzrus http://sourceforge.net/projects/yaf-datalayers/
-- They are distributed under terms of GPLv2 only licence as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2006-2012

CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('AccessMask','INT NOT NULL AUTO_INCREMENT','AccessMaskID',null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Active',null,'SessionID', 'BoardID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Attachment','INT NOT NULL AUTO_INCREMENT','AttachmentID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('BannedIP','INT NOT NULL AUTO_INCREMENT','ID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('BBCode','INT NOT NULL AUTO_INCREMENT','BBCodeID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Board','INT NOT NULL AUTO_INCREMENT','BoardID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Buddy','INT NOT NULL AUTO_INCREMENT','ID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Category','INT NOT NULL AUTO_INCREMENT','CategoryID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('CheckEmail','INT NOT NULL AUTO_INCREMENT','CheckEmailID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Choice','INT NOT NULL AUTO_INCREMENT','ChoiceID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('EventLog','INT NOT NULL AUTO_INCREMENT','EventLogID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Extension','INT NOT NULL AUTO_INCREMENT','ExtensionID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('FavoriteTopic','INT NOT NULL AUTO_INCREMENT','ID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Forum','INT NOT NULL AUTO_INCREMENT','ForumID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('ForumAccess',null,'GroupID', 'ForumID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Group','INT NOT NULL AUTO_INCREMENT','GroupID', null);
--GO 
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('IgnoreUser',null,'UserID', 'IgnoredUserID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Mail','INT NOT NULL AUTO_INCREMENT','MailID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Medal','INT NOT NULL AUTO_INCREMENT','MedalID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Message','INT NOT NULL AUTO_INCREMENT','MessageID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('MessageReportedAudit','INT NOT NULL AUTO_INCREMENT','LogID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('NntpForum','INT NOT NULL AUTO_INCREMENT','NntpForumID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('NntpServer','INT NOT NULL AUTO_INCREMENT','NntpServerID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('NntpTopic','INT NOT NULL AUTO_INCREMENT','NntpTopicID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('PMessage','INT NOT NULL AUTO_INCREMENT','PMessageID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Poll','INT NOT NULL AUTO_INCREMENT','PollID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('PollVote','INT NOT NULL AUTO_INCREMENT','PollVoteID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Rank','INT NOT NULL AUTO_INCREMENT','RankID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Registry','INT NOT NULL AUTO_INCREMENT','RegistryID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Replace_Words','INT NOT NULL AUTO_INCREMENT','ID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('ShoutboxMessage','INT NOT NULL AUTO_INCREMENT','ShoutBoxMessageID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Smiley','INT NOT NULL AUTO_INCREMENT','SmileyID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Thanks','INT NOT NULL AUTO_INCREMENT','ThanksID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('Topic','INT NOT NULL AUTO_INCREMENT','TopicID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('User','INT NOT NULL AUTO_INCREMENT','UserID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('UserAlbum','INT NOT NULL AUTO_INCREMENT','AlbumID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('UserAlbumImage','INT NOT NULL AUTO_INCREMENT','ImageID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('UserForum',null,'UserID', 'ForumID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('UserGroup',null,'UserID', 'GroupID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('UserPMessage','INT NOT NULL AUTO_INCREMENT','UserPMessageID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('WatchForum','INT NOT NULL AUTO_INCREMENT','WatchForumID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('WatchTopic','INT NOT NULL AUTO_INCREMENT','WatchTopicID', null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('ActiveAccess',null,'UserID', 'ForumID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('UserProfile',null,'UserID', 'ApplicationName');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('ForumReadTracking',null,'UserID', 'ForumID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('TopicReadTracking',null,'UserID', 'TopicID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('GroupMedal',null,'MedalID', 'GroupID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('UserMedal',null,'MedalID', 'UserID');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('MessageHistory',null,'MessageID', 'Edited');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('MessageReported',null,'MessageID',null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('MessageReportedAudit',null,'LogID',null);
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('AdminPageUserAccess',null,'UserID', 'PageName');
--GO
CALL  {databaseSchema}.{objectQualifier}add_or_check_pkeys('EventLogGroupAccess',null,'GroupID', 'EventTypeID');
--GO

