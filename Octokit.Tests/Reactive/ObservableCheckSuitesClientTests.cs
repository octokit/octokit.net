using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ObservableObservableCheckSuitesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableCheckSuitesClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                client.Get("fake", "repo", 1);

                gitHubClient.Check.Suite.Received().Get("fake", "repo", 1);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                client.Get(1, 1);

                gitHubClient.Check.Suite.Received().Get(1, 1);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "repo", 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("fake", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.Get(null, "repo", 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                Assert.Throws<ArgumentException>(() => client.Get("", "repo", 1));
                Assert.Throws<ArgumentException>(() => client.Get("fake", "", 1));
            }
        }

        public class TheGetAllForReferenceMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckSuitesClient(gitHubClient);

                client.GetAllForReference("fake", "repo", "ref");

                connection.Received().Get<List<CheckSuitesResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-suites"),
                    Args.EmptyDictionary,
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckSuitesClient(gitHubClient);

                client.GetAllForReference(1, "ref");
                
                connection.Received().Get<List<CheckSuitesResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-suites"),
                    Args.EmptyDictionary,
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var request = new CheckSuiteRequest
                {
                    AppId = 123,
                    CheckName = "build"
                };

                client.GetAllForReference("fake", "repo", "ref", request);

                connection.Received().Get<List<CheckSuitesResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-suites"),
                    Arg.Is<Dictionary<string, string>>(x =>
                        x["app_id"] == "123"
                        && x["check_name"] == "build"),
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var request = new CheckSuiteRequest
                {
                    AppId = 123,
                    CheckName = "build"
                };

                client.GetAllForReference(1, "ref", request);

                connection.Received().Get<List<CheckSuitesResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-suites"),
                    Arg.Is<Dictionary<string, string>>(x =>
                        x["app_id"] == "123"
                        && x["check_name"] == "build"),
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var request = new CheckSuiteRequest();

                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(null, "repo", "ref"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", null, "ref"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(null, "repo", "ref", request));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", null, "ref", request));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", null, request));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", "ref", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(null, "repo", "ref", request, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", null, "ref", request, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", null, request, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", "ref", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", "ref", request, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(1, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(1, null, request));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(1, "ref", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(1, null, request, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(1, "ref", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForReference(1, "ref", request, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var request = new CheckSuiteRequest();

                Assert.Throws<ArgumentException>(() => client.GetAllForReference("", "repo", "ref"));
                Assert.Throws<ArgumentException>(() => client.GetAllForReference("fake", "", "ref"));
                Assert.Throws<ArgumentException>(() => client.GetAllForReference("fake", "repo", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllForReference("", "repo", "ref", request));
                Assert.Throws<ArgumentException>(() => client.GetAllForReference("fake", "", "ref", request));
                Assert.Throws<ArgumentException>(() => client.GetAllForReference("fake", "repo", "", request));

                Assert.Throws<ArgumentException>(() => client.GetAllForReference("", "repo", "ref", request, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForReference("fake", "", "ref", request, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForReference("fake", "repo", "", request, ApiOptions.None));

                Assert.Throws<ArgumentException>(() => client.GetAllForReference(1, ""));

                Assert.Throws<ArgumentException>(() => client.GetAllForReference(1, "", request));

                Assert.Throws<ArgumentException>(() => client.GetAllForReference(1, "", request, ApiOptions.None));
            }
        }

        public class TheUpdatePreferencesMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var preferences = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(123, true) });

                client.UpdatePreferences("fake", "repo", preferences);

                gitHubClient.Check.Suite.Received().UpdatePreferences("fake", "repo", preferences);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var preferences = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(123, true) });

                client.UpdatePreferences(1, preferences);

                gitHubClient.Check.Suite.Received().UpdatePreferences(1, preferences);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var preferences = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(123, true) });

                Assert.Throws<ArgumentNullException>(() => client.UpdatePreferences(null, "repo", preferences));
                Assert.Throws<ArgumentNullException>(() => client.UpdatePreferences("fake", null, preferences));
                Assert.Throws<ArgumentNullException>(() => client.UpdatePreferences("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var preferences = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(123, true) });

                Assert.Throws<ArgumentException>(() => client.UpdatePreferences("", "repo", preferences));
                Assert.Throws<ArgumentException>(() => client.UpdatePreferences("fake", "", preferences));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var newCheckSuite = new NewCheckSuite("123abc");

                client.Create("fake", "repo", newCheckSuite);

                gitHubClient.Check.Suite.Received().Create("fake", "repo", newCheckSuite);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var newCheckSuite = new NewCheckSuite("123abc");

                client.Create(1, newCheckSuite);

                gitHubClient.Check.Suite.Received().Create(1, newCheckSuite);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var newCheckSuite = new NewCheckSuite("123abc");

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "repo", newCheckSuite));
                Assert.Throws<ArgumentNullException>(() => client.Create("fake", null, newCheckSuite));
                Assert.Throws<ArgumentNullException>(() => client.Create("fake", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var newCheckSuite = new NewCheckSuite("123abc");

                Assert.Throws<ArgumentException>(() => client.Create("", "repo", newCheckSuite));
                Assert.Throws<ArgumentException>(() => client.Create("fake", "", newCheckSuite));
            }
        }

        public class TheRequestMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var request = new CheckSuiteTriggerRequest("123abc");

                client.Request("fake", "repo", request);

                gitHubClient.Check.Suite.Received().Request("fake", "repo", request);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var request = new CheckSuiteTriggerRequest("123abc");

                client.Request(1, request);

                gitHubClient.Check.Suite.Received().Request(1, request);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var request = new CheckSuiteTriggerRequest("123abc");

                Assert.Throws<ArgumentNullException>(() => client.Request(null, "repo", request));
                Assert.Throws<ArgumentNullException>(() => client.Request("fake", null, request));
                Assert.Throws<ArgumentNullException>(() => client.Request("fake", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.Request(1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckSuitesClient(gitHubClient);

                var request = new CheckSuiteTriggerRequest("123abc");

                Assert.Throws<ArgumentException>(() => client.Request("", "repo", request));
                Assert.Throws<ArgumentException>(() => client.Request("fake", "", request));
            }
        }
    }
}
