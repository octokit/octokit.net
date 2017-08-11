﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
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

        public class TheGetAllPendingInvitationsMethod
        {
            readonly IGitHubClient _gitHub;

            public TheGetAllPendingInvitationsMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [IntegrationTest]
            public async Task ReturnsNoPendingInvitations()
            {
                var pendingInvitations = await _gitHub.Organization.Member.GetAllPendingInvitations(Helper.Organization);
                Assert.NotNull(pendingInvitations);
                Assert.Empty(pendingInvitations);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfPendingInvitationsWithoutStart()
            {
                using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
                {
                    teamContext.InviteMember("octokitnet-test1");
                    teamContext.InviteMember("octokitnet-test2");

                    var options = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 2
                    };

                    var pendingInvitations = await _gitHub.Organization.Member.GetAllPendingInvitations(Helper.Organization, options);
                    Assert.NotEmpty(pendingInvitations);
                    Assert.Equal(2, pendingInvitations.Count);
                }
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfPendingInvitationsWithStart()
            {
                using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
                {
                    teamContext.InviteMember("octokitnet-test1");
                    teamContext.InviteMember("octokitnet-test2");

                    var firstPageOptions = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 1,
                        StartPage = 1
                    };

                    var firstPagePendingInvitations = await _gitHub.Organization.Member.GetAllPendingInvitations(Helper.Organization, firstPageOptions);
                    Assert.NotEmpty(firstPagePendingInvitations);
                    Assert.Equal(1, firstPagePendingInvitations.Count);

                    var secondPageOptions = new ApiOptions
                    {
                        PageCount = 1,
                        PageSize = 1,
                        StartPage = 2
                    };

                    var secondPagePendingInvitations = await _gitHub.Organization.Member.GetAllPendingInvitations(Helper.Organization, secondPageOptions);
                    Assert.NotEmpty(secondPagePendingInvitations);
                    Assert.Equal(1, secondPagePendingInvitations.Count);
                }
            }
        }
    }
}
