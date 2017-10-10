using System;
using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class StandardLars
    {
        [Keyword(NullValue = "null")]
        public int Id { get; set; }

        [Keyword(NullValue = "null")]
        public string Title { get; set; }

        [Keyword(NullValue = "null")]
        public int NotionalEndLevel { get; set; }

        [Keyword(NullValue = "null")]
        public string StandardUrl { get; set; }

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
    }
}
