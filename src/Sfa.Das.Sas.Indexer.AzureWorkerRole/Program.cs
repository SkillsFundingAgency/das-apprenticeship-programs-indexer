using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.AzureWorkerRole
{
    public class Program
    {

        static void Main(string[] args)
        {
            var workerRole = new WorkerRole();
            workerRole.OnStart();
            workerRole.Run();
        }
    }
}
