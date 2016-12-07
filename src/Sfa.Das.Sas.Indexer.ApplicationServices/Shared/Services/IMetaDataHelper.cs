﻿namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

    public interface IMetaDataHelper
    {
        void UpdateMetadataRepository();

        IEnumerable<StandardMetaData> GetAllStandardsMetaData();

        IEnumerable<FrameworkMetaData> GetAllFrameworkMetaData();

        LarsData GetAllApprenticeshipLarsMetaData();
    }
}