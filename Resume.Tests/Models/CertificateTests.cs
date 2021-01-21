using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class CertificateTests
    {
        private const string FormatStringEmpty = "{{\"certificates\": [{0}]}}";
        private const string FormatString = "{{\"certificates\": [{{\"name\": {0}, \"date\": {1}, \"url\": {2}, \"issuer\": {3}, \"extra\": {4}}}]}}";
        
        private Certificate Path(Resume resume) => resume.Certificates.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string name = null, string date = null, string url = null, string issuer = null, string extra = null)
        {
            return Utils.FromJson(FormatString, name, date, url, issuer, extra);
        }

        private Resume Constructed(string name = null, DateTime? date = null, Uri url = null, string issuer = null)
        {
            var certificate = new Certificate
            {
                Name = name,
                Date = date,
                Url = url,
                Issuer = issuer
            };

            return new Resume() { Certificates = new List<Certificate>() { certificate } };
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
        [TestCase("Certified Kubernetes Administrator")]
        public void NameTest(string name)
        {
            var fromJson = FromJson(name: name);
            var constructed = Constructed(name: name);
            Utils.ValidatePropertyPair(fromJson, constructed, name, x => Path(x)?.Name);
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
            var constructed = Constructed(date: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.Date);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("http://example.com")]
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
        [TestCase("CNCF")]
        public void IssuerTest(string issuer)
        {
            var fromJson = FromJson(issuer: issuer);
            var constructed = Constructed(issuer: issuer);
            Utils.ValidatePropertyPair(fromJson, constructed, issuer, x => Path(x)?.Issuer);
        }
    }
}
