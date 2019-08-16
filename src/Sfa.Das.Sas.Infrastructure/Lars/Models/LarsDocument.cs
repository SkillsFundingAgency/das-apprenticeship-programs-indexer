using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class LarsDocument
    {
        public LarsDocument(string documentType)
        {
            DocumentType = documentType;
        }

        public string DocumentType { get; }
    }
}
