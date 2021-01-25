using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Resume
{
    public class CountryCodeConverter : JsonConverter<string>
    {
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string countryCodeString = reader.Value as string;

            // Empty is ignored, not an exception
            if (string.IsNullOrEmpty(countryCodeString))
            {
                return null;
            }

            try
            {
                var countryCode = new RegionInfo(countryCodeString);

                if (!countryCode.TwoLetterISORegionName.Equals(countryCodeString, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new FormatException($"Country code string {countryCodeString} has additional decorations beyond ISO region {countryCode.TwoLetterISORegionName}");
                }

                return countryCode.ToString();
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException($"Invalid country code string at path '{reader.Path}'", ex);
            }
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }
    }
}
