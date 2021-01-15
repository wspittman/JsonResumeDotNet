using Newtonsoft.Json;
using System;

namespace Resume
{
    public class Award
    {
        /// <summary>
        /// e.g. One of the 100 greatest minds of the century
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// e.g. Time Magazine
        /// </summary>
        [JsonProperty("awarder")]
        public string Awarder { get; set; }

        /// <summary>
        /// e.g. Received for my work with Quantum Physics
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}
