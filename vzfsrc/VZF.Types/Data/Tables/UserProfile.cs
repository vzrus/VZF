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
    
    public partial class yaf_UserProfile
    {
        public int UserID { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<System.DateTime> LastActivity { get; set; }
        public string ApplicationName { get; set; }
        public bool IsAnonymous { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Country { get; set; }
        public string BlogServicePassword { get; set; }
        public string GoogleTalk { get; set; }
        public string ICQ { get; set; }
        public string YIM { get; set; }
        public string BlogServiceUsername { get; set; }
        public string MSN { get; set; }
        public string Twitter { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string XMPP { get; set; }
        public string AIM { get; set; }
        public string Interests { get; set; }
        public string Blog { get; set; }
        public Nullable<int> Gender { get; set; }
        public string BlogServiceUrl { get; set; }
        public string Occupation { get; set; }
        public string Homepage { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Facebook { get; set; }
        public string Region { get; set; }
        public string Skype { get; set; }
        public string TwitterId { get; set; }
    
        public virtual yaf_User yaf_User { get; set; }
    }
}
