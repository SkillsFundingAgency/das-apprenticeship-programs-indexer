using System;
using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Ukrlp.SoapApi.Types;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Mapping
{
    using Provider = Core.Models.Provider.Provider;

    public class UkrlpProviderMapper : IUkrlpProviderMapper
    {
        public Provider Map(Ukrlp.SoapApi.Types.Provider ukrlpProvider)
        {
            var contact = GetPrimaryOrLegalContact(ukrlpProvider);

            return new Provider
            {
                Ukprn = Convert.ToInt32(ukrlpProvider.UnitedKingdomProviderReferenceNumber),
                ContactDetails = new ContactInformation
                {
                    Email = contact.ContactEmail,
                    Phone = contact.ContactTelephone1,
                    Website = contact.ContactWebsiteAddress
                }
            };
        }

        public Core.Models.Provider.ContactAddress MapAddress(Ukrlp.SoapApi.Types.Contact contact)
        {
            return new Core.Models.Provider.ContactAddress
            {
                ContactType = contact.ContactType == "L" ? "LEGAL" : "PRIMARY",
                Primary = contact.Address?.PAON,
                Secondary = contact.Address?.SAON,
                Street = contact.Address?.StreetDescription,
                Town = contact.Address?.PostTown ?? contact.Address?.Town,
                PostCode = contact.Address?.PostCode
            };
        }

        private static Contact GetPrimaryOrLegalContact(Ukrlp.SoapApi.Types.Provider ukrlpProvider)
        {
            return ukrlpProvider.ProviderContact.OrderByDescending(x => x.ContactType).FirstOrDefault();
        }
    }
}
