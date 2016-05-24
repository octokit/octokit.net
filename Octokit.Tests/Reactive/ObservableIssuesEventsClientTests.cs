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
    public class ObservableIssuesEventsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableIssuesEventsClient(null));
            }
        }

        public class TheGetForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var result = new List<EventInfo> { new EventInfo() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesEventsClient(gitHubClient);

                IApiResponse<List<EventInfo>> response = new ApiResponse<List<EventInfo>>(
                    new Response
                    {
                        ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()),
                    }, result);
                gitHubClient.Connection.Get<List<EventInfo>>(Args.Uri, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult(response));

                var eventInfos = await client.GetAllForIssue("fake", "repo", 42).ToList();

                connection.Received().Get<List<EventInfo>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/events"), Args.EmptyDictionary, null);
                Assert.Equal(1, eventInfos.Count);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var result = new List<EventInfo> { new EventInfo() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesEventsClient(gitHubClient);
                
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                IApiResponse<List<EventInfo>> response = new ApiResponse<List<EventInfo>>(
                    new Response
                    {
                        ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()),
                    }, result);
                gitHubClient.Connection.Get<List<EventInfo>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null)
                    .Returns(Task.FromResult(response));

                var eventInfos = await client.GetAllForIssue("fake", "repo", 42, options).ToList();

                connection.Received().Get<List<EventInfo>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/events"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
                Assert.Equal(1, eventInfos.Count);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesEventsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, null));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var result = new List<IssueEvent> { new IssueEvent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesEventsClient(gitHubClient);

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(
                    new Response
                    {
                        ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag1", new RateLimit()),
                    }, result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult(response));

                var issueEvents = await client.GetAllForRepository("fake", "repo").ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"), Args.EmptyDictionary, null);
                Assert.Equal(1, issueEvents.Count);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var result = new List<IssueEvent> { new IssueEvent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesEventsClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(
                    new Response
                    {
                        ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag1", new RateLimit()),
                    }, result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null)
                    .Returns(Task.FromResult(response));

                var issueEvents = await client.GetAllForRepository("fake", "repo", options).ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
                Assert.Equal(1, issueEvents.Count);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesEventsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesEventsClient(gitHubClient);

                client.Get("fake", "repo", 42);

                gitHubClient.Received().Issue.Events.Get("fake", "repo", 42);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesEventsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));
            }
        }
    }
}
