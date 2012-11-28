/* VZF by vzrus
 * Copyright (C) 2012 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
namespace VZF.Kernel
{
    #region Using

    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using VZF.Data.Common;
 
    using YAF.Core;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using YAF.Utils; 

    #endregion

    public class UserForumAccess
    {
        private static string _yesImage;
        private static string _noImage;

        public static List<UserAccessMasksResult> GetUserAccessListBase(int userId)
        {
            var values = new List<UserAccessMasksResult>();
            using (
                var forumaccess = CommonDb.user_accessmasksbyforum(YafContext.Current.PageModuleID,
                                                              YafContext.Current.PageBoardID, userId))
                                                              {
                values = GetUserAccessRawData(forumaccess, values, false);
            }
            return values;
        }

        public static IOrderedEnumerable<UserAccessMasksResult> GetUserAccessListSortedByForumName(int userId)
        {
            var values = GetUserAccessListBase(userId);
            return values.OrderBy(s => s.ForumName);
        }

        private static List<UserAccessMasksResult> GetUserAccessRawData(DataTable groupaccess, List<UserAccessMasksResult> values, bool isGroup)
        {
            if (groupaccess != null && groupaccess.Rows.Count > 0)
            {
                for (var i = 0; i < groupaccess.Rows.Count; i++)
                {
                    var currentMask = groupaccess.Rows[i]["AccessMaskName"].ToString().Trim();
                    var currentName = groupaccess.Rows[i]["ForumName"].ToString().Trim();
                    int currentlevel = DashCounter(currentName);
                    bool singleFirstOrAnyLastRow = (i == 0 && groupaccess.Rows.Count == 1) ||
                                                   i == groupaccess.Rows.Count - 1;
                    int nextlevel = singleFirstOrAnyLastRow
                                        ? 0
                                        : DashCounter(groupaccess.Rows[i + 1]["ForumName"].ToString().Trim());

                    var uams = new UserAccessMasksResult();
                    var forumid = groupaccess.Rows[i]["ForumID"].ToType<int>();
                    uams.ForumId = forumid;
                    uams.CurrentLevel = currentlevel;
                    uams.NextLevel = nextlevel;
                    uams.ForumName = currentName;
                    var am = new UserAccessMasksResult.AccessMask
                    {
                        AccessMaskName = currentMask,
                        AccessMaskId = groupaccess.Rows[i]["AccessMaskID"].ToType<int>(),
                        AccessMaskFlags = groupaccess.Rows[i]["AccessMaskFlags"].ToType<int>(),
                        IsUserMask = groupaccess.Rows[i]["IsUserMask"].ToType<bool>(),
                        GroupId = groupaccess.Rows[i]["GroupID"].ToType<int>(),
                        GroupName = groupaccess.Rows[i]["GroupName"].ToString()
                    };

                    int exists = values.FindIndex(vvv => vvv.ForumId == forumid);

                    if (exists > -1)
                    {
                        if (am.AccessMaskName.IsSet())
                        {
                            values[exists].AccessMaskData.Add(am);
                        }
                    }
                    else
                    {
                        uams.AccessMaskData.Add(am);
                        values.Add(uams);
                    }
                }
            }
            return values;
        }

        // 
        public static string AddAccessImagesAndTips(int userId, int forumId)
        {
            _yesImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASACCESS");
            _noImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASNOACCESS");
            var values = GetUserAccessListBase(userId);

            bool iReadAccess = false;
            string iReadAccessLegend = "RE:";

            bool iPostAccess = false;
            string iPostAccessLegend = "PO: ";

            bool iReplyAccess = false;
            string iReplyAccessLegend = "RP: ";

            bool iPriorityAccess = false;
            string iPriorityAccessLegend = "PR: ";

            bool iPollAccess = false;
            string iPollAccessLegend = "PL: ";

            bool iVoteAccess = false;
            string iVoteAccessLegend = "VO: ";

            bool iModeratorAccess = false;
            string iModeratorAccessLegend = "MD: ";

            bool iEditAccess = false;
            string iEditAccessLegend = "ED: ";

            bool iDeleteAccess = false;
            string iDeleteAccessLegend = "DE: ";

            bool iUploadAccess = false;
            string iUploadAccessLegend = "UP: ";

            bool iDownloadAccess = false;
            string iDownloadAccessLegend = "DL: ";

            var sb = new StringBuilder();
            foreach (var forum in values)
            {
                if (forumId != forum.ForumId) continue;
                foreach (var accessMask in forum.AccessMaskData)
                {
                    iReadAccess = iReadAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.ReadAccess);
                    iReadAccessLegend = iReadAccessLegend +
                                        (!accessMask.IsUserMask
                                             ? ("Group:" + accessMask.GroupName + ":")
                                             : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                        "{0}".FormatWith(iReadAccess ? "+" : "-") + ",";

                    iPostAccess = iPostAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.PostAccess);
                    iPostAccessLegend = iPostAccessLegend +
                                        (!accessMask.IsUserMask
                                             ? ("Group:" + accessMask.GroupName + ":")
                                             : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                        "{0}".FormatWith(iPostAccess ? "+" : "-") + ",";

                    iReplyAccess = iReplyAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.ReplyAccess);
                    iReplyAccessLegend = iReplyAccessLegend +
                                         (!accessMask.IsUserMask
                                              ? ("Group:" + accessMask.GroupName + ":")
                                              : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                         "{0}".FormatWith(iReplyAccess ? "+" : "-") + ",";

                    iPriorityAccess = iPriorityAccess ||
                                      accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.PriorityAccess);
                    iPriorityAccessLegend = iPriorityAccessLegend +
                                            (!accessMask.IsUserMask
                                                 ? ("Group:" + accessMask.GroupName + ":")
                                                 : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                            "{0}".FormatWith(iPriorityAccess ? "+" : "-") + ",";

                    iPollAccess = iPollAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.PollAccess);
                    iPollAccessLegend = iPollAccessLegend +
                                        (!accessMask.IsUserMask
                                             ? ("Group:" + accessMask.GroupName + ":")
                                             : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                        "{0}".FormatWith(iPollAccess ? "+" : "-") + ",";

                    iVoteAccess = iVoteAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.VoteAccess);
                    iVoteAccessLegend = iVoteAccessLegend +
                                        (!accessMask.IsUserMask
                                             ? ("Group:" + accessMask.GroupName + ":")
                                             : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                        "{0}".FormatWith(iVoteAccess ? "+" : "-") + ",";

                    iModeratorAccess = iModeratorAccess ||
                                       accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.ModeratorAccess);
                    iModeratorAccessLegend = iModeratorAccessLegend +
                                             (!accessMask.IsUserMask
                                                  ? ("Group:" + accessMask.GroupName + ":")
                                                  : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                             "{0}".FormatWith(iModeratorAccess ? "+" : "-") + ",";

                    iEditAccess = iEditAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.EditAccess);
                    iEditAccessLegend = iEditAccessLegend +
                                        (!accessMask.IsUserMask
                                             ? ("Group:" + accessMask.GroupName + ":")
                                             : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                        "{0}".FormatWith(iEditAccess ? "+" : "-") + ",";

                    iDeleteAccess = iDeleteAccess ||
                                    accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.DeleteAccess);
                    iDeleteAccessLegend = iDeleteAccessLegend +
                                          (!accessMask.IsUserMask
                                               ? ("Group:" + accessMask.GroupName + ":")
                                               : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                          "{0}".FormatWith(iDeleteAccess ? "+" : "-") + ",";

                    iUploadAccess = iUploadAccess ||
                                    accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.UploadAccess);
                    iUploadAccessLegend = iUploadAccessLegend +
                                          (!accessMask.IsUserMask
                                               ? ("Group:" + accessMask.GroupName + ":")
                                               : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                          "{0}".FormatWith(iUploadAccess ? "+" : "-") + ",";

                    iDownloadAccess = iDownloadAccess ||
                                      accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.DownloadAccess);
                    iDownloadAccessLegend = iDownloadAccessLegend +
                                            (!accessMask.IsUserMask
                                                 ? ("Group:" + accessMask.GroupName + ":")
                                                 : " PersonalAccess:") + accessMask.AccessMaskName + ":" +
                                            "{0}".FormatWith(iDownloadAccess ? "+" : "-") + ",";
                }


                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                    "img{0}re".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iReadAccess ? "+" : "-"),
                    iReadAccessLegend.TrimEnd(','),
                    iReadAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                    "img{0}po".FormatWith(forum.ForumId),
                   "{0}".FormatWith(iPostAccess ? "+" : "-"),
                   iPostAccessLegend.TrimEnd(','),
                   iPostAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}rp".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iReplyAccess ? "+" : "-"),
                    iReplyAccessLegend.TrimEnd(','),
                    iReplyAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}pr".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iPriorityAccess ? "+" : "-"),
                    iPriorityAccessLegend.TrimEnd(','),
                 iPriorityAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}pl".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iPollAccess ? "+" : "-"),
                    iPollAccessLegend.TrimEnd(','),
                    iPollAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}vo".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iVoteAccess ? "+" : "-"),
                    iVoteAccessLegend.TrimEnd(','),
                    iVoteAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}mo".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iModeratorAccess ? "+" : "-"),
                    iModeratorAccessLegend.TrimEnd(','),
                    iModeratorAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}ed".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iEditAccess ? "+" : "-"),
                    iEditAccessLegend.TrimEnd(','),
                    iEditAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}de".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iDeleteAccess ? "+" : "-"),
                    iDeleteAccessLegend.TrimEnd(','),
                    iDeleteAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}up".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iUploadAccess ? "+" : "-"),
                    iUploadAccessLegend.TrimEnd(','),
                    iUploadAccess ? _yesImage : _noImage));
                sb.Append(@"<img id='{0}' alt='{1}'  title='{2}' src='{3}' />".FormatWith(
                     "img{0}dl".FormatWith(forum.ForumId),
                    "{0}".FormatWith(iDownloadAccess ? "+" : "-"),
                    iDownloadAccessLegend.TrimEnd(','),
                    iDownloadAccess ? _yesImage : _noImage));
                
                values.Remove(forum);
                if (forumId == forum.ForumId) break;
            }
            return sb.ToString();
        }
        public static int DashCounter(string title)
        {
            int dashCounter = 0;
            int currentLevel = 0;

            // a loop to fathom out level from dashes.Needs date layer modification.
            foreach (char c in title)
            {
                if (c == '-')
                {
                    dashCounter++;
                }
                if (dashCounter > 0)
                {
                    if (c == ' ')
                    {
                        break;
                    }
                }
            }

            if (dashCounter == 0)
            {
                // this is category
                currentLevel = 0;
            }

            if (dashCounter >= 1)
            {
                // forums
                currentLevel = (dashCounter - 1) / 2 + 1;
            }
            return currentLevel;
        }
    }
}
