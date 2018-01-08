using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EventsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new EventsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAll();

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAll(options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/events"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository(1, options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
            }
        }

        public class TheGetAllIssuesForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllIssuesForRepository("fake", "repo");

                connection.Received().GetAll<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllIssuesForRepository(1);

                connection.Received().GetAll<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllIssuesForRepository("fake", "repo", options);

                connection.Received().GetAll<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/events"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllIssuesForRepository(1, options);

                connection.Received().GetAll<IssueEvent>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllIssuesForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllIssuesForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllIssuesForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllIssuesForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllIssuesForRepository("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllIssuesForRepository(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllIssuesForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllIssuesForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllIssuesForRepository("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllIssuesForRepository("", "name", ApiOptions.None));
            }
        }

        public class TheGetAllForRepositoryNetworkMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllForRepositoryNetwork("fake", "repo");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "networks/fake/repo/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllForRepositoryNetwork("fake", "repo", options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "networks/fake/repo/events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepositoryNetwork(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepositoryNetwork("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepositoryNetwork(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepositoryNetwork("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepositoryNetwork("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepositoryNetwork("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepositoryNetwork("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepositoryNetwork("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepositoryNetwork("", "name", ApiOptions.None));
            }
        }

        public class TheGetAllForOrganizationMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllForOrganization("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllForOrganization("fake", options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("fake", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", ApiOptions.None));
            }
        }

        public class TheGetUserReceivedMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllUserReceived("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllUserReceived("fake", options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserReceived(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserReceived("fake", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserReceived(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserReceived("", ApiOptions.None));
            }
        }

        public class TheGetUserReceivedPublicMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllUserReceivedPublic("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events/public"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllUserReceivedPublic("fake", options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/received_events/public"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserReceivedPublic(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserReceivedPublic("fake", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserReceivedPublic(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserReceivedPublic("", ApiOptions.None));
            }
        }

        public class TheGetUserPerformedMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllUserPerformed("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllUserPerformed("fake", options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserPerformed(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserPerformed("fake", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserPerformed(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserPerformed("", ApiOptions.None));
            }
        }

        public class TheGetUserPerformedPublicMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllUserPerformedPublic("fake");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/public"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllUserPerformedPublic("fake", options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/public"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserPerformedPublic(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllUserPerformedPublic("fake", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserPerformedPublic(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllUserPerformedPublic("", ApiOptions.None));
            }
        }

        public class TheGetForAnOrganizationMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await client.GetAllForAnOrganization("fake", "org");

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/orgs/org"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllForAnOrganization("fake", "org", options);

                connection.Received().GetAll<Activity>(Arg.Is<Uri>(u => u.ToString() == "users/fake/events/orgs/org"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EventsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForAnOrganization(null, "org"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForAnOrganization("fake", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForAnOrganization(null, "org", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForAnOrganization("fake", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForAnOrganization("fake", "org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForAnOrganization("", "org"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForAnOrganization("fake", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForAnOrganization("fake", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForAnOrganization("", "org", ApiOptions.None));
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
            {"StatusEvent", typeof(StatusEventPayload)},
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
                        action = "created",
                        issue = new
                        {
                            number = 1337,
                            assignee = new
                            {
                                id = 1337
                            },
                            labels = new[]
                            {
                                new { name = "bug"}
                            }
                        }
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as IssueEventPayload;
            Assert.Equal("created", payload.Action);
            Assert.Equal(1337, payload.Issue.Number);
            Assert.Equal(1337, payload.Issue.Assignee.Id);
            Assert.Equal("bug", payload.Issue.Labels.First().Name);
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
        public async Task DeserializesStatusEventCorrectly()
        {
            var jsonObj = new JsonObject
            {
                { "type", "StatusEvent" },
                {
                    "payload", new
                    {
                        id = 214015194,
                        sha = "9049f1265b7d61be4a8904a9a27120d2064dab3b",
                        name = "baxterthehacker/public-repo",
                        target_url = "https://www.some_target_url.com",
                        context = "default",
                        description = "some human readable text",
                        state = "success",
                        branches = new []
                        {
                            new
                            {
                                name = "master",
                                commit = new
                                {
                                    sha = "9049f1265b7d61be4a8904a9a27120d2064dab3b",
                                    url = "https://api.github.com/repos/baxterthehacker/public-repo/commits/9049f1265b7d61be4a8904a9a27120d2064dab3b"
                                }
                            }
                        },
                        created_at = "2015-05-05T23:40:39Z"
                    }
                }
            };

            var client = GetTestingEventsClient(jsonObj);
            var activities = await client.GetAll();
            Assert.Equal(1, activities.Count);

            var payload = activities.FirstOrDefault().Payload as StatusEventPayload;
            Assert.Equal(214015194, payload.Id);
            Assert.Equal("9049f1265b7d61be4a8904a9a27120d2064dab3b", payload.Sha);
            Assert.Equal("baxterthehacker/public-repo", payload.Name);
            Assert.Equal("https://www.some_target_url.com", payload.TargetUrl);
            Assert.Equal("default", payload.Context);
            Assert.Equal("some human readable text", payload.Description);
            Assert.Equal(CommitState.Success, payload.State.Value);
            Assert.Equal(1, payload.Branches.Count);
            Assert.Equal(new DateTimeOffset(2015, 05, 05, 23, 40, 39, TimeSpan.Zero), payload.CreatedAt);
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
