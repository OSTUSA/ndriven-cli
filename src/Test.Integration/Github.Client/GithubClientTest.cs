using System.Threading.Tasks;
using Github.Client;
using Github.Domain.Model;
using NUnit.Framework;

namespace Test.Integration.Github.Client
{
    [TestFixture]
    public class GithubClientTest
    {
        protected GithubClient Client { get; set; }

        [SetUp]
        public void SetUp()
        {
            Client = new GithubClient();
        }

        [Test]
        public async void Query_should_return_Task_for_TagCollection()
        {
            var getTags = Client.QueryAsync<TagCollection>(new {owner = "OSTUSA", repo = "ndriven"});
            Assert.IsInstanceOf<Task<TagCollection>>(getTags);
            var tags = await getTags;
            Assert.IsInstanceOf<TagCollection>(tags);
        }
    }
}
