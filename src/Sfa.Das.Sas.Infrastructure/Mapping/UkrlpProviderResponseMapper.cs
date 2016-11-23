////namespace Sfa.Das.Sas.Indexer.Infrastructure.Mapping
////{
////    using System.Linq;
////    using Sfa.Das.Sas.Indexer.Core.Models.Provider;
////    using UkrlpProviderContact = Services.Wrappers.ProviderContact;

////    public class UkrlpProviderResponseMapper : IUkrlpProviderResponseMapper
////    {
////        public Provider MapFromUkrlpProviderRecord(Services.Wrappers.Provider record)
//        {
//            var provider = new Provider
//            {
//                Ukprn = int.Parse(record.UnitedKingdomProviderReferenceNumber),
//                Name = record.ProviderName,
//                LegalName = record.ProviderName,
//                Addresses = record.ProviderContact?.Select(MapFromContactAddress).ToList()
//            };

//            return provider;
//        }

//        private ContactAddress MapFromContactAddress(UkrlpProviderContact contact)
//        {
//            return new ContactAddress
//            {
//                ContactType = contact?.ContactType,
//                PostCode = contact?.ContactAddress?.PostCode,
//                Primary = contact?.ContactAddress.Primary,
//                Secondary = contact?.ContactAddress.Secondary,
//                Street = contact?.ContactAddress?.StreetDescription,
//                Town = contact?.ContactAddress?.PostTown
//            };
//        }
//    }
//}
