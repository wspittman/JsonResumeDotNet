using Newtonsoft.Json;
using System;

namespace Resume
{
    public partial class Resume
    {
        public static Resume FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Resume>(json);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
