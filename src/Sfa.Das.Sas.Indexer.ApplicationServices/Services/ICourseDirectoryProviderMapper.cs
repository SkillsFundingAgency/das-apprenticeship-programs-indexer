using Sfa.Das.Sas.Indexer.Core.Models.Provider;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    public interface ICourseDirectoryProviderMapper
    {
        Provider Map(Models.Provider input);
    }
}