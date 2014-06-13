-- This scripts for MySQL for Yet Another Forum http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team https://github.com/vzrus http://sourceforge.net/projects/yaf-datalayers/
-- They are distributed under terms of GPLv2 only licence as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2006-2012

-- RENAME TABLE {databaseSchema}.{objectQualifier}AccessMask TO {databaseSchema}.{objectQualifier}accessmask;

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}AccessMask
       (
       `AccessMaskID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `Name` VARCHAR(128)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Flags` INT NOT NULL DEFAULT 0,
       `SortOrder` INT NOT NULL DEFAULT 0,
       `CreatedByUserID` INT NULL,
       `CreatedByUserName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `CreatedByUserDisplayName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `CreatedDate` DATETIME,
       `IsUserMask`  TINYINT(1) NOT NULL DEFAULT 0,
       `IsAdminMask`  TINYINT(1) NOT NULL DEFAULT 0,
       PRIMARY KEY (`AccessMaskID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO


CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}AccessMaskHistory
       (
       `AccessMaskID` INT NOT NULL,    
       `ChangedUserID`		INT,	
       `ChangedUserName`	VARCHAR (255)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `ChangedDisplayName`	VARCHAR (255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation}  NOT NULL,
       `ChangedDate`         DATETIME  NOT NULL,    
       PRIMARY KEY (`AccessMaskID`,`ChangedDate`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Active
       (
       `SessionID` VARCHAR(24) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `BoardID` INT NOT NULL,
       `UserID` INT NOT NULL,
       `IP` VARCHAR(39) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Login` DATETIME NOT NULL,
       `LastActive` DATETIME NOT NULL,
       `Location` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `ForumPage` VARCHAR(1024) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `ForumID` INT NULL,
       `TopicID` INT NULL,
       `Browser` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Platform` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Flags` INT NOT NULL DEFAULT 0,
       PRIMARY KEY (`SessionID`, `BoardID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}ActiveAccess
        (		
        `UserID`			    int NOT NULL ,
        `BoardID`			    int NOT NULL ,			
        `ForumID`			    int NOT NULL DEFAULT 0,
        `IsAdmin`				TINYINT(1) NOT NULL DEFAULT 0,
        `IsForumModerator`	    TINYINT(1) NOT NULL DEFAULT 0,
        `IsModerator`			TINYINT(1) NOT NULL DEFAULT 0,
        `IsGuestX`		     	TINYINT(1) NOT NULL DEFAULT 0,
        `LastActive`		    DATETIME NOT NULL,
        `ReadAccess`			TINYINT(1) NOT NULL DEFAULT 0,
        `PostAccess`			TINYINT(1) NOT NULL DEFAULT 0,
        `ReplyAccess`			TINYINT(1) NOT NULL DEFAULT 0,
        `PriorityAccess`		TINYINT(1) NOT NULL DEFAULT 0,
        `PollAccess`			TINYINT(1) NOT NULL DEFAULT 0,
        `VoteAccess`			TINYINT(1) NOT NULL DEFAULT 0,
        `ModeratorAccess`		TINYINT(1) NOT NULL DEFAULT 0,
        `EditAccess`			TINYINT(1) NOT NULL DEFAULT 0,
        `DeleteAccess`		    TINYINT(1) NOT NULL DEFAULT 0,
        `UploadAccess`		    TINYINT(1) NOT NULL DEFAULT 0,		
        `DownloadAccess`		TINYINT(1) NOT NULL DEFAULT 0,
        `UserForumAccess`       TINYINT(1) NOT NULL DEFAULT 0
        )
        ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}AdminPageUserAccess
       (
       `UserID` INT NOT NULL,	  
       `PageName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL	  
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}EventLogGroupAccess
       (
       `GroupID`		       int NOT NULL,
       `EventTypeID`           int NOT NULL,
       `EventTypeName`	       varchar (128) NOT NULL,
       `DeleteAccess`          TINYINT(1) NOT NULL DEFAULT 0
    )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Attachment
       (
       `AttachmentID` INT NOT NULL AUTO_INCREMENT,
       `MessageID` INT NOT NULL,
       `FileName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Bytes` INT NOT NULL,
       `FileID` INT NULL,
       `ContentType` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Downloads` INT NOT NULL,
       `FileData` LONGBLOB NULL,
       PRIMARY KEY (`AttachmentID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}BannedIP
       (
       `ID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `Mask` VARCHAR(57) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Since` DATETIME NOT NULL,
       `Reason`  VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `UserID` INT NOT NULL,
       PRIMARY KEY (`ID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}BBCode
       (
       `BBCodeID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `Name` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Description` VARCHAR(4000) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `OnClickJS` VARCHAR(1000) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `DisplayJS` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `EditJS` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `DisplayCSS` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `SearchRegex` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `ReplaceRegex` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Variables` VARCHAR(1000) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `UseModule` TINYINT(1) NULL,
       `ModuleClass` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `ExecOrder` INT NOT NULL,
       PRIMARY KEY (`BBCodeID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Board
       (
       `BoardID` INT NOT NULL AUTO_INCREMENT,
       `BoardUID` BINARY(16),
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `AllowThreaded` TINYINT(1) NOT NULL,
       `MembershipAppName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `RolesAppName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       PRIMARY KEY (`BoardID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Category
       (
       `CategoryID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `CategoryImage` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `SortOrder` SMALLINT(5) NOT NULL,
       `PollGroupID` int,
	   `CanHavePersForums` TINYINT(1) NOT NULL DEFAULT 0,
       PRIMARY KEY (`CategoryID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}CheckEmail
       (
       `CheckEmailID` INT NOT NULL AUTO_INCREMENT,
       `UserID` INT NOT NULL,
       `Email` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Created` DATETIME NOT NULL,
       `Hash` VARCHAR(32) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       PRIMARY KEY (`CheckEmailID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Choice
       (
       `ChoiceID` INT NOT NULL AUTO_INCREMENT,
       `PollID` INT NOT NULL,
       `Choice` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Votes` INT NOT NULL DEFAULT 0,
       `ObjectPath` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `MimeType` VARCHAR(50) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       PRIMARY KEY (`ChoiceID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}EventLog
       (
       `EventLogID` INT NOT NULL AUTO_INCREMENT,
       `EventTime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
       `UserID` INT NULL,
       `Source` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Description` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Type` INT NOT NULL DEFAULT 0,
       PRIMARY KEY (`EventLogID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Extension
       (
       `ExtensionID` INT NOT NULL AUTO_INCREMENT,
       `BoardId` INT NOT NULL DEFAULT 1,
       `Extension` VARCHAR(10) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       PRIMARY KEY (`ExtensionID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Forum
       (
       `ForumID` INT NOT NULL AUTO_INCREMENT,
       `CategoryID` INT NOT NULL,
       `ParentID` INT NULL,
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Description` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `SortOrder` INT NOT NULL DEFAULT 0,
       `LastPosted` DATETIME NULL,
       `LastTopicID` INT NULL,
       `LastMessageID` INT NULL,
       `LastUserID` INT NULL,
       `LastUserName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `LastUserDisplayName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `NumTopics` INT NOT NULL,
       `NumPosts` INT NOT NULL,
       `RemoteURL` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Flags` INT NOT NULL DEFAULT 0,
       `ThemeURL` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `ImageURL` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Styles` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `PollGroupID` INT NULL,
       `CreatedByUserID` INT NULL,
       `CreatedByUserName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `CreatedByUserDisplayName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `CreatedDate` DATETIME,
       `IsUserForum`  TINYINT(1) NOT NULL DEFAULT 0,
	   `CanHavePersForums`  TINYINT(1) NOT NULL DEFAULT 0,
       PRIMARY KEY (`ForumID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}ForumHistory
       (
       `ForumID` INT NOT NULL,    
       `ChangedUserID`		INT,	
       `ChangedUserName`	VARCHAR (255)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `ChangedDisplayName`	VARCHAR (255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation}  NOT NULL,
       `ChangedDate`         DATETIME  NOT NULL,    
       PRIMARY KEY (`ForumID`,`ChangedDate`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}ForumAccess
       (
       `GroupID` INT NOT NULL,
       `ForumID` INT NOT NULL,
       `AccessMaskID` INT NOT NULL,
       PRIMARY KEY (`GroupID`, `ForumID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Group
       (
       `GroupID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Flags` INT NOT NULL DEFAULT 0,
       `PMLimit` INT,
       `Style`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `SortOrder`  INT NOT NULL DEFAULT 0,
       `Description` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `UsrSigChars`   INT NOT NULL DEFAULT 0,
       `UsrSigBBCodes` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `UsrSigHTMLTags` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `UsrAlbums`  INT NOT NULL DEFAULT 0,
       `UsrAlbumImages`  INT NOT NULL DEFAULT 0,
       `CreatedByUserID` INT NULL,
       `CreatedByUserName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `CreatedByUserDisplayName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `CreatedDate` DATETIME,   
       `IsHidden` TINYINT(1) NOT NULL DEFAULT 0,
       `IsUserGroup` TINYINT(1) NOT NULL DEFAULT 0,
	   `UsrPersonalForums`  INT NOT NULL DEFAULT 0,
	   `UsrPersonalMasks`  INT NOT NULL DEFAULT 0,
	   `UsrPersonalGroups`  INT NOT NULL DEFAULT 0,
       PRIMARY KEY (`GroupID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}GroupHistory
       (
       `GroupID` INT NOT NULL,    
       `ChangedUserID`		INT,	
       `ChangedUserName`	VARCHAR (255)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `ChangedDisplayName`	VARCHAR (255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation}  NOT NULL,
       `ChangedDate`         DATETIME  NOT NULL,    
       PRIMARY KEY (`GroupID`,`ChangedDate`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}GroupHistory
       (
       `GroupID` INT NOT NULL,    
       `ChangedUserID`		INT NOT NULL,	
       `ChangedUserName`	VARCHAR (255)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `ChangedDisplayName`	VARCHAR (255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation}  NOT NULL,
       `ChangedDate`        DATETIME  NOT NULL,    
       PRIMARY KEY (`GroupID`,`ChangedDate`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}GroupMedal
       (
       `GroupID` INT NOT NULL,
       `MedalID` INT NOT NULL,
       `Message` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Hide` TINYINT NOT NULL DEFAULT 0,
       `OnlyRibbon` TINYINT(1) NOT NULL DEFAULT 0,
       `SortOrder` TINYINT(3) UNSIGNED NOT NULL DEFAULT 255
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Mail
       (
       `MailID` INT NOT NULL AUTO_INCREMENT,
       `FromUser` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `FromUserName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `ToUser` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `ToUserName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Created` DATETIME NOT NULL,
       `Subject` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Body` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `BodyHtml` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `SendTries` INT NOT NULL DEFAULT 0,
       `SendAttempt` DATETIME NULL,
       `ProcessID` INT NULL,
       PRIMARY KEY (`MailID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO


CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Medal
       (
       `BoardID` INT NOT NULL,
       `MedalID` INT NOT NULL AUTO_INCREMENT,
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Description` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Message` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Category` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `MedalURL` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `RibbonURL` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `SmallMedalURL` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `SmallRibbonURL` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `SmallMedalWidth` SMALLINT(5) NOT NULL,
       `SmallMedalHeight` SMALLINT(5) NOT NULL,
       `SmallRibbonWidth` SMALLINT(5) NULL,
       `SmallRibbonHeight` SMALLINT(5) NULL,
       `SortOrder` TINYINT(3) UNSIGNED NOT NULL DEFAULT 255,
       `Flags` INT NOT NULL DEFAULT 0,
       PRIMARY KEY (`MedalID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Message
       (
       `MessageID` INT NOT NULL AUTO_INCREMENT,
       `TopicID` INT NOT NULL,
       `ReplyTo` INT NULL,
       `Position` INT NOT NULL,
       `Indent` INT NOT NULL,
       `UserID` INT NOT NULL,
       `UserName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `UserDisplayName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Posted` DATETIME NOT NULL,
       `Message` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL ,
	   `Description` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `IP` VARCHAR(39) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Edited` DATETIME NULL,
       `EditedBy` INT,
       `Flags` INT NOT NULL DEFAULT 23,
       `EditReason` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `IsModeratorChanged` TINYINT(1) NOT NULL DEFAULT 0,
       `DeleteReason` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `BlogPostID` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `ExternalMessageId`	varchar(255) NULL,
       `ReferenceMessageId` varchar(255) NULL,
       PRIMARY KEY (`MessageID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}MessageHistory
       (
       `MessageID` INT NOT NULL,
       `Message` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL ,
       `IP` VARCHAR(39) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Edited` DATETIME NULL,
       `EditedBy` INT,
       `EditReason` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `IsModeratorChanged` TINYINT(1) NOT NULL DEFAULT 0,
       `Flags` INT NOT NULL DEFAULT 23
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}MessageReported
       (
       `MessageID` INT NOT NULL,
       `Message` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Resolved` TINYINT(1) NULL,
       `ResolvedBy` INT NULL,
       `ResolvedDate` DATETIME NULL
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}MessageReportedAudit
       (
       `LogID` INT NOT NULL AUTO_INCREMENT,
       `UserID` INT NULL,
       `MessageID` INT NULL,
       `Reported` DATETIME NULL,
       PRIMARY KEY (`LogID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}NntpForum
       (
       `NntpForumID` INT NOT NULL AUTO_INCREMENT,
       `NntpServerID` INT NOT NULL,
       `GroupName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `ForumID` INT NOT NULL,
       `LastMessageNo` INT NOT NULL,
       `LastUpdate` DATETIME NOT NULL,
       `Active` TINYINT(1) NOT NULL,
       `DateCutOff` DATETIME,
       PRIMARY KEY (`NntpForumID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}NntpServer
       (
       `NntpServerID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Address` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Port` INT NULL,
       `UserName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `UserPass` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       PRIMARY KEY (`NntpServerID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}NntpTopic
       (
       `NntpTopicID` INT NOT NULL AUTO_INCREMENT,
       `NntpForumID` INT NOT NULL,
       `Thread` VARCHAR(64) NOT NULL,
       `TopicID` INT NOT NULL,
       PRIMARY KEY (`NntpTopicID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}PMessage
       (
       `PMessageID` INT NOT NULL AUTO_INCREMENT,
       `FromUserID` INT NOT NULL,
       `Created` DATETIME NOT NULL,
       `Subject` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Body` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Flags` INT NOT NULL,
       `ReplyTo` INT,
       PRIMARY KEY (`PMessageID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}PollGroupCluster
       (
       `PollGroupID` INT NOT NULL AUTO_INCREMENT,
       `UserID`	int not NULL,
       `Flags` int NOT NULL DEFAULT 0,
       PRIMARY KEY (`PollGroupID`)
       );
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Poll
       (
       `PollID` INT NOT NULL AUTO_INCREMENT,
       `Question` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Closes` DATETIME NULL,
       `PollGroupID` int NULL,
       `UserID` int not NULL,
       `ObjectPath` varchar(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `MimeType` varchar(50) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Flags` int NULL,
       PRIMARY KEY (`PollID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}PollVote
       (
       `PollVoteID` INT NOT NULL AUTO_INCREMENT,
       `PollID` INT NOT NULL,
       `UserID` INT NULL,
       `RemoteIP` VARCHAR(57) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `ChoiceID` INT,
       PRIMARY KEY (`PollVoteID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}PollVoteRefuse
       (
       `RefuseID` INT NOT NULL AUTO_INCREMENT,
       `PollID` INT NOT NULL,
       `UserID` INT NULL,
       `RemoteIP` VARCHAR(57) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       PRIMARY KEY (`RefuseID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Rank
       (
       `RankID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `MinPosts` INT NULL,
       `RankImage` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Flags` INT NOT NULL DEFAULT 0,
       `PMLimit` INT NOT NULL DEFAULT 0,
       `Style`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `SortOrder`  INT NOT NULL DEFAULT 0,
       `Description` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation}, 
       `UsrSigChars`   INT NOT NULL DEFAULT 0,
       `UsrSigBBCodes` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `UsrSigHTMLTags` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
       `UsrAlbums`  INT NOT NULL DEFAULT 0,
       `UsrAlbumImages`  INT NOT NULL DEFAULT 0,
       PRIMARY KEY (`RankID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Registry
       (
       `RegistryID` INT NOT NULL AUTO_INCREMENT,
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Value` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `BoardID` INT NULL,
       PRIMARY KEY (`RegistryID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE  IF NOT EXISTS {databaseSchema}.{objectQualifier}ShoutboxMessage(
        ShoutBoxMessageID INT  NOT NULL AUTO_INCREMENT,		
        UserID int,
        UserName varchar(128)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
        UserDisplayName varchar(128)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
        Message LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation},
        `Date` datetime NOT NULL,
        `IP` varchar(39) NOT NULL,
        PRIMARY KEY (`ShoutBoxMessageID`)
        )
        ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Replace_Words
       (
       `ID` INT NOT NULL AUTO_INCREMENT,
       `BoardId` INT NOT NULL,
       `BadWord` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `GoodWord` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       PRIMARY KEY (`ID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Smiley
       (
       `SmileyID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `Code` VARCHAR(10) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Icon` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Emoticon` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `SortOrder` TINYINT(3) NOT NULL DEFAULT 0,
       PRIMARY KEY (`SmileyID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Topic
       (
       `TopicID` INT NOT NULL AUTO_INCREMENT,
       `ForumID` INT NOT NULL,
       `UserID` INT NOT NULL,
       `UserName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `UserDisplayName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Posted` DATETIME NOT NULL,
       `Topic` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Status` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Styles` VARCHAR(255) NULL,
       `Description` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Views` INT NOT NULL,
       `Priority` SMALLINT(5) NOT NULL,
       `PollID` INT NULL,
       `TopicMovedID` INT NULL,
       `LastPosted` DATETIME NULL,
       `LastMessageID` INT NULL,
       `LastUserID` INT NULL,
       `LastUserName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `LastUserDisplayName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `NumPosts` INT NOT NULL,
       `Flags` INT NOT NULL DEFAULT 0,
       `AnswerMessageId` INT NULL, 
       `LastMessageFlags`	INT NOT NULL DEFAULT 22,
       `LinkDate` DATETIME,
	   `TopicImage` varchar(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,  
	   `TopicImageType` varchar(50) NULL,  
	   `TopicImageBin` LONGBLOB NULL,  
       PRIMARY KEY (`TopicID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}User
       (
       `UserID` INT NOT NULL AUTO_INCREMENT,
       `BoardID` INT NOT NULL,
       `ProviderUserKey` VARCHAR(64) NULL,
       `Name` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `DisplayName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Password` VARCHAR(32) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `Email` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Joined` DATETIME NOT NULL,
       `LastVisit` DATETIME NOT NULL,
       `IP` VARCHAR(39) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `NumPosts` INT NOT NULL,
       `TimeZone` INT NOT NULL,
       `UseSingleSignOn`  TINYINT(1) NOT NULL DEFAULT 0,
       `TextEditor` VARCHAR(50),
       `Avatar` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Signature` LONGTEXT CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `AvatarImage` LONGBLOB NULL,
       `AvatarImageType`	VARCHAR (50) NULL,
       `RankID` INT NOT NULL,
       `Suspended` DATETIME NULL,
       `LanguageFile` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `ThemeFile` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `OverrideDefaultThemes` TINYINT(1) NOT NULL DEFAULT 0,
       `PMNotification` TINYINT(1) NOT NULL DEFAULT 1,
       `NotificationType` INT DEFAULT 10,
       `DailyDigest` TINYINT(1) NOT NULL DEFAULT 0,
       `AutoWatchTopics` TINYINT(1) NOT NULL DEFAULT 0,
       `Culture` VARCHAR(10) NULL,
       `Flags` INT NOT NULL DEFAULT 0,
       `Points` INT NOT NULL DEFAULT 1,
       `IsFacebookUser` TINYINT(1) NOT NULL DEFAULT 0,
       `IsTwitterUser` TINYINT(1) NOT NULL DEFAULT 0,
       `UserStyle` VARCHAR(510) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `StyleFlags` INT NOT NULL DEFAULT 0,
       `IsUserStyle` TINYINT(1) NOT NULL DEFAULT 0,
       `IsGroupStyle` TINYINT(1) NOT NULL DEFAULT 0,
       `IsRankStyle` TINYINT(1) NOT NULL DEFAULT 0,
	   `CommonViewType` INT NOT NULL DEFAULT 0,
	   `PostsPerPage`   INT NOT NULL DEFAULT 10, 
	   `TopicsPerPage`   INT NOT NULL DEFAULT 20, 
       PRIMARY KEY (`UserID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}UserForum
       (
       `UserID` INT NOT NULL,
       `ForumID` INT NOT NULL,
       `AccessMaskID` INT NOT NULL,
       `Invited` DATETIME NOT NULL,
       `Accepted` TINYINT(1) NOT NULL,
       PRIMARY KEY (`UserID`, `ForumID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}UserGroup
       (
       `UserID` INT NOT NULL,
       `GroupID` INT NOT NULL,
       PRIMARY KEY (`UserID`, `GroupID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}UserMedal
       (
       `UserID` INT NOT NULL,
       `MedalID` INT NOT NULL,
       `Message` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL,
       `Hide` TINYINT(1) NOT NULL DEFAULT 0,
       `OnlyRibbon` TINYINT(1) NOT NULL DEFAULT 0,
       `SortOrder` TINYINT(3) UNSIGNED NOT NULL DEFAULT 255,
       `DateAwarded` DATETIME NOT NULL
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}IgnoreUser
       (
       `UserID` INT NOT NULL,
       `IgnoredUserID` INT NOT NULL,
       PRIMARY KEY (`UserID`, `IgnoredUserID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};	
--GO
    
CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}UserPMessage
      (
  `UserPMessageID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `PMessageID` int(11) NOT NULL,
  `Flags` int(11) NOT NULL DEFAULT '0',
  `IsReply` tinyint(1) NOT NULL DEFAULT '0',
  `IsRead` tinyint(1) NOT NULL,
  `IsInOutbox` tinyint(1) NOT NULL,
  `IsArchived` tinyint(1) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL,  
   PRIMARY KEY (`UserPMessageID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}WatchForum
       (
       `WatchForumID` INT NOT NULL AUTO_INCREMENT,
       `ForumID` INT NOT NULL,
       `UserID` INT NOT NULL,
       `Created` DATETIME NOT NULL,
       `LastMail` DATETIME NULL,
       PRIMARY KEY (`WatchForumID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}WatchTopic
       (
       `WatchTopicID` INT NOT NULL AUTO_INCREMENT,
       `TopicID` INT NOT NULL,
       `UserID` INT NOT NULL,
       `Created` DATETIME NOT NULL,
       `LastMail` DATETIME NULL,
       PRIMARY KEY (`WatchTopicID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Thanks
       (
       `ThanksID` INT NOT NULL AUTO_INCREMENT,
       `ThanksFromUserID` INT NOT NULL,
       `ThanksToUserID` INT NOT NULL,
       `MessageID` INT NOT NULL,
       `ThanksDate` DATETIME NOT NULL,
       PRIMARY KEY (`ThanksID`)
       ) ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}Buddy
       (
       `ID` INT NOT NULL AUTO_INCREMENT,
       `FromUserID` INT NOT NULL,
       `ToUserID` INT NOT NULL,
       `Approved` TINYINT(1) NOT NULL,
       `Requested` DATETIME NOT NULL,
       PRIMARY KEY (`ID`)
       ) 
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

/* YAF FavoriteTopic Table */
CREATE TABLE IF NOT EXISTS {databaseSchema}.{objectQualifier}FavoriteTopic
       (
       `ID` INT NOT NULL AUTO_INCREMENT,
       `UserID` INT NOT NULL,
       `TopicID` INT NOT NULL,
       PRIMARY KEY (`ID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}UserAlbum
       (
       `AlbumID` INT NOT NULL  AUTO_INCREMENT,
       `UserID` INT NOT NULL,
       `Title` VARCHAR(255),
       `CoverImageID` INT,
       `Updated` DATETIME NOT NULL,
       PRIMARY KEY (`AlbumID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}UserAlbumImage
       (
       `ImageID` INT NOT NULL  AUTO_INCREMENT,
       `AlbumID`  INT NOT NULL,
       `Caption` VARCHAR(255),
       `FileName` VARCHAR(255) NOT NULL,
       `Bytes` INT NOT NULL,
       `ContentType`  VARCHAR(50),
       `Uploaded` DATETIME NOT NULL,
       `Downloads` INT NOT NULL,
       PRIMARY KEY (`ImageID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}TopicReadTracking
       (
       `UserID`			INT NOT NULL ,
       `TopicID`			INT NOT NULL ,
       `LastAccessDate`	DATETIME NOT NULL
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}ForumReadTracking
       (
       `UserID`			INT NOT NULL ,
       `ForumID`			INT NOT NULL ,
       `LastAccessDate`	DATETIME NOT NULL
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}TopicStatus
       (
       `TopicStatusID` INT NOT NULL AUTO_INCREMENT,
       `TopicStatusName` VARCHAR(100)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `BoardID` int NOT NULL,
       `DefaultDescription` VARCHAR(100)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       PRIMARY KEY (`TopicStatusID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}Tags
       (
       `TagID` INT NOT NULL AUTO_INCREMENT,
       `Tag` VARCHAR(1024)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL,
       `TagCount` INT NOT NULL DEFAULT 0,
        PRIMARY KEY (`TagID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}TopicTags
       (	 
       `TagID` int NOT NULL,
       `TopicID` int NOT NULL,
       PRIMARY KEY (`TagID`,`TopicID`)
       )
       ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}UserProfile
       (
       `UserID` INT NOT NULL,
       `LastUpdatedDate` DATETIME NOT NULL,
       -- added columns
       `LastActivity` DATETIME,
       `ApplicationName` VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation}  NOT NULL,
       `IsAnonymous` TINYINT(1) NOT NULL,
       `UserName` VARCHAR(255)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NOT NULL
       ) ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

CREATE TABLE IF NOT EXISTS  {databaseSchema}.{objectQualifier}ReputationVote(
        `ReputationFromUserID`  INT NOT NULL,
        `ReputationToUserID`	  INT NOT NULL,
        `VoteDate`	DATETIME NOT NULL
    ) ENGINE=InnoDB DEFAULT CHARSET={databaseEncoding} COLLATE {databaseCollation};
--GO

-- update procedures 

DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}tables_upgrade;
--GO;
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_migration;
--GO;
CREATE PROCEDURE {databaseSchema}.{objectQualifier}pollgroup_migration  
 (	 	
 )
 BEGIN
      -- local variables for temporary values
      DECLARE ici_ptmp INT;
      DECLARE ici_ttmp INT;
      DECLARE ici_utmp INT;
      DECLARE ici_PollGroupID INT;
      DECLARE cltt CURSOR FOR
      SELECT  PollID, TopicID, UserID 
       FROM {databaseSchema}.{objectQualifier}Topic 
        WHERE PollID IS NOT NULL;
    
        OPEN cltt;				
        
        BEGIN	
          DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
        LOOP        
            FETCH cltt INTO ici_ptmp, ici_ttmp, ici_utmp;       
             IF ici_ptmp is not null THEN
               insert into {databaseSchema}.{objectQualifier}PollGroupCluster(UserID, Flags) 
                values (ici_utmp, 0);	
               SET ici_PollGroupID = LAST_INSERT_ID(); 
               update {databaseSchema}.{objectQualifier}Topic SET PollID = ici_PollGroupID WHERE TopicID = ici_ttmp;
               update {databaseSchema}.{objectQualifier}Poll SET UserID = ici_utmp, PollGroupID = ici_PollGroupID, Flags = 0 WHERE PollID = ici_ptmp;
            END IF; 
        END LOOP;
        END;

        CLOSE cltt;		
 END;
--GO


CREATE  PROCEDURE {databaseSchema}.{objectQualifier}tables_upgrade()
BEGIN
-- clean-up for active tables
DELETE FROM {databaseSchema}.{objectQualifier}Active;
DELETE FROM {databaseSchema}.{objectQualifier}ActiveAccess;

-- Active Table

  -- add `ForumPage` column  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
                 WHERE TABLE_SCHEMA='{databaseSchema}' 
                 AND (TABLE_NAME='{objectQualifier}Active' 
                      OR TABLE_NAME=LOWER('{objectQualifier}Active'))
                 AND COLUMN_NAME='ForumPage' LIMIT 1) THEN
     ALTER TABLE  {databaseSchema}.{objectQualifier}Active ADD `ForumPage` VARCHAR(1024) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} AFTER `Location`;
  END IF;
  
  -- Check for a right column `ForumPage` size
  IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
             WHERE TABLE_SCHEMA='{databaseSchema}'  
             AND (TABLE_NAME='{objectQualifier}Active' 
                  OR TABLE_NAME=LOWER('{objectQualifier}Active'))
             AND COLUMN_NAME='ForumPage' AND CHARACTER_MAXIMUM_LENGTH < 255 LIMIT 1) THEN
     ALTER TABLE  {databaseSchema}.{objectQualifier}Active CHANGE `ForumPage` `ForumPage` VARCHAR(1024) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;
  
  -- Check for a right column `Location` size
  IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
             WHERE TABLE_SCHEMA='{databaseSchema}' 
             AND (TABLE_NAME='{objectQualifier}Active' 
                  OR TABLE_NAME=LOWER('{objectQualifier}Active'))
             AND COLUMN_NAME='Location' AND CHARACTER_MAXIMUM_LENGTH < 255 LIMIT 1) THEN
     ALTER TABLE  {databaseSchema}.{objectQualifier}Active CHANGE `Location` `Location` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;  

  -- add `Flags` column  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Active' OR TABLE_NAME=LOWER('{objectQualifier}Active'))
  AND COLUMN_NAME='Flags' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Active ADD `Flags` INT;
  END IF;

-- Active Access Table
   -- add `IsGuestX` column  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}ActiveAccess' OR TABLE_NAME=LOWER('{objectQualifier}ActiveAccess'))
  AND COLUMN_NAME='IsGuestX' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}ActiveAccess ADD `IsGuestX` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

     -- add `LastActive` column  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}ActiveAccess' OR TABLE_NAME=LOWER('{objectQualifier}ActiveAccess'))
  AND COLUMN_NAME='LastActive' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}ActiveAccess ADD `LastActive`  DATETIME NOT NULL;
  END IF;

 
  
-- AccessMask Table
IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}AccessMask' OR TABLE_NAME=LOWER('{objectQualifier}AccessMask'))
  AND COLUMN_NAME='SortOrder' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}AccessMask ADD `SortOrder`  INT NOT NULL DEFAULT 0;
  END IF;  
-- Board Table
IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Board' OR TABLE_NAME=LOWER('{objectQualifier}Board'))
  AND COLUMN_NAME='BoardUID' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Board ADD `BoardUID`  BINARY(16);
  END IF;  
-- Topic Table
IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='AnswerMessageId' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Topic ADD `AnswerMessageId` INT AFTER `Flags`;
  END IF;
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='Description' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Topic ADD  `Description` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL AFTER `Topic`;
  END IF;

   IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='Styles' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Topic ADD  `Styles` VARCHAR(255) NULL AFTER `Status`;
  END IF;

  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='Status' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Topic ADD  `Status` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL AFTER `Topic`;
  END IF;

  IF  EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='Status' LIMIT 1) THEN
  ALTER TABLE {databaseSchema}.{objectQualifier}Topic CHANGE `Status` `Status`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;  
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='LinkDate' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Topic ADD  `LinkDate` DATETIME;
  END IF;

  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='UserDisplayName' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Topic ADD `UserDisplayName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL AFTER `UserName`;
  END IF;

  IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='LinkDays' LIMIT 1) THEN
  UPDATE `{databaseSchema}`.`{objectQualifier}Topic` SET `LinkDate` =  `LinkDays` where TopicMovedID IS NOT NULL and `LinkDays` IS NOT NULL;
  ALTER TABLE  {databaseSchema}.{objectQualifier}Topic DROP COLUMN `LinkDays`;
  END IF;
    
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Topic' OR TABLE_NAME=LOWER('{objectQualifier}Topic')) 
  AND COLUMN_NAME='LastUserDisplayName' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Topic ADD `LastUserDisplayName` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL AFTER `LastUserName`;
  END IF;
 
-- Rank Table
IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='PMLimit' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `PMLimit` INT;
  END IF;
  
 IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='Style' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `Style` VARCHAR(255);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='SortOrder' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `SortOrder`  INT NOT NULL DEFAULT 0;
  END IF; 
  UPDATE  {databaseSchema}.{objectQualifier}Rank SET PMLimit = 0 WHERE PMLimit IS NULL;
  
   IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='Description' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `Description`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='UsrSigChars' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `UsrSigChars`  INT NOT NULL DEFAULT 0;
  END IF;  
 
 
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='UsrSigBBCodes' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `UsrSigBBCodes`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='UsrSigHTMLTags' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `UsrSigHTMLTags`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='UsrAlbums' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `UsrAlbums`  INT NOT NULL DEFAULT 0;
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Rank' OR TABLE_NAME=LOWER('{objectQualifier}Rank'))
  AND COLUMN_NAME='UsrAlbumImages' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Rank ADD `UsrAlbumImages`  INT NOT NULL DEFAULT 0;
  END IF;  
  
  -- Group Table
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='PMLimit' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `PMLimit` INT;
  END IF;
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='Style' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `Style`  VARCHAR(255);
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='SortOrder' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `SortOrder` INT NOT NULL DEFAULT 0;
  END IF;
  UPDATE  {databaseSchema}.{objectQualifier}Group SET PMLimit = 30 WHERE PMLimit IS NULL;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='Description' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `Description`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='UsrSigChars' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `UsrSigChars`  INT NOT NULL DEFAULT 0;
  END IF;
  
  UPDATE  {databaseSchema}.{objectQualifier}Group SET UsrSigChars = 128 WHERE UsrSigChars IS NULL;
 
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='UsrSigBBCodes' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `UsrSigBBCodes`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='UsrSigHTMLTags' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `UsrSigHTMLTags`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='UsrAlbums' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `UsrAlbums`  INT NOT NULL DEFAULT 0;
  END IF;
  
  UPDATE  {databaseSchema}.{objectQualifier}Group SET UsrAlbums = 1 WHERE UsrAlbums IS NULL AND Name !='Guest';
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  (TABLE_NAME='{objectQualifier}Group' OR TABLE_NAME=LOWER('{objectQualifier}Group'))
  AND COLUMN_NAME='UsrAlbumImages' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Group ADD `UsrAlbumImages`  INT NOT NULL DEFAULT 0;
  END IF;  
   UPDATE  {databaseSchema}.{objectQualifier}Group SET UsrAlbumImages = 30 WHERE UsrAlbumImages IS NULL AND Name !='Guest';


  -- MessageReportedAudit Table
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}MessageReportedAudit'
  AND COLUMN_NAME='ReportedNumber' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}MessageReportedAudit ADD `ReportedNumber` INT NOT NULL DEFAULT 1;
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}MessageReportedAudit'
  AND COLUMN_NAME='ReportText' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}MessageReportedAudit ADD `ReportText` VARCHAR(4000);
  END IF;

  -- User Table
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='DisplayName' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}User ADD `DisplayName`  VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} AFTER `Name`;
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='AutoWatchTopics' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}User ADD `AutoWatchTopics` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='NotificationType' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}User ADD `NotificationType` INT NOT NULL DEFAULT 10;
  END IF; 
   
       IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='TextEditor' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}User ADD  `TextEditor` VARCHAR(50);
  END IF; 
 
    
    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='DailyDigest' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}User ADD `DailyDigest` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;
  
     IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='Culture' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}User ADD `Culture` VARCHAR(10) NULL;
  END IF;

  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='UseSingleSignOn' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `UseSingleSignOn`  TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='IsFacebookUser' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `IsFacebookUser` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;  

    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='IsTwitterUser' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `IsTwitterUser` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

        IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='UserStyle' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `UserStyle` VARCHAR(510) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='StyleFlags' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `StyleFlags` INT NOT NULL DEFAULT 0;
  END IF;

        IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='IsUserStyle' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `IsUserStyle` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

          IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='IsGroupStyle' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `IsGroupStyle` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

            IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='IsRankStyle' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `IsRankStyle` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;   

              IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='IsRankStyle' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `IsRankStyle` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;  

                IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='CommonViewType' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `CommonViewType` INT NOT NULL DEFAULT 0;
  END IF;  

                  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='PostsPerPage' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD `PostsPerPage`   INT NOT NULL DEFAULT 10;
  END IF;  

                    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}User'
  AND COLUMN_NAME='TopicsPerPage' LIMIT 1) THEN
        ALTER TABLE   {databaseSchema}.{objectQualifier}User ADD  `TopicsPerPage`   INT NOT NULL DEFAULT 20;
  END IF;   

  -- Message Table
      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Message'
  AND COLUMN_NAME='EditedBy' LIMIT 1) THEN
         ALTER TABLE  {databaseSchema}.{objectQualifier}Message ADD `EditedBy`  INT  AFTER `Edited`;
  END IF; 

       IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Message'
  AND COLUMN_NAME='ExternalMessageId' LIMIT 1) THEN
         ALTER TABLE  {databaseSchema}.{objectQualifier}Message ADD  `ExternalMessageId`	varchar(255) NULL;
  END IF; 

         IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Message'
  AND COLUMN_NAME='ReferenceMessageId' LIMIT 1) THEN
         ALTER TABLE  {databaseSchema}.{objectQualifier}Message ADD  `ReferenceMessageId`	varchar(255) NULL;
  END IF; 

           IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Message'
  AND COLUMN_NAME='UserDisplayName' LIMIT 1) THEN
         ALTER TABLE  {databaseSchema}.{objectQualifier}Message ADD  `UserDisplayName`	varchar(255)  CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL AFTER  `UserName`;
  END IF; 

             IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Message'
  AND COLUMN_NAME='Description' LIMIT 1) THEN
         ALTER TABLE  {databaseSchema}.{objectQualifier}Message ADD   `Description` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} AFTER `Message`;
  END IF; 

  -- Forum Table
      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='ImageURL' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Forum ADD `ImageURL`  VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='Styles' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Forum ADD `Styles`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF; 

    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='PollGroupID' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Forum ADD `PollGroupID`  INT NULL;
  END IF; 

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='LastUserDisplayName' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Forum ADD `LastUserDisplayName`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL AFTER `LastUserName`;
  END IF; 

  -- PollVote Table
   IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}PollVote'
  AND COLUMN_NAME='ChoiceID' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}PollVote ADD `ChoiceID`  INT NULL;
  END IF;   

  -- Category Table
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Category'
  AND COLUMN_NAME='PollGroupID' LIMIT 1) THEN
  ALTER TABLE  {databaseSchema}.{objectQualifier}Category ADD `PollGroupID`  INT NULL;
  END IF;    
   
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Category'
  AND COLUMN_NAME='CanHavePersForums' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Category ADD `CanHavePersForums`  TINYINT(1) NOT NULL DEFAULT 0;
  END IF;
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}BannedIP'
  AND COLUMN_NAME='Reason' LIMIT 1) THEN
         ALTER TABLE  {databaseSchema}.{objectQualifier}BannedIP ADD `Reason`  VARCHAR(128) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}BannedIP'
  AND COLUMN_NAME='UserID' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}BannedIP ADD `UserID`   INT NOT NULL;
  END IF;

  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}ShoutboxMessage'
  AND COLUMN_NAME='BoardID' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}ShoutboxMessage ADD `BoardID`   INT NOT NULL DEFAULT 1;
  END IF;

    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Topic'
  AND COLUMN_NAME='LastMessageFlags' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}Topic ADD `LastMessageFlags`   INT NOT NULL DEFAULT 22;
  END IF;

  -- Poll Table
  IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Poll'
  AND COLUMN_NAME='Question' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}Poll MODIFY `Question`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;

    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Poll'
  AND COLUMN_NAME='PollGroupID' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}Poll ADD `PollGroupID`  INT NULL;
  END IF;

    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Poll'
  AND COLUMN_NAME='PollGroupID' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}Poll ADD `UserID`  INT NOT NULL DEFAULT 1;
  END IF;

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Poll'
  AND COLUMN_NAME='PollGroupID' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}Poll ADD `UserID`  INT NOT NULL DEFAULT 1;
  END IF;

        IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Poll'
  AND COLUMN_NAME='Flags' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}Poll ADD `Flags`  INT NOT NULL DEFAULT 0;
  END IF;

          IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Poll'
  AND COLUMN_NAME='ObjectPath' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}Poll ADD `ObjectPath`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

          IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Poll'
  AND COLUMN_NAME='MimeType' LIMIT 1) THEN
        ALTER TABLE  {databaseSchema}.{objectQualifier}Poll ADD `MimeType`  VARCHAR(50) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

  CALL {databaseSchema}.{objectQualifier}pollgroup_migration();

  IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Poll'
  AND COLUMN_NAME='Flags' LIMIT 1) THEN
  update {databaseSchema}.{objectQualifier}Poll set Flags = 0 where Flags is null;

  END IF;

-- Choice Table
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Choice'
  AND COLUMN_NAME='ObjectPath' LIMIT 1) THEN
    alter table {databaseSchema}.{objectQualifier}Choice ADD `ObjectPath`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  
    END IF;

  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Choice'
  AND COLUMN_NAME='MimeType' LIMIT 1) THEN
    alter table {databaseSchema}.{objectQualifier}Choice ADD `MimeType`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;



    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}NntpForum'
  AND COLUMN_NAME='DateCutOff' LIMIT 1) THEN
    alter table {databaseSchema}.{objectQualifier}NntpForum ADD `DateCutOff` DATETIME;
     END IF;  

-- ShoutboxMessage Table
  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}ShoutboxMessage'
  AND COLUMN_NAME='UserDisplayName' LIMIT 1) THEN
    alter table {databaseSchema}.{objectQualifier}ShoutboxMessage ADD `UserDisplayName`  VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL AFTER `UserName`;
  END IF;

 -- UserPMessage Table
         IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}UserPMessage'
  AND COLUMN_NAME='IsRead' LIMIT 1) THEN
    ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage ADD `IsRead` TINYINT(1) NULL;
    UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
    SET IsRead = IFNULL(SIGN(Flags & 1)>0,false) WHERE IsRead IS NULL;
    ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage CHANGE `IsRead` `IsRead`  TINYINT(1) NOT NULL;
     END IF; 
     
             IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}UserPMessage'
  AND COLUMN_NAME='IsInOutbox' LIMIT 1) THEN
    ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage ADD `IsInOutbox` TINYINT(1) NULL;
    UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
    SET IsInOutbox = IFNULL(SIGN(Flags & 2)>0,false) WHERE IsInOutbox IS NULL;
    ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage 
    CHANGE `IsInOutbox` `IsInOutbox`  TINYINT(1) NOT NULL;
     END IF;  

        IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}UserPMessage'
  AND COLUMN_NAME='IsArchived' LIMIT 1) THEN
    ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage ADD `IsArchived` TINYINT(1) NULL;
    UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
    SET IsArchived = IFNULL(SIGN(Flags & 4)>0,false) WHERE IsArchived IS NULL;
    ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage CHANGE `IsArchived` `IsArchived`  TINYINT(1) NOT NULL;
     END IF;
     
        IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}UserPMessage'
  AND COLUMN_NAME='IsDeleted' LIMIT 1) THEN
    ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage ADD `IsDeleted` TINYINT(1) NULL;
    UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
    SET IsDeleted = IFNULL(SIGN(Flags & 8)>0,false) WHERE IsDeleted IS NULL;
    ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage CHANGE `IsDeleted` `IsDeleted`  TINYINT(1) NOT NULL;
     END IF;  
  
    -- drop pk `MessageHistoryID`
IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}MessageHistory'
  AND COLUMN_NAME='MessageHistoryID' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}MessageHistory CHANGE `MessageHistoryID` `MessageHistoryID` BINARY(16) NOT NULL;
 ALTER TABLE {databaseSchema}.{objectQualifier}MessageHistory DROP PRIMARY KEY;
 ALTER TABLE {databaseSchema}.{objectQualifier}MessageHistory DROP `MessageHistoryID`;
END IF;

    -- drop pk `TrackingID`
IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}TopicReadtracking'
  AND COLUMN_NAME='TrackingID' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}TopicReadtracking CHANGE `TrackingID` `TrackingID` BINARY(16) NOT NULL;
 ALTER TABLE {databaseSchema}.{objectQualifier}TopicReadtracking DROP PRIMARY KEY;
 ALTER TABLE {databaseSchema}.{objectQualifier}TopicReadtracking DROP `TrackingID`;
  END IF;

  IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}PollVoteRefuse'
  AND COLUMN_NAME='BoardID' LIMIT 1) THEN 
   ALTER TABLE {databaseSchema}.{objectQualifier}PollVoteRefuse DROP `BoardID`;
  END IF;


    -- drop pk `TrackingID`
IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}ForumReadtracking'
  AND COLUMN_NAME='TrackingID' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}ForumReadtracking CHANGE `TrackingID` `TrackingID` BINARY(16) NOT NULL;
 ALTER TABLE {databaseSchema}.{objectQualifier}ForumReadtracking DROP PRIMARY KEY;
 ALTER TABLE {databaseSchema}.{objectQualifier}ForumReadtracking DROP `TrackingID`;
  END IF; 
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}ActiveAccess_UserID_Forum' AND S.TABLE_NAME=LOWER('{objectQualifier}ActiveAccess') AND (S.COLUMN_NAME='UserID' OR  S.COLUMN_NAME='ForumID') LIMIT 1) THEN

ALTER TABLE {databaseSchema}.{objectQualifier}ActiveAccess
DROP INDEX  `IX_{databaseSchema}_{objectQualifier}ActiveAccess_UserID_Forum`;

END IF;

IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}MessageReportedAudit'
  AND COLUMN_NAME='MessageID' AND IS_NULLABLE='YES' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}MessageReportedAudit CHANGE `MessageID` `MessageID` INT NOT NULL;
  END IF; 

  IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}MessageReportedAudit'
  AND COLUMN_NAME='UserID' AND IS_NULLABLE='YES' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}MessageReportedAudit CHANGE `UserID` `UserID` INT NOT NULL;
  END IF; 

    IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}MessageReportedAudit'
  AND COLUMN_NAME='Reported' AND IS_NULLABLE='YES' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}MessageReportedAudit CHANGE `Reported` `Reported` DATETIME NOT NULL;
  END IF; 

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}PMessage'
  AND COLUMN_NAME='ReplyTo' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}PMessage ADD `ReplyTo`  INT;
  END IF; 

   IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}UserPMessage'
  AND COLUMN_NAME='IsReply' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}UserPMessage ADD `IsReply`  TINYINT(1) NOT NULL DEFAULT 0;
  END IF; 

     IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Topic'
  AND COLUMN_NAME='LinkDate' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Topic ADD `LinkDate`  DATETIME;
  END IF;
    
     IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Topic'
  AND COLUMN_NAME='TopicImage' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Topic ADD `TopicImage` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

       IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Topic'
  AND COLUMN_NAME='TopicImageType' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Topic ADD `TopicImageType` varchar(50) NULL;
  END IF;

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Topic'
  AND COLUMN_NAME='TopicImageBin' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Topic ADD `TopicImageBin` LONGBLOB NULL;
  END IF;
 
  -- Group Table

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='CreatedByUserID' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD `CreatedByUserID` INT;
  END IF;

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='CreatedByUserName' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD `CreatedByUserName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

        IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='CreatedByUserDisplayName' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD `CreatedByUserDisplayName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

     IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='CreatedDate' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD `CreatedDate` DATETIME;
  END IF;

  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='IsUserGroup' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD `IsUserGroup` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;  
 
   IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='IsHidden' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD `IsHidden` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='UsrPersonalForums' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD  `UsrPersonalForums`  INT NOT NULL DEFAULT 0;
  END IF;  

     IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='UsrPersonalMasks' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD  `UsrPersonalMasks`  INT NOT NULL DEFAULT 0;
  END IF; 

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='UsrPersonalGroups' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Group ADD  `UsrPersonalGroups`  INT NOT NULL DEFAULT 0;
  END IF; 
  -- Forum Table

    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='CreatedByUserID' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Forum ADD `CreatedByUserID` INT;
  END IF;

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='CreatedByUserName' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Forum ADD `CreatedByUserName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

        IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='CreatedByUserDisplayName' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Forum ADD `CreatedByUserDisplayName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

     IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='CreatedDate' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Forum ADD `CreatedDate` DATETIME;
  END IF;

       IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='IsUserForum' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Forum ADD `IsUserForum`  TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

         IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='CanHavePersForums' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}Forum ADD `CanHavePersForums`  TINYINT(1) NOT NULL DEFAULT 0;
  END IF;
  
  -- make Description column nullable
    IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='Description' AND COLUMN_DEFAULT IS NOT NULL LIMIT 1) THEN
  ALTER TABLE {databaseSchema}.{objectQualifier}Forum ALTER `Description` DROP DEFAULT; 
  END IF;

      IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='Description' AND IS_NULLABLE='NO' LIMIT 1) THEN
  ALTER TABLE {databaseSchema}.{objectQualifier}Forum MODIFY `Description` VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation};
  END IF;

  -- set sortorder of forum table to integer

  IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Forum'
  AND COLUMN_NAME='SortOrder' AND DATA_TYPE = 'smallint' LIMIT 1) THEN
  ALTER TABLE {databaseSchema}.{objectQualifier}Forum MODIFY `SortOrder` INT;
  END IF;

    IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}AccessMask'
  AND COLUMN_NAME='SortOrder' AND DATA_TYPE = 'smallint' LIMIT 1) THEN
  ALTER TABLE {databaseSchema}.{objectQualifier}AccessMask MODIFY `SortOrder` INT;
  END IF; 
      
  -- AccessMask Table

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}AccessMask'
  AND COLUMN_NAME='CreatedByUserID' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}AccessMask ADD `CreatedByUserID` INT;
  END IF;

      IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}AccessMask'
  AND COLUMN_NAME='CreatedByUserName' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}AccessMask ADD `CreatedByUserName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

        IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}AccessMask'
  AND COLUMN_NAME='CreatedByUserDisplayName' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}AccessMask ADD `CreatedByUserDisplayName`   VARCHAR(255) CHARACTER SET {databaseEncoding} COLLATE {databaseCollation} NULL;
  END IF;

     IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}AccessMask'
  AND COLUMN_NAME='CreatedDate' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}AccessMask ADD `CreatedDate` DATETIME;
  END IF;

       IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}AccessMask'
  AND COLUMN_NAME='IsUserMask' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}AccessMask ADD `IsUserMask`  TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

         IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}AccessMask'
  AND COLUMN_NAME='IsAdminMask' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}AccessMask ADD `IsAdminMask`  TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

         IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}ActiveAccess'
  AND COLUMN_NAME='UserForumAccess' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}ActiveAccess ADD `UserForumAccess` TINYINT(1) NOT NULL DEFAULT 0;
  END IF;

  IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}UserProfile'
  AND COLUMN_NAME='Birthday' LIMIT 1) THEN
 ALTER TABLE {databaseSchema}.{objectQualifier}UserProfile ADD `Birthday` DATETIME;
  END IF;   
  
    IF EXISTS (SELECT 1 FROM information_schema.COLUMNS 
  WHERE TABLE_SCHEMA='{databaseSchema}'  AND
  TABLE_NAME='{objectQualifier}Group'
  AND COLUMN_NAME='SortOrder' AND DATA_TYPE = 'smallint' LIMIT 1) THEN
  ALTER TABLE {databaseSchema}.{objectQualifier}Group MODIFY `SortOrder` INT;
  END IF;



  END;
--GO

CALL {databaseSchema}.{objectQualifier}tables_upgrade();
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}tables_upgrade;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_migration;	
--GO

/* Check and update table column sizes */

DROP PROCEDURE   IF EXISTS {databaseSchema}.{objectQualifier}change_table_columns;
--GO
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}change_table_columns()
        BEGIN				
             ALTER TABLE  {databaseSchema}.{objectQualifier}PollVote CHANGE `RemoteIP` `RemoteIP` VARCHAR(39);
             ALTER TABLE  {databaseSchema}.{objectQualifier}BannedIP CHANGE `Mask` `Mask` VARCHAR(57);
             ALTER TABLE  {databaseSchema}.{objectQualifier}Active CHANGE `Location` `Location` VARCHAR(255);
             ALTER TABLE  {databaseSchema}.{objectQualifier}User CHANGE `Culture` `Culture` VARCHAR(10);
             ALTER TABLE  {databaseSchema}.{objectQualifier}AccessMask CHANGE `SortOrder` `SortOrder` INT;
             ALTER TABLE  {databaseSchema}.{objectQualifier}Group CHANGE `SortOrder` `SortOrder` INT;
             ALTER TABLE  {databaseSchema}.{objectQualifier}Forum CHANGE `SortOrder` `SortOrder` INT;
        END;
--GO
CALL {databaseSchema}.{objectQualifier}change_table_columns();
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}change_table_columns;
--GO
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_initdisplayname() 

BEGIN

    declare ici_tmp int;
    declare ici_tmpUserID int;
    declare ici_tmpLastUserID int;
        
        declare fc cursor for
        select ForumID, LastUserID from {databaseSchema}.{objectQualifier}Forum
        where LastUserDisplayName IS NULL;

        declare sbc cursor for
        select ShoutBoxMessageID,UserID from {databaseSchema}.{objectQualifier}ShoutboxMessage
        where UserDisplayName IS NULL;

        declare mc cursor for
        select MessageID,UserID from {databaseSchema}.{objectQualifier}Message
        where UserDisplayName IS NULL;

        declare tc cursor for
        select TopicID,UserID,LastUserID from {databaseSchema}.{objectQualifier}Topic
        where UserDisplayName IS NULL OR LastUserDisplayName IS NULL;

        open fc;

        BEGIN	
        DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
        LOOP        
        fetch fc into ici_tmp,ici_tmpLastUserID;		
        update {databaseSchema}.{objectQualifier}Forum set LastUserDisplayName = (select u.DisplayName FROM {databaseSchema}.{objectQualifier}User u WHERE u.UserID = ici_tmpLastUserID) where {databaseSchema}.{objectQualifier}Forum.ForumID = ici_tmp; 	
        END LOOP;
        END;
        close fc;		
        
            
        open sbc;
        BEGIN	
        DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
        LOOP 
        fetch sbc into ici_tmp,ici_tmpUserID;		
        update {databaseSchema}.{objectQualifier}ShoutboxMessage 
        set UserDisplayName = (select u.DisplayName FROM {databaseSchema}.{objectQualifier}User u where u.UserID = ici_tmpUserID) where {databaseSchema}.{objectQualifier}ShoutboxMessage.ShoutBoxMessageID = ici_tmp;
        END LOOP;
        END;
        close sbc;		
                
        open mc;
        BEGIN	
        DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
        LOOP 
        fetch  mc into ici_tmp,ici_tmpUserID;				
        update {databaseSchema}.{objectQualifier}Message  set UserDisplayName = (select u.DisplayName FROM {databaseSchema}.{objectQualifier}User u  WHERE u.UserID = ici_tmpUserID) where MessageID = ici_tmp;
         END LOOP;
        END;		
        close mc;
                
        open tc;
            BEGIN	
        DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;         
        LOOP 
        fetch next from tc into ici_tmp,ici_tmpUserID,ici_tmpLastUserID;	    
        update {databaseSchema}.{objectQualifier}Topic set UserDisplayName = (select u.DisplayName FROM {databaseSchema}.{objectQualifier}User u  WHERE u.UserID = ici_tmpUserID) where TopicID = ici_tmp;
        update {databaseSchema}.{objectQualifier}Topic set LastUserDisplayName = (select u.DisplayName FROM {databaseSchema}.{objectQualifier}User u WHERE u.UserID = ici_tmpLastUserID) where TopicID = ici_tmp;			
         END LOOP;
        END;
        close tc;
            
end;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}tmp_initdisplayname()
BEGIN
if exists (select 1 from {databaseSchema}.{objectQualifier}Message where UserDisplayName IS NULL limit 1) then
CALL {databaseSchema}.{objectQualifier}forum_initdisplayname();
end if;
END;
--GO
  DROP PROCEDURE  IF EXISTS {databaseSchema}.{objectQualifier}tmp_initdisplayname;
  --GO 
    DROP PROCEDURE  IF EXISTS {databaseSchema}.{objectQualifier}forum_initdisplayname;
  --GO 

  CREATE PROCEDURE {databaseSchema}.{objectQualifier}tmp_updatevalues()
BEGIN
UPDATE {databaseSchema}.{objectQualifier}Group SET Style = NULL where Style IS NOT NULL and CHAR_LENGTH(Style)<=2;
UPDATE {databaseSchema}.{objectQualifier}Rank SET Style = NULL where Style IS NOT NULL and CHAR_LENGTH(Style)<=2;
UPDATE {databaseSchema}.{objectQualifier}Rank SET Flags = Flags | 4 where Name LIKE 'Guest';
END;
--GO
CALL {databaseSchema}.{objectQualifier}tmp_updatevalues();
--GO
  DROP PROCEDURE  IF EXISTS {databaseSchema}.{objectQualifier}tmp_updatevalues;
  --GO 
