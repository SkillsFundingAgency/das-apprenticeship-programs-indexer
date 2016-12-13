namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Factories.MetaData
{
    using System;
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Extensions;

    public class ApprenticeshipFundingFactory : IMetaDataFactory
    {
        public Type MetaDataType => typeof(ApprenticeshipFunding);

        public object Create(IReadOnlyList<string> values)
        {
            if (values == null || values.Count <= 13 || values[0].RemoveQuotationMark().Contains("ApprenticeshipType"))
            {
                return null;
            }

            return new ApprenticeshipFunding
            {
                ApprenticeshipType = values[0].RemoveQuotationMark(),
                ApprenticeshipCode = values[1],
                ProgType = values[2].SafeParseInt(),
                PwayCode = values[3].SafeParseInt(),
                ReservedValue1 = values[10],
                MaxEmployerLevyCap = values[12]
            };
        }
    }
}
