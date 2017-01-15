using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ObservableCommitStatusClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitStatusClient(gitHubClient);

                client.GetAll("fake", "repo", "sha");

                gitHubClient.Received().Repository.Status.GetAll("fake", "repo", "sha");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitStatusClient(gitHubClient);

                client.GetAll(1, "sha");

                gitHubClient.Received().Repository.Status.GetAll(1, "sha");
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitStatusClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll("fake", "repo", "sha", options);

                connection.Received().Repository.Status.GetAll("fake", "repo", "sha", options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitStatusClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll(1, "sha", options);

                connection.Received().Repository.Status.GetAll(1, "sha", options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableCommitStatusClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", "sha"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, "sha"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", "sha", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, "sha", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", "sha", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, "sha", null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", "sha"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", "sha"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "name", ""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", "sha", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", "sha", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "name", "", ApiOptions.None));

                Assert.Throws<ArgumentException>(() => client.GetAll(1, "", ApiOptions.None));
            }
        }

        public class TheGetCombinedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitStatusClient(gitHubClient);

                client.GetCombined("fake", "repo", "sha");

                gitHubClient.Received().Repository.Status.GetCombined("fake", "repo", "sha");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitStatusClient(gitHubClient);

                client.GetCombined(1, "sha");

                gitHubClient.Received().Repository.Status.GetCombined(1, "sha");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableCommitStatusClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetCombined(null, "name", "sha"));
                Assert.Throws<ArgumentNullException>(() => client.GetCombined("owner", null, "sha"));
                Assert.Throws<ArgumentNullException>(() => client.GetCombined("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetCombined(1, null));

                Assert.Throws<ArgumentException>(() => client.GetCombined("", "name", "sha"));
                Assert.Throws<ArgumentException>(() => client.GetCombined("owner", "", "sha"));
                Assert.Throws<ArgumentException>(() => client.GetCombined("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.GetCombined(1, ""));
            }
        }

        public class TheCreateMethodForUser
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitStatusClient(gitHubClient);

                var newCommitStatus = new NewCommitStatus { State = CommitState.Success };

                client.Create("owner", "repo", "sha", newCommitStatus);

                gitHubClient.Received().Repository.Status.Create("owner", "repo", "sha", newCommitStatus);
            }

            [Fact]
            public void PostsToTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitStatusClient(gitHubClient);

                var newCommitStatus = new NewCommitStatus { State = CommitState.Success };

                client.Create(1, "sha", newCommitStatus);

                gitHubClient.Received().Repository.Status.Create(1, "sha", newCommitStatus);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableCommitStatusClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", "sha", new NewCommitStatus()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, "sha", new NewCommitStatus()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null, new NewCommitStatus()));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", "sha", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null, new NewCommitStatus()));
                Assert.Throws<ArgumentNullException>(() => client.Create(1, "sha", null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", "sha", new NewCommitStatus()));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", "sha", new NewCommitStatus()));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "name", "", new NewCommitStatus()));

                Assert.Throws<ArgumentException>(() => client.Create(1, "", new NewCommitStatus()));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableCommitStatusClient(null));
            }
        }
    }
}
