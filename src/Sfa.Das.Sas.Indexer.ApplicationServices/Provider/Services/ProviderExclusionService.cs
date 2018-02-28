using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public class ProviderExclusionService : IProviderExclusionService
    {
        private readonly IIndexSettings<IMaintainProviderIndex> _settings;
        private List<int> _exclusionList;
        public ProviderExclusionService(IIndexSettings<IMaintainProviderIndex> settings)
        {
            _settings = settings;
        }

        public bool IsProviderInExclusionList(int ukprn)
        {
            var providersToExclude = GetProviderExclusionList();
            return providersToExclude.Count != 0 && providersToExclude.Contains(ukprn);
        }

        private List<int> GetProviderExclusionList()
        {
            if (_exclusionList != null)
            {
                return _exclusionList;
            }

            var providerExclusionList = _settings.ProviderExclusionList.Split(',');
            var providersToExclude = new List<int>();
            foreach (var providerExclusion in providerExclusionList)
            {
                int res;
                if (int.TryParse(providerExclusion.Trim(), out res) && providerExclusion.Trim().Length == 8)
                {
                    providersToExclude.Add(res);
                }
            }

            _exclusionList = providersToExclude;
            return _exclusionList;
        }
    }

    
}
