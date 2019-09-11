﻿using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public class TokenService : ITokenService
    {
        private readonly IAppServiceSettings _settings;
          public TokenService(IAppServiceSettings settings) 
        {
            _settings = settings;
        }

        public string GetToken()
        {
            if (_settings.RoatpApiAuthenticationApiBaseAddress.Contains("localhost"))
            {
                return string.Empty;
            }

            var tenantId = _settings.RoatpApiAuthenticationTenantId;
            var clientId = _settings.RoatpApiAuthenticationClientId;
            var appKey = _settings.RoatpApiAuthenticationClientSecret;
            var resourceId = _settings.RoatpApiAuthenticationResourceId;
            var instance = _settings.RoatpApiAuthenticationInstance;

            instance = instance.TrimEnd('/');

            var authority = $"{instance}/{tenantId}";
            var clientCredential = new ClientCredential(clientId, appKey);
            var context = new AuthenticationContext(authority, true);
            var result = context.AcquireTokenAsync(resourceId, clientCredential).Result;

            return result.AccessToken;

        }
    }
}