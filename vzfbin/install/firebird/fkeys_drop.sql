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

-- Drop all foreign keys if exist.


EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}WATCHTOPIC', 'FK_{objectQualifier}WATCHTOPIC_TOPIC');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}POLL', 'FK_{objectQualifier}POLL_POLLGROUPCLUSTER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}WATCHTOPIC', 'FK_{objectQualifier}WATCHTOPIC_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}WATCHFORUM', 'FK_{objectQualifier}WATCHFORUM1_FORUM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}WATCHFORUM', 'FK_{objectQualifier}WATCHFORUM1_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USERFORUM', 'FK_{objectQualifier}USERFORUM_ACCM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USERFORUM', 'FK_{objectQualifier}USERFORUM_FORUM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USERFORUM', 'FK_{objectQualifier}USERFORUM_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}TOPIC', 'FK_{objectQualifier}TOPIC_FORUM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}TOPIC', 'FK_{objectQualifier}TOPIC_MESSAGE');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}TOPIC', 'FK_{objectQualifier}TOPIC_POLL');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}TOPIC', 'FK_{objectQualifier}TOPIC_TOPIC');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}TOPIC', 'FK_{objectQualifier}TOPIC_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}TOPIC', 'FK_{objectQualifier}TOPIC_USER_2');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}NNTPTOPIC', 'FK_{objectQualifier}NNTPTOPIC_NNTPF');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}NNTPTOPIC', 'FK_{objectQualifier}NNTPTOPIC_TOPIC');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}NNTPFORUM', 'TR_AI_{objectQualifier}NNTPFORUM_FORUM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}NNTPFORUM', 'TR_AI_{objectQualifier}NNTPFORUM_NNTPSERV');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}MESSAGE', 'FK_{objectQualifier}MESSAGE_MESID_REPLYTO');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}MESSAGE', 'FK_{objectQualifier}MESSAGE_TOPIC');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}MESSAGE', 'FK_{objectQualifier}MESSAGE_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUMACCESS', 'FK_{objectQualifier}FORUMACCESS_MASK');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUMACCESS', 'FK_{objectQualifier}FORUMACCESS_FORUM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUMACCESS', 'FK_{objectQualifier}FORUMACCESS_GROUP');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUM', 'FK_{objectQualifier}FORUM_CATEGORY');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUM', 'FK_{objectQualifier}FORUM_FORUM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUM', 'FK_{objectQualifier}FORUM_MESSAGE');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUM', 'FK_{objectQualifier}FORUM_TOPIC');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUM', 'FK_{objectQualifier}FORUM_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}ATTACHMENT', 'FK_{objectQualifier}ATTACHMENT_MESSAGE');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}ACTIVE', 'FK_{objectQualifier}ACTIVE_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}ACTIVE', 'FK_{objectQualifier}ACTIVE_FORUM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}ACTIVE', 'FK_{objectQualifier}ACTIVE_TOPIC');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}ACTIVE', 'FK_{objectQualifier}ACTIVE_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USERPMESSAGE', 'FK_{objectQualifier}USERPMESSAGE_PM');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USERPMESSAGE', 'FK_{objectQualifier}USERPMESSAGE_PMU');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USERGROUP', 'FK_{objectQualifier}USERGROUP_GROUP');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USERGROUP', 'FK_{objectQualifier}USERGROUP_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}PMESSAGE', 'FK_{objectQualifier}PMESSAGE_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}EVENTLOG', 'FK_{objectQualifier}EVENTLOG_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}CHECKEMAIL', 'FK_{objectQualifier}CHECKEMAIL_USER');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USER', 'FK_{objectQualifier}USER_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}USER', 'FK_{objectQualifier}USER_RANK');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}SMILEY', 'FK_{objectQualifier}SMILEY_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}REGISTRY', 'FK_{objectQualifier}REGISTRY_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}RANK', 'FK_{objectQualifier}RANK_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}POLLVOTE', 'FK_{objectQualifier}POLLVOTE_POLL');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}CHOICE', 'FK_{objectQualifier}CHOICE_POLL');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}NNTPSERVER', 'FK_{objectQualifier}NNTPSERVER_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}GROUP', 'FK_{objectQualifier}GROUP_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}EXTENSION', 'FK_{objectQualifier}EXTENSION_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}CATEGORY', 'FK_{objectQualifier}Category_{objectQualifier}Boar');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}BBCODE', 'FK_{objectQualifier}BBCODE_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}BANNEDIP', 'FK_{objectQualifier}BANNEDIP_BOARDID');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}ACCESSMASK', 'FK_{objectQualifier}ACCESSMASK_BOARD');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}EVENTLOGGROUPACCESS', 'FK_{objectQualifier}ELOGGROUPACCESS_GROUPID');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}ADMINPAGEUSERACCESS', 'FK_{objectQualifier}ADMPGUSRACCESS_USERID');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}GROUPHISTORY', 'FK_{objectQualifier}GROUPHISTORY_GROUPID');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}FORUMHISTORY', 'FK_{objectQualifier}FORUMHISTORY_FORUMID');
--GO
EXECUTE PROCEDURE DP_DROP_FKEY_PROC('{objectQualifier}ACCESSMASKHISTORY', 'FK_{objectQualifier}ACSMASKHIST_ACSMASKID');
--GO



