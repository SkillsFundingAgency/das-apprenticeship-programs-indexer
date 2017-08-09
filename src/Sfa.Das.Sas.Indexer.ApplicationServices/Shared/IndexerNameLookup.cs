using System;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared
{
    public static class IndexerNameLookup
    {
        public static string GetIndexTypeName(Type type)
        {
            if (type == typeof(IMaintainProviderIndex))
            {
                return "Provider Index";
            }

            if (type == typeof(IMaintainApprenticeshipIndex))
            {
                return "Apprenticeship Index";
            }

            if (type == typeof(IMaintainLarsIndex))
            {
                return "Lars Index";
            }

            return "AssessmentOrgs Index";
        }
    }
}