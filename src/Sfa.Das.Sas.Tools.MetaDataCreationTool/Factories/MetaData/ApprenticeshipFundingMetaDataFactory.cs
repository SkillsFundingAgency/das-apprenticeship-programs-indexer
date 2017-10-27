using System;
using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
using Sfa.Das.Sas.Indexer.Core.Extensions;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Factories.MetaData
{
    public class ApprenticeshipFundingMetaDataFactory : IMetaDataFactory
    {
        public Type MetaDataType => typeof(ApprenticeshipFundingMetaData);
        public object Create(IReadOnlyList<string> values)
        {
            if (values == null || values.Count <= 13 || values[0].RemoveQuotationMark().Contains("ApprenticeshipType"))
            {
                return null;
            }
            
            return new ApprenticeshipFundingMetaData
            {
                ApprenticeshipType = values[0].RemoveQuotationMark(),
                ApprenticeshipCode = values[1].SafeParseInt(),
                ProgType = values[2].SafeParseInt(),
                PwayCode = values[3].SafeParseInt(),
                EffectiveFrom = values[5].SafeParseDate(),
                EffectiveTo = values[6].SafeParseDate(),
                ReservedValue1 = Convert.ToInt32(values[13].SafeParseDouble()),
                MaxEmployerLevyCap = Convert.ToInt32(values[16].SafeParseDouble())
            };
        }
    }
}
