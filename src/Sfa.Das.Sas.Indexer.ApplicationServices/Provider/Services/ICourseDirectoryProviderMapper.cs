using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using ContactAddress = Sfa.Das.Sas.Indexer.Core.Models.Provider.ContactAddress;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface ICourseDirectoryProviderMapper
    {
        Core.Models.Provider.Provider Map(ApplicationServices.Provider.Models.CourseDirectory.Provider input);
        Core.Models.Provider.Provider PopulateExistingProviderFromCD(ApplicationServices.Provider.Models.CourseDirectory.Provider input, Core.Models.Provider.Provider prov);
        Core.Models.Provider.Provider CreateFrom(Models.UkRlp.Provider ukrlpProvider);
        ContactAddress MapAddress(ProviderContact contact);
    }
}