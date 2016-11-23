namespace Sfa.Das.Sas.Indexer.Infrastructure.Mapping
{
    using System.Linq;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

    public class UkrlpProviderWrapperMapper : IUkrlpProviderWrapperMapper
    {
        public Provider MapFromUkrlpProviderRecord(ProviderRecordStructure record)
        {
            var provider = new Provider
            {
                UnitedKingdomProviderReferenceNumber = record.UnitedKingdomProviderReferenceNumber,
                ProviderName = record.ProviderName,
                ProviderContact = record.ProviderContact?.Select(MapFromContactAddress)
            };

            return provider;
        }

        private ProviderContact MapFromContactAddress(ProviderContactStructure contact)
        {
            return new ProviderContact
            {
                ContactType = contact?.ContactType,
                ContactAddress = MapFromContactAddress2(contact?.ContactAddress),
                ContactEmail = contact?.ContactEmail,
                ContactWebsiteAddress = contact?.ContactWebsiteAddress,
                ContactTelephone1 = contact?.ContactTelephone1
            };
        }

        private ContactAddress MapFromContactAddress2(BSaddressStructure contactAddress)
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