/*
  YAF SQL Stored Procedures File Created 03/01/06
    

  Remove Comments RegEx: \/\*(.*)\*\/
  Remove Extra Stuff: SET ANSI_NULLS ON\nGO\nSET QUOTED_IDENTIFIER ON\nGO\n\n\n 
*/
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}db_handle_computedcolumns]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}db_handle_computedcolumns]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}adminpageaccess_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}adminpageaccess_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}adminpageaccess_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}adminpageaccess_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}db_size]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}db_size]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}adminpageaccess_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}adminpageaccess_list]
GO
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}digest_topicnew]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}digest_topicnew]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}digest_topicactive]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}digest_topicactive]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}eventloggroupaccess_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}eventloggroupaccess_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}eventloggroupaccess_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}eventloggroupaccess_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}eventloggroupaccess_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}eventloggroupaccess_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_savestyle]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_savestyle]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_unanswered]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_unanswered]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_unread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_unread]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_update_ssn_status]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_update_ssn_status]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_update_single_sign_on_status]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_update_single_sign_on_status]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_updatefacebookstatus]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_updatefacebookstatus]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_move]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_move]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User_ListTodaysBirthdays]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}User_ListTodaysBirthdays]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}TopicStatus_Delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}TopicStatus_Delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}TopicStatus_Edit]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}TopicStatus_Edit]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}TopicStatus_List]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}TopicStatus_List]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}TopicStatus_Save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}TopicStatus_Save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topics_byuser]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topics_byuser]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}readtopic_addorupdate]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}readtopic_addorupdate]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}readtopic_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}readtopic_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}readtopic_lastread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}readtopic_lastread]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}readforum_addorupdate]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}readforum_addorupdate]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}readforum_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}readforum_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}readforum_lastread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}readforum_lastread]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_lastread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_lastread]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}recent_users]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}recent_users]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_thankfromcount]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_thankfromcount]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_repliedtopic]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_repliedtopic]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_thankedmessage]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_thankedmessage]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}accessmask_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}accessmask_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}accessmask_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}accessmask_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}accessmask_searchlist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}accessmask_searchlist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}accessmask_aforumlist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}accessmask_aforumlist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}accessmask_pforumlist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}accessmask_pforumlist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}accessmask_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}accessmask_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}active_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}active_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}active_listforum]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}active_listforum]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}active_listtopic]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}active_listtopic]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}active_stats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}active_stats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}activeaccess_reset]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}activeaccess_reset]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}active_updatemaxstats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}active_updatemaxstats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}attachment_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}attachment_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}attachment_download]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}attachment_download]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}attachment_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}attachment_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}attachment_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}attachment_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}bannedip_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}bannedip_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}bannedip_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}bannedip_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}bannedip_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}bannedip_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_create]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_create]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_poststats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_poststats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_userstats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_userstats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_resync]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_resync]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_setguid]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_setguid]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}board_stats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}board_stats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}category_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}category_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}category_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}category_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}category_getadjacentforum]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}category_getadjacentforum]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}category_listread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}category_listread]
GO
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}category_pfaccesslist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}category_pfaccesslist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}category_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}category_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}checkemail_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}checkemail_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}checkemail_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}checkemail_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}checkemail_update]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}checkemail_update]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}choice_add]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}choice_add]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}choice_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}choice_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}choice_update]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}choice_update]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}choice_vote]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}choice_vote]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}eventlog_create]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}eventlog_create]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}eventlog_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}eventlog_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}eventlog_deletebyuser]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}eventlog_deletebyuser]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}eventlog_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}eventlog_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}extension_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}extension_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}extension_edit]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}extension_edit]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}extension_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}extension_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_cataccess_actuser]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_cataccess_actuser]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}extension_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}extension_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_byuserlist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_byuserlist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_maxid]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_maxid]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_listall]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_listall]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_listall_fromcat]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_listall_fromcat]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_listallmymoderated]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_listallmymoderated]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_listpath]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_listpath]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_listread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_listread]
GO
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_listread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_listread]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_listSubForums]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_listSubForums]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_listtopics]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_listtopics]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_moderatelist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_moderatelist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_moderators]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_moderators]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_save_prntchck]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_save_prntchck]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}moderators_team_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}moderators_team_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_resync]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_resync]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_updatelastpost]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_updatelastpost]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_updatestats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_updatestats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forumaccess_group]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forumaccess_group]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forumaccess_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forumaccess_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forumaccess_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forumaccess_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_eventlogaccesslist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_eventlogaccesslist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_byuserlist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_byuserlist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_medal_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_medal_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_medal_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_medal_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_medal_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_medal_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_member]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_member]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}group_rank_style]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}group_rank_style]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}mail_create]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}mail_create]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}mail_createwatch]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}mail_createwatch]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}mail_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}mail_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}mail_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}mail_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}medal_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}medal_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}medal_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}medal_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}medal_listusers]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}medal_listusers]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}medal_resort]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}medal_resort]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}medal_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}medal_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_approve]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_approve]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_findunread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_findunread]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_getReplies]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_getReplies]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_listreported]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_listreported]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_listreporters]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_listreporters]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_report]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_report]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_reportcopyover]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_reportcopyover]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_reportresolve]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_reportresolve]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_secdata]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_secdata]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_unapproved]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_unapproved]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_update]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_update]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntpforum_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntpforum_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntpforum_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntpforum_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntpforum_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntpforum_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntpforum_update]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntpforum_update]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntpserver_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntpserver_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntpserver_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntpserver_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntpserver_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntpserver_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntptopic_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntptopic_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}nntptopic_savemessage]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}nntptopic_savemessage]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pageaccess]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pageaccess]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pageaccess_path]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pageaccess_path]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pageload]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pageload]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pmessage_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pmessage_info]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_info]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pmessage_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pmessage_markread]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_markread]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pmessage_prune]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_prune]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pmessage_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pmessage_archive]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_archive]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}poll_remove]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}poll_remove]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}poll_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}poll_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_attach]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_attach]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_remove]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_remove]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}poll_stats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}poll_stats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_stats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_stats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}poll_update]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}poll_update]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollvote_check]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollvote_check]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_votecheck]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_votecheck]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}choice_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}choice_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}pollgroup_setlinks]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_setlinks]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}post_last10user]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}post_last10user]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}post_alluser]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}post_alluser]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}post_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}post_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}post_list_reverse10]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}post_list_reverse10]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}rank_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}rank_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}rank_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}rank_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}rank_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}rank_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}registry_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}registry_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}registry_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}registry_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}replace_words_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}replace_words_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}replace_words_edit]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}replace_words_edit]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}replace_words_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}replace_words_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}replace_words_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}replace_words_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}smiley_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}smiley_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}smiley_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}smiley_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}smiley_listunique]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}smiley_listunique]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}smiley_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}smiley_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}smiley_resort]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}smiley_resort]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}system_initialize]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}system_initialize]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}system_updateversion]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}system_updateversion]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_active]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_active]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_tags]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_tags]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_tags]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_tags]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_bytags]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_bytags]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_findduplicate]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_findduplicate]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_findnext]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_findnext]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_findprev]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_findprev]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_info]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_info]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_announcements]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_announcements]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_latest]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_latest]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}rss_topic_latest]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}rss_topic_latest]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}rsstopic_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}rsstopic_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}announcements_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}announcements_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_listmessages]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_listmessages]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_deleteattachements]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_deleteattachements]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_lock]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_lock]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_move]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_move]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_poll_update]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_poll_update]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_prune]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_prune]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_restore]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_restore]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_updatelastpost]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_updatelastpost]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_updatetopic]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_updatetopic]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_accessmasks]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_accessmasks]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_accessmasksbyforum]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_accessmasksbyforum]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_activity_rank]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_activity_rank]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_addpoints]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_addpoints]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_adminsave]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_adminsave]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_approve]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_approve]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_approveall]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_approveall]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_aspnet]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_aspnet]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_migrate]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_migrate]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_avatarimage]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_avatarimage]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_changepassword]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_changepassword]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_pmcount]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_pmcount]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_deleteavatar]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_deleteavatar]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_deleteold]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_deleteold]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_emails]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_emails]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_find]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_find]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_getpoints]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_getpoints]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_getsignature]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_getsignature]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_guest]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_guest]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}admin_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}admin_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}admin_pageaccesslist]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}admin_pageaccesslist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}User_ListTodaysBirthdays]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}User_ListTodaysBirthdays]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_listmembers]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_listmembers]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_listmedals]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_listmedals]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_login]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_login]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_medal_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_medal_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_medal_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_medal_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_medal_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_medal_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_nntp]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_nntp]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_recoverpassword]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_recoverpassword]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_removepoints]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_removepoints]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_removepointsbytopicid]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_removepointsbytopicid]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_resetpoints]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_resetpoints]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_savenotification]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_savenotification]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_saveavatar]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_saveavatar]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_savepassword]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_savepassword]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_savesignature]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_savesignature]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_setnotdirty]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_setnotdirty]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_setpoints]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_setpoints]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_setrole]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_setrole]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_suspend]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_suspend]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_upgrade]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_upgrade]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}userforum_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}userforum_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}userforum_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}userforum_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}userforum_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}userforum_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}usergroup_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}usergroup_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}usergroup_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}usergroup_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}userpmessage_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}userpmessage_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}userpmessage_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}userpmessage_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}watchforum_add]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}watchforum_add]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}watchforum_check]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}watchforum_check]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}watchforum_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}watchforum_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}watchforum_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}watchforum_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}watchtopic_add]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}watchtopic_add]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}watchtopic_check]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}watchtopic_check]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}watchtopic_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}watchtopic_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}watchtopic_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}watchtopic_list]
GO

IF EXISTS (SELECT 1 FROM sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_reply_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_reply_list]
GO

IF EXISTS (SELECT 1 FROM sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_deleteundelete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_deleteundelete]
GO

IF EXISTS (SELECT 1 FROM sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_create_by_message]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_create_by_message]
GO

IF EXISTS (SELECT 1 FROM sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_move]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_move]
GO

IF exists (select top 1 1 from sys.objects
           WHERE  object_id = object_id(N'[{databaseSchema}].[{objectQualifier}category_simplelist]')
           and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}category_simplelist] 
GO

IF EXISTS (select top 1 1 from sys.objects
           WHERE  object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_simplelist]')
           and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_simplelist] 
GO

IF EXISTS (select top 1 1 from sys.objects
           WHERE  object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_simplelist]')
           and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_simplelist] 
GO

IF EXISTS (select top 1 1 from sys.objects
           WHERE  object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_simplelist]')
           and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_simplelist] 
GO

IF EXISTS (select top 1 1 from sys.objects
           WHERE  object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_simplelist]')
           and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_simplelist] 
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}bbcode_delete]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}bbcode_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}bbcode_list]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}bbcode_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}bbcode_save]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}bbcode_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_addignoreduser]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_addignoreduser]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_removeignoreduser]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_removeignoreduser]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_get]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_get]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_isuserignored]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_isuserignored]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_ignoredlist]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_ignoredlist]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}shoutbox_getmessages]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}shoutbox_getmessages]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}shoutbox_savemessage]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}shoutbox_savemessage]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}shoutbox_clearmessages]') and type in (N'P', N'PC'))
    DROP PROCEDURE [{databaseSchema}].[{objectQualifier}shoutbox_clearmessages]
GO

/* These stored procedures are for the Thanks Table. For safety, first check to see if they exist. If so, drop them. */
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_addthanks]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_addthanks]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_getthanks]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_getthanks]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_getallthanks]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_getallthanks]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_removethanks]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_removethanks]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_thanksnumber]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_thanksnumber]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_getthanks_from]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_getthanks_from]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_getthanks_to]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_getthanks_to]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}active_list_user]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}active_list_user]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_viewallthanks]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_viewallthanks]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_viewthanksfrom]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_viewthanksfrom]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_viewthanksto]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_viewthanksto]
GO
/* End of Thanks table stored procedures */
 
/* Buddy feature stored procedures */
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}buddy_addrequest]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_addrequest]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}buddy_approverequest]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_approverequest]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}buddy_denyrequest]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_denyrequest]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}buddy_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}buddy_remove]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_remove]
GO
/* End of Buddy feature stored procedures */

/****** Object:  StoredProcedure [{databaseSchema}].[{objectQualifier}topic_favorite_add]    Script Date: 12/08/2009 18:13:19 ******/
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_favorite_add]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_add]
GO

/****** Object:  StoredProcedure [{databaseSchema}].[{objectQualifier}topic_favorite_details]    Script Date: 12/08/2009 18:13:20 ******/
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_favorite_details]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_details]
GO

/****** Object:  StoredProcedure [{databaseSchema}].[{objectQualifier}topic_favorite_list]    Script Date: 12/08/2009 18:13:20 ******/
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_favorite_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_list]
GO

/****** Object:  StoredProcedure [{databaseSchema}].[{objectQualifier}topic_favorite_list]    Script Date: 12/08/2009 18:13:20 ******/
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_favorite_count]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_count]
GO

/****** Object:  StoredProcedure [{databaseSchema}].[{objectQualifier}topic_favorite_remove]    Script Date: 12/08/2009 18:13:20 ******/
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_favorite_remove]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_remove]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_save]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_gettitle]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_gettitle]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_getstats]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_getstats]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_image_save]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_image_save]
Go

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_image_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_image_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_images_by_user]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_images_by_user]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_image_delete]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_image_delete]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}album_image_download]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}album_image_download]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_getsignaturedata]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_getsignaturedata]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_getalbumsdata]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_getalbumsdata]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}messagehistory_list]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}messagehistory_list]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}user_lazydata]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}user_lazydata]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_gettextbyids]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}message_gettextbyids]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_imagesave]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_imagesave]
GO
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_tagsave]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}topic_tagsave]
GO
/*****************************************************************************************************************************/
/***** BEGIN CREATE PROCEDURES ******/
/*****************************************************************************************************************************/
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_tagsave] 
    @TopicID int, @Tags nvarchar(max)
AS
BEGIN

DECLARE @ThisTagID int 
DECLARE @Delimeter char(1)
DECLARE @Tag nvarchar(50)
DECLARE @StartPos int, @Length int
-- deleted tags table variable
DECLARE @DeletedTagIds table
 (
   TagID int
 )

DELETE FROM  [{databaseSchema}].[{objectQualifier}TopicTags] output deleted.TagID
INTO @DeletedTagIds(TagID)
where TopicID = @TopicID
-- decresae tag count if somethin was deleted
UPDATE [{databaseSchema}].[{objectQualifier}Tags] SET TagCount = TagCount-1 WHERE TagID IN (SELECT TagID FROM @DeletedTagIds)
SET @Delimeter = ','
WHILE LEN(@Tags) > 0
  BEGIN
    SET @StartPos = CHARINDEX(@Delimeter, @Tags)
    IF @StartPos < 0 SET @StartPos = 0
    SET @Length = LEN(@Tags) - @StartPos - 1
    IF @Length < 0 SET @Length = 0
    IF @StartPos > 0
      BEGIN
        SET @Tag = SUBSTRING(@Tags, 1, @StartPos - 1)
        SET @Tags = SUBSTRING(@Tags, @StartPos + 1, LEN(@Tags) - @StartPos)
      END
    ELSE
      BEGIN
        SET @Tag = @Tags
        SET @Tags = ''
      END
      if (not exists(SELECT 1 FROM  [{databaseSchema}].[{objectQualifier}Tags] where LOWER(Tag) = LOWER(@Tag)))
                              BEGIN
                              INSERT INTO [{databaseSchema}].[{objectQualifier}Tags]  (Tag) VALUES (@Tag) --Use Appropriate conversion
                              SELECT  @ThisTagID = SCOPE_IDENTITY() 
                              END
                              ELSE
                              BEGIN							  
                              SELECT  @ThisTagID = TagID FROM  [{databaseSchema}].[{objectQualifier}Tags] where LOWER(Tag) = LOWER(@Tag)
                              END
                              -- really the check doesn't work
                              if (not exists( SELECT 1 FROM  [{databaseSchema}].[{objectQualifier}TopicTags] where TopicID = @TopicID and TagID = @ThisTagID))
                              BEGIN							  
                              INSERT INTO [{databaseSchema}].[{objectQualifier}TopicTags]  (TagID,TopicID) VALUES (@ThisTagID,@TopicID) --Use Appropriate conversion
                              -- update tag count 
                              UPDATE [{databaseSchema}].[{objectQualifier}Tags] SET TagCount = TagCount+1 where TagID = @ThisTagID 
                              END
END
END
GO



/* Procedures for "Thanks" Mod */
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_addthanks] 
    @FromUserID int,
    @MessageID int,
    @UTCTIMESTAMP datetime,
    @UseDisplayName bit
AS
BEGIN
declare @ToUserID int;
select @ToUserID = (SELECT UserID FROM [{databaseSchema}].[{objectQualifier}Message] WHERE (MessageID = @MessageID));
IF not exists (SELECT top 1 ThanksID FROM [{databaseSchema}].[{objectQualifier}Thanks] WHERE (MessageID = @MessageID AND ThanksFromUserID=@FromUserID))
BEGIN
    INSERT INTO [{databaseSchema}].[{objectQualifier}Thanks] (ThanksFromUserID, ThanksToUserID, MessageID, ThanksDate) Values 
                                (@FromUserID, @ToUserID, @MessageID, @UTCTIMESTAMP )
    SELECT (CASE WHEN @UseDisplayName = 1 THEN DisplayName ELSE [Name] END) FROM [{databaseSchema}].[{objectQualifier}User] WHERE (UserID=@ToUserID)
END
ELSE
    SELECT ''
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_getthanks] 
    @MessageID int
AS
BEGIN
    SELECT a.ThanksFromUserID as UserID, a.ThanksDate, b.Name, b.DisplayName
    FROM [{databaseSchema}].[{objectQualifier}Thanks] a 
    Inner Join [{databaseSchema}].[{objectQualifier}User] b
    ON (a.ThanksFromUserID = b.UserID) WHERE (MessageID=@MessageID)
    ORDER BY a.ThanksDate DESC
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}db_size] 
    @DbScheme nvarchar(255), @DbName nvarchar(255)
AS
BEGIN
SELECT (SUM(cast(reserved_page_count as integer))*8192)/1024000 FROM sys.dm_db_partition_stats;
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_getallthanks] 
    @MessageIDs varchar(max)
AS
BEGIN
    SELECT CONVERT(int,a.item) as MessageID, b.ThanksFromUserID AS FromUserID, b.ThanksDate,
    (SELECT COUNT(ThanksID) FROM [{databaseSchema}].[{objectQualifier}Thanks] b WHERE b.ThanksFromUserID=d.UserID) AS ThanksFromUserNumber,
    (SELECT COUNT(ThanksID) FROM [{databaseSchema}].[{objectQualifier}Thanks] b WHERE b.ThanksToUserID=d.UserID) AS ThanksToUserNumber,
    (SELECT COUNT(DISTINCT(MessageID)) FROM [{databaseSchema}].[{objectQualifier}Thanks] b WHERE b.ThanksToUserID=d.userID) AS ThanksToUserPostsNumber
    FROM [{databaseSchema}].[{objectQualifier}table_intfromdelimitedstr](@MessageIDs,',') a
    INNER JOIN [{databaseSchema}].[{objectQualifier}Message] d ON (d.MessageID=a.item)
    LEFT JOIN [{databaseSchema}].[{objectQualifier}Thanks] b ON (b.MessageID = a.item)
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_removethanks] 
    @FromUserID int,
    @MessageID int,  
    @UseDisplayName bit
AS
BEGIN
    DELETE FROM [{databaseSchema}].[{objectQualifier}Thanks] WHERE (ThanksFromUserID=@FromUserID AND MessageID=@MessageID)
    DECLARE @ToUserID int
    SET @ToUserID = (SELECT UserID FROM [{databaseSchema}].[{objectQualifier}Message] WHERE (MessageID = @MessageID))
    SELECT (CASE WHEN @UseDisplayName = 1 THEN DisplayName ELSE [Name] END) FROM [{databaseSchema}].[{objectQualifier}User] 
	WHERE (UserID=@ToUserID)
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_thanksnumber] 
    @MessageID int
AS
BEGIN
RETURN (SELECT Count(1) from [{databaseSchema}].[{objectQualifier}Thanks] WHERE (MessageID=@MessageID))
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_getthanks_from] 
    @UserID int, @PageUserID  int
AS
BEGIN
SELECT Count(1) FROM [{databaseSchema}].[{objectQualifier}Thanks] 
WHERE ThanksFromUserID=@UserID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_getthanks_to] 
    @UserID			int,
    @PageUserID     int,
    @ThanksToNumber int output,
    @ThanksToPostsNumber int output
AS
BEGIN
SELECT @ThanksToNumber=(SELECT Count(1) FROM [{databaseSchema}].[{objectQualifier}Thanks] WHERE ThanksToUserID=@UserID)	
SELECT @ThanksToPostsNumber=(SELECT Count(DISTINCT MessageID) FROM [{databaseSchema}].[{objectQualifier}Thanks] WHERE ThanksToUserID=@UserID)	
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_viewthanksfrom] @UserID int, @PageUserID int,@PageIndex int=null, @PageSize int=null
AS 
begin
   declare @TotalRows int
   declare @FirstSelectRowNumber int
   declare @LastSelectRowNumber int
   
   -- find total returned count
   select @TotalRows = count(1)  from   [{databaseSchema}].[{objectQualifier}Thanks] t
                join [{databaseSchema}].[{objectQualifier}Message] c  on c.MessageID = t.MessageID		 
                join [{databaseSchema}].[{objectQualifier}Topic] a on a.TopicID = c.TopicID
                join [{databaseSchema}].[{objectQualifier}User] b on c.UserID = b.UserID
                join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID = a.ForumID
        where   x.ReadAccess > 0
                AND x.UserID = @PageUserID
				-- message is approved			
                AND c.IsApproved = 1
                AND a.TopicMovedID IS NULL
				-- topic is not deleted
                AND a.IsDeleted = 0
				-- message is not deleted
                AND c.IsDeleted = 0
             AND
           t.ThanksFromUserID = @UserID;      
    
    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
    
    with ThanksIds as
     (
     select ROW_NUMBER() over (order by c.Posted desc,c.Edited desc) as RowNum, t.ThanksID
     from [{databaseSchema}].[{objectQualifier}Thanks] t
                join [{databaseSchema}].[{objectQualifier}Message] c  on c.MessageID = t.MessageID		 
                join [{databaseSchema}].[{objectQualifier}Topic] a on a.TopicID = c.TopicID
                join [{databaseSchema}].[{objectQualifier}User] b on c.UserID = b.UserID
                join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID = a.ForumID
				where  x.ReadAccess > 0
                AND x.UserID = @PageUserID
				-- message is approved			
                AND c.IsApproved = 1
                AND a.TopicMovedID IS NULL
				-- topic is not deleted
                AND a.IsDeleted = 0
				-- message is not deleted
                AND c.IsDeleted = 0
             AND
           t.ThanksFromUserID = @UserID        
      )	
	  select
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
                c.[Message],
                c.Flags,
                TotalRows = @TotalRows
    from  ThanksIds ti
	join [{databaseSchema}].[{objectQualifier}Thanks] t on t.ThanksID = ti.ThanksID
	join [{databaseSchema}].[{objectQualifier}Message] c  on c.MessageID = t.MessageID		 
                join [{databaseSchema}].[{objectQualifier}Topic] a on a.TopicID = c.TopicID
                join [{databaseSchema}].[{objectQualifier}User] b on c.UserID = b.UserID             
            WHERE ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_viewthanksto] @UserID int, @PageUserID int,@PageIndex int=null, @PageSize int=null
AS 
begin
   declare @TotalRows int
   declare @FirstSelectRowNumber int
   declare @LastSelectRowNumber int
   
   -- find total returned count
   select @TotalRows = count(1)  from   [{databaseSchema}].[{objectQualifier}Thanks] t
                join [{databaseSchema}].[{objectQualifier}Message] c  on c.MessageID = t.MessageID		 
                join [{databaseSchema}].[{objectQualifier}Topic] a on a.TopicID = c.TopicID
                join [{databaseSchema}].[{objectQualifier}User] b on c.UserID = b.UserID
                join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID = a.ForumID
        where   x.ReadAccess > 0
                AND x.UserID = @PageUserID
				-- message is approved			
                AND c.IsApproved = 1
                AND a.TopicMovedID IS NULL
				-- topic is not deleted
                AND a.IsDeleted = 0
				-- message is not deleted
                AND c.IsDeleted = 0
             AND
           t.ThanksToUserID = @UserID;      
    
    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
    
    with ThanksIds as
     (
     select ROW_NUMBER() over (order by c.Posted desc,c.Edited desc) as RowNum, t.ThanksID
     from [{databaseSchema}].[{objectQualifier}Thanks] t
                join [{databaseSchema}].[{objectQualifier}Message] c  on c.MessageID = t.MessageID		 
                join [{databaseSchema}].[{objectQualifier}Topic] a on a.TopicID = c.TopicID
                join [{databaseSchema}].[{objectQualifier}User] b on c.UserID = b.UserID
                join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID = a.ForumID
				where  x.ReadAccess > 0
                AND x.UserID = @PageUserID
				-- message is approved			
                AND c.IsApproved = 1
                AND a.TopicMovedID IS NULL
				-- topic is not deleted
                AND a.IsDeleted = 0
				-- message is not deleted
                AND c.IsDeleted = 0
             AND
           t.ThanksToUserID = @UserID        
      )	
	  select
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
                c.[Message],
                c.Flags,
                TotalRows = @TotalRows
    from  ThanksIds ti
	join [{databaseSchema}].[{objectQualifier}Thanks] t on t.ThanksID = ti.ThanksID
	join [{databaseSchema}].[{objectQualifier}Message] c  on c.MessageID = t.MessageID		 
                join [{databaseSchema}].[{objectQualifier}Topic] a on a.TopicID = c.TopicID
                join [{databaseSchema}].[{objectQualifier}User] b on c.UserID = b.UserID             
            WHERE ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC
end
Go

/* End of procedures for "Thanks" Mod */

create procedure [{databaseSchema}].[{objectQualifier}accessmask_delete](@AccessMaskID int) as
begin
        declare @flag int
    
    set @flag=1
    if exists(select 1 from [{databaseSchema}].[{objectQualifier}ForumAccess] where AccessMaskID=@AccessMaskID) or exists(select 1 from [{databaseSchema}].[{objectQualifier}UserForum] where AccessMaskID=@AccessMaskID)
        set @flag=0
    else
        delete from [{databaseSchema}].[{objectQualifier}AccessMask] where AccessMaskID=@AccessMaskID
    
    select @flag
end
GO

create procedure [{databaseSchema}].[{objectQualifier}accessmask_list](@BoardID int,@AccessMaskID int=null,@ExcludeFlags int = 0, @PageUserID INT, @IsUserMask bit, @IsAdminMask  bit, @IsCommonMask bit, @PageIndex int, @PageSize int) as
begin
declare @TotalRows int;
declare @FirstSelectRowNumber int;
declare @LastSelectRowNumber int;
        if @AccessMaskID is null
		begin       set @PageIndex = @PageIndex + 1;
           set @FirstSelectRowNumber = 0;
           set @LastSelectRowNumber = 0;
           set @TotalRows = 0;
           
           select @TotalRows = count(1) from   
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
          where
            a.BoardID = @BoardID and
            (a.Flags & @ExcludeFlags) = 0 
			and (@IsCommonMask = 0 or (a.IsUserMask = 0 and a.IsAdminMask = 0))
			and (@IsUserMask = 0 or a.IsUserMask = 1)
            and (@IsAdminMask = 0 or a.IsAdminMask = 1)
            and (@PageUserID is null  or a.CreatedByUserID = @PageUserID);
           select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
           select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;

		    with MaskIds as
           (
             select ROW_NUMBER() over (order by SortOrder) as RowNum, AccessMaskID
            from 
            [{databaseSchema}].[{objectQualifier}AccessMask]   where
            BoardID = @BoardID and
            (Flags & @ExcludeFlags) = 0 
			and (@IsCommonMask = 0 or (IsUserMask = 0 and IsAdminMask = 0))
			and (@IsUserMask = 0 or IsUserMask = 1)
            and (@IsAdminMask = 0 or IsAdminMask = 1)
            and (@PageUserID is null  or CreatedByUserID = @PageUserID)
           )
           select
            a.*,
            @TotalRows as TotalRows
            from
            MaskIds c
            inner join  [{databaseSchema}].[{objectQualifier}AccessMask] a 	
            on c.AccessMaskID = a.AccessMaskID	
            where c.RowNum between (@FirstSelectRowNumber) and (@LastSelectRowNumber)
            order by c.RowNum asc;   
	end
    else
        select 
            a.* 
        from 
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
        where
            a.BoardID = @BoardID and
            a.AccessMaskID = @AccessMaskID
        order by 
            a.SortOrder;
end
GO

create procedure [{databaseSchema}].[{objectQualifier}accessmask_searchlist](@BoardID int,@AccessMaskID int=null,@ExcludeFlags int = 0, @PageUserID INT, @IsUserMask bit, @IsAdminMask  bit, @IsCommonMask bit, @PageIndex int, @PageSize int) as
begin
declare @TotalRows int;
declare @FirstSelectRowNumber int;
declare @LastSelectRowNumber int;
        if @AccessMaskID is null
		begin       set @PageIndex = @PageIndex + 1;
           set @FirstSelectRowNumber = 0;
           set @LastSelectRowNumber = 0;
           set @TotalRows = 0;
           
           select @TotalRows = count(1) from   
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
          where
            a.BoardID = @BoardID and
            (a.Flags & @ExcludeFlags) = 0 
			and (@IsCommonMask = 0 or (a.IsUserMask = 0 and a.IsAdminMask = 0))
			and (@IsUserMask = 0 or a.IsUserMask = 1)
            and (@IsAdminMask = 0 or IsAdminMask = 1)
            and (@PageUserID is null  or CreatedByUserID = @PageUserID);
           select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
           select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;

		    with MaskIds as
           (
             select ROW_NUMBER() over (order by SortOrder) as RowNum, AccessMaskID
            from 
            [{databaseSchema}].[{objectQualifier}AccessMask]   where
            BoardID = @BoardID and
            (Flags & @ExcludeFlags) = 0 
			and (@IsCommonMask = 0 or (IsUserMask = 0 and IsAdminMask = 0))
			and (@IsUserMask = 0 or IsUserMask = 1)
            and (@IsAdminMask = 0 or IsAdminMask = 1)
            and (@PageUserID is null  or CreatedByUserID = @PageUserID)
           )
           select
            a.*,
            @TotalRows as TotalRows
            from
            MaskIds c
            inner join  [{databaseSchema}].[{objectQualifier}AccessMask] a 	
            on c.AccessMaskID = a.AccessMaskID	
            where c.RowNum between (@FirstSelectRowNumber) and (@LastSelectRowNumber)
            order by c.RowNum asc;   
	end
    else
        select 
            a.* 
        from 
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
        where
            a.BoardID = @BoardID and
            a.AccessMaskID = @AccessMaskID
        order by 
            a.SortOrder;
end
GO

create procedure [{databaseSchema}].[{objectQualifier}accessmask_aforumlist](@BoardID int,@AccessMaskID int=null,@ExcludeFlags int = 0, @PageUserID INT, @IsAdminMask  bit, @IsCommonMask bit) as
begin
        if @AccessMaskID is null
        select 
            a.* 
        from 
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
        where
            a.BoardID = @BoardID and
            (a.Flags & @ExcludeFlags) = 0 
			 and ((@IsAdminMask = 1 and a.IsAdminMask = 1)
             or (@IsCommonMask = 1 and (a.IsAdminMask = 0 and a.IsUserMask = 0)))  
        order by 
            a.SortOrder
    else
        select 
            a.* 
        from 
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
        where
            a.BoardID = @BoardID and
            a.AccessMaskID = @AccessMaskID
		 and ((@IsAdminMask = 1 and a.IsAdminMask = 1)
         or (@IsCommonMask = 1 and (a.IsAdminMask = 0 and a.IsUserMask = 0)))  
        order by 
            a.SortOrder
end
GO

create procedure [{databaseSchema}].[{objectQualifier}accessmask_pforumlist](@BoardID int,@AccessMaskID int=null,@ExcludeFlags int = 0, @PageUserID INT, @IsUserMask bit, @IsCommonMask  bit) as
begin
        if @AccessMaskID is null
		if @IsCommonMask = 1
        select 
            a.* 
        from 
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
        where
            a.BoardID = @BoardID and
            (a.Flags & @ExcludeFlags) = 0 
			and ((@IsUserMask = 0 or (a.CreatedByUserID = @PageUserID and a.IsUserMask = 1)) 
		    or (@IsCommonMask = 0 or (a.IsAdminMask = 0 and a.IsUserMask = 0))) 		
        order by
		    a.IsUserMask, 
            a.SortOrder
			else
			 select 
            a.* 
        from 
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
        where
            a.BoardID = @BoardID and
            (a.Flags & @ExcludeFlags) = 0 
			and (@IsUserMask = 1 and (a.CreatedByUserID = @PageUserID and a.IsUserMask = 1)) 		    		
        order by
		    a.IsUserMask, 
            a.SortOrder
    else
        select 
            a.* 
        from 
            [{databaseSchema}].[{objectQualifier}AccessMask] a 
        where
            a.BoardID = @BoardID and
            a.AccessMaskID = @AccessMaskID 
end
GO

create procedure [{databaseSchema}].[{objectQualifier}accessmask_save](
    @AccessMaskID		int=null,
    @BoardID			int,
    @Name				nvarchar(50),
    @ReadAccess			bit,
    @PostAccess			bit,
    @ReplyAccess		bit,
    @PriorityAccess		bit,
    @PollAccess			bit,
    @VoteAccess			bit,
    @ModeratorAccess	bit,
    @EditAccess			bit,
    @DeleteAccess		bit,
    @UploadAccess		bit,
    @DownloadAccess		bit,
    @UserForumAccess    bit,
    @SortOrder          int,
    @UserID             int,
    @IsUserMask         bit,
	@IsAdminMask        bit,
    @UTCTIMESTAMP       datetime 
) as
begin
        declare @Flags	int, @UserName nvarchar(255), @UserDisplayName nvarchar(255)
    
    set @Flags = 0
    if @ReadAccess<>0 set @Flags = @Flags | 1
    if @PostAccess<>0 set @Flags = @Flags | 2
    if @ReplyAccess<>0 set @Flags = @Flags | 4
    if @PriorityAccess<>0 set @Flags = @Flags | 8
    if @PollAccess<>0 set @Flags = @Flags | 16
    if @VoteAccess<>0 set @Flags = @Flags | 32
    if @ModeratorAccess<>0 set @Flags = @Flags | 64
    if @EditAccess<>0 set @Flags = @Flags | 128
    if @DeleteAccess<>0 set @Flags = @Flags | 256
    if @UploadAccess<>0 set @Flags = @Flags | 512
    if @DownloadAccess<>0 set @Flags = @Flags | 1024
    if @UserForumAccess<>0 set @Flags = @Flags | 32768
    

    if @AccessMaskID is null
    begin
        insert into [{databaseSchema}].[{objectQualifier}AccessMask](Name,BoardID,Flags,SortOrder,IsUserMask,IsAdminMask, CreatedByUserID,CreatedByUserName, CreatedByUserDisplayName, CreatedDate)
        values(@Name,@BoardID,@Flags,@SortOrder,@IsUserMask,@IsAdminMask,@UserID,(SELECT TOP 1 Name FROM [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID),(SELECT TOP 1 DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID),@UTCTIMESTAMP)
        SET @AccessMaskID = SCOPE_IDENTITY()				
    end
    else
    begin
        update [{databaseSchema}].[{objectQualifier}AccessMask] set
            Name			= @Name,
            Flags			= @Flags,
            SortOrder       = @SortOrder,
            IsUserMask      = @IsUserMask,
			IsAdminMask     = @IsAdminMask
        where AccessMaskID=@AccessMaskID
    end
        
    if @UserID is not null
    begin
    SELECT @UserName = Name, @UserDisplayName = DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
    end
    else
    begin 
    SELECT top 1 @UserName = Name, @UserDisplayName = DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where BoardID = @BoardID and (Flags & 4) = 4 ORDER BY Joined
    end

    if not exists(SELECT TOP 1 1 FROM  [{databaseSchema}].[{objectQualifier}AccessMaskHistory] where AccessMaskID = @AccessMaskID and ChangedDate = @UTCTIMESTAMP)
    begin
    insert into [{databaseSchema}].[{objectQualifier}AccessMaskHistory](AccessMaskID,ChangedUserID,ChangedUserName,ChangedDisplayName,ChangedDate)
        values(@AccessMaskID,@UserID, @UserName,@UserDisplayName,@UTCTIMESTAMP)
    end	
    else
    begin
    UPDATE [{databaseSchema}].[{objectQualifier}AccessMaskHistory]
    SET ChangedUserID = @UserID,ChangedUserName = @UserName,ChangedDisplayName = @UserDisplayName
     where AccessMaskID = @AccessMaskID and ChangedDate = @UTCTIMESTAMP
    end	 
end
GO

create procedure [{databaseSchema}].[{objectQualifier}active_list](@BoardID int,@Guests bit=0,@ShowCrawlers bit=0,@ActiveTime int,@StyledNicks bit=0,@UTCTIMESTAMP datetime) as
begin
    delete from [{databaseSchema}].[{objectQualifier}Active] where DATEDIFF(minute,LastActive,@UTCTIMESTAMP )>@ActiveTime 
    -- we don't delete guest access
    delete from [{databaseSchema}].[{objectQualifier}ActiveAccess] where DATEDIFF(minute,LastActive,@UTCTIMESTAMP )>@ActiveTime AND  IsGuestX = 0
    -- select active	
    if @Guests<>0 
        select
            a.UserID,
            UserName = a.Name,
            UserDisplayName = a.DisplayName,
            c.IP,
            c.SessionID,
            c.ForumID,
            c.TopicID,
            ForumName = (select Name from [{databaseSchema}].[{objectQualifier}Forum] x where x.ForumID=c.ForumID),
            TopicName = (select Topic from [{databaseSchema}].[{objectQualifier}Topic] x where x.TopicID=c.TopicID),
            IsGuest = (select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x inner join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 2)<>0),
            IsCrawler = CONVERT(int, SIGN((c.Flags & 8))),
            IsHidden = ( a.IsActiveExcluded ),				
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end, 			
            UserCount = 1,
            c.[Login],
            c.LastActive,
            c.Location,
            Active = DATEDIFF(minute,c.Login,c.LastActive),
            c.Browser,
            c.[Platform],
            c.ForumPage
        from
            [{databaseSchema}].[{objectQualifier}User] a	
            JOIN [{databaseSchema}].[{objectQualifier}Rank] r on r.RankID=a.RankID	
            INNER JOIN [{databaseSchema}].[{objectQualifier}Active] c ON c.UserID = a.UserID
        where
            c.BoardID = @BoardID 	
                
        order by 
            c.LastActive desc
    else if @ShowCrawlers = 1 and @Guests = 0 
        select
            a.UserID,
            UserName = a.Name,
            UserDisplayName = a.DisplayName,
            c.IP,
            c.SessionID,
            c.ForumID,
            c.TopicID,
            ForumName = (select Name from [{databaseSchema}].[{objectQualifier}Forum] x where x.ForumID=c.ForumID),
            TopicName = (select Topic from [{databaseSchema}].[{objectQualifier}Topic] x where x.TopicID=c.TopicID),
            IsGuest = (select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x inner join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 2)<>0),
            IsCrawler = CONVERT(int, SIGN((c.Flags & 8))),
            IsHidden = ( a.IsActiveExcluded ),		
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end, 	 						
            UserCount = 1,
            c.[Login],
            c.LastActive,
            c.Location,
            Active = DATEDIFF(minute,c.Login,c.LastActive),
            c.Browser,
            c.[Platform],
            c.ForumPage
        from
            [{databaseSchema}].[{objectQualifier}User] a	
            JOIN [{databaseSchema}].[{objectQualifier}Rank] r on r.RankID=a.RankID	
            INNER JOIN [{databaseSchema}].[{objectQualifier}Active] c ON c.UserID = a.UserID							  
        where
            c.BoardID = @BoardID 
               -- is registered or is crawler 
               and ((c.Flags & 4) = 4 OR (c.Flags & 8) = 8)			  
        order by 
            c.LastActive desc
    else
        select
            a.UserID,
            UserName = a.Name,
            UserDisplayName = a.DisplayName,
            c.IP,
            c.SessionID,
            c.ForumID,
            c.TopicID,
            ForumName = (select Name from [{databaseSchema}].[{objectQualifier}Forum] x where x.ForumID=c.ForumID),
            TopicName = (select Topic from [{databaseSchema}].[{objectQualifier}Topic] x where x.TopicID=c.TopicID),
            IsGuest = (select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x inner join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 2)<>0),
            IsCrawler = CONVERT(int, SIGN((c.Flags & 8))),
            IsHidden = ( a.IsActiveExcluded ),
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end, 				
            UserCount = 1,
            c.[Login],
            c.LastActive,
            c.Location,
            Active = DATEDIFF(minute,c.Login,c.LastActive),
            c.Browser,
            c.[Platform],
            c.ForumPage
        from
            [{databaseSchema}].[{objectQualifier}User] a	
            JOIN [{databaseSchema}].[{objectQualifier}Rank] r on r.RankID=a.RankID
            INNER JOIN [{databaseSchema}].[{objectQualifier}Active] c ON c.UserID = a.UserID							  
        where
            c.BoardID = @BoardID and
            -- no guests
            not exists(				
                select 1 
                    from [{databaseSchema}].[{objectQualifier}UserGroup] x
                        inner join [{databaseSchema}].[{objectQualifier}Group] y ON y.GroupID=x.GroupID 
                    where x.UserID=a.UserID and (y.Flags & 2)<>0
                )
        order by
            c.LastActive desc
end
GO

create procedure [{databaseSchema}].[{objectQualifier}active_list_user](@BoardID int, @UserID int, @Guests bit=0, @ShowCrawlers bit = 0, @ActiveTime int,@StyledNicks bit=0, @UTCTIMESTAMP datetime) as
begin
    -- select active
    if @Guests<>0
        select
            a.UserID,
            UserName = a.Name,
            UserDisplayName = a.DisplayName,
            c.IP,
            c.SessionID,
            c.ForumID,
            HasForumAccess = CONVERT(int,x.ReadAccess),			
            c.TopicID,
            ForumName = (select [Name] from [{databaseSchema}].[{objectQualifier}Forum] x where x.ForumID=c.ForumID),
            TopicName = (select Topic from [{databaseSchema}].[{objectQualifier}Topic] x where x.TopicID=c.TopicID),
            IsGuest = ISNULL((select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x inner join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 2)<>0),0),
            IsCrawler = CONVERT(int, SIGN((c.Flags & 8))),
            IsHidden = ( a.IsActiveExcluded ),
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end, 				
            UserCount = 1,
            c.[Login],
            c.LastActive,
            c.Location,		
            Active = DATEDIFF(minute,c.Login,c.LastActive),
            c.Browser,
            c.[Platform],
            c.ForumPage
        from
            [{databaseSchema}].[{objectQualifier}User] a
            JOIN [{databaseSchema}].[{objectQualifier}Rank] r on r.RankID=a.RankID
            inner join [{databaseSchema}].[{objectQualifier}Active] c 
            ON c.UserID = a.UserID
            inner join [{databaseSchema}].[{objectQualifier}ActiveAccess] x
            ON (x.ForumID = ISNULL(c.ForumID,0))						
        where		
            c.BoardID = @BoardID AND x.UserID = @UserID		
        order by
            c.LastActive desc
        else if @ShowCrawlers = 1 and @Guests = 0 
            select
            a.UserID,
            UserName = a.Name,
            UserDisplayName = a.DisplayName,
            c.IP,
            c.SessionID,
            c.ForumID,
            HasForumAccess = CONVERT(int,x.ReadAccess),			
            c.TopicID,
            ForumName = (select [Name] from [{databaseSchema}].[{objectQualifier}Forum] x where x.ForumID=c.ForumID),
            TopicName = (select Topic from [{databaseSchema}].[{objectQualifier}Topic] x where x.TopicID=c.TopicID),
            IsGuest = ISNULL((select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x inner join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 2)<>0),0),
            IsCrawler = CONVERT(int, SIGN((c.Flags & 8))),
            IsHidden = ( a.IsActiveExcluded ),
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end, 					
            UserCount = 1,
            c.[Login],
            c.LastActive,
            c.Location,
            Active = DATEDIFF(minute,c.Login,c.LastActive),
            c.Browser,
            c.[Platform],
            c.ForumPage
        from
            [{databaseSchema}].[{objectQualifier}User] a
            JOIN [{databaseSchema}].[{objectQualifier}Rank] r on r.RankID=a.RankID
            inner join [{databaseSchema}].[{objectQualifier}Active] c 
            ON c.UserID = a.UserID
            inner join [{databaseSchema}].[{objectQualifier}ActiveAccess] x
            ON (x.ForumID = ISNULL(c.ForumID,0))						
        where		
            c.BoardID = @BoardID AND x.UserID = @UserID	     
            -- is registered or (is crawler and is registered 	
               and ((c.Flags & 4) = 4 OR (c.Flags & 8) = 8)		
        order by
            c.LastActive desc
    else
        select
            a.UserID,
            UserName = a.Name,
            UserDisplayName = a.DisplayName,
            c.IP,
            c.SessionID,
            c.ForumID,
            HasForumAccess = CONVERT(int,x.ReadAccess),			
            c.TopicID,
            ForumName = (select Name from [{databaseSchema}].[{objectQualifier}Forum] x where x.ForumID=c.ForumID),
            TopicName = (select Topic from [{databaseSchema}].[{objectQualifier}Topic] x where x.TopicID=c.TopicID),
            IsGuest = (select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x inner join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 2)<>0),
            IsCrawler = CONVERT(int, SIGN((c.Flags & 8))),
            IsHidden = ( a.IsActiveExcluded ),
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end, 					
            UserCount = 1,
            c.[Login],
            c.LastActive,
            c.Location,
            Active = DATEDIFF(minute,c.Login,c.LastActive),
            c.Browser,
            c.[Platform],
            c.ForumPage
        from
            [{databaseSchema}].[{objectQualifier}User] a	
            JOIN [{databaseSchema}].[{objectQualifier}Rank] r on r.RankID=a.RankID
            INNER JOIN [{databaseSchema}].[{objectQualifier}Active] c 
            ON c.UserID = a.UserID
            inner join [{databaseSchema}].[{objectQualifier}ActiveAccess] x
            ON (x.ForumID = ISNULL(c.ForumID,0))
            where		
            c.BoardID = @BoardID  AND x.UserID = @UserID				      
         and
            not exists(
                select 1 
                    from [{databaseSchema}].[{objectQualifier}UserGroup] x
                        inner join [{databaseSchema}].[{objectQualifier}Group] y ON y.GroupID=x.GroupID 
                    where x.UserID=a.UserID and (y.Flags & 2)<>0
                )
        order by
            c.LastActive desc
end
GO

create procedure [{databaseSchema}].[{objectQualifier}active_listforum](@ForumID int, @StyledNicks bit = 0) as
begin
        select
        UserID		= a.UserID,
        UserName	= b.Name,
        UserDisplayName = b.DisplayName,
        IsHidden	= ( b.IsActiveExcluded ),
        IsCrawler	= Convert(int,a.Flags & 8),		
        Style = case(@StyledNicks)
        when 1 then  b.UserStyle
        else ''	 end, 			
        UserCount   = (SELECT COUNT(ac.UserID) from
        [{databaseSchema}].[{objectQualifier}Active] ac with(nolock) where ac.UserID = a.UserID and ac.ForumID = @ForumID),
        Browser = a.Browser
    from
        [{databaseSchema}].[{objectQualifier}Active] a 
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
    where
        a.ForumID = @ForumID
    group by
        a.UserID,
        b.DisplayName,
        b.Name,
        b.IsActiveExcluded,
        b.UserID,
        b.UserStyle,
        a.Flags,
        a.Browser
    order by
        b.Name
end
GO

create procedure [{databaseSchema}].[{objectQualifier}active_listtopic](@TopicID int,@StyledNicks bit = 0) as
begin   
        select
        UserID		= a.UserID,
        UserName	= b.Name,
        UserDisplayName = b.DisplayName,
        IsHidden = ( b.IsActiveExcluded ),		
        IsCrawler	= Convert(int,a.Flags & 8),
        Style = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end, 	
        UserCount   = (SELECT COUNT(ac.UserID) from
        [{databaseSchema}].[{objectQualifier}Active] ac with(nolock) where ac.UserID = a.UserID and ac.TopicID = @TopicID),
        Browser = a.Browser
    from
        [{databaseSchema}].[{objectQualifier}Active] a with(nolock)
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID		
    where
        a.TopicID = @TopicID
    group by
        a.UserID,
        b.DisplayName,
        b.Name,
        b.IsActiveExcluded,
        b.UserID,
        b.UserStyle,
        a.Flags,
        a.Browser		
    order by
        b.Name
end
GO

create procedure [{databaseSchema}].[{objectQualifier}active_stats](@BoardID int) as
begin
        select
        ActiveUsers = (select count(1) from [{databaseSchema}].[{objectQualifier}Active] x JOIN [{databaseSchema}].[{objectQualifier}User] usr ON x.UserID = usr.UserID where x.BoardID = @BoardID AND usr.IsActiveExcluded = 0),
        ActiveMembers = (select count(1) from [{databaseSchema}].[{objectQualifier}Active] x JOIN [{databaseSchema}].[{objectQualifier}User] usr ON x.UserID = usr.UserID where x.BoardID = @BoardID and exists(select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] y inner join [{databaseSchema}].[{objectQualifier}Group] z on y.GroupID=z.GroupID where y.UserID=x.UserID and (z.Flags & 2)=0  AND usr.IsActiveExcluded = 0)),
        ActiveGuests = (select count(1) from [{databaseSchema}].[{objectQualifier}Active] x where x.BoardID = @BoardID and exists(select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] y inner join [{databaseSchema}].[{objectQualifier}Group] z on y.GroupID=z.GroupID where y.UserID=x.UserID and (z.Flags & 2)<>0)),
        ActiveHidden = (select count(1) from [{databaseSchema}].[{objectQualifier}Active] x JOIN [{databaseSchema}].[{objectQualifier}User] usr ON x.UserID = usr.UserID where x.BoardID = @BoardID and exists(select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] y inner join [{databaseSchema}].[{objectQualifier}Group] z on y.GroupID=z.GroupID where y.UserID=x.UserID and (z.Flags & 2)=0  AND usr.IsActiveExcluded = 1))
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}active_updatemaxstats]
(
    @BoardID int, @UTCTIMESTAMP datetime
)
AS
BEGIN
        DECLARE @count int, @max int, @maxStr nvarchar(255), @countStr nvarchar(255), @dtStr nvarchar(255)
    
    SET @count = ISNULL((SELECT COUNT(DISTINCT IP + '.' + CAST(UserID as varchar(10))) FROM [{databaseSchema}].[{objectQualifier}Active] WITH (NOLOCK) WHERE BoardID = @BoardID),0)
    SET @maxStr = (SELECT ISNULL([{databaseSchema}].[{objectQualifier}registry_value](N'maxusers', @BoardID), '1'))
    SET @max = CAST(@maxStr AS int)
    SET @countStr = CAST(@count AS nvarchar)
    SET @dtStr = CONVERT(nvarchar,@UTCTIMESTAMP,126)

    IF NOT EXISTS ( SELECT 1 FROM [{databaseSchema}].[{objectQualifier}Registry] WHERE BoardID = @BoardID and [Name] = N'maxusers')
    BEGIN 
        INSERT INTO [{databaseSchema}].[{objectQualifier}Registry](BoardID,[Name],[Value]) VALUES (@BoardID,N'maxusers',CAST(@countStr AS ntext))
        INSERT INTO [{databaseSchema}].[{objectQualifier}Registry](BoardID,[Name],[Value]) VALUES (@BoardID,N'maxuserswhen',CAST(@dtStr AS ntext))
    END
    ELSE IF (@count > @max)	
    BEGIN
        UPDATE [{databaseSchema}].[{objectQualifier}Registry] SET [Value] = CAST(@countStr AS ntext) WHERE BoardID = @BoardID AND [Name] = N'maxusers'
        UPDATE [{databaseSchema}].[{objectQualifier}Registry] SET [Value] = CAST(@dtStr AS ntext) WHERE BoardID = @BoardID AND [Name] = N'maxuserswhen'
    END
END
GO

create procedure [{databaseSchema}].[{objectQualifier}attachment_delete](@AttachmentID int) as begin
        delete from [{databaseSchema}].[{objectQualifier}Attachment] where AttachmentID=@AttachmentID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}attachment_download](@AttachmentID int) as
begin
        update [{databaseSchema}].[{objectQualifier}Attachment] set Downloads=Downloads+1 where AttachmentID=@AttachmentID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}attachment_list](@MessageID int=null,@AttachmentID int=null,@BoardID int=null,@PageIndex int = null, @PageSize int = 0) as begin

   if @MessageID is not null
        select 
            a.*,
            e.BoardID
        from
            [{databaseSchema}].[{objectQualifier}Attachment] a
            inner join [{databaseSchema}].[{objectQualifier}Message] b on b.MessageID = a.MessageID
            inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = b.TopicID
            inner join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID = c.ForumID
            inner join [{databaseSchema}].[{objectQualifier}Category] e on e.CategoryID = d.CategoryID
            inner join [{databaseSchema}].[{objectQualifier}Board] brd on brd.BoardID = e.BoardID
        where
            a.MessageID=@MessageID
    else if @AttachmentID is not null
        select 
            a.*,
            e.BoardID
        from
            [{databaseSchema}].[{objectQualifier}Attachment] a
            inner join [{databaseSchema}].[{objectQualifier}Message] b on b.MessageID = a.MessageID
            inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = b.TopicID
            inner join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID = c.ForumID
            inner join [{databaseSchema}].[{objectQualifier}Category] e on e.CategoryID = d.CategoryID
            inner join [{databaseSchema}].[{objectQualifier}Board] brd on brd.BoardID = e.BoardID
        where 
            a.AttachmentID=@AttachmentID
    else
    begin


declare @TotalRows int
declare @FirstSelectRowNumber int
declare @LastSelectRowNumber int
           set @PageIndex = @PageIndex + 1;
           set @FirstSelectRowNumber = 0;
           set @LastSelectRowNumber = 0;
           set @TotalRows = 0;
           
           select @TotalRows = count(1) from [{databaseSchema}].[{objectQualifier}Attachment] a
           inner join [{databaseSchema}].[{objectQualifier}Message] b on b.MessageID = a.MessageID
           inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = b.TopicID
           inner join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID = c.ForumID
           inner join [{databaseSchema}].[{objectQualifier}Category] e on e.CategoryID = d.CategoryID			
        where
            e.BoardID = @BoardID;

           select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
           select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
           
           with AttachmentIps as
           (
             select ROW_NUMBER() over (order by  AttachmentID) as RowNum,  AttachmentID
             from   [{databaseSchema}].[{objectQualifier}Attachment] 
           )
           select			
            a.*,
            BoardID		= @BoardID,
            Posted		= b.Posted,
            ForumID		= d.ForumID,
            ForumName	= d.Name,
            TopicID		= c.TopicID,
            TopicName	= c.Topic,
            TotalRows  = @TotalRows
        from 
		    AttachmentIps ips 
			inner join [{databaseSchema}].[{objectQualifier}Attachment] a on a.AttachmentID = ips.AttachmentID
            inner join [{databaseSchema}].[{objectQualifier}Message] b on b.MessageID = a.MessageID
            inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = b.TopicID
            inner join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID = c.ForumID
            inner join [{databaseSchema}].[{objectQualifier}Category] e on e.CategoryID = d.CategoryID			
        where
            e.BoardID = @BoardID      
            and ips.RowNum between (@FirstSelectRowNumber) and (@LastSelectRowNumber)
            order by ips.RowNum asc       
        
    end
end
GO

create procedure [{databaseSchema}].[{objectQualifier}attachment_save](@MessageID int,@FileName nvarchar(255),@Bytes int,@ContentType nvarchar(50)=null,@FileData varbinary(max)=null) as begin
        insert into [{databaseSchema}].[{objectQualifier}Attachment](MessageID,[FileName],Bytes,ContentType,Downloads,FileData) values(@MessageID,@FileName,@Bytes,@ContentType,0,@FileData)
end
GO

create procedure [{databaseSchema}].[{objectQualifier}bannedip_delete](@ID int) as
begin
        delete from [{databaseSchema}].[{objectQualifier}BannedIP] where ID = @ID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}bannedip_list](@BoardID int,@ID int=null,@PageIndex int=null, @PageSize int=null) as
begin
declare @TotalRows int
declare @FirstSelectRowNumber int
declare @LastSelectRowNumber int
 if @ID is null
        begin
           set @PageIndex = @PageIndex + 1;
           set @FirstSelectRowNumber = 0;
           set @LastSelectRowNumber = 0;
           set @TotalRows = 0;
           
           select @TotalRows = count(1) from [{databaseSchema}].[{objectQualifier}BannedIP] where BoardID=@BoardID;
           select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
           select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
           
           with BannedIps as
           (
             select ROW_NUMBER() over (order by Mask) as RowNum, Mask
             from [{databaseSchema}].[{objectQualifier}BannedIP] where BoardID=@BoardID
           )
           select
            a.*,
            @TotalRows as TotalRows
            from
            BannedIps c
            inner join [{databaseSchema}].[{objectQualifier}BannedIP] a	
            on c.Mask = a.Mask	
            where c.RowNum between (@FirstSelectRowNumber) and (@LastSelectRowNumber)
            order by c.RowNum asc
  end
  else
  select * from [{databaseSchema}].[{objectQualifier}BannedIP] where ID=@ID and BoardID=@BoardID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}bannedip_save](@ID int=null,@BoardID int,@Mask varchar(57), @Reason nvarchar(128), @UserID int, @UTCTIMESTAMP datetime) as
begin
        if (@ID is null or @ID = 0 ) 
        begin
        insert into [{databaseSchema}].[{objectQualifier}BannedIP](BoardID,Mask,Since,Reason,UserID) values(@BoardID,@Mask,@UTCTIMESTAMP ,@Reason,@UserID)
    end
    else begin
        update [{databaseSchema}].[{objectQualifier}BannedIP] set Mask = @Mask,Reason = @Reason, UserID = @UserID where ID = @ID
    end
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}board_create](
    @BoardName 		nvarchar(50),
    @Culture varchar(10),
    @LanguageFile 	nvarchar(50),
    @MembershipAppName nvarchar(50),
    @RolesAppName nvarchar(50),
    @UserName		nvarchar(255),
    @UserEmail		nvarchar(255),
    @UserKey		nvarchar(64),
    @IsHostAdmin	bit,
    @RolePrefix     nvarchar(255),
    @UTCTIMESTAMP datetime
) as 
begin
    declare @BoardID				int
    declare @TimeZone				int
    declare @ForumEmail				nvarchar(50)
    declare	@GroupIDAdmin			int
    declare	@GroupIDGuest			int
    declare @GroupIDMember			int
    declare	@AccessMaskIDAdmin		int
    declare @AccessMaskIDModerator	int
    declare @AccessMaskIDMember		int
    declare	@AccessMaskIDReadOnly	int
    declare @UserIDAdmin			int
    declare @UserIDGuest			int
    declare @RankIDAdmin			int
    declare @RankIDGuest			int
    declare @RankIDNewbie			int
    declare @RankIDMember			int
    declare @RankIDAdvanced			int
    declare	@CategoryID				int
    declare	@ForumID				int
    declare @UserFlags				int

	
    -- Board
    INSERT INTO [{databaseSchema}].[{objectQualifier}Board](Name, AllowThreaded, MembershipAppName, RolesAppName ) values(@BoardName,0, @MembershipAppName, @RolesAppName)
    SET @BoardID = SCOPE_IDENTITY()

    SET @TimeZone = (SELECT ISNULL(CAST([{databaseSchema}].[{objectQualifier}registry_value](N'TimeZone', @BoardID) as int), 0))
    SET @ForumEmail = (SELECT [{databaseSchema}].[{objectQualifier}registry_value](N'ForumEmail', @BoardID))
    
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'culture',@Culture,@BoardID
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'language',@LanguageFile,@BoardID
    
    -- Rank
    INSERT INTO [{databaseSchema}].[{objectQualifier}Rank](BoardID,Name,Flags,MinPosts,PMLimit,Style,SortOrder) VALUES (@BoardID,'Administration',0,null,2147483647,'default!font-size: 8pt; color: #811334/yafpro!font-size: 8pt; color:blue',0)
    SET @RankIDAdmin = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}Rank](BoardID,Name,Flags,MinPosts,PMLimit,SortOrder) VALUES(@BoardID,'Guest',0,null,0,100)
    SET @RankIDGuest = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}Rank](BoardID,Name,Flags,MinPosts,PMLimit,SortOrder) VALUES(@BoardID,'Newbie',3,0,10,3)
    SET @RankIDNewbie = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}Rank](BoardID,Name,Flags,MinPosts,PMLimit,SortOrder) VALUES(@BoardID,'Member',2,10,30,2)
    SET @RankIDMember = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}Rank](BoardID,Name,Flags,MinPosts,PMLimit,SortOrder) VALUES(@BoardID,'Advanced Member',2,30,100,1)
    SET @RankIDAdvanced = SCOPE_IDENTITY()

    -- AccessMask
    INSERT INTO [{databaseSchema}].[{objectQualifier}AccessMask](BoardID,Name,Flags,SortOrder)
    VALUES(@BoardID,'Admin Access',1023 + 1024,4)
    SET @AccessMaskIDAdmin = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}AccessMask](BoardID,Name,Flags,SortOrder)
    VALUES(@BoardID,'Moderator Access',487 + 1024,3)
    SET @AccessMaskIDModerator = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}AccessMask](BoardID,Name,Flags,SortOrder)
    VALUES(@BoardID,'Member Access',423 + 1024,2)
    SET @AccessMaskIDMember = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}AccessMask](BoardID,Name,Flags,SortOrder)
    VALUES(@BoardID,'Read Only Access',1,1)
    SET @AccessMaskIDReadOnly = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}AccessMask](BoardID,Name,Flags,SortOrder)
    VALUES(@BoardID,'No Access',0,0)

    -- Group
    INSERT INTO [{databaseSchema}].[{objectQualifier}Group](BoardID,Name,Flags,PMLimit,Style,SortOrder,
	UsrSigChars,UsrSigBBCodes,UsrAlbums,UsrAlbumImages,IsUserGroup,IsHidden) 
	values(@BoardID, ISNULL(@RolePrefix,'') + 'Administrators',1,2147483647,'default!font-size: 8pt; color: red/yafpro!font-size: 8pt; color:blue',0,256,
	'URL,IMG,SPOILER,QUOTE',10,120,0,0)
    set @GroupIDAdmin = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}Group](BoardID,Name,Flags,PMLimit,Style,SortOrder,
	UsrSigChars,UsrSigBBCodes,UsrAlbums,UsrAlbumImages,IsUserGroup,IsHidden) 
	values(@BoardID,'Guests',2,0,'default!font-size: 8pt; font-style: italic; font-weight: bold; color: #0c7333/yafpro!font-size: 8pt; color: #6e1987',1,0,
	null,0,0,0,0)
    SET @GroupIDGuest = SCOPE_IDENTITY()
    INSERT INTO [{databaseSchema}].[{objectQualifier}Group](BoardID,Name,Flags,PMLimit,SortOrder,
	UsrSigChars,UsrSigBBCodes,UsrAlbums,UsrAlbumImages,IsUserGroup,IsHidden) 
	values(@BoardID,ISNULL(@RolePrefix,'') + 'Registered',4,100,1,128,'URL,IMG,SPOILER,QUOTE',5,30,0,0)

    SET @GroupIDMember = SCOPE_IDENTITY()	
    
    -- User (GUEST)
    INSERT INTO [{databaseSchema}].[{objectQualifier}User](BoardID,RankID,[Name],DisplayName,[Password],Joined,LastVisit,NumPosts,TimeZone,Email,Flags)
    VALUES(@BoardID,@RankIDGuest,'Guest','Guest','na',@UTCTIMESTAMP ,@UTCTIMESTAMP ,0,@TimeZone,@ForumEmail,6)
    SET @UserIDGuest = SCOPE_IDENTITY()	
    
    SET @UserFlags = 2
    if @IsHostAdmin<>0 SET @UserFlags = 3
    
    -- User (ADMIN)
    INSERT INTO [{databaseSchema}].[{objectQualifier}User](BoardID,RankID,[Name],DisplayName, [Password], Email,ProviderUserKey, Joined,LastVisit,NumPosts,TimeZone,Flags)
    VALUES(@BoardID,@RankIDAdmin,@UserName,@UserName,'na',@UserEmail,@UserKey,@UTCTIMESTAMP ,@UTCTIMESTAMP ,0,@TimeZone,@UserFlags)
    SET @UserIDAdmin = SCOPE_IDENTITY()

	-- update all groups that they were created by admin
	update [{databaseSchema}].[{objectQualifier}Group]
	set    CreatedByUserID = @UserIDAdmin,
           CreatedByUserName = @UserName,
           CreatedByUserDisplayName = @UserName,
           CreatedDate = @UTCTIMESTAMP;
    
    -- UserGroup
    INSERT INTO [{databaseSchema}].[{objectQualifier}UserGroup](UserID,GroupID) VALUES(@UserIDAdmin,@GroupIDAdmin)
    INSERT INTO [{databaseSchema}].[{objectQualifier}UserGroup](UserID,GroupID) VALUES(@UserIDGuest,@GroupIDGuest)

    -- Category
    INSERT INTO [{databaseSchema}].[{objectQualifier}Category](BoardID,Name,SortOrder) VALUES(@BoardID,'Test Category',1)
    set @CategoryID = SCOPE_IDENTITY()
    
    -- Forum
    INSERT INTO [{databaseSchema}].[{objectQualifier}Forum](CategoryID,Name,Description,SortOrder,NumTopics,NumPosts,Flags, left_key, right_key, [level])
    VALUES(@CategoryID,'Test Forum','A test forum',1,0,0,4,1,2,0)
    set @ForumID = SCOPE_IDENTITY()

    -- ForumAccess
    INSERT INTO [{databaseSchema}].[{objectQualifier}ForumAccess](GroupID,ForumID,AccessMaskID) VALUES(@GroupIDAdmin,@ForumID,@AccessMaskIDAdmin)
    INSERT INTO [{databaseSchema}].[{objectQualifier}ForumAccess](GroupID,ForumID,AccessMaskID) VALUES(@GroupIDGuest,@ForumID,@AccessMaskIDReadOnly)
    INSERT INTO [{databaseSchema}].[{objectQualifier}ForumAccess](GroupID,ForumID,AccessMaskID) VALUES(@GroupIDMember,@ForumID,@AccessMaskIDMember)
	
    SELECT @BoardID;
end
GO

create procedure [{databaseSchema}].[{objectQualifier}board_delete](@BoardID int) as
begin
        declare @tmpForumID int;

    declare forum_cursor cursor for
        select ForumID 
        from [{databaseSchema}].[{objectQualifier}Forum] a join [{databaseSchema}].[{objectQualifier}Category] b on a.CategoryID=b.CategoryID
        where b.BoardID=@BoardID
        order by ForumID desc
    
    open forum_cursor
    fetch next from forum_cursor into @tmpForumID
    while @@FETCH_STATUS = 0
    begin
        exec [{databaseSchema}].[{objectQualifier}forum_delete] @tmpForumID, 0, 0;
        fetch next from forum_cursor into @tmpForumID
    end
    close forum_cursor
    deallocate forum_cursor

    delete from [{databaseSchema}].[{objectQualifier}ForumAccess] where exists(select 1 from [{databaseSchema}].[{objectQualifier}Group] x where x.GroupID=[{databaseSchema}].[{objectQualifier}ForumAccess].GroupID and x.BoardID=@BoardID)
    delete from [{databaseSchema}].[{objectQualifier}Forum] where exists(select 1 from [{databaseSchema}].[{objectQualifier}Category] x where x.CategoryID=[{databaseSchema}].[{objectQualifier}Forum].CategoryID and x.BoardID=@BoardID)
    delete from [{databaseSchema}].[{objectQualifier}UserGroup] where exists(select 1 from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=[{databaseSchema}].[{objectQualifier}UserGroup].UserID and x.BoardID=@BoardID)
    delete from [{databaseSchema}].[{objectQualifier}Category] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}ActiveAccess] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Active] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Rank] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Group] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}AccessMask] where BoardID=@BoardID	
    delete from [{databaseSchema}].[{objectQualifier}BBCode] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Extension] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}ShoutboxMessage] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Medal] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Smiley] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Replace_Words] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}NntpServer] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}BannedIP] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Registry] where BoardID=@BoardID
    delete from [{databaseSchema}].[{objectQualifier}Board] where BoardID=@BoardID	
end
GO

create procedure [{databaseSchema}].[{objectQualifier}board_list](@BoardID int=null) as
begin
        select
        a.*,
        SQLVersion = @@VERSION
    from 
        [{databaseSchema}].[{objectQualifier}Board] a
    where
        (@BoardID is null or a.BoardID = @BoardID)
end
GO

create procedure [{databaseSchema}].[{objectQualifier}board_poststats](@BoardID int, @StyledNicks bit = 0, @ShowNoCountPosts bit = 0, @GetDefaults bit = 0 , @UtcTimestamp datetime) as
BEGIN

-- vzrus: while  a new installation or like this we don't have the row and should return a dummy data
IF @GetDefaults <= 0
BEGIN
        SELECT TOP 1 
        Posts = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] a join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID=a.TopicID join [{databaseSchema}].[{objectQualifier}Forum] c on c.ForumID=b.ForumID join [{databaseSchema}].[{objectQualifier}Category] d on d.CategoryID=c.CategoryID where d.BoardID=@BoardID AND (a.Flags & 24)=16),
        Topics = (select count(1) from [{databaseSchema}].[{objectQualifier}Topic] a join [{databaseSchema}].[{objectQualifier}Forum] b on b.ForumID=a.ForumID join [{databaseSchema}].[{objectQualifier}Category] c on c.CategoryID=b.CategoryID where c.BoardID=@BoardID AND a.IsDeleted = 0),
        Forums = (select count(1) from [{databaseSchema}].[{objectQualifier}Forum] a join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID),	
        LastPostInfoID	= 1,
        LastPost	= a.Posted,
        LastUserID	= a.UserID,
        LastUser	= e.Name,
        LastUserDisplayName	= e.DisplayName,
        LastUserStyle =  case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = a.UserID)
            else ''	 end
            FROM 
                [{databaseSchema}].[{objectQualifier}Message] a 
                join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID=a.TopicID 
                join [{databaseSchema}].[{objectQualifier}Forum] c on c.ForumID=b.ForumID 
                join [{databaseSchema}].[{objectQualifier}Category] d on d.CategoryID=c.CategoryID 
                join [{databaseSchema}].[{objectQualifier}User] e on e.UserID=a.UserID						
            WHERE 
                (a.Flags & 24) = 16
                AND b.IsDeleted = 0 
                AND d.BoardID = @BoardID 
                AND c.[IsNoCount] <> (CASE WHEN @ShowNoCountPosts > 0 THEN -1 ELSE 1 END)
            ORDER BY
                a.Posted DESC
        END
        ELSE
        BEGIN
        SELECT
        Posts = 0,
        Topics = 0,
        Forums = 1,	
        LastPostInfoID	= 1,
        LastPost	= null,
        LastUserID	= null,
        LastUser	= null,
        LastUserDisplayName	= null,
        LastUserStyle = ''
        END
        -- this can be in any very rare updatable cached place 
		-- first delete tags
        DELETE FROM [{databaseSchema}].[{objectQualifier}TopicTags] 
		 WHERE TopicID IN (SELECT TopicID FROM [{databaseSchema}].[{objectQualifier}Topic] WHERE LinkDate IS NOT NULL AND LinkDate < @UtcTimestamp);
        -- then a link
        DELETE FROM [{databaseSchema}].[{objectQualifier}Topic] where TopicMovedID IS NOT NULL AND LinkDate IS NOT NULL AND LinkDate < @UtcTimestamp
        
END
GO

create procedure [{databaseSchema}].[{objectQualifier}board_userstats](@BoardID int, @StyledNicks bit) as
BEGIN
        SELECT		
        Members = (select count(1) from [{databaseSchema}].[{objectQualifier}User] a where a.BoardID=@BoardID AND (Flags & 2) = 2 AND (a.Flags & 4) = 0),
        MaxUsers = (SELECT [{databaseSchema}].[{objectQualifier}registry_value](N'maxusers', @BoardID)),
        MaxUsersWhen = (SELECT [{databaseSchema}].[{objectQualifier}registry_value](N'maxuserswhen', @BoardID)),
        LastMemberInfo.*
    FROM
        (
            SELECT TOP 1 
                LastMemberInfoID= 1,
                LastMemberID	= UserID,
                LastMember	= [Name],
                LastMemberDisplayName	= [DisplayName]
            FROM 
                [{databaseSchema}].[{objectQualifier}User]
            WHERE 
               -- is approved
                (Flags & 2) = 2
                -- is not a guest
                AND (Flags & 4) <> 4
                AND BoardID = @BoardID 
            ORDER BY 
                Joined DESC
        ) as LastMemberInfo
        
END
GO

create procedure [{databaseSchema}].[{objectQualifier}board_save](@BoardID int,@Name nvarchar(50), @LanguageFile nvarchar(50), @Culture varchar(10), @AllowThreaded bit) as
begin

        EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'culture', @Culture, @BoardID
        EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'language', @LanguageFile, @BoardID
        update [{databaseSchema}].[{objectQualifier}Board] set
        Name = @Name,
        AllowThreaded = @AllowThreaded
    where BoardID=@BoardID
    select @BoardID 
end
GO

create procedure [{databaseSchema}].[{objectQualifier}board_stats]
    @BoardID	int = null
as 
begin
        if (@BoardID is null) begin
        select
            NumPosts	= (select count(1) from [{databaseSchema}].[{objectQualifier}Message] where IsApproved = 1 AND IsDeleted = 0),
            NumTopics	= (select count(1) from [{databaseSchema}].[{objectQualifier}Topic] where IsDeleted = 0),
            NumUsers	= (select count(1) from [{databaseSchema}].[{objectQualifier}User] where IsApproved = 1),
            BoardStart	= (select min(Joined) from [{databaseSchema}].[{objectQualifier}User])
    end
    else begin
        select
            NumPosts	= (select count(1)	
                                from [{databaseSchema}].[{objectQualifier}Message] a
                                join [{databaseSchema}].[{objectQualifier}Topic] b ON a.TopicID=b.TopicID
                                join [{databaseSchema}].[{objectQualifier}Forum] c ON b.ForumID=c.ForumID
                                join [{databaseSchema}].[{objectQualifier}Category] d ON c.CategoryID=d.CategoryID
                                where a.IsApproved = 1 AND a.IsDeleted = 0 and b.IsDeleted = 0 AND d.BoardID=@BoardID
                            ),
            NumTopics	= (select count(1) 
                                from [{databaseSchema}].[{objectQualifier}Topic] a
                                join [{databaseSchema}].[{objectQualifier}Forum] b ON a.ForumID=b.ForumID
                                join [{databaseSchema}].[{objectQualifier}Category] c ON b.CategoryID=c.CategoryID
                                where c.BoardID=@BoardID AND a.IsDeleted = 0
                            ),
            NumUsers	= (select count(1) from [{databaseSchema}].[{objectQualifier}User] where IsApproved = 1 and BoardID=@BoardID),
            BoardStart	= (select min(Joined) from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID)
    end
end
GO

create procedure [{databaseSchema}].[{objectQualifier}category_delete](@CategoryID int, @NewCategoryID int) as
begin
   declare @flag int
   if (@NewCategoryID is not null) 
		  begin
	         update [{databaseSchema}].[{objectQualifier}Forum] set CategoryID = @NewCategoryID where CategoryID = @CategoryID;
			 execute [{databaseSchema}].[{objectQualifier}forum_ns_recreate] null, @CategoryID	
			 execute [{databaseSchema}].[{objectQualifier}forum_ns_recreate] null, @NewCategoryID	
		  end

    if exists(select 1 from [{databaseSchema}].[{objectQualifier}Forum] where CategoryID = @CategoryID)
    begin
        set @flag = 0
    end else
    begin	  
        delete from [{databaseSchema}].[{objectQualifier}Category] where CategoryID = @CategoryID
        set @flag = 1		
    end
	
	
    select @flag
end
GO

create procedure [{databaseSchema}].[{objectQualifier}category_list](@BoardID int,@CategoryID int=null) as
begin
        if @CategoryID is null
        select * from [{databaseSchema}].[{objectQualifier}Category] where BoardID = @BoardID order by SortOrder
    else
        select * from [{databaseSchema}].[{objectQualifier}Category] where BoardID = @BoardID and CategoryID = @CategoryID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}category_getadjacentforum](@BoardID int,@CategoryID int=null,@PageUserID int, @IsAfter bit) as
begin
declare @ForumID int;
declare @SortOrder int;
SET @ForumID = 0;
SET @SortOrder = 0;
   -- find first forum in category and its sort order. If no forums, returns 0 for both and is a first forum in a category.
   if @IsAfter > 0 begin 
    select TOP 1 @ForumID=ISNULL(f.ForumID,0),@SortOrder=ISNULL(f.SortOrder,0) from [{databaseSchema}].[{objectQualifier}Forum] f 
	JOIN [{databaseSchema}].[{objectQualifier}Category] c
	ON c.CategoryID = f.CategoryID
	JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa
	ON (aa.ForumID = f.ForumID and aa.UserID = @PageUserID)
	where c.BoardID = @BoardID and c.CategoryID = @CategoryID order by f.SortOrder 
   end

   -- increase order to shift forums order in the category
  if  @ForumID > 0 and @SortOrder = 0
  begin
  UPDATE [{databaseSchema}].[{objectQualifier}Forum] set SortOrder = SortOrder + 1 where CategoryID =  @CategoryID
  end
   SELECT @ForumID as ForumID, @SortOrder as SortOrder
end
GO

create procedure [{databaseSchema}].[{objectQualifier}category_listread](@BoardID int,@UserID int,@CategoryID int=null) as
begin
        select 
        a.CategoryID,
        a.Name,
        a.CategoryImage
    from 
        [{databaseSchema}].[{objectQualifier}Category] a
        join [{databaseSchema}].[{objectQualifier}Forum] b on b.CategoryID=a.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] v on v.ForumID=b.ForumID
    where
        a.BoardID=@BoardID and
        v.UserID=@UserID and
        (CONVERT(int,v.ReadAccess)<>0 or (b.Flags & 2)=0) and
        (@CategoryID is null or a.CategoryID=@CategoryID) and
        b.ParentID is null
    group by
        a.CategoryID,
        a.Name,
        a.SortOrder,
        a.CategoryImage
    order by 
        a.SortOrder
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}category_pfaccesslist](@BoardID INT, @CategoryID INT) 
as
BEGIN
    IF @CategoryID IS NULL 
        SELECT c.*,
        IsNULL((SELECT top 1 SIGN(f.ForumID) FROM [{databaseSchema}].[{objectQualifier}Forum] f where f.CategoryID = c.CategoryID and f.CanHavePersForums = 1 ),0) AS HasForumsForPersForums
        FROM [{databaseSchema}].[{objectQualifier}Category] c WHERE c.BoardID = @BoardID ORDER BY c.SortOrder;
    ELSE
        SELECT c.*,
        IsNULL((SELECT top 1 SIGN(f.ForumID) FROM [{databaseSchema}].[{objectQualifier}Forum] f where f.CategoryID = c.CategoryID and f.CanHavePersForums  = 1 ),0)  AS HasForumsForPersForums
        FROM [{databaseSchema}].[{objectQualifier}Category] c WHERE c.BoardID = @BoardID AND c.CategoryID = @CategoryID;       
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}category_save]
(
    @BoardID    INT,
    @CategoryID INT,
    @Name       NVARCHAR(128),	
    @SortOrder  SMALLINT,
    @CategoryImage NVARCHAR(255) = NULL,
	@CanHavePersForums BIT,
	@AdjacentCategoryID INT,
	@AdjacentCategoryMode INT
)
AS
BEGIN
declare @OldSortOrder int;
declare @OldBoardID   int;
declare @tmp int;
declare @cntr int = 0;
declare @afterset bit;

  select @OldBoardID = BoardID, @OldSortOrder = SortOrder 
			from [{databaseSchema}].[{objectQualifier}Category]
            WHERE  CategoryID = @CategoryID

    -- re-order categories removing gaps, create sortorder gap for a category
	 if (@AdjacentCategoryID is not null) 	
	 begin
	  	  -- over doesn't possible 	
	declare c cursor for
		select CategoryID from [{databaseSchema}].[{objectQualifier}Category]
		where BoardID = @BoardID order by SortOrder, CategoryID
		
		open c
		
		fetch next from c into @tmp
		while @@FETCH_STATUS = 0
		begin
		if (@AdjacentCategoryID = @tmp) 
		begin
		-- before
		if (@AdjacentCategoryMode = 1) 
		begin
		select @SortOrder = @cntr;
		select @cntr = @cntr + 1;
		end
		-- after
		if (@AdjacentCategoryMode = 2) 
		begin
		select @SortOrder = @cntr + 1;
		select @afterset = 1;	
		end
		end 
		-- this is after gap
		if (@SortOrder = @cntr and @afterset = 1)
		begin
		select @cntr = @cntr + 1;
		select @afterset = 0;
		end
		update	[{databaseSchema}].[{objectQualifier}Category]
		set SortOrder = @cntr where CategoryID = @tmp;
		select @cntr = @cntr + 1;
		
			fetch next from c into @tmp
		end
		close c
		deallocate c

      end
        IF @CategoryID > 0
        BEGIN
	    select @OldBoardID = BoardID, @OldSortOrder = SortOrder 
			from [{databaseSchema}].[{objectQualifier}Category]
            WHERE  CategoryID = @CategoryID

        UPDATE [{databaseSchema}].[{objectQualifier}Category]
        SET    Name = @Name,
               CategoryImage = @CategoryImage,
			   CanHavePersForums = @CanHavePersForums,
               SortOrder = (CASE WHEN @AdjacentCategoryMode = -1  THEN SortOrder ELSE @SortOrder END)
        WHERE  CategoryID = @CategoryID
        SELECT CategoryID = @CategoryID
    END
    ELSE
    BEGIN
        INSERT INTO [{databaseSchema}].[{objectQualifier}Category]
                   (BoardID,
                    [Name],
                    [CategoryImage],
					[CanHavePersForums],
                    SortOrder)
        VALUES     (@BoardID,
                    @Name,
                    @CategoryImage,
					@CanHavePersForums,
                    @SortOrder)
        SELECT @CategoryID = Scope_identity()		
    END
	SELECT CategoryID = @CategoryID	
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}checkemail_list]
(
    @Email nvarchar(255) = null
)
AS
BEGIN
        IF @Email IS NULL
        SELECT * FROM [{databaseSchema}].[{objectQualifier}CheckEmail]
    ELSE
        SELECT * FROM [{databaseSchema}].[{objectQualifier}CheckEmail] WHERE Email = LOWER(@Email)
END
GO

create procedure [{databaseSchema}].[{objectQualifier}checkemail_save]
(
    @UserID int,
    @Hash nvarchar(32),
    @Email nvarchar(255),
    @UTCTIMESTAMP datetime
)
AS
BEGIN
        INSERT INTO [{databaseSchema}].[{objectQualifier}CheckEmail]
        (UserID,Email,Created,Hash)
    VALUES
        (@UserID,LOWER(@Email),@UTCTIMESTAMP ,@Hash)	
END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}checkemail_update](@Hash nvarchar(32)) as
begin
        declare @UserID int
    declare @CheckEmailID int
    declare @Email nvarchar(255)

    set @UserID = null

    select 
        @CheckEmailID = CheckEmailID,
        @UserID = UserID,
        @Email = Email
    from
        [{databaseSchema}].[{objectQualifier}CheckEmail]
    where
        Hash = @Hash

    if @UserID is null
    begin
        select convert(nvarchar(64),NULL) as ProviderUserKey, convert(nvarchar(255),NULL) as Email
        return
    end

    -- Update new user email
    update [{databaseSchema}].[{objectQualifier}User] set Email = LOWER(@Email), Flags = Flags | 2 where UserID = @UserID
    delete [{databaseSchema}].[{objectQualifier}CheckEmail] where CheckEmailID = @CheckEmailID

    -- return the UserProviderKey
    SELECT ProviderUserKey, Email FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserID = @UserID
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}choice_vote](@ChoiceID int,@UserID int = NULL, @RemoteIP varchar(39) = NULL) AS
BEGIN
        DECLARE @PollID int

    SET @PollID = (SELECT PollID FROM [{databaseSchema}].[{objectQualifier}Choice] WHERE ChoiceID = @ChoiceID)

    IF @UserID = NULL
    BEGIN
        IF @RemoteIP != NULL
        BEGIN
            INSERT INTO [{databaseSchema}].[{objectQualifier}PollVote] (PollID, UserID, RemoteIP, ChoiceID) VALUES (@PollID,NULL,@RemoteIP, @ChoiceID)	
        END
    END
    ELSE
    BEGIN
        INSERT INTO [{databaseSchema}].[{objectQualifier}PollVote] (PollID, UserID, RemoteIP, ChoiceID) VALUES (@PollID,@UserID,@RemoteIP,@ChoiceID)
    END

    UPDATE [{databaseSchema}].[{objectQualifier}Choice] SET Votes = Votes + 1 WHERE ChoiceID = @ChoiceID
END
GO

create procedure [{databaseSchema}].[{objectQualifier}eventlog_create](@UserID int,@Source nvarchar(50),@Description ntext,@Type int,@UTCTIMESTAMP datetime) as
begin
        insert into [{databaseSchema}].[{objectQualifier}EventLog](UserID,Source,[Description],[Type])
    values(@UserID,@Source,@Description,@Type)	
end
GO

create procedure [{databaseSchema}].[{objectQualifier}eventlog_delete]
(
    @EventLogID int = null, 
    @BoardID int = null,
    @PageUserID int
) as
begin
        -- either EventLogID or BoardID must be null, not both at the same time
    if (@EventLogID is null) begin
        -- delete all events of this board
        delete from [{databaseSchema}].[{objectQualifier}EventLog]
        where
            (UserID is null or
            UserID in (select UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID))
    end
    else begin
        -- delete just one event
        delete from [{databaseSchema}].[{objectQualifier}EventLog] where EventLogID=@EventLogID
    end
end
GO

create procedure [{databaseSchema}].[{objectQualifier}eventlog_deletebyuser]
(	
    @BoardID int = null,
    @PageUserID int 
) as
begin
if (exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}User] where ((Flags & 1) = 1 and UserID = @PageUserID)))
begin
delete from [{databaseSchema}].[{objectQualifier}EventLog] where
            (UserID is null or
            UserID in (select UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID))
end
else
begin
declare @tmp_evlogdelacc table (EventLogTID int);

        -- either EventLogID or BoardID must be null, not both at the same time
    insert into	@tmp_evlogdelacc(EventLogTID)
    select a.EventLogID from [{databaseSchema}].[{objectQualifier}EventLog] a
        left join [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] e on e.EventTypeID = a.[Type] 
        join [{databaseSchema}].[{objectQualifier}UserGroup] ug on (ug.UserID =  @PageUserID and ug.GroupID = e.GroupID)
        left join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
        where e.DeleteAccess = 1
        delete from [{databaseSchema}].[{objectQualifier}EventLog]
        where EventLogID in (select EventLogTID from @tmp_evlogdelacc)
    end	
end
GO

create procedure [{databaseSchema}].[{objectQualifier}eventlog_list](@BoardID int, @PageUserID int, @MaxRows int, @MaxDays int,  @PageIndex int,
   @PageSize int, @SinceDate datetime, @ToDate datetime, @EventIDs varchar(4000) = null,
@UTCTIMESTAMP datetime) as
begin
   declare @TotalRows int 
   declare @FirstSelectRowNumber int 
   declare @FirstSelectRowID int
   declare @ParsedEventIDs table
   (
     EventID int
   ) 

   -- check here if the value is not empty
   if REPLACE(@EventIDs, ',', '') <> ''
   begin
        insert into @ParsedEventIDs
        select * from [{databaseSchema}].[{objectQualifier}table_intfromdelimitedstr](@EventIDs,',');
   end

   -- delete entries older than @MaxDays days
      delete from [{databaseSchema}].[{objectQualifier}EventLog] where EventTime+@MaxDays<@UTCTIMESTAMP 

   -- or if there are more then @MaxRows	
   if ((select count(1) from [{databaseSchema}].[{objectQualifier}eventlog]) >= @MaxRows + 50)
   begin		
        delete from [{databaseSchema}].[{objectQualifier}EventLog] WHERE EventLogID IN (SELECT TOP 100 EventLogID FROM [{databaseSchema}].[{objectQualifier}EventLog] ORDER BY EventTime)
   end	

  set nocount on
  set @PageIndex = @PageIndex + 1
  -- user is host admin - a shortcut without checking access
    if (exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}User] where ((Flags & 1) = 1 and UserID = @PageUserID)))		
    begin
      set @FirstSelectRowNumber = 0
      set @FirstSelectRowID = 0
      set @TotalRows = 0

        select @TotalRows = count(1) from
        [{databaseSchema}].[{objectQualifier}EventLog] a		
        left join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
        where	   
        (b.UserID IS NULL or b.BoardID = @BoardID)	and ((@EventIDs IS NULL )  OR  a.[Type] IN (select * from @ParsedEventIDs))  and EventTime between @SinceDate and @ToDate
    
        select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1

    if (@FirstSelectRowNumber <= @TotalRows)
        begin
           -- find first selectedrowid 
  
    set rowcount @FirstSelectRowNumber
   end
   else
   begin   
   set rowcount 1
   end
       
        select @FirstSelectRowID = EventLogID 
       from
        [{databaseSchema}].[{objectQualifier}EventLog] a		
        left join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
        where	   
        (b.UserID IS NULL or b.BoardID = @BoardID) and (@EventIDs IS NULL OR  a.[Type] IN (select * from @ParsedEventIDs))  and a.EventTime between @SinceDate and @ToDate
        order by a.EventLogID desc

      set rowcount @PageSize
      select
        a.*,		
        ISNULL(b.[Name],'System') as [Name],
        TotalRows = @TotalRows
    from
        [{databaseSchema}].[{objectQualifier}EventLog] a		
        left join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
      where EventLogID <= @FirstSelectRowID  and (b.UserID IS NULL or b.BoardID = @BoardID)	and (@EventIDs IS NULL OR  a.[Type] IN (select * from @ParsedEventIDs)) -- and a.EventTime between @SinceDate and @ToDate
      order by a.EventLogID   desc   
   end  
else
-- end of the shortcut for host admin
begin
        select @TotalRows = count(1)  from
        [{databaseSchema}].[{objectQualifier}EventLog] a
        left join [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] e on e.EventTypeID = a.[Type]
        join [{databaseSchema}].[{objectQualifier}UserGroup] ug on (ug.UserID =  @PageUserID and ug.GroupID = e.GroupID)
        left join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
    where	 
        (b.UserID IS NULL or b.BoardID = @BoardID)		and (@EventIDs IS NULL OR  a.[Type] IN (select * from @ParsedEventIDs))	 and a.EventTime between @SinceDate and @ToDate
    
        select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1
                   -- find first selectedrowid 
   if (@TotalRows > 0)
   begin
    set rowcount @FirstSelectRowNumber
   end
   else
   begin   
   set rowcount 1
   end

        select @FirstSelectRowID = EventLogID 
      from
        [{databaseSchema}].[{objectQualifier}EventLog] a
        left join [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] e on e.EventTypeID = a.[Type]
        join [{databaseSchema}].[{objectQualifier}UserGroup] ug on (ug.UserID =  @PageUserID and ug.GroupID = e.GroupID)
        left join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
    where	   
        (b.UserID IS NULL or b.BoardID = @BoardID)	and (@EventIDs IS NULL OR  a.[Type] IN (select * from @ParsedEventIDs))	 and a.EventTime between @SinceDate and @ToDate
        order by  a.EventLogID   desc

      set rowcount @PageSize
      select
      a.*,		
        ISNULL(b.[Name],'System') as [Name],
        TotalRows = @TotalRows
         from
        [{databaseSchema}].[{objectQualifier}EventLog] a
        left join [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] e on e.EventTypeID = a.[Type]
        join [{databaseSchema}].[{objectQualifier}UserGroup] ug on (ug.UserID =  @PageUserID and ug.GroupID = e.GroupID)
        left join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
    where	  EventLogID <= @FirstSelectRowID and (b.UserID IS NULL or b.BoardID = @BoardID) and (@EventIDs IS NULL OR  a.[Type] IN (select * from @ParsedEventIDs)) and a.EventTime between @SinceDate and @ToDate	
      order by a.EventLogID  desc   
   end  
 set nocount off

end
GO

create procedure [{databaseSchema}].[{objectQualifier}extension_delete] (@ExtensionID int) as
begin
        delete from [{databaseSchema}].[{objectQualifier}Extension] 
    where ExtensionID = @ExtensionID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}extension_edit] (@ExtensionID int=NULL) as
BEGIN
        SELECT * 
    FROM [{databaseSchema}].[{objectQualifier}Extension] 
    WHERE ExtensionID = @ExtensionID 
    ORDER BY Extension
END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}extension_list] (@BoardID int, @Extension nvarchar(10)) as
BEGIN
    
    -- If an extension is passed, then we want to check for THAT extension
    IF LEN(@Extension) > 0
        BEGIN
            SELECT
                a.*
            FROM
                [{databaseSchema}].[{objectQualifier}Extension] a
            WHERE
                a.BoardID = @BoardID AND a.Extension=@Extension
            ORDER BY
                a.Extension
        END

    ELSE
        -- Otherwise, just get a list for the given @BoardId
        BEGIN
            SELECT
                a.*
            FROM
                [{databaseSchema}].[{objectQualifier}Extension] a
            WHERE
                a.BoardID = @BoardID	
            ORDER BY
                a.Extension
        END
END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}extension_save] (@ExtensionID int=null,@BoardID int,@Extension nvarchar(10)) as
begin
        if @ExtensionID is null or @ExtensionID = 0 begin
        insert into [{databaseSchema}].[{objectQualifier}Extension] (BoardID,Extension) 
        values(@BoardID,@Extension)
    end
    else begin
        update [{databaseSchema}].[{objectQualifier}Extension] 
        set Extension = @Extension 
        where ExtensionID = @ExtensionID
    end
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}forum_delete](@ForumID int, @MoveChildren bit, @RebuildTree bit) as
begin  
        -- Maybe an idea to use cascading foreign keys instead? Too bad they don't work on MS SQL 7.0...
    update [{databaseSchema}].[{objectQualifier}Forum] set LastMessageID=null,LastTopicID=null where ForumID=@ForumID
    update [{databaseSchema}].[{objectQualifier}Topic] set LastMessageID=null where ForumID=@ForumID
    update [{databaseSchema}].[{objectQualifier}Active] set ForumID=null where ForumID=@ForumID
    delete from [{databaseSchema}].[{objectQualifier}WatchTopic] from [{databaseSchema}].[{objectQualifier}Topic] where [{databaseSchema}].[{objectQualifier}Topic].ForumID = @ForumID and [{databaseSchema}].[{objectQualifier}WatchTopic].TopicID = [{databaseSchema}].[{objectQualifier}Topic].TopicID
    delete from [{databaseSchema}].[{objectQualifier}Active] from [{databaseSchema}].[{objectQualifier}Topic] where [{databaseSchema}].[{objectQualifier}Topic].ForumID = @ForumID and [{databaseSchema}].[{objectQualifier}Active].TopicID = [{databaseSchema}].[{objectQualifier}Topic].TopicID
    delete from [{databaseSchema}].[{objectQualifier}NntpTopic] from [{databaseSchema}].[{objectQualifier}NntpForum] where [{databaseSchema}].[{objectQualifier}NntpForum].ForumID = @ForumID and [{databaseSchema}].[{objectQualifier}NntpTopic].NntpForumID = [{databaseSchema}].[{objectQualifier}NntpForum].NntpForumID
    delete from [{databaseSchema}].[{objectQualifier}NntpForum] where ForumID=@ForumID	
    delete from [{databaseSchema}].[{objectQualifier}WatchForum] where ForumID = @ForumID
    delete from [{databaseSchema}].[{objectQualifier}ForumReadTracking] where ForumID = @ForumID

    -- BAI CHANGED 02.02.2004
    -- Delete topics, messages and attachments

    declare @tmpTopicID int;
    declare topic_cursor cursor for
        select TopicID from [{databaseSchema}].[{objectQualifier}topic]
        where ForumID = @ForumID
        order by TopicID desc
    
    open topic_cursor
    
    fetch next from topic_cursor
    into @tmpTopicID
    
    -- Check @@FETCH_STATUS to see if there are any more rows to fetch.
    while @@FETCH_STATUS = 0
    begin
        exec [{databaseSchema}].[{objectQualifier}topic_delete] @tmpTopicID,null,0,1;
    
       -- This is executed as long as the previous fetch succeeds.
        fetch next from topic_cursor
        into @tmpTopicID
    end
    
    close topic_cursor
    deallocate topic_cursor

    -- TopicDelete finished
    -- END BAI CHANGED 02.02.2004

    delete from [{databaseSchema}].[{objectQualifier}ForumAccess] where ForumID = @ForumID
    --ABOT CHANGED
    --Delete UserForums Too 
    delete from [{databaseSchema}].[{objectQualifier}UserForum] where ForumID = @ForumID
    --END ABOT CHANGED 09.04.2004
 
    declare  @old_categoryid int, @old_left_key int, @old_right_key int, @old_level int, @old_parentid int;
	select @old_categoryid = categoryid, @old_left_key = left_key, @old_right_key = right_key, @old_level = [level], @old_parentid = parentid
	from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumID
-- rebuild tree
	if (@RebuildTree = 1)
	begin	
	delete from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumID	
	if @MoveChildren = 1 
	begin
	-- select @RebuildTree = 1;
	-- move children 1 level higher before deleting a forum
	  execute  [{databaseSchema}].[{objectQualifier}forum_after_del2_func] @old_categoryid, @old_left_key, @old_right_key, @old_level, @old_parentid
	end	
	  execute  [{databaseSchema}].[{objectQualifier}forum_after_del_func] @old_categoryid, @old_left_key, @old_right_key, @old_level, @old_parentid
	--  execute [{databaseSchema}].[{objectQualifier}forum_ns_recreate];	
	end
	else
	  delete from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumID	
	end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_byuserlist](@BoardID INT,@ForumID INT, @UserID INT, @IsUserForum bit) 
as
BEGIN
        IF @ForumID = 0  SET @ForumID = NULL; 
        IF @ForumID IS NULL 
                      SELECT a.* FROM [{databaseSchema}].[{objectQualifier}Forum] a 
                                  JOIN [{databaseSchema}].[{objectQualifier}Category] b 
                                     ON b.CategoryID=a.CategoryID                                  
                                        WHERE b.BoardID=@BoardID and a.IsUserForum = @IsUserForum
                                        and (@UserID IS NULL OR a.CreatedByUserID = @UserID)
                                          ORDER BY a.SortOrder;
        ELSE
        SELECT a.* FROM [{databaseSchema}].[{objectQualifier}Forum] a 
                   JOIN [{databaseSchema}].[{objectQualifier}Category] b 
                    ON b.CategoryID=a.CategoryID 
                     WHERE b.BoardID=@BoardID 
                      AND a.ForumID = @ForumID;
       
END
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_list](@BoardID int,@ForumID int=null, @UserID int, @IsUserForum bit) as
begin
    if @ForumID = 0 set @ForumID = null
    if @ForumID is null
        select a.* from [{databaseSchema}].[{objectQualifier}Forum] a join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID order by a.SortOrder
    else
        select a.* from [{databaseSchema}].[{objectQualifier}Forum] a join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID and a.ForumID = @ForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_maxid](@BoardID int) as
begin	
    select top 1 a.ForumID from [{databaseSchema}].[{objectQualifier}Forum] a join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID order by a.ForumID desc	
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}forum_listall] (@BoardID int,@UserID int,@root int = 0, @ReturnAll bit) as
begin
    if @root = 0
begin
      select
        b.CategoryID,
        Category = b.Name,
        a.ForumID,
        Forum = a.Name,
        Indent = 0,
        a.ParentID,
        a.PollGroupID,
        IsNull(SIGN(a.Flags & 2),0) as IsHidden,
		a.CanHavePersForums,
        CONVERT(int,c.ReadAccess) as ReadAccess
    from
        [{databaseSchema}].[{objectQualifier}Forum] a
        join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] c on c.ForumID=a.ForumID
    where
        c.UserID=@UserID and
        b.BoardID=@BoardID and
     (@ReturnAll = 1 OR CONVERT(int,c.ReadAccess) > 0)
    order by
        b.SortOrder,
        a.SortOrder,
        b.CategoryID,
        a.ForumID
end
else if  @root > 0
begin
    select
        b.CategoryID,
        Category = b.Name,
        a.ForumID,
        Forum = a.Name,
        Indent = 0,
        a.ParentID,
        a.PollGroupID,
        IsNull(SIGN(a.Flags & 2),0) as IsHidden,
		a.CanHavePersForums,
        CONVERT(int,c.ReadAccess) as ReadAccess
    from
        [{databaseSchema}].[{objectQualifier}Forum] a
        join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] c on c.ForumID=a.ForumID
    where
        c.UserID=@UserID and
        b.BoardID=@BoardID and
        (@ReturnAll = 1 OR CONVERT(int,c.ReadAccess) > 0) and
        a.ForumID = @root

    order by
        b.SortOrder,
        a.SortOrder,
        b.CategoryID,
        a.ForumID
end
else
begin
    select
        b.CategoryID,
        Category = b.Name,
        a.ForumID,
        Forum = a.Name,
        Indent = 0,
        a.ParentID,
        a.PollGroupID,
        IsNull(SIGN(a.Flags & 2),0) as IsHidden,
		a.CanHavePersForums,		
        CONVERT(int,c.ReadAccess) as ReadAccess
    from
        [{databaseSchema}].[{objectQualifier}Forum] a
        join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] c on c.ForumID=a.ForumID
    where
        c.UserID=@UserID and
        b.BoardID=@BoardID and
        (@ReturnAll = 1 OR CONVERT(int,c.ReadAccess) > 0) and
        b.CategoryID = -@root

    order by
        b.SortOrder,
        a.SortOrder,
        b.CategoryID,
        a.ForumID
end
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_listall_fromcat](@BoardID int,@CategoryID int, @AllowUserForumsOnly bit) AS
BEGIN
        SELECT     b.CategoryID, b.Name AS Category, a.ForumID, a.Name AS Forum, a.ParentID, a.PollGroupID,
           a.CanHavePersForums 
    FROM         [{databaseSchema}].[{objectQualifier}Forum] a INNER JOIN
                          [{databaseSchema}].[{objectQualifier}Category] b ON b.CategoryID = a.CategoryID
        WHERE
            b.CategoryID=@CategoryID and
            b.BoardID=@BoardID
        ORDER BY
            b.SortOrder,
            a.SortOrder
END
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_listallmymoderated](@BoardID int,@UserID int) as
begin
        select
        b.CategoryID,
        Category = b.Name,
        a.ForumID,
        Forum = a.Name,
        x.Indent
    from
        (select
            b.ForumID,
            Indent = 0
        from
            [{databaseSchema}].[{objectQualifier}Category] a
            join [{databaseSchema}].[{objectQualifier}Forum] b on b.CategoryID=a.CategoryID
        where
            a.BoardID=@BoardID and
            b.ParentID is null
    
        union
    
        select
            c.ForumID,
            Indent = 1
        from
            [{databaseSchema}].[{objectQualifier}Category] a
            join [{databaseSchema}].[{objectQualifier}Forum] b on b.CategoryID=a.CategoryID
            join [{databaseSchema}].[{objectQualifier}Forum] c on c.ParentID=b.ForumID
        where
            a.BoardID=@BoardID and
            b.ParentID is null
    
        union
    
        select
            d.ForumID,
            Indent = 2
        from
            [{databaseSchema}].[{objectQualifier}Category] a
            join [{databaseSchema}].[{objectQualifier}Forum] b on b.CategoryID=a.CategoryID
            join [{databaseSchema}].[{objectQualifier}Forum] c on c.ParentID=b.ForumID
            join [{databaseSchema}].[{objectQualifier}Forum] d on d.ParentID=c.ForumID
        where
            a.BoardID=@BoardID and
            b.ParentID is null
        ) as x
        join [{databaseSchema}].[{objectQualifier}Forum] a on a.ForumID=x.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] c on c.ForumID=a.ForumID
    where
        c.UserID=@UserID and
        b.BoardID=@BoardID and
        CONVERT(int,c.ModeratorAccess)>0
    order by
        b.SortOrder,
        a.SortOrder
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_listpath](@ForumID int) as
begin
        -- supports up to 4 levels of nested forums
    select
        a.ForumID,
        a.Name
    from
        (select
            a.ForumID,
            Indent = 0
        from
            [{databaseSchema}].[{objectQualifier}Forum] a
        where
            a.ForumID=@ForumID

        union

        select
            b.ForumID,
            Indent = 1
        from
            [{databaseSchema}].[{objectQualifier}Forum] a
            join [{databaseSchema}].[{objectQualifier}Forum] b on b.ForumID=a.ParentID
        where
            a.ForumID=@ForumID

        union

        select
            c.ForumID,
            Indent = 2
        from
            [{databaseSchema}].[{objectQualifier}Forum] a
            join [{databaseSchema}].[{objectQualifier}Forum] b on b.ForumID=a.ParentID
            join [{databaseSchema}].[{objectQualifier}Forum] c on c.ForumID=b.ParentID
        where
            a.ForumID=@ForumID

        union 

        select
            d.ForumID,
            Indent = 3
        from
            [{databaseSchema}].[{objectQualifier}Forum] a
            join [{databaseSchema}].[{objectQualifier}Forum] b on b.ForumID=a.ParentID
            join [{databaseSchema}].[{objectQualifier}Forum] c on c.ForumID=b.ParentID
            join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ParentID
        where
            a.ForumID=@ForumID
        ) as x	
        join [{databaseSchema}].[{objectQualifier}Forum] a on a.ForumID=x.ForumID
    order by
        x.Indent desc
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_listread](@BoardID int,@UserID int,@CategoryID int=null,@ParentID int=null, @StyledNicks bit=null,	@FindLastRead bit = 0, @ShowCommonForums bit = 1, @ShowPersonalForums bit = 1, @ForumCreatedByUserId int, @UTCTIMESTAMP datetime) as
begin
declare @tbl1 table
( ForumID int, ParentID int)
declare @tbl table
( ForumID int, ParentID int)
-- get parent forums list first
insert into @tbl1(ForumID,ParentID)
select 	
        b.ForumID,
        b.ParentID		
    from 
        [{databaseSchema}].[{objectQualifier}Category] a with(nolock) 
        join [{databaseSchema}].[{objectQualifier}Forum] b  with(nolock) on b.CategoryID=a.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x  with(nolock) on x.ForumID=b.ForumID	
    where 
        a.BoardID = @BoardID and
        ((b.Flags & 2)=0 or x.ReadAccess<>0) and
        (@CategoryID is null or a.CategoryID=@CategoryID) and
        ((@ParentID is null and b.ParentID is null) or b.ParentID=@ParentID) and
        x.UserID = @UserID
            order by
        a.SortOrder,
        b.SortOrder
            
-- child forums
insert into @tbl(ForumID,ParentID)
select 	
        b.ForumID,
        b.ParentID		
    from 
        [{databaseSchema}].[{objectQualifier}Category] a  with(nolock)
        join [{databaseSchema}].[{objectQualifier}Forum] b  with(nolock) on b.CategoryID=a.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x  with(nolock) on x.ForumID=b.ForumID		
    where 
        a.BoardID = @BoardID and
        ((b.Flags & 2)=0 or x.ReadAccess<>0) and
        (@CategoryID is null or a.CategoryID=@CategoryID) and
        (b.ParentID IN (SELECT ForumID FROM @tbl1)) and
        x.UserID = @UserID
        order by
        a.SortOrder,
        b.SortOrder

 insert into @tbl(ForumID,ParentID)
 select * FROM @tbl1
 -- more childrens can be added to display as a tree

        select 
        a.CategoryID, 
        Category		= a.Name, 
        ForumID			= b.ForumID,
        Forum			= b.Name, 
        b.[Description],
        b.ImageUrl,
        b.Styles,
        b.ParentID,
        b.PollGroupID,
        Topics			= [{databaseSchema}].[{objectQualifier}forum_topics](b.ForumID),
        Posts			= [{databaseSchema}].[{objectQualifier}forum_posts](b.ForumID),		
        LastPosted		= t.LastPosted,
        LastMessageID	= t.LastMessageID,
        LastMessageFlags = t.LastMessageFlags,
        LastUserID		= t.LastUserID,
        LastUser		= IsNull(t.LastUserName,(select x.[Name] from [{databaseSchema}].[{objectQualifier}User] x with(nolock) where x.UserID=t.LastUserID)),
        LastUserDisplayName	= IsNull(t.LastUserDisplayName,(select x.[DisplayName] from [{databaseSchema}].[{objectQualifier}User] x with(nolock) where x.UserID=t.LastUserID)),
        LastTopicID		= t.TopicID,
        TopicMovedID    = t.TopicMovedID,
        LastTopicName	= t.Topic,
        LastTopicStatus = t.Status,
        LastTopicStyles = t.Styles,
        b.Flags,
        Viewing			= (select count(1) from [{databaseSchema}].[{objectQualifier}Active] x with(nolock) JOIN [{databaseSchema}].[{objectQualifier}User] usr with(nolock) ON x.UserID = usr.UserID where x.ForumID=b.ForumID AND usr.IsActiveExcluded = 0),
        b.RemoteURL,		
        ReadAccess = CONVERT(int,x.ReadAccess),
        Style = case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = t.LastUserID)
            else ''	 end,
        LastForumAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x with(nolock) WHERE x.ForumID=b.ForumID AND x.UserID = @UserID)
             else ''	 end,
        LastTopicAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y with(nolock) WHERE y.TopicID=t.TopicID AND y.UserID = @UserID)
             else ''	 end 					
    from 
        [{databaseSchema}].[{objectQualifier}Category] a with(nolock)
        join [{databaseSchema}].[{objectQualifier}Forum] b with(nolock) on b.CategoryID=a.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on (x.ForumID=b.ForumID and  x.UserID = @UserID)
        left outer join [{databaseSchema}].[{objectQualifier}Topic] t with(nolock) ON t.TopicID = [{databaseSchema}].[{objectQualifier}forum_lasttopic](b.ForumID,@UserID,b.LastTopicID,b.LastPosted)
    where 		
        (@CategoryID is null or a.CategoryID=@CategoryID) and        		
        (b.ForumID IN (SELECT ForumID FROM @tbl) )
    order by
        a.SortOrder,
        b.SortOrder
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_ns_listread](@BoardID int,@UserID int,@CategoryID int=null,@ParentID int=null, @StyledNicks bit=null,	@FindLastRead bit = 0, @ShowCommonForums bit = 1, @ShowPersonalForums bit = 1, @ForumCreatedByUserId int, @UTCTIMESTAMP datetime) as
begin
 DECLARE @lvl INT = 0; 
 DECLARE @rk INT; 
 DECLARE @lk INT; 

 -- these are a forum subforums from a forum topics
 IF (@ParentID IS NOT NULL) 
 begin
 SELECT @lvl = [level], @lk = left_key + 1, @rk = right_key - 1 
 from [{databaseSchema}].[{objectQualifier}Forum]  with(nolock)
 where ForumID = @ParentID;
 end
 -- thiese are forums from a category
 IF (@ParentID IS NULL AND @CategoryID > 0) 
 begin
 SELECT @lvl = 0, @lk = min(left_key), @rk = max(right_key) 
 from [{databaseSchema}].[{objectQualifier}Forum] with(nolock)
 where CategoryID = @CategoryID;
 end
 -- this is a board view
 IF (@ParentID IS NULL AND @CategoryID IS NULL)
 begin 
  SELECT @lvl = 0, @lk = min(f.left_key), @rk = max(f.right_key) 
  from [{databaseSchema}].[{objectQualifier}Forum] f with(nolock)
  join [{databaseSchema}].[{objectQualifier}Category] c with(nolock)
  on c.CategoryID = f.CategoryID
  where c.BoardID = @BoardID;
 end
 select @lvl = @lvl - 1;

        select 
        a.CategoryID, 
        Category		= a.Name, 
        ForumID			= b.ForumID,
        Forum			= b.Name, 
        b.[Description],
        b.ImageUrl,
        b.Styles,
        b.ParentID,
        b.PollGroupID,
        Topics			= [{databaseSchema}].[{objectQualifier}forum_ns_topics](b.left_key,b.right_key,a.CategoryID),
        Posts			= [{databaseSchema}].[{objectQualifier}forum_ns_posts](b.left_key,b.right_key, a.CategoryID),		
        LastPosted		= t.LastPosted,
        LastMessageID	= t.LastMessageID,
        LastMessageFlags = t.LastMessageFlags,
        LastUserID		= t.LastUserID,
        LastUser		= IsNull(t.LastUserName,(select x.[Name] from [{databaseSchema}].[{objectQualifier}User] x with(nolock) where x.UserID=t.LastUserID)),
        LastUserDisplayName	= IsNull(t.LastUserDisplayName,(select x.[DisplayName] from [{databaseSchema}].[{objectQualifier}User] x with(nolock) where x.UserID=t.LastUserID)),
        LastTopicID		= t.TopicID,
        TopicMovedID    = t.TopicMovedID,
        LastTopicName	= t.Topic,
        LastTopicStatus = t.Status,
        LastTopicStyles = t.Styles,
        b.Flags,
        Viewing			= (select count(1) from [{databaseSchema}].[{objectQualifier}Active] x with(nolock) JOIN [{databaseSchema}].[{objectQualifier}User] usr with(nolock) ON x.UserID = usr.UserID where x.ForumID=b.ForumID AND usr.IsActiveExcluded = 0),
        b.RemoteURL,		
        ReadAccess = CONVERT(int,x.ReadAccess),
        Style = case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = t.LastUserID)
            else ''	 end,
        LastForumAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x with(nolock) WHERE x.ForumID=b.ForumID AND x.UserID = @UserID)
             else ''	 end,
        LastTopicAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y with(nolock) WHERE y.TopicID=t.TopicID AND y.UserID = @UserID)
             else ''	 end 					
    from 
        [{databaseSchema}].[{objectQualifier}Category] a with(nolock)
        join [{databaseSchema}].[{objectQualifier}Forum] b with(nolock) on b.CategoryID=a.CategoryID
		join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on (x.ForumID=b.ForumID and  x.UserID = @UserID)
        left outer join [{databaseSchema}].[{objectQualifier}Topic] t with(nolock) ON t.TopicID = [{databaseSchema}].[{objectQualifier}forum_ns_lasttopic](b.left_key,b.right_key,a.CategoryID,@UserID,b.LastTopicID,b.LastPosted)
    where 		
        (@CategoryID is null or a.CategoryID = @CategoryID) and
	    (b.[level] >= @lvl) and
		((b.Flags & 2)=0 or x.ReadAccess <>0 ) and
		b.left_key >= @lk and b.right_key <= @rk      	
    order by
	   a.SortOrder,
	   b.left_key;  
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_listSubForums](@ForumID int) as
begin
        select Sum(1) from [{databaseSchema}].[{objectQualifier}Forum] where ParentID = @ForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_listtopics](@ForumID int) as
begin
        select * from [{databaseSchema}].[{objectQualifier}Topic]
    Where ForumID = @ForumID
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_moderatelist](@BoardID int,@UserID int,@IsUserForum bit) AS
BEGIN
    
SELECT
        b.*,
        MessageCount  = 
        (SELECT     count([{databaseSchema}].[{objectQualifier}Message].MessageID)
        FROM         [{databaseSchema}].[{objectQualifier}Message] INNER JOIN
                              [{databaseSchema}].[{objectQualifier}Topic] ON [{databaseSchema}].[{objectQualifier}Message].TopicID = [{databaseSchema}].[{objectQualifier}Topic].TopicID
        WHERE ([{databaseSchema}].[{objectQualifier}Message].IsApproved=0) and ([{databaseSchema}].[{objectQualifier}Message].IsDeleted=0) and ([{databaseSchema}].[{objectQualifier}Topic].IsDeleted  = 0) AND ([{databaseSchema}].[{objectQualifier}Topic].ForumID=b.ForumID)),

        ReportedCount	= 
        (SELECT     count([{databaseSchema}].[{objectQualifier}Message].MessageID)
        FROM         [{databaseSchema}].[{objectQualifier}Message] INNER JOIN
                              [{databaseSchema}].[{objectQualifier}Topic] ON [{databaseSchema}].[{objectQualifier}Message].TopicID = [{databaseSchema}].[{objectQualifier}Topic].TopicID
        WHERE (([{databaseSchema}].[{objectQualifier}Message].Flags & 128)=128) and ([{databaseSchema}].[{objectQualifier}Message].IsDeleted=0) and ([{databaseSchema}].[{objectQualifier}Topic].IsDeleted = 0) AND ([{databaseSchema}].[{objectQualifier}Topic].ForumID=b.ForumID))
        FROM
        [{databaseSchema}].[{objectQualifier}Category] a

    JOIN [{databaseSchema}].[{objectQualifier}Forum] b ON b.CategoryID=a.CategoryID
    JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] c ON c.ForumID=b.ForumID

    WHERE
        a.BoardID=@BoardID AND
		b.IsUserForum = @IsUserForum AND
        CONVERT(int,c.ModeratorAccess)>0 AND
        c.UserID=@UserID
    ORDER BY
        a.SortOrder,
        b.SortOrder
END
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_moderators] (@StyledNicks bit) as
BEGIN
        select
        ForumID = a.ForumID, 
        ForumName = f.Name,
        ModeratorID = a.GroupID, 
        ModeratorName = b.Name,	
        ModeratorEmail = '',
        ModeratorAvatar = '',
        ModeratorAvatarImage = CAST(0 as bit),
        ModeratorDisplayName = b.Name,
        Style = case(@StyledNicks)
            when 1 then b.Style  
            else ''	 end,						
        IsGroup=1
    from
        [{databaseSchema}].[{objectQualifier}Forum] f WITH(NOLOCK) 
        INNER JOIN [{databaseSchema}].[{objectQualifier}ForumAccess] a WITH(NOLOCK)
        ON a.ForumID = f.ForumID
        INNER JOIN [{databaseSchema}].[{objectQualifier}Group] b WITH(NOLOCK) ON b.GroupID = a.GroupID
        INNER JOIN [{databaseSchema}].[{objectQualifier}AccessMask] c WITH(NOLOCK) ON c.AccessMaskID = a.AccessMaskID
    where
        (b.Flags & 1)=0 and
        (c.Flags & 64)<>0
    union all
    select 
        ForumID = access.ForumID,
        ForumName = f.Name,
        ModeratorID = usr.UserID, 
        ModeratorName = usr.Name,
        ModeratorEmail = usr.Email,
        ModeratorAvatar = ISNULL(usr.Avatar, ''),
        ModeratorAvatarImage = CAST((select count(1) from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=usr.UserID and AvatarImage is not null)as bit),
        ModeratorDisplayName = usr.DisplayName,
        Style = case(@StyledNicks)
            when 1 then  usr.UserStyle
            else ''	 end,						
        IsGroup=0
    from
        [{databaseSchema}].[{objectQualifier}User] usr WITH(NOLOCK)
        INNER JOIN (
            select
                UserID				= a.UserID,
                ForumID				= x.ForumID,
                ModeratorAccess		= MAX(ModeratorAccess)						
            from
                [{databaseSchema}].[{objectQualifier}vaccessfull] as x WITH(NOLOCK)		       				
                INNER JOIN [{databaseSchema}].[{objectQualifier}UserGroup] a WITH(NOLOCK) on a.UserID=x.UserID
                INNER JOIN [{databaseSchema}].[{objectQualifier}Group] b WITH(NOLOCK) on b.GroupID=a.GroupID
            WHERE 
                ModeratorAccess <> 0 AND x.AdminGroup = 0
            GROUP BY a.UserId, x.ForumID
        ) access ON usr.UserID = access.UserID
        JOIN    [{databaseSchema}].[{objectQualifier}Forum] f WITH(NOLOCK) 
        ON f.ForumID = access.ForumID
                   
        JOIN [{databaseSchema}].[{objectQualifier}Rank] r
        ON r.RankID = usr.RankID
    where
        access.ModeratorAccess<>0
    order by
        IsGroup desc,
        ModeratorName asc
END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}forum_save](
    @ForumID 		int=null,
    @CategoryID		int,
    @ParentID		int=null,
    @Name			nvarchar(50),
    @Description	nvarchar(255),
    @SortOrder		int,
    @Locked			bit,
    @Hidden			bit,
    @IsTest			bit,
    @Moderated		bit,
    @RemoteURL		nvarchar(100)=null,
    @ThemeURL		nvarchar(100)=null,
    @ImageURL       nvarchar(128)=null,
    @Styles         nvarchar(255)=null,
    @AccessMaskID	int = null,
    @UserID         int = null,
    @IsUserForum    bit,
	@CanHavePersForums bit,
	@AdjacentForumID int,
	@AdjacentForumMode int,
    @UTCTIMESTAMP   datetime
) as
begin			
    declare @BoardID	int
    declare @Flags		int	
	declare @NewForum bit = 0
    declare @UserName  nvarchar(255), @UserDisplayName  nvarchar(255)

	declare @OldParentID	int
	declare @OldCategoryID	int
	declare @OldSortOrder		int
	declare @OldLeftKey	int
	declare @OldRightKey	int
	declare @OldLevel	int
	declare @OldNid	int
	declare @cntr int = 0;
	declare @tmp int;
	declare @afterset bit;

	    -- re-order forums removing gaps, create sortorder gap for a forum
    if (@AdjacentForumID is not null)
	begin
	-- over
		if (@AdjacentForumMode = 3) 
		begin
		select @SortOrder = 0;
		select @cntr = @cntr + 1;
		end
	declare c cursor for
		select f.ForumID from [{databaseSchema}].[{objectQualifier}Forum] f
		join [{databaseSchema}].[{objectQualifier}Category] c
		on c.CategoryID = f.CategoryID
		where c.CategoryID = @CategoryID and (@ParentID is null or f.ParentID = @ParentID)   
		order by c.SortOrder, f.SortOrder, f.ForumID
		
		open c
		
		fetch next from c into @tmp
		while @@FETCH_STATUS = 0
		begin
		if (@AdjacentForumID = @tmp) 
		begin
		-- before
		if (@AdjacentForumMode = 1) 
		begin
		select @SortOrder = @cntr;
		select @cntr = @cntr + 1;
		end
		-- after
		if (@AdjacentForumMode = 2) 
		begin
		select @SortOrder = @cntr + 1;	
		select @afterset = 1;
		end
		end 
				
		-- this is after gap
		if (@SortOrder = @cntr)
		begin
		select @cntr = @cntr + 1;
		select @afterset = 0;
		end

		update	[{databaseSchema}].[{objectQualifier}Forum]
		set SortOrder = @cntr where ForumID = @tmp;
		select @cntr = @cntr + 1;
		
			fetch next from c into @tmp
		end
		close c
		deallocate c
   end
  

    set @Flags = 0
    if @Locked<>0 set @Flags = @Flags | 1
    if @Hidden<>0 set @Flags = @Flags | 2
    if @IsTest<>0 set @Flags = @Flags | 4
    if @Moderated<>0 set @Flags = @Flags | 8
    
    if @ForumID = 0 set @ForumID = null
    if @ParentID = 0 set @ParentID = null
    	SELECT @BoardID = BoardID FROM [{databaseSchema}].[{objectQualifier}Category] WHERE CategoryID = @CategoryID
    if @ForumID is not null begin
	select @OldParentID = f.ParentID, @OldCategoryID = f.CategoryID, 
   @OldSortOrder = f.SortOrder, @OldLeftKey = f.left_key, 
   @OldRightKey = f.right_key,@OldLevel = f.[level]
   from [{databaseSchema}].[{objectQualifier}Forum]  f 
   where f.ForumID=@ForumID;

	-- rebuild tree
	/* if (@CategoryID != @OldCategoryID OR @SortOrder != @OldSortOrder OR @OldParentID != @ParentID)
	begin
	declare @newlk int;
	 if (@AdjacentForumID is not null)
   begin
 
    if (@AdjacentForumMode = 1)  
	select @newlk = left_key from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @AdjacentForumID;
	if (@AdjacentForumMode = 2)  
	select @newlk = right_key+1  from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @AdjacentForumID;	
	declare @o_lk int, @o_rk int, @o_lv int;

    execute	[{databaseSchema}].[{objectQualifier}forum_before_update_func] 
    @ForumID, @CategoryID, @newlk, null, null, @ParentID,0,0, 
    @ForumID, @OldCategoryID, @OldLeftKey, @OldRightKey, @OldLevel, @OldParentID,
	@o_lk output,@o_rk output,@o_lv output; 

	end 
	end */
	
        update [{databaseSchema}].[{objectQualifier}Forum] set 
            ParentID=@ParentID,
            Name=@Name,
            [Description]=@Description,
            SortOrder=(CASE WHEN @AdjacentForumMode = -1  THEN SortOrder ELSE @SortOrder END),
            CategoryID=@CategoryID,
            RemoteURL = @RemoteURL,
            ThemeURL = @ThemeURL,
            ImageURL = @ImageURL,
            Styles = @Styles,
            Flags = @Flags,
            IsUserForum = @IsUserForum,
            CanHavePersForums = @CanHavePersForums
        where ForumID=@ForumID
		if (@OldCategoryID <> @CategoryID)
		execute [{databaseSchema}].[{objectQualifier}forum_ns_recreate] null,@OldCategoryID;

		execute [{databaseSchema}].[{objectQualifier}forum_ns_recreate] null,@CategoryID;

    end
    else begin 

        insert into [{databaseSchema}].[{objectQualifier}Forum](ParentID,Name,Description,SortOrder,CategoryID,NumTopics,NumPosts,RemoteURL,ThemeURL,Flags,ImageURL,Styles,IsUserForum, CanHavePersForums, CreatedByUserID,CreatedByUserName, CreatedByUserDisplayName, CreatedDate)
        values(@ParentID,@Name,@Description,@SortOrder,@CategoryID,0,0,@RemoteURL,@ThemeURL,@Flags,@ImageURL,@Styles,@IsUserForum,@CanHavePersForums,@UserID,(SELECT TOP 1 Name FROM [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID),(SELECT TOP 1 DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID),@UTCTIMESTAMP)
        select @ForumID = SCOPE_IDENTITY()
		-- rebuild tree 
	   select @OldParentID = f.ParentID, @OldCategoryID = f.CategoryID, 
              @OldSortOrder = f.SortOrder, @OldLeftKey = f.left_key, 
              @OldRightKey = f.right_key,@OldLevel = f.[level]
        from [{databaseSchema}].[{objectQualifier}Forum]  f 
         where f.ForumID=@ForumID;

   if (@AdjacentForumID is not null)
   begin
   declare @nlk int;
    if (@AdjacentForumMode = 1)  
	select @nlk = left_key from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @AdjacentForumID;
	if (@AdjacentForumMode = 2)  
	select @nlk = right_key + 1  from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @AdjacentForumID;	
    
	execute [{databaseSchema}].[{objectQualifier}forum_before_insert_func] 
	@OldCategoryID, @ForumID,@OldCategoryID, @nlk, null, null, @OldParentID, @SortOrder,0, 0

   end	
		-- tree rebuiled
        insert into [{databaseSchema}].[{objectQualifier}ForumAccess](GroupID,ForumID,AccessMaskID) 
        select GroupID,@ForumID,@AccessMaskID
        from [{databaseSchema}].[{objectQualifier}Group]
        where BoardID IN (select BoardID from [{databaseSchema}].[{objectQualifier}Category] where CategoryID=@CategoryID) 

		
    end 

     
    IF @UserID IS NOT NULL
    BEGIN	
    SELECT TOP 1 @UserName = Name,  @UserDisplayName = DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
    END
    -- guests should not create forums
    ELSE
    BEGIN     
    SELECT TOP 1 @UserName = Name,  @UserDisplayName = DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where BoardID = @BoardID and (Flags & 4) = 4  ORDER BY Joined DESC
    END
    if exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}ForumHistory] where ForumID = @ForumID and ChangedDate = @UTCTIMESTAMP)
    begin
    update [{databaseSchema}].[{objectQualifier}ForumHistory] set 
           ChangedUserID = @UserID,	
           ChangedUserName = @UserName,
           ChangedDisplayName = @UserDisplayName
        where ForumID = @ForumID and ChangedDate = @UTCTIMESTAMP
    end
    else
    begin
    INSERT INTO [{databaseSchema}].[{objectQualifier}ForumHistory](ForumID,ChangedUserID,ChangedUserName,ChangedDisplayName,ChangedDate)
    VALUES (@ForumID,@UserID,@UserName,@UserDisplayName,@UTCTIMESTAMP)
    end
	
	-- if (@CategoryID != @OldCategoryID OR @SortOrder != @OldSortOrder OR @OldParentID != @ParentID)
	-- begin
	-- execute [{databaseSchema}].[{objectQualifier}forum_ns_recreate];
	-- end
    select ForumID = @ForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_updatelastpost](@ForumID int) as
begin
        update [{databaseSchema}].[{objectQualifier}Forum] set
        LastPosted = (select top 1 y.Posted from [{databaseSchema}].[{objectQualifier}Topic] x join [{databaseSchema}].[{objectQualifier}Message] y on y.TopicID=x.TopicID where x.ForumID = @ForumID and (y.Flags & 24)=16 and x.IsDeleted = 0 order by y.Posted desc),
        LastTopicID = (select top 1 y.TopicID from [{databaseSchema}].[{objectQualifier}Topic] x join [{databaseSchema}].[{objectQualifier}Message] y on y.TopicID=x.TopicID where x.ForumID = @ForumID and (y.Flags & 24)=16 and x.IsDeleted = 0order by y.Posted desc),
        LastMessageID = (select top 1 y.MessageID from [{databaseSchema}].[{objectQualifier}Topic] x join [{databaseSchema}].[{objectQualifier}Message] y on y.TopicID=x.TopicID where x.ForumID = @ForumID and (y.Flags & 24)=16 and x.IsDeleted = 0order by y.Posted desc),
        LastUserID = (select top 1 y.UserID from [{databaseSchema}].[{objectQualifier}Topic] x join [{databaseSchema}].[{objectQualifier}Message] y on y.TopicID=x.TopicID where x.ForumID = @ForumID and (y.Flags & 24)=16 and x.IsDeleted = 0order by y.Posted desc),
        LastUserName = (select top 1 y.UserName from [{databaseSchema}].[{objectQualifier}Topic] x join [{databaseSchema}].[{objectQualifier}Message] y on y.TopicID=x.TopicID where x.ForumID = @ForumID and (y.Flags & 24)=16 and x.IsDeleted = 0order by y.Posted desc),
        LastUserDisplayName = (select top 1 y.UserDisplayName from [{databaseSchema}].[{objectQualifier}Topic] x join [{databaseSchema}].[{objectQualifier}Message] y on y.TopicID=x.TopicID where x.ForumID = @ForumID and (y.Flags & 24)=16 and x.IsDeleted = 0 order by y.Posted desc)
    where ForumID = @ForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_updatestats](@ForumID int) as
begin
        update [{databaseSchema}].[{objectQualifier}Forum] set 
        NumPosts = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x join [{databaseSchema}].[{objectQualifier}Topic] y on y.TopicID=x.TopicID where y.ForumID = @ForumID and x.IsApproved = 1 and x.IsDeleted = 0 and y.IsDeleted = 0 ),
        NumTopics = (select count(distinct x.TopicID) from [{databaseSchema}].[{objectQualifier}Topic] x join [{databaseSchema}].[{objectQualifier}Message] y on y.TopicID=x.TopicID where x.ForumID = @ForumID and y.IsApproved = 1 and y.IsDeleted = 0 and x.IsDeleted = 0)
    where ForumID=@ForumID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}forumaccess_group](@GroupID int, @UserID int, @IncludeUserForums bit) as
begin
        select 
        a.*,
        ForumName = b.Name,
        CategoryName = c.Name ,
        CategoryID = b.CategoryID,
        ParentID = b.ParentID,
        BoardName = brd.Name 
    from 
        [{databaseSchema}].[{objectQualifier}ForumAccess] a
        inner join [{databaseSchema}].[{objectQualifier}Forum] b on b.ForumID=a.ForumID
        inner join [{databaseSchema}].[{objectQualifier}Category] c on c.CategoryID=b.CategoryID
        inner join [{databaseSchema}].[{objectQualifier}Board] brd on brd.BoardID=c.BoardID
    where 
        a.GroupID = @GroupID and (@IncludeUserForums <> 1 or (b.IsUserForum = 1 and b.CreatedByUserID = @UserID))
    order by
        brd.Name,
        c.SortOrder,
        b.SortOrder
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forumaccess_list](@ForumID int, @PersonalGroupUserID int, @IncludeUserGroups bit, @IncludeCommonGroups bit, @IncludeAdminGroups bit) as
begin   
        select 
        a.*,
        GroupName=b.Name 
    from 
        [{databaseSchema}].[{objectQualifier}ForumAccess] a 
        inner join [{databaseSchema}].[{objectQualifier}Group] b on b.GroupID=a.GroupID
    where 
        a.ForumID = @ForumID AND 
		b.IsUserGroup = (case when @IncludeUserGroups = 1 and @IncludeCommonGroups = 0 AND b.CreatedByUserID = @PersonalGroupUserID then 1
		else 0 end)
		AND 
		((@IncludeAdminGroups = 1 AND @IncludeCommonGroups = 1) OR b.IsHidden = @IncludeAdminGroups);	
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forumaccess_save](
    @ForumID			int,
    @GroupID			int,
    @AccessMaskID		int
) as
begin
        update [{databaseSchema}].[{objectQualifier}ForumAccess]
        set AccessMaskID=@AccessMaskID
    where 
        ForumID = @ForumID and 
        GroupID = @GroupID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}group_delete](@GroupID int) as
begin
    delete from [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] where GroupID = @GroupID
    delete from [{databaseSchema}].[{objectQualifier}ForumAccess] where GroupID = @GroupID
    delete from [{databaseSchema}].[{objectQualifier}UserGroup] where GroupID = @GroupID
    delete from [{databaseSchema}].[{objectQualifier}Group] where GroupID = @GroupID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}group_byuserlist](
        @BoardID INT,
        @GroupID INT,
        @UserID INT,
        @IsUserGroup bit)
as
begin
       IF @GroupID IS NULL 
        SELECT *
        FROM   [{databaseSchema}].[{objectQualifier}Group]
        WHERE  BoardID = @BoardID and CreatedByUserID = @UserID  order by SortOrder;
        ELSE
        SELECT TOP 1 *
        FROM   [{databaseSchema}].[{objectQualifier}Group]
        WHERE  BoardID = @BoardID and CreatedByUserID = @UserID 
        AND GroupID = @GroupID;       
end
GO

create procedure [{databaseSchema}].[{objectQualifier}group_list](@BoardID int,@GroupID int=null,@PageIndex int, @PageSize int) as
begin
declare @TotalRows int;
declare @FirstSelectRowNumber int;
declare @LastSelectRowNumber int;
   if @GroupID is null
   begin       
           set @PageIndex = @PageIndex + 1;
           set @FirstSelectRowNumber = 0;
           set @LastSelectRowNumber = 0;
           set @TotalRows = 0;
           
           select @TotalRows = count(1) from [{databaseSchema}].[{objectQualifier}Group] where BoardID=@BoardID;
           select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
           select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
           
           with GroupIds as
           (
             select ROW_NUMBER() over (order by SortOrder) as RowNum, GroupID
             from [{databaseSchema}].[{objectQualifier}Group] where BoardID=@BoardID
           )
           select
            a.*,
            @TotalRows as TotalRows
            from
            GroupIds c
            inner join  [{databaseSchema}].[{objectQualifier}Group] a	
            on c.GroupID = a.GroupID	
            where c.RowNum between (@FirstSelectRowNumber) and (@LastSelectRowNumber)
            order by c.RowNum asc;
  end       
    else
  begin
        select top 1 * from [{databaseSchema}].[{objectQualifier}Group] where BoardID=@BoardID and GroupID=@GroupID;
  end
end
GO

create procedure [{databaseSchema}].[{objectQualifier}group_eventlogaccesslist](@BoardID int = null) as
begin
        if @BoardID is null
        select g.*,b.Name as BoardName from [{databaseSchema}].[{objectQualifier}Group] g
        join [{databaseSchema}].[{objectQualifier}Board] b on b.BoardID = g.BoardID order by g.SortOrder 
    else
        select g.*,b.Name as BoardName from [{databaseSchema}].[{objectQualifier}Group] g
        join [{databaseSchema}].[{objectQualifier}Board] b on b.BoardID = g.BoardID where g.BoardID=@BoardID  order by g.SortOrder
end
GO

create procedure [{databaseSchema}].[{objectQualifier}group_member](@BoardID int,@UserID int) as
begin
        select 
        a.GroupID,
        a.Name,
		CONVERT(bit, SIGN((a.Flags & 16))) as IsHidden,
		a.IsUserGroup,
        a.Style, 
        Member = (select count(1) from [{databaseSchema}].[{objectQualifier}UserGroup] x where x.UserID=@UserID and x.GroupID=a.GroupID)
    from
        [{databaseSchema}].[{objectQualifier}Group] a
    where
        a.BoardID=@BoardID  
    order by
        a.Name
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}group_save](
    @GroupID		int,
    @BoardID		int,
    @Name			nvarchar(50),
    @IsAdmin		bit,
	@IsGuest		bit,
    @IsStart		bit,
    @IsModerator	bit,   
	@IsHidden       bit,
    @AccessMaskID	int=null,
    @PMLimit int=null,
    @Style nvarchar(255)=null,
    @SortOrder int,
    @Description nvarchar(128)=null,
    @UsrSigChars int=null,
    @UsrSigBBCodes	nvarchar(255)=null,
    @UsrSigHTMLTags nvarchar(255)=null,
    @UsrAlbums int=null,
    @UsrAlbumImages int=null,
    @UserID         int = null,
    @IsUserGroup    bit,
	@PersonalAccessMasksNumber int,
	@PersonalGroupsNumber int,
	@PersonalForumsNumber int,
    @UTCTIMESTAMP datetime 
) as
begin
    declare @Flags	int
    declare @UserName  nvarchar(255),  @UserDisplayName  nvarchar(255)
    set @Flags = 0
    if @IsAdmin<>0 set @Flags = @Flags | 1
    if @IsGuest<>0 set @Flags = @Flags | 2
    if @IsStart<>0 set @Flags = @Flags | 4
    if @IsModerator<>0 set @Flags = @Flags | 8

	IF @UserID IS NOT NULL
    BEGIN	
    SELECT TOP 1 @UserName = Name,  @UserDisplayName = DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
    END
    -- guests should not create forums
    ELSE
    BEGIN 
    SELECT TOP 1 @UserName = Name,  @UserDisplayName = DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where BoardID = @BoardID and (Flags & 4) = 4  ORDER BY Joined DESC
    END

    if @GroupID>0 begin
        update [{databaseSchema}].[{objectQualifier}Group] set
            Name = @Name,
            Flags = @Flags,
            PMLimit = @PMLimit,
            Style = @Style,
            SortOrder = @SortOrder,
            Description = @Description,
            UsrSigChars = @UsrSigChars,
            UsrSigBBCodes = @UsrSigBBCodes,
            UsrSigHTMLTags = @UsrSigHTMLTags,
            UsrAlbums = @UsrAlbums,
            UsrAlbumImages = @UsrAlbumImages,
            IsUserGroup = @IsUserGroup,
			IsHidden = @IsHidden,
			UsrPersonalMasks = @PersonalAccessMasksNumber,
			UsrPersonalGroups = @PersonalGroupsNumber,
			UsrPersonalForums = @PersonalForumsNumber
        where GroupID = @GroupID
    end
    else begin
        insert into [{databaseSchema}].[{objectQualifier}Group]
		(Name,BoardID,Flags,PMLimit,Style, SortOrder,Description,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages,IsUserGroup,IsHidden,UsrPersonalMasks,UsrPersonalGroups,UsrPersonalForums,
                        CreatedByUserID,
						CreatedByUserName,
                        CreatedByUserDisplayName,
                        CreatedDate)
        values
		(@Name,@BoardID,@Flags,@PMLimit,@Style,@SortOrder,@Description,@UsrSigChars,@UsrSigBBCodes,@UsrSigHTMLTags,@UsrAlbums,@UsrAlbumImages,@IsUserGroup,@IsHidden,@PersonalAccessMasksNumber,@PersonalGroupsNumber,@PersonalForumsNumber, 
		                @UserID,
                        @UserName,
                        @UserDisplayName,
						@UTCTIMESTAMP);
        set @GroupID = SCOPE_IDENTITY()
        insert into [{databaseSchema}].[{objectQualifier}ForumAccess](GroupID,ForumID,AccessMaskID)
        select @GroupID,a.ForumID,@AccessMaskID from [{databaseSchema}].[{objectQualifier}Forum] a join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID
    end	 
    -- group styles override rank styles	
    EXEC [{databaseSchema}].[{objectQualifier}user_savestyle] @GroupID,null
    
         
    if exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}GroupHistory] where GroupID = @GroupID and ChangedDate = @UTCTIMESTAMP)
    begin
    update [{databaseSchema}].[{objectQualifier}GroupHistory] set 
           ChangedUserID = @UserID,	
           ChangedUserName = @UserName,
           ChangedDisplayName = @UserDisplayName
        where GroupID = @GroupID and ChangedDate = @UTCTIMESTAMP
    end
    else
    begin
    INSERT INTO [{databaseSchema}].[{objectQualifier}GroupHistory](GroupID,ChangedUserID,ChangedUserName,ChangedDisplayName,ChangedDate)
    VALUES (@GroupID,@UserID,@UserName,@UserDisplayName,@UTCTIMESTAMP)
    end     
    select GroupID = @GroupID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}group_rank_style]( @BoardID int) as
begin
-- added fields to get overall info about groups and ranks
SELECT 1 AS LegendID,[Name],Style, PMLimit,[Description],UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages FROM [{databaseSchema}].[{objectQualifier}Group]
WHERE BoardID = @BoardID GROUP BY SortOrder,[Name],Style,[Description],PMLimit,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages
UNION
SELECT 2  AS LegendID,[Name],Style,PMLimit, [Description],UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages FROM [{databaseSchema}].[{objectQualifier}Rank]
WHERE BoardID = @BoardID GROUP BY SortOrder,[Name],Style,[Description],PMLimit,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages
end
GO

create procedure [{databaseSchema}].[{objectQualifier}mail_create]
(
    @From nvarchar(255),
    @FromName nvarchar(255) = NULL,
    @To nvarchar(255),
    @ToName nvarchar(255) = NULL,
    @Subject nvarchar(100),
    @Body ntext,
    @BodyHtml ntext = NULL,
    @UTCTIMESTAMP datetime
)
AS 
BEGIN
        insert into [{databaseSchema}].[{objectQualifier}Mail]
        (FromUser,FromUserName,ToUser,ToUserName,Created,Subject,Body,BodyHtml)
    values
        (@From,@FromName,@To,@ToName,@UTCTIMESTAMP ,@Subject,@Body,@BodyHtml)	
END
GO

create procedure [{databaseSchema}].[{objectQualifier}mail_createwatch]
(
    @TopicID int,
    @From nvarchar(255),
    @FromName nvarchar(255) = NULL,
    @Subject nvarchar(100),
    @Body ntext,
    @BodyHtml ntext = null,
    @UserID int,
    @UTCTIMESTAMP datetime
)
AS
BEGIN
    insert into [{databaseSchema}].[{objectQualifier}Mail](FromUser,FromUserName,ToUser,ToUserName,Created,[Subject],Body,BodyHtml)
    select
        @From,
        @FromName,
        b.Email,
        b.Name,
        @UTCTIMESTAMP ,
        @Subject,
        @Body,
        @BodyHtml
    from
        [{databaseSchema}].[{objectQualifier}WatchTopic] a
        inner join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
    where
        b.UserID <> @UserID and
        b.NotificationType NOT IN (10, 20) AND
        a.TopicID = @TopicID and
        (a.LastMail is null or a.LastMail < b.LastVisit)
    
    insert into [{databaseSchema}].[{objectQualifier}Mail](FromUser,FromUserName,ToUser,ToUserName,Created,Subject,Body,BodyHtml)
    select
        @From,
        @FromName,
        b.Email,
        b.Name,
        @UTCTIMESTAMP,
        @Subject,
        @Body,
        @BodyHtml
    from
        [{databaseSchema}].[{objectQualifier}WatchForum] a
        inner join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
        inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.ForumID=a.ForumID
    where
        b.UserID <> @UserID and
        b.NotificationType NOT IN (10, 20) AND
        c.TopicID = @TopicID and
        (a.LastMail is null or a.LastMail < b.LastVisit) and
        not exists(select 1 from [{databaseSchema}].[{objectQualifier}WatchTopic] x where x.UserID=b.UserID and x.TopicID=c.TopicID)

    update [{databaseSchema}].[{objectQualifier}WatchTopic] set LastMail = @UTCTIMESTAMP
    where TopicID = @TopicID
    and UserID <> @UserID
    
    update [{databaseSchema}].[{objectQualifier}WatchForum] set LastMail = @UTCTIMESTAMP  
    where ForumID = (select ForumID from [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID)
    and UserID <> @UserID
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}mail_delete](@MailID int) as
BEGIN
        DELETE FROM [{databaseSchema}].[{objectQualifier}Mail] WHERE MailID = @MailID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}mail_list]
(
    @ProcessID int,
    @UTCTIMESTAMP datetime
)
AS
BEGIN
    BEGIN TRANSACTION TRANSUPDATEMAIL
        UPDATE [{databaseSchema}].[{objectQualifier}Mail]
        SET 
            ProcessID = NULL
        WHERE
            ProcessID IS NOT NULL AND SendAttempt > @UTCTIMESTAMP

        UPDATE [{databaseSchema}].[{objectQualifier}Mail]
        SET 
            SendTries = SendTries + 1,
            SendAttempt = DATEADD(n,5,@UTCTIMESTAMP),
            ProcessID = @ProcessID
        WHERE
            MailID IN (SELECT TOP 10 MailID FROM [{databaseSchema}].[{objectQualifier}Mail] WHERE SendAttempt < @UTCTIMESTAMP OR SendAttempt IS NULL ORDER BY SendAttempt, Created)
    COMMIT TRANSACTION TRANSUPDATEMAIL

    -- now select all mail reserved for this process...
    SELECT TOP 10 * FROM [{databaseSchema}].[{objectQualifier}Mail] WHERE ProcessID = @ProcessID ORDER BY SendAttempt, Created desc
END

GO

create procedure [{databaseSchema}].[{objectQualifier}message_approve](@MessageID int) as begin
    
    declare	@UserID		int
    declare	@ForumID	int
    declare	@TopicID	int
    declare	@Flags	    int
    declare @Posted		datetime
    declare	@UserName	nvarchar(255)
    declare	@UserDisplayName	nvarchar(255)
    select 
        @UserID = a.UserID,
        @TopicID = a.TopicID,
        @ForumID = b.ForumID,
        @Posted = a.Posted,
        @UserName = a.UserName,
        @UserDisplayName = a.UserDisplayName,
        @Flags	= a.Flags
    from
        [{databaseSchema}].[{objectQualifier}Message] a
        inner join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID=a.TopicID
    where
        a.MessageID = @MessageID

    -- update Message table, set meesage flag to approved
    update [{databaseSchema}].[{objectQualifier}Message] set Flags = Flags | 16 where MessageID = @MessageID

    -- update User table to increase postcount
    if exists(select top 1 1 from [{databaseSchema}].[{objectQualifier}Forum] where ForumID=@ForumID and (Flags & 4)=0)
    begin
        update [{databaseSchema}].[{objectQualifier}User] set NumPosts = NumPosts + 1 where UserID = @UserID
        -- upgrade user, i.e. promote rank if conditions allow it
        exec [{databaseSchema}].[{objectQualifier}user_upgrade] @UserID
    end

    -- update Forum table with last topic/post info
    update [{databaseSchema}].[{objectQualifier}Forum] set
        LastPosted = @Posted,
        LastTopicID = @TopicID,
        LastMessageID = @MessageID,
        LastUserID = @UserID,
        LastUserName = @UserName,
        LastUserDisplayName = @UserDisplayName
    where ForumID = @ForumID

    -- update Topic table with info about last post in topic
    update [{databaseSchema}].[{objectQualifier}Topic] set
        LastPosted = @Posted,
        LastMessageID = @MessageID,
        LastUserID = @UserID,
        LastUserName = @UserName,
        LastUserDisplayName = @UserDisplayName,		
        LastMessageFlags = @Flags | 16,
        NumPosts = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and x.IsApproved = 1 and x.IsDeleted = 0)
    where TopicID = @TopicID
    
    -- update forum stats
    exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID	
end
GO

create procedure [{databaseSchema}].[{objectQualifier}message_delete](@MessageID int, @EraseMessage bit = 0) as
begin
    
    declare @TopicID		int
    declare @ForumID		int
    declare @MessageCount	int
    declare @LastMessageID	int
    declare @UserID			int

    -- Find TopicID and ForumID
    select @TopicID=b.TopicID,@ForumID=b.ForumID,@UserID = a.UserID 
        from 
            [{databaseSchema}].[{objectQualifier}Message] a
            inner join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID=a.TopicID
        where
            a.MessageID=@MessageID
   

    -- Update LastMessageID in Topic
    update [{databaseSchema}].[{objectQualifier}Topic] set 
        LastPosted = null,
        LastMessageID = null,
        LastUserID = null,
        LastUserName = null,
        LastUserDisplayName = null,
        LastMessageFlags = null
    where LastMessageID = @MessageID

    -- Update LastMessageID in Forum
    update [{databaseSchema}].[{objectQualifier}Forum] set 
        LastPosted = null,
        LastTopicID = null,
        LastMessageID = null,
        LastUserID = null,
        LastUserName = null,
        LastUserDisplayName = null
    where LastMessageID = @MessageID

    -- should it be physically deleter or not?
    if (@EraseMessage = 1) begin
        delete [{databaseSchema}].[{objectQualifier}Attachment] where MessageID = @MessageID
        delete [{databaseSchema}].[{objectQualifier}MessageReported] where MessageID = @MessageID
        delete [{databaseSchema}].[{objectQualifier}MessageReportedAudit] where MessageID = @MessageID
        --delete thanks related to this message
        delete [{databaseSchema}].[{objectQualifier}Thanks] where MessageID = @MessageID
        delete [{databaseSchema}].[{objectQualifier}MessageHistory] where MessageID = @MessageID
        delete [{databaseSchema}].[{objectQualifier}Message] where MessageID = @MessageID
        
    end
    else begin
        -- "Delete" it only by setting deleted flag message
        update [{databaseSchema}].[{objectQualifier}Message] set Flags = Flags | 8 where MessageID = @MessageID
    end
    
    -- update user post count
    UPDATE [{databaseSchema}].[{objectQualifier}User] SET NumPosts = (SELECT count(MessageID) FROM [{databaseSchema}].[{objectQualifier}Message] WHERE UserID = @UserID AND IsDeleted = 0 AND IsApproved = 1) WHERE UserID = @UserID
    
    -- Delete topic if there are no more messages
    select @MessageCount = count(1) from [{databaseSchema}].[{objectQualifier}Message] where TopicID = @TopicID and IsDeleted=0
    if @MessageCount=0 exec [{databaseSchema}].[{objectQualifier}topic_delete] @TopicID, null, 0, @EraseMessage

    -- update lastpost
    exec [{databaseSchema}].[{objectQualifier}topic_updatelastpost] @ForumID,@TopicID
    exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID

    -- update topic numposts
    update [{databaseSchema}].[{objectQualifier}Topic] set
        NumPosts = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and x.IsApproved = 1 and x.IsDeleted = 0)
    where TopicID = @TopicID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}message_findunread](
@TopicID int,
@MessageID int,
@LastRead datetime,
@ShowDeleted bit = 0,
@AuthorUserID int) as
begin

declare @firstmessageid int

declare @tbl_msglunr table 
(
cntrt int IDENTITY(1,1) NOT NULL,
MessageID int,
TopicID int,
Posted datetime,
Edited datetime
)
-- find first message id
select top 1	  
        @firstmessageid = m.MessageID
    from
        [{databaseSchema}].[{objectQualifier}Message] m	
    where
        m.TopicID = @TopicID ORDER BY m.Posted


   -- we return last 100 messages ONLY if we look for first unread or lastpost(Messageid = 0)
   if (@MessageID > 0)
   begin
   -- fill in the table variable with all topic's messages(slow). It's used in cases when we forced to find a particular message. 		
    insert into @tbl_msglunr (MessageID,TopicID,Posted,Edited) 
    select  
        m.MessageID,
        m.TopicID,
        m.Posted,
        Edited = IsNull(m.Edited,m.Posted)
    from
        [{databaseSchema}].[{objectQualifier}Message] m	
    where
        m.TopicID = @TopicID			
        AND m.IsApproved = 1
        AND (m.IsDeleted = 0 OR ((@ShowDeleted = 1 AND m.IsDeleted = 1) OR (@AuthorUserID > 0 AND m.UserID = @AuthorUserID)))
         AND m.Posted >	 @LastRead
    order by		
        m.Posted DESC
        end
    else
        begin
    -- fill in the table variable with last 100 values from topic's messages		
    insert into @tbl_msglunr (MessageID,TopicID,Posted,Edited) 
    select  top 100	  
        m.MessageID,
        m.TopicID,
        m.Posted,
        Edited = IsNull(m.Edited,m.Posted)
    from
        [{databaseSchema}].[{objectQualifier}Message] m	
    where
        m.TopicID = @TopicID			
        AND m.IsApproved = 1
        AND (m.IsDeleted = 0 OR ((@ShowDeleted = 1 AND m.IsDeleted = 1) OR (@AuthorUserID > 0 AND m.UserID = @AuthorUserID)))
        AND m.Posted >	@LastRead
    order by		
        m.Posted DESC
        end

         -- simply return last post if no unread message is found
  if EXISTS (SELECT TOP 1 1 FROM @tbl_msglunr) 
  begin
    -- the messageid was already supplied, find a particular message
    if (@MessageID > 0)
    begin
       if EXISTS (SELECT TOP 1 1 FROM @tbl_msglunr WHERE TopicID = @TopicID and MessageID = @MessageID)
        begin
         -- return first unread		
           select top 1 MessageID, MessagePosition = cntrt, FirstMessageID = @firstmessageid 
           from @tbl_msglunr
           where TopicID=@TopicID and  MessageID = @MessageID 		
        end
        else
        begin
         -- simply return last post if no unread message is found
           select top 1 MessageID, MessagePosition = 1, FirstMessageID = @firstmessageid 
           from @tbl_msglunr
           where TopicID=@TopicID and Posted> @LastRead
           order by Posted DESC
        end
    end
    else
    begin
       -- simply return last message as no MessageID was supplied 
       if EXISTS (SELECT TOP 1 1 FROM @tbl_msglunr WHERE Posted> @LastRead)
        begin
         -- return first unread		
           select top 1 MessageID, MessagePosition = cntrt, FirstMessageID = @firstmessageid 
           from @tbl_msglunr
           where TopicID=@TopicID and Posted>@LastRead  
           order by Posted  ASC
        end
        else
        begin
           select top 1 MessageID, MessagePosition = 1, FirstMessageID = @firstmessageid 
           from @tbl_msglunr
           where TopicID=@TopicID
           order by Posted DESC  
        end	
    end
end
    else
begin
        select top 1 m.MessageID, MessagePosition = 1, FirstMessageID = @firstmessageid 
    from
        [{databaseSchema}].[{objectQualifier}Message] m	
    where
        m.TopicID = @TopicID			
        AND m.IsApproved = 1
        AND (m.IsDeleted = 0 OR ((@ShowDeleted = 1 AND m.IsDeleted = 1) OR (@AuthorUserID > 0 AND m.UserID = @AuthorUserID)))	
    order by		
        m.Posted DESC
end

end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_getReplies](@MessageID int) as
BEGIN
    SELECT MessageID FROM [{databaseSchema}].[{objectQualifier}Message] WHERE ReplyTo = @MessageID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_list](@MessageID int) AS
BEGIN
        SELECT
        a.MessageID,
        a.UserID,
        UserName = b.Name,
        UserDisplayName = b.DisplayName,
        a.[Message],
        c.TopicID,
        c.ForumID,
        c.Topic,
        c.Priority,
        c.Description,
        c.Status,
        c.Styles,
        a.Flags,
        c.UserID AS TopicOwnerID,
        Edited = IsNull(a.Edited,a.Posted),
        TopicFlags = c.Flags,
        ForumFlags = d.Flags,
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
        [{databaseSchema}].[{objectQualifier}Message] a
        inner join [{databaseSchema}].[{objectQualifier}User] b on b.UserID = a.UserID
        inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = a.TopicID
        inner join [{databaseSchema}].[{objectQualifier}Forum] d on c.ForumID = d.ForumID
    WHERE
        a.MessageID = @MessageID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_secdata]( @PageUserID int, @MessageID int ) AS
BEGIN
-- BoardID=@BoardID and
if (@PageUserID is null)
select top 1 @PageUserID = UserID from [{databaseSchema}].[{objectQualifier}User] where  (Flags & 4)<>0 ORDER BY Joined DESC
SELECT
        m.MessageID,
        m.UserID,
        IsNull(t.UserName, u.Name) as Name,
        IsNull(t.UserDisplayName, u.DisplayName) as DisplayName,
        m.[Message],
        m.Posted,
        t.TopicID,
        t.ForumID,
        t.Topic,
        t.Priority,
        m.Flags,
        t.UserID,
        Edited = IsNull(m.Edited,m.Posted),
        EditedBy = IsNull(m.EditedBy,m.UserID), 
        TopicFlags = t.Flags,		
        m.EditReason,
        m.Position,
        m.IsModeratorChanged,
        m.DeleteReason,
        m.BlogPostID,
        t.PollID,
        m.IP
    FROM		
        [{databaseSchema}].[{objectQualifier}Topic] t 
        join  [{databaseSchema}].[{objectQualifier}Message] m ON m.TopicID = t.TopicID
        join  [{databaseSchema}].[{objectQualifier}User] u ON u.UserID = t.UserID
        left join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID=IsNull(t.ForumID,0)
    WHERE
        m.MessageID = @MessageID AND x.UserID=@PageUserID  AND  CONVERT(int,x.ReadAccess) > 0
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_listreported](@ForumID int) AS
BEGIN
        SELECT
        a.*,
        OriginalMessage = b.[Message],
        b.[Flags],
        b.[IsModeratorChanged],	
        UserName	= IsNull(b.UserName,d.Name),
        UserDisplayName	= IsNull(b.UserDisplayName,d.DisplayName),
        UserID = b.UserID,
        Posted		= b.Posted,
        TopicID = b.TopicID,
        Topic		= c.Topic,		
        NumberOfReports = (SELECT count(LogID) FROM [{databaseSchema}].[{objectQualifier}MessageReportedAudit] WHERE [{databaseSchema}].[{objectQualifier}MessageReportedAudit].MessageID = a.MessageID)
    FROM
        [{databaseSchema}].[{objectQualifier}MessageReported] a
    INNER JOIN
        [{databaseSchema}].[{objectQualifier}Message] b ON a.MessageID = b.MessageID
    INNER JOIN
        [{databaseSchema}].[{objectQualifier}Topic] c ON b.TopicID = c.TopicID
    INNER JOIN
        [{databaseSchema}].[{objectQualifier}User] d ON b.UserID = d.UserID
    WHERE
        c.ForumID = @ForumID and
        (c.Flags & 16)=0 and
        b.IsDeleted=0 and
        c.IsDeleted=0 and
        (b.Flags & 128)=128
    ORDER BY
        b.TopicID DESC, b.Posted DESC
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_listreporters](@MessageID int, @UserID int = null) AS
BEGIN
    IF ( @UserID > 0 )
    BEGIN
    SELECT b.UserID, UserName = a.Name,UserDisplayName = a.DisplayName, b.ReportedNumber, b.ReportText  
    FROM [{databaseSchema}].[{objectQualifier}User] a,			
    [{databaseSchema}].[{objectQualifier}MessageReportedAudit] b		
    WHERE   a.UserID = b.UserID  AND b.MessageID = @MessageID AND b.UserID = @UserID 
    END
    ELSE
    BEGIN
    SELECT b.UserID, UserName = a.Name,UserDisplayName = a.DisplayName, b.ReportedNumber, b.ReportText  
    FROM [{databaseSchema}].[{objectQualifier}User] a,			
    [{databaseSchema}].[{objectQualifier}MessageReportedAudit] b		
    WHERE   a.UserID = b.UserID  AND b.MessageID = @MessageID
    END
    
    
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_report](@MessageID int, @ReporterID int, @ReportedDate datetime, @ReportText nvarchar(4000),@UTCTIMESTAMP datetime) AS
BEGIN
    IF @ReportText IS NULL SET @ReportText = '';
    IF NOT exists(SELECT MessageID FROM [{databaseSchema}].[{objectQualifier}MessageReported] WHERE MessageID=@MessageID)
    BEGIN
        INSERT INTO [{databaseSchema}].[{objectQualifier}MessageReported](MessageID, [Message])
        SELECT 
            a.MessageID,
            a.[Message]
        FROM
            [{databaseSchema}].[{objectQualifier}Message] a
        WHERE
            a.MessageID = @MessageID
    END	
    IF NOT exists(SELECT MessageID from [{databaseSchema}].[{objectQualifier}MessageReportedAudit] WHERE MessageID=@MessageID AND UserID=@ReporterID)
        INSERT INTO [{databaseSchema}].[{objectQualifier}MessageReportedAudit](MessageID,UserID,Reported,ReportText) VALUES (@MessageID,@ReporterID,@ReportedDate, CONVERT(varchar,@UTCTIMESTAMP )+ '??' + @ReportText)
    ELSE 
        UPDATE [{databaseSchema}].[{objectQualifier}MessageReportedAudit] SET ReportedNumber = ( CASE WHEN ReportedNumber < 2147483647 THEN  ReportedNumber  + 1 ELSE ReportedNumber END ), Reported = @ReportedDate, ReportText = (CASE WHEN (LEN(ReportText) + LEN(@ReportText) + 255 < 4000)  THEN  ReportText + '|' + CONVERT(varchar(36),@UTCTIMESTAMP )+ '??' +  @ReportText ELSE ReportText END) WHERE MessageID=@MessageID AND UserID=@ReporterID 
    

    -- update Message table to set message with flag Reported
    UPDATE [{databaseSchema}].[{objectQualifier}Message] SET Flags = Flags | 128 WHERE MessageID = @MessageID

END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_reportresolve](@MessageFlag int, @MessageID int, @UserID int,@UTCTIMESTAMP datetime) AS
BEGIN
    
    UPDATE [{databaseSchema}].[{objectQualifier}MessageReported]
    SET Resolved = 1, ResolvedBy = @UserID, ResolvedDate = @UTCTIMESTAMP 
    WHERE MessageID = @MessageID;
    
    /* Remove Flag */
    UPDATE [{databaseSchema}].[{objectQualifier}Message]
    SET Flags = Flags & (~POWER(2, @MessageFlag))
    WHERE MessageID = @MessageID;
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_reportcopyover](@MessageID int) AS
BEGIN
        UPDATE [{databaseSchema}].[{objectQualifier}MessageReported]
    SET [{databaseSchema}].[{objectQualifier}MessageReported].[Message] = m.[Message]
    FROM [{databaseSchema}].[{objectQualifier}MessageReported] mr
    JOIN [{databaseSchema}].[{objectQualifier}Message] m ON m.MessageID = mr.MessageID
    WHERE mr.MessageID = @MessageID;
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_save](
    @TopicID		int,
    @UserID			int,
    @Message		ntext,	
    @UserName		nvarchar(255)=null,
    @IP				varchar(39),
    @Posted			datetime=null,
    @ReplyTo		int,
    @BlogPostID		nvarchar(50) = null,
    @ExternalMessageId nvarchar(255) = null,
    @ReferenceMessageId nvarchar(255) = null,
	@MessageDescription nvarchar(255) = null,
    @Flags			int,
    @UTCTIMESTAMP datetime,	
    @MessageID		int output
)
AS
BEGIN
        DECLARE @ForumID INT, @ForumFlags INT, @Position INT, @Indent INT, @OverrideDisplayName BIT, @ReplaceName nvarchar(255) 

    IF @Posted IS NULL
        SET @Posted = @UTCTIMESTAMP 

    SELECT @ForumID = x.ForumID, @ForumFlags = y.Flags
    FROM 
        [{databaseSchema}].[{objectQualifier}Topic] x
    INNER JOIN 
        [{databaseSchema}].[{objectQualifier}Forum] y ON y.ForumID=x.ForumID
    WHERE x.TopicID = @TopicID 

    IF @ReplyTo IS NULL
            SELECT @Position = 0, @Indent = 0 -- New thread

    ELSE IF @ReplyTo<0
        -- Find post to reply to AND indent of this post
        SELECT TOP 1 @ReplyTo = MessageID, @Indent = Indent+1
        FROM [{databaseSchema}].[{objectQualifier}Message]
        WHERE TopicID = @TopicID AND ReplyTo IS NULL
        ORDER BY Posted

    ELSE
        -- Got reply, find indent of this post
            SELECT @Indent=Indent+1
            FROM [{databaseSchema}].[{objectQualifier}Message]
            WHERE MessageID=@ReplyTo

    -- Find position
    IF @ReplyTo IS NOT NULL
    BEGIN
        DECLARE @temp INT
        
        SELECT @temp=ReplyTo,@Position=Position FROM [{databaseSchema}].[{objectQualifier}Message] WHERE MessageID=@ReplyTo

        IF @temp IS NULL
            -- We are replying to first post
            SELECT @Position=MAX(Position)+1 FROM [{databaseSchema}].[{objectQualifier}Message] WHERE TopicID=@TopicID

        ELSE
            -- Last position of replies to parent post
            SELECT @Position=MIN(Position) FROM [{databaseSchema}].[{objectQualifier}Message] WHERE ReplyTo=@temp AND Position>@Position

        -- No replies, THEN USE parent post's position+1
        IF @Position IS NULL
            SELECT @Position=Position+1 FROM [{databaseSchema}].[{objectQualifier}Message] WHERE MessageID=@ReplyTo
        -- Increase position of posts after this

        UPDATE [{databaseSchema}].[{objectQualifier}Message] SET Position=Position+1 WHERE TopicID=@TopicID AND Position>=@Position
    END
        -- this check is for guest user only to not override replace name 
    if (SELECT Name FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserID = @UserID) != @UserName
    begin
    SET @OverrideDisplayName = 1
    end
    SET @ReplaceName = (CASE WHEN @OverrideDisplayName = 1 THEN @UserName ELSE (SELECT DisplayName FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserID = @UserID) END);
    INSERT [{databaseSchema}].[{objectQualifier}Message] ( UserID, [Message],[Description], TopicID, Posted, Edited,UserName, UserDisplayName, IP, ReplyTo, Position, Indent, Flags, BlogPostID, ExternalMessageId, ReferenceMessageId)
    VALUES ( @UserID, @Message,@MessageDescription, @TopicID, @Posted, @Posted, @UserName,@ReplaceName, @IP, @ReplyTo, @Position, @Indent, @Flags & ~16, @BlogPostID, @ExternalMessageId, @ReferenceMessageId)	
    
    SET @MessageID = SCOPE_IDENTITY()

    IF ((@Flags & 16) = 16)
        EXEC [{databaseSchema}].[{objectQualifier}message_approve] @MessageID	
END
    
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}message_unapproved](@ForumID int) as begin
        select
        MessageID	= b.MessageID,
        UserID		= b.UserID,
        UserName	= IsNull(b.UserName,c.Name),
        UserDisplayName = IsNull(b.UserDisplayName, c.DisplayName),		
        Posted		= b.Posted,
        TopicID		= a.TopicID,
        Topic		= a.Topic,
		MessageCount    = a.NumPosts,
        [Message]	= b.[Message],
        [Flags]		= b.Flags,
        [IsModeratorChanged] = b.IsModeratorChanged
    from
        [{databaseSchema}].[{objectQualifier}Topic] a
        inner join [{databaseSchema}].[{objectQualifier}Message] b on b.TopicID = a.TopicID
        inner join [{databaseSchema}].[{objectQualifier}User] c on c.UserID = b.UserID
    where
        a.ForumID = @ForumID and
        b.IsApproved=0 and
        a.IsDeleted =0 and
        b.IsDeleted=0
    order by
        a.Posted
end

GO

CREATE procedure [{databaseSchema}].[{objectQualifier}message_update](
@MessageID int,
@Priority int,
@Subject nvarchar(100),
@Status nvarchar(255),
@Styles nvarchar(255),
@Description nvarchar(255),
@Flags int, 
@Message ntext, 
@Reason nvarchar(100), 
@EditedBy int,
@IsModeratorChanged bit, 
@OverrideApproval bit = null,
@OriginalMessage nvarchar(max),
@MessageDescription nvarchar(255),
@Tags nvarchar(1024),
@UTCTIMESTAMP datetime) as
begin
        declare @TopicID	int
    declare	@ForumFlags	int

    set @Flags = @Flags & ~16	
    
    select 
        @TopicID	= a.TopicID,
        @ForumFlags	= c.Flags
    from 
        [{databaseSchema}].[{objectQualifier}Message] a
        inner join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID = a.TopicID
        inner join [{databaseSchema}].[{objectQualifier}Forum] c on c.ForumID = b.ForumID
    where 
        a.MessageID = @MessageID

    if (@OverrideApproval = 1 OR (@ForumFlags & 8)=0) set @Flags = @Flags | 16
    
    
    -- insert current message variant - use OriginalMessage in future 	
    insert into [{databaseSchema}].[{objectQualifier}MessageHistory]
    (MessageID,		
        [Message],
        IP,
        Edited,
        EditedBy,		
        EditReason,
        IsModeratorChanged,
        Flags)
    select 
    MessageID, OriginalMessage=@OriginalMessage, IP , IsNull(Edited,Posted), IsNull(EditedBy,UserID), EditReason, IsModeratorChanged, Flags
    from [{databaseSchema}].[{objectQualifier}Message] where
        MessageID = @MessageID
    
    update [{databaseSchema}].[{objectQualifier}Message] set
        [Message] = @Message,
		[Description] = @MessageDescription,
        Edited = @UTCTIMESTAMP,
        EditedBy = @EditedBy,
        Flags = @Flags,
        IsModeratorChanged  = @IsModeratorChanged,
                EditReason = @Reason
    where
        MessageID = @MessageID

    if @Priority is not null begin
        update [{databaseSchema}].[{objectQualifier}Topic] set
            Priority = @Priority
        where
            TopicID = @TopicID
    end

    if not @Subject = '' and @Subject is not null begin
        update [{databaseSchema}].[{objectQualifier}Topic] set
            Topic = @Subject, 
            [Description] = @Description,
            [Status] = @Status,
            [Styles] = @Styles
        where
            TopicID = @TopicID
    end 

    exec  [{databaseSchema}].[{objectQualifier}topic_tagsave] @TopicID,@Tags

    -- If forum is moderated, make sure last post pointers are correct
    if (@ForumFlags & 8)<>0 exec [{databaseSchema}].[{objectQualifier}topic_updatelastpost]
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntpforum_delete](@NntpForumID int) as
begin
        delete from [{databaseSchema}].[{objectQualifier}NntpTopic] where NntpForumID = @NntpForumID
    delete from [{databaseSchema}].[{objectQualifier}NntpForum] where NntpForumID = @NntpForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntpforum_list](@BoardID int,@Minutes int=null,@NntpForumID int=null,@Active bit=null,@UTCTIMESTAMP datetime) as
begin
        select
        a.Name,
        a.[Address],
        Port = IsNull(a.Port,119),
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
        ForumName = c.Name,
        c.CategoryID
    from
        [{databaseSchema}].[{objectQualifier}NntpServer] a
        join [{databaseSchema}].[{objectQualifier}NntpForum] b on b.NntpServerID = a.NntpServerID
        join [{databaseSchema}].[{objectQualifier}Forum] c on c.ForumID = b.ForumID
    where
        (@Minutes is null or datediff(n,b.LastUpdate,@UTCTIMESTAMP )>@Minutes) and
        (@NntpForumID is null or b.NntpForumID=@NntpForumID) and
        a.BoardID=@BoardID and
        (@Active is null or b.Active=@Active)
    order by
        a.Name,
        b.GroupName
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntpforum_save](@NntpForumID int=null,@NntpServerID int,@GroupName nvarchar(100),@ForumID int,@Active bit,@DateCutOff datetime = null,@UTCTIMESTAMP datetime) as
begin
        if @NntpForumID is null
        insert into [{databaseSchema}].[{objectQualifier}NntpForum](NntpServerID,GroupName,ForumID,LastMessageNo,LastUpdate,Active,DateCutOff)
        values(@NntpServerID,@GroupName,@ForumID,0,DATEADD(d,-1,@UTCTIMESTAMP),@Active,@DateCutOff)
    else
        update [{databaseSchema}].[{objectQualifier}NntpForum] set
            NntpServerID = @NntpServerID,
            GroupName = @GroupName,
            ForumID = @ForumID,
            Active = @Active,
            DateCutOff = @DateCutOff
        where NntpForumID = @NntpForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntpforum_update](@NntpForumID int,@LastMessageNo int,@UserID int,@UTCTIMESTAMP datetime) as
begin
        declare	@ForumID	int
    
    select @ForumID=ForumID from [{databaseSchema}].[{objectQualifier}NntpForum] where NntpForumID=@NntpForumID

    update [{databaseSchema}].[{objectQualifier}NntpForum] set
        LastMessageNo = @LastMessageNo,
        LastUpdate = @UTCTIMESTAMP 
    where NntpForumID = @NntpForumID

    update [{databaseSchema}].[{objectQualifier}Topic] set 
        NumPosts = (select count(1) from [{databaseSchema}].[{objectQualifier}message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and x.IsApproved = 1 and x.IsDeleted = 0)
    where ForumID=@ForumID

    --exec [{databaseSchema}].[{objectQualifier}user_upgrade] @UserID
    exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID
    -- exec [{databaseSchema}].[{objectQualifier}topic_updatelastpost] @ForumID,null
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntpserver_delete](@NntpServerID int) as
begin
        delete from [{databaseSchema}].[{objectQualifier}NntpTopic] where NntpForumID in (select NntpForumID from [{databaseSchema}].[{objectQualifier}NntpForum] where NntpServerID = @NntpServerID)
    delete from [{databaseSchema}].[{objectQualifier}NntpForum] where NntpServerID = @NntpServerID
    delete from [{databaseSchema}].[{objectQualifier}NntpServer] where NntpServerID = @NntpServerID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntpserver_list](@BoardID int=null,@NntpServerID int=null) as
begin
        if @NntpServerID is null
        select * from [{databaseSchema}].[{objectQualifier}NntpServer] where BoardID=@BoardID order by Name
    else
        select * from [{databaseSchema}].[{objectQualifier}NntpServer] where NntpServerID=@NntpServerID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntpserver_save](
    @NntpServerID 	int=null,
    @BoardID	int,
    @Name		nvarchar(50),
    @Address	nvarchar(100),
    @Port		int,
    @UserName	nvarchar(255)=null,
    @UserPass	nvarchar(50)=null
) as begin
        if @NntpServerID is null
        insert into [{databaseSchema}].[{objectQualifier}NntpServer](Name,BoardID,Address,Port,UserName,UserPass)
        values(@Name,@BoardID,@Address,@Port,@UserName,@UserPass)
    else
        update [{databaseSchema}].[{objectQualifier}NntpServer] set
            Name = @Name,
            [Address] = @Address,
            Port = @Port,
            UserName = @UserName,
            UserPass = @UserPass
        where NntpServerID = @NntpServerID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntptopic_list](@Thread nvarchar(64)) as
begin
        select
        a.*
    from
        [{databaseSchema}].[{objectQualifier}NntpTopic] a
    where
        a.Thread = @Thread
end
GO

create procedure [{databaseSchema}].[{objectQualifier}nntptopic_savemessage](
    @NntpForumID	int,
    @Topic 			nvarchar(100),
    @Body 			ntext,
    @UserID 		int,
    @UserName		nvarchar(255),
    @IP				varchar(39),
    @Posted			datetime,
    @ExternalMessageId	nvarchar(255),
    @ReferenceMessageId nvarchar(255) = null,
    @UTCTIMESTAMP datetime
) as 
begin
    declare	@ForumID	int
    declare @TopicID	int
    declare	@MessageID	int
    declare @ReplyTo	int
    
    SET @TopicID = NULL
    SET @ReplyTo = NULL

    select @ForumID = ForumID from [{databaseSchema}].[{objectQualifier}NntpForum] where NntpForumID=@NntpForumID

    if exists(select top 1 1 from [{databaseSchema}].[{objectQualifier}Message] where ExternalMessageId = @ReferenceMessageId)
    begin
        -- referenced message exists
        select @TopicID = TopicID, @ReplyTo = MessageID from [{databaseSchema}].[{objectQualifier}Message] where ExternalMessageId = @ReferenceMessageId
    end else
    if not exists(select 1 from [{databaseSchema}].[{objectQualifier}Message] where ExternalMessageId = @ExternalMessageId)
    begin
        if (@ReferenceMessageId IS NULL)
        begin
            -- thread doesn't exists
            insert into [{databaseSchema}].[{objectQualifier}Topic](ForumID,UserID,UserName, UserDisplayName,Posted,Topic,[Views],Priority,NumPosts)
            values (@ForumID,@UserID,@UserName, @UserName,@Posted,@Topic,0,0,0)
            set @TopicID=SCOPE_IDENTITY()

            insert into [{databaseSchema}].[{objectQualifier}NntpTopic](NntpForumID,Thread,TopicID)
            values (@NntpForumID,'',@TopicID)
        end
    end
    
    IF @TopicID IS NOT NULL
    BEGIN
        exec [{databaseSchema}].[{objectQualifier}message_save]  @TopicID, @UserID, @Body, @UserName, @IP, @Posted, @ReplyTo, NULL, @ExternalMessageId, @ReferenceMessageId, null, 17,@UTCTIMESTAMP, @MessageID OUTPUT
    END	
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}activeaccess_reset] 
as
begin
delete from [{databaseSchema}].[{objectQualifier}Active];
delete from [{databaseSchema}].[{objectQualifier}ActiveAccess];
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}pageaccess](
    @BoardID int,
    @UserID	int,
    @IsGuest bit,
    @UTCTIMESTAMP datetime
) as
begin
    -- ensure that access right are in place		
        if not exists (select top 1
            UserID	
            from [{databaseSchema}].[{objectQualifier}ActiveAccess] WITH(NOLOCK) 
            where UserID = @UserID )		
            begin
            insert into [{databaseSchema}].[{objectQualifier}ActiveAccess](
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
            @BoardID, 
            ForumID, 
            IsAdmin,
            IsForumModerator,
            IsModerator,
            @IsGuest,
            @UTCTIMESTAMP,
            ReadAccess,
            (CONVERT([bit],sign([PostAccess]&(2)),(0))),
            ReplyAccess,
            PriorityAccess,
            PollAccess,
            VoteAccess,
            ModeratorAccess,
            EditAccess,
            DeleteAccess,
            UploadAccess,
            DownloadAccess,
            UserForumAccess			
            from [{databaseSchema}].[{objectQualifier}vaccess] 
            where UserID = @UserID 
            end
    -- return information
    select   
        x.*
    from
     [{databaseSchema}].[{objectQualifier}ActiveAccess] x 
    where
        x.UserID = @UserID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}pageaccess_path](
    @SessionID	nvarchar(24),
    @BoardID	int,
    @UserKey	nvarchar(64),
    @IP			varchar(39),
    @Location	nvarchar(255),
    @ForumPage  nvarchar(255) = null,
    @Browser	nvarchar(50),
    @Platform	nvarchar(50),
    @CategoryID	int = null,
    @ForumID	int = null,
    @TopicID	int = null,
    @MessageID	int = null,
    @IsCrawler	bit = 0,
    @IsMobileDevice	bit = 0,
    @DontTrack	bit = 0,
    @UTCTIMESTAMP datetime
) as
begin
    declare @UserID			int
    declare @UserBoardID	int
    declare @IsGuest		tinyint	
    declare @rowcount		int
    declare @PreviousVisit	datetime
    declare @ActiveUpdate   tinyint	
    declare @ActiveFlags	int
    declare @GuestID        int
    
    set implicit_transactions off	
    -- set IsActiveNow ActiveFlag - it's a default
    set @ActiveFlags = 1;


    -- find a guest id should do it every time to be sure that guest access rights are in ActiveAccess table
    select top 1 @GuestID = UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID and (Flags & 4)=4 ORDER BY Joined DESC
        set @rowcount=@@rowcount
        if (@rowcount > 1)
        begin
            raiserror('Found %d possible guest users. There should be one and only one user marked as guest.',16,1,@rowcount)
            end	
        if (@rowcount = 0)
        begin
            raiserror('No candidates for a guest were found for the board %d.',16,1,@BoardID)
            end
     -- verify that there's not the sane session for other board and drop it if required. Test code for portals with many boards
     delete from [{databaseSchema}].[{objectQualifier}Active] where (SessionID=@SessionID  and BoardID <> @BoardID)
             
    if @UserKey is null
    begin
    -- this is a guest
        SET @UserID = @GuestID
        set @IsGuest = 1
        -- set IsGuest ActiveFlag  1 | 2
        set @ActiveFlags = 3
        set @UserBoardID = @BoardID
        -- crawlers are always guests 
        if	@IsCrawler = 1
            begin
                -- set IsCrawler ActiveFlag
                set @ActiveFlags =  @ActiveFlags | 8
            end 
    end 
    else	
    begin
        select @UserID = UserID, @UserBoardID = BoardID from [{databaseSchema}].[{objectQualifier}User] with(nolock) where BoardID=@BoardID and ProviderUserKey=@UserKey
        set @IsGuest = 0
        -- make sure that registered users are not crawlers
        set @IsCrawler = 0
        -- set IsRegistered ActiveFlag
        set @ActiveFlags = @ActiveFlags | 4
    end

    
    -- Check valid ForumID
    if @ForumID is not null and not exists(select 1 from [{databaseSchema}].[{objectQualifier}Forum] where ForumID=@ForumID) begin
        set @ForumID = null
    end
    -- Check valid CategoryID
    if @CategoryID is not null and not exists(select 1 from [{databaseSchema}].[{objectQualifier}Category] where CategoryID=@CategoryID) begin
        set @CategoryID = null
    end
    -- Check valid MessageID
    if @MessageID is not null and not exists(select 1 from [{databaseSchema}].[{objectQualifier}Message] where MessageID=@MessageID) begin
        set @MessageID = null
    end
    -- Check valid TopicID
    if @TopicID is not null and not exists(select 1 from [{databaseSchema}].[{objectQualifier}Topic] where TopicID=@TopicID) begin
        set @TopicID = null
    end	
    
    -- get previous visit
    if  @IsGuest = 0	 begin
        select @PreviousVisit = LastVisit from [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
    end
    
    -- update last visit
    update [{databaseSchema}].[{objectQualifier}User] set 
        LastVisit = @UTCTIMESTAMP,
        IP = @IP
    where UserID = @UserID

    -- find missing ForumID/TopicID
    if @MessageID is not null begin
        select
            @CategoryID = c.CategoryID,
            @ForumID = b.ForumID,
            @TopicID = b.TopicID
        from
            [{databaseSchema}].[{objectQualifier}Message] a
            inner join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID = a.TopicID
            inner join [{databaseSchema}].[{objectQualifier}Forum] c on c.ForumID = b.ForumID
            inner join [{databaseSchema}].[{objectQualifier}Category] d on d.CategoryID = c.CategoryID
        where
            a.MessageID = @MessageID and
            d.BoardID = @BoardID
    end
    else if @TopicID is not null begin
        select 
            @CategoryID = b.CategoryID,
            @ForumID = a.ForumID 
        from 
            [{databaseSchema}].[{objectQualifier}Topic] a
            inner join [{databaseSchema}].[{objectQualifier}Forum] b on b.ForumID = a.ForumID
            inner join [{databaseSchema}].[{objectQualifier}Category] c on c.CategoryID = b.CategoryID
        where 
            a.TopicID = @TopicID and
            c.BoardID = @BoardID
    end
    else if @ForumID is not null begin
        select
            @CategoryID = a.CategoryID
        from
            [{databaseSchema}].[{objectQualifier}Forum] a
            inner join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID = a.CategoryID
        where
            a.ForumID = @ForumID and
            b.BoardID = @BoardID
    end
    
    if @DontTrack != 1 and @UserID is not null and @UserBoardID=@BoardID begin
      if exists(select 1 from [{databaseSchema}].[{objectQualifier}Active] where (SessionID=@SessionID OR ( Browser = @Browser AND (Flags & 8) = 8 )) and BoardID=@BoardID)
        begin
          -- user is not a crawler - use his session id
          if @IsCrawler <> 1
          begin		   
            update [{databaseSchema}].[{objectQualifier}Active] set
                UserID = @UserID,
                IP = @IP,
                LastActive = @UTCTIMESTAMP ,
                Location = @Location,
                ForumID = @ForumID,
                TopicID = @TopicID,
                Browser = @Browser,
                [Platform] = @Platform,
                ForumPage = @ForumPage		
            where SessionID = @SessionID AND BoardID=@BoardID			
            end
            else
            begin
            -- search crawler by other parameters then session id
            update [{databaseSchema}].[{objectQualifier}Active] set
                UserID = @UserID,
                IP = @IP,
                LastActive = @UTCTIMESTAMP ,
                Location = @Location,
                ForumID = @ForumID,
                TopicID = @TopicID,
                Browser = @Browser,
                [Platform] = @Platform,
                ForumPage = @ForumPage	
            where Browser = @Browser AND IP = @IP AND BoardID=@BoardID
            -- trace crawler: the cache is reset every time crawler moves to next page ? Disabled as cache reset will overload server 
            -- set @ActiveUpdate = 1			
            end
        end
        else 
        begin				
             -- we set @ActiveFlags ready flags 	
            insert into [{databaseSchema}].[{objectQualifier}Active](
            SessionID,
            BoardID,
            UserID,
            IP,
            Login,
            LastActive,
            Location,
            ForumID,
            TopicID,
            Browser,
            [Platform],
            Flags)
            values(
            @SessionID,
            @BoardID,
            @UserID,
            @IP,
            @UTCTIMESTAMP,
            @UTCTIMESTAMP,
            @Location,
            @ForumID,
            @TopicID,
            @Browser,
            @Platform,
            @ActiveFlags)			

            -- update max user stats
            exec [{databaseSchema}].[{objectQualifier}active_updatemaxstats] @BoardID,@UTCTIMESTAMP 
            -- parameter to update active users cache if this is a new user
            if @IsGuest=0
                  begin
                  set @ActiveUpdate = 1
            end
            
        end
        -- remove duplicate users
        if @IsGuest=0
        begin
            -- ensure that no duplicates 
            delete from [{databaseSchema}].[{objectQualifier}Active] where UserID=@UserID and BoardID=@BoardID and SessionID<>@SessionID	    
        
        end
        
    end
    -- return information
    select 
        ActiveUpdate        = ISNULL(@ActiveUpdate,0),
        PreviousVisit		= @PreviousVisit,	
        IsCrawler           = @IsCrawler,
        IsMobileDevice      = @IsMobileDevice,
        CategoryID			= @CategoryID,
        CategoryName		= (select Name from [{databaseSchema}].[{objectQualifier}Category] where CategoryID = @CategoryID),
        ForumName			= (select Name from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumID),
        TopicID				= @TopicID,
        TopicName			= (select Topic from [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID),
        ForumTheme			= (select ThemeURL from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumID)
    
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}pageload](
    @SessionID	nvarchar(24),
    @BoardID	int,
    @UserKey	nvarchar(64),
    @IP			varchar(39),
    @Location	nvarchar(255),
    @ForumPage  nvarchar(255) = null,
    @Browser	nvarchar(50),
    @Platform	nvarchar(50),
    @CategoryID	int = null,
    @ForumID	int = null,
    @TopicID	int = null,
    @MessageID	int = null,
    @IsCrawler	bit = 0,
    @IsMobileDevice	bit = 0,
    @DontTrack	bit = 0,
    @UTCTIMESTAMP datetime
) as
begin
    declare @UserID			int
    declare @UserBoardID	int
    declare @IsGuest		bit	
    declare @rowcount		int
    declare @PreviousVisit	datetime
    declare @ActiveUpdate   bit	
    declare @ActiveFlags	int
    declare @GuestID        int
    
    set implicit_transactions off	
    -- set IsActiveNow ActiveFlag - it's a default
    set @ActiveFlags = 1;


    -- find a guest id should do it every time to be sure that guest access rights are in ActiveAccess table
    select top 1 @GuestID = UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID and (Flags & 4)=4 ORDER BY Joined DESC
        set @rowcount=@@rowcount
        if (@rowcount > 1)
        begin
            raiserror('Found %d possible guest users. There should be one and only one user marked as guest.',16,1,@rowcount)
            end	
        if (@rowcount = 0)
        begin
            raiserror('No candidates for a guest were found for the board %d.',16,1,@BoardID)
            end
    
             
    if @UserKey is null
    begin
    -- this is a guest
        SET @UserID = @GuestID
        set @IsGuest = 1
        -- set IsGuest ActiveFlag  1 | 2
        set @ActiveFlags = 3
        set @UserBoardID = @BoardID
        -- crawlers are always guests 
        if	@IsCrawler = 1
            begin
                -- set IsCrawler ActiveFlag
                set @ActiveFlags =  @ActiveFlags | 8
            end 
    end 
    else	
    begin
        select @UserID = UserID, @UserBoardID = BoardID from [{databaseSchema}].[{objectQualifier}User] with(nolock) where BoardID=@BoardID and ProviderUserKey=@UserKey
        set @IsGuest = 0
        -- make sure that registered users are not crawlers
        set @IsCrawler = 0
        -- set IsRegistered ActiveFlag
        set @ActiveFlags = @ActiveFlags | 4
    end

     -- verify that there's not the sane session for other board and drop it if required. Test code for portals with many boards
     delete from [{databaseSchema}].[{objectQualifier}Active] where (SessionID=@SessionID  and (BoardID <> @BoardID or userid <> @UserID))

    -- Check valid ForumID
    if @ForumID is not null and not exists(select 1 from [{databaseSchema}].[{objectQualifier}Forum] where ForumID=@ForumID) begin
        set @ForumID = null
    end
    -- Check valid CategoryID
    if @CategoryID is not null and not exists(select 1 from [{databaseSchema}].[{objectQualifier}Category] where CategoryID=@CategoryID) begin
        set @CategoryID = null
    end
    -- Check valid MessageID
    if @MessageID is not null and not exists(select 1 from [{databaseSchema}].[{objectQualifier}Message] where MessageID=@MessageID) begin
        set @MessageID = null
    end
    -- Check valid TopicID
    if @TopicID is not null and not exists(select 1 from [{databaseSchema}].[{objectQualifier}Topic] where TopicID=@TopicID) begin
        set @TopicID = null
    end	
    
    -- get previous visit
    if  @IsGuest = 0	 begin
        select @PreviousVisit = LastVisit from [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
    end
    
    -- update last visit
    update [{databaseSchema}].[{objectQualifier}User] set 
        LastVisit = @UTCTIMESTAMP,
        IP = @IP
    where UserID = @UserID

    -- find missing ForumID/TopicID
    if @MessageID is not null begin
        select
            @CategoryID = c.CategoryID,
            @ForumID = b.ForumID,
            @TopicID = b.TopicID
        from
            [{databaseSchema}].[{objectQualifier}Message] a
            inner join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID = a.TopicID
            inner join [{databaseSchema}].[{objectQualifier}Forum] c on c.ForumID = b.ForumID
            inner join [{databaseSchema}].[{objectQualifier}Category] d on d.CategoryID = c.CategoryID
        where
            a.MessageID = @MessageID and
            d.BoardID = @BoardID
    end
    else if @TopicID is not null begin
        select 
            @CategoryID = b.CategoryID,
            @ForumID = a.ForumID 
        from 
            [{databaseSchema}].[{objectQualifier}Topic] a
            inner join [{databaseSchema}].[{objectQualifier}Forum] b on b.ForumID = a.ForumID
            inner join [{databaseSchema}].[{objectQualifier}Category] c on c.CategoryID = b.CategoryID
        where 
            a.TopicID = @TopicID and
            c.BoardID = @BoardID
    end
    else if @ForumID is not null begin
        select
            @CategoryID = a.CategoryID
        from
            [{databaseSchema}].[{objectQualifier}Forum] a
            inner join [{databaseSchema}].[{objectQualifier}Category] b on b.CategoryID = a.CategoryID
        where
            a.ForumID = @ForumID and
            b.BoardID = @BoardID
    end
    
    if @DontTrack != 1 and @UserID is not null and @UserBoardID=@BoardID begin
      if exists(select 1 from [{databaseSchema}].[{objectQualifier}Active] where (SessionID=@SessionID OR ( Browser = @Browser AND (Flags & 8) = 8 )) and BoardID=@BoardID)
        begin
          if (LEN(@ForumPage) > 1)
          begin
          -- user is not a crawler - use his session id
          if @IsCrawler <> 1
          begin		   
            update [{databaseSchema}].[{objectQualifier}Active] set
                UserID = @UserID,
                IP = @IP,
                LastActive = @UTCTIMESTAMP ,
                Location = @Location,
                ForumID = @ForumID,
                TopicID = @TopicID,
                Browser = @Browser,
                [Platform] = @Platform,
                ForumPage = @ForumPage,
                Flags = @ActiveFlags		
            where SessionID = @SessionID AND BoardID=@BoardID			
                -- cache will be updated every time set @ActiveUpdate = 1		
            end
            else
            begin
            -- search crawler by other parameters then session id
            update [{databaseSchema}].[{objectQualifier}Active] set
                UserID = @UserID,
                IP = @IP,
                LastActive = @UTCTIMESTAMP ,
                Location = @Location,
                ForumID = @ForumID,
                TopicID = @TopicID,
                Browser = @Browser,
                [Platform] = @Platform,
                ForumPage = @ForumPage,
                Flags = @ActiveFlags		
            where Browser = @Browser AND IP = @IP AND BoardID=@BoardID
            -- trace crawler: the cache is reset every time crawler moves to next page ? Disabled as cache reset will overload server 
                -- set @ActiveUpdate = 1				 
            end
        end				
        else
        begin
         update [{databaseSchema}].[{objectQualifier}Active] set           
                LastActive = @UTCTIMESTAMP               
            where SessionID = @SessionID AND BoardID=@BoardID			
        end	
        end	
        else 
        begin				
             -- we set @ActiveFlags ready flags 	
            insert into [{databaseSchema}].[{objectQualifier}Active](
            SessionID,
            BoardID,
            UserID,
            IP,
            Login,
            LastActive,
            Location,
            ForumID,
            TopicID,
            Browser,
            [Platform],
            Flags)
            values(
            @SessionID,
            @BoardID,
            @UserID,
            @IP,
            @UTCTIMESTAMP,
            @UTCTIMESTAMP,
            @Location,
            @ForumID,
            @TopicID,
            @Browser,
            @Platform,
            @ActiveFlags)			
            
            -- update max user stats
            exec [{databaseSchema}].[{objectQualifier}active_updatemaxstats] @BoardID, @UTCTIMESTAMP
            -- parameter to update active users cache if this is a new user
            if @IsGuest=0
                  begin
                  set @ActiveUpdate = 1
            end
            
        end
        -- remove duplicate users
        if @IsGuest=0
        begin
            -- ensure that no duplicates 
            delete from [{databaseSchema}].[{objectQualifier}Active] where UserID=@UserID and BoardID=@BoardID and SessionID<>@SessionID	    
        
        end
        
    end
    -- update active access
    -- ensure that access right are in place		
        if not exists (select top 1
            UserID	
            from [{databaseSchema}].[{objectQualifier}ActiveAccess] WITH(NOLOCK) 
            where UserID = @UserID )		
            begin
            insert into [{databaseSchema}].[{objectQualifier}ActiveAccess](
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
            @BoardID, 
            ForumID, 
            IsAdmin,
            IsForumModerator,
            IsModerator,
            @IsGuest,
            @UTCTIMESTAMP,
            ReadAccess,
            (CONVERT([bit],sign([PostAccess]&(2)),(0))),
            ReplyAccess,
            PriorityAccess,
            PollAccess,
            VoteAccess,
            ModeratorAccess,
            EditAccess,
            DeleteAccess,
            UploadAccess,
            DownloadAccess,
            UserForumAccess			
            from [{databaseSchema}].[{objectQualifier}vaccess] 
            where UserID = @UserID 
            end

                -- ensure that guest access right are in place		
        if not exists (select top 1
            UserID	
            from [{databaseSchema}].[{objectQualifier}ActiveAccess] WITH(NOLOCK) 
            where UserID = @GuestID )		
            begin
            insert into [{databaseSchema}].[{objectQualifier}ActiveAccess](
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
            @BoardID, 
            ForumID, 
            IsAdmin,
            IsForumModerator,
            IsModerator,
            @IsGuest,
            @UTCTIMESTAMP,
            ReadAccess,
            (CONVERT([bit],sign([PostAccess]&(2)),(0))),
            ReplyAccess,
            PriorityAccess,
            PollAccess,
            VoteAccess,
            ModeratorAccess,
            EditAccess,
            DeleteAccess,
            UploadAccess,
            DownloadAccess,
            UserForumAccess			
            from [{databaseSchema}].[{objectQualifier}vaccess] 
            where UserID = @GuestID 
            end

            
    -- return information
    select top 1
        ActiveUpdate        = ISNULL(@ActiveUpdate,0),
        PreviousVisit		= @PreviousVisit,	   
        x.*,	
		IsGuest             = @IsGuest,
        IsCrawler           = @IsCrawler,
        IsMobileDevice      = @IsMobileDevice,
        CategoryID			= @CategoryID,
        CategoryName		= (select Name from [{databaseSchema}].[{objectQualifier}Category] where CategoryID = @CategoryID),
        ForumName			= (select Name from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumID),
        TopicID				= @TopicID,
        TopicName			= (select Topic from [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID),
        ForumTheme			= (select ThemeURL from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumID)	 
    from
     [{databaseSchema}].[{objectQualifier}ActiveAccess] x 
    where
        x.UserID = @UserID and x.ForumID=IsNull(@ForumID,0)
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_delete](@UserPMessageID int, @FromOutbox bit = 0) as
BEGIN
        DECLARE @PMessageID int

    SET @PMessageID = (SELECT TOP 1 PMessageID FROM [{databaseSchema}].[{objectQualifier}UserPMessage] where UserPMessageID = @UserPMessageID);
        
    IF ( @FromOutbox = 1 AND EXISTS(SELECT (1) FROM [{databaseSchema}].[{objectQualifier}UserPMessage] WHERE UserPMessageID = @UserPMessageID AND IsInOutbox = 1 ) )
    BEGIN
        -- remove IsInOutbox bit which will remove it from the senders outbox
        UPDATE [{databaseSchema}].[{objectQualifier}UserPMessage] SET [Flags] = ([Flags] ^ 2) WHERE UserPMessageID = @UserPMessageID
    END
    
    IF ( @FromOutbox = 0 )
    BEGIN
            -- The pmessage is in archive but still is in sender outbox  
    IF ( EXISTS(SELECT (1) FROM [{databaseSchema}].[{objectQualifier}UserPMessage] WHERE UserPMessageID = @UserPMessageID AND IsInOutbox = 1 AND IsArchived = 1 AND IsDeleted = 0) )
    BEGIN
    -- Remove archive flag and set IsDeleted flag
    UPDATE [{databaseSchema}].[{objectQualifier}UserPMessage] SET [Flags] = [Flags] ^ 4  WHERE UserPMessageID = @UserPMessageID AND IsArchived = 1	
    END
        -- set is deleted...
        UPDATE [{databaseSchema}].[{objectQualifier}UserPMessage] SET [Flags] = ([Flags] ^ 8) WHERE UserPMessageID = @UserPMessageID
    END	
    
    -- see if there are no longer references to this PM.
    IF ( EXISTS(SELECT (1) FROM [{databaseSchema}].[{objectQualifier}UserPMessage] WHERE UserPMessageID = @UserPMessageID AND IsInOutbox = 0 AND IsDeleted = 1 ) )
    BEGIN
        -- delete
        DELETE FROM [{databaseSchema}].[{objectQualifier}UserPMessage] WHERE [PMessageID] = @PMessageID
        DELETE FROM [{databaseSchema}].[{objectQualifier}PMessage] WHERE [PMessageID] = @PMessageID
    END	

END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_info] as
begin
        select
        NumRead	= (select count(1) from [{databaseSchema}].[{objectQualifier}UserPMessage] WHERE IsRead<>0  AND IsDeleted<>1),
        NumUnread = (select count(1) from [{databaseSchema}].[{objectQualifier}UserPMessage] WHERE IsRead=0  AND IsDeleted<>1),
        NumTotal = (select count(1) from [{databaseSchema}].[{objectQualifier}UserPMessage] WHERE IsDeleted<>1)
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_list](@FromUserID int=null,@ToUserID int=null,@UserPMessageID int=null) AS
BEGIN				
        SELECT
    a.ReplyTo, a.PMessageID, b.UserPMessageID, a.FromUserID, d.[Name] AS FromUser, 
    b.[UserID] AS ToUserId, c.[Name] AS ToUser, a.Created, a.[Subject], 
    a.Body, a.Flags, b.IsRead,b.IsReply, b.IsInOutbox, b.IsArchived, b.IsDeleted
FROM
    [{databaseSchema}].[{objectQualifier}PMessage] a
INNER JOIN
    [{databaseSchema}].[{objectQualifier}UserPMessage] b ON a.PMessageID = b.PMessageID
INNER JOIN
    [{databaseSchema}].[{objectQualifier}User] c ON b.UserID = c.UserID
INNER JOIN
    [{databaseSchema}].[{objectQualifier}User] d ON a.FromUserID = d.UserID	
        WHERE	((@UserPMessageID IS NOT NULL AND b.UserPMessageID=@UserPMessageID) OR 
                 (@ToUserID   IS NOT NULL AND b.[UserID]  = @ToUserID) OR (@FromUserID IS NOT NULL AND a.FromUserID = @FromUserID))		
        ORDER BY Created DESC
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_markread](@UserPMessageID int=null)
AS
BEGIN
        UPDATE [{databaseSchema}].[{objectQualifier}UserPMessage] SET [Flags] = [Flags] | 1 WHERE UserPMessageID = @UserPMessageID AND IsRead = 0
END
GO

create procedure [{databaseSchema}].[{objectQualifier}pmessage_prune](@DaysRead int,@DaysUnread int,@UTCTIMESTAMP datetime) as
begin
        delete from [{databaseSchema}].[{objectQualifier}UserPMessage]
    where IsRead<>0
    and datediff(dd,(select Created from [{databaseSchema}].[{objectQualifier}PMessage] x where x.PMessageID=[{databaseSchema}].[{objectQualifier}UserPMessage].PMessageID),@UTCTIMESTAMP )>@DaysRead

    delete from [{databaseSchema}].[{objectQualifier}UserPMessage]
    where IsRead=0
    and datediff(dd,(select Created from [{databaseSchema}].[{objectQualifier}PMessage] x where x.PMessageID=[{databaseSchema}].[{objectQualifier}UserPMessage].PMessageID),@UTCTIMESTAMP )>@DaysUnread

    delete from [{databaseSchema}].[{objectQualifier}PMessage]
    where not exists(select 1 from [{databaseSchema}].[{objectQualifier}UserPMessage] x where x.PMessageID=[{databaseSchema}].[{objectQualifier}PMessage].PMessageID)
end
GO

create procedure [{databaseSchema}].[{objectQualifier}pmessage_save](
    @FromUserID	int,
    @ToUserID	int,
    @Subject	nvarchar(100),
    @Body		ntext,
    @Flags		int,
    @ReplyTo    int,
    @UTCTIMESTAMP datetime
) as
begin
    declare @PMessageID int
    declare @UserID int     
    
    IF @ReplyTo<0
    begin
        insert into [{databaseSchema}].[{objectQualifier}PMessage](FromUserID,Created,Subject,Body,Flags)
        values(@FromUserID,@UTCTIMESTAMP ,@Subject,@Body,@Flags)
    end
    else
    begin
        insert into [{databaseSchema}].[{objectQualifier}PMessage](FromUserID,Created,Subject,Body,Flags,ReplyTo)
        values(@FromUserID,@UTCTIMESTAMP ,@Subject,@Body,@Flags,@ReplyTo)

        UPDATE [{databaseSchema}].[{objectQualifier}UserPMessage] SET [IsReply] = (1) WHERE PMessageID = @ReplyTo
    end

    set @PMessageID = SCOPE_IDENTITY()
    if (@ToUserID = 0)
    begin
        insert into [{databaseSchema}].[{objectQualifier}UserPMessage](UserID,PMessageID,Flags)
        select
                a.UserID,@PMessageID,2
        from
                [{databaseSchema}].[{objectQualifier}User] a
                join [{databaseSchema}].[{objectQualifier}UserGroup] b on b.UserID=a.UserID
                join [{databaseSchema}].[{objectQualifier}Group] c on c.GroupID=b.GroupID where
                (c.Flags & 2)=0 and
                c.BoardID=(select BoardID from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=@FromUserID) and a.UserID<>@FromUserID
        group by
                a.UserID
    end
    else
    begin
        insert into [{databaseSchema}].[{objectQualifier}UserPMessage](UserID,PMessageID,Flags) values(@ToUserID,@PMessageID,2)
    end	
end
GO


CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}pmessage_archive](@UserPMessageID int = NULL) AS
BEGIN
        -- set IsArchived bit
    UPDATE [{databaseSchema}].[{objectQualifier}UserPMessage] SET [Flags] = ([Flags] | 4) WHERE UserPMessageID = @UserPMessageID AND IsArchived = 0
END
GO


CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}poll_stats](@PollID int = null) AS
BEGIN

    SELECT
            
        a.PollID,
        b.Question,
        b.Closes,
        b.UserID,		
        a.[ObjectPath],
        a.[MimeType],
        QuestionObjectPath = b.[ObjectPath],
        QuestionMimeType = b.[MimeType],
        a.ChoiceID,
        a.Choice,
        a.Votes,
        pg.IsBound, 
        b.IsClosedBound, 	
        b.AllowMultipleChoices,
        b.ShowVoters,
        b.AllowSkipVote,
        (select sum(x.Votes) from [{databaseSchema}].[{objectQualifier}Choice] x where  x.PollID = a.PollID) as [Total],
        [Stats] = (select 100 * a.Votes / case sum(x.Votes) when 0 then 1 else sum(x.Votes) end from [{databaseSchema}].[{objectQualifier}Choice] x where x.PollID=a.PollID)
    FROM
        [{databaseSchema}].[{objectQualifier}Choice] a		
    INNER JOIN 
        [{databaseSchema}].[{objectQualifier}Poll] b ON b.PollID = a.PollID
    INNER JOIN  
        [{databaseSchema}].[{objectQualifier}PollGroupCluster] pg ON pg.PollGroupID = b.PollGroupID	
        WHERE
        b.PollID = @PollID

END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_stats](@PollGroupID int) AS
BEGIN
        SELECT		
        GroupUserID = pg.UserID,	
        a.PollID,
        b.PollGroupID,
        b.Question,
        b.Closes,
        a.ChoiceID,		
        a.Choice,
        a.Votes,
        a.ObjectPath,
        a.MimeType,
        QuestionObjectPath = b.[ObjectPath],
        QuestionMimeType = b.[MimeType],
        pg.IsBound,
        b.IsClosedBound,		
        b.AllowMultipleChoices,
        b.ShowVoters,
        b.AllowSkipVote,
        (select sum(x.Votes) from [{databaseSchema}].[{objectQualifier}Choice] x where  x.PollID = a.PollID) as [Total],
        [Stats] = (select 100 * a.Votes / case sum(x.Votes) when 0 then 1 else sum(x.Votes) end from [{databaseSchema}].[{objectQualifier}Choice] x where x.PollID=a.PollID)
    FROM
        [{databaseSchema}].[{objectQualifier}Choice] a		
    INNER JOIN 
        [{databaseSchema}].[{objectQualifier}Poll] b ON b.PollID = a.PollID
    INNER JOIN  
        [{databaseSchema}].[{objectQualifier}PollGroupCluster] pg ON pg.PollGroupID = b.PollGroupID	  
    WHERE
        pg.PollGroupID = @PollGroupID
        ORDER BY b.PollGroupID
    --	GROUP BY a.PollID, b.Question, b.Closes, a.ChoiceID, a.Choice,a.Votes
        END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}pollvote_check](@PollID int, @UserID int = NULL,@RemoteIP varchar(39) = NULL) AS
        IF @UserID IS NULL
    BEGIN
        IF @RemoteIP IS NOT NULL
        BEGIN
            -- check by remote IP
            SELECT PollVoteID FROM [{databaseSchema}].[{objectQualifier}PollVote] WHERE PollID = @PollID AND RemoteIP = @RemoteIP
        END
    END
    ELSE
    BEGIN
        -- check by userid or remote IP
        SELECT PollVoteID FROM [{databaseSchema}].[{objectQualifier}PollVote] WHERE PollID = @PollID AND (UserID = @UserID OR RemoteIP = @RemoteIP)
    END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}pollgroup_votecheck](@PollGroupID int, @UserID int = NULL,@RemoteIP varchar(39) = NULL) AS
    IF @UserID IS NULL
      BEGIN
        IF @RemoteIP IS NOT NULL
        BEGIN
            -- check by remote IP
            SELECT PollID, ChoiceID FROM [{databaseSchema}].[{objectQualifier}PollVote] WHERE PollID IN ( SELECT PollID FROM [{databaseSchema}].[{objectQualifier}Poll] WHERE PollGroupID = @PollGroupID) AND RemoteIP = @RemoteIP
        END
        ELSE
        BEGIN
        -- to get structure
            SELECT pv.PollID, pv.ChoiceID, usr.Name as UserName 
            FROM [{databaseSchema}].[{objectQualifier}PollVote] pv 
            JOIN [{databaseSchema}].[{objectQualifier}User] usr ON usr.UserID = pv.UserID
            WHERE pv.PollID IN ( SELECT PollID FROM [{databaseSchema}].[{objectQualifier}Poll] WHERE PollGroupID = @PollGroupID)
        END
      END
    ELSE
      BEGIN
        -- check by userid or remote IP
        SELECT PollID, ChoiceID FROM [{databaseSchema}].[{objectQualifier}PollVote] WHERE PollID IN ( SELECT PollID FROM [{databaseSchema}].[{objectQualifier}Poll] WHERE PollGroupID = @PollGroupID) AND (UserID = @UserID OR RemoteIP = @RemoteIP)
       END
GO

create procedure [{databaseSchema}].[{objectQualifier}post_alluser](@BoardID int,@UserID int,@PageUserID int,@NumberOfMessages int = 0) as
begin
        IF (@NumberOfMessages IS NULL) SET @NumberOfMessages = 0;		
        SET NOCOUNT ON
        SET ROWCOUNT @NumberOfMessages

    select
        a.MessageID,
        a.Posted,
        [Subject] = c.Topic,
        a.[Message],		
        a.IP,
        a.UserID,
        a.Flags,
        UserName = IsNull(a.UserName,b.Name),
        UserDisplayName = IsNull(a.UserDisplayName, b.DisplayName),
        b.[Signature],
        c.TopicID,
		x.ReadAccess
    from
        [{databaseSchema}].[{objectQualifier}Message] a
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=a.UserID
        join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID=a.TopicID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] e on e.CategoryID=d.CategoryID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID=d.ForumID
    where
        a.UserID = @UserID and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        e.BoardID = @BoardID and
        (a.Flags & 24)=16 and
        c.IsDeleted=0
    order by
        a.Posted desc
        
    SET ROWCOUNT 0;
	SET NOCOUNT OFF
end
GO

create procedure [{databaseSchema}].[{objectQualifier}post_list](
                 @TopicID int,
                 @PageUserID int,
                 @AuthorUserID int,
                 @UpdateViewCount smallint=1, 
                 @ShowDeleted bit = 1, 
                 @StyledNicks bit = 0,
                 @ShowReputation bit = 0,
                 @SincePostedDate datetime, 
                 @ToPostedDate datetime, 
                 @SinceEditedDate datetime, 
                 @ToEditedDate datetime, 
                 @PageIndex int = 1, 
                 @PageSize int = 0, 
                 @SortPosted int = 2, 
                 @SortEdited int = 0,
                 @SortPosition int = 0,				
                 @ShowThanks bit = 0,
                 @MessagePosition int = 0,
				 @MessageID int,
				 @LastRead datetime,
                 @UTCTIMESTAMP datetime) as
begin
   declare @post_totalrowsnumber int 
   declare @firstselectrownum int 
  
   declare @firstselectposted datetime
   declare @firstselectedited datetime

   declare @floor decimal
   declare @ceiling decimal
  
   declare @offset int 
   
   declare @pagecorrection int
   declare @pageshift int;

    set nocount on
    if @UpdateViewCount>0
        update [{databaseSchema}].[{objectQualifier}Topic] set [Views] = [Views] + 1 where TopicID = @TopicID
    -- find total returned count
        select
        @post_totalrowsnumber = COUNT(m.MessageID)
    from
        [{databaseSchema}].[{objectQualifier}Message] m
    where
        m.TopicID = @TopicID
        AND m.IsApproved = 1
        AND (m.IsDeleted = 0 OR ((@ShowDeleted = 1 AND m.IsDeleted = 1) OR (@AuthorUserID > 0 AND m.UserID = @AuthorUserID)))
        AND m.Posted BETWEEN
         @SincePostedDate AND @ToPostedDate
         /*
        AND 
        m.Edited >= SinceEditedDate
        */
  
   -- number of messages on the last page @post_totalrowsnumber - @floor*@PageSize
   if (@MessagePosition > 0)
 begin

       -- round to ceiling - total number of pages  
       SELECT @ceiling = CEILING(CONVERT(decimal,@post_totalrowsnumber)/@PageSize) 
       -- round to floor - a number of full pages
       SELECT @floor = FLOOR(CONVERT(decimal,@post_totalrowsnumber)/@PageSize)

       SET @pageshift = @MessagePosition - (@post_totalrowsnumber - @floor*@PageSize)
            if  @pageshift < 0
               begin
                  SET @pageshift = 0
                     end   
   -- here pageshift converts to full pages 
   if (@pageshift <= 0)
   begin    
   set @pageshift = 0
   end
   else 
   begin
   set @pageshift = CEILING(CONVERT(decimal,@pageshift)/@PageSize) 
   end   
   
   SET @PageIndex = @ceiling - @pageshift 
   if @ceiling != @floor
   SET @PageIndex = @PageIndex - 1	      

   select @firstselectrownum = (@PageIndex) * @PageSize + 1    
  
 end  
 else
 begin
   select @PageIndex = @PageIndex+1;
   select @firstselectrownum = (@PageIndex - 1) * @PageSize + 1 
 end 
  
   -- find first selectedrowid 
   if (@firstselectrownum > 0)   
   set rowcount @firstselectrownum
   else
   -- should not be 0
   set rowcount 1
    
   select		
        @firstselectposted = m.Posted,
        @firstselectedited = m.Edited
    from
        [{databaseSchema}].[{objectQualifier}Message] m
    where
        m.TopicID = @TopicID
        AND m.IsApproved = 1
        AND (m.IsDeleted = 0 OR ((@ShowDeleted = 1 AND m.IsDeleted = 1) OR (@AuthorUserID > 0 AND m.UserID = @AuthorUserID)))
        AND m.Posted BETWEEN
         @SincePostedDate AND @ToPostedDate
         /*
        AND m.Edited > @SinceEditedDate
        */
        
    order by
        (case 
        when @SortPosition = 1 then m.Position end) ASC,	
        (case 
        when @SortPosted = 2 then m.Posted end) DESC,
        (case 
        when @SortPosted = 1 then m.Posted end) ASC, 
        (case 
        when @SortEdited = 2 then m.Edited end) DESC,
        (case 
        when @SortEdited = 1 then m.Edited end) ASC  	 		
            
    
    set rowcount @PageSize	
        
    select
        d.TopicID,
        d.Topic,
        d.Priority,
        d.Description,
        d.Status,
        d.Styles,
        d.PollID,
        d.UserID AS TopicOwnerID,
        TopicFlags	= d.Flags,
        ForumFlags	= g.Flags,
        m.MessageID,
        m.Posted,		
        [Message] = m.Message, 
		m.Description as MessageDescription, 
        m.UserID,
        m.Position,
        m.Indent,
        m.IP,		
        m.Flags,
        m.EditReason,
        m.IsModeratorChanged,
        m.IsDeleted,
        m.DeleteReason,
        m.BlogPostID,
        m.ExternalMessageId,
        m.ReferenceMessageId,
        UserName = IsNull(m.UserName,b.Name),
        DisplayName =IsNull(m.UserDisplayName,b.DisplayName),
        b.Suspended,
        b.Joined,
        b.Avatar,
        b.[Signature],
        Posts		= b.NumPosts,
        b.Points,
        ReputationVoteDate = (CASE WHEN @ShowReputation = 1 THEN CAST(ISNULL((select top 1 VoteDate from [{databaseSchema}].[{objectQualifier}ReputationVote] repVote where repVote.ReputationToUserID=b.UserID and repVote.ReputationFromUserID=@PageUserID), null) as datetime) ELSE @UTCTIMESTAMP END),
        (CONVERT([bit],sign(b.[Flags]&(4)),0))  as IsGuest,
        d.[Views],
        d.ForumID,
        RankName = c.Name,		
        c.RankImage,
		c.Style as RankStyle,	
        Style = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end, 
        Edited = IsNull(m.Edited,m.Posted),
		m.EditedBy,
        HasAttachments	= ISNULL((select top 1 1 from [{databaseSchema}].[{objectQualifier}Attachment] x where x.MessageID=m.MessageID),0),
        HasAvatarImage = ISNULL((select top 1 1 from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=b.UserID and AvatarImage is not null),0),
        TotalRows = @post_totalrowsnumber,
        PageIndex = @PageIndex,
        up.*
    from
        [{databaseSchema}].[{objectQualifier}Message] m
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=m.UserID
        left join [{databaseSchema}].[{objectQualifier}UserProfile] up on up.UserID=b.UserID
        join [{databaseSchema}].[{objectQualifier}Topic] d on d.TopicID=m.TopicID
        join [{databaseSchema}].[{objectQualifier}Forum] g on g.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] h on h.CategoryID=g.CategoryID
        join [{databaseSchema}].[{objectQualifier}Rank] c on c.RankID=b.RankID
    where
        m.TopicID = @TopicID
        AND m.IsApproved = 1
        AND (m.IsDeleted = 0 OR ((@ShowDeleted = 1 AND m.IsDeleted = 1) OR (@AuthorUserID > 0 AND m.UserID = @AuthorUserID)))
        AND (m.Posted is null OR (m.Posted is not null AND
        (m.Posted >= (case 
        when @SortPosted = 1 then
         @firstselectposted end) OR m.Posted <= (case 
        when @SortPosted = 2 then @firstselectposted end) OR
        m.Posted >= (case 
        when @SortPosted = 0 then 0 end))))	AND
        (m.Posted <= @ToPostedDate)	
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
        when @SortPosition = 1 then m.Position end) ASC,	
        (case 
        when @SortPosted = 2 then m.Posted end) DESC,
        (case 
        when @SortPosted = 1 then m.Posted end) ASC, 
        (case 
        when @SortEdited = 2 then m.Edited end) DESC,
        (case 
        when @SortEdited = 1 then m.Edited end) ASC  

        SET ROWCOUNT 0
end
GO

create procedure [{databaseSchema}].[{objectQualifier}post_list_reverse10](@TopicID int) as
begin
        set nocount on

    select top 10
        a.Posted,
        [Subject] = d.Topic,
        a.[Message],
        a.UserID,
        a.Flags,
        UserName = IsNull(a.UserName,b.Name),
        DisplayName = IsNull(a.UserDisplayName,b.DisplayName),
        Style = b.UserStyle,
        b.[Signature]
    from
        [{databaseSchema}].[{objectQualifier}Message] a 
        inner join [{databaseSchema}].[{objectQualifier}User] b on b.UserID = a.UserID
        inner join [{databaseSchema}].[{objectQualifier}Topic] d on d.TopicID = a.TopicID
    where
        (a.Flags & 24)=16 and
        a.TopicID = @TopicID
    order by
        a.Posted desc
end
GO

create procedure [{databaseSchema}].[{objectQualifier}rank_delete](@RankID int) as begin
        delete from [{databaseSchema}].[{objectQualifier}Rank] where RankID = @RankID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}rank_list](@BoardID int,@RankID int=null) as begin
        if @RankID is null
        select
            a.*
        from
            [{databaseSchema}].[{objectQualifier}Rank] a
        where
            a.BoardID=@BoardID
        order by
            a.SortOrder,
            a.Name
    else
        select
            a.*
        from
            [{databaseSchema}].[{objectQualifier}Rank] a
        where
            a.RankID = @RankID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_get](@BoardID int,@ProviderUserKey nvarchar(64)) as begin
      select UserID 
	  from [{databaseSchema}].[{objectQualifier}User] 
	  where BoardID=@BoardID and ProviderUserKey=@ProviderUserKey
end
GO

create procedure [{databaseSchema}].[{objectQualifier}rank_save](
    @RankID		int,
    @BoardID	int,
    @Name		nvarchar(50),
    @IsStart	bit,
    @IsLadder	bit,
	@IsGuest	bit,
    @MinPosts	int,
    @RankImage	nvarchar(50)=null,
    @PMLimit    int,
    @Style      nvarchar(255)=null,
    @SortOrder  int,
    @Description nvarchar(128)=null,
    @UsrSigChars int=null,
    @UsrSigBBCodes	nvarchar(255)=null,
    @UsrSigHTMLTags nvarchar(255)=null,
    @UsrAlbums int=null,
    @UsrAlbumImages int=null  
) as
begin
        declare @Flags int

    if @IsLadder=0 set @MinPosts = null
    if @IsLadder=1 and @MinPosts is null set @MinPosts = 0
    
    set @Flags = 0
    if @IsStart<>0 set @Flags = @Flags | 1
    if @IsLadder<>0 set @Flags = @Flags | 2
	IF @IsGuest<>0  set @Flags = @Flags | 4; 
    
    if @RankID>0 begin
        update [{databaseSchema}].[{objectQualifier}Rank] set
            Name = @Name,
            Flags = @Flags,
            MinPosts = @MinPosts,
            RankImage = @RankImage,
            PMLimit = @PMLimit,
            Style = @Style,
            SortOrder = @SortOrder,
            [Description] = @Description,
            UsrSigChars = @UsrSigChars,
            UsrSigBBCodes = @UsrSigBBCodes,
            UsrSigHTMLTags = @UsrSigHTMLTags,
            UsrAlbums = @UsrAlbums,
            UsrAlbumImages = @UsrAlbumImages
        where RankID = @RankID
    end
    else begin
        insert into [{databaseSchema}].[{objectQualifier}Rank](BoardID,Name,Flags,MinPosts,RankImage, PMLimit,Style,SortOrder,Description,UsrSigChars,UsrSigBBCodes,UsrSigHTMLTags,UsrAlbums,UsrAlbumImages)
        values(@BoardID,@Name,@Flags,@MinPosts,@RankImage,@PMLimit,@Style,@SortOrder,@Description,@UsrSigChars,@UsrSigBBCodes,@UsrSigHTMLTags,@UsrAlbums,@UsrAlbumImages);
        set @RankID = SCOPE_IDENTITY()
        -- select @RankID = RankID from [{databaseSchema}].[{objectQualifier}Rank] where RankID = @@Identity;
    end	
        -- group styles override rank styles
    EXEC [{databaseSchema}].[{objectQualifier}user_savestyle] null,@RankID	
end
GO

create procedure [{databaseSchema}].[{objectQualifier}registry_list](@Name nvarchar(50) = null,@BoardID int = null) as
BEGIN
        if @BoardID is null
    begin
        IF @Name IS NULL OR @Name = ''
        BEGIN
            SELECT * FROM [{databaseSchema}].[{objectQualifier}Registry] where BoardID is null
        END ELSE
        BEGIN
            SELECT * FROM [{databaseSchema}].[{objectQualifier}Registry] WHERE LOWER(Name) = LOWER(@Name) and BoardID is null
        END
    end else 
    begin
        IF @Name IS NULL OR @Name = ''
        BEGIN
            SELECT * FROM [{databaseSchema}].[{objectQualifier}Registry] where BoardID=@BoardID
        END ELSE
        BEGIN
            SELECT * FROM [{databaseSchema}].[{objectQualifier}Registry] WHERE LOWER(Name) = LOWER(@Name) and BoardID=@BoardID
        END
    end
END
GO

create procedure [{databaseSchema}].[{objectQualifier}registry_save](
    @Name nvarchar(50),
    @Value ntext = NULL,
    @BoardID int = null
) AS
BEGIN
        if @BoardID is null
    begin
        if exists(select 1 from [{databaseSchema}].[{objectQualifier}Registry] where lower(Name)=lower(@Name))
            update [{databaseSchema}].[{objectQualifier}Registry] set Value = @Value where lower(Name)=lower(@Name) and BoardID is null
        else
        begin
            insert into [{databaseSchema}].[{objectQualifier}Registry](Name,Value) values(lower(@Name),@Value)
        end
    end else
    begin
        if exists(select 1 from [{databaseSchema}].[{objectQualifier}Registry] where lower(Name)=lower(@Name) and BoardID=@BoardID)
            update [{databaseSchema}].[{objectQualifier}Registry] set Value = @Value where lower(Name)=lower(@Name) and BoardID=@BoardID
        else
        begin
            insert into [{databaseSchema}].[{objectQualifier}Registry](Name,Value,BoardID) values(lower(@Name),@Value,@BoardID)
        end
    end
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}replace_words_delete](@ID int) AS
BEGIN
        DELETE FROM [{databaseSchema}].[{objectQualifier}replace_words] WHERE ID = @ID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}replace_words_list]
(
    @BoardID int,
    @ID int = null
)
AS BEGIN
        IF (@ID IS NOT NULL AND @ID <> 0)
        SELECT * FROM [{databaseSchema}].[{objectQualifier}Replace_Words] WHERE BoardID = @BoardID AND ID = @ID
    ELSE
        SELECT * FROM [{databaseSchema}].[{objectQualifier}Replace_Words] WHERE BoardID = @BoardID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}replace_words_save]
(
    @BoardID int,
    @ID int = null,
    @BadWord nvarchar(255),
    @GoodWord nvarchar(255)
)
AS
BEGIN
        IF (@ID IS NOT NULL AND @ID <> 0)
    BEGIN
        UPDATE [{databaseSchema}].[{objectQualifier}replace_words] SET BadWord = @BadWord, GoodWord = @GoodWord WHERE ID = @ID		
    END
    ELSE BEGIN
        INSERT INTO [{databaseSchema}].[{objectQualifier}replace_words]
            (BoardID,BadWord,GoodWord)
        VALUES
            (@BoardID,@BadWord,@GoodWord)
    END
END
GO

create procedure [{databaseSchema}].[{objectQualifier}smiley_delete](@SmileyID int=null) as begin
        if @SmileyID is not null
        delete from [{databaseSchema}].[{objectQualifier}Smiley] where SmileyID=@SmileyID
    else
        delete from [{databaseSchema}].[{objectQualifier}Smiley]
end
GO

create procedure [{databaseSchema}].[{objectQualifier}smiley_list](@BoardID int,@SmileyID int=null) as
begin
        if @SmileyID is null
        select
        SmileyID,
        BoardID,
        Code,
        Icon,
        Emoticon,
        SortOrder = CONVERT(int,SortOrder)	 
        from [{databaseSchema}].[{objectQualifier}Smiley] where BoardID=@BoardID order by SortOrder, LEN(Code) desc
    else
        select 	
        SmileyID,
        BoardID,
        Code,
        Icon,
        Emoticon,
        SortOrder = CONVERT(int,SortOrder)	 
        from [{databaseSchema}].[{objectQualifier}Smiley] where SmileyID=@SmileyID order by SortOrder
end
GO

create procedure [{databaseSchema}].[{objectQualifier}smiley_listunique](@BoardID int) as
begin
        select 
        Icon, 
        Emoticon,
        Code = (select top 1 Code from [{databaseSchema}].[{objectQualifier}Smiley] x where x.Icon=[{databaseSchema}].[{objectQualifier}Smiley].Icon),
        SortOrder = (select top 1 SortOrder from [{databaseSchema}].[{objectQualifier}Smiley] x where x.Icon=[{databaseSchema}].[{objectQualifier}Smiley].Icon order by x.SortOrder asc)
    from 
        [{databaseSchema}].[{objectQualifier}Smiley]
    where
        BoardID=@BoardID
    group by
        Icon,
        Emoticon
    order by
        SortOrder,
        Code
end
GO

create procedure [{databaseSchema}].[{objectQualifier}smiley_save](@SmileyID int=null,@BoardID int,@Code nvarchar(10),@Icon nvarchar(50),@Emoticon nvarchar(50),@SortOrder tinyint,@Replace smallint=0) as begin
        if @SmileyID is not null begin
        update [{databaseSchema}].[{objectQualifier}Smiley] set Code = @Code, Icon = @Icon, Emoticon = @Emoticon, SortOrder = @SortOrder where SmileyID = @SmileyID
    end
    else begin
        if @Replace>0
            delete from [{databaseSchema}].[{objectQualifier}Smiley] where Code=@Code

        if not exists(select 1 from [{databaseSchema}].[{objectQualifier}Smiley] where BoardID=@BoardID and Code=@Code)
            insert into [{databaseSchema}].[{objectQualifier}Smiley](BoardID,Code,Icon,Emoticon,SortOrder) values(@BoardID,@Code,@Icon,@Emoticon,@SortOrder)
    end
end
GO

create procedure [{databaseSchema}].[{objectQualifier}smiley_resort](@BoardID int,@SmileyID int,@Move int) as
begin
        declare @Position int

    SELECT @Position=SortOrder FROM [{databaseSchema}].[{objectQualifier}Smiley] WHERE BoardID=@BoardID and SmileyID=@SmileyID

    if (@Position is null) return

    if (@Move > 0) begin
        update [{databaseSchema}].[{objectQualifier}Smiley]
            set SortOrder=SortOrder-1
            where BoardID=@BoardID and 
                SortOrder between @Position and (@Position + @Move) and
                SortOrder between 1 and 255
    end
    else if (@Move < 0) begin
        update [{databaseSchema}].[{objectQualifier}Smiley]
            set SortOrder=SortOrder+1
            where BoardID=@BoardID and 
                SortOrder between (@Position+@Move) and @Position and
                SortOrder between 0 and 254
    end

    SET @Position = @Position + @Move

    if (@Position>255) SET @Position = 255
    else if (@Position<0) SET @Position = 0

    update [{databaseSchema}].[{objectQualifier}Smiley]
        set SortOrder=@Position
        where BoardID=@BoardID and 
            SmileyID=@SmileyID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}system_initialize](
    @Name		nvarchar(50),
    @TimeZone	int,
    @Culture	varchar(10),
    @LanguageFile nvarchar(50),
    @ForumEmail	nvarchar(50),
    @SmtpServer	nvarchar(50),
    @User		nvarchar(255),
    @UserEmail	nvarchar(255),
    @Userkey	nvarchar(64),
    @RolePrefix nvarchar(255),
    @UTCTIMESTAMP datetime
    
) as 
begin
        DECLARE @tmpValue AS nvarchar(100)

    -- initalize required 'registry' settings
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'version','1'
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'versionname','1.0.0'
    SET @tmpValue = CAST(@TimeZone AS nvarchar(100))
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'timezone', @tmpValue
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'culture', @Culture
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'language', @LanguageFile
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'smtpserver', @SmtpServer
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'forumemail', @ForumEmail

    -- initalize new board
    EXEC [{databaseSchema}].[{objectQualifier}board_create] @Name, @Culture, @LanguageFile, '','',@User,@UserEmail,@UserKey,1,@RolePrefix,@UTCTIMESTAMP
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}system_updateversion]
(
    @Version		int,
    @VersionName	nvarchar(50)
) 
AS
BEGIN
        DECLARE @tmpValue AS nvarchar(100)
    SET @tmpValue = CAST(@Version AS nvarchar(100))
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'version', @tmpValue
    EXEC [{databaseSchema}].[{objectQualifier}registry_save] 'versionname',@VersionName

END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}topic_unread]
(   @BoardID int,
    @CategoryID int=null,
    @PageUserID int,		
    @SinceDate datetime=null,
    @ToDate datetime,
    @PageIndex int = 1, 
    @PageSize int = 0, 
    @StyledNicks bit = 0,	
    @FindLastRead bit = 0,
	@UTCTIMESTAMP datetime
)
AS
begin
   declare @post_totalrowsnumber int 
   declare @firstselectrownum int   
   declare @firstselectposted datetime
  -- declare @ceiling decimal  
  -- declare @offset int 

    set nocount on	

    -- find total returned count
        select
        @post_totalrowsnumber = count(1)		
        from
        [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
    where
        (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
        and	c.TopicMovedID is null	

      select @PageIndex = @PageIndex+1;
      select @firstselectrownum = (@PageIndex - 1) * @PageSize + 1 
        -- find first selectedrowid 
   if (@firstselectrownum > 0)   
   set rowcount @firstselectrownum
   else
   -- should not be 0
   set rowcount 1
    
   select		
        @firstselectposted = c.LastPosted		
    from
            [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
    where
        (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
        and	c.TopicMovedID is null	
    order by
        c.LastPosted desc,
        cat.SortOrder asc,
        d.SortOrder asc,
        d.Name asc,
        c.Priority desc		
    
    set rowcount @PageSize	
            select
        c.ForumID,
        c.TopicID,
        c.TopicMovedID,
        c.Posted,
        LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),
        [Subject] = c.Topic,
        [Description] = c.Description,
        [Status] = c.Status,
        [Styles] = c.Styles,
        c.UserID,
        Starter = IsNull(c.UserName,b.Name),		
        StarterDisplay = IsNull(c.UserDisplayName,b.DisplayName),		
        NumPostsDeleted = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] mes WHERE mes.TopicID = c.TopicID AND mes.IsDeleted = 1 AND mes.IsApproved = 1 AND ((@PageUserID IS NOT NULL AND mes.UserID = @PageUserID) OR (@PageUserID IS NULL)) ),
        Replies = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=c.TopicID and x.IsDeleted=0) - 1,
        [Views] = c.[Views],
        LastPosted = c.LastPosted,
        LastUserID = c.LastUserID,
        LastUserName = IsNull(c.LastUserName,(select x.Name from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastUserDisplayName = IsNull(c.LastUserDisplayName,(select x.DisplayName from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastMessageID = c.LastMessageID,
        LastMessageFlags = c.LastMessageFlags,
        LastTopicID = c.TopicID,
        TopicFlags = c.Flags,
        FavoriteCount = (SELECT COUNT(ID) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = IsNull(c.TopicMovedID,c.TopicID)),
        c.Priority,
        c.PollID,
        ForumName = d.Name,
        c.TopicMovedID,
        ForumFlags = d.Flags,
        FirstMessage = (SELECT TOP 1 CAST([Message] as nvarchar(1000)) FROM [{databaseSchema}].[{objectQualifier}Message] mes2 where mes2.TopicID = IsNull(c.TopicMovedID,c.TopicID) AND mes2.Position = 0),
        StarterStyle = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end,
        LastUserStyle= case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = c.LastUserID)
            else ''	 end,
        LastForumAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x WHERE x.ForumID=d.ForumID AND x.UserID = @PageUserID)
             else ''	 end,
        LastTopicAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y WHERE y.TopicID=c.TopicID AND y.UserID = @PageUserID)
             else ''	 end,
		[{databaseSchema}].[{objectQualifier}topic_gettags_str](c.TopicID) as TopicTags,
        c.TopicImage,
        c.TopicImageType,
        c.TopicImageBin,
        0 as HasAttachments, 
        TotalRows = @post_totalrowsnumber,
        PageIndex = @PageIndex
    from
        [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
    where
        c.LastPosted <= @firstselectposted and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
        and	c.TopicMovedID is null
    order by
        c.LastPosted desc,
        cat.SortOrder asc,
        d.SortOrder asc,
        d.Name asc,
        c.Priority desc	

        SET ROWCOUNT 0		
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}topic_active]
(   @BoardID int,
    @CategoryID int=null,
    @PageUserID int,		
    @SinceDate datetime,
    @ToDate datetime,
    @PageIndex int = 1, 
    @PageSize int = 0, 
    @StyledNicks bit = 0,	
    @FindLastUnread bit = 0,
	@UTCTIMESTAMP datetime
)
AS
begin
   declare @TotalRows int
   declare @FirstSelectRowNumber int
   declare @LastSelectRowNumber int
   
   -- find total returned count
   select @TotalRows = count(1)	
        from
        [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
    where
        (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
        and	c.TopicMovedID is null	
    
    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
    
    with TopicIds as
     (
     select ROW_NUMBER() over (order by cat.SortOrder asc, d.SortOrder asc, c.LastPosted desc) as RowNum, c.TopicID
     from [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
     where
        (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
        and	c.TopicMovedID is null	
      )	
      select
        c.ForumID,
        c.TopicID,
        c.TopicMovedID,		
        c.Posted,
        LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),
        [Subject] = c.Topic,
        [Description] = c.Description,
        [Status] = c.Status,
        [Styles] = c.Styles,
        c.UserID,
        Starter = IsNull(c.UserName,b.Name),
        StarterDisplay = IsNull(c.UserDisplayName, b.DisplayName),
        NumPostsDeleted = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] mes WHERE mes.TopicID = c.TopicID AND mes.IsDeleted = 1 AND mes.IsApproved = 1 AND ((@PageUserID IS NOT NULL AND mes.UserID = @PageUserID) OR (@PageUserID IS NULL)) ),
        Replies = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=c.TopicID and x.IsDeleted=0) - 1,
        [Views] = c.[Views],
        LastPosted = c.LastPosted,
        LastUserID = c.LastUserID,
        LastUserName = IsNull(c.LastUserName,(select x.Name from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastUserDisplayName = IsNull(c.LastUserDisplayName,(select x.DisplayName from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastMessageID = c.LastMessageID,
        LastMessageFlags = c.LastMessageFlags,
        LastTopicID = c.TopicID,
        TopicFlags = c.Flags,
        FavoriteCount = (SELECT COUNT(ID) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = IsNull(c.TopicMovedID,c.TopicID)),
        c.Priority,
        c.PollID,
        ForumName = d.Name,
        c.TopicMovedID,
        ForumFlags = d.Flags,
        FirstMessage = (SELECT TOP 1 CAST([Message] as nvarchar(1000)) FROM [{databaseSchema}].[{objectQualifier}Message] mes2 where mes2.TopicID = IsNull(c.TopicMovedID,c.TopicID) AND mes2.Position = 0),
        StarterStyle = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end,
        LastUserStyle= case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = c.LastUserID)
            else ''	 end,
        LastForumAccess = case(@FindLastUnread)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x WHERE x.ForumID=d.ForumID AND x.UserID = @PageUserID)
             else ''	 end,
        LastTopicAccess = case(@FindLastUnread)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y WHERE y.TopicID=c.TopicID AND y.UserID = @PageUserID)
             else ''	 end,
        [{databaseSchema}].[{objectQualifier}topic_gettags_str](c.TopicID) as TopicTags,
		c.TopicImage,
        c.TopicImageType,
        c.TopicImageBin,
        0 as HasAttachments, 
        TotalRows = @TotalRows,
        PageIndex = @PageIndex   
    from
        TopicIds ti
        inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = ti.TopicID
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
    where ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC
    
end
GO

create procedure [{databaseSchema}].[{objectQualifier}topic_tags](@BoardID int,@PageUserID int,@TopicID int) as
begin
declare @ici_allcount int

SET @ici_allcount = (SELECT MAX(tg.TagCount)  FROM [{databaseSchema}].[{objectQualifier}Tags] tg 
    JOIN [{databaseSchema}].[{objectQualifier}TopicTags] tt ON tt.TagID = tg.TagID 
    JOIN [{databaseSchema}].[{objectQualifier}Topic] t ON t.TopicID = tt.TagID
    JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa ON aa.ForumID = t.ForumID	 
    WHERE BoardID=@BoardID and tt.TopicID=@TopicID AND aa.UserID = @PageUserID)

    SELECT tg.TagID,tg.Tag,tg.TagCount,@ici_allcount AS MaxTagCount FROM [{databaseSchema}].[{objectQualifier}Tags] tg 
    JOIN [{databaseSchema}].[{objectQualifier}TopicTags] tt ON tt.TagID = tg.TagID 
    JOIN [{databaseSchema}].[{objectQualifier}Topic] t ON t.TopicID = tt.TagID
    JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa ON aa.ForumID = t.ForumID
    WHERE BoardID=@BoardID and tt.TopicID=@TopicID AND aa.UserID = @PageUserID
    ORDER BY Tag
end
GO

create procedure [{databaseSchema}].[{objectQualifier}topic_bytags](@BoardID int, @ForumID int, @UserID int = null,@Tags nvarchar(max), @SinceDate datetime, @PageIndex int, @PageSize int) as
begin
declare @TotalRows int
declare @FirstSelectRowNumber int
declare @FirstSelectPosted datetime

           set nocount on
           set @PageIndex = @PageIndex + 1
           set @FirstSelectRowNumber = 0
          -- set @FirstSelectPosted = 0
           set @TotalRows = 0
           
           select @TotalRows = count(1) from [{databaseSchema}].[{objectQualifier}Topic] t 
           JOIN [{databaseSchema}].[{objectQualifier}TopicTags] tt ON tt.TopicID = t.TopicID
           JOIN [{databaseSchema}].[{objectQualifier}Tags] tg ON tg.TagID = tt.TagID
           JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa ON (aa.ForumID = t.ForumID and aa.UserID = @UserID) 
           where   t.IsDeleted = 0 AND (@ForumID <=0 OR t.ForumID = @ForumID) and tt.TagID  IN (SELECT CONVERT(int,@Tags)) and t.Posted > @SinceDate
            
           select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1
           
           if (@FirstSelectRowNumber <= @TotalRows)
           begin
           -- find first selectedrowid 
           set rowcount @FirstSelectRowNumber
           end
           else
           begin  
           set rowcount 1
           end
      -- find first row id for a current page 
      select @FirstSelectPosted = t.Posted 
      from [{databaseSchema}].[{objectQualifier}Topic] t 
           JOIN [{databaseSchema}].[{objectQualifier}TopicTags] tt ON tt.TopicID = t.TopicID
           JOIN [{databaseSchema}].[{objectQualifier}Tags] tg ON tg.TagID = tt.TagID
           JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa ON (aa.ForumID = t.ForumID and aa.UserID = @UserID) 
           where  t.IsDeleted = 0 AND (@ForumID <=0 OR t.ForumID = @ForumID)  and tt.TagID   IN (SELECT CONVERT(int,@Tags)) and t.Posted > @SinceDate
        ORDER BY t.Posted DESC

      -- display page 
      set rowcount @PageSize
     select	        
            t.TopicID,
            t.Posted,
            LinkTopicID = IsNull(t.TopicMovedID,t.TopicID),
            t.TopicMovedID,
            FavoriteCount = (SELECT COUNT(1) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = IsNull(t.TopicMovedID,t.TopicID)),
            [Subject] = t.Topic,
            t.[Description],
            t.[Status],
            t.[Styles],
            t.UserID,
            Starter = IsNull(t.UserName,b.Name),
            StarterDisplay = IsNull(t.UserDisplayName,b.DisplayName),
            Replies = t.NumPosts - 1,
            NumPostsDeleted = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] mes WHERE mes.TopicID = t.TopicID AND mes.IsDeleted = 1 AND mes.IsApproved = 1 AND ((@UserID IS NOT NULL AND mes.UserID = @UserID) OR (@UserID IS NULL)) ),			
            [Views] = t.[Views],
            LastPosted = t.LastPosted,
            LastUserID = t.LastUserID,
            LastUserName = IsNull(t.LastUserName,(SELECT x.Name FROM [{databaseSchema}].[{objectQualifier}User] x where x.UserID=t.LastUserID)),
            LastUserDisplayName = IsNull(t.LastUserDisplayName,(SELECT x.DisplayName FROM [{databaseSchema}].[{objectQualifier}User] x where x.UserID=t.LastUserID)),
            LastMessageID = t.LastMessageID,
            LastTopicID = t.TopicID,
            LinkDate = t.LinkDate,
            TopicFlags = t.Flags,
            t.Priority,
            t.PollID,
            ForumFlags = d.Flags,
            FirstMessage = (SELECT TOP 1 CAST([Message] as nvarchar(1000)) FROM [{databaseSchema}].[{objectQualifier}Message] mes2 where mes2.TopicID = IsNull(t.TopicMovedID,t.TopicID) AND mes2.Position = 0),
            StarterStyle = case(0)
            when 1 then  b.UserStyle
            else ''	 end,
            LastUserStyle= case(0)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = t.LastUserID)
            else ''	 end,
            LastForumAccess = '',
            LastTopicAccess = '',					
        t.ForumID,		
        m.Message,
        Tags = (SELECT TOP 1 Tag FROM [{databaseSchema}].[{objectQualifier}Tags] where TagID = CONVERT(int,@Tags)),
		[{databaseSchema}].[{objectQualifier}topic_gettags_str](t.TopicID) as TopicTags,
        t.TopicImage,
        t.TopicImageType,
        t.TopicImageBin,
        0 as HasAttachments, 		
        @TotalRows as TotalRows
    from [{databaseSchema}].[{objectQualifier}Topic] t 
           JOIN [{databaseSchema}].[{objectQualifier}TopicTags] tt ON tt.TopicID = t.TopicID
           JOIN [{databaseSchema}].[{objectQualifier}Tags] tg ON tg.TagID = tt.TagID
           JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa ON (aa.ForumID = t.ForumID and aa.UserID = @UserID) 
           JOIN [{databaseSchema}].[{objectQualifier}Message] m ON (m.TopicID = t.TopicID and m.Position = 0)
           JOIN [{databaseSchema}].[{objectQualifier}User] b ON b.UserID=t.UserID
           JOIN [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=t.ForumID
           where t.Posted <= @FirstSelectPosted and t.IsDeleted = 0 AND (@ForumID <=0 OR t.ForumID = @ForumID) and tt.TagID  IN (SELECT CONVERT(int,@Tags)) and t.Posted > @SinceDate
        ORDER BY t.Posted DESC
                
   set nocount off	
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_delete] (@TopicID int, @TopicMovedID int, @UpdateLastPost bit=1,@EraseTopic bit=0) 
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @ForumID int   	
    DECLARE @pollID int
    -- AND LinkDate IS NOT NULL	   
    IF (@TopicMovedID = @TopicID)
    BEGIN
    DELETE FROM [{databaseSchema}].[{objectQualifier}Topic] where TopicMovedID = @TopicID
    END
    ELSE
    BEGIN
	SELECT @ForumID=ForumID FROM  [{databaseSchema}].[{objectQualifier}Topic] WHERE TopicID=@TopicID
    UPDATE [{databaseSchema}].[{objectQualifier}Topic] SET LastMessageID = null WHERE TopicID = @TopicID
    
    UPDATE [{databaseSchema}].[{objectQualifier}Forum] SET 
        LastTopicID = null,
        LastMessageID = null,
        LastUserID = null,
        LastUserName = null,
        LastUserDisplayName = null,
        LastPosted = null
    WHERE LastMessageID IN (SELECT MessageID FROM  [{databaseSchema}].[{objectQualifier}Message] WHERE TopicID = @TopicID)
    
    UPDATE  [{databaseSchema}].[{objectQualifier}Active] SET TopicID = null WHERE TopicID = @TopicID
    
    --delete messages and topics
	IF @EraseTopic = 0
    BEGIN
        UPDATE  [{databaseSchema}].[{objectQualifier}topic] set Flags = Flags | 8 where TopicMovedID = @TopicID
        UPDATE  [{databaseSchema}].[{objectQualifier}topic] set Flags = Flags | 8 where TopicID = @TopicID
        UPDATE  [{databaseSchema}].[{objectQualifier}Message] set Flags = Flags | 8 where TopicID = @TopicID
    END
    ELSE
    BEGIN
	    DELETE FROM  [{databaseSchema}].[{objectQualifier}nntptopic] WHERE TopicID = @TopicID
        --remove polls	
        SELECT @pollID = pollID FROM  [{databaseSchema}].[{objectQualifier}Topic] WHERE TopicID = @TopicID
        IF (@pollID is not null)
        BEGIN
            UPDATE  [{databaseSchema}].[{objectQualifier}Topic] SET PollID = null WHERE TopicID = @TopicID
            EXEC [{databaseSchema}].[{objectQualifier}pollgroup_remove] @pollID, @TopicID, null, null, null, 0, 0 
        END	

		-- remove tags references
        UPDATE [{databaseSchema}].[{objectQualifier}Tags] set TagCount = TagCount - 1 where TagID in (select TagID from [{databaseSchema}].[{objectQualifier}TopicTags]  WHERE TopicID = @TopicID);
	    DELETE FROM [{databaseSchema}].[{objectQualifier}TopicTags]  WHERE TopicID IN (select TopicID from [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID or TopicMovedID = @TopicID); 
       
	    DELETE FROM  [{databaseSchema}].[{objectQualifier}Topic] WHERE TopicMovedID = @TopicID
        
        DELETE  [{databaseSchema}].[{objectQualifier}Attachment] WHERE MessageID IN (SELECT MessageID FROM  [{databaseSchema}].[{objectQualifier}message] WHERE TopicID = @TopicID) 
        DELETE  [{databaseSchema}].[{objectQualifier}MessageHistory] WHERE MessageID IN (SELECT MessageID FROM  [{databaseSchema}].[{objectQualifier}message] WHERE TopicID = @TopicID) 	
        DELETE  [{databaseSchema}].[{objectQualifier}Message] WHERE TopicID = @TopicID
        DELETE  [{databaseSchema}].[{objectQualifier}WatchTopic] WHERE TopicID = @TopicID
        DELETE  [{databaseSchema}].[{objectQualifier}TopicReadTracking] WHERE TopicID = @TopicID
        DELETE  [{databaseSchema}].[{objectQualifier}FavoriteTopic]  WHERE TopicID = @TopicID
        DELETE  [{databaseSchema}].[{objectQualifier}Topic] WHERE TopicMovedID = @TopicID
        DELETE  [{databaseSchema}].[{objectQualifier}Topic] WHERE TopicID = @TopicID
        DELETE  [{databaseSchema}].[{objectQualifier}MessageReportedAudit] WHERE MessageID IN (SELECT MessageID FROM  [{databaseSchema}].[{objectQualifier}message] WHERE TopicID = @TopicID) 
        DELETE  [{databaseSchema}].[{objectQualifier}MessageReported] WHERE MessageID IN (SELECT MessageID FROM  [{databaseSchema}].[{objectQualifier}message] WHERE TopicID = @TopicID)
        
    END
        
    --commit
    IF @UpdateLastPost<>0
        EXEC  [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @ForumID
    
    IF @ForumID is not null
        EXEC  [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID
END
END
GO

create procedure [{databaseSchema}].[{objectQualifier}pollgroup_remove](@PollGroupID int, @TopicID int =null, @ForumID int= null, @CategoryID int = null, @BoardID int = null, @RemoveCompletely bit, @RemoveEverywhere bit)
 as
  begin   
    declare @polllist table( PollID int)
    declare @tmp int

    
    
             -- we delete poll from the place only it persists in other places 
         if @RemoveEverywhere <> 1
             begin
                   if @TopicID > 0
                   Update [{databaseSchema}].[{objectQualifier}Topic] set PollID = NULL where TopicID = @TopicID                 
                  
                   if @ForumID > 0
                   Update [{databaseSchema}].[{objectQualifier}Forum] set PollGroupID = NULL where ForumID = @ForumID
              
                   if @CategoryID > 0
                   Update [{databaseSchema}].[{objectQualifier}Category] set PollGroupID = NULL where CategoryID = @CategoryID
                
             end        
            
          -- we remove poll group links from all places where they are
         if ( @RemoveEverywhere = 1 OR @RemoveCompletely = 1)
         begin
                   Update [{databaseSchema}].[{objectQualifier}Topic] set PollID = NULL where PollID = @PollGroupID 
                   Update [{databaseSchema}].[{objectQualifier}Forum] set PollGroupID = NULL where PollGroupID = @PollGroupID
                   Update [{databaseSchema}].[{objectQualifier}Category] set PollGroupID = NULL where PollGroupID = @PollGroupID				 
         end

         -- simply remove all polls
    if @RemoveCompletely = 1 
    begin	
    insert into @polllist (PollID)
    select PollID from [{databaseSchema}].[{objectQualifier}Poll] where PollGroupID = @PollGroupID   
            DELETE FROM  [{databaseSchema}].[{objectQualifier}pollvote] WHERE PollID IN (SELECT PollID FROM @polllist)
            DELETE FROM  [{databaseSchema}].[{objectQualifier}choice] WHERE PollID IN (SELECT PollID FROM @polllist)	
            DELETE FROM  [{databaseSchema}].[{objectQualifier}poll] WHERE PollGroupID = @PollGroupID 
            DELETE FROM  [{databaseSchema}].[{objectQualifier}PollGroupCluster] WHERE PollGroupID = @PollGroupID		
    end

    -- don't remove cluster if the polls are not removed from db 
    end
GO

create procedure [{databaseSchema}].[{objectQualifier}pollgroup_attach](@PollGroupID int, @TopicID int = null, @ForumID int = null, @CategoryID int = null, @BoardID int = null) as
begin
                   -- this deletes possible polls without choices it should not normally happen
                   DECLARE @tablett table (PollID int) 
                   INSERT INTO @tablett(PollID)
                   SELECT PollID FROM [{databaseSchema}].[{objectQualifier}Poll] WHERE PollGroupID = NULL
                  
                   DELETE FROM [{databaseSchema}].[{objectQualifier}PollVote] WHERE PollID IN (select PollID FROM @tablett)
                   DELETE FROM [{databaseSchema}].[{objectQualifier}Choice] WHERE PollID IN (select PollID FROM @tablett)
                   DELETE FROM [{databaseSchema}].[{objectQualifier}Poll] WHERE PollID IN (select PollID FROM @tablett)
                                   
                   if NOT EXISTS (SELECT top 1 1 FROM @tablett)
                   begin
                   if @TopicID > 0
                   begin
                   if exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID  and PollID is not null)
                   begin
                   SELECT 1
                   end
                   else
                   begin
                   Update [{databaseSchema}].[{objectQualifier}Topic] set PollID = @PollGroupID where TopicID = @TopicID 
                   SELECT 0
                   end
                   end              
                  
                   if @ForumID > 0
                   begin
                   if exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumID and PollGroupID is not null)
                   begin
                   SELECT 1
                   end
                   else
                   begin
                   Update [{databaseSchema}].[{objectQualifier}Forum] set PollGroupID = @PollGroupID where ForumID = @ForumID
                   SELECT 0
                   end
                   end

                   if @CategoryID > 0
                   begin
                   if exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}Category] where CategoryID = @CategoryID and PollGroupID is null)
                   begin
                   SELECT 1
                   end
                   else
                   begin
                   Update [{databaseSchema}].[{objectQualifier}Category] set PollGroupID = @PollGroupID where CategoryID = @CategoryID
                   SELECT 0
                   end
                   end
                   end
                   SELECT 1
                       

end
GO

create procedure [{databaseSchema}].[{objectQualifier}pollgroup_list](@UserID int, @ForumID int = null, @BoardID int) as
begin
    select distinct(p.Question), p.PollGroupID from [{databaseSchema}].[{objectQualifier}Poll] p
    LEFT JOIN 	[{databaseSchema}].[{objectQualifier}PollGroupCluster] pgc ON pgc.PollGroupID = p.PollGroupID
    WHERE p.PollGroupID is not null
    -- WHERE p.Closes IS NULL OR p.Closes > @UTCTIMESTAMP
    order by Question asc
end
GO


create procedure [{databaseSchema}].[{objectQualifier}topic_findnext](@TopicID int) as
begin
        declare @LastPosted datetime
    declare @ForumID int
    select @LastPosted = LastPosted, @ForumID = ForumID from [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID AND TopicMovedID IS NULL
    select top 1 TopicID from [{databaseSchema}].[{objectQualifier}Topic] where LastPosted>@LastPosted and ForumID = @ForumID AND IsDeleted=0 AND TopicMovedID IS NULL order by LastPosted asc
end
GO

create procedure [{databaseSchema}].[{objectQualifier}topic_findprev](@TopicID int) AS 
BEGIN
        DECLARE @LastPosted datetime
    DECLARE @ForumID int
    SELECT @LastPosted = LastPosted, @ForumID = ForumID FROM [{databaseSchema}].[{objectQualifier}Topic] WHERE TopicID = @TopicID AND TopicMovedID IS NULL
    SELECT TOP 1 TopicID from [{databaseSchema}].[{objectQualifier}Topic] where LastPosted<@LastPosted AND ForumID = @ForumID AND IsDeleted=0 AND TopicMovedID IS NULL ORDER BY LastPosted DESC
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_info]
(
    @TopicID int,	
    @ShowDeleted bit = 0,
    @GetTags bit = 0
)
AS
BEGIN
    IF @TopicID = 0 SET @TopicID = NULL

    IF @TopicID IS NULL
    BEGIN
        IF @ShowDeleted = 1 

            SELECT t.*,
            (CASE WHEN @GetTags = 1 THEN [{databaseSchema}].[{objectQualifier}topic_gettags_str](t.TopicID) ELSE '' END) as TopicTags FROM [{databaseSchema}].[{objectQualifier}Topic] t
        ELSE
            SELECT t.*,
            (CASE WHEN @GetTags = 1 THEN [{databaseSchema}].[{objectQualifier}topic_gettags_str](t.TopicID) ELSE '' END) as TopicTags FROM [{databaseSchema}].[{objectQualifier}Topic] t WHERE IsDeleted=0
    END
    ELSE
    BEGIN
        IF @ShowDeleted = 1 
		   BEGIN
            SELECT t.*,
            (CASE WHEN @GetTags = 1 THEN [{databaseSchema}].[{objectQualifier}topic_gettags_str](t.TopicID) ELSE '' END) as TopicTags FROM [{databaseSchema}].[{objectQualifier}Topic] t WHERE TopicID = @TopicID 
           END
	    ELSE
            SELECT t.*,
            (CASE WHEN @GetTags = 1 THEN [{databaseSchema}].[{objectQualifier}topic_gettags_str](t.TopicID) ELSE '' END) as TopicTags FROM [{databaseSchema}].[{objectQualifier}Topic] t WHERE TopicID = @TopicID AND IsDeleted=0		
    END
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_findduplicate]
(
    @TopicName nvarchar(255)
)
AS
BEGIN
    IF @TopicName IS NOT NULL
    BEGIN	
        IF EXISTS (SELECT TOP 1 1 FROM [{databaseSchema}].[{objectQualifier}Topic] WHERE [Topic] LIKE  @TopicName AND TopicMovedID IS NULL)
        SELECT 1
        ELSE
        SELECT 0
    END
    ELSE
    BEGIN
        SELECT 0
    END	
END
GO


CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_announcements]
(
    @BoardID int,
    @NumPosts int,
    @PageUserID int
)
AS
BEGIN
        DECLARE @SQL nvarchar(500)

    SET @SQL = 'SELECT DISTINCT TOP ' + convert(varchar, @NumPosts) + ' t.Topic, t.LastPosted, t.Posted, t.TopicID, t.LastMessageID, t.LastMessageFlags FROM'
    SET @SQL = @SQL + ' [{databaseSchema}].[{objectQualifier}Topic] t INNER JOIN [{databaseSchema}].[{objectQualifier}Category] c INNER JOIN [{databaseSchema}].[{objectQualifier}Forum] f ON c.CategoryID = f.CategoryID ON t.ForumID = f.ForumID'
    SET @SQL = @SQL + ' join [{databaseSchema}].[{objectQualifier}ActiveAccess] v on v.ForumID=f.ForumID'
    SET @SQL = @SQL + ' WHERE c.BoardID = ' + convert(varchar, @BoardID) + ' AND v.UserID=' + convert(varchar,@PageUserID) + ' AND (CONVERT(int,v.ReadAccess) <> 0 or (f.Flags & 2) = 0) AND t.IsDeleted=0 AND t.TopicMovedID IS NULL AND (t.Priority = 2) ORDER BY t.LastPosted DESC'

    EXEC(@SQL)	

END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}rss_topic_latest]
(
    @BoardID int,
    @NumPosts int,
    @PageUserID int,
    @StyledNicks bit = 0,
    @ShowNoCountPosts  bit = 0
)
AS
BEGIN	
    
    SET ROWCOUNT @NumPosts
    SELECT
        LastMessage = m.[Message],
        t.LastPosted,
        t.ForumID,
        f.Name as Forum,
        t.Topic,
        t.TopicID,
        t.TopicMovedID,
        t.UserID,
        t.UserName,
        t.UserDisplayName,
        StarterIsGuest = (select x.IsGuest from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=t.UserID),		
        t.LastMessageID,
        t.LastMessageFlags,
        t.LastUserID,			
        LastUserName = IsNull(t.LastUserName,(select x.[Name] from [{databaseSchema}].[{objectQualifier}User] x where x.UserID = t.LastUserID)),
        LastUserDisplayName = IsNull(t.LastUserName,(select x.[DisplayName] from [{databaseSchema}].[{objectQualifier}User] x where x.UserID = t.LastUserID)),
        LastUserIsGuest = (select x.IsGuest from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=t.LastUserID),	
        t.Posted					
    FROM
        [{databaseSchema}].[{objectQualifier}Message] m 
    INNER JOIN	
        [{databaseSchema}].[{objectQualifier}Topic] t  ON t.LastMessageID = m.MessageID
    INNER JOIN
        [{databaseSchema}].[{objectQualifier}Forum] f ON t.ForumID = f.ForumID	
    INNER JOIN
        [{databaseSchema}].[{objectQualifier}Category] c ON c.CategoryID = f.CategoryID
    JOIN
        [{databaseSchema}].[{objectQualifier}ActiveAccess] v ON v.ForumID=f.ForumID
    WHERE	
        c.BoardID = @BoardID
        AND t.TopicMovedID is NULL
        AND v.UserID=@PageUserID
        AND (CONVERT(int,v.ReadAccess) <> 0)
        AND t.IsDeleted != 1
        AND t.LastPosted IS NOT NULL
        AND
        f.Flags & 4 <> (CASE WHEN @ShowNoCountPosts > 0 THEN -1 ELSE 4 END)
    ORDER BY
        t.LastPosted DESC;
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_latest]
(
    @BoardID int,
    @NumPosts int,
    @PageUserID int,
    @StyledNicks bit = 0,
    @ShowNoCountPosts  bit = 0,
    @FindLastUnread bit = 0,
	@UTCTIMESTAMP datetime
)
AS
BEGIN	
    
    SET ROWCOUNT @NumPosts
    SELECT
        t.LastPosted,
        t.ForumID,
        f.Name as Forum,
        t.Topic,
        t.Status,
        t.Styles,
        t.TopicID,
        t.TopicMovedID,
        t.UserID,
        UserName = IsNull(t.UserName,(select x.[Name] from [{databaseSchema}].[{objectQualifier}User] x where x.UserID = t.UserID)),
        UserDisplayName = IsNull(t.UserDisplayName,(select x.[DisplayName] from [{databaseSchema}].[{objectQualifier}User] x where x.UserID = t.UserID)),		
        t.LastMessageID,
        t.LastMessageFlags,
        t.LastUserID,
        t.NumPosts,
        t.Posted,		
        LastUserName = IsNull(t.LastUserName,(select x.[Name] from [{databaseSchema}].[{objectQualifier}User] x where x.UserID = t.LastUserID)),
        LastUserDisplayName = IsNull(t.LastUserDisplayName,(select x.[DisplayName] from [{databaseSchema}].[{objectQualifier}User] x where x.UserID = t.LastUserID)),
        LastUserStyle = case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = t.LastUserID)
            else ''	 end,
        LastForumAccess = case(@FindLastUnread)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x WHERE x.ForumID=f.ForumID AND x.UserID = @PageUserID)
             else ''	 end,
        LastTopicAccess = case(@FindLastUnread)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y WHERE y.TopicID=t.TopicID AND y.UserID = @PageUserID)
             else ''	 end
            
    FROM	
        [{databaseSchema}].[{objectQualifier}Topic] t 
    INNER JOIN
        [{databaseSchema}].[{objectQualifier}Forum] f ON t.ForumID = f.ForumID	
    INNER JOIN
        [{databaseSchema}].[{objectQualifier}Category] c ON c.CategoryID = f.CategoryID
    JOIN
        [{databaseSchema}].[{objectQualifier}ActiveAccess] v ON v.ForumID=f.ForumID
    WHERE	
        c.BoardID = @BoardID
        AND t.TopicMovedID is NULL
        AND v.UserID=@PageUserID
        AND (CONVERT(int,v.ReadAccess) <> 0)
        AND t.IsDeleted != 1
        AND t.LastPosted IS NOT NULL
        AND
        f.Flags & 4 <> (CASE WHEN @ShowNoCountPosts > 0 THEN -1 ELSE 4 END)
    ORDER BY
        t.LastPosted DESC;
END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}announcements_list]
(
    @ForumID int,
    @UserID int = null,
    @SinceDate datetime=null,
    @ToDate datetime=null,
    @PageIndex int = 1, 
    @PageSize int = 0, 
    @StyledNicks bit = 0,
    @ShowMoved  bit = 0,
	@ShowDeleted bit = 0,
    @FindLastRead bit = 0,
    @GetTags bit = 0,
	@UTCTIMESTAMP datetime
)
AS
begin
   declare @TotalRows int
   declare @FirstSelectRowNumber int
   declare @LastSelectRowNumber int
   
   -- find total returned count
   select @TotalRows = COUNT(c.TopicID)
   FROM [{databaseSchema}].[{objectQualifier}Topic] c
   WHERE c.ForumID = @ForumID
   AND	c.[Priority] = 2
   AND	(@ShowDeleted = 1 or c.IsDeleted = 0)
    AND	(c.TopicMovedID IS NOT NULL OR c.NumPosts > 0)
    AND
    (@ShowMoved = 1 or (@ShowMoved <> 1 AND c.TopicMovedID IS NULL))
    
    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
    
    with TopicIds as
     (
     select ROW_NUMBER() over (order by tt.[Priority] desc,tt.LastPosted desc) as RowNum, tt.TopicID
     from [{databaseSchema}].[{objectQualifier}Topic] tt
     where tt.ForumID = @ForumID and tt.[Priority] = 2
      AND	(@ShowDeleted = 1 or tt.IsDeleted = 0)
      AND	((tt.TopicMovedID IS NOT NULL) OR (tt.NumPosts > 0))
      AND
      (@ShowMoved = 1 or (@ShowMoved <> 1 AND TopicMovedID IS NULL))
      )	
	  select
         c.ForumID,
            c.TopicID,
            c.Posted,
            LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),
            c.TopicMovedID,
            FavoriteCount = (SELECT COUNT(1) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = IsNull(c.TopicMovedID,c.TopicID)),
            [Subject] = c.Topic,
            c.[Description],
            c.[Status],
            c.[Styles],
            c.UserID,
            Starter = IsNull(c.UserName,b.Name),
            StarterDisplay = IsNull(c.UserDisplayName,b.DisplayName),
            Replies = c.NumPosts - 1,
            NumPostsDeleted = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] mes WHERE mes.TopicID = c.TopicID AND mes.IsDeleted = 1 AND mes.IsApproved = 1 AND ((@UserID IS NOT NULL AND mes.UserID = @UserID) OR (@UserID IS NULL)) ),			
            [Views] = c.[Views],
            LastPosted = c.LastPosted,
            LastUserID = c.LastUserID,
            LastUserName = IsNull(c.LastUserName,(SELECT x.Name FROM [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
            LastUserDisplayName = IsNull(c.LastUserDisplayName,(SELECT x.DisplayName FROM [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
            LastMessageID = c.LastMessageID,
            LastTopicID = c.TopicID,
            TopicFlags = c.Flags,
            c.Priority,
            c.PollID,
            ForumFlags = d.Flags,
            FirstMessage = (SELECT TOP 1 CAST([Message] as nvarchar(1000)) FROM [{databaseSchema}].[{objectQualifier}Message] mes2 where mes2.TopicID = IsNull(c.TopicMovedID,c.TopicID) AND mes2.Position = 0),
            StarterStyle = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end,
                    LastUserStyle= case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = c.LastUserID)
            else ''	 end,
            LastForumAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x WHERE x.ForumID=c.ForumID AND x.UserID = c.UserID)
             else ''	 end,
            LastTopicAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y WHERE y.TopicID=c.TopicID AND y.UserID = c.UserID)
             else ''	 end,
            (case(@GetTags)
            when 1 then
            [{databaseSchema}].[{objectQualifier}topic_gettags_str](c.TopicID) else '' end) as TopicTags,				
            c.TopicImage,
            c.TopicImageType,
            c.TopicImageBin,
            0 as HasAttachments, 
            TotalRows = @TotalRows,
            PageIndex = @PageIndex
            from
            TopicIds ti
            inner join [{databaseSchema}].[{objectQualifier}Topic] c	
            ON c.TopicID = ti.TopicID
            JOIN [{databaseSchema}].[{objectQualifier}User] b
            ON b.UserID=c.UserID
            join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
            WHERE ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC    
end
GO
CREATE procedure [{databaseSchema}].[{objectQualifier}topic_list]
(
    @ForumID int,
    @UserID int = null,
    @SinceDate datetime=null,
    @ToDate datetime=null,
    @PageIndex int = 1, 
    @PageSize int = 0, 
    @StyledNicks bit = 0,
    @ShowMoved  bit = 0,
	@ShowDeleted bit = 0,
    @FindLastRead bit = 0,
    @GetTags bit = 0,
	@UTCTIMESTAMP datetime
)
AS
begin
   declare @TotalRows int
   declare @FirstSelectRowNumber int
   declare @LastSelectRowNumber int
   
   -- find total returned count
   select @TotalRows = COUNT(c.TopicID)
   FROM [{databaseSchema}].[{objectQualifier}Topic] c
   WHERE c.ForumID = @ForumID
   AND	((c.Priority = 1) OR (c.Priority <=0 AND c.LastPosted>=@SinceDate ))
   AND	(@ShowDeleted = 1 or c.IsDeleted = 0)
    AND	(c.TopicMovedID IS NOT NULL OR c.NumPosts > 0)
    AND
    (@ShowMoved = 1 or (@ShowMoved <> 1 AND c.TopicMovedID IS NULL))
    
    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
    
    with TopicIds as
     (
     select ROW_NUMBER() over (order by tt.[Priority] desc,tt.LastPosted desc) as RowNum, tt.TopicID
     from [{databaseSchema}].[{objectQualifier}Topic] tt
     where tt.ForumID = @ForumID and (tt.[Priority] = 1 OR (tt.[Priority] <=0 AND tt.LastPosted >=@SinceDate))
       AND	(@ShowDeleted = 1 or tt.IsDeleted = 0)
      AND	((tt.TopicMovedID IS NOT NULL) OR (tt.NumPosts > 0))
      AND
      (@ShowMoved = 1 or (@ShowMoved <> 1 AND TopicMovedID IS NULL))
      )	
	  select
         c.ForumID,
            c.TopicID,
            c.Posted,
            LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),
            c.TopicMovedID,
            FavoriteCount = (SELECT COUNT(1) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = IsNull(c.TopicMovedID,c.TopicID)),
            [Subject] = c.Topic,
            c.[Description],
            c.[Status],
            c.[Styles],
            c.UserID,
            Starter = IsNull(c.UserName,b.Name),
            StarterDisplay = IsNull(c.UserDisplayName,b.DisplayName),
            Replies = c.NumPosts - 1,
            NumPostsDeleted = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] mes WHERE mes.TopicID = c.TopicID AND mes.IsDeleted = 1 AND mes.IsApproved = 1 AND ((@UserID IS NOT NULL AND mes.UserID = @UserID) OR (@UserID IS NULL)) ),			
            [Views] = c.[Views],
            LastPosted = c.LastPosted,
            LastUserID = c.LastUserID,
            LastUserName = IsNull(c.LastUserName,(SELECT x.Name FROM [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
            LastUserDisplayName = IsNull(c.LastUserDisplayName,(SELECT x.DisplayName FROM [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
            LastMessageID = c.LastMessageID,
            LastTopicID = c.TopicID,
            LinkDate = c.LinkDate,
            TopicFlags = c.Flags,
            c.Priority,
            c.PollID,
            ForumFlags = d.Flags,
            FirstMessage = (SELECT TOP 1 CAST([Message] as nvarchar(1000)) FROM [{databaseSchema}].[{objectQualifier}Message] mes2 where mes2.TopicID = IsNull(c.TopicMovedID,c.TopicID) AND mes2.Position = 0),
            StarterStyle = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end,
            LastUserStyle= case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = c.LastUserID)
            else ''	 end,
            LastForumAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x WHERE x.ForumID=c.ForumID AND x.UserID = @UserID)
             else ''	 end,
            LastTopicAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y WHERE y.TopicID=c.TopicID AND y.UserID = @UserID)
             else ''	 end,	
            (case(@GetTags)
            when 1 then
            [{databaseSchema}].[{objectQualifier}topic_gettags_str](c.TopicID) else '' end) as TopicTags,		
            c.TopicImage,
            c.TopicImageType,
            c.TopicImageBin,
            0 as HasAttachments, 
            TotalRows = @TotalRows,
            PageIndex = @PageIndex
            from
            TopicIds ti
            inner join [{databaseSchema}].[{objectQualifier}Topic] c	
            ON c.TopicID = ti.TopicID
            JOIN [{databaseSchema}].[{objectQualifier}User] b
            ON b.UserID=c.UserID
            join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
            WHERE ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC
end
GO

create procedure [{databaseSchema}].[{objectQualifier}topic_listmessages](@TopicID int) as
begin
   select 
        a.MessageID,
        a.UserID,
        UserName = b.Name,
        UserDisplayName = b.DisplayName,
        a.[Message],
        c.TopicID,
        c.ForumID,
        c.Topic,
        c.Priority,
        c.Description,
        c.Status,
        c.Styles,
        a.Flags,
        c.UserID AS TopicOwnerID,
        Edited = IsNull(a.Edited,a.Posted),
        TopicFlags = c.Flags,
        ForumFlags = d.Flags,
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
    from 
        [{databaseSchema}].[{objectQualifier}Message] a
        inner join [{databaseSchema}].[{objectQualifier}User] b on b.UserID = a.UserID
        inner join [{databaseSchema}].[{objectQualifier}Topic] c on a.TopicID = c.TopicID
        inner join [{databaseSchema}].[{objectQualifier}Forum] d on c.ForumID = d.ForumID
    where a.TopicID = @TopicID
end
GO
create procedure [{databaseSchema}].[{objectQualifier}topic_lock](@TopicID int,@Locked bit) as
begin
        if @Locked<>0
        update [{databaseSchema}].[{objectQualifier}Topic] set Flags = Flags | 1 where TopicID = @TopicID
    else
        update [{databaseSchema}].[{objectQualifier}Topic] set Flags = Flags & ~1 where TopicID = @TopicID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}topic_move](@TopicID int,@ForumID int,@ShowMoved bit, @LinkDays int, @UTCTIMESTAMP datetime) AS
begin
        declare @OldForumID int;		
        declare @newTimestamp datetime;
		declare @MovedTopicID int;

        if @LinkDays > -1
        begin
        SET @newTimestamp = DATEADD(d,@LinkDays,@UTCTIMESTAMP);
        end
    select @OldForumID = ForumID from [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID

    if @ShowMoved <> 0 begin
        -- delete an old link if exists
        delete from [{databaseSchema}].[{objectQualifier}Topic] where TopicMovedID = @TopicID
        -- create a moved message
        insert into [{databaseSchema}].[{objectQualifier}Topic](ForumID,UserID,UserName,UserDisplayName,Posted,Topic,[Views],Flags,Priority,PollID,TopicMovedID,LastPosted,NumPosts,LinkDate)
        select ForumID,UserID,UserName,UserDisplayName,Posted,Topic,0,Flags,Priority,PollID,@TopicID,LastPosted,0,@newTimestamp
        from [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID;
		 set @MovedTopicID = @@IDENTITY;
			 INSERT INTO [{databaseSchema}].[{objectQualifier}TopicTags](TopicID,TagID) 
		 select @MovedTopicID,TagID from [{databaseSchema}].[{objectQualifier}TopicTags] WHERE TopicID = @TopicID;
		 end
		 else
		 begin	
		 delete from [{databaseSchema}].[{objectQualifier}TopicTags] WHERE TopicID = @TopicID;
		 end
		 UPDATE [{databaseSchema}].[{objectQualifier}Tags]	set  TagCount = TagCount - 1 where TagID in (select TagID from 	[{databaseSchema}].[{objectQualifier}TopicTags] WHERE TopicID = @TopicID);
    

    -- move the topic
    update [{databaseSchema}].[{objectQualifier}Topic] set ForumID = @ForumID where TopicID = @TopicID

    -- update last posts
    exec [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @OldForumID
    exec [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @ForumID
    
    -- update stats
    exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @OldForumID
    exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID
    
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_prune](@BoardID int, @ForumID int=null,@Days int, @DeletedOnly bit, @UTCTIMESTAMP datetime) as
BEGIN      
    IF @ForumID = 0 SET @ForumID = NULL;   
           
        SELECT 
            yt.TopicID
        FROM [{databaseSchema}].[{objectQualifier}topic] yt
        INNER JOIN
        [{databaseSchema}].[{objectQualifier}Forum] yf
        ON
        yt.ForumID = yf.ForumID
        INNER JOIN
        [{databaseSchema}].[{objectQualifier}Category] yc
        ON
        yf.CategoryID = yc.CategoryID
        WHERE
            yc.BoardID = @BoardID AND
            (@ForumID IS NULL OR yt.ForumID = @ForumID) AND
            yt.Priority = 0 AND
            (yt.Flags & 512) = 0 AND /* not flagged as persistent */
            datediff(dd,yt.LastPosted,@UTCTIMESTAMP )>@Days AND (@DeletedOnly = 0 OR (yt.Flags & 8) = 8);
END
GO

create procedure [{databaseSchema}].[{objectQualifier}topic_save](
    @ForumID	int,
    @Subject	nvarchar(100),
	@Status 	nvarchar(255)=null,
	@Styles 	nvarchar(255)=null,
    @Description	nvarchar(255)=null,
    @UserID		int,
    @Message	ntext, 
    @Priority	smallint,
    @UserName	nvarchar(255)=null,
    @IP			varchar(39),
    @Posted		datetime=null,
    @BlogPostID	nvarchar(50),
    @Flags		int, 
	@MessageDescription nvarchar(255),
    @Tags nvarchar(1024),
    @UTCTIMESTAMP datetime
) as
begin
        declare @TopicID int
    declare @MessageID int, @OverrideDisplayName BIT, @ReplaceName nvarchar(255)

    if @Posted is null set @Posted = @UTCTIMESTAMP 
        -- this check is for guest user only to not override replace name 
    if (SELECT Name FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserID = @UserID) != @UserName
    begin
    SET @OverrideDisplayName = 1
    end	
    SET @ReplaceName = (CASE WHEN @OverrideDisplayName = 1 THEN @UserName ELSE (SELECT DisplayName FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserID = @UserID) END);
    -- create the topic
    insert into [{databaseSchema}].[{objectQualifier}Topic](ForumID,Topic,UserID,Posted,[Views],[Priority],UserName,UserDisplayName,NumPosts, [Description], [Status], [Styles])
    values(@ForumID,@Subject,@UserID,@Posted,0,@Priority,@UserName,@ReplaceName, 0,@Description, @Status, @Styles)

    -- get its id
    set @TopicID = SCOPE_IDENTITY()
    
    -- add message to the topic
    exec [{databaseSchema}].[{objectQualifier}message_save] @TopicID,@UserID,@Message,@UserName,@IP,@Posted,null,@BlogPostID,null,null,null,@Flags,@UTCTIMESTAMP,@MessageID output

    exec  [{databaseSchema}].[{objectQualifier}topic_tagsave] @TopicID,@Tags

    select TopicID = @TopicID, MessageID = @MessageID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}topic_updatelastpost]
(@ForumID int=null,@TopicID int=null) as
begin
        if @TopicID is not null
        update [{databaseSchema}].[{objectQualifier}Topic] set
            LastPosted = (select top 1 x.Posted from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastMessageID = (select top 1 x.MessageID from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastUserID = (select top 1 x.UserID from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastUserName = (select top 1 x.UserName from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastUserDisplayName = (select top 1 x.UserDisplayName from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastMessageFlags = (select top 1 x.Flags from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc)
        where TopicID = @TopicID
    else
        update [{databaseSchema}].[{objectQualifier}Topic] set
            LastPosted = (select top 1 x.Posted from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastMessageID = (select top 1 x.MessageID from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastUserID = (select top 1 x.UserID from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastUserName = (select top 1 x.UserName from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastUserDisplayName = (select top 1 x.UserDisplayName from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc),
            LastMessageFlags = (select top 1 x.Flags from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and (x.Flags & 24)=16 order by Posted desc)
        where TopicMovedID is null
        and (@ForumID is null or ForumID=@ForumID)

    exec [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @ForumID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}topic_updatetopic]
(@TopicID int,@Topic nvarchar (100)) as
begin
        if @TopicID is not null
        update [{databaseSchema}].[{objectQualifier}Topic] set
            Topic = @Topic
        where TopicID = @TopicID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_accessmasks](@BoardID int,@UserID int) as
begin
        
    select * from(
        select
            AccessMaskID	= e.AccessMaskID,
            AccessMaskName	= e.Name,
            ForumID			= f.ForumID,
            ForumName		= f.Name,
            CategoryID		= f.CategoryID,
            ParentID		= f.ParentID
        from
            [{databaseSchema}].[{objectQualifier}User] a 
            join [{databaseSchema}].[{objectQualifier}UserGroup] b on b.UserID=a.UserID
            join [{databaseSchema}].[{objectQualifier}Group] c on c.GroupID=b.GroupID
            join [{databaseSchema}].[{objectQualifier}ForumAccess] d on d.GroupID=c.GroupID
            join [{databaseSchema}].[{objectQualifier}AccessMask] e on e.AccessMaskID=d.AccessMaskID
            join [{databaseSchema}].[{objectQualifier}Forum] f on f.ForumID=d.ForumID
        where
            a.UserID=@UserID and
            c.BoardID=@BoardID
        group by
            e.AccessMaskID,
            e.Name,
            f.ForumID,
            f.ParentID,
            f.CategoryID,
            f.Name
        
        union
            
        select
            AccessMaskID	= c.AccessMaskID,
            AccessMaskName	= c.Name,
            ForumID			= d.ForumID,
            ForumName		= d.Name,
            CategoryID		= d.CategoryID,
            ParentID		= d.ParentID
        from
            [{databaseSchema}].[{objectQualifier}User] a 
            join [{databaseSchema}].[{objectQualifier}UserForum] b on b.UserID=a.UserID
            join [{databaseSchema}].[{objectQualifier}AccessMask] c on c.AccessMaskID=b.AccessMaskID
            join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=b.ForumID
        where
            a.UserID=@UserID and
            c.BoardID=@BoardID
        group by
            c.AccessMaskID,
            c.Name,
            d.ForumID,
            d.ParentID,
            d.CategoryID,
            d.Name
    ) as x
    order by
        ForumName, AccessMaskName
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_accessmasksbyforum](@BoardID int,@UserID int) as
begin
        
    select * from(
        select
            AccessMaskID	= e.AccessMaskID,
            AccessMaskName	= e.Name,
            e.Flags AS AccessMaskFlags,
            0 as IsUserMask,
            ForumID			= f.ForumID,
            ForumName		= f.Name,
            CategoryID		= f.CategoryID,
            ParentID		= f.ParentID,
            c.GroupID,
            c.Name	as GroupName
        from
            [{databaseSchema}].[{objectQualifier}User] a 
            join [{databaseSchema}].[{objectQualifier}UserGroup] b on b.UserID=a.UserID
            join [{databaseSchema}].[{objectQualifier}Group] c on c.GroupID=b.GroupID
            join [{databaseSchema}].[{objectQualifier}ForumAccess] d on d.GroupID=c.GroupID
            join [{databaseSchema}].[{objectQualifier}AccessMask] e on e.AccessMaskID=d.AccessMaskID
            join [{databaseSchema}].[{objectQualifier}Forum] f on f.ForumID=d.ForumID
        where
            a.UserID=@UserID and
            c.BoardID=@BoardID    
        union
            
        select
            AccessMaskID	= c.AccessMaskID,
            AccessMaskName	= c.Name,
            c.Flags AS AccessMaskFlags,
            1 as IsUserMask,
            ForumID			= d.ForumID,
            ForumName		= d.Name,
            CategoryID		= d.CategoryID,
            ParentID		= d.ParentID,
            0 AS GroupID,
            ''	as GroupName
        from
            [{databaseSchema}].[{objectQualifier}User] a 
            join [{databaseSchema}].[{objectQualifier}UserForum] b on b.UserID=a.UserID
            join [{databaseSchema}].[{objectQualifier}AccessMask] c on c.AccessMaskID=b.AccessMaskID
            join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=b.ForumID
        where
            a.UserID=@UserID and
            c.BoardID=@BoardID      
    ) as x   
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_activity_rank]
(
    @BoardID AS int,
    @DisplayNumber AS int,
    @StartDate AS datetime
)
AS
BEGIN
        
    DECLARE @GuestUserID int
    DECLARE @AllIntervalPostCount int	

    SET @GuestUserID =
    (SELECT top 1
        a.UserID
    from
        [{databaseSchema}].[{objectQualifier}User] a
        inner join [{databaseSchema}].[{objectQualifier}UserGroup] b on b.UserID = a.UserID
        inner join [{databaseSchema}].[{objectQualifier}Group] c on b.GroupID = c.GroupID
    where
        a.BoardID = @BoardID and
        (c.Flags & 2)<>0
    )

    SET @AllIntervalPostCount =(SELECT  Count(m.UserID) FROM [{databaseSchema}].[{objectQualifier}Message] m
            WHERE m.Posted >= @StartDate)

    SET ROWCOUNT @DisplayNumber	

    SELECT
        counter.[ID],
        u.[Name],
        u.[DisplayName],
        u.[Joined],
        u.[UserStyle],
        u.[IsActiveExcluded] AS IsHidden,
        counter.[NumOfPosts],
        NumOfAllIntervalPosts = @AllIntervalPostCount 
    FROM
        [{databaseSchema}].[{objectQualifier}User] u inner join
        (
            SELECT m.UserID as ID, Count(m.UserID) as NumOfPosts FROM [{databaseSchema}].[{objectQualifier}Message] m
            WHERE m.Posted >= @StartDate
            GROUP BY m.UserID
        ) AS counter ON u.UserID = counter.ID
    WHERE
        u.BoardID = @BoardID and u.UserID != @GuestUserID
    ORDER BY
        NumOfPosts DESC

    SET ROWCOUNT 0
END
GO


create PROCEDURE [{databaseSchema}].[{objectQualifier}user_addpoints] (@UserID int,@FromUserID int = null, @UTCTIMESTAMP datetime, @Points int) AS
BEGIN
    UPDATE [{databaseSchema}].[{objectQualifier}User] SET Points = Points + @Points WHERE UserID = @UserID

    IF @FromUserID IS NOT NULL 
    BEGIN
        declare	@VoteDate datetime
    set @VoteDate = (select top 1 VoteDate from [{databaseSchema}].[{objectQualifier}ReputationVote] where ReputationFromUserID=@FromUserID AND ReputationToUserID=@UserID)
    IF @VoteDate is not null
    begin	     
          update [{databaseSchema}].[{objectQualifier}ReputationVote] set VoteDate=@UTCTIMESTAMP where VoteDate = @VoteDate AND ReputationFromUserID=@FromUserID AND ReputationToUserID=@UserID
    end
    ELSE
      begin
          insert into [{databaseSchema}].[{objectQualifier}ReputationVote](ReputationFromUserID,ReputationToUserID,VoteDate)
          values (@FromUserID, @UserID, @UTCTIMESTAMP)
      end
    END
END

GO

create procedure [{databaseSchema}].[{objectQualifier}user_adminsave]
(@BoardID int,@UserID int,@Name nvarchar(255),@DisplayName nvarchar(255), @Email nvarchar(255),@Flags int,@RankID int) as
begin
        
    update [{databaseSchema}].[{objectQualifier}User] set
        Name = @Name,
        DisplayName = @DisplayName,
        Email = @Email,
        RankID = @RankID,
        Flags = @Flags
    where UserID = @UserID
    select UserID = @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_approve](@UserID int) as
begin
        
    declare @CheckEmailID int
    declare @Email nvarchar(255)

    select 
        @CheckEmailID = CheckEmailID,
        @Email = Email
    from
        [{databaseSchema}].[{objectQualifier}CheckEmail]
    where
        UserID = @UserID

    -- Update new user email
    update [{databaseSchema}].[{objectQualifier}User] set Email = @Email, Flags = Flags | 2 where UserID = @UserID
    delete [{databaseSchema}].[{objectQualifier}CheckEmail] where CheckEmailID = @CheckEmailID
    select convert(bit,1)
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}user_approveall](@BoardID int) as
begin
        
    DECLARE userslist CURSOR FOR 
        SELECT UserID FROM [{databaseSchema}].[{objectQualifier}User] WHERE BoardID=@BoardID AND (Flags & 2)=0
        FOR READ ONLY


    OPEN userslist

    DECLARE @UserID int

    FETCH userslist INTO @UserID

    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC [{databaseSchema}].[{objectQualifier}user_approve] @UserID
        FETCH userslist INTO @UserID		
    END

    CLOSE userslist

end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_aspnet](@BoardID int,@UserName nvarchar(255),@DisplayName nvarchar(255) = null,@Email nvarchar(255),@ProviderUserKey nvarchar(64),@IsApproved bit,@UTCTIMESTAMP datetime) as
BEGIN
        SET NOCOUNT ON

    DECLARE @UserID int, @RankID int, @approvedFlag int, @timezone int

    SET @approvedFlag = 0;
    IF (@IsApproved = 1) SET @approvedFlag = 2;	
    
    IF EXISTS(SELECT 1 FROM [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID and ([ProviderUserKey]=@ProviderUserKey OR [Name] = @UserName))
    BEGIN
        SELECT TOP 1 @UserID = UserID FROM [{databaseSchema}].[{objectQualifier}User] WHERE [BoardID]=@BoardID and ([ProviderUserKey]=@ProviderUserKey OR [Name] = @UserName)
        
        IF (@DisplayName IS NULL) 
        BEGIN
            SELECT TOP 1 @DisplayName = DisplayName FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserId = @UserID
        END

        UPDATE [{databaseSchema}].[{objectQualifier}User] SET 
            [Name] = @UserName,
            DisplayName = @DisplayName,
            Email = @Email,
            [ProviderUserKey] = @ProviderUserKey,
            Flags = Flags | @approvedFlag
        WHERE
            UserID = @UserID
    END ELSE
    BEGIN
        SELECT @RankID = RankID from [{databaseSchema}].[{objectQualifier}Rank] where (Flags & 1)<>0 and BoardID=@BoardID
        
        IF (@DisplayName IS NULL) 
        BEGIN
            SET @DisplayName = @UserName
        END		

        SET @TimeZone = (SELECT ISNULL(CAST([{databaseSchema}].[{objectQualifier}registry_value]('TimeZone', @BoardID) as int), 0))

        INSERT INTO [{databaseSchema}].[{objectQualifier}User](BoardID,RankID,[Name],DisplayName,Password,Email,Joined,LastVisit,NumPosts,TimeZone,Flags,ProviderUserKey) 
        VALUES(@BoardID,@RankID,@UserName,@DisplayName,'-',@Email,@UTCTIMESTAMP ,@UTCTIMESTAMP ,0, @timezone,@approvedFlag,@ProviderUserKey)
    
        SET @UserID = SCOPE_IDENTITY()	
    END
    
    SELECT UserID=@UserID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_migrate]
(
    @UserID int,
    @ProviderUserKey nvarchar(64),
    @UpdateProvider bit = 0
)
AS
BEGIN
    
    DECLARE @Password nvarchar(255), @IsApproved bit, @LastActivity datetime, @Joined datetime
    
    UPDATE [{databaseSchema}].[{objectQualifier}User] SET ProviderUserKey = @ProviderUserKey where UserID = @UserID

    IF (@UpdateProvider = 1)
    BEGIN
        SELECT
            @Password = [Password],
            @IsApproved = (CASE (Flags & 2) WHEN 2 THEN 1 ELSE 0 END),
            @LastActivity = LastVisit,
            @Joined = Joined
        FROM
            [{databaseSchema}].[{objectQualifier}User]
        WHERE
            UserID = @UserID
        
        UPDATE
            [{databaseSchema}].[{objectQualifier}prov_Membership]
        SET
            [Password] = @Password,
            PasswordFormat = '1',
            LastActivity = @LastActivity,
            IsApproved = @IsApproved,
            Joined = @Joined
        WHERE
            UserID = @ProviderUserKey
    END
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_avatarimage]
(
    @UserID int
)
AS
BEGIN
    
    SELECT
        UserID,
        AvatarImage,
        AvatarImageType
    FROM
        [{databaseSchema}].[{objectQualifier}User]
    WHERE
        UserID = @UserID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_changepassword](@UserID int,@OldPassword nvarchar(32),@NewPassword nvarchar(32)) as
begin
    
    declare @CurrentOld nvarchar(32)
    select @CurrentOld = Password from [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
    if @CurrentOld<>@OldPassword begin
        select Success = convert(bit,0)
        return
    end
    update [{databaseSchema}].[{objectQualifier}User] set Password = @NewPassword where UserID = @UserID
    select Success = convert(bit,1)
end
GO

CREATE PROC [{databaseSchema}].[{objectQualifier}user_pmcount]
    (@UserID int) 
AS
BEGIN
        DECLARE @CountIn int	
        DECLARE @CountOut int
        DECLARE @CountArchivedIn int		
        DECLARE @plimit1 int        
        DECLARE @pcount int
        
      set @plimit1 = (SELECT TOP 1 (c.PMLimit) FROM [{databaseSchema}].[{objectQualifier}User] a 
                        JOIN [{databaseSchema}].[{objectQualifier}UserGroup] b
                          ON a.UserID = b.UserID
                            JOIN [{databaseSchema}].[{objectQualifier}Group] c                         
                              ON b.GroupID = c.GroupID WHERE a.UserID = @UserID ORDER BY c.PMLimit DESC)
      set @pcount = (SELECT TOP 1 c.PMLimit FROM [{databaseSchema}].[{objectQualifier}Rank] c 
                        JOIN [{databaseSchema}].[{objectQualifier}User] d
                           ON c.RankID = d.RankID WHERE d.UserID = @UserID ORDER BY c.PMLimit DESC)
      if (@plimit1 > @pcount) 
      begin
      set @pcount = @plimit1      
      end 
      
    -- get count of pm's in user's sent items
    
    SELECT 
        @CountOut=COUNT(1) 
    FROM 
        [{databaseSchema}].[{objectQualifier}UserPMessage] a
    INNER JOIN [{databaseSchema}].[{objectQualifier}PMessage] b ON a.PMessageID=b.PMessageID
    WHERE 
        (a.Flags & 2)<>0 AND 
        b.FromUserID = @UserID
    -- get count of pm's in user's  received items
    SELECT 
        @CountIn=COUNT(1) 
    FROM 
    [{databaseSchema}].[{objectQualifier}PMessage] a
    INNER JOIN
    [{databaseSchema}].[{objectQualifier}UserPMessage] b ON a.PMessageID = b.PMessageID
    WHERE b.IsDeleted = 0  
         AND b.IsArchived=0  
         -- ToUserID
         AND b.[UserID] = @UserID
    
    SELECT 
        @CountArchivedIn=COUNT(1) 
    FROM 
    [{databaseSchema}].[{objectQualifier}PMessage] a
    INNER JOIN
    [{databaseSchema}].[{objectQualifier}UserPMessage] b ON a.PMessageID = b.PMessageID
        WHERE
        b.IsArchived <>0 AND
        -- ToUserID
        b.[UserID] = @UserID

    -- return all pm data
    SELECT 
        NumberIn = @CountIn,
        NumberOut =  @CountOut,
        NumberTotal = @CountIn + @CountOut + @CountArchivedIn,
        NumberArchived =@CountArchivedIn,
        NumberAllowed = @pcount
            

END
GO

create procedure [{databaseSchema}].[{objectQualifier}user_delete](@UserID int) as
begin
    
    declare @GuestUserID	int
    declare @UserName		nvarchar(255)
    declare @UserDisplayName		nvarchar(255)
    declare @GuestCount		int

    select @UserName = Name, @UserDisplayName = DisplayName from [{databaseSchema}].[{objectQualifier}User] where UserID=@UserID

    select top 1
        @GuestUserID = a.UserID
    from
        [{databaseSchema}].[{objectQualifier}User] a
        inner join [{databaseSchema}].[{objectQualifier}UserGroup] b on b.UserID = a.UserID
        inner join [{databaseSchema}].[{objectQualifier}Group] c on b.GroupID = c.GroupID
    where
        (c.Flags & 2)<>0

    select 
        @GuestCount = count(1) 
    from 
        [{databaseSchema}].[{objectQualifier}UserGroup] a
        join [{databaseSchema}].[{objectQualifier}Group] b on b.GroupID=a.GroupID
    where
        (b.Flags & 2)<>0

    if @GuestUserID=@UserID and @GuestCount=1 begin
        return
    end

    update [{databaseSchema}].[{objectQualifier}Message] set UserName=@UserName,UserDisplayName=@UserDisplayName,UserID=@GuestUserID,EditedBy=@GuestUserID where UserID=@UserID
    update [{databaseSchema}].[{objectQualifier}Topic] set UserName=@UserName,UserDisplayName=@UserDisplayName,UserID=@GuestUserID where UserID=@UserID
    update [{databaseSchema}].[{objectQualifier}Topic] set LastUserName=@UserName,LastUserDisplayName=@UserDisplayName,LastUserID=@GuestUserID where LastUserID=@UserID
    update [{databaseSchema}].[{objectQualifier}Forum] set LastUserName=@UserName,LastUserDisplayName=@UserDisplayName,LastUserID=@GuestUserID where LastUserID=@UserID

    delete from [{databaseSchema}].[{objectQualifier}Active] where UserID=@UserID
    delete from [{databaseSchema}].[{objectQualifier}EventLog] where UserID=@UserID	
    delete from [{databaseSchema}].[{objectQualifier}UserPMessage] where UserID=@UserID
    delete from [{databaseSchema}].[{objectQualifier}PMessage] where FromUserID=@UserID AND PMessageID NOT IN (select PMessageID FROM [{databaseSchema}].[{objectQualifier}PMessage])
    -- Delete all the thanks entries associated with this UserID.
    delete from [{databaseSchema}].[{objectQualifier}Thanks] where ThanksFromUserID=@UserID OR ThanksToUserID=@UserID
    -- Delete all the FavoriteTopic entries associated with this UserID.
    delete from [{databaseSchema}].[{objectQualifier}FavoriteTopic] where UserID=@UserID
    -- Delete all the Buddy relations between this user and other users.
    delete from [{databaseSchema}].[{objectQualifier}Buddy] where FromUserID=@UserID   
    delete from [{databaseSchema}].[{objectQualifier}Buddy] where ToUserID=@UserID	 
    -- set messages as from guest so the User can be deleted
    update [{databaseSchema}].[{objectQualifier}PMessage] SET FromUserID = @GuestUserID WHERE FromUserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}CheckEmail] where UserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}WatchTopic] where UserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}WatchForum] where UserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}TopicReadTracking] where UserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}ForumReadTracking] where UserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}ReputationVote] where ReputationFromUserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}UserGroup] where UserID = @UserID
    -- ABOT CHANGED
    -- Delete UserForums entries Too 
    delete from [{databaseSchema}].[{objectQualifier}UserForum] where UserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}IgnoreUser] where UserID = @UserID OR IgnoredUserID = @UserID
    --END ABOT CHANGED 09.04.2004
    delete from [{databaseSchema}].[{objectQualifier}AdminPageUserAccess] where UserID = @UserID
    delete from [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}user_deleteavatar](@UserID int) as begin
    
    UPDATE
        [{databaseSchema}].[{objectQualifier}User]
    SET
        AvatarImage = null,
        Avatar = null,
        AvatarImageType = null
    WHERE
        UserID = @UserID
END
GO

create procedure [{databaseSchema}].[{objectQualifier}user_deleteold](@BoardID int, @Days int,@UTCTIMESTAMP datetime) as
begin
    
    declare @Since datetime

    set @Since = @UTCTIMESTAMP 

    delete from [{databaseSchema}].[{objectQualifier}EventLog]  where UserID in(select UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID and IsApproved=0 and datediff(day,Joined,@Since)>@Days)
    delete from [{databaseSchema}].[{objectQualifier}CheckEmail] where UserID in(select UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID and IsApproved=0 and datediff(day,Joined,@Since)>@Days)
    delete from [{databaseSchema}].[{objectQualifier}UserGroup] where UserID in(select UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID and IsApproved=0 and datediff(day,Joined,@Since)>@Days)
    delete from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID and IsApproved=0 and datediff(day,Joined,@Since)>@Days
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_emails](@BoardID int,@GroupID int=null) as
begin
    
    if @GroupID = 0 set @GroupID = null
    if @GroupID is null
        select 
            a.Email 
        from 
            [{databaseSchema}].[{objectQualifier}User] a
        where 
            a.Email is not null and 
            a.BoardID = @BoardID and
            a.Email is not null and 
            a.Email<>''
    else
        select 
            a.Email 
        from 
            [{databaseSchema}].[{objectQualifier}User] a
            join [{databaseSchema}].[{objectQualifier}UserGroup] b on b.UserID=a.UserID
            join [{databaseSchema}].[{objectQualifier}Group] c on c.GroupID=b.GroupID
        where 
            b.GroupID = @GroupID and 
            (c.Flags & 2)=0 and
            a.Email is not null and 
            a.Email<>''
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_find](
    @BoardID int,
    @Filter bit,
    @UserName nvarchar(255)=null,
    @Email nvarchar(255)=null,
    @DisplayName nvarchar(255)=null,
    @NotificationType int = null,
    @DailyDigest bit = null
)
AS
begin
    
    if @Filter<>0
    begin
        if @UserName is not null
            set @UserName = '%' + @UserName + '%'
            
        if @DisplayName is not null
            set @DisplayName = '%' + @DisplayName + '%'			

        select 
            a.*,			
            IsAdmin = (select count(1) from [{databaseSchema}].[{objectQualifier}UserGroup] x join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 1)<>0)
        from 
            [{databaseSchema}].[{objectQualifier}User] a
        where 
            a.BoardID=@BoardID and
            ((@UserName is not null and a.Name like @UserName) or
            (@Email is not null and Email like @Email) or
            (@DisplayName is not null and a.DisplayName like @DisplayName) or
            (@NotificationType is not null and a.NotificationType = @NotificationType) or
            (@DailyDigest is not null and a.DailyDigest = @DailyDigest))
        order by
            a.Name
    end else
    begin
        select 
            a.*,			
            IsAdmin = (select count(1) from [{databaseSchema}].[{objectQualifier}UserGroup] x join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 1)<>0)
        from 
            [{databaseSchema}].[{objectQualifier}User] a
        where 
            a.BoardID=@BoardID and
            ((@UserName is not null and a.Name like @UserName) or
            (@Email is not null and Email like @Email) or
            (@DisplayName is not null and a.DisplayName like @DisplayName) or
            (@NotificationType is not null and a.NotificationType = @NotificationType) or
            (@DailyDigest is not null and a.DailyDigest = @DailyDigest))
    end
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_getpoints] (@UserID int) AS
BEGIN
    
    SELECT Points FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserID = @UserID
END
GO

create procedure [{databaseSchema}].[{objectQualifier}user_getsignature](@UserID int) as
begin
    
    select [Signature] from [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_guest]
(
    @BoardID int
)
as
begin
    
    select top 1
        a.UserID
    from
        [{databaseSchema}].[{objectQualifier}User] a
        inner join [{databaseSchema}].[{objectQualifier}UserGroup] b on b.UserID = a.UserID
        inner join [{databaseSchema}].[{objectQualifier}Group] c on b.GroupID = c.GroupID
    where
        a.BoardID = @BoardID and
        (c.Flags & 2)<>0
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_list](@BoardID int,@UserID int=null,@Approved bit=null,@GroupID int=null,@RankID int=null,@StyledNicks bit = null, @UTCTIMESTAMP datetime) as
begin	
    if @UserID is not null
        select 
        a.UserID,
        a.BoardID,
        a.ProviderUserKey,
        a.[Name],
        a.[DisplayName],
        a.[Password],
        a.[Email],
        a.Joined,
        a.LastVisit,
        a.IP,
        a.NumPosts,
        a.TimeZone,
        a.Avatar,
        a.[Signature],
        a.AvatarImage,
        a.AvatarImageType,
        a.RankID,
        a.Suspended,
        a.LanguageFile,
        a.ThemeFile,
        a.[UseSingleSignOn],
        a.TextEditor,
        a.OverrideDefaultThemes,
        a.[PMNotification],
        a.[AutoWatchTopics],
        a.[DailyDigest],
        a.[NotificationType],
        a.[Flags],
        a.[Points],		
        a.[IsApproved],
        a.[IsGuest],
        a.[IsCaptchaExcluded],
        a.[IsActiveExcluded],
        a.[IsDST],
        a.[IsDirty],
        a.[IsFacebookUser],
        a.[IsTwitterUser],
        a.[Culture],
		a.TopicsPerPage,
		a.PostsPerPage,		
            CultureUser = a.Culture,						
            RankName = b.Name,
            Style = case(@StyledNicks)
            when 1 then a.UserStyle
            else ''	 end, 
            NumDays = datediff(d,a.Joined,@UTCTIMESTAMP )+1,
            NumPostsForum = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.IsApproved = 1 and x.IsDeleted = 0),
            HasAvatarImage = (select count(1) from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=a.UserID and AvatarImage is not null),
            IsAdmin	= IsNull(c.IsAdmin,0),
            IsGuest	= IsNull(a.Flags & 4,0),
            IsHostAdmin	= IsNull(a.Flags & 1,0),
            IsForumModerator	= IsNull(c.IsForumModerator,0),
            IsModerator		= IsNull(c.IsModerator,0)
        from 
            [{databaseSchema}].[{objectQualifier}User] a
            join [{databaseSchema}].[{objectQualifier}Rank] b on b.RankID=a.RankID			
            left join [{databaseSchema}].[{objectQualifier}vaccess] c on c.UserID=a.UserID
        where 
            a.UserID = @UserID and
            a.BoardID = @BoardID and
            IsNull(c.ForumID,0) = 0 and
            (@Approved is null or (@Approved=0 and (a.Flags & 2)=0) or (@Approved=1 and (a.Flags & 2)=2))
        order by 
            a.Name 

    else if @GroupID is null and @RankID is null
        select 
        a.UserID,
        a.BoardID,
        a.ProviderUserKey,
        a.[Name],
        a.[DisplayName],
        a.[Password],
        a.[Email],
        a.Joined,
        a.LastVisit,
        a.IP,
        a.NumPosts,
        a.TimeZone,
        a.Avatar,
        a.[Signature],
        a.AvatarImage,
        a.AvatarImageType,
        a.RankID,
        a.Suspended,
        a.LanguageFile,
        a.ThemeFile,
        a.[UseSingleSignOn],
        a.TextEditor,
        a.OverrideDefaultThemes,
        a.[PMNotification],
        a.[AutoWatchTopics],
        a.[DailyDigest],
        a.[NotificationType],
        a.[Flags],
        a.[Points],		
        a.[IsApproved],
        a.[IsGuest],
        a.[IsCaptchaExcluded],
        a.[IsActiveExcluded],
        a.[IsDST],
        a.[IsDirty],
        a.[IsFacebookUser],
        a.[IsTwitterUser],
        a.[Culture],			
            CultureUser = a.Culture,	
            Style = case(@StyledNicks)
            when 1 then a.UserStyle
            else ''	 end, 	
            IsAdmin = (select count(1) from [{databaseSchema}].[{objectQualifier}UserGroup] x join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 1)<>0),
            IsGuest	= IsNull(a.Flags & 4,0),
            IsHostAdmin	= IsNull(a.Flags & 1,0),		
            RankName = b.Name
        from 
            [{databaseSchema}].[{objectQualifier}User] a
            join [{databaseSchema}].[{objectQualifier}Rank] b on b.RankID=a.RankID			
        where 
            a.BoardID = @BoardID and
            (@Approved is null or (@Approved=0 and (a.Flags & 2)=0) or (@Approved=1 and (a.Flags & 2)=2))
        order by 
            a.Name
    else
        select 
        a.UserID,
        a.BoardID,
        a.ProviderUserKey,
        a.[Name],
        a.[DisplayName],
        a.[Password],
        a.[Email],
        a.Joined,
        a.LastVisit,
        a.IP,
        a.NumPosts,
        a.TimeZone,
        a.Avatar,
        a.[Signature],
        a.AvatarImage,
        a.AvatarImageType,
        a.RankID,
        a.Suspended,
        a.LanguageFile,
        a.ThemeFile,
        a.[UseSingleSignOn],
        a.TextEditor,
        a.OverrideDefaultThemes,
        a.[PMNotification],
        a.[AutoWatchTopics],
        a.[DailyDigest],
        a.[NotificationType],
        a.[Flags],
        a.[Points],		
        a.[IsApproved],
        a.[IsGuest],
        a.[IsCaptchaExcluded],
        a.[IsActiveExcluded],
        a.[IsDST],
        a.[IsDirty],
        a.[IsFacebookUser],
        a.[IsTwitterUser],
        a.[Culture],		
            CultureUser = a.Culture,
            IsAdmin = (select count(1) from [{databaseSchema}].[{objectQualifier}UserGroup] x join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 1)<>0),
            IsGuest	= IsNull(a.Flags & 4,0),
            IsHostAdmin	= IsNull(a.Flags & 1,0),			
            RankName = b.Name,
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end 
        from 
            [{databaseSchema}].[{objectQualifier}User] a
            join [{databaseSchema}].[{objectQualifier}Rank] b on b.RankID=a.RankID			
        where 
            a.BoardID = @BoardID and
            (@Approved is null or (@Approved=0 and (a.Flags & 2)=0) or (@Approved=1 and (a.Flags & 2)=2)) and
            (@GroupID is null or exists(select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x where x.UserID=a.UserID and x.GroupID=@GroupID)) and
            (@RankID is null or a.RankID=@RankID)
        order by 
            a.Name
end
GO

create procedure [{databaseSchema}].[{objectQualifier}admin_list](@BoardID int = null, @StyledNicks bit = null,@UTCTIMESTAMP datetime) as
begin
         select 
        a.UserID,
        a.BoardID,
        b.Name AS BoardName,
        a.ProviderUserKey,
        a.[Name],
        a.[DisplayName],
        a.[Password],
        a.[Email],
        a.Joined,
        a.LastVisit,
        a.IP,
        a.NumPosts,
        a.TimeZone,
        a.Avatar,
        a.[Signature],
        a.AvatarImage,
        a.AvatarImageType,
        a.RankID,
        a.Suspended,
        a.LanguageFile,
        a.ThemeFile,
        a.[UseSingleSignOn],
        a.TextEditor,
        a.OverrideDefaultThemes,
        a.[PMNotification],
        a.[AutoWatchTopics],
        a.[DailyDigest],
        a.[NotificationType],
        a.[Flags],
        a.[Points],		
        a.[IsApproved],
        a.[IsGuest],
        a.[IsCaptchaExcluded],
        a.[IsActiveExcluded],
        a.[IsDST],
        a.[IsDirty],
        a.[IsFacebookUser],
        a.[IsTwitterUser],
        a.[Culture],
            a.NumPosts,
            CultureUser = a.Culture,			
            r.RankID,						
            RankName = r.Name,
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end, 
            NumDays = datediff(d,a.Joined,@UTCTIMESTAMP )+1,
            NumPostsForum = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.IsApproved = 1 and x.IsDeleted = 0),
            HasAvatarImage = (select count(1) from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=a.UserID and AvatarImage is not null),
            IsAdmin	= IsNull(c.IsAdmin,0),			
            IsHostAdmin	= IsNull(a.Flags & 1,0)
        from 
            [{databaseSchema}].[{objectQualifier}User] a
            JOIN
            [{databaseSchema}].[{objectQualifier}Board] b	
            ON b.BoardID = a.BoardID			
            JOIN
            [{databaseSchema}].[{objectQualifier}Rank] r	
            ON r.RankID = a.RankID		
            left join [{databaseSchema}].[{objectQualifier}vaccess] c on c.UserID=a.UserID
        where 			
            (@BoardID IS NULL OR a.BoardID = @BoardID) and
            -- is not guest 
            IsNull(a.Flags & 4,0) = 0 and
            c.ForumID = 0 and
            -- is admin 
            (IsNull(c.IsAdmin,0) <> 0) 
        order by 
            a.DisplayName
end
GO

create procedure [{databaseSchema}].[{objectQualifier}admin_pageaccesslist](@BoardID int = null, @StyledNicks bit = null,@UTCTIMESTAMP datetime) as
begin
         select 
        a.UserID,
        a.BoardID,
        b.Name AS BoardName,
        a.[Name],
        a.[DisplayName],
        a.[Culture],
            a.NumPosts,
            CultureUser = a.Culture,
            Style = case(@StyledNicks)
            when 1 then  a.UserStyle
            else ''	 end
        from 
            [{databaseSchema}].[{objectQualifier}User] a
            JOIN
            [{databaseSchema}].[{objectQualifier}Board] b	
            ON b.BoardID = a.BoardID			
            left join [{databaseSchema}].[{objectQualifier}vaccess] c 
            on c.UserID=a.UserID
        where 			
            (@BoardID IS NULL OR a.BoardID = @BoardID) and
            -- is admin 
            (IsNull(c.IsAdmin,0) <> 0) and
            c.ForumID = 0 and 			
            -- is not host admin 
            IsNull(a.Flags & 1,0) = 0 
        order by 
            a.DisplayName
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_listmembers](
                @BoardID int,
                @UserID int=null,
                @Approved bit=null,
                @GroupID int=null,
                @RankID int=null,
                @StyledNicks bit = null,
                @Literals nvarchar(255), 
                @Exclude bit = null, 
                @BeginsWith bit = null, 				
                @PageIndex int, 
                @PageSize int,
                @SortName int = 0,
                @SortRank int = 0,
                @SortJoined int = 0,
                @SortPosts int = 0,
                @SortLastVisit int = 0,
                @NumPosts int = 0,
                @NumPostsCompare int = 0) as
begin
    declare @TotalRows int
    declare @FirstSelectRowNumber int
    declare @LastSelectRowNumber int
    -- find total returned count

    select @TotalRows = count(a.UserID)
    from [{databaseSchema}].[{objectQualifier}User] a with(nolock)
      join [{databaseSchema}].[{objectQualifier}Rank] b with(nolock)
      on b.RankID=a.RankID
      where
       a.BoardID = @BoardID	
       and
        (@Approved is null or (@Approved=0 and (a.Flags & 2)=0) or (@Approved=1 and (a.Flags & 2)=2)) and
        (@GroupID is null or exists(select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x where x.UserID=a.UserID and x.GroupID=@GroupID)) and
        (@RankID is null or a.RankID=@RankID) AND
        -- user is not guest
        ISNULL(a.Flags & 4,0) <> 4
            AND
        (LOWER(a.DisplayName) LIKE CASE
            WHEN (@BeginsWith = 0 AND @Literals IS NOT NULL AND LEN(@Literals) > 0) THEN '%' + LOWER(@Literals) + '%'
            WHEN (@BeginsWith = 1 AND @Literals IS NOT NULL AND LEN(@Literals) > 0) THEN LOWER(@Literals) + '%'
            ELSE '%' END
            or
         LOWER(a.Name) LIKE CASE
            WHEN (@BeginsWith = 0 AND @Literals IS NOT NULL AND LEN(@Literals) > 0) THEN '%' + LOWER(@Literals) + '%'
            WHEN (@BeginsWith = 1 AND @Literals IS NOT NULL AND LEN(@Literals) > 0) THEN LOWER(@Literals) + '%'
            ELSE '%' END)
        and
        (a.NumPosts >= (case
        when @NumPostsCompare = 3 then @NumPosts end)
        OR a.NumPosts <= (case
        when @NumPostsCompare = 2 then @NumPosts end) OR
        a.NumPosts = (case
        when @NumPostsCompare = 1 then @NumPosts end));

    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;	
    
    with UserIds as
     (
     select ROW_NUMBER() over (order by (case
        when @SortName = 2 then a.[Name] end) DESC,
        (case
        when @SortName = 1 then a.[Name] end) ASC,
        (case
        when @SortRank = 2 then a.RankID end) DESC,
        (case
        when @SortRank = 1 then a.RankID end) ASC,	
        (case
        when @SortJoined = 2 then a.Joined end) DESC,
        (case
        when @SortJoined = 1 then a.Joined end) ASC,
        (case
        when @SortLastVisit = 2 then a.LastVisit end) DESC,
        (case
        when @SortLastVisit = 1 then a.LastVisit end) ASC,
        (case
         when @SortPosts = 2 then a.NumPosts end) DESC,
        (case
         when @SortPosts = 1 then a.NumPosts end) ASC ) as RowNum, a.UserID
     from [{databaseSchema}].[{objectQualifier}User] a with(nolock)
            join [{databaseSchema}].[{objectQualifier}Rank] b with(nolock) on b.RankID=a.RankID	
     where
       a.BoardID = @BoardID	
       and
        (@Approved is null or (@Approved=0 and (a.Flags & 2)=0) or (@Approved=1 and (a.Flags & 2)=2)) and
        (@GroupID is null or exists(select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] x where x.UserID=a.UserID and x.GroupID=@GroupID)) and
        (@RankID is null or a.RankID=@RankID) AND
        -- user is not guest
        ISNULL(a.Flags & 4,0) <> 4
            AND
        (LOWER(a.DisplayName) LIKE CASE
            WHEN (@BeginsWith = 0 AND @Literals IS NOT NULL AND LEN(@Literals) > 0) THEN '%' + LOWER(@Literals) + '%'
            WHEN (@BeginsWith = 1 AND @Literals IS NOT NULL AND LEN(@Literals) > 0) THEN LOWER(@Literals) + '%'
            ELSE '%' END
            or
         LOWER(a.Name) LIKE CASE
            WHEN (@BeginsWith = 0 AND @Literals IS NOT NULL AND LEN(@Literals) > 0) THEN '%' + LOWER(@Literals) + '%'
            WHEN (@BeginsWith = 1 AND @Literals IS NOT NULL AND LEN(@Literals) > 0) THEN LOWER(@Literals) + '%'
            ELSE '%' END)
        and
        (a.NumPosts >= (case
        when @NumPostsCompare = 3 then @NumPosts end)
        OR a.NumPosts <= (case
        when @NumPostsCompare = 2 then @NumPosts end) OR
        a.NumPosts = (case
        when @NumPostsCompare = 1 then @NumPosts end))
      )	
      select
            a.*,	
            CultureUser = a.Culture,
            IsAdmin = (select COUNT(1) from [{databaseSchema}].[{objectQualifier}UserGroup] x join [{databaseSchema}].[{objectQualifier}Group] y on y.GroupID=x.GroupID where x.UserID=a.UserID and (y.Flags & 1)<>0),
            IsHostAdmin	= ISNULL(a.Flags & 1,0),
            b.RankID,
            RankName = b.Name,
            Style = case(@StyledNicks)
            when 1 then a.UserStyle
            else ''	end,
            TotalCount = @TotalRows
            from
            UserIds ti inner join
            [{databaseSchema}].[{objectQualifier}User] a with(nolock)
            on a.UserID = ti.UserID
            join [{databaseSchema}].[{objectQualifier}Rank] b with(nolock) on b.RankID=a.RankID	
    
    where ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC;
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_login](@BoardID int,@Name nvarchar(255),@Password nvarchar(32)) as
begin
    
    declare @UserID int

    -- Try correct board first
    if exists(select UserID from [{databaseSchema}].[{objectQualifier}User] where Name=@Name and Password=@Password and BoardID=@BoardID and (Flags & 2)=2)
    begin
        select UserID from [{databaseSchema}].[{objectQualifier}User] where Name=@Name and Password=@Password and BoardID=@BoardID and (Flags & 2)=2
        return
    end

    if not exists(select UserID from [{databaseSchema}].[{objectQualifier}User] where Name=@Name and Password=@Password and (BoardID=@BoardID or (Flags & 3)=3))
        set @UserID=null
    else
        select 
            @UserID=UserID 
        from 
            [{databaseSchema}].[{objectQualifier}User]
        where 
            Name=@Name and 
            [Password]=@Password and 
            (BoardID=@BoardID or (Flags & 1)=1) and
            (Flags & 2)=2

    select @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_nntp](@BoardID int,@UserName nvarchar(255),@Email nvarchar(255),@TimeZone int, @UTCTIMESTAMP datetime) as
begin	
    
    declare @UserID int

    set @UserName = @UserName + ' (NNTP)'

    select
        @UserID=UserID
    from
        [{databaseSchema}].[{objectQualifier}User]
    where
        BoardID=@BoardID and
        Name=@UserName

    if @UserID is null
    begin   
        exec [{databaseSchema}].[{objectQualifier}user_save] null,@BoardID,@UserName,@UserName,@Email,@TimeZone,
		                                                    null,null,null,null,null,null,1,0,null,null,null,null,
															null,20,20,@UTCTIMESTAMP   

        -- The next one is not safe, but this procedure is only used for testing
        -- select @UserID = @@IDENTITY
		SELECT top 1  @UserID = UserID FROM [{databaseSchema}].[{objectQualifier}User] order by UserID desc;
    end

    select UserID=@UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_recoverpassword](@BoardID int,@UserName nvarchar(255),@Email nvarchar(250)) as
begin
    
    declare @UserID int
    select @UserID = UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID = @BoardID and Name = @UserName and Email = @Email
    if @UserID is null begin
        select UserID = convert(int,null)
        return
    end else
    begin
        select UserID = @UserID
    end
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_removepoints] (@UserID int, @FromUserID int = null, @UTCTIMESTAMP datetime, @Points int) AS
BEGIN
    
    UPDATE [{databaseSchema}].[{objectQualifier}User] SET Points = Points - @Points WHERE UserID = @UserID

    IF @FromUserID IS NOT NULL 
    BEGIN
        declare	@VoteDate datetime
    set @VoteDate = (select top 1 VoteDate from [{databaseSchema}].[{objectQualifier}ReputationVote] where ReputationFromUserID=@FromUserID AND ReputationToUserID=@UserID)
    IF @VoteDate is not null
    begin	     
          update [{databaseSchema}].[{objectQualifier}ReputationVote] set VoteDate=@UTCTIMESTAMP where VoteDate = @VoteDate AND ReputationFromUserID=@FromUserID AND ReputationToUserID=@UserID
    end
    ELSE
      begin
          insert into [{databaseSchema}].[{objectQualifier}ReputationVote](ReputationFromUserID,ReputationToUserID,VoteDate)
          values (@FromUserID, @UserID, @UTCTIMESTAMP)
      end
    END
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_resetpoints] AS
BEGIN
    
    UPDATE [{databaseSchema}].[{objectQualifier}User] SET Points = NumPosts * 3
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_savenotification](
    @UserID				int,
    @PMNotification		bit = null,
    @AutoWatchTopics    bit = null,
    @NotificationType	int = null,
    @DailyDigest		bit = null
)
AS
BEGIN

        UPDATE
            [{databaseSchema}].[{objectQualifier}User]
        SET
            PMNotification = (CASE WHEN (@PMNotification is not null) THEN  @PMNotification ELSE PMNotification END),
            AutoWatchTopics = (CASE WHEN (@AutoWatchTopics is not null) THEN  @AutoWatchTopics ELSE AutoWatchTopics END),
            NotificationType =  (CASE WHEN (@NotificationType is not null) THEN  @NotificationType ELSE NotificationType END),
            DailyDigest = (CASE WHEN (@DailyDigest is not null) THEN  @DailyDigest ELSE DailyDigest END)
        WHERE
            UserID = @UserID
END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}user_save](
    @UserID				int,
    @BoardID			int,
    @UserName			nvarchar(255) = null,
    @DisplayName		nvarchar(255) = null,
    @Email				nvarchar(255) = null,
    @TimeZone			int,
    @LanguageFile		nvarchar(50) = null,
    @Culture		    varchar(10) = null,
    @ThemeFile			nvarchar(50) = null,
    @UseSingleSignOn    bit = null,
    @TextEditor			nvarchar(50) = null,
    @OverrideDefaultTheme	bit = null,
    @Approved			bit = null,
    @PMNotification		bit = null,
	@NotificationType	int = null,
	@ProviderUserKey	nvarchar(64) = null,
    @AutoWatchTopics    bit = null,   
    @DSTUser            bit = null,
    @HideUser           bit = null,
	@TopicsPerPage      int,
    @PostsPerPage       int,
    @UTCTIMESTAMP datetime)
AS
begin
    
    declare @RankID int
    declare @Flags int = 0	
    declare @OldDisplayName nvarchar(255)		
        
    if @DSTUser is null SET @DSTUser = 0
    if @HideUser is null SET @HideUser = 0
    if @PMNotification is null SET @PMNotification = 1
    if @AutoWatchTopics is null SET @AutoWatchTopics = 0
    if @OverrideDefaultTheme is null SET @OverrideDefaultTheme=0
    if @UseSingleSignOn is null SET @UseSingleSignOn=0

	-- i.e. nntp user or like
    if @UserID is null or @UserID<1 begin
        
        if @Approved<>0 set @Flags = @Flags | 2	
        if @Email = '' set @Email = null
        
        select @RankID = RankID from [{databaseSchema}].[{objectQualifier}Rank] where (Flags & 1)<>0 and BoardID=@BoardID

        insert into [{databaseSchema}].[{objectQualifier}User](BoardID,RankID,[Name],DisplayName,Password,Email,Joined,LastVisit,NumPosts,TimeZone,Flags,PMNotification,AutoWatchTopics,NotificationType,ProviderUserKey,TopicsPerPage,PostsPerPage) 
        values(@BoardID,@RankID,@UserName,@DisplayName,'-',@Email,@UTCTIMESTAMP ,@UTCTIMESTAMP ,0,@TimeZone, @Flags,@PMNotification,@AutoWatchTopics,@NotificationType,@ProviderUserKey,@TopicsPerPage,@PostsPerPage)		
    
        set @UserID = SCOPE_IDENTITY()

        insert into [{databaseSchema}].[{objectQualifier}UserGroup](UserID,GroupID) select @UserID,GroupID from [{databaseSchema}].[{objectQualifier}Group] where BoardID=@BoardID and (Flags & 4)<>0
    end
    else begin
        SELECT @Flags = Flags, @OldDisplayName = DisplayName FROM [{databaseSchema}].[{objectQualifier}User] where UserID = @UserID
        
        -- set user dirty 
        set @Flags = @Flags	| 64
        
        IF ((@DSTUser<>0) AND (@Flags & 32) <> 32)		
        SET @Flags = @Flags | 32
        ELSE IF ((@DSTUser=0) AND (@Flags & 32) = 32)
        SET @Flags = @Flags ^ 32
            
        IF ((@HideUser<>0) AND ((@Flags & 16) <> 16)) 
        SET @Flags = @Flags | 16 
        ELSE IF ((@HideUser=0) AND ((@Flags & 16) = 16)) 
        SET @Flags = @Flags ^ 16
        
        update [{databaseSchema}].[{objectQualifier}User] set
            TimeZone = @TimeZone,
            LanguageFile = @LanguageFile,
            ThemeFile = @ThemeFile,
            Culture = @Culture,
            UseSingleSignOn = @UseSingleSignOn,
            TextEditor = @TextEditor,
            OverrideDefaultThemes = @OverrideDefaultTheme,
            PMNotification = (CASE WHEN (@PMNotification is not null) THEN  @PMNotification ELSE PMNotification END),
            AutoWatchTopics = (CASE WHEN (@AutoWatchTopics is not null) THEN  @AutoWatchTopics ELSE AutoWatchTopics END),
            NotificationType =  (CASE WHEN (@NotificationType is not null) THEN  @NotificationType ELSE NotificationType END),
            Flags = (CASE WHEN @Flags<>Flags THEN  @Flags ELSE Flags END),
            DisplayName = (CASE WHEN (@DisplayName is not null) THEN  @DisplayName ELSE DisplayName END),
            Email = (CASE WHEN (@Email is not null) THEN  @Email ELSE Email END),
			TopicsPerPage = @TopicsPerPage,
			PostsPerPage = @PostsPerPage
        where UserID = @UserID	
        -- here we sync a new display name everywhere
        if (@DisplayName IS NOT NULL AND COALESCE(@OldDisplayName,'') != COALESCE(@DisplayName,''))
        begin
        -- sync display names everywhere - can run a long time on large forums
        update [{databaseSchema}].[{objectQualifier}Forum] set LastUserDisplayName = @DisplayName where LastUserID = @UserID  AND (LastUserDisplayName IS NULL OR LastUserDisplayName = @OldDisplayName)
        update [{databaseSchema}].[{objectQualifier}Topic] set LastUserDisplayName = @DisplayName where LastUserID = @UserID AND (LastUserDisplayName IS NULL OR LastUserDisplayName = @OldDisplayName)
        update [{databaseSchema}].[{objectQualifier}Topic] set UserDisplayName = @DisplayName where UserID = @UserID AND (UserDisplayName IS NULL OR UserDisplayName = @OldDisplayName)
        update [{databaseSchema}].[{objectQualifier}Message] set UserDisplayName = @DisplayName where UserID = @UserID AND (UserDisplayName IS NULL OR UserDisplayName = @OldDisplayName)
        update [{databaseSchema}].[{objectQualifier}ShoutboxMessage] set UserDisplayName = @DisplayName where UseriD = @UserID AND (UserDisplayName IS NULL OR UserDisplayName = @OldDisplayName)
        end
        
    end
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}user_saveavatar]
(
    @UserID int,
    @Avatar nvarchar(255) = NULL,
    @AvatarImage varbinary(max) = NULL,
    @AvatarImageType nvarchar(50) = NULL
)
AS
BEGIN
    
    IF @Avatar IS NOT NULL 
    BEGIN
        UPDATE
            [{databaseSchema}].[{objectQualifier}User]
        SET
            Avatar = @Avatar,
            AvatarImage = null,
            AvatarImageType = null
        WHERE
            UserID = @UserID
    END
    ELSE IF @AvatarImage IS NOT NULL 
    BEGIN
        UPDATE
            [{databaseSchema}].[{objectQualifier}User]
        SET
            AvatarImage = @AvatarImage,
            AvatarImageType = @AvatarImageType,
            Avatar = null
        WHERE
            UserID = @UserID
    END
END

GO

create procedure [{databaseSchema}].[{objectQualifier}user_savepassword](@UserID int,@Password nvarchar(32)) as
begin
    
    update [{databaseSchema}].[{objectQualifier}User] set Password = @Password where UserID = @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_savesignature](@UserID int,@Signature ntext) as
begin
    
    update [{databaseSchema}].[{objectQualifier}User] set Signature = @Signature where UserID = @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_setnotdirty](@UserID int) as
begin	
    update [{databaseSchema}].[{objectQualifier}User] set Flags = Flags ^ 64 where UserID = @UserID
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_setpoints] (@UserID int,@Points int) AS
BEGIN
    
    UPDATE [{databaseSchema}].[{objectQualifier}User] SET Points = @Points WHERE UserID = @UserID
END
GO

create procedure [{databaseSchema}].[{objectQualifier}user_setrole](@BoardID int,@ProviderUserKey nvarchar(64),@Role nvarchar(50)) as
begin
    
    declare @UserID int, @GroupID int
    
    select @UserID=UserID from [{databaseSchema}].[{objectQualifier}User] where BoardID=@BoardID and ProviderUserKey=@ProviderUserKey

    if @Role is null
    begin
        delete from [{databaseSchema}].[{objectQualifier}UserGroup] where UserID=@UserID
    end else
    begin
        if not exists(select 1 from [{databaseSchema}].[{objectQualifier}Group] where BoardID=@BoardID and Name=@Role)
        begin
            insert into [{databaseSchema}].[{objectQualifier}Group](Name,BoardID,Flags)
            values(@Role,@BoardID,0);
            set @GroupID = SCOPE_IDENTITY()

            insert into [{databaseSchema}].[{objectQualifier}ForumAccess](GroupID,ForumID,AccessMaskID)
            select
                @GroupID,
                a.ForumID,
                min(a.AccessMaskID)
            from
                [{databaseSchema}].[{objectQualifier}ForumAccess] a
                join [{databaseSchema}].[{objectQualifier}Group] b on b.GroupID=a.GroupID
            where
                b.BoardID=@BoardID and
                (b.Flags & 4)=4
            group by
                a.ForumID
        end else
        begin
            select @GroupID = GroupID from [{databaseSchema}].[{objectQualifier}Group] where BoardID=@BoardID and Name=@Role
        end
        -- user already can be in the group even if Role isn't null, an extra check is required 
        if not exists(select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] where UserID=@UserID and GroupID=@GroupID)
        begin
        insert into [{databaseSchema}].[{objectQualifier}UserGroup](UserID,GroupID) values(@UserID,@GroupID)
        end
    end
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_suspend](@UserID int,@Suspend datetime=null) as
begin
    
    update [{databaseSchema}].[{objectQualifier}User] set Suspended = @Suspend where UserID=@UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_upgrade](@UserID int) as
begin
    
    declare @RankID			int
    declare @Flags			int
    declare @MinPosts		int
    declare @NumPosts		int
    declare @BoardId		int
    declare @RankBoardID	int

    -- Get user and rank information
    select
        @RankID = b.RankID,
        @Flags = b.Flags,
        @MinPosts = b.MinPosts,
        @NumPosts = a.NumPosts,
        @BoardId = a.BoardId		
    from
        [{databaseSchema}].[{objectQualifier}User] a
        inner join [{databaseSchema}].[{objectQualifier}Rank] b on b.RankID = a.RankID
    where
        a.UserID = @UserID
    
    -- If user isn't member of a ladder rank, exit
    if (@Flags & 2) = 0 return

    -- retrieve board current user's rank beling to	
    select @RankBoardId = BoardID
    from   [{databaseSchema}].[{objectQualifier}Rank]
    where  RankID = @RankID

    -- does user have rank from his board?
    IF @RankBoardId <> @BoardId begin
        -- get highest rank user can get
        select top 1
               @RankID = RankID
        from   [{databaseSchema}].[{objectQualifier}Rank]
        where  BoardId = @BoardId
               and (Flags & 2) = 2
               and MinPosts <= @NumPosts
        order by
               MinPosts desc
    end
    else begin
        -- See if user got enough posts for next ladder group
        select top 1
            @RankID = RankID
        from
            [{databaseSchema}].[{objectQualifier}Rank]
        where
            BoardId = @BoardId and
            (Flags & 2) = 2 and
            MinPosts <= @NumPosts and
            MinPosts > @MinPosts
        order by
            MinPosts
    end

    if @@ROWCOUNT=1
        update [{databaseSchema}].[{objectQualifier}User] set RankID = @RankID where UserID = @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}userforum_delete](@UserID int,@ForumID int) as
begin
    
    delete from [{databaseSchema}].[{objectQualifier}UserForum] where UserID=@UserID and ForumID=@ForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}userforum_list](@UserID int=null,@ForumID int=null) as 
begin
    
    select 
        a.*,
        b.AccessMaskID,
        b.Accepted,
        Access = c.Name
    from
        [{databaseSchema}].[{objectQualifier}User] a
        join [{databaseSchema}].[{objectQualifier}UserForum] b on b.UserID=a.UserID
        join [{databaseSchema}].[{objectQualifier}AccessMask] c on c.AccessMaskID=b.AccessMaskID
    where
        (@UserID is null or a.UserID=@UserID) and
        (@ForumID is null or b.ForumID=@ForumID)
    order by
        a.Name	
end
GO

create procedure [{databaseSchema}].[{objectQualifier}userforum_save](@UserID int,@ForumID int,@AccessMaskID int,@UTCTIMESTAMP datetime) as
begin
    
    if exists(select 1 from [{databaseSchema}].[{objectQualifier}UserForum] where UserID=@UserID and ForumID=@ForumID)
        update [{databaseSchema}].[{objectQualifier}UserForum] set AccessMaskID=@AccessMaskID where UserID=@UserID and ForumID=@ForumID
    else
        insert into [{databaseSchema}].[{objectQualifier}UserForum](UserID,ForumID,AccessMaskID,Invited,Accepted) values(@UserID,@ForumID,@AccessMaskID,@UTCTIMESTAMP ,1)
end
GO

create procedure [{databaseSchema}].[{objectQualifier}usergroup_list](@UserID int) as begin
    
    select 
        b.GroupID,
        b.Name,
        b.Style
    from
        [{databaseSchema}].[{objectQualifier}UserGroup] a
        join [{databaseSchema}].[{objectQualifier}Group] b on b.GroupID=a.GroupID
    where
        a.UserID = @UserID
    order by
        b.Name
end
GO

create procedure [{databaseSchema}].[{objectQualifier}usergroup_save](@UserID int,@GroupID int,@Member bit) as
begin
    
    if @Member=0
    begin
        delete from [{databaseSchema}].[{objectQualifier}UserGroup] where UserID=@UserID and GroupID=@GroupID
    end 
    else
    begin
        insert into [{databaseSchema}].[{objectQualifier}UserGroup](UserID,GroupID)
        select @UserID,@GroupID
        where not exists(select 1 from [{databaseSchema}].[{objectQualifier}UserGroup] where UserID=@UserID and GroupID=@GroupID)
        UPDATE [{databaseSchema}].[{objectQualifier}User] SET UserStyle= ISNULL(( SELECT TOP 1 f.Style FROM [{databaseSchema}].[{objectQualifier}UserGroup] e 
        join [{databaseSchema}].[{objectQualifier}Group] f on f.GroupID=e.GroupID WHERE e.UserID=@UserID AND LEN(f.Style) > 2 ORDER BY f.SortOrder), (SELECT TOP 1 r.Style FROM [{databaseSchema}].[{objectQualifier}Rank] r where r.RankID = [{databaseSchema}].[{objectQualifier}User].RankID)) 
        WHERE UserID = @UserID    	
    end  
end
GO

create procedure [{databaseSchema}].[{objectQualifier}userpmessage_delete](@UserPMessageID int) as
begin
    
    delete from [{databaseSchema}].[{objectQualifier}UserPMessage] where UserPMessageID=@UserPMessageID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}userpmessage_list](@UserPMessageID int) as
begin
    
    SELECT
        a.*,
        FromUser = b.Name,
        ToUserID = c.UserID,
        ToUser = c.Name,
        d.IsRead,
        d.IsReply,
        d.UserPMessageID
    FROM
        [{databaseSchema}].[{objectQualifier}PMessage] a
        INNER JOIN [{databaseSchema}].[{objectQualifier}UserPMessage] d ON d.PMessageID = a.PMessageID
        INNER JOIN [{databaseSchema}].[{objectQualifier}User] b ON b.UserID = a.FromUserID
        inner join [{databaseSchema}].[{objectQualifier}User] c ON c.UserID = d.UserID
    WHERE
        d.UserPMessageID = @UserPMessageID
    AND
        d.IsDeleted=0
end
GO

create procedure [{databaseSchema}].[{objectQualifier}watchforum_add](@UserID int,@ForumID int,@UTCTIMESTAMP datetime) as
begin
    
    insert into [{databaseSchema}].[{objectQualifier}WatchForum](ForumID,UserID,Created)
    select @ForumID, @UserID, @UTCTIMESTAMP 
    where not exists(select 1 from [{databaseSchema}].[{objectQualifier}WatchForum] where ForumID=@ForumID and UserID=@UserID)
end
GO

create procedure [{databaseSchema}].[{objectQualifier}watchforum_check](@UserID int,@ForumID int) as
begin
    
    SELECT WatchForumID FROM [{databaseSchema}].[{objectQualifier}WatchForum] WHERE UserID = @UserID AND ForumID = @ForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}watchforum_delete](@WatchForumID int) as
begin
    
    delete from [{databaseSchema}].[{objectQualifier}WatchForum] where WatchForumID = @WatchForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}watchforum_list](@UserID int) as
begin
    
    select
        a.*,
        ForumName = b.Name,
        [Messages] = (select count(1) from [{databaseSchema}].[{objectQualifier}Topic] x join [{databaseSchema}].[{objectQualifier}Message] y on y.TopicID=x.TopicID where x.ForumID=a.ForumID),
        Topics = (select count(1) from [{databaseSchema}].[{objectQualifier}Topic] x where x.ForumID=a.ForumID and x.TopicMovedID is null),
        b.LastPosted,
        b.LastMessageID,
        LastTopicID = (select TopicID from [{databaseSchema}].[{objectQualifier}Message] x where x.MessageID=b.LastMessageID),
        b.LastUserID,
        LastUserName = IsNull(b.LastUserName,(select x.Name from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=b.LastUserID)),
        LastUserDisplayName = IsNull(b.LastUserDisplayName,(select x.DisplayName from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=b.LastUserID))
    from
        [{databaseSchema}].[{objectQualifier}WatchForum] a
        inner join [{databaseSchema}].[{objectQualifier}Forum] b on b.ForumID = a.ForumID
    where
        a.UserID = @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}watchtopic_add](@UserID int,@TopicID int,@UTCTIMESTAMP datetime) as
begin
    
    insert into [{databaseSchema}].[{objectQualifier}WatchTopic](TopicID,UserID,Created)
    select @TopicID, @UserID, @UTCTIMESTAMP 
    where not exists(select 1 from [{databaseSchema}].[{objectQualifier}WatchTopic] where TopicID=@TopicID and UserID=@UserID)
end
GO

create procedure [{databaseSchema}].[{objectQualifier}watchtopic_check](@UserID int,@TopicID int) as
begin
    
    SELECT WatchTopicID FROM [{databaseSchema}].[{objectQualifier}WatchTopic] WHERE UserID = @UserID AND TopicID = @TopicID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}watchtopic_delete](@WatchTopicID int) as
begin
        delete from [{databaseSchema}].[{objectQualifier}WatchTopic] where WatchTopicID = @WatchTopicID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}watchtopic_list](@UserID int) as
begin
        select
        a.*,
        TopicName = b.Topic,
        Replies = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=b.TopicID) -1,
        b.[Views],
        b.LastPosted,
        b.LastMessageID,
        b.LastUserID,
        LastUserName = IsNull(b.LastUserName,(select x.Name from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=b.LastUserID)),
        LastUserDisplayName = IsNull(b.LastUserDisplayName,(select x.DisplayName from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=b.LastUserID))
    from
        [{databaseSchema}].[{objectQualifier}WatchTopic] a
        inner join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID = a.TopicID
    where
        a.UserID = @UserID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}message_reply_list](@MessageID int) as
begin
        set nocount on
    select
                a.MessageID,
        a.Posted,
        Subject = c.Topic,
        a.[Message],
        a.UserID,
        a.Flags,
        UserName = IsNull(a.UserName,b.Name),
        b.Signature
    from
        [{databaseSchema}].[{objectQualifier}Message] a
        inner join [{databaseSchema}].[{objectQualifier}User] b on b.UserID = a.UserID
        inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = a.TopicID
    where
        a.IsApproved = 1 and
        a.ReplyTo = @MessageID
end
GO


CREATE procedure [{databaseSchema}].[{objectQualifier}message_deleteundelete](@MessageID int, @isModeratorChanged bit, @DeleteReason nvarchar(100), @isDeleteAction INTEGER) as
begin
    
    declare @TopicID		int
    declare @ForumID		int
    declare @MessageCount	int
    declare @LastMessageID	int
    declare @UserID			int

    -- Find TopicID and ForumID
    select @TopicID=b.TopicID,@ForumID=b.ForumID,@UserID = a.UserID 
    from 
        [{databaseSchema}].[{objectQualifier}Message] a
        inner join [{databaseSchema}].[{objectQualifier}Topic] b on b.TopicID=a.TopicID
    where 
        a.MessageID=@MessageID

    -- Update LastMessageID in Topic and Forum
    update [{databaseSchema}].[{objectQualifier}Topic] set
        LastPosted = null,
        LastMessageID = null,
        LastUserID = null,
        LastUserName = null,
        LastUserDisplayName = null,
        LastMessageFlags = null
    where LastMessageID = @MessageID

    update [{databaseSchema}].[{objectQualifier}Forum] set
        LastPosted = null,
        LastTopicID = null,
        LastMessageID = null,
        LastUserID = null,
        LastUserName = null,
        LastUserDisplayName = null
    where LastMessageID = @MessageID

    -- "Delete" message
    update [{databaseSchema}].[{objectQualifier}Message]
     set IsModeratorChanged = @isModeratorChanged, DeleteReason = @DeleteReason, Flags = Flags ^ 8
     where MessageID = @MessageID and ((Flags & 8) <> @isDeleteAction*8)
    
    -- update num posts for user now that the delete/undelete status has been toggled...
    UPDATE [{databaseSchema}].[{objectQualifier}User] SET NumPosts = (SELECT count(MessageID) FROM [{databaseSchema}].[{objectQualifier}Message] WHERE UserID = @UserID AND IsDeleted = 0 AND IsApproved = 1) WHERE UserID = @UserID

    -- Delete topic if there are no more messages
    select @MessageCount = count(1) from [{databaseSchema}].[{objectQualifier}Message] where TopicID = @TopicID and IsDeleted=0
    if @MessageCount=0 exec [{databaseSchema}].[{objectQualifier}topic_delete] @TopicID,null,0,1 
    -- update lastpost
    exec [{databaseSchema}].[{objectQualifier}topic_updatelastpost] @ForumID,@TopicID
    exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID
    -- update topic numposts
    update [{databaseSchema}].[{objectQualifier}Topic] set
        NumPosts = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and x.IsApproved = 1 and x.IsDeleted = 0 )
    where TopicID = @TopicID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}topic_create_by_message] (
    @MessageID int,
    @ForumID	int,
    @Subject	nvarchar(100),
    @UTCTIMESTAMP datetime
) as
begin
        
declare		@UserID		int
declare		@Posted		datetime

set @UserID = (select UserID from [{databaseSchema}].[{objectQualifier}message] where MessageID =  @MessageID)
set  @Posted  = (select  posted from [{databaseSchema}].[{objectQualifier}message] where MessageID =  @MessageID)


    declare @TopicID int
    --declare @MessageID int

    if @Posted is null set @Posted = @UTCTIMESTAMP 

    insert into [{databaseSchema}].[{objectQualifier}Topic](ForumID,Topic,UserID,Posted,[Views],Priority,PollID,UserName,NumPosts)
    values(@ForumID,@Subject,@UserID,@Posted,0,0,null,null,0)

    set @TopicID = @@IDENTITY
--	exec [{databaseSchema}].[{objectQualifier}message_save] @TopicID,@UserID,@Message,@UserName,@IP,@Posted,null,null, null,@Flags,@MessageID output
    select TopicID = @TopicID, MessageID = @MessageID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_move] (@MessageID int, @MoveToTopic int) AS
BEGIN
    DECLARE
    @Position int,
    @ReplyToID int,
    @OldTopicID int,
    @OldForumID int

    
    declare @NewForumID		int
    declare @MessageCount	int
    declare @LastMessageID	int

    -- Find TopicID and ForumID
--	select @OldTopicID=b.TopicID,@ForumID=b.ForumID from [{databaseSchema}].[{objectQualifier}Message] a,{objectQualifier}Topic b where a.MessageID=@MessageID and b.TopicID=a.TopicID

SET 	@NewForumID = (SELECT     ForumID
                FROM         [{databaseSchema}].[{objectQualifier}Topic]
                WHERE     (TopicId = @MoveToTopic))


SET 	@OldTopicID = 	(SELECT     TopicID
                FROM         [{databaseSchema}].[{objectQualifier}Message]
                WHERE     (MessageID = @MessageID))

SET 	@OldForumID = (SELECT     ForumID
                FROM         [{databaseSchema}].[{objectQualifier}Topic]
                WHERE     (TopicId = @OldTopicID))

SET	@ReplyToID = (SELECT     MessageID
            FROM         [{databaseSchema}].[{objectQualifier}Message]
            WHERE     ([Position] = 0) AND (TopicID = @MoveToTopic))

SET	@Position = 	(SELECT     MAX([Position]) + 1 AS Expr1
            FROM         [{databaseSchema}].[{objectQualifier}Message]
            WHERE     (TopicID = @MoveToTopic) and posted < (select posted from [{databaseSchema}].[{objectQualifier}Message] where MessageID = @MessageID ) )

if @Position is null  set @Position = 0

update [{databaseSchema}].[{objectQualifier}Message] set
        Position = Position+1
     WHERE     (TopicID = @MoveToTopic) and posted > (select posted from [{databaseSchema}].[{objectQualifier}Message] where MessageID = @MessageID)

update [{databaseSchema}].[{objectQualifier}Message] set
        Position = Position-1
     WHERE     (TopicID = @OldTopicID) and posted > (select posted from [{databaseSchema}].[{objectQualifier}Message] where MessageID = @MessageID)

    


    -- Update LastMessageID in Topic and Forum
    update [{databaseSchema}].[{objectQualifier}Topic] set
        LastPosted = null,
        LastMessageID = null,
        LastUserID = null,
        LastUserName = null,
        LastMessageFlags = null,
        LastUserDisplayName = null
    where LastMessageID = @MessageID

    update [{databaseSchema}].[{objectQualifier}Forum] set
        LastPosted = null,
        LastTopicID = null,
        LastMessageID = null,
        LastUserID = null,
        LastUserName = null,
        LastUserDisplayName = null
    where LastMessageID = @MessageID


UPDATE [{databaseSchema}].[{objectQualifier}Message] SET
    TopicID = @MoveToTopic,
    ReplyTo = @ReplyToID,
    [Position] = @Position
WHERE  MessageID = @MessageID

    -- Delete topic if there are no more messages
    select @MessageCount = count(1) from [{databaseSchema}].[{objectQualifier}Message] where TopicID = @OldTopicID and IsDeleted=0
    if @MessageCount=0 exec [{databaseSchema}].[{objectQualifier}topic_delete] @OldTopicID,null,0,1

    -- update lastpost
    exec [{databaseSchema}].[{objectQualifier}topic_updatelastpost] @OldForumID,@OldTopicID
    exec [{databaseSchema}].[{objectQualifier}topic_updatelastpost] @NewForumID,@MoveToTopic

    -- update topic numposts
    update [{databaseSchema}].[{objectQualifier}Topic] set
        NumPosts = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and x.IsApproved = 1 and x.IsDeleted = 0)
    where TopicID = @OldTopicID
    update [{databaseSchema}].[{objectQualifier}Topic] set
        NumPosts = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=[{databaseSchema}].[{objectQualifier}Topic].TopicID and x.IsApproved = 1 and x.IsDeleted = 0)
    where TopicID = @MoveToTopic

    exec [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @NewForumID
    exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @NewForumID
    exec [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @OldForumID
    exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @OldForumID

END
GO

create proc [{databaseSchema}].[{objectQualifier}forum_resync]
    @BoardID int,
    @ForumID int = null
AS
begin
    
    if (@ForumID is null) begin
        declare curForums cursor for
            select 
                a.ForumID
            from
                [{databaseSchema}].[{objectQualifier}Forum] a
                JOIN [{databaseSchema}].[{objectQualifier}Category] b on a.CategoryID=b.CategoryID
                JOIN [{databaseSchema}].[{objectQualifier}Board] c on b.BoardID = c.BoardID  
            where
                c.BoardID=@BoardID

        open curForums
        
        -- cycle through forums
        fetch next from curForums into @ForumID
        while @@FETCH_STATUS = 0
        begin
            --update statistics
            exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID
            --update last post
            exec [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @ForumID

            fetch next from curForums into @ForumID
        end
        close curForums
        deallocate curForums
    end
    else begin
        --update statistics
        exec [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID
        --update last post
        exec [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @ForumID
    end
end
GO

create proc [{databaseSchema}].[{objectQualifier}board_resync]
    @BoardID int = null
as
begin
    
    if (@BoardID is null) begin
        declare curBoards cursor for
            select BoardID from	[{databaseSchema}].[{objectQualifier}Board]

        open curBoards
        
        -- cycle through forums
        fetch next from curBoards into @BoardID
        while @@FETCH_STATUS = 0
        begin
            --resync board forums
            exec [{databaseSchema}].[{objectQualifier}forum_resync] @BoardID

            fetch next from curBoards into @BoardID
        end
        close curBoards
        deallocate curBoards
    end
    else begin
        --resync board forums
        exec [{databaseSchema}].[{objectQualifier}forum_resync] @BoardID
    end
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}category_simplelist](
                @StartID INT  = 0,
                @Limit   INT  = 500)
AS
    BEGIN
            
        SET ROWCOUNT  @Limit
        SELECT   c.[CategoryID],
                 c.[Name]
        FROM     [{databaseSchema}].[{objectQualifier}Category] c
        WHERE    c.[CategoryID] >= @StartID
        AND c.[CategoryID] < (@StartID + @Limit)
        ORDER BY c.[CategoryID]
        SET ROWCOUNT  0
    END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_simplelist](
                @StartID INT  = 0,
                @Limit   INT  = 500)
AS
    BEGIN
                SET ARITHABORT ON				
        SET ROWCOUNT  @Limit
        SELECT   f.[ForumID],
                 f.[Name],
				 f.[LastPosted]
        FROM     [{databaseSchema}].[{objectQualifier}Forum] f
        WHERE    f.[ForumID] >= @StartID
        AND f.[ForumID] < (@StartID + @Limit)
        ORDER BY f.[ForumID]
        SET ROWCOUNT  0
    END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_simplelist](
                @StartID INT  = 0,
                @Limit   INT  = 1000)
AS
    BEGIN
                SET ARITHABORT ON				
        SET ROWCOUNT  @Limit
        SELECT   m.[MessageID],
                 m.[TopicID]
        FROM     [{databaseSchema}].[{objectQualifier}Message] m
        WHERE    m.[MessageID] >= @StartID
        AND m.[MessageID] < (@StartID + @Limit)
        AND m.[TopicID] IS NOT NULL
        ORDER BY m.[MessageID]
        SET ROWCOUNT  0
    END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_simplelist](
                @StartID INT  = 0,
                @Limit   INT  = 500)
AS
    BEGIN
            SET ARITHABORT ON				
        SET ROWCOUNT  @Limit
        SELECT   t.[TopicID],
                 t.[Topic]
        FROM     [{databaseSchema}].[{objectQualifier}Topic] t
        WHERE    t.[TopicID] >= @StartID
        AND t.[TopicID] < (@StartID + @Limit)
        ORDER BY t.[TopicID]
        SET ROWCOUNT  0
    END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_simplelist](
                @StartID INT  = 0,
                @Limit   INT  = 500)
AS
    BEGIN
                
        SET ROWCOUNT  @Limit
        SELECT   a.[UserID],
                 a.[Name],
				 a.[DisplayName]
        FROM     [{databaseSchema}].[{objectQualifier}User] a
        WHERE    a.[UserID] >= @StartID
        AND a.[UserID] < (@StartID + @Limit)
        ORDER BY a.[UserID]
        SET ROWCOUNT  0
    END
GO

-- BBCode

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}bbcode_delete]
(
    @BBCodeID int = NULL
)
AS
BEGIN
        
    IF @BBCodeID IS NOT NULL
        DELETE FROM [{objectQualifier}BBCode] WHERE BBCodeID = @BBCodeID
    ELSE
        DELETE FROM [{objectQualifier}BBCode]
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}bbcode_list]
(
    @BoardID int,
    @BBCodeID int = null
)
AS
BEGIN
        
    IF @BBCodeID IS NULL
        SELECT * FROM [{objectQualifier}BBCode] WHERE BoardID = @BoardID ORDER BY ExecOrder, [Name] DESC
    ELSE
        SELECT * FROM [{objectQualifier}BBCode] WHERE BBCodeID = @BBCodeID ORDER BY ExecOrder
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}bbcode_save]
(
    @BBCodeID int = null,
    @BoardID int,
    @Name nvarchar(255),
    @Description nvarchar(4000) = null,
    @OnClickJS nvarchar(1000) = null,
    @DisplayJS ntext = null,
    @EditJS ntext = null,
    @DisplayCSS ntext = null,
    @SearchRegEx ntext,
    @ReplaceRegEx ntext,
    @Variables nvarchar(1000) = null,
    @UseModule bit = null,
    @ModuleClass nvarchar(255) = null,	
    @ExecOrder int = 1
)
AS
BEGIN
        
    IF @BBCodeID IS NOT NULL BEGIN
        UPDATE
            [{objectQualifier}BBCode]
        SET
            [Name] = @Name,
            [Description] = @Description,
            [OnClickJS] = @OnClickJS,
            [DisplayJS] = @DisplayJS,
            [EditJS] = @EditJS,
            [DisplayCSS] = @DisplayCSS,
            [SearchRegEx] = @SearchRegEx,
            [ReplaceRegEx] = @ReplaceRegEx,
            [Variables] = @Variables,
            [UseModule] = @UseModule,
            [ModuleClass] = @ModuleClass,			
            [ExecOrder] = @ExecOrder
        WHERE
            BBCodeID = @BBCodeID
    END
    ELSE BEGIN
        IF NOT EXISTS(SELECT 1 FROM [{objectQualifier}BBCode] WHERE BoardID = @BoardID AND [Name] = @Name)
            INSERT INTO
                [{objectQualifier}BBCode] ([BoardID],[Name],[Description],[OnClickJS],[DisplayJS],[EditJS],[DisplayCSS],[SearchRegEx],[ReplaceRegEx],[Variables],[UseModule],[ModuleClass],[ExecOrder])
            VALUES (@BoardID,@Name,@Description,@OnClickJS,@DisplayJS,@EditJS,@DisplayCSS,@SearchRegEx,@ReplaceRegEx,@Variables,@UseModule,@ModuleClass,@ExecOrder)
    END
END
GO

-- polls

CREATE procedure [{databaseSchema}].[{objectQualifier}choice_add](
    @PollID		int,
    @Choice		nvarchar(50),
    @ObjectPath nvarchar(255),
    @MimeType nvarchar(50)
) as
begin
    
    insert into [{databaseSchema}].[{objectQualifier}Choice]
        (PollID, Choice, Votes, ObjectPath, MimeType)
        values
        (@PollID, @Choice, 0, @ObjectPath, @MimeType)
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}choice_update](
    @ChoiceID	int,
    @Choice		nvarchar(50),
    @ObjectPath nvarchar(255),
    @MimeType nvarchar(50)
) as
begin
    
    update [{databaseSchema}].[{objectQualifier}Choice]
        set Choice = @Choice, ObjectPath =  @ObjectPath, MimeType = @MimeType
        where ChoiceID = @ChoiceID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}choice_delete](
    @ChoiceID	int
) as
begin
    
    delete from [{databaseSchema}].[{objectQualifier}Choice]
        where ChoiceID = @ChoiceID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}poll_update](
    @PollID		int,
    @Question	nvarchar(50),
    @Closes 	datetime = null,
    @QuestionObjectPath nvarchar(255), 
    @QuestionMimeType varchar(50),
    @IsBounded  bit,
    @IsClosedBounded  bit,
    @AllowMultipleChoices bit,
    @ShowVoters bit,
    @AllowSkipVote bit

) as
begin
    declare @pgid int
    declare @flags int

        update [{databaseSchema}].[{objectQualifier}Poll]
        set Flags	= 0 where PollID = @PollID AND Flags IS NULL;

        SELECT @flags = Flags FROM [{databaseSchema}].[{objectQualifier}Poll]		
        where PollID = @PollID

        -- is closed bound flag
        SET @flags = (CASE				
        WHEN @IsClosedBounded > 0 AND (@flags & 4) <> 4 THEN @flags | 4		
        WHEN @IsClosedBounded <= 0 AND (@flags & 4) = 4  THEN @flags ^ 4
        ELSE @flags END)

        -- allow multiple choices flag
        SET @flags = (CASE				
        WHEN @AllowMultipleChoices > 0 AND (@flags & 8) <> 8 THEN @flags | 8		
        WHEN @AllowMultipleChoices <= 0 AND (@flags & 8) = 8  THEN @flags ^ 8
        ELSE @flags END)
        
        -- show who's voted for a poll flag
        SET @flags = (CASE				
        WHEN @ShowVoters > 0 AND (@flags & 16) <> 16 THEN @flags | 16		
        WHEN @ShowVoters <= 0 AND (@flags & 16) = 16  THEN @flags ^ 16
        ELSE @flags END)

        -- allow users don't vote and see results
        SET @flags = (CASE				
        WHEN @AllowSkipVote > 0 AND (@flags & 32) <> 32 THEN @flags | 32		
        WHEN @AllowSkipVote <= 0 AND (@flags & 32) = 32  THEN @flags ^ 32
        ELSE @flags END)

      update [{databaseSchema}].[{objectQualifier}Poll]
        set Question	=	@Question,
            Closes		=	@Closes,
            ObjectPath = @QuestionObjectPath,
            MimeType = @QuestionMimeType,
            Flags	= @flags
        where PollID = @PollID

      SELECT  @pgid = PollGroupID FROM [{databaseSchema}].[{objectQualifier}Poll]
      where PollID = @PollID
   
    update [{databaseSchema}].[{objectQualifier}PollGroupCluster]
        set Flags	= (CASE 
        WHEN @IsBounded > 0 AND (Flags & 2) <> 2 THEN Flags | 2 		
        WHEN @IsBounded <= 0 AND (Flags & 2) = 2 THEN Flags ^ 2 		
        ELSE Flags END)		
        where PollGroupID = @pgid
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}poll_remove](
    @PollGroupID int, @PollID int = null, @BoardID int = null, @RemoveCompletely bit, @RemoveEverywhere bit)
as
begin
declare @groupcount int
    
    if @RemoveCompletely = 1 
    begin
    -- delete vote records first
    delete from [{databaseSchema}].[{objectQualifier}PollVote] where PollID = @PollID
    -- delete choices 
    delete from [{databaseSchema}].[{objectQualifier}Choice] where PollID = @PollID
    -- delete poll
    Update [{databaseSchema}].[{objectQualifier}Poll] set PollGroupID = NULL where PollID = @PollID
    delete from [{databaseSchema}].[{objectQualifier}Poll] where PollID = @PollID 	
    if  NOT EXISTS (SELECT TOP 1 1 FROM [{databaseSchema}].[{objectQualifier}Poll] where PollGroupID = @PollGroupID) 
        begin	
              
                   Update [{databaseSchema}].[{objectQualifier}Topic] set PollID = NULL where PollID = @PollGroupID                 
                  
                   
                   Update [{databaseSchema}].[{objectQualifier}Forum] set PollGroupID = NULL where PollGroupID = @PollGroupID
              
    
                   Update [{databaseSchema}].[{objectQualifier}Category] set PollGroupID = NULL where PollGroupID = @PollGroupID                
        
              
        
       
         
        DELETE FROM  [{databaseSchema}].[{objectQualifier}PollGroupCluster] WHERE PollGroupID = @PollGroupID	
        end  	
    end
    else
    begin    
    Update [{databaseSchema}].[{objectQualifier}Poll] set PollGroupID = NULL where PollID = @PollID	                         
    end

end
GO

-- medals

create proc [{databaseSchema}].[{objectQualifier}group_medal_delete]
    @GroupID int,
    @MedalID int
as begin
    
    delete from [{databaseSchema}].[{objectQualifier}GroupMedal] where [GroupID]=@GroupID and [MedalID]=@MedalID
end
GO

CREATE proc [{databaseSchema}].[{objectQualifier}group_medal_list]
    @GroupID int = null,
    @MedalID int = null
as begin
    
    select 
        a.[MedalID],
        a.[Name],
        a.[MedalURL],
        a.[RibbonURL],
        a.[SmallMedalURL],
        a.[SmallRibbonURL],
        a.[SmallMedalWidth],
        a.[SmallMedalHeight],
        a.[SmallRibbonWidth],
        a.[SmallRibbonHeight],
        b.[SortOrder],
        a.[Flags],
        c.[Name] as [GroupName],
        b.[GroupID],
        isnull(b.[Message],a.[Message]) as [Message],
        b.[Message] as [MessageEx],
        b.[Hide],
        b.[OnlyRibbon],
        b.[SortOrder] as CurrentSortOrder
    from
        [{databaseSchema}].[{objectQualifier}Medal] a
        inner join [{databaseSchema}].[{objectQualifier}GroupMedal] b on b.[MedalID] = a.[MedalID]
        inner join [{databaseSchema}].[{objectQualifier}Group] c on  c.[GroupID] = b.[GroupID]
    where
        (@GroupID is null or b.[GroupID] = @GroupID) and
        (@MedalID is null or b.[MedalID] = @MedalID)		
    order by
        c.[Name] ASC,
        b.[SortOrder] ASC

end
GO

create proc [{databaseSchema}].[{objectQualifier}group_medal_save]
   @GroupID int,
   @MedalID int,
   @Message nvarchar(100) = NULL,
   @Hide bit,
   @OnlyRibbon bit,
   @SortOrder tinyint
as begin
    
    if exists(select 1 from [{databaseSchema}].[{objectQualifier}GroupMedal] where [GroupID]=@GroupID and [MedalID]=@MedalID) begin
        update [{databaseSchema}].[{objectQualifier}GroupMedal]
        set
            [Message] = @Message,
            [Hide] = @Hide,
            [OnlyRibbon] = @OnlyRibbon,
            [SortOrder] = @SortOrder
        where 
            [GroupID]=@GroupID and 
            [MedalID]=@MedalID
    end
    else begin

        insert into [{databaseSchema}].[{objectQualifier}GroupMedal]
            ([GroupID],[MedalID],[Message],[Hide],[OnlyRibbon],[SortOrder])
        values
            (@GroupID,@MedalID,@Message,@Hide,@OnlyRibbon,@SortOrder)
    end

end
GO

CREATE proc [{databaseSchema}].[{objectQualifier}medal_delete]
    @BoardID	int = null,
    @MedalID	int = null,
    @Category	nvarchar(50) = null
as begin
    
    if not @MedalID is null begin
        delete from [{databaseSchema}].[{objectQualifier}GroupMedal] where [MedalID] = @MedalID
        delete from [{databaseSchema}].[{objectQualifier}UserMedal] where [MedalID] = @MedalID

        delete from [{databaseSchema}].[{objectQualifier}Medal] where [MedalID]=@MedalID
    end
    else if not @Category is null and not @BoardID is null begin
        delete from [{databaseSchema}].[{objectQualifier}GroupMedal] 
            where [MedalID] in (SELECT [MedalID] FROM [{databaseSchema}].[{objectQualifier}Medal] where [Category]=@Category and [BoardID]=@BoardID)

        delete from [{databaseSchema}].[{objectQualifier}UserMedal] 
            where [MedalID] in (SELECT [MedalID] FROM [{databaseSchema}].[{objectQualifier}Medal] where [Category]=@Category and [BoardID]=@BoardID)

        delete from [{databaseSchema}].[{objectQualifier}Medal] where [Category]=@Category
    end
    else if not @BoardID is null begin
        delete from [{databaseSchema}].[{objectQualifier}GroupMedal] 
            where [MedalID] in (SELECT [MedalID] FROM [{databaseSchema}].[{objectQualifier}Medal] where [BoardID]=@BoardID)

        delete from [{databaseSchema}].[{objectQualifier}UserMedal] 
            where [MedalID] in (SELECT [MedalID] FROM [{databaseSchema}].[{objectQualifier}Medal] where [BoardID]=@BoardID)

        delete from [{databaseSchema}].[{objectQualifier}Medal] where [BoardID]=@BoardID
    end

end
GO

CREATE proc [{databaseSchema}].[{objectQualifier}medal_list]
    @BoardID	int = null,
    @MedalID	int = null,
    @Category	nvarchar(50) = null
as begin
        if not @MedalID is null begin
        select 
            * 
        from 
            [{databaseSchema}].[{objectQualifier}Medal] 
        where 
            [MedalID]=@MedalID 
        order by 
            [Category] asc, 
            [SortOrder] asc
    end
    else if not @Category is null and not @BoardID is null begin
        select 
            * 
        from 
            [{databaseSchema}].[{objectQualifier}Medal] 
        where 
            [Category]=@Category and [BoardID]=@BoardID
        order by 
            [Category] asc, 
            [SortOrder] asc
    end
    else if not @BoardID is null begin
        select 
            * 
        from 
            [{databaseSchema}].[{objectQualifier}Medal] 
        where 
            [BoardID]=@BoardID
        order by 
            [Category] asc, 
            [SortOrder] asc
    end

end
GO

CREATE proc [{databaseSchema}].[{objectQualifier}medal_listusers]
    @MedalID	int
as begin
        (select 
        a.UserID, a.Name
    from 
        [{databaseSchema}].[{objectQualifier}User] a
        inner join [{databaseSchema}].[{objectQualifier}UserMedal] b on a.[UserID] = b.[UserID]
    where
        b.[MedalID]=@MedalID) 
    
    union	

    (select 
        a.UserID, a.Name
    from 
        [{databaseSchema}].[{objectQualifier}User] a
        inner join [{databaseSchema}].[{objectQualifier}UserGroup] b on a.[UserID] = b.[UserID]
        inner join [{databaseSchema}].[{objectQualifier}GroupMedal] c on b.[GroupID] = c.[GroupID]
    where
        c.[MedalID]=@MedalID) 


end
GO

create proc [{databaseSchema}].[{objectQualifier}medal_resort]
    @BoardID int,@MedalID int,@Move int
as
begin
        declare @Position int
    declare @Category nvarchar(50)

    select 
        @Position=[SortOrder],
        @Category=[Category]
    from 
        [{databaseSchema}].[{objectQualifier}Medal] 
    where 
        [BoardID]=@BoardID and [MedalID]=@MedalID

    if (@Position is null) return

    if (@Move > 0) begin
        update 
            [{databaseSchema}].[{objectQualifier}Medal]
        set 
            [SortOrder]=[SortOrder]-1
        where 
            [BoardID]=@BoardID and 
            [Category]=@Category and
            [SortOrder] between @Position and (@Position + @Move) and
            [SortOrder] between 1 and 255
    end
    else if (@Move < 0) begin
        update 
            [{databaseSchema}].[{objectQualifier}Medal]
        set
            [SortOrder]=[SortOrder]+1
        where 
            BoardID=@BoardID and 
            [Category]=@Category and
            [SortOrder] between (@Position+@Move) and @Position and
            [SortOrder] between 0 and 254
    end

    SET @Position = @Position + @Move

    if (@Position>255) SET @Position = 255
    else if (@Position<0) SET @Position = 0

    update [{databaseSchema}].[{objectQualifier}Medal]
        set [SortOrder]=@Position
        where [BoardID]=@BoardID and 
            [MedalID]=@MedalID
end
GO

CREATE proc [{databaseSchema}].[{objectQualifier}medal_save]
    @BoardID int = NULL,
    @MedalID int = NULL,
    @Name nvarchar(100),
    @Description ntext,
    @Message nvarchar(100),
    @Category nvarchar(50) = NULL,
    @MedalURL nvarchar(250),
    @RibbonURL nvarchar(250) = NULL,
    @SmallMedalURL nvarchar(250),
    @SmallRibbonURL nvarchar(250) = NULL,
    @SmallMedalWidth smallint,
    @SmallMedalHeight smallint,
    @SmallRibbonWidth smallint = NULL,
    @SmallRibbonHeight smallint = NULL,
    @SortOrder tinyint = 255,
    @Flags int = 0
as begin
        if @MedalID is null begin
        insert into [{databaseSchema}].[{objectQualifier}Medal]
            ([BoardID],[Name],[Description],[Message],[Category],
            [MedalURL],[RibbonURL],[SmallMedalURL],[SmallRibbonURL],
            [SmallMedalWidth],[SmallMedalHeight],[SmallRibbonWidth],[SmallRibbonHeight],
            [SortOrder],[Flags])
        values
            (@BoardID,@Name,@Description,@Message,@Category,
            @MedalURL,@RibbonURL,@SmallMedalURL,@SmallRibbonURL,
            @SmallMedalWidth,@SmallMedalHeight,@SmallRibbonWidth,@SmallRibbonHeight,
            @SortOrder,@Flags)

        select @@rowcount
    end
    else begin
        update [{databaseSchema}].[{objectQualifier}Medal]
            set [BoardID] = BoardID,
                [Name] = @Name,
                [Description] = @Description,
                [Message] = @Message,
                [Category] = @Category,
                [MedalURL] = @MedalURL,
                [RibbonURL] = @RibbonURL,
                [SmallMedalURL] = @SmallMedalURL,
                [SmallRibbonURL] = @SmallRibbonURL,
                [SmallMedalWidth] = @SmallMedalWidth,
                [SmallMedalHeight] = @SmallMedalHeight,
                [SmallRibbonWidth] = @SmallRibbonWidth,
                [SmallRibbonHeight] = @SmallRibbonHeight,
                [SortOrder] = @SortOrder,
                [Flags] = @Flags
        where [MedalID] = @MedalID

        select @@rowcount
    end

end
GO

create proc [{databaseSchema}].[{objectQualifier}user_listmedals]
    @UserID	int
as begin
        (select
        a.[MedalID],
        a.[Name],
        isnull(b.[Message], a.[Message]) as [Message],
        a.[MedalURL],
        a.[RibbonURL],
        a.[SmallMedalURL],
        isnull(a.[SmallRibbonURL], a.[SmallMedalURL]) as [SmallRibbonURL],
        a.[SmallMedalWidth],
        a.[SmallMedalHeight],
        isnull(a.[SmallRibbonWidth], a.[SmallMedalWidth]) as [SmallRibbonWidth],
        isnull(a.[SmallRibbonHeight], a.[SmallMedalHeight]) as [SmallRibbonHeight],
        [{databaseSchema}].[{objectQualifier}medal_getsortorder](b.[SortOrder],a.[SortOrder],a.[Flags]) as [SortOrder],
        [{databaseSchema}].[{objectQualifier}medal_gethide](b.[Hide],a.[Flags]) as [Hide],
        [{databaseSchema}].[{objectQualifier}medal_getribbonsetting](a.[SmallRibbonURL],a.[Flags],b.[OnlyRibbon]) as [OnlyRibbon],
        a.[Flags],
        b.[DateAwarded]
    from
        [{databaseSchema}].[{objectQualifier}Medal] a
        inner join [{databaseSchema}].[{objectQualifier}UserMedal] b on a.[MedalID] = b.[MedalID]
    where
        b.[UserID] = @UserID)

    union

    (select
        a.[MedalID],
        a.[Name],
        isnull(b.[Message], a.[Message]) as [Message],
        a.[MedalURL],
        a.[RibbonURL],
        a.[SmallMedalURL],
        isnull(a.[SmallRibbonURL], a.[SmallMedalURL]) as [SmallRibbonURL],
        a.[SmallMedalWidth],
        a.[SmallMedalHeight],
        isnull(a.[SmallRibbonWidth], a.[SmallMedalWidth]) as [SmallRibbonWidth],
        isnull(a.[SmallRibbonHeight], a.[SmallMedalHeight]) as [SmallRibbonHeight],
        [{databaseSchema}].[{objectQualifier}medal_getsortorder](b.[SortOrder],a.[SortOrder],a.[Flags]) as [SortOrder],
        [{databaseSchema}].[{objectQualifier}medal_gethide](b.[Hide],a.[Flags]) as [Hide],
        [{databaseSchema}].[{objectQualifier}medal_getribbonsetting](a.[SmallRibbonURL],a.[Flags],b.[OnlyRibbon]) as [OnlyRibbon],
        a.[Flags],
        NULL as [DateAwarded]
    from
        [{databaseSchema}].[{objectQualifier}Medal] a
        inner join [{databaseSchema}].[{objectQualifier}GroupMedal] b on a.[MedalID] = b.[MedalID]
        inner join [{databaseSchema}].[{objectQualifier}UserGroup] c on b.[GroupID] = c.[GroupID]
    where
        c.[UserID] = @UserID)
    order by
        [OnlyRibbon] desc,
        [SortOrder] asc

end
GO

create proc [{databaseSchema}].[{objectQualifier}user_medal_delete]
    @UserID int,
    @MedalID int
as begin
        delete from [{databaseSchema}].[{objectQualifier}UserMedal] where [UserID]=@UserID and [MedalID]=@MedalID

end
GO

create proc [{databaseSchema}].[{objectQualifier}user_medal_list]
    @UserID int = null,
    @MedalID int = null,
	@UTCTIMESTAMP datetime
as begin
        select 
        a.[MedalID],
        a.[Name],
        a.[MedalURL],
        a.[RibbonURL],
        a.[SmallMedalURL],
        a.[SmallRibbonURL],
        a.[SmallMedalWidth],
        a.[SmallMedalHeight],
        a.[SmallRibbonWidth],
        a.[SmallRibbonHeight],
        b.[SortOrder],
        a.[Flags],
        c.[Name] as [UserName],
        c.[DisplayName] as [DisplayName],
        b.[UserID],
        isnull(b.[Message],a.[Message]) as [Message],
        b.[Message] as [MessageEx],
        b.[Hide],
        b.[OnlyRibbon],
        b.[SortOrder] as [CurrentSortOrder],
        b.[DateAwarded]
    from
        [{databaseSchema}].[{objectQualifier}Medal] a
        inner join [{databaseSchema}].[{objectQualifier}UserMedal] b on b.[MedalID] = a.[MedalID]
        inner join [{databaseSchema}].[{objectQualifier}User] c on c.[UserID] = b.[UserID]
    where
        (@UserID is null or b.[UserID] = @UserID) and
        (@MedalID is null or b.[MedalID] = @MedalID)		
    order by
        c.[Name] ASC,
        b.[SortOrder] ASC

end
GO

create proc [{databaseSchema}].[{objectQualifier}user_medal_save]
    @UserID int,
    @MedalID int,
    @Message nvarchar(100) = NULL,
    @Hide bit,
    @OnlyRibbon bit,
    @SortOrder tinyint,
    @DateAwarded datetime = NULL,
    @UTCTIMESTAMP datetime
as begin
        if exists(select 1 from [{databaseSchema}].[{objectQualifier}UserMedal] where [UserID]=@UserID and [MedalID]=@MedalID) begin
        update [{databaseSchema}].[{objectQualifier}UserMedal]
        set
            [Message] = @Message,
            [Hide] = @Hide,
            [OnlyRibbon] = @OnlyRibbon,
            [SortOrder] = @SortOrder
        where 
            [UserID]=@UserID and 
            [MedalID]=@MedalID
    end
    else begin

        if (@DateAwarded is null) set @DateAwarded = @UTCTIMESTAMP  

        insert into [{databaseSchema}].[{objectQualifier}UserMedal]
            ([UserID],[MedalID],[Message],[Hide],[OnlyRibbon],[SortOrder],[DateAwarded])
        values
            (@UserID,@MedalID,@Message,@Hide,@OnlyRibbon,@SortOrder,@DateAwarded)
    end

end
GO

create procedure [{databaseSchema}].[{objectQualifier}pollgroup_save]    
    @TopicID int = null,
    @ForumID int= null,
	@CategoryID int = null,	
	@UserID int = null,	
	@Flags int = null,	
	@UTCTIMESTAMP datetime
as 
begin
declare @PollGroupID int;
declare @NewPollGroupID int;

 -- Check if the group already exists
  if (@TopicId is not null)  
                select @PollGroupID = PollID  from [{databaseSchema}].[{objectQualifier}Topic] WHERE TopicID = @TopicID;               
     else if (@ForumID is not null)                
				select @PollGroupID = PollGroupID  from [{databaseSchema}].[{objectQualifier}Forum] WHERE ForumID = @ForumID;                
         else if (@CategoryID is not null)
                select @PollGroupID = PollGroupID  from [{databaseSchema}].[{objectQualifier}Category]
				WHERE CategoryID = @CategoryID;
			if (@PollGroupID is null) 	
				begin
				  INSERT INTO [{databaseSchema}].[{objectQualifier}PollGroupCluster](UserID,Flags ) VALUES(@UserID, @Flags);
				   
				  SET @NewPollGroupID = SCOPE_IDENTITY(); 
                end

      SELECT PollGroupID = @PollGroupID, NewPollGroupID = @NewPollGroupID;				
end
GO

create procedure [{databaseSchema}].[{objectQualifier}poll_save]    
    @Question nvarchar(255), 
    @Closes datetime,
	@UserID int,	
	@PollGroupID int,
	@ObjectPath nvarchar(255),  
	@MimeType varchar(50), 
	@Flags int,
	@UTCTIMESTAMP datetime
as 
begin
declare @PollID int

 insert [{databaseSchema}].[{objectQualifier}Poll](question,closes,pollgroupid,userid,objectpath,mimetype,flags)
 values(@Question,@Closes,@PollGroupID,@UserID,@ObjectPath,@MimeType,@Flags); 
 set @PollID = SCOPE_IDENTITY();
 select @PollID;				
end
GO

create procedure [{databaseSchema}].[{objectQualifier}choice_save]    
    @PollID int, 
    @Choice nvarchar(255),
	@Votes int,	
	@ObjectPath nvarchar(255),  
	@MimeType varchar(50), 	
	@UTCTIMESTAMP datetime
as 
begin
/* DECLARE @sItem NVARCHAR(MAX)
WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
 BEGIN
 SELECT
  @sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1))),
  @sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))
 
 IF LEN(@sItem) > 0
 insert [{databaseSchema}].[{objectQualifier}Poll](question,closes,pollgroupid,userid,objectpath,mimetype,flags)
 values(@Question,@Closes,@PollGroupID,@UserID,@ObjectPath,@MimeType,@Flags); 
 set @PollID = SCOPE_IDENTITY();
 select @PollID;		
 END

IF LEN(@sInputList) > 0
 INSERT INTO @List SELECT @sInputList */



 insert [{databaseSchema}].[{objectQualifier}Choice](PollID,Choice,Votes,ObjectPath,MimeType)
 values(@PollID,@Choice,@Votes,@ObjectPath,@MimeType); 
 set @PollID = SCOPE_IDENTITY();
 select PollID = @PollID;				
end
GO

create procedure [{databaseSchema}].[{objectQualifier}pollgroup_setlinks]    
   	@TopicID int,
    @ForumID int,
	@CategoryID int,	
	@PollGroupID int,
	@UTCTIMESTAMP datetime
as 
begin               
               -- we don't update if no new group is being created 
                IF  @PollGroupID IS NOT NULL 
				BEGIN 
                -- fill a pollgroup field - double work if a poll exists 
                if (@TopicId is not null)
                    UPDATE [{databaseSchema}].[{objectQualifier}Topic] SET PollID = @PollGroupID WHERE TopicID = @TopicID; 
				-- fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
                else if (@ForumID is not null)                 
                    UPDATE [{databaseSchema}].[{objectQualifier}Forum] SET PollGroupID= @PollGroupID WHERE ForumID= @ForumID; 
                -- fill a pollgroup field in Category Table if the call comes from a category's topic list 
                else if (@CategoryID is not null)              
                    UPDATE [{databaseSchema}].[{objectQualifier}Category] SET PollGroupID= @PollGroupID WHERE CategoryID= @CategoryID;                 

                -- fill a pollgroup field in Board Table if the call comes from the main page poll 
                END;  

                -- the functionality is primitive for other databases compliance.It will save calls for each choice. 				
end
GO

/* User Ignore Procedures */

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_addignoreduser]
    @UserId int,
    @IgnoredUserId int
AS BEGIN
        IF NOT EXISTS (SELECT * FROM [{databaseSchema}].[{objectQualifier}IgnoreUser] WHERE UserID = @userId AND IgnoredUserID = @IgnoredUserId)
    BEGIN
        INSERT INTO [{databaseSchema}].[{objectQualifier}IgnoreUser] (UserID, IgnoredUserID) VALUES (@UserId, @IgnoredUserId)
    END
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_removeignoreduser]
    @UserId int,
    @IgnoredUserId int
AS BEGIN
        DELETE FROM [{databaseSchema}].[{objectQualifier}IgnoreUser] WHERE UserID = @userId AND IgnoredUserID = @IgnoredUserId
    
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_isuserignored]
    @UserId int,
    @IgnoredUserId int
AS BEGIN
        IF EXISTS(SELECT * FROM [{databaseSchema}].[{objectQualifier}IgnoreUser] WHERE UserID = @userId AND IgnoredUserID = @IgnoredUserId)
    BEGIN
        RETURN 1
    END
    ELSE
    BEGIN
        RETURN 0
    END
    
END	
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_ignoredlist]
    @UserId int
AS
BEGIN
        SELECT * FROM [{databaseSchema}].[{objectQualifier}IgnoreUser] WHERE UserID = @userId
END	
GO

/*****************************************************************************************************
//  Original code by: DLESKTECH at http://www.dlesktech.com/support.aspx
//  Modifications by: KASL Technologies at www.kasltechnologies.com
//  Modifications for integration into YAF/Conventions by Jaben Cargman
*****************************************************************************************************/

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}shoutbox_getmessages]
(
  @BoardID int,
  @NumberOfMessages int, 
  @StyledNicks bit = 0
)  
AS
begin
declare @PageIndex int = 0
declare @PageSize int = @NumberOfMessages
declare @TotalRows int
declare @FirstSelectRowNumber int
declare @LastSelectRowNumber int

           set @PageIndex = @PageIndex + 1;
           set @FirstSelectRowNumber = 0;
           set @LastSelectRowNumber = 0;
           set @TotalRows = 0;
           
           select @TotalRows = count(1) from [{databaseSchema}].[{objectQualifier}BannedIP] where BoardID=@BoardID;
           select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
           select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
           
           with MessageIds as
           (
             select ROW_NUMBER() over (order by [Date]) as RowNum, [ShoutBoxMessageID]
			 from
             [{databaseSchema}].[{objectQualifier}ShoutboxMessage] 
             WHERE 
             BoardId = @BoardID
           )
           select
            sh.[ShoutBoxMessageID],
			sh.UserID,
            sh.UserName,
            sh.UserDisplayName,            
            sh.[Message],
            sh.[Date], 
            Style = case(@StyledNicks)
            when 1 then  usr.UserStyle
            else ''	 end,
            @TotalRows as TotalRows
            from
            MessageIds mi
            inner join  [{databaseSchema}].[{objectQualifier}ShoutboxMessage] sh
			on mi.[ShoutBoxMessageID] = sh.[ShoutBoxMessageID]
            JOIN [{databaseSchema}].[{objectQualifier}User] usr on usr.UserID = sh.UserID
            where mi.RowNum between (@FirstSelectRowNumber) and (@LastSelectRowNumber)
            order by mi.RowNum asc  
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}shoutbox_savemessage](
    @UserName		nvarchar(255)=null,
    @BoardId		int,
    @UserID			int,
    @Message		ntext,
    @Date			datetime=null,
    @IP				varchar(39),
    @UTCTIMESTAMP datetime
)
AS
BEGIN
DECLARE @OverrideDisplayName BIT, @ReplaceName nvarchar(255)
        IF @Date IS NULL
        SET @Date = @UTCTIMESTAMP 
        -- this check is for guest user only to not override replace name 
if (SELECT Name FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserID = @UserID) != @UserName
    begin
    SET @OverrideDisplayName = 1
    end	
    SET @ReplaceName = (CASE WHEN @OverrideDisplayName = 1 THEN @UserName ELSE (SELECT DisplayName FROM [{databaseSchema}].[{objectQualifier}User] WHERE UserID = @UserID) END);
    INSERT [{databaseSchema}].[{objectQualifier}ShoutboxMessage] (UserName,UserDisplayName,BoardId, UserID, Message, Date, IP)
    VALUES (@UserName,@ReplaceName, @BoardId, @UserID, @Message, @Date, @IP)
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}shoutbox_clearmessages]
(
    @BoardId int,
    @UTCTIMESTAMP datetime
)
AS
BEGIN
        DELETE FROM
            [{databaseSchema}].[{objectQualifier}ShoutboxMessage]
        WHERE
            BoardId = @BoardId AND
            DATEDIFF(minute, Date, @UTCTIMESTAMP ) > 1
END
GO

/* Stored procedures for Buddy feature */

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_addrequest]
    @FromUserID INT,
    @ToUserID INT,
	@UseDisplayName BIT,
    @UTCTIMESTAMP datetime  
AS 
    BEGIN
	DECLARE
	  @approved BIT = NULL,	@paramOutput NVARCHAR(255) = NULL 
        IF NOT EXISTS ( SELECT  ID
                        FROM    [{databaseSchema}].[{objectQualifier}Buddy]
                        WHERE   ( FromUserID = @FromUserID
                                  AND ToUserID = @ToUserID
                                ) ) 
            BEGIN
                IF ( NOT EXISTS ( SELECT    ID
                                  FROM      [{databaseSchema}].[{objectQualifier}Buddy]
                                  WHERE     ( FromUserID = @ToUserID
                                              AND ToUserID = @FromUserID
                                            ) )
                   ) 
                    BEGIN
                        INSERT  INTO [{databaseSchema}].[{objectQualifier}Buddy]
                                (
                                  FromUserID,
                                 ToUserID,
                                  Approved,
                                  Requested
                                )
                        VALUES  (
                                  @FromUserID,
                                  @ToUserID,
                                  0,
                                  @UTCTIMESTAMP 
                                )
                        SET @paramOutput = (SELECT (CASE WHEN @UseDisplayName = 1 THEN [DisplayName] ELSE [Name] END)
                                             FROM   [{databaseSchema}].[{objectQualifier}User]
                                             WHERE  ( UserID = @ToUserID )
                                           )
                        SET @approved = 0
                    END
                ELSE 
                    BEGIN
                        INSERT  INTO [{databaseSchema}].[{objectQualifier}Buddy]
                                (
                                  FromUserID,
                                  ToUserID,
                                  Approved,
                                  Requested
                                )
                        VALUES  (
                                  @FromUserID,
                                  @ToUserID,
                                  1,
                                  @UTCTIMESTAMP 
                                )
                        UPDATE  [{databaseSchema}].[{objectQualifier}Buddy]
                        SET     Approved = 1
                        WHERE   ( FromUserID = @ToUserID
                                  AND ToUserID = @FromUserID
                                )
                        SET @paramOutput = ( SELECT (CASE WHEN @UseDisplayName = 1 THEN [DisplayName] ELSE [Name] END)
                                             FROM   [{databaseSchema}].[{objectQualifier}User]
                                             WHERE  ( UserID = @ToUserID )
                                           )
                        SET @approved = 1
                    END
            END	
        ELSE 
            BEGIN
                SET @paramOutput = ''
                SET @approved = 0
            END
			SELECT @approved as i_approved,	@paramOutput as i_paramOutput

    END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_approverequest]
    @FromUserID INT,
    @ToUserID INT,
    @Mutual BIT,
	@UseDisplayName BIT,
    @UTCTIMESTAMP datetime
AS 
    BEGIN
	DECLARE   @paramOutput NVARCHAR(255) = NULL
        IF EXISTS ( SELECT  ID
                    FROM    [{databaseSchema}].[{objectQualifier}Buddy]
                    WHERE   ( FromUserID = @FromUserID
                              AND ToUserID = @ToUserID
                            ) ) 
            BEGIN
                UPDATE  [{databaseSchema}].[{objectQualifier}Buddy]
                SET     Approved = 1
                WHERE   ( FromUserID = @FromUserID
                          AND ToUserID = @ToUserID
                        )
                SET @paramOutput = ( SELECT  (CASE WHEN @UseDisplayName = 1 THEN [DisplayName] ELSE [Name] END)
                                     FROM   [{databaseSchema}].[{objectQualifier}User]
                                     WHERE  ( UserID = @FromUserID )
                                   )
                IF ( @Mutual = 1 )
                    AND ( NOT EXISTS ( SELECT   ID
                                       FROM     [{databaseSchema}].[{objectQualifier}Buddy]
                                       WHERE    FromUserID = @ToUserID
                                                AND ToUserID = @FromUserID )
                        ) 
                    INSERT  INTO [{databaseSchema}].[{objectQualifier}Buddy]
                            (
                              FromUserID,
                              ToUserID,
                              Approved,
                              Requested
                            )
                    VALUES  (
                              @ToUserID,
                              @FromUserID,
                              1,
                              @UTCTIMESTAMP 
                            )
            END
			SELECT @paramOutput
    END
GO

    CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_list] @FromUserID INT
AS 
    BEGIN
        SELECT  a.UserID,
                a.BoardID,
                a.[Name],
				a.[DisplayName],
                a.Joined,
                a.NumPosts,
                RankName = b.NAME,
                c.Approved,
                c.FromUserID,
                c.Requested
        FROM   [{databaseSchema}].[{objectQualifier}User] a
                JOIN [{databaseSchema}].[{objectQualifier}Rank] b ON b.RankID = a.RankID
                JOIN [{databaseSchema}].[{objectQualifier}Buddy] c ON ( c.ToUserID = a.UserID
                                              AND c.FromUserID = @FromUserID
                                            )
        UNION
        SELECT  @FromUserID AS UserID,
                a.BoardID,
                a.[Name],
				a.[DisplayName],
                a.Joined,
                a.NumPosts,
                RankName = b.NAME,
                c.Approved,
                c.FromUserID,
                c.Requested
        FROM    [{databaseSchema}].[{objectQualifier}User] a
                JOIN [{databaseSchema}].[{objectQualifier}Rank] b ON b.RankID = a.RankID
                JOIN [{databaseSchema}].[{objectQualifier}Buddy] c ON ( ( c.Approved = 0 )
                                              AND ( c.ToUserID = @FromUserID )
                                              AND ( a.UserID = c.FromUserID )
                                            )
        ORDER BY a.NAME
    END
    GO

    CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_remove]
    @FromUserID INT,
    @ToUserID INT,
	@UseDisplayName BIT,
    @paramOutput NVARCHAR(255) = NULL OUT
AS 
    BEGIN
        DELETE  FROM [{databaseSchema}].[{objectQualifier}Buddy]
        WHERE   ( FromUserID = @FromUserID
                  AND ToUserID = @ToUserID
                )
        SET @paramOutput = ( SELECT (CASE WHEN @UseDisplayName = 1 THEN [DisplayName] ELSE [Name] END)
                             FROM   [{databaseSchema}].[{objectQualifier}User]
                             WHERE  ( UserID = @ToUserID )
                           )
    END
    GO
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}buddy_denyrequest]
    @FromUserID INT,
    @ToUserID INT,
	@UseDisplayName BIT
AS 
    BEGIN
        DELETE  FROM [{databaseSchema}].[{objectQualifier}Buddy]
        WHERE   FromUserID = @FromUserID
                AND ToUserID = @ToUserID
        SELECT (CASE WHEN @UseDisplayName = 1 THEN [DisplayName] ELSE [Name] END)
                             FROM   [{databaseSchema}].[{objectQualifier}User]
                             WHERE  (UserID = @FromUserID)
                           
    END
Go    
/* End of stored procedures for Buddy feature */

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_add] 
    @UserID int,
    @TopicID int
AS
BEGIN
    IF NOT EXISTS (SELECT ID FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE (UserID = @UserID AND TopicID=@TopicID))
    BEGIN
        INSERT INTO [{databaseSchema}].[{objectQualifier}FavoriteTopic] (UserID, TopicID) Values 
                                (@UserID, @TopicID)
    END
END
Go

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_remove] 
    @UserID int,
    @TopicID int
AS
BEGIN
    DELETE FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE UserID=@UserID AND TopicID=@TopicID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_list](@UserID int) as
BEGIN
SELECT TopicID FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE UserID=@UserID
END
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_favorite_count](@TopicID int) as
BEGIN
    SELECT COUNT(ID) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = @TopicID
END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}topic_favorite_details]
(   @BoardID int,
    @CategoryID int=null,
    @PageUserID int,		
    @SinceDate datetime=null,
    @ToDate datetime,
    @PageIndex int = 1, 
    @PageSize int = 0, 
    @StyledNicks bit = 0,	
    @FindLastUnread bit = 0,
	@UTCTIMESTAMP datetime
)
AS
begin
   declare @TotalRows int
   declare @FirstSelectRowNumber int
   declare @LastSelectRowNumber int

   -- find total returned count
   select @TotalRows = count(1)	
        from
        [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
        JOIN [{databaseSchema}].[{objectQualifier}FavoriteTopic] z ON z.TopicID=c.TopicID AND z.UserID=@PageUserID
    where
        (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
    
    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
    
    with TopicIds as
     (
     select ROW_NUMBER() over (order by cat.SortOrder asc, d.SortOrder asc, c.LastPosted desc) as RowNum, c.TopicID
     from [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
        JOIN [{databaseSchema}].[{objectQualifier}FavoriteTopic] z ON z.TopicID=c.TopicID AND z.UserID=@PageUserID
    where
        (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
      )	
      select
	    c.ForumID,
        c.TopicID,
        c.TopicMovedID,
        c.Posted,
        LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),
        [Subject] = c.Topic,
        [Description] = c.Description,
        [Status] = c.Status,
        [Styles] = c.Styles,
        c.UserID,
        Starter = IsNull(c.UserName,b.Name),
        StarterDisplay = IsNull(c.UserDisplayName,b.DisplayName),
        NumPostsDeleted = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] mes WHERE mes.TopicID = c.TopicID AND mes.IsDeleted = 1 AND mes.IsApproved = 1 AND ((@PageUserID IS NOT NULL AND mes.UserID = @PageUserID) OR (@PageUserID IS NULL)) ),
        Replies = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=c.TopicID and x.IsDeleted=0) - 1,
        [Views] = c.[Views],
        LastPosted = c.LastPosted,
        LastUserID = c.LastUserID,
        LastUserName = IsNull(c.LastUserName,(select x.Name from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastUserDisplayName = IsNull(c.LastUserDisplayName,(select x.DisplayName from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastMessageID = c.LastMessageID,
        LastMessageFlags = c.LastMessageFlags,
        LastTopicID = c.TopicID,
        TopicFlags = c.Flags,
        FavoriteCount = (SELECT COUNT(ID) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = IsNull(c.TopicMovedID,c.TopicID)),
        c.Priority,
        c.PollID,
        ForumName = d.Name,
        c.TopicMovedID,
        ForumFlags = d.Flags,
        FirstMessage = (SELECT TOP 1 CAST([Message] as nvarchar(1000)) FROM [{databaseSchema}].[{objectQualifier}Message] mes2 where mes2.TopicID = IsNull(c.TopicMovedID,c.TopicID) AND mes2.Position = 0),
        StarterStyle = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end,
        LastUserStyle= case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = c.LastUserID)
            else ''	 end,
        LastForumAccess = case(@FindLastUnread)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x WHERE x.ForumID=d.ForumID AND x.UserID = @PageUserID)
             else ''	 end,
        LastTopicAccess = case(@FindLastUnread)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y WHERE y.TopicID=c.TopicID AND y.UserID = @PageUserID)
             else ''	 end,
        [{databaseSchema}].[{objectQualifier}topic_gettags_str](c.TopicID) as TopicTags,
        c.TopicImage,
        c.TopicImageType,
        c.TopicImageBin,
        0 as HasAttachments, 
        TotalRows = @TotalRows,
        PageIndex = @PageIndex	
    from
        TopicIds ti
        inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = ti.TopicID
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
    where ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}album_save]
    (
      @AlbumID INT = NULL,
      @UserID INT = null,
      @Title NVARCHAR(255) = NULL,
      @CoverImageID INT = NULL,
      @UTCTIMESTAMP datetime
    )
as 
    BEGIN
	DECLARE @Result INT = @AlbumID;
        -- Update Cover?
        IF ( @CoverImageID IS NOT NULL
             AND @CoverImageID <> 0
           ) 
            UPDATE  [{databaseSchema}].[{objectQualifier}UserAlbum]
            SET     CoverImageID = @CoverImageID
            WHERE   AlbumID = @AlbumID
        ELSE 
            --Remove Cover?
            IF ( @CoverImageID = 0 ) 
                UPDATE  [{databaseSchema}].[{objectQualifier}UserAlbum]
                SET     CoverImageID = NULL
                WHERE   AlbumID = @AlbumID            
            ELSE 
            -- Update Title?
                IF @AlbumID is not null 
                    UPDATE  [{databaseSchema}].[{objectQualifier}UserAlbum]
                    SET     Title = @Title
                    WHERE   AlbumID = @AlbumID
                ELSE 
                    BEGIN
                    -- New album. insert into table.
                        INSERT  INTO [{databaseSchema}].[{objectQualifier}UserAlbum]
                                (
                                  UserID,
                                  Title,
                                  CoverImageID,
                                  Updated
                                )
                        VALUES  (
                                  @UserID,
                                  @Title,
                                  @CoverImageID,
                                  @UTCTIMESTAMP 
                                )
                        SELECT @Result = SCOPE_IDENTITY()
                    END
					SELECT @Result
    END
    GO
    
CREATE procedure [{databaseSchema}].[{objectQualifier}album_list]
    (
      @UserID INT = NULL,
      @AlbumID INT = NULL
    )
as 
    BEGIN
        IF @UserID IS NOT null 
            select  *
            FROM    [{databaseSchema}].[{objectQualifier}UserAlbum]
            WHERE   UserID = @UserID
            ORDER BY Updated DESC
        ELSE 
            SELECT  *
            FROM    [{databaseSchema}].[{objectQualifier}UserAlbum]
            WHERE   AlbumID = @AlbumID
    END
    GO
    
CREATE procedure [{databaseSchema}].[{objectQualifier}album_delete] ( @AlbumID int )
as 
    BEGIN
        DELETE  FROM [{databaseSchema}].[{objectQualifier}UserAlbumImage]
        WHERE   AlbumID = @AlbumID
        DELETE  FROM [{databaseSchema}].[{objectQualifier}UserAlbum]
        WHERE   AlbumID = @AlbumID       
    END
    GO
    
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}album_gettitle]
    (
      @AlbumID INT
    )
as 
    BEGIN
         SELECT [Title] FROM   [{databaseSchema}].[{objectQualifier}UserAlbum]
                             WHERE   AlbumID = @AlbumID                            
    END
    GO
    
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}album_getstats]
    @UserID INT = NULL,
    @AlbumID INT = NULL
as 
    BEGIN
	DECLARE @AlbumNumber INT = 0,
    @ImageNumber INT = 0
        IF @AlbumID IS NOT NULL 
                   SELECT @ImageNumber =COUNT(ImageID)
                                 FROM   [{databaseSchema}].[{objectQualifier}UserAlbumImage]
                                 WHERE  AlbumID = @AlbumID;                               
        ELSE 
            BEGIN
                   SELECT @AlbumNumber = COUNT(AlbumID)  
                                     FROM   [{databaseSchema}].[{objectQualifier}UserAlbum]
                                     WHERE  UserID = @UserID;
                                   
                   SELECT @ImageNumber = COUNT(ImageID)
                                     FROM   [{databaseSchema}].[{objectQualifier}UserAlbumImage]
                                     WHERE  AlbumID in (
                                            SELECT  AlbumID
                                            FROM    [{databaseSchema}].[{objectQualifier}UserAlbum]
                                            WHERE   UserID = @UserID);                                   
            END
			SELECT @AlbumNumber as AlbumNumber,@ImageNumber as ImageNumber
    END
    GO
    
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}album_image_save]
    (
      @ImageID INT = NULL,
      @AlbumID INT = null,
      @Caption NVARCHAR(255) = null,
      @FileName NVARCHAR(255) = null,
      @Bytes INT = null,
      @ContentType NVARCHAR(50) = null,
      @UTCTIMESTAMP datetime
    )
as 
    BEGIN
        IF @ImageID is not null 
            UPDATE  [{databaseSchema}].[{objectQualifier}UserAlbumImage]
            SET     Caption = @Caption
            WHERE   ImageID = @ImageID
        ELSE
            INSERT  INTO [{databaseSchema}].[{objectQualifier}UserAlbumImage]
                    (
                      AlbumID,
                      Caption,
                      [FileName],
                      Bytes,
                      ContentType,
                      Uploaded,
                      Downloads
                    )
            VALUES  (
                      @AlbumID,
                      @Caption,
                      @FileName,
                      @Bytes,
                      @ContentType,
                      @UTCTIMESTAMP ,
                      0
                    )
    END        
    GO
    
CREATE procedure [{databaseSchema}].[{objectQualifier}album_image_list]
    (
      @AlbumID INT = NULL,
      @ImageID INT = null
    )
as 
    BEGIN
        IF @AlbumID IS NOT null 
            SELECT  *
            FROM    [{databaseSchema}].[{objectQualifier}UserAlbumImage]
            WHERE   AlbumID = @AlbumID
            ORDER BY Uploaded DESC
        ELSE 
            SELECT  a.*,
                    b.UserID
            FROM    [{databaseSchema}].[{objectQualifier}UserAlbumImage] a
                    INNER JOIN [{databaseSchema}].[{objectQualifier}UserAlbum] b ON b.AlbumID = a.AlbumID
            WHERE   ImageID = @ImageID
    END
    GO

CREATE procedure [{databaseSchema}].[{objectQualifier}album_images_by_user](@UserID INT = null)
as 
    BEGIN
        SELECT      a.*
        FROM    [{databaseSchema}].[{objectQualifier}UserAlbumImage] a
                    INNER JOIN [{databaseSchema}].[{objectQualifier}UserAlbum] b ON b.AlbumID = a.AlbumID
        WHERE  b.UserID = @UserID
    END
    GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}album_image_delete] ( @ImageID INT )
as 
    BEGIN
        DELETE  FROM [{databaseSchema}].[{objectQualifier}UserAlbumImage]
        WHERE   ImageID = @ImageID
        UPDATE  [{databaseSchema}].[{objectQualifier}UserAlbum]
        SET     CoverImageID = NULL
        WHERE   CoverImageID = @ImageID
        UPDATE  [{databaseSchema}].[{objectQualifier}UserAlbum]
        SET     CoverImageID = NULL
        WHERE   CoverImageID = @ImageID
    END
    GO
    
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}album_image_download] ( @ImageID INT )
as 
    BEGIN
        UPDATE  [{databaseSchema}].[{objectQualifier}UserAlbumImage]
        SET     Downloads = Downloads + 1
        WHERE   ImageID = @ImageID
    END
    GO
    
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_getsignaturedata] (@BoardID INT, @UserID INT)
as 
    BEGIN

    

DECLARE   @GroupData TABLE
(
    G_UsrSigChars int,
    G_UsrSigBBCodes nvarchar(4000),
    G_UsrSigHTMLTags nvarchar(4000)
)
   
   declare @ust int, @usbbc nvarchar(4000), 
    @ushtmlt nvarchar(4000), @rust int, @rusbbc nvarchar(4000),  
    @rushtmlt nvarchar(4000) 
          
      declare c cursor for
      SELECT ISNULL(c.UsrSigChars,0), ISNULL(c.UsrSigBBCodes,''), ISNULL(c.UsrSigHTMLTags,'')
      FROM [{databaseSchema}].[{objectQualifier}User] a 
                        JOIN [{databaseSchema}].[{objectQualifier}UserGroup] b
                          ON a.UserID = b.UserID
                            JOIN [{databaseSchema}].[{objectQualifier}Group] c                         
                              ON b.GroupID = c.GroupID 
                              WHERE a.UserID = @UserID AND c.BoardID = @BoardID ORDER BY c.SortOrder ASC
        
        -- first check ranks
        SELECT TOP 1 @rust = ISNULL(c.UsrSigChars,0), @rusbbc = c.UsrSigBBCodes, 
        @rushtmlt = c.UsrSigHTMLTags
        FROM [{databaseSchema}].[{objectQualifier}Rank] c 
                                JOIN [{databaseSchema}].[{objectQualifier}User] d
                                  ON c.RankID = d.RankID
                                   WHERE d.UserID = @UserID AND c.BoardID = @BoardID 
                                   ORDER BY c.RankID DESC        
        open c
       
        fetch next from c into  @ust, @usbbc , @ushtmlt
        while @@FETCH_STATUS = 0
        begin
        if not exists (select top 1 1 from @GroupData)
        begin	

        -- insert first row and compare with ranks data
    INSERT INTO @GroupData(G_UsrSigChars,G_UsrSigBBCodes,G_UsrSigHTMLTags) 
        select (CASE WHEN @rust > ISNULL(@ust,0) THEN @rust ELSE ISNULL(@ust,0) END), 
        (COALESCE(@rusbbc + ',','') + COALESCE(@usbbc,'')) ,(COALESCE(@rushtmlt + ',','') + COALESCE(@ushtmlt, '') ) 	  
        end
        else
        begin
        update @GroupData set 
        G_UsrSigChars = (CASE WHEN G_UsrSigChars > COALESCE(@ust, 0) THEN G_UsrSigChars ELSE COALESCE(@ust, 0) END), 
        G_UsrSigBBCodes = COALESCE(@usbbc + ',','') + G_UsrSigBBCodes, 
        G_UsrSigHTMLTags = COALESCE(@ushtmlt + ',', '') + G_UsrSigHTMLTags
        end 

        fetch next from c into   @ust, @usbbc , @ushtmlt
        
        end

       close c
       deallocate c 	
                 
       
        SELECT 
        UsrSigChars = G_UsrSigChars, 
        UsrSigBBCodes = G_UsrSigBBCodes, 
        UsrSigHTMLTags = G_UsrSigHTMLTags
        FROM @GroupData 

   END
GO      
    
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}user_getalbumsdata] (@BoardID INT, @UserID INT )
as 
    BEGIN
    DECLARE 
    @OR_UsrAlbums int,     
    @OG_UsrAlbums int,
    @OR_UsrAlbumImages int,     
    @OG_UsrAlbumImages int
     -- Ugly but bullet proof - it used very rarely   
    DECLARE  @GroupData TABLE
(
    G_UsrAlbums int,
    G_UsrAlbumImages int
)
    DECLARE
   @RankData TABLE
(
    R_UsrAlbums int,
    R_UsrAlbumImages int
)

      -- REMOVED ORDER BY c.SortOrder ASC, SELECTING ALL
     
    INSERT INTO @GroupData(G_UsrAlbums,
    G_UsrAlbumImages)
    SELECT  ISNULL(c.UsrAlbums,0), ISNULL(c.UsrAlbumImages,0)   
    FROM [{databaseSchema}].[{objectQualifier}User] a
                        JOIN [{databaseSchema}].[{objectQualifier}UserGroup] b
                          ON a.UserID = b.UserID
                            JOIN [{databaseSchema}].[{objectQualifier}Group] c                         
                              ON b.GroupID = c.GroupID
                              WHERE a.UserID = @UserID AND a.BoardID = @BoardID
     
                             
     INSERT INTO @RankData(R_UsrAlbums, R_UsrAlbumImages)
     SELECT  ISNULL(c.UsrAlbums,0), ISNULL(c.UsrAlbumImages,0)   
     FROM [{databaseSchema}].[{objectQualifier}Rank] c
                                JOIN [{databaseSchema}].[{objectQualifier}User] d
                                  ON c.RankID = d.RankID WHERE d.UserID = @UserID 
                                  AND d.BoardID = @BoardID
       
       -- SELECTING MAX()
       
       SET @OR_UsrAlbums = (SELECT Max(R_UsrAlbums) FROM @RankData)
       SET @OG_UsrAlbums = (SELECT Max(G_UsrAlbums) FROM @GroupData)
       SET @OR_UsrAlbumImages = (SELECT Max(R_UsrAlbumImages) FROM @RankData)
       SET @OG_UsrAlbumImages = (SELECT Max(G_UsrAlbumImages) FROM @GroupData)
       
       SELECT
        NumAlbums  = (SELECT COUNT(ua.AlbumID) FROM [{databaseSchema}].[{objectQualifier}UserAlbum] ua
                      WHERE ua.UserID = @UserID),
        NumImages = (SELECT COUNT(uai.ImageID) FROM  [{databaseSchema}].[{objectQualifier}UserAlbumImage] uai
                     INNER JOIN [{databaseSchema}].[{objectQualifier}UserAlbum] ua
                     ON ua.AlbumID = uai.AlbumID
                     WHERE ua.UserID = @UserID),
        UsrAlbums = CASE WHEN @OG_UsrAlbums > @OR_UsrAlbums THEN @OG_UsrAlbums ELSE @OR_UsrAlbums END,
        UsrAlbumImages = CASE WHEN @OG_UsrAlbumImages > @OR_UsrAlbumImages THEN @OG_UsrAlbumImages ELSE @OR_UsrAlbumImages END
             
     
END
GO  

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}messagehistory_list] (@MessageID INT, @DaysToClean INT,
      @UTCTIMESTAMP datetime)
as 
    BEGIN    
    -- delete all message variants older then DaysToClean days Flags reserved for possible pms   
     delete from [{databaseSchema}].[{objectQualifier}MessageHistory]
     where DATEDIFF(day,Edited,@UTCTIMESTAMP ) > @DaysToClean	
              
     SELECT mh.*, m.UserID, m.UserName, IsNull(m.UserDisplayName,(SELECT u.DisplayName FROM [{databaseSchema}].[{objectQualifier}User] u where u.UserID = m.UserID)) AS UserDisplayName, t.ForumID, t.TopicID, t.Topic, m.Posted
     FROM [{databaseSchema}].[{objectQualifier}MessageHistory] mh
     LEFT JOIN [{databaseSchema}].[{objectQualifier}Message] m ON m.MessageID = mh.MessageID
     LEFT JOIN [{databaseSchema}].[{objectQualifier}Topic] t ON t.TopicID = m.TopicID
     LEFT JOIN [{databaseSchema}].[{objectQualifier}User] u ON u.UserID = t.UserID
     WHERE mh.MessageID = @MessageID
     order by mh.Edited, mh.MessageID
    END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}user_lazydata](
    @UserID	int,
    @BoardID int,
    @ShowPendingMails bit = 0,
    @ShowPendingBuddies bit = 0,
    @ShowUnreadPMs bit = 0,
    @ShowUserAlbums bit = 0,
    @ShowUserStyle bit = 0
    
) as
begin 
    declare 
    @G_UsrAlbums int,
    @R_UsrAlbums int,
    @R_Style varchar(255),
    @G_Style varchar(255) 	

	declare @UsrPersonalGroups int = 0;
    declare @UsrPersonalMasks int = 0;
    declare @UsrPersonalForums int = 0;
    
       SELECT TOP 1 @UsrPersonalGroups = IsNULL(MAX(c.UsrPersonalGroups),0)     
       FROM [{databaseSchema}].[{objectQualifier}User] a 
                        JOIN [{databaseSchema}].[{objectQualifier}UserGroup] b
                          ON a.UserID = b.UserID
                            JOIN [{databaseSchema}].[{objectQualifier}Group] c                         
                              ON b.GroupID = c.GroupID 
                              WHERE a.UserID = @UserID AND a.BoardID = @BoardID;  
							    
      SELECT TOP 1 @UsrPersonalMasks = IsNULL(MAX(c.UsrPersonalMasks),0), @UsrPersonalForums = IsNULL(MAX(c.UsrPersonalForums),0) 
      FROM [{databaseSchema}].[{objectQualifier}User] a 
                        JOIN [{databaseSchema}].[{objectQualifier}UserGroup] b
                          ON a.UserID = b.UserID
                            JOIN  [{databaseSchema}].[{objectQualifier}Group] c                         
                              ON b.GroupID = c.GroupID 
                              WHERE a.UserID = @UserID AND a.BoardID = @BoardID ;    
   
    IF (@ShowUserAlbums > 0)
    BEGIN	
    SELECT @G_UsrAlbums = ISNULL(MAX(c.UsrAlbums),0)
    FROM [{databaseSchema}].[{objectQualifier}User] a 
                        JOIN [{databaseSchema}].[{objectQualifier}UserGroup] b
                          ON a.UserID = b.UserID
                            JOIN [{databaseSchema}].[{objectQualifier}Group] c                         
                              ON b.GroupID = c.GroupID 
                               WHERE a.UserID = @UserID 
                                 AND a.BoardID = @BoardID
                                 
    SELECT  @R_UsrAlbums = ISNULL(MAX(c.UsrAlbums),0)
    FROM [{databaseSchema}].[{objectQualifier}Rank] c 
                                JOIN [{databaseSchema}].[{objectQualifier}User] d
                                  ON c.RankID = d.RankID WHERE d.UserID = @UserID 
                                    AND d.BoardID = @BoardID 
    END 	
    ELSE	
    BEGIN
    SET @G_UsrAlbums = 0
    SET @R_UsrAlbums = 0
    END
    
                                                                 

    -- return information
    select TOP 1		
        a.ProviderUserKey,
        UserFlags			= a.Flags,
        UserName			= a.Name,
        DisplayName			= a.DisplayName,
        Suspended			= a.Suspended,
        ThemeFile			= a.ThemeFile,
        LanguageFile		= a.LanguageFile,
        UseSingleSignOn     = a.UseSingleSignOn,
        TextEditor		    = a.TextEditor,
        TimeZoneUser		= a.TimeZone,
        CultureUser		    = a.Culture,		
        IsGuest				= SIGN(a.IsGuest),
        IsDirty				= SIGN(a.IsDirty),
        IsFacebookUser      = a.IsFacebookUser,
        IsTwitterUser       = a.IsTwitterUser,
        MailsPending		= CASE WHEN @ShowPendingMails > 0 THEN (select count(1) from [{databaseSchema}].[{objectQualifier}Mail] WHERE [ToUserName] = a.Name) ELSE 0 END,
        UnreadPrivate		= CASE WHEN @ShowUnreadPMs > 0 THEN (select count(1) from [{databaseSchema}].[{objectQualifier}UserPMessage] where UserID=@UserID and IsRead=0 and IsDeleted = 0 and IsArchived = 0) ELSE 0 END,
        LastUnreadPm		= CASE WHEN @ShowUnreadPMs > 0 THEN (SELECT TOP 1 Created FROM [{databaseSchema}].[{objectQualifier}PMessage] pm INNER JOIN [{databaseSchema}].[{objectQualifier}UserPMessage] upm ON pm.PMessageID = upm.PMessageID WHERE upm.UserID=@UserID and upm.IsRead=0  and upm.IsDeleted = 0 and upm.IsArchived = 0 ORDER BY pm.Created DESC) ELSE NULL END,		
        PendingBuddies      = CASE WHEN @ShowPendingBuddies > 0 THEN (SELECT COUNT(ID) FROM [{databaseSchema}].[{objectQualifier}Buddy] WHERE ToUserID = @UserID AND Approved = 0) ELSE 0 END,
        LastPendingBuddies	= CASE WHEN @ShowPendingBuddies > 0 THEN (SELECT TOP 1 Requested FROM [{databaseSchema}].[{objectQualifier}Buddy] WHERE ToUserID=@UserID and Approved = 0 ORDER BY Requested DESC) ELSE NULL END,
        UserStyle 		    = CASE WHEN @ShowUserStyle > 0 THEN (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = @UserID) ELSE '' END,			
        NumAlbums  = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}UserAlbum] ua
        WHERE ua.UserID = @UserID),
        UsrAlbums  = (CASE WHEN @G_UsrAlbums > @R_UsrAlbums THEN @G_UsrAlbums ELSE @R_UsrAlbums END),
        UserHasBuddies  = SIGN(ISNULL((SELECT TOP 1 1 FROM [{databaseSchema}].[{objectQualifier}Buddy] WHERE [FromUserID] = @UserID OR [ToUserID] = @UserID),0)),
        -- Guest can't vote in polls attached to boards, we need some temporary access check by a criteria 
        BoardVoteAccess	= (CASE WHEN a.Flags & 4 > 0 THEN 0 ELSE 1 END),
        Reputation         = a.Points,
		(SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Forum] WHERE CreatedByUserID = @UserID AND IsUserForum = 1) as PersonalForumsNumber,
        (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}AccessMask] WHERE CreatedByUserID = @UserID AND IsUserMask = 1) as PersonalAccessMasksNumber,
        (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Group] WHERE CreatedByUserID = @UserID AND IsUserGroup = 1) as PersonalGroupsNumber,
        @UsrPersonalGroups AS UsrPersonalGroups,
        @UsrPersonalMasks AS UsrPersonalMasks,
        @UsrPersonalForums AS UsrPersonalForums,
        a.CommonViewType,
        a.TopicsPerPage,
        a.PostsPerPage
        from
           [{databaseSchema}].[{objectQualifier}User] a		
        where
        a.UserID = @UserID
     end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}message_gettextbyids] (@MessageIDs varchar(max))
AS 
    BEGIN
    -- vzrus says: the server version > 2000 ntext works too slowly with substring in the 2005 
    DECLARE @ParsedMessageIDs TABLE
          (
                MessageID int
          )
      
    DECLARE @MessageID varchar(11), @Pos INT      

    SET @Pos = CHARINDEX(',', @MessageIDs, 1)

    -- check here if the value is not empty
    IF REPLACE(@MessageIDs, ',', '') <> ''
    BEGIN
        WHILE @Pos > 0
        BEGIN
            SET @MessageID = LTRIM(RTRIM(LEFT(@MessageIDs, @Pos - 1)))
            IF @MessageID <> ''
            BEGIN
                  INSERT INTO @ParsedMessageIDs (MessageID) VALUES (CAST(@MessageID AS int)) --Use Appropriate conversion
            END
            SET @MessageIDs = RIGHT(@MessageIDs, LEN(@MessageIDs) - @Pos)
            SET @Pos = CHARINDEX(',', @MessageIDs, 1)
        END
        -- to be sure that last value is inserted
        IF (LEN(@MessageIDs) > 0)
               INSERT INTO @ParsedMessageIDs (MessageID) VALUES (CAST(@MessageIDs AS int)) 
        END 

        SELECT a.MessageID, d.Message
            FROM @ParsedMessageIDs a
            INNER JOIN [{databaseSchema}].[{objectQualifier}Message] d ON (d.MessageID = a.MessageID)
    END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}user_thankfromcount]
(@UserID int) as
begin
        SELECT COUNT(TH.ThanksID) 
        FROM [{databaseSchema}].[{objectQualifier}Thanks] AS TH WHERE (TH.ThanksToUserID=@UserID)
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}user_repliedtopic]
(@MessageID int, @UserID int) as
begin
        DECLARE @TopicID int
        SET @TopicID = (SELECT TopicID FROM [{databaseSchema}].[{objectQualifier}Message] WHERE (MessageID = @MessageID))

        SELECT COUNT(t.MessageID)
        FROM [{databaseSchema}].[{objectQualifier}Message] AS t WHERE (t.TopicID=@TopicID) AND (t.UserID = @UserID)
        
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}user_thankedmessage]
(@MessageID int, @UserID int) as
begin
        SELECT COUNT(TH.ThanksID)
        FROM [{databaseSchema}].[{objectQualifier}Thanks] AS TH WHERE (TH.MessageID=@MessageID) AND (TH.ThanksFromUserID = @UserID)
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}recent_users](@BoardID int,@TimeSinceLastLogin int,@StyledNicks bit=0, @UTCTIMESTAMP datetime) as
begin  
    SELECT U.UserId,
    UserName = U.Name,
    UserDisplayName = U.DisplayName,
    IsCrawler = 0,
    UserCount = 1,
    IsHidden = (IsActiveExcluded),
    Style = CASE(@StyledNicks)
                WHEN 1 THEN U.UserStyle
                ELSE ''
            END,
    U.LastVisit
    FROM [{databaseSchema}].[{objectQualifier}User] AS U
                JOIN [{databaseSchema}].[{objectQualifier}Rank] R on R.RankID=U.RankID
    WHERE (U.IsApproved = '1') AND
     U.BoardID = @BoardID AND
     (DATEADD(mi, 0 - @TimeSinceLastLogin, GETDATE()) < U.LastVisit) AND
                --Excluding guests
                NOT EXISTS(             
                    SELECT 1 
                        FROM [{databaseSchema}].[{objectQualifier}UserGroup] x
                            inner join [{databaseSchema}].[{objectQualifier}Group] y ON y.GroupID=x.GroupID 
                        WHERE x.UserID=U.UserID and (y.Flags & 2)<>0
                    )
    ORDER BY U.LastVisit
end
GO

create procedure [{databaseSchema}].[{objectQualifier}readtopic_addorupdate](@UserID int,@TopicID int,
      @UTCTIMESTAMP datetime) as
begin

    declare	@LastAccessDate	datetime
    set @LastAccessDate = (select top 1 LastAccessDate from [{databaseSchema}].[{objectQualifier}TopicReadTracking] where UserID=@UserID AND TopicID=@TopicID)
    IF @LastAccessDate is not null
    begin	     
          update [{databaseSchema}].[{objectQualifier}TopicReadTracking] set LastAccessDate=@UTCTIMESTAMP where LastAccessDate = @LastAccessDate AND UserID=@UserID AND TopicID=@TopicID
    end
    ELSE
      begin
          insert into [{databaseSchema}].[{objectQualifier}TopicReadTracking](UserID,TopicID,LastAccessDate)
          values (@UserID, @TopicID, @UTCTIMESTAMP)
      end
end
GO

create procedure [{databaseSchema}].[{objectQualifier}readtopic_delete](@UserID int) as
begin
        delete from [{databaseSchema}].[{objectQualifier}TopicReadTracking] where UserID = @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}readtopic_lastread](@UserID int,@TopicID int) as
begin
        SELECT LastAccessDate FROM  [{databaseSchema}].[{objectQualifier}TopicReadTracking] WHERE UserID = @UserID AND TopicID = @TopicID
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}readforum_addorupdate] (
    @UserID INT
    ,@ForumID INT,
      @UTCTIMESTAMP datetime
    )
AS
BEGIN
    DECLARE @LastAccessDate DATETIME

    IF EXISTS (
            SELECT 1
            FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking]
            WHERE UserID = @UserID
                AND ForumID = @ForumID
            )
    BEGIN
        SET @LastAccessDate = (
                SELECT LastAccessDate
                FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking]
                WHERE (
                        UserID = @UserID
                        AND ForumID = @ForumID
                        )
                )

        UPDATE [{databaseSchema}].[{objectQualifier}ForumReadTracking]
        SET LastAccessDate = @UTCTIMESTAMP
        WHERE LastAccessDate = @LastAccessDate
            AND UserID = @UserID
            AND ForumID = @ForumID
    END
    ELSE
    BEGIN
        INSERT INTO [{databaseSchema}].[{objectQualifier}ForumReadTracking] (
            UserID
            ,ForumID
            ,LastAccessDate
            )
        VALUES (
            @UserID
            ,@ForumID
            ,@UTCTIMESTAMP
            )
    END

    -- Delete TopicReadTracking for forum...
    DELETE
    FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking]
    WHERE UserID = @UserID
        AND TopicID IN (
            SELECT TopicID
            FROM yaf_Topic
            WHERE ForumID = @ForumID
            )
END
GO

create procedure [{databaseSchema}].[{objectQualifier}readforum_delete](@UserID int) as
begin
        delete from [{databaseSchema}].[{objectQualifier}ForumReadTracking] where UserID = @UserID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}readforum_lastread](@UserID int,@ForumID int) as
begin
        SELECT LastAccessDate FROM  [{databaseSchema}].[{objectQualifier}ForumReadTracking] WHERE UserID = @UserID AND ForumID = @ForumID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_lastread](@UserID int) as
begin
        DECLARE @LastForumRead datetime
        DECLARE @LastTopicRead datetime
        
         SET @LastForumRead = (SELECT TOP 1 LastAccessDate FROM  [{databaseSchema}].[{objectQualifier}ForumReadTracking] WHERE UserID = @UserID ORDER BY LastAccessDate DESC)
        SET @LastTopicRead = (SELECT TOP 1 LastAccessDate FROM  [{databaseSchema}].[{objectQualifier}TopicReadTracking] WHERE UserID = @UserID ORDER BY LastAccessDate DESC)

        IF @LastForumRead is not null AND @LastTopicRead is not null
        
        IF @LastForumRead > @LastTopicRead
           SELECT LastAccessDate = @LastForumRead
        ELSE
           SELECT LastAccessDate = @LastTopicRead
           
        ELSE IF @LastForumRead is not null
           SELECT LastAccessDate = @LastForumRead
            
        ELSE IF @LastTopicRead is not null
            SELECT LastAccessDate = @LastTopicRead
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}topics_byuser]
(   @BoardID int,
    @CategoryID int=null,
    @PageUserID int,		
    @SinceDate datetime=null,
    @ToDate datetime,
    @PageIndex int = 1, 
    @PageSize int = 0, 
    @StyledNicks bit = 0,	
    @FindLastUnread bit = 0,
	@UTCTIMESTAMP datetime
)
AS
 begin
  declare @TotalRows int
   declare @FirstSelectRowNumber int
   declare @LastSelectRowNumber int
   -- find total returned count
   select @TotalRows = count(1)	
        from
        [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
    where
        (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
        and	c.TopicMovedID is null
        and c.TopicID = (SELECT TOP 1 mess.TopicID FROM [{databaseSchema}].[{objectQualifier}Message] mess WHERE mess.UserID=@PageUserID AND mess.TopicID=c.TopicID)	
    
    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
    
    with TopicIds as
     (
     select ROW_NUMBER() over (order by cat.SortOrder asc, d.SortOrder asc, c.LastPosted desc) as RowNum, c.TopicID
     from [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
  where
        (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0
        and	c.TopicMovedID is null
        and c.TopicID = (SELECT TOP 1 mess.TopicID FROM [{databaseSchema}].[{objectQualifier}Message] mess WHERE mess.UserID=@PageUserID AND mess.TopicID=c.TopicID)	
      )	
    select
        c.ForumID,
        c.TopicID,
        c.TopicMovedID,
        c.Posted,
        LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),
        [Subject] = c.Topic,
        [Description] = c.Description,
        [Status] = c.Status,
        [Styles] = c.Styles,
        c.UserID,
        Starter = IsNull(c.UserName,b.Name),
        StarterDisplay = IsNull(c.UserDisplayName,b.DisplayName),
        NumPostsDeleted = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] mes WHERE mes.TopicID = c.TopicID AND mes.IsDeleted = 1 AND mes.IsApproved = 1 AND ((@PageUserID IS NOT NULL AND mes.UserID = @PageUserID) OR (@PageUserID IS NULL)) ),
        Replies = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=c.TopicID and x.IsDeleted=0) - 1,
        [Views] = c.[Views],
        LastPosted = c.LastPosted,
        LastUserID = c.LastUserID,
        LastUserName = IsNull(c.LastUserName,(select x.Name from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastUserDisplayName = IsNull(c.LastUserDisplayName,(select x.DisplayName from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastMessageID = c.LastMessageID,
        LastMessageFlags = c.LastMessageFlags,
        LastTopicID = c.TopicID,
        TopicFlags = c.Flags,
        FavoriteCount = (SELECT COUNT(ID) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = IsNull(c.TopicMovedID,c.TopicID)),
        c.Priority,
        c.PollID,
        ForumName = d.Name,
        c.TopicMovedID,
        ForumFlags = d.Flags,
        FirstMessage = (SELECT TOP 1 CAST([Message] as nvarchar(1000)) FROM [{databaseSchema}].[{objectQualifier}Message] mes2 where mes2.TopicID = IsNull(c.TopicMovedID,c.TopicID) AND mes2.Position = 0),
        StarterStyle = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end,
        LastUserStyle= case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = c.LastUserID)
            else ''	 end,
        LastForumAccess = case(@FindLastUnread)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x WHERE x.ForumID=d.ForumID AND x.UserID = @PageUserID)
             else ''	 end,
        LastTopicAccess = case(@FindLastUnread)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y WHERE y.TopicID=c.TopicID AND y.UserID = @PageUserID)
             else ''	 end,
        [{databaseSchema}].[{objectQualifier}topic_gettags_str](c.TopicID) as TopicTags,
        c.TopicImage,
        c.TopicImageType,
        c.TopicImageBin,
        0 as HasAttachments, 
        TotalRows = @ToTalRows,
        PageIndex = @PageIndex
    from
        TopicIds ti
        inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = ti.TopicID
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
    where ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC
end
GO

create procedure [{databaseSchema}].[{objectQualifier}TopicStatus_Delete] (@TopicStatusID int) as
begin
   delete from [{databaseSchema}].[{objectQualifier}TopicStatus] 
    where TopicStatusID = @TopicStatusID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}TopicStatus_Edit] (@TopicStatusID int) as
BEGIN
    SELECT * 
    FROM [{databaseSchema}].[{objectQualifier}TopicStatus] 
    WHERE 
        TopicStatusID = @TopicStatusID
END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}TopicStatus_List] (@BoardID int) as
    BEGIN
            SELECT
                *
            FROM
                [{databaseSchema}].[{objectQualifier}TopicStatus]
            WHERE
                BoardID = @BoardID	
            ORDER BY
                TopicStatusID
        END
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}TopicStatus_Save] (@TopicStatusID int=null, @BoardID int, @TopicStatusName nvarchar(100),@DefaultDescription nvarchar(100)) as
begin
        if @TopicStatusID is null or @TopicStatusID = 0 begin
        insert into [{databaseSchema}].[{objectQualifier}TopicStatus] (BoardID,TopicStatusName,DefaultDescription) 
        values(@BoardID,@TopicStatusName,@DefaultDescription)
    end
    else begin
        update [{databaseSchema}].[{objectQualifier}TopicStatus] 
        set TopicStatusName = @TopicStatusName, 
            DefaultDescription = @DefaultDescription
        where TopicStatusID = @TopicStatusID
    end
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}forum_move](@ForumOldID int,@ForumNewID int) as
begin
        -- Maybe an idea to use cascading foreign keys instead? Too bad they don't work on MS SQL 7.0...
    update [{databaseSchema}].[{objectQualifier}Forum] set LastMessageID=null,LastTopicID=null where ForumID=@ForumOldID
    update [{databaseSchema}].[{objectQualifier}Active] set ForumID=@ForumNewID where ForumID=@ForumOldID
    update [{databaseSchema}].[{objectQualifier}NntpForum] set ForumID=@ForumNewID where ForumID=@ForumOldID
    update [{databaseSchema}].[{objectQualifier}WatchForum] set ForumID=@ForumNewID where ForumID=@ForumOldID
    delete from [{databaseSchema}].[{objectQualifier}ForumReadTracking] where ForumID = @ForumOldID

    -- BAI CHANGED 02.02.2004
    -- Move topics, messages and attachments

    declare @tmpTopicID int;
    declare topic_cursor cursor for
        select TopicID from [{databaseSchema}].[{objectQualifier}topic]
        where ForumID = @ForumOldID
        order by TopicID desc
    
    open topic_cursor
    
    fetch next from topic_cursor
    into @tmpTopicID
    
    -- Check @@FETCH_STATUS to see if there are any more rows to fetch.
    while @@FETCH_STATUS = 0
    begin
        exec [{databaseSchema}].[{objectQualifier}topic_move] @tmpTopicID,@ForumNewID,0, -1;
    
       -- This is executed as long as the previous fetch succeeds.
        fetch next from topic_cursor
        into @tmpTopicID
    end
    
    close topic_cursor
    deallocate topic_cursor

    -- TopicMove finished
    -- END BAI CHANGED 02.02.2004

    delete from [{databaseSchema}].[{objectQualifier}ForumAccess] where ForumID = @ForumOldID

    --Update UserForums Too 
    update [{databaseSchema}].[{objectQualifier}UserForum] set ForumID=@ForumNewID where ForumID=@ForumOldID
    --END ABOT CHANGED 09.04.2004
    delete from [{databaseSchema}].[{objectQualifier}Forum] where ForumID = @ForumOldID

	execute [{databaseSchema}].[{objectQualifier}forum_ns_recreate] null, null	
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_update_ssn_status](@UserID int,@IsFacebookUser bit,@IsTwitterUser bit) as
begin
    
    update [{databaseSchema}].[{objectQualifier}User] set IsFacebookUser = @IsFacebookUser , IsTwitterUser = @IsTwitterUser where UserID = @UserID
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}topic_unanswered]
(   @BoardID int,
    @CategoryID int=null,
    @PageUserID int,		
    @SinceDate datetime=null,
    @ToDate datetime,
    @PageIndex int = 1, 
    @PageSize int = 0, 
    @StyledNicks bit = 0,	
    @FindLastRead bit = 0,
	@UTCTIMESTAMP datetime
)
AS
begin
  declare @TotalRows int
   declare @FirstSelectRowNumber int
   declare @LastSelectRowNumber int

   -- find total returned count
   select @TotalRows = count(1)	
        from
        [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
    where
        c.LastPosted IS NOT NULL and (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0 and	
        c.TopicMovedID is null and
        c.NumPosts = 1
    
    select @PageIndex = @PageIndex+1;
    select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
    select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;
    
    with TopicIds as
     (
     select ROW_NUMBER() over (order by cat.SortOrder asc, d.SortOrder asc, c.LastPosted desc) as RowNum, c.TopicID
     from [{databaseSchema}].[{objectQualifier}Topic] c
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
        join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on x.ForumID=d.ForumID
        join [{databaseSchema}].[{objectQualifier}Category] cat on cat.CategoryID=d.CategoryID
    where
        c.LastPosted IS NOT NULL and (c.LastPosted between @SinceDate and @ToDate) and
        x.UserID = @PageUserID and
        CONVERT(int,x.ReadAccess) <> 0 and
        cat.BoardID = @BoardID and
        (@CategoryID is null or cat.CategoryID=@CategoryID) and
        c.IsDeleted = 0 and	
        c.TopicMovedID is null and
        c.NumPosts = 1
      )	
      select
                c.ForumID,
        c.TopicID,
        c.TopicMovedID,
        c.Posted,
        LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),
        [Subject] = c.Topic,
        [Description] = c.Description,
        [Status] = c.Status,
        [Styles] = c.Styles,
        c.UserID,
        Starter = IsNull(c.UserName,b.Name),
        StarterDisplay = IsNull(c.UserDisplayName,b.DisplayName),
        NumPostsDeleted = (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] mes WHERE mes.TopicID = c.TopicID AND mes.IsDeleted = 1 AND mes.IsApproved = 1 AND ((@PageUserID IS NOT NULL AND mes.UserID = @PageUserID) OR (@PageUserID IS NULL)) ),
        Replies = (select count(1) from [{databaseSchema}].[{objectQualifier}Message] x where x.TopicID=c.TopicID and x.IsDeleted=0) - 1,
        [Views] = c.[Views],
        LastPosted = c.LastPosted,
        LastUserID = c.LastUserID,
        LastUserName = IsNull(c.LastUserName,(select x.Name from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastUserDisplayName = IsNull(c.LastUserDisplayName,(select x.DisplayName from [{databaseSchema}].[{objectQualifier}User] x where x.UserID=c.LastUserID)),
        LastMessageID = c.LastMessageID,
        LastMessageFlags = c.LastMessageFlags,
        LastTopicID = c.TopicID,
        TopicFlags = c.Flags,
        FavoriteCount = (SELECT COUNT(ID) as [FavoriteCount] FROM [{databaseSchema}].[{objectQualifier}FavoriteTopic] WHERE TopicId = IsNull(c.TopicMovedID,c.TopicID)),
        c.Priority,
        c.PollID,
        ForumName = d.Name,
        c.TopicMovedID,
        ForumFlags = d.Flags,
        FirstMessage = (SELECT TOP 1 CAST([Message] as nvarchar(1000)) FROM [{databaseSchema}].[{objectQualifier}Message] mes2 where mes2.TopicID = IsNull(c.TopicMovedID,c.TopicID) AND mes2.Position = 0),
        StarterStyle = case(@StyledNicks)
            when 1 then  b.UserStyle
            else ''	 end,
        LastUserStyle= case(@StyledNicks)
            when 1 then  (select top 1 usr.[UserStyle] from [{databaseSchema}].[{objectQualifier}User] usr with(nolock) where usr.UserID = c.LastUserID)
            else ''	 end,
        LastForumAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}ForumReadTracking] x WHERE x.ForumID=d.ForumID AND x.UserID = @PageUserID)
             else ''	 end,
        LastTopicAccess = case(@FindLastRead)
             when 1 then
               (SELECT top 1 LastAccessDate FROM [{databaseSchema}].[{objectQualifier}TopicReadTracking] y WHERE y.TopicID=c.TopicID AND y.UserID = @PageUserID)
             else ''	 end,
        [{databaseSchema}].[{objectQualifier}topic_gettags_str](c.TopicID) as TopicTags,
        c.TopicImage,
        c.TopicImageType,
        c.TopicImageBin,
        0 as HasAttachments, 
        TotalRows = @TotalRows,
        PageIndex = @PageIndex
    from
        TopicIds ti
        inner join [{databaseSchema}].[{objectQualifier}Topic] c on c.TopicID = ti.TopicID
        join [{databaseSchema}].[{objectQualifier}User] b on b.UserID=c.UserID
        join [{databaseSchema}].[{objectQualifier}Forum] d on d.ForumID=c.ForumID
    where ti.RowNum between @FirstSelectRowNumber and @LastSelectRowNumber
        order by
            RowNum ASC

end
GO


create procedure [{databaseSchema}].[{objectQualifier}db_handle_computedcolumns]( @SetOnDisk bit )  
as
begin
    declare @tmpC nvarchar(255)
    declare @tmpT nvarchar(255)
    declare @tmpD nvarchar(255)	

    CREATE TABLE #MyTempTable (tname nvarchar(255),cname nvarchar(255), ctext nvarchar(255))
    INSERT INTO #MyTempTable(tname,cname, ctext)     
        SELECT        o.name,s.name,sc.text
FROM            sys.syscolumns AS s INNER JOIN
                         sys.sys.objects AS o ON o.id = s.id INNER JOIN
                         sys.syscomments AS sc ON sc.id = o.id
WHERE        (s.iscomputed = 1) AND (o.type = 'U') AND (s.xtype = 104)

    if @SetOnDisk = 1
    begin
        declare c cursor for
        SELECT    tname, cname, ctext
        FROM           #MyTempTable       
        
        open c
        
        fetch next from c into @tmpT, @tmpC, @tmpD
        while @@FETCH_STATUS = 0
        begin
            
        exec('ALTER TABLE [{databaseSchema}].[{objectQualifier}'+ @tmpT +'] drop column ' + @tmpC)
        exec('ALTER TABLE [{databaseSchema}].[{objectQualifier}'+ @tmpT +'] add ' + @tmpC + ' AS ' + @tmpD + ' PERSISTED ' )

            fetch next from c into  @tmpT, @tmpC, @tmpD
        end
        close c
        deallocate c
    end
    else
    begin
        declare c cursor for
            SELECT    tname, cname, ctext
        FROM           #MyTempTable 
        
        open c
        
        fetch next from c into @tmpT, @tmpC, @tmpD
        while @@FETCH_STATUS = 0
        begin		    	
            exec('ALTER TABLE [{databaseSchema}].[{objectQualifier}'+ @tmpT +'] drop column ' + @tmpC)
            exec('ALTER TABLE [{databaseSchema}].[{objectQualifier}'+ @tmpT +'] add ' + @tmpC + ' AS ' + @tmpD)
            fetch next from c into @tmpT, @tmpC, @tmpD
        end
        close c
        deallocate c
    end	
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}adminpageaccess_save] (@UserID int, @PageName nvarchar(128)) as
begin
    if not exists (select 1 from [{databaseSchema}].[{objectQualifier}AdminPageUserAccess] where UserID = @UserID and PageName = @PageName) 
        begin
        insert into [{databaseSchema}].[{objectQualifier}AdminPageUserAccess]  (UserID,PageName) 
        values(@UserID,@PageName)
    end	
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}adminpageaccess_delete] (@UserID int, @PageName nvarchar(128)) as
begin
        delete from [{databaseSchema}].[{objectQualifier}AdminPageUserAccess]  where UserID = @UserID AND (@PageName IS NULL OR PageName = @PageName);
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}adminpageaccess_list] (@UserID int, @PageName nvarchar(128) = null) as
begin
        if (@UserID > 0  and @PageName IS NOT NULL) 
        select ap.*, 
        u.Name as UserName, 
        u.DisplayName as UserDisplayName, 
        b.Name as BoardName
        from [{databaseSchema}].[{objectQualifier}AdminPageUserAccess] ap 
        JOIN  [{databaseSchema}].[{objectQualifier}User] u on ap.UserID = u.UserID 
        JOIN [{databaseSchema}].[{objectQualifier}Board] b ON b.BoardID = u.BoardID 
        where u.UserID = @UserID and PageName = @PageName and (u.Flags & 1) <> 1 order by  b.BoardID,u.Name,ap.PageName;
        else if (@UserID > 0 and @PageName IS  NULL) 
        select ap.*, 
        u.Name as UserName, 
        u.DisplayName as UserDisplayName, 
        b.Name as BoardName,
        1 as ReadAccess  
         from [{databaseSchema}].[{objectQualifier}AdminPageUserAccess] ap 
        JOIN  [{databaseSchema}].[{objectQualifier}User] u on ap.UserID = u.UserID 
        JOIN [{databaseSchema}].[{objectQualifier}Board] b ON b.BoardID = u.BoardID 
        where u.UserID = @UserID and (u.Flags & 1) <> 1 order by  b.BoardID,u.Name,ap.PageName;
        else
        select ap.*, 
        u.Name as UserName, 
        u.DisplayName as UserDisplayName, 
        b.Name as BoardName
        from [{databaseSchema}].[{objectQualifier}AdminPageUserAccess] ap 
        JOIN  [{databaseSchema}].[{objectQualifier}User] u on ap.UserID = u.UserID 
        JOIN [{databaseSchema}].[{objectQualifier}Board] b ON b.BoardID = u.BoardID 
        where (u.Flags & 1) <> 1
        order by  b.BoardID,u.Name,ap.PageName;
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}eventloggroupaccess_save] (@GroupID int, @EventTypeID int, @EventTypeName nvarchar(128), @DeleteAccess bit = 0) as
begin
    if not exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] where GroupID = @GroupID and EventTypeName = @EventTypeName) 
        begin
        insert into [{databaseSchema}].[{objectQualifier}EventLogGroupAccess]  (GroupID,EventTypeID,EventTypeName,DeleteAccess) 
        values(@GroupID,@EventTypeID,@EventTypeName,@DeleteAccess)
    end	
    else
    begin
        update [{databaseSchema}].[{objectQualifier}EventLogGroupAccess]  set DeleteAccess = @DeleteAccess
        where GroupID = @GroupID and EventTypeID = @EventTypeID
    end
end
GO


CREATE procedure [{databaseSchema}].[{objectQualifier}eventloggroupaccess_delete] (@GroupID int, @EventTypeID int, @EventTypeName nvarchar(128)) as
begin
    if @EventTypeName is not null 
    begin
        delete from [{databaseSchema}].[{objectQualifier}EventLogGroupAccess]  where GroupID = @GroupID and EventTypeID = @EventTypeID
    end	
    else
    begin
    -- delete all access rights
        delete from [{databaseSchema}].[{objectQualifier}EventLogGroupAccess]  where GroupID = @GroupID 
    end
end
GO

CREATE procedure [{databaseSchema}].[{objectQualifier}eventloggroupaccess_list] (@GroupID int, @EventTypeID int = null) as
begin 
-- TODO - exclude host admins from list   
if @EventTypeID is null   
        select e.*, g.Name as GroupName from [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] e 
        join [{databaseSchema}].[{objectQualifier}Group] g on g.GroupID = e.GroupID where  e.GroupID = @GroupID
        else
        select e.*, g.Name as GroupName from [{databaseSchema}].[{objectQualifier}EventLogGroupAccess] e 
        join [{databaseSchema}].[{objectQualifier}Group] g on g.GroupID = e.GroupID where  e.GroupID = @GroupID and e.EventTypeID = @EventTypeID
end
GO

create procedure [{databaseSchema}].[{objectQualifier}user_savestyle](@GroupID int, @RankID int)  as

begin
-- loop thru users to sync styles
 if @GroupID is not null or @RankID is not null or not exists (select 1 from [{databaseSchema}].[{objectQualifier}User] where UserStyle IS NOT NULL)
 begin
    declare @usridtmp int 
    declare @styletmp varchar(255)
    declare @rankidtmp int    
      
        declare c cursor for
        select UserID,UserStyle,RankID from [{databaseSchema}].[{objectQualifier}User]    
        FOR UPDATE -- OF UserStyle
        open c
        
        fetch next from c into @usridtmp,@styletmp,@rankidtmp
        while @@FETCH_STATUS = 0
        begin      
        UPDATE [{databaseSchema}].[{objectQualifier}User] SET UserStyle= ISNULL(( SELECT TOP 1 f.Style FROM [{databaseSchema}].[{objectQualifier}UserGroup] e 
            join [{databaseSchema}].[{objectQualifier}Group] f on f.GroupID=e.GroupID WHERE e.UserID=@usridtmp AND LEN(f.Style) > 2 ORDER BY f.SortOrder), (SELECT TOP 1 r.Style FROM [{databaseSchema}].[{objectQualifier}Rank] r where RankID = @rankidtmp)) 
        WHERE UserID = @usridtmp  -- CURRENT OF c 	
             
        fetch next from c into @usridtmp,@styletmp,@rankidtmp		
        
        end
        close c
        deallocate c  
        end   
   
end
GO
exec('[{databaseSchema}].[{objectQualifier}user_savestyle] null,null')
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_cataccess_actuser](@BoardID int,@UserID int) as
begin      

    select 
      DISTINCT(f.CategoryID),
      c.Name as CategoryName, c.SortOrder   
    from
        [{databaseSchema}].[{objectQualifier}Forum] f
        join [{databaseSchema}].[{objectQualifier}Category] c on c.CategoryID = f.CategoryID
        JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] access ON (f.ForumID = access.ForumID and access.UserID = @UserID)  
    WHERE c.BoardID = @BoardID and f.ParentID IS NULL and (access.ReadAccess > 0 or (access.ReadAccess <= 0 and f.Flags & 2 <> 2)) ORDER BY c.SortOrder, f.CategoryID, c.Name
end
GO

create procedure [{databaseSchema}].[{objectQualifier}forum_tags](@BoardID int,@PageUserID int,@ForumID int,@PageIndex int, @PageSize int, @SearchText nvarchar(255), @BeginsWith bit) as
begin
declare @AllTagsCount int
declare @TotalRows int
declare @FirstSelectRowNumber int
declare @LastSelectRowNumber int

           set @PageIndex = @PageIndex + 1;
           set @FirstSelectRowNumber = 0;
           set @LastSelectRowNumber = 0;
           set @TotalRows = 0;
           
    select @TotalRows = COUNT(DISTINCT(tg.TagID))
    FROM [{databaseSchema}].[{objectQualifier}Tags] tg 
    JOIN [{databaseSchema}].[{objectQualifier}TopicTags] tt ON tt.TagID = tg.TagID 
    JOIN [{databaseSchema}].[{objectQualifier}Topic] t ON t.TopicID = tt.TagID
    JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa ON (aa.ForumID = t.ForumID and aa.UserID = @PageUserID)
    WHERE BoardID=@BoardID and (@ForumID is null or t.ForumID=@ForumID)  AND (t.Flags & 8) <> 8
    AND  LOWER(tg.Tag) LIKE CASE 
            WHEN (@BeginsWith = 0 and @SearchText IS NOT NULL AND LEN(@SearchText) > 0) THEN '%' + LOWER(@SearchText) + '%' 
            WHEN (@BeginsWith = 1 and @SearchText IS NOT NULL AND LEN(@SearchText) > 0) THEN LOWER(@SearchText) + '%'        
            ELSE '%' END 
    AND aa.UserID = @PageUserID
           select @FirstSelectRowNumber = (@PageIndex - 1) * @PageSize + 1;
           select @LastSelectRowNumber = (@PageIndex - 1) * @PageSize + @PageSize;

  SET @AllTagsCount = (SELECT MAX(tg.TagCount)  FROM [{databaseSchema}].[{objectQualifier}Tags] tg 
    JOIN [{databaseSchema}].[{objectQualifier}TopicTags] tt ON tt.TagID = tg.TagID 
    JOIN [{databaseSchema}].[{objectQualifier}Topic] t ON t.TopicID = tt.TagID
    JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa ON (aa.ForumID = t.ForumID and aa.UserID = @PageUserID)	 
    WHERE BoardID=@BoardID and (@ForumID is null or t.ForumID=@ForumID)  AND (t.Flags & 8) <> 8
	      AND  LOWER(tg.Tag) LIKE CASE 
            WHEN (@BeginsWith = 0 and @SearchText IS NOT NULL AND LEN(@SearchText) > 0) THEN '%' + LOWER(@SearchText) + '%' 
            WHEN (@BeginsWith = 1 and @SearchText IS NOT NULL AND LEN(@SearchText) > 0) THEN LOWER(@SearchText) + '%'             
            ELSE '%' END 
    AND aa.UserID = @PageUserID);
           
           with TagsIds as
           (
             select ROW_NUMBER() over (order by tg.Tag) as RowNum, tg.TagID
			 from
             [{databaseSchema}].[{objectQualifier}Tags] tg 
            JOIN [{databaseSchema}].[{objectQualifier}TopicTags] tt ON tt.TagID = tg.TagID 
            JOIN [{databaseSchema}].[{objectQualifier}Topic] t ON t.TopicID = tt.TagID
            JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] aa 
			ON (aa.ForumID = t.ForumID  and aa.UserID = @PageUserID)     
			 where BoardID=@BoardID and (@ForumID is null or t.ForumID=@ForumID) AND (t.Flags & 8) <> 8		   
          AND  LOWER(tg.Tag) LIKE CASE 
            WHEN (@BeginsWith = 0 and @SearchText IS NOT NULL AND LEN(@SearchText) > 0) THEN '%' + LOWER(@SearchText) + '%' 
            WHEN (@BeginsWith = 1 and @SearchText IS NOT NULL AND LEN(@SearchText) > 0) THEN LOWER(@SearchText) + '%'             
            ELSE '%' END 
           )
           select
            tg.TagID,
			tg.Tag,
			tg.TagCount,
			@AllTagsCount AS MaxTagCount,
            @TotalRows as TotalCount
            from
            TagsIds c
            inner join [{databaseSchema}].[{objectQualifier}Tags] tg on tg.TagID = c.TagID           
            where c.RowNum between (@FirstSelectRowNumber) and (@LastSelectRowNumber)
            order by c.RowNum asc 
end
GO


create procedure [{databaseSchema}].[{objectQualifier}User_ListTodaysBirthdays](
@BoardID int,@StyledNicks bit, @CurrentYear datetime, @CurrentUtc datetime, @CurrentUtc1 datetime, @CurrentUtc2 datetime ) as
begin  
   SELECT 
   up.Birthday, 
   up.UserID, 
   u.Name as UserName,
   u.DisplayName AS UserDisplayName, 
   u.TimeZone,
   (case(@StyledNicks) when 1 then  u.UserStyle  else '' end) AS Style  FROM 
                [{databaseSchema}].[{objectQualifier}UserProfile] up 
				JOIN [{databaseSchema}].[{objectQualifier}User]u 
				ON u.UserID = up.UserID 
				JOIN [{databaseSchema}].[{objectQualifier}Rank] r ON
				 r.RankID = u.RankID where u.BoardID = @BoardID 
				 AND DATEADD(year, DATEDIFF(year,up.Birthday,@CurrentUtc1),up.Birthday) > @CurrentUtc1 
				 and  DATEADD(year, DATEDIFF(year,up.Birthday,@CurrentUtc2),up.Birthday) < @CurrentUtc2;
   
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_save_prntchck]
(
	@ForumID INT,
	@ParentID INT
 ) 
 as
 begin
        SELECT [{databaseSchema}].[{objectQualifier}forum_save_parentschecker](@ForumID,	@ParentID);
end;
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}rsstopic_list]
(
	@ForumID INT,
	@Start INT,
	@Limit INT
 ) 
 as
 begin
   select top (@Limit) Topic = a.Topic,
   TopicID = a.TopicID, 
   Name = b.Name, 
   LastPosted = IsNull(a.LastPosted,a.Posted), 
   LastUserID = IsNull(a.LastUserID, a.UserID), LastMessageID = IsNull(a.LastMessageID,
   (select top 1 m.MessageID from [{databaseSchema}].[{objectQualifier}Message] m where m.TopicID = a.TopicID order by m.Posted desc)), 
   LastMessageFlags = IsNull(a.LastMessageFlags,22) from [{databaseSchema}].[{objectQualifier}Topic] a, [{databaseSchema}].[{objectQualifier}Forum] b where a.ForumID = @ForumID and b.ForumID = a.ForumID and a.TopicMovedID is null and a.IsDeleted = 0
            order by a.Posted desc;          
end;
GO


CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}topic_imagesave](
    @TopicID 	        INT,
    @ImageURL	        NVARCHAR(255),
    @Stream		    VARBINARY(MAX),
    @TopicImageType	VARCHAR(128)
 ) 
 AS 
 BEGIN
      UPDATE [{databaseSchema}].[{objectQualifier}Topic] 
      SET    TopicImage = @ImageURL,
             TopicImageBin = @Stream,
             TopicImageType = @TopicImageType
      WHERE  TopicID = @TopicID;       
END;
GO

create procedure [{databaseSchema}].[{objectQualifier}topic_restore](@TopicID int, @UserID int) as
begin
declare @ForumID int;
        UPDATE [{databaseSchema}].[{objectQualifier}Topic] SET Flags = Flags ^ 8 WHERE  TopicID = @TopicID and (Flags & 8) = 8;
		UPDATE [{databaseSchema}].[{objectQualifier}Topic] SET Flags = Flags ^ 8 WHERE  TopicMovedID = @TopicID and (Flags & 8) = 8;
		UPDATE [{databaseSchema}].[{objectQualifier}Message] SET Flags = Flags ^ 8 WHERE  TopicID = @TopicID and (Flags & 8) = 8;
        UPDATE  [{databaseSchema}].[{objectQualifier}Topic] 
		SET LastMessageID = (SELECT TOP 1 m.MessageID from  [{databaseSchema}].[{objectQualifier}Message] m
            where m.TopicID = @TopicID and (m.Flags & 8) != 8 ORDER BY m.Posted desc)
        where TopicID = @TopicID; 		
		
		SELECT @ForumID = ForumID FROM [{databaseSchema}].[{objectQualifier}Topic] where TopicID = @TopicID;
        EXEC  [{databaseSchema}].[{objectQualifier}forum_updatelastpost] @ForumID    
        EXEC  [{databaseSchema}].[{objectQualifier}forum_updatestats] @ForumID
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}digest_topicnew](
                 @boardid integer,
                 @pageuserid integer,
                 @sincedate datetime,
                 @todate datetime,              
                 @stylednicks bit,               
				 @utctimestamp datetime)
				 as
 BEGIN
 SELECT
        d.Name AS "ForumName",
		c.Topic AS "Subject",
		c.UserDisplayName AS "StartedUserName",		
	    c.LastUserDisplayName as "LastUserName" ,
	    c.LastMessageID as "LastMessageID",
		(SELECT x.Message FROM [{databaseSchema}].[{objectQualifier}Message] x 
          WHERE x.TopicID=c.TopicID and x.MessageID = c.LastMessageID) as "LastMessage",
	    (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] x 
          WHERE x.TopicID=c.TopicID and (x.Flags & 8) = 0) as "Replies"    
    FROM
        [{databaseSchema}].[{objectQualifier}Topic] c  
        JOIN [{databaseSchema}].[{objectQualifier}Forum] d ON d.ForumID=c.ForumID 
		JOIN [{databaseSchema}].[{objectQualifier}Category] cat ON cat.CategoryID = d.CategoryID     
        join [{databaseSchema}].[{objectQualifier}vaccess] x on (x.ForumID=d.ForumID AND x.UserID = @pageuserid AND x.ReadAccess <> 0)      
    WHERE
	   cat.BoardID = @boardid and  
       c.Posted > @sincedate and        
      (c.Flags & 8) = 0
    ORDER BY
	d.SortOrder,
    c.LastPosted desc;	
end
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}digest_topicactive](
                 @boardid integer,
                 @pageuserid integer,
                 @sincedate datetime,
                 @todate datetime,              
                 @stylednicks bit,               
				 @utctimestamp datetime)
				 as
 BEGIN
 SELECT
        d.Name AS "ForumName",
		c.Topic AS "Subject",
		c.UserDisplayName AS "StartedUserName",		
	    c.LastUserDisplayName as "LastUserName" ,
	    c.LastMessageID as "LastMessageID",
		(SELECT x.Message FROM [{databaseSchema}].[{objectQualifier}Message] x 
          WHERE x.TopicID=c.TopicID and x.MessageID = c.LastMessageID) as "LastMessage",
	    (SELECT COUNT(1) FROM [{databaseSchema}].[{objectQualifier}Message] x 
          WHERE x.TopicID=c.TopicID and (x.Flags & 8) = 0) as "Replies"    
    FROM
        [{databaseSchema}].[{objectQualifier}Topic] c  
        JOIN [{databaseSchema}].[{objectQualifier}Forum] d ON d.ForumID=c.ForumID 
		JOIN [{databaseSchema}].[{objectQualifier}Category] cat ON cat.CategoryID = d.CategoryID     
        join [{databaseSchema}].[{objectQualifier}vaccess] x on (x.ForumID=d.ForumID AND x.UserID = @pageuserid AND x.ReadAccess <> 0)      
    WHERE
	   cat.BoardID = @boardid and  
	   c.lastposted > @sincedate and
       c.lastposted < @todate  and           
      (c.Flags & 8) = 0
    ORDER BY
	d.SortOrder,
    c.LastPosted desc;	
end
GO