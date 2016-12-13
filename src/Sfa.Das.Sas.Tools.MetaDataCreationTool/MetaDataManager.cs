﻿using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Extensions;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models.Git;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces;

    public class MetaDataManager : IGetStandardMetaData, IGenerateStandardMetaData, IGetFrameworkMetaData, IGetLarsMetadata
    {
        private readonly IAppServiceSettings _appServiceSettings;

        private readonly ILarsDataService _larsDataService;

        private readonly ILog _logger;

        private readonly IAngleSharpService _angleSharpService;
        private readonly IMetadataApiService _metadataApiService;

        private readonly IVstsService _vstsService;

        public MetaDataManager(
            ILarsDataService larsDataService,
            IVstsService vstsService,
            IAppServiceSettings appServiceSettings,
            IAngleSharpService angleSharpService,
            IMetadataApiService metadataApiService,
            ILog logger)
        {
            _larsDataService = larsDataService;
            _vstsService = vstsService;
            _appServiceSettings = appServiceSettings;
            _logger = logger;
            _angleSharpService = angleSharpService;
            _metadataApiService = metadataApiService;
        }

        public LarsData GetLarsData()
        {
            var larsData = _larsDataService.GetDataFromLars();
            UpdateVstsStandards(larsData.Standards);
            return larsData;
        }

        public void GenerateStandardMetadataFiles()
        {
            var currentMetaDataIds = _vstsService.GetExistingStandardIds().ToArray();

            var standards = _larsDataService
                .GetListOfCurrentStandards()
                .Select(MapStandardData)
                .Where(m => !currentMetaDataIds.Contains($"{m.Id}"))
                .ToArray();

            standards.ForEach(m => m.Published = false);

            PushStandardsToGit(standards.Select(MapToFileContent).ToList());
        }

        public IEnumerable<StandardMetaData> GetStandardsMetaData()
        {
            var standards = _vstsService
                .GetStandards()
                .ToList();
            _logger.Debug($"Retrieved {standards.Count} standards from VSTS");

            UpdateStandardsInformationFromLarsAndResolveUrls(standards);
            return standards;
        }

        public IEnumerable<FrameworkMetaData> GetAllFrameworks()
        {
            var frameworks = _larsDataService.GetListOfCurrentFrameworks();
            _logger.Debug($"Retrieved {frameworks.Count()} frameworks from LARS");
            UpdateFrameworkInformationFromVSTS(frameworks);
            return frameworks;
        }

        private static StandardRepositoryData MapStandardData(LarsStandard larsStandard)
        {
            var standardRepositoryData = new StandardRepositoryData
            {
                Id = larsStandard.Id,
                Title = larsStandard.Title,
                JobRoles = new List<string>(),
                Keywords = new List<string>(),
                TypicalLength = new TypicalLength { Unit = "m" },
                OverviewOfRole = string.Empty,
                EntryRequirements = string.Empty,
                WhatApprenticesWillLearn = string.Empty,
                Qualifications = string.Empty,
                ProfessionalRegistration = string.Empty
            };
            return standardRepositoryData;
        }

        private void UpdateVstsStandards(IEnumerable<LarsStandard> larsDataStandards)
        {
            var currentMetaDataIds = _vstsService.GetExistingStandardIds().ToArray();

            var standards = larsDataStandards
                .Select(MapStandardData)
                .Where(m => !currentMetaDataIds.Contains($"{m.Id}"))
                .ToArray();

            standards.ForEach(m => m.Published = false);

            PushStandardsToGit(standards.Select(MapToFileContent).ToList());
        }

        private FileContents MapToFileContent(StandardRepositoryData standardRepositoryData)
        {
            var json = JsonConvert.SerializeObject(standardRepositoryData, Formatting.Indented);
            var standardTitle = Path.GetInvalidFileNameChars().Aggregate(standardRepositoryData.Title, (current, c) => current.Replace(c, '_')).Replace(" ", string.Empty);
            var gitFilePath = $"{_appServiceSettings.VstsGitStandardsFolderPath}/{standardRepositoryData.Id}-{standardTitle}.json";
            return new FileContents(gitFilePath, json);
        }

        private void UpdateFrameworkInformationFromVSTS(IEnumerable<FrameworkMetaData> frameworks)
        {
            int updated = 0;
            var repositoryFrameworks = _vstsService.GetFrameworks();

            foreach (var framework in frameworks)
            {
                var repositoryFramework = repositoryFrameworks.FirstOrDefault(m =>
                    m.FrameworkCode == framework.FworkCode &&
                    m.ProgType == framework.ProgType &&
                    m.PathwayCode == framework.PwayCode);

                if (repositoryFramework == null)
                {
                    continue;
                }

                framework.Published = repositoryFramework.Published;
                framework.JobRoleItems = repositoryFramework.JobRoleItems;
                framework.Keywords = repositoryFramework.Keywords;
                framework.TypicalLength = repositoryFramework.TypicalLength;
                framework.CompletionQualifications = repositoryFramework.CompletionQualifications;
                framework.EntryRequirements = repositoryFramework.EntryRequirements;
                framework.ProfessionalRegistration = repositoryFramework.ProfessionalRegistration;
                framework.FrameworkOverview = repositoryFramework.FrameworkOverview;
                updated++;
            }

            _logger.Debug($"Updated {updated} frameworks from VSTS");
        }

        private void UpdateStandardsInformationFromLarsAndResolveUrls(IEnumerable<StandardMetaData> standards)
        {
            int updated = 0;
            var currentStandards = _larsDataService.GetListOfCurrentStandards().ToArray();

            foreach (var standard in standards)
            {
                var standardFromLars = currentStandards.SingleOrDefault(m => m.Id.Equals(standard.Id));

                if (standardFromLars == null)
                {
                    continue;
                }

                standard.NotionalEndLevel = standardFromLars.NotionalEndLevel;
                standard.StandardPdfUrl = GetLinkUri(standardFromLars.StandardUrl, "Apprenticeship");
                standard.AssessmentPlanPdfUrl = GetLinkUri(standardFromLars.StandardUrl, "Assessment");
                standard.SectorSubjectAreaTier1 = standardFromLars.SectorSubjectAreaTier1;
                standard.SectorSubjectAreaTier2 = standardFromLars.SectorSubjectAreaTier2;
                updated++;
            }

            _logger.Debug($"Updated {updated} standards from LARS and resolved PDF urls");
        }

        private string GetLinkUri(string link, string linkTitle)
        {
            if (string.IsNullOrEmpty(link))
            {
                return string.Empty;
            }

            var uri = _angleSharpService.GetLinks(link.RemoveQuotationMark(), ".attachment-details h2 a", linkTitle)?.FirstOrDefault();

            return uri != null ? new Uri(new Uri(_appServiceSettings.GovWebsiteUrl), uri).ToString() : string.Empty;
        }

        private void PushStandardsToGit(List<FileContents> standards)
        {
            if (!standards.Any())
            {
                return;
            }

            _vstsService.PushCommit(standards);
            _logger.Info($"Pushed {standards.Count} new meta files to Git Repository.");
        }
    }
}