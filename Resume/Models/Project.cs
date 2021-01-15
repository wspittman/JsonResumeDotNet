using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Resume
{
    public class Project
    {
        /// <summary>
        /// e.g. The World Wide Web
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Short summary of project. e.g. Collated works of 2017.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Specify multiple features
        /// e.g. Directs you close but not quite there
        /// </summary>
        [JsonProperty("highlights")]
        public List<string> Highlights { get; set; } = new List<string>();

        /// <summary>
        /// Specify special elements involved
        /// e.g. AngularJS
        /// </summary>
        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; } = new List<string>();

        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// e.g. http://www.computer.org/csdl/mags/co/1996/10/rx069-abs.html
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// Specify your role on this project or in company
        /// e.g. Team Lead, Speaker, Writer
        /// </summary>
        [JsonProperty("roles")]
        public List<string> Roles { get; set; } = new List<string>();

        /// <summary>
        /// Specify the relevant company/entity affiliations e.g. 'greenpeace', 'corporationXYZ'
        /// </summary>
        [JsonProperty("entity")]
        public string Entity { get; set; }

        /// <summary>
        /// e.g. 'volunteering', 'presentation', 'talk', 'application', 'conference'
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
