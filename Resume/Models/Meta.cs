using Newtonsoft.Json;
using System;

namespace JsonResume
{
    public class Meta
    {
        /// <summary>
        /// URL (as per RFC 3986) to latest version of this document
        /// </summary>
        [JsonProperty("canonical")]
        public Uri Canonical { get; set; }

        /// <summary>
        /// A version field which follows semver - e.g. v1.0.0
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Using ISO 8601 with YYYY-MM-DDThh:mm:ss
        /// </summary>
        [JsonProperty("lastModified")]
        public DateTime? LastModified { get; set; }
    }
}
