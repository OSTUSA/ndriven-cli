using System.IO;
using NUnit.Framework;
using SolutionTransform.Parsing;

namespace Test.Unit.SolutionTransform.Parsing
{
    [TestFixture]
    public class SolutionParserTest
    {
        protected string SolutionPath { get; set; }
        protected SolutionParser Parser { get; set; }

        [SetUp]
        public void SetUp()
        {
            var fixturesPath = Path.GetFullPath("Fixtures");
            SolutionPath = fixturesPath + Path.DirectorySeparatorChar + "NDriven.sln";
            Parser = new SolutionParser();
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ParseFile_throws_exception_if_path_doesnt_exist()
        {
            Parser.ParseFile("nope.sln");
        }

        [Test]
        public void ParseFile_sets_name_on_returned_solution()
        {
            var solution = Parser.ParseFile(SolutionPath);
            Assert.AreEqual("NDriven", solution.Name);
        }

        [Test]
        public void ParseFile_populates_list_of_projects()
        {
            var solution = Parser.ParseFile(SolutionPath);
            Assert.IsTrue(solution.Projects.Count > 0);
        }

        [Test]
        public void ParseFile_sets_assembly_and_namespace_info_on_Projects()
        {
            var solution = Parser.ParseFile(SolutionPath);
            foreach (var project in solution.Projects)
            {
                Assert.IsNotNull(project.AssemblyName);
                Assert.IsNotNull(project.RootNamespace);
            }
        }
    }
}
