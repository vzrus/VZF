using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YAF.Classes.Data
{
    public class CurrentModule
    {
        public int ModuleId { get; set; }
        public string ModuleShortName { get; set; }
        public int ModuleTamplateId { get; set; }
        public string ModuleTemplateShortName { get; set; }
        public int ModuleConnectionId { get; set; }
        public string ModuleConnectionName { get; set; }
    }

}
