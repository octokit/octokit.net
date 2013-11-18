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
                client.SearchUsers(new UsersRequest("something"));
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());
                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchUsers(null));
            }
        }

        public class TheSearchRepoMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);
                client.SearchRepo(new RepositoriesRequest("something"));
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
                //lets see how this API fairs out with comments from @shiftkey and @haacked.

                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);

                var request = new RepositoriesRequest("something");
                
                request.Size = new SizeQualifier(55); //match 55Mb Exactly
                request.Size = new SizeQualifier(100, 5000); //match repo's between 100 and 5000 MB's
                request.Size = new SizeQualifier(1000,  QualifierOperator.GreaterOrEqualTo); //match repo's that are greater than or equal to 1000
                request.Size = new SizeQualifier(1000, QualifierOperator.LessOrEqualTo); //match repo's that are less than or equal to 1000
                request.Size = new SizeQualifier(1000, QualifierOperator.LessThan); //match repo's that are less than 1000
                request.Size = new SizeQualifier(1000, QualifierOperator.GreaterThan); //match repo's that are greater than 1000
                request.Size = SizeQualifier.GreaterThan(5000);
                request.Size = SizeQualifier.GreaterThanOrEquals(5000);
                request.Size = SizeQualifier.LessThan(5000);
                request.Size = SizeQualifier.LessThanOrEquals(5000);
                
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
                client.SearchIssues(new IssuesRequest("something"));
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
                client.SearchCode(new CodeRequest("something"));
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
