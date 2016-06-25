using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class MilestonesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Get("fake", "repo", 42);

                connection.Received().Get<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Get(1, 42);

                connection.Received().Get<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new MilestonesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Any<Dictionary<string, string>>(), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones"),
                    Arg.Any<Dictionary<string, string>>(), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Any<Dictionary<string, string>>(), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository(1, options);

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones"),
                    Arg.Any<Dictionary<string, string>>(), options);
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.GetAllForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending });

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"), Args.ApiOptions);
            }

            [Fact]
            public void SendsAppropriateParametersWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.GetAllForRepository(1, new MilestoneRequest { SortDirection = SortDirection.Descending });

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"), Args.ApiOptions);
            }

            [Fact]
            public void SendsAppropriateParametersWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending }, options);

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"), options);
            }

            [Fact]
            public void SendsAppropriateParametersWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository(1, new MilestoneRequest { SortDirection = SortDirection.Descending }, options);

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new MilestonesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new MilestoneRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new MilestoneRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (MilestoneRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new MilestoneRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new MilestoneRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", new MilestoneRequest(), null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (MilestoneRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, new MilestoneRequest(), null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", new MilestoneRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", new MilestoneRequest()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", new MilestoneRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", new MilestoneRequest(), ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newMilestone = new NewMilestone("some title");
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Create("fake", "repo", newMilestone);

                connection.Received().Post<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    newMilestone);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var newMilestone = new NewMilestone("some title");
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Create(1, newMilestone);

                connection.Received().Post<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones"),
                    newMilestone);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var milestoneUpdate = new MilestoneUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Update("fake", "repo", 42, milestoneUpdate);

                connection.Received().Patch<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"),
                    milestoneUpdate);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var milestoneUpdate = new MilestoneUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Update(1, 42, milestoneUpdate);

                connection.Received().Patch<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones/42"),
                    milestoneUpdate);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Delete("fake", "repo", 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"));
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Delete(1, 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/milestones/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 42));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 42));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 42));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 42));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new MilestonesClient(null));
            }
        }
    }
}
