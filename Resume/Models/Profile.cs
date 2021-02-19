using Newtonsoft.Json;
using System;

namespace JsonResume
{
    public class Profile
    {
        /// <summary>
        /// e.g. Facebook or Twitter
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// e.g. neutralthoughts
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// e.g. http://twitter.example.com/neutralthoughts
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
