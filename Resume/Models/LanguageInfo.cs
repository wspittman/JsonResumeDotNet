using Newtonsoft.Json;

namespace JsonResume
{
    public class LanguageInfo
    {
        /// <summary>
        /// e.g. English, Spanish
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// e.g. Fluent, Beginner
        /// </summary>
        [JsonProperty("fluency")]
        public string Fluency { get; set; }
    }
}
