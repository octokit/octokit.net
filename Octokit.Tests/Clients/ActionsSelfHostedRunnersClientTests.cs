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

    public class TheGetMethod
    {
      [Fact]
      public async Task RequestsCorrectUrl()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        // Get some data
      }

      [Fact]
      public async Task EnsuresNonNullArguments()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo"));
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("fake", null));
      }

      [Fact]
      public async Task EnsuresNonEmptyArguments()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("fake", ""));
      }
    }


  }
}
