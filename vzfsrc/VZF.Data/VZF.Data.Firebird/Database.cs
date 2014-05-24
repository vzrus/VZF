namespace VZF.Data.Firebird
{
    using YAF.Types;

    public static class Database
    {
        /// <summary>
        /// The create database.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="embeded">
        /// The embeded.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string CreateDatabase([NotNull] int? mid, bool embeded)
        {
            //FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
            // csb.ServerType = FbServerType.Embedded;
            // csb.Database = @I_"C:\Documents and Settings\bob\My Documents\Projects\yaffirebird\YetAnotherForum.NET\App_Data\yafnet.fdb";
            // csb.UserID = "SYSDBA";
            // csb.Password = "myfirebird";

            //if (System.IO.File.Exists(csb.Database))
            // {
            //System.IO.File.Delete(csb.Database);

            //  }
            // FbConnection.CreateDatabase(csb.ToString());  
            return null;
        }
    }
}
