using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class RespositoryCommitsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableRepositoryCommitsClient(null));
            }
        }

        public class TheCompareMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.Compare("fake", "repo", "base", "head");

                gitHubClient.Received().Repository.Commit.Compare("fake", "repo", "base", "head");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.Compare(1, "base", "head");

                gitHubClient.Received().Repository.Commit.Compare(1, "base", "head");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Compare(null, "name", "base", "head"));
                Assert.Throws<ArgumentNullException>(() => client.Compare("owner", null, "base", "head"));
                Assert.Throws<ArgumentNullException>(() => client.Compare("owner", "name", null, "head"));
                Assert.Throws<ArgumentNullException>(() => client.Compare("owner", "name", "base", null));

                Assert.Throws<ArgumentNullException>(() => client.Compare(1, null, "head"));
                Assert.Throws<ArgumentNullException>(() => client.Compare(1, "base", null));

                Assert.Throws<ArgumentException>(() => client.Compare("", "name", "base", "head"));
                Assert.Throws<ArgumentException>(() => client.Compare("owner", "", "base", "head"));
                Assert.Throws<ArgumentException>(() => client.Compare("owner", "name", "", "head"));
                Assert.Throws<ArgumentException>(() => client.Compare("owner", "name", "base", ""));
                Assert.Throws<ArgumentException>(() => client.Compare(1, "", "head"));
                Assert.Throws<ArgumentException>(() => client.Compare(1, "base", ""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.Get("fake", "repo", "reference");

                gitHubClient.Received().Repository.Commit.Get("fake", "repo", "reference");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.Get(1, "reference");

                gitHubClient.Received().Repository.Commit.Get(1, "reference");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

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

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.GetAll("fake", "repo");

                gitHubClient.Received().Repository.Commit.GetAll("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.GetAll(1);

                gitHubClient.Received().Repository.Commit.GetAll(1);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll("fake", "repo", options);

                gitHubClient.Received().Repository.Commit.GetAll("fake", "repo", options);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll(1, options);

                gitHubClient.Received().Repository.Commit.GetAll(1, options);
            }

            [Fact]
            public void RequestsCorrectUrlParameterized()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                var commitRequest = new CommitRequest
                {
                    Author = "author",
                    Sha = "sha",
                    Path = "path",
                    Since = null,
                    Until = null
                };

                client.GetAll("fake", "repo", commitRequest);

                gitHubClient.Received().Repository.Commit.GetAll("fake", "repo", commitRequest);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdParameterized()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                var commitRequest = new CommitRequest
                {
                    Author = "author",
                    Sha = "sha",
                    Path = "path",
                    Since = null,
                    Until = null
                };

                client.GetAll(1, commitRequest);

                gitHubClient.Received().Repository.Commit.GetAll(1, commitRequest);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsParameterized()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                var commitRequest = new CommitRequest
                {
                    Author = "author",
                    Sha = "sha",
                    Path = "path",
                    Since = null,
                    Until = null
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll("fake", "repo", commitRequest, options);

                gitHubClient.Received().Repository.Commit.GetAll("fake", "repo", commitRequest, options);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptionsParameterized()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                var commitRequest = new CommitRequest
                {
                    Author = "author",
                    Sha = "sha",
                    Path = "path",
                    Since = null,
                    Until = null
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll(1, commitRequest, options);

                gitHubClient.Received().Repository.Commit.GetAll(1, commitRequest, options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", (ApiOptions)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", new CommitRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, new CommitRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", (CommitRequest)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", new CommitRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, new CommitRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", new CommitRequest(), null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, (CommitRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, new CommitRequest(), null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", new CommitRequest()));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", new CommitRequest()));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", new CommitRequest(), ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", new CommitRequest(), ApiOptions.None));
            }
        }

        public class TheGetSha1Method
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.GetSha1("fake", "repo", "ref");

                gitHubClient.Received().Repository.Commit.GetSha1("fake", "repo", "ref");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.GetSha1(1, "ref");

                gitHubClient.Received().Repository.Commit.GetSha1(1, "ref");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.GetSha1(null, "name", "ref"));
                Assert.Throws<ArgumentNullException>(() => client.GetSha1("owner", null, "ref"));
                Assert.Throws<ArgumentNullException>(() => client.GetSha1("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetSha1(1, null));

                Assert.Throws<ArgumentException>(() => client.GetSha1("", "name", "ref"));
                Assert.Throws<ArgumentException>(() => client.GetSha1("owner", "", "ref"));
                Assert.Throws<ArgumentException>(() => client.GetSha1("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.GetSha1(1, ""));
            }
        }
    }
}