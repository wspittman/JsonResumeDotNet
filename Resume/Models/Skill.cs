using Newtonsoft.Json;
using System.Collections.Generic;

namespace Resume
{
    public class Skill
    {
        /// <summary>
        /// e.g. Web Development
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// e.g. Master
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }

        /// <summary>
        /// List some keywords pertaining to this skill
        /// e.g. HTML
        /// </summary>
        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; } = new List<string>();
    }
}
