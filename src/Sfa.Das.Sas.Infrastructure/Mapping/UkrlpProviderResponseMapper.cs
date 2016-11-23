namespace Sfa.Das.Sas.Indexer.Infrastructure.Mapping
{
    using System.Linq;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;
    using Ukrlp;

    public class UkrlpProviderResponseMapper : IUkrlpProviderResponseMapper
    {
        public Provider MapFromUkrlpProviderRecord(ProviderRecordStructure record)
        {
            var provider = new Provider
            {
                Ukprn = int.Parse(record.UnitedKingdomProviderReferenceNumber),
                Name = record.ProviderName,
                LegalName = record.ProviderName,
                Addresses = record.ProviderContact?.Select(MapFromContactAddress).ToList()
            };

            return provider;
        }

        private ContactAddress MapFromContactAddress(ProviderContactStructure contact)
        {
            return new ContactAddress
            {
                ContactType = contact?.ContactType,
                PostCode = contact?.ContactAddress?.PostCode,
                Primary = contact?.ContactAddress?.PAON?.Description,
                Secondary = contact?.ContactAddress?.SAON?.Description,
                Street = contact?.ContactAddress?.StreetDescription,
                Town = contact?.ContactAddress?.PostTown
            };
        }
    }
}
