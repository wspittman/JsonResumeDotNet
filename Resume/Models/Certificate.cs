using Newtonsoft.Json;
using System;

namespace Resume
{
    public class Certificate
    {
        /// <summary>
        /// e.g. Certified Kubernetes Administrator
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// e.g. 1989-06-12
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// e.g. http://example.com
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// e.g. CNCF
        /// </summary>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }
    }
}
