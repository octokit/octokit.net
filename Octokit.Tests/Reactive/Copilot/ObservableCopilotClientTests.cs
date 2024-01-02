using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableCopilotClientTests
    {
        private const string orgName = "test";
        
        public class TheGetCopilotBillingSettingsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCopilotClient(githubClient);
                
                client.GetSummaryForOrganization("test");
                
                githubClient.Copilot.Received(1).GetSummaryForOrganization(orgName);
            }
        }
        
        public class TheGetAllCopilotLicensesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri($"orgs/test/copilot/billing/seats", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableCopilotClient(gitHubClient);

                var apiOptions = new ApiOptions() { PageSize = 50, PageCount = 10 };
                client.Licensing.GetAll("test", apiOptions);
                
                connection.Received().Get<List<CopilotSeats>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => d.Count > 0));
            }
        }
        
        public class TheAssignCopilotLicenseMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCopilotClient(githubClient);
                const string expectedUser = "copilot-user";
                
                client.Licensing.Assign(orgName, expectedUser);
                
                githubClient.Copilot.Licensing.Received().Assign(orgName, expectedUser);
            }
        }
        
        public class TheAssignCopilotLicensesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCopilotClient(githubClient);

                var payloadData = new UserSeatAllocation() { SelectedUsernames = new[] { "copilot-user" } };
                client.Licensing.Assign(orgName, payloadData);
                
                githubClient.Copilot.Licensing.Received().Assign(orgName, payloadData);
            }
        }
        
        public class TheRemoveCopilotLicenseMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCopilotClient(githubClient);
                const string expectedUser = "copilot-user";
                
                client.Licensing.Remove(orgName, expectedUser);
                
                githubClient.Copilot.Licensing.Received().Remove(orgName, expectedUser);
            }
        }
        
        public class TheRemoveCopilotLicensesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCopilotClient(githubClient);

                var payloadData = new UserSeatAllocation() { SelectedUsernames = new[] { "copilot-user" } };
                client.Licensing.Remove(orgName, payloadData);
                
                githubClient.Copilot.Licensing.Received().Remove(orgName, payloadData);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableCopilotClient(null));
            }
        }
    }
}