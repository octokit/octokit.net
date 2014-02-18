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

                await client.GetForIssue("fake", "repo", 42);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                await client.GetForRepository("fake", "repo");

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
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

                connection.Received().Get<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/labels/label"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "label"));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.AddToIssue(null, "name", 42, labels));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.AddToIssue("owner", null, 42, labels));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.AddToIssue("owner", "name", 42, null));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.RemoveFromIssue(null, "name", 42, "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.RemoveFromIssue("owner", null, 42, "label"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.RemoveFromIssue("owner", "name", 42, null));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.ReplaceAllForIssue(null, "name", 42, labels));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.ReplaceAllForIssue("owner", null, 42, labels));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.ReplaceAllForIssue("owner", "name", 42, null));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.RemoveAllFromIssue(null, "name", 42));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.RemoveAllFromIssue("owner", null, 42));
            }
        }

        public class TheGetForMilestoneMethod
        {
            [Fact]
            public void GetsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesLabelsClient(connection);

                client.GetForMilestone("fake", "repo", 42);

                connection.Received().GetAll<Label>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42/labels"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesLabelsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForMilestone(null, "name", 42));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForMilestone("owner", null, 42));
            }
        }
    }
}
