using System;
using System.IO;
using CommandLine;
using CommandLine.Text;

namespace Presentation.Console
{
    public class Options
    {
        private const string CliVersion = "0.1.3";

        [Option('s', "solution", Required = true, HelpText = "The solution name")]
        public string SolutionName { get; set; }

        [Option('d', "directory", HelpText = "The directory to extract the project to")]
        public string Directory { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("NDriven CLI", CliVersion),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage: ndriven -s MySolution -d /path/to/solution/location");
            help.AddOptions(this);
            return help;
        }
    }
}
