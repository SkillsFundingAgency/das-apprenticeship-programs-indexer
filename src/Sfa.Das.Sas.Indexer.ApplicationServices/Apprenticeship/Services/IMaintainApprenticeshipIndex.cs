namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

    public interface IMaintainApprenticeshipIndex : IMaintainSearchIndexes
    {
        Task IndexStandards(string indexName, IEnumerable<StandardMetaData> entries);

        Task IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> entries);
    }
}