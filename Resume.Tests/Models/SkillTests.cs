using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class SkillTests
    {
        private const string FormatStringEmpty = "{{\"skills\": [{0}]}}";
        private const string FormatString = "{{\"skills\": [{{\"name\": {0}, \"level\": {1}, \"keywords\": {2}, \"extra\": {3}}}]}}";

        private Skill Path(Resume resume) => resume.Skills.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string name = null, string level = null, string[] keywords = null, string extra = null)
        {
            return Utils.FromJson(FormatString, name, level, keywords, extra);
        }

        private Resume Constructed(string name = null, string level = null, List<string> keywords = null)
        {
            var skill = new Skill();

            // Set values separately from object initialization to preserve any default values.
            if (name != null) skill.Name = name;
            if (level != null) skill.Level = level;
            if (keywords != null) skill.Keywords = keywords;

            return new Resume() { Skills = new List<Skill>() { skill } };
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
        [TestCase("Web Development")]
        public void NameTest(string name)
        {
            var fromJson = FromJson(name: name);
            var constructed = Constructed(name: name);
            Utils.ValidatePropertyPair(fromJson, constructed, name, x => Path(x)?.Name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Master")]
        public void LevelTest(string level)
        {
            var fromJson = FromJson(level: level);
            var constructed = Constructed(level: level);
            Utils.ValidatePropertyPair(fromJson, constructed, level, x => Path(x)?.Level);
        }

        [TestCase(null)]
        [TestCase(new object[] { new string[] { } })]
        [TestCase(new object[] { new string[] { "HTML" } })]
        [TestCase(new object[] { new string[] { "HTML", "CSS" } })]
        public void KeywordsTest(string[] keywords)
        {
            var keywordList = keywords == null ? new List<string>() : new List<string>(keywords);

            var fromJson = FromJson(keywords: keywords);
            var constructed = Constructed(keywords: keywordList);
            Utils.ValidatePropertyPair(fromJson, constructed, keywordList, x => Path(x)?.Keywords);
        }
    }
}
