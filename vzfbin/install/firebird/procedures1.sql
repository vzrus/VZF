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

CREATE PROCEDURE {objectQualifier}REGISTRY_SAVE(
                I_NAME    VARCHAR(128),
                I_VALUE   BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
                I_BOARDID INTEGER)
                AS
BEGIN
        
        IF (I_BOARDID IS NULL) THEN
        BEGIN
            IF (EXISTS (SELECT FIRST 1 1
                       FROM   {objectQualifier}REGISTRY
                       WHERE  Lower(NAME) = Lower(:I_NAME))) THEN
            UPDATE {objectQualifier}REGISTRY
            SET "VALUE" = :I_VALUE
            WHERE  Lower(NAME) = Lower(:I_NAME)
            AND BOARDID IS NULL;
            ELSE            
                INSERT INTO {objectQualifier}REGISTRY
                           (REGISTRYID,
                            NAME,
                            "VALUE")
                VALUES     ((SELECT NEXT VALUE FOR SEQ_{objectQualifier}REGISTRY_REGISTRYID FROM RDB$DATABASE),Lower(:I_NAME),
                            :I_VALUE);          
        END
        ELSE
        BEGIN
            IF (EXISTS (SELECT 1
                       FROM   {objectQualifier}REGISTRY
                       WHERE  Lower(NAME) = Lower(:I_NAME)
                       AND BOARDID = :I_BOARDID)) THEN
             begin
            UPDATE {objectQualifier}REGISTRY
            SET    "VALUE" = :I_VALUE
            WHERE  Lower(NAME) = Lower(:I_NAME)
            AND BOARDID = :I_BOARDID;
             END
            ELSE            
                INSERT INTO {objectQualifier}REGISTRY
                           (REGISTRYID,
                            NAME,
                            "VALUE",
                            BOARDID)
                VALUES     ((SELECT NEXT VALUE FOR SEQ_{objectQualifier}REGISTRY_REGISTRYID FROM RDB$DATABASE),
                             Lower(:I_NAME),
                            :I_VALUE,
                            :I_BOARDID);           
        END     
    
    END;
--GO

CREATE  PROCEDURE {objectQualifier}BOARD_SAVE(
                I_BOARDID       INTEGER,
                I_NAME         VARCHAR(128),
                I_LANGUAGEFILE VARCHAR(128),
                I_CULTURE VARCHAR(10),
                I_ALLOWTHREADED BOOL)
                RETURNS (O_BOARDID INTEGER)
 AS
BEGIN
   EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'culture', :I_CULTURE, :I_BOARDID;
   EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'language', :I_LANGUAGEFILE, :I_BOARDID;
       
        UPDATE {objectQualifier}BOARD
        SET    NAME = :I_NAME,
               ALLOWTHREADED = :I_ALLOWTHREADED
        WHERE  BOARDID = :I_BOARDID;
        SELECT :I_BOARDID FROM RDB$DATABASE INTO :O_BOARDID;
        SUSPEND;
END;
--GO


CREATE PROCEDURE {objectQualifier}BOARD_CREATE(
    I_BOARDNAME		VARCHAR(128),
    I_CULTURE VARCHAR(10),
    I_LANGUAGEFILE 	varchar(50),
    I_MEMBERSHIPAPPNAME VARCHAR(128),
    I_ROLESAPPNAME VARCHAR(128),
    I_USERNAME		VARCHAR(128),
    I_USEREMAIL		VARCHAR(128),
    I_USERKEY		VARCHAR(64), 
    I_ISHOSTADMIN	SMALLINT,
    I_ROLEPREFIX    VARCHAR(255),
    I_UTCTIMESTAMP TIMESTAMP
 ) RETURNS ( OUT_BOARDID INTEGER )
 AS 
    DECLARE VARIABLE L_BOARDID			     INTEGER;
    DECLARE VARIABLE L_TIMEZONE			     INTEGER;
    DECLARE VARIABLE L_FORUMEMAIL			 VARCHAR(128);
    DECLARE VARIABLE L_GROUPIDADMIN			 INTEGER;
    DECLARE VARIABLE L_GROUPIDGUEST			 INTEGER;
    DECLARE VARIABLE L_GROUPIDMEMBER		 INTEGER;
    DECLARE VARIABLE L_ACCESSMASKIDADMIN	 INTEGER;
    DECLARE VARIABLE L_ACCESSMASKIDMODERATOR INTEGER;
    DECLARE VARIABLE L_ACCESSMASKIDMEMBER    INTEGER;
    DECLARE VARIABLE L_ACCESSMASKIDREADONLY	 INTEGER;
    DECLARE VARIABLE l_UserIDAdmin			 INTEGER;
    DECLARE VARIABLE l_UserIDGuest			 INTEGER;
    DECLARE VARIABLE l_RankIDAdmin			 INTEGER;
    DECLARE VARIABLE l_RankIDGuest			 INTEGER;
    DECLARE VARIABLE l_RankIDNewbie			 INTEGER;
    DECLARE VARIABLE l_RankIDMember			 INTEGER;
    DECLARE VARIABLE l_RankIDAdvanced		 INTEGER;
    DECLARE VARIABLE l_CategoryID			 INTEGER;
    DECLARE VARIABLE l_ForumID			     INTEGER;
    DECLARE VARIABLE l_UserFlags			 INTEGER;
 BEGIN
    
 
       L_TIMEZONE = (SELECT CAST(CAST("VALUE" as varchar(50)) as INTEGER) FROM   {objectQualifier}REGISTRY
       WHERE LOWER("NAME") = LOWER('TIMEZONE')) ;
        
        SELECT CAST("VALUE" AS CHAR(50))                           			
                           FROM   {objectQualifier}REGISTRY
                           WHERE  Lower(NAME) = 
                           Lower('ForumEmail') INTO :L_FORUMEMAIL;       
 
       SELECT NEXT VALUE FOR SEQ_{objectQualifier}BOARD_BOARDID FROM RDB$DATABASE INTO :L_BOARDID;
       -- SELECT GEN_ID(GEN_{objectQualifier}board_BoardID, 1) INTO L_BOARDID  from RDB$database;
       /*Board SET FOREIGN_KEY_CHECKS =0;*/  
    
        
        INSERT INTO {objectQualifier}BOARD
                   (BOARDID, NAME, ALLOWTHREADED, MEMBERSHIPAPPNAME, ROLESAPPNAME )
        VALUES(:L_BOARDID,:i_BoardName,0, :I_MEMBERSHIPAPPNAME, :I_ROLESAPPNAME);
        /*SET FOREIGN_KEY_CHECKS =1;*/
        
       EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'culture', :I_CULTURE, :L_BOARDID;
       EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'language', :I_LANGUAGEFILE, :L_BOARDID;       
       
        
     /*Rank*/
     SELECT NEXT VALUE FOR SEQ_{objectQualifier}RANK_RANKID FROM RDB$DATABASE INTO :l_RankIDAdmin;
     INSERT INTO {objectQualifier}RANK        
                   (RANKID,
                   BOARDID,
                   NAME,
                   FLAGS,
                   MINPOSTS,
                   PMLIMIT,
                   STYLE,
                   SORTORDER)
        VALUES     (:l_RankIDAdmin,
                    :L_BOARDID,
                    'Administration',
                    0,
                    NULL,
                    0,
                    '',
                    0);
        
        -- SET l_RankIDAdmin = LAST_INSERT_ID();
        SELECT NEXT VALUE FOR SEQ_{objectQualifier}RANK_RANKID FROM RDB$DATABASE INTO :l_RankIDGuest;
        INSERT INTO {objectQualifier}RANK
                   (RANKID,
                    BOARDID,
                    NAME,                   
                    FLAGS,
                    MINPOSTS,
                    PMLIMIT,
                    STYLE,
                    SORTORDER)
        VALUES     (:l_RankIDGuest,
                    :L_BOARDID,
                    'Guest',                   
                    4,
                    NULL,
                    0,
                    '',
                    1);
                    
         SELECT NEXT VALUE FOR SEQ_{objectQualifier}RANK_RANKID FROM RDB$DATABASE INTO :l_RankIDNewbie;                   
         INSERT INTO {objectQualifier}RANK
                   (RANKID,
                    BOARDID,
                    NAME,
                    FLAGS,
                    MINPOSTS,
                    PMLIMIT,
                    STYLE,
                    SORTORDER)
        VALUES     (:l_RankIDNewbie,
                    :L_BOARDID,
                    'Newbie',
                    3,
                    0,
                    10,
                    '',
                    2);
                    
        SELECT NEXT VALUE FOR SEQ_{objectQualifier}RANK_RANKID FROM RDB$DATABASE INTO :l_RankIDMember;       
        INSERT INTO {objectQualifier}RANK
                   (RANKID,
                    BOARDID,
                    NAME,
                    FLAGS,
                    MINPOSTS,
                    PMLIMIT,
                    STYLE,
                    SORTORDER)
        VALUES     (:l_RankIDMember,
                    :L_BOARDID,
                    'Member',
                    2,
                    10,
                    30,
                    '',
                    3);
        
        SELECT NEXT VALUE FOR SEQ_{objectQualifier}RANK_RANKID FROM RDB$DATABASE INTO :l_RankIDAdvanced;       
        INSERT INTO {objectQualifier}RANK
                   (RANKID,
                    BOARDID,
                    NAME,
                    FLAGS,
                    MINPOSTS,
                    PMLIMIT,
                    STYLE,
                    SORTORDER)
        VALUES     (:l_RankIDAdvanced,
                    :L_BOARDID,
                    'Advanced Member',
                    2,
                    30,
                    100,
                    '',
                    4);   
        
 
    /*AccessMask*/
    SELECT NEXT VALUE FOR SEQ_{objectQualifier}ACCESSMASK_ACCESSMASKID FROM RDB$DATABASE INTO :L_ACCESSMASKIDADMIN;       
    INSERT INTO {objectQualifier}ACCESSMASK(ACCESSMASKID,BOARDID,NAME,FLAGS,SORTORDER)
    VALUES(:L_ACCESSMASKIDADMIN,:L_BOARDID,'Admin Access',1023 + 1024,4);
    
    SELECT NEXT VALUE FOR SEQ_{objectQualifier}ACCESSMASK_ACCESSMASKID FROM RDB$DATABASE INTO :L_ACCESSMASKIDMODERATOR;       
    INSERT INTO {objectQualifier}ACCESSMASK(ACCESSMASKID,BOARDID,NAME,FLAGS,SORTORDER)
    VALUES(:L_ACCESSMASKIDMODERATOR,:L_BOARDID,'Moderator Access',487 + 1024,3);
    
    SELECT NEXT VALUE FOR SEQ_{objectQualifier}ACCESSMASK_ACCESSMASKID FROM RDB$DATABASE INTO :L_ACCESSMASKIDMEMBER;       
    INSERT INTO {objectQualifier}ACCESSMASK(ACCESSMASKID,BOARDID,NAME,FLAGS,SORTORDER)
    VALUES(:L_ACCESSMASKIDMEMBER,:L_BOARDID,'Member Access',423 + 1024,2); 	

    SELECT NEXT VALUE FOR SEQ_{objectQualifier}ACCESSMASK_ACCESSMASKID FROM RDB$DATABASE INTO :L_ACCESSMASKIDREADONLY;       
    INSERT INTO {objectQualifier}ACCESSMASK(ACCESSMASKID,BOARDID,NAME,FLAGS,SORTORDER)
    VALUES(:L_ACCESSMASKIDREADONLY,:L_BOARDID,'Read Only Access',1,1);
               
    INSERT INTO {objectQualifier}ACCESSMASK(ACCESSMASKID,BOARDID,NAME,FLAGS,SORTORDER)
    VALUES((SELECT NEXT VALUE FOR SEQ_{objectQualifier}ACCESSMASK_ACCESSMASKID FROM RDB$DATABASE), :L_BOARDID,'No Access',0,0);
    
     /*Group*/
    SELECT NEXT VALUE FOR SEQ_{objectQualifier}GROUP_GROUPID FROM RDB$DATABASE INTO :L_GROUPIDADMIN;       
    INSERT INTO {objectQualifier}GROUP(GROUPID,BOARDID,NAME,FLAGS,PMLIMIT,STYLE,SORTORDER) values(:L_GROUPIDADMIN,:L_BOARDID,(COALESCE(:I_ROLEPREFIX,'') || 'Administrators'),1,2000000,'default!font-size: 8pt; color: red/flatearth!font-size: 8pt; color:blue', 0);
    SELECT NEXT VALUE FOR SEQ_{objectQualifier}GROUP_GROUPID FROM RDB$DATABASE INTO :L_GROUPIDGUEST;       
    INSERT INTO {objectQualifier}GROUP(GROUPID,BOARDID,NAME,FLAGS,PMLIMIT,STYLE,SORTORDER) values(:L_GROUPIDGUEST,:L_BOARDID, 'Guests',2,0,'', 1);
    SELECT NEXT VALUE FOR SEQ_{objectQualifier}GROUP_GROUPID FROM RDB$DATABASE INTO :L_GROUPIDMEMBER; 
    INSERT INTO {objectQualifier}GROUP(GROUPID,BOARDID,NAME,FLAGS,PMLIMIT,STYLE,SORTORDER) values(:L_GROUPIDMEMBER,:L_BOARDID,(COALESCE(:I_ROLEPREFIX,'') || 'Registered'),4, 100,'', 2);
        
     /*User (GUEST)*/
    SELECT NEXT VALUE FOR SEQ_{objectQualifier}USER_USERID FROM RDB$DATABASE INTO :l_UserIDGuest;       
    INSERT INTO {objectQualifier}USER(USERID, BOARDID,PROVIDERUSERKEY,NAME, DISPLAYNAME,"PASSWORD","EMAIL", JOINED,LASTVISIT,NUMPOSTS,TIMEZONE,RANKID,FLAGS)
    VALUES(:l_UserIDGuest,:L_BOARDID,NULL,'Guest','Guest','na', :L_FORUMEMAIL,:I_UTCTIMESTAMP,:I_UTCTIMESTAMP,0,:L_TIMEZONE,:l_RankIDGuest,6);
        
    
    l_UserFlags = 2;
    IF (I_ISHOSTADMIN<>0) THEN l_UserFlags = 3;

  /*User (ADMIN)*/
  SELECT NEXT VALUE FOR SEQ_{objectQualifier}USER_USERID FROM RDB$DATABASE INTO :l_UserIDAdmin;       
  INSERT INTO {objectQualifier}USER(USERID, BOARDID,PROVIDERUSERKEY,NAME, DISPLAYNAME,"PASSWORD","EMAIL", JOINED,LASTVISIT,NUMPOSTS,TIMEZONE,RANKID,FLAGS)
  VALUES(:l_UserIDAdmin,:L_BOARDID,CHAR_TO_UUID(:I_USERKEY),:I_USERNAME,:I_USERNAME,'na',:I_USEREMAIL,:I_UTCTIMESTAMP,:I_UTCTIMESTAMP,0,:L_TIMEZONE,:l_RankIDAdmin,:l_UserFlags);

  -- update all groups that they were created by admin
	update {objectQualifier}GROUP
	set    CreatedByUserID = :l_UserIDAdmin,
           CreatedByUserName = :I_USERNAME,
           CreatedByUserDisplayName = :I_USERNAME,
           CreatedDate = :I_UTCTIMESTAMP;
  /*UserGroup*/
  INSERT INTO {objectQualifier}USERGROUP(USERID,GROUPID) VALUES(:l_UserIDAdmin,:L_GROUPIDADMIN);
  INSERT INTO {objectQualifier}USERGROUP(USERID,GROUPID) VALUES(:l_UserIDGuest,:L_GROUPIDGUEST);

  /*Category*/
  SELECT NEXT VALUE FOR SEQ_{objectQualifier}CATEGORY_CATEGORYID FROM RDB$DATABASE INTO :l_CategoryID;       
  INSERT INTO {objectQualifier}CATEGORY(CATEGORYID,BOARDID,NAME,SORTORDER) VALUES(:l_CategoryID,:L_BOARDID,'Test Category',1);
 
  /*Forum*/
  SELECT NEXT VALUE FOR SEQ_{objectQualifier}FORUM_FORUMID FROM RDB$DATABASE INTO :l_ForumID;       
  INSERT INTO {objectQualifier}FORUM(FORUMID,CATEGORYID,NAME,DESCRIPTION,SORTORDER,NUMTOPICS,NUMPOSTS,FLAGS)
  VALUES(:l_ForumID,:l_CategoryID,'Test Forum','A test forum',1,0,0,4);
  /* ForumAccess */
  INSERT INTO {objectQualifier}FORUMACCESS(GROUPID,FORUMID,ACCESSMASKID) VALUES(:L_GROUPIDADMIN,:l_ForumID,:L_ACCESSMASKIDADMIN);
  INSERT INTO {objectQualifier}FORUMACCESS(GROUPID,FORUMID,ACCESSMASKID) VALUES(:L_GROUPIDGUEST,:l_ForumID,:L_ACCESSMASKIDREADONLY);
  INSERT INTO {objectQualifier}FORUMACCESS(GROUPID,FORUMID,ACCESSMASKID) VALUES(:L_GROUPIDMEMBER,:l_ForumID,:L_ACCESSMASKIDMEMBER);
  SELECT :L_BOARDID FROM RDB$DATABASE INTO :OUT_BOARDID ;
  SUSPEND;
  END;
--GO

CREATE PROCEDURE {objectQualifier}SYSTEM_UPDATEVERSION
(
    I_VERSION		INTEGER,
    I_VERSIONNAME	VARCHAR(255)
) 
AS
BEGIN
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'Version',CAST(:I_VERSION AS VARCHAR(128)),null;
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'VersionName',CAST(:I_VERSIONNAME AS BLOB),null;
END;
--GO


 CREATE PROCEDURE {objectQualifier}SYSTEM_INITIALIZE(
    I_NAME		VARCHAR(128),
    I_TIMEZONE	INTEGER,
    I_CULTURE	VARCHAR(10),
    I_LANGUAGEFILE VARCHAR(50),
    I_FORUMEMAIL	VARCHAR(128),
    I_SMTPSERVER	VARCHAR(128),
    I_USER		VARCHAR(128),
    I_USEREMAIL	VARCHAR(255),
    I_USERKEY	VARCHAR(64), 
    I_ROLEPREFIX VARCHAR(255),	
    I_UTCTIMESTAMP TIMESTAMP)
    AS
    DECLARE VARIABLE ici_tmpValue VARCHAR(128);
    DECLARE VARIABLE ICI_BOARDID INTEGER; 
BEGIN
    
 
     /*initalize required 'registry' settings*/
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'version','1',null;
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'versionname','1.0.0',null;
    ici_tmpValue = CAST(:I_TIMEZONE AS CHAR(100));
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'timezone', :ici_tmpValue,null;
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'smtpserver', :I_SMTPSERVER,null;
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'forumemail', :I_FORUMEMAIL,null;
   
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'culture', :I_CULTURE, null;
    EXECUTE PROCEDURE {objectQualifier}REGISTRY_SAVE 'language', :I_LANGUAGEFILE, null;
     /*initalize new board*/
    EXECUTE PROCEDURE {objectQualifier}BOARD_CREATE :I_NAME, :I_CULTURE, :I_LANGUAGEFILE, null, null, :I_USER, :I_USEREMAIL, :I_USERKEY, 1, :I_ROLEPREFIX, :I_UTCTIMESTAMP
    RETURNING_VALUES :ICI_BOARDID; 
 END;
--GO





CREATE PROCEDURE {objectQualifier}FORUM_RESYNC
    (I_BOARDID INTEGER,I_FORUMID INTEGER)
AS
 DECLARE VARIABLE  ICI_TMP_FORUMID INTEGER;
 DECLARE currForums CURSOR FOR
            (SELECT 
                a.FORUMID
            FROM
                {objectQualifier}FORUM a
                JOIN {objectQualifier}CATEGORY b on a.CATEGORYID=b.CATEGORYID
                JOIN {objectQualifier}BOARD c on b.BOARDID = c.BOARDID  
            WHERE
                c.BOARDID=:I_BOARDID);
                
BEGIN

    IF (I_FORUMID IS NULL) THEN
    BEGIN	
 
        OPEN currForums;
        
         /*cycle through forums*/
                WHILE (1 = 1) DO
     BEGIN
                FETCH currForums INTO ICI_TMP_FORUMID; 
            IF(ROW_COUNT = 0)THEN
      LEAVE;
        /*update statistics*/
            EXECUTE PROCEDURE {objectQualifier}FORUM_UPDATESTATS :ICI_TMP_FORUMID;
            /*update last post*/
            EXECUTE PROCEDURE {objectQualifier}FORUM_UPDATELASTPOST :ICI_TMP_FORUMID;	
 
        SUSPEND;
        END
        CLOSE currForums;
        /*deallocate curForums*/
    END
    ELSE 
    BEGIN		
        /*update statistics*/
        EXECUTE PROCEDURE {objectQualifier}FORUM_UPDATESTATS :I_FORUMID;
        /*update last post*/
        EXECUTE PROCEDURE {objectQualifier}FORUM_UPDATELASTPOST :I_FORUMID;
    END
END;
--GO

CREATE PROCEDURE  {objectQualifier}BOARD_RESYNC
    (I_BOARDID INTEGER)
AS
DECLARE donecurboard INTEGER DEFAULT 0;
DECLARE  ICI_TMP_FORUMID INTEGER;
DECLARE currBoards CURSOR FOR
            (SELECT BOARDID FROM {objectQualifier}BOARD);
BEGIN       

    IF (I_BOARDID IS NULL) THEN
        begin 
        OPEN currBoards;
             /*cycle through forums*/
                WHILE (1 = 1) DO
  BEGIN
                FETCH currBoards INTO :ICI_TMP_FORUMID ;
                /*resync board forums*/
            
        IF(ROW_COUNT = 0)THEN
      LEAVE;
        EXECUTE PROCEDURE {objectQualifier}FORUM_RESYNC :ICI_TMP_FORUMID, null;
      SUSPEND;
      END        
        CLOSE currBoards;
        /*deallocate curBoards*/
    END
    ELSE
        /*resync board forums*/
        EXECUTE PROCEDURE {objectQualifier}FORUM_RESYNC :I_BOARDID, null;
        SUSPEND;
    END;
--GO

CREATE PROCEDURE {objectQualifier}BBCODE_LIST
 (
    I_BOARDID INTEGER,
    I_BBCODEID INTEGER
 )
 returns
 (
  "BBCodeID" INTEGER,
  "BoardID" INTEGER,
  "Name" VARCHAR(128) CHARACTER SET UTF8,
  "Description" VARCHAR(4000) CHARACTER SET UTF8,
  "OnClickJS" VARCHAR(1000) CHARACTER SET UTF8,
  "DisplayJS" BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
  "EditJS" BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
  "DisplayCSS" BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
  "SearchRegex" BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
  "ReplaceRegex" BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
  "Variables" VARCHAR(1000) CHARACTER SET UTF8,
  "UseModule" SMALLINT,
  "ModuleClass" VARCHAR(128) CHARACTER SET UTF8,
  "ExecOrder" INTEGER
 )
 AS
 BEGIN
    IF (I_BBCODEID IS NULL) THEN
    FOR 
        SELECT * FROM {objectQualifier}BBCODE a  
        WHERE a.BOARDID = :I_BOARDID 
        ORDER BY a.EXECORDER, a.NAME DESC 
        INTO
         :"BBCodeID",
  :"BoardID",
  :"Name",
  :"Description",
  :"OnClickJS",
  :"DisplayJS",
  :"EditJS",
  :"DisplayCSS",
  :"SearchRegex",
  :"ReplaceRegex",
  :"Variables",
  :"UseModule",
  :"ModuleClass",
  :"ExecOrder"
  DO 
  SUSPEND; 
    ELSE
    FOR 
        SELECT * FROM {objectQualifier}BBCODE b 
        WHERE b.BBCODEID = :I_BBCODEID 
        ORDER BY b.EXECORDER
            INTO
         :"BBCodeID",
  :"BoardID",
  :"Name",
  :"Description",
  :"OnClickJS",
  :"DisplayJS",
  :"EditJS",
  :"DisplayCSS",
  :"SearchRegex",
  :"ReplaceRegex",
  :"Variables",
  :"UseModule",
  :"ModuleClass",
  :"ExecOrder"
        DO  SUSPEND;
 END;
--GO

CREATE PROCEDURE {objectQualifier}BBCODE_SAVE
(
    I_BBCODEID INTEGER,
    I_BOARDID INTEGER,
    I_NAME VARCHAR(128),
    I_DESCRIPTION varchar(4000),
    I_ONCLICKJS varchar(1000),
    I_DISPLAYJS BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
    I_EDITJS BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
    I_DISPLAYCSS BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
    I_SEARCHREGEX BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
    I_REPLACEREGEX BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
    I_VARIABLES varchar(1000),
    I_USEMODULE BOOL,
    I_MODULECLASS VARCHAR(128),	
    I_EXECORDER INTEGER
)
AS
BEGIN
    IF (I_BBCODEID IS NOT NULL) THEN
        UPDATE
            {objectQualifier}BBCODE
        SET
            NAME = :I_NAME,
            DESCRIPTION = :I_DESCRIPTION,
            ONCLICKJS = :I_ONCLICKJS,
            DISPLAYJS = :I_DISPLAYJS,
            EDITJS = :I_EDITJS,
            DISPLAYCSS = :I_DISPLAYCSS,
            SEARCHREGEX = :I_SEARCHREGEX,
            REPLACEREGEX = :I_REPLACEREGEX,
            VARIABLES = :I_VARIABLES,
            USEMODULE = :I_USEMODULE,
            MODULECLASS = :I_MODULECLASS,			
            EXECORDER = :I_EXECORDER
        WHERE
            BBCODEID = :I_BBCODEID;	
    ELSE 
    BEGIN
        IF (NOT EXISTS(SELECT 1 FROM {objectQualifier}BBCODE WHERE BOARDID = :I_BOARDID AND NAME = :I_NAME)) THEN
            INSERT INTO
                {objectQualifier}BBCODE (BBCODEID,BOARDID,NAME,DESCRIPTION,ONCLICKJS,DISPLAYJS,EDITJS,DISPLAYCSS,SEARCHREGEX,REPLACEREGEX,VARIABLES,USEMODULE,MODULECLASS,EXECORDER)
            VALUES ((SELECT NEXT VALUE FOR SEQ_{objectQualifier}BBCODE_BBCODEID FROM RDB$DATABASE),:I_BOARDID,:I_NAME,:I_DESCRIPTION,:I_ONCLICKJS,:I_DISPLAYJS,:I_EDITJS,:I_DISPLAYCSS,:I_SEARCHREGEX,:I_REPLACEREGEX,:I_VARIABLES,:I_USEMODULE,:I_MODULECLASS,:I_EXECORDER);
    END
END;
--GO


CREATE  PROCEDURE {objectQualifier}EVENTLOG_LIST(
                I_BOARDID INTEGER, I_PAGEUSERID INTEGER, I_MAXROWS INTEGER, I_MAXDAYS INTEGER,I_PAGEINDEX INTEGER,I_PAGESIZE INTEGER, I_SINCEDATE TIMESTAMP,I_TODATE TIMESTAMP, I_EVENTIDS  BLOB SUB_TYPE 1, I_UTCTIMESTAMP TIMESTAMP)
                RETURNS
                (
                "EventLogID" INTEGER,
  "EventTime" TIMESTAMP,
  "UserID" INTEGER,
  "Source" VARCHAR(128) CHARACTER SET UTF8,
  "Description" BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
  "Type" INTEGER,
  "Name" VARCHAR(128),
  "TotalRows" INTEGER
                )
                AS
DECLARE recCount INTEGER DEFAULT 0;
DECLARE VARIABLE ICI_EVENTID varchar(11);
DECLARE VARIABLE ICI_EVENTIDSCHUNK varchar(4000);
DECLARE VARIABLE ICI_POS integer;
DECLARE VARIABLE ICI_ITR integer;
DECLARE VARIABLE ICI_TRIMINDEX integer;
DECLARE VARIABLE ICI_LONG INTEGER DEFAULT 0;
DECLARE VARIABLE ICI_EVENTIDSCHUNKCURRENT integer DEFAULT 0;
DECLARE VARIABLE ICI_FIRSTSELECTROWNUMBER INTEGER DEFAULT 0;
DECLARE ICI_TOTALROWS  INTEGER DEFAULT 0;
DECLARE ICI_TOROW  INTEGER;
BEGIN
    ICI_ITR = 0; 
    ICI_POS = 0; 
    ICI_TRIMINDEX = 0;
    ICI_EVENTIDSCHUNK  = SUBSTRING( :I_EVENTIDS FROM 1 FOR 4000 );	
    ICI_TOROW = :I_PAGESIZE;

  IF (NOT EXISTS(SELECT 1  FROM rdb$relations r
  JOIN rdb$types t ON r.rdb$relation_type = t.rdb$type
  WHERE t.rdb$field_name = 'RDB$RELATION_TYPE'
  AND r.rdb$relation_name = '{objectQualifier}TMPEVLOGIDS')) THEN
    EXECUTE STATEMENT 'CREATE GLOBAL TEMPORARY TABLE {objectQualifier}TMPEVLOGIDS
   (EVENTTYPEID INTEGER)  ON COMMIT DELETE ROWS;';


 IF ( REPLACE(:ICI_EVENTIDSCHUNK, ',', '') <> '') THEN
    BEGIN
      WHILE (:ICI_POS > 0) DO
       BEGIN	
       ICI_EVENTID = LEFT(:ICI_EVENTIDSCHUNK, :ICI_POS  - 1);
       ICI_EVENTID = TRIM( BOTH FROM :ICI_EVENTID  );					
            
    IF (:ICI_EVENTID <> '') THEN
    BEGIN
        INSERT INTO {objectQualifier}TMPEVLOGIDS(EVENTTYPEID)
        SELECT (SELECT CAST(:ICI_EVENTID AS integer) FROM RDB$DATABASE)
            FROM  {objectQualifier}MESSAGE d WHERE d.MESSAGEID = (CAST(:ICI_EVENTID AS integer));				
    END
    END
END 
-- delete entries older than I_MAXDAYS days
DELETE FROM {objectQualifier}EVENTLOG
WHERE  DATEDIFF(DAY, :I_UTCTIMESTAMP, EVENTTIME) > :I_MAXDAYS;

       -- or if there are more then I_MAXROWS
      SELECT COUNT(1)
           FROM   {objectQualifier}EVENTLOG INTO :recCount;
           
        IF (:recCount >= (:I_MAXROWS + 50)) THEN
            BEGIN
          DELETE FROM {objectQualifier}EVENTLOG WHERE EVENTLOGID IN (SELECT  FIRST 100 EVENTLOGID FROM {objectQualifier}EVENTLOG ORDER BY EVENTTIME) ;             
           
      --     SELECT  FIRST 1 DISTINCT EVENTLOGID  FROM  {objectQualifier}EVENTLOG ORDER BY EVENTLOGID INTO :topLogID ; 
           
       --    DELETE FROM {objectQualifier}EVENTLOG
       --    WHERE       EVENTLOGID BETWEEN "topLogID"  AND "topLogID"  +100;
       END
I_PAGEINDEX = :I_PAGEINDEX + 1;	 
if (exists (select  1 from {objectQualifier}USER where (BIN_AND(FLAGS,1) = 1 and USERID = :I_PAGEUSERID) ROWS 1)) THEN
BEGIN
select count(1)  from
        {objectQualifier}EVENTLOG el		
        left join {objectQualifier}USER b 
        on b.USERID=el.USERID
        where	   
         (b.USERID IS NULL or b.BoardID = :I_BOARDID)	
         and ((:ICI_EVENTIDSCHUNK IS NULL )  OR  
         el."TYPE" IN (select EVENTTYPEID from {objectQualifier}TMPEVLOGIDS))  
         and (el.EventTime between :I_SINCEDATE and :I_TODATE) 
         into :ICI_TOTALROWS ;
            
        ICI_FIRSTSELECTROWNUMBER = (:I_PAGEINDEX - 1) * :I_PAGESIZE + 1;
        ICI_TOROW = :ICI_FIRSTSELECTROWNUMBER + :I_PAGESIZE - 1;
FOR
        SELECT   el.EVENTLOGID,
                 el.EVENTTIME,
                 el.USERID,
                 el.SOURCE,
                 el.DESCRIPTION,
                 el."TYPE",
                 COALESCE(b.NAME,'System')  AS "Name",
                 (SELECT :ICI_TOTALROWS FROM RDB$DATABASE) as TotalRows
        FROM     {objectQualifier}EVENTLOG el
                 LEFT JOIN {objectQualifier}USER b
                   ON b.USERID = el.USERID
        WHERE    (b.UserID IS NULL or b.BOARDID = :I_BOARDID)	
         and ((:ICI_EVENTIDSCHUNK IS NULL )  OR  
         el."TYPE" IN (select EVENTTYPEID from {objectQualifier}TMPEVLOGIDS))  
         and el.EventTime between :I_SINCEDATE and :I_TODATE 
        ORDER BY el.EVENTLOGID DESC  ROWS (:ICI_FIRSTSELECTROWNUMBER) TO (:ICI_TOROW)
        INTO
        :"EventLogID",
        :"EventTime",
        :"UserID",
        :"Source",
        :"Description",
        :"Type",
        :"Name",
        :"TotalRows"
         DO SUSPEND;
END
 ELSE
BEGIN
select count(1) from
        {objectQualifier}EVENTLOG el
                 left join {objectQualifier}EVENTLOGGROUPACCESS e on e.eventtypeid = el."TYPE"
                 join {objectQualifier}USERGROUP ug on (ug.userid =  :i_pageuserid and ug.groupid = e.groupid)
                 LEFT JOIN {objectQualifier}USER u
                 ON u.userid = el.userid
        where	   
        (u.UserID IS NULL or u.BoardID = :I_BOARDID)	
         and ((:ICI_EVENTIDSCHUNK IS NULL )  OR  
        el."TYPE" IN (select EVENTTYPEID from {objectQualifier}TMPEVLOGIDS))  
         and el.EventTime between :I_SINCEDATE and :I_TODATE 
         into :ICI_TOTALROWS;
            
         ICI_FIRSTSELECTROWNUMBER = (:I_PAGEINDEX - 1) * :I_PAGESIZE + 1;
        ICI_TOROW = :ICI_FIRSTSELECTROWNUMBER + :ICI_TOROW - 1;
        FOR SELECT   el.EVENTLOGID,
                 el.EVENTTIME,
                 el.USERID,
                 el.SOURCE,
                 el.DESCRIPTION,
                 el."TYPE",
                 COALESCE(b.NAME,'System')  AS "Name",
                (SELECT :ICI_TOTALROWS FROM RDB$DATABASE) as TotalRows
        FROM   {objectQualifier}EVENTLOG el
               LEFT JOIN {objectQualifier}EVENTLOGGROUPACCESS e ON e.EVENTTYPEID = el."TYPE"
               JOIN {objectQualifier}USERGROUP ug ON (ug.USERID = :I_PAGEUSERID and ug.GROUPID = e.GROUPID)
               LEFT JOIN {objectQualifier}USER b  ON b.USERID = el.USERID
        WHERE   (b.UserID IS NULL or b.BoardID = :I_BOARDID)	
         and ((:ICI_EVENTIDSCHUNK IS NULL )  OR  
         el."TYPE" IN (select EVENTTYPEID from {objectQualifier}TMPEVLOGIDS))  
         and el.EventTime between :I_SINCEDATE and :I_TODATE 
        ORDER BY el.EVENTLOGID DESC ROWS (:ICI_FIRSTSELECTROWNUMBER) TO (:ICI_TOROW)
        INTO
        :"EventLogID",
        :"EventTime",
        :"UserID",
        :"Source",
        :"Description",
        :"Type",
        :"Name",
        :"TotalRows"
         DO SUSPEND;
END 
END;
--GO

CREATE PROCEDURE {objectQualifier}EXTENSION_LIST(I_BOARDID INTEGER, I_EXTENSION VARCHAR(10)) 
RETURNS
 (
 "ExtensionID" INTEGER,
  "BoardID" INTEGER,
  "Extension" VARCHAR(10)
 )
AS
BEGIN

    -- If an extension is passed, then we want to check for THAT extension
    -- Otherwise, just get a list for the given BoardId
    
    IF (CHAR_LENGTH(:I_EXTENSION) > 0) THEN		
            FOR SELECT
                a.*
            FROM
                {objectQualifier}EXTENSION a
            WHERE
                a.BOARDID = :I_BOARDID
                AND a.EXTENSION=:I_EXTENSION
            ORDER BY
                a.EXTENSION
                INTO
                :"ExtensionID",
                :"BoardID",
                :"Extension"
                DO  SUSPEND;
                

    ELSE		
        FOR
            SELECT
                a.*
            FROM
                {objectQualifier}EXTENSION a
            WHERE
                a.BOARDID = :I_BOARDID	
            ORDER BY
                a.EXTENSION
                INTO
                :"ExtensionID",
                :"BoardID",
                :"Extension"
                DO SUSPEND;
              
        
END;
--GO

CREATE procedure {objectQualifier}EXTENSION_SAVE 
(I_EXTENSIONID INTEGER,
I_BOARDID INTEGER,
I_EXTENSION varchar(10)) 
as
begin
    if (I_EXTENSIONID is null or I_EXTENSIONID = 0) THEN
        insert INTO {objectQualifier}EXTENSION (EXTENSIONID,BOARDID,EXTENSION) 
        values((SELECT NEXT VALUE FOR SEQ_{objectQualifier}EXTENSION_EXTENSIONID FROM RDB$DATABASE),:I_BOARDID,:I_EXTENSION);	
    else 
        update {objectQualifier}EXTENSION 
        set EXTENSION = :I_EXTENSION 
        where EXTENSIONID = :I_EXTENSIONID;
    
END;
--GO

CREATE PROCEDURE {objectQualifier}ACTIVE_UPDATEMAXSTATS
(
    I_BOARDID INTEGER,
    I_UTCTIMESTAMP TIMESTAMP 
)
AS
DECLARE VARIABLE  ici_count INTEGER;
DECLARE VARIABLE  ici_max INTEGER; 
DECLARE VARIABLE  ici_maxStr VARCHAR(128);
DECLARE VARIABLE  ici_countStr VARCHAR(128);
DECLARE VARIABLE  ici_one VARCHAR(128) DEFAULT '1';
BEGIN
    
    SELECT CAST(a."VALUE" AS VARCHAR(128)) FROM {objectQualifier}REGISTRY a WHERE a.BOARDID = :I_BOARDID AND a.NAME = 'maxusers' INTO :ici_maxStr;
    ici_maxStr = COALESCE(:ici_maxStr,'1');	
    SELECT CAST(:ici_maxStr AS INTEGER) FROM rdb$database INTO :ici_max;
    SELECT COUNT(DISTINCT IP) FROM {objectQualifier}ACTIVE WHERE BOARDID = :I_BOARDID INTO :ici_count;
    ici_count = COALESCE(:ici_count,0);	
    SELECT CAST(:ici_count AS VARCHAR(128)) FROM rdb$database INTO :ici_countStr;
    
    
        
    IF (NOT EXISTS ( SELECT 1 FROM {objectQualifier}REGISTRY WHERE BOARDID = :I_BOARDID and NAME = 'maxusers' )) THEN
    BEGIN 
        INSERT INTO {objectQualifier}REGISTRY(REGISTRYID,BOARDID,NAME,"VALUE") 
        VALUES ((SELECT NEXT VALUE FOR SEQ_{objectQualifier}REGISTRY_REGISTRYID FROM RDB$DATABASE),:I_BOARDID,'maxusers',
        CAST(:ici_countStr AS BLOB SUB_TYPE 1 SEGMENT SIZE 20));
        INSERT INTO {objectQualifier}REGISTRY(REGISTRYID,BOARDID,NAME,"VALUE") 
        VALUES ((SELECT NEXT VALUE FOR SEQ_{objectQualifier}REGISTRY_REGISTRYID FROM RDB$DATABASE),:I_BOARDID,'maxuserswhen',
        :I_UTCTIMESTAMP);
    END
    ELSE IF (ici_count > ici_max) THEN	
    BEGIN
        UPDATE {objectQualifier}REGISTRY SET "VALUE" = CAST(:ici_countStr AS BLOB SUB_TYPE 1 SEGMENT SIZE 20) WHERE BOARDID = :I_BOARDID AND NAME = 'maxusers';
        UPDATE {objectQualifier}REGISTRY SET "VALUE" = :I_UTCTIMESTAMP WHERE BOARDID = :I_BOARDID AND NAME ='maxuserswhen';
    END
END;
--GO

CREATE  PROCEDURE {objectQualifier}PAGELOAD(I_SESSIONID varchar(32), 
I_BOARDID INTEGER, 
I_USERKEY  VARCHAR(64), 
I_IP VARCHAR(39), 
I_LOCATION VARCHAR(255),
I_FORUMPAGE VARCHAR(255),
I_BROWSER VARCHAR(128), 
I_PLATFORM VARCHAR(128), 
I_CATEGORYID INTEGER, 
I_FORUMID INTEGER,
I_TOPICID INTEGER, 
I_MESSAGEID INTEGER,
I_ISCRAWLER BOOL,
I_ISMOBILEDEVICE BOOL, 
I_DONTTRACK BOOL,
I_UTCTIMESTAMP TIMESTAMP)
  RETURNS 
  (
"ActiveUpdate" BOOL,  
"PreviousVisit" TIMESTAMP,
"UserID" INTEGER,
"ForumID" INTEGER,
"IsAdmin"	SMALLINT,
"IsGuest"	SMALLINT,
"IsForumModerator" SMALLINT,
"IsModerator"  SMALLINT,
"LastActive" TIMESTAMP,
"ReadAccess" SMALLINT,			
"PostAccess" SMALLINT,
"ReplyAccess" SMALLINT,
"PriorityAccess" SMALLINT,
"PollAccess" SMALLINT,
"VoteAccess" SMALLINT,
"ModeratorAccess" SMALLINT,
"EditAccess" SMALLINT,
"DeleteAccess" SMALLINT,
"UploadAccess" SMALLINT,		
"DownloadAccess" SMALLINT, 
"UserForumAccess" SMALLINT,
"IsCrawler" SMALLINT,
"IsMobileDevice" SMALLINT,			
"CategoryID" INTEGER,
"CategoryName" varchar(128),
"ForumName" varchar(128),
"TopicID" INTEGER,
"TopicName" varchar(128),
"ForumTheme" varchar(128)
) 
AS
DECLARE VARIABLE ici_userid		INTEGER;
DECLARE VARIABLE ici_userboardid	INTEGER;
DECLARE VARIABLE ici_rowcount	INTEGER;
DECLARE VARIABLE ici_previousvisit	timestamp;
DECLARE VARIABLE ici_ActiveUpdate	BOOL DEFAULT 0;
DECLARE VARIABLE ici_IsCrawler BOOL;
DECLARE VARIABLE ici_ActiveFlags INTEGER DEFAULT 1;
DECLARE VARIABLE ICI_GUESTID  INTEGER;
BEGIN

I_CATEGORYID =coalesce(:I_CATEGORYID,0); 
I_FORUMID    =coalesce(:I_FORUMID,0);
I_TOPICID    =coalesce(:I_TOPICID,0);
I_MESSAGEID  = coalesce(:I_MESSAGEID,0);
ici_IsCrawler = :I_ISCRAWLER;
-- SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
    /*set implicit_transactions off*/        
    -- find a guest id should do it every time to be sure that guest access rights are in ActiveAccess table
    -- Look into this - it doesn't work	
    SELECT  USERID, COUNT(1) 
        FROM {objectQualifier}USER 
        WHERE BOARDID=:I_BOARDID AND BIN_AND(FLAGS,4) = 4 GROUP BY JOINED, USERID 
        INTO :ICI_GUESTID,:ici_rowcount;	
            
    IF (ici_rowcount>1) THEN
    BEGIN
    EXCEPTION TOO_MANY_GUESTS;  
    END 
 
    IF (:I_USERKEY IS NULL) THEN
    BEGIN       
 
        ici_userid = :ICI_GUESTID;       
        ici_userboardid = :I_BOARDID;
        -- set IsGuest ActiveFlag  1 | 2
        ici_ActiveFlags = 3;		
        -- crawlers are always guests 
        if	(:I_ISCRAWLER = 1) THEN
            begin
            -- set IsCrawler ActiveFlag
            ici_ActiveFlags =  BIN_OR(ici_ActiveFlags, 8);
            END 
    END 
    ELSE
    BEGIN
        SELECT USERID, BOARDID   
        FROM {objectQualifier}USER 
        where BOARDID=:I_BOARDID AND PROVIDERUSERKEY=CHAR_TO_UUID(:I_USERKEY)
        INTO :ICI_USERID,:ici_UserBoardID;      
        -- make sure that registered users are not crawlers
        ici_IsCrawler = 0;
        -- set IsRegistered ActiveFlag
        ici_ActiveFlags = BIN_OR(ici_ActiveFlags, 4);        
    END
    
    /* Check valid ForumID */
    IF (:I_FORUMID IS NOT NULL 
            AND NOT EXISTS
            (SELECT 1 FROM {objectQualifier}FORUM WHERE FORUMID=:I_FORUMID))
                THEN 
        I_FORUMID = NULL;             
    
    /* Check valid CategoryID*/
    IF (:I_CATEGORYID IS NOT NULL 
    AND NOT EXISTS
    (SELECT 1 FROM {objectQualifier}CATEGORY WHERE CATEGORYID=:I_CATEGORYID))  THEN 
        I_CATEGORYID = NULL;
        
    -- Check valid MessageID
    IF (:i_messageid IS NOT NULL 
    AND NOT EXISTS
    (SELECT 1 FROM {objectQualifier}MESSAGE WHERE MESSAGEID=:i_messageid)) 
           THEN
        i_messageid = NULL;
       
   -- Check valid TopicID
    IF (:I_TOPICID IS NOT NULL 
    AND NOT EXISTS
    (SELECT 1 FROM {objectQualifier}TOPIC WHERE TOPICID=:I_TOPICID)) 
           THEN
        I_TOPICID = NULL;  


    -- find missing ForumID/TopicID
    IF (:i_messageid IS NOT NULL) THEN
        SELECT
            c.CATEGORYID,
            b.FORUMID,
            b.TOPICID               
        FROM
            {objectQualifier}MESSAGE a
            INNER JOIN {objectQualifier}TOPIC b 
            ON b.TOPICID = a.TOPICID
            INNER JOIN {objectQualifier}FORUM c 
            ON c.FORUMID = b.FORUMID
            INNER JOIN {objectQualifier}CATEGORY d 
            ON d.CATEGORYID = c.CATEGORYID
        WHERE
            a.MESSAGEID = :i_messageid AND
            BOARDID = :I_BOARDID
             INTO :I_CATEGORYID,:I_FORUMID,:I_TOPICID;
    ELSE IF (:I_TOPICID IS NOT NULL) THEN
        SELECT 
            b.CATEGORYID,
            a.FORUMID                 
        FROM 
            {objectQualifier}TOPIC a
            inner join {objectQualifier}FORUM b 
            on b.FORUMID = a.FORUMID
            inner join {objectQualifier}CATEGORY c 
            on c.CATEGORYID = b.CATEGORYID
        WHERE 
            a.TOPICID = :I_TOPICID AND
            c.BOARDID = :I_BOARDID
            INTO :I_CATEGORYID,:I_FORUMID;
    
    ELSE IF (:I_FORUMID IS NOT NULL) THEN
        SELECT
             a.CATEGORYID	                    
        FROM	{objectQualifier}FORUM a
            inner join {objectQualifier}CATEGORY b 
            on b.CATEGORYID = a.CATEGORYID
        WHERE
            a.FORUMID = :I_FORUMID and
            b.BOARDID = :I_BOARDID
            INTO     :I_CATEGORYID;	 

    -- ensure that access right are in place		
     if (NOT EXISTS (SELECT FIRST 1
            USERID	
            from {objectQualifier}ACTIVEACCESS 
            where USERID = :ICI_USERID)) THEN
            BEGIN				
            insert into {objectQualifier}ACTIVEACCESS(
            USERID,
            BOARDID,
            FORUMID,
            ISADMIN, 
            ISFORUMMODERATOR,
            ISMODERATOR,
            ISGUESTX,
            LASTACTIVE, 
            READACCESS,
            POSTACCESS,
            REPLYACCESS,
            PRIORITYACCESS,
            POLLACCESS,
            VOTEACCESS,	
            MODERATORACCESS,
            EDITACCESS,
            DELETEACCESS,
            UPLOADACCESS,
            DOWNLOADACCESS,
            USERFORUMACCESS)
            select 
            USERID, 
            (SELECT :I_BOARDID FROM RDB$DATABASE), 
            FORUMID, 
            SIGN(ISADMIN), 
            SIGN(ISFORUMMODERATOR),
            SIGN(ISMODERATOR),			
            (CASE WHEN (:I_USERKEY IS NULL) THEN (SELECT 1 FROM RDB$DATABASE) ELSE (SELECT 0 FROM RDB$DATABASE)  END),
            (SELECT :I_UTCTIMESTAMP FROM RDB$DATABASE),
            SIGN(READACCESS),
            SIGN(POSTACCESS),
            SIGN(REPLYACCESS),
            SIGN(PRIORITYACCESS),
            SIGN(POLLACCESS),
            SIGN(VOTEACCESS),
            SIGN(MODERATORACCESS),
            SIGN(EDITACCESS),
            SIGN(DELETEACCESS),
            SIGN(UPLOADACCESS),
            SIGN(DOWNLOADACCESS),
            SIGN(USERFORUMACCESS)			
            from {objectQualifier}VACCESS
            where USERID = :ICI_USERID;
        END	

        -- ensure that guest access right are in place		
     if (:ICI_USERID != :ICI_GUESTID and (NOT EXISTS (SELECT FIRST 1
            USERID	
            from {objectQualifier}ACTIVEACCESS 
            where USERID = :ICI_GUESTID))) THEN
            BEGIN				
            insert into {objectQualifier}ACTIVEACCESS(
            USERID,
            BOARDID,
            FORUMID,
            ISADMIN, 
            ISFORUMMODERATOR,
            ISMODERATOR,
            ISGUESTX,
            LASTACTIVE, 
            READACCESS,
            POSTACCESS,
            REPLYACCESS,
            PRIORITYACCESS,
            POLLACCESS,
            VOTEACCESS,	
            MODERATORACCESS,
            EDITACCESS,
            DELETEACCESS,
            UPLOADACCESS,
            DOWNLOADACCESS,
            USERFORUMACCESS)
            select 
            USERID, 
            (SELECT :I_BOARDID FROM RDB$DATABASE), 
            FORUMID, 
            SIGN(ISADMIN), 
            SIGN(ISFORUMMODERATOR),
            SIGN(ISMODERATOR),
            (CASE WHEN (:I_USERKEY IS NULL) THEN (SELECT CAST(1 AS BOOL)  FROM RDB$DATABASE) ELSE (SELECT CAST(0 AS BOOL) FROM RDB$DATABASE)  END),
            (SELECT :I_UTCTIMESTAMP FROM RDB$DATABASE),
            SIGN(READACCESS),
            SIGN(POSTACCESS),
            SIGN(REPLYACCESS),
            SIGN(PRIORITYACCESS),
            SIGN(POLLACCESS),
            SIGN(VOTEACCESS),
            SIGN(MODERATORACCESS),
            SIGN(EDITACCESS),
            SIGN(DELETEACCESS),
            SIGN(UPLOADACCESS),
            SIGN(DOWNLOADACCESS),
            SIGN(USERFORUMACCESS)			
            from {objectQualifier}VACCESS
            where USERID = :ICI_GUESTID;
        END	
  if (EXISTS (SELECT FIRST 1
            USERID	
            from {objectQualifier}ACTIVEACCESS 
            where USERID = :ICI_USERID and FORUMID = COALESCE(:I_FORUMID,0) and (COALESCE(:I_FORUMID,0) = 0 OR READACCESS = 1))) THEN
            BEGIN
   -- update active

         DELETE FROM {objectQualifier}ACTIVE WHERE SESSIONID = :I_SESSIONID AND BOARDID <> :I_BOARDID;
            -- get previous visit if not a guest
    if (:I_USERKEY IS NULL) THEN
     begin
         select LastVisit from {objectQualifier}USER where UserID = :ici_userid
         INTO :ici_previousvisit;
    END

    -- update last visit
    UPDATE {objectQualifier}USER SET 
        LASTVISIT = :I_UTCTIMESTAMP,
        IP = :I_IP
    WHERE USERID = :ici_userid;
    
    IF ((:I_DONTTRACK <> 1) 
    AND ici_userid IS NOT NULL 
    AND ici_userboardid=:I_BOARDID) THEN
    BEGIN
        IF (EXISTS(SELECT 1 FROM {objectQualifier}ACTIVE 
        WHERE SESSIONID=:I_SESSIONID 
        AND BOARDID=:I_BOARDID)) THEN
        IF (CHAR_LENGTH(:I_FORUMPAGE) > 0) THEN
        BEGIN
        IF (:ici_IsCrawler <> 1) THEN
        BEGIN
         UPDATE {objectQualifier}ACTIVE SET
                USERID = :ici_userid,
                IP = :I_IP,
                LASTACTIVE = :I_UTCTIMESTAMP,
                LOCATION = :I_LOCATION,
                FORUMID = :I_FORUMID,
                TOPICID = :I_TOPICID,
                BROWSER = :I_BROWSER,
                PLATFORM = :I_PLATFORM,
                FORUMPAGE = :I_FORUMPAGE,
                FLAGS = :ici_ActiveFlags
            WHERE SESSIONID= :I_SESSIONID;	
            END
            ELSE
            BEGIN
             UPDATE {objectQualifier}ACTIVE SET
                USERID = :ici_userid,
                IP = :I_IP,
                LASTACTIVE = :I_UTCTIMESTAMP,
                LOCATION = :I_LOCATION,
                FORUMID = :I_FORUMID,
                TOPICID = :I_TOPICID,
                BROWSER = :I_BROWSER,
                PLATFORM = :I_PLATFORM,
                FORUMPAGE = :I_FORUMPAGE,
                FLAGS = :ici_ActiveFlags
            WHERE BROWSER = :I_BROWSER AND IP = :I_IP;
            -- trace crawler: the cache is reset every time crawler moves to next page ? Disabled as cache reset will overload server 
             -- we not update cache for a guest
			   if (:I_USERKEY IS NULL) then	
                BEGIN	
                 ici_ActiveUpdate = 1;
                END	
            END	
            END
            ELSE			
             UPDATE {objectQualifier}ACTIVE SET				
                LASTACTIVE = :I_UTCTIMESTAMP
            WHERE SESSIONID= :I_SESSIONID;				
        ELSE
        BEGIN
            INSERT INTO {objectQualifier}ACTIVE(
            SESSIONID,
            BOARDID,
            USERID,
            IP,
            LOGIN,
            LASTACTIVE,
            LOCATION,
            FORUMID,
            TOPICID,
            BROWSER,
            PLATFORM,
            FORUMPAGE,
            FLAGS)
            VALUES(
            :I_SESSIONID,
            :I_BOARDID,
            :ICI_USERID,
            :I_IP,
            :I_UTCTIMESTAMP,
            :I_UTCTIMESTAMP,
            :I_LOCATION,
            :I_FORUMID,
            :I_TOPICID,
            :I_BROWSER,
            :I_PLATFORM,
            :I_FORUMPAGE,
            :ici_ActiveFlags);
            
            -- parameter to update active users cache if this is a new user			
                if (:I_USERKEY IS NULL) then	
                BEGIN	
                 ici_ActiveUpdate = 1;
                END
        
            -- update max user stats
            EXECUTE PROCEDURE {objectQualifier}ACTIVE_UPDATEMAXSTATS :I_BOARDID, :I_UTCTIMESTAMP;			
        END
        -- remove duplicate users but it happens with regular users TODO:
        IF (:I_USERKEY IS NOT NULL) THEN
            DELETE FROM {objectQualifier}ACTIVE
             WHERE USERID=:ici_userid 
             AND BOARDID=:I_BOARDID 
             AND SESSIONID<>:I_SESSIONID; 
    END
    END
    -- return information	
      
    FOR 
    SELECT FIRST 1 	-- get previous visit 
    (SELECT :ici_ActiveUpdate FROM RDB$DATABASE),	 
(CASE WHEN (:I_USERKEY IS NOT NULL) THEN
        (SELECT  LASTVISIT 
        FROM {objectQualifier}USER WHERE USERID = :ici_userid) ELSE NULL END) AS "PreviousVisit",		
x.USERID AS "UserID",
x.FORUMID AS "ForumID",
x.ISADMIN AS "IsAdmin",
(CASE WHEN (:I_USERKEY IS NULL) THEN (SELECT 1 FROM RDB$DATABASE) ELSE (SELECT 0 FROM RDB$DATABASE)  END) AS "IsGuest",
x.ISFORUMMODERATOR AS "IsForumModerator",
x.ISMODERATOR AS "IsModerator",
x.LASTACTIVE,
x.READACCESS,			
x.POSTACCESS,
x.REPLYACCESS,
x.PRIORITYACCESS,
x.POLLACCESS,
x.VOTEACCESS,
x.MODERATORACCESS,
x.EDITACCESS,
x.DELETEACCESS,
x.UPLOADACCESS,		
x.DOWNLOADACCESS,
x.USERFORUMACCESS,
(SELECT :ici_IsCrawler FROM RDB$DATABASE),
(SELECT :I_ISMOBILEDEVICE FROM RDB$DATABASE),
        COALESCE(:I_CATEGORYID,0) AS "CategoryID",
        (SELECT NAME FROM {objectQualifier}CATEGORY
         WHERE CATEGORYID = :I_CATEGORYID) AS "CategoryName",		
        (SELECT NAME from {objectQualifier}FORUM 
         WHERE FORUMID = :I_FORUMID) AS "ForumName",
        (SELECT :I_TOPICID FROM rdb$database) AS "TopicID",
        (SELECT TOPIC from {objectQualifier}TOPIC
         where TOPICID = :I_TOPICID) AS "TopicName",
         (SELECT THEMEURL from {objectQualifier}FORUM
         where FORUMID = :I_FORUMID) AS "ForumTheme"
    FROM
     {objectQualifier}ACTIVEACCESS x 
        where x.USERID= :ici_userid 
        and x.FORUMID=COALESCE(:I_FORUMID,0)
        ORDER BY x.USERID 		
        INTO
        :"ActiveUpdate",
        :"PreviousVisit",
        :"UserID", 
        :"ForumID", 
        :"IsAdmin",
        :"IsGuest",
        :"IsForumModerator",
        :"IsModerator",    
        :"LastActive",
        :"ReadAccess",			
        :"PostAccess",
        :"ReplyAccess",
        :"PriorityAccess",
        :"PollAccess",
        :"VoteAccess",
        :"ModeratorAccess",
        :"EditAccess",
        :"DeleteAccess",
        :"UploadAccess",		
        :"DownloadAccess",
        :"UserForumAccess",
        :"IsCrawler",
        :"IsMobileDevice",		
        :"CategoryID",
        :"CategoryName",       
        :"ForumName",
        :"TopicID",
        :"TopicName",
        :"ForumTheme"
DO SUSPEND;	

END;
--GO


CREATE PROCEDURE {objectQualifier}BOARD_LIST(I_BOARDID INTEGER)
  RETURNS (
    "BoardID" INTEGER,
    "Name" VARCHAR(128),
    "AllowThreaded" BOOL,
    "MembershipAppName" VARCHAR(255),
    "RolesAppName" VARCHAR(255),
    "SQLVersion" VARCHAR(500)
)
  AS
  DECLARE VARIABLE  ici_engine_version VARCHAR(128);
  DECLARE VARIABLE ici_db_creation_date TIMESTAMP;
BEGIN 
  SELECT rdb$get_context('SYSTEM', 'ENGINE_VERSION') FROM rdb$database
  INTO :ici_engine_version;
  SELECT mon$creation_date FROM mon$database
  INTO :ici_db_creation_date;
  
  FOR  
  SELECT a.*,
   ( 'Firebird-' || :ici_engine_version || '.' || ' The current database created on '|| :ici_db_creation_date || '.') AS "SQLVersion"
  FROM   {objectQualifier}BOARD a
  WHERE  (:I_BOARDID is null or a.BOARDID = :I_BOARDID)
  INTO
  :"BoardID",
  :"Name",
  :"AllowThreaded",
  :"MembershipAppName",
  :"RolesAppName",
  :"SQLVersion"  
DO SUSPEND;  

  END;
--GO

CREATE PROCEDURE {objectQualifier}REGISTRY_LIST(I_NAME VARCHAR(128), I_BOARDID INTEGER)
  RETURNS(
    "RegistryID" INTEGER,
    "Name" VARCHAR(128),
    "VALUE" BLOB SUB_TYPE 1 SEGMENT SIZE 20 CHARACTER SET UTF8,
    "BoardID" INTEGER
) AS

BEGIN
IF (I_BOARDID IS NULL) THEN
    BEGIN
        IF (I_NAME IS NULL OR I_NAME = '') THEN
        FOR 
            SELECT * FROM {objectQualifier}REGISTRY 
                        WHERE BOARDID IS NULL
            INTO
    :"RegistryID",
    :"Name",
    :"VALUE",
    :"BoardID"
DO SUSPEND;

         ELSE

        FOR 
            SELECT * FROM {objectQualifier}REGISTRY 
                        WHERE LOWER(NAME) = LOWER(:I_NAME) and BOARDID IS NULL
INTO
    :"RegistryID",
    :"Name",
    :"VALUE",
    :"BoardID"
DO SUSPEND;
 END		
    ELSE 
    BEGIN	
        IF (I_NAME IS NULL OR I_NAME = '') THEN
        FOR 
            SELECT * FROM {objectQualifier}REGISTRY 
                        WHERE BOARDID=:I_BOARDID
INTO
    :"RegistryID",
    :"Name",
    :"VALUE",
    :"BoardID"
DO SUSPEND;
         ELSE
        FOR 
            SELECT * FROM {objectQualifier}REGISTRY 
                        WHERE LOWER(NAME) = LOWER(:I_NAME) and BOARDID=:I_BOARDID
INTO
    :"RegistryID",
    :"Name",
    :"VALUE",
    :"BoardID"
DO SUSPEND;
    END 	
END;
--GO

create procedure {objectQualifier}FORUM_MODERATORS(I_STYLEDNICKS BOOL)
returns ("ForumID" INTEGER, 
         "ForumName" VARCHAR(128),  
        "ModeratorID" INTEGER, 
        "ModeratorName" VARCHAR(128),
        "ModeratorDisplayName" VARCHAR(128),
        "ModeratorEmail" varchar(255),
        "ModeratorAvatar" varchar(255),
        "ModeratorAvatarImage" BOOL,
        "Style" VARCHAR(128),
        "IsGroup" SMALLINT)
 as
BEGIN
    FOR SELECT * FROM (select
        a.FORUMID AS FORUMID,
        f.NAME AS "ForumName", 
        a.GROUPID AS "ModeratorID", 
        b.NAME AS "ModeratorName",
        b.NAME AS "ModeratorDisplayName",
        (SELECT '' FROM RDB$DATABASE) AS "ModeratorEmail",
        (SELECT '' FROM RDB$DATABASE) AS "ModeratorAvatar",
        (SELECT CAST(0 as BOOL) FROM RDB$DATABASE) as "ModeratorAvatarImage",
            (case(:I_STYLEDNICKS)
            when 1 then b.STYLE  
            else ''	 END) as Style,	
        (SELECT 1 FROM RDB$DATABASE) AS "IsGroup"			
    from
        {objectQualifier}FORUM f
        INNER JOIN	{objectQualifier}FORUMACCESS a
        ON a.FORUMID = f.FORUMID
        INNER JOIN {objectQualifier}GROUP b 
        ON b.GROUPID = a.GROUPID
        INNER JOIN {objectQualifier}ACCESSMASK c 
        ON c.ACCESSMASKID = a.ACCESSMASKID
    where
        BIN_AND(b.FLAGS,1)=0 and
        BIN_AND(c.FLAGS,64)<>0
    union all
    select 
        access.FORUMID AS FORUMID, 
        f.NAME as "ForumName",
        usr.USERID AS "ModeratorID", 
        usr.NAME AS "ModeratorName",
        usr.DISPLAYNAME AS "ModeratorDisplayName",
        usr.email AS "ModeratorEmail",
        COALESCE(usr.Avatar, '') AS "ModeratorAvatar",
        (CASE WHEN (bit_length(usr.AVATARIMAGE) > 10) THEN CAST(1 AS SMALLINT) ELSE CAST(0 AS SMALLINT) END),		
        (case(:I_STYLEDNICKS)
            when 1 then  usr.USERSTYLE
            else ''	 END) as Style,	
        (SELECT 0 FROM RDB$DATABASE) AS "IsGroup"
    from
        {objectQualifier}USER usr
        INNER JOIN (
            select
                a.USERID AS USERID,
                x.FORUMID AS FORUMID,
                max(x.MODERATORACCESS) AS MODERATORACCESS
            from
                {objectQualifier}vaccessfull x
                INNER JOIN {objectQualifier}USERGROUP a on a.USERID=x.USERID
                INNER JOIN {objectQualifier}GROUP b on b.GROUPID=a.GROUPID
            WHERE 
                x.MODERATORACCESS <> 0 AND x.ADMINGROUP = 0
            GROUP BY
                a.USERID,x.FORUMID		
        ) access ON usr.USERID = access.USERID
        JOIN {objectQualifier}FORUM f			
        ON f.FORUMID = access.FORUMID			
    where
        access.MODERATORACCESS<>0)
    order by
        "IsGroup" desc,
        "ModeratorName" asc
        INTO
        :"ForumID",
        :"ForumName",
        :"ModeratorID", 
        :"ModeratorName",
        :"ModeratorDisplayName",
        :"ModeratorEmail",
        :"ModeratorAvatar",
        :"ModeratorAvatarImage",
        :"Style",
        :"IsGroup"
        DO SUSPEND;
END;
--GO

create procedure {objectQualifier}FORUM_LISTREAD_HELPER(I_FORUMID INTEGER) 
RETURNS
(
"LastPosted" timestamp,
"LastMessageID" INTEGER,
"LastUserID" INTEGER,
"LastTopicID" INTEGER,
"LastTopicName" varchar(255) CHARACTER SET UTF8
)
as
DECLARE VARIABLE ici_LastPosted timestamp;
DECLARE VARIABLE ici_LastMessageID integer;
DECLARE VARIABLE ici_LastUserID integer;
DECLARE VARIABLE ici_LastTopicID integer;
begin

SELECT	FIRST 1		
        b.LASTPOSTED,
        b.LASTMESSAGEID,
        b.LASTUSERID, 		                
        b.LASTTOPICID 
 FROM 
        {objectQualifier}FORUM b
        WHERE
         b.FORUMID=:I_FORUMID OR b.PARENTID=:I_FORUMID AND b.LASTPOSTED IS NOT NULL		
    ORDER BY 		
        b.LASTPOSTED DESC
    INTO :ici_LastPosted,:ici_LastMessageID,:ici_LastUserID,:ici_LastTopicID;
    
for select 
        (SELECT :ici_LastPosted FROM RDB$DATABASE),
        (SELECT :ici_LastMessageID FROM RDB$DATABASE),
        (SELECT :ici_LastUserID FROM RDB$DATABASE), 	
        (SELECT :ici_LastTopicID FROM RDB$DATABASE),
        t.TOPIC 		
    FROM 
        {objectQualifier}TOPIC t WHERE
        t.TOPICID =:ici_LastTopicID
        INTO
:"LastPosted",
:"LastMessageID",
:"LastUserID",
:"LastTopicID",
:"LastTopicName"	
        DO SUSPEND;		
END;
--GO
/* create procedure {objectQualifier}FORUM_LISTREAD_OLD(I_BOARDID INTEGER,I_USERID INTEGER,I_CATEGORYID 

INTEGER,I_PARENTID INTEGER) 
RETURNS
(
"CategoryID" INTEGER,
"Category" varchar(255) CHARACTER SET UTF8,
"ForumID" INTEGER,
"Forum" varchar(255) CHARACTER SET UTF8,
"Description" varchar(255) CHARACTER SET UTF8,
"Topics" INTEGER,
"Posts" INTEGER,
"Subforums" INTEGER,
"Flags" INTEGER,
"Viewing" INTEGER,
"RemoteURL" varchar(255) CHARACTER SET UTF8,
"ReadAccess" INTEGER,
"LastTopicID" INTEGER,
"LastPosted" timestamp,
"LastMessageID" INTEGER,
"LastTopicName" varchar(255) CHARACTER SET UTF8,
"LastUserID" INTEGER, 
"LastUser" varchar(128) CHARACTER SET UTF8
)
as
DECLARE VARIABLE ici_LastPosted timestamp;
DECLARE VARIABLE ici_LastMessageID integer;
DECLARE VARIABLE ici_LastTopicName integer;
begin
for select 
        a.CATEGORYID, 
        a.NAME AS "Category", 
        b.FORUMID AS "ForumID",
        b.NAME AS "Forum", 
        DESCRIPTION,
        (SELECT * FROM {objectQualifier}FORUM_TOPICS(b.FORUMID)) AS "Topics",
        (SELECT * FROM {objectQualifier}FORUM_POSTS(b.FORUMID)) AS "Posts",
        (SELECT * FROM {objectQualifier}FORUM_SUBFORUMS(b.FORUMID, :I_USERID)) AS "Subforums",
        b.FLAGS,
        (select count(1) from {objectQualifier}ACTIVE x JOIN {objectQualifier}USER usr 
        ON x.USERID = usr.USERID 
        where x.FORUMID=b.FORUMID 
        AND usr.ISACTIVEEXCLUDED = 0) AS "Viewing",
        b.REMOTEURL,
        x.READACCESS,
        (SELECT * FROM {objectQualifier}FORUM_LASTTOPIC

(b.FORUMID,:I_USERID,b.LASTTOPICID,b.LASTPOSTED)) AS LASTTOPICID,
      (SELECT :ici_LastPosted FROM RDB$DATABASE),
        (SELECT :ici_LastMessageID FROM RDB$DATABASE),
        (SELECT :ici_LastTopicName FROM RDB$DATABASE),
        (SELECT LASTPOSTED FROM {objectQualifier}TOPIC WHERE TOPICID = LASTTOPICID),
        (SELECT LASTMESSAGEID FROM {objectQualifier}TOPIC WHERE TOPICID = LASTTOPICID),
        (SELECT TOPIC FROM {objectQualifier}TOPIC WHERE TOPICID = LASTTOPICID),
        (SELECT LASTUSERID FROM {objectQualifier}TOPIC WHERE TOPICID = LASTTOPICID),
         COALESCE((SELECT LASTUSERNAME FROM {objectQualifier}TOPIC WHERE TOPICID = LASTTOPICID),(select 

NAME from {objectQualifier}USER x 
        where x.USERID=b.LASTUSERID)) AS "LastUser"		 
     from 
        {objectQualifier}CATEGORY a
        join {objectQualifier}FORUM b on b.CATEGORYID=a.CATEGORYID
        join {objectQualifier}vaccess x on x.FORUMID=b.FORUMID				
    where 
        a.BOARDID = :I_BOARDID and
        (BIN_AND(b.FLAGS,2)=0 or x.READACCESS<>0) and
        (:I_CATEGORYID is null or a.CATEGORYID=:I_CATEGORYID) and
        ((:I_PARENTID is null and b.PARENTID is null) 
        or b.PARENTID=:I_PARENTID) and
        x.USERID = :I_USERID
        order by
        a.SORTORDER,
        b.SORTORDER
        INTO		
         :"CategoryID",
         :"Category",
         :"ForumID",
         :"Forum",
         :"Description",
         :"Topics",
         :"Posts",
         :"Subforums",
         :"Flags",
         :"Viewing",
         :"RemoteURL",
         :"ReadAccess",
         :"LastTopicID",
         :"LastPosted",
         :"LastMessageID",
         :"LastTopicName",
         :"LastUserID",
         :"LastUser"
        DO SUSPEND;		
END;
*/

create procedure {objectQualifier}FORUM_LISTREAD(
I_BOARDID INTEGER,
I_USERID INTEGER,
I_CATEGORYID INTEGER,
I_PARENTID INTEGER,
I_STYLEDNICKS INTEGER,
I_FINDLASTUNREAD BOOL,
I_SHOWCOMMONFORUMS BOOL,
I_SHOWPERSONALFORUMS BOOL,
I_FORUMCREATEDBYUSERID INTEGER,
I_UTCTIMESTAMP TIMESTAMP) 
RETURNS
(
"CategoryID" INTEGER,
"Category" varchar(255) CHARACTER SET UTF8,
"ForumID" INTEGER,
"Forum" varchar(255) CHARACTER SET UTF8,
"Description" varchar(255) CHARACTER SET UTF8,
"ImageUrl" varchar(128),
"Styles" varchar(255),
"ParentID" INTEGER,
"PollGroupID" INTEGER,
"IsUserForum" BOOL,
"Topics" INTEGER,
"Posts" INTEGER,
"Flags" INTEGER,
"Viewing" INTEGER,
"RemoteURL" varchar(255) CHARACTER SET UTF8,
"ReadAccess" INTEGER,
"LastTopicID" INTEGER,
"LastPosted" timestamp,
"LastMessageID" INTEGER,
"LastMessageFlags" INTEGER,
"LastTopicName" varchar(255) CHARACTER SET UTF8,
"LastTopicStatus" varchar(255) CHARACTER SET UTF8,
"LastTopicStyles" varchar(255),
"TopicMovedID" INTEGER,
"LastUserID" INTEGER, 
"LastUser" varchar(128) CHARACTER SET UTF8,
"LastUserDisplayName" varchar(128) CHARACTER SET UTF8,
"Style" varchar(255) CHARACTER SET UTF8,
"LastForumAccess" timestamp,
"LastTopicAccess" timestamp
)
as
DECLARE VARIABLE ici_LastPosted timestamp;
DECLARE VARIABLE ici_LastMessageID integer default 2;
DECLARE VARIABLE ici_LastMessageFlags integer default 23;
DECLARE VARIABLE ici_LastTopicName varchar(128) default '';
DECLARE VARIABLE ici_LastUserID integer default 2; 
DECLARE VARIABLE ici_LastTopicID integer default 2;
DECLARE VARIABLE ici_LastUserName varchar(128) default '';
DECLARE VARIABLE ici_LastUserDisplayName varchar(128) default '';
DECLARE VARIABLE ici_TopicMovedID integer default 0;


begin
ici_LastPosted = :I_UTCTIMESTAMP;


-- get parent forums list first
insert into {objectQualifier}TBL1(ForumID,ParentID)
select 	
        b.FORUMID,
        b.PARENTID		
    from 
        {objectQualifier}CATEGORY a
        join {objectQualifier}FORUM b   on b.CATEGORYID=a.CATEGORYID
        join {objectQualifier}ACTIVEACCESS x   on x.FORUMID=b.FORUMID	
    where 
        a.BOARDID = :I_BOARDID and
        (b.ISHIDDEN=0 or x.READACCESS<>0) and
        (:I_CATEGORYID is null or a.CATEGORYID=:I_CATEGORYID) and
        ((:I_PARENTID is null and b.PARENTID is null) or b.PARENTID = :I_PARENTID) and
        x.USERID = :I_USERID
            order by
        a.SORTORDER,
        b.SORTORDER;
            
-- child forums
insert into {objectQualifier}TBL(ForumID,ParentID)
select 	
        b.FORUMID,
        b.PARENTID		
    from 
        {objectQualifier}CATEGORY a
        join {objectQualifier}FORUM b   on b.CATEGORYID=a.CATEGORYID
        join {objectQualifier}ACTIVEACCESS x   on x.FORUMID=b.FORUMID	
    where 
        a.BOARDID = :I_BOARDID and
        (b.ISHIDDEN=0 or x.READACCESS<>0) and
        (:I_CATEGORYID is null or a.CATEGORYID=:I_CATEGORYID) and
        (b.PARENTID IN (SELECT FORUMID FROM {objectQualifier}TBL1)) and
        x.USERID = :I_USERID
        order by
        a.SORTORDER,
        b.SORTORDER;

 insert into {objectQualifier}TBL(FORUMID,PARENTID)
  select * FROM {objectQualifier}TBL1;

    for	select 
        a.CategoryID, 
        a.NAME AS Category, 
        b.ForumID AS ForumID,
        b.NAME AS Forum, 
        b.Description,
        b.ImageUrl,
        b.Styles,
        b.ParentID,
        b.PollGroupID,
        b.IsUserForum,
        (SELECT I_NUMTOPICS FROM {objectQualifier}FORUM_TOPICS(b.FORUMID)),
        (SELECT I_NUMPOSTS FROM {objectQualifier}FORUM_POSTS(b.FORUMID)),			
        t.LastPosted AS LastPosted,
        t.LastMessageID AS LastMessageID,
        t.LASTMESSAGEFLAGS AS LastMessageFlags,
        t.LASTUSERID AS LastUserID,
        b.LastUserName,
        b.LastUserDisplayName,
        t.TOPICID AS LastTopicID,
        t.TOPICMOVEDID AS TopicMovedID,
        t.TOPIC AS LastTopicName,
        t.STATUS,
        t.STYLES,
        b.FLAGS,
        (select count(1) from {objectQualifier}ACTIVE x JOIN {objectQualifier}USER usr ON x.USERID = usr.USERID where x.FORUMID=b.FORUMID AND usr.ISACTIVEEXCLUDED = 0) AS Viewing,
        b.REMOTEURL,		
        (CAST(x.READACCESS as INTEGER)),
        (case(:I_STYLEDNICKS)
            when 1 then  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = t.LASTUSERID)  
            else (select '' from rdb$database)	 END),
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=b.FORUMID AND x.USERID = x.USERID)
             else (select :I_UTCTIMESTAMP  FROM RDB$DATABASE) END) AS "LastForumAccess",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=t.TOPICID AND y.USERID = x.USERID)
             else (select :I_UTCTIMESTAMP  FROM RDB$DATABASE)  END) AS  "LastTopicAccess"    				
    from 
        {objectQualifier}CATEGORY a
        join {objectQualifier}FORUM b on b.CATEGORYID=a.CATEGORYID
        join {objectQualifier}ACTIVEACCESS x on x.FORUMID=b.FORUMID
        left outer join {objectQualifier}TOPIC t ON t.TopicID = (select * from {objectQualifier}FORUM_LASTTOPIC(b.ForumID, :I_USERID,b.LastTopicID,b.LastPosted))
    where 		
        (:I_CATEGORYID is null or a.CATEGORYID=:I_CATEGORYID) and		
         x.USERID = :I_USERID and		
        (b.FORUMID IN (SELECT FORUMID FROM {objectQualifier}TBL) )
    order by
        a.SORTORDER,
        b.SORTORDER
        INTO		
         :"CategoryID",
         :"Category",
         :"ForumID",
         :"Forum",
         :"Description",		
         :"ImageUrl",
         :"Styles",
         :"ParentID",
         :"PollGroupID",
         :"IsUserForum",
         :"Topics",
         :"Posts", 
         :"LastPosted",
         :"LastMessageID",
         :"LastMessageFlags",
         :"LastUserID",
         :"LastUser",
         :"LastUserDisplayName",
         :"LastTopicID",
         :"TopicMovedID",
         :"LastTopicName",
         :"LastTopicStatus",
         :"LastTopicStyles",
         :"Flags",
         :"Viewing",
         :"RemoteURL",
         :"ReadAccess",
         :"Style",
         :"LastForumAccess",
         :"LastTopicAccess"
         DO 
         BEGIN
         IF (:"LastUser" IS NULL OR CHAR_LENGTH(:"LastUser") < 2) THEN
         select x.NAME, x.DISPLAYNAME from {objectQualifier}USER x 
         where x.USERID=:"LastUserID" INTO :"LastUser",:"LastUserDisplayName";		
         SUSPEND; 
         END
END;
--GO

create procedure {objectQualifier}FORUM_LISTREADPERSONAL(
I_BOARDID INTEGER,
I_USERID INTEGER,
I_CATEGORYID INTEGER,
I_PARENTID INTEGER,
I_STYLEDNICKS INTEGER,
I_FINDLASTUNREAD BOOL,
I_SHOWCOMMONFORUMS BOOL,
I_SHOWPERSONALFORUMS BOOL,
I_FORUMCREATEDBYUSERID INTEGER,
I_UTCTIMESTAMP TIMESTAMP) 
RETURNS
(
"CategoryID" INTEGER,
"Category" varchar(255) CHARACTER SET UTF8,
"ForumID" INTEGER,
"Forum" varchar(255) CHARACTER SET UTF8,
"Description" varchar(255) CHARACTER SET UTF8,
"ImageUrl" varchar(128),
"Styles" varchar(255),
"ParentID" INTEGER,
"PollGroupID" INTEGER,
"IsUserForum" BOOL,
"Topics" INTEGER,
"Posts" INTEGER,
"Flags" INTEGER,
"Viewing" INTEGER,
"RemoteURL" varchar(255) CHARACTER SET UTF8,
"ReadAccess" INTEGER,
"LastTopicID" INTEGER,
"LastPosted" timestamp,
"LastMessageID" INTEGER,
"LastMessageFlags" INTEGER,
"LastTopicName" varchar(255) CHARACTER SET UTF8,
"LastTopicStatus" varchar(255) CHARACTER SET UTF8,
"LastTopicStyles" varchar(255),
"TopicMovedID" INTEGER,
"LastUserID" INTEGER, 
"LastUser" varchar(128) CHARACTER SET UTF8,
"LastUserDisplayName" varchar(128) CHARACTER SET UTF8,
"Style" varchar(255) CHARACTER SET UTF8,
"LastForumAccess" timestamp,
"LastTopicAccess" timestamp
)
as
DECLARE VARIABLE ici_LastPosted timestamp;
DECLARE VARIABLE ici_LastMessageID integer default 2;
DECLARE VARIABLE ici_LastMessageFlags integer default 23;
DECLARE VARIABLE ici_LastTopicName varchar(128) default '';
DECLARE VARIABLE ici_LastUserID integer default 2; 
DECLARE VARIABLE ici_LastTopicID integer default 2;
DECLARE VARIABLE ici_LastUserName varchar(128) default '';
DECLARE VARIABLE ici_LastUserDisplayName varchar(128) default '';
DECLARE VARIABLE ici_TopicMovedID integer default 0;


begin
ici_LastPosted = :I_UTCTIMESTAMP;


    for	select 
        a.CategoryID, 
        a.NAME AS Category, 
        b.ForumID AS ForumID,
        b.NAME AS Forum, 
        b.Description,
        b.ImageUrl,
        b.Styles,
        b.ParentID,
        b.PollGroupID,
        b.IsUserForum,
        (SELECT I_NUMTOPICS FROM {objectQualifier}FORUM_TOPICS(b.FORUMID)),
        (SELECT I_NUMPOSTS FROM {objectQualifier}FORUM_POSTS(b.FORUMID)),			
        t.LastPosted AS LastPosted,
        t.LastMessageID AS LastMessageID,
        t.LASTMESSAGEFLAGS AS LastMessageFlags,
        t.LASTUSERID AS LastUserID,
        b.LastUserName,
        b.LastUserDisplayName,
        t.TOPICID AS LastTopicID,
        t.TOPICMOVEDID AS TopicMovedID,
        t.TOPIC AS LastTopicName,
        t.STATUS,
        t.STYLES,
        b.FLAGS,
        (select count(1) from {objectQualifier}ACTIVE x JOIN {objectQualifier}USER usr ON x.USERID = usr.USERID where x.FORUMID=b.FORUMID AND usr.ISACTIVEEXCLUDED = 0) AS Viewing,
        b.REMOTEURL,		
        (CAST(x.READACCESS as INTEGER)),
        (case(:I_STYLEDNICKS)
            when 1 then  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = t.LASTUSERID)  
            else (select '' from rdb$database)	 END),
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=b.FORUMID AND x.USERID = x.USERID)
             else (select :I_UTCTIMESTAMP  FROM RDB$DATABASE) END) AS "LastForumAccess",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=t.TOPICID AND y.USERID = x.USERID)
             else (select :I_UTCTIMESTAMP  FROM RDB$DATABASE)  END) AS  "LastTopicAccess"    				
    from 
        {objectQualifier}CATEGORY a
        join {objectQualifier}FORUM b on b.CATEGORYID=a.CATEGORYID
        join {objectQualifier}ACTIVEACCESS x on x.FORUMID=b.FORUMID
        left outer join {objectQualifier}TOPIC t ON t.TopicID = (select * from {objectQualifier}FORUM_LASTTOPIC(b.ForumID, :I_USERID,b.LastTopicID,b.LastPosted))
    where 
		a.BOARDID = :I_BOARDID and
        (b.ISHIDDEN=0 or x.READACCESS<>0) and	
        (:I_CATEGORYID is null or a.CATEGORYID=:I_CATEGORYID) and		
         x.USERID = :I_USERID and		
         b.CREATEDBYUSERID = :I_FORUMCREATEDBYUSERID AND b.ISUSERFORUM = 1 
    order by
        a.SORTORDER,
        b.SORTORDER
        INTO		
         :"CategoryID",
         :"Category",
         :"ForumID",
         :"Forum",
         :"Description",		
         :"ImageUrl",
         :"Styles",
         :"ParentID",
         :"PollGroupID",
         :"IsUserForum",
         :"Topics",
         :"Posts", 
         :"LastPosted",
         :"LastMessageID",
         :"LastMessageFlags",
         :"LastUserID",
         :"LastUser",
         :"LastUserDisplayName",
         :"LastTopicID",
         :"TopicMovedID",
         :"LastTopicName",
         :"LastTopicStatus",
         :"LastTopicStyles",
         :"Flags",
         :"Viewing",
         :"RemoteURL",
         :"ReadAccess",
         :"Style",
         :"LastForumAccess",
         :"LastTopicAccess"
         DO 
         BEGIN
         IF (:"LastUser" IS NULL OR CHAR_LENGTH(:"LastUser") < 2) THEN
         select x.NAME, x.DISPLAYNAME from {objectQualifier}USER x 
         where x.USERID=:"LastUserID" INTO :"LastUser",:"LastUserDisplayName";		
         SUSPEND; 
         END
END;
--GO

CREATE PROCEDURE {objectQualifier}TOPIC_LATEST
(
    I_BOARDID INTEGER,
    I_NUMPOSTS INTEGER,
    I_PAGEUSERID INTEGER,
    I_STYLEDNICKS BOOL,
    I_SHOWNOCOUNTPOSTS BOOL,
    I_FINDLASTUNREAD BOOL,
	I_UTCTIMESTAMP TIMESTAMP
)
RETURNS
(
        "LastPosted" timestamp,
        "ForumID" INTEGER,
        "Forum" varchar(128),
        "Topic" varchar(128),
        "Status" varchar(255),
        "Styles" varchar(255),
        "TopicID" INTEGER,
        "TopicMovedID" INTEGER,
        "UserID" INTEGER,
        "UserName" VARCHAR(128),
        "UserDisplayName" VARCHAR(128),
        "StarterIsGuest" BOOL,
        "LastMessageID" INTEGER,
        "LastMessageFlags" INTEGER,
        "LastUserID" INTEGER,
        "NumPosts" INTEGER,	
        "Posted" TIMESTAMP,
        "LastUserName" varchar(128),
        "LastUserDisplayName" varchar(128),
        "LastUserStyle"  varchar(255),
        "LastUserIsGuest" BOOL,	
        "LastForumAccess" timestamp,
        "LastTopicAccess" timestamp		
)
AS
BEGIN	
    FOR 
    SELECT FIRST(:I_NUMPOSTS)
        t.LASTPOSTED,
        t.FORUMID,
        f.NAME as "Forum",
        t.TOPIC,
        t.STATUS,
        t.STYLES,
        t.TOPICID,
        t.TOPICMOVEDID,
        t.USERID,
        t.USERNAME,	
        t.USERDISPLAYNAME,
        (select x.ISGUEST from {objectQualifier}USER x 
        where x.USERID = t.USERID),	
        t.LASTMESSAGEID,
        t.LASTMESSAGEFLAGS,
        t.LASTUSERID,
        t.NUMPOSTS,
        t.POSTED,
        COALESCE(t.LASTUSERNAME,
        (select x.NAME from {objectQualifier}USER x 
        where x.USERID = t.LASTUSERID)) AS "LastUserName",
        COALESCE(t.LASTUSERNAME,
        (select x.DISPLAYNAME from {objectQualifier}USER x 
        where x.USERID = t.LASTUSERID)) AS "LastUserDisplayName",
        case(:I_STYLEDNICKS)
            when 1 then (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = t.LASTUSERID)  
            else (SELECT '' FROM RDB$DATABASE)	 END,
        (select x.ISGUEST from {objectQualifier}USER x 
        where x.USERID = t.LASTUSERID) AS "LastUserIsGuest",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=f.FORUMID AND x.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE) END) AS "LastForumAccess",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=t.TOPICID AND y.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE)  END) AS  "LastTopicAccess"  
    FROM 
        {objectQualifier}TOPIC t
    INNER JOIN
        {objectQualifier}FORUM f ON t.FORUMID = f.FORUMID	
    INNER JOIN
        {objectQualifier}CATEGORY c ON c.CATEGORYID = f.CATEGORYID
    JOIN
        {objectQualifier}ACTIVEACCESS v ON v.FORUMID=f.FORUMID
    WHERE
        c.BOARDID = :I_BOARDID
        AND t.TOPICMOVEDID is NULL
        AND v.USERID=:I_PAGEUSERID
        AND (v.READACCESS <> 0)
        AND t.ISDELETED != 1
        AND t.LASTPOSTED IS NOT NULL		AND
        BIN_AND(f.Flags,4) <> (CASE WHEN :I_SHOWNOCOUNTPOSTS > 0 THEN -1 ELSE 4 END)		
    ORDER BY
        t.LASTPOSTED DESC
        INTO
        :"LastPosted",
        :"ForumID",
        :"Forum",
        :"Topic",
        :"Status",
        :"Styles",
        :"TopicID",
        :"TopicMovedID",
        :"UserID",
        :"UserName",
        :"UserDisplayName",
        :"StarterIsGuest",
        :"LastMessageID",
        :"LastMessageFlags",
        :"LastUserID",
        :"NumPosts",
        :"Posted",		
        :"LastUserName",
        :"LastUserDisplayName",
        :"LastUserStyle",
        :"LastUserIsGuest",
        :"LastForumAccess",
        :"LastTopicAccess"
        DO SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}RSS_TOPIC_LATEST
(
    I_BOARDID INTEGER,
    I_NUMPOSTS INTEGER,
    I_PAGEUSERID INTEGER,
    I_STYLEDNICKS BOOL,
    I_SHOWNOCOUNTPOSTS BOOL
)
RETURNS
(
        "LastMessage" blob sub_type 1,
        "LastPosted" timestamp,
        "ForumID" INTEGER,
        "Forum" varchar(128),
        "Topic" varchar(128),
        "TopicID" INTEGER,
        "TopicMovedID" INTEGER,
        "UserID" INTEGER,
        "UserName" varchar(128),
        "UserDisplayName" varchar(128),
        "StarterIsGuest" BOOL,
        "LastMessageID" INTEGER,
        "LastMessageFlags" INTEGER,
        "LastUserID" INTEGER,
        "Posted" timestamp,		
        "LastUserName" varchar(128),
        "LastUserDisplayName" varchar(128),
        "LastUserIsGuest" BOOL
)
AS
BEGIN	
    FOR 
    SELECT FIRST(:I_NUMPOSTS)
        m.Message,
        t.LASTPOSTED,
        t.FORUMID,
        f.NAME as "Forum",
        t.TOPIC,
        t.TOPICID,
        t.TOPICMOVEDID,
        t.USERID,
        t.USERNAME,
        t.USERDISPLAYNAME,
        (select x.ISGUEST from {objectQualifier}USER x 
        where x.USERID = t.USERID),		
        t.LASTMESSAGEID,
        t.LASTMESSAGEFLAGS,
        t.LASTUSERID,
        t.POSTED,
        COALESCE(t.LASTUSERNAME,
        (select x.NAME from {objectQualifier}USER x 
        where x.USERID = t.LASTUSERID)) AS "LastUserName",
        COALESCE(t.LASTUSERDISPLAYNAME,
        (select x.DISPLAYNAME from {objectQualifier}USER x 
        where x.USERID = t.LASTUSERID)) AS "LastUserDisplayName",		
        (select x.ISGUEST from {objectQualifier}USER x 
        where x.USERID = t.LASTUSERID) AS "LastUserIsGuest"
    FROM
        {objectQualifier}MESSAGE m 
    INNER JOIN	
        {objectQualifier}TOPIC t ON t.LASTMESSAGEID = m.MESSAGEID
    INNER JOIN
        {objectQualifier}FORUM f ON t.FORUMID = f.FORUMID	
    INNER JOIN
        {objectQualifier}CATEGORY c ON c.CATEGORYID = f.CATEGORYID
    JOIN
        {objectQualifier}ACTIVEACCESS v ON v.FORUMID=f.FORUMID
    WHERE
        c.BOARDID = :I_BOARDID
        AND t.TOPICMOVEDID is NULL
        AND v.USERID=:I_PAGEUSERID
        AND (v.READACCESS <> 0)
        AND t.ISDELETED != 1
        AND t.LASTPOSTED IS NOT NULL		
        AND
        BIN_AND(f.Flags,4) <> (CASE WHEN :I_SHOWNOCOUNTPOSTS > 0 THEN -1 ELSE 4 END)
    ORDER BY
        t.LASTPOSTED DESC
        INTO
        :"LastMessage",
        :"LastPosted",
        :"ForumID",
        :"Forum",
        :"Topic",
        :"TopicID",
        :"TopicMovedID",
        :"UserID",
        :"UserName",
        :"UserDisplayName",
        :"StarterIsGuest",
        :"LastMessageID",
        :"LastMessageFlags",
        :"LastUserID",		
        :"Posted",		
        :"LastUserName",
        :"LastUserDisplayName",
        :"LastUserIsGuest"
        DO SUSPEND;
END;
--GO

create procedure {objectQualifier}ACTIVE_LIST(
I_BOARDID INTEGER,
I_GUESTS BOOL, 
I_SHOWCRAWLERS BOOL,
I_INTERVAL INTEGER, 
I_STYLEDNICKS BOOL, 
I_UTCTIMESTAMP TIMESTAMP) 
RETURNS
(
        "UserID" INTEGER,
        "UserName" VARCHAR(128),
        "UserDisplayName" VARCHAR(128),
        "IP" VARCHAR(39),
        "SessionID" VARCHAR(36),
        "ForumID" INTEGER,
        "TopicID" INTEGER,
        "ForumName" VARCHAR(128),
        "TopicName" VARCHAR(128),
        "IsGuest" SMALLINT,
        "IsCrawler" SMALLINT,
        "IsHidden" SMALLINT,
        "Style"  VARCHAR(255),
        "UserCount" INTEGER,
        "Login" TIMESTAMP,
        "LastActive" TIMESTAMP,
        "Location" VARCHAR(128),
        "Active" INTEGER,
        "Browser" VARCHAR(128),
        "Platform" VARCHAR(128),
        "ForumPage" VARCHAR(255)
)
as
begin

    -- delete non-active
    delete from {objectQualifier}ACTIVE where DATEDIFF(MINUTE FROM LASTACTIVE TO :I_UTCTIMESTAMP)>:I_INTERVAL; 
    delete from {objectQualifier}ACTIVEACCESS where DATEDIFF(MINUTE FROM LASTACTIVE TO :I_UTCTIMESTAMP)>:I_INTERVAL AND ISGUESTX = 0;	

    -- select active
    if (I_GUESTS<>0) THEN
        for select 
            a.USERID,
            a.NAME AS "UserName",
            a.DISPLAYNAME,
            c.IP,
            c.SESSIONID,
            c.FORUMID,
            c.TOPICID,
            (select NAME from {objectQualifier}FORUM x where x.FORUMID=c.FORUMID) AS "ForumName" ,
            (select TOPIC from {objectQualifier}TOPIC x where x.TOPICID=c.TOPICID) AS "TopicName",
            c.ISGUEST  AS ISGUEST,
            c.ISCRAWLER AS ISCRAWLER,
            a.ISACTIVEEXCLUDED AS "IsHidden",
             CASE :I_STYLEDNICKS
             WHEN 1 THEN  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = a.USERID)                   
              ELSE (SELECT '' FROM RDB$DATABASE)
            END,
            /*	CASE :I_STYLEDNICKS
             WHEN 1 THEN  (SELECT FIRST 1 f.STYLE FROM {objectQualifier}USERGROUP e 
            JOIN {objectQualifier}GROUP f ON f.GROUPID=e.GROUPID WHERE e.USERID=a.USERID AND CHAR_LENGTH(f.STYLE) > 2 ORDER BY f.SORTORDER), r.STYLE)  
            ELSE (SELECT '' FROM RDB$DATABASE) END, */			 					
            (SELECT 1 FROM RDB$DATABASE) AS "UserCount",
            c.LOGIN,
            c.LASTACTIVE,
            c.LOCATION,
            DATEDIFF(MINUTE FROM  c.LOGIN TO  c.LASTACTIVE) AS "Active",
            c.BROWSER,
            c.PLATFORM,			
            c.FORUMPAGE
        from
            {objectQualifier}USER a
            -- JOIN {objectQualifier}RANK r on r.RANKID=a.RANKID	
            inner join {objectQualifier}ACTIVE c ON c.USERID = a.USERID
        where
            c.BOARDID = :I_BOARDID
        order by
            c.LASTACTIVE desc
            INTO
        :"UserID",
        :"UserName",
        :"UserDisplayName",
        :"IP",
        :"SessionID",
        :"ForumID",
        :"TopicID",
        :"ForumName",
        :"TopicName",
        :"IsGuest",
        :"IsCrawler",
        :"IsHidden",
        :"Style",
        :"UserCount",
        :"Login",
        :"LastActive",
        :"Location",
        :"Active",
        :"Browser",
        :"Platform",
        :"ForumPage"
        DO SUSPEND;
    else if (:I_SHOWCRAWLERS = 1 and :I_GUESTS = 0) THEN
            for select 
            a.USERID,
            a.NAME AS "UserName",
            a.DISPLAYNAME,
            c.IP,
            c.SESSIONID,
            c.FORUMID,
            c.TOPICID,
            (select NAME from {objectQualifier}FORUM x where x.FORUMID=c.FORUMID) AS "ForumName" ,
            (select TOPIC from {objectQualifier}TOPIC x where x.TOPICID=c.TOPICID) AS "TopicName",
            c.ISGUEST  AS ISGUEST,
            c.ISCRAWLER AS ISCRAWLER,
            a.ISACTIVEEXCLUDED AS "IsHidden",
            CASE :I_STYLEDNICKS
            WHEN 1 THEN  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = a.USERID)                     
            ELSE (SELECT '' FROM RDB$DATABASE)
            END,
            /*	CASE :I_STYLEDNICKS
             WHEN 1 THEN  (SELECT FIRST 1 f.STYLE FROM {objectQualifier}USERGROUP e 
            JOIN {objectQualifier}GROUP f ON f.GROUPID=e.GROUPID WHERE e.USERID=a.USERID AND CHAR_LENGTH(f.STYLE) > 2 ORDER BY f.SORTORDER), r.STYLE)  
            ELSE (SELECT '' FROM RDB$DATABASE) END, */			
            (SELECT 1 FROM RDB$DATABASE) AS "UserCount",
            c.LOGIN,
            c.LASTACTIVE,
            c.LOCATION,
            DATEDIFF(MINUTE FROM  c.LOGIN TO  c.LASTACTIVE) AS "Active",
            c.BROWSER,
            c.PLATFORM,			
            c.FORUMPAGE
        from
            {objectQualifier}USER a
            -- JOIN {objectQualifier}RANK r on r.RANKID=a.RANKID	
            inner join {objectQualifier}ACTIVE c ON c.USERID = a.USERID
        where
            c.BOARDID = :I_BOARDID AND
            (BIN_AND(c.Flags, 4) = 4 OR BIN_AND(c.Flags, 8) = 8)			
        order by
            c.LASTACTIVE desc
            INTO
        :"UserID",
        :"UserName",
        :"UserDisplayName",
        :"IP",
        :"SessionID",
        :"ForumID",
        :"TopicID",
        :"ForumName",
        :"TopicName",
        :"IsGuest",
        :"IsCrawler",
        :"IsHidden",
        :"Style",
        :"UserCount",
        :"Login",
        :"LastActive",
        :"Location",
        :"Active",
        :"Browser",
        :"Platform",
        :"ForumPage"
        DO SUSPEND;
    else
    for	select
            a.USERID,
            a.NAME AS "UserName",
            a.DISPLAYNAME,
            c.IP,
            c.SESSIONID,
            c.FORUMID,
            c.TOPICID,
            (select NAME from {objectQualifier}FORUM x where x.FORUMID=c.FORUMID) AS "ForumName",
            (select TOPIC from {objectQualifier}TOPIC x where x.TOPICID=c.TOPICID) AS "TopicName",
            c.ISGUEST  AS ISGUEST,
             c.ISCRAWLER AS ISCRAWLER,
             a.ISACTIVEEXCLUDED AS "IsHidden" ,
             CASE :I_STYLEDNICKS
             WHEN 1 THEN  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = a.USERID)                    
              ELSE (SELECT '' FROM RDB$DATABASE)
            END,
            /*	CASE :I_STYLEDNICKS
             WHEN 1 THEN  (SELECT FIRST 1 f.STYLE FROM {objectQualifier}USERGROUP e 
            JOIN {objectQualifier}GROUP f ON f.GROUPID=e.GROUPID WHERE e.USERID=a.USERID AND CHAR_LENGTH(f.STYLE) > 2 ORDER BY f.SORTORDER), r.STYLE)  
            ELSE (SELECT '' FROM RDB$DATABASE) END, */		
            (SELECT 1 FROM RDB$DATABASE) AS "UserCount",
            c.LOGIN,
            c.LASTACTIVE,
            c.LOCATION,
            DATEDIFF(MINUTE FROM  c.LOGIN TO  c.LASTACTIVE) AS "Active",
            c.BROWSER,
            c.PLATFORM,
            c.FORUMPAGE
        from
            {objectQualifier}USER a
            -- JOIN {objectQualifier}RANK r on r.RANKID=a.RANKID	
            INNER JOIN {objectQualifier}ACTIVE c ON c.USERID = a.USERID
        where
            c.BOARDID = :I_BOARDID and
            not (exists(
                select FIRST 1 1
                    from {objectQualifier}USERGROUP x
                        inner join {objectQualifier}GROUP y ON y.GROUPID=x.GROUPID 
                    where x.USERID=a.USERID and BIN_AND(y.FLAGS, 2)<>0
                ))
        order by
            c.LASTACTIVE desc
            INTO
        :"UserID",
        :"UserName",
        :"UserDisplayName",
        :"IP",
        :"SessionID",
        :"ForumID",
        :"TopicID",
        :"ForumName",
        :"TopicName",
        :"IsGuest",
        :"IsCrawler",
        :"IsHidden",
        :"Style",
        :"UserCount",
        :"Login",
        :"LastActive",
        :"Location",
        :"Active",
        :"Browser",
        :"Platform",
        :"ForumPage"
        DO SUSPEND;
END;
--GO

create procedure {objectQualifier}ACTIVE_STATS(I_BOARDID INTEGER) 
RETURNS ("ActiveUsers" INTEGER,
"ActiveMembers" INTEGER,
"ActiveGuests" INTEGER,
"ActiveHidden" INTEGER)
as
begin

        SELECT 
        (select count(1) from {objectQualifier}ACTIVE x 
        JOIN {objectQualifier}USER usr 
        ON x.USERID = usr.USERID 
        where x.BOARDID = :I_BOARDID 
        AND usr.ISACTIVEEXCLUDED = 0),
        (select count(1) from {objectQualifier}ACTIVE x JOIN {objectQualifier}USER usr ON x.USERID = usr.USERID where x.BOARDID = :I_BOARDID and (exists(select 1 from {objectQualifier}USERGROUP y 
         inner join {objectQualifier}GROUP z 
         on y.GROUPID=z.GROUPID 
         where y.USERID=x.USERID 
         and BIN_AND(z.FLAGS, 2)=0  
         AND usr.ISACTIVEEXCLUDED = 0))),
        (select count(1) from {objectQualifier}ACTIVE x where x.BOARDID = :I_BOARDID 
        and (exists(select 1 from {objectQualifier}USERGROUP y 
        inner join {objectQualifier}GROUP z on y.GROUPID=z.GROUPID 
        where y.USERID=x.USERID and BIN_AND(z.FLAGS, 2)<>0))),
        (select count(1) from {objectQualifier}ACTIVE x 
        JOIN {objectQualifier}USER usr 
        ON x.USERID = usr.USERID 
        where x.BOARDID = :I_BOARDID 
        and (exists(select 1 from {objectQualifier}USERGROUP y
        JOIN {objectQualifier}GROUP z		
        on y.GROUPID=z.GROUPID 
        where y.USERID=x.USERID 
        and BIN_AND(z.FLAGS, 2)=0  
        AND usr.ISACTIVEEXCLUDED = 1)))
        FROM RDB$DATABASE
        INTO
        "ActiveUsers",
        "ActiveMembers",
        "ActiveGuests",
        "ActiveHidden";
        SUSPEND;
        END;
--GO

create procedure {objectQualifier}BOARD_USERSTATS(I_BOARDID INTEGER, I_STYLEDNICKS BOOL) 
RETURNS 
(
"Members" INTEGER,
"MaxUsers" INTEGER,
"MaxUsersWhen" timestamp,
"LastMemberInfoID" INTEGER,
"LastMemberID" INTEGER,
"LastMember" varchar(128),
"LastMemberDisplayName" varchar(128)
)
as
BEGIN
    SELECT
        (SELECT count(1) from {objectQualifier}USER a where a.BOARDID=:I_BOARDID AND BIN_AND(FLAGS, 2) = 2 AND BIN_AND(a.FLAGS, 4) = 0) AS "Members",
        (SELECT CAST("VALUE" as varchar(255)) FROM {objectQualifier}REGISTRY WHERE LOWER(NAME) = LOWER('maxusers') and BOARDID=:I_BOARDID) AS "MaxUsers",
        (SELECT CAST("VALUE" as varchar(255)) FROM {objectQualifier}REGISTRY WHERE LOWER(NAME) = LOWER('maxuserswhen') and BOARDID=:I_BOARDID) AS "MaxUsersWhen",	
        LastMemberInfo.*
    FROM
        (
            SELECT FIRST 1 
            (SELECT 1 FROM RDB$DATABASE) AS	"LastMemberInfoID",
                USERID AS "LastMemberID",
                NAME AS "LastMember",
                DISPLAYNAME AS "LastMemberDisplayName"
            FROM 
                {objectQualifier}USER
            WHERE 
                BIN_AND(FLAGS, 2) = 2
                AND BIN_AND(FLAGS, 4) = 0
                AND BOARDID = :I_BOARDID 
            ORDER BY 
                JOINED DESC
        ) as LastMemberInfo		
        INTO	
        :"Members",
        :"MaxUsers",
        :"MaxUsersWhen",
        :"LastMemberInfoID",
        :"LastMemberID",
        :"LastMember",
        :"LastMemberDisplayName";
SUSPEND;
END; 
--GO
create procedure {objectQualifier}BOARD_POSTSTATS(
I_BOARDID INTEGER, 
I_STYLEDNICKS BOOL, 
I_SHOWNOCOUNTPOSTS BOOL, 
I_GETDEFAULTS BOOL,
I_UTCTIMESTAMP TIMESTAMP
) 
RETURNS 
(
"Posts" INTEGER,
"Topics" INTEGER,
"Forums" INTEGER,
"LastPostInfoID" INTEGER,
"LastPost" timestamp,
"LastUserID" INTEGER,
"LastUser" varchar(128),
"LastUserDisplayName" varchar(128),
"LastUserStyle"  varchar(255)
)
as
BEGIN
-- vzrus: while  a new installation we don have the row and should return a dummy data
IF (I_GETDEFAULTS = 0) THEN
BEGIN
    SELECT FIRST 1
        (SELECT count(1) from {objectQualifier}MESSAGE a join {objectQualifier}TOPIC b on b.TOPICID=a.TOPICID join {objectQualifier}FORUM c on c.FORUMID=b.FORUMID join {objectQualifier}CATEGORY d on d.CATEGORYID=c.CATEGORYID where d.BOARDID=:I_BOARDID AND BIN_AND(a.FLAGS, 24)=16),
        (SELECT count(1) from {objectQualifier}TOPIC a join {objectQualifier}FORUM b on b.FORUMID=a.FORUMID join {objectQualifier}CATEGORY c on c.CATEGORYID=b.CATEGORYID where c.BOARDID=:I_BOARDID AND BIN_AND(a.FLAGS, 8) <> 8),
        (SELECT count(1) from {objectQualifier}FORUM a join {objectQualifier}CATEGORY b on b.CATEGORYID=a.CATEGORYID where b.BOARDID=:I_BOARDID),		
        (SELECT 1 FROM RDB$DATABASE) AS "LastPostInfoID",
        a.POSTED AS "LastPost",
        a.USERID AS LASTUSERID,
        e.NAME AS "LastUser",
        e.DISPLAYNAME,
        (CASE WHEN :I_STYLEDNICKS > 0 THEN e.USERSTYLE  ELSE '' END) AS "LastUserStyle"	
            FROM 
                {objectQualifier}MESSAGE a 
                join {objectQualifier}TOPIC b on b.TOPICID=a.TOPICID 
                join {objectQualifier}FORUM c on c.FORUMID=b.FORUMID 
                join {objectQualifier}CATEGORY d on d.CATEGORYID=c.CATEGORYID 
                join {objectQualifier}USER e on e.USERID=a.USERID
                JOIN {objectQualifier}RANK r on r.RANKID=e.RANKID
            WHERE 
                BIN_AND(a.FLAGS, 24) = 16
                AND b.ISDELETED = 0 
                AND d.BOARDID = :I_BOARDID
                AND c.ISNOCOUNT <> (CASE WHEN :I_SHOWNOCOUNTPOSTS > 0 THEN -1 ELSE 1 END)	
            ORDER BY
                a.POSTED DESC			
        INTO
        :"Posts",
        :"Topics",
        :"Forums",
        :"LastPostInfoID",
        :"LastPost",
        :"LastUserID",
        :"LastUser",
        :"LastUserDisplayName",
        :"LastUserStyle";
        END
        ELSE
        BEGIN
            SELECT
        (SELECT 0 FROM RDB$DATABASE),
        (SELECT 0 FROM RDB$DATABASE), 
        (SELECT 1 FROM RDB$DATABASE), 	
        (SELECT 1 FROM RDB$DATABASE),  
        (SELECT NULL FROM RDB$DATABASE), 
        (SELECT NULL FROM RDB$DATABASE), 
        (SELECT NULL FROM RDB$DATABASE),
        (SELECT NULL FROM RDB$DATABASE) ,
        (SELECT '' FROM RDB$DATABASE) 
            FROM 
                RDB$DATABASE
        INTO
        :"Posts",
        :"Topics",
        :"Forums",    
        :"LastPostInfoID",
        :"LastPost",
        :"LastUserID",
        :"LastUser",
        :"LastUserDisplayName",
        :"LastUserStyle";
        END
SUSPEND;
-- can be put in every place with slow update rate
DELETE FROM {objectQualifier}TOPIC WHERE TOPICMOVEDID IS NOT NULL AND LINKDATE < current_timestamp;
END;
--GO
CREATE  PROCEDURE {objectQualifier}USER_ACCESSMASKS(
     I_BOARDID INTEGER,
     I_USERID  INTEGER)
     RETURNS
     (
"AccessMaskID" integer,
"AccessMaskName" VARCHAR(128),
"ForumID" integer,
"ForumName" varchar(128),
"CategoryID" integer,
"ParentID" integer
)
     AS
     BEGIN
    FOR SELECT  
     x.ACCESSMASKID,
     x."AccessMaskName",
     x.FORUMID,
     x."ForumName",
     x.CATEGORYID,
     x.PARENTID
     FROM  
     
     (SELECT  
     e.ACCESSMASKID,
     e.NAME AS "AccessMaskName",
     f.FORUMID AS FORUMID,
     f.NAME AS "ForumName",
     f.CATEGORYID AS CATEGORYID,
     f.PARENTID AS PARENTID
     FROM     {objectQualifier}USER a
     JOIN {objectQualifier}USERGROUP b
     ON b.USERID = a.USERID
     JOIN {objectQualifier}GROUP c
     ON c.GROUPID = b.GROUPID
     JOIN {objectQualifier}FORUMACCESS d
     ON d.GROUPID = c.GROUPID
     JOIN {objectQualifier}ACCESSMASK e
     ON e.ACCESSMASKID = d.ACCESSMASKID
     JOIN {objectQualifier}FORUM f
     ON f.FORUMID = d.FORUMID
     WHERE    a.USERID = :I_USERID
     AND c.BOARDID = :I_BOARDID
     GROUP BY e.ACCESSMASKID,e.NAME,f.FORUMID,f.NAME,f.CATEGORYID,f.PARENTID
     
     UNION
     
     SELECT   c.ACCESSMASKID AS "AccessMaskID",
     c.NAME AS "AccessMaskName",
     d.FORUMID AS FORUMID,
     d.NAME AS  "ForumName",
     d.CATEGORYID AS "CategoryID",
     d.PARENTID AS PARENTID
     FROM     {objectQualifier}USER a
     JOIN {objectQualifier}USERFORUM b
     ON b.USERID = a.USERID
     JOIN {objectQualifier}ACCESSMASK c
     ON c.ACCESSMASKID = b.ACCESSMASKID
     JOIN {objectQualifier}FORUM d
     ON d.FORUMID = b.FORUMID
     WHERE    a.USERID = :I_USERID
     AND c.BOARDID = :I_BOARDID
     GROUP BY c.ACCESSMASKID,c.NAME,d.FORUMID,d.NAME,d.CATEGORYID,d.PARENTID) AS x 
     ORDER BY 4, 2, 1, 3, 5, 6    
     INTO
     :"AccessMaskID",
     :"AccessMaskName",
     :"ForumID",
     :"ForumName",
     :"CategoryID",
     :"ParentID"
      DO SUSPEND;
       -- "ForumName",
    --  "AccessMaskName","AccessMaskID","ForumID"
     END;   
--GO

CREATE  PROCEDURE {objectQualifier}USER_ACCESSMASKSBYFORUM(
     I_BOARDID INTEGER,
     I_USERID  INTEGER)
     RETURNS
     (
"AccessMaskID" integer,
"AccessMaskName" VARCHAR(128),
"AccessMaskFlags" integer,
"IsUserMask" bool,
"ForumID" integer,
"ForumName" varchar(128),
"CategoryID" integer,
"ParentID" integer,
"GroupID" integer,
"GroupName" varchar(128)
)
     AS
     BEGIN
    FOR SELECT  
     x.ACCESSMASKID,
     x."AccessMaskName",
     x.FLAGS,
     x."IsUserMask",
     x.FORUMID,
     x."ForumName",
     x.CATEGORYID,
     x.PARENTID,
     x.GROUPID,
     x.NAME     FROM  
     
     (SELECT  
     e.ACCESSMASKID,
     e.NAME AS "AccessMaskName",
     e.FLAGS,
     (SELECT 1 FROM RDB$DATABASE) as "IsUserMask",
     f.FORUMID AS FORUMID,
     f.NAME AS "ForumName",
     f.CATEGORYID AS CATEGORYID,
     f.PARENTID AS PARENTID,
     c.GROUPID,
     c.NAME
     FROM     {objectQualifier}USER a
     JOIN {objectQualifier}USERGROUP b
     ON b.USERID = a.USERID
     JOIN {objectQualifier}GROUP c
     ON c.GROUPID = b.GROUPID
     JOIN {objectQualifier}FORUMACCESS d
     ON d.GROUPID = c.GROUPID
     JOIN {objectQualifier}ACCESSMASK e
     ON e.ACCESSMASKID = d.ACCESSMASKID
     JOIN {objectQualifier}FORUM f
     ON f.FORUMID = d.FORUMID
     WHERE    a.USERID = :I_USERID
     AND c.BOARDID = :I_BOARDID
    
     
     UNION
     
     SELECT   c.ACCESSMASKID AS "AccessMaskID",
     c.NAME AS "AccessMaskName",
     c.FLAGS,
     (SELECT 0 FROM RDB$DATABASE) as "IsUserMask",
     d.FORUMID AS FORUMID,
     d.NAME AS  "ForumName",
     d.CATEGORYID AS "CategoryID",
     d.PARENTID AS PARENTID,
     (SELECT 0 FROM RDB$DATABASE),
     (SELECT '' FROM RDB$DATABASE)
     FROM     {objectQualifier}USER a
     JOIN {objectQualifier}USERFORUM b
     ON b.USERID = a.USERID
     JOIN {objectQualifier}ACCESSMASK c
     ON c.ACCESSMASKID = b.ACCESSMASKID
     JOIN {objectQualifier}FORUM d
     ON d.FORUMID = b.FORUMID
     WHERE    a.USERID = :I_USERID
     AND c.BOARDID = :I_BOARDID ) x
     INTO
     :"AccessMaskID",
     :"AccessMaskName",
     :"AccessMaskFlags", 
     :"IsUserMask",
     :"ForumID",
     :"ForumName",
     :"CategoryID",
     :"ParentID",
     :"GroupID",
     :"GroupName"
      DO SUSPEND;
    
     END;   
--GO

create procedure {objectQualifier}USER_GUEST
(
    I_BOARDID INTEGER
)
RETURNS
("UserID" INTEGER)
as
begin
    select FIRST 1
        a.USERID
    from
        {objectQualifier}USER a
        inner join {objectQualifier}USERGROUP b 
        on b.USERID = a.USERID
        inner join {objectQualifier}GROUP c 
        on b.GROUPID = c.GROUPID
    where
        a.BOARDID = :I_BOARDID and
        BIN_AND(c.FLAGS, 2)<>0
        INTO :"UserID";
        SUSPEND;
END;
--GO

-- END of required to see start page


create procedure {objectQualifier}FORUM_LISTPATH(I_FORUMID INTEGER) 
RETURNS
(
"ForumID" INTEGER,
"Name" VARCHAR(255)
)
as
begin
    -- supports up to 4 levels of nested forums
    for select
        a.FORUMID,
        a.NAME
    from
        (select
            a.FORUMID,
            (SELECT 0 FROM RDB$DATABASE) AS INDENT
        from
            {objectQualifier}FORUM a
        where
            a.FORUMID=:I_FORUMID

        union

        select
            b.FORUMID,
            (SELECT 1 FROM RDB$DATABASE) AS INDENT
        from
            {objectQualifier}FORUM a
            join {objectQualifier}FORUM b on b.FORUMID=a.PARENTID
        where
            a.FORUMID=:I_FORUMID

        union

        select
            c.FORUMID,
            (SELECT 2 FROM RDB$DATABASE) AS INDENT
        from
            {objectQualifier}FORUM a
            join {objectQualifier}FORUM b on b.FORUMID=a.PARENTID
            join {objectQualifier}FORUM c on c.FORUMID=b.PARENTID
        where
            a.FORUMID=:I_FORUMID

        union 

        select
            d.FORUMID,
            (SELECT 3 FROM RDB$DATABASE) AS INDENT
        from
            {objectQualifier}FORUM a
            join {objectQualifier}FORUM b on b.FORUMID=a.PARENTID
            join {objectQualifier}FORUM c on c.FORUMID=b.PARENTID
            join {objectQualifier}FORUM d on d.FORUMID=c.PARENTID
        where
            a.FORUMID=:I_FORUMID
        ) as x	
        join {objectQualifier}FORUM a on a.FORUMID=x.FORUMID
    order by
        x.INDENT desc
        INTO
        :"ForumID",
        :"Name"
        DO SUSPEND;
END;
--GO

create procedure {objectQualifier}FORUM_LIST(I_BOARDID INTEGER,I_FORUMID INTEGER, I_USERID INTEGER, I_ISUSERFORUM BOOL) 
RETURNS (
"ForumID" INTEGER,
"CategoryID" INTEGER,
"ParentID" INTEGER,
"Name" varchar(128),
"Description" varchar(255),
"ImageUrl" varchar(128),
"Styles" varchar(255),
"SortOrder" INTEGER,
"LastPosted" timestamp,
"LastTopicID" INTEGER,
"LastMessageID" INTEGER,
"LastUserID" INTEGER,
"LastUserName" varchar(128),
"NumTopics" INTEGER,
"NumPosts" INTEGER,
"RemoteURL" varchar(128),
"Flags" INTEGER,
"ThemeURL" varchar(128),
"PollGroupID" INTEGER,
"UserID" INTEGER,
"IsLocked" BOOL,
"IsHidden" BOOL,
"IsNoCount" BOOL,
"IsModerated" BOOL,
"IsUserForum" BOOL,
"CreatedByUserID" INTEGER,
"CanHavePersForums" BOOL
)		
as
begin
    if (I_FORUMID = 0) THEN  I_FORUMID = null;
    if (I_FORUMID is null) THEN
        for select a.FORUMID,
                   a.CATEGORYID,
                   a.PARENTID,
                   a.NAME,
                   a.DESCRIPTION,
                   a.IMAGEURL,
                   a.STYLES,
                   a.SORTORDER,
                   a.LASTPOSTED,
                   a.LASTTOPICID, 
                   a.LASTMESSAGEID,
                   a.LASTUSERID,
                   a.LASTUSERNAME,
                   a.NUMTOPICS,
                   a.NUMPOSTS,
                   a.REMOTEURL,
                   a.FLAGS,
                   a.THEMEURL,
                   a.POLLGROUPID,
                   a.USERID,
                   a.ISLOCKED,
                   a.ISHIDDEN,
                   a.ISNOCOUNT,
                   a.ISMODERATED,
                   a.ISUSERFORUM,
                   a.CREATEDBYUSERID,
                   a.CANHAVEPERSFORUMS 
        from {objectQualifier}FORUM a 
        join {objectQualifier}CATEGORY b 
        on b.CATEGORYID=a.CATEGORYID 
        where b.BOARDID=:I_BOARDID 		
        order by a.SORTORDER
        INTO
        :"ForumID",
        :"CategoryID",
        :"ParentID",
        :"Name",
        :"Description",
        :"ImageUrl",
        :"Styles",
        :"SortOrder",
        :"LastPosted",
        :"LastTopicID",
        :"LastMessageID",
        :"LastUserID",
        :"LastUserName",
        :"NumTopics",
        :"NumPosts",
        :"RemoteURL",
        :"Flags",
        :"ThemeURL",
        :"PollGroupID",
        :"UserID",		
        :"IsLocked",
        :"IsHidden",
        :"IsNoCount",
        :"IsModerated",
        :"IsUserForum",
        :"CreatedByUserID",
        :"CanHavePersForums"
DO SUSPEND;
    else
        for select first 1 a.FORUMID,
                   a.CATEGORYID,
                   a.PARENTID,
                   a.NAME,
                   a.DESCRIPTION,
                   a.IMAGEURL,
                   a.STYLES,
                   a.SORTORDER,
                   a.LASTPOSTED,
                   a.LASTTOPICID, 
                   a.LASTMESSAGEID,
                   a.LASTUSERID,
                   a.LASTUSERNAME,
                   a.NUMTOPICS,
                   a.NUMPOSTS,
                   a.REMOTEURL,
                   a.FLAGS,
                   a.THEMEURL,
                   a.POLLGROUPID,
                   a.USERID,
                   a.ISLOCKED,
                   a.ISHIDDEN,
                   a.ISNOCOUNT,
                   a.ISMODERATED,
                   a.ISUSERFORUM,
                   a.CREATEDBYUSERID,
                   a.CANHAVEPERSFORUMS
                   from {objectQualifier}FORUM a 
        join {objectQualifier}CATEGORY b 
        on b.CATEGORYID=a.CATEGORYID 
        where b.BOARDID=:I_BOARDID 
        and a.FORUMID = :I_FORUMID		
                INTO
        :"ForumID",
        :"CategoryID",
        :"ParentID",
        :"Name",
        :"Description",
        :"ImageUrl",
        :"Styles",
        :"SortOrder",
        :"LastPosted",
        :"LastTopicID",
        :"LastMessageID",
        :"LastUserID",
        :"LastUserName",
        :"NumTopics",
        :"NumPosts",
        :"RemoteURL",
        :"Flags",
        :"ThemeURL",
        :"PollGroupID",
        :"UserID",		
        :"IsLocked",
        :"IsHidden",
        :"IsNoCount",
        :"IsModerated",
        :"IsUserForum",
        :"CreatedByUserID",
        :"CanHavePersForums"
DO SUSPEND;
END;
--GO

create procedure {objectQualifier}FORUM_BYUSERLIST(I_BOARDID INTEGER,I_FORUMID INTEGER, I_USERID INTEGER, I_ISUSERFORUM BOOL) 
RETURNS (
"ForumID" INTEGER,
"CategoryID" INTEGER,
"ParentID" INTEGER,
"Name" varchar(128),
"Description" varchar(255),
"ImageUrl" varchar(128),
"Styles" varchar(255),
"SortOrder" INTEGER,
"LastPosted" timestamp,
"LastTopicID" INTEGER,
"LastMessageID" INTEGER,
"LastUserID" INTEGER,
"LastUserName" varchar(128),
"NumTopics" INTEGER,
"NumPosts" INTEGER,
"RemoteURL" varchar(128),
"Flags" INTEGER,
"ThemeURL" varchar(128),
"PollGroupID" INTEGER,
"UserID" INTEGER,
"IsLocked" BOOL,
"IsHidden" BOOL,
"IsNoCount" BOOL,
"IsModerated" BOOL,
"CreatedByUserID" INTEGER
)		
as
begin
    if (I_FORUMID = 0) THEN  I_FORUMID = null;
    if (I_FORUMID is null) THEN
        for select a.FORUMID,
                   a.CATEGORYID,
                   a.PARENTID,
                   a.NAME,
                   a.DESCRIPTION,
                   a.IMAGEURL,
                   a.STYLES,
                   a.SORTORDER,
                   a.LASTPOSTED,
                   a.LASTTOPICID, 
                   a.LASTMESSAGEID,
                   a.LASTUSERID,
                   a.LASTUSERNAME,
                   a.NUMTOPICS,
                   a.NUMPOSTS,
                   a.REMOTEURL,
                   a.FLAGS,
                   a.THEMEURL,
                   a.POLLGROUPID,
                   a.USERID,
                   a.ISLOCKED,
                   a.ISHIDDEN,
                   a.ISNOCOUNT,
                   a.ISMODERATED,
                   a.CREATEDBYUSERID 
        from {objectQualifier}FORUM a 
        join {objectQualifier}CATEGORY b 
        on b.CATEGORYID=a.CATEGORYID 
        where b.BOARDID=:I_BOARDID 
        AND a.ISUSERFORUM = :I_ISUSERFORUM
        AND (:I_USERID IS NULL OR a.CREATEDBYUSERID = :I_USERID)
        order by a.SORTORDER
        INTO
        :"ForumID",
        :"CategoryID",
        :"ParentID",
        :"Name",
        :"Description",
        :"ImageUrl",
        :"Styles",
        :"SortOrder",
        :"LastPosted",
        :"LastTopicID",
        :"LastMessageID",
        :"LastUserID",
        :"LastUserName",
        :"NumTopics",
        :"NumPosts",
        :"RemoteURL",
        :"Flags",
        :"ThemeURL",
        :"PollGroupID",
        :"UserID",		
        :"IsLocked",
        :"IsHidden",
        :"IsNoCount",
        :"IsModerated",
        :"CreatedByUserID"
DO SUSPEND;
    else
        for select first 1 a.FORUMID,
                   a.CATEGORYID,
                   a.PARENTID,
                   a.NAME,
                   a.DESCRIPTION,
                   a.IMAGEURL,
                   a.STYLES,
                   a.SORTORDER,
                   a.LASTPOSTED,
                   a.LASTTOPICID, 
                   a.LASTMESSAGEID,
                   a.LASTUSERID,
                   a.LASTUSERNAME,
                   a.NUMTOPICS,
                   a.NUMPOSTS,
                   a.REMOTEURL,
                   a.FLAGS,
                   a.THEMEURL,
                   a.POLLGROUPID,
                   a.USERID,
                   a.ISLOCKED,
                   a.ISHIDDEN,
                   a.ISNOCOUNT,
                   a.ISMODERATED,
                   a.CREATEDBYUSERID
                     from {objectQualifier}FORUM a 
        join {objectQualifier}CATEGORY b 
        on b.CATEGORYID=a.CATEGORYID 
        where b.BOARDID=:I_BOARDID 
        and a.FORUMID = :I_FORUMID
        AND a.ISUSERFORUM = :I_ISUSERFORUM
        AND (:I_USERID IS NULL OR a.CREATEDBYUSERID = :I_USERID)
                INTO
        :"ForumID",
        :"CategoryID",
        :"ParentID",
        :"Name",
        :"Description",
        :"ImageUrl",
        :"Styles",
        :"SortOrder",
        :"LastPosted",
        :"LastTopicID",
        :"LastMessageID",
        :"LastUserID",
        :"LastUserName",
        :"NumTopics",
        :"NumPosts",
        :"RemoteURL",
        :"Flags",
        :"ThemeURL",
        :"PollGroupID",
        :"UserID",		
        :"IsLocked",
        :"IsHidden",
        :"IsNoCount",
        :"IsModerated",
        :"CreatedByUserID"
DO SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}FORUM_LISTALL (I_BOARDID INTEGER,I_USERID INTEGER, I_ROOT INTEGER, I_RETURNALL SMALLINT)
RETURNS 
(
"CategoryID" integer,
"Category" varchar(128),
"ForumID" integer,
"Forum" varchar(128),
"Indent" integer,
"ParentID" integer,
"PollGroupID" integer,
"IsHidden" bool,
"CanHavePersForums" bool,
"ReadAccess" bool
)
AS
   BEGIN
            IF (I_ROOT IS NULL) THEN  I_ROOT = 0 ; 
            IF (I_ROOT = 0) THEN
              FOR   SELECT
                    b.CATEGORYID,
                    b.NAME AS "Category",
                    a.FORUMID,
                    a.NAME AS "Forum",
                    (SELECT 0 FROM RDB$DATABASE)  AS INDENT,
                    a.PARENTID,
                    a.POLLGROUPID,
                    SIGN(BIN_AND(a.FLAGS, 2)),
					a.CANHAVEPERSFORUMS,
                    c.READACCESS
              FROM
                    {objectQualifier}FORUM a
                    JOIN {objectQualifier}CATEGORY b 
                    ON b.CATEGORYID=a.CATEGORYID
                    JOIN {objectQualifier}ACTIVEACCESS c 
                    ON c.FORUMID=a.FORUMID
              WHERE
                    c.USERID=:I_USERID AND
                    b.BOARDID=:I_BOARDID AND
                    (:I_RETURNALL = 1 OR c.READACCESS>0)
              ORDER BY
                    b.SORTORDER,
                    a.SORTORDER,
                    b.CATEGORYID,
                    a.FORUMID
                    INTO
                    :"CategoryID",
                    :"Category",
                    :"ForumID",
                    :"Forum",
                    :"Indent",
                    :"ParentID",
                    :"PollGroupID",
                    :"IsHidden",
					:"CanHavePersForums",
                    :"ReadAccess"
                     DO SUSPEND;

              ELSE IF  (I_ROOT > 0)  THEN

    FOR SELECT
        b.CATEGORYID,
        b.NAME AS "Category",
        a.FORUMID,
        a.NAME AS "Forum",
        (SELECT 0 FROM RDB$DATABASE) AS INDENT,
        a.PARENTID,
        a.POLLGROUPID,
        SIGN(BIN_AND(a.FLAGS, 2)),
		a.CANHAVEPERSFORUMS,
        c.READACCESS
    FROM
        {objectQualifier}FORUM a
        JOIN {objectQualifier}CATEGORY b 
        ON b.CATEGORYID=a.CATEGORYID
        JOIN {objectQualifier}ACTIVEACCESS c
        ON c.FORUMID=a.FORUMID
    WHERE
        c.USERID=:I_USERID AND
        b.BOARDID=:I_BOARDID AND
        (:I_RETURNALL = 1 OR c.READACCESS>0) AND
        a.FORUMID = :I_ROOT
    ORDER BY
        b.SORTORDER,
        a.SORTORDER,
        b.CATEGORYID,
        a.FORUMID
         INTO
                    :"CategoryID",
                    :"Category",
                    :"ForumID",
                    :"Forum",
                    :"Indent",
                    :"ParentID",
                    :"PollGroupID",
                    :"IsHidden",
					:"CanHavePersForums",
                    :"ReadAccess"
                     DO SUSPEND;
ELSE 
   FOR SELECT
        b.CATEGORYID,
        b.NAME AS "Category",
        a.FORUMID,
        a.NAME AS "Forum",
        (SELECT 0 FROM RDB$DATABASE) AS INDENT,
        a.PARENTID,
        a.POLLGROUPID,
        SIGN(BIN_AND(a.FLAGS, 2)),
        c.READACCESS
    FROM
        {objectQualifier}FORUM a
        JOIN {objectQualifier}CATEGORY b 
        ON b.CATEGORYID=a.CATEGORYID
        JOIN {objectQualifier}ACTIVEACCESS c
        ON c.FORUMID=a.FORUMID
    WHERE
        c.USERID=:I_USERID AND
        b.BOARDID=:I_BOARDID AND
       (:I_RETURNALL = 1 OR c.READACCESS>0) AND
        b.CATEGORYID = -(:I_ROOT)
    ORDER BY
        b.SORTORDER,
        a.SORTORDER,
        b.CATEGORYID,
        a.FORUMID
         INTO
                    :"CategoryID",
                    :"Category",
                    :"ForumID",
                    :"Forum",
                    :"Indent",
                    :"ParentID",
                    :"PollGroupID",
                    :"IsHidden",
                    :"ReadAccess"
                     DO SUSPEND;

END;
--GO

CREATE PROCEDURE {objectQualifier}USER_NNTP(I_BOARDID INTEGER,I_USERNAME varchar(128),
I_EMAIL VARCHAR(128), I_TIMEZONE INTEGER, I_UTCTIMESTAMP TIMESTAMP) 
RETURNS ("UserID" INTEGER)
AS
DECLARE icic_UserID INTEGER DEFAULT 0; 
DECLARE icic_cntr INTEGER;  
BEGIN
I_USERNAME = :I_USERNAME || ' NNTP';
SELECT qqq.USERID FROM {objectQualifier}USER qqq 
WHERE qqq.BOARDID=:I_BOARDID AND qqq.NAME =:I_USERNAME
INTO  :icic_UserID;
icic_cntr = ROW_COUNT;
        
 IF (icic_cntr IS NULL OR icic_cntr < 1) THEN 		
BEGIN
        EXECUTE PROCEDURE {objectQualifier}USER_SAVE(
        null,
        :I_BOARDID,
        :I_USERNAME,
        :I_USERNAME,
        :I_EMAIL,
        :I_TIMEZONE,
        null,
        null,
        null,
        0,
        null,
        1,
        0,
        0,
        0,
        0,
        null,
        0,
        :I_UTCTIMESTAMP,
        0,
		10,
		10) RETURNING_VALUES :icic_UserID; 			
        
END		
    SELECT :icic_UserID FROM RDB$DATABASE INTO :"UserID" ;
    SUSPEND;		
END;
--GO


CREATE PROCEDURE {objectQualifier}USER_PMCOUNT
    (I_USERID INTEGER)
    RETURNS(
        "NumberIn" INTEGER,
        "NumberOut" INTEGER,
        "NumberArchived" INTEGER,
        "NumberTotal" INTEGER,
        "NumberAllowed" INTEGER)
 AS
 DECLARE ICI_COUNTIN INTEGER;
 DECLARE ICI_COUNTOUT INTEGER;
 DECLARE ICI_COUNTARCHIVEDIN INTEGER;
 DECLARE ICI_COUNTTOTAL INTEGER;
 DECLARE ICI_COUNTALLOWED INTEGER;
 DECLARE ICI_PMLIMIT1  INTEGER;
 BEGIN
        
      SELECT FIRST 1 (c.PMLIMIT) FROM {objectQualifier}USER a 
                        JOIN {objectQualifier}USERGROUP b
                          ON a.USERID = b.USERID
                            JOIN {objectQualifier}GROUP c                         
                              ON b.GROUPID = c.GROUPID 
                              WHERE a.USERID = :I_USERID 
                              ORDER BY c.PMLIMIT DESC
                              INTO :ICI_PMLIMIT1;
      SELECT FIRST 1 c.PMLIMIT FROM {objectQualifier}RANK c 
                        JOIN {objectQualifier}USER d
                           ON c.RANKID = d.RANKID 
                           WHERE d.USERID = :I_USERID 
                           ORDER BY c.PMLIMIT DESC
                           INTO :ICI_COUNTALLOWED;
      if (:ICI_PMLIMIT1 > :ICI_COUNTALLOWED) THEN
      begin
      ICI_COUNTALLOWED = :ICI_PMLIMIT1;      
      END 
      
    -- get count of pm's in user's sent items
    
    SELECT 
        COUNT(1) 
    FROM 
        {objectQualifier}USERPMESSAGE a
    INNER JOIN {objectQualifier}PMESSAGE b 
    ON a.PMESSAGEID=b.PMESSAGEID
    WHERE 
        BIN_AND(a.FLAGS, 2)<>0 AND
        b.FROMUSERID = :I_USERID
        INTO :ICI_COUNTOUT;
    -- get count of pm's in user's received items
    SELECT 
        COUNT(1)
    FROM 
        {objectQualifier}USERPMESSAGE
    WHERE 
        USERID = :I_USERID
        AND ISDELETED = 0  AND ISARCHIVED =0
        INTO :ICI_COUNTIN;
        
    SELECT 
        COUNT(1) 
    FROM 
        {objectQualifier}PMessageView a
        WHERE
        a.ISARCHIVED <>0 AND
        a.TOUSERID = :I_USERID
        INTO :ICI_COUNTARCHIVEDIN;
        
    -- return all pm data
    SELECT 
        :ICI_COUNTIN,
        :ICI_COUNTOUT,
        :ICI_COUNTARCHIVEDIN,
        :ICI_COUNTIN + :ICI_COUNTOUT + :ICI_COUNTARCHIVEDIN,
        :ICI_COUNTALLOWED
        FROM RDB$DATABASE INTO	
        :"NumberIn",
        :"NumberOut",
        :"NumberArchived",
        :"NumberTotal",
        :"NumberAllowed";	
 SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}USER_RECOVERPASSWORD(I_BOARDID INTEGER,I_USERNAME VARCHAR(128),I_EMAIL VARCHAR(128)) 
RETURNS
("UserID" INTEGER)
AS
DECLARE ICI_USERID INTEGER;
BEGIN
    
    SELECT  USERID FROM {objectQualifier}USER 
          WHERE BOARDID = :I_BOARDID 
          AND NAME = :I_USERNAME 
          and "EMAIL" = :I_EMAIL
           INTO :ICI_USERID;
    IF (ICI_USERID IS NULL) THEN
        SELECT NULL FROM RDB$DATABASE INTO :"UserID"; 		
    ELSE 	
        "UserID"=ICI_USERID;
        SUSPEND; 	
END;
--GO

CREATE PROCEDURE {objectQualifier}USER_RESETPOINTS 
AS
BEGIN
    UPDATE {objectQualifier}USER SET POINTS = NUMPOSTS * 3;
END;
--GO

 CREATE PROCEDURE {objectQualifier}USER_SAVEAVATAR
    (
    I_USERID INTEGER,
    I_AVATAR VARCHAR(255),
    I_AVATARIMAGE BLOB,
    I_AVATARIMAGETYPE VARCHAR(128)
    )
    AS
    BEGIN
    IF (I_AVATAR IS NOT NULL)  THEN

    UPDATE {objectQualifier}USER
    SET AVATAR = :I_AVATAR,
    AVATARIMAGE = null,
    AVATARIMAGETYPE = null
    WHERE USERID = :I_USERID;

    IF (I_AVATARIMAGE IS NOT NULL) THEN
    UPDATE {objectQualifier}USER
    SET AVATARIMAGE = :I_AVATARIMAGE,
    AVATARIMAGETYPE = :I_AVATARIMAGETYPE,
    AVATAR = null WHERE USERID = :I_USERID;   
    END;
--GO

 CREATE PROCEDURE {objectQualifier}USER_SAVEPASSWORD(I_USERID INTEGER,I_PASSWORD VARCHAR(32))
    AS
    BEGIN
    UPDATE {objectQualifier}USER 
    SET "PASSWORD" = :I_PASSWORD where USERID = :I_USERID;
    END;
--GO

CREATE PROCEDURE {objectQualifier}USER_SAVESIGNATURE(I_USERID INTEGER,I_SIGNATURE BLOB SUB_TYPE 1) 
AS
BEGIN
    UPDATE {objectQualifier}USER 
    SET SIGNATURE = :I_SIGNATURE 
    WHERE USERID = :I_USERID;
END;
--GO


 CREATE PROCEDURE {objectQualifier}USER_FIND(
 I_BOARDID INTEGER,
I_FILTER BOOL,
I_USERNAME VARCHAR(128),
I_DISPLAYNAME varchar(255),
I_EMAIL VARCHAR(128),
I_NOTIFICATIONTYPE BOOL,
I_DAILYDIGEST BOOL)
RETURNS
 (
"UserID" integer,
  "BoardID" integer,
  "ProviderUserKey" VARCHAR(64),
  "Name" varchar(128),
  "DisplayName" varchar(128), 
  "Password" varchar(32),
  "Email" VARCHAR(128),
  "Joined" timestamp,
  "LastVisit" timestamp,
  "IP" VARCHAR(39),
  "NumPosts" integer,
  "TimeZone" integer,
  "Avatar" varchar(255),
  "Signature" BLOB SUB_TYPE 1,
  "AvatarImage" BLOB SUB_TYPE 0,
  "AvatarImageType" VARCHAR(128),
  "RankID" integer,
  "Suspended" timestamp,
  "LanguageFile" VARCHAR(128),
  "ThemeFile" VARCHAR(128),
  "OverrideDefaultThemes" BOOL,
  "PMNotification" BOOL,
  "NotificationType" INTEGER,
  "AutoWatchTopics" BOOL,
  "UseSingleSignOn" BOOL,
  "TextEditor" VARCHAR(128),
  "Culture" VARCHAR(10),
  "DailyDigest" BOOL,
  "Flags" integer,
  "Points" integer,
  "IsApproved" BOOL,
  "IsActiveExcluded" BOOL,
  "IsGuest" BOOL,
  "IsAdmin" BOOL
)
AS
BEGIN
    IF (I_FILTER<>0) THEN 	
    BEGIN
        IF (:I_USERNAME IS NOT NULL) THEN
            I_USERNAME = '%' || :I_USERNAME || '%';  
        IF (:I_DISPLAYNAME is not null) THEN
            I_DISPLAYNAME = '%' || :I_DISPLAYNAME || '%';			

        FOR SELECT 
            a.USERID,
            a.BOARDID,
            UUID_TO_CHAR(a.PROVIDERUSERKEY),
            a.NAME,
            a.DISPLAYNAME,
            a."PASSWORD",
            a."EMAIL",
            a.JOINED,
            a.LASTVISIT,
            a.IP,
            a.NUMPOSTS,
            a.TIMEZONE,
            a.AVATAR,
            a.SIGNATURE,
            a.AVATARIMAGE,
            a.AVATARIMAGETYPE,
            a.RANKID,
            a.SUSPENDED,
            a.LANGUAGEFILE,
            a.THEMEFILE,
            a.OVERRIDEDEFAULTTHEMES,
            a.PMNOTIFICATION,
            a.NOTIFICATIONTYPE,
            a.AUTOWATCHTOPICS,
            a.USESINGLESIGNON,
            a.TEXTEDITOR,
            a.CULTURE,
            a.DAILYDIGEST,
            a.FLAGS,
            a.POINTS,
            a.ISAPPROVED,
            a.ISACTIVEEXCLUDED,           
            (SELECT COUNT(1) FROM {objectQualifier}USERGROUP x 
            join {objectQualifier}GROUP y ON x.GROUPID=y.GROUPID 
            WHERE x.USERID = a.USERID AND BIN_AND(y.FLAGS, 2)<>0) AS ISGUEST, 			
            (SELECT 0 FROM RDB$DATABASE)
         
        FROM 
            {objectQualifier}USER a
        WHERE 
            a.BOARDID=:I_BOARDID AND 
            ((:I_USERNAME IS NOT NULL and a.NAME LIKE :I_USERNAME) 
            OR (:I_EMAIL IS NOT NULL and a."EMAIL" LIKE :I_EMAIL)
            OR (:I_DISPLAYNAME IS NOT NULL and a.DISPLAYNAME LIKE :I_DISPLAYNAME) 
            OR (:I_NOTIFICATIONTYPE is not null and a.NOTIFICATIONTYPE = :I_NOTIFICATIONTYPE) or
            (:I_DAILYDIGEST is not null and a.DAILYDIGEST = :I_DAILYDIGEST))		
            INTO
            :"UserID",
            :"BoardID",
            :"ProviderUserKey",
            :"Name",
            :"DisplayName",
            :"Password",
            :"Email",
            :"Joined",
            :"LastVisit",
            :"IP",
            :"NumPosts",
            :"TimeZone",
            :"Avatar",
            :"Signature",
            :"AvatarImage",
            :"AvatarImageType",
            :"RankID",
            :"Suspended",
            :"LanguageFile",
            :"ThemeFile",
            :"OverrideDefaultThemes",
            :"PMNotification",
            :"NotificationType",
            :"AutoWatchTopics",
            :"UseSingleSignOn",
            :"TextEditor",
            :"Culture",
            :"DailyDigest",
            :"Flags",
            :"Points",
            :"IsApproved",
            :"IsActiveExcluded",
            :"IsGuest",
            :"IsAdmin"
DO SUSPEND;
END
    ELSE
    
        FOR SELECT
            a.USERID,
            a.BOARDID,
            UUID_TO_CHAR(a.PROVIDERUSERKEY),
            a.NAME,
            a.DISPLAYNAME,
            a."PASSWORD",
            a."EMAIL",
            a.JOINED,
            a.LASTVISIT,
            a.IP,
            a.NUMPOSTS,
            a.TIMEZONE,
            a.AVATAR,
            a.SIGNATURE,
            a.AVATARIMAGE,
            a.AVATARIMAGETYPE,
            a.RANKID,
            a.SUSPENDED,
            a.LANGUAGEFILE,
            a.THEMEFILE,
            a.OVERRIDEDEFAULTTHEMES,
            a.PMNOTIFICATION,
            a.NOTIFICATIONTYPE,
            a.AUTOWATCHTOPICS,
            a.USESINGLESIGNON,
            a.TEXTEDITOR,
            a.CULTURE,
            a.DAILYDIGEST,
            a.FLAGS,
            a.POINTS,
            a.ISAPPROVED,
            a.ISACTIVEEXCLUDED,
            (SELECT count(1) from {objectQualifier}USERGROUP x 
            JOIN {objectQualifier}GROUP y ON x.GROUPID=y.GROUPID 
            where x.USERID=a.USERID and BIN_AND(y.FLAGS, 2)<>0) AS ISGUEST,
            (select count(1) from {objectQualifier}USERGROUP x 
            join {objectQualifier}GROUP y 
            on y.GROUPID=x.GROUPID where x.USERID = a.USERID and BIN_AND(y.FLAGS, 1)<>0)
        FROM 
            {objectQualifier}USER a
        WHERE
            a.BOARDID = :I_BOARDID AND 
            ((:I_USERNAME IS NOT NULL AND a.NAME = :I_USERNAME) 
            OR (:I_EMAIL IS NOT NULL AND a."EMAIL"= :I_EMAIL)
            OR (:I_DISPLAYNAME IS NOT NULL and a.DISPLAYNAME = :I_DISPLAYNAME)
            OR (:I_NOTIFICATIONTYPE is not null and a.NOTIFICATIONTYPE = :I_NOTIFICATIONTYPE) or
            (:I_DAILYDIGEST is not null and a.DAILYDIGEST = :I_DAILYDIGEST))
                        INTO
            :"UserID",
            :"BoardID",
            :"ProviderUserKey",
            :"Name",
            :"DisplayName",
            :"Password",
            :"Email",
            :"Joined",
            :"LastVisit",
            :"IP",
            :"NumPosts",
            :"TimeZone",
            :"Avatar",
            :"Signature",
            :"AvatarImage",
            :"AvatarImageType",
            :"RankID",
            :"Suspended",
            :"LanguageFile",
            :"ThemeFile",
            :"OverrideDefaultThemes",
            :"PMNotification",
            :"NotificationType",
            :"AutoWatchTopics",
            :"UseSingleSignOn",
            :"TextEditor",
            :"Culture",
            :"DailyDigest",
            :"Flags",
            :"Points",
            :"IsApproved",
            :"IsActiveExcluded",
            :"IsGuest",
            :"IsAdmin"
DO SUSPEND;
    
END;
--GO   

CREATE procedure {objectQualifier}USER_ASPNET
 (I_BOARDID INTEGER,
  I_USERNAME VARCHAR(128),
  I_DISPLAYNAME VARCHAR(128),
  I_EMAIL VARCHAR(128),
  I_PROVIDERUSERKEY VARCHAR(64),
  I_ISAPPROVED BOOL,
  I_UTCTIMESTAMP TIMESTAMP) 
  RETURNS ("UserID" INTEGER)
  AS
        DECLARE ICI_USERID INTEGER;
        DECLARE ici_RankID INTEGER;
        DECLARE ici_approvedFlag INTEGER DEFAULT 0;
        DECLARE VARIABLE ICI_DISPLAYNAME VARCHAR(128) ;
BEGIN  
 
    ICI_DISPLAYNAME  = :I_DISPLAYNAME ;
    IF (:I_ISAPPROVED = 1) THEN ici_approvedFlag = 2;
    
    IF (EXISTS(SELECT FIRST 1 1 FROM {objectQualifier}USER 
                  WHERE BOARDID=:I_BOARDID 
                  AND ((PROVIDERUSERKEY=CHAR_TO_UUID(:I_PROVIDERUSERKEY)) OR (NAME = :I_USERNAME)))) THEN
     BEGIN 
            
        SELECT FIRST 1 USERID 
        from {objectQualifier}USER
        where BOARDID=:I_BOARDID 
        AND ((PROVIDERUSERKEY=CHAR_TO_UUID(:I_PROVIDERUSERKEY)) OR (NAME = :I_USERNAME))
        INTO :ICI_USERID;
         IF (:ICI_DISPLAYNAME IS NULL) THEN 		 
            SELECT FIRST 1 DISPLAYNAME 
              from {objectQualifier}USER
               where BOARDID=:I_BOARDID 
                 and USERID=:ICI_USERID
                  INTO :ICI_DISPLAYNAME;
        
        
        UPDATE {objectQualifier}USER SET 
            "NAME" = :I_USERNAME,
            DISPLAYNAME =:ICI_DISPLAYNAME,
            "EMAIL" = :I_EMAIL,
            FLAGS = BIN_OR(FLAGS,:ici_approvedFlag)
        WHERE
            USERID = :ICI_USERID 
            ORDER BY USERID;
    END
    ELSE
    BEGIN
        SELECT RANKID  
        FROM {objectQualifier}RANK 
        WHERE BIN_AND(FLAGS, 1)<>0 
        AND BOARDID=:I_BOARDID
        INTO :ici_RankID;

        IF (:ICI_DISPLAYNAME IS NULL) THEN		
            ICI_DISPLAYNAME = :I_USERNAME;
            
                  SELECT NEXT VALUE FOR SEQ_{objectQualifier}USER_USERID FROM RDB$DATABASE 
                  INTO :ICI_USERID;
                  INSERT INTO {objectQualifier}USER(USERID,BOARDID,RANKID,NAME,DISPLAYNAME, "PASSWORD","EMAIL",JOINED,LASTVISIT,NUMPOSTS,TIMEZONE,FLAGS,PROVIDERUSERKEY)
                  VALUES(
                  :ICI_USERID,
                  :I_BOARDID,
                  :ici_RankID,
                  :I_USERNAME,
                  :ICI_DISPLAYNAME,
                  '-',
                  :I_EMAIL,
                  :I_UTCTIMESTAMP,
                  :I_UTCTIMESTAMP,
                  0,				  
                  -- (SELECT CAST(CAST("VALUE" AS VARCHAR(10)) AS INTEGER) from {objectQualifier}REGISTRY where "NAME" LIKE 'timezone' and BOARDID = :I_BOARDID),
                  (SELECT CAST(CAST("VALUE" AS VARCHAR(10)) AS INTEGER) from {objectQualifier}REGISTRY where "NAME" LIKE 'timezone'),
                  :ici_approvedFlag,
                  CHAR_TO_UUID(:I_PROVIDERUSERKEY));
    END
                  
                  "UserID"= :ICI_USERID;
                  SUSPEND;
                  END;

--GO  



CREATE PROCEDURE {objectQualifier}USER_LISTMEDALS(I_USERID	INTEGER)
RETURNS
(
"MedalID" integer,
"Name" varchar(128),
"Message" BLOB SUB_TYPE 1,
"MedalURL" varchar(250),
"RibbonURL" varchar(250),
"SmallMedalURL" varchar(250),
"SmallRibbonURL" varchar(250),
"SmallMedalWidth" integer,
"SmallMedalHeight" integer,
"SmallRibbonWidth" integer,
"SmallRibbonHeight" integer,
"SortOrder" INTEGER,
"Hide" BOOL,
"OnlyRibbon" BOOL,
"Flags" integer,
"DateAwarded" timestamp
)
AS
BEGIN
 FOR SELECT 
        x.MEDALID,
        x.NAME,
        x.MESSAGE,
        x.MEDALURL,
        x.RIBBONURL,
        x.SMALLMEDALURL,
        x.SMALLRIBBONURL,
        x.SMALLMEDALWIDTH,
        x.SMALLMEDALHEIGHT,
        x.SMALLRIBBONWIDTH,
        x.SMALLRIBBONHEIGHT,
        x.SORTORDER,
        x.HIDE,
        x.ONLYRIBBON,
        x.FLAGS,
        x.DATEAWARDED 
        FROM
    (SELECT
        a.MEDALID,
        a.NAME,
        COALESCE(b.MESSAGE, a.MESSAGE) AS MESSAGE,
        a.MEDALURL,
        a.RIBBONURL,
        a.SMALLMEDALURL,
        COALESCE(a.SMALLRIBBONURL, a.SMALLMEDALURL) AS SMALLRIBBONURL,
        a.SMALLMEDALWIDTH,
        a.SMALLMEDALHEIGHT,
        COALESCE(a.SMALLRIBBONWIDTH, a.SMALLMEDALWIDTH) AS SMALLRIBBONWIDTH,
        COALESCE(a.SMALLRIBBONHEIGHT, a.SMALLMEDALHEIGHT) AS SMALLRIBBONHEIGHT,
        (SELECT 0 FROM RDB$DATABASE)  as SORTORDER,
        (SELECT 0 FROM RDB$DATABASE) AS HIDE ,
        (SELECT 1 FROM RDB$DATABASE)  AS ONLYRIBBON, 		 	
        a.FLAGS,
        b.DATEAWARDED
    FROM
        {objectQualifier}MEDAL a
        INNER JOIN {objectQualifier}USERMEDAL b ON a.MEDALID = b.MEDALID
    WHERE
        b.USERID = :I_USERID
 
    UNION
 
    SELECT	
        a.MEDALID,
        a.NAME,
        COALESCE(b.MESSAGE, a.MESSAGE) as "Message",
        a.MEDALURL,
        a.RIBBONURL,
        a.SMALLMEDALURL,
        COALESCE(a.SMALLRIBBONURL, a.SMALLMEDALURL) as SMALLRIBBONURL,
        a.SMALLMEDALWIDTH,
        a.SMALLMEDALHEIGHT,
        COALESCE(a.SMALLRIBBONWIDTH, a.SMALLMEDALWIDTH) as SMALLRIBBONWIDTH,
        COALESCE(a.SMALLRIBBONHEIGHT, a.SMALLMEDALHEIGHT) as SMALLRIBBONHEIGHT,
        (SELECT 0 FROM RDB$DATABASE)  as SORTORDER,
        (SELECT 0 FROM RDB$DATABASE) AS HIDE ,
        (SELECT 1 FROM RDB$DATABASE)  AS "OnlyRibbon", 		 		
        a.FLAGS,
        (SELECT NULL FROM RDB$DATABASE) AS "DateAwarded"
    FROM
        {objectQualifier}MEDAL a
        INNER JOIN {objectQualifier}GROUPMEDAL b ON a.MEDALID = b.MEDALID
        INNER JOIN {objectQualifier}USERGROUP c ON b.GROUPID = c.GROUPID
    WHERE
        c.USERID = :I_USERID) as x
            ORDER BY
        x.ONLYRIBBON DESC,
        x.SORTORDER ASC
        INTO
        :"MedalID",
        :"Name",
        :"Message",
        :"MedalURL",
        :"RibbonURL",
        :"SmallMedalURL",
        :"SmallRibbonURL",
        :"SmallMedalWidth",
        :"SmallMedalHeight",
        :"SmallRibbonWidth",
        :"SmallRibbonHeight",
        :"SortOrder",
        :"Hide",
        :"OnlyRibbon",
        :"Flags",
        :"DateAwarded"
        DO           
        SUSPEND;
        
 
END;
--GO


CREATE procedure {objectQualifier}USER_LAZYDATA
(
    I_USERID  INTEGER,
    I_BOARDID INTEGER,
    I_SHOWPENDINGMAILS bool,
    I_SHOWPENDINGBUDDIES bool,
    I_SHOWUNREADPMS bool,
    I_SHOWUSERULBUMS bool,
    I_SHOWUSERSTYLE bool	
)
returns
(
"ProviderUserKey" VARCHAR(64),
"UserFlags" INTEGER,
"UserName" VARCHAR(255),
"DisplayName" VARCHAR(255),
"Suspended" TIMESTAMP,
"ThemeFile" VARCHAR(128),
"LanguageFile" VARCHAR(128),
"UseSingleSignOn" BOOL,
"IsDirty" BOOL,
"TextEditor" VARCHAR(50),
"TimeZoneUser" INTEGER,
"CultureUser"	VARCHAR(10),
"IsFacebookUser" BOOL,
"IsTwitterUser" BOOL,
"IsGuest" BOOL,
"MailsPending" INTEGER,
"UnreadPrivate" INTEGER,
"LastUnreadPm" TIMESTAMP,
"PendingBuddies" INTEGER,
"LastPendingBuddies" TIMESTAMP,
"UserStyle" VARCHAR(255),
"NumAlbums" INTEGER,
"UsrAlbums" INTEGER,
"UserHasBuddies" BOOL,
"BoardVoteAccess" BOOL,
"Reputation" INTEGER,
"PersonalForumsNumber" INTEGER,
"PersonalAccessMasksNumber" INTEGER,
"PersonalGroupsNumber" INTEGER,
"UsrPersonalMasks" INTEGER,
"UsrPersonalGroups" INTEGER,
"UsrPersonalForums" INTEGER,
"CommonViewType" INTEGER,
"TopicsPerPage" INTEGER,
"PostsPerPage" INTEGER
)
 as 
    DECLARE VARIABLE G_UsrAlbums INTEGER;
    DECLARE VARIABLE R_UsrAlbums INTEGER;
    DECLARE VARIABLE ICI_UsrPersonalGroups INTEGER;
    DECLARE VARIABLE ICI_UsrPersonalMasks INTEGER;
    DECLARE VARIABLE ICI_UsrPersonalForums INTEGER;
    DECLARE VARIABLE ICI_UserID INTEGER;
    DECLARE VARIABLE ICI_UserName VARCHAR(255);	
         
begin 
        
    
SELECT COALESCE(MAX(c.USRPERSONALGROUPS),0), COALESCE(MAX(c.USRPERSONALMASKS),0),COALESCE(MAX(c.USRPERSONALFORUMS),0)
    FROM {objectQualifier}USER a 
                        JOIN {objectQualifier}USERGROUP b
                          ON a.USERID = b.USERID
                            JOIN {objectQualifier}GROUP c                         
                              ON b.GROUPID = c.GROUPID 
                              WHERE a.USERID = :I_USERID AND a.BOARDID = :I_BOARDID
                              INTO :ICI_UsrPersonalGroups,:ICI_UsrPersonalMasks,:ICI_UsrPersonalForums;
  
IF (:I_SHOWUSERULBUMS	> 0) THEN
    BEGIN
    SELECT FIRST 1 COALESCE(MAX(c.USRALBUMS),0)  
    FROM {objectQualifier}USER a 
                        JOIN {objectQualifier}USERGROUP b
                          ON a.USERID = b.USERID
                            JOIN {objectQualifier}GROUP c                         
                              ON b.GROUPID = c.GROUPID 
                              WHERE a.USERID = :I_USERID AND a.BOARDID = :I_BOARDID
                              INTO :G_UsrAlbums;
  
                              
    SELECT FIRST 1 COALESCE(c.USRALBUMS,0)   
    FROM {objectQualifier}RANK c 
                                JOIN {objectQualifier}USER d
                                  ON c.RANKID = d.RANKID WHERE d.USERID = :I_USERID  
                                  AND d.BOARDID = :I_BOARDID 
                                  INTO :R_UsrAlbums;
    END	
                               
  
    -- return information
    for select FIRST 1		
        UUID_TO_CHAR(a.PROVIDERUSERKEY) AS "ProviderUserKey",
        a.FLAGS AS "UserFlags",
        a.NAME AS "UserName",
        a.DISPLAYNAME,
        a.SUSPENDED AS "Suspended",
        a.THEMEFILE AS "ThemeFile",
        a.LANGUAGEFILE AS "LanguageFile",
        a.USESINGLESIGNON,
        a.ISDIRTY,
        A.TEXTEDITOR,
        a.TIMEZONE AS "TimeZoneUser",
        a.CULTURE AS "CultureUser",	
        a.ISFACEBOOKUSER,
        a.ISTWITTERUSER,
		(CASE WHEN BIN_AND(a.Flags,4) <> 4 THEN CAST(0 AS SMALLINT) ELSE CAST(1 AS SMALLINT)  END),
        (CASE WHEN :I_SHOWPENDINGMAILS > 0 THEN (select count(1) from {objectQualifier}MAIL WHERE TOUSERNAME = a.NAME) ELSE 0 END) AS "MailsPending",
        (CASE WHEN (:I_SHOWUNREADPMS > 0) THEN (SELECT count(1)  
        FROM {objectQualifier}USERPMESSAGE b  
        where b.USERID=:I_USERID
        and b.ISREAD = 0 AND b.ISDELETED = 0 
        AND b.ISARCHIVED = 0) ELSE 0 END) AS "Incoming",   
             
        (CASE WHEN (:I_SHOWUNREADPMS > 0) THEN (SELECT FIRST 1 CREATED FROM {objectQualifier}PMESSAGE pm INNER JOIN {objectQualifier}USERPMESSAGE upm ON pm.PMESSAGEID = upm.PMESSAGEID WHERE upm.USERID=a.USERID and upm.ISREAD=0 and upm.ISDELETED=0 and upm.ISARCHIVED=0 ORDER BY pm.CREATED DESC) ELSE NULL END) AS LastUnreadPm,	
            
        (CASE WHEN (:I_SHOWPENDINGBUDDIES > 0) THEN (SELECT COUNT(ID) FROM {objectQualifier}BUDDY WHERE TOUSERID = :I_USERID AND APPROVED = 0) ELSE 0 END) AS PendingBuddies,
        (CASE WHEN (:I_SHOWPENDINGBUDDIES > 0) THEN (SELECT FIRST 1 REQUESTED FROM {objectQualifier}BUDDY WHERE TOUSERID=a.USERID and APPROVED = 0 ORDER BY REQUESTED DESC) ELSE NULL END) AS LastPendingBuddies,
        (CASE WHEN (:I_SHOWUSERSTYLE > 0) THEN 
        (SELECT FIRST 1 COALESCE(USERSTYLE,'')  FROM {objectQualifier}USER WHERE USERID = :I_USERID) 
        ELSE '' END), 
        (SELECT COUNT(ua.ALBUMID) FROM {objectQualifier}USERALBUM ua
        WHERE ua.UserID = :I_USERID),
        (CASE WHEN (:G_UsrAlbums > :R_UsrAlbums) THEN :G_UsrAlbums ELSE :R_UsrAlbums END),
        (CASE WHEN (:I_SHOWPENDINGBUDDIES > 0) THEN (SELECT COALESCE((SELECT SIGN(COUNT(1)) FROM {objectQualifier}BUDDY WHERE FROMUSERID = :I_USERID OR TOUSERID = :I_USERID),0) FROM RDB$DATABASE) ELSE 0 END) as UserHasBuddies,
          -- Guest can't vote in polls attached to boards, we need some temporary access check by a criteria 
        (CASE WHEN BIN_AND(a.Flags,4) <> 4 THEN CAST(0 AS SMALLINT)  ELSE CAST(1 AS SMALLINT)  END),	
        a.Points,
        (SELECT COUNT(1) FROM {objectQualifier}FORUM WHERE CREATEDBYUSERID = :I_USERID AND ISUSERFORUM = 1),
        (SELECT COUNT(1) FROM {objectQualifier}ACCESSMASK WHERE CREATEDBYUSERID = :I_USERID AND ISUSERMASK = 1),
        (SELECT COUNT(1) FROM {objectQualifier}GROUP WHERE CREATEDBYUSERID = :I_USERID AND ISUSERGROUP = 1),
        (SELECT :ICI_UsrPersonalMasks FROM RDB$DATABASE),
        (SELECT :ICI_UsrPersonalGroups FROM RDB$DATABASE),		
        (SELECT :ICI_UsrPersonalForums FROM RDB$DATABASE),
        a.COMMONVIEWTYPE,
        a.TOPICSPERPAGE,
        a.POSTSPERPAGE 
        from
           {objectQualifier}USER a		
        where
        a.USERID = :I_USERID
        into		
        :"ProviderUserKey",
        :"UserFlags",
        :"UserName",
        :"DisplayName",
        :"Suspended",
        :"ThemeFile",
        :"LanguageFile",
        :"UseSingleSignOn",
        :"IsDirty",
        :"TextEditor",
        :"TimeZoneUser",
        :"CultureUser",
        :"IsFacebookUser",
        :"IsTwitterUser",
		:"IsGuest",
        :"MailsPending",
        :"UnreadPrivate" ,
        :"LastUnreadPm" ,
        :"PendingBuddies",
        :"LastPendingBuddies",
        :"UserStyle",
        :"NumAlbums",
        :"UsrAlbums",
        :"UserHasBuddies",
        :"BoardVoteAccess",
        :"Reputation",
        :"PersonalForumsNumber",
        :"PersonalAccessMasksNumber",
        :"PersonalGroupsNumber",
        :"UsrPersonalMasks",
        :"UsrPersonalGroups",
        :"UsrPersonalForums",
        :"CommonViewType",
        :"TopicsPerPage",
        :"PostsPerPage"
do suspend;
     END;
--GO

CREATE PROCEDURE {objectQualifier}MESSAGE_GETTEXTBYIDS(I_MESSAGEIDS BLOB SUB_TYPE 1)
RETURNS 
("MessageID" integer, 
 "Message" BLOB SUB_TYPE 1 )
AS
DECLARE VARIABLE ICI_MESSAGEID varchar(11);
DECLARE VARIABLE ICI_MESSAGEIDSCHUNK varchar(4000);
DECLARE VARIABLE ICI_POS integer;
DECLARE VARIABLE ICI_ITR integer;
DECLARE VARIABLE ICI_TRIMINDEX integer;
DECLARE VARIABLE ICI_LONG INTEGER DEFAULT 0;
DECLARE VARIABLE ICI_MESSAGEIDSCHUNKCURRENT integer DEFAULT 0;
BEGIN
    ICI_ITR = 0; 
    ICI_POS = 0;
    ICI_TRIMINDEX = 0;
    ICI_MESSAGEIDSCHUNK  = SUBSTRING( :I_MESSAGEIDS FROM 1 FOR 4000 );
    
    
    WHILE (CHAR_LENGTH(:ICI_MESSAGEIDSCHUNK) > 0) DO
    BEGIN 
    IF (CHAR_LENGTH(:ICI_MESSAGEIDSCHUNK) > 3989) THEN
    BEGIN    
    -- todo: Find commas in last positions
    -- ICI_Last = POSITION(',',REVERSE( :ICI_MESSAGEIDSCHUNK ), CHAR_LENGTH(:ICI_MESSAGEIDSCHUNK));
    ICI_LONG = 1;
    END
             -- find first comma position if we have more then 4000 length  
             IF ( :ICI_LONG > 0 )  THEN       
             ICI_TRIMINDEX = POSITION(',',REVERSE( :ICI_MESSAGEIDSCHUNK ), 1 ); 
             ELSE
             ICI_TRIMINDEX = 0;   
             ICI_MESSAGEIDSCHUNKCURRENT = CHAR_LENGTH(:ICI_MESSAGEIDSCHUNK); 
             ICI_MESSAGEIDSCHUNK = SUBSTRING(:ICI_MESSAGEIDSCHUNK FROM 1 FOR :ICI_MESSAGEIDSCHUNKCURRENT - COALESCE(:ICI_TRIMINDEX,0));
             ICI_ITR = :ICI_ITR - :ICI_TRIMINDEX;
             ICI_MESSAGEIDSCHUNK = TRIM(BOTH FROM :ICI_MESSAGEIDSCHUNK) || ',';
             ICI_POS = POSITION(',', :ICI_MESSAGEIDSCHUNK, 1);
          
 IF ( REPLACE(:ICI_MESSAGEIDSCHUNK, ',', '') <> '') THEN
    BEGIN
      WHILE (:ICI_POS > 0) DO
       BEGIN	
       ICI_MESSAGEID = LEFT(:ICI_MESSAGEIDSCHUNK, :ICI_POS  - 1);
       ICI_MESSAGEID = TRIM( BOTH FROM :ICI_MESSAGEID  );					
            
    IF (:ICI_MESSAGEID <> '') THEN
    BEGIN
        SELECT (SELECT CAST(:ICI_MESSAGEID AS integer) FROM RDB$DATABASE), d.MESSAGE
            FROM  {objectQualifier}MESSAGE d WHERE d.MESSAGEID = (CAST(:ICI_MESSAGEID AS integer))	 
            INTO	 
    :"MessageID", 
    :"Message";
    "MessageID" = CAST(:ICI_MESSAGEID AS integer);
     SUSPEND;			
    END
            
            ICI_MESSAGEIDSCHUNK = RIGHT(:ICI_MESSAGEIDSCHUNK , (CHAR_LENGTH(:ICI_MESSAGEIDSCHUNK ) - :ICI_POS));
            ICI_POS = POSITION(',', :ICI_MESSAGEIDSCHUNK , 1);	
            -- to be sure that last value is inserted			
        
    END	
    END    
          ICI_ITR = :ICI_ITR + :ICI_MESSAGEIDSCHUNKCURRENT;            
          ICI_MESSAGEIDSCHUNK  = SUBSTRING( :I_MESSAGEIDS FROM :ICI_ITR FOR ( :ICI_ITR + 4000 ) );
     
            

END 	
    
    
END;
--GO
 
CREATE PROCEDURE {objectQualifier}USER_THANKFROMCOUNT
(I_USERID INTEGER)
 RETURNS
("ThanksCount" integer)
AS
BEGIN
        SELECT COUNT(TH.THANKSID) 
        FROM {objectQualifier}THANKS TH WHERE (TH.THANKSTOUSERID=:I_USERID) into :"ThanksCount";
        SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}USERREPLIEDTOPIC
(I_MESSAGEID INTEGER, I_USERID INTEGER) 
RETURNS
("MessageID" integer)
AS
DECLARE ICI_TOPICID INTEGER;
begin
       
        SELECT TopicID FROM {objectQualifier}MESSAGE WHERE (MESSAGEID = :I_MESSAGEID)
        INTO :ICI_TOPICID;

        SELECT COUNT(t.MESSAGEID)
        FROM {objectQualifier}MESSAGE AS t WHERE (t.TOPICID= :ICI_TOPICID) AND (t.USERID = :I_USERID)
        INTO :"MessageID";
        SUSPEND;
        
END;
--GO

/* CREATE PROCEDURE {objectQualifier}TOPICS_BYUSER(
                 I_BOARDID INTEGER, 
                 I_PAGEUSERID INTEGER, 
                 I_SINCE TIMESTAMP, 
                 I_CATEGORYID INTEGER, 
                 I_STYLEDNICKS BOOL, 
                 I_FINDLASTREAD BOOL) 
       RETURNS
       ("ForumID" INTEGER,
        "TopicID" INTEGER,
        "TopicMovedID" INTEGER,
        "Posted" TIMESTAMP,
        "LinkTopicID" INTEGER,
        "Subject" VARCHAR(255),
        "Description" VARCHAR(255),
        "Status" VARCHAR(255),
        "Styles" VARCHAR(255),
        "UserID" INTEGER,
        "Starter" VARCHAR(255),
        "NumPostsDeleted" INTEGER,
        "Replies" INTEGER,
        "Views" INTEGER,
        "LastPosted" TIMESTAMP, 
        "LastUserID" INTEGER,
        "LastUserName" VARCHAR(255),
        "LastMessageID" INTEGER,
        "LastMessageFlags" INTEGER,
        "LastTopicID" INTEGER,
        "TopicFlags" INTEGER,
        "FavoriteCount" INTEGER,
        "Priority" INTEGER,
        "PollID" INTEGER,
        "ForumName" VARCHAR(255),	
        "ForumFlags" INTEGER,
        "FirstMessage" VARCHAR(1000) ,
        "StarterStyle" VARCHAR(255),
        "LastUserStyle" VARCHAR(255),
        "LastForumAccess" TIMESTAMP,
        "LastTopicAccess" TIMESTAMP	   
      )
AS
BEGIN
        FOR SELECT
        c.FORUMID,
        c.TOPICID,
        c.TOPICMOVEDID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID),
        c.TOPIC,
        c.DESCRIPTION,
        c.STATUS,
        c.STYLES,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME),
        (SELECT COUNT(1) FROM {objectQualifier}MESSAGE mes WHERE mes.TOPICID = c.TOPICID AND mes.ISDELETED = 1 AND mes.ISAPPROVED = 1 AND ((:I_PAGEUSERID IS NOT NULL AND mes.USERID = :I_PAGEUSERID) OR (:I_PAGEUSERID IS NULL)) ),
        (select count(1)-1 from {objectQualifier}MESSAGE x where x.TOPICID=c.TOPICID and BIN_AND(x.Flags,8)<> 8),
        c.VIEWS,
        c.LASTPOSTED,
        c.LASTUSERID,
        COALESCE(c.LASTUSERNAME,(select NAME from {objectQualifier}USER x where x.USERID=c.LASTUSERID)),
        c.LASTMESSAGEID,
        c.LASTMESSAGEFLAGS,
        c.TOPICID,
        c.FLAGS,
        (SELECT COUNT(ID) FROM {objectQualifier}FAVORITETOPIC WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)),
        c.PRIORITY,
        c.POLLID,
        d.NAME,	
        d.FLAGS,
        (SELECT FIRST 1 CAST(MESSAGE as varchar(1000)) FROM {objectQualifier}MESSAGE mes2 where mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) AND mes2."POSITION" = 0),
        (case(:I_STYLEDNICKS)
            when 1 then  COALESCE((SELECT FIRST 1 f.STYLE FROM {objectQualifier}USERGROUP e 
            join {objectQualifier}GROUP f on f.GROUPID=e.GROUPID WHERE e.USERID=c.USERID AND CHAR_LENGTH(f.STYLE) > 2 ORDER BY f.SortOrder), 
            (select r.STYLE from {objectQualifier}USER usr 
            join {objectQualifier}RANK r ON r.RankID = usr.RankID  where usr.USERID=c.USERID))  
            else ''	 END),
        (case(:I_STYLEDNICKS)
            when 1 then  COALESCE((SELECT FIRST 1 f.STYLE FROM {objectQualifier}USERGROUP e 
            join {objectQualifier}GROUP f on f.GROUPID=e.GROUPID WHERE e.USERID=c.LASTUSERID AND CHAR_LENGTH(f.STYLE) > 2 ORDER BY f.SortOrder), 
            (select r.STYLE from {objectQualifier}USER usr 
            join {objectQualifier}RANK r ON r.RankID = usr.RankID  where usr.USERID=c.LASTUSERID))  
            else ''	 END),
        (case(:I_FINDLASTREAD)
             when 1 then
               (SELECT FIRST 1  LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=d.FORUMID AND x.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp) FROM RDB$DATABASE) 	 END),
        (case(:I_FINDLASTREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=c.TOPICID AND y.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp) FROM RDB$DATABASE)	 END)
    from
        {objectQualifier}TOPIC c
        join {objectQualifier}USER b on b.USERID=c.USERID
        join {objectQualifier}FORUM d on d.FORUMID=c.FORUMID
        join {objectQualifier}ACTIVEACCESS x on x.FORUMID=d.FORUMID
        join {objectQualifier}CATEGORY cat on cat.CATEGORYID=d.CATEGORYID
    where
        c.LASTPOSTED > :I_SINCE  and
        x.UserID =: I_PAGEUSERID and
        x.ReadAccess <> 0 and
        cat.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or cat.CATEGORYID=:I_CATEGORYID) and
        c.ISDELETED = 0
        and	c.TOPICMOVEDID is null
        and c.TOPICID = (SELECT FIRST 1 mess.TOPICID FROM {objectQualifier}MESSAGE mess WHERE mess.USERID=:I_PAGEUSERID AND mess.TOPICID=c.TOPICID)
    order by
        cat.SORTORDER ASC,
        d.SORTORDER ASC,
        d.NAME ASC,
        PRIORITY DESC,
        LASTPOSTED DESC
        INTO
        :"ForumID",
        :"TopicID",
        :"TopicMovedID",
        :"Posted",
        :"LinkTopicID",
        :"Subject",
        :"Description",
        :"Status",
        :"Styles",
        :"UserID",
        :"Starter",
        :"NumPostsDeleted",
        :"Replies",
        :"Views",
        :"LastPosted",
        :"LastUserID",
        :"LastUserName",
        :"LastMessageID",
        :"LastMessageFlags",
        :"LastTopicID",
        :"TopicFlags",
        :"FavoriteCount",
        :"Priority",
        :"PollID",
        :"ForumName",	
        :"ForumFlags",
        :"FirstMessage",
        :"StarterStyle" ,
        :"LastUserStyle",
        :"LastForumAccess",
        :"LastTopicAccess"
        DO SUSPEND;
END;


 create procedure {objectQualifier}TOPIC_UNANSWERED(I_BOARDID INTEGER,I_PAGEUSERID INTEGER,I_SINCE TIMESTAMP,I_CATEGORYID INTEGER, I_STYLEDNICKS BOOL, I_FINDLASTREAD BOOL) 
RETURNS
(
        "ForumID" INTEGER,
        "TopicID" INTEGER,
        "TopicMovedID" INTEGER,
        "Posted" TIMESTAMP,
        "LinkTopicID" INTEGER,
        "Subject"  VARCHAR(255),
        "Description"  VARCHAR(255),
        "Status"  VARCHAR(255),
        "Styles" VARCHAR(255),
        "UserID" INTEGER,
        "Starter" VARCHAR(255),
        "NumPostsDeleted" INTEGER,
        "Replies" INTEGER,
        "Views" INTEGER,
        "LastPosted" TIMESTAMP,
        "LastUserID" INTEGER,
        "LastUserName"   VARCHAR(255),
        "LastMessageID" INTEGER,
        "LastMessageFlags" INTEGER,
        "LastTopicID" INTEGER,
        "TopicFlags" INTEGER,
        "FavoriteCount" INTEGER,
        "Priority" INTEGER,
        "PollID" INTEGER,
        "ForumName"  VARCHAR(255),			
        "ForumFlags" INTEGER,
        "FirstMessage" VARCHAR(1000),
        "StarterStyle" VARCHAR(255),
        "LastUserStyle" VARCHAR(255),
        "LastForumAccess" TIMESTAMP,
        "LastTopicAccess" TIMESTAMP
)
AS
begin
        FOR SELECT
        c.FORUMID,
        c.TOPICID,
        c.TOPICMOVEDID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID),
        c.TOPIC,
        c.DESCRIPTION,
        c.STATUS,
        c.STYLES,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME),
        (SELECT COUNT(1) FROM  {objectQualifier}MESSAGE mes WHERE mes.TOPICID = c.TOPICID AND mes.ISDELETED = 1 AND mes.ISAPPROVED = 1 AND ((:I_PAGEUSERID IS NOT NULL AND mes.USERID = :I_PAGEUSERID) OR (:I_PAGEUSERID IS NULL)) ),
        (SELECT COUNT(1) FROM {objectQualifier}MESSAGE x WHERE x.TOPICID=c.TOPICID and x.ISDELETED = 0) - 1,
        c.VIEWS,
        c.LASTPOSTED,
        c.LASTUSERID,
        COALESCE(c.LASTUSERNAME,(SELECT X.NAME FROM {objectQualifier}USER x WHERE x.USERID=c.LASTUSERID)),
        c.LASTMESSAGEID,
        c.LASTMESSAGEFLAGS,	
        c.TOPICID AS LastTopicID,
        c.FLAGS,
        (SELECT COUNT(ID) as FAVORITECOUNT FROM {objectQualifier}FAVORITETOPIC WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)),
        c.PRIORITY,
        c.POLLID,
        d.NAME,			
        d.FLAGS,
        (SELECT FIRST 1 CAST(mes2.MESSAGE as varchar(1000)) FROM {objectQualifier}MESSAGE mes2 where mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) AND mes2."POSITION" = 0),
        (case(:I_STYLEDNICKS)
            when 1 then  COALESCE((SELECT FIRST 1 f.STYLE FROM {objectQualifier}USERGROUP e 
            join {objectQualifier}GROUP f on f.GroupID=e.GroupID WHERE e.UserID=c.UserID AND CHAR_LENGTH(f.STYLE) > 2 ORDER BY f.SortOrder), 
            (select r.STYLE from {objectQualifier}USER usr 
            join {objectQualifier}RANK r ON r.RankID = usr.RankID  where usr.UserID=c.UserID))  
            else ''	 END) as StarterStyle ,
        (case(:I_STYLEDNICKS)
            when 1 then  COALESCE((SELECT FIRST 1 f.STYLE FROM {objectQualifier}USERGROUP e 
            join {objectQualifier}GROUP f on f.GroupID=e.GroupID WHERE e.UserID=c.LastUserID AND CHAR_LENGTH(f.STYLE) > 2 ORDER BY f.SortOrder), 
            (select r.STYLE from {objectQualifier}USER usr 
            join {objectQualifier}RANK r ON r.RankID = usr.RankID  where usr.UserID=c.LastUserID))  
            else ''	 END) as LastUserStyle,
       (case(:I_FINDLASTREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=d.FORUMID AND x.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE) END) AS "LastForumAccess",
        (case(:I_FINDLASTREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID = c.TOPICID AND y.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE)  END) AS  "LastTopicAccess"  
    from
        {objectQualifier}TOPIC c
        join {objectQualifier}USER b on b.USERID=c.USERID
        join {objectQualifier}FORUM d on d.FORUMID=c.FORUMID
        join {objectQualifier}ACTIVEACCESS x on x.FORUMID=d.FORUMID
        join {objectQualifier}CATEGORY cat on cat.CATEGORYID=d.CATEGORYID
    where
        :I_SINCE < c.LASTPOSTED and
        x.USERID = :I_PAGEUSERID and
        x.READACCESS <> 0 and
        cat.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or cat.CATEGORYID = :I_CATEGORYID) and
        c.ISDELETED = 0 and	
        c.TOPICMOVEDID is null and
        c.NUMPOSTS = 1
    order by
        cat.SORTORDER asc,
        d.SORTORDER asc,
        d.NAME asc,
        c.PRIORITY desc,
        c.LASTPOSTED desc
        INTO
        :"ForumID",
        :"TopicID",
        :"TopicMovedID",
        :"Posted",
        :"LinkTopicID",
        :"Subject",
        :"Description",
        :"Status",
        :"Styles",
        :"UserID",
        :"Starter",
        :"NumPostsDeleted",
        :"Replies",
        :"Views",
        :"LastPosted",
        :"LastUserID",
        :"LastUserName",
        :"LastMessageID",
        :"LastMessageFlags",
        :"LastTopicID",
        :"TopicFlags",
        :"FavoriteCount",
        :"Priority",
        :"PollID",
        :"ForumName",			
        :"ForumFlags",
        :"FirstMessage",
        :"StarterStyle",
        :"LastUserStyle",
        :"LastForumAccess",
        :"LastTopicAccess"
        DO SUSPEND;
END;
*/


CREATE PROCEDURE {objectQualifier}USER_SAVENOTIFICATION
(
I_USERID	            integer,
i_pmnotification		bool,
i_autowatchtopics       bool,
I_NOTIFICATIONTYPE	    integer,
I_DAILYDIGEST		    bool
)
AS
BEGIN
        UPDATE
            {objectQualifier}USER
        SET
            PMNOTIFICATION = (CASE WHEN (:i_pmnotification is not null) THEN  :i_pmnotification ELSE pmnotification END),
            AUTOWATCHTOPICS = (CASE WHEN (:i_autowatchtopics is not null) THEN  :i_autowatchtopics ELSE autowatchtopics END),
            NOTIFICATIONTYPE =  (CASE WHEN (:I_NOTIFICATIONTYPE is not null) THEN  :I_NOTIFICATIONTYPE ELSE NOTIFICATIONTYPE END),
            DAILYDIGEST = (CASE WHEN (:I_DAILYDIGEST is not null) THEN  :I_DAILYDIGEST ELSE DAILYDIGEST END)
        WHERE
            USERID = :I_USERID;
END;
--GO

CREATE PROCEDURE {objectQualifier}ALBUM_IMAGES_BY_USER(I_USERID integer)
 
    RETURNS (
"ImageID" integer,
"AlbumID" integer,
"Caption"  VARCHAR(255),	
"FileName"  VARCHAR(255),
"Bytes" integer,
"ContentType"  VARCHAR(255),
"Uploaded" timestamp,
"Downloads" integer
) AS
BEGIN
        for  select
                a.IMAGEID,
                a.ALBUMID,
                a.CAPTION,	
                a.FILENAME,
                a.BYTES,
                a.CONTENTTYPE,
                a.UPLOADED,
                a.DOWNLOADS
        FROM    {objectQualifier}USERALBUMIMAGE a
                    INNER JOIN {objectQualifier}USERALBUM b ON b.ALBUMID = a.ALBUMID
        WHERE  b.USERID = :I_USERID ORDER BY a.ALBUMID, a.IMAGEID DESC
        INTO
        :"ImageID",
        :"AlbumID",
        :"Caption",	
        :"FileName",
        :"Bytes",
        :"ContentType",
        :"Uploaded",
        :"Downloads"
        DO SUSPEND;
END;
--GO


CREATE PROCEDURE {objectQualifier}ADMINPAGEACCESS_SAVE 
(
I_USERID	    integer,
I_PAGENAME	    VARCHAR(255)
)
AS
BEGIN
     If (not exists (select 1 from  {objectQualifier}ADMINPAGEUSERACCESS where USERID = :I_USERID and PAGENAME = :I_PAGENAME ROWS 1)) THEN		
        insert into {objectQualifier}ADMINPAGEUSERACCESS  (USERID,PAGENAME) 
        values(:I_USERID,:I_PAGENAME);			
END;
--GO

CREATE PROCEDURE {objectQualifier}ADMINPAGEACCESS_DELETE
(
I_USERID	    integer,
I_PAGENAME	    VARCHAR(255)
)
AS
BEGIN
        DELETE FROM {objectQualifier}ADMINPAGEUSERACCESS	
        WHERE USERID = :I_USERID and (PAGENAME IS NULL OR PAGENAME = :I_PAGENAME);	
END;
--GO

CREATE PROCEDURE {objectQualifier}ADMINPAGEACCESS_LIST(I_USERID integer, I_PAGENAME VARCHAR(255))
 
    RETURNS (
"UserID" integer,
"PageName"  VARCHAR(255),	
"UserName"  VARCHAR(255),
"UserDisplayName"  VARCHAR(255),
"BoardName"  VARCHAR(255)
) AS
BEGIN
if (:I_USERID > 0  and :I_PAGENAME IS NOT NULL) then
        FOR select 
        ap.USERID,
        ap.PAGENAME,					  
        u.NAME as UserName, 
        u.DISPLAYNAME as UserDisplayName, 
        b.NAME as BoardName 
        from {objectQualifier}ADMINPAGEUSERACCESS ap 
        JOIN  {objectQualifier}USER u on ap.USERID = u.USERID 
        JOIN {objectQualifier}BOARD b ON b.BOARDID = u.BOARDID 
        where u.UserID = :I_USERID and PAGENAME = :I_PAGENAME
         and (BIN_AND(u.FLAGS,1) <> 1) 
         order by  b.BOARDID,u.NAME,ap.PAGENAME
        INTO "UserID",
             "PageName",
             "UserName",
             "UserDisplayName",
             "BoardName" DO SUSPEND;
        else if (:I_USERID > 0 and :I_PAGENAME IS  NULL) then
        FOR select
        ap.USERID,
        ap.PAGENAME,					  
        u.NAME as UserName, 
        u.DISPLAYNAME as UserDisplayName, 
        b.NAME as BoardName 
         from {objectQualifier}ADMINPAGEUSERACCESS ap 
        JOIN  {objectQualifier}USER u on ap.USERID = u.USERID 
        JOIN {objectQualifier}BOARD b ON b.BOARDID = u.BOARDID 
        where u.UserID = :I_USERID 
        and (BIN_AND(u.FLAGS,1) <> 1) order by  b.BOARDID,u.NAME,ap.PAGENAME
        INTO "UserID",
             "PageName",
             "UserName",
             "UserDisplayName",
             "BoardName" DO SUSPEND;
        else
        FOR select
        ap.USERID,
        ap.PAGENAME,					  
        u.NAME as UserName, 
        u.DISPLAYNAME as UserDisplayName, 
        b.NAME as BoardName 
        from {objectQualifier}ADMINPAGEUSERACCESS ap 
        JOIN  {objectQualifier}USER u on ap.USERID = u.USERID 
        JOIN {objectQualifier}BOARD b ON b.BOARDID = u.BOARDID 
        where (BIN_AND(u.FLAGS,1) <> 1)
        order by  b.BOARDID,u.NAME,ap.PAGENAME 
        INTO "UserID",
             "PageName",
             "UserName",
             "UserDisplayName",
             "BoardName" DO SUSPEND;

END;
--GO

CREATE PROCEDURE {objectQualifier}FORUM_MAXID(I_BOARDID INTEGER)
RETURNS ("ForumID" integer)
    AS
begin	
    select first 1 a.FORUMID from {objectQualifier}FORUM a join {objectQualifier}CATEGORY b 
    on b.CATEGORYID=a.CATEGORYID where b.BOARDID=:I_BOARDID order by a.FORUMID desc
    INTO :"ForumID";
    SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}EVENTLOG_DELETEBYUSER
(
I_BOARDID	    integer,
I_PAGEUSERID	integer
)
AS
BEGIN
        if (exists (select 1  from {objectQualifier}USER where (BIN_AND(FLAGS,1) = 1 and USERID = :I_PAGEUSERID) ROWS 1)) THEN
begin
delete from {objectQualifier}EVENTLOG where
            (USERID is null or
            USERID in (select USERID from {objectQualifier}USER where BOARDID=:I_BOARDID));
END
else
begin
        -- either EventLogID or BoardID must be null, not both at the same time
    
        delete from {objectQualifier}EVENTLOG
        where EVENTLOGID in (select a.EVENTLOGID from {objectQualifier}EVENTLOG a
        left join {objectQualifier}EVENTLOGGROUPACCESS e on e.EVENTTYPEID = a.TYPE 
        join {objectQualifier}USERGROUP ug on (ug.USERID =  :I_PAGEUSERID and ug.GROUPID = e.GROUPID)
        left join {objectQualifier}USER b on b.USERID=a.USERID
        where e.DELETEACCESS = 1);
END		
END;
--GO

CREATE PROCEDURE {objectQualifier}GROUP_EVENTLOGACCESSLIST(I_BOARDID integer)
 
RETURNS (
"GroupID" integer,
"BoardID" integer,
"Name"  VARCHAR(255),
"Flags" integer,
"PMLimit" integer,	
"Style"  VARCHAR(255),
"SortOrder" integer,
"Description" VARCHAR(128),
"BoardName"  VARCHAR(255)
) AS
BEGIN
        if (:I_BOARDID is null) then
        for select g.GROUPID,
               g.BOARDID,
               g.NAME,
               g.FLAGS,
               g.PMLIMIT,
               g.STYLE,
               g.SORTORDER,
               g.DESCRIPTION,
               b.Name as BoardName 
               from {objectQualifier}GROUP g
               join {objectQualifier}BOARD b 
               on b.BoardID = g.BoardID order by g.SortOrder 
               into
               :"GroupID",
               :"BoardID",
               :"Name",
               :"Flags",
               :"PMLimit",
               :"Style",
               :"SortOrder",
               :"Description",
               :"BoardName"
                DO SUSPEND;

    else
        for select g.GROUPID,
               g.BOARDID,
               g.NAME,
               g.FLAGS,
               g.PMLIMIT,
               g.STYLE,
               g.SORTORDER,
               g.DESCRIPTION,
               b.NAME as BoardName 
               from {objectQualifier}GROUP g
               join {objectQualifier}BOARD b 
               on b.BOARDID = g.BOARDID where g.BOARDID= :I_BOARDID  order by g.SortOrder 
               into
               :"GroupID",
               :"BoardID",
               :"Name",
               :"Flags",
               :"PMLimit",
               :"Style",
               :"SortOrder",
               :"Description",
               :"BoardName" 
               DO SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}EVENTLOGGROUPACCESS_SAVE
(
I_GROUPID	    integer,
I_EVENTTYPEID	integer,
I_EVENTTYPENAME VARCHAR(128),
I_DELETEACCESS  BOOL
)
AS
BEGIN
    if (not exists (select 1 from {objectQualifier}EVENTLOGGROUPACCESS where GROUPID = :I_GROUPID and EVENTTYPENAME = :I_EVENTTYPENAME ROWS 1)) THEN
        insert into {objectQualifier}EVENTLOGGROUPACCESS  (GROUPID,EVENTTYPEID,EVENTTYPENAME,DELETEACCESS) 
        values(:I_GROUPID,:I_EVENTTYPEID,:I_EVENTTYPENAME,:I_DELETEACCESS);	
    else	
        update {objectQualifier}EVENTLOGGROUPACCESS  set DELETEACCESS = :I_DELETEACCESS
        where GROUPID = :I_GROUPID and EVENTTYPEID = :I_EVENTTYPEID;	
END;
--GO

CREATE PROCEDURE {objectQualifier}EVENTLOGGROUPACCESS_DELETE
(
I_GROUPID	    integer,
I_EVENTTYPEID	integer,
I_EVENTTYPENAME VARCHAR(128)
)
AS
BEGIN
if (:I_EVENTTYPENAME is not null) THEN
    begin
        delete from {objectQualifier}EVENTLOGGROUPACCESS  where GROUPID = :I_GROUPID and EVENTTYPEID = :I_EVENTTYPEID;
    END	
    else
    begin
    -- delete all access rights
        delete from {objectQualifier}EVENTLOGGROUPACCESS  where GROUPID = :I_GROUPID; 
    END
END;
--GO


CREATE PROCEDURE {objectQualifier}EVENTLOGGROUPACCESS_LIST(
I_GROUPID	    integer,
I_EVENTTYPEID	integer
) 
RETURNS (
"GroupID" integer,
"EventTypeID" integer,
"EventTypeName"  VARCHAR(255),
"DeleteAccess" bool,
"GroupName"  VARCHAR(255)
) AS
BEGIN
-- TODO - exclude host admins from list   
if (:I_EVENTTYPEID is null)  THEN  
        FOR select  e.GROUPID,
                e.EVENTTYPEID,
                e.EVENTTYPENAME,
                e.DELETEACCESS, 
                g.NAME as GroupName 
                from {objectQualifier}EVENTLOGGROUPACCESS e 
        join {objectQualifier}GROUP g on g.GROUPID = e.GROUPID 
        where  e.GROUPID = :I_GROUPID
        into
        :"GroupID",
        :"EventTypeID",
        :"EventTypeName",
        :"DeleteAccess",
        :"GroupName"
        DO SUSPEND;
        else
        FOR select  e.GROUPID,
                e.EVENTTYPEID,
                e.EVENTTYPENAME,
                e.DELETEACCESS, 
                g.NAME as GroupName 
                from {objectQualifier}EVENTLOGGROUPACCESS e 
        join {objectQualifier}GROUP g on g.GROUPID = e.GROUPID 
        where e.GROUPID = :I_GROUPID and e.EVENTTYPEID = :I_EVENTTYPEID
        into
        :"GroupID",
        :"EventTypeID",
        :"EventTypeName",
        :"DeleteAccess",
        :"GroupName"
        DO SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}FORUM_CATACCESS_ACTIVEUSER(I_BOARDID integer, I_USERID integer)
 
RETURNS (
"CategoryID" integer,
"CategoryName"  VARCHAR(255),
"SortOrder" integer
) AS
BEGIN		
        for select
               DISTINCT(c.CATEGORYID),
               c.NAME, c.SORTORDER
               from {objectQualifier}CATEGORY c			  
               join {objectQualifier}FORUM f
               ON f.CATEGORYID = c.CATEGORYID
               JOIN {objectQualifier}ACTIVEACCESS access ON (f.FORUMID = access.FORUMID and access.USERID = :I_USERID)  
               WHERE c.BOARDID = :I_BOARDID and f.PARENTID IS NULL and  (access.READACCESS = 1 or (access.READACCESS <= 0 and BIN_AND(f.FLAGS, 2) <> 2)) ORDER BY c.SORTORDER, f.CATEGORYID, c.NAME			  
               into
               :"CategoryID",			  
               :"CategoryName",
               :"SortOrder"
                DO SUSPEND;			 
END;
--GO


CREATE PROCEDURE {objectQualifier}POLLGROUP_SAVE
(   i_TopicID integer,
    i_ForumID integer,
	i_CategoryID integer,	
	i_UserID integer,	
	i_Flags integer,	
	i_UTCTIMESTAMP timestamp
)
RETURNS
(
"PollGroupID" INTEGER,
"NewPollGroupID" INTEGER
)
AS
declare i_PollGroupID integer;
declare i_NewPollGroupID integer;
BEGIN
 -- Check if the group already exists
  IF (:i_TopicId is not null) THEN
  BEGIN  
                select PollID  from {objectQualifier}Topic WHERE TopicID = :i_TopicID into :i_PollGroupID; 
  END				              
  IF (:i_ForumID is not null) THEN 
  BEGIN             
				select  PollGroupID    from {objectQualifier}Forum WHERE ForumID = :i_ForumID into  :i_PollGroupID ;  
  END              
  if (:i_CategoryID is not null) THEN
  BEGIN
                select PollGroupID  from {objectQualifier}Category
				WHERE CategoryID = :i_CategoryID into  :i_PollGroupID;
  END
  IF (:i_PollGroupID IS NULL) THEN
  begin
				  INSERT INTO {objectQualifier}PollGroupCluster(UserID,Flags ) VALUES(:i_UserID, :i_Flags) 
				   returning POLLGROUPID INTO :i_NewPollGroupID;		  
  END
     SELECT :i_PollGroupID , :i_NewPollGroupID FROM RDB$DATABASE 
	  INTO :"PollGroupID",:"NewPollGroupID";
	  SUSPEND;	
END;
--GO

CREATE PROCEDURE {objectQualifier}POLL_SAVE
(
i_Question varchar(255), 
    i_Closes TIMESTAMP,
	i_UserID INTEGER,	
	i_PollGroupID INTEGER,
	i_ObjectPath VARCHAR(255),  
	i_MimeType VARCHAR(255), 
	i_Flags INTEGER,
	i_UTCTIMESTAMP TIMESTAMP
)
RETURNS("PollID" INTEGER)
AS
BEGIN
 insert INTO {objectQualifier}Poll(PollID,Question,Closes,PollGroupID,UserID,ObjectPath,MimeType,Flags)
 values((SELECT NEXT VALUE FOR SEQ_{objectQualifier}POLL_POLLID FROM RDB$DATABASE),:i_Question,:i_Closes,:i_PollGroupID,:i_UserID,:i_ObjectPath,:i_MimeType,:i_Flags) 
 RETURNING POLLID INTO :"PollID"; 
 SUSPEND;			
END;
--GO


CREATE PROCEDURE {objectQualifier}CHOICE_SAVE
(
    i_PollID INTEGER, 
    i_Choice VARCHAR(255),
	i_Votes INTEGER,	
	i_ObjectPath varchar(255),  
	i_MimeType varchar(50), 	
	i_UTCTIMESTAMP TIMESTAMP
)
AS
BEGIN
insert INTO {objectQualifier}Choice(ChoiceID,PollID,Choice,Votes,ObjectPath,MimeType)
 values((SELECT NEXT VALUE FOR SEQ_{objectQualifier}CHOICE_CHOICEID FROM RDB$DATABASE),:i_PollID,:i_Choice,:i_Votes,:i_ObjectPath,:i_MimeType); 	
END;
--GO


CREATE PROCEDURE {objectQualifier}POLLGROUP_SETLINKS
(
    i_TopicID INTEGER,
    i_ForumID INTEGER,
	i_CategoryID INTEGER,	
	i_PollGroupID INTEGER,
	i_UTCTIMESTAMP TIMESTAMP
)
AS
BEGIN
  -- we don't update if no new group is being created 
                IF  (:i_PollGroupID IS NOT NULL) THEN
				BEGIN 
                -- fill a pollgroup field - double work if a poll exists 
                if (:i_TopicId is not null) THEN
                    UPDATE {objectQualifier}Topic SET PollID = :i_PollGroupID WHERE TopicID = :i_TopicID; 
				-- fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
                else if (:i_ForumID is not null) THEN                 
                    UPDATE {objectQualifier}Forum SET PollGroupID= :i_PollGroupID WHERE ForumID= :i_ForumID; 
                -- fill a pollgroup field in Category Table if the call comes from a category's topic list 
                else if (:i_CategoryID is not null)   THEN           
                    UPDATE {objectQualifier}Category SET PollGroupID= :i_PollGroupID WHERE CategoryID= :i_CategoryID;                 
              END
                -- fill a pollgroup field in Board Table if the call comes from the main page poll 
                
                -- the functionality is primitive for other databases compliance.It will save calls for each choice. 
END;
--GO


CREATE PROCEDURE {objectQualifier}USERPROFILE_SETPROPERTIES
(    
	I_TABLENAME INTEGER,
	I_PARAMSTRING BLOB SUB_TYPE 1,
	I_VALUESTRING BLOB SUB_TYPE 1,
	I_MATCHPARAM1 
	I_UTCTIMESTAMP TIMESTAMP
)
AS
BEGIN

-- TO DO LOOP HERE TO ADD COLONS.
/* FOR 
    EXECUTE STATEMENT 'UPDATE OR INSERT INTO ' || :I_TABLENAME || '(' || I_VALUESTRING ||
	 ') VALUES( ' || :I_VALUESTRING || ') MATCHING ' || :I_MATCHPARAM1 || ',' || :I_MATCHPARAM2 || '' 	 
 DO   
  SUSPEND; */
  
END;
--GO
