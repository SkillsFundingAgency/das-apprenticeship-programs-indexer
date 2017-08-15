namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services
{
    public interface IMonitoringService
    {
        void SendMonitoringNotification(params string[] url);
    }
}
