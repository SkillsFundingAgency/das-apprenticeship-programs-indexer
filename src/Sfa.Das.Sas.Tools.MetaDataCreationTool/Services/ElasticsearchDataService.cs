namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    using System.Collections.Generic;
    using Nest;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces;

    public sealed class ElasticsearchDataService : IElasticsearchDataService
    {
        private readonly IElasticsearchCustomClient _elasticsearchCustomClient;
        private readonly IIndexSettings<IMaintainLarsIndex> _larsSettings;

        public ElasticsearchDataService(
            IElasticsearchCustomClient elasticsearchCustomClient,
            IIndexSettings<IMaintainLarsIndex> larsSettings)
        {
            _elasticsearchCustomClient = elasticsearchCustomClient;
            _larsSettings = larsSettings;
        }

        public IEnumerable<LarsStandard> GetListOfCurrentStandards()
        {
            var size = GetLarsStandardsSize();

            var standards = _elasticsearchCustomClient
                .Search<LarsStandard>(s => s
                    .Index(_larsSettings.IndexesAlias)
                    .Type(Types.Parse("standardlars"))
                    .MatchAll()
                    .From(0)
                    .Size(size));

            return standards.Documents;
        }

        private int GetLarsStandardsSize()
        {
            return (int)_elasticsearchCustomClient
                .Search<LarsStandard>(s => s
                    .Index(_larsSettings.IndexesAlias)
                    .Type(Types.Parse("standardlars"))
                    .MatchAll()).HitsMetaData.Total;
        }

        public IEnumerable<FrameworkMetaData> GetListOfCurrentFrameworks()
        {
            var size = GetLarsFrameworksSize();

            var frameworks = _elasticsearchCustomClient
                .Search<FrameworkMetaData>(s => s
                    .Index(_larsSettings.IndexesAlias)
                    .Type(Types.Parse("frameworklars"))
                    .MatchAll()
                    .From(0)
                    .Size(size));

            return frameworks.Documents;
        }

        private int GetLarsFrameworksSize()
        {
            return (int)_elasticsearchCustomClient
                .Search<FrameworkMetaData>(s => s
                    .Index(_larsSettings.IndexesAlias)
                    .Type(Types.Parse("frameworklars"))
                    .MatchAll()).HitsMetaData.Total;
        }
    }
}