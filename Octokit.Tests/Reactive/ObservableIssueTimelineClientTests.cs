using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableIssueTimelineClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableIssueTimelineClient(null));
            }
        }

        public class TheGetAllForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var result = new List<TimelineEventInfo> { new TimelineEventInfo() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssueTimelineClient(gitHubClient);

                IApiResponse<List<TimelineEventInfo>> response = new ApiResponse<List<TimelineEventInfo>>(
                    new Response
                    {
                        ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()),
                    }, result);
                gitHubClient.Connection.Get<List<TimelineEventInfo>>(Args.Uri, Args.EmptyDictionary, "application/vnd.github.mockingbird-preview")
                    .Returns(Task.FromResult(response));

                var timelineEvents = await client.GetAllForIssue("fake", "repo", 42).ToList();

                connection.Received().Get<List<TimelineEventInfo>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/timeline"), 
                    Arg.Any<Dictionary<string, string>>(), 
                    "application/vnd.github.mockingbird-preview");
                Assert.Equal(1, timelineEvents.Count);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var result = new List<TimelineEventInfo> { new TimelineEventInfo() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssueTimelineClient(gitHubClient);

                IApiResponse<List<TimelineEventInfo>> response = new ApiResponse<List<TimelineEventInfo>>(
                    new Response
                    {
                        ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()),
                    }, result);
                gitHubClient.Connection.Get<List<TimelineEventInfo>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 1), "application/vnd.github.mockingbird-preview")
                    .Returns(Task.FromResult(response));

                var timelineEvents = await client.GetAllForIssue("fake", "repo", 42, new ApiOptions {PageSize = 30}).ToList();

                connection.Received().Get<List<TimelineEventInfo>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/timeline"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 1 && d["per_page"] == "30"),
                    "application/vnd.github.mockingbird-preview");
                Assert.Equal(1, timelineEvents.Count);
            }

            [Fact]
            public async Task RequestCorrectUrlWithRepositoryId()
            {
                var result = new List<TimelineEventInfo> { new TimelineEventInfo() };
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableIssueTimelineClient(githubClient);

                IApiResponse<List<TimelineEventInfo>> response = new ApiResponse<List<TimelineEventInfo>>(
                    new Response
                    {
                        ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()),
                    }, result);
                githubClient.Connection.Get<List<TimelineEventInfo>>(Args.Uri, Args.EmptyDictionary, "application/vnd.github.mockingbird-preview")
                    .Returns(Task.FromResult(response));

                var timelineEvents = await client.GetAllForIssue(1, 42).ToList();

                connection.Received().Get<List<TimelineEventInfo>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/timeline"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.mockingbird-preview");
                Assert.Equal(1, timelineEvents.Count);
            }

            [Fact]
            public async Task RequestCorrectUrlWithRepositoryIdAndApiOptions()
            {
                var result = new List<TimelineEventInfo> { new TimelineEventInfo() };
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableIssueTimelineClient(githubClient);

                IApiResponse<List<TimelineEventInfo>> response = new ApiResponse<List<TimelineEventInfo>>(
                    new Response
                    {
                        ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()),
                    }, result);
                githubClient.Connection.Get<List<TimelineEventInfo>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 1), "application/vnd.github.mockingbird-preview")
                    .Returns(Task.FromResult(response));

                var timelineEvents = await client.GetAllForIssue(1, 42, new ApiOptions {PageSize = 30}).ToList();

                connection.Received().Get<List<TimelineEventInfo>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/timeline"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 1 && d["per_page"] == "30"),
                    "application/vnd.github.mockingbird-preview");
                Assert.Equal(1, timelineEvents.Count);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssueTimelineClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(null, "repo", 42));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 42));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", "repo", 42, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(1, 42, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("", "repo", 42));
                Assert.Throws<ArgumentException>(() => client.GetAllForIssue("owner", "", 42));

            }
        }
    }
}
