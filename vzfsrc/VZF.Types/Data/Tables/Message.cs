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
    
    public partial class yaf_Message
    {
        public yaf_Message()
        {
            this.yaf_Attachment = new HashSet<yaf_Attachment>();
            this.yaf_Forum = new HashSet<yaf_Forum>();
            this.yaf_Message1 = new HashSet<yaf_Message>();
            this.yaf_MessageHistory = new HashSet<yaf_MessageHistory>();
            this.yaf_Topic1 = new HashSet<yaf_Topic>();
        }
    
        public int MessageID { get; set; }
        public int TopicID { get; set; }
        public Nullable<int> ReplyTo { get; set; }
        public int Position { get; set; }
        public int Indent { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public System.DateTime Posted { get; set; }
        public string Message { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> Edited { get; set; }
        public int Flags { get; set; }
        public string EditReason { get; set; }
        public bool IsModeratorChanged { get; set; }
        public string DeleteReason { get; set; }
        public string ExternalMessageId { get; set; }
        public string ReferenceMessageId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string BlogPostID { get; set; }
        public Nullable<int> EditedBy { get; set; }
        public string UserDisplayName { get; set; }
    
        public virtual ICollection<yaf_Attachment> yaf_Attachment { get; set; }
        public virtual ICollection<yaf_Forum> yaf_Forum { get; set; }
        public virtual ICollection<yaf_Message> yaf_Message1 { get; set; }
        public virtual yaf_Message yaf_Message2 { get; set; }
        public virtual yaf_Topic yaf_Topic { get; set; }
        public virtual yaf_User yaf_User { get; set; }
        public virtual ICollection<yaf_MessageHistory> yaf_MessageHistory { get; set; }
        public virtual ICollection<yaf_Topic> yaf_Topic1 { get; set; }
    }
}
