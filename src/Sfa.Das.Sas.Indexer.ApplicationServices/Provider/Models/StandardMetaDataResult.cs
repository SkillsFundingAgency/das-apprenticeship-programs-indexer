using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models
{
    public class StandardMetaDataResult
    {
        public IEnumerable<StandardMetaData> Standards { get; set; }
    }
}