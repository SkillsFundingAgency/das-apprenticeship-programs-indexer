using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData
{
    public interface IVstsClient
    {
        string GetFileContent(string path);
        Task<string> GetFileContentAsync(string path);

        string Get(string url);

        void Post(string url, string username, string pwd, string body);

        string GetLatesCommit();
    }
}