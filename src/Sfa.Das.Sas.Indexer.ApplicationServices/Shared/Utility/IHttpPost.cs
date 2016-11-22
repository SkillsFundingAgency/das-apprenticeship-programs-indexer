namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility
{
    public interface IHttpPost
    {
        void Post(string url, string body, string user, string password);
    }
}