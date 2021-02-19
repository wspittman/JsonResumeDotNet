using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Resume.Tests
{
    public class ResumeTests
    {
        private const string DirectoryName = "SampleJson";
        private static string shallowNulls;
        private static string deepNulls;
        private static string unknownMembers;
        private static string sample;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            shallowNulls = File.ReadAllText(Path.Combine(DirectoryName, "shallowNulls.json"));
            deepNulls = File.ReadAllText(Path.Combine(DirectoryName, "deepNulls.json"));
            unknownMembers = File.ReadAllText(Path.Combine(DirectoryName, "unknownMembers.json"));
            sample = File.ReadAllText(Path.Combine(DirectoryName, "sample.json"));
        }

        [Test]
        public void EmptyTest([Values] bool throwOnError, [Values] bool errorOnUnknownMember)
        {
            var fromNull = Resume.FromJson(null, throwOnError, errorOnUnknownMember);
            var fromEmptyString = Resume.FromJson(string.Empty, throwOnError, errorOnUnknownMember);
            var fromEmptyJson = Resume.FromJson("{}", throwOnError, errorOnUnknownMember);
            var constructed = new Resume();

            Utils.ValidateResume(fromNull, constructed);
            Utils.ValidateResume(fromEmptyString, constructed);
            Utils.ValidateResume(fromEmptyJson, constructed);
        }

        [Test]
        public void ShallowNullsTest([Values] bool throwOnError, [Values] bool errorOnUnknownMember)
        {
            var fromJson = Resume.FromJson(shallowNulls, throwOnError, errorOnUnknownMember);
            var constructed = new Resume();

            Utils.ValidateResume(fromJson, constructed);
        }

        [Test]
        public void DeepNullsTest([Values] bool throwOnError, [Values] bool errorOnUnknownMember)
        {
            var fromJson = Resume.FromJson(deepNulls, throwOnError, errorOnUnknownMember);
            var stringList = new List<string>() { null, null };

            var constructed = new Resume()
            {
                Basics = new Basics()
                {
                    Location = new Location(),
                    Profiles = new List<Profile>() { new Profile(), new Profile() },
                },
                Work = new List<Work>() { new Work() { Highlights = stringList }, new Work() { Highlights = stringList } },
                Volunteer = new List<Volunteer>() { new Volunteer() { Highlights = stringList }, new Volunteer() { Highlights = stringList } },
                Education = new List<Education>() { new Education() { Courses = stringList }, new Education() { Courses = stringList } },
                Awards = new List<Award>() { new Award(), new Award() },
                Certificates = new List<Certificate>() { new Certificate(), new Certificate() },
                Publications = new List<Publication>() { new Publication(), new Publication() },
                Skills = new List<Skill>() { new Skill() { Keywords = stringList }, new Skill() { Keywords = stringList } },
                Languages = new List<LanguageInfo>() { new LanguageInfo(), new LanguageInfo() },
                Interests = new List<Interest>() { new Interest() { Keywords = stringList }, new Interest() { Keywords = stringList } },
                References = new List<ReferenceInfo>() { new ReferenceInfo(), new ReferenceInfo() },
                Projects = new List<Project>() { new Project() { Highlights = stringList, Keywords = stringList, Roles = stringList }, new Project() { Highlights = stringList, Keywords = stringList, Roles = stringList } },
            };

            Utils.ValidateResume(fromJson, constructed);
        }

        [Test]
        public void UnknownMembersTest([Values] bool throwOnError, [Values] bool errorOnUnknownMember)
        {
            Resume fromJson;

            try
            {
                fromJson = Resume.FromJson(unknownMembers, throwOnError, errorOnUnknownMember);
            }
            catch (JsonSerializationException)
            {
                Assert.IsTrue(errorOnUnknownMember, "ErrorOnUnknownMember = false, but JsonSerializationException exception thrown");
                Assert.IsTrue(throwOnError, "ThrowOnError = false, but JsonSerializationException exception thrown");
                return;
            }

            var constructed = new Resume()
            {
                Basics = new Basics()
                {
                    Location = new Location(),
                    Profiles = new List<Profile>() { new Profile() }
                },
                Work = new List<Work>() { new Work() },
                Volunteer = new List<Volunteer>() { new Volunteer() },
                Education = new List<Education>() { new Education() },
                Awards = new List<Award>() { new Award() },
                Certificates = new List<Certificate>() { new Certificate() },
                Publications = new List<Publication>() { new Publication() },
                Skills = new List<Skill>() { new Skill() },
                Languages = new List<LanguageInfo>() { new LanguageInfo() },
                Interests = new List<Interest>() { new Interest() },
                References = new List<ReferenceInfo>() { new ReferenceInfo() },
                Projects = new List<Project>() { new Project() }
            };

            Utils.ValidateResume(fromJson, constructed, errorOnUnknownMember ? 14 : 0);
        }

        [Test]
        public void SampleTest([Values] bool throwOnError, [Values] bool errorOnUnknownMember)
        {
            var fromJson = Resume.FromJson(sample, throwOnError, errorOnUnknownMember);

            // Yeah, I'm not recreating the sample by hand here, we'll just reparse and compare for now
            var reparse = JObject.Parse(sample).ToObject<Resume>();

            Utils.ValidateResume(fromJson, reparse);
        }
    }
}