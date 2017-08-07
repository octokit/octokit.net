using System;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class OrganizationOutsideCollaboratorsClientTests
    {
        public class TheGetAllMethod
        {
            readonly IGitHubClient _gitHub;
            readonly string _fixtureCollaborator = "alfhenrik-test-2";
            public TheGetAllMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [IntegrationTest]
            public async Task ReturnsNoOutsideCollaborators()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Empty(outsideCollaborators);
                }
            }

            [IntegrationTest]
            public async Task ReturnsOutsideCollaborators()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(1, outsideCollaborators.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithoutStart()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                    var options = new ApiOptions
                    {
                        PageSize = 1,
                        PageCount = 1
                    };

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, options);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(1, outsideCollaborators.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithStart()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                    var options = new ApiOptions
                    {
                        PageSize = 1,
                        PageCount = 1,
                        StartPage = 1
                    };

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, options);

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

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(2, outsideCollaborators.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilterAndApiOptions()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                    var options = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 2
                    };

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All, options);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(2, outsideCollaborators.Count);
                }
            }

            [IntegrationTest(Skip = "It seems this API endpoint does not support pagination as page size/count are not adhered to")]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilterWithStart()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                    var firstPageOptions = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 1,
                        StartPage = 1
                    };

                    var firstPageOfOutsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All, firstPageOptions);

                    var secondPageOptions = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 1,
                        StartPage = 2
                    };

                    var secondPageOfOutsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All, secondPageOptions);

                    Assert.Equal(1, firstPageOfOutsideCollaborators.Count);
                    Assert.Equal(1, secondPageOfOutsideCollaborators.Count);
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

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(1, outsideCollaborators.Count);
                    Assert.Equal(_fixtureCollaborator, outsideCollaborators[0].Login);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithTwoFactorFilterAndApiOptions()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                    var options = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 2
                    };

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled, options);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Equal(1, outsideCollaborators.Count);
                    Assert.Equal(_fixtureCollaborator, outsideCollaborators[0].Login);
                }
            }
        }

        public class TheDeleteMethod
        {
            readonly IGitHubClient _gitHub;
            readonly string _fixtureCollaborator = "alfhenrik-test-2";

            public TheDeleteMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [IntegrationTest]
            public async Task CanRemoveOutsideCollaborator()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                    var result = await _gitHub
                        .Organization
                        .OutsideCollaborator
                        .Delete(Helper.Organization, _fixtureCollaborator);

                    Assert.True(result);

                    var outsideCollaborators = await _gitHub
                        .Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Empty(outsideCollaborators);
                }
            }

            [IntegrationTest]
            public async Task CannotRemoveMemberOfOrganizationAsOutsideCollaborator()
            {
                var ex = await Assert.ThrowsAsync<UserIsOrganizationMemberException>(() 
                    => _gitHub.Organization.OutsideCollaborator.Delete(Helper.Organization, Helper.UserName));

                Assert.True(string.Equals(
                    "You cannot specify an organization member to remove as an outside collaborator.",
                    ex.Message,
                    StringComparison.OrdinalIgnoreCase));
            }
        }

        public class TheConvertFromMemberMethod
        {
            readonly IGitHubClient _gitHub;
            readonly string _fixtureCollaborator = "alfhenrik-test-2";

            public TheConvertFromMemberMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [IntegrationTest(Skip = "This test relies on https://github.com/octokit/octokit.net/issues/1533 being implemented before being re-enabled as there's currently no way to invite a member to an org")]
            public async Task CanConvertOrgMemberToOutsideCollaborator()
            {
                var result = await _gitHub.Organization.OutsideCollaborator.ConvertFromMember(Helper.Organization, _fixtureCollaborator);
                Assert.True(result);

                var outsideCollaborators = await _gitHub.Organization.OutsideCollaborator.GetAll(Helper.Organization);

                Assert.Equal(1, outsideCollaborators.Count);
                Assert.Equal(_fixtureCollaborator, outsideCollaborators[0].Login);
            }

            [IntegrationTest]
            public async Task CannotConvertNonOrgMemberToOutsideCollaborator()
            {
                var ex = await Assert.ThrowsAsync<UserIsNotMemberOfOrganizationException>(()
                    => _gitHub.Organization.OutsideCollaborator.ConvertFromMember(Helper.Organization, _fixtureCollaborator));

                Assert.True(string.Equals(
                    $"{_fixtureCollaborator} is not a member of the {Helper.Organization} organization.",
                    ex.Message,
                    StringComparison.OrdinalIgnoreCase));
            }

            [IntegrationTest]
            public async Task CannotConvertLastOrgOwnerToOutsideCollaborator()
            {
                var ex = await Assert.ThrowsAsync<UserIsLastOwnerOfOrganizationException>(()
                    => _gitHub.Organization.OutsideCollaborator.ConvertFromMember(Helper.Organization, Helper.UserName));

                Assert.True(string.Equals(
                    "Cannot convert the last owner to an outside collaborator",
                    ex.Message,
                    StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
