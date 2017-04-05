using System.Collections.Generic;
using OfficeOpenXml;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public interface IAssessmentOrgsExcelPackageService
    {
        IEnumerable<Organisation> GetAssessmentOrganisations(ExcelPackage package);
        ExcelPackage GetExcelPackageFromFilePath(string filePath);
        IEnumerable<StandardOrganisationsData> GetStandardOrganisationsData(ExcelPackage package);
    }
}