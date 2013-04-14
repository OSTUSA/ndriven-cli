using System;
using CommandLine;
using CommandLine.Text;

namespace Presentation.Console
{
    public class Options
    {
        private const string CliVersion = "0.1.0";

        [Option('s', "solution", Required = true, HelpText = "The solution name")]
        public string SolutionName { get; set; }

        [Option('v', "version", DefaultValue = "latest", HelpText = "The version to fetch")]
        public string Version { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("NDriven CLI", CliVersion),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage: ndriven -s MySolution");
            help.AddOptions(this);
            return help;
        }
    }
}
