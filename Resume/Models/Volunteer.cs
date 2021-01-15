using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Resume
{
    public class Volunteer
    {
        /// <summary>
        /// e.g. Facebook
        /// </summary>
        [JsonProperty("organization")]
        public string Organization { get; set; }

        /// <summary>
        /// e.g. Software Engineer
        /// </summary>
        [JsonProperty("position")]
        public string Position { get; set; }

        /// <summary>
        /// e.g. http://facebook.example.com
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Give an overview of your responsibilities at the company
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Specify multiple accomplishments
        /// e.g. Increased profits by 20% from 2011-2012 through viral advertising
        /// </summary>
        [JsonProperty("highlights")]
        public List<string> Highlights { get; set; } = new List<string>();
    }
}
