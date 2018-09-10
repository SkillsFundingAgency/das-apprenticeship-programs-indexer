using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Blob;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers
{
    public interface IBlobStorageHelper
    {
        CloudBlobContainer GetStandardsBlobContainer();
        CloudBlobContainer GetFrameworksBlobContainer();
        CloudBlobContainer GetAssessmentOrgsBlobContainer();
        CloudBlobContainer GetFcsBlobcontainer();
        CloudBlobContainer GetRoatpBlobContainer();
        CloudBlobContainer GetEmployerProvidersBlobcontainer();
        void UploadToContainer(CloudBlockBlob blockBlob, StandardRepositoryData standardRepositoryData);
        IEnumerable<string> GetAllBlobs(CloudBlobContainer cloudBlobContainer);
        IEnumerable<CloudBlockBlob> GetAllBlockBlobs(CloudBlobContainer cloudBlobContainer);
    }
}