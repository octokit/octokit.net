using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;
using System.Collections.Generic;

namespace Octokit.Tests.Clients
{
    public class IssuesLabelsClientTests
    {
        public class TheGetForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetAllForIssue("fake", "repo", 42);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null));
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

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.Get("fake", "repo", "label");

                connection.Received().Get<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels/label"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "label"));
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
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddToIssue(null, "name", 42, labels));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddToIssue("owner", null, 42, labels));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddToIssue("owner", "name", 42, null));
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

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels/label"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveFromIssue(null, "name", 42, "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveFromIssue("owner", null, 42, "label"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveFromIssue("owner", "name", 42, null));
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
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReplaceAllForIssue(null, "name", 42, labels));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReplaceAllForIssue("owner", null, 42, labels));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReplaceAllForIssue("owner", "name", 42, null));
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
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAllFromIssue(null, "name", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAllFromIssue("owner", null, 42));
            }
        }

        public class TheGetForMilestoneMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.GetAllForMilestone("fake", "repo", 42);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForMilestone(null, "name", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForMilestone("owner", null, 42));
            }
        }
    }
}
