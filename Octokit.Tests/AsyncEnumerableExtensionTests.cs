using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.AsyncPaginationExtension;
using Xunit;

namespace Octokit.Tests
{
    public class AsyncEnumerableExtensionTests
    {
        public class ThePaginatedList
        {
            [Fact]
            public void RejectsInvalidValues()
            {
                var client = Substitute.For<IRepositoriesClient>();

                Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.GetAllForOrgAsync("octokit")[-1]);
            }

            [Fact]
            public async Task ReturnsNullOnExceedingTotalSize()
            {
                var client = Substitute.For<IRepositoriesClient>();

                Assert.Null(await client.GetAllForOrgAsync("octokit")[int.MaxValue]);
            }

            [Fact]
            public async Task EnumeratesCorrectValues()
            {
                var client = Substitute.For<IRepositoriesClient>();

                var list = new List<Repository>();
                var enumerator = client.GetAllForOrgAsync("octokit").GetAsyncEnumerator();
                while (await enumerator.MoveNextAsync())
                {
                    list.Add(enumerator.Current);
                }

                Assert.Equal(await client.GetAllForOrg("octokit"), list);
            }

            [Fact]
            public async Task HandlesZeroCorrectly()
            {
                var client = Substitute.For<IRepositoriesClient>();
                client.GetAllForOrg(Arg.Any<string>(), Arg.Any<ApiOptions>())
                    .Returns(Enumerable.Repeat(new Repository(), 24).ToList());

                Assert.NotNull(await client.GetAllForOrgAsync("octokit")[0]);
            }

            [Fact]
            public async Task HandlesPageEdgesCorrectly()
            {
                var client = Substitute.For<IRepositoriesClient>();
                client.GetAllForOrg(Arg.Any<string>(), Arg.Any<ApiOptions>())
                    .Returns(Enumerable.Repeat(new Repository(), 5).ToList());

                var repos = client.GetAllForOrgAsync("octokit", 5);
                Assert.NotNull(await repos[4]);
                Assert.NotNull(await repos[5]);
            }
        }

        public class ThePaginationOverloads
        {
            [Fact]
            public void RejectInvalidValues()
            {
                var client = Substitute.For<IRepositoriesClient>();
                
                Assert.Throws<ArgumentOutOfRangeException>(() => client.GetAllForUserAsync("fake", -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => client.GetAllForUserAsync("fake", 0));
            }
        }
    }
}
