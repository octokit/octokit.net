using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Reactive
{
    public class ObservableMilestonesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void GetsFromClientIssueMilestone()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.Get("fake", "repo", 42);

                gitHubClient.Issue.Milestone.Received().Get("fake", "repo", 42);
            }

            [Fact]
            public void GetsFromClientIssueMilestoneWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.Get(1, 42);

                gitHubClient.Issue.Milestone.Received().Get(1, 42);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableMilestonesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", null, 1).ToTask());
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                gitHubClient.Received().Issue.Milestone.GetAllForRepository("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.GetAllForRepository(1);

                gitHubClient.Received().Issue.Milestone.GetAllForRepository(1);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository("fake", "repo", options);

                gitHubClient.Received().Issue.Milestone.GetAllForRepository("fake", "repo", options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository(1, options);

                gitHubClient.Received().Issue.Milestone.GetAllForRepository(1, options);
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                var milestoneRequest = new MilestoneRequest { SortDirection = SortDirection.Descending };
                client.GetAllForRepository("fake", "repo", milestoneRequest);

                gitHubClient.Received().Issue.Milestone.GetAllForRepository("fake", "repo", milestoneRequest, Args.ApiOptions);
            }

            [Fact]
            public void SendsAppropriateParametersWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                var milestoneRequest = new MilestoneRequest { SortDirection = SortDirection.Descending };
                client.GetAllForRepository(1, milestoneRequest);

                gitHubClient.Received().Issue.Milestone.GetAllForRepository(1, milestoneRequest, Args.ApiOptions);
            }

            [Fact]
            public void SendsAppropriateParametersWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var milestoneRequest = new MilestoneRequest { SortDirection = SortDirection.Descending };
                client.GetAllForRepository("fake", "repo", milestoneRequest, options);

                gitHubClient.Received().Issue.Milestone.GetAllForRepository("fake", "repo", milestoneRequest, options);
            }

            [Fact]
            public void SendsAppropriateParametersWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var milestoneRequest = new MilestoneRequest { SortDirection = SortDirection.Descending };
                client.GetAllForRepository(1, milestoneRequest, options);

                gitHubClient.Received().Issue.Milestone.GetAllForRepository(1, milestoneRequest, options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableMilestonesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new MilestoneRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new MilestoneRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (MilestoneRequest)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new MilestoneRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new MilestoneRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", new MilestoneRequest(), null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (MilestoneRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, new MilestoneRequest(), null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", new MilestoneRequest()));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", new MilestoneRequest()));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", new MilestoneRequest(), ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", new MilestoneRequest(), ApiOptions.None));
            }

            [Fact]
            public async Task ReturnsEveryPageOfMilestones()
            {
                var firstPageUrl = new Uri("repos/fake/repo/milestones", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<Milestone>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<Milestone>
                    {
                        new Milestone(1),
                        new Milestone(2),
                        new Milestone(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<Milestone>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<Milestone>
                    {
                        new Milestone(4),
                        new Milestone(5),
                        new Milestone(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<Milestone>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<Milestone>
                    {
                        new Milestone(7)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<Milestone>>(firstPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<Milestone>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<Milestone>>(secondPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<Milestone>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<Milestone>>(thirdPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<Milestone>>>(lastPageResponse));
                var client = new ObservableMilestonesClient(gitHubClient);

                var results = await client.GetAllForRepository("fake", "repo").ToArray();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
            }

            [Fact]
            public async Task SendsAppropriateParametersMulti()
            {
                var firstPageUrl = new Uri("repos/fake/repo/milestones", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<Milestone>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<Milestone>
                    {
                        new Milestone(1),
                        new Milestone(2),
                        new Milestone(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<Milestone>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<Milestone>
                    {
                        new Milestone(4),
                        new Milestone(5),
                        new Milestone(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<Milestone>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<Milestone>
                    {
                        new Milestone(7)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<Milestone>>(Arg.Is(firstPageUrl),
                        Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                            && d["direction"] == "desc"
                            && d["state"] == "open"
                            && d["sort"] == "due_date"))
                    .Returns(Task.FromResult<IApiResponse<List<Milestone>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<Milestone>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"))
                    .Returns(Task.FromResult<IApiResponse<List<Milestone>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<Milestone>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"))
                    .Returns(Task.FromResult<IApiResponse<List<Milestone>>>(lastPageResponse));

                var client = new ObservableMilestonesClient(gitHubClient);

                var results = await client.GetAllForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending }).ToArray();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CreatesFromClientIssueMilestone()
            {
                var newMilestone = new NewMilestone("some title");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.Create("fake", "repo", newMilestone);

                gitHubClient.Issue.Milestone.Received().Create("fake", "repo", newMilestone);
            }

            [Fact]
            public void CreatesFromClientIssueMilestoneWithRepositoryId()
            {
                var newMilestone = new NewMilestone("some title");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.Create(1, newMilestone);

                gitHubClient.Issue.Milestone.Received().Create(1, newMilestone);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void UpdatesClientIssueMilestone()
            {
                var milestoneUpdate = new MilestoneUpdate();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.Update("fake", "repo", 42, milestoneUpdate);

                gitHubClient.Issue.Milestone.Received().Update("fake", "repo", 42, milestoneUpdate);
            }

            [Fact]
            public void UpdatesClientIssueMilestoneWithRepositoryId()
            {
                var milestoneUpdate = new MilestoneUpdate();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.Update(1, 42, milestoneUpdate);

                gitHubClient.Issue.Milestone.Received().Update(1, 42, milestoneUpdate);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", 42, new MilestoneUpdate()));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, 42, new MilestoneUpdate()));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", 42, null));

                Assert.Throws<ArgumentNullException>(() => client.Update(1, 42, null));

                Assert.Throws<ArgumentException>(() => client.Update("", "name", 42, new MilestoneUpdate()));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "", 42, new MilestoneUpdate()));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesFromClientIssueMilestone()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.Delete("fake", "repo", 42);

                gitHubClient.Issue.Milestone.Received().Delete("fake", "repo", 42);
            }

            [Fact]
            public void DeletesFromClientIssueMilestoneWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                client.Delete(1, 42);

                gitHubClient.Issue.Milestone.Received().Delete(1, 42);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 42));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 42));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 42));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 42));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableMilestonesClient(null));
            }
        }

        static IResponse CreateResponseWithApiInfo(IDictionary<string, Uri> links)
        {
            var response = Substitute.For<IResponse>();
            response.ApiInfo.Returns(new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>())));
            return response;
        }
    }
}
