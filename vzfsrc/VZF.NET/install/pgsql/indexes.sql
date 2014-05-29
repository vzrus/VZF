-- This scripts for PostgreSQL Yet Another Forum https://github.com/vzrus/YetAnotherForumExtraDataLayers http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team  https://github.com/vzrus
-- They are distributed under terms of GPLv2 licence only as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2009-2012

-- Index
-- {databaseSchema}.{objectQualifier}create_or_replace_index args:
--  _schemaname, _tablename, _indexname ,_indexuniue('unique'|''),	_indexcolumn
SELECT {databaseSchema}.{objectQualifier}create_or_replace_index
('{databaseSchema}', '{objectQualifier}bbcode','fki_{databaseSchema}_{objectQualifier}_bbcode_board','','boardid');
--GO	
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}create_or_check_indexes()
RETURNS void AS
$BODY$
BEGIN


/*IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}bbcode' AND indexname='fki_{databaseSchema}_{objectQualifier}_bbcode_board') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_bbcode_board ON {databaseSchema}.{objectQualifier}bbcode USING btree (boardid);
END IF;*/

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}accessmask' AND indexname='fki_{databaseSchema}_{objectQualifier}_accessmask_{objectQualifier}_board') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_accessmask_{objectQualifier}_board ON {databaseSchema}.{objectQualifier}accessmask USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}active' AND indexname='fki_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_board') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_board ON {databaseSchema}.{objectQualifier}active USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}active' AND indexname='fki_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_forum') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_forum ON {databaseSchema}.{objectQualifier}active USING btree (forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}active' AND indexname='fki_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_topic') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_topic ON {databaseSchema}.{objectQualifier}active USING btree (topicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}active' AND indexname='fki_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_user') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_active_{objectQualifier}_user ON {databaseSchema}.{objectQualifier}active USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}group' AND indexname='ix_{objectQualifier}_group') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_group ON {databaseSchema}.{objectQualifier}group USING btree (groupid, name);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}pollvote' AND indexname='ix_{objectQualifier}_pollvote_remoteip') THEN
CREATE INDEX ix_{objectQualifier}_pollvote_remoteip ON {databaseSchema}.{objectQualifier}pollvote USING btree (remoteip);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}pollvote' AND indexname='ix_{objectQualifier}_pollvote_userid') THEN
CREATE INDEX ix_{objectQualifier}_pollvote_userid ON {databaseSchema}.{objectQualifier}pollvote USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}rank' AND indexname='ix_{objectQualifier}_rank_boardid_name') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_rank_boardid_name ON {databaseSchema}.{objectQualifier}rank USING btree (boardid, name);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}registry' AND indexname='ix_{objectQualifier}_registry_boardid_name') THEN
CREATE INDEX ix_{objectQualifier}_registry_boardid_name ON {databaseSchema}.{objectQualifier}registry USING btree (boardid, name);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}reputationvote' AND indexname='ix_{objectQualifier}_reputationvote_fruserid_touserid') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_reputationvote_fruserid_touserid ON {databaseSchema}.{objectQualifier}reputationvote USING btree (reputationfromuserid, reputationtouserid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}smiley' AND indexname='ix_{objectQualifier}_smiley_boardid_code') THEN
CREATE INDEX ix_{objectQualifier}_smiley_boardid_code ON {databaseSchema}.{objectQualifier}smiley USING btree (boardid, code);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}topic' AND indexname='ix_{objectQualifier}_topic_lastposted_topicid') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_topic_lastposted_topicid ON {databaseSchema}.{objectQualifier}topic USING btree (lastposted,topicid DESC);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}group' AND indexname='ix_{objectQualifier}_group_sortorder') THEN
CREATE INDEX ix_{objectQualifier}_group_sortorder ON {databaseSchema}.{objectQualifier}group USING btree (sortorder ASC);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}user' AND indexname='ix_{objectQualifier}_user_displayname') THEN
CREATE INDEX ix_{objectQualifier}_user_displayname ON {databaseSchema}.{objectQualifier}user USING btree (displayname ASC);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}topic' AND indexname='ix_{objectQualifier}_topic_flags') THEN
CREATE INDEX ix_{objectQualifier}_topic_flags ON {databaseSchema}.{objectQualifier}topic USING btree (flags);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}usergroup' AND indexname='ix_{objectQualifier}_usergroup_userid') THEN
CREATE INDEX ix_{objectQualifier}_usergroup_userid ON {databaseSchema}.{objectQualifier}usergroup USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}user' AND indexname='ix_{objectQualifier}_user_flags') THEN
CREATE INDEX ix_{objectQualifier}_user_flags ON {databaseSchema}.{objectQualifier}user USING btree (flags);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}user' AND indexname='ix_{objectQualifier}_user_name') THEN
CREATE INDEX ix_{objectQualifier}_user_name ON {databaseSchema}.{objectQualifier}user USING btree (name);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}user' AND indexname='ix_{objectQualifier}_user_provideruserkey') THEN
CREATE INDEX ix_{objectQualifier}_user_provideruserkey ON {databaseSchema}.{objectQualifier}user USING btree (provideruserkey);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}watchforum' AND indexname='ix_{objectQualifier}_watchforum_forumid_userid') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_watchforum_forumid_userid ON {databaseSchema}.{objectQualifier}watchforum USING btree (forumid, userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}watchtopic' AND indexname='ix_{objectQualifier}_watchtopic_topicid_userid') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_watchtopic_topicid_userid ON {databaseSchema}.{objectQualifier}watchtopic USING btree (topicid, userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}attachment' AND indexname='fki_{databaseSchema}_{objectQualifier}_attachment_{objectQualifier}_message_messageid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_attachment_{objectQualifier}_message_messageid ON {databaseSchema}.{objectQualifier}attachment USING btree (messageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}checkemail' AND indexname='fki_{databaseSchema}_{objectQualifier}_checkemail_{objectQualifier}_user_userid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_checkemail_{objectQualifier}_user_userid ON {databaseSchema}.{objectQualifier}checkemail USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}choice' AND indexname='fki_{databaseSchema}_{objectQualifier}_choice_{objectQualifier}_poll_pollid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_choice_{objectQualifier}_poll_pollid ON {databaseSchema}.{objectQualifier}choice USING btree (pollid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}eventlog' AND indexname='fki_{databaseSchema}_{objectQualifier}_eventlog_{objectQualifier}_user_userid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_eventlog_{objectQualifier}_user_userid ON {databaseSchema}.{objectQualifier}eventlog USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}extension' AND indexname='fki_{databaseSchema}_{objectQualifier}_extension_{objectQualifier}_board_boardid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_extension_{objectQualifier}_board_boardid ON {databaseSchema}.{objectQualifier}extension USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forumaccess' AND indexname='fki_{databaseSchema}_{objectQualifier}_forumaccess_{objectQualifier}_accessmask_accessmaskid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_forumaccess_{objectQualifier}_accessmask_accessmaskid ON {databaseSchema}.{objectQualifier}forumaccess USING btree (accessmaskid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='fki_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_message_lastmessageid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_message_lastmessageid ON {databaseSchema}.{objectQualifier}forum USING btree (lastmessageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='fki_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_topic_lasttopicid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_topic_lasttopicid ON {databaseSchema}.{objectQualifier}forum USING btree (lasttopicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='fki_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_user_lastuserid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_user_lastuserid ON {databaseSchema}.{objectQualifier}forum USING btree (lastuserid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}group' AND indexname='fki_{databaseSchema}_{objectQualifier}_group_{objectQualifier}_board_boardid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_group_{objectQualifier}_board_boardid ON {databaseSchema}.{objectQualifier}group USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}message' AND indexname='fki_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_message_replyto') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_message_replyto ON {databaseSchema}.{objectQualifier}message USING btree (replyto);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}message' AND indexname='fki_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_topic_topicid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_topic_topicid ON {databaseSchema}.{objectQualifier}message USING btree (topicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}message' AND indexname='fki_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_posted_desc') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_posted_desc ON {databaseSchema}.{objectQualifier}message USING btree (posted DESC);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}message' AND indexname='fki_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_user_userid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_message_{objectQualifier}_user_userid ON {databaseSchema}.{objectQualifier}message USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}nntpforum' AND indexname='fki_{databaseSchema}_{objectQualifier}_nntpforum_{objectQualifier}_forum_forumid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_nntpforum_{objectQualifier}_forum_forumid ON {databaseSchema}.{objectQualifier}nntpforum USING btree (forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}nntpforum' AND indexname='fki_{databaseSchema}_{objectQualifier}_nntpforum_{objectQualifier}_nntpserver_nntpserverid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_nntpforum_{objectQualifier}_nntpserver_nntpserverid ON {databaseSchema}.{objectQualifier}nntpforum USING btree (nntpserverid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}nntpserver' AND indexname='fki_{databaseSchema}_{objectQualifier}_nntpserver_{objectQualifier}_board_boardid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_nntpserver_{objectQualifier}_board_boardid ON {databaseSchema}.{objectQualifier}nntpserver USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}nntptopic' AND indexname='fki_{databaseSchema}_{objectQualifier}_nntptopic_{objectQualifier}_nntpforum_nntpforumid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_nntptopic_{objectQualifier}_nntpforum_nntpforumid ON {databaseSchema}.{objectQualifier}nntptopic USING btree (nntpforumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}nntptopic' AND indexname='fki_{databaseSchema}_{objectQualifier}_nntptopic_{objectQualifier}_topic_topicid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_nntptopic_{objectQualifier}_topic_topicid ON {databaseSchema}.{objectQualifier}nntptopic USING btree (topicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}pmessage' AND indexname='fki_{databaseSchema}_{objectQualifier}_pmessage_{objectQualifier}_user1_fromuserid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_pmessage_{objectQualifier}_user1_fromuserid ON {databaseSchema}.{objectQualifier}pmessage USING btree (fromuserid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}pollvote' AND indexname='fki_{databaseSchema}_{objectQualifier}_pollvote_{objectQualifier}_poll_pollid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_pollvote_{objectQualifier}_poll_pollid ON {databaseSchema}.{objectQualifier}pollvote USING btree (pollid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}registry' AND indexname='fki_{databaseSchema}_{objectQualifier}_registry_{objectQualifier}_board_boardid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_registry_{objectQualifier}_board_boardid ON {databaseSchema}.{objectQualifier}registry USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}smiley' AND indexname='fki_{databaseSchema}_{objectQualifier}_smiley_{objectQualifier}_board_boardid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_smiley_{objectQualifier}_board_boardid ON {databaseSchema}.{objectQualifier}smiley USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}topic' AND indexname='fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_forum_forumid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_forum_forumid ON {databaseSchema}.{objectQualifier}topic USING btree (forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}topic' AND indexname='fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_message_lastmessageid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_message_lastmessageid ON {databaseSchema}.{objectQualifier}topic USING btree (lastmessageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}topic' AND indexname='fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_poll_pollid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_poll_pollid ON {databaseSchema}.{objectQualifier}topic USING btree (pollid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}topic' AND indexname='fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_topic_topicmovedid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_topic_topicmovedid ON {databaseSchema}.{objectQualifier}topic USING btree (topicmovedid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}topic' AND indexname='fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_user_userid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_user_userid ON {databaseSchema}.{objectQualifier}topic USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}topic' AND indexname='fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_user2_lastuserid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_topic_{objectQualifier}_user2_lastuserid ON {databaseSchema}.{objectQualifier}topic USING btree (lastuserid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}userforum' AND indexname='fki_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_accessmask_accessmaskid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_accessmask_accessmaskid ON {databaseSchema}.{objectQualifier}userforum USING btree (accessmaskid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}userforum' AND indexname='fki_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_forum_forumid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_userforum_{objectQualifier}_forum_forumid ON {databaseSchema}.{objectQualifier}userforum USING btree (forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}usergroup' AND indexname='fki_{databaseSchema}_{objectQualifier}_usergroup_{objectQualifier}_group_groupid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_usergroup_{objectQualifier}_group_groupid ON {databaseSchema}.{objectQualifier}usergroup USING btree (groupid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}userpmessage' AND indexname='fki_{databaseSchema}_{objectQualifier}_userpmessage_{objectQualifier}_pmessage_pmessageid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_userpmessage_{objectQualifier}_pmessage_pmessageid ON {databaseSchema}.{objectQualifier}userpmessage USING btree (pmessageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}userpmessage' AND indexname='fki_{databaseSchema}_{objectQualifier}_userpmessage_{objectQualifier}_user_userid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_userpmessage_{objectQualifier}_user_userid ON {databaseSchema}.{objectQualifier}userpmessage USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}user' AND indexname='fki_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_board_boardid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_board_boardid ON {databaseSchema}.{objectQualifier}user USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}user' AND indexname='fki_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_rank_rankid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_user_{objectQualifier}_rank_rankid ON {databaseSchema}.{objectQualifier}user USING btree (rankid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}watchforum' AND indexname='fki_{databaseSchema}_{objectQualifier}_watchforum1_{objectQualifier}_forum_forumid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_watchforum1_{objectQualifier}_forum_forumid ON {databaseSchema}.{objectQualifier}watchforum USING btree (forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}watchforum' AND indexname='fki_{databaseSchema}_{objectQualifier}_watchforum1_{objectQualifier}_user_userid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_watchforum1_{objectQualifier}_user_userid ON {databaseSchema}.{objectQualifier}watchforum USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}watchtopic' AND indexname='fki_{databaseSchema}_{objectQualifier}_watchtopic_{objectQualifier}_topic_topicid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_watchtopic_{objectQualifier}_topic_topicid ON {databaseSchema}.{objectQualifier}watchtopic USING btree (topicid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}watchtopic' AND indexname='fki_{databaseSchema}_{objectQualifier}_watchtopic_{objectQualifier}_user_userid') THEN
CREATE INDEX fki_{databaseSchema}_{objectQualifier}_watchtopic_{objectQualifier}_user_userid ON {databaseSchema}.{objectQualifier}watchtopic USING btree (userid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}bannedip' AND indexname='ix_{objectQualifier}_bannedip_boardid_mask') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_bannedip_boardid_mask ON {databaseSchema}.{objectQualifier}bannedip USING btree (boardid, mask);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}category' AND indexname='ix_{objectQualifier}_category_boardid_name') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_category_boardid_name ON {databaseSchema}.{objectQualifier}category USING btree (boardid, name);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}category' AND indexname='ix_{objectQualifier}_category_boardid') THEN
CREATE INDEX ix_{objectQualifier}_category_boardid ON {databaseSchema}.{objectQualifier}category USING btree (boardid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}category' AND indexname='ix_{objectQualifier}_category_name') THEN
CREATE INDEX ix_{objectQualifier}_category_name ON {databaseSchema}.{objectQualifier}category USING btree (name);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}checkemail' AND indexname='ix_{objectQualifier}_checkemail_hash') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_checkemail_hash ON {databaseSchema}.{objectQualifier}checkemail USING btree (hash);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='ix_{objectQualifier}_forum_parentid_name') THEN
CREATE UNIQUE INDEX ix_{objectQualifier}_forum_parentid_name ON {databaseSchema}.{objectQualifier}forum USING btree (parentid, name);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forumaccess' AND indexname='ix_{objectQualifier}_forumaccess_forumid') THEN
CREATE INDEX ix_{objectQualifier}_forumaccess_forumid ON {databaseSchema}.{objectQualifier}forumaccess USING btree (forumid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='ix_{objectQualifier}_forum_categoryid') THEN
CREATE INDEX ix_{objectQualifier}_forum_categoryid ON {databaseSchema}.{objectQualifier}forum USING btree (categoryid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='ix_{objectQualifier}_forum_flags') THEN
CREATE INDEX ix_{objectQualifier}_forum_flags ON {databaseSchema}.{objectQualifier}forum USING btree (flags);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='ix_{objectQualifier}_forum_parentid') THEN
CREATE INDEX ix_{objectQualifier}_forum_parentid ON {databaseSchema}.{objectQualifier}forum USING btree (parentid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}buddy' AND indexname='ix_{objectQualifier}_buddy_fromuserid_touserid') THEN
CREATE UNIQUE INDEX  ix_{objectQualifier}_buddy_fromuserid_touserid ON {databaseSchema}.{objectQualifier}buddy USING btree (fromuserid,touserid);
END IF;

-- {objectQualifier}Thanks

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}thanks' AND indexname='ix_{objectQualifier}_thanks_messageid') THEN
CREATE INDEX ix_{objectQualifier}_thanks_messageid ON {databaseSchema}.{objectQualifier}thanks USING btree (messageid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}thanks' AND indexname='ix_{objectQualifier}_thanks_thanksfromuserid') THEN
CREATE INDEX ix_{objectQualifier}_thanks_thanksfromuserid ON {databaseSchema}.{objectQualifier}thanks USING btree (thanksfromuserid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}thanks' AND indexname='ix_{objectQualifier}_thanks_thankstouserid') THEN
CREATE INDEX ix_{objectQualifier}_thanks_thankstouserid ON {databaseSchema}.{objectQualifier}thanks USING btree (thankstouserid);
END IF;

END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;
     GRANT EXECUTE ON FUNCTION {databaseSchema}.{objectQualifier}create_or_check_indexes() TO public;
    --GO
    SELECT {databaseSchema}.{objectQualifier}create_or_check_indexes();
    --GO
    DROP FUNCTION {databaseSchema}.{objectQualifier}create_or_check_indexes();
--GO
