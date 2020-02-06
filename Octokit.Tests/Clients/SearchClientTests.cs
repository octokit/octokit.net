﻿using System;
using System.Collections.Generic;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class SearchClientTests
    {
        public class TheCtor
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
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SearchUsers(null));
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
                request.AccountType = AccountSearchType.User;
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
                request.AccountType = AccountSearchType.Org;
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
                //get users who have mostly repos where language is Literate Haskell
                var request = new SearchUsersRequest("github");
                request.Language = Language.LiterateHaskell;
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+language:\"Literate Haskell\""));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.GreaterThan(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:>2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThanOrEqualTo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.GreaterThanOrEquals(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:>=2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThanOrEqualTo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.LessThanOrEquals(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:<=2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.LessThan(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));
                client.SearchUsers(request);
                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:<2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_Between()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchUsersRequest("github");
                request.Created = DateRange.Between(
                    new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero),
                    new DateTimeOffset(new DateTime(2014, 2, 1), TimeSpan.Zero));

                client.SearchUsers(request);

                connection.Received().Get<SearchUsersResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/users"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:2014-01-01T00:00:00+00:00..2014-02-01T00:00:00+00:00"));
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
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SearchRepo(null));
            }

            [Fact]
            public void TestingTheSizeQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchRepositoriesRequest("github");
                request.Size = Range.GreaterThan(1);
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+size:>1"));
            }

            [Fact]
            public void TestingTheStarsQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos who's stargazers are greater than 500
                var request = new SearchRepositoriesRequest("github");
                request.Stars = Range.GreaterThan(500);
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+stars:>500"));
            }

            [Fact]
            public void TestingTheStarsQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos who's stargazers are less than 500
                var request = new SearchRepositoriesRequest("github");
                request.Stars = Range.LessThan(500);
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+stars:<500"));
            }

            [Fact]
            public void TestingTheStarsQualifier_LessThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos who's stargazers are less than 500 or equal to
                var request = new SearchRepositoriesRequest("github");
                request.Stars = Range.LessThanOrEquals(500);
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+stars:<=500"));
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
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+forks:>50"));
            }

            [Fact]
            public void TestingTheIncludeForkQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //search repos that contains rails and forks are included in the search
                var request = new SearchRepositoriesRequest("github");
                request.Fork = ForkQualifier.IncludeForks;
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+fork:true"));
            }

            [Fact]
            public void TestingTheOnlyForkQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //search repos that contains rails and forks are included in the search
                var request = new SearchRepositoriesRequest("github");
                request.Fork = ForkQualifier.OnlyForks;
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+fork:only"));
            }

            [Fact]
            public void TestingTheLangaugeQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos who's language is Literate Haskell
                var request = new SearchRepositoriesRequest("github");
                request.Language = Language.LiterateHaskell;
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+language:\"Literate Haskell\""));
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
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+in:Description"));
            }

            [Fact]
            public void TestingTheInQualifier_Name()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchRepositoriesRequest("github");
                request.In = new[] { InQualifier.Name };
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+in:Name"));
            }

            [Fact]
            public void TestingTheInQualifier_Readme()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchRepositoriesRequest("github");
                request.In = new[] { InQualifier.Readme };
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+in:Readme"));
            }

            [Fact]
            public void TestingTheInQualifier_Multiple()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchRepositoriesRequest("github");
                request.In = new[] { InQualifier.Readme, InQualifier.Description, InQualifier.Name };
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+in:Readme,Description,Name"));
            }

            [Fact]
            public void TestingTheCreatedQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been created after year jan 1 2011
                var request = new SearchRepositoriesRequest("github");
                request.Created = DateRange.GreaterThan(new DateTimeOffset(new DateTime(2011, 1, 1), TimeSpan.Zero));
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:>2011-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been created after year jan 1 2011
                var request = new SearchRepositoriesRequest("github");
                request.Created = DateRange.GreaterThanOrEquals(new DateTimeOffset(new DateTime(2011, 1, 1), TimeSpan.Zero));
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:>=2011-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been created after year jan 1 2011
                var request = new SearchRepositoriesRequest("github");
                request.Created = DateRange.LessThan(new DateTimeOffset(new DateTime(2011, 1, 1), TimeSpan.Zero));
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:<2011-01-01T00:00:00+00:00"));
            }


            [Fact]
            public void TestingTheCreatedQualifier_LessThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been created after year jan 1 2011
                var request = new SearchRepositoriesRequest("github");
                request.Created = DateRange.LessThanOrEquals(new DateTimeOffset(new DateTime(2011, 1, 1), TimeSpan.Zero));
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:<=2011-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_Between()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchRepositoriesRequest("github");
                request.Created = DateRange.Between(
                    new DateTimeOffset(new DateTime(2011, 1, 1), TimeSpan.Zero),
                    new DateTimeOffset(new DateTime(2012, 11, 11), TimeSpan.Zero));

                client.SearchRepo(request);

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+created:2011-01-01T00:00:00+00:00..2012-11-11T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been pushed before year jan 1 2013
                var request = new SearchRepositoriesRequest("github");
                request.Updated = DateRange.GreaterThan(new DateTimeOffset(new DateTime(2013, 1, 1), TimeSpan.Zero));
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+pushed:>2013-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_GreaterThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been pushed before year jan 1 2013
                var request = new SearchRepositoriesRequest("github");
                request.Updated = DateRange.GreaterThanOrEquals(new DateTimeOffset(new DateTime(2013, 1, 1), TimeSpan.Zero));
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+pushed:>=2013-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been pushed before year jan 1 2013
                var request = new SearchRepositoriesRequest("github");
                request.Updated = DateRange.LessThan(new DateTimeOffset(new DateTime(2013, 1, 1), TimeSpan.Zero));
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+pushed:<2013-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_LessThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where the search contains 'github' and has been pushed before year jan 1 2013
                var request = new SearchRepositoriesRequest("github");
                request.Updated = DateRange.LessThanOrEquals(new DateTimeOffset(new DateTime(2013, 1, 1), TimeSpan.Zero));
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+pushed:<=2013-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_Between()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchRepositoriesRequest("github");
                request.Updated = DateRange.Between(
                    new DateTimeOffset(new DateTime(2012, 4, 30), TimeSpan.Zero),
                    new DateTimeOffset(new DateTime(2012, 7, 4), TimeSpan.Zero));

                client.SearchRepo(request);

                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+pushed:2012-04-30T00:00:00+00:00..2012-07-04T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUserQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                //get repos where search contains 'github' and user/org is 'github'
                var request = new SearchRepositoriesRequest("github");
                request.User = "rails";
                client.SearchRepo(request);
                connection.Received().Get<SearchRepositoryResult>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "github+user:rails"));
            }

            [Fact]
            public void TestingTheSortParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchRepositoriesRequest("github");
                request.SortField = RepoSearchSort.Stars;

                client.SearchRepo(request);

                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["q"] == "github" &&
                        d["sort"] == "stars"));
            }
            [Fact]
            public void TestingTheSearchParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchRepositoriesRequest();

                client.SearchRepo(request);

                connection.Received().Get<SearchRepositoryResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/repositories"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        string.IsNullOrEmpty(d["q"])));
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
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SearchIssues(null));
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
            public void TestingTheTypeQualifier_PullRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Type = IssueTypeQualifier.PullRequest;

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
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+label:\"bug\""));
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
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+label:\"bug\"+label:\"feature\""));
            }

            [Fact]
            public void TestingTheLanguageQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Language = Language.LiterateHaskell;

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+language:\"Literate Haskell\""));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.GreaterThan(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:>2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.GreaterThanOrEquals(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:>=2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.LessThan(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:<2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.LessThanOrEquals(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:<=2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_Between()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.Between(
                    new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero),
                    new DateTimeOffset(new DateTime(2014, 2, 2), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:2014-01-01T00:00:00+00:00..2014-02-02T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.GreaterThan(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:>2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_GreaterThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.GreaterThanOrEquals(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:>=2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.LessThan(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:<2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_LessThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.LessThanOrEquals(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:<=2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_Between()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.Between(
                    new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero),
                    new DateTimeOffset(new DateTime(2014, 2, 2), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:2014-01-01T00:00:00+00:00..2014-02-02T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_GreaterThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.GreaterThan(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:>2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_GreaterThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.GreaterThanOrEquals(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:>=2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_LessThan()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.LessThan(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:<2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_LessThanOrEquals()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.LessThanOrEquals(new DateTimeOffset(new DateTime(2014, 1, 1), TimeSpan.Zero));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:<=2014-01-01T00:00:00+00:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThanDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.GreaterThan(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:>2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_GreaterThanOrEqualsDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.GreaterThanOrEquals(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:>=2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThanDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.LessThan(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));
                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:<2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_LessThanOrEqualsDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.LessThanOrEquals(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:<=2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheCreatedQualifier_BetweenDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Created = DateRange.Between(
                    new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)),
                    new DateTimeOffset(2014, 2, 2, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+created:2014-01-01T02:04:06+10:00..2014-02-02T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_GreaterThanDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.GreaterThan(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:>2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_GreaterThanOrEqualsDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.GreaterThanOrEquals(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:>=2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_LessThanDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.LessThan(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:<2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_LessThanOrEqualsDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.LessThanOrEquals(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:<=2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheMergedQualifier_BetweenDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Merged = DateRange.Between(
                    new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)),
                    new DateTimeOffset(2014, 2, 2, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+merged:2014-01-01T02:04:06+10:00..2014-02-02T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_GreaterThanDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.GreaterThan(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:>2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_GreaterThanOrEqualsDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.GreaterThanOrEquals(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:>=2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_LessThanDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.LessThan(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:<2014-01-01T02:04:06+10:00"));
            }

            [Fact]
            public void TestingTheUpdatedQualifier_LessThanOrEqualsDateTime()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Updated = DateRange.LessThanOrEquals(new DateTimeOffset(2014, 1, 1, 2, 4, 6, new TimeSpan(10, 0, 0)));

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+updated:<=2014-01-01T02:04:06+10:00"));
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
                request.Repos.Add("octokit", "octokit.net");

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+repo:octokit/octokit.net"));
            }

            [Fact]
            public async Task ErrorOccursWhenSpecifyingInvalidFormatForRepos()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);

                var request = new SearchIssuesRequest("windows");
                request.Repos = new RepositoryCollection {
                    "haha-business"
                };

                request.SortField = IssueSearchSort.Created;
                request.Order = SortDirection.Descending;

                await Assert.ThrowsAsync<RepositoryFormatException>(
                    async () => await client.SearchIssues(request));
            }

            [Fact]
            public void TestingTheRepoAndUserAndLabelQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchIssuesRequest("something");
                request.Repos.Add("octokit/octokit.net");
                request.User = "alfhenrik";
                request.Labels = new[] { "bug" };

                client.SearchIssues(request);

                connection.Received().Get<SearchIssuesResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] ==
                        "something+label:\"bug\"+user:alfhenrik+repo:octokit/octokit.net"));
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
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SearchCode(null));
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
                request.Language = Language.LiterateHaskell;

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+language:\"Literate Haskell\""));
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
                var request = new SearchCodeRequest("something")
                {
                    Extensions = new[] { "txt" }
                };

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+extension:txt"));
            }

            [Fact]
            public void TestingTheExtensionQualifier_Multiple()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something")
                {
                    Extensions = new[] { "cs", "lol" }
                };

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+extension:cs+extension:lol"));
            }

            [Fact]
            public void TestingTheFileNameQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.FileName = "packages.config";

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+filename:packages.config"));
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
                var request = new SearchCodeRequest("something", "octokit", "octokit.net");

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+repo:octokit/octokit.net"));
            }

            [Fact]
            public void TestingTheOrgQualifier()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something");
                request.Organization = "octokit";

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d => d["q"] == "something+org:octokit"));
            }

            [Fact]
            public void TestingTheRepoAndPathAndExtensionQualifiers()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchCodeRequest("something", "octokit", "octokit.net")
                {
                    Extensions = new[] { "fs", "cs" }
                };
                request.Path = "tools/FAKE.core";

                client.SearchCode(request);

                connection.Received().Get<SearchCodeResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/code"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["q"] == "something+path:tools/FAKE.core+extension:fs+extension:cs+repo:octokit/octokit.net"));
            }

            [Fact]
            public async Task ErrorOccursWhenSpecifyingInvalidFormatForRepos()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);

                var request = new SearchCodeRequest("windows");
                request.Repos = new RepositoryCollection {
                    "haha-business"
                };

                request.Order = SortDirection.Descending;

                await Assert.ThrowsAsync<RepositoryFormatException>(
                    async () => await client.SearchCode(request));
            }
        }

        public class TheSearchLabelsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                client.SearchLabels(new SearchLabelsRequest("something", 1));
                connection.Received().Get<SearchLabelsResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/labels"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.symmetra-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SearchLabels(null));
            }

            [Fact]
            public void TestingTheTermParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchLabelsRequest("something", 1);

                client.SearchLabels(request);

                connection.Received().Get<SearchLabelsResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/labels"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["q"] == "something" &&
                        d["repository_id"] == "1"),
                    "application/vnd.github.symmetra-preview+json");
            }

            [Fact]
            public void TestingTheSortParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchLabelsRequest("something", 1);
                request.SortField = LabelSearchSort.Created;

                client.SearchLabels(request);

                connection.Received().Get<SearchLabelsResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/labels"),
                    Arg.Is<Dictionary<string, string>>(d => d["sort"] == "created"),
                    "application/vnd.github.symmetra-preview+json");
            }

            [Fact]
            public void TestingTheOrderParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchLabelsRequest("something", 1);
                request.SortField = LabelSearchSort.Created;
                request.Order = SortDirection.Ascending;

                client.SearchLabels(request);

                connection.Received().Get<SearchLabelsResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/labels"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["sort"] == "created" &&
                        d["order"] == "asc"),
                    "application/vnd.github.symmetra-preview+json");
            }

            [Fact]
            public void TestingTheDefaultOrderParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchLabelsRequest("something", 1);

                client.SearchLabels(request);

                connection.Received().Get<SearchLabelsResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/labels"),
                    Arg.Is<Dictionary<string, string>>(d => d["order"] == "desc"),
                    "application/vnd.github.symmetra-preview+json");
            }

            [Fact]
            public void TestingTheRepositoryIdParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                var request = new SearchLabelsRequest("something", 1);

                client.SearchLabels(request);

                connection.Received().Get<SearchLabelsResult>(
                    Arg.Is<Uri>(u => u.ToString() == "search/labels"),
                    Arg.Is<Dictionary<string, string>>(d =>
                        d["q"] == "something" &&
                        d["repository_id"] == "1"),
                    "application/vnd.github.symmetra-preview+json");
            }
        }
    }
}