using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Resume
{
    /// <summary>
    /// Converts an ISO-3166 region name from JSON.
    /// </summary>
    public class TwoLetterISORegionNameConverter : JsonConverter<string>
    {
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string regionNameString = reader.Value as string;

            // Empty is ignored, not an exception
            if (string.IsNullOrEmpty(regionNameString))
            {
                return null;
            }

            try
            {
                var regionInfo = new RegionInfo(regionNameString);

                if (!regionInfo.TwoLetterISORegionName.Equals(regionNameString, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new FormatException($"Region name string '{regionNameString}' has additional decorations beyond ISO region name '{regionInfo.TwoLetterISORegionName}'");
                }

                return regionInfo.ToString();
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException($"Invalid region name string at path '{reader.Path}'", ex);
            }
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }
    }
}
