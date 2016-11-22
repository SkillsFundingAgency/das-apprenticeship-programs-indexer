using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData
{
    public interface IGetFrameworkMetaData
    {
        IEnumerable<FrameworkMetaData> GetAllFrameworks();
    }
}
