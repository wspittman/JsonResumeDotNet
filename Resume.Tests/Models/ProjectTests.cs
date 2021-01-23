using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class ProjectTests
    {
        private const string FormatStringEmpty = "{{ \"projects\": [ {0} ] }}";
        private const string FormatString = "{{ \"projects\": [ {{ \"name\": {0}, \"description\": {1}, \"highlights\": {2}, \"keywords\": {3}, \"startDate\": {4}, \"endDate\": {5}, \"url\": {6}, \"roles\": {7}, \"entity\": {8}, \"type\": {9} }} ] }}";

        private Project Path(Resume resume) => resume.Projects.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string name = null, string description = null, string[] highlights = null, string[] keywords = null, string startDate = null, string endDate = null, string url = null, string[] roles = null, string entity = null, string type = null)
        {
            return Utils.FromJson(FormatString, name, description, highlights, keywords, startDate, endDate, url, roles, entity, type);
        }

        private Resume Constructed(string name = null, string description = null, List<string> highlights = null, List<string> keywords = null, DateTime? startDate = null, DateTime? endDate = null, Uri url = null, List<string> roles = null, string entity = null, string type = null)
        {
            var project = new Project();

            // Set values separately from object initialization to preserve any default values.
            if (name != null) project.Name = name;
            if (description != null) project.Description = description;
            if (highlights != null) project.Highlights = highlights;
            if (keywords != null) project.Keywords = keywords;
            if (startDate != null) project.StartDate = startDate;
            if (endDate != null) project.EndDate = endDate;
            if (url != null) project.Url = url;
            if (roles != null) project.Roles = roles;
            if (entity != null) project.Entity = entity;
            if (type != null) project.Type = type;

            return new Resume() { Projects = new List<Project>() { project } };
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
        [TestCase("Collated works of 2017")]
        public void DescriptionTest(string description)
        {
            var fromJson = FromJson(description: description);
            var constructed = Constructed(description: description);
            Utils.ValidatePropertyPair(fromJson, constructed, description, x => Path(x)?.Description);
        }

        [TestCase(null)]
        [TestCase(new object[] { new string[] { } })]
        [TestCase(new object[] { new string[] { "Directs you close but not quite there" } })]
        [TestCase(new object[] { new string[] { "Directs you close but not quite there", "Also did other stuff" } })]
        public void HighlightsTest(string[] highlights)
        {
            var highlightList = highlights == null ? new List<string>() : new List<string>(highlights);

            var fromJson = FromJson(highlights: highlights);
            var constructed = Constructed(highlights: highlightList);
            Utils.ValidatePropertyPair(fromJson, constructed, highlightList, x => Path(x)?.Highlights);
        }

        [TestCase(null)]
        [TestCase(new object[] { new string[] { } })]
        [TestCase(new object[] { new string[] { "AngularJS" } })]
        [TestCase(new object[] { new string[] { "AngularJS", "HTML" } })]
        public void KeywordsTest(string[] keywords)
        {
            var keywordList = keywords == null ? new List<string>() : new List<string>(keywords);

            var fromJson = FromJson(keywords: keywords);
            var constructed = Constructed(keywords: keywordList);
            Utils.ValidatePropertyPair(fromJson, constructed, keywordList, x => Path(x)?.Keywords);
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
        [TestCase("http://www.computer.org/csdl/mags/co/1996/10/rx069-abs.html")]
        public void UrlTest(string urlString)
        {
            // Match deserialization corner cases by using UriTypeConverter instead of Uri.TryCreate 
            var parsedUri = urlString == null ? null : new UriTypeConverter().ConvertFromString(urlString) as Uri;

            var fromJson = FromJson(url: urlString);
            var constructed = Constructed(url: parsedUri);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedUri, x => Path(x)?.Url);
        }

        [TestCase(null)]
        [TestCase(new object[] { new string[] { } })]
        [TestCase(new object[] { new string[] { "Team Lead" } })]
        [TestCase(new object[] { new string[] { "Team Lead", "Speaker" } })]
        public void RolesTest(string[] roles)
        {
            var roleList = roles == null ? new List<string>() : new List<string>(roles);

            var fromJson = FromJson(roles: roles);
            var constructed = Constructed(roles: roleList);
            Utils.ValidatePropertyPair(fromJson, constructed, roleList, x => Path(x)?.Roles);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("greenpeace")]
        public void EntityTest(string entity)
        {
            var fromJson = FromJson(entity: entity);
            var constructed = Constructed(entity: entity);
            Utils.ValidatePropertyPair(fromJson, constructed, entity, x => Path(x)?.Entity);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("volunteering")]
        public void Type(string type)
        {
            var fromJson = FromJson(type: type);
            var constructed = Constructed(type: type);
            Utils.ValidatePropertyPair(fromJson, constructed, type, x => Path(x)?.Type);
        }
    }
}
