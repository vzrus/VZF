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
    
    public partial class yaf_UserForum
    {
        public int UserID { get; set; }
        public int ForumID { get; set; }
        public int AccessMaskID { get; set; }
        public System.DateTime Invited { get; set; }
        public bool Accepted { get; set; }
    
        public virtual yaf_AccessMask yaf_AccessMask { get; set; }
        public virtual yaf_Forum yaf_Forum { get; set; }
        public virtual yaf_User yaf_User { get; set; }
    }
}
