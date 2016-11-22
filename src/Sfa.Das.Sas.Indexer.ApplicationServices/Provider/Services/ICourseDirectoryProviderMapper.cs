namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface ICourseDirectoryProviderMapper
    {
        Core.Models.Provider.Provider Map(ApplicationServices.Provider.Models.CourseDirectory.Provider input);
        Core.Models.Provider.Provider PopulateExistingProviderFromCD(ApplicationServices.Provider.Models.CourseDirectory.Provider input, Core.Models.Provider.Provider prov);
    }
}