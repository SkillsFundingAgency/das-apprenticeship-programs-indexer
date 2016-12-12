using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models
{
    public class FrameworkMetaDataResult
    {
        public IEnumerable<FrameworkMetaData> Frameworks { get; set; }
    }
}