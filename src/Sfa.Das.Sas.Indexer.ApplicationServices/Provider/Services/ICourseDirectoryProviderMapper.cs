namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface ICourseDirectoryProviderMapper
    {
        Core.Provider.Models.Provider.Provider Map(ApplicationServices.Provider.Models.CourseDirectory.Provider input);
    }
}