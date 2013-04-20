using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Github.Client;
using Github.Domain.Model;
using ProjectExtractor;

namespace Presentation.Console
{
    public class ConsoleMain
    {
        private static readonly GithubClient _client;

        static ConsoleMain()
        {
            _client = new GithubClient();
        }

        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                var archive = FetchArchive(FetchTag());
                if (string.IsNullOrEmpty(options.Directory) || !Directory.Exists(options.Directory))
                    options.Directory = Environment.CurrentDirectory;
                var zipPath = options.Directory + Path.DirectorySeparatorChar + "ndriven.zip";
                var destPath = options.Directory + Path.DirectorySeparatorChar + options.SolutionName;
                WriteArchive(archive, zipPath, destPath);
                RenameProject(destPath, options);
            }
        }

        private static void RenameProject(string destPath, Options options)
        {
            var slnRoot = destPath + Path.DirectorySeparatorChar;
            File.Move(slnRoot + "NDriven.sln", slnRoot + options.SolutionName + ".sln");
            System.Console.WriteLine();
            System.Console.WriteLine(string.Format("Project {0} created", options.SolutionName));
        }

        private static void WriteArchive(Archive archive, string zipPath, string destPath)
        {
            var writeArchive = archive.WriteToFile(zipPath);
            ConsoleHelper.WriteUntilComplete(writeArchive, "Extracting project");
            Extractor.Extract(zipPath, destPath);
            File.Delete(zipPath);
        }

        private static Archive FetchArchive(Tag tag)
        {
            var archiveParams = ArchiveParams(tag);
            Task<Archive> getArchive = _client.QueryAsync<Archive>(archiveParams);
            ConsoleHelper.WriteUntilComplete(getArchive, string.Format("Downloading version {0}", tag.name));
            return getArchive.Result;
        }

        private static Tag FetchTag()
        {
            Task<TagCollection> getTags = _client.QueryAsync<TagCollection>(new {owner = "OSTUSA", repo = "ndriven"});
            ConsoleHelper.WriteUntilComplete(getTags, "Fetching versions");
            return getTags.Result.GetLatest();
        }

        private static Dictionary<string, string> ArchiveParams(Tag tag)
        {
            var archiveParams = new Dictionary<string, string>()
                {
                    {"owner", "OSTUSA"},
                    {"repo", "ndriven"},
                    {"format", "zipball"},
                    {"ref", tag.name}
                };
            return archiveParams;
        }
    }
}