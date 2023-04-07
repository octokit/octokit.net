using System;
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

    public class TheListAllRunnersForOrganizationRunnerGroupMethod
    {
      [Fact]
      public async Task RequstsCorrectUrl()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await client.ListAllRunnersForOrganizationRunnerGroup("fake", 1);

        connection.Received().GetAll<RunnerResponse>(
          Arg.Is<Uri>(u => u.ToString() == "orgs/fake/actions/runner-groups/1/runners"), Args.ApiOptions);

      }

      [Fact]
      public async Task EnsuresNonNullArguments()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.ListAllRunnersForOrganizationRunnerGroup(null, 1));
      }

      [Fact]
      public async Task EnsuresNonEmptyArguments()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await Assert.ThrowsAsync<ArgumentException>(() => client.ListAllRunnersForOrganizationRunnerGroup("", 1));
      }
    }

    public class TheListAllRunnerApplicationsForEnterprise
    {
      [Fact]
      public async Task RequstsCorrectUrl()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await client.ListAllRunnerApplicationsForEnterprise("fake");

        connection.Received().GetAll<RunnerApplicationResponse>(
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

    public class TheListAllRunnerApplicationsForOrganization
    {
      [Fact]
      public async Task RequstsCorrectUrl()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await client.ListAllRunnerApplicationsForOrganization("fake");

        connection.Received().GetAll<RunnerApplicationResponse>(
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

    public class TheListAllRunnerApplicationsForRepository
    {
      [Fact]
      public async Task RequstsCorrectUrl()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await client.ListAllRunnerApplicationsForRepository("fake", "repo");

        connection.Received().GetAll<RunnerApplicationResponse>(
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

  }
}
