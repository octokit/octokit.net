using System;
using NSubstitute;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

public class CommitsClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public async void EnsuresNonNullArguments()
        {
            var client = new CommitsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "reference"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "reference"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", "reference"));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", "reference"));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "name", ""));
        }

        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new CommitsClient(connection);

            client.Get("owner", "repo", "reference");

            connection.Received().Get<Commit>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/commits/reference"), null);
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