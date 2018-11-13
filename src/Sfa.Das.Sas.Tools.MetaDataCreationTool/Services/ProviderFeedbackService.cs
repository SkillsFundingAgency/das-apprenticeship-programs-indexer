using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class ProviderFeedbackService : IGetProviderFeedback
    {
        private const string EmployerFeedbackPath = "api/Feedback";
        private readonly HttpClient _httpClient;

        public ProviderFeedbackService(IAppServiceSettings appServiceSettings)
        {
            var baseUrl = appServiceSettings.ProviderFeedbackApiUri;
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<EmployerFeedbackSourceDto>> GetProviderFeedbackData()
        {
            var response = await _httpClient.GetAsync(EmployerFeedbackPath);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException("Error getting provider feedback data");
            }

            return await response.Content.ReadAsAsync<List<EmployerFeedbackSourceDto>>();
        }
    }
}
