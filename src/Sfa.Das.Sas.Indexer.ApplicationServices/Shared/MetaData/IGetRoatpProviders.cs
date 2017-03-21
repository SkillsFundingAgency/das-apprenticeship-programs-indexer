using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData
{
    public interface IGetRoatpProviders
    {
        List<RoatpProviderResult> GetRoatpData();
    }
}
