using System;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Infrastructure
{
    public interface IWebClient : IDisposable
    {
        byte[] DownloadData(Uri address, string accessToken);
    }
}
