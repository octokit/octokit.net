﻿using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class OrganizationMembersClientTests
    {
        public class TheGetAllMethod
        {
            private IGitHubClient _gitHub;
            private string _organizationFixture;

            public TheGetAllMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _organizationFixture = "octokit";
            }

            [IntegrationTest]
            public async Task ReturnsMembers()
            {
                var members = await
                    _gitHub.Organization.Member.GetAll(_organizationFixture);
                Assert.NotEmpty(members);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfMembersWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1
                };

                var members = await _gitHub.Organization.Member.GetAll(_organizationFixture, options);

                Assert.Equal(1, members.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfMembersWithStart()
            {
                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var members = await _gitHub.Organization.Member.GetAll(_organizationFixture, options);

                Assert.Equal(1, members.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctMembersBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var firstPage = await _gitHub.Organization.Member.GetAll(_organizationFixture, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await _gitHub.Organization.Member.GetAll(_organizationFixture, skipStartOptions);

                Assert.Equal(1, firstPage.Count);
                Assert.Equal(1, secondPage.Count);
                Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
            }

            [IntegrationTest(Skip = "TwoFactor filter can't be used unless the requester is an organization owner")]
            public async Task ReturnsMembersWithFilter()
            {
                var no2FAMembers = await _gitHub.Organization.Member.GetAll(_organizationFixture, OrganizationMembersFilter.TwoFactorAuthenticationDisabled);
                Assert.NotNull(no2FAMembers);
            }

            [IntegrationTest(Skip = "Admin/Member filter doesn't work unless the requester is an organization member")]
            public async Task ReturnsMembersWithRole()
            {
                var adminMembers = await _gitHub.Organization.Member.GetAll(_organizationFixture, OrganizationMembersRole.Admin);
                Assert.NotNull(adminMembers);

                var normalMembers = await _gitHub.Organization.Member.GetAll(_organizationFixture, OrganizationMembersRole.Member);
                Assert.NotNull(normalMembers);

                // There shouldnt be any members that are in both groups if the role filter works correctly
                var membersInBoth = adminMembers.Select(a => a.Id).Intersect(normalMembers.Select(n => n.Id));
                Assert.Empty(membersInBoth);
            }

            [IntegrationTest(Skip = "TwoFactor filter can't be used unless the requester is an organization owner")]
            public async Task ReturnsMembersWthFilterAndRole()
            {
                // Get count of admin/normal members
                var adminCount = (await _gitHub.Organization.Member.GetAll(_organizationFixture, OrganizationMembersRole.Admin)).Count;
                var memberCount = (await _gitHub.Organization.Member.GetAll(_organizationFixture, OrganizationMembersRole.Member)).Count;

                // Ensure that there are less admins with no 2 factor, than there are total admins
                var adminsWithNo2FA = await _gitHub.Organization.Member.GetAll(_organizationFixture, OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.Admin);
                Assert.NotNull(adminsWithNo2FA);
                Assert.True(adminsWithNo2FA.Count <= adminCount);

                // Ensure that there are less members with no 2 factor, than there are total admins
                var membersWithNo2FA = await _gitHub.Organization.Member.GetAll(_organizationFixture, OrganizationMembersFilter.TwoFactorAuthenticationDisabled, OrganizationMembersRole.Member);
                Assert.NotNull(membersWithNo2FA);
                Assert.True(membersWithNo2FA.Count <= memberCount);
            }
        }
    }
}
