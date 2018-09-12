using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class ProviderFeedbackService : IGetProviderFeedback
    {
        private const string EmployerFeedbackPath = "employer-feedback";
        private readonly HttpClient _httpClient;

        public ProviderFeedbackService()
        {
            var baseUrl = ConfigurationManager.AppSettings["ProviderFeedbackUrl"];
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<EmployerFeedback>> GetProviderFeedbackData()
        {
            var response = await _httpClient.GetAsync(EmployerFeedbackPath);
            return await response.Content.ReadAsAsync<List<EmployerFeedback>>();
        }
    }
}
