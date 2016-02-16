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

-- Source Index: IX_{objectQualifier}P_APPLICATION_Name
EXECUTE BLOCK
AS
BEGIN
-- Source Index: IX_{objectQualifier}P_ROLE_APPLICA
IF (NOT EXISTS (SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME='IX_{objectQualifier}P_APPLICATION_')) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}P_APPLICATION_ ON {objectQualifier}P_APPLICATION(APPLICATIONNAME);';
END
--GO

-- Source Index: IX_{objectQualifier}P_MEMBERSHIP_APPpplicat
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS (SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME='IX_{objectQualifier}P_MEMBERSHIP_APP')) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}P_MEMBERSHIP_APP ON {objectQualifier}P_MEMBERSHIP(APPLICATIONID);';
END
--GO

-- Source Index: IX_{objectQualifier}P_MEMBERSHIP_Email
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS (SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME='IX_{objectQualifier}P_MEMBERSHIP_E')) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}P_MEMBERSHIP_E ON {objectQualifier}P_MEMBERSHIP("EMAIL");';
END
--GO

-- Source Index: IX_{objectQualifier}P_MEMBERSHIP_Username
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS (SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME='IX_{objectQualifier}P_MEMBERSHIP_U')) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}P_MEMBERSHIP_U ON {objectQualifier}P_MEMBERSHIP(USERNAME);';
END
--GO 


-- Source Index: IX_{objectQualifier}P_ROLEMEMBERSHIPip_Role
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS (SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME='IX_{objectQualifier}P_ROLEMEMBERSHIP')) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}P_ROLEMEMBERSHIP ON {objectQualifier}P_ROLEMEMBERSHIP(ROLEID);';
END
--GO

-- Source Index: IX_{objectQualifier}P_ROLEMEMBERSHIPip_User
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS (SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME='IX_{objectQualifier}P_ROLEMEMBERSHIP1')) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}P_ROLEMEMBERSHIP1 ON {objectQualifier}P_ROLEMEMBERSHIP(USERID);';
END
--GO


-- Source Index: IX_{objectQualifier}P_ROLE_APPLICAtionID
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS (SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME='IX_{objectQualifier}P_ROLE_APPLICA')) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}P_ROLE_APPLICA ON {objectQualifier}P_ROLE(APPLICATIONID);';
END
--GO

-- Source Index: IX_{objectQualifier}P_ROLE_NAME
EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS (SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME='IX_{objectQualifier}P_ROLE_NAME')) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}P_ROLE_NAME ON {objectQualifier}P_ROLE(ROLENAME);';
END
--GO