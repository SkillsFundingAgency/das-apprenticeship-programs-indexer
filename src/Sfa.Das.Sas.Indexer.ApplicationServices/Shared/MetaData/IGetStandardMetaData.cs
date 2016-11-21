using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData
{
    public interface IGetStandardMetaData
    {
        IEnumerable<StandardMetaData> GetStandardsMetaData();
    }
}