﻿// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Sfa.Infrastructure.CourseDirectory.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    public static class ProviderCollection
    {
        /// <summary>
        ///     Deserialize the object
        /// </summary>
        public static IList<Provider> DeserializeJson(JToken inputObject)
        {
            IList<Provider> deserializedObject = new List<Provider>();
            // HACK: fix for the api implementation being wrong
            //foreach (var iListValue in (JArray)((JObject)inputObject).Property("providers").Value)
            foreach (var iListValue in (JArray)inputObject)
            {
                var provider = new Provider();
                provider.DeserializeJson(iListValue);
                deserializedObject.Add(provider);
            }

            return deserializedObject;
        }
    }
}