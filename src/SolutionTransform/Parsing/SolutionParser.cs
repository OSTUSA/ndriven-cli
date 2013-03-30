using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
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
                var matches = ProjectPattern.Matches(reader.ReadToEnd());
                foreach (var project in from Match match in matches select new Project(match.Groups["name"].Value, match.Groups["path"].Value))
                {
                    if (!project.Path.Contains("proj"))
                        project.IsSolutionFolder = true;
                    solution.Projects.Add(project);
                } 
            }
            SetProjectAssemblyAndNamespace(path, solution.Projects);
        }

        private static void SetProjectAssemblyAndNamespace(string path, IEnumerable<Project> projects)
        {
            var slnDir = new FileInfo(path).Directory;
            foreach (var p in projects)
            {
                var csproj = slnDir.FullName + Path.DirectorySeparatorChar + p.Path;
                if (!File.Exists(csproj)) continue;
                SetAssemblyAndNamsepace(csproj, p);
            }
        }

        private static void SetAssemblyAndNamsepace(string projPath, Project project)
        {
            var data = XDocument.Load(projPath).Descendants()
                .Descendants()
                .Where(d => d.Name.LocalName == "PropertyGroup")
                .Descendants()
                .Where(x => x.Name.LocalName == "AssemblyName" || x.Name.LocalName == "RootNamespace")
                .Select(d => d.Value);
            project.RootNamespace = data.ElementAt(0).ToString();
            project.AssemblyName = data.ElementAt(1).ToString();
        }
    }
}
