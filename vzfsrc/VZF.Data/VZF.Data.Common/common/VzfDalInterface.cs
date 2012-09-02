using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Objects;

namespace YAF.Classes.Data
{
    public interface IVzfDalInterface
    {
        bool accessmask_delete(object accessMaskID);
        DataTable accessmask_list(object boardId, object accessMaskID);
        DataTable accessmask_list(object boardId, object accessMaskID, object excludeFlags);

        void accessmask_save(object accessMaskID, object boardId, object name, object readAccess, object postAccess,
                             object replyAccess, object priorityAccess, object pollAccess, object voteAccess,
                             object moderatorAccess, object editAccess, object deleteAccess, object uploadAccess,
                             object downloadAccess, object sortOrder);

        DataTable active_list(object boardId, object guests, object showCrawlers, int interval, object styledNicks);

        DataTable active_list_user(object boardID, object userID, object Guests, object showCrawlers, int activeTime,
                                   object styledNicks);

        DataTable active_listforum(object forumID, object styledNicks);
        DataTable active_listtopic(object topicID, object styledNicks);
        DataRow active_stats(object boardId);
        void activeaccess_reset();
        DataTable admin_list(object boardId, object useStyledNicks);
        void album_delete(object AlbumID);
        int[] album_getstats(object UserID, object AlbumID);
        string album_gettitle(object AlbumID);
        void album_image_delete(object ImageID);
        void album_image_download(object ImageID);
        DataTable album_image_list(object AlbumID, object ImageID);

        void album_image_save(object ImageID, object AlbumID, object Caption, object FileName, object Bytes,
                              object ContentType);
        DataTable album_list(object UserID, object AlbumID);
        int album_save(object AlbumID, object UserID, object Title, object CoverImageID);
        void attachment_delete(object attachmentID);
        void attachment_download(object attachmentID);
        DataTable attachment_list(object messageID, object attachmentID, object boardId);

        void attachment_save(object messageID, object fileName, object bytes, object contentType,
                             System.IO.Stream stream);

        void bannedip_delete(object ID);

        DataTable bannedip_list(object boardId, object ID);
        void bannedip_save(object ID, object boardId, object Mask, string reason, int userID);
        void bbcode_delete(object bbcodeID);

        void bbcode_save(object bbcodeID, object boardId, object name, object description, object onclickjs,
                         object displayjs, object editjs, object displaycss, object searchregex, object replaceregex,
                         object variables, object usemodule, object moduleclass, object execorder);

        IEnumerable<TypedBBCode> BBCodeList(int boardID, int? bbcodeID);

        int board_create(object adminUsername, object adminUserEmail, object adminUserKey, object boardName,
                         object culture, object languageFile, object boardMembershipName, object boardRolesName,
                object rolePrefix);

        void board_delete(object boardId);
        DataTable board_list(object boardId);
        DataRow board_poststats(int? boardId, bool useStyledNicks, bool showNoCountPosts);
        void board_resync();
        void board_resync(object boardId);
        int board_save(object boardId, object languageFile, object culture, object name, object allowThreaded);
        DataRow board_stats();
        DataRow board_stats(object boardId);
        DataRow board_userstats(int? boardId);
        string[] buddy_addrequest(object FromUserID, object ToUserID);
        string buddy_approveRequest(object FromUserID, object ToUserID, object Mutual);
        string buddy_denyRequest(object FromUserID, object ToUserID);
        DataTable buddy_list(object FromUserID);
        string buddy_remove(object FromUserID, object ToUserID);
        bool category_delete(object CategoryID);
        DataTable category_list(object boardId, object categoryID);
        DataTable category_listread(object boardId, object userId, object categoryID);
        DataTable category_simplelist(int startID, int limit);
        void category_save(object boardId, object categoryId, object name, object categoryImage, object sortOrder);
        DataTable checkemail_list(object email);
        void checkemail_save(object userId, object hash, object email);
        DataTable checkemail_update(object hash);
        void choice_add(object pollID, object choice, object path, object mime);
        void choice_delete(object choiceID);
        void choice_update(object choiceID, object choice, object path, object mime);
        void choice_vote(object choiceID, object userId, object remoteIP);
        void db_getstats(MsSqlDbConnectionManager conn);
        string db_getstats_warning(MsSqlDbConnectionManager conn);
        void db_recovery_mode(MsSqlDbConnectionManager DBName, string dbRecoveryMode);
        string db_recovery_mode_warning(MsSqlDbConnectionManager DBName);
        void db_reindex(MsSqlDbConnectionManager conn);
        string db_reindex_warning(MsSqlDbConnectionManager conn);
        string db_runsql(string sql, MsSqlDbConnectionManager connMan, bool useTransaction);
        void db_shrink(MsSqlDbConnectionManager DBName);
        string db_shrink_warning(MsSqlDbConnectionManager DBName);
        DataSet ds_forumadmin(object boardId);
        void eventlog_create(object userId, object source, object description);
        void eventlog_delete(int boardId);
        void eventlog_delete(object eventLogID);
        DataTable eventlog_list(object boardId);
        void extension_delete(object extensionId);
        DataTable extension_edit(object extensionId);
        DataTable extension_list(object boardId);
        DataTable extension_list(object boardId, object extension);
        void extension_save(object extensionId, object boardId, object extension);
        bool forum_delete(object forumID);
        DataTable forum_list(object boardId, object forumID);
        DataTable forum_listall(object boardId, object userId);
        DataTable forum_listall(object boardId, object userId, object startAt);
        DataTable forum_listall_fromCat(object boardId, object categoryID);
        DataTable forum_listall_fromCat(object boardId, object categoryID, bool emptyFirstRow);
        DataTable forum_listall_sorted(object boardId, object userId);
        DataTable forum_listall_sorted(object boardId, object userId, int[] forumidExclusions);

        DataTable forum_listall_sorted(object boardId, object userId, int[] forumidExclusions, bool emptyFirstRow,
                                       int startAt);

        DataTable forum_listallMyModerated(object boardId, object userId);
        DataTable forum_listpath(object forumID);

        DataTable forum_listread(object boardId,
                       object userId, object categoryID, object parentID, object useStyledNicks);

        DataSet forum_moderatelist(object userId, object boardId);
        DataTable forum_moderators(bool useStyledNicks);
        void forum_resync(object boardId);
        void forum_resync(object boardId, object forumID);

        long forum_save(object forumID,
                        object categoryID, object parentID, object name,
                        object description, object sortOrder, object locked,
                        object hidden, object isTest, object moderated,
                        object accessMaskID, object remoteURL,
                        object themeURL,
                        object imageURL,
                        object styles,
                        bool dummy);

        int forum_save_parentschecker(object forumID, object parentID);
        DataTable forum_simplelist(int startID, int limit);
        DataTable forumaccess_group(object groupID);
        DataTable forumaccess_list(object forumID);
        void forumaccess_save(object forumID, object groupID, object accessMaskID);
        IEnumerable<TypedForumListAll> ForumListAll(int boardId, int userId);
        IEnumerable<TypedForumListAll> ForumListAll(int boardId, int userId, int startForumId);
        bool forumpage_initdb(out string errorStr, bool debugging);
        string forumpage_validateversion(int appVersion);

        DataTable GetSearchResult(string toSearchWhat, string toSearchFromWho, SearchWhatFlags searchFromWhoMethod,
                                  SearchWhatFlags searchWhatMethod, int forumIDToStartAt, int userId, int boardId,
                                  int maxResults, bool useFullText, bool searchDisplayName);

        void group_delete(object groupID);
        DataTable group_list(object boardId, object groupID);
        void group_medal_delete(object groupID, object medalID);
        DataTable group_medal_list(object groupID, object medalID);

        void group_medal_save(
            object groupID, object medalID, object message,
            object hide, object onlyRibbon, object sortOrder);

        DataTable group_member(object boardId, object userId);
        DataTable group_rank_style(object boardID);

        long group_save(object groupID, object boardId, object name,
                        object isAdmin, object isGuest, object isStart, object isModerator,
                        object accessMaskID, object pmLimit, object style, object sortOrder,
                        object description,
                        object usrSigChars,
                        object usrSigBBCodes,
                        object usrSigHTMLTags,
                        object usrAlbums,
                        object usrAlbumImages);

        void mail_create(object from, object fromName, object to, object toName, object subject, object body,
                         object bodyHtml);

        void mail_createwatch(object topicID, object from, object fromName, object subject, object body, object bodyHtml,
                              object userId);
        void mail_delete(object mailID);
        // DataTable mail_list(object processId); 
        IEnumerable<TypedMailList> MailList(long processId);
        void medal_delete(object boardId, object medalID, object category);
        void medal_delete(object medalID);
        DataTable medal_list(object boardId, object category);
        DataTable medal_list(object medalID);
        DataTable medal_listusers(object medalID);
        void medal_resort(object boardId, object medalID, int move);

        bool medal_save(
            object boardId, object medalID, object name, object description, object message, object category,
            object medalURL, object ribbonURL, object smallMedalURL, object smallRibbonURL, object smallMedalWidth,
            object smallMedalHeight, object smallRibbonWidth, object smallRibbonHeight, object sortOrder, object flags);

        string message_AddThanks(object fromUserID, object messageID);
        void message_approve(object messageID);

        void message_delete(object messageID, bool isModeratorChanged, string deleteReason, int isDeleteAction,
                            bool DeleteLinked);

        void message_delete(object messageID, bool isModeratorChanged, string deleteReason, int isDeleteAction,
                            bool DeleteLinked, bool eraseMessage);

        DataTable message_findunread(object topicID, object messageId, object lastRead, object showDeleted,
                                     object authorUserID);
       
        DataTable message_getRepliesList(object messageID);
        DataTable message_GetTextByIds(string messageIDs);
        DataTable message_GetThanks(object messageID);
        DataTable message_list(object messageID);
        DataTable message_listreported(object forumID);
        DataTable message_listreporters(int messageID);
        DataTable message_listreporters(int messageID, object userID);
        void message_move(object messageID, object moveToTopic, bool moveAll);
        string message_RemoveThanks(object fromUserID, object messageID);

        void message_report(object messageID, object userId, object reportedDateTime, object reportText);
        void message_reportcopyover(object messageID);
        void message_reportresolve(object messageFlag, object messageID, object userId);

        bool message_save(
            [NotNull] object topicId,
            [NotNull] object userId,
            [NotNull] object message,
            [NotNull] object userName,
            [NotNull] object ip,
            [NotNull] object posted,
            [NotNull] object replyTo,
            [NotNull] object flags,
            ref long messageId);

        DataTable message_secdata(int MessageID, object pageUserId);

        DataTable message_simplelist(int StartID, int Limit);

        int message_ThanksNumber(object messageID);

        DataTable message_unapproved(object forumID);

        void message_update(object messageID, object priority, object message, object description, object subject,
                            object flags, object reasonOfEdit, object isModeratorChanged, object origMessage,
                            object editedBy);

        void message_update(object messageID, object priority, object message, object description, object subject,
                            object flags, object reasonOfEdit, object isModeratorChanged, object overrideApproval,
                            object origMessage, object editedBy);

        IEnumerable<TypedAllThanks> MessageGetAllThanks(string messageIdsSeparatedWithColon);
        DataTable messagehistory_list(int messageID, int daysToClean);
        IEnumerable<TypedMessageList> MessageList(int messageID);
        void nntpforum_delete(object nntpForumID);
        DataTable nntpforum_list(object boardId, object minutes, object nntpForumID, object active);

        void nntpforum_save(object nntpForumID, object nntpServerID, object groupName, object forumID, object active,
                            object cutoffdate);

        void nntpforum_update(object nntpForumID, object lastMessageNo, object userId);
        IEnumerable<TypedNntpForum> NntpForumList(int boardId, int? minutes, int? nntpForumID, bool? active);
        void nntpserver_delete(object nntpServerID);
        DataTable nntpserver_list(object boardId, object nntpServerID);

        void nntpserver_save(object nntpServerID, object boardId, object name, object address, object port,
                             object userName, object userPass);

        DataTable nntptopic_list(object thread);

        void nntptopic_savemessage(
            object nntpForumID,
            object topic,
            object body,
            object userId,
            object userName,
            object ip,
            object posted,
            object externalMessageId,
            object referenceMessageId);

        DataRow pageload(
            object sessionID,
            object boardId,
            object userKey,
            object ip,
            object location,
            object forumPage,
            object browser,
            object platform,
            object categoryID,
            object forumID,
            object topicID,
            object messageID,
            object isCrawler,
            object isMobileDevice,
            object donttrack);

        void pmessage_archive(object userPMessageID);
        void pmessage_delete(object userPMessageID);
        void pmessage_delete(object userPMessageID, bool fromOutbox);
        DataTable pmessage_info();
        DataTable pmessage_list(object toUserID, object fromUserID, object userPMessageID);
        DataTable pmessage_list(object userPMessageID);
        void pmessage_markread(object userPMessageID);
        void pmessage_prune(object daysRead, object daysUnread);
        void pmessage_save(object fromUserID, object toUserID, object subject, object body, object Flags);
        void poll_remove(object pollGroupID, object pollID, object boardId, bool removeCompletely, bool removeEverywhere);
        int? poll_save(List<PollSaveList> pollList);
        DataTable poll_stats(int? pollId);
        int pollgroup_attach(int? pollGroupId, int? topicId, int? forumId, int? categoryId, int? boardId);

        void pollgroup_remove(object pollGroupID, object topicId, object forumId, object categoryId, object boardId,
                              bool removeCompletely, bool removeEverywhere);

        DataTable pollgroup_stats(int? pollGroupId);
        DataTable pollgroup_votecheck(object pollGroupId, object userId, object remoteIp);
        IEnumerable<TypedPollGroup> PollGroupList(int userID, int? forumId, int boardId);
        DataTable pollvote_check(object pollid, object userid, object remoteip);
        DataTable post_alluser(object boardid, object userid, object pageUserID, object topCount);
        DataTable post_last10user(object boardID, object userID, object pageUserID);

        DataTable post_list(
            object topicId,
            object authoruserId,
            object updateViewCount,
            bool showDeleted,
            bool styledNicks,
            DateTime sincePostedDate,
            DateTime toPostedDate,
            DateTime sinceEditedDate,
            DateTime toEditedDate,
            int pageIndex,
            int pageSize,
            int sortPosted,
            int sortEdited,
            int sortPosition,
            bool showThanks,
            int messagePosition);

        DataTable post_list_reverse10(object topicID);
        void rank_delete(object rankID);
        DataTable rank_list(object boardId, object rankID);

        void rank_save(object rankID, object boardId, object name,
                       object isStart, object isLadder, object minPosts, object rankImage,
                       object pmLimit, object style, object sortOrder,
                       object description,
                       object usrSigChars,
                       object usrSigBBCodes,
                       object usrSigHTMLTags,
                       object usrAlbums,
                       object usrAlbumImages);

        DataTable recent_users(object boardID, int timeSinceLastLogin, object styledNicks);
        DataTable registry_list();
        DataTable registry_list(object name, object boardId);
        void registry_save(object name, object value);
        void registry_save(object name, object value, object boardId);
        void replace_words_delete(object id);
        DataTable replace_words_list(object boardId, object id);
        void replace_words_save(object boardId, object id, object badword, object goodword);

        DataTable rss_topic_latest(object boardId, object numOfPostsToRetrieve, object pageUserId, bool useStyledNicks,
                                   bool showNoCountPosts);
        DataTable rsstopic_list(int forumID);
        Boolean shoutbox_clearmessages(int boardId);
        DataTable shoutbox_getmessages(int boardId, int numberOfMessages, object useStyledNicks);
        bool shoutbox_savemessage(int boardId, string message, string userName, int userID, object ip);
        void smiley_delete(object smileyID);
        DataTable smiley_list(object boardId, object smileyID);
        DataTable smiley_listunique(object boardId);
        void smiley_resort(object boardId, object smileyID, int move);

        void smiley_save(object smileyID, object boardId, object code, object icon, object emoticon, object sortOrder,
                         object replace);

        IEnumerable<TypedSmileyList> SmileyList(int boardId, int? smileyID);
        void system_deleteinstallobjects();

        void system_initialize(string forumName, string timeZone, string culture, string languageFile, string forumEmail,
                               string smtpServer, string userName, string userEmail, object providerUserKey,
                               string rolePrefix);

        void system_initialize_executescripts(string script, string scriptFile, bool useTransactions);
        void system_initialize_fixaccess(bool bGrant);
        DataTable system_list();
        void system_updateversion(int version, string name);
        DataTable topic_active(object boardId, object pageUserId, object since, object categoryID, object useStyledNicks);
        DataTable topic_announcements(object boardId, object numOfPostsToRetrieve, object pageUserId);
        long topic_create_by_message(object messageID, object forumId, object newTopicSubj);
        void topic_delete(object topicID);
        void topic_delete(object topicID, object eraseTopic);
        void topic_favorite_add(object userID, object topicID);

        DataTable topic_favorite_details(object boardID, object pageUserId, object since, object categoryID,
                                         object useStyledNicks);

        DataTable topic_favorite_list(object userID);
        void topic_favorite_remove(object userID, object topicID);
        int topic_findduplicate(object topicName);
        DataTable topic_findnext(object topicID);
        DataTable topic_findprev(object topicID);
        DataRow topic_info(object topicID);

        DataTable topic_latest(object boardId, object numOfPostsToRetrieve, object pageUserId, bool useStyledNicks,
                               bool showNoCountPosts);

        DataTable topic_list(object forumID, object userId, object announcement, object date, object offset,
                             object count, object useStyledNicks, object showMoved);

        int topic_prune([NotNull] object boardID, [NotNull] object forumID, [NotNull] object days,
                       [NotNull] object permDelete);

        long topic_save(object forumID, object subject, object description, object message, object userId,
                        object priority, object userName, object ip, object posted, object blogPostID, object flags,
                        ref long messageID);

        DataTable topic_simplelist(int StartID, int Limit);
        void topic_updatetopic(int topicId, string topic);
       
        int TopicFavoriteCount(int topicId);
        void unencode_all_topics_subjects(Func<string, string> decodeTopicFunc);
        DataTable user_accessmasks(object boardId, object userId);
        DataTable user_activity_rank(object boardId, object startDate, object displayNumber);
        void user_addignoreduser(object userId, object ignoredUserId);
        void user_addpoints(object userId, object points);

        void user_adminsave
            (object boardId, object userId, object name, object displayName, object email, object flags, object rankID);

        void user_approve(object userId);
        void user_approveall(object boardId);

        int user_aspnet(int boardId, string userName, string displayName, string email, object providerUserKey,
                        object isApproved);

        DataTable user_avatarimage(object userId);
        bool user_changepassword(object userId, object oldPassword, object newPassword);
        void user_delete(object userId);
        void user_deleteavatar(object userId);
        void user_deleteold(object boardId, object days);
        DataTable user_emails(object boardId, object groupID);
        int user_get(int boardId, object providerUserKey);
        DataTable user_getalbumsdata(object userID, object boardID);
        int user_getpoints(object userId);
        string user_getsignature(object userId);
        DataTable user_getsignaturedata(object userID, object boardID);
        int user_getthanks_from(object userID, object pageUserId);
        int[] user_getthanks_to(object userID, object pageUserId);
        int? user_guest(object boardId);
        DataTable user_ignoredlist(object userId);
        bool user_isuserignored(object userId, object ignoredUserId);

        DataRow user_lazydata(object userID, object boardID, bool showPendingMails, bool showPendingBuddies,
                              bool showUnreadPMs, bool showUserAlbums, bool styledNicks);

        DataTable user_list(object boardID, object userID, object approved);

        DataTable user_list(object boardId, object userId, object approved, object groupID, object rankID,
                            object useStyledNicks);

        DataTable user_list(object boardID, object userID, object approved, object useStyledNicks);
        DataTable user_listmedals(object userId);

        DataTable user_listmembers(
            object boardId,
            object userId,
            object approved,
            object groupId,
            object rankId,
            object useStyledNicks,
            object lastUserId,
            object literals,
            object exclude,
            object beginsWith,
            object pageIndex,
            object pageSize,
            object sortName,
            object sortRank,
            object sortJoined,
            object sortPosts,
            object sortLastVisit,
            object numPosts,
            object numPostCompare);

        object user_login(object boardId, object name, object password);
        void user_medal_delete(object userId, object medalID);
        DataTable user_medal_list(object userId, object medalID);

        void user_medal_save(
            object userId, object medalID, object message,
            object hide, object onlyRibbon, object sortOrder, object dateAwarded);

        void user_migrate(object userId, object providerUserKey, object updateProvider);
        int user_nntp(object boardId, object userName, object email, int? timeZone);
        DataTable user_pmcount(object userId);
        object user_recoverpassword(object boardId, object userName, object email);

        bool user_register(object boardId, object userName, object password, object hash, object email, object location,
                           object homePage, object timeZone, bool approved);

        void user_removeignoreduser(object userId, object ignoredUserId);
        void user_removepoints(object userId, object points);
        void user_removepointsByTopicID(object topicID, object points);
        bool user_RepliedTopic([NotNull] object messageId, [NotNull] object userId);

        void user_save(
            object userId,
            object boardId,
            object userName,
            object displayName,
            object email,
            object timeZone,
            object languageFile,
            object culture,
            object themeFile,
            object useSingleSignOn,
            object textEditor,
            object overrideDefaultThemes,
            object approved,
            object pmNotification,
            object autoWatchTopics,
            object dSTUser,
            object isHidden,
            object notificationType);

        void user_saveavatar(object userId, object avatar, System.IO.Stream stream, object avatarImageType);

        void user_savenotification(
            object userId,
            object pmNotification,
            object autoWatchTopics,
            object notificationType,
            object dailyDigest);

        void user_savepassword(object userId, object password);
        void user_savesignature(object userId, object signature);
        void user_setinfo(int boardId, System.Web.Security.MembershipUser user);
        void user_setnotdirty(int boardId, int userId);
        void user_setpoints(object userId, object points);
        void user_setrole(int boardId, object providerUserKey, object role);
        DataTable user_simplelist(int StartID, int Limit);
        void user_suspend(object userId, object suspend);
        bool user_ThankedMessage(object messageId, object userId);
        int user_ThankFromCount([NotNull] object userId);
        DataTable user_viewallthanks(object UserID, object pageUserId);

        IEnumerable<TypedUserFind> UserFind(int boardId, bool filter, string userName, string email, string displayName,
                                            object notificationType, object dailyDigest);

        void userforum_delete(object userId, object forumID);
        DataTable userforum_list(object userId, object forumID);
        void userforum_save(object userId, object forumID, object accessMaskID);
        DataTable usergroup_list(object userId);
        void usergroup_save(object userId, object groupID, object member);

        IEnumerable<TypedUserList> UserList(int boardId, int? userId, bool? approved, int? groupID, int? rankID,
                                            bool? useStyledNicks);

        void watchforum_add(object userId, object forumID);
        DataTable watchforum_check(object userId, object forumID);
        void watchforum_delete(object watchForumID);
        DataTable watchforum_list(object userId);
        void watchtopic_add(object userId, object topicID);
        DataTable watchtopic_check(object userId, object topicID);
        void watchtopic_delete(object watchTopicID);
        DataTable watchtopic_list(object userId);

    }
}
