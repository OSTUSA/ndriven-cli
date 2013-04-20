using System;
using System.IO;
using CommandLine;
using CommandLine.Text;

namespace Presentation.Console
{
    public class Options
    {
        private const string CliVersion = "0.1.0";

        [Option('s', "solution", Required = true, HelpText = "The solution name")]
        public string SolutionName { get; set; }

        [Option('d', "directory", HelpText = "The directory where the project is created")]
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
            help.AddPreOptionsLine("Usage: ndriven -s MySolution -v 0.1.1 -d /path/to/location");
            help.AddOptions(this);
            return help;
        }
    }
}
