using Newtonsoft.Json;

namespace Resume
{
    public class ReferenceInfo
    {
        /// <summary>
        /// e.g. Timothy Cook
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// e.g. Joe blogs was a great employee, who turned up to work at least once a week. He exceeded my expectations when it came to doing nothing.
        /// </summary>
        [JsonProperty("reference")]
        public string Reference{ get; set; }
    }
}
