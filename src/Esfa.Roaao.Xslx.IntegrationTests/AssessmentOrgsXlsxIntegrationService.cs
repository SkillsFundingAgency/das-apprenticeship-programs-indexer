using System.IO;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Services;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Infrastructure;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services;
using SFA.DAS.NLog.Logger;

namespace Esfa.Roaao.Xslx.IntegrationTests
{
    internal class AssessmentOrgsXlsxIntegrationService : AssessmentOrgsXlsxService, IGetAssessmentOrgsData
	{
        public AssessmentOrgsXlsxIntegrationService(IAssessmentOrgsExcelPackageService assessmentOrgsExcelPackageService, 
													IWebClient webClient, 
													IAppServiceSettings appServiceSettings, 
													ILog log) : base(assessmentOrgsExcelPackageService, webClient, appServiceSettings, log)
        {
        }

		public override Stream GetFileStream()
		{
			return File.Open(_appServiceSettings.VstsAssessmentOrgsUrl, FileMode.OpenOrCreate);
		}
    }
}
