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

    public class TheListMethod
    {
      [Fact]
      public async Task RequestsCorrectUrl()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await client.List("fake", "repo");

        connection.Received().GetAll<Runner>(
          Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runners"));

      }

      [Fact]
      public async Task EnsuresNonNullArguments()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.List(null, "repo"));
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", null));
      }

      [Fact]
      public async Task EnsuresNonEmptyArguments()
      {
        var connection = Substitute.For<IApiConnection>();
        var client = new ActionsSelfHostedRunnersClient(connection);

        await Assert.ThrowsAsync<ArgumentException>(() => client.List("", "repo"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.List("fake", ""));
      }
    }


  }
}
