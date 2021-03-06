//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VZF.Types.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class yaf_Forum
    {
        public yaf_Forum()
        {
            this.yaf_Active = new HashSet<yaf_Active>();
            this.yaf_Forum1 = new HashSet<yaf_Forum>();
            this.yaf_ForumAccess = new HashSet<yaf_ForumAccess>();
            this.yaf_ForumReadTracking = new HashSet<yaf_ForumReadTracking>();
            this.yaf_NntpForum = new HashSet<yaf_NntpForum>();
            this.yaf_Topic1 = new HashSet<yaf_Topic>();
            this.yaf_UserForum = new HashSet<yaf_UserForum>();
            this.yaf_WatchForum = new HashSet<yaf_WatchForum>();
        }
    
        public int ForumID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short SortOrder { get; set; }
        public Nullable<System.DateTime> LastPosted { get; set; }
        public Nullable<int> LastTopicID { get; set; }
        public Nullable<int> LastMessageID { get; set; }
        public Nullable<int> LastUserID { get; set; }
        public string LastUserName { get; set; }
        public int NumTopics { get; set; }
        public int NumPosts { get; set; }
        public string RemoteURL { get; set; }
        public int Flags { get; set; }
        public Nullable<bool> IsLocked { get; set; }
        public Nullable<bool> IsHidden { get; set; }
        public Nullable<bool> IsNoCount { get; set; }
        public Nullable<bool> IsModerated { get; set; }
        public string ThemeURL { get; set; }
        public Nullable<int> PollGroupID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string ImageURL { get; set; }
        public string Styles { get; set; }
        public Nullable<int> NodeIndex { get; set; }
        public Nullable<int> RightChildIndex { get; set; }
        public string LastUserDisplayName { get; set; }
    
        public virtual ICollection<yaf_Active> yaf_Active { get; set; }
        public virtual yaf_Category yaf_Category { get; set; }
        public virtual ICollection<yaf_Forum> yaf_Forum1 { get; set; }
        public virtual yaf_Forum yaf_Forum2 { get; set; }
        public virtual yaf_Message yaf_Message { get; set; }
        public virtual yaf_PollGroupCluster yaf_PollGroupCluster { get; set; }
        public virtual yaf_Topic yaf_Topic { get; set; }
        public virtual yaf_User yaf_User { get; set; }
        public virtual ICollection<yaf_ForumAccess> yaf_ForumAccess { get; set; }
        public virtual ICollection<yaf_ForumReadTracking> yaf_ForumReadTracking { get; set; }
        public virtual ICollection<yaf_NntpForum> yaf_NntpForum { get; set; }
        public virtual ICollection<yaf_Topic> yaf_Topic1 { get; set; }
        public virtual ICollection<yaf_UserForum> yaf_UserForum { get; set; }
        public virtual ICollection<yaf_WatchForum> yaf_WatchForum { get; set; }
    }
}
