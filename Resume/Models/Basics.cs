using Newtonsoft.Json;
using System.Collections.Generic;

namespace Resume
{
    public class Basics
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// e.g. Web Developer
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// URL (as per RFC 3986) to a image in JPEG or PNG format
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        /// e.g. thomas@gmail.com
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Phone numbers are stored as strings so use any format you like, e.g. 712-117-2923
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// URL (as per RFC 3986) to your website, e.g. personal homepage
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Write a short 2-3 sentence biography about yourself
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        /// <summary>
        /// Specify any number of social networks that you participate in
        /// </summary>
        [JsonProperty("profiles")]
        public List<Profile> Profiles { get; set; }
    }
}
