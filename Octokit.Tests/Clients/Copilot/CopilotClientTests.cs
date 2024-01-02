using System;
using System.Collections.Generic;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class CopilotClientTests
    {
        private const string orgName = "test";
        
        public class TheGetCopilotBillingSettingsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CopilotClient(connection);

                var expectedUri = $"orgs/{orgName}/copilot/billing";
                client.GetSummaryForOrganization("test");
                
                connection.Received().Get<BillingSettings>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }
        
        public class TheGetAllCopilotLicensesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CopilotClient(connection);

                var expectedUri = $"orgs/{orgName}/copilot/billing/seats";
                client.Licensing.GetAll("test", new ApiOptions());
                
                connection.Received().GetAll<CopilotSeats>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<ApiOptions>());
            }
        }
        
        public class TheAssignCopilotLicenseMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CopilotClient(connection);
                var expectedUri = $"orgs/{orgName}/copilot/billing/selected_users";

                client.Licensing.Assign(orgName, "copilot-user");
                
                connection.Received().Post<CopilotSeatAllocation>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<UserSeatAllocation>());
            }
        }
        
        public class TheAssignCopilotLicensesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CopilotClient(connection);
                var expectedUri = $"orgs/{orgName}/copilot/billing/selected_users";

                var payloadData = new UserSeatAllocation() { SelectedUsernames = new[] { "copilot-user" } };
                client.Licensing.Assign(orgName, payloadData);
                
                connection.Received().Post<CopilotSeatAllocation>(Arg.Is<Uri>(u => u.ToString() == expectedUri), payloadData);
            }
        }
        
        public class TheRemoveCopilotLicenseMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CopilotClient(connection);
                var expectedUri = $"orgs/{orgName}/copilot/billing/selected_users";
                
                client.Licensing.Remove(orgName, "copilot-user" );
                
                connection.Received().Delete<CopilotSeatAllocation>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<UserSeatAllocation>());
            }
        }
        
        public class TheRemoveCopilotLicensesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CopilotClient(connection);
                var expectedUri = $"orgs/{orgName}/copilot/billing/selected_users";

                var payloadData = new UserSeatAllocation() { SelectedUsernames = new[] { "copilot-user" } };
                client.Licensing.Remove(orgName, payloadData);
                
                connection.Received().Delete<CopilotSeatAllocation>(Arg.Is<Uri>(u => u.ToString() == expectedUri), payloadData);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new CopilotClient(null));
            }
        }
    }
}