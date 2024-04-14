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
            readonly string _fixtureCollaborator = "octokitnet-test1";
            public TheGetAllMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [OrganizationTest]
            public async Task ReturnsNoOutsideCollaborators()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Empty(outsideCollaborators);
                }
            }

            [OrganizationTest]
            public async Task ReturnsOutsideCollaborators()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
                {
                    await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization);

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

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, options);

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

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, options);

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

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All);

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

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.All, options);

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

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Single(outsideCollaborators);
                    Assert.Equal(_fixtureCollaborator, outsideCollaborators[0].Login);
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

                    var outsideCollaborators = await _gitHub.Organization
                        .OutsideCollaborator
                        .GetAll(Helper.Organization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled, options);

                    Assert.NotNull(outsideCollaborators);
                    Assert.Single(outsideCollaborators);
                    Assert.Equal(_fixtureCollaborator, outsideCollaborators[0].Login);
                }
            }
        }

        public class TheDeleteMethod
        {
            readonly IGitHubClient _gitHub;
            readonly string _fixtureCollaborator = "octokitnet-test1";

            public TheDeleteMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [OrganizationTest]
            public async Task CanRemoveOutsideCollaborator()
            {
                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                using (var context = await _gitHub.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(repoName)))
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

            [OrganizationTest]
            public async Task CannotRemoveMemberOfOrganizationAsOutsideCollaborator()
            {
                var ex = await Assert.ThrowsAsync<UserIsOrganizationMemberException>(()
                    => _gitHub.Organization.OutsideCollaborator.Delete(Helper.Organization, Helper.UserName));

                Assert.Equal("You cannot specify an organization member to remove as an outside collaborator.", ex.Message, ignoreCase: true);
            }
        }

        public class TheConvertFromMemberMethod
        {
            readonly IGitHubClient _gitHub;
            readonly string _fixtureCollaborator = "octokitnet-test1";

            public TheConvertFromMemberMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [OrganizationTest(Skip = "This test relies on https://github.com/octokit/octokit.net/issues/1533 being implemented before being re-enabled as there's currently no way to invite a member to an org")]
            public async Task CanConvertOrgMemberToOutsideCollaborator()
            {
                var result = await _gitHub.Organization.OutsideCollaborator.ConvertFromMember(Helper.Organization, _fixtureCollaborator);
                Assert.True(result);

                var outsideCollaborators = await _gitHub.Organization.OutsideCollaborator.GetAll(Helper.Organization);

                Assert.Single(outsideCollaborators);
                Assert.Equal(_fixtureCollaborator, outsideCollaborators[0].Login);
            }

            [OrganizationTest]
            public async Task CannotConvertNonOrgMemberToOutsideCollaborator()
            {
                var ex = await Assert.ThrowsAsync<UserIsNotMemberOfOrganizationException>(()
                    => _gitHub.Organization.OutsideCollaborator.ConvertFromMember(Helper.Organization, _fixtureCollaborator));

                Assert.Equal($"{_fixtureCollaborator} is not a member of the {Helper.Organization} organization.", ex.Message, ignoreCase: true);
            }

            [OrganizationTest]
            public async Task CannotConvertLastOrgOwnerToOutsideCollaborator()
            {
                var ex = await Assert.ThrowsAsync<UserIsLastOwnerOfOrganizationException>(()
                    => _gitHub.Organization.OutsideCollaborator.ConvertFromMember(Helper.Organization, Helper.UserName));

                Assert.Equal("Cannot convert the last owner to an outside collaborator", ex.Message, ignoreCase: true);
            }
        }
    }
}
