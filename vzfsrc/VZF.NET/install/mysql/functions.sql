-- This scripts for MySQL for Yet Another Forum http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team https://github.com/vzrus http://sourceforge.net/projects/yaf-datalayers/
-- They are distributed under terms of GPLv2 only licence as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2006-2012

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}bitset;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_posts;
--GO 
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_topics;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_posts1;
--GO 
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_posts2;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_posts3;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_topics1;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_topics2;
--GO 
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_topics3;
--GO 
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}medal_gethide;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}medal_getsortorder;
--GO 
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_lasttopic;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_subforums;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}medal_getribbonsetting;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_lastposted;
--GO 
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}get_userstyle;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}message_getthanksinfo;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_save_dependency;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}forum_save_parentschecker;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}biginttobool;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}biginttoint;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}inttobool;
--GO
-- vaccess functions drops 
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}vaccess_s_readaccess_combo;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}vaccess_s_isadmin;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}vaccess_s_isforummoderator;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}vaccess_s_ismoderator;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}vaccess_s_moderatoraccess;
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}registry_value;
--GO

-- vaccess functions 
/* ********************************************************************  */
/* ReadAccess Block */
/* ********************************************************************  */ 

CREATE FUNCTION {databaseSchema}.{objectQualifier}vaccess_s_readaccess_combo (i_UserID INT, i_ForumID INT)
RETURNS INT
READS SQL DATA
BEGIN
RETURN GREATEST(IFNULL((SELECT
c.Flags & 1 AS ReadAccess                              
FROM          {databaseSchema}.{objectQualifier}UserForum AS b
INNER JOIN    {databaseSchema}.{objectQualifier}AccessMask AS c
ON c.AccessMaskID = b.AccessMaskID  
WHERE b.UserID=i_UserID AND b.ForumID=IFNULL(i_ForumID,0) LIMIT 1),0),
IFNULL((SELECT
d.Flags & 1 AS ReadAccess
FROM   {databaseSchema}.{objectQualifier}UserGroup AS b
INNER JOIN {databaseSchema}.{objectQualifier}ForumAccess AS c
ON c.GroupID = b.GroupID
INNER JOIN {databaseSchema}.{objectQualifier}AccessMask AS d
ON d.AccessMaskID = c.AccessMaskID
WHERE b.UserID=i_UserID AND c.ForumID=IFNULL(i_ForumID,0) LIMIT 1),0));
END;


/* ********************************************************************  */
/* isadmin Chunk */
/* ********************************************************************  */
 
CREATE FUNCTION {databaseSchema}.{objectQualifier}vaccess_s_isadmin(i_UserID INT, i_ForumID INT)
RETURNS INT
READS SQL DATA
BEGIN
 -- DECLARE out_IsAdmin INT DEFAULT 0;
RETURN (SELECT MAX(b.Flags & 1)
			FROM {databaseSchema}.{objectQualifier}UserGroup a             
		   JOIN {databaseSchema}.{objectQualifier}Group b
			 ON b.GroupID = a.GroupID
			 WHERE a.UserID=i_UserID  LIMIT 1);
 
 -- RETURN out_IsAdmin;
END;
--GO


/* ********************************************************************  */
/* IsForumModerator Block */
/* ********************************************************************  */

CREATE FUNCTION {databaseSchema}.{objectQualifier}vaccess_s_isforummoderator(i_UserID INT, i_ForumID INT)
RETURNS INT
READS SQL DATA
BEGIN
-- DECLARE ici_IsAdmin INT DEFAULT 0;
-- DECLARE out_IsForumModerator INT DEFAULT 0;
RETURN (IFNULL((SELECT         
	  MAX(b.Flags & 8)     
	  FROM  {databaseSchema}.{objectQualifier}UserGroup a             
		   JOIN {databaseSchema}.{objectQualifier}Group b
			 ON b.GroupID = a.GroupID
			 WHERE a.UserID=i_UserID),0));
 
-- out_IsForumModerator;
END;
--GO

/* ********************************************************************  */
/* IsModerator Block */
/* ********************************************************************  */

CREATE FUNCTION {databaseSchema}.{objectQualifier}vaccess_s_ismoderator(i_UserID INT, i_ForumID INT)
RETURNS INT
READS SQL DATA
BEGIN
-- DECLARE IsModerator INT DEFAULT 0;
return 
(IFNULL((select count(v.UserID) 
from ((({databaseSchema}.{objectQualifier}UserGroup v join {databaseSchema}.{objectQualifier}Group `w`
on((v.GroupID= w.GroupID))) join {databaseSchema}.{objectQualifier}ForumAccess c)
join {databaseSchema}.{objectQualifier}accessmask `y`) where ((v.UserID = i_UserID)
and (c.GroupID = w.GroupID) and (y.AccessMaskID = c.AccessMaskID)
and ((y.Flags & 64) <> 0))),0));
 -- RETURN IsModerator;
  END;
--GO 


/* ********************************************************************  */
/* ModeratorAccess Block */
/* ********************************************************************  */

CREATE FUNCTION {databaseSchema}.{objectQualifier}vaccess_s_moderatoraccess(i_UserID INT, i_ForumID INT)
RETURNS INT
READS SQL DATA
BEGIN

DECLARE ici_ModeratorAccess INT;
DECLARE out_ModeratorAccess INT;  

SELECT ModeratorAccess  
INTO ici_ModeratorAccess FROM {databaseSchema}.{objectQualifier}vaccessfull1  WHERE UserID=i_UserID AND ForumID=IFNULL(i_ForumID,0)  LIMIT 1;

SELECT ModeratorAccess  
INTO out_ModeratorAccess FROM {databaseSchema}.{objectQualifier}vaccessfull2  WHERE UserID=i_UserID AND ForumID=IFNULL(i_ForumID,0) LIMIT 1;

IF (ici_ModeratorAccess IS NOT NULL) THEN
SET out_ModeratorAccess=GREATEST(ici_ModeratorAccess,out_ModeratorAccess);
END IF;
RETURN out_ModeratorAccess;

END;
--GO 

-- eof vaccess functions 


 -- bitset FUNCTION CREATED BY VZ_TEAM

CREATE FUNCTION {databaseSchema}.{objectQualifier}bitset(iFlags INT, iMask INT)
RETURNS TINYINT(1)
NO SQL
BEGIN
DECLARE obool TINYINT(1);
IF (iFlags & iMask) = iMask THEN
SET obool = 1;
ELSE
SET obool = 0;
END IF;
RETURN obool;
END ;
--GO 

/****************************************/
/* forum_posts recursion emulation chain */
/****************************************/

CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_posts(iForumID INT)
RETURNS INT
READS SQL DATA
BEGIN
DECLARE  oNumPosts INT DEFAULT 0;
DECLARE itmpp INT;
DECLARE ctpcr20 CURSOR  FOR
SELECT b.`ForumID`
FROM   {databaseSchema}.{objectQualifier}Forum b
WHERE  b.`ParentID` = iForumID;

SELECT NumPosts INTO oNumPosts
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ForumID = iForumID;

IF EXISTS(SELECT 1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ParentID = iForumID) THEN

OPEN ctpcr20;
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP
FETCH ctpcr20 INTO itmpp;
SET oNumPosts = oNumPosts+ {databaseSchema}.{objectQualifier}forum_posts1(itmpp);
END LOOP;
END;

CLOSE ctpcr20;

END IF;
RETURN oNumPosts;
END ;
--GO 

/* FUNCTION CREATED BY VZ_TEAM */ 
 
CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_posts1(iForumID  INT)
RETURNS INT
READS SQL DATA
BEGIN
DECLARE  oNumPosts1 INT DEFAULT 0;
DECLARE  itmpp1 INT;

DECLARE ctpcr21 CURSOR  FOR
SELECT b.`ForumID`
FROM   {databaseSchema}.{objectQualifier}Forum b
WHERE  b.`ParentID` = iForumID;



SELECT NumPosts INTO oNumPosts1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ForumID = iForumID;


IF EXISTS(SELECT 1 FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ParentID = iForumID) THEN
OPEN ctpcr21;

BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP
FETCH ctpcr21 INTO itmpp1;
SET oNumPosts1 = oNumPosts1+ {databaseSchema}.{objectQualifier}forum_posts2(itmpp1);
END LOOP;
END;
CLOSE ctpcr21;

END IF;
RETURN oNumPosts1;
END;
--GO 

/* forum_posts2 FUNCTION CREATED BY VZ_TEAM */

CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_posts2(iForumID  INT)
RETURNS INT
READS SQL DATA
BEGIN
DECLARE  oNumPosts2 INT DEFAULT 0;
DECLARE itmpp2 INT;

DECLARE ctpcr2p CURSOR  FOR
SELECT b.`ForumID`
FROM   {databaseSchema}.{objectQualifier}Forum b
WHERE  b.`ParentID` = iForumID;

SELECT NumPosts INTO oNumPosts2
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ForumID = iForumID;

IF EXISTS (SELECT 1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ParentID = iForumID)   THEN
OPEN ctpcr2p;

BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP
FETCH ctpcr2p INTO itmpp2;
SET oNumPosts2 = oNumPosts2+ {databaseSchema}.{objectQualifier}forum_posts3(itmpp2);
END LOOP;
END;
CLOSE ctpcr2p;

END IF;
RETURN oNumPosts2;
END ;
--GO 

/* FUNCTION CREATED BY VZ_TEAM */ 
 
CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_posts3(iForumID  INT)
RETURNS INT
READS SQL DATA
BEGIN
DECLARE  oNumPosts3 INT DEFAULT 0;
DECLARE  itmpp3 INT;


DECLARE ctpcr3p CURSOR  FOR
SELECT b.`ForumID`
FROM   {databaseSchema}.{objectQualifier}Forum b
WHERE  b.`ParentID` = iForumID;



SELECT NumPosts INTO oNumPosts3
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ForumID = iForumID;



IF EXISTS (SELECT 1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ParentID = iForumID) THEN
OPEN ctpcr3p;

BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP
FETCH ctpcr3p INTO itmpp3;
SET oNumPosts3 = oNumPosts3+1;
END LOOP;
END;
CLOSE ctpcr3p;

END IF;
RETURN oNumPosts3;
END ;
--GO

/****************************************/
/* forum_topics recursion emulation chain */
/****************************************/

/* forum_topics FUNCTION CREATED BY VZ_TEAM */

CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_topics(iForumID INT)
RETURNS INT
READS SQL DATA
BEGIN
DECLARE  TopicID INT;
DECLARE oNumTopics INT DEFAULT 0;
DECLARE  l_NumPosts INT;
DECLARE  itmpt INT;

DECLARE ctpcr  CURSOR  FOR
SELECT a.`ForumID`
FROM   {databaseSchema}.{objectQualifier}Forum a
WHERE  a.`ParentID` = iForumID;

SELECT NumTopics INTO oNumTopics
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ForumID = iForumID;

IF EXISTS(SELECT 1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ParentID = iForumID) THEN
OPEN ctpcr ;


BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP
FETCH ctpcr INTO itmpt;

SET oNumTopics = oNumTopics+ {databaseSchema}.{objectQualifier}forum_topics1(itmpt);
END LOOP;
END;


CLOSE ctpcr ;

END IF;
RETURN oNumTopics;
END;
--GO 

/* FUNCTION CREATED BY VZ_TEAM */ 
 
CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_topics1(iForumID INT)
RETURNS int(11)
READS SQL DATA
BEGIN
DECLARE oNumTopics1 INT DEFAULT 0;
DECLARE  itmpt1 INT;

DECLARE ctpcr1t  CURSOR  FOR
SELECT a.`ForumID`
FROM   {databaseSchema}.{objectQualifier}Forum a
WHERE  a.`ParentID` = iForumID;

SELECT NumTopics INTO oNumTopics1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ForumID = iForumID;

IF EXISTS (SELECT 1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ParentID = iForumID) THEN
OPEN ctpcr1t ;

BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP
FETCH ctpcr1t INTO itmpt1;
SET oNumTopics1 = oNumTopics1+{databaseSchema}.{objectQualifier}forum_topics2(itmpt1);
END LOOP;
END;
CLOSE ctpcr1t ;

END IF;
RETURN oNumTopics1;
END ;
--GO 

/* forum_topics2 FUNCTION CREATED BY VZ_TEAM */

CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_topics2(iForumID INT)
RETURNS INT
READS SQL DATA
BEGIN

DECLARE oNumTopics2 INT DEFAULT 0;
DECLARE itmpt2 INT DEFAULT 0;


DECLARE ctpcr2t  CURSOR  FOR
SELECT a.`ForumID`
FROM   {databaseSchema}.{objectQualifier}Forum a
WHERE  a.`ParentID` = iForumID;

SELECT NumTopics INTO oNumTopics2
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ForumID = iForumID;

IF EXISTS(SELECT 1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ParentID = iForumID) THEN
OPEN ctpcr2t ;

BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP
FETCH ctpcr2t INTO itmpt2;
SET oNumTopics2 = oNumTopics2+ {databaseSchema}.{objectQualifier}forum_topics3(itmpt2);
END LOOP;
END;

CLOSE ctpcr2t ;
END IF;
RETURN oNumTopics2;
END;
--GO 

/* FUNCTION CREATED BY VZ_TEAM */ 

CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_topics3(iForumID INT)
RETURNS INT
READS SQL DATA
BEGIN

DECLARE oNumTopics3 INT DEFAULT 0;
DECLARE itmpt3 INT;


DECLARE ctpcr3t  CURSOR  FOR
SELECT a.`ForumID`
FROM   {databaseSchema}.{objectQualifier}Forum a
WHERE  a.`ParentID` = iForumID;

SELECT NumTopics INTO oNumTopics3
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ForumID = iForumID;

IF EXISTS(SELECT 1
FROM   {databaseSchema}.{objectQualifier}Forum
WHERE  ParentID = iForumID) THEN
OPEN ctpcr3t ;

BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP
FETCH ctpcr3t INTO itmpt3;
SET oNumTopics3 = oNumTopics3+1;
END LOOP;

END;
CLOSE ctpcr3t ;

END IF;
RETURN oNumTopics3;
END;
--GO 

/* medal_gethide FUNCTION CREATED BY VZ_TEAM */

 
CREATE FUNCTION {databaseSchema}.{objectQualifier}medal_gethide
(
i_Hide TINYINT(1),
i_Flags int
)
RETURNS TINYINT(1)
NO SQL
BEGIN
IF ((i_Flags & 4) = 0) THEN SET i_Hide = 0; END IF;
RETURN i_Hide;
END;
--GO 


/* medal_getsortorder FUNCTION CREATED BY VZ_TEAM */

CREATE FUNCTION {databaseSchema}.{objectQualifier}medal_getsortorder
(
i_SortOrder TINYINT,
i_DefaultSortOrder TiNYINT,
i_Flags INT
)
RETURNS TINYINT
NO SQL
BEGIN
IF ((i_Flags & 8) = 0) THEN SET i_SortOrder = i_DefaultSortOrder; END IF;
RETURN i_SortOrder;

END;
--GO 

 /*FUNCTION CREATED BY VZ_TEAM */ 

 CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_lasttopic 
 
 (	
	i_ForumID INT,
	i_UserID INT,
	i_LastTopicID INT,
	i_LastPosted DATETIME 
 ) 
RETURNS INT
READS SQL DATA
 BEGIN
	/*local variables for temporary values*/
	DECLARE ici_SubforumID INT;   
	DECLARE ici_TopicID INT;
	DECLARE ici_Posted DATETIME;
		DECLARE cltt CURSOR FOR
			SELECT 
				a.ForumID,
				a.LastTopicID,
				a.LastPosted
			FROM
				{databaseSchema}.{objectQualifier}Forum a
			WHERE
				a.ParentID=i_ForumID  AND
				(
					(i_UserID IS NULL AND (a.Flags & 2)=0) OR 
					(((a.Flags & 2)=0 
					OR {databaseSchema}.{objectQualifier}vaccess_s_readaccess_combo(a.ForumID, i_UserID)<>0))
				);
	  
 
	/*try to retrieve last direct topic posed in forums if not supplied as argument*/ 
	if (i_LastTopicID is null or i_LastPosted is null) THEN
		SELECT 
			a.LastTopicID,
			a.LastPosted
				INTO  i_LastTopicID,i_LastPosted
		FROM
			{databaseSchema}.{objectQualifier}Forum a
		WHERE
			a.ForumID=i_ForumID and
			(
				(i_UserID is null and (a.Flags & 2)=0) or 
				(((a.Flags & 2)=0 or {databaseSchema}.{objectQualifier}vaccess_s_readaccess_combo(a.ForumID, i_UserID)<>0))
			);
	END IF;
 
	/*look for newer topic/message in subforums*/
	IF EXISTS(select 1 from {databaseSchema}.{objectQualifier}Forum where ParentID=i_ForumID) THEN 		
		
	
	 open cltt;
				-- cycle through subforums
		
	   BEGIN	
		DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
	   LOOP        
		FETCH cltt INTO ici_SubforumID, ici_TopicID, ici_Posted;       

		-- get last topic/message info for subforum
			SELECT 
			{databaseSchema}.{objectQualifier}forum_lastposted(ici_SubforumID,i_UserID,ici_TopicID,ici_Posted) 				
						INTO ici_TopicID; 
									
			SELECT LastPosted INTO ici_Posted FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicID=ici_TopicID;
			
					-- if subforum has newer topic/message, make it last for parent forum
			IF i_LastPosted is not null AND ici_Posted is not null THEN
			IF (ici_TopicID is not null and ici_Posted is not null and UNIX_TIMESTAMP(i_LastPosted) < UNIX_TIMESTAMP(ici_Posted)) THEN
				SET i_LastTopicID = ici_TopicID;
				SET i_LastPosted = ici_Posted;
			END IF; 
			ELSEIF (i_LastPosted is null AND ici_Posted is not null) THEN 
			   SET i_LastTopicID = ici_TopicID;
			   SET i_LastPosted = ici_Posted;		    
			END IF;       
		
	  END LOOP;
	  END;
		CLOSE cltt;
		
			
 END IF;
	/*return id of topic with last message in this forum or its subforums*/
	RETURN i_LastTopicID;
 END;
--GO 





/* FUNCTION CREATED BY VZ_TEAM */ 
 
CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_subforums(i_ForumID int, i_UserID int) 
RETURNS INT
 READS SQL DATA
 BEGIN
	DECLARE l_NumSubforums INT;
 
	SELECT 
		CAST(COUNT(1) AS UNSIGNED)
		INTO l_NumSubforums	
	FROM 
		{databaseSchema}.{objectQualifier}Forum a
	WHERE 
		((a.Flags & 2)=0 or {databaseSchema}.{objectQualifier}vaccess_s_readaccess_combo(a.ForumID, i_UserID)<>0) AND
		(a.ParentID=i_ForumID);
 
	RETURN l_NumSubforums;
 END;
--GO 

/* FUNCTION CREATED BY VZ_TEAM */ 

 CREATE FUNCTION {databaseSchema}.{objectQualifier}medal_getribbonsetting
 (
	i_RibbonURL VARCHAR(250),
	i_Flags INT,
	i_OnlyRibbon TINYINT(1)
 )
   RETURNS TINYINT(1)
   NO SQL
 BEGIN
 
	IF ((i_RibbonURL IS NULL) OR ((i_Flags & 2) = 0)) THEN SET i_OnlyRibbon = 0;END IF;
 
	RETURN i_OnlyRibbon;
 
 END;
--GO 

/* FUNCTION CREATED BY VZ_TEAM */ 

CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_lastposted 
 
 (	
	i_ForumID INT,
	i_UserID INT,
	i_LastTopicID INT,
	i_LastPosted DATETIME
 )
 RETURNS INT
READS SQL DATA
 BEGIN
	/*local variables for temporary values*/
	DECLARE ici_SubforumID INT;  
	DECLARE ici_TopicID INT;
	DECLARE ici_Posted DATETIME;
		DECLARE ctt CURSOR FOR
			SELECT 
				a.ForumID,
				a.LastTopicID,
				a.LastPosted
			FROM
				{databaseSchema}.{objectQualifier}Forum a
			WHERE
				a.ParentID=i_ForumID and
				(
					(i_UserID is null and (a.Flags & 2)=0) or 
					(((a.Flags & 2)=0 or {databaseSchema}.{objectQualifier}vaccess_s_readaccess_combo(a.ForumID, i_UserID)<>0))
				);
	   

 
	/*try to retrieve last direct topic posed in forums if not supplied as argument */
	IF (i_LastTopicID IS NULL OR i_LastPosted IS NULL) THEN
		SELECT
			a.LastTopicID,
			a.LastPosted
				INTO i_LastTopicID,i_LastPosted  
		FROM
			{databaseSchema}.{objectQualifier}Forum a
		WHERE
			a.ForumID=i_ForumID and
			(
				(i_UserID is null and (a.Flags & 2)=0) or 
				(((a.Flags & 2)=0 or {databaseSchema}.{objectQualifier}vaccess_s_readaccess_combo(a.ForumID, i_UserID)<>0))
			);
	END IF;
 
	/*look for newer topic/message in subforums*/
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Forum WHERE ParentID=i_ForumID)
 
	THEN		
		OPEN ctt;
	BEGIN	
		DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
	LOOP            
	/*cycle through subforums*/
	FETCH ctt INTO ici_SubforumID, ici_TopicID, ici_Posted;

   /* get last topic/message info for subforum */
		
	--	SELECT {databaseSchema}.{objectQualifier}forum_lasttopic(ici_SubforumID, i_UserID, ici_TopicID, ici_Posted)
	--	INTO ici_TopicID;
		
		SELECT LastPosted INTO ici_Posted
		FROM {databaseSchema}.{objectQualifier}Forum  		
		WHERE ForumID = ici_SubforumID LIMIT 1;
	
			/* if subforum has newer topic/message, make it last for parent forum */
			IF ici_TopicID is not null and ici_Posted is not null and i_LastPosted is not null THEN
			IF UNIX_TIMESTAMP(i_LastPosted) < UNIX_TIMESTAMP(ici_Posted) THEN
				SET i_LastTopicID = ici_TopicID;
				SET i_LastPosted = ici_Posted; 		
			END IF;
			END IF; 
	END LOOP;		
	END;

		CLOSE ctt; 		
	END IF; 	
	RETURN i_LastTopicID;
END;
--GO 

/* FUNCTION CREATED BY VZ_TEAM */ 

 
  CREATE FUNCTION {databaseSchema}.{objectQualifier}get_userstyle
 (
	i_UserID INT
 )
   RETURNS VARCHAR(255)
   READS SQL DATA
 BEGIN
 
 declare ici_style varchar(255);
	SET ici_style = ( SELECT c.Style FROM {databaseSchema}.{objectQualifier}User a 
						JOIN {databaseSchema}.{objectQualifier}UserGroup b
						  ON a.UserID = b.UserID
							JOIN {databaseSchema}.{objectQualifier}Group c                         
							  ON b.GroupID = c.GroupID 
							  WHERE a.UserID = i_UserID AND LENGTH(c.Style) > 2 ORDER BY c.SortOrder ASC LIMIT 1);
	   if ( ici_style is null or LENGTH(ici_style) < 3 ) THEN                  
							  set ici_style = (SELECT c.Style FROM {databaseSchema}.{objectQualifier}Rank c 
								JOIN {databaseSchema}.{objectQualifier}User d
								  ON c.RankID = d.RankID AND LENGTH(c.Style) > 2 WHERE d.UserID = i_UserID LIMIT 1);
				 END IF;
	  RETURN ici_style;
 
 END;
--GO
/* FUNCTION CREATED BY VZ_TEAM */ 

  CREATE FUNCTION {databaseSchema}.{objectQualifier}message_getthanksinfo
 (
	i_MessageID INT,
	i_ShowThanksDate TINYINT(1)
 )
   RETURNS VARCHAR(4000)
   READS SQL DATA
 BEGIN 
 DECLARE ici_From  VARCHAR(128);
 DECLARE ici_Date  VARCHAR(50);
 
 DECLARE ici_Output VARCHAR(4000) DEFAULT '';
   DECLARE cth CURSOR FOR
   SELECT
			CONVERT(i.ThanksFromUserID,char(11)), 
	CASE i_ShowThanksDate WHEN 1 THEN (CONCAT(',' , (CONVERT(i.ThanksDate,char(40)))))  ELSE '' end
			FROM	{databaseSchema}.{objectQualifier}Thanks i
			WHERE	i.MessageID = i_MessageID  ORDER BY i.ThanksDate;
	
	OPEN cth;		
	  BEGIN	
		DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
	LOOP            
	/*cycle through subforums*/
	FETCH cth INTO ici_From, ici_Date;    
		SET ici_Output = CONCAT(ici_Output,ici_From, ici_Date,','); 	
	END LOOP;
	END;
		CLOSE cth; 
		RETURN ici_Output; 
 END;
--GO

/* FUNCTION CREATED BY VZ_TEAM */ 


  CREATE FUNCTION {databaseSchema}.{objectQualifier}forum_save_parentschecker
 (
	i_ForumID INT,
	i_ParentID INT
 )
   RETURNS INT
   READS SQL DATA
 BEGIN
  -- Checks if the forum is already referenced as a parent 
	declare i_dependency int default 0;
	declare i_haschildren int default 0;
		declare i_frmtmp int;
	declare i_prntmp int;
	DECLARE ctt CURSOR FOR
			select ForumID,ParentID from {databaseSchema}.{objectQualifier}Forum
		where ParentID = i_ForumID;

	select ForumID into i_dependency from {databaseSchema}.{objectQualifier}Forum where ParentID=i_ForumID AND ForumID = i_ParentID;
	if i_dependency > 0
	then
	return i_ParentID;
	end if;

	-- simply disable it
	return i_dependency;

	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Forum WHERE ParentID=i_ForumID)
	THEN		
		OPEN ctt;
	BEGIN	
		DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
	LOOP            
	/*cycle through subforums*/
	FETCH ctt INTO i_frmtmp, i_prntmp;
		if i_frmtmp > 0 AND i_frmtmp IS NOT NULL then
		   -- recursion doesn't work for mysql - use cascades             
			-- set i_haschildren = {databaseSchema}.{objectQualifier}forum_save_parentschecker(i_frmtmp,i_ParentID);            
			if  i_prntmp = i_ParentID then
			set i_dependency = i_ParentID;               
			ELSEIF i_haschildren > 0  then
			set i_dependency= i_haschildren;
			end if;      
		end if; 
	END LOOP;
	END;
		CLOSE ctt; 		
	END IF; 
	RETURN i_dependency; 
 END;
--GO


  CREATE FUNCTION {databaseSchema}.{objectQualifier}biginttobool
 (
	toconv BIGINT
 )
   RETURNS TINYINT(1)
   NO SQL
 BEGIN
 RETURN toconv; 
 END;
  --GO


	CREATE FUNCTION {databaseSchema}.{objectQualifier}biginttoint
 (
	toconv BIGINT
 )
   RETURNS INT
   NO SQL
 BEGIN
 RETURN toconv; 
 END;
  --GO

   CREATE FUNCTION {databaseSchema}.{objectQualifier}inttobool
 (
	toconv INT
 )
   RETURNS TINYINT(1)
   NO SQL
 BEGIN
 RETURN toconv; 
 END;
  --GO

CREATE FUNCTION {databaseSchema}.{objectQualifier}registry_value (
	i_Name VARCHAR(64)
	,i_BoardID INT
	)
RETURNS LONGTEXT
READS SQL DATA
BEGIN
	DECLARE ici_returnValue LONGTEXT;

	IF i_BoardID IS NOT NULL AND EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}registry WHERE LOWER(`Name`) = LOWER(i_Name) AND BoardID = i_BoardID) THEN
  
		SET ici_returnValue = (
			SELECT `Value`
			FROM {databaseSchema}.{objectQualifier}Registry
			WHERE LOWER(`Name`) = LOWER(i_Name) AND BoardID = i_BoardID);
  
	ELSE  
		SET ici_returnValue = (
			SELECT `Value`
			FROM {databaseSchema}.{objectQualifier}Registry
			WHERE LOWER(`Name`) = LOWER(i_Name) AND BoardID IS NULL);
	END IF;

	RETURN ici_returnValue;
END;
--GO

