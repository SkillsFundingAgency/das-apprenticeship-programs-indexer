﻿using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models
{
    public class EmployerSatisfactionRateResult
    {
        public IEnumerable<SatisfactionRateProvider> Rates { get; set; }
    }
}