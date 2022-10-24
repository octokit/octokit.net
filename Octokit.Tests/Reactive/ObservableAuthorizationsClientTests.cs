using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableAuthorizationsClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IGitHubClient>();
                var authEndpoint = new ObservableAuthorizationsClient(client);

                authEndpoint.GetAll();

                client.Connection.Received(1).Get<List<Authorization>>(Arg.Is<Uri>(u => u.ToString() == "authorizations"),
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0));
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOption()
            {
                var client = Substitute.For<IGitHubClient>();
                var authEndpoint = new ObservableAuthorizationsClient(client);

                authEndpoint.GetAll(ApiOptions.None);

                client.Connection.Received(1).Get<List<Authorization>>(Arg.Is<Uri>(u => u.ToString() == "authorizations"),
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var client = Substitute.For<IGitHubClient>();
                var authEndpoint = new ObservableAuthorizationsClient(client);

                await Assert.ThrowsAsync<ArgumentNullException>(() => authEndpoint.GetAll(null).ToTask());
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableAuthorizationsClient(null));
            }
        }
    }
}
