using System;
using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class StandardLars : LarsDocument
    {
        public StandardLars(string documentType)
            : base(documentType)
        {
        }

        [Keyword(NullValue = "null")]
        public int Id { get; set; }

        [Keyword(NullValue = "null")]
        public string Title { get; set; }

        [Keyword(NullValue = "null")]
        public int StandardSectorCode { get; set; }

        [Keyword(NullValue = "null")]
        public int NotionalEndLevel { get; set; }

        [Keyword(NullValue = "null")]
        public double SectorSubjectAreaTier1 { get; set; }

        [Keyword(NullValue = "null")]
        public double SectorSubjectAreaTier2 { get; set; }

        [Keyword(NullValue = "null")]
        public int Duration { get; set; }

        [Keyword(NullValue = "null")]
        public int FundingCap { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime? EffectiveFrom { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime? EffectiveTo { get; set; }

        public List<FundingPeriod> FundingPeriods { get; set; }

        public DateTime? LastDateForNewStarts { get; set; }

		public bool RegulatedStandard { get; set; }
    }
}
