using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class CheckSuitesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new CheckSuitesClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                await client.Get("fake", "repo", 1);

                connection.Received().Get<CheckSuite>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/1"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                await client.Get(1, 1);

                connection.Received().Get<CheckSuite>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/1"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("fake", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("fake", "", 1));
            }
        }

        public class TheGetAllForReferenceMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                await client.GetAllForReference("fake", "repo", "ref");

                connection.Received().GetAll<CheckSuitesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-suites"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                await client.GetAllForReference(1, "ref");

                connection.Received().GetAll<CheckSuitesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-suites"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var request = new CheckSuiteRequest
                {
                    AppId = 123,
                    CheckName = "build"
                };

                await client.GetAllForReference("fake", "repo", "ref", request);

                connection.Received().GetAll<CheckSuitesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-suites"),
                    Arg.Is<Dictionary<string, string>>(x =>
                        x["app_id"] == "123"
                        && x["check_name"] == "build"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var request = new CheckSuiteRequest
                {
                    AppId = 123,
                    CheckName = "build"
                };

                await client.GetAllForReference(1, "ref", request);

                connection.Received().GetAll<CheckSuitesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-suites"),
                    Arg.Is<Dictionary<string, string>>(x =>
                        x["app_id"] == "123"
                        && x["check_name"] == "build"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var request = new CheckSuiteRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(null, "repo", "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", null, "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(null, "repo", "ref", request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", null, "ref", request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", null, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", "ref", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(null, "repo", "ref", request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", null, "ref", request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", null, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", "ref", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference("fake", "repo", "ref", request, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(1, null, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(1, "ref", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(1, null, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(1, "ref", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForReference(1, "ref", request, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var request = new CheckSuiteRequest();

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("", "repo", "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("fake", "", "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("fake", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("", "repo", "ref", request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("fake", "", "ref", request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("fake", "repo", "", request));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("", "repo", "ref", request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("fake", "", "ref", request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference("fake", "repo", "", request, ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference(1, ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference(1, "", request));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForReference(1, "", request, ApiOptions.None));
            }
        }

        public class TheUpdatePreferencesMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var preferences = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(123, true) });

                await client.UpdatePreferences("fake", "repo", preferences);

                connection.Received().Patch<CheckSuitePreferencesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/preferences"),
                    preferences);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var preferences = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(123, true) });

                await client.UpdatePreferences(1, preferences);

                connection.Received().Patch<CheckSuitePreferencesResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/preferences"),
                    preferences);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var preferences = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(123, true) });

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdatePreferences(null, "repo", preferences));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdatePreferences("fake", null, preferences));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdatePreferences("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var preferences = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(123, true) });

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdatePreferences("", "repo", preferences));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdatePreferences("fake", "", preferences));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var newCheckSuite = new NewCheckSuite("123abc");

                await client.Create("fake", "repo", newCheckSuite);

                connection.Received().Post<CheckSuite>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites"),
                    newCheckSuite);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var newCheckSuite = new NewCheckSuite("123abc");

                await client.Create(1, newCheckSuite);

                connection.Received().Post<CheckSuite>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites"),
                    newCheckSuite);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var newCheckSuite = new NewCheckSuite("123abc");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "repo", newCheckSuite));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fake", null, newCheckSuite));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fake", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                var newCheckSuite = new NewCheckSuite("123abc");

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "repo", newCheckSuite));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("fake", "", newCheckSuite));
            }
        }

        public class TheRerequestMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Created);
                var client = new CheckSuitesClient(connection);

                await client.Rerequest("fake", "repo", 1);

                connection.Connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/1/rerequest"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Created);
                var client = new CheckSuitesClient(connection);

                await client.Rerequest(1, 1);

                connection.Connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/1/rerequest"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Rerequest(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Rerequest("fake", null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckSuitesClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Rerequest("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Rerequest("fake", "", 1));
            }
        }
    }
}
