using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YAF.Types.Flags;

namespace VZF.Kernel
{
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
