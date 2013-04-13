using System.IO;
using NUnit.Framework;
using ProjectExtractor;

namespace Test.Unit.ProjectExtractor
{
    [TestFixture]
    public class ExtractorTest
    {
        protected string ZipPath { get; set; }
        protected Extractor Extractor { get; set; }

        [SetUp]
        public void SetUp()
        {
            var fixturesPath = Path.GetFullPath("Fixtures");
            ZipPath = fixturesPath + Path.DirectorySeparatorChar + "ndriven-master.zip";
            Extractor = new Extractor(ZipPath);
        }

        [Test]
        [ExpectedException(typeof (FileNotFoundException))]
        public void Extractor_throws_exception_if_zip_file_does_not_exist()
        {
            var extractor = new Extractor("nope");
        }

        [Test]
        public void Constructor_should_set_zip_path()
        {
            Assert.AreEqual(ZipPath, Extractor.ZipPath);
        }
    }
}
