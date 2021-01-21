using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;

namespace Resume.Tests
{
    public static class Utils
    {
        public static Resume FromJson(string format, params object[] args)
        {
            var prepped = args.Select(str => (str as string == "{}") ? "{}" : JsonConvert.SerializeObject(str)).ToArray();
            return Resume.FromJson(string.Format(format, prepped));
        }

        public static void ValidatePropertyPair(Resume parsed, Resume constructed, object expectedPropertyValue, Func<Resume, object> getProperty)
        {
            // Is the property value what we expect?
            Assert.AreEqual(expectedPropertyValue, getProperty(parsed));

            // Do the parsed and constructed objects both have matching values?
            Assert.AreEqual(getProperty(parsed), getProperty(constructed));

            // Do the parsed and constructed objects both have matching serialized forms?
            Assert.AreEqual(parsed.ToJson(), constructed.ToJson());
        }
    }
}
