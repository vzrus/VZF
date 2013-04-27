
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

    /// <summary>
    /// The module clone.
    /// </summary>
    public class ModuleClone
    {
        /// <summary>
        /// Gets or sets the object id.
        /// </summary>
        public static int ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the object inner id.
        /// </summary>
        public static int ObjectInnerId { get; set; }

        /// <summary>
        /// Gets or sets the module type.
        /// </summary>
        public static int ModuleType { get; set; }
    }
}
