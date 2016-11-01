using LINQtoCSV;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider
{
    public class HeiProviderCsvRecord
    {
        [CsvColumn(Name = "UKPRN", FieldIndex = 1)]
        public string UkPrn { get; set; }
    }
}