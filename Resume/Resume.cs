using Newtonsoft.Json;
using System;

namespace Resume
{
    public partial class Resume
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            // Error when JSON has a property that class doesn't
            //MissingMemberHandling = MissingMemberHandling.Error
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
        };

        public static Resume FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Resume>(json, settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, settings);
        }
    }
}
