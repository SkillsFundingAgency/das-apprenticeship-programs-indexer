﻿namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public class TokenService : ITokenService
    {
       // private readonly IAppServiceSettings _settings;
       //// private readonly IHostingEnvironment _hostingEnvironment;
       // public TokenService(IAppServiceSettings settings) //, IHostingEnvironment hostingEnvironment)
       // {
       //     //_hostingEnvironment = hostingEnvironment;
       //     _settings = settings;
       // }

        public string GetToken()
        {
            //if (_hostingEnvironment.IsDevelopment())
            //{
                return string.Empty;
            //}

            //var tenantId = _settings.RoatpApiAuthenticationTenantId;
            //var clientId = _settings.RoatpApiAuthenticationClientId;
            //var appKey = _settings.RoatpApiAuthenticationClientSecret;
            //var resourceId = _settings.RoatpApiAuthenticationResourceId;
            //var instance = _settings.RoatpApiAuthenticationInstance;

            //var authority = $"{instance}/{tenantId}";
            //var clientCredential = new ClientCredential(clientId, appKey);
            //var context = new AuthenticationContext(authority, true);
            //var result = context.AcquireTokenAsync(resourceId, clientCredential).Result;

            //return result.AccessToken;
        }
    }
}