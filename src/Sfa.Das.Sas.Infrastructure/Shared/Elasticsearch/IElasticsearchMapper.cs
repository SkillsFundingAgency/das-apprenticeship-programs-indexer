using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models;
    using Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models;

    public interface IElasticsearchMapper
    {
        StandardDocument CreateStandardDocument(StandardMetaData standard);

        StandardLars CreateLarsStandardDocument(LarsStandard standard);

        FrameworkDocument CreateFrameworkDocument(FrameworkMetaData frameworkMetaData);

        FrameworkLars CreateLarsFrameworkDocument(FrameworkMetaData frameworkMetaData);

        FundingDocument CreateFundingMetaDataDocument(FundingMetaData fundingMetaData);

        FrameworkAimDocument CreateFrameworkAimMetaDataDocument(FrameworkAimMetaData frameworkAimMetaData);

        LearningDeliveryDocument CreateLearningDeliveryMetaDataDocument(LearningDeliveryMetaData learningDeliveryMetaData);

        ApprenticeshipFundingDocument CreateApprenticeshipFundingDocument(ApprenticeshipFunding apprenticeshipFunding);

         ApprenticeshipComponentTypeDocument CreateApprenticeshipComponentTypeMetaDataDocument(ApprenticeshipComponentTypeMetaData apprenticeshipComponentTypeMetaData);

        int MapToLevelFromProgType(int level);

        StandardProvider CreateStandardProviderDocument(Provider provider, StandardInformation standardInformation, IEnumerable<DeliveryInformation> deliveryInformation);

        StandardProvider CreateStandardProviderDocument(Provider provider, StandardInformation standardInformation, DeliveryInformation deliveryInformation);

        FrameworkProvider CreateFrameworkProviderDocument(Provider provider, FrameworkInformation standardInformation, IEnumerable<DeliveryInformation> deliveryInformation);

        FrameworkProvider CreateFrameworkProviderDocument(Provider provider, FrameworkInformation standardInformation, DeliveryInformation deliveryInformation);

        ProviderDocument CreateProviderDocument(Provider provider);
    }
}