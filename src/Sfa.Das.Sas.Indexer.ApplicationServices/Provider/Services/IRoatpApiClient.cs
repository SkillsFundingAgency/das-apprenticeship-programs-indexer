using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IRoatpApiClient
    {
        Task<List<RoatpResult>> GetRoatpSummary();
    }
}
