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
    
    public partial class user_listmedals_Result
    {
        public int MedalID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string MedalURL { get; set; }
        public string RibbonURL { get; set; }
        public string SmallMedalURL { get; set; }
        public string SmallRibbonURL { get; set; }
        public short SmallMedalWidth { get; set; }
        public short SmallMedalHeight { get; set; }
        public short SmallRibbonWidth { get; set; }
        public short SmallRibbonHeight { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public Nullable<bool> Hide { get; set; }
        public Nullable<bool> OnlyRibbon { get; set; }
        public int Flags { get; set; }
        public Nullable<System.DateTime> DateAwarded { get; set; }
    }
}