using System;
using System.Collections.Generic;
using Github.Client.Query;
using Github.Domain.Model;
using Github.Domain.Query;
using NUnit.Framework;

namespace Test.Unit.Github.Client.Query
{
    [TestFixture]
    public class QueryBaseTest
    {
        protected IQuery<TagCollection> Query { get; set; }

        [SetUp]
        public void SetUp()
        {
            Query = new TestQuery();
        }

        [Test]
        public void Constructor_sets_UriTemplate()
        {
            var tpl = new UriTemplate("repos/{owner}/{repo}/tags");
            Assert.AreEqual(tpl.ToString(), Query.Template.ToString());
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public async void Request_throws_exception_if_Params_null()
        {
            var query = new TestQuery();
            await query.RequestAsync(new Uri("https://api.github.com"));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public async void Request_throws_exception_if_not_enough_params()
        {
            var query = new TestQuery();
            query.Params = new Dictionary<string, string>()
                {
                    {"owner", "me"}
                };
            await query.RequestAsync(new Uri("https://api.github.com"));
        }
    }

    public class TestQuery : QueryBase<TagCollection>
    {
        public TestQuery() : base("repos/{owner}/{repo}/tags")
        {
            
        }
    }
}
