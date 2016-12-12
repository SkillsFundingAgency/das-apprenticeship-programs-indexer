using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IGetCourseDirectoryProviders
    {
        Task<CourseDirectoryResult> GetApprenticeshipProvidersAsync();
    }
}