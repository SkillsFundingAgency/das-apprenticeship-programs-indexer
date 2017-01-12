namespace Sfa.Das.Sas.Indexer.Infrastructure.Mapping
{
    using System.Linq;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

    public class UkrlpProviderResponseMapper : IUkrlpProviderResponseMapper
    {
        public Provider MapFromUkrlpProviderRecord(ProviderRecordStructure record)
        {
            var aliases = record.ProviderAliases?.Where(pa => !string.IsNullOrEmpty(pa.ProviderAlias)).Select(pa => pa.ProviderAlias);

            var provider = new Provider
            {
                UnitedKingdomProviderReferenceNumber = record.UnitedKingdomProviderReferenceNumber,
                ProviderName = record.ProviderName,
                ProviderAliases = aliases.Any() ? aliases : null,
                ProviderContact = record.ProviderContact?.Select(MapFromContact)
            };
            
            return provider;
        }

        private ProviderContact MapFromContact(ProviderContactStructure contact)
        {
            return new ProviderContact
            {
                ContactType = contact?.ContactType,
                ContactAddress = MapFromContactAddress(contact?.ContactAddress),
                ContactEmail = contact?.ContactEmail,
                ContactWebsiteAddress = contact?.ContactWebsiteAddress,
                ContactTelephone1 = contact?.ContactTelephone1
            };
        }

        private ContactAddress MapFromContactAddress(BSaddressStructure contactAddress)
        {
            return new ContactAddress
            {
                PostCode = contactAddress?.PostCode,
                PAON = contactAddress?.PAON?.Description,
                SAON = contactAddress?.SAON?.Description,
                StreetDescription = contactAddress?.StreetDescription,
                PostTown = contactAddress?.PostTown
            };
        }
    }
}