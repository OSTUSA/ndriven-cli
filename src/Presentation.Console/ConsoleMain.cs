using System.Threading;
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
            }
        }
    }
}
