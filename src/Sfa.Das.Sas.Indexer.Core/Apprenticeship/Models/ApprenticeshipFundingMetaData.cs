using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models
{
    public class ApprenticeshipFundingMetaData
    {
        public string ApprenticeshipType { get; set; }

        public int ApprenticeshipCode { get; set; }

        public int ProgType { get; set; }

        public int PwayCode { get; set; }

        public int MaxEmployerLevyCap { get; set; }

        public int ReservedValue1 { get; set; }
    }
}
