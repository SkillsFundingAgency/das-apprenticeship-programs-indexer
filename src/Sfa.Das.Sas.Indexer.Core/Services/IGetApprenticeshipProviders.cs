namespace Sfa.Das.Sas.Indexer.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGetApprenticeshipProviders
    {
        ICollection<string> GetEmployerProviders();

        ICollection<string> GetHeiProviders();
    }
}