using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces
{
    public interface IElasticsearchLarsDataService
    {
        IEnumerable<LarsStandard> GetListOfCurrentStandards();

        IEnumerable<FrameworkMetaData> GetListOfCurrentFrameworks();
    }
}