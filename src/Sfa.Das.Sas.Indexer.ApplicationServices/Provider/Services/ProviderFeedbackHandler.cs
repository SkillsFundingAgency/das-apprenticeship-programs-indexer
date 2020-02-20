using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.ProviderFeedback;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public class ProviderFeedbackHandler : IAsyncRequestHandler<ProviderFeedbackRequest, ProviderFeedbackResult>
    {
        private readonly IGetProviderFeedback _getProviderFeedback;
        private readonly ILog _logger;

        public ProviderFeedbackHandler(IGetProviderFeedback getProviderFeedback, ILog logger)
        {
            _getProviderFeedback = getProviderFeedback;
            _logger = logger;
        }

        public async Task<ProviderFeedbackResult> Handle(ProviderFeedbackRequest message)
        {
            var records = new List<EmployerFeedbackSourceDto>();
            _logger.Debug("Starting to retreive Provider Feedback");

            try
            {
                records = await _getProviderFeedback.GetProviderFeedbackData();
                _logger.Debug($"Retrieved {records.Count} Provider feedback results", new Dictionary<string, object> { { "TotalCount", records.Count } });
                return new ProviderFeedbackResult(records);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unable to retrieve provider feedback results");
                throw ex;
            }
        }
    }
}
