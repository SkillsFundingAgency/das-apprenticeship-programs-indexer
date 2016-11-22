using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IGetCourseDirectoryProviders
    {
        Task<IEnumerable<Provider.Models.CourseDirectory.Provider>> GetApprenticeshipProvidersAsync();
    }
}