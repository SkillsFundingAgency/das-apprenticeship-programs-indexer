using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Fsc;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class FcsActiveProvidersService : IFcsActiveProvidersService
    {
        private readonly IBlobStorageHelper _blobStorageHelper;
        private readonly IConvertFromCsv _convertFromCsv;
        private readonly ILog _log;

        public FcsActiveProvidersService(IBlobStorageHelper blobStorageHelper, IConvertFromCsv convertFromCsv, ILog log)
        {
            _blobStorageHelper = blobStorageHelper;
            _convertFromCsv = convertFromCsv;
            _log = log;
        }

        public ICollection<ActiveProviderCsvRecord> GetFcsData()
        {
            var loadProvidersFromBlobStorage = GetFcsContent();
            var records = _convertFromCsv.CsvTo<ActiveProviderCsvRecord>(loadProvidersFromBlobStorage);
            return records;
        }

        private string GetFcsContent()
        {
            var container = _blobStorageHelper.GetFcsBlobcontainer();
            var blockBlobs = _blobStorageHelper.GetAllBlockBlobs(container);

            var fcsFile = blockBlobs.FirstOrDefault();

            try
            {
                if (fcsFile == null)
                {
                    return null;
                }

                _log.Debug("Downloading FCS");
                return fcsFile.DownloadText();
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Problem downloading FCS file from Blob Storage");

                return null;
            }
        }
    }
}
