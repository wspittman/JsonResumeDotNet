using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class VolunteerTests
    {
        private const string FormatStringEmpty = "{{\"volunteer\": [{0}]}}";
        private const string FormatString = "{{\"volunteer\": [{{\"organization\": {0}, \"position\": {1}, \"url\": {2}, \"startDate\": {3}, \"endDate\": {4}, \"summary\": {5}, \"highlights\": {6}, \"extra\": {7}}}]}}";

        private Volunteer Path(Resume resume) => resume.Volunteer.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string organization = null, string position = null, string url = null, string startDate = null, string endDate = null, string summary = null, string[] highlights = null, string extra = null)
        {
            return Utils.FromJson(FormatString, organization, position, url, startDate, endDate, summary, highlights, extra);
        }

        private Resume Constructed(string organization = null, string position = null, Uri url = null, DateTime? startDate = null, DateTime? endDate = null, string summary = null, List<string> highlights = null)
        {
            var volunteer = new Volunteer();

            // Set values separately from object initialization to preserve any default values.
            if (organization != null) volunteer.Organization = organization;
            if (position != null) volunteer.Position = position;
            if (url != null) volunteer.Url = url;
            if (startDate != null) volunteer.StartDate = startDate;
            if (endDate != null) volunteer.EndDate = endDate;
            if (summary != null) volunteer.Summary = summary;
            if (highlights != null) volunteer.Highlights = highlights;

            return new Resume() { Volunteer = new List<Volunteer>() { volunteer } };
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
        public void OrganizationTest(string organization)
        {
            var fromJson = FromJson(organization: organization);
            var constructed = Constructed(organization: organization);
            Utils.ValidatePropertyPair(fromJson, constructed, organization, x => Path(x)?.Organization);
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
        public void UrlTest(string urlString)
        {
            // Match deserialization corner cases by using UriTypeConverter instead of Uri.TryCreate 
            var parsedUri = urlString == null ? null : new UriTypeConverter().ConvertFromString(urlString) as Uri;

            var fromJson = FromJson(url: urlString);
            var constructed = Constructed(url: parsedUri);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedUri, x => Path(x)?.Url);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        public void StartDateTest(string startDate)
        {
            DateTime? parsedDate = DateTime.TryParse(startDate, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(startDate: startDate);
            var constructed = Constructed(startDate: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.StartDate);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        public void EndDateTest(string endDate)
        {
            DateTime? parsedDate = DateTime.TryParse(endDate, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(endDate: endDate);
            var constructed = Constructed(endDate: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.EndDate);
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
