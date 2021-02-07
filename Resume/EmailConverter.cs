using Newtonsoft.Json;
using System;
using System.Net.Mail;

namespace Resume
{
    /// <summary>
    /// Converts an email address from JSON.
    /// </summary>
    public class EmailConverter : JsonConverter<string>
    {
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string emailString = reader.Value as string;

            // Empty is ignored, not an exception
            if (string.IsNullOrEmpty(emailString))
            {
                return null;
            }

            try
            {
                var email = new MailAddress(emailString);

                if (email.Address != emailString)
                {
                    throw new FormatException($"Email string '{emailString}' has additional decorations beyond address '{email.Address}'");
                }
                
                return email.Address;
            }
            catch (FormatException ex)
            {
                throw new JsonSerializationException($"Invalid email at path '{reader.Path}'", ex);
            }
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }
    }
}
