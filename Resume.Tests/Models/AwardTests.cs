using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;

namespace Resume.Tests
{
    public class AwardTests
    {
        private const string FormatStringEmpty = "{{\"awards\": [{0}]}}";
        private const string FormatString = "{{\"awards\": [{{\"title\": {0}, \"date\": {1}, \"awarder\": {2}, \"summary\": {3}, \"extra\": {4}}}]}}";

        private Award FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg).Awards.FirstOrDefault();

        private Award FromJson(string title = null, string date = null, string awarder = null, string summary = null, string extra = null)
        {
            return Utils.FromJson(FormatString, title, date, awarder, summary, extra).Awards.FirstOrDefault();
        }

        [Test]
        public void EmptyTest()
        {
            var fromNull = FromJsonEmpty(null);
            var fromEmpty = FromJsonEmpty("{}");
            var constructed = new Award();

            Assert.AreEqual("null", JsonConvert.SerializeObject(fromNull));
            Assert.AreEqual(JsonConvert.SerializeObject(fromEmpty), JsonConvert.SerializeObject(constructed));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("One of the 100 greatest minds of the century")]
        public void TitleTest(string title)
        {
            var fromJson = FromJson(title: title);
            var constructed = new Award() { Title = title };
            Utils.ValidatePropertyPair(fromJson, constructed, title, x => (x as Award).Title);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        public void DateTest(string dateString)
        {
            DateTime? parsedDate = DateTime.TryParse(dateString, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(date: dateString);
            var constructed = new Award() { Date = parsedDate };
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => (x as Award).Date);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Time Magazine")]
        public void AwarderTest(string awarder)
        {
            var fromJson = FromJson(awarder: awarder);
            var constructed = new Award() { Awarder = awarder };
            Utils.ValidatePropertyPair(fromJson, constructed, awarder, x => (x as Award).Awarder);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Received for my work with Quantum Physics")]
        public void SummaryTest(string summary)
        {
            var fromJson = FromJson(summary: summary);
            var constructed = new Award() { Summary = summary };
            Utils.ValidatePropertyPair(fromJson, constructed, summary, x => (x as Award).Summary);
        }
    }
}