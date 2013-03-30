using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SolutionTransform.Model;

namespace SolutionTransform.Parsing
{
    public class SolutionParser
    {
        private static readonly Regex ProjectPattern = new Regex(@"Project\(.*\)\s+=\s+""(?<name>[^,]*)"",\s+""(?<path>[^,]*)"",", RegexOptions.Compiled | RegexOptions.Multiline); 

        public static Solution Parse(string path)
        {
            var parser = new SolutionParser();
            return parser.ParseFile(path);
        }

        public Solution ParseFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Solution file not found");

            var solution = new Solution(Path.GetFileName(path).Replace(".sln", ""));
            ParseProjects(path, solution);
            return solution;
        }

        private static void ParseProjects(string path, Solution solution)
        {
            using (var reader = new StreamReader(path))
            {
                var contents = reader.ReadToEnd();
                var matches = ProjectPattern.Matches(contents);
                foreach (var project in from Match match in matches select new Project(match.Groups["name"].Value, match.Groups["path"].Value))
                    solution.Projects.Add(project);
            }
        }
    }
}
