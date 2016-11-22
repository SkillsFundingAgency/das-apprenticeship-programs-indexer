namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility
{
    public interface IServerEnvironment
    {
        string GetEnvironmentVariable(string settingName);
    }
}
