using System;
using Github.Client;
using Github.Domain.Model;
using Github.Domain.Query;
using NUnit.Framework;

namespace Test.Unit.Github.Client
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
        public void Uri_should_return_base_github_uri()
        {
            var uri = new Uri("https://api.github.com");
            Assert.AreEqual(uri, Client.Uri);
        }

        [Test]
        public void GetQuery_should_return_typed_query()
        {
            var tparams = new {owner = "OSTUSA", repo = "ndriven"};
            var query = Client.GetQuery<TagCollection>(tparams);
            Assert.IsInstanceOf<IQuery<TagCollection>>(query);
        }

        [Test]
        public void GetQuery_should_return_null_if_command_does_not_exist()
        {
            var tparams = new { owner = "OSTUSA", repo = "ndriven" };
            var query = Client.GetQuery<Commit>(tparams);
            Assert.IsNull(query);
        }

        [Test]
        [ExpectedException(typeof(QueryNotFoundException))]
        public async void Query_should_throw_exception_if_query_command_not_found()
        {
            var tparams = new { owner = "OSTUSA", repo = "ndriven" };
            var result = await Client.Query<Commit>(tparams);            
        }
    }
}
