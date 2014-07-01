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
                connection.Received().Get<SearchUsersResult>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => client.SearchUsers(null));
            }

            [Fact]
            public void TestingTheTermParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github"));
            }

            [Fact]
            public void TestingTheAccountTypeQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.AccountType = AccountType.User;
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+type:User"));
            }

            [Fact]
            public void TestingTheAccountTypeQualifier_Org()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.AccountType = AccountType.Org;
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+type:Org"));
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
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+in:Fullname"));
            }

            [Fact]
            public void TestingTheInQualifier_Email()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.In = new[] { UserInQualifier.Email };
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+in:Email"));
            }

            [Fact]
            public void TestingTheInQualifier_Username()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.In = new[] { UserInQualifier.Username };
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+in:Username"));
            }

            [Fact]
            public void TestingTheInQualifier_Multiple()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.In = new[] { UserInQualifier.Username, UserInQualifier.Fullname, UserInQualifier.Email };
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+in:Username,Fullname,Email"));
            }

            [Fact]
            public void TestingTheReposQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Repositories = Range.GreaterThan(5);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+repos:>5"));
            }

            [Fact]
            public void TestingTheReposQualifier_GreaterThanOrEqualTo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Repositories = Range.GreaterThanOrEquals(5);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+repos:>=5"));
            }

            [Fact]
            public void TestingTheReposQualifier_LessThanOrEqualTo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Repositories = Range.LessThanOrEquals(5);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+repos:<=5"));
            }

            [Fact]
            public void TestingTheReposQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Repositories = Range.LessThan(5);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+repos:<5"));
            }

            [Fact]
            public void TestingTheLocationQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Location = "San Francisco";
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+location:San Francisco"));
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
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+language:Ruby"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.GreaterThan(new DateTime(2014, 1, 1));
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:>2014-01-01"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThanOrEqualTo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.GreaterThanOrEquals(new DateTime(2014, 1, 1));
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:>=2014-01-01"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThanOrEqualTo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.LessThanOrEquals(new DateTime(2014, 1, 1));
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:<=2014-01-01"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.LessThan(new DateTime(2014, 1, 1));
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:<2014-01-01"));
            }

            [Fact]
            public void TestingTheFollowersQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Followers = Range.GreaterThan(1);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+followers:>1"));
            }

            [Fact]
            public void TestingTheFollowersQualifier_GreaterThanOrEqualTo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Followers = Range.GreaterThanOrEquals(1);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+followers:>=1"));
            }

            [Fact]
            public void TestingTheFollowersQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Followers = Range.LessThan(1);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+followers:<1"));
            }

            [Fact]
            public void TestingTheFollowersQualifier_LessThanOrEqualTo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Followers = Range.LessThanOrEquals(1);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+followers:<=1"));
            }

            [Fact]
            public void TestingTheFollowersQualifier_Range()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Followers = new Range(1, 1000);
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+followers:1..1000"));
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
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => client.SearchRepo(null));
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
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
                connection.Received().Get<SearchIssuesResult>(Arg.Is<Uri>(u => u.ToString() == "search/issues"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => client.SearchIssues(null));
            }

            [Fact]
            public void TestingTheTermParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("pub");

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "pub"));
            }

            [Fact]
            public void TestingTheSortParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.SortField = IssueSearchSort.Comments;

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["sort"] == "comments"));
            }

            [Fact]
            public void TestingTheOrderParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.SortField = IssueSearchSort.Updated;
                request.Order = SortDirection.Ascending;

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["sort"] == "updated" &&
                        d["order"] == "asc"));
            }

            [Fact]
            public void TestingTheDefaultOrderParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["order"] == "desc"));
            }

            [Fact]
            public void TestingTheInQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.In = new[] { IssueInQualifier.Comment };

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+in:comment"));
            }

            [Fact]
            public void TestingTheInQualifiers_Multiple()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.In = new[] { IssueInQualifier.Body, IssueInQualifier.Title };

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+in:body,title"));
            }

            [Fact]
            public void TestingTheTypeQualifier_Issue()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Type = IssueTypeQualifier.Issue;

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+type:issue"));
            }

            [Fact]
            public void TestingTheTypeQualifier_PR()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Type = IssueTypeQualifier.PR;

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+type:pr"));
            }

            [Fact]
            public void TestingTheAuthorQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Author = "alfhenrik";

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+author:alfhenrik"));
            }

            [Fact]
            public void TestingTheAssigneeQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Assignee = "alfhenrik";

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+assignee:alfhenrik"));
            }

            [Fact]
            public void TestingTheMentionsQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Mentions = "alfhenrik";

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+mentions:alfhenrik"));
            }

            [Fact]
            public void TestingTheCommenterQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Commenter = "alfhenrik";

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+commenter:alfhenrik"));
            }

            [Fact]
            public void TestingTheInvolvesQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Involves = "alfhenrik";

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+involves:alfhenrik"));
            }

            [Fact]
            public void TestingTheStateQualifier_Open()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.State = ItemState.Open;

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+state:open"));
            }

            [Fact]
            public void TestingTheStateQualifier_Closed()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.State = ItemState.Closed;

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+state:closed"));
            }

            [Fact]
            public void TestingTheLabelsQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Labels = new[] { "bug" };

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+label:bug"));
            }

            [Fact]
            public void TestingTheLabelsQualifier_Multiple()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Labels = new[] { "bug", "feature" };

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+label:bug+label:feature"));
            }

            [Fact]
            public void TestingTheLanguageQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Language = Language.CSharp;

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+language:CSharp"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.GreaterThan(new DateTime(2014, 1, 1));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:>2014-01-01"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.GreaterThanOrEquals(new DateTime(2014, 1, 1));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:>=2014-01-01"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.LessThan(new DateTime(2014, 1, 1));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:<2014-01-01"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.LessThanOrEquals(new DateTime(2014, 1, 1));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:<=2014-01-01"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.GreaterThan(new DateTime(2014, 1, 1));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:>2014-01-01"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_GreaterThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.GreaterThanOrEquals(new DateTime(2014, 1, 1));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:>=2014-01-01"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.LessThan(new DateTime(2014, 1, 1));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:<2014-01-01"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_LessThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.LessThanOrEquals(new DateTime(2014, 1, 1));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:<=2014-01-01"));
            }

            [Fact]
            public void TestingTheCommentsQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Comments = Range.GreaterThan(10);

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+comments:>10"));
            }

            [Fact]
            public void TestingTheCommentsQualifier_GreaterThanOrEqual()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Comments = Range.GreaterThanOrEquals(10);

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+comments:>=10"));
            }

            [Fact]
            public void TestingTheCommentsQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Comments = Range.LessThan(10);

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+comments:<10"));
            }

            [Fact]
            public void TestingTheCommentsQualifier_LessThanOrEqual()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Comments = Range.LessThanOrEquals(10);

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+comments:<=10"));
            }

            [Fact]
            public void TestingTheCommentsQualifier_Range()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Comments = new Range(10, 20);

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+comments:10..20"));
            }

            [Fact]
            public void TestingTheUserQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.User = "alfhenrik";

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+user:alfhenrik"));
            }

            [Fact]
            public void TestingTheRepoQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Repo = "octokit.net";

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+repo:octokit.net"));
            }

            [Fact]
            public void TestingTheRepoAndUserAndLabelQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Repo = "octokit.net";
                request.User = "alfhenrik";
                request.Labels = new[] { "bug" };

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] ==
                        "something+label:bug+user:alfhenrik+repo:octokit.net"));
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
                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => client.SearchCode(null));
            }

            [Fact]
            public void TestingTheTermParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something"));
            }

            [Fact]
            public void TestingTheSortParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.SortField = CodeSearchSort.Indexed;

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["sort"] == "indexed"));
            }

            [Fact]
            public void TestingTheOrderParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.SortField = CodeSearchSort.Indexed;
                request.Order = SortDirection.Ascending;

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["sort"] == "indexed" &&
                        d["order"] == "asc"));
            }

            [Fact]
            public void TestingTheDefaultOrderParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["order"] == "desc"));
            }

            [Fact]
            public void TestingTheInQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.In = new[] { CodeInQualifier.File };


                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+in:file"));
            }

            [Fact]
            public void TestingTheInQualifier_Multiple()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.In = new[] { CodeInQualifier.File, CodeInQualifier.Path };

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+in:file,path"));
            }

            [Fact]
            public void TestingTheLanguageQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Language = Language.CSharp;

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+language:C#"));
            }

            [Fact]
            public void TestingTheForksQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Forks = true;

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+fork:true"));
            }

            [Fact]
            public void TestingTheSizeQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Size = Range.GreaterThan(10);

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+size:>10"));
            }

            [Fact]
            public void TestingTheSizeQualifier_GreaterThanOrEqual()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Size = Range.GreaterThanOrEquals(10);

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+size:>=10"));
            }

            [Fact]
            public void TestingTheSizeQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Size = Range.LessThan(10);

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+size:<10"));
            }

            [Fact]
            public void TestingTheSizeQualifier_LessThanOrEqual()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Size = Range.LessThanOrEquals(10);

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+size:<=10"));
            }

            [Fact]
            public void TestingTheSizeQualifier_Range()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Size = new Range(10, 100);

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+size:10..100"));
            }

            [Fact]
            public void TestingThePathQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Path = "app/public";

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+path:app/public"));
            }

            [Fact]
            public void TestingTheExtensionQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Extension = "cs";

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+extension:cs"));
            }

            [Fact]
            public void TestingTheUserQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.User = "alfhenrik";

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+user:alfhenrik"));
            }

            [Fact]
            public void TestingTheRepoQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Repo = "octokit.net";

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+repo:octokit.net"));
            }

            [Fact]
            public void TestingTheRepoAndPathAndExtensionQualifiers()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Repo = "octokit.net";
                request.Path = "tools/FAKE.core";
                request.Extension = "fs";

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["q"] == "something+path:tools/FAKE.core+extension:fs+repo:octokit.net"));
            }
        }
    }
}
