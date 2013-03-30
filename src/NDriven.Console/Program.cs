using Github.Client;
using Github.Client.Query;

namespace NDriven.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new GithubClient();
            client.Query<GetTagCollection>(new {owner = "OSTUSA",repo = "ndriven"}, (rargs) => System.Console.WriteLine(rargs.Model.GetType().ToString()));
        }
    }
}
