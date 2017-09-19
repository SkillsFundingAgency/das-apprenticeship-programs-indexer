using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class AssessmentOrgsExcelPackageService : IAssessmentOrgsExcelPackageService
    {
        private readonly string organisationsWorksheetName = "Register - Organisations";
        private readonly string standardsWorkSheetName = "Register - Standards";

        public ExcelPackage GetExcelPackageFromFilePath(string filePath)
        {
            return new ExcelPackage(new FileInfo(filePath));
        }

        public IEnumerable<Organisation> GetAssessmentOrganisations(ExcelPackage package)
        {
            var organisationsWorkSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == organisationsWorksheetName);

            if (organisationsWorkSheet != null)
            {
                for (var i = organisationsWorkSheet.Dimension.Start.Row + 1; i <= organisationsWorkSheet.Dimension.End.Row; i++)
                {
                    yield return new Organisation
                    {
                        EpaOrganisationIdentifier = organisationsWorkSheet.Cells[i, 1].Value != null ? organisationsWorkSheet.Cells[i, 1].Value.ToString() : string.Empty,
                        EpaOrganisation = organisationsWorkSheet.Cells[i, 2].Value != null ? organisationsWorkSheet.Cells[i, 2].Value.ToString() : string.Empty,
                        OrganisationType = organisationsWorkSheet.Cells[i, 3].Value != null ? organisationsWorkSheet.Cells[i, 3].Value.ToString() : string.Empty,
                        WebsiteLink = organisationsWorkSheet.Cells[i, 4].Value != null ? organisationsWorkSheet.Cells[i, 4].Value.ToString() : string.Empty,
                        Address = new Address
                        {
                            Primary = organisationsWorkSheet.Cells[i, 5].Value != null ? organisationsWorkSheet.Cells[i, 5].Value.ToString() : string.Empty,
                            Secondary = organisationsWorkSheet.Cells[i, 6].Value != null ? organisationsWorkSheet.Cells[i, 6].Value.ToString() : string.Empty,
                            Street = organisationsWorkSheet.Cells[i, 7].Value != null ? organisationsWorkSheet.Cells[i, 7].Value.ToString() : string.Empty,
                            Town = organisationsWorkSheet.Cells[i, 8].Value != null ? organisationsWorkSheet.Cells[i, 8].Value.ToString() : string.Empty,
                            Postcode = organisationsWorkSheet.Cells[i, 9].Value != null ? organisationsWorkSheet.Cells[i, 9].Value.ToString() : string.Empty
                        }
                    };
                }
            }
        }

        public IEnumerable<StandardOrganisationsData> GetStandardOrganisationsData(ExcelPackage package)
        {
            var standardOrganisationWorkSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == standardsWorkSheetName);
            if (standardOrganisationWorkSheet != null)
            {
                for (var i = standardOrganisationWorkSheet.Dimension.Start.Row + 1; i <= standardOrganisationWorkSheet.Dimension.End.Row; i++)
                {
                    yield return new StandardOrganisationsData
                    {
                        EpaOrganisationIdentifier = standardOrganisationWorkSheet.Cells[i, 1].Value != null ? standardOrganisationWorkSheet.Cells[i, 1].Value.ToString() : string.Empty,
                        StandardCode = standardOrganisationWorkSheet.Cells[i, 3].Value != null ? standardOrganisationWorkSheet.Cells[i, 3].Value.ToString() : string.Empty,
                        EffectiveFrom = !string.IsNullOrWhiteSpace(standardOrganisationWorkSheet.Cells[i, 5].Value?.ToString()) ? Convert.ToDateTime(standardOrganisationWorkSheet.Cells[i, 5].Value.ToString()) : DateTime.MaxValue,
                        EffectiveTo = !string.IsNullOrWhiteSpace(standardOrganisationWorkSheet.Cells[i, 6].Value?.ToString()) ? Convert.ToDateTime(standardOrganisationWorkSheet.Cells[i, 6].Value.ToString()) : (DateTime?)null,
                        Phone = standardOrganisationWorkSheet.Cells[i, 8].Value != null ? standardOrganisationWorkSheet.Cells[i, 8].Value.ToString() : string.Empty,
                        Email = standardOrganisationWorkSheet.Cells[i, 9].Value != null ? standardOrganisationWorkSheet.Cells[i, 9].Value.ToString() : string.Empty
                    };
                }
            }
        }
    }
}
