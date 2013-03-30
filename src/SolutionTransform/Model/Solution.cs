using System.Collections.Generic;

namespace SolutionTransform.Model
{
    public class Solution
    {
        public string Name { get; set; }
        public List<Project> Projects { get; set; }

        public Solution(string name)
        {
            Name = name;
            Projects = new List<Project>();
        }
    }
}
