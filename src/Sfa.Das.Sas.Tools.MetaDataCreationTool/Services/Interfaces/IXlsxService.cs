﻿using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces
{
    public interface IXlsxService
    {
        AssessmentOrganisationsDTO GetAssessmentOrganisationsData();
    }
}