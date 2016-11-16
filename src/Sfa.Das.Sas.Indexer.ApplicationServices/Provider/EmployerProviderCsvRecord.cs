namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider
{
    using LINQtoCSV;

    public class EmployerProviderCsvRecord
    {
        [CsvColumn(Name = "UKPRN", FieldIndex = 1)]
        public int UkPrn { get; set; }

        [CsvColumn(Name = "OrganisationName", FieldIndex = 2)]
        public string OrganisationName { get; set; }
    }
}