using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Resume.Tests
{
    public class BasicsTests
    {
        private const string FormatStringEmpty = "{{ \"basics\": {0} }}";
        private const string FormatString = "{{ \"basics\": {{ \"name\": {0}, \"label\": {1}, \"image\": {2}, \"email\": {3}, \"phone\": {4}, \"url\": {5}, \"summary\": {6}, \"location\": {7}, \"profiles\": {8} }} }}";

        private Basics Path(Resume resume) => resume.Basics;

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string name = null, string label = null, string image = null, string email = null, string phone = null, string url = null, string summary = null, string location = null, string[] profiles = null)
        {
            return Utils.FromJson(FormatString, name, label, image, email, phone, url, summary, location, profiles);
        }

        private Resume Constructed(string name = null, string label = null, Uri image = null, string email = null, string phone = null, Uri url = null, string summary = null, Location location = null, List<Profile> profiles = null)
        {
            var basics = new Basics();

            // Set values separately from object initialization to preserve any default values.
            if (name != null) basics.Name = name;
            if (label != null) basics.Label = label;
            if (image != null) basics.Image = image;
            if (email != null) basics.Email = email;
            if (phone != null) basics.Phone = phone;
            if (url != null) basics.Url = url;
            if (summary != null) basics.Summary = summary;
            if (location != null) basics.Location = location;
            if (profiles != null) basics.Profiles = profiles;

            return new Resume() { Basics = basics };
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
        [TestCase("Richard Hendriks")]
        public void NameTest(string name)
        {
            var fromJson = FromJson(name: name);
            var constructed = Constructed(name: name);
            Utils.ValidatePropertyPair(fromJson, constructed, name, x => Path(x)?.Name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Web Developer")]
        public void LabelTest(string label)
        {
            var fromJson = FromJson(label: label);
            var constructed = Constructed(label: label);
            Utils.ValidatePropertyPair(fromJson, constructed, label, x => Path(x)?.Label);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("https://upload.wikimedia.org/wikipedia/commons/a/a9/Example.jpg")]
        [TestCase("upload.wikimedia.org/wikipedia/commons/a/a9/Example.jpg", true)]
        [TestCase("Clearly not a url", true)]
        public void ImageTest(string imageString, bool expectParsingError = false)
        {
            Uri.TryCreate(imageString, UriKind.Absolute, out Uri parsedUri);

            var fromJson = FromJson(image: imageString);
            var constructed = Constructed(image: parsedUri);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedUri, x => Path(x)?.Image, expectParsingError ? 1 : 0);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("thomas@gmail.com")]
        [TestCase("Clearly not an email address", true)]
        public void EmailTest(string email, bool expectParsingError = false)
        {
            string expected = string.IsNullOrEmpty(email) || expectParsingError ? null : email;

            var fromJson = FromJson(email: email);
            var constructed = Constructed(email: expected);
            Utils.ValidatePropertyPair(fromJson, constructed, expected, x => Path(x)?.Email, expectParsingError ? 1 : 0);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("712-117-2923")]
        public void PhoneTest(string phone)
        {
            var fromJson = FromJson(phone: phone);
            var constructed = Constructed(phone: phone);
            Utils.ValidatePropertyPair(fromJson, constructed, phone, x => Path(x)?.Phone);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("http://facebook.example.com")]
        [TestCase("facebook.example.com", true)]
        [TestCase("Clearly not a url", true)]
        public void UrlTest(string urlString, bool expectParsingError = false)
        {
            Uri.TryCreate(urlString, UriKind.Absolute, out Uri parsedUri);

            var fromJson = FromJson(url: urlString);
            var constructed = Constructed(url: parsedUri);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedUri, x => Path(x)?.Url, expectParsingError ? 1 : 0);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("A short 2-3 sentence biography about yourself")]
        public void SummaryTest(string summary)
        {
            var fromJson = FromJson(summary: summary);
            var constructed = Constructed(summary: summary);
            Utils.ValidatePropertyPair(fromJson, constructed, summary, x => Path(x)?.Summary);
        }

        // Location tested in LocationTests

        // Profiles tested in ProfileTests
    }
}
