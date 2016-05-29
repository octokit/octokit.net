using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableIssuesLabelsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableIssuesLabelsClient(null));
            }
        }

        public class TheGetForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.GetAllForIssue("fake", "repo", 42);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForIssue("fake", "repo", 42, options);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("", "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("owner", "", 1, ApiOptions.None));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForRepository("fake", "repo", options);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
            }
        }

        public class TheGetForMilestoneMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.GetAllForMilestone("fake", "repo", 42);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42/labels"), Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForMilestone("fake", "repo", 42, options);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForMilestone(null, "name", 42));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForMilestone("owner", null, 42));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForMilestone(null, "name", 42, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForMilestone("owner", null, 42, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForMilestone("owner", "name", 42, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForMilestone("", "name", 42));
                Assert.Throws<ArgumentException>(() => client.GetAllForMilestone("owner", "", 42));
                Assert.Throws<ArgumentException>(() => client.GetAllForMilestone("", "name", 42, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForMilestone("owner", "", 42, ApiOptions.None));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.Get("fake", "repo", "label");

                gitHubClient.Issue.Labels.Received().Get("fake", "repo", "label");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", "label"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, "label"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", "name", null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", "label"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", "label"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "name", ""));
            }
        }

        public class TheAddToIssueMethod
        {
            readonly string[] labels = { "foo", "bar" };

            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.AddToIssue("fake", "repo", 42, labels);

                gitHubClient.Issue.Labels.Received().AddToIssue("fake", "repo", 42, labels);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.AddToIssue(null, "name", 42, labels));
                Assert.Throws<ArgumentNullException>(() => client.AddToIssue("owner", null, 42, labels));
                Assert.Throws<ArgumentNullException>(() => client.AddToIssue("owner", "name", 42, null));

                Assert.Throws<ArgumentException>(() => client.AddToIssue("", "name", 42, labels));
                Assert.Throws<ArgumentException>(() => client.AddToIssue("owner", "", 42, labels));
            }
        }

        public class TheRemoveFromIssueMethod
        {
            [Fact]
            public void DeleteCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.RemoveFromIssue("fake", "repo", 42, "label");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels/label"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.RemoveFromIssue(null, "name", 42, "label"));
                Assert.Throws<ArgumentNullException>(() => client.RemoveFromIssue("owner", null, 42, "label"));
                Assert.Throws<ArgumentNullException>(() => client.RemoveFromIssue("owner", "name", 42, null));

                Assert.Throws<ArgumentException>(() => client.RemoveFromIssue("", "name", 42, "label"));
                Assert.Throws<ArgumentException>(() => client.RemoveFromIssue("owner", "", 42, "label"));
                Assert.Throws<ArgumentException>(() => client.RemoveFromIssue("owner", "name", 42, ""));
            }
        }

        public class TheReplaceForIssueMethod
        {
            readonly string[] labels = { "foo", "bar" };

            [Fact]
            public void PutsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.ReplaceAllForIssue("fake", "repo", 42, labels);

                gitHubClient.Issue.Labels.Received().ReplaceAllForIssue("fake", "repo", 42, labels);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.ReplaceAllForIssue(null, "name", 42, labels));
                Assert.Throws<ArgumentNullException>(() => client.ReplaceAllForIssue("owner", null, 42, labels));
                Assert.Throws<ArgumentNullException>(() => client.ReplaceAllForIssue("owner", "name", 42, null));

                Assert.Throws<ArgumentException>(() => client.ReplaceAllForIssue("", "name", 42, labels));
                Assert.Throws<ArgumentException>(() => client.ReplaceAllForIssue("owner", "", 42, labels));
            }
        }

        public class TheRemoveAllFromIssueMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.RemoveAllFromIssue("fake", "repo", 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.RemoveAllFromIssue(null, "name", 42));
                Assert.Throws<ArgumentNullException>(() => client.RemoveAllFromIssue("owner", null, 42));

                Assert.Throws<ArgumentException>(() => client.RemoveAllFromIssue("", "name", 42));
                Assert.Throws<ArgumentException>(() => client.RemoveAllFromIssue("owner", "", 42));
            }
        }
    }
}
