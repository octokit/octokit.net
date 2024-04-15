using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableOrganizationOutsideCollaboratorsClientTests
    {
        public class TheGetAllMethod
        {
            readonly IGitHubClient _gitHub;
            readonly ObservableOrganizationOutsideCollaboratorsClient _client;
            readonly string _fixtureCollaborator = "octokitnet-test1";

            public TheGetAllMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _client = new ObservableOrganizationOutsideCollaboratorsClient(_gitHub);
            }

            [OrganizationTest]
            public async Task ReturnsNoOutsideCollaborators()
            {
                var outsideCollaborators = await _client
                    .GetAll(Helper.Organization).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Empty(outsideCollaborators);
            }

            [OrganizationTest]
            public async Task ReturnsOutsideCollaborators()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Single(outsideCollaborators);
                }
            }

            [OrganizationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithoutStart()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                    var options = new ApiOptions
                    {
                        PageSize = 1,
                        PageCount = 1
                    };

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization, options).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Single(outsideCollaborators);
                }
            }

            [OrganizationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithStart()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "octokitnet-test2");

                    var options = new ApiOptions
                    {
                        PageSize = 1,
                        PageCount = 1,
                        StartPage = 1
                    };

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization, options).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Single(outsideCollaborators);
                }
            }

            [OrganizationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilter()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "octokitnet-test2");

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(2, outsideCollaborators.Count);
                }
            }

            [OrganizationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilterAndApiOptions()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "octokitnet-test2");

                    var options = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 1
                    };

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All, options).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Single(outsideCollaborators);
                }
            }

            [OrganizationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilterWithStart()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "octokitnet-test2");

                    var firstPageOptions = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 1,
                        StartPage = 1
                    };

                    var firstPageOfOutsideCollaborators = await _client
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All, firstPageOptions).ToList();

                    var secondPageOptions = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 1,
                        StartPage = 2
                    };

                    var secondPageOfOutsideCollaborators = await _client
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All, secondPageOptions).ToList();

                    Assert.Single(firstPageOfOutsideCollaborators);
                    Assert.Single(secondPageOfOutsideCollaborators);
                    Assert.NotEqual(firstPageOfOutsideCollaborators[0].Login, secondPageOfOutsideCollaborators[0].Login);
                }
            }

            [OrganizationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithTwoFactorFilter()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "octokitnet-test2");

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Single(outsideCollaborators);
                    Assert.Equal("octokitnet-test1", outsideCollaborators[0].Login);
                }
            }

            [OrganizationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithTwoFactorFilterAndApiOptions()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "octokitnet-test2");

                    var options = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 1
                    };

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled, options).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Single(outsideCollaborators);
                    Assert.Equal("octokitnet-test1", outsideCollaborators[0].Login);
                }
            }
        }

        public class TheDeleteMethod
        {
            readonly IGitHubClient _gitHub;
            readonly ObservableOrganizationOutsideCollaboratorsClient _client;
            readonly string _fixtureCollaborator = "octokitnet-test1";

            public TheDeleteMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _client = new ObservableOrganizationOutsideCollaboratorsClient(_gitHub);
            }

            [OrganizationTest]
            public async Task CanRemoveOutsideCollaborator()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                    var result = await _client.Delete(Helper.Organization, _fixtureCollaborator);
                    Assert.True(result);

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization).ToList();

                    Assert.Empty(outsideCollaborators);
                }
            }
        }

        public class TheConvertFromMemberMethod
        {
            readonly IGitHubClient _gitHub;
            readonly ObservableOrganizationOutsideCollaboratorsClient _client;
            readonly string _fixtureCollaborator = "octokitnet-test1";

            public TheConvertFromMemberMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _client = new ObservableOrganizationOutsideCollaboratorsClient(_gitHub);
            }

            [OrganizationTest(Skip = "This test relies on https://github.com/octokit/octokit.net/issues/1533 being implemented before being re-enabled as there's currently no way to invite a member to an org")]
            public async Task CanConvertOrgMemberToOutsideCollaborator()
            {
                var result = await _client.ConvertFromMember(Helper.Organization, _fixtureCollaborator);
                Assert.True(result);

                var outsideCollaborators = await _client
                    .GetAll(Helper.Organization).ToList();

                Assert.Single(outsideCollaborators);
                Assert.Equal(_fixtureCollaborator, outsideCollaborators[0].Login);
            }
        }
    }
}
