using System;
using System.Collections.Generic;

namespace Sfa.Das.Sas.Core.Configuration
{
    public interface IConfigurationSettings
    {
        string ApprenticeshipIndexAlias { get; }

        string ProviderIndexAlias { get; }

        string BuildId { get; }

        IEnumerable<Uri> ElasticServerUrls { get; }

        Uri SurveyUrl { get; }

        bool UseSecureCookies { get; }
    }
}