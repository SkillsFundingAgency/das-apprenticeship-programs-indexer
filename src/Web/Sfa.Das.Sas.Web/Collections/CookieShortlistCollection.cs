﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.ApplicationServices.Settings;
using Sfa.Das.Sas.Web.Factories.Interfaces;

namespace Sfa.Das.Sas.Web.Collections
{
    public class CookieShortlistCollection : IShortlistCollection<int>
    {
        private readonly ICookieSettings _settings;
        private readonly IHttpCookieFactory _cookieFactory;

        public CookieShortlistCollection(ICookieSettings settings, IHttpCookieFactory cookieFactory)
        {
            _settings = settings;
            _cookieFactory = cookieFactory;
        }

        public ICollection<ShortlistedApprenticeship> GetAllItems(string listName)
        {
            var listCookie = GetListCookie(listName);
            return GetListItems(listCookie);
        }

        public void AddItem(string listName, ShortlistedApprenticeship item)
        {
            var listCookie = GetListCookie(listName);

            var listItems = GetListItems(listCookie);

            if (item.ProvidersUkrpnAndLocation == null)
            {
                item.ProvidersUkrpnAndLocation = new List<ShortlistedProvider>();
            }

            if (!listItems.Any(x => x.ApprenticeshipId.Equals(item.ApprenticeshipId)))
            {
                listItems.Add(item);
            }
            else
            {
                foreach (var shortlistedApprenticeship in listItems)
                {
                    if (shortlistedApprenticeship.ApprenticeshipId.Equals(item.ApprenticeshipId))
                    {
                        foreach (var provider in item.ProvidersUkrpnAndLocation)
                        {
                            if (!shortlistedApprenticeship.ProvidersUkrpnAndLocation
                                .Any(x => x.Ukprn == provider.Ukprn && x.LocationId == provider.LocationId))
                            {
                                shortlistedApprenticeship.ProvidersUkrpnAndLocation.Add(provider);
                            }
                        }
                    }
                }
            }

            var listString = CovertItemListToString(listItems);

            AddListToResponse(listName, listString);
        }

        public void RemoveApprenticeship(string listName, int item)
        {
            var listCookie = GetListCookie(listName);

            var listItems = GetListItems(listCookie);

            foreach (var shortlistedApprenticeship in listItems.Where(shortlistedApprenticeship => shortlistedApprenticeship.ApprenticeshipId.Equals(item)))
            {
                listItems.Remove(shortlistedApprenticeship);
                break;
            }

            if (!listItems.Any())
            {
                RemoveList(listName);
                return;
            }

            var listString = CovertItemListToString(listItems);
            AddListToResponse(listName, listString);
        }

        public void RemoveProvider(string listName, int apprenticeship, ShortlistedProvider item)
        {
            var listCookie = GetListCookie(listName);

            var listItems = GetListItems(listCookie);

            foreach (var shortlistedApprenticeship in listItems)
            {
                if (shortlistedApprenticeship.ApprenticeshipId != apprenticeship)
                {
                    continue;
                }

                foreach (var provider in shortlistedApprenticeship.ProvidersUkrpnAndLocation
                    .Where(provider => provider.Ukprn == item.Ukprn &&
                                       provider.LocationId == item.LocationId))
                {
                    shortlistedApprenticeship.ProvidersUkrpnAndLocation.Remove(provider);
                    break;
                }
            }

            if (!listItems.Any())
            {
                RemoveList(listName);
                return;
            }

            var listString = CovertItemListToString(listItems);
            AddListToResponse(listName, listString);
        }

        public void RemoveList(string listName)
        {
            var requestCookies = _cookieFactory.GetRequestCookies();

            if (requestCookies[listName] == null)
            {
                return;
            }

            var cookie = new HttpCookie(listName)
            {
                Expires = DateTime.Now.AddDays(-1),
                Domain = _settings.CookieDomain,
                HttpOnly = false,
                Secure = _settings.UseSecureCookies
            };

            var responseCookies = _cookieFactory.GetResponseCookies();

            responseCookies.Add(cookie);
        }

        private static List<ShortlistedApprenticeship> GetListItems(HttpCookie cookie)
        {
            var listItems = new List<ShortlistedApprenticeship>();

            if (string.IsNullOrEmpty(cookie.Value))
            {
                return listItems;
            }

            var shortlistedApprenticeships = SplitCookie(cookie);

            foreach (var shortlistedApprenticeship in shortlistedApprenticeships)
            {
                var splittedApprenticeships = SplitShortlistedApprenticeship(shortlistedApprenticeship).ToList();

                var providers = splittedApprenticeships.Count > 1 ?
                    SplitProviderUkprnsAndLocations(splittedApprenticeships.ElementAt(1)) :
                    new List<string>();

                var providersList = providers.Select(SplitShortllistedProvider).ToList();

                int apprenticeshipId;
                int.TryParse(splittedApprenticeships.ElementAt(0), out apprenticeshipId);

                listItems.Add(new ShortlistedApprenticeship
                {
                    ApprenticeshipId = apprenticeshipId,
                    ProvidersUkrpnAndLocation = providersList
                });
            }

            return listItems;
        }

        private static ShortlistedProvider SplitShortllistedProvider(string provider)
        {
            var prov = provider.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            if (prov.Count() > 1)
            {
                return new ShortlistedProvider
                {
                    Ukprn = int.Parse(prov[0]),
                    LocationId = int.Parse(prov[1])
                };
            }

            return new ShortlistedProvider();
        }

        private static IEnumerable<string> SplitCookie(HttpCookie cookie)
        {
            return cookie.Value.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static IEnumerable<string> SplitShortlistedApprenticeship(string shortlistedApprenticeship)
        {
            return shortlistedApprenticeship.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static IEnumerable<string> SplitProviderUkprnsAndLocations(string s)
        {
            return s.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string CovertItemListToString(List<ShortlistedApprenticeship> listItems)
        {
            if (!listItems.Any())
            {
                return string.Empty;
            }

            var listShortlistedProviders = new List<string>();
            foreach (var shortlistedApprenticeship in listItems)
            {
                var text = new StringBuilder();
                var count = 0;
                foreach (var formattedProvider in shortlistedApprenticeship.ProvidersUkrpnAndLocation.Select(provider => $"{provider.Ukprn}-{provider.LocationId}"))
                {
                    text.Append(count == 0 ? formattedProvider : string.Concat("|", formattedProvider));
                    count++;
                }

                listShortlistedProviders.Add($"{shortlistedApprenticeship.ApprenticeshipId}={text}");
            }

            var listString = listShortlistedProviders.Select(x => x)
                .Aggregate((x1, x2) => x1 + "&" + x2);

            return listString;
        }

        private void AddListToResponse(string listName, string listString)
        {
            var responseCookie = new HttpCookie(listName)
            {
                Value = listString,
                HttpOnly = false,
                Expires = DateTime.Now.AddYears(1),
                Secure = _settings.UseSecureCookies,
                Domain = _settings.CookieDomain
            };

            var responseCookies = _cookieFactory.GetResponseCookies();

            responseCookies.Add(responseCookie);
        }

        private HttpCookie GetListCookie(string listName)
        {
            var requestCookies = _cookieFactory.GetRequestCookies();

            var listCookie = requestCookies[listName] ?? new HttpCookie(listName);
            return listCookie;
        }
    }
}