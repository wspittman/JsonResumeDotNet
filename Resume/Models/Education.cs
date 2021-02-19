using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonResume
{
    public class Education
    {
        /// <summary>
        /// e.g. Massachusetts Institute of Technology
        /// </summary>
        [JsonProperty("institution")]
        public string Institution { get; set; }

        /// <summary>
        /// e.g. http://facebook.example.com
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// e.g. Arts
        /// </summary>
        [JsonProperty("area")]
        public string Area { get; set; }

        /// <summary>
        /// e.g. Bachelor
        /// </summary>
        [JsonProperty("studyType")]
        public string StudyType { get; set; }

        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// grade point average, e.g. 3.67/4.0
        /// </summary>
        [JsonProperty("score")]
        public string Score { get; set; }

        /// <summary>
        /// List notable courses/subjects
        /// e.g. H1302 - Introduction to American history
        /// </summary>
        [JsonProperty("courses")]
        public List<string> Courses { get; set; } = new List<string>();
    }
}
