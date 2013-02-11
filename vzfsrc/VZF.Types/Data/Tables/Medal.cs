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
    
    public partial class yaf_Medal
    {
        public yaf_Medal()
        {
            this.yaf_GroupMedal = new HashSet<yaf_GroupMedal>();
            this.yaf_UserMedal = new HashSet<yaf_UserMedal>();
        }
    
        public int BoardID { get; set; }
        public int MedalID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public string MedalURL { get; set; }
        public string RibbonURL { get; set; }
        public string SmallMedalURL { get; set; }
        public string SmallRibbonURL { get; set; }
        public short SmallMedalWidth { get; set; }
        public short SmallMedalHeight { get; set; }
        public Nullable<short> SmallRibbonWidth { get; set; }
        public Nullable<short> SmallRibbonHeight { get; set; }
        public byte SortOrder { get; set; }
        public int Flags { get; set; }
    
        public virtual ICollection<yaf_GroupMedal> yaf_GroupMedal { get; set; }
        public virtual ICollection<yaf_UserMedal> yaf_UserMedal { get; set; }
    }
}