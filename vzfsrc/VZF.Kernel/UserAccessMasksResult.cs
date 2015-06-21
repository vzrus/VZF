// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="UserAccessMasksResult.cs">
//   VZF by vzrus
//   Copyright (C) 2013-2015 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The UserAccessMasksResult functionality.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace VZF.Kernel
{
    #region Using

    using System.Collections.Generic;

    using YAF.Types.Flags; 

    #endregion

    public class UserAccessMasksResult
    {
        public UserAccessMasksResult()
        {
            AccessMaskData = new List<AccessMask>();
        }

        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public int CurrentLevel { get; set; }
        public int NextLevel { get; set; }
       
        public List<AccessMask> AccessMaskData  { get; set; }

        public class AccessMask
        {
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public int AccessMaskId { get; set; }
            public string AccessMaskName { get; set; }
            public bool IsUserMask { get; set; }
            public int AccessMaskFlags { get; set; }

            public bool ReadAccess
            {
                get { return new AccessFlags(AccessMaskFlags).ReadAccess; }
            }

            public bool PostAccess
            {
                get { return new AccessFlags(AccessMaskFlags).PostAccess; }
            }

            public bool ReplyAccess
            {
                get { return new AccessFlags(AccessMaskFlags).ReplyAccess; }
            }

            public bool PriorityAccess
            {
                get { return new AccessFlags(AccessMaskFlags).PriorityAccess; }
            }

            public bool PollAccess
            {
                get { return new AccessFlags(AccessMaskFlags).PollAccess; }
            }

            public bool VoteAccess
            {
                get { return new AccessFlags(AccessMaskFlags).VoteAccess; }
            }

            public bool ModeratorAccess
            {
                get { return new AccessFlags(AccessMaskFlags).ModeratorAccess; }
            }

            public bool EditAccess
            {
                get { return new AccessFlags(AccessMaskFlags).EditAccess; }
            }

            public bool DeleteAccess
            {
                get { return new AccessFlags(AccessMaskFlags).DeleteAccess; }
            }

            public bool UploadAccess
            {
                get { return new AccessFlags(AccessMaskFlags).UploadAccess; }
            }

            public bool DownloadAccess
            {
                get { return new AccessFlags(AccessMaskFlags).DownloadAccess; }
            }
        }
    }
}
