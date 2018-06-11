namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Metrics
{
    public struct ExecutionTimerResult<T>
    {
        public T Result { get; set; }

        public double ElaspedMilliseconds { get; set; }
    }
}
