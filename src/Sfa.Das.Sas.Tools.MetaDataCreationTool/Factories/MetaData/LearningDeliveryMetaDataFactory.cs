namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Factories.MetaData
{
    using System;
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Extensions;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

    public class LearningDeliveryMetaDataFactory : IMetaDataFactory
    {
        public Type MetaDataType => typeof(LearningDeliveryMetaData);

        public object Create(IReadOnlyList<string> values)
        {
            if (values == null || values.Count < 5 || values[0].RemoveQuotationMark().Contains("LearnAimRef"))
            {
                return null;
            }

            return new LearningDeliveryMetaData
            {
                LearnAimRef = values[0].RemoveQuotationMark(),
                EffectiveFrom = values[1].SafeParseDate() ?? DateTime.MinValue,
                EffectiveTo = values[2].SafeParseDate(),
                LearnAimRefTitle = values[3].RemoveQuotationMark(),
                LearnAimRefType = values[4].RemoveQuotationMark().SafeParseInt()
            };
        }
    }
}
