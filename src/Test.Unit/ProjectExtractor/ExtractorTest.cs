using System.IO;
using System.Linq;
using System.Security.AccessControl;
using NUnit.Framework;
using ProjectExtractor;

namespace Test.Unit.ProjectExtractor
{
    [TestFixture]
    public class ExtractorTest
    {
        protected string ZipPath { get; set; }
        protected Extractor Extractor { get; set; }
        protected string To = Path.GetFullPath("Fixtures") + Path.DirectorySeparatorChar + "testlocation";

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

        [Test]
        public void Extract_should_extract_folder_and_rename_it()
        {
            Extractor.Extract(To);
            Assert.True(Directory.Exists(To));
        }

        [Test]
        public void Extract_should_put_zip_contents_into_destination_path()
        {
            var info = new DirectoryInfo(To);
            var di = info.GetDirectories().First();
            Assert.AreNotEqual(di.Name, "ndriven-master");
        }
    }
}
