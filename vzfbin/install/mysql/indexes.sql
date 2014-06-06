-- This scripts for MySQL for Yet Another Forum http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team https://github.com/vzrus http://sourceforge.net/projects/yaf-datalayers/
-- They are distributed under terms of GPLv2 only licence as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2006-2012


DROP  PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}add_or_check_indexes;
--GO
CREATE PROCEDURE {databaseSchema}.{objectQualifier}add_or_check_indexes()
BEGIN

SET foreign_key_checks=0; 

/*ADD  INDEXES AT FIRST INSTALL*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}BannedIP' AND S.TABLE_NAME=LOWER('{objectQualifier}BannedIP') AND (S.COLUMN_NAME='BoardID' OR  S.COLUMN_NAME='Mask')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}BannedIP 
ADD UNIQUE INDEX  `IX_{databaseSchema}_{objectQualifier}BannedIP` (`BoardID`, `Mask`);

END IF;

 /* Thanks - it gives duplicate entry 

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Thanks_UserID' AND S.TABLE_NAME=LOWER('{objectQualifier}Thanks') AND (S.COLUMN_NAME='ThanksToUserID' OR  S.COLUMN_NAME='ThanksFromUserID')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Thanks
ADD UNIQUE INDEX  `IX_{databaseSchema}_{objectQualifier}Thanks_UserID` (`ThanksFromUserID`, `ThanksToUserID`);

END IF; 

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}ActiveAccess_UserID_Forum' AND S.TABLE_NAME=LOWER('{objectQualifier}ActiveAccess') AND (S.COLUMN_NAME='UserID' OR  S.COLUMN_NAME='ForumID') LIMIT 1) THEN

ALTER TABLE {databaseSchema}.{objectQualifier}ActiveAccess
ADD UNIQUE INDEX  `IX_{databaseSchema}_{objectQualifier}ActiveAccess_UserID_Forum` (`UserID`,`ForumID`);

END IF;*/

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}MessageHistory_Edited_MessageID' AND S.TABLE_NAME=LOWER('{objectQualifier}MessageHistory') AND (S.COLUMN_NAME='Edited' OR  S.COLUMN_NAME='MessageID') LIMIT 1) THEN

ALTER TABLE {databaseSchema}.{objectQualifier}MessageHistory
ADD INDEX  `IX_{databaseSchema}_{objectQualifier}MessageHistory_Edited_MessageID` (`Edited`);

END IF;

/* IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}ForumReadTracking_UserID_ForumID' AND S.TABLE_NAME=LOWER('{objectQualifier}ForumReadTracking') AND (S.COLUMN_NAME='UserID' OR  S.COLUMN_NAME='ForumID') LIMIT 1) THEN

ALTER TABLE {databaseSchema}.{objectQualifier}ForumReadTracking
ADD  INDEX  `IX_{databaseSchema}_{objectQualifier}ForumReadTracking_UserID_ForumID` (`UserID`,`ForumID`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}TopicReadTracking_UserID_TopicID' AND S.TABLE_NAME=LOWER('{objectQualifier}TopicReadTracking') AND (S.COLUMN_NAME='UserID' OR  S.COLUMN_NAME='TopicID') LIMIT 1) THEN

ALTER TABLE {databaseSchema}.{objectQualifier}TopicReadTracking
ADD  INDEX  `IX_{databaseSchema}_{objectQualifier}TopicReadTracking_UserID_TopicID` (`UserID`,`TopicID`);

END IF; */


-- Buddies 

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Buddy_UserID' AND S.TABLE_NAME=LOWER('{objectQualifier}Buddy') AND (S.COLUMN_NAME='ToUserID' OR  S.COLUMN_NAME='FromUserID')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Buddy
ADD UNIQUE INDEX  `IX_{databaseSchema}_{objectQualifier}Buddy_UserID` (`FromUserID`, `ToUserID`);

END IF;

/*CheckEmail*/

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE INDEX_SCHEMA='{databaseSchema}' AND TABLE_NAME ='{objectQualifier}checkemail' AND S.COLUMN_NAME='Hash' AND INDEX_NAME='IX_{databaseSchema}_{objectQualifier}CheckEmail') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}CheckEmail 
ADD 
UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}CheckEmail` (`Hash`);

END IF;

/*Category*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Category' AND S.TABLE_NAME='{objectQualifier}category' AND (S.COLUMN_NAME='BoardID' OR  S.COLUMN_NAME='Name')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Category 
ADD UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}Category` (`BoardID`, `Name`);

END IF;

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE INDEX_SCHEMA='{databaseSchema}' AND TABLE_NAME ='{objectQualifier}category' AND S.COLUMN_NAME='BoardID' AND INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Category_BoardID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Category 
ADD INDEX `IX_{databaseSchema}_{objectQualifier}Category_BoardID` (`BoardID`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Category_Name' AND S.TABLE_NAME='{objectQualifier}category' AND S.COLUMN_NAME='Name') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Category 
ADD INDEX `IX_{databaseSchema}_{objectQualifier}Category_Name` (`Name`);

END IF;

/*Forum*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Forum' AND S.TABLE_NAME='{objectQualifier}forum' AND (S.COLUMN_NAME='ParentID' OR  S.COLUMN_NAME='Name')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Forum 
ADD
UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}Forum` (`ParentID`, `Name`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Forum_CategoryID' AND S.TABLE_NAME='{objectQualifier}forum' AND S.COLUMN_NAME='CategoryID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Forum 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Forum_CategoryID` (`CategoryID`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Forum_Flags' AND S.TABLE_NAME='{objectQualifier}forum' AND S.COLUMN_NAME='Flags') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Forum 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Forum_Flags` (`Flags`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Forum_ParentID' AND S.TABLE_NAME='{objectQualifier}forum' AND S.COLUMN_NAME='ParentID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Forum 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Forum_ParentID` (`ParentID`);

END IF;

/*ForumAccess*/

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}ForumAccess_ForumID' AND S.TABLE_NAME='{objectQualifier}forumaccess' AND S.COLUMN_NAME='ForumID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}ForumAccess 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}ForumAccess_ForumID` (`ForumID`);

END IF;

/*Group*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Group' AND S.TABLE_NAME='{objectQualifier}group' AND (S.COLUMN_NAME='BoardID' OR  S.COLUMN_NAME='Name')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Group 
ADD
UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}Group` (`BoardID`, `Name`);

END IF;

/*Message*/

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Message_Flags' AND S.TABLE_NAME='{objectQualifier}message' AND S.COLUMN_NAME='Flags') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Message 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Message_Flags` (`Flags`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Message_TopicID' AND S.TABLE_NAME='{objectQualifier}message' AND S.COLUMN_NAME='TopicID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Message 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Message_TopicID` (`TopicID`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Message_UserID' AND S.TABLE_NAME='{objectQualifier}message' AND S.COLUMN_NAME='UserID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Message 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Message_UserID` (`UserID`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Message_Posted_Desc' AND S.TABLE_NAME='{objectQualifier}message' AND S.COLUMN_NAME='Posted') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Message 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Message_Posted_Desc` (`Posted` DESC);
END IF;

/*PollVote*/

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}PollVote_PollID' AND S.TABLE_NAME='{objectQualifier}pollvote' AND S.COLUMN_NAME='PollID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}PollVote 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}PollVote_PollID` (`PollID`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}PollVote_RemoteIP' AND S.TABLE_NAME='{objectQualifier}pollvote' AND S.COLUMN_NAME='RemoteIP') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}PollVote 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}PollVote_RemoteIP` (`RemoteIP`);

END IF;


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}PollVote_UserID' AND S.TABLE_NAME='{objectQualifier}pollvote' AND S.COLUMN_NAME='UserID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}PollVote 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}PollVote_UserID` (`UserID`);

END IF;

/*Rank*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Rank' AND S.TABLE_NAME='{objectQualifier}rank' AND (S.COLUMN_NAME='BoardID' OR  S.COLUMN_NAME='Name')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Rank 
ADD
UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}Rank` (`BoardID`, `Name`);

END IF;

/*Registry*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Registry_Name' AND S.TABLE_NAME='{objectQualifier}registry' AND (S.COLUMN_NAME='BoardID' OR  S.COLUMN_NAME='Name')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Registry 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Registry_Name` (`BoardID`, `Name`);

END IF;

/*Reputation*/
IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}ReputationVote_FU_TU' AND S.TABLE_NAME='{objectQualifier}ReputationVote' AND (S.COLUMN_NAME='ReputationFromUserID' OR  S.COLUMN_NAME='ReputationToUserID')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}ReputationVote 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}ReputationVote_FU_TU` (`ReputationFromUserID`, `ReputationToUserID`);

END IF;

/*Smiley*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Smiley' AND S.TABLE_NAME='{objectQualifier}smiley' AND (S.COLUMN_NAME='BoardID' OR  S.COLUMN_NAME='Code')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Smiley 
ADD
UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}Smiley` (`BoardID`, `Code`);

END IF;

/*Topic*/

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Topic_Flags' AND S.TABLE_NAME='{objectQualifier}topic' AND S.COLUMN_NAME='Flags') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Topic 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Topic_Flags` (`Flags`);

END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Topic_ForumID' AND S.TABLE_NAME='{objectQualifier}topic' AND S.COLUMN_NAME='ForumID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Topic 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Topic_ForumID` (`ForumID`);

END IF;


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Topic_UserID' AND S.TABLE_NAME='{objectQualifier}topic' AND S.COLUMN_NAME='UserID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}Topic 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Topic_UserID` (`UserID`);

END IF;


/* User

-- too long for 5.5
IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}User' AND S.TABLE_NAME='{objectQualifier}user' AND (S.COLUMN_NAME='BoardID' OR  S.COLUMN_NAME='Name')) < 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}User 
ADD
UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}User` (`BoardID`, `Name`);

END IF;
*/

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}User_Flags' AND S.TABLE_NAME='{objectQualifier}user' 
AND S.COLUMN_NAME='Flags') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}User 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}User_Flags` (`Flags`);

END IF;

/* too long for 5.5
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}User_Name' AND S.TABLE_NAME='{objectQualifier}user' 
AND S.COLUMN_NAME='Name') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}User 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}User_Name` (`Name`);

END IF; */

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.TABLE_NAME='{objectQualifier}user' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}User_ProviderUserKey' AND S.COLUMN_NAME='ProviderUserKey') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}User 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}User_ProviderUserKey` (`ProviderUserKey`);

END IF;


/*UserGroup*/

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}UserGroup_UserID' AND S.TABLE_NAME='{objectQualifier}usergroup' AND S.COLUMN_NAME='UserID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}UserGroup 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}UserGroup_UserID` (`UserID`);

END IF;

/*UserPMessage*/

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}UserPMessage_UserID' AND S.TABLE_NAME='{objectQualifier}userpmessage' AND S.COLUMN_NAME='UserID') THEN

ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}UserPMessage_UserID` (`UserID`);

END IF;


/*WatchForum*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}WatchForum' AND S.TABLE_NAME='{objectQualifier}watchforum' AND (S.COLUMN_NAME='ForumID' OR  S.COLUMN_NAME='UserID'))< 2 THEN

ALTER TABLE {databaseSchema}.{objectQualifier}WatchForum 
ADD
UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}WatchForum` (`ForumID`, `UserID`);

END IF;


/*WatchTopic*/

IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}WatchTopic'  AND S.TABLE_NAME='{objectQualifier}watchtopic' AND (S.COLUMN_NAME='TopicID' OR  S.COLUMN_NAME='UserID')) < 2 THEN
ALTER TABLE {databaseSchema}.{objectQualifier}WatchTopic 
ADD
UNIQUE INDEX `IX_{databaseSchema}_{objectQualifier}WatchTopic` (`TopicID`, `UserID`);
END IF;

-- {databaseSchema}.{objectQualifier}Thanks

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Thanks_MessageID'  AND S.TABLE_NAME='{objectQualifier}thanks' AND S.COLUMN_NAME='MessageID')  THEN
ALTER TABLE {databaseSchema}.{objectQualifier}Thanks 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Thanks_MessageID` (`MessageID`);
END IF;


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Thanks_ThanksFromUserID'  AND S.TABLE_NAME='{objectQualifier}thanks' AND S.COLUMN_NAME='ThanksFromUserID')  THEN
ALTER TABLE {databaseSchema}.{objectQualifier}Thanks 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Thanks_ThanksFromUserID` (`ThanksFromUserID`);
END IF;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Thanks_ThanksToUserID'  AND S.TABLE_NAME='{objectQualifier}thanks' AND S.COLUMN_NAME='ThanksToUserID')  THEN
ALTER TABLE {databaseSchema}.{objectQualifier}Thanks 
ADD
INDEX `IX_{databaseSchema}_{objectQualifier}Thanks_ThanksToUserID` (`ThanksToUserID`);
END IF;

SET foreign_key_checks=1; 
END;
--GO

CALL {databaseSchema}.{objectQualifier}add_or_check_indexes();
--GO


