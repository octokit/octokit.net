using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
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

            connection.Received().Get<GitTag>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/tags/reference"), null);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new TagsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "reference"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "reference"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", "reference"));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", "reference"));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "name", ""));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToTheCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new TagsClient(connection);

            client.Create("owner", "repo", new NewTag{Type = TaggedType.Tree});

            connection.Received().Post<GitTag>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/tags"), 
                                            Arg.Is<NewTag>(nt => nt.Type == TaggedType.Tree));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new TagsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", new NewTag()));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, new NewTag()));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", new NewTag()));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", new NewTag()));
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
                Tagger = new Signature
                {
                    Name = "tagger-name",
                    Email = "tagger-email",
                    Date = new DateTime(2013, 09, 03, 13, 42, 52, DateTimeKind.Utc)
                }
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
