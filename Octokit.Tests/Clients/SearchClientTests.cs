using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;
using System.Collections.Generic;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class SearchClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new SearchClient(null));
            }
        }

        public class TheSearchUsersMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                client.SearchUsers(new SearchUsersRequest("something"));
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchUsers(null));
            }

            [Fact]
            public void TestingTheAccountTypeQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.AccountType = AccountType.User;
                client.SearchUsers(request);
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }
            
            [Fact]
            public void TestingTheInQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get users where the fullname contains 'github'
                var request = new SearchUsersRequest("github");
                request.In = new[] { UserInQualifier.Fullname };
                client.SearchUsers(request);
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheReposQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Repositories = Range.GreaterThan(5);
                client.SearchUsers(request);
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheLocationQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Location = "San Francisco";
                client.SearchUsers(request);
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheLanguageQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get users who have mostly repos where language is Ruby
                var request = new SearchUsersRequest("github");
                request.Language = Language.Ruby;
                client.SearchUsers(request);
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheCreatedQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.GreaterThan(DateTime.Today.AddYears(-25));
                client.SearchUsers(request);
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheFollowersQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Followers = Range.GreaterThan(1);
                client.SearchUsers(request);
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }
        }

        public class TheSearchRepoMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                client.SearchRepo(new SearchRepositoriesRequest("something"));
                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchRepo(null));
            }

            [Fact]
            public void TestingTheSizeQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //check sizes for repos that are greater than 50 MB
                var request = new SearchRepositoriesRequest("github");
                request.Size = Range.GreaterThan(50);

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheStarsQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos whos stargazers are greater than 500
                var request = new SearchRepositoriesRequest("github");
                request.Stars = Range.GreaterThan(500);

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheForksQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos which has forks that are greater than 50
                var request = new SearchRepositoriesRequest("github");
                request.Forks = Range.GreaterThan(50);

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheForkQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //search repos that contains rails and forks are included in the search
                var request = new SearchRepositoriesRequest("rails");
                request.Fork = ForkQualifier.IncludeForks;

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheLangaugeQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos whos language is Ruby
                var request = new SearchRepositoriesRequest("github");
                request.Language = Language.Ruby;

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheInQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the Description contains the test 'github'
                var request = new SearchRepositoriesRequest("github");
                request.In = new[] { InQualifier.Description };
                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheCreatedQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been created after year jan 1 2011
                var request = new SearchRepositoriesRequest("github");
                request.Created = DateRange.GreaterThan(new DateTime(2011, 1, 1));

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheUpdatedQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been pushed before year jan 1 2013
                var request = new SearchRepositoriesRequest("github");
                request.Updated = DateRange.LessThan(new DateTime(2013, 1, 1));

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheUserQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the Description contains rails and user/org is 'github'
                var request = new SearchRepositoriesRequest("rails");
                request.User = "github";

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void TestingTheSortParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the Description contains rails and user/org is 'github'
                var request = new SearchRepositoriesRequest("rails");
                request.Sort = RepoSearchSort.Forks;

                client.SearchRepo(request);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }
        }

        public class TheSearchIssuesMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                client.SearchIssues(new SearchIssuesRequest("something"));
                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "search/issues"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchIssues(null));
            }
        }

        public class TheSearchCodeMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                client.SearchCode(new SearchCodeRequest("something"));
                connection.Received().GetAll<SearchCode>(Arg.Is<Uri>(u => u.ToString() == "search/code"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchCode(null));
            }
        }
    }
}
