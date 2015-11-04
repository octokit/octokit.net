using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class MergingClientTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MergingClient(connection);

                var newMerge = new NewMerge("baseBranch", "shaToMerge")
                {
                    CommitMessage = "some mergingMessage"
                };
                client.Create("owner", "repo", newMerge);

                connection.Received().Post<Merge>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/merges"),
                    Arg.Is<NewMerge>(nm => nm.Base == "baseBranch"
                                            && nm.Head == "shaToMerge"
                                            && nm.CommitMessage == "some mergingMessage"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new MergingClient(Substitute.For<IApiConnection>());

                var newMerge = new NewMerge("baseBranch", "shaToMerge") { CommitMessage = "some mergingMessage" };
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", newMerge));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, newMerge));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", newMerge));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", newMerge));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", null));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new CommitsClient(null));
            }
        }
    }
}
