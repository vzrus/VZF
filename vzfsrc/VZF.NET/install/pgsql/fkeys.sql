-- This scripts for PostgreSQL Yet Another Forum https://github.com/vzrus/YetAnotherForumExtraDataLayers http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team  https://github.com/vzrus
-- They are distributed under terms of GPLv2 licence only as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2009-2012

-- Foregn keys

--{databaseSchema}.{objectQualifier}check_or_create_keys arguments
-- constraint_schema,constraint_table, key_table(''(for pkeys)|'table_name'(for fkeys),constraint_name,constraint_column,constraint_type('p'(primary key)|'f'(foreign key))	

-- SELECT {databaseSchema}.{objectQualifier}check_or_create_keys('{databaseSchema}','{objectQualifier}bbcode','{objectQualifier}board','fk_{databaseSchema}_{objectQualifier}_bbcode_{objectQualifier}_board_boardid','boardid','f');
-- GO

-- SELECT {databaseSchema}.{objectQualifier}check_or_create_keys('{databaseSchema}','{objectQualifier}accessmask','{objectQualifier}board','fk_{databaseSchema}_{objectQualifier}_accessmask_{objectQualifier}_board_boardid','boardid','f');
-- GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}create_or_check_fkeys()
RETURNS void AS
$BODY$
BEGIN

IF EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_poll_pollid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topic
    DROP CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_poll_pollid;
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_bbcode_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}bbcode
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_bbcode_{objectQualifier}_board_boardid 
    FOREIGN KEY (boardid) 
    REFERENCES {databaseSchema}.{objectQualifier}board(boardid) 
    ON DELETE NO ACTION;
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_accessmask_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}accessmask
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_accessmask_{objectQualifier}_board_boardid 
    FOREIGN KEY (boardid) 
    REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}active
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_board_boardid 
    FOREIGN KEY (boardid) 
    REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;    

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_forum_forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}active
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_forum_forumid 
    FOREIGN KEY (forumid) 
    REFERENCES {databaseSchema}.{objectQualifier}forum(forumid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_messagehistory_messageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}messagehistory
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_messagehistory_messageid 
    FOREIGN KEY (messageid) 
    REFERENCES {databaseSchema}.{objectQualifier}message(messageid) ON DELETE CASCADE;
END IF;  

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_topic_topicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}active
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_topic_topicid 
    FOREIGN KEY (topicid) 
    REFERENCES {databaseSchema}.{objectQualifier}topic(topicid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}active
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_user_userid 
    FOREIGN KEY (userid) 
    REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;   

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_attachment_{objectQualifier}_message_messageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}attachment
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_attachment_{objectQualifier}_message_messageid 
    FOREIGN KEY (messageid) 
    REFERENCES {databaseSchema}.{objectQualifier}message(messageid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_bannedip_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}bannedip
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_bannedip_{objectQualifier}_board_boardid 
    FOREIGN KEY (boardid) 
    REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_category_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}category
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_category_{objectQualifier}_board_boardid 
    FOREIGN KEY (boardid) 
    REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_checkemail_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}checkemail
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_checkemail_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_choice_{objectQualifier}_poll_pollid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}choice
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_choice_{objectQualifier}_poll_pollid FOREIGN KEY (pollid) REFERENCES {databaseSchema}.{objectQualifier}poll(pollid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_eventlog_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}eventlog
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_eventlog_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid) ON DELETE CASCADE;
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_extension_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}extension
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_extension_{objectQualifier}_board_boardid FOREIGN KEY (boardid) REFERENCES {databaseSchema}.{objectQualifier}board(boardid) ON DELETE CASCADE;
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forumaccess_{objectQualifier}_accessmask_accessmaskid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forumaccess
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forumaccess_{objectQualifier}_accessmask_accessmaskid FOREIGN KEY (accessmaskid) REFERENCES {databaseSchema}.{objectQualifier}accessmask(accessmaskid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forumaccess_{objectQualifier}_forum_forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forumaccess
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forumaccess_{objectQualifier}_forum_forumid FOREIGN KEY (forumid) REFERENCES {databaseSchema}.{objectQualifier}forum(forumid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forumaccess_{objectQualifier}_group_groupid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forumaccess
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forumaccess_{objectQualifier}_group_groupid FOREIGN KEY (groupid) REFERENCES {databaseSchema}.{objectQualifier}group(groupid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_category_categoryid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_category_categoryid FOREIGN KEY (categoryid) REFERENCES {databaseSchema}.{objectQualifier}category(categoryid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_forum_parentid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_forum_parentid FOREIGN KEY (parentid) REFERENCES {databaseSchema}.{objectQualifier}forum(forumid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_message_lastmessageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_message_lastmessageid FOREIGN KEY (lastmessageid) REFERENCES {databaseSchema}.{objectQualifier}message(messageid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_topic_lasttopicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_topic_lasttopicid FOREIGN KEY (lasttopicid) REFERENCES {databaseSchema}.{objectQualifier}topic(topicid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_user_lastuserid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_user_lastuserid FOREIGN KEY (lastuserid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_group_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}group
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_group_{objectQualifier}_board_boardid FOREIGN KEY (boardid) REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_message_replyto' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}message
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_message_replyto FOREIGN KEY (replyto) REFERENCES {databaseSchema}.{objectQualifier}message(messageid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_topic_topicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}message
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_topic_topicid FOREIGN KEY (topicid) REFERENCES {databaseSchema}.{objectQualifier}topic(topicid);
END IF; 

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}message
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF; 

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_nntpforum_{objectQualifier}_forum_forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}nntpforum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_nntpforum_{objectQualifier}_forum_forumid FOREIGN KEY (forumid) REFERENCES {databaseSchema}.{objectQualifier}forum(forumid);
END IF; 

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_nntpforum_{objectQualifier}_nntpserver_nntpserverid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}nntpforum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_nntpforum_{objectQualifier}_nntpserver_nntpserverid FOREIGN KEY (nntpserverid) REFERENCES {databaseSchema}.{objectQualifier}nntpserver(nntpserverid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_nntpserver_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}nntpserver
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_nntpserver_{objectQualifier}_board_boardid FOREIGN KEY (boardid) REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_nntptopic_{objectQualifier}_nntpforum_nntpforumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}nntptopic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_nntptopic_{objectQualifier}_nntpforum_nntpforumid FOREIGN KEY (nntpforumid) REFERENCES {databaseSchema}.{objectQualifier}nntpforum(nntpforumid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_nntptopic_{objectQualifier}_topic_topicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}nntptopic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_nntptopic_{objectQualifier}_topic_topicid FOREIGN KEY (topicid) REFERENCES {databaseSchema}.{objectQualifier}topic(topicid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_pmessage_{objectQualifier}_user1_fromuserid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}pmessage
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_pmessage_{objectQualifier}_user1_fromuserid FOREIGN KEY (fromuserid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_pollvote_{objectQualifier}_poll_pollid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}pollvote
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_pollvote_{objectQualifier}_poll_pollid FOREIGN KEY (pollid) REFERENCES {databaseSchema}.{objectQualifier}poll(pollid) ON DELETE CASCADE;
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_rank_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}rank
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_rank_{objectQualifier}_board_boardid FOREIGN KEY (boardid) REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_registry_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}registry
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_registry_{objectQualifier}_board_boardid FOREIGN KEY (boardid) REFERENCES {databaseSchema}.{objectQualifier}board(boardid) ON DELETE CASCADE;
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_smiley_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}smiley
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_smiley_{objectQualifier}_board_boardid FOREIGN KEY (boardid) REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_forum_forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_forum_forumid FOREIGN KEY (forumid) REFERENCES {databaseSchema}.{objectQualifier}forum(forumid) ON DELETE CASCADE;
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_message_lastmessageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_message_lastmessageid FOREIGN KEY (lastmessageid) REFERENCES {databaseSchema}.{objectQualifier}message(messageid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_topic_topicmovedid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_topic_topicmovedid FOREIGN KEY (topicmovedid) REFERENCES {databaseSchema}.{objectQualifier}topic(topicid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_user2_lastuserid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}topic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_user2_lastuserid FOREIGN KEY (lastuserid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_accessmask_accessmaskid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}userforum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_accessmask_accessmaskid FOREIGN KEY (accessmaskid) REFERENCES {databaseSchema}.{objectQualifier}accessmask(accessmaskid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_forum_forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}userforum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_forum_forumid FOREIGN KEY (forumid) REFERENCES {databaseSchema}.{objectQualifier}forum(forumid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}userforum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_usergroup_{objectQualifier}_group_groupid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}usergroup
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_usergroup_{objectQualifier}_group_groupid FOREIGN KEY (groupid) REFERENCES {databaseSchema}.{objectQualifier}group(groupid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_usergroup_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}usergroup
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_usergroup_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_userpmessage_{objectQualifier}_pmessage_pmessageid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}userpmessage
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_userpmessage_{objectQualifier}_pmessage_pmessageid FOREIGN KEY (pmessageid) REFERENCES {databaseSchema}.{objectQualifier}pmessage(pmessageid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_userpmessage_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}userpmessage
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_userpmessage_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}rank
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_board_boardid FOREIGN KEY (boardid) REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_board_boardid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}user
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_board_boardid FOREIGN KEY (boardid) REFERENCES {databaseSchema}.{objectQualifier}board(boardid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_rank_rankid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}user
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_rank_rankid FOREIGN KEY (rankid) REFERENCES {databaseSchema}.{objectQualifier}rank(rankid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_watchforum1_{objectQualifier}_forum_forumid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}watchforum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_watchforum1_{objectQualifier}_forum_forumid FOREIGN KEY (forumid) REFERENCES {databaseSchema}.{objectQualifier}forum(forumid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_watchforum1_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}watchforum
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_watchforum1_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_watchtopic_{objectQualifier}_topic_topicid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}watchtopic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_watchtopic_{objectQualifier}_topic_topicid FOREIGN KEY (topicid) REFERENCES {databaseSchema}.{objectQualifier}topic(topicid);
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_watchtopic_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE ONLY {databaseSchema}.{objectQualifier}watchtopic
    ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_watchtopic_{objectQualifier}_user_userid FOREIGN KEY (userid) REFERENCES {databaseSchema}.{objectQualifier}user(userid);
END IF;

-- Foreign Key: fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_message
IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_message_messageid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}topic
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_message_messageid FOREIGN KEY (lastmessageid)
      REFERENCES {databaseSchema}.{objectQualifier}message(messageid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION;
END IF;

-- Foreign Key: databaseSchema.fk_{databaseSchema}_{objectQualifier}_poll_{objectQualifier}_pollgroupcluster
IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_poll_{objectQualifier}_pollgroupcluster' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}poll
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_poll_{objectQualifier}_pollgroupcluster FOREIGN KEY (pollgroupid)
      REFERENCES {databaseSchema}.{objectQualifier}pollgroupcluster(pollgroupid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION;
END IF; 

-- Foreign Key: databaseSchema.fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_favorite
IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_favorite' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}topic
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_favorite FOREIGN KEY (topicid)
      REFERENCES {databaseSchema}.{objectQualifier}topic(topicid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION;
END IF; 

-- Foreign Key: fk_{databaseSchema}_{objectQualifier}_buddy_{objectQualifier}_user
IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_buddy_{objectQualifier}_user' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}buddy
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_buddy_{objectQualifier}_user FOREIGN KEY (fromuserid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION;
END IF;

-- Foreign Key: fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_pollgroupcluster
IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_pollgroupcluster' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}topic
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_pollgroupcluster FOREIGN KEY (pollid)
      REFERENCES {databaseSchema}.{objectQualifier}pollgroupcluster(pollgroupid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION;
END IF;

-- Foreign Key: fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_pollgroupcluster
IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_pollgroupcluster' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}forum
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_pollgroupcluster FOREIGN KEY (pollgroupid)
      REFERENCES {databaseSchema}.{objectQualifier}pollgroupcluster(pollgroupid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION;
END IF;

 -- Foreign Key: fk_{databaseSchema}_{objectQualifier}_category_{objectQualifier}_pollgroupcluster
IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_category_{objectQualifier}_pollgroupcluster' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}category
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_category_{objectQualifier}_pollgroupcluster FOREIGN KEY (pollgroupid)
      REFERENCES {databaseSchema}.{objectQualifier}pollgroupcluster(pollgroupid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION;
END IF;

 -- Foreign Key: fk_{databaseSchema}_{objectQualifier}_useralbum_{objectQualifier}_user
 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_useralbum_{objectQualifier}_user' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}useralbum
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_useralbum_{objectQualifier}_user FOREIGN KEY (userid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_activeaccess_{objectQualifier}_user' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}activeaccess
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_activeaccess_{objectQualifier}_user FOREIGN KEY (userid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_messagereportedaudit_{objectQualifier}_messagereported' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}messagereportedaudit
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_messagereportedaudit_{objectQualifier}_messagereported FOREIGN KEY (messageid)
      REFERENCES {databaseSchema}.{objectQualifier}messagereported(messageid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;
 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forumreadtracking_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}forumreadtracking
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forumreadtracking_{objectQualifier}_user_userid FOREIGN KEY (userid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forumreadtracking_{objectQualifier}_forum_forumid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}forumreadtracking
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forumreadtracking_{objectQualifier}_forum_forumid FOREIGN KEY (forumid)
      REFERENCES {databaseSchema}.{objectQualifier}forum(forumid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topicreadtracking_{objectQualifier}_user_userid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}topicreadtracking
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topicreadtracking_{objectQualifier}_user_userid FOREIGN KEY (userid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topicreadtracking_{objectQualifier}_topic_topicid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}topicreadtracking
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topicreadtracking_{objectQualifier}_topic_topicid FOREIGN KEY (topicid)
      REFERENCES {databaseSchema}.{objectQualifier}topic(topicid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_reputationvote_{objectQualifier}_user_to' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}reputationvote
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_reputationvote_{objectQualifier}_user_to FOREIGN KEY (reputationtouserid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_reputationvote_{objectQualifier}_user_from' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}reputationvote
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_reputationvote_{objectQualifier}_user_from FOREIGN KEY (reputationfromuserid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_reputationvote_{objectQualifier}_user_to' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}reputationvote
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_reputationvote_{objectQualifier}_user_to FOREIGN KEY (reputationtouserid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topictags_{objectQualifier}_tagid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}topictags
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topictags_{objectQualifier}_tagid FOREIGN KEY (tagid)
      REFERENCES {databaseSchema}.{objectQualifier}tags(tagid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_topictags_{objectQualifier}_topicid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}topictags
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_topictags_{objectQualifier}_topicid FOREIGN KEY (topicid)
      REFERENCES {databaseSchema}.{objectQualifier}topic(topicid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_adminpageuseraccess_{objectQualifier}_user' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}adminpageuseraccess
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_adminpageuseraccess_{objectQualifier}_user FOREIGN KEY (userid)
      REFERENCES {databaseSchema}.{objectQualifier}user(userid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_eventloggroupaccess_{objectQualifier}_group_groupid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}eventloggroupaccess
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_eventloggroupaccess_{objectQualifier}_group_groupid FOREIGN KEY (groupid)
      REFERENCES {databaseSchema}.{objectQualifier}group(groupid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_forumhistory_{objectQualifier}_forum_forumid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}forumhistory
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forumhistory_{objectQualifier}_forum_forumid FOREIGN KEY (forumid)
      REFERENCES {databaseSchema}.{objectQualifier}forum(forumid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_grouphistory_{objectQualifier}_group_groupid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}grouphistory
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_grouphistory_{objectQualifier}_group_groupid FOREIGN KEY (groupid)
      REFERENCES {databaseSchema}.{objectQualifier}group(groupid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

 IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_grouphistory_{objectQualifier}_group_groupid' LIMIT 1) THEN
ALTER TABLE {databaseSchema}.{objectQualifier}accessmaskhistory
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_accessmaskhistory_{objectQualifier}_accessmask_accessmaskid FOREIGN KEY (accessmaskid)
      REFERENCES {databaseSchema}.{objectQualifier}accessmask(accessmaskid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE; 
END IF;

-- Foreign Key: fk_{databaseSchema}_{objectQualifier}_useralbum_{objectQualifier}_useralbumimage
/* IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype='f' and conname ='fk_{databaseSchema}_{objectQualifier}_album_{objectQualifier}_albumimage') THEN
ALTER TABLE {databaseSchema}.{objectQualifier}useralbumimage
  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_album_{objectQualifier}_albumimage FOREIGN KEY (albumid)
      REFERENCES {databaseSchema}.{objectQualifier}useralbum(albumid) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION;
END IF; */

         END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
     GRANT EXECUTE ON FUNCTION {databaseSchema}.{objectQualifier}create_or_check_fkeys() TO public;
    --GO
    SELECT {databaseSchema}.{objectQualifier}create_or_check_fkeys();
    --GO
    DROP FUNCTION {databaseSchema}.{objectQualifier}create_or_check_fkeys();
--GO 
      



