﻿// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

using System.Collections.Generic;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory.Models
{
    public class Provider
    {
        /// <summary>
        ///     Initializes a new instance of the Provider class.
        /// </summary>
        public Provider()
        {
            Frameworks = new LazyList<Framework>();
            Locations = new LazyList<Location>();
            Standards = new LazyList<Standard>();
        }

        /// <summary>
        ///     Initializes a new instance of the Provider class with required
        ///     arguments.  
        /// </summary>
        public Provider(int ukprn)
            : this()
        {
            Ukprn = ukprn;
        }

        /// <summary>
        ///     Optional.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public double? EmployerSatisfaction { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public IList<Framework> Frameworks { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public double? LearnerSatisfaction { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public IList<Location> Locations { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public string MarketingInfo { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public bool NationalProvider { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public IList<Standard> Standards { get; set; }

        /// <summary>
        ///     Required.
        /// </summary>
        public int Ukprn { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        ///     Deserialize the object
        /// </summary>
        public virtual void DeserializeJson(JToken inputObject)
        {
            if (inputObject != null && inputObject.Type != JTokenType.Null)
            {
                var idValue = inputObject["id"];
                if (idValue != null && idValue.Type != JTokenType.Null)
                {
                    Id = int.Parse((string)idValue);
                }

                var emailValue = inputObject["email"];
                if (emailValue != null && emailValue.Type != JTokenType.Null)
                {
                    Email = (string)emailValue;
                }

                var employerSatisfactionValue = inputObject["employerSatisfaction"];
                if (employerSatisfactionValue != null && employerSatisfactionValue.Type != JTokenType.Null)
                {
                    EmployerSatisfaction = (double)employerSatisfactionValue;
                }

                var frameworksSequence = inputObject["frameworks"];
                if (frameworksSequence != null && frameworksSequence.Type != JTokenType.Null)
                {
                    foreach (var frameworksValue in (JArray)frameworksSequence)
                    {
                        var framework = new Framework();
                        framework.DeserializeJson(frameworksValue);
                        Frameworks.Add(framework);
                    }
                }

                var learnerSatisfactionValue = inputObject["learnerSatisfaction"];
                if (learnerSatisfactionValue != null && learnerSatisfactionValue.Type != JTokenType.Null)
                {
                    LearnerSatisfaction = (double)learnerSatisfactionValue;
                }

                var locationsSequence = inputObject["locations"];
                if (locationsSequence != null && locationsSequence.Type != JTokenType.Null)
                {
                    foreach (var locationsValue in (JArray)locationsSequence)
                    {
                        var location = new Location();
                        location.DeserializeJson(locationsValue);
                        Locations.Add(location);
                    }
                }

                var marketingInfoValue = inputObject["marketingInfo"];
                if (marketingInfoValue != null && marketingInfoValue.Type != JTokenType.Null)
                {
                    MarketingInfo = (string)marketingInfoValue;
                }

                var nameValue = inputObject["name"];
                if (nameValue != null && nameValue.Type != JTokenType.Null)
                {
                    Name = (string)nameValue;
                }

                var nationalProviderValue = inputObject["nationalProvider"];
                if (nationalProviderValue != null && nationalProviderValue.Type != JTokenType.Null)
                {
                    NationalProvider = (bool)nationalProviderValue;
                }

                var phoneValue = inputObject["phone"];
                if (phoneValue != null && phoneValue.Type != JTokenType.Null)
                {
                    Phone = (string)phoneValue;
                }

                var standardsSequence = inputObject["standards"];
                if (standardsSequence != null && standardsSequence.Type != JTokenType.Null)
                {
                    foreach (var standardsValue in (JArray)standardsSequence)
                    {
                        var standard = new Standard();
                        standard.DeserializeJson(standardsValue);
                        Standards.Add(standard);
                    }
                }

                var ukprnValue = inputObject["ukprn"];
                if (ukprnValue != null && ukprnValue.Type != JTokenType.Null)
                {
                    Ukprn = (int)ukprnValue;
                }

                var websiteValue = inputObject["website"];
                if (websiteValue != null && websiteValue.Type != JTokenType.Null)
                {
                    Website = (string)websiteValue;
                }
            }
        }
    }
}