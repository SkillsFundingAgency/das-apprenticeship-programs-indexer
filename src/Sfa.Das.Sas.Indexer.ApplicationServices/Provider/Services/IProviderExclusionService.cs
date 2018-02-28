using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IProviderExclusionService
    {
        bool IsProviderInExclusionList(int ukprn);
    }
}