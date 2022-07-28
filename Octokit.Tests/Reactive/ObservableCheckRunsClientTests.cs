﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ObservableCheckRunsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableCheckRunsClient(null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                client.Create("fake", "repo", newCheckRun);

                gitHubClient.Check.Run.Received().Create("fake", "repo", newCheckRun);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                client.Create(1, newCheckRun);

                gitHubClient.Check.Run.Received().Create(1, newCheckRun);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "repo", newCheckRun));
                Assert.Throws<ArgumentNullException>(() => client.Create("fake", null, newCheckRun));
                Assert.Throws<ArgumentNullException>(() => client.Create("fake", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                Assert.Throws<ArgumentException>(() => client.Create("", "repo", newCheckRun));
                Assert.Throws<ArgumentException>(() => client.Create("fake", "", newCheckRun));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var update = new CheckRunUpdate { Status = CheckStatus.InProgress };

                client.Update("fake", "repo", 1, update);

                gitHubClient.Check.Run.Received().Update("fake", "repo", 1, update);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var update = new CheckRunUpdate { Status = CheckStatus.InProgress };

                client.Update(1, 1, update);

                gitHubClient.Check.Run.Received().Update(1, 1, update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var update = new CheckRunUpdate { Status = CheckStatus.InProgress };

                Assert.Throws<ArgumentNullException>(() => client.Update(null, "repo", 1, update));
                Assert.Throws<ArgumentNullException>(() => client.Update("fake", null, 1, update));
                Assert.Throws<ArgumentNullException>(() => client.Update("fake", "repo", 1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var update = new CheckRunUpdate { Status = CheckStatus.InProgress };

                Assert.Throws<ArgumentException>(() => client.Update("", "repo", 1, update));
                Assert.Throws<ArgumentException>(() => client.Update("fake", "", 1, update));
            }
        }

        public class TheGetAllForReferenceMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                client.GetAllForReference("fake", "repo", "ref");

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-runs"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                client.GetAllForReference(1, "ref");

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-runs"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };

                client.GetAllForReference("fake", "repo", "ref", request);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 3
                            && x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };

                client.GetAllForReference(1, "ref", request);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 3
                            && x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                client.GetAllForReference("fake", "repo", "ref", request, options);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/ref/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 4
                            && x["per_page"] == "1"
                            && x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                client.GetAllForReference(1, "ref", request, options);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/ref/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 4
                            && x["per_page"] == "1"
                            && x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build" };

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
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build" };

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

        public class TheGetAllForCheckSuiteMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                client.GetAllForCheckSuite("fake", "repo", 1);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/1/check-runs"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                client.GetAllForCheckSuite(1, 1);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/1/check-runs"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };

                client.GetAllForCheckSuite("fake", "repo", 1, request);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/1/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 3
                            && x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };

                client.GetAllForCheckSuite(1, 1, request);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/1/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 3
                            && x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                client.GetAllForCheckSuite("fake", "repo", 1, request, options);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-suites/1/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 4
                            && x["per_page"] == "1"
                            && x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build", Filter = CheckRunCompletedAtFilter.Latest, Status = CheckStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                client.GetAllForCheckSuite(1, 1, request, options);

                connection.Received().Get<List<CheckRunsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-suites/1/check-runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 4
                            && x["per_page"] == "1"
                            && x["check_name"] == "build"
                            && x["status"] == "in_progress"
                            && x["filter"] == "latest"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build" };

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite(null, "repo", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", null, 1));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite(null, "repo", 1, request));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", null, 1, request));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", "repo", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite(null, "repo", 1, request, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", null, 1, request, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", "repo", 1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite("fake", "repo", 1, request, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite(1, 1, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite(1, 1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCheckSuite(1, 1, request, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var request = new CheckRunRequest { CheckName = "build" };

                Assert.Throws<ArgumentException>(() => client.GetAllForCheckSuite("", "repo", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllForCheckSuite("fake", "", 1));

                Assert.Throws<ArgumentException>(() => client.GetAllForCheckSuite("", "repo", 1, request));
                Assert.Throws<ArgumentException>(() => client.GetAllForCheckSuite("fake", "", 1, request));

                Assert.Throws<ArgumentException>(() => client.GetAllForCheckSuite("", "repo", 1, request, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForCheckSuite("fake", "", 1, request, ApiOptions.None));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                client.Get("fake", "repo", 1);

                gitHubClient.Check.Run.Received().Get("fake", "repo", 1);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                client.Get(1, 1);

                gitHubClient.Check.Run.Received().Get(1, 1);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "repo", 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("fake", null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                Assert.Throws<ArgumentException>(() => client.Get("", "repo", 1));
                Assert.Throws<ArgumentException>(() => client.Get("fake", "", 1));
            }
        }

        public class TheGetAllAnnotationsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                client.GetAllAnnotations("fake", "repo", 1);

                connection.Received().Get<List<CheckRunAnnotation>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-runs/1/annotations"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                client.GetAllAnnotations(1, 1);

                connection.Received().Get<List<CheckRunAnnotation>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-runs/1/annotations"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var options = new ApiOptions { PageSize = 1 };

                client.GetAllAnnotations("fake", "repo", 1, options);

                connection.Received().Get<List<CheckRunAnnotation>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-runs/1/annotations"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 1
                            && x["per_page"] == "1"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableCheckRunsClient(gitHubClient);

                var options = new ApiOptions { PageSize = 1 };

                client.GetAllAnnotations(1, 1, options);

                connection.Received().Get<List<CheckRunAnnotation>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-runs/1/annotations"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 1
                            && x["per_page"] == "1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllAnnotations(null, "repo", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllAnnotations("fake", null, 1));

                Assert.Throws<ArgumentNullException>(() => client.GetAllAnnotations(null, "repo", 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllAnnotations("fake", null, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllAnnotations("fake", "repo", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllAnnotations(1, 1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                Assert.Throws<ArgumentException>(() => client.GetAllAnnotations("", "repo", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllAnnotations("fake", "", 1));

                Assert.Throws<ArgumentException>(() => client.GetAllAnnotations("", "repo", 1, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllAnnotations("fake", "", 1, ApiOptions.None));
            }
        }
    }
}