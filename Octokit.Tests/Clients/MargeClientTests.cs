using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class MargeClientTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MergingClient(connection);

                var newMerge = new NewMerge("baseBranch", "shaToMerge", "some mergingMessage");
                client.Create("owner", "repo", newMerge);

                connection.Received().Post<Merge>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/merges"),
                    Arg.Is<NewMerge>(nm => nm.Base == "baseBranch"
                                            && nm.Head == "shaToMerge"
                                            && nm.CommitMessage == "some mergingMessage"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new MergingClient(Substitute.For<IApiConnection>());

                var newMerge = new NewMerge("baseBranch", "shaToMerge", "some mergingMessage");
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", newMerge));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, newMerge));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", newMerge));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", newMerge));            
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", null));            
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
