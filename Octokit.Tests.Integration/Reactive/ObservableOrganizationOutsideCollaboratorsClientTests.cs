using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableOrganizationOutsideCollaboratorsClientTests
    {
        readonly IGitHubClient _gitHub;
        readonly ObservableOrganizationOutsideCollaboratorsClient _client;
        readonly string _fixtureOrganization = "alfhenrik-test-org";
        readonly string _fixtureCollaborator = "alfhenrik-test-2";

        public ObservableOrganizationOutsideCollaboratorsClientTests()
        {
            _gitHub = Helper.GetAuthenticatedClient();
            _client = new ObservableOrganizationOutsideCollaboratorsClient(_gitHub);
        }

        [IntegrationTest]
        public async Task ReturnsNoOutsideCollaborators()
        {
            var outsideCollaborators = await _client
                .GetAll(_fixtureOrganization).ToList();

            Assert.NotNull(outsideCollaborators);
            Assert.Empty(outsideCollaborators);
        }

        [IntegrationTest]
        public async Task ReturnsOutsideCollaborators()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            using (var context = await _gitHub.CreateRepositoryContext(_fixtureOrganization, new NewRepository(repoName)))
            {
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                var outsideCollaborators = await _client
                    .GetAll(_fixtureOrganization).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Equal(1, outsideCollaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithoutStart()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            using (var context = await _gitHub.CreateRepositoryContext(_fixtureOrganization, new NewRepository(repoName)))
            {
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var outsideCollaborators = await _client
                    .GetAll(_fixtureOrganization, options).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Equal(1, outsideCollaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithStart()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            using (var context = await _gitHub.CreateRepositoryContext(_fixtureOrganization, new NewRepository(repoName)))
            {
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                var outsideCollaborators = await _client
                    .GetAll(_fixtureOrganization, options).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Equal(1, outsideCollaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilter()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            using (var context = await _gitHub.CreateRepositoryContext(_fixtureOrganization, new NewRepository(repoName)))
            {
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                var outsideCollaborators = await _client
                    .GetAll(_fixtureOrganization, OrganizationMembersFilter.All).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Equal(2, outsideCollaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilterAndApiOptions()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            using (var context = await _gitHub.CreateRepositoryContext(_fixtureOrganization, new NewRepository(repoName)))
            {
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 2
                };

                var outsideCollaborators = await _client
                    .GetAll(_fixtureOrganization, OrganizationMembersFilter.All, options).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Equal(2, outsideCollaborators.Count);
            }
        }

        [IntegrationTest(Skip = "It seems this API endpoint does not support pagination as page size/count are not adhered to")]
        public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithAllFilterWithStart()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            using (var context = await _gitHub.CreateRepositoryContext(_fixtureOrganization, new NewRepository(repoName)))
            {
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                var firstPageOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var firstPageOfOutsideCollaborators = await _client
                    .GetAll(_fixtureOrganization, OrganizationMembersFilter.All, firstPageOptions).ToList();

                var secondPageOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 2
                };

                var secondPageOfOutsideCollaborators = await _client
                    .GetAll(_fixtureOrganization, OrganizationMembersFilter.All, secondPageOptions).ToList();

                Assert.Equal(1, firstPageOfOutsideCollaborators.Count);
                Assert.Equal(1, secondPageOfOutsideCollaborators.Count);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithTwoFactorFilter()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            using (var context = await _gitHub.CreateRepositoryContext(_fixtureOrganization, new NewRepository(repoName)))
            {
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                var outsideCollaborators = await _client
                    .GetAll(_fixtureOrganization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Equal(1, outsideCollaborators.Count);
                Assert.Equal("alfhenrik-test-2", outsideCollaborators[0].Login);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfOutsideCollaboratorsWithTwoFactorFilterAndApiOptions()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            using (var context = await _gitHub.CreateRepositoryContext(_fixtureOrganization, new NewRepository(repoName)))
            {
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, _fixtureCollaborator);
                await _gitHub.Repository.Collaborator.Add(context.RepositoryOwner, context.RepositoryName, "alfhenrik");

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 2
                };

                var outsideCollaborators = await _client
                    .GetAll(_fixtureOrganization, OrganizationMembersFilter.TwoFactorAuthenticationDisabled, options).ToList();

                Assert.NotNull(outsideCollaborators);
                Assert.Equal(1, outsideCollaborators.Count);
                Assert.Equal("alfhenrik-test-2", outsideCollaborators[0].Login);
            }
        }
    }
}
