namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared
{
    public interface IIndexerServiceFactory
    {
        IIndexerService<T> GetIndexerService<T>();
    }
}
