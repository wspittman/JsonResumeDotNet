using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class ProfileTests
    {
        private const string FormatStringEmpty = "{{ \"basics\": {{ \"profiles\": [ {0} ] }} }}";
        private const string FormatString = "{{ \"basics\": {{ \"profiles\": [ {{ \"network\": {0}, \"username\": {1}, \"url\": {2} }} ] }} }}";

        private Profile Path(Resume resume) => resume.Basics?.Profiles.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string network = null, string username = null, string url = null)
        {
            return Utils.FromJson(FormatString, network, username, url);
        }

        private Resume Constructed(string network = null, string username = null, Uri url = null)
        {
            var profile = new Profile();

            // Set values separately from object initialization to preserve any default values.
            if (network != null) profile.Network = network;
            if (username != null) profile.Username = username;
            if (url != null) profile.Url = url;

            return new Resume() { Basics = new Basics() { Profiles = new List<Profile>() { profile } } };
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
        [TestCase("Twitter")]
        public void NetworkTest(string network)
        {
            var fromJson = FromJson(network: network);
            var constructed = Constructed(network: network);
            Utils.ValidatePropertyPair(fromJson, constructed, network, x => Path(x)?.Network);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("neutralthoughts")]
        public void UsernameTest(string username)
        {
            var fromJson = FromJson(username: username);
            var constructed = Constructed(username: username);
            Utils.ValidatePropertyPair(fromJson, constructed, username, x => Path(x)?.Username);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("http://twitter.example.com/neutralthoughts")]
        [TestCase("twitter.example.com/neutralthoughts", true)]
        [TestCase("Clearly not a url", true)]
        public void UrlTest(string urlString, bool expectParsingError = false)
        {
            Uri.TryCreate(urlString, UriKind.Absolute, out Uri parsedUri);

            var fromJson = FromJson(url: urlString);
            var constructed = Constructed(url: parsedUri);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedUri, x => Path(x)?.Url, expectParsingError ? 1 : 0);
        }
    }
}
