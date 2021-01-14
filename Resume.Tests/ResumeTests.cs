using NUnit.Framework;
using System.IO;

namespace Resume.Tests
{
    public class ResumeTests
    {
        private static string SampleJson;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string sampleName = "sample.resume.json";
            SampleJson = File.ReadAllText(sampleName);
        }

        [Test]
        public void Test1()
        {
            Assert.IsNotEmpty(SampleJson);

            var resume = Resume.FromJson(SampleJson);

            Assert.IsNotNull(resume);

            var roundTrip = resume.ToJson();

            Assert.IsNotEmpty(roundTrip);
        }
    }
}