using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.EmployerProvider;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Fsc;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class EmployerProvidersService : IEmployerProvidersService
    {
        private readonly IBlobStorageHelper _blobStorageHelper;
        private readonly IConvertFromCsv _convertFromCsv;
        private readonly ILog _log;

        public EmployerProvidersService(IBlobStorageHelper blobStorageHelper, IConvertFromCsv convertFromCsv, ILog log)
        {
            _blobStorageHelper = blobStorageHelper;
            _convertFromCsv = convertFromCsv;
            _log = log;
        }

        public ICollection<EmployerProviderCsvRecord> GetEmployerProviders()
        {
            var loadProvidersFromVsts = GetEmployerProvidersContent();
            var records = _convertFromCsv.CsvTo<EmployerProviderCsvRecord>(loadProvidersFromVsts);
            return records;
        }

        private string GetEmployerProvidersContent()
        {
            var container = _blobStorageHelper.GetEmployerProvidersBlobcontainer();
            var blockBlobs = _blobStorageHelper.GetAllBlockBlobs(container);

            var fcsFile = blockBlobs.FirstOrDefault();

            try
            {
                if (fcsFile == null)
                {
                    return null;
                }

                _log.Debug("Downloading Employer Providers");
                return fcsFile.DownloadText();
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Problem downloading Employer Providers file from Blob Storage");

                return null;
            }
        }
    }
}
