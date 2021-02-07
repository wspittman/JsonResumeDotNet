using Newtonsoft.Json;

namespace Resume
{
    public class Location
    {
        /// <summary>
        /// To add multiple address lines, use \n. For example, 1234 Glücklichkeit Straße\nHinterhaus 5. Etage li.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// code as per ISO-3166-1 ALPHA-2, e.g. US, AU, IN
        /// </summary>
        [JsonProperty("countryCode")]
        [JsonConverter(typeof(TwoLetterISORegionNameConverter))]
        public string CountryCode { get; set; }

        /// <summary>
        /// The general region where you live. Can be a US state, or a province, for instance.
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }
    }
}
