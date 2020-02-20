using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class ApprenticeshipFundingDocument : LarsDocument
    {
        public ApprenticeshipFundingDocument()
            : base(nameof(ApprenticeshipFundingDocument))
        {
        }

        public string ApprenticeshipType { get; set; }

        public int ApprenticeshipCode { get; set; }

        public int ProgType { get; set; }

        public int PwayCode { get; set; }

        public int ReservedValue1 { get; set; }

        public int MaxEmployerLevyCap { get; set; }
    }
}
