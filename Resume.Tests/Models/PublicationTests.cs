﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonResume.Tests
{
    public class PublicationTests
    {
        private const string FormatStringEmpty = "{{ \"publications\": [ {0} ] }}";
        private const string FormatString = "{{ \"publications\": [ {{ \"name\": {0}, \"publisher\": {1}, \"releaseDate\": {2}, \"url\": {3}, \"summary\": {4} }} ] }}";

        private Publication Path(Resume resume) => resume.Publications.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string name = null, string publisher = null, string releaseDate = null, string url = null, string summary = null)
        {
            return Utils.FromJson(FormatString, name, publisher, releaseDate, url, summary);
        }

        private Resume Constructed(string name = null, string publisher = null, DateTime? releaseDate = null, Uri url = null, string summary = null)
        {
            var publication = new Publication();

            // Set values separately from object initialization to preserve any default values.
            if (name != null) publication.Name = name;
            if (publisher != null) publication.Publisher = publisher;
            if (releaseDate != null) publication.ReleaseDate = releaseDate;
            if (url != null) publication.Url = url;
            if (summary != null) publication.Summary = summary;

            return new Resume() { Publications = new List<Publication>() { publication } };
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
        [TestCase("The World Wide Web")]
        public void NameTest(string name)
        {
            var fromJson = FromJson(name: name);
            var constructed = Constructed(name: name);
            Utils.ValidatePropertyPair(fromJson, constructed, name, x => Path(x)?.Name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("IEEE, Computer Magazine")]
        public void PublisherTest(string publisher)
        {
            var fromJson = FromJson(publisher: publisher);
            var constructed = Constructed(publisher: publisher);
            Utils.ValidatePropertyPair(fromJson, constructed, publisher, x => Path(x)?.Publisher);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        [TestCase("Bad Date", true)]
        [TestCase("1/1/0000", true)]
        [TestCase("12/31/10000", true)]
        public void ReleaseDateTest(string dateString, bool expectParsingError = false)
        {
            DateTime? parsedDate = DateTime.TryParse(dateString, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(releaseDate: dateString);
            var constructed = Constructed(releaseDate: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.ReleaseDate, expectParsingError ? 1 : 0);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("http://www.computer.org.example.com/csdl/mags/co/1996/10/rx069-abs.html")]
        [TestCase("www.computer.org.example.com/csdl/mags/co/1996/10/rx069-abs.html", true)]
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
        [TestCase("Discussion of the World Wide Web, HTTP, HTML.")]
        public void SummaryTest(string summary)
        {
            var fromJson = FromJson(summary: summary);
            var constructed = Constructed(summary: summary);
            Utils.ValidatePropertyPair(fromJson, constructed, summary, x => Path(x)?.Summary);
        }
    }
}
