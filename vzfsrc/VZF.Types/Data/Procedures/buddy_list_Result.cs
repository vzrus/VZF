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
    
    public partial class buddy_list_Result
    {
        public Nullable<int> UserID { get; set; }
        public int BoardID { get; set; }
        public string Name { get; set; }
        public System.DateTime Joined { get; set; }
        public int NumPosts { get; set; }
        public string RankName { get; set; }
        public bool Approved { get; set; }
        public int FromUserID { get; set; }
        public System.DateTime Requested { get; set; }
    }
}