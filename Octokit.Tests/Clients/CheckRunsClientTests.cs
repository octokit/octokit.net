using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class CheckRunsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new CheckRunsClient(null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                await client.Create("fake", "repo", newCheckRun);

                connection.Received().Post<CheckRun>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-runs"),
                    newCheckRun,
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                await client.Create(1, newCheckRun);

                connection.Received().Post<CheckRun>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-runs"),
                    newCheckRun,
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "repo", newCheckRun));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fake", null, newCheckRun));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fake", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "repo", newCheckRun));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("fake", "", newCheckRun));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var update = new CheckRunUpdate("status") { Status = CheckStatus.InProgress };

                await client.Update("fake", "repo", 1, update);

                connection.Received().Patch<CheckRun>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-runs/1"),
                    update,
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var update = new CheckRunUpdate("status") { Status = CheckStatus.InProgress };

                await client.Update(1, 1, update);

                connection.Received().Patch<CheckRun>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-runs/1"),
                    update,
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var update = new CheckRunUpdate("status") { Status = CheckStatus.InProgress };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "repo", 1, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("fake", null, 1, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("fake", "repo", 1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var update = new CheckRunUpdate("status") { Status = CheckStatus.InProgress };

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "repo", 1, update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("fake", "", 1, update));
            }
        }

        public class TheGetAllForReferenceMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                await client.GetAllForReference("fake", "repo", "ref");

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-runs"),
                    Args.EmptyDictionary,
                    "application/vnd.github.antiope-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                await client.GetAllForReference(1, "ref");

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-runs"),
                    Args.EmptyDictionary,
                    "application/vnd.github.antiope-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };

                await client.GetAllForReference("fake", "repo", "ref", request);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"),
                    "application/vnd.github.antiope-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };

                await client.GetAllForReference(1, "ref", request);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"),
                    "application/vnd.github.antiope-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                await client.GetAllForReference("fake", "repo", "ref", request, options);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"),
                    "application/vnd.github.antiope-preview+json",
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                await client.GetAllForReference(1, "ref", request, options);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"),
                    "application/vnd.github.antiope-preview+json",
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build" };

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
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build" };

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

        public class TheGetAllForCheckSuiteMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                await client.GetAllForCheckSuite("fake", "repo", 1);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/1/check-runs"),
                    Args.EmptyDictionary,
                    "application/vnd.github.antiope-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                await client.GetAllForCheckSuite(1, 1);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/1/check-runs"),
                    Args.EmptyDictionary,
                    "application/vnd.github.antiope-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };

                await client.GetAllForCheckSuite("fake", "repo", 1, request);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/1/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"),
                    "application/vnd.github.antiope-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };

                await client.GetAllForCheckSuite(1, 1, request);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/1/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"),
                    "application/vnd.github.antiope-preview+json",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                await client.GetAllForCheckSuite("fake", "repo", 1, request, options);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/1/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"),
                    "application/vnd.github.antiope-preview+json",
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                await client.GetAllForCheckSuite(1, 1, request, options);

                connection.Received().GetAll<CheckRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/1/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"),
                    "application/vnd.github.antiope-preview+json",
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", null, 1));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite(null, "repo", 1, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", null, 1, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", "repo", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite(null, "repo", 1, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", null, 1, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", "repo", 1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", "repo", 1, request, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite(1, 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite(1, 1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCheckSuite(1, 1, request, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var request = new CheckRunRequest { CheckName = "build" };

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCheckSuite("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCheckSuite("fake", "", 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCheckSuite("", "repo", 1, request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCheckSuite("fake", "", 1, request));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCheckSuite("", "repo", 1, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCheckSuite("fake", "", 1, request, ApiOptions.None));
            }
        }
    }
}