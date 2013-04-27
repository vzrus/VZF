
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
    /// The module connection.
    /// </summary>
    public class ModuleConnection
    {
        /// <summary>
        /// Gets or sets the object id.
        /// </summary>
        public object ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the object inner id.
        /// </summary>
        public object ObjectInnerId { get; set; }

        /// <summary>
        /// Gets or sets the module type.
        /// </summary>
        public int ModuleType { get; set; }
    }
}
