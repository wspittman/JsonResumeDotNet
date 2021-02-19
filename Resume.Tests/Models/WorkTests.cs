using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonResume.Tests
{
    public class WorkTests
    {
        private const string FormatStringEmpty = "{{ \"work\": [ {0} ] }}";
        private const string FormatString = "{{ \"work\": [ {{ \"name\": {0}, \"location\": {1}, \"description\": {2}, \"position\": {3}, \"url\": {4}, \"startDate\": {5}, \"endDate\": {6}, \"summary\": {7}, \"highlights\": {8} }} ] }}";

        private Work Path(Resume resume) => resume.Work.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string name = null, string location = null, string description = null, string position = null, string url = null, string startDate = null, string endDate = null, string summary = null, string[] highlights = null)
        {
            return Utils.FromJson(FormatString, name, location, description, position, url, startDate, endDate, summary, highlights);
        }

        private Resume Constructed(string name = null, string location = null, string description = null, string position = null, Uri url = null, DateTime? startDate = null, DateTime? endDate = null, string summary = null, List<string> highlights = null)
        {
            var work = new Work();

            // Set values separately from object initialization to preserve any default values.
            if (name != null) work.Name = name;
            if (location != null) work.Location = location;
            if (description != null) work.Description = description;
            if (position != null) work.Position = position;
            if (url != null) work.Url = url;
            if (startDate != null) work.StartDate = startDate;
            if (endDate != null) work.EndDate = endDate;
            if (summary != null) work.Summary = summary;
            if (highlights != null) work.Highlights = highlights;

            return new Resume() { Work = new List<Work>() { work } };
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
        [TestCase("Facebook")]
        public void NameTest(string name)
        {
            var fromJson = FromJson(name: name);
            var constructed = Constructed(name: name);
            Utils.ValidatePropertyPair(fromJson, constructed, name, x => Path(x)?.Name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Menlo Park, CA")]
        public void LocationTest(string location)
        {
            var fromJson = FromJson(location: location);
            var constructed = Constructed(location: location);
            Utils.ValidatePropertyPair(fromJson, constructed, location, x => Path(x)?.Location);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Social Media Company")]
        public void DescriptionTest(string description)
        {
            var fromJson = FromJson(description: description);
            var constructed = Constructed(description: description);
            Utils.ValidatePropertyPair(fromJson, constructed, description, x => Path(x)?.Description);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Software Engineer")]
        public void PositionTest(string position)
        {
            var fromJson = FromJson(position: position);
            var constructed = Constructed(position: position);
            Utils.ValidatePropertyPair(fromJson, constructed, position, x => Path(x)?.Position);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("http://facebook.example.com")]
        [TestCase("facebook.example.com", true)]
        [TestCase("Clearly not a url", true)]
        public void UrlTest(string urlString, bool expectParsingError = false)
        {
            Uri.TryCreate(urlString, UriKind.Absolute, out Uri parsedUri);

            var fromJson = FromJson(url: urlString);
            var constructed = Constructed(url: parsedUri);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedUri, x => Path(x)?.Url, expectParsingError ? 1 : 0);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        [TestCase("Bad Date", true)]
        [TestCase("1/1/0000", true)]
        [TestCase("12/31/10000", true)]
        public void StartDateTest(string startDate, bool expectParsingError = false)
        {
            DateTime? parsedDate = DateTime.TryParse(startDate, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(startDate: startDate);
            var constructed = Constructed(startDate: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.StartDate, expectParsingError ? 1 : 0);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        [TestCase("Bad Date", true)]
        [TestCase("1/1/0000", true)]
        [TestCase("12/31/10000", true)]
        public void EndDateTest(string endDate, bool expectParsingError = false)
        {
            DateTime? parsedDate = DateTime.TryParse(endDate, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(endDate: endDate);
            var constructed = Constructed(endDate: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.EndDate, expectParsingError ? 1 : 0);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("An overview of your responsibilities at the company")]
        public void SummaryTest(string summary)
        {
            var fromJson = FromJson(summary: summary);
            var constructed = Constructed(summary: summary);
            Utils.ValidatePropertyPair(fromJson, constructed, summary, x => Path(x)?.Summary);
        }

        [TestCase(null)]
        [TestCase(new object[] { new string[] { } })]
        [TestCase(new object[] { new string[] { "Increased profits by 20% from 2011-2012 through viral advertising" } })]
        [TestCase(new object[] { new string[] { "Increased profits by 20% from 2011-2012 through viral advertising", "Also did other stuff" } })]
        public void HighlightsTest(string[] highlights)
        {
            var highlightList = highlights == null ? new List<string>() : new List<string>(highlights);

            var fromJson = FromJson(highlights: highlights);
            var constructed = Constructed(highlights: highlightList);
            Utils.ValidatePropertyPair(fromJson, constructed, highlightList, x => Path(x)?.Highlights);
        }
    }
}
