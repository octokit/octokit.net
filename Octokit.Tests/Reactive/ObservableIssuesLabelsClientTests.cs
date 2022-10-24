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

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"), Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.GetAllForIssue(1, 42);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels"), Args.EmptyDictionary);
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

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
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

                client.GetAllForIssue(1, 42, options);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
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

                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(1, 1, null));

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

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"), Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.GetAllForRepository(1);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/labels"), Args.EmptyDictionary);
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

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
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

                client.GetAllForRepository(1, options);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
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

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null));

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

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42/labels"), Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.GetAllForMilestone(1, 42);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones/42/labels"), Args.EmptyDictionary);
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

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
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

                client.GetAllForMilestone(1, 42, options);

                connection.Received().Get<List<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones/42/labels"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
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

                Assert.Throws<ArgumentNullException>(() => client.GetAllForMilestone(1, 42, null));

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
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.Get(1, "label");

                gitHubClient.Issue.Labels.Received().Get(1, "label");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", "label"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, "label"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Get(1, null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", "label"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", "label"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.Get(1, ""));
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
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.AddToIssue(1, 42, labels);

                gitHubClient.Issue.Labels.Received().AddToIssue(1, 42, labels);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.AddToIssue(null, "name", 42, labels));
                Assert.Throws<ArgumentNullException>(() => client.AddToIssue("owner", null, 42, labels));
                Assert.Throws<ArgumentNullException>(() => client.AddToIssue("owner", "name", 42, null));

                Assert.Throws<ArgumentNullException>(() => client.AddToIssue(1, 42, null));

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

                connection.Received().Delete<IReadOnlyList<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels/label"), Arg.Any<object>());
            }

            [Fact]
            public void DeleteCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.RemoveFromIssue(1, 42, "label");

                connection.Received().Delete<IReadOnlyList<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels/label"), Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.RemoveFromIssue(null, "name", 42, "label"));
                Assert.Throws<ArgumentNullException>(() => client.RemoveFromIssue("owner", null, 42, "label"));
                Assert.Throws<ArgumentNullException>(() => client.RemoveFromIssue("owner", "name", 42, null));

                Assert.Throws<ArgumentNullException>(() => client.RemoveFromIssue(1, 42, null));

                Assert.Throws<ArgumentException>(() => client.RemoveFromIssue("", "name", 42, "label"));
                Assert.Throws<ArgumentException>(() => client.RemoveFromIssue("owner", "", 42, "label"));
                Assert.Throws<ArgumentException>(() => client.RemoveFromIssue("owner", "name", 42, ""));

                Assert.Throws<ArgumentException>(() => client.RemoveFromIssue(1, 42, ""));
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
            public void PutsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.ReplaceAllForIssue(1, 42, labels);

                gitHubClient.Issue.Labels.Received().ReplaceAllForIssue(1, 42, labels);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.ReplaceAllForIssue(null, "name", 42, labels));
                Assert.Throws<ArgumentNullException>(() => client.ReplaceAllForIssue("owner", null, 42, labels));
                Assert.Throws<ArgumentNullException>(() => client.ReplaceAllForIssue("owner", "name", 42, null));

                Assert.Throws<ArgumentNullException>(() => client.ReplaceAllForIssue(1, 42, null));

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
            public void DeletesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.RemoveAllFromIssue(1, 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels"));
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

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.Delete("fake", "repo", "labelName");

                gitHubClient.Received().Issue.Labels.Delete("fake", "repo", "labelName");
            }

            [Fact]
            public void DeletesCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                client.Delete(1, "labelName");

                gitHubClient.Received().Issue.Labels.Delete(1, "labelName");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", "labelName"));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, "labelName"));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Delete(1, null));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", "labelName"));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", "labelName"));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.Delete(1, ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CreatesCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                var newLabel = new NewLabel("labelName", "FF0000");

                client.Create("fake", "repo", newLabel);

                gitHubClient.Received().Issue.Labels.Create("fake", "repo", newLabel);
            }

            [Fact]
            public void CreatesCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                var newLabel = new NewLabel("labelName", "FF0000");

                client.Create(1, newLabel);

                gitHubClient.Received().Issue.Labels.Create(1, newLabel);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());
                var newLabel = new NewLabel("labelName", "FF0000");

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", newLabel));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, newLabel));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", newLabel));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", newLabel));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void UpdatesCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                var labelUpdate = new LabelUpdate("name", "FF0000");

                client.Update("fake", "repo", "labelName", labelUpdate);

                gitHubClient.Received().Issue.Labels.Update("fake", "repo", "labelName", labelUpdate);
            }

            [Fact]
            public void UpdatesCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesLabelsClient(gitHubClient);

                var labelUpdate = new LabelUpdate("name", "FF0000");

                client.Update(1, "labelName", labelUpdate);

                gitHubClient.Received().Issue.Labels.Update(1, "labelName", labelUpdate);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesLabelsClient(Substitute.For<IGitHubClient>());
                var labelUpdate = new LabelUpdate("name", "FF0000");

                Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", "labelName", labelUpdate));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, "labelName", labelUpdate));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", null, labelUpdate));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", "labelName", null));

                Assert.Throws<ArgumentNullException>(() => client.Update(1, null, labelUpdate));
                Assert.Throws<ArgumentNullException>(() => client.Update(1, "labelName", null));

                Assert.Throws<ArgumentException>(() => client.Update("", "name", "labelName", labelUpdate));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "", "labelName", labelUpdate));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "name", "", labelUpdate));

                Assert.Throws<ArgumentException>(() => client.Update(1, "", labelUpdate));
            }
        }
    }
}
