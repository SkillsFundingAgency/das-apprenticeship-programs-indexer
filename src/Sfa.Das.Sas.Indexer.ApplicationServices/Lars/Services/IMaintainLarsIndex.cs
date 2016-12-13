namespace Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

    public interface IMaintainLarsIndex : IMaintainSearchIndexes
    {
        Task IndexStandards(string indexName, IEnumerable<LarsStandard> entries);

        Task IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> entries);

        Task IndexFundingMetadata(string indexName, IEnumerable<FundingMetaData> entries);

        Task IndexFrameworkAimMetaData(string indexName, IEnumerable<FrameworkAimMetaData> entries);

        Task IndexApprenticeshipComponentTypeMetaData(string indexName, IEnumerable<ApprenticeshipComponentTypeMetaData> entries);

        Task IndexLearningDeliveryMetaData(string indexName, IEnumerable<LearningDeliveryMetaData> entries);

        Task IndexApprenticeshipFundingDetails(string indexName, IEnumerable<ApprenticeshipFundingMetaData> entries);
    }
}