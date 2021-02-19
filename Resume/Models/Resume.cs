using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonResume
{
    public partial class Resume
    {
        /// <summary>
        /// Link to the version of the schema that can validate the resume
        /// </summary>
        [JsonProperty("$schema")]
        public Uri Schema { get; set; }

        [JsonProperty("basics")]
        public Basics Basics { get; set; }

        [JsonProperty("work")]
        public List<Work> Work { get; set; } = new List<Work>();

        [JsonProperty("volunteer")]
        public List<Volunteer> Volunteer { get; set; } = new List<Volunteer>();

        [JsonProperty("education")]
        public List<Education> Education { get; set; } = new List<Education>();

        /// <summary>
        /// Specify any awards you have received throughout your professional career
        /// </summary>
        [JsonProperty("awards")]
        public List<Award> Awards { get; set; } = new List<Award>();

        /// <summary>
        /// Specify any certificates you have received throughout your professional career
        /// </summary>
        [JsonProperty("certificates")]
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();

        /// <summary>
        /// Specify your publications through your career
        /// </summary>
        [JsonProperty("publications")]
        public List<Publication> Publications { get; set; } = new List<Publication>();

        /// <summary>
        /// List out your professional skill-set
        /// </summary>
        [JsonProperty("skills")]
        public List<Skill> Skills { get; set; } = new List<Skill>();

        /// <summary>
        /// List any other languages you speak
        /// </summary>
        [JsonProperty("languages")]
        public List<LanguageInfo> Languages { get; set; } = new List<LanguageInfo>();

        [JsonProperty("interests")]
        public List<Interest> Interests { get; set; } = new List<Interest>();

        /// <summary>
        /// List references you have received
        /// </summary>
        [JsonProperty("references")]
        public List<ReferenceInfo> References { get; set; } = new List<ReferenceInfo>();

        /// <summary>
        /// Specify career projects
        /// </summary>
        [JsonProperty("projects")]
        public List<Project> Projects { get; set; } = new List<Project>();

        /// <summary>
        /// The schema version and any other tooling configuration lives here
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
