﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonResume
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
        public Uri Image { get; set; }

        /// <summary>
        /// e.g. thomas@gmail.com
        /// </summary>
        [JsonProperty("email")]
        [JsonConverter(typeof(EmailConverter))]
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
        public Uri Url { get; set; }

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
        public List<Profile> Profiles { get; set; } = new List<Profile>();
    }
}
