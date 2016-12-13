using MediatR;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    public sealed class StandardsMetaDataHelper : IRequestHandler<StandardMetaDataRequest, StandardMetaDataResult>
    {
        private readonly IMetaDataHelper _metaDataHelper;

        public StandardsMetaDataHelper(IMetaDataHelper metaDataHelper)
        {
            _metaDataHelper = metaDataHelper;
        }

        public StandardMetaDataResult Handle(StandardMetaDataRequest message)
        {
            return _metaDataHelper.GetAllStandardsMetaData();
        }
    }
}