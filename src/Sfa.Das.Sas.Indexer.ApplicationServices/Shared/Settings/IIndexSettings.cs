namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings
{
    public interface IIndexSettings<T>
    {
        string IndexesAlias { get; }

        string PauseTime { get; }

        string StandardProviderDocumentType { get; }

        string FrameworkProviderDocumentType { get; }


        string ProviderExclusionList { get; }
    }
}