using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers
{
    public class BlobStorageHelper : IBlobStorageHelper
    {
        private const string ContentType = "application/json";

        private readonly IAppServiceSettings _appServiceSettings;

        public BlobStorageHelper(IAppServiceSettings appServiceSettings)
        {
            _appServiceSettings = appServiceSettings;
        }

        public CloudBlobContainer GetStandardsBlobContainer()
        {
            return GetBlobContainer(_appServiceSettings.StandardBlobContainerReference);
        }

        public CloudBlobContainer GetFrameworksBlobContainer()
        {
            return GetBlobContainer(_appServiceSettings.FrameworkBlobContainerReference);
        }

        public CloudBlobContainer GetAssessmentOrgsBlobContainer()
        {
            return GetBlobContainer(_appServiceSettings.AssessmentOrgsBlobContainerReference);
        }

        public CloudBlobContainer GetFcsBlobcontainer()
        {
            return GetBlobContainer(_appServiceSettings.FcsBlobContainerReference);
        }

        public CloudBlobContainer GetRoatpBlobContainer()
        {
            return GetBlobContainer(_appServiceSettings.RoatpBlobContainerReference);
        }

        public CloudBlobContainer GetEmployerProvidersBlobcontainer()
        {
            return GetBlobContainer(_appServiceSettings.EmployerProvidersBlobContainerReference);
        }

        public void UploadToContainer(CloudBlockBlob blockBlob, StandardRepositoryData standardRepositoryData)
        {
            SetBlobProperties(blockBlob);

            using (var ms = new MemoryStream())
            {
                LoadStreamWithJson(ms, standardRepositoryData);
                blockBlob.UploadFromStream(ms);
            }
        }

        public IEnumerable<string> GetAllBlobs(CloudBlobContainer cloudBlobContainer)
        {
            var blobs = cloudBlobContainer.ListBlobs();

            return (from listBlobItem in blobs where listBlobItem.GetType() == typeof(CloudBlockBlob) select listBlobItem.Uri.ToString()).ToList();
        }

        public IEnumerable<CloudBlockBlob> GetAllBlockBlobs(CloudBlobContainer cloudBlobContainer)
        {
            var blobs = cloudBlobContainer.ListBlobs();

            return (from listBlobItem in blobs where listBlobItem.GetType() == typeof(CloudBlockBlob) select listBlobItem as CloudBlockBlob).ToList();
        }

        private void LoadStreamWithJson(MemoryStream ms, StandardRepositoryData standardRepositoryData)
        {
            var writer = new StreamWriter(ms);
            writer.Write(JsonConvert.SerializeObject(standardRepositoryData));
            writer.Flush();
            ms.Position = 0;
        }

        private void SetBlobProperties(CloudBlockBlob blockBlob)
        {
            blockBlob.Properties.ContentType = ContentType;
        }

        private CloudBlobContainer GetBlobContainer(string containerReference)
        {
            var storageAccount = CloudStorageAccount.Parse(_appServiceSettings.ConnectionString);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerReference);

            container.CreateIfNotExists();
            return container;
        }
    }
}
