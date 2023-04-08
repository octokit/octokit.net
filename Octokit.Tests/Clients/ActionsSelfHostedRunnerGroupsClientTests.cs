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

    public class TheListAllRunnerGroupOrganizationsForEnterpriseMethod
    {
      [Fact]
      public async Task RequestsCorrectUrl()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnerGroupsClient(connection);

        await client.ListAllRunnerGroupOrganizationsForEnterprise("fake", 1);

        connection.Received().GetAll<Organization>(
          Arg.Is<Uri>(u => u.ToString() == "enterprises/fake/actions/runner-groups/1/organizations"), Args.ApiOptions);
      }

      [Fact]
      public async Task EnsuresNonNullArguments()
      {
        var client = new ActionsSelfHostedRunnerGroupsClient(Substitute.For<IApiConnection>());

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnerGroupOrganizationsForEnterprise(null ,1, ApiOptions.None));
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

        connection.Received().GetAll<Repository>(
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
