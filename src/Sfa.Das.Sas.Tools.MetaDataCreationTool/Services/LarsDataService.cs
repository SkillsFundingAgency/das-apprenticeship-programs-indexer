﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    using System.IO;

    using Sfa.Das.Sas.Indexer.Core.Logging.Metrics;
    using Sfa.Das.Sas.Indexer.Core.Logging.Models;

    public sealed class LarsDataService : ILarsDataService
    {
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

            zipStream.Close();

            return standards;
        }

        public IEnumerable<FrameworkMetaData> GetListOfCurrentFrameworks()
        {
            var zipFilePath = GetZipFilePath();
            _logger.Debug($"Zip file path: {zipFilePath}");

            var csvData = GetLarsCsvData(zipFilePath);

            var larsMetaData = GetLarsMetaData(csvData);

            AddQualificationsToFrameworks(larsMetaData);

            return larsMetaData.Frameworks;
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

        private static ICollection<FrameworkMetaData> FilterFrameworks(IEnumerable<FrameworkMetaData> frameworks)
        {
            var progTypeList = new[] { 2, 3, 20, 21, 22, 23 };

            return frameworks.Where(s => s.FworkCode > 399)
                .Where(s => s.PwayCode > 0)
                .Where(s => !s.EffectiveFrom.Equals(DateTime.MinValue))
                .Where(s => !s.EffectiveTo.HasValue || s.EffectiveTo >= DateTime.Today)
                .Where(s => progTypeList.Contains(s.ProgType))
                .ToList();
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

                frameworkAims = frameworkAims.Where(x => x.EffectiveTo >= DateTime.Now || x.EffectiveTo == null).ToList();

                var qualifications =
                    (from aim in frameworkAims
                     join comp in metaData.FrameworkContentTypes on aim.FrameworkComponentType equals comp.FrameworkComponentType
                     join ld in metaData.LearningDeliveries on aim.LearnAimRef equals ld.LearnAimRef
                     select new FrameworkQualification
                     {
                         Title = ld.LearnAimRefTitle.Replace("(QCF)", string.Empty).Trim(),
                         LearnAimRef = aim.LearnAimRef,
                         CompetenceType = comp.FrameworkComponentType,
                         CompetenceDescription = comp.FrameworkComponentTypeDesc
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

        private LarsMetaData GetLarsMetaData(LarsCsvData larsCsvData)
        {
            var metaData = default(LarsMetaData);

            var frameworksMetaData = _csvService.ReadFromString<FrameworkMetaData>(larsCsvData.Framework);
            _logger.Debug($"Read {frameworksMetaData.Count} frameworks from file.");

            metaData.Frameworks = FilterFrameworks(frameworksMetaData);
            _logger.Debug($"There are {metaData.Frameworks.Count} frameworks after filtering.");

            metaData.FrameworkAims = _csvService.ReadFromString<FrameworkAimMetaData>(larsCsvData.FrameworkAim);
            metaData.FrameworkContentTypes = _csvService.ReadFromString<FrameworkComponentTypeMetaData>(larsCsvData.FrameworkContentType);
            metaData.LearningDeliveries = _csvService.ReadFromString<LearningDeliveryMetaData>(larsCsvData.LearningDelivery);
            metaData.Fundings = _csvService.ReadFromString<FundingMetaData>(larsCsvData.Funding);

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
                zipStream, _appServiceSettings.CsvFileNameFrameworkComponentType, true);

                csvData.LearningDelivery = _fileExtractor.ExtractFileFromStream(
                    zipStream, _appServiceSettings.CsvFileNameLearningDelivery, true);

                csvData.Funding = _fileExtractor.ExtractFileFromStream(
                    zipStream, _appServiceSettings.CsvFileNameFunding, true);

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
        }

        private struct LarsMetaData
        {
            public ICollection<FrameworkMetaData> Frameworks { get; set; }
            public ICollection<FrameworkAimMetaData> FrameworkAims { get; set; }
            public ICollection<FrameworkComponentTypeMetaData> FrameworkContentTypes { get; set; }
            public ICollection<LearningDeliveryMetaData> LearningDeliveries { get; set; }
            public ICollection<FundingMetaData> Fundings { get; set; }
        }

        private struct CategorisedQualifications
        {
            public ICollection<string> Competency { get; set; }
            public ICollection<string> Knowledge { get; set; }
            public ICollection<string> Combined { get; set; }
        }
    }
}