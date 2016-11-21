using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Services
{
    public interface IGetCourseDirectoryProviders
    {
        Task<IEnumerable<Indexer.Infrastructure.CourseDirectory.Models.Provider>> GetApprenticeshipProvidersAsync();
    }
}