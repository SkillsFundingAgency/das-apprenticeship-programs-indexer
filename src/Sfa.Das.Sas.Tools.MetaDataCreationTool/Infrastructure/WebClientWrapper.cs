using System;
using System.Net;
using System.Text;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Infrastructure
{
    public class WebClientWrapper : IWebClient
    {
        private WebClient client;

        public WebClientWrapper()
        {
            client = new WebClient();
        }

        public void Dispose()
        {
            client?.Dispose();
        }

        public byte[] DownloadData(Uri address, string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", accessToken)));
                client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
            }

            return client.DownloadData(address);
        }
    }
}