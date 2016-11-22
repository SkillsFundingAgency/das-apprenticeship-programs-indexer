using System.IO;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility
{
    public interface IUnzipStream
    {
        string ExtractFileFromStream(Stream stream, string filePath);

        string ExtractFileFromStream(Stream stream, string filePath, bool leaveStreamOpen);
    }
}