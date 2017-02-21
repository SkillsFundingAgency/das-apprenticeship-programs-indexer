﻿using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Mapping
{
    public class UkrlpProviderResponseMapper : IUkrlpProviderResponseMapper
    {
        public ApplicationServices.Provider.Models.UkRlp.Provider MapFromUkrlpProviderRecord(ProviderRecordStructure record)
        {
            var aliases = record.ProviderAliases?.Where(pa => !string.IsNullOrEmpty(pa.ProviderAlias)).Select(pa => pa.ProviderAlias);

            var provider = new ApplicationServices.Provider.Models.UkRlp.Provider
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
                PostTown = contactAddress?.PostTown,
                Town = GetItem(contactAddress, ItemsChoiceType.Town),
                AdministrativeArea = GetItem(contactAddress, ItemsChoiceType.AdministrativeArea),
                Locality = contactAddress?.Locality,
                UniquePropertyReferenceNumber = contactAddress?.UniquePropertyReferenceNumber,
                UniqueStreetReferenceNumber = contactAddress?.UniqueStreetReferenceNumber
            };
        }

        private string GetItem(BSaddressStructure contactAddress, ItemsChoiceType choice)
        {
            if (contactAddress.ItemsElementName != null && contactAddress.ItemsElementName.Any(x => x == choice))
            {
                return contactAddress?.Items[System.Array.IndexOf(contactAddress?.ItemsElementName, choice)];
            }

            return null;
        }
    }
}