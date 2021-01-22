using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class LanguageInfoTests
    {
        private const string FormatStringEmpty = "{{\"languages\": [{0}]}}";
        private const string FormatString = "{{\"languages\": [{{\"language\": {0}, \"fluency\": {1}, \"extra\": {2}}}]}}";

        private LanguageInfo Path(Resume resume) => resume.Languages.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string language = null, string fluency = null, string extra = null)
        {
            return Utils.FromJson(FormatString, language, fluency, extra);
        }

        private Resume Constructed(string language = null, string fluency = null)
        {
            var languageInfo = new LanguageInfo();

            // Set values separately from object initialization to preserve any default values.
            if (language != null) languageInfo.Language = language;
            if (fluency != null) languageInfo.Fluency = fluency;

            return new Resume() { Languages = new List<LanguageInfo>() { languageInfo } };
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
        [TestCase("English")]
        public void LanguageTest(string language)
        {
            var fromJson = FromJson(language: language);
            var constructed = Constructed(language: language);
            Utils.ValidatePropertyPair(fromJson, constructed, language, x => Path(x)?.Language);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Fluent")]
        public void FluencyTest(string fluency)
        {
            var fromJson = FromJson(fluency: fluency);
            var constructed = Constructed(fluency: fluency);
            Utils.ValidatePropertyPair(fromJson, constructed, fluency, x => Path(x)?.Fluency);
        }
    }
}
