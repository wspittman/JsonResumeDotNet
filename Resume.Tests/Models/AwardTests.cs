using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class AwardTests
    {
        private const string FormatStringEmpty = "{{ \"awards\": [ {0} ] }}";
        private const string FormatString = "{{ \"awards\": [ {{ \"title\": {0}, \"date\": {1}, \"awarder\": {2}, \"summary\": {3} }} ] }}";

        private Award Path(Resume resume) => resume.Awards.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string title = null, string date = null, string awarder = null, string summary = null)
        {
            return Utils.FromJson(FormatString, title, date, awarder, summary);
        }

        private Resume Constructed(string title = null, DateTime? date = null, string awarder = null, string summary = null)
        {
            var award = new Award();

            // Set values separately from object initialization to preserve any default values.
            if (title != null) award.Title = title;
            if (date != null) award.Date = date;
            if (awarder != null) award.Awarder = awarder;
            if (summary != null) award.Summary = summary;

            return new Resume() { Awards = new List<Award>() { award } };
        }

        [Test]
        public void EmptyTest()
        {
            var fromNull = FromJsonEmpty(null);
            var fromEmpty = FromJsonEmpty("{}");
            var constructed = Constructed();

            Assert.AreEqual(null, Path(fromNull));
            Assert.AreEqual(fromEmpty.ToJson(), constructed.ToJson());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("One of the 100 greatest minds of the century")]
        public void TitleTest(string title)
        {
            var fromJson = FromJson(title: title);
            var constructed = Constructed(title: title);
            Utils.ValidatePropertyPair(fromJson, constructed, title, x => Path(x)?.Title);
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
            var constructed = Constructed(date: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.Date);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Time Magazine")]
        public void AwarderTest(string awarder)
        {
            var fromJson = FromJson(awarder: awarder);
            var constructed = Constructed(awarder: awarder);
            Utils.ValidatePropertyPair(fromJson, constructed, awarder, x => Path(x)?.Awarder);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Received for my work with Quantum Physics")]
        public void SummaryTest(string summary)
        {
            var fromJson = FromJson(summary: summary);
            var constructed = Constructed(summary: summary);
            Utils.ValidatePropertyPair(fromJson, constructed, summary, x => Path(x)?.Summary);
        }
    }
}