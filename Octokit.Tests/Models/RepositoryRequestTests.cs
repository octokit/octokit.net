using Xunit;

namespace Octokit.Tests.Models
{
    public class RepositoryRequestTests
    {
        public class TheToParametersDictionaryMethod
        {
            [Fact]
            public void ContainsSetValues()
            {
                var request = new RepositoryRequest
                {
                    Type = RepositoryType.All,
                    Sort = RepositorySort.FullName,
                    Direction = SortDirection.Ascending
                };

                var parameters = request.ToParametersDictionary();

                Assert.Equal(3, parameters.Count);
                Assert.Equal("all", parameters["type"]);
                Assert.Equal("full_name", parameters["sort"]);
                Assert.Equal("asc", parameters["direction"]);

                request = new RepositoryRequest
                {
                    Affiliation = RepositoryAffiliation.All,
                    Visibility = RepositoryRequestVisibility.Public
                };

                parameters = request.ToParametersDictionary();

                Assert.Equal(2, parameters.Count);
                Assert.Equal("owner, collaborator, organization_member", parameters["affiliation"]);
                Assert.Equal("public", parameters["visibility"]);
            }

            [Fact]
            public void DoesNotReturnValuesForDefaultRequest()
            {
                var request = new RepositoryRequest();

                var parameters = request.ToParametersDictionary();

                Assert.Empty(parameters);
            }
        }
    }
}
