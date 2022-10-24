using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class IssueTimelineClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new IssueTimelineClient(null));
            }
        }

        public class TheGetAllForIssueMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueTimelineClient(connection);

                await client.GetAllForIssue("fake", "repo", 42);

                connection.Received().GetAll<TimelineEventInfo>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/timeline"),
                    Arg.Any<Dictionary<string, string>>(),
                    Arg.Any<ApiOptions>());
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueTimelineClient(connection);

                await client.GetAllForIssue("fake", "repo", 42, new ApiOptions { PageSize = 30 });

                connection.Received().GetAll<TimelineEventInfo>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/timeline"),
                    Arg.Any<Dictionary<string, string>>(),
                    Arg.Is<ApiOptions>(ao => ao.PageSize == 30));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueTimelineClient(connection);

                await client.GetAllForIssue(1, 42);

                connection.Received().GetAll<TimelineEventInfo>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/timeline"),
                    Arg.Any<Dictionary<string, string>>(),
                    Arg.Any<ApiOptions>());
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdAndApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueTimelineClient(connection);

                await client.GetAllForIssue(1, 42, new ApiOptions { PageSize = 30 });

                connection.Received().GetAll<TimelineEventInfo>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/timeline"),
                    Arg.Any<Dictionary<string, string>>(),
                    Arg.Is<ApiOptions>(ao => ao.PageSize == 30));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssueTimelineClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "repo", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", "repo", 42, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(1, 42, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("", "repo", 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("owner", "", 42));
            }
        }
    }
}
