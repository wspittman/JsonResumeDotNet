using NUnit.Framework;

namespace Resume.Tests
{
    public class LocationTests
    {
        private const string FormatStringEmpty = "{{\"basics\": {{ \"location\": {0} }} }}";
        private const string FormatString = "{{\"basics\": {{ \"location\": {{ \"address\": {0}, \"postalCode\": {1}, \"city\": {2}, \"countryCode\": {3}, \"region\": {4}, \"extra\": {5} }} }} }}";

        private Location Path(Resume resume) => resume.Basics?.Location;

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string address = null, string postalCode = null, string city = null, string countryCode = null, string region = null, string extra = null)
        {
            return Utils.FromJson(FormatString, address, postalCode, city, countryCode, region, extra);
        }

        private Resume Constructed(string address = null, string postalCode = null, string city = null, string countryCode = null, string region = null)
        {
            var location = new Location();

            // Set values separately from object initialization to preserve any default values.
            if (address != null) location.Address = address;
            if (postalCode != null) location.PostalCode = postalCode;
            if (city != null) location.City = city;
            if (countryCode != null) location.CountryCode = countryCode;
            if (region != null) location.Region = region;

            return new Resume() { Basics = new Basics() { Location = location } };
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
        [TestCase("1234 Glücklichkeit Straße\nHinterhaus 5. Etage li.")]
        public void AddressTest(string address)
        {
            var fromJson = FromJson(address: address);
            var constructed = Constructed(address: address);
            Utils.ValidatePropertyPair(fromJson, constructed, address, x => Path(x)?.Address);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("98111")]
        public void PostalCodeTest(string postalCode)
        {
            var fromJson = FromJson(postalCode: postalCode);
            var constructed = Constructed(postalCode: postalCode);
            Utils.ValidatePropertyPair(fromJson, constructed, postalCode, x => Path(x)?.PostalCode);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Seattle")]
        public void CityTest(string city)
        {
            var fromJson = FromJson(city: city);
            var constructed = Constructed(city: city);
            Utils.ValidatePropertyPair(fromJson, constructed, city, x => Path(x)?.City);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("US")]
        public void CountryCodeTest(string countryCode)
        {
            var fromJson = FromJson(countryCode: countryCode);
            var constructed = Constructed(countryCode: countryCode);
            Utils.ValidatePropertyPair(fromJson, constructed, countryCode, x => Path(x)?.CountryCode);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Washington")]
        public void RegionTest(string region)
        {
            var fromJson = FromJson(region: region);
            var constructed = Constructed(region: region);
            Utils.ValidatePropertyPair(fromJson, constructed, region, x => Path(x)?.Region);
        }
    }
}
