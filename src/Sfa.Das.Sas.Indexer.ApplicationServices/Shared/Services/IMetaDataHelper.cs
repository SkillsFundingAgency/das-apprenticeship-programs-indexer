using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    public interface IMetaDataHelper
    {
        void UpdateMetadataRepository();

        IEnumerable<StandardMetaData> GetAllStandardsMetaData();

        IEnumerable<FrameworkMetaData> GetAllFrameworkMetaData();
    }
}