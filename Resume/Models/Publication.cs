using Newtonsoft.Json;
using System;

namespace Resume
{
    public class Publication
    {
        /// <summary>
        /// e.g. The World Wide Web
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// e.g. IEEE, Computer Magazine
        /// </summary>
        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("releaseDate")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// e.g. http://www.computer.org.example.com/csdl/mags/co/1996/10/rx069-abs.html
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// Short summary of publication. e.g. Discussion of the World Wide Web, HTTP, HTML.
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}
