namespace Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

    public interface IMaintainLarsIndex : IMaintainSearchIndexes
    {
        void IndexStandards(string indexName, IEnumerable<LarsStandard> entries);

        void IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> entries);

        void IndexFundingMetadata(string indexName, IEnumerable<FundingMetaData> entries);

        void IndexFrameworkAimMetaData(string indexName, IEnumerable<FrameworkAimMetaData> entries);

        void IndexApprenticeshipComponentTypeMetaData(string indexName, IEnumerable<ApprenticeshipComponentTypeMetaData> entries);

        void IndexLearningDeliveryMetaData(string indexName, IEnumerable<LearningDeliveryMetaData> entries);

        void IndexApprenticeshipFundingDetails(string indexName, IEnumerable<ApprenticeshipFundingMetaData> entries);
    }
}