//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VZF.Types.Data
{
    using System;
    
    public partial class watchtopic_list_Result
    {
        public int WatchTopicID { get; set; }
        public int TopicID { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> LastMail { get; set; }
        public string TopicName { get; set; }
        public Nullable<int> Replies { get; set; }
        public int Views { get; set; }
        public Nullable<System.DateTime> LastPosted { get; set; }
        public Nullable<int> LastMessageID { get; set; }
        public Nullable<int> LastUserID { get; set; }
        public string LastUserName { get; set; }
        public string LastUserDisplayName { get; set; }
    }
}