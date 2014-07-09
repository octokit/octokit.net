using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

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
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableMilestonesClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get(null, "", 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", null, 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task ReturnsEveryPageOfMilestones()
            {
                var firstPageUrl = new Uri("repos/fake/repo/milestones", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> {{"next", secondPageUrl}};
                var firstPageResponse = new ApiResponse<List<Milestone>>
                {
                    BodyAsObject = new List<Milestone>
                    {
                        new Milestone {Number = 1},
                        new Milestone {Number = 2},
                        new Milestone {Number = 3},
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> {{"next", thirdPageUrl}};
                var secondPageResponse = new ApiResponse<List<Milestone>>
                {
                    BodyAsObject = new List<Milestone>
                    {
                        new Milestone {Number = 4},
                        new Milestone {Number = 5},
                        new Milestone {Number = 6},
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<Milestone>>
                {
                    BodyAsObject = new List<Milestone>
                    {
                        new Milestone {Number = 7},
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<Milestone>>(firstPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<Milestone>>>(() => firstPageResponse));
                gitHubClient.Connection.Get<List<Milestone>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<Milestone>>>(() => secondPageResponse));
                gitHubClient.Connection.Get<List<Milestone>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<Milestone>>>(() => lastPageResponse));
                var client = new ObservableMilestonesClient(gitHubClient);

                var results = await client.GetForRepository("fake", "repo").ToArray();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.BodyAsObject[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.BodyAsObject[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.BodyAsObject[0].Number, results[6].Number);
            }

            [Fact]
            public async Task SendsAppropriateParameters()
            {
                var firstPageUrl = new Uri("repos/fake/repo/milestones", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> {{"next", secondPageUrl}};
                var firstPageResponse = new ApiResponse<List<Milestone>>
                {
                    BodyAsObject = new List<Milestone>
                    {
                        new Milestone {Number = 1},
                        new Milestone {Number = 2},
                        new Milestone {Number = 3},
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> {{"next", thirdPageUrl}};
                var secondPageResponse = new ApiResponse<List<Milestone>>
                {
                    BodyAsObject = new List<Milestone>
                    {
                        new Milestone {Number = 4},
                        new Milestone {Number = 5},
                        new Milestone {Number = 6},
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<Milestone>>
                {
                    BodyAsObject = new List<Milestone>
                    {
                        new Milestone {Number = 7},
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<Milestone>>(Arg.Is(firstPageUrl),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"), Arg.Any<string>())
                    .Returns(Task.Factory.StartNew<IResponse<List<Milestone>>>(() => firstPageResponse));
                gitHubClient.Connection.Get<List<Milestone>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<Milestone>>>(() => secondPageResponse));
                gitHubClient.Connection.Get<List<Milestone>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<Milestone>>>(() => lastPageResponse));
                var client = new ObservableMilestonesClient(gitHubClient);

                var results = await client.GetForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending }).ToArray();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.BodyAsObject[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.BodyAsObject[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.BodyAsObject[0].Number, results[6].Number);
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
            public void EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));
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
            public void EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));
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
            public void EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMilestonesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 42));
                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 42));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 42));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 42));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new MilestonesClient(null));
            }
        }

        static ApiInfo CreateApiInfo(IDictionary<string, Uri> links)
        {
            return new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>()));
        }
    }
}
