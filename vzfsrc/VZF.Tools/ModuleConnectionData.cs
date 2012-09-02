using System;

namespace VZF.Tools
{
  /*  public class ModuleConnectionData
    {
        public ModuleConnection GetModuleConnectionData (ModuleClone moduleClone)
        {
            var mc = new ModuleConnection();
            ModuleConnection.ObjectId = moduleClone.ObjectId;
            ModuleConnection.ObjectInnerId = moduleClone.ObjectInnerId;
            ModuleConnection.ModuleType = moduleClone.ModuleType;
            return mc;
        }
       
    } */

    public class ModuleClone
    {
        public static int ObjectId
        {
            get; set;
        }

        public static int ObjectInnerId
        {
            get;
            set;
        }

        public static int ModuleType
        {
            get;
            set;
        }
    }

    public  class ModuleConnection
    {
        public  object ObjectId
        {
            get;
            set;
        }

        public  object ObjectInnerId
        {
            get;
            set;
        }
        public  int ModuleType
        {
            get;
            set;
        }

       
    }
}
