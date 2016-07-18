using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class TagsClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTagsClient(gitHubClient);

                client.Get("owner", "repo", "reference");

                gitHubClient.Received().Git.Tag.Get("owner", "repo", "reference");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTagsClient(gitHubClient);

                client.Get(1, "reference");

                gitHubClient.Received().Git.Tag.Get(1, "reference");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableTagsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", "reference"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, "reference"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Get(1, null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", "reference"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", "reference"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.Get(1, ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTagsClient(gitHubClient);

                var newTag = new NewTag { Type = TaggedType.Tree };

                client.Create("owner", "repo", newTag);

                gitHubClient.Received().Git.Tag.Create("owner", "repo", newTag);
            }

            [Fact]
            public void PostsToTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableTagsClient(gitHubClient);

                var newTag = new NewTag { Type = TaggedType.Tree };

                client.Create(1, newTag);

                gitHubClient.Received().Git.Tag.Create(1, newTag);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableTagsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewTag()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewTag()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewTag()));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewTag()));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableTagsClient(null));
            }
        }
    }
}