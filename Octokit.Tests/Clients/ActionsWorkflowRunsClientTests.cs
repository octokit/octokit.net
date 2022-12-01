using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ActionsWorkflowRunsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ActionsWorkflowRunsClient(null));
            }
        }

        public class TheApproveMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.Approve("fake", "repo", 123);

                connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/approve"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Approve(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Approve("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Approve("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Approve("fake", "", 123));
            }
        }

        public class TheCancelMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.Cancel("fake", "repo", 123);

                connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/cancel"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Cancel(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Cancel("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Cancel("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Cancel("fake", "", 123));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.Delete("fake", "repo", 123);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("fake", "", 123));
            }
        }

        public class TheDeleteLogsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.DeleteLogs("fake", "repo", 123);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/logs"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteLogs(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteLogs("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteLogs("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteLogs("fake", "", 123));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.Get("fake", "repo", 123);

                connection.Received().Get<WorkflowRun>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("fake", "", 123));
            }
        }

        public class TheGetLogsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var headers = new Dictionary<string, string>();
                var response = TestSetup.CreateResponse(HttpStatusCode.OK, new byte[] { 1, 2, 3, 4 }, headers);
                var responseTask = Task.FromResult<IApiResponse<byte[]>>(new ApiResponse<byte[]>(response));

                var connection = Substitute.For<IConnection>();
                connection.GetRaw(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/logs"), null)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new ActionsWorkflowRunsClient(apiConnection);

                var actual = await client.GetLogs("fake", "repo", 123);

                Assert.Equal(new byte[] { 1, 2, 3, 4 }, actual);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetLogs(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetLogs("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetLogs("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetLogs("fake", "", 123));
            }
        }

        public class TheGetAttemptMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.GetAttempt("fake", "repo", 123, 456);

                connection.Received().Get<WorkflowRun>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/attempts/456"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAttempt(null, "repo", 123, 456));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAttempt("fake", null, 123, 456));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAttempt("", "repo", 123, 456));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAttempt("fake", "", 123, 456));
            }
        }

        public class TheGetAttemptLogsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var headers = new Dictionary<string, string>();
                var response = TestSetup.CreateResponse(HttpStatusCode.OK, new byte[] { 1, 2, 3, 4 }, headers);
                var responseTask = Task.FromResult<IApiResponse<byte[]>>(new ApiResponse<byte[]>(response));

                var connection = Substitute.For<IConnection>();
                connection.GetRaw(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/attempts/456/logs"), null)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new ActionsWorkflowRunsClient(apiConnection);

                var actual = await client.GetAttemptLogs("fake", "repo", 123, 456);
                
                Assert.Equal(new byte[] { 1, 2, 3, 4 }, actual);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAttemptLogs(null, "repo", 123, 456));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAttemptLogs("fake", null, 123, 456));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAttemptLogs("", "repo", 123, 456));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAttemptLogs("fake", "", 123, 456));
            }
        }

        public class TheGetReviewHistoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.GetReviewHistory("fake", "repo", 123);

                connection.Received().GetAll<EnvironmentApprovals>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/approvals"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var workflowRunsRequest = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReviewHistory(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReviewHistory("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var workflowRunsRequest = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReviewHistory("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReviewHistory("fake", "", 123));
            }
        }

        public class TheGetUsageMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.GetUsage("fake", "repo", 123);

                connection.Received().Get<WorkflowRunUsage>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/timing"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUsage(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUsage("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetUsage("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetUsage("fake", "", 123));
            }
        }

        public class TheListMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.List("fake", "repo");

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest
                {
                    Actor = "octocat",
                    Branch = "main",
                    CheckSuiteId = 42,
                    Created = "2020-2022",
                    Event = "push",
                    ExcludePullRequests = true,
                    Status = CheckRunStatusFilter.InProgress,
                };

                await client.List("fake", "repo", request);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 7
                            && x["actor"] == "octocat"
                            && x["branch"] == "main"
                            && x["check_suite_id"] == "42"
                            && x["created"] == "2020-2022"
                            && x["event"] == "push"
                            && x["branch"] == "main"
                            && x["exclude_pull_requests"] == "true"
                            && x["status"] == "in_progress"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest { Branch = "main", CheckSuiteId = 42, Status = CheckRunStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                await client.List("fake", "repo", request, options);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 3
                            && x["branch"] == "main"
                            && x["status"] == "in_progress"
                            && x["check_suite_id"] == "42"),
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var workflowRunsRequest = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List(null, "repo", workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", null, workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List(null, "repo", workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", null, workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", "repo", null, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", "repo", workflowRunsRequest, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var workflowRunsRequest = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentException>(() => client.List("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.List("fake", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.List("", "repo", workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.List("fake", "", workflowRunsRequest));

                await Assert.ThrowsAsync<ArgumentException>(() => client.List("", "repo", workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.List("fake", "", workflowRunsRequest, options));
            }
        }

        public class TheListByWorkflowMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.ListByWorkflow("fake", "repo", 123);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/123/runs"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await client.ListByWorkflow("fake", "repo", "main.yml");

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/main.yml/runs"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithIdWithRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest
                {
                    Actor = "octocat",
                    Branch = "main",
                    CheckSuiteId = 42,
                    Created = "2020-2022",
                    Event = "push",
                    ExcludePullRequests = true,
                    Status = CheckRunStatusFilter.InProgress,
                };

                await client.ListByWorkflow("fake", "repo", 123, request);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/123/runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 7
                            && x["actor"] == "octocat"
                            && x["branch"] == "main"
                            && x["check_suite_id"] == "42"
                            && x["created"] == "2020-2022"
                            && x["event"] == "push"
                            && x["branch"] == "main"
                            && x["exclude_pull_requests"] == "true"
                            && x["status"] == "in_progress"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithNameWithRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest
                {
                    Actor = "octocat",
                    Branch = "main",
                    CheckSuiteId = 42,
                    Created = "2020-2022",
                    Event = "push",
                    ExcludePullRequests = true,
                    Status = CheckRunStatusFilter.InProgress,
                };

                await client.ListByWorkflow("fake", "repo", "main.yml", request);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/main.yml/runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 7
                            && x["actor"] == "octocat"
                            && x["branch"] == "main"
                            && x["check_suite_id"] == "42"
                            && x["created"] == "2020-2022"
                            && x["event"] == "push"
                            && x["branch"] == "main"
                            && x["exclude_pull_requests"] == "true"
                            && x["status"] == "in_progress"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithIdWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest { Branch = "main", CheckSuiteId = 42, Status = CheckRunStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                await client.ListByWorkflow("fake", "repo", 123, request, options);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/123/runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 3
                            && x["branch"] == "main"
                            && x["status"] == "in_progress"
                            && x["check_suite_id"] == "42"),
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithNameWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest { Branch = "main", CheckSuiteId = 42, Status = CheckRunStatusFilter.InProgress };
                var options = new ApiOptions { PageSize = 1 };

                await client.ListByWorkflow("fake", "repo", "main.yml", request, options);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/main.yml/runs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 3
                            && x["branch"] == "main"
                            && x["status"] == "in_progress"
                            && x["check_suite_id"] == "42"),
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var workflowRunsRequest = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", null, 123));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow(null, "repo", "main.yml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", null, "main.yml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow(null, "repo", 123, workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", null, 123, workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", 123, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow(null, "repo", "main.yml", workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", null, "main.yml", workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", null, workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", "main.yml", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow(null, "repo", 123, workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", null, 123, workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", 123, null, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", 123, workflowRunsRequest, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow(null, "repo", "main.yml", workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", null, "main.yml", workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", null, workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", "main.yml", null, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListByWorkflow("fake", "repo", "main.yml", workflowRunsRequest, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var workflowRunsRequest = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "", 123));

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("", "repo", "main.yml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "", "main.yml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("", "repo", 123, workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "", 123, workflowRunsRequest));

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("", "repo", "main.yml", workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "", "main.yml", workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "repo", "", workflowRunsRequest));

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("", "repo", "main.yml", workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "", "main.yml", workflowRunsRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "repo", "", workflowRunsRequest));

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("", "repo", "main.yml", workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "", "main.yml", workflowRunsRequest, options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListByWorkflow("fake", "repo", "", workflowRunsRequest, options));
            }
        }

        public class TheRerunMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                await client.Rerun("fake", "repo", 123);

                connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/rerun"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Rerun(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Rerun("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Rerun("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Rerun("fake", "", 123));
            }
        }

        public class TheRerunFailedJobsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                await client.RerunFailedJobs("fake", "repo", 123);

                connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/rerun-failed-jobs"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RerunFailedJobs(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RerunFailedJobs("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.RerunFailedJobs("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RerunFailedJobs("fake", "", 123));
            }
        }

        public class TheReviewFailedJobsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var review = new PendingDeploymentReview(new[] { 1L }, PendingDeploymentReviewState.Approved, "");

                await client.ReviewPendingDeployments("fake", "repo", 123, review);

                connection.Received().Post<Deployment>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/pending_deployments"),
                    review);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var review = new PendingDeploymentReview(new[] { 1L }, PendingDeploymentReviewState.Approved, "Ship it!");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReviewPendingDeployments(null, "repo", 123, review));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReviewPendingDeployments("fake", null, 123, review));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReviewPendingDeployments("fake", "repo", 123, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowRunsClient(connection);

                var review = new PendingDeploymentReview(new[] { 1L }, PendingDeploymentReviewState.Approved, "Ship it!");

                await Assert.ThrowsAsync<ArgumentException>(() => client.ReviewPendingDeployments("", "repo", 123, review));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ReviewPendingDeployments("fake", "", 123, review));
            }
        }
    }
}
