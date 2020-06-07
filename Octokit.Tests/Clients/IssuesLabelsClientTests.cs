using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using System.Collections.Generic;

namespace Octokit.Tests.Clients
{
    public class IssuesLabelsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new IssuesLabelsClient(null));
            }
        }

        public class TheGetForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetAllForIssue("fake", "repo", 42);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetAllForIssue(1, 42);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForIssue("fake", "repo", 42, options);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForIssue(1, 42, options);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("owner", "", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("", "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("owner", "", 1, ApiOptions.None));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/labels"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository(1, options);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/labels"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
            }
        }

        public class TheGetForMilestoneMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetAllForMilestone("fake", "repo", 42);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42/labels"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetAllForMilestone(1, 42);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones/42/labels"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForMilestone("fake", "repo", 42, options);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42/labels"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForMilestone(1, 42, options);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones/42/labels"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForMilestone(null, "name", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForMilestone("owner", null, 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForMilestone(null, "name", 42, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForMilestone("owner", null, 42, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForMilestone("owner", "name", 42, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForMilestone(1, 42, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForMilestone("", "name", 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForMilestone("owner", "", 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForMilestone("", "name", 42, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForMilestone("owner", "", 42, ApiOptions.None));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.Get("fake", "repo", "label");

                connection.Received().Get<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels/label"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.Get(1, "label");

                connection.Received().Get<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/labels/label"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "label"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "label"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, ""));
            }
        }

        public class TheAddToIssueMethod
        {
            readonly string[] labels = { "foo", "bar" };

            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.AddToIssue("fake", "repo", 42, labels);

                connection.Received().Post<IReadOnlyList<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"), Arg.Any<string[]>());
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.AddToIssue(1, 42, labels);

                connection.Received().Post<IReadOnlyList<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels"), Arg.Any<string[]>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddToIssue(null, "name", 42, labels));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddToIssue("owner", null, 42, labels));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddToIssue("owner", "name", 42, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddToIssue(1, 42, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddToIssue("", "name", 42, labels));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddToIssue("owner", "", 42, labels));
            }
        }

        public class TheRemoveFromIssueMethod
        {
            [Fact]
            public void DeleteCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.RemoveFromIssue("fake", "repo", 42, "label");

                connection.Received().Delete<IReadOnlyList<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels/label"), Args.Object);
            }

            [Fact]
            public void DeleteCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.RemoveFromIssue(1, 42, "label");

                connection.Received().Delete<IReadOnlyList<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels/label"), Args.Object);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveFromIssue(null, "name", 42, "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveFromIssue("owner", null, 42, "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveFromIssue("owner", "name", 42, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveFromIssue(1, 42, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveFromIssue("", "name", 42, "label"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveFromIssue("owner", "", 42, "label"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveFromIssue("owner", "name", 42, ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveFromIssue(1, 42, ""));
            }
        }

        public class TheReplaceForIssueMethod
        {
            readonly string[] labels = { "foo", "bar" };

            [Fact]
            public void PutsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.ReplaceAllForIssue("fake", "repo", 42, labels);

                connection.Received().Put<IReadOnlyList<Label>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"), Arg.Any<string[]>());
            }

            [Fact]
            public void PutsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.ReplaceAllForIssue(1, 42, labels);

                connection.Received().Put<IReadOnlyList<Label>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels"), Arg.Any<string[]>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReplaceAllForIssue(null, "name", 42, labels));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReplaceAllForIssue("owner", null, 42, labels));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReplaceAllForIssue("owner", "name", 42, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReplaceAllForIssue(1, 42, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.ReplaceAllForIssue("", "name", 42, labels));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ReplaceAllForIssue("owner", "", 42, labels));
            }
        }

        public class TheRemoveAllFromIssueMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.RemoveAllFromIssue("fake", "repo", 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"));
            }

            [Fact]
            public void DeletesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.RemoveAllFromIssue(1, 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAllFromIssue(null, "name", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAllFromIssue("owner", null, 42));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveAllFromIssue("", "name", 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveAllFromIssue("owner", "", 42));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.Delete("fake", "repo", "labelName");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels/labelName"));
            }

            [Fact]
            public void DeletesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.Delete(1, "labelName");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/labels/labelName"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", "labelName"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, "labelName"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", "labelName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", "labelName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(1, ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CreatesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var newLabel = new NewLabel("labelName", "FF0000");

                client.Create("fake", "repo", newLabel);

                connection.Received().Post<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"), newLabel);
            }

            [Fact]
            public void CreatesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var newLabel = new NewLabel("labelName", "FF0000");

                client.Create(1, newLabel);

                connection.Received().Post<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/labels"), newLabel);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());
                var newLabel = new NewLabel("labelName", "FF0000");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", newLabel));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, newLabel));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", newLabel));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", newLabel));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void UpdatesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var labelUpdate = new LabelUpdate("name", "FF0000");

                client.Update("fake", "repo", "labelName", labelUpdate);

                connection.Received().Post<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels/labelName"), labelUpdate);
            }

            [Fact]
            public void UpdatesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                var labelUpdate = new LabelUpdate("name", "FF0000");

                client.Update(1, "labelName", labelUpdate);

                connection.Received().Post<Label>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/labels/labelName"), labelUpdate);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());
                var labelUpdate = new LabelUpdate("name", "FF0000");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", "labelName", labelUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, "labelName", labelUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", null, labelUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", "labelName", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null, labelUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, "labelName", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", "labelName", labelUpdate));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", "labelName", labelUpdate));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "name", "", labelUpdate));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update(1, "", labelUpdate));
            }
        }
    }
}
