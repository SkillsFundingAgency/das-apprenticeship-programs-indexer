using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using SFA.DAS.NLog.Logger;

    public class MonitoringService : IMonitoringService
    {
        private readonly ILog _logger;

        public MonitoringService(ILog logger)
        {
            _logger = logger;
        }

        public void SendMonitoringNotification(params string[] urls)
        {
            using (var client = new HttpClient())
            {
                foreach (var url in urls)
                {
                    if (string.IsNullOrWhiteSpace(url))
                    {
                        return;
                    }

                    try
                    {
                        _logger.Debug($"Sending a request to {url}");
                        var task = Task.Run(() => client.GetAsync(url));
                        task.Wait();

                        if (task.Result.StatusCode != HttpStatusCode.OK)
                        {
                            _logger.Warn($"Something failed trying to send a request to StatusCake: {url}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn(ex, $"Something failed trying to send a request to StatusCake: {url}");
                    }
                }
            }
        }
    }
}
