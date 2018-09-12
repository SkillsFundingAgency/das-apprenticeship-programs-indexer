using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData
{
    public interface IGetProviderFeedback
    {
        Task<List<EmployerFeedback>> GetProviderFeedbackData();
    }
}
