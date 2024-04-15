using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

using static Octokit.Internal.TestSetup;

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
                var result = new List<IssueEvent> { new IssueEvent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesEventsClient(gitHubClient);

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(CreateResponse(HttpStatusCode.OK), result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Args.EmptyDictionary)
                    .Returns(Task.FromResult(response));

                var eventInfos = await client.GetAllForIssue("fake", "repo", 42).ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/events"), Args.EmptyDictionary);
                Assert.Single(eventInfos);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var result = new List<IssueEvent> { new IssueEvent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesEventsClient(gitHubClient);

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(CreateResponse(HttpStatusCode.OK), result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Args.EmptyDictionary)
                    .Returns(Task.FromResult(response));

                var eventInfos = await client.GetAllForIssue(1, 42).ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/events"), Args.EmptyDictionary);
                Assert.Single(eventInfos);
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

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(CreateResponse(HttpStatusCode.OK), result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2))
                    .Returns(Task.FromResult(response));

                var eventInfos = await client.GetAllForIssue("fake", "repo", 42, options).ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/events"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
                Assert.Single(eventInfos);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
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
                    CreateResponse(HttpStatusCode.OK), result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2))
                    .Returns(Task.FromResult(response));

                var eventInfos = await client.GetAllForIssue(1, 42, options).ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/events"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
                Assert.Single(eventInfos);
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
                var result = new List<IssueEvent> { new IssueEvent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesEventsClient(gitHubClient);

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(CreateResponse(HttpStatusCode.OK), result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Args.EmptyDictionary)
                    .Returns(Task.FromResult(response));

                var issueEvents = await client.GetAllForRepository("fake", "repo").ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"), Args.EmptyDictionary);
                Assert.Single(issueEvents);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var result = new List<IssueEvent> { new IssueEvent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableIssuesEventsClient(gitHubClient);

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(CreateResponse(HttpStatusCode.OK), result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Args.EmptyDictionary)
                    .Returns(Task.FromResult(response));

                var issueEvents = await client.GetAllForRepository(1).ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/events"), Args.EmptyDictionary);
                Assert.Single(issueEvents);
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

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(CreateResponse(HttpStatusCode.OK), result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2))
                    .Returns(Task.FromResult(response));

                var issueEvents = await client.GetAllForRepository("fake", "repo", options).ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
                Assert.Single(issueEvents);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
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

                IApiResponse<List<IssueEvent>> response = new ApiResponse<List<IssueEvent>>(CreateResponse(HttpStatusCode.OK), result);
                gitHubClient.Connection.Get<List<IssueEvent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2))
                    .Returns(Task.FromResult(response));

                var issueEvents = await client.GetAllForRepository(1, options).ToList();

                connection.Received().Get<List<IssueEvent>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/events"), Arg.Is<Dictionary<string, string>>(d => d.Count == 2));
                Assert.Single(issueEvents);
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

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
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
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableIssuesEventsClient(gitHubClient);

                client.Get(1, 42);

                gitHubClient.Received().Issue.Events.Get(1, 42);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableIssuesEventsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }
    }
}
