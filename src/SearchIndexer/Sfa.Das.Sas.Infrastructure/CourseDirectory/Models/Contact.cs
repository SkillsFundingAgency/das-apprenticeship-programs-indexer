﻿// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

using Newtonsoft.Json.Linq;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory.Models
{
    public class Contact
    {
        /// <summary>
        ///     Optional.
        /// </summary>
        public string ContactUsUrl { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Optional.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     Deserialize the object
        /// </summary>
        public virtual void DeserializeJson(JToken inputObject)
        {
            if (inputObject != null && inputObject.Type != JTokenType.Null)
            {
                var contactUsUrlValue = inputObject["contactUsUrl"];
                if (contactUsUrlValue != null && contactUsUrlValue.Type != JTokenType.Null)
                {
                    ContactUsUrl = (string)contactUsUrlValue;
                }
                var emailValue = inputObject["email"];
                if (emailValue != null && emailValue.Type != JTokenType.Null)
                {
                    Email = (string)emailValue;
                }
                var phoneValue = inputObject["phone"];
                if (phoneValue != null && phoneValue.Type != JTokenType.Null)
                {
                    Phone = (string)phoneValue;
                }
            }
        }
    }
}