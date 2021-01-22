using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class ReferenceInfoTests
    {
        private const string FormatStringEmpty = "{{ \"references\": [ {0} ] }}";
        private const string FormatString = "{{ \"references\": [ {{ \"name\": {0}, \"reference\": {1}, \"extra\": {2} }} ] }}";

        private ReferenceInfo Path(Resume resume) => resume.References.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string name = null, string reference = null, string extra = null)
        {
            return Utils.FromJson(FormatString, name, reference, extra);
        }

        private Resume Constructed(string name = null, string reference = null)
        {
            var referenceInfo = new ReferenceInfo();

            // Set values separately from object initialization to preserve any default values.
            if (name != null) referenceInfo.Name = name;
            if (reference != null) referenceInfo.Reference = reference;

            return new Resume() { References = new List<ReferenceInfo>() { referenceInfo } };
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
        [TestCase("Timothy Cook")]
        public void NameTest(string name)
        {
            var fromJson = FromJson(name: name);
            var constructed = Constructed(name: name);
            Utils.ValidatePropertyPair(fromJson, constructed, name, x => Path(x)?.Name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Joe blogs was a great employee, who turned up to work at least once a week. He exceeded my expectations when it came to doing nothing.")]
        public void ReferenceTest(string reference)
        {
            var fromJson = FromJson(reference: reference);
            var constructed = Constructed(reference: reference);
            Utils.ValidatePropertyPair(fromJson, constructed, reference, x => Path(x)?.Reference);
        }
    }
}
