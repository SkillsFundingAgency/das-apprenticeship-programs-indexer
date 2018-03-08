using Microsoft.WindowsAzure.Storage.Blob;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Helpers
{
    public interface IBlobStorageHelper
    {
        CloudBlobContainer GetStandardsBlobContainer();
        CloudBlobContainer GetFrameworksBlobContainer();
    }
}