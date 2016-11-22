using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility
{
    public interface IHttpGet
    {
        string Get(string url, string username, string pwd);
        Task<string> GetAsync(string url, string username, string pwd);
    }
}