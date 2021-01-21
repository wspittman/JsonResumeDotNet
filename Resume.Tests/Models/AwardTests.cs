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
            var awardNull = FromJsonEmpty(null);
            var awardEmpty = FromJsonEmpty("{}");
            var awardConstructed = new Award();

            Assert.AreEqual("null", JsonConvert.SerializeObject(awardNull));
            Assert.AreEqual(JsonConvert.SerializeObject(awardEmpty), JsonConvert.SerializeObject(awardConstructed));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("One of the 100 greatest minds of the century")]
        public void TitleTest(string title)
        {
            var awardFromJson = FromJson(title: title);
            var awardConstructed = new Award() { Title = title };
            Utils.ValidatePropertyPair(awardFromJson, awardConstructed, title, x => (x as Award).Title);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        public void DateTest(string dateString)
        {
            DateTime? parsedDate = DateTime.TryParse(dateString, out DateTime parsed) ? parsed : (DateTime?)null;

            var awardFromJson = FromJson(date: dateString);
            var awardConstructed = new Award() { Date = parsedDate };
            Utils.ValidatePropertyPair(awardFromJson, awardConstructed, parsedDate, x => (x as Award).Date);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Time Magazine")]
        public void AwarderTest(string awarder)
        {
            var awardFromJson = FromJson(awarder: awarder);
            var awardConstructed = new Award() { Awarder = awarder };
            Utils.ValidatePropertyPair(awardFromJson, awardConstructed, awarder, x => (x as Award).Awarder);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Received for my work with Quantum Physics")]
        public void SummaryTest(string summary)
        {
            var awardFromJson = FromJson(summary: summary);
            var awardConstructed = new Award() { Summary = summary };
            Utils.ValidatePropertyPair(awardFromJson, awardConstructed, summary, x => (x as Award).Summary);
        }
    }
}