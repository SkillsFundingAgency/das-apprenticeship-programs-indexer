using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.Core.Models
{
    public class SatisfactionRateProvider
    {
        public long Ukprn { get; set; }

        public double? FinalScore { get; set; }

        public int LearnerCount { get; set; }

        public int ResponseCount { get; set; }
    }
}
