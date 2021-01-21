using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;

namespace Resume.Tests
{
    public class CertificateTests
    {
        private const string FormatStringEmpty = "{{\"certificates\": [{0}]}}";
        private const string FormatString = "{{\"certificates\": [{{\"name\": {0}, \"date\": {1}, \"url\": {2}, \"issuer\": {3}, \"extra\": {4}}}]}}";

        private Certificate FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg).Certificates.FirstOrDefault();

        private Certificate FromJson(string name = null, string date = null, string url = null, string issuer = null, string extra = null)
        {
            return Utils.FromJson(FormatString, name, date, url, issuer, extra).Certificates.FirstOrDefault();
        }

        [Test]
        public void EmptyTest()
        {
            var certificateNull = FromJsonEmpty(null);
            var certificateEmpty = FromJsonEmpty("{}");
            var certificateConstructed = new Certificate();

            Assert.AreEqual("null", JsonConvert.SerializeObject(certificateNull));
            Assert.AreEqual(JsonConvert.SerializeObject(certificateEmpty), JsonConvert.SerializeObject(certificateConstructed));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Certified Kubernetes Administrator")]
        public void NameTest(string name)
        {
            var certificateFromJson = FromJson(name: name);
            var certificateConstructed = new Certificate() { Name = name };
            Utils.ValidatePropertyPair(certificateFromJson, certificateConstructed, name, x => (x as Certificate).Name);
        }
    }
}
