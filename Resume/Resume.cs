using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Resume
{
    public partial class Resume
    {
        [JsonIgnore]
        public List<Exception> ParsingErrors { get; private set; } = new List<Exception>();

        public static Resume FromJson(string json, bool throwOnError = true, bool errorOnUnknownMember = false)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new Resume();
            }

            var settings = GetSettings(errorOnUnknownMember);
            var errors = new List<Exception>();

            settings.Error = (_, args) =>
            {
                // If this is the source of the error, rather than a parent collection/object, log it
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

            settings.Converters.Add(new AbsoluteUriConverter());

            var resume = JsonConvert.DeserializeObject<Resume>(json, settings);
            
            resume.ParsingErrors = errors;
            
            return resume;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, GetSettings());
        }

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
