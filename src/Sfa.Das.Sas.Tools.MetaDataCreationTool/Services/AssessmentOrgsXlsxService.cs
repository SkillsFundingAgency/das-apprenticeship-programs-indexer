using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using OfficeOpenXml;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Extensions;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class AssessmentOrgsXlsxService : IGetAssessmentOrgsData
    {
        private readonly IAssessmentOrgsExcelPackageService _assessmentOrgsExcelPackageService;
        private readonly IBlobStorageHelper _blobStorageHelper;
        private readonly ILog _log;

        public AssessmentOrgsXlsxService(
            IAssessmentOrgsExcelPackageService assessmentOrgsExcelPackageService,
            IBlobStorageHelper blobStorageHelper,
            ILog log)
        {
            _assessmentOrgsExcelPackageService = assessmentOrgsExcelPackageService;
            _blobStorageHelper = blobStorageHelper;
            _log = log;
        }

        public AssessmentOrganisationsDTO GetAssessmentOrganisationsData()
        {
            var container = _blobStorageHelper.GetAssessmentOrgsBlobContainer();
            var blockBlobs = _blobStorageHelper.GetAllBlockBlobs(container);

            var assessmentOrgsFile = blockBlobs.FirstOrDefault();

            try
            {
                _log.Debug("Downloading ROAAO");
                IEnumerable<Organisation> assessmentOrgs;
                IEnumerable<StandardOrganisationsData> standardOrganisationsData;
                using (var stream = new MemoryStream())
                {
                    assessmentOrgsFile?.DownloadToStream(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        assessmentOrgs = _assessmentOrgsExcelPackageService.GetAssessmentOrganisations(package).ToList();
                        standardOrganisationsData = _assessmentOrgsExcelPackageService.GetStandardOrganisationsData(package).ToList();

                        foreach (var org in assessmentOrgs)
                        {
                            var standard = standardOrganisationsData.FirstOrDefault(x => x.EpaOrganisationIdentifier == org.EpaOrganisationIdentifier);
                            org.Phone = standard?.Phone;
                            org.Email = standard?.Email;
                        }

                        foreach (var data in standardOrganisationsData)
                        {
                            var organisation = assessmentOrgs.FirstOrDefault(x => x.EpaOrganisationIdentifier == data.EpaOrganisationIdentifier);
                            data.Address = organisation?.Address;
                            data.EpaOrganisation = organisation?.EpaOrganisation;
                            data.WebsiteLink = organisation?.WebsiteLink;
                            data.OrganisationType = organisation?.OrganisationType;
                        }
                    }
                }

                return new AssessmentOrganisationsDTO
                {
                    Organisations = FilterOrganisations(assessmentOrgs),
                    StandardOrganisationsData = FilterStandardOrganisations(standardOrganisationsData)
                };
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Problem downloading ROATP from Blob Storage");

                return null;
            }
        }

        private List<Organisation> FilterOrganisations(IEnumerable<Organisation> organisationsData)
        {
            return organisationsData.Where(organisation => organisation.EpaOrganisationIdentifier != string.Empty && organisation.EpaOrganisation != string.Empty).DistinctBy(x => x.EpaOrganisationIdentifier).ToList();
        }

        private List<StandardOrganisationsData> FilterStandardOrganisations(IEnumerable<StandardOrganisationsData> standardOrganisationsData)
        {
            return
                standardOrganisationsData.Where(
                    organisationsData =>
                        organisationsData.EpaOrganisationIdentifier != string.Empty && organisationsData.StandardCode != string.Empty && organisationsData.EffectiveFrom != DateTime.MaxValue).ToList();
        }
    }
}
