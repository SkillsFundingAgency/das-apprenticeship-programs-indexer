namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

    public interface IMaintainApprenticeshipIndex : IMaintainSearchIndexes
    {
        void IndexStandards(string indexName, IEnumerable<StandardMetaData> entries);

        void IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> entries);
    }
}