namespace SolutionTransform.Model
{
    public class Project
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string RootNamespace { get; set; }
        public string AssemblyName { get; set; }

        public Project(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
