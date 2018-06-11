using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Metrics;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Models;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public sealed class LarsDataService : ILarsDataService
    {
        private const int MinimumValidFrameworkCode = 400;

        private readonly IReadMetaDataFromCsv _csvService;
        private readonly IUnzipStream _fileExtractor;
        private readonly IAngleSharpService _angleSharpService;
        private readonly IHttpGetFile _httpGetFile;
        private readonly ILog _logger;
        private readonly IAppServiceSettings _appServiceSettings;

        public LarsDataService(
            IAppServiceSettings appServiceSettings,
            IReadMetaDataFromCsv csvService,
            IHttpGetFile httpGetFile,
            IUnzipStream fileExtractor,
            IAngleSharpService angleSharpService,
            ILog logger)
        {
            _appServiceSettings = appServiceSettings;
            _csvService = csvService;
            _httpGetFile = httpGetFile;
            _fileExtractor = fileExtractor;
            _angleSharpService = angleSharpService;
            _logger = logger;
        }

        public LarsData GetDataFromLars()
        {
            var zipFilePath = GetZipFilePath();
            _logger.Debug($"Zip file path: {zipFilePath}");

            var zipStream = GetZipStream(zipFilePath);
            _logger.Debug("Zip file downloaded");

            var larsData = GetZipFileData(zipStream);

            return larsData;
        }

        public IEnumerable<LarsStandard> GetListOfCurrentStandards()
        {
            var zipFilePath = GetZipFilePath();
            _logger.Debug($"Zip file path: {zipFilePath}");

            var zipStream = GetZipStream(zipFilePath);

            _logger.Debug("Zip file downloaded");

            var fileContent = _fileExtractor.ExtractFileFromStream(zipStream, _appServiceSettings.CsvFileNameStandards);
            _logger.Debug($"Extracted content. Length: {fileContent.Length}");

            var standards = _csvService.ReadFromString<LarsStandard>(fileContent);
            _logger.Debug($"Read: {standards.Count} standards from file.");

            var csvData = GetLarsCsvData(zipFilePath);

            zipStream.Close();

            var larsMetaData = GetLarsMetaData(csvData);

            AddDurationAndFundingToStandards(standards, larsMetaData);

            return standards;
        }

        public IEnumerable<FrameworkMetaData> GetListOfCurrentFrameworks()
        {
            var zipFilePath = GetZipFilePath();
            _logger.Debug($"Zip file path: {zipFilePath}");

            var csvData = GetLarsCsvData(zipFilePath);

            var larsMetaData = GetLarsMetaData(csvData);

            AddQualificationsToFrameworks(larsMetaData);
            AddDurationAndFundingToFrameworks(larsMetaData);

            return larsMetaData.Frameworks;
        }

        private static void DetermineQualificationFundingStatus(IEnumerable<FrameworkQualification> qualifications, IEnumerable<FundingMetaData> fundings)
        {
            foreach (var qualification in qualifications)
            {
                // Get fundings associated with a given qualification (Learn Aim Ref)
                var qualificationFundings = fundings.Where(x => x.LearnAimRef.Equals(qualification.LearnAimRef, StringComparison.OrdinalIgnoreCase)).ToList();

                // Get qualification fundings that are associated with apprenticeship funding
                var apprenticeshipFundings = qualificationFundings.Where(x => !string.IsNullOrEmpty(x.FundingCategory) &&
                                                                              x.FundingCategory.Equals("APP_ACT_COST", StringComparison.CurrentCultureIgnoreCase)).ToList();

                // Get apprenticeship fundings that are still active (not out of date)
                var activeFundings = apprenticeshipFundings.Where(x => (x.EffectiveTo.HasValue && x.EffectiveTo.Value >= DateTime.Now) || !x.EffectiveTo.HasValue).ToList();

                // If no fundings are found we assume qualification is unfunded
                if (!activeFundings.Any())
                {
                    qualification.IsFunded = false;
                    continue;
                }

                // Only if the funding Rate weight is greater than zero do we class the qualification as funded
                qualification.IsFunded = activeFundings.Any(x => x.RateWeighted > 0);
            }
        }

        private static void DetermineQualificationFundingStatus(IEnumerable<FrameworkQualification> qualifications, ICollection<FundingMetaData> fundings)
        {
            foreach (var qualification in qualifications)
            {
                // Get fundings associated with a given qualification (Learn Aim Ref)
                var qualificationFundings = fundings.Where(x => x.LearnAimRef.Equals(qualification.LearnAimRef, StringComparison.OrdinalIgnoreCase)).ToList();

                // Get qualification fundings that are associated with apprenticeship funding
                var apprenticeshipFundings = qualificationFundings.Where(x => !string.IsNullOrEmpty(x.FundingCategory) &&
                                                                              x.FundingCategory.Equals("APP_ACT_COST", StringComparison.CurrentCultureIgnoreCase)).ToList();

                // Get apprenticeship fundings that are still active (not out of date)
                var activeFundings = apprenticeshipFundings.Where(x => (x.EffectiveTo.HasValue && x.EffectiveTo.Value >= DateTime.Now) || !x.EffectiveTo.HasValue).ToList();

                // If no fundings are found we assume qualification is unfunded
                if (!activeFundings.Any())
                {
                    qualification.IsFunded = false;
                    continue;
                }

                // Only if the funding Rate weight is greater than zero do we class the qualification as funded
                qualification.IsFunded = activeFundings.Any(x => x.RateWeighted > 0);
            }
        }

        private static CategorisedQualifications GetCategorisedQualifications(ICollection<FrameworkQualification> qualifications)
        {
            var qualificationSet = default(CategorisedQualifications);

            qualificationSet.Combined = qualifications.Where(x => x.CompetenceType == 3)
                .Select(x => x.Title)
                .GroupBy(x => x.ToUpperInvariant())
                .Select(group => group.First())
                .ToList();

            qualificationSet.Competency = qualifications.Where(x => x.CompetenceType == 1)
                .Select(x => x.Title)
                .GroupBy(x => x.ToUpperInvariant())
                .Select(group => group.First())
                .Except(qualificationSet.Combined)
                .ToList();

            qualificationSet.Knowledge = qualifications.Where(x => x.CompetenceType == 2)
                .Select(x => x.Title)
                .GroupBy(x => x.ToUpperInvariant())
                .Select(group => group.First())
                .Except(qualificationSet.Combined)
                .ToList();

            return qualificationSet;
        }

        private ICollection<FrameworkMetaData> FilterFrameworks(IEnumerable<FrameworkMetaData> frameworks)
        {
            var progTypeList = new[] { 2, 3, 20, 21, 22, 23 };

            var list = frameworks.Where(s => s.FworkCode >= MinimumValidFrameworkCode)
                .Where(s => s.PwayCode > 0)
                .Where(s => !s.EffectiveFrom.Equals(DateTime.MinValue))
                .Where(s => !s.EffectiveTo.HasValue || s.EffectiveTo >= DateTime.MinValue)
                .Where(s => progTypeList.Contains(s.ProgType))
                .ToList();

                _logger.Warn($"Adding expired frameworks {string.Join(", ", list.Where(s => IsSpecialFramework($"{s.FworkCode}-{s.ProgType}-{s.PwayCode}")).Select(s => $"{s.FworkCode}-{s.ProgType}-{s.PwayCode}"))}");

            return list;
        }

        private bool IsSpecialFramework(string frameworkId)
        {
            return _appServiceSettings.FrameworksExpiredRequired.Any(specialFrameworkId => specialFrameworkId == frameworkId);
        }

        private LarsData GetZipFileData(Stream zipStream)
        {
            var larsData = GetLarsData(zipStream);

            AddQualificationsToFrameworks(larsData);

            return larsData;
        }

        private LarsData GetLarsData(Stream zipStream)
        {
            var standards = GetLarsStandards(zipStream);

            var frameworks = GetLarsFrameworks(zipStream);

            var frameworkAimMetadata = GetLarsFrameworkAimData(zipStream);

            var apprenticeshipComponentTypeMetadata = GetLarsApprenticeshipComponentTypeMetadata(zipStream);

            var learningDeliveryMetadata = GetLarsLearningDeliveryMetaData(zipStream);

            var fundingMetadata = GetLarsFundingMetaData(zipStream);

            var apprenticeshipFunding = GetApprenticeshipFundingMetaData(zipStream);

            CloseStream(zipStream);

            AddDurationAndFundingToStandards(standards, apprenticeshipFunding);
            AddDurationAndFundingToFrameworks(frameworks, apprenticeshipFunding);

            return new LarsData
            {
                Standards = standards,
                Frameworks = frameworks,
                FrameworkAimMetaData = frameworkAimMetadata,
                ApprenticeshipComponentTypeMetaData = apprenticeshipComponentTypeMetadata,
                LearningDeliveryMetaData = learningDeliveryMetadata,
                FundingMetaData = fundingMetadata,
                ApprenticeshipFunding = apprenticeshipFunding
            };
        }

        private ICollection<LarsStandard> GetLarsStandards(Stream zipStream)
        {
            var fileContent = _fileExtractor.ExtractFileFromStream(zipStream, _appServiceSettings.CsvFileNameStandards, true);
            _logger.Debug($"Extracted content. Length: {fileContent.Length}");

            var standards = _csvService.ReadFromString<LarsStandard>(fileContent);
            _logger.Debug($"Read: {standards.Count} standards from file.");
            return standards;
        }

        private ICollection<FrameworkMetaData> GetLarsFrameworks(Stream zipStream)
        {
            var fileContent = _fileExtractor.ExtractFileFromStream(zipStream, _appServiceSettings.CsvFileNameFrameworks, true);

            var frameworksMetaData = _csvService.ReadFromString<FrameworkMetaData>(fileContent);
            _logger.Debug($"Read {frameworksMetaData.Count} frameworks from file.");

            var frameworks = FilterFrameworks(frameworksMetaData);
            _logger.Debug($"There are {frameworks.Count} frameworks after filtering.");

            return frameworks;
        }

        private ICollection<FrameworkAimMetaData> GetLarsFrameworkAimData(Stream zipStream)
        {
            var fileContent = _fileExtractor.ExtractFileFromStream(zipStream, _appServiceSettings.CsvFileNameFrameworksAim, true);

            var frameworkAimMetaData = _csvService.ReadFromString<FrameworkAimMetaData>(fileContent);

            return frameworkAimMetaData;
        }

        private ICollection<ApprenticeshipComponentTypeMetaData> GetLarsApprenticeshipComponentTypeMetadata(Stream zipStream)
        {
            var fileContent = _fileExtractor.ExtractFileFromStream(zipStream, _appServiceSettings.CsvFileNameApprenticeshipComponentType, true);

            var apprenticeshipComponentTypeMetadata = _csvService.ReadFromString<ApprenticeshipComponentTypeMetaData>(fileContent);

            return apprenticeshipComponentTypeMetadata;
        }

        private ICollection<LearningDeliveryMetaData> GetLarsLearningDeliveryMetaData(Stream zipStream)
        {
            var fileContent = _fileExtractor.ExtractFileFromStream(zipStream, _appServiceSettings.CsvFileNameLearningDelivery, true);

            var learningDeliveryMetaData = _csvService.ReadFromString<LearningDeliveryMetaData>(fileContent);

            return learningDeliveryMetaData;
        }

        private ICollection<FundingMetaData> GetLarsFundingMetaData(Stream zipStream)
        {
            var fileContent = _fileExtractor.ExtractFileFromStream(zipStream, _appServiceSettings.CsvFileNameFunding, true);

            var fundingMetaData = _csvService.ReadFromString<FundingMetaData>(fileContent);

            return fundingMetaData;
        }

        private ICollection<ApprenticeshipFundingMetaData> GetApprenticeshipFundingMetaData(Stream zipStream)
        {
            var fileContent = _fileExtractor.ExtractFileFromStream(zipStream, _appServiceSettings.CsvFileNameApprenticeshipFunding, true);

            var apprenticeshipFunding = _csvService.ReadFromString<ApprenticeshipFundingMetaData>(fileContent);

            return apprenticeshipFunding;
        }

        private void AddQualificationsToFrameworks(LarsData larsData)
        {
            foreach (var framework in larsData.Frameworks)
            {
                var frameworkAims = larsData.FrameworkAimMetaData.Where(x => x.FworkCode.Equals(framework.FworkCode) &&
                                                                      x.ProgType.Equals(framework.ProgType) &&
                                                                      x.PwayCode.Equals(framework.PwayCode)).ToList();

                frameworkAims = frameworkAims.Where(x => x.EffectiveTo >= DateTime.Now || x.EffectiveTo == null || IsSpecialFramework($"{x.FworkCode}-{x.ProgType}-{x.PwayCode}")).ToList();

                var qualifications =
                    (from aim in frameworkAims
                     join comp in larsData.ApprenticeshipComponentTypeMetaData on aim.ApprenticeshipComponentType equals comp.ApprenticeshipComponentType
                     join ld in larsData.LearningDeliveryMetaData on aim.LearnAimRef equals ld.LearnAimRef
                     select new FrameworkQualification
                     {
                         Title = ld.LearnAimRefTitle.Replace("(QCF)", string.Empty).Trim(),
                         LearnAimRef = aim.LearnAimRef,
                         CompetenceType = comp.ApprenticeshipComponentType,
                         CompetenceDescription = comp.ApprenticeshipComponentTypeDesc
                     }).ToList();

                // Determine if the qualifications are funded or not by the apprenticeship scheme
                DetermineQualificationFundingStatus(qualifications, larsData.FundingMetaData);

                // Only show funded qualifications
                qualifications = qualifications.Where(x => x.IsFunded).ToList();

                var categorisedQualifications = GetCategorisedQualifications(qualifications);

                framework.CompetencyQualification = categorisedQualifications.Competency;
                framework.KnowledgeQualification = categorisedQualifications.Knowledge;
                framework.CombinedQualification = categorisedQualifications.Combined;
            }
        }

        private void CloseStream(Stream zipStream)
        {
            zipStream.Close();
        }

        private Stream GetZipStream(string zipFilePath)
        {
            var timer = ExecutionTimer.GetTiming(() => _httpGetFile.GetFile(zipFilePath));
            LogExecutionTime(zipFilePath, timer.ElaspedMilliseconds);
            return timer.Result;
        }

        private void AddQualificationsToFrameworks(LarsMetaData metaData)
        {
            foreach (var framework in metaData.Frameworks)
            {
                var frameworkAims = metaData.FrameworkAims.Where(x => x.FworkCode.Equals(framework.FworkCode) &&
                                                                      x.ProgType.Equals(framework.ProgType) &&
                                                                      x.PwayCode.Equals(framework.PwayCode)).ToList();

                frameworkAims = frameworkAims.Where(x => x.EffectiveTo >= DateTime.Now || x.EffectiveTo == null || IsSpecialFramework($"{x.FworkCode}-{x.ProgType}-{x.PwayCode}")).ToList();

                var qualifications =
                    (from aim in frameworkAims
                     join comp in metaData.FrameworkContentTypes on aim.ApprenticeshipComponentType equals comp.ApprenticeshipComponentType
                     join ld in metaData.LearningDeliveries on aim.LearnAimRef equals ld.LearnAimRef
                     select new FrameworkQualification
                     {
                         Title = ld.LearnAimRefTitle.Replace("(QCF)", string.Empty).Trim(),
                         LearnAimRef = aim.LearnAimRef,
                         CompetenceType = comp.ApprenticeshipComponentType,
                         CompetenceDescription = comp.ApprenticeshipComponentTypeDesc
                     }).ToList();

                    // Determine if the qualifications are funded or not by the apprenticeship scheme
                DetermineQualificationFundingStatus(qualifications, metaData.Fundings);

                // Only show funded qualifications
                qualifications = qualifications.Where(x => x.IsFunded).ToList();

                var categorisedQualifications = GetCategorisedQualifications(qualifications);

                framework.CompetencyQualification = categorisedQualifications.Competency;
                framework.KnowledgeQualification = categorisedQualifications.Knowledge;
                framework.CombinedQualification = categorisedQualifications.Combined;
            }
        }

        private void AddDurationAndFundingToFrameworks(LarsMetaData metaData)
        {
            foreach (var framework in metaData.Frameworks)
            {
                var fw =
                    metaData.ApprenticeshipFundings.FirstOrDefault(fwk =>
                        fwk.ApprenticeshipType.ToLower() == "fwk" &&
                        fwk.ApprenticeshipCode == framework.FworkCode &&
                        fwk.ProgType == framework.ProgType &&
                        fwk.PwayCode == framework.PwayCode &&
                        fwk.EffectiveFrom.HasValue &&
                        fwk.EffectiveFrom.Value.Date <= DateTime.UtcNow.Date &&
                        (!fwk.EffectiveTo.HasValue || fwk.EffectiveTo.Value.Date >= DateTime.UtcNow.Date));

                if (fw == null)
                {
                    continue;
                }

                framework.Duration = fw.ReservedValue1;
                framework.FundingCap = fw.MaxEmployerLevyCap;
            }
        }

        private void AddDurationAndFundingToStandards(ICollection<LarsStandard> standards, LarsMetaData metaData)
        {
            foreach (var std in standards)
            {
                var s =
                    metaData.ApprenticeshipFundings.FirstOrDefault(stdrd =>
                        stdrd.ApprenticeshipType.ToLower() == "std" &&
                        stdrd.ApprenticeshipCode == std.Id && stdrd.EffectiveFrom.HasValue &&
                        stdrd.EffectiveFrom.Value.Date <= DateTime.UtcNow.Date &&
                        (!stdrd.EffectiveTo.HasValue || stdrd.EffectiveTo.Value.Date >= DateTime.UtcNow.Date));

                if (s == null)
                {
                    continue;
                }

                std.Duration = s.ReservedValue1;
                std.FundingCap = s.MaxEmployerLevyCap;
            }
        }

        private void AddDurationAndFundingToFrameworks(ICollection<FrameworkMetaData> frameworks, ICollection<ApprenticeshipFundingMetaData> metaData)
        {
            foreach (var framework in frameworks)
            {
                var fw =
                    metaData.Where(fwk =>
                        fwk.ApprenticeshipType.ToLower() == "fwk" &&
                        fwk.ApprenticeshipCode == framework.FworkCode &&
                        fwk.ProgType == framework.ProgType &&
                        fwk.PwayCode == framework.PwayCode &&
                        fwk.EffectiveFrom.HasValue &&
                        fwk.EffectiveFrom.Value.Date <= DateTime.UtcNow.Date &&
                            (!fwk.EffectiveTo.HasValue
                                || fwk.EffectiveTo.Value.Date >= DateTime.UtcNow.Date
                                || IsSpecialFramework($"{fwk.ApprenticeshipCode}-{fwk.ProgType}-{fwk.PwayCode}")))
                        .OrderByDescending(x => x.EffectiveFrom)
                        .FirstOrDefault();

                if (fw == null)
                {
                    continue;
                }

                framework.Duration = fw.ReservedValue1;
                framework.FundingCap = fw.MaxEmployerLevyCap;
            }
        }

        private void AddDurationAndFundingToStandards(ICollection<LarsStandard> standards, ICollection<ApprenticeshipFundingMetaData> metaData)
        {
            foreach (var std in standards)
            {
                var s =
                    metaData.FirstOrDefault(stdrd =>
                        stdrd.ApprenticeshipType.ToLower() == "std" &&
                        stdrd.ApprenticeshipCode == std.Id &&
                        stdrd.EffectiveFrom.HasValue &&
                        stdrd.EffectiveFrom.Value.Date <= DateTime.UtcNow.Date &&
                        (!stdrd.EffectiveTo.HasValue || stdrd.EffectiveTo.Value.Date >= DateTime.UtcNow.Date));

                if (s == null)
                {
                    continue;
                }

                std.Duration = s.ReservedValue1;
                std.FundingCap = s.MaxEmployerLevyCap;
            }
        }

        private LarsMetaData GetLarsMetaData(LarsCsvData larsCsvData)
        {
            var metaData = default(LarsMetaData);

            var frameworksMetaData = _csvService.ReadFromString<FrameworkMetaData>(larsCsvData.Framework);
            _logger.Debug($"Read {frameworksMetaData.Count} frameworks from file.");

            metaData.Frameworks = FilterFrameworks(frameworksMetaData);
            _logger.Debug($"There are {metaData.Frameworks.Count} frameworks after filtering.");

            metaData.FrameworkAims = _csvService.ReadFromString<FrameworkAimMetaData>(larsCsvData.FrameworkAim);
            metaData.FrameworkContentTypes = _csvService.ReadFromString<ApprenticeshipComponentTypeMetaData>(larsCsvData.FrameworkContentType);
            metaData.LearningDeliveries = _csvService.ReadFromString<LearningDeliveryMetaData>(larsCsvData.LearningDelivery);
            metaData.Fundings = _csvService.ReadFromString<FundingMetaData>(larsCsvData.Funding);
            metaData.ApprenticeshipFundings = _csvService.ReadFromString<ApprenticeshipFundingMetaData>(larsCsvData.ApprenticeshipFundingAndDuration);

            return metaData;
        }

        private string GetZipFilePath()
        {
            var url = $"{_appServiceSettings.ImServiceBaseUrl}/{_appServiceSettings.ImServiceUrl}";

            var link = _angleSharpService.GetLinks(url, "li a", "LARS CSV");
            var linkEndpoint = link?.FirstOrDefault();
            var fullLink = linkEndpoint != null ? $"{_appServiceSettings.ImServiceBaseUrl}/{linkEndpoint}" : string.Empty;

            if (string.IsNullOrEmpty(fullLink))
            {
                throw new Exception($"Can not find LARS zip file. Url: {url}");
            }

            return fullLink;
        }

        private LarsCsvData GetLarsCsvData(string zipFilePath)
        {
            var csvData = default(LarsCsvData);

            var zipStream = GetZipStream(zipFilePath);

            _logger.Debug("Zip file downloaded");
            csvData.Framework = _fileExtractor.ExtractFileFromStream(
                zipStream, _appServiceSettings.CsvFileNameFrameworks, true);

            csvData.FrameworkAim = _fileExtractor.ExtractFileFromStream(
                zipStream, _appServiceSettings.CsvFileNameFrameworksAim, true);

            csvData.FrameworkContentType = _fileExtractor.ExtractFileFromStream(
                zipStream, _appServiceSettings.CsvFileNameApprenticeshipComponentType, true);

            csvData.LearningDelivery = _fileExtractor.ExtractFileFromStream(
                zipStream, _appServiceSettings.CsvFileNameLearningDelivery, true);

            csvData.Funding = _fileExtractor.ExtractFileFromStream(
                zipStream, _appServiceSettings.CsvFileNameFunding, true);

            csvData.ApprenticeshipFundingAndDuration = _fileExtractor.ExtractFileFromStream(
                zipStream, _appServiceSettings.CsvFileNameApprenticeshipFunding, true);

            zipStream.Close();

            return csvData;
        }

        private void LogExecutionTime(string url, double elaspedMilliseconds)
        {
            var logEntry = new DependencyLogEntry
            {
                Identifier = "LarsZipDownload",
                ResponseTime = elaspedMilliseconds,
                Url = url
            };

            _logger.Debug("LARS zip download", logEntry);
        }

        private struct LarsCsvData
        {
            public string Framework { get; set; }
            public string FrameworkAim { get; set; }
            public string FrameworkContentType { get; set; }
            public string LearningDelivery { get; set; }
            public string Funding { get; set; }
            public string ApprenticeshipFundingAndDuration { get; set; }
        }

        private struct LarsMetaData
        {
            public ICollection<FrameworkMetaData> Frameworks { get; set; }
            public ICollection<FrameworkAimMetaData> FrameworkAims { get; set; }
            public ICollection<ApprenticeshipComponentTypeMetaData> FrameworkContentTypes { get; set; }
            public ICollection<LearningDeliveryMetaData> LearningDeliveries { get; set; }
            public ICollection<FundingMetaData> Fundings { get; set; }
            public ICollection<ApprenticeshipFundingMetaData> ApprenticeshipFundings { get; set; }
        }

        private struct CategorisedQualifications
        {
            public ICollection<string> Competency { get; set; }
            public ICollection<string> Knowledge { get; set; }
            public ICollection<string> Combined { get; set; }
        }
    }
}