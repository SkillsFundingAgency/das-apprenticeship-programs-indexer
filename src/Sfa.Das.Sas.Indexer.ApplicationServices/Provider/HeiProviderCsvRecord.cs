using LINQtoCSV;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider
{
    public class HeiProviderCsvRecord
    {
        [CsvColumn(Name = "UKPRN", FieldIndex = 1)]
        public string UkPrn { get; set; }

        [CsvColumn(Name = "ORGTYPE", FieldIndex = 2)]
        public string OrgType { get; set; }
    }
}