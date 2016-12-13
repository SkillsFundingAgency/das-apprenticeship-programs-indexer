﻿namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    using System;
    using System.Collections.Generic;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Factories;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces;

    public class CsvService : IReadMetaDataFromCsv
    {
        private readonly IGenericMetaDataFactory _metaDataFactory;

        public CsvService(IGenericMetaDataFactory metaDataFactory)
        {
            _metaDataFactory = metaDataFactory;
        }

        public ICollection<T> ReadFromString<T>(string csvString)
            where T : class
        {
            var metaDataElements = new List<T>();

            foreach (var line in csvString.Split('\n'))
            {
                if (line == string.Empty)
                {
                    continue;
                }

                var values = line?.Split(new[] { "\",\"" }, StringSplitOptions.None);

                var metaData = _metaDataFactory.Create<T>(values);

                if (metaData != null)
                {
                    metaDataElements.Add(metaData);
                }
            }

            return metaDataElements;
        }
    }
}
