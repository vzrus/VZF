-- These scripts for MySQL for Yet Another Forum http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team https://github.com/vzrus http://sourceforge.net/projects/yaf-datalayers/
-- They are distributed under terms of GPLv2 only licence as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2006-2013


/* STORED PROCEDURE CREATED BY VZ-TEAM */

/* DROP procedures */
-- not in use
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_setguid;
--GO
-- vaccess procedure
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}vaccess_s_moderatoraccess_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}vaccessfull_combo;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}vaccess_combo;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}vaccess_s_moderatoraccess_list;
--GO
-- eof vaccess procedure
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}digest_topicnew;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}digest_topicactive;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_tags;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_bytags;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_unanswered;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_unanswered_result;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_update_ssn_status;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}active_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}active_listforum;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}active_listtopic;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}active_stats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}active_updatemaxstats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}admin_pageaccesslist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}adminpageaccess_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}adminpageaccess_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}adminpageaccess_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}eventloggroupaccess_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}eventloggroupaccess_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}eventloggroupaccess_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}eventlog_deletebyuser;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_eventlogaccesslist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}attachment_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}attachment_download;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}attachment_list ;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}attachment_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}bannedip_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}bannedip_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}bannedip_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}bbcode_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}bbcode_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}bbcode_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_create;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_poststats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_userstats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_poststats1;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_poststats2;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_poststats3;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_resync;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}board_stats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}category_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}category_getadjacentforum;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}category_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}category_pfaccesslist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}category_listread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}category_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}checkemail_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}checkemail_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}checkemail_update;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}choice_add;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}choice_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}choice_update;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}choice_vote;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}eventlog_create;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}eventlog_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}eventlog_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}extension_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}extension_edit;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}extension_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}extension_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_cataccess_actuser;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_move;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_byuserlist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listall;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listall_fromcat;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listallmymoderated;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listpath;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_listread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listreadpersonal;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listread1;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listread_old;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listSubForums;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listtopics;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_maxid;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_moderatelist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_moderators;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_moderators_1;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_moderators_2;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}moderators_team_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_resync;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_save_prntchck;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_updatelastpost ;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_updatestats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forumaccess_group;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forumaccess_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forumaccess_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_byuserlist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_medal_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_medal_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_medal_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_member;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}group_rank_style;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}mail_create;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}mail_createwatch;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}mail_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}mail_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}medal_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}medal_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}medal_listusers;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}medal_resort;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}medal_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_approve;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_deleteundelete; 
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_findunread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_getReplies;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_listreported;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_move;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_reply_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_listreporters;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_report;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_reportcopyover;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_reportresolve;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_unapproved;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_update;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntpforum_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntpforum_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntpforum_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}readtopic_addorupdate;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}readtopic_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}readforum_addorupdate;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}readtopic_lastread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}readforum_lastread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}readforum_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntpforum_update;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntpserver_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntpserver_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntpserver_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntptopic_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntptopic_savemessage;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}nntptopic_addmessage;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}activeaccess_reset;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pageload;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pmessage_archive;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pmessage_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pmessage_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pmessage_markread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pmessage_info;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pmessage_prune;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pmessage_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}poll_remove;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_attach;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_setlinks;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}choice_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_remove;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}poll_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_stats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}poll_stats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollgroup_votecheck;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}poll_update;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}pollvote_check;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}post_last10user;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}post_alluser;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}post_list_result;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}post_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}post_listbyposition;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}post_list_reverse10;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}rank_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}rank_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}rank_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}registry_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}registry_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}replace_words_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}replace_words_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}replace_words_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}shoutbox_getmessages;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}shoutbox_savemessage;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}shoutbox_clearmessages;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}smiley_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}smiley_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}smiley_listunique;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}smiley_resort;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}smiley_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}system_initialize;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}system_updateversion;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_tagsave;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_active;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_active_result;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_unread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_unread_result;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_create_by_message;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_favorite_count;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_findduplicate;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_findnext;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_findprev;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_info;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_findduplicate;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_imagesave;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_latest;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}rss_topic_latest;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}announcements_list_result;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}announcements_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_list_result;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_tags;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_list_old;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_listmessages;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_lock;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_move;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_poll_update;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_prune;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_restore;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_updatelastpost;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_updatetopic;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topics_byuser;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topics_byuser_result;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_accessmasks;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_accessmasksbyforum;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_addpoints;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_adminsave;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_approve;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_approveall;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_deleteavatar;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_aspnet;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_avatarimage;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_changepassword;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_addignoreduser;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_ignoredlist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_removeignoreduser;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_savestyle;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_savestyle_all;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_isuserignored;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_deleteold;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_emails;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_lastread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_updatefacebookstatus;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_find;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_getpoints;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_getsignature;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_guest;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_lastread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_listtodaysbirthdays;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_lastread;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_list_new;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_listmembers;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_listmembers_result;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}admin_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_login;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_medal_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_medal_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_medal_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_nntp;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_pmcount;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_recoverpassword;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_removepoints;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_removepointsbytopicid;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_resetpoints;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_savenotification;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_saveavatar;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_savepassword;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_savesignature;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_setnotdirty;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_setpoints;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_setrole;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_suspend;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_upgrade;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}userforum_access;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}userforum_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}userforum_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}userforum_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}usergroup_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}usergroup_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}userpmessage_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}userpmessage_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}watchforum_add;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}watchforum_check;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}watchforum_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}watchforum_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}watchtopic_add;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}watchtopic_check;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}watchtopic_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}watchtopic_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_migrate;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_listmedals;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_thankedmessage;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}accessmask_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}accessmask_searchlist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}accessmask_pforumlist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}accessmask_aforumlist;
--GO
DROP PROCEDURE IF EXISTS {objectQualifier}accessmask_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}accessmask_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_get;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_latest1;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_latest;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_simplelist;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_activity_rank;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_simplelist;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_announcements;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topicstatus_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topicstatus_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topicstatus_edit;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topicstatus_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_simplelist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}category_simplelist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_simplelist;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}db_size;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}rsstopic_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_addthanks;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_getthanks;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_isthankedbyuser;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_removethanks;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_thanksnumber;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_getthanks_from;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_getthanks_to;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}recent_users;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_getallthanks ;
--GO
 -- Drop old procedures
 DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}active_insert;
 --GO  
 DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listread_helper;
 --GO
 DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listread_helper_old; 
--GO
 DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_listread_old;
 --GO
-- eof Drop old procedures
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}buddy_addrequest;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}buddy_approverequest;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}buddy_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}buddy_remove;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}buddy_denyrequest;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_favorite_add;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_favorite_remove;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_favorite_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_favorite_details;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}topic_favorite_details_result;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_save;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_list;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_gettitle;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_getstats;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_image_save;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_image_list;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_images_by_user;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_image_delete;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}album_image_download;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_getsignaturedata;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_getalbumsdata;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_secdata;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}messagehistory_list;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}active_list_user;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}message_gettextbyids;
--GO 
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_thankfromcount;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_repliedtopic;
--GO


DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_viewallthanks;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_viewthanksfrom;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_viewthanksto;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}user_lazydata;
--GO

/* Create vaccess procedures */

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}eventlog_create(
i_UserID      INT,
i_Source      VARCHAR(128),
i_Description TEXT,
i_Type        INT,
i_UTCTIMESTAMP DATETIME)
MODIFIES SQL DATA
BEGIN

INSERT INTO {databaseSchema}.{objectQualifier}EventLog
(UserID,
Source,
Description,
`Type`)
VALUES     (i_UserID,
i_Source,
i_Description,
i_Type);
	
	END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}eventlog_delete
 (
	i_EventLogID INT, 
	i_BoardID  INT,
	i_PageUserID INT
 )
 MODIFIES SQL DATA
 BEGIN
	 /*either EventLogID or BoardID must be null, not both at the same time*/
	if i_EventLogID IS NULL THEN 
		/* delete all events of this board*/
		DELETE FROM {databaseSchema}.{objectQualifier}EventLog
		WHERE
			(UserID IS NULL or
			UserID IN (SELECT UserID FROM {databaseSchema}.{objectQualifier}User WHERE BoardID=i_BoardID));
	
	ELSE 
		 /*delete just one event*/
		DELETE FROM {databaseSchema}.{objectQualifier}EventLog WHERE EventLogID=i_EventLogID;
	END IF;
 END;
--GO

create procedure {databaseSchema}.{objectQualifier}eventlog_list(
				 i_BoardID INT, i_PageUserID int, i_MaxRows int, i_MaxDays int,  i_PageIndex int,
				 i_PageSize int, i_SinceDate datetime, i_ToDate datetime, i_EventIDs VARCHAR(8000),
				 i_UTCTIMESTAMP datetime) 
begin
   DECLARE ici_TotalRows INT ;
   DECLARE ici_FirstSelectRowNumber INT DEFAULT 0;
   DECLARE ici_FirstSelectRowID INT;
   DECLARE ici_EventID VARCHAR(11);
   DECLARE ici_Pos INT;
   DECLARE topLogID INT;
   DECLARE Version VARCHAR(128);

   --  delete entries older than i_MaxDays days
   
   DELETE FROM {databaseSchema}.{objectQualifier}EventLog
   WHERE DATEDIFF(i_UTCTIMESTAMP,EventTime) > i_MaxDays;

   -- or if there are more then i_MaxRows
   IF ((SELECT COUNT(1) FROM   {databaseSchema}.{objectQualifier}EventLog) >= (i_MaxRows+50)) THEN
   SELECT VERSION() INTO Version;
   
   IF LOCATE('5.1',Version)<>0 OR LOCATE('5.4',Version)<>0 OR LOCATE('6.0',Version)<>0 THEN

   -- DELETE FROM {databaseSchema}.{objectQualifier}EventLog WHERE EventLogID IN (SELECT EventLogID FROM {databaseSchema}.{objectQualifier}EventLog ORDER BY EventTime LIMIT 100) ; 
   SELECT EventLogID INTO topLogID  FROM  {databaseSchema}.{objectQualifier}EventLog ORDER BY EventLogID LIMIT 1;
   DELETE FROM {databaseSchema}.{objectQualifier}EventLog
			WHERE  EventLogID BETWEEN  topLogID  AND topLogID +100;
   ELSE 
   SELECT EventLogID INTO topLogID  FROM  {databaseSchema}.{objectQualifier}EventLog ORDER BY EventLogID LIMIT 1;
   DELETE FROM {databaseSchema}.{objectQualifier}EventLog WHERE EventLogID BETWEEN  topLogID  AND topLogID + 100;
   END IF;
   END IF;    
  
   CREATE TEMPORARY TABLE IF NOT EXISTS  {objectQualifier}tmp_ParsedEventIDs
   (
   EventID int
   );
   
   TRUNCATE TABLE {objectQualifier}tmp_ParsedEventIDs;
   
   SET i_EventIDs = (CONCAT(TRIM(i_EventIDs), ','));
   SET ici_Pos = (LOCATE(',', i_EventIDs, 1));
   IF REPLACE(i_EventIDs, ',', '') <> '' THEN
		WHILE ici_Pos > 0 DO SET ici_EventID = LTRIM(RTRIM(LEFT(i_EventIDs, ici_Pos - 1)));
		IF ici_EventID <> '' THEN
		INSERT INTO {objectQualifier}tmp_ParsedEventIDs(EventID) VALUES (CAST(ici_EventID AS SIGNED)); 
		-- Use Appropriate conversion
		END IF;
		
		SET i_EventIDs = RIGHT(i_EventIDs, CHAR_LENGTH(i_EventIDs) - ici_Pos);
		SET ici_Pos = LOCATE(',', i_EventIDs, 1);
		END WHILE;
		-- to be sure that last value is inserted
		IF (CHAR_LENGTH(ici_EventID) > 0) THEN
		 INSERT INTO {objectQualifier}tmp_ParsedEventIDs (MessageID)
		  VALUES (CAST(ici_EventID AS SIGNED)); 
		END IF;
		END IF;
		
		set i_PageIndex = i_PageIndex + 1;
		
		if (exists (select 1 from {databaseSchema}.{objectQualifier}User where ((Flags & 1) = 1 and UserID = i_PageUserID) limit 1)) then
		select  count(1) into  ici_TotalRows from {databaseSchema}.{objectQualifier}EventLog a		
		left join {databaseSchema}.{objectQualifier}User b on b.UserID=a.UserID
		where  (b.UserID IS NULL or b.BoardID = i_BoardID)	
		and ((i_EventIDs IS NULL )  
			  OR  a.`Type` IN (select * from {objectQualifier}tmp_ParsedEventIDs))  
		and EventTime between i_SinceDate and i_ToDate;
		
		select  (i_PageIndex - 1) * i_PageSize into ici_FirstSelectRowNumber; 
		
		set @elprep = CONCAT('select
		a.*,		
		IFNULL(b.`Name`,''System'') as `Name`,
		{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
		from
		{databaseSchema}.{objectQualifier}EventLog a		
		left join {databaseSchema}.{objectQualifier}User b on b.UserID=a.UserID
		where (b.UserID IS NULL or b.BoardID = ',i_BoardID,')	and (',COALESCE(i_EventIDs,-1),' = - 1 OR  a.`Type` IN (',COALESCE(i_EventIDs,-1),')) and a.EventTime between ''',i_SinceDate,''' and ''',i_ToDate,''' 
		order by a.EventLogID   desc LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');
		
	   PREPARE stmt_els FROM @elprep;
	   EXECUTE stmt_els;
	   DEALLOCATE PREPARE stmt_els;     
else
	   select  count(1) into  ici_TotalRows 
	   from  {databaseSchema}.{objectQualifier}EventLog a
	   left join {databaseSchema}.{objectQualifier}EventLogGroupAccess e on e.EventTypeID = a.`Type`
	   join {databaseSchema}.{objectQualifier}UserGroup ug on (ug.UserID =  i_PageUserID and ug.GroupID = e.GroupID)
	   left join {databaseSchema}.{objectQualifier}User b on b.UserID=a.UserID
	   where	 
	   (b.UserID IS NULL or b.BoardID = i_BoardID)	
	   and ((i_EventIDs IS NULL )  
			 OR  a.`Type` IN (select * from {objectQualifier}tmp_ParsedEventIDs))  
	   and EventTime between i_SinceDate and i_ToDate;
	
	   select  (i_PageIndex - 1) * i_PageSize + 1 into ici_FirstSelectRowNumber;
	   -- find first selectedrowid 
	  set @elprep = CONCAT('select
		a.*,		
		IFNULL(b.`Name`,''System'') as `Name`,
		{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
	from
		{databaseSchema}.{objectQualifier}EventLog a	
		left join {databaseSchema}.{objectQualifier}EventLogGroupAccess e on e.EventTypeID = a.`Type`
		join {databaseSchema}.{objectQualifier}UserGroup ug on (ug.UserID = ',i_PageUserID,' and ug.GroupID = e.GroupID)
		left join {databaseSchema}.{objectQualifier}User b on b.UserID=a.UserID
	  where (b.UserID IS NULL or b.BoardID = ',i_BoardID,')	and (',COALESCE(i_EventIDs,-1),' = - 1 OR  a.`Type` IN (',COALESCE(i_EventIDs,-1),')) and a.EventTime between''',i_SinceDate,''' and ''',i_ToDate,'''
	  order by a.EventLogID   desc LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');

	PREPARE stmt_els FROM  @elprep;
	EXECUTE stmt_els;
	DEALLOCATE PREPARE stmt_els; 
  
   end  if;
end;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}vaccess_combo(i_UserID INT, i_ForumID INT)
READS SQL DATA
BEGIN
DECLARE ici_UserID INT;
DECLARE ici_ForumID INT;
DECLARE ici_IsAdmin INT  DEFAULT 0;
DECLARE ici_IsGuest INT  DEFAULT 0;
DECLARE ici_IsForumModerator INT  DEFAULT 0;
DECLARE ici_IsModerator INT DEFAULT 0;
DECLARE ici_ReadAccess  INT DEFAULT 0;
DECLARE ici_PostAccess INT DEFAULT 0;
DECLARE ici_ReplyAccess INT DEFAULT 0;
DECLARE ici_PriorityAccess INT DEFAULT 0;
DECLARE ici_PollAccess INT DEFAULT 0;
DECLARE ici_VoteAccess INT DEFAULT 0;
DECLARE ici_ModeratorAccess INT DEFAULT 0;
DECLARE ici_EditAccess INT DEFAULT 0;
DECLARE ici_DeleteAccess INT DEFAULT 0;
DECLARE ici_UploadAccess INT DEFAULT 0;
DECLARE ici_DownloadAccess INT DEFAULT 0;
DECLARE ici_UserForumAccess INT DEFAULT 0;

DECLARE out_UserID INT;
DECLARE out_ForumID INT;
DECLARE out_IsAdmin INT DEFAULT 0;
DECLARE out_IsGuest INT DEFAULT 0;
DECLARE out_IsForumModerator INT DEFAULT 0;
DECLARE out_IsModerator INT DEFAULT 0;
DECLARE out_ReadAccess INT DEFAULT 0;
DECLARE out_PostAccess INT DEFAULT 0;
DECLARE out_ReplyAccess INT DEFAULT 0;
DECLARE out_PriorityAccess INT DEFAULT 0;
DECLARE out_PollAccess INT DEFAULT 0;
DECLARE out_VoteAccess INT DEFAULT 0;
DECLARE out_ModeratorAccess INT DEFAULT 0;
DECLARE out_EditAccess INT DEFAULT 0;
DECLARE out_DeleteAccess INT DEFAULT 0;
DECLARE out_UploadAccess INT DEFAULT 0;
DECLARE out_DownloadAccess INT DEFAULT 0;
DECLARE out_UserForumAccess INT DEFAULT 0;

SELECT 
IFNULL(UserID,i_UserID),IFNULL(ForumID,0),IFNULL(ReadAccess,0), IFNULL(PostAccess,0),IFNULL(ReplyAccess,0),IFNULL(PriorityAccess,0),
IFNULL(PollAccess,0),IFNULL(VoteAccess,0),IFNULL(ModeratorAccess,0),IFNULL(EditAccess,0),
IFNULL(DeleteAccess,0),IFNULL(UploadAccess,0),IFNULL(DownloadAccess,0),IFNULL(UserForumAccess,0)
INTO
ici_UserID,ici_ForumID,
ici_ReadAccess,ici_PostAccess,ici_ReplyAccess,ici_PriorityAccess,
ici_PollAccess,ici_VoteAccess,ici_ModeratorAccess, ici_EditAccess,
ici_DeleteAccess,ici_UploadAccess, ici_DownloadAccess, ici_UserForumAccess
FROM
 {databaseSchema}.{objectQualifier}vaccessfull1  
 WHERE UserID=i_UserID AND ForumID = IFNULL(i_ForumID,0) LIMIT 1; 
 
 SELECT 
IFNULL(UserID,i_UserID),IFNULL(ForumID,0),IFNULL(ReadAccess,0), IFNULL(PostAccess,0),IFNULL(ReplyAccess,0),IFNULL(PriorityAccess,0),
IFNULL(PollAccess,0),IFNULL(VoteAccess,0),IFNULL(ModeratorAccess,0),IFNULL(EditAccess,0),
IFNULL(DeleteAccess,0),IFNULL(UploadAccess,0),IFNULL(DownloadAccess,0),IFNULL(UserForumAccess,0)
INTO 
out_UserID,out_ForumID,
out_ReadAccess,out_PostAccess,out_ReplyAccess,out_PriorityAccess,
out_PollAccess,out_VoteAccess,out_ModeratorAccess, out_EditAccess,
out_DeleteAccess,out_UploadAccess, out_DownloadAccess, out_UserForumAccess
FROM
 {databaseSchema}.{objectQualifier}vaccessfull2  
 WHERE UserID=i_UserID AND ForumID = IFNULL(i_ForumID,0) LIMIT 1;
  
 SELECT         
	  SIGN(MAX(b.Flags & 1)),
	  SIGN(MAX(b.Flags & 2)),
	  SIGN(MAX(b.Flags & 8)) 
	  INTO  out_IsAdmin, out_IsGuest, out_IsForumModerator 
	  FROM {databaseSchema}.{objectQualifier}UserGroup a             
		   JOIN {databaseSchema}.{objectQualifier}Group b
			 ON b.GroupID = a.GroupID
			 WHERE a.UserID=i_UserID LIMIT 1;
 
SELECT
i_UserID AS UserID,
IFNULL(i_ForumID,0) AS ForumID,
out_IsAdmin AS IsAdmin,
out_IsGuest AS IsGuest,
out_IsForumModerator AS IsForumModerator,
(SELECT     COUNT(v.UserID) AS Expr1
FROM          {databaseSchema}.{objectQualifier}UserGroup AS v
INNER JOIN    {databaseSchema}.{objectQualifier}Group AS w
ON v.GroupID = w.GroupID
CROSS JOIN  {databaseSchema}.{objectQualifier}ForumAccess AS x
CROSS JOIN  {databaseSchema}.{objectQualifier}AccessMask AS y
WHERE (v.UserID = i_UserID)
AND (x.GroupID = w.GroupID)
AND (y.AccessMaskID = x.AccessMaskID)
AND (y.Flags & 64 <> 0)) AS IsModerator,
(CASE WHEN ici_ReadAccess > out_ReadAccess THEN ici_ReadAccess ELSE out_ReadAccess END)AS ReadAccess,
(CASE WHEN ici_PostAccess > out_PostAccess THEN ici_PostAccess ELSE out_PostAccess END) AS PostAccess,
(CASE WHEN ici_ReplyAccess > out_ReplyAccess THEN ici_ReplyAccess ELSE out_ReplyAccess END) AS ReplyAccess,
(CASE WHEN ici_PriorityAccess > out_PriorityAccess THEN ici_PriorityAccess ELSE out_PriorityAccess END) AS PriorityAccess,
(CASE WHEN ici_PollAccess > out_PollAccess THEN ici_PollAccess ELSE out_PollAccess END) AS PollAccess,
(CASE WHEN ici_VoteAccess > out_VoteAccess THEN ici_VoteAccess ELSE out_VoteAccess END) AS VoteAccess,
(CASE WHEN ici_ModeratorAccess > out_ModeratorAccess THEN ici_ModeratorAccess ELSE out_ModeratorAccess END) AS ModeratorAccess,
(CASE WHEN ici_EditAccess > out_EditAccess THEN ici_EditAccess ELSE out_EditAccess END) AS EditAccess,
(CASE WHEN ici_DeleteAccess > out_DeleteAccess THEN ici_DeleteAccess ELSE out_DeleteAccess END) AS DeleteAccess,
(CASE WHEN ici_UploadAccess > out_UploadAccess THEN ici_UploadAccess ELSE out_UploadAccess END) AS UploadAccess,
(CASE WHEN ici_DownloadAccess > out_DownloadAccess THEN ici_DownloadAccess ELSE out_DownloadAccess END) AS DownloadAccess,
(CASE WHEN ici_UserForumAccess > out_UserForumAccess THEN ici_UserForumAccess ELSE out_UserForumAccess END) AS UserForumAccess;
END;

--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}vaccessfull_combo(i_UserID INT, i_ForumID INT)
READS SQL DATA
BEGIN
DECLARE ici_UserID INT;
DECLARE ici_ForumID INT;
DECLARE ici_IsAdmin INT  DEFAULT 0;
DECLARE ici_IsGuest INT  DEFAULT 0;
DECLARE ici_IsForumModerator INT  DEFAULT 0;
DECLARE ici_IsModerator INT DEFAULT 0;
DECLARE ici_ReadAccess  INT DEFAULT 0;
DECLARE ici_PostAccess INT DEFAULT 0;
DECLARE ici_ReplyAccess INT DEFAULT 0;
DECLARE ici_PriorityAccess INT DEFAULT 0;
DECLARE ici_PollAccess INT DEFAULT 0;
DECLARE ici_VoteAccess INT DEFAULT 0;
DECLARE ici_ModeratorAccess INT DEFAULT 0;
DECLARE ici_EditAccess INT DEFAULT 0;
DECLARE ici_DeleteAccess INT DEFAULT 0;
DECLARE ici_UploadAccess INT DEFAULT 0;
DECLARE ici_DownloadAccess INT DEFAULT 0;
DECLARE ici_UserForumAccess INT DEFAULT 0;

DECLARE out_UserID INT;
DECLARE out_ForumID INT;
DECLARE out_IsAdmin INT DEFAULT 0;
DECLARE out_IsGuest INT DEFAULT 0;
DECLARE out_IsForumModerator INT DEFAULT 0;
DECLARE out_IsModerator INT DEFAULT 0;
DECLARE out_ReadAccess INT DEFAULT 0;
DECLARE out_PostAccess INT DEFAULT 0;
DECLARE out_ReplyAccess INT DEFAULT 0;
DECLARE out_PriorityAccess INT DEFAULT 0;
DECLARE out_PollAccess INT DEFAULT 0;
DECLARE out_VoteAccess INT DEFAULT 0;
DECLARE out_ModeratorAccess INT DEFAULT 0;
DECLARE out_EditAccess INT DEFAULT 0;
DECLARE out_DeleteAccess INT DEFAULT 0;
DECLARE out_UploadAccess INT DEFAULT 0;
DECLARE out_DownloadAccess INT DEFAULT 0;
DECLARE out_UserForumAccess INT DEFAULT 0;

SELECT 
IFNULL(UserID,i_UserID),IFNULL(ForumID,0),IFNULL(ReadAccess,0), IFNULL(PostAccess,0),IFNULL(ReplyAccess,0),IFNULL(PriorityAccess,0),
IFNULL(PollAccess,0),IFNULL(VoteAccess,0),IFNULL(ModeratorAccess,0),IFNULL(EditAccess,0),
IFNULL(DeleteAccess,0),IFNULL(UploadAccess,0),IFNULL(DownloadAccess,0),IFNULL(UserForumAccess,0)
INTO
ici_UserID,ici_ForumID,
ici_ReadAccess,ici_PostAccess,ici_ReplyAccess,ici_PriorityAccess,
ici_PollAccess,ici_VoteAccess,ici_ModeratorAccess, ici_EditAccess,
ici_DeleteAccess,ici_UploadAccess, ici_DownloadAccess, ici_UserForumAccess
FROM
 {databaseSchema}.{objectQualifier}vaccessfull1  
 WHERE UserID=i_UserID AND ForumID = IFNULL(i_ForumID,0) LIMIT 1; 
 
 SELECT 
IFNULL(UserID,i_UserID),IFNULL(ForumID,0),IFNULL(ReadAccess,0), IFNULL(PostAccess,0),IFNULL(ReplyAccess,0),IFNULL(PriorityAccess,0),
IFNULL(PollAccess,0),IFNULL(VoteAccess,0),IFNULL(ModeratorAccess,0),IFNULL(EditAccess,0),
IFNULL(DeleteAccess,0),IFNULL(UploadAccess,0),IFNULL(DownloadAccess,0),IFNULL(UserForumAccess,0)
INTO 
out_UserID,out_ForumID,
out_ReadAccess,out_PostAccess,out_ReplyAccess,out_PriorityAccess,
out_PollAccess,out_VoteAccess,out_ModeratorAccess, out_EditAccess,
out_DeleteAccess,out_UploadAccess, out_DownloadAccess, out_UserForumAccess 
FROM
 {databaseSchema}.{objectQualifier}vaccessfull2  
 WHERE UserID=i_UserID AND ForumID = IFNULL(i_ForumID,0) LIMIT 1;
  
 SELECT         
	  SIGN(MAX(b.Flags & 1)),
	  SIGN(MAX(b.Flags & 2)),
	  SIGN(MAX(b.Flags & 8)) 
	  INTO  out_IsAdmin, out_IsGuest, out_IsForumModerator 
	  FROM {databaseSchema}.{objectQualifier}UserGroup a             
		   JOIN {databaseSchema}.{objectQualifier}Group b
			 ON b.GroupID = a.GroupID
			 WHERE a.UserID=i_UserID LIMIT 1;
 
SELECT
i_UserID AS UserID,
IFNULL(i_ForumID,0) AS ForumID,
out_IsAdmin AS IsAdmin,
out_IsGuest AS IsGuest,
out_IsForumModerator AS IsForumModerator,
(SELECT     COUNT(v.UserID) AS Expr1
FROM          {databaseSchema}.{objectQualifier}UserGroup AS v
INNER JOIN    {databaseSchema}.{objectQualifier}Group AS w
ON v.GroupID = w.GroupID
CROSS JOIN  {databaseSchema}.{objectQualifier}ForumAccess AS x
CROSS JOIN  {databaseSchema}.{objectQualifier}AccessMask AS y
WHERE (v.UserID = i_UserID)
AND (x.GroupID = w.GroupID)
AND (y.AccessMaskID = x.AccessMaskID)
AND (y.Flags & 64 <> 0)) AS IsModerator,
(CASE WHEN ici_ReadAccess > out_ReadAccess THEN ici_ReadAccess ELSE out_ReadAccess END)AS ReadAccess,
(CASE WHEN ici_PostAccess > out_PostAccess THEN ici_PostAccess ELSE out_PostAccess END) AS PostAccess,
(CASE WHEN ici_ReplyAccess > out_ReplyAccess THEN ici_ReplyAccess ELSE out_ReplyAccess END) AS ReplyAccess,
(CASE WHEN ici_PriorityAccess > out_PriorityAccess THEN ici_PriorityAccess ELSE out_PriorityAccess END) AS PriorityAccess,
(CASE WHEN ici_PollAccess > out_PollAccess THEN ici_PollAccess ELSE out_PollAccess END) AS PollAccess,
(CASE WHEN ici_VoteAccess > out_VoteAccess THEN ici_VoteAccess ELSE out_VoteAccess END) AS VoteAccess,
(CASE WHEN ici_ModeratorAccess > out_ModeratorAccess THEN ici_ModeratorAccess ELSE out_ModeratorAccess END) AS ModeratorAccess,
(CASE WHEN ici_EditAccess > out_EditAccess THEN ici_EditAccess ELSE out_EditAccess END) AS EditAccess,
(CASE WHEN ici_DeleteAccess > out_DeleteAccess THEN ici_DeleteAccess ELSE out_DeleteAccess END) AS DeleteAccess,
(CASE WHEN ici_UploadAccess > out_UploadAccess THEN ici_UploadAccess ELSE out_UploadAccess END) AS UploadAccess,
(CASE WHEN ici_DownloadAccess > out_DownloadAccess THEN ici_DownloadAccess ELSE out_DownloadAccess END) AS DownloadAccess,
(CASE WHEN ici_UserForumAccess > out_UserForumAccess THEN ici_UserForumAccess ELSE out_UserForumAccess END) AS UserForumAccess;
END;

--GO


CREATE PROCEDURE {databaseSchema}.{objectQualifier}vaccess_s_moderatoraccess_list()
READS SQL DATA
BEGIN
DECLARE ici_UserID INT;
DECLARE ici_ForumID INT;
DECLARE ici_ModeratorAccess INT DEFAULT 0;

DECLARE out_UserID INT ;
DECLARE out_ForumID INT;
DECLARE out_ModeratorAccess INT DEFAULT 0;
SELECT DISTINCT
CAST(r.ForumID AS UNSIGNED),
r.UserID,
MAX(r.ModeratorAccess) AS ModeratorAccess,
(SELECT Name FROM  {databaseSchema}.{objectQualifier}User u WHERE u.UserID=r.UserID) AS ModeratorName,
0 AS IsGroup
FROM (SELECT * FROM (SELECT 
ForumID,UserID,ModeratorAccess 
FROM
 {databaseSchema}.{objectQualifier}vaccessfull1  
 WHERE ModeratorAccess<>0 AND AdminGroup = 0) AS t1
UNION
SELECT * FROM (SELECT 
ForumID,UserID,ModeratorAccess
FROM
 {databaseSchema}.{objectQualifier}vaccessfull2  
  WHERE ModeratorAccess<>0 AND AdminGroup = 0) AS t2
  UNION 
 SELECT * FROM (SELECT 
 ForumID,UserID,ModeratorAccess
 FROM
 {databaseSchema}.{objectQualifier}vaccessfull3  
  WHERE ModeratorAccess<>0  AND AdminGroup = 0) AS t3  
   ) r ;
END;
--GO 

 /* Create procedures */
 

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}active_list(
			 IN i_BoardID INT,
			 IN i_Guests  TINYINT(1),
			 IN i_ShowCrawlers  TINYINT(1),
			 IN i_ActiveTime INT,
			 IN i_StyledNicks TINYINT(1),
			 IN i_UTCTIMESTAMP DATETIME)
			 MODIFIES SQL DATA
  BEGIN

	DELETE FROM {databaseSchema}.{objectQualifier}Active 
	WHERE  (LastActive < DATE_SUB(i_UTCTIMESTAMP, INTERVAL i_ActiveTime MINUTE) OR LastActive IS NULL); 
	-- we don't delete guest access
	DELETE FROM {databaseSchema}.{objectQualifier}ActiveAccess 
	WHERE  (LastActive < DATE_SUB(i_UTCTIMESTAMP, INTERVAL i_ActiveTime MINUTE) OR LastActive IS NULL) 
	AND  IsGuestX = 0;
	
				  
	   -- select active non-guest users
	   
		IF i_Guests <> 0 AND i_Guests IS NOT NULL THEN
			  SELECT   a.UserID,
					   a.Name AS UserName,
					   a.DisplayName AS UserDisplayName,
					   c.IP,
					   c.SessionID,
					   c.ForumID,
					   c.TopicID,
						 (SELECT x.Name
						   FROM   {databaseSchema}.{objectQualifier}Forum x
							 WHERE  x.ForumID = c.ForumID) AS ForumName,
						 (SELECT Topic
						   FROM   {databaseSchema}.{objectQualifier}Topic x
							 WHERE  x.TopicID = c.TopicID) AS TopicName,
						 (SELECT 1
						   FROM   {databaseSchema}.{objectQualifier}UserGroup x,
						   {databaseSchema}.{objectQualifier}Group y
							 WHERE  x.UserID = a.UserID
							   AND y.GroupID = x.GroupID
							   AND (y.Flags & 2) <> 0) AS IsGuest,
					   CAST(SIGN(c.Flags & 8) AS SIGNED) AS IsCrawler,		
					   {databaseSchema}.{objectQualifier}biginttobool(CAST(SIGN(c.Flags & 16) AS SIGNED)) AS IsHidden,                      
					  (case(i_StyledNicks)
			when 1 then  a.UserStyle  
			else ''	 end) AS  Style,	 
					  1 AS UserCount,
					  c.Login,
					  c.LastActive,
					  c.Location,
					  c.ForumPage,
					  TIMESTAMPDIFF(MINUTE,IFNULL(c.Login,i_UTCTIMESTAMP),IFNULL(c.LastActive,i_UTCTIMESTAMP))  AS Active,                  
					  c.Browser,
					  c.Platform
			  FROM     {databaseSchema}.{objectQualifier}User a
					   INNER JOIN 
					   {databaseSchema}.{objectQualifier}Active c
					   ON  c.UserID = a.UserID
			  WHERE 
				 c.BoardID = i_BoardID
			  ORDER BY c.LastActive DESC;
	elseif (i_ShowCrawlers = 1 and i_Guests = 0) THEN		
					SELECT   a.UserID,
					   a.Name AS UserName,
					   a.DisplayName AS UserDisplayName,
					   c.IP,
					   c.SessionID,
					   c.ForumID,
					   c.TopicID,
						 (SELECT x.Name
						   FROM   {databaseSchema}.{objectQualifier}Forum x
							 WHERE  x.ForumID = c.ForumID) AS ForumName,
						 (SELECT Topic
						   FROM   {databaseSchema}.{objectQualifier}Topic x
							 WHERE  x.TopicID = c.TopicID) AS TopicName,
						 (SELECT 1
						   FROM   {databaseSchema}.{objectQualifier}UserGroup x,
						   {databaseSchema}.{objectQualifier}Group y
							 WHERE  x.UserID = a.UserID
							   AND y.GroupID = x.GroupID
							   AND (y.Flags & 2) <> 0) AS IsGuest,
					   CAST(SIGN(c.Flags & 8) AS SIGNED) AS IsCrawler,		
						{databaseSchema}.{objectQualifier}biginttobool(CAST(SIGN(c.Flags & 16) AS SIGNED)) AS IsHidden,                      
					  (case(i_StyledNicks)
			when 1 then   a.UserStyle
			else ''	 end) AS  Style,	 
					  1 AS UserCount,
					  c.Login,
					  c.LastActive,
					  c.Location,
					  c.ForumPage,
					  TIMESTAMPDIFF(MINUTE,IFNULL(c.Login,i_UTCTIMESTAMP),IFNULL(c.LastActive,i_UTCTIMESTAMP))  AS Active,                  
					  c.Browser,
					  c.Platform	
			   FROM     {databaseSchema}.{objectQualifier}User a
					   INNER JOIN 
					   {databaseSchema}.{objectQualifier}Active c
					   ON  c.UserID = a.UserID
				  
		where
			c.BoardID = i_BoardID			
			   -- is registered or is crawler 
			   and ((c.Flags & 4) = 4 OR (c.Flags & 8) = 8)	 		   								  
		order by 
			c.LastActive desc;
		ELSE
					SELECT   a.UserID,
					   a.Name AS UserName,
					   a.DisplayName AS UserDisplayName,
					   c.IP,
					   c.SessionID,
					   c.ForumID,
					   c.TopicID,
						 (SELECT x.Name
						   FROM   {databaseSchema}.{objectQualifier}Forum x
							 WHERE  x.ForumID = c.ForumID) AS ForumName,
						 (SELECT Topic
						   FROM   {databaseSchema}.{objectQualifier}Topic x
							 WHERE  x.TopicID = c.TopicID) AS TopicName,
						 (SELECT 1
						   FROM   {databaseSchema}.{objectQualifier}UserGroup x,
						   {databaseSchema}.{objectQualifier}Group y
							 WHERE  x.UserID = a.UserID
							   AND y.GroupID = x.GroupID
							   AND (y.Flags & 2) <> 0) AS IsGuest,
					   CAST(SIGN(c.Flags & 8) AS SIGNED) AS IsCrawler,		
					   {databaseSchema}.{objectQualifier}biginttobool(CAST(SIGN(c.Flags & 16) AS SIGNED)) AS IsHidden,                      
					  (case(i_StyledNicks)
			when 1 then   a.UserStyle  
			else ''	 end) AS  Style,	 
					  1 AS UserCount,
					  c.Login,
					  c.LastActive,
					  c.Location,
					  c.ForumPage,
					  TIMESTAMPDIFF(MINUTE,IFNULL(c.Login,i_UTCTIMESTAMP),IFNULL(c.LastActive,i_UTCTIMESTAMP))  AS Active,                  
					  c.Browser,
					  c.Platform
			   FROM     {databaseSchema}.{objectQualifier}User a
						INNER JOIN 
					   {databaseSchema}.{objectQualifier}Active c
					   ON  c.UserID = a.UserID
			   WHERE    
				 c.BoardID = i_BoardID
				 -- no guests
				 AND NOT EXISTS (SELECT 1
								  FROM   {databaseSchema}.{objectQualifier}UserGroup x,
										 {databaseSchema}.{objectQualifier}Group y
								 WHERE  x.UserID = a.UserID
								  AND y.GroupID = x.GroupID
								  AND (y.Flags & 2) <> 0)
			  ORDER BY c.LastActive DESC;
  END IF;
END;
--GO

create procedure {databaseSchema}.{objectQualifier}active_list_user(i_BoardID INT, i_UserID INT, i_Guests TINYINT(1), i_ShowCrawlers TINYINT(1), i_ActiveTime INT,i_StyledNicks TINYINT(1), i_UTCTIMESTAMP datetime) 
READS SQL DATA
begin                 
	   -- select active non-guest users
	   
		IF (i_Guests <> 0 AND i_Guests IS NOT NULL) THEN
			  SELECT   a.UserID,
					   a.Name AS UserName,
					   a.DisplayName AS UserDisplayName,
					   c.IP,
					   c.SessionID,
					   c.ForumID,
					   x.ReadAccess AS HasForumAccess,		
					   c.TopicID,
						 (SELECT x.Name
						   FROM   {databaseSchema}.{objectQualifier}Forum x
							 WHERE  x.ForumID = c.ForumID) AS ForumName,
						 (SELECT Topic
						   FROM   {databaseSchema}.{objectQualifier}Topic x
							 WHERE  x.TopicID = c.TopicID) AS TopicName,
						 (SELECT 1
						   FROM   {databaseSchema}.{objectQualifier}UserGroup x,
						   {databaseSchema}.{objectQualifier}Group y
							 WHERE  x.UserID = a.UserID
							   AND y.GroupID = x.GroupID
							   AND (y.Flags & 2) <> 0) AS IsGuest,
						IFNULL(SIGN(a.Flags & 8)>0,false) AS IsCrawler,
						IFNULL(SIGN(a.Flags & 16)>0,false) AS IsHidden,
					  (case(i_StyledNicks)
			when 1 then   a.UserStyle
			else ''	 end) AS  Style,	 
					  1 AS UserCount,
					  c.Login,
					  c.LastActive,
					  c.Location,
					  c.ForumPage,
						CAST(ROUND((c.LastActive-
						c.Login)/60) AS SIGNED) AS Active,
					  c.Browser,
					  c.Platform
			  FROM     {databaseSchema}.{objectQualifier}User a
					   INNER JOIN 
					   {databaseSchema}.{objectQualifier}Active c 
					   ON c.UserID = a.UserID
					   INNER JOIN {databaseSchema}.{objectQualifier}ActiveAccess x
					   ON (x.ForumID = IFNULL(c.ForumID,0))	
			  WHERE  
				 c.BoardID = i_BoardID  AND x.UserID = i_UserID	
			  ORDER BY c.LastActive DESC;
		ELSEIF (i_ShowCrawlers = 1 and i_Guests = 0) THEN
					  SELECT   a.UserID,
					   a.Name AS UserName,
					   a.DisplayName AS UserDisplayName,
					   c.IP,
					   c.SessionID,
					   c.ForumID,
					   x.ReadAccess AS HasForumAccess,			
					   c.TopicID,
						 (SELECT x.Name
						   FROM   {databaseSchema}.{objectQualifier}Forum x
							 WHERE  x.ForumID = c.ForumID) AS ForumName,
						 (SELECT Topic
						   FROM   {databaseSchema}.{objectQualifier}Topic x
							 WHERE  x.TopicID = c.TopicID) AS TopicName,
						 (SELECT 1
						   FROM   {databaseSchema}.{objectQualifier}UserGroup x,
						   {databaseSchema}.{objectQualifier}Group y
							 WHERE  x.UserID = a.UserID
							   AND y.GroupID = x.GroupID
							   AND (y.Flags & 2) <> 0) AS IsGuest,
						IFNULL(SIGN(a.Flags & 8)>0,false) AS IsCrawler,
						IFNULL(SIGN(a.Flags & 16)>0,false) AS IsHidden,
					  (case(i_StyledNicks)
			when 1 then   a.UserStyle 
			else ''	 end) AS  Style,	 
					  1 AS UserCount,
					  c.Login,
					  c.LastActive,
					  c.Location,
					  c.ForumPage,
						CAST(ROUND((c.LastActive-
						c.Login)/60) AS SIGNED) AS Active,
					  c.Browser,
					  c.Platform
			  FROM     {databaseSchema}.{objectQualifier}User a
					   INNER JOIN
					   {databaseSchema}.{objectQualifier}Active c
						ON c.UserID = a.UserID
					   INNER JOIN {databaseSchema}.{objectQualifier}ActiveAccess x
					   ON (x.ForumID = IFNULL(c.ForumID,0))	
			  WHERE   c.BoardID = i_BoardID AND x.UserID = i_UserID	and   
			   -- is registered or is crawler 
				((c.Flags & 4) = 4 OR (c.Flags & 8) = 8)	 

			  ORDER BY c.LastActive DESC;	
		ELSE
			  SELECT   a.UserID,
					   a.Name AS UserName,
					   a.DisplayName AS UserDisplayName,
					   c.IP,
					   c.SessionID,
					   c.ForumID,
					   x.ReadAccess AS HasForumAccess,			
					   c.TopicID,
						 (SELECT x.Name
						   FROM   {databaseSchema}.{objectQualifier}Forum x
							 WHERE  x.ForumID = c.ForumID) AS ForumName,
						 (SELECT Topic
						   FROM   {databaseSchema}.{objectQualifier}Topic x
							 WHERE  x.TopicID = c.TopicID) AS TopicName,
						 (SELECT 1
						   FROM   {databaseSchema}.{objectQualifier}UserGroup x,
						   {databaseSchema}.{objectQualifier}Group y
							 WHERE  x.UserID = a.UserID
							   AND y.GroupID = x.GroupID
							   AND (y.Flags & 2) <> 0) AS IsGuest,
						IFNULL(SIGN(a.Flags & 8)>0,false) AS IsCrawler,
						IFNULL(SIGN(a.Flags & 16)>0,false) AS IsHidden,
					  (case(i_StyledNicks)
			when 1 then   a.UserStyle  
			else ''	 end) AS  Style,	 
					  1 AS UserCount,
					  c.Login,
					  c.LastActive,
					  c.Location,
					  c.ForumPage,
						CAST(ROUND((c.LastActive-
						c.Login)/60) AS SIGNED) AS Active,
					  c.Browser,
					  c.Platform
			  FROM     {databaseSchema}.{objectQualifier}User a
					   INNER JOIN
					   {databaseSchema}.{objectQualifier}Active c
						ON c.UserID = a.UserID
					   INNER JOIN {databaseSchema}.{objectQualifier}ActiveAccess x
					   ON (x.ForumID = IFNULL(c.ForumID,0))	
			  WHERE   c.BoardID = i_BoardID AND x.UserID = i_UserID
				 AND NOT EXISTS (SELECT 1
								  FROM   {databaseSchema}.{objectQualifier}UserGroup x,
										 {databaseSchema}.{objectQualifier}Group y
								 WHERE  x.UserID = a.UserID
								  AND y.GroupID = x.GroupID
								  AND (y.Flags & 2) <> 0)
			  ORDER BY c.LastActive DESC;
  END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}active_listforum(
IN i_ForumID INT, IN i_StyledNicks TINYINT(1))
READS SQL DATA
BEGIN
SELECT
a.UserID AS UserID,
b.Name AS UserName,
b.DisplayName AS UserDisplayName,
IFNULL(CAST(SIGN(b.Flags & 16) AS SIGNED),false) AS IsHidden,
IFNULL(CAST(SIGN(b.Flags & 8) AS SIGNED),false) AS IsCrawler,
(case(i_StyledNicks)
			when 1 then   b.UserStyle  
			else ''	 end) AS  Style,
(SELECT COUNT(ac.UserID) from
		{databaseSchema}.{objectQualifier}Active ac  where ac.UserID = a.UserID and ac.ForumID = i_ForumID) AS UserCount ,
		Browser = a.Browser 
FROM     {databaseSchema}.{objectQualifier}Active a
JOIN {databaseSchema}.{objectQualifier}User b
ON b.UserID = a.UserID
WHERE    a.ForumID = i_ForumID
GROUP BY
a.UserID,
b.DisplayName,
b.Name,
IsHidden,
IsCrawler,
a.Flags,
a.Browser,
Style
ORDER BY b.Name;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}active_listtopic(
IN i_TopicID INT, IN i_StyledNicks TINYINT(1))
READS SQL DATA
BEGIN

SELECT    a.UserID AS UserID,
b.Name AS UserName,
b.DisplayName AS UserDisplayName,
IFNULL(CAST(SIGN(b.Flags & 16) AS SIGNED),false) AS IsHidden,
IFNULL(CAST(SIGN(b.Flags & 8) AS SIGNED),false) AS IsCrawler,
 (case(i_StyledNicks)
			when 1 then   b.UserStyle  
			else ''	 end) AS  Style,

	   (SELECT COUNT(ac.UserID) from
		{databaseSchema}.{objectQualifier}Active ac  
		where ac.UserID = a.UserID and ac.TopicID = i_TopicID) AS UserCount,
		a.Browser AS  Browser
FROM     {databaseSchema}.{objectQualifier}Active a
JOIN {databaseSchema}.{objectQualifier}User b
ON b.UserID = a.UserID
WHERE    a.TopicID = i_TopicID
GROUP BY
a.UserID,
b.DisplayName,
b.Name,
IsHidden,
IsCrawler,
a.Flags,
a.Browser,
Style
ORDER BY b.Name;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}active_stats(
i_BoardID INT)
READS SQL DATA
BEGIN
DECLARE ActiveUsers SMALLINT DEFAULT 0 ;
DECLARE ActiveGuests SMALLINT DEFAULT 0;
DECLARE ActiveMembers SMALLINT DEFAULT 0;
DECLARE ActiveHidden SMALLINT DEFAULT 0;
SELECT COUNT(x.SessionID) INTO ActiveUsers
FROM {databaseSchema}.{objectQualifier}Active x
JOIN {databaseSchema}.{objectQualifier}UserSelectView usr
ON x.UserID = usr.UserID
WHERE x.BoardID = i_BoardID
AND usr.IsActiveExcluded = 0 OR usr.IsActiveExcluded IS NULL;
SELECT count(x.SessionID) INTO ActiveMembers
FROM {databaseSchema}.{objectQualifier}Active x
JOIN {databaseSchema}.{objectQualifier}UserSelectView usr
ON x.UserID = usr.UserID
WHERE x.BoardID = i_BoardID
AND EXISTS(select 1
from {databaseSchema}.{objectQualifier}UserGroup y
inner join {databaseSchema}.{objectQualifier}Group z
on y.GroupID=z.GroupID
where y.UserID=x.UserID
and (z.Flags & 2)=0
AND usr.IsActiveExcluded = 0 OR usr.IsActiveExcluded IS NULL );
SELECT count(x.SessionID) INTO  ActiveGuests
from {databaseSchema}.{objectQualifier}Active x
WHERE x.BoardID = i_BoardID
AND EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}UserGroup y
inner join {databaseSchema}.{objectQualifier}Group z
on y.GroupID=z.GroupID
WHERE y.UserID=x.UserID
and (z.Flags & 2)<>0);
SELECT count(x.SessionID) INTO ActiveHidden
from {databaseSchema}.{objectQualifier}Active x
JOIN {databaseSchema}.{objectQualifier}UserSelectView usr
ON x.UserID = usr.UserID
WHERE x.BoardID = i_BoardID
AND EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}UserGroup y
inner join {databaseSchema}.{objectQualifier}Group z
on y.GroupID=z.GroupID
WHERE y.UserID=x.UserID
and (z.Flags & 2)=0  
AND usr.IsActiveExcluded = 1);
SELECT ActiveUsers, ActiveGuests, ActiveMembers,ActiveHidden;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}active_updatemaxstats
(
	i_BoardID		INT, i_UTCTIMESTAMP DATETIME
) 
MODIFIES SQL DATA
BEGIN
  DECLARE ici_count int;
  DECLARE ici_max int;
  DECLARE ici_maxStr VARCHAR(255);

-- SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
SET ici_count = IFNULL((SELECT COUNT(DISTINCT CAST(IP AS CHAR(37))+ CAST(UserID AS CHAR(10)))
FROM   {databaseSchema}.{objectQualifier}Active a
WHERE  a.BoardID = i_BoardID ORDER BY UserID LIMIT 1),0);

/* Here we find current max users value and transform it to integer
SET TRANSACTION ISOLATION LEVEL READ COMMITTED; */

SET ici_maxStr = IFNULL((SELECT CAST(`VALUE` AS CHAR) 
FROM   {databaseSchema}.{objectQualifier}Registry
WHERE  BoardID = i_BoardID
AND `Name` = CONVERT('maxusers' USING {databaseEncoding})),'1');

SET ici_max =(SELECT CAST(ici_maxStr AS SIGNED));
/* Here we transform the rest */

	

	IF NOT EXISTS ( SELECT 1 FROM {databaseSchema}.{objectQualifier}Registry 
	WHERE BoardID = i_BoardID and `Name` = CONVERT('maxusers' USING {databaseEncoding}) ) THEN
	INSERT INTO {databaseSchema}.{objectQualifier}Registry
(BoardID,
`Name`,
`VALUE`)
VALUES     (i_BoardID,
CONVERT('maxusers' USING {databaseEncoding}),
CAST(ici_count AS CHAR));
INSERT INTO {databaseSchema}.{objectQualifier}Registry
(BoardID,
`Name`,
`VALUE`)
VALUES     (i_BoardID,
CONVERT('maxuserswhen' USING {databaseEncoding}),
CAST(i_UTCTIMESTAMP AS CHAR));
	
	ELSEIF (ici_count > ici_max)	THEN
	
		/* In the case we of course simply update 2 registry values*/
UPDATE {databaseSchema}.{objectQualifier}Registry
SET    `VALUE` = CAST(ici_count AS CHAR)
WHERE  BoardID = i_BoardID
AND `Name` = CONVERT('maxusers' USING {databaseEncoding});

UPDATE {databaseSchema}.{objectQualifier}Registry
SET    `VALUE` = CAST(i_UTCTIMESTAMP AS CHAR)
WHERE  BoardID = i_BoardID
AND `Name` = CONVERT('maxuserswhen' USING {databaseEncoding});
	END  IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}attachment_delete(
i_AttachmentID INT)
MODIFIES SQL DATA
BEGIN
DELETE FROM {databaseSchema}.{objectQualifier}Attachment
WHERE       AttachmentID = i_AttachmentID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}attachment_download(
i_AttachmentID INT)
MODIFIES SQL DATA
BEGIN
UPDATE {databaseSchema}.{objectQualifier}Attachment
SET    Downloads = Downloads + 1
WHERE  AttachmentID = i_AttachmentID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}attachment_list(
i_MessageID    INT,
i_AttachmentID INT,
i_BoardID      INT,  
i_PageIndex int,
i_PageSize int)
READS SQL DATA
BEGIN
   declare ici_TotalRows int ;
   declare ici_FirstSelectRowNumber int ;
   declare ici_FirstSelectRowID int;	
   IF i_MessageID IS NOT NULL THEN
	 select 
			a.*,
			e.BoardID
		from
			{databaseSchema}.{objectQualifier}Attachment a
			inner join {databaseSchema}.{objectQualifier}Message b on b.MessageID = a.MessageID
			inner join {databaseSchema}.{objectQualifier}Topic c on c.TopicID = b.TopicID
			inner join {databaseSchema}.{objectQualifier}Forum d on d.ForumID = c.ForumID
			inner join {databaseSchema}.{objectQualifier}Category e on e.CategoryID = d.CategoryID
			inner join {databaseSchema}.{objectQualifier}Board brd on brd.BoardID = e.BoardID
		where
			a.MessageID=i_MessageID;
   ELSEIF i_AttachmentID IS NOT NULL THEN
	 select 
			a.*,
			e.BoardID
		from
			{databaseSchema}.{objectQualifier}Attachment a
			inner join {databaseSchema}.{objectQualifier}Message b on b.MessageID = a.MessageID
			inner join {databaseSchema}.{objectQualifier}Topic c on c.TopicID = b.TopicID
			inner join {databaseSchema}.{objectQualifier}Forum d on d.ForumID = c.ForumID
			inner join {databaseSchema}.{objectQualifier}Category e on e.CategoryID = d.CategoryID
			inner join {databaseSchema}.{objectQualifier}Board brd on brd.BoardID = e.BoardID
		where 
			a.AttachmentID=i_AttachmentID;
   ELSE
   set i_PageIndex = i_PageIndex + 1;  	
	
	
		select  count(1) into  ici_TotalRows from 
			{databaseSchema}.{objectQualifier}Attachment a
			inner join {databaseSchema}.{objectQualifier}Message b on b.MessageID = a.MessageID
			inner join {databaseSchema}.{objectQualifier}Topic c on c.TopicID = b.TopicID
			inner join {databaseSchema}.{objectQualifier}Forum d on d.ForumID = c.ForumID
			inner join {databaseSchema}.{objectQualifier}Category e on e.CategoryID = d.CategoryID
		where
			e.BoardID = i_BoardID;
	
		 select  (i_PageIndex - 1) * i_PageSize into ici_FirstSelectRowNumber;
	   
	set @alpst = CONCAT('select 
			a.*,
			',i_BoardID,' AS BoardID,						
			b.`Posted` AS Posted,
			d.`ForumID` AS ForumID,
			d.`Name` AS ForumName,
			c.`TopicID` AS TopicID,
			c.`Topic` AS TopicName,
			{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
		from 
			{databaseSchema}.{objectQualifier}Attachment a
			inner join {databaseSchema}.{objectQualifier}Message b on b.MessageID = a.MessageID
			inner join {databaseSchema}.{objectQualifier}Topic c on c.TopicID = b.TopicID
			inner join {databaseSchema}.{objectQualifier}Forum d on d.ForumID = c.ForumID
			inner join {databaseSchema}.{objectQualifier}Category e on e.CategoryID = d.CategoryID
		where  e.BoardID = ',i_BoardID,' 
	  order by a.AttachmentID  LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');

	   PREPARE stmt_els FROM @alpst;

	EXECUTE stmt_els;

	DEALLOCATE PREPARE stmt_els;
			
   END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}attachment_save(
i_MessageID   INT,
i_FileName    VARCHAR(128),
i_Bytes       INT,
i_ContentType VARCHAR(128) ,
i_FileData    LONGBLOB)
MODIFIES SQL DATA
BEGIN

INSERT INTO {databaseSchema}.{objectQualifier}Attachment
(MessageID,
FileName,
Bytes,
ContentType,
Downloads,
FileData)
VALUES     (i_MessageID,
i_FileName,
i_Bytes,
i_ContentType,
0,
i_FileData);
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}bannedip_delete(
i_ID INT)
MODIFIES SQL DATA
BEGIN
DELETE FROM {databaseSchema}.{objectQualifier}BannedIP
WHERE       ID = i_ID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}bannedip_list(
i_BoardID INT,
i_ID      INT,  
i_PageIndex int,
i_PageSize int)
READS SQL DATA
BEGIN
  declare ici_TotalRows int ;
   declare ici_FirstSelectRowNumber int ;
   declare ici_FirstSelectRowID int;	  
DECLARE ici_ID INT DEFAULT NULL;

IF i_ID IS NOT NULL THEN SET ici_ID=i_ID;END IF;
IF ici_ID IS NULL THEN
 set i_PageIndex = i_PageIndex + 1;  	
	
	
		select  count(1) into  ici_TotalRows FROM   {databaseSchema}.{objectQualifier}BannedIP
WHERE  BoardID = i_BoardID;
	
		 select  (i_PageIndex - 1) * i_PageSize into ici_FirstSelectRowNumber;
	   
   set @biplpr = CONCAT('select
		b.*,
		{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
	  FROM   {databaseSchema}.{objectQualifier}BannedIP b
	  WHERE  b.BoardID = ',i_BoardID,'
	  order by b.Mask  LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');
	PREPARE stmt_els FROM @biplpr;
	EXECUTE stmt_els;
	-- show global status like 'com_stmt%';
	-- show session VARIABLES like 'max_prepared_stmt_count' 
	-- Com_stmt_close  0   times prepared statements closed
   -- Com_stmt_execute  43795   times prepared statements executed
   -- Com_stmt_fetch  0 
   -- Com_stmt_prepare  34159  times prepared statements created
  -- Com_stmt_reprepare  41
   -- Com_stmt_reset  0 
   -- Com_stmt_send_long_data  0 
if ((SELECT variable_value + 1000
	  FROM information_schema.global_status  where variable_name like 'com_stmt_prepare') >= 
	  (select @@global.max_prepared_stmt_count)) then   
   DEALLOCATE PREPARE stmt_els;
   end if; 
	ELSE
SELECT *
FROM   {databaseSchema}.{objectQualifier}BannedIP
WHERE  BoardID = i_BoardID
AND ID = ici_ID;
END IF;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}bannedip_save(
i_ID      INT,
i_BoardID INT,
i_Mask    VARCHAR(57),
i_Reason VARCHAR(128),
i_UserID INT,
i_UTCTIMESTAMP DATETIME)
MODIFIES SQL DATA
BEGIN
DECLARE ici_ID INT DEFAULT NULL;

IF i_ID IS NOT NULL THEN SET ici_ID=i_ID;
END IF;

IF ici_ID IS NULL OR ici_ID = 0 THEN
BEGIN
INSERT INTO {databaseSchema}.{objectQualifier}BannedIP
(BoardID,
Mask,
Since,
Reason,
UserID)
VALUES     (i_BoardID,
i_Mask,
i_UTCTIMESTAMP,
i_Reason,
i_UserID);
END;
ELSE
BEGIN
UPDATE {databaseSchema}.{objectQualifier}BannedIP
SET    Mask = i_Mask, Reason = i_Reason, UserID = i_UserID
WHERE  ID = ici_ID;
END;
END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}bbcode_delete
 (
	i_BBCodeID INT
 )
 MODIFIES SQL DATA
 BEGIN
	IF i_BBCodeID IS NOT NULL THEN
		DELETE FROM {databaseSchema}.{objectQualifier}BBCode WHERE BBCodeID = i_BBCodeID;
	ELSE
		DELETE FROM {databaseSchema}.{objectQualifier}BBCode;
   END IF;
 END;
--GO

 /* STORED PROCEDURE CREATED BY VZ-TEAM */
 
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}bbcode_list
 (
	i_BoardID INT,
	i_BBCodeID INT
 )
 READS SQL DATA
 BEGIN
	IF i_BBCodeID IS NULL THEN
		SELECT * FROM {databaseSchema}.{objectQualifier}BBCode a  WHERE a.BoardID = i_BoardID ORDER BY a.ExecOrder, a.`Name` DESC;
	ELSE
		SELECT * FROM {databaseSchema}.{objectQualifier}BBCode b WHERE b.BBCodeID = i_BBCodeID ORDER BY b.ExecOrder;
   END IF; 
 END;
--GO


 /* STORED PROCEDURE CREATED BY VZ-TEAM */

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}bbcode_save
 (
	i_BBCodeID INT,
	i_BoardID INT,
	i_Name VARCHAR(255),
	i_Description VARCHAR(4000),
	i_OnClickJS VARCHAR(1000),
	i_DisplayJS TEXT,
	i_EditJS TEXT,
	i_DisplayCSS TEXT,
	i_SearchRegEx TEXT,
	i_ReplaceRegEx TEXT,
	i_Variables VARCHAR(1000),
	i_UseModule TINYINT(1),
	i_ModuleClass VARCHAR(255),	
	i_ExecOrder INT
 )
MODIFIES SQL DATA
 BEGIN
	IF i_BBCodeID IS NOT NULL THEN
		
		UPDATE
			{databaseSchema}.{objectQualifier}BBCode
		SET
			`Name` = i_Name,
			`Description` = i_Description,
			`OnClickJS` = i_OnClickJS,
			`DisplayJS` = i_DisplayJS,
			`EditJS` = i_EditJS,
			`DisplayCSS` = i_DisplayCSS,
			`SearchRegEx` = i_SearchRegEx,
			`ReplaceRegEx` = i_ReplaceRegEx,
			`Variables` = i_Variables,
			`UseModule` = i_UseModule,
			`ModuleClass` = i_ModuleClass,			
			`ExecOrder` = i_ExecOrder
		WHERE
			BBCodeID = i_BBCodeID;
	
	ELSE 
		IF NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}BBCode WHERE BoardID = i_BoardID AND `Name` = i_Name) THEN
			INSERT INTO
				{databaseSchema}.{objectQualifier}BBCode (`BoardID`,`Name`,`Description`,`OnClickJS`,`DisplayJS`,`EditJS`,`DisplayCSS`,`SearchRegEx`,`ReplaceRegEx`,`Variables`,`UseModule`,`ModuleClass`,`ExecOrder`)
			VALUES (i_BoardID,i_Name,i_Description,i_OnClickJS,i_DisplayJS,i_EditJS,i_DisplayCSS,i_SearchRegEx,i_ReplaceRegEx,i_Variables,i_UseModule,i_ModuleClass,i_ExecOrder);
	END IF; 
	END IF;
 END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}board_create(
	i_BoardName 		VARCHAR(128),
	i_Culture           VARCHAR(10),
	i_LanguageFile   	VARCHAR(128),
	i_MembershipAppName VARCHAR(128),
	i_RolesAppName      VARCHAR(128),
	i_UserName		    VARCHAR(255),
	i_UserEmail		    VARCHAR(255),
	i_UserKey		    VARCHAR(64), 
	i_IsHostAdmin	    TINYINT(1),
	i_RolePrefix VARCHAR(128),
	i_UTCTIMESTAMP DATETIME
 ) 
 MODIFIES SQL DATA
 BEGIN
	DECLARE l_BoardID			    INT;
	DECLARE l_TimeZone			    INT;
	DECLARE l_ForumEmail			VARCHAR(128);
	DECLARE l_GroupIDAdmin			INT;
	DECLARE l_GroupIDGuest			INT;
	DECLARE l_GroupIDMember			INT;
	DECLARE l_AccessMaskIDAdmin		INT;
	DECLARE l_AccessMaskIDModerator	INT;
	DECLARE l_AccessMaskIDMember	INT;
	DECLARE l_AccessMaskIDReadOnly	INT;
	DECLARE l_UserIDAdmin			INT;
	DECLARE l_UserIDGuest			INT;
	DECLARE l_RankIDAdmin			INT;
	DECLARE l_RankIDGuest			INT;
	DECLARE l_RankIDNewbie			INT;
	DECLARE l_RankIDMember			INT;
	DECLARE l_RankIDAdvanced		INT;
	DECLARE l_CategoryID			INT;
	DECLARE l_ForumID			    INT;
	DECLARE l_UserFlags			    INT;
 

		SELECT CAST(`Value` AS SIGNED)
						 INTO l_TimeZone
						 FROM   {databaseSchema}.{objectQualifier}Registry
						 WHERE  Lower(`Name`) = Lower('TimeZone');
		SELECT CAST(`Value` AS CHAR(128))
						   INTO l_ForumEmail			
						   FROM   {databaseSchema}.{objectQualifier}Registry
						   WHERE  Lower(`Name`) = Lower('ForumEmail');
 
	 /*Board 
		SET FOREIGN_KEY_CHECKS =0;*/
		INSERT INTO {databaseSchema}.{objectQualifier}Board
				   (`Name`, AllowThreaded, MembershipAppName, RolesAppName )
		VALUES     (i_BoardName,0, i_MembershipAppName, i_RolesAppName);
		/*SET FOREIGN_KEY_CHECKS =1;*/
		SET l_BoardID = LAST_INSERT_ID();
		
		CALL {databaseSchema}.{objectQualifier}registry_save('culture',i_Culture,l_BoardID);
		CALL {databaseSchema}.{objectQualifier}registry_save('language',i_LanguageFile,l_BoardID);	
		
	 /*Rank*/
		INSERT INTO {databaseSchema}.{objectQualifier}Rank
				   (BoardID,
					`Name`,
					Flags,
					MinPosts,
					PMLimit,
					Style,
					SortOrder
					)
		VALUES     (l_BoardID,
					'Administration',
					0,
					NULL,
					0,
					'',
					2);
		
		SET l_RankIDAdmin = LAST_INSERT_ID();
		INSERT INTO {databaseSchema}.{objectQualifier}Rank
				   (BoardID,
					`Name`,
					Flags,
					MinPosts,
					PMLimit,
					Style,
					SortOrder)
		VALUES     (l_BoardID,
					'Guest',
					4,
					NULL,
					0,
					'',
					2);
		
		SET l_RankIDGuest = LAST_INSERT_ID();
		INSERT INTO {databaseSchema}.{objectQualifier}Rank
				   (BoardID,
					`Name`,
					Flags,
					MinPosts,
					PMLimit,
					Style,
					SortOrder)
		VALUES     (l_BoardID,
					'Newbie',
					3,
					0,
					0,
					'',
					2);
		
		SET l_RankIDNewbie = LAST_INSERT_ID();
		INSERT INTO {databaseSchema}.{objectQualifier}Rank
				   (BoardID,
					`Name`,
					Flags,
					MinPosts,
					PMLimit,
					Style,
					SortOrder)
		VALUES     (l_BoardID,
					'Member',
					2,
					10,
					0,
					'',
					2);
		
		SET l_RankIDMember = LAST_INSERT_ID();
		INSERT INTO {databaseSchema}.{objectQualifier}Rank
				   (BoardID,
					`Name`,
					Flags,
					MinPosts,
					PMLimit,
					Style,
					SortOrder)
		VALUES     (l_BoardID,
					'Advanced Member',
					2,
					30,
					0,
					'',
					2);
		
		SET l_RankIDAdvanced =  LAST_INSERT_ID();
 
	/*AccessMask*/
	INSERT INTO {databaseSchema}.{objectQualifier}AccessMask(BoardID,`Name`,Flags, SortOrder)
	VALUES(l_BoardID,'Admin Access',1023 + 1024,4);
	SET l_AccessMaskIDAdmin = LAST_INSERT_ID();

	INSERT INTO {databaseSchema}.{objectQualifier}AccessMask(BoardID,`Name`,Flags, SortOrder)
	VALUES(l_BoardID,'Moderator Access',487 + 1024,3);
	SET l_AccessMaskIDModerator = LAST_INSERT_ID();

	INSERT INTO {databaseSchema}.{objectQualifier}AccessMask(BoardID,`Name`,Flags, SortOrder)
	VALUES(l_BoardID,'Member Access',423 + 1024,2);
	SET l_AccessMaskIDMember = LAST_INSERT_ID();

	INSERT INTO {databaseSchema}.{objectQualifier}AccessMask(BoardID,`Name`,Flags, SortOrder)
	VALUES(l_BoardID,'Read Only Access',1,1);
	SET l_AccessMaskIDReadOnly = LAST_INSERT_ID();

	INSERT INTO {databaseSchema}.{objectQualifier}AccessMask(BoardID,`Name`,Flags, SortOrder)
	VALUES(l_BoardID,'No Access',0,0);

   
 
	 /*Group*/
	INSERT INTO {databaseSchema}.{objectQualifier}Group(BoardID,`Name`,Flags,PMLimit,Style,SortOrder,UsrSigChars,UsrSigBBCodes,UsrAlbums,UsrAlbumImages,IsUserGroup,IsHidden,
	UsrPersonalForums,UsrPersonalGroups,UsrPersonalMasks) 
	values(l_BoardID, CONCAT(COALESCE(i_RolePrefix,''), 'Administrators'),1, 2147483647,'default!font-size: 8pt; color: red/flatearth!font-size: 8pt; color:blue',0,256,'URL,IMG,SPOILER,QUOTE',10,120,0,0,
	0,0,0);
	set l_GroupIDAdmin = LAST_INSERT_ID();
	INSERT INTO {databaseSchema}.{objectQualifier}Group(BoardID,`Name`,Flags,PMLimit,Style,SortOrder,UsrSigChars,UsrSigBBCodes,UsrAlbums,UsrAlbumImages,IsUserGroup,IsHidden,
	UsrPersonalForums,UsrPersonalGroups,UsrPersonalMasks) 
	values(l_BoardID,'Guests',2,0,'',1,0,null,0,0,0,0,
	0,0,0);
	SET l_GroupIDGuest = LAST_INSERT_ID();
	INSERT INTO {databaseSchema}.{objectQualifier}Group(BoardID,`Name`,Flags,PMLimit,Style,SortOrder,UsrSigChars,UsrSigBBCodes,UsrAlbums,UsrAlbumImages,IsUserGroup,IsHidden,
	UsrPersonalForums,UsrPersonalGroups,UsrPersonalMasks) 
	values(l_BoardID, CONCAT(COALESCE(i_RolePrefix,''),'Registered'),4,30,'',2,128,'URL,IMG,SPOILER,QUOTE',5,30,0,0,
	0,0,0);
	SET l_GroupIDMember = LAST_INSERT_ID();	
	
	-- User (GUEST)
	INSERT INTO {databaseSchema}.{objectQualifier}User(BoardID,RankID,`Name`,`DisplayName`,Password,Joined,LastVisit,NumPosts,TimeZone,Email,Flags)
	VALUES(l_BoardID,l_RankIDGuest,'Guest','Guest','na', i_UTCTIMESTAMP,i_UTCTIMESTAMP,0,l_TimeZone,l_ForumEmail,6);
	SET l_UserIDGuest = LAST_INSERT_ID();	
	
	SET l_UserFlags = 2;
	IF i_IsHostAdmin<>0 THEN SET l_UserFlags = 3; END IF;

	-- User (ADMIN)
	INSERT INTO {databaseSchema}.{objectQualifier}User(BoardID,RankID,`Name`,`DisplayName`,Password, Email,ProviderUserKey, Joined,LastVisit,NumPosts,TimeZone,Flags,Points)
	VALUES(l_BoardID,l_RankIDAdmin,i_UserName,i_UserName,'na',i_UserEmail,i_UserKey,i_UTCTIMESTAMP,i_UTCTIMESTAMP,0,l_TimeZone,l_UserFlags,1);
	SET l_UserIDAdmin = LAST_INSERT_ID();

	-- update all groups that they were created by admin
	update {databaseSchema}.{objectQualifier}Group
	set    CreatedByUserID = l_UserIDAdmin,
		   CreatedByUserName = i_UserName,
		   CreatedByUserDisplayName = i_UserName,
		   CreatedDate = i_UTCTIMESTAMP;

  -- UserGroup
  INSERT INTO {databaseSchema}.{objectQualifier}UserGroup(UserID,GroupID) VALUES(l_UserIDAdmin,l_GroupIDAdmin);
  INSERT INTO {databaseSchema}.{objectQualifier}UserGroup(UserID,GroupID) VALUES(l_UserIDGuest,l_GroupIDGuest);

  -- Category
  INSERT INTO {databaseSchema}.{objectQualifier}Category(BoardID,`Name`,SortOrder) VALUES(l_BoardID,'Test Category',1);
  set l_CategoryID = LAST_INSERT_ID();

  /* Forum */
  INSERT INTO {databaseSchema}.{objectQualifier}Forum(CategoryID,`Name`,Description,SortOrder,NumTopics,NumPosts,Flags, left_key, right_key, `level`)
  VALUES(l_CategoryID,'Test Forum','A test forum',1,0,0,4,1,2,0);
  SET l_ForumID = LAST_INSERT_ID();
  /* ForumAccess */
  INSERT INTO {databaseSchema}.{objectQualifier}ForumAccess(GroupID,ForumID,AccessMaskID) VALUES(l_GroupIDAdmin,l_ForumID,l_AccessMaskIDAdmin);
  INSERT INTO {databaseSchema}.{objectQualifier}ForumAccess(GroupID,ForumID,AccessMaskID) VALUES(l_GroupIDGuest,l_ForumID,l_AccessMaskIDReadOnly);
  INSERT INTO {databaseSchema}.{objectQualifier}ForumAccess(GroupID,ForumID,AccessMaskID) VALUES(l_GroupIDMember,l_ForumID,l_AccessMaskIDMember);
	 
  SELECT l_BoardID;
  
  END;
--GO

  /* STORED PROCEDURE CREATED BY VZ-TEAM */
  CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_delete(
  i_BoardID INT)
  MODIFIES SQL DATA
  BEGIN
   
  DECLARE  itmpForumID INT;
  
  DECLARE board_cursor CURSOR  FOR
  SELECT   ForumID
  FROM     {databaseSchema}.{objectQualifier}Forum a
  JOIN {databaseSchema}.{objectQualifier}Category b
  ON a.CategoryID = b.CategoryID
  WHERE    b.BoardID = i_BoardID
  ORDER BY ForumID DESC;


  OPEN board_cursor;
   BEGIN
   DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
   LOOP
  FETCH board_cursor  INTO itmpForumID ;
  
  CALL {databaseSchema}.{objectQualifier}forum_delete(itmpForumID, 0, 0);
  END LOOP; 
  END;
  CLOSE board_cursor;
  /*DEALLOCATE board_cursor;*/

  DELETE FROM {databaseSchema}.{objectQualifier}ForumAccess
  WHERE       EXISTS (SELECT 1
  FROM   {databaseSchema}.{objectQualifier}Group x
  WHERE  x.GroupID = {databaseSchema}.{objectQualifier}ForumAccess.GroupID
  AND x.BoardID = i_BoardID);
  DELETE FROM {databaseSchema}.{objectQualifier}Forum
  WHERE       EXISTS (SELECT 1
  FROM   {databaseSchema}.{objectQualifier}Category x
  WHERE  x.CategoryID = {databaseSchema}.{objectQualifier}Forum.CategoryID
  AND x.BoardID = i_BoardID);
 

  DELETE FROM {databaseSchema}.{objectQualifier}UserGroup
  WHERE       EXISTS (SELECT 1
  FROM   {databaseSchema}.{objectQualifier}User x
  WHERE  x.UserID = {databaseSchema}.{objectQualifier}UserGroup.UserID
  AND x.BoardID = i_BoardID);



  DELETE FROM {databaseSchema}.{objectQualifier}Category
  WHERE       BoardID = i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}AccessMask
  WHERE       BoardID = i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}ActiveAccess
  WHERE       BoardID = i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Active
  WHERE       BoardID = i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}User
  WHERE       BoardID = i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Rank
  WHERE       BoardID = i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Group
  WHERE       BoardID = i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Extension where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}BBCode where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}ShoutboxMessage where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Medal where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Smiley where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Extension where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Replace_Words where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}NntpServer where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}BannedIP where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Registry where BoardID= i_BoardID;
  DELETE FROM {databaseSchema}.{objectQualifier}Board
  WHERE       BoardID = i_BoardID;
  END;
--GO


  /* STORED PROCEDURE CREATED BY VZ-TEAM */

  CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_list(
  i_BoardID INT)
  READS SQL DATA
  BEGIN
  DECLARE ici_BoardID INT DEFAULT NULL;

  IF i_BoardID IS NOT NULL THEN SET ici_BoardID=i_BoardID; END IF;
  IF ici_BoardID IS NULL THEN
  SELECT a.*,
  CONCAT('MySQL', ' ',VERSION()) AS SQLVersion
  FROM   {databaseSchema}.{objectQualifier}Board a;
  ELSE
  SELECT a.*,
  CONCAT('Database: MySQL', ' ',VERSION()) AS SQLVersion
  FROM   {databaseSchema}.{objectQualifier}Board a
  WHERE  a.BoardID = ici_BoardID;
  END IF;
  END;
--GO

   /* STORED PROCEDURE CREATED BY VZ-TEAM */
   CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_poststats(
					 i_BoardID INT, 
					 i_StyledNicks TINYINT(1), 
					 i_ShowNoCountPosts TINYINT(1), 
					 i_GetDefaults TINYINT(1),
					 i_UTCTIMESTAMP DATETIME)
  READS SQL DATA
  BEGIN
  IF i_GetDefaults <= 0 THEN

  SELECT (SELECT CAST(COUNT(a.Indent) AS UNSIGNED)
  FROM   {databaseSchema}.{objectQualifier}Message a
  JOIN {databaseSchema}.{objectQualifier}Topic b
  ON b.TopicID = a.TopicID
  JOIN {databaseSchema}.{objectQualifier}Forum c
  ON c.ForumID = b.ForumID
  JOIN {databaseSchema}.{objectQualifier}Category d
  ON d.CategoryID = c.CategoryID
  WHERE  d.BoardID = i_BoardID  AND (a.Flags & 24)=16)   AS Posts,

(SELECT CAST(COUNT(a.TopicID) AS UNSIGNED)
FROM   {databaseSchema}.{objectQualifier}Topic a
JOIN {databaseSchema}.{objectQualifier}Forum b
ON b.ForumID = a.ForumID
JOIN {databaseSchema}.{objectQualifier}Category c
ON c.CategoryID = b.CategoryID
WHERE  c.BoardID = i_BoardID AND (a.Flags & 8) <> 8) AS Topics,

	(SELECT CAST(COUNT(a.ForumID) AS UNSIGNED)
	FROM   {databaseSchema}.{objectQualifier}Forum a
	JOIN {databaseSchema}.{objectQualifier}Category b
	ON b.CategoryID = a.CategoryID
	WHERE  b.BoardID = i_BoardID ) AS Forums,
1 AS LastPostInfoID,
a.Posted AS LastPost,
a.UserID AS LastUserID,
e.Name AS LastUser,	
e.DisplayName AS LastUserDisplayName,
(case(i_StyledNicks) when 1 then   e.UserStyle  
			else ''	 end)  AS LastUserStyle		
FROM     {databaseSchema}.{objectQualifier}Message a
JOIN {databaseSchema}.{objectQualifier}Topic b
ON b.TopicID = a.TopicID
JOIN {databaseSchema}.{objectQualifier}Forum c
ON c.ForumID = b.ForumID
JOIN {databaseSchema}.{objectQualifier}Category d
ON d.CategoryID = c.CategoryID
JOIN {databaseSchema}.{objectQualifier}User e
ON e.UserID = a.UserID
WHERE    (a.Flags & 24) = 16
	-- topic not deleted
	AND (b.Flags & 8) <> 8 
	AND d.BoardID = i_BoardID
	-- nocount
	AND (c.Flags & 8) <> (CASE WHEN i_ShowNoCountPosts > 0 THEN -1 ELSE 8 END)
ORDER BY a.Posted DESC LIMIT 1;
ELSE
SELECT
		{databaseSchema}.{objectQualifier}biginttoint(0) AS Posts,
		{databaseSchema}.{objectQualifier}biginttoint(0) AS Topics,
		{databaseSchema}.{objectQualifier}biginttoint(1) AS Forums,	
		{databaseSchema}.{objectQualifier}biginttoint(1) AS LastPostInfoID,
		NULL AS LastPost,
		NULL AS LastUserID,
		NULL AS LastUser,
		NULL AS LastUserDisplayName,
		'' AS LastUserStyle ;
END IF;
-- can be anyway in a place with very low update rate
 -- first delete tags
 DELETE FROM {databaseSchema}.{objectQualifier}TopicTags 
		 WHERE TopicID IN (SELECT TopicID FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicMovedID IS NOT NULL AND LinkDate IS NOT NULL AND LinkDate < i_UTCTIMESTAMP);
 -- then a link
 DELETE FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicMovedID IS NOT NULL AND LinkDate IS NOT NULL AND LinkDate < i_UTCTIMESTAMP;
END;
--GO

   /* STORED PROCEDURE CREATED BY VZ-TEAM */
   CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_userstats(
  i_BoardID INT, i_StyledNicks TINYINT(1))
  READS SQL DATA
  BEGIN
  SELECT 
(SELECT CAST(COUNT(1) AS UNSIGNED)
FROM   {databaseSchema}.{objectQualifier}User a
WHERE  a.BoardID = i_BoardID AND (a.Flags & 2) = 2 AND (a.Flags & 4) = 0) AS Members,
	(SELECT `Value` FROM {databaseSchema}.{objectQualifier}Registry WHERE LOWER(`Name`) = LOWER('maxusers') AND      BoardID=i_BoardID) AS MaxUsers,
	(SELECT `Value` FROM {databaseSchema}.{objectQualifier}Registry WHERE LOWER(`Name`) = LOWER('maxuserswhen') AND     BoardID=i_BoardID) AS MaxUsersWhen,
 1 AS LastMemberInfoID,
 `UserID` AS LastMemberID,
`Name` AS LastMember,
`DisplayName` AS LastMemberDisplayName
FROM      {databaseSchema}.{objectQualifier}User
WHERE    (Flags & 2) = 2
	 AND (Flags & 4) <> 4
	 AND BoardID = i_BoardID 
ORDER BY `Joined` DESC LIMIT 1;
END;
--GO

   /* STORED PROCEDURE CREATED BY VZ-TEAM */
  CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_poststats1(
  i_BoardID INT)
  READS SQL DATA
  BEGIN
  SELECT (SELECT CAST(COUNT(a.Indent) AS UNSIGNED)
  FROM   {databaseSchema}.{objectQualifier}Message a
  JOIN {databaseSchema}.{objectQualifier}Topic b
  ON b.TopicID = a.TopicID
  JOIN {databaseSchema}.{objectQualifier}Forum c
  ON c.ForumID = b.ForumID
  JOIN {databaseSchema}.{objectQualifier}Category d
  ON d.CategoryID = c.CategoryID
  WHERE  d.BoardID = i_BoardID  AND (a.Flags & 24)=16) AS Posts,
(SELECT CAST(COUNT(a.TopicID) AS UNSIGNED)
FROM   {databaseSchema}.{objectQualifier}Topic a
JOIN {databaseSchema}.{objectQualifier}Forum b
ON b.ForumID = a.ForumID
JOIN {databaseSchema}.{objectQualifier}Category c
ON c.CategoryID = b.CategoryID
WHERE  c.BoardID = i_BoardID AND (a.Flags & 8) <> 8) AS Topics,
	(SELECT CAST(COUNT(a.ForumID) AS UNSIGNED)
	FROM   {databaseSchema}.{objectQualifier}Forum a
	JOIN {databaseSchema}.{objectQualifier}Category b
	ON b.CategoryID = a.CategoryID
	WHERE  b.BoardID = i_BoardID ) AS Forums,
(SELECT CAST(COUNT(1) AS UNSIGNED)
FROM   {databaseSchema}.{objectQualifier}User a
WHERE  a.BoardID = i_BoardID AND (a.Flags & 2) = 2 AND (a.Flags & 4) = 0) AS Members,
	(SELECT `Value` FROM {databaseSchema}.{objectQualifier}Registry WHERE LOWER(`Name`) = LOWER('maxusers') AND      BoardID=i_BoardID) AS MaxUsers,
	(SELECT `Value` FROM {databaseSchema}.{objectQualifier}Registry WHERE LOWER(`Name`) = LOWER('maxuserswhen') AND     BoardID=i_BoardID) AS MaxUsersWhen;
END;
--GO

  /* STORED PROCEDURE CREATED BY VZ-TEAM */

  CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_poststats2(
  i_BoardID INT)
  READS SQL DATA
  BEGIN
SELECT 
1 AS LastPostInfoID,
a.Posted AS LastPost,
a.UserID AS LastUserID,
e.Name AS LastUser,
{databaseSchema}.{objectQualifier}get_userstyle(a.UserID) AS LastUserStyle	
FROM     {databaseSchema}.{objectQualifier}Message a
JOIN {databaseSchema}.{objectQualifier}Topic b
ON b.TopicID = a.TopicID
JOIN {databaseSchema}.{objectQualifier}Forum c
ON c.ForumID = b.ForumID
JOIN {databaseSchema}.{objectQualifier}Category d
ON d.CategoryID = c.CategoryID
JOIN {databaseSchema}.{objectQualifier}User e
ON e.UserID = a.UserID
WHERE    (a.Flags & 24) = 16
	AND (b.Flags & 8) <> 8 
	AND d.BoardID = i_BoardID
ORDER BY a.Posted DESC LIMIT 1;
END;
--GO

 /* STORED PROCEDURE CREATED BY VZ-TEAM */
  CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_poststats3(
  i_BoardID INT)
  READS SQL DATA
  BEGIN
SELECT 
1 AS LastMemberInfoID,
`UserID` AS LastMemberID,
`Name` AS LastMember
FROM     {databaseSchema}.{objectQualifier}User
WHERE    (Flags & 2) = 2
	 AND (Flags & 4) = 0
	 AND BoardID = i_BoardID 
ORDER BY `Joined` DESC LIMIT 1;
END;
--GO
  
/* STORED PROCEDURE CREATED BY VZ-TEAM */

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}board_resync
	(i_BoardID INT)
	MODIFIES SQL DATA
BEGIN

		DECLARE  itmpForumID INT;
DECLARE currBoards CURSOR FOR
			SELECT BoardID FROM {databaseSchema}.{objectQualifier}Board;

	IF i_BoardID IS NULL THEN		
 
		OPEN currBoards;
		
		 /*cycle through forums*/
	BEGIN
   DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
	LOOP
		FETCH currBoards INTO itmpForumID ;		
		
		   /*resync board forums*/
			CALL {databaseSchema}.{objectQualifier}forum_resync (itmpForumID, null);
	 END LOOP; 		
	  END;
		CLOSE currBoards;
		/*deallocate curBoards*/ 	
	ELSE
		/*resync board forums*/
		CALL {databaseSchema}.{objectQualifier}forum_resync(i_BoardID, null);
	END IF;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_save(
				i_BoardID       INT,
				i_Name          VARCHAR(128),
				i_LanguageFile  VARCHAR(128), 
				i_Culture       VARCHAR(10),
				i_AllowThreaded TINYINT(1))
			   MODIFIES SQL DATA 
BEGIN
		CALL {databaseSchema}.{objectQualifier}registry_save('culture',i_Culture,i_BoardID);
		CALL {databaseSchema}.{objectQualifier}registry_save('language',i_LanguageFile,i_BoardID);
		UPDATE {databaseSchema}.{objectQualifier}Board
		SET    `Name` = CONVERT(i_Name USING {databaseEncoding}),
			   AllowThreaded = i_AllowThreaded
		WHERE  BoardID = i_BoardID;
		SELECT i_BoardID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_stats
	(i_BoardID INT)
	READS SQL DATA
BEGIN
	IF i_BoardID IS NULL THEN
		SELECT
		(SELECT CAST(COUNT(1) AS UNSIGNED) FROM {databaseSchema}.{objectQualifier}Message where SIGN(Flags & 16) = 1 AND SIGN(a.Flags & 8) = 0) AS NumPosts,
		(SELECT CAST(COUNT(1) AS UNSIGNED) FROM {databaseSchema}.{objectQualifier}Topic where SIGN(Flags & 8) = 0) AS NumTopics,
		(SELECT CAST(COUNT(1) AS UNSIGNED) FROM {databaseSchema}.{objectQualifier}User where SIGN(Flags & 2) = 1) AS NumUsers,
		(SELECT MIN(Joined)  FROM {databaseSchema}.{objectQualifier}User) AS BoardStart;
	
	ELSE
		
		SELECT
		(SELECT CAST(COUNT(a.MessageID) AS UNSIGNED) FROM  {databaseSchema}.{objectQualifier}Message a
		JOIN {databaseSchema}.{objectQualifier}Topic b ON a.TopicID=b.TopicID
		JOIN {databaseSchema}.{objectQualifier}Forum c ON b.ForumID=c.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category d ON c.CategoryID=d.CategoryID
		WHERE SIGN(a.Flags & 16) = 1 
				  AND SIGN(a.Flags & 8) = 0 
				  AND SIGN(b.Flags & 8) = 0 
				  AND d.BoardID=i_BoardID) AS NumPosts,
		(SELECT CAST(COUNT(a.TopicID) AS UNSIGNED) FROM {databaseSchema}.{objectQualifier}Topic a
		JOIN {databaseSchema}.{objectQualifier}Forum b ON a.ForumID=b.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category c ON b.CategoryID=c.CategoryID
		WHERE c.BoardID=i_BoardID 
				  AND SIGN(a.Flags & 8) = 0) AS NumTopics,
		(SELECT CAST(COUNT(UserID) AS UNSIGNED) from {databaseSchema}.{objectQualifier}User 
				WHERE SIGN(Flags & 2) = 1 
				   AND BoardID=i_BoardID) AS NumUsers,
		(SELECT MIN(`Joined`)  FROM {databaseSchema}.{objectQualifier}User where BoardID=i_BoardID) AS BoardStart;
	END IF;
END;
/*
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}board_stats
	(i_BoardID INT)
BEGIN
	IF i_BoardID IS NULL THEN
		SELECT
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}MessageSelectView where IsApproved = 1 AND IsDeleted = 0) AS NumPosts,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}TopicSelectView where IsDeleted = 0) AS NumTopics,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}UserSelectView where IsApproved = 1) AS NumUsers,
		(SELECT min(Joined) FROM {databaseSchema}.{objectQualifier}User) AS BoardStart;
	
	ELSE
		
		SELECT
		(SELECT COUNT(1) FROM  {databaseSchema}.{objectQualifier}MessageSelectView a
		JOIN {databaseSchema}.{objectQualifier}TopicSelectView b ON a.TopicID=b.TopicID
		JOIN {databaseSchema}.{objectQualifier}Forum c ON b.ForumID=c.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category d ON c.CategoryID=d.CategoryID
		WHERE a.IsApproved = 1 
				  AND a.IsDeleted = 0 
				  AND b.IsDeleted = 0 
				  AND d.BoardID=i_BoardID) AS NumPosts,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}TopicSelectView a
		JOIN {databaseSchema}.{objectQualifier}Forum b ON a.ForumID=b.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category c ON b.CategoryID=c.CategoryID
		WHERE c.BoardID=i_BoardID 
				  AND a.IsDeleted = 0) AS NumTopics,
		(SELECT COUNT(1) from {databaseSchema}.{objectQualifier}UserSelectView 
				WHERE IsApproved = 1 
				   AND BoardID=i_BoardID) AS NumUsers,
			(SELECT MIN(`Joined`) FROM {databaseSchema}.{objectQualifier}User where BoardID=i_BoardID) AS BoardStart;
	END IF;
END;*/
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}category_delete(
				i_CategoryID INT, i_NewCategoryID INT)
			   MODIFIES SQL DATA 
BEGIN
		DECLARE  iflag INT;

		 if (i_NewCategoryID is not null) then
			 update {databaseSchema}.{objectQualifier}Forum set CategoryID = i_NewCategoryID where CategoryID = i_CategoryID;
			 -- rebuild tree			
			call {databaseSchema}.{objectQualifier}forum_ns_recreate( null, i_CategoryID); 
			call {databaseSchema}.{objectQualifier}forum_ns_recreate( null, i_NewCategoryID);
		 end if;

		IF EXISTS (SELECT 1
				   FROM   {databaseSchema}.{objectQualifier}Forum
				   WHERE  CategoryID = i_CategoryID) THEN       
			SET iflag = 0;       
		ELSE
			DELETE FROM {databaseSchema}.{objectQualifier}Category
			WHERE       CategoryID = i_CategoryID;
			SET iflag = 1;	      
		END IF;
		
		SELECT iflag;
	END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */ 

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}category_list(i_BoardID INT,i_CategoryID INT) 
READS SQL DATA
BEGIN
	IF i_CategoryID IS NULL THEN
		SELECT * FROM {databaseSchema}.{objectQualifier}Category WHERE BoardID = i_BoardID ORDER BY SortOrder;
	ELSE
		SELECT * FROM {databaseSchema}.{objectQualifier}Category WHERE BoardID = i_BoardID AND CategoryID = i_CategoryID;
		END IF;
END;
--GO

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}category_pfaccesslist(i_BoardID INT,i_CategoryID INT) 
READS SQL DATA
BEGIN
	IF i_CategoryID IS NULL THEN
		SELECT c.*,
		IFNULL((SELECT SIGN(f.ForumID) FROM {databaseSchema}.{objectQualifier}Forum f where f.CategoryID = c.CategoryID and f.CanHavePersForums  = 1 LIMIT 1),0) AS HasForumsForPersForums
		FROM {databaseSchema}.{objectQualifier}Category c WHERE c.BoardID = i_BoardID ORDER BY c.SortOrder;
	ELSE
		SELECT c.*,
		IFNULL((SELECT SIGN(f.ForumID) FROM {databaseSchema}.{objectQualifier}Forum f where f.CategoryID = c.CategoryID and f.CanHavePersForums  = 1 LIMIT 1),0)  AS HasForumsForPersForums
		FROM {databaseSchema}.{objectQualifier}Category c WHERE c.BoardID = i_BoardID AND c.CategoryID = i_CategoryID;
		END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}category_listread(
IN i_BoardID    INT,
IN i_UserID     INT,
IN i_CategoryID INT)
READS SQL DATA
BEGIN
SELECT   a.CategoryID,
a.Name,
a.CategoryImage
FROM     {databaseSchema}.{objectQualifier}Category a
JOIN {databaseSchema}.{objectQualifier}Forum b
ON b.CategoryID = a.CategoryID
JOIN {databaseSchema}.{objectQualifier}ActiveAccess v on v.ForumID=b.ForumID
WHERE    a.BoardID = i_BoardID
AND v.UserID=i_UserID 
AND (CAST(v.ReadAccess AS UNSIGNED)<>0 OR (b.Flags & 2)=0) 
AND (i_CategoryID IS NULL
OR a.CategoryID = i_CategoryID)
AND b.ParentID IS NULL
GROUP BY a.CategoryID,a.Name,a.SortOrder,
a.CategoryImage
ORDER BY a.SortOrder;
END;
--GO

create procedure {databaseSchema}.{objectQualifier}category_getadjacentforum(i_BoardID int,i_CategoryID int,i_PageUserID int, i_IsAfter tinyint(1)) 
begin
declare ici_ForumID int default 0;
declare ici_SortOrder int default 0;
   -- find first forum in category and its sort order. If no forums, returns 0 for both and is a first forum in a category.
   if i_IsAfter > 0 then 
	select IFNULL(f.ForumID,0),IFNULL(f.SortOrder,0) 
	INTO ici_ForumID
	from {databaseSchema}.{objectQualifier}Forum f 
	JOIN {databaseSchema}.{objectQualifier}Category c
	ON c.CategoryID = f.CategoryID
	JOIN {databaseSchema}.{objectQualifier}ActiveAccess aa
	ON (aa.ForumID = f.ForumID and aa.UserID = i_PageUserID)
	where BoardID = i_BoardID and CategoryID = i_CategoryID order by f.SortOrder LIMIT 1;
   end if;

   -- increase order to shift forums order in the category
  if ici_ForumID > 0 and ici_SortOrder = 0 then  
  UPDATE {databaseSchema}.{objectQualifier}Forum set SortOrder = SortOrder + 1 where CategoryID =  i_CategoryID;
  end if;
   SELECT ici_ForumID as ForumID, ici_SortOrder as SortOrder;
end;
--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE PROCEDURE {databaseSchema}.{objectQualifier}category_save
(
i_BoardID    INT,
i_CategoryID INT,
i_Name       VARCHAR(128),
i_SortOrder  SMALLINT,
i_CategoryImage VARCHAR(255),
i_CanHavePersForums TINYINT(1),
i_AdjacentCategoryID INT,
i_AdjacentCategoryMode INT
)
MODIFIES SQL DATA
BEGIN
declare ici_OldSortOrder int;
declare ici_OldBoardID   int;
declare tmp int;
declare cntr int default 0;
declare afterset tinyint(1);
  

	-- re-order categories removing gaps, create sortorder gap for a category
	declare c cursor for
		select CategoryID from {databaseSchema}.{objectQualifier}Category
		where BoardID = i_BoardID order by SortOrder, CategoryID;
	 if (i_AdjacentCategoryID is not null) then	
		  -- over doesn't possible 	
		open c;
		
		BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
LOOP	
	FETCH c INTO tmp;
		if (i_AdjacentCategoryID = tmp) 
		then
		-- before
		if (i_AdjacentCategoryMode = 1) 
		then
		select cntr into i_SortOrder;
		set cntr = cntr + 1;
		end if;
		-- after
		if (i_AdjacentCategoryMode = 2) 
		then
		set i_SortOrder = cntr + 1;	
		set afterset = 1;
		end if;
		end if;

		-- this is after gap
		if (i_SortOrder = cntr and afterset = 1)
		then
		set cntr = cntr + 1;
		end if;
		
		update	{databaseSchema}.{objectQualifier}Category
		set SortOrder = cntr where CategoryID = tmp;
		set cntr = cntr + 1;
		
		END LOOP;
		END;
		close c;   
		end if;

IF i_CategoryID > 0 THEN

		select BoardID, SortOrder 
		 into ici_OldBoardID, ici_OldSortOrder
			from {databaseSchema}.{objectQualifier}Category
			  WHERE  CategoryID = i_CategoryID;

		UPDATE {databaseSchema}.{objectQualifier}Category
		 SET    `Name` = CONVERT(i_Name USING {databaseEncoding}),
				CategoryImage = i_CategoryImage,
				SortOrder = (CASE WHEN i_AdjacentCategoryMode != -1 THEN i_SortOrder ELSE SortOrder END),
				CanHavePersForums = i_CanHavePersForums
		 WHERE  CategoryID = i_CategoryID;
	   
  ELSE
		 INSERT INTO {databaseSchema}.{objectQualifier}Category
		 (BoardID,`Name`,`CategoryImage`,SortOrder,CanHavePersForums)
		 VALUES     (i_BoardID,i_Name,i_CategoryImage,i_SortOrder,i_CanHavePersForums);

		 SELECT LAST_INSERT_ID() into i_CategoryID;
 END IF;
	SELECT  i_CategoryID AS CategoryID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}checkemail_list
(
i_Email VARCHAR(128)
)
READS SQL DATA
BEGIN
IF i_Email IS NULL THEN
SELECT * FROM {databaseSchema}.{objectQualifier}CheckEmail;
ELSE
SELECT * FROM {databaseSchema}.{objectQualifier}CheckEmail WHERE Email = LOWER(i_EMail);
END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}checkemail_save
(
i_UserID INT,
i_Hash VARCHAR(32),
i_Email VARCHAR(128),
i_UTCTIMESTAMP DATETIME
)
MODIFIES SQL DATA
BEGIN
INSERT INTO {databaseSchema}.{objectQualifier}CheckEmail
(UserID,Email,Created,Hash)
VALUES
(i_UserID,LOWER(i_Email),i_UTCTIMESTAMP,i_Hash);
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE procedure {databaseSchema}.{objectQualifier}checkemail_update(i_Hash VARCHAR(32))
MODIFIES SQL DATA
BEGIN
DECLARE l_UserID INT;
DECLARE l_CheckEmailID INT;
DECLARE l_Email VARCHAR(128);

SET l_UserID = NULL;

SELECT
CheckEmailID,
UserID,
Email
INTO l_CheckEmailID,l_UserID,l_Email
FROM
{databaseSchema}.{objectQualifier}CheckEmail
WHERE
Hash = i_Hash;

IF l_UserID IS NULL THEN
SELECT CONVERT(NULL, CHAR(36)) AS ProviderUserKey, CONVERT(NULL, CHAR(255)) AS Email;
ELSE
/*Update new user email*/
UPDATE {databaseSchema}.{objectQualifier}User SET Email = LOWER(l_Email), Flags = Flags | 2  WHERE UserID = l_UserID;
DELETE FROM {databaseSchema}.{objectQualifier}CheckEmail WHERE CheckEmailID = l_CheckEmailID;

/*return the UserProviderKey*/
SELECT ProviderUserKey, Email FROM {databaseSchema}.{objectQualifier}User WHERE UserID = l_UserID;
END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
/*polls*/

CREATE PROCEDURE {databaseSchema}.{objectQualifier}choice_add(
i_PollID		INT,
i_Choice		VARCHAR(128),
i_ObjectPath    VARCHAR(255),
i_MimeType      VARCHAR(50)
)
MODIFIES SQL DATA
BEGIN
INSERT INTO {databaseSchema}.{objectQualifier}Choice
(PollID, Choice, Votes, ObjectPath, MimeType)
VALUES
(i_PollID, i_Choice, 0, i_ObjectPath, i_MimeType);
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE procedure {databaseSchema}.{objectQualifier}choice_delete(
i_ChoiceID	INT
)
MODIFIES SQL DATA
BEGIN
DELETE FROM {databaseSchema}.{objectQualifier}Choice
WHERE ChoiceID = i_ChoiceID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}choice_update(
i_ChoiceID	INT,
i_Choice		VARCHAR(128),
i_ObjectPath    VARCHAR(255),
i_MimeType      VARCHAR(50)
)
MODIFIES SQL DATA
BEGIN

UPDATE {databaseSchema}.{objectQualifier}Choice
SET Choice = i_Choice,
ObjectPath = i_ObjectPath,
MimeType = i_MimeType 
WHERE ChoiceID = i_ChoiceID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}choice_vote
(i_ChoiceID INT,i_UserID INT, i_RemoteIP VARCHAR(39))
MODIFIES SQL DATA
BEGIN
DECLARE l_PollID INT;

SET l_PollID = (SELECT PollID FROM {databaseSchema}.{objectQualifier}Choice WHERE ChoiceID = i_ChoiceID);

IF i_UserID IS NULL THEN
IF i_RemoteIP IS NOT NULL THEN
INSERT INTO {databaseSchema}.{objectQualifier}PollVote (PollID, UserID, RemoteIP, ChoiceID) VALUES (l_PollID,NULL,i_RemoteIP, i_ChoiceID);
END IF;

ELSE
INSERT INTO {databaseSchema}.{objectQualifier}PollVote (PollID, UserID, RemoteIP, ChoiceID) VALUES (l_PollID,i_UserID,i_RemoteIP, i_ChoiceID);
END IF;

UPDATE {databaseSchema}.{objectQualifier}Choice SET Votes = Votes + 1 WHERE ChoiceID = i_ChoiceID;
END;
--GO





/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}extension_delete (i_ExtensionID INT) 
 MODIFIES SQL DATA
 BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}Extension 
	WHERE ExtensionID = i_ExtensionID;
 END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}extension_edit (i_ExtensionID INT)
 READS SQL DATA
 BEGIN
	SELECT * 
	FROM {databaseSchema}.{objectQualifier}Extension 
	WHERE ExtensionID = i_ExtensionID 
	ORDER BY Extension;
 END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}extension_list (i_BoardID INT, i_Extension VARCHAR(10)) 
 READS SQL DATA
 BEGIN
 
	/*If an extension is passed, then we want to check for THAT extension*/
	IF LENGTH(i_Extension) > 0 THEN
		
			SELECT
				a.*
			FROM
				{databaseSchema}.{objectQualifier}Extension a
			WHERE
				a.BoardID = i_BoardID AND a.Extension=i_Extension
			ORDER BY
				a.Extension; 
	ELSE
		/* Otherwise, just get a list for the given i_BoardId */        
			SELECT
				a.*
			FROM
				{databaseSchema}.{objectQualifier}Extension a
			WHERE
				a.BoardID = i_BoardID	
			ORDER BY
				a.Extension;
		END IF;
 END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE procedure {databaseSchema}.{objectQualifier}extension_save (i_ExtensionID INT,i_BoardID INT,i_Extension VARCHAR(10))
 MODIFIES SQL DATA
 BEGIN
	if i_ExtensionID IS NULL OR i_ExtensionID = 0 THEN
		INSERT INTO {databaseSchema}.{objectQualifier}Extension (BoardID,Extension) 
		VALUES(i_BoardID,i_Extension);
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}Extension
		SET Extension = i_Extension 
		WHERE ExtensionID = i_ExtensionID;
	END IF;
 END;
--GO

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_delete(
				i_ForumID INT, i_MoveChildren tinyint(1), i_RebuildTree tinyint(1))
MODIFIES SQL DATA              
BEGIN
DECLARE ici_LastTopicID INT;
DECLARE ici_LastMessageID INT;
DECLARE ici_LastUserID INT;
DECLARE ici_LastUserName VARCHAR(128);
DECLARE ici_ParentID INT;
DECLARE ici_LastPosted DATETIME;
DECLARE ici_LastTopicID_Check INT;
DECLARE ici_LastMessageID_Check INT;
DECLARE  itmpTopicID INT;
DECLARE ici_NntpForumID INT;
DECLARE ici_WatchTopicID INT;
declare old_left_key int;
declare old_right_key int;
declare old_level int;
declare old_parentid int;
declare old_tree int;

DECLARE topic_cursor CURSOR  FOR
		SELECT   TopicID
		FROM     {databaseSchema}.{objectQualifier}topic
		WHERE    ForumID = i_ForumID
		ORDER BY TopicID DESC;
			   
/* Here we change Last things in forums */
SELECT LastMessageID, categoryid
INTO ici_LastMessageID, old_tree
FROM {databaseSchema}.{objectQualifier}Forum
WHERE ForumID = i_ForumID;

		UPDATE {databaseSchema}.{objectQualifier}Forum
		SET    `LastMessageID` = NULL,
				LastTopicID = NULL
		WHERE  ForumID = i_ForumID;
		

		UPDATE {databaseSchema}.{objectQualifier}Topic
		SET    `LastMessageID` = NULL
		WHERE  `ForumID` = i_ForumID;

		UPDATE {databaseSchema}.{objectQualifier}Active 
		SET ForumID=NULL 
		WHERE ForumID=i_ForumID; 
		
		DELETE  {databaseSchema}.{objectQualifier}WatchTopic,{databaseSchema}.{objectQualifier}Topic
		FROM {databaseSchema}.{objectQualifier}WatchTopic, {databaseSchema}.{objectQualifier}Topic
		WHERE `ForumID` = i_ForumID
		AND {databaseSchema}.{objectQualifier}WatchTopic.`TopicID` = {databaseSchema}.{objectQualifier}Topic.`TopicID`;
		
		DELETE {databaseSchema}.{objectQualifier}Active, {databaseSchema}.{objectQualifier}Topic
		FROM  {databaseSchema}.{objectQualifier}Active, {databaseSchema}.{objectQualifier}Topic
		WHERE {databaseSchema}.{objectQualifier}Topic.`ForumID` = i_ForumID
		AND   {databaseSchema}.{objectQualifier}Active.`TopicID` = {databaseSchema}.{objectQualifier}Topic.`TopicID`;

		SELECT NntpForumID INTO ici_NntpForumID FROM  {databaseSchema}.{objectQualifier}NntpForum WHERE ForumID = i_ForumID;		

		DELETE FROM  {databaseSchema}.{objectQualifier}NntpTopic WHERE  `NntpForumID` = ici_NntpForumID;      

		DELETE FROM {databaseSchema}.{objectQualifier}NntpForum
		WHERE       `ForumID` = i_ForumID;

		DELETE FROM {databaseSchema}.{objectQualifier}WatchForum
		WHERE       `ForumID` = i_ForumID;
				
		/*Delete topics, messages and attachments*/       

   OPEN topic_cursor;
   BEGIN
   DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
   LOOP
		   FETCH topic_cursor INTO itmpTopicID;           
		   CALL {databaseSchema}.{objectQualifier}topic_delete(itmpTopicID, null, 0, 1);
   END LOOP;          
   END;
		   CLOSE topic_cursor;
	  
		/* TopicDelete finished*/ 
	  
		DELETE FROM {databaseSchema}.{objectQualifier}ForumAccess
		WHERE       ForumID = i_ForumID;
	  
	   -- Delete UserForums

		DELETE FROM {databaseSchema}.{objectQualifier}UserForum
		WHERE       ForumID = i_ForumID;


	   -- And after this we can delete Forum itself

		DELETE FROM {databaseSchema}.{objectQualifier}Forum
		WHERE       ForumID = i_ForumID;
		/* Forum on update */
		SELECT ParentID INTO ici_ParentID FROM  {databaseSchema}.{objectQualifier}Forum
		WHERE ForumID = i_ForumID;
		IF ici_ParentID > 0 THEN
		SELECT LastPosted 
		INTO ici_LastPosted 
		FROM  {databaseSchema}.{objectQualifier}Forum
		WHERE ParentID = ici_ParentID ORDER BY LastPosted DESC LIMIT 1;
		IF ici_LastPosted IS NOT NULL THEN 
		SELECT ForumID INTO ici_ParentID FROM  {databaseSchema}.{objectQualifier}Forum
		WHERE LastPosted = ici_LastPosted ORDER BY ForumID DESC LIMIT 1;
		CALL {databaseSchema}.{objectQualifier}forum_updatelasttopic(ici_ParentID);
		CALL {databaseSchema}.{objectQualifier}forum_updatestats(ici_ParentID);
		END IF; 
		END IF; 
		-- rebuild tree
		if (i_RebuildTree = 1) then	
		  select left_key, right_key, `level`, parentid
		   into old_left_key, old_right_key, old_level, old_parentid
			from {databaseSchema}.{objectQualifier}forum_ns where tree = 0;

		if i_MoveChildren = 1 then	
		-- move children 1 level higher before deleting a forum
		   call  {databaseSchema}.{objectQualifier}forum_after_del2_func(old_tree, old_left_key, old_right_key, old_level, old_parentid);
		end	if;

		call  {databaseSchema}.{objectQualifier}forum_after_del_func(old_tree, old_left_key, old_right_key, old_level, old_parentid);
		-- call {databaseSchema}.{objectQualifier}forum_ns_recreate();	
		end if;
	END;
--GO
CREATE procedure {databaseSchema}.{objectQualifier}forum_move(i_ForumOldID int,i_ForumNewID int) 
MODIFIES SQL DATA              
BEGIN
DECLARE ici_LastTopicID INT;
DECLARE ici_LastMessageID INT;
DECLARE ici_LastUserID INT;
DECLARE ici_LastUserName VARCHAR(128);
DECLARE ici_ParentID INT;
DECLARE ici_LastPosted DATETIME;
DECLARE ici_LastTopicID_Check INT;
DECLARE ici_LastMessageID_Check INT;
DECLARE  itmpTopicID INT;
	   
		DECLARE topic_cursor CURSOR  FOR
		SELECT   TopicID
		FROM     {databaseSchema}.{objectQualifier}topic
		WHERE    ForumID = i_ForumOldID
		ORDER BY TopicID DESC;
			   -- Maybe an idea to use cascading foreign keys instead? Too bad they don't work on MS SQL 7.0...
	update {databaseSchema}.{objectQualifier}Forum set LastMessageID=null,LastTopicID=null where ForumID=i_ForumOldID;
	update {databaseSchema}.{objectQualifier}Active set ForumID=i_ForumNewID where ForumID=i_ForumOldID;
	update {databaseSchema}.{objectQualifier}NntpForum set ForumID=i_ForumNewID where ForumID=i_ForumOldID;
	update {databaseSchema}.{objectQualifier}WatchForum set ForumID=i_ForumNewID where ForumID=i_ForumOldID;
	delete from {databaseSchema}.{objectQualifier}ForumReadTracking where ForumID = i_ForumOldID;
	
	-- Move topics, messages and attachments
	  OPEN topic_cursor;
   BEGIN
   DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
   LOOP
		   FETCH topic_cursor INTO itmpTopicID;           
		   CALL {databaseSchema}.{objectQualifier}topic_move(itmpTopicID, i_ForumNewID , 0, -1);
   END LOOP;          
   END;
		   CLOSE topic_cursor;
	  
		/* TopicMove finished*/ 
	  
		DELETE FROM {databaseSchema}.{objectQualifier}ForumAccess
		WHERE       ForumID = i_ForumID;
	  
		/*Delete UserForums*/

		DELETE FROM {databaseSchema}.{objectQualifier}UserForum
		WHERE       ForumID = i_ForumID;

	   /*And after this we can delete Forum itself*/

		DELETE FROM {databaseSchema}.{objectQualifier}Forum
		WHERE       ForumID = i_ForumID;	
		call {databaseSchema}.{objectQualifier}forum_ns_recreate( null, null);

		/* Forum on update */
		SELECT ParentID INTO ici_ParentID FROM  {databaseSchema}.{objectQualifier}Forum
		WHERE ForumID = i_ForumID;
		IF ici_ParentID > 0 THEN
		SELECT LastPosted 
		INTO ici_LastPosted 
		FROM  {databaseSchema}.{objectQualifier}Forum
		WHERE ParentID = ici_ParentID ORDER BY LastPosted DESC LIMIT 1;
		IF ici_LastPosted IS NOT NULL THEN 
		SELECT ForumID INTO ici_ParentID FROM  {databaseSchema}.{objectQualifier}Forum
		WHERE LastPosted = ici_LastPosted ORDER BY ForumID DESC LIMIT 1;
		CALL {databaseSchema}.{objectQualifier}forum_updatelasttopic(ici_ParentID);
		CALL {databaseSchema}.{objectQualifier}forum_updatestats(ici_ParentID);
		END IF; 
		END IF; 
end;
 --GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_list(i_BoardID INT,i_ForumID INT, i_UserID INT, i_IsUserForum TINYINT(1)) 
READS SQL DATA
BEGIN
		IF i_ForumID = 0 THEN 
							 SET i_ForumID = NULL; END IF; 
		IF i_ForumID IS NULL THEN
					  SELECT a.* FROM {databaseSchema}.{objectQualifier}Forum a 
								  JOIN {databaseSchema}.{objectQualifier}Category b 
									 ON b.CategoryID=a.CategoryID                                  
										WHERE b.BoardID=i_BoardID 
										  ORDER BY a.SortOrder;
	ELSE
		SELECT a.* FROM {databaseSchema}.{objectQualifier}Forum a 
				   JOIN {databaseSchema}.{objectQualifier}Category b 
					ON b.CategoryID=a.CategoryID 
					 WHERE b.BoardID=i_BoardID 
					  AND a.ForumID = i_ForumID;
		END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_byuserlist(i_BoardID INT,i_ForumID INT, i_UserID INT, i_IsUserForum TINYINT(1)) 
READS SQL DATA
BEGIN
		
		IF i_ForumID IS NULL THEN
					  SELECT a.* FROM {databaseSchema}.{objectQualifier}Forum a 
								  JOIN {databaseSchema}.{objectQualifier}Category b 
									 ON b.CategoryID=a.CategoryID                                  
										WHERE b.BoardID=i_BoardID and a.IsUserForum = i_IsUserForum
										and (i_UserID IS NULL OR a.CreatedByUserID = i_UserID)
										  ORDER BY a.SortOrder;
	ELSE
		SELECT a.* FROM {databaseSchema}.{objectQualifier}Forum a 
				   JOIN {databaseSchema}.{objectQualifier}Category b 
					ON b.CategoryID=a.CategoryID 
					 WHERE b.BoardID=i_BoardID 
					  AND a.ForumID = i_ForumID and a.IsUserForum = i_IsUserForum
										and (i_UserID IS NULL OR a.CreatedByUserID = i_UserID);
		END IF;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_listall (i_BoardID INT,i_UserID INT, i_Root INT, i_ReturnAll TINYINT(1))
   READS SQL DATA
   BEGIN
			IF i_Root IS NULL THEN SET i_Root = 0 ; END IF;
			IF i_Root = 0 THEN
				 SELECT
					b.CategoryID,
					b.Name AS Category,
					a.ForumID,
					a.Name AS Forum,
					0 AS Indent,
					a.ParentID,
					a.PollGroupID,			
					a.Flags & 2 = 2,					
					a.CanHavePersForums,
					CAST(c.ReadAccess AS UNSIGNED) > 0
			  FROM
					{databaseSchema}.{objectQualifier}Forum a
					JOIN {databaseSchema}.{objectQualifier}Category b ON b.CategoryID=a.CategoryID
					JOIN {databaseSchema}.{objectQualifier}ActiveAccess c ON c.ForumID=a.ForumID
			  WHERE                    
					b.BoardID = i_BoardID AND
					c.UserID = i_UserID AND
					(i_ReturnAll = 1 OR CAST(c.ReadAccess AS UNSIGNED) > 0) 
			  ORDER BY
					b.SortOrder,
					a.SortOrder,
					b.CategoryID,
					a.ForumID;

			  ELSEIF  i_Root > 0  THEN

	SELECT
		b.CategoryID,
		b.Name AS Category,
		a.ForumID,
		a.Name AS Forum,
		0 AS Indent,
		a.ParentID,
		a.PollGroupID,
		a.Flags & 2 = 2,
		a.CanHavePersForums,
		CAST(c.ReadAccess AS UNSIGNED) > 0
	FROM
		{databaseSchema}.{objectQualifier}Forum a
		JOIN {databaseSchema}.{objectQualifier}Category b ON b.CategoryID=a.CategoryID 
		JOIN {databaseSchema}.{objectQualifier}ActiveAccess c ON c.ForumID=a.ForumID   
	WHERE       
		b.BoardID=i_BoardID AND
		c.UserID = i_UserID AND
		(i_ReturnAll = 1 OR CAST(c.ReadAccess AS UNSIGNED) > 0)  AND
		a.ForumID = i_Root
	ORDER BY
		b.SortOrder,
		a.SortOrder,
		b.CategoryID,
		a.ForumID;
ELSE 
   SELECT
		b.CategoryID,
		b.Name AS Category,
		a.ForumID,
		a.Name AS Forum,
		0 AS Indent,
		a.ParentID,
		a.PollGroupID,
		a.Flags & 2 = 2,
		a.CanHavePersForums,
		CAST(c.ReadAccess AS UNSIGNED) > 0
	FROM
		{databaseSchema}.{objectQualifier}Forum a
		JOIN {databaseSchema}.{objectQualifier}Category b ON b.CategoryID=a.CategoryID
		JOIN {databaseSchema}.{objectQualifier}ActiveAccess c ON c.ForumID=a.ForumID     
	WHERE      
		b.BoardID=i_BoardID AND
		 (i_ReturnAll = 1 OR CAST(c.ReadAccess AS UNSIGNED) > 0)  AND
		b.CategoryID = -i_Root
	ORDER BY
		b.SortOrder,
		a.SortOrder,
		b.CategoryID,
		a.ForumID;
END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_listall_fromcat(i_BoardID INT,i_CategoryID INT,i_AllowUserForumsOnly TINYINT(1)) 
 READS SQL DATA
 BEGIN
	SELECT b.CategoryID,
		   b.Name AS Category, 
		   a.ForumID, 
		   a.Name AS Forum, 
		   a.ParentID, 
		   a.PollGroupID,
		   a.CanHavePersForums 
	FROM   {databaseSchema}.{objectQualifier}Forum a 
		   INNER JOIN
		   {databaseSchema}.{objectQualifier}Category b 
					 ON b.CategoryID = a.CategoryID
		   WHERE
			b.CategoryID=i_CategoryID and
			b.BoardID=i_BoardID
			and (i_AllowUserForumsOnly = 0 OR IsUserForum = 1)
		   ORDER BY
			b.SortOrder,
			a.SortOrder;
 END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_listallmymoderated(i_BoardID INT,i_UserID INT)
READS SQL DATA
BEGIN
	SELECT
		b.CategoryID,
		b.Name AS Category,
		a.ForumID,
		a.Name AS Forum,
		x.Indent
	FROM
		((SELECT
			b.ForumID,
			0 AS Indent
		FROM
			{databaseSchema}.{objectQualifier}Category a
			JOIN {databaseSchema}.{objectQualifier}Forum b ON b.CategoryID=a.CategoryID
		WHERE
			a.BoardID=i_BoardID AND
			b.ParentID IS NULL)
	
		UNION
	
		(SELECT
			c.ForumID,
			1 AS Indent
		FROM
			{databaseSchema}.{objectQualifier}Category a
			JOIN {databaseSchema}.{objectQualifier}Forum b on b.CategoryID=a.CategoryID
			JOIN {databaseSchema}.{objectQualifier}Forum c on c.ParentID=b.ForumID
		WHERE
			a.BoardID=i_BoardID and
			b.ParentID IS NULL)
	
		UNION
	
		(SELECT
			d.ForumID,
			2 AS Indent 
		FROM
			{databaseSchema}.{objectQualifier}Category a
			JOIN {databaseSchema}.{objectQualifier}Forum b ON b.CategoryID=a.CategoryID
			JOIN {databaseSchema}.{objectQualifier}Forum c ON c.ParentID=b.ForumID
			JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ParentID=c.ForumID
		WHERE
			a.BoardID=i_BoardID AND
			b.ParentID IS NULL
		)) AS x
		JOIN {databaseSchema}.{objectQualifier}Forum a ON a.ForumID=x.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category b ON b.CategoryID=a.CategoryID
		JOIN {databaseSchema}.{objectQualifier}ActiveAccess c on c.ForumID=a.ForumID

	WHERE 		
		b.BoardID=i_BoardID AND
		c.ModeratorAccess > 0
	ORDER BY
		b.SortOrder,
		a.SortOrder;
END;
--GO

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_listSubForums(
							i_ForumID int) 
							  READS SQL DATA

begin
		select Sum(1) from {databaseSchema}.{objectQualifier}Forum where ParentID = i_ForumID;
end;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_listpath(
							i_ForumID INT)
							READS SQL DATA
BEGIN
							/*supports up to 4 levels of nested forums*/
							SELECT a.ForumID,
							a.Name
							FROM     ((SELECT a.ForumID,
							0 AS Indent
							FROM   {databaseSchema}.{objectQualifier}Forum a
							WHERE  a.ForumID = i_ForumID)
							UNION
							(SELECT b.ForumID,
							1 AS Indent
							FROM   {databaseSchema}.{objectQualifier}Forum a
							JOIN {databaseSchema}.{objectQualifier}Forum b
							ON b.ForumID = a.ParentID
							WHERE  a.ForumID = i_ForumID)
							UNION
							(SELECT c.ForumID,
							2 AS Indent
							FROM   {databaseSchema}.{objectQualifier}Forum a
							JOIN {databaseSchema}.{objectQualifier}Forum b
							ON b.ForumID = a.ParentID
							JOIN {databaseSchema}.{objectQualifier}Forum c
							ON c.ForumID = b.ParentID
							WHERE  a.ForumID = i_ForumID)
							UNION
							(SELECT d.ForumID,
							3 AS Indent
							FROM   {databaseSchema}.{objectQualifier}Forum a
							JOIN {databaseSchema}.{objectQualifier}Forum b
							ON b.ForumID = a.ParentID
							JOIN {databaseSchema}.{objectQualifier}Forum c
							ON c.ForumID = b.ParentID
							JOIN {databaseSchema}.{objectQualifier}Forum d
							ON d.ForumID = c.ParentID
							WHERE  a.ForumID = i_ForumID)) AS x
							JOIN {databaseSchema}.{objectQualifier}Forum a
							ON a.ForumID = x.ForumID
							ORDER BY x.Indent DESC;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_listread(
 i_BoardID INT,
 i_UserID INT,
 i_CategoryID INT,
 i_ParentID INT, 
 i_StyledNicks TINYINT(1),
 i_FindLastRead TINYINT(1), 
 i_ShowCommonForums TINYINT(1), 
 i_ShowPersonalForums  TINYINT(1), 
 i_ForumCreatedByUserId INT, 
 i_UTCTIMESTAMP DATETIME
 ) 
 BEGIN
 DROP TEMPORARY TABLE IF EXISTS tbl_1;
 DROP TEMPORARY TABLE IF EXISTS tbl;
 
 -- get parent forums list first
 CREATE TEMPORARY TABLE IF NOT EXISTS  tbl_1
 select 	
		b.ForumID,
		b.ParentID		
	from 
		{databaseSchema}.{objectQualifier}Category a  
		join {databaseSchema}.{objectQualifier}Forum b  on b.CategoryID=a.CategoryID
		join {databaseSchema}.{objectQualifier}ActiveAccess x  on x.ForumID=b.ForumID	
	where 
		a.BoardID = i_BoardID and
		((b.Flags & 2)=0 or x.ReadAccess<>0) and
		(i_CategoryID is null or a.CategoryID=i_CategoryID) and
		((i_ParentID is null and b.ParentID is null) or b.ParentID=i_ParentID) and
		x.UserID = i_UserID
	order by
		a.SortOrder,
		b.SortOrder;
-- child forums
CREATE TEMPORARY TABLE IF NOT EXISTS  tbl
select 	
		b.ForumID,
		b.ParentID		
	from 
		{databaseSchema}.{objectQualifier}Category a  
		join {databaseSchema}.{objectQualifier}Forum b   on b.CategoryID=a.CategoryID
		join {databaseSchema}.{objectQualifier}ActiveAccess x   on x.ForumID=b.ForumID		
	where 
		a.BoardID = i_BoardID and
		((b.Flags & 2)=0 or x.ReadAccess<>0) and
		(i_CategoryID is null or a.CategoryID=i_CategoryID) and
		(b.ParentID IN (SELECT ForumID FROM tbl_1)) and
		x.UserID = i_UserID
	order by
		a.SortOrder,
		b.SortOrder;

insert into tbl_1(ForumID,ParentID)
select * FROM tbl;
 -- more childrens can be added to display as a tree


   CREATE TEMPORARY TABLE IF NOT EXISTS  tmp_flr
	SELECT 
		a.CategoryID, 
		a.Name AS Category, 
		b.ForumID AS ForumID,
		b.Name AS Forum, 
		b.Description,
		b.ImageURL AS ImageUrl,
		b.Styles, 
		b.ParentID,
		b.PollGroupID,  
		b.IsUserForum,        
		b.Flags,
	(SELECT CAST(COUNT(a1.SessionID)AS UNSIGNED)  FROM {databaseSchema}.{objectQualifier}Active a1 
	JOIN {databaseSchema}.{objectQualifier}User usr 
	ON a1.UserID = usr.UserID     
	WHERE a1.ForumID=b.ForumID    
	AND SIGN(usr.Flags & 16) = 0)  AS Viewing,   
		b.RemoteURL, 		
		{databaseSchema}.{objectQualifier}forum_topics(b.ForumID) AS Topics,
		{databaseSchema}.{objectQualifier}forum_posts(b.ForumID) AS Posts, 				
		CAST(x.ReadAccess AS signed) AS ReadAccess,
		b.LastTopicID AS LTID,
		b.LastPosted AS LP,		
		{databaseSchema}.{objectQualifier}forum_lasttopic(b.ForumID,i_UserID,b.LastTopicID,b.LastPosted) AS LastTopicID,
		a.SortOrder AS CategoryOrder,
		b.SortOrder AS ForumOrder  
		/* {databaseSchema}.{objectQualifier}forum_lasttopic(b.ForumID,i_UserID,b.LastTopicID,b.LastPosted) AS LastTopicID,
		(SELECT t.LastPosted  FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE  t.TopicID=LastTopicID LIMIT 1) AS LastPosted, 
		 (SELECT t.LastMessageID  FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE  t.TopicID=LastTopicID LIMIT 1) AS LastMessageID,
		 (SELECT t.LastUserID  FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE   t.TopicID=LastTopicID LIMIT 1) AS LastUserID, 	 
		(SELECT t.Topic  FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE   t.TopicID=LastTopicID LIMIT 1) AS LastTopicName,
		COALESCE((SELECT t.LastUserName FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE  t.TopicID=LastTopicID LIMIT 1),(SELECT u2.Name
			 FROM   {databaseSchema}.{objectQualifier}User u2
			 WHERE  u2.UserID = b.LastUserID LIMIT 1)) AS LastUser */
	FROM 
		{databaseSchema}.{objectQualifier}Category a
		JOIN {databaseSchema}.{objectQualifier}Forum b 
		ON b.CategoryID=a.CategoryID 
		JOIN {databaseSchema}.{objectQualifier}ActiveAccess x 
		ON x.ForumID=b.ForumID

	WHERE 
		a.BoardID = i_BoardID and
		((b.Flags & 2)=0 or x.ReadAccess<>0) and 
		a.BoardID = i_BoardID
		AND
		(i_CategoryID IS NULL OR a.CategoryID=i_CategoryID) AND 
		--	(b.ForumID IN (SELECT aa.ForumID FROM tbl aa  UNION SELECT ab.ForumID FROM tbl_1 ab)) and		
		(b.ForumID IN (SELECT a.ForumID FROM tbl_1 a)) and
		x.UserID = i_UserID
	ORDER BY
		a.SortOrder,
		b.SortOrder;

			DROP TEMPORARY TABLE IF EXISTS tbl_1;
	DROP TEMPORARY TABLE IF EXISTS tbl;
		
		SELECT tf.*, 		
		t.LastPosted AS LastPosted,
		t.LastMessageID AS LastMessageID,
		t.LastMessageFlags,
		t.TopicMovedID,
		t.LastUserID AS LastUserID,		
		t.Topic AS LastTopicName,
		t.Status AS LastTopicStatus,
		t.Styles AS LastTopicStyles,
				(case(i_StyledNicks)
			when 1 then (select usr.UserStyle from {databaseSchema}.{objectQualifier}User usr where usr.UserID = t.LastUserID LIMIT 1)
			else ''	 end)  AS 	Style,		
		COALESCE(t.LastUserName,(SELECT u2.Name
			 FROM   {databaseSchema}.{objectQualifier}User u2
			 WHERE  u2.UserID = t.LastUserID LIMIT 1)) AS LastUser,
		COALESCE(t.LastUserDisplayName,(SELECT u2.DisplayName
			 FROM   {databaseSchema}.{objectQualifier}User u2
			 WHERE  u2.UserID = t.LastUserID LIMIT 1)) AS LastUserDisplayName,
		(case(i_FindLastRead)
			 when 1 then
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}ForumReadTracking y WHERE y.ForumID=t.ForumID AND y.UserID = i_UserID limit 1)
			 else CAST(NULL AS DATETIME)	end) AS  LastForumAccess,  
		(case(i_FindLastRead)
			 when 1 then
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=t.TopicID AND y.UserID = i_UserID limit 1)
			 else CAST(NULL AS DATETIME)	end) AS  LastTopicAccess    
		 FROM tmp_flr tf 
		 LEFT JOIN {databaseSchema}.{objectQualifier}Topic t 
		 ON t.TopicID = tf.LastTopicID 
		ORDER BY
		tf.CategoryOrder,
		tf.ForumOrder;

		 DROP TEMPORARY TABLE IF EXISTS tmp_flr;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_listread(
 i_BoardID INT,
 i_UserID INT,
 i_CategoryID INT,
 i_ParentID INT, 
 i_StyledNicks TINYINT(1),
 i_FindLastRead TINYINT(1), 
 i_ShowCommonForums TINYINT(1), 
 i_ShowPersonalForums  TINYINT(1), 
 i_ForumCreatedByUserId INT, 
 i_UTCTIMESTAMP DATETIME
 ) 
 BEGIN
 DECLARE lvl INT DEFAULT 0;
 DECLARE rk INT; 
 DECLARE lk INT; 

 IF i_ParentID IS NOT NULL THEN
 SELECT `level`,left_key + 1, right_key - 1 into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}Forum   
 where ForumID = i_ParentID;
 END IF;
 IF i_ParentID IS NULL AND i_CategoryID > 0 THEN
 SELECT 0, min(left_key), max(right_key)  into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}Forum
 where CategoryID = i_CategoryID;
 END IF;
 IF i_ParentID IS NULL AND i_CategoryID IS NULL THEN
 SELECT  0, min(f.left_key), max(f.right_key)  into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}Forum f
 join {databaseSchema}.{objectQualifier}Category c 
 on c.CategoryID = f.CategoryID
 where c.BoardID = i_BoardID;
 END IF;
 set lvl = lvl - 1;

   select 
		a.CategoryID, 
		a.Name as Category, 
		b.ForumID as ForumID,
		b.Name as Forum, 
		b.Description as `Description`,
		b.ImageUrl,
		b.Styles,
		b.ParentID,
		b.PollGroupID,
		(select sum(fp.NumTopics) from {databaseSchema}.{objectQualifier}Forum fp
		where fp.CategoryID = b.CategoryID and fp.left_key >= b.left_key and fp.right_key <= b.right_key) as Topics,
		(select sum(fp.NumPosts) from {databaseSchema}.{objectQualifier}Forum fp
		where fp.CategoryID = b.CategoryID and fp.left_key >= b.left_key and fp.right_key <= b.right_key) as Posts,		
		t.LastPosted as LastPosted,
		t.LastMessageID,
		t.LastMessageFlags,
		t.LastUserID,
		  COALESCE(t.LastUserName,(SELECT u2.Name
			 FROM   {databaseSchema}.{objectQualifier}User u2
			 WHERE  u2.UserID = t.LastUserID LIMIT 1)) AS LastUser,

		  COALESCE(t.LastUserDisplayName,(SELECT u2.DisplayName
			 FROM   {databaseSchema}.{objectQualifier}User u2
			 WHERE  u2.UserID = t.LastUserID LIMIT 1)) AS LastUserDisplayName,
		t.TopicID as LastTopicID,
		t.TopicMovedID,
		t.Topic as LastTopicName,
		t.Status as LastTopicStatus,
		t.Styles as LastTopicStyles,
		b.Flags,
		 (SELECT CAST(COUNT(a1.SessionID)AS UNSIGNED)  FROM {databaseSchema}.{objectQualifier}Active a1 
	JOIN {databaseSchema}.{objectQualifier}User usr 
	ON a1.UserID = usr.UserID     
	WHERE a1.ForumID=b.ForumID    
	AND SIGN(usr.Flags & 16) = 0)  AS Viewing,
		b.RemoteURL,		
		CAST(x.ReadAccess AS signed) AS ReadAccess,
		 (case(i_StyledNicks)
			when 1 then (select usr.UserStyle from {databaseSchema}.{objectQualifier}User usr where usr.UserID = t.LastUserID LIMIT 1)
			else ''	 end)  AS 	Style,		
	  (case(i_FindLastRead)
			 when 1 then
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}ForumReadTracking y WHERE y.ForumID=t.ForumID AND y.UserID = i_UserID limit 1)
			 else CAST(NULL AS DATETIME)	end) AS  LastForumAccess,  
		(case(i_FindLastRead)
			 when 1 then
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=t.TopicID AND y.UserID = i_UserID limit 1)
			 else CAST(NULL AS DATETIME)	end) AS  LastTopicAccess   	
	from 
		{databaseSchema}.{objectQualifier}Category a 
		join {databaseSchema}.{objectQualifier}Forum b on b.CategoryID=a.CategoryID		
		join {databaseSchema}.{objectQualifier}ActiveAccess x  on (x.ForumID=b.ForumID and x.UserID = i_UserID) 
		left outer join {databaseSchema}.{objectQualifier}Topic t ON t.TopicID = {databaseSchema}.{objectQualifier}forum_ns_lasttopic(b.left_key,b.right_key, a.CategoryId, i_UserID)
	where 		
	   (i_CategoryID IS NULL OR a.CategoryID = i_CategoryID) AND
		(b.`level` >= lvl) and		 
		((b.Flags & 2)=0 OR x.ReadAccess) and         	
		b.left_key >= lk and b.right_key <= rk 		
	order by
		a.SortOrder,
		b.left_key;  
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_listreadpersonal(
 i_BoardID INT,
 i_UserID INT,
 i_CategoryID INT,
 i_ParentID INT, 
 i_StyledNicks TINYINT(1),
 i_FindLastRead TINYINT(1), 
 i_ShowCommonForums TINYINT(1), 
 i_ShowPersonalForums  TINYINT(1), 
 i_ForumCreatedByUserId INT, 
 i_UTCTIMESTAMP DATETIME
 ) 
 BEGIN
   CREATE TEMPORARY TABLE IF NOT EXISTS  tmp_flr
	SELECT 
		a.CategoryID, 
		a.Name AS Category, 
		b.ForumID AS ForumID,
		b.Name AS Forum, 
		b.Description,
		b.ImageURL AS ImageUrl,
		b.Styles, 
		b.ParentID,
		b.PollGroupID,  
		b.IsUserForum,        
		b.Flags,
	(SELECT CAST(COUNT(a1.SessionID)AS UNSIGNED)  FROM {databaseSchema}.{objectQualifier}Active a1 
	JOIN {databaseSchema}.{objectQualifier}User usr 
	ON a1.UserID = usr.UserID     
	WHERE a1.ForumID=b.ForumID    
	AND SIGN(usr.Flags & 16) = 0)  AS Viewing,   
		b.RemoteURL, 		
		{databaseSchema}.{objectQualifier}forum_topics(b.ForumID) AS Topics,
		{databaseSchema}.{objectQualifier}forum_posts(b.ForumID) AS Posts, 				
		CAST(x.ReadAccess AS signed) AS ReadAccess,
		b.LastTopicID AS LTID,
		b.LastPosted AS LP,		
		{databaseSchema}.{objectQualifier}forum_lasttopic(b.ForumID,i_UserID,b.LastTopicID,b.LastPosted) AS LastTopicID,
		a.SortOrder AS CategoryOrder,
		b.SortOrder AS ForumOrder  
		/* {databaseSchema}.{objectQualifier}forum_lasttopic(b.ForumID,i_UserID,b.LastTopicID,b.LastPosted) AS LastTopicID,
		(SELECT t.LastPosted  FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE  t.TopicID=LastTopicID LIMIT 1) AS LastPosted, 
		 (SELECT t.LastMessageID  FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE  t.TopicID=LastTopicID LIMIT 1) AS LastMessageID,
		 (SELECT t.LastUserID  FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE   t.TopicID=LastTopicID LIMIT 1) AS LastUserID, 	 
		(SELECT t.Topic  FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE   t.TopicID=LastTopicID LIMIT 1) AS LastTopicName,
		COALESCE((SELECT t.LastUserName FROM 
		{databaseSchema}.{objectQualifier}Topic t
		WHERE  t.TopicID=LastTopicID LIMIT 1),(SELECT u2.Name
			 FROM   {databaseSchema}.{objectQualifier}User u2
			 WHERE  u2.UserID = b.LastUserID LIMIT 1)) AS LastUser */
	FROM 
		{databaseSchema}.{objectQualifier}Category a
		JOIN {databaseSchema}.{objectQualifier}Forum b 
		ON b.CategoryID=a.CategoryID 
		JOIN {databaseSchema}.{objectQualifier}ActiveAccess x 
		ON x.ForumID=b.ForumID

	WHERE 
		a.BoardID = i_BoardID and
		((b.Flags & 2)=0 or x.ReadAccess<>0) and 
		a.BoardID = i_BoardID
		AND
		(i_CategoryID IS NULL OR a.CategoryID=i_CategoryID) AND 
		--	(b.ForumID IN (SELECT aa.ForumID FROM tbl aa  UNION SELECT ab.ForumID FROM tbl_1 ab)) and		
		b.CreatedByUserID = i_ForumCreatedByUserId and b.IsUserForum = 1 and
		x.UserID = i_UserID
	ORDER BY
		a.SortOrder,
		b.SortOrder;

			DROP TEMPORARY TABLE IF EXISTS tbl_1;
	DROP TEMPORARY TABLE IF EXISTS tbl;
		
		SELECT tf.*, 		
		t.LastPosted AS LastPosted,
		t.LastMessageID AS LastMessageID,
		t.LastMessageFlags,
		t.TopicMovedID,
		t.LastUserID AS LastUserID,		
		t.Topic AS LastTopicName,
		t.Status AS LastTopicStatus,
		t.Styles AS LastTopicStyles,
				(case(i_StyledNicks)
			when 1 then (select usr.UserStyle from {databaseSchema}.{objectQualifier}User usr where usr.UserID = t.LastUserID LIMIT 1)
			else ''	 end)  AS 	Style,		
		COALESCE(t.LastUserName,(SELECT u2.Name
			 FROM   {databaseSchema}.{objectQualifier}User u2
			 WHERE  u2.UserID = t.LastUserID LIMIT 1)) AS LastUser,
		COALESCE(t.LastUserDisplayName,(SELECT u2.DisplayName
			 FROM   {databaseSchema}.{objectQualifier}User u2
			 WHERE  u2.UserID = t.LastUserID LIMIT 1)) AS LastUserDisplayName,
		(case(i_FindLastRead)
			 when 1 then
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}ForumReadTracking y WHERE y.ForumID=t.ForumID AND y.UserID = i_UserID limit 1)
			 else CAST(NULL AS DATETIME)	end) AS  LastForumAccess,  
		(case(i_FindLastRead)
			 when 1 then
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=t.TopicID AND y.UserID = i_UserID limit 1)
			 else CAST(NULL AS DATETIME)	end) AS  LastTopicAccess    
		 FROM tmp_flr tf 
		 LEFT JOIN {databaseSchema}.{objectQualifier}Topic t 
		 ON t.TopicID = tf.LastTopicID 
		ORDER BY
		tf.CategoryOrder,
		tf.ForumOrder;

		 DROP TEMPORARY TABLE IF EXISTS tmp_flr;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_listtopics(i_ForumID INT) 
BEGIN
SELECT t.*,IFNULL(t.Flags,0) AS Flags,
IFNULL(SIGN(t.Flags & 8)>0,false) AS IsDeleted,
IFNULL(SIGN(t.Flags & 1024)>0,false)  AS IsQuestion from {databaseSchema}.{objectQualifier}Topic t
WHERE t.ForumID = i_ForumID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_moderatelist(i_BoardID INT,i_UserID INT, i_IsUserForum TINYINT(1)) 
 BEGIN
 
 SELECT
		b.*,
		(SELECT     COUNT({databaseSchema}.{objectQualifier}Message.MessageID)
		FROM         {databaseSchema}.{objectQualifier}Message 
				INNER JOIN  {databaseSchema}.{objectQualifier}Topic 
				ON {databaseSchema}.{objectQualifier}Message.TopicID ={databaseSchema}.{objectQualifier}Topic.TopicID
		WHERE (({databaseSchema}.{objectQualifier}Message.Flags & 16)=0) 
		and (({databaseSchema}.{objectQualifier}Message.Flags & 8)=0) 
		AND (({databaseSchema}.{objectQualifier}Topic.Flags & 8) = 0) 
		AND ({databaseSchema}.{objectQualifier}Topic.ForumID=b.ForumID)) AS MessageCount,
		(SELECT     count({databaseSchema}.{objectQualifier}Message.MessageID)
		FROM         {databaseSchema}.{objectQualifier}Message 
				INNER JOIN   {databaseSchema}.{objectQualifier}Topic 
				ON {databaseSchema}.{objectQualifier}Message.TopicID = {databaseSchema}.{objectQualifier}Topic.TopicID
		WHERE (({databaseSchema}.{objectQualifier}Message.Flags & 128)=128) 
				AND (({databaseSchema}.{objectQualifier}Message.Flags & 8)=0) 
				AND (({databaseSchema}.{objectQualifier}Topic.Flags & 8) = 0) 
				AND ({databaseSchema}.{objectQualifier}Topic.ForumID=b.ForumID)) AS ReportedCount
			FROM
		{databaseSchema}.{objectQualifier}Category a 
			JOIN {databaseSchema}.{objectQualifier}Forum b ON b.CategoryID=a.CategoryID
			JOIN {databaseSchema}.{objectQualifier}ActiveAccess c ON c.ForumID=b.ForumID
			WHERE
		a.BoardID=i_BoardID AND
		b.IsUserForum = i_IsUserForum AND
		c.ModeratorAccess> 0
		AND
		c.UserID=i_UserID
			ORDER BY
		a.SortOrder,
		b.SortOrder;
 END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_moderators(i_StyledNicks TINYINT(1)) 
 BEGIN
 DECLARE bf0 TINYINT(1) DEFAULT 0;
 DECLARE bf1 TINYINT(1) DEFAULT 1;
	SELECT
		{databaseSchema}.{objectQualifier}biginttoint(a.ForumID) AS ForumID,
		f.Name AS ForumName, 
		a.GroupID AS ModeratorID, 
		b.Name AS ModeratorName,
		b.Name AS ModeratorDisplayName,
		'' AS ModeratorEmail,
		'' AS ModeratorAvatar,
		{databaseSchema}.{objectQualifier}biginttobool(0) as ModeratorAvatarImage,
		b.Style AS Style,	
		bf1 AS IsGroup 
	FROM
		{databaseSchema}.{objectQualifier}Forum f 
		INNER JOIN {databaseSchema}.{objectQualifier}ForumAccess a ON a.ForumID = f.ForumID
		INNER JOIN {databaseSchema}.{objectQualifier}Group b ON b.GroupID = a.GroupID
		INNER JOIN {databaseSchema}.{objectQualifier}AccessMask c ON c.AccessMaskID = a.AccessMaskID
	WHERE
		(b.Flags & 1)=0 
				 AND
		(c.Flags & 64)<>0
	UNION ALL
	SELECT 
		{databaseSchema}.{objectQualifier}biginttoint(acc.ForumID) AS ForumID, 
		f.Name AS ForumName, 		 
		usr.UserID AS ModeratorID, 
		usr.Name AS ModeratorName,
		usr.DisplayName AS ModeratorDisplayName,
		usr.Email AS ModeratorEmail,
		COALESCE(usr.Avatar, '') AS ModeratorAvatar,
		(select {databaseSchema}.{objectQualifier}biginttobool(1) from {databaseSchema}.{objectQualifier}User x where x.UserID=usr.UserID and x.AvatarImage is not null) AS ModeratorAvatarImage,		
		case(i_StyledNicks)
			when 1 then  usr.UserStyle  
			else ''	 end AS Style,	
		bf0 AS IsGroup
			FROM
		{databaseSchema}.{objectQualifier}User usr
		INNER JOIN (
			SELECT
				a.UserID,
				x.ForumID,
				max(x.ModeratorAccess) AS ModeratorAccess
			FROM
				{databaseSchema}.{objectQualifier}vaccessfull as x
				INNER JOIN {databaseSchema}.{objectQualifier}UserGroup a ON a.UserID=x.UserID
				INNER JOIN {databaseSchema}.{objectQualifier}Group b ON b.GroupID=a.GroupID
			WHERE 
				x.ModeratorAccess <> 0 AND x.AdminGroup = 0
			GROUP BY
				a.UserID,x.ForumID		
		) acc ON usr.UserID = acc.UserID
		JOIN    {databaseSchema}.{objectQualifier}Forum f 
		ON f.ForumID = acc.ForumID
	WHERE
		acc.ModeratorAccess<>0
	ORDER BY
		IsGroup desc,
		ModeratorName asc;
END;
--GO 

 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_moderators_1() 
 BEGIN
 DECLARE bf1 TINYINT(1) DEFAULT 1;
	SELECT
		a.ForumID AS ForumID, 
		f.Name AS ForumName, 
		a.GroupID AS ModeratorID,
		b.Name AS ModeratorName, 
		b.Name AS ModeratorDisplayName, 
		'' AS ModeratorEmail,
		'' AS ModeratorAvatar,
		{databaseSchema}.{objectQualifier}biginttobool(0) as ModeratorAvatarImage,		
		b.Style AS Style, 		
		bf1 AS IsGroup 
	FROM
		{databaseSchema}.{objectQualifier}Forum f 
		INNER JOIN {databaseSchema}.{objectQualifier}ForumAccess a ON a.ForumID = f.ForumID
		INNER JOIN {databaseSchema}.{objectQualifier}Group b ON b.GroupID = a.GroupID
		INNER JOIN {databaseSchema}.{objectQualifier}AccessMask c ON c.AccessMaskID = a.AccessMaskID
	WHERE
		(b.Flags & 1)=0 
				 AND
		(c.Flags & 64)<>0 	
	ORDER BY
		IsGroup desc,
		ModeratorName asc;
END;
--GO

 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_moderators_2(i_StyledNicks TINYINT(1)) 
 BEGIN
 DECLARE bf0 TINYINT(1) DEFAULT 0; 
SELECT DISTINCT
r.ForumID  AS ForumID,
f.Name AS ForumName, 
r.UserID as ModeratorID,
u.Name AS ModeratorName,
u.DisplayName AS ModeratorDisplayName,
u.Email AS ModeratorEmail,
u.RankID,
COALESCE(u.Avatar, '') AS ModeratorAvatar,
{databaseSchema}.{objectQualifier}biginttobool(IFNULL((select x.UserID from {databaseSchema}.{objectQualifier}User x where x.UserID=u.UserID and x.AvatarImage is not null),0)) AS ModeratorAvatarImage,			
case(i_StyledNicks)
			when 1 then  u.UserStyle
			else ''	 end AS Style,
bf0 AS IsGroup
FROM {databaseSchema}.{objectQualifier}User u INNER JOIN (SELECT * FROM (SELECT 
ForumID,UserID,ModeratorAccess,AdminGroup 
FROM
 {databaseSchema}.{objectQualifier}vaccessfull  
 WHERE ModeratorAccess<>0) AS t1
   ) r 
   ON r.UserID = u.UserID  
   JOIN    {databaseSchema}.{objectQualifier}Forum f 
	ON f.ForumID = r.ForumID        
	JOIN {databaseSchema}.{objectQualifier}Rank rr
		ON rr.RankID = u.RankID
	where
		r.ModeratorAccess<>0 and r.AdminGroup = 0
	order by
		IsGroup desc,
		ModeratorName asc;
END;
 --GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_resync
	(i_BoardID INT,i_ForumID INT)

BEGIN

DECLARE  itmpForumID INT;
DECLARE currForums CURSOR FOR
			SELECT 
				a.ForumID
			FROM
				{databaseSchema}.{objectQualifier}Forum a
				JOIN {databaseSchema}.{objectQualifier}Category b on a.CategoryID=b.CategoryID
				JOIN {databaseSchema}.{objectQualifier}Board c on b.BoardID = c.BoardID  
			WHERE
				c.BoardID=i_BoardID;

	IF i_ForumID IS NULL THEN	
 
		OPEN currForums; 		
		 /*cycle through forums*/
				BEGIN
   DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
	  LOOP
				FETCH currForums INTO itmpForumID;  		
		/*update statistics*/
			CALL {databaseSchema}.{objectQualifier}forum_updatestats(itmpForumID);
			/*update last post*/
			CALL {databaseSchema}.{objectQualifier}forum_updatelastpost(itmpForumID);	
		
				END LOOP;
				END;
		CLOSE currForums;
		/*deallocate curForums*/
	
	ELSE 		
		/*update statistics*/
		CALL {databaseSchema}.{objectQualifier}forum_updatestats(i_ForumID);
		/*update last post*/
		CALL {databaseSchema}.{objectQualifier}forum_updatelastpost(i_ForumID);
	END IF;
END;
--GO


	/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_save(
	i_ForumID 		INT,
	i_CategoryID	INT,
	i_ParentID		INT,
	i_Name			VARCHAR(128),
	i_Description	VARCHAR(255),
	i_SortOrder		INT,
	i_Locked		TINYINT(1),
	i_Hidden		TINYINT(1),
	i_IsTest		TINYINT(1),
	i_Moderated		TINYINT(1),
	i_RemoteURL		VARCHAR(128),
	i_ThemeURL		VARCHAR(128),
	i_ImageURL		VARCHAR(128),
	i_Styles 		VARCHAR(255),
	i_AccessMaskID	INT,
	i_UserID        INT,
	i_IsUserForum   TINYINT(1),
	i_CanHavePersForums   TINYINT(1),
	i_AdjacentForumID INT,
	i_AdjacentForumMode INT,
	i_UTCTIMESTAMP  datetime  
 )
BEGIN
	DECLARE l_BoardID	INT;
	DECLARE l_Flags		INT;
	DECLARE l_HasDependency		INT;
	DECLARE ici_UserName  VARCHAR(255);
	DECLARE ici_UserDisplayName  VARCHAR(255);
	DECLARE ici_BoardID INT;
	DECLARE ici_LatestOtherSortOrder INT;  

	declare ici_OldParentID	int;
	declare ici_OldCategoryID int;
	declare ici_OldSortOrder int;
	declare ici_OldLeftKey	int;
	declare ici_OldRightKey	int;
	declare ici_OldLevel	int;
	declare ici_OldNid	int;
	declare ici_cntr int default 0;
	declare ici_tmp int;
	declare ici_newid int;
	declare ici_nid int;
	declare parins int;
	declare ici_ocat int;
	declare afterset tinyint(1);
	declare newlk int;

	-- re-order forums removing gaps, create sort order gap for a forum.
	declare c cursor for
		select f.ForumID from {databaseSchema}.{objectQualifier}Forum f
		join {databaseSchema}.{objectQualifier}Category c
		on c.CategoryID = f.CategoryID
		where c.CategoryID = i_CategoryID and (i_ParentID is null or f.ParentID = i_ParentID)   
		order by c.SortOrder, f.ForumID;
	 if (i_AdjacentForumID is not null) then	 
	   -- over  	
				  if (i_AdjacentForumMode = 3) THEN		               
					 set  i_sortorder = 0;
					 set  ici_cntr = 1;
				  end if;   	
		open c;
		BEGIN
		DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
		 LOOP
		   FETCH c INTO ici_tmp;	
		
		if (i_AdjacentForumID = ici_tmp) then
		-- before
		if (i_AdjacentForumMode = 1) then
		select ici_cntr into i_SortOrder;
		select ici_cntr + 1 into ici_cntr;
		end if;
		-- after
		if (i_AdjacentForumMode = 2) then
		set i_SortOrder = ici_cntr + 1;	
		set afterset = 1;
		end if;
		end if;

		-- this is after gap
		if (i_SortOrder = ici_cntr and afterset = 1) then		
		select ici_cntr + 1 into ici_cntr;
		end if;
		
		update	{databaseSchema}.{objectQualifier}Forum
		set SortOrder = ici_cntr where ForumID = ici_tmp;
		select ici_cntr + 1 into ici_cntr;
		
		END LOOP;
		END;
		close c;
	end if;	  

	 -- If this is a personal forum we should override SortOrder 
	-- if (i_IsUserForum = 1 OR ) THEN
	-- SELECT SortOrder INTO ici_LatestOtherSortOrder FROM {databaseSchema}.{objectQualifier}Forum WHERE CreatedDate IS NOT NULL
	-- ORDER BY  CreatedDate DESC LIMIT 1;
	 -- personal forums should be sorted in creation order
	-- IF i_SortOrder <= ici_LatestOtherSortOrder THEN	
	-- i_SortOrder = ici_LatestOtherSortOrder + 1;
	-- END IF;	 
	-- END IF;

	SET l_Flags = 0;
	IF i_Locked<>0 THEN SET l_Flags = l_Flags | 1;END IF;
	IF i_Hidden<>0 THEN SET l_Flags = l_Flags | 2;END IF;
	IF i_IsTest<>0 THEN SET l_Flags = l_Flags | 4;END IF;
	IF i_Moderated<>0 THEN SET l_Flags = l_Flags | 8;END IF;
	  IF i_UserID IS NOT NULL THEN   
	SELECT Name, DisplayName INTO ici_UserName, ici_UserDisplayName FROM {databaseSchema}.{objectQualifier}User where UserID = i_UserID LIMIT  1;
	   -- guests should not create forums
	ELSE  
	SELECT Name, DisplayName INTO ici_UserName, ici_UserDisplayName FROM {databaseSchema}.{objectQualifier}User where BoardID = i_BoardID and (Flags & 4) = 4  ORDER BY Joined LIMIT 1;
	END IF;

  SET l_BoardID = (SELECT BoardID  
  from {databaseSchema}.{objectQualifier}Category
  WHERE CategoryID=i_CategoryID LIMIT 1);

  IF i_ForumID IS NOT NULL AND i_ForumID > 0 THEN 
  -- rebuild tree
   select f.ParentID, f.CategoryID, 
   f.SortOrder, f.left_key, 
   f.right_key, f.`level`, f.CategoryID
   INTO ici_OldParentID, ici_OldCategoryID, ici_OldSortOrder, ici_OldLeftKey, ici_OldRightKey, ici_OldLevel, ici_OldNid
   from {databaseSchema}.{objectQualifier}Forum  f 
   where f.ForumID=i_ForumID;

  /*  if (i_CategoryID != ici_OldCategoryID OR i_SortOrder != ici_OldSortOrder OR ici_OldParentID != i_ParentID) then	
	if (i_AdjacentForumID is not null)  then  
	if (i_AdjacentForumMode = 1) then 
	select left_key into newlk from {databaseSchema}.{objectQualifier}Forum where ForumID = i_AdjacentForumID;
	end if;
	if (i_AdjacentForumMode = 2) then 
	select right_key+1 into newlk from {databaseSchema}.{objectQualifier}Forum where ForumID = i_AdjacentForumID;	
	end if;

	call	{databaseSchema}.{objectQualifier}forum_before_update_func(i_ForumID, i_CategoryID, newlk, null, null, i_ParentID,0,0, 
			ici_ForumID, ici_OldCategoryID, ici_OldLeftKey, ici_OldRightKey, ici_OldLevel, ici_OldParentID);
	end if;
	end if;  */
	-- tree rebuilded
  UPDATE {databaseSchema}.{objectQualifier}Forum
  SET
  ParentID=(CASE WHEN(i_ParentID = 0) THEN NULL ELSE i_ParentID END),
  `Name`= CONVERT(i_Name USING {databaseEncoding}),
  `Description`=CONVERT(i_Description USING {databaseEncoding}),
  SortOrder=(CASE WHEN i_AdjacentForumMode != -1 THEN i_SortOrder ELSE SortOrder END),
  CategoryID=i_CategoryID,
  RemoteURL = i_RemoteURL,
  ThemeURL = i_ThemeURL,
  ImageURL = i_ImageURL,
  Styles = i_Styles,
  Flags = l_Flags,
  IsUserForum = i_IsUserForum,
  CanHavePersForums = i_CanHavePersForums
  WHERE ForumID=i_ForumID;
  if (i_CategoryID != ici_OldCategoryID) then
  call {databaseSchema}.{objectQualifier}forum_ns_recreate( null, ici_OldCategoryID);
  end if;
   call {databaseSchema}.{objectQualifier}forum_ns_recreate( null, i_CategoryID);
  ELSE
  INSERT INTO {databaseSchema}.{objectQualifier}Forum(ParentID,`Name`,Description,SortOrder,CategoryID,NumTopics,NumPosts,RemoteURL,ThemeURL,ImageURL,Styles,Flags,IsUserForum,CreatedByUserID,CreatedByUserName,CreatedByUserDisplayName,CreatedDate,CanHavePersForums)
  VALUES((CASE WHEN(i_ParentID = 0) THEN NULL ELSE i_ParentID END),CONVERT(i_Name USING {databaseEncoding}),CONVERT(i_Description USING {databaseEncoding}),i_SortOrder,i_CategoryID,0,0,i_RemoteURL,i_ThemeURL,i_ImageURL,i_Styles,l_Flags,i_IsUserForum
  ,i_UserID,ici_UserName, ici_UserDisplayName,i_UTCTIMESTAMP,i_CanHavePersForums);
  SET i_ForumID = LAST_INSERT_ID(); 

  -- rebuild tree 
	 select f.ParentID, f.CategoryID, 
   f.SortOrder, f.left_key, 
   f.right_key, f.`level`, f.CategoryID
   INTO ici_OldParentID, ici_OldCategoryID, ici_OldSortOrder, ici_OldLeftKey, ici_OldRightKey, ici_OldLevel, ici_OldNid
   from {databaseSchema}.{objectQualifier}Forum  f 
   where f.ForumID=i_ForumID;

	if (i_CategoryID != ici_OldCategoryID OR i_SortOrder != ici_OldSortOrder OR ici_OldParentID != i_ParentID) then	
	if (i_AdjacentForumID is not null)  then  
	if (i_AdjacentForumMode = 1) then 
	select left_key into newlk from {databaseSchema}.{objectQualifier}Forum where ForumID = i_AdjacentForumID;
	end if;
	if (i_AdjacentForumMode = 2) then 
	select right_key+1 into newlk from {databaseSchema}.{objectQualifier}Forum where ForumID = i_AdjacentForumID;	
	end if;

	call {databaseSchema}.{objectQualifier}forum_before_insert_func(ici_OldCategoryID, 
	i_ForumID,ici_OldCategoryID, nlk, null, null, ici_OldParentID, i_SortOrder,0, 0);
	end if;
	end if;
	-- tree rebuiled
  
  INSERT INTO {databaseSchema}.{objectQualifier}ForumAccess(GroupID,ForumID,AccessMaskID)
  SELECT GroupID,i_ForumID,i_AccessMaskID
  FROM {databaseSchema}.{objectQualifier}Group
  WHERE BoardID = l_BoardID;
  END IF;


	if exists (select 1 from {databaseSchema}.{objectQualifier}ForumHistory where ForumID = i_ForumID and ChangedDate = i_UTCTIMESTAMP  LIMIT 1) THEN
	update {databaseSchema}.{objectQualifier}ForumHistory set 
		   ChangedUserID = i_UserID,	
		   ChangedUserName = ici_UserName,
		   ChangedDisplayName = ici_UserDisplayName
	 where ForumID = i_ForumID and ChangedDate = i_UTCTIMESTAMP; 
	else    
	INSERT INTO {databaseSchema}.{objectQualifier}ForumHistory(ForumID,ChangedUserID,ChangedUserName,ChangedDisplayName,ChangedDate)
	VALUES (i_ForumID,i_UserID,ici_UserName,ici_UserDisplayName,i_UTCTIMESTAMP);
	end IF;

  SELECT i_ForumID AS ForumID;
  END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_updatelastpost(i_ForumID INT)
BEGIN
DECLARE ici_ParentID         INT;
DECLARE ici_tmpParent        INT;
DECLARE ici_LastPosted       DATETIME;
DECLARE ici_tmpMaxPosted3    DATETIME DEFAULT NULL;

DECLARE ici_LastTopicID      INT;
DECLARE ici_LastMessageID    INT;
DECLARE ici_LastUserID       INT;
DECLARE ici_LastUserName     VARCHAR(255);
DECLARE ici_LastUserDisplayName VARCHAR(255);

DECLARE ici_LastPostedTmp    DATETIME;
DECLARE ici_LastTopicIDTmp   INT;
DECLARE ici_LastMessageIDTmp INT;
DECLARE ici_LastUserIDTmp    INT;
DECLARE ici_LastUserNameTmp  VARCHAR(255);
DECLARE ici_LastUserDisplayNameTmp  VARCHAR(255);

DECLARE ici_MaxTPosted       DATETIME;

SELECT z.ParentID
INTO ici_ParentID
FROM {databaseSchema}.{objectQualifier}Forum z
WHERE z.ForumID = i_ForumID;

/*SELECT DISTINCTROW LastPosted,
TopicID,
LastMessageID,
LastUserID,
LastUserName
INTO ici_LastPosted,ici_LastTopicID,ici_LastMessageID,ici_LastUserID,ici_LastUserName
from {databaseSchema}.{objectQualifier}Topic
WHERE ForumID =i_ForumID ORDER BY LastPosted DESC LIMIT 1;

IF ici_LastTopicID IS NULL THEN*/
SELECT y.Posted,y.TopicID,y.MessageID,y.UserID,y.UserName,y.UserDisplayName
INTO ici_LastPosted,ici_LastTopicID,ici_LastMessageID,ici_LastUserID,ici_LastUserName,ici_LastUserDisplayName
FROM
{databaseSchema}.{objectQualifier}Forum z
JOIN {databaseSchema}.{objectQualifier}Topic x ON z.ForumID=x.ForumID 
JOIN {databaseSchema}.{objectQualifier}Message y ON y.TopicID=x.TopicID
WHERE x.ForumID = i_ForumID
AND (y.Flags & 24)=16
-- is deleted
AND SIGN(x.Flags & 8) = 0
ORDER BY y.Posted DESC LIMIT 1;
/*END IF; Look for it in children*/

SELECT LastPosted,
LastTopicID,
LastMessageID,
LastUserID,
LastUserName,
LastUserDisplayName
INTO 

ici_LastPostedTmp,ici_LastTopicIDTmp,ici_LastMessageIDTmp,ici_LastUserIDTmp,ici_LastUserNameTmp,ici_LastUserDisplayNameTmp
from {databaseSchema}.{objectQualifier}Forum
WHERE ParentID =i_ForumID ORDER BY LastPosted DESC LIMIT 1;
-- END IF; 
IF ici_LastPostedTmp IS NOT NULL AND ici_LastPosted IS NOT NULL THEN
IF TIMESTAMPDIFF(SECOND,IFNULL(ici_LastPostedTmp,'1001-01-01'),IFNULL(ici_LastPosted,'1001-01-01')) THEN

SET ici_LastPosted=ici_LastPostedTmp;
SET ici_LastTopicID=ici_LastTopicIDTmp;
SET ici_LastMessageID=ici_LastMessageIDTmp;
SET ici_LastUserID=ici_LastUserIDTmp;
SET ici_LastUserName=ici_LastUserNameTmp;
SET ici_LastUserDisplayName=ici_LastUserDisplayNameTmp;
END IF;
END IF;

IF ici_LastPostedTmp IS NOT NULL AND ici_LastPosted IS NULL THEN

SET ici_LastPosted=ici_LastPostedTmp;
SET ici_LastTopicID=ici_LastTopicIDTmp;
SET ici_LastMessageID=ici_LastMessageIDTmp;
SET ici_LastUserID=ici_LastUserIDTmp;
SET ici_LastUserName=ici_LastUserNameTmp;
SET ici_LastUserDisplayName=ici_LastUserDisplayNameTmp;

END IF;


IF (ici_LastTopicID IS NOT NULL AND ici_LastPostedTmp IS NOT NULL AND (UNIX_TIMESTAMP(ici_LastPostedTmp) <= UNIX_TIMESTAMP(ici_LastPosted))) OR (ici_LastTopicID IS NOT NULL AND 

ici_LastPostedTmp IS NULL) THEN

UPDATE {databaseSchema}.{objectQualifier}Forum
   SET
		LastPosted = ici_LastPosted,
				LastTopicID = ici_LastTopicID,
				LastMessageID = ici_LastMessageID,
				LastUserID = ici_LastUserID,
				LastUserName = ici_LastUserName,
				LastUserDisplayName = ici_LastUserDisplayName
 WHERE ForumID = i_ForumID;

END IF;

 CALL {databaseSchema}.{objectQualifier}forum_updatestats(i_ForumID);



--   max value  in the current forum we compare with its peers to use in parent

IF ici_ParentID >0  THEN

	 -- CALL {databaseSchema}.{objectQualifier}forum_updatestats(i_ForumID);

/* In peers to use in parent*/
SET ici_tmpMaxPosted3 =
(SELECT MAX(LastPosted)
FROM {databaseSchema}.{objectQualifier}Forum
WHERE ParentID = ici_ParentID
AND ForumID != i_ForumID AND LastPosted IS NOT NULL ORDER BY LastPosted DESC LIMIT 1);


IF ici_tmpMaxPosted3 IS NOT NULL AND ici_LastPosted IS NULL THEN
SET  ici_MaxTPosted = ici_tmpMaxPosted3; END IF;

IF
(ici_tmpMaxPosted3 IS NULL AND ici_LastPosted IS NOT NULL)
OR ((ici_tmpMaxPosted3 IS NOT NULL AND ici_LastPosted IS NOT NULL)
AND (UNIX_TIMESTAMP(ici_tmpMaxPosted3) <= UNIX_TIMESTAMP(ici_LastPosted))) THEN
SET ici_MaxTPosted = ici_LastPosted; END IF;

IF ici_tmpMaxPosted3 IS NOT NULL
AND ici_LastPosted IS NOT NULL
AND UNIX_TIMESTAMP(ici_tmpMaxPosted3) > UNIX_TIMESTAMP(ici_LastPosted) THEN
SET ici_MaxTPosted = ici_tmpMaxPosted3; END IF;

SET ici_tmpMaxPosted3 = NULL;

/* In parent themes
SELECT DISTINCTROW LastPosted
INTO ici_tmpMaxPosted3
FROM {databaseSchema}.{objectQualifier}Topic
WHERE ForumID=ici_ParentID ORDER BY LastPosted LIMIT 1;


IF ici_tmpMaxPosted3 IS NOT NULL AND ici_MaxTPosted IS NULL THEN
SET ici_MaxTPosted = ici_tmpMaxPosted3;
END IF;

IF ici_tmpMaxPosted3 IS NOT NULL
AND ici_LastPosted IS NOT NULL
AND UNIX_TIMESTAMP(ici_tmpMaxPosted3) > UNIX_TIMESTAMP(ici_MaxTPosted) THEN
SET ici_MaxTPosted = ici_tmpMaxPosted3;
END IF; */

IF ici_MaxTPosted IS NOT NULL THEN
SELECT  LastPosted,
LastTopicID,
LastMessageID,
LastUserID,
LastUserName,
LastUserDisplayName
INTO ici_LastPosted,ici_LastTopicID,ici_LastMessageID,ici_LastUserID,ici_LastUserName,ici_LastUserDisplayName
FROM {databaseSchema}.{objectQualifier}Forum
WHERE LastPosted =ici_MaxTPosted ORDER BY LastPosted DESC LIMIT 1;



	  UPDATE {databaseSchema}.{objectQualifier}Forum
		  SET
		LastPosted = ici_LastPosted,
				LastTopicID = ici_LastTopicID,
				LastMessageID = ici_LastMessageID,
				LastUserID = ici_LastUserID,
				LastUserName = ici_LastUserName,
				LastUserDisplayName = ici_LastUserDisplayName               
	  WHERE ForumID = ici_ParentID;
END IF;
CALL {databaseSchema}.{objectQualifier}forum_updatestats(ici_ParentID);



SELECT  ParentID INTO  ici_tmpParent 
  FROM  {databaseSchema}.{objectQualifier}Forum
  WHERE ForumID = ici_ParentID LIMIT 1;


 -- Here we set new values in parents

WHILE ici_tmpParent  > 0 DO
IF ici_tmpParent > 0 THEN
SET ici_MaxTPosted =
(SELECT MAX(LastPosted)
FROM {databaseSchema}.{objectQualifier}Forum
WHERE ParentID = ici_tmpParent
AND LastPosted IS NOT NULL
ORDER BY LastPosted DESC LIMIT 1);
IF ici_MaxTPosted IS NOT NULL THEN
SELECT  LastPosted,
LastTopicID,
LastMessageID,
LastUserID,
LastUserName,
LastUserDisplayName
INTO ici_LastPosted,
ici_LastTopicID,
ici_LastMessageID,
ici_LastUserID,
ici_LastUserName,
ici_LastUserDisplayName
FROM {databaseSchema}.{objectQualifier}Forum
WHERE LastPosted =ici_MaxTPosted ORDER BY LastPosted DESC LIMIT 1;
END IF;

		UPDATE {databaseSchema}.{objectQualifier}Forum SET
				LastPosted = ici_LastPosted,
				LastTopicID = ici_LastTopicID,
				LastMessageID = ici_LastMessageID,
				LastUserID = ici_LastUserID,
				LastUserName = ici_LastUserName,
				LastUserDisplayName = ici_LastUserDisplayName
			WHERE
				ForumID = ici_tmpParent
		AND ((UNIX_TIMESTAMP(LastPosted) <= UNIX_TIMESTAMP(ici_LastPosted))
		OR LastPosted IS NULL);
		CALL {databaseSchema}.{objectQualifier}forum_updatestats(ici_tmpParent);
 END IF;
		 SELECT  ParentID INTO  ici_tmpParent
  FROM  {databaseSchema}.{objectQualifier}Forum
  WHERE ForumID = ici_tmpParent LIMIT 1;

  END WHILE;

  END IF;
END;
--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM forum_updatelastpost */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forum_updatestats(i_ForumID INT)
BEGIN
		UPDATE {databaseSchema}.{objectQualifier}Forum 
		   SET 
		NumPosts = (SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x 
							   JOIN {databaseSchema}.{objectQualifier}Topic y 
								  ON y.TopicID=x.TopicID 
								   WHERE y.ForumID = i_ForumID 
								   -- message is approved
									 AND (x.Flags & 16) = 16
									 -- topic is not deleted
									   AND (y.Flags & 8) != 8
									   -- message is not deleted
										  AND (x.Flags & 8) != 8),
		NumTopics = (SELECT COUNT(distinct x.TopicID) FROM {databaseSchema}.{objectQualifier}Topic x 
							   JOIN {databaseSchema}.{objectQualifier}Message y 
								  ON y.TopicID=x.TopicID 
								   WHERE x.ForumID = i_ForumID 
									AND (y.Flags & 16) = 16 AND (y.Flags & 8) <> 8
										   AND (x.Flags & 8) <> 8 )
	WHERE ForumID=i_ForumID;
END;
--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forumaccess_group(i_GroupID INT, i_UserID INT, i_IncludeUserForums TINYINT(1)) 
 BEGIN
	SELECT 
		 a.*,
		 b.Name AS ForumName,
		 c.Name AS CategoryName,
		 b.CategoryID AS CategoryID,
		 b.ParentID AS ParentID,
		 brd.Name  AS BoardName
	FROM 
		{databaseSchema}.{objectQualifier}ForumAccess a
		INNER JOIN {databaseSchema}.{objectQualifier}Forum b on b.ForumID=a.ForumID
		INNER JOIN {databaseSchema}.{objectQualifier}Category c on c.CategoryID=b.CategoryID
		INNER JOIN {databaseSchema}.{objectQualifier}Board brd on brd.BoardID=c.BoardID	
	WHERE 
		a.GroupID = i_GroupID and (i_IncludeUserForums <> 1 or (b.IsUserForum = 1 and b.CreatedByUserID = i_UserID))
	ORDER BY 
		brd.Name,
		c.SortOrder,
		b.SortOrder;
 END;
--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forumaccess_list(
				i_ForumID INT, i_PersonalGroupUserID INT, i_IncludeUserGroups TINYINT(1), i_IncludeCommonGroups TINYINT(1), i_IncludeAdminGroups TINYINT(1) )
BEGIN
		SELECT a.*,
			   b.Name AS GroupName
		FROM   {databaseSchema}.{objectQualifier}ForumAccess a
			   INNER JOIN {databaseSchema}.{objectQualifier}Group b ON b.GroupID=a.GroupID
		WHERE  a.ForumID = i_ForumID 
		AND 
		b.IsUserGroup = (case when i_IncludeUserGroups = 1 and i_IncludeCommonGroups = 0 AND b.CreatedByUserID = i_PersonalGroupUserID then 1
		else 0 end)
		AND
		((i_IncludeCommonGroups = 1 AND i_IncludeAdminGroups = 1) OR b.IsHidden = i_IncludeAdminGroups);	       
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}forumaccess_save(
				i_ForumID      INT,
				i_GroupID      INT,
				i_AccessMaskID INT)
BEGIN
		UPDATE {databaseSchema}.{objectQualifier}ForumAccess
		SET    AccessMaskID = i_AccessMaskID
		WHERE  ForumID = i_ForumID
		AND GroupID = i_GroupID;
	END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
		CREATE  PROCEDURE {databaseSchema}.{objectQualifier}group_delete(
		i_GroupID INT)
		BEGIN
		 DELETE FROM {databaseSchema}.{objectQualifier}EventLogGroupAccess
		WHERE       GroupID = i_GroupID;
		DELETE FROM {databaseSchema}.{objectQualifier}ForumAccess
		WHERE       GroupID = i_GroupID;
		DELETE FROM {databaseSchema}.{objectQualifier}UserGroup
		WHERE       GroupID = i_GroupID;
		DELETE FROM {databaseSchema}.{objectQualifier}Group
		WHERE       GroupID = i_GroupID;
		END;

--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
		CREATE  PROCEDURE {databaseSchema}.{objectQualifier}group_list(
		i_BoardID INT,
		i_GroupID INT,
		i_PageIndex int,
		i_PageSize int)
		BEGIN
		  declare ici_TotalRows int ;
		  declare ici_FirstSelectRowNumber int ;
		  declare ici_FirstSelectRowID int;	  
 IF i_GroupID IS NULL THEN
		set i_PageIndex = i_PageIndex + 1;  
		select  count(1) into  ici_TotalRows FROM   {databaseSchema}.{objectQualifier}Group
					   WHERE  BoardID = i_BoardID;
	
	   select  (i_PageIndex - 1) * i_PageSize into ici_FirstSelectRowNumber;
	   
	   set @biplpr = CONCAT('select
		b.*,
		{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
		FROM   {databaseSchema}.{objectQualifier}Group b
		WHERE  b.BoardID = ',i_BoardID,'
		order by b.SortOrder  LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');
		PREPARE stmt_els FROM @biplpr;
		EXECUTE stmt_els;
		DEALLOCATE PREPARE stmt_els;
   ELSE
		SELECT *,
		{databaseSchema}.{objectQualifier}biginttoint(0) AS TotalRows
		FROM   {databaseSchema}.{objectQualifier}Group
		WHERE  BoardID = i_BoardID
		AND GroupID = i_GroupID LIMIT 1;
		END IF;
		END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
		CREATE  PROCEDURE {databaseSchema}.{objectQualifier}group_byuserlist(
		i_BoardID INT,
		i_GroupID INT,
		i_UserID INT,
		i_IsUserGroup TINYINT(1))
		BEGIN

		IF i_GroupID IS NULL THEN
		SELECT *
		FROM   {databaseSchema}.{objectQualifier}Group
		WHERE  BoardID = i_BoardID and CreatedByUserID = i_UserID  order by SortOrder;
		ELSE
		SELECT *
		FROM   {databaseSchema}.{objectQualifier}Group
		WHERE  BoardID = i_BoardID and CreatedByUserID = i_UserID 
		AND GroupID = i_GroupID LIMIT 1;
		END IF;
		END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

 CREATE PROCEDURE
 {databaseSchema}.{objectQualifier}group_medal_delete
	(i_GroupID INT,
	i_MedalID  INT)
 BEGIN
 
	DELETE FROM {databaseSchema}.{objectQualifier}GroupMedal WHERE `GroupID`=i_GroupID AND `MedalID`=i_MedalID;
 
 END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}group_medal_list
	(i_GroupID INT,
	i_MedalID  INT)
 BEGIN
 
	SELECT 
		a.MedalID,
		a.Name,
		a.MedalURL,
		a.RibbonURL,
		a.SmallMedalURL,
		a.SmallRibbonURL,
		a.SmallMedalWidth,
		a.SmallMedalHeight,
		a.SmallRibbonWidth,
		a.SmallRibbonHeight,
		b.SortOrder,
		a.Flags,
		c.Name AS GroupName,
		b.GroupID,
		IFNULL(b.`Message`,a.`Message`) AS `Message`,
		b.`Message` AS `MessageEx`,
		b.`Hide`,
		b.`OnlyRibbon`,
		b.`SortOrder` AS CurrentSortOrder
	FROM
		{databaseSchema}.{objectQualifier}Medal a
		INNER JOIN {databaseSchema}.{objectQualifier}GroupMedal b ON b.`MedalID` = a.`MedalID`
		INNER JOIN {databaseSchema}.{objectQualifier}Group c ON  c.`GroupID` = b.`GroupID`
	WHERE
		(i_GroupID IS NULL OR b.`GroupID` = i_GroupID) AND
		(i_MedalID IS NULL OR b.`MedalID` = i_MedalID)		
	ORDER BY
		c.`Name` ASC,
		b.`SortOrder` ASC;
 
END;
--GO



/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}group_medal_save
	(i_GroupID   INT,
	i_MedalID    INT,
	i_Message    VARCHAR(128),
	i_Hide       TINYINT(1),
	i_OnlyRibbon TINYINT(1),
	i_SortOrder  TINYINT(3))
 BEGIN
 
	IF EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}GroupMedal WHERE `GroupID`=i_GroupID AND `MedalID`=i_MedalID) THEN
		UPDATE {databaseSchema}.{objectQualifier}GroupMedal
		SET
			Message = i_Message,
			Hide = i_Hide,
			OnlyRibbon = i_OnlyRibbon,
			SortOrder = i_SortOrder
		WHERE
			GroupID=i_GroupID and 
			MedalID=i_MedalID;
	
	ELSE
 
		INSERT INTO {databaseSchema}.{objectQualifier}GroupMedal
			(`GroupID`,`MedalID`,`Message`,`Hide`,`OnlyRibbon`,`SortOrder`)
		VALUES
			(i_GroupID,i_MedalID,i_Message,i_Hide,i_OnlyRibbon,i_SortOrder);
	END IF;
 
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
		CREATE  PROCEDURE {databaseSchema}.{objectQualifier}group_member(
		i_BoardID INT,
		i_UserID  INT)
		BEGIN
		SELECT   a.GroupID,
		a.Name,
		(a.Flags & 16 = 16) as IsHidden,
		a.IsUserGroup,
		a.Style, 
		(SELECT COUNT(1)
		FROM   {databaseSchema}.{objectQualifier}UserGroup x
		WHERE  x.UserID = i_UserID
		AND x.GroupID = a.GroupID) AS Member
		FROM     {databaseSchema}.{objectQualifier}Group a
		WHERE    a.BoardID = i_BoardID
		ORDER BY a.Name;
		END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */ 
		CREATE  PROCEDURE {databaseSchema}.{objectQualifier}group_save(
		i_GroupID      INT,
		i_BoardID      INT,
		i_Name         VARCHAR(128),
		i_IsAdmin      TINYINT(1),
		i_IsGuest      TINYINT(1),
		i_IsStart      TINYINT(1),
		i_IsModerator  TINYINT(1),
		i_IsHidden  TINYINT(1),
		i_AccessMaskID INT,
		i_PMLimit INT,
		i_Style VARCHAR(255),
		i_SortOrder INT,        
		i_Description VARCHAR(255),
		i_UsrSigChars INT,
		i_UsrSigBBCodes	VARCHAR(255),
		i_UsrSigHTMLTags VARCHAR(255),
		i_UsrAlbums INT,
		i_UsrAlbumImages INT,
	 IN i_UserID          INT,
	 IN i_IsUserGroup      TINYINT(1),
	 i_PersonalAccessMasksNumber INT,
	 i_PersonalGroupsNumber INT,
	 i_PersonalForumsNumber INT,
	 IN i_UTCTIMESTAMP    DATETIME)
		BEGIN
		DECLARE ici_UserName  VARCHAR(255);
		DECLARE ici_UserDisplayName  VARCHAR(255);
		DECLARE  iciFlags INT;
		SET iciFlags = 0;
		IF i_IsAdmin <> 0 THEN
		SET iciFlags = iciFlags | 1 ; END IF;
		IF i_IsGuest <> 0 THEN
		SET iciFlags = iciFlags | 2; END IF;
		IF i_IsStart <> 0 THEN
		SET iciFlags = iciFlags | 4; END IF;
		IF i_IsModerator <> 0 THEN
		SET iciFlags = iciFlags | 8; END IF;
		IF i_IsHidden <> 0 THEN
		SET iciFlags = iciFlags | 16; END IF;
		IF CHAR_LENGTH(i_Style) <= 2 THEN
		SET i_Style = NULL; END IF;

		 IF i_UserID IS NOT NULL THEN   
	SELECT Name, DisplayName INTO ici_UserName, ici_UserDisplayName FROM {databaseSchema}.{objectQualifier}User where UserID = i_UserID LIMIT  1;
	   -- guests should not create forums
	ELSE    
	SELECT Name, DisplayName INTO ici_UserName, ici_UserDisplayName FROM {databaseSchema}.{objectQualifier}User where BoardID = i_BoardID and (Flags & 4) = 4  ORDER BY Joined LIMIT 1;
	END IF;

		IF i_GroupID > 0 THEN        
			UPDATE {databaseSchema}.{objectQualifier}Group
			SET `Name` = i_Name,
				 Flags = iciFlags,
				 PMLimit = i_PMLimit,
				 Style = i_Style,
				 SortOrder = i_SortOrder, 			
				 Description = i_Description,
				 UsrSigChars = i_UsrSigChars,
				 UsrSigBBCodes = i_UsrSigBBCodes,
				 UsrSigHTMLTags = i_UsrSigHTMLTags,
				 UsrAlbums = i_UsrAlbums,
				 UsrAlbumImages = i_UsrAlbumImages,
				 IsUserGroup =  i_IsUserGroup,
				 IsHidden = i_IsHidden,
				 UsrPersonalMasks = i_PersonalAccessMasksNumber,
				 UsrPersonalGroups = i_PersonalGroupsNumber,
				 UsrPersonalForums = i_PersonalForumsNumber				
			WHERE  GroupID = i_GroupID;        
		ELSE        
			INSERT INTO {databaseSchema}.{objectQualifier}Group
					   (`Name`,
						BoardID,
						Flags,
						PMLimit,
						Style,
						SortOrder,
						Description,
						UsrSigChars,
						UsrSigBBCodes,
						UsrSigHTMLTags,
						UsrAlbums,
						UsrAlbumImages,
						IsUserGroup,
						IsHidden,
						CreatedByUserID,
						CreatedByUserName,
						CreatedByUserDisplayName,
						CreatedDate,
						UsrPersonalMasks,
						UsrPersonalGroups,
						UsrPersonalForums 
						)
			VALUES     (i_Name,
						i_BoardID,
						iciFlags,
						i_PMLimit,
						i_Style,
						i_SortOrder,
						i_Description,
						i_UsrSigChars,
						i_UsrSigBBCodes,
						i_UsrSigHTMLTags,
						i_UsrAlbums,
						i_UsrAlbumImages,
						i_IsUserGroup,
						i_IsHidden,
						i_UserID,
						ici_UserName,
						ici_UserDisplayName,
						i_UTCTIMESTAMP,
						i_PersonalAccessMasksNumber,
						i_PersonalGroupsNumber,
						i_PersonalForumsNumber
						);
			SET i_GroupID = LAST_INSERT_ID();
			INSERT INTO {databaseSchema}.{objectQualifier}ForumAccess
					   (GroupID,
						ForumID,
						AccessMaskID)
			SELECT i_GroupID,
				   a.ForumID,
				   i_AccessMaskID
			FROM   {databaseSchema}.{objectQualifier}Forum a
				   JOIN {databaseSchema}.{objectQualifier}Category b
					 ON b.CategoryID = a.CategoryID
			WHERE  b.BoardID = i_BoardID;
		 END IF; 
		 IF (i_Style is not null and char_length(i_Style > 2)) THEN 		
		CALL {databaseSchema}.{objectQualifier}user_savestyle(i_GroupID, null);
		END IF;
	   
	if exists (select 1 from {databaseSchema}.{objectQualifier}GroupHistory where GroupID = i_GroupID and ChangedDate = i_UTCTIMESTAMP  LIMIT 1) THEN
	update {databaseSchema}.{objectQualifier}GroupHistory set 
		   ChangedUserID = i_UserID,	
		   ChangedUserName = ici_UserName,
		   ChangedDisplayName = ici_UserDisplayName
	 where  GroupID = i_GroupID  and ChangedDate = i_UTCTIMESTAMP; 
	else    
	INSERT INTO {databaseSchema}.{objectQualifier}GroupHistory(GroupID,ChangedUserID,ChangedUserName,ChangedDisplayName,ChangedDate)
	VALUES (i_GroupID, i_UserID,ici_UserName,ici_UserDisplayName,i_UTCTIMESTAMP);
	end IF;
		SELECT i_GroupID AS GroupID; 
	END;
--GO

-- to split it it returns columns only with style
CREATE procedure {databaseSchema}.{objectQualifier}group_rank_style( i_BoardID int) 
begin
SELECT 1 AS LegendID,`Name`,Style,Description,PMLimit,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages FROM {databaseSchema}.{objectQualifier}Group
WHERE BoardID = i_BoardID AND Length(Style) > 2   GROUP BY SortOrder,`Name`,Style,Description,PMLimit,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages
UNION
SELECT 2  AS LegendID,`Name`,Style,Description,PMLimit,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages FROM {databaseSchema}.{objectQualifier}Rank
WHERE BoardID = i_BoardID AND Length(Style) > 2 GROUP BY SortOrder,`Name`,Style,Description,PMLimit,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages;
end;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}mail_create
 (
	i_From     VARCHAR(128),
	i_FromName VARCHAR(128),
	i_To       VARCHAR(128),
	i_ToName   VARCHAR(128),
	i_Subject  VARCHAR(128),
	i_Body     TEXT,
	i_BodyHtml TEXT,
	i_UTCTIMESTAMP DATETIME
 )

 BEGIN
	INSERT INTO {databaseSchema}.{objectQualifier}Mail
		(FromUser,FromUserName,ToUser,ToUserName,Created,Subject,Body,BodyHtml,sendtries)
	VALUES
		(i_From,i_FromName,i_To,i_ToName,i_UTCTIMESTAMP,i_Subject,i_Body,i_BodyHtml,0);	
 END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 create procedure {databaseSchema}.{objectQualifier}mail_createwatch
 (
	i_TopicID   INT,
	i_FROM      VARCHAR(128),
	i_FROMName  VARCHAR(128),
	i_Subject   VARCHAR(255),
	i_Body      TEXT,
	i_BodyHtml  TEXT,
	i_UserID    INT,
	i_UTCTIMESTAMP DATETIME
 )
 BEGIN
	INSERT INTO {databaseSchema}.{objectQualifier}Mail(
		 FromUser,
		 FromUserName,
		 ToUser,
		 ToUserName,
		 Created,
		 `Subject`,
		 Body,
		 BodyHtml)
	SELECT
		i_FROM,
		i_FROMName,
		b.Email,
		b.Name,
		i_UTCTIMESTAMP,
		i_Subject,
		i_Body,
		i_BodyHtml
	FROM
		{databaseSchema}.{objectQualifier}WatchTopic a
		INNER JOIN {databaseSchema}.{objectQualifier}User b on b.UserID=a.UserID
	WHERE
		b.UserID <> i_UserID AND
		b.NotificationType NOT IN (10, 20) AND		
		a.TopicID = i_TopicID AND
		(a.LastMail IS NULL OR UNIX_TIMESTAMP(a.LastMail) < UNIX_TIMESTAMP(b.LastVisit));
	
	INSERT INTO {databaseSchema}.{objectQualifier}Mail(
	FromUser,
	FromUserName,
	ToUser,
	ToUserName,
	Created,
	`Subject`,
	Body,
	BodyHtml)
	SELECT
		i_From,
		i_FromName,
		b.Email,
		b.Name,
		i_UTCTIMESTAMP,
		i_Subject,
		i_Body,
		i_BodyHtml
	FROM
		{databaseSchema}.{objectQualifier}WatchForum a
		INNER JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID=a.UserID
		INNER JOIN {databaseSchema}.{objectQualifier}Topic c ON c.ForumID=a.ForumID
	WHERE
		b.UserID <> i_UserID AND
		b.NotificationType NOT IN (10, 20) AND		
		c.TopicID = i_TopicID AND
		(a.LastMail IS NULL OR UNIX_TIMESTAMP(a.LastMail) < UNIX_TIMESTAMP(b.LastVisit)) AND
		NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}WatchTopic x WHERE x.UserID=b.UserID AND x.TopicID=c.TopicID);
 
	UPDATE {databaseSchema}.{objectQualifier}WatchTopic SET LastMail = i_UTCTIMESTAMP
	WHERE TopicID = i_TopicID
	AND UserID <> i_UserID;
	
	UPDATE {databaseSchema}.{objectQualifier}WatchForum SET LastMail = i_UTCTIMESTAMP
	WHERE ForumID = (SELECT ForumID FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicID = i_TopicID)
	AND UserID <> i_UserID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}mail_delete(i_MailID INT)
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}Mail WHERE MailID = i_MailID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}mail_list
(
	i_ProcessID INT,
	i_UTCTIMESTAMP DATETIME
 )
 BEGIN
 CREATE TEMPORARY TABLE IF NOT EXISTS tmp_table55 SELECT  MailID FROM {databaseSchema}.{objectQualifier}Mail WHERE SendAttempt < i_UTCTIMESTAMP OR SendAttempt IS NULL ORDER BY SendAttempt, Created LIMIT 10;
	  UPDATE {databaseSchema}.{objectQualifier}Mail
			  SET 
			ProcessID = NULL
		WHERE
			ProcessID IS NOT NULL AND SendAttempt > i_UTCTIMESTAMP;
   
	UPDATE {databaseSchema}.{objectQualifier}Mail
	SET 
		SendTries = SendTries + 1,
		SendAttempt = ADDDATE(i_UTCTIMESTAMP, INTERVAL 5 MINUTE),
		ProcessID = i_ProcessID
	WHERE
		MailID IN (SELECT MailID FROM tmp_table55 ORDER BY SendAttempt, Created desc);
 
	/*now select all mail reserved for this process...*/
	SELECT * FROM {databaseSchema}.{objectQualifier}Mail WHERE ProcessID = i_ProcessID ORDER BY SendAttempt, Created LIMIT 10;
		   DROP TABLE IF EXISTS tmp_table55;  
 END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}medal_delete(i_BoardID	INT,
	i_MedalID	INT,
	i_Category	VARCHAR(128))
BEGIN
 
	IF i_MedalID IS NOT NULL THEN
		DELETE from {databaseSchema}.{objectQualifier}GroupMedal WHERE `MedalID` = i_MedalID;
		DELETE from {databaseSchema}.{objectQualifier}UserMedal WHERE `MedalID` = i_MedalID; 
		DELETE from {databaseSchema}.{objectQualifier}Medal WHERE `MedalID`=i_MedalID;
	
	ELSEIF i_Category IS NOT NULL AND i_BoardID IS NOT NULL THEN
		DELETE from {databaseSchema}.{objectQualifier}GroupMedal 
			WHERE `MedalID` in (SELECT `MedalID` FROM {databaseSchema}.{objectQualifier}Medal WHERE `Category`=i_Category and `BoardID`=i_BoardID);
 
		DELETE from {databaseSchema}.{objectQualifier}UserMedal 
			WHERE `MedalID` in (SELECT `MedalID` FROM {databaseSchema}.{objectQualifier}Medal WHERE `Category`=i_Category and `BoardID`=i_BoardID);
 
		DELETE from {databaseSchema}.{objectQualifier}Medal WHERE `Category`=i_Category;
	
	ELSEIF  i_BoardID IS NOT NULL THEN
		DELETE from {databaseSchema}.{objectQualifier}GroupMedal 
			WHERE `MedalID` in (SELECT `MedalID` FROM {databaseSchema}.{objectQualifier}Medal WHERE `BoardID`=i_BoardID);
 
		DELETE from {databaseSchema}.{objectQualifier}UserMedal 
			WHERE `MedalID` in (SELECT `MedalID` FROM {databaseSchema}.{objectQualifier}Medal WHERE `BoardID`=i_BoardID);

		DELETE from {databaseSchema}.{objectQualifier}Medal WHERE `BoardID`=i_BoardID;
	END IF;
 
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}medal_list
	(i_BoardID	INT,
	i_MedalID	INT,
	i_Category	VARCHAR(128))
BEGIN
 
	IF i_MedalID IS NOT NULL THEN
		SELECT 
			* 
		FROM 
			{databaseSchema}.{objectQualifier}Medal 
		WHERE 
			`MedalID`=i_MedalID 
		ORDER BY 
			`Category` ASC, 
			`SortOrder` ASC;
	
	ELSEIF i_Category IS NOT NULL AND  i_BoardID IS NOT NULL THEN
		SELECT 
			* 
		FROM 
			{databaseSchema}.{objectQualifier}Medal 
		WHERE 
			`Category`=i_Category and `BoardID`=i_BoardID
		ORDER BY 
			`Category` ASC, 
			`SortOrder` ASC;
	ELSEIF i_BoardID IS NOT NULL THEN
		SELECT 
			* 
		FROM 
			{databaseSchema}.{objectQualifier}Medal 
		WHERE 
			`BoardID`=i_BoardID
		ORDER BY 
			`Category` ASC, 
			`SortOrder` ASC;
	  END IF; 
END;
--GO



/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}medal_listusers
	(i_MedalID	INT)
  BEGIN
 
	(SELECT 
		a.UserID, a.Name
	FROM 
		{databaseSchema}.{objectQualifier}User a
		INNER JOIN {databaseSchema}.{objectQualifier}UserMedal b ON a.`UserID` = b.`UserID`
	WHERE
		b.`MedalID`=i_MedalID)
	UNION	
 
	(SELECT 
		a.UserID, a.Name
	FROM 
		{databaseSchema}.{objectQualifier}User a
		INNER JOIN {databaseSchema}.{objectQualifier}UserGroup b ON a.`UserID` = b.`UserID`
		INNER JOIN {databaseSchema}.{objectQualifier}GroupMedal c ON b.`GroupID` = c.`GroupID`
	WHERE
		c.`MedalID`=i_MedalID); 
 
 
END;
 --GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}medal_resort
	(i_BoardID INT,i_MedalID INT,i_Move INT)

BEGIN
	DECLARE i_Position INT;
	DECLARE i_Category VARCHAR(128);
 
	SELECT 
		`SortOrder`,
		`Category`
		INTO i_Position,i_Category
	FROM 
		{databaseSchema}.{objectQualifier}Medal 
	WHERE 
		`BoardID`=i_BoardID and `MedalID`=i_MedalID;
 
	IF (i_Position IS NOT NULL) THEN
 
	IF (i_Move > 0) THEN
		UPDATE 
			{databaseSchema}.{objectQualifier}Medal
		SET 
			`SortOrder`=`SortOrder`-1
		WHERE 
			`BoardID`=i_BoardID and 
			`Category`=i_Category and
			`SortOrder` between i_Position 
						 AND (i_Position + i_Move) 
						 AND `SortOrder` between 1 and 255; 	
	ELSEIF (i_Move < 0) THEN
		UPDATE
			{databaseSchema}.{objectQualifier}Medal
		SET
			`SortOrder`=`SortOrder`+1
		WHERE 
			BoardID=i_BoardID AND 
			`Category`=i_Category AND
			`SortOrder` BETWEEN (i_Position+i_Move) AND i_Position AND
			`SortOrder` BETWEEN 0 and 254;
	END IF;
 
	SET i_Position = i_Position + i_Move;
 
	IF (i_Position>255) THEN SET i_Position = 255;
	ELSEIF (i_Position<0) THEN 
		SET i_Position = 0; END IF;
	UPDATE {databaseSchema}.{objectQualifier}Medal
		SET `SortOrder`=i_Position
		WHERE `BoardID`=i_BoardID AND 
			`MedalID`=i_MedalID;
		END IF;
END;
--GO



/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}medal_save
 (	i_BoardID INT,
	i_MedalID INT,
	i_Name VARCHAR(128),
	i_Description TEXT,
	i_Message VARCHAR(128),
	i_Category VARCHAR(128),
	i_MedalURL VARCHAR(250),
	i_RibbonURL VARCHAR(250),
	i_SmallMedalURL VARCHAR(250),
	i_SmallRibbonURL VARCHAR(250),
	i_SmallMedalWidth SMALLINT,
	i_SmallMedalHeight SMALLINT,
	i_SmallRibbonWidth SMALLINT,
	i_SmallRibbonHeight SMALLINT,
	i_SortOrder TINYINT(3),
	i_Flags INT)
BEGIN
DECLARE rcount integer;
 
	IF i_MedalID IS NULL THEN
		INSERT INTO {databaseSchema}.{objectQualifier}Medal
			(`BoardID`,`Name`,`Description`,`Message`,`Category`,
			`MedalURL`,`RibbonURL`,`SmallMedalURL`,`SmallRibbonURL`,
			`SmallMedalWidth`,`SmallMedalHeight`,`SmallRibbonWidth`,`SmallRibbonHeight`,
			`SortOrder`,`Flags`)
		VALUES
			(i_BoardID,i_Name,i_Description,i_Message,i_Category,
			i_MedalURL,i_RibbonURL,i_SmallMedalURL,i_SmallRibbonURL,
			i_SmallMedalWidth,i_SmallMedalHeight,i_SmallRibbonWidth,i_SmallRibbonHeight,
			i_SortOrder,i_Flags);
 SET rcount = (SELECT ROW_COUNT());
		SELECT rcount;

	
	ELSE 
		UPDATE {databaseSchema}.{objectQualifier}Medal
			SET `BoardID` = BoardID,
				`Name` = i_Name,
				`Description` = i_Description,
				`Message` = i_Message,
				`Category` = i_Category,
				`MedalURL` = i_MedalURL,
				`RibbonURL` = i_RibbonURL,
				`SmallMedalURL` = i_SmallMedalURL,
				`SmallRibbonURL` = i_SmallRibbonURL,
				`SmallMedalWidth` = i_SmallMedalWidth,
				`SmallMedalHeight` = i_SmallMedalHeight,
				`SmallRibbonWidth` = i_SmallRibbonWidth,
				`SmallRibbonHeight` = i_SmallRibbonHeight,
				`SortOrder` = i_SortOrder,
				`Flags` = i_Flags
		WHERE `MedalID` = i_MedalID; 
	 SET rcount = (SELECT ROW_COUNT());               
				SELECT rcount;
	 END IF;
END;
--GO



/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_approve(i_MessageID INT) 
BEGIN
	DECLARE	ici_UserID	INT;
	DECLARE	ici_ForumID	INT;
	DECLARE	ici_ParentID	INT;
	DECLARE	ici_TopicID	INT;
	DECLARE ici_Posted	DATETIME;
	DECLARE	ici_UserName	VARCHAR(255);
	DECLARE	ici_UserDisplayName	VARCHAR(255);
	DECLARE	ici_NewFlag	INT; 	
 
	SELECT 
		 a.UserID,
		 a.TopicID,
		 b.ForumID,
		 a.Posted,
		 a.UserName,
		 a.UserDisplayName,
		 a.Flags
		INTO ici_UserID,ici_TopicID,ici_ForumID,ici_Posted,ici_UserName,ici_UserDisplayName, ici_NewFlag
	FROM
		{databaseSchema}.{objectQualifier}Message a
		inner join {databaseSchema}.{objectQualifier}Topic b on b.TopicID=a.TopicID
	WHERE
		a.MessageID = i_MessageID;
 
	/* update Message table, set meesage flag to approved */
	UPDATE {databaseSchema}.{objectQualifier}Message 
	 SET
		Flags = Flags | 16
	WHERE MessageID = i_MessageID;

	/*update User table to increase postcount*/
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Forum WHERE ForumID=ici_ForumID AND (Flags & 4)=0 LIMIT 1) THEN
	
		UPDATE {databaseSchema}.{objectQualifier}User set NumPosts = NumPosts + 1 where UserID = ici_UserID;
		-- upgrade user, i.e. promote rank if conditions allow it
		CALL {databaseSchema}.{objectQualifier}user_upgrade (ici_UserID);
	
 END IF;
	/*update Forum table with last topic/post info*/

  SELECT  ParentID INTO  ici_ParentID
  FROM  {databaseSchema}.{objectQualifier}Forum
  WHERE ForumID = ici_ForumID LIMIT 1;  
 
	UPDATE {databaseSchema}.{objectQualifier}Forum set
		LastPosted = ici_Posted,
		LastTopicID = ici_TopicID,
		LastMessageID = i_MessageID,
		LastUserID = ici_UserID ,
		LastUserName = ici_UserName,
		LastUserDisplayName = ici_UserDisplayName
	WHERE ForumID = ici_ForumID;
	
	WHILE ici_ParentID > 0 DO
		UPDATE {databaseSchema}.{objectQualifier}Forum SET
				LastPosted = ici_Posted,
				LastTopicID = ici_TopicID,
				LastMessageID = i_MessageID,
				LastUserID = ici_UserID,
				LastUserName = ici_UserName,
				LastUserDisplayName = ici_UserDisplayName
			WHERE
				ForumID = ici_ParentID
		AND ((UNIX_TIMESTAMP(LastPosted) < UNIX_TIMESTAMP(ici_Posted))
		OR LastPosted IS NULL);         
		 SELECT ParentID INTO  ici_ParentID
  FROM  {databaseSchema}.{objectQualifier}Forum
  WHERE ForumID = ici_ParentID LIMIT 1;  
  END WHILE; 	
  

 
	/*update Topic table with info about last post in topic*/
	UPDATE {databaseSchema}.{objectQualifier}Topic set
		LastPosted = ici_Posted,
		LastMessageID = i_MessageID,
		LastMessageFlags = ici_NewFlag | 16,
		LastUserID = ici_UserID,
		LastUserName = ici_UserName,
		LastUserDisplayName = ici_UserDisplayName,	
		NumPosts = (select count(1) from {databaseSchema}.{objectQualifier}Message x WHERE x.TopicID={databaseSchema}.{objectQualifier}Topic.TopicID AND (x.Flags & 16) > 0 AND (x.Flags & 8) = 0)
	WHERE TopicID = ici_TopicID;
	
	/*update forum stats*/
	CALL {databaseSchema}.{objectQualifier}forum_updatestats (ici_ForumID);
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_delete(i_MessageID INT, i_EraseMessage TINYINT(1)) 
BEGIN
	DECLARE ici_TopicID		INT;
	DECLARE ici_ForumID		INT;
	DECLARE ici_ForumID2	INT;
	DECLARE ici_MessageCount	INT;
	DECLARE ici_LastMessageID	INT;
	DECLARE ici_UserID		    INT;

		DECLARE ici_LastTopicID_Check INT;
		DECLARE ici_LastMessageID_Check INT;
		  
	/*Find TopicID and ForumID*/
	SELECT b.TopicID,b.ForumID 
		INTO ici_TopicID,ici_ForumID
		FROM 
			{databaseSchema}.{objectQualifier}Message a
			INNER JOIN  {databaseSchema}.{objectQualifier}Topic b 
			ON b.TopicID=a.TopicID
		WHERE
			a.MessageID=i_MessageID;
 
	/*UPDATE LastMessageID in Topic*/
	UPDATE {databaseSchema}.{objectQualifier}Topic SET 
		LastPosted = NULL,
		LastMessageID = NULL,
		LastUserID = NULL,
		LastUserName = NULL,
		LastUserDisplayName = NULL
	WHERE LastMessageID = i_MessageID;

	/*UPDATE LastMessageID in Forum*/
	UPDATE {databaseSchema}.{objectQualifier}Forum SET 
		LastPosted = NULL,
		LastTopicID = NULL,
		LastMessageID = NULL,
		LastUserID = NULL,
		LastUserName = NULL,
		LastUserDisplayName = NULL
	WHERE LastMessageID = i_MessageID;
 
	SET ici_UserID = (SELECT UserID FROM {databaseSchema}.{objectQualifier}Message WHERE MessageID = i_MessageID);	
 
	/*should it be physically deleter or not*/
	IF i_EraseMessage = 1 THEN
		DELETE FROM {databaseSchema}.{objectQualifier}Attachment WHERE MessageID = i_MessageID;
		DELETE FROM {databaseSchema}.{objectQualifier}MessageReportedAudit WHERE MessageID = i_MessageID;
		DELETE FROM {databaseSchema}.{objectQualifier}MessageReported WHERE MessageID = i_MessageID; 		
		DELETE FROM {databaseSchema}.{objectQualifier}MessageHistory WHERE MessageID = i_MessageID;
		DELETE FROM {databaseSchema}.{objectQualifier}Thanks where MessageID = i_MessageID;
	
		DELETE FROM {databaseSchema}.{objectQualifier}Message WHERE MessageID = i_MessageID; 	
	ELSE
		/*"Delete" it only by setting deleted flag message*/
		UPDATE {databaseSchema}.{objectQualifier}Message SET Flags = Flags | 8
		WHERE MessageID = i_MessageID;
	END IF;
	
	/* UPDATE user post count*/
	UPDATE {databaseSchema}.{objectQualifier}User SET NumPosts = (SELECT count(MessageID) FROM {databaseSchema}.{objectQualifier}Message WHERE UserID = ici_UserID AND (Flags & 16) > 0 AND (Flags & 8)= 0) WHERE UserID = ici_UserID;
	
	/* Delete topic if there are no more messages*/
	SELECT COUNT(1) INTO ici_MessageCount FROM {databaseSchema}.{objectQualifier}Message WHERE TopicID = ici_TopicID AND (Flags & 8)=0;
	IF ici_MessageCount=0 THEN CALL {databaseSchema}.{objectQualifier}topic_delete (ici_TopicID, null, 0, i_EraseMessage); END IF;
 
	/*UPDATE lastpost*/
	CALL {databaseSchema}.{objectQualifier}topic_updatelastpost (ici_ForumID,ici_TopicID);
	CALL {databaseSchema}.{objectQualifier}forum_updatelastpost (ici_ForumID);
	CALL {databaseSchema}.{objectQualifier}forum_updatestats (ici_ForumID);
 
	/*UPDATE topic numposts*/
	UPDATE {databaseSchema}.{objectQualifier}Topic SET
		NumPosts = (SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x WHERE x.TopicID={databaseSchema}.{objectQualifier}Topic.TopicID AND (x.Flags & 16) > 0 AND (x.Flags & 8)= 0)
	WHERE TopicID = ici_TopicID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_deleteundelete
(i_MessageID INT, 
i_isModeratorChanged TINYINT(1), 
i_DeleteReason VARCHAR(128), 
i_isDeleteAction INTEGER) 
BEGIN
	DECLARE ici_TopicID		INT;
	DECLARE ici_ForumID		INT;
	DECLARE ici_ForumID2            INT;
	DECLARE ici_MessageCount	INT;
	DECLARE ici_LastMessageID	INT;
	DECLARE ici_UserID		INT;

		DECLARE ici_LastTopicID_Check   INT;
		DECLARE ici_LastMessageID_Check INT;

	/*Find TopicID and ForumID*/
	SELECT b.TopicID,b.ForumID 
		INTO ici_TopicID,ici_ForumID
	FROM 
		{databaseSchema}.{objectQualifier}Message a
		INNER JOIN {databaseSchema}.{objectQualifier}Topic b 
		ON b.TopicID=a.TopicID
	WHERE 
		a.MessageID=i_MessageID;
 
	/*Update LastMessageID in Topic and Forum*/
	UPDATE {databaseSchema}.{objectQualifier}Topic SET
		LastPosted = NULL,
		LastMessageID = NULL,
		LastUserID = NULL,
		LastUserName = NULL,
		LastUserDisplayName = NULL
	WHERE LastMessageID = i_MessageID;
 
	UPDATE {databaseSchema}.{objectQualifier}Forum 
			SET
		LastPosted = NULL,
		LastTopicID = NULL,
		LastMessageID = NULL,
		LastUserID = NULL,
		LastUserName = NULL,
		LastUserDisplayName = NULL
	   WHERE LastMessageID = i_MessageID;
 /*get the userID for this message...*/ 	
	 SELECT UserID INTO ici_UserID 
	 FROM {databaseSchema}.{objectQualifier}Message 
	 WHERE MessageID = i_MessageID;
 
	/* "Delete" message*/
	 UPDATE {databaseSchema}.{objectQualifier}Message 
	 SET IsModeratorChanged = i_isModeratorChanged 
	 WHERE MessageID = i_MessageID 
	 AND ((Flags & 8) <> i_isDeleteAction*8);
	UPDATE {databaseSchema}.{objectQualifier}Message 
	SET DeleteReason = i_DeleteReason 
	WHERE MessageID = i_MessageID 
	AND ((Flags & 8) <> i_isDeleteAction*8);
	UPDATE {databaseSchema}.{objectQualifier}Message 
	SET Flags = Flags ^ 8 WHERE MessageID =i_MessageID 
	AND ((Flags & 8) <> i_isDeleteAction*8);
	 
	 /* update num posts for user now that the delete/undelete status has been toggled...*/
	 UPDATE {databaseSchema}.{objectQualifier}User SET NumPosts = (SELECT count(MessageID) FROM {databaseSchema}.{objectQualifier}Message WHERE UserID = ici_UserID AND (Flags & 16) > 0 AND (Flags & 8)= 0) WHERE UserID = ici_UserID;
 
	/* Delete topic if there are no more messages*/
	SELECT COUNT(1) INTO ici_MessageCount FROM {databaseSchema}.{objectQualifier}Message WHERE TopicID = ici_TopicID AND (Flags & 8)=0;
	IF ici_MessageCount=0 THEN CALL {databaseSchema}.{objectQualifier}topic_delete (ici_TopicID,null,0,1); END IF;
	/*update lastpost*/
	CALL {databaseSchema}.{objectQualifier}topic_updatelastpost (ici_ForumID,ici_TopicID);
	CALL {databaseSchema}.{objectQualifier}forum_updatelastpost (ici_ForumID);
	CALL {databaseSchema}.{objectQualifier}forum_updatestats (ici_ForumID);
	/* update topic numposts*/
	UPDATE {databaseSchema}.{objectQualifier}Topic set
		NumPosts = (select count(1) from {databaseSchema}.{objectQualifier}Message x WHERE x.TopicID={databaseSchema}.{objectQualifier}Topic.TopicID AND (x.Flags & 16) > 0 AND (x.Flags & 8)= 0 )
	WHERE TopicID = ici_TopicID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
		CREATE  PROCEDURE {databaseSchema}.{objectQualifier}message_findunread(
		 i_TopicID  INT,
		 i_MessageID INT,
		 i_LastRead DATETIME,
		 i_ShowDeleted TINYINT(1),
		 i_AuthorUserID INT
	   )
		BEGIN     
		
		 
DECLARE ici_firstmessageid INT;
DROP TABLE IF EXISTS tbl_msglunr ; 
CREATE TEMPORARY table tbl_msglunr 
(
cntrt INT NOT NULL AUTO_INCREMENT,
MessageID int,
TopicID int,
Posted datetime,
Edited datetime,
PRIMARY KEY (`cntrt`)
);
TRUNCATE tbl_msglunr;
-- find first message id
select   
		m.MessageID INTO ici_firstmessageid
	from
		{databaseSchema}.{objectQualifier}Message m	
	where
		m.TopicID = i_TopicID ORDER BY m.Posted LIMIT 1;


   -- we return last 100 messages ONLY if we look for last unread or lastpost(Messageid = 0)
   if (i_MessageID > 0) THEN   
   -- fill in the table variable with all topic's messages(slow). It's used in cases when we forced to find a particular message. 		
	insert into tbl_msglunr (MessageID,TopicID,Posted,Edited) 
	select  
		m.MessageID,
		m.TopicID,
		m.Posted,
		IFNULL(m.Edited,m.Posted)
	from
		{databaseSchema}.{objectQualifier}Message m	
	where
		m.TopicID = i_TopicID			
		AND (m.Flags & 16)  = 16
	   -- deleted
		AND (i_ShowDeleted = 1 OR m.Flags & 8 <> 8 OR (i_AuthorUserID > 0 AND m.UserID =i_AuthorUserID))  
		 AND UNIX_TIMESTAMP(m.Posted) >	 UNIX_TIMESTAMP(i_LastRead)
	order by		
		m.Posted DESC;		
	else	    
	-- fill in the table variable with last 100 values from topic's messages		
	insert into tbl_msglunr (MessageID,TopicID,Posted,Edited) 
	select  
		m.MessageID,
		m.TopicID,
		m.Posted,
		IFNULL(m.Edited,m.Posted)
	from
		{databaseSchema}.{objectQualifier}Message m	
	where
		m.TopicID = i_TopicID
		-- approved 			
		AND (m.Flags & 16)  = 16
		-- deleted
	   -- deleted
		AND (i_ShowDeleted = 1 OR m.Flags & 8 <> 8 OR (i_AuthorUserID > 0 AND m.UserID =i_AuthorUserID))  
		AND m.Posted >	i_LastRead
	order by		
		m.Posted DESC LIMIT 100;
	END IF;

		 -- simply return last post if no unread message is found
  if EXISTS (SELECT 1 FROM tbl_msglunr LIMIT 1) THEN  
	-- the messageid was already supplied, find a particular message
	if (i_MessageID > 0) THEN	
	   if EXISTS (SELECT 1 FROM tbl_msglunr WHERE TopicID = i_TopicID and MessageID = i_MessageID LIMIT 1) THEN
		
		 -- return last unread		
		   select MessageID, cntrt AS MessagePosition, ici_firstmessageid  AS FirstMessageID
		   from tbl_msglunr
		   where TopicID=i_TopicID and  MessageID = i_MessageID LIMIT 1;		
		
		else
		
		 -- simply return last post if no unread message is found
		   select  MessageID, {databaseSchema}.{objectQualifier}biginttoint(1) AS MessagePosition, ici_firstmessageid  AS FirstMessageID
		   from tbl_msglunr
		   where TopicID=i_TopicID and Posted> i_LastRead
		   order by Posted DESC LIMIT 1;
		end if;
	
	else	
	   -- simply return last message as no MessageID was supplied 
	   if EXISTS (SELECT 1 FROM tbl_msglunr WHERE Posted > i_LastRead LIMIT 1)	THEN    
		 -- return last unread		
		   select  MessageID, cntrt AS MessagePosition, ici_firstmessageid  AS FirstMessageID
		   from tbl_msglunr
		   where TopicID=i_TopicID and Posted > i_LastRead  
		   order by Posted  ASC LIMIT 1;
		
		else	    
		   select  MessageID, {databaseSchema}.{objectQualifier}biginttoint(1) AS MessagePosition, ici_firstmessageid AS  FirstMessageID
		   from tbl_msglunr
		   where TopicID=i_TopicID
		   order by Posted DESC LIMIT 1; 
		end	if;	
	end IF;
	else
		select m.MessageID,  {databaseSchema}.{objectQualifier}biginttoint(1) AS MessagePosition, ici_firstmessageid AS FirstMessageID
	from
		{databaseSchema}.{objectQualifier}Message m	
	where
		m.TopicID = i_TopicID			
		-- approved 			
		AND (m.Flags & 16)  = 16
	   -- deleted
		AND (i_ShowDeleted = 1 OR m.Flags & 8 <> 8 OR (i_AuthorUserID > 0 AND m.UserID =i_AuthorUserID))  
	order by		
		m.Posted DESC LIMIT 1;
end if;
DROP TABLE IF EXISTS tbl_msglunr ;
		END;
--GO



/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_getReplies(i_MessageID int) 
 BEGIN
	SELECT MessageID FROM {databaseSchema}.{objectQualifier}Message WHERE ReplyTo = i_MessageID;
 END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_list(i_MessageID INT)
 BEGIN
	SELECT
		a.MessageID,
		a.UserID,
		b.Name AS UserName,
		b.DisplayName AS UserDisplayName,
		a.Message,
		c.TopicID,
		c.ForumID,
		c.Topic,
		c.Status,
		c.Styles,
		c.Priority,
		c.Description,
		a.Flags,
		c.UserID AS TopicOwnerID,
		IFNULL(a.Edited,a.Posted) AS Edited,
		c.Flags AS TopicFlags,
		d.Flags AS ForumFlags,
		a.EditReason,
		a.Position,
		a.IsModeratorChanged,
		a.DeleteReason,
		a.BlogPostID,
		c.PollID,
		a.IP,
		a.ReplyTo,
		a.ExternalMessageId,
		a.ReferenceMessageId
	FROM
		{databaseSchema}.{objectQualifier}Message a
		INNER JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID = a.UserID
		INNER JOIN {databaseSchema}.{objectQualifier}Topic c ON c.TopicID = a.TopicID
		INNER JOIN {databaseSchema}.{objectQualifier}Forum d ON c.ForumID = d.ForumID
	WHERE
		a.MessageID = i_MessageID;
 END;
--GO



/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_listreported(i_ForumID INT) 
 BEGIN
	SELECT
		a.*,
		b.Message AS OriginalMessage,
		b.Flags,
		b.IsModeratorChanged,	
		IFNULL(b.UserName,d.Name) AS UserName,
		IFNULL(b.UserDisplayName,d.DisplayName) AS UserDisplayName,
		b.UserID AS UserID,
		b.Posted AS Posted,
		b.TopicID AS TopicID,
		c.Topic AS Topic,
		(SELECT count(LogID) FROM {databaseSchema}.{objectQualifier}MessageReportedAudit WHERE {databaseSchema}.{objectQualifier}MessageReportedAudit.MessageID = a.MessageID) AS NumberOfReports
	FROM
		{databaseSchema}.{objectQualifier}MessageReported a
	INNER JOIN
		{databaseSchema}.{objectQualifier}Message b ON a.MessageID = b.MessageID
	INNER JOIN
		{databaseSchema}.{objectQualifier}Topic c ON b.TopicID = c.TopicID
	INNER JOIN
		{databaseSchema}.{objectQualifier}User d ON b.UserID = d.UserID
	WHERE
		c.ForumID = i_ForumID AND
		(c.Flags & 16)=0 AND
		(b.Flags & 8)=0 AND
		(c.Flags & 8)=0 AND
		(b.Flags & 128)=128
	ORDER BY
		b.TopicID DESC, b.Posted DESC;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_move (i_MessageID INT, i_MoveToTopic INT)
 BEGIN

 DECLARE ici_Position INT;
 DECLARE ici_ReplyToID INT;
 DECLARE ici_ForumID2 INT;
 DECLARE ici_OldTopicID INT;
 DECLARE ici_OldForumID INT;
 DECLARE ici_NewForumID	INT;
 DECLARE ici_LastMessageID INT;
 DECLARE ici_Posted DATETIME;
 DECLARE ici_UserID INT;
 DECLARE ici_UserName VARCHAR(255);
 DECLARE ici_UserDisplayName VARCHAR(255);
 DECLARE ici_EraseOldTopic TINYINT(1) DEFAULT 0;
 
 SELECT Posted INTO ici_Posted 
FROM {databaseSchema}.{objectQualifier}Message  
WHERE MessageID= i_MessageID;
 SET 	ici_NewForumID = (SELECT     ForumID
				FROM         {databaseSchema}.{objectQualifier}Topic
				WHERE     (TopicID = i_MoveToTopic));
 
 
 SET 	ici_OldTopicID = 	(SELECT     TopicID
				FROM         {databaseSchema}.{objectQualifier}Message
				WHERE     (MessageID = i_MessageID));
 
 SET 	ici_OldForumID = (SELECT     ForumID
				FROM         {databaseSchema}.{objectQualifier}Topic
				WHERE     (TopicId = ici_OldTopicID));
 
 SET	ici_ReplyToID = (SELECT     MessageID
			FROM         {databaseSchema}.{objectQualifier}Message
			WHERE     (`Position` = 0) AND (TopicID = i_MoveToTopic));
 
 SET	ici_Position = 	(SELECT     MAX(`Position`) + 1 AS Expr1
			FROM         {databaseSchema}.{objectQualifier}Message
			WHERE     (TopicID = i_MoveToTopic) and UNIX_TIMESTAMP(Posted) < UNIX_TIMESTAMP(ici_Posted));
 
IF ici_Position IS NULL THEN set ici_Position = 0; END IF;
 
 update {databaseSchema}.{objectQualifier}Message SET
		`Position` = `Position`+1
	 WHERE     (TopicID = i_MoveToTopic) and Posted > ici_Posted;
 
 update {databaseSchema}.{objectQualifier}Message set
		`Position` = `Position`-1
	 WHERE (TopicID = ici_OldTopicID) and Posted > ici_Posted;
 
	-- Update LastMessageID in Topic and Forum
	UPDATE {databaseSchema}.{objectQualifier}Topic set
		LastPosted = NULL,
		LastMessageID = NULL,
		LastUserID = NULL,
		LastUserName = NULL,
		LastUserDisplayName = NULL
	WHERE LastMessageID = i_MessageID;
 
	UPDATE {databaseSchema}.{objectQualifier}Forum set
		LastPosted = NULL,
		LastTopicID = NULL,
		LastMessageID = NULL,
		LastUserID = NULL,
		LastUserName = NULL,
		LastUserDisplayName = NULL
	WHERE LastMessageID = i_MessageID;

 
 UPDATE {databaseSchema}.{objectQualifier}Message SET
	TopicID = i_MoveToTopic,
	ReplyTo = ici_ReplyToID,
	`Position` = ici_Position
 WHERE  MessageID = i_MessageID;
 
	-- Delete topic if there are no more messages 
	IF NOT EXISTS(SELECT 1 
	FROM {databaseSchema}.{objectQualifier}Message 
	WHERE TopicID = ici_OldTopicID and (Flags & 8)=0) THEN 
			
	 SELECT  UserID, UserName, UserDisplayName INTO ici_UserID,ici_UserName,ici_UserDisplayName
	FROM {databaseSchema}.{objectQualifier}Message WHERE MessageID=i_MessageID;

	UPDATE {databaseSchema}.{objectQualifier}Topic set
		UserID = ici_UserID,
		UserName = ici_UserName,
		UserDisplayName = ici_UserDisplayName
	WHERE TopicID = i_MoveToTopic;
		
	IF (SELECT COUNT(MessageID) 
	FROM {databaseSchema}.{objectQualifier}Message 
	WHERE TopicID = ici_OldTopicID and (Flags & 8)=8) = 0 THEN 
	SET ici_EraseOldTopic = 1;
	END IF;

	CALL {databaseSchema}.{objectQualifier}topic_delete(ici_OldTopicID,null,0, ici_EraseOldTopic); 
	END IF;
 
	-- update lastpost
	CALL {databaseSchema}.{objectQualifier}topic_updatelastpost(ici_OldForumID,ici_OldTopicID);
	CALL {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_OldForumID);
	CALL {databaseSchema}.{objectQualifier}topic_updatelastpost(ici_NewForumID,i_MoveToTopic);
	CALL {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_NewForumID);
	-- update topic numposts
	UPDATE {databaseSchema}.{objectQualifier}Topic SET
		NumPosts = (SELECT COUNT(1) from {databaseSchema}.{objectQualifier}Message x 
		WHERE x.TopicID={databaseSchema}.{objectQualifier}Topic.TopicID 
		AND (x.Flags & 16) > 0 AND (x.Flags & 8)= 0)
	WHERE TopicID = ici_OldTopicID;
	UPDATE {databaseSchema}.{objectQualifier}Topic set
		NumPosts = (SELECT COUNT(1) from {databaseSchema}.{objectQualifier}Message x 
		WHERE x.TopicID={databaseSchema}.{objectQualifier}Topic.TopicID 
		AND (x.Flags & 16) > 0 AND (x.Flags & 8)= 0)
	WHERE TopicID = i_MoveToTopic;
 
	CALL {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_NewForumID);
	CALL {databaseSchema}.{objectQualifier}forum_updatestats(ici_NewForumID);
	CALL {databaseSchema}.{objectQualifier}forum_updatestats(ici_OldForumID);
 
 END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_reply_list(i_MessageID INT) 
BEGIN
 SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	SELECT
				 a.MessageID,
		a.Posted,
		c.Topic AS Subject,
		a.Message,
		a.UserID,
		a.Flags,
		IFNULL(a.UserName,b.Name) AS UserName,
		b.Signature
	FROM
		{databaseSchema}.{objectQualifier}Message a
		INNER JOIN {databaseSchema}.{objectQualifier}User b on b.UserID = a.UserID
		INNER JOIN {databaseSchema}.{objectQualifier}Topic c on c.TopicID = a.TopicID
	WHERE
		(a.Flags & 16)=16 AND
		a.ReplyTo = i_MessageID;
 
 SET TRANSACTION ISOLATION LEVEL READ COMMITTED; 
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_listreporters(i_MessageID int, i_UserID int)
BEGIN
	IF i_UserID > 0 THEN
	SELECT DISTINCTROW b.UserID, a.Name AS UserName, a.DisplayName AS UserDisplayName, b.ReportedNumber, b.ReportText  
	FROM {databaseSchema}.{objectQualifier}User a,
	{databaseSchema}.{objectQualifier}MessageReportedAudit b
	WHERE a.UserID = b.UserID AND b.MessageID = i_MessageID  AND a.UserID =i_UserID ORDER BY b.Reported DESC;
	ELSE
	SELECT DISTINCTROW b.UserID, a.Name AS UserName, a.DisplayName AS UserDisplayName, b.ReportedNumber, b.ReportText  
	FROM {databaseSchema}.{objectQualifier}User a,
	{databaseSchema}.{objectQualifier}MessageReportedAudit b
	WHERE a.UserID = b.UserID AND b.MessageID = i_MessageID ORDER BY b.Reported DESC;
	END IF;
	
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_report
(i_MessageID INT, 
i_ReporterID INT, 
i_ReportedDate DATETIME, i_ReportText VARCHAR(4000),
	i_UTCTIMESTAMP DATETIME) 
 BEGIN
	IF i_ReportText IS NULL THEN SET i_ReportText = ''; END IF;
	IF NOT EXISTS (SELECT MessageID from {databaseSchema}.{objectQualifier}MessageReportedAudit WHERE MessageID=i_MessageID AND UserID=i_ReporterID) THEN
		INSERT INTO {databaseSchema}.{objectQualifier}MessageReportedAudit(MessageID,UserID,Reported, ReportText) VALUES (i_MessageID,i_ReporterID,i_ReportedDate,CONCAT(CAST(i_UTCTIMESTAMP AS CHAR(40)), '??' ,i_ReportText) ); 
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}MessageReportedAudit SET ReportedNumber = ( CASE WHEN ReportedNumber < 2147483647 THEN  ReportedNumber  + 1 ELSE ReportedNumber END ), Reported = i_ReportedDate, ReportText = (CASE WHEN (CHAR_LENGTH(ReportText) + CHAR_LENGTH(i_ReportText ) + 255 < 4000)  THEN  CONCAT(ReportText , '|' , CAST(i_UTCTIMESTAMP AS CHAR(40)), '??' ,  i_ReportText)  ELSE ReportText END) WHERE MessageID=i_MessageID AND UserID=i_ReporterID; 
	END IF;
	
	IF NOT EXISTS (SELECT MessageID FROM {databaseSchema}.{objectQualifier}MessageReported WHERE MessageID=i_MessageID) THEN
		INSERT INTO {databaseSchema}.{objectQualifier}MessageReported(MessageID, `Message`)
		SELECT 
			a.MessageID,
			a.Message
		FROM
			{databaseSchema}.{objectQualifier}Message a
		WHERE
			a.MessageID = i_MessageID;
	END IF;
	/*update Message table to set message with flag Reported*/
	UPDATE {databaseSchema}.{objectQualifier}Message 
SET Flags = Flags | 128 WHERE MessageID = i_MessageID;

 END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_reportcopyover(i_MessageID INT)
 BEGIN
	UPDATE {databaseSchema}.{objectQualifier}MessageReported,{databaseSchema}.{objectQualifier}Message
	SET {databaseSchema}.{objectQualifier}MessageReported.`Message` = (SELECT m.`Message` FROM {databaseSchema}.{objectQualifier}MessageReported mr
	JOIN {databaseSchema}.{objectQualifier}Message m
   ON m.MessageID = mr.MessageID
	WHERE mr.MessageID = i_MessageID);
	
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_reportresolve(i_MessageFlag INT, i_MessageID INT, i_UserID INT, i_UTCTIMESTAMP DATETIME) 
 BEGIN

	UPDATE {databaseSchema}.{objectQualifier}MessageReported
	SET Resolved = 1, ResolvedBy = i_UserID, ResolvedDate = i_UTCTIMESTAMP
	WHERE MessageID = i_MessageID; 	
	/* Remove Flag */
	UPDATE {databaseSchema}.{objectQualifier}Message
	SET Flags = Flags & (~POWER(2, i_MessageFlag))
	WHERE MessageID = i_MessageID;
 END;
--GO


	 
/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_save(
	i_TopicID		INT,
	i_UserID		INT,
	i_Message		TEXT,
	i_UserName		VARCHAR(128),
	i_IP			VARCHAR(39),
	i_Posted		DATETIME,
	i_ReplyTo		INT,
	i_BlogPostID	VARCHAR(128),
	i_ExternalMessageId VARCHAR(255),
	i_ReferenceMessageId VARCHAR(255),	
	i_MessageDescription VARCHAR(255),
	i_Flags			INT,
	i_UTCTIMESTAMP DATETIME,
	OUT i_MessageID	INT
 )
 BEGIN
	DECLARE ici_ForumID INT;
		DECLARE ici_ForumFlags INT;
		DECLARE ici_Position INT;
		DECLARE ici_Indent INT;
		DECLARE ici_temp INT;
		DECLARE ici_OverrideDisplayName TINYINT(1);
	   
	IF i_Posted IS NULL THEN
		SET i_Posted = i_UTCTIMESTAMP; END IF;
 
	SELECT  x.ForumID,  y.Flags
		INTO ici_ForumID,ici_ForumFlags
	FROM 
		{databaseSchema}.{objectQualifier}Topic x
	INNER JOIN 
		{databaseSchema}.{objectQualifier}Forum y ON y.ForumID=x.ForumID
	WHERE x.TopicID = i_TopicID;
 
	IF i_ReplyTo IS NULL THEN
			SELECT 0,0 INTO ici_Position, ici_Indent; /* New thread*/
 
	ELSEIF i_ReplyTo<0 THEN
		/* Find post to reply to AND indent of this post */
		SELECT   MessageID, Indent+1
				INTO i_ReplyTo,ici_Indent
		FROM {databaseSchema}.{objectQualifier}Message
		WHERE TopicID = i_TopicID AND ReplyTo IS NULL
		ORDER BY Posted LIMIT 1;
 
	ELSE
		/* Got reply, find indent of this post */
			SELECT Indent+1 INTO ici_Indent
			FROM {databaseSchema}.{objectQualifier}Message
			WHERE MessageID=i_ReplyTo;
		END IF;  
 
	/* Find position */
	IF i_ReplyTo IS NOT NULL THEN
		
		 SELECT ReplyTo,`Position` INTO ici_temp,ici_Position 
		 FROM {databaseSchema}.{objectQualifier}Message WHERE MessageID=i_ReplyTo;
 
		 IF ici_temp IS NULL THEN
			/* We are replying to first post */
			 SELECT MAX(`Position`)+1 INTO ici_Position  FROM {databaseSchema}.{objectQualifier}Message WHERE TopicID=i_TopicID;
 
		   ELSE
			/* Last position of replies to parent post*/
			 SELECT MIN(`Position`) INTO ici_Position 
			 FROM {databaseSchema}.{objectQualifier}Message 
			 WHERE ReplyTo=ici_temp AND `Position`>ici_Position;
 
		 /* No replies, THEN USE parent post's position+1*/
			 IF ici_Position IS NULL THEN
				 SELECT `Position`+1 
				 INTO ici_Position 
				 FROM {databaseSchema}.{objectQualifier}Message 
				 WHERE MessageID=i_ReplyTo;
		/*Increase position of posts after this*/

		 UPDATE {databaseSchema}.{objectQualifier}Message 
		 SET `Position`=`Position`+1 
		 WHERE TopicID=i_TopicID 
		 AND `Position`>=ici_Position; 
			END IF;
		 END IF;
  END IF;
	SELECT SIGN(COUNT(Name))  INTO ici_OverrideDisplayName FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID AND Name != i_UserName;	
   -- Add points to Users total points 
	UPDATE {databaseSchema}.{objectQualifier}User SET Points = Points + 3  WHERE UserID = i_UserID;       
	INSERT {databaseSchema}.{objectQualifier}Message ( UserID, Message, Description, TopicID, Posted, Edited, UserName,UserDisplayName, IP, ReplyTo, `Position`, Indent, Flags, BlogPostID, ExternalMessageId, ReferenceMessageId)
	VALUES ( i_UserID, CONVERT(i_Message USING {databaseEncoding}),i_MessageDescription, i_TopicID, i_Posted, i_Posted,(CASE WHEN ici_OverrideDisplayName = 1 THEN i_UserName ELSE (SELECT Name FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID) END),(CASE WHEN ici_OverrideDisplayName = 1 THEN i_UserName ELSE (SELECT DisplayName FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID) END) ,i_IP, i_ReplyTo, ici_Position, ici_Indent, i_Flags & ~16, i_BlogPostID, i_ExternalMessageId, i_ReferenceMessageId);

	SET i_MessageID = LAST_INSERT_ID();

	IF ((i_Flags & 16) = 16) THEN
	  CALL {databaseSchema}.{objectQualifier}message_approve (i_MessageID); 
	END IF;
	  END;
--GO 


	  /* STORED PROCEDURE CREATED BY VZ-TEAM */     
	  CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_unapproved(i_ForumID INT)
	  BEGIN
	  SELECT
	  b.MessageID AS MessageID,
	  b.UserID AS UserID,
	  IFNULL(b.UserName,c.Name) AS UserName,
	  IFNULL(b.UserDisplayName,c.DisplayName) AS UserDisplayName,
	  b.Posted AS Posted,
	  a.Topic AS Topic,
	  a.NumPosts as MessageCount,
	  a.TopicID,
	  b.Message AS `Message`,
	  b.Flags AS Flags,
	  b.IsModeratorChanged AS IsModeratorChanged
	  FROM
	  {databaseSchema}.{objectQualifier}Topic a
	  INNER JOIN {databaseSchema}.{objectQualifier}Message b on b.TopicID = a.TopicID
	  INNER JOIN {databaseSchema}.{objectQualifier}User c on c.UserID = b.UserID
	  WHERE
	  a.ForumID = i_ForumID AND
	  (b.Flags & 16)=0 AND
		(a.Flags & 8)=0 AND
		(b.Flags & 8)=0
	ORDER BY
		a.Posted;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_tagsave 
	(i_TopicID int, i_Tags VARCHAR(4001))
BEGIN
	DECLARE ici_MessageID VARCHAR(11);
	DECLARE ici_MessageIDsChunk VARCHAR(4000);
	DECLARE ici_Pos int;
	DECLARE ici_Itr int;
	DECLARE ici_trimindex int;
	DECLARE ici_thistagid int;

	UPDATE {databaseSchema}.{objectQualifier}Tags SET TagCount = TagCount - 1 WHERE TagID IN (SELECT TagID FROM  {databaseSchema}.{objectQualifier}TopicTags where TopicID = i_TopicID);
	DELETE FROM {databaseSchema}.{objectQualifier}TopicTags where TopicID = i_TopicID;    
		
	SET i_Tags = (CONCAT(TRIM(i_Tags), ','));
	SET ici_Pos = (LOCATE(',', i_Tags, 1));
	IF REPLACE(i_Tags, ',', '') <> '' THEN	
		WHILE ici_Pos > 0 DO		
			SET ici_MessageID = LTRIM(RTRIM(LEFT(i_Tags, ici_Pos - 1)));
			IF ici_MessageID <> '' THEN	
				IF (NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Tags WHERE LOWER(Tag) = LOWER(ici_MessageID) LIMIT 1)) THEN
				INSERT INTO {databaseSchema}.{objectQualifier}Tags(Tag) VALUES (ici_MessageID);
				SET ici_thistagid = LAST_INSERT_ID();
				ELSE
				SELECT TagID INTO ici_thistagid FROM {databaseSchema}.{objectQualifier}Tags WHERE LOWER(Tag) = LOWER(ici_MessageID) LIMIT 1;
				END IF;
				IF (NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}TopicTags WHERE TagID = ici_thistagid AND TopicID = i_TopicID LIMIT 1)) THEN
				INSERT INTO {databaseSchema}.{objectQualifier}TopicTags(TagID,TopicID) VALUES (ici_thistagid,i_TopicID);
				UPDATE {databaseSchema}.{objectQualifier}Tags SET TagCount = TagCount + 1 WHERE TagID = ici_thistagid;
				ELSE
				UPDATE {databaseSchema}.{objectQualifier}TopicTags SET TagID = ici_thistagid WHERE TopicID = i_TopicID and TagID = ici_thistagid;
				END IF;
				-- Use Appropriate conversion
			END IF;
			SET i_Tags = RIGHT(i_Tags, CHAR_LENGTH(i_Tags) - ici_Pos);
			SET ici_Pos = LOCATE(',', i_Tags, 1);
		END WHILE;
		  -- to be sure that last value is inserted shouldbe deleted?					
		  /* IF (CHAR_LENGTH(ici_MessageID) > 0) THEN
					 INSERT INTO {objectQualifier}tmp_ParsedMessageIDs (MessageID) 
					 VALUES (CAST(ici_MessageID AS SIGNED));  
					END IF; */
	END	IF;		
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_update
(i_MessageID INT,
i_Priority INT,
i_Subject VARCHAR(255),
i_status VARCHAR(255),
i_Styles VARCHAR(255),
i_Description VARCHAR(255),
i_Flags INT, 
i_Message TEXT, 
i_Reason VARCHAR(128),
i_EditedBy INT, 
i_IsModeratorChanged TINYINT(1), 
i_OverrideApproval TINYINT(1),
i_OriginalMessage TEXT,
i_MessageDescription VARCHAR(255),
i_Tags 	VARCHAR(1024),
i_UtcTimeStamp DATETIME) 
BEGIN
	DECLARE ici_TopicID	INT;
	DECLARE	ici_ForumFlags	INT;
 
	SET i_Flags = i_Flags & ~16;	
	
	SELECT 
	a.TopicID,
	c.Flags
		INTO ici_TopicID,ici_ForumFlags
	FROM 
		{databaseSchema}.{objectQualifier}Message a
		INNER JOIN {databaseSchema}.{objectQualifier}Topic b ON b.TopicID = a.TopicID
		INNER JOIN {databaseSchema}.{objectQualifier}Forum c ON c.ForumID = b.ForumID
	WHERE 
		a.MessageID = i_MessageID;
 
	IF (i_OverrideApproval = 1 OR (ici_ForumFlags & 8)=0) THEN SET i_Flags = i_Flags | 16; END IF;
 -- insert current message variant - use OriginalMessage in future 	
	insert into {databaseSchema}.{objectQualifier}MessageHistory
	(MessageID,		
		Message,
		IP,
		Edited,
		EditedBy,		
		EditReason,
		IsModeratorChanged,
		Flags)
	select 
	MessageID, i_OriginalMessage, IP , 
	IFNULL(Edited,Posted), IFNULL(EditedBy,UserID), 
	EditReason, IsModeratorChanged, Flags
	from {databaseSchema}.{objectQualifier}Message where
		MessageID = i_MessageID;
	
	UPDATE {databaseSchema}.{objectQualifier}Message SET
		Message = CONVERT(i_Message USING {databaseEncoding}),
		Description = i_MessageDescription,
		Edited = i_UtcTimeStamp,
		EditedBy = i_EditedBy,
		Flags = i_Flags,
		IsModeratorChanged  = i_IsModeratorChanged,
		EditReason = CONVERT(i_Reason USING {databaseEncoding})
	WHERE
		MessageID = i_MessageID;
 
	IF i_Priority IS NOT NULL THEN
		UPDATE {databaseSchema}.{objectQualifier}Topic SET
			Priority = i_Priority
		WHERE
			TopicID = ici_TopicID;
	END IF;
 
	IF NOT i_Subject = '' AND i_Subject IS NOT NULL THEN
		UPDATE {databaseSchema}.{objectQualifier}Topic SET
			Topic = CONVERT(i_Subject USING {databaseEncoding}), 
			Description =  CONVERT(i_Description USING {databaseEncoding}),
			Status = i_status,
			Styles = i_Styles
		WHERE
			TopicID = ici_TopicID;
	END IF; 	
	CALL {databaseSchema}.{objectQualifier}topic_tagsave(ici_TopicID, i_Tags);
	/*If forum is moderated, make sure last post pointers are correct*/
	IF (ici_ForumFlags & 8)<>0 THEN 
	CALL {databaseSchema}.{objectQualifier}topic_updatelastpost(null,null);
	/*CALL {databaseSchema}.{objectQualifier}forum_updatelastpost (ici_ForumID);*/
	END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntpforum_delete(i_NntpForumID INT) 
BEGIN
DELETE FROM {databaseSchema}.{objectQualifier}NntpTopic where NntpForumID = i_NntpForumID;
DELETE FROM {databaseSchema}.{objectQualifier}NntpForum where NntpForumID = i_NntpForumID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntpforum_list(i_BoardID INT,i_Minutes INT,i_NntpForumID INT,i_Active TINYINT(1),i_UTCTIMESTAMP DATETIME) 
BEGIN
	SELECT
		a.Name,
		a.Address,
		IFNULL(a.Port,119) AS Port,
		a.UserName,
		a.UserPass,		
		a.NntpServerID,
		b.NntpForumID,		
		b.GroupName,
		b.ForumID,
		b.LastMessageNo,
		b.LastUpdate,
		b.Active,	
		b.DateCutOff,
		c.Name AS ForumName,
		c.CategoryID 
	FROM
		{databaseSchema}.{objectQualifier}NntpServer a
		JOIN {databaseSchema}.{objectQualifier}NntpForum b 
				ON b.NntpServerID = a.NntpServerID
		JOIN {databaseSchema}.{objectQualifier}Forum c 
				ON c.ForumID = b.ForumID
	WHERE
		(i_Minutes IS NULL 
				 OR date_add(b.LastUpdate, interval i_Minutes minute) < i_UTCTIMESTAMP) AND
		(i_NntpForumID IS NULL OR b.NntpForumID=i_NntpForumID) AND
		a.BoardID=i_BoardID AND
		(i_Active IS NULL OR b.Active=i_Active)
	ORDER BY
		a.Name,
		b.GroupName;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntpforum_save(
				 i_NntpForumID INT,
				 i_NntpServerID INT,
				 i_GroupName VARCHAR(128),
				 i_ForumID INT,
				 i_Active TINYINT(1), 
				 i_DateCutoff DATETIME,
				 i_UTCTIMESTAMP DATETIME) 
BEGIN
	IF i_NntpForumID IS NULL THEN
		INSERT INTO {databaseSchema}.{objectQualifier}NntpForum(NntpServerID,GroupName,ForumID,LastMessageNo,LastUpdate,Active, DateCutoff)
		VALUES(i_NntpServerID,i_GroupName,i_ForumID,0,i_UTCTIMESTAMP,i_Active, i_DateCutoff);
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}NntpForum SET
			NntpServerID = i_NntpServerID,
			GroupName = i_GroupName,
			ForumID = i_ForumID,
			Active = i_Active,
			DateCutoff = i_DateCutoff
		WHERE NntpForumID = i_NntpForumID;
	   END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntpforum_update(i_NntpForumID INT,i_LastMessageNo INT,i_UserID INT,
				 i_UTCTIMESTAMP DATETIME) 
BEGIN
	DECLARE	ici_ForumID	INT;
	
	SELECT ForumID INTO ici_ForumID from {databaseSchema}.{objectQualifier}NntpForum where NntpForumID=i_NntpForumID;
 
	UPDATE {databaseSchema}.{objectQualifier}NntpForum SET
		LastMessageNo = i_LastMessageNo,
		LastUpdate = i_UTCTIMESTAMP
	WHERE NntpForumID = i_NntpForumID;
 
	UPDATE {databaseSchema}.{objectQualifier}Topic SET 
		NumPosts = (SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x WHERE x.TopicID={databaseSchema}.{objectQualifier}Topic.TopicID AND (x.Flags & 16) > 0 AND (x.Flags & 8)= 0)
	WHERE ForumID=ici_ForumID;
 
	/* CALL {databaseSchema}.{objectQualifier}user_upgrade(i_UserID) */
	CALL {databaseSchema}.{objectQualifier}forum_updatestats(ici_ForumID);
	/* CALL {databaseSchema}.{objectQualifier}topic_updatelastpost ici_ForumID,null*/
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntpserver_delete(i_NntpServerID INT) 
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}NntpTopic WHERE NntpForumID IN (SELECT NntpForumID FROM {databaseSchema}.{objectQualifier}NntpForum WHERE NntpServerID = i_NntpServerID);
	DELETE FROM {databaseSchema}.{objectQualifier}NntpForum WHERE NntpServerID = i_NntpServerID;
	DELETE FROM {databaseSchema}.{objectQualifier}NntpServer WHERE NntpServerID = i_NntpServerID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntpserver_list(i_BoardID INT,i_NntpServerID INT)
BEGIN
	IF i_NntpServerID IS NULL THEN
		SELECT * FROM {databaseSchema}.{objectQualifier}NntpServer WHERE BoardID=i_BoardID ORDER BY `Name`;
	ELSE
		SELECT * FROM {databaseSchema}.{objectQualifier}NntpServer WHERE NntpServerID=i_NntpServerID;
		END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntpserver_save(
	i_NntpServerID 	INT,
	i_BoardID	INT,
	i_Name		VARCHAR(128),
	i_Address	VARCHAR(128),
	i_Port		INT,
	i_UserName	VARCHAR(128),
	i_UserPass	VARCHAR(128)
 )  BEGIN
	IF i_NntpServerID IS NULL THEN
		INSERT INTO {databaseSchema}.{objectQualifier}NntpServer(`Name`,BoardID,Address,Port,UserName,UserPass)
		VALUES(i_Name,i_BoardID,i_Address,i_Port,i_UserName,i_UserPass);
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}NntpServer SET
			`Name` = i_Name,
			Address = i_Address,
			Port = i_Port,
			UserName = i_UserName,
			UserPass = i_UserPass
		WHERE NntpServerID = i_NntpServerID;
		END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntptopic_list(i_Thread VARCHAR(128))
BEGIN
	SELECT
		a.*
	FROM
		{databaseSchema}.{objectQualifier}NntpTopic a
	WHERE
		a.Thread = i_Thread;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}nntptopic_addmessage(
	i_NntpForumID	int,
	i_Topic 		VARCHAR(128),
	i_Body 			TEXT,
	i_UserID 		INT,
	i_UserName		VARCHAR(128),
	i_IP			VARCHAR(39),
	i_Posted			DATETIME, 
	i_ExternalMessageId	VARCHAR(255),
	i_ReferenceMessageId VARCHAR(255),
	i_UTCTIMESTAMP DATETIME
 )  
 BEGIN   
	DECLARE	ici_ForumID	INT;
	DECLARE	ici_ParentID	INT;
	DECLARE ici_TopicID	INT;
	DECLARE	ici_MessageID	INT;

DECLARE ici_LastTopicID_Check INT;
DECLARE ici_LastMessageID_Check INT;
DECLARE FlagDeleted TINYINT(1) DEFAULT 0;
DECLARE FlagApproved TINYINT(1) DEFAULT 1;

declare ici_ReplyTo	INT DEFAULT NULL;

	SELECT ForumID INTO ici_ForumID 
	FROM {databaseSchema}.{objectQualifier}NntpForum 
	WHERE NntpForumID=i_NntpForumID;  
	
	select TopicID,  MessageID into ici_TopicID, ici_ReplyTo
		 from {databaseSchema}.{objectQualifier}Message where ExternalMessageId = i_ReferenceMessageId;

 if ici_TopicID IS NULL AND i_ReferenceMessageId IS NULL and not exists(select 1 from {databaseSchema}.{objectQualifier}Message where ExternalMessageId = i_ExternalMessageId limit 1) 
	 then        
		INSERT INTO {databaseSchema}.{objectQualifier}Topic(ForumID,UserID,UserName,UserDisplayName,Posted,Topic,Views,Priority,NumPosts,LastMessageFlags)
		VALUES(ici_ForumID,i_UserID,i_UserName,i_UserName,i_Posted,i_Topic,0,0,0,22);
		SELECT LAST_INSERT_ID() INTO ici_TopicID; 

		INSERT INTO {databaseSchema}.{objectQualifier}NntpTopic(NntpForumID,Thread,TopicID)
		VALUES (i_NntpForumID,'',ici_TopicID);     
	END IF;	
	IF ici_TopicID IS NOT NULL
	then
	 CALL {databaseSchema}.{objectQualifier}message_save (ici_TopicID,i_UserID,i_Body,i_UserName,i_IP,i_Posted,ici_ReplyTo,null, i_ExternalMessageId, i_ReferenceMessageId, null,17,i_UTCTIMESTAMP,ici_MessageID);

	 /* update user */
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Forum 
	WHERE ForumID=ici_ForumID AND (Flags & 4)=0) THEN    
		UPDATE {databaseSchema}.{objectQualifier}User SET NumPosts=NumPosts+1 WHERE UserID=i_UserID;		
	END IF;    

	 UPDATE {databaseSchema}.{objectQualifier}Topic SET 
		LastPosted		= i_Posted,
		LastMessageID	= ici_MessageID,
		LastUserID		= i_UserID,
		LastUserName	= i_UserName,
		NumPosts =NumPosts + 1
	WHERE TopicID=ici_TopicID;	
	UPDATE {databaseSchema}.{objectQualifier}Forum SET
		LastPosted		= i_Posted,
		LastTopicID	= ici_TopicID,
		LastMessageID	= ici_MessageID,
		LastUserID		= i_UserID,
		LastUserName	= i_UserName
	WHERE ForumID=ici_ForumID AND (LastPosted IS NULL OR (UNIX_TIMESTAMP(LastPosted) < UNIX_TIMESTAMP(i_Posted))); 
	END if;	

 
	

  
  



END;
--GO
CREATE procedure {databaseSchema}.{objectQualifier}activeaccess_reset() 
begin
delete from {databaseSchema}.{objectQualifier}Active;
delete from {databaseSchema}.{objectQualifier}ActiveAccess;
END;
--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}pageload(   
	i_SessionID 	 VARCHAR(24),
	i_BoardID	     INT,	
	i_UserKey	     VARCHAR(64),
	i_IP		     VARCHAR(37),
	i_Location	     VARCHAR(255),
	i_ForumPage	     VARCHAR(255),
	i_Browser	     VARCHAR(128),
	i_Platform	     VARCHAR(128),
	i_CategoryID	 INT,
	i_ForumID	     INT,
	i_TopicID	     INT,
	i_MessageID  	 INT,
	i_IsCrawler      TINYINT(1),
	i_IsMobileDevice TINYINT(1),
	i_DontTrack      TINYINT(1),
	i_UTCTIMESTAMP    DATETIME
)
BEGIN
	DECLARE ici_UserID		INT;
	DECLARE ici_UserBoardID	INT;
	DECLARE ici_IsGuest	TINYINT(1) DEFAULT 0;
	DECLARE ici_rowcount	INT;
	DECLARE ici_PreviousVisit	DATETIME;
	DECLARE ici_ForumID INT;
	DECLARE ici_ActiveUpdate TINYINT(1) DEFAULT 0;
	declare ici_ActiveFlags	INT DEFAULT 1;
	declare ici_GuestID     INT;  
	

	/* set implicit_transactions off */
 
 START TRANSACTION WITH CONSISTENT SNAPSHOT;
	-- find a guest id should do it every time to be sure that guest access rights are in ActiveAccess table
		SELECT SQL_CALC_FOUND_ROWS UserID INTO ici_GuestID from {databaseSchema}.{objectQualifier}User where BoardID=i_BoardID and (Flags & 4)<>0 ORDER BY Joined DESC LIMIT 1;
		SET ici_rowcount=FOUND_ROWS();
		IF ici_rowcount>1 THEN
				/*raiserror('Found %d possible guest users. There should be one and only one user marked as guest.',16,1,ici_rowcount)*/		 
					SET ici_rowcount = ici_rowcount;			
		END IF;

-- verify that there's not the same session for other board and drop it if required.TestĚcode for portals with many boards  
 

	IF i_UserKey IS NULL THEN
		SET ici_UserID = ici_GuestID;
		set ici_IsGuest = 1;
		-- set IsGuest ActiveFlag  1 | 2
		set ici_ActiveFlags = 3;
		set ici_UserBoardID = i_BoardID;
		-- crawlers are always guests 
		if	i_IsCrawler = 1 then			
			-- set IsCrawler ActiveFlag
			set ici_ActiveFlags =  ici_ActiveFlags | 8;
		end  if;		
	ELSE
	
		SELECT UserID, BoardID INTO ici_UserID,ici_UserBoardID  
		FROM {databaseSchema}.{objectQualifier}User
		where BoardID=i_BoardID AND ProviderUserKey=i_UserKey;
		
		SET ici_IsGuest = 0;
		-- make sure that registered users are not crawlers
		SET i_IsCrawler = 0;
		-- set IsRegistered ActiveFlag
		SET ici_ActiveFlags = ici_ActiveFlags | 4;

	END IF;
	-- START TRANSACTION WITH CONSISTENT SNAPSHOT;
	-- Check valid ForumID 
	IF i_ForumID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Forum WHERE ForumID=i_ForumID)
		  THEN 
		SET i_ForumID = NULL; 
			END IF;
	
	-- Check valid CategoryID
	IF i_CategoryID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Category WHERE CategoryID=i_CategoryID)              THEN 
		SET i_CategoryID = NULL;
		END IF;
	-- Check valid MessageID
	IF i_MessageID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Message WHERE MessageID=i_MessageID) 
		   THEN
		SET i_MessageID = NULL;
		END IF;
	-- Check valid TopicID
	IF i_TopicID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicID=i_TopicID) 
		   THEN
		SET i_TopicID = NULL;
		END IF;	
	
   
   -- START TRANSACTION WITH CONSISTENT SNAPSHOT;
   -- find missing ForumID/TopicID
	IF i_MessageID IS NOT NULL THEN
		SELECT
			c.CategoryID,
			b.ForumID,
			b.TopicID
				INTO i_CategoryID,i_ForumID,i_TopicID
		FROM
			{databaseSchema}.{objectQualifier}Message a
			INNER JOIN {databaseSchema}.{objectQualifier}Topic b ON b.TopicID = a.TopicID
			INNER JOIN {databaseSchema}.{objectQualifier}Forum c ON c.ForumID = b.ForumID
			INNER JOIN {databaseSchema}.{objectQualifier}Category d ON d.CategoryID = c.CategoryID
		WHERE
			a.MessageID = i_MessageID AND
			d.BoardID = i_BoardID;
	ELSEIF i_TopicID IS NOT NULL THEN
		SELECT 
			b.CategoryID,
			a.ForumID 
				INTO i_CategoryID,i_ForumID
		FROM 
			{databaseSchema}.{objectQualifier}Topic a
			inner join {databaseSchema}.{objectQualifier}Forum b on b.ForumID = a.ForumID
			inner join {databaseSchema}.{objectQualifier}Category c on c.CategoryID = b.CategoryID
		WHERE 
			a.TopicID = i_TopicID AND
			c.BoardID = i_BoardID;
	
	ELSEIF i_ForumID IS NOT NULL THEN
		SELECT
			 a.CategoryID
		INTO     i_CategoryID
					
		FROM	{databaseSchema}.{objectQualifier}Forum a
			inner join {databaseSchema}.{objectQualifier}Category b on b.CategoryID = a.CategoryID
		WHERE
			a.ForumID = i_ForumID and
			b.BoardID = i_BoardID;
	END IF;   
  

		-- ensure that access right are in place		
		if not exists (select 
			UserID	
			from {databaseSchema}.{objectQualifier}ActiveAccess  
			where UserID = ici_UserID LIMIT 1)
			then							
			insert into {databaseSchema}.{objectQualifier}ActiveAccess(
			UserID,
			BoardID,
			ForumID,
			IsAdmin, 
			IsForumModerator,
			IsModerator,
			IsGuestX,
			LastActive,
			ReadAccess,
			PostAccess,
			ReplyAccess,
			PriorityAccess,
			PollAccess,
			VoteAccess,	
			ModeratorAccess,
			EditAccess,
			DeleteAccess,
			UploadAccess,
			DownloadAccess,
			UserForumAccess)
			select 
			UserID, 
			i_BoardID, 
			ForumID, 
			SIGN(IsAdmin),
			SIGN(IsForumModerator),
			SIGN(IsModerator),
			ici_IsGuest,
			i_UTCTIMESTAMP,
			SIGN(ReadAccess),
			SIGN(PostAccess),
			SIGN(ReplyAccess),
			SIGN(PriorityAccess),
			SIGN(PollAccess),
			SIGN(VoteAccess),
			SIGN(ModeratorAccess),
			IFNULL(SIGN(EditAccess),0),
			SIGN(DeleteAccess),
			SIGN(UploadAccess),
			SIGN(DownloadAccess),
			SIGN(UserForumAccess)			
			from {databaseSchema}.{objectQualifier}vaccess 
			where UserID = ici_UserID;			
	end if;
			-- ensure that access right are in place		
		if ici_userID != ici_GuestID and not exists (select 
			UserID	
			from {databaseSchema}.{objectQualifier}ActiveAccess  
			where UserID = ici_GuestID LIMIT 1)
			then							
			insert into {databaseSchema}.{objectQualifier}ActiveAccess(
			UserID,
			BoardID,
			ForumID,
			IsAdmin, 
			IsForumModerator,
			IsModerator,
			IsGuestX,
			LastActive,
			ReadAccess,
			PostAccess,
			ReplyAccess,
			PriorityAccess,
			PollAccess,
			VoteAccess,	
			ModeratorAccess,
			EditAccess,
			DeleteAccess,
			UploadAccess,
			DownloadAccess,
			UserForumAccess)
			select 
			UserID, 
			i_BoardID, 
			ForumID, 
			SIGN(IsAdmin),
			SIGN(IsForumModerator),
			SIGN(IsModerator),
			ici_IsGuest,
			i_UTCTIMESTAMP,
			SIGN(ReadAccess),
			SIGN(PostAccess),
			SIGN(ReplyAccess),
			SIGN(PriorityAccess),
			SIGN(PollAccess),
			SIGN(VoteAccess),
			SIGN(ModeratorAccess),
			IFNULL(SIGN(EditAccess),0),
			SIGN(DeleteAccess),
			SIGN(UploadAccess),
			SIGN(DownloadAccess),
			SIGN(UserForumAccess)			
			from {databaseSchema}.{objectQualifier}vaccess 
			where UserID = ici_GuestID;			
	end if;

	-- update active only if a user has a read access to a forum
	  if exists (select 
			UserID	
			from {databaseSchema}.{objectQualifier}ActiveAccess  
			where UserID = ici_UserID and  ForumID = IFNULL(i_ForumID,0) and (IFNULL(i_ForumID,0) = 0 or ReadAccess = 1) LIMIT 1)
			then	
	  -- update active
	
	 -- START TRANSACTION WITH CONSISTENT SNAPSHOT;
	/* update last visit */
	UPDATE {databaseSchema}.{objectQualifier}User SET 
		LastVisit = i_UTCTIMESTAMP,
		IP = i_IP
	WHERE UserID = ici_UserID;
	-- COMMIT;
	IF i_DontTrack != 1 AND ici_UserID IS NOT NULL AND ici_UserBoardID=i_BoardID THEN
		IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Active 
		WHERE (SessionID=i_SessionID OR ( Browser = i_Browser AND (Flags & 8) = 8 )) 
		AND BoardID=i_BoardID) THEN
		if (char_length(i_location) > 0) then
		if i_IsCrawler <> 1 THEN
		 UPDATE {databaseSchema}.{objectQualifier}Active SET
				UserID = ici_UserID,
				IP = CAST(i_IP AS CHAR(39)),
				LastActive = i_UTCTIMESTAMP,
				Location = i_Location,
				ForumPage = i_ForumPage,
				ForumID = i_ForumID,
				TopicID = i_TopicID,
				Browser = i_Browser,
				Platform = i_Platform
			WHERE SessionID = i_SessionID and BoardID=i_BoardID;	
			ELSE
			 UPDATE {databaseSchema}.{objectQualifier}Active SET
				UserID = ici_UserID,
				IP = i_IP,
				LastActive = i_UTCTIMESTAMP,
				Location = i_Location,
				ForumPage = i_ForumPage,
				ForumID = i_ForumID,
				TopicID = i_TopicID,
				Browser = i_Browser,
				Platform = i_Platform
			WHERE Browser = i_Browser AND IP = i_IP and BoardID=i_BoardID;
			-- trace crawler: the cache is reset every time crawler moves to next page ? Disabled as cache reset will overload server 
			-- set @ActiveUpdate = 1;	
			END IF;	
			else
			UPDATE {databaseSchema}.{objectQualifier}Active SET
			   LastActive = i_UTCTIMESTAMP
			WHERE SessionID = i_SessionID and BoardID=i_BoardID;	
			end if;
		ELSE
			-- we set ici_ActiveFlags ready flags 	
			INSERT INTO {databaseSchema}.{objectQualifier}Active(SessionID,BoardID,UserID,IP,Login,LastActive,Location,ForumID,TopicID,Browser,Platform,Flags)
			VALUES(i_SessionID,i_BoardID,ici_UserID,i_IP,i_UTCTIMESTAMP,i_UTCTIMESTAMP,i_Location,i_ForumID,i_TopicID,i_Browser,i_Platform,ici_ActiveFlags);
			/*update max user stats*/
			if ici_IsGuest = 0 then 
			SET ici_ActiveUpdate = 1; 
			end if;
			-- parameter to update active users cache if this is a new user
			 if ici_IsGuest=0 THEN
	
			 CALL {databaseSchema}.{objectQualifier}active_updatemaxstats (i_BoardID, i_UTCTIMESTAMP); 
			END IF; 
		END IF;
		-- remove duplicate users
		IF ici_IsGuest = 0 THEN
			DELETE FROM {databaseSchema}.{objectQualifier}Active WHERE UserID=ici_UserID AND BoardID=i_BoardID AND SessionID<>i_SessionID; END IF;
		END IF;
	 END IF;
	-- return information
	  --  SELECT count(1) INTO ici_Incoming FROM {databaseSchema}.{objectQualifier}UserPMessageSelectView b  where b.UserID=ici_UserID and b.IsRead=0;
	 SELECT	
		x.*,		
		ici_IsGuest AS IsGuest,
		i_IsCrawler AS IsCrawler,
		i_IsMobileDevice AS IsMobileDevice,
		ici_UserID AS UserID,		
		(SELECT  LastVisit  FROM {databaseSchema}.{objectQualifier}User WHERE UserID = ici_UserID) AS PreviousVisit,		      	
		CAST(i_CategoryID AS SIGNED) AS CategoryID,
		(SELECT `Name` FROM {databaseSchema}.{objectQualifier}Category WHERE CategoryID = i_CategoryID) AS CategoryName,
		CAST(i_ForumID AS SIGNED) AS ForumID,
		(select `Name` from {databaseSchema}.{objectQualifier}Forum where ForumID = i_ForumID) AS ForumName,
		CAST(i_TopicID AS SIGNED) AS TopicID,
		(select Topic from {databaseSchema}.{objectQualifier}Topic where TopicID = i_TopicID) AS TopicName,		
		(select ThemeURL from {databaseSchema}.{objectQualifier}Forum where ForumID = i_ForumID LIMIT 1) AS ForumTheme,
		ici_ActiveUpdate AS ActiveUpdate
		from
		{databaseSchema}.{objectQualifier}ActiveAccess x 
		where
		x.UserID = ici_UserID and x.ForumID=IFNULL(i_ForumID,0); 
COMMIT;
-- SET AUTOCOMMIT=1;

END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}pmessage_archive(i_UserPMessageID INT) 
 BEGIN
	/* set IsArchived bit */
	UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
	SET 
	   IsRead = IFNULL(SIGN((`Flags` | 4) & 1)>0,false)  ,
	   IsInOutbox = IFNULL(SIGN((`Flags` | 4) & 2)>0,false),
	   IsArchived = IFNULL(SIGN((`Flags` | 4) & 4)>0,false),
	   IsDeleted = IFNULL(SIGN((`Flags` | 4) & 8)>0,false),
	   `Flags` = (`Flags` | 4) 
	WHERE UserPMessageID = i_UserPMessageID AND IsArchived = 0;
 END;

--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}pmessage_delete(i_UserPMessageID INT, i_FromOutbox TINYINT(1)) 
BEGIN
	DECLARE ici_PMessageID INT; 
	
	SELECT PMessageID INTO ici_PMessageID FROM {databaseSchema}.{objectQualifier}UserPMessage 
	where `UserPMessageID` = i_UserPMessageID LIMIT 1; 
	
	
	
	IF ( i_FromOutbox = 1 AND 
	EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}UserPMessage
	WHERE UserPMessageID = i_UserPMessageID AND IsInOutBox = 1 ) ) THEN
	-- set IsInOutbox bit which will remove it from the senders outbox
		UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
		SET 
		   IsRead = IFNULL(SIGN((Flags ^ 2) & 1)>0,false),
		   IsInOutbox = IFNULL(SIGN((Flags ^ 2) & 2)>0,false),
		   IsArchived = IFNULL(SIGN((Flags ^ 2) & 4)>0,false),
		   IsDeleted = IFNULL(SIGN((Flags ^ 2) & 8)>0,false),
		   Flags = (Flags ^ 2) 
		WHERE UserPMessageID = i_UserPMessageID;
		
		
	END IF;
	
	IF i_FromOutbox = 0 OR i_FromOutbox IS NULL THEN 
		-- The pmessage is in archive but still is in sender outbox
					
		IF ( 
	EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}UserPMessage
	WHERE UserPMessageID = i_UserPMessageID AND SIGN(Flags & 4) = 1 AND SIGN(Flags & 2) = 1 AND SIGN(Flags & 8) = 0) ) THEN
	  -- Remove archive flag
	UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
	SET  IsRead = IFNULL(SIGN((Flags ^ 4) & 1)>0,false)  ,
	   IsInOutbox = IFNULL(SIGN((Flags ^ 4) & 2)>0,false),
	   IsArchived = IFNULL(SIGN((Flags ^ 4) & 4)>0,false),
	   IsDeleted = IFNULL(SIGN((Flags ^ 4) & 8)>0,false),
	Flags = (Flags ^ 4)   
	WHERE UserPMessageID = i_UserPMessageID;
	END IF;		
	  -- set IsInOutbox bit which will remove it from the senders outbox
	  UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
	  SET 
	   IsRead = IFNULL(SIGN((Flags ^ 8) & 1)>0,false)  ,
	   IsInOutbox = IFNULL(SIGN((Flags ^ 8) & 2)>0,false),
	   IsArchived = IFNULL(SIGN((Flags ^ 8) & 4)>0,false),
	   IsDeleted = IFNULL(SIGN((Flags ^ 8) & 8)>0,false),
	  Flags = Flags ^ 8 WHERE UserPMessageID = i_UserPMessageID;	 
	 END IF;
	
	
	
	-- see if there are no longer references to this PM.
	IF ( EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}UserPMessage WHERE UserPMessageID = i_UserPMessageID AND IsInOutbox = 0 AND IsDeleted = 1 ) ) THEN
	-- delete
		DELETE FROM {databaseSchema}.{objectQualifier}UserPMessage WHERE PMessageID = ici_PMessageID;
		DELETE FROM {databaseSchema}.{objectQualifier}PMessage WHERE `PMessageID` = ici_PMessageID;
	END IF;
	
	/*SET ici_MsgCount = (SELECT COUNT(UserPMessageID) FROM {databaseSchema}.{objectQualifier}UserPMessage  WHERE  Flags & 2 = 0 AND UserPMessageID = i_UserPMessageID) ;
	
	IF ici_MsgCount > 0 THEN
	SET ici_dummy =1;
	DELETE FROM {databaseSchema}.{objectQualifier}UserPMessage WHERE PMessageID = ici_PMessageID;
	DELETE FROM {databaseSchema}.{objectQualifier}PMessage WHERE PMessageID = ici_PMessageID;
	END IF;*/
	
 END;
--GO



/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}pmessage_list(i_FromUserID INT,i_ToUserID INT,i_UserPMessageID INT) 
BEGIN
	 SELECT
		   a.PMessageID,
		   a.ReplyTo,
		   b.UserPMessageID,
		   a.FromUserID,
		   d.`Name` AS FromUser,
		   b.`UserID` AS ToUserId,
		   c.`Name` AS ToUser,
		   a.Created, 
		   a.Subject,
		   a.Body,
		   a.Flags,
		   b.IsRead,
		   b.IsInOutbox,
		   b.IsArchived,
		   b.IsDeleted,
		   b.IsReply
	 FROM
		 {databaseSchema}.{objectQualifier}PMessage a
		 INNER JOIN
		 {databaseSchema}.{objectQualifier}UserPMessage b 
		 ON a.PMessageID = b.PMessageID
		 INNER JOIN
		 {databaseSchema}.{objectQualifier}User c 
		 ON b.UserID = c.UserID
		 INNER JOIN
		 {databaseSchema}.{objectQualifier}User d ON a.FromUserID = d.UserID		
	WHERE	((i_UserPMessageID IS NOT NULL AND b.UserPMessageID=i_UserPMessageID) 
			  OR
			 (i_ToUserID   IS NOT NULL AND b.`UserID` = i_ToUserID) 
			  OR
			  (i_FromUserID IS NOT NULL AND a.FromUserID = i_FromUserID AND b.IsDeleted = 0))  
	ORDER BY Created DESC;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}pmessage_markread(i_UserPMessageID INT)

 BEGIN
	UPDATE {databaseSchema}.{objectQualifier}UserPMessage 
	SET 
	 IsRead = IFNULL(SIGN((`Flags` | 1) & 1)>0,false)  ,
	   IsInOutbox = IFNULL(SIGN((`Flags` | 1) & 2)>0,false),
	   IsArchived = IFNULL(SIGN((`Flags` | 1) & 4)>0,false),
	   IsDeleted = IFNULL(SIGN((`Flags` | 1) & 8)>0,false),
	`Flags` = `Flags` | 1 
	WHERE UserPMessageID = i_UserPMessageID AND SIGN(`Flags` &  1) = 0;
/* IsRead */
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}pmessage_info()
 BEGIN
 /* Is Read and Is Deleted bits */
SELECT
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}UserPMessage 
		WHERE IsRead = 1  
		AND IsDeleted = 0 ) AS NumRead, 		
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}UserPMessage
		WHERE IsRead = 0   
		AND IsDeleted = 0 ) AS NumUnread,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}UserPMessage
		WHERE IsDeleted = 0 ) AS NumTotal;		
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE PROCEDURE {databaseSchema}.{objectQualifier}pmessage_prune(i_DaysRead INT,i_DaysUnread INT,
				 i_UTCTIMESTAMP DATETIME) 
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}UserPMessage
	WHERE CAST(SIGN((`Flags` | 4)  & 1) AS SIGNED)<>0
	and DATEDIFF(i_UTCTIMESTAMP,
	(SELECT x.Created FROM {databaseSchema}.{objectQualifier}PMessage x 
	WHERE x.PMessageID={databaseSchema}.{objectQualifier}UserPMessage.PMessageID))>i_DaysRead;
 
	DELETE FROM {databaseSchema}.{objectQualifier}UserPMessage
	WHERE CAST(SIGN((`Flags` | 4)  & 1) AS SIGNED)=0
	and DATEDIFF(i_UTCTIMESTAMP,(SELECT Created FROM {databaseSchema}.{objectQualifier}PMessage x 
	WHERE x.PMessageID={databaseSchema}.{objectQualifier}UserPMessage.PMessageID))>i_DaysUnread;
 
	DELETE FROM {databaseSchema}.{objectQualifier}PMessage
	WHERE NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}UserPMessage x WHERE x.PMessageID={databaseSchema}.{objectQualifier}PMessage.PMessageID);
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}pmessage_save(
	i_FromUserID	INT,
	i_ToUserID	INT,
	i_Subject	VARCHAR(128),
	i_Body		TEXT,
	i_Flags		INT,
	i_ReplyTo   INT,
	i_UTCTIMESTAMP DATETIME
 ) 
BEGIN
	DECLARE ici_PMessageID INT;
	DECLARE ici_UserID INT;
	IF i_ReplyTo<0 THEN
	INSERT INTO {databaseSchema}.{objectQualifier}PMessage(FromUserID,Created,Subject,Body,Flags)
	VALUES(i_FromUserID,i_UTCTIMESTAMP,i_Subject,i_Body,i_Flags);
	ELSE
	INSERT INTO {databaseSchema}.{objectQualifier}PMessage(FromUserID,Created,Subject,Body,Flags,ReplyTo)
	VALUES(i_FromUserID,i_UTCTIMESTAMP,i_Subject,i_Body,i_Flags,i_ReplyTo);
	UPDATE {databaseSchema}.{objectQualifier}UserPMessage SET IsReply = 1 WHERE PMessageID = i_ReplyTo;
	END IF;
	SET ici_PMessageID = LAST_INSERT_ID();
	IF (i_ToUserID = 0) THEN
	
		INSERT INTO {databaseSchema}.{objectQualifier}UserPMessage(UserID,PMessageID,Flags,IsRead,IsInOutbox,IsArchived,IsDeleted) 
		SELECT
				a.UserID,ici_PMessageID,2,IFNULL(SIGN(2 & 1)>0,false),IFNULL(SIGN(2 & 2)>0,false),IFNULL(SIGN(2 & 4)>0,false),IFNULL(SIGN(2 & 8)>0,false)
		FROM
				{databaseSchema}.{objectQualifier}User a
				JOIN {databaseSchema}.{objectQualifier}UserGroup b on b.UserID=a.UserID
				JOIN {databaseSchema}.{objectQualifier}Group c on c.GroupID=b.GroupID 
				WHERE	(c.Flags & 2)=0 AND
				c.BoardID=(SELECT BoardID from {databaseSchema}.{objectQualifier}User x 
				WHERE  x.UserID=i_FromUserID) AND a.UserID<>i_FromUserID
						GROUP BY a.UserID;
	
	ELSE 	
		INSERT INTO {databaseSchema}.{objectQualifier}UserPMessage(UserID,PMessageID,Flags,IsRead,IsInOutbox,IsArchived,IsDeleted) 
				VALUES (i_ToUserID,ici_PMessageID,2,IFNULL(SIGN(2 & 1)>0,false),IFNULL(SIGN(2 & 2)>0,false),IFNULL(SIGN(2 & 4)>0,false),IFNULL(SIGN(2 & 8)>0,false));
	END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE procedure {databaseSchema}.{objectQualifier}poll_remove(
	i_PollGroupID int, i_PollID int, i_BoardID INT, i_RemoveCompletely TINYINT(1), i_RemoveEverywhere TINYINT(1)
 ) 
 BEGIN
 declare ici_groupcount int;

 IF i_RemoveCompletely = 1  THEN
	/* DELETE vote records first */
	DELETE FROM {databaseSchema}.{objectQualifier}PollVote WHERE PollID = i_PollID;
	/* DELETE choices first */
	DELETE FROM {databaseSchema}.{objectQualifier}Choice WHERE PollID = i_PollID;
	/* DELETE it from topic itself */
	UPDATE {databaseSchema}.{objectQualifier}Poll SET PollGroupID = NULL WHERE PollID = i_PollID;
	/* DELETE poll */
	DELETE FROM {databaseSchema}.{objectQualifier}Poll WHERE PollID = i_PollID;
	DELETE FROM {databaseSchema}.{objectQualifier}Poll WHERE PollID = i_PollID;
		IF  NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}Poll 
			 WHERE PollGroupID = i_PollGroupID LIMIT 1) THEN
		  UPDATE {databaseSchema}.{objectQualifier}Topic set PollID = NULL where PollID = i_PollGroupID;
		  UPDATE {databaseSchema}.{objectQualifier}Forum set PollGroupID = NULL where PollGroupID = i_PollGroupID;
		  UPDATE {databaseSchema}.{objectQualifier}Category set PollGroupID = NULL where PollGroupID = i_PollGroupID; 
		  DELETE FROM  {databaseSchema}.{objectQualifier}PollGroupCluster WHERE PollGroupID = i_PollGroupID;
		 END IF;     
	
 END IF;

 END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}poll_stats(
i_PollID INT)
BEGIN

DECLARE iciCount INT;
DECLARE iciStats INT;
DECLARE ici_SumVotes INT;

SET ici_SumVotes =  (SELECT SUM(x.Votes)
FROM   {databaseSchema}.{objectQualifier}Choice x
WHERE  x.PollID = i_PollID);

IF ici_SumVotes =0 THEN SET iciCount =1;
ELSE
SET iciCount =ici_SumVotes ;
END IF;

SELECT 
		a.PollID,
		b.Question,
		b.Closes,
		b.UserID,		
		a.ObjectPath,
		a.MimeType,
		b.ObjectPath AS QuestionObjectPath,
		b.MimeType AS QuestionMimeType,
		a.ChoiceID,
		a.Choice,
		CAST(a.Votes AS SIGNED) AS Votes,
		-- is bound
		pg.Flags & 2 AS IsBound,
		-- is closed bound 
		b.Flags & 4 AS IsClosedBound,
		-- Allow Multiple Choices	
		b.Flags & 8 AS AllowMultipleChoices,
		b.Flags & 16 AS ShowVoters,
		b.Flags & 32 AS AllowSkipVote,
ici_SumVotes as Total,
CAST(100*a.Votes/iciCount AS UNSIGNED) AS Stats
FROM   {databaseSchema}.{objectQualifier}Choice a
JOIN
{databaseSchema}.{objectQualifier}Poll b 
ON a.PollID=b.PollID
INNER JOIN  
{databaseSchema}.{objectQualifier}PollGroupCluster pg 
ON pg.PollGroupID = b.PollGroupID
AND b.PollID = i_PollID;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}pollgroup_stats(i_PollGroupID int) 
BEGIN
		SELECT		
		pg.UserID AS GroupUserID,	
		a.PollID,
		b.PollGroupID,
		b.Question,
		b.Closes,
		a.ChoiceID,		
		a.Choice,
		a.Votes as Votes,
		a.ObjectPath,
		a.MimeType,
		b.ObjectPath AS QuestionObjectPath,
		b.MimeType AS QuestionMimeType,
		-- is bound
		pg.Flags & 2 AS IsBound,
		-- is closed bound 
		b.Flags & 4 AS IsClosedBound,
		-- Allow Multiple Choices	
		b.Flags & 8 AS AllowMultipleChoices,
		SIGN(b.Flags & 16) AS ShowVoters,
		SIGN(b.Flags & 32) AS AllowSkipVote,
		(select sum(x.Votes) from {databaseSchema}.{objectQualifier}Choice x where  x.PollID = a.PollID) as Total,
		(select 100 * a.Votes / (case sum(x.Votes) when 0 then 1 else sum(x.Votes) end) from {databaseSchema}.{objectQualifier}Choice x where x.PollID=a.PollID) AS Stats
	FROM
		{databaseSchema}.{objectQualifier}Choice a		
	INNER JOIN 
		{databaseSchema}.{objectQualifier}Poll b ON b.PollID = a.PollID
	INNER JOIN  
		{databaseSchema}.{objectQualifier}PollGroupCluster pg ON pg.PollGroupID = b.PollGroupID	  
	WHERE
		pg.PollGroupID = i_PollGroupID
		ORDER BY b.PollGroupID;
	--	GROUP BY a.PollID, b.Question, b.Closes, a.ChoiceID, a.Choice,a.Votes
		END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}poll_update(
	i_PollID		INT,
	i_Question	VARCHAR(128),
	i_Closes 	DATETIME,
	i_QuestionObjectPath VARCHAR(255), 
	i_QuestionMimeType VARCHAR(50),
	i_IsBounded  TINYINT(1),
	i_IsClosedBounded  TINYINT(1),
	i_AllowMultipleChoices TINYINT(1),
	i_ShowVoters TINYINT(1),
	i_AllowSkipVote TINYINT(1)
 ) 
BEGIN

declare ici_pgid int;
declare ici_flags int;

		update {databaseSchema}.{objectQualifier}Poll
		set Flags = 0 where PollID = i_PollID AND Flags IS NULL;

		SELECT Flags INTO ici_flags FROM {databaseSchema}.{objectQualifier}Poll		
		where PollID = i_PollID;

		-- is closed bound flag
		SET ici_flags = (CASE				
		WHEN i_IsClosedBounded > 0 AND (ici_flags & 4) <> 4 THEN ici_flags | 4		
		WHEN i_IsClosedBounded <= 0 AND (ici_flags & 4) = 4  THEN ici_flags ^ 4
		ELSE ici_flags END);

		-- allow multiple choices flag
		SET ici_flags = (CASE				
		WHEN i_AllowMultipleChoices > 0 AND (ici_flags & 8) <> 8 THEN ici_flags | 8		
		WHEN i_AllowMultipleChoices <= 0 AND (ici_flags & 8) = 8  THEN ici_flags ^ 8
		ELSE ici_flags END);

		-- show who's voted for a poll flag
		SET ici_flags = (CASE				
		WHEN i_ShowVoters > 0 AND (ici_flags & 16) <> 16 THEN ici_flags | 16		
		WHEN i_ShowVoters <= 0 AND (ici_flags & 16) = 16  THEN ici_flags ^ 16
		ELSE ici_flags END);

		-- allow users don't vote and see results
		SET ici_flags = (CASE				
		WHEN i_AllowSkipVote > 0 AND (ici_flags & 32) <> 32 THEN ici_flags | 32		
		WHEN i_AllowSkipVote <= 0 AND (ici_flags & 32) = 32  THEN ici_flags ^ 32
		ELSE ici_flags END);

	  update {databaseSchema}.{objectQualifier}Poll
		set Question	=	i_Question,
			Closes		=	i_Closes,
			ObjectPath = i_QuestionObjectPath,
			MimeType = i_QuestionMimeType,
			Flags	= ici_flags
		where PollID = i_PollID;

	  SELECT  PollGroupID INTO ici_pgid FROM {databaseSchema}.{objectQualifier}Poll
	  where PollID = i_PollID;
   
	update {databaseSchema}.{objectQualifier}PollGroupCluster
		set Flags	= (CASE 
		WHEN i_IsBounded > 0 AND (Flags & 2) <> 2 THEN Flags | 2 		
		WHEN i_IsBounded <= 0 AND (Flags & 2) = 2 THEN Flags ^ 2 		
		ELSE Flags END)		
		where PollGroupID = ici_pgid;	
 
END;
--GO



CREATE PROCEDURE {databaseSchema}.{objectQualifier}pollgroup_votecheck(i_PollGroupID int, i_UserID int, i_RemoteIP VARCHAR(39)) 
BEGIN
	IF i_UserID IS NULL THEN
	  IF i_RemoteIP IS NOT NULL THEN		
			-- check by remote IP
			SELECT pv.PollID, pv.ChoiceID, u.Name as UserName FROM {databaseSchema}.{objectQualifier}PollVote pv
			JOIN {databaseSchema}.{objectQualifier}User u ON u.UserID = pv.UserID
			WHERE pv.PollID IN ( SELECT PollID FROM {databaseSchema}.{objectQualifier}Poll 
			WHERE PollGroupID = i_PollGroupID) AND pv.RemoteIP = i_RemoteIP;		
		ELSE		
		-- to get structure
			SELECT pv.PollID, pv.ChoiceID, u.Name as UserName FROM {databaseSchema}.{objectQualifier}PollVote pv
			JOIN {databaseSchema}.{objectQualifier}User u ON u.UserID = pv.UserID
			WHERE pv.PollID IN ( SELECT PollID FROM {databaseSchema}.{objectQualifier}Poll 
			WHERE PollGroupID = i_PollGroupID);
		END IF;	 
	ELSE	    
		-- check by userid or remote IP
		SELECT pv.PollID, pv.ChoiceID, u.Name as UserName FROM {databaseSchema}.{objectQualifier}PollVote pv
		JOIN {databaseSchema}.{objectQualifier}User u ON u.UserID = pv.UserID
		WHERE pv.PollID IN (SELECT PollID FROM {databaseSchema}.{objectQualifier}Poll
		WHERE PollGroupID = i_PollGroupID) AND (pv.UserID = i_UserID OR pv.RemoteIP = i_RemoteIP);
	END IF;
		END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}pollvote_check(i_PollID INT, i_UserID INT,i_RemoteIP VARCHAR(39))
BEGIN
	IF i_UserID IS NULL THEN 	
		IF i_RemoteIP IS NOT NULL THEN 		
			/*check by remote IP*/
			SELECT PollVoteID FROM {databaseSchema}.{objectQualifier}PollVote
	   WHERE PollID = i_PollID AND RemoteIP = i_RemoteIP;
		END IF;
	ELSE
		/*check by userid or remote IP*/
		  SELECT PollVoteID FROM {databaseSchema}.{objectQualifier}PollVote
	   WHERE PollID = i_PollID AND (UserID = i_UserID OR RemoteIP = i_RemoteIP);
  END IF;
END;
--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM */


CREATE  PROCEDURE {databaseSchema}.{objectQualifier}post_alluser(
				i_BoardID    INT,
				i_UserID     INT,
				i_PageUserID INT,
				I_NumberOfMessages INT)
BEGIN
		
		set @stmtstrpau  =
		CONCAT('SELECT a.MessageID,
				a.Posted,
				c.Topic AS `Subject`,
				a.Message,
				a.IP,
				a.UserID,
				a.Flags,                
				IFNULL(a.UserName,b.Name) AS UserName,
				IFNULL(a.UserDisplayName,b.Name) AS UserDisplayName,
				b.Signature,
				c.TopicID,
				c.ForumID,
				x.ReadAccess           
		FROM    {databaseSchema}.{objectQualifier}Message a
				 JOIN {databaseSchema}.{objectQualifier}User b
				   ON b.UserID = a.UserID
				 JOIN {databaseSchema}.{objectQualifier}Topic c
				   ON c.TopicID = a.TopicID
				 JOIN {databaseSchema}.{objectQualifier}Forum d
				   ON d.ForumID = c.ForumID
				 JOIN {databaseSchema}.{objectQualifier}Category e
				   ON e.CategoryID = d.CategoryID  
				 JOIN {databaseSchema}.{objectQualifier}ActiveAccess x 
				   ON x.ForumID=d.ForumID	           
		WHERE    a.UserID = ',i_UserID,' AND
				 x.UserID = ',i_PageUserID, 
				 ' AND x.ReadAccess <> 0      
				 AND e.BoardID = ',i_BoardID,
				 ' AND (a.Flags & 24) = 16
				AND (c.Flags & 8) = 0
		ORDER BY a.Posted DESC LIMIT ',COALESCE(I_NumberOfMessages,10000000),'');
		PREPARE stmt_pau FROM @stmtstrpau;
		EXECUTE stmt_pau;
		DEALLOCATE PREPARE stmt_pau;         
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}post_list
				 (i_TopicID INT,				
				 i_AuthorUserID INT,
				 i_PageUserID INT,
				 i_UpdateViewCount SMALLINT,
				 i_ShowDeleted TINYINT(1),
				 i_StyledNicks TINYINT(1),				
				 i_SincePostedDate datetime,
				 i_ToPostedDate datetime,
				 i_SinceEditedDate datetime,
				 i_ToEditedDate datetime,
				 i_PageIndex int,
				 i_PageSize int,
				 i_SortPosted int,
				 i_SortEdited int,
				 i_SortPosition int,
				 i_ShowThanks TINYINT(1),
				 i_ShowReputation TINYINT(1),
				 i_MessagePosition int,
				 i_MessageID int,
				 i_LastRead datetime,
				 i_UTCTIMESTAMP datetime
				 )
BEGIN
   declare ici_post_totalrowsnumber int default 0; 
   declare ici_firstselectrownum int; 
  
   declare ici_firstselectposted datetime;
   declare ici_firstselectedited datetime;

   declare ici_floor decimal;
   declare ici_ceiling decimal;
  
   declare ici_offset int; 
   
   declare ici_pagecorrection int;
   declare ici_pageshift int;

   set @PostTotalRowsNumber = null;
   set @i_FirstSelectPosted = null;
   set @i_FirstSelectEdited = null;
   
IF i_SortPosted IS NULL THEN
SET i_SortPosted = 2;
END IF;
/* IF i_PageIndex IS NULL OR i_PageIndex = 0 THEN
SET i_PageIndex = 1; 
END IF; */
SET i_PageIndex = i_PageIndex + 1;
	/* how set nocount on */
	IF i_UpdateViewCount>0 THEN
		UPDATE {databaseSchema}.{objectQualifier}Topic 
		SET `Views` = `Views` + 1 WHERE TopicID = i_TopicID;
	END IF;

	-- find total returned count
		select
		COUNT(m.MessageID) INTO ici_post_totalrowsnumber
	from
		{databaseSchema}.{objectQualifier}Message m
	where
		m.TopicID = i_TopicID
		AND m.Flags & 16 = 16
		AND (m.Flags & 8 <> 8 OR ((i_ShowDeleted = 1 AND m.Flags & 8 = 8 ) 
		OR (i_AuthorUserID > 0 AND m.UserID = i_AuthorUserID)))
		AND m.Posted BETWEEN
		 i_SincePostedDate AND i_ToPostedDate;
		 /*
		AND 
		m.Edited >= SinceEditedDate
		*/

  -- number of messages on the last page @post_totalrowsnumber - @floor*@PageSize
   if (i_MessagePosition > 0) then
	   -- round to ceiling - total number of pages  
	   SET ici_ceiling = CEILING(CAST(ici_post_totalrowsnumber AS decimal)/i_PageSize) ;
	   -- round to floor - a number of full pages
	   SET ici_floor = FLOOR(CAST(ici_post_totalrowsnumber AS decimal)/i_PageSize);

	   SET ici_pageshift = i_MessagePosition - (ici_post_totalrowsnumber - ici_floor*i_PageSize);
			if  ici_pageshift < 0
			   then
				  SET ici_pageshift = 0;
					 end if;  
   -- here pageshift converts to full pages 
   if (ici_pageshift <= 0)
   then    
   set ici_pageshift = 0;   
   else  
   set ici_pageshift = CEILING(CAST(ici_pageshift AS decimal)/i_PageSize); 
   end  if; 
   
   SET i_PageIndex = ici_ceiling -ici_pageshift ;
   if ici_ceiling != ici_floor then
   SET i_PageIndex = i_PageIndex - 1;
   end if;	      
   
   select (i_PageIndex) * i_PageSize + 1 INTO ici_firstselectrownum;  
  -- SET i_PageIndex = i_PageIndex + 2;

 else  
   -- select i_PageIndex + 1 INTO i_PageIndex;
   select (i_PageIndex-1) * i_PageSize + 1 INTO ici_firstselectrownum;
   
 end if;
  
   -- find first selectedrowid 
  /* if (ici_firstselectrownum > 0) then  
   set rowcount @firstselectrownum;
   else
   -- should not be 0
   set rowcount 1;
   end if; */

 set @pplv = CONCAT('select
CAST(' , coalesce(ici_post_totalrowsnumber,1),  ' AS SIGNED),
m.Posted,
m.Edited
INTO  	@PostTotalRowsNumber,  
@i_FirstSelectPosted, 
@i_FirstSelectEdited
	 from
		{databaseSchema}.{objectQualifier}Message m	
	where
		m.TopicID = ',i_TopicID, 
		-- IsApproved
		' AND (m.Flags & 16) = 16
		-- IsDeleted
		AND ((m.Flags & 8) <> 8 OR ((',i_ShowDeleted, ' = 1 AND (m.Flags & 8) = 8) 
		OR (',i_AuthorUserID, ' > 0 AND m.UserID = ', i_AuthorUserID,')))
		AND m.Posted BETWEEN ''',i_SincePostedDate,''' AND ''',i_ToPostedDate,''' 
		 /*
		AND m.Edited > @SinceEditedDate
		*/
		
	order by
		(case 
		when ',i_SortPosition,' = 1 then m.Position end) ASC,	
		(case 
		when ',i_SortPosted,' = 2 then m.Posted end) DESC,
		(case 
		when ',i_SortPosted,' = 1 then m.Posted end) ASC, 
		(case 
		when ',i_SortEdited,' = 2 then m.Edited end) DESC,
		(case
		when ',i_SortEdited,' = 1 then m.Edited end) ASC LIMIT 1 OFFSET ',ici_firstselectrownum - 1,'');
		
		PREPARE plist2 FROM @pplv;

		EXECUTE plist2 ;		
		 
		DEALLOCATE PREPARE plist2;		

		set @pplr = CONCAT('select
		d.TopicID,
		d.Topic,
		d.Priority,
		d.Description,
		d.Status,
		d.Styles,
		d.PollID,
		d.UserID AS TopicOwnerID,	
		d.Flags AS TopicFlags,
		g.Flags AS ForumFlags,
		m.MessageID,
		m.Posted,
		d.Topic AS Subject,       
		m.Message AS Message, 
		m.Description as MessageDescription, 
		m.UserID,
		m.Position,
		m.Indent,
		m.IP,
		m.Flags,
		m.EditReason,
		m.IsModeratorChanged,
		(m.Flags & 8) = 8 AS IsDeleted,
		m.DeleteReason,
		m.BlogPostID,
		m.ExternalMessageId,
		m.ReferenceMessageId,
		IfNull(m.UserName,b.Name) AS UserName,
		IfNull(m.UserDisplayName,b.DisplayName) AS DisplayName,       
		b.Suspended,
		b.Joined,
		b.Avatar,
		b.Signature,
		b.NumPosts AS Posts,
		b.Points,
		(CASE (',i_ShowReputation,') WHEN 1 THEN CAST(IFNULL((select repVote.VoteDate
		from {databaseSchema}.{objectQualifier}ReputationVote repVote 
		where repVote.ReputationToUserID=b.UserID and repVote.ReputationFromUserID = ',i_PageUserID,' LIMIT 1), NULL) as datetime) 
		ELSE CAST(NULL AS DATETIME) END) AS ReputationVoteDate,		
		d.Views,
		d.ForumID,
		c.Name AS RankName,		
		c.RankImage,	
		c.Style as RankStyle,	
		(case(',i_StyledNicks,')
			when 1 then  
			b.UserStyle 
			else '''' end) as Style, 
		IFNULL(m.Edited,m.Posted) AS Edited,
		m.EditedBy,
		IFNULL((select 1 from {databaseSchema}.{objectQualifier}Attachment x 
		where x.MessageID=m.MessageID LIMIT 1),0) AS HasAttachments,
		IFNULL((select  1 from {databaseSchema}.{objectQualifier}User x 
		where x.UserID=b.UserID and AvatarImage is not null LIMIT 1),0) AS HasAvatarImage,
		{databaseSchema}.{objectQualifier}biginttobool((b.Flags & 4)=4) as IsGuest, 
		-- ici_post_totalrowsnumber
		{databaseSchema}.{objectQualifier}biginttoint(',@PostTotalRowsNumber,') AS TotalRows,
		-- i_PageIndex
		{databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex,
		up.*
	from
		{databaseSchema}.{objectQualifier}Message m
		join {databaseSchema}.{objectQualifier}User b on b.UserID=m.UserID
		left join {databaseSchema}.{objectQualifier}UserProfile up on up.UserID=b.UserID
		join {databaseSchema}.{objectQualifier}Topic d on d.TopicID=m.TopicID
		join {databaseSchema}.{objectQualifier}Forum g on g.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category h on h.CategoryID=g.CategoryID
		join {databaseSchema}.{objectQualifier}Rank c on c.RankID=b.RankID
	where	   
		(m.TopicID = ',i_TopicID,')
		-- IsApproved
		AND IFNULL((m.Flags & 16)=16,false) 
		-- IsDeleted AuthorUserID	
		 AND 
		(IFNULL((m.Flags & 8) <> 8,false) OR ((',i_ShowDeleted,' = 1 AND IFNULL((m.Flags & 8 )= 8,false)) OR (',i_AuthorUserID,' > 0 AND m.UserID = ',i_AuthorUserID,'))) 
		AND 
		(m.Posted is null OR 
		(m.Posted is not null AND
		-- SortPosted
		(m.Posted >= (case 
		when ',i_SortPosted,' = 1 then
		 ''',@i_FirstSelectPosted,''' end) OR m.Posted <= (case 
		when ',i_SortPosted,' = 2 then  ''',@i_FirstSelectPosted,''' end) OR
		m.Posted >= (case 
		when ''',i_SortPosted,''' = 0 then 0 end))))	AND
		-- ToPostedDate
		(m.Posted <= (''',i_ToPostedDate,'''))	
		/*
		AND (m.Edited is null OR (m.Edited is not null AND
		(m.Edited >= (case 
		when @SortEdited = 1 then @firstselectedited end) 
		OR m.Edited <= (case 
		when @SortEdited = 2 then @firstselectedited end) OR
		m.Edited >= (case 
		when @SortEdited = 0 then 0
		end)))) 
		*/
	order by		
		(case 
		when ',i_SortPosition,' = 1 then m.Position end) ASC,	
		(case 
		when ',i_SortPosted,' = 2 then m.Posted end) DESC,
		(case 
		when ',i_SortPosted,' = 1 then m.Posted end) ASC, 
		(case 
		when ',i_SortEdited,' = 2 then m.Edited end) DESC,
		(case 
		when ',i_SortEdited,' = 1 then m.Edited end) ASC LIMIT ',i_PageSize,''); 

		PREPARE plist1 FROM  @pplr;		
	
		EXECUTE plist1;         
		DEALLOCATE PREPARE plist1;      

END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}post_list_reverse10(i_TopicID INT)
BEGIN
	/*set nocount on*/
 
	SELECT
		a.Posted,
		d.Topic AS `Subject`,
		a.MessageID,
		a.Message,		
		a.UserID,
		a.Flags,
		IFNULL(a.UserName,b.Name) AS UserName,
		IFNULL(a.UserDisplayName,b.DisplayName) AS DisplayName,
		b.Signature
	FROM
		{databaseSchema}.{objectQualifier}Message a 
		inner join {databaseSchema}.{objectQualifier}User b on b.UserID = a.UserID
		inner join {databaseSchema}.{objectQualifier}Topic d on d.TopicID = a.TopicID
	WHERE
		(a.Flags & 24)=16 AND
		a.TopicID = i_TopicID
	ORDER BY
		a.Posted DESC LIMIT 10;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}rank_delete(i_RankID INT) 
BEGIN
	DELETE from {databaseSchema}.{objectQualifier}Rank where RankID = i_RankID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE PROCEDURE {databaseSchema}.{objectQualifier}rank_list(i_BoardID INT,i_RankID INT) 
BEGIN
	IF i_RankID IS NULL THEN
		SELECT
			a.*
		FROM
			{databaseSchema}.{objectQualifier}Rank a
		WHERE
			a.BoardID=i_BoardID
		ORDER BY
			a.SortOrder,
			a.Name;
	ELSE
		SELECT
			a.*
		FROM
			{databaseSchema}.{objectQualifier}Rank a
		WHERE
			a.RankID = i_RankID;
		END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}rank_save(
	i_RankID		INT,
	i_BoardID	INT,
	i_Name		VARCHAR(128),
	i_IsStart	TINYINT(1),
	i_IsLadder	TINYINT(1),
	i_IsGuest	TINYINT(1),
	i_MinPosts	INT,
	i_RankImage	VARCHAR(128),
	i_PMLimit INT,
	i_Style VARCHAR(255),
	i_SortOrder SMALLINT,        
	i_Description VARCHAR(255),
	i_UsrSigChars INT,
	i_UsrSigBBCodes	VARCHAR(255),
	i_UsrSigHTMLTags VARCHAR(255),
	i_UsrAlbums INT,
	i_UsrAlbumImages INT
 ) 
BEGIN
	DECLARE ici_Flags INT;
 
	IF i_IsLadder=0 THEN SET i_MinPosts = NULL; END IF; 
	IF i_IsLadder=1 AND i_MinPosts IS NULL THEN SET i_MinPosts = 0; END IF;
	
	SET ici_Flags = 0;
	IF i_IsStart<>0 THEN SET ici_Flags = ici_Flags | 1; END IF;
	IF i_IsLadder<>0 THEN SET ici_Flags = ici_Flags | 2; END IF;
	IF i_IsGuest<>0 THEN SET ici_Flags = ici_Flags | 4; END IF;
	IF CHAR_LENGTH(i_Style) <= 2 THEN
		SET i_Style = NULL; END IF;
	
	IF i_RankID>0 THEN
		UPDATE {databaseSchema}.{objectQualifier}Rank 
				SET
			`Name` = i_Name,
			Flags = ici_Flags,
			MinPosts = i_MinPosts,
			RankImage = i_RankImage,
			PMLimit = i_PMLimit,
			Style = i_Style,
			SortOrder = i_SortOrder, 			
			Description = i_Description,
			UsrSigChars = i_UsrSigChars,
			UsrSigBBCodes = i_UsrSigBBCodes,
			UsrSigHTMLTags = i_UsrSigHTMLTags,
			UsrAlbums = i_UsrAlbums,
			UsrAlbumImages = i_UsrAlbumImages 
		WHERE RankID = i_RankID;
	
	ELSE 
		INSERT INTO {databaseSchema}.{objectQualifier}Rank(BoardID,`Name`,Flags,MinPosts,RankImage,PMLimit,Style,SortOrder,Description,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages)
		VALUES(i_BoardID,i_Name,ici_Flags,i_MinPosts,i_RankImage,i_PMLimit,i_Style,i_SortOrder,i_Description,i_UsrSigChars,i_UsrSigBBCodes,i_UsrSigHTMLTags,i_UsrAlbums,i_UsrAlbumImages);
		SET i_RankID = LAST_INSERT_ID();
	END IF;
	 IF (i_Style is not null and char_length(i_Style > 2)) THEN 		
		CALL {databaseSchema}.{objectQualifier}user_savestyle(null, i_RankID);
		END IF;
   
END;
--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}registry_list(i_Name VARCHAR(128),i_BoardID INT) 
BEGIN
	IF i_BoardID IS NULL THEN
	
		IF i_Name IS NULL OR i_Name = '' THEN
		
			SELECT * FROM {databaseSchema}.{objectQualifier}Registry 
						WHERE BoardID IS NULL;
		 ELSE
		
			SELECT * FROM {databaseSchema}.{objectQualifier}Registry 
						WHERE LOWER(`Name`) = LOWER(i_Name) and BoardID IS NULL;
		END IF;
	ELSE 	
		IF i_Name IS NULL OR i_Name = '' THEN
		
			SELECT * FROM {databaseSchema}.{objectQualifier}Registry 
						WHERE BoardID=i_BoardID;
		 ELSE
		
			SELECT * FROM {databaseSchema}.{objectQualifier}Registry 
						WHERE LOWER(`Name`) = LOWER(i_Name) and BoardID=i_BoardID;
		END IF;
	END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}registry_save(
				i_Name    VARCHAR(128),
				i_Value   TEXT,
				i_BoardID INT)
BEGIN
		
		IF i_BoardID IS NULL THEN
		BEGIN
			IF EXISTS (SELECT 1
					   FROM   {databaseSchema}.{objectQualifier}Registry
					   WHERE  Lower(`Name`) = Lower(i_Name)) THEN
			UPDATE {databaseSchema}.{objectQualifier}Registry
			SET    VALUE = i_Value
			WHERE  Lower(`Name`) = Lower(i_Name)
			AND BoardID IS NULL;
			ELSE            
				INSERT INTO {databaseSchema}.{objectQualifier}Registry
						   (`Name`,
							VALUE)
				VALUES     (Lower(i_Name),
							i_Value);
			END IF;
		END;
		ELSE
		BEGIN
			IF EXISTS (SELECT 1
					   FROM   {databaseSchema}.{objectQualifier}Registry
					   WHERE  Lower(`Name`) = Lower(i_Name)
					   AND BoardID = i_BoardID) THEN
			UPDATE {databaseSchema}.{objectQualifier}Registry
			SET    VALUE = i_Value
			WHERE  Lower(`Name`) = Lower(i_Name)
			AND BoardID = i_BoardID;
			ELSE            
				INSERT INTO {databaseSchema}.{objectQualifier}Registry
						   (`Name`,
							VALUE,
							BoardID)
				VALUES     (Lower(i_Name),
							i_Value,
							i_BoardID);
			END IF;
		END;
		END IF;
	END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}replace_words_delete(i_ID INT)
 BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}replace_words WHERE id = i_ID;
 END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}replace_words_list
 (
	i_BoardID INT,
	i_ID INT
 )
 BEGIN
	IF (i_ID IS NOT NULL AND i_ID <> 0) THEN
		SELECT * FROM {databaseSchema}.{objectQualifier}Replace_Words WHERE BoardID = i_BoardID AND ID = i_ID;
	ELSE
		SELECT * FROM {databaseSchema}.{objectQualifier}Replace_Words WHERE BoardID = i_BoardID;
		END IF;
 END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */ 
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}replace_words_save
 (
	i_BoardID INT,
	i_ID INT,
	i_BadWord VARCHAR(255),
	i_GoodWord VARCHAR(255)
 )
 BEGIN
	IF (i_ID IS NOT NULL AND i_ID <> 0) THEN 	
		UPDATE {databaseSchema}.{objectQualifier}replace_words SET BadWord = i_BadWord, GoodWord = i_GoodWord WHERE ID = i_ID;		
		ELSE 
		INSERT INTO {databaseSchema}.{objectQualifier}replace_words
			(BoardID,BadWord,GoodWord)
		VALUES
			(i_BoardID,i_BadWord,i_GoodWord);
	END IF;
 END;
--GO

/*****************************************************************************************************
//  Original code by: DLESKTECH at http://www.dlesktech.com/support.aspx
//  Modifications by: KASL Technologies at www.kasltechnologies.com
//  Modifications for integration into YAF/Conventions by Jaben Cargman
//  Rewritten by vzrus for mysql database.
*****************************************************************************************************/

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}shoutbox_getmessages(
 i_BoardId int, I_NumberOfMessages INT, I_StyledNicks TINYINT(1))
BEGIN

set @prstr_shsl = CONCAT('SELECT
		sh.UserName,
		sh.UserDisplayName,
		sh.UserID,
		sh.`Message`,
		sh.ShoutBoxMessageID,
		sh.`Date`,
		(case(',I_StyledNicks,') 
			when 1 then usr.UserStyle  
			else NULL end ) AS Style    
	FROM
		{databaseSchema}.{objectQualifier}ShoutboxMessage sh
		JOIN {databaseSchema}.{objectQualifier}User usr ON usr.UserID = sh.UserID
		WHERE 
		sh.BoardID = ',i_BoardId,
	' ORDER BY sh.Date DESC LIMIT ',I_NumberOfMessages,'');
	PREPARE stmt_shsl FROM @prstr_shsl;
	EXECUTE stmt_shsl;
	DEALLOCATE PREPARE stmt_shsl;         
	END;
--GO 

CREATE PROCEDURE {databaseSchema}.{objectQualifier}shoutbox_savemessage(
	i_BoardId int,
	i_UserID		int,
	i_UserName		VARCHAR(128),	
	i_Message		text,
	i_Date			datetime,
	i_IP			VARCHAR(39),
	i_UTCTIMESTAMP DATETIME
)
BEGIN
DECLARE ici_OverrideDisplayName TINYINT(1);

	-- i_UserName def null
	-- i_Date def null
	IF i_Date IS NULL THEN
		SET i_Date = i_UTCTIMESTAMP;
		END IF;
	SELECT SIGN(COUNT(Name))  INTO ici_OverrideDisplayName FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID AND Name != i_UserName;	
	INSERT {databaseSchema}.{objectQualifier}ShoutboxMessage (BoardID, UserName,UserDisplayName, UserID, `Message`, `Date`, IP)
	VALUES (i_BoardId, i_UserName,(CASE WHEN ici_OverrideDisplayName = 1 THEN i_UserName ELSE (SELECT DisplayName FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID) END) ,i_UserID, i_Message, i_Date, i_IP);

END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}shoutbox_clearmessages(i_BoardId int, i_UTCTIMESTAMP datetime)
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}ShoutboxMessage 
	WHERE BoardID = i_BoardId AND DATEDIFF(UTC_DATE(),LastPosted) > 1;
END;
--GO



/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}smiley_delete(i_SmileyID INT)
BEGIN
	IF i_SmileyID IS NOT NULL THEN
		DELETE FROM {databaseSchema}.{objectQualifier}Smiley WHERE SmileyID=i_SmileyID;
	ELSE
		DELETE FROM {databaseSchema}.{objectQualifier}Smiley;
		END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}smiley_list(
				 i_BoardID INT,
				 i_SmileyID INT) 
BEGIN
IF i_SmileyID IS NULL THEN
		SELECT 	
		SmileyID,
		BoardID,
		Code,
		Icon,
		Emoticon,
		CAST(SortOrder AS SIGNED) AS SortOrder
		FROM {databaseSchema}.{objectQualifier}Smiley WHERE BoardID=i_BoardID ORDER BY SortOrder, CHAR_LENGTH(Code) DESC;
	ELSE
		SELECT 		
		SmileyID,
		BoardID,
		Code,
		Icon,
		Emoticon,
		CAST(SortOrder AS SIGNED) AS  SortOrder
		FROM {databaseSchema}.{objectQualifier}Smiley WHERE SmileyID=i_SmileyID ORDER BY SortOrder;
		END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}smiley_listunique(i_BoardID INT) 
BEGIN
	SELECT 
		Icon, 
		Emoticon,
		(SELECT Code from {databaseSchema}.{objectQualifier}Smiley x where x.Icon={databaseSchema}.{objectQualifier}Smiley.Icon ORDER BY Code LIMIT 1) AS Code,
		(SELECT SortOrder from {databaseSchema}.{objectQualifier}Smiley x where x.Icon={databaseSchema}.{objectQualifier}Smiley.Icon ORDER BY x.SortOrder ASC LIMIT 1) AS SortOrder
	FROM 
		{databaseSchema}.{objectQualifier}Smiley
	WHERE
		BoardID=i_BoardID
	GROUP BY
		Icon,
		Emoticon
	ORDER BY
		SortOrder,
		Code;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}smiley_resort(i_BoardID INT,i_SmileyID INT,i_Move INT)
BEGIN
	DECLARE ici_Position INT;
 
	SELECT SortOrder INTO ici_Position FROM {databaseSchema}.{objectQualifier}Smiley WHERE BoardID=i_BoardID AND SmileyID=i_SmileyID;
 
	IF (ici_Position IS NOT NULL) THEN
 
	IF (i_Move > 0) THEN
		UPDATE {databaseSchema}.{objectQualifier}Smiley
			SET SortOrder=SortOrder-1
			WHERE BoardID=i_BoardID and 
				SortOrder BETWEEN ici_Position AND (ici_Position + i_Move) AND
				SortOrder BETWEEN 1 and 255;
	
	ELSEIF (i_Move < 0) THEN
		update {databaseSchema}.{objectQualifier}Smiley
			set SortOrder=SortOrder+1
			where BoardID=i_BoardID and 
				SortOrder between (ici_Position+i_Move) and ici_Position and
				SortOrder between 0 and 254;
	END IF;
 
	SET ici_Position = ici_Position + i_Move;
 
	IF (ici_Position>255) THEN SET ici_Position = 255;
	ELSEIF (ici_Position<0) THEN  SET ici_Position = 0; END IF;

	UPDATE {databaseSchema}.{objectQualifier}Smiley
		SET SortOrder=ici_Position
		WHERE BoardID=i_BoardID AND 
			SmileyID=i_SmileyID;
END IF;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}smiley_save
(i_SmileyID INT,i_BoardID INT,i_Code VARCHAR(10),i_Icon VARCHAR(128),i_Emoticon VARCHAR(128),i_SortOrder TINYINT(3),i_Replace TINYINT(1)) 
BEGIN
	IF i_SmileyID IS NOT NULL THEN
		UPDATE {databaseSchema}.{objectQualifier}Smiley SET Code = i_Code, Icon = i_Icon, Emoticon = i_Emoticon, SortOrder = i_SortOrder WHERE SmileyID = i_SmileyID;
	
	ELSE
		IF i_Replace>0 THEN
			DELETE FROM {databaseSchema}.{objectQualifier}Smiley WHERE Code=i_Code; 
				END IF;
 
		IF NOT EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}Smiley 
							   WHERE BoardID=i_BoardID AND Code=i_Code) THEN
			INSERT INTO {databaseSchema}.{objectQualifier}Smiley(BoardID,Code,Icon,Emoticon,SortOrder)
									  VALUES(i_BoardID,i_Code,i_Icon,i_Emoticon,i_SortOrder); 
				END IF;
	END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}system_initialize(
	i_Name		VARCHAR(128),
	i_TimeZone	INT,
	i_Culture	VARCHAR(10),
	i_LanguageFile VARCHAR(128),
	i_ForumEmail	VARCHAR(128),
	i_SmtpServer	VARCHAR(128),
	i_User		VARCHAR(128),
	i_UserEmail	VARCHAR(255),
	i_UserKey	CHAR(36),
	i_RolePrefix VARCHAR(128),
	i_UTCTIMESTAMP DATETIME
	
 ) BEGIN
	DECLARE ici_tmpValue VARCHAR(128);
 
	
	SET ici_tmpValue = CAST(i_TimeZone AS CHAR(100));
	CALL {databaseSchema}.{objectQualifier}registry_save ('TimeZone', ici_tmpValue,null);
	CALL {databaseSchema}.{objectQualifier}registry_save ('SmtpServer', i_SmtpServer,null);
	CALL {databaseSchema}.{objectQualifier}registry_save ('ForumEmail', i_ForumEmail,null);
	
	CALL {databaseSchema}.{objectQualifier}registry_save('culture',i_Culture,null);
	CALL {databaseSchema}.{objectQualifier}registry_save('language',i_LanguageFile,null);
		
	 /*initalize new board*/
	CALL {databaseSchema}.{objectQualifier}board_create (i_Name, i_Culture, i_LanguageFile, null,null,i_User,i_UserEmail,i_UserKey,1,i_RolePrefix,i_UTCTIMESTAMP);
	 /*initalize required 'registry' settings*/
	CALL {databaseSchema}.{objectQualifier}registry_save ('version','1',null);
	CALL {databaseSchema}.{objectQualifier}registry_save ('versionName','1.0.0',null);
 END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}system_updateversion
(
	i_Version		INT,
	i_VersionName	VARCHAR(128)
) 

BEGIN

	DECLARE ici_tmpValue VARCHAR (100);
	SET ici_tmpValue= CAST(i_Version AS CHAR(100));
	CALL {databaseSchema}.{objectQualifier}registry_save ('Version',ici_tmpValue,null);
	CALL {databaseSchema}.{objectQualifier}registry_save ('VersionName',i_VersionName,null);

END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM returns a prepared result with a first select postion */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_active
(
	i_BoardID INT,
	i_CategoryID INT,
	i_PageUserID INT,
	i_SinceDate DATETIME,
	i_ToDate DATETIME, 	
	i_PageIndex INT, 	
	i_PageSize INT,
	i_StyledNicks TINYINT(1),
	i_FindLastUnread TINYINT(1),
	i_UTCTIMESTAMP DATETIME 	
)	
BEGIN

 DECLARE  ici_post_totalrowsnumber INT; 
 DECLARE  ici_firstselectrownum INT; 
 DECLARE  ici_firstselectposted DATETIME; 

 SET i_PageIndex = i_PageIndex + 1;
	/* how set nocount on */	 

		
	-- find total returned count
			select
		COUNT(1)
		INTO ici_post_totalrowsnumber
		FROM
		{databaseSchema}.{objectQualifier}Topic c
		JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID=c.UserID
		JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category e ON e.CategoryID=d.CategoryID
	WHERE
		(c.LastPosted BETWEEN i_SinceDate and i_ToDate) AND 	
		x.UserID = i_PageUserID and
		x.ReadAccess <> 0 
		 AND e.BoardID = i_BoardID AND
	(i_CategoryID IS NULL OR e.CategoryID=i_CategoryID) AND    
	-- is deleted
	c.Flags & 8 <> 8 AND
	c.TopicMovedID is null ;

	
	  select (i_PageIndex-1) * i_PageSize + 1 INTO ici_firstselectrownum;

	 

	 if (ici_firstselectrownum > 0)
	 then
	 select ici_firstselectrownum 
	 into ici_firstselectrownum;
	 end  if;
	
	set @i_FirstSelectPosted = null;
	set @i_FirstSelectEdited = null;
set @tlist2_rec = CONCAT('select
c.LastPosted,
c.Posted
INTO @i_FirstSelectLastPosted,
@i_FirstSelectPosted
	FROM
		{databaseSchema}.{objectQualifier}Topic c
		JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID=c.UserID
		JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category e ON e.CategoryID=d.CategoryID
	WHERE
		(c.LastPosted BETWEEN ''',i_SinceDate,''' and ''',i_ToDate,''') AND 	
		x.UserID = ',i_PageUserID,' and
		x.ReadAccess <> 0 
		 AND e.BoardID = ',i_BoardID,' AND
	(',COALESCE(i_CategoryID,0),' = 0 OR e.CategoryID= ',COALESCE(i_CategoryID,0),') AND    
	-- is deleted
	(c.Flags & 8) <> 8 AND
	c.TopicMovedID is null 
	ORDER BY
	c.LastPosted desc,
	e.SortOrder asc,
	d.SortOrder asc,
	d.Name DESC,
	c.Priority DESC  LIMIT 1 OFFSET ',ici_firstselectrownum - 1,';');  
	PREPARE tlist2 FROM @tlist2_rec;
	EXECUTE tlist2;		
	DEALLOCATE PREPARE tlist2;
	
	SET  @talist1_rec =
	CONCAT('select
		c.ForumID,
		c.TopicID,
		c.TopicMovedID,
		c.`Status`,
		c.Styles,
		c.Posted,
		IFNULL(c.TopicMovedID,c.TopicID) AS LinkTopicID,
		c.Topic AS Subject,
		c.Description,
		c.UserID,
		IFNULL(c.UserName,b.Name) AS Starter,
		IFNULL(c.UserDisplayName,b.DisplayName) AS StarterDisplay,
		(SELECT COUNT(1) 
					  FROM {databaseSchema}.{objectQualifier}Message mes 
					  WHERE mes.TopicID = c.TopicID 
					  -- deleted
						AND (mes.Flags & 8) = 8
					  -- approved
						AND (mes.Flags & 16) = 16 
						AND ((',COALESCE(i_PageUserID,0),' <> 0 AND mes.UserID = ',i_PageUserID,') 
						OR (',COALESCE(i_PageUserID,0),' = 0)) ) AS NumPostsDeleted,
		((SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x WHERE x.TopicID=c.TopicID and (x.Flags & 8)=0) - 1)
				 AS Replies,
		c.Views AS Views,
		c.LastPosted AS LastPosted ,
		c.LastUserID AS LastUserID,
		IFNULL(c.LastUserName,(SELECT `Name` FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserName,
		IFNULL(c.LastUserDisplayName,(SELECT `DisplayName` FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserDisplayName,
		c.LastMessageID AS LastMessageID,
		c.LastMessageFlags,
		c.TopicID AS LastTopicID,
		c.Flags AS TopicFlags,
		(SELECT COUNT(ID) FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE TopicID = IFNULL(c.TopicMovedID,c.TopicID)) as FavoriteCount,
		c.Priority,
		c.PollID,
		d.Name AS ForumName,
		c.TopicMovedID,
		d.Flags AS ForumFlags, 
		(SELECT CAST(`Message` AS CHAR(1000)) 
		FROM {databaseSchema}.{objectQualifier}Message mes2 
		where mes2.TopicID = IFNULL(c.TopicMovedID,c.TopicID) 
		AND mes2.Position = 0) AS FirstMessage,
		 (case(',i_StyledNicks,')
			when 1 then  b.UserStyle 
			else ''''	 end) AS  StarterStyle,
		(case(',i_StyledNicks,')
			when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=c.LastUserID)  
			else ''''	 end ) AS LastUserStyle,
		(case(',i_FindLastUnread,')
			 when 1 then
			   (SELECT CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=d.ForumID AND x.UserID = ',i_PageUserID,' limit 1)
			 else CAST(NULL AS DATETIME)	 end) AS LastForumAccess,
		(case(',i_FindLastUnread,')
			 when 1 then
			   (SELECT CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = ',i_PageUserID,' limit 1)
			 else CAST(NULL AS DATETIME)	 end) AS  LastTopicAccess,
			 (SELECT GROUP_CONCAT(tg.tag separator '','') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = c.TopicID) AS TopicTags			 , 
			 c.TopicImage,
			 c.TopicImageType,
			 c.TopicImageBin,
			 0 as HasAttachments, 
						 {databaseSchema}.{objectQualifier}biginttoint(',ici_post_totalrowsnumber,') AS TotalRows,
			{databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex 
	FROM
		{databaseSchema}.{objectQualifier}Topic c
		JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID=c.UserID
		JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category e ON e.CategoryID=d.CategoryID
	WHERE
		c.LastPosted <= ''',COALESCE(@i_FirstSelectLastPosted,UTC_TIMESTAMP()),''' and
		x.UserID = ',i_PageUserID,' and
		x.ReadAccess <> 0 
		 AND e.BoardID = ',i_BoardID,' AND
	 (',COALESCE(i_CategoryID,0),' = 0 OR e.CategoryID= ',COALESCE(i_CategoryID,0),') AND    
	-- is deleted
	(c.Flags & 8) <> 8 AND
	c.TopicMovedID is null 
	ORDER BY
	c.LastPosted desc,
	e.SortOrder asc,
	d.SortOrder asc,
	d.Name DESC,
	c.Priority DESC  LIMIT ',i_PageSize,'  ;'); 
	PREPARE talist1 FROM @talist1_rec;
	EXECUTE talist1;
	DEALLOCATE PREPARE talist1;

END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM returns a prepared result with a first select postion */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_unread
(
	i_BoardID INT,
	i_CategoryID INT,
	i_PageUserID INT,
	i_SinceDate DATETIME,
	i_ToDate DATETIME,
	i_PageIndex INT, 	
	i_PageSize INT,
 i_StyledNicks TINYINT(1),
 i_FindLastRead TINYINT(1) 
)
BEGIN
 DECLARE  cnt INT DEFAULT 1; 
 DECLARE  ici_shiftsticky INT DEFAULT 0;
 DECLARE  ici_post_totalrowsnumber INT; 
 DECLARE  ici_firstselectrownum INT;  
 DECLARE  ici_firstselectposted DATETIME; 


 SET i_PageIndex = i_PageIndex + 1;
	/* how set nocount on */	 

		
	-- find total returned count
			select
		COUNT(c.TopicID)
		INTO ici_post_totalrowsnumber
		FROM
		{databaseSchema}.{objectQualifier}Topic c
		JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID=c.UserID
		JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category e ON e.CategoryID=d.CategoryID
	WHERE
		(c.LastPosted BETWEEN i_SinceDate and i_ToDate) AND 	
		x.UserID = i_PageUserID and
		x.ReadAccess <> 0 
		 AND e.BoardID = i_BoardID AND
	(i_CategoryID IS NULL OR e.CategoryID=i_CategoryID) AND    
	-- is deleted
	IFNULL(SIGN(c.Flags & 8),0) = 0 AND
	c.TopicMovedID is null ;

	
	  select (i_PageIndex-1) * i_PageSize + 1 INTO ici_firstselectrownum;

	 

	 if (ici_firstselectrownum > 0)
	 then
	 select ici_firstselectrownum 
	 into ici_firstselectrownum;
	 end  if;
	

SET @tlist2_res = CONCAT('select
c.LastPosted,
c.Posted
INTO @i_FirstSelectLastPosted,@i_FirstSelectPosted
	FROM
		{databaseSchema}.{objectQualifier}Topic c
		JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID=c.UserID
		JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category e ON e.CategoryID=d.CategoryID
	WHERE
		(c.LastPosted BETWEEN ''',i_SinceDate,''' and ''',i_ToDate,''') AND 	
		x.UserID = ',i_PageUserID,' and
		x.ReadAccess <> 0 
		 AND e.BoardID = ',i_BoardID,' AND
	(',COALESCE(i_CategoryID,0),' = 0 OR e.CategoryID= ',COALESCE(i_CategoryID,0),') AND    
	-- is deleted
	IFNULL(SIGN(c.Flags & 8),0) = 0 AND
	c.TopicMovedID is null 
	ORDER BY
	c.LastPosted,
	e.SortOrder asc,
	d.SortOrder asc,
	d.Name DESC,
	c.Priority DESC  LIMIT 1 OFFSET ',ici_firstselectrownum - 1,';');
	  
	PREPARE tlist2 FROM @tlist2_res;	
	EXECUTE tlist2;
	DEALLOCATE PREPARE tlist2;		 		
			
	SET @tlist1_res =
	CONCAT('select
		c.ForumID,
		c.TopicID,
		c.TopicMovedID,
		c.`Status`,
		c.Styles,
		c.Posted,
		IFNULL(c.TopicMovedID,c.TopicID) AS LinkTopicID,
		c.Topic AS Subject,
		c.Description,
		c.UserID,
		IFNULL(c.UserName,b.Name) AS Starter,
		IFNULL(c.UserDisplayName,b.DisplayName) AS StarterDisplay,
		(SELECT COUNT(1) 
					  FROM {databaseSchema}.{objectQualifier}Message mes 
					  WHERE mes.TopicID = c.TopicID 
					  -- deleted
						AND (mes.Flags & 8) = 8
					  -- approved
						AND (mes.Flags & 16) = 16 
						AND ((',i_PageUserID,' IS NOT NULL AND mes.UserID = ',i_PageUserID,') 
						OR (',i_PageUserID,' IS NULL)) ) AS NumPostsDeleted,
		((SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x WHERE x.TopicID=c.TopicID and (x.Flags & 8)=0) - 1)
				 AS Replies,
		c.Views AS Views,
		c.LastPosted AS LastPosted,
		c.LastUserID AS LastUserID,
		IFNULL(c.LastUserName,(SELECT `Name` FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserName,
		IFNULL(c.LastUserDisplayName,(SELECT `DisplayName` FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserDisplayName,
		c.LastMessageID AS LastMessageID,
		c.LastMessageFlags,
		c.TopicID AS LastTopicID,
		c.Flags AS TopicFlags,
		(SELECT COUNT(ID) FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE TopicID = IFNULL(c.TopicMovedID,c.TopicID)) as FavoriteCount,
		c.Priority,
		c.PollID,
		d.Name AS ForumName,
		c.TopicMovedID,
		d.Flags AS ForumFlags, 
		(SELECT CAST(`Message` AS CHAR(1000)) 
		FROM {databaseSchema}.{objectQualifier}Message mes2 
		where mes2.TopicID = IFNULL(c.TopicMovedID,c.TopicID) 
		AND mes2.Position = 0) AS FirstMessage,
		 (case(',i_StyledNicks,')
			when 1 then  b.UserStyle  
			else ''''	 end) AS  StarterStyle,
		(case(',i_StyledNicks,')
			when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=c.LastUserID)  
			else ''''	 end ) AS LastUserStyle,
		(case(',i_FindLastRead,')
			 when 1 then
			   (SELECT CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=d.ForumID AND x.UserID = ',i_PageUserID,' limit 1)
			 else CAST(NULL AS DATETIME)	 end) AS LastForumAccess,
		(case(',i_FindLastRead,')
			 when 1 then
			   (SELECT CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = ',i_PageUserID,' limit 1)
			 else CAST(NULL AS DATETIME)	 end) AS  LastTopicAccess,	
			 (SELECT GROUP_CONCAT(tg.tag separator '','') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = c.TopicID) AS TopicTags, 
			  c.TopicImage,
		 c.TopicImageType,
			 c.TopicImageBin,
			 0 as HasAttachments, 
			 {databaseSchema}.{objectQualifier}biginttoint(',ici_post_totalrowsnumber,') AS TotalRows,
			 {databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex 
	FROM
		{databaseSchema}.{objectQualifier}Topic c
		JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID=c.UserID
		JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		JOIN {databaseSchema}.{objectQualifier}Category e ON e.CategoryID=d.CategoryID
	WHERE
		c.LastPosted <= ''',COALESCE(@i_FirstSelectLastPosted,UTC_TIMESTAMP()),''' and	
		x.UserID = ',i_PageUserID,' and
		x.ReadAccess <> 0 
		 AND e.BoardID = ',i_BoardID,' AND
   (',COALESCE(i_CategoryID,0),' = 0 OR e.CategoryID= ',COALESCE(i_CategoryID,0),') AND    
	-- is deleted
	IFNULL(SIGN(c.Flags & 8),0) = 0 AND
	c.TopicMovedID is null 
	ORDER BY
	c.LastPosted,
	e.SortOrder asc,
	d.SortOrder asc,
	d.Name DESC,
	c.Priority DESC  LIMIT ',i_PageSize,'  ;'); 

	PREPARE tlist1 FROM @tlist1_res;
	EXECUTE tlist1;
	DEALLOCATE PREPARE tlist1;	

END;
--GO

	/* STORED PROCEDURE CREATED BY VZ-TEAM topic_announcements */
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_create_by_message (
	i_MessageID      INT,
	i_ForumID	INT,
	i_Subject	VARCHAR(128),
	i_UTCTIMESTAMP DATETIME
	)
	BEGIN


	DECLARE		ici_UserID		INT;
	DECLARE		ici_Posted		DATETIME;
	DECLARE i_TopicID INT;

   SELECT UserID,Posted INTO ici_UserID,ici_Posted  from {databaseSchema}.{objectQualifier}message WHERE MessageID =  i_MessageID;


	/*declare i_MessageID int*/

	IF ici_Posted IS NULL THEN SET ici_Posted = i_UTCTIMESTAMP; END IF;

	INSERT INTO {databaseSchema}.{objectQualifier}Topic(ForumID,Topic,UserID,Posted,Views,Priority,PollID,UserName,NumPosts)
	VALUES(i_ForumID,i_Subject,ici_UserID,ici_Posted,0,0,null,null,0);

	SET i_TopicID = LAST_INSERT_ID();
  /*CALL {databaseSchema}.{objectQualifier}message_save (i_TopicID,ici_UserID,i_Message,i_MessageDescription,i_UserName,i_IP,ici_Posted,null,null,i_Flags, i_MessageID);*/
	SELECT i_TopicID AS TopicID,i_MessageID AS MessageID;
	END;

--GO
	/* STORED PROCEDURE CREATED BY VZ-TEAM */    
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_delete (i_TopicID int,i_TopicMovedID INT,i_EraseTopic TINYINT(1),i_UpdateLastPost TINYINT(1))
	BEGIN
	/*SET NOCOUNT ON*/
	DECLARE ici_ForumID INT;
	DECLARE ici_ForumID2 INT;
	DECLARE ici_pollID INT;
	DECLARE ici_Deleted INT;	
		
	IF (i_TopicMovedID = i_TopicID) THEN
	-- delete link only	   
	DELETE FROM {databaseSchema}.{objectQualifier}Topic where TopicMovedID = i_TopicID;	
	ELSE

	SELECT ForumID INTO ici_ForumID FROM  {databaseSchema}.{objectQualifier}Topic  WHERE TopicID=i_TopicID;
	UPDATE  {databaseSchema}.{objectQualifier}Topic SET LastMessageID = NULL 
	WHERE TopicID = i_TopicID; 

	UPDATE  {databaseSchema}.{objectQualifier}Forum SET
	LastTopicID = NULL,
	LastMessageID = NULL,
	LastUserID = NULL,
	LastUserName = NULL,
	LastUserDisplayName = NULL,
	LastPosted = NULL
	WHERE LastMessageID IN (SELECT MessageID from  {databaseSchema}.{objectQualifier}Message 
	where TopicID = i_TopicID); 
	
	  
	UPDATE  {databaseSchema}.{objectQualifier}Active SET TopicID = NULL WHERE TopicID = i_TopicID;

	/*delete messages and topics*/
   
	UPDATE {databaseSchema}.{objectQualifier}Topic SET PollID = NULL WHERE TopicID = i_TopicID AND TopicMovedID IS NOT NULL;
	

	IF i_EraseTopic = 0 THEN
	UPDATE  {databaseSchema}.{objectQualifier}Topic SET `Flags` = `Flags` | 8 WHERE TopicID = i_TopicID OR TopicMovedID = i_TopicID;
	UPDATE  {databaseSchema}.{objectQualifier}Message SET `Flags` = `Flags` | 8 WHERE TopicID = i_TopicID;
	ELSE
			DELETE FROM  {databaseSchema}.{objectQualifier}NntpTopic WHERE TopicID = i_TopicID;
		 -- remove polls	
			SELECT  pollID INTO ici_pollID FROM  {databaseSchema}.{objectQualifier}Topic WHERE TopicID = i_TopicID;
	IF ici_pollID IS NOT NULL THEN
	UPDATE  {databaseSchema}.{objectQualifier}Topic set PollID = NULL where TopicID = i_TopicID;
	CALL {databaseSchema}.{objectQualifier}pollgroup_remove(ici_pollID, i_TopicID, null, null, null, 0, 0); 
	END IF; 

	-- remove tags references
	UPDATE {databaseSchema}.{objectQualifier}Tags set TagCount = TagCount - 1 where TagID in (select TagID from {databaseSchema}.{objectQualifier}TopicTags  WHERE TopicID = i_TopicID);
	DELETE FROM {databaseSchema}.{objectQualifier}TopicTags  where TopicID = i_TopicID = i_TopicID; 
		  
	DELETE FROM  {databaseSchema}.{objectQualifier}Topic WHERE TopicMovedID = i_TopicID;	
	
	DELETE FROM  {databaseSchema}.{objectQualifier}Attachment
	WHERE MessageID IN
	(SELECT MessageID FROM  {databaseSchema}.{objectQualifier}Message WHERE TopicID = i_TopicID);
	DELETE FROM {databaseSchema}.{objectQualifier}MessageHistory where MessageID IN (select MessageID from  {databaseSchema}.{objectQualifier}message where TopicID = i_TopicID);
	DELETE FROM {databaseSchema}.{objectQualifier}Message WHERE TopicID = i_TopicID;
	DELETE FROM {databaseSchema}.{objectQualifier}WatchTopic WHERE TopicID = i_TopicID;    
	DELETE FROM {databaseSchema}.{objectQualifier}TopicReadTracking WHERE TopicID = i_TopicID; 
	DELETE FROM {databaseSchema}.{objectQualifier}Topic WHERE  TopicMovedID = i_TopicID;
	DELETE FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicID = i_TopicID; 
	DELETE FROM {databaseSchema}.{objectQualifier}MessageReportedAudit where MessageID IN (select MessageID from  {databaseSchema}.{objectQualifier}message where TopicID = i_TopicID); 
	DELETE FROM {databaseSchema}.{objectQualifier}MessageReported where MessageID IN (select MessageID from  {databaseSchema}.{objectQualifier}message where TopicID = i_TopicID);	
	DELETE FROM {databaseSchema}.{objectQualifier}FavoriteTopic  WHERE TopicID = i_TopicID;

	END IF;

	/*commit*/
	IF i_UpdateLastPost<>0 THEN
		CALL  {databaseSchema}.{objectQualifier}forum_updatelastpost (ici_ForumID);
		END IF;
	
	IF ici_ForumID IS NOT NULL THEN 		
		CALL  {databaseSchema}.{objectQualifier}forum_updatestats(ici_ForumID); 
		END IF;
END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_findnext(i_TopicID INT) 
BEGIN
	DECLARE ici_LastPosted DATETIME;
	DECLARE ici_ForumID INT;
	SELECT LastPosted,ForumID INTO ici_LastPosted, ici_ForumID  FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicID = i_TopicID AND TopicMovedID IS NULL;
	SELECT TopicID FROM {databaseSchema}.{objectQualifier}Topic 
	WHERE UNIX_TIMESTAMP(LastPosted)>UNIX_TIMESTAMP(ici_LastPosted) 
	AND ForumID = ici_ForumID AND (Flags & 8) = 0 
	AND TopicMovedID IS NULL
	ORDER BY LastPosted ASC LIMIT 1;
END;
--GO

create procedure {databaseSchema}.{objectQualifier}pollgroup_remove(i_PollGroupID int, i_TopicID int, i_ForumID int, i_CategoryID int, i_BoardID int, i_RemoveCompletely TINYINT(1), i_RemoveEverywhere TINYINT(1))
  begin   
	declare ici_tmp int;
	
	
			 -- we delete poll from the place only it persists in other places 
		 if i_RemoveEverywhere <> 1
			 then
				   if i_TopicID > 0 then
				   Update {databaseSchema}.{objectQualifier}Topic set PollID = NULL where TopicID = i_TopicID;                 
				   end if; 
				   if i_ForumID > 0 then
				   Update {databaseSchema}.{objectQualifier}Forum set PollGroupID = NULL where ForumID = i_ForumID;
				   end if; 
				   if i_CategoryID > 0 then
				   Update {databaseSchema}.{objectQualifier}Category set PollGroupID = NULL where CategoryID = i_CategoryID;
				   end if;
			 end if;        
			
		  -- we remove poll group links from all places where they are
		 if (i_RemoveEverywhere = 1 OR i_RemoveCompletely = 1)
		 then
				   Update {databaseSchema}.{objectQualifier}Topic set PollID = NULL where PollID = i_PollGroupID;
				   Update {databaseSchema}.{objectQualifier}Forum set PollGroupID = NULL where PollGroupID = i_PollGroupID;
				   Update {databaseSchema}.{objectQualifier}Category set PollGroupID = NULL where PollGroupID = i_PollGroupID;				 
		 end if;

		 -- simply remove all polls
	if i_RemoveCompletely = 1 
	then	   
			DELETE FROM  {databaseSchema}.{objectQualifier}pollvote WHERE PollID IN (select PollID from {databaseSchema}.{objectQualifier}Poll where PollGroupID = i_PollGroupID);
			DELETE FROM  {databaseSchema}.{objectQualifier}choice WHERE PollID IN (select PollID from {databaseSchema}.{objectQualifier}Poll where PollGroupID = i_PollGroupID);	
			DELETE FROM  {databaseSchema}.{objectQualifier}poll WHERE PollGroupID = i_PollGroupID; 
			DELETE FROM  {databaseSchema}.{objectQualifier}PollGroupCluster WHERE PollGroupID = i_PollGroupID;		
	end  if;

	-- don't remove cluster if the polls are not removed from db 
	end;
--GO

create procedure {databaseSchema}.{objectQualifier}pollgroup_attach(i_PollGroupID int, i_TopicID int, i_ForumID int, i_CategoryID int, i_BoardID int) 
begin
				   -- this deletes possible polls without choices it should not normally happen
							 
				--  DELETE FROM {databaseSchema}.{objectQualifier}PollVote WHERE PollID IN (SELECT PollID FROM {databaseSchema}.{objectQualifier}Poll WHERE PollGroupID = NULL);
				--  DELETE FROM {databaseSchema}.{objectQualifier}Choice WHERE PollID IN (SELECT PollID FROM {databaseSchema}.{objectQualifier}Poll WHERE PollGroupID = NULL);
				--  DELETE FROM {databaseSchema}.{objectQualifier}Poll WHERE PollID IN (SELECT PollID FROM {databaseSchema}.{objectQualifier}Poll WHERE PollGroupID = NULL);
								   
				   if NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}Poll WHERE PollGroupID IS NULL LIMIT 1)
				   then
				   if i_TopicID > 0
				   then
				   if exists (select 1 from {databaseSchema}.{objectQualifier}Topic where TopicID = i_TopicID  and PollID is not null)
				   then
				   SELECT 1;				  
				   else				  
				   Update {databaseSchema}.{objectQualifier}Topic set PollID = i_PollGroupID where TopicID = i_TopicID; 
				   SELECT 10;
				   end if;
				   end if;            
				  
				   if i_ForumID > 0
				   then
				   if exists (select 1 from {databaseSchema}.{objectQualifier}Forum where ForumID = i_ForumID and PollGroupID is not null)
				   then
				   SELECT 1;				  
				   else				  
				   Update {databaseSchema}.{objectQualifier}Forum set PollGroupID = i_PollGroupID where ForumID = i_ForumID;
				   SELECT 0;
				   end if;
				   end if;

				   if i_CategoryID > 0
				   then
				   if exists (select 1 from {databaseSchema}.{objectQualifier}Category where CategoryID = i_CategoryID and PollGroupID is null)
				   then
				   SELECT 1;				   
				   else				   
				   Update {databaseSchema}.{objectQualifier}Category set PollGroupID = i_PollGroupID where CategoryID = i_CategoryID;
				   SELECT 0;
				   end if;
				   end if;
				   end if;
				   SELECT 1;	               

end;
--GO

create procedure {databaseSchema}.{objectQualifier}pollgroup_list(i_UserID int, i_ForumID int, i_BoardID int)
begin
	select 
	distinct(p.Question),
			p.PollGroupID 
	from {databaseSchema}.{objectQualifier}Poll p
	LEFT JOIN {databaseSchema}.{objectQualifier}PollGroupCluster pgc 
	ON pgc.PollGroupID = p.PollGroupID
	WHERE p.PollGroupID is not null
	-- WHERE p.Closes IS NULL OR p.Closes > UTC_TIMESTAMP()
	order by Question asc;
end;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_findprev(i_TopicID INT)  
 BEGIN
	DECLARE ici_LastPosted DATETIME;
	DECLARE ici_ForumID INT;
	SELECT LastPosted,ForumID INTO ici_LastPosted,ici_ForumID 
		  FROM {databaseSchema}.{objectQualifier}Topic 
			WHERE TopicID = i_TopicID AND TopicMovedID IS NULL;
	SELECT TopicID from {databaseSchema}.{objectQualifier}Topic 
	WHERE UNIX_TIMESTAMP(LastPosted)<UNIX_TIMESTAMP(ici_LastPosted) 
	AND ForumID = ici_ForumID AND (Flags & 8) = 0 
	AND TopicMovedID IS NULL
	ORDER BY LastPosted DESC LIMIT 1;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_info
 (
	i_TopicID INT,	
	i_ShowDeleted TINYINT(1),
	i_GetTags TINYINT(1)
 )
 BEGIN  
	if i_TopicID = 0 then set i_TopicID = NULL; end if;
	IF i_TopicID IS NULL THEN    
	  IF i_ShowDeleted = 1 THEN
		SELECT t.*,(CASE WHEN i_GetTags = 1 THEN (SELECT GROUP_CONCAT(tg.tag separator ',') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = t.TopicID) ELSE '' END) AS TopicTags,
		(SELECT `Message` FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(t.TopicMovedID,t.TopicID) AND mes2.Position = 0) AS FirstMessage
		FROM {databaseSchema}.{objectQualifier}Topic t ;
		ELSE
		SELECT t.*,(CASE WHEN i_GetTags = 1 THEN (SELECT GROUP_CONCAT(tg.tag separator ',') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = t.TopicID) ELSE '' END) AS TopicTags,
		(SELECT `Message` FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(t.TopicMovedID,t.TopicID) AND mes2.Position = 0) AS FirstMessage
		FROM {databaseSchema}.{objectQualifier}Topic t WHERE (Flags & 8) = 0;
	  END IF;
	ELSE 	
		IF i_ShowDeleted = 1 THEN
			SELECT t.*,(CASE WHEN i_GetTags = 1 THEN (SELECT GROUP_CONCAT(tg.tag separator ',') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = t.TopicID) ELSE '' END) AS TopicTags,
		(SELECT `Message` FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(t.TopicMovedID,t.TopicID) AND mes2.Position = 0) AS FirstMessage
			FROM {databaseSchema}.{objectQualifier}Topic t WHERE TopicID = i_TopicID;
		ELSE
			SELECT t.*,(CASE WHEN i_GetTags = 1 THEN (SELECT GROUP_CONCAT(tg.tag separator ',') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = t.TopicID) ELSE '' END) AS TopicTags,
		(SELECT `Message` FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(t.TopicMovedID,t.TopicID) AND mes2.Position = 0) AS FirstMessage 
			FROM {databaseSchema}.{objectQualifier}Topic t WHERE TopicID = i_TopicID AND (Flags & 8) = 0;		
		END IF;
   END IF; 
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_findduplicate
(
	i_TopicName VARCHAR(255)
)
BEGIN
	IF i_TopicName IS NOT NULL
	THEN	
		IF EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}Topic WHERE Topic LIKE  i_TopicName AND TopicMovedID IS NULL LIMIT 1)
		THEN
		SELECT 1;
		ELSE
		SELECT 0;
	END IF;
	ELSE	
		SELECT 0;
	END	IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM returns a prepared result with a first select postion */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}announcements_list
(
	i_ForumID INT,
	i_UserID INT,
	i_SinceDate DATETIME,
	i_ToDate DATETIME,
	i_PageIndex INT,
	i_PageSize INT,
	i_StyledNicks TINYINT(1),
	i_ShowMoved TINYINT(1),
	i_ShowDeleted TINYINT(1),
	i_FindLastRead TINYINT(1),
	i_GetTags TINYINT(1),
	i_UTCTIMESTAMP DATETIME
)
BEGIN
 DECLARE  ici_RowTotalCount INT DEFAULT 0; 
 DECLARE  cnt INT DEFAULT 1; 
 DECLARE  ici_shiftsticky INT DEFAULT 0;
 DECLARE  ici_post_totalrowsnumber INT DEFAULT 0; 
 DECLARE  ici_post_priorityrowsnumber_pages INT;
 DECLARE  ici_post_priorityrowsnumber INT;
 DECLARE  ici_post_priorityrowsnumber_shift INT;
 DECLARE  ici_firstselectrownum INT DEFAULT 0; 
 DECLARE  ici_sortsincelatest INT;  
 DECLARE  ici_firstselectposted DATETIME; 
 DECLARE  ici_ceiling decimal;
 DECLARE  ici_retcount INT; 
 DECLARE  ici_counter INT; 

 SET i_PageIndex = i_PageIndex + 1;
	/* how set nocount on */	 

		
	-- find total returned count
			select
		COUNT(c.TopicID)
		INTO ici_post_priorityrowsnumber
	FROM {databaseSchema}.{objectQualifier}Topic c
	WHERE c.ForumID = i_ForumID		
		AND	(c.Priority=2)	
		AND	(i_ShowDeleted = 1 or (c.Flags & 8) <> 8)
		AND	(c.TopicMovedID IS NOT NULL OR c.NumPosts > 0) 
		AND
		((i_ShowMoved = 1)
		or
		(i_ShowMoved <> 1 AND  c.TopicMovedID IS NULL));

	
	  select (i_PageIndex-1) * i_PageSize + 1 INTO ici_firstselectrownum;

	 

	 if (ici_firstselectrownum > 0)
	 then
	 select ici_firstselectrownum 
	 into ici_firstselectrownum;
	 end  if;
	
	 SET @i_FirstSelectLastPosted = NULL;


SET @talist2_str = CONCAT('select
t.LastPosted INTO @i_FirstSelectLastPosted
	from
		{databaseSchema}.{objectQualifier}Topic t 
	where
		t.ForumID = ',i_ForumID,'	    
		AND	(t.Priority=2)
		 AND (',i_ShowDeleted,' = 1 or (t.Flags & 8) <> 8)
		AND	(t.TopicMovedID IS NOT NULL OR t.NumPosts > 0) 
		AND
		((',i_ShowMoved,' = 1)
		or
		(',i_ShowMoved,' <> 1 AND  t.TopicMovedID IS NULL))		
	order by
		t.Priority DESC, t.LastPosted DESC LIMIT 1 OFFSET ',ici_firstselectrownum - 1,';');  

		PREPARE talist2 FROM @talist2_str;
		EXECUTE talist2;	 
		DEALLOCATE PREPARE talist2;	

		SET @talist1_str =
	CONCAT('select
			c.ForumID,
			c.TopicID,
			c.Posted,
			IFNULL(c.TopicMovedID,c.TopicID) AS LinkTopicID,
			c.TopicMovedID,
			(SELECT COUNT(1) as FavoriteCount FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE TopicId = IFNULL(c.TopicMovedID,c.TopicID)) AS FavoriteCount,
			c.Topic as Subject,
			c.Description,
			c.Status,
			c.Styles,
			c.UserID,
			IFNULL(c.UserName,b.Name) AS Starter,
			IFNULL(c.UserDisplayName,b.DisplayName) AS StarterDisplay,
			(c.NumPosts - 1) AS Replies,
			(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message mes WHERE mes.TopicID = c.TopicID AND (mes.Flags & 8) = 8 AND (mes.Flags & 16) = 16 AND ((',COALESCE(i_UserID,0),' <> 0 AND mes.UserID = ',COALESCE(i_UserID,0),') OR (',COALESCE(i_UserID,0),' = 0)) ) AS NumPostsDeleted, 			
			c.Views, 
			c.LastPosted,
			c.LastUserID,
			IFNULL(c.LastUserName,(SELECT x.Name FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserName,
			IFNULL(c.LastUserDisplayName,(SELECT x.DisplayName FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserDisplayName,
			c.LastMessageID,
			c.TopicID AS LastTopicID,
			c.Flags AS TopicFlags,
			c.Priority,
			c.PollID,
			d.Flags AS ForumFlags,
			 (SELECT CAST(Message as char(1000)) FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(c.TopicMovedID,c.TopicID) AND mes2.Position = 0 LIMIT 1) AS FirstMessage,
			(case(',i_StyledNicks,')
			when 1 then  b.UserStyle
			else ''''	 end) AS StarterStyle,
			(case(',i_StyledNicks,')
			when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=c.LastUserID)  
			else ''''	 end) AS LastUserStyle,
			(case(',i_FindLastRead,')
			 when 1 then
			   (SELECT  CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=c.ForumID AND x.UserID = c.UserID LIMIT 1)
			 else CAST(NULL AS DATETIME)	 end ) AS LastForumAccess,
			(case(',i_FindLastRead,')
			 when 1 then
			   (SELECT CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = c.UserID LIMIT 1)
			 else CAST(NULL AS DATETIME)	 end) AS LastTopicAccess,
			 (case(',i_GetTags,')
			 when 1 then
			 (SELECT GROUP_CONCAT(tg.tag separator '','') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = c.TopicID) 
			 else ''''	 end)  AS TopicTags, 
			 c.TopicImage,
			 c.TopicImageType,
			 c.TopicImageBin,
			{databaseSchema}.{objectQualifier}biginttoint(',ici_post_priorityrowsnumber,') AS TotalRows,
			{databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex
	from	
		{databaseSchema}.{objectQualifier}Topic c
		JOIN {databaseSchema}.{objectQualifier}User b 
		ON b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID	
		WHERE c.ForumID = ',i_ForumID,'
		AND	(Priority=2) AND c.LastPosted <= ''',COALESCE(@i_FirstSelectLastPosted,UTC_TIMESTAMP()),'''
		 AND (',i_ShowDeleted,' = 1 or (c.Flags & 8) <> 8)
		AND	((c.TopicMovedID IS NOT NULL) OR (c.NumPosts > 0)) 
		AND
		((',i_ShowMoved,' = 1)
		or
		(',i_ShowMoved,' <> 1 AND  c.TopicMovedID IS NULL))		
	order by
		 c.Priority DESC,	c.LastPosted DESC LIMIT ',i_PageSize,';'); 
		 PREPARE talist1 FROM  @talist1_str;	
		EXECUTE talist1;		 
		DEALLOCATE PREPARE talist1;
		END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM returns a prepared result with a first select postion */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_list
(
	i_ForumID INT,
	i_UserID INT,
	i_SinceDate DATETIME,
	i_ToDate DATETIME,
	i_PageIndex INT,
	i_PageSize INT,
	i_StyledNicks TINYINT(1),
	i_ShowMoved TINYINT(1),
	i_ShowDeleted TINYINT(1),
	i_FindLastRead TINYINT(1),
	i_GetTags TINYINT(1),
	i_UTCTIMESTAMP DATETIME
)
BEGIN
 DECLARE  ici_RowTotalCount INT DEFAULT 0; 
 DECLARE  cnt INT DEFAULT 1; 
 DECLARE  ici_shiftsticky INT DEFAULT 0;
 DECLARE  ici_post_totalrowsnumber INT DEFAULT 0; 
 DECLARE  ici_post_priorityrowsnumber_pages INT DEFAULT 0;
 DECLARE  ici_post_priorityrowsnumber INT DEFAULT 0;
 DECLARE  ici_post_priorityrowsnumber_shift INT DEFAULT 0;
 DECLARE  ici_firstselectrownum INT DEFAULT 0; 
 DECLARE  ici_sortsincelatest INT DEFAULT 0;  
 DECLARE  ici_firstselectposted DATETIME; 
 DECLARE  ici_ceiling decimal;
 DECLARE  ici_retcount INT DEFAULT 0; 
 DECLARE  ici_counter INT DEFAULT 0; 

 SET i_PageIndex = i_PageIndex + 1;
	/* how set nocount on */
 
	-- find priority returned count

		select
		COUNT(c.TopicID)
		INTO ici_post_priorityrowsnumber
	FROM {databaseSchema}.{objectQualifier}Topic c
	WHERE c.ForumID = i_ForumID		
		AND (c.Priority = 1) 
		  AND (i_ShowDeleted = 1 or (c.Flags & 8) <> 8)
		AND	(c.TopicMovedID IS NOT NULL OR c.NumPosts > 0) 
		AND
		((i_ShowMoved = 1)
		or
		(i_ShowMoved <> 1 AND  c.TopicMovedID IS NULL));
		set ici_post_priorityrowsnumber_pages = CEILING(CAST(ici_post_priorityrowsnumber AS decimal)/i_PageSize); 		 

		
	-- find total returned count
			select
		COUNT(c.TopicID)
		INTO ici_post_totalrowsnumber
	FROM {databaseSchema}.{objectQualifier}Topic c
	WHERE c.ForumID = i_ForumID		
		AND	((Priority>0 AND c.Priority <> 2) 
		OR (c.Priority <= 0 AND c.LastPosted >= i_SinceDate )) 
		 AND (i_ShowDeleted = 1 or (c.Flags & 8) <> 8)
		AND	(c.TopicMovedID IS NOT NULL OR c.NumPosts > 0) 
		AND
		((i_ShowMoved = 1)
		or
		(i_ShowMoved <> 1 AND  c.TopicMovedID IS NULL));

	
	  select (i_PageIndex-1) * i_PageSize + 1  INTO ici_firstselectrownum;

	 
 if (ici_post_priorityrowsnumber_pages < i_PageIndex)
	 then	
	  select ici_firstselectrownum - ici_post_priorityrowsnumber into ici_firstselectrownum;
	 else -- if (@post_priorityrowsnumber_pages >= i_PageIndex)
	 select ici_post_priorityrowsnumber into ici_post_priorityrowsnumber_shift;
	 select 1 into ici_shiftsticky;
	 if (ici_firstselectrownum > 0)
	 then
	 select ici_firstselectrownum 
	 + ici_post_priorityrowsnumber into ici_firstselectrownum;
	 end  if;
	 end  if;	
		
  SET @FirstSelectLastPosted = NULL;
  SET @Shiftsticky	= 0;

 SET @tlist2_str = CONCAT('select
CAST(',ici_shiftsticky,' AS SIGNED),
t.LastPosted INTO @Shiftsticky, @i_FirstSelectLastPosted
	from
		{databaseSchema}.{objectQualifier}Topic t 
	where
		t.ForumID = ',i_ForumID,'	    
	AND	(( (',ici_shiftsticky,' = 1) and (t.Priority > 0 AND t.Priority <> 2)) OR (t.Priority <= 0 AND t.LastPosted >= ''',i_SinceDate,''' )) 
		  AND (',i_ShowDeleted,' = 1 or (t.Flags & 8) <> 8)
		AND	(t.TopicMovedID IS NOT NULL OR t.NumPosts > 0) 
		AND
		((',i_ShowMoved,' = 1)
		or
		(',i_ShowMoved,' <> 1 AND  t.TopicMovedID IS NULL))		
	order by
		t.Priority DESC, t.LastPosted DESC LIMIT 1 OFFSET ',ici_firstselectrownum - 1,';');  
		PREPARE tlist2 from @tlist2_str;
		EXECUTE tlist2;		 
		DEALLOCATE PREPARE tlist2;			
			  
	SET @tlist1_str = CONCAT( 
	'select
			c.ForumID,
			c.TopicID,
			c.Posted,
			IFNULL(c.TopicMovedID,c.TopicID) AS LinkTopicID,
			c.TopicMovedID,
			(SELECT COUNT(1) as FavoriteCount FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE TopicId = IFNULL(c.TopicMovedID,c.TopicID)) AS FavoriteCount,
			c.Topic as Subject,
			c.Description,
			c.Status,
			c.Styles,
			c.UserID,
			IFNULL(c.UserName,b.Name) AS Starter,
			IFNULL(c.UserDisplayName,b.DisplayName) AS StarterDisplay,
			(c.NumPosts - 1) AS Replies,
			(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message mes WHERE mes.TopicID = c.TopicID AND (mes.Flags & 8) = 8 AND (mes.Flags & 16) = 16 AND ((',COALESCE(i_UserID,0),' <> 0 AND mes.UserID = ',COALESCE(i_UserID,0),') 
						OR (',COALESCE(i_UserID,0),' = 0)) ) AS NumPostsDeleted, 			
			 c.Views,
			c.LastPosted,
			c.LastUserID,
			IFNULL(c.LastUserName,(SELECT x.Name FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserName,
			IFNULL(c.LastUserDisplayName,(SELECT x.DisplayName FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserDisplayName,
			c.LastMessageID,
			c.TopicID AS LastTopicID,
			c.LinkDate,
			c.Flags AS TopicFlags,
			c.Priority,
			c.PollID,
			d.Flags AS ForumFlags,
			 (SELECT CAST(Message as char(1000)) FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(c.TopicMovedID,c.TopicID) AND mes2.Position = 0 LIMIT 1) AS FirstMessage,		    
			(case(',i_StyledNicks,')
			when 1 then  b.UserStyle 
			else ''''	 end) AS StarterStyle,
			(case(',i_StyledNicks,')
			when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=c.LastUserID)   
			else '''' end) AS LastUserStyle,			
			(case(',i_FindLastRead,')
			 when 1 then
			   (SELECT  CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=c.ForumID AND x.UserID = c.UserID LIMIT 1)
			 else CAST(NULL AS DATETIME) end ) AS LastForumAccess,
			(case(',i_FindLastRead,')
			 when 1 then
			   (SELECT CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = c.UserID LIMIT 1)
			 else CAST(NULL AS DATETIME) end) AS LastTopicAccess,	
		   (case(',i_GetTags,')
			 when 1 then
			 (SELECT GROUP_CONCAT(tg.tag separator '','') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = c.TopicID) 
			 else ''''	 end)  AS TopicTags, 
			 c.TopicImage,
			 c.TopicImageType,
			 c.TopicImageBin,
			 0 as HasAttachments,
			 {databaseSchema}.{objectQualifier}biginttoint(',ici_post_totalrowsnumber,') AS TotalRows,
			{databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex 
	from	
		{databaseSchema}.{objectQualifier}Topic c
		JOIN {databaseSchema}.{objectQualifier}User b 
		ON b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID	
		WHERE c.ForumID = ',i_ForumID,'
		AND	(( (',@Shiftsticky,' = 1) and (c.Priority>0 AND c.Priority<>2)) OR (c.Priority <=0 AND c.LastPosted <= ''',COALESCE(@i_FirstSelectLastPosted,UTC_TIMESTAMP()),''' )) 
		 AND (',i_ShowDeleted,' = 1 or (c.Flags & 8) <> 8)
		AND	((c.TopicMovedID IS NOT NULL) OR (c.NumPosts > 0)) 
		AND
		((',i_ShowMoved,' = 1)
		or
		(',i_ShowMoved,' <> 1 AND  c.TopicMovedID IS NULL))		
	order by
		 c.Priority DESC,c.LastPosted DESC LIMIT ',i_PageSize,' ;'); 
		 PREPARE tlist1 FROM @tlist1_str;
		 EXECUTE tlist1;		 
		DEALLOCATE PREPARE tlist1;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM returns a prepared result with a first select postion */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_bytags
(
	i_BoardID INT,
	i_ForumID INT,
	i_UserID INT,
	i_Tags VARCHAR(1024),
	i_SinceDate DATETIME,	
	i_PageIndex INT,
	i_PageSize INT
)
BEGIN
 DECLARE  ici_RowTotalCount INT DEFAULT 0;  
 DECLARE  ici_post_totalrowsnumber INT DEFAULT 0;
 DECLARE  ici_firstselectrownum INT DEFAULT 0; 
 DECLARE  ici_sortsincelatest INT DEFAULT 0;  
 DECLARE  ici_firstselectposted DATETIME; 
 
 SET i_PageIndex = i_PageIndex + 1;
		
	-- find total returned count
			
 select count(1) into ici_post_totalrowsnumber from {databaseSchema}.{objectQualifier}Topic t 
		   JOIN {databaseSchema}.{objectQualifier}TopicTags tt ON tt.TopicID = t.TopicID
		   JOIN {databaseSchema}.{objectQualifier}Tags tg ON tg.TagID = tt.TagID
		   JOIN {databaseSchema}.{objectQualifier}ActiveAccess aa ON (aa.ForumID = t.ForumID and aa.UserID = i_UserID) 
		   where   (t.Flags & 8) <> 8 and (i_ForumID <= 0 OR t.ForumID = i_ForumID) and tt.TagID  IN (SELECT CAST(i_Tags AS UNSIGNED)) and t.Posted > i_SinceDate;
	
	  select (i_PageIndex-1) * i_PageSize + 1  INTO ici_firstselectrownum;
 
	
  SET @FirstSelectPosted = NULL;

 SET @ttlist2_str = CONCAT('select
 t.Posted INTO @i_FirstSelectPosted
	from
		{databaseSchema}.{objectQualifier}Topic t 
		   JOIN {databaseSchema}.{objectQualifier}TopicTags tt ON tt.TopicID = t.TopicID
		   JOIN {databaseSchema}.{objectQualifier}Tags tg ON tg.TagID = tt.TagID
		   JOIN {databaseSchema}.{objectQualifier}ActiveAccess aa ON (aa.ForumID = t.ForumID and aa.UserID = ',COALESCE(i_UserID,0),') 
		   where   (t.Flags & 8) <> 8 and (',i_ForumID,' <= 0 OR t.ForumID = ',i_ForumID,')  and tt.TagID  IN (SELECT CAST(',i_Tags,' AS UNSIGNED)) and t.Posted > ''',i_SinceDate,'''
	order by
		t.Posted DESC LIMIT 1 OFFSET ',ici_firstselectrownum - 1,';');  
		PREPARE ttlist2 from @ttlist2_str;
		EXECUTE ttlist2;		 
		DEALLOCATE PREPARE ttlist2;			
			  
	SET @ttlist1_str = CONCAT( 
	'select	
			c.ForumID,
			c.TopicID,
			c.Posted,
			IFNULL(c.TopicMovedID,c.TopicID) AS LinkTopicID,
			c.TopicMovedID,
			(SELECT COUNT(1) as FavoriteCount FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE TopicId = IFNULL(c.TopicMovedID,c.TopicID)) AS FavoriteCount,
			c.Topic as Subject,
			c.Description,
			c.Status,
			c.Styles,
			c.UserID,
			IFNULL(c.UserName,b.Name) AS Starter,
			IFNULL(c.UserDisplayName,b.DisplayName) AS StarterDisplay,
			(c.NumPosts - 1) AS Replies,
			(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message mes WHERE mes.TopicID = c.TopicID AND (mes.Flags & 8) = 8 AND (mes.Flags & 16) = 16 AND ((',COALESCE(i_UserID,0),' <> 0 AND mes.UserID = ',COALESCE(i_UserID,0),') 
						OR (',COALESCE(i_UserID,0),' = 0)) ) AS NumPostsDeleted, 			
			c.Views,
			c.LastPosted,
			c.LastUserID,
			IFNULL(c.LastUserName,(SELECT x.Name FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserName,
			IFNULL(c.LastUserDisplayName,(SELECT x.DisplayName FROM {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserDisplayName,
			c.LastMessageID,
			c.TopicID AS LastTopicID,
			c.LinkDate,
			c.Flags AS TopicFlags,
			c.Priority,
			c.PollID,
			d.Flags AS ForumFlags,
			(SELECT CAST(Message as char(1000)) FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(c.TopicMovedID,c.TopicID) AND mes2.Position = 0 LIMIT 1) AS FirstMessage,		    
			(case(',0,')
			when 1 then  b.UserStyle 
			else ''''	 end) AS StarterStyle,
			(case(',0,')
			when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=c.LastUserID)   
			else '''' end) AS LastUserStyle,			
			(case(',0,')
			 when 1 then
			(SELECT  CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=c.ForumID AND x.UserID = c.UserID LIMIT 1)
			 else CAST(NULL AS DATETIME) end ) AS LastForumAccess,
			(case(',0,')
			 when 1 then
			   (SELECT CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = c.UserID LIMIT 1)
			 else CAST(NULL AS DATETIME)	 end) AS LastTopicAccess,		
			 m.Message,
			 {databaseSchema}.{objectQualifier}biginttoint(',ici_post_totalrowsnumber,') AS TotalRows,
			{databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex,
			 (SELECT Tag FROM {databaseSchema}.{objectQualifier}Tags where TagID = CAST(',i_Tags,' AS UNSIGNED) LIMIT 1) AS Tags, 
			 (SELECT GROUP_CONCAT(tg.tag separator '','') 
			 FROM {databaseSchema}.{objectQualifier}Tags tg 
			 JOIN {databaseSchema}.{objectQualifier}TopicTags tt 
			 on tt.tagID = tg.TagID where tt.TopicID = c.TopicID) 
			 AS TopicTags,
			 c.TopicImage,
			 c.TopicImageType,
			 c.TopicImageBin,
			 0 as HasAttachments
	from		
		{databaseSchema}.{objectQualifier}Topic c 
		   JOIN {databaseSchema}.{objectQualifier}TopicTags tt ON tt.TopicID = c.TopicID
		   JOIN {databaseSchema}.{objectQualifier}Tags tg ON tg.TagID = tt.TagID
		   JOIN {databaseSchema}.{objectQualifier}ActiveAccess aa ON (aa.ForumID = c.ForumID and aa.UserID = ',COALESCE(i_UserID,0),') 
		   JOIN {databaseSchema}.{objectQualifier}Message m ON (m.TopicID = c.TopicID and m.Position = 0)
		   JOIN {databaseSchema}.{objectQualifier}User b ON b.UserID=c.UserID
		   JOIN {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		   where  (',i_ForumID,' <= 0 OR c.ForumID = ',i_ForumID,')  and c.Posted <= ''',COALESCE(@i_FirstSelectPosted,UTC_TIMESTAMP()),''' AND  (c.Flags & 8) <> 8 and tt.TagID  IN (SELECT CAST(',i_Tags,' AS UNSIGNED)) and c.Posted > ''',i_SinceDate,'''	
	order by
		 c.Posted DESC LIMIT ',i_PageSize,' ;'); 
		 PREPARE ttlist1 FROM @ttlist1_str;
		 EXECUTE ttlist1;		 
		DEALLOCATE PREPARE ttlist1;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_listmessages(i_TopicID INT)
BEGIN
	SELECT * FROM {databaseSchema}.{objectQualifier}Message
	WHERE TopicID = i_TopicID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_lock(i_TopicID INT,i_Locked TINYINT(1)) 
BEGIN

	IF i_Locked<>0 THEN
		UPDATE {databaseSchema}.{objectQualifier}Topic 
		SET Flags = Flags | 1
		WHERE TopicID = i_TopicID;
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}Topic
		SET Flags = Flags & ~1
		WHERE TopicID = i_TopicID;
		END IF;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE procedure {databaseSchema}.{objectQualifier}topic_move(i_TopicID INT,i_ForumID INT,i_ShowMoved TINYINT(1), i_LinkDays INT,
	i_UTCTIMESTAMP DATETIME) 
BEGIN
	 DECLARE ici_OldForumID INT;
	 declare ici_newTimestamp datetime;
	 declare i_MovedTopicID int;
		if i_LinkDays > -1
		then
		SET ici_newTimestamp = DATE_ADD(i_UTCTIMESTAMP, INTERVAL i_LinkDays day );
		end if;
	 SELECT  ForumID INTO ici_OldForumID FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicID = i_TopicID;
 
 IF ici_OldForumID !=i_ForumID THEN 
	 IF i_ShowMoved<>0  THEN
	  -- delete an old link if exists
	 DELETE FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicMovedID = i_TopicID;	
		 /*create a moved message*/
		 INSERT INTO {databaseSchema}.{objectQualifier}Topic(ForumID,UserID,UserName,UserDisplayName,Posted,Topic,Views,Flags,Priority,PollID,TopicMovedID,LastPosted,NumPosts, LinkDate)
		 SELECT ForumID,UserID,UserName,UserDisplayName,Posted,Topic,0,Flags,Priority,PollID,i_TopicID,LastPosted,0, ici_newTimestamp 
		 FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicID = i_TopicID;
		 SET i_MovedTopicID = LAST_INSERT_ID();
		 INSERT INTO {databaseSchema}.{objectQualifier}TopicTags(TopicID,TagID) 
		 select i_MovedTopicID,TagID from {databaseSchema}.{objectQualifier}TopicTags WHERE TopicID = i_TopicID;		 
	 ELSE
	  DELETE FROM {databaseSchema}.{objectQualifier}TopicTags 
		 WHERE TopicID = i_TopicID;
	 END IF;

	 UPDATE {databaseSchema}.{objectQualifier}Tags	set  TagCount = TagCount - 1 where TagID in (select TagID from 	{databaseSchema}.{objectQualifier}TopicTags WHERE TopicID = i_TopicID);
 
	/* move the topic */
	 UPDATE {databaseSchema}.{objectQualifier}Topic SET ForumID = i_ForumID WHERE TopicID = i_TopicID;
 
	 /* update last posts */
	 CALL {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_OldForumID);
	 CALL {databaseSchema}.{objectQualifier}forum_updatelastpost(i_ForumID);
	 
	 /* update stats */
	  CALL {databaseSchema}.{objectQualifier}forum_updatestats (ici_OldForumID);
	  CALL {databaseSchema}.{objectQualifier}forum_updatestats (i_ForumID);
  END IF;   
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}topic_prune(
				i_BoardID INT,
				i_ForumID INT,
				i_Days    INT,
				i_DeletedOnly TINYINT(1),
				i_UTCTIMESTAMP DATETIME)                
BEGIN  
  
			  
		SELECT yt.TopicID
		FROM  {databaseSchema}.{objectQualifier}Topic yt
		INNER JOIN
		{databaseSchema}.{objectQualifier}Forum yf
		ON
		yt.ForumID = yf.ForumID
		INNER JOIN
		{databaseSchema}.{objectQualifier}Category yc
		ON
		yf.CategoryID = yc.CategoryID
		WHERE
			yc.BoardID = i_BoardID AND
		   (i_ForumID IS NULL OR yt.ForumID = i_ForumID) AND
			Priority = 0 AND
			(yt.Flags & 512) = 0 AND 			
				-- not flagged as persistent 
		 DATEDIFF(UTC_DATE(),yt.LastPosted) > i_Days
		 AND (i_DeletedOnly = 0 OR (yt.Flags & 8) = 8);
		  END;
	 --GO



	 /* STORED PROCEDURE CREATED BY VZ-TEAM */     
	 CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_save(
	 i_ForumID	INT,
	 i_Subject	VARCHAR(255),
	 i_status	VARCHAR(255),
	 i_Styles	VARCHAR(255),
	 i_Description	VARCHAR(255),
	 i_UserID		INT,
	 i_Message	TEXT,	
	 i_Priority	SMALLINT,
	 i_UserName	VARCHAR(128),
	 i_IP		VARCHAR(39), 
	 i_Posted		DATETIME,
	 i_BlogPostID	VARCHAR(128),
	 i_Flags		INT,
	 i_MessageDescription VARCHAR(255),
	 i_Tags 	VARCHAR(1024),
	 i_UTCTIMESTAMP DATETIME
	 )
	 BEGIN
	 DECLARE ici_TopicID INT;
	 DECLARE ici_MessageID INT;
	 DECLARE ici_OverrideDisplayName tINYINT(1);


	 IF i_Posted IS NULL THEN
	 SET i_Posted = i_UTCTIMESTAMP; 
	 END IF;

	 SELECT SIGN(COUNT(Name))  
	 INTO ici_OverrideDisplayName 
	 FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID AND Name != i_UserName;	

	 -- create the topic
	 INSERT INTO {databaseSchema}.{objectQualifier}Topic(ForumID,Topic,UserID,Posted,Views,Priority,UserName,UserDisplayName,NumPosts, Description, Status, Styles)
	 VALUES(i_ForumID,CONVERT(i_Subject USING {databaseEncoding}),i_UserID,i_Posted,0,i_Priority,(CASE WHEN ici_OverrideDisplayName = 1 THEN i_UserName ELSE (SELECT Name FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID) END),(CASE WHEN ici_OverrideDisplayName = 1 THEN i_UserName ELSE (SELECT DisplayName FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID) END) ,0, i_Description, i_status, i_Styles);

	 /* get its id*/
	 SET ici_TopicID = LAST_INSERT_ID();

	 /* add message to the topic*/
	 CALL {databaseSchema}.{objectQualifier}message_save (ici_TopicID,i_UserID,i_Message,i_UserName,i_IP,i_Posted,NULL,i_BlogPostID, null, null, i_MessageDescription,i_Flags,i_UTCTIMESTAMP,ici_MessageID);

	 CALL {databaseSchema}.{objectQualifier}topic_tagsave(ici_TopicID, i_Tags);

	 SELECT   ici_TopicID AS TopicID,  ici_MessageID AS MessageID;
	 END;
--GO

   /* STORED PROCEDURE CREATED BY VZ-TEAM */
	 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}topic_updatelastpost(
	 i_ForumID INT,
	 i_TopicID INT)
	 BEGIN
	 DECLARE ici_ForumID INT DEFAULT NULL;
	 DECLARE ici_TopicID INT DEFAULT NULL;

	 IF i_ForumID IS NOT NULL OR i_ForumID >=0 THEN SET ici_ForumID = i_ForumID;END IF;
	 IF i_TopicID  IS NOT NULL OR i_TopicID  >=0 THEN SET ici_TopicID = i_TopicID ;END IF;

	 IF ici_TopicID IS NOT NULL THEN
	 UPDATE {databaseSchema}.{objectQualifier}Topic
	 SET    LastPosted = (SELECT  x.Posted
	 FROM     {databaseSchema}.{objectQualifier}Message x
	 WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
	 AND (x.Flags & 24) = 16
							 ORDER BY Posted DESC LIMIT 1),
				LastMessageID=(SELECT    x.MessageID
								FROM     {databaseSchema}.{objectQualifier}Message x
								WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
								AND (x.Flags & 24) = 16
								ORDER BY Posted DESC LIMIT 1),
			   LastUserID=(SELECT   x.UserID
							 FROM    {databaseSchema}.{objectQualifier}Message x
							 WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
							 AND (x.Flags & 24) = 16
							 ORDER BY Posted DESC LIMIT 1) ,
			   LastUserName=(SELECT   x.UserName
							   FROM     {databaseSchema}.{objectQualifier}Message x
							   WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
							   AND (x.Flags & 24) = 16 
							   ORDER BY Posted DESC LIMIT 1),
			   LastUserDisplayName=(SELECT   x.UserDisplayName
							   FROM     {databaseSchema}.{objectQualifier}Message x
							   WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
							   AND (x.Flags & 24) = 16 
							   ORDER BY Posted DESC LIMIT 1),
			   LastMessageFlags = (SELECT x.Flags FROM {databaseSchema}.{objectQualifier}Message x 
								   WHERE x.TopicID={databaseSchema}.{objectQualifier}Topic.TopicID 
								   and (x.Flags & 24)=16 
								   ORDER BY Posted DESC LIMIT 1)                          
		WHERE  TopicID = ici_TopicID;
		ELSE
		UPDATE {databaseSchema}.{objectQualifier}Topic
		SET    LastPosted=(SELECT   x.Posted
							 FROM     {databaseSchema}.{objectQualifier}Message x
							 WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
							 AND (x.Flags & 24) = 16
							 ORDER BY Posted DESC LIMIT 1),
			   LastMessageID=(SELECT    x.MessageID
								FROM     {databaseSchema}.{objectQualifier}Message x
								WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
								AND (x.Flags & 24) = 16
								ORDER BY Posted DESC LIMIT 1),
			   LastUserID=(SELECT   x.UserID
							 FROM     {databaseSchema}.{objectQualifier}Message x
							 WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
							 AND (x.Flags & 24) = 16
							 ORDER BY Posted DESC LIMIT 1),
			   LastUserName = (SELECT   x.UserName
							   FROM     {databaseSchema}.{objectQualifier}Message x
							   WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
							   AND (x.Flags & 24) = 16 
							   ORDER BY Posted DESC LIMIT 1),
			   LastUserDisplayName=(SELECT   x.UserDisplayName
							   FROM     {databaseSchema}.{objectQualifier}Message x
							   WHERE    x.TopicID = {databaseSchema}.{objectQualifier}Topic.`TopicID`
							   AND (x.Flags & 24) = 16 
							   ORDER BY Posted DESC LIMIT 1),
			   LastMessageFlags = (SELECT x.Flags FROM {databaseSchema}.{objectQualifier}Message x 
								   WHERE x.TopicID={databaseSchema}.{objectQualifier}Topic.TopicID 
								   and (x.Flags & 24)=16 
								   ORDER BY Posted DESC LIMIT 1)
				WHERE  TopicMovedID IS NULL
	 AND (ici_ForumID IS NULL
	 OR ForumID = ici_ForumID);
	
	/* CALL {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_ForumID);*/
	 END IF;
	 END ;
  --GO

  CREATE procedure {databaseSchema}.{objectQualifier}topic_updatetopic
(i_TopicID int,i_Topic VARCHAR (100))
begin
		if i_TopicID is not null then
		update {databaseSchema}.{objectQualifier}Topic set
			Topic = i_Topic
		where TopicID = i_TopicID;
		end if;
end;
--GO
	 
	 /* STORED PROCEDURE CREATED BY VZ-TEAM */     
	 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}user_accessmasks(
	 i_BoardID INT,
	 i_UserID  INT)
	 BEGIN
	 SELECT   *
	 FROM     ((SELECT   e.AccessMaskID AS AccessMaskID,
	 e.Name AS AccessMaskName,
	 f.ForumID AS ForumID,
	 f.Name AS ForumName,
	 f.CategoryID,
	 f.ParentID
	 FROM     {databaseSchema}.{objectQualifier}User a
	 JOIN {databaseSchema}.{objectQualifier}UserGroup b
	 ON b.UserID = a.UserID
	 JOIN {databaseSchema}.{objectQualifier}Group c
	 ON c.GroupID = b.GroupID
	 JOIN {databaseSchema}.{objectQualifier}ForumAccess d
	 ON d.GroupID = c.GroupID
	 JOIN {databaseSchema}.{objectQualifier}AccessMask e
	 ON e.AccessMaskID = d.AccessMaskID
	 JOIN {databaseSchema}.{objectQualifier}Forum f
	 ON f.ForumID = d.ForumID
	 WHERE    a.UserID = i_UserID
	 AND c.BoardID = i_BoardID
	 GROUP BY e.AccessMaskID,e.Name,f.ForumID,f.Name)
	 UNION
	 (SELECT   c.AccessMaskID AS AccessMaskID,
	 c.Name AS AccessMaskName,
	 d.ForumID AS ForumID,
	 d.Name AS  ForumName,
	 d.CategoryID,
	 d.ParentID
	 FROM     {databaseSchema}.{objectQualifier}User a
	 JOIN {databaseSchema}.{objectQualifier}UserForum b
	 ON b.UserID = a.UserID
	 JOIN {databaseSchema}.{objectQualifier}AccessMask c
	 ON c.AccessMaskID = b.AccessMaskID
	 JOIN {databaseSchema}.{objectQualifier}Forum d
	 ON d.ForumID = b.ForumID
	 WHERE    a.UserID = i_UserID
	 AND c.BoardID = i_BoardID
	 GROUP BY c.AccessMaskID,c.Name,d.ForumID,d.Name)) AS x
	 ORDER BY ForumName,
	 AccessMaskName;
	 END;   
--GO
	 /* STORED PROCEDURE CREATED BY VZ-TEAM */     
	 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}user_accessmasksbyforum(
	 i_BoardID INT,
	 i_UserID  INT)
	 BEGIN
	 (SELECT   e.AccessMaskID AS AccessMaskID,
	 e.Name AS AccessMaskName,
	 e.Flags AS AccessMaskFlags,
	 {databaseSchema}.{objectQualifier}biginttobool(0) as IsUserMask,
	 f.ForumID AS ForumID,
	 f.Name AS ForumName,
	 f.CategoryID,
	 f.ParentID,
	 c.GroupID,
	 c.Name	as GroupName
	 FROM     {databaseSchema}.{objectQualifier}User a
	 JOIN {databaseSchema}.{objectQualifier}UserGroup b
	 ON b.UserID = a.UserID
	 JOIN {databaseSchema}.{objectQualifier}Group c
	 ON c.GroupID = b.GroupID
	 JOIN {databaseSchema}.{objectQualifier}ForumAccess d
	 ON d.GroupID = c.GroupID
	 JOIN {databaseSchema}.{objectQualifier}AccessMask e
	 ON e.AccessMaskID = d.AccessMaskID
	 JOIN {databaseSchema}.{objectQualifier}Forum f
	 ON f.ForumID = d.ForumID
	 WHERE    a.UserID = i_UserID
	 AND c.BoardID = i_BoardID)
	 UNION
	 (SELECT   c.AccessMaskID AS AccessMaskID,
	 c.Name AS AccessMaskName,
	 c.Flags AS AccessMaskFlags,
	 {databaseSchema}.{objectQualifier}biginttobool(1) as IsUserMask,
	 d.ForumID AS ForumID,
	 d.Name AS  ForumName,
	 d.CategoryID,
	 d.ParentID,
	 {databaseSchema}.{objectQualifier}biginttobool(0) AS GroupID,
	 ''	as GroupName
	 FROM     {databaseSchema}.{objectQualifier}User a
	 JOIN {databaseSchema}.{objectQualifier}UserForum b
	 ON b.UserID = a.UserID
	 JOIN {databaseSchema}.{objectQualifier}AccessMask c
	 ON c.AccessMaskID = b.AccessMaskID
	 JOIN {databaseSchema}.{objectQualifier}Forum d
	 ON d.ForumID = b.ForumID
	 WHERE    a.UserID = i_UserID
	 AND c.BoardID = i_BoardID);
	 END;   
--GO



	 /* STORED PROCEDURE CREATED BY VZ-TEAM */     
	 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}user_addpoints(
	 i_UserID INT,i_FromUserID INT, i_UTCTIMESTAMP datetime,i_Points int)
	 BEGIN
	 declare i_VoteDate datetime;
	 UPDATE {databaseSchema}.{objectQualifier}User
	 SET    Points = Points + i_Points
	 WHERE  UserID = i_UserID;
	 IF i_FromUserID IS NOT NULL THEN		
	set i_VoteDate = (select VoteDate from {databaseSchema}.{objectQualifier}ReputationVote where ReputationFromUserID=i_FromUserID AND ReputationToUserID=i_UserID LIMIT 1);
	IF i_VoteDate is not null then    
		  update {databaseSchema}.{objectQualifier}ReputationVote set VoteDate=i_UTCTIMESTAMP where VoteDate = i_VoteDate AND ReputationFromUserID=i_FromUserID AND ReputationToUserID=i_UserID;	
	ELSE	 
		  insert into {databaseSchema}.{objectQualifier}ReputationVote(ReputationFromUserID,ReputationToUserID,VoteDate)
		  values (i_FromUserID, i_UserID, i_UTCTIMESTAMP);	 
	END IF;
	END IF;
	END;
	 
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */     
	 CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_adminsave
	 (i_BoardID INT,
	 i_UserID INT,
	 i_Name VARCHAR(128),
	 i_DisplayName VARCHAR(128),
	 i_Email VARCHAR(128),
	 i_Flags INT,
	 i_RankID INT)
	 BEGIN
	 UPDATE {databaseSchema}.{objectQualifier}User
	 SET
	 `Name` = i_Name,
	 DisplayName = i_DisplayName,
	 Email = i_Email,
	 RankID = i_RankID,
	 Flags = i_Flags
	 WHERE UserID = i_UserID;
	 SELECT i_UserID AS UserID;
	 END;
--GO
	 /* STORED PROCEDURE CREATED BY VZ-TEAM */     
	 CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_approve(i_UserID INT)
	 BEGIN
	 DECLARE ici_CheckEmailID INT;
	 DECLARE ici_Email VARCHAR(128);
	 DECLARE ici_bit TINYINT(1) DEFAULT 1;

	 SELECT
	 CheckEmailID,Email INTO ici_CheckEmailID,ici_Email
	 FROM
	 {databaseSchema}.{objectQualifier}CheckEmail
	 WHERE
	 UserID = i_UserID;

	 /*Update new user email*/
	 UPDATE {databaseSchema}.{objectQualifier}User SET Email = ici_Email, 
Flags = Flags | 2 WHERE UserID = i_UserID;
	 DELETE FROM {databaseSchema}.{objectQualifier}CheckEmail WHERE CheckEmailID = ici_CheckEmailID;
	 SELECT ici_bit;
	 END;
--GO

	 /* STORED PROCEDURE CREATED BY VZ-TEAM */  
	 CREATE procedure {databaseSchema}.{objectQualifier}user_approveall(i_BoardID INT)
   BEGIN
   
   DECLARE ici_UserID INT;
	
   DECLARE userslist CURSOR  FOR
	 SELECT UserID FROM {databaseSchema}.{objectQualifier}User WHERE BoardID=i_BoardID AND (Flags & 2)=0 ;
	  
	 OPEN userslist;   
   BEGIN
	  DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
   LOOP
		FETCH userslist INTO ici_UserID;          
		   CALL {databaseSchema}.{objectQualifier}user_approve (ici_UserID);
   END LOOP;        
	END;
	 
	CLOSE userslist;
	 
END;
--GO

	/* STORED PROCEDURE CREATED BY VZ-TEAM */    
	CREATE procedure {databaseSchema}.{objectQualifier}user_deleteavatar(i_UserID INT)
	BEGIN
	UPDATE {databaseSchema}.{objectQualifier}User
	SET AvatarImage = null,
	Avatar = null,
	AvatarImageType = null
	where UserID = i_UserID;
	END;
 --GO     

 CREATE procedure {databaseSchema}.{objectQualifier}user_aspnet
 (i_BoardID INT,
  i_UserName VARCHAR(128),
  i_DisplayName VARCHAR(128),
  i_Email VARCHAR(128),
  i_ProviderUserKey VARCHAR(64),
  i_IsApproved TINYINT(1),
 i_UTCTIMESTAMP DATETIME) 
BEGIN
/*SET NOCOUNT ON*/
	DECLARE ici_UserID INT;
		DECLARE ici_RankID INT;
		DECLARE ici_approvedFlag INT;
		DECLARE ici_DisplayName VARCHAR(128);
		DECLARE ici_tz int;
	SET ici_approvedFlag = 0;
	IF (i_IsApproved = 1) THEN SET ici_approvedFlag = 2;END IF;	
	
	SELECT UserID, DisplayName INTO ici_UserID,ici_DisplayName  FROM {databaseSchema}.{objectQualifier}User 
				  WHERE BoardID=i_BoardID 
				  AND ((`ProviderUserKey`=i_ProviderUserKey) OR (`Name` = i_UserName)) LIMIT 1;

	IF ici_UserID IS NOT NULL THEN 	 		
		
		UPDATE {databaseSchema}.{objectQualifier}User SET 
			`Name` = i_UserName,
			`DisplayName` = IFNULL(i_DisplayName,ici_DisplayName), 
			Email = i_Email,
			Flags = Flags | ici_approvedFlag
		WHERE
			UserID = ici_UserID ORDER BY UserID LIMIT 1;
	ELSE
	
		SELECT RankID INTO ici_RankID FROM {databaseSchema}.{objectQualifier}Rank 
				  WHERE (Flags & 1)<>0 AND BoardID=i_BoardID LIMIT 1;
	  IF (i_DisplayName IS NULL) 
		THEN
			SET i_DisplayName = i_UserName;
		END IF;		
				  set   ici_tz =(COALESCE((SELECT CAST(CAST(Value AS CHAR(10)) AS SIGNED) from {databaseSchema}.{objectQualifier}Registry where Name LIKE 'timezone' and BoardID = i_BoardID),(SELECT CAST(CAST(Value AS CHAR(10)) AS SIGNED) from {databaseSchema}.{objectQualifier}Registry where Name LIKE 'timezone')));
				  if ici_tz is null then
				  insert into {databaseSchema}.{objectQualifier}Registry(Name) values ('timezone');
				  insert into {databaseSchema}.{objectQualifier}Registry(Name, BoardID) values ('timezone',i_BoardID);
				  set ici_tz = 0;
				  end if;
				  INSERT INTO {databaseSchema}.{objectQualifier}User(BoardID,RankID,`Name`,`DisplayName`,Password,Email,Joined,LastVisit,NumPosts,TimeZone,Flags,ProviderUserKey)
				  VALUES(i_BoardID,ici_RankID,i_UserName,i_DisplayName,'-',i_Email,i_UTCTIMESTAMP,i_UTCTIMESTAMP,0,ici_tz,ici_approvedFlag,i_ProviderUserKey);

				  SET ici_UserID = LAST_INSERT_ID();
	   END IF;

				  SELECT  ici_UserID AS UserID;
	   END;
--GO
				  /* STORED PROCEDURE CREATED BY VZ-TEAM */                  
				  CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_avatarimage(i_UserID INT)
				  BEGIN
				  SELECT
				  UserID,
				  AvatarImage,
				  AvatarImageType
				  FROM {databaseSchema}.{objectQualifier}User WHERE UserID=i_UserID;
				  END;


				  /* STORED PROCEDURE CREATED BY VZ-TEAM */                 
				  CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_changepassword
				  (i_UserID INT,i_OldPassword VARCHAR(32),i_NewPassword VARCHAR(32))
				  BEGIN
				  DECLARE ici_CurrentOld VARCHAR(32);
				  DECLARE ici_Success TINYINT(1) DEFAULT 1;
				  SELECT  Password INTO ici_CurrentOld FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID;
				  IF ici_CurrentOld<>i_OldPassword THEN
				SET ici_Success=0;  
		SELECT ici_Success AS Success;		
	
		ELSE 
	UPDATE {databaseSchema}.{objectQualifier}User SET Password = i_NewPassword WHERE UserID = i_UserID;
	SELECT ici_Success AS Success;
		END IF;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_delete(i_UserID INT)
BEGIN
	DECLARE ici_GuestUserID	INT;
	DECLARE ici_UserName	VARCHAR(255);
	DECLARE ici_UserDisplayName	VARCHAR(255);
	DECLARE ici_GuestCount	INT;
 
	SELECT `Name`, DisplayName INTO ici_UserName,ici_UserDisplayName FROM {databaseSchema}.{objectQualifier}User WHERE UserID=i_UserID;
 
	SELECT 
		 a.UserID INTO ici_GuestUserID
	FROM
		{databaseSchema}.{objectQualifier}User a
		inner join {databaseSchema}.{objectQualifier}UserGroup b on b.UserID = a.UserID
		inner join {databaseSchema}.{objectQualifier}Group c on b.GroupID = c.GroupID
	WHERE
		(c.Flags & 2)<>0 GROUP BY a.UserID LIMIT 1;
 
	SELECT 
		  COUNT(1) INTO  ici_GuestCount
	FROM 
		{databaseSchema}.{objectQualifier}UserGroup a
		join {databaseSchema}.{objectQualifier}Group b on b.GroupID=a.GroupID
	WHERE
		(b.Flags & 2)<>0;

	IF NOT (ici_GuestUserID=i_UserID AND ici_GuestCount=1) THEN


	UPDATE {databaseSchema}.{objectQualifier}Message SET UserName=ici_UserName,UserDisplayName = ici_UserDisplayName,UserID=ici_GuestUserID,EditedBy=ici_GuestUserID WHERE UserID=i_UserID;
	UPDATE {databaseSchema}.{objectQualifier}Topic SET UserName=ici_UserName,UserDisplayName = ici_UserDisplayName,UserID=ici_GuestUserID WHERE UserID=i_UserID;
	UPDATE {databaseSchema}.{objectQualifier}Topic SET LastUserName=ici_UserName,LastUserDisplayName = ici_UserDisplayName,LastUserID=ici_GuestUserID WHERE LastUserID=i_UserID;
	UPDATE {databaseSchema}.{objectQualifier}Forum SET LastUserName=ici_UserName,LastUserDisplayName = ici_UserDisplayName,LastUserID=ici_GuestUserID WHERE LastUserID=i_UserID;

	DELETE FROM {databaseSchema}.{objectQualifier}ActiveAccess WHERE UserID=i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}Active WHERE UserID=i_UserID;

	DELETE FROM {databaseSchema}.{objectQualifier}EventLog WHERE UserID=i_UserID	;
	DELETE FROM {databaseSchema}.{objectQualifier}UserPMessage WHERE UserID=i_UserID;
  
	IF NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}PMessage WHERE FromUserID=i_UserID) THEN
	DELETE FROM {databaseSchema}.{objectQualifier}PMessage
	WHERE FromUserID=i_UserID;
	END IF;
	-- set messages as from guest so the User can be deleted
	UPDATE {databaseSchema}.{objectQualifier}PMessage 
	SET FromUserID = ici_GuestUserID WHERE FromUserID = i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}CheckEmail WHERE UserID = i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}WatchTopic WHERE UserID = i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}WatchForum WHERE UserID = i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}TopicReadTracking WHERE UserID = i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}ForumReadTracking WHERE UserID = i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}ReputationVote WHERE ReputationFromUserID = i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}UserGroup WHERE UserID = i_UserID;
	/*ABOT CHANGED
	Delete UserForums entries Too*/
	DELETE FROM  {databaseSchema}.{objectQualifier}UserForum WHERE UserID = i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}IgnoreUser WHERE UserID = i_UserID OR IgnoredUserID = i_UserID;
	/*END ABOT CHANGED 09.04.2004*/
	DELETE FROM {databaseSchema}.{objectQualifier}Thanks where ThanksFromUserID = i_UserID OR ThanksToUserID = i_UserID;
	DELETE FROM  {databaseSchema}.{objectQualifier}FavoriteTopic WHERE UserID = i_UserID;
	-- Delete all the Buddy relations between this user and other users.
	DELETE FROM {databaseSchema}.{objectQualifier}Buddy where FromUserID=i_UserID OR ToUserID=i_UserID;
	DELETE FROM {databaseSchema}.{objectQualifier}AdminPageUserAccess where UserID = i_UserID;
	DELETE FROM  {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID;
	END IF;
	END;
--GO

/* User Ignore Procedures */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_addignoreduser
	(i_UserID INT,
	i_IgnoredUserID INT)
BEGIN
	IF NOT EXISTS (SELECT * FROM {databaseSchema}.{objectQualifier}IgnoreUser WHERE UserID = i_UserID AND IgnoredUserID = i_IgnoredUserID)
	THEN
		INSERT INTO {databaseSchema}.{objectQualifier}IgnoreUser (UserID, IgnoredUserID) VALUES (i_UserID, i_IgnoredUserID);
	END IF;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_ignoredlist
	(i_UserID INT)
BEGIN
	SELECT * FROM {databaseSchema}.{objectQualifier}IgnoreUser WHERE UserID = i_UserID;
END;	
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_removeignoreduser
	(i_UserId INT,
	i_IgnoredUserId INT)
 BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}IgnoreUser WHERE UserID = i_userId AND IgnoredUserID = i_ignoredUserId;
	
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_isuserignored
	(i_UserId INT,
	i_IgnoredUserId INT)
BEGIN
	IF EXISTS(SELECT * FROM {databaseSchema}.{objectQualifier}IgnoreUser WHERE UserID = i_UserId AND IgnoredUserID = i_IgnoredUserId)
	THEN
		SELECT 1;	
	ELSE	
		SELECT 0;
	END IF;
	
END;	
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */   
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_deleteold(i_BoardID INT, i_Days INT,
 i_UTCTIMESTAMP DATETIME)
	BEGIN
	DECLARE ici_Since DATETIME DEFAULT i_UTCTIMESTAMP;

	/*set ici_Since = UTC_TIMESTAMP()*/

	DELETE FROM {databaseSchema}.{objectQualifier}EventLog  WHERE UserID IN (SELECT UserID from {databaseSchema}.{objectQualifier}User WHERE BoardID=i_BoardID and {databaseSchema}.{objectQualifier}bitset(Flags,2)=0 and DATEDIFF(ici_Since,Joined)>i_Days);
	DELETE FROM {databaseSchema}.{objectQualifier}CheckEmail WHERE UserID IN (SELECT UserID from {databaseSchema}.{objectQualifier}User WHERE BoardID=i_BoardID and {databaseSchema}.{objectQualifier}bitset(Flags,2)=0 and DATEDIFF(ici_Since,Joined)>i_Days);
	DELETE FROM {databaseSchema}.{objectQualifier}UserGroup WHERE UserID IN (SELECT UserID from {databaseSchema}.{objectQualifier}User WHERE BoardID=i_BoardID and {databaseSchema}.{objectQualifier}bitset(Flags,2)=0 and DATEDIFF(ici_Since,Joined)>i_Days);
	DELETE FROM {databaseSchema}.{objectQualifier}User where BoardID=i_BoardID and {databaseSchema}.{objectQualifier}bitset(Flags,2)=0 AND DATEDIFF(ici_Since,Joined)>i_Days;
	END;
--GO

 /* STORED PROCEDURE CREATED BY VZ-TEAM */
  
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_emails(i_BoardID INT,i_GroupID INT)
	BEGIN
	IF i_GroupID = 0 THEN SET i_GroupID = null; END IF;
	IF i_GroupID IS NULL THEN
	SELECT
	a.Email
	FROM
	{databaseSchema}.{objectQualifier}User a
	WHERE
	a.Email IS NOT NULL AND
	a.BoardID = i_BoardID and
	a.Email IS NOT NULL AND
	a.Email<>'';
	ELSE
		SELECT 
			a.Email 
		FROM
			{databaseSchema}.{objectQualifier}User a 
			JOIN {databaseSchema}.{objectQualifier}UserGroup b 
			ON b.UserID=a.UserID
			JOIN {databaseSchema}.{objectQualifier}Group c 
			ON c.GroupID=b.GroupID		
		WHERE 
			b.GroupID = i_GroupID AND 
			(c.Flags & 2)=0 AND
			a.Email IS NOT NULL AND 
			a.Email<>'';
		END IF;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_find(
i_BoardID INT,
i_Filter TINYINT(1),
i_UserName VARCHAR(128),
i_DisplayName VARCHAR(128),
i_Email VARCHAR(128),
i_NotificationType INT,
i_DailyDigest TINYINT(1)) 
BEGIN
	IF i_Filter<>0 THEN 	
		IF i_UserName IS NOT NULL THEN
			SET i_UserName = CONCAT('%',i_UserName,'%');  END IF;
			
	IF i_DisplayName IS NOT NULL THEN
			SET i_DisplayName = CONCAT('%',i_DisplayName,'%');  END IF;
		SELECT 
			a.userid,    
			a.boardid,    
			a.provideruserkey,    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.Culture,
			a.Culture as UserCulture,
			a.DailyDigest,
			a.NotificationType,
			a.TextEditor,
			a.usesinglesignon,
			{databaseSchema}.{objectQualifier}biginttobool(COALESCE((a.flags & 2)= 2,false)) AS isapproved,
			{databaseSchema}.{objectQualifier}biginttobool(COALESCE((a.flags & 16)=16,false)) AS isactiveexcluded,	
			(SELECT {databaseSchema}.{objectQualifier}biginttobool(COUNT(1)) FROM {databaseSchema}.{objectQualifier}UserGroup x join {databaseSchema}.{objectQualifier}Group y ON x.GroupID=y.GroupID WHERE x.UserID=a.UserID AND (y.Flags & 2)<>0) AS IsGuest,
			(SELECT COUNT(1) from {databaseSchema}.{objectQualifier}UserGroup x JOIN {databaseSchema}.{objectQualifier}Group y ON x.GroupID=y.GroupID where x.UserID=a.UserID and (y.Flags & 1)<>0) AS IsAdmin
		FROM 
			{databaseSchema}.{objectQualifier}User a
		WHERE 
			a.BoardID=i_BoardID AND
			((i_UserName IS NOT NULL and a.Name LIKE i_UserName) 
			OR (i_Email IS NOT NULL and Email LIKE i_Email) 
			OR (i_DisplayName IS NOT NULL AND a.DisplayName like i_DisplayName)
			OR (i_NotificationType IS NOT NULL AND a.NotificationType = i_NotificationType) 
			OR (i_DailyDigest IS NOT NULL AND a.DailyDigest = i_DailyDigest))
		ORDER BY
			a.Name;
	ELSE
	
		SELECT
			a.userid,    
			a.boardid,    
			a.provideruserkey,    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.Culture ,
			a.Culture as UserCulture,
			a.dailydigest,
			a.notificationtype,
			a.texteditor,
			a.usesinglesignon,
			{databaseSchema}.{objectQualifier}biginttobool(COALESCE((a.flags & 2)= 2,false)) AS isapproved,
			{databaseSchema}.{objectQualifier}biginttobool(COALESCE((a.flags & 16)=16,false)) AS isactiveexcluded,	
			(SELECT {databaseSchema}.{objectQualifier}biginttobool(count(1)) from {databaseSchema}.{objectQualifier}UserGroup x JOIN {databaseSchema}.{objectQualifier}Group y ON x.GroupID=y.GroupID where x.UserID=a.UserID and (y.Flags & 2)<>0) AS IsGuest,
			(SELECT count(1) from {databaseSchema}.{objectQualifier}UserGroup x JOIN {databaseSchema}.{objectQualifier}Group y ON x.GroupID=y.GroupID where x.UserID=a.UserID and (y.Flags & 1)<>0) AS IsAdmin
		FROM 
			{databaseSchema}.{objectQualifier}User a
		WHERE
			a.BoardID=i_BoardID AND
			((i_UserName IS NOT NULL AND a.Name=i_UserName) 
			OR (i_Email IS NOT NULL AND Email=i_Email)
			OR (i_DisplayName IS NOT NULL AND a.DisplayName=i_DisplayName)
			OR (i_NotificationType IS NOT NULL AND a.NotificationType = i_NotificationType) 
			OR (i_DailyDigest IS NOT NULL AND a.DailyDigest = i_DailyDigest));
	END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_getpoints (i_UserID INT) 
BEGIN
	SELECT Points FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_getsignature(i_UserID INT) 
BEGIN
	SELECT Signature FROM {databaseSchema}.{objectQualifier}User WHERE UserID = i_UserID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_guest
 (
	i_BoardID INT
 )
BEGIN
	SELECT 
		a.UserID
	FROM
		{databaseSchema}.{objectQualifier}User a
		INNER JOIN {databaseSchema}.{objectQualifier}UserGroup b ON b.UserID = a.UserID
		INNER JOIN {databaseSchema}.{objectQualifier}Group c ON b.GroupID = c.GroupID
	WHERE
		a.BoardID = i_BoardID AND
		(c.Flags & 2)<>0 ORDER BY a.UserID LIMIT 1;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_list_new
(i_BoardID INT,i_UserID INT,i_Approved TINYINT(1),i_GroupID INT,i_RankID INT, i_StyledNicks TINYINT(1),i_UTCTIMESTAMP DATETIME) 
BEGIN
	IF i_UserID IS NOT NULL THEN
		SELECT 
			a.*, 
			a.Culture AS CultureUser,			
			b.Name AS RankName,
			(case(i_StyledNicks)
			  when 1 then a.UserStyle
			else ''	 end) AS Style, 
			IFNULL(a.Flags & 1,0) AS IsHostAdmin,
			IFNULL(a.Flags & 4,0) AS IsGuest, 
			DATEDIFF(i_UTCTIMESTAMP,a.Joined)+1 AS NumDays, 			
			(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x WHERE (x.Flags & 24)=16) AS NumPostsForum,
			(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}User x 
						  WHERE x.UserID=a.UserID 
							AND AvatarImage IS NOT NULL) AS HasAvatarImage
		FROM 
			{databaseSchema}.{objectQualifier}User a
			JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID=a.RankID
			/*LEFT JOIN {databaseSchema}.{objectQualifier}vaccess c ON c.UserID=a.UserID*/
		WHERE 
			a.UserID = i_UserID AND
			a.BoardID = i_BoardID AND
			(i_Approved IS NULL OR (i_Approved=0 AND (a.Flags & 2)=0) 
							OR (i_Approved=1 and (a.Flags & 2)=2))
		ORDER BY 
			a.Name;

	ELSEIF i_GroupID IS NULL and i_RankID IS NULL THEN
		SELECT 
			a.*,
			a.Culture AS CultureUser, 
			b.Name AS RankName,
			(case(i_StyledNicks)
			  when 1 then  a.UserStyle
			else ''	 end) AS Style, 
			IFNULL(a.Flags & 1,0) AS IsHostAdmin,                     
			IFNULL(a.Flags & 4,0) AS IsGuest, 			
			(SELECT COUNT(1) from {databaseSchema}.{objectQualifier}UserGroup x
						   JOIN {databaseSchema}.{objectQualifier}Group y 
							 ON y.GroupID=x.GroupID 
							   WHERE x.UserID=a.UserID
								 AND (y.Flags & 1)<>0) AS IsAdmin           		 			
			
		FROM 
			{databaseSchema}.{objectQualifier}User a
			JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID=a.RankID
		WHERE 
			a.BoardID = i_BoardID 
						  AND (i_Approved IS NULL 
						   OR (i_Approved=0 AND (a.Flags & 2)=0) 
							 OR (i_Approved=1 AND (a.Flags & 2)=2))
		ORDER BY 
			a.Name;
	ELSE
		SELECT 
			a.*,
			a.Culture AS CultureUser,
			b.Name AS RankName,	
			(case(i_StyledNicks)
			  when 1 then  a.UserStyle
			else ''	 end) AS Style, 
			IFNULL(a.Flags & 1,0) AS IsHostAdmin,            
			IFNULL(a.Flags & 4,0) AS IsGuest,	
			(SELECT COUNT(1) from {databaseSchema}.{objectQualifier}UserGroup x 
						JOIN {databaseSchema}.{objectQualifier}Group y ON y.GroupID=x.GroupID 
						WHERE x.UserID=a.UserID 
						AND (y.Flags & 1)<>0) AS IsAdmin 			 			
			
		FROM 
			{databaseSchema}.{objectQualifier}User a
			JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID=a.RankID
		WHERE 
			a.BoardID = i_BoardID and
			(i_Approved IS NULL 
						  OR (i_Approved=0 AND (a.Flags & 2)=0) 
						  OR (i_Approved=1 AND (a.Flags & 2)=2)) 
						  AND (i_GroupID IS NULL OR EXISTS
							  (SELECT 1 FROM {databaseSchema}.{objectQualifier}UserGroup x 
								 WHERE x.UserID=a.UserID AND x.GroupID=i_GroupID)) 
								  AND (i_RankID IS NULL OR a.RankID=i_RankID)
		ORDER BY 
			a.Name;
		 END IF;
END;
--GO

create procedure {databaseSchema}.{objectQualifier}user_listmembers(
				I_BoardID int,
				I_UserID int,
				I_Approved  tinyint(1),
				I_GroupID int,
				I_RankID int,
				I_StyledNicks tinyint(1),
				I_Literals VARCHAR(255), 
				I_Exclude  tinyint(1), 
				I_BeginsWith  tinyint(1), 				
				I_PageIndex int, 
				I_PageSize int,
				I_SortName int,
				I_SortRank int,
				I_SortJoined int,
				I_SortPosts int,
				I_SortLastVisit int,
				I_NumPosts int,
				I_NumPostsCompare int)
begin
  
   declare ici_user_totalrowsnumber int; 
   declare ici_firstselectrownum int;
   declare literals0 varchar(500) DEFAULT CONCAT('%',LOWER(IFNULL(I_Literals,'')),'%');
   declare literals1 varchar(500) DEFAULT CONCAT(LOWER(IFNULL(I_Literals,'')),'%');
   declare litaralsAll varchar(4) default '%'; 
   declare ici_wildcard char(1) default '%' ;

/*   declare ici_firstselectrankid int;
   declare ici_firstselectlastvisit datetime;
   declare ici_firstselectjoined datetime;
   declare ici_firstselectposts int;
   declare ici_firstselectuserid int; */            

	--	SET @user_totalrowsnumber = @@ROWCOUNT    

   SET I_PageIndex = I_PageIndex+1;   
   SET ici_firstselectrownum = (I_PageIndex - 1) * I_PageSize + 1; 
   SET ici_firstselectrownum = ici_firstselectrownum -1 ;
	 SET  @ici_firstselectuserid = NULL; 
	 SET  @ici_firstselectlastvisit =NULL; 
	  SET @ici_firstselectjoined = NULL; 
	 SET  @ici_firstselectrankid = null; 
	 SET  @ici_firstselectposts	= NULL;
	  -- fined first selectedrowid  
	/*  if (ici_firstselectrownum > 0)
	  set rowcount ici_firstselectrownum
	  else
	  set rowcount	1 */

	SET @stmt_ulm_str = (CONCAT('select  a.Name, a.LastVisit,a.Joined,a.RankID , a.NumPosts
			   INTO @ici_firstselectuserid,  
			   @ici_firstselectlastvisit, 
			   @ici_firstselectjoined, 
			   @ici_firstselectrankid, 
			   @ici_firstselectposts			
	  from {databaseSchema}.{objectQualifier}User a 
	  join {databaseSchema}.{objectQualifier}Rank b 
	  on b.RankID=a.RankID 
	 where
	   a.BoardID = ',I_BoardID,'	   
	   and
		(',I_Approved IS NULL,' or (',IFNULL(I_Approved,2),' = 0 and (a.Flags & 2)=0) or (',IFNULL(I_Approved,0),' = 1 and (a.Flags & 2)=2)) and
		(',I_GroupID IS NULL,' or exists(select 1 from {databaseSchema}.{objectQualifier}UserGroup x where x.UserID=a.UserID 
		and x.GroupID=',IFNULL(I_GroupID,0),')) and
		(',IFNULL(I_RankID,0),' = 0 or a.RankID = ',IFNULL(I_RankID,0),') AND
		(a.Flags & 4) <> 4
			   AND     
		 LOWER(a.DisplayName) LIKE (CASE 
			WHEN (',I_BeginsWith,' = 0 AND ',I_Literals IS NOT NULL ,') THEN (''',literals0,''')  
			WHEN (',I_BeginsWith,' = 1 AND ',I_Literals IS NOT NULL ,') THEN (''',literals1,''')
			ELSE (''%'') END)           
	   and       
		(a.NumPosts >= (case 
		when ',I_NumPostsCompare,' = 3 then  ',I_NumPosts,' end) 
		OR a.NumPosts <= (case 
		when ',I_NumPostsCompare,' = 2 then ',I_NumPosts,' end) OR
		a.NumPosts = (case 
		when ',I_NumPostsCompare,' = 1 then ',I_NumPosts,' end)) 
	 order by  	 
		(case 
		when ',I_SortName,' = 2 then a.Name end) DESC,
		(case 
		when ',I_SortName,' = 1 then a.Name end) ASC, 
		(case 
		when ',I_SortRank,' = 2 then a.RankID end) DESC,
		(case 
		when ',I_SortRank,' = 1 then a.RankID end) ASC,		
		(case 
		when ',I_SortJoined,' = 2 then a.Joined end) DESC,
		(case 
		when ',I_SortJoined,' = 1 then a.Joined end) ASC,
		(case 
		when ',I_SortLastVisit,' = 2 then a.LastVisit end) DESC,
		(case 
		when ',I_SortLastVisit,' = 1 then a.LastVisit end) ASC,
		(case
		 when ',I_SortPosts,' = 2 then a.NumPosts end) DESC, 
		(case
		 when ',I_SortPosts,' = 1 then a.NumPosts end) ASC  LIMIT 1 OFFSET ',ici_firstselectrownum,';'));          
		PREPARE stmt_ulm FROM @stmt_ulm_str;
		 EXECUTE stmt_ulm;
		 DEALLOCATE PREPARE stmt_ulm; 

		 if (CHAR_LENGTH(@ici_firstselectuserid) > 0) THEN
  -- get total number of users in the db
   select CAST(count(a.UserID) AS SIGNED) INTO ici_user_totalrowsnumber
	from {databaseSchema}.{objectQualifier}User a  
	  join {databaseSchema}.{objectQualifier}Rank b 
	  on b.RankID=a.RankID 
	  where
	   a.BoardID = I_BoardID	   
	   and
		(I_Approved is null or (I_Approved=0 and (a.Flags & 2)<>2) or (I_Approved=1 and (a.Flags & 2)=2)) and
		(I_GroupID is null or exists(select 1 from {databaseSchema}.{objectQualifier}UserGroup x where x.UserID=a.UserID and x.GroupID=I_GroupID)) and
		(I_RankID is null or a.RankID=I_RankID) AND
		-- user is not guest
		IFNULL(a.Flags & 4,0) <> 4
			AND
		LOWER(a.DisplayName) LIKE CASE 
			WHEN (I_BeginsWith = 0 AND I_Literals IS NOT NULL AND CHAR_LENGTH(I_Literals) > 0) THEN CONCAT('%', LOWER(IFNULL(I_Literals,'')) , '%') 
			WHEN (I_BeginsWith = 1 AND I_Literals IS NOT NULL AND CHAR_LENGTH(I_Literals) > 0) THEN CONCAT(LOWER(IFNULL(I_Literals,'')), '%')
			ELSE '%' END  
		and
		(a.NumPosts >= (case 
		when I_NumPostsCompare = 3 then  I_NumPosts end) 
		OR a.NumPosts <= (case 
		when I_NumPostsCompare = 2 then I_NumPosts end) OR
		a.NumPosts = (case 
		when I_NumPostsCompare = 1 then I_NumPosts end)); 
		  PREPARE stlistmem FROM 
		  'select a.*,			
				  a.Culture AS CultureUser,
				  (select COUNT(1) from {databaseSchema}.{objectQualifier}UserGroup x 
				  join {databaseSchema}.{objectQualifier}Group y 
				  on y.GroupID=x.GroupID
				  where x.UserID=a.UserID 
				  and (y.Flags & 1)=1) AS IsAdmin,
				 IFNULL(a.Flags & 1,0) AS IsHostAdmin,
				 b.RankID,
				 b.Name AS RankName,
				 (case(?)
				 when 1 then  a.UserStyle
				 else '''' end) AS Style,
				{databaseSchema}.{objectQualifier}biginttoint(?) as TotalCount
				 from {databaseSchema}.{objectQualifier}User a 
				 join {databaseSchema}.{objectQualifier}Rank b on b.RankID=a.RankID	
	  where 
				  (a.Name >= (case 
								 when ? = 1 then ? end) OR a.Name <= (case 
								 when ? = 2 then ? end) OR
				   a.Name >= (case 
								 when ? = 0 then '''' end)) 
				 and
		(a.Joined >= (case 
		when ? = 1 then ? end) 
		OR a.Joined <= (case 
		when ? = 2 then ? end) OR
		a.Joined >= (case 
		when ? = 0 then 0 end)) and
		(a.LastVisit >= (case 
		when ? = 1 then  ? end) 
		OR a.LastVisit <= (case 
		when ? = 2 then ? end) OR
		a.LastVisit >= (case 
		when ? = 0 then 0 end))  and
		(a.NumPosts >= (case 
		when ? = 3 then  ? end) 
		OR a.NumPosts <= (case 
		when ? = 2 then ? end) OR
		a.NumPosts = (case 
		when ? = 1 then ? end))  and
		/*
		(a.NumPosts >= (case 
		when @SortPosts = 1 then @ici_firstselectposts end) 
		OR a.NumPosts <= (case 
		when @SortPosts = 2 then @ici_firstselectposts end) OR
		a.NumPosts >= (case 
		when @SortPosts = 0 then 0 end))   and
		(a.RankID >= (case 
		when @SortRank = 1 then @ici_firstselectrankid end) 
		OR a.RankID <= (case 
		when @SortRank = 2 then @ici_firstselectrankid end) OR
		a.RankID >= (case 
		when @SortRank = 0 then 0 end)) and */
	
			 a.BoardID = ? and
			(? is null or (?=0 and (a.Flags & 2)<>2) or (?=1 and (a.Flags & 2)=2)) and
			(? is null or exists(SELECT 1 FROM {databaseSchema}.{objectQualifier}UserGroup x 
			where x.UserID=a.UserID and x.GroupID=?)) and
			(? is null or a.RankID=?) AND
			IFNULL(a.Flags & 4,0) <> 4
			AND
			LOWER(a.DisplayName) LIKE 
			CASE 
			WHEN (? = 0 AND ? IS NOT NULL AND CHAR_LENGTH(?) > 0) THEN CONCAT(''%'', LOWER(?), ''%'') 
			WHEN (? = 1 AND ? IS NOT NULL AND CHAR_LENGTH(?) > 0) THEN CONCAT(LOWER(?), ''%'')
			ELSE ''%'' 
			END 
	ORDER BY 	
	 (case 
		when ? = 2 then a.Name end) DESC,
		(case 
		when ? = 1 then a.Name end) ASC, 
		(case 
		when ? = 2 then a.RankID end) DESC,
		(case 
		when ? = 1 then a.RankID end) ASC,		
		(case 
		when ? = 2 then a.Joined end) DESC,
		(case 
		when ? = 1 then a.Joined end) ASC,
		(case 
		when ? = 2 then a.LastVisit end) DESC,
		(case 
		when ? = 1 then a.LastVisit end) ASC,
		(case
		 when ? = 2 then a.NumPosts end) DESC, 
		(case
		 when ? = 1 then a.NumPosts end) ASC LIMIT ?';

		 SET @PI_StyledNicks = I_StyledNicks;
		 SET @PI_SortName = I_SortName;
		 SET @pici_user_totalrowsnumber = ici_user_totalrowsnumber;
		 SET @pici_firstselectuserid = @ici_firstselectuserid;
		 SET @PI_SortJoined = I_SortJoined ;
		 SET @pici_firstselectjoined =  @ici_firstselectjoined;
		 SET @PI_SortLastVisit = I_SortLastVisit;
		 SET @pici_firstselectlastvisit = @ici_firstselectlastvisit; 
		 SET @PI_NumPostsCompare = I_NumPostsCompare;
		 SET @PI_NumPosts = I_NumPosts;
		 SET @PI_BoardID = I_BoardID;
		 SET @PI_Approved = I_Approved;
		 SET @PI_GroupID = I_GroupID;
		 SET @PI_RankID = I_RankID;
		 SET @PI_BeginsWith = I_BeginsWith;
		 SET @PI_Literals = I_Literals;
		 SET @PI_Literals = I_Literals;		
		 SET @PI_BeginsWith = I_BeginsWith;
		 SET @PI_Literals = I_Literals;
		 SET @PI_Literals = I_Literals;
		 SET @PI_Literals = I_Literals;
		 SET @PI_SortPosts = I_SortPosts;
		 SET @PI_SortRank = I_SortRank;
		 SET @PI_PageSize = I_PageSize;
		

 EXECUTE stlistmem USING 
 @PI_StyledNicks, 
 @pici_user_totalrowsnumber, 
 -- where
 @PI_SortName, 
 @pici_firstselectuserid, 
 @PI_SortName, 
 @pici_firstselectuserid,
 @PI_SortName,
 @PI_SortJoined,
 @pici_firstselectjoined,
 @PI_SortJoined,
 @pici_firstselectjoined,
 @PI_SortJoined,
 @PI_SortLastVisit,
 @pici_firstselectlastvisit,
 @PI_SortLastVisit,
 @pici_firstselectlastvisit,
 @PI_SortLastVisit,
 @PI_NumPostsCompare,
 @PI_NumPosts,
 @PI_NumPostsCompare,
 @PI_NumPosts,
 @PI_NumPostsCompare,
 @PI_NumPosts,
 -- where boardid section 
 @PI_BoardID,
 @PI_Approved,
 @PI_Approved,
 @PI_Approved,

 @PI_GroupID,
 @PI_GroupID,
 @PI_RankID,
 @PI_RankID,
 @PI_BeginsWith,
 @PI_Literals,
 @PI_Literals,
 @PI_Literals,
 @PI_BeginsWith,
 @PI_Literals,
 @PI_Literals,
 @PI_Literals,
 -- order by
 @PI_SortName,
 @PI_SortName,
 @PI_SortRank,
 @PI_SortRank,
 @PI_SortJoined,
 @PI_SortJoined,
 @PI_SortLastVisit,
 @PI_SortLastVisit,
 @PI_SortPosts,
 @PI_SortPosts,
 -- limit
 @PI_PageSize;
 DEALLOCATE PREPARE stlistmem;
 else
 select a.*,			
				 a.Culture AS CultureUser,
				 0 AS IsAdmin,
				 0 AS IsHostAdmin,
				 1,
				 '' AS RankName,
				 '' AS Style,
				 0 as TotalCount
				 from {databaseSchema}.{objectQualifier}User a 
	  where a.UserID = -1;
 end if;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}user_listmembers_result(
				I_PageIndex int,
				I_UserID int,				
				I_PageSize int,
				I_StyledNicks tinyint(1),
				I_SortName int,	
				I_SortJoined int,
				I_SortLastVisit int,
				I_NumPostsCompare int,
				I_NumPosts int,
				I_BoardID int,
				I_Approved  tinyint(1),
				I_GroupID int,
				I_RankID int,
				I_BeginsWith  tinyint(1), 
				I_Literals VARCHAR(255), 
				I_SortRank int,			
				I_SortPosts int,
				I_Exclude  tinyint(1),		
				i_firstselectuserid VARCHAR(255),
				i_firstselectlastvisit datetime,
				i_firstselectjoined datetime,
				i_firstselectrankid int,
				i_firstselectposts  int)
begin
  
   declare ici_user_totalrowsnumber int default 0; 
   declare ici_firstselectrownum int; 
   declare ici_wildcard char(1) default '%' ;
 if (CHAR_LENGTH(i_firstselectuserid) > 0) THEN
  -- get total number of users in the db
   select CAST(count(a.UserID) AS SIGNED) INTO ici_user_totalrowsnumber
	from {databaseSchema}.{objectQualifier}User a  
	  join {databaseSchema}.{objectQualifier}Rank b 
	  on b.RankID=a.RankID 
	  where
	   a.BoardID = I_BoardID	   
	   and
		(I_Approved is null or (I_Approved=0 and (a.Flags & 2)<>2) or (I_Approved=1 and (a.Flags & 2)=2)) and
		(I_GroupID is null or exists(select 1 from {databaseSchema}.{objectQualifier}UserGroup x where x.UserID=a.UserID and x.GroupID=I_GroupID)) and
		(I_RankID is null or a.RankID=I_RankID) AND
		-- user is not guest
		IFNULL(a.Flags & 4,0) <> 4
			AND
		LOWER(a.DisplayName) LIKE CASE 
			WHEN (I_BeginsWith = 0 AND I_Literals IS NOT NULL AND CHAR_LENGTH(I_Literals) > 0) THEN CONCAT('%', LOWER(IFNULL(I_Literals,'')) , '%') 
			WHEN (I_BeginsWith = 1 AND I_Literals IS NOT NULL AND CHAR_LENGTH(I_Literals) > 0) THEN CONCAT(LOWER(IFNULL(I_Literals,'')), '%')
			ELSE '%' END  
		and
		(a.NumPosts >= (case 
		when I_NumPostsCompare = 3 then  I_NumPosts end) 
		OR a.NumPosts <= (case 
		when I_NumPostsCompare = 2 then I_NumPosts end) OR
		a.NumPosts = (case 
		when I_NumPostsCompare = 1 then I_NumPosts end)); 

	-- set rowcount I_PageSize
	-- else
	-- set rowcount 10
	
	 PREPARE stlistmem FROM 
		  'select a.*,			
				  a.Culture AS CultureUser,
				  (select COUNT(1) from {databaseSchema}.{objectQualifier}UserGroup x 
				  join {databaseSchema}.{objectQualifier}Group y 
				  on y.GroupID=x.GroupID
				  where x.UserID=a.UserID 
				  and (y.Flags & 1)=1) AS IsAdmin,
				 IFNULL(a.Flags & 1,0) AS IsHostAdmin,
				 b.RankID,
				 b.Name AS RankName,
				 (case(?)
				 when 1 then  a.UserStyle
				 else '''' end) AS Style,
				{databaseSchema}.{objectQualifier}biginttoint(?) as TotalCount
				 from {databaseSchema}.{objectQualifier}User a 
				 join {databaseSchema}.{objectQualifier}Rank b on b.RankID=a.RankID	
	  where 
				  (a.Name >= (case 
								 when ? = 1 then ? end) OR a.Name <= (case 
								 when ? = 2 then ? end) OR
				   a.Name >= (case 
								 when ? = 0 then '''' end)) 
				 and
		(a.Joined >= (case 
		when ? = 1 then ? end) 
		OR a.Joined <= (case 
		when ? = 2 then ? end) OR
		a.Joined >= (case 
		when ? = 0 then 0 end)) and
		(a.LastVisit >= (case 
		when ? = 1 then  ? end) 
		OR a.LastVisit <= (case 
		when ? = 2 then ? end) OR
		a.LastVisit >= (case 
		when ? = 0 then 0 end))  and
		(a.NumPosts >= (case 
		when ? = 3 then  ? end) 
		OR a.NumPosts <= (case 
		when ? = 2 then ? end) OR
		a.NumPosts = (case 
		when ? = 1 then ? end))  and
		/*
		(a.NumPosts >= (case 
		when @SortPosts = 1 then @firstselectposts end) 
		OR a.NumPosts <= (case 
		when @SortPosts = 2 then @firstselectposts end) OR
		a.NumPosts >= (case 
		when @SortPosts = 0 then 0 end))   and
		(a.RankID >= (case 
		when @SortRank = 1 then @firstselectrankid end) 
		OR a.RankID <= (case 
		when @SortRank = 2 then @firstselectrankid end) OR
		a.RankID >= (case 
		when @SortRank = 0 then 0 end)) and */
	
			 a.BoardID = ? and
			(? is null or (?=0 and (a.Flags & 2)<>2) or (?=1 and (a.Flags & 2)=2)) and
			(? is null or exists(SELECT 1 FROM {databaseSchema}.{objectQualifier}UserGroup x 
			where x.UserID=a.UserID and x.GroupID=?)) and
			(? is null or a.RankID=?) AND
			IFNULL(a.Flags & 4,0) <> 4
			AND
			LOWER(a.DisplayName) LIKE 
			CASE 
			WHEN (? = 0 AND ? IS NOT NULL AND CHAR_LENGTH(?) > 0) THEN CONCAT(''%'', LOWER(?), ''%'') 
			WHEN (? = 1 AND ? IS NOT NULL AND CHAR_LENGTH(?) > 0) THEN CONCAT(LOWER(?), ''%'')
			ELSE ''%'' 
			END 
	ORDER BY 	
	 (case 
		when ? = 2 then a.Name end) DESC,
		(case 
		when ? = 1 then a.Name end) ASC, 
		(case 
		when ? = 2 then a.RankID end) DESC,
		(case 
		when ? = 1 then a.RankID end) ASC,		
		(case 
		when ? = 2 then a.Joined end) DESC,
		(case 
		when ? = 1 then a.Joined end) ASC,
		(case 
		when ? = 2 then a.LastVisit end) DESC,
		(case 
		when ? = 1 then a.LastVisit end) ASC,
		(case
		 when ? = 2 then a.NumPosts end) DESC, 
		(case
		 when ? = 1 then a.NumPosts end) ASC LIMIT ?';

		 SET @PI_StyledNicks = I_StyledNicks;
		 SET @PI_SortName = I_SortName;
		 SET @pici_user_totalrowsnumber = ici_user_totalrowsnumber;
		 SET @pici_firstselectuserid = i_firstselectuserid;
		 SET @PI_SortJoined = I_SortJoined ;
		 SET @pici_firstselectjoined = i_firstselectjoined;
		 SET @PI_SortLastVisit = I_SortLastVisit;
		 SET @pici_firstselectlastvisit = i_firstselectlastvisit; 
		 SET @PI_NumPostsCompare = I_NumPostsCompare;
		 SET @PI_NumPosts = I_NumPosts;
		 SET @PI_BoardID = I_BoardID;
		 SET @PI_Approved = I_Approved;
		 SET @PI_GroupID = I_GroupID;
		 SET @PI_RankID = I_RankID;
		 SET @PI_BeginsWith = I_BeginsWith;
		 SET @PI_Literals = I_Literals;
		 SET @PI_Literals = I_Literals;		
		 SET @PI_BeginsWith = I_BeginsWith;
		 SET @PI_Literals = I_Literals;
		 SET @PI_Literals = I_Literals;
		 SET @PI_Literals = I_Literals;
		 SET @PI_SortPosts = I_SortPosts;
		 SET @PI_SortRank = I_SortRank;
		 SET @PI_PageSize = I_PageSize;
		

 EXECUTE stlistmem USING 
 @PI_StyledNicks, 
 @pici_user_totalrowsnumber, 
 -- where
 @PI_SortName, 
 @pici_firstselectuserid, 
 @PI_SortName, 
 @pici_firstselectuserid,
 @PI_SortName,
 @PI_SortJoined,
 @pici_firstselectjoined,
 @PI_SortJoined,
 @pici_firstselectjoined,
 @PI_SortJoined,
 @PI_SortLastVisit,
 @pici_firstselectlastvisit,
 @PI_SortLastVisit,
 @pici_firstselectlastvisit,
 @PI_SortLastVisit,
 @PI_NumPostsCompare,
 @PI_NumPosts,
 @PI_NumPostsCompare,
 @PI_NumPosts,
 @PI_NumPostsCompare,
 @PI_NumPosts,
 -- where boardid section 
 @PI_BoardID,
 @PI_Approved,
 @PI_Approved,
 @PI_Approved,

 @PI_GroupID,
 @PI_GroupID,
 @PI_RankID,
 @PI_RankID,
 @PI_BeginsWith,
 @PI_Literals,
 @PI_Literals,
 @PI_Literals,
 @PI_BeginsWith,
 @PI_Literals,
 @PI_Literals,
 @PI_Literals,
 -- order by
 @PI_SortName,
 @PI_SortName,
 @PI_SortRank,
 @PI_SortRank,
 @PI_SortJoined,
 @PI_SortJoined,
 @PI_SortLastVisit,
 @PI_SortLastVisit,
 @PI_SortPosts,
 @PI_SortPosts,
 -- limit
 @PI_PageSize;
 DEALLOCATE PREPARE stlistmem;
 else
 select a.*,			
				 a.Culture AS CultureUser,
				 0 AS IsAdmin,
				 0 AS IsHostAdmin,
				 1,
				 '' AS RankName,
				 '' AS Style,
				 0 as TotalCount
				 from {databaseSchema}.{objectQualifier}User a 
	  where a.UserID = -1;
 end if;
		  
end;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_list
(i_BoardID INT,i_UserID INT,i_Approved TINYINT(1),i_GroupID INT,i_RankID INT, i_StyledNicks TINYINT(1),
	i_UTCTIMESTAMP DATETIME) 
BEGIN
	IF i_UserID IS NOT NULL THEN
		SELECT
			  a.`UserID`,
			  a.`BoardID`,
			  a.`ProviderUserKey`,
			  a.`Name`,
			  a.`DisplayName`,
			  a.`Password`,
			  a.`Email`,
			  a.`Joined`,
			  a.`LastVisit`,
			  a.`IP`,
			  a.`NumPosts`,
			  a.`TimeZone`,
			  a.`UseSingleSignOn`,
			  a.`TextEditor`,
			  a.`Avatar`,
			  a.`Signature`,
			  a.`AvatarImage`,
			  a.`AvatarImageType`,
			  a.`RankID`,
			  a.`Suspended`,
			  a.`LanguageFile`,
			  a.`ThemeFile`,
			  a.`OverrideDefaultThemes`,
			  a.`PMNotification`,
			  a.`NotificationType`,
			  a.`DailyDigest`,
			  a.`AutoWatchTopics`,
			  a.`Culture`,
			  a.`Flags`,
			  a.`Points`,
			  a.`IsFacebookUser`,
			  a.`IsTwitterUser`,
			  a.Culture AS CultureUser,
			  b.Name AS RankName,
			(case(i_StyledNicks)
			  when 1 then  a.UserStyle
			else ''	 end) AS Style,  			
			-- {databaseSchema}.{objectQualifier}biginttoint(IFNULL(a.Flags & 4,0)) AS IsGuest,
			DATEDIFF(i_UTCTIMESTAMP,a.Joined)+1 AS NumDays,
			(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x WHERE (x.Flags & 24)=16) AS NumPostsForum,
			SIGN((SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}User x 
						  WHERE x.UserID=a.UserID 
							AND AvatarImage IS NOT NULL LIMIT 1)) AS HasAvatarImage, 
			{databaseSchema}.{objectQualifier}biginttoint(IFNULL(c.IsAdmin,0)) AS IsAdmin,
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 4,0)) AS IsGuest,
			{databaseSchema}.{objectQualifier}biginttoint(IFNULL(a.Flags & 1,0)) AS IsHostAdmin,
			IFNULL(c.IsForumModerator,0) AS IsForumModerator,
			IFNULL(c.IsModerator,0) AS IsModerator,
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 2,0)) AS IsApproved,
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 16,0)) AS IsActiveExcluded,
			a.`TopicsPerPage`,
			a.`PostsPerPage`										
		FROM 
			{databaseSchema}.{objectQualifier}User a
			JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID=a.RankID
			left join {databaseSchema}.{objectQualifier}ActiveAccess c on c.UserID=a.UserID
		WHERE 
			a.UserID = i_UserID AND
			a.BoardID = i_BoardID AND
			(i_Approved IS NULL OR (i_Approved=0 AND (a.Flags & 2)=0) 
							OR (i_Approved=1 and (a.Flags & 2)=2))
		ORDER BY 
			a.Name;

	ELSEIF i_GroupID IS NULL and i_RankID IS NULL THEN
		SELECT 
			a.*,  
			a.Culture AS CultureUser,						
			b.Name AS RankName,
			(case(i_StyledNicks)
			  when 1 then  a.UserStyle
			else ''	 end) AS Style,
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 4,0)) AS IsGuest, 
			IFNULL(a.Flags & 1,0) AS IsHostAdmin,                     
			-- {databaseSchema}.{objectQualifier}biginttoint(IFNULL(a.Flags & 4,0)) AS IsGuest, 			
			(SELECT COUNT(1) from {databaseSchema}.{objectQualifier}UserGroup x
						   JOIN {databaseSchema}.{objectQualifier}Group y 
							 ON y.GroupID=x.GroupID 
							   WHERE x.UserID=a.UserID
								 AND (y.Flags & 1)<>0) AS IsAdmin,
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 2,0)) AS IsApproved,
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 16,0)) AS IsActiveExcluded																											      		 			
			
		FROM 
			{databaseSchema}.{objectQualifier}User a
			JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID=a.RankID
		WHERE 
			a.BoardID = i_BoardID 
						  AND (i_Approved IS NULL 
						   OR (i_Approved=0 AND (a.Flags & 2)=0) 
							 OR (i_Approved=1 AND (a.Flags & 2)=2))
		ORDER BY 
			a.Name;
	ELSE
		SELECT 
			a.*,
			a.Culture AS CultureUser,				
			b.Name AS RankName,	
			(case(i_StyledNicks)
			  when 1 then  a.UserStyle
			else ''	 end) AS Style, 
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 4,0)) AS IsGuest,
			IFNULL(a.Flags & 1,0) AS IsHostAdmin,            
			-- {databaseSchema}.{objectQualifier}biginttoint(IFNULL(a.Flags & 4,0)) AS IsGuest,	
			(SELECT COUNT(1) from {databaseSchema}.{objectQualifier}UserGroup x 
						JOIN {databaseSchema}.{objectQualifier}Group y ON y.GroupID=x.GroupID 
						WHERE x.UserID=a.UserID 
						AND (y.Flags & 1)<>0) AS IsAdmin,
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 2,0)) AS IsApproved,
			{databaseSchema}.{objectQualifier}biginttobool (IFNULL(a.Flags & 16,0)) AS IsActiveExcluded																								 			 			
			
		FROM 
			{databaseSchema}.{objectQualifier}User a 
			JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID=a.RankID
		WHERE 
			a.BoardID = i_BoardID and
			(i_Approved IS NULL 
						  OR (i_Approved=0 AND (a.Flags & 2) != 2) 
						  OR (i_Approved=1 AND (a.Flags & 2)=2)) 
						  AND (i_GroupID IS NULL OR EXISTS
							  (SELECT 1 FROM {databaseSchema}.{objectQualifier}UserGroup x 
								 WHERE x.UserID=a.UserID AND x.GroupID=i_GroupID)) 
								  AND (i_RankID IS NULL OR a.RankID=i_RankID)
		ORDER BY 
			a.Name;
		 END IF;
END;
--GO

create procedure {databaseSchema}.{objectQualifier}admin_list(i_BoardID int, i_StyledNicks tinyint(1),
	i_UTCTIMESTAMP DATETIME) 
begin
		 select 
			a.*,
			b.Name AS BoardName,			
			a.Culture AS CultureUser,			
			r.RankID,						
			r.Name AS RankName,
			(case(i_StyledNicks)
			when 1 then  a.UserStyle
			else ''	 end) AS Style,  
			(DATEDIFF(a.Joined, i_UTCTIMESTAMP)+1) AS NumDays,
			(select count(1) from {databaseSchema}.{objectQualifier}Message x where (x.Flags & 24)=16) as NumPostsForum,
			(select count(1) from {databaseSchema}.{objectQualifier}User x 
			where x.UserID=a.UserID and AvatarImage is not null) AS HasAvatarImage,
			IfNull(c.IsAdmin,0) AS IsAdmin,			
			IfNull(a.Flags & 1,0) AS IsHostAdmin
		from 
			{databaseSchema}.{objectQualifier}User a
			JOIN {databaseSchema}.{objectQualifier}Board b	
			ON b.BoardID = a.BoardID	
			JOIN {databaseSchema}.{objectQualifier}Rank r	
			ON r.RankID = a.RankID		
			left join {databaseSchema}.{objectQualifier}vaccess c on c.UserID=a.UserID
		where 			
			(i_BoardID IS NULL OR a.BoardID = i_BoardID) and
			-- is not guest 
			IfNull(a.Flags & 4,0) = 0 and
			c.ForumID = 0 and
			-- is admin 
			(IfNull(c.IsAdmin,0) <> 0) 
		order by 
			a.DisplayName;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}admin_pageaccesslist(i_BoardID int, i_StyledNicks tinyint(1),i_UTCTIMESTAMP datetime) 
begin
		 select 
		a.UserID,
		a.BoardID,
		b.Name AS BoardName,
		a.Name,
		a.DisplayName,
		a.Culture,
			a.NumPosts,
			a.Culture AS CultureUser,
			(case(i_StyledNicks)
			when 1 then  a.UserStyle
			else ''	 end) as Style
		from 
			{databaseSchema}.{objectQualifier}User a
			JOIN
			{databaseSchema}.{objectQualifier}Board b	
			ON b.BoardID = a.BoardID			
			left join {databaseSchema}.{objectQualifier}vaccess c 
			on c.UserID=a.UserID
		where 			
			(i_BoardID IS NULL OR a.BoardID = i_BoardID) and
			-- is admin 
			(IfNull(c.IsAdmin,0) <> 0) and
			c.ForumID = 0 and 			
			-- is not host admin 
			IfNull(a.Flags & 1,0) = 0 
		order by 
			a.DisplayName;
end;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_login(i_BoardID INT,i_Name VARCHAR(128),i_Password VARCHAR(32))
BEGIN
	DECLARE ici_UserID INT;
 
	/*Try correct board first*/
	IF EXISTS(SELECT UserID FROM {databaseSchema}.{objectQualifier}User WHERE `Name`=i_Name AND Password=i_Password AND BoardID=i_BoardID and (Flags & 2)=2) THEN
	
		SELECT UserID  FROM {databaseSchema}.{objectQualifier}User WHERE `Name`=i_Name and Password=i_Password AND BoardID=i_BoardID and (Flags & 2)=2;
		
	ELSE
 
	IF NOT EXISTS(select UserID FROM {databaseSchema}.{objectQualifier}User WHERE `Name`=i_Name AND Password=i_Password AND (BoardID=i_BoardID OR (Flags & 3)=3)) THEN
		SELECT NULL AS UserID;
				 
	ELSE
		SELECT 
			 UserID 
		FROM 
			{databaseSchema}.{objectQualifier}User
		WHERE 
			`Name`=i_Name AND
			Password=i_Password AND 
			(BoardID=i_BoardID OR (Flags & 1)=1) AND
			(Flags & 2)=2; 
	
		END IF;
		END IF; 
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_medal_delete
	(i_UserID INT,
	i_MedalID INT)
BEGIN 
 
	DELETE FROM {databaseSchema}.{objectQualifier}UserMedal WHERE UserID=i_UserID AND MedalID=i_MedalID;
 
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_medal_list(i_UserID INT,
	i_MedalID INT, I_UTCTIMESTAMP DATETIME)
BEGIN 
	SELECT 
		a.MedalID,
		a.Name,
		a.MedalURL,
		a.RibbonURL,
		a.SmallMedalURL,
		a.SmallRibbonURL,
		a.SmallMedalWidth,
		a.SmallMedalHeight,
		a.SmallRibbonWidth,
		a.SmallRibbonHeight,
		b.SortOrder,
		a.Flags,
		c.Name as UserName,
		c.DisplayName,
		b.UserID,
		IFNULL(b.Message,a.Message) as Message,
		b.Message as MessageEx,
		b.Hide,
		b.OnlyRibbon,
		b.SortOrder as CurrentSortOrder,
		b.DateAwarded
	FROM
		{databaseSchema}.{objectQualifier}Medal a
		INNER JOIN {databaseSchema}.{objectQualifier}UserMedal b ON b.MedalID = a.MedalID
		INNER JOIN {databaseSchema}.{objectQualifier}User c ON c.UserID = b.UserID
	WHERE
		(i_UserID IS NULL OR b.UserID = i_UserID) AND
		(i_MedalID IS NULL OR b.MedalID = i_MedalID)		
	ORDER BY
		c.Name ASC,
		b.SortOrder ASC;
 
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_medal_save(
	i_UserID INT,
	i_MedalID INT,
	i_Message VARCHAR(128),
	i_Hide TINYINT(1),
	i_OnlyRibbon TINYINT(1),
	i_SortOrder TINYINT(3),
	i_DateAwarded DATETIME,
	i_UTCTIMESTAMP DATETIME)
BEGIN
 
	IF EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}UserMedal WHERE UserID=i_UserID AND MedalID=i_MedalID) THEN
		UPDATE {databaseSchema}.{objectQualifier}UserMedal
		SET
			Message = i_Message,
			Hide = i_Hide,
			OnlyRibbon = i_OnlyRibbon,
			SortOrder = i_SortOrder
		WHERE
			UserID=i_UserID AND 
			MedalID=i_MedalID;
	
	ELSE 
 
		IF (i_DateAwarded IS NULL) THEN SET i_DateAwarded = i_UTCTIMESTAMP; END IF;
 
		INSERT INTO {databaseSchema}.{objectQualifier}UserMedal(UserID,MedalID,Message,Hide,OnlyRibbon,SortOrder,DateAwarded)
		VALUES
			(i_UserID,i_MedalID,i_Message,i_Hide,i_OnlyRibbon,i_SortOrder,i_DateAwarded);
	END IF; 
 
END;
--GO

	 /* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_nntp(i_BoardID INT,i_UserName VARCHAR(128),i_Email VARCHAR(128),i_TimeZone int, i_UTCTIMESTAMP datetime) 
BEGIN
DECLARE icic_UserID INT DEFAULT 0; 
DECLARE icic_cntr INT;  
SET i_UserName = CONCAT(i_UserName,' ','NNTP');
SELECT qqq.UserID,COUNT(qqq.UserID) INTO  icic_UserID, icic_cntr FROM {databaseSchema}.{objectQualifier}User qqq 
WHERE qqq.BoardID=i_BoardID AND qqq.Name =i_UserName;
/*SET icic_cntr = FOUND_ROWS();
SELECT
		UserID INTO  icic_UserID
		FROM
		{databaseSchema}.{objectQualifier}User
		WHERE
		BoardID=i_BoardID 
		AND `Name`=i_UserName LIMIT 1;*/
		
 IF icic_cntr < 1 OR icic_cntr IS NULL THEN 
		CALL {databaseSchema}.{objectQualifier}user_save(
		null,i_BoardID,
		i_UserName,
		i_UserName,i_Email,i_TimeZone,null,null,null,0, null, 0, 1, 0, 0,null,0,0,0,10,10,i_UTCTIMESTAMP); 		
		SELECT MAX(UserID) INTO icic_UserID FROM {databaseSchema}.{objectQualifier}User;
END IF;		
	SELECT icic_UserID AS UserID;		
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_pmcount
	(i_UserID INT)
 BEGIN 	
	
		DECLARE ici_CountIn int;	
		DECLARE ici_CountOut int;
		DECLARE ici_CountArchivedIn int;
		DECLARE ici_plimit1 int;       
		DECLARE ici_pcount int default 0;
		
	  set ici_plimit1 = (SELECT  c.PMLimit FROM {databaseSchema}.{objectQualifier}User a 
						JOIN {databaseSchema}.{objectQualifier}UserGroup b
						  ON a.UserID = b.UserID
							JOIN {databaseSchema}.{objectQualifier}Group c                         
							  ON b.GroupID = c.GroupID WHERE a.UserID = i_UserID ORDER BY c.PMLimit DESC LIMIT 1);
	  set ici_pcount = (SELECT  c.PMLimit FROM {databaseSchema}.{objectQualifier}Rank c 
						JOIN {databaseSchema}.{objectQualifier}User d
						   ON c.RankID = d.RankID WHERE d.UserID = i_UserID ORDER BY c.PMLimit DESC LIMIT 1);
	  if (ici_plimit1 > ici_pcount)  THEN     
	  set ici_pcount = ici_plimit1;      
	  end if; 
	  
	-- get count of pm's in user's sent items
	
	SELECT 
	COUNT(1) INTO	ici_CountOut 
	FROM 
		{databaseSchema}.{objectQualifier}UserPMessage a
	INNER JOIN {databaseSchema}.{objectQualifier}PMessage b 
	ON a.PMessageID=b.PMessageID
	WHERE 
		IsInOutBox <> 0 AND
		b.FromUserID = i_UserID;
	-- get count of pm's in user's received items
	SELECT 
	 COUNT(1) INTO	ici_CountIn  
	FROM 
		{databaseSchema}.{objectQualifier}UserPMessage 
	WHERE 
		UserID = i_UserID
		AND IsDeleted = 0 AND IsArchived = 0;
		-- archived pmessages
	SELECT 
		COUNT(1) INTO ici_CountArchivedIn
	FROM 
		{databaseSchema}.{objectQualifier}UserPMessage a
		WHERE
		a.IsArchived <> 0 AND
		a.UserID = i_UserID;	

	-- return all pm data
	SELECT 
		ici_CountIn AS NumberIn,
		ici_CountOut AS NumberOut,
		ici_CountArchivedIn AS NumberArchived,
		ici_CountIn + ici_CountOut + ici_CountArchivedIn AS NumberTotal,
		ici_pcount AS NumberAllowed;
END;
--GO

/* XXX STORED PROCEDURE CREATED BY VZ-TEAM XXX */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_recoverpassword(i_BoardID INT,i_UserName VARCHAR(128),i_Email VARCHAR(128)) 
BEGIN
	DECLARE ici_UserID INT;
	SELECT  UserID INTO ici_UserID FROM {databaseSchema}.{objectQualifier}User 
		  WHERE BoardID = i_BoardID AND `Name` = i_UserName and Email = i_Email;
	IF ici_UserID IS NULL THEN
		SELECT CONVERT(null,SIGNED) AS UserID;
		/*return*/
	ELSE 	
		SELECT  ici_UserID AS UserID;
	END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
	 CREATE  PROCEDURE {databaseSchema}.{objectQualifier}user_removepoints(
	 i_UserID INT,i_FromUserID INT, i_UTCTIMESTAMP datetime,i_Points int)
	 BEGIN
	 declare i_VoteDate datetime;
	 UPDATE {databaseSchema}.{objectQualifier}User
	 SET    Points = Points + i_Points
	 WHERE  UserID = i_UserID;
	 IF i_FromUserID IS NOT NULL THEN		
	set i_VoteDate = (select VoteDate from {databaseSchema}.{objectQualifier}ReputationVote where ReputationFromUserID=i_FromUserID AND ReputationToUserID=i_UserID LIMIT 1);
	IF i_VoteDate is not null then    
		  update {databaseSchema}.{objectQualifier}ReputationVote set VoteDate=i_UTCTIMESTAMP where VoteDate = i_VoteDate AND ReputationFromUserID=i_FromUserID AND ReputationToUserID=i_UserID;	
	ELSE	 
		  insert into {databaseSchema}.{objectQualifier}ReputationVote(ReputationFromUserID,ReputationToUserID,VoteDate)
		  values (i_FromUserID, i_UserID, i_UTCTIMESTAMP);	 
	END IF;
	END IF;
	END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_savenotification(
	i_UserID				int,
	i_PMNotification		TINYINT(1),
	i_AutoWatchTopics    TINYINT(1),
	i_NotificationType	INT,
	i_DailyDigest		TINYINT(1)
)
BEGIN

		UPDATE
			{databaseSchema}.{objectQualifier}User
		SET
			PMNotification = (CASE WHEN (i_PMNotification is not null) THEN  i_PMNotification ELSE PMNotification END),
			AutoWatchTopics = (CASE WHEN (i_AutoWatchTopics is not null) THEN  i_AutoWatchTopics ELSE AutoWatchTopics END),
			NotificationType =  (CASE WHEN (i_NotificationType is not null) THEN  i_NotificationType ELSE NotificationType END),
			DailyDigest = (CASE WHEN (i_DailyDigest is not null) THEN  i_DailyDigest ELSE DailyDigest END)
		WHERE
			UserID = i_UserID;
END
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_resetpoints ()
BEGIN
	UPDATE {databaseSchema}.{objectQualifier}User SET Points = NumPosts * 3;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */ 
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_save(
	i_UserID		        INT,
	i_BoardID		        INT,
	i_UserName		        VARCHAR(128),
	i_DisplayName		    VARCHAR(128),
	i_Email			        VARCHAR(128),
	i_TimeZone		        INT,
	i_LanguageFile	        VARCHAR(128),
	i_Culture		        VARCHAR(10),
	i_ThemeFile		        VARCHAR(128),
	i_UseSingleSignOn       TINYINT(1),
	i_TextEditor			VARCHAR(50),
	i_OverrideDefaultTheme	TINYINT(1),
	i_Approved		        TINYINT(1),
	i_PMNotification		TINYINT(1),
	i_NotificationType	    INT,
	i_ProviderUserKey	    VARCHAR(64),
	i_AutoWatchTopics		TINYINT(1),
	i_DSTUser               TINYINT(1),
	i_HideUser              TINYINT(1),
	i_TopicsPerPage         INT,
	i_PostsPerPage          INT,
	i_UTCTIMESTAMP DATETIME)

BEGIN
	DECLARE ici_RankID INT;
	DECLARE ici_Flags INT DEFAULT 0;
	DECLARE ici_OldDisplayName VARCHAR(255);
	
					 if i_DSTUser is null 
					  THEN 
						 SET i_DSTUser = 0;
					  END IF;
					 if i_HideUser is null
					 THEN 
						 SET i_HideUser = 0;
					  END IF;
	
					 IF i_PMNotification IS NULL 
					 THEN 
						SET i_PMNotification = 1; 
					 END IF;
					 IF i_OverrideDefaultTheme IS NULL 
					 THEN
						SET i_OverrideDefaultTheme=0;
					 END IF;
						IF i_UseSingleSignOn IS NULL 
					 THEN
						SET i_UseSingleSignOn = 0;
					 END IF;
				  
 
	IF (i_UserID IS NULL OR i_UserID < 1) THEN
	-- begin it
	IF i_Approved<>0 THEN 
	  SET ici_Flags = ici_Flags | 2; 
		END IF;
		IF (i_Email = '') 
		  THEN 
		   SET i_Email = null; 
			END IF;
		
		SELECT RankID 
		INTO ici_RankID 
		FROM {databaseSchema}.{objectQualifier}Rank 
		WHERE (Flags & 1)<>0 AND BoardID=i_BoardID;
 
		INSERT INTO {databaseSchema}.{objectQualifier}User(BoardID,RankID,`Name`,DisplayName,Password,Email,Joined,LastVisit,NumPosts,TimeZone,Flags,PMNotification,NotificationType,ProviderUserKey,AutoWatchTopics,TopicsPerPage,PostsPerPage) 
		  VALUES(i_BoardID,ici_RankID,i_UserName,i_DisplayName,'-',i_Email,i_UTCTIMESTAMP,i_UTCTIMESTAMP,0,i_TimeZone,ici_Flags,i_PMNotification,i_NotificationType,i_ProviderUserKey,i_AutoWatchTopics,i_TopicsPerPage,i_PostsPerPage);		
	
		SET i_UserID = LAST_INSERT_ID();
 
		INSERT INTO {databaseSchema}.{objectQualifier}UserGroup(UserID,GroupID) 
		SELECT i_UserID AS UserID,
		GroupID 
		FROM {databaseSchema}.{objectQualifier}Group 
		where BoardID=i_BoardID and (Flags & 4)<>0;
		-- else condition
	ELSE
		SELECT Flags, DisplayName INTO ici_Flags,ici_OldDisplayName FROM {databaseSchema}.{objectQualifier}User where UserID = i_UserID;	
		-- isdirty flag -set only		
		IF ((ici_flags & 64) <> 64) then		
		SET ici_flags = ici_flags | 64;
		end if;

		 IF ((i_DSTUser<>0) AND ((ici_Flags & 32) <> 32)) 	
		THEN	
		SET ici_Flags = ici_Flags | 32;
		ELSEIF ((i_DSTUser=0) AND ((ici_Flags & 32) = 32)) 
		THEN
		SET ici_Flags = ici_Flags ^ 32;
		END IF;
				
		IF i_HideUser<>0 AND (ici_Flags & 16) <> 16 
		  THEN
			SET ici_Flags = (ici_Flags | 16); 
		ELSEIF i_HideUser=0 AND (ici_Flags & 16) = 16 
		  THEN 
		   SET ici_Flags = (ici_Flags ^ 16);		
		END IF; 
		
	UPDATE {databaseSchema}.{objectQualifier}User SET
	TimeZone = i_TimeZone,
	LanguageFile = i_LanguageFile,
	Culture = i_Culture,
	ThemeFile = i_ThemeFile,
	UseSingleSignOn = i_UseSingleSignOn,
	TextEditor = i_TextEditor,
	OverrideDefaultThemes = i_OverrideDefaultTheme,
	PMNotification = (CASE WHEN (i_PMNotification > 0) THEN i_PMNotification ELSE PMNotification END),
	AutoWatchTopics = (CASE WHEN (i_AutoWatchTopics > 0) THEN  i_AutoWatchTopics ELSE AutoWatchTopics END),
	NotificationType =  (CASE WHEN (i_NotificationType > 0) THEN  i_NotificationType ELSE NotificationType END),
	Flags = (CASE WHEN ici_Flags<>Flags THEN  ici_Flags ELSE Flags END),
	DisplayName = (CASE WHEN (i_DisplayName is not null) THEN  i_DisplayName ELSE DisplayName END),
	Email = (CASE WHEN (i_Email is not null) THEN  i_Email ELSE Email END),
	TopicsPerPage = i_TopicsPerPage,
	PostsPerPage = i_PostsPerPage				
	WHERE UserID = i_UserID; 
	  if (i_DisplayName IS NOT NULL AND COALESCE(ici_OldDisplayName,'') != COALESCE(i_DisplayName,''))
		then
		-- sync display names everywhere - can run a long time on large forums
		update {databaseSchema}.{objectQualifier}Forum set LastUserDisplayName = i_DisplayName where LastUserID = i_UserID AND (LastUserDisplayName IS NULL OR LastUserDisplayName = ici_OldDisplayName);  
		update {databaseSchema}.{objectQualifier}Topic set LastUserDisplayName = i_DisplayName where LastUserID = i_UserID AND (LastUserDisplayName IS NULL OR LastUserDisplayName = ici_OldDisplayName);
		update {databaseSchema}.{objectQualifier}Topic set UserDisplayName = i_DisplayName where UserID = i_UserID AND (UserDisplayName IS NULL OR UserDisplayName = ici_OldDisplayName);
		update {databaseSchema}.{objectQualifier}Message set UserDisplayName = i_DisplayName where UserID = i_UserID AND (UserDisplayName IS NULL OR UserDisplayName = ici_OldDisplayName);
		update {databaseSchema}.{objectQualifier}ShoutboxMessage set UserDisplayName = I_DisplayName where UseriD = I_UserID AND (UserDisplayName IS NULL OR UserDisplayName = ici_OldDisplayName);
		end if;
	
	END IF;
			
	END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */    
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_saveavatar
	(
	i_UserID INT,
	i_Avatar VARCHAR(255),
	i_AvatarImage BLOB,
	i_AvatarImageType VARCHAR(128)
	)
	BEGIN
	IF i_Avatar IS NOT NULL  THEN

	UPDATE {databaseSchema}.{objectQualifier}User
	SET Avatar = i_Avatar,
	AvatarImage = null,
	AvatarImageType = null
	WHERE UserID = i_UserID;

	ELSEIF i_AvatarImage IS NOT NULL THEN
	UPDATE {databaseSchema}.{objectQualifier}User
	SET AvatarImage = i_AvatarImage,
	AvatarImageType = i_AvatarImageType,
	Avatar = null WHERE UserID = i_UserID;
	END IF;
	END;
--GO

	/* STORED PROCEDURE CREATED BY VZ-TEAM */    
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_savepassword(i_UserID INT,i_Password VARCHAR(32))
	BEGIN
	UPDATE {databaseSchema}.{objectQualifier}User SET Password = i_Password where UserID = i_UserID;
	END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_savesignature(i_UserID INT,i_Signature TEXT) 
BEGIN
	UPDATE {databaseSchema}.{objectQualifier}User SET Signature = i_Signature WHERE UserID = i_UserID;
END;
--GO

	/* STORED PROCEDURE CREATED BY VZ-TEAM */    
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_setnotdirty(i_UserID INT)
	BEGIN
	UPDATE {databaseSchema}.{objectQualifier}User SET Flags = Flags ^ 64 WHERE UserID = i_UserID AND (Flags & 64) = 64;
	END;
--GO

	/* STORED PROCEDURE CREATED BY VZ-TEAM */    
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_setpoints(i_UserID INT,i_Points INT)
	BEGIN
	UPDATE {databaseSchema}.{objectQualifier}User SET Points = i_Points WHERE UserID = i_UserID;
	END;
--GO

	/* STORED PROCEDURE CREATED BY VZ-TEAM */   
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_setrole
	(i_BoardID INT,
	i_ProviderUserKey VARCHAR(64),
	i_Role VARCHAR(128))
	BEGIN
	DECLARE ici_UserID INT;
	DECLARE ici_GroupID INT;

	SELECT UserID INTO ici_UserID
	FROM {databaseSchema}.{objectQualifier}User
	WHERE BoardID=i_BoardID AND ProviderUserKey=i_ProviderUserKey;

	IF i_Role IS NULL THEN
	DELETE FROM {databaseSchema}.{objectQualifier}UserGroup WHERE UserID=ici_UserID;
	ELSE
	IF NOT EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}Group 
	WHERE BoardID=i_BoardID AND `Name`=i_Role) THEN

	INSERT INTO {databaseSchema}.{objectQualifier}Group(`Name`,BoardID,Flags)
	VALUES (i_Role,i_BoardID,0);
	SET ici_GroupID = LAST_INSERT_ID();
	
	insert into {databaseSchema}.{objectQualifier}ForumAccess(GroupID,ForumID,AccessMaskID)
	SELECT
	ici_GroupID AS GroupID,
	a.ForumID,
	MIN(a.AccessMaskID) AS AccessMaskID
	FROM
	{databaseSchema}.{objectQualifier}ForumAccess a
	JOIN {databaseSchema}.{objectQualifier}Group b ON b.GroupID=a.GroupID
	WHERE
	b.BoardID=i_BoardID AND
	(b.Flags & 4)=4
	GROUP BY
	a.ForumID;
	ELSE
	SELECT  GroupID INTO ici_GroupID FROM {databaseSchema}.{objectQualifier}Group WHERE BoardID=i_BoardID AND `Name`=i_Role;
	END IF;
	IF NOT EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}UserGroup 
	WHERE UserID=ici_UserID AND GroupID=ici_GroupID) THEN
	INSERT INTO {databaseSchema}.{objectQualifier}UserGroup(UserID,GroupID) VALUES (ici_UserID,ici_GroupID);
	END IF;
	END IF;
	END;
--GO    
	
	/* STORED PROCEDURE CREATED BY VZ-TEAM */    
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_suspend(i_UserID INT,i_Suspend DATETIME)
	BEGIN
	UPDATE {databaseSchema}.{objectQualifier}User SET Suspended = i_Suspend WHERE UserID=i_UserID;
	END;
--GO

	/* STORED PROCEDURE CREATED BY VZ-TEAM */    
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_upgrade(i_UserID INT)
	BEGIN
	DECLARE ici_RankID	INT;
	DECLARE ici_Flags	INT;
	DECLARE ici_MinPosts	INT;
	DECLARE ici_NumPosts	INT;
	DECLARE myrowcount INT;
	DECLARE ici_BoardID	INT;
	DECLARE ici_RankBoardID	INT;
	/* Get user and rank information*/
	SELECT
	b.RankID ,
	b.Flags,
	b.MinPosts,
	a.NumPosts,
	a.BoardID	
	INTO
	ici_RankID,
	ici_Flags,
	ici_MinPosts,
	ici_NumPosts,
	ici_BoardID
	FROM
	{databaseSchema}.{objectQualifier}User a
	INNER JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID = a.RankID
	WHERE
	a.UserID = i_UserID;

	/*If user isn't member of a ladder rank, exit*/
	if (ici_Flags & 2) != 0 THEN
	
		-- retrieve board current user's rank beling to	
	select BoardID INTO ici_RankBoardID
	from   {databaseSchema}.{objectQualifier}Rank
	where  RankID = ici_RankID LIMIT 1;

	-- does user have rank from his board?
	IF (ici_RankBoardID <> ici_BoardID) THEN
		-- get highest rank user can get
		select RankID INTO ici_RankID
		from    {databaseSchema}.{objectQualifier}Rank
		where BoardID = ici_BoardID 
			   and (Flags & 2) = 2
			   and MinPosts <= ici_NumPosts
		order by
			   MinPosts desc LIMIT 1;	
	else 	
				/* See if user got enough posts for next ladder group */
	SELECT 
	 RankID
		INTO
		ici_RankID
	FROM
		{databaseSchema}.{objectQualifier}Rank
	WHERE
		BoardID = ici_BoardID AND
		(Flags & 2) = 2 and
		MinPosts <= ici_NumPosts and
		MinPosts > ici_MinPosts
	ORDER BY
		MinPosts LIMIT 1;
	END IF;
	
	IF ici_RankID IS NOT NULL THEN
		UPDATE {databaseSchema}.{objectQualifier}User SET RankID = ici_RankID WHERE UserID = i_UserID; END IF;
		 END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}userforum_delete(i_UserID INT,i_ForumID INT)
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}UserForum WHERE UserID=i_UserID AND ForumID=i_ForumID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}userforum_list(i_UserID INT,i_ForumID INT)
BEGIN
 SELECT
		a.*,
		b.AccessMaskID,
		b.Accepted,
		c.Name AS Access 
	FROM
		{databaseSchema}.{objectQualifier}UserSelectView a
		JOIN {databaseSchema}.{objectQualifier}UserForum b ON b.UserID=a.UserID
		JOIN {databaseSchema}.{objectQualifier}AccessMask c ON c.AccessMaskID=b.AccessMaskID
	WHERE
		(i_UserID IS NULL OR a.UserID=i_UserID) AND
		(i_ForumID IS NULL OR b.ForumID=i_ForumID)
	ORDER BY
		a.Name;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}userforum_save(i_UserID INT,i_ForumID INT,i_AccessMaskID INT,
	i_UTCTIMESTAMP DATETIME)
BEGIN
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}UserForum WHERE UserID=i_UserID AND ForumID=i_ForumID) THEN
		UPDATE {databaseSchema}.{objectQualifier}UserForum SET AccessMaskID=i_AccessMaskID WHERE UserID=i_UserID AND ForumID=i_ForumID;
	ELSE
		INSERT INTO {databaseSchema}.{objectQualifier}UserForum(UserID,ForumID,AccessMaskID,Invited,Accepted) VALUES(i_UserID,i_ForumID,i_AccessMaskID,i_UTCTIMESTAMP,1);
		END IF;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}usergroup_list(i_UserID INT) 
BEGIN
	SELECT 
		b.GroupID,
		b.Name,
		b.Style
	FROM
		{databaseSchema}.{objectQualifier}UserGroup a
		JOIN {databaseSchema}.{objectQualifier}Group b ON b.GroupID=a.GroupID
	WHERE
		a.UserID = i_UserID
	ORDER BY
		b.Name;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}usergroup_save(i_UserID INT,i_GroupID INT,i_Member TINYINT(1))
BEGIN
	IF i_Member=0 THEN
		DELETE FROM {databaseSchema}.{objectQualifier}UserGroup WHERE UserID=i_UserID AND GroupID=i_GroupID;
	ELSE
	IF NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}UserGroup WHERE UserID=i_UserID AND GroupID=i_GroupID) THEN
		INSERT INTO {databaseSchema}.{objectQualifier}UserGroup(UserID,GroupID)
		VALUES (i_UserID,i_GroupID);         
		 UPDATE {databaseSchema}.{objectQualifier}User SET UserStyle= IFNULL(( SELECT f.Style FROM {databaseSchema}.{objectQualifier}UserGroup e 
		join {databaseSchema}.{objectQualifier}Group f on f.GroupID=e.GroupID WHERE e.UserID=i_UserID AND f.Style IS NOT NULL ORDER BY f.SortOrder LIMIT 1), (SELECT r.Style FROM {databaseSchema}.{objectQualifier}Rank r where r.RankID = {databaseSchema}.{objectQualifier}User.RankID LIMIT 1)) 
		WHERE UserID = i_UserID; 
		END IF;
	END IF;

END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}userpmessage_delete(i_UserPMessageID INT)
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}UserPMessage WHERE UserPMessageID=i_UserPMessageID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}userpmessage_list(i_UserPMessageID INT) 
BEGIN
	SELECT
		a.*,
		b.Name AS FromUser,
		c.UserID AS ToUserID,
		c.Name AS ToUser,
		d.IsRead,
		d.UserPMessageID,
		d.IsReply
	FROM
		{databaseSchema}.{objectQualifier}PMessage a
		inner join {databaseSchema}.{objectQualifier}UserPMessage d on d.PMessageID = a.PMessageID
		inner join {databaseSchema}.{objectQualifier}User b on b.UserID = a.FromUserID
		inner join {databaseSchema}.{objectQualifier}User c on c.UserID = d.UserID
	WHERE
		d.UserPMessageID = i_UserPMessageID
		/*AND 
		SIGN(d.IsDeleted)=0*/;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}watchforum_add(i_UserID INT,i_ForumID INT,
	i_UTCTIMESTAMP DATETIME)
BEGIN
						  
							IF NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}watchforum a
							WHERE a.ForumID=i_ForumID
							AND a.UserID = i_UserID) THEN
							INSERT INTO {databaseSchema}.{objectQualifier}WatchForum
							(ForumID,
							UserID,
							Created)
							VALUES (i_ForumID,
							i_UserID,
							i_UTCTIMESTAMP);
							END IF;
END;

--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}watchforum_check(i_UserID INT,i_ForumID INT)
BEGIN 
	SELECT WatchForumID
	 FROM {databaseSchema}.{objectQualifier}WatchForum
		 WHERE UserID = i_UserID AND ForumID = i_ForumID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}watchforum_delete(i_WatchForumID INT) 
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}WatchForum WHERE WatchForumID = i_WatchForumID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}watchforum_list(i_UserID INT) 
BEGIN
	SELECT
		a.*,
		b.Name AS ForumName,
		(SELECT CAST(COUNT(x.Priority) AS UNSIGNED) FROM {databaseSchema}.{objectQualifier}Topic x JOIN {databaseSchema}.{objectQualifier}Message y ON y.TopicID=x.TopicID WHERE x.ForumID=a.ForumID) AS Messages,
		(SELECT CAST(COUNT(1) AS UNSIGNED) FROM {databaseSchema}.{objectQualifier}Topic x WHERE x.ForumID=a.ForumID AND x.TopicMovedID IS NULL) AS Topics,
		b.LastPosted,
		b.LastMessageID,
		(SELECT TopicID FROM {databaseSchema}.{objectQualifier}Message x WHERE x.MessageID=b.LastMessageID) AS LastTopicID,
		b.LastUserID,
		IFNULL(b.LastUserName,(select `Name` from {databaseSchema}.{objectQualifier}User x where x.UserID=b.LastUserID)) AS LastUserName,
		IFNULL(b.LastUserDisplayName,(select `DisplayName` from {databaseSchema}.{objectQualifier}User x where x.UserID=b.LastUserID)) AS LastUserDisplayName
	FROM
		{databaseSchema}.{objectQualifier}WatchForum a
		INNER JOIN {databaseSchema}.{objectQualifier}Forum b ON b.ForumID = a.ForumID
	WHERE
		a.UserID = i_UserID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}watchtopic_add(i_UserID INT,i_TopicID INT,
	i_UTCTIMESTAMP DATETIME) 
BEGIN
		IF NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}WatchTopic WHERE TopicID=i_TopicID AND UserID=i_UserID) THEN
	INSERT INTO {databaseSchema}.{objectQualifier}WatchTopic(TopicID,UserID,Created)
	VALUES (i_TopicID, i_UserID, i_UTCTIMESTAMP); 	
		END IF; 
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}watchtopic_check(i_UserID INT,i_TopicID INT) 
BEGIN
	SELECT WatchTopicID FROM {databaseSchema}.{objectQualifier}WatchTopic WHERE UserID = i_UserID AND TopicID = i_TopicID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}watchtopic_delete(i_WatchTopicID INT)
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}WatchTopic WHERE WatchTopicID = i_WatchTopicID;
END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}watchtopic_list(i_UserID INT) 
BEGIN
	SELECT
		a.*,
		b.Topic AS TopicName,
		(SELECT COUNT(1) from {databaseSchema}.{objectQualifier}Message x where x.TopicID=b.TopicID) - 1 AS Replies,
		b.Views,
		b.LastPosted,
		b.LastMessageID,
		b.LastUserID,
		IFNULL(b.LastUserName,(SELECT `Name` FROM {databaseSchema}.{objectQualifier}User x WHERE x.UserID=b.LastUserID)) AS LastUserName,
		IFNULL(b.LastUserDisplayName,(SELECT `DisplayName` FROM {databaseSchema}.{objectQualifier}User x WHERE x.UserID=b.LastUserID)) AS LastUserDisplayName
	FROM
		{databaseSchema}.{objectQualifier}WatchTopic a
		INNER JOIN {databaseSchema}.{objectQualifier}Topic b ON b.TopicID = a.TopicID
	WHERE
		a.UserID = i_UserID;
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_migrate
 (
	i_UserID INT,
	i_ProviderUserKey VARCHAR(64),
	i_UpdateProvider TINYINT(1)
 )
 BEGIN
	DECLARE ici_Password VARCHAR(255);
		DECLARE ici_IsApproved TINYINT(1);
		DECLARE ici_LastActivity DATETIME;
		DECLARE ici_Joined DATETIME;
		DECLARE ici_case INT;
		DECLARE ici_result TINYINT(1) DEFAULT 0;


	UPDATE {databaseSchema}.{objectQualifier}User SET ProviderUserKey = i_ProviderUserKey WHERE UserID = i_UserID;
 
	IF (i_UpdateProvider = 1) THEN 	
				
				  SELECT			
			Flags & 2 INTO ici_case 	
				
		  FROM
			{databaseSchema}.{objectQualifier}User
		  WHERE
			UserID = i_UserID;
				  IF ici_case =2 THEN SET ici_result =1;  END IF;               
				 
				
		SELECT
			  Password,
			  ici_result,
			  LastVisit,
			  Joined
				INTO ici_Password, ici_IsApproved, ici_LastActivity, ici_Joined
		FROM
			{databaseSchema}.{objectQualifier}User
		WHERE
			UserID = i_UserID;
		
		UPDATE
			{databaseSchema}.{objectQualifier}prov_Membership
		SET
			Password = ici_Password,
			PasswordFormat = '1',
			LastActivity = ici_LastActivity,
			IsApproved = ici_IsApproved,
			Joined = ici_Joined
		WHERE
			UserID = i_ProviderUserKey;
	END IF;
 END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_listmedals(i_UserID	INT)
BEGIN
 
	(SELECT
		a.`MedalID`,
		a.`Name`,
		IFNULL(b.`Message`, a.`Message`) AS `Message`,
		a.`MedalURL`,
		a.`RibbonURL`,
		a.`SmallMedalURL`,
		IFNULL(a.`SmallRibbonURL`, a.`SmallMedalURL`) AS SmallRibbonURL,
		a.`SmallMedalWidth`,
		a.`SmallMedalHeight`,
		IFNULL(a.`SmallRibbonWidth`, a.`SmallMedalWidth`) AS `SmallRibbonWidth`,
		IFNULL(a.`SmallRibbonHeight`, a.`SmallMedalHeight`) AS `SmallRibbonHeight`,
		{databaseSchema}.{objectQualifier}medal_getsortorder(b.`SortOrder`,a.`SortOrder`,a.`Flags`) as `SortOrder`,
		{databaseSchema}.{objectQualifier}medal_gethide(b.`Hide`,a.`Flags`) AS `Hide`,
		{databaseSchema}.{objectQualifier}medal_getribbonsetting(a.`SmallRibbonURL`,a.`Flags`,b.`OnlyRibbon`) AS `OnlyRibbon`,
		a.`Flags`,
		b.`DateAwarded`
	FROM
		{databaseSchema}.{objectQualifier}Medal a
		INNER JOIN {databaseSchema}.{objectQualifier}UserMedal b ON a.`MedalID` = b.`MedalID`
	WHERE
		b.`UserID` = i_UserID)
 
	UNION
 
	(SELECT	a.`MedalID`, a.`Name`,
		IFNULL(b.`Message`, a.`Message`) as `Message`,
		a.`MedalURL`,
		a.`RibbonURL`,
		a.`SmallMedalURL`,
		IFNULL(a.`SmallRibbonURL`, a.`SmallMedalURL`) as `SmallRibbonURL`,
		a.`SmallMedalWidth`,
		a.`SmallMedalHeight`,
		IFNULL(a.`SmallRibbonWidth`, a.`SmallMedalWidth`) as `SmallRibbonWidth`,
		IFNULL(a.`SmallRibbonHeight`, a.`SmallMedalHeight`) as `SmallRibbonHeight`,
		{databaseSchema}.{objectQualifier}medal_getsortorder(b.`SortOrder`,a.`SortOrder`,a.`Flags`) as `SortOrder`,
		{databaseSchema}.{objectQualifier}medal_gethide(b.`Hide`,a.`Flags`) as `Hide`,
		{databaseSchema}.{objectQualifier}medal_getribbonsetting(a.`SmallRibbonURL`,a.`Flags`,b.`OnlyRibbon`) as `OnlyRibbon`,
		a.`Flags`,
		NULL AS `DateAwarded`
	FROM
		{databaseSchema}.{objectQualifier}Medal a
		INNER JOIN {databaseSchema}.{objectQualifier}GroupMedal b ON a.`MedalID` = b.`MedalID`
		INNER JOIN {databaseSchema}.{objectQualifier}UserGroup c ON b.`GroupID` = c.`GroupID`
	WHERE
		c.`UserID` = i_UserID)
	ORDER BY
		`OnlyRibbon` DESC,
		`SortOrder` ASC;
 
END; 
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}accessmask_list(
i_BoardID  INT, i_AccessMaskID INT, i_ExcludeFlags INT, i_PageUserID INT, i_IsUserMask TINYINT(1), i_IsAdminMask  TINYINT(1), i_IsCommonMask  TINYINT(1), i_PageIndex int, i_PageSize int)
BEGIN
	 declare ici_TotalRows int default 0;
	 declare ici_FirstSelectRowNumber int default 0;
 IF i_AccessMaskID IS NULL THEN
		if i_ExcludeFlags IS NULL THEN
		SET i_ExcludeFlags = 0;
		END IF;
		set i_PageIndex = i_PageIndex + 1;  
		select  count(1) into  ici_TotalRows FROM    {databaseSchema}.{objectQualifier}AccessMask a
			  WHERE    a.BoardID = i_BoardID and
			  (a.Flags & i_ExcludeFlags) = 0
			  and a.IsUserMask = case when i_IsUserMask = 1 and i_IsCommonMask = 0 and (i_PageUserID is null or a.CreatedByUserID = i_PageUserID) 
			  then 1  else 0  end
			  and a.IsAdminMask = case when i_IsAdminMask = 1  and i_IsCommonMask = 0  then 1 else 0 end;
			
	
	   select  (i_PageIndex - 1) * i_PageSize into ici_FirstSelectRowNumber;
	   
	   set @biplpr = CONCAT('select
		a.*,
		{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
		FROM     {databaseSchema}.{objectQualifier}AccessMask a  		
		WHERE  a.BoardID = ',i_BoardID,' and		
			(a.Flags & ',i_ExcludeFlags,') = 0
	   and a.IsUserMask = case when ',i_IsUserMask,' = 1 and ',i_IsCommonMask,' = 0 and (',IFNULL(i_PageUserID,0),' = 0 or a.CreatedByUserID = ',IFNULL(i_PageUserID,0),') 
			  then 1  else 0  end
			  and a.IsAdminMask = case when ',i_IsAdminMask,' = 1  and ',i_IsCommonMask,' = 0  then 1 else 0 end       
			  order by a.SortOrder  LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');
		PREPARE stmt_els FROM @biplpr;
		EXECUTE stmt_els;
		DEALLOCATE PREPARE stmt_els;
ELSE
SELECT   a.*,
		 {databaseSchema}.{objectQualifier}biginttobool(0) as TotalRows 
FROM     {databaseSchema}.{objectQualifier}AccessMask a
WHERE    a.BoardID = i_BoardID
AND a.AccessMaskID = i_AccessMaskID LIMIT 1;
END IF;
END;
--GO


/* STORED PROCEDURE CREATi_BoardIDED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}accessmask_searchlist(
i_BoardID  INT, i_AccessMaskID INT, i_ExcludeFlags INT, i_PageUserID INT, i_IsUserMask TINYINT(1), i_IsAdminMask  TINYINT(1), i_IsCommonMask  TINYINT(1), i_PageIndex int, i_PageSize int)
BEGIN
	 declare ici_TotalRows int default 0;
	 declare ici_FirstSelectRowNumber int default 0;
 IF i_AccessMaskID IS NULL THEN
		if i_ExcludeFlags IS NULL THEN
		SET i_ExcludeFlags = 0;
		END IF;
		set i_PageIndex = i_PageIndex + 1;  
		select  count(1) into  ici_TotalRows FROM    {databaseSchema}.{objectQualifier}AccessMask a
			  WHERE    a.BoardID = i_BoardID and
			  (a.Flags & i_ExcludeFlags) = 0
			  and (i_IsCommonMask = 0 or (a.IsUserMask = 0 and a.IsAdminMask = 0))
			  and (i_IsUserMask = 0 or a.IsUserMask = 1)
			  and (i_IsAdminMask = 0 or a.IsAdminMask = 1)
			and (i_PageUserID is null  or CreatedByUserID = i_PageUserID);
	
	   select  (i_PageIndex - 1) * i_PageSize into ici_FirstSelectRowNumber;
	   
	   set @biplpr = CONCAT('select
		a.*,
		{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
		FROM     {databaseSchema}.{objectQualifier}AccessMask a  		
		WHERE  a.BoardID = ',i_BoardID,' and		
			(a.Flags & ',i_ExcludeFlags,') = 0
		and (',i_IsCommonMask,' = 0 or (a.IsUserMask = 0 and a.IsAdminMask = 0))
			  and (',i_IsUserMask,' = 0 or a.IsUserMask = 1)
			  and (',i_IsAdminMask,' = 0 or a.IsAdminMask = 1) 
			  and (',IFNULL(i_PageUserID,0) ,' = 0 or a.CreatedByUserID = ',IFNULL(i_PageUserID,0),')
		order by a.SortOrder  LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');
		PREPARE stmt_els FROM @biplpr;
		EXECUTE stmt_els;
		DEALLOCATE PREPARE stmt_els;
ELSE
SELECT   a.*,
		 {databaseSchema}.{objectQualifier}biginttobool(0) as TotalRows 
FROM     {databaseSchema}.{objectQualifier}AccessMask a
WHERE    a.BoardID = i_BoardID
AND a.AccessMaskID = i_AccessMaskID LIMIT 1;
END IF;
END;
--GO


CREATE  PROCEDURE {databaseSchema}.{objectQualifier}accessmask_pforumlist(
i_BoardID  INT, i_AccessMaskID INT, i_ExcludeFlags INT, i_PageUserID INT, i_IsUserMask TINYINT(1), i_IsCommonMask  TINYINT(1))
BEGIN
IF i_AccessMaskID IS NULL THEN
IF i_IsCommonMask = 1 THEN
SELECT   a.*
FROM     {databaseSchema}.{objectQualifier}AccessMask a
WHERE    a.BoardID = i_BoardID and          
			(a.Flags & i_ExcludeFlags) = 0         
		 and ((i_IsUserMask = 0 or (a.CreatedByUserID = i_PageUserID and a.IsUserMask = 1)) 
		 or (i_IsCommonMask = 0 or (a.IsAdminMask = 0 and a.IsUserMask = 0))) 					
ORDER BY a.IsUserMask,a.SortOrder;
ELSE
SELECT   a.*
FROM     {databaseSchema}.{objectQualifier}AccessMask a
WHERE    a.BoardID = i_BoardID and          
			(a.Flags & i_ExcludeFlags) = 0         
		 and (i_IsUserMask = 1 and (a.CreatedByUserID = i_PageUserID and a.IsUserMask = 1)) 							
ORDER BY a.IsUserMask,a.SortOrder;
END IF;
ELSE
SELECT   a.*
FROM     {databaseSchema}.{objectQualifier}AccessMask a
WHERE    a.BoardID = i_BoardID
AND a.AccessMaskID = i_AccessMaskID  LIMIT 1;
END IF;
END;
--GO

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}accessmask_aforumlist(
i_BoardID  INT, i_AccessMaskID INT, i_ExcludeFlags INT, i_PageUserID INT, i_IsAdminMask TINYINT(1), i_IsCommonMask  TINYINT(1))
BEGIN
IF i_AccessMaskID IS NULL THEN
SELECT   a.*
FROM     {databaseSchema}.{objectQualifier}AccessMask a
WHERE    a.BoardID = i_BoardID and
			(a.Flags & i_ExcludeFlags) = 0
			 and ((i_IsAdminMask = 1 and a.IsAdminMask = 1)
			 or (i_IsCommonMask = 1 and (a.IsAdminMask = 0 and a.IsUserMask = 0)))  		
ORDER BY a.SortOrder;
ELSE
SELECT   a.*
FROM     {databaseSchema}.{objectQualifier}AccessMask a
WHERE    a.BoardID = i_BoardID
AND a.AccessMaskID = i_AccessMaskID   
			and ((i_IsAdminMask = 1 and a.IsAdminMask = 1)
			or (i_IsCommonMask = 1 and (a.IsAdminMask = 0 and a.IsUserMask = 0))) LIMIT 1;
END IF;
END;
--GO

CREATE  PROCEDURE {objectQualifier}accessmask_delete(
i_AccessMaskID INT)
BEGIN
DECLARE  l_flag INT DEFAULT 1;

IF EXISTS (SELECT 1
FROM   {databaseSchema}.{objectQualifier}ForumAccess
WHERE  AccessMaskID = i_AccessMaskID)
OR EXISTS (SELECT 1
FROM   {databaseSchema}.{objectQualifier}UserForum
WHERE  AccessMaskID = i_AccessMaskID) THEN
SET l_flag = 0;
ELSE
DELETE FROM {databaseSchema}.{objectQualifier}AccessMask
WHERE       AccessMaskID = i_AccessMaskID;
END IF;
SELECT l_flag;
END;
--GO
/* STORED PROCEDURE CREATED BY VZ-TEAM */

CREATE  PROCEDURE {databaseSchema}.{objectQualifier}accessmask_save(
IN i_AccessMaskID    INT,
IN i_BoardID         INT,
IN i_Name            VARCHAR(128),
IN i_ReadAccess      TINYINT(1),
IN i_PostAccess      TINYINT(1),
IN i_ReplyAccess     TINYINT(1),
IN i_PriorityAccess  TINYINT(1),
IN i_PollAccess      TINYINT(1),
IN i_VoteAccess      TINYINT(1),
IN i_ModeratorAccess TINYINT(1),
IN i_EditAccess      TINYINT(1),
IN i_DeleteAccess    TINYINT(1),
IN i_UploadAccess    TINYINT(1),
IN i_DownloadAccess  TINYINT(1),
IN i_UserForumAccess TINYINT(1),
IN i_SortOrder       INT,
IN i_UserID          INT,
IN i_IsUserMask      TINYINT(1),
IN i_IsAdminMask    TINYINT(1),
IN i_UTCTIMESTAMP    DATETIME)
BEGIN
DECLARE ici_UserName  VARCHAR(255);
DECLARE ici_UserDisplayName  VARCHAR(255);
DECLARE  l_Flags INT;
SET l_Flags = 0;

IF i_ReadAccess <> 0 THEN
SET l_Flags = l_Flags | 1;
END IF;
IF i_PostAccess <> 0 THEN
SET l_Flags = l_Flags | 2;
END IF;
IF i_ReplyAccess <> 0 THEN
SET l_Flags = l_Flags | 4;
END IF;
IF i_PriorityAccess <> 0 THEN
SET l_Flags = l_Flags | 8;
END IF;
IF i_PollAccess <> 0 THEN
SET l_Flags = l_Flags | 16;
END IF;
IF i_VoteAccess <> 0 THEN
SET l_Flags = l_Flags | 32;
END IF;
IF i_ModeratorAccess <> 0 THEN
SET l_Flags = l_Flags | 64;
END IF;
IF i_EditAccess <> 0 THEN
SET l_Flags = l_Flags | 128;
END IF;
IF i_DeleteAccess <> 0 THEN
SET l_Flags = l_Flags | 256;
END IF;
IF i_UploadAccess <> 0 THEN
SET l_Flags = l_Flags | 512;
END IF;
IF i_DownloadAccess <> 0 THEN
SET l_Flags = l_Flags | 1024;
END IF;
IF i_UserForumAccess <> 0 THEN
SET l_Flags = l_Flags | 32768;
END IF;
IF i_UserID IS NOT NULL THEN   
	SELECT Name, DisplayName INTO ici_UserName, ici_UserDisplayName FROM {databaseSchema}.{objectQualifier}User where UserID = i_UserID LIMIT  1;
END IF;
IF i_AccessMaskID IS NULL THEN
INSERT INTO {databaseSchema}.{objectQualifier}AccessMask
(`Name`,
`BoardID`,
`Flags`,`SortOrder`,`IsUserMask`,`IsAdminMask`,`CreatedByUserID`,`CreatedByUserName`,`CreatedByUserDisplayName`,`CreatedDate`)
VALUES     (i_Name,
i_BoardID,
l_Flags,i_SortOrder,i_IsUserMask,i_IsAdminMask,i_UserId,ici_UserName,ici_UserDisplayName,i_UTCTIMESTAMP);
SET i_AccessMaskID =  LAST_INSERT_ID();
ELSE
UPDATE {databaseSchema}.{objectQualifier}AccessMask
SET    `Name` = i_Name,
`Flags` = l_Flags,`SortOrder` = i_SortOrder, `IsUserMask` = i_IsUserMask,`IsAdminMask` = i_IsAdminMask
WHERE  AccessMaskID = i_AccessMaskID;
END IF;

 IF i_UserID IS NOT NULL THEN   
	SELECT Name, DisplayName INTO ici_UserName, ici_UserDisplayName FROM {databaseSchema}.{objectQualifier}User where UserID = i_UserID LIMIT  1;
	   -- guests should not create forums
	ELSE    
	SELECT Name, DisplayName INTO ici_UserName, ici_UserDisplayName FROM {databaseSchema}.{objectQualifier}User where BoardID = i_BoardID and (Flags & 4) = 4  ORDER BY Joined LIMIT 1;
	END IF;
	if exists (select 1 from {databaseSchema}.{objectQualifier}AccessMaskHistory where AccessMaskID = i_AccessMaskID and ChangedDate = i_UTCTIMESTAMP  LIMIT 1) THEN
	update {databaseSchema}.{objectQualifier}AccessMaskHistory set 
		   ChangedUserID = i_UserID,	
		   ChangedUserName = ici_UserName,
		   ChangedDisplayName = ici_UserDisplayName
	 where AccessMaskID = i_AccessMaskID and ChangedDate = i_UTCTIMESTAMP; 
	else    
	INSERT INTO {databaseSchema}.{objectQualifier}AccessMaskHistory(AccessMaskID,ChangedUserID,ChangedUserName,ChangedDisplayName,ChangedDate)
	VALUES (i_AccessMaskID, i_UserID,ici_UserName,ici_UserDisplayName,i_UTCTIMESTAMP);
	end IF;

END;
--GO
/* My procedures */

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_get
(
	i_BoardID		INT,
	i_ProviderUserKey       VARCHAR(64) 
) 

BEGIN

	SELECT UserID FROM {databaseSchema}.{objectQualifier}User 
		WHERE BoardID=i_BoardID 
		AND ProviderUserKey=i_ProviderUserKey;
END;
--GO 


/* STORED PROCEDURE CREATED BY VZ-TEAM topic_latest */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_latest
 (
	i_BoardID INT,
	i_NumPosts INT,
	i_PageUserID INT,
	i_StyledNicks TINYINT(1),
	i_ShowNoCountPosts TINYINT(1),
	i_FindLastUnread TINYINT(1),
	i_UTCTIMESTAMP DATETIME
 )
 BEGIN
 -- to boost performance
 -- SET i_StyledNicks = 0;		
set @tlpreps = CONCAT('SELECT
		t.LastPosted,
		t.ForumID,
		f.Name as Forum,
		t.Topic,
		t.`Status`,
		t.Styles,
		t.TopicID,
		t.TopicMovedID,
		t.UserID,
		IFNull(t.UserName,(select x.Name from {databaseSchema}.{objectQualifier}User x where x.UserID = t.UserID)) AS UserName,
		IFNull(t.UserDisplayName,(select x.DisplayName from {databaseSchema}.{objectQualifier}User x where x.UserID = t.UserID)) AS UserDisplayName,
		(select (x.Flags & 4) from {databaseSchema}.{objectQualifier}User x where x.UserID = t.UserID) AS StarterIsGuest,
		t.LastMessageID,
		t.LastMessageFlags,
		t.LastUserID,
		t.NumPosts, 
		t.Posted,	
		IFNULL(t.LastUserName,(SELECT `Name` from {databaseSchema}.{objectQualifier}User x WHERE x.UserID = t.LastUserID)) AS LastUserName,
		IFNULL(t.LastUserDisplayName,(SELECT `DisplayName` from {databaseSchema}.{objectQualifier}User x WHERE x.UserID = t.LastUserID)) AS LastUserDisplayName,
		(SELECT (x.Flags & 4) from {databaseSchema}.{objectQualifier}User x WHERE x.UserID = t.LastUserID) AS LastUserIsGuest,
		(case(',i_StyledNicks,')
			  when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=t.LastUserID)  
			else null end) AS LastUserStyle,	
		(case(',i_FindLastUnread,')
			 when 1 then
			   (SELECT  CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=f.ForumID AND x.UserID = ',i_PageUserID,' LIMIT 1)
			 else CAST(NULL AS DATETIME)	end) AS LastForumAccess,
		(case(',i_FindLastUnread,')
			 when 1 then
			   (SELECT  CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=t.TopicID AND y.UserID = ',i_PageUserID,' LIMIT 1)
			 else CAST(NULL AS DATETIME)	 end) AS  	LastTopicAccess    
	FROM 
		{databaseSchema}.{objectQualifier}Topic t
	INNER JOIN
		{databaseSchema}.{objectQualifier}Forum f ON t.ForumID = f.ForumID	
	INNER JOIN
		{databaseSchema}.{objectQualifier}Category c ON c.CategoryID = f.CategoryID
	JOIN
		{databaseSchema}.{objectQualifier}ActiveAccess v ON v.ForumID=f.ForumID	 	
	WHERE
		c.BoardID = ',i_BoardID,'
		AND t.TopicMovedID is NULL
		AND v.UserID= ',i_PageUserID,'
		AND v.ReadAccess <> 0
		AND SIGN(t.Flags & 8) != 1
		AND t.LastPosted IS NOT NULL
		AND
		f.Flags & 4 <> (CASE WHEN (',i_ShowNoCountPosts,') > 0 THEN -1 ELSE 4 END)	       
	ORDER BY
	t.LastPosted DESC LIMIT ',i_NumPosts,'');	
	-- AND ({databaseSchema}.{objectQualifier}vaccess_s_readaccess(?,f.ForumID)<>0)
	  
	PREPARE stmt_tl FROM @tlpreps;
	EXECUTE stmt_tl;
	DEALLOCATE PREPARE stmt_tl;    
	END;    
--GO

   /* STORED PROCEDURE CREATED BY VZ-TEAM topic_simplelist */
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_simplelist(
	i_StartID INT,
	i_Limit   INT)

	BEGIN
	DECLARE firstTipicID INTEGER DEFAULT 1;
	SELECT   t.`TopicID`
	INTO firstTipicID
	FROM     {databaseSchema}.{objectQualifier}Topic t
	WHERE    t.`TopicID` >= i_StartID LIMIT 1;
  
	set @tsl_prps =concat('SELECT   t.`TopicID`,
	t.`Topic`
	FROM     {databaseSchema}.{objectQualifier}Topic t
	WHERE    t.`TopicID` >= ',firstTipicID,'  
	ORDER BY t.`TopicID` LIMIT ',i_Limit,'');
	
	PREPARE stmt_tsl FROM @tsl_prps;
	EXECUTE stmt_tsl;			
	DEALLOCATE PREPARE stmt_tsl;  
		 
	 END;
 --GO  

	  /* STORED PROCEDURE CREATED BY VZ-TEAM user_activity_rank */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_activity_rank
 (
	i_BoardID INT,
	i_DisplayNumber INT,
	i_StartDate  DATETIME
 ) 
 BEGIN
	DECLARE ici_GuestUserID INT;
	DECLARE ici_AllIntervalPostCount INT;	 

	SET ici_GuestUserID =
	(SELECT 
		a.UserID
	FROM
		{databaseSchema}.{objectQualifier}User a
		INNER JOIN {databaseSchema}.{objectQualifier}UserGroup b on b.UserID = a.UserID
		INNER JOIN {databaseSchema}.{objectQualifier}Group c on b.GroupID = c.GroupID
	WHERE
		a.BoardID = i_BoardID and
		(c.Flags & 2)<>0 ORDER BY a.UserID LIMIT 1
	);

	SELECT  Count(m.UserID) INTO ici_AllIntervalPostCount FROM {databaseSchema}.{objectQualifier}Message m
			WHERE m.Posted >= i_StartDate;

	set @str_stmt_uar = CONCAT('SELECT
		counter.ID,
		u.Name,
		u.DisplayName,
		u.Joined,
		u.UserStyle,
		{databaseSchema}.{objectQualifier}biginttobool(COALESCE((u.flags & 16)=16,false)) AS IsHidden,
		counter.NumOfPosts,'
		,ici_AllIntervalPostCount,'  as NumOfAllIntervalPosts
	FROM
	{databaseSchema}.{objectQualifier}User u inner join
	(
	SELECT m.UserID as ID, Count(m.UserID) as NumOfPosts FROM {databaseSchema}.{objectQualifier}Message m
	WHERE m.Posted >= ''',i_StartDate,''' 
	GROUP BY m.UserID
	) AS counter ON u.UserID = counter.ID
	WHERE
	u.BoardID =',i_BoardID,' and u.UserID != ',ici_GuestUserID,'
	ORDER BY
	NumOfPosts DESC LIMIT 0,',i_DisplayNumber,'');
	PREPARE stmt_uar FROM @str_stmt_uar;    
	EXECUTE stmt_uar;
	DEALLOCATE PREPARE stmt_uar;
   
	END;
--GO
	/* STORED PROCEDURE CREATED BY VZ-TEAM user_simplelist */   
	CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_simplelist(
	i_StartID INT,
	i_Limit   INT)
	BEGIN
	DECLARE l_Limit INT DEFAULT 500;
	DECLARE l_StartID INT DEFAULT 0;
		IF i_StartID IS NOT NULL THEN SET l_StartID =i_StartID ;END IF;
		IF i_Limit IS NOT NULL THEN SET l_Limit=i_Limit;END IF;
 
	/*SET ROWCOUNT  i_Limit*/
	set @str_stmt_usl = CONCAT('SELECT   a.`UserID`,
	a.`Name`, a.`DisplayName`
	FROM     {databaseSchema}.{objectQualifier}User a
	WHERE    a.`UserID` >= ',l_StartID,'
	AND a.`UserID` < (',l_StartID,' + ',l_Limit,')
		 ORDER BY a.`UserID` LIMIT ',l_StartID,', ',l_Limit,'');
		   PREPARE stmt_usl FROM @str_stmt_usl;
		   EXECUTE stmt_usl;
		   DEALLOCATE PREPARE stmt_usl;  
		 /*SET ROWCOUNT  0*/
	 END;
   --GO  
	/* STORED PROCEDURE CREATED BY VZ-TEAM topic_announcements */
 CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_announcements
 (
	i_BoardID int,
	i_NumPosts int,
	i_PageUserID int
 )

 BEGIN 
	DECLARE ici_SQL VARCHAR(500);
	DECLARE ici_numPosts CHAR;
	 
	SET @ici_SQL = CONCAT('SELECT t.Topic, 
						   t.LastPosted, 
						   t.Posted,
						   t.TopicID,
						   t.LastMessageID, 
						   t.LastMessageFlags FROM
						   {databaseSchema}.{objectQualifier}Topic t 
						   INNER JOIN {databaseSchema}.{objectQualifier}Category c
						   ON c.CategoryID = f.CategoryID 
						   INNER JOIN {databaseSchema}.{objectQualifier}Forum f                          
						   ON t.ForumID = f.ForumID
						   join {databaseSchema}.{objectQualifier}ActiveAccess v 
						   on v.ForumID=f.ForumID  
						   WHERE c.BoardID = ',CONVERT(i_BoardID, CHAR),'
						   AND v.UserID=',CONVERT(i_PageUserID, CHAR),'
						   (v.ReadAccess) <> 0                        
						   OR (f.Flags & 2) = 0) 
						   AND (t.Flags & 8) != 8 
						   AND t.TopicMovedID IS NULL
						   AND (t.Priority = 2) ORDER BY t.LastPosted DESC LIMIT ',CONVERT (i_NumPosts, CHAR),'');

	 PREPARE stmt_ta FROM @ici_SQL;
	 EXECUTE stmt_ta;
	 DEALLOCATE PREPARE stmt_ta;	
 
 END;  
 --GO

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}rss_topic_latest
(
	i_BoardID int,
	i_NumPosts int,
	i_PageUserID int,
	i_StyledNicks TINYINT(1),
	i_ShowNoCountPosts  TINYINT(1)
)
BEGIN	
	set @str_stmt_ta = CONCAT('
	SELECT
		m.Message AS LastMessage,
		t.LastPosted,
		t.ForumID,
		f.Name AS Forum,
		t.Topic,
		t.TopicID,
		t.TopicMovedID,
		t.UserID,
		t.UserName,
		t.UserDisplayName,	
		(select (x.Flags & 4) from {databaseSchema}.{objectQualifier}User x where x.UserID = t.UserID) AS StarterIsGuest,
		t.LastMessageID,
		t.LastMessageFlags,
		t.LastUserID,	
		t.Posted,		
		IFNULL(t.LastUserName,(select x.Name from {databaseSchema}.{objectQualifier}User x where x.UserID = t.LastUserID)) AS LastUserName,
		IFNULL(t.LastUserDisplayName,(select x.DisplayName from {databaseSchema}.{objectQualifier}User x where x.UserID = t.LastUserID)) AS LastUserDisplayName,
		(SELECT (x.Flags & 4) from {databaseSchema}.{objectQualifier}User x WHERE x.UserID = t.LastUserID) AS LastUserIsGuest		
	FROM
		{databaseSchema}.{objectQualifier}Message m 
	INNER JOIN	
		{databaseSchema}.{objectQualifier}Topic t  ON t.LastMessageID = m.MessageID
	INNER JOIN
		{databaseSchema}.{objectQualifier}Forum f ON t.ForumID = f.ForumID	
	INNER JOIN
		{databaseSchema}.{objectQualifier}Category c ON c.CategoryID = f.CategoryID
	JOIN
		{databaseSchema}.{objectQualifier}ActiveAccess v ON v.ForumID=f.ForumID
	WHERE	
		c.BoardID = ',CONVERT(i_BoardID, CHAR),'
		AND (t.TopicMovedID IS NULL) 
		AND (v.UserID = ',CONVERT(i_PageUserID, CHAR),') 
		AND (CAST(v.ReadAccess AS UNSIGNED) <> 0) 
		AND (t.Flags & 8) != 8  
		AND (t.LastPosted IS NOT NULL)
		AND	f.Flags & 4 <> (CASE WHEN (',CONVERT (i_ShowNoCountPosts, CHAR),') > 0 THEN -1 ELSE 4 END)
	ORDER BY
		t.LastPosted DESC LIMIT ',CONVERT (i_NumPosts, CHAR),'');
		PREPARE stmt_ta FROM @str_stmt_ta;       
		EXECUTE stmt_ta;
		DEALLOCATE PREPARE stmt_ta;	
END;
--GO

	 
 /* STORED PROCEDURE CREATED BY VZ-TEAM */

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_simplelist(
				 i_StartID INT,
				 i_Limit   INT)
  BEGIN
	DECLARE l_Limit INT DEFAULT 500;
	DECLARE l_StartID INT DEFAULT 0;
		IF i_StartID IS NOT NULL THEN SET l_StartID =i_StartID ;END IF;
		IF i_Limit IS NOT NULL THEN SET l_Limit=i_Limit;END IF;
		   
		 SET @stmt_fsl_rec = CONCAT('SELECT   f.`ForumID`,
				  f.`Name`, f.LastPosted
		 FROM     {databaseSchema}.{objectQualifier}Forum f
		 WHERE    f.ForumID >= ',l_StartID,'   
		 AND    f.ForumID < ',(l_StartID+l_Limit),'     
		 ORDER BY f.`ForumID` ');
	   PREPARE stmt_fsl FROM  @stmt_fsl_rec;
		   EXECUTE stmt_fsl;
		   DEALLOCATE PREPARE stmt_fsl;          
	 END;
 --GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE  PROCEDURE {databaseSchema}.{objectQualifier}category_simplelist(
i_StartID INT,
i_Limit   INT)
BEGIN
SET @stmt_csl_rec = CONCAT('SELECT c.CategoryID,
c.Name
FROM     {databaseSchema}.{objectQualifier}Category c
WHERE    c.CategoryID >= ',i_StartID,'
AND c.CategoryID < ',(i_StartID + i_Limit),'
ORDER BY c.CategoryID LIMIT ',i_Limit,'');
PREPARE stmt_csl FROM @stmt_csl_rec;
	EXECUTE stmt_csl;
	DEALLOCATE PREPARE stmt_csl;         
	END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_simplelist(
				i_StartID INT,
				i_Limit   INT)
BEGIN
		DECLARE ici_Limit INT DEFAULT 1000;
		DECLARE ici_StartID INT DEFAULT 0; 
		DECLARE firstMessage INT DEFAULT 1;   
		IF i_StartID IS NOT NULL THEN 
		SET ici_StartID =i_StartID ;
		END IF; 
		
		IF i_Limit IS NOT NULL THEN 
		SET ici_Limit=i_Limit;
		END IF;

		SELECT  m.`MessageID`
					INTO  firstMessage  
		FROM     {databaseSchema}.{objectQualifier}Message m
		WHERE    m.`MessageID` >= i_StartID LIMIT 1;
	   

	set @stmt_msl_rec = CONCAT('SELECT  m.`MessageID`,
				 m.`TopicID`        
		FROM     {databaseSchema}.{objectQualifier}Message m
		WHERE    m.`MessageID` >= ',firstMessage,'    
		AND     m.`MessageID` < ',(firstMessage + i_StartID),'   
		AND m.`TopicID` IS NOT NULL ORDER BY m.`MessageID` LIMIT ',i_Limit,' ;');
	PREPARE stmt_msl FROM  @stmt_msl_rec;
	EXECUTE stmt_msl;
	DEALLOCATE PREPARE stmt_msl;       
		
	END;
--GO 

/* VZRUS ADDONS - STORED PROCEDURES CREATED BY VZ-TEAM */
		CREATE  PROCEDURE {databaseSchema}.{objectQualifier}db_size(
		i_DbScheme VARCHAR(128), i_DbName VARCHAR(128))
		BEGIN
		SELECT DATABASE() INTO  i_DbScheme;
		SELECT 
		IFNULL((ROUND(((SUM(t.data_length)+ SUM(t.index_length))/1048576),2)),0)
		FROM INFORMATION_SCHEMA.TABLES t 
		WHERE t.engine = 'InnoDB' AND t.TABLE_SCHEMA = i_DbScheme;
		END;
--GO


		CREATE  PROCEDURE {databaseSchema}.{objectQualifier}rsstopic_list(
		i_ForumID INT, i_StartID INT, i_Limit INT)
		BEGIN               
	-- check for IsDeleted flag a.Flags & 8
	SET @stmt_rsstl_rec = CONCAT('SELECT a.Topic,
			   a.TopicID, 
			   b.Name, 
			   IFNULL(a.LastPosted,a.Posted) AS LastPosted,
			   a.Posted,
			   a.LastMessageID,
			   IFNULL(a.LastUserID, a.UserID) AS LastUserID,
			   IFNULL(a.LastMessageID,(select  m.MessageID 
			   FROM {databaseSchema}.{objectQualifier}Message m where m.TopicID = a.TopicID order by m.Posted desc LIMIT 1)) AS LastMessageID, 
			   IFNULL(a.LastMessageFlags,22) AS LastMessageFlags
			   FROM {databaseSchema}.{objectQualifier}Topic a
			   INNER JOIN {databaseSchema}.{objectQualifier}Forum b 
			   ON b.ForumID = a.ForumID
			   WHERE a.ForumID = ',i_ForumID,'  AND IFNULL(SIGN(a.Flags & 8),0) <> 8  
			   AND a.TopicMovedID IS NULL
			   ORDER BY a.Posted DESC
			   LIMIT ',i_StartID,',',i_Limit,' ;');
	PREPARE stmt_rsstl FROM  @stmt_rsstl_rec;
	EXECUTE stmt_rsstl;
	DEALLOCATE PREPARE stmt_rsstl;    
	END;
--GO

/* *************************************************************************************************************************** */
/* **** BEGIN CREATE PROCEDURES ******/

/* Procedures for "Thanks" Mod  */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_addthanks 
	(I_FromUserID INT,
	I_MessageID INT,
	i_UTCTIMESTAMP DATETIME,
	I_UseDisplayName TINYINT(1)
	)
BEGIN
DECLARE ici_ToUserID INT;
IF not exists (SELECT ThanksID FROM {databaseSchema}.{objectQualifier}Thanks WHERE MessageID = I_MessageID AND ThanksFromUserID=I_FromUserID LIMIT 1) THEN
	SET ici_ToUserID = (SELECT UserID FROM {databaseSchema}.{objectQualifier}Message WHERE MessageID = I_MessageID);
	INSERT INTO {databaseSchema}.{objectQualifier}Thanks (ThanksFromUserID, ThanksToUserID, MessageID, ThanksDate) Values 
								(I_FromUserID,ici_ToUserID, I_MessageID, i_UTCTIMESTAMP);
	SELECT (CASE WHEN I_UseDisplayName = 1 THEN DisplayName ELSE `Name` END) FROM {databaseSchema}.{objectQualifier}User WHERE UserID=ici_ToUserID LIMIT 1;
ELSE
	SELECT '';
END IF;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_getthanks 
	(I_MessageID INT)
BEGIN
	SELECT a.ThanksFromUserID as UserID, a.ThanksDate, b.Name, b.DisplayName
	FROM {databaseSchema}.{objectQualifier}Thanks a 
	INNER JOIN {databaseSchema}.{objectQualifier}User b
	ON a.ThanksFromUserID = b.UserID WHERE MessageID = I_MessageID
	ORDER BY a.ThanksDate DESC;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_isthankedbyuser 
	(I_UserID INT,
	I_MessageID INT)

BEGIN
DECLARE ret TINYINT(1) DEFAULT 1;
	IF NOT EXISTS (SELECT ThanksID FROM {databaseSchema}.{objectQualifier}Thanks WHERE ThanksFromUserID=I_UserID AND MessageID=I_MessageID) THEN
		SET ret = 0;
		SELECT ret;
	ELSE
		SELECT ret;
	END IF;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_removethanks 
	(I_FromUserID int,
	I_MessageID int,
	I_UseDisplayName tinyint(1))
	BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}Thanks 
	WHERE ThanksFromUserID=I_FromUserID AND MessageID=I_MessageID;	
	SELECT (CASE WHEN I_UseDisplayName = 1 THEN DisplayName ELSE `Name` END) FROM {databaseSchema}.{objectQualifier}User WHERE UserID IN (SELECT UserID FROM {databaseSchema}.{objectQualifier}Message WHERE MessageID = I_MessageID);
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_thanksnumber 
	(I_MessageID INT)
BEGIN
SELECT Count(1) from {databaseSchema}.{objectQualifier}Thanks WHERE MessageID=I_MessageID;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_getthanks_from 
	(I_UserID INT, I_PageUserID INT)
BEGIN
SELECT Count(1) FROM {databaseSchema}.{objectQualifier}Thanks WHERE ThanksFromUserID=I_UserID;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_getthanks_to 
	(I_UserID			   INT,
	 I_PageUserID          INT,
 OUT I_ThanksToNumber      INT,
 OUT I_ThanksToPostsNumber INT)
BEGIN
	 SET I_ThanksToNumber =(SELECT Count(1) FROM {databaseSchema}.{objectQualifier}Thanks WHERE ThanksToUserID=I_UserID);	
	 SET I_ThanksToPostsNumber =(SELECT Count(DISTINCT MessageID) FROM {databaseSchema}.{objectQualifier}Thanks WHERE ThanksToUserID=I_UserID);	
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_getallthanks 
	(i_MessageIDs longtext)
BEGIN
	DECLARE ici_MessageID VARCHAR(11);
	DECLARE ici_MessageIDsChunk VARCHAR(4000);
	DECLARE ici_Pos int;
	DECLARE ici_Itr int;
	DECLARE ici_trimindex int;
	
	DROP TEMPORARY TABLE  IF EXISTS {objectQualifier}tmp_ParsedMessageIDs;
	CREATE TEMPORARY TABLE IF NOT EXISTS {objectQualifier}tmp_ParsedMessageIDs 
	(
		MessageID INT NOT NULL
	);
	-- drop table is not required with it
	TRUNCATE TABLE {objectQualifier}tmp_ParsedMessageIDs;
		
	SET i_MessageIDs = (CONCAT(TRIM(i_MessageIDs), ','));
	SET ici_Pos = (LOCATE(',', i_MessageIDs, 1));
	IF REPLACE(i_MessageIDs, ',', '') <> '' THEN	
		WHILE ici_Pos > 0 DO		
			SET ici_MessageID = LTRIM(RTRIM(LEFT(i_MessageIDs, ici_Pos - 1)));
			IF ici_MessageID <> '' THEN			
				INSERT INTO {objectQualifier}tmp_ParsedMessageIDs(MessageID) VALUES (CAST(ici_MessageID AS SIGNED)) ;
				-- Use Appropriate conversion
			END IF;
			SET i_MessageIDs = RIGHT(i_MessageIDs, CHAR_LENGTH(i_MessageIDs) - ici_Pos);
			SET ici_Pos = LOCATE(',', i_MessageIDs, 1);
		END WHILE;
		  -- to be sure that last value is inserted
					/* IF (CHAR_LENGTH(ici_MessageID) > 0) THEN
					 INSERT INTO {objectQualifier}tmp_ParsedMessageIDs (MessageID) 
					 VALUES (CAST(ici_MessageID AS SIGNED));  
					END IF; */
	END	IF;

	SELECT b.ThanksFromUserID AS FromUserID, 
		   b.ThanksDate, 
		   a.MessageID, 
		   b.ThanksToUserID AS ToUserID,
		   (SELECT {databaseSchema}.{objectQualifier}biginttoint(COUNT(b.ThanksID)) FROM {databaseSchema}.{objectQualifier}Thanks b WHERE b.ThanksFromUserID=d.UserID) AS ThanksFromUserNumber,
		   (SELECT {databaseSchema}.{objectQualifier}biginttoint(COUNT(b.ThanksID)) FROM {databaseSchema}.{objectQualifier}Thanks b WHERE b.ThanksToUserID=d.UserID) AS ThanksToUserNumber,
		   (SELECT {databaseSchema}.{objectQualifier}biginttoint(COUNT(DISTINCT(b.MessageID))) FROM {databaseSchema}.{objectQualifier}Thanks b WHERE b.ThanksToUserID=d.userID) AS ThanksToUserPostsNumber
	FROM  {databaseSchema}.{objectQualifier}tmp_ParsedMessageIDs a
	INNER JOIN {databaseSchema}.{objectQualifier}Message d 
	ON (d.MessageID=a.MessageID)
	LEFT JOIN {databaseSchema}.{objectQualifier}Thanks b 
	ON (b.MessageID = a.MessageID);
	
	DROP TEMPORARY TABLE  IF EXISTS {objectQualifier}tmp_ParsedMessageIDs;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_viewthanksfrom(
I_UserID int, 
I_PageUserID int,  
i_PageIndex int,
i_PageSize int)
	READS SQL DATA
	BEGIN
		 declare ici_TotalRows int ;
   declare ici_FirstSelectRowNumber int ;
   declare ici_FirstSelectRowID int;	
   set i_PageIndex = i_PageIndex + 1; 

		select  count(1) into  ici_TotalRows FROM   {databaseSchema}.{objectQualifier}thanks t 
				left join {databaseSchema}.{objectQualifier}message c on c.MessageID = t.MessageID
				left join {databaseSchema}.{objectQualifier}topic a on a.TopicID = c.TopicID
				join {databaseSchema}.{objectQualifier}user b on c.userID = b.UserID    
				join {databaseSchema}.{objectQualifier}activeaccess x on (x.ForumID = a.ForumID and x.UserID = I_PageUserID)
		where   x.ReadAccess > 0
				AND x.UserID = I_PageUserID
				-- Message IsApproved
				AND (c.Flags & 16) = 16
				AND a.TopicMovedID IS NULL
				-- Topic IsDeleted
				AND (a.Flags & 8) <> 8
				-- Message IsDeleted
				AND (c.Flags & 8) <> 8
				AND
				 t.ThanksFromUserID = I_UserID ;
	
		 select  (i_PageIndex - 1) * i_PageSize into ici_FirstSelectRowNumber;
	   
   set @uvfpr = CONCAT('select
		t.ThanksFromUserID,
				t.ThanksToUserID,
				c.MessageID,
				a.ForumID,
				a.TopicID,
				a.Topic,
				b.UserID,
				b.Name as UserName,
				b.DisplayName,
				c.MessageID,
				c.Posted,
				c.Message,
				c.Flags,       
		{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
	  FROM   {databaseSchema}.{objectQualifier}thanks t 
				left join {databaseSchema}.{objectQualifier}message c on c.MessageID = t.MessageID
				left join {databaseSchema}.{objectQualifier}topic a on a.TopicID = c.TopicID
				join {databaseSchema}.{objectQualifier}user b on c.UserID = b.UserID    
				join {databaseSchema}.{objectQualifier}activeaccess x on (x.ForumID = a.ForumID and x.UserID = ',I_PageUserID,')
		where   x.ReadAccess > 0
				AND x.UserID = ',I_PageUserID,'
				-- Message IsApproved
				AND (c.Flags & 16) = 16
				AND a.TopicMovedID IS NULL
				-- Topic IsDeleted
				AND (a.Flags & 8) <> 8
				-- Message IsDeleted
				AND (c.Flags & 8) <> 8
				AND
				 t.ThanksFromUserID = ',I_UserID,' ORDER BY c.Posted DESC   LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');
	PREPARE stmt_uvfpr FROM @uvfpr;
	EXECUTE stmt_uvfpr;   
	END;
--GO
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_viewthanksto(
I_UserID int, 
I_PageUserID int,  
i_PageIndex int,
i_PageSize int)
	READS SQL DATA
	BEGIN
		 declare ici_TotalRows int ;
   declare ici_FirstSelectRowNumber int ;
   declare ici_FirstSelectRowID int;	
   set i_PageIndex = i_PageIndex + 1; 

		select  count(1) into  ici_TotalRows FROM   {databaseSchema}.{objectQualifier}thanks t 
				left join {databaseSchema}.{objectQualifier}message c on c.MessageID = t.MessageID
				left join {databaseSchema}.{objectQualifier}topic a on a.TopicID = c.TopicID
				join {databaseSchema}.{objectQualifier}user b on c.UserID = b.UserID    
				join {databaseSchema}.{objectQualifier}activeaccess x on (x.ForumID = a.ForumID and x.UserID = I_PageUserID)
		where   x.ReadAccess > 0
				AND x.UserID = I_PageUserID
				-- Message IsApproved
				AND (c.Flags & 16) = 16
				AND a.TopicMovedID IS NULL
				-- Topic IsDeleted
				AND (a.Flags & 8) <> 8
				-- Message IsDeleted
				AND (c.Flags & 8) <> 8
				AND
				t.thankstouserID = I_UserID ;
	
		 select  (i_PageIndex - 1) * i_PageSize into ici_FirstSelectRowNumber;
	   
   set @uvfpr = CONCAT('select
		t.ThanksFromUserID,
				t.ThanksToUserID,
				c.MessageID,
				a.ForumID,
				a.TopicID,
				a.Topic,
				b.UserID,
				b.DisplayName,
				b.Name as UserName,
				c.MessageID,
				c.Posted,
				c.Message,
				c.Flags,       
		{databaseSchema}.{objectQualifier}biginttoint(',ici_TotalRows,') AS TotalRows
	  FROM   {databaseSchema}.{objectQualifier}thanks t 
				left join {databaseSchema}.{objectQualifier}message c on c.MessageID = t.MessageID
				left join {databaseSchema}.{objectQualifier}topic a on a.TopicID = c.TopicID
				join {databaseSchema}.{objectQualifier}user b on c.UserID = b.UserID    
				join {databaseSchema}.{objectQualifier}activeaccess x on (x.ForumID = a.ForumID and x.UserID = ',I_PageUserID,')
		where   x.ReadAccess > 0
				AND x.UserID = ',I_PageUserID,'
				-- Message IsApproved
				AND (c.Flags & 16) = 16
				AND a.TopicMovedID IS NULL
				-- Topic IsDeleted
				AND (a.Flags & 8) <> 8
				-- Message IsDeleted
				AND (c.Flags & 8) <> 8
				AND
				t.thankstouserID = ',I_UserID,' 
				ORDER BY c.Posted DESC   LIMIT ',ici_FirstSelectRowNumber,',',i_PageSize,'');
	PREPARE stmt_uvfpr FROM @uvfpr;
	EXECUTE stmt_uvfpr;   
	END;
--GO


/* End of procedures for "Thanks" Mod */

/* Stored procedures for Buddy feature */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}buddy_addrequest
	( i_FromUserID INT,
	i_ToUserID INT,
	i_UseDisplayName TINYINT(1),
	i_UTCTIMESTAMP DATETIME
	) 
	 MODIFIES SQL DATA
	 BEGIN
	 DECLARE i_approved TINYINT(1);
	 DECLARE i_paramOutput VARCHAR(128);
		IF NOT EXISTS ( SELECT  ID
						FROM    {databaseSchema}.{objectQualifier}Buddy
						WHERE   ( FromUserID = i_FromUserID
								  AND ToUserID = i_ToUserID
								) ) 
			THEN
				IF ( NOT EXISTS ( SELECT    ID
								  FROM      {databaseSchema}.{objectQualifier}Buddy
								  WHERE     ( FromUserID = i_ToUserID
											  AND ToUserID = i_FromUserID
											) )
				   ) 
					THEN
						INSERT  INTO {databaseSchema}.{objectQualifier}Buddy
								(
								  FromUserID,
								 ToUserID,
								  Approved,
								  Requested
								)
						VALUES  (
								  i_FromUserID,
								  i_ToUserID,
								  0,
								  i_UTCTIMESTAMP
								);
						SET i_paramOutput = ( SELECT (CASE WHEN i_UseDisplayName = 1 THEN `DisplayName` ELSE `Name` END)
											 FROM   {databaseSchema}.{objectQualifier}User
											 WHERE  ( UserID = i_ToUserID )
										   );
						SET i_approved = 0;
					ELSE 
					
						INSERT  INTO {databaseSchema}.{objectQualifier}Buddy
								(
								  FromUserID,
								  ToUserID,
								  Approved,
								  Requested
								)
						VALUES  (
								  i_FromUserID,
								  i_ToUserID,
								  1,
								  i_UTCTIMESTAMP
								);
						UPDATE  {databaseSchema}.{objectQualifier}Buddy
						SET     Approved = 1
						WHERE   ( FromUserID = i_ToUserID
								  AND ToUserID = i_FromUserID
								);
						SET i_paramOutput = ( SELECT (CASE WHEN i_UseDisplayName = 1 THEN `DisplayName` ELSE `Name` END)
											 FROM   {databaseSchema}.{objectQualifier}User
											 WHERE  ( UserID = i_ToUserID )
										   );
						SET i_approved = 1;
					
			END	IF;
		ELSE            
				SET i_paramOutput = '';
				SET i_approved = 0;
		END IF;
		SELECT i_approved,i_paramOutput;
	END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}buddy_approverequest
	(i_FromUserID INT,
	i_ToUserID INT,
	i_Mutual TINYINT(1),
	i_UseDisplayName TINYINT(1),
	i_UTCTIMESTAMP DATETIME)     
	MODIFIES SQL DATA
	BEGIN
	DECLARE i_paramOutput VARCHAR(255);
		IF EXISTS ( SELECT  ID
					FROM    {databaseSchema}.{objectQualifier}Buddy
					WHERE   ( FromUserID = i_FromUserID
							  AND ToUserID = i_ToUserID
							) ) 
			THEN
				UPDATE  {databaseSchema}.{objectQualifier}Buddy
				SET     Approved = 1
				WHERE   ( FromUserID = i_FromUserID
						  AND ToUserID = i_ToUserID
						);
				SET i_paramOutput = ( SELECT (CASE WHEN i_UseDisplayName = 1 THEN `DisplayName` ELSE `Name` END)
									 FROM   {databaseSchema}.{objectQualifier}User
									 WHERE  ( UserID = i_FromUserID )
								   );                                   
				IF ( i_Mutual = 1 )
					AND ( NOT EXISTS ( SELECT   ID
									   FROM     {databaseSchema}.{objectQualifier}Buddy
									   WHERE    FromUserID = i_ToUserID
												AND ToUserID = i_FromUserID )
						) THEN
					INSERT  INTO {databaseSchema}.{objectQualifier}Buddy
							(
							  FromUserID,
							  ToUserID,
							  Approved,
							  Requested
							)
					VALUES  (
							  i_ToUserID,
							  i_FromUserID,
							  1,
							  i_UTCTIMESTAMP
							);
			END IF;
	END IF;
	SELECT i_paramOutput;
END;	
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}buddy_list ( i_FromUserID INT )
	READS SQL DATA
	BEGIN    
		SELECT  a.UserID,
				a.BoardID,
				a.`Name`,
				a.`DisplayName`,
				a.Joined,
				a.NumPosts,
				b.`Name` AS RankName,
				c.Approved,
				c.FromUserID,
				c.Requested
		FROM   {databaseSchema}.{objectQualifier}User a
				JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID = a.RankID
				JOIN {databaseSchema}.{objectQualifier}Buddy c ON ( c.ToUserID = a.UserID
											  AND c.FromUserID = i_FromUserID
											)
		UNION
		SELECT  i_FromUserID AS UserID,
				a.BoardID,
				a.`Name`,
				a.`DisplayName`,
				a.Joined,
				a.NumPosts,
				b.`Name` AS RankName,
				c.Approved,
				c.FromUserID,
				c.Requested
		FROM    {databaseSchema}.{objectQualifier}User a
				JOIN {databaseSchema}.{objectQualifier}Rank b ON b.RankID = a.RankID
				JOIN {databaseSchema}.{objectQualifier}Buddy c ON ( ( c.Approved = 0 )
											  AND ( c.ToUserID = i_FromUserID )
											  AND ( a.UserID = c.FromUserID )
											)
		ORDER BY `Name`;
	END;
	--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}buddy_remove
(    i_FromUserID INT,
	i_ToUserID INT,
	i_UseDisplayName TINYINT(1))
	MODIFIES SQL DATA
	BEGIN
		DELETE  FROM {databaseSchema}.{objectQualifier}Buddy
		WHERE   ( FromUserID = i_FromUserID
				  AND ToUserID = i_ToUserID
				);
		 SELECT (CASE WHEN i_UseDisplayName = 1 THEN `DisplayName` ELSE `Name` END)
							 FROM   {databaseSchema}.{objectQualifier}User
							 WHERE  ( UserID = i_ToUserID );
	END;
	--GO    

CREATE PROCEDURE {databaseSchema}.{objectQualifier}buddy_denyrequest
	( i_FromUserID INT,
	i_ToUserID INT,
	i_UseDisplayName TINYINT(1))
	MODIFIES SQL DATA
	BEGIN
		DELETE  FROM {databaseSchema}.{objectQualifier}Buddy
		WHERE   FromUserID = i_FromUserID
				AND ToUserID = i_ToUserID;
		 SELECT (CASE WHEN i_UseDisplayName = 1 THEN `DisplayName` ELSE `Name` END)
							 FROM   {databaseSchema}.{objectQualifier}User
							 WHERE  ( UserID = i_FromUserID );
	END;
--GO   
/* End of stored procedures for Buddy feature */

CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_favorite_add 
	( i_UserID int,
	i_TopicID int)
MODIFIES SQL DATA
BEGIN
	IF NOT EXISTS (SELECT ID FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE (UserID = i_UserID AND TopicID=i_TopicID))
	THEN  
		INSERT INTO {databaseSchema}.{objectQualifier}FavoriteTopic(UserID, TopicID) Values 
								(i_UserID, i_TopicID);
	END IF;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_favorite_remove 
(	i_UserID int,
	i_TopicID int)
MODIFIES SQL DATA
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE UserID=i_UserID AND TopicID=i_TopicID;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_favorite_list(i_UserID int)
READS SQL DATA
BEGIN
SELECT TopicID FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE UserID=i_UserID;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_favorite_details
(
	i_BoardID INT,
	i_CategoryID INT,
	i_PageUserID INT,
	i_SinceDate DATETIME,
	i_ToDate DATETIME,
	i_PageIndex INT, 	
	i_PageSize INT,
	i_StyledNicks TINYINT(1),
	i_FindLastUnread TINYINT(1),
	i_UTCTIMESTAMP DATETIME 	 
	)
BEGIN
 DECLARE  ici_post_totalrowsnumber INT; 
 DECLARE  ici_firstselectrownum INT;   
 DECLARE  ici_firstselectposted DATETIME; 

 SET i_PageIndex = i_PageIndex + 1;	

		
	-- find total returned count
			select
		COUNT(c.TopicID)
		INTO ici_post_totalrowsnumber
		from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category e on e.CategoryID=d.CategoryID
		JOIN {databaseSchema}.{objectQualifier}FavoriteTopic z ON (z.TopicID=c.TopicID AND z.UserID = i_PageUserID)
	where
			(c.LastPosted BETWEEN i_SinceDate AND i_ToDate) and
			x.UserID = i_PageUserID and
		CAST(x.ReadAccess AS SIGNED) <> 0 and
		e.BoardID = i_BoardID and
		(i_CategoryID is null or e.CategoryID=i_CategoryID) and
		(c.Flags & 8) <> 8 	
	order by
		c.LastPosted desc,
		d.Name asc,
		c.Priority desc;	

	
	  select (i_PageIndex-1) * i_PageSize INTO ici_firstselectrownum;

	 

	 if (ici_firstselectrownum > 0)
	 then
	 select ici_firstselectrownum 
	 into ici_firstselectrownum;
	 end  if;
	
SET @i_FirstSelectLastPosted = null;
SET @i_FirstSelectPosted = null;

SET @tlist2_rec = CONCAT('select
c.LastPosted,
c.Posted
INTO @i_FirstSelectLastPosted, @i_FirstSelectPosted
		from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category e on e.CategoryID=d.CategoryID
		JOIN {databaseSchema}.{objectQualifier}FavoriteTopic z ON (z.TopicID=c.TopicID AND z.UserID= ',i_PageUserID,' )
	where
		(c.LastPosted BETWEEN ''',i_SinceDate,''' AND ''',i_ToDate,''') and
		x.UserID = ',i_PageUserID,' and
		x.ReadAccess <> 0 and 
		e.BoardID = ',i_BoardID,' and
		(',COALESCE(i_CategoryID,0),' = 0 or e.CategoryID=',COALESCE(i_CategoryID,0),') and
		c.Flags & 8 <> 8
	order by
		c.LastPosted desc,
		d.Name asc,
		c.Priority desc
		LIMIT 1 OFFSET ',ici_firstselectrownum,';');  
		PREPARE tlist2 FROM  @tlist2_rec;
		EXECUTE tlist2;		 
		DEALLOCATE PREPARE tlist2;		 		
			
   SET @tlist1_rec =
	CONCAT('select
		c.ForumID,
		c.TopicID,
		c.TopicMovedID,
		c.Posted,
		c.`Status`,
		c.Styles,
		IFNULL(c.TopicMovedID,c.TopicID) AS LinkTopicID,
		c.Topic AS Subject,
		c.Description,
		c.UserID,
		IFNULL(c.UserName,b.Name) AS Starter,
		IFNULL(c.UserDisplayName,b.DisplayName) AS StarterDisplay,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message mes WHERE mes.TopicID = c.TopicID AND SIGN(mes.Flags & 8) = 1 AND SIGN(mes.Flags & 16) = 1 AND ((',i_PageUserID,' IS NOT NULL AND mes.UserID = ',i_PageUserID,') OR (',i_PageUserID,' IS NULL)) ) AS NumPostsDeleted,
		(select count(1) - 1  from {databaseSchema}.{objectQualifier}Message x where x.TopicID=c.TopicID and (x.Flags & 8)=0) AS Replies,
		c.Views,
		c.LastPosted,
		c.LastUserID,
		IFNULL(c.LastUserName,(select x.Name from {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserName,
		IFNULL(c.LastUserDisplayName,(select x.DisplayName from {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) AS LastUserDisplayName,
		c.LastMessageID,
		c.LastMessageFlags AS LastMessageFlags,
		c.TopicID AS LastTopicID,
		c.Flags AS TopicFlags,
		(SELECT COUNT(ID) FROM {databaseSchema}.{objectQualifier}FavoriteTopic 
		WHERE TopicId = IfNull(c.TopicMovedID,c.TopicID)) as FavoriteCount ,
		c.Priority,
		c.PollID,
		d.Name AS ForumName,
		c.TopicMovedID,
		d.Flags AS ForumFlags,
		(SELECT `Message` FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(c.TopicMovedID,c.TopicID) AND mes2.Position = 0) AS FirstMessage,
		(case(',i_StyledNicks,')
			when 1 then  b.UserStyle 
			else ''''	 end) AS StarterStyle,
		(case(',i_StyledNicks,')
			when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=c.LastUserID)  
			else ''''	 end) AS LastUserStyle,
		(case(',i_FindLastUnread,')
			 when 1 then
			   (SELECT  CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=d.ForumID AND x.UserID = ',i_PageUserID,' limit 1)
			 else CAST(NULL AS DATETIME)	 end) as LastForumAccess,
		(case(',i_FindLastUnread,')
			 when 1 then
			   (SELECT  CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = ',i_PageUserID,' limit 1)
			 else CAST(NULL AS DATETIME)	 end) as LastTopicAccess,
		(SELECT GROUP_CONCAT(tg.tag separator '','') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = c.TopicID) AS TopicTags			 , 
			 c.TopicImage,
			 c.TopicImageType,
			 c.TopicImageBin,
			 0 as HasAttachments, 	
			{databaseSchema}.{objectQualifier}biginttoint(',ici_post_totalrowsnumber,') AS TotalRows,
			{databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex 
	from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category e on e.CategoryID=d.CategoryID
		JOIN {databaseSchema}.{objectQualifier}FavoriteTopic z ON (z.TopicID=c.TopicID AND z.UserID = ',i_PageUserID,')
	where
		c.LastPosted <= (''',COALESCE(@i_FirstSelectPosted,UTC_TIMESTAMP()),''') and
		x.UserID = ',i_PageUserID,' and
		x.ReadAccess <> 0 and 
		e.BoardID = ',i_BoardID,' and
		(',COALESCE(i_CategoryID,0),' = 0 or e.CategoryID=',COALESCE(i_CategoryID,0),') and
		c.Flags & 8 <> 8
	order by
		c.LastPosted desc,
		d.Name asc,
		c.Priority desc
		  LIMIT ',i_PageSize,';'); 
		PREPARE tlist1 FROM @tlist1_rec;		
		EXECUTE tlist1;		 
		DEALLOCATE PREPARE tlist1;	
END;
--GO

-- Albums 
CREATE procedure {databaseSchema}.{objectQualifier}album_save
	(
	  i_AlbumID INT,
	  i_UserID INT,
	  i_Title VARCHAR(255),
	  i_CoverImageID INT,
	  i_UTCTIMESTAMP DATETIME
	)
	MODIFIES SQL DATA
	BEGIN 
	DECLARE ici_AlbumInserted INT DEFAULT 0;   
		-- Update Cover?
		IF ( i_CoverImageID IS NOT NULL
			 AND i_CoverImageID <> 0
		   ) THEN
			UPDATE  {databaseSchema}.{objectQualifier}UserAlbum
			SET     CoverImageID = i_CoverImageID
			WHERE   AlbumID = i_AlbumID;
		ELSE 
			-- Remove Cover?
			IF ( i_CoverImageID = 0 )  THEN
				UPDATE  {databaseSchema}.{objectQualifier}UserAlbum
				SET     CoverImageID = NULL
				WHERE   AlbumID = i_AlbumID;            
			ELSE 
			-- Update Title?
				IF i_AlbumID is not null THEN
					UPDATE  {databaseSchema}.{objectQualifier}UserAlbum
					SET     Title = i_Title
					WHERE   AlbumID = i_AlbumID;
				ELSE                    
					-- New album. insert into table.
						INSERT  INTO {databaseSchema}.{objectQualifier}UserAlbum
								(
								  UserID,
								  Title,
								  CoverImageID,
								  Updated
								)
						VALUES  (
								  i_UserID,
								  i_Title,
								  i_CoverImageID,
								  i_UTCTIMESTAMP
								);
					   SET ici_AlbumInserted = (SELECT LAST_INSERT_ID());       
				END IF;
				END IF;
				END IF;
				SELECT ici_AlbumInserted;       
end;
--GO
   
CREATE procedure {databaseSchema}.{objectQualifier}album_list
	(
	  i_UserID INT,
	  i_AlbumID INT
	)
	READS SQL DATA
	BEGIN
		IF i_UserID IS NOT NULL THEN
			select  *
			FROM    {databaseSchema}.{objectQualifier}UserAlbum
			WHERE   UserID = i_UserID
			ORDER BY Updated DESC;
		ELSE 
			SELECT  *
			FROM    {databaseSchema}.{objectQualifier}UserAlbum
			WHERE   AlbumID = i_AlbumID;
		END IF;
 end;
--GO
   
CREATE PROCEDURE {databaseSchema}.{objectQualifier}album_delete ( i_AlbumID int )
MODIFIES SQL DATA
BEGIN
		DELETE  FROM {databaseSchema}.{objectQualifier}UserAlbumImage
		WHERE   AlbumID = i_AlbumID;
		DELETE  FROM {databaseSchema}.{objectQualifier}UserAlbum
		WHERE   AlbumID = i_AlbumID;        
END;
--GO
   
CREATE PROCEDURE {databaseSchema}.{objectQualifier}album_gettitle
	(
	  i_AlbumID INT 
	)
	READS SQL DATA
	BEGIN
		 SELECT `Title`
		  FROM   {databaseSchema}.{objectQualifier}UserAlbum
			WHERE  AlbumID = i_AlbumID;
						   
	END;
--GO
   
CREATE PROCEDURE {databaseSchema}.{objectQualifier}album_getstats
	( i_UserID INT,
	i_AlbumID INT )
	READS SQL DATA
	BEGIN
	DECLARE  i_AlbumNumber INT DEFAULT 0;
	DECLARE  i_ImageNumber INT DEFAULT 0; 
	
		IF i_AlbumID IS NOT NULL  THEN
			SET i_ImageNumber = ( SELECT COUNT(ImageID)
								 FROM   {databaseSchema}.{objectQualifier}UserAlbumImage
								 WHERE  AlbumID = i_AlbumID
							   );
		ELSE            
				SET i_AlbumNumber = ( SELECT COUNT(AlbumID)
									 FROM   {databaseSchema}.{objectQualifier}UserAlbum
									 WHERE  UserID = i_UserID
								   );
				SET i_ImageNumber = ( SELECT COUNT(ImageID)
									 FROM   {databaseSchema}.{objectQualifier}UserAlbumImage
									 WHERE  AlbumID in (
											SELECT  AlbumID
											FROM    {databaseSchema}.{objectQualifier}UserAlbum
											WHERE   UserID = i_UserID )
								   );
		 END IF;
			SELECT i_AlbumNumber as AlbumNumber, i_ImageNumber as ImageNumber;
 end;
--GO  
CREATE PROCEDURE {databaseSchema}.{objectQualifier}album_image_save
	(
	  i_ImageID INT,
	  i_AlbumID INT,
	  i_Caption VARCHAR(255),
	  i_FileName VARCHAR(255),
	  i_Bytes INT,
	  i_ContentType VARCHAR(50),
	  i_UTCTIMESTAMP DATETIME
	)
	MODIFIES SQL DATA
	BEGIN
		IF i_ImageID is not null  THEN
			UPDATE  {databaseSchema}.{objectQualifier}UserAlbumImage
			SET     Caption = i_Caption
			WHERE   ImageID = i_ImageID;
		ELSE
			INSERT  INTO {databaseSchema}.{objectQualifier}UserAlbumImage
					(
					  AlbumID,
					  Caption,
					  FileName,
					  Bytes,
					  ContentType,
					  Uploaded,
					  Downloads
					)
			VALUES  (
					  i_AlbumID,
					  i_Caption,
					  i_FileName,
					  i_Bytes,
					  i_ContentType,
					  i_UTCTIMESTAMP,
					  0
					);
		END IF;
end;
--GO
	
CREATE procedure {databaseSchema}.{objectQualifier}album_image_list
	(
	  i_AlbumID INT,
	  i_ImageID INT
	)
	READS SQL DATA
	BEGIN
		IF i_AlbumID IS NOT null THEN
			SELECT  *
			FROM    {databaseSchema}.{objectQualifier}UserAlbumImage
			WHERE   AlbumID = i_AlbumID
			ORDER BY Uploaded DESC;
		ELSE 
			SELECT  a.*,
					b.UserID
			FROM    {databaseSchema}.{objectQualifier}UserAlbumImage a
					INNER JOIN {databaseSchema}.{objectQualifier}UserAlbum b ON b.AlbumID = a.AlbumID
			WHERE   ImageID = i_ImageID;
		END IF;
end;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}album_images_by_user(i_UserID INT)
	READS SQL DATA
	BEGIN        
			SELECT  *
			FROM    {databaseSchema}.{objectQualifier}UserAlbumImage uai
			INNER JOIN {databaseSchema}.{objectQualifier}UserAlbum ua
			WHERE   ua.UserID = i_UserID
			ORDER BY ua.AlbumID,uai.ImageID DESC;       
end;
--GO


CREATE PROCEDURE {databaseSchema}.{objectQualifier}album_image_delete ( i_ImageID INT )
	MODIFIES SQL DATA
	BEGIN
		DELETE  FROM {databaseSchema}.{objectQualifier}UserAlbumImage
		WHERE   ImageID = i_ImageID;
		UPDATE  {databaseSchema}.{objectQualifier}UserAlbum
		SET     CoverImageID = NULL
		WHERE   CoverImageID = i_ImageID;      
end;
--GO
  
CREATE PROCEDURE {databaseSchema}.{objectQualifier}album_image_download ( i_ImageID INT )
	MODIFIES SQL DATA
	BEGIN
		UPDATE  {databaseSchema}.{objectQualifier}UserAlbumImage
		SET     Downloads = Downloads + 1
		WHERE   ImageID = i_ImageID;
 end;
--GO
   

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_getsignaturedata(i_BoardID INT, i_UserID INT)
READS SQL DATA
BEGIN
	-- Ugly but bullet proof - it used very rarely

	DECLARE i_G_UsrSigChars int;
	DECLARE i_G_UsrSigBBCodes VARCHAR(4000);
	DECLARE i_G_UsrSigHTMLTags VARCHAR(4000);
	 
	DECLARE i_R_UsrSigChars int;
	DECLARE i_R_UsrSigBBCodes VARCHAR(4000);
	DECLARE i_R_UsrSigHTMLTags VARCHAR(4000); 

	-- for cursors
	DECLARE i_tmp_UsrSigChars int;
	DECLARE i_tmp_UsrSigBBCodes VARCHAR(4000);
	DECLARE i_tmp_UsrSigHTMLTags VARCHAR(4000);
	
	
  DECLARE sig_cursor CURSOR  FOR
  SELECT IFNULL(c.UsrSigChars,0), IFNULL(c.UsrSigBBCodes,''), IFNULL(c.UsrSigHTMLTags,'')
	  FROM {databaseSchema}.{objectQualifier}User a 
						JOIN {databaseSchema}.{objectQualifier}UserGroup b
						  ON a.UserID = b.UserID
							JOIN {databaseSchema}.{objectQualifier}Group c                         
							  ON b.GroupID = c.GroupID 
							   WHERE a.UserID = i_UserID AND c.BoardID = i_BoardID 
								ORDER BY c.SortOrder ASC;


   OPEN sig_cursor;
   BEGIN
   DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END;
   LOOP
	FETCH sig_cursor  INTO i_tmp_UsrSigChars, i_tmp_UsrSigBBCodes, i_tmp_UsrSigHTMLTags;
  
	if i_G_UsrSigChars IS NULL then		
		-- first check ranks		
		SELECT  IFNULL(c.UsrSigChars,0), c.UsrSigBBCodes, c.UsrSigHTMLTags
		INTO i_R_UsrSigChars, i_R_UsrSigBBCodes, i_R_UsrSigHTMLTags 
		FROM {databaseSchema}.{objectQualifier}Rank c 
								JOIN {databaseSchema}.{objectQualifier}User d
								  ON c.RankID = d.RankID
								   WHERE d.UserID = i_UserID AND c.BoardID = i_BoardID 
								   ORDER BY c.RankID DESC LIMIT 1;

		-- compare with rank data	   
		SELECT (CASE WHEN i_R_UsrSigChars > IFNULL(i_tmp_UsrSigChars,0) THEN i_R_UsrSigChars ELSE IFNULL(i_tmp_UsrSigChars,0) END) 
		INTO i_G_UsrSigChars;
		SELECT CONCAT(COALESCE(CONCAT(i_R_UsrSigBBCodes ,','),''), COALESCE(i_tmp_UsrSigBBCodes,'')) 
		INTO i_G_UsrSigBBCodes;
		SELECT CONCAT(COALESCE(CONCAT(i_R_UsrSigHTMLTags,','),''), COALESCE(i_tmp_UsrSigHTMLTags, '')) 
		INTO i_G_UsrSigHTMLTags;		
		else		
		SELECT  (CASE WHEN i_G_UsrSigChars > COALESCE(i_tmp_UsrSigChars, 0) THEN i_G_UsrSigChars ELSE COALESCE(i_tmp_UsrSigChars, 0) END) 
		INTO i_G_UsrSigChars;
		SELECT  CONCAT(COALESCE(CONCAT(i_tmp_UsrSigBBCodes, ','),''), i_G_UsrSigBBCodes) 
		INTO i_G_UsrSigBBCodes; 
		SELECT CONCAT(COALESCE(CONCAT(i_tmp_UsrSigHTMLTags , ','), ''), i_G_UsrSigHTMLTags)
		INTO i_G_UsrSigHTMLTags;	
		end if;
  END LOOP;  
  END;
  CLOSE sig_cursor;

	SELECT 
		i_G_UsrSigChars AS UsrSigChars, 
		i_G_UsrSigBBCodes AS UsrSigBBCodes, 
		i_G_UsrSigHTMLTags AS UsrSigHTMLTags;
	END;
	--GO 

 CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_getalbumsdata(i_BoardID INT, i_UserID INT )
	READS SQL DATA
	BEGIN     
	DECLARE i_OR_UsrAlbums int;    
	DECLARE i_OG_UsrAlbums int;
	DECLARE i_OR_UsrAlbumImages int;     
	DECLARE i_OG_UsrAlbumImages int; 


	SELECT IFNULL(c.UsrAlbums,0), IFNULL(c.UsrAlbumImages,0) 
	INTO  i_OG_UsrAlbums, i_OG_UsrAlbumImages 
	FROM {databaseSchema}.{objectQualifier}User a 
						JOIN {databaseSchema}.{objectQualifier}UserGroup b
						  ON a.UserID = b.UserID
							JOIN {databaseSchema}.{objectQualifier}Group c                         
							  ON b.GroupID = c.GroupID 
							  WHERE a.UserID = i_UserID AND c.BoardID = i_BoardID 
							  ORDER BY c.SortOrder ASC LIMIT 1;
	 
	 SELECT  IFNULL(c.UsrAlbums,0), IFNULL(c.UsrAlbumImages,0) 
	 INTO  i_OR_UsrAlbums, i_OR_UsrAlbumImages 
	 FROM {databaseSchema}.{objectQualifier}Rank c 
								JOIN {databaseSchema}.{objectQualifier}User d
								  ON c.RankID = d.RankID WHERE d.UserID = i_UserID 
								  AND c.BoardID = i_BoardID 
								  ORDER BY c.RankID DESC LIMIT 1; 
	   
	   if (i_OG_UsrAlbums > i_OR_UsrAlbums)
	   then
	   SET i_OR_UsrAlbums = i_OG_UsrAlbums;
	   end if;
	   if (i_OG_UsrAlbumImages > i_OR_UsrAlbumImages)
	   then
	   SET i_OR_UsrAlbumImages = i_OG_UsrAlbumImages;
	   end if;                 
	  
	  SELECT
	   (SELECT COUNT(ua.AlbumID) FROM {databaseSchema}.{objectQualifier}UserAlbum ua
	   WHERE ua.UserID = i_UserID) AS NumAlbums,
	   (SELECT COUNT(uai.ImageID) FROM  {databaseSchema}.{objectQualifier}UserAlbumImage uai
	   INNER JOIN {databaseSchema}.{objectQualifier}UserAlbum ua
	   ON ua.AlbumID = uai.AlbumID
	   WHERE ua.UserID = i_UserID) AS NumImages, 
	   i_OR_UsrAlbums AS UsrAlbums, 
	   i_OR_UsrAlbumImages AS UsrAlbumImages ;            
	
	END;
	--GO  
 
   CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_secdata(i_MessageID int, i_PageUserID int)
	BEGIN
	-- BoardID=@BoardID and
if (i_PageUserID is null) THEN
select UserID INTO i_PageUserID from {databaseSchema}.{objectQualifier}User where  (Flags & 4)<>0 ORDER BY Joined DESC LIMIT 1;
END IF;
		SELECT
		m.MessageID,
		m.UserID,
		IFNULL(t.UserName, u.Name) as `Name`,
		IFNULL(t.UserDisplayName, u.DisplayName) as UserDisplayName,
		m.Message,
		m.Posted,
		t.TopicID,
		t.ForumID,
		t.Topic,
		t.Priority,
		m.Flags,		
		IFNULL(m.Edited,m.Posted) AS Edited,
		IFNULL(m.EditedBy,m.UserID) AS EditedBy, 		
		t.Flags AS TopicFlags,		
		m.EditReason,
		m.Position,
		m.IsModeratorChanged,
		m.DeleteReason,
		m.BlogPostID,
		t.PollID,
		m.IP
	FROM		
		{databaseSchema}.{objectQualifier}Topic t 
		join  {databaseSchema}.{objectQualifier}Message m ON m.TopicID = t.TopicID
		join  {databaseSchema}.{objectQualifier}User u ON u.UserID = t.UserID
		left join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=IFNULL(t.ForumID,0)
	WHERE
	m.MessageID = i_MessageID AND x.UserID=i_PageUserID  AND  CAST(x.ReadAccess AS SIGNED) > 0;
END;
--GO 

CREATE PROCEDURE {databaseSchema}.{objectQualifier}messagehistory_list (i_MessageID INT, i_DaysToClean INT, i_UTCTIMESTAMP DATETIME)
	MODIFIES SQL DATA
	BEGIN    
	-- delete all message variants older then DaysToClean days Flags reserved for possible pms
   
	delete from {databaseSchema}.{objectQualifier}MessageHistory
	 where DATEDIFF(UTC_DATE(),Edited) > i_DaysToClean;
	
	-- we don't return Message text and ip if it's simply a user       
		  
	 SELECT mh.*, m.UserID, m.UserName, m.UserDisplayName, u.UserStyle, t.ForumID, t.TopicID, t.Topic, m.Posted
	 FROM {databaseSchema}.{objectQualifier}MessageHistory mh
	 LEFT JOIN {databaseSchema}.{objectQualifier}Message m ON m.MessageID = mh.MessageID
	 LEFT JOIN {databaseSchema}.{objectQualifier}Topic t ON t.TopicID = m.TopicID
	 LEFT JOIN {databaseSchema}.{objectQualifier}User u ON u.UserID = t.UserID
	 WHERE mh.MessageID = i_MessageID  order by mh.Edited, mh.MessageID;      
END;
--GO 

CREATE procedure {databaseSchema}.{objectQualifier}user_lazydata(
	I_UserID int,
	I_BoardID int,
	I_ShowPendingMails tinyint(1),
	I_ShowPendingBuddies tinyint(1),
	I_ShowUnreadPMs tinyint(1),
	I_ShowUserAlbums tinyint(1),
	I_ShowUserStyle tinyint(1)
	
)
READS SQL DATA
begin 
	declare G_UsrAlbums int default 0;
	declare R_UsrAlbums int default 0;
	declare ici_UsrPersonalGroups int default 0;
	declare ici_UsrPersonalMasks int default 0;
	declare ici_UsrPersonalForums int default 0;
							   
   SELECT IFNULL(MAX(c.UsrPersonalGroups),0)
   INTO  ici_UsrPersonalGroups
	FROM {databaseSchema}.{objectQualifier}User a 
						JOIN {databaseSchema}.{objectQualifier}UserGroup b
						  ON a.UserID = b.UserID
							JOIN {databaseSchema}.{objectQualifier}Group c                         
							  ON b.GroupID = c.GroupID 
							  WHERE a.UserID = I_UserID AND a.BoardID = I_BoardID 
							  LIMIT 1;    
SELECT IFNULL(MAX(c.UsrPersonalMasks),0),IFNULL(MAX(c.UsrPersonalForums),0) 
   INTO  ici_UsrPersonalMasks,ici_UsrPersonalForums
	FROM {databaseSchema}.{objectQualifier}User a 
						JOIN {databaseSchema}.{objectQualifier}UserGroup b
						  ON a.UserID = b.UserID
							JOIN {databaseSchema}.{objectQualifier}Group c                         
							  ON b.GroupID = c.GroupID 
							  WHERE a.UserID = I_UserID AND a.BoardID = I_BoardID 
							  LIMIT 1;    


	IF (I_ShowUserAlbums > 0) THEN	
	SELECT IFNULL(MAX(r.UsrAlbums),0) INTO R_UsrAlbums
	FROM {databaseSchema}.{objectQualifier}User u 
		INNER JOIN  {databaseSchema}.{objectQualifier}Rank r 
		ON  r.RankID = u.RankID
		WHERE u.UserID = I_UserID AND u.BoardID = I_BoardID LIMIT 1;
		
	SELECT IFNULL(MAX(c.UsrAlbums),0) INTO  G_UsrAlbums 
	FROM {databaseSchema}.{objectQualifier}User a 
						JOIN {databaseSchema}.{objectQualifier}UserGroup b
						  ON a.UserID = b.UserID
							JOIN {databaseSchema}.{objectQualifier}Group c                         
							  ON b.GroupID = c.GroupID 
							  WHERE a.UserID = I_UserID AND a.BoardID = I_BoardID 
							  ORDER BY c.SortOrder ASC LIMIT 1 ;
	ELSE
	SET G_UsrAlbums = 0;
	SET R_UsrAlbums = 0;				    
	END IF; 					
													

	-- return information
	select 		
		a.ProviderUserKey,
		a.Flags AS UserFlags,
		a.Name AS UserName,
		a.DisplayName AS DisplayName,
		a.Suspended,
		a.UseSingleSignOn,
		a.ThemeFile,
		a.LanguageFile,
		a.TextEditor,
		a.TimeZone AS TimeZoneUser,
		a.IsFacebookUser,
		a.IsTwitterUser,
		a.Culture AS CultureUser,
		SIGN(a.Flags & 64) AS IsDirty,		
		SIGN(a.Flags & 4) AS IsGuest, 
		(select count(1) from {databaseSchema}.{objectQualifier}Mail) AS MailsPending,
		/* IsRead and IsDeleted bits */
		(CASE WHEN i_ShowUnreadPMs > 0 THEN (SELECT count(1) FROM {databaseSchema}.{objectQualifier}UserPMessage b  where
		 b.UserID=I_UserID AND b.IsRead=0  AND b.IsDeleted=0 AND b.IsArchived=0
		 ) ELSE 0 END) AS UnreadPrivate,
		(CASE WHEN i_ShowUnreadPMs > 0 THEN (SELECT Created FROM {databaseSchema}.{objectQualifier}PMessage pm 
		INNER JOIN {databaseSchema}.{objectQualifier}UserPMessage upm 
		ON pm.PMessageID = upm.PMessageID 
		WHERE upm.UserID=a.UserID and upm.IsRead=0   AND upm.IsDeleted=0 AND upm.IsArchived=0 
		ORDER BY pm.Created DESC LIMIT 1) ELSE NULL END) AS LastUnreadPm,
		CASE WHEN i_ShowPendingBuddies > 0 THEN (SELECT COUNT(ID) FROM {databaseSchema}.{objectQualifier}Buddy WHERE ToUserID = I_UserID AND Approved = 0) ELSE 0 END AS PendingBuddies,
		CASE WHEN i_ShowPendingBuddies > 0 THEN (SELECT Requested FROM {databaseSchema}.{objectQualifier}Buddy WHERE ToUserID=a.UserID and Approved = 0 ORDER BY Requested DESC  LIMIT 1) ELSE NULL END AS LastPendingBuddies, 			
		CASE WHEN i_ShowUserStyle > 0 THEN (SELECT UserStyle FROM {databaseSchema}.{objectQualifier}User where UserID = i_UserID LIMIT 1) ELSE NULL END AS  UserStyle,		
		(SELECT COUNT(ua.AlbumID) FROM {databaseSchema}.{objectQualifier}UserAlbum ua
		WHERE ua.UserID = I_UserID) AS NumAlbums,
		(CASE WHEN G_UsrAlbums > R_UsrAlbums THEN G_UsrAlbums ELSE R_UsrAlbums END) AS UsrAlbums,
		SIGN(IFNULL((SELECT 1 FROM {databaseSchema}.{objectQualifier}Buddy WHERE FromUserID = i_UserID OR ToUserID = I_UserID LIMIT 1),0)) AS UserHasBuddies,	    
		-- Guest can't vote in polls attached to boards, we need some temporary access check by a criteria 
		(CASE WHEN a.Flags & 4 > 0 THEN 0 ELSE 1 END) AS BoardVoteAccess,
		a.Points as Reputation,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Forum WHERE CreatedByUserID = i_UserID AND IsUserForum = 1) as PersonalForumsNumber,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}AccessMask WHERE CreatedByUserID = i_UserID AND IsUserMask = 1) as PersonalAccessMasksNumber,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Group WHERE CreatedByUserID = i_UserID AND IsUserGroup = 1) as PersonalGroupsNumber,
		ici_UsrPersonalGroups AS UsrPersonalGroups,
		ici_UsrPersonalMasks AS UsrPersonalMasks,
		ici_UsrPersonalForums AS UsrPersonalForums,
		a.CommonViewType,
		a.TopicsPerPage,
		a.PostsPerPage
		from
		   {databaseSchema}.{objectQualifier}User a		
		where
		a.UserID = I_UserID AND a.BoardID = I_BoardID LIMIT 1;
	 end;
--GO


CREATE PROCEDURE {databaseSchema}.{objectQualifier}message_gettextbyids 
	(i_MessageIDs longtext)
BEGIN
	DECLARE ici_MessageID VARCHAR(11);
	DECLARE ici_MessageIDsChunk VARCHAR(4000);
	DECLARE ici_Pos int;
	DECLARE ici_Itr int;
	DECLARE ici_trimindex int;
	
   -- DROP TEMPORARY TABLE  IF EXISTS {objectQualifier}tmp_ParsedMessageIDs;
	CREATE TEMPORARY TABLE IF NOT EXISTS {objectQualifier}tmp_ParsedMessageIDs 
	(
		MessageID INT NOT NULL
	);

		
	SET i_MessageIDs = (CONCAT(TRIM(i_MessageIDs), ','));
	SET ici_Pos = (LOCATE(',', i_MessageIDs, 1));
	IF REPLACE(i_MessageIDs, ',', '') <> '' THEN	
		WHILE ici_Pos > 0 DO		
			SET ici_MessageID = LTRIM(RTRIM(LEFT(i_MessageIDs, ici_Pos - 1)));
			IF ici_MessageID <> '' THEN			
				INSERT INTO {objectQualifier}tmp_ParsedMessageIDs(MessageID) VALUES (CAST(ici_MessageID AS SIGNED)) ;
				-- Use Appropriate conversion
			END IF;
			SET i_MessageIDs = RIGHT(i_MessageIDs, CHAR_LENGTH(i_MessageIDs) - ici_Pos);
			SET ici_Pos = LOCATE(',', i_MessageIDs, 1);
		END WHILE;
	END	IF;

	SELECT a.MessageID, d.Message
			FROM {objectQualifier}tmp_ParsedMessageIDs a
			INNER JOIN {databaseSchema}.{objectQualifier}Message d ON d.MessageID = a.MessageID;
	
	DROP TEMPORARY TABLE IF EXISTS {objectQualifier}tmp_ParsedMessageIDs;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_thankedmessage
(i_MessageID int, i_UserID int) 
begin
		SELECT COUNT(TH.ThanksID)
		FROM {databaseSchema}.{objectQualifier}Thanks AS TH WHERE (TH.MessageID=i_MessageID) AND (TH.ThanksFromUserID = i_UserID);
end;
--GO
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_thankfromcount
(i_UserID int) 
begin
		SELECT COUNT(TH.ThanksID) 
		FROM {databaseSchema}.{objectQualifier}Thanks AS TH WHERE (TH.ThanksToUserID=i_UserID);
end;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_save_prntchck
(
	i_ForumID INT,
	i_ParentID INT
 )
 begin
		SELECT {databaseSchema}.{objectQualifier}forum_save_parentschecker(i_ForumID,	i_ParentID);
end;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}user_repliedtopic
(i_MessageID int, i_UserID int) 
begin
		DECLARE ici_TopicID int;
		SET ici_TopicID = (SELECT TopicID FROM {databaseSchema}.{objectQualifier}Message WHERE (MessageID = i_MessageID));

		SELECT COUNT(1)
		FROM {databaseSchema}.{objectQualifier}Message AS t WHERE (t.TopicID=ici_TopicID) AND (t.UserID = i_UserID);		
end;
--GO


CREATE PROCEDURE {databaseSchema}.{objectQualifier}recent_users(i_BoardID int,i_TimeSinceLastLogin int,i_StyledNicks TINYINT(1), i_UTCTIMESTAMP DATETIME) 
begin  
	SELECT 
	usr.UserId,
	usr.Name AS UserName,
	usr.DisplayName AS UserDisplayName,
	usr.LastVisit,
	0 as IsCrawler,
	1 as UserCount,
	-- IsActiveExcluded
	((usr.Flags & 16) = 16) AS IsHidden,
	(CASE(i_StyledNicks)
				WHEN 1 THEN
						usr.UserStyle
				ELSE ''
			END) AS Style
	FROM {databaseSchema}.{objectQualifier}User AS usr
				JOIN {databaseSchema}.{objectQualifier}Rank R on R.RankID=usr.RankID
				-- APPROVED
	WHERE ((usr.Flags & 2) = 2) AND
	 usr.BoardID = i_BoardID AND usr.LastVisit > ADDDATE(UTC_DATE(), INTERVAL -i_TimeSinceLastLogin MINUTE)
	 -- Excluding guests
		   AND  NOT EXISTS(             
					SELECT 1 
						FROM {databaseSchema}.{objectQualifier}UserGroup x
							inner join {databaseSchema}.{objectQualifier}Group y ON y.GroupID=x.GroupID 
						WHERE x.UserID=usr.UserID and (y.Flags & 2)<>0 LIMIT 1
					)
	ORDER BY usr.LastVisit;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}readtopic_addorupdate(i_UserID int,i_TopicID int,
 i_UTCTIMESTAMP DATETIME) 
begin

	declare	ici_LastAccessDate	DATETIME;
	select LastAccessDate INTO ici_LastAccessDate from {databaseSchema}.{objectQualifier}TopicReadTracking where UserID=i_UserID AND TopicID=i_TopicID LIMIT 1;
	IF ici_LastAccessDate IS NOT NULL then	     
		  update {databaseSchema}.{objectQualifier}TopicReadTracking set LastAccessDate=i_UTCTIMESTAMP where LastAccessDate = ici_LastAccessDate;
	ELSE	  
		  insert into {databaseSchema}.{objectQualifier}TopicReadTracking(UserID,TopicID,LastAccessDate)
		  values (i_UserID, i_TopicID, i_UTCTIMESTAMP);
	end if;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}readtopic_delete(i_userid int) 
begin
		delete from {databaseSchema}.{objectQualifier}TopicReadTracking where UseridID = i_UserID;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}readtopic_lastread(i_UserID int, i_TopicID int) 
begin
		SELECT LastAccessDate FROM  {databaseSchema}.{objectQualifier}TopicReadTracking WHERE UserID = i_UserID AND TopicID = i_TopicID;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}readforum_addorupdate(i_UserID int,i_ForumID int,
 i_UTCTIMESTAMP DATETIME) 
begin
	 declare ici_LastAccessDate	DATETIME;
	 SELECT LastAccessDate INTO ici_LastAccessDate from {databaseSchema}.{objectQualifier}ForumReadTracking where UserID=i_UserID AND ForumID=i_ForumID limit 1;
	IF ici_LastAccessDate IS NULL then
		  SELECT LastAccessDate INTO ici_LastAccessDate FROM {databaseSchema}.{objectQualifier}ForumReadTracking WHERE (UserID=i_UserID AND ForumID=i_ForumID);
		  update {databaseSchema}.{objectQualifier}ForumReadTracking set LastAccessDate=i_UTCTIMESTAMP where LastAccessDate = ici_LastAccessDate;
	ELSE	  
		  insert into {databaseSchema}.{objectQualifier}ForumReadTracking(UserID,ForumID,LastAccessDate)
		  values (i_UserID, i_ForumID, i_UTCTIMESTAMP);
	end if;

	-- Delete TopicReadTracking for forum...
	DELETE
	FROM {databaseSchema}.{objectQualifier}TopicReadTracking
	WHERE UserID = i_UserID
		AND TopicID IN (
			SELECT TopicID
			FROM {databaseSchema}.{objectQualifier}Topic
			WHERE ForumID = i_ForumID
			);
end;
--GO


create procedure {databaseSchema}.{objectQualifier}readforum_delete(i_userid int) 
begin
		delete from {databaseSchema}.{objectQualifier}ForumReadTracking where UserID = i_userid;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}readforum_lastread(i_UserID int,i_ForumID int) 
begin
		SELECT LastAccessDate FROM  {databaseSchema}.{objectQualifier}ForumReadTracking WHERE UserID = i_UserID AND ForumID = i_ForumID;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}user_lastread(i_UserID int) 
begin
		DECLARE i_LastForumRead DATETIME;
		DECLARE i_LastTopicRead DATETIME;
		
		SET i_LastForumRead = (SELECT MAX(LastAccessDate) FROM  {databaseSchema}.{objectQualifier}ForumReadTracking WHERE UserID = i_UserID);
		SET i_LastTopicRead = (SELECT MAX(LastAccessDate) FROM  {databaseSchema}.{objectQualifier}TopicReadTracking WHERE UserID = i_UserID);

		IF i_LastForumRead is not null AND i_LastTopicRead is not null then
		
		IF i_LastForumRead > i_LastTopicRead then
		   SELECT LastAccessDate = i_LastForumRead;
		ELSE
		   SELECT LastAccessDate = i_LastTopicRead;
		   end if;
		   
		ELSEIF i_LastForumRead is not null then
		   SELECT i_LastForumRead as LastAccessDate;
			
		ELSEIF i_LastTopicRead is not null then
			SELECT i_LastTopicRead as LastAccessDate;
			end if;
end;
--GO
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topics_byuser
(
	i_BoardID INT,
	i_CategoryID INT,
	i_PageUserID INT,
	i_SinceDate DATETIME,
	i_ToDate DATETIME, 
	i_PageIndex INT, 	
	i_PageSize INT,
	i_StyledNicks TINYINT(1),
	i_FindLastUnread TINYINT(1),
	i_UTCTIMESTAMP DATETIME 	
	)
BEGIN
 DECLARE  ici_post_totalrowsnumber INT; 
 DECLARE  ici_firstselectrownum INT;   
 DECLARE  ici_firstselectposted DATETIME; 

 SET i_PageIndex = i_PageIndex + 1;
	-- find total returned count
			select
		COUNT(c.TopicID)
		INTO ici_post_totalrowsnumber
		from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category cat on cat.CategoryID=d.CategoryID
	where
		(c.LastPosted BETWEEN i_SinceDate AND i_ToDate)  and
		x.UserID = i_PageUserID and
		CAST(x.ReadAccess AS SIGNED) <> 0 and
		cat.BoardID = i_BoardID and
		(i_CategoryID is null or cat.CategoryID=i_CategoryID) and
		IFNULL((c.Flags & 8)<>8,false)
		and	c.TopicMovedID is null
		and c.TopicID = (SELECT mess.TopicID FROM {databaseSchema}.{objectQualifier}Message mess WHERE mess.UserID=i_PageUserID AND mess.TopicID=c.TopicID limit 1);

	
	  select (i_PageIndex-1) * i_PageSize + 1 INTO ici_firstselectrownum;

	 

	 if (ici_firstselectrownum > 0)
	 then
	 select ici_firstselectrownum 
	 into ici_firstselectrownum;
	 end  if;
	
set @i_FirstSelectPosted = null;
set @i_FirstSelectLastPosted = null;

 set @tlist2_str = CONCAT('select
c.LastPosted,
c.Posted
INTO @i_FirstSelectLastPosted,@i_FirstSelectPosted
		from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category cat on cat.CategoryID=d.CategoryID
	where
		(c.LastPosted BETWEEN ''',i_SinceDate,''' AND ''',i_ToDate,''')  and
		x.UserID = ',i_PageUserID,' and
		CAST(x.ReadAccess AS SIGNED) <> 0 and
		cat.BoardID = ',i_BoardID,' and
		(',COALESCE(i_CategoryID,0),' = 0 or cat.CategoryID=',COALESCE(i_CategoryID,0),') and
		IFNULL((c.Flags & 8)<>8,false)
		and	c.TopicMovedID is null
		and c.TopicID = (SELECT mess.TopicID FROM {databaseSchema}.{objectQualifier}Message mess WHERE mess.UserID=',i_PageUserID,' AND mess.TopicID=c.TopicID limit 1)
	order by
		c.LastPosted desc,
		cat.SortOrder asc,
		d.SortOrder asc,
		d.Name asc,
		c.Priority desc		
		LIMIT 1 OFFSET ',ici_firstselectrownum-1,';');  
		PREPARE tlist2 FROM  @tlist2_str;

		EXECUTE tlist2;		 
		DEALLOCATE PREPARE tlist2;	
		
			
	 set @tlist1_str = CONCAT('select
		c.ForumID,
		c.TopicID,
		c.TopicMovedID,
		c.Posted,
		c.`Status`,
		c.Styles,
		IFNULL(c.TopicMovedID,c.TopicID) as LinkTopicID,
		c.Topic as Subject,
		c.Description,
		c.UserID,
		IFNULL(c.UserName,b.Name) as Starter,
		IFNULL(c.UserDisplayName,b.DisplayName) as StarterDisplay,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message mes WHERE mes.TopicID = c.TopicID AND (mes.Flags & 8) = 8 AND (mes.Flags & 16) = 16 AND ((',i_PageUserID,' IS NOT NULL AND mes.UserID = ',i_PageUserID,') OR (',i_PageUserID,' IS NULL)) ) as NumPostsDeleted,
		(select count(1) from {databaseSchema}.{objectQualifier}Message x where x.TopicID=c.TopicID and (x.Flags & 8)<> 8) - 1 as Replies,
		c.Views,
		c.LastPosted,
		c.LastUserID,
		IFNULL(c.LastUserName,(select x.Name from {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) as LastUserName,
		IFNULL(c.LastUserDisplayName,(select x.DisplayName from {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID)) as LastUserDisplayName,
		c.LastMessageID as LastMessageID,
		c.LastMessageFlags as LastMessageFlags,
		c.TopicID as LastTopicID,
		c.Flags as TopicFlags,
		(SELECT COUNT(ID) as FavoriteCount FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE TopicId = IFNULL(c.TopicMovedID,c.TopicID)) as FavoriteCount,
		c.Priority,
		c.PollID,
		d.Name as ForumName,		
		d.Flags as ForumFlags,
		(SELECT CAST(Message as char(1000)) FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(c.TopicMovedID,c.TopicID) AND mes2.Position = 0 limit 1) as FirstMessage,
		(case(',i_StyledNicks,')
			when 1 then  b.UserStyle
			else ''''	 end) as  StarterStyle,
		(case(',i_StyledNicks,')
			when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=c.LastUserID)   
			else ''''	 end) as LastUserStyle,
		(case(',i_FindLastUnread,')
			 when 1 then
			   (SELECT  CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=d.ForumID AND x.UserID = ',i_PageUserID,' limit 1)
			 else CAST(NULL AS DATETIME)	 end) as LastForumAccess,
		(case(',i_FindLastUnread,')
			 when 1 then
			   (SELECT  CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = ',i_PageUserID,' limit 1)
			 else CAST(NULL AS DATETIME)	 end) as LastTopicAccess,	
			  (SELECT GROUP_CONCAT(tg.tag separator '','') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = c.TopicID) AS TopicTags			 , 
			 c.TopicImage,
			 c.TopicImageType,
			 c.TopicImageBin,
			 0 as HasAttachments,
			 {databaseSchema}.{objectQualifier}biginttoint(',ici_post_totalrowsnumber,') AS TotalRows,
			 {databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex 
	from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category cat on cat.CategoryID=d.CategoryID
	where
		c.LastPosted <= ''',COALESCE(@i_FirstSelectPosted,UTC_TIMESTAMP()),''' and
		x.UserID = ',i_PageUserID,' and
		CAST(x.ReadAccess AS SIGNED) <> 0 and
		cat.BoardID = ',i_BoardID,' and
		(',COALESCE(i_CategoryID,0),' = 0 or cat.CategoryID =',COALESCE(i_CategoryID,0),') and
		IFNULL((c.Flags & 8)<>8,false)
		and	c.TopicMovedID is null
		and c.TopicID = (SELECT mess.TopicID FROM {databaseSchema}.{objectQualifier}Message mess WHERE mess.UserID=',i_PageUserID,' AND mess.TopicID=c.TopicID limit 1)

	order by
		c.LastPosted desc,
		cat.SortOrder asc,
		d.SortOrder asc,
		d.Name asc,
		c.Priority desc
		  LIMIT ',i_PageSize,'  ;'); 
			PREPARE tlist1 FROM @tlist1_str;	
		EXECUTE tlist1;		 
		DEALLOCATE PREPARE tlist1;
END;
--GO

create procedure {databaseSchema}.{objectQualifier}user_update_ssn_status(
				 i_UserID int,
				 i_IsFacebookUser TINYINT(1),
				 i_IsTwitterUser  TINYINT(1)) 
begin	
	update {databaseSchema}.{objectQualifier}User 
	set IsFacebookUser = i_IsFacebookUser, 
	IsTwitterUser = i_IsTwitterUser 
	where UserID = i_UserID;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}TopicStatus_Delete(i_TopicStatusID int) 
begin
   delete from {databaseSchema}.{objectQualifier}TopicStatus
	where TopicStatusID = i_TopicStatusID;
end;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}TopicStatus_Edit(i_TopicStatusID int) 
BEGIN
	SELECT * 
	FROM {databaseSchema}.{objectQualifier}TopicStatus 
	WHERE 
		TopicStatusID = i_TopicStatusID;
END;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}TopicStatus_List(i_BoardID int) 
	BEGIN
			SELECT
				*
			FROM
				{databaseSchema}.{objectQualifier}TopicStatus
			WHERE
				BoardID = i_BoardID	
			ORDER BY
				TopicStatusID;
		END;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}TopicStatus_Save(i_TopicStatusID int, i_BoardID int, i_TopicStatusName VARCHAR(100),i_DefaultDescription VARCHAR(100)) 
begin
	if i_TopicStatusID is null or i_TopicStatusID = 0 then
		insert into {databaseSchema}.{objectQualifier}TopicStatus(BoardID,TopicStatusName,DefaultDescription) 
		values(i_BoardID,i_TopicStatusName,i_DefaultDescription);
	
	else 
		update {databaseSchema}.{objectQualifier}TopicStatus 
		set TopicStatusName = i_TopicStatusName, 
			DefaultDescription = i_DefaultDescription
		where TopicStatusID = i_TopicStatusID;
	end if;
end;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_unanswered
(
	i_BoardID INT,
	i_CategoryID INT,
	i_PageUserID INT,
	i_SinceDate DATETIME,
	i_ToDate DATETIME, 	
	i_PageIndex INT, 	
	i_PageSize INT,
	i_StyledNicks TINYINT(1),
	i_FindLastRead TINYINT(1),	
	i_UTCTIMESTAMP DATETIME 	
	)
BEGIN

 DECLARE  ici_post_totalrowsnumber INT; 
 DECLARE  ici_firstselectrownum INT;   
 DECLARE  ici_firstselectposted DATETIME; 

 SET i_PageIndex = i_PageIndex + 1; 
		
	-- find total returned count
			select
		COUNT(c.TopicID)
		INTO ici_post_totalrowsnumber
		from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category cat on cat.CategoryID=d.CategoryID
	where
		(c.LastPosted BETWEEN i_SinceDate AND i_ToDate) and
		x.UserID = i_PageUserID and
		CAST(x.ReadAccess AS SIGNED) <> 0 and
		cat.BoardID = i_BoardID and
		(i_CategoryID is null or cat.CategoryID=i_CategoryID) and
		(c.Flags & 8) <> 8 and	
		c.TopicMovedID is null and
		c.NumPosts = 1;

	
	  select (i_PageIndex-1) * i_PageSize INTO ici_firstselectrownum;

	 

	 if (ici_firstselectrownum > 0)
	 then
	 select ici_firstselectrownum
	 into ici_firstselectrownum;
	 end  if;
	
	set @i_FirstSelectPosted = null;
	set @i_FirstSelectEdited = null;
SET @sutop = CONCAT('select
c.LastPosted,
c.Posted
INTO   
@i_FirstSelectPosted, 
@i_FirstSelectEdited
		from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category cat on cat.CategoryID=d.CategoryID
	where
		(c.LastPosted BETWEEN ''',i_SinceDate,''' AND ''',i_ToDate,''' ) and
		x.UserID = ',i_PageUserID,' and
		CAST(x.ReadAccess AS SIGNED) <> 0 and
		cat.BoardID = ',i_BoardID,' and
		(',COALESCE(i_CategoryID,0),' = 0 or cat.CategoryID = ',COALESCE(i_CategoryID,0),') and
		(c.Flags & 8) <> 8 and	
		c.TopicMovedID is null and
		c.NumPosts = 1
	order by
		c.LastPosted desc,
		cat.SortOrder asc,
		d.SortOrder asc,
		d.Name asc,
		c.Priority desc
		LIMIT 1 OFFSET ',ici_firstselectrownum,' ;'); 
		 
		PREPARE tlist2 FROM @sutop;
		EXECUTE tlist2;			 
		DEALLOCATE PREPARE tlist2;	

	 SET @sutop_res =
	CONCAT('select
		c.ForumID,
		c.TopicID,
		c.TopicMovedID,
		c.Posted,
		IFNULL(c.TopicMovedID,c.TopicID) as LinkTopicID,
		c.Topic as Subject,
		c.Description,
		c.Status,
		c.Styles,
		c.UserID,
		IFNULL(c.UserName,b.Name) AS Starter,
		IFNULL(c.UserDisplayName,b.DisplayName) AS StarterDisplay,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message mes WHERE mes.TopicID = c.TopicID AND (mes.Flags & 8) = 8  AND ((',i_PageUserID,' IS NOT NULL AND mes.UserID = ',i_PageUserID,') OR (',i_PageUserID,' IS NULL)) ) AS NumPostsDeleted,
		(select count(1) from {databaseSchema}.{objectQualifier}Message x where x.TopicID=c.TopicID and (x.Flags & 8)=0) - 1 AS Replies,
		c.Views,
		c.LastPosted,
		c.LastUserID,
		(IFNULL(c.LastUserName,(select x.Name from {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID))) AS LastUserName,
		(IFNULL(c.LastUserDisplayName,(select x.DisplayName from {databaseSchema}.{objectQualifier}User x where x.UserID=c.LastUserID))) AS LastUserDisplayName,
		c.LastMessageID AS LastMessageID,
		c.LastMessageFlags AS LastMessageFlags,
		c.TopicID AS LastTopicID,
		c.Flags AS TopicFlags,
		(SELECT COUNT(ID) FROM {databaseSchema}.{objectQualifier}FavoriteTopic WHERE TopicId = IFNULL(c.TopicMovedID,c.TopicID)) AS FavoriteCount,
		c.Priority,
		c.PollID,
		d.Name AS ForumName,
		c.TopicMovedID,
		d.Flags AS ForumFlags,
		IFNULL(SIGN(c.Flags & 8)>0,false) AS IsDeleted,
		(SELECT  CAST(Message as CHAR(1000)) FROM {databaseSchema}.{objectQualifier}Message mes2 where mes2.TopicID = IFNULL(c.TopicMovedID,c.TopicID) AND mes2.Position = 0 LIMIT 1) AS FirstMessage,
		(case(',i_StyledNicks,')
			when 1 then   b.UserStyle
			else '''' end) AS StarterStyle,
		(case(',i_StyledNicks,')
			when 1 then   (SELECT usr.UserStyle FROM  {databaseSchema}.{objectQualifier}User usr WHERE usr.UserID=c.LastUserID)  
			else '''' end) AS LastUserStyle,
		(case(',i_FindLastRead,')
			 when 1 then
			   (SELECT CAST(x.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=d.ForumID AND x.UserID = ',i_PageUserID,'  LIMIT 1)
			 else CAST(NULL AS DATETIME) end) AS LastForumAccess,
		(case(',i_FindLastRead,')
			 when 1 then
			   (SELECT CAST(y.LastAccessDate AS DATETIME) FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = ',i_PageUserID,' LIMIT 1)
			 else CAST(NULL AS DATETIME) end) AS  LastTopicAccess,
			 (SELECT GROUP_CONCAT(tg.tag separator '','') FROM {databaseSchema}.{objectQualifier}Tags tg JOIN {databaseSchema}.{objectQualifier}TopicTags tt on tt.tagID = tg.TagID where tt.TopicID = c.TopicID) AS TopicTags,  
			 c.TopicImage,
			 c.TopicImageType,
			 c.TopicImageBin,
			 0 as HasAttachments,	
			 {databaseSchema}.{objectQualifier}biginttoint(',ici_post_totalrowsnumber,') AS TotalRows,
			 {databaseSchema}.{objectQualifier}biginttoint(',i_PageIndex,') AS PageIndex 
	from
		{databaseSchema}.{objectQualifier}Topic c
		join {databaseSchema}.{objectQualifier}User b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}Forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}ActiveAccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}Category cat on cat.CategoryID=d.CategoryID
	where
		c.LastPosted <= ''',COALESCE(@i_FirstSelectPosted,UTC_TIMESTAMP()),''' and
		x.UserID = ',i_PageUserID,' and
		CAST(x.ReadAccess AS SIGNED) <> 0 and
		cat.BoardID = ',i_BoardID,' and
		(',COALESCE(i_CategoryID,0),' = 0 or  cat.CategoryID = ',COALESCE(i_CategoryID,0),') and
		(c.Flags & 8) <> 8 and	
		c.TopicMovedID is null and
		c.NumPosts = 1
	order by
		c.LastPosted desc,
		cat.SortOrder asc,
		d.SortOrder asc,
		d.Name asc,
		c.Priority desc
		  LIMIT ',i_PageSize,' ;'); 
	  PREPARE tlist1 FROM @sutop_res;
	  EXECUTE tlist1;
	  DEALLOCATE PREPARE tlist1; 
END;
--GO

create procedure {databaseSchema}.{objectQualifier}user_savestyle(i_GroupID int, i_RankID int) 
begin
declare _usridtmp int; 
	declare _styletmp VARCHAR(255);
	declare _rankidtmp int;    
	  
		declare c cursor for
		select UserID,UserStyle,RankID from {databaseSchema}.{objectQualifier}User;   
-- loop thru users to sync styles
   
	   
		open c;
		BEGIN	
		DECLARE EXIT HANDLER FOR NOT FOUND BEGIN END; 
		LOOP
			FETCH c into _usridtmp,_styletmp,_rankidtmp;
			  
		UPDATE {databaseSchema}.{objectQualifier}User  
		SET UserStyle = IFNULL((SELECT f.Style FROM {databaseSchema}.{objectQualifier}UserGroup e 
			join {databaseSchema}.{objectQualifier}Group f on f.GroupID=e.GroupID 
			WHERE e.UserID=_usridtmp AND f.Style IS NOT NULL ORDER BY f.SortOrder LIMIT 1), 
			(SELECT r.Style FROM {databaseSchema}.{objectQualifier}Rank r where RankID = _rankidtmp LIMIT 1)) 
		WHERE UserID = _usridtmp ;		  	 
		
		END LOOP;
		END;
		close c; 
		
		/* UPDATE {databaseSchema}.{objectQualifier}User usr, 
		(SELECT e.UserID,f.Style FROM {databaseSchema}.{objectQualifier}UserGroup e 
			join {databaseSchema}.{objectQualifier}Group f on f.GroupID=e.GroupID 
			ORDER BY f.SortOrder LIMIT 1) gt, 
		   (SELECT u.UserID, r.Style FROM {databaseSchema}.{objectQualifier}Rank r join {databaseSchema}.{objectQualifier}User u ON u.RankID = r.RankID) rt
SET usr.UserStyle = IFNULL(gt.Style, rt.STYLE)
WHERE usr.UserID = rt.UserID  and usr.UserID = gt.UserID;*/

end;
--GO
CALL {databaseSchema}.{objectQualifier}user_savestyle (null,null);
--GO
CALL {databaseSchema}.{objectQualifier}user_savestyle (null,null);
--GO

CREATE procedure {databaseSchema}.{objectQualifier}adminpageaccess_save (i_UserID int, i_PageName VARCHAR(128))
begin
	if not exists (select 1 from {databaseSchema}.{objectQualifier}AdminPageUserAccess where UserID = i_UserID and PageName = i_PageName limit 1) then 
		insert into {databaseSchema}.{objectQualifier}AdminPageUserAccess(UserID,PageName) 
		values(i_UserID,i_PageName);
	end if;
end;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}adminpageaccess_delete (i_UserID int, i_PageName VARCHAR(128)) 
begin
		delete from {databaseSchema}.{objectQualifier}AdminPageUserAccess  where UserID = i_UserID AND (i_PageName IS NULL OR PageName = i_PageName);
end;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}adminpageaccess_list (i_UserID int, i_PageName VARCHAR(128)) 
begin
		if (i_UserID > 0  and i_PageName IS NOT NULL) then
		select ap.*, 
		u.Name as UserName, 
		u.DisplayName as UserDisplayName, 
		b.Name as BoardName 
		from {databaseSchema}.{objectQualifier}AdminPageUserAccess ap 
		JOIN  {databaseSchema}.{objectQualifier}User u on u.UserID = ap.UserID 
		JOIN {databaseSchema}.{objectQualifier}Board b ON b.BoardID = u.BoardID 
		where u.UserID = i_UserID and PageName = i_PageName and (u.Flags & 1) <> 1 order by  b.BoardID,u.DisplayName,ap.PageName;
		elseif (i_UserID > 0 and i_PageName IS  NULL) then
		select ap.*, 
		u.Name as UserName, 
		u.DisplayName as UserDisplayName, 
		b.Name as BoardName 
		 from {databaseSchema}.{objectQualifier}AdminPageUserAccess ap 
		JOIN  {databaseSchema}.{objectQualifier}User u on u.UserID = ap.UserID 
		JOIN {databaseSchema}.{objectQualifier}Board b ON b.BoardID = u.BoardID 
		where u.UserID = i_UserID and (u.Flags & 1) <> 1 order by  b.BoardID,u.DisplayName,ap.PageName;
		else
		select ap.*, 
		u.Name as UserName, 
		u.DisplayName as UserDisplayName, 
		b.Name as BoardName 
		from {databaseSchema}.{objectQualifier}AdminPageUserAccess ap 
		JOIN  {databaseSchema}.{objectQualifier}User u on ap.UserID = u.UserID 
		JOIN {databaseSchema}.{objectQualifier}Board b ON b.BoardID = u.BoardID 
		where (u.Flags & 1) <> 1
		order by  b.BoardID,u.DisplayName,ap.PageName;
		end if;
end;
--GO

--	1048576 max packet text length

create procedure {databaseSchema}.{objectQualifier}forum_maxid(i_BoardID int) 
begin	
	select 
	a.ForumID 
	from {databaseSchema}.{objectQualifier}Forum a 
	join {databaseSchema}.{objectQualifier}Category b 
	on b.CategoryID=a.CategoryID 
	where b.BoardID=i_BoardID order by a.ForumID desc;	
end;
--GO


create procedure {databaseSchema}.{objectQualifier}eventlog_deletebyuser
(	
	i_BoardID int,
	i_PageUserID int 
) 
begin
CREATE TEMPORARY TABLE IF NOT EXISTS tmp_tableeldlbu (EventLogID int);

if (exists (select 1 from {databaseSchema}.{objectQualifier}User where ((Flags & 1) = 1 and UserID = i_PageUserID) limit 1)) then

delete from {databaseSchema}.{objectQualifier}EventLog where
			(UserID is null or
			UserID in (select UserID from {databaseSchema}.{objectQualifier}User where BoardID=i_BoardID));

else
insert into tmp_tableeldlbu(EventLogID)
 select a.EventLogID from {databaseSchema}.{objectQualifier}EventLog a
		left join {databaseSchema}.{objectQualifier}EventLogGroupAccess e on e.EventTypeID = a.`Type` 
		join {databaseSchema}.{objectQualifier}UserGroup ug on (ug.UserID =  i_PageUserID and ug.GroupID = e.GroupID)
		left join {databaseSchema}.{objectQualifier}User b on b.UserID=a.UserID
		where e.DeleteAccess = 1;
		-- either EventLogID or BoardID must be null, not both at the same time
		delete from {databaseSchema}.{objectQualifier}EventLog
		where EventLogID in (select * from tmp_tableeldlbu);
	DROP TEMPORARY TABLE IF EXISTS tmp_tableeldlbu;
	end	if;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}group_eventlogaccesslist(i_BoardID int) 
begin
		if i_BoardID is null then
		select g.*,b.Name as BoardName from {databaseSchema}.{objectQualifier}Group g
		join {databaseSchema}.{objectQualifier}Board b on b.BoardID = g.BoardID order by g.SortOrder; 
	else
		select g.*,b.Name as BoardName from {databaseSchema}.{objectQualifier}Group g
		join {databaseSchema}.{objectQualifier}Board b on b.BoardID = g.BoardID where g.BoardID=i_BoardID  order by g.SortOrder;
		end if;
end;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}eventloggroupaccess_save (i_GroupID int, i_EventTypeID int, i_EventTypeName VARCHAR(128), i_DeleteAccess tinyint(1)) 
begin
	if not exists (select  1 from {databaseSchema}.{objectQualifier}EventLogGroupAccess where GroupID = i_GroupID and EventTypeName = i_EventTypeName limit 1) then
	
		insert into {databaseSchema}.{objectQualifier}EventLogGroupAccess  (GroupID,EventTypeID,EventTypeName,DeleteAccess) 
		values(i_GroupID,i_EventTypeID,i_EventTypeName,i_DeleteAccess);	
	else
		update {databaseSchema}.{objectQualifier}EventLogGroupAccess  set DeleteAccess = i_DeleteAccess
		where GroupID = i_GroupID and EventTypeID = i_EventTypeID;
	end if;
end;
--GO


CREATE procedure {databaseSchema}.{objectQualifier}eventloggroupaccess_delete (i_GroupID int, i_EventTypeID int, i_EventTypeName VARCHAR(128)) 
begin
	if i_EventTypeName is not null  then
	
		delete from {databaseSchema}.{objectQualifier}EventLogGroupAccess  where GroupID = i_GroupID and EventTypeID = i_EventTypeID;
	
	else
	
	-- delete all access rights
		delete from {databaseSchema}.{objectQualifier}EventLogGroupAccess  where GroupID = i_GroupID;
	end if;
end;
--GO

CREATE procedure {databaseSchema}.{objectQualifier}eventloggroupaccess_list (i_GroupID int, i_EventTypeID int) 
begin 
-- TODO - exclude host admins from list   
if i_EventTypeID is null   then
		select e.*, g.Name as GroupName from {databaseSchema}.{objectQualifier}EventLogGroupAccess e 
		join {databaseSchema}.{objectQualifier}Group g on g.GroupID = e.GroupID where  e.GroupID = i_GroupID;
		else
		select e.*, g.Name as GroupName from {databaseSchema}.{objectQualifier}EventLogGroupAccess e 
		join {databaseSchema}.{objectQualifier}Group g on g.GroupID = e.GroupID where  e.GroupID = i_GroupID and e.EventTypeID = i_EventTypeID;
		end if;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}forum_cataccess_actuser(i_BoardID int, i_UserID int) 
begin	
	select 
	DISTINCT(a.CategoryID) as CategoryID,
	b.Name as CategoryName
	from {databaseSchema}.{objectQualifier}Forum a 
	join {databaseSchema}.{objectQualifier}Category b 
	on b.CategoryID=a.CategoryID
	JOIN {databaseSchema}.{objectQualifier}ActiveAccess access ON (a.ForumID = access.ForumID and access.UserID = i_UserID)  
	WHERE b.BoardID = i_BoardID and a.ParentID IS NULL and  (access.ReadAccess > 0 or (access.ReadAccess <= 0 and (a.Flags & 2) <> 2)) ORDER BY b.SortOrder, a.CategoryID, b.Name;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}topic_tags(i_BoardID int, i_PageUserID int, i_TopicID int) 
begin	
	declare ici_maxcount int;

	SELECT MAX(tg.TagCount) INTO ici_maxcount
	FROM {databaseSchema}.{objectQualifier}Tags tg 
	JOIN  {databaseSchema}.{objectQualifier}TopicTags tt ON tt.TagID = tg.TagID 
	JOIN  {databaseSchema}.{objectQualifier}Topic t ON t.TopicID = tt.TopicID
	JOIN  {databaseSchema}.{objectQualifier}ActiveAccess aa ON aa.ForumID = t.ForumID
	WHERE BoardID=i_BoardID and t.TopicID=i_TopicID AND aa.UserID = i_PageUserID;

	SELECT tg.TagID,tg.Tag,tg.TagCount,ici_maxcount AS MaxTagCount 
	FROM {databaseSchema}.{objectQualifier}Tags tg 
	JOIN  {databaseSchema}.{objectQualifier}TopicTags tt ON tt.TagID = tg.TagID 
	JOIN  {databaseSchema}.{objectQualifier}Topic t ON t.TopicID = tt.TopicID
	JOIN  {databaseSchema}.{objectQualifier}ActiveAccess aa ON aa.ForumID = t.ForumID
	WHERE BoardID=i_BoardID and t.TopicID=i_TopicID AND aa.UserID = i_PageUserID	
	ORDER BY Tag;
end;
--GO

create procedure {databaseSchema}.{objectQualifier}forum_tags(i_BoardID int, i_PageUserID int, i_ForumID int, i_PageIndex int, i_PageSize int, i_SearchText VARCHAR(255), i_BeginsWith tinyint(1)) 
begin	
	DECLARE ici_maxcount INT DEFAULT 0;   
	DECLARE ici_tags_totalrowsnumber INT DEFAULT 0;
	DECLARE ici_firstselectrownum INT DEFAULT 0;

	DECLARE PI_Literals0 VARCHAR(255);
	DECLARE PI_Literals1 VARCHAR(255);
	DECLARE PI_LiteralsALL VARCHAR(4);

	if (i_SearchText is null) then
	set i_SearchText = '';
	end if;
	 
	SET i_SearchText = LOWER(i_SearchText);
	
	if CHAR_LENGTH(i_SearchText) > 0 THEN 
	SET PI_Literals0 = CONCAT('%',i_SearchText,'%');
	ELSE
	SET PI_Literals0 = '';
	END IF;
	if CHAR_LENGTH(i_SearchText) > 0 THEN 
	 SET PI_Literals1 = CONCAT(i_SearchText,'%');
	ELSE
	SET PI_Literals1 = '';
	END IF;   

	SET PI_LiteralsALL ='%';

	if (i_ForumID is null) then
	set i_ForumID = 0;
	end if; 
	 
	SELECT IFNULL(MAX(tg.TagCount),0) INTO ici_maxcount
	FROM {databaseSchema}.{objectQualifier}Tags tg 
	JOIN  {databaseSchema}.{objectQualifier}TopicTags tt ON tt.TagID = tg.TagID 
	JOIN  {databaseSchema}.{objectQualifier}Topic t ON t.TopicID = tt.TopicID
	JOIN  {databaseSchema}.{objectQualifier}ActiveAccess aa ON (aa.ForumID = t.ForumID AND aa.UserID = i_PageUserID)
	WHERE BoardID=i_BoardID and (i_ForumID <= 0 OR t.ForumID=i_ForumID) AND (t.Flags & 8) <> 8 
	AND     
		 LOWER(tg.Tag) LIKE (CASE 
			WHEN (i_BeginsWith = 0 and i_SearchText != '') THEN PI_Literals0 
			WHEN (i_BeginsWith <> 0 and i_SearchText != '') THEN PI_Literals1
			ELSE '%' END);

	SET i_PageIndex = i_PageIndex + 1;
			
	-- find total returned count
	SELECT
		COUNT(DISTINCT(tt.TagID))
		INTO ici_tags_totalrowsnumber
	FROM {databaseSchema}.{objectQualifier}Tags tg 
	JOIN  {databaseSchema}.{objectQualifier}TopicTags tt ON tt.TagID = tg.TagID 
	JOIN  {databaseSchema}.{objectQualifier}Topic t ON t.TopicID = tt.TopicID
	JOIN  {databaseSchema}.{objectQualifier}ActiveAccess aa ON aa.ForumID = t.ForumID
	WHERE BoardID=i_BoardID and (i_ForumID IS NULL OR t.forumid=i_ForumID) AND aa.UserID = i_PageUserID 	
	AND (t.Flags & 8) <> 8 
	AND     
		 LOWER(tg.Tag) LIKE (CASE 
			WHEN (i_BeginsWith = 0 and i_SearchText != '') THEN PI_Literals0 
			WHEN (i_BeginsWith <> 0 and i_SearchText != '') THEN PI_Literals1               
			ELSE '%' END);

	select (i_PageIndex-1) * i_PageSize + 1  INTO ici_firstselectrownum; 
   SET ici_firstselectrownum = (I_PageIndex - 1) * I_PageSize + 1; 
   SET ici_firstselectrownum = ici_firstselectrownum -1 ;
   SET @ii_SearchText = i_SearchText;
	SET @tlist1_str = CONCAT( 
	'SELECT DISTINCT(tt.TagID),tg.Tag,tg.TagCount,',ici_maxcount,' AS MaxTagCount,', ici_tags_totalrowsnumber,' as TotalCount 
	FROM {databaseSchema}.{objectQualifier}Tags tg 
	JOIN {databaseSchema}.{objectQualifier}TopicTags tt ON tt.TagID = tg.TagID 
	JOIN {databaseSchema}.{objectQualifier}Topic t ON t.TopicID = tt.TopicID
	JOIN {databaseSchema}.{objectQualifier}ActiveAccess aa ON aa.ForumID = t.ForumID
		  WHERE BoardID=', i_BoardID,' and  (',IFNULL(i_ForumID,0),' = 0 OR t.ForumID = ',IFNULL(i_ForumID,0),') AND aa.UserID = ',i_PageUserID,'	
		AND (t.Flags & 8) <> 8 
		AND     
		 LOWER(tg.Tag) LIKE (CASE 
			WHEN (',COALESCE(i_BeginsWith,0),' = 0  AND ''',i_SearchText,''' != '''') THEN ''',PI_Literals0,'''    
			WHEN (',COALESCE(i_BeginsWith,0),' <> 0 AND ''',i_SearchText,''' != '''') THEN ''',PI_Literals1,'''               
			ELSE ''%'' END) 
	   ORDER BY tg.Tag LIMIT ',ici_firstselectrownum, ',' ,i_PageSize,' ;'); 
		 PREPARE tlist1 FROM @tlist1_str;
			  EXECUTE tlist1;	  	 
		DEALLOCATE PREPARE tlist1;
end;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_imagesave(
	i_TopicID 	        INT,
	i_ImageURL	        VARCHAR(255),
	i_Stream		    LONGBLOB,
	i_TopicImageType	VARCHAR(128)
 )  
 BEGIN
	  UPDATE {databaseSchema}.{objectQualifier}Topic 
	  SET    TopicImage = i_ImageURL,
			 TopicImageBin = i_Stream,
			 TopicImageType = i_TopicImageType
	  WHERE  TopicID = i_TopicID;       
END;
--GO


/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}userforum_access(
	i_SessionID 	 VARCHAR(24),
	i_BoardID	     INT,	
	i_UserKey	     VARCHAR(64),
	i_IP		     VARCHAR(37),
	i_Location	     VARCHAR(255),
	i_ForumPage	     VARCHAR(255),
	i_Browser	     VARCHAR(128),
	i_Platform	     VARCHAR(128),
	i_CategoryID	 INT,
	i_ForumID	     INT,
	i_TopicID	     INT,
	i_MessageID  	 INT,
	i_IsCrawler      TINYINT(1),
	i_IsMobileDevice TINYINT(1),
	i_DontTrack      TINYINT(1),
	i_UTCTIMESTAMP    DATETIME
)
BEGIN
	DECLARE ici_UserID		INT;
	DECLARE ici_UserBoardID	INT;
	DECLARE ici_IsGuest	TINYINT(1) DEFAULT 0;
	DECLARE ici_rowcount	INT;
	DECLARE ici_PreviousVisit	DATETIME;
	DECLARE ici_ForumID INT;
	DECLARE ici_ActiveUpdate TINYINT(1) DEFAULT 0;
	declare ici_ActiveFlags	INT DEFAULT 1;
	declare ici_GuestID     INT;  
	

	/* set implicit_transactions off */
 
 START TRANSACTION WITH CONSISTENT SNAPSHOT;
	-- find a guest id should do it every time to be sure that guest access rights are in ActiveAccess table
		SELECT SQL_CALC_FOUND_ROWS UserID INTO ici_GuestID from {databaseSchema}.{objectQualifier}User where BoardID=i_BoardID and (Flags & 4)<>0 ORDER BY Joined DESC LIMIT 1;
		SET ici_rowcount=FOUND_ROWS();
		IF ici_rowcount>1 THEN
				/*raiserror('Found %d possible guest users. There should be one and only one user marked as guest.',16,1,ici_rowcount)*/		 
					SET ici_rowcount = ici_rowcount;			
		END IF;

-- verify that there's not the same session for other board and drop it if required.TestĚcode for portals with many boards  
 
delete from {databaseSchema}.{objectQualifier}Active where (SessionID = i_SessionID and BoardID != i_BoardID) ;
	IF i_UserKey IS NULL THEN
		SET ici_UserID = ici_GuestID;
		set ici_IsGuest = 1;
		-- set IsGuest ActiveFlag  1 | 2
		set ici_ActiveFlags = 3;
		set ici_UserBoardID = i_BoardID;
		-- crawlers are always guests 
		if	i_IsCrawler = 1 then			
			-- set IsCrawler ActiveFlag
			set ici_ActiveFlags =  ici_ActiveFlags | 8;
		end  if;		
	ELSE
	
		SELECT UserID, BoardID INTO ici_UserID,ici_UserBoardID  
		FROM {databaseSchema}.{objectQualifier}User
		where BoardID=i_BoardID AND ProviderUserKey=i_UserKey;
		
		SET ici_IsGuest = 0;
		-- make sure that registered users are not crawlers
		SET i_IsCrawler = 0;
		-- set IsRegistered ActiveFlag
		SET ici_ActiveFlags = ici_ActiveFlags | 4;

	END IF;
	-- START TRANSACTION WITH CONSISTENT SNAPSHOT;
	/* Check valid ForumID */
	IF i_ForumID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Forum WHERE ForumID=i_ForumID)
		  THEN 
		SET i_ForumID = NULL; 
			END IF;
	
	/* Check valid CategoryID*/
	IF i_CategoryID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Category WHERE CategoryID=i_CategoryID)              THEN 
		SET i_CategoryID = NULL;
		END IF;
	/*Check valid MessageID*/
	IF i_MessageID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Message WHERE MessageID=i_MessageID) 
		   THEN
		SET i_MessageID = NULL;
		END IF;
	/*Check valid TopicID*/
	IF i_TopicID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Topic WHERE TopicID=i_TopicID) 
		   THEN
		SET i_TopicID = NULL;
		END IF;	
	
	-- START TRANSACTION WITH CONSISTENT SNAPSHOT;
	/*update last visit*/
	UPDATE {databaseSchema}.{objectQualifier}User SET 
		LastVisit = i_UTCTIMESTAMP,
		IP = i_IP
	WHERE UserID = ici_UserID;
	-- COMMIT;
   -- START TRANSACTION WITH CONSISTENT SNAPSHOT;
	/*find missing ForumID/TopicID*/
	IF i_MessageID IS NOT NULL THEN
		SELECT
			c.CategoryID,
			b.ForumID,
			b.TopicID
				INTO i_CategoryID,i_ForumID,i_TopicID
		FROM
			{databaseSchema}.{objectQualifier}Message a
			INNER JOIN {databaseSchema}.{objectQualifier}Topic b ON b.TopicID = a.TopicID
			INNER JOIN {databaseSchema}.{objectQualifier}Forum c ON c.ForumID = b.ForumID
			INNER JOIN {databaseSchema}.{objectQualifier}Category d ON d.CategoryID = c.CategoryID
		WHERE
			a.MessageID = i_MessageID AND
			d.BoardID = i_BoardID;
	ELSEIF i_TopicID IS NOT NULL THEN
		SELECT 
			b.CategoryID,
			a.ForumID 
				INTO i_CategoryID,i_ForumID
		FROM 
			{databaseSchema}.{objectQualifier}Topic a
			inner join {databaseSchema}.{objectQualifier}Forum b on b.ForumID = a.ForumID
			inner join {databaseSchema}.{objectQualifier}Category c on c.CategoryID = b.CategoryID
		WHERE 
			a.TopicID = i_TopicID AND
			c.BoardID = i_BoardID;
	
	ELSEIF i_ForumID IS NOT NULL THEN
		SELECT
			 a.CategoryID
		INTO     i_CategoryID
					
		FROM	{databaseSchema}.{objectQualifier}Forum a
			inner join {databaseSchema}.{objectQualifier}Category b on b.CategoryID = a.CategoryID
		WHERE
			a.ForumID = i_ForumID and
			b.BoardID = i_BoardID;
	END IF;
	
	/*update active*/
	
	
	IF i_DontTrack != 1 AND ici_UserID IS NOT NULL AND ici_UserBoardID=i_BoardID THEN
		IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}Active 
		WHERE (SessionID=i_SessionID OR ( Browser = i_Browser AND (Flags & 8) = 8 )) 
		AND BoardID=i_BoardID) THEN
		if (char_length(i_location) > 0) then
		if i_IsCrawler <> 1 THEN
		 UPDATE {databaseSchema}.{objectQualifier}Active SET
				UserID = ici_UserID,
				IP = CAST(i_IP AS CHAR(39)),
				LastActive = i_UTCTIMESTAMP,
				Location = i_Location,
				ForumPage = i_ForumPage,
				ForumID = i_ForumID,
				TopicID = i_TopicID,
				Browser = i_Browser,
				Platform = i_Platform
			WHERE SessionID = i_SessionID and BoardID=i_BoardID;	
			ELSE
			 UPDATE {databaseSchema}.{objectQualifier}Active SET
				UserID = ici_UserID,
				IP = i_IP,
				LastActive = i_UTCTIMESTAMP,
				Location = i_Location,
				ForumPage = i_ForumPage,
				ForumID = i_ForumID,
				TopicID = i_TopicID,
				Browser = i_Browser,
				Platform = i_Platform
			WHERE Browser = i_Browser AND IP = i_IP and BoardID=i_BoardID;
			-- trace crawler: the cache is reset every time crawler moves to next page ? Disabled as cache reset will overload server 
			-- set @ActiveUpdate = 1		 ;	
			END IF;	
			else
			UPDATE {databaseSchema}.{objectQualifier}Active SET
			   LastActive = i_UTCTIMESTAMP
			WHERE SessionID = i_SessionID and BoardID=i_BoardID;	
			end if;
		ELSE
			-- we set ici_ActiveFlags ready flags 	
			INSERT INTO {databaseSchema}.{objectQualifier}Active(SessionID,BoardID,UserID,IP,Login,LastActive,Location,ForumID,TopicID,Browser,Platform,Flags)
			VALUES(i_SessionID,i_BoardID,ici_UserID,i_IP,i_UTCTIMESTAMP,i_UTCTIMESTAMP,i_Location,i_ForumID,i_TopicID,i_Browser,i_Platform,ici_ActiveFlags);
			/*update max user stats*/
			if ici_IsGuest = 0 then 
			SET ici_ActiveUpdate = 1; 
			end if;
			-- parameter to update active users cache if this is a new user
			 if ici_IsGuest=0 THEN
	
			 CALL {databaseSchema}.{objectQualifier}active_updatemaxstats (i_BoardID, i_UTCTIMESTAMP); 
			END IF; 
		END IF;
		/*remove duplicate users*/
		IF ici_IsGuest = 0 THEN
			DELETE FROM {databaseSchema}.{objectQualifier}Active WHERE UserID=ici_UserID AND BoardID=i_BoardID AND SessionID<>i_SessionID; END IF;
		END IF;

		-- ensure that access right are in place		
		if not exists (select 
			UserID	
			from {databaseSchema}.{objectQualifier}ActiveAccess  
			where UserID = ici_UserID LIMIT 1)
			then							
			insert into {databaseSchema}.{objectQualifier}ActiveAccess(
			UserID,
			BoardID,
			ForumID,
			IsAdmin, 
			IsForumModerator,
			IsModerator,
			IsGuestX,
			LastActive,
			ReadAccess,
			PostAccess,
			ReplyAccess,
			PriorityAccess,
			PollAccess,
			VoteAccess,	
			ModeratorAccess,
			EditAccess,
			DeleteAccess,
			UploadAccess,
			DownloadAccess,
			UserForumAccess)
			select 
			UserID, 
			i_BoardID, 
			ForumID, 
			SIGN(IsAdmin),
			SIGN(IsForumModerator),
			SIGN(IsModerator),
			ici_IsGuest,
			i_UTCTIMESTAMP,
			SIGN(ReadAccess),
			SIGN(PostAccess),
			SIGN(ReplyAccess),
			SIGN(PriorityAccess),
			SIGN(PollAccess),
			SIGN(VoteAccess),
			SIGN(ModeratorAccess),
			IFNULL(SIGN(EditAccess),0),
			SIGN(DeleteAccess),
			SIGN(UploadAccess),
			SIGN(DownloadAccess),
			SIGN(UserForumAccess)			
			from {databaseSchema}.{objectQualifier}vaccess 
			where UserID = ici_UserID;			
	end if;
			-- ensure that access right are in place		
		if not exists (select 
			UserID	
			from {databaseSchema}.{objectQualifier}ActiveAccess  
			where UserID = ici_GuestID LIMIT 1)
			then							
			insert into {databaseSchema}.{objectQualifier}ActiveAccess(
			UserID,
			BoardID,
			ForumID,
			IsAdmin, 
			IsForumModerator,
			IsModerator,
			IsGuestX,
			LastActive,
			ReadAccess,
			PostAccess,
			ReplyAccess,
			PriorityAccess,
			PollAccess,
			VoteAccess,	
			ModeratorAccess,
			EditAccess,
			DeleteAccess,
			UploadAccess,
			DownloadAccess,
			UserForumAccess)
			select 
			UserID, 
			i_BoardID, 
			ForumID, 
			SIGN(IsAdmin),
			SIGN(IsForumModerator),
			SIGN(IsModerator),
			ici_IsGuest,
			i_UTCTIMESTAMP,
			SIGN(ReadAccess),
			SIGN(PostAccess),
			SIGN(ReplyAccess),
			SIGN(PriorityAccess),
			SIGN(PollAccess),
			SIGN(VoteAccess),
			SIGN(ModeratorAccess),
			IFNULL(SIGN(EditAccess),0),
			SIGN(DeleteAccess),
			SIGN(UploadAccess),
			SIGN(DownloadAccess),
			SIGN(UserForumAccess)			
			from {databaseSchema}.{objectQualifier}vaccess 
			where UserID = ici_GuestID;			
	end if;

	/*return information*/
	  --  SELECT count(1) INTO ici_Incoming FROM {databaseSchema}.{objectQualifier}UserPMessageSelectView b  where b.UserID=ici_UserID and b.IsRead=0;
	 SELECT
		x.*,		
		ici_IsGuest AS IsGuest,
		i_IsCrawler AS IsCrawler,
		i_IsMobileDevice AS IsMobileDevice,
		ici_UserID AS UserID,		
		(SELECT  LastVisit  FROM {databaseSchema}.{objectQualifier}User WHERE UserID = ici_UserID) AS PreviousVisit,		      	
		CAST(i_CategoryID AS SIGNED) AS CategoryID,
		(SELECT `Name` FROM {databaseSchema}.{objectQualifier}Category WHERE CategoryID = i_CategoryID) AS CategoryName,
		CAST(i_ForumID AS SIGNED) AS ForumID,
		(select `Name` from {databaseSchema}.{objectQualifier}Forum where ForumID = i_ForumID) AS ForumName,
		CAST(i_TopicID AS SIGNED) AS TopicID,
		(select Topic from {databaseSchema}.{objectQualifier}Topic where TopicID = i_TopicID) AS TopicName,		
		(select ThemeURL from {databaseSchema}.{objectQualifier}Forum where ForumID = i_ForumID LIMIT 1) AS ForumTheme,
		ici_ActiveUpdate AS ActiveUpdate
		from
		{databaseSchema}.{objectQualifier}ActiveAccess x 
		where
		x.UserID = ici_UserID and x.ForumID=IFNULL(i_ForumID,0); 
COMMIT;
-- SET AUTOCOMMIT=1;

END;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}user_listtodaysbirthdays(
	i_BoardID 	        INT,
	i_StyledNicks       TINYINT(1),
	i_CurrentYear	    INT,
	i_CurrentUtc	    DATETIME,
	i_CurrentUtc1	    DATETIME,
	i_CurrentUtc2	    DATETIME
 )  
 BEGIN
		SELECT 
		up.Birthday, 
		up.UserID, 
		u.TimeZone, 
		u.Name as UserName,
		u.DisplayName as UserDisplayName, 
		(case(i_StyledNicks) when 1 then  u.UserStyle else '' end) AS Style  
		FROM {databaseSchema}.{objectQualifier}UserProfile up 
		JOIN {databaseSchema}.{objectQualifier}User u ON u.UserID = up.UserID 
		where u.BoardID = i_BoardID 
		AND (DATE_ADD(up.Birthday,INTERVAL (YEAR(i_CurrentUtc1) - YEAR(up.Birthday)) YEAR)  BETWEEN i_CurrentUtc1 and i_CurrentUtc2);             
END;
--GO



create procedure {databaseSchema}.{objectQualifier}pollgroup_save    
	(i_TopicID int,
	i_ForumID int,
	i_CategoryID int,	
	i_UserID int,	
	i_Flags int,	
	i_UTCTIMESTAMP datetime)
begin
declare i_PollGroupID int;
declare i_NewPollGroupID int;

 -- Check if the group already exists
  if (i_TopicId is not null) then 
				select PollID iNTO  i_PollGroupID from {databaseSchema}.{objectQualifier}Topic WHERE TopicID = i_TopicID;               
	 elseif (i_ForumID is not null) then               
				select PollGroupID  into  i_PollGroupID from {databaseSchema}.{objectQualifier}Forum WHERE ForumID = i_ForumID;                
		 elseif (i_CategoryID is not null) then
				select PollGroupID  into  i_PollGroupID from {databaseSchema}.{objectQualifier}Category
				WHERE CategoryID = i_CategoryID;
			end if;	
			if (i_PollGroupID is null) then	
				  INSERT INTO {databaseSchema}.{objectQualifier}PollGroupCluster(UserID,Flags ) VALUES(i_UserID, i_Flags);
				   
				  SET i_NewPollGroupID = LAST_INSERT_ID(); 
				end if;

	  SELECT i_PollGroupID as PollGroupID, i_NewPollGroupID as NewPollGroupID;				
end;
--GO

create procedure {databaseSchema}.{objectQualifier}poll_save    
	(i_Question varchar(255), 
	i_Closes datetime,
	i_UserID int,	
	i_PollGroupID int,
	i_ObjectPath varchar(255),  
	i_MimeType varchar(255), 
	i_Flags int,
	i_UTCTIMESTAMP datetime)
begin
declare i_PollID int;

 insert into {databaseSchema}.{objectQualifier}Poll(Question,Closes,PollGroupID,UserID,ObjectPath,MimeType,Flags)
 values(i_Question,i_Closes,i_PollGroupID,i_UserID,i_ObjectPath,i_MimeType,i_Flags); 
 set i_PollID = LAST_INSERT_ID();
 select i_PollID as PollID;				
end;
--GO

create procedure {databaseSchema}.{objectQualifier}choice_save    
	(i_PollID int, 
	i_Choice VARCHAR(255),
	i_Votes int,	
	i_ObjectPath varchar(255),  
	i_MimeType varchar(50), 	
	i_UTCTIMESTAMP datetime) 
begin
 insert into {databaseSchema}.{objectQualifier}Choice(PollID,Choice,Votes,ObjectPath,MimeType)
 values(i_PollID,i_Choice,i_Votes,i_ObjectPath,i_MimeType); 		
end;
--GO

create procedure {databaseSchema}.{objectQualifier}pollgroup_setlinks    
	(i_TopicID int,
	i_ForumID int,
	i_CategoryID int,	
	i_PollGroupID int,
	i_UTCTIMESTAMP datetime)
begin               
			   -- we don't update if no new group is being created 
			   IF  (i_PollGroupID IS NOT NULL) THEN 
				-- fill a pollgroup field - double work if a poll exists 
				if (i_TopicId is not null) THEN
					UPDATE {databaseSchema}.{objectQualifier}Topic SET PollID = i_PollGroupID WHERE TopicID = i_TopicID; 
				-- fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
				elseif (i_ForumID is not null) then                 
					UPDATE {databaseSchema}.{objectQualifier}Forum SET PollGroupID = i_PollGroupID WHERE ForumID= i_ForumID; 
				-- fill a pollgroup field in Category Table if the call comes from a category's topic list 
				elseif (i_CategoryID is not null) then             
					UPDATE {databaseSchema}.{objectQualifier}Category SET PollGroupID = i_PollGroupID WHERE CategoryID= i_CategoryID;                 
			 END IF;  
			  END IF;              
end;
--GO

/* STORED PROCEDURE CREATED BY VZ-TEAM */
CREATE PROCEDURE {databaseSchema}.{objectQualifier}topic_restore(i_TopicID INT, i_UserID INT)
BEGIN
DECLARE ici_ForumID INT;
	UPDATE {databaseSchema}.{objectQualifier}Topic SET Flags = Flags ^ 8 
	WHERE TopicID = i_TopicID and (Flags & 8) = 8;
	UPDATE {databaseSchema}.{objectQualifier}Topic SET Flags = Flags ^ 8 
	WHERE TopicMovedID = i_TopicID and (Flags & 8) = 8;
	UPDATE {databaseSchema}.{objectQualifier}Message SET Flags = Flags ^ 8 
	WHERE TopicID = i_TopicID and (Flags & 8) = 8;

	UPDATE  {databaseSchema}.{objectQualifier}Topic SET 
	LastMessageID = (SELECT m.MessageID from  {databaseSchema}.{objectQualifier}Message m
	where m.TopicID = i_TopicID and (m.Flags & 8) != 8 ORDER BY m.Posted desc LIMIT 1)
	where TopicID = i_TopicID;     

	SELECT ForumID INTO ici_ForumID FROM {databaseSchema}.{objectQualifier}Topic where TopicID = i_TopicID;
	CALL  {databaseSchema}.{objectQualifier}forum_updatelastpost (ici_ForumID);   	
	CALL  {databaseSchema}.{objectQualifier}forum_updatestats(ici_ForumID); 
	   
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}digest_topicnew(
				 i_boardid integer,
				 i_pageuserid integer,
				 i_sincedate datetime,
				 i_todate datetime,              
				 i_StyledNicks tinyint(1),               
				 i_utctimestamp datetime)
 BEGIN
 SELECT
		d.Name AS "ForumName",
		c.Topic AS "Subject",
		c.UserDisplayName AS "StartedUserName",		
		c.LastUserDisplayName as "LastUserName" ,
		c.LastMessageID as "LastMessageID",
		(SELECT x.Message FROM {databaseSchema}.{objectQualifier}Message x 
		  WHERE x.TopicID=c.TopicID and x.MessageID = c.LastMessageID) as "LastMessage",
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x 
		  WHERE x.TopicID=c.TopicID and (x.Flags & 8) = 0) as "Replies"    
	FROM
		{databaseSchema}.{objectQualifier}Topic c  
		JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ForumID=c.ForumID 
		JOIN {databaseSchema}.{objectQualifier}Category cat ON cat.CategoryID = d.CategoryID     
		join {databaseSchema}.{objectQualifier}vaccess x on (x.ForumID=d.ForumID AND x.UserID = i_pageuserid AND x.ReadAccess <> 0)      
	WHERE
	   cat.BoardID = i_boardid AND  
	   c.Posted > i_sincedate and        
	  (c.Flags & 8) = 0
	ORDER BY
	d.SortOrder,
	c.LastPosted desc;	
end;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}digest_topicactive(
				 i_boardid integer,
				 i_pageuserid integer,
				 i_sincedate datetime,
				 i_todate datetime,              
				 i_StyledNicks tinyint(1),               
				 i_utctimestamp datetime)
 BEGIN
 SELECT
		d.Name AS "ForumName",
		c.Topic AS "Subject",
		c.UserDisplayName AS "StartedUserName",		
		c.LastUserDisplayName as "LastUserName",
		c.LastMessageID as "LastMessageID",
		(SELECT x.Message FROM {databaseSchema}.{objectQualifier}Message x 
		  WHERE x.TopicID=c.TopicID and x.MessageID = c.LastMessageID) as "LastMessage",
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}Message x 
		  WHERE x.TopicID=c.TopicID and (x.Flags & 8) = 0) as "Replies"    
	FROM
		{databaseSchema}.{objectQualifier}Topic c  
		JOIN {databaseSchema}.{objectQualifier}Forum d ON d.ForumID=c.ForumID 
		JOIN {databaseSchema}.{objectQualifier}Category cat ON cat.CategoryID = d.CategoryID     
		join {databaseSchema}.{objectQualifier}vaccess x on (x.ForumID=d.ForumID AND x.UserID = i_pageuserid AND x.ReadAccess <> 0)      
	WHERE
	   cat.BoardID = i_boardid AND        
	   c.LastPosted > i_sincedate and
	   c.LastPosted < i_todate  and    
	  (c.Flags & 8) = 0
	ORDER BY
	d.SortOrder,
	c.LastPosted desc;	
end;
--GO