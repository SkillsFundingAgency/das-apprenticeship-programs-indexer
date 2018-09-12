using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.ProviderFeedback;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;

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
            _logger.Debug("Starting to retreive Provider Feedback");

            var records = await _getProviderFeedback.GetProviderFeedbackData();
            _logger.Debug($"Retrieved {records.Count} Provider feedback results", new Dictionary<string, object> { { "TotalCount", records.Count } });

            return new ProviderFeedbackResult(records);
        }
    }
}
