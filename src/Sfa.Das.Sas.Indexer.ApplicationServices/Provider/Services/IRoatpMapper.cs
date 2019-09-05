using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IRoatpMapper
    {
        List<RoatpProviderResult> Map(List<RoatpResult> roatpResult);
    }
}
