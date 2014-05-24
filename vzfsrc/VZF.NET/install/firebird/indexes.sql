/* Yet Another Forum.NET Firebird data layer by vzrus
 * Copyright (C) 2006-2014 Vladimir Zakharov
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
BEGIN
-- Source Index: IX_{objectQualifier}CATEGORY_BOARDID
  IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}CATEGORY_BOARDID') AND (I.RDB$RELATION_NAME = '{objectQualifier}CATEGORY')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}CATEGORY_BOARDID ON {objectQualifier}CATEGORY(BOARDID);';
  END
--GO

EXECUTE BLOCK
AS
BEGIN
-- Source Index: IX_{objectQualifier}CATEGORY_NAME
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}CATEGORY_NAME') AND (I.RDB$RELATION_NAME = '{objectQualifier}CATEGORY')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}CATEGORY_NAME ON {objectQualifier}CATEGORY(NAME);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}FORUM_CATEGORYID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}FORUM_CATEGORYID') AND (I.RDB$RELATION_NAME = '{objectQualifier}FORUM')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}FORUM_CATEGORYID ON {objectQualifier}FORUM(CATEGORYID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}FORUM_FLAGS
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}FORUM_FLAGS') AND (I.RDB$RELATION_NAME = '{objectQualifier}FORUM')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}FORUM_FLAGS ON {objectQualifier}FORUM(FLAGS);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}FORUM_PARENTID

 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}FORUM_PARENTID') AND (I.RDB$RELATION_NAME = '{objectQualifier}FORUM')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}FORUM_PARENTID ON {objectQualifier}FORUM(PARENTID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}ForumAccess_ForumID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}FORUMACCESS_FORUM') AND (I.RDB$RELATION_NAME = '{objectQualifier}FORUMACCESS')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}FORUMACCESS_FORUM ON {objectQualifier}FORUMACCESS(FORUMID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}MESSAGE_FLAGS
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}MESSAGE_FLAGS') AND (I.RDB$RELATION_NAME = '{objectQualifier}MESSAGE')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}MESSAGE_FLAGS ON {objectQualifier}MESSAGE(FLAGS);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}MESSAGE_TOPICID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}MESSAGE_TOPICID') AND (I.RDB$RELATION_NAME = '{objectQualifier}MESSAGE')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}MESSAGE_TOPICID ON {objectQualifier}MESSAGE(TOPICID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}MESSAGE_USERID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}MESSAGE_USERID') AND (I.RDB$RELATION_NAME = '{objectQualifier}MESSAGE')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}MESSAGE_USERID ON {objectQualifier}MESSAGE(USERID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}MESSAGE_USERID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}MESSAGE_POSTED_D') AND (I.RDB$RELATION_NAME = '{objectQualifier}MESSAGE')
   )) THEN
  EXECUTE STATEMENT 'CREATE DESC INDEX IX_{objectQualifier}MESSAGE_POSTED_D ON {objectQualifier}MESSAGE(POSTED);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}POLLVOTE_POLLID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}POLLVOTE_POLLID') AND (I.RDB$RELATION_NAME = '{objectQualifier}POLLVOTE')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}POLLVOTE_POLLID ON {objectQualifier}POLLVOTE(POLLID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}POLLVOTE_REMOTEIP
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}POLLVOTE_REMOTEIP') AND (I.RDB$RELATION_NAME = '{objectQualifier}POLLVOTE')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}POLLVOTE_REMOTEIP ON {objectQualifier}POLLVOTE(REMOTEIP);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}POLLVOTE_USERID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}POLLVOTE_USERID') AND (I.RDB$RELATION_NAME = '{objectQualifier}POLLVOTE')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}POLLVOTE_USERID ON {objectQualifier}POLLVOTE(USERID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN
-- Source Index: IX_{objectQualifier}REGISTRY_NAME
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}REGISTRY_NAME') AND (I.RDB$RELATION_NAME = '{objectQualifier}REGISTRY')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}REGISTRY_NAME ON {objectQualifier}REGISTRY(BOARDID,NAME);';
 END
--GO


EXECUTE BLOCK
AS
BEGIN
-- Source Index: IX_{objectQualifier}REPUTATIONVOTE_FU_TU
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}REPUTATIONVOTE_FU_TU') AND (I.RDB$RELATION_NAME = '{objectQualifier}REPUTATIONVOTE')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}REPUTATIONVOTE_FU_TU ON {objectQualifier}REPUTATIONVOTE(REPUTATIONFROMUSERID,REPUTATIONTOUSERID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN


-- Source Index: IX_{objectQualifier}TOPIC_FLAGS
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}TOPIC_FLAGS') AND (I.RDB$RELATION_NAME = '{objectQualifier}TOPIC')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}TOPIC_FLAGS ON {objectQualifier}TOPIC(FLAGS);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}TOPIC_FORUMID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}TOPIC_FORUMID') AND (I.RDB$RELATION_NAME = '{objectQualifier}TOPIC')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}TOPIC_FORUMID ON {objectQualifier}TOPIC(FORUMID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}TOPIC_USERID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}TOPIC_USERID') AND (I.RDB$RELATION_NAME = '{objectQualifier}TOPIC')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}TOPIC_USERID ON {objectQualifier}TOPIC(USERID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN


-- Source Index: IX_{objectQualifier}USER_FLAGS
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}USER_FLAGS') AND (I.RDB$RELATION_NAME = '{objectQualifier}USER')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}USER_FLAGS ON {objectQualifier}USER(FLAGS);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}USER_NAME
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}USER_NAME') AND (I.RDB$RELATION_NAME = '{objectQualifier}USER')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}USER_NAME ON {objectQualifier}USER(NAME);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}User_ProviderUserKey
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}USER_PROVIDERUSER') AND (I.RDB$RELATION_NAME = '{objectQualifier}USER')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}USER_PROVIDERUSER ON {objectQualifier}USER(PROVIDERUSERKEY);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}USERGROUP_USERID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}USERGROUP_USERID') AND (I.RDB$RELATION_NAME = '{objectQualifier}USERGROUP')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}USERGROUP_USERID ON {objectQualifier}USERGROUP(USERID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN

-- Source Index: IX_{objectQualifier}UserPMessage_UserID
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}USERPMESSAGE_USER') AND (I.RDB$RELATION_NAME = '{objectQualifier}USERPMESSAGE')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}USERPMESSAGE_USER ON {objectQualifier}USERPMESSAGE(USERID);';
 END
--GO

-- IX_{objectQualifier}Thanks

EXECUTE BLOCK
AS
BEGIN
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}THANKS_MESSAGEID') AND (I.RDB$RELATION_NAME = '{objectQualifier}THANKS')
   )) THEN
  EXECUTE STATEMENT 'CREATE  INDEX IX_{objectQualifier}THANKS_MESSAGEID ON {objectQualifier}THANKS(MESSAGEID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}THANKS_THANKSFRUSRID') AND (I.RDB$RELATION_NAME = '{objectQualifier}THANKS')
   )) THEN
  EXECUTE STATEMENT 'CREATE  INDEX IX_{objectQualifier}THANKS_THANKSFRUSRID ON {objectQualifier}THANKS(THANKSFROMUSERID);';
 END
--GO

EXECUTE BLOCK
AS
BEGIN
 IF (NOT EXISTS (
    SELECT 1 FROM RDB$INDICES I
    WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}THANKS_THANKSTOUSRID') AND (I.RDB$RELATION_NAME = '{objectQualifier}THANKS')
   )) THEN
  EXECUTE STATEMENT 'CREATE  INDEX IX_{objectQualifier}THANKS_THANKSTOUSRID ON {objectQualifier}THANKS(THANKSTOUSERID);';
 END
--GO



