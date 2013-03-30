using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Github.Client.Query;
using Github.Domain.Model;
using Github.Domain.Query;
using NUnit.Framework;

namespace Test.Integration.Github.Client.Query
{
    [TestFixture]
    public class GetArchiveTest
    {
        protected IQuery<Archive> Query { get; set; }
        protected Uri Prefix { get; set; }

        [SetUp]
        public void SetUp()
        {
            Query = new GetArchive();
            Prefix = new Uri("https://api.github.com");
        }

        [Test]
        public async void Request_should_return_Archive_Task()
        {
            Query.Params = new Dictionary<string, string>()
                {
                    {"owner", "OSTUSA"},
                    {"repo", "ndriven"},
                    {"format", "zipball"},
                    {"ref", "v0.1.0"}
                };
            var getArchive = Query.RequestAsync(Prefix);
            Assert.IsInstanceOf<Task<Archive>>(getArchive);
            var archive = await getArchive;
            Assert.IsInstanceOf<Archive>(archive);
        }
    }
}

