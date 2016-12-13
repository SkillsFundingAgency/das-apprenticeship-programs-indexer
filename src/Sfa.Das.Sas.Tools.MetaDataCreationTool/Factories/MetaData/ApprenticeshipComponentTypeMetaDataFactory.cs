namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Factories.MetaData
{
    using System;
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Extensions;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

    public class ApprenticeshipComponentTypeMetaDataFactory : IMetaDataFactory
    {
        public Type MetaDataType => typeof(ApprenticeshipComponentTypeMetaData);

        public object Create(IReadOnlyList<string> values)
        {
            if (values == null || values.Count < 5 || values[0].RemoveQuotationMark().Contains("ApprenticeshipComponentType"))
            {
                return null;
            }

            return new ApprenticeshipComponentTypeMetaData
            {
                ApprenticeshipComponentType = values[0].RemoveQuotationMark().SafeParseInt(),
                ApprenticeshipComponentTypeDesc = values[1].RemoveQuotationMark(),
                ApprenticeshipComponentTypeDesc2 = values[2].RemoveQuotationMark(),
                EffectiveFrom = values[3].SafeParseDate() ?? DateTime.MinValue,
                EffectiveTo = values[4].SafeParseDate()
            };
        }
    }
}
