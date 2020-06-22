using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.CourseDirectory
{
    internal class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        private readonly string _apiKey;

        public ApiKeyServiceClientCredentials(string apiKey)
        {
            _apiKey = apiKey;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", _apiKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
