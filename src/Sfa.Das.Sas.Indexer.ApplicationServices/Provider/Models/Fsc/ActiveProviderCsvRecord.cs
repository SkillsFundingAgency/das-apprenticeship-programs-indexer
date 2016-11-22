using LINQtoCSV;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Fsc
{
    public class ActiveProviderCsvRecord
    {
        [CsvColumn(Name = "UKPRN", FieldIndex = 1)]
        public int UkPrn { get; set; }
    }
}