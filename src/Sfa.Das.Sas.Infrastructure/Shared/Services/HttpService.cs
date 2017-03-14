﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    public class HttpService : IHttpGet, IHttpGetFile, IHttpPost
    {
        private readonly ILog _logger;
        private readonly IInfrastructureSettings _settings;

        public HttpService(ILog logger, IInfrastructureSettings settings)
        {
            this._logger = logger;
            _settings = settings;
        }

        public string Get(string url, string username, string pwd)
        {
            using (var client = new WebClient())
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{pwd}"));
                    client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
                }

                try
                {
                    client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

                    return client.DownloadString(url);
                }
                catch (WebException ex)
                {
                    _logger.Warn(ex, $"Cannot download string from {url}");
                    throw;
                }
            }
        }

        public Task<string> GetAsync(string url, string username, string pwd)
        {
            using (var client = new WebClient())
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{pwd}"));
                    client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
                }

                try
                {
                    client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

                    return client.DownloadStringTaskAsync(url);
                }
                catch (WebException ex)
                {
                    _logger.Warn(ex, $"Cannot download string from {url}");
                    throw;
                }
            }
        }

        public Stream GetFile(string url)
        {
            using (var client = new WebClient())
            {
                var content = client.DownloadData(url);
                return new MemoryStream(content);
            }
        }

        public void Post(string url, string body, string user, string password)
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromMilliseconds(_settings.HttpClientTimeout) })
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                using (var content = new StringContent(body, Encoding.UTF8, "application/json"))
                {
                    client.PostAsync(url, content).Wait();
                }
            }
        }
    }
}