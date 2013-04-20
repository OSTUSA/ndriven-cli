using System.Collections.Generic;
using System.Threading.Tasks;
using Github.Client;
using Github.Domain.Model;

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
                Task<TagCollection> getTags = _client.QueryAsync<TagCollection>(new {owner = "OSTUSA", repo = "ndriven" });
                ConsoleHelper.WriteUntilComplete(getTags, "Fetching versions");
                var tag = getTags.Result.GetLatest();
                var archiveParams = ArchiveParams(tag);
                Task<Archive> getArchive = _client.QueryAsync<Archive>(archiveParams);
            }
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