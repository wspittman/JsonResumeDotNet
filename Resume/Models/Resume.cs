using Newtonsoft.Json;
using System.Collections.Generic;

namespace Resume
{
    public partial class Resume
    {
        [JsonProperty("basics")]
        public Basics Basics { get; set; }

        [JsonProperty("work")]
        public List<Work> Work { get; set; }

        [JsonProperty("volunteer")]
        public List<Volunteer> Volunteer { get; set; }

        [JsonProperty("education")]
        public List<Education> Education { get; set; }

        /// <summary>
        /// Specify any awards you have received throughout your professional career
        /// </summary>
        [JsonProperty("awards")]
        public List<Award> Awards { get; set; }

        /// <summary>
        /// Specify any certificates you have received throughout your professional career
        /// </summary>
        [JsonProperty("certificates")]
        public List<Certificate> Certificates { get; set; }

        /// <summary>
        /// Specify your publications through your career
        /// </summary>
        [JsonProperty("publications")]
        public List<Publication> Publications { get; set; }

        /// <summary>
        /// List out your professional skill-set
        /// </summary>
        [JsonProperty("skills")]
        public List<Skill> Skills { get; set; }

        /// <summary>
        /// List any other languages you speak
        /// </summary>
        [JsonProperty("languages")]
        public List<LanguageInfo> Languages { get; set; }

        [JsonProperty("interests")]
        public List<Interest> Interests { get; set; }

        /// <summary>
        /// List references you have received
        /// </summary>
        [JsonProperty("references")]
        public List<ReferenceInfo> References { get; set; }

        /// <summary>
        /// Specify career projects
        /// </summary>
        [JsonProperty("projects")]
        public List<Project> Projects { get; set; }
    }
}
