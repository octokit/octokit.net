using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ActionsSelfHostedRunnersClientTests
    {

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ActionsSelfHostedRunnersClient(null));
            }
        }

        public class TheListAllRunnersForEnterproseMethod
        {
            [Fact]
            public async Task RequstsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.ListAllRunnersForEnterprise("fake");

                connection.Received().GetAll<RunnerResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runners"), Args.ApiOptions);

            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnersForEnterprise(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnersForEnterprise(""));
            }
        }

        public class TheListAllRunnersForOrganizationMethod
        {
            [Fact]
            public async Task RequstsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.ListAllRunnersForOrganization("fake");

                connection.Received().GetAll<RunnerResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runners"), Args.ApiOptions);

            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnersForOrganization(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnersForOrganization(""));
            }
        }

        public class TheListAllRunnersForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.ListAllRunnersForRepository("fake", "repo");

                connection.Received().GetAll<RunnerResponse>(
                  Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners"), Args.ApiOptions);

            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnersForRepository(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnersForRepository("fake", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnersForRepository("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnersForRepository("fake", ""));
            }
        }


        public class TheListAllRunnerApplicationsForEnterpriseMethod
        {
            [Fact]
            public async Task RequstsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.ListAllRunnerApplicationsForEnterprise("fake");

                connection.Received().GetAll<RunnerApplication>(
                  Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runners/downloads"), Args.ApiOptions);

            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerApplicationsForEnterprise(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnerApplicationsForEnterprise(""));
            }
        }

        public class TheListAllRunnerApplicationsForOrganizationMethod
        {
            [Fact]
            public async Task RequstsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.ListAllRunnerApplicationsForOrganization("fake");

                connection.Received().GetAll<RunnerApplication>(
                  Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runners/downloads"), Args.ApiOptions);

            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerApplicationsForOrganization(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnerApplicationsForOrganization(""));
            }
        }

        public class TheListAllRunnerApplicationsForRepositoryMethod
        {
            [Fact]
            public async Task RequstsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.ListAllRunnerApplicationsForRepository("fake", "repo");

                connection.Received().GetAll<RunnerApplication>(
                  Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners/downloads"), Args.ApiOptions);

            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerApplicationsForRepository(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerApplicationsForRepository("fake", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnerApplicationsForRepository("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnerApplicationsForRepository("fake", ""));
            }
        }

        public class TheDeleteEnterpriseRunnerMethod
        {
            [Fact]
            public async Task RequstsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.DeleteEnterpriseRunner("fake", 1);

                connection.Received().Delete(
                  Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runners/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteEnterpriseRunner(null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteEnterpriseRunner("", 1));
            }
        }

        public class TheDeleteOrganizationRunnerMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.DeleteOrganizationRunner("fake", 1);

                connection.Received().Delete(
                  Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runners/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteOrganizationRunner(null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteOrganizationRunner("", 1));
            }
        }

        public class TheDeleteRepositoryRunnerMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.DeleteRepositoryRunner("fake", "repo", 1);

                connection.Received().Delete(
                  Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRepositoryRunner(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteRepositoryRunner("fake", null, 1));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRepositoryRunner("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteRepositoryRunner("fake", "", 1));
            }
        }

        public class TheCreateEnterpriseRegistrationTokenMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.CreateEnterpriseRegistrationToken("fake");

                connection.Received().Post<AccessToken>(
                  Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runners/registration-token"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateEnterpriseRegistrationToken(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateEnterpriseRegistrationToken(""));
            }
        }

        public class TheCreateOrganizationRegistrationTokenMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.CreateOrganizationRegistrationToken("fake");

                connection.Received().Post<AccessToken>(
                  Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runners/registration-token"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrganizationRegistrationToken(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrganizationRegistrationToken(""));
            }
        }

        public class TheCreateRepositoryRegistrationToken
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await client.CreateRepositoryRegistrationToken("fake", "repo");

                connection.Received().Post<AccessToken>(
                  Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners/registration-token"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateRepositoryRegistrationToken(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateRepositoryRegistrationToken("fake", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsSelfHostedRunnersClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateRepositoryRegistrationToken("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateRepositoryRegistrationToken("fake", ""));
            }
        }
    }
}
