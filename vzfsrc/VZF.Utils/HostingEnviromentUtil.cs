using System.Web.Hosting;

namespace VZF.Utils
{
    public static class HostingEnvironmentUtil
    {
        public static string MapPath(string path)
        {
            return System.Web.Hosting.HostingEnvironment.MapPath(path);
        }
    }
}