using System;
using Github.Client;
using Github.Client.Query;
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
            var query = Client.GetQuery<GetTagCollection>(tparams);
            Assert.IsInstanceOf<IQuery<TagCollection>>(query);
        }
    }
}
