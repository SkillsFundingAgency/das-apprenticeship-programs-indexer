using System;
using System.Net;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Infrastructure
{
    public class WebClientWrapper : IWebClient
    {
        private WebClient client;

        public WebClientWrapper()
        {
            client = new WebClient();
        }

        public void DownloadFile(Uri address, string filePath)
        {
            client.DownloadFile(address, filePath);
        }

        public void Dispose()
        {
            client?.Dispose();
        }

        public WebHeaderCollection Headers => client.Headers;
    }
}