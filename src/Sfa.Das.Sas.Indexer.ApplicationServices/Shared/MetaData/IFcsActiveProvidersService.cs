using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Fsc;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData
{
    public interface IFcsActiveProvidersService
    {
        ICollection<ActiveProviderCsvRecord> GetFcsData();
    }
}