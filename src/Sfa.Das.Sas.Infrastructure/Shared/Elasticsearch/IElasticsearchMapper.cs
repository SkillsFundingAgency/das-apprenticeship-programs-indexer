using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;
    using Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models;

    public interface IElasticsearchMapper
    {
        ApprenticeshipDocument CreateStandardDocument(StandardMetaData standard);

        StandardLars CreateLarsStandardDocument(LarsStandard standard);

        ApprenticeshipDocument CreateFrameworkDocument(FrameworkMetaData frameworkMetaData);

        FrameworkLars CreateLarsFrameworkDocument(FrameworkMetaData frameworkMetaData);

        FundingDocument CreateFundingMetaDataDocument(FundingMetaData fundingMetaData);

        FrameworkAimDocument CreateFrameworkAimMetaDataDocument(FrameworkAimMetaData frameworkAimMetaData);

        LearningDeliveryDocument CreateLearningDeliveryMetaDataDocument(LearningDeliveryMetaData learningDeliveryMetaData);

        ApprenticeshipFundingDocument CreateApprenticeshipFundingDocument(ApprenticeshipFundingMetaData apprenticeshipFunding);

        ApprenticeshipComponentTypeDocument CreateApprenticeshipComponentTypeMetaDataDocument(ApprenticeshipComponentTypeMetaData apprenticeshipComponentTypeMetaData);

        AssessmentOrgsDocument CreateOrganisationDocument(Organisation organisation);

        AssessmentOrgsDocument CreateStandardOrganisationDocument(StandardOrganisationsData standardOrganisationsData);

        int MapToLevelFromProgType(int level);

        StandardProvider CreateStandardProviderDocument(Provider provider, StandardInformation standardInformation, IEnumerable<DeliveryInformation> deliveryInformation);

        StandardProvider CreateStandardProviderDocument(Provider provider, StandardInformation standardInformation, DeliveryInformation deliveryInformation);

        FrameworkProvider CreateFrameworkProviderDocument(Provider provider, FrameworkInformation standardInformation, IEnumerable<DeliveryInformation> deliveryInformation);

        FrameworkProvider CreateFrameworkProviderDocument(Provider provider, FrameworkInformation standardInformation, DeliveryInformation deliveryInformation);

        ProviderApiDocument CreateProviderApiDocument(Provider provider);
    }
}