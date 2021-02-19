using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace JsonResume.Tests
{
    public class InterestTests
    {
        private const string FormatStringEmpty = "{{ \"interests\": [ {0} ] }}";
        private const string FormatString = "{{ \"interests\": [ {{ \"name\": {0}, \"keywords\": {1} }} ] }}";

        private Interest Path(Resume resume) => resume.Interests.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string name = null, string[] keywords = null)
        {
            return Utils.FromJson(FormatString, name, keywords);
        }

        private Resume Constructed(string name = null, List<string> keywords = null)
        {
            var interest = new Interest();

            // Set values separately from object initialization to preserve any default values.
            if (name != null) interest.Name = name;
            if (keywords != null) interest.Keywords = keywords;

            return new Resume() { Interests = new List<Interest>() { interest } };
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
        [TestCase("Philosophy")]
        public void NameTest(string name)
        {
            var fromJson = FromJson(name: name);
            var constructed = Constructed(name: name);
            Utils.ValidatePropertyPair(fromJson, constructed, name, x => Path(x)?.Name);
        }

        [TestCase(null)]
        [TestCase(new object[] { new string[] { } })]
        [TestCase(new object[] { new string[] { "Friedrich Nietzsche" } })]
        [TestCase(new object[] { new string[] { "Friedrich Nietzsche", "Arthur Schopenhauer" } })]
        public void KeywordsTest(string[] keywords)
        {
            var keywordList = keywords == null ? new List<string>() : new List<string>(keywords);

            var fromJson = FromJson(keywords: keywords);
            var constructed = Constructed(keywords: keywordList);
            Utils.ValidatePropertyPair(fromJson, constructed, keywordList, x => Path(x)?.Keywords);
        }
    }
}
