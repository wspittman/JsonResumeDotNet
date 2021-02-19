using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonResume
{
    /// <summary>
    /// Represents a JSONResume object
    /// </summary>
    public partial class Resume
    {
        /// <summary>
        /// A list of errors encountered during JSON parsing. Only used if FromJson is called with throwOnError=false.
        /// </summary>
        [JsonIgnore]
        public List<Exception> ParsingErrors { get; private set; } = new List<Exception>();

        /// <summary>
        /// Parse a JSONResume into a Resume object.
        /// </summary>
        /// <param name="json">The JSON text to parse. If null or empty, an empty Resume object is returned.</param>
        /// <param name="throwOnError">True to throw an exception on the first parsing error, false to save the parsing errors to the ParsingErrors property.</param>
        /// <param name="errorOnUnknownMember">True if unknown properties in the JSON are treated as a parsing error, false if they are ignored.</param>
        /// <returns>The Resume object version of the JSON text.</returns>
        public static Resume FromJson(string json, bool throwOnError = true, bool errorOnUnknownMember = false)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new Resume();
            }

            var errors = new List<Exception>();
            var settings = GetSettings(errorOnUnknownMember);
            
            settings.Error = (_, args) =>
            {
                // Only log errors from the source token, not a parent collection/object
                if (args.CurrentObject == args.ErrorContext.OriginalObject)
                {
                    errors.Add(args.ErrorContext.Error);
                }

                // If we don't want to throw, set Handled=true to prevent JsonConvert from throwing
                if (!throwOnError)
                {
                    args.ErrorContext.Handled = true;
                }
            };

            // All URIs should be absolute URIs
            settings.Converters.Add(new AbsoluteUriConverter());

            var resume = JsonConvert.DeserializeObject<Resume>(json, settings);

            if (resume == null)
            {
                errors.Insert(0, new ArgumentException("Text could not be parsed as JSON", "json"));
                resume = new Resume();
            }
            
            resume.ParsingErrors = errors;
            
            return resume;
        }

        /// <summary>
        /// Convert this Resume object to its JSONResume equivalent.
        /// </summary>
        /// <returns>The JSON text.</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, GetSettings());
        }

        /// <summary>
        /// Get the standard settings.
        /// </summary>
        private static JsonSerializerSettings GetSettings(bool errorOnUnknownMember = false)
        {
            return new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                MissingMemberHandling = errorOnUnknownMember ? MissingMemberHandling.Error : MissingMemberHandling.Ignore,
            };
        }
    }
}
