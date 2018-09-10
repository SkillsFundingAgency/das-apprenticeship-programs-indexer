using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.EmployerProvider;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData
{
    public interface IEmployerProvidersService
    {
        ICollection<EmployerProviderCsvRecord> GetEmployerProviders();
    }
}