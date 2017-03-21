namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Core.Logging.Metrics;
    using Core.Logging.Models;
    using Newtonsoft.Json;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
    using Sfa.Das.Sas.Indexer.Infrastructure.Services.Models;

    public class VstsClient : IVstsClient
    {
        private readonly IHttpGet _httpHelper;

        private readonly IHttpPost _httpPost;

        private readonly ILog _logger;

        private readonly IAppServiceSettings _appServiceSettings;

        public VstsClient(
            IAppServiceSettings appServiceSettings,
            IHttpGet httpHelper,
            IHttpPost httpPost,
            ILog logger)
        {
            _appServiceSettings = appServiceSettings;
            _httpHelper = httpHelper;
            _httpPost = httpPost;
            _logger = logger;
        }

        public string GetFileContent(string path)
        {
            var url = string.Format(_appServiceSettings.VstsGitGetFilesUrlFormat, path);
            return Get(url);
        }

        public Task<string> GetFileContentAsync(string path)
        {
            var url = string.Format(_appServiceSettings.VstsGitGetFilesUrlFormat, path);
            return GetAsync(url);
        }

        public string Get(string url)
        {
            var timing = ExecutionTimer.GetTiming(() => _httpHelper.Get(url, _appServiceSettings.GitUsername, _appServiceSettings.GitPassword));

            var logEntry = new DependencyLogEntry
            {
                Identifier = "VstsContent",
                ResponseTime = timing.ElaspedMilliseconds,
                Url = url
            };

            _logger.Debug("VSTS content", logEntry);

            return timing.Result;
        }

        public Task<string> GetAsync(string url)
        {
            var timing = ExecutionTimer.GetTiming(() => _httpHelper.GetAsync(url, _appServiceSettings.GitUsername, _appServiceSettings.GitPassword));

            var logEntry = new DependencyLogEntry
            {
                Identifier = "VstsContent",
                ResponseTime = timing.ElaspedMilliseconds,
                Url = url
            };

            _logger.Debug("VSTS content", logEntry);

            return timing.Result;
        }

        public void Post(string url, string username, string pwd, string body)
        {
            _httpPost.Post(url, body, username, pwd);
        }

        public string GetLatesCommit()
        {
            var commitResponse = Get(_appServiceSettings.VstsGitAllCommitsUrl);
            var gitTree = JsonConvert.DeserializeObject<GitTree>(commitResponse);
            if (gitTree == null)
            {
                return string.Empty;
            }

            return gitTree.Value[0]?.CommitId;
        }
    }
}