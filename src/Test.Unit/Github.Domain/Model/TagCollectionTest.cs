using Github.Domain.Model;
using NUnit.Framework;

namespace Test.Unit.Github.Domain.Model
{
    [TestFixture]
    public class TagCollectionTest
    {
        protected TagCollection Tags { get; set; }

        [SetUp]
        public void SetUp()
        {
            Tags = new TagCollection();
            Tags.Add(new Tag()
                {
                    name = "v0.2.0"
                });
            Tags.Add(new Tag()
                {
                    name = "v0.1.0"
                });
        }

        [Test]
        public void GetLatest_returns_first_element()
        {
            var latest = Tags.GetLatest();
            Assert.AreEqual("v0.2.0", latest.name);
        }
    }
}
