using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EventsClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAll();

                connection.Received().GetAll<Activity>(
                    Arg.Is<Uri>(u => u.ToString() == "events"),
                    Args.ApiOptions);
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForRepository(null, "name"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForRepository("", "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForRepository("owner", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForRepository("owner", ""));
            }
        }

        public class TheGetAllForRepositoryNetworkMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllForRepositoryNetwork("fake", "repo");

                connection.Received().GetAll<Activity>(
                    Arg.Is<Uri>(u => u.ToString() == "networks/fake/repo/events"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForRepositoryNetwork(null, "name"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForRepositoryNetwork("", "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForRepositoryNetwork("owner", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForRepositoryNetwork("owner", ""));
            }
        }

        public class TheGetAllForOrganizationMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllForOrganization("fake");

                connection.Received().GetAll<Activity>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/fake/events"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForOrganization(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForOrganization(""));
            }
        }

        public class TheGetUserReceivedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllUserReceived("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllUserReceived(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllUserReceived(""));
            }
        }

        public class TheGetUserReceivedPublicMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllUserReceivedPublic("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events/public"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllUserReceivedPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllUserReceivedPublic(""));
            }
        }

        public class TheGetUserPerformedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllUserPerformed("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllUserPerformed(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllUserPerformed(""));
            }
        }

        public class TheGetUserPerformedPublicMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllUserPerformedPublic("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/public"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllUserPerformedPublic(null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllUserPerformedPublic(""));
            }
        }

        public class TheGetForAnOrganizationMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                client.GetAllForAnOrganization("fake", "org");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/orgs/org"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForAnOrganization(null, "org"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForAnOrganization("", "org"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForAnOrganization("fake", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForAnOrganization("fake", ""));
            }
        }

        private readonly Dictionary<string, Type> _activityTypes = new Dictionary<string, Type>
        {
            {"CommitCommentEvent", typeof(CommitCommentPayload)},
            {"ForkEvent", typeof(ForkEventPayload)},
            {"IssueCommentEvent", typeof(IssueCommentPayload)},
            {"IssuesEvent", typeof(IssueEventPayload)},
            {"PullRequestEvent", typeof(PullRequestEventPayload)},
            {"PullRequestReviewCommentEvent", typeof(PullRequestCommentPayload)},
            {"PushEvent", typeof(PushEventPayload)},
            {"WatchEvent", typeof(StarredEventPayload)},
            {"unknown", typeof(ActivityPayload)}
        };
        
        [Fact]
        public async Task DeserializesPayloadToCorrectType()
        {
            _activityTypes.ToList().ForEach(async kvp =>
            {
                var jsonObj = new JsonObject {{ "type", kvp.Key }, {"payload", new
                {
                    repository = new
                    {
                        id = 1337
                    },
                    sender = new
                    {
                       id = 1337 
                    }
                }}};

                var client = GetTestingEventsClient(jsonObj);

                var activities = await client.GetAll();
                Assert.Equal(1, activities.Count);
                var activity = activities.FirstOrDefault();
                Assert.Equal(kvp.Value, activity.Payload.GetType());
                Assert.NotNull(activity.Payload.Repository);
                Assert.NotNull(activity.Payload.Sender);
                Assert.Equal(1337, activity.Payload.Repository.Id);
                Assert.Equal(1337, activity.Payload.Sender.Id);
            });
        }

        [Fact]
        public async Task DeserializesCommitCommentEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "CommitCommentEvent" },
                {
                    "payload", new
                    {
                        comment = new
                        {
                            id = 1337
                        }
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as CommitCommentPayload;
            Assert.Equal(1337, payload.Comment.Id);
        }

        [Fact]
        public async Task DeserializesForkEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "ForkEvent" },
                {
                    "payload", new
                    {
                        forkee = new
                        {
                            id = 1337
                        }
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as ForkEventPayload;
            Assert.Equal(1337, payload.Forkee.Id);
        }

        [Fact]
        public async Task DeserializesIssueCommentEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "IssueCommentEvent" },
                {
                    "payload", new
                    {
                        action = "created",
                        issue = new
                        {
                            number = 1337
                        },
                        comment = new
                        {
                            id = 1337
                        }
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as IssueCommentPayload;
            Assert.Equal("created", payload.Action);
            Assert.Equal(1337, payload.Comment.Id);
            Assert.Equal(1337, payload.Issue.Number);
        }

        [Fact]
        public async Task DeserializesIssueEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "IssuesEvent" },
                {
                    "payload", new
                    {
                        action = "assigned",
                        issue = new
                        {
                            number = 1337
                        },
                        assignee = new
                        {
                            id = 1337
                        },
                        label = new
                        {
                            name = "bug"
                        }
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as IssueEventPayload;
            Assert.Equal("assigned", payload.Action);
            Assert.Equal(1337, payload.Issue.Number);
            Assert.Equal(1337, payload.Assignee.Id);
            Assert.Equal("bug", payload.Label.Name);
        }

        [Fact]
        public async Task DeserializesPullRequestEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "PullRequestEvent" },
                {
                    "payload", new
                    {
                        action = "assigned",
                        number = 1337,
                        pull_request = new
                        {
                            title = "PR Title"
                        }
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as PullRequestEventPayload;
            Assert.Equal("assigned", payload.Action);
            Assert.Equal(1337, payload.Number);
            Assert.Equal("PR Title", payload.PullRequest.Title);
        }

        [Fact]
        public async Task DeserializesPullRequestCommentEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "PullRequestReviewCommentEvent" },
                {
                    "payload", new
                    {
                        action = "assigned",
                        pull_request = new
                        {
                            title = "PR Title"
                        },
                        comment = new
                        {
                            id = 1337
                        }
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as PullRequestCommentPayload;
            Assert.Equal("assigned", payload.Action);
            Assert.Equal("PR Title", payload.PullRequest.Title);
            Assert.Equal(1337, payload.Comment.Id);
        }

        [Fact]
        public async Task DeserializesPushEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "PushEvent" },
                {
                    "payload", new
                    {
                        head = "head",
                        @ref = "ref",
                        size = 1337,
                        commits = new []
                        {
                            new
                            {
                                message = "message"
                            }
                        }
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as PushEventPayload;
            Assert.Equal("head", payload.Head);
            Assert.Equal("ref", payload.Ref);
            Assert.Equal(1337, payload.Size);
            Assert.NotNull(payload.Commits);
            Assert.Equal(1, payload.Commits.Count);
            Assert.Equal("message", payload.Commits.FirstOrDefault().Message);
        }

        [Fact]
        public async Task DeserializesStarredEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "WatchEvent" },
                {
                    "payload", new
                    {
                        action = "started"
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as StarredEventPayload;
            Assert.Equal("started", payload.Action);
        }

        private EventsClient GetTestingEventsClient(JsonObject response)
        {
            var responseString = response.ToString();
            var httpClientMock = Substitute.For<IHttpClient>();
            httpClientMock.Send(Arg.Is((IRequest r) => r.Endpoint.ToString().Contains("events")), Arg.Any<CancellationToken>()).Returns(Task.FromResult(
                new Response(HttpStatusCode.Accepted, responseString, new Dictionary<string, string>(), "application/json") as IResponse));

            return new EventsClient(new ApiConnection(new Connection(new ProductHeaderValue("mock"), httpClientMock)));
        }
    }
}
