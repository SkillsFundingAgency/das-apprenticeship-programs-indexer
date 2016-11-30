namespace Sfa.Das.Sas.Indexer.Infrastructure.Mapping
{
    using System;
    using System.Linq;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;
    using Provider = Sfa.Das.Sas.Indexer.Core.Models.Provider.Provider;

    public class UkrlpProviderMapper : IUkrlpProviderMapper
    {
        public Provider Map(ApplicationServices.Provider.Models.UkRlp.Provider ukrlpProvider)
        {
            ProviderContact contact = GetPrimaryOrLegalContact(ukrlpProvider);

            return new Provider
            {
                Ukprn = Convert.ToInt32(ukrlpProvider.UnitedKingdomProviderReferenceNumber),
                Name = ukrlpProvider.ProviderName,
                ContactDetails = new ContactInformation
                {
                    Email = contact.ContactEmail,
                    Phone = contact.ContactTelephone1,
                    Website = contact.ContactWebsiteAddress
                }
            };
        }

        public Core.Models.Provider.ContactAddress MapAddress(ProviderContact contact)
        {
            return new Core.Models.Provider.ContactAddress
            {
                ContactType = contact.ContactType == "L" ? "LEGAL" : "PRIMARY",
                Primary = contact.ContactAddress?.PAON,
                Secondary = contact.ContactAddress?.SAON,
                Street = contact.ContactAddress?.StreetDescription,
                Town = contact.ContactAddress?.PostTown,
                PostCode = contact.ContactAddress?.PostCode
            };
        }

        private static ProviderContact GetPrimaryOrLegalContact(ApplicationServices.Provider.Models.UkRlp.Provider ukrlpProvider)
        {
            return ukrlpProvider.ProviderContact.OrderByDescending(x => x.ContactType).FirstOrDefault();
        }
    }
}
