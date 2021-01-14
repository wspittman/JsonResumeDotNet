using Newtonsoft.Json;
using System.Collections.Generic;

namespace Resume
{
    public class Interest
    {
        /// <summary>
        /// e.g. Philosophy
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// e.g. Friedrich Nietzsche
        /// </summary>
        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; } = new List<string>();
    }
}
