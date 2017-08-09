namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Services;

    public class IndexerService<T> : IIndexerService<T>
    {
        private readonly IGenericIndexerHelper<T> _indexerHelper;

        private readonly IIndexSettings<T> _indexSettings;

        private readonly string _name;

        public ILog Log { get; set; }

        public IndexerService(IIndexSettings<T> indexSettings, IGenericIndexerHelper<T> indexerHelper, ILog log)
        {
            _indexSettings = indexSettings;
            _indexerHelper = indexerHelper;
            Log = log;
            _name = IndexerNameLookup.GetIndexTypeName(typeof(T));
        }

        public async Task CreateScheduledIndex(DateTime scheduledRefreshDateTime)
        {
            Log.Info($"Creating new scheduled {_name}");

            var stopwatch = Stopwatch.StartNew();

            var newIndexName = IndexerHelper.GetIndexNameAndDateExtension(scheduledRefreshDateTime, _indexSettings.IndexesAlias);
            var indexProperlyCreated = _indexerHelper.CreateIndex(newIndexName);

            if (!indexProperlyCreated)
            {
                throw new Exception($"{_name} index not created properly, exiting...");
            }

            Log.Info($"Indexing documents for {_name}.");

            var indexHasBeenCreated = await _indexerHelper.IndexEntries(newIndexName).ConfigureAwait(false);

            if (indexHasBeenCreated)
            {
                _indexerHelper.ChangeUnderlyingIndexForAlias(newIndexName);

                Log.Debug("Swap completed...");

                _indexerHelper.DeleteOldIndexes(scheduledRefreshDateTime);
            }

            stopwatch.Stop();
            var properties = new Dictionary<string, object> {{"Alias", _indexSettings.IndexesAlias}, {"ExecutionTime", stopwatch.ElapsedMilliseconds}, {"IndexCorrectlyCreated", indexHasBeenCreated}};
            Log.Debug($"Created {_name}", properties);
            Log.Info($"{_name}ing complete.");
        }
    }
}