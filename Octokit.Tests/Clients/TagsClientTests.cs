using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
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

            connection.Received().Get<GitTag>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/tags/reference"));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new TagsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "reference"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "reference"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "reference"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "reference"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", ""));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new TagsClient(connection);

            client.Create("owner", "repo", new NewTag { Type = TaggedType.Tree });

            connection.Received().Post<GitTag>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/tags"),
                                            Arg.Is<NewTag>(nt => nt.Type == TaggedType.Tree));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new TagsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewTag()));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewTag()));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewTag()));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewTag()));
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

    public class Serialization
    {
        [Fact]
        public void PerformsNewTagSerialization()
        {
            var tag = new NewTag
            {
                Message = "tag-message",
                Tag = "tag-name",
                Object = "tag-object",
                Type = TaggedType.Tree,
                Tagger = new Committer("tagger-name", "tagger-email", DateTimeOffset.Parse("2013-09-03T13:42:52Z"))
            };

            var json = new SimpleJsonSerializer().Serialize(tag);

            const string expectedResult = "{\"tag\":\"tag-name\"," +
                                            "\"message\":\"tag-message\"," +
                                            "\"object\":\"tag-object\"," +
                                            "\"type\":\"tree\"," +
                                            "\"tagger\":{" +
                                                "\"name\":\"tagger-name\"," +
                                                "\"email\":\"tagger-email\"," +
                                                "\"date\":\"2013-09-03T13:42:52Z\"" +
                                            "}" +
                                        "}";

            Assert.Equal(expectedResult, json);
        }
    }
}
