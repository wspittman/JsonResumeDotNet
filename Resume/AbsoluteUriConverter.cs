using Newtonsoft.Json;
using System;

namespace JsonResume
{
    /// <summary>
    /// Converts an absolute URI from JSON.
    /// The built-in URI converter accepts relative URIs.
    /// </summary>
    public class AbsoluteUriConverter : JsonConverter<Uri>
    {
        public override Uri ReadJson(JsonReader reader, Type objectType, Uri existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string urlString = reader.Value as string;

            // Empty is ignored, not an exception
            if (string.IsNullOrEmpty(urlString))
            {
                return null;
            }

            try
            {
                return new Uri(urlString, UriKind.Absolute);
            }
            catch (UriFormatException ex)
            {
                throw new JsonSerializationException($"Invalid URI at path '{reader.Path}'", ex);
            }
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Uri value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }
    }
}
