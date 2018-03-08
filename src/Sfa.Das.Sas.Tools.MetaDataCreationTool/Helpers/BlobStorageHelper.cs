using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers
{
    public class BlobStorageHelper : IBlobStorageHelper
    {
        private readonly IAppServiceSettings _appServiceSettings;

        public BlobStorageHelper(IAppServiceSettings appServiceSettings)
        {
            _appServiceSettings = appServiceSettings;
        }

        public CloudBlobContainer GetStandardsBlobContainer()
        {
            return CreateBlobContainer(_appServiceSettings.StandardBlobContainerReference);
        }

        public CloudBlobContainer GetFrameworksBlobContainer()
        {
            return CreateBlobContainer(_appServiceSettings.FrameworkBlobContainerReference);
        }

        private CloudBlobContainer CreateBlobContainer(string containerReference)
        {
            var storageAccount = CloudStorageAccount.Parse(_appServiceSettings.ConnectionString);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerReference);

            container.CreateIfNotExists();
            return container;
        }
    }
}
