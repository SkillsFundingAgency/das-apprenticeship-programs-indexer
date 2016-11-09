using System.Diagnostics;
using System.Reflection;

namespace Sfa.Das.Sas.Indexer.ApplicationServices
{
    public static class VersionHelper
    {
        public static string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}