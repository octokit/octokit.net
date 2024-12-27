using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ActionsSelfHostedRunnerGroupsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ActionsSelfHostedRunnerGroupsClient(null));
            }
        }

        public class TheGetRunnerGroupForEnterpriseMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await client.GetRunnerGroupForEnterprise("fake", 1);

                connection.Received().Get<RunnerGroup>(
                  Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runner-groups/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRunnerGroupForEnterprise(null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRunnerGroupForEnterprise("", 1));
            }
        }

        public class TheGetRunnerGroupForOrganizationMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await client.GetRunnerGroupForOrganization("fake", 1);

                connection.Received().Get<RunnerGroup>(
                  Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runner-groups/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRunnerGroupForOrganization(null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRunnerGroupForOrganization("", 1));
            }
        }

        public class TheListAllRunnerGroupsForEnterpriseMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await client.ListAllRunnerGroupsForEnterprise("fake");

                connection.Received().GetAll<RunnerGroupResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runner-groups"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerGroupsForEnterprise(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnerGroupsForEnterprise(""));
            }
        }

        public class TheListAllRunnerGroupsForOrganizationMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await client.ListAllRunnerGroupsForOrganization("fake");

                connection.Received().GetAll<RunnerGroupResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runner-groups"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerGroupsForOrganization(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnerGroupsForOrganization(""));
            }
        }

        public class TheListAllRunnersForEnterpriseRunnerGroupMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await client.ListAllRunnersForEnterpriseRunnerGroup("fake", 1);

                connection.Received().GetAll<RunnerResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runner-groups/1/runners"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnersForEnterpriseRunnerGroup(null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnersForEnterpriseRunnerGroup("", 1));
            }
        }

        public class TheListAllRunnersForOrganizationRunnerGroupMethod
        {
            [Fact]
            public async Task RequstsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await client.ListAllRunnersForOrganizationRunnerGroup("fake", 1);

                connection.Received().GetAll<RunnerResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runner-groups/1/runners"), Args.ApiOptions);

            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnersForOrganizationRunnerGroup(null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnersForOrganizationRunnerGroup("", 1));
            }
        }

        public class TheListAllRunnerGroupOrganizationsForEnterpriseMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await client.ListAllRunnerGroupOrganizationsForEnterprise("fake", 1);

                connection.Received().GetAll<OrganizationsResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runner-groups/1/organizations"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerGroupOrganizationsForEnterprise(null, 1, ApiOptions.None));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnerGroupOrganizationsForEnterprise("", 1, ApiOptions.None));
            }
        }

        public class TheListAllRunnerGroupRepositoriesForOrganizationMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnerGroupsClient(connection);

                await client.ListAllRunnerGroupRepositoriesForOrganization("fake", 1, ApiOptions.None);

                connection.Received().GetAll<RepositoriesResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runner-groups/1/repositories"), Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerGroupRepositoriesForOrganization(null, 1, ApiOptions.None));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnerGroupRepositoriesForOrganization("", 1, ApiOptions.None));
            }
        }
    }
}
