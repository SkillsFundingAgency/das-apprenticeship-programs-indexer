using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class ApprenticeshipFundingDocument : LarsDocument
    {
        public ApprenticeshipFundingDocument()
            : base(nameof(ApprenticeshipFundingDocument))
        {
        }

        [Keyword(NullValue = "null")]
        public string ApprenticeshipType { get; set; }

        [Keyword(NullValue = "null")]
        public int ApprenticeshipCode { get; set; }

        [Keyword(NullValue = "null")]
        public int ProgType { get; set; }

        [Keyword(NullValue = "null")]
        public int PwayCode { get; set; }

        [Keyword(NullValue = "null")]
        public int ReservedValue1 { get; set; }

        [Keyword(NullValue = "null")]
        public int MaxEmployerLevyCap { get; set; }
    }
}
