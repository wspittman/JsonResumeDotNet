using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resume.Tests
{
    public class EducationTests
    {
        private const string FormatStringEmpty = "{{ \"education\": [ {0} ] }}";
        private const string FormatString = "{{ \"education\": [ {{ \"institution\": {0}, \"url\": {1}, \"area\": {2}, \"studyType\": {3}, \"startDate\": {4}, \"endDate\": {5}, \"score\": {6}, \"courses\": {7} }} ] }}";

        private Education Path(Resume resume) => resume.Education.FirstOrDefault();

        private Resume FromJsonEmpty(string arg) => Utils.FromJson(FormatStringEmpty, arg);

        private Resume FromJson(string institution = null, string url = null, string area = null, string studyType = null, string startDate = null, string endDate = null, string score = null, string[] courses = null)
        {
            return Utils.FromJson(FormatString, institution, url, area, studyType, startDate, endDate, score, courses);
        }

        private Resume Constructed(string institution = null, Uri url = null, string area = null, string studyType = null, DateTime? startDate = null, DateTime? endDate = null, string score = null, List<string> courses = null)
        {
            var education = new Education();

            // Set values separately from object initialization to preserve any default values.
            if (institution != null) education.Institution = institution;
            if (url != null) education.Url = url;
            if (area != null) education.Area = area;
            if (studyType != null) education.StudyType = studyType;
            if (startDate != null) education.StartDate = startDate;
            if (endDate != null) education.EndDate = endDate;
            if (score != null) education.Score = score;
            if (courses != null) education.Courses = courses;

            return new Resume() { Education = new List<Education>() { education } };
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
        [TestCase("Massachusetts Institute of Technology")]
        public void InstitutionTest(string institution)
        {
            var fromJson = FromJson(institution: institution);
            var constructed = Constructed(institution: institution);
            Utils.ValidatePropertyPair(fromJson, constructed, institution, x => Path(x)?.Institution);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("http://facebook.example.com")]
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
        [TestCase("Arts")]
        public void AreaTest(string area)
        {
            var fromJson = FromJson(area: area);
            var constructed = Constructed(area: area);
            Utils.ValidatePropertyPair(fromJson, constructed, area, x => Path(x)?.Area);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Bachelor")]
        public void StudyTypeTest(string studyType)
        {
            var fromJson = FromJson(studyType: studyType);
            var constructed = Constructed(studyType: studyType);
            Utils.ValidatePropertyPair(fromJson, constructed, studyType, x => Path(x)?.StudyType);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        public void StartDateTest(string startDate)
        {
            DateTime? parsedDate = DateTime.TryParse(startDate, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(startDate: startDate);
            var constructed = Constructed(startDate: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.StartDate);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("1/1/0001")]
        [TestCase("12/31/9999")]
        [TestCase("1989-06-12")]
        public void EndDateTest(string endDate)
        {
            DateTime? parsedDate = DateTime.TryParse(endDate, out DateTime parsed) ? parsed : (DateTime?)null;

            var fromJson = FromJson(endDate: endDate);
            var constructed = Constructed(endDate: parsedDate);
            Utils.ValidatePropertyPair(fromJson, constructed, parsedDate, x => Path(x)?.EndDate);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("3.67/4.0")]
        public void ScoreTest(string score)
        {
            var fromJson = FromJson(score: score);
            var constructed = Constructed(score: score);
            Utils.ValidatePropertyPair(fromJson, constructed, score, x => Path(x)?.Score);
        }

        [TestCase(null)]
        [TestCase(new object[] { new string[] { } })]
        [TestCase(new object[] { new string[] { "H1302 - Introduction to American history" } })]
        [TestCase(new object[] { new string[] { "H1302 - Introduction to American history", "CSE 143 Intro Programming II" } })]
        public void CoursesTest(string[] courses)
        {
            var courseList = courses == null ? new List<string>() : new List<string>(courses);

            var fromJson = FromJson(courses: courses);
            var constructed = Constructed(courses: courseList);
            Utils.ValidatePropertyPair(fromJson, constructed, courseList, x => Path(x)?.Courses);
        }
    }
}
