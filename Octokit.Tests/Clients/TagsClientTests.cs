using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

public class TagsClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new TagsClient(connection);

            client.Get("owner", "repo", "reference");

            connection.Received().Get<Tag>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/tags/reference"), null);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new TagsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "sha"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "sha"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get(null, "", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("", null, null));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get(null, null, ""));
        }
    }


    public class TheCtor
    {
        [Fact]
        public void EnsuresArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new TagsClient(null));
        }
    }
}
