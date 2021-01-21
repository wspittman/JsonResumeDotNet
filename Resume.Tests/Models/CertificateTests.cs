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

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        public void DateTest(string dateString)
        {
            DateTime? parsedDate = DateTime.TryParse(dateString, out DateTime parsed) ? parsed : (DateTime?)null;

            var certificateFromJson = FromJson(date: dateString);
            var certificateConstructed = new Certificate() { Date = parsedDate };
            Utils.ValidatePropertyPair(certificateFromJson, certificateConstructed, parsedDate, x => (x as Certificate).Date);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("http://example.com")]
        public void UrlTest(string urlString)
        {
            // Match deserialization corner cases by using UriTypeConverter instead of Uri.TryCreate 
            var parsedUri = urlString == null ? null : new UriTypeConverter().ConvertFromString(urlString) as Uri;

            var certificateFromJson = FromJson(url: urlString);
            var certificateConstructed = new Certificate() { Url = parsedUri };
            Utils.ValidatePropertyPair(certificateFromJson, certificateConstructed, parsedUri, x => (x as Certificate).Url);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("CNCF")]
        public void IssuerTest(string issuer)
        {
            var certificateFromJson = FromJson(issuer: issuer);
            var certificateConstructed = new Certificate() { Issuer = issuer };
            Utils.ValidatePropertyPair(certificateFromJson, certificateConstructed, issuer, x => (x as Certificate).Issuer);
        }
    }
}
