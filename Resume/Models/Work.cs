﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Resume
{
    public class Work
    {
        /// <summary>
        /// e.g. Facebook
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// e.g. Menlo Park, CA
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// e.g. Social Media Company
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// e.g. Software Engineer
        /// </summary>
        [JsonProperty("position")]
        public string Position { get; set; }

        /// <summary>
        /// e.g. http://facebook.example.com
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

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
        public List<string> Highlights { get; set; }
    }
}
