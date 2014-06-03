-- This scripts for PostgreSQL Yet Another Forum https://github.com/vzrus/YetAnotherForumExtraDataLayers http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team  https://github.com/vzrus
-- They are distributed under terms of GPLv2 licence only as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2009-2012

-- Primary keys

-- IF EXISTS (SELECT 1 FROM pg_indexes WHERE indexname='' AND tablename='' and schemaname='')
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}create_or_check_pkeys()
RETURNS void AS
$BODY$
BEGIN
-- drop
/* IF  EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}watchtopicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}watchtopic
	DROP CONSTRAINT pk_{databaseSchema}_{objectQualifier}watchtopicid;
END IF;

IF  EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}watchforumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}watchforum
	DROP CONSTRAINT pk_{databaseSchema}_{objectQualifier}watchforumid;
END IF; */

IF EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}logid_messagereportedaudit' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}messagereportedaudit
	DROP CONSTRAINT pk_{databaseSchema}_{objectQualifier}logid_messagereportedaudit CASCADE;
END IF;

-- create


IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}attachmentid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}attachment
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}attachmentid PRIMARY KEY (attachmentid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}bbcodeid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}bbcode
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}bbcodeid PRIMARY KEY (bbcodeid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}topicstatusid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topicstatus
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}topicstatusid PRIMARY KEY (topicstatusid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}board
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}boardid PRIMARY KEY (boardid);
END IF;    

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}categoryid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}category
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}categoryid PRIMARY KEY (categoryid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}checkemailid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}checkemail
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}checkemailid PRIMARY KEY (checkemailid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}choiceid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}choice
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}choiceid PRIMARY KEY (choiceid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}eventlogid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}eventlog
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}eventlogid PRIMARY KEY (eventlogid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}eventloggroupaccess_groupid_eventtypeid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}eventloggroupaccess
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}eventloggroupaccess_groupid_eventtypeid PRIMARY KEY (groupid,eventtypeid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}extensionid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}extension
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}extensionid PRIMARY KEY (extensionid);
END IF; 

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}forumid PRIMARY KEY (forumid);
END IF;


IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}groupid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}group
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}groupid PRIMARY KEY (groupid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}groupid_forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forumaccess
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}groupid_forumid PRIMARY KEY (groupid, forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}mailid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}mail
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}mailid PRIMARY KEY (mailid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}medalid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}medal
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}medalid PRIMARY KEY (medalid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}messageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}message
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}messageid PRIMARY KEY (messageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}nntpforumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}nntpforum
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}nntpforumid PRIMARY KEY (nntpforumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}nntpserverid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}nntpserver
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}nntpserverid PRIMARY KEY (nntpserverid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}accessmaskid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}accessmask
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}accessmaskid PRIMARY KEY (accessmaskid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}replace_words_id' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}replace_words
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}replace_words_id PRIMARY KEY (id);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}bannedip_id' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}bannedip
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}bannedip_id PRIMARY KEY (id);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}rankid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}rank
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}rankid PRIMARY KEY (rankid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}registryid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}registry
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}registryid PRIMARY KEY (registryid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}smileyid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}smiley
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}smileyid PRIMARY KEY (smileyid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}topicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topic
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}topicid PRIMARY KEY (topicid);
END IF;   

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}user
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid PRIMARY KEY (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}userid_forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}userforum
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid_forumid PRIMARY KEY (userid, forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}userid_groupid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}usergroup
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid_groupid PRIMARY KEY (userid, groupid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}userpmessageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}userpmessage
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userpmessageid PRIMARY KEY (userpmessageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}watchforumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}watchforum
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}watchforumid PRIMARY KEY (watchforumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}watchtopicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}watchtopic
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}watchtopicid PRIMARY KEY (watchtopicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}pmessageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}pmessage
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}pmessageid PRIMARY KEY (pmessageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}pollgroupid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}pollgroupcluster
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}pollgroupid PRIMARY KEY (pollgroupid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}pollid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}poll
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}pollid PRIMARY KEY (pollid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}pollvoteid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}pollvote
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}pollvoteid PRIMARY KEY (pollvoteid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}ignoreuser' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}ignoreuser
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}ignoreuser PRIMARY KEY (userid, ignoreduserid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}sessionid_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}active
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}sessionid_boardid PRIMARY KEY (sessionid, boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}nntptopicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}nntptopic
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}nntptopicid PRIMARY KEY (nntptopicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}shoutboxmessageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}shoutboxmessage
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}shoutboxmessageid PRIMARY KEY (shoutboxmessageid);
END IF;
 
-- Thanks table

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}thanksid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}thanks
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}thanksid PRIMARY KEY (thanksid);
END IF; 

-- Buddy table

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}buddyid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}buddy
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}buddyid PRIMARY KEY (id);
END IF; 

-- TopicActive table

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}favoritetopic_id' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}favoritetopic
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}favoritetopic_id PRIMARY KEY (id);
END IF; 
 
IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}useralbum_useralbumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}useralbum
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}useralbum_useralbumid PRIMARY KEY (albumid);
END IF; 

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}useralbumimage_imageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}useralbumimage
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}useralbumimage_imageid PRIMARY KEY (imageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}tags_tagid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}tags
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}tags_tagid PRIMARY KEY (tagid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint where contype='p' and conname ='pk_{databaseSchema}_{objectQualifier}topictags_tagidtopicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topictags
	ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}topictags_tagidtopicid PRIMARY KEY (tagid, topicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}userid_forumid_activeaccess' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}activeaccess
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid_forumid_activeaccess PRIMARY KEY (userid,forumid);
END IF;
IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}userid_applicationname_userprofile' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}userprofile
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid_applicationname_userprofile PRIMARY KEY (userid,applicationname);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}userid_medalid_usermedal' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}usermedal
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid_medalid_usermedal PRIMARY KEY (userid,medalid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}userid_forumid_forumreadtracking' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forumreadtracking
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid_forumid_forumreadtracking PRIMARY KEY (userid,forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}userid_topicid_topicreadtracking' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topicreadtracking
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid_topicid_topicreadtracking PRIMARY KEY (userid,topicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}medalid_groupid_groupmedal' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}groupmedal
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}medalid_groupid_groupmedal PRIMARY KEY (medalid,groupid);
END IF;


IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}messageid_edited_messagehistory' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}messagehistory
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}messageid_edited_messagehistory PRIMARY KEY (messageid,edited);
END IF;


IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}messageid_messagereported' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}messagereported
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}messageid_messagereported PRIMARY KEY (messageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}repfruid_reptouid_reputationvote' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}reputationvote
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}repfruid_reptouid_reputationvote PRIMARY KEY (reputationfromuserid,reputationtouserid);
END IF; 

IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}userid_pagename_adminpageuseraccess' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}adminpageuseraccess
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}userid_pagename_adminpageuseraccess PRIMARY KEY (userid,pagename);
END IF; 

IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}messageid_userid_reported_{objectQualifier}messagereportedaudit' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}messagereportedaudit
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}messageid_userid_reported_{objectQualifier}messagereportedaudit PRIMARY KEY (messageid,userid,reported);
END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;
	 GRANT EXECUTE ON FUNCTION {databaseSchema}.{objectQualifier}create_or_check_pkeys() TO public;
	--GO
	SELECT {databaseSchema}.{objectQualifier}create_or_check_pkeys();
	--GO
	DROP FUNCTION {databaseSchema}.{objectQualifier}create_or_check_pkeys();
--GO
