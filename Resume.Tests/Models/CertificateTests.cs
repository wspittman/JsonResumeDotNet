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
            var fromNull = FromJsonEmpty(null);
            var fromEmpty = FromJsonEmpty("{}");
            var constructed = new Certificate();

            Assert.AreEqual("null", JsonConvert.SerializeObject(fromNull));
            Assert.AreEqual(JsonConvert.SerializeObject(fromEmpty), JsonConvert.SerializeObject(constructed));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Certified Kubernetes Administrator")]
        public void NameTest(string name)
        {
            var fromJson = FromJson(name: name);
            var constructed = new Certificate() { Name = name };
            Utils.ValidatePropertyPair(fromJson, constructed, name, x => (x as Certificate).Name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        public void DateTest(string dateString)
        {
            DateTime? parsedDate = DateTime.TryParse(dateString, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(date: dateString);
            var constructed = new Certificate() { Date = parsedDate };
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => (x as Certificate).Date);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("http://example.com")]
        public void UrlTest(string urlString)
        {
            // Match deserialization corner cases by using UriTypeConverter instead of Uri.TryCreate 
            var parsedUri = urlString == null ? null : new UriTypeConverter().ConvertFromString(urlString) as Uri;

            var fromJson = FromJson(url: urlString);
            var constructed = new Certificate() { Url = parsedUri };
            Utils.ValidatePropertyPair(fromJson, constructed, parsedUri, x => (x as Certificate).Url);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("CNCF")]
        public void IssuerTest(string issuer)
        {
            var fromJson = FromJson(issuer: issuer);
            var constructed = new Certificate() { Issuer = issuer };
            Utils.ValidatePropertyPair(fromJson, constructed, issuer, x => (x as Certificate).Issuer);
        }
    }
}
