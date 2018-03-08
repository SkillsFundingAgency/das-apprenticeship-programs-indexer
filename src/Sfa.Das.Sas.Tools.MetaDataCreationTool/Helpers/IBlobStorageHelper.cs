using Microsoft.WindowsAzure.Storage.Blob;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers
{
    public interface IBlobStorageHelper
    {
        CloudBlobContainer GetStandardsBlobContainer();
        CloudBlobContainer GetFrameworksBlobContainer();
        void UploadToContainer(CloudBlockBlob blockBlob, StandardRepositoryData standardRepositoryData);
    }
}