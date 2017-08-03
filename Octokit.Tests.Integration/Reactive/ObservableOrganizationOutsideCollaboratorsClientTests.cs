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
            readonly string _fixtureCollaborator = "alfhenrik-test-2";

            public TheGetAllMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _client = new ObservableOrganizationOutsideCollaboratorsClient(_gitHub);
            }

            [IntegrationTest]
            public async Task ReturnsNoOutsideCollaborators()
            {
                var outsideCollaborators = await _client
                    .GetAll(Helper.Organization).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Empty(outsideCollaborators);
            }

            [IntegrationTest]
            public async Task ReturnsOutsideCollaborators()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(1, outsideCollaborators.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilter()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(2, outsideCollaborators.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithTwoFactorFilter()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                    var outsideCollaborators = await _client
                        .GetAll(Helper.Organization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled).ToList();

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(1, outsideCollaborators.Count);
                    Assert.Equal("alfhenrik-test-2", outsideCollaborators[0].Login);
                }
            }
        }

        public class TheDeleteMethod
        {
            readonly IGitHubClient _gitHub;
            readonly ObservableOrganizationOutsideCollaboratorsClient _client;
            readonly string _fixtureCollaborator = "alfhenrik-test-2";

            public TheDeleteMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _client = new ObservableOrganizationOutsideCollaboratorsClient(_gitHub);
            }

            [IntegrationTest]
            public async Task CanRemoveOutsideCollaborator()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
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
            readonly string _fixtureCollaborator = "alfhenrik-test-2";

            public TheConvertFromMemberMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _client = new ObservableOrganizationOutsideCollaboratorsClient(_gitHub);
            }

            [IntegrationTest(Skip = "This test relies on https://github.com/octokit/octokit.net/issues/1533 being implemented before being re-enabled as there's currently no way to invite a member to an org")]
            public async Task CanConvertOrgMemberToOutsideCollaborator()
            {
                var result = await _client.ConvertFromMember(Helper.Organization, _fixtureCollaborator);
                Assert.True(result);

                var outsideCollaborators = await _client
                    .GetAll(Helper.Organization).ToList();

                Assert.Equal(1, outsideCollaborators.Count);
                Assert.Equal(_fixtureCollaborator, outsideCollaborators[0].Login);
            }
        }
    }
}
