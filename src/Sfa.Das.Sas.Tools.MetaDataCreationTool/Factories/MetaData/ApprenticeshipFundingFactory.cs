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
            return new ApprenticeshipFunding
            {
                ApprenticeshipType = values[0].RemoveQuotationMark(),
                ApprenticeshipCode = values[1],
                ProgType = values[2],
                PwayCode = values[3],
                ReservedValue1 = values[10],
                MaxEmployerLevyCap = values[12]
            };
        }
    }
}
