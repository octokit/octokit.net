using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableOrganizationMembersClientTests
    {
        public class TheGetAllPendingInvitationsMethod
        {
            readonly ObservableOrganizationMembersClient _client;

            public TheGetAllPendingInvitationsMethod()
            {
                _client = new ObservableOrganizationMembersClient(Helper.GetAuthenticatedClient());
            }

            [IntegrationTest]
            public async Task ReturnsNoPendingInvitations()
            {
                var pendingInvitations = await _client.GetAllPendingInvitations(Helper.Organization).ToList();

                Assert.Empty(pendingInvitations);
            }
        }
    }
}
