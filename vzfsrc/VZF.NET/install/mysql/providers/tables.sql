-- This scripts for MySQL for Yet Another Forum http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team https://github.com/vzrus http://sourceforge.net/projects/yaf-datalayers/
-- They are distributed under terms of GPLv2 only licence as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2006-2012

/* PROVIDER TABLE SCRIPT BY VZ_TEAM */

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}prov_Application (
  `ApplicationID` BINARY(16) NOT NULL,
  `ApplicationName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `ApplicationNameLwd` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `Description` LONGTEXT NULL,
  PRIMARY KEY (`ApplicationID`),
  INDEX `IX_{databaseSchema}_{objectQualifier}prov_Application_Name` (`ApplicationName`(165))
)
ENGINE = INNODB;
--GO 
CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}prov_Membership (
  `UserID` BINARY(16) NOT NULL,
  `UserUUID` CHAR(36) NOT NULL,
  `ApplicationID` BINARY(16) NOT NULL,
  `Username` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
  `UsernameLwd` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
  `Password` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `PasswordSalt` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `PasswordFormat` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `Email` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `EmailLwd` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `PasswordQuestion` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `PasswordAnswer` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
  `IsApproved` TINYINT(1) NULL,
  `IsLockedOut` TINYINT(1) NULL,
  `LastLogin` DATETIME NULL,
  `LastActivity` DATETIME NULL,
  `LastPasswordChange` DATETIME NULL,
  `LastLockOut` DATETIME NULL,
  `FailedPasswordAttempts` INT(10) NULL,
  `FailedAnswerAttempts` INT(10) NULL,
  `FailedPasswordWindow` DATETIME NULL,
  `FailedAnswerWindow` DATETIME NULL,
  `Joined` DATETIME NULL,
  `Comment` LONGTEXT NULL,
  PRIMARY KEY (`UserID`),
  INDEX `IX_{databaseSchema}_{objectQualifier}prov_Membership_Applicat` (`ApplicationID`),
  INDEX `IX_{databaseSchema}_{objectQualifier}prov_Membership_Email` (`Email`),
  INDEX `IX_{databaseSchema}_{objectQualifier}prov_Membership_Username` (`Username`)
  )
  ENGINE = INNODB;
  --GO
  
    CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}prov_Role (
  `RoleID` BINARY(16) NOT NULL,
  `ApplicationID` BINARY(16) NOT NULL,
  `RoleName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
  `RoleNameLwd` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
  PRIMARY KEY (`RoleID`),
  INDEX `IX_{databaseSchema}_{objectQualifier}prov_Role_ApplicationID` (`ApplicationID`),
  INDEX `IX_{databaseSchema}_{objectQualifier}prov_Role_Name` (`RoleName`)
  )
  ENGINE = INNODB;
  CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}prov_RoleMembership (
  `RoleID` BINARY(16) NOT NULL,
  `UserID` BINARY(16) NOT NULL,
  INDEX `IX_{databaseSchema}_{objectQualifier}prov_RoleMembership_Role` (`RoleID`),
  INDEX `IX_{databaseSchema}_{objectQualifier}prov_RoleMembership_User` (`UserID`)
  )
  ENGINE = INNODB; 
   --GO
  CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}prov_Profile (
  `UserID` BINARY(16) NOT NULL,
  `LastUpdatedDate` DATETIME NOT NULL,
  `valueindex` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
  `stringdata` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
  `binarydata` LONGBLOB,  
  PRIMARY KEY (`UserID`)
  )
  ENGINE = INNODB;   
--GO
  
/* PROVIDER TABLE SCRIPT BY VZ_TEAM */
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}tmp_install;
--GO
 
CREATE PROCEDURE {databaseSchema}.{objectQualifier}add_or_change_prov_columns()
BEGIN

/*IF (SELECT 1 table_name FROM INFORMATION_SCHEMA.TABLES
  WHERE table_schema = {databaseSchema}
  AND table_name LIKE '{databaseSchema}.{objectQualifier}prov_Membership') <=0 AND (SELECT *
  FROM INFORMATION_SCHEMA.SCHEMA_PRIVILEGES
  WHERE  GRANTEE LIKE '%mojouser%' AND TABLE_SCHEMA='mojoportal' AND PRIVILEGE_TYPE = 'SELECT' AND IS_GRANTABLE='YES') >=0 THEN*/
   
  -- Mysql standard provider compatability columns added on 29.07.09

IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE LOWER(TABLE_SCHEMA)=LOWER('{databaseSchema}')  
  AND LOWER(TABLE_NAME)=LOWER('{objectQualifier}prov_Membership')
  AND LOWER(COLUMN_NAME)=LOWER('UserUUID')) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}prov_Membership ADD `UserUUID` CHAR(38) AFTER `UserID`;
  END IF;
  

IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE LOWER(TABLE_SCHEMA)=LOWER('{databaseSchema}')  AND
  (TABLE_NAME='{objectQualifier}prov_Profile' OR TABLE_NAME=LOWER('{objectQualifier}prov_Profile')) 
  AND COLUMN_NAME='valueindex') THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}prov_Profile ADD `valueindex` LONGTEXT AFTER `UserID`;
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE LOWER(TABLE_SCHEMA)=LOWER('{databaseSchema}')  AND 
  (TABLE_NAME='{objectQualifier}prov_Profile' OR TABLE_NAME=LOWER('{objectQualifier}prov_Profile'))
  AND COLUMN_NAME='stringdata') THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}prov_Profile ADD `stringdata` LONGTEXT AFTER `valueindex`;
  END IF;
     
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE LOWER(TABLE_SCHEMA)=LOWER('{databaseSchema}')  AND
  (TABLE_NAME='{objectQualifier}prov_Profile' OR TABLE_NAME=LOWER('{objectQualifier}prov_Profile'))
  AND COLUMN_NAME='binarydata') THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}prov_Profile ADD `binarydata` LONGBLOB AFTER `stringdata`;
  END IF;
  
 IF  EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE LOWER(TABLE_SCHEMA)=LOWER('{databaseSchema}')  AND 
  (TABLE_NAME='{objectQualifier}prov_Profile' OR TABLE_NAME=LOWER('{objectQualifier}prov_Profile'))
  AND LOWER(COLUMN_NAME)=LOWER('LastUpdatedDate')) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}prov_Profile CHANGE `LastUpdatedDate` `LastUpdatedDate` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;
  END IF; 
  END;
  --GO
    
  CALL {databaseSchema}.{objectQualifier}add_or_change_prov_columns();
  --GO
  
  DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}add_or_change_prov_columns;
  --GO 
  
  DROP PROCEDURE  IF EXISTS {databaseSchema}.{objectQualifier}tmp_install;
  --GO 




