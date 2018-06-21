namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Factories.MetaData
{
    using System;
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Extensions;

    public class StandardMetaDataFactory : IMetaDataFactory
    {
        public Type MetaDataType => typeof(LarsStandard);

        public object Create(IReadOnlyList<string> values)
        {
            if (values == null)
            {
                return null;
            }

            if (values.Count < 11)
            {
                return null;
            }

            var standardid = GetStandardId(values);

            if (standardid < 0)
            {
                return null;
            }

            return new LarsStandard
            {
                Id = standardid,
                Title = values[StandardCsvCols.Title].RemoveQuotationMark(),
                StandardSectorCode = values[StandardCsvCols.StandardSectorCode].RemoveQuotationMark().SafeParseInt(),
                NotionalEndLevel = values[StandardCsvCols.NotionalEndLevel].RemoveQuotationMark().SafeParseInt(),
                SectorSubjectAreaTier1 = values[StandardCsvCols.SectorSubjectAreaTier1].RemoveQuotationMark().SafeParseDouble(),
                SectorSubjectAreaTier2 = values[StandardCsvCols.SectorSubjectAreaTier2].RemoveQuotationMark().SafeParseDouble(),
                EffectiveFrom = values[StandardCsvCols.EffectiveFrom].SafeParseDate(),
                EffectiveTo = values[StandardCsvCols.EffectiveTo].SafeParseDate(),
                LastDateForNewStarts = values[StandardCsvCols.LastDateForNewStarts].SafeParseDate()
            };
        }

        private static int GetStandardId(IReadOnlyList<string> values)
        {
            if (values == null || values.Count < 7)
            {
                return -1;
            }

            return values[0].RemoveQuotationMark().SafeParseInt();
        }
    }
}
