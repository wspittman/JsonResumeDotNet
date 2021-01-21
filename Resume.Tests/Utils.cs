using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;

namespace Resume.Tests
{
    public static class Utils
    {
        public static Resume FromJson(string format, params string[] args)
        {
            var prepped = args.Select(str =>
            {
                return str switch
                {
                    null => "null",
                    "{}" => "{}",
                    _ => $"\"{str}\"",
                };
            }).ToArray();

            return Resume.FromJson(string.Format(format, prepped));
        }

        public static void ValidatePropertyPair(object objectParsed, object objectConstructed, object expectedPropertyValue, Func<object, object> getProperty)
        {
            // Is the property value what we expect?
            Assert.AreEqual(expectedPropertyValue, getProperty(objectParsed));

            // Do the parsed and constructed objects both have matching values?
            Assert.AreEqual(getProperty(objectParsed), getProperty(objectConstructed));

            // Do the parsed and constructed objects both have matching serialized forms?
            Assert.AreEqual(JsonConvert.SerializeObject(objectParsed), JsonConvert.SerializeObject(objectConstructed));
        }
    }
}
