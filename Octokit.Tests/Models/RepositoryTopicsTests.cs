using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Models
{
    public class RepositoryTopicsTests
    {
        public class Ctor
        {
            [Fact]
            public void EmptyListWhenCtorEmpty()
            {
                var result = new RepositoryTopics();
                Assert.NotNull(result.Names);
                Assert.Empty(result.Names);
            }

            [Fact]
            public void EmptyListWhenCtorListIsEmpty()
            {
                var result = new RepositoryTopics(new List<string>());
                Assert.NotNull(result.Names);
                Assert.Empty(result.Names);
            }

            [Fact]
            public void EmptyListWhenCtorIsNull()
            {
                var result = new RepositoryTopics(null);
                Assert.NotNull(result.Names);
                Assert.Empty(result.Names);
            }

            [Fact]
            public void SetsListWhenListProvided()
            {
                var theItems = new List<string> {"one", "two", "three"};
                var result = new RepositoryTopics(theItems);

                Assert.Contains("one", result.Names);
                Assert.Contains("two", result.Names);
                Assert.Contains("three", result.Names);
            }
        }
    }
}
