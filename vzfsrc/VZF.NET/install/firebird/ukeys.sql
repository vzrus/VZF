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

-- Source unique key: IX_{objectQualifier}BannedIP
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}BANNEDIP' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}BANNEDIP ADD CONSTRAINT IX_{objectQualifier}BANNEDIP UNIQUE (BOARDID,MASK);';
END
--GO
-- Source unique key: IX_{objectQualifier}Category

EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}CATEGORY' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}CATEGORY ADD CONSTRAINT IX_{objectQualifier}CATEGORY UNIQUE (BOARDID,NAME);';
END
--GO

-- Source unique key: IX_{objectQualifier}CheckEmail
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}CHECKEMAIL' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}CHECKEMAIL ADD CONSTRAINT IX_{objectQualifier}CHECKEMAIL UNIQUE ("HASH");';
END
--GO

-- Source unique key: IX_{objectQualifier}Forum
-- TODO ParentID is required to not show empty or no access categories but it fails because of duplicate nulls which are forbidden for uniques
/* EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}FORUM')) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}FORUM ADD CONSTRAINT IX_{objectQualifier}FORUM UNIQUE (CATEGORYID,"NAME");';
END
*/

-- Source unique key: IX_{objectQualifier}Group
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}GROUP' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}GROUP ADD CONSTRAINT IX_{objectQualifier}GROUP UNIQUE (BOARDID,NAME);';
END
--GO

-- Source unique key: IX_{objectQualifier}Rank

EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}RANK' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}RANK ADD CONSTRAINT IX_{objectQualifier}RANK UNIQUE (BOARDID,NAME);';
END
--GO

-- Source unique key: IX_{objectQualifier}Smiley
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}SMILEY' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}SMILEY ADD CONSTRAINT IX_{objectQualifier}SMILEY UNIQUE (BOARDID,"CODE");';
END
--GO

-- Source unique key: IX_{objectQualifier}User
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}USER' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}USER ADD CONSTRAINT IX_{objectQualifier}USER UNIQUE (BOARDID,NAME);';
END
--GO

-- Source unique key: IX_{objectQualifier}WatchForum
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}WATCHFORUM' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}WATCHFORUM ADD CONSTRAINT IX_{objectQualifier}WATCHFORUM UNIQUE (FORUMID,USERID);';
END
--GO

-- Source unique key: IX_{objectQualifier}WatchTopic
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='IX_{objectQualifier}WATCHTOPIC' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}WATCHTOPIC ADD CONSTRAINT IX_{objectQualifier}WATCHTOPIC UNIQUE (TOPICID,USERID);';
END
--GO




