using System;
using System.Net;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Infrastructure
{
    public interface IWebClient : IDisposable
    {
        void DownloadFile(Uri address, string filePath);
        byte[] DownloadData(Uri address);
        WebHeaderCollection Headers { get; }
    }
}
