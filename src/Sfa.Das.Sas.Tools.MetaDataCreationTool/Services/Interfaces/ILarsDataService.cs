using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;

    public interface ILarsDataService
    {
        LarsData GetDataFromLars();

        IEnumerable<LarsStandard> GetListOfCurrentStandards();

        IEnumerable<FrameworkMetaData> GetListOfCurrentFrameworks();
    }
}