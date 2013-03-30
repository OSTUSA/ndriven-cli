using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Github.Client.Query;
using Github.Domain.Model;
using Github.Domain.Query;
using NUnit.Framework;

namespace Test.Integration.Github.Client.Query
{
    [TestFixture]
    public class GetTagCollectionTest
    {
        protected IQuery<TagCollection> Query { get; set; }
        protected Uri Prefix { get; set; }

        [SetUp]
        public void SetUp()
        {
            Query = new GetTagCollection();
            Prefix = new Uri("https://api.github.com");
        }

        [Test]
        public async void Request_should_return_TagCollection_Task()
        {
            Query.Params = new Dictionary<string, string>()
                {
                    {"owner", "OSTUSA"},
                    {"repo", "ndriven"}
                };
            var getTags = Query.RequestAsync(Prefix);
            Assert.IsInstanceOf<Task<TagCollection>>(getTags);
            var tags = await getTags;
            Assert.IsInstanceOf<TagCollection>(tags);
        }
    }
}
