using System.IO;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility
{
    public interface IHttpGetFile
    {
        Stream GetFile(string url);
    }
}