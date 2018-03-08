using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Indexer.Core.Models;
    using Interfaces;
    using Models.Git;
    using Newtonsoft.Json;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

    public class VstsService : IVstsService
    {
        private readonly IAppServiceSettings _appServiceSettings;

        private readonly IGitDynamicModelGenerator _gitDynamicModelGenerator;

        private readonly IJsonMetaDataConvert _jsonMetaDataConvert;

        private readonly IVstsClient _vstsClient;
        private readonly IBlobStorageHelper _blobStorageHelper;

        private readonly ILog _logger;

        public VstsService(
            IAppServiceSettings appServiceSettings,
            IGitDynamicModelGenerator gitDynamicModelGenerator,
            IJsonMetaDataConvert jsonMetaDataConvert,
            IVstsClient vstsClient,
            IBlobStorageHelper blobStorageHelper,
            ILog logger)
        {
            _appServiceSettings = appServiceSettings;
            _gitDynamicModelGenerator = gitDynamicModelGenerator;
            _jsonMetaDataConvert = jsonMetaDataConvert;
            _vstsClient = vstsClient;
            _blobStorageHelper = blobStorageHelper;
            _logger = logger;
        }

        public IEnumerable<string> GetExistingStandardIds()
        {
            var container = _blobStorageHelper.GetStandardsBlobContainer();
            var blobs = GetAllBlobs(container);

            var result = blobs?.Select(GetIdFromPath) ?? new List<string>();
            _logger.Info($"Got {result.Count()} current meta data files Git Repository.");

            return result;
        }

        public IEnumerable<StandardMetaData> GetStandards()
        {
            try
            {
                var container = _blobStorageHelper.GetStandardsBlobContainer();
                var standardsDictionary = GetAllFileContents(container);
                return _jsonMetaDataConvert.DeserializeObject<StandardMetaData>(standardsDictionary);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting framework meta data");
            }

            return new List<StandardMetaData>();
        }

        public IEnumerable<FrameworkRepositoryMetaData> GetFrameworks()
        {
            try
            {
                var container = _blobStorageHelper.GetFrameworksBlobContainer();
                var frameworkDictionary = GetAllFileContents(container);
                return _jsonMetaDataConvert.DeserializeObject<FrameworkRepositoryMetaData>(frameworkDictionary);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting framework meta data");
            }

            return new List<FrameworkRepositoryMetaData>();
        }

        public void PushStandards(List<StandardRepositoryData> items)
        {
            foreach (var standardRepositoryData in items)
            {
                var container = _blobStorageHelper.GetStandardsBlobContainer();

                var blockBlob = container.GetBlockBlobReference($"{standardRepositoryData.Id}-{standardRepositoryData.Title.Replace("/", "_").Replace(" ", string.Empty)}.json");

                blockBlob.UploadText(JsonConvert.SerializeObject(standardRepositoryData));
            }
        }

        public IDictionary<string, string> GetAllFileContents(CloudBlobContainer container)
        {
            var blobs = GetAllBlockBlobs(container);

            var standardsAsJson = new Dictionary<string, string>();
            foreach (var cloudBlockBlob in blobs)
            {
                using (var memoryStream = new MemoryStream())
                {
                    cloudBlockBlob.DownloadToStream(memoryStream);
                    standardsAsJson.Add(cloudBlockBlob.Uri.ToString(), System.Text.Encoding.UTF8.GetString(memoryStream.ToArray()));
                }
            }

            return standardsAsJson;
        }

        // Helpers
        private IEnumerable<string> GetAllBlobs(CloudBlobContainer cloudBlobContainer)
        {
            var blobs = cloudBlobContainer.ListBlobs();

            return (from listBlobItem in blobs where listBlobItem.GetType() == typeof(CloudBlockBlob) select listBlobItem.Uri.ToString()).ToList();
        }

        private IEnumerable<CloudBlockBlob> GetAllBlockBlobs(CloudBlobContainer cloudBlobContainer)
        {
            var blobs = cloudBlobContainer.ListBlobs();

            return (from listBlobItem in blobs where listBlobItem.GetType() == typeof(CloudBlockBlob) select listBlobItem as CloudBlockBlob).ToList();
        }

        private string GetIdFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var i = path.LastIndexOf("/", StringComparison.Ordinal);
            if (i < 0)
            {
                return string.Empty;
            }

            var str = path.Substring(i);
            var standardId = str.Split('-')[0].Replace("/", string.Empty);
            int outData;
            if (int.TryParse(standardId, out outData))
            {
                return standardId;
            }

            return string.Empty;
        }
    }
}