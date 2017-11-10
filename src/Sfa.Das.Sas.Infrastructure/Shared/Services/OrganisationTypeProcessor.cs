using System;
using System.Configuration;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services
{
    public class OrganisationTypeProcessor : IOrganisationTypeProcessor
    {
        private readonly ILog _log;

        public OrganisationTypeProcessor(ILog log)
        {
            _log = log;
        }

        public string ProcessOrganisationType(string organisationType)
        {
            var validOrganisationTypes = ConfigurationManager.AppSettings["ValidOrganisationTypes"].Split('|');
            foreach (var orgType in validOrganisationTypes)
            {
                if (string.Equals(organisationType.Trim(), orgType.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return orgType;
                }
            }

            _log.Warn($"The organisation type '{organisationType}' could not be matched to an existing type, so has been set to 'Other'");
            return "Other";
        }
    }
}