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
            return Resume.FromJson(string.Format(format, prepped), throwOnError: false, errorOnUnknownMember: true);
        }

        public static void ValidatePropertyPair(Resume parsed, Resume constructed, object expectedPropertyValue, Func<Resume, object> getProperty, int expectedParsingErrorCount = 0)
        {
            Assert.AreEqual(expectedPropertyValue, getProperty(parsed), "The property value is not what we expect");

            Assert.AreEqual(expectedParsingErrorCount, parsed.ParsingErrors.Count, $"Unexpected number of parsing errors{Environment.NewLine}{string.Join(Environment.NewLine, parsed.ParsingErrors)}");

            Assert.AreEqual(getProperty(parsed), getProperty(constructed), "Parsed and constructed objects do not have matching values");

            Assert.AreEqual(parsed.ToJson(), constructed.ToJson(), "Parsed and constructed objects do not have matching serialized forms");
        }
    }
}
